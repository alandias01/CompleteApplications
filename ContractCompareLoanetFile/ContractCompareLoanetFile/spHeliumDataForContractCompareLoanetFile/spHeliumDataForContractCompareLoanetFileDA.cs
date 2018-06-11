using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using WPFUtils;

namespace ContractCompareLoanetFile.spHeliumDataForContractCompareLoanetFile
{
    class spHeliumDataForContractCompareLoanetFileDA
    {
        string cstr = Properties.Settings.Default.spHeliumDataForContractCompareLoanetFile;

        public void Get_spHeliumDataForContractCompareLoanetFile_byDateAndBook(ICollection<spHeliumDataForContractCompareLoanetFileObject> list, string FileDate, string bookname, int LoanNet_Zone, ref bool SendTextOnError)
        {
            string CmdStr = "spHeliumDataForContractCompareLoanetFileNewZ4Spec";

            using (SqlConnection conn = new SqlConnection(cstr))
            {

                SqlCommand cmd = new SqlCommand(CmdStr, conn);

                //Added this line for APEX timing out
                cmd.CommandTimeout = 0;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FileDate", FileDate);
                cmd.Parameters.AddWithValue("@bookname", bookname);
                cmd.Parameters.AddWithValue("@LoanNet_Zone", LoanNet_Zone);
                                
                string SQLErrors = "";

                SqlDataReader rdr = null;
                try
                {
                    conn.Open();
                                                            
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        list.Add(new spHeliumDataForContractCompareLoanetFileObject(rdr));
                    }

                }

                catch (Exception ex)
                {                    
                    Utils.LogError(ex.Message);
                    SQLErrors += ex.Message;

                    SendTextOnError = true;

                    string EmailGroup = Properties.Settings.Default.EmailGroup;
                    string EmailSubj = "ContractCompare Loanet File creator SQL Error for file " + bookname + " Zone " + LoanNet_Zone;
                    string EmailMessage = @"<H3>Folders to check for issue</H3>";
                    EmailMessage += @"File Creator Log File \\einstein\D_Drive\maple\ContractCompare_Loanet_FileCreator\scripts\log.txt <BR>";
                    EmailMessage += @"File Creator Specific SQL Error Log File \\einstein\D_Drive\maple\ContractCompare_Loanet_FileCreator\scripts\log.txt <BR>";
                    EmailMessage += @"Files are initially created here \\einstein\D_Drive\maple\ContractCompare_Loanet_FileCreator\spool <BR>";
                    EmailMessage += @"Then gets transferred here \\rutherford\dfs\DailyFiles\global1_ContractCompare\in Find your folder<BR><BR>";
                    EmailMessage += @"Check the created files.  If there are no positions in them, you know there was issue.<BR>";
                    EmailMessage += @"Possible causes:Newton was down or helium.mpdata_load.dbo.ve_LoanNet_Position was down<BR><BR>";
                    EmailMessage += "SQL Error: " + SQLErrors;
                    Utils.SendEmail(EmailGroup, EmailSubj, EmailMessage, true);
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
