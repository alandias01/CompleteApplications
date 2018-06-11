using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using WPFUtils;
namespace ContractCompareEquilendFile.ve_g1position_gql
{
    public class ve_g1position_gqlObject
    {
        StringBuilder Errors = new StringBuilder();
        #region Properties
                
        public string BookName { get; set; }
        public string CPTY { get; set; }
        public string NAME { get; set; }
        public string CtpyAgentAccount { get; set; }
        public string LoanNet_Counterparty_Id { get; set; }
        public string BL { get; set; }
        public string COLL_FLG { get; set; }
        public string CUSIP { get; set; }
        public string ISIN { get; set; }
        public string SEDOL { get; set; }
        public DateTime? TRADE { get; set; }
        public double? QTY { get; set; }
        public double? LNVAL { get; set; }
        public double? LNRATE { get; set; }
        public double? CRATE { get; set; }
        public string UN_FLAG { get; set; }
        public double? MKT_VAL { get; set; }
        public double? LNMRG { get; set; }
        public string CASH { get; set; }
        public string LoanNetMark_Rounding_FACTOR { get; set; }
        public string BOND { get; set; }
        public string LNCUR { get; set; }
        public DateTime? CSET_DT { get; set; }
        public DateTime? SSET_DT { get; set; }
        public DateTime? TERMDT { get; set; }
        public string OP { get; set; }
        public double? DIV_AGE { get; set; }
        public string CALL { get; set; }
        public string MARK_ELIG { get; set; }
        public string BGNREF { get; set; }
        public string DIV_TRACK { get; set; }
        public string INTERCO_REF { get; set; }

        public string LoanNet_Eligable { get; set; }
        //public string LNT_YN { get; set; }
        public string LNT_INC_COL { get; set; }
        public string LNT_INC_PEND { get; set; }
        public double? LNT_ZONE { get; set; }
        public string LNT_ID { get; set; }
        public string LNT_PTYPES { get; set; }
        public string LNT_CALLBACK { get; set; }
        public string LNT_DIV_TRACKING { get; set; }
        public string LNT_AUTO_MARKS { get; set; }
        public string LNT_NC_COLL_TYPE { get; set; }
        public string LNT_CollateralFlag { get; set; }
        public string LNT_GOLIM { get; set; }

        public int TimeTableID { get; set; }
                

