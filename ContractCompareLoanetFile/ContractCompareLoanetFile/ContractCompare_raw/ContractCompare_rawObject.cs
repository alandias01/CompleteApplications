using System;
using System.Data.SqlClient;

namespace ContractCompareLoanetFile.ContractCompare_raw
{
	public class ContractCompare_rawObject
	{
		#region Properties
		public DateTime? DateOfData { get; set; }
		public DateTime? DateOfImport { get; set; }
		public String Record_Type { get; set; }
		public String Receiving_Participant { get; set; }
		public String Account_Number { get; set; }
		public String Borrow_Loan_Indicator { get; set; }
		public String CUSIP_Number { get; set; }
		public String Delivery_Date { get; set; }
		public String Quantity { get; set; }
		public String Contract_Amount { get; set; }
		public String Rebate_Rate { get; set; }
		public String Call_Rate { get; set; }
		public String Rate_Code { get; set; }
		public String Mark_Parameter { get; set; }
		public String NonCash_Collateral { get; set; }
		public String Mark_Rounding_Factor { get; set; }
		public String Accrued_Interest_for_Bounds { get; set; }
		public String Uncompared_Advisory { get; set; }
		public String User_Contract_Information { get; set; }
		public String Income_Tracking_Indicator { get; set; }
		public String DivPercentage { get; set; }
		#endregion

		#region Constructors

        public ContractCompare_rawObject(){}

		public ContractCompare_rawObject(SqlDataReader row)
		{
			this.DateOfData = row["DateOfData"] == DBNull.Value ? DateOfData : (DateTime?)row["DateOfData"];
			this.DateOfImport = row["DateOfImport"] == DBNull.Value ? DateOfImport : (DateTime?)row["DateOfImport"];
			this.Record_Type = row["Record_Type"] == DBNull.Value ? Record_Type : (String)row["Record_Type"];
			this.Receiving_Participant = row["Receiving_Participant"] == DBNull.Value ? Receiving_Participant : (String)row["Receiving_Participant"];
			this.Account_Number = row["Account_Number"] == DBNull.Value ? Account_Number : (String)row["Account_Number"];
			this.Borrow_Loan_Indicator = row["Borrow_Loan_Indicator"] == DBNull.Value ? Borrow_Loan_Indicator : (String)row["Borrow_Loan_Indicator"];
			this.CUSIP_Number = row["CUSIP_Number"] == DBNull.Value ? CUSIP_Number : (String)row["CUSIP_Number"];
			this.Delivery_Date = row["Delivery_Date"] == DBNull.Value ? Delivery_Date : (String)row["Delivery_Date"];
			this.Quantity = row["Quantity"] == DBNull.Value ? Quantity : (String)row["Quantity"];
			this.Contract_Amount = row["Contract_Amount"] == DBNull.Value ? Contract_Amount : (String)row["Contract_Amount"];
			this.Rebate_Rate = row["Rebate_Rate"] == DBNull.Value ? Rebate_Rate : (String)row["Rebate_Rate"];
			this.Call_Rate = row["Call_Rate"] == DBNull.Value ? Call_Rate : (String)row["Call_Rate"];
			this.Rate_Code = row["Rate_Code"] == DBNull.Value ? Rate_Code : (String)row["Rate_Code"];
			this.Mark_Parameter = row["Mark_Parameter"] == DBNull.Value ? Mark_Parameter : (String)row["Mark_Parameter"];
			this.NonCash_Collateral = row["NonCash_Collateral"] == DBNull.Value ? NonCash_Collateral : (String)row["NonCash_Collateral"];
			this.Mark_Rounding_Factor = row["Mark_Rounding_Factor"] == DBNull.Value ? Mark_Rounding_Factor : (String)row["Mark_Rounding_Factor"];
			this.Accrued_Interest_for_Bounds = row["Accrued_Interest_for_Bounds"] == DBNull.Value ? Accrued_Interest_for_Bounds : (String)row["Accrued_Interest_for_Bounds"];
			this.Uncompared_Advisory = row["Uncompared_Advisory"] == DBNull.Value ? Uncompared_Advisory : (String)row["Uncompared_Advisory"];
			this.User_Contract_Information = row["User_Contract_Information"] == DBNull.Value ? User_Contract_Information : (String)row["User_Contract_Information"];
			this.Income_Tracking_Indicator = row["Income_Tracking_Indicator"] == DBNull.Value ? Income_Tracking_Indicator : (String)row["Income_Tracking_Indicator"];
			this.DivPercentage = row["DivPercentage"] == DBNull.Value ? DivPercentage : (String)row["DivPercentage"];
		}

