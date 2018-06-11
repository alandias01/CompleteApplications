using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContractCompareLoanetFile.spHeliumDataForContractCompareLoanetFile;
using WPFUtils;

namespace ContractCompareLoanetFile
{

    //Object Creator is responsible for 3 things.
    //Step 1. Gets raw data
    //Step 2. Applies business rules to it
    //Step 3. Provides the data for the file

    public class Z4ObjectCreator : IObjectCreator
    {   
        List<Z4FileSpec> Z4List;
        
        public Z4ObjectCreator(DateTime inputdate, string bookname, string booknumber, int LoanNet_Zone, ref bool SendTextOnError)
        {
            spHeliumDataForContractCompareLoanetFileFactory sph = new spHeliumDataForContractCompareLoanetFileFactory();
            List<spHeliumDataForContractCompareLoanetFileObject> spo = new List<spHeliumDataForContractCompareLoanetFileObject>();

            //Step 1
            sph.Get_spHeliumDataForContractCompareLoanetFile_byDateAndBook(spo, inputdate.ToString("yyyyMMdd"), bookname, LoanNet_Zone, ref SendTextOnError);

            //Step 2            
            Z4List = ApplyBusinessRulesToRawData(spo, booknumber, inputdate, LoanNet_Zone);
        }
        
        
        

        private List<Z4FileSpec> ApplyBusinessRulesToRawData(List<spHeliumDataForContractCompareLoanetFileObject> spo, string BookNumber, DateTime inputdate, int compareZone)
        {
            //Z4FileSpec.CreateHeader(BookNumber, inputdate, compareZone);

            string RecordType = "1";
            string ParticipantID = GR.getval1(BookNumber, 8, true, '0', GR.GetPName(() => BookNumber), "Header"); //participantID.PadLeft(8, '0');
            string FileID = "COMPAREI";
            string FileFormatVersion = "01.00";
            string Filler1 = GR.ReturnFiller(6);
            string FileDate = inputdate.ToString("MMddyyyy");
            string CompareZone = GR.SanitizeNumber(compareZone.ToString(), 1, false, 0, false); // compareZone.ToString();
            string Filler2 = GR.ReturnFiller(963);
            Z4FileSpec.Header = RecordType + ParticipantID + FileID + FileFormatVersion + Filler1 + FileDate + CompareZone + Filler2;


            List<Z4FileSpec> FileData = new List<Z4FileSpec>();

            foreach (var SpoItem in spo)
            {
                Z4FileSpec Z4FI = new Z4FileSpec();

                #region Create Z4FI
                try
                {
                    Z4FI.RecordType = "2";
                    Z4FI.ParticipantID = GR.getval1(BookNumber, 8, true, '0', GR.GetPName(() => BookNumber), SpoItem.BGNREF);
                    Z4FI.ContraPartyID = GR.getval1(SpoItem.LoanNet_Counterparty_Id, 8, true, '0', GR.GetPName(() => SpoItem.LoanNet_Counterparty_Id), SpoItem.BGNREF);
                    Z4FI.ActivityCode = GR.getval1(SpoItem.BL, 1, true, '0', GR.GetPName(() => SpoItem.BL), SpoItem.BGNREF);
                    Z4FI.UserContractInternalReferenceNumber = GR.getval1(SpoItem.BGNREF, 15, false, ' ', GR.GetPName(() => SpoItem.BGNREF), SpoItem.BGNREF);
                    Z4FI.Filler = GR.ReturnFiller(5);
                    Z4FI.SecurityID = GR.getval1(SpoItem.CUSIP, 12, false, ' ', GR.GetPName(() => SpoItem.CUSIP), SpoItem.BGNREF);
                    Z4FI.SecurityIDType = "C";
                    Z4FI.OpenQuantity = GR.SanitizeNumber(SpoItem.QTY.ToString(), 14, true, 0, false);
                    Z4FI.ContractValue = GR.SanitizeNumber(SpoItem.LNVAL.ToString(), 16, true, 2, false);

                    Z4FI.RebateRateCode = SpoItem.LNRATE < 0 ? "N" : " ";

                    Z4FI.RebateFeeRate = GR.SanitizeNumber(Math.Abs(SpoItem.LNRATE.Value).ToString(), 3, true, 6, false);
                    Z4FI.CollateralType = GR.getval1(SpoItem.CASH, 1, true, '0', GR.GetPName(() => SpoItem.CASH), SpoItem.BGNREF);
                    Z4FI.Filler1 = GR.ReturnFiller(12);
                    Z4FI.DeliveryDateofDeal = SpoItem.SSET_DT.HasValue ? SpoItem.SSET_DT.Value.ToString("MMddyyyy") : GR.ReturnFiller(8, '0');
                    Z4FI.CloseDateorTermDateofTransaction = SpoItem.TERMDT.HasValue ? SpoItem.TERMDT.Value.ToString("MMddyyyy") : GR.ReturnFiller(8, '0');
                    Z4FI.UserContractInfo = GR.ReturnFiller(60);
                    Z4FI.Filler2 = GR.ReturnFiller(102);
                    Z4FI.SunGardUseOnly1 = "N";
                    Z4FI.InternalAccountNumber = GR.getval1(SpoItem.LoanNet_Counterparty_Id, 16, true, '0', GR.GetPName(() => SpoItem.LoanNet_Counterparty_Id), SpoItem.BGNREF);
                    Z4FI.SunGardUseOnly2 = GR.ReturnFiller(205);

                    /*          Mark to Market Section          */
                    Z4FI.MarginParameter = GR.SanitizeNumber(SpoItem.LNMRG.ToString(), 4, true, 2, false);
                    Z4FI.RoundingDirection = "U";
                    Z4FI.MarkRoundingFactor = GR.getval1(SpoItem.LoanNetMark_Rounding_FACTOR_NewZ4Spec, 4, true, '0', GR.GetPName(() => SpoItem.LoanNetMark_Rounding_FACTOR_NewZ4Spec), SpoItem.BGNREF);
                    Z4FI.Filler3 = GR.ReturnFiller(5);
                    Z4FI.AccruedBondInterestincludedinmarks = (SpoItem.BOND != null && SpoItem.BOND.Trim().ToLower() == "y") ? "Y" : "N";
                    Z4FI.MarkRepricingEligibleFlag = (SpoItem.LNT_AUTO_MARKS != null && SpoItem.LNT_AUTO_MARKS.Trim().ToLower() == "y") ? "Y" : "N";
                    Z4FI.SunGardUseOnly3 = GR.ReturnFiller(60);

                    /*          Dividend/Income Section         */
                    Z4FI.DividendFlowThrough = GR.SanitizeNumber(SpoItem.DIV_AGE.ToString(), 3, true, 3, false);
                    Z4FI.Filler4 = GR.ReturnFiller(1);
                    Z4FI.IncomeTrackingIndicator = (SpoItem.LNT_DIV_TRACKING != null && SpoItem.LNT_DIV_TRACKING.Trim().ToLower() == "y") ? "Y" : "N";
                    Z4FI.SunGardUseOnly4 = GR.ReturnFiller(42);
                    Z4FI.Filler5 = GR.ReturnFiller(12);
                    Z4FI.SunGardUseOnly5 = GR.ReturnFiller(48);

                    /*          Settlement Section              */
                    Z4FI.OCCHedgeContract = "N";
                    Z4FI.CustodianID = GR.ReturnFiller(8);
                    Z4FI.SubAccount = GR.ReturnFiller(35);
                    Z4FI.Filler6 = GR.ReturnFiller(203);
                    Z4FI.SunGardUseOnly6 = GR.ReturnFiller(59);
                }
                catch (Exception ex)
                {
                    string tid = SpoItem.BGNREF != null ? SpoItem.BGNREF : "Unknown";

                    Utils.LogError("Tradeid:" + tid + " " + ex.Message);
                    continue;
                }

                #endregion

                FileData.Add(Z4FI);
            }

            string RecordTypeFooter = "3";
            string ParticipantIDFooter = BookNumber.PadLeft(8, '0');
            string DetailRecordCount = FileData.Count.ToString().PadLeft(9, '0');
            string Filler = GR.ReturnFiller(982);
            Z4FileSpec.Footer = RecordTypeFooter + ParticipantIDFooter + DetailRecordCount + Filler;

            //Z4FileSpec.CreateFooter(BookNumber, FileData.Count);
            return FileData;
        }
        
