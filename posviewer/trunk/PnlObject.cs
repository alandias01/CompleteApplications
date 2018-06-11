using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Maple.Dtc.PositionClient
{
    public class PnlObject
    {
        public double? Profit { get; set; }
        public string Ticker { get; set; }
        public int? BorrowQty { get; set; }
        public int? LoanQty { get; set; }
        public double? BorrowCash { get; set; }
        public double? LoanCash { get; set; }
        public double? AvgBorrowRate { get; set; }
        public double? AvgLoanRate { get; set; }
        public string Book { get; set; }

        public PnlObject(IDataReader r)
        {
            Book = r["Book"].ToString();
            Ticker = r["Ticker"] == DBNull.Value ? Ticker : r["Ticker"].ToString();
            BorrowQty = r["QuantityBorrowed"] == DBNull.Value ? BorrowQty : int.Parse(r["QuantityBorrowed"].ToString());
            LoanQty = r["QuantityLoaned"] == DBNull.Value ? LoanQty : int.Parse(r["QuantityLoaned"].ToString());
            BorrowCash = r["BorrowValue"] == DBNull.Value ? BorrowCash : double.Parse(r["BorrowValue"].ToString());
            LoanCash = r["LoanValue"] == DBNull.Value ? LoanCash : double.Parse(r["LoanValue"].ToString());
            AvgBorrowRate = r["AvgBorrowRate"] == DBNull.Value ? AvgBorrowRate : double.Parse(r["AvgBorrowRate"].ToString());
            AvgLoanRate = r["AvgLoanRate"] == DBNull.Value ? AvgLoanRate : double.Parse(r["AvgLoanRate"].ToString());
            Profit = r["PnL"] == DBNull.Value ? Profit : double.Parse(r["PnL"].ToString());
        }


    }
}
