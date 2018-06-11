using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ContractCompareLoanetFile.ContractCompareDomestic
{
	public class ContractCompareDomesticFactory
	{
		public ContractCompareDomesticDA DA = new ContractCompareDomesticDA();
		public void GetAllContractCompareDomestic(ICollection<ContractCompareDomesticObject> list) { DA.GetAllContractCompareDomestic(list);}
		public void InsertSingle(ContractCompareDomesticObject obj) { DA.InsertSingle(obj); }
        public void DeleteByDateAndBook(DateTime DateOfDate, string ParticipantID) { DA.DeleteByDateAndBook(DateOfDate, ParticipantID); }
	}
}
