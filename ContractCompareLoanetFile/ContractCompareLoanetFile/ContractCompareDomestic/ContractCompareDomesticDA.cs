using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WPFUtils;

namespace ContractCompareLoanetFile.ContractCompareDomestic
{
	public class ContractCompareDomesticDA
	{
		#region Sql Variables
        string cstr = Properties.Settings.Default.spHeliumDataForContractCompareLoanetFile;
        //string cstr = @"Data Source=maplesqldev; Initial Catalog=ContractCompare; Integrated Security=True";

		#endregion

		public void GetAllContractCompareDomestic(ICollection<ContractCompareDomesticObject> list)
		{
			//Change to SP if required
			string sql =@"SELECT [ContractCompareDomestic].* FROM [dbo].[ContractCompareDomestic]";

			using (SqlConnection conn = new SqlConnection(cstr))
			{
				SqlCommand cmd = new SqlCommand(sql, conn);
				//cmd.CommandType=CommandType.StoredProcedure;

				//cmd.Parameters.AddWithValue(",");


				SqlDataReader rdr = null;
				try
				{
					conn.Open(); 
                    rdr = cmd.ExecuteReader();
					while (rdr.Read()) 
                    {
                        list.Add(new ContractCompareDomesticObject(rdr));
                    }
				}

				//Only use whats in catch block if you included Log.cs
				catch (SqlException ex) 
                {
                    Utils.LogError(ex.Message); 
                }

				finally
				{
					if (rdr != null)
					{
						rdr.Close();
					}
				}

			}
		}
        
