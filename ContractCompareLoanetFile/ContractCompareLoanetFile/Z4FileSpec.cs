using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContractCompareLoanetFile.spHeliumDataForContractCompareLoanetFile;
using WPFUtils;

namespace ContractCompareLoanetFile
{
    /* 
     * mark rounding factor 9v999
     * OCC Hedge
     */


    public class Z4FileSpec
    {
        #region Properties
        public static string Header { get; set; }
        public static string Footer { get; set; }

        public string RecordType { get; set; }
        public string ParticipantID { get; set; }
        public string ContraPartyID { get; set; }
        public string ActivityCode { get; set; }
        public string UserContractInternalReferenceNumber { get; set; }
        public string Filler { get; set; }
        public string SecurityID { get; set; }
        public string SecurityIDType { get; set; }
        public string OpenQuantity { get; set; }
        public string ContractValue { get; set; }
        public string RebateRateCode { get; set; }
        public string RebateFeeRate { get; set; }
        public string CollateralType { get; set; }
        public string Filler1 { get; set; }
        public string DeliveryDateofDeal { get; set; }
        public string CloseDateorTermDateofTransaction { get; set; }
        public string UserContractInfo { get; set; }
        public string Filler2 { get; set; }
        public string SunGardUseOnly1 { get; set; }
        public string InternalAccountNumber { get; set; }
        public string SunGardUseOnly2 { get; set; }
        public string MarginParameter { get; set; }
        public string RoundingDirection { get; set; }
        public string MarkRoundingFactor { get; set; }
        public string Filler3 { get; set; }
        public string AccruedBondInterestincludedinmarks { get; set; }
        public string MarkRepricingEligibleFlag { get; set; }
        public string SunGardUseOnly3 { get; set; }
        public string DividendFlowThrough { get; set; }
        public string Filler4 { get; set; }
        public string IncomeTrackingIndicator { get; set; }
        public string SunGardUseOnly4 { get; set; }
        public string Filler5 { get; set; }
        public string SunGardUseOnly5 { get; set; }
        public string OCCHedgeContract { get; set; }
        public string CustodianID { get; set; }
        public string SubAccount { get; set; }
        public string Filler6 { get; set; }
        public string SunGardUseOnly6 { get; set; }

        #endregion
        
        public Z4FileSpec() { }
        
    }
}