        private StringBuilder CreateStringFromFormattedList(List<Z4FileSpec> filedata)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Z4FileSpec.Header);

            foreach (var item in filedata)
            {
                sb.Append(item.RecordType);
                sb.Append(item.ParticipantID);
                sb.Append(item.ContraPartyID);
                sb.Append(item.ActivityCode);
                sb.Append(item.UserContractInternalReferenceNumber);
                sb.Append(item.Filler);
                sb.Append(item.SecurityID);
                sb.Append(item.SecurityIDType);
                sb.Append(item.OpenQuantity);
                sb.Append(item.ContractValue);
                sb.Append(item.RebateRateCode);
                sb.Append(item.RebateFeeRate);
                sb.Append(item.CollateralType);
                sb.Append(item.Filler1);
                sb.Append(item.DeliveryDateofDeal);
                sb.Append(item.CloseDateorTermDateofTransaction);
                sb.Append(item.UserContractInfo);
                sb.Append(item.Filler2);
                sb.Append(item.SunGardUseOnly1);
                sb.Append(item.InternalAccountNumber);
                sb.Append(item.SunGardUseOnly2);
                sb.Append(item.MarginParameter);
                sb.Append(item.RoundingDirection);
                sb.Append(item.MarkRoundingFactor);
                sb.Append(item.Filler3);
                sb.Append(item.AccruedBondInterestincludedinmarks);
                sb.Append(item.MarkRepricingEligibleFlag);
                sb.Append(item.SunGardUseOnly3);
                sb.Append(item.DividendFlowThrough);
                sb.Append(item.Filler4);
                sb.Append(item.IncomeTrackingIndicator);
                sb.Append(item.SunGardUseOnly4);
                sb.Append(item.Filler5);
                sb.Append(item.SunGardUseOnly5);
                sb.Append(item.OCCHedgeContract);
                sb.Append(item.CustodianID);
                sb.Append(item.SubAccount);
                sb.Append(item.Filler6);
                sb.Append(item.SunGardUseOnly6);
            }

            sb.Append(Z4FileSpec.Footer);

            return sb;
        }

        //Step 3
        public string GetDataForFile()
        {
            StringBuilder sb = CreateStringFromFormattedList(Z4List);
            return sb.ToString();
        }

    }

    

}
