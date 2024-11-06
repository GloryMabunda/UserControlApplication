--Script for creating db objects for user control app
if not exists(select * from sys.databases where name = 'UserControlDb') 
begin
	create database UserControlDb;
end
go

use UserControlDb
go


if object_id('Users') is null 
begin
	create table Users 
	(
		UserId int primary key identity,
		UserName varchar(100) not null
	)
end
go

if object_id('GroupPermissions') is null
begin
	create table GroupPermissions
	(
		GroupPermissionId int primary key identity,
		GroupPermissionName varchar(100) null
	)
end
go

if object_id('Groups') is null
begin
	create table Groups
	(
		GroupId int not null,
		GroupName varchar(100) null,
		UserId int not null,
		GroupPermissionId int not null,
		primary key (GroupId, GroupPermissionId),
		foreign key (UserId) references Users(UserId),
		foreign key (GroupPermissionId) references GroupPermissions(GroupPermissionId)
	)
end
go