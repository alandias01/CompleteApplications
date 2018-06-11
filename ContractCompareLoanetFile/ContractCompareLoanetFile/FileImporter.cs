using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;

using WPFUtils;
using ContractCompareLoanetFile.ContractCompareDomestic;
using ContractCompareLoanetFile.ContractCompare_raw;
using ContractCompareLoanetFile.LoanetBook;


//alandias Commented out inserts

namespace ContractCompareLoanetFile
{
    public class FileImporter
    {
        List<LoanetBookObject> LoanetBooks;

        ContractCompare_rawFactory CCRF = new ContractCompare_rawFactory();

        public FileImporter()
        {            

            try
            {
                //string dir = Properties.Settings.Default.ImportFileDir + inputdate.ToString("yyyyMMdd");
                string dir = @"C:\dev\backoffice\ContractCompareLoanetFile\ContractCompareLoanetFile\bin\Debug";

                List<string> Allfiles = Directory.GetFiles(dir, "XX_ECO*").ToList();

                if (Allfiles.Count < 1)
                {
                    throw new Exception("There were no files to import");
                }

                //Filename must have length of 10
                List<string> FilesToImport = new List<string>();
                Allfiles.Where(x => Path.GetFileName(x).Length == 10).ToList().ForEach(FilesToImport.Add);

                if (FilesToImport.Count < 1)
                {
                    throw new Exception("There were files in dir but didn't have a length of 10");
                }

                ContractCompareDomesticFactory CCDF = new ContractCompareDomesticFactory();
                
                foreach (string f in FilesToImport)
                {
                    try
                    {
                        string[] RawData = File.ReadAllText(f).BreakStringIntoArrayEveryNspaces(1000);

                        #region Header Data
                        string RecordType_Header = RawData[0].Substring(0, 1);
                        string ParticipantID_Header = RawData[0].Substring(1, 8);
                        string FileID_Header = RawData[0].Substring(9, 8);
                        string FileFormatVersion_Header = RawData[0].Substring(17, 5);
                        string Filler__Header_01 = RawData[0].Substring(22, 6);
                        string FileDate_Header = RawData[0].Substring(28, 8);
                        string CompareZone_Header = RawData[0].Substring(36, 1);
                        string Filler_Header_02 = RawData[0].Substring(37, 963);
                        #endregion

                        DateTime FileDateOfData = DateTime.ParseExact(FileDate_Header, "MMddyyyy", CultureInfo.InvariantCulture);

                        CCRF.DeleteByDateAndBook(FileDateOfData, ParticipantID_Header.Substring(4, 4));
                        CCDF.DeleteByDateAndBook(FileDateOfData, ParticipantID_Header);
                        
                        List<ContractCompareDomesticObject> CCDAList = new List<ContractCompareDomesticObject>();

                        bool iterError = false;
                        //skip first line and last line for header and trailer                        
                        for (int i = 1; i < RawData.Count() - 1; i++)
                        {
                            #region Add Each item to CCDAList
                            ContractCompareDomesticObject CCDO = new ContractCompareDomesticObject();
                            try
                            {
                                CCDO.DateOfData = FileDateOfData;
                                CCDO.DateOfImport = DateTime.Now;

                                CCDO.RecordType = RawData[i].Substring(0, 1);
                                CCDO.ParticipantID = RawData[i].Substring(1, 8);
                                CCDO.ContraPartyID = RawData[i].Substring(9, 8);
                                CCDO.ActivityCode = RawData[i].Substring(17, 1);
                                CCDO.UserContractInternalReferenceNumber = RawData[i].Substring(18, 15);
                                CCDO.Filler1 = RawData[i].Substring(33, 5);
                                CCDO.SecurityID = RawData[i].Substring(38, 12);
                                CCDO.SecurityIDType = RawData[i].Substring(50, 1);
                                CCDO.OpenQuantity = RawData[i].Substring(51, 14);
                                CCDO.ContractValue = RawData[i].Substring(65, 18);
                                CCDO.RebateRateCode = RawData[i].Substring(83, 1);
                                CCDO.RebateFeeRate = RawData[i].Substring(84, 9);
                                CCDO.CollateralType = RawData[i].Substring(93, 1);
                                CCDO.Filler2 = RawData[i].Substring(94, 12);
                                CCDO.DeliveryDateofDeal = RawData[i].Substring(106, 8);
                                CCDO.CloseDateorTermDateofTransaction = RawData[i].Substring(114, 8);
                                CCDO.Filler3 = RawData[i].Substring(122, 8);
                                CCDO.UserContractInfo = RawData[i].Substring(130, 60);
                                CCDO.SunGardUseOnly1 = RawData[i].Substring(190, 9);
                                CCDO.ComparisonCode = RawData[i].Substring(199, 1);
                                CCDO.InternalAccountNumber = RawData[i].Substring(200, 16);
                                CCDO.Filler4 = RawData[i].Substring(216, 106);
                                CCDO.SunGardUseOnly2 = RawData[i].Substring(322, 184);
                                CCDO.MarginParameter = RawData[i].Substring(506, 6);
                                CCDO.RoundingDirection = RawData[i].Substring(512, 1);
                                CCDO.MarkRoundingFactor = RawData[i].Substring(513, 4);
                                CCDO.Filler5 = RawData[i].Substring(517, 5);
                                CCDO.AccruedBondInterestincludedinmarks = RawData[i].Substring(522, 1);
                                CCDO.Filler6 = RawData[i].Substring(523, 1);
                                CCDO.SunGardUseOnly3 = RawData[i].Substring(524, 60);
                                CCDO.DividendFlowThrough = RawData[i].Substring(584, 6);
                                CCDO.Filler7 = RawData[i].Substring(590, 1);
                                CCDO.IncomeTrackingIndicator = RawData[i].Substring(591, 1);
                                CCDO.SunGardUseOnly4 = RawData[i].Substring(592, 42);
                                CCDO.Filler8 = RawData[i].Substring(634, 12);
                                CCDO.SunGardUseOnly5 = RawData[i].Substring(646, 48);
                                CCDO.OCCHedgeContract = RawData[i].Substring(694, 1);
                                CCDO.CustodianID = RawData[i].Substring(695, 8);
                                CCDO.SubAccount = RawData[i].Substring(703, 35);
                                CCDO.Filler9 = RawData[i].Substring(738, 203);
                                CCDO.SunGardUseOnly6 = RawData[i].Substring(941, 59);

                                CCDAList.Add(CCDO);
                                CCDF.InsertSingle(CCDO);
                            }
                            catch (Exception e3)
                            {
                                iterError = true;
                                Utils.LogError("Issue with counter # " + i + " from RawData[I] in book " + ParticipantID_Header + " " + e3.Message);
                            }
                            #endregion
                        }

                        #region Footer Data
                        int ft = RawData.Count() - 1;
                        string RecordType_Footer = RawData[ft].Substring(0, 1);
                        string ParticipantID_Footer = RawData[ft].Substring(1, 8);
                        string DetailRecordCount_Footer = RawData[ft].Substring(9, 9);
                        string Filler_Footer = RawData[ft].Substring(18, 982);
                        #endregion

                        ConvertToOrigForExistingContractCompareRawTable(CCDAList);

                        if (iterError)
                        {
                            //send email log notifying that some lines were skipped and check app logs
                            throw new Exception("Some lines were skipped while iterating RawData[i]. Please check app logs");
                        }
                    }
                    catch (Exception e2)
                    {
                        Utils.LogError("ForEach(FilesToImport) " + e2.Message);
                        //send email log
                    }
                }
                
            }
            catch (Exception e)
            {
                Utils.LogError(e.Message);
                //Send Email Log
            }

        }

