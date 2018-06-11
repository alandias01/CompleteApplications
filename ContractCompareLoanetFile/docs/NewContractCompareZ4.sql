select h.LoanNet_Counterparty_Id
		,h.LoanNetMark_Rounding_FACTOR LoanNetMarkRoundingFACTOR
		,h.*
	from helium.mpdata_load.dbo.ve_LoanNet_Position h
		where 1=1
		--and loannet_eligable='y'		
		and h.FileDate = '2014-09-09'
		and h.LoanNetMark_Rounding_FACTOR in ('2')
		and h.op='o'
		--and h.LoanNet_Counterparty_Id='0997'
		--and h.cpty like '%647%'
		--and bookname=@bookname
		--and LoanNet_Zone=@LoanNet_Zone
		order by h.LoanNet_Counterparty_Id
		

select h.LoanNet_Counterparty_Id 
		,h.LoanNetMark_Rounding_FACTOR 		
		from helium.mpdata_load.dbo.ve_LoanNet_Position h
			where 1=1
			and loannet_eligable='y'		
			and h.FileDate = '2014-09-09'
				group by 	h.LoanNet_Counterparty_Id,h.LoanNetMark_Rounding_FACTOR
				order by h.LoanNet_Counterparty_Id

select LoanNet_Counterparty_Id, count(*) from #t group by LoanNet_Counterparty_Id

SELECT *
	FROM Helium.mpdata_load.dbo.ve_g1counterparty_gql
		WHERE filedate='2014-09-09'
		and code='5166'
		

SELECT bookname,code, rnd_std, count(*)
	FROM Helium.mpdata_load.dbo.ve_g1counterparty_gql
		WHERE filedate='2014-09-09'
		group by bookname,code, rnd_std
		order by code



select *
	from Helium.mpdata_load.dbo.APEXCounterpartyMARK_ROUNDING_FACTOR
		


