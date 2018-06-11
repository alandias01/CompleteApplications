using System;
using System.Data.SqlClient;

namespace ContractCompareLoanetFile.ContractCompareDomestic
{
	public class ContractCompareDomesticObject
	{
		#region Properties
		public Int32? Id { get; set; }
		public DateTime? DateOfData { get; set; }
		public DateTime? DateOfImport { get; set; }
		public String RecordType { get; set; }
		public String ParticipantID { get; set; }
		public String ContraPartyID { get; set; }
		public String ActivityCode { get; set; }
		public String UserContractInternalReferenceNumber { get; set; }
		public String Filler1 { get; set; }
		public String SecurityID { get; set; }
		public String SecurityIDType { get; set; }
		public String OpenQuantity { get; set; }
		public String ContractValue { get; set; }
		public String RebateRateCode { get; set; }
		public String RebateFeeRate { get; set; }
		public String CollateralType { get; set; }
		public String Filler2 { get; set; }
		public String DeliveryDateofDeal { get; set; }
		public String CloseDateorTermDateofTransaction { get; set; }
		public String Filler3 { get; set; }
		public String UserContractInfo { get; set; }
		public String SunGardUseOnly1 { get; set; }
		public String ComparisonCode { get; set; }
		public String InternalAccountNumber { get; set; }
		public String Filler4 { get; set; }
		public String SunGardUseOnly2 { get; set; }
		public String MarginParameter { get; set; }
		public String RoundingDirection { get; set; }
		public String MarkRoundingFactor { get; set; }
		public String Filler5 { get; set; }
		public String AccruedBondInterestincludedinmarks { get; set; }
		public String Filler6 { get; set; }
		public String SunGardUseOnly3 { get; set; }
		public String DividendFlowThrough { get; set; }
		public String Filler7 { get; set; }
		public String IncomeTrackingIndicator { get; set; }
		public String SunGardUseOnly4 { get; set; }
		public String Filler8 { get; set; }
		public String SunGardUseOnly5 { get; set; }
		public String OCCHedgeContract { get; set; }
		public String CustodianID { get; set; }
		public String SubAccount { get; set; }
		public String Filler9 { get; set; }
		public String SunGardUseOnly6 { get; set; }
		#endregion

		#region Constructors
        public ContractCompareDomesticObject() { }

