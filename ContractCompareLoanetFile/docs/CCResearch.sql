
SELECT * 
  FROM newton.[ContractCompare].[dbo].[ContractCompare_raw]
	where dateofdata='2016-01-28'
	--and receiving_participant='0269'

SELECT * 
   FROM maplesqldev.[ContractCompare].[dbo].[ContractCompare_raw]
	where dateofdata>='2016-01-21' order by account_number

--delete FROM maplesqldev.[ContractCompare].[dbo].[ContractCompare_raw]	where dateofdata='2016-01-21'

SELECT *
  FROM newton.[ContractCompare].[dbo].[ContractCompare_breaks]
	where dateofdata>='2016-01-21'

SELECT *
   FROM maplesqldev.[ContractCompare].[dbo].[ContractCompare_breaks]
	where dateofdata='2016-01-21'

--delete FROM maplesqldev.[ContractCompare].[dbo].[ContractCompare_breaks]	where dateofdata='2016-01-21'


SELECT *
	 FROM maplesqldev.[ContractCompare].[dbo].[ContractCompareDomestic]
		where dateofdata>='2016-01-21'
		and securityid in ('48244B100','74347R503','78462F103') order by openquantity

SELECT *
	 FROM newton.[ContractCompare].[dbo].[ContractCompareDomestic]
		where dateofdata>='2016-01-28'
		and ParticipantID='00005239'
		and ContraPartyID='00000101'
		order by securityid, openquantity



------------------------------------------RESEARCH------------------------------------------
SELECT * 
  FROM maplesqldev.[ContractCompare].[dbo].[ContractCompare_raw]
	where dateofdata='2016-01-21' 
	and cusip_number in('48244B100','74347R503','78462F103')
	order by account_number

select *
	from blob.apex.dbo.heliumpositionview
		where filedate='2016-01-28'
		and bgnref='1007337182'


select *
	from helium.mpdata_load.dbo.ve_LoanNet_Position_nj h
		where h.loannet_eligable='y'		
		and h.FileDate = '2016-01-27'
		--and h.bookname in (select book from #lbook)
		--and h.LoanNet_Zone=@LoanNet_Zone
		--and h.ptype not in ('RPI','RPO')
		and bgnref='1007306101'


select *
	from blob.apex.dbo.mark
		where 1=1-- internalsource='LoanetMarkPoster'
		and  tradesid='1006047275'

