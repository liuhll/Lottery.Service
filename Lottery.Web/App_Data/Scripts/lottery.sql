/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2012                    */
/* Created on:     2017/11/21 20:32:30                          */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('F_RolePower') and o.name = 'FK_F_ROLEPO_REFERENCE_F_ROLE')
alter table F_RolePower
   drop constraint FK_F_ROLEPO_REFERENCE_F_ROLE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('F_RolePower') and o.name = 'FK_F_ROLEPO_REFERENCE_F_POWER')
alter table F_RolePower
   drop constraint FK_F_ROLEPO_REFERENCE_F_POWER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('F_UserRole') and o.name = 'FK_F_USERRO_REFERENCE_F_ROLE')
alter table F_UserRole
   drop constraint FK_F_USERRO_REFERENCE_F_ROLE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('F_UserRole') and o.name = 'FK_F_USERRO_REFERENCE_F_USERIN')
alter table F_UserRole
   drop constraint FK_F_USERRO_REFERENCE_F_USERIN
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('L_NormGroup') and o.name = 'FK_L_NORMGR_REFERENCE_L_LOTTER')
alter table L_NormGroup
   drop constraint FK_L_NORMGR_REFERENCE_L_LOTTER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('L_PlanInfo') and o.name = 'FK_L_PLANIN_REFERENCE_L_NORMGR')
alter table L_PlanInfo
   drop constraint FK_L_PLANIN_REFERENCE_L_NORMGR
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('L_PlanKeyNumber') and o.name = 'FK_L_PLANKE_REFERENCE_L_PLANIN')
alter table L_PlanKeyNumber
   drop constraint FK_L_PLANKE_REFERENCE_L_PLANIN
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('L_PlanKeyNumber') and o.name = 'FK_L_PLANKE_REFERENCE_L_POSITI')
alter table L_PlanKeyNumber
   drop constraint FK_L_PLANKE_REFERENCE_L_POSITI
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('L_PositionInfo') and o.name = 'FK_L_POSITI_REFERENCE_L_LOTTER')
alter table L_PositionInfo
   drop constraint FK_L_POSITI_REFERENCE_L_LOTTER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('L_TimeRule') and o.name = 'FK_L_TIMERU_REFERENCE_L_LOTTER')
alter table L_TimeRule
   drop constraint FK_L_TIMERU_REFERENCE_L_LOTTER
go

if exists (select 1
            from  sysobjects
           where  id = object_id('F_Power')
            and   type = 'U')
   drop table F_Power
go

if exists (select 1
            from  sysobjects
           where  id = object_id('F_Role')
            and   type = 'U')
   drop table F_Role
go

if exists (select 1
            from  sysobjects
           where  id = object_id('F_RolePower')
            and   type = 'U')
   drop table F_RolePower
go

if exists (select 1
            from  sysobjects
           where  id = object_id('F_UserInfo')
            and   type = 'U')
   drop table F_UserInfo
go

if exists (select 1
            from  sysobjects
           where  id = object_id('F_UserRole')
            and   type = 'U')
   drop table F_UserRole
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LA_LotteryPredictData')
            and   type = 'U')
   drop table LA_LotteryPredictData
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LA_PlanNorm')
            and   type = 'U')
   drop table LA_PlanNorm
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LA_PlanNorm_SystemDefault')
            and   type = 'U')
   drop table LA_PlanNorm_SystemDefault
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LA_UserBasicNorm')
            and   type = 'U')
   drop table LA_UserBasicNorm
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LA_UserNorm')
            and   type = 'U')
   drop table LA_UserNorm
go

if exists (select 1
            from  sysobjects
           where  id = object_id('L_LotteryData')
            and   type = 'U')
   drop table L_LotteryData
go

if exists (select 1
            from  sysobjects
           where  id = object_id('L_LotteryFinalData')
            and   type = 'U')
   drop table L_LotteryFinalData
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('L_LotteryInfo')
            and   name  = 'Index_1'
            and   indid > 0
            and   indid < 255)
   drop index L_LotteryInfo.Index_1
go

if exists (select 1
            from  sysobjects
           where  id = object_id('L_LotteryInfo')
            and   type = 'U')
   drop table L_LotteryInfo
go

if exists (select 1
            from  sysobjects
           where  id = object_id('L_NormGroup')
            and   type = 'U')
   drop table L_NormGroup
go

if exists (select 1
            from  sysobjects
           where  id = object_id('L_PlanInfo')
            and   type = 'U')
   drop table L_PlanInfo
go

if exists (select 1
            from  sysobjects
           where  id = object_id('L_PlanKeyNumber')
            and   type = 'U')
   drop table L_PlanKeyNumber
go

if exists (select 1
            from  sysobjects
           where  id = object_id('L_PositionInfo')
            and   type = 'U')
   drop table L_PositionInfo
go

if exists (select 1
            from  sysobjects
           where  id = object_id('L_SubLotteryPredictTableMap')
            and   type = 'U')
   drop table L_SubLotteryPredictTableMap
go

if exists (select 1
            from  sysobjects
           where  id = object_id('L_TimeRule')
            and   type = 'U')
   drop table L_TimeRule