		public ContractCompareDomesticObject(SqlDataReader row)
		{
			this.Id = row["Id"] == DBNull.Value ? Id : (Int32?)row["Id"];
			this.DateOfData = row["DateOfData"] == DBNull.Value ? DateOfData : (DateTime?)row["DateOfData"];
			this.DateOfImport = row["DateOfImport"] == DBNull.Value ? DateOfImport : (DateTime?)row["DateOfImport"];
			this.RecordType = row["RecordType"] == DBNull.Value ? RecordType : (String)row["RecordType"];
			this.ParticipantID = row["ParticipantID"] == DBNull.Value ? ParticipantID : (String)row["ParticipantID"];
			this.ContraPartyID = row["ContraPartyID"] == DBNull.Value ? ContraPartyID : (String)row["ContraPartyID"];
			this.ActivityCode = row["ActivityCode"] == DBNull.Value ? ActivityCode : (String)row["ActivityCode"];
			this.UserContractInternalReferenceNumber = row["UserContractInternalReferenceNumber"] == DBNull.Value ? UserContractInternalReferenceNumber : (String)row["UserContractInternalReferenceNumber"];
			this.Filler1 = row["Filler1"] == DBNull.Value ? Filler1 : (String)row["Filler1"];
			this.SecurityID = row["SecurityID"] == DBNull.Value ? SecurityID : (String)row["SecurityID"];
			this.SecurityIDType = row["SecurityIDType"] == DBNull.Value ? SecurityIDType : (String)row["SecurityIDType"];
			this.OpenQuantity = row["OpenQuantity"] == DBNull.Value ? OpenQuantity : (String)row["OpenQuantity"];
			this.ContractValue = row["ContractValue"] == DBNull.Value ? ContractValue : (String)row["ContractValue"];
			this.RebateRateCode = row["RebateRateCode"] == DBNull.Value ? RebateRateCode : (String)row["RebateRateCode"];
			this.RebateFeeRate = row["RebateFeeRate"] == DBNull.Value ? RebateFeeRate : (String)row["RebateFeeRate"];
			this.CollateralType = row["CollateralType"] == DBNull.Value ? CollateralType : (String)row["CollateralType"];
			this.Filler2 = row["Filler2"] == DBNull.Value ? Filler2 : (String)row["Filler2"];
			this.DeliveryDateofDeal = row["DeliveryDateofDeal"] == DBNull.Value ? DeliveryDateofDeal : (String)row["DeliveryDateofDeal"];
			this.CloseDateorTermDateofTransaction = row["CloseDateorTermDateofTransaction"] == DBNull.Value ? CloseDateorTermDateofTransaction : (String)row["CloseDateorTermDateofTransaction"];
			this.Filler3 = row["Filler3"] == DBNull.Value ? Filler3 : (String)row["Filler3"];
			this.UserContractInfo = row["UserContractInfo"] == DBNull.Value ? UserContractInfo : (String)row["UserContractInfo"];
			this.SunGardUseOnly1 = row["SunGardUseOnly1"] == DBNull.Value ? SunGardUseOnly1 : (String)row["SunGardUseOnly1"];
			this.ComparisonCode = row["ComparisonCode"] == DBNull.Value ? ComparisonCode : (String)row["ComparisonCode"];
			this.InternalAccountNumber = row["InternalAccountNumber"] == DBNull.Value ? InternalAccountNumber : (String)row["InternalAccountNumber"];
			this.Filler4 = row["Filler4"] == DBNull.Value ? Filler4 : (String)row["Filler4"];
			this.SunGardUseOnly2 = row["SunGardUseOnly2"] == DBNull.Value ? SunGardUseOnly2 : (String)row["SunGardUseOnly2"];
			this.MarginParameter = row["MarginParameter"] == DBNull.Value ? MarginParameter : (String)row["MarginParameter"];
			this.RoundingDirection = row["RoundingDirection"] == DBNull.Value ? RoundingDirection : (String)row["RoundingDirection"];
			this.MarkRoundingFactor = row["MarkRoundingFactor"] == DBNull.Value ? MarkRoundingFactor : (String)row["MarkRoundingFactor"];
			this.Filler5 = row["Filler5"] == DBNull.Value ? Filler5 : (String)row["Filler5"];
			this.AccruedBondInterestincludedinmarks = row["AccruedBondInterestincludedinmarks"] == DBNull.Value ? AccruedBondInterestincludedinmarks : (String)row["AccruedBondInterestincludedinmarks"];
			this.Filler6 = row["Filler6"] == DBNull.Value ? Filler6 : (String)row["Filler6"];
			this.SunGardUseOnly3 = row["SunGardUseOnly3"] == DBNull.Value ? SunGardUseOnly3 : (String)row["SunGardUseOnly3"];
			this.DividendFlowThrough = row["DividendFlowThrough"] == DBNull.Value ? DividendFlowThrough : (String)row["DividendFlowThrough"];
			this.Filler7 = row["Filler7"] == DBNull.Value ? Filler7 : (String)row["Filler7"];
			this.IncomeTrackingIndicator = row["IncomeTrackingIndicator"] == DBNull.Value ? IncomeTrackingIndicator : (String)row["IncomeTrackingIndicator"];
			this.SunGardUseOnly4 = row["SunGardUseOnly4"] == DBNull.Value ? SunGardUseOnly4 : (String)row["SunGardUseOnly4"];
			this.Filler8 = row["Filler8"] == DBNull.Value ? Filler8 : (String)row["Filler8"];
			this.SunGardUseOnly5 = row["SunGardUseOnly5"] == DBNull.Value ? SunGardUseOnly5 : (String)row["SunGardUseOnly5"];
			this.OCCHedgeContract = row["OCCHedgeContract"] == DBNull.Value ? OCCHedgeContract : (String)row["OCCHedgeContract"];
			this.CustodianID = row["CustodianID"] == DBNull.Value ? CustodianID : (String)row["CustodianID"];
			this.SubAccount = row["SubAccount"] == DBNull.Value ? SubAccount : (String)row["SubAccount"];
			this.Filler9 = row["Filler9"] == DBNull.Value ? Filler9 : (String)row["Filler9"];
			this.SunGardUseOnly6 = row["SunGardUseOnly6"] == DBNull.Value ? SunGardUseOnly6 : (String)row["SunGardUseOnly6"];
		}

