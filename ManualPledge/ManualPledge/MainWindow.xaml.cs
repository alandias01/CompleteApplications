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
using System.IO;
using System.Data.OleDb;

namespace ManualPledge
{
    /* What if sheet doesn't have a cusip in the middle row, or qty
     * Create Force option where it doesn't check price in slate
     * 
     *      
     */
    public partial class MainWindow : Window
    {
        
        PledgeSystem ps = new PledgeSystem();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonPledge_Click(object sender, RoutedEventArgs e)
        {
            PledgeItemsOnGrid();
        }


        //Skips item with no cusip

        private void PledgeItemsOnGrid()
        {
            Dictionary<string, int> STP = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            
            List<CSVDragObject> cs = dataGridPledge.ItemsSource as List<CSVDragObject>;

            if (DoesGridHaveErrors(cs))
            {
                return;
            }

            List<CSVDragObject> csrejects = new List<CSVDragObject>();

            //Checks if cusip is in Slate.stock.  Do we care?
            //We won't take in empty cusips
            foreach (CSVDragObject csitem in cs)
            {
                if (string.IsNullOrEmpty(csitem.CUSIP))
                {
                    continue;
                }

                var r = ps.StockList.Find(x => x.Cusip.ToUpper() == csitem.CUSIP.ToUpper());
                if (r != null)
                {                    
                    int qty = int.Parse(csitem.Qty);
                    if (STP.ContainsKey(csitem.CUSIP))
                    {
                        MessageBox.Show("Duplicate Cusips on Grid, exiting");
                        return;
                    }
                    STP.Add(csitem.CUSIP, qty);
                }

                else
                {
                    csrejects.Add(csitem);
                }
            }
            string csrejects_string = string.Join("\n", csrejects.Select(x => x.CUSIP).ToArray());

            if (csrejects.Count > 0)
            {
                MessageBoxResult mbr_csrejects = MessageBox.Show("These items won't be pledged because we can't find the price for this CUSIP.  Pledge everything else? \n " + csrejects_string, "Confirm", MessageBoxButton.OKCancel);

                if (mbr_csrejects == MessageBoxResult.Cancel)
                {
                    return;
                }
            }

            ps.PledgeStocks(STP, "ManualPledge", "0269", checkBoxCreateApexTrade.IsChecked.Value, checkBoxSendToOcc.IsChecked.Value);
            
            MessageBox.Show("Completed");

        }

        /// <summary>
        /// Checks 
        /// Is there anything on the grid
        /// Is at least 1 check box checked
        /// Is the qty a number
        /// Just skips over any cusip not there
        /// </summary>
        /// <param name="cs"></param>
        /// <returns></returns>

        private bool DoesGridHaveErrors(List<CSVDragObject> cs)
        {
            if (cs == null)
            {
                MessageBox.Show("Nothing on Grid");
                return true;
            }

            if (checkBoxSendToOcc.IsChecked == false && checkBoxCreateApexTrade.IsChecked == false)
            {
                MessageBox.Show("You must check either Send To Occ and/or Create Apex Trade");
                return true;
            }

            if (cs.Count < 1)
            {
                MessageBox.Show("There are no items");
                return true;
            }

            foreach (CSVDragObject item in cs)
            {
                /*  FOr now, we can just skip items where there is no cusip
                if (string.IsNullOrEmpty(item.CUSIP))
                {
                    MessageBox.Show("Make sure every row has a value for cusip");
                    return true;
                }
                */

                int tmp;
                if (!int.TryParse(item.Qty, out tmp))
                {
                    MessageBox.Show("The qty for cusip " + item.CUSIP + " is not reading in as a number");
                    return true;
                }
            }

            return false;
        }

        private void dataGridPledge_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void dataGridPledge_Drop(object sender, DragEventArgs e)
        {
            string[] Files = (string[])e.Data.GetData(DataFormats.FileDrop);

            string ext = System.IO.Path.GetExtension(Files[0]).ToLower();

            switch (ext)
            {
                case ".csv": dataGridPledge_Drop_csv(Files[0]);
                    break;
                case ".xls": dataGridPledge_Drop_xls(Files[0]);
                    break;
                default: MessageBox.Show("you can only import csv or xls type file");
                    break;
            }

            
        }

        private void dataGridPledge_Drop_csv(string file)
        {
            List<string> FileLines = new List<string>();
            FileLines = File.ReadAllLines(file).ToList();

            List<CSVDragObject> CSVDOList = new List<CSVDragObject>();

            foreach (string FileLine in FileLines)
            {
                string[] ColumnsInLine = FileLine.Split(",".ToCharArray());
                CSVDOList.Add(new CSVDragObject(ColumnsInLine[0], ColumnsInLine[1]));
            }

            dataGridPledge.ItemsSource = null;
            dataGridPledge.ItemsSource = CSVDOList;
        }

        private void dataGridPledge_Drop_xls(string file)
        {
            OleDbConnection con = new OleDbConnection();
            con.ConnectionString = "Provider=Microsoft.jet.OLEDB.4.0;Data Source=" + file + ";Extended Properties=\"Excel 8.0;IMEX=1;HDR=no;\"";

            //pass query
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandText = "select * from [Sheet1$]";
            cmd.Connection = con;

            //Read Data
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            System.Data.DataSet ds = new System.Data.DataSet();
            da.Fill(ds, "demo");

            System.Data.DataTable dt = ds.Tables[0];
            List<CSVDragObject> CSVDOList = new List<CSVDragObject>();

            foreach (System.Data.DataRow r in dt.Rows)
            {
                CSVDOList.Add(new CSVDragObject(r[0].ToString(), r[1].ToString()));
            }

            dataGridPledge.ItemsSource = null;
            dataGridPledge.ItemsSource = CSVDOList;
        }

        public class CSVDragObject
        {
            public string CUSIP { get; set; }
            public string Qty { get; set; }

            public CSVDragObject(string cusip, string qty)
            {
                CUSIP = cusip;
                Qty = qty;
            }
        }
                
    }
}
