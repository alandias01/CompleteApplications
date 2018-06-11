using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContractCompareEquilendFile.ve_g1position_gql
{
    class ve_g1position_gqlFactory
    {
        public ve_g1position_gqlDA ve_g1pDA = new ve_g1position_gqlDA();

        public void Get_ve_g1position_gql_byDateAndBook(ICollection<ve_g1position_gqlObject> list, string FileDate, string bookname, int LoanNet_Zone)
        {
            ve_g1pDA.Get_ve_g1position_gql_byDateAndBook(list, FileDate, bookname, LoanNet_Zone );
        }

        //Had issue on laborday since we needed data from monday for tuesday.  This method takes into account holidays
        public DateTime GetLastTradeDate() {return ve_g1position_gqlDA.GetLastTradeDate(); }
 

        
    }
}
