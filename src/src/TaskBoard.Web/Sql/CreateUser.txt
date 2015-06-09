-- in master (switch in the dropdown in SSMS, "use master" does not work in SQL Azure)
CREATE LOGIN [taskboard_app_user] WITH PASSWORD=N'<password goes here>'

-- in TaskBoard_db
CREATE USER [taskboard_app_user] FOR LOGIN [taskboard_app_user]

EXEC sp_addrolemember N'db_datareader', N'taskboard_app_user'
EXEC sp_addrolemember N'db_datawriter', N'taskboard_app_user'