go

/*==============================================================*/
/* Table: F_Power                                               */
/*==============================================================*/
create table F_Power (
   Id                   varchar(36)          not null,
   PowerCode            varchar(18)          not null,
   PowerName            varchar(36)          null,
   ParentId             varchar(36)          null,
   Url                  varchar(36)          null,
   PowerType            varchar(18)          null,
   Description          varchar(200)         null default 'system',
   CreateBy             varchar(36)          null,
   CreateTime           datetime             null default getdate(),
   UpdateBy             varchar(36)          null,
   UpdateTime           datetime             null,
   IsDelete             bit                  null,
   constraint PK_F_POWER primary key (Id)
)
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('F_Power')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'F_Power', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键Id',
   'user', @CurrentUser, 'table', 'F_Power', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('F_Power')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PowerCode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'F_Power', 'column', 'PowerCode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '权限编码',
   'user', @CurrentUser, 'table', 'F_Power', 'column', 'PowerCode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('F_Power')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PowerName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'F_Power', 'column', 'PowerName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '权限名称',
   'user', @CurrentUser, 'table', 'F_Power', 'column', 'PowerName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('F_Power')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ParentId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'F_Power', 'column', 'ParentId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '父权限ID',
   'user', @CurrentUser, 'table', 'F_Power', 'column', 'ParentId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('F_Power')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Url')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'F_Power', 'column', 'Url'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'URI',
   'user', @CurrentUser, 'table', 'F_Power', 'column', 'Url'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('F_Power')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PowerType')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'F_Power', 'column', 'PowerType'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '权限类型',
   'user', @CurrentUser, 'table', 'F_Power', 'column', 'PowerType'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('F_Power')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Description')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'F_Power', 'column', 'Description'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '描述',
   'user', @CurrentUser, 'table', 'F_Power', 'column', 'Description'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('F_Power')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreateBy')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'F_Power', 'column', 'CreateBy'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人',
   'user', @CurrentUser, 'table', 'F_Power', 'column', 'CreateBy'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('F_Power')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'F_Power', 'column', 'CreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'F_Power', 'column', 'CreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('F_Power')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateBy')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'F_Power', 'column', 'UpdateBy'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新人',
   'user', @CurrentUser, 'table', 'F_Power', 'column', 'UpdateBy'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('F_Power')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'F_Power', 'column', 'UpdateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'F_Power', 'column', 'UpdateTime'
go

/*==============================================================*/
/* Table: F_Role                                                */
/*==============================================================*/
create table F_Role (
   Id                   varchar(36)          not null,
   RoleName             varchar(36)          null,
   Description          varchar(200)         null,
   CreateBy             varchar(36)          null,
   CreateTime           datetime             null default getdate(),
   UpdateBy             varchar(36)          null,
   UpdaeTime            datetime             null,
   IsDelete             bit                  null default 0,
   constraint PK_F_ROLE primary key (Id)
)
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('F_Role')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'F_Role', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '角色Id',
   'user', @CurrentUser, 'table', 'F_Role', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('F_Role')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'RoleName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'F_Role', 'column', 'RoleName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '角色名称',
   'user', @CurrentUser, 'table', 'F_Role', 'column', 'RoleName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('F_Role')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Description')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'F_Role', 'column', 'Description'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '描述',
   'user', @CurrentUser, 'table', 'F_Role', 'column', 'Description'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('F_Role')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreateBy')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'F_Role', 'column', 'CreateBy'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人',
   'user', @CurrentUser, 'table', 'F_Role', 'column', 'CreateBy'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('F_Role')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'F_Role', 'column', 'CreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'F_Role', 'column', 'CreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('F_Role')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateBy')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'F_Role', 'column', 'UpdateBy'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '修改人',
   'user', @CurrentUser, 'table', 'F_Role', 'column', 'UpdateBy'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('F_Role')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdaeTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'F_Role', 'column', 'UpdaeTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '修改时间',
   'user', @CurrentUser, 'table', 'F_Role', 'column', 'UpdaeTime'
go

/*==============================================================*/
/* Table: F_RolePower                                           */
/*==============================================================*/
create table F_RolePower (
   Id                   varchar(36)          not null,
   RoleId               varchar(36)          null,
   PowerId              varchar(36)          null,
   CreateBy             varchar(36)          null default 'system',
   CreateTime           datetime             not null default getdate(),
   IsDelete             bit                  null,
   constraint PK_F_ROLEPOWER primary key (Id)
)
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('F_RolePower')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'F_RolePower', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'ID',
   'user', @CurrentUser, 'table', 'F_RolePower', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('F_RolePower')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'RoleId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'F_RolePower', 'column', 'RoleId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '角色Id',
   'user', @CurrentUser, 'table', 'F_RolePower', 'column', 'RoleId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('F_RolePower')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PowerId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'F_RolePower', 'column', 'PowerId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '权限Id',
   'user', @CurrentUser, 'table', 'F_RolePower', 'column', 'PowerId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('F_RolePower')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreateBy')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'F_RolePower', 'column', 'CreateBy'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人',
   'user', @CurrentUser, 'table', 'F_RolePower', 'column', 'CreateBy'
