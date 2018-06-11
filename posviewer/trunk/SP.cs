using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Maple.Dtc.PositionClient
{
    public class SP
    {
        public static void GetPnLSummaryByTicker(ICollection<PnlObject> list)
        {
            string cs = "Data Source=Newton;Initial Catalog=StockLoan;Integrated Security=True";

            try
            {
                using (SqlConnection conn = new SqlConnection(cs))
                {

                    conn.Open();

                    SqlCommand cmd = new SqlCommand("spGetPnlSummaryByTicker", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;


                    SqlDataReader r = cmd.ExecuteReader();

                    while (r.Read())
                    {
                        PnlObject o = new PnlObject(r);
                        list.Add(o);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
