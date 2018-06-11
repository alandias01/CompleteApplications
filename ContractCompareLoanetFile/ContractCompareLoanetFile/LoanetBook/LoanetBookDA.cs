using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WPFUtils;

namespace ContractCompareLoanetFile.LoanetBook
{
	public class LoanetBookDA
	{
		#region Sql Variables
        string cstr = Properties.Settings.Default.spHeliumDataForContractCompareLoanetFile;
		//string cstr = "Data Source=maplesqldev; Initial Catalog=ContractCompare; Integrated Security=True";
		#endregion

		public void GetAllLoanetBook(ICollection<LoanetBookObject> list)
		{
			//Change to SP if required
			string sql =@"SELECT [LoanetBook].* FROM [dbo].[LoanetBook]";

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
                        list.Add(new LoanetBookObject(rdr)); 
                    }
				}

				//Only use whats in catch block if you included Log.cs
				catch (Exception ex) 
                {
                    //Utils.LogError(ex.Message);
                    throw;
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


		public void InsertSingle(LoanetBookObject obj)
		{
			//Change to SP if required
			string sql =@"INSERT INTO LoanetBook(BookName, BookNumber, LoanetZone, FileName, Active) VALUES(@BookName, @BookNumber, @LoanetZone, @FileName, @Active)";

			using (SqlConnection conn = new SqlConnection(cstr))
			{
				SqlCommand cmd = new SqlCommand(sql, conn);
				//cmd.CommandType=CommandType.StoredProcedure;

				//cmd.Parameters.AddWithValue(",");


				cmd.Parameters.AddWithValue("@BookName", SetNullValue(obj.BookName));
				cmd.Parameters.AddWithValue("@BookNumber", SetNullValue(obj.BookNumber));
				cmd.Parameters.AddWithValue("@LoanetZone", SetNullValue(obj.LoanetZone));
				cmd.Parameters.AddWithValue("@FileName", SetNullValue(obj.FileName));
				cmd.Parameters.AddWithValue("@Active", SetNullValue(obj.Active));

				try
				{
					conn.Open();
					cmd.ExecuteNonQuery();
				}

				//Only use whats in catch block if you included Log.cs
				catch (SqlException ex)
                { 
                    //Utils.LogError(ex.Message); 
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
