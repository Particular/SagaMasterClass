#Requires -RunAsAdministrator

$databaseName = "sagamasterclass"

sqllocaldb create $databaseName
sqllocaldb share $databaseName $databaseName
sqllocaldb start $databaseName
sqllocaldb info $databaseName

$serverName = "(localdb)\" + $databaseName
sqlcmd -S $serverName -i ".\SetupDatabases.sql"
