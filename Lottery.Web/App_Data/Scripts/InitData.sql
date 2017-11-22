﻿INSERT INTO [dbo].[L_LotteryInfo]([Id],[LotteryCode],[Name],[TableStrategy],[IsCompleteDynamicTable],[CreateBy],[CreateTime])
VALUES('ACB89F4E-7C71-4785-BA09-D7E73084B467','BJPKS','北京PK10',1,0,'System',GETDATE())
GO


INSERT INTO [dbo].[L_TimeRule]([Id],[LotteryId],[Weekday],[StartTime],[EndTime],[Tick],[CreateBy],[CreateTime])VALUES
(NEWID(),'ACB89F4E-7C71-4785-BA09-D7E73084B467','0','09:07:00','23:57:00','00:05:00','system' ,GETDATE()),
(NEWID(),'ACB89F4E-7C71-4785-BA09-D7E73084B467','1','09:07:00','23:57:00','00:05:00','system' ,GETDATE()),
(NEWID(),'ACB89F4E-7C71-4785-BA09-D7E73084B467','2','09:07:00','23:57:00','00:05:00','system' ,GETDATE()),
(NEWID(),'ACB89F4E-7C71-4785-BA09-D7E73084B467','3','09:07:00','23:57:00','00:05:00','system' ,GETDATE()),
(NEWID(),'ACB89F4E-7C71-4785-BA09-D7E73084B467','4','09:07:00','23:57:00','00:05:00','system' ,GETDATE()),
(NEWID(),'ACB89F4E-7C71-4785-BA09-D7E73084B467','5','09:07:00','23:57:00','00:05:00','system' ,GETDATE()),
(NEWID(),'ACB89F4E-7C71-4785-BA09-D7E73084B467','6','09:07:00','23:57:00','00:05:00','system' ,GETDATE())
GO

INSERT INTO [dbo].[L_PositionInfo]([Id],[LotteryId],[Name],[PositionType],[Position],[MaxValue],[MinxValue])VALUES
('DA8DAADD-52B8-4F2D-9B9D-D98373900001','ACB89F4E-7C71-4785-BA09-D7E73084B467','冠军','1',1,1,10),
('DA8DAADD-52B8-4F2D-9B9D-D98373900002','ACB89F4E-7C71-4785-BA09-D7E73084B467','亚军','1',2,1,10),
('DA8DAADD-52B8-4F2D-9B9D-D98373900003','ACB89F4E-7C71-4785-BA09-D7E73084B467','季军','1',3,1,10),
('DA8DAADD-52B8-4F2D-9B9D-D98373900004','ACB89F4E-7C71-4785-BA09-D7E73084B467','第四名','1',4,1,10),
('DA8DAADD-52B8-4F2D-9B9D-D98373900005','ACB89F4E-7C71-4785-BA09-D7E73084B467','第五名','1',5,1,10),
('DA8DAADD-52B8-4F2D-9B9D-D98373900006','ACB89F4E-7C71-4785-BA09-D7E73084B467','第六名','1',6,1,10),
('DA8DAADD-52B8-4F2D-9B9D-D98373900007','ACB89F4E-7C71-4785-BA09-D7E73084B467','第七名','1',7,1,10),
('DA8DAADD-52B8-4F2D-9B9D-D98373900008','ACB89F4E-7C71-4785-BA09-D7E73084B467','第八名','1',8,1,10),
('DA8DAADD-52B8-4F2D-9B9D-D98373900009','ACB89F4E-7C71-4785-BA09-D7E73084B467','第九名','1',9,1,10),
('DA8DAADD-52B8-4F2D-9B9D-D98373900010','ACB89F4E-7C71-4785-BA09-D7E73084B467','第十名','1',10,1,10)
GO