        private void ConvertToOrigForExistingContractCompareRawTablebak(List<ContractCompareDomesticObject> CCDAList)
        {
            foreach (ContractCompareDomesticObject ccdo in CCDAList)
            {
                #region Add item to Contract Compare Raw Object
                try
                {                    
                    ContractCompare_rawObject CCRO = new ContractCompare_rawObject();

                    CCRO.DateOfData = ccdo.DateOfData;
                    CCRO.DateOfImport = ccdo.DateOfImport;
                    CCRO.Record_Type = ccdo.RecordType; //'3';
                    CCRO.Receiving_Participant = FormatVal(ccdo.ParticipantID, 0, 4, "0000");
                    CCRO.Account_Number = FormatVal(ccdo.ContraPartyID, 0, 4, "0000");
                    CCRO.Borrow_Loan_Indicator = string.IsNullOrEmpty(ccdo.ActivityCode) ? " " : ccdo.ActivityCode;
                    CCRO.CUSIP_Number = FormatCusip(ccdo.SecurityID);
                    CCRO.Delivery_Date = FormatVal(ccdo.DeliveryDateofDeal, 4, 2, "000000");    //Convert Ex. 03212015 to 032115
                    CCRO.Quantity = FormatVal(ccdo.OpenQuantity, 0, 5, "000000000");
                    CCRO.Contract_Amount = FormatVal(ccdo.ContractValue, 0, 6, "000000000000");
                    CCRO.Rebate_Rate = ccdo.RebateFeeRate;  //Adjust in stored proc to read new length of 9 for US.  Already done for INTL
                    CCRO.Call_Rate = "00000";
                    CCRO.Rate_Code = string.IsNullOrEmpty(ccdo.RebateRateCode) ? " " : ccdo.RebateRateCode;

                    //MARK PARAMTER
                    if (!string.IsNullOrEmpty(ccdo.MarginParameter) && ccdo.MarginParameter.Length == 6)
                    {
                        string mp = ccdo.MarginParameter.Remove(4, 2);
                        mp = mp.Remove(0, 1);

                        CCRO.Mark_Parameter = mp;
                    }
                    else
                    {
                        CCRO.Mark_Parameter = " ";
                    }

                    //COLLATERAL TYPE
                    if (!string.IsNullOrEmpty(ccdo.CollateralType))
                    {
                        CCRO.NonCash_Collateral = ccdo.CollateralType.ToLower() == "c" ? " " : "N";
                    }
                    else
                    {
                        CCRO.NonCash_Collateral = " ";
                    }

                    CCRO.Mark_Rounding_Factor = string.IsNullOrEmpty(ccdo.MarkRoundingFactor) ? "X" : ConvertMarkRoundingTo80ByteSpec(ccdo.MarkRoundingFactor);
                    CCRO.Accrued_Interest_for_Bounds = ccdo.AccruedBondInterestincludedinmarks;
                    CCRO.Uncompared_Advisory = ccdo.ComparisonCode; // W,T,M

                    if (!string.IsNullOrEmpty(ccdo.UserContractInternalReferenceNumber))
                    {
                        string bgnref = ccdo.UserContractInternalReferenceNumber.Trim();
                        bgnref = bgnref.PadRight(15);
                        CCRO.User_Contract_Information = bgnref;
                    }
                    else
                    {
                        CCRO.User_Contract_Information = ccdo.UserContractInternalReferenceNumber;
                    }


                    //INCOME TRACKING
                    if (!string.IsNullOrEmpty(ccdo.IncomeTrackingIndicator))
                    {
                        CCRO.Income_Tracking_Indicator = ccdo.IncomeTrackingIndicator.ToLower() == "N" ? "N" : " ";
                    }
                    else
                    {
                        CCRO.Income_Tracking_Indicator = " ";
                    }

                    //DIV PERCENT
                    string DivFlow = FormatVal(ccdo.DividendFlowThrough, 3, 3, "0");
                    DivFlow = DivFlow == "000" ? "0" : DivFlow;
                    DivFlow = DivFlow != "0" ? DivFlow.TrimStart("0".ToCharArray()) : DivFlow;
                    CCRO.DivPercentage = DivFlow;

                    CCRF.InsertSingle(CCRO);
                    
                }
                catch (Exception e)
                {
                    Utils.LogError(e.Message);
                }
                #endregion
            }

        }

