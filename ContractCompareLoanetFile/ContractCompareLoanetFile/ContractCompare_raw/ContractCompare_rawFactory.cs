using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ContractCompareLoanetFile.ContractCompare_raw
{
	public class ContractCompare_rawFactory
	{
		public ContractCompare_rawDA DA = new ContractCompare_rawDA();
		public void GetAllContractCompare_raw(ICollection<ContractCompare_rawObject> list) { DA.GetAllContractCompare_raw(list);}
		public void InsertSingle(ContractCompare_rawObject obj) { DA.InsertSingle(obj); }
        public void DeleteIntercompanyPositionsByBookAndCtpy(string booknumberwe, string booknumberthey, DateTime DateOfData) { DA.DeleteIntercompanyPositionsByBookAndCtpy(booknumberwe, booknumberthey, DateOfData); }
        public void DeleteByDateAndBook(DateTime DateOfData, string Receiving_Participant) { DA.DeleteByDateAndBook(DateOfData, Receiving_Participant); }
	}
}