go

/*==============================================================*/
/* Table: F_UserInfo                                            */
/*==============================================================*/
create table F_UserInfo (
   Id                   varchar(36)          not null,
   UserName             varchar(36)          null,
   SurName              varchar(36)          null,
   Email                varchar(36)          null,
   Phone                varchar(22)          null,
   Password             varchar(36)          not null,
   IsActive             bit                  not null default 1,
   TokenId              varchar(36)          null,
   LastLoginTime        datetime             null,
   QQCode               varchar(36)          null,
   Wechat               varchar(36)          null,
   WechatOpenId         varchar(36)          null,
   UserRegistType       int                  null,
   UpdateBy             datetime             null,
   UpdateTime           datetime             null,
   Createby             varchar(36)          null,
   CreateTime           datetime             not null default getdate(),
   IsDelete             bit                  null default 0,
   constraint PK_F_USERINFO primary key (Id)
)
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('F_UserInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UserName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'F_UserInfo', 'column', 'UserName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '用户名',
   'user', @CurrentUser, 'table', 'F_UserInfo', 'column', 'UserName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('F_UserInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SurName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'F_UserInfo', 'column', 'SurName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '昵称',
   'user', @CurrentUser, 'table', 'F_UserInfo', 'column', 'SurName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('F_UserInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Email')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'F_UserInfo', 'column', 'Email'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Email电子邮件',
   'user', @CurrentUser, 'table', 'F_UserInfo', 'column', 'Email'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('F_UserInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Phone')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'F_UserInfo', 'column', 'Phone'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '手机号码',
   'user', @CurrentUser, 'table', 'F_UserInfo', 'column', 'Phone'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('F_UserInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Password')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'F_UserInfo', 'column', 'Password'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '密码',
   'user', @CurrentUser, 'table', 'F_UserInfo', 'column', 'Password'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('F_UserInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsActive')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'F_UserInfo', 'column', 'IsActive'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否激活:1,有效；0.冻结',
   'user', @CurrentUser, 'table', 'F_UserInfo', 'column', 'IsActive'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('F_UserInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TokenId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'F_UserInfo', 'column', 'TokenId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '票据Id',
   'user', @CurrentUser, 'table', 'F_UserInfo', 'column', 'TokenId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('F_UserInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LastLoginTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'F_UserInfo', 'column', 'LastLoginTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后登录时间',
   'user', @CurrentUser, 'table', 'F_UserInfo', 'column', 'LastLoginTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('F_UserInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'QQCode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'F_UserInfo', 'column', 'QQCode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'qq',
   'user', @CurrentUser, 'table', 'F_UserInfo', 'column', 'QQCode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('F_UserInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Wechat')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'F_UserInfo', 'column', 'Wechat'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '微信',
   'user', @CurrentUser, 'table', 'F_UserInfo', 'column', 'Wechat'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('F_UserInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'WechatOpenId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'F_UserInfo', 'column', 'WechatOpenId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '微信OpenId',
   'user', @CurrentUser, 'table', 'F_UserInfo', 'column', 'WechatOpenId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('F_UserInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UserRegistType')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'F_UserInfo', 'column', 'UserRegistType'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '用户注册来源',
   'user', @CurrentUser, 'table', 'F_UserInfo', 'column', 'UserRegistType'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('F_UserInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateBy')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'F_UserInfo', 'column', 'UpdateBy'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人',
   'user', @CurrentUser, 'table', 'F_UserInfo', 'column', 'UpdateBy'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('F_UserInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'F_UserInfo', 'column', 'UpdateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'F_UserInfo', 'column', 'UpdateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('F_UserInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Createby')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'F_UserInfo', 'column', 'Createby'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '账号创建人',
   'user', @CurrentUser, 'table', 'F_UserInfo', 'column', 'Createby'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('F_UserInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'F_UserInfo', 'column', 'CreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'F_UserInfo', 'column', 'CreateTime'
go

/*==============================================================*/
/* Table: F_UserRole                                            */
/*==============================================================*/
create table F_UserRole (
   Id                   varchar(36)          not null,
   UserId               varchar(36)          not null,
   RoleId               varchar(36)          not null,
   CreateBy             varchar(36)          null,
   CreateTime           datetime             null default getdate(),
   IsDelete             bit                  null default 0,
   constraint PK_F_USERROLE primary key (Id)
)
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('F_UserRole')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'F_UserRole', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Id',
   'user', @CurrentUser, 'table', 'F_UserRole', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('F_UserRole')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UserId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'F_UserRole', 'column', 'UserId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '用户Id',
   'user', @CurrentUser, 'table', 'F_UserRole', 'column', 'UserId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('F_UserRole')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'RoleId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'F_UserRole', 'column', 'RoleId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '角色表',
   'user', @CurrentUser, 'table', 'F_UserRole', 'column', 'RoleId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('F_UserRole')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreateBy')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'F_UserRole', 'column', 'CreateBy'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人',
   'user', @CurrentUser, 'table', 'F_UserRole', 'column', 'CreateBy'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('F_UserRole')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'F_UserRole', 'column', 'CreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'F_UserRole', 'column', 'CreateTime'
go

/*==============================================================*/
/* Table: LA_LotteryPredictData                                 */
/*==============================================================*/
create table LA_LotteryPredictData (
   Id                   varchar(36)          not null,
   PlanNormId           varchar(36)          null,
   CurrentPredictPeriod int                  not null,
   StartPeriod          int                  null,
   EndPeriod            int                  null,
   MinorCycle           int                  null,
   PredictedData        varchar(200)         null,
   PredictedResult      int                  null,
   CreatBy              varchar(36)          null,
   CreateTime           datetime             null,
   UpdateBy             varchar(36)          null,
   UpdateTime           datetime             null,
   constraint PK_LA_LOTTERYPREDICTDATA primary key (Id)
)
go

/*==============================================================*/
/* Table: LA_PlanNorm                                           */
/*==============================================================*/
create table LA_PlanNorm (
   Id                   varchar(36)          not null,
   LotteryId            varchar(36)          null,
   PlanId               varchar(36)          null,
   PlanCycle            int                  not null,
   ForecastCount        int                  not null,
   BasicHistoryCount    int                  not null,
   UnitHistoryCount     int                  not null,
   HotWeight            decimal(18,2)        null,
   SizeWeight           decimal(18,2)        null,
   ThreeRegionWeight    decimal(18,2)        null,
   MissingValueWeight   decimal(18,2)        null,
   OddEvenWeight        decimal(18,2)        null,
   LastStartPeriod      int                  null,
   CurrentAccuracy      decimal(18,2)        null,
   IsEnable             bit                  null,
   IsDefault            bit                  null,
   CreatBy              varchar(36)          null,
   CreateTime           datetime             null,
   UpdateBy             varchar(36)          null,
   UpdateTime           datetime             null,
   constraint PK_LA_PLANNORM primary key (Id)
)
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LA_PlanNorm')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LA_PlanNorm', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键Id',
   'user', @CurrentUser, 'table', 'LA_PlanNorm', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LA_PlanNorm')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LotteryId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LA_PlanNorm', 'column', 'LotteryId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '彩种编码',
   'user', @CurrentUser, 'table', 'LA_PlanNorm', 'column', 'LotteryId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LA_PlanNorm')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PlanId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LA_PlanNorm', 'column', 'PlanId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '计划Id',
   'user', @CurrentUser, 'table', 'LA_PlanNorm', 'column', 'PlanId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LA_PlanNorm')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PlanCycle')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LA_PlanNorm', 'column', 'PlanCycle'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '计划周期',
   'user', @CurrentUser, 'table', 'LA_PlanNorm', 'column', 'PlanCycle'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LA_PlanNorm')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ForecastCount')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LA_PlanNorm', 'column', 'ForecastCount'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '预测个数',
   'user', @CurrentUser, 'table', 'LA_PlanNorm', 'column', 'ForecastCount'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LA_PlanNorm')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'BasicHistoryCount')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LA_PlanNorm', 'column', 'BasicHistoryCount'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '分析基数',
   'user', @CurrentUser, 'table', 'LA_PlanNorm', 'column', 'BasicHistoryCount'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LA_PlanNorm')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UnitHistoryCount')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LA_PlanNorm', 'column', 'UnitHistoryCount'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '分析单元数',
   'user', @CurrentUser, 'table', 'LA_PlanNorm', 'column', 'UnitHistoryCount'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LA_PlanNorm')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'HotWeight')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LA_PlanNorm', 'column', 'HotWeight'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '冷热权重',
   'user', @CurrentUser, 'table', 'LA_PlanNorm', 'column', 'HotWeight'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LA_PlanNorm')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SizeWeight')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LA_PlanNorm', 'column', 'SizeWeight'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '大小权重',
   'user', @CurrentUser, 'table', 'LA_PlanNorm', 'column', 'SizeWeight'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LA_PlanNorm')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ThreeRegionWeight')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LA_PlanNorm', 'column', 'ThreeRegionWeight'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '三区间权重',
   'user', @CurrentUser, 'table', 'LA_PlanNorm', 'column', 'ThreeRegionWeight'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LA_PlanNorm')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'MissingValueWeight')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LA_PlanNorm', 'column', 'MissingValueWeight'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '遗漏值权重',
   'user', @CurrentUser, 'table', 'LA_PlanNorm', 'column', 'MissingValueWeight'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LA_PlanNorm')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'OddEvenWeight')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LA_PlanNorm', 'column', 'OddEvenWeight'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '奇偶权重',
   'user', @CurrentUser, 'table', 'LA_PlanNorm', 'column', 'OddEvenWeight'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LA_PlanNorm')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LastStartPeriod')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LA_PlanNorm', 'column', 'LastStartPeriod'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '指标开始周期',
   'user', @CurrentUser, 'table', 'LA_PlanNorm', 'column', 'LastStartPeriod'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LA_PlanNorm')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CurrentAccuracy')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LA_PlanNorm', 'column', 'CurrentAccuracy'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '当前正确了(计算最近10期)',
   'user', @CurrentUser, 'table', 'LA_PlanNorm', 'column', 'CurrentAccuracy'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LA_PlanNorm')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsEnable')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LA_PlanNorm', 'column', 'IsEnable'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否可用',
   'user', @CurrentUser, 'table', 'LA_PlanNorm', 'column', 'IsEnable'
