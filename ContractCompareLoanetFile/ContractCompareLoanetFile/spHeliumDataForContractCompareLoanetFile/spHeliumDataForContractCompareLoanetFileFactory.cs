using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContractCompareLoanetFile.spHeliumDataForContractCompareLoanetFile
{
    class spHeliumDataForContractCompareLoanetFileFactory
    {
        public spHeliumDataForContractCompareLoanetFileDA ve_g1pDA = new spHeliumDataForContractCompareLoanetFileDA();

        public void Get_spHeliumDataForContractCompareLoanetFile_byDateAndBook(ICollection<spHeliumDataForContractCompareLoanetFileObject> list, string FileDate, string bookname, int LoanNet_Zone, ref bool SendTextOnError)
        {
            ve_g1pDA.Get_spHeliumDataForContractCompareLoanetFile_byDateAndBook(list, FileDate, bookname, LoanNet_Zone, ref SendTextOnError);
        }

        //Had issue on laborday since we needed data from monday for tuesday.  This method takes into account holidays
        public DateTime GetLastTradeDate() {return spHeliumDataForContractCompareLoanetFileDA.GetLastTradeDate(); }
 

        
    }
}
