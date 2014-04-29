-- MySQL dump 10.11
--
-- Host: localhost    Database: cinarcms
-- ------------------------------------------------------
-- Server version	5.0.81-community-nt

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
  `Title` varchar(100) collate utf8_turkish_ci default NULL,
  `DisableAutoContent` tinyint(1) NOT NULL default '0',
  `UserId` int(11) default NULL,
  `Name` varchar(100) collate utf8_turkish_ci NOT NULL,
  `Description` text collate utf8_turkish_ci,
  `Picture` varchar(100) collate utf8_turkish_ci default NULL,
  `Picture2` varchar(100) collate utf8_turkish_ci default NULL,
  `Id` int(11) NOT NULL auto_increment,
  `InsertDate` datetime NOT NULL default '1990-01-01 00:00:00',
  `InsertUserId` int(11) default NULL,
  `UpdateDate` datetime default NULL,
  `UpdateUserId` int(11) default NULL,
  `Visible` tinyint(1) NOT NULL default '1',
  `OrderNo` int(11) default NULL,
  PRIMARY KEY  (`Id`)
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
-- Table structure for table `company`
--

DROP TABLE IF EXISTS `company`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `company` (
  `Tags` varchar(300) collate utf8_turkish_ci default NULL,
  `SectorId` int(11) default NULL,
  `UserId` int(11) NOT NULL,
  `Phone` varchar(50) collate utf8_turkish_ci default NULL,
  `Fax` varchar(50) collate utf8_turkish_ci default NULL,
  `AddressLine1` varchar(200) collate utf8_turkish_ci default NULL,
  `AddressLine2` varchar(200) collate utf8_turkish_ci default NULL,
  `City` varchar(50) collate utf8_turkish_ci default NULL,
  `CountryId` int(11) default NULL,
  `ZipCode` varchar(5) collate utf8_turkish_ci default NULL,
  `Email` varchar(100) collate utf8_turkish_ci NOT NULL,
  `Web` varchar(150) collate utf8_turkish_ci default NULL,
  `ExtraField1` varchar(100) collate utf8_turkish_ci default NULL,
  `ExtraField2` varchar(100) collate utf8_turkish_ci default NULL,
  `ExtraField3` varchar(100) collate utf8_turkish_ci default NULL,
  `ExtraField4` varchar(100) collate utf8_turkish_ci default NULL,
  `ExtraField5` varchar(100) collate utf8_turkish_ci default NULL,
  `Name` varchar(100) collate utf8_turkish_ci NOT NULL,
  `Description` text collate utf8_turkish_ci,
  `Picture` varchar(100) collate utf8_turkish_ci default NULL,
  `Picture2` varchar(100) collate utf8_turkish_ci default NULL,
  `Id` int(11) NOT NULL auto_increment,
  `InsertDate` datetime NOT NULL default '1990-01-01 00:00:00',
  `InsertUserId` int(11) default NULL,
  `UpdateDate` datetime default NULL,
  `UpdateUserId` int(11) default NULL,
  `Visible` tinyint(1) NOT NULL default '1',
  `OrderNo` int(11) default NULL,
  PRIMARY KEY  (`Id`),
  UNIQUE KEY `UNQ_Company_Email` (`Email`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `company`
--

LOCK TABLES `company` WRITE;
/*!40000 ALTER TABLE `company` DISABLE KEYS */;
INSERT INTO `company` VALUES ('User',1,5,'+90 (212) 305-0000','+90 (212) 305-0100','Eski Büyükdere Caddesi SOCAR Bosphorus','Plaza No: 231 Kat:7 34398 Maslak Şişli','Istanbul',6,NULL,'info@socar.com.tr',NULL,'yes','','','','','SOCAR Turkey Enerji A.Ş.',NULL,'/socar_logo.jpg',NULL,1,'2014-04-28 23:29:55',1,'2014-04-29 00:04:40',1,1,0),(NULL,2,4,'','','','','',7,NULL,'',NULL,'','','','','','Magic Finger',NULL,'/magicfinger.gif',NULL,2,'2014-04-29 01:16:50',1,'2014-04-29 01:16:50',1,1,0);
/*!40000 ALTER TABLE `company` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `configuration`
--

DROP TABLE IF EXISTS `configuration`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `configuration` (
  `SiteName` varchar(100) collate utf8_turkish_ci default NULL,
  `SiteAddress` varchar(100) collate utf8_turkish_ci default NULL,
  `SiteDescription` text collate utf8_turkish_ci,
  `SiteKeywords` text collate utf8_turkish_ci,
  `SiteLogo` varchar(100) collate utf8_turkish_ci default NULL,
  `SiteIcon` varchar(100) collate utf8_turkish_ci default NULL,
  `UseExternalLibrary` varchar(30) collate utf8_turkish_ci NOT NULL default 'False',
  `SessionTimeout` int(11) default NULL,
  `BufferOutput` tinyint(1) default NULL,
  `MultiLang` tinyint(1) default NULL,
  `DefaultLang` int(11) NOT NULL,
  `LogHit` tinyint(1) default NULL,
  `DefaultStyleSheet` text collate utf8_turkish_ci,
  `DefaultJavascript` text collate utf8_turkish_ci,
  `DefaultPageLoadScript` text collate utf8_turkish_ci,
  `Routes` text collate utf8_turkish_ci,
  `CountTags` tinyint(1) default NULL,
  `DefaultDateFormat` varchar(100) collate utf8_turkish_ci default NULL,
  `NoPicture` varchar(100) collate utf8_turkish_ci default NULL,
  `ThumbQuality` int(11) default NULL,
  `ImageUploadMaxWidth` int(11) default NULL,
  `AuthEmail` varchar(100) collate utf8_turkish_ci default NULL,
  `MailHost` varchar(100) collate utf8_turkish_ci default NULL,
  `MailPort` int(11) default NULL,
  `MailUsername` varchar(100) collate utf8_turkish_ci default NULL,
  `MailPassword` varchar(100) collate utf8_turkish_ci default NULL,
  `UseCache` varchar(30) collate utf8_turkish_ci NOT NULL default 'False',
  `CacheLifeTime` int(11) default NULL,
  `MainPage` varchar(100) collate utf8_turkish_ci default NULL,
  `CategoryPage` varchar(100) collate utf8_turkish_ci default NULL,
  `ContentPage` varchar(100) collate utf8_turkish_ci default NULL,
  `LoginPage` varchar(100) collate utf8_turkish_ci default NULL,
  `MembershipFormPage` varchar(100) collate utf8_turkish_ci default NULL,
  `MembershipProfilePage` varchar(100) collate utf8_turkish_ci default NULL,
  `RememberPasswordFormPage` varchar(100) collate utf8_turkish_ci default NULL,
  `UserActivationPage` varchar(100) collate utf8_turkish_ci default NULL,
  `AdminPage` varchar(100) collate utf8_turkish_ci default NULL,
  `Id` int(11) NOT NULL auto_increment,
  `InsertDate` datetime NOT NULL default '1990-01-01 00:00:00',
  `InsertUserId` int(11) default NULL,
  `UpdateDate` datetime default NULL,
  `UpdateUserId` int(11) default NULL,
  `Visible` tinyint(1) NOT NULL default '1',
  `OrderNo` int(11) default NULL,
  `FacebookAppId` varchar(100) collate utf8_turkish_ci NOT NULL default '1431691133709277',
  `AfterMembershipPage` varchar(50) collate utf8_turkish_ci default NULL,
  `AfterRememberPasswordPage` varchar(50) collate utf8_turkish_ci default NULL,
  `AfterUserActivationPage` varchar(50) collate utf8_turkish_ci default NULL,
  `DefaultPageViewRole` varchar(50) collate utf8_turkish_ci default NULL,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `configuration`
--

LOCK TABLES `configuration` WRITE;
/*!40000 ALTER TABLE `configuration` DISABLE KEYS */;
INSERT INTO `configuration` VALUES ('Hazar CRM','www.hazarcrm.com','Hazar Strategy Institute','cinar, cms, for developers, dotnet, html, javascript, bootstrap, jquery','','','None',120,1,0,1,0,'.dataTables_wrapper .row:last-child {\r\n    background-color: #fff;\r\n    border-bottom: 1px solid #fff;\r\n}','function bindTable(options) {\r\n    var oTable1 = $(options.tableId).dataTable({\r\n        \"aaData\": options.list,\r\n        \"bDestroy\": true,\r\n        \"bRetrieve\":true,\r\n        \"bProcessing\": true,     \r\n        \"oTableTools\":{\r\n            \"sRowSelect\": \"single\"\r\n        },\r\n        \"iDisplayLength\": options.pageSize || 10,\r\n\r\n        \"aoColumns\": options.columns,\r\n      \r\n        \"sAjaxDataProp\": \"data\",\r\n        \"oLanguage\": {\r\n            //\"sLengthMenu\": \"Her sayfada  _MENU_ kayıt görülsün\",\r\n            //\"sZeroRecords\": \"Kayıt bulunamadı\",\r\n            //\"sInfo\": \" Görüntülenen: _START_ - _END_ |  Toplam: _TOTAL_  kayıt\",\r\n            //\"sInfoEmpty\": \"Görüntülenecek kayıt yok.\",\r\n            //\"sInfoFiltered\": \"  Toplam kayıt sayısı: _MAX_ \",\r\n            //\"sSearch\": \"Ara\"\r\n        }      \r\n    });\r\n\r\n    $(\'table th input:checkbox\').on(\'click\', function () {\r\n        var that = this;\r\n        $(this).closest(\'table\').find(\'tr td input:checkbox\')\r\n        .each(function () {\r\n            this.checked = that.checked;\r\n            $(this).closest(\'tr\').toggleClass(\'selected\');\r\n        });\r\n\r\n    });\r\n\r\n    $(\"#table-entity tbody tr\").on(\'dblclick\',function (e) {\r\n        if ($(this).hasClass(\'row_selected\')) {\r\n            $(this).removeClass(\'row_selected\');\r\n        }\r\n        else {\r\n            oTable1.$(\'tr.row_selected\').removeClass(\'row_selected\');\r\n            $(this).addClass(\'row_selected\');\r\n        }\r\n    });\r\n\r\n    $(\'[data-rel=\"tooltip\"]\').tooltip({ placement: tooltip_placement });\r\n    function tooltip_placement(context, source) {\r\n        var $source = $(source);\r\n        var $parent = $source.closest(\'table\')\r\n        var off1 = $parent.offset();\r\n        var w1 = $parent.width();\r\n\r\n        var off2 = $source.offset();\r\n        var w2 = $source.width();\r\n\r\n        if (parseInt(off2.left) < parseInt(off1.left) + parseInt(w1 / 2)) return \'right\';\r\n        return \'left\';\r\n    }\r\n    \r\n    $(options.tableId).prev().find(\'.col-sm-6\').hide();\r\n}\r\n\r\n$(function(){\r\n    $(\'input[type=file]\').ace_file_input({\r\n		no_file:\'No File ...\',\r\n		btn_choose:\'Choose\',\r\n		btn_change:\'Change\',\r\n		droppable:false,\r\n		onchange:null,\r\n		thumbnail: true, //| large\r\n		whitelist:\'gif|png|jpg|jpeg\'\r\n		//blacklist:\'exe|php\'\r\n		//onchange:\'\'\r\n		//\r\n	});\r\n    \r\n	$(\".chosen-select\").chosen(); \r\n    \r\n	$(\'.date-picker\').datepicker({autoclose:true}).next().on(ace.click_event, function(){\r\n		$(this).prev().focus();\r\n	});\r\n	$(\'.date-range-picker\').daterangepicker().prev().on(ace.click_event, function(){\r\n		$(this).next().focus();\r\n	});\r\n    $(\'.input-mask-phone\').mask(\'+99 (999) 999-9999\');\r\n});\r\n\r\nfunction loadForm(theForm, entity, mode){\r\n    theForm = $(theForm);\r\n    theForm.data(\'entity\',entity);\r\n    if(mode && mode==\'view\'){\r\n        theForm.find(\':input\').attr(\"disabled\", true);\r\n        $(\'.ace-file-input\').hide();\r\n        theForm.find(\'.form-actions\').hide();\r\n    }\r\n    for(var key in entity){\r\n        try{\r\n            var ctrl = $(\'input[name=\'+key+\'],select[name=\'+key+\'],textarea[name=\'+key+\'],img[name=\'+key+\']\');\r\n            if(ctrl.length>0){\r\n                if(ctrl[0].tagName.toLowerCase()==\'select\' && ctrl.attr(\"multiple\"))\r\n                    setChosenValue(ctrl, entity[key]);\r\n                else if(ctrl[0].tagName.toLowerCase()==\'img\' && entity[key])\r\n                    ctrl.attr(\'src\', entity[key]);\r\n                else if(ctrl.attr(\'type\')==\'checkbox\')\r\n                    ctrl.prop(\'checked\', entity[key] ? true : false);\r\n                else\r\n                    ctrl.val(entity[key]);\r\n            }\r\n        }catch(e){\r\n            console.log(e);\r\n        }\r\n    }\r\n}\r\n\r\nfunction setChosenValue(chosen, val){\r\n    $(chosen).val(val.split(\',\'));\r\n    $(chosen).trigger(\"chosen:updated\");\r\n}\r\n\r\nfunction makeChosen(select, entityName, filter){\r\n    var select = $(select);\r\n    select.chosen();\r\n	select.next().find(\".chosen-select-deselect\").chosen({allow_single_deselect:true});\r\n	select.next().find(\'.chosen-search input\').autocomplete({\r\n		source: function( request, response ) {\r\n            readEntityList(entityName, filter, function(entityList){\r\n                select.next().find(\'ul.chosen-results\').empty(); select.empty();\r\n				response( $.map( entityList, function( item ) {\r\n					select.next().find(\'ul.chosen-results\').append(\'<li class=\"active-result\">\' + item.Name + \'</li>\');\r\n					select.append(\'<option value=\"\' + item.Id + \'\">\' + item.Name + \'</option>\');\r\n				}));\r\n				select.trigger(\"chosen:updated\");\r\n            });\r\n		}\r\n	});\r\n}\r\n\r\n\r\nfunction fillSelectOptions(select, entityName, filter){\r\n    var select = $(select);\r\n    readEntityList(entityName, filter, function(entityList){\r\n        entityList.each(function(item){\r\n    		select.append(\'<option value=\"\' + item.Id + \'\">\' + item.Name + \'</option>\');\r\n        });\r\n        var key = select.attr(\'id\'); \r\n        select.val(select.closest(\'form\').data(\'entity\')[key]);\r\n    });\r\n}','','',1,'dd MMMM yyyy','',90,960,'info@example.com','mail.example.com',25,'','','False',15,'Default.aspx','Category.aspx','Content.aspx','Login.aspx','Membership.aspx','Profile.aspx','RememberPassword.aspx','Activation.aspx','Admin.aspx',1,'2013-08-05 22:29:00',2,'2014-04-29 01:54:32',1,1,0,'1431691133709277','','','','User');
/*!40000 ALTER TABLE `configuration` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `contact`
--

DROP TABLE IF EXISTS `contact`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `contact` (
  `TitleId` int(11) default NULL,
  `Title2Id` int(11) default NULL,
  `CompanyId` int(11) default NULL,
  `Tags` varchar(300) collate utf8_turkish_ci default NULL,
  `Language1Id` int(11) default NULL,
  `Language2Id` int(11) default NULL,
  `Language3Id` int(11) default NULL,
  `Gender` varchar(50) collate utf8_turkish_ci default NULL,
  `BirthDate` datetime default NULL,
  `Kind1Id` int(11) default NULL,
  `Kind2Id` int(11) default NULL,
  `Kind3Id` int(11) default NULL,
  `Kind4Id` int(11) default NULL,
  `Kind5Id` int(11) default NULL,
  `PhoneCell` varchar(50) collate utf8_turkish_ci default NULL,
  `PhoneWork` varchar(50) collate utf8_turkish_ci default NULL,
  `PhoneHome` varchar(50) collate utf8_turkish_ci default NULL,
  `Fax` varchar(50) collate utf8_turkish_ci default NULL,
  `AddressLine1` varchar(200) collate utf8_turkish_ci default NULL,
  `AddressLine2` varchar(200) collate utf8_turkish_ci default NULL,
  `City` varchar(50) collate utf8_turkish_ci default NULL,
  `CountryId` int(11) default NULL,
  `ZipCode` varchar(5) collate utf8_turkish_ci default NULL,
  `Email` varchar(100) collate utf8_turkish_ci NOT NULL,
  `Web` varchar(150) collate utf8_turkish_ci default NULL,
  `ExtraField1` varchar(100) collate utf8_turkish_ci default NULL,
  `ExtraField2` varchar(100) collate utf8_turkish_ci default NULL,
  `ExtraField3` varchar(100) collate utf8_turkish_ci default NULL,
  `ExtraField4` varchar(100) collate utf8_turkish_ci default NULL,
  `ExtraField5` varchar(100) collate utf8_turkish_ci default NULL,
  `UserId` int(11) NOT NULL,
  `NewsletterMembership` tinyint(1) default NULL,
  `Name` varchar(100) collate utf8_turkish_ci NOT NULL,
  `Description` text collate utf8_turkish_ci,
  `Picture` varchar(100) collate utf8_turkish_ci default NULL,
  `Picture2` varchar(100) collate utf8_turkish_ci default NULL,
  `Id` int(11) NOT NULL auto_increment,
  `InsertDate` datetime NOT NULL default '1990-01-01 00:00:00',
  `InsertUserId` int(11) default NULL,
  `UpdateDate` datetime default NULL,
  `UpdateUserId` int(11) default NULL,
  `Visible` tinyint(1) NOT NULL default '1',
  `OrderNo` int(11) default NULL,
  PRIMARY KEY  (`Id`),
  UNIQUE KEY `UNQ_Contact_Email` (`Email`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `contact`
--

LOCK TABLES `contact` WRITE;
/*!40000 ALTER TABLE `contact` DISABLE KEYS */;
/*!40000 ALTER TABLE `contact` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `content`
--

DROP TABLE IF EXISTS `content`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `content` (
  `Keyword` varchar(100) collate utf8_turkish_ci default NULL,
  `ClassName` varchar(100) collate utf8_turkish_ci NOT NULL default 'Content',
  `CategoryId` int(11) NOT NULL default '1',
  `Title` varchar(200) collate utf8_turkish_ci NOT NULL,
  `Description` text collate utf8_turkish_ci,
  `Metin` text collate utf8_turkish_ci,
  `PublishDate` datetime NOT NULL default '1990-01-01 00:00:00',
  `Picture` varchar(100) collate utf8_turkish_ci default NULL,
  `Picture2` varchar(100) collate utf8_turkish_ci default NULL,
  `Keywords` text collate utf8_turkish_ci,
  `Hierarchy` varchar(100) collate utf8_turkish_ci NOT NULL,
  `AuthorId` int(11) default NULL,
  `SourceId` int(11) default NULL,
  `Tags` varchar(300) collate utf8_turkish_ci default NULL,
  `TagRanks` varchar(300) collate utf8_turkish_ci default NULL,
  `ShowInPage` varchar(100) collate utf8_turkish_ci default NULL,
  `ShowContentsInPage` varchar(100) collate utf8_turkish_ci default NULL,
  `ShowCategoriesInPage` varchar(100) collate utf8_turkish_ci default NULL,
  `IsManset` tinyint(1) NOT NULL default '0',
  `SpotTitle` varchar(200) collate utf8_turkish_ci default NULL,
  `ContentSourceId` int(11) default NULL,
  `SourceLink` varchar(200) collate utf8_turkish_ci default NULL,
  `ViewCount` int(11) NOT NULL default '0',
  `CommentCount` int(11) NOT NULL default '0',
  `RecommendCount` int(11) NOT NULL default '0',
  `LikeIt` int(11) NOT NULL default '0',
  `Id` int(11) NOT NULL auto_increment,
  `InsertDate` datetime NOT NULL default '1990-01-01 00:00:00',
  `InsertUserId` int(11) default NULL,
  `UpdateDate` datetime default NULL,
  `UpdateUserId` int(11) default NULL,
  `Visible` tinyint(1) NOT NULL default '1',
  `OrderNo` int(11) default NULL,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `content`
--

LOCK TABLES `content` WRITE;
/*!40000 ALTER TABLE `content` DISABLE KEYS */;
INSERT INTO `content` VALUES (NULL,'Category',0,'Kök',NULL,NULL,'1990-01-01 00:00:00',NULL,NULL,NULL,'',NULL,NULL,NULL,NULL,NULL,NULL,NULL,0,NULL,NULL,NULL,0,0,0,0,1,'1990-01-01 00:00:00',NULL,NULL,NULL,1,NULL),('ba4f14c8-6219-4894-ab3b-3d5e728b6ddc','Category',1,'Contacts','','','2006-08-03 01:32:31','','','','00001',1,1,'','','','','',0,'',0,'',0,0,0,0,2,'2013-08-06 01:33:00',1,'2014-04-18 22:50:39',1,1,0),('bab0ac25-185b-40d9-9446-ed849373cdb3','Category',1,'Companies','','','2006-08-03 01:33:45','','','','00001',1,1,'hicret','','','','',0,'',0,'',0,0,0,0,3,'2013-08-06 01:34:00',1,'2014-04-18 22:50:59',1,1,0),('4fbfa145-9ad9-4c57-b493-fcb350a31b4d','Category',1,'Events','','','2003-08-27 01:34:36','','','','00001',1,1,'ürün, hizmet','','','','',0,'',0,'',0,0,0,0,4,'2013-08-06 01:34:00',1,'2014-04-18 23:00:44',1,1,0),('5a622fd5-6d65-450c-98a7-7ae72b7f1487','Category',1,'Definitions','','','2003-08-27 01:34:19','','','','00001',1,1,'','','','','',0,'',0,'',0,0,0,0,5,'2013-08-06 01:34:00',1,'2014-04-19 02:01:24',1,1,0),('13a2d432-bd84-4b90-9500-8fb410ddc889','Content',2,'List','Donec id elit non mi porta gravida at eget metus. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus. Etiam porta sem malesuada magna mollis euismod. Donec sed odio dui. </p>\r\n','','2003-08-27 17:44:17','/UserFiles/body_bg3.jpg','/UserFiles/Images/logo.png','','00001,00002',1,1,'','','ContactList.aspx','','',0,'',0,'',0,0,0,0,6,'2013-08-06 17:44:00',1,'2014-04-18 23:23:21',1,1,0),('72966369-d084-4be1-b50d-3749bb9c4654','Content',3,'List','','','2013-08-04 00:00:44','','','','00001,00003',1,1,'','','','','',0,'',0,'',0,0,0,0,8,'2013-08-06 17:45:00',1,'2014-04-18 23:16:50',1,1,0),('a524cde2-fff8-453d-aa6c-87e9581d95d6','Content',3,'Add new...','','','2006-08-03 17:45:54','','','','00001,00003',1,1,'','','','','',0,'',0,'',0,0,0,0,9,'2013-08-06 17:45:00',1,'2014-04-18 23:17:04',1,1,0),('a008ef08-b595-40b1-80cd-76fc50eac8ad','Content',2,'Reports','Donec id elit non mi porta gravida at eget metus. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus. Etiam porta sem malesuada magna mollis euismod. Donec sed odio dui.','','2013-09-15 14:19:44','/UserFiles/6.jpg','','','00001,00002',1,1,'','','','','',0,'',0,'',0,0,0,0,14,'2013-09-15 14:20:30',1,'2014-04-18 23:15:50',1,1,0),('a28734e9-09d0-497a-8289-5e933cce485b','Content',2,'Add New...','Donec id elit non mi porta gravida at eget metus. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus. Etiam porta sem malesuada magna mollis euismod. Donec sed odio dui. </p>\r\n','','2013-09-16 01:11:48','','','','00001,00002',1,1,'','','','','',0,'',0,'',0,0,0,0,15,'2013-09-16 01:11:24',1,'2014-04-18 23:14:58',1,1,0),('cb8846d9-911d-4e8f-8b7f-bd79d4787c4c','Content',3,'Reports','','','2014-04-18 23:17:07','','','','00001,00003',1,1,'','','','','',0,'',0,'',0,0,0,0,16,'2014-04-18 23:17:13',1,'2014-04-18 23:17:13',1,1,0),('11a29415-80d7-4613-9fc2-9f90747ba87a','Content',4,'List','','','2014-04-18 23:17:22','','','','00001,00004',1,1,'','','','','',0,'',0,'',0,0,0,0,17,'2014-04-18 23:17:27',1,'2014-04-18 23:17:27',1,1,0),('67245ac8-96ed-4e0b-ad8c-32e8f1681493','Content',4,'Add new...','','','2014-04-18 23:17:30','','','','00001,00004',1,1,'','','','','',0,'',0,'',0,0,0,0,18,'2014-04-18 23:17:36',1,'2014-04-18 23:17:36',1,1,0),('43e7ea49-d4c9-4ca0-b11a-c7265c229b1b','Content',4,'Reports','','','2014-04-18 23:17:39','','','','00001,00004',1,1,'','','','','',0,'',0,'',0,0,0,0,19,'2014-04-18 23:17:45',1,'2014-04-18 23:17:45',1,1,0),('ead655d1-19c2-472e-a06e-08f660c37235','Content',5,'Contact Types','','','2014-04-19 02:01:33','','','','00001,00005',1,1,'','','','','',0,'',0,'',0,0,0,0,20,'2014-04-19 02:01:53',1,'2014-04-19 02:01:53',1,1,0),('ea7922b3-65f8-440a-b528-fb68b2296480','Content',5,'Contact Titles','','','2014-04-19 02:01:56','','','','00001,00005',1,1,'','','','','',0,'',0,'',0,0,0,0,21,'2014-04-19 02:02:23',1,'2014-04-19 02:02:23',1,1,0),('d56dbe6b-376a-4a04-bc82-8835cf67bd3c','Content',5,'Languages','','','2014-04-19 02:02:26','','','','00001,00005',1,1,'','','','','',0,'',0,'',0,0,0,0,22,'2014-04-19 02:02:40',1,'2014-04-19 02:02:40',1,1,0),('7ae2f43e-c33d-4fb5-943f-93d18d702424','Content',5,'Countries','','','2014-04-19 02:02:51','','','','00001,00005',1,1,'','','','','',0,'',0,'',0,0,0,0,23,'2014-04-19 02:02:59',1,'2014-04-19 02:02:59',1,1,0);
/*!40000 ALTER TABLE `content` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `contentpicture`
--

DROP TABLE IF EXISTS `contentpicture`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `contentpicture` (
  `ContentId` int(11) default NULL,
  `Title` varchar(200) collate utf8_turkish_ci default NULL,
  `Description` text collate utf8_turkish_ci,
  `Tag` varchar(200) collate utf8_turkish_ci default NULL,
  `TagData` text collate utf8_turkish_ci,
  `LikeIt` int(11) NOT NULL default '0',
  `FileName` varchar(100) collate utf8_turkish_ci NOT NULL,
  `Id` int(11) NOT NULL auto_increment,
  `InsertDate` datetime NOT NULL default '1990-01-01 00:00:00',
  `InsertUserId` int(11) default NULL,
  `UpdateDate` datetime default NULL,
  `UpdateUserId` int(11) default NULL,
  `Visible` tinyint(1) NOT NULL default '1',
  `OrderNo` int(11) default NULL,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `contentpicture`
--

LOCK TABLES `contentpicture` WRITE;
/*!40000 ALTER TABLE `contentpicture` DISABLE KEYS */;
INSERT INTO `contentpicture` VALUES (15,'','','','',0,'/UserFiles/6.jpg',1,'2013-11-03 15:53:51',1,'2013-11-03 15:53:51',1,1,0),(15,'','','','',0,'/UserFiles/body_bg3.jpg',2,'2013-11-03 15:53:51',1,'2013-11-03 15:53:51',1,1,0);
/*!40000 ALTER TABLE `contentpicture` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `contenttag`
--

DROP TABLE IF EXISTS `contenttag`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `contenttag` (
  `ContentId` int(11) NOT NULL,
  `TagId` int(11) NOT NULL,
  `Rank` int(11) NOT NULL default '0',
  `Id` int(11) NOT NULL auto_increment,
  `InsertDate` datetime NOT NULL default '1990-01-01 00:00:00',
  `InsertUserId` int(11) default NULL,
  `UpdateDate` datetime default NULL,
  `UpdateUserId` int(11) default NULL,
  `Visible` tinyint(1) NOT NULL default '1',
  `OrderNo` int(11) default NULL,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `contenttag`
--

LOCK TABLES `contenttag` WRITE;
/*!40000 ALTER TABLE `contenttag` DISABLE KEYS */;
INSERT INTO `contenttag` VALUES (4,1,0,1,'2013-09-03 19:09:00',1,'2013-09-03 19:09:00',1,1,0),(4,2,0,2,'2013-09-03 19:09:01',1,'2013-09-03 19:09:01',1,1,0),(3,5,0,5,'2013-09-03 19:32:42',1,'2013-09-03 19:32:42',1,1,0);
/*!40000 ALTER TABLE `contenttag` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `definition`
--

DROP TABLE IF EXISTS `definition`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `definition` (
  `Kind` varchar(100) collate utf8_turkish_ci default NULL,
  `Name` varchar(100) collate utf8_turkish_ci NOT NULL,
  `Description` text collate utf8_turkish_ci,
  `Picture` varchar(100) collate utf8_turkish_ci default NULL,
  `Picture2` varchar(100) collate utf8_turkish_ci default NULL,
  `Id` int(11) NOT NULL auto_increment,
  `InsertDate` datetime NOT NULL default '1990-01-01 00:00:00',
  `InsertUserId` int(11) default NULL,
  `UpdateDate` datetime default NULL,
  `UpdateUserId` int(11) default NULL,
  `Visible` tinyint(1) NOT NULL default '1',
  `OrderNo` int(11) default NULL,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `definition`
--

LOCK TABLES `definition` WRITE;
/*!40000 ALTER TABLE `definition` DISABLE KEYS */;
INSERT INTO `definition` VALUES ('Sector','Enerji','','','',1,'2014-04-22 23:34:42',1,'2014-04-22 23:34:42',1,1,0),('Sector','Teknoloji','','','',2,'2014-04-22 23:34:56',1,'2014-04-22 23:34:56',1,1,0),('Sector','Kamu','','','',3,'2014-04-22 23:35:35',1,'2014-04-22 23:35:35',1,1,0),('Title','Prof.',NULL,NULL,NULL,4,'2014-04-22 23:47:48',1,'2014-04-22 23:47:48',1,1,0),('Title','Doç.',NULL,NULL,NULL,5,'2014-04-22 23:56:50',1,'2014-04-22 23:56:50',1,1,0),('Country','Turkey',NULL,NULL,NULL,6,'2014-04-28 20:09:16',1,'2014-04-28 20:09:53',1,1,0),('Country','United States',NULL,NULL,NULL,7,'2014-04-28 20:11:28',1,'2014-04-28 20:11:49',1,1,0),('CompanyTag','Gas',NULL,NULL,NULL,8,'2014-04-28 23:41:07',1,'2014-04-28 23:41:07',1,1,0),('CompanyTag','Green Energy',NULL,NULL,NULL,9,'2014-04-28 23:41:39',1,'2014-04-28 23:41:39',1,1,0);
/*!40000 ALTER TABLE `definition` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `event`
--

DROP TABLE IF EXISTS `event`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `event` (
  `EventDate` datetime default NULL,
  `Location` varchar(100) collate utf8_turkish_ci default NULL,
  `EventTime` varchar(100) collate utf8_turkish_ci default NULL,
  `Name` varchar(100) collate utf8_turkish_ci NOT NULL,
  `Description` text collate utf8_turkish_ci,
  `Picture` varchar(100) collate utf8_turkish_ci default NULL,
  `Picture2` varchar(100) collate utf8_turkish_ci default NULL,
  `Id` int(11) NOT NULL auto_increment,
  `InsertDate` datetime NOT NULL default '1990-01-01 00:00:00',
  `InsertUserId` int(11) default NULL,
  `UpdateDate` datetime default NULL,
  `UpdateUserId` int(11) default NULL,
  `Visible` tinyint(1) NOT NULL default '1',
  `OrderNo` int(11) default NULL,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `event`
--

LOCK TABLES `event` WRITE;
/*!40000 ALTER TABLE `event` DISABLE KEYS */;
/*!40000 ALTER TABLE `event` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `lang`
--

DROP TABLE IF EXISTS `lang`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `lang` (
  `Code` varchar(100) collate utf8_turkish_ci NOT NULL,
  `Name` varchar(100) collate utf8_turkish_ci NOT NULL,
  `Id` int(11) NOT NULL auto_increment,
  `InsertDate` datetime NOT NULL default '1990-01-01 00:00:00',
  `InsertUserId` int(11) default NULL,
  `UpdateDate` datetime default NULL,
  `UpdateUserId` int(11) default NULL,
  `Visible` tinyint(1) NOT NULL default '1',
  `OrderNo` int(11) default NULL,
  PRIMARY KEY  (`Id`)
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
-- Table structure for table `log`
--

DROP TABLE IF EXISTS `log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `log` (
  `LogType` varchar(100) collate utf8_turkish_ci NOT NULL,
  `Category` varchar(100) collate utf8_turkish_ci NOT NULL,
  `Description` text collate utf8_turkish_ci NOT NULL,
  `Id` int(11) NOT NULL auto_increment,
  `InsertDate` datetime NOT NULL default '1990-01-01 00:00:00',
  `InsertUserId` int(11) default NULL,
  `UpdateDate` datetime default NULL,
  `UpdateUserId` int(11) default NULL,
  `Visible` tinyint(1) NOT NULL default '1',
  `OrderNo` int(11) default NULL,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `log`
--

LOCK TABLES `log` WRITE;
/*!40000 ALTER TABLE `log` DISABLE KEYS */;
INSERT INTO `log` VALUES ('Error','SocialAuth','An error occurred while executing http://api.linkedin.com/v1/people/~/connections:(id,first-name,last-name,public-profile-url)\r\nRequest Parameters: \r\nUnauthorized! Please ensure:\r\n(1) All required parameters are passed\r\n(2) Signature is Url Encoded\r\n(3) Authorization header is properly set',1,'2013-12-03 23:37:11',4,'2013-12-03 23:37:11',4,1,0),('History_Company','Insert','Id,1,0Name,24,SOCAR Turkey Enerji A.Ş.Email,17,info@socar.com.trPhone,18,+90 (212) 305-0000Fax,18,+90 (212) 305-0100Tags,4,UserSectorId,1,1UserId,1,5ExtraField1,0,AddressLine1,38,Eski Büyükdere Caddesi SOCAR BosphorusAddressLine2,38,Plaza No: 231 Kat:7 34398 Maslak ŞişliCity,8,IstanbulCountryId,1,6ExtraField2,0,ExtraField3,0,ExtraField4,0,ExtraField5,0,InsertDate,18,28.4.2014 23:29:55InsertUserId,1,1UpdateDate,18,28.4.2014 23:29:55UpdateUserId,1,1Visible,4,TrueOrderNo,1,0',2,'2014-04-28 23:29:55',1,'2014-04-28 23:29:55',1,1,0),('History_Company','Update','SectorId : 1 ==>> 0\r\nUserId : 5 ==>> 0\r\nCountryId : 6 ==>> 0\r\nUpdateDate : 28.4.2014 23:29:55 ==>> 28.4.2014 23:34:11\r\n',3,'2014-04-28 23:34:11',1,'2014-04-28 23:34:11',1,1,0),('History_Company','Update','SectorId : 0 ==>> 1\r\nUserId : 0 ==>> 5\r\nCountryId : 0 ==>> 6\r\nUpdateDate : 28.4.2014 23:34:11 ==>> 28.4.2014 23:58:54\r\n',4,'2014-04-28 23:58:54',1,'2014-04-28 23:58:54',1,1,0),('History_Company','Update','ExtraField1 :  ==>> yes\r\nUpdateDate : 28.4.2014 23:58:54 ==>> 29.4.2014 00:04:40\r\n',5,'2014-04-29 00:04:40',1,'2014-04-29 00:04:40',1,1,0),('History_Company','Insert','Id,1,0Name,12,Magic FingerEmail,0,Phone,0,Fax,0,SectorId,1,2UserId,1,4AddressLine1,0,AddressLine2,0,City,0,CountryId,1,7ExtraField1,0,ExtraField2,0,ExtraField3,0,ExtraField4,0,ExtraField5,0,InsertDate,18,29.4.2014 01:16:50InsertUserId,1,1UpdateDate,18,29.4.2014 01:16:50UpdateUserId,1,1Visible,4,TrueOrderNo,1,0',6,'2014-04-29 01:16:50',1,'2014-04-29 01:16:50',1,1,0);
/*!40000 ALTER TABLE `log` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `meeting`
--

DROP TABLE IF EXISTS `meeting`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `meeting` (
  `TaskId` int(11) default NULL,
  `ContactId` int(11) default NULL,
  `MeetingType` varchar(16) collate utf8_turkish_ci default NULL,
  `Details` varchar(100) collate utf8_turkish_ci default NULL,
  `Id` int(11) NOT NULL auto_increment,
  `InsertDate` datetime NOT NULL default '1990-01-01 00:00:00',
  `InsertUserId` int(11) default NULL,
  `UpdateDate` datetime default NULL,
  `UpdateUserId` int(11) default NULL,
  `Visible` tinyint(1) NOT NULL default '1',
  `OrderNo` int(11) default NULL,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `meeting`
--

LOCK TABLES `meeting` WRITE;
/*!40000 ALTER TABLE `meeting` DISABLE KEYS */;
/*!40000 ALTER TABLE `meeting` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `module`
--

DROP TABLE IF EXISTS `module`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `module` (
  `Id` int(11) NOT NULL auto_increment,
  `Template` varchar(50) collate utf8_turkish_ci NOT NULL,
  `Region` varchar(50) collate utf8_turkish_ci NOT NULL,
  `OrderNo` int(11) NOT NULL default '0',
  `Name` varchar(30) collate utf8_turkish_ci NOT NULL,
  `CSS` text collate utf8_turkish_ci,
  `Details` text collate utf8_turkish_ci,
  `TopHtml` text collate utf8_turkish_ci,
  `BottomHtml` text collate utf8_turkish_ci,
  `ParentModuleId` int(11) NOT NULL default '0',
  `CSSClass` varchar(100) collate utf8_turkish_ci default NULL,
  `Visible` tinyint(1) default NULL,
  `RoleToRead` varchar(100) collate utf8_turkish_ci default NULL,
  `UseCache` varchar(30) collate utf8_turkish_ci NOT NULL default 'Default',
  `CacheLifeTime` int(11) default NULL,
  `ElementId` varchar(50) collate utf8_turkish_ci NOT NULL,
  `ElementName` varchar(20) collate utf8_turkish_ci NOT NULL,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=107 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `module`
--

LOCK TABLES `module` WRITE;
/*!40000 ALTER TABLE `module` DISABLE KEYS */;
INSERT INTO `module` VALUES (50,'Login.aspx','Content',0,'LoginForm2','','Cinar.CMS.Serialization\nInnerHtml,1495,$\r\nif (Provider.Session[\'loginError\'])\r\n{\r\n    echo(\'<p class=\"text-danger\">\' + Provider.Session[\'loginError\'] + \'</p>\');\r\n	Provider.Session.Remove(\'loginError\');\r\n}\r\n$\r\n<form accept-charset=\"UTF-8\" role=\"form\" id=\"fLogin\" method=\"post\" action=\"DoLogin.ashx\">\r\n	<input type=\"hidden\" name=\"RedirectURL\" value=\"$=Provider.Request.RedirectURL ? Provider.Server.HtmlEncode(Provider.Request.RedirectURL) : Provider.Configuration.MainPage$\"/>\r\n	<fieldset>\r\n		<label class=\"block clearfix\">\r\n			<span class=\"block input-icon input-icon-right\">\r\n				<input type=\"text\" id=\"username\" name=\"Email\" class=\"form-control\" placeholder=\"E-mail\"/>\r\n				<i class=\"icon-user\"></i>\r\n			</span>\r\n		</label>\r\n\r\n		<label class=\"block clearfix\">\r\n			<span class=\"block input-icon input-icon-right\">\r\n				<input type=\"password\" id=\"pass\" class=\"form-control\" placeholder=\"Password\" name=\"Passwd\" />\r\n				<i class=\"icon-lock\"></i>\r\n			</span>\r\n		</label>\r\n\r\n		<div class=\"checkbox\">\r\n	    	<label>\r\n	    		<input name=\"RememberMe\" type=\"checkbox\" value=\"1\"> Remember Me\r\n	    	</label>\r\n	    </div>\r\n		\r\n		<div class=\"space\"></div>\r\n\r\n		<div class=\"clearfix\">\r\n			<button type=\"submit\" class=\"width-35 pull-right btn btn-sm btn-primary\" id=\"login\">\r\n				<i class=\"icon-key\"></i>\r\n				Login\r\n			</button>\r\n		</div>\r\n\r\n		<div class=\"space-4\"></div>\r\n	</fieldset>\r\n</form>\r\n\r\n<script>\r\n    \\$(function(){\r\n        if(getCookie(\'keyword\'))\r\n            \\$(\'input[name=RememberMe]\').prop(\'checked\', true);\r\n    });\r\n</script>Id,2,50Template,10,Login.aspxRegion,7,ContentOrderNo,1,0Name,10,LoginForm2CSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,3,rowVisible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0ElementName,3,divElementId,0,',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL,'','div'),(55,'Default.aspx','navbar',0,'StaticHtml','','Cinar.CMS.Serialization\nInnerHtml,7290,    <div class=\"navbar-header pull-left\">\r\n		<a href=\"/\" class=\"navbar-brand\">\r\n			<small>\r\n				<img src=\"/UserFiles/assets/cinar.png\" class=\"msg-photo\" style=\"width: 33px;position: absolute;top: 6px;\" />\r\n				&nbsp; &nbsp; &nbsp; &nbsp; $=Provider.Configuration.SiteName$\r\n			</small>\r\n		</a><!-- /.brand -->\r\n	</div><!-- /.navbar-header -->\r\n\r\n	<div class=\"navbar-header pull-right\" role=\"navigation\">\r\n		<ul class=\"nav ace-nav\">\r\n			<li class=\"grey\">\r\n				<a data-toggle=\"dropdown\" class=\"dropdown-toggle\" href=\"#\">\r\n					<i class=\"icon-tasks\"></i>\r\n					<span class=\"badge badge-grey\">4</span>\r\n				</a>\r\n\r\n				<ul class=\"pull-right dropdown-navbar dropdown-menu dropdown-caret dropdown-close\">\r\n					<li class=\"dropdown-header\">\r\n						<i class=\"icon-ok\"></i>\r\n						4 Tasks to complete\r\n					</li>\r\n\r\n					<li>\r\n						<a href=\"#\">\r\n							<div class=\"clearfix\">\r\n								<span class=\"pull-left\">Software Update</span>\r\n								<span class=\"pull-right\">65%</span>\r\n							</div>\r\n\r\n							<div class=\"progress progress-mini \">\r\n								<div style=\"width:65%\" class=\"progress-bar \"></div>\r\n							</div>\r\n						</a>\r\n					</li>\r\n\r\n					<li>\r\n						<a href=\"#\">\r\n							<div class=\"clearfix\">\r\n								<span class=\"pull-left\">Hardware Upgrade</span>\r\n								<span class=\"pull-right\">35%</span>\r\n							</div>\r\n\r\n							<div class=\"progress progress-mini \">\r\n								<div style=\"width:35%\" class=\"progress-bar progress-bar-danger\"></div>\r\n							</div>\r\n						</a>\r\n					</li>\r\n\r\n					<li>\r\n						<a href=\"#\">\r\n							<div class=\"clearfix\">\r\n								<span class=\"pull-left\">Unit Testing</span>\r\n								<span class=\"pull-right\">15%</span>\r\n							</div>\r\n\r\n							<div class=\"progress progress-mini \">\r\n								<div style=\"width:15%\" class=\"progress-bar progress-bar-warning\"></div>\r\n							</div>\r\n						</a>\r\n					</li>\r\n\r\n					<li>\r\n						<a href=\"#\">\r\n							<div class=\"clearfix\">\r\n								<span class=\"pull-left\">Bug Fixes</span>\r\n								<span class=\"pull-right\">90%</span>\r\n							</div>\r\n\r\n							<div class=\"progress progress-mini progress-striped active\">\r\n								<div style=\"width:90%\" class=\"progress-bar progress-bar-success\"></div>\r\n							</div>\r\n						</a>\r\n					</li>\r\n\r\n					<li>\r\n						<a href=\"#\">\r\n							See tasks with details\r\n							<i class=\"icon-arrow-right\"></i>\r\n						</a>\r\n					</li>\r\n				</ul>\r\n			</li>\r\n\r\n			<li class=\"purple\">\r\n				<a data-toggle=\"dropdown\" class=\"dropdown-toggle\" href=\"#\">\r\n					<i class=\"icon-bell-alt icon-animated-bell\"></i>\r\n					<span class=\"badge badge-important\">8</span>\r\n				</a>\r\n\r\n				<ul class=\"pull-right dropdown-navbar navbar-pink dropdown-menu dropdown-caret dropdown-close\">\r\n					<li class=\"dropdown-header\">\r\n						<i class=\"icon-warning-sign\"></i>\r\n						8 Notifications\r\n					</li>\r\n\r\n					<li>\r\n						<a href=\"#\">\r\n							<div class=\"clearfix\">\r\n								<span class=\"pull-left\">\r\n									<i class=\"btn btn-xs no-hover btn-pink icon-comment\"></i>\r\n									New Comments\r\n								</span>\r\n								<span class=\"pull-right badge badge-info\">+12</span>\r\n							</div>\r\n						</a>\r\n					</li>\r\n\r\n					<li>\r\n						<a href=\"#\">\r\n							<i class=\"btn btn-xs btn-primary icon-user\"></i>\r\n							Bob just signed up as an editor ...\r\n						</a>\r\n					</li>\r\n\r\n					<li>\r\n						<a href=\"#\">\r\n							<div class=\"clearfix\">\r\n								<span class=\"pull-left\">\r\n									<i class=\"btn btn-xs no-hover btn-success icon-shopping-cart\"></i>\r\n									New Orders\r\n								</span>\r\n								<span class=\"pull-right badge badge-success\">+8</span>\r\n							</div>\r\n						</a>\r\n					</li>\r\n\r\n					<li>\r\n						<a href=\"#\">\r\n							<div class=\"clearfix\">\r\n								<span class=\"pull-left\">\r\n									<i class=\"btn btn-xs no-hover btn-info icon-twitter\"></i>\r\n									Followers\r\n								</span>\r\n								<span class=\"pull-right badge badge-info\">+11</span>\r\n							</div>\r\n						</a>\r\n					</li>\r\n\r\n					<li>\r\n						<a href=\"#\">\r\n							See all notifications\r\n							<i class=\"icon-arrow-right\"></i>\r\n						</a>\r\n					</li>\r\n				</ul>\r\n			</li>\r\n\r\n			<li class=\"green\">\r\n				<a data-toggle=\"dropdown\" class=\"dropdown-toggle\" href=\"#\">\r\n					<i class=\"icon-envelope icon-animated-vertical\"></i>\r\n					<span class=\"badge badge-success\">5</span>\r\n				</a>\r\n\r\n				<ul class=\"pull-right dropdown-navbar dropdown-menu dropdown-caret dropdown-close\">\r\n					<li class=\"dropdown-header\">\r\n						<i class=\"icon-envelope-alt\"></i>\r\n						5 Messages\r\n					</li>\r\n\r\n					<li>\r\n						<a href=\"#\">\r\n							<img src=\"/UserFiles/assets/avatars/avatar.png\" class=\"msg-photo\" alt=\"Alex\'s Avatar\" />\r\n							<span class=\"msg-body\">\r\n								<span class=\"msg-title\">\r\n									<span class=\"blue\">Alex:</span>\r\n									Ciao sociis natoque penatibus et auctor ...\r\n								</span>\r\n\r\n								<span class=\"msg-time\">\r\n									<i class=\"icon-time\"></i>\r\n									<span>a moment ago</span>\r\n								</span>\r\n							</span>\r\n						</a>\r\n					</li>\r\n\r\n					<li>\r\n						<a href=\"#\">\r\n							<img src=\"/UserFiles/assets/avatars/avatar3.png\" class=\"msg-photo\" alt=\"Susan\'s Avatar\" />\r\n							<span class=\"msg-body\">\r\n								<span class=\"msg-title\">\r\n									<span class=\"blue\">Susan:</span>\r\n									Vestibulum id ligula porta felis euismod ...\r\n								</span>\r\n\r\n								<span class=\"msg-time\">\r\n									<i class=\"icon-time\"></i>\r\n									<span>20 minutes ago</span>\r\n								</span>\r\n							</span>\r\n						</a>\r\n					</li>\r\n\r\n					<li>\r\n						<a href=\"#\">\r\n							<img src=\"/UserFiles/assets/avatars/avatar4.png\" class=\"msg-photo\" alt=\"Bob\'s Avatar\" />\r\n							<span class=\"msg-body\">\r\n								<span class=\"msg-title\">\r\n									<span class=\"blue\">Bob:</span>\r\n									Nullam quis risus eget urna mollis ornare ...\r\n								</span>\r\n\r\n								<span class=\"msg-time\">\r\n									<i class=\"icon-time\"></i>\r\n									<span>3:15 pm</span>\r\n								</span>\r\n							</span>\r\n						</a>\r\n					</li>\r\n\r\n					<li>\r\n						<a href=\"inbox.html\">\r\n							See all messages\r\n							<i class=\"icon-arrow-right\"></i>\r\n						</a>\r\n					</li>\r\n				</ul>\r\n			</li>\r\n\r\n			<li class=\"light-blue\">\r\n				<a data-toggle=\"dropdown\" href=\"#\" class=\"dropdown-toggle\">\r\n					<img class=\"nav-user-photo\" src=\"/UserFiles/assets/avatars/user.jpg\" alt=\"Jason\'s Photo\" />\r\n					<span class=\"user-info\">\r\n						<small>Welcome<br/> <b>$=Provider.User.Nick$</b></small>\r\n						\r\n					</span>\r\n\r\n					<i class=\"icon-caret-down\"></i>\r\n				</a>\r\n\r\n				<ul class=\"user-menu pull-right dropdown-menu dropdown-yellow dropdown-caret dropdown-close\">\r\n                $\r\n                if(Provider.User.IsInRole(\'Designer\')){\r\n                $\r\n    				<li>\r\n						<a href=\"?DesignMode=$=Provider.DesignMode ? \'Off\':\'On\'$\">\r\n							<i class=\"icon-check$=Provider.DesignMode ? \'-empty\':\'\'$\"></i>\r\n							$=Provider.DesignMode ? \'View Mode\':\'Design Mode\'$\r\n						</a>\r\n					</li>\r\n                $\r\n                }\r\n                $\r\n                    <li>\r\n						<a href=\"#\">\r\n							<i class=\"icon-cog\"></i>\r\n							Settings\r\n						</a>\r\n					</li>\r\n\r\n					<li>\r\n						<a href=\"#\">\r\n							<i class=\"icon-user\"></i>\r\n							Profile\r\n						</a>\r\n					</li>\r\n\r\n					<li class=\"divider\"></li>\r\n\r\n					<li>\r\n						<a href=\"/DoLogin.ashx?logout=1\">\r\n							<i class=\"icon-off\"></i>\r\n							Logout\r\n						</a>\r\n					</li>\r\n				</ul>\r\n			</li>\r\n		</ul><!-- /.ace-nav -->\r\n	</div><!-- /.navbar-header -->\r\nId,2,55Template,12,Default.aspxRegion,6,navbarOrderNo,1,0Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,16,navbar-containerVisible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0ElementId,16,navbar-containerElementName,3,div',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL,'navbar-container','div'),(57,'Default.aspx','sidebar',0,'StaticHtml','','Cinar.CMS.Serialization\nInnerHtml,623,<div class=\"sidebar-shortcuts-large\" id=\"sidebar-shortcuts-large\">\r\n	<button class=\"btn btn-success\">\r\n		<i class=\"icon-signal\"></i>\r\n	</button>\r\n\r\n	<button class=\"btn btn-info\">\r\n		<i class=\"icon-pencil\"></i>\r\n	</button>\r\n\r\n	<button class=\"btn btn-warning\">\r\n		<i class=\"icon-group\"></i>\r\n	</button>\r\n\r\n	<button class=\"btn btn-danger\">\r\n		<i class=\"icon-cogs\"></i>\r\n	</button>\r\n</div>\r\n\r\n<div class=\"sidebar-shortcuts-mini\" id=\"sidebar-shortcuts-mini\">\r\n	<span class=\"btn btn-success\"></span>\r\n\r\n	<span class=\"btn btn-info\"></span>\r\n\r\n	<span class=\"btn btn-warning\"></span>\r\n\r\n	<span class=\"btn btn-danger\"></span>\r\n</div>Id,2,57ElementName,3,divElementId,17,sidebar-shortcutsTemplate,12,Default.aspxRegion,7,sidebarOrderNo,1,0Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,17,sidebar-shortcutsVisible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0Details,261,Cinar.CMS.Serialization\nInnerHtml,0,Id,2,57ElementName,3,divElementId,0,Template,12,Default.aspxRegion,7,sidebarOrderNo,1,0Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL,'sidebar-shortcuts','div'),(58,'Default.aspx','sidebar',1,'StaticHtml','','Cinar.CMS.Serialization\nInnerHtml,3853,$\r\n    function isMenuActive(page)\r\n    {\r\n        if (Provider.Request.RawUrl.Contains(page))\r\n            return \' class=\"active\"\';\r\n        else\r\n            return \'\';\r\n    }\r\n$\r\n\r\n<li>\r\n    <a href=\"#\" class=\"dropdown-toggle\">\r\n		<i class=\"icon-home\"></i>\r\n		<span class=\"menu-text\"> Welcome </span>\r\n		<b class=\"arrow icon-angle-down\"></b>\r\n	</a>\r\n\r\n    <ul class=\"submenu\">\r\n    	<li $=isMenuActive(\'/Default\')$>\r\n			<a href=\"/Default.aspx\">\r\n		        <i class=\"icon-dashboard\"></i>	\r\n		        Dashboard \r\n			</a>\r\n		</li>\r\n        <li $=isMenuActive(\'/Profile\')$>\r\n			<a href=\"/Profile.aspx\">\r\n		        <i class=\"icon-user\"></i>	\r\n		        Profile \r\n			</a>\r\n		</li>\r\n    	<li $=isMenuActive(\'/Settings\')$>\r\n			<a href=\"/Settings.aspx\">\r\n		        <i class=\"icon-cog\"></i>	\r\n		        Settings \r\n			</a>\r\n		</li>\r\n	</ul>\r\n    \r\n</li>\r\n\r\n<li>\r\n	<a href=\"#\" class=\"dropdown-toggle\">\r\n		<i class=\"icon-th-list\"></i>\r\n		<span class=\"menu-text\"> CMS </span>\r\n		<b class=\"arrow icon-angle-down\"></b>\r\n	</a>\r\n\r\n	<ul class=\"submenu\">\r\n		<li $=isMenuActive(\'/CmsContent\')$>\r\n			<a href=\"/CmsContents.aspx\">\r\n		        <i class=\"icon-hdd\"></i>	\r\n		        Site Content \r\n			</a>\r\n		</li>\r\n\r\n		<li $=isMenuActive(\'/CmsAuthor\')$>\r\n			<a href=\"/CmsAuthors.aspx\">\r\n				<i class=\"icon-th-list\"></i>\r\n				Authors\r\n			</a>\r\n		</li>\r\n\r\n		<li $=isMenuActive(\'/CmsSource\')$>\r\n			<a href=\"/CmsSources.aspx\">\r\n				<i class=\"icon-folder-close\"></i>\r\n				Sources\r\n			</a>\r\n		</li>\r\n\r\n        <li $=isMenuActive(\'/CmsTag\')$>\r\n			<a href=\"/CmsTags.aspx\">\r\n				<i class=\"icon-film\"></i>\r\n				Tags\r\n			</a>\r\n		</li>  \r\n	</ul>\r\n</li>\r\n\r\n\r\n<li>\r\n    <a href=\"/Modules/Archive/\" class=\"dropdown-toggle\">\r\n		<i class=\"icon-user\"></i>\r\n		<span class=\"menu-text\"> CRM </span>\r\n\r\n		<b class=\"arrow icon-angle-down\"></b>\r\n	</a>\r\n\r\n	<ul class=\"submenu\">\r\n		<li $=isMenuActive(\'/CrmCompany\')$>\r\n			<a href=\"/CrmCompany.aspx\">\r\n		        <i class=\"icon-hdd\"></i>	\r\n		        Companies\r\n			</a>\r\n		</li>\r\n\r\n		<li $=isMenuActive(\'/CrmContact\')$>\r\n			<a href=\"/CrmContacts.aspx\">\r\n				<i class=\"icon-th-list\"></i>\r\n				Contacts\r\n			</a>\r\n		</li>\r\n\r\n        <li $=isMenuActive(\'/CrmEvent\')$>\r\n    		<a href=\"/CrmEvents.aspx\">\r\n				<i class=\"icon-film\"></i>\r\n				Events\r\n			</a>\r\n		</li>  \r\n\r\n		<li $=isMenuActive(\'/CrmTask\')$>\r\n			<a href=\"/CrmTasks.aspx\">\r\n				<i class=\"icon-folder-close\"></i>\r\n				Tasks\r\n			</a>\r\n		</li>\r\n	</ul>\r\n</li>\r\n\r\n<li>\r\n	<a href=\"#\" class=\"dropdown-toggle\">\r\n		<i class=\"icon-shopping-cart\"></i>\r\n		<span class=\"menu-text\"> E-Commerce </span>\r\n\r\n		<b class=\"arrow icon-angle-down\"></b>\r\n	</a>\r\n\r\n	<ul class=\"submenu\">\r\n		<li $=isMenuActive(\'/CommmerceProduct\')$>\r\n			<a href=\"/CommmerceProducts.aspx\">\r\n				<i class=\"icon-edit\"></i>\r\n				Products\r\n			</a>\r\n		</li>\r\n        <li $=isMenuActive(\'/CommmerceOrder\')$>\r\n			<a href=\"/CommmerceOrders.aspx\">\r\n				<i class=\"icon-edit\"></i>\r\n				Orders\r\n			</a>\r\n		</li>\r\n	</ul>\r\n</li>\r\n\r\n<li>\r\n	<a href=\"#\" class=\"dropdown-toggle\">\r\n		<i class=\"icon-lock\"></i>\r\n		<span class=\"menu-text\"> Admin </span>\r\n\r\n		<b class=\"arrow icon-angle-down\"></b>\r\n	</a>\r\n\r\n    <ul class=\"submenu\">\r\n    	<li $=isMenuActive(\'/AdminUser\')$>\r\n			<a href=\"/AdminUsers.aspx\">\r\n			<i class=\"icon-user\"></i> 	\r\n				Users \r\n			</a>\r\n		</li>\r\n    	<li $=isMenuActive(\'/AdminLang\')$>\r\n			<a href=\"/AdminLangs.aspx\">\r\n			<i class=\"icon-user\"></i> 	\r\n				Languages\r\n			</a>\r\n		</li>\r\n        <li $=isMenuActive(\'/AdminTran\')$>\r\n			<a href=\"/AdminTrans.aspx\">\r\n			<i class=\"icon-user\"></i> 	\r\n				Translations\r\n			</a>\r\n		</li>\r\n        <li $=isMenuActive(\'/AdminDefinition\')$>\r\n    		<a href=\"/AdminDefinitions.aspx\">\r\n			<i class=\"icon-user\"></i> 	\r\n				Definitions\r\n			</a>\r\n		</li>\r\n        <li $=isMenuActive(\'/AdminLogs\')$>\r\n    		<a href=\"/AdminLogs.aspx\">\r\n			<i class=\"icon-user\"></i> 	\r\n				Logs\r\n			</a>\r\n		</li>\r\n	</ul>\r\n	\r\n</li>\r\nId,2,58ElementName,2,ulElementId,0,Template,12,Default.aspxRegion,7,sidebarOrderNo,1,1Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,12,nav nav-listVisible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0Details,5146,Cinar.CMS.Serialization\nInnerHtml,1674,<li>\r\n	<a href=\"/Default.aspx\">\r\n		<i class=\"icon-dashboard\"></i>\r\n		<span class=\"menu-text\"> Dashboard </span>\r\n	</a>\r\n</li>\r\n\r\n<li>\r\n	<a href=\"/Modules/Archive/\" class=\"dropdown-toggle\">\r\n		<i class=\"icon-th-list\"></i>\r\n		<span class=\"menu-text\"> CMS </span>\r\n\r\n		<b class=\"arrow icon-angle-down\"></b>\r\n	</a>\r\n\r\n	<ul class=\"submenu\">\r\n		<li>\r\n			<a href=\"/Modules/Archive/ListStorage.aspx\">\r\n		        <i class=\"icon-hdd\"></i>	\r\n		        İçerikler \r\n			</a>\r\n		</li>\r\n\r\n		<li>\r\n			<a href=\"/Modules/Archive/ListChannel.aspx\">\r\n				<i class=\"icon-th-list\"></i>\r\n				Yazarlar\r\n			</a>\r\n		</li>\r\n\r\n		<li>\r\n			<a href=\"/Modules/Archive/ListFolder.aspx\">\r\n				<i class=\"icon-folder-close\"></i>\r\n				Kaynaklar\r\n			</a>\r\n		</li>\r\n\r\n        <li>\r\n			<a href=\"/Modules/Archive/ListVersion.aspx\">\r\n				<i class=\"icon-film\"></i>\r\n				Etiketler\r\n			</a>\r\n		</li>  \r\n	</ul>\r\n</li>\r\n\r\n<li>\r\n	<a href=\"#\" class=\"dropdown-toggle\">\r\n		<i class=\"icon-facetime-video\"></i>\r\n		<span class=\"menu-text\"> E-Ticaret </span>\r\n\r\n		<b class=\"arrow icon-angle-down\"></b>\r\n	</a>\r\n\r\n	<ul class=\"submenu\">\r\n		<li>\r\n			<a href=\"/Modules/Cast/ListPayment.aspx\">\r\n				<i class=\"icon-edit\"></i>\r\n				Ürünler\r\n			</a>\r\n		</li>\r\n        <li>\r\n			<a href=\"/Modules/Cast/ListCastPaymentCustomer.aspx\">\r\n				<i class=\"icon-edit\"></i>\r\n				Siparişler\r\n			</a>\r\n		</li>\r\n	</ul>\r\n</li>\r\n\r\n<li>\r\n	<a href=\"#\" class=\"dropdown-toggle\">\r\n		<i class=\"icon-lock\"></i>\r\n		<span class=\"menu-text\"> Yönetim </span>\r\n\r\n		<b class=\"arrow icon-angle-down\"></b>\r\n	</a>\r\n\r\n    <ul class=\"submenu\">\r\n		<li>\r\n			<a href=\"/AdminUsers.aspx\">\r\n			<i class=\"icon-user\"></i> 	\r\n				Kullanıcılar \r\n			</a>\r\n		</li>\r\n\r\n	</ul>\r\n	\r\n</li>\r\nId,2,58ElementName,2,ulElementId,0,Template,12,Default.aspxRegion,7,sidebarOrderNo,1,1Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0Details,3196,Cinar.CMS.Serialization\nInnerHtml,2647,    					<li>\r\n							<a href=\"/Modules/Dashboard/Dashboard.aspx\">\r\n								<i class=\"icon-dashboard\"></i>\r\n								<span class=\"menu-text\"> Dashboard </span>\r\n							</a>\r\n						</li>\r\n\r\n						<li>\r\n							<a href=\"/Modules/Archive/\" class=\"dropdown-toggle\">\r\n								<i class=\"icon-th-list\"></i>\r\n								<span class=\"menu-text\"> Arşiv </span>\r\n\r\n								<b class=\"arrow icon-angle-down\"></b>\r\n							</a>\r\n\r\n							<ul class=\"submenu\">\r\n								<li>\r\n									<a href=\"/Modules/Archive/ListStorage.aspx\">\r\n								        <i class=\"icon-hdd\"></i>	\r\n								        Storage Alanları \r\n									</a>\r\n								</li>\r\n\r\n								<li>\r\n									<a href=\"/Modules/Archive/ListChannel.aspx\">\r\n										<i class=\"icon-th-list\"></i>\r\n										Kanallar\r\n									</a>\r\n								</li>\r\n\r\n								<li>\r\n									<a href=\"/Modules/Archive/ListFolder.aspx\">\r\n										<i class=\"icon-folder-close\"></i>\r\n										Klasörler\r\n									</a>\r\n								</li>\r\n\r\n                                <li>\r\n									<a href=\"/Modules/Archive/ListVersion.aspx\">\r\n										<i class=\"icon-film\"></i>\r\n										Versiyonlar\r\n									</a>\r\n								</li>  \r\n							</ul>\r\n						</li>\r\n\r\n                        <li>\r\n							<a href=\"#\" class=\"dropdown-toggle\">\r\n								<i class=\"icon-facetime-video\"></i>\r\n								<span class=\"menu-text\"> Cast </span>\r\n\r\n								<b class=\"arrow icon-angle-down\"></b>\r\n							</a>\r\n\r\n							<ul class=\"submenu\">\r\n								<li>\r\n									<a href=\"/Modules/Cast/ListPayment.aspx\">\r\n										<i class=\"icon-edit\"></i>\r\n										Ödeme Listesi Oluştur\r\n									</a>\r\n								</li>\r\n                                <li>\r\n									<a href=\"/Modules/Cast/ListCastPaymentCustomer.aspx\">\r\n										<i class=\"icon-edit\"></i>\r\n										Müşteri Ekle\r\n									</a>\r\n								</li>\r\n							</ul>\r\n						</li>\r\n\r\n                        <li>\r\n							<a href=\"#\" class=\"dropdown-toggle\">\r\n								<i class=\"icon-lock\"></i>\r\n								<span class=\"menu-text\"> Yönetim </span>\r\n\r\n								<b class=\"arrow icon-angle-down\"></b>\r\n							</a>\r\n\r\n                            <ul class=\"submenu\">\r\n								<li>\r\n									<a href=\"/Modules/Management/ListMember.aspx\">\r\n									<i class=\"icon-user\"></i> 	\r\n										Kullanıcılar \r\n									</a>\r\n								</li>\r\n\r\n                                <li>\r\n									<a href=\"/Modules/Management/ListRole.aspx\">\r\n										<i class=\"icon-th-large\"></i>\r\n										Roller\r\n									</a>\r\n								</li>\r\n\r\n                                <li>\r\n									<a href=\"/Modules/Management/ListRight.aspx\">\r\n										<i class=\"icon-th\"></i>\r\n										Yetkiler\r\n									</a>\r\n								</li>\r\n							</ul>\r\n							\r\n						</li>\r\nId,2,58ElementName,2,ulElementId,0,Template,12,Default.aspxRegion,7,sidebarOrderNo,1,1Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,12,nav nav-listVisible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0Details,261,Cinar.CMS.Serialization\nInnerHtml,0,Id,2,58ElementName,3,divElementId,0,Template,12,Default.aspxRegion,7,sidebarOrderNo,1,1Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL,'','ul'),(60,'Default.aspx','breadcrumbs',0,'StaticHtml','','Cinar.CMS.Serialization\nInnerHtml,163,<li>\r\n	<i class=\"icon-home home-icon\"></i>\r\n	<a href=\"/Default.aspx\">Home</a>\r\n</li>\r\n<li>\r\n	<a href=\"#\">Other Pages</a>\r\n</li>\r\n<li class=\"active\">Blank Page</li>Id,2,60ElementName,2,ulElementId,0,Template,12,Default.aspxRegion,11,breadcrumbsOrderNo,1,0Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,10,breadcrumbVisible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0Details,266,Cinar.CMS.Serialization\nInnerHtml,0,Id,2,60ElementName,3,divElementId,0,Template,12,Default.aspxRegion,11,breadcrumbsOrderNo,1,0Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL,'','ul'),(61,'Default.aspx','breadcrumbs',1,'StaticHtml','','Cinar.CMS.Serialization\nInnerHtml,236,<form class=\"form-search\">\r\n	<span class=\"input-icon\">\r\n		<input type=\"text\" placeholder=\"Search ...\" class=\"nav-search-input\" id=\"nav-search-input\" autocomplete=\"off\" />\r\n		<i class=\"icon-search nav-search-icon\"></i>\r\n	</span>\r\n</form>Id,2,61ElementName,3,divElementId,10,nav-searchTemplate,12,Default.aspxRegion,11,breadcrumbsOrderNo,1,1Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,10,nav-searchVisible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0Details,266,Cinar.CMS.Serialization\nInnerHtml,0,Id,2,61ElementName,3,divElementId,0,Template,12,Default.aspxRegion,11,breadcrumbsOrderNo,1,1Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL,'nav-search','div'),(62,'Default.aspx','breadcrumbs',2,'StaticHtml','','Cinar.CMS.Serialization\nInnerHtml,1695,    				<div class=\"btn btn-app btn-xs btn-warning ace-settings-btn\" id=\"ace-settings-btn\">\r\n						<i class=\"icon-cog bigger-150\"></i>\r\n					</div>\r\n\r\n					<div class=\"ace-settings-box\" id=\"ace-settings-box\">\r\n						<div>\r\n							<div class=\"pull-left\">\r\n								<select id=\"skin-colorpicker\" class=\"hide\">\r\n									<option data-skin=\"default\" value=\"#438EB9\">#438EB9</option>\r\n									<option data-skin=\"skin-1\" value=\"#222A2D\">#222A2D</option>\r\n									<option data-skin=\"skin-2\" value=\"#C6487E\">#C6487E</option>\r\n									<option data-skin=\"skin-3\" value=\"#D0D0D0\">#D0D0D0</option>\r\n								</select>\r\n							</div>\r\n							<span>&nbsp; Tema Rengi</span>\r\n						</div>\r\n\r\n						<div>\r\n							<input type=\"checkbox\" class=\"ace ace-checkbox-2\" id=\"ace-settings-navbar\" />\r\n							<label class=\"lbl\" for=\"ace-settings-navbar\"> Logoyu Sabitle</label>\r\n						</div>\r\n\r\n						<div>\r\n							<input type=\"checkbox\" class=\"ace ace-checkbox-2\" id=\"ace-settings-sidebar\" />\r\n							<label class=\"lbl\" for=\"ace-settings-sidebar\"> Sol Menü Sabitle</label>\r\n						</div>\r\n\r\n						<div>\r\n							<input type=\"checkbox\" class=\"ace ace-checkbox-2\" id=\"ace-settings-breadcrumbs\" />\r\n							<label class=\"lbl\" for=\"ace-settings-breadcrumbs\">Breadcrumbs Sabitle</label>\r\n						</div>\r\n\r\n						<div>\r\n							<input type=\"checkbox\" class=\"ace ace-checkbox-2\" id=\"ace-settings-rtl\" />\r\n							<label class=\"lbl\" for=\"ace-settings-rtl\"> Sağdan Sola Menü</label>\r\n						</div>\r\n\r\n						<div>\r\n							<input type=\"checkbox\" class=\"ace ace-checkbox-2\" id=\"ace-settings-add-container\" />\r\n							<label class=\"lbl\" for=\"ace-settings-add-container\">\r\n								Sayfayı Ortala\r\n							</label>\r\n						</div>\r\n					</div>\r\nId,2,62ElementName,3,divElementId,22,ace-settings-containerTemplate,12,Default.aspxRegion,11,breadcrumbsOrderNo,1,2Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,22,ace-settings-containerVisible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0Details,266,Cinar.CMS.Serialization\nInnerHtml,0,Id,2,62ElementName,3,divElementId,0,Template,12,Default.aspxRegion,11,breadcrumbsOrderNo,1,2Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL,'ace-settings-container','div'),(64,'Default.aspx','contentDiv',8,'StaticHtml','','Cinar.CMS.Serialization\nInnerHtml,54,<h3 class=\"header smaller lighter blue\">Dashboard</h3>Id,2,64ElementName,3,divElementId,0,Template,12,Default.aspxRegion,10,contentDivOrderNo,1,8Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0Details,609,Cinar.CMS.Serialization\nInnerHtml,57,<h3 class=\"header smaller lighter blue\">Kullanıcılar</h3>Id,2,64ElementName,3,divElementId,0,Template,12,Default.aspxRegion,10,contentDivOrderNo,1,9Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,9,col-xs-12Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0Details,265,Cinar.CMS.Serialization\nInnerHtml,0,Id,2,64ElementName,3,divElementId,0,Template,12,Default.aspxRegion,10,contentDivOrderNo,1,9Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL,'','div'),(72,'AdminUsers.aspx','contentDiv',0,'StaticHtml','','Cinar.CMS.Serialization\nInnerHtml,1893,$\r\nvar sql = \"select \r\n                Id, \r\n                concat(Name,\' \',Surname) as FullName,\r\n                Email,\r\n                Roles\r\n            from\r\n                User\r\n            where\r\n                Id>2\";\r\n$\r\n\r\n<h3 class=\"header smaller lighter blue\">Kullanıcılar</h3>\r\n\r\n<div class=\"table-responsive\">\r\n    <div id=\"table-storage_wrapper\" class=\"dataTables_wrapper\" role=\"grid\">\r\n        <table id=\"table-entity\" class=\"table table-striped table-bordered table-hover dataTable\" aria-describedby=\"table-storage_info\">\r\n        </table>\r\n    </div>\r\n</div>\r\n\r\n<script>\r\n    \\$(function(){\r\n        var dt = $=Utility.ToJSON(Provider.Database.GetDataTable(sql))$;\r\n        bindTable({\r\n              list: dt.rows,\r\n              tableId: \"#table-entity\",\r\n              columns: [\r\n                  {\r\n                      \"mData\": \"Id\",\r\n                      \"bVisible\": false\r\n                  },\r\n                  {\r\n                      \"mData\": \"FullName\",\r\n                      \"sTitle\": \"Name\"\r\n                  },\r\n                  {\r\n                      \"mData\": \"Email\",\r\n                      \"sTitle\": \"Mail\"\r\n                  },\r\n                  {\r\n                      \"mData\": \"Roles\",\r\n                      \"sTitle\": \"Roles\"\r\n                  },\r\n                  {\r\n                      \"mData\": null,\r\n                      \"mRender\": function (data, type, full) {\r\n\r\n                          return \'<div class=\"visible-md visible-lg hidden-sm hidden-xs action-buttons\">\' +\r\n                                  \'<a class=\"green\" href=\"/AdminUsersEdit.aspx?Id=\'+full.Id+\'\"><i class=\"icon-pencil bigger-130\"></i></a>\' +\r\n                                  \'<a class=\"red\" href=\"#\"><i class=\"icon-trash bigger-130\" ></i></a></div>\'\r\n                      }\r\n                  }\r\n              ]\r\n          });\r\n    });\r\n    \r\n</script>\r\nId,2,72ElementName,3,divElementId,0,Template,15,AdminUsers.aspxRegion,10,contentDivOrderNo,1,0Name,10,StaticHtmlCSS,0,Details,609,Cinar.CMS.Serialization\nInnerHtml,57,<h3 class=\"header smaller lighter blue\">Kullanıcılar</h3>Id,2,64ElementName,3,divElementId,0,Template,12,Default.aspxRegion,10,contentDivOrderNo,1,9Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,9,col-xs-12Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0Details,265,Cinar.CMS.Serialization\nInnerHtml,0,Id,2,64ElementName,3,divElementId,0,Template,12,Default.aspxRegion,10,contentDivOrderNo,1,9Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL,'','div'),(74,'AdminUsers.aspx','navbar',0,'RegionRepeater','','Cinar.CMS.Serialization\nPage,12,Default.aspxRegionToCopy,6,navbarId,2,74ElementName,3,divElementId,0,Template,15,AdminUsers.aspxRegion,6,navbarOrderNo,1,0Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL,'','div'),(75,'AdminUsers.aspx','sidebar',0,'RegionRepeater','','Cinar.CMS.Serialization\nPage,12,Default.aspxRegionToCopy,7,sidebarId,2,75ElementName,3,divElementId,0,Template,15,AdminUsers.aspxRegion,7,sidebarOrderNo,1,0Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL,'','div'),(76,'AdminUsers.aspx','breadcrumbs',0,'RegionRepeater','','Cinar.CMS.Serialization\nPage,12,Default.aspxRegionToCopy,11,breadcrumbsId,2,76ElementName,3,divElementId,0,Template,15,AdminUsers.aspxRegion,11,breadcrumbsOrderNo,1,0Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL,'','div'),(78,'AdminLangs.aspx','contentDiv',0,'StaticHtml','','Cinar.CMS.Serialization\nInnerHtml,1828,<h3 class=\"header smaller lighter blue\">Diller</h3>\r\n\r\n            <div class=\"table-responsive\">\r\n                <div id=\"table-storage_wrapper\" class=\"dataTables_wrapper\" role=\"grid\">\r\n                    <table id=\"table-entity\" class=\"table table-striped table-bordered table-hover dataTable\" aria-describedby=\"table-storage_info\">\r\n                    </table>\r\n                </div>\r\n            </div>\r\n\r\n<script>\r\n    \\$(function(){\r\n              readEntityList(\'Lang\', \'\', \r\n                function (list) {\r\n                    bindTable({\r\n                          list: list,\r\n                          tableId: \"#table-entity\",\r\n                          columns: [\r\n                              {\r\n                                  \"mData\": \"Id\",\r\n                                  \"sTitle\": \"#\"\r\n                              },\r\n                              {\r\n                                  \"mData\": \"Name\",\r\n                                  \"sTitle\": \"Dil\"\r\n                              },\r\n                              {\r\n                                  \"mData\": null,\r\n\r\n                                  \"mRender\": function (data, type, full) {\r\n\r\n                                      return \'<div class=\"visible-md visible-lg hidden-sm hidden-xs action-buttons\">\' +\r\n                                              \'<a class=\"green\" onclick=\"editMember()\"  href=\"#\"><i class=\"icon-pencil bigger-130\"></i></a>\' +\r\n                                              \'<a class=\"red\" onclick=\"deleteFromTable(\\\\\'#table-entity\\\\\',\\\\\'Member.ashx?method=delete&id=\\\\\')\"  href=\"#\"><i class=\"icon-trash bigger-130\" ></i></a></div>\'\r\n                                  }\r\n                              }\r\n                          ]\r\n                      });\r\n                  });\r\n    });\r\n    \r\n</script>Id,2,78ElementName,3,divElementId,0,Template,15,AdminLangs.aspxRegion,10,contentDivOrderNo,1,0Name,10,StaticHtmlCSS,0,Details,609,Cinar.CMS.Serialization\nInnerHtml,57,<h3 class=\"header smaller lighter blue\">Kullanıcılar</h3>Id,2,64ElementName,3,divElementId,0,Template,12,Default.aspxRegion,10,contentDivOrderNo,1,9Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,9,col-xs-12Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0Details,265,Cinar.CMS.Serialization\nInnerHtml,0,Id,2,64ElementName,3,divElementId,0,Template,12,Default.aspxRegion,10,contentDivOrderNo,1,9Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL,'','div'),(79,'AdminLangs.aspx','navbar',0,'RegionRepeater','','Cinar.CMS.Serialization\nPage,12,Default.aspxRegionToCopy,6,navbarId,2,79ElementName,3,divElementId,0,Template,15,AdminLangs.aspxRegion,6,navbarOrderNo,1,0Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL,'','div'),(80,'AdminLangs.aspx','sidebar',0,'RegionRepeater','','Cinar.CMS.Serialization\nPage,12,Default.aspxRegionToCopy,7,sidebarId,2,80ElementName,3,divElementId,0,Template,15,AdminLangs.aspxRegion,7,sidebarOrderNo,1,0Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL,'','div'),(81,'AdminLangs.aspx','breadcrumbs',0,'RegionRepeater','','Cinar.CMS.Serialization\nPage,12,Default.aspxRegionToCopy,11,breadcrumbsId,2,81ElementName,3,divElementId,0,Template,15,AdminLangs.aspxRegion,11,breadcrumbsOrderNo,1,0Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL,'','div'),(83,'CmsContents.aspx','contentDiv',0,'StaticHtml','','Cinar.CMS.Serialization\nInnerHtml,2737,<h3 class=\"header smaller lighter blue\">Site Content</h3>\r\n\r\n            <div class=\"table-responsive\">\r\n                <div id=\"table-storage_wrapper\" class=\"dataTables_wrapper\" role=\"grid\">\r\n                    <table id=\"table-entity\" class=\"table table-striped table-bordered table-hover dataTable\" aria-describedby=\"table-storage_info\">\r\n                    </table>\r\n                </div>\r\n            </div>\r\n\r\n<script>\r\n    \\$(function(){\r\n              readEntityList(\'Content\', \'\', \r\n                function (list) {\r\n                    bindTable({\r\n                          list: list,\r\n                          tableId: \"#table-entity\",\r\n                          columns: [\r\n                              {\r\n                                  \"mData\": \"Id\",\r\n                                  \"sTitle\": \"#\"\r\n                              },\r\n                              {\r\n                                  \"mData\": \"Title\",\r\n                                  \"sTitle\": \"Title\"\r\n                              },\r\n                              {\r\n                                  \"mData\": \"PublishDate\",\r\n                                  \"sTitle\": \"Publish Date\",\r\n                                  \"mRender\": function(val){\r\n                                      return (new Date(val)).toDateString();\r\n                                  }\r\n                              },\r\n                              {\r\n                                  \"mData\": \"ClassName\",\r\n                                  \"sTitle\": \"Class\"\r\n                              },\r\n                              {\r\n                                  \"mData\": \"Visible\",\r\n                                  \"sTitle\": \"Active\",\r\n                                  \"mRender\": function (data, type, full) {\r\n                                      return data ? \'Yes\' : \'No\';\r\n                                  }\r\n                              },\r\n                              {\r\n                                  \"mData\": null,\r\n\r\n                                  \"mRender\": function (data, type, full) {\r\n\r\n                                      return \'<div class=\"visible-md visible-lg hidden-sm hidden-xs action-buttons\">\' +\r\n                                              \'<a class=\"green\" onclick=\"editMember()\"  href=\"#\"><i class=\"icon-pencil bigger-130\"></i></a>\' +\r\n                                              \'<a class=\"red\" onclick=\"deleteFromTable(\\\\\'#table-entity\\\\\',\\\\\'Member.ashx?method=delete&id=\\\\\')\"  href=\"#\"><i class=\"icon-trash bigger-130\" ></i></a></div>\'\r\n                                  }\r\n                              }\r\n                          ]\r\n                      });\r\n                  });\r\n    });\r\n    \r\n</script>Id,2,83ElementName,3,divElementId,0,Template,16,CmsContents.aspxRegion,10,contentDivOrderNo,1,0Name,10,StaticHtmlCSS,0,Details,609,Cinar.CMS.Serialization\nInnerHtml,57,<h3 class=\"header smaller lighter blue\">Kullanıcılar</h3>Id,2,64ElementName,3,divElementId,0,Template,12,Default.aspxRegion,10,contentDivOrderNo,1,9Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,9,col-xs-12Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0Details,265,Cinar.CMS.Serialization\nInnerHtml,0,Id,2,64ElementName,3,divElementId,0,Template,12,Default.aspxRegion,10,contentDivOrderNo,1,9Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL,'','div'),(84,'CmsContents.aspx','navbar',0,'RegionRepeater','','Cinar.CMS.Serialization\nPage,12,Default.aspxRegionToCopy,6,navbarId,2,84ElementName,3,divElementId,0,Template,16,CmsContents.aspxRegion,6,navbarOrderNo,1,0Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL,'','div'),(85,'CmsContents.aspx','sidebar',0,'RegionRepeater','','Cinar.CMS.Serialization\nPage,12,Default.aspxRegionToCopy,7,sidebarId,2,85ElementName,3,divElementId,0,Template,16,CmsContents.aspxRegion,7,sidebarOrderNo,1,0Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL,'','div'),(86,'CmsContents.aspx','breadcrumbs',0,'RegionRepeater','','Cinar.CMS.Serialization\nPage,12,Default.aspxRegionToCopy,11,breadcrumbsId,2,86ElementName,3,divElementId,0,Template,16,CmsContents.aspxRegion,11,breadcrumbsOrderNo,1,0Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL,'','div'),(87,'AdminUsersEdit.aspx','contentDiv',0,'StaticHtml','','Cinar.CMS.Serialization\nInnerHtml,4788,$\r\nvar listPage = \"AdminUsers.aspx\";\r\nvar entityName = \"User\";\r\nvar e = new User();\r\nvar id = int.Parse(Provider.Request.Id ?? \"0\");\r\nif(id>0)\r\n    e = Provider.Database.Read(typeof(User), id);\r\n\r\nif(Provider.Request.cmdName){\r\n    if(Provider.Request.cmdName==\"save\")\r\n    {\r\n        e.SetFieldsByPostData(Provider.Request.Form);\r\n        e.Save();\r\n        Provider.Response.Redirect(\"/\"+listPage);\r\n    }\r\n}\r\n$\r\n<script>\r\n    \\$(function(){\r\n        loadForm($=Utility.ToJSON(e)$);\r\n    });\r\n</script>\r\n\r\n<div class=\"page-header\">\r\n	<h1>\r\n		User\r\n		<small>\r\n			<i class=\"icon-double-angle-right\"></i>\r\n			Edit user details\r\n		</small>\r\n	</h1>\r\n</div>\r\n\r\n<form method=\"post\" action=\"$=Provider.Request.RawUrl$\" enctype=\"multipart/form-data\" class=\"form-horizontal\" role=\"form\" autocomplete=\"off\">\r\n<input type=\"hidden\" name=\"cmdName\" value=\"save\"/>\r\n<input type=\"hidden\" name=\"Id\" value=\"$=Provider.Request.Id ?? 0$\"/>\r\n<div class=\"row\">\r\n    <div class=\"col-sm-9\">\r\n            <div class=\"form-group\">\r\n            	<label class=\"col-sm-4 control-label no-padding-right\" for=\"Email\"> Email </label>\r\n                <div class=\"col-sm-8\">\r\n    			    <input type=\"text\" id=\"Email\" name=\"Email\" placeholder=\"\" class=\"col-sm-8\"/>\r\n                </div>\r\n        	</div>\r\n        \r\n            <div class=\"space-4\"></div>\r\n        \r\n        	<div class=\"form-group\">\r\n        		<label class=\"col-sm-4 control-label no-padding-right\" for=\"Password\"> Password </label>\r\n        		<div class=\"col-sm-8\">\r\n        			<input type=\"password\" id=\"Password\" name=\"Password\" placeholder=\"\" />\r\n        			<span class=\"help-inline\">\r\n        				<span class=\"middle\">Leave blank to unchange</span>\r\n        			</span>\r\n        		</div>\r\n        	</div>\r\n        \r\n            <div class=\"space-4\"></div>\r\n\r\n            <div class=\"form-group\">\r\n                <label class=\"col-sm-4 control-label no-padding-right\" for=\"Nick\"> Nick </label>\r\n                <div class=\"col-sm-8\">\r\n    			    <input type=\"text\" id=\"Nick\" name=\"Nick\" placeholder=\"\" />\r\n                </div>\r\n        	</div>\r\n        \r\n            <div class=\"space-4\"></div>\r\n\r\n            <div class=\"form-group\">\r\n                <label class=\"col-sm-4 control-label no-padding-right\" for=\"Roles\"> Roles </label>\r\n            	<div class=\"col-sm-8\">\r\n        			<select multiple=\"\" class=\"width-80 chosen-select\" id=\"Roles\" Name=\"Roles\" data-placeholder=\"Select roles\">\r\n                        <option value=\"User\">User</option>\r\n                        <option value=\"Editor\">Editor</option>\r\n                        <option value=\"Designer\">Designer</option>\r\n                    </select>\r\n        		</div>\r\n        	</div>\r\n\r\n    </div>\r\n    <div class=\"col-sm-3\">\r\n		<img name=\"Avatar\" class=\"img-responsive\" src=\"/UserFiles/assets/avatars/profile-pic.jpg\" />\r\n        <input type=\"file\" name=\"Avatar\" />\r\n    </div>\r\n</div>\r\n\r\n<div class=\"row\">\r\n    <div class=\"col-xs-12 col-sm-6\">\r\n        <h3 class=\"header smaller lighter blue\">Personal Details</h3>\r\n        <div class=\"form-group\">\r\n            <label for=\"Name\" class=\"col-sm-4 control-label no-padding-right\"> Name </label>\r\n			<div class=\"col-sm-8\"><input type=\"text\" id=\"Name\" name=\"Name\" placeholder=\"\" /></div>\r\n        </div>\r\n\r\n        <div class=\"form-group\">\r\n            <label for=\"Surname\" class=\"col-sm-4 control-label no-padding-right\"> Surname </label>\r\n			<div class=\"col-sm-8\"><input type=\"text\" id=\"Surname\" name=\"Surname\" placeholder=\"\" /></div>\r\n        </div>\r\n\r\n        <div class=\"form-group\">\r\n            <label for=\"Gender\" class=\"col-sm-4 control-label no-padding-right\"> Gender </label>\r\n    		<div class=\"col-sm-8\">\r\n                <select id=\"Gender\" name=\"Gender\">\r\n                    <option>Male</option>\r\n                    <option>Female</option>\r\n                </select>\r\n            </div>\r\n        </div>\r\n\r\n        <div class=\"form-group\">\r\n            <label for=\"BirthDate\" class=\"col-sm-4 control-label no-padding-right\"> BirthDate </label>\r\n    		<div class=\"col-sm-2 input-group\">\r\n                <input type=\"text\" class=\"date-picker\" id=\"BirthDate\" name=\"BirthDate\" data-date-format=\"dd-mm-yyyy\"/>\r\n				<span class=\"input-group-addon\">\r\n					<i class=\"icon-calendar bigger-110\"></i>\r\n				</span>\r\n            </div>\r\n        </div>\r\n\r\n    </div>\r\n    <div class=\"col-xs-12 col-sm-6\">\r\n        <h3 class=\"header smaller lighter blue\">Address</h3>        \r\n    </div>\r\n</div>\r\n\r\n\r\n<div class=\"clearfix form-actions\">\r\n	<div class=\"text-right\">\r\n		<button class=\"btn btn-info\" type=\"submit\">\r\n			<i class=\"icon-ok bigger-110\"></i>\r\n			Save\r\n		</button>\r\n\r\n		&nbsp; &nbsp; &nbsp;\r\n		<button class=\"btn\" type=\"button\">\r\n			<i class=\"icon-undo bigger-110\"></i>\r\n			Cancel\r\n		</button>\r\n	</div>\r\n</div>\r\n\r\n</form>\r\nId,2,87ElementName,3,divElementId,0,Template,19,AdminUsersEdit.aspxRegion,10,contentDivOrderNo,1,0Name,10,StaticHtmlCSS,0,Details,609,Cinar.CMS.Serialization\nInnerHtml,57,<h3 class=\"header smaller lighter blue\">Kullanıcılar</h3>Id,2,64ElementName,3,divElementId,0,Template,12,Default.aspxRegion,10,contentDivOrderNo,1,9Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,9,col-xs-12Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0Details,265,Cinar.CMS.Serialization\nInnerHtml,0,Id,2,64ElementName,3,divElementId,0,Template,12,Default.aspxRegion,10,contentDivOrderNo,1,9Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL,'','div'),(88,'AdminUsersEdit.aspx','navbar',0,'RegionRepeater','','Cinar.CMS.Serialization\nPage,12,Default.aspxRegionToCopy,6,navbarId,2,88ElementName,3,divElementId,0,Template,19,AdminUsersEdit.aspxRegion,6,navbarOrderNo,1,0Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL,'','div'),(89,'AdminUsersEdit.aspx','sidebar',0,'RegionRepeater','','Cinar.CMS.Serialization\nPage,12,Default.aspxRegionToCopy,7,sidebarId,2,89ElementName,3,divElementId,0,Template,19,AdminUsersEdit.aspxRegion,7,sidebarOrderNo,1,0Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL,'','div'),(90,'AdminUsersEdit.aspx','breadcrumbs',0,'RegionRepeater','','Cinar.CMS.Serialization\nPage,12,Default.aspxRegionToCopy,11,breadcrumbsId,2,90ElementName,3,divElementId,0,Template,19,AdminUsersEdit.aspxRegion,11,breadcrumbsOrderNo,1,0Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL,'','div'),(91,'AdminDefinitions.aspx','contentDiv',0,'StaticHtml','','Cinar.CMS.Serialization\nInnerHtml,1985,$\r\nvar editPage = \"AdminDefinitionEdit.aspx\";\r\nvar sql = \"select \r\n                Id, \r\n                Kind,\r\n                Name\r\n            from\r\n                Definition\";\r\n$\r\n\r\n<h3 class=\"header smaller lighter blue\">Definitions</h3>\r\n\r\n<div class=\"table-responsive\">\r\n    <div id=\"table-storage_wrapper\" class=\"dataTables_wrapper\" role=\"grid\">\r\n        <table id=\"table-entity\" class=\"table table-striped table-bordered table-hover dataTable\" aria-describedby=\"table-storage_info\">\r\n        </table>\r\n    </div>\r\n</div>\r\n\r\n<script>\r\n    \\$(function(){\r\n        var dt = $=Utility.ToJSON(Provider.Database.GetDataTable(sql))$;\r\n        bindTable({\r\n              list: dt.rows,\r\n              tableId: \"#table-entity\",\r\n              columns: [\r\n                  {\r\n                      \"mData\": \"Id\",\r\n                      \"mRender\": function (data, type, full) {\r\n                          return \'<input type=\"checkbox\" name=\"selected_row\" value=\"\'+full.Id+\'\"/>\'\r\n                      }\r\n                  },\r\n                  {\r\n                      \"mData\": \"Kind\",\r\n                      \"sTitle\": \"Kind\"\r\n                  },\r\n                  {\r\n                      \"mData\": \"Name\",\r\n                      \"sTitle\": \"Name\"\r\n                  },\r\n                  {\r\n                      \"mData\": null,\r\n                      \"mRender\": function (data, type, full) {\r\n\r\n                          return \'<a class=\"green\" href=\"/$=editPage$?Id=\'+full.Id+\'\"><i class=\"icon-pencil bigger-130\"></i></a> &nbsp; \' +\r\n                                  \'<a class=\"red\" href=\"#\"><i class=\"icon-trash bigger-130\" ></i></a>\'\r\n                      }\r\n                  }\r\n              ]\r\n          });\r\n    });\r\n    \r\n</script>\r\n\r\n<div class=\"clearfix form-actions\">\r\n    <div class=\"text-right\">\r\n		<button class=\"btn btn-info\" type=\"button\" onclick=\"location.href=\'/$=editPage$\';\">\r\n			<i class=\"icon-add bigger-110\"></i>\r\n			Add New\r\n		</button>\r\n	</div>\r\n</div>\r\nId,2,91ElementName,3,divElementId,0,Template,21,AdminDefinitions.aspxRegion,10,contentDivOrderNo,1,0Name,10,StaticHtmlCSS,0,Details,609,Cinar.CMS.Serialization\nInnerHtml,57,<h3 class=\"header smaller lighter blue\">Kullanıcılar</h3>Id,2,64ElementName,3,divElementId,0,Template,12,Default.aspxRegion,10,contentDivOrderNo,1,9Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,9,col-xs-12Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0Details,265,Cinar.CMS.Serialization\nInnerHtml,0,Id,2,64ElementName,3,divElementId,0,Template,12,Default.aspxRegion,10,contentDivOrderNo,1,9Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL,'','div'),(92,'AdminDefinitions.aspx','navbar',0,'RegionRepeater','','Cinar.CMS.Serialization\nPage,12,Default.aspxRegionToCopy,6,navbarId,2,92ElementName,3,divElementId,0,Template,21,AdminDefinitions.aspxRegion,6,navbarOrderNo,1,0Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL,'','div'),(93,'AdminDefinitions.aspx','sidebar',0,'RegionRepeater','','Cinar.CMS.Serialization\nPage,12,Default.aspxRegionToCopy,7,sidebarId,2,93ElementName,3,divElementId,0,Template,21,AdminDefinitions.aspxRegion,7,sidebarOrderNo,1,0Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL,'','div'),(94,'AdminDefinitions.aspx','breadcrumbs',0,'RegionRepeater','','Cinar.CMS.Serialization\nPage,12,Default.aspxRegionToCopy,11,breadcrumbsId,2,94ElementName,3,divElementId,0,Template,21,AdminDefinitions.aspxRegion,11,breadcrumbsOrderNo,1,0Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL,'','div'),(95,'AdminDefinitionEdit.aspx','contentDiv',0,'StaticHtml','','Cinar.CMS.Serialization\nInnerHtml,2229,$\r\nvar listPage = \"AdminDefinitions.aspx\";\r\nvar entityName = \"Definition\";\r\nvar e = new Definition();\r\nvar id = int.Parse(Provider.Request.Id ?? \"0\");\r\nif(id>0)\r\n    e = Provider.Database.Read(typeof(Definition), id);\r\n\r\nif(Provider.Request.cmdName){\r\n    if(Provider.Request.cmdName==\"save\")\r\n    {\r\n        e.SetFieldsByPostData(Provider.Request.Form);\r\n        e.Save();\r\n        Provider.Response.Redirect(\"/\"+listPage);\r\n    }\r\n}\r\n$\r\n<script>\r\n    \\$(function(){\r\n        loadForm($=Utility.ToJSON(e)$);\r\n    });\r\n</script>\r\n\r\n<div class=\"page-header\">\r\n	<h1>\r\n		$=entityName$\r\n		<small>\r\n			<i class=\"icon-double-angle-right\"></i>\r\n			Edit $=entityName.ToLowerInvariant()$ details\r\n		</small>\r\n	</h1>\r\n</div>\r\n\r\n<form method=\"post\" action=\"$=Provider.Request.RawUrl$\" class=\"form-horizontal\" role=\"form\" autocomplete=\"off\">\r\n<input type=\"hidden\" name=\"cmdName\" value=\"save\"/>\r\n<input type=\"hidden\" name=\"Id\" value=\"$=Provider.Request.Id ?? 0$\"/>\r\n\r\n<div class=\"row\">\r\n    <div class=\"col-xs-12 col-sm-12\">\r\n        <div class=\"form-group\">\r\n            <label for=\"Name\" class=\"col-sm-4 control-label no-padding-right\"> Name </label>\r\n			<div class=\"col-sm-8\"><input type=\"text\" id=\"Name\" name=\"Name\" placeholder=\"\" /></div>\r\n        </div>\r\n\r\n        <div class=\"form-group\">\r\n            <label for=\"Kind\" class=\"col-sm-4 control-label no-padding-right\"> Kind </label>\r\n    		<div class=\"col-sm-8\">\r\n                <select id=\"Kind\" name=\"Kind\">\r\n                    <option>Sector</option>\r\n                    <option>Title</option>\r\n                    <option>Country</option>\r\n                    <option>Language</option>\r\n                    <option value=\"ContactKind1\">Statu</option>\r\n                    <option value=\"CompanyTag\">Company Tags</option>\r\n                </select>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n\r\n<div class=\"clearfix form-actions\">\r\n	<div class=\"text-right\">\r\n		<button class=\"btn btn-info\" type=\"submit\">\r\n			<i class=\"icon-ok bigger-110\"></i>\r\n			Save\r\n		</button>\r\n\r\n		&nbsp; &nbsp; &nbsp;\r\n		<button class=\"btn\" type=\"button\" onclick=\"location.href=\'/$=listPage$\';\">\r\n			<i class=\"icon-undo bigger-110\"></i>\r\n			Cancel\r\n		</button>\r\n	</div>\r\n</div>\r\n\r\n</form>\r\nId,2,95ElementName,3,divElementId,0,Template,24,AdminDefinitionEdit.aspxRegion,10,contentDivOrderNo,1,0Name,3,GasCSS,0,Details,609,Cinar.CMS.Serialization\nInnerHtml,57,<h3 class=\"header smaller lighter blue\">Kullanıcılar</h3>Id,2,64ElementName,3,divElementId,0,Template,12,Default.aspxRegion,10,contentDivOrderNo,1,9Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,9,col-xs-12Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0Details,265,Cinar.CMS.Serialization\nInnerHtml,0,Id,2,64ElementName,3,divElementId,0,Template,12,Default.aspxRegion,10,contentDivOrderNo,1,9Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL,'','div'),(96,'AdminDefinitionEdit.aspx','navbar',0,'RegionRepeater','','Cinar.CMS.Serialization\nPage,12,Default.aspxRegionToCopy,6,navbarId,2,96ElementName,3,divElementId,0,Template,24,AdminDefinitionEdit.aspxRegion,6,navbarOrderNo,1,0Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL,'','div'),(97,'AdminDefinitionEdit.aspx','sidebar',0,'RegionRepeater','','Cinar.CMS.Serialization\nPage,12,Default.aspxRegionToCopy,7,sidebarId,2,97ElementName,3,divElementId,0,Template,24,AdminDefinitionEdit.aspxRegion,7,sidebarOrderNo,1,0Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL,'','div'),(98,'AdminDefinitionEdit.aspx','breadcrumbs',0,'RegionRepeater','','Cinar.CMS.Serialization\nPage,12,Default.aspxRegionToCopy,11,breadcrumbsId,2,98ElementName,3,divElementId,0,Template,24,AdminDefinitionEdit.aspxRegion,11,breadcrumbsOrderNo,1,0Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL,'','div'),(99,'CrmCompany.aspx','contentDiv',0,'StaticHtml','','Cinar.CMS.Serialization\nInnerHtml,4913,$\r\nvar editPage = \"CrmCompanyEdit.aspx\";\r\nvar sql = \"select \r\n                c.Id, \r\n                c.Name,\r\n                d.Name as Country,\r\n                c.ExtraField1,\r\n                u.Name as UserName,\r\n                d2.Name as Sector\r\n            from\r\n                Company c \r\n                left join Definition d ON d.Id=c.CountryId\r\n                left join Definition d2 ON d2.Id=c.SectorId\r\n                left join User u ON u.Id=c.UserId\r\n            where\r\n                1=1\";\r\n\r\nif(Provider.Request.CountryId>0)\r\n    sql += \" AND c.CountryId = \" + Int32.Parse(Provider.Request.CountryId);\r\nif(Provider.Request.UserId>0)\r\n    sql += \" AND c.UserId = \" + Int32.Parse(Provider.Request.UserId);\r\nif(Provider.Request.SectorId>0)\r\n    sql += \" AND c.SectorId = \" + Int32.Parse(Provider.Request.SectorId);\r\nif(Provider.Request.Sponsorship)\r\n    sql += \" AND c.ExtraField1 = \'yes\'\";\r\n    \r\n//echo(\'<pre>\'+sql+\'</pre>\');\r\n$\r\n\r\n<div class=\"page-header\">\r\n	<h1>\r\n		Companies\r\n		<small>\r\n			<i class=\"ace-icon fa fa-angle-double-right\"></i>\r\n			Sectoral database\r\n		</small>\r\n	</h1>\r\n</div>\r\n\r\n<h4 class=\"header smaller lighter red\" style=\"margin-left:10px\">Filter</h4>\r\n<form method=\"get\" action=\"CrmCompany.aspx\">\r\n    <div class=\"col-sm-3\">\r\n        <select id=\"CountryId\" name=\"CountryId\" class=\"width-100\">\r\n            <option value=\"0\">Select country</option>\r\n        </select>\r\n    </div>\r\n    <script>\r\n        \\$(function(){\r\n            fillSelectOptions(\'#CountryId\', \'Definition\', \'Kind=Country\');\r\n        });\r\n    </script>\r\n	<div class=\"col-sm-3\">\r\n        <select id=\"UserId\" name=\"UserId\" class=\"width-100\">\r\n            <option value=\"0\">Select user</option>\r\n        </select>\r\n    </div>\r\n    <script>\r\n        \\$(function(){\r\n            fillSelectOptions(\'#UserId\', \'User\', \'Id>2\');\r\n        });\r\n    </script>\r\n\r\n	<div class=\"col-sm-3\">\r\n        <select id=\"SectorId\" name=\"SectorId\" class=\"width-100\">\r\n            <option value=\"0\">Select sector</option>\r\n        </select>\r\n    </div>\r\n    <script>\r\n        \\$(function(){\r\n            fillSelectOptions(\'#SectorId\', \'Definition\', \'Kind=Sector\');\r\n        });\r\n    </script>\r\n\r\n    <label class=\"col-sm-1\">\r\n		<input name=\"Sponsorship\" id=\"Sponsorship\" class=\"ace ace-switch ace-switch-5\" value=\"yes\" type=\"checkbox\">\r\n		<span class=\"lbl\"></span>\r\n	</label>\r\n    \r\n    <div class=\"col-sm-2\">\r\n        <button type=\"submit\" class=\"btn btn-xs btn-success\">\r\n			Filter\r\n			<i class=\"icon-arrow-right icon-on-right bigger-110\"></i>\r\n		</button>\r\n    </div>\r\n</form>\r\n<br/><br/>\r\n\r\n<div class=\"table-responsive\">\r\n    <div id=\"table-storage_wrapper\" class=\"dataTables_wrapper\" role=\"grid\">\r\n        <table id=\"table-entity\" class=\"table table-striped table-bordered table-hover dataTable\" aria-describedby=\"table-storage_info\">\r\n        </table>\r\n    </div>\r\n</div>\r\n\r\n<script>\r\n    \\$(function(){\r\n        var dt = $=Utility.ToJSON(Provider.Database.GetDataTable(sql))$;\r\n        bindTable({\r\n              list: dt.rows,\r\n              tableId: \"#table-entity\",\r\n              columns: [\r\n                  {\r\n                      \"mData\": \"Id\",\r\n                      \"mRender\": function (data, type, full) {\r\n                          return \'<input type=\"checkbox\" name=\"selected_row\" value=\"\'+full.Id+\'\"/>\';\r\n                      }\r\n                  },\r\n                  {\r\n                      \"mData\": \"Name\",\r\n                      \"sTitle\": \"Company Name\",\r\n                      \"mRender\": function (data, type, full) {\r\n                          return \'<a href=\"/$=editPage$?Id=\'+full.Id+\'&mode=view\">\'+full.Name+\'</a>\';\r\n                      }\r\n                  },\r\n                  {\r\n                      \"mData\": \"Country\",\r\n                      \"sTitle\": \"Country\"\r\n                  },\r\n                  {\r\n                      \"mData\": \"UserName\",\r\n                      \"sTitle\": \"Hazar Contact\"\r\n                  },\r\n                  {\r\n                      \"mData\": \"Sector\",\r\n                      \"sTitle\": \"Sector\"\r\n                  },\r\n                  {\r\n                      \"mData\": \"ExtraField1\",\r\n                      \"sTitle\": \"Sp\"\r\n                  },\r\n                  {\r\n                      \"mData\": null,\r\n                      \"mRender\": function (data, type, full) {\r\n                          return \'<a class=\"green\" href=\"/$=editPage$?Id=\'+full.Id+\'\"><i class=\"icon-pencil bigger-130\"></i></a> &nbsp; \' +\r\n                                  \'<a class=\"red\" href=\"#\"><i class=\"icon-trash bigger-130\" ></i></a>\';\r\n                      }\r\n                  }\r\n              ]\r\n          });\r\n    });\r\n    \r\n</script>\r\n\r\n<div class=\"clearfix form-actions\">\r\n    <div class=\"text-right\">\r\n		<button class=\"btn btn-info\" type=\"button\" onclick=\"location.href=\'/$=editPage$\';\">\r\n			<i class=\"icon-add bigger-110\"></i>\r\n			Add New\r\n		</button>\r\n	</div>\r\n</div>\r\nId,2,99ElementName,3,divElementId,0,Template,15,CrmCompany.aspxRegion,10,contentDivOrderNo,1,0Name,10,StaticHtmlCSS,0,Details,609,Cinar.CMS.Serialization\nInnerHtml,57,<h3 class=\"header smaller lighter blue\">Kullanıcılar</h3>Id,2,64ElementName,3,divElementId,0,Template,12,Default.aspxRegion,10,contentDivOrderNo,1,9Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,9,col-xs-12Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0Details,265,Cinar.CMS.Serialization\nInnerHtml,0,Id,2,64ElementName,3,divElementId,0,Template,12,Default.aspxRegion,10,contentDivOrderNo,1,9Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL,'','div'),(100,'CrmCompany.aspx','navbar',0,'RegionRepeater','','Cinar.CMS.Serialization\nPage,12,Default.aspxRegionToCopy,6,navbarId,3,100ElementName,3,divElementId,0,Template,15,CrmCompany.aspxRegion,6,navbarOrderNo,1,0Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL,'','div'),(101,'CrmCompany.aspx','sidebar',0,'RegionRepeater','','Cinar.CMS.Serialization\nPage,12,Default.aspxRegionToCopy,7,sidebarId,3,101ElementName,3,divElementId,0,Template,15,CrmCompany.aspxRegion,7,sidebarOrderNo,1,0Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL,'','div'),(102,'CrmCompany.aspx','breadcrumbs',0,'RegionRepeater','','Cinar.CMS.Serialization\nPage,12,Default.aspxRegionToCopy,11,breadcrumbsId,3,102ElementName,3,divElementId,0,Template,15,CrmCompany.aspxRegion,11,breadcrumbsOrderNo,1,0Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL,'','div'),(103,'CrmCompanyEdit.aspx','contentDiv',0,'StaticHtml','','Cinar.CMS.Serialization\nInnerHtml,7672,$\r\nvar listPage = \"CrmCompany.aspx\";\r\nvar entityName = \"Company\";\r\nvar e = new Company();\r\nvar id = int.Parse(Provider.Request.Id ?? \"0\");\r\nif(id>0)\r\n    e = Provider.Database.Read(typeof(Company), id);\r\n\r\nif(Provider.Request.cmdName){\r\n    if(Provider.Request.cmdName==\"save\")\r\n    {\r\n        e.SetFieldsByPostData(Provider.Request.Form);\r\n        e.Save();\r\n        Provider.Response.Redirect(\"/\"+listPage);\r\n    }\r\n}\r\n$\r\n<script>\r\n    \\$(function(){\r\n        loadForm(\'#formCompany\', $=Utility.ToJSON(e)$, \'$=Provider.Request.mode$\');\r\n    });\r\n</script>\r\n\r\n<div class=\"page-header\">\r\n	<h1>\r\n		Company\r\n		<small>\r\n			<i class=\"icon-double-angle-right\"></i>\r\n			$=Provider.Request.mode ? \'View\' : \'Edit\'$ company details\r\n		</small>\r\n	</h1>\r\n</div>\r\n\r\n<form id=\"formCompany\" method=\"post\" action=\"$=Provider.Request.RawUrl$\" enctype=\"multipart/form-data\" class=\"form-horizontal\" role=\"form\" autocomplete=\"off\">\r\n<input type=\"hidden\" name=\"cmdName\" value=\"save\"/>\r\n<input type=\"hidden\" name=\"Id\" value=\"$=Provider.Request.Id ?? 0$\"/>\r\n<div class=\"row\">\r\n    <div class=\"col-sm-9\">\r\n            <div class=\"form-group\">\r\n        		<label class=\"col-sm-3 control-label no-padding-right\" for=\"Name\"> Name </label>\r\n        		<div class=\"col-sm-9\">\r\n        			<input type=\"text\" id=\"Name\" name=\"Name\" placeholder=\"\" class=\"col-sm-8\" />\r\n        		</div>\r\n        	</div>\r\n        \r\n            <div class=\"space-4\"></div>\r\n\r\n            <div class=\"form-group\">\r\n            	<label class=\"col-sm-3 control-label no-padding-right\" for=\"Email\"> Email </label>\r\n                <div class=\"col-sm-9\">\r\n    			    <input type=\"text\" id=\"Email\" name=\"Email\" placeholder=\"\" class=\"col-sm-6\"/>\r\n                </div>\r\n        	</div>\r\n        \r\n            <div class=\"space-4\"></div>\r\n            \r\n            <div class=\"form-group\">\r\n                <label class=\"col-sm-3 control-label no-padding-right\" for=\"Phone\">\r\n            		Phone\r\n            	</label>\r\n            \r\n            	<div class=\"col-sm-9 input-group\">\r\n            		<span class=\"input-group-addon\">\r\n            			<i class=\"icon-phone\"></i>\r\n            		</span>\r\n            		<input class=\"input-mask-phone\" type=\"text\" id=\"Phone\" name=\"Phone\">\r\n            	</div>\r\n            </div>            \r\n        \r\n            <div class=\"space-4\"></div>\r\n            \r\n            <div class=\"form-group\">\r\n                <label class=\"col-sm-3 control-label no-padding-right\" for=\"Phone2\">\r\n                	Phone 2\r\n            	</label>\r\n            \r\n            	<div class=\"col-sm-9 input-group\">\r\n            		<span class=\"input-group-addon\">\r\n            			<i class=\"icon-phone\"></i>\r\n            		</span>\r\n            		<input class=\"input-mask-phone\" type=\"text\" id=\"Phone2\" name=\"Phone2\">\r\n            	</div>\r\n            </div>            \r\n        \r\n            <div class=\"space-4\"></div>\r\n            \r\n            <div class=\"form-group\">\r\n                <label class=\"col-sm-3 control-label no-padding-right\" for=\"Fax\">\r\n                	Fax\r\n            	</label>\r\n            \r\n            	<div class=\"col-sm-9 input-group\">\r\n            		<span class=\"input-group-addon\">\r\n            			<i class=\"icon-print\"></i>\r\n            		</span>\r\n            		<input class=\"input-mask-phone\" type=\"text\" id=\"Fax\" name=\"Fax\">\r\n            	</div>\r\n            </div>            \r\n        \r\n            <div class=\"space-4\"></div>\r\n\r\n            <div class=\"form-group\">\r\n                <label class=\"col-sm-3 control-label no-padding-right\" for=\"Roles\"> Tags </label>\r\n            	<div class=\"col-sm-9\">\r\n        			<select multiple=\"\" class=\"width-80 chosen-select\" id=\"Tags\" Name=\"Tags\" data-placeholder=\"Select tags\">\r\n                    </select>\r\n                    <script>\r\n                        \\$(function(){\r\n                            fillSelectOptions(\'#Tags\', \'Definition\', \'Kind=CompanyTag\');\r\n                        });\r\n                    </script>                    \r\n        		</div>\r\n        	</div>\r\n\r\n    </div>\r\n    <div class=\"col-sm-3\">\r\n		<img name=\"Picture\" class=\"img-responsive\" src=\"/UserFiles/assets/avatars/profile-pic.jpg\" />\r\n        <input type=\"file\" id=\"Picture\" name=\"Picture\" />\r\n    </div>\r\n</div>\r\n\r\n<div class=\"row\">\r\n    <div class=\"col-xs-12 col-sm-6\">\r\n        <h3 class=\"header smaller lighter blue\">Details</h3>\r\n        <div class=\"form-group\">\r\n            <label for=\"SectorId\" class=\"col-sm-4 control-label no-padding-right\"> Sector </label>\r\n    		<div class=\"col-sm-8\">\r\n                <select id=\"SectorId\" name=\"SectorId\" class=\"width-80\">\r\n                    <option value=\"0\">Select sector</option>\r\n                </select>\r\n            </div>\r\n            <script>\r\n                \\$(function(){\r\n                    fillSelectOptions(\'#SectorId\', \'Definition\', \'Kind=Sector\');\r\n                });\r\n            </script>\r\n        </div>\r\n\r\n        <div class=\"form-group\">\r\n            <label for=\"UserId\" class=\"col-sm-4 control-label no-padding-right\"> Hazar Contact </label>\r\n    		<div class=\"col-sm-8\">\r\n                <select id=\"UserId\" name=\"UserId\" class=\"width-80\">\r\n                    <option value=\"0\">Select user</option>\r\n                </select>\r\n            </div>\r\n            <script>\r\n                \\$(function(){\r\n                    fillSelectOptions(\'#UserId\', \'User\', \'Id>2\');\r\n                });\r\n            </script>\r\n        </div>\r\n\r\n        <div class=\"form-group\">\r\n            <label for=\"ExtraField1\" class=\"col-sm-4 control-label no-padding-right\"> Sponsorship </label>\r\n        	<label class=\"col-sm-8\">\r\n        		<input name=\"ExtraField1\" id=\"ExtraField1\" class=\"ace ace-switch ace-switch-5\" value=\"yes\" type=\"checkbox\">\r\n        		<span class=\"lbl\"></span>\r\n        	</label>\r\n        </div>\r\n\r\n    </div>\r\n    <div class=\"col-xs-12 col-sm-6\">\r\n        <h3 class=\"header smaller lighter blue\">Address</h3>\r\n\r\n        <div class=\"form-group\">\r\n            <label for=\"AddressLine1\" class=\"col-sm-4 control-label no-padding-right\"> Adress Line 1 </label>\r\n            <div class=\"col-sm-8\">\r\n                <input type=\"text\" id=\"AddressLine1\" name=\"AddressLine1\"/>\r\n            </div>\r\n        </div>\r\n\r\n        <div class=\"form-group\">\r\n            <label for=\"AddressLine2\" class=\"col-sm-4 control-label no-padding-right\"> Line 2 </label>\r\n            <div class=\"col-sm-8\">\r\n                <input type=\"text\" id=\"AddressLine2\" name=\"AddressLine2\"/>\r\n            </div>\r\n        </div>\r\n\r\n        <div class=\"form-group\">\r\n            <label for=\"City\" class=\"col-sm-4 control-label no-padding-right\"> City </label>\r\n            <div class=\"col-sm-8\">\r\n                <input type=\"text\" id=\"City\" name=\"City\"/>\r\n            </div>\r\n        </div>\r\n        \r\n        <div class=\"form-group\">\r\n            <label for=\"CountryId\" class=\"col-sm-4 control-label no-padding-right\"> Country </label>\r\n        	<div class=\"col-sm-8\">\r\n                <select id=\"CountryId\" name=\"CountryId\" class=\"width-80\">\r\n                    <option value=\"0\">Select country</option>\r\n                </select>\r\n            </div>\r\n            <script>\r\n                \\$(function(){\r\n                    fillSelectOptions(\'#CountryId\', \'Definition\', \'Kind=Country\');\r\n                });\r\n            </script>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n\r\n<div class=\"clearfix form-actions\">\r\n	<div class=\"text-right\">\r\n		<button class=\"btn btn-info\" type=\"submit\">\r\n			<i class=\"icon-ok bigger-110\"></i>\r\n			Save\r\n		</button>\r\n\r\n		&nbsp; &nbsp; &nbsp;\r\n		<button class=\"btn\" type=\"button\">\r\n			<i class=\"icon-undo bigger-110\"></i>\r\n			Cancel\r\n		</button>\r\n	</div>\r\n</div>\r\n\r\n</form>\r\nId,3,103ElementName,3,divElementId,0,Template,19,CrmCompanyEdit.aspxRegion,10,contentDivOrderNo,1,0Name,10,StaticHtmlCSS,0,Details,609,Cinar.CMS.Serialization\nInnerHtml,57,<h3 class=\"header smaller lighter blue\">Kullanıcılar</h3>Id,2,64ElementName,3,divElementId,0,Template,12,Default.aspxRegion,10,contentDivOrderNo,1,9Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,9,col-xs-12Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0Details,265,Cinar.CMS.Serialization\nInnerHtml,0,Id,2,64ElementName,3,divElementId,0,Template,12,Default.aspxRegion,10,contentDivOrderNo,1,9Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL,'','div'),(104,'CrmCompanyEdit.aspx','navbar',0,'RegionRepeater','','Cinar.CMS.Serialization\nPage,12,Default.aspxRegionToCopy,6,navbarId,3,104ElementName,3,divElementId,0,Template,19,CrmCompanyEdit.aspxRegion,6,navbarOrderNo,1,0Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL,'','div'),(105,'CrmCompanyEdit.aspx','sidebar',0,'RegionRepeater','','Cinar.CMS.Serialization\nPage,12,Default.aspxRegionToCopy,7,sidebarId,3,105ElementName,3,divElementId,0,Template,19,CrmCompanyEdit.aspxRegion,7,sidebarOrderNo,1,0Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL,'','div'),(106,'CrmCompanyEdit.aspx','breadcrumbs',0,'RegionRepeater','','Cinar.CMS.Serialization\nPage,12,Default.aspxRegionToCopy,11,breadcrumbsId,3,106ElementName,3,divElementId,0,Template,19,CrmCompanyEdit.aspxRegion,11,breadcrumbsOrderNo,1,0Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0',NULL,NULL,0,NULL,NULL,NULL,'Default',NULL,'','div');
/*!40000 ALTER TABLE `module` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `post`
--

DROP TABLE IF EXISTS `post`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `post` (
  `Metin` varchar(200) collate utf8_turkish_ci default NULL,
  `Picture` varchar(100) collate utf8_turkish_ci default NULL,
  `OriginalPostId` int(11) default NULL,
  `ReplyToPostId` int(11) default NULL,
  `ThreadId` int(11) default NULL,
  `LangId` int(11) default NULL,
  `Lat` int(11) default NULL,
  `Lng` int(11) default NULL,
  `VideoId` varchar(100) collate utf8_turkish_ci default NULL,
  `VideoType` int(11) default NULL,
  `ShareCount` int(11) default NULL,
  `LikeCount` int(11) default NULL,
  `Id` int(11) NOT NULL auto_increment,
  `InsertDate` datetime NOT NULL default '1990-01-01 00:00:00',
  `InsertUserId` int(11) default NULL,
  `UpdateDate` datetime default NULL,
  `UpdateUserId` int(11) default NULL,
  `Visible` tinyint(1) NOT NULL default '1',
  `OrderNo` int(11) default NULL,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `post`
--

LOCK TABLES `post` WRITE;
/*!40000 ALTER TABLE `post` DISABLE KEYS */;
/*!40000 ALTER TABLE `post` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `source`
--

DROP TABLE IF EXISTS `source`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `source` (
  `UserId` int(11) default NULL,
  `Name` varchar(100) collate utf8_turkish_ci NOT NULL,
  `Description` text collate utf8_turkish_ci,
  `Picture` varchar(100) collate utf8_turkish_ci default NULL,
  `Picture2` varchar(100) collate utf8_turkish_ci default NULL,
  `Id` int(11) NOT NULL auto_increment,
  `InsertDate` datetime NOT NULL default '1990-01-01 00:00:00',
  `InsertUserId` int(11) default NULL,
  `UpdateDate` datetime default NULL,
  `UpdateUserId` int(11) default NULL,
  `Visible` tinyint(1) NOT NULL default '1',
  `OrderNo` int(11) default NULL,
  PRIMARY KEY  (`Id`)
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
  `DisplayName` varchar(100) collate utf8_turkish_ci default NULL,
  `Headline` tinyint(1) default '0',
  `ContentCount` int(11) default NULL,
  `Noise` tinyint(1) default '0',
  `Name` varchar(100) collate utf8_turkish_ci NOT NULL,
  `Description` text collate utf8_turkish_ci,
  `Picture` varchar(100) collate utf8_turkish_ci default NULL,
  `Picture2` varchar(100) collate utf8_turkish_ci default NULL,
  `Id` int(11) NOT NULL auto_increment,
  `InsertDate` datetime NOT NULL default '1990-01-01 00:00:00',
  `InsertUserId` int(11) default NULL,
  `UpdateDate` datetime default NULL,
  `UpdateUserId` int(11) default NULL,
  `Visible` tinyint(1) NOT NULL default '1',
  `OrderNo` int(11) default NULL,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tag`
--

LOCK TABLES `tag` WRITE;
/*!40000 ALTER TABLE `tag` DISABLE KEYS */;
INSERT INTO `tag` VALUES (NULL,0,1,0,'ürün',NULL,NULL,NULL,1,'2013-09-03 19:09:00',1,'2013-09-03 19:09:01',1,1,0),(NULL,0,1,0,'hizmet',NULL,NULL,NULL,2,'2013-09-03 19:09:01',1,'2013-09-03 19:09:01',1,1,0),(NULL,0,1,0,'hicret',NULL,NULL,NULL,5,'2013-09-03 19:32:42',1,'2013-09-03 19:32:42',1,1,0);
/*!40000 ALTER TABLE `tag` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `task`
--

DROP TABLE IF EXISTS `task`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `task` (
  `EventId` int(11) default NULL,
  `ContactId` int(11) default NULL,
  `AssignedToUserId` int(11) default NULL,
  `Details` text collate utf8_turkish_ci,
  `TaskState` varchar(16) collate utf8_turkish_ci default NULL,
  `Name` varchar(100) collate utf8_turkish_ci NOT NULL,
  `Description` text collate utf8_turkish_ci,
  `Picture` varchar(100) collate utf8_turkish_ci default NULL,
  `Picture2` varchar(100) collate utf8_turkish_ci default NULL,
  `Id` int(11) NOT NULL auto_increment,
  `InsertDate` datetime NOT NULL default '1990-01-01 00:00:00',
  `InsertUserId` int(11) default NULL,
  `UpdateDate` datetime default NULL,
  `UpdateUserId` int(11) default NULL,
  `Visible` tinyint(1) NOT NULL default '1',
  `OrderNo` int(11) default NULL,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `task`
--

LOCK TABLES `task` WRITE;
/*!40000 ALTER TABLE `task` DISABLE KEYS */;
/*!40000 ALTER TABLE `task` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `template`
--

DROP TABLE IF EXISTS `template`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `template` (
  `FileName` varchar(50) collate utf8_turkish_ci default NULL,
  `HTMLCode` text collate utf8_turkish_ci,
  `Id` int(11) NOT NULL auto_increment,
  `InsertDate` datetime NOT NULL default '1990-01-01 00:00:00',
  `InsertUserId` int(11) default NULL,
  `UpdateDate` datetime default NULL,
  `UpdateUserId` int(11) default NULL,
  `Visible` tinyint(1) NOT NULL default '1',
  `OrderNo` int(11) default NULL,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `template`
--

LOCK TABLES `template` WRITE;
/*!40000 ALTER TABLE `template` DISABLE KEYS */;
INSERT INTO `template` VALUES ('Default.aspx','<!DOCTYPE html>\r\n<html>\r\n<head>\r\n    	<link href=\"/UserFiles/assets/css/bootstrap.min.css\" rel=\"stylesheet\" />\r\n		<link rel=\"stylesheet\" href=\"/UserFiles/assets/css/font-awesome.min.css\" />\r\n\r\n        <!--link rel=\"stylesheet\" href=\"/UserFiles/assets/css/jquery-ui-1.10.3.full.min.css\" /-->\r\n    	<link rel=\"stylesheet\" href=\"/UserFiles/assets/css/datepicker.css\" />\r\n		<!--link rel=\"stylesheet\" href=\"/UserFiles/assets/css/ui.jqgrid.css\" /-->\r\n        <link rel=\"stylesheet\" href=\"/UserFiles/assets/css/chosen.css\" />\r\n		<!--link rel=\"stylesheet\" href=\"/UserFiles/assets/css/bootstrap-timepicker.css\" /-->\r\n		<link rel=\"stylesheet\" href=\"/UserFiles/assets/css/daterangepicker.css\" />\r\n		<!--link rel=\"stylesheet\" href=\"/UserFiles/assets/css/colorpicker.css\" /-->\r\n\r\n    	<!--[if IE 7]>\r\n		  <link rel=\"stylesheet\" href=\"/UserFiles/assets/css/font-awesome-ie7.min.css\" />\r\n		<![endif]-->\r\n\r\n		<!-- page specific plugin styles -->\r\n\r\n		<!-- fonts -->\r\n\r\n		<link rel=\"stylesheet\" href=\"/UserFiles/assets/css/ace-fonts.css\" />\r\n\r\n		<!-- ace styles -->\r\n\r\n		<link rel=\"stylesheet\" href=\"/UserFiles/assets/css/uncompressed/ace.css\" />\r\n		<link rel=\"stylesheet\" href=\"/UserFiles/assets/css/uncompressed/ace-rtl.css\" />\r\n		<link rel=\"stylesheet\" href=\"/UserFiles/assets/css/uncompressed/ace-skins.css\" />\r\n\r\n		<!--[if lte IE 8]>\r\n		  <link rel=\"stylesheet\" href=\"/UserFiles/assets/css/ace-ie.min.css\" />\r\n		<![endif]-->\r\n\r\n		<!-- inline styles related to this page -->\r\n\r\n		<!-- ace settings handler -->\r\n\r\n		<script src=\"/UserFiles/assets/js/ace-extra.min.js\"></script>\r\n		<!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->\r\n\r\n		<!--[if lt IE 9]>\r\n		<script src=\"/UserFiles/assets/js/html5shiv.js\"></script>\r\n		<script src=\"/UserFiles/assets/js/respond.min.js\"></script>\r\n		<![endif]-->\r\n\r\n    		<!--[if !IE]> -->\r\n\r\n		<script type=\"text/javascript\">\r\n		    window.jQuery || document.write(\"<script src=\'/UserFiles/assets/js/jquery-2.0.3.min.js\'>\" + \"<\" + \"/script>\");\r\n		</script>\r\n\r\n		<!-- <![endif]-->\r\n\r\n		<!--[if IE]>\r\n        <script type=\"text/javascript\">\r\n         window.jQuery || document.write(\"<script src=\'/UserFiles/assets/js/jquery-1.10.2.min.js\'>\"+\"<\"+\"/script>\");\r\n        </script>\r\n        <![endif]-->\r\n\r\n		<script type=\"text/javascript\">\r\n		    if (\"ontouchend\" in document) document.write(\"<script src=\'assets/js/jquery.mobile.custom.min.js\'>\" + \"<\" + \"/script>\");\r\n		</script>\r\n		<script src=\"/UserFiles/assets/js/bootstrap.min.js\"></script>\r\n		<script src=\"/UserFiles/assets/js/typeahead-bs2.min.js\"></script>\r\n\r\n		<!-- page specific plugin scripts -->\r\n\r\n		<!--[if lte IE 8]>\r\n		  <script src=\"/UserFiles/assets/js/excanvas.min.js\"></script>\r\n		<![endif]-->\r\n\r\n$=this.HeadSection$\r\n</head>\r\n<body>\r\n        <div class=\"Region navbar navbar-default\" id=\"navbar\">\r\n    		<script type=\"text/javascript\">\r\n			    try { ace.settings.check(\'navbar\', \'fixed\') } catch (e) { }\r\n			</script>\r\n            $=this.navbar$\r\n		</div>\r\n\r\n        <div class=\"main-container\" id=\"main-container\">\r\n    		<script type=\"text/javascript\">\r\n			    try { ace.settings.check(\'main-container\', \'fixed\') } catch (e) { }\r\n			</script>\r\n\r\n			<div class=\"main-container-inner\">\r\n				<a class=\"menu-toggler\" id=\"menu-toggler\" href=\"#\">\r\n					<span class=\"menu-text\"></span>\r\n				</a>\r\n\r\n				<div class=\"Region sidebar\" id=\"sidebar\">\r\n        			<script type=\"text/javascript\">\r\n					    try { ace.settings.check(\'sidebar\', \'fixed\') } catch (e) { }\r\n					</script>\r\n                    \r\n                    $=this.sidebar$\r\n                    \r\n    				<div class=\"sidebar-collapse\" id=\"sidebar-collapse\">\r\n						<i class=\"icon-double-angle-left\" data-icon1=\"icon-double-angle-left\" data-icon2=\"icon-double-angle-right\"></i>\r\n					</div>\r\n\r\n					<script type=\"text/javascript\">\r\n					    try { ace.settings.check(\'sidebar\', \'collapsed\') } catch (e) { }\r\n					</script>\r\n\r\n                </div>\r\n\r\n				<div class=\"main-content\">\r\n					<div class=\"Region breadcrumbs\" id=\"breadcrumbs\">\r\n						<script type=\"text/javascript\">\r\n						    try { ace.settings.check(\'breadcrumbs\', \'fixed\') } catch (e) { }\r\n						</script>\r\n\r\n                        $=this.breadcrumbs$\r\n					</div>\r\n\r\n					<div id=\"listForm\" class=\"page-content\">\r\n						<div class=\"row\">\r\n							<div class=\"col-xs-12\">\r\n                                    <div class=\"Region row\" id=\"contentDiv\">\r\n                                        $=this.contentDiv$\r\n                                    </div>\r\n							</div>\r\n						</div>\r\n					</div>\r\n				</div>\r\n\r\n			</div><!-- /.main-container-inner -->\r\n\r\n			<a href=\"#\" id=\"btn-scroll-up\" class=\"btn-scroll-up btn btn-sm btn-inverse\">\r\n				<i class=\"icon-double-angle-up icon-only bigger-110\"></i>\r\n			</a>\r\n		</div><!-- /.main-container -->\r\n\r\n        <!-- basic scripts -->\r\n        <script src=\"/UserFiles/assets/js/jquery-ui-1.10.3.full.min.js\"></script>\r\n        <script src=\"/UserFiles/assets/js/jquery-ui-1.10.3.custom.min.js\"></script>\r\n    	<!--script src=\"/UserFiles/assets/js/jquery.ui.touch-punch.min.js\"></script-->\r\n        <script src=\"/UserFiles/assets/js/chosen.jquery.min.js\"></script>\r\n		<!--script src=\"/UserFiles/assets/js/fuelux/fuelux.spinner.min.js\"></script-->\r\n		<script src=\"/UserFiles/assets/js/date-time/bootstrap-datepicker.min.js\"></script>\r\n		<!--script src=\"/UserFiles/assets/js/date-time/bootstrap-timepicker.min.js\"></script-->\r\n		<!--script src=\"/UserFiles/assets/js/date-time/moment.min.js\"></script-->\r\n		<script src=\"/UserFiles/assets/js/date-time/daterangepicker.min.js\"></script>\r\n		<!--script src=\"/UserFiles/assets/js/bootstrap-colorpicker.min.js\"></script-->\r\n		<!--script src=\"/UserFiles/assets/js/jquery.knob.min.js\"></script-->\r\n		<!--script src=\"/UserFiles/assets/js/jquery.autosize.min.js\"></script-->\r\n		<!--script src=\"/UserFiles/assets/js/jquery.inputlimiter.1.3.1.min.js\"></script-->\r\n		<script src=\"/UserFiles/assets/js/jquery.maskedinput.min.js\"></script>\r\n		<!--script src=\"/UserFiles/assets/js/bootstrap-tag.min.js\"></script-->\r\n        <script src=\"/UserFiles/assets/js/jquery.dataTables.min.js\"></script>\r\n        <script src=\"/UserFiles/assets/js/jquery.dataTables.bootstrap.js\"></script>\r\n    	<!--script src=\"/UserFiles/assets/js/jquery.nestable.min.js\"></script-->\r\n		<!-- ace scripts -->\r\n\r\n        <!--for Dashboar -->\r\n        <!--script src=\"/UserFiles/assets/js/uncompressed/flot/jquery.flot.js\"></script-->\r\n        <!--script src=\"/UserFiles/assets/js/uncompressed/flot/jquery.flot.pie.js\"></script-->\r\n        <!--script src=\"/UserFiles/assets/js/uncompressed/flot/jquery.flot.resize.js\"></script-->\r\n        <!--for Dashboar -->\r\n\r\n		<script src=\"/UserFiles/assets/js/uncompressed/ace-elements.js\"></script>\r\n		<script src=\"/UserFiles/assets/js/uncompressed/ace.js\"></script>\r\n\r\n    \r\n        	<script type=\"text/javascript\">\r\n		    jQuery(function (\\$) {\r\n                var navParent = \\$(\'.nav li.active\').parent().closest(\'li\');\r\n                navParent.addClass(\"active\").addClass(\'open\');\r\n    	        \\$(\'ul.breadcrumb li:eq(1)\').text(\\$(\'li.active.open a:first\').text());\r\n		        \\$(\'ul.breadcrumb li:eq(2)\').text(\\$(\'ul.submenu li.active\').text());\r\n\r\n/*\r\n		        \\$(\'[data-rel=tooltip]\').tooltip({ container: \'body\' });\r\n		        \\$(\'[data-rel=popover]\').popover({ container: \'body\' });\r\n\r\n\r\n		        \\$(\'textarea[class*=autosize]\').autosize({ append: \"\\n\" });\r\n		        \\$(\'textarea.limited\').inputlimiter({\r\n		            remText: \'%n character%s remaining...\',\r\n		            limitText: \'max allowed : %n.\'\r\n		        });\r\n\r\n		        \\$.mask.definitions[\'~\'] = \'[+-]\';\r\n		        \\$(\'.input-mask-date\').mask(\'99/99/9999\');\r\n		        \\$(\'.input-mask-phone\').mask(\'(999) 999-9999\');\r\n		        \\$(\'.input-mask-eyescript\').mask(\'~9.99 ~9.99 999\');\r\n		        \\$(\".input-mask-product\").mask(\"a*-999-a999\", { placeholder: \" \", completed: function () { alert(\"You typed the following: \" + this.val()); } });\r\n\r\n		        \\$(\'.date-picker\').datepicker({ autoclose: true }).next().on(ace.click_event, function () {\r\n		            \\$(this).prev().focus();\r\n		        });\r\n		        \\$(\'input[name=date-range-picker]\').daterangepicker().prev().on(ace.click_event, function () {\r\n		            \\$(this).next().focus();\r\n		        });\r\n\r\n		        \\$(\'.colorpicker\').colorpicker();\r\n		        \\$(\'.simple-colorpicker\').ace_colorpicker();\r\n\r\n\r\n		        \\$(\".knob\").knob();\r\n\r\n		        \\$(\'table th input:checkbox\').on(\'click\', function () {\r\n		            var that = this;\r\n		            \\$(this).closest(\'table\').find(\'tr > td:first-child input:checkbox\')\r\n					.each(function () {\r\n					    this.checked = that.checked;\r\n					    \\$(this).closest(\'tr\').toggleClass(\'selected\');\r\n					});\r\n\r\n		        });\r\n*/\r\n		    });\r\n		</script>\r\n\r\n\r\n</body>\r\n</html>',1,'1990-01-01 00:00:00',0,'2014-04-28 22:39:10',1,1,0),('Login.aspx','<!DOCTYPE html>\r\n\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head runat=\"server\">\r\n    <meta charset=\"utf-8\" />\r\n\r\n        <link href=\"/UserFiles/assets/css/bootstrap.min.css\" rel=\"stylesheet\" />\r\n		<link rel=\"stylesheet\" href=\"/UserFiles/assets/css/font-awesome.min.css\" />\r\n\r\n		<!--[if IE 7]>\r\n		  <link rel=\"stylesheet\" href=\"/UserFiles/assets/css/font-awesome-ie7.min.css\" />\r\n		<![endif]-->\r\n\r\n		<link rel=\"stylesheet\" href=\"/UserFiles/assets/css/ace-fonts.css\" />\r\n		<link rel=\"stylesheet\" href=\"/UserFiles/assets/css/ace.min.css\" />\r\n		<link rel=\"stylesheet\" href=\"/UserFiles/assets/css/ace-rtl.min.css\" />\r\n\r\n		<!--[if lte IE 8]>\r\n		  <link rel=\"stylesheet\" href=\"/UserFiles/assets/css/ace-ie.min.css\" />\r\n		<![endif]-->\r\n\r\n		<!--[if lt IE 9]>\r\n		<script src=\"/UserFiles/assets/js/html5shiv.js\"></script>\r\n		<script src=\"/UserFiles/assets/js/respond.min.js\"></script>\r\n		<![endif]-->\r\n\r\n		<!--[if !IE]> -->\r\n\r\n		<script type=\"text/javascript\">\r\n		    window.jQuery || document.write(\"<script src=\'/UserFiles/assets/js/jquery-2.0.3.min.js\'>\" + \"<\" + \"/script>\");\r\n		</script>\r\n\r\n		<!-- <![endif]-->\r\n\r\n		<!--[if IE]>\r\n        <script type=\"text/javascript\">\r\n         window.jQuery || document.write(\"<script src=\'/UserFiles/assets/js/jquery-1.10.2.min.js\'>\"+\"<\"+\"/script>\");\r\n        </script>\r\n        <![endif]-->\r\n\r\n		<script type=\"text/javascript\">\r\n		    if (\"ontouchend\" in document) document.write(\"<script src=\'/UserFiles/assets/js/jquery.mobile.custom.min.js\'>\" + \"<\" + \"/script>\");\r\n		</script>\r\n\r\n		<script type=\"text/javascript\">\r\n		    function show_box(id) {\r\n		        jQuery(\'.widget-box.visible\').removeClass(\'visible\');\r\n		        jQuery(\'#\' + id).addClass(\'visible\');\r\n		    }\r\n		</script>\r\n\r\n        $=this.HeadSection$\r\n</head>\r\n<body class=\"login-layout\">\r\n		<div class=\"main-container\">\r\n			<div class=\"main-content\">\r\n				<div class=\"row\">\r\n					<div class=\"col-sm-10 col-sm-offset-1\">\r\n						<div class=\"login-container\">\r\n							<div class=\"center\">\r\n\r\n								<h1>\r\n									<img id=\"stv_logo\" src=\"/UserFiles/assets/cinar.png\" /><br/>\r\n									<span class=\"white\">$=Provider.Configuration.SiteName$</span>\r\n								</h1>\r\n								<h4 class=\"blue\">&copy; $=Provider.Configuration.SiteDescription$</h4>\r\n							</div>\r\n\r\n							<div class=\"space-6\"></div>\r\n\r\n							<div class=\"position-relative\">\r\n								<div id=\"login-box\" class=\"login-box visible widget-box no-border\">\r\n									<div class=\"widget-body\">\r\n										<div class=\"widget-main\">\r\n											<h4 class=\"header blue lighter bigger\">\r\n												<i class=\"icon-coffee green\"></i>\r\n												Please login\r\n											</h4>\r\n\r\n											<div class=\"space-6\"></div>\r\n    <div id=\"Content\" class=\"container\" class=\"Region navbar navbar-inverse navbar-fixed-top\">\r\n        $=this.Content$\r\n    </div>\r\n\r\n    										<div class=\"social-or-login center\">\r\n												<span class=\"bigger-110\"></span>\r\n											</div>\r\n										</div><!-- /widget-main -->\r\n\r\n										<div class=\"toolbar clearfix\">\r\n											<div>\r\n                                                <h6></h6>\r\n											</div>\r\n										</div>\r\n									</div><!-- /widget-body -->\r\n								</div><!-- /login-box -->\r\n							</div><!-- /position-relative -->\r\n						</div>\r\n					</div><!-- /.col -->\r\n				</div><!-- /.row -->\r\n			</div>\r\n		</div><!-- /.main-container -->\r\n\r\n	</body>\r\n</html>\r\n',4,'2014-04-19 02:07:57',1,'2014-04-26 19:29:05',1,1,0),('AdminUsers.aspx','use:Default.aspx',5,'2014-04-20 23:32:50',1,'2014-04-21 01:57:35',1,1,0),('AdminLangs.aspx','use:Default.aspx',6,'2014-04-21 00:22:46',1,'2014-04-21 01:59:24',1,1,0),('CmsContents.aspx','use:Default.aspx',7,'2014-04-21 01:20:47',1,'2014-04-21 02:00:15',1,1,0),('AdminUsersEdit.aspx','use:Default.aspx',8,'2014-04-22 12:55:47',1,'2014-04-22 12:55:47',1,1,0),('AdminDefinitions.aspx','use:Default.aspx',9,'2014-04-22 22:56:33',1,'2014-04-22 22:56:33',1,1,0),('AdminDefinitionEdit.aspx','use:Default.aspx',10,'2014-04-22 23:42:21',1,'2014-04-22 23:42:21',1,1,0),('CrmCompany.aspx','use:Default.aspx',11,'2014-04-28 21:04:38',1,'2014-04-28 21:04:38',1,1,0),('CrmCompanyEdit.aspx','use:Default.aspx',12,'2014-04-28 21:11:38',1,'2014-04-28 21:11:38',1,1,0);
/*!40000 ALTER TABLE `template` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user` (
  `Email` varchar(100) collate utf8_turkish_ci NOT NULL,
  `Password` varchar(16) collate utf8_turkish_ci NOT NULL,
  `Keyword` varchar(16) collate utf8_turkish_ci default NULL,
  `Nick` varchar(100) collate utf8_turkish_ci default NULL,
  `Roles` varchar(100) collate utf8_turkish_ci NOT NULL,
  `Name` varchar(50) collate utf8_turkish_ci default NULL,
  `Surname` varchar(50) collate utf8_turkish_ci default NULL,
  `Gender` varchar(50) collate utf8_turkish_ci default NULL,
  `BirthDate` datetime default NULL,
  `IdentityNumber` varchar(100) collate utf8_turkish_ci default NULL,
  `Occupation` varchar(50) collate utf8_turkish_ci default NULL,
  `Company` varchar(100) collate utf8_turkish_ci default NULL,
  `Department` varchar(50) collate utf8_turkish_ci default NULL,
  `PhoneCell` varchar(50) collate utf8_turkish_ci default NULL,
  `PhoneWork` varchar(50) collate utf8_turkish_ci default NULL,
  `PhoneHome` varchar(50) collate utf8_turkish_ci default NULL,
  `AddressLine1` varchar(200) collate utf8_turkish_ci default NULL,
  `AddressLine2` varchar(200) collate utf8_turkish_ci default NULL,
  `City` varchar(50) collate utf8_turkish_ci default NULL,
  `Country` varchar(50) collate utf8_turkish_ci default NULL,
  `ZipCode` varchar(5) collate utf8_turkish_ci default NULL,
  `Web` varchar(150) collate utf8_turkish_ci default NULL,
  `Education` varchar(50) collate utf8_turkish_ci default NULL,
  `Certificates` varchar(200) collate utf8_turkish_ci default NULL,
  `About` varchar(200) collate utf8_turkish_ci default NULL,
  `Avatar` varchar(100) collate utf8_turkish_ci default NULL,
  `RedirectCount` int(11) default NULL,
  `Id` int(11) NOT NULL auto_increment,
  `InsertDate` datetime NOT NULL default '1990-01-01 00:00:00',
  `InsertUserId` int(11) default NULL,
  `UpdateDate` datetime default NULL,
  `UpdateUserId` int(11) default NULL,
  `Visible` tinyint(1) NOT NULL default '1',
  `OrderNo` int(11) default NULL,
  `FacebookId` varchar(20) collate utf8_turkish_ci NOT NULL default '',
  `GoogleId` varchar(50) collate utf8_turkish_ci NOT NULL,
  `YahooId` varchar(50) collate utf8_turkish_ci NOT NULL,
  `MsnId` varchar(50) collate utf8_turkish_ci NOT NULL,
  `LinkedinId` varchar(50) collate utf8_turkish_ci NOT NULL,
  `TwitterId` varchar(50) collate utf8_turkish_ci NOT NULL,
  `MyspaceId` varchar(50) collate utf8_turkish_ci NOT NULL,
  PRIMARY KEY  (`Id`),
  KEY `UNQ_User_Email` (`Email`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES ('root@local','63A9F0EA7BB98050','jhrd74ghe63','admin','User,Editor,Designer','Root','User',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,1,'1990-01-01 00:00:00',NULL,NULL,NULL,1,NULL,'','','','','','',''),('anonim','','63beyte674hge','anonim','','Anonim','User',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,2,'1990-01-01 00:00:00',NULL,NULL,NULL,1,NULL,'','','','','','',''),('editor','63A9F0EA7BB98050','ge548rhe46e','editor','User,Editor','Editor','User',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,3,'1990-01-01 00:00:00',NULL,NULL,NULL,1,NULL,'','','','','','',''),('bulentkeskin@gmail.com','','239EEFF469ACE96A','bbazk','User,Editor','Bülent','Keskin','Male','1970-01-01 00:00:00',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'tr',NULL,NULL,NULL,NULL,NULL,'/UserFiles/Images/_member/bulentkeskin@gmail.com.jpg',0,4,'2013-11-16 02:11:38',2,'2014-04-28 21:31:14',1,0,0,'642493603','102570363921625842618','','','','',''),('admin@hasen.org.tr','562F8F7B8C48B17D','E5D37BC316219932','hasen','User,Editor','Hasen','Orgtr','Male','2014-04-10 00:00:00',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'tr',NULL,NULL,NULL,NULL,NULL,'/UserFiles/Images/_member/admin@hasen.org.tr.jpg',0,5,'2014-04-22 21:34:34',1,'2014-04-28 21:35:24',1,0,0,'0','0','0','0','0','0','0');
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

-- Dump completed on 2014-04-29  0:09:53