go

/*==============================================================*/
/* Table: LA_PlanNorm_SystemDefault                             */
/*==============================================================*/
create table LA_PlanNorm_SystemDefault (
   Id                   varchar(36)          not null,
   LotteryId            varchar(36)          null,
   PlanId               varchar(36)          null,
   PlanCycle            int                  not null,
   ForecastCount        int                  not null,
   BasicHistoryCount    int                  not null,
   UnitHistoryCount     int                  not null,
   HotWeight            decimal(18,2)        null,
   SizeWeight           decimal(18,2)        null,
   ThreeRegionWeight    decimal(18,2)        null,
   MissingValueWeight   decimal(18,2)        null,
   OddEvenWeight        decimal(18,2)        null,
   LastStartPeriod      int                  null,
   CurrentAccuracy      decimal(18,2)        null,
   IsEnable             bit                  null default 1,
   IsDefault            bit                  null default 1,
   CreatBy              varchar(36)          null,
   CreateTime           datetime             null,
   UpdateBy             varchar(36)          null,
   UpdateTime           datetime             null,
   constraint PK_LA_PLANNORM_SYSTEMDEFAULT primary key (Id)
)
go

/*==============================================================*/
/* Table: LA_UserBasicNorm                                      */
/*==============================================================*/
create table LA_UserBasicNorm (
   Id                   varchar(36)          not null,
   UserId               varchar(36)          null,
   LotteryId            varchar(36)          null,
   PlanCycle            int                  not null,
   ForecastCount        int                  not null,
   BasicHistoryCount    int                  not null,
   UnitHistoryCount     int                  not null,
   HotWeight            decimal(18,2)        null,
   SizeWeight           decimal(18,2)        null,
   ThreeRegionWeight    decimal(18,2)        null,
   MissingValueWeight   decimal(18,2)        null,
   OddEvenWeight        decimal(18,2)        null,
   LastStartPeriod      int                  null,
   CurrentAccuracy      decimal(18,2)        null,
   IsEnable             bit                  null default 1,
   CreatBy              varchar(36)          null,
   CreateTime           datetime             null,
   UpdateBy             varchar(36)          null,
   UpdateTime           datetime             null,
   constraint PK_LA_USERBASICNORM primary key (Id)
)
go

