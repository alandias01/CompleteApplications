
SET SRCDIR=C:\dev\Maple\DTC\PositionClient\trunk\bin\Debug
SET DESDIR=C$\Maple\DTC\RealTimePositionViewer
SET SRCSC=C:\dev\Maple\DTC\PositionClient\trunk\Install\RealTimePositionViewer.lnk


REM David
xcopy %SRCDIR% \\javaheri\%DESDIR% /Y /I /E
xcopy %SRCSC% "\\javaheri\c$\Documents and Settings\All Users\Desktop\" /V /Y

REM Rich
xcopy %SRCDIR%  \\meyer\%DESDIR% /Y /I /E
xcopy %SRCSC% "\\meyer\c$\Documents and Settings\All Users\Desktop\" /V /Y


REM Jason
REM xcopy %SRCDIR% \\sangekarxp\%DESDIR% /Y /I /E
REM xcopy %SRCSC% "\\sangekarxp\c$\Documents and Settings\All Users\Desktop\" /V /Y


pause