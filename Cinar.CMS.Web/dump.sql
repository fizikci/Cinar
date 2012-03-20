/*
SQLyog Community v8.71 
MySQL - 5.1.50-community : Database - osmanozmen
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
/*Table structure for table `author` */

DROP TABLE IF EXISTS `author`;

CREATE TABLE `author` (
  `DisableAutoContent` tinyint(1) NOT NULL DEFAULT '0',
  `UserId` int(11) DEFAULT NULL,
  `Name` varchar(100) COLLATE utf8_turkish_ci NOT NULL,
  `Description` text COLLATE utf8_turkish_ci,
  `Picture` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `Picture2` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `InsertDate` datetime NOT NULL DEFAULT '1990-01-01 00:00:00',
  `InsertUserId` int(11) DEFAULT NULL,
  `UpdateDate` datetime DEFAULT NULL,
  `UpdateUserId` int(11) DEFAULT NULL,
  `Visible` tinyint(1) NOT NULL DEFAULT '1',
  `OrderNo` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;

/*Data for the table `author` */

insert  into `author`(`DisableAutoContent`,`UserId`,`Name`,`Description`,`Picture`,`Picture2`,`Id`,`InsertDate`,`InsertUserId`,`UpdateDate`,`UpdateUserId`,`Visible`,`OrderNo`) values (0,NULL,'Editorial',NULL,NULL,NULL,1,'1990-01-01 00:00:00',NULL,NULL,NULL,1,NULL);

/*Table structure for table `configuration` */

DROP TABLE IF EXISTS `configuration`;

CREATE TABLE `configuration` (
  `SiteName` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `SiteAddress` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `SiteDescription` text COLLATE utf8_turkish_ci,
  `SiteKeywords` text COLLATE utf8_turkish_ci,
  `SiteLogo` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `SiteIcon` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `SessionTimeout` int(11) DEFAULT NULL,
  `BufferOutput` tinyint(1) DEFAULT NULL,
  `MultiLang` tinyint(1) DEFAULT NULL,
  `DefaultLang` int(11) NOT NULL,
  `DefaultStyleSheet` text COLLATE utf8_turkish_ci,
  `DefaultJavascript` text COLLATE utf8_turkish_ci,
  `CountTags` tinyint(1) DEFAULT NULL,
  `DefaultDateFormat` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `NoPicture` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `ThumbQuality` int(11) DEFAULT NULL,
  `ImageUploadMaxWidth` int(11) DEFAULT NULL,
  `AuthEmail` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `MailHost` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `MailUsername` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `MailPassword` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `UseCache` varchar(30) COLLATE utf8_turkish_ci NOT NULL DEFAULT 'False',
  `CacheLifeTime` int(11) DEFAULT NULL,
  `MainPage` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `CategoryPage` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `ContentPage` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `LoginPage` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `MembershipFormPage` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `MembershipProfilePage` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `RememberPasswordFormPage` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `UserActivationPage` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `AdminPage` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `InsertDate` datetime NOT NULL DEFAULT '1990-01-01 00:00:00',
  `InsertUserId` int(11) DEFAULT NULL,
  `UpdateDate` datetime DEFAULT NULL,
  `UpdateUserId` int(11) DEFAULT NULL,
  `Visible` tinyint(1) NOT NULL DEFAULT '1',
  `OrderNo` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;

/*Data for the table `configuration` */

insert  into `configuration`(`SiteName`,`SiteAddress`,`SiteDescription`,`SiteKeywords`,`SiteLogo`,`SiteIcon`,`SessionTimeout`,`BufferOutput`,`MultiLang`,`DefaultLang`,`DefaultStyleSheet`,`DefaultJavascript`,`CountTags`,`DefaultDateFormat`,`NoPicture`,`ThumbQuality`,`ImageUploadMaxWidth`,`AuthEmail`,`MailHost`,`MailUsername`,`MailPassword`,`UseCache`,`CacheLifeTime`,`MainPage`,`CategoryPage`,`ContentPage`,`LoginPage`,`MembershipFormPage`,`MembershipProfilePage`,`RememberPasswordFormPage`,`UserActivationPage`,`AdminPage`,`Id`,`InsertDate`,`InsertUserId`,`UpdateDate`,`UpdateUserId`,`Visible`,`OrderNo`) values ('isimsiz.com','www.isimsiz.com','Sitenin tanımı, bu site ne hakkındadır?','sitenizle,ilgili,anahtar,kelimleri,böyle,yazınız','','',120,1,0,1,'body {\n	font-family:tahoma,arial;\n	font-size:12px;\n	text-align:center;\n	margin:0px;\n	background-image:url(/UserFiles/_design/top_bg.png);\n	background-repeat:repeat-x;\n	color:#717171;\n}\ntd {\n	font-size:12px;\n}\na {\n	color: blue;\n	text-decoration:none\n}\na:hover{\n	color: black;\n}\nb {font-weight:bold}\ni {font-style:italic}\n* {\n	-moz-box-sizing:border-box;\n	box-sizing:border-box;\n}\nul {}\nli {list-style:circle; margin-left:26px}\nol li {list-style:decimal;}\n\ndiv.Region {\n	width: 950px; \n	margin-right:auto;\n	margin-left:auto;\n	text-align:left;\n}\n#HeaderTop {text-align:left;height:108px;background-image:url(/UserFiles/_design/top_bg.jpg);}\n#HeaderCont {\n    background: none repeat scroll 0 0 #151515;\n    box-shadow: 0 0 20px #999999;\n    height: 410px;\n    margin-bottom: 20px;\n    padding: 30px 0;\n    position: relative;\n}\n#Header {text-align:left}\n#ContentCont {background:url(/UserFiles/_design/content_bg.png);padding:50px 0px;}\n#Content {text-align:left; padding:4px;min-width:710px;min-height:500px;}\n#FooterCont {background:url(/UserFiles/_design/bottom_bg.jpg)}\n#Footer {text-align:left;width: 950px;margin-right:auto;margin-left:auto;}\n\n#site-title {\n    font-size: 36px;\n    line-height: 100px;\n    margin: 0 auto;\n    text-transform: none;\n}\n','',1,'dd MMMM yyyy','',55,640,'info@isimsiz.com','mail.isimsiz.com','','','False',15,'Default.aspx','Category.aspx','Content.aspx','Login.aspx','Membership.aspx','Profile.aspx','RememberPassword.aspx','Activation.aspx','Admin.aspx',1,'2012-03-20 22:39:46',3,'2012-03-20 23:21:07',1,1,0);

/*Table structure for table `content` */

DROP TABLE IF EXISTS `content`;

CREATE TABLE `content` (
  `ClassName` varchar(100) COLLATE utf8_turkish_ci NOT NULL DEFAULT 'Content',
  `CategoryId` int(11) NOT NULL DEFAULT '1',
  `Title` varchar(200) COLLATE utf8_turkish_ci NOT NULL,
  `Description` text COLLATE utf8_turkish_ci,
  `Metin` text COLLATE utf8_turkish_ci,
  `PublishDate` datetime NOT NULL DEFAULT '1990-01-01 00:00:00',
  `Picture` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `Picture2` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `Keywords` text COLLATE utf8_turkish_ci,
  `Hierarchy` varchar(100) COLLATE utf8_turkish_ci NOT NULL,
  `AuthorId` int(11) DEFAULT NULL,
  `SourceId` int(11) DEFAULT NULL,
  `Tags` varchar(300) COLLATE utf8_turkish_ci DEFAULT NULL,
  `TagRanks` varchar(300) COLLATE utf8_turkish_ci DEFAULT NULL,
  `ShowInPage` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `ShowContentsInPage` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `ShowCategoriesInPage` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `IsManset` tinyint(1) NOT NULL DEFAULT '0',
  `SpotTitle` varchar(200) COLLATE utf8_turkish_ci DEFAULT NULL,
  `ContentSourceId` int(11) DEFAULT NULL,
  `SourceLink` varchar(200) COLLATE utf8_turkish_ci DEFAULT NULL,
  `ViewCount` int(11) NOT NULL DEFAULT '0',
  `CommentCount` int(11) NOT NULL DEFAULT '0',
  `RecommendCount` int(11) NOT NULL DEFAULT '0',
  `LikeIt` int(11) NOT NULL DEFAULT '0',
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `InsertDate` datetime NOT NULL DEFAULT '1990-01-01 00:00:00',
  `InsertUserId` int(11) DEFAULT NULL,
  `UpdateDate` datetime DEFAULT NULL,
  `UpdateUserId` int(11) DEFAULT NULL,
  `Visible` tinyint(1) NOT NULL DEFAULT '1',
  `OrderNo` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;

/*Data for the table `content` */

insert  into `content`(`ClassName`,`CategoryId`,`Title`,`Description`,`Metin`,`PublishDate`,`Picture`,`Picture2`,`Keywords`,`Hierarchy`,`AuthorId`,`SourceId`,`Tags`,`TagRanks`,`ShowInPage`,`ShowContentsInPage`,`ShowCategoriesInPage`,`IsManset`,`SpotTitle`,`ContentSourceId`,`SourceLink`,`ViewCount`,`CommentCount`,`RecommendCount`,`LikeIt`,`Id`,`InsertDate`,`InsertUserId`,`UpdateDate`,`UpdateUserId`,`Visible`,`OrderNo`) values ('Category',0,'Kök',NULL,NULL,'1980-01-01 00:00:00',NULL,NULL,NULL,'',NULL,NULL,NULL,NULL,NULL,NULL,NULL,0,NULL,NULL,NULL,0,0,0,0,1,'2012-03-20 22:39:47',NULL,NULL,NULL,1,NULL),('Category',1,'Sayfalar','','','2012-01-30 00:00:44','','','','00001',1,1,'','','','','',0,'',0,'',0,0,0,0,2,'2012-03-20 23:50:58',1,'2012-03-21 00:06:57',1,1,0),('Content',2,'Biografi','','','2012-03-21 00:10:17','/UserFiles/Images/_upload/2012/3/21/Biografi_173.jpg','','','00001,00002',1,1,'','','','','',0,'',0,'',0,0,0,0,3,'2012-03-21 00:11:08',1,'2012-03-21 00:31:15',1,1,0),('Content',2,'Heykel','','','2012-03-21 00:11:34','/UserFiles/Images/_upload/2012/3/21/Heykel_744.jpg','','','00001,00002',1,1,'','','','','',0,'',0,'',0,0,0,0,4,'2012-03-21 00:11:36',1,'2012-03-21 00:14:40',1,1,0),('Content',2,'Rolyef','','','2012-03-21 00:11:56','/UserFiles/Images/_upload/2012/3/21/Rolyef_935.JPG','','','00001,00002',1,1,'','','','','',0,'',0,'',0,0,0,0,5,'2012-03-21 00:11:46',1,'2012-03-21 00:16:09',1,1,0),('Content',2,'Vitray','','','2012-03-21 00:11:57','/UserFiles/Images/_upload/2012/3/21/Vitray_34.jpg','','','00001,00002',1,1,'','','','','',0,'',0,'',0,0,0,0,6,'2012-03-21 00:11:53',1,'2012-03-21 00:23:16',1,1,0),('Content',2,'Resim','','','2012-03-21 00:11:25','/UserFiles/Images/_upload/2012/3/21/Resim_585.jpg','','','00001,00002',1,1,'','','','','',0,'',0,'',0,0,0,0,7,'2012-03-21 00:12:00',1,'2012-03-21 00:27:32',1,1,0),('Content',2,'Grafik','','','2012-03-21 00:12:06','/UserFiles/Images/_upload/2012/3/21/Grafik_217.jpg','','','00001,00002',1,1,'','','','','',0,'',0,'',0,0,0,0,8,'2012-03-21 00:12:08',1,'2012-03-21 00:26:14',1,1,0),('Content',2,'Tasarım','','','2012-03-21 00:12:34','/UserFiles/Images/_upload/2012/3/21/Tasarim_194.jpg','','','00001,00002',1,1,'','','','','',0,'',0,'',0,0,0,0,9,'2012-03-21 00:12:16',1,'2012-03-21 00:28:39',1,1,0),('Content',2,'İletişim','','','2012-03-21 00:12:47','/UserFiles/Images/_upload/2012/3/21/Iletisim_36.jpg','','','00001,00002',1,1,'','','','','',0,'',0,'',0,0,0,0,10,'2012-03-21 00:12:23',1,'2012-03-21 00:30:04',1,1,0);

/*Table structure for table `lang` */

DROP TABLE IF EXISTS `lang`;

CREATE TABLE `lang` (
  `Code` varchar(100) COLLATE utf8_turkish_ci NOT NULL,
  `Name` varchar(100) COLLATE utf8_turkish_ci NOT NULL,
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `InsertDate` datetime NOT NULL DEFAULT '1990-01-01 00:00:00',
  `InsertUserId` int(11) DEFAULT NULL,
  `UpdateDate` datetime DEFAULT NULL,
  `UpdateUserId` int(11) DEFAULT NULL,
  `Visible` tinyint(1) NOT NULL DEFAULT '1',
  `OrderNo` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;

/*Data for the table `lang` */

insert  into `lang`(`Code`,`Name`,`Id`,`InsertDate`,`InsertUserId`,`UpdateDate`,`UpdateUserId`,`Visible`,`OrderNo`) values ('tr-TR','Türkçe',1,'1990-01-01 00:00:00',NULL,NULL,NULL,1,NULL);

/*Table structure for table `log` */

DROP TABLE IF EXISTS `log`;

CREATE TABLE `log` (
  `LogType` varchar(100) COLLATE utf8_turkish_ci NOT NULL,
  `Category` varchar(100) COLLATE utf8_turkish_ci NOT NULL,
  `Description` text COLLATE utf8_turkish_ci NOT NULL,
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `InsertDate` datetime NOT NULL DEFAULT '1990-01-01 00:00:00',
  `InsertUserId` int(11) DEFAULT NULL,
  `UpdateDate` datetime DEFAULT NULL,
  `UpdateUserId` int(11) DEFAULT NULL,
  `Visible` tinyint(1) NOT NULL DEFAULT '1',
  `OrderNo` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;

/*Data for the table `log` */

insert  into `log`(`LogType`,`Category`,`Description`,`Id`,`InsertDate`,`InsertUserId`,`UpdateDate`,`UpdateUserId`,`Visible`,`OrderNo`) values ('Notice','DownloadPicture','Illegal characters in path.\n (Picture: http://www.ebibleteacher.com/sites/default/files/powerpoint-backgrounds/1/stainedglass.jpg?1310970409)',1,'2012-03-21 00:22:32',1,'2012-03-21 00:22:32',1,1,0);

/*Table structure for table `module` */

DROP TABLE IF EXISTS `module`;

CREATE TABLE `module` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Template` varchar(50) COLLATE utf8_turkish_ci NOT NULL,
  `Region` varchar(50) COLLATE utf8_turkish_ci NOT NULL,
  `OrderNo` int(11) NOT NULL DEFAULT '0',
  `Name` varchar(30) COLLATE utf8_turkish_ci NOT NULL,
  `CSS` text COLLATE utf8_turkish_ci,
  `Details` text COLLATE utf8_turkish_ci,
  `TopHtml` text COLLATE utf8_turkish_ci,
  `BottomHtml` text COLLATE utf8_turkish_ci,
  `ParentModuleId` int(11) NOT NULL DEFAULT '0',
  `CSSClass` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `Visible` tinyint(1) DEFAULT NULL,
  `RoleToRead` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `UseCache` varchar(30) COLLATE utf8_turkish_ci NOT NULL DEFAULT 'Default',
  `CacheLifeTime` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;

/*Data for the table `module` */

insert  into `module`(`Id`,`Template`,`Region`,`OrderNo`,`Name`,`CSS`,`Details`,`TopHtml`,`BottomHtml`,`ParentModuleId`,`CSSClass`,`Visible`,`RoleToRead`,`UseCache`,`CacheLifeTime`) values (1,'Default.aspx','Content',0,'LoginForm',NULL,'Cinar.CMS.Serialization\nRedirect,0,ShowMembershipLink,4,TrueShowMembershipInfoLink,4,TrueShowPasswordForgetLink,4,TrueShowRememberMe,4,TrueShowActivationLink,4,TrueId,1,1Template,9,Main.aspxRegion,7,ContentOrderNo,1,0Name,9,LoginFormCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL),(2,'Default.aspx','HeaderTop',0,'StaticHtml','','Cinar.CMS.Serialization\nInnerHtml,36,<h1 id=\"site-title\">Özgür Özmen</h1>LangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,1,2Template,12,Default.aspxRegion,9,HeaderTopOrderNo,1,0Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL);

/*Table structure for table `source` */

DROP TABLE IF EXISTS `source`;

CREATE TABLE `source` (
  `UserId` int(11) DEFAULT NULL,
  `Name` varchar(100) COLLATE utf8_turkish_ci NOT NULL,
  `Description` text COLLATE utf8_turkish_ci,
  `Picture` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `Picture2` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `InsertDate` datetime NOT NULL DEFAULT '1990-01-01 00:00:00',
  `InsertUserId` int(11) DEFAULT NULL,
  `UpdateDate` datetime DEFAULT NULL,
  `UpdateUserId` int(11) DEFAULT NULL,
  `Visible` tinyint(1) NOT NULL DEFAULT '1',
  `OrderNo` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;

/*Data for the table `source` */

insert  into `source`(`UserId`,`Name`,`Description`,`Picture`,`Picture2`,`Id`,`InsertDate`,`InsertUserId`,`UpdateDate`,`UpdateUserId`,`Visible`,`OrderNo`) values (NULL,'Editorial',NULL,NULL,NULL,1,'1990-01-01 00:00:00',NULL,NULL,NULL,1,NULL);

/*Table structure for table `template` */

DROP TABLE IF EXISTS `template`;

CREATE TABLE `template` (
  `FileName` varchar(50) COLLATE utf8_turkish_ci DEFAULT NULL,
  `HTMLCode` text COLLATE utf8_turkish_ci,
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `InsertDate` datetime NOT NULL DEFAULT '1990-01-01 00:00:00',
  `InsertUserId` int(11) DEFAULT NULL,
  `UpdateDate` datetime DEFAULT NULL,
  `UpdateUserId` int(11) DEFAULT NULL,
  `Visible` tinyint(1) NOT NULL DEFAULT '1',
  `OrderNo` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;

/*Data for the table `template` */

insert  into `template`(`FileName`,`HTMLCode`,`Id`,`InsertDate`,`InsertUserId`,`UpdateDate`,`UpdateUserId`,`Visible`,`OrderNo`) values ('Default.aspx','<html>\n<head>\n$=this.HeadSection$\n</head>\n<body>\n	<div id=\"HeaderTop\" class=\"Region HeaderTop\">$=this.HeaderTop$</div>\n\n	<div id=\"HeaderCont\">\n        	<div id=\"Header\" class=\"Region Header\">$=this.Header$</div>\n	</div>\n\n	<div id=\"ContentCont\">\n        	<div id=\"Content\" class=\"Region Content\" style=\"min-height:100px\">$=this.Content$</div>\n		<div style=\"clear:both\"></div>\n	</div>\n\n	<div id=\"FooterCont\">\n		<div id=\"Footer\" class=\"Region Footer\">$=this.Footer$</div>\n	</div>\n</body>\n</html>',1,'1990-01-01 00:00:00',0,'2012-03-20 23:03:19',1,1,0);

/*Table structure for table `user` */

DROP TABLE IF EXISTS `user`;

CREATE TABLE `user` (
  `Email` varchar(100) COLLATE utf8_turkish_ci NOT NULL,
  `Password` varchar(16) COLLATE utf8_turkish_ci NOT NULL,
  `Keyword` varchar(16) COLLATE utf8_turkish_ci DEFAULT NULL,
  `Nick` varchar(100) COLLATE utf8_turkish_ci NOT NULL,
  `Roles` varchar(100) COLLATE utf8_turkish_ci NOT NULL,
  `Name` varchar(50) COLLATE utf8_turkish_ci DEFAULT NULL,
  `Surname` varchar(50) COLLATE utf8_turkish_ci DEFAULT NULL,
  `Gender` varchar(50) COLLATE utf8_turkish_ci DEFAULT NULL,
  `BirthDate` datetime DEFAULT NULL,
  `IdentityNumber` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `Occupation` varchar(50) COLLATE utf8_turkish_ci DEFAULT NULL,
  `Company` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `Department` varchar(50) COLLATE utf8_turkish_ci DEFAULT NULL,
  `PhoneCell` varchar(50) COLLATE utf8_turkish_ci DEFAULT NULL,
  `PhoneWork` varchar(50) COLLATE utf8_turkish_ci DEFAULT NULL,
  `PhoneHome` varchar(50) COLLATE utf8_turkish_ci DEFAULT NULL,
  `AddressLine1` varchar(200) COLLATE utf8_turkish_ci DEFAULT NULL,
  `AddressLine2` varchar(200) COLLATE utf8_turkish_ci DEFAULT NULL,
  `City` varchar(50) COLLATE utf8_turkish_ci DEFAULT NULL,
  `Country` varchar(50) COLLATE utf8_turkish_ci DEFAULT NULL,
  `ZipCode` varchar(5) COLLATE utf8_turkish_ci DEFAULT NULL,
  `Web` varchar(150) COLLATE utf8_turkish_ci DEFAULT NULL,
  `Education` varchar(50) COLLATE utf8_turkish_ci DEFAULT NULL,
  `Certificates` varchar(200) COLLATE utf8_turkish_ci DEFAULT NULL,
  `About` varchar(200) COLLATE utf8_turkish_ci DEFAULT NULL,
  `Avatar` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `RedirectCount` int(11) DEFAULT NULL,
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `InsertDate` datetime NOT NULL DEFAULT '1990-01-01 00:00:00',
  `InsertUserId` int(11) DEFAULT NULL,
  `UpdateDate` datetime DEFAULT NULL,
  `UpdateUserId` int(11) DEFAULT NULL,
  `Visible` tinyint(1) NOT NULL DEFAULT '1',
  `OrderNo` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;

/*Data for the table `user` */

insert  into `user`(`Email`,`Password`,`Keyword`,`Nick`,`Roles`,`Name`,`Surname`,`Gender`,`BirthDate`,`IdentityNumber`,`Occupation`,`Company`,`Department`,`PhoneCell`,`PhoneWork`,`PhoneHome`,`AddressLine1`,`AddressLine2`,`City`,`Country`,`ZipCode`,`Web`,`Education`,`Certificates`,`About`,`Avatar`,`RedirectCount`,`Id`,`InsertDate`,`InsertUserId`,`UpdateDate`,`UpdateUserId`,`Visible`,`OrderNo`) values ('root@local','63A9F0EA7BB98050','jhrd74ghe63','Admin','User,Editor,Designer',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,1,'1990-01-01 00:00:00',NULL,NULL,NULL,1,NULL),('editor','63A9F0EA7BB98050','ge548rhe46e','Editör','User,Editor',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,2,'1990-01-01 00:00:00',NULL,NULL,NULL,1,NULL),('anonim','','63beyte674hge','Anonim','',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,3,'1990-01-01 00:00:00',NULL,NULL,NULL,1,NULL);

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
