echo ------------------ ContractCompareImport Start %date%%time% ------------------------

set mm=%DATE:~4,2%
set dd=%DATE:~7,2%
set yyyy=%DATE:~10,4%

set dir=files\export\%yyyy%%mm%%dd%
mkdir %dir%

..\ContractCompareLoanetFile\bin\debug\ContractCompareLoanetFile.exe export

ftp -s:PRODApexftp_send_files_to_loanet_test.txt

copy ECI* %dir%

del ECI*


echo ------------------  ContractCompareImport Finish %date%%time% ------------------ 