        #endregion

        
        public ve_g1position_gqlObject(SqlDataReader row)
        {
            BookName = row["bookname"] == DBNull.Value ? BookName : (string)row["bookname"];
            
            CPTY = row["CPTY"] == DBNull.Value ? CPTY : (string)row["CPTY"];
            NAME = row["NAME"] == DBNull.Value ? NAME : (string)row["NAME"];
            CtpyAgentAccount = row["CtpyAgentAccount"] == DBNull.Value ? CtpyAgentAccount : (string)row["CtpyAgentAccount"];            
            LoanNet_Counterparty_Id = row["LoanNet_Counterparty_Id"] == DBNull.Value ? LoanNet_Counterparty_Id : (string)row["LoanNet_Counterparty_Id"];
            BL = row["bl"] == DBNull.Value ? BL : (string)row["bl"];
            COLL_FLG = row["COLL_FLG"] == DBNull.Value ? COLL_FLG : (string)row["COLL_FLG"];
            CUSIP = row["cusip"] == DBNull.Value ? CUSIP : (string)row["cusip"];
            ISIN = row["ISIN"] == DBNull.Value ? ISIN : (string)row["ISIN"];
            SEDOL = row["sedol"] == DBNull.Value ? SEDOL : (string)row["sedol"];
            TRADE = row["trade"] == DBNull.Value ? TRADE : (DateTime)row["trade"];
            QTY = row["qty"] == DBNull.Value ? QTY : (double)row["qty"];
            LNVAL = row["lnval"] == DBNull.Value ? LNVAL : (double)row["lnval"];            
            LNRATE = row["lnrate"] == DBNull.Value ? LNRATE : (double)row["lnrate"];
            CRATE= row["crate"] == DBNull.Value ? CRATE : (double)row["crate"];
            UN_FLAG = row["un_flag"] == DBNull.Value ? UN_FLAG : (string)row["un_flag"];
            MKT_VAL = row["mkt_val"] == DBNull.Value ? MKT_VAL : (double)row["mkt_val"];
            LNMRG = row["LNMRG"] == DBNull.Value ? LNMRG : (double)row["LNMRG"];
            CASH = row["CASH"] == DBNull.Value ? CASH : (string)row["CASH"];
            LoanNetMark_Rounding_FACTOR = row["LoanNetMark_Rounding_FACTOR"] == DBNull.Value ? LoanNetMark_Rounding_FACTOR : (string)row["LoanNetMark_Rounding_FACTOR"];
            BOND = row["BOND"] == DBNull.Value ? BOND : (string)row["BOND"];
            LNCUR = row["LNCUR"] == DBNull.Value ? LNCUR : (string)row["LNCUR"];
            CSET_DT = row["CSET_DT"] == DBNull.Value ? CSET_DT : (DateTime)row["CSET_DT"];
            SSET_DT = row["SSET_DT"] == DBNull.Value ? SSET_DT : (DateTime)row["SSET_DT"];
            TERMDT = row["TERMDT"] == DBNull.Value ? TERMDT : (DateTime)row["TERMDT"];
            OP = row["OP"] == DBNull.Value ? OP : (string)row["OP"];
            DIV_AGE = row["DIV_AGE"] == DBNull.Value ? DIV_AGE : (double)row["DIV_AGE"];
            CALL = row["CALL"] == DBNull.Value ? CALL : (string)row["CALL"];
            MARK_ELIG = row["MARK_ELIG"] == DBNull.Value ? MARK_ELIG : (string)row["MARK_ELIG"];
            BGNREF = row["bgnref"] == DBNull.Value ? BGNREF : (string)row["bgnref"];
            DIV_TRACK = row["DIV_TRACK"] == DBNull.Value ? DIV_TRACK : (string)row["DIV_TRACK"];            
            INTERCO_REF = row["INTERCO_REF"] == DBNull.Value ? INTERCO_REF : (string)row["INTERCO_REF"];
            //LNT_INC_COL = row["LNT_INC_COL"] == DBNull.Value ? LNT_INC_COL : (string)row["LNT_INC_COL"];
            //LNT_INC_PEND = row["LNT_INC_PEND"] == DBNull.Value ? LNT_INC_PEND : (string)row["LNT_INC_PEND"];

            //LNT_YN = row["LNT_YN"] == DBNull.Value ? LNT_YN : (string)row["LNT_YN"];
            LoanNet_Eligable = row["LoanNet_Eligable"] == DBNull.Value ? LoanNet_Eligable : (string)row["LoanNet_Eligable"];

            LNT_INC_COL = row["LNT_INC_COL"] == DBNull.Value ? LNT_INC_COL : (string)row["LNT_INC_COL"];
            LNT_INC_PEND = row["LNT_INC_PEND"] == DBNull.Value ? LNT_INC_PEND : (string)row["LNT_INC_PEND"];
            LNT_ZONE = row["LNT_ZONE"] == DBNull.Value ? LNT_ZONE : (double)row["LNT_ZONE"];
            LNT_ID = row["LNT_ID"] == DBNull.Value ? LNT_ID : (string)row["LNT_ID"];
            LNT_PTYPES = row["LNT_PTYPES"] == DBNull.Value ? LNT_PTYPES : (string)row["LNT_PTYPES"];
            LNT_CALLBACK = row["LNT_CALLBACK"] == DBNull.Value ? LNT_CALLBACK : (string)row["LNT_CALLBACK"];
            LNT_DIV_TRACKING = row["LNT_DIV_TRACKING"] == DBNull.Value ? LNT_DIV_TRACKING : (string)row["LNT_DIV_TRACKING"];
            LNT_AUTO_MARKS = row["LNT_AUTO_MARKS"] == DBNull.Value ? LNT_AUTO_MARKS : (string)row["LNT_AUTO_MARKS"];
            LNT_NC_COLL_TYPE = row["LNT_NC_COLL_TYPE"] == DBNull.Value ? LNT_NC_COLL_TYPE : (string)row["LNT_NC_COLL_TYPE"];
            LNT_CollateralFlag = row["LNT_CollateralFlag"] == DBNull.Value ? LNT_CollateralFlag : (string)row["LNT_CollateralFlag"];            
            LNT_GOLIM = row["LNT_GOLIM"] == DBNull.Value ? LNT_GOLIM : (string)row["LNT_GOLIM"];

            TimeTableID = row["TimeTableID"] == DBNull.Value ? TimeTableID : (int)row["TimeTableID"];
            
            //CheckIfNull();
            //ValidateData();   //mark_elig and divtrack keep triggering, investigate
        }
        

        //Needs items in the parameter list.  Haven't done this yet since there is no need
        public ve_g1position_gqlObject()
        {
            this.BookName = BookName;
            this.CPTY = CPTY;
            this.NAME = NAME;
            this.CtpyAgentAccount = CtpyAgentAccount;
            this.LoanNet_Counterparty_Id = LoanNet_Counterparty_Id;
            this.BL = BL;
            this.COLL_FLG = COLL_FLG;
            this.CUSIP = CUSIP;
            this.ISIN = ISIN;
            this.SEDOL = SEDOL;
            this.TRADE = TRADE;
            this.QTY = QTY;
            this.LNVAL = LNVAL;
            this.LNRATE = LNRATE;
            this.CRATE = CRATE;
            this.UN_FLAG = UN_FLAG;
            this.MKT_VAL = MKT_VAL;
            this.LNMRG = LNMRG;
            this.CASH = CASH;
            this.LoanNetMark_Rounding_FACTOR = LoanNetMark_Rounding_FACTOR;
            this.BOND = BOND;
            this.LNCUR = LNCUR;
            this.CSET_DT = CSET_DT;
            this.SSET_DT = SSET_DT;
            this.TERMDT = TERMDT;
            this.OP = OP;
            this.DIV_AGE = DIV_AGE;
            this.CALL = CALL;
            this.MARK_ELIG = MARK_ELIG;
            this.BGNREF = BGNREF;
            this.DIV_TRACK = DIV_TRACK;
            this.LNT_INC_COL = LNT_INC_COL;
            this.LNT_INC_PEND = LNT_INC_PEND;
            this.INTERCO_REF = INTERCO_REF;
            this.TimeTableID = TimeTableID;
            //Fill in the LNT_Columns

        }

        public ve_g1position_gqlObject(ve_g1position_gqlObject x)
        {
            this.BookName = x.BookName;
            this.CPTY = x.CPTY;
            this.NAME = x.NAME;
            this.CtpyAgentAccount = x.CtpyAgentAccount;
            this.LoanNet_Counterparty_Id = x.LoanNet_Counterparty_Id;
            this.BL = x.BL;
            this.COLL_FLG = x.COLL_FLG;
            this.CUSIP = x.CUSIP;
            this.ISIN = x.ISIN;
            this.SEDOL = x.SEDOL;
            this.TRADE = x.TRADE;
            this.QTY = x.QTY;
            this.LNVAL = x.LNVAL;
            this.LNRATE = x.LNRATE;
            this.CRATE = x.CRATE;
            this.UN_FLAG = x.UN_FLAG;
            this.MKT_VAL = x.MKT_VAL;
            this.LNMRG = x.LNMRG;
            this.CASH = x.CASH;
            this.LoanNetMark_Rounding_FACTOR = x.LoanNetMark_Rounding_FACTOR;
            this.BOND = x.BOND;
            this.LNCUR = x.LNCUR;
            this.CSET_DT = x.CSET_DT;
            this.SSET_DT = x.SSET_DT;
            this.TERMDT = x.TERMDT;
            this.OP = x.OP;
            this.DIV_AGE = x.DIV_AGE;
            this.CALL = x.CALL;
            this.MARK_ELIG = x.MARK_ELIG;
            this.BGNREF = x.BGNREF;
            this.DIV_TRACK = x.DIV_TRACK;
            this.INTERCO_REF = x.INTERCO_REF;

            //this.LNT_YN=x.LNT_YN;
            this.LNT_INC_COL = x.LNT_INC_COL;
            this.LNT_INC_PEND = x.LNT_INC_PEND;
            this.LNT_ZONE=x.LNT_ZONE;
            this.LNT_ID=x.LNT_ID;
            this.LNT_PTYPES=x.LNT_PTYPES;
            this.LNT_CALLBACK = x.LNT_CALLBACK;
            this.LNT_DIV_TRACKING=x.LNT_DIV_TRACKING;
            this.LNT_AUTO_MARKS=x.LNT_AUTO_MARKS;
            this.LNT_NC_COLL_TYPE=x.LNT_NC_COLL_TYPE;
            this.LNT_CollateralFlag = x.LNT_CollateralFlag;
            this.LNT_GOLIM = x.LNT_GOLIM;
            this.TimeTableID = x.TimeTableID;
        }
        
        public void Replicate(ve_g1position_gqlObject x)
        {
            this.BookName = x.BookName;
            this.CPTY = x.CPTY;
            this.NAME = x.NAME;
            this.CtpyAgentAccount = x.CtpyAgentAccount;
            this.LoanNet_Counterparty_Id = x.LoanNet_Counterparty_Id;
            this.BL = x.BL;
            this.COLL_FLG = x.COLL_FLG;
            this.CUSIP = x.CUSIP;
            this.ISIN = x.ISIN;
            this.SEDOL = x.SEDOL;
            this.TRADE = x.TRADE;
            this.QTY = x.QTY;
            this.LNVAL = x.LNVAL;
            this.LNRATE = x.LNRATE;
            this.CRATE = x.CRATE;
            this.UN_FLAG = x.UN_FLAG;
            this.MKT_VAL = x.MKT_VAL;
            this.LNMRG = x.LNMRG;
            this.CASH = x.CASH;
            this.LoanNetMark_Rounding_FACTOR = x.LoanNetMark_Rounding_FACTOR;
            this.BOND = x.BOND;
            this.LNCUR = x.LNCUR;
            this.CSET_DT = x.CSET_DT;
            this.SSET_DT = x.SSET_DT;
            this.TERMDT = x.TERMDT;
            this.OP = x.OP;
            this.DIV_AGE = x.DIV_AGE;
            this.CALL = x.CALL;
            this.MARK_ELIG = x.MARK_ELIG;
            this.BGNREF = x.BGNREF;
            this.DIV_TRACK = x.DIV_TRACK;
            this.INTERCO_REF = x.INTERCO_REF;

            //this.LNT_YN = x.LNT_YN;
            this.LNT_INC_COL = x.LNT_INC_COL;
            this.LNT_INC_PEND = x.LNT_INC_PEND;
            this.LNT_ZONE = x.LNT_ZONE;
            this.LNT_ID = x.LNT_ID;
            this.LNT_PTYPES = x.LNT_PTYPES;
            this.LNT_CALLBACK = x.LNT_CALLBACK;
            this.LNT_DIV_TRACKING = x.LNT_DIV_TRACKING;
            this.LNT_AUTO_MARKS = x.LNT_AUTO_MARKS;
            this.LNT_NC_COLL_TYPE = x.LNT_NC_COLL_TYPE;
            this.LNT_CollateralFlag = x.LNT_CollateralFlag;
            this.LNT_GOLIM = x.LNT_GOLIM;
            this.TimeTableID = x.TimeTableID;
        }

        private void CheckIfNull()
        {
            if (string.IsNullOrEmpty(BookName)) { BookName = "X"; WriteErrors("CheckIfNull", "BookName"); }
            if (string.IsNullOrEmpty(LoanNet_Counterparty_Id)) { LoanNet_Counterparty_Id = "X"; WriteErrors("CheckIfNull", "LoanNet_Counterparty_Id"); }
            if (string.IsNullOrEmpty(BL)) { BL = "X"; WriteErrors("CheckIfNull", "BL"); }
            if (string.IsNullOrEmpty(COLL_FLG)) { COLL_FLG = "X"; WriteErrors("CheckIfNull", "COLL_FLG"); }
            //if (string.IsNullOrEmpty(CUSIP)) { CUSIP = "X"; }         //Business Rule needs this to be null when it is null
            if (string.IsNullOrEmpty(SEDOL)) { SEDOL = "X"; WriteErrors("CheckIfNull", "SEDOL"); }
            if (!TRADE.HasValue) { TRADE = new DateTime(1900, 1, 1); WriteErrors("CheckIfNull", "TRADE"); }
            if (!QTY.HasValue) { QTY = 0; WriteErrors("CheckIfNull", "QTY"); }
            if (!LNVAL.HasValue) { LNVAL = 0; WriteErrors("CheckIfNull", "LNVAL"); }
            if (!LNRATE.HasValue) { LNRATE = 0; WriteErrors("CheckIfNull", "LNRATE"); }
            if (!CRATE.HasValue) { CRATE = 0; WriteErrors("CheckIfNull", "CRATE"); }
            if (string.IsNullOrEmpty(UN_FLAG)) { UN_FLAG = "X"; WriteErrors("CheckIfNull", "UN_FLAG"); }
            if (!MKT_VAL.HasValue) { MKT_VAL = 0; WriteErrors("CheckIfNull", "MKT_VAL"); }
            if (!LNMRG.HasValue) { LNMRG = 0; WriteErrors("CheckIfNull", "LNMRG"); }
            if (string.IsNullOrEmpty(CASH)) { CASH = "X"; WriteErrors("CheckIfNull", "CASH"); }
            if (string.IsNullOrEmpty(LoanNetMark_Rounding_FACTOR)) { LoanNetMark_Rounding_FACTOR = "X"; WriteErrors("CheckIfNull", "LoanNetMark_Rounding_FACTOR"); }
            if (string.IsNullOrEmpty(BOND)) { BOND = "X"; WriteErrors("CheckIfNull", "BOND"); }
            if (string.IsNullOrEmpty(LNCUR)) { LNCUR = "X"; WriteErrors("CheckIfNull", "LNCUR"); }
            //if(!CSET_DT.HasValue){}               //Business Rule needs this to be null when it is null
            //if(!SSET_DT.HasValue){}               //Business Rule needs this to be null when it is null
            //if(!TERMDT.HasValue){}                //Business Rule needs this to be null when it is null
            if (string.IsNullOrEmpty(OP)) { OP = "X"; WriteErrors("CheckIfNull", "OP"); }
            if (!DIV_AGE.HasValue) { DIV_AGE = 0; WriteErrors("CheckIfNull", "DIV_AGE"); }
            if (string.IsNullOrEmpty(CALL)) { CALL = "X"; WriteErrors("CheckIfNull", "CALL"); }
            if (string.IsNullOrEmpty(MARK_ELIG)) { MARK_ELIG = "X"; WriteErrors("CheckIfNull", "MARK_ELIG"); }
            if (string.IsNullOrEmpty(BGNREF)) { BGNREF = "X"; WriteErrors("CheckIfNull", "BGNREF"); }
            if (string.IsNullOrEmpty(DIV_TRACK)) { DIV_TRACK = "X"; WriteErrors("CheckIfNull", "DIV_TRACK"); }            
            if (string.IsNullOrEmpty(INTERCO_REF)) { INTERCO_REF = "X"; WriteErrors("CheckIfNull", "INTERCO_REF"); }

            //if (string.IsNullOrEmpty(LNT_YN)) { LNT_YN = "X"; WriteErrors("CheckIfNull", "LNT_YN"); }
            if (string.IsNullOrEmpty(LNT_INC_COL)) { LNT_INC_COL = "X"; WriteErrors("CheckIfNull", "LNT_INC_COL"); }
            if (string.IsNullOrEmpty(LNT_INC_PEND)) { LNT_INC_PEND = "X"; WriteErrors("CheckIfNull", "LNT_INC_PEND"); }
            if (!LNT_ZONE.HasValue) { LNT_ZONE = 0; WriteErrors("CheckIfNull", "LNT_ZONE"); }
            if (string.IsNullOrEmpty(LNT_ID)) { LNT_ID = "X"; WriteErrors("CheckIfNull", "LNT_ID"); }
            if (string.IsNullOrEmpty(LNT_PTYPES)) { LNT_PTYPES = "X"; WriteErrors("CheckIfNull", "LNT_PTYPES"); }
            if (string.IsNullOrEmpty(LNT_CALLBACK)) { LNT_CALLBACK = "X"; WriteErrors("CheckIfNull", "LNT_CALLBACK"); }
            if (string.IsNullOrEmpty(LNT_DIV_TRACKING)) { LNT_DIV_TRACKING = "X"; WriteErrors("CheckIfNull", "LNT_DIV_TRACKING"); }
            if (string.IsNullOrEmpty(LNT_AUTO_MARKS)) { LNT_AUTO_MARKS = "X"; WriteErrors("CheckIfNull", "LNT_AUTO_MARKS"); }
            if (string.IsNullOrEmpty(LNT_NC_COLL_TYPE)) { LNT_NC_COLL_TYPE = "X"; WriteErrors("CheckIfNull", "LNT_NC_COLL_TYPE"); }
            if (string.IsNullOrEmpty(LNT_CollateralFlag)) { LNT_CollateralFlag = "X"; WriteErrors("CheckIfNull", "LNT_Collateral_Flag"); }
            if (string.IsNullOrEmpty(LNT_GOLIM)) { LNT_GOLIM = "X"; WriteErrors("CheckIfNull", "LNT_GOLIM"); }

        }
                