		public ContractCompareDomesticObject(Int32? Id, DateTime? DateOfData, DateTime? DateOfImport, String RecordType, String ParticipantID, String ContraPartyID, String ActivityCode, String UserContractInternalReferenceNumber, String Filler1, String SecurityID, String SecurityIDType, String OpenQuantity, String ContractValue, String RebateRateCode, String RebateFeeRate, String CollateralType, String Filler2, String DeliveryDateofDeal, String CloseDateorTermDateofTransaction, String Filler3, String UserContractInfo, String SunGardUseOnly1, String ComparisonCode, String InternalAccountNumber, String Filler4, String SunGardUseOnly2, String MarginParameter, String RoundingDirection, String MarkRoundingFactor, String Filler5, String AccruedBondInterestincludedinmarks, String Filler6, String SunGardUseOnly3, String DividendFlowThrough, String Filler7, String IncomeTrackingIndicator, String SunGardUseOnly4, String Filler8, String SunGardUseOnly5, String OCCHedgeContract, String CustodianID, String SubAccount, String Filler9, String SunGardUseOnly6)
		{
			this.Id = Id;
			this.DateOfData = DateOfData;
			this.DateOfImport = DateOfImport;
			this.RecordType = RecordType;
			this.ParticipantID = ParticipantID;
			this.ContraPartyID = ContraPartyID;
			this.ActivityCode = ActivityCode;
			this.UserContractInternalReferenceNumber = UserContractInternalReferenceNumber;
			this.Filler1 = Filler1;
			this.SecurityID = SecurityID;
			this.SecurityIDType = SecurityIDType;
			this.OpenQuantity = OpenQuantity;
			this.ContractValue = ContractValue;
			this.RebateRateCode = RebateRateCode;
			this.RebateFeeRate = RebateFeeRate;
			this.CollateralType = CollateralType;
			this.Filler2 = Filler2;
			this.DeliveryDateofDeal = DeliveryDateofDeal;
			this.CloseDateorTermDateofTransaction = CloseDateorTermDateofTransaction;
			this.Filler3 = Filler3;
			this.UserContractInfo = UserContractInfo;
			this.SunGardUseOnly1 = SunGardUseOnly1;
			this.ComparisonCode = ComparisonCode;
			this.InternalAccountNumber = InternalAccountNumber;
			this.Filler4 = Filler4;
			this.SunGardUseOnly2 = SunGardUseOnly2;
			this.MarginParameter = MarginParameter;
			this.RoundingDirection = RoundingDirection;
			this.MarkRoundingFactor = MarkRoundingFactor;
			this.Filler5 = Filler5;
			this.AccruedBondInterestincludedinmarks = AccruedBondInterestincludedinmarks;
			this.Filler6 = Filler6;
			this.SunGardUseOnly3 = SunGardUseOnly3;
			this.DividendFlowThrough = DividendFlowThrough;
			this.Filler7 = Filler7;
			this.IncomeTrackingIndicator = IncomeTrackingIndicator;
			this.SunGardUseOnly4 = SunGardUseOnly4;
			this.Filler8 = Filler8;
			this.SunGardUseOnly5 = SunGardUseOnly5;
			this.OCCHedgeContract = OCCHedgeContract;
			this.CustodianID = CustodianID;
			this.SubAccount = SubAccount;
			this.Filler9 = Filler9;
			this.SunGardUseOnly6 = SunGardUseOnly6;
		}

