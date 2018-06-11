using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ContractCompareLoanetFile.LoanetBook
{
	public class LoanetBookFactory
	{
		public LoanetBookDA DA = new LoanetBookDA();
		public void GetAllLoanetBook(ICollection<LoanetBookObject> list) { DA.GetAllLoanetBook(list);}
		public void InsertSingle(LoanetBookObject obj) { DA.InsertSingle(obj); }
	}
}
