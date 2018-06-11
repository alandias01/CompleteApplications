using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ContractCompareEquilendFile.ve_g1position_gql;
using WPFUtils;
using System.Threading.Tasks;
using System.ComponentModel;

using System.Reflection;
using System.IO;
using LoadingControl.Control;
using Data.Equilend;


namespace ContractCompareEquilendFile
{
        
    /// <summary>    
    /// Documentation: \\maplenj\dfs\BusinessGrps\All\CallLog\Other Application\documentation\8150
    /// 
    /// Helpful Notes
    /// If you have rate of 3%, it would be .03  You don't have to prepend or trail with zeros
    /// </summary>
    public partial class MainWindow : Window
    {
        /* Why I used AsyncObservableCollection 
         * When you fill data into ObservableCollection on another thread, the collection raises its CollectionChanged event 
         * on the same thread that caused the change which means controls are being updated from a background thread. 
         * AsyncObservableCollection raises it on the main thread (UI Thread). 
         * If you don't want to use AsyncObservableCollection.  Fill a new collection in the background and then copy that 
         * data into the databound collection EQData, or just remove background worker all together. Boo!
        */

        //Data Bound Items
        public AsyncObservableCollection<EquilendExcelDataObjectGrid> EQData { get; set; }
        public AsyncObservableCollection<string> EQDataLegalEntities { get; set; }

        private string comboBoxSelectedLegalEntity;

        public string ComboBoxSelectedLegalEntity
        {
            get { return comboBoxSelectedLegalEntity; }
            set
            {
                comboBoxSelectedLegalEntity = value;
                //RaisePropertyChanged("PropertyName");
            }
        }

        LoadingAnimation LA1 = new LoadingAnimation();

        public MainWindow()
        {

            InitializeComponent();
            this.DataContext = this;

            EQData = new AsyncObservableCollection<EquilendExcelDataObjectGrid>();
            EQDataLegalEntities = new AsyncObservableCollection<string>();

            DGMenu<EquilendExcelDataObjectGrid> DGMdataGridEQData = new DGMenu<EquilendExcelDataObjectGrid>();
            DGMdataGridEQData.SetGrid(dataGridEQData);

            //LoadLegalentities();

        }

        private void LoadBusy()
        {
            LA1.VerticalAlignment = VerticalAlignment.Center;
            LA1.HorizontalAlignment = HorizontalAlignment.Center;

            Grid.SetColumn(LA1, 0);
            Grid.SetColumnSpan(LA1, 2);
            Grid.SetRow(LA1, 1);
            GridMain.Children.Add(LA1);
        }

        private void UnloadBusy()
        {
            GridMain.Children.Remove(LA1);
        }

        private void buttonLoadData_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            DateTime EqDate;

            if (datePickerEqDate.SelectedDate.HasValue)
            {
                EqDate = datePickerEqDate.SelectedDate.Value;
            }
            else
            {
                MessageBox.Show("Please pick a date");
                return;
            }

            string EQLegalEntity = "5773";

            buttonLoadData.IsEnabled = false;

            LoadBusy();

            BackgroundWorker BW = new BackgroundWorker();
            BW.DoWork += (s, t) =>
            {
                GetEQData(EqDate, EQLegalEntity);
            };

            BW.RunWorkerCompleted += (s, t) =>
            {
                if (t.Error != null)
                {
                    MessageBox.Show(t.Error.Message);
                }

                buttonLoadData.IsEnabled = true;
                UnloadBusy();
            };

            BW.RunWorkerAsync();
        }

        private void GetEQData(DateTime EqDate, string EQLegalEntity)
        {
            var HPositionDataRaw = new List<ve_g1position_gqlObject>();
            GetEquilendDataFromSource(HPositionDataRaw, EqDate);

            EQData.Clear();
            EQDataLegalEntities.Clear();

            //5773 is our Equilend legal Entity            
            PopulateEQDataObject(HPositionDataRaw, EQData, EQLegalEntity);

            //Populate the ComboBox.  It's databound            
            EQDataLegalEntities.AddRange(EQData.Select(x => x.CPTY_LEGAL_ENTITY_ID).Distinct().ToList());

            if (EQData.Count == 0)
            {
                MessageBox.Show("No Data");
                return;
            }

            EQData.OrderBy(x => x.CPTY_LEGAL_ENTITY_ID);
        }