INSERT INTO [dbo].[L_NormGroup]([Id],[LotteryId],[GroupCode],[GroupName],[Sort])VALUES
('ACCAAD93-2DF3-40FA-9B2D-42164EF00001','ACB89F4E-7C71-4785-BA09-D7E73084B467','DWXL' ,'定位系列',1),
('ACCAAD93-2DF3-40FA-9B2D-42164EF00002','ACB89F4E-7C71-4785-BA09-D7E73084B467','DXXL' ,'大小系列',2),
('ACCAAD93-2DF3-40FA-9B2D-42164EF00003','ACB89F4E-7C71-4785-BA09-D7E73084B467','DSXL' ,'单双系列',3),
('ACCAAD93-2DF3-40FA-9B2D-42164EF00004','ACB89F4E-7C71-4785-BA09-D7E73084B467','BDWXL' ,'不定位系列',4),
('ACCAAD93-2DF3-40FA-9B2D-42164EF00005','ACB89F4E-7C71-4785-BA09-D7E73084B467','MCXL' ,'名次系列',5),
('ACCAAD93-2DF3-40FA-9B2D-42164EF00006','ACB89F4E-7C71-4785-BA09-D7E73084B467','LHXL' ,'龙虎系列',6)
GO


INSERT INTO [dbo].[L_PlanInfo]([Id],[NormGroupId],[PlanCode],[PlanNormTable],[PredictCode],[PlanName],[DsType],[Sort])VALUES
('83FF7434-C88E-45CD-A39F-73B3EB500001','ACCAAD93-2DF3-40FA-9B2D-42164EF00001','GJDM','LA_LotteryPredictData_GJDM','NUM','冠军定码',1,1),
('83FF7434-C88E-45CD-A39F-73B3EB500002','ACCAAD93-2DF3-40FA-9B2D-42164EF00001','YJDM','LA_LotteryPredictData_YJDM','NUM','亚军定码',1,2),
('83FF7434-C88E-45CD-A39F-73B3EB500003','ACCAAD93-2DF3-40FA-9B2D-42164EF00001','JJDM','LA_LotteryPredictData_JJDM','NUM','季军定码',1,3),
('83FF7434-C88E-45CD-A39F-73B3EB500004','ACCAAD93-2DF3-40FA-9B2D-42164EF00001','DSIMDM','LA_LotteryPredictData_DSIMDM','NUM','第4名定码',1,4),
('83FF7434-C88E-45CD-A39F-73B3EB500005','ACCAAD93-2DF3-40FA-9B2D-42164EF00001','DWUMDM','LA_LotteryPredictData_DWUMDM','NUM','第5名定码',1,5),
('83FF7434-C88E-45CD-A39F-73B3EB500006','ACCAAD93-2DF3-40FA-9B2D-42164EF00001','DLIUMDM','LA_LotteryPredictData_DLIUMDM','NUM','第6名定码',1,6),
('83FF7434-C88E-45CD-A39F-73B3EB500007','ACCAAD93-2DF3-40FA-9B2D-42164EF00001','DQIMDM','LA_LotteryPredictData_DQIMDM','NUM','第7名定码',1,7),
('83FF7434-C88E-45CD-A39F-73B3EB500008','ACCAAD93-2DF3-40FA-9B2D-42164EF00001','DBAMDM','LA_LotteryPredictData_DBAMDM','NUM','第8名定码',1,8),
('83FF7434-C88E-45CD-A39F-73B3EB500009','ACCAAD93-2DF3-40FA-9B2D-42164EF00001','DJIUMDM','LA_LotteryPredictData_DJIUMDM','NUM','第9名定码',1,9),
('83FF7434-C88E-45CD-A39F-73B3EB500010','ACCAAD93-2DF3-40FA-9B2D-42164EF00001','GSHIMDM','LA_LotteryPredictData_GSHIMDM','NUM','第10名定码',1,10),