		public void InsertSingle(ContractCompareDomesticObject obj)
		{
			//Change to SP if required
			string sql =@"INSERT INTO ContractCompareDomestic(DateOfData, DateOfImport, RecordType, ParticipantID, ContraPartyID, ActivityCode, UserContractInternalReferenceNumber, Filler1, SecurityID, SecurityIDType, OpenQuantity, ContractValue, RebateRateCode, RebateFeeRate, CollateralType, Filler2, DeliveryDateofDeal, CloseDateorTermDateofTransaction, Filler3, UserContractInfo, SunGardUseOnly1, ComparisonCode, InternalAccountNumber, Filler4, SunGardUseOnly2, MarginParameter, RoundingDirection, MarkRoundingFactor, Filler5, AccruedBondInterestincludedinmarks, Filler6, SunGardUseOnly3, DividendFlowThrough, Filler7, IncomeTrackingIndicator, SunGardUseOnly4, Filler8, SunGardUseOnly5, OCCHedgeContract, CustodianID, SubAccount, Filler9, SunGardUseOnly6) VALUES(@DateOfData, @DateOfImport, @RecordType, @ParticipantID, @ContraPartyID, @ActivityCode, @UserContractInternalReferenceNumber, @Filler1, @SecurityID, @SecurityIDType, @OpenQuantity, @ContractValue, @RebateRateCode, @RebateFeeRate, @CollateralType, @Filler2, @DeliveryDateofDeal, @CloseDateorTermDateofTransaction, @Filler3, @UserContractInfo, @SunGardUseOnly1, @ComparisonCode, @InternalAccountNumber, @Filler4, @SunGardUseOnly2, @MarginParameter, @RoundingDirection, @MarkRoundingFactor, @Filler5, @AccruedBondInterestincludedinmarks, @Filler6, @SunGardUseOnly3, @DividendFlowThrough, @Filler7, @IncomeTrackingIndicator, @SunGardUseOnly4, @Filler8, @SunGardUseOnly5, @OCCHedgeContract, @CustodianID, @SubAccount, @Filler9, @SunGardUseOnly6)";

			using (SqlConnection conn = new SqlConnection(cstr))
			{
				SqlCommand cmd = new SqlCommand(sql, conn);
				//cmd.CommandType=CommandType.StoredProcedure;

				//cmd.Parameters.AddWithValue(",");
                				
				cmd.Parameters.AddWithValue("@DateOfData", SetNullValue(obj.DateOfData));
				cmd.Parameters.AddWithValue("@DateOfImport", SetNullValue(obj.DateOfImport));
				cmd.Parameters.AddWithValue("@RecordType", SetNullValue(obj.RecordType));
				cmd.Parameters.AddWithValue("@ParticipantID", SetNullValue(obj.ParticipantID));
				cmd.Parameters.AddWithValue("@ContraPartyID", SetNullValue(obj.ContraPartyID));
				cmd.Parameters.AddWithValue("@ActivityCode", SetNullValue(obj.ActivityCode));
				cmd.Parameters.AddWithValue("@UserContractInternalReferenceNumber", SetNullValue(obj.UserContractInternalReferenceNumber));
				cmd.Parameters.AddWithValue("@Filler1", SetNullValue(obj.Filler1));
				cmd.Parameters.AddWithValue("@SecurityID", SetNullValue(obj.SecurityID));
				cmd.Parameters.AddWithValue("@SecurityIDType", SetNullValue(obj.SecurityIDType));
				cmd.Parameters.AddWithValue("@OpenQuantity", SetNullValue(obj.OpenQuantity));
				cmd.Parameters.AddWithValue("@ContractValue", SetNullValue(obj.ContractValue));
				cmd.Parameters.AddWithValue("@RebateRateCode", SetNullValue(obj.RebateRateCode));
				cmd.Parameters.AddWithValue("@RebateFeeRate", SetNullValue(obj.RebateFeeRate));
				cmd.Parameters.AddWithValue("@CollateralType", SetNullValue(obj.CollateralType));
				cmd.Parameters.AddWithValue("@Filler2", SetNullValue(obj.Filler2));
				cmd.Parameters.AddWithValue("@DeliveryDateofDeal", SetNullValue(obj.DeliveryDateofDeal));
				cmd.Parameters.AddWithValue("@CloseDateorTermDateofTransaction", SetNullValue(obj.CloseDateorTermDateofTransaction));
				cmd.Parameters.AddWithValue("@Filler3", SetNullValue(obj.Filler3));
				cmd.Parameters.AddWithValue("@UserContractInfo", SetNullValue(obj.UserContractInfo));
				cmd.Parameters.AddWithValue("@SunGardUseOnly1", SetNullValue(obj.SunGardUseOnly1));
				cmd.Parameters.AddWithValue("@ComparisonCode", SetNullValue(obj.ComparisonCode));
				cmd.Parameters.AddWithValue("@InternalAccountNumber", SetNullValue(obj.InternalAccountNumber));
				cmd.Parameters.AddWithValue("@Filler4", SetNullValue(obj.Filler4));
				cmd.Parameters.AddWithValue("@SunGardUseOnly2", SetNullValue(obj.SunGardUseOnly2));
				cmd.Parameters.AddWithValue("@MarginParameter", SetNullValue(obj.MarginParameter));
				cmd.Parameters.AddWithValue("@RoundingDirection", SetNullValue(obj.RoundingDirection));
				cmd.Parameters.AddWithValue("@MarkRoundingFactor", SetNullValue(obj.MarkRoundingFactor));
				cmd.Parameters.AddWithValue("@Filler5", SetNullValue(obj.Filler5));
				cmd.Parameters.AddWithValue("@AccruedBondInterestincludedinmarks", SetNullValue(obj.AccruedBondInterestincludedinmarks));
				cmd.Parameters.AddWithValue("@Filler6", SetNullValue(obj.Filler6));
				cmd.Parameters.AddWithValue("@SunGardUseOnly3", SetNullValue(obj.SunGardUseOnly3));
				cmd.Parameters.AddWithValue("@DividendFlowThrough", SetNullValue(obj.DividendFlowThrough));
				cmd.Parameters.AddWithValue("@Filler7", SetNullValue(obj.Filler7));
				cmd.Parameters.AddWithValue("@IncomeTrackingIndicator", SetNullValue(obj.IncomeTrackingIndicator));
				cmd.Parameters.AddWithValue("@SunGardUseOnly4", SetNullValue(obj.SunGardUseOnly4));
				cmd.Parameters.AddWithValue("@Filler8", SetNullValue(obj.Filler8));
				cmd.Parameters.AddWithValue("@SunGardUseOnly5", SetNullValue(obj.SunGardUseOnly5));
				cmd.Parameters.AddWithValue("@OCCHedgeContract", SetNullValue(obj.OCCHedgeContract));
				cmd.Parameters.AddWithValue("@CustodianID", SetNullValue(obj.CustodianID));
				cmd.Parameters.AddWithValue("@SubAccount", SetNullValue(obj.SubAccount));
				cmd.Parameters.AddWithValue("@Filler9", SetNullValue(obj.Filler9));
				cmd.Parameters.AddWithValue("@SunGardUseOnly6", SetNullValue(obj.SunGardUseOnly6));

				try
				{
					conn.Open();
					cmd.ExecuteNonQuery();
				}

				//Only use whats in catch block if you included Log.cs
				catch (SqlException ex) 
                {
                    Utils.LogError(ex.Message); 
                }

			}
		}

        public void DeleteByDateAndBook(DateTime DateOfDate, string ParticipantID)
        {
            //Change to SP if required
            string sql = @"DELETE FROM ContractCompareDomestic where DateOfData=@DateOfData and ParticipantID=@ParticipantID";

            using (SqlConnection conn = new SqlConnection(cstr))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                //cmd.CommandType=CommandType.StoredProcedure;
                                
                cmd.Parameters.AddWithValue("@DateOfData", DateOfDate);
                cmd.Parameters.AddWithValue("@ParticipantID", ParticipantID);
                
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                //Only use whats in catch block if you included Log.cs
                catch (SqlException ex)
                {
                    Utils.LogError(ex.Message);
                }

            }
        }


		//Sets DBNULL if NULL and if string is empty, sets to null
		private object SetNullValue(object S)
		{

			if (S is string)
			{
				if (string.IsNullOrEmpty((string)S))
				{
					return DBNull.Value;
				}
				else { return S; }

			}

			else if (S == null)
			{
				return DBNull.Value;
			}

			else { return S; }
		}
	}
}