        private void ButtonExportExcel_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxSelectedLegalEntity == null)
            {
                MessageBox.Show("Please select a Legal Entity");
                return;
            }

            List<EquilendExcelDataObjectSingleCtpy> EQDataSingle = new List<EquilendExcelDataObjectSingleCtpy>();
            foreach (var item in EQData)
            {
                EQDataSingle.Add(new EquilendExcelDataObjectSingleCtpy(item));
            }


            List<EquilendExcelDataObjectSingleCtpy> FilteredEQData = EQDataSingle.Where(x => x.CPTY_LEGAL_ENTITY_ID == ComboBoxSelectedLegalEntity).ToList();

            var Exporter = new ExportToExcelLocal<EquilendExcelDataObjectSingleCtpy, List<EquilendExcelDataObjectSingleCtpy>>();
            Exporter.dataToPrint = FilteredEQData;

            Exporter.GenerateReport();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            List<EquilendExcelDataObjectTimeTable> EQDataTimeTable = new List<EquilendExcelDataObjectTimeTable>();
            foreach (var item in EQData)
            {
                EQDataTimeTable.Add(new EquilendExcelDataObjectTimeTable(item));
            }


            var Exporter = new ExportToExcelLocal<EquilendExcelDataObjectTimeTable, List<EquilendExcelDataObjectTimeTable>>();
            Exporter.dataToPrint = EQDataTimeTable;

            Exporter.GenerateReport();

        }

        private void GetEquilendDataFromSource(List<ve_g1position_gqlObject> HPositionDataRaw, DateTime EqDate)
        {
            var vefaq = new ve_g1position_gqlFactory();
            string DateofData = EqDate.ToString("yyyyMMdd");
            vefaq.Get_ve_g1position_gql_byDateAndBook(HPositionDataRaw, DateofData, "APEX-MS5239", 3);
        }

        private void PopulateEQDataObject(List<ve_g1position_gqlObject> raw, AsyncObservableCollection<EquilendExcelDataObjectGrid> EQData, string LegalEntity)
        {
            foreach (var ri in raw)
            {
                try
                {
                    EquilendExcelDataObjectGrid EQ = new EquilendExcelDataObjectGrid();

                    //Just for grid
                    EQ.Cpty = ri.CPTY;
                    EQ.Name = ri.NAME;
                    EQ.CtpyAgentAccount = ri.CtpyAgentAccount;
                    EQ.TIMETABLE_ID = ri.TimeTableID.ToString();

                    EQ.EQUILEND_TXN_ID = "";

                    //Required
                    EQ.LEGAL_ENTITY_ID = LegalEntity;
                    EQ.CPTY_LEGAL_ENTITY_ID = ri.LoanNet_Counterparty_Id;   //Stored proc changes this to equilend id
                    EQ.BORROW_LOAN_TYPE_CD = ri.BL;

                    EQ.SUBACCOUNT_ID = "";

                    //Required
                    EQ.SECURITY_ID = string.IsNullOrEmpty(ri.CUSIP) ? ri.SEDOL : ri.CUSIP;
                    EQ.SECURITY_ID_TYPE_CD = string.IsNullOrEmpty(ri.CUSIP) ? "S" : "C";    //I,S,C,Q,V

                    EQ.RATE_TYPE_CD = "";
                    EQ.RATE_AMT = (ri.CASH != null && ri.CASH.ToLower() == "c") ? ri.LNRATE.Value.ToString() : EQ.RATE_AMT;
                    EQ.FEE_TYPE_CD = "";
                    
                    //EQ.FEE_AMT = (ri.CASH != null && ri.CASH.ToLower() == "n" && ri.COLL_FLG != null && ri.COLL_FLG.ToLower() == "t") ? ri.LNRATE.Value.ToString() : EQ.FEE_AMT;

                    if (ri.CASH != null && ri.CASH.ToLower() == "n")
                    {
                        EQ.FEE_AMT = ri.LNRATE.Value.ToString();                        
                    }
                    else
                    {
                        EQ.FEE_AMT = EQ.FEE_AMT;
                    }

                    //if cash n and coll flg c then 

                    EQ.DIVIDEND_RATE_PCT = ri.DIV_AGE.ToString();
                    EQ.DIVIDEND_TRACKING_IND = ri.LNT_DIV_TRACKING;


                    switch (ri.CASH.ToUpper())
                    {
                        case "C": EQ.COLLATERAL_TYPE_CD = "CA";
                            break;
                        case "N": EQ.COLLATERAL_TYPE_CD = "NC";
                            break;
                        case "T": EQ.COLLATERAL_TYPE_CD = "CP";
                            break;
                        default: EQ.COLLATERAL_TYPE_CD = "CA";
                            break;
                    }

                    EQ.BILLING_FX_RATE = "";
                    EQ.BILLING_DERIVATION_IND = "";
                    EQ.COLLATERAL_CURRENCY_CD = ri.LNCUR;
                    EQ.CALLABLE_IND = ri.CALL;   //if callabledate is set, this must be set to y
                    EQ.SETTLEMENT_DT = ri.SSET_DT.HasValue ? ri.SSET_DT.Value.ToString("yyyyMMdd") : "";

                    //Required  //CT=Contract, RT=Return, RC=Recall, CL=Collateral                    
                    switch (ri.COLL_FLG.ToUpper())
                    {
                        case "T": EQ.COMPARE_RECORD_TYPE_CD = "CT";
                            break;
                        case "C": EQ.COMPARE_RECORD_TYPE_CD = "CL";
                            break;
                        default: EQ.COMPARE_RECORD_TYPE_CD = EQ.COMPARE_RECORD_TYPE_CD;
                            break;
                    }


                    EQ.UNIT_QTY = ri.QTY.ToString();

                    //Required
                    switch (ri.OP.ToUpper())
                    {
                        case "O": EQ.ORDER_STATE_CD = "OP";
                            break;
                        case "P": EQ.ORDER_STATE_CD = "PS";
                            break;
                        default: EQ.ORDER_STATE_CD = "OP";
                            break;
                    }
                    EQ.ORDER_STATE_CD = ri.OP == "O" ? "OP" : "PS";

                    EQ.PREPAY_RATE_PCT = ri.CRATE.HasValue ? ri.CRATE.ToString() : "";
                    EQ.CASH_PAYMENT_AMT = ri.LNVAL.ToString();
                    EQ.BILLING_VALUE_AMT = ri.LNVAL.ToString();
                    EQ.BILLING_CURRENCY_CD = "";
                    EQ.COLLATERAL_DESC_CD = "";

                    if (ri.QTY > 0)
                    {
                        EQ.CONTRACT_PRICE_AMT = Math.Round(ri.LNVAL.Value / ri.QTY.Value, 6).ToString();
                        //EQ.CONTRACT_PRICE_AMT = (ri.LNVAL / ri.QTY).ToString();
                    }
                    else
                    {
                        EQ.CONTRACT_PRICE_AMT = 0.ToString();
                    }



                    EQ.COLLATERAL_MARGIN_PCT = ri.LNMRG.ToString();
                    EQ.CONTRACT_VALUE_AMT = ri.LNVAL.ToString();
                    EQ.TRADE_DT = ri.TRADE.HasValue ? ri.TRADE.Value.ToString("yyyyMMdd") : "";
                    EQ.COLLATERAL_DT = "";
                    EQ.TERM_DT = ri.TERMDT.HasValue ? ri.TERMDT.Value.ToString("yyyyMMdd") : EQ.TERM_DT;
                    EQ.TERM_TYPE_CD = "";
                    EQ.HOLD_DT = "";
                    EQ.CALLABLE_DT = "";
                    EQ.RESET_INTERVAL_DAYS = "";
                    EQ.REBATE_RECEIVABLE_AMT = "";
                    EQ.REBATE_PAYABLE_AMT = "";
                    EQ.FEE_RECEIVABLE_AMT = "";
                    EQ.FEE_PAYABLE_AMT = "";
                    EQ.RATE_ADJUST_DT = "";
                    EQ.BUYIN_DT = "";
                    EQ.TERMINATION_IND = "";
                    EQ.BORROWER_SETTLE_INSTRUC_ID = "";
                    EQ.LENDER_SETTLE_INSTRUC_ID = "";
                    EQ.SETTLEMENT_TYPE_CD = "";
                    EQ.MARKING_PARAMETERS = "";

                    //Required
                    EQ.INTERNAL_REF_ID = ri.BGNREF;

                    EQ.COUNTERPARTY_REF_ID = "";
                    EQ.CORPORATE_ACTION_TYPE = "";  //Corp Actions
                    EQ.EX_DT = "";                  //Corp Actions
                    EQ.RECORD_DT = "";              //Corp Actions
                    EQ.INTERNAL_CUSTOM_FIELD = "";
                    EQ.EXTERNAL_CUSTOM_FIELD = "";
                    EQ.OLD_EQUILEND_TXN_ID = "";
                    EQ.BILLING_PRICE_AMT = "";
                    EQ.BILLING_MARGIN_PCT = "";
                    EQ.COLLATERAL_VALUE_AMT = ri.LNVAL.ToString();
                    EQ.EQUILEND_RETURN_ID = "";
                    EQ.RETURN_TRADE_DT = "";
                    EQ.RETURN_SETTLEMENT_DT = "";
                    EQ.EQUILEND_RECALL_ID = "";
                    EQ.RECALL_EFFECTIVE_DT = "";
                    EQ.RECALL_DUE_DT = "";
                    EQ.REASON_CD = "";


                    EQData.Add(EQ);
                }
                catch (Exception e)
                {
                    throw;
                }

            }


        }


        #region Belongs to Legal Entity Tab
        /*
        private void buttonLegalEntityRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadLegalentities();
        }
        
        
        private void LoadLegalentities()
        {
            EquilendEntities eqe = new EquilendEntities();

            var q = from i in eqe.DTCMaps
                    join j in eqe.LegalEntityContacts on i.LegalEntity equals j.LegalEntity into t
                    from rt in t.DefaultIfEmpty() orderby i.DTC
                    select new
                    {
                        i.DTC,
                        i.LegalEntity,
                        rt.Name,
                        rt.Role,
                        rt.Email,
                        rt.PhoneNumber,
                        rt.NumberType
                    } ;

            dataGridLegalEntities.ItemsSource = q;
            
        }
        */
        #endregion


        //Not being used.  Just in case you want to implement it
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }

    public static class EquilendCodeForStoredProc
    {
        public static void CreateFileWithSPDate()
        {
            PropertyInfo[] pi = typeof(EquilendExcelDataObjectSingleCtpy).GetProperties();
            List<string> data = new List<string>();

            int i = 1;

            data.Add("CREATE TABLE #EQUILENDTable(");

            foreach (var pt in pi)
            {
                string n = pt.Name + " varchar(150),";
                data.Add(n.PadRight(50) + "--" + i.ToString());
                i++;
            }
            data.Add(")");


            i = 1;

            data.Add("");
            data.Add("INSERT INTO #EQUILENDTable(");

            foreach (var pt in pi)
            {
                string n = pt.Name + ",";
                data.Add(n.PadRight(50) + "--" + i.ToString());
                i++;
            }
            data.Add(")");
            data.Add("VALUES (" + Environment.NewLine + Environment.NewLine + ")");


            File.WriteAllLines(AppDomain.CurrentDomain.BaseDirectory + "/AddToSP.txt", data.ToArray());
        }
    }

}