        private void ConvertToOrigForExistingContractCompareRawTable(List<ContractCompareDomesticObject> CCDAList)
        {
            bool iterError = false;
            
            for(int i=0;i<CCDAList.Count;i++)
            {
                #region Add item to Contract Compare Raw Object
                try
                {
                    ContractCompare_rawObject CCRO = new ContractCompare_rawObject();

                    CCRO.DateOfData = CCDAList[i].DateOfData;
                    CCRO.DateOfImport = CCDAList[i].DateOfImport;
                    CCRO.Record_Type = CCDAList[i].RecordType; //'3';
                    CCRO.Receiving_Participant = FormatVal(CCDAList[i].ParticipantID, 0, 4, "0000");
                    CCRO.Account_Number = FormatVal(CCDAList[i].ContraPartyID, 0, 4, "0000");
                    CCRO.Borrow_Loan_Indicator = string.IsNullOrEmpty(CCDAList[i].ActivityCode) ? " " : CCDAList[i].ActivityCode;
                    CCRO.CUSIP_Number = FormatCusip(CCDAList[i].SecurityID);
                    CCRO.Delivery_Date = FormatVal(CCDAList[i].DeliveryDateofDeal, 4, 2, "000000");    //Convert Ex. 03212015 to 032115
                    CCRO.Quantity = FormatVal(CCDAList[i].OpenQuantity, 0, 5, "000000000");
                    CCRO.Contract_Amount = FormatVal(CCDAList[i].ContractValue, 0, 6, "000000000000");
                    CCRO.Rebate_Rate = CCDAList[i].RebateFeeRate;  //Adjust in stored proc to read new length of 9 for US.  Already done for INTL
                    CCRO.Call_Rate = "00000";
                    CCRO.Rate_Code = string.IsNullOrEmpty(CCDAList[i].RebateRateCode) ? " " : CCDAList[i].RebateRateCode;

                    //MARK PARAMTER
                    if (!string.IsNullOrEmpty(CCDAList[i].MarginParameter) && CCDAList[i].MarginParameter.Length == 6)
                    {
                        string mp = CCDAList[i].MarginParameter.Remove(4, 2);
                        mp = mp.Remove(0, 1);

                        CCRO.Mark_Parameter = mp;
                    }
                    else
                    {
                        CCRO.Mark_Parameter = " ";
                    }

                    //COLLATERAL TYPE
                    if (!string.IsNullOrEmpty(CCDAList[i].CollateralType))
                    {
                        CCRO.NonCash_Collateral = CCDAList[i].CollateralType.ToLower() == "c" ? " " : "N";
                    }
                    else
                    {
                        CCRO.NonCash_Collateral = " ";
                    }

                    CCRO.Mark_Rounding_Factor = string.IsNullOrEmpty(CCDAList[i].MarkRoundingFactor) ? "X" : ConvertMarkRoundingTo80ByteSpec(CCDAList[i].MarkRoundingFactor);
                    CCRO.Accrued_Interest_for_Bounds = CCDAList[i].AccruedBondInterestincludedinmarks;
                    CCRO.Uncompared_Advisory = CCDAList[i].ComparisonCode; // W,T,M

                    if (!string.IsNullOrEmpty(CCDAList[i].UserContractInternalReferenceNumber))
                    {
                        string bgnref = CCDAList[i].UserContractInternalReferenceNumber.Trim();
                        bgnref = bgnref.PadRight(15);
                        CCRO.User_Contract_Information = bgnref;
                    }
                    else
                    {
                        CCRO.User_Contract_Information = CCDAList[i].UserContractInternalReferenceNumber;
                    }


                    //INCOME TRACKING
                    if (!string.IsNullOrEmpty(CCDAList[i].IncomeTrackingIndicator))
                    {
                        CCRO.Income_Tracking_Indicator = CCDAList[i].IncomeTrackingIndicator.ToLower() == "N" ? "N" : " ";
                    }
                    else
                    {
                        CCRO.Income_Tracking_Indicator = " ";
                    }

                    //DIV PERCENT
                    string DivFlow = FormatVal(CCDAList[i].DividendFlowThrough, 3, 3, "0");
                    DivFlow = DivFlow == "000" ? "0" : DivFlow;
                    DivFlow = DivFlow != "0" ? DivFlow.TrimStart("0".ToCharArray()) : DivFlow;
                    CCRO.DivPercentage = DivFlow;

                    CCRF.InsertSingle(CCRO);

                }
                catch (Exception e)
                {
                    iterError = true;
                    string pid = CCDAList[i].ParticipantID ?? "XXXX";
                    Utils.LogError("Issue with counter # " + i + " from CCDAList object in book " + pid + " " + e.Message);
                }
                #endregion
            }

            if (iterError)
            {
                //send email log notifying that some lines were skipped and check app logs 
            }            

        }

