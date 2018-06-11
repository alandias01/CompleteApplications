using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using WPFUtils;
namespace ContractCompareEquilendFile.ve_g1position_gql
{
    class ve_g1position_gqlDA
    {       

        string cstr = Properties.Settings.Default.ContractCompare;
        

        public void Get_ve_g1position_gql_byDateAndBook(ICollection<ve_g1position_gqlObject> list, string FileDate, string bookname, int LoanNet_Zone)
        {
            string CmdStr = "spHeliumDataForContractCompareEquilendFile";

            using (SqlConnection conn = new SqlConnection(cstr))
            {

                SqlCommand cmd = new SqlCommand(CmdStr, conn); 
                
                //Added this line for APEX timing out
                cmd.CommandTimeout = 0;
                
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FileDate", FileDate);
                cmd.Parameters.AddWithValue("@bookname", bookname);
                cmd.Parameters.AddWithValue("@LoanNet_Zone", LoanNet_Zone);

                bool CannotConnect = false;
                string SQLErrors = "";

                SqlDataReader rdr = null;
                try
                {
                    conn.Open();
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        list.Add(new ve_g1position_gqlObject(rdr));
                    }
                    
                }

                catch (SqlException ex)
                {                    
                    Utils.LogError(ex.Message);
                    SQLErrors += ex.Message;
                    CannotConnect = true;
                    throw;
                }

                finally
                {
                    if (rdr != null)
                    {
                        rdr.Close();
                    }
                    
                    if (CannotConnect)
                    {
                        
                    }
 
                }



                
                
            }

        }

        public static DateTime GetLastTradeDate()
        {

            string cs = "Data Source=Newton;Initial Catalog=StockLoan;Integrated Security=True";
            DateTime lastTradeDate = DateTime.Today;

            try
            {
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("spGetLastTradeDate", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    string country = "US";

                    SqlParameter cc = new SqlParameter("@CountryShortCode", SqlDbType.VarChar, 10);
                    cc.Direction = ParameterDirection.Input;
                    cc.Value = country;
                    cmd.Parameters.Add(cc);

                    SqlParameter p = new SqlParameter("@Date", SqlDbType.DateTime);
                    p.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(p);

                    cmd.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    cmd.ExecuteNonQuery();
                    
                    DateTime dt = (DateTime)cmd.Parameters["@Date"].Value;

                    lastTradeDate = dt;

                }

            }

            catch (Exception) { throw; }

            return lastTradeDate;

        }





    }
}
