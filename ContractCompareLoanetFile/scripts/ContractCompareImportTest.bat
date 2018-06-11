echo ------------------ ContractCompareImport Start %date%%time% ------------------------

set mm=%DATE:~4,2%
set dd=%DATE:~7,2%
set yyyy=%DATE:~10,4%

set dir=files\import\%yyyy%%mm%%dd%
mkdir %dir%

ftp -s:ftp_get_G1_files_fromloanet.txt

rename eco5239 XX_ECO5239

copy XX_ECO* %dir%

del XX_ECO*

echo ------------------  ContractCompareImport Finish %date%%time% ------------------ 