('83FF7434-C88E-45CD-A39F-73B3EB500011','ACCAAD93-2DF3-40FA-9B2D-42164EF00001','GJSM','LA_LotteryPredictData_GJSM','NUM','冠军杀码',0,11),
('83FF7434-C88E-45CD-A39F-73B3EB500012','ACCAAD93-2DF3-40FA-9B2D-42164EF00001','YJSM','LA_LotteryPredictData_YJSM','NUM','亚军杀码',0,12),
('83FF7434-C88E-45CD-A39F-73B3EB500013','ACCAAD93-2DF3-40FA-9B2D-42164EF00001','JJSM','LA_LotteryPredictData_JJSM','NUM','季军杀码',0,13),
('83FF7434-C88E-45CD-A39F-73B3EB500014','ACCAAD93-2DF3-40FA-9B2D-42164EF00001','DSIMSM','LA_LotteryPredictData_DSIMSM','NUM','第4名杀码',0,14),
('83FF7434-C88E-45CD-A39F-73B3EB500015','ACCAAD93-2DF3-40FA-9B2D-42164EF00001','DWUMSM','LA_LotteryPredictData_DWUMSM','NUM','第5名杀码',0,15),
('83FF7434-C88E-45CD-A39F-73B3EB500016','ACCAAD93-2DF3-40FA-9B2D-42164EF00001','DLIUMSM','LA_LotteryPredictData_DLIUMSM','NUM','第6名杀码',0,16),
('83FF7434-C88E-45CD-A39F-73B3EB500017','ACCAAD93-2DF3-40FA-9B2D-42164EF00001','DQIMSM','LA_LotteryPredictData_DQIMSM','NUM','第7名杀码',0,17),
('83FF7434-C88E-45CD-A39F-73B3EB500018','ACCAAD93-2DF3-40FA-9B2D-42164EF00001','DBAMSM','LA_LotteryPredictData_DBAMSM','NUM','第8名杀码',0,18),
('83FF7434-C88E-45CD-A39F-73B3EB500019','ACCAAD93-2DF3-40FA-9B2D-42164EF00001','DJIUMSM','LA_LotteryPredictData_DJIUMSM','NUM','第9名杀码',0,19),
('83FF7434-C88E-45CD-A39F-73B3EB500020','ACCAAD93-2DF3-40FA-9B2D-42164EF00001','GSHIMSM','LA_LotteryPredictData_GSHIMSM','NUM','第10名杀码',0,20),

('83FF7434-C88E-45CD-A39F-73B3EB500021','ACCAAD93-2DF3-40FA-9B2D-42164EF00002','GJDX','LA_LotteryPredictData_GJDX','SIZE','冠军大小',1,21),
('83FF7434-C88E-45CD-A39F-73B3EB500022','ACCAAD93-2DF3-40FA-9B2D-42164EF00002','YJDX','LA_LotteryPredictData_YJDX','SIZE','亚军大小',1,22),
('83FF7434-C88E-45CD-A39F-73B3EB500023','ACCAAD93-2DF3-40FA-9B2D-42164EF00002','JJDX','LA_LotteryPredictData_JJDX','SIZE','季军大小',1,23),
('83FF7434-C88E-45CD-A39F-73B3EB500024','ACCAAD93-2DF3-40FA-9B2D-42164EF00002','DSIDX','LA_LotteryPredictData_DSIDX','SIZE','第4名大小',1,24),
('83FF7434-C88E-45CD-A39F-73B3EB500025','ACCAAD93-2DF3-40FA-9B2D-42164EF00002','DWUDX','LA_LotteryPredictData_DWUDX','SIZE','第5名大小',1,25),
('83FF7434-C88E-45CD-A39F-73B3EB500026','ACCAAD93-2DF3-40FA-9B2D-42164EF00002','DLIUDX','LA_LotteryPredictData_DLIUDX','SIZE','第6名大小',1,26),
('83FF7434-C88E-45CD-A39F-73B3EB500027','ACCAAD93-2DF3-40FA-9B2D-42164EF00002','DQIDX','LA_LotteryPredictData_DQIDX','SIZE','第7名大小',1,27),
('83FF7434-C88E-45CD-A39F-73B3EB500028','ACCAAD93-2DF3-40FA-9B2D-42164EF00002','DBADX','LA_LotteryPredictData_DBADX','SIZE','第8名大小',1,28),
('83FF7434-C88E-45CD-A39F-73B3EB500029','ACCAAD93-2DF3-40FA-9B2D-42164EF00002','DJIUDX','LA_LotteryPredictData_DJIUDX','SIZE','第9名大小',1,29),
('83FF7434-C88E-45CD-A39F-73B3EB500030','ACCAAD93-2DF3-40FA-9B2D-42164EF00002','DSHIDX','LA_LotteryPredictData_DSHIDX','SIZE','第10名大小',1,30),
('83FF7434-C88E-45CD-A39F-73B3EB500031','ACCAAD93-2DF3-40FA-9B2D-42164EF00002','DSHIDX','LA_LotteryPredictData_DSHIDX','SIZE','冠亚和值大小',1,31),

('83FF7434-C88E-45CD-A39F-73B3EB500032','ACCAAD93-2DF3-40FA-9B2D-42164EF00003','GJDS','LA_LotteryPredictData_GJDS','SHAPE','冠军单双',1,32),
('83FF7434-C88E-45CD-A39F-73B3EB500033','ACCAAD93-2DF3-40FA-9B2D-42164EF00003','YJDS','LA_LotteryPredictData_YJDS','SHAPE','亚军单双',1,33),
('83FF7434-C88E-45CD-A39F-73B3EB500034','ACCAAD93-2DF3-40FA-9B2D-42164EF00003','JJDS','LA_LotteryPredictData_JJDS','SHAPE','季军单双',1,34),
('83FF7434-C88E-45CD-A39F-73B3EB500035','ACCAAD93-2DF3-40FA-9B2D-42164EF00003','DSIDS','LA_LotteryPredictData_DSIDS','SHAPE','第4名单双',1,35),
('83FF7434-C88E-45CD-A39F-73B3EB500036','ACCAAD93-2DF3-40FA-9B2D-42164EF00003','DWUDS','LA_LotteryPredictData_DWUDS','SHAPE','第5名单双',1,36),
('83FF7434-C88E-45CD-A39F-73B3EB500037','ACCAAD93-2DF3-40FA-9B2D-42164EF00003','DLIUDS','LA_LotteryPredictData_DLIUDS','SHAPE','第6名单双',1,37),
('83FF7434-C88E-45CD-A39F-73B3EB500038','ACCAAD93-2DF3-40FA-9B2D-42164EF00003','DQIDS','LA_LotteryPredictData_DQIDS','SHAPE','第7名单双',1,38),
('83FF7434-C88E-45CD-A39F-73B3EB500039','ACCAAD93-2DF3-40FA-9B2D-42164EF00003','DBADS','LA_LotteryPredictData_DBADS','SHAPE','第8名单双',1,39),
('83FF7434-C88E-45CD-A39F-73B3EB500040','ACCAAD93-2DF3-40FA-9B2D-42164EF00003','DJIUDS','LA_LotteryPredictData_DJIUDS','SHAPE','第9名单双',1,40),
('83FF7434-C88E-45CD-A39F-73B3EB500041','ACCAAD93-2DF3-40FA-9B2D-42164EF00003','DSHIDS','LA_LotteryPredictData_DSHIDS','SHAPE','第10名单双',1,41),
('83FF7434-C88E-45CD-A39F-73B3EB500042','ACCAAD93-2DF3-40FA-9B2D-42164EF00003','DSHIDS','LA_LotteryPredictData_DSHIDS','SHAPE','冠亚和值单双',1,42),

('83FF7434-C88E-45CD-A39F-73B3EB500043','ACCAAD93-2DF3-40FA-9B2D-42164EF00004','QERBDW','LA_LotteryPredictData_QERBDW','NUM','前二不定位定码',1,43),
('83FF7434-C88E-45CD-A39F-73B3EB500044','ACCAAD93-2DF3-40FA-9B2D-42164EF00004','QSANBDW','LA_LotteryPredictData_QSANBDW','NUM','前三不定位定码',1,44),
('83FF7434-C88E-45CD-A39F-73B3EB500045','ACCAAD93-2DF3-40FA-9B2D-42164EF00004','QSIBDW','LA_LotteryPredictData_QSIBDW','NUM','前四不定位定码',1,45),

('83FF7434-C88E-45CD-A39F-73B3EB500046','ACCAAD93-2DF3-40FA-9B2D-42164EF00004','HERBDW','LA_LotteryPredictData_HERBDW','NUM','后二不定位定码',1,46),
('83FF7434-C88E-45CD-A39F-73B3EB500047','ACCAAD93-2DF3-40FA-9B2D-42164EF00004','HSANBDW','LA_LotteryPredictData_HSANBDW','NUM','后三不定位定码',1,47),
('83FF7434-C88E-45CD-A39F-73B3EB500048','ACCAAD93-2DF3-40FA-9B2D-42164EF00004','HSIBDW','LA_LotteryPredictData_HSIBDW','NUM','后四不定位定码',1,48),