/*==============================================================*/
/* Table: LA_UserNorm                                           */
/*==============================================================*/
create table LA_UserNorm (
   Id                   varchar(36)          not null,
   UserId               varchar(36)          null,
   PlanId               varchar(36)          null,
   PlanNormId           varchar(36)          null,
   LotteryCode          varchar(36)          null,
   PlanNormTable        varchar(200)         null,
   CreateBy             varchar(36)          null,
   CreateTime           datetime             null,
   UpdateBy             varchar(36)          null,
   UpdateTime           datetime             null,
   constraint PK_LA_USERNORM primary key (Id)
)
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LA_UserNorm')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LA_UserNorm', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键Id',
   'user', @CurrentUser, 'table', 'LA_UserNorm', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LA_UserNorm')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UserId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LA_UserNorm', 'column', 'UserId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '用户Id',
   'user', @CurrentUser, 'table', 'LA_UserNorm', 'column', 'UserId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LA_UserNorm')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PlanId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LA_UserNorm', 'column', 'PlanId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '计划Id',
   'user', @CurrentUser, 'table', 'LA_UserNorm', 'column', 'PlanId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LA_UserNorm')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PlanNormId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LA_UserNorm', 'column', 'PlanNormId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '计划分析指标Id',
   'user', @CurrentUser, 'table', 'LA_UserNorm', 'column', 'PlanNormId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LA_UserNorm')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LotteryCode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LA_UserNorm', 'column', 'LotteryCode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '彩种编码',
   'user', @CurrentUser, 'table', 'LA_UserNorm', 'column', 'LotteryCode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LA_UserNorm')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PlanNormTable')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LA_UserNorm', 'column', 'PlanNormTable'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '计划分析存储的表',
   'user', @CurrentUser, 'table', 'LA_UserNorm', 'column', 'PlanNormTable'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LA_UserNorm')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreateBy')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LA_UserNorm', 'column', 'CreateBy'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人',
   'user', @CurrentUser, 'table', 'LA_UserNorm', 'column', 'CreateBy'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LA_UserNorm')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LA_UserNorm', 'column', 'CreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'LA_UserNorm', 'column', 'CreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LA_UserNorm')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateBy')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LA_UserNorm', 'column', 'UpdateBy'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '修改人',
   'user', @CurrentUser, 'table', 'LA_UserNorm', 'column', 'UpdateBy'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LA_UserNorm')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LA_UserNorm', 'column', 'UpdateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '时间时间',
   'user', @CurrentUser, 'table', 'LA_UserNorm', 'column', 'UpdateTime'
go