        private void ValidateData()
        {
            if (Regex.IsMatch(this.BL.ToLower(), @"[^bl]", RegexOptions.IgnoreCase)) { Utils.LogError(BGNREF + ": " + "BL does not equal B or L"); }
            if (this.BL.Length != 1) { Utils.LogError(BGNREF + ": " + "BL length is not  the required length of 1"); }

            if (Regex.IsMatch(this.COLL_FLG.ToLower(), @"[^ct]", RegexOptions.IgnoreCase)) { Utils.LogError(BGNREF + ": " + "COLL_FLG does not equal C or T"); }
            if (this.COLL_FLG.Length != 1) { Utils.LogError(BGNREF + ": " + "COLL_FLG length is not  the required length of 1"); }

            if (!string.IsNullOrEmpty(CUSIP)) { if (CUSIP.Length != 9) { Utils.LogError("BGNREF: " + BGNREF + " CUSIP is not the right length of 9"); } }

            if (Regex.IsMatch(this.LNMRG.ToString(), @"[^(000)(102)(105)]", RegexOptions.IgnoreCase)) { Utils.LogError(BGNREF + ": " + "LNMRG does not equal C or N"); }
            if (this.LNMRG.ToString().Length != 3) { Utils.LogError(BGNREF + ": " + "LNMRG (Loanet Mark Parameter) length is not the required length of 3"); }

            if (Regex.IsMatch(this.CASH.ToLower(), @"[^cn]", RegexOptions.IgnoreCase)) { Utils.LogError(BGNREF + ": " + "CASH does not equal C or N"); }
            if (this.CASH.Length != 1) { Utils.LogError(BGNREF + ": " + "Cash Length is not 1"); }

            if (this.LoanNetMark_Rounding_FACTOR.Length != 1) { Utils.LogError(BGNREF + ": " + "Loanet Mark Rounding Factor length is not the required length of 1"); }

            if (Regex.IsMatch(this.BOND.ToLower(), @"[^yn]", RegexOptions.IgnoreCase)) { Utils.LogError(BGNREF + ": " + "BOND does not equal B or L"); }
            if (this.BOND.Length != 1) { Utils.LogError(BGNREF + ": " + "BOND length is not  the required length of 1"); }

            if (Regex.IsMatch(this.OP.ToLower(), @"[^op]", RegexOptions.IgnoreCase)) { Utils.LogError(BGNREF + ": " + "OP does not equal o or p"); }
            if (this.OP.Length != 1) { Utils.LogError(BGNREF + ": " + "OP length is not  the required length of 1"); }

            if (Regex.IsMatch(this.CALL.ToLower(), @"[^yn]", RegexOptions.IgnoreCase)) { Utils.LogError(BGNREF + ": " + "CALL does not equal y or n"); }
            if (this.CALL.Length != 1) { Utils.LogError(BGNREF + ": " + "CALL length is not the required length of 1"); }

            if (Regex.IsMatch(this.MARK_ELIG.ToLower(), @"[^yn ]", RegexOptions.IgnoreCase)) { Utils.LogError(BGNREF + ": " + "MARK_ELIG does not equal y,n, or space"); }
            if (this.MARK_ELIG.Length != 1) { Utils.LogError(BGNREF + ": " + "Mark_Elig length is not the required length of 1"); }

            if (Regex.IsMatch(this.DIV_TRACK.ToLower(), @"[^yn ]", RegexOptions.IgnoreCase)) { Utils.LogError(BGNREF + ": " + "DIV_TRACK does not equal y,n, or space"); }
            if (this.DIV_TRACK.Length != 1) { Utils.LogError(BGNREF + ": " + "DIV_TRACK (Income Tracking Indicator) length is not the required length of 1"); }

            if (Regex.IsMatch(this.LNT_INC_COL.ToLower(), @"[^yn]", RegexOptions.IgnoreCase)) { Utils.LogError(BGNREF + ": " + "LNT_INC_COL does not equal y or n"); }
            if (this.LNT_INC_COL.Length != 1) { Utils.LogError(BGNREF + ": " + "LNT_INC_COL length is not the required length of 1"); }

            if (Regex.IsMatch(this.LNT_INC_PEND.ToLower(), @"[^yn]", RegexOptions.IgnoreCase)) { Utils.LogError(BGNREF + ": " + "LNT_INC_PEND does not equal y or n"); }
            if (this.LNT_INC_PEND.Length != 1) { Utils.LogError(BGNREF + ": " + "LNT_INC_PEND length is not the required length of 1"); }

            
        }