('83FF7434-C88E-45CD-A39F-73B3EB500049','ACCAAD93-2DF3-40FA-9B2D-42164EF00004','QERBSW','LA_LotteryPredictData_QERBSW','NUM','前二不定位杀码',0,49),
('83FF7434-C88E-45CD-A39F-73B3EB500050','ACCAAD93-2DF3-40FA-9B2D-42164EF00004','QSANBSW','LA_LotteryPredictData_QSANBSW','NUM','前三不定位杀码',0,50),
('83FF7434-C88E-45CD-A39F-73B3EB500051','ACCAAD93-2DF3-40FA-9B2D-42164EF00004','QSIBSW','LA_LotteryPredictData_QSIBSW','NUM','前四不定位杀码',0,51),

('83FF7434-C88E-45CD-A39F-73B3EB500052','ACCAAD93-2DF3-40FA-9B2D-42164EF00004','HERBSW','LA_LotteryPredictData_HERBSW','NUM','后二不定位杀码',0,52),
('83FF7434-C88E-45CD-A39F-73B3EB500053','ACCAAD93-2DF3-40FA-9B2D-42164EF00004','HSANBSW','LA_LotteryPredictData_HSANBSW','NUM','后三不定位杀码',0,53),
('83FF7434-C88E-45CD-A39F-73B3EB500054','ACCAAD93-2DF3-40FA-9B2D-42164EF00004','HSIBSW','LA_LotteryPredictData_HSIBSW','NUM','后四不定位杀码',0,54),

('83FF7434-C88E-45CD-A39F-73B3EB500055','ACCAAD93-2DF3-40FA-9B2D-42164EF00005','YIMC','LA_LotteryPredictData_YIMC','RANK','一号车名次',1,55),
('83FF7434-C88E-45CD-A39F-73B3EB500056','ACCAAD93-2DF3-40FA-9B2D-42164EF00005','ERMC','LA_LotteryPredictData_ERMC','RANK','二号车名次',1,56),
('83FF7434-C88E-45CD-A39F-73B3EB500057','ACCAAD93-2DF3-40FA-9B2D-42164EF00005','SANMC','LA_LotteryPredictData_SANMC','RANK','三号车名次',1,57),
('83FF7434-C88E-45CD-A39F-73B3EB500058','ACCAAD93-2DF3-40FA-9B2D-42164EF00005','SIMC','LA_LotteryPredictData_SIMC','RANK','四号车名次',1,58),
('83FF7434-C88E-45CD-A39F-73B3EB500059','ACCAAD93-2DF3-40FA-9B2D-42164EF00005','WUMC','LA_LotteryPredictData_WUMC','RANK','五号车名次',1,59),
('83FF7434-C88E-45CD-A39F-73B3EB500060','ACCAAD93-2DF3-40FA-9B2D-42164EF00005','LIUMC','LA_LotteryPredictData_LIUMC','RANK','六号车名次',1,60),
('83FF7434-C88E-45CD-A39F-73B3EB500061','ACCAAD93-2DF3-40FA-9B2D-42164EF00005','QIMC','LA_LotteryPredictData_QIMC','RANK','七号车名次',1,61),
('83FF7434-C88E-45CD-A39F-73B3EB500062','ACCAAD93-2DF3-40FA-9B2D-42164EF00005','BAMC','LA_LotteryPredictData_BAMC','RANK','八号车名次',1,62),
('83FF7434-C88E-45CD-A39F-73B3EB500063','ACCAAD93-2DF3-40FA-9B2D-42164EF00005','JIUMC','LA_LotteryPredictData_JIUMC','RANK','九号车名次',1,63),
('83FF7434-C88E-45CD-A39F-73B3EB500064','ACCAAD93-2DF3-40FA-9B2D-42164EF00005','SHIMC','LA_LotteryPredictData_SHIMC','RANK','十号车名次',1,64),

