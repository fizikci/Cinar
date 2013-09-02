-- MySQL dump 10.13  Distrib 5.1.68, for Win64 (unknown)
--
-- Host: 127.0.0.1    Database: cinarcms
-- ------------------------------------------------------
-- Server version	5.1.68-community

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `author`
--

DROP TABLE IF EXISTS `author`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `author` (
  `Title` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
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
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `author`
--

LOCK TABLES `author` WRITE;
/*!40000 ALTER TABLE `author` DISABLE KEYS */;
INSERT INTO `author` VALUES (NULL,0,NULL,'Editorial',NULL,NULL,NULL,1,'1990-01-01 00:00:00',NULL,NULL,NULL,1,NULL);
/*!40000 ALTER TABLE `author` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `configuration`
--

DROP TABLE IF EXISTS `configuration`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `configuration` (
  `SiteName` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `SiteAddress` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `SiteDescription` text COLLATE utf8_turkish_ci,
  `SiteKeywords` text COLLATE utf8_turkish_ci,
  `SiteLogo` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `SiteIcon` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `UseExternalLibrary` varchar(30) COLLATE utf8_turkish_ci NOT NULL DEFAULT 'False',
  `SessionTimeout` int(11) DEFAULT NULL,
  `BufferOutput` tinyint(1) DEFAULT NULL,
  `MultiLang` tinyint(1) DEFAULT NULL,
  `DefaultLang` int(11) NOT NULL,
  `LogHit` tinyint(1) DEFAULT NULL,
  `DefaultStyleSheet` text COLLATE utf8_turkish_ci,
  `DefaultJavascript` text COLLATE utf8_turkish_ci,
  `DefaultPageLoadScript` text COLLATE utf8_turkish_ci,
  `Routes` text COLLATE utf8_turkish_ci,
  `CountTags` tinyint(1) DEFAULT NULL,
  `DefaultDateFormat` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `NoPicture` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `ThumbQuality` int(11) DEFAULT NULL,
  `ImageUploadMaxWidth` int(11) DEFAULT NULL,
  `AuthEmail` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `MailHost` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `MailPort` int(11) DEFAULT NULL,
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
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `configuration`
--

LOCK TABLES `configuration` WRITE;
/*!40000 ALTER TABLE `configuration` DISABLE KEYS */;
INSERT INTO `configuration` VALUES ('Merhaba Dünya','www.example.com','This is sample web site developed with Cinar CMS','cinar, cms, for developers, dotnet, html, javascript, bootstrap, jquery','','','Bootstrap + jQuery',120,1,1,1,0,'/* Move down content because we have a fixed navbar that is 50px tall */\r\nbody {\r\n  padding-top: 50px;\r\n  padding-bottom: 20px;\r\n}\r\n\r\n/* Set widths on the navbar form inputs since otherwise they\'re 100% wide */\r\n.navbar-form input`type=\"text\"`,\r\n.navbar-form input`type=\"password\"` {\r\n  width: 93px;\r\n}\r\n\r\n.navbar-form {\r\n    margin-top: 13px;\r\n}\r\n\r\n.navbar-form .form-control {\r\n    height: 22px;\r\n}\r\n\r\n.navbar .btn-small {\r\n    padding: 1px 10px;\r\n}\r\n\r\n/* Wrapping element */\r\n/* Set some basic padding to keep content from hitting the edges */\r\n#content {\r\n  padding-left: 15px;\r\n  padding-right: 15px;\r\n}\r\n\r\n/* Responsive: Portrait tablets and up */\r\n@media screen and (min-width: 768px) {\r\n  /* Let the jumbotron breathe */\r\n  .jumbotron {\r\n    margin-top: 20px;\r\n  }\r\n  /* Remove padding from wrapping element since we kick in the grid classes here */\r\n  #content {\r\n    padding: 0;\r\n  }\r\n}','','','',1,'dd MMMM yyyy','',90,960,'info@example.com','mail.example.com',25,'','','False',15,'Default.aspx','Category.aspx','Content.aspx','Login.aspx','Membership.aspx','Profile.aspx','RememberPassword.aspx','Activation.aspx','Admin.aspx',1,'2013-08-05 22:29:00',2,'2013-08-29 00:27:31',1,1,0);
/*!40000 ALTER TABLE `configuration` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `content`
--

DROP TABLE IF EXISTS `content`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `content` (
  `Keyword` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
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
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `content`
--

LOCK TABLES `content` WRITE;
/*!40000 ALTER TABLE `content` DISABLE KEYS */;
INSERT INTO `content` VALUES (NULL,'Category',0,'Kök',NULL,NULL,'1990-01-01 00:00:00',NULL,NULL,NULL,'',NULL,NULL,NULL,NULL,NULL,NULL,NULL,0,NULL,NULL,NULL,0,0,0,0,1,'1990-01-01 00:00:00',NULL,NULL,NULL,1,NULL),('ba4f14c8-6219-4894-ab3b-3d5e728b6ddc','Category',1,'Ürünler','','','2006-08-03 01:32:00','','','','00001',1,1,'','','','','',0,'',0,'',0,0,0,0,2,'2013-08-06 01:33:00',1,'2013-08-06 01:33:00',1,1,0),('bab0ac25-185b-40d9-9446-ed849373cdb3','Category',1,'Hizmetler','','','2006-08-03 01:33:00','','','','00001',1,1,'','','','','',0,'',0,'',0,0,0,0,3,'2013-08-06 01:34:00',1,'2013-08-06 01:34:00',1,1,0),('4fbfa145-9ad9-4c57-b493-fcb350a31b4d','Content',1,'Hakkımızda','','','2003-08-27 01:34:00','','','','00001',1,1,'','','','','',0,'',0,'',0,0,0,0,4,'2013-08-06 01:34:00',1,'2013-08-06 03:00:00',1,1,0),('5a622fd5-6d65-450c-98a7-7ae72b7f1487','Content',1,'İletişim','','','2003-08-27 01:34:00','','','','00001',1,1,'','','','','',0,'',0,'',0,0,0,0,5,'2013-08-06 01:34:00',1,'2013-08-06 03:00:00',1,1,0),('13a2d432-bd84-4b90-9500-8fb410ddc889','Content',2,'Ürün 1','','','2003-08-27 17:44:51','/UserFiles/body_bg3.jpg','','','00001,00002',1,1,'','','','','',0,'',0,'',0,0,0,0,6,'2013-08-06 17:44:00',1,'2013-09-02 16:10:57',1,1,0),('0cca40c8-9a31-4f53-8671-6826c1741405','Content',2,'Ürün 2','','','2006-08-03 17:44:04','/UserFiles/6.jpg','','','00001,00002',1,1,'','','','','',0,'',0,'',0,0,0,0,7,'2013-08-06 17:45:00',1,'2013-09-02 16:10:37',1,1,0),('72966369-d084-4be1-b50d-3749bb9c4654','Content',3,'Hizmet 1','','','2013-08-04 00:00:00','','','','00001,00003',1,1,'','','','','',0,'',0,'',0,0,0,0,8,'2013-08-06 17:45:00',1,'2013-08-30 16:51:34',1,1,0),('a524cde2-fff8-453d-aa6c-87e9581d95d6','Content',3,'Hizmet 2','','','2006-08-03 17:45:00','','','','00001,00003',1,1,'','','','','',0,'',0,'',0,0,0,0,9,'2013-08-06 17:45:00',1,'2013-08-06 17:45:00',1,1,0);
/*!40000 ALTER TABLE `content` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `contentsource`
--

DROP TABLE IF EXISTS `contentsource`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `contentsource` (
  `Name` varchar(50) COLLATE utf8_turkish_ci NOT NULL,
  `SourceId` int(11) NOT NULL,
  `CategoryId` int(11) DEFAULT NULL,
  `AuthorId` int(11) DEFAULT NULL,
  `ClassName` varchar(100) COLLATE utf8_turkish_ci NOT NULL,
  `ListPageAddress` varchar(200) COLLATE utf8_turkish_ci NOT NULL,
  `ListRegExp` text COLLATE utf8_turkish_ci,
  `ContentRegExp` text COLLATE utf8_turkish_ci,
  `Encoding` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `FetchFrequency` int(11) NOT NULL DEFAULT '180',
  `LastFetched` datetime DEFAULT NULL,
  `LastFetchTrial` datetime DEFAULT NULL,
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `InsertDate` datetime NOT NULL DEFAULT '1990-01-01 00:00:00',
  `InsertUserId` int(11) DEFAULT NULL,
  `UpdateDate` datetime DEFAULT NULL,
  `UpdateUserId` int(11) DEFAULT NULL,
  `Visible` tinyint(1) NOT NULL DEFAULT '1',
  `OrderNo` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `contentsource`
--

LOCK TABLES `contentsource` WRITE;
/*!40000 ALTER TABLE `contentsource` DISABLE KEYS */;
/*!40000 ALTER TABLE `contentsource` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `lang`
--

DROP TABLE IF EXISTS `lang`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
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
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `lang`
--

LOCK TABLES `lang` WRITE;
/*!40000 ALTER TABLE `lang` DISABLE KEYS */;
INSERT INTO `lang` VALUES ('tr-TR','Türkçe',1,'1990-01-01 00:00:00',NULL,NULL,NULL,1,NULL);
/*!40000 ALTER TABLE `lang` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `module`
--

DROP TABLE IF EXISTS `module`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
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
) ENGINE=InnoDB AUTO_INCREMENT=29 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `module`
--

LOCK TABLES `module` WRITE;
/*!40000 ALTER TABLE `module` DISABLE KEYS */;
INSERT INTO `module` VALUES (1,'Default.aspx','content',6,'StaticHtml','','Cinar.CMS.Serialization\nInnerHtml,1126,  <div class=\"col-lg-4\">\n    <h2>Başlık 1</h2>\n    <p>Donec id elit non mi porta gravida at eget metus. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus. Etiam porta sem malesuada magna mollis euismod. Donec sed odio dui. </p>\n    <p><a class=\"btn btn-default\" href=\"#\">View details &raquo;</a></p>\n  </div>\n  <div class=\"col-lg-4\">\n    <h2>Başlık 2</h2>\n    <p>Donec id elit non mi porta gravida at eget metus. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus. Etiam porta sem malesuada magna mollis euismod. Donec sed odio dui. </p>\n    <p><a class=\"btn btn-default\" href=\"#\">View details &raquo;</a></p>\n </div>\n  <div class=\"col-lg-4\">\n    <h2>Başlık 3</h2>\n    <p>Donec sed odio dui. Cras justo odio, dapibus ac facilisis in, egestas eget quam. Vestibulum id ligula porta felis euismod semper. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus.</p>\n    <p><a class=\"btn btn-default\" href=\"#\">View details &raquo;</a></p>\n  </div>\nLangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,1,1Template,12,Default.aspxRegion,7,contentOrderNo,1,0Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,3,rowVisible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL),(2,'Default.aspx','footer',3,'StaticHtml','','Cinar.CMS.Serialization\nInnerHtml,60,<hr/>\r\n<p>&copy; $=Provider.Configuration.SiteName$ 2013</p>LangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,1,2Template,12,Default.aspxRegion,6,footerOrderNo,1,3Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL),(3,'Default.aspx','topNav',0,'StaticHtml','','Cinar.CMS.Serialization\nInnerHtml,2974,<div class=\"navbar-header\">\n    <button type=\"button\" class=\"navbar-toggle\" data-toggle=\"collapse\" data-target=\".navbar-collapse\">\n        <span class=\"icon-bar\"></span>\n        <span class=\"icon-bar\"></span>\n        <span class=\"icon-bar\"></span>\n    </button>\n    <a class=\"navbar-brand\" href=\"#\">$=Provider.Configuration.SiteName$</a>\n</div>\n\n<div class=\"navbar-collapse collapse\">\n    <ul class=\"nav navbar-nav\">\n        <li class=\"active\"><a href=\"/\">Home</a></li>\n$\nforeach(var cat in Provider.Content.Root.Contents){\n    if(cat.ClassName==\"Category\"){\n        echo(\'<li class=\"dropdown\"><a href=\"\'+cat.GetPageLinkWithTitle(\'\')+\'\" class=\"dropdown-toggle\" data-toggle=\"dropdown\">\'+cat.Title+\' <b class=\"caret\"></b></a>\');\n        echo(\'<ul class=\"dropdown-menu\">\');\n        foreach(var subCat in cat.Contents)\n            echo(\'<li><a href=\"\'+subCat.GetPageLinkWithTitle(\'\')+\'\">\'+subCat.Title+\'</a></li>\');\n        echo(\'</ul></li>\');\n    }\n    else \n        echo(\'<li><a href=\"\'+cat.GetPageLinkWithTitle(\'\')+\'\">\'+cat.Title+\'</a></li>\');\n}\n$\n    </ul>\n$\nif(Provider.Configuration.MultiLang){\n$\n    <div class=\"btn-group pull-right\" style=\"margin-left:7px\">\n        <button type=\"button\" class=\"btn btn-default btn-small dropdown-toggle\" data-toggle=\"dropdown\" style=\"margin-top:13px\">\n            Dil <b class=\"caret\"></b>\n        </button>\n        <ul class=\"dropdown-menu\">\n            <li><a href=\"#\">Türkçe</a></li>\n            <li><a href=\"#\">English</a></li>\n        </ul>\n    </div>\n$\n}\n\nif (Provider.User.IsAnonim())\n{\n$\n    <form class=\"navbar-form form-inline pull-right\" method=\"post\" action=\"DoLogin.ashx\">\n        <input type=\"hidden\" name=\"RedirectURL\" value=\"$=Provider.Request.RawUrl$\"/>\n        <input type=\"text\" name=\"Email\" placeholder=\"Email\" class=\"form-control\">\n        <input type=\"password\" name=\"Passwd\" placeholder=\"Şifre\" class=\"form-control\">\n        <button type=\"submit\" class=\"btn btn-primary btn-small\">Giriş</button>\n    </form>\n$\n} else {\n$\n    <div class=\"btn-group pull-right\">\n        <button type=\"button\" class=\"btn btn-success btn-small dropdown-toggle\" data-toggle=\"dropdown\" style=\"margin-top:13px\">\n            $=Provider.User.FullName$ <b class=\"caret\"></b>\n        </button>\n        <ul class=\"dropdown-menu\">\n            <li><a href=\"$=Provider.Configuration.MembershipProfilePage$\">Profil</a></li>\n            $ if(Provider.User.IsInRole(\'Designer\')){ $ <li><a href=\"?DesignMode=$= (Provider.DesignMode?\'Off\':\'On\') $\">$= (Provider.DesignMode?\'İzleme Modu\':\'Tasarım Modu\') $ </a></li>$ } $\n            $ if(Provider.User.IsInRole(\'Editor\')){ $ <li><a href=\"$=Provider.Configuration.AdminPage$\">Site Yönetimi</a></li>$ } $\n            <li class=\"divider\"></li>\n            <li><a href=\"/DoLogin.ashx?logout=1\">Oturumu Kapat</a></li>\n        </ul>\n    </div>        \n    <img src=\"$=Provider.GetThumbPath(Provider.User.Avatar,0,32,false)$\" class=\"img-circle pull-right\" style=\"height: 32px;margin: 9px 10px;\"/>\n$\n}\n$\n</div>LangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,1,3Template,12,Default.aspxRegion,6,topNavOrderNo,1,0Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,9,containerVisible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL),(4,'Default.aspx','jumbo',1,'StaticHtml','','Cinar.CMS.Serialization\nInnerHtml,317,<h1>Merhaba, dünya!</h1>\r\n<p>This is a template for a simple marketing or informational website. It includes a large callout called the hero unit and three supporting pieces of content. Use it as a starting point to create something more unique.</p>\r\n<p><a class=\"btn btn-primary btn-large\">Learn more &raquo;</a></p>LangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,1,4Template,12,Default.aspxRegion,5,jumboOrderNo,1,1Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,9,jumbotronVisible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL),(28,'Default.aspx','content',7,'DataList','','Cinar.CMS.Serialization\nEntityName,7,ContentFilter,12,CategoryId=2HowManyItems,2,30OrderBy,2,IdAscending,5,FalseShowPaging,4,TrueAjaxPaging,5,FalseLabelPrevPage,13,Previous PageLabelNextPage,9,Next PageDataTemplate,119,<img \r\n    src=\"$=entity.GetThumbPicture(0,150,false)$\"\r\n    path=\"$=entity.Picture$\"\r\n    height=\"120\"\r\n    />\r\n&nbsp;PictureWidth,1,0PictureHeight,1,0CropPicture,5,FalseDisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseDivClassName,12,extraItemDivId,2,28Template,12,Default.aspxRegion,7,contentOrderNo,1,7Name,8,DataListCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,15,lightBox gogPopVisible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0SQL,0,',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL);
/*!40000 ALTER TABLE `module` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `source`
--

DROP TABLE IF EXISTS `source`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
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
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `source`
--

LOCK TABLES `source` WRITE;
/*!40000 ALTER TABLE `source` DISABLE KEYS */;
INSERT INTO `source` VALUES (NULL,'Editorial',NULL,NULL,NULL,1,'1990-01-01 00:00:00',NULL,NULL,NULL,1,NULL);
/*!40000 ALTER TABLE `source` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tag`
--

DROP TABLE IF EXISTS `tag`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tag` (
  `DisplayName` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `Headline` tinyint(1) DEFAULT '0',
  `ContentCount` int(11) DEFAULT NULL,
  `Noise` tinyint(1) DEFAULT '0',
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tag`
--

LOCK TABLES `tag` WRITE;
/*!40000 ALTER TABLE `tag` DISABLE KEYS */;
/*!40000 ALTER TABLE `tag` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `template`
--

DROP TABLE IF EXISTS `template`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
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
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `template`
--

LOCK TABLES `template` WRITE;
/*!40000 ALTER TABLE `template` DISABLE KEYS */;
INSERT INTO `template` VALUES ('Default.aspx','<!DOCTYPE html>\r\n<html>\r\n<head>\r\n$=this.HeadSection$\r\n</head>\r\n<body>\r\n    <div class=\"container\">\r\n        <div id=\"topNav\" class=\"Region navbar navbar-inverse navbar-fixed-top\">$=this.topNav$</div>\r\n    </div>\r\n\r\n    <div id=\"jumbo\" class=\"Region container\">\r\n        $=this.jumbo$\r\n    </div>\r\n    \r\n    <div id=\"content\" class=\"Region container\">\r\n        $=this.content$\r\n    </div>\r\n    \r\n    <footer id=\"footer\" class=\"Region container\">\r\n        $=this.footer$\r\n    </footer>\r\n</body>\r\n</html>',1,'1990-01-01 00:00:00',0,'2013-08-29 00:21:05',1,1,0);
/*!40000 ALTER TABLE `template` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user` (
  `Email` varchar(100) COLLATE utf8_turkish_ci NOT NULL,
  `Password` varchar(16) COLLATE utf8_turkish_ci NOT NULL,
  `Keyword` varchar(16) COLLATE utf8_turkish_ci DEFAULT NULL,
  `Nick` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
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
  PRIMARY KEY (`Id`),
  KEY `UNQ_User_Email` (`Email`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES ('root@local','63A9F0EA7BB98050','jhrd74ghe63','admin','User,Editor,Designer','Root','User',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,1,'1990-01-01 00:00:00',NULL,NULL,NULL,1,NULL),('anonim','','63beyte674hge','anonim','','Anonim','User',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,2,'1990-01-01 00:00:00',NULL,NULL,NULL,1,NULL),('editor','63A9F0EA7BB98050','ge548rhe46e','editor','User,Editor','Editor','User',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,3,'1990-01-01 00:00:00',NULL,NULL,NULL,1,NULL);
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2013-09-02 18:19:44