/*==============================================================*/
/* Table: L_LotteryData                                         */
/*==============================================================*/
create table L_LotteryData (
   Id                   varchar(36)          not null,
   Period               int                  not null,
   LotteryId            varchar(36)          null,
   Data                 varchar(200)         null,
   InsertTime           datetime             null,
   LotteryTime          datetime             null,
   constraint PK_L_LOTTERYDATA primary key (Id)
)
go

/*==============================================================*/
/* Table: L_LotteryFinalData                                    */
/*==============================================================*/
create table L_LotteryFinalData (
   Id                   varchar(36)          not null,
   LotteryId            varchar(36)          null,
   FinalPeriod          int                  not null,
   PlanState            int                  not null,
   Data                 varchar(200)         not null,
   LotteryTime          datetime             not null default getdate(),
   UpdateTime           datetime             not null default getdate(),
   constraint PK_L_LOTTERYFINALDATA primary key (Id)
)
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_LotteryFinalData')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_LotteryFinalData', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键Id',
   'user', @CurrentUser, 'table', 'L_LotteryFinalData', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_LotteryFinalData')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LotteryId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_LotteryFinalData', 'column', 'LotteryId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '彩种Id',
   'user', @CurrentUser, 'table', 'L_LotteryFinalData', 'column', 'LotteryId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_LotteryFinalData')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FinalPeriod')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_LotteryFinalData', 'column', 'FinalPeriod'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '期数',
   'user', @CurrentUser, 'table', 'L_LotteryFinalData', 'column', 'FinalPeriod'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_LotteryFinalData')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PlanState')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_LotteryFinalData', 'column', 'PlanState'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '计划追号状态',
   'user', @CurrentUser, 'table', 'L_LotteryFinalData', 'column', 'PlanState'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_LotteryFinalData')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Data')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_LotteryFinalData', 'column', 'Data'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '开奖数据',
   'user', @CurrentUser, 'table', 'L_LotteryFinalData', 'column', 'Data'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_LotteryFinalData')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LotteryTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_LotteryFinalData', 'column', 'LotteryTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '开奖时间',
   'user', @CurrentUser, 'table', 'L_LotteryFinalData', 'column', 'LotteryTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_LotteryFinalData')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_LotteryFinalData', 'column', 'UpdateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'L_LotteryFinalData', 'column', 'UpdateTime'
go

/*==============================================================*/
/* Table: L_LotteryInfo                                         */
/*==============================================================*/
create table L_LotteryInfo (
   Id                   varchar(36)          not null,
   LotteryCode          varchar(12)          null,
   Name                 varchar(36)          null,
   TableStrategy        int                  null default 1,
   IsCompleteDynamicTable bit                  null,
   CreateBy             varchar(36)          null,
   CreateTime           datetime             null,
   constraint PK_L_LOTTERYINFO primary key (Id)
)
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_LotteryInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_LotteryInfo', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键Id',
   'user', @CurrentUser, 'table', 'L_LotteryInfo', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_LotteryInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LotteryCode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_LotteryInfo', 'column', 'LotteryCode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '彩种编码',
   'user', @CurrentUser, 'table', 'L_LotteryInfo', 'column', 'LotteryCode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_LotteryInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Name')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_LotteryInfo', 'column', 'Name'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '彩种名称',
   'user', @CurrentUser, 'table', 'L_LotteryInfo', 'column', 'Name'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_LotteryInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TableStrategy')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_LotteryInfo', 'column', 'TableStrategy'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '分表策略;1.按月;2.按季度;3.按年',
   'user', @CurrentUser, 'table', 'L_LotteryInfo', 'column', 'TableStrategy'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_LotteryInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsCompleteDynamicTable')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_LotteryInfo', 'column', 'IsCompleteDynamicTable'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否完成动态分表的配置',
   'user', @CurrentUser, 'table', 'L_LotteryInfo', 'column', 'IsCompleteDynamicTable'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_LotteryInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreateBy')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_LotteryInfo', 'column', 'CreateBy'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人',
   'user', @CurrentUser, 'table', 'L_LotteryInfo', 'column', 'CreateBy'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_LotteryInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_LotteryInfo', 'column', 'CreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '日期',
   'user', @CurrentUser, 'table', 'L_LotteryInfo', 'column', 'CreateTime'
go

/*==============================================================*/
/* Index: Index_1                                               */
/*==============================================================*/
create unique index Index_1 on L_LotteryInfo (
LotteryCode ASC
)
go

/*==============================================================*/
/* Table: L_NormGroup                                           */
/*==============================================================*/
create table L_NormGroup (
   Id                   varchar(36)          not null,
   LotteryId            varchar(36)          not null,
   GroupCode            varchar(36)          null,
   GroupName            varchar(36)          null,
   Sort                 int                  null,  
   constraint PK_L_NORMGROUP primary key (Id)
)
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_NormGroup')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_NormGroup', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键Id',
   'user', @CurrentUser, 'table', 'L_NormGroup', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_NormGroup')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LotteryId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_NormGroup', 'column', 'LotteryId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '彩种Id',
   'user', @CurrentUser, 'table', 'L_NormGroup', 'column', 'LotteryId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_NormGroup')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'GroupCode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_NormGroup', 'column', 'GroupCode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '计划分组编码',
   'user', @CurrentUser, 'table', 'L_NormGroup', 'column', 'GroupCode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_NormGroup')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'GroupName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_NormGroup', 'column', 'GroupName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '组名',
   'user', @CurrentUser, 'table', 'L_NormGroup', 'column', 'GroupName'
