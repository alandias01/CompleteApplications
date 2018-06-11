To test the whole process, this is where you start
There are 2 batch files, one to export and one to import but not import into a table for safety

ApexContractCompareCreateFileAndSendToLoanetTest.bat
Corresponding ftp script "PRODApexftp_send_files_to_loanet_test.txt"
Runs the filecreator with export switch and output file gets created in the directory of where the exe ran
Then moves all ECI* files to .\files\export\%yyyy%%mm%%dd%
Then deletes the ECI* files that were here

ContractCompareImportTest.bat
Corresponding ftp script "ftp_get_G1_files_fromloanet.txt"

Downloads files from ftp site /5239/test/out
moves it to files\import\%yyyy%%mm%%dd% and renames eco5239 to XX_ECO5239

Import does not run filecreator import

Run import manually to make sure it goes into test db

goto c:\dev\backoffice\contractcomparetest
Run app , GUI opens up.  Click menu option Open->Process Breaks.  If your in test, breaks will goto test breaks




GoLive notes
prev exporter
shut off only 5239
for ftp txt file, test adding other files for upload and download

current export should run with 5239 disabled.  Test prod one with modified one.  Compare files
Then have it run one after the other

Import should run.  Then your import with 5239 only enabled.  What about the prev import
Will the prev impirt get all files?