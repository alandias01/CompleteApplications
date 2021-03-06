
SELECT  rtrim(UserContractInternalReferenceNumber) bgnref,* 
	FROM maplesqldev.[ContractCompare].[dbo].[ContractCompareDomestic] 
		where contrapartyid like '%286%' 
			order by rtrim(UserContractInternalReferenceNumber), contrapartyid

SELECT rtrim(user_contract_information) bgnref, *  
	FROM newton.[ContractCompare].[dbo].contractcompare_raw 
		where dateofdata>='2014-09-16' 
		and Receiving_Participant='5239'
		order by rtrim(user_contract_information)


select * 
	from blob.apex.dbo.heliumpositionview
		where filedate='2014-09-16'
		and bookname='apex-ms5239'
		and bgnref='1006409730'


select h.bookname, h.LoanNet_Counterparty_Id, h.MARK_ELIG, h.DIV_TRACK, h.bl, h.COLL_FLG,	
	h.cusip, 	
	h.isin, h.sedol, 
	h.trade, h.qty, abs(h.lnval) as lnval, h.lnrate, h.crate, h.un_flag, h.mkt_val, h.lnmrg, h.cash, h.LoanNetMark_Rounding_FACTOR, h.bond,
	h.LNCUR, h.CSET_DT, h.SSET_DT, h.TERMDT, h.OP , h.DIV_AGE , h.CALL, h.bgnref, h.INTERCO_REF,
	h.LNT_YN, h.LNT_INC_COL, h.LNT_INC_PEND, h.LNT_ZONE, h.LNT_ID, h.LNT_PTYPES, h.LNT_CALLBACK, h.LNT_DIV_TRACKING, h.LNT_AUTO_MARKS, 
	h.LNT_NC_COLL_TYPE, h.LNT_CollateralFlag, h.LNT_GOLIM,h.LoanNet_Zone
	--into #FilteredPos1
	from helium.mpdata_load.dbo.ve_LoanNet_Position h
		where loannet_eligable='y'		
			and FileDate = '2014-09-16'
			and bookname='apex-ms5239'
			and LoanNet_Zone=4
			and bgnref='1006731083'
			--and status!='r'			
			order by bgnref 		

		when 'E' then '0000'
		when '5' then '0050'
		when '1' then '0100'
		when '8' then '0125'
		when '4' then '0250'
		when '2' then '0500'
		when 'U' then '1000'
		when 'H' then '0500'
		else 'XXXX'			