EXEC sp_msforeachtable "ALTER TABLE ? NOCHECK CONSTRAINT all"
GO
EXEC sp_MSforeachtable @command1 = "DROP TABLE ?"
GO
--https://stackoverflow.com/questions/27606518/how-to-drop-all-tables-from-a-database-with-one-sql-query