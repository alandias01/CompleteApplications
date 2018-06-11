using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using WPFUtils;

namespace ContractCompareLoanetFile.ContractCompare_raw
{
	public class ContractCompare_rawDA
	{
		#region Sql Variables
        string cstr = Properties.Settings.Default.spHeliumDataForContractCompareLoanetFile;
        //string cstr = "Data Source=maplesqldev; Initial Catalog=ContractCompare; Integrated Security=True";
		#endregion

		public void GetAllContractCompare_raw(ICollection<ContractCompare_rawObject> list)
		{
			//Change to SP if required
			string sql =@"SELECT [ContractCompare_raw].* FROM [dbo].[ContractCompare_raw]";

			using (SqlConnection conn = new SqlConnection(cstr))
			{
				SqlCommand cmd = new SqlCommand(sql, conn);
				//cmd.CommandType=CommandType.StoredProcedure;

				//cmd.Parameters.AddWithValue(",");


				SqlDataReader rdr = null;
				try
				{
					conn.Open(); 
                    rdr = cmd.ExecuteReader();
					while (rdr.Read()) 
                    { 
                        list.Add(new ContractCompare_rawObject(rdr)); 
                    }
				}

				//Only use whats in catch block if you included Log.cs
				catch (SqlException ex) 
                {
                    Utils.LogError(ex.Message);
                }

				finally
				{
					if (rdr != null)
					{
						rdr.Close();
					}
				}

			}
		}

		public void InsertSingle(ContractCompare_rawObject obj)
		{
			//Change to SP if required
			string sql =@"INSERT INTO ContractCompare_raw(DateOfData, DateOfImport, Record_Type, Receiving_Participant, Account_Number, Borrow_Loan_Indicator, CUSIP_Number, Delivery_Date, Quantity, Contract_Amount, Rebate_Rate, Call_Rate, Rate_Code, Mark_Parameter, NonCash_Collateral, Mark_Rounding_Factor, Accrued_Interest_for_Bounds, Uncompared_Advisory, User_Contract_Information, Income_Tracking_Indicator, DivPercentage) VALUES(@DateOfData, @DateOfImport, @Record_Type, @Receiving_Participant, @Account_Number, @Borrow_Loan_Indicator, @CUSIP_Number, @Delivery_Date, @Quantity, @Contract_Amount, @Rebate_Rate, @Call_Rate, @Rate_Code, @Mark_Parameter, @NonCash_Collateral, @Mark_Rounding_Factor, @Accrued_Interest_for_Bounds, @Uncompared_Advisory, @User_Contract_Information, @Income_Tracking_Indicator, @DivPercentage)";

			using (SqlConnection conn = new SqlConnection(cstr))
			{
				SqlCommand cmd = new SqlCommand(sql, conn);
				//cmd.CommandType=CommandType.StoredProcedure;

				//cmd.Parameters.AddWithValue(",");


				cmd.Parameters.AddWithValue("@DateOfData", SetNullValue(obj.DateOfData));
				cmd.Parameters.AddWithValue("@DateOfImport", SetNullValue(obj.DateOfImport));
				cmd.Parameters.AddWithValue("@Record_Type", SetNullValue(obj.Record_Type));
				cmd.Parameters.AddWithValue("@Receiving_Participant", SetNullValue(obj.Receiving_Participant));
				cmd.Parameters.AddWithValue("@Account_Number", SetNullValue(obj.Account_Number));
				cmd.Parameters.AddWithValue("@Borrow_Loan_Indicator", SetNullValue(obj.Borrow_Loan_Indicator));
				cmd.Parameters.AddWithValue("@CUSIP_Number", SetNullValue(obj.CUSIP_Number));
				cmd.Parameters.AddWithValue("@Delivery_Date", SetNullValue(obj.Delivery_Date));
				cmd.Parameters.AddWithValue("@Quantity", SetNullValue(obj.Quantity));
				cmd.Parameters.AddWithValue("@Contract_Amount", SetNullValue(obj.Contract_Amount));
				cmd.Parameters.AddWithValue("@Rebate_Rate", SetNullValue(obj.Rebate_Rate));
				cmd.Parameters.AddWithValue("@Call_Rate", SetNullValue(obj.Call_Rate));
				cmd.Parameters.AddWithValue("@Rate_Code", SetNullValue(obj.Rate_Code));
				cmd.Parameters.AddWithValue("@Mark_Parameter", SetNullValue(obj.Mark_Parameter));
				cmd.Parameters.AddWithValue("@NonCash_Collateral", SetNullValue(obj.NonCash_Collateral));
				cmd.Parameters.AddWithValue("@Mark_Rounding_Factor", SetNullValue(obj.Mark_Rounding_Factor));
				cmd.Parameters.AddWithValue("@Accrued_Interest_for_Bounds", SetNullValue(obj.Accrued_Interest_for_Bounds));
				cmd.Parameters.AddWithValue("@Uncompared_Advisory", SetNullValue(obj.Uncompared_Advisory));
				cmd.Parameters.AddWithValue("@User_Contract_Information", SetNullValue(obj.User_Contract_Information));
				cmd.Parameters.AddWithValue("@Income_Tracking_Indicator", SetNullValue(obj.Income_Tracking_Indicator));
				cmd.Parameters.AddWithValue("@DivPercentage", SetNullValue(obj.DivPercentage));

				try
				{
					conn.Open();
					cmd.ExecuteNonQuery();
				}

				//Only use whats in catch block if you included Log.cs
				catch (SqlException ex) 
                {
                    Utils.LogError(ex.Message);
                }

			}
		}

        public void DeleteByDateAndBook(DateTime DateOfData, string Receiving_Participant)
        {
            //Change to SP if required
            string sql = @"DELETE FROM ContractCompare_raw where DateOfData=@DateOfData and Receiving_Participant=@Receiving_Participant";

            using (SqlConnection conn = new SqlConnection(cstr))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                //cmd.CommandType=CommandType.StoredProcedure;
                
                cmd.Parameters.AddWithValue("@DateOfData", DateOfData);
                cmd.Parameters.AddWithValue("@Receiving_Participant", Receiving_Participant);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                //Only use whats in catch block if you included Log.cs
                catch (SqlException ex)
                {
                    Utils.LogError(ex.Message);
                }

            }
        }

        public void DeleteIntercompanyPositionsByBookAndCtpy(string booknumberwe, string booknumberthey, DateTime DateOfData)
        {
            string CmdStr = "spHeliumDataForContractCompareIntercompany_DeleteByBookAndCtpy";

            using (SqlConnection conn = new SqlConnection(cstr))
            {
                SqlCommand cmd = new SqlCommand(CmdStr, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                //Added this line for APEX timing out
                cmd.CommandTimeout = 0;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@booknumberwe", booknumberwe);
                cmd.Parameters.AddWithValue("@booknumberthey", booknumberthey);
                cmd.Parameters.AddWithValue("@DateOfData", DateOfData);


                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();

                }

                catch (SqlException ex)
                {
                    Utils.LogError(ex.Message);
                    throw;
                }

            }

        }

		//Sets DBNULL if NULL and if string is empty, sets to null
		private object SetNullValue(object S)
		{
			if (S is string)
			{
				if (string.IsNullOrEmpty((string)S))
				{
					return DBNull.Value;
				}
				else { return S; }

			}

			else if (S == null)
			{
				return DBNull.Value;
			}

			else { return S; }
		}




        

	}
}