        //Try to pass in whole object to log bgnref if it fails
        public string FormatVal(string val, int startIndexToRemove, int LengthToRemove, string PreferredDefaultValue)
        {
            if (!string.IsNullOrEmpty(val))
            {
                if (val.Length >= startIndexToRemove + LengthToRemove)
                {
                    val = val.Remove(startIndexToRemove, LengthToRemove);
                }
                else
                {
                    val = string.IsNullOrEmpty(PreferredDefaultValue) ? "X" : PreferredDefaultValue;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(PreferredDefaultValue))
                {
                    val = PreferredDefaultValue;
                }
                else
                {
                    val = "X";
                }
            }

            return val;
        }

        public string ConvertMarkRoundingTo80ByteSpec(string MarkRoundFactor)
        {
            switch (MarkRoundFactor)
            {
                case "0000": return "E";
                case "0050": return "5";
                case "0100": return "1";
                case "0125": return "8";
                case "0250": return "4";
                case "0500": return "H";
                case "1000": return "U";
                case "": return "";

                default: return "X";

            }
        }

        private string FormatCusip(string cusip)
        {
            //If has value
            if (!string.IsNullOrEmpty(cusip))
            {
                cusip = cusip.Trim();

                if (cusip.Length >= 9)
                {
                    if (cusip.Length == 12)
                    {
                        cusip = cusip.Substring(2, 9);  //Could be an ISIN
                    }
                    else if (cusip.Length == 9)
                    {
                        //Do Nothing.  It's a CUSIP
                    }
                    else
                    {
                        cusip = cusip.Substring(0, 9);  //May be an error.  Cut it and Log
                    }

                }
                else
                {
                    //Do nothing.  It may be a CINS                    
                }
            }

            //If Empty
            else
            {
                cusip = "000000000";
            }

            return cusip;
        }

    }
}