		public ContractCompareDomesticObject(ContractCompareDomesticObject x)
		{
			this.Id = x.Id;
			this.DateOfData = x.DateOfData;
			this.DateOfImport = x.DateOfImport;
			this.RecordType = x.RecordType;
			this.ParticipantID = x.ParticipantID;
			this.ContraPartyID = x.ContraPartyID;
			this.ActivityCode = x.ActivityCode;
			this.UserContractInternalReferenceNumber = x.UserContractInternalReferenceNumber;
			this.Filler1 = x.Filler1;
			this.SecurityID = x.SecurityID;
			this.SecurityIDType = x.SecurityIDType;
			this.OpenQuantity = x.OpenQuantity;
			this.ContractValue = x.ContractValue;
			this.RebateRateCode = x.RebateRateCode;
			this.RebateFeeRate = x.RebateFeeRate;
			this.CollateralType = x.CollateralType;
			this.Filler2 = x.Filler2;
			this.DeliveryDateofDeal = x.DeliveryDateofDeal;
			this.CloseDateorTermDateofTransaction = x.CloseDateorTermDateofTransaction;
			this.Filler3 = x.Filler3;
			this.UserContractInfo = x.UserContractInfo;
			this.SunGardUseOnly1 = x.SunGardUseOnly1;
			this.ComparisonCode = x.ComparisonCode;
			this.InternalAccountNumber = x.InternalAccountNumber;
			this.Filler4 = x.Filler4;
			this.SunGardUseOnly2 = x.SunGardUseOnly2;
			this.MarginParameter = x.MarginParameter;
			this.RoundingDirection = x.RoundingDirection;
			this.MarkRoundingFactor = x.MarkRoundingFactor;
			this.Filler5 = x.Filler5;
			this.AccruedBondInterestincludedinmarks = x.AccruedBondInterestincludedinmarks;
			this.Filler6 = x.Filler6;
			this.SunGardUseOnly3 = x.SunGardUseOnly3;
			this.DividendFlowThrough = x.DividendFlowThrough;
			this.Filler7 = x.Filler7;
			this.IncomeTrackingIndicator = x.IncomeTrackingIndicator;
			this.SunGardUseOnly4 = x.SunGardUseOnly4;
			this.Filler8 = x.Filler8;
			this.SunGardUseOnly5 = x.SunGardUseOnly5;
			this.OCCHedgeContract = x.OCCHedgeContract;
			this.CustodianID = x.CustodianID;
			this.SubAccount = x.SubAccount;
			this.Filler9 = x.Filler9;
			this.SunGardUseOnly6 = x.SunGardUseOnly6;
		}

		public void Replicate(ContractCompareDomesticObject x)
		{
			this.Id = x.Id;
			this.DateOfData = x.DateOfData;
			this.DateOfImport = x.DateOfImport;
			this.RecordType = x.RecordType;
			this.ParticipantID = x.ParticipantID;
			this.ContraPartyID = x.ContraPartyID;
			this.ActivityCode = x.ActivityCode;
			this.UserContractInternalReferenceNumber = x.UserContractInternalReferenceNumber;
			this.Filler1 = x.Filler1;
			this.SecurityID = x.SecurityID;
			this.SecurityIDType = x.SecurityIDType;
			this.OpenQuantity = x.OpenQuantity;
			this.ContractValue = x.ContractValue;
			this.RebateRateCode = x.RebateRateCode;
			this.RebateFeeRate = x.RebateFeeRate;
			this.CollateralType = x.CollateralType;
			this.Filler2 = x.Filler2;
			this.DeliveryDateofDeal = x.DeliveryDateofDeal;
			this.CloseDateorTermDateofTransaction = x.CloseDateorTermDateofTransaction;
			this.Filler3 = x.Filler3;
			this.UserContractInfo = x.UserContractInfo;
			this.SunGardUseOnly1 = x.SunGardUseOnly1;
			this.ComparisonCode = x.ComparisonCode;
			this.InternalAccountNumber = x.InternalAccountNumber;
			this.Filler4 = x.Filler4;
			this.SunGardUseOnly2 = x.SunGardUseOnly2;
			this.MarginParameter = x.MarginParameter;
			this.RoundingDirection = x.RoundingDirection;
			this.MarkRoundingFactor = x.MarkRoundingFactor;
			this.Filler5 = x.Filler5;
			this.AccruedBondInterestincludedinmarks = x.AccruedBondInterestincludedinmarks;
			this.Filler6 = x.Filler6;
			this.SunGardUseOnly3 = x.SunGardUseOnly3;
			this.DividendFlowThrough = x.DividendFlowThrough;
			this.Filler7 = x.Filler7;
			this.IncomeTrackingIndicator = x.IncomeTrackingIndicator;
			this.SunGardUseOnly4 = x.SunGardUseOnly4;
			this.Filler8 = x.Filler8;
			this.SunGardUseOnly5 = x.SunGardUseOnly5;
			this.OCCHedgeContract = x.OCCHedgeContract;
			this.CustodianID = x.CustodianID;
			this.SubAccount = x.SubAccount;
			this.Filler9 = x.Filler9;
			this.SunGardUseOnly6 = x.SunGardUseOnly6;
		}

		#endregion

	}
}