('83FF7434-C88E-45CD-A39F-73B3EB500065','ACCAAD93-2DF3-40FA-9B2D-42164EF00006','GJLH','LA_LotteryPredictData_GJLH','LH','冠军龙虎',1,65),
('83FF7434-C88E-45CD-A39F-73B3EB500066','ACCAAD93-2DF3-40FA-9B2D-42164EF00006','YJLH','LA_LotteryPredictData_YJLH','LH','亚军龙虎',1,66),
('83FF7434-C88E-45CD-A39F-73B3EB500067','ACCAAD93-2DF3-40FA-9B2D-42164EF00006','JJLH','LA_LotteryPredictData_JJLH','LH','季军龙虎',1,67),
('83FF7434-C88E-45CD-A39F-73B3EB500068','ACCAAD93-2DF3-40FA-9B2D-42164EF00006','SILH','LA_LotteryPredictData_SILH','LH','第四名龙虎',1,68),
('83FF7434-C88E-45CD-A39F-73B3EB500069','ACCAAD93-2DF3-40FA-9B2D-42164EF00006','WULH','LA_LotteryPredictData_WULH','LH','第五名龙虎',1,69)


GO

INSERT INTO [dbo].[L_PlanKeyNumber]([Id],[PlanInfoId],[PositionId])VALUES
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500001','DA8DAADD-52B8-4F2D-9B9D-D98373900001'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500002','DA8DAADD-52B8-4F2D-9B9D-D98373900002'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500003','DA8DAADD-52B8-4F2D-9B9D-D98373900003'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500004','DA8DAADD-52B8-4F2D-9B9D-D98373900004'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500005','DA8DAADD-52B8-4F2D-9B9D-D98373900005'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500006','DA8DAADD-52B8-4F2D-9B9D-D98373900006'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500007','DA8DAADD-52B8-4F2D-9B9D-D98373900007'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500008','DA8DAADD-52B8-4F2D-9B9D-D98373900008'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500009','DA8DAADD-52B8-4F2D-9B9D-D98373900009'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500010','DA8DAADD-52B8-4F2D-9B9D-D98373900010'),

(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500011','DA8DAADD-52B8-4F2D-9B9D-D98373900001'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500012','DA8DAADD-52B8-4F2D-9B9D-D98373900002'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500013','DA8DAADD-52B8-4F2D-9B9D-D98373900003'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500014','DA8DAADD-52B8-4F2D-9B9D-D98373900004'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500015','DA8DAADD-52B8-4F2D-9B9D-D98373900005'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500016','DA8DAADD-52B8-4F2D-9B9D-D98373900006'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500017','DA8DAADD-52B8-4F2D-9B9D-D98373900007'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500018','DA8DAADD-52B8-4F2D-9B9D-D98373900008'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500019','DA8DAADD-52B8-4F2D-9B9D-D98373900009'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500020','DA8DAADD-52B8-4F2D-9B9D-D98373900010'),

(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500021','DA8DAADD-52B8-4F2D-9B9D-D98373900001'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500022','DA8DAADD-52B8-4F2D-9B9D-D98373900002'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500023','DA8DAADD-52B8-4F2D-9B9D-D98373900003'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500024','DA8DAADD-52B8-4F2D-9B9D-D98373900004'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500025','DA8DAADD-52B8-4F2D-9B9D-D98373900005'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500026','DA8DAADD-52B8-4F2D-9B9D-D98373900006'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500027','DA8DAADD-52B8-4F2D-9B9D-D98373900007'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500028','DA8DAADD-52B8-4F2D-9B9D-D98373900008'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500029','DA8DAADD-52B8-4F2D-9B9D-D98373900009'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500030','DA8DAADD-52B8-4F2D-9B9D-D98373900010'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500031','DA8DAADD-52B8-4F2D-9B9D-D98373900001'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500031','DA8DAADD-52B8-4F2D-9B9D-D98373900002'),

(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500032','DA8DAADD-52B8-4F2D-9B9D-D98373900001'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500033','DA8DAADD-52B8-4F2D-9B9D-D98373900002'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500034','DA8DAADD-52B8-4F2D-9B9D-D98373900003'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500035','DA8DAADD-52B8-4F2D-9B9D-D98373900004'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500036','DA8DAADD-52B8-4F2D-9B9D-D98373900005'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500037','DA8DAADD-52B8-4F2D-9B9D-D98373900006'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500038','DA8DAADD-52B8-4F2D-9B9D-D98373900007'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500039','DA8DAADD-52B8-4F2D-9B9D-D98373900008'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500040','DA8DAADD-52B8-4F2D-9B9D-D98373900009'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500041','DA8DAADD-52B8-4F2D-9B9D-D98373900010'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500042','DA8DAADD-52B8-4F2D-9B9D-D98373900001'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500042','DA8DAADD-52B8-4F2D-9B9D-D98373900002'),

