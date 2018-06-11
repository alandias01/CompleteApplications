using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Maple.Global1.G1BizObjects;
using System.Reflection;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Maple.Dtc.PositionClient
{
    public class NonCashItemsFromCurrentPositionDa
    {
        public string cstr;
        public NonCashItemsFromCurrentPositionDa()
        {
            cstr = GetConnString();
        }

        public string GetConnString()
        {
            string PathToG1BizObjectsdll = AppDomain.CurrentDomain.BaseDirectory + "G1BizObjects.dll";
            Assembly dll = Assembly.LoadFile(PathToG1BizObjectsdll);
            Configuration config = ConfigurationManager.OpenExeConfiguration(dll.Location);
            string sqlConn = config.AppSettings.Settings["SqlConnString"].Value.ToString();

            return sqlConn;
        }

        public void Load(List<NonCashItemsFromCurrentPositionObject> collection)
        {
            SqlDataReader row;
			SqlCommand sp;

            string sql=@"select cp.ClearingNo,cp.counterpartycode,cp.collateraltypeindicator,cm.dtcnumber as Ctpydtcnumber,
		                cp.cusip,cp.quantity,cp.loanvalue,cp.tradereference,cp.rate, cp.tradedate,
		                cp.outstandingpendingindicator,cp.borrowloanindicator,cp.tradecategory
		                from newton.global1.dbo.currentposition cp
			                left join [Blob].[TransactionProcessor].[dbo].[CounterpartyMapping] cm
				                on cp.counterpartycode = cm.ctpy
					                where cp.collateraltypeindicator='N'
					                and cp.outstandingpendingindicator='O'";
			
            
            try
			{
				using (SqlConnection conn = new SqlConnection(cstr))
				{
					conn.Open();
					sp = new SqlCommand(sql, conn);
					sp.CommandType = CommandType.Text;
					row = sp.ExecuteReader();
					while(row.Read())
					{
						NonCashItemsFromCurrentPositionObject obj = new NonCashItemsFromCurrentPositionObject(row);
						collection.Add(obj);
					}
				}
			}
			catch(Exception e)
			{
				throw;
			}             
        }

    }



    public class NonCashItemsFromCurrentPositionObject
    {        
        public string ClearingNo { get; set; }
        public string counterpartycode { get; set; }
        public string collateraltypeindicator { get; set; }
        public string Ctpydtcnumber { get; set; }
        public string cusip { get; set; }
        public double quantity { get; set; }
        public double loanvalue { get; set; }
        public string tradereference { get; set; }
        public double rate { get; set; }
        public DateTime tradedate { get; set; }
        public string outstandingpendingindicator { get; set; }
        public string borrowloanindicator { get; set; }
        public string tradecategory { get; set; }

        public NonCashItemsFromCurrentPositionObject (SqlDataReader row)
        {
            ClearingNo = row["ClearingNo"] == DBNull.Value ? ClearingNo : (string)row["ClearingNo"];
            counterpartycode = row["counterpartycode"] == DBNull.Value ? counterpartycode : (string)row["counterpartycode"];
            collateraltypeindicator = row["collateraltypeindicator"] == DBNull.Value ? collateraltypeindicator : (string)row["collateraltypeindicator"];
            Ctpydtcnumber = row["Ctpydtcnumber"] == DBNull.Value ? Ctpydtcnumber : (string)row["Ctpydtcnumber"];
            cusip = row["cusip"] == DBNull.Value ? cusip : (string)row["cusip"];
            quantity = row["quantity"] == DBNull.Value ? quantity : (double)row["quantity"];
            loanvalue = row["loanvalue"] == DBNull.Value ? loanvalue : (double)row["loanvalue"];
            tradereference = row["tradereference"] == DBNull.Value ? tradereference : (string)row["tradereference"];
            rate = row["rate"] == DBNull.Value ? rate : (double)row["rate"];
            tradedate = row["tradedate"] == DBNull.Value ? tradedate : (DateTime)row["tradedate"];
            outstandingpendingindicator = row["outstandingpendingindicator"] == DBNull.Value ? outstandingpendingindicator : (string)row["outstandingpendingindicator"];
            borrowloanindicator = row["borrowloanindicator"] == DBNull.Value ? borrowloanindicator : (string)row["borrowloanindicator"];
            tradecategory = row["tradecategory"] == DBNull.Value ? tradecategory : (string)row["tradecategory"];
        }
    }

}