        private void WriteErrors(string MethodName, string FieldName)
        {
            Errors.AppendLine("BGNREF: " + BGNREF + " " + FieldName + " did not have value.  Value was added");
        }

        //If New gives issues, revert back to this
        private void CheckIfNullWithOutLogging()
        {
            if (string.IsNullOrEmpty(BookName)) { BookName = "X"; }
            if (string.IsNullOrEmpty(LoanNet_Counterparty_Id)) { LoanNet_Counterparty_Id = "X"; }
            if (string.IsNullOrEmpty(BL)) { BL = "X"; }
            if (string.IsNullOrEmpty(COLL_FLG)) { COLL_FLG = "X"; }
            //if (string.IsNullOrEmpty(CUSIP)) { CUSIP = "X"; }         //Business Rule needs this to be null when it is null
            if (string.IsNullOrEmpty(SEDOL)) { SEDOL = "X"; }
            if (!TRADE.HasValue) { TRADE = new DateTime(1900, 1, 1); }
            if (!QTY.HasValue) { QTY = 0; }
            if (!LNVAL.HasValue) { LNVAL = 0; }
            if (!LNRATE.HasValue) { LNRATE = 0; }
            if (!CRATE.HasValue) { CRATE = 0; }
            if (string.IsNullOrEmpty(UN_FLAG)) { UN_FLAG = "X"; }
            if (!MKT_VAL.HasValue) { MKT_VAL = 0; }
            if (!LNMRG.HasValue) { LNMRG = 0; }
            if (string.IsNullOrEmpty(CASH)) { CASH = "X"; }
            if (string.IsNullOrEmpty(LoanNetMark_Rounding_FACTOR)) { LoanNetMark_Rounding_FACTOR = "X"; }
            if (string.IsNullOrEmpty(BOND)) { BOND = "X"; }
            if (string.IsNullOrEmpty(LNCUR)) { LNCUR = "X"; }
            //if(!CSET_DT.HasValue){}               //Business Rule needs this to be null when it is null
            //if(!SSET_DT.HasValue){}               //Business Rule needs this to be null when it is null
            //if(!TERMDT.HasValue){}                //Business Rule needs this to be null when it is null
            if (string.IsNullOrEmpty(OP)) { OP = "X"; }
            if (!DIV_AGE.HasValue) { DIV_AGE = 0; }
            if (string.IsNullOrEmpty(CALL)) { CALL = "X"; }
            if (string.IsNullOrEmpty(MARK_ELIG)) { MARK_ELIG = "X"; }  //Change for apex testing to N
            if (string.IsNullOrEmpty(BGNREF)) { BGNREF = "X"; }
            if (string.IsNullOrEmpty(DIV_TRACK)) { DIV_TRACK = "X"; }
            if (string.IsNullOrEmpty(LNT_INC_COL)) { LNT_INC_COL = "X"; }
            if (string.IsNullOrEmpty(LNT_INC_PEND)) { LNT_INC_PEND = "X"; }
            if (string.IsNullOrEmpty(INTERCO_REF)) { INTERCO_REF = "X"; }

        }

    }
}
