set hr=%time:~0,2%
if "%hr:~0,1%" equ " " set hr=0%hr:~1,1%

"c:\Program Files\MySQL\MySQL Server 5.1\bin\mysqldump" --single-transaction -h 127.0.0.1 -u root -pbk queue_test > %date:~-4,4%%date:~-7,2%%date:~-10,2%_%hr%%time:~3,2%%time:~6,2%.sql