(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500043','DA8DAADD-52B8-4F2D-9B9D-D98373900001'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500043','DA8DAADD-52B8-4F2D-9B9D-D98373900002'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500044','DA8DAADD-52B8-4F2D-9B9D-D98373900001'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500044','DA8DAADD-52B8-4F2D-9B9D-D98373900002'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500044','DA8DAADD-52B8-4F2D-9B9D-D98373900003'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500045','DA8DAADD-52B8-4F2D-9B9D-D98373900001'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500045','DA8DAADD-52B8-4F2D-9B9D-D98373900002'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500045','DA8DAADD-52B8-4F2D-9B9D-D98373900003'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500045','DA8DAADD-52B8-4F2D-9B9D-D98373900004'),

(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500046','DA8DAADD-52B8-4F2D-9B9D-D98373900009'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500046','DA8DAADD-52B8-4F2D-9B9D-D98373900010'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500047','DA8DAADD-52B8-4F2D-9B9D-D98373900008'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500047','DA8DAADD-52B8-4F2D-9B9D-D98373900009'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500047','DA8DAADD-52B8-4F2D-9B9D-D98373900010'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500048','DA8DAADD-52B8-4F2D-9B9D-D98373900007'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500048','DA8DAADD-52B8-4F2D-9B9D-D98373900008'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500048','DA8DAADD-52B8-4F2D-9B9D-D98373900009'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500048','DA8DAADD-52B8-4F2D-9B9D-D98373900010'),

(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500049','DA8DAADD-52B8-4F2D-9B9D-D98373900001'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500049','DA8DAADD-52B8-4F2D-9B9D-D98373900002'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500050','DA8DAADD-52B8-4F2D-9B9D-D98373900001'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500050','DA8DAADD-52B8-4F2D-9B9D-D98373900002'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500050','DA8DAADD-52B8-4F2D-9B9D-D98373900003'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500051','DA8DAADD-52B8-4F2D-9B9D-D98373900001'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500051','DA8DAADD-52B8-4F2D-9B9D-D98373900002'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500051','DA8DAADD-52B8-4F2D-9B9D-D98373900003'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500051','DA8DAADD-52B8-4F2D-9B9D-D98373900004'),

(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500052','DA8DAADD-52B8-4F2D-9B9D-D98373900009'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500052','DA8DAADD-52B8-4F2D-9B9D-D98373900010'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500053','DA8DAADD-52B8-4F2D-9B9D-D98373900008'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500053','DA8DAADD-52B8-4F2D-9B9D-D98373900009'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500053','DA8DAADD-52B8-4F2D-9B9D-D98373900010'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500054','DA8DAADD-52B8-4F2D-9B9D-D98373900007'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500054','DA8DAADD-52B8-4F2D-9B9D-D98373900008'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500054','DA8DAADD-52B8-4F2D-9B9D-D98373900009'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500054','DA8DAADD-52B8-4F2D-9B9D-D98373900010'),

(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500055','DA8DAADD-52B8-4F2D-9B9D-D98373900001'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500056','DA8DAADD-52B8-4F2D-9B9D-D98373900002'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500057','DA8DAADD-52B8-4F2D-9B9D-D98373900003'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500058','DA8DAADD-52B8-4F2D-9B9D-D98373900004'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500059','DA8DAADD-52B8-4F2D-9B9D-D98373900005'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500060','DA8DAADD-52B8-4F2D-9B9D-D98373900006'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500061','DA8DAADD-52B8-4F2D-9B9D-D98373900007'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500062','DA8DAADD-52B8-4F2D-9B9D-D98373900008'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500063','DA8DAADD-52B8-4F2D-9B9D-D98373900009'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500064','DA8DAADD-52B8-4F2D-9B9D-D98373900010'),

(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500065','DA8DAADD-52B8-4F2D-9B9D-D98373900001'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500066','DA8DAADD-52B8-4F2D-9B9D-D98373900002'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500067','DA8DAADD-52B8-4F2D-9B9D-D98373900003'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500068','DA8DAADD-52B8-4F2D-9B9D-D98373900004'),
(NEWID(),'83FF7434-C88E-45CD-A39F-73B3EB500069','DA8DAADD-52B8-4F2D-9B9D-D98373900005')
GO
