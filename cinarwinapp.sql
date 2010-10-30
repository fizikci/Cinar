/*
SQLyog Community v8.7 Beta2
MySQL - 5.5.1-m2-community : Database - cinarwinapp
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`cinarwinapp` /*!40100 DEFAULT CHARACTER SET utf8 COLLATE utf8_turkish_ci */;

USE `cinarwinapp`;

/*Table structure for table `author` */

CREATE TABLE `author` (
  `Deleted` tinyint(1) DEFAULT NULL,
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `InsertDate` datetime DEFAULT NULL,
  `InsertUserId` int(11) DEFAULT NULL,
  `MediaId` int(11) DEFAULT NULL,
  `Name` varchar(50) COLLATE utf8_turkish_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;

/*Data for the table `author` */

/*Table structure for table `category` */

CREATE TABLE `category` (
  `Deleted` tinyint(1) DEFAULT NULL,
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `InsertDate` datetime DEFAULT NULL,
  `InsertUserId` int(11) DEFAULT NULL,
  `Name` varchar(50) COLLATE utf8_turkish_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;

/*Data for the table `category` */

insert  into `category`(`Deleted`,`Id`,`InsertDate`,`InsertUserId`,`Name`) values (0,1,'2010-10-29 17:08:33',1,'POLITIKA'),(0,2,'2010-10-29 17:08:59',1,'SPOR'),(0,3,'2010-10-30 01:12:56',1,'DÜNYA');

/*Table structure for table `content` */

CREATE TABLE `content` (
  `AuthorId` int(11) DEFAULT NULL,
  `ContentDefinitionId` int(11) DEFAULT NULL,
  `Deleted` tinyint(1) DEFAULT NULL,
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ImageUrl` varchar(500) COLLATE utf8_turkish_ci DEFAULT NULL,
  `InsertDate` datetime DEFAULT NULL,
  `InsertUserId` int(11) DEFAULT NULL,
  `PublishDate` datetime DEFAULT NULL,
  `SourceUrl` varchar(500) COLLATE utf8_turkish_ci DEFAULT NULL,
  `Title` varchar(500) COLLATE utf8_turkish_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;

/*Data for the table `content` */

/*Table structure for table `contentdefinition` */

CREATE TABLE `contentdefinition` (
  `AuthorSelector` varchar(500) COLLATE utf8_turkish_ci DEFAULT NULL,
  `CategoryId` int(11) DEFAULT NULL,
  `ContentSelector` varchar(500) COLLATE utf8_turkish_ci DEFAULT NULL,
  `DateSelector` varchar(500) COLLATE utf8_turkish_ci DEFAULT NULL,
  `Deleted` tinyint(1) DEFAULT NULL,
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ImageSelector` varchar(500) COLLATE utf8_turkish_ci DEFAULT NULL,
  `InsertDate` datetime DEFAULT NULL,
  `InsertUserId` int(11) DEFAULT NULL,
  `MediaId` int(11) DEFAULT NULL,
  `RSSUrl` varchar(500) COLLATE utf8_turkish_ci DEFAULT NULL,
  `TitleSelector` varchar(500) COLLATE utf8_turkish_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;

/*Data for the table `contentdefinition` */

insert  into `contentdefinition`(`AuthorSelector`,`CategoryId`,`ContentSelector`,`DateSelector`,`Deleted`,`Id`,`ImageSelector`,`InsertDate`,`InsertUserId`,`MediaId`,`RSSUrl`,`TitleSelector`) values ('',1,'#container > #ortaBuyukSutunDiv > #detayDiv > TD.metin > #haberMetinDiv','',0,1,'#container > #ortaBuyukSutunDiv > #detayDiv > TD.metin > TD.spot-haber > IMG.haberresim','2010-10-29 18:19:27',1,1,'http://www.zaman.com.tr/politika.rss','#container > #ortaBuyukSutunDiv > #detayDiv > TD.metin > TD.metin'),(NULL,3,NULL,NULL,0,2,NULL,'2010-10-30 01:13:09',1,2,'http://www.sabah.com.tr/rss/Dunya.xml',NULL),(NULL,3,NULL,NULL,0,3,NULL,'2010-10-30 01:32:54',1,3,'http://rss.hurriyet.com.tr/rss.aspx?sectionId=3',NULL),(NULL,3,NULL,NULL,0,4,NULL,'2010-10-30 14:03:25',1,4,'http://podcast.samanyoluhaber.com/Dünya.rss',NULL),(NULL,3,'#_body > #_container > #_Middle1_1 > ','#_body > #_container > #_Middle1_1 > P.habdetay_tarih > ',0,5,'#_body > #_container > #_Middle1_1 > DIV.habdetay_habimg2 > IMG.flt_lft > ','2010-10-30 14:10:25',1,5,'http://www.radikal.com.tr/d/rss/Rss_81.xml','#_body > #_container > #_Middle1_1 > H1.habdetay_h1 > ');

/*Table structure for table `media` */

CREATE TABLE `media` (
  `Deleted` tinyint(1) DEFAULT NULL,
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `InsertDate` datetime DEFAULT NULL,
  `InsertUserId` int(11) DEFAULT NULL,
  `Name` varchar(50) COLLATE utf8_turkish_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;

/*Data for the table `media` */

insert  into `media`(`Deleted`,`Id`,`InsertDate`,`InsertUserId`,`Name`) values (0,1,'2010-10-29 15:37:42',1,'ZAMAN'),(0,2,'2010-10-29 15:38:00',1,'SABAH'),(0,3,'2010-10-30 01:31:32',1,'HÜRRİYET'),(0,4,'2010-10-30 14:02:57',1,'SAMANYOLU HABER'),(0,5,'2010-10-30 14:10:04',1,'RADİKAL');

/*Table structure for table `report` */

CREATE TABLE `report` (
  `Deleted` tinyint(1) DEFAULT NULL,
  `Html` text COLLATE utf8_turkish_ci,
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `InsertDate` datetime DEFAULT NULL,
  `InsertUserId` int(11) DEFAULT NULL,
  `Name` varchar(50) COLLATE utf8_turkish_ci DEFAULT NULL,
  `ReportLayout` text COLLATE utf8_turkish_ci,
  `SQLQuery` text COLLATE utf8_turkish_ci,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;

/*Data for the table `report` */

insert  into `report`(`Deleted`,`Html`,`Id`,`InsertDate`,`InsertUserId`,`Name`,`ReportLayout`,`SQLQuery`) values (0,'',1,'2010-10-29 04:59:24',1,'KULLANICI LİSTESİ','/// <XRTypeInfo>\r\n///   <AssemblyFullName>DevExpress.XtraReports.v9.1, Version=9.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a</AssemblyFullName>\r\n///   <AssemblyLocation>C:\\WINDOWS\\assembly\\GAC_MSIL\\DevExpress.XtraReports.v9.1\\9.1.5.0__b88d1754d700e49a\\DevExpress.XtraReports.v9.1.dll</AssemblyLocation>\r\n///   <TypeName>DevExpress.XtraReports.UI.XtraReport</TypeName>\r\n///   <Localization>tr-TR</Localization>\r\n/// </XRTypeInfo>\r\nnamespace XtraReportSerialization {\r\n    \r\n    public class XtraReport : DevExpress.XtraReports.UI.XtraReport {\r\n        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;\r\n        private DevExpress.XtraReports.UI.XRLabel label1;\r\n        private DevExpress.XtraReports.UI.DetailBand Detail;\r\n        private DevExpress.XtraReports.UI.XRLabel label4;\r\n        private DevExpress.XtraReports.UI.XRLabel label3;\r\n        private DevExpress.XtraReports.UI.XRLabel label2;\r\n        private System.Resources.ResourceManager _resources;\r\n        public XtraReport() {\r\n            this.InitializeComponent();\r\n        }\r\n        private System.Resources.ResourceManager resources {\r\n            get {\r\n                if (_resources == null) {\r\n                    string resourceString = @\"zsrvvgEAAACRAAAAbFN5c3RlbS5SZXNvdXJjZXMuUmVzb3VyY2VSZWFkZXIsIG1zY29ybGliLCBWZXJzaW9uPTIuMC4wLjAsIEN1bHR1cmU9bmV1dHJhbCwgUHVibGljS2V5VG9rZW49Yjc3YTVjNTYxOTM0ZTA4OSNTeXN0ZW0uUmVzb3VyY2VzLlJ1bnRpbWVSZXNvdXJjZVNldAIAAAAAAAAAAAAAAFBBRFBBRFC0AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA==\";\r\n                    this._resources = new DevExpress.XtraReports.Serialization.XRResourceManager(resourceString);\r\n                }\r\n                return this._resources;\r\n            }\r\n        }\r\n        private void InitializeComponent() {\r\n            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();\r\n            this.Detail = new DevExpress.XtraReports.UI.DetailBand();\r\n            this.label1 = new DevExpress.XtraReports.UI.XRLabel();\r\n            this.label4 = new DevExpress.XtraReports.UI.XRLabel();\r\n            this.label3 = new DevExpress.XtraReports.UI.XRLabel();\r\n            this.label2 = new DevExpress.XtraReports.UI.XRLabel();\r\n            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();\r\n            // \r\n            // ReportHeader\r\n            // \r\n            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {\r\n                        this.label1});\r\n            this.ReportHeader.Height = 82;\r\n            this.ReportHeader.Name = \"ReportHeader\";\r\n            // \r\n            // Detail\r\n            // \r\n            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {\r\n                        this.label4,\r\n                        this.label3,\r\n                        this.label2});\r\n            this.Detail.Height = 25;\r\n            this.Detail.Name = \"Detail\";\r\n            // \r\n            // label1\r\n            // \r\n            this.label1.Font = new System.Drawing.Font(\"Times New Roman\", 12.75F);\r\n            this.label1.Location = new System.Drawing.Point(167, 33);\r\n            this.label1.Name = \"label1\";\r\n            this.label1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);\r\n            this.label1.Size = new System.Drawing.Size(358, 25);\r\n            this.label1.StylePriority.UseFont = false;\r\n            this.label1.StylePriority.UseTextAlignment = false;\r\n            this.label1.Text = \"Kullanıcı Listesi\";\r\n            this.label1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;\r\n            // \r\n            // label4\r\n            // \r\n            this.label4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {\r\n                        new DevExpress.XtraReports.UI.XRBinding(\"Text\", null, \"Table.Password\", \"\")});\r\n            this.label4.Location = new System.Drawing.Point(208, 0);\r\n            this.label4.Name = \"label4\";\r\n            this.label4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);\r\n            this.label4.Size = new System.Drawing.Size(100, 25);\r\n            this.label4.Text = \"label4\";\r\n            // \r\n            // label3\r\n            // \r\n            this.label3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {\r\n                        new DevExpress.XtraReports.UI.XRBinding(\"Text\", null, \"Table.UserName\", \"\")});\r\n            this.label3.Location = new System.Drawing.Point(108, 0);\r\n            this.label3.Name = \"label3\";\r\n            this.label3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);\r\n            this.label3.Size = new System.Drawing.Size(100, 25);\r\n            this.label3.Text = \"label3\";\r\n            // \r\n            // label2\r\n            // \r\n            this.label2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {\r\n                        new DevExpress.XtraReports.UI.XRBinding(\"Text\", null, \"Table.Name\", \"\")});\r\n            this.label2.Location = new System.Drawing.Point(8, 0);\r\n            this.label2.Name = \"label2\";\r\n            this.label2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);\r\n            this.label2.Size = new System.Drawing.Size(100, 25);\r\n            this.label2.Text = \"label2\";\r\n            // \r\n            // XtraReport\r\n            // \r\n            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {\r\n                        this.ReportHeader,\r\n                        this.Detail});\r\n            this.Name = \"XtraReport\";\r\n            this.PageHeight = 1100;\r\n            this.PageWidth = 850;\r\n            this.Version = \"9.1\";\r\n            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();\r\n        }\r\n    }\r\n}\r\n','select * from User where id=@id'),(0,'<ul>\r\n$\r\nforeach(user in ds.Tables[0].Rows)\r\n   echo(\"<li>\"+user.Name+\"</li>\");\r\n$\r\n</ul>',2,'2010-10-29 05:04:34',1,'HTML RAPORU','/// <XRTypeInfo>\r\n///   <AssemblyFullName>DevExpress.XtraReports.v9.1, Version=9.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a</AssemblyFullName>\r\n///   <AssemblyLocation>C:\\WINDOWS\\assembly\\GAC_MSIL\\DevExpress.XtraReports.v9.1\\9.1.5.0__b88d1754d700e49a\\DevExpress.XtraReports.v9.1.dll</AssemblyLocation>\r\n///   <TypeName>DevExpress.XtraReports.UI.XtraReport</TypeName>\r\n///   <Localization>tr-TR</Localization>\r\n/// </XRTypeInfo>\r\nnamespace XtraReportSerialization {\r\n    \r\n    public class XtraReport : DevExpress.XtraReports.UI.XtraReport {\r\n        private System.Resources.ResourceManager _resources;\r\n        public XtraReport() {\r\n            this.InitializeComponent();\r\n        }\r\n        private System.Resources.ResourceManager resources {\r\n            get {\r\n                if (_resources == null) {\r\n                    string resourceString = @\"zsrvvgEAAACRAAAAbFN5c3RlbS5SZXNvdXJjZXMuUmVzb3VyY2VSZWFkZXIsIG1zY29ybGliLCBWZXJzaW9uPTIuMC4wLjAsIEN1bHR1cmU9bmV1dHJhbCwgUHVibGljS2V5VG9rZW49Yjc3YTVjNTYxOTM0ZTA4OSNTeXN0ZW0uUmVzb3VyY2VzLlJ1bnRpbWVSZXNvdXJjZVNldAIAAAAAAAAAAAAAAFBBRFBBRFC0AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA==\";\r\n                    this._resources = new DevExpress.XtraReports.Serialization.XRResourceManager(resourceString);\r\n                }\r\n                return this._resources;\r\n            }\r\n        }\r\n        private void InitializeComponent() {\r\n            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();\r\n            // \r\n            // XtraReport\r\n            // \r\n            this.Name = \"XtraReport\";\r\n            this.PageHeight = 1100;\r\n            this.PageWidth = 850;\r\n            this.Version = \"9.1\";\r\n            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();\r\n        }\r\n    }\r\n}\r\n','select * from User');

/*Table structure for table `reportparam` */

CREATE TABLE `reportparam` (
  `Deleted` tinyint(1) DEFAULT NULL,
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `InsertDate` datetime DEFAULT NULL,
  `InsertUserId` int(11) DEFAULT NULL,
  `Name` varchar(50) COLLATE utf8_turkish_ci DEFAULT NULL,
  `PDisplayField` varchar(50) COLLATE utf8_turkish_ci DEFAULT NULL,
  `PEntityName` varchar(50) COLLATE utf8_turkish_ci DEFAULT NULL,
  `PName` varchar(50) COLLATE utf8_turkish_ci DEFAULT NULL,
  `PType` int(11) DEFAULT NULL,
  `PValueField` varchar(50) COLLATE utf8_turkish_ci DEFAULT NULL,
  `ReportId` int(11) DEFAULT NULL,
  `PModuleName` varchar(50) COLLATE utf8_turkish_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;

/*Data for the table `reportparam` */

insert  into `reportparam`(`Deleted`,`Id`,`InsertDate`,`InsertUserId`,`Name`,`PDisplayField`,`PEntityName`,`PName`,`PType`,`PValueField`,`ReportId`,`PModuleName`) values (0,1,'2010-10-29 13:02:11',0,'Kullanıcı','Name','User','id',6,'Id',1,'Standart');

/*Table structure for table `right` */

CREATE TABLE `right` (
  `Deleted` tinyint(1) DEFAULT NULL,
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `InsertDate` datetime DEFAULT NULL,
  `InsertUserId` int(11) DEFAULT NULL,
  `Name` varchar(50) COLLATE utf8_turkish_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;

/*Data for the table `right` */

insert  into `right`(`Deleted`,`Id`,`InsertDate`,`InsertUserId`,`Name`) values (0,1,'2010-10-29 04:53:20',1,'Open User');

/*Table structure for table `role` */

CREATE TABLE `role` (
  `Deleted` tinyint(1) DEFAULT NULL,
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `InsertDate` datetime DEFAULT NULL,
  `InsertUserId` int(11) DEFAULT NULL,
  `Name` varchar(50) COLLATE utf8_turkish_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;

/*Data for the table `role` */

insert  into `role`(`Deleted`,`Id`,`InsertDate`,`InsertUserId`,`Name`) values (0,1,'2010-10-29 04:47:24',1,'ADMİN');

/*Table structure for table `roleright` */

CREATE TABLE `roleright` (
  `Deleted` tinyint(1) DEFAULT NULL,
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `InsertDate` datetime DEFAULT NULL,
  `InsertUserId` int(11) DEFAULT NULL,
  `RightId` int(11) DEFAULT NULL,
  `RoleId` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;

/*Data for the table `roleright` */

insert  into `roleright`(`Deleted`,`Id`,`InsertDate`,`InsertUserId`,`RightId`,`RoleId`) values (0,1,'2010-10-29 04:53:20',1,1,1);

/*Table structure for table `roleuser` */

CREATE TABLE `roleuser` (
  `Deleted` tinyint(1) DEFAULT NULL,
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `InsertDate` datetime DEFAULT NULL,
  `InsertUserId` int(11) DEFAULT NULL,
  `RoleId` int(11) DEFAULT NULL,
  `UserId` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;

/*Data for the table `roleuser` */

insert  into `roleuser`(`Deleted`,`Id`,`InsertDate`,`InsertUserId`,`RoleId`,`UserId`) values (0,1,'2010-10-29 04:57:00',0,1,1);

/*Table structure for table `usagereport` */

CREATE TABLE `usagereport` (
  `Deleted` tinyint(1) DEFAULT NULL,
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `InsertDate` datetime DEFAULT NULL,
  `InsertUserId` int(11) DEFAULT NULL,
  `IntVal` int(11) DEFAULT NULL,
  `UsageType1` varchar(50) COLLATE utf8_turkish_ci DEFAULT NULL,
  `UsageType2` varchar(50) COLLATE utf8_turkish_ci DEFAULT NULL,
  `UsageType3` varchar(50) COLLATE utf8_turkish_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=79 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;

/*Data for the table `usagereport` */

insert  into `usagereport`(`Deleted`,`Id`,`InsertDate`,`InsertUserId`,`IntVal`,`UsageType1`,`UsageType2`,`UsageType3`) values (0,1,'2010-10-29 04:27:09',1,0,'OpenForm','EditUser',''),(0,2,'2010-10-29 04:32:48',1,0,'OpenForm','EditRole',''),(0,3,'2010-10-29 04:41:45',1,0,'OpenForm','EditUser',''),(0,4,'2010-10-29 04:47:21',1,0,'OpenForm','EditRole',''),(0,5,'2010-10-29 04:50:24',1,0,'OpenForm','EditRole',''),(0,6,'2010-10-29 04:52:35',1,0,'OpenForm','EditRole',''),(0,7,'2010-10-29 04:56:53',1,0,'OpenForm','EditRole',''),(0,8,'2010-10-29 04:57:27',1,0,'OpenForm','EditRole',''),(0,9,'2010-10-29 04:57:34',1,0,'OpenForm','EditTemplate',''),(0,10,'2010-10-29 04:59:24',1,0,'OpenForm','EditTemplate',''),(0,11,'2010-10-29 05:02:14',1,0,'OpenForm','EditTemplate',''),(0,12,'2010-10-29 05:06:28',1,0,'OpenForm','FormExecuteTemplate',''),(0,13,'2010-10-29 05:10:01',1,0,'OpenForm','EditTemplate',''),(0,14,'2010-10-29 05:10:23',1,0,'OpenForm','FormExecuteTemplate',''),(0,15,'2010-10-29 05:14:40',1,0,'OpenForm','FormExecuteTemplate',''),(0,16,'2010-10-29 05:21:56',1,0,'OpenForm','EditRole',''),(0,17,'2010-10-29 05:26:41',1,0,'OpenForm','EditUser',''),(0,18,'2010-10-29 05:26:43',1,0,'OpenForm','EditRole',''),(0,19,'2010-10-29 05:26:47',1,0,'OpenForm','EditTemplate',''),(0,20,'2010-10-29 05:26:53',1,0,'OpenForm','FormExecuteTemplate',''),(0,21,'2010-10-29 12:59:21',1,0,'OpenForm','EditTemplate',''),(0,22,'2010-10-29 13:17:23',1,0,'OpenForm','EditTemplate',''),(0,23,'2010-10-29 13:21:10',1,0,'OpenForm','EditTemplate',''),(0,24,'2010-10-29 13:29:24',1,0,'OpenForm','EditTemplate',''),(0,25,'2010-10-29 13:59:55',1,0,'OpenForm','EditTemplate',''),(0,26,'2010-10-29 14:03:43',1,0,'OpenForm','EditTemplate',''),(0,27,'2010-10-29 14:05:35',1,0,'OpenForm','EditTemplate',''),(0,28,'2010-10-29 14:07:09',1,0,'OpenForm','FormExecuteTemplate',''),(0,29,'2010-10-29 15:36:37',1,0,'OpenForm','EditMedia',''),(0,30,'2010-10-29 15:57:25',1,0,'OpenForm','EditRole',''),(0,31,'2010-10-29 15:59:05',1,0,'OpenForm','EditMedia',''),(0,32,'2010-10-29 15:59:41',1,0,'OpenForm','EditUser',''),(0,33,'2010-10-29 16:40:01',1,0,'OpenForm','EditRole',''),(0,34,'2010-10-29 16:41:11',1,0,'OpenForm','EditRole',''),(0,35,'2010-10-29 16:43:49',1,0,'OpenForm','EditRole',''),(0,36,'2010-10-29 17:08:26',1,0,'OpenForm','EditCategory',''),(0,37,'2010-10-29 17:29:01',1,0,'OpenForm','EditCategory',''),(0,38,'2010-10-29 18:17:19',1,0,'OpenForm','EditContentDefinition',''),(0,39,'2010-10-29 18:19:28',1,0,'OpenForm','EditContentDefinition',''),(0,40,'2010-10-29 18:28:35',1,0,'OpenForm','EditContentDefinition',''),(0,41,'2010-10-29 18:28:59',1,0,'OpenForm','EditContentDefinition',''),(0,42,'2010-10-29 18:29:14',1,0,'OpenForm','EditContentDefinition',''),(0,43,'2010-10-29 19:23:01',1,0,'OpenForm','EditContentDefinition',''),(0,44,'2010-10-29 19:25:21',1,0,'OpenForm','EditContentDefinition',''),(0,45,'2010-10-29 22:11:26',1,0,'OpenForm','EditContentDefinition',''),(0,46,'2010-10-29 23:09:36',1,0,'OpenForm','EditContentDefinition',''),(0,47,'2010-10-29 23:13:13',1,0,'OpenForm','EditContentDefinition',''),(0,48,'2010-10-29 23:16:08',1,0,'OpenForm','EditContentDefinition',''),(0,49,'2010-10-29 23:24:04',1,0,'OpenForm','EditContentDefinition',''),(0,50,'2010-10-29 23:30:46',1,0,'OpenForm','EditContentDefinition',''),(0,51,'2010-10-29 23:33:58',1,0,'OpenForm','EditContentDefinition',''),(0,52,'2010-10-29 23:41:26',1,0,'OpenForm','EditContentDefinition',''),(0,53,'2010-10-29 23:45:53',1,0,'OpenForm','EditContentDefinition',''),(0,54,'2010-10-29 23:53:53',1,0,'OpenForm','EditContentDefinition',''),(0,55,'2010-10-30 00:13:46',1,0,'OpenForm','EditContentDefinition',''),(0,56,'2010-10-30 00:34:07',1,0,'OpenForm','EditContentDefinition',''),(0,57,'2010-10-30 00:37:14',1,0,'OpenForm','EditContentDefinition',''),(0,58,'2010-10-30 00:42:41',1,0,'OpenForm','EditContentDefinition',''),(0,59,'2010-10-30 00:45:30',1,0,'OpenForm','EditContentDefinition',''),(0,60,'2010-10-30 00:45:39',1,0,'OpenForm','EditContentDefinition',''),(0,61,'2010-10-30 01:12:53',1,0,'OpenForm','EditCategory',''),(0,62,'2010-10-30 01:13:05',1,0,'OpenForm','EditContentDefinition',''),(0,63,'2010-10-30 01:31:31',1,0,'OpenForm','EditMedia',''),(0,64,'2010-10-30 01:31:45',1,0,'OpenForm','EditContentDefinition',''),(0,65,'2010-10-30 09:50:17',1,0,'OpenForm','EditContentDefinition',''),(0,66,'2010-10-30 10:05:56',1,0,'OpenForm','EditMedia',''),(0,67,'2010-10-30 10:06:10',1,0,'OpenForm','EditCategory',''),(0,68,'2010-10-30 10:55:39',1,0,'OpenForm','EditContentDefinition',''),(0,69,'2010-10-30 11:00:16',1,0,'OpenForm','EditContentDefinition',''),(0,70,'2010-10-30 11:05:02',1,0,'OpenForm','EditContentDefinition',''),(0,71,'2010-10-30 11:07:10',1,0,'OpenForm','EditContentDefinition',''),(0,72,'2010-10-30 11:31:39',1,0,'OpenForm','EditContentDefinition',''),(0,73,'2010-10-30 11:34:05',1,0,'OpenForm','EditContentDefinition',''),(0,74,'2010-10-30 12:58:32',1,0,'OpenForm','EditContentDefinition',''),(0,75,'2010-10-30 13:04:19',1,0,'OpenForm','EditContentDefinition',''),(0,76,'2010-10-30 13:15:44',1,0,'OpenForm','EditContentDefinition',''),(0,77,'2010-10-30 13:48:49',1,0,'OpenForm','EditContentDefinition',''),(0,78,'2010-10-30 14:02:54',1,0,'OpenForm','EditMedia','');

/*Table structure for table `user` */

CREATE TABLE `user` (
  `Deleted` tinyint(1) DEFAULT NULL,
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `InsertDate` datetime DEFAULT NULL,
  `InsertUserId` int(11) DEFAULT NULL,
  `Name` varchar(50) COLLATE utf8_turkish_ci DEFAULT NULL,
  `Password` varchar(50) COLLATE utf8_turkish_ci DEFAULT NULL,
  `UserName` varchar(50) COLLATE utf8_turkish_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;

/*Data for the table `user` */

insert  into `user`(`Deleted`,`Id`,`InsertDate`,`InsertUserId`,`Name`,`Password`,`UserName`) values (0,1,'1980-01-01 00:00:00',1,'Bülent','bk','bulent');

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
