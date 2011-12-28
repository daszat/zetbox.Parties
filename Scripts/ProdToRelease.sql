-- Kill existing users
DECLARE @dbname varchar(40)
select @dbname = 'dasz_rel_test'
DECLARE @strSQL varchar(255)

CREATE table #tmpUsers(
spid int,
eid int,
status varchar(30),
loginname varchar(50),
hostname varchar(50),
blk int,
dbname varchar(50),
cmd varchar(30),
request_id int )

INSERT INTO #tmpUsers EXEC SP_WHO


DECLARE LoginCursor CURSOR
READ_ONLY
FOR SELECT spid FROM #tmpUsers WHERE cmd != 'CHECKPOINT' AND dbname = @dbname

DECLARE @spid varchar(30)
OPEN LoginCursor

FETCH NEXT FROM LoginCursor INTO @spid
WHILE (@@fetch_status <> -1)
BEGIN
IF (@@fetch_status <> -2)
BEGIN
PRINT 'Killing ' + @spid
SET @strSQL ='KILL ' + @spid
EXEC (@strSQL)
END
FETCH NEXT FROM LoginCursor INTO @spid
END

CLOSE LoginCursor
DEALLOCATE LoginCursor

DROP table #tmpUsers

GO
-- Restore database
declare @dbfile nvarchar(500)
declare @logfile nvarchar(500)
SELECT @dbfile = filename FROM dasz_rel_test.sys.sysfiles where filename like '%.mdf'
SELECT @logfile = filename FROM dasz_rel_test.sys.sysfiles where filename like '%.ldf'

RESTORE DATABASE dasz_rel_test FROM DISK = 'c:\temp\dasz_prod.bak' 
	WITH RECOVERY, REPLACE, 
	MOVE 'dasz_prod' to @dbfile,
	MOVE 'dasz_prod' to @logfile