go

/*==============================================================*/
/* Table: L_PlanInfo                                            */
/*==============================================================*/
create table L_PlanInfo (
   Id                   varchar(36)          not null,
   NormGroupId          varchar(36)          null,
   PlanCode             varchar(18)          null,
   PlanNormTable        varchar(200)         null,
   PlanName             varchar(200)         null,
   PredictCode          varchar(36)          null,
   DsType               bit                  not null,
   Sort                 int                  null,
   constraint PK_L_PLANINFO primary key (Id)
)
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_PlanInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_PlanInfo', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键Id',
   'user', @CurrentUser, 'table', 'L_PlanInfo', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_PlanInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'NormGroupId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_PlanInfo', 'column', 'NormGroupId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '所属组',
   'user', @CurrentUser, 'table', 'L_PlanInfo', 'column', 'NormGroupId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_PlanInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PlanCode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_PlanInfo', 'column', 'PlanCode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '计划编码',
   'user', @CurrentUser, 'table', 'L_PlanInfo', 'column', 'PlanCode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_PlanInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PlanNormTable')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_PlanInfo', 'column', 'PlanNormTable'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '计划追号指标分表名称',
   'user', @CurrentUser, 'table', 'L_PlanInfo', 'column', 'PlanNormTable'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_PlanInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PlanName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_PlanInfo', 'column', 'PlanName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '计划名称',
   'user', @CurrentUser, 'table', 'L_PlanInfo', 'column', 'PlanName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_PlanInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PredictCode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_PlanInfo', 'column', 'PredictCode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '预测类型',
   'user', @CurrentUser, 'table', 'L_PlanInfo', 'column', 'PredictCode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_PlanInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DsType')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_PlanInfo', 'column', 'DsType'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '0.杀码；1.定码',
   'user', @CurrentUser, 'table', 'L_PlanInfo', 'column', 'DsType'
go

/*==============================================================*/
/* Table: L_PlanKeyNumber                                       */
/*==============================================================*/
create table L_PlanKeyNumber (
   Id                   varchar(36)          not null,
   PlanInfoId           varchar(36)          null,
   PositionId           varchar(36)          null default '0',
   constraint PK_L_PLANKEYNUMBER primary key (Id)
)
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_PlanKeyNumber')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_PlanKeyNumber', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键Id',
   'user', @CurrentUser, 'table', 'L_PlanKeyNumber', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_PlanKeyNumber')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PlanInfoId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_PlanKeyNumber', 'column', 'PlanInfoId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '计划Id',
   'user', @CurrentUser, 'table', 'L_PlanKeyNumber', 'column', 'PlanInfoId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_PlanKeyNumber')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PositionId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_PlanKeyNumber', 'column', 'PositionId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '彩票位置Id',
   'user', @CurrentUser, 'table', 'L_PlanKeyNumber', 'column', 'PositionId'
go

/*==============================================================*/
/* Table: L_PositionInfo                                        */
/*==============================================================*/
create table L_PositionInfo (
   Id                   varchar(36)          not null,
   LotteryId            varchar(36)          null,
   Name                 varchar(12)          null,
   PositionType         varchar(18)          null,
   Position             int                  not null,
   MaxValue             int                  not null,
   MinxValue            int                  not null,
   constraint PK_L_POSITIONINFO primary key (Id)
)
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_PositionInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_PositionInfo', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键Id',
   'user', @CurrentUser, 'table', 'L_PositionInfo', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_PositionInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LotteryId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_PositionInfo', 'column', 'LotteryId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '彩种Id',
   'user', @CurrentUser, 'table', 'L_PositionInfo', 'column', 'LotteryId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_PositionInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Name')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_PositionInfo', 'column', 'Name'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '位置名称',
   'user', @CurrentUser, 'table', 'L_PositionInfo', 'column', 'Name'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_PositionInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PositionType')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_PositionInfo', 'column', 'PositionType'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '位置类型',
   'user', @CurrentUser, 'table', 'L_PositionInfo', 'column', 'PositionType'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_PositionInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Position')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_PositionInfo', 'column', 'Position'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '位置编码',
   'user', @CurrentUser, 'table', 'L_PositionInfo', 'column', 'Position'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_PositionInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'MaxValue')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_PositionInfo', 'column', 'MaxValue'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '允许的最大值',
   'user', @CurrentUser, 'table', 'L_PositionInfo', 'column', 'MaxValue'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_PositionInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'MinxValue')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_PositionInfo', 'column', 'MinxValue'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '允许的最小值',
   'user', @CurrentUser, 'table', 'L_PositionInfo', 'column', 'MinxValue'
go

