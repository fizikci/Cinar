/*
SQLyog Trial v9.30 
MySQL - 5.5.9 : Database - blank
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
USE `blank`;

/*Table structure for table `author` */

DROP TABLE IF EXISTS `author`;

CREATE TABLE `author` (
  `DisableAutoContent` tinyint(1) NOT NULL DEFAULT '0',
  `UserId` int(11) DEFAULT NULL,
  `Name` varchar(100) COLLATE utf8_turkish_ci NOT NULL,
  `Description` text COLLATE utf8_turkish_ci,
  `Picture` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `InsertDate` datetime NOT NULL DEFAULT '1990-01-01 00:00:00',
  `InsertUserId` int(11) DEFAULT NULL,
  `UpdateDate` datetime DEFAULT NULL,
  `UpdateUserId` int(11) DEFAULT NULL,
  `Visible` tinyint(1) NOT NULL DEFAULT '1',
  `OrderNo` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;

/*Data for the table `author` */

insert  into `author`(`DisableAutoContent`,`UserId`,`Name`,`Description`,`Picture`,`Id`,`InsertDate`,`InsertUserId`,`UpdateDate`,`UpdateUserId`,`Visible`,`OrderNo`) values (0,NULL,'Editorial',NULL,NULL,1,'1990-01-01 00:00:00',NULL,NULL,NULL,1,NULL),(1,4,'Tanla','','/UserFiles/Images/_author/tanla.jpg',2,'2011-10-26 20:18:54',1,'2011-10-26 20:28:57',1,1,0),(1,7,'Bahar','','/UserFiles/Images/_author/bahar.jpg',3,'2011-10-26 21:30:11',1,'2011-10-26 21:30:11',1,1,0),(1,8,'Deniz','','/UserFiles/Images/_author/deniz.jpg',4,'2011-10-26 22:52:10',1,'2011-10-26 23:03:21',1,1,0);

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
  `CountTags` tinyint(1) DEFAULT NULL,
  `UseHTMLEditor` tinyint(1) DEFAULT NULL,
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
  `Visible` int(11) NOT NULL DEFAULT '1',
  `OrderNo` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;

/*Data for the table `configuration` */

insert  into `configuration`(`SiteName`,`SiteAddress`,`SiteDescription`,`SiteKeywords`,`SiteLogo`,`SiteIcon`,`SessionTimeout`,`BufferOutput`,`MultiLang`,`DefaultLang`,`DefaultStyleSheet`,`CountTags`,`UseHTMLEditor`,`DefaultDateFormat`,`NoPicture`,`ThumbQuality`,`ImageUploadMaxWidth`,`AuthEmail`,`MailHost`,`MailUsername`,`MailPassword`,`UseCache`,`CacheLifeTime`,`MainPage`,`CategoryPage`,`ContentPage`,`LoginPage`,`MembershipFormPage`,`MembershipProfilePage`,`RememberPasswordFormPage`,`UserActivationPage`,`AdminPage`,`Id`,`InsertDate`,`InsertUserId`,`UpdateDate`,`UpdateUserId`,`Visible`,`OrderNo`) values ('isimsiz.com','www.isimsiz.com','Sitenin tanımı, bu site ne hakkındadır?','sitenizle,ilgili,anahtar,kelimleri,böyle,yazınız','','',120,1,0,1,'body {\n	font-family:Verdana;\n	font-size:12px;\n	text-align:center;\n}\ntd {\n	font-size:12px;\n}\na {\n	color: #9c9c9c;\n	text-decoration:none\n}\na:hoven	color: #0687d5;\n}\n\nDIV {\n	-moz-box-sizing:border-box;\n	box-sizing:border-box;\n	margin:0;\n	padding:0;\n}\n\n#page {\n	width: 984px; \n	height: 100%; \n	margin-top:10px; \n	margin-right:auto;\n	margin-left:auto;\n	background: white;\n}\n#Header {text-align:center}\n#ContentLeft {text-align:left; float:left; width:240px;min-height:400px;margin-right:10px;}\n#Content {text-align:left; float:left; width:484px;}\n#ContentRight {text-align:left; float:right; width:240px;margin-left:10px;}\n#Footer {text-align:left; clear:both;}\n\n\n\n.vitrin {\n	background: #ECE8DC;\n	padding: 13px;\n	margin-top: 10px;\n}\n.vitrin div.clAuthor, .vitrin div.clCategory {\n	width: 160px;\n	height: 20px;\n	padding-top: 5px;\n	background: url(/UserFiles/Images/design/iam_bg.png);\n	padding-left: 40px;\n	font-family: Buxton Sketch, script, Arial;\n	font-size: 18px;\n}\n.sagda {float:right}\n.solda {float:left}',1,0,'dd MMMM yyyy','',90,640,'info@isimsiz.com','mail.isimsiz.com','','','False',15,'Main.aspx','Category.aspx','Content.aspx','Login.aspx','Membership.aspx','Profile.aspx','RememberPassword.aspx','Activation.aspx','Admin.aspx',1,'2011-10-26 11:00:00',3,'2011-10-26 23:42:13',1,1,0);

/*Table structure for table `content` */

DROP TABLE IF EXISTS `content`;

CREATE TABLE `content` (
  `ClassName` varchar(100) COLLATE utf8_turkish_ci NOT NULL DEFAULT 'Content',
  `Title` varchar(200) COLLATE utf8_turkish_ci NOT NULL,
  `Description` text COLLATE utf8_turkish_ci,
  `Keywords` text COLLATE utf8_turkish_ci,
  `Metin` text COLLATE utf8_turkish_ci,
  `Hierarchy` varchar(100) COLLATE utf8_turkish_ci NOT NULL,
  `AuthorId` int(11) DEFAULT NULL,
  `SourceId` int(11) DEFAULT NULL,
  `PublishDate` datetime NOT NULL DEFAULT '1990-01-01 00:00:00',
  `Picture` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `Tags` varchar(300) COLLATE utf8_turkish_ci DEFAULT NULL,
  `TagRanks` varchar(300) COLLATE utf8_turkish_ci DEFAULT NULL,
  `CategoryId` int(11) NOT NULL DEFAULT '1',
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
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `InsertDate` datetime NOT NULL DEFAULT '1990-01-01 00:00:00',
  `InsertUserId` int(11) DEFAULT NULL,
  `UpdateDate` datetime DEFAULT NULL,
  `UpdateUserId` int(11) DEFAULT NULL,
  `Visible` tinyint(1) NOT NULL DEFAULT '1',
  `OrderNo` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=29 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;

/*Data for the table `content` */

insert  into `content`(`ClassName`,`Title`,`Description`,`Keywords`,`Metin`,`Hierarchy`,`AuthorId`,`SourceId`,`PublishDate`,`Picture`,`Tags`,`TagRanks`,`CategoryId`,`ShowInPage`,`ShowContentsInPage`,`ShowCategoriesInPage`,`IsManset`,`SpotTitle`,`ContentSourceId`,`SourceLink`,`ViewCount`,`CommentCount`,`RecommendCount`,`Id`,`InsertDate`,`InsertUserId`,`UpdateDate`,`UpdateUserId`,`Visible`,`OrderNo`) values ('Category','Kök',NULL,NULL,NULL,'',NULL,NULL,'1980-01-01 00:00:00',NULL,NULL,NULL,0,NULL,NULL,NULL,0,NULL,NULL,NULL,0,0,0,1,'2011-10-26 11:00:00',NULL,'2011-10-26 17:18:00',NULL,1,NULL),('Category','Foto','','','','00001',1,1,'2011-10-17 09:00:00','','','',1,'','','',0,'',0,'',0,0,0,2,'2011-10-26 17:18:00',1,'2011-10-26 17:18:00',1,1,0),('Category','City','','','','00001',1,1,'2011-10-26 17:18:00','','','',1,'','','',0,'',0,'',0,0,0,3,'2011-10-26 17:18:00',1,'2011-10-26 17:18:00',1,1,0),('Category','Hate','','','','00001',1,1,'2011-10-26 17:18:00','','','',1,'','','',0,'',0,'',0,0,0,4,'2011-10-26 17:18:00',1,'2011-10-26 17:18:00',1,1,0),('Category','Love','','','','00001',1,1,'2011-10-26 17:18:00','','','',1,'','','',0,'',0,'',0,0,0,5,'2011-10-26 17:18:00',1,'2011-10-26 17:18:00',1,1,0),('Category','Moda','','','','00001',1,1,'2011-10-26 17:18:00','','','',1,'','','',0,'',0,'',0,0,0,6,'2011-10-26 17:19:00',1,'2011-10-26 17:19:00',1,1,0),('Category','People','','','','00001',1,1,'2011-10-26 17:19:00','','','',1,'','','',0,'',0,'',0,0,0,7,'2011-10-26 17:19:00',1,'2011-10-26 17:19:00',1,1,0),('Category','Author','','','','00001',1,1,'2011-10-26 17:19:00','','','',1,'','','',0,'',0,'',0,0,0,8,'2011-10-26 17:19:00',1,'2011-10-26 17:19:00',1,1,0),('Category','Bird View','','','','00001',1,1,'2011-10-26 17:19:00','','','',1,'','','',0,'',0,'',0,0,0,9,'2011-10-26 17:19:00',1,'2011-10-26 17:19:00',1,1,0),('Category','Cook','','','','00001',1,1,'2011-10-26 17:19:00','','','',1,'','','',0,'',0,'',0,0,0,10,'2011-10-26 17:20:00',1,'2011-10-26 17:20:00',1,1,0),('Content','Tanla\'nın ilk yazısı','','','','00001,00008',2,1,'2011-10-26 20:19:23','','','',8,'','','',0,'',0,'',0,0,0,11,'2011-10-26 20:20:16',1,'2011-10-26 21:31:37',1,1,0),('Content','İstanbul','','','','00001,00003',1,1,'2011-10-26 21:16:28','/UserFiles/Images/city/city.jpg','','',3,'','','',0,'',0,'',0,0,0,12,'2011-10-26 21:17:26',1,'2011-10-26 21:17:26',1,1,0),('Content','a bird view','','','','00001,00009',1,1,'2011-10-26 21:23:31','/UserFiles/Images/birdview/birdview.jpg','','',9,'','','',0,'',0,'',0,0,0,13,'2011-10-26 21:24:16',1,'2011-10-26 21:24:16',1,1,0),('Content','Bahar\'ın ilk yazısı','','','','00001,00008',3,1,'2011-10-26 21:30:45','','','',8,'','','',0,'',0,'',0,0,0,14,'2011-10-26 21:31:16',1,'2011-10-26 21:31:16',1,1,0),('Category','Room','','','','00001',1,1,'2011-10-26 21:55:36','','','',1,'','','',0,'',0,'',0,0,0,15,'2011-10-26 21:55:33',1,'2011-10-26 21:56:42',1,1,0),('Content','a room','','','','00001,00015',1,1,'2011-10-26 21:56:26','/UserFiles/Images/room/room.jpg','','',15,'','','',0,'',0,'',0,0,0,16,'2011-10-26 21:56:32',1,'2011-10-26 22:09:53',1,1,0),('Content','a cook','','','','00001,00010',1,1,'2011-10-26 22:12:36','/UserFiles/Images/cook/cook.jpg','','',10,'','','',0,'',0,'',0,0,0,17,'2011-10-26 22:12:28',1,'2011-10-26 22:12:54',1,1,0),('Content','Deniz\'in ilk yazısı','','','','00001,00008',4,1,'2011-10-26 23:04:18','','','',8,'','','',0,'',0,'',0,0,0,18,'2011-10-26 23:05:20',1,'2011-10-26 23:05:20',1,1,0),('Content','a people','','','','00001,00007',1,1,'2011-10-26 23:09:26','/UserFiles/Images/people/people.jpg','','',7,'','','',0,'',0,'',0,0,0,19,'2011-10-26 23:10:23',1,'2011-10-26 23:10:23',1,1,0),('Category','Music','','','','00001',1,1,'2011-10-26 23:12:25','','','',1,'','','',0,'',0,'',0,0,0,20,'2011-10-26 23:12:42',1,'2011-10-26 23:12:42',1,1,0),('Content','a music','','','','00001,00020',1,1,'2011-10-26 23:12:42','/UserFiles/Images/music/music.jpg','','',20,'','','',0,'',0,'',0,0,0,21,'2011-10-26 23:13:38',1,'2011-10-26 23:13:38',1,1,0),('Category','Words','','','','00001',1,1,'2011-10-26 23:17:30','','','',1,'','','',0,'',0,'',0,0,0,22,'2011-10-26 23:17:44',1,'2011-10-26 23:17:44',1,1,0),('Content','a words','','','','00001,00022',1,1,'2011-10-26 23:17:45','/UserFiles/Images/words/words.jpg','','',22,'','','',0,'',0,'',0,0,0,23,'2011-10-26 23:18:23',1,'2011-10-26 23:18:23',1,1,0),('Category','Beauty','','','','00001',1,1,'2011-10-26 23:21:01','','','',1,'','','',0,'',0,'',0,0,0,24,'2011-10-26 23:21:14',1,'2011-10-26 23:21:14',1,1,0),('Content','a beauty','','','','00001,00024',1,1,'2011-10-26 23:21:15','/UserFiles/Images/beauty/beauty.jpg','','',24,'','','',0,'',0,'',0,0,0,25,'2011-10-26 23:22:19',1,'2011-10-26 23:22:19',1,1,0),('Category','Think','','','','00001',1,1,'2011-10-26 23:34:29','','','',1,'','','',0,'',0,'',0,0,0,26,'2011-10-26 23:34:47',1,'2011-10-26 23:34:47',1,1,0),('Content','a think','','','','00001,00026',1,1,'2011-10-26 23:34:20','/UserFiles/Images/think/think.jpg','','',26,'','','',0,'',0,'',0,0,0,27,'2011-10-26 23:35:01',1,'2011-10-26 23:36:46',1,1,0),('Content','a moda','','','','00001,00006',1,1,'2011-10-26 23:51:07','/UserFiles/Images/moda/moda.jpg','','',6,'','','',0,'',0,'',0,0,0,28,'2011-10-26 23:51:54',1,'2011-10-26 23:52:29',1,1,0);

/*Table structure for table `contentpicture` */

DROP TABLE IF EXISTS `contentpicture`;

CREATE TABLE `contentpicture` (
  `ContentId` int(11) DEFAULT NULL,
  `Title` varchar(200) COLLATE utf8_turkish_ci NOT NULL,
  `Description` varchar(300) COLLATE utf8_turkish_ci DEFAULT NULL,
  `FileName` varchar(100) COLLATE utf8_turkish_ci NOT NULL,
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `InsertDate` datetime NOT NULL DEFAULT '1990-01-01 00:00:00',
  `InsertUserId` int(11) DEFAULT NULL,
  `UpdateDate` datetime DEFAULT NULL,
  `UpdateUserId` int(11) DEFAULT NULL,
  `Visible` tinyint(1) NOT NULL DEFAULT '1',
  `OrderNo` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;

/*Data for the table `contentpicture` */

insert  into `contentpicture`(`ContentId`,`Title`,`Description`,`FileName`,`Id`,`InsertDate`,`InsertUserId`,`UpdateDate`,`UpdateUserId`,`Visible`,`OrderNo`) values (28,'deneme resmi','','/deneme',1,'2011-10-27 17:07:46',1,'2011-10-27 17:07:46',1,1,0),(0,'a think resim 1','','/resim1',2,'2011-10-27 17:08:48',1,'2011-10-27 17:08:48',1,1,0),(0,'a think resim 1','','fdg',3,'2011-10-27 17:15:40',1,'2011-10-27 17:15:40',1,1,0),(27,'a think resim 2','','dfsdf',4,'2011-10-27 17:16:26',1,'2011-10-27 17:16:26',1,1,0);

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

/*Table structure for table `module` */

DROP TABLE IF EXISTS `module`;

CREATE TABLE `module` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Template` varchar(20) COLLATE utf8_turkish_ci NOT NULL,
  `Region` varchar(20) COLLATE utf8_turkish_ci NOT NULL,
  `OrderNo` int(11) NOT NULL DEFAULT '0',
  `Name` varchar(30) COLLATE utf8_turkish_ci NOT NULL,
  `CSS` text COLLATE utf8_turkish_ci,
  `Details` text COLLATE utf8_turkish_ci,
  `TopHtml` text COLLATE utf8_turkish_ci,
  `BottomHtml` text COLLATE utf8_turkish_ci,
  `ParentModuleId` int(11) NOT NULL DEFAULT '0',
  `CSSClass` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `UseCache` varchar(30) COLLATE utf8_turkish_ci NOT NULL DEFAULT 'Default',
  `CacheLifeTime` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;

/*Data for the table `module` */

insert  into `module`(`Id`,`Template`,`Region`,`OrderNo`,`Name`,`CSS`,`Details`,`TopHtml`,`BottomHtml`,`ParentModuleId`,`CSSClass`,`UseCache`,`CacheLifeTime`) values (1,'Main.aspx','Content',8,'LoginForm','#LoginForm_1 {\n	clear:both;\n}\n#LoginForm_1 div.loginEmailLabel {\n}\n#LoginForm_1 input.loginEmailInput {\n}\n#LoginForm_1 div.loginPassLabel {\n}\n#LoginForm_1 input.loginPassInput {\n}\n#LoginForm_1 div.loginRememberMeLabel {\n}\n#LoginForm_1 div.loginError {\n	color:red}\n#LoginForm_1 input.loginSubmitButton {\n	display:block}\n#LoginForm_1 a {\n	display:block;\n	padding-left:10px}\n','Cinar.CMS.Serialization\nRedirect,0,ShowMembershipLink,4,TrueShowMembershipInfoLink,4,TrueShowPasswordForgetLink,4,TrueShowRememberMe,4,TrueShowActivationLink,4,TrueId,1,1Template,9,Main.aspxRegion,7,ContentOrderNo,1,8Name,9,LoginFormCSS,372,#LoginForm_1 {\n	clear:both;\n}\n#LoginForm_1 div.loginEmailLabel {\n}\n#LoginForm_1 input.loginEmailInput {\n}\n#LoginForm_1 div.loginPassLabel {\n}\n#LoginForm_1 input.loginPassInput {\n}\n#LoginForm_1 div.loginRememberMeLabel {\n}\n#LoginForm_1 div.loginError {\n	color:red}\n#LoginForm_1 input.loginSubmitButton {\n	display:block}\n#LoginForm_1 a {\n	display:block;\n	padding-left:10px}\nTopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,'Default',NULL),(2,'Main.aspx','Header',0,'StaticHtml','#StaticHtml_2 {\n}\n\n#StaticHtml_2 .headerMenu { border-bottom:1px solid black}\n#StaticHtml_2 .tab {\n	border:1pxlid black; \n	border-bottom:none;\n	float:right; \n	padding:5px 20px;\n	cursor:pointer;\n	margin-left:10px;\n}\n#StaticHtml_2 .search {\n}\n#StaticHtml_2 .subscribe {\n	background:black;\n	color:white;\n}','Cinar.CMS.Serialization\nInnerHtml,226,<a href=\"/Main.aspx\"><img src=\"/UserFiles/Images/design/logo.jpg\" border=\"0\"/></a>\n\n<div class=\"headerMenu\">\n<div class=\"subscribe tab\">Subscribe</div>\n<div class=\"search tab\">Search</div>\n<div style=\"clear:both\"></div>\n</div>LangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,1,2Template,9,Main.aspxRegion,6,HeaderOrderNo,1,0Name,10,StaticHtmlCSS,302,#StaticHtml_2 {\n}\n\n#StaticHtml_2 .headerMenu { border-bottom:1px solid black}\n#StaticHtml_2 .tab {\n	border:1pxlid black; \n	border-bottom:none;\n	float:right; \n	padding:5px 20px;\n	cursor:pointer;\n	margin-left:10px;\n}\n#StaticHtml_2 .search {\n}\n#StaticHtml_2 .subscribe {\n	background:black;\n	color:white;\n}TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,'Default',NULL),(4,'Main.aspx','Footer',0,'StaticHtml','#StaticHtml_4 {\n	border-top:1px solid black;\n	text-align:center;\n	font-weight:bold;\n	color:#404040;\n	padding:4px;\n	margin-top:10px;\n}\n#StaticHtml_4 a {\n	color:#404040;\n}\n','Cinar.CMS.Serialization\nInnerHtml,134,<a href=\"\">ABOUT US</a> &nbsp; &nbsp; &nbsp;\n<a href=\"\">CONTACT US</a> &nbsp; &nbsp; &nbsp;\n<a href=\"\">(C) 2011 BLANK-MAGAZINE.COM</a>LangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,1,4Template,9,Main.aspxRegion,6,FooterOrderNo,1,0Name,10,StaticHtmlCSS,170,#StaticHtml_4 {\n	border-top:1px solid black;\n	text-align:center;\n	font-weight:bold;\n	color:#404040;\n	padding:4px;\n	margin-top:10px;\n}\n#StaticHtml_4 a {\n	color:#404040;\n}\nTopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,'Default',NULL),(5,'Main.aspx','ContentLeft',0,'ContentListByFilter','','Cinar.CMS.Serialization\nFilter,10,AuthorId=2HowManyItems,1,1SkipFirst,1,0Random,1,0OrderBy,19,Content.PublishDateAscending,5,FalseShowPictureLeftRight,5,FalseShowCurrentContent,5,FalseShowFirstItemWithPicture,5,FalseWhichPicture,17,TAuthorId.PicturePictureWidth,3,214PictureHeight,3,266BulletIcon,0,TitleLength,2,50DescriptionLength,3,150DateFormat,12,dd MMMM yyyyFieldOrder,12,image,authorCreateLink,4,TrueLinkTarget,0,ShowDescriptionAsLink,5,FalseUseTemplate,0,ForceToUseTemplate,5,FalseMoreLink,0,DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,1,5Template,9,Main.aspxRegion,11,ContentLeftOrderNo,1,0Name,19,ContentListByFilterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,12,vitrin soldaUseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,'Default',NULL),(6,'Main.aspx','ContentLeft',1,'ContentListByFilter','','Cinar.CMS.Serialization\nFilter,12,CategoryId=3HowManyItems,1,1SkipFirst,1,0Random,1,0OrderBy,19,Content.PublishDateAscending,5,FalseShowPictureLeftRight,5,FalseShowCurrentContent,5,FalseShowFirstItemWithPicture,5,FalseWhichPicture,15,Content.PicturePictureWidth,3,212PictureHeight,3,244BulletIcon,0,TitleLength,2,50DescriptionLength,3,150DateFormat,12,dd MMMM yyyyFieldOrder,14,image,categoryCreateLink,4,TrueLinkTarget,0,ShowDescriptionAsLink,5,FalseUseTemplate,0,ForceToUseTemplate,5,FalseMoreLink,0,DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,1,6Template,9,Main.aspxRegion,11,ContentLeftOrderNo,1,1Name,19,ContentListByFilterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,12,vitrin soldaUseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,'Default',NULL),(7,'Main.aspx','ContentLeft',2,'ContentListByFilter','','Cinar.CMS.Serialization\nFilter,12,CategoryId=9HowManyItems,1,1SkipFirst,1,0Random,1,0OrderBy,19,Content.PublishDateAscending,5,FalseShowPictureLeftRight,5,FalseShowCurrentContent,5,FalseShowFirstItemWithPicture,5,FalseWhichPicture,15,Content.PicturePictureWidth,3,214PictureHeight,3,266BulletIcon,0,TitleLength,2,50DescriptionLength,3,150DateFormat,12,dd MMMM yyyyFieldOrder,14,image,categoryCreateLink,4,TrueLinkTarget,0,ShowDescriptionAsLink,5,FalseUseTemplate,0,ForceToUseTemplate,5,FalseMoreLink,0,DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,1,7Template,9,Main.aspxRegion,11,ContentLeftOrderNo,1,2Name,19,ContentListByFilterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,12,vitrin soldaUseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,'Default',NULL),(8,'Main.aspx','ContentRight',0,'ContentListByFilter','','Cinar.CMS.Serialization\nFilter,10,AuthorId=3HowManyItems,1,1SkipFirst,1,0Random,1,0OrderBy,19,Content.PublishDateAscending,5,FalseShowPictureLeftRight,5,FalseShowCurrentContent,5,FalseShowFirstItemWithPicture,5,FalseWhichPicture,17,TAuthorId.PicturePictureWidth,3,214PictureHeight,3,266BulletIcon,0,TitleLength,2,50DescriptionLength,3,150DateFormat,12,dd MMMM yyyyFieldOrder,12,image,authorCreateLink,4,TrueLinkTarget,0,ShowDescriptionAsLink,5,FalseUseTemplate,0,ForceToUseTemplate,5,FalseMoreLink,0,DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,1,8Template,9,Main.aspxRegion,12,ContentRightOrderNo,1,0Name,19,ContentListByFilterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,12,vitrin sagdaUseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,'Default',NULL),(9,'Main.aspx','ContentRight',1,'ContentListByFilter','','Cinar.CMS.Serialization\nFilter,13,CategoryId=15HowManyItems,1,1SkipFirst,1,0Random,1,0OrderBy,19,Content.PublishDateAscending,5,FalseShowPictureLeftRight,5,FalseShowCurrentContent,5,FalseShowFirstItemWithPicture,5,FalseWhichPicture,15,Content.PicturePictureWidth,3,212PictureHeight,3,244BulletIcon,0,TitleLength,2,50DescriptionLength,3,150DateFormat,12,dd MMMM yyyyFieldOrder,14,image,categoryCreateLink,4,TrueLinkTarget,0,ShowDescriptionAsLink,5,FalseUseTemplate,0,ForceToUseTemplate,5,FalseMoreLink,0,DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,1,9Template,9,Main.aspxRegion,12,ContentRightOrderNo,1,1Name,19,ContentListByFilterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,12,vitrin sagdaUseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,'Default',NULL),(10,'Main.aspx','ContentRight',2,'ContentListByFilter','','Cinar.CMS.Serialization\nFilter,13,CategoryId=10HowManyItems,1,1SkipFirst,1,0Random,1,0OrderBy,19,Content.PublishDateAscending,5,FalseShowPictureLeftRight,5,FalseShowCurrentContent,5,FalseShowFirstItemWithPicture,5,FalseWhichPicture,15,Content.PicturePictureWidth,3,212PictureHeight,3,244BulletIcon,0,TitleLength,2,50DescriptionLength,3,150DateFormat,12,dd MMMM yyyyFieldOrder,14,image,categoryCreateLink,4,TrueLinkTarget,0,ShowDescriptionAsLink,5,FalseUseTemplate,0,ForceToUseTemplate,5,FalseMoreLink,0,DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,2,10Template,9,Main.aspxRegion,12,ContentRightOrderNo,1,2Name,19,ContentListByFilterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,12,vitrin sagdaUseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,'Default',NULL),(11,'Main.aspx','Content',2,'ContentListByFilter','','Cinar.CMS.Serialization\nFilter,10,AuthorId=4HowManyItems,1,1SkipFirst,1,0Random,1,0OrderBy,19,Content.PublishDateAscending,5,FalseShowPictureLeftRight,5,FalseShowCurrentContent,5,FalseShowFirstItemWithPicture,5,FalseWhichPicture,17,TAuthorId.PicturePictureWidth,3,211PictureHeight,3,304BulletIcon,0,TitleLength,2,50DescriptionLength,3,150DateFormat,12,dd MMMM yyyyFieldOrder,12,image,authorCreateLink,4,TrueLinkTarget,0,ShowDescriptionAsLink,5,FalseUseTemplate,0,ForceToUseTemplate,5,FalseMoreLink,0,DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,2,11Template,9,Main.aspxRegion,7,ContentOrderNo,1,2Name,19,ContentListByFilterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,12,vitrin soldaUseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,'Default',NULL),(12,'Main.aspx','Content',3,'ContentListByFilter','','Cinar.CMS.Serialization\nFilter,12,CategoryId=7HowManyItems,1,1SkipFirst,1,0Random,1,0OrderBy,19,Content.PublishDateAscending,5,FalseShowPictureLeftRight,5,FalseShowCurrentContent,5,FalseShowFirstItemWithPicture,5,FalseWhichPicture,15,Content.PicturePictureWidth,3,211PictureHeight,3,236BulletIcon,0,TitleLength,2,50DescriptionLength,3,150DateFormat,12,dd MMMM yyyyFieldOrder,14,image,categoryCreateLink,4,TrueLinkTarget,0,ShowDescriptionAsLink,5,FalseUseTemplate,0,ForceToUseTemplate,5,FalseMoreLink,0,DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,2,12Template,9,Main.aspxRegion,7,ContentOrderNo,1,3Name,19,ContentListByFilterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,12,vitrin sagdaUseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,'Default',NULL),(13,'Main.aspx','Content',5,'ContentListByFilter','','Cinar.CMS.Serialization\nFilter,13,CategoryId=20HowManyItems,1,1SkipFirst,1,0Random,1,0OrderBy,19,Content.PublishDateAscending,5,FalseShowPictureLeftRight,5,FalseShowCurrentContent,5,FalseShowFirstItemWithPicture,5,FalseWhichPicture,15,Content.PicturePictureWidth,3,211PictureHeight,3,236BulletIcon,0,TitleLength,2,50DescriptionLength,3,150DateFormat,12,dd MMMM yyyyFieldOrder,14,image,categoryCreateLink,4,TrueLinkTarget,0,ShowDescriptionAsLink,5,FalseUseTemplate,0,ForceToUseTemplate,5,FalseMoreLink,0,DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,2,13Template,9,Main.aspxRegion,7,ContentOrderNo,1,4Name,19,ContentListByFilterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,12,vitrin soldaUseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,'Default',NULL),(14,'Main.aspx','Content',4,'ContentListByFilter','','Cinar.CMS.Serialization\nFilter,13,CategoryId=22HowManyItems,1,1SkipFirst,1,0Random,1,0OrderBy,19,Content.PublishDateAscending,5,FalseShowPictureLeftRight,5,FalseShowCurrentContent,5,FalseShowFirstItemWithPicture,5,FalseWhichPicture,15,Content.PicturePictureWidth,3,211PictureHeight,3,205BulletIcon,0,TitleLength,2,50DescriptionLength,3,150DateFormat,12,dd MMMM yyyyFieldOrder,14,image,categoryCreateLink,4,TrueLinkTarget,0,ShowDescriptionAsLink,5,FalseUseTemplate,0,ForceToUseTemplate,5,FalseMoreLink,0,DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,2,14Template,9,Main.aspxRegion,7,ContentOrderNo,1,5Name,19,ContentListByFilterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,12,vitrin sagdaUseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,'Default',NULL),(15,'Main.aspx','Content',6,'ContentListByFilter','','Cinar.CMS.Serialization\nFilter,13,CategoryId=24HowManyItems,1,1SkipFirst,1,0Random,1,0OrderBy,19,Content.PublishDateAscending,5,FalseShowPictureLeftRight,5,FalseShowCurrentContent,5,FalseShowFirstItemWithPicture,5,FalseWhichPicture,15,Content.PicturePictureWidth,3,211PictureHeight,3,215BulletIcon,0,TitleLength,2,50DescriptionLength,3,150DateFormat,12,dd MMMM yyyyFieldOrder,14,image,categoryCreateLink,4,TrueLinkTarget,0,ShowDescriptionAsLink,5,FalseUseTemplate,0,ForceToUseTemplate,5,FalseMoreLink,0,DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,2,15Template,9,Main.aspxRegion,7,ContentOrderNo,1,6Name,19,ContentListByFilterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,12,vitrin sagdaUseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,'Default',NULL),(16,'Main.aspx','Content',7,'ContentListByFilter','','Cinar.CMS.Serialization\nFilter,13,CategoryId=26HowManyItems,1,1SkipFirst,1,0Random,1,0OrderBy,19,Content.PublishDateAscending,5,FalseShowPictureLeftRight,5,FalseShowCurrentContent,5,FalseShowFirstItemWithPicture,5,FalseWhichPicture,15,Content.PicturePictureWidth,3,211PictureHeight,3,241BulletIcon,0,TitleLength,2,50DescriptionLength,3,150DateFormat,12,dd MMMM yyyyFieldOrder,14,image,categoryCreateLink,4,TrueLinkTarget,0,ShowDescriptionAsLink,5,FalseUseTemplate,0,ForceToUseTemplate,5,FalseMoreLink,0,DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,2,16Template,9,Main.aspxRegion,7,ContentOrderNo,1,7Name,19,ContentListByFilterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,12,vitrin soldaUseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,'Default',NULL),(17,'Main.aspx','Content',1,'ContentListByFilter','#ContentListByFilter_17 {\n	margin-top:10px;\n}\n','Cinar.CMS.Serialization\nFilter,12,CategoryId=6HowManyItems,1,1SkipFirst,1,0Random,1,0OrderBy,19,Content.PublishDateAscending,5,FalseShowPictureLeftRight,5,FalseShowCurrentContent,5,FalseShowFirstItemWithPicture,5,FalseWhichPicture,15,Content.PicturePictureWidth,3,480PictureHeight,1,0BulletIcon,0,TitleLength,2,50DescriptionLength,3,150DateFormat,12,dd MMMM yyyyFieldOrder,5,imageCreateLink,4,TrueLinkTarget,0,ShowDescriptionAsLink,5,FalseUseTemplate,0,ForceToUseTemplate,5,FalseMoreLink,0,DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,2,17Template,9,Main.aspxRegion,7,ContentOrderNo,1,1Name,19,ContentListByFilterCSS,46,#ContentListByFilter_17 {\n	margin-top:10px;\n}\nTopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,'Default',NULL);

/*Table structure for table `source` */

DROP TABLE IF EXISTS `source`;

CREATE TABLE `source` (
  `UserId` int(11) DEFAULT NULL,
  `Name` varchar(100) COLLATE utf8_turkish_ci NOT NULL,
  `Description` text COLLATE utf8_turkish_ci,
  `Picture` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
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

insert  into `source`(`UserId`,`Name`,`Description`,`Picture`,`Id`,`InsertDate`,`InsertUserId`,`UpdateDate`,`UpdateUserId`,`Visible`,`OrderNo`) values (NULL,'Editorial',NULL,NULL,1,'1990-01-01 00:00:00',NULL,NULL,NULL,1,NULL);

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

insert  into `template`(`FileName`,`HTMLCode`,`Id`,`InsertDate`,`InsertUserId`,`UpdateDate`,`UpdateUserId`,`Visible`,`OrderNo`) values ('Main.aspx','<html>\n<head>\n$=this.HeadSection$\n</head>\n<body>\n    <div id=\"page\">\n        <div id=\"Header\" class=\"Region Header\">$=this.Header$</div>\n\n		<div id=\"ContentLeft\" class=\"Region ContentLeft\">$=this.ContentLeft$</div>\n		<div id=\"Content\" class=\"Region Content\">$=this.Content$</div>\n		<div id=\"ContentRight\" class=\"Region ContentRight\">$=this.ContentRight$</div>\n		<div style=\"clear:both\"></div>\n\n        <div id=\"Footer\" class=\"Region Footer\">$=this.Footer$</div>\n    </div>\n</body>\n</html>',1,'1990-01-01 00:00:00',0,'2011-10-26 15:35:00',1,1,0);

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
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;

/*Data for the table `user` */

insert  into `user`(`Email`,`Password`,`Keyword`,`Nick`,`Roles`,`Name`,`Surname`,`Gender`,`Occupation`,`Company`,`Department`,`PhoneCell`,`PhoneWork`,`PhoneHome`,`AddressLine1`,`AddressLine2`,`City`,`Country`,`ZipCode`,`Web`,`Education`,`Certificates`,`About`,`Avatar`,`RedirectCount`,`Id`,`InsertDate`,`InsertUserId`,`UpdateDate`,`UpdateUserId`,`Visible`,`OrderNo`) values ('root@local','63A9F0EA7BB98050','jhrd74ghe63','Admin','User,Editor,Designer',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,1,'1990-01-01 00:00:00',NULL,NULL,NULL,1,NULL),('editor','63A9F0EA7BB98050','ge548rhe46e','Editör','User,Editor',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,2,'1990-01-01 00:00:00',NULL,NULL,NULL,1,NULL),('anonim','','63beyte674hge','Anonim','',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,3,'1990-01-01 00:00:00',NULL,NULL,NULL,1,NULL),('tanla@blank-magazine.com','','EA43183197BD2AE5','tanla','User','Tanla','','','','','','','','','','','','','','','','','','',0,4,'2011-10-26 20:10:46',1,'2011-10-26 22:51:44',1,1,0),('bahar@blank-magazine.com','','1224702C19A12CD7','bahar','User','Bahar','','','','','','','','','','','','','','','','','','',0,7,'2011-10-26 21:27:49',1,'2011-10-26 22:51:36',1,1,0),('deniz@blank-magazine.com','','2C27E24C0E9FC51C','deniz','User','Deniz','','','','','','','','','','','','','','','','','','',0,8,'2011-10-26 22:49:23',1,'2011-10-26 22:51:28',1,1,0);

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