		public ContractCompare_rawObject(DateTime? DateOfData, DateTime? DateOfImport, String Record_Type, String Receiving_Participant, String Account_Number, String Borrow_Loan_Indicator, String CUSIP_Number, String Delivery_Date, String Quantity, String Contract_Amount, String Rebate_Rate, String Call_Rate, String Rate_Code, String Mark_Parameter, String NonCash_Collateral, String Mark_Rounding_Factor, String Accrued_Interest_for_Bounds, String Uncompared_Advisory, String User_Contract_Information, String Income_Tracking_Indicator, String DivPercentage)
		{
			this.DateOfData = DateOfData;
			this.DateOfImport = DateOfImport;
			this.Record_Type = Record_Type;
			this.Receiving_Participant = Receiving_Participant;
			this.Account_Number = Account_Number;
			this.Borrow_Loan_Indicator = Borrow_Loan_Indicator;
			this.CUSIP_Number = CUSIP_Number;
			this.Delivery_Date = Delivery_Date;
			this.Quantity = Quantity;
			this.Contract_Amount = Contract_Amount;
			this.Rebate_Rate = Rebate_Rate;
			this.Call_Rate = Call_Rate;
			this.Rate_Code = Rate_Code;
			this.Mark_Parameter = Mark_Parameter;
			this.NonCash_Collateral = NonCash_Collateral;
			this.Mark_Rounding_Factor = Mark_Rounding_Factor;
			this.Accrued_Interest_for_Bounds = Accrued_Interest_for_Bounds;
			this.Uncompared_Advisory = Uncompared_Advisory;
			this.User_Contract_Information = User_Contract_Information;
			this.Income_Tracking_Indicator = Income_Tracking_Indicator;
			this.DivPercentage = DivPercentage;
		}

		public ContractCompare_rawObject(ContractCompare_rawObject x)
		{
			this.DateOfData = x.DateOfData;
			this.DateOfImport = x.DateOfImport;
			this.Record_Type = x.Record_Type;
			this.Receiving_Participant = x.Receiving_Participant;
			this.Account_Number = x.Account_Number;
			this.Borrow_Loan_Indicator = x.Borrow_Loan_Indicator;
			this.CUSIP_Number = x.CUSIP_Number;
			this.Delivery_Date = x.Delivery_Date;
			this.Quantity = x.Quantity;
			this.Contract_Amount = x.Contract_Amount;
			this.Rebate_Rate = x.Rebate_Rate;
			this.Call_Rate = x.Call_Rate;
			this.Rate_Code = x.Rate_Code;
			this.Mark_Parameter = x.Mark_Parameter;
			this.NonCash_Collateral = x.NonCash_Collateral;
			this.Mark_Rounding_Factor = x.Mark_Rounding_Factor;
			this.Accrued_Interest_for_Bounds = x.Accrued_Interest_for_Bounds;
			this.Uncompared_Advisory = x.Uncompared_Advisory;
			this.User_Contract_Information = x.User_Contract_Information;
			this.Income_Tracking_Indicator = x.Income_Tracking_Indicator;
			this.DivPercentage = x.DivPercentage;
		}

		public void Replicate(ContractCompare_rawObject x)
		{
			this.DateOfData = x.DateOfData;
			this.DateOfImport = x.DateOfImport;
			this.Record_Type = x.Record_Type;
			this.Receiving_Participant = x.Receiving_Participant;
			this.Account_Number = x.Account_Number;
			this.Borrow_Loan_Indicator = x.Borrow_Loan_Indicator;
			this.CUSIP_Number = x.CUSIP_Number;
			this.Delivery_Date = x.Delivery_Date;
			this.Quantity = x.Quantity;
			this.Contract_Amount = x.Contract_Amount;
			this.Rebate_Rate = x.Rebate_Rate;
			this.Call_Rate = x.Call_Rate;
			this.Rate_Code = x.Rate_Code;
			this.Mark_Parameter = x.Mark_Parameter;
			this.NonCash_Collateral = x.NonCash_Collateral;
			this.Mark_Rounding_Factor = x.Mark_Rounding_Factor;
			this.Accrued_Interest_for_Bounds = x.Accrued_Interest_for_Bounds;
			this.Uncompared_Advisory = x.Uncompared_Advisory;
			this.User_Contract_Information = x.User_Contract_Information;
			this.Income_Tracking_Indicator = x.Income_Tracking_Indicator;
			this.DivPercentage = x.DivPercentage;
		}

		#endregion

	}
}