/*==============================================================*/
/* Table: L_SubLotteryPredictTableMap                           */
/*==============================================================*/
create table L_SubLotteryPredictTableMap (
   Id                   varchar(36)          not null,
   PlanId               varchar(36)          null,
   PredictDataTable     varchar(200)         null,
   PredictDataTableId   varchar(36)          null,
   CreateBy             varchar(36)          null default 'system',
   CreateTime           varchar(36)          null default getdate(),
   constraint PK_L_SUBLOTTERYPREDICTTABLEMAP primary key (Id)
)
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_SubLotteryPredictTableMap')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_SubLotteryPredictTableMap', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键Id',
   'user', @CurrentUser, 'table', 'L_SubLotteryPredictTableMap', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_SubLotteryPredictTableMap')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PredictDataTable')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_SubLotteryPredictTableMap', 'column', 'PredictDataTable'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '计划追号结果存储表名称',
   'user', @CurrentUser, 'table', 'L_SubLotteryPredictTableMap', 'column', 'PredictDataTable'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_SubLotteryPredictTableMap')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PredictDataTableId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_SubLotteryPredictTableMap', 'column', 'PredictDataTableId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '上一个计划追号存储表Id',
   'user', @CurrentUser, 'table', 'L_SubLotteryPredictTableMap', 'column', 'PredictDataTableId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_SubLotteryPredictTableMap')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreateBy')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_SubLotteryPredictTableMap', 'column', 'CreateBy'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人',
   'user', @CurrentUser, 'table', 'L_SubLotteryPredictTableMap', 'column', 'CreateBy'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_SubLotteryPredictTableMap')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_SubLotteryPredictTableMap', 'column', 'CreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建日期',
   'user', @CurrentUser, 'table', 'L_SubLotteryPredictTableMap', 'column', 'CreateTime'
go

/*==============================================================*/
/* Table: L_TimeRule                                            */
/*==============================================================*/
create table L_TimeRule (
   Id                   varchar(36)          not null,
   LotteryId            varchar(36)          null,
   Weekday              varchar(36)          null,
   StartTime            datetime             null,
   EndTime              datetime             null,
   Tick                 time                 null,
   CreateBy             varchar(36)         null,
   CreateTime           datetime             null default getdate(),
   constraint PK_L_TIMERULE primary key (Id)
)
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_TimeRule')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LotteryId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_TimeRule', 'column', 'LotteryId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '彩种Id',
   'user', @CurrentUser, 'table', 'L_TimeRule', 'column', 'LotteryId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_TimeRule')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Weekday')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_TimeRule', 'column', 'Weekday'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '星期',
   'user', @CurrentUser, 'table', 'L_TimeRule', 'column', 'Weekday'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_TimeRule')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'StartTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_TimeRule', 'column', 'StartTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '开奖开始时间',
   'user', @CurrentUser, 'table', 'L_TimeRule', 'column', 'StartTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_TimeRule')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'EndTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_TimeRule', 'column', 'EndTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '开奖结束时间',
   'user', @CurrentUser, 'table', 'L_TimeRule', 'column', 'EndTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_TimeRule')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Tick')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_TimeRule', 'column', 'Tick'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '时间间隔',
   'user', @CurrentUser, 'table', 'L_TimeRule', 'column', 'Tick'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_TimeRule')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreateBy')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_TimeRule', 'column', 'CreateBy'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人',
   'user', @CurrentUser, 'table', 'L_TimeRule', 'column', 'CreateBy'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('L_TimeRule')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'L_TimeRule', 'column', 'CreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建日期',
   'user', @CurrentUser, 'table', 'L_TimeRule', 'column', 'CreateTime'
go

alter table F_RolePower
   add constraint FK_F_ROLEPO_REFERENCE_F_ROLE foreign key (RoleId)
      references F_Role (Id)
go

alter table F_RolePower
   add constraint FK_F_ROLEPO_REFERENCE_F_POWER foreign key (PowerId)
      references F_Power (Id)
go

alter table F_UserRole
   add constraint FK_F_USERRO_REFERENCE_F_ROLE foreign key (RoleId)
      references F_Role (Id)
go

alter table F_UserRole
   add constraint FK_F_USERRO_REFERENCE_F_USERIN foreign key (UserId)
      references F_UserInfo (Id)
go

alter table L_NormGroup
   add constraint FK_L_NORMGR_REFERENCE_L_LOTTER foreign key (LotteryId)
      references L_LotteryInfo (Id)
go

alter table L_PlanInfo
   add constraint FK_L_PLANIN_REFERENCE_L_NORMGR foreign key (NormGroupId)
      references L_NormGroup (Id)
go

alter table L_PlanKeyNumber
   add constraint FK_L_PLANKE_REFERENCE_L_PLANIN foreign key (PlanInfoId)
      references L_PlanInfo (Id)
go

alter table L_PlanKeyNumber
   add constraint FK_L_PLANKE_REFERENCE_L_POSITI foreign key (PositionId)
      references L_PositionInfo (Id)
go

alter table L_PositionInfo
   add constraint FK_L_POSITI_REFERENCE_L_LOTTER foreign key (LotteryId)
      references L_LotteryInfo (Id)
go

alter table L_TimeRule
   add constraint FK_L_TIMERU_REFERENCE_L_LOTTER foreign key (LotteryId)
      references L_LotteryInfo (Id)
go

