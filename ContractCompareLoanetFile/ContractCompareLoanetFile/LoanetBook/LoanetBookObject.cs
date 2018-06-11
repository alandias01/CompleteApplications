using System;
using System.Data.SqlClient;

namespace ContractCompareLoanetFile.LoanetBook
{
	public class LoanetBookObject
	{
		#region Properties
		public String BookName { get; set; }
		public String BookNumber { get; set; }
		public String LoanetZone { get; set; }
		public String FileName { get; set; }
		public Boolean? Active { get; set; }
		#endregion

		#region Constructors
		public LoanetBookObject(SqlDataReader row)
		{
			this.BookName = row["BookName"] == DBNull.Value ? BookName : (String)row["BookName"];
			this.BookNumber = row["BookNumber"] == DBNull.Value ? BookNumber : (String)row["BookNumber"];
			this.LoanetZone = row["LoanetZone"] == DBNull.Value ? LoanetZone : (String)row["LoanetZone"];
			this.FileName = row["FileName"] == DBNull.Value ? FileName : (String)row["FileName"];
			this.Active = row["Active"] == DBNull.Value ? Active : (Boolean?)row["Active"];
		}

		public LoanetBookObject(String BookName, String BookNumber, String LoanetZone, String FileName, Boolean? Active)
		{
			this.BookName = BookName;
			this.BookNumber = BookNumber;
			this.LoanetZone = LoanetZone;
			this.FileName = FileName;
			this.Active = Active;
		}

		public LoanetBookObject(LoanetBookObject x)
		{
			this.BookName = x.BookName;
			this.BookNumber = x.BookNumber;
			this.LoanetZone = x.LoanetZone;
			this.FileName = x.FileName;
			this.Active = x.Active;
		}

		public void Replicate(LoanetBookObject x)
		{
			this.BookName = x.BookName;
			this.BookNumber = x.BookNumber;
			this.LoanetZone = x.LoanetZone;
			this.FileName = x.FileName;
			this.Active = x.Active;
		}

		#endregion

	}
}
