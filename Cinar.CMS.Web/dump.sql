-- phpMyAdmin SQL Dump
-- version 3.3.1
-- http://www.phpmyadmin.net
--
-- Anamakine: localhost:3306
-- Üretim Zamanı: 23 Kasım 2011 saat 13:04:25
-- Sunucu sürümü: 5.1.48
-- PHP Sürümü: 5.2.6

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Veritabanı: `blank`
--

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı: `author`
--

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
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=5 ;

--
-- Tablo döküm verisi `author`
--

INSERT INTO `author` (`DisableAutoContent`, `UserId`, `Name`, `Description`, `Picture`, `Picture2`, `Id`, `InsertDate`, `InsertUserId`, `UpdateDate`, `UpdateUserId`, `Visible`, `OrderNo`) VALUES
(0, NULL, 'Editorial', NULL, NULL, NULL, 1, '1990-01-01 00:00:00', NULL, NULL, NULL, 1, NULL),
(1, 4, 'Tanla', '', '/UserFiles/Images/_author/tanla.jpg', NULL, 2, '2011-10-26 20:18:54', 1, '2011-10-26 20:28:57', 1, 1, 0),
(1, 7, 'Bahar', 'Bahar hakkında yazı... Bahar hakkında yazı... Bahar hakkında yazı... Bahar hakkında yazı... Bahar hakkında yazı... Bahar hakkında yazı... Bahar hakkında yazı... Bahar hakkında yazı... Bahar hakkında yazı... Bahar hakkında yazı... <br/>\n<br/>\nBahar hakkında yazı... Bahar hakkında yazı... Bahar hakkında yazı... Bahar hakkında yazı... Bahar hakkında yazı... Bahar hakkında yazı... Bahar hakkında yazı... Bahar hakkında yazı... Bahar hakkında yazı... Bahar hakkında yazı... <br/>\n<br/>\nBahar hakkında yazı... Bahar hakkında yazı... Bahar hakkında yazı... Bahar hakkında yazı... Bahar hakkında yazı... Bahar hakkında yazı... Bahar hakkında yazı... Bahar hakkında yazı... Bahar hakkında yazı... Bahar hakkında yazı... <br/>\n<br/>\n\n', '/UserFiles/Images/_author/bahar.jpg', '/UserFiles/Images/_author/bahar2.jpg', 3, '2011-10-26 21:30:11', 1, '2011-11-19 23:15:44', 1, 1, 0),
(1, 8, 'Deniz', '', '/UserFiles/Images/_author/deniz.jpg', NULL, 4, '2011-10-26 22:52:10', 1, '2011-10-26 23:03:21', 1, 1, 0);

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı: `authorlang`
--

DROP TABLE IF EXISTS `authorlang`;
CREATE TABLE `authorlang` (
  `AuthorId` int(11) NOT NULL,
  `LangId` int(11) NOT NULL,
  `Name` varchar(200) COLLATE utf8_turkish_ci NOT NULL,
  `Description` varchar(300) COLLATE utf8_turkish_ci DEFAULT NULL,
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=1 ;

--
-- Tablo döküm verisi `authorlang`
--


-- --------------------------------------------------------

--
-- Tablo için tablo yapısı: `bannerad`
--

DROP TABLE IF EXISTS `bannerad`;
CREATE TABLE `bannerad` (
  `Name` varchar(50) COLLATE utf8_turkish_ci NOT NULL,
  `Href` varchar(100) COLLATE utf8_turkish_ci NOT NULL,
  `BannerFile` varchar(100) COLLATE utf8_turkish_ci NOT NULL,
  `Tags` varchar(300) COLLATE utf8_turkish_ci DEFAULT NULL,
  `ViewCondition` varchar(100) COLLATE utf8_turkish_ci NOT NULL,
  `StartDate` datetime NOT NULL,
  `FinishDate` datetime NOT NULL,
  `MaxViewCount` int(11) DEFAULT NULL,
  `ViewCount` int(11) DEFAULT NULL,
  `MaxClickCount` int(11) DEFAULT NULL,
  `ClickCount` int(11) DEFAULT NULL,
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `InsertDate` datetime NOT NULL DEFAULT '1990-01-01 00:00:00',
  `InsertUserId` int(11) DEFAULT NULL,
  `UpdateDate` datetime DEFAULT NULL,
  `UpdateUserId` int(11) DEFAULT NULL,
  `Visible` tinyint(1) NOT NULL DEFAULT '1',
  `OrderNo` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=1 ;

--
-- Tablo döküm verisi `bannerad`
--


-- --------------------------------------------------------

--
-- Tablo için tablo yapısı: `circulation`
--

DROP TABLE IF EXISTS `circulation`;
CREATE TABLE `circulation` (
  `WeekStartDate` datetime DEFAULT NULL,
  `DailyName` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `AvgDailySale` int(11) DEFAULT NULL,
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `InsertDate` datetime NOT NULL DEFAULT '1990-01-01 00:00:00',
  `InsertUserId` int(11) DEFAULT NULL,
  `UpdateDate` datetime DEFAULT NULL,
  `UpdateUserId` int(11) DEFAULT NULL,
  `Visible` tinyint(1) NOT NULL DEFAULT '1',
  `OrderNo` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=1 ;

--
-- Tablo döküm verisi `circulation`
--


-- --------------------------------------------------------

--
-- Tablo için tablo yapısı: `configuration`
--

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
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=2 ;

--
-- Tablo döküm verisi `configuration`
--

INSERT INTO `configuration` (`SiteName`, `SiteAddress`, `SiteDescription`, `SiteKeywords`, `SiteLogo`, `SiteIcon`, `SessionTimeout`, `BufferOutput`, `MultiLang`, `DefaultLang`, `DefaultStyleSheet`, `CountTags`, `UseHTMLEditor`, `DefaultDateFormat`, `NoPicture`, `ThumbQuality`, `ImageUploadMaxWidth`, `AuthEmail`, `MailHost`, `MailUsername`, `MailPassword`, `UseCache`, `CacheLifeTime`, `MainPage`, `CategoryPage`, `ContentPage`, `LoginPage`, `MembershipFormPage`, `MembershipProfilePage`, `RememberPasswordFormPage`, `UserActivationPage`, `AdminPage`, `Id`, `InsertDate`, `InsertUserId`, `UpdateDate`, `UpdateUserId`, `Visible`, `OrderNo`) VALUES
('blank mag', 'www.blank-magazine.com', '', '', '', '', 120, 1, 0, 1, '@font-face {\n    font-family: ''Spinnaker'';\n    src: url(''/UserFiles/fonts/spinnaker-regular-webfont.eot'');\n    src: url(''/UserFiles/fonts/spinnaker-regular-webfont.eot?#iefix'') format(''embedded-opentype''),\n         url(''/UserFiles/fonts/spinnaker-regular-webfont.woff'') format(''woff''),\n         url(''/UserFiles/fonts/spinnaker-regular-webfont.ttf'') format(''truetype''),\n         url(''/UserFiles/fonts/spinnaker-regular-webfont.svg#SpinnakerRegular'') format(''svg'');\n    font-weight: normal;\n    font-style: normal;\n\n}\n@font-face {\n    font-family: ''WaitingfortheSunrise'';\n    src: url(''/UserFiles/fonts/waitingforthesunrise-webfont.eot'');\n    src: url(''/UserFiles/fonts/waitingforthesunrise-webfont.eot?#iefix'') format(''embedded-opentype''),\n         url(''/UserFiles/fonts/waitingforthesunrise-webfont.woff'') format(''woff''),\n         url(''/UserFiles/fonts/waitingforthesunrise-webfont.ttf'') format(''truetype''),\n         url(''/UserFiles/fonts/waitingforthesunrise-webfont.svg#WaitingfortheSunriseRegular'') format(''svg'');\n    font-weight: normal;\n    font-style: normal;\n\n}\n\nbody {\n	font-family:Times New Roman;\n	font-size:14px;\n	text-align:center;\n	color: black;\n}\ntd {\n	font-size:14px;\n}\na {\n	color: black;\n	text-decoration:none\n}\na:hover {\n	text-decoration:underline;\n}\n\np {margin:0px;padding:0px}\n\nDIV {\n	-moz-box-sizing:border-box;\n	box-sizing:border-box;\n	margin:0;\n	padding:0;\n}\n\ntextarea:focus, input:focus{\n    outline: none;\n}\n\n#page {\n	width: 990px; \n	min-height: 700px; \n	margin-top:10px; \n	margin-right:auto;\n	margin-left:auto;\n	background: white;\n	text-align:left;\n}\n#Header {text-align:center}\n#Footer {text-align:left; clear:both;}\n\n.shadow {\n	-moz-box-shadow: 3px 3px 4px #c1c3c2;\n	-webkit-box-shadow: 3px 3px 4px #c1c3c2;\n	box-shadow: 3px 3px 4px #c1c3c2;\n	/* For IE 8 */\n	-ms-filter: "progid:DXImageTransform.Microsoft.Shadow(Strength=4, Direction=135, Color=''#c1c3c2'')";\n	/* For IE 5.5 - 7 */\n	filter: progid:DXImageTransform.Microsoft.Shadow(Strength=4, Direction=135, Color=''#c1c3c2'');\n}\n\n.vitrin div.clItem {\n	background: #E1E3E2;\n	padding: 10px;\n	margin-top: 10px;\n	border: 1px solid #E1E3E2;\n	overflow:hidden;\n}\n.vitrin div.clItem2 {\n	background: #C1C3C2;\n	padding: 10px;\n	margin-top: 10px;\n	border: 1px solid #838383;\n}\n.vitrin div.clAuthor, .vitrin div.clCategory, .vitrin div.clTitle {\n	height: 25px;\n	padding-top: 2px;\n	background: url(/UserFiles/Images/design/iam_bg.png);\n	padding-left: 40px;\n	width:160px;\n	font-family: WaitingfortheSunrise, "Buxton Sketch", script, Arial;\n	font-size: 18px;\n	color:black;\n}\n.vitrin div.clDesc {width:210px; margin-top:12px;}\n.vitrin div.clPubDate {text-align:right;}\n.sagda {float:right}\n.solda {float:left}\n\n.rotate-10{ \n    -moz-transform: rotate(-10deg);  /* FF3.5+ */\n    -o-transform: rotate(-10deg);  /* Opera 10.5 */\n    -webkit-transform: rotate(-10deg);  /* Saf3.1+, Chrome */\n	-ms-transform: rotate(-10deg);  /* IE9 */\n    transform: rotate(-10deg);\n 	filter: progid:DXImageTransform.Microsoft.Matrix(sizingMethod=''clip to original'', /* IE6-IE9 */\n	M11=0.984807753012208, M12=0.17364817766693033, M21=-0.17364817766693033, M22=0.984807753012208);\n 	zoom: 1;\n}\n\n.clsContentDisplay {\n	border:1px solid #E5E5E5;\n	margin-top:10px;\n	background:#E1E3E2;\n	padding: 24px 24px 24px 24px;\n}\n.clsContentDisplay div.title {\n	font-weight:bold;\n	font-size:18px;\n	border-bottom:1px solid #CFCFCF;\n}\n.clsContentDisplay div.text {\n	background:white;\n	margin-top:25px;\n}\n.clsContentDisplay div.innerDiv {\n	padding:24px;\n	background:white;	\n}\n.clsContentDisplay div.date {\n	margin-bottom:24px;\n	font-size:12px;\n	text-align:right;\n}\n\n.tags {\n	margin-top:60px;\n	width:230px;\n	font-family: spinnaker;\n}\n.tags .tag {\n	border-bottom: 1px solid #8A8A8A;\n	padding:10px 0px 1px 4px;\n	text-transform:uppercase;\n}\n\n.adminDataList {margin-bottom:10px}\n.adminDataList h2 {\n	background: #808080;\n	color: white;\n	font-family: Spinnaker,tahoma;\n	font-size: 14px;\n	margin: 0px;\n	padding: 4px;\n}\n.adminDataList h2:hover {background:#FFA826}\n.adminDataList h2 span {cursor:pointer}\n.adminDataList h2 .add {float:right; font-size:12px}\n.adminDataList img {\n	cursor:pointer;\n}\n.adminDataList table {\n	border:1px solid black;\n}\n.adminDataList td {\n	padding:4px;\n	text-align:center;\n	min-width:150px;\n}\n.adminDataList .innerDiv {\n	overflow-y:hidden;\n}\n.adminDataList div.paging {\n	padding:1px;\n	text-align:center}\n', 1, 0, 'dd MMMM yyyy', '/UserFiles/Images/design/blank.png', 90, 640, 'info@isimsiz.com', 'mail.isimsiz.com', '', '', 'False', 15, 'Default.aspx', 'Category.aspx', 'Content.aspx', 'Login.aspx', 'Membership.aspx', 'Profile.aspx', 'RememberPassword.aspx', 'Activation.aspx', 'Admin.aspx', 1, '2011-10-26 11:00:00', 3, '2011-11-22 21:23:29', 1, 1, 0);

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı: `contactus`
--

DROP TABLE IF EXISTS `contactus`;
CREATE TABLE `contactus` (
  `Name` varchar(100) COLLATE utf8_turkish_ci NOT NULL,
  `Email` varchar(100) COLLATE utf8_turkish_ci NOT NULL,
  `Subject` varchar(100) COLLATE utf8_turkish_ci NOT NULL,
  `Message` text COLLATE utf8_turkish_ci NOT NULL,
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `InsertDate` datetime NOT NULL DEFAULT '1990-01-01 00:00:00',
  `InsertUserId` int(11) DEFAULT NULL,
  `UpdateDate` datetime DEFAULT NULL,
  `UpdateUserId` int(11) DEFAULT NULL,
  `Visible` tinyint(1) NOT NULL DEFAULT '1',
  `OrderNo` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=1 ;

--
-- Tablo döküm verisi `contactus`
--


-- --------------------------------------------------------

--
-- Tablo için tablo yapısı: `content`
--

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
  `LikeIt` int(11) DEFAULT '0',
  PRIMARY KEY (`Id`),
  KEY `ContentPublishDate` (`PublishDate`),
  KEY `ContentCategoryId` (`CategoryId`),
  KEY `ContentAuthorIdPublishDateCategoryId` (`AuthorId`,`PublishDate`,`CategoryId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=68 ;

--
-- Tablo döküm verisi `content`
--

INSERT INTO `content` (`ClassName`, `Title`, `Description`, `Keywords`, `Metin`, `Hierarchy`, `AuthorId`, `SourceId`, `PublishDate`, `Picture`, `Tags`, `TagRanks`, `CategoryId`, `ShowInPage`, `ShowContentsInPage`, `ShowCategoriesInPage`, `IsManset`, `SpotTitle`, `ContentSourceId`, `SourceLink`, `ViewCount`, `CommentCount`, `RecommendCount`, `Id`, `InsertDate`, `InsertUserId`, `UpdateDate`, `UpdateUserId`, `Visible`, `OrderNo`, `LikeIt`) VALUES
('Category', 'Kök', NULL, NULL, NULL, '', NULL, NULL, '1980-01-01 00:00:00', NULL, NULL, NULL, 0, NULL, NULL, NULL, 0, NULL, NULL, NULL, 0, 0, 0, 1, '2011-10-26 11:00:00', NULL, '2011-10-26 17:18:00', NULL, 1, NULL, 0),
('Category', 'Foto', '', '', '', '00001', 1, 1, '2011-10-17 09:00:00', '', '', '', 1, '', '', '', 0, '', 0, '', 0, 0, 0, 2, '2011-10-26 17:18:00', 1, '2011-10-26 17:18:00', 1, 1, 0, 0),
('Category', 'City', '', '', '', '00001', 1, 1, '2011-10-26 17:18:49', '', '', '', 1, 'City.aspx', 'Content.aspx', 'City.aspx', 0, '', 0, '', 0, 0, 0, 3, '2011-10-26 17:18:00', 1, '2011-11-02 18:33:30', 1, 1, 0, 0),
('Content', 'Hate', 'Select which you hate amongst a selection of disturbing pictures.', '', '', '00001,00048', 1, 1, '2011-10-26 17:18:01', '/UserFiles/Images/design/hate1.jpg', '', '', 48, '', '', '', 0, '', 0, '', 0, 0, 0, 4, '2011-10-26 17:18:00', 1, '2011-11-19 20:49:12', 1, 1, 0, 0),
('Content', 'Love', 'Select which are really lovely for you amongst a selection of lovely pictures.', '', '', '00001,00048', 1, 1, '2011-10-26 17:18:16', '/UserFiles/Images/design/love.jpg', '', '', 48, '', '', '', 0, '', 0, '', 0, 0, 0, 5, '2011-10-26 17:18:00', 1, '2011-11-19 20:49:21', 1, 1, 0, 0),
('Category', 'Moda', '', '', '', '00001', 1, 1, '2011-10-26 17:18:37', '', '', '', 1, 'Moda.aspx', 'Moda.aspx', 'Moda.aspx', 0, '', 0, '', 0, 0, 0, 6, '2011-10-26 17:19:00', 1, '2011-11-09 19:52:03', 1, 1, 0, 0),
('Category', 'People', '', '', '', '00001', 1, 1, '2011-10-26 17:19:30', '', '', '', 1, 'People.aspx', 'People.aspx', 'People.aspx', 0, '', 0, '', 0, 0, 0, 7, '2011-10-26 17:19:00', 1, '2011-11-12 11:47:46', 1, 1, 0, 0),
('Category', 'Author', '', '', '', '00001', 1, 1, '2011-10-26 17:19:17', '', '', '', 1, '', 'Author.aspx', '', 0, '', 0, '', 0, 0, 0, 8, '2011-10-26 17:19:00', 1, '2011-10-28 16:29:33', 1, 1, 0, 0),
('Category', 'Bird View', '', '', '', '00001', 1, 1, '2011-10-26 17:19:00', '', '', '', 1, '', '', '', 0, '', 0, '', 0, 0, 0, 9, '2011-10-26 17:19:00', 1, '2011-10-26 17:19:00', 1, 1, 0, 0),
('Category', 'Cook', '', '', '', '00001', 1, 1, '2011-10-26 17:19:00', '', '', '', 1, '', '', '', 0, '', 0, '', 0, 0, 0, 10, '2011-10-26 17:20:00', 1, '2011-10-26 17:20:00', 1, 1, 0, 0),
('Content', 'Tanla''nın ilk yazısı', 'Colorfull shoes from benetton. Benetton? Believe me!', '', '', '00001,00008', 2, 1, '2011-10-26 20:19:49', '', 'RÖPORTAJ', '', 8, '', '', '', 0, '', 0, '', 0, 0, 0, 11, '2011-10-26 20:20:16', 1, '2011-10-30 22:17:29', 1, 1, 0, 0),
('Content', 'İstanbul', 'İstanbul is one of the most beautiful cities in the planet.', '', '', '00001,00003', 1, 1, '2011-10-26 21:16:58', '/UserFiles/Images/city/city.jpg', '', '', 3, '', '', '', 0, '', 0, '', 0, 0, 0, 12, '2011-10-26 21:17:26', 1, '2011-10-30 21:54:56', 1, 1, 0, 0),
('Content', 'a bird view', '', '', '', '00001,00009', 1, 1, '2011-10-26 21:23:23', '/UserFiles/Images/birdview/birdview.jpg', 'Selection', '', 9, '', '', '', 0, '', 0, '', 0, 0, 0, 13, '2011-10-26 21:24:16', 1, '2011-11-02 17:15:27', 1, 1, 0, 0),
('Content', 'Bahar''ın ilk yazısı', '', '', 'Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.<br/>\n<br/>\nLorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.<br/>\n<br/>\nLorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.<br/>\n<br/>\nLorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.<br/>\n<br/>\n', '00001,00008', 3, 1, '2011-10-26 21:30:07', '/UserFiles/Images/_upload/2011/11/2/Bahar''in_ilk_yazisi_547.jpg', 'RÖPORTAJ', '', 8, '', '', '', 0, '', 0, '', 0, 0, 0, 14, '2011-10-26 21:31:16', 1, '2011-11-02 17:10:50', 1, 1, 0, 0),
('Category', 'Room', '', '', '', '00001', 1, 1, '2011-10-26 21:55:36', '', '', '', 1, '', '', '', 0, '', 0, '', 0, 0, 0, 15, '2011-10-26 21:55:33', 1, '2011-10-26 21:56:42', 1, 1, 0, 0),
('Content', 'a room', '', '', '', '00001,00015', 1, 1, '2011-10-26 21:56:26', '/UserFiles/Images/room/room.jpg', '', '', 15, '', '', '', 0, '', 0, '', 0, 0, 0, 16, '2011-10-26 21:56:32', 1, '2011-10-26 22:09:53', 1, 1, 0, 0),
('Content', 'a cook', '', '', '', '00001,00010', 1, 1, '2011-10-26 22:12:36', '/UserFiles/Images/cook/cook.jpg', '', '', 10, '', '', '', 0, '', 0, '', 0, 0, 0, 17, '2011-10-26 22:12:28', 1, '2011-10-26 22:12:54', 1, 1, 0, 0),
('Content', 'Deniz''in ilk yazısı', 'Hi, I will share my design with you on these pages.', '', '', '00001,00008', 4, 1, '2011-10-26 23:04:07', '', 'RÖPORTAJ', '', 8, '', '', '', 0, '', 0, '', 0, 0, 0, 18, '2011-10-26 23:05:20', 1, '2011-10-30 22:02:50', 1, 1, 0, 0),
('Content', 'ERIN WASSON - Stylist & Designer', 'The most exiting people of the sector to meet with you, their works and their feelings. All is gonna be on this section. Start browsing by giving a pressure with your point finger onto the left button of mouse.', '', 'Steinbeck''s contacts with leftist authors, journalists, and labor union figures may have influenced his writing and he joined the League of American Writers, a Communist organization, in 1935.[37] Steinbeck was mentored by radical writers Lincoln Steffens and his wife Ella Winter. Through Francis Whitaker, a member of the United States Communist Party’s John Reed Club for writers, Steinbeck met with strike organizers from the Cannery and Agricultural Workers'' Industrial Union.<br/>\n<br/>\nIn 1967, when he was sent to Vietnam to report on the war, his sympathetic portrayal of the United States Army led the New York Post to denounce him for betraying his liberal past. Steinbeck''s biographer, Jay Parini, says Steinbeck''s friendship with President Lyndon B. Johnson influenced his views on Vietnam. Steinbeck may also have been concerned about the safety of his son serving in Vietnam.<br/>\n<br/>\nSteinbeck was a close associate of playwright Arthur Miller. In June 1959, Steinbeck took a personal and professional risk by standing up for him when Miller refused to name names in the House Un-American Activities Committee trials.[29] Steinbeck called the period one of the "strangest and most frightening times a government and people have ever faced."<br/>', '00001,00007', 1, 1, '2011-10-26 23:09:58', '/UserFiles/Images/people/diger.jpg', 'FAMEOUS', '', 7, '', '', '', 0, '', 0, '', 0, 0, 0, 19, '2011-10-26 23:10:23', 1, '2011-11-12 12:58:26', 1, 1, 0, 0),
('Category', 'Music', '', '', '', '00001', 1, 1, '2011-10-26 23:12:25', '', '', '', 1, '', '', '', 0, '', 0, '', 0, 0, 0, 20, '2011-10-26 23:12:42', 1, '2011-10-26 23:12:42', 1, 1, 0, 0),
('Content', 'a music', '', '', '', '00001,00020', 1, 1, '2011-10-26 23:12:20', '/UserFiles/Images/music/music.jpg', '', '', 20, '', '', '', 0, '', 0, '', 0, 0, 0, 21, '2011-10-26 23:13:38', 1, '2011-11-21 16:34:27', 1, 1, 0, 0),
('Category', 'Words', '', '', '', '00001', 1, 1, '2011-10-26 23:17:30', '', '', '', 1, '', '', '', 0, '', 0, '', 0, 0, 0, 22, '2011-10-26 23:17:44', 1, '2011-10-26 23:17:44', 1, 1, 0, 0),
('Content', 'a words', '', '', '', '00001,00022', 1, 1, '2011-10-26 23:17:45', '/UserFiles/Images/words/words.jpg', '', '', 22, '', '', '', 0, '', 0, '', 0, 0, 0, 23, '2011-10-26 23:18:23', 1, '2011-10-26 23:18:23', 1, 1, 0, 0),
('Category', 'Beauty', '', '', '', '00001', 1, 1, '2011-10-26 23:21:01', '', '', '', 1, '', '', '', 0, '', 0, '', 0, 0, 0, 24, '2011-10-26 23:21:14', 1, '2011-10-26 23:21:14', 1, 1, 0, 0),
('Content', 'a beauty', '', '', '', '00001,00024', 1, 1, '2011-10-26 23:21:15', '/UserFiles/Images/beauty/beauty.jpg', '', '', 24, '', '', '', 0, '', 0, '', 0, 0, 0, 25, '2011-10-26 23:22:19', 1, '2011-10-26 23:22:19', 1, 1, 0, 0),
('Category', 'Think', '', '', '', '00001', 1, 1, '2011-10-26 23:34:29', '', '', '', 1, '', '', '', 0, '', 0, '', 0, 0, 0, 26, '2011-10-26 23:34:47', 1, '2011-10-26 23:34:47', 1, 1, 0, 0),
('Content', 'a think', '', '', '', '00001,00026', 1, 1, '2011-10-26 23:34:20', '/UserFiles/Images/think/think.jpg', '', '', 26, '', '', '', 0, '', 0, '', 0, 0, 0, 27, '2011-10-26 23:35:01', 1, '2011-10-26 23:36:46', 1, 1, 0, 0),
('Content', 'Kuş Cenneti', '', '', '', '00001,00008', 3, 1, '2011-10-30 16:34:02', '/UserFiles/Images/_upload/2011/11/2/Kus_Cenneti_917.jpg', 'FOTOĞRAF ÇEKİMİ', '', 8, '', '', '', 0, '', 0, '', 0, 0, 0, 29, '2011-10-30 16:35:35', 1, '2011-11-02 17:09:41', 1, 1, 0, 0),
('Content', 'Şurdan burdan', '', '', '', '00001,00008', 3, 1, '2011-10-30 16:36:00', '/UserFiles/Images/_upload/2011/11/2/Surdan_burdan_444.jpg', 'ORDAN BURDAN', '', 8, '', '', '', 0, '', 0, '', 0, 0, 0, 30, '2011-10-30 16:36:42', 1, '2011-11-02 17:08:35', 1, 1, 0, 0),
('Category', 'Other', '', '', '', '00001', 1, 1, '2011-10-30 17:48:26', '', '', '', 1, '', '', '', 0, '', 0, '', 0, 0, 0, 32, '2011-10-30 17:49:18', 1, '2011-10-30 17:49:18', 1, 1, 0, 0),
('Content', 'ABOUT US', '', '', 'The Coveteur, founded by designer Erin Kleinberg and stylist Stephanie Mark, is a new way of looking at the creative process and influences of some of today''s most recognized global tastemakers.<br/>\n<br/>\nThe Coveteur takes you inside the closets of internationally influential cultural forecasters, showing us what culminates in their personal style - one item at a time.<br/>\n<br/>\nThe site focuses on curations, styling, and new ways of showcasing the personalization behind fashion''s elite. Kleinberg and Mark, in collaboration with the talent themselves, show and describe, in their own words, the significance each item plays in their lives - from personal accomplishments to the way these meaningful belongings have influenced their unique sense of style.<br/>\n<br/>\nCaptured through the lens of photographer Jake Rosenberg, The Coveteur explores what you''ve always wanted to see - the behind the scenes and intimate surveillance of today''s modern trendsetters.<br/>\n<br/>\nThis is... The Coveteur\n', '00001,00032', 1, 1, '2011-10-30 17:49:21', '', '', '', 32, '', '', '', 0, '', 0, '', 0, 0, 0, 33, '2011-10-30 17:51:47', 1, '2011-10-30 17:51:47', 1, 1, 0, 0),
('Content', 'CONTACT US', '', '', '<h2>Contact Us</h2>\n\nCant get enough of The Coveteur? We''d love to hear from you. Hit us up anytime kids!\n\n<h2>By Email:</h2>\n\ninfo@thecoveteur.com\n\n<h2>By Post:</h2>\n\n50 Spadina Road Apt. 712 | Toronto, ON | M5R 2P1 | Canada\n', '00001,00032', 1, 1, '2011-10-30 17:51:51', '', '', '', 32, '', '', '', 0, '', 0, '', 0, 0, 0, 34, '2011-10-30 17:53:27', 1, '2011-10-30 17:53:27', 1, 1, 0, 0),
('Content', 'Milano', '', '', '', '00001,00003', 1, 1, '2011-11-02 15:37:28', '/UserFiles/Images/_upload/2011/11/2/Milano_260.jpg', '', '', 3, '', '', '', 0, '', 0, '', 0, 0, 0, 35, '2011-11-02 15:38:29', 1, '2011-11-02 15:44:40', 1, 1, 0, 0),
('Content', 'Amsterdam', '', '', '', '00001,00003', 1, 1, '2011-11-02 16:17:17', '/UserFiles/Images/_upload/2011/11/2/Amsterdam_398.jpg', '', '', 3, '', '', '', 0, '', 0, '', 0, 0, 1, 36, '2011-11-02 16:18:09', 1, '2011-11-02 16:18:09', 1, 1, 0, 0),
('Content', 'Madrid', '', '', '', '00001,00003', 1, 1, '2011-11-02 16:19:19', '/UserFiles/Images/_upload/2011/11/2/Madrid_589.jpg', '', '', 3, '', '', '', 0, '', 0, '', 0, 0, 0, 37, '2011-11-02 16:19:36', 1, '2011-11-02 16:19:36', 1, 1, 0, 0),
('Content', 'Barcelona', 'Barcelona is a beautiful city!', '', '', '00001,00003', 1, 1, '2011-11-02 16:20:00', '/UserFiles/Images/_upload/2011/11/2/Barcelona_294.jpg', '', '', 3, '', '', '', 0, '', 0, '', 0, 0, 0, 38, '2011-11-02 16:21:16', 1, '2011-11-08 21:29:16', 1, 1, 0, 0),
('Content', 'Paris', '', '', '', '00001,00003', 1, 1, '2011-11-02 16:21:20', '/UserFiles/Images/_upload/2011/11/2/Paris_62.jpg', '', '', 3, '', '', '', 0, '', 0, '', 0, 0, 0, 39, '2011-11-02 16:22:30', 1, '2011-11-02 16:22:30', 1, 1, 0, 0),
('Content', 'New York', '', '', '', '00001,00003', 1, 1, '2011-11-02 16:22:19', '/UserFiles/Images/_upload/2011/11/2/New_York_150.jpg', 'BATI', '', 3, '', '', '', 0, '', 0, '', 0, 0, 0, 40, '2011-11-02 16:23:14', 1, '2011-11-02 18:38:01', 1, 1, 0, 0),
('Content', 'Hakkari', '', '', '', '00001,00003', 1, 1, '2011-11-02 16:25:51', '/UserFiles/Images/_upload/2011/11/2/Hakkari_950.jpg', 'DOĞU', '', 3, '', '', '', 0, '', 0, '', 0, 0, 0, 41, '2011-11-02 16:26:34', 1, '2011-11-02 18:37:49', 1, 1, 0, 0),
('Content', 'Yeni Türkü', '', '', '', '00001,00020', 1, 1, '2011-11-02 17:17:50', '/UserFiles/Images/_upload/2011/11/2/Yeni_Turku_137.jpg', '', '', 20, '', '', '', 0, '', 0, '', 0, 0, 0, 42, '2011-11-02 17:18:37', 1, '2011-11-02 17:18:37', 1, 1, 0, 0),
('Content', 'Ezginin Günlüğü', 'Ezginin Günlüğü konser verecek. Rizede.', '', '', '00001,00020', 1, 1, '2011-11-02 17:18:03', '/UserFiles/Images/_upload/2011/11/2/Ezginin_Gunlugu_442.jpg', '', '', 20, '', '', '', 0, '', 0, '', 0, 0, 0, 43, '2011-11-02 17:19:51', 1, '2011-11-02 17:19:51', 1, 1, 0, 0),
('Category', 'Love and Hate', '', '', '', '00001', 1, 1, '2011-11-11 17:13:28', '', '', '', 1, '', 'LoveHate.aspx', '', 0, '', 0, '', 0, 0, 0, 48, '2011-11-11 17:13:55', 1, '2011-11-19 20:49:48', 1, 1, 0, 0),
('Content', 'One From The Heart', '', '', '', '00001,00006', 1, 1, '2011-11-19 19:29:22', '/UserFiles/Images/moda/one_from_the_heart/1.jpg', '', '', 6, '', '', '', 0, '', 0, '', 0, 0, 0, 61, '2011-11-19 19:30:29', 1, '2011-11-19 19:30:29', 1, 1, 0, 0),
('Content', 'Old Fahion', '', '', '', '00001,00006', 1, 1, '2011-11-21 15:01:45', '/UserFiles/468.jpg', '', '', 6, '', '', '', 0, '', 0, '', 0, 0, 0, 62, '2011-11-21 15:03:02', 1, '2011-11-21 15:28:54', 1, 1, 0, 0),
('Content', 'Big City Life', '', '', '', '00001,00006', 1, 1, '2011-11-21 15:42:19', '/UserFiles/Images/moda/Big_City_Life/57026-800w.jpg', '', '', 6, '', '', '', 0, '', 0, '', 0, 0, 0, 63, '2011-11-21 15:44:24', 1, '2011-11-21 15:47:34', 1, 1, 0, 0),
('Content', 'Sugar', '', '', '', '00001,00006', 1, 1, '2011-11-21 16:14:39', '/UserFiles/Images/moda/Sugar/sugar1.jpg', '', '', 6, '', '', '', 0, '', 0, '', 0, 0, 0, 64, '2011-11-21 16:14:48', 1, '2011-11-21 16:24:36', 1, 1, 0, 0),
('Content', 'intro', '', '', '', '00001', 1, 1, '2011-11-21 16:30:23', '', 'photo', '', 1, '', '', '', 0, '', 0, '', 0, 0, 0, 65, '2011-11-21 16:31:28', 1, '2011-11-21 16:31:28', 1, 0, 0, 0),
('Content', 'Red Beauty', '', '', 'Fashion often presents us with other worlds and alternative realities, but design duo Viktor Horsting and Rolf Snoeren have created their own galaxy for autumn/winter 2011. In it, scarlet-faced warrior queens march out to do battle, in preparation for a war against beauty, to a soundtrack of ratcheting drumbeats and soaring strings.\n\n"We were compelled to create an army," they explain. "An army to battle for beauty. A crusade to communicate our passion for fashion. What emerged is a sharp, graphic approach to tailoring; strong fabrics such as heavy felt, bonded suiting, sculpted leather and tuxedo wool were softened and crafted into pieces fit for our battlefield."\nIn this exclusive film for AnOther, director KT Auleta captured models as they exited from backstage onto the catwalk in Paris last month, and focused on the dramatic red visages created for the show by make-up artist Pat McGrath. "This developed alongside the concept of the show," the designers add, "which was Battle for the Sun. Pat is always able to translate a concept into a real, beautiful reality."\n\nFrom shimmering, iridescent cheekbones to shaded eye sockets and feathered, flicking lashes, McGrath''s red face-paint is more than merely a matte base; it is both textured and flat, characterful and homegeneous. And it suits the autumn/winter collection in its duality: these are clothes which are both strictly tailored and soft, graphically imposing yet whimsical in places.\n"We wanted the clothes to look like modern armour," says Viktor & Rolf. "Powerful silhouettes, fortified fabrics, bold colour contrasts and, above all else, triumphant beauty. We also wanted to illustrate a powerful facial statement that was aggressive yet beautiful at the same time."\n\nAn idiosyncratic list of juxtapositions from some of fashion''s most theatrical and paradoxical design talents, Viktor & Rolf embrace with open arms the more performative aspects of their shows – Russian Doll in 1999 saw model Maggie Rizer swaddled in layers of clothing so as to make her a human matrioshka, while autumn/winter 2001''s Black Hole collection featured models clad entirely in black and painted in the shade too with only their eyes burning out from the colourless void. Meanwhile, for autumn/winter 2008, wool coats came with stand-out, cartoon lettering that read ''NO'' ballooning from their shoulders like tacit speech bubbles. Triumphant beauty indeed.', '00001,00008', 2, 1, '2011-11-21 17:07:31', '', '', '', 8, '', '', '', 0, '', 0, '', 0, 0, 0, 66, '2011-11-21 17:08:05', 1, '2011-11-21 17:32:40', 1, 1, 0, 0),
('Content', 'Andie Loves me Not', '', '', '', '00001,00008', 3, 1, '2011-11-21 17:24:05', '', '', '', 8, '', '', '', 0, '', 0, '', 0, 0, 0, 67, '2011-11-21 17:24:14', 1, '2011-11-21 17:24:14', 1, 1, 0, 0);

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı: `contentlang`
--

DROP TABLE IF EXISTS `contentlang`;
CREATE TABLE `contentlang` (
  `ContentId` int(11) NOT NULL,
  `LangId` int(11) NOT NULL,
  `Title` varchar(200) COLLATE utf8_turkish_ci NOT NULL,
  `Description` text COLLATE utf8_turkish_ci,
  `Metin` text COLLATE utf8_turkish_ci,
  `Picture` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `SpotTitle` varchar(200) COLLATE utf8_turkish_ci DEFAULT NULL,
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `InsertDate` datetime NOT NULL DEFAULT '1990-01-01 00:00:00',
  `InsertUserId` int(11) DEFAULT NULL,
  `UpdateDate` datetime DEFAULT NULL,
  `UpdateUserId` int(11) DEFAULT NULL,
  `Visible` tinyint(1) NOT NULL DEFAULT '1',
  `OrderNo` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=1 ;

--
-- Tablo döküm verisi `contentlang`
--


-- --------------------------------------------------------

--
-- Tablo için tablo yapısı: `contentpicture`
--

DROP TABLE IF EXISTS `contentpicture`;
CREATE TABLE `contentpicture` (
  `ContentId` int(11) DEFAULT NULL,
  `Title` varchar(200) COLLATE utf8_turkish_ci DEFAULT NULL,
  `Description` varchar(300) COLLATE utf8_turkish_ci DEFAULT NULL,
  `FileName` varchar(100) COLLATE utf8_turkish_ci NOT NULL,
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `InsertDate` datetime NOT NULL DEFAULT '1990-01-01 00:00:00',
  `InsertUserId` int(11) DEFAULT NULL,
  `UpdateDate` datetime DEFAULT NULL,
  `UpdateUserId` int(11) DEFAULT NULL,
  `Visible` tinyint(1) NOT NULL DEFAULT '1',
  `OrderNo` int(11) DEFAULT NULL,
  `TagData` text COLLATE utf8_turkish_ci,
  `LikeIt` int(11) DEFAULT '0',
  PRIMARY KEY (`Id`),
  KEY `ContentPicture_ContentIdOrderNo` (`ContentId`,`OrderNo`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=105 ;

--
-- Tablo döküm verisi `contentpicture`
--

INSERT INTO `contentpicture` (`ContentId`, `Title`, `Description`, `FileName`, `Id`, `InsertDate`, `InsertUserId`, `UpdateDate`, `UpdateUserId`, `Visible`, `OrderNo`, `TagData`, `LikeIt`) VALUES
(5, '', '', '/UserFiles/Images/moda/Chrysanthemum.jpg', 14, '2011-11-11 17:16:10', 1, '2011-11-16 18:39:02', 1, 1, 0, '', 1),
(5, '', '', '/UserFiles/Images/moda/Desert.jpg', 15, '2011-11-11 17:16:10', 1, '2011-11-11 17:16:10', 1, 1, 0, '', 0),
(5, '', '', '/UserFiles/Images/moda/Hydrangeas.jpg', 16, '2011-11-11 17:16:10', 1, '2011-11-21 18:27:25', 3, 1, 0, '', 2),
(5, '', '', '/UserFiles/Images/moda/moda_orj.jpg', 17, '2011-11-11 17:16:10', 1, '2011-11-11 17:27:11', 3, 1, 0, '', 1),
(5, '', '', '/UserFiles/Images/moda/Penguins.jpg', 18, '2011-11-11 17:16:10', 1, '2011-11-11 17:16:10', 1, 1, 0, '', 0),
(4, '', '', '/UserFiles/Images/beauty/beauty.jpg', 19, '2011-11-11 17:16:38', 1, '2011-11-11 17:16:38', 1, 1, 0, '', 0),
(4, '', '', '/UserFiles/Images/city/city.jpg', 20, '2011-11-11 17:16:49', 1, '2011-11-11 17:16:49', 1, 1, 0, '', 0),
(4, '', '', '/UserFiles/Images/_upload/2011/11/2/Amsterdam_398.jpg', 21, '2011-11-11 17:17:18', 1, '2011-11-11 17:17:18', 1, 1, 0, '', 0),
(4, '', '', '/UserFiles/Images/_upload/2011/11/2/Bahar''in_ilk_yazisi_547.jpg', 22, '2011-11-11 17:17:18', 1, '2011-11-11 17:17:18', 1, 1, 0, '', 0),
(4, '', '', '/UserFiles/Images/_upload/2011/11/2/Barcelona_294.jpg', 23, '2011-11-11 17:17:18', 1, '2011-11-11 17:17:18', 1, 1, 0, '', 0),
(4, '', '', '/UserFiles/Images/_upload/2011/11/2/Ezginin_Gunlugu_442.jpg', 24, '2011-11-11 17:17:18', 1, '2011-11-18 19:09:16', 3, 1, 0, '', 1),
(4, '', '', '/UserFiles/Images/_upload/2011/11/2/Hakkari_950.jpg', 25, '2011-11-11 17:17:18', 1, '2011-11-11 17:53:51', 1, 1, 0, '[{"x":351,"y":352,"tag":"100 TL","text":"Hakkari Lalesi","url":""},{"x":593,"y":183,"tag":"25 TL","text":"Ağrı Dağı","url":""}]', 0),
(4, '', '', '/UserFiles/Images/_upload/2011/11/2/Kus_Cenneti_917.jpg', 26, '2011-11-11 17:17:18', 1, '2011-11-11 17:17:18', 1, 1, 0, '', 0),
(4, '', '', '/UserFiles/Images/_upload/2011/11/2/Madrid_589.jpg', 27, '2011-11-11 17:17:18', 1, '2011-11-11 17:46:55', 1, 1, 0, '', 1),
(4, '', '', '/UserFiles/Images/_upload/2011/11/2/Milano_260.jpg', 28, '2011-11-11 17:17:18', 1, '2011-11-16 17:27:32', 1, 1, 0, '', 1),
(4, '', '', '/UserFiles/Images/_upload/2011/11/2/New_York_150.jpg', 29, '2011-11-11 17:17:18', 1, '2011-11-16 17:26:58', 1, 1, 0, '', 1),
(4, '', '', '/UserFiles/Images/_upload/2011/11/2/Paris_62.jpg', 30, '2011-11-11 17:17:18', 1, '2011-11-16 17:26:37', 1, 1, 0, '', 1),
(4, '', '', '/UserFiles/Images/_upload/2011/11/2/Surdan_burdan_444.jpg', 31, '2011-11-11 17:17:18', 1, '2011-11-21 18:32:48', 3, 1, 0, '', 7),
(4, '', '', '/UserFiles/Images/_upload/2011/11/2/Van_Depremi_238.jpg', 32, '2011-11-11 17:17:18', 1, '2011-11-21 18:32:35', 3, 1, 0, '', 2),
(4, '', '', '/UserFiles/Images/_upload/2011/11/2/Yeni_Turku_137.jpg', 33, '2011-11-11 17:17:18', 1, '2011-11-11 17:46:41', 1, 1, 0, '', 1),
(61, '', '', '/UserFiles/Images/moda/one_from_the_heart/1.jpg', 69, '2011-11-19 19:31:04', 1, '2011-11-19 19:31:04', 1, 1, 0, '', 0),
(61, '', '', '/UserFiles/Images/moda/one_from_the_heart/2.jpg', 70, '2011-11-19 19:31:04', 1, '2011-11-21 00:56:17', 1, 1, 0, '[{"x":205,"y":205,"tag":"40 TL","text":"Benetton Gözlük","url":"http://www.benetton.com"}]', 0),
(61, '', '', '/UserFiles/Images/moda/one_from_the_heart/1.jpg', 71, '2011-11-19 19:31:18', 1, '2011-11-19 19:31:18', 1, 1, 0, '', 0),
(61, '', '', '/UserFiles/Images/moda/one_from_the_heart/2.jpg', 72, '2011-11-19 19:31:18', 1, '2011-11-19 19:31:18', 1, 1, 0, '', 0),
(61, '', '', '/UserFiles/Images/moda/one_from_the_heart/1.jpg', 73, '2011-11-19 19:31:38', 1, '2011-11-19 19:31:38', 1, 1, 0, '', 0),
(61, '', '', '/UserFiles/Images/moda/one_from_the_heart/2.jpg', 74, '2011-11-19 19:31:38', 1, '2011-11-19 19:31:38', 1, 1, 0, '', 0),
(62, '', '', '/UserFiles/468.jpg', 75, '2011-11-21 15:23:54', 1, '2011-11-21 15:23:54', 1, 1, 0, '', 0),
(62, '', '', '/UserFiles/Images/moda/old_fashion/1.jpg', 77, '2011-11-21 15:24:39', 1, '2011-11-21 15:24:39', 1, 1, 0, '', 0),
(62, '', '', '/UserFiles/Images/moda/old_fashion/7.jpg', 78, '2011-11-21 15:25:06', 1, '2011-11-21 15:25:06', 1, 1, 0, '', 0),
(62, '', '', '/UserFiles/Images/moda/old_fashion/5.jpg', 79, '2011-11-21 15:25:25', 1, '2011-11-21 15:25:25', 1, 1, 0, '', 0),
(62, '', '', '/UserFiles/Images/moda/old_fashion/3.jpg', 80, '2011-11-21 15:25:49', 1, '2011-11-21 15:27:02', 1, 1, 0, '[{"x":157,"y":266,"tag":"495TL","text":"Cassetta","url":"http://www.casetta.com"}]', 0),
(62, '', '', '/UserFiles/Images/moda/old_fashion/4.jpg', 81, '2011-11-21 15:34:26', 1, '2011-11-21 15:34:26', 1, 1, 0, '', 0),
(62, '', '', '/UserFiles/Images/moda/old_fashion/1052.jpg', 82, '2011-11-21 15:40:00', 1, '2011-11-21 15:40:00', 1, 1, 0, '', 0),
(63, '', '', '/UserFiles/Images/moda/Big_City_Life/57026-800w.jpg', 83, '2011-11-21 15:46:40', 1, '2011-11-21 15:46:40', 1, 1, 0, '', 0),
(63, '', '', '/UserFiles/Images/moda/Big_City_Life/12.jpg', 84, '2011-11-21 16:11:33', 1, '2011-11-21 16:11:33', 1, 1, 0, '', 0),
(63, '', '', '/UserFiles/Images/moda/Big_City_Life/13.jpg', 86, '2011-11-21 16:12:47', 1, '2011-11-21 16:12:47', 1, 1, 0, '', 0),
(64, '', '', '/UserFiles/sugar1.jpg', 87, '2011-11-21 16:25:49', 1, '2011-11-21 16:25:49', 1, 1, 0, '', 0),
(64, '', '', '/UserFiles/Images/moda/Sugar/sugar2.jpg', 88, '2011-11-21 16:26:34', 1, '2011-11-21 17:02:08', 1, 1, 0, '[{"x":358,"y":203,"tag":"299 TL","text":"blunn","url":"http://www.blunn.com"}]', 0),
(64, '', '', '/UserFiles/Images/moda/Sugar/sugar3.jpg', 89, '2011-11-21 16:26:46', 1, '2011-11-21 16:26:46', 1, 1, 0, '', 0),
(64, '', '', '/UserFiles/Images/moda/Sugar/sugar4.jpg', 90, '2011-11-21 16:27:00', 1, '2011-11-21 16:27:00', 1, 1, 0, '', 0),
(19, '', '', '/UserFiles/Images/people/people.jpg', 91, '2011-11-21 16:37:21', 1, '2011-11-21 16:37:21', 1, 1, 0, '', 0),
(19, '', '', '/UserFiles/Images/people/people1.jpg', 92, '2011-11-21 16:40:02', 1, '2011-11-21 16:40:02', 1, 1, 0, '', 0),
(19, '', '', '/UserFiles/Images/people/people3.jpg', 93, '2011-11-21 16:40:11', 1, '2011-11-21 16:40:11', 1, 1, 0, '', 0),
(19, '', '', '/UserFiles/Images/people/people4.jpg', 94, '2011-11-21 16:40:35', 1, '2011-11-21 16:40:35', 1, 1, 0, '', 0),
(19, '', '', '/UserFiles/Images/people/people5.jpg', 95, '2011-11-21 16:43:30', 1, '2011-11-21 16:43:30', 1, 1, 0, '', 0),
(19, '', '', '/UserFiles/Images/people/people6.jpg', 96, '2011-11-21 16:43:48', 1, '2011-11-21 16:43:48', 1, 1, 0, '', 0),
(19, '', '', '/UserFiles/Images/people/people7.jpg', 97, '2011-11-21 16:44:07', 1, '2011-11-21 16:44:07', 1, 1, 0, '', 0),
(65, '', '', '/UserFiles/intro/images/back1.jpg', 98, '2011-11-21 16:44:19', 1, '2011-11-21 16:44:19', 1, 1, 0, '', 0),
(65, '', '', '/UserFiles/intro/images/back2.jpg', 99, '2011-11-21 16:44:19', 1, '2011-11-21 16:44:19', 1, 1, 0, '', 0),
(65, 'Micheal Jackson', '', '/UserFiles/intro/music/music1.mp3', 100, '2011-11-21 16:44:46', 1, '2011-11-21 17:18:36', 1, 1, 0, '', 0),
(65, '', '', '/UserFiles/sugar1.jpg', 101, '2011-11-21 17:15:27', 1, '2011-11-21 17:15:27', 1, 1, 0, '', 0),
(65, 'deneme 2', '', '/UserFiles/intro/music/music1.mp3', 102, '2011-11-21 17:19:25', 1, '2011-11-21 17:19:37', 1, 1, 0, '', 0),
(66, '', '', '/UserFiles/andi.jpg', 103, '2011-11-21 17:31:18', 1, '2011-11-21 17:31:18', 1, 1, 0, '', 0),
(66, '', '', '/UserFiles/girl.jpg', 104, '2011-11-21 17:31:28', 1, '2011-11-21 17:31:28', 1, 1, 0, '', 0);

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı: `contentsource`
--

DROP TABLE IF EXISTS `contentsource`;
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=1 ;

--
-- Tablo döküm verisi `contentsource`
--


-- --------------------------------------------------------

--
-- Tablo için tablo yapısı: `contenttag`
--

DROP TABLE IF EXISTS `contenttag`;
CREATE TABLE `contenttag` (
  `ContentId` int(11) NOT NULL,
  `TagId` int(11) NOT NULL,
  `Rank` int(11) NOT NULL DEFAULT '0',
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `InsertDate` datetime NOT NULL DEFAULT '1990-01-01 00:00:00',
  `InsertUserId` int(11) DEFAULT NULL,
  `UpdateDate` datetime DEFAULT NULL,
  `UpdateUserId` int(11) DEFAULT NULL,
  `Visible` tinyint(1) NOT NULL DEFAULT '1',
  `OrderNo` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `ContentTag_ContentIdTagId` (`ContentId`,`TagId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=11 ;

--
-- Tablo döküm verisi `contenttag`
--

INSERT INTO `contenttag` (`ContentId`, `TagId`, `Rank`, `Id`, `InsertDate`, `InsertUserId`, `UpdateDate`, `UpdateUserId`, `Visible`, `OrderNo`) VALUES
(14, 1, 0, 1, '2011-10-28 20:22:17', 1, '2011-10-28 20:22:17', 1, 1, 0),
(29, 2, 0, 2, '2011-10-30 16:35:35', 1, '2011-10-30 16:35:35', 1, 1, 0),
(30, 3, 0, 3, '2011-10-30 16:36:42', 1, '2011-10-30 16:36:42', 1, 1, 0),
(11, 1, 0, 5, '2011-10-30 16:40:38', 1, '2011-10-30 16:40:38', 1, 1, 0),
(18, 1, 0, 6, '2011-10-30 16:41:01', 1, '2011-10-30 16:41:01', 1, 1, 0),
(13, 5, 0, 7, '2011-11-02 17:15:27', 1, '2011-11-02 17:15:27', 1, 1, 0),
(19, 6, 0, 8, '2011-11-02 17:21:33', 1, '2011-11-02 17:21:33', 1, 1, 0),
(41, 7, 0, 9, '2011-11-02 18:37:49', 1, '2011-11-02 18:37:49', 1, 1, 0),
(40, 8, 0, 10, '2011-11-02 18:38:01', 1, '2011-11-02 18:38:01', 1, 1, 0);

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı: `lang`
--

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
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=2 ;

--
-- Tablo döküm verisi `lang`
--

INSERT INTO `lang` (`Code`, `Name`, `Id`, `InsertDate`, `InsertUserId`, `UpdateDate`, `UpdateUserId`, `Visible`, `OrderNo`) VALUES
('tr-TR', 'Türkçe', 1, '1990-01-01 00:00:00', NULL, NULL, NULL, 1, NULL);

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı: `log`
--

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
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=10 ;

--
-- Tablo döküm verisi `log`
--

INSERT INTO `log` (`LogType`, `Category`, `Description`, `Id`, `InsertDate`, `InsertUserId`, `UpdateDate`, `UpdateUserId`, `Visible`, `OrderNo`) VALUES
('Notice', 'Subscribe', 'bulentkeskin@gmail.com', 3, '2011-11-15 23:39:17', 3, '2011-11-15 23:39:17', 3, 1, 0),
('Notice', 'Subscribe', 'minyaturist@gmail.com', 4, '2011-11-15 23:42:15', 1, '2011-11-15 23:42:15', 1, 1, 0),
('Notice', 'Subscribe', 'deneme@bes.com', 5, '2011-11-15 23:55:35', 1, '2011-11-15 23:55:35', 1, 1, 0),
('Notice', 'Subscribe', 'dene-me.bir@bes.com.tr.tc', 6, '2011-11-15 23:55:54', 1, '2011-11-15 23:55:54', 1, 1, 0),
('Notice', 'Subscribe', 'ali@veli.com', 7, '2011-11-09 00:00:00', 1, '2011-11-09 00:00:00', 1, 1, 0),
('Notice', 'Subscribe', 'yagmur.kali@artechin.com', 8, '2011-11-18 17:45:40', 3, '2011-11-18 17:45:40', 3, 1, 0),
('Notice', 'Subscribe', 'yagmurkali1@hotmail.com', 9, '2011-11-21 17:52:47', 3, '2011-11-21 17:52:47', 3, 1, 0);

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı: `module`
--

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
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=223 ;

--
-- Tablo döküm verisi `module`
--

INSERT INTO `module` (`Id`, `Template`, `Region`, `OrderNo`, `Name`, `CSS`, `Details`, `TopHtml`, `BottomHtml`, `ParentModuleId`, `CSSClass`, `UseCache`, `CacheLifeTime`) VALUES
(23, 'Default.aspx', 'Header', 0, 'StaticHtml', '#StaticHtml_23 {\n}\n\n#StaticHtml_23 .headerMenu { border-bottom:1px solid black}\n#StaticHtml_23 .tab {\n	border:1px solid black; \n	border-bottom:none;\n	float:right; \n	padding:2px 40px;\n	cursor:pointer;\n	margin-left:10px;\n}\n#StaticHtml_23 .search {\n}\n#StaticHtml_23 .subscribe {\n	background:black;\n	color:white;\n}\n#StaticHtml_23 #subscribeForm {\n    background: none repeat scroll 0 0 white;\n    border: 1px solid black;\n    height: 40px;\n    margin: 0px;\n    position: absolute;\n    top: 123px;\n    width: 200px;\n}\n#StaticHtml_23 #email {\n	border:1px solid black;\n	margin-top: 9px;\n}\n#StaticHtml_23 #btnGo {\n    background:black;\n    color:white;\n    border: 1px solid black;\n    width: 31px;\n}', 'Cinar.CMS.Serialization\nInnerHtml,1511,<a href="/Default.aspx"><img src="/UserFiles/Images/design/logo.jpg" border="0"/></a>\n\n<form id="subscribeForm" class="hideOnOut" style="display:none"  onsubmit="return subscribe(this)">\n<input id="email"/><input id="btnGo" type="submit" value="Go"/>\n</form>\n\n\n<div class="headerMenu">\n<div class="subscribe tab hideOnOutException" onclick="showSubscribe(this)">Subscribe <img src="/UserFiles/Images/design/ok_up.png"/></div>\n<div class="search tab" id="searchTab">Search</div>\n<div style="clear:both"></div>\n</div>\n\n<script>\ndocument.observe(''dom:loaded'', function(){\n	var sf = \\$(''SearchForm_82'');\n	sf.hide();\n	var tab = \\$(''searchTab'');\n	tab.on(''click'', function(){\n		sf.show();\n		sf.setStyle({left:tab.viewportOffset().left+5});\n		sf.down(''.searchText'').focus();\n	});\n	sf.down(''.searchText'').on(''blur'', function(){\n		sf.hide();\n	});\n});\n\nfunction showSubscribe(tab){\n	var form = \\$(''subscribeForm'').setStyle({left:tab.viewportOffset().left-48});\n	form.toggle();\n}\nfunction subscribe(form){\n	form = \\$(form);\n	var email = form.down(''#email'').value;\n	var reg = /^([A-Za-z0-9_\\\\-\\\\.])+\\\\@([A-Za-z0-9_\\\\-\\\\.])+\\\\.([A-Za-z]{2,4})\\$/;\n	if(!reg.test(email)){\n		alert(''Invalid email address. Please correct.'');\n		return false;\n	}\n	var res = ajax({\n		url:''Subscribe.ashx?email=''+email,\n		isJSON:false,\n		noCache:true\n	});\n	if(res==''OK''){\n		alert(''Thanks for subscription'');\n		form.down(''#email'').value = '''';\n		form.hide();\n	}\n	else\n		alert(''Something went wrong, please try again later.'');\n	return false;\n}\n</script>LangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,2,23Template,12,Default.aspxRegion,6,HeaderOrderNo,1,0Name,10,StaticHtmlCSS,692,#StaticHtml_23 {\n}\n\n#StaticHtml_23 .headerMenu { border-bottom:1px solid black}\n#StaticHtml_23 .tab {\n	border:1px solid black; \n	border-bottom:none;\n	float:right; \n	padding:2px 40px;\n	cursor:pointer;\n	margin-left:10px;\n}\n#StaticHtml_23 .search {\n}\n#StaticHtml_23 .subscribe {\n	background:black;\n	color:white;\n}\n#StaticHtml_23 #subscribeForm {\n    background: none repeat scroll 0 0 white;\n    border: 1px solid black;\n    height: 40px;\n    margin: 0px;\n    position: absolute;\n    top: 123px;\n    width: 200px;\n}\n#StaticHtml_23 #email {\n	border:1px solid black;\n	margin-top: 9px;\n}\n#StaticHtml_23 #btnGo {\n    background:black;\n    color:white;\n    border: 1px solid black;\n    width: 31px;\n}TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(24, 'Default.aspx', 'Footer', 0, 'StaticHtml', '#StaticHtml_24 {\n	margin-top:50px;\n}\n#StaticHtml_24 .footerInner {\n	border-top:1px solid black;\n	border-bottom:1px solid black;\n	text-align:center;\n	font-family: Spinnaker, tahoma;\n	padding:4px;\n	clear:both;\n	margin-top:10px;\n	margin-bottom:20px;\n}\n', 'Cinar.CMS.Serialization\nInnerHtml,1333,<p align="center">\n<a title="facebook" onclick="window.open(''http://www.facebook.com/sharer.php?u=''+encodeURIComponent(location.href), ''facebook'',''toolbar=yes,scrollbars=yes,resizable=yes,width=800,height=600''); return false;" href="javascript:;"><img src="/UserFiles/Images/design/facebook.png" border="0"/></a>\n<a title="twitter" onclick="window.open(''http://twitter.com/?status=''+encodeURIComponent(document.title)+'' ''+encodeURIComponent(location.href), ''twitter'',''toolbar=yes,scrollbars=yes,resizable=yes,width=800,height=600''); return false;" href="javascript:;"><img src="/UserFiles/Images/design/twitter.png" border="0"/></a>\n<a title="google" onclick="window.open(''http://www.google.com/bookmarks/mark?op=edit&amp;bkmk=''+encodeURIComponent(location.href)+''&amp;title=''+encodeURIComponent(document.title)+''&amp;source=Blank'', ''google'',''toolbar=yes,scrollbars=yes,resizable=yes,width=800,height=600''); return false;" href="javascript:;"><img src="/UserFiles/Images/design/google.png" border="0"/></a>\n</p>\n\n<div class="footerInner">\n<a href="/Other.aspx?item=33">ABOUT US</a> &nbsp; &nbsp; &nbsp;\n<a href="/Other.aspx?item=34">CONTACT US</a> &nbsp; &nbsp; &nbsp;\n<a href="/">© 2011 BLANK-MAGAZINE.COM</a>\n</div>\n\n$\nvar i = 1;\nforeach(var sql in Provider.Database.SQLLog)\n	echo(i++ + ". " + sql.Replace("\\n","<br/>")+"<hr/>");\n$LangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,2,24Template,12,Default.aspxRegion,6,FooterOrderNo,1,0Name,10,StaticHtmlCSS,249,#StaticHtml_24 {\n	margin-top:50px;\n}\n#StaticHtml_24 .footerInner {\n	border-top:1px solid black;\n	border-bottom:1px solid black;\n	text-align:center;\n	font-family: Spinnaker, tahoma;\n	padding:4px;\n	clear:both;\n	margin-top:10px;\n	margin-bottom:20px;\n}\nTopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(25, 'Default.aspx', 'ContentLeft', 0, 'ContentListByFilter', '', 'Cinar.CMS.Serialization\nFilter,10,AuthorId=2HowManyItems,1,1SkipFirst,1,0Random,1,0OrderBy,19,Content.PublishDateAscending,5,FalseShowPictureLeftRight,5,FalseShowCurrentContent,5,FalseShowFirstItemWithPicture,5,FalseWhichPicture,17,TAuthorId.PicturePictureWidth,3,218PictureHeight,1,0BulletIcon,0,TitleLength,2,50DescriptionLength,3,150DateFormat,3,agoFieldOrder,29,image,author,description,dateCreateLink,4,TrueLinkTarget,0,ShowDescriptionAsLink,5,FalseUseTemplate,15,AuthorList.aspxForceToUseTemplate,4,TrueMoreLink,0,DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,2,25Template,12,Default.aspxRegion,11,ContentLeftOrderNo,1,0Name,19,ContentListByFilterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,24,vitrin shadow toogleDescUseCache,7,DefaultCacheLifeTime,1,0CropPicture,5,FalseDivClassName,12,extraItemDiv', NULL, NULL, 0, NULL, 'Default', NULL),
(26, 'Default.aspx', 'ContentRight', 0, 'ContentListByFilter', '', 'Cinar.CMS.Serialization\nFilter,10,AuthorId=3HowManyItems,1,1SkipFirst,1,0Random,1,0OrderBy,19,Content.PublishDateAscending,5,FalseShowPictureLeftRight,5,FalseShowCurrentContent,5,FalseShowFirstItemWithPicture,5,FalseWhichPicture,17,TAuthorId.PicturePictureWidth,3,218PictureHeight,1,0BulletIcon,0,TitleLength,2,50DescriptionLength,3,150DateFormat,3,agoFieldOrder,29,image,author,description,dateCreateLink,4,TrueLinkTarget,0,ShowDescriptionAsLink,5,FalseUseTemplate,15,AuthorList.aspxForceToUseTemplate,4,TrueMoreLink,0,DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,2,26Template,12,Default.aspxRegion,12,ContentRightOrderNo,1,0Name,19,ContentListByFilterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,24,vitrin shadow toogleDescUseCache,7,DefaultCacheLifeTime,1,0CropPicture,5,FalseDivClassName,12,extraItemDiv', NULL, NULL, 0, NULL, 'Default', NULL),
(27, 'Default.aspx', 'ContentLeft', 1, 'ContentListByFilter', '', 'Cinar.CMS.Serialization\nFilter,12,CategoryId=3HowManyItems,1,1SkipFirst,1,0Random,1,0OrderBy,19,Content.PublishDateAscending,5,FalseShowPictureLeftRight,5,FalseShowCurrentContent,5,FalseShowFirstItemWithPicture,5,FalseWhichPicture,15,Content.PicturePictureWidth,3,218PictureHeight,1,0BulletIcon,0,TitleLength,2,50DescriptionLength,3,150DateFormat,3,agoFieldOrder,31,image,category,description,dateCreateLink,4,TrueLinkTarget,0,ShowDescriptionAsLink,5,FalseUseTemplate,9,City.aspxForceToUseTemplate,4,TrueMoreLink,0,DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,2,27Template,12,Default.aspxRegion,11,ContentLeftOrderNo,1,1Name,19,ContentListByFilterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,24,vitrin shadow toogleDescUseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(28, 'Default.aspx', 'ContentRight', 1, 'ContentListByFilter', '', 'Cinar.CMS.Serialization\nFilter,13,CategoryId=15HowManyItems,1,1SkipFirst,1,0Random,1,0OrderBy,19,Content.PublishDateAscending,5,FalseShowPictureLeftRight,5,FalseShowCurrentContent,5,FalseShowFirstItemWithPicture,5,FalseWhichPicture,15,Content.PicturePictureWidth,3,218PictureHeight,1,0BulletIcon,0,TitleLength,2,50DescriptionLength,3,150DateFormat,3,agoFieldOrder,31,image,category,description,dateCreateLink,4,TrueLinkTarget,0,ShowDescriptionAsLink,5,FalseUseTemplate,0,ForceToUseTemplate,5,FalseMoreLink,0,DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,2,28Template,12,Default.aspxRegion,12,ContentRightOrderNo,1,1Name,19,ContentListByFilterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,24,vitrin shadow toogleDescUseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(29, 'Default.aspx', 'Content', 0, 'ContentListByFilter', '#ContentListByFilter_29 {\n	margin-top:10px;\n	height:367px;\n}\n', 'Cinar.CMS.Serialization\nFilter,12,CategoryId=6HowManyItems,1,5SkipFirst,1,0Random,1,0OrderBy,10,Content.IdAscending,5,FalseShowPictureLeftRight,5,FalseShowCurrentContent,5,FalseShowFirstItemWithPicture,5,FalseWhichPicture,15,Content.PicturePictureWidth,3,490PictureHeight,3,368BulletIcon,0,TitleLength,2,50DescriptionLength,3,150DateFormat,12,dd MMMM yyyyFieldOrder,5,imageCreateLink,4,TrueLinkTarget,0,ShowDescriptionAsLink,5,FalseUseTemplate,0,ForceToUseTemplate,5,FalseMoreLink,0,DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,2,29Template,12,Default.aspxRegion,7,ContentOrderNo,1,0Name,19,ContentListByFilterCSS,61,#ContentListByFilter_29 {\n	margin-top:10px;\n	height:367px;\n}\nTopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,8,fadeShowUseCache,7,DefaultCacheLifeTime,1,0DivClassName,12,extraItemDivCropPicture,4,True', NULL, NULL, 0, NULL, 'Default', NULL),
(30, 'Default.aspx', 'ContentLeft', 2, 'ContentListByFilter', '', 'Cinar.CMS.Serialization\nFilter,12,CategoryId=9HowManyItems,1,1SkipFirst,1,0Random,1,0OrderBy,19,Content.PublishDateAscending,5,FalseShowPictureLeftRight,5,FalseShowCurrentContent,5,FalseShowFirstItemWithPicture,5,FalseWhichPicture,15,Content.PicturePictureWidth,3,218PictureHeight,1,0BulletIcon,0,TitleLength,2,50DescriptionLength,3,150DateFormat,3,agoFieldOrder,31,image,category,description,dateCreateLink,4,TrueLinkTarget,0,ShowDescriptionAsLink,5,FalseUseTemplate,0,ForceToUseTemplate,5,FalseMoreLink,0,DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,2,30Template,12,Default.aspxRegion,11,ContentLeftOrderNo,1,2Name,19,ContentListByFilterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,24,vitrin shadow toogleDescUseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(31, 'Default.aspx', 'ContentRight', 2, 'ContentListByFilter', '', 'Cinar.CMS.Serialization\nFilter,13,CategoryId=10HowManyItems,1,1SkipFirst,1,0Random,1,0OrderBy,19,Content.PublishDateAscending,5,FalseShowPictureLeftRight,5,FalseShowCurrentContent,5,FalseShowFirstItemWithPicture,5,FalseWhichPicture,15,Content.PicturePictureWidth,3,218PictureHeight,1,0BulletIcon,0,TitleLength,2,50DescriptionLength,3,150DateFormat,3,agoFieldOrder,31,image,category,description,dateCreateLink,4,TrueLinkTarget,0,ShowDescriptionAsLink,5,FalseUseTemplate,0,ForceToUseTemplate,5,FalseMoreLink,0,DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,2,31Template,12,Default.aspxRegion,12,ContentRightOrderNo,1,2Name,19,ContentListByFilterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,24,vitrin shadow toogleDescUseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(43, 'Author.aspx', 'Footer', 2, 'RegionRepeater', '', 'Cinar.CMS.Serialization\nId,2,43Template,11,Author.aspxRegion,6,FooterOrderNo,1,0Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0RegionToCopy,6,FooterPage,12,Default.aspx', NULL, NULL, 0, NULL, 'Default', NULL),
(44, 'Author.aspx', 'Content', 0, 'ContentDisplay', '', 'Cinar.CMS.Serialization\nFieldOrder,22,title,date,region,textDateFormat,12,dd MMMM yyyyFilter,11,Id=@ContentTagTemplate,0,Id,2,44Template,11,Author.aspxRegion,7,ContentOrderNo,1,0Name,14,ContentDisplayCSS,0,TopHtml,22,<div class="innerDiv">BottomHtml,1063,</div>\n<p style="text-align:right;padding:20px 20px;background:white">\n<a title="facebook" onclick="window.open(''http://www.facebook.com/sharer.php?u=''+encodeURIComponent(location.href), ''facebook'',''toolbar=yes,scrollbars=yes,resizable=yes,width=800,height=600''); return false;" href="javascript:;"><img src="/UserFiles/Images/design/facebook.png" border="0"/></a>\n<a title="twitter" onclick="window.open(''http://twitter.com/?status=''+encodeURIComponent(document.title)+'' ''+encodeURIComponent(location.href), ''twitter'',''toolbar=yes,scrollbars=yes,resizable=yes,width=800,height=600''); return false;" href="javascript:;"><img src="/UserFiles/Images/design/twitter.png" border="0"/></a>\n<a title="google" onclick="window.open(''http://www.google.com/bookmarks/mark?op=edit&amp;bkmk=''+encodeURIComponent(location.href)+''&amp;title=''+encodeURIComponent(document.title)+''&amp;source=Blank'', ''google'',''toolbar=yes,scrollbars=yes,resizable=yes,width=800,height=600''); return false;" href="javascript:;"><img src="/UserFiles/Images/design/google.png" border="0"/></a>\n</p>ParentModuleId,1,0CSSClass,24,shadow clsContentDisplayUseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(47, 'Author.aspx', 'ContentLeft', 2, 'SQLDataList', '', 'Cinar.CMS.Serialization\nSQL,180,SELECT distinct\n	t.Id,\n	t.Name\nFROM\n	Tag t\n	INNER JOIN ContentTag ct ON ct.TagId = t.Id\n	INNER JOIN Content c ON c.Id = ct.ContentId\nWHERE\n	c.AuthorId = $=Context.Content.AuthorId$DataTemplate,110,<div class="tag">\n<a href="/AuthorList.aspx?item=$=Context.Content.Id$&tagId=$=row.Id$">$=row.Name$</a>\n</div>PictureWidth,1,0PictureHeight,1,0DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,2,47Template,11,Author.aspxRegion,11,ContentLeftOrderNo,1,2Name,11,SQLDataListCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,10,solda tagsUseCache,7,DefaultCacheLifeTime,1,0CropPicture,5,FalseDivClassName,12,extraItemDiv', NULL, NULL, 0, NULL, 'Default', NULL),
(50, 'Author.aspx', 'conRegion44', 0, 'ContentGallery', '#ContentGallery_50 {\n	width:640px;\n	height:420px;\n	margin-bottom:25px;\n}\n\n', 'Cinar.CMS.Serialization\nPictureWidth,3,640PictureHeight,3,420ShowTitle,5,FalseShowTitleFirst,5,FalseDisplayAsTable,5,FalseCols,1,1EncloseWithDiv,4,TrueDivClassName,6,clItemCSS,74,#ContentGallery_50 {\n	width:640px;\n	height:420px;\n	margin-bottom:25px;\n}\n\nTopHtml,0,BottomHtml,0,CSSClass,14,fadeWithArrowsUseCache,7,DefaultCacheLifeTime,1,0Id,2,50Template,11,Author.aspxRegion,11,conRegion44OrderNo,1,0Name,14,ContentGalleryParentModuleId,2,44CropPicture,4,True', NULL, NULL, 44, NULL, 'Default', NULL),
(53, 'Author.aspx', 'Footer', 1, 'ContentListByTag', '#ContentListByTag_53 div.title {\n	border-bottom:1px solid black;\n	padding:50px 0px 5px 65px;\n	margin-bottom:30px;\n	font-family:spinnaker;\n}\n\n#ContentListByTag_53 div.paging {\n	float:left;\n	width:60px;\n	height:120px;\n	background-repeat:no-repeat;\n}\n\n#ContentListByTag_53 .cerceve {\n	float:left;\n	width:855px;\n	border: 1px solid #efefef;\n\n}\n\n#ContentListByTag_53 div.clItem {\n	float:left;\n	margin:0px 5px 5px 0px;\n}\n', 'Cinar.CMS.Serialization\nFilter,16,AuthorId=@AuthorHowManyItems,2,10SkipFirst,1,0Random,1,0OrderBy,19,Content.PublishDateAscending,5,FalseShowPictureLeftRight,5,FalseShowCurrentContent,4,TrueShowFirstItemWithPicture,5,FalseWhichPicture,15,Content.PicturePictureWidth,1,0PictureHeight,3,120BulletIcon,0,TitleLength,2,50DescriptionLength,3,150DateFormat,12,dd MMMM yyyyFieldOrder,5,imageCreateLink,4,TrueLinkTarget,0,ShowDescriptionAsLink,5,FalseUseTemplate,0,ForceToUseTemplate,5,FalseMoreLink,0,DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,2,53Template,11,Author.aspxRegion,6,FooterOrderNo,1,1Name,16,ContentListByTagCSS,414,#ContentListByTag_53 div.title {\n	border-bottom:1px solid black;\n	padding:50px 0px 5px 65px;\n	margin-bottom:30px;\n	font-family:spinnaker;\n}\n\n#ContentListByTag_53 div.paging {\n	float:left;\n	width:60px;\n	height:120px;\n	background-repeat:no-repeat;\n}\n\n#ContentListByTag_53 .cerceve {\n	float:left;\n	width:855px;\n	border: 1px solid #efefef;\n\n}\n\n#ContentListByTag_53 div.clItem {\n	float:left;\n	margin:0px 5px 5px 0px;\n}\nTopHtml,182,<div class="title">ARCHIVE</div>\n\n<div class="paging" style="background-image:url(/UserFiles/Images/design/prev.png); background-position:right center;"></div>\n\n<div class="cerceve">BottomHtml,163,</div>\n<div class="paging"  style="background-image:url(/UserFiles/Images/design/next.png); background-position:left center;"></div>\n<div style="clear:both"></div>ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0DivClassName,12,extraItemDiv', NULL, NULL, 0, NULL, 'Default', NULL),
(55, 'AuthorList.aspx', 'Header', 0, 'RegionRepeater', '', 'Cinar.CMS.Serialization\nId,2,55Template,15,AuthorList.aspxRegion,6,HeaderOrderNo,1,0Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0RegionToCopy,6,HeaderPage,12,Default.aspx', NULL, NULL, 0, NULL, 'Default', NULL),
(56, 'AuthorList.aspx', 'Header', 1, 'StaticHtml', '#StaticHtml_56 {\n}\n\n#StaticHtml_56 .headerMenu { border-bottom:1px solid black}\n#StaticHtml_56 .tab {\n	border:1pxlid black; \n	border-bottom:none;\n	float:right; \n	padding:5px 20px;\n	cursor:pointer;\n	margin-left:10px;\n}\n#StaticHtml_56 .search {\n}\n#StaticHtml_56 .subscribe {\n	background:black;\n	color:white;\n}', 'Cinar.CMS.Serialization\nInnerHtml,229,<a href="/Default.aspx"><img src="/UserFiles/Images/design/logo.jpg" border="0"/></a>\n\n<div class="headerMenu">\n<div class="subscribe tab">Subscribe</div>\n<div class="search tab">Search</div>\n<div style="clear:both"></div>\n</div>LangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,2,56Template,15,AuthorList.aspxRegion,6,HeaderOrderNo,1,1Name,10,StaticHtmlCSS,307,#StaticHtml_56 {\n}\n\n#StaticHtml_56 .headerMenu { border-bottom:1px solid black}\n#StaticHtml_56 .tab {\n	border:1pxlid black; \n	border-bottom:none;\n	float:right; \n	padding:5px 20px;\n	cursor:pointer;\n	margin-left:10px;\n}\n#StaticHtml_56 .search {\n}\n#StaticHtml_56 .subscribe {\n	background:black;\n	color:white;\n}TopHtml,0,BottomHtml,0,ParentModuleId,2,55CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 55, NULL, 'Default', NULL),
(60, 'AuthorList.aspx', 'Footer', 0, 'ContentListByTag', '#ContentListByTag_60 div.title {\n	border-bottom:1px solid black;\n	padding:50px 0px 5px 65px;\n	margin-bottom:30px;\n	font-family:spinnaker;\n}\n\n#ContentListByTag_60 div.paging {\n	float:left;\n	width:60px;\n	height:120px;\n	background-repeat:no-repeat;\n}\n\n#ContentListByTag_60 .cerceve {\n	float:left;\n	width:855px;\n	border: 1px solid #efefef;\n\n}\n\n#ContentListByTag_60 div.clItem {\n	float:left;\n	margin:0px 5px 5px 0px;\n}\n', 'Cinar.CMS.Serialization\nFilter,16,AuthorId=@AuthorHowManyItems,2,10SkipFirst,1,0Random,1,0OrderBy,19,Content.PublishDateAscending,5,FalseShowPictureLeftRight,5,FalseShowCurrentContent,4,TrueShowFirstItemWithPicture,5,FalseWhichPicture,15,Content.PicturePictureWidth,1,0PictureHeight,3,120BulletIcon,0,TitleLength,2,50DescriptionLength,3,150DateFormat,12,dd MMMM yyyyFieldOrder,5,imageCreateLink,4,TrueLinkTarget,0,ShowDescriptionAsLink,5,FalseUseTemplate,0,ForceToUseTemplate,5,FalseMoreLink,0,DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,2,60Template,15,AuthorList.aspxRegion,6,FooterOrderNo,1,0Name,16,ContentListByTagCSS,414,#ContentListByTag_60 div.title {\n	border-bottom:1px solid black;\n	padding:50px 0px 5px 65px;\n	margin-bottom:30px;\n	font-family:spinnaker;\n}\n\n#ContentListByTag_60 div.paging {\n	float:left;\n	width:60px;\n	height:120px;\n	background-repeat:no-repeat;\n}\n\n#ContentListByTag_60 .cerceve {\n	float:left;\n	width:855px;\n	border: 1px solid #efefef;\n\n}\n\n#ContentListByTag_60 div.clItem {\n	float:left;\n	margin:0px 5px 5px 0px;\n}\nTopHtml,182,<div class="title">ARCHIVE</div>\n\n<div class="paging" style="background-image:url(/UserFiles/Images/design/prev.png); background-position:right center;"></div>\n\n<div class="cerceve">BottomHtml,163,</div>\n<div class="paging"  style="background-image:url(/UserFiles/Images/design/next.png); background-position:left center;"></div>\n<div style="clear:both"></div>ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0DivClassName,12,extraItemDivCropPicture,5,False', NULL, NULL, 0, NULL, 'Default', NULL),
(61, 'AuthorList.aspx', 'Footer', 1, 'RegionRepeater', '', 'Cinar.CMS.Serialization\nId,2,61Template,15,AuthorList.aspxRegion,6,FooterOrderNo,1,1Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0RegionToCopy,6,FooterPage,12,Default.aspx', NULL, NULL, 0, NULL, 'Default', NULL),
(62, 'AuthorList.aspx', 'Footer', 2, 'StaticHtml', '#StaticHtml_62 {\n	border-top:1px solid black;\n	text-align:center;\n	font-weight:bold;\n	padding:4px;\n	margin-top:10px;\n	clear:both;\n	margin-top:30px;\n}\n', 'Cinar.CMS.Serialization\nInnerHtml,134,<a href="">ABOUT US</a> &nbsp; &nbsp; &nbsp;\n<a href="">CONTACT US</a> &nbsp; &nbsp; &nbsp;\n<a href="">(C) 2011 BLANK-MAGAZINE.COM</a>LangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,2,62Template,15,AuthorList.aspxRegion,6,FooterOrderNo,1,2Name,10,StaticHtmlCSS,150,#StaticHtml_62 {\n	border-top:1px solid black;\n	text-align:center;\n	font-weight:bold;\n	padding:4px;\n	margin-top:10px;\n	clear:both;\n	margin-top:30px;\n}\nTopHtml,0,BottomHtml,0,ParentModuleId,2,61CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 61, NULL, 'Default', NULL),
(63, 'AuthorList.aspx', 'Content', 1, 'ContentListByTag', '#ContentListByTag_63 .extraItemDiv {\n	border:1px solid #E5E5E5;\n	margin-top:10px;\n	background:#E1E3E2;\n	padding: 24px;\n\n	-moz-box-shadow: 3px 3px 4px #E5E5E5;\n	-webkit-box-shadow: 3px 3px 4px #E5E5E5;\n	box-shadow: 3px 3px 4px #E5E5E5;\n	/* For IE 8 */\n	-ms-filter: "progid:DXImageTransform.Microsoft.Shadow(Strength=4, Direction=135, Color=''#E5E5E5'')";\n	/* For IE 5.5 - 7 */\n	filter: progid:DXImageTransform.Microsoft.Shadow(Strength=4, Direction=135, Color=''#E5E5E5'');\n}\n#ContentListByTag_63 img.pic {\n}\n#ContentListByTag_63 div.clItem {\n	padding:24px;\n	background:white;\n}\n#ContentListByTag_63 div.clTitle {\n	font-weight:bold;\n	font-size:18px;\n	border-bottom:1px solid #CFCFCF;\n	padding-bottom:1px;\n}\n#ContentListByTag_63 div.clPubDate {\n	margin-bottom:24px;\n	font-size:12px;\n	text-align:right;\n}\n#ContentListByTag_63 div.clMetin {\n	background:white;	\n}', 'Cinar.CMS.Serialization\nFilter,16,AuthorId=@AuthorHowManyItems,1,2SkipFirst,1,0Random,1,0OrderBy,19,Content.PublishDateAscending,5,FalseShowPictureLeftRight,5,FalseShowCurrentContent,4,TrueShowFirstItemWithPicture,5,FalseWhichPicture,15,Content.PicturePictureWidth,3,636PictureHeight,1,0BulletIcon,0,TitleLength,2,50DescriptionLength,3,150DateFormat,12,dd MMMM yyyyFieldOrder,16,title,date,imageCreateLink,4,TrueLinkTarget,0,ShowDescriptionAsLink,5,FalseUseTemplate,0,ForceToUseTemplate,5,FalseMoreLink,0,DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,4,TrueId,2,63Template,15,AuthorList.aspxRegion,7,ContentOrderNo,1,1Name,16,ContentListByTagCSS,854,#ContentListByTag_63 .extraItemDiv {\n	border:1px solid #E5E5E5;\n	margin-top:10px;\n	background:#E1E3E2;\n	padding: 24px;\n\n	-moz-box-shadow: 3px 3px 4px #E5E5E5;\n	-webkit-box-shadow: 3px 3px 4px #E5E5E5;\n	box-shadow: 3px 3px 4px #E5E5E5;\n	/* For IE 8 */\n	-ms-filter: "progid:DXImageTransform.Microsoft.Shadow(Strength=4, Direction=135, Color=''#E5E5E5'')";\n	/* For IE 5.5 - 7 */\n	filter: progid:DXImageTransform.Microsoft.Shadow(Strength=4, Direction=135, Color=''#E5E5E5'');\n}\n#ContentListByTag_63 img.pic {\n}\n#ContentListByTag_63 div.clItem {\n	padding:24px;\n	background:white;\n}\n#ContentListByTag_63 div.clTitle {\n	font-weight:bold;\n	font-size:18px;\n	border-bottom:1px solid #CFCFCF;\n	padding-bottom:1px;\n}\n#ContentListByTag_63 div.clPubDate {\n	margin-bottom:24px;\n	font-size:12px;\n	text-align:right;\n}\n#ContentListByTag_63 div.clMetin {\n	background:white;	\n}TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0DivClassName,12,extraItemDivCropPicture,5,False', NULL, NULL, 0, NULL, 'Default', NULL),
(64, 'AuthorList.aspx', 'ContentLeft', 0, 'RegionRepeater', '', 'Cinar.CMS.Serialization\nId,2,64Template,15,AuthorList.aspxRegion,11,ContentLeftOrderNo,1,0Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0RegionToCopy,11,ContentLeftPage,11,Author.aspx', NULL, NULL, 0, NULL, 'Default', NULL),
(81, 'Other.aspx', 'Content', 0, 'ContentDisplay', '#ContentDisplay_81 {\n	margin:120px 250px;\n}\n', 'Cinar.CMS.Serialization\nFieldOrder,4,textDateFormat,12,dd MMMM yyyyFilter,0,TagTemplate,0,Id,2,81Template,10,Other.aspxRegion,7,ContentOrderNo,1,0Name,14,ContentDisplayCSS,44,#ContentDisplay_81 {\n	margin:120px 250px;\n}\nTopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(82, 'Default.aspx', 'Header', 1, 'SearchForm', '#SearchForm_82 {\n	position:absolute;\n	top:165px;\n}\n#SearchForm_82 input.searchText {\n	border:none;\n	width:114px;\n}\n\n', 'Cinar.CMS.Serialization\nResultsPage,11,Search.aspxId,2,82Template,12,Default.aspxRegion,6,HeaderOrderNo,1,1Name,10,SearchFormCSS,116,#SearchForm_82 {\n	position:absolute;\n	top:165px;\n}\n#SearchForm_82 input.searchText {\n	border:none;\n	width:114px;\n}\n\nTopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(83, 'Other.aspx', 'Header', 0, 'RegionRepeater', '', 'Cinar.CMS.Serialization\nPage,12,Default.aspxRegionToCopy,6,HeaderId,2,83Template,10,Other.aspxRegion,6,HeaderOrderNo,1,0Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(84, 'Other.aspx', 'Footer', 0, 'RegionRepeater', '', 'Cinar.CMS.Serialization\nPage,12,Default.aspxRegionToCopy,6,FooterId,2,84Template,10,Other.aspxRegion,6,FooterOrderNo,1,0Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(86, 'Search.aspx', 'Header', 0, 'RegionRepeater', '', 'Cinar.CMS.Serialization\nPage,12,Default.aspxRegionToCopy,6,HeaderId,2,86Template,11,Search.aspxRegion,6,HeaderOrderNo,1,0Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(87, 'Search.aspx', 'Header', 1, 'StaticHtml', '#StaticHtml_87 {\n}\n\n#StaticHtml_87 .headerMenu { border-bottom:1px solid black}\n#StaticHtml_87 .tab {\n	border:1px solid black; \n	border-bottom:none;\n	float:right; \n	padding:2px 40px;\n	cursor:pointer;\n	margin-left:10px;\n}\n#StaticHtml_87 .search {\n}\n#StaticHtml_87 .subscribe {\n	background:black;\n	color:white;\n}', 'Cinar.CMS.Serialization\nInnerHtml,244,<a href="/Default.aspx"><img src="/UserFiles/Images/design/logo.jpg" border="0"/></a>\n\n<div class="headerMenu">\n<div class="subscribe tab">Subscribe</div>\n<div class="search tab" id="searchTab">Search</div>\n<div style="clear:both"></div>\n</div>LangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,2,87Template,11,Search.aspxRegion,6,HeaderOrderNo,1,1Name,10,StaticHtmlCSS,310,#StaticHtml_87 {\n}\n\n#StaticHtml_87 .headerMenu { border-bottom:1px solid black}\n#StaticHtml_87 .tab {\n	border:1px solid black; \n	border-bottom:none;\n	float:right; \n	padding:2px 40px;\n	cursor:pointer;\n	margin-left:10px;\n}\n#StaticHtml_87 .search {\n}\n#StaticHtml_87 .subscribe {\n	background:black;\n	color:white;\n}TopHtml,0,BottomHtml,0,ParentModuleId,2,86CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 86, NULL, 'Default', NULL),
(88, 'Search.aspx', 'Header', 2, 'SearchForm', '#SearchForm_88 {\n	position:absolute;\n	right:147px;\n	top:156px;\n}\n#SearchForm_88 input.searchText {\n	border:none;\n	width:114px;\n}\n\n', 'Cinar.CMS.Serialization\nResultsPage,0,Id,2,88Template,11,Search.aspxRegion,6,HeaderOrderNo,1,2Name,10,SearchFormCSS,130,#SearchForm_88 {\n	position:absolute;\n	right:147px;\n	top:156px;\n}\n#SearchForm_88 input.searchText {\n	border:none;\n	width:114px;\n}\n\nTopHtml,273,<script>\nElement.observe(document, ''dom:loaded'', function(){\n	var sf = $(''SearchForm_82'');\n	sf.hide();\n	$(''searchTab'').on(''click'', function(){\n		sf.show();\n		sf.down(''.searchText'').focus();\n	});\n	sf.down(''.searchText'').on(''blur'', function(){\n		sf.hide();\n	});\n});\n</script>BottomHtml,0,ParentModuleId,2,86CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 86, NULL, 'Default', NULL),
(89, 'Search.aspx', 'Footer', 0, 'RegionRepeater', '', 'Cinar.CMS.Serialization\nPage,12,Default.aspxRegionToCopy,6,FooterId,2,89Template,11,Search.aspxRegion,6,FooterOrderNo,1,0Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(90, 'Search.aspx', 'Footer', 1, 'StaticHtml', '#StaticHtml_90 {\n	border-top:1px solid black;\n	text-align:center;\n	font-weight:bold;\n	padding:4px;\n	margin-top:10px;\n	clear:both;\n	margin-top:30px;\n}\n', 'Cinar.CMS.Serialization\nInnerHtml,173,<a href="/Other.aspx?item=33">ABOUT US</a> &nbsp; &nbsp; &nbsp;\n<a href="/Other.aspx?item=34">CONTACT US</a> &nbsp; &nbsp; &nbsp;\n<a href="/">(C) 2011 BLANK-MAGAZINE.COM</a>LangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,2,90Template,11,Search.aspxRegion,6,FooterOrderNo,1,1Name,10,StaticHtmlCSS,150,#StaticHtml_90 {\n	border-top:1px solid black;\n	text-align:center;\n	font-weight:bold;\n	padding:4px;\n	margin-top:10px;\n	clear:both;\n	margin-top:30px;\n}\nTopHtml,0,BottomHtml,0,ParentModuleId,2,89CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 89, NULL, 'Default', NULL),
(91, 'Author.aspx', 'Header', 0, 'RegionRepeater', '', 'Cinar.CMS.Serialization\nId,2,91Template,11,Author.aspxRegion,6,HeaderOrderNo,1,0Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0RegionToCopy,6,HeaderPage,12,Default.aspx', NULL, NULL, 0, NULL, 'Default', NULL),
(92, 'Search.aspx', 'Content', 0, 'SearchResults', '#SearchResults_92 {\n}\n#SearchResults_92 div.paging {\n	background:#0C51B1;\n	font-weight:bold;\n	padding:4px;\n	text-align:center}\n#SearchResults_92 div.paging a {\n	color:white}\n#SearchResults_92 div.resultItem {\n}\n#SearchResults_92 div.title {\n	font-weight:bold}\n#SearchResults_92 div.desc {\n}\n', 'Cinar.CMS.Serialization\nEntityName,7,ContentFilter,0,HowManyItems,2,30OrderBy,2,IdAscending,5,FalseDataTemplate,166,<div class="resultItem"><div class="title"><a href="$=entity.PageLink$?item=$=entity.Id$">$=entity.Title$</a></div><div class="desc">$=entity.Description$</div></div>PictureWidth,1,0PictureHeight,1,0DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,2,92Template,11,Search.aspxRegion,7,ContentOrderNo,1,0Name,13,SearchResultsCSS,291,#SearchResults_92 {\n}\n#SearchResults_92 div.paging {\n	background:#0C51B1;\n	font-weight:bold;\n	padding:4px;\n	text-align:center}\n#SearchResults_92 div.paging a {\n	color:white}\n#SearchResults_92 div.resultItem {\n}\n#SearchResults_92 div.title {\n	font-weight:bold}\n#SearchResults_92 div.desc {\n}\nTopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(101, 'City.aspx', 'Header', 0, 'RegionRepeater', '', 'Cinar.CMS.Serialization\nPage,12,Default.aspxRegionToCopy,6,HeaderId,3,101Template,9,City.aspxRegion,6,HeaderOrderNo,1,0Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(102, 'City.aspx', 'Header', 1, 'StaticHtml', '#StaticHtml_102 {\n}\n\n#StaticHtml_102 .headerMenu { border-bottom:1px solid black}\n#StaticHtml_102 .tab {\n	border:1px solid black; \n	border-bottom:none;\n	float:right; \n	padding:2px 40px;\n	cursor:pointer;\n	margin-left:10px;\n}\n#StaticHtml_102 .search {\n}\n#StaticHtml_102 .subscribe {\n	background:black;\n	color:white;\n}', 'Cinar.CMS.Serialization\nInnerHtml,244,<a href="/Default.aspx"><img src="/UserFiles/Images/design/logo.jpg" border="0"/></a>\n\n<div class="headerMenu">\n<div class="subscribe tab">Subscribe</div>\n<div class="search tab" id="searchTab">Search</div>\n<div style="clear:both"></div>\n</div>LangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,3,102Template,9,City.aspxRegion,6,HeaderOrderNo,1,1Name,10,StaticHtmlCSS,315,#StaticHtml_102 {\n}\n\n#StaticHtml_102 .headerMenu { border-bottom:1px solid black}\n#StaticHtml_102 .tab {\n	border:1px solid black; \n	border-bottom:none;\n	float:right; \n	padding:2px 40px;\n	cursor:pointer;\n	margin-left:10px;\n}\n#StaticHtml_102 .search {\n}\n#StaticHtml_102 .subscribe {\n	background:black;\n	color:white;\n}TopHtml,0,BottomHtml,0,ParentModuleId,3,101CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 101, NULL, 'Default', NULL),
(103, 'City.aspx', 'Header', 2, 'SearchForm', '#SearchForm_103 {\n	position:absolute;\n	right:147px;\n	top:156px;\n}\n#SearchForm_103 input.searchText {\n	border:none;\n	width:114px;\n}\n\n', 'Cinar.CMS.Serialization\nResultsPage,11,Search.aspxId,3,103Template,9,City.aspxRegion,6,HeaderOrderNo,1,2Name,10,SearchFormCSS,132,#SearchForm_103 {\n	position:absolute;\n	right:147px;\n	top:156px;\n}\n#SearchForm_103 input.searchText {\n	border:none;\n	width:114px;\n}\n\nTopHtml,273,<script>\nElement.observe(document, ''dom:loaded'', function(){\n	var sf = $(''SearchForm_82'');\n	sf.hide();\n	$(''searchTab'').on(''click'', function(){\n		sf.show();\n		sf.down(''.searchText'').focus();\n	});\n	sf.down(''.searchText'').on(''blur'', function(){\n		sf.hide();\n	});\n});\n</script>BottomHtml,0,ParentModuleId,3,101CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 101, NULL, 'Default', NULL),
(104, 'City.aspx', 'Footer', 0, 'RegionRepeater', '', 'Cinar.CMS.Serialization\nPage,12,Default.aspxRegionToCopy,6,FooterId,3,104Template,9,City.aspxRegion,6,FooterOrderNo,1,0Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(105, 'City.aspx', 'Footer', 1, 'StaticHtml', '#StaticHtml_105 {\n	border-top:1px solid black;\n	text-align:center;\n	font-weight:bold;\n	padding:4px;\n	margin-top:10px;\n	clear:both;\n	margin-top:30px;\n}\n', 'Cinar.CMS.Serialization\nInnerHtml,173,<a href="/Other.aspx?item=33">ABOUT US</a> &nbsp; &nbsp; &nbsp;\n<a href="/Other.aspx?item=34">CONTACT US</a> &nbsp; &nbsp; &nbsp;\n<a href="/">(C) 2011 BLANK-MAGAZINE.COM</a>LangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,3,105Template,9,City.aspxRegion,6,FooterOrderNo,1,1Name,10,StaticHtmlCSS,151,#StaticHtml_105 {\n	border-top:1px solid black;\n	text-align:center;\n	font-weight:bold;\n	padding:4px;\n	margin-top:10px;\n	clear:both;\n	margin-top:30px;\n}\nTopHtml,0,BottomHtml,0,ParentModuleId,3,104CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 104, NULL, 'Default', NULL),
(107, 'City.aspx', 'ContentLeft', 0, 'ContentListByFilter', '', 'Cinar.CMS.Serialization\nFilter,12,CategoryId=3HowManyItems,1,2SkipFirst,1,0Random,1,0OrderBy,19,Content.PublishDateAscending,5,FalseShowPictureLeftRight,5,FalseShowCurrentContent,5,FalseShowFirstItemWithPicture,5,FalseWhichPicture,15,Content.PicturePictureWidth,3,214PictureHeight,1,0BulletIcon,0,TitleLength,2,50DescriptionLength,3,150DateFormat,12,dd MMMM yyyyFieldOrder,28,image,title,description,dateCreateLink,4,TrueLinkTarget,0,ShowDescriptionAsLink,5,FalseUseTemplate,0,ForceToUseTemplate,5,FalseMoreLink,0,DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,3,107Template,9,City.aspxRegion,11,ContentLeftOrderNo,1,0Name,19,ContentListByFilterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,17,vitrin toogleDescUseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(109, 'City.aspx', 'Content', 0, 'ContentListByFilter', '', 'Cinar.CMS.Serialization\nFilter,12,CategoryId=3HowManyItems,1,4SkipFirst,1,2Random,1,0OrderBy,19,Content.PublishDateAscending,5,FalseShowPictureLeftRight,5,FalseShowCurrentContent,5,FalseShowFirstItemWithPicture,5,FalseWhichPicture,15,Content.PicturePictureWidth,3,214PictureHeight,1,0BulletIcon,0,TitleLength,2,50DescriptionLength,3,150DateFormat,12,dd MMMM yyyyFieldOrder,28,image,title,description,dateCreateLink,4,TrueLinkTarget,0,ShowDescriptionAsLink,5,FalseUseTemplate,0,ForceToUseTemplate,5,FalseMoreLink,0,DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,3,109Template,9,City.aspxRegion,7,ContentOrderNo,1,0Name,19,ContentListByFilterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,17,vitrin toogleDescUseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(110, 'City.aspx', 'Content2', 0, 'ContentListByFilter', '', 'Cinar.CMS.Serialization\nFilter,12,CategoryId=3HowManyItems,1,6SkipFirst,1,4Random,1,0OrderBy,19,Content.PublishDateAscending,5,FalseShowPictureLeftRight,5,FalseShowCurrentContent,5,FalseShowFirstItemWithPicture,5,FalseWhichPicture,15,Content.PicturePictureWidth,3,214PictureHeight,1,0BulletIcon,0,TitleLength,2,50DescriptionLength,3,150DateFormat,12,dd MMMM yyyyFieldOrder,28,image,title,description,dateCreateLink,4,TrueLinkTarget,0,ShowDescriptionAsLink,5,FalseUseTemplate,0,ForceToUseTemplate,5,FalseMoreLink,0,DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,3,110Template,9,City.aspxRegion,8,Content2OrderNo,1,0Name,19,ContentListByFilterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,17,vitrin toogleDescUseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(111, 'City.aspx', 'ContentRight', 0, 'ContentListByFilter', '', 'Cinar.CMS.Serialization\nFilter,12,CategoryId=3HowManyItems,1,8SkipFirst,1,6Random,1,0OrderBy,19,Content.PublishDateAscending,5,FalseShowPictureLeftRight,5,FalseShowCurrentContent,5,FalseShowFirstItemWithPicture,5,FalseWhichPicture,15,Content.PicturePictureWidth,3,214PictureHeight,1,0BulletIcon,0,TitleLength,2,50DescriptionLength,3,150DateFormat,12,dd MMMM yyyyFieldOrder,28,image,title,description,dateCreateLink,4,TrueLinkTarget,0,ShowDescriptionAsLink,5,FalseUseTemplate,0,ForceToUseTemplate,5,FalseMoreLink,0,DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,3,111Template,9,City.aspxRegion,12,ContentRightOrderNo,1,0Name,19,ContentListByFilterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,17,vitrin toogleDescUseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(112, 'Category.aspx', 'Header', 0, 'RegionRepeater', '', 'Cinar.CMS.Serialization\nId,3,112Template,13,Category.aspxRegion,6,HeaderOrderNo,1,0Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0RegionToCopy,6,HeaderPage,12,Default.aspx', NULL, NULL, 0, NULL, 'Default', NULL),
(113, 'Category.aspx', 'Header', 1, 'StaticHtml', '#StaticHtml_113 {\n}\n\n#StaticHtml_113 .headerMenu { border-bottom:1px solid black}\n#StaticHtml_113 .tab {\n	border:1px solid black; \n	border-bottom:none;\n	float:right; \n	padding:2px 40px;\n	cursor:pointer;\n	margin-left:10px;\n}\n#StaticHtml_113 .search {\n}\n#StaticHtml_113 .subscribe {\n	background:black;\n	color:white;\n}', 'Cinar.CMS.Serialization\nInnerHtml,244,<a href="/Default.aspx"><img src="/UserFiles/Images/design/logo.jpg" border="0"/></a>\n\n<div class="headerMenu">\n<div class="subscribe tab">Subscribe</div>\n<div class="search tab" id="searchTab">Search</div>\n<div style="clear:both"></div>\n</div>LangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,3,113Template,13,Category.aspxRegion,6,HeaderOrderNo,1,1Name,10,StaticHtmlCSS,315,#StaticHtml_113 {\n}\n\n#StaticHtml_113 .headerMenu { border-bottom:1px solid black}\n#StaticHtml_113 .tab {\n	border:1px solid black; \n	border-bottom:none;\n	float:right; \n	padding:2px 40px;\n	cursor:pointer;\n	margin-left:10px;\n}\n#StaticHtml_113 .search {\n}\n#StaticHtml_113 .subscribe {\n	background:black;\n	color:white;\n}TopHtml,0,BottomHtml,0,ParentModuleId,3,112CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 112, NULL, 'Default', NULL),
(114, 'Category.aspx', 'Header', 2, 'SearchForm', '#SearchForm_114 {\n	position:absolute;\n	right:147px;\n	top:156px;\n}\n#SearchForm_114 input.searchText {\n	border:none;\n	width:114px;\n}\n\n', 'Cinar.CMS.Serialization\nResultsPage,11,Search.aspxId,3,114Template,13,Category.aspxRegion,6,HeaderOrderNo,1,2Name,10,SearchFormCSS,132,#SearchForm_114 {\n	position:absolute;\n	right:147px;\n	top:156px;\n}\n#SearchForm_114 input.searchText {\n	border:none;\n	width:114px;\n}\n\nTopHtml,273,<script>\nElement.observe(document, ''dom:loaded'', function(){\n	var sf = $(''SearchForm_82'');\n	sf.hide();\n	$(''searchTab'').on(''click'', function(){\n		sf.show();\n		sf.down(''.searchText'').focus();\n	});\n	sf.down(''.searchText'').on(''blur'', function(){\n		sf.hide();\n	});\n});\n</script>BottomHtml,0,ParentModuleId,3,112CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 112, NULL, 'Default', NULL),
(115, 'Category.aspx', 'Footer', 0, 'ContentListByTag', '#ContentListByTag_115 div.title {\n	border-bottom:1px solid black;\n	padding:50px 0px 5px 65px;\n	margin-bottom:30px;\n}\n\n#ContentListByTag_115 div.paging {\n	float:left;\n	width:60px;\n	height:120px;\n	background-repeat:no-repeat;\n}\n\n#ContentListByTag_115 .cerceve {\n	float:left;\n	width:855px;\n	border: 1px solid #efefef;\n\n}\n\n#ContentListByTag_115 div.clItem {\n	float:left;\n	margin:0px 5px 5px 0px;\n}\n', 'Cinar.CMS.Serialization\nFilter,16,AuthorId=@AuthorHowManyItems,2,10SkipFirst,1,0Random,1,0OrderBy,19,Content.PublishDateAscending,5,FalseShowPictureLeftRight,5,FalseShowCurrentContent,4,TrueShowFirstItemWithPicture,5,FalseWhichPicture,15,Content.PicturePictureWidth,1,0PictureHeight,3,120BulletIcon,0,TitleLength,2,50DescriptionLength,3,150DateFormat,12,dd MMMM yyyyFieldOrder,5,imageCreateLink,4,TrueLinkTarget,0,ShowDescriptionAsLink,5,FalseUseTemplate,0,ForceToUseTemplate,5,FalseMoreLink,0,DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,3,115Template,13,Category.aspxRegion,6,FooterOrderNo,1,0Name,16,ContentListByTagCSS,394,#ContentListByTag_115 div.title {\n	border-bottom:1px solid black;\n	padding:50px 0px 5px 65px;\n	margin-bottom:30px;\n}\n\n#ContentListByTag_115 div.paging {\n	float:left;\n	width:60px;\n	height:120px;\n	background-repeat:no-repeat;\n}\n\n#ContentListByTag_115 .cerceve {\n	float:left;\n	width:855px;\n	border: 1px solid #efefef;\n\n}\n\n#ContentListByTag_115 div.clItem {\n	float:left;\n	margin:0px 5px 5px 0px;\n}\nTopHtml,182,<div class="title">ARCHIVE</div>\n\n<div class="paging" style="background-image:url(/UserFiles/Images/design/prev.png); background-position:right center;"></div>\n\n<div class="cerceve">BottomHtml,163,</div>\n<div class="paging"  style="background-image:url(/UserFiles/Images/design/next.png); background-position:left center;"></div>\n<div style="clear:both"></div>ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(116, 'Category.aspx', 'ContentLeft', 0, 'RegionRepeater', '', 'Cinar.CMS.Serialization\nId,3,116Template,13,Category.aspxRegion,11,ContentLeftOrderNo,1,0Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0RegionToCopy,11,ContentLeftPage,11,Author.aspx', NULL, NULL, 0, NULL, 'Default', NULL),
(117, 'Category.aspx', 'ContentLeft', 1, 'ContentListByFilter', '#ContentListByFilter_117 {\n	margin-top:30px;\n}\n', 'Cinar.CMS.Serialization\nFilter,16,AuthorId=@AuthorHowManyItems,1,1SkipFirst,1,0Random,1,0OrderBy,19,Content.PublishDateAscending,5,FalseShowPictureLeftRight,5,FalseShowCurrentContent,4,TrueShowFirstItemWithPicture,5,FalseWhichPicture,17,TAuthorId.PicturePictureWidth,3,180PictureHeight,1,0BulletIcon,0,TitleLength,2,50DescriptionLength,3,150DateFormat,12,dd MMMM yyyyFieldOrder,12,image,authorCreateLink,4,TrueLinkTarget,0,ShowDescriptionAsLink,5,FalseUseTemplate,0,ForceToUseTemplate,5,FalseMoreLink,0,DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,3,117Template,13,Category.aspxRegion,11,ContentLeftOrderNo,1,1Name,19,ContentListByFilterCSS,47,#ContentListByFilter_117 {\n	margin-top:30px;\n}\nTopHtml,0,BottomHtml,0,ParentModuleId,3,116CSSClass,22,vitrin solda rotate-10UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 116, NULL, 'Default', NULL),
(118, 'Category.aspx', 'ContentLeft', 2, 'SQLDataList', '#SQLDataList_118 {\n	margin-top:60px;\n	width:230px;\n}\n#SQLDataList_118 .tag {\n	border-bottom: 1px solid #8A8A8A;\n	padding:10px 0px 4px 4px;\n	text-transform:uppercase;\n}', 'Cinar.CMS.Serialization\nSQL,171,SELECT\n	t.Id,\n	t.Name\nFROM\n	Tag t\n	INNER JOIN ContentTag ct ON ct.TagId = t.Id\n	INNER JOIN Content c ON c.Id = ct.ContentId\nWHERE\n	c.AuthorId = $=Context.Content.AuthorId$DataTemplate,110,<div class="tag">\n<a href="/AuthorList.aspx?item=$=Context.Content.Id$&tagId=$=row.Id$">$=row.Name$</a>\n</div>PictureWidth,1,0PictureHeight,1,0DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,3,118Template,13,Category.aspxRegion,11,ContentLeftOrderNo,1,2Name,11,SQLDataListCSS,167,#SQLDataList_118 {\n	margin-top:60px;\n	width:230px;\n}\n#SQLDataList_118 .tag {\n	border-bottom: 1px solid #8A8A8A;\n	padding:10px 0px 4px 4px;\n	text-transform:uppercase;\n}TopHtml,0,BottomHtml,0,ParentModuleId,3,116CSSClass,5,soldaUseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 116, NULL, 'Default', NULL),
(119, 'Category.aspx', 'Footer', 1, 'RegionRepeater', '', 'Cinar.CMS.Serialization\nId,3,119Template,13,Category.aspxRegion,6,FooterOrderNo,1,1Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0RegionToCopy,6,FooterPage,12,Default.aspx', NULL, NULL, 0, NULL, 'Default', NULL),
(120, 'Category.aspx', 'Footer', 2, 'StaticHtml', '#StaticHtml_120 {\n	border-top:1px solid black;\n	text-align:center;\n	font-weight:bold;\n	padding:4px;\n	margin-top:10px;\n	clear:both;\n	margin-top:30px;\n}\n', 'Cinar.CMS.Serialization\nInnerHtml,173,<a href="/Other.aspx?item=33">ABOUT US</a> &nbsp; &nbsp; &nbsp;\n<a href="/Other.aspx?item=34">CONTACT US</a> &nbsp; &nbsp; &nbsp;\n<a href="/">(C) 2011 BLANK-MAGAZINE.COM</a>LangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,3,120Template,13,Category.aspxRegion,6,FooterOrderNo,1,2Name,10,StaticHtmlCSS,151,#StaticHtml_120 {\n	border-top:1px solid black;\n	text-align:center;\n	font-weight:bold;\n	padding:4px;\n	margin-top:10px;\n	clear:both;\n	margin-top:30px;\n}\nTopHtml,0,BottomHtml,0,ParentModuleId,3,119CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 119, NULL, 'Default', NULL),
(121, 'Category.aspx', 'Content', 0, 'ContentListByTag', '', 'Cinar.CMS.Serialization\nFilter,0,HowManyItems,1,2SkipFirst,1,0Random,1,0OrderBy,19,Content.PublishDateAscending,5,FalseShowPictureLeftRight,5,FalseShowCurrentContent,4,TrueShowFirstItemWithPicture,5,FalseWhichPicture,15,Content.PicturePictureWidth,3,636PictureHeight,1,0BulletIcon,0,TitleLength,2,50DescriptionLength,3,150DateFormat,12,dd MMMM yyyyFieldOrder,16,title,date,imageCreateLink,4,TrueLinkTarget,0,ShowDescriptionAsLink,5,FalseUseTemplate,0,ForceToUseTemplate,5,FalseMoreLink,0,DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,4,TrueId,3,121Template,13,Category.aspxRegion,7,ContentOrderNo,1,0Name,16,ContentListByTagCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,17,clsContentDisplayUseCache,7,DefaultCacheLifeTime,1,0CropPicture,5,FalseDivClassName,8,innerDiv', NULL, NULL, 0, NULL, 'Default', NULL),
(122, 'Content.aspx', 'ContentLeft', 0, 'ContentListByFilter', '#ContentListByFilter_122 {\n	margin-top:30px;\n}\n', 'Cinar.CMS.Serialization\nFilter,18,Category=@CategoryHowManyItems,1,1SkipFirst,1,0Random,1,0OrderBy,19,Content.PublishDateAscending,5,FalseShowPictureLeftRight,5,FalseShowCurrentContent,4,TrueShowFirstItemWithPicture,5,FalseWhichPicture,15,Content.PicturePictureWidth,3,180PictureHeight,1,0BulletIcon,0,TitleLength,2,50DescriptionLength,3,150DateFormat,12,dd MMMM yyyyFieldOrder,14,image,categoryCreateLink,4,TrueLinkTarget,0,ShowDescriptionAsLink,5,FalseUseTemplate,0,ForceToUseTemplate,5,FalseMoreLink,0,DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,3,122Template,12,Content.aspxRegion,11,ContentLeftOrderNo,1,0Name,19,ContentListByFilterCSS,47,#ContentListByFilter_122 {\n	margin-top:30px;\n}\nTopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,22,vitrin solda rotate-10UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL);
INSERT INTO `module` (`Id`, `Template`, `Region`, `OrderNo`, `Name`, `CSS`, `Details`, `TopHtml`, `BottomHtml`, `ParentModuleId`, `CSSClass`, `UseCache`, `CacheLifeTime`) VALUES
(123, 'Content.aspx', 'Content', 0, 'ContentDisplay', '\n', 'Cinar.CMS.Serialization\nFieldOrder,22,title,date,region,textDateFormat,12,dd MMMM yyyyFilter,11,Id=@ContentTagTemplate,0,Id,3,123Template,12,Content.aspxRegion,7,ContentOrderNo,1,0Name,14,ContentDisplayCSS,1,\nTopHtml,22,<div class="innerDiv">BottomHtml,6,</div>ParentModuleId,1,0CSSClass,17,clsContentDisplayUseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(124, 'Content.aspx', 'conRegion123', 0, 'ContentGallery', '', 'Cinar.CMS.Serialization\nPictureWidth,3,636PictureHeight,1,0ShowTitle,4,TrueShowTitleFirst,5,FalseDisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,3,124Template,12,Content.aspxRegion,12,conRegion123OrderNo,1,0Name,14,ContentGalleryCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,3,123CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 123, NULL, 'Default', NULL),
(125, 'Content.aspx', 'Header', 0, 'RegionRepeater', '', 'Cinar.CMS.Serialization\nId,3,125Template,12,Content.aspxRegion,6,HeaderOrderNo,1,0Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0RegionToCopy,6,HeaderPage,12,Default.aspx', NULL, NULL, 0, NULL, 'Default', NULL),
(126, 'Content.aspx', 'Header', 1, 'StaticHtml', '#StaticHtml_126 {\n}\n\n#StaticHtml_126 .headerMenu { border-bottom:1px solid black}\n#StaticHtml_126 .tab {\n	border:1px solid black; \n	border-bottom:none;\n	float:right; \n	padding:2px 40px;\n	cursor:pointer;\n	margin-left:10px;\n}\n#StaticHtml_126 .search {\n}\n#StaticHtml_126 .subscribe {\n	background:black;\n	color:white;\n}', 'Cinar.CMS.Serialization\nInnerHtml,244,<a href="/Default.aspx"><img src="/UserFiles/Images/design/logo.jpg" border="0"/></a>\n\n<div class="headerMenu">\n<div class="subscribe tab">Subscribe</div>\n<div class="search tab" id="searchTab">Search</div>\n<div style="clear:both"></div>\n</div>LangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,3,126Template,12,Content.aspxRegion,6,HeaderOrderNo,1,1Name,10,StaticHtmlCSS,315,#StaticHtml_126 {\n}\n\n#StaticHtml_126 .headerMenu { border-bottom:1px solid black}\n#StaticHtml_126 .tab {\n	border:1px solid black; \n	border-bottom:none;\n	float:right; \n	padding:2px 40px;\n	cursor:pointer;\n	margin-left:10px;\n}\n#StaticHtml_126 .search {\n}\n#StaticHtml_126 .subscribe {\n	background:black;\n	color:white;\n}TopHtml,0,BottomHtml,0,ParentModuleId,3,125CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 125, NULL, 'Default', NULL),
(127, 'Content.aspx', 'Header', 2, 'SearchForm', '#SearchForm_127 {\n	position:absolute;\n	right:147px;\n	top:156px;\n}\n#SearchForm_127 input.searchText {\n	border:none;\n	width:114px;\n}\n\n', 'Cinar.CMS.Serialization\nResultsPage,11,Search.aspxId,3,127Template,12,Content.aspxRegion,6,HeaderOrderNo,1,2Name,10,SearchFormCSS,132,#SearchForm_127 {\n	position:absolute;\n	right:147px;\n	top:156px;\n}\n#SearchForm_127 input.searchText {\n	border:none;\n	width:114px;\n}\n\nTopHtml,273,<script>\nElement.observe(document, ''dom:loaded'', function(){\n	var sf = $(''SearchForm_82'');\n	sf.hide();\n	$(''searchTab'').on(''click'', function(){\n		sf.show();\n		sf.down(''.searchText'').focus();\n	});\n	sf.down(''.searchText'').on(''blur'', function(){\n		sf.hide();\n	});\n});\n</script>BottomHtml,0,ParentModuleId,3,125CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 125, NULL, 'Default', NULL),
(128, 'Content.aspx', 'ContentLeft', 1, 'SQLDataList', '', 'Cinar.CMS.Serialization\nSQL,175,SELECT\n	t.Id,\n	t.Name\nFROM\n	Tag t\n	INNER JOIN ContentTag ct ON ct.TagId = t.Id\n	INNER JOIN Content c ON c.Id = ct.ContentId\nWHERE\n	c.CategoryId = $=Context.Content.CategoryId$DataTemplate,110,<div class="tag">\n<a href="/AuthorList.aspx?item=$=Context.Content.Id$&tagId=$=row.Id$">$=row.Name$</a>\n</div>PictureWidth,1,0PictureHeight,1,0DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,3,128Template,12,Content.aspxRegion,11,ContentLeftOrderNo,1,1Name,11,SQLDataListCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,10,solda tagsUseCache,7,DefaultCacheLifeTime,1,0CropPicture,5,FalseDivClassName,12,extraItemDiv', NULL, NULL, 0, NULL, 'Default', NULL),
(129, 'Content.aspx', 'Footer', 0, 'ContentListByTag', '#ContentListByTag_129 div.title {\n	border-bottom:1px solid black;\n	padding:50px 0px 5px 65px;\n	margin-bottom:30px;\n	font-family:spinnaker;\n}\n\n#ContentListByTag_129 div.paging {\n	float:left;\n	width:60px;\n	height:120px;\n	background-repeat:no-repeat;\n}\n\n#ContentListByTag_129 .cerceve {\n	float:left;\n	width:855px;\n	border: 1px solid #efefef;\n\n}\n\n#ContentListByTag_129 div.clItem {\n	float:left;\n	margin:0px 5px 5px 0px;\n}\n', 'Cinar.CMS.Serialization\nFilter,20,CategoryId=@CategoryHowManyItems,2,10SkipFirst,1,0Random,1,0OrderBy,19,Content.PublishDateAscending,5,FalseShowPictureLeftRight,5,FalseShowCurrentContent,4,TrueShowFirstItemWithPicture,5,FalseWhichPicture,15,Content.PicturePictureWidth,1,0PictureHeight,3,120BulletIcon,0,TitleLength,2,50DescriptionLength,3,150DateFormat,12,dd MMMM yyyyFieldOrder,5,imageCreateLink,4,TrueLinkTarget,0,ShowDescriptionAsLink,5,FalseUseTemplate,0,ForceToUseTemplate,5,FalseMoreLink,0,DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,3,129Template,12,Content.aspxRegion,6,FooterOrderNo,1,0Name,16,ContentListByTagCSS,418,#ContentListByTag_129 div.title {\n	border-bottom:1px solid black;\n	padding:50px 0px 5px 65px;\n	margin-bottom:30px;\n	font-family:spinnaker;\n}\n\n#ContentListByTag_129 div.paging {\n	float:left;\n	width:60px;\n	height:120px;\n	background-repeat:no-repeat;\n}\n\n#ContentListByTag_129 .cerceve {\n	float:left;\n	width:855px;\n	border: 1px solid #efefef;\n\n}\n\n#ContentListByTag_129 div.clItem {\n	float:left;\n	margin:0px 5px 5px 0px;\n}\nTopHtml,182,<div class="title">ARCHIVE</div>\n\n<div class="paging" style="background-image:url(/UserFiles/Images/design/prev.png); background-position:right center;"></div>\n\n<div class="cerceve">BottomHtml,163,</div>\n<div class="paging"  style="background-image:url(/UserFiles/Images/design/next.png); background-position:left center;"></div>\n<div style="clear:both"></div>ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0CropPicture,5,FalseDivClassName,12,extraItemDiv', NULL, NULL, 0, NULL, 'Default', NULL),
(130, 'Content.aspx', 'Footer', 1, 'RegionRepeater', '', 'Cinar.CMS.Serialization\nId,3,130Template,12,Content.aspxRegion,6,FooterOrderNo,1,1Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0RegionToCopy,6,FooterPage,12,Default.aspx', NULL, NULL, 0, NULL, 'Default', NULL),
(131, 'Content.aspx', 'Footer', 2, 'StaticHtml', '#StaticHtml_131 {\n	border-top:1px solid black;\n	text-align:center;\n	font-weight:bold;\n	padding:4px;\n	margin-top:10px;\n	clear:both;\n	margin-top:30px;\n}\n', 'Cinar.CMS.Serialization\nInnerHtml,173,<a href="/Other.aspx?item=33">ABOUT US</a> &nbsp; &nbsp; &nbsp;\n<a href="/Other.aspx?item=34">CONTACT US</a> &nbsp; &nbsp; &nbsp;\n<a href="/">(C) 2011 BLANK-MAGAZINE.COM</a>LangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,3,131Template,12,Content.aspxRegion,6,FooterOrderNo,1,2Name,10,StaticHtmlCSS,151,#StaticHtml_131 {\n	border-top:1px solid black;\n	text-align:center;\n	font-weight:bold;\n	padding:4px;\n	margin-top:10px;\n	clear:both;\n	margin-top:30px;\n}\nTopHtml,0,BottomHtml,0,ParentModuleId,3,130CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 130, NULL, 'Default', NULL),
(133, 'Default.aspx', 'OrtaLeft', 0, 'ContentListByFilter', '', 'Cinar.CMS.Serialization\nFilter,10,AuthorId=4HowManyItems,1,1SkipFirst,1,0Random,1,0OrderBy,19,Content.PublishDateAscending,5,FalseShowPictureLeftRight,5,FalseShowCurrentContent,5,FalseShowFirstItemWithPicture,5,FalseWhichPicture,17,TAuthorId.PicturePictureWidth,3,218PictureHeight,1,0BulletIcon,0,TitleLength,2,50DescriptionLength,3,150DateFormat,3,agoFieldOrder,29,image,author,description,dateCreateLink,4,TrueLinkTarget,0,ShowDescriptionAsLink,5,FalseUseTemplate,15,AuthorList.aspxForceToUseTemplate,4,TrueMoreLink,0,DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,3,133Template,12,Default.aspxRegion,8,OrtaLeftOrderNo,1,0Name,19,ContentListByFilterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,24,vitrin shadow toogleDescUseCache,7,DefaultCacheLifeTime,1,0CropPicture,5,FalseDivClassName,12,extraItemDiv', NULL, NULL, 0, NULL, 'Default', NULL),
(134, 'Default.aspx', 'OrtaRight', 0, 'ContentListByFilter', '', 'Cinar.CMS.Serialization\nFilter,12,CategoryId=7HowManyItems,1,1SkipFirst,1,0Random,1,0OrderBy,19,Content.PublishDateAscending,5,FalseShowPictureLeftRight,5,FalseShowCurrentContent,5,FalseShowFirstItemWithPicture,5,FalseWhichPicture,15,Content.PicturePictureWidth,3,218PictureHeight,1,0BulletIcon,0,TitleLength,2,50DescriptionLength,3,100DateFormat,3,agoFieldOrder,31,image,category,description,dateCreateLink,4,TrueLinkTarget,0,ShowDescriptionAsLink,5,FalseUseTemplate,0,ForceToUseTemplate,5,FalseMoreLink,0,DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,3,134Template,12,Default.aspxRegion,9,OrtaRightOrderNo,1,0Name,19,ContentListByFilterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,24,vitrin shadow toogleDescUseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(135, 'Default.aspx', 'OrtaLeft', 1, 'ContentListByFilter', '', 'Cinar.CMS.Serialization\nFilter,13,CategoryId=20HowManyItems,1,1SkipFirst,1,0Random,1,0OrderBy,19,Content.PublishDateAscending,5,FalseShowPictureLeftRight,5,FalseShowCurrentContent,5,FalseShowFirstItemWithPicture,5,FalseWhichPicture,15,Content.PicturePictureWidth,3,218PictureHeight,1,0BulletIcon,0,TitleLength,2,50DescriptionLength,3,150DateFormat,3,agoFieldOrder,31,image,category,description,dateCreateLink,4,TrueLinkTarget,0,ShowDescriptionAsLink,5,FalseUseTemplate,0,ForceToUseTemplate,5,FalseMoreLink,0,DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,3,135Template,12,Default.aspxRegion,8,OrtaLeftOrderNo,1,1Name,19,ContentListByFilterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,24,vitrin shadow toogleDescUseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(136, 'Default.aspx', 'OrtaLeft', 2, 'ContentListByFilter', '', 'Cinar.CMS.Serialization\nFilter,13,CategoryId=26HowManyItems,1,1SkipFirst,1,0Random,1,0OrderBy,19,Content.PublishDateAscending,5,FalseShowPictureLeftRight,5,FalseShowCurrentContent,5,FalseShowFirstItemWithPicture,5,FalseWhichPicture,15,Content.PicturePictureWidth,3,218PictureHeight,1,0BulletIcon,0,TitleLength,2,50DescriptionLength,3,150DateFormat,3,agoFieldOrder,31,image,category,description,dateCreateLink,4,TrueLinkTarget,0,ShowDescriptionAsLink,5,FalseUseTemplate,0,ForceToUseTemplate,5,FalseMoreLink,0,DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,3,136Template,12,Default.aspxRegion,8,OrtaLeftOrderNo,1,2Name,19,ContentListByFilterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,24,vitrin shadow toogleDescUseCache,7,DefaultCacheLifeTime,1,0DivClassName,12,extraItemDivCropPicture,5,False', NULL, NULL, 0, NULL, 'Default', NULL),
(137, 'Default.aspx', 'OrtaRight', 1, 'ContentListByFilter', '', 'Cinar.CMS.Serialization\nFilter,13,CategoryId=24HowManyItems,1,1SkipFirst,1,0Random,1,0OrderBy,19,Content.PublishDateAscending,5,FalseShowPictureLeftRight,5,FalseShowCurrentContent,5,FalseShowFirstItemWithPicture,5,FalseWhichPicture,15,Content.PicturePictureWidth,3,218PictureHeight,1,0BulletIcon,0,TitleLength,2,50DescriptionLength,3,150DateFormat,3,agoFieldOrder,31,image,category,description,dateCreateLink,4,TrueLinkTarget,0,ShowDescriptionAsLink,5,FalseUseTemplate,0,ForceToUseTemplate,5,FalseMoreLink,0,DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,3,137Template,12,Default.aspxRegion,9,OrtaRightOrderNo,1,1Name,19,ContentListByFilterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,24,vitrin shadow toogleDescUseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(138, 'Default.aspx', 'OrtaRight', 2, 'ContentListByFilter', '', 'Cinar.CMS.Serialization\nFilter,13,CategoryId=22HowManyItems,1,1SkipFirst,1,0Random,1,0OrderBy,19,Content.PublishDateAscending,5,FalseShowPictureLeftRight,5,FalseShowCurrentContent,5,FalseShowFirstItemWithPicture,5,FalseWhichPicture,15,Content.PicturePictureWidth,3,218PictureHeight,1,0BulletIcon,0,TitleLength,2,50DescriptionLength,3,150DateFormat,3,agoFieldOrder,31,image,category,description,dateCreateLink,4,TrueLinkTarget,0,ShowDescriptionAsLink,5,FalseUseTemplate,0,ForceToUseTemplate,5,FalseMoreLink,0,DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,3,138Template,12,Default.aspxRegion,9,OrtaRightOrderNo,1,2Name,19,ContentListByFilterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,24,vitrin shadow toogleDescUseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(142, 'AboutMe.aspx', 'Header', 0, 'RegionRepeater', '', 'Cinar.CMS.Serialization\nPage,12,Default.aspxRegionToCopy,6,HeaderId,3,142Template,12,AboutMe.aspxRegion,6,HeaderOrderNo,1,0Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(143, 'AboutMe.aspx', 'Header', 1, 'StaticHtml', '#StaticHtml_143 {\n}\n\n#StaticHtml_143 .headerMenu { border-bottom:1px solid black}\n#StaticHtml_143 .tab {\n	border:1px solid black; \n	border-bottom:none;\n	float:right; \n	padding:2px 40px;\n	cursor:pointer;\n	margin-left:10px;\n}\n#StaticHtml_143 .search {\n}\n#StaticHtml_143 .subscribe {\n	background:black;\n	color:white;\n}', 'Cinar.CMS.Serialization\nInnerHtml,244,<a href="/Default.aspx"><img src="/UserFiles/Images/design/logo.jpg" border="0"/></a>\n\n<div class="headerMenu">\n<div class="subscribe tab">Subscribe</div>\n<div class="search tab" id="searchTab">Search</div>\n<div style="clear:both"></div>\n</div>LangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,3,143Template,12,AboutMe.aspxRegion,6,HeaderOrderNo,1,1Name,10,StaticHtmlCSS,315,#StaticHtml_143 {\n}\n\n#StaticHtml_143 .headerMenu { border-bottom:1px solid black}\n#StaticHtml_143 .tab {\n	border:1px solid black; \n	border-bottom:none;\n	float:right; \n	padding:2px 40px;\n	cursor:pointer;\n	margin-left:10px;\n}\n#StaticHtml_143 .search {\n}\n#StaticHtml_143 .subscribe {\n	background:black;\n	color:white;\n}TopHtml,0,BottomHtml,0,ParentModuleId,3,142CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 142, NULL, 'Default', NULL),
(144, 'AboutMe.aspx', 'Header', 2, 'SearchForm', '#SearchForm_144 {\n	position:absolute;\n	right:147px;\n	top:156px;\n}\n#SearchForm_144 input.searchText {\n	border:none;\n	width:114px;\n}\n\n', 'Cinar.CMS.Serialization\nResultsPage,11,Search.aspxId,3,144Template,12,AboutMe.aspxRegion,6,HeaderOrderNo,1,2Name,10,SearchFormCSS,132,#SearchForm_144 {\n	position:absolute;\n	right:147px;\n	top:156px;\n}\n#SearchForm_144 input.searchText {\n	border:none;\n	width:114px;\n}\n\nTopHtml,273,<script>\nElement.observe(document, ''dom:loaded'', function(){\n	var sf = $(''SearchForm_82'');\n	sf.hide();\n	$(''searchTab'').on(''click'', function(){\n		sf.show();\n		sf.down(''.searchText'').focus();\n	});\n	sf.down(''.searchText'').on(''blur'', function(){\n		sf.hide();\n	});\n});\n</script>BottomHtml,0,ParentModuleId,3,142CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 142, NULL, 'Default', NULL),
(147, 'AboutMe.aspx', 'Footer', 1, 'RegionRepeater', '', 'Cinar.CMS.Serialization\nPage,12,Default.aspxRegionToCopy,6,FooterId,3,147Template,12,AboutMe.aspxRegion,6,FooterOrderNo,1,1Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(148, 'AboutMe.aspx', 'Footer', 2, 'StaticHtml', '#StaticHtml_148 {\n	border-top:1px solid black;\n	text-align:center;\n	font-weight:bold;\n	padding:4px;\n	margin-top:10px;\n	clear:both;\n	margin-top:30px;\n}\n', 'Cinar.CMS.Serialization\nInnerHtml,224,<a href="/Love.aspx">LOVE</a> &nbsp; &nbsp; &nbsp;\n<a href="/Other.aspx?item=33">ABOUT US</a> &nbsp; &nbsp; &nbsp;\n<a href="/Other.aspx?item=34">CONTACT US</a> &nbsp; &nbsp; &nbsp;\n<a href="/">(C) 2011 BLANK-MAGAZINE.COM</a>LangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,3,148Template,12,AboutMe.aspxRegion,6,FooterOrderNo,1,2Name,10,StaticHtmlCSS,151,#StaticHtml_148 {\n	border-top:1px solid black;\n	text-align:center;\n	font-weight:bold;\n	padding:4px;\n	margin-top:10px;\n	clear:both;\n	margin-top:30px;\n}\nTopHtml,0,BottomHtml,0,ParentModuleId,3,147CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 147, NULL, 'Default', NULL),
(149, 'AboutMe.aspx', 'ContentLeft', 0, 'RegionRepeater', '', 'Cinar.CMS.Serialization\nPage,11,Author.aspxRegionToCopy,11,ContentLeftId,3,149Template,12,AboutMe.aspxRegion,11,ContentLeftOrderNo,1,0Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(150, 'AboutMe.aspx', 'Content', 0, 'StaticHtml', '', 'Cinar.CMS.Serialization\nInnerHtml,131,<div class="innerDiv">\n	<div class="title">About Me</div>\n	<div class="text">\n	$=Context.Content.Author.Description$\n	</div>\n</div>LangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,3,150Template,12,AboutMe.aspxRegion,7,ContentOrderNo,1,0Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,24,shadow clsContentDisplayUseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(152, 'Login.aspx', 'Header', 0, 'RegionRepeater', '', 'Cinar.CMS.Serialization\nPage,12,Default.aspxRegionToCopy,6,HeaderId,3,152Template,10,Login.aspxRegion,6,HeaderOrderNo,1,0Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(153, 'Login.aspx', 'Header', 1, 'StaticHtml', '#StaticHtml_153 {\n}\n\n#StaticHtml_153 .headerMenu { border-bottom:1px solid black}\n#StaticHtml_153 .tab {\n	border:1px solid black; \n	border-bottom:none;\n	float:right; \n	padding:2px 40px;\n	cursor:pointer;\n	margin-left:10px;\n}\n#StaticHtml_153 .search {\n}\n#StaticHtml_153 .subscribe {\n	background:black;\n	color:white;\n}', 'Cinar.CMS.Serialization\nInnerHtml,244,<a href="/Default.aspx"><img src="/UserFiles/Images/design/logo.jpg" border="0"/></a>\n\n<div class="headerMenu">\n<div class="subscribe tab">Subscribe</div>\n<div class="search tab" id="searchTab">Search</div>\n<div style="clear:both"></div>\n</div>LangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,3,153Template,10,Login.aspxRegion,6,HeaderOrderNo,1,1Name,10,StaticHtmlCSS,315,#StaticHtml_153 {\n}\n\n#StaticHtml_153 .headerMenu { border-bottom:1px solid black}\n#StaticHtml_153 .tab {\n	border:1px solid black; \n	border-bottom:none;\n	float:right; \n	padding:2px 40px;\n	cursor:pointer;\n	margin-left:10px;\n}\n#StaticHtml_153 .search {\n}\n#StaticHtml_153 .subscribe {\n	background:black;\n	color:white;\n}TopHtml,0,BottomHtml,0,ParentModuleId,3,152CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 152, NULL, 'Default', NULL),
(154, 'Login.aspx', 'Header', 2, 'SearchForm', '#SearchForm_154 {\n	position:absolute;\n	right:147px;\n	top:156px;\n}\n#SearchForm_154 input.searchText {\n	border:none;\n	width:114px;\n}\n\n', 'Cinar.CMS.Serialization\nResultsPage,11,Search.aspxId,3,154Template,10,Login.aspxRegion,6,HeaderOrderNo,1,2Name,10,SearchFormCSS,132,#SearchForm_154 {\n	position:absolute;\n	right:147px;\n	top:156px;\n}\n#SearchForm_154 input.searchText {\n	border:none;\n	width:114px;\n}\n\nTopHtml,273,<script>\nElement.observe(document, ''dom:loaded'', function(){\n	var sf = $(''SearchForm_82'');\n	sf.hide();\n	$(''searchTab'').on(''click'', function(){\n		sf.show();\n		sf.down(''.searchText'').focus();\n	});\n	sf.down(''.searchText'').on(''blur'', function(){\n		sf.hide();\n	});\n});\n</script>BottomHtml,0,ParentModuleId,3,152CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 152, NULL, 'Default', NULL),
(155, 'Login.aspx', 'Footer', 0, 'RegionRepeater', '', 'Cinar.CMS.Serialization\nPage,12,Default.aspxRegionToCopy,6,FooterId,3,155Template,10,Login.aspxRegion,6,FooterOrderNo,1,0Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(156, 'Login.aspx', 'Footer', 1, 'StaticHtml', '#StaticHtml_156 {\n	border-top:1px solid black;\n	text-align:center;\n	font-weight:bold;\n	padding:4px;\n	margin-top:10px;\n	clear:both;\n	margin-top:30px;\n}\n', 'Cinar.CMS.Serialization\nInnerHtml,1246,<a href="/Love.aspx">LOVE</a> &nbsp; &nbsp; &nbsp;\n<a href="/Other.aspx?item=33">ABOUT US</a> &nbsp; &nbsp; &nbsp;\n<a href="/Other.aspx?item=34">CONTACT US</a> &nbsp; &nbsp; &nbsp;\n<a href="/">(C) 2011 BLANK-MAGAZINE.COM</a>\n\n<div style="float:right">\n<a title="facebook" onclick="window.open(''http://www.facebook.com/sharer.php?u=''+encodeURIComponent(location.href), ''facebook'',''toolbar=yes,scrollbars=yes,resizable=yes,width=800,height=600''); return false;" href="javascript:;"><img src="/UserFiles/Images/design/facebook.png" border="0"/></a>\n<a title="twitter" onclick="window.open(''http://twitter.com/?status=''+encodeURIComponent(document.title)+'' ''+encodeURIComponent(location.href), ''twitter'',''toolbar=yes,scrollbars=yes,resizable=yes,width=800,height=600''); return false;" href="javascript:;"><img src="/UserFiles/Images/design/twitter.png" border="0"/></a>\n<a title="google" onclick="window.open(''http://www.google.com/bookmarks/mark?op=edit&amp;bkmk=''+encodeURIComponent(location.href)+''&amp;title=''+encodeURIComponent(document.title)+''&amp;source=Blank'', ''google'',''toolbar=yes,scrollbars=yes,resizable=yes,width=800,height=600''); return false;" href="javascript:;"><img src="/UserFiles/Images/design/google.png" border="0"/></a>\n</div>LangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,3,156Template,10,Login.aspxRegion,6,FooterOrderNo,1,1Name,10,StaticHtmlCSS,151,#StaticHtml_156 {\n	border-top:1px solid black;\n	text-align:center;\n	font-weight:bold;\n	padding:4px;\n	margin-top:10px;\n	clear:both;\n	margin-top:30px;\n}\nTopHtml,0,BottomHtml,0,ParentModuleId,3,155CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 155, NULL, 'Default', NULL),
(157, 'Login.aspx', 'Content', 0, 'LoginForm', '#LoginForm_157 {\n	clear:both;\n	margin:100px;\n	margin-left:270px;\n}\n\n#LoginForm_157 div.loginError {\n	color:red}\n#LoginForm_157 a {\n	display:block;}\n#LoginForm_157 input.loginSubmitButton {\n	display:block;\n	margin-top:20px}\n\n', 'Cinar.CMS.Serialization\nRedirect,10,Admin.aspxShowMembershipLink,5,FalseShowMembershipInfoLink,5,FalseShowPasswordForgetLink,5,FalseShowRememberMe,5,FalseShowActivationLink,5,FalseId,3,157Template,10,Login.aspxRegion,7,ContentOrderNo,1,0Name,9,LoginFormCSS,224,#LoginForm_157 {\n	clear:both;\n	margin:100px;\n	margin-left:270px;\n}\n\n#LoginForm_157 div.loginError {\n	color:red}\n#LoginForm_157 a {\n	display:block;}\n#LoginForm_157 input.loginSubmitButton {\n	display:block;\n	margin-top:20px}\n\nTopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(159, 'Moda.aspx', 'Header', 0, 'RegionRepeater', '', 'Cinar.CMS.Serialization\nPage,12,Default.aspxRegionToCopy,6,HeaderId,3,159Template,9,Moda.aspxRegion,6,HeaderOrderNo,1,0Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(160, 'Moda.aspx', 'Header', 1, 'StaticHtml', '#StaticHtml_160 {\n}\n\n#StaticHtml_160 .headerMenu { border-bottom:1px solid black}\n#StaticHtml_160 .tab {\n	border:1px solid black; \n	border-bottom:none;\n	float:right; \n	padding:2px 40px;\n	cursor:pointer;\n	margin-left:10px;\n}\n#StaticHtml_160 .search {\n}\n#StaticHtml_160 .subscribe {\n	background:black;\n	color:white;\n}', 'Cinar.CMS.Serialization\nInnerHtml,244,<a href="/Default.aspx"><img src="/UserFiles/Images/design/logo.jpg" border="0"/></a>\n\n<div class="headerMenu">\n<div class="subscribe tab">Subscribe</div>\n<div class="search tab" id="searchTab">Search</div>\n<div style="clear:both"></div>\n</div>LangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,3,160Template,9,Moda.aspxRegion,6,HeaderOrderNo,1,1Name,10,StaticHtmlCSS,315,#StaticHtml_160 {\n}\n\n#StaticHtml_160 .headerMenu { border-bottom:1px solid black}\n#StaticHtml_160 .tab {\n	border:1px solid black; \n	border-bottom:none;\n	float:right; \n	padding:2px 40px;\n	cursor:pointer;\n	margin-left:10px;\n}\n#StaticHtml_160 .search {\n}\n#StaticHtml_160 .subscribe {\n	background:black;\n	color:white;\n}TopHtml,0,BottomHtml,0,ParentModuleId,3,159CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 159, NULL, 'Default', NULL),
(161, 'Moda.aspx', 'Header', 2, 'SearchForm', '#SearchForm_161 {\n	position:absolute;\n	right:147px;\n	top:156px;\n}\n#SearchForm_161 input.searchText {\n	border:none;\n	width:114px;\n}\n\n', 'Cinar.CMS.Serialization\nResultsPage,11,Search.aspxId,3,161Template,9,Moda.aspxRegion,6,HeaderOrderNo,1,2Name,10,SearchFormCSS,132,#SearchForm_161 {\n	position:absolute;\n	right:147px;\n	top:156px;\n}\n#SearchForm_161 input.searchText {\n	border:none;\n	width:114px;\n}\n\nTopHtml,273,<script>\nElement.observe(document, ''dom:loaded'', function(){\n	var sf = $(''SearchForm_82'');\n	sf.hide();\n	$(''searchTab'').on(''click'', function(){\n		sf.show();\n		sf.down(''.searchText'').focus();\n	});\n	sf.down(''.searchText'').on(''blur'', function(){\n		sf.hide();\n	});\n});\n</script>BottomHtml,0,ParentModuleId,3,159CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 159, NULL, 'Default', NULL),
(162, 'Moda.aspx', 'Footer', 1, 'RegionRepeater', '', 'Cinar.CMS.Serialization\nPage,12,Default.aspxRegionToCopy,6,FooterId,3,162Template,9,Moda.aspxRegion,6,FooterOrderNo,1,0Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(163, 'Moda.aspx', 'Footer', 2, 'StaticHtml', '#StaticHtml_163 {\n	border-top:1px solid black;\n	text-align:center;\n	font-weight:bold;\n	padding:4px;\n	margin-top:10px;\n	clear:both;\n	margin-top:30px;\n}\n', 'Cinar.CMS.Serialization\nInnerHtml,1246,<a href="/Love.aspx">LOVE</a> &nbsp; &nbsp; &nbsp;\n<a href="/Other.aspx?item=33">ABOUT US</a> &nbsp; &nbsp; &nbsp;\n<a href="/Other.aspx?item=34">CONTACT US</a> &nbsp; &nbsp; &nbsp;\n<a href="/">(C) 2011 BLANK-MAGAZINE.COM</a>\n\n<div style="float:right">\n<a title="facebook" onclick="window.open(''http://www.facebook.com/sharer.php?u=''+encodeURIComponent(location.href), ''facebook'',''toolbar=yes,scrollbars=yes,resizable=yes,width=800,height=600''); return false;" href="javascript:;"><img src="/UserFiles/Images/design/facebook.png" border="0"/></a>\n<a title="twitter" onclick="window.open(''http://twitter.com/?status=''+encodeURIComponent(document.title)+'' ''+encodeURIComponent(location.href), ''twitter'',''toolbar=yes,scrollbars=yes,resizable=yes,width=800,height=600''); return false;" href="javascript:;"><img src="/UserFiles/Images/design/twitter.png" border="0"/></a>\n<a title="google" onclick="window.open(''http://www.google.com/bookmarks/mark?op=edit&amp;bkmk=''+encodeURIComponent(location.href)+''&amp;title=''+encodeURIComponent(document.title)+''&amp;source=Blank'', ''google'',''toolbar=yes,scrollbars=yes,resizable=yes,width=800,height=600''); return false;" href="javascript:;"><img src="/UserFiles/Images/design/google.png" border="0"/></a>\n</div>LangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,3,163Template,9,Moda.aspxRegion,6,FooterOrderNo,1,1Name,10,StaticHtmlCSS,151,#StaticHtml_163 {\n	border-top:1px solid black;\n	text-align:center;\n	font-weight:bold;\n	padding:4px;\n	margin-top:10px;\n	clear:both;\n	margin-top:30px;\n}\nTopHtml,0,BottomHtml,0,ParentModuleId,3,162CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 162, NULL, 'Default', NULL),
(164, 'Moda.aspx', 'Content', 0, 'DataList', '.slideShow {\n	width:910px;\n	margin-left:40px;\n}\n.slideShow .clipper {\n	width:828px;\n}\n.slideShow .clipper .innerDiv img {\n	margin-right:20px;\n}', 'Cinar.CMS.Serialization\nEntityName,14,ContentPictureFilter,18,ContentId=@ContentHowManyItems,2,30OrderBy,7,OrderNoAscending,4,TrueDataTemplate,333,<img \n	src="$=this.ThumbPicture$"\n	path="$=entity.FileName$" \n	like=''$=entity.LikeIt$'' \n	title=''$=Context.Server.HtmlEncode(entity.Title)$'' \n	desc=''$=Context.Server.HtmlEncode(entity.Description)$'' \n	tagData=''$=Context.Server.HtmlEncode(entity.TagData)$'' \n	entityId=''$=entity.Id$''\n	likeSrc=''/external/icons/love.png''\n	height="360"/> PictureWidth,1,0PictureHeight,3,360DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseDivClassName,12,extraItemDivId,3,164Template,9,Moda.aspxRegion,7,ContentOrderNo,1,0Name,8,DataListCSS,143,.slideShow {\n	width:910px;\n	margin-left:40px;\n}\n.slideShow .clipper {\n	width:828px;\n}\n.slideShow .clipper .innerDiv img {\n	margin-right:20px;\n}TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,18,slideShow lightBoxUseCache,7,DefaultCacheLifeTime,1,0ShowPaging,5,False', NULL, NULL, 0, NULL, 'Default', NULL),
(165, 'Moda.aspx', 'Footer', 0, 'ContentListByTag', '#ContentListByTag_165 div.title {\n	border-bottom:1px solid black;\n	padding:50px 0px 5px 5px;\n	margin-bottom:30px;\n	font-family:spinnaker;\n}\n\n#ContentListByTag_165 div.paging {\n	float:left;\n	width:60px;\n	height:120px;\n	background-repeat:no-repeat;\n}\n\n#ContentListByTag_165 .cerceve {\n	float:left;\n	width:855px;\n	border: 1px solid #efefef;\n\n}\n\n#ContentListByTag_165 div.clItem {\n	float:left;\n	margin:0px 5px 5px 0px;\n}\n', 'Cinar.CMS.Serialization\nFilter,20,CategoryId=@CategoryHowManyItems,1,5SkipFirst,1,0Random,1,0OrderBy,10,Content.IdAscending,5,FalseShowPictureLeftRight,5,FalseShowCurrentContent,4,TrueShowFirstItemWithPicture,5,FalseWhichPicture,15,Content.PicturePictureWidth,1,0PictureHeight,3,120BulletIcon,0,TitleLength,2,50DescriptionLength,3,150DateFormat,12,dd MMMM yyyyFieldOrder,5,imageCreateLink,4,TrueLinkTarget,0,ShowDescriptionAsLink,5,FalseUseTemplate,0,ForceToUseTemplate,5,FalseMoreLink,0,DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,3,165Template,9,Moda.aspxRegion,6,FooterOrderNo,1,0Name,16,ContentListByTagCSS,417,#ContentListByTag_165 div.title {\n	border-bottom:1px solid black;\n	padding:50px 0px 5px 5px;\n	margin-bottom:30px;\n	font-family:spinnaker;\n}\n\n#ContentListByTag_165 div.paging {\n	float:left;\n	width:60px;\n	height:120px;\n	background-repeat:no-repeat;\n}\n\n#ContentListByTag_165 .cerceve {\n	float:left;\n	width:855px;\n	border: 1px solid #efefef;\n\n}\n\n#ContentListByTag_165 div.clItem {\n	float:left;\n	margin:0px 5px 5px 0px;\n}\nTopHtml,181,<div class="title">ISSUES</div>\n\n<div class="paging" style="background-image:url(/UserFiles/Images/design/prev.png); background-position:right center;"></div>\n\n<div class="cerceve">BottomHtml,163,</div>\n<div class="paging"  style="background-image:url(/UserFiles/Images/design/next.png); background-position:left center;"></div>\n<div style="clear:both"></div>ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0DivClassName,12,extraItemDiv', NULL, NULL, 0, NULL, 'Default', NULL),
(167, 'LoveHate.aspx', 'Header', 0, 'RegionRepeater', '', 'Cinar.CMS.Serialization\nPage,12,Default.aspxRegionToCopy,6,HeaderId,3,167Template,9,Hate.aspxRegion,6,HeaderOrderNo,1,0Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(168, 'LoveHate.aspx', 'Header', 1, 'StaticHtml', '#StaticHtml_168 {\n}\n\n#StaticHtml_168 .headerMenu { border-bottom:1px solid black}\n#StaticHtml_168 .tab {\n	border:1px solid black; \n	border-bottom:none;\n	float:right; \n	padding:2px 40px;\n	cursor:pointer;\n	margin-left:10px;\n}\n#StaticHtml_168 .search {\n}\n#StaticHtml_168 .subscribe {\n	background:black;\n	color:white;\n}', 'Cinar.CMS.Serialization\nInnerHtml,244,<a href="/Default.aspx"><img src="/UserFiles/Images/design/logo.jpg" border="0"/></a>\n\n<div class="headerMenu">\n<div class="subscribe tab">Subscribe</div>\n<div class="search tab" id="searchTab">Search</div>\n<div style="clear:both"></div>\n</div>LangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,3,168Template,9,Hate.aspxRegion,6,HeaderOrderNo,1,1Name,10,StaticHtmlCSS,315,#StaticHtml_168 {\n}\n\n#StaticHtml_168 .headerMenu { border-bottom:1px solid black}\n#StaticHtml_168 .tab {\n	border:1px solid black; \n	border-bottom:none;\n	float:right; \n	padding:2px 40px;\n	cursor:pointer;\n	margin-left:10px;\n}\n#StaticHtml_168 .search {\n}\n#StaticHtml_168 .subscribe {\n	background:black;\n	color:white;\n}TopHtml,0,BottomHtml,0,ParentModuleId,3,167CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 167, NULL, 'Default', NULL),
(169, 'LoveHate.aspx', 'Header', 2, 'SearchForm', '#SearchForm_169 {\n	position:absolute;\n	right:147px;\n	top:156px;\n}\n#SearchForm_169 input.searchText {\n	border:none;\n	width:114px;\n}\n\n', 'Cinar.CMS.Serialization\nResultsPage,11,Search.aspxId,3,169Template,9,Hate.aspxRegion,6,HeaderOrderNo,1,2Name,10,SearchFormCSS,132,#SearchForm_169 {\n	position:absolute;\n	right:147px;\n	top:156px;\n}\n#SearchForm_169 input.searchText {\n	border:none;\n	width:114px;\n}\n\nTopHtml,273,<script>\nElement.observe(document, ''dom:loaded'', function(){\n	var sf = $(''SearchForm_82'');\n	sf.hide();\n	$(''searchTab'').on(''click'', function(){\n		sf.show();\n		sf.down(''.searchText'').focus();\n	});\n	sf.down(''.searchText'').on(''blur'', function(){\n		sf.hide();\n	});\n});\n</script>BottomHtml,0,ParentModuleId,3,167CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 167, NULL, 'Default', NULL),
(170, 'LoveHate.aspx', 'Footer', 0, 'RegionRepeater', '', 'Cinar.CMS.Serialization\nPage,12,Default.aspxRegionToCopy,6,FooterId,3,170Template,9,Hate.aspxRegion,6,FooterOrderNo,1,0Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(171, 'LoveHate.aspx', 'Footer', 1, 'StaticHtml', '#StaticHtml_171 {\n	border-top:1px solid black;\n	text-align:center;\n	font-weight:bold;\n	padding:4px;\n	margin-top:10px;\n	clear:both;\n	margin-top:30px;\n}\n', 'Cinar.CMS.Serialization\nInnerHtml,1311,<a href="/Love.aspx?item=5">LOVE</a> &nbsp; &nbsp; &nbsp;\n<a href="/Hate.aspx?item=4">HATE</a> &nbsp; &nbsp; &nbsp;\n<a href="/Other.aspx?item=33">ABOUT US</a> &nbsp; &nbsp; &nbsp;\n<a href="/Other.aspx?item=34">CONTACT US</a> &nbsp; &nbsp; &nbsp;\n<a href="/">(C) 2011 BLANK-MAGAZINE.COM</a>\n\n<div style="float:right">\n<a title="facebook" onclick="window.open(''http://www.facebook.com/sharer.php?u=''+encodeURIComponent(location.href), ''facebook'',''toolbar=yes,scrollbars=yes,resizable=yes,width=800,height=600''); return false;" href="javascript:;"><img src="/UserFiles/Images/design/facebook.png" border="0"/></a>\n<a title="twitter" onclick="window.open(''http://twitter.com/?status=''+encodeURIComponent(document.title)+'' ''+encodeURIComponent(location.href), ''twitter'',''toolbar=yes,scrollbars=yes,resizable=yes,width=800,height=600''); return false;" href="javascript:;"><img src="/UserFiles/Images/design/twitter.png" border="0"/></a>\n<a title="google" onclick="window.open(''http://www.google.com/bookmarks/mark?op=edit&amp;bkmk=''+encodeURIComponent(location.href)+''&amp;title=''+encodeURIComponent(document.title)+''&amp;source=Blank'', ''google'',''toolbar=yes,scrollbars=yes,resizable=yes,width=800,height=600''); return false;" href="javascript:;"><img src="/UserFiles/Images/design/google.png" border="0"/></a>\n</div>LangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,3,171Template,9,Hate.aspxRegion,6,FooterOrderNo,1,1Name,10,StaticHtmlCSS,151,#StaticHtml_171 {\n	border-top:1px solid black;\n	text-align:center;\n	font-weight:bold;\n	padding:4px;\n	margin-top:10px;\n	clear:both;\n	margin-top:30px;\n}\nTopHtml,0,BottomHtml,0,ParentModuleId,3,170CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 170, NULL, 'Default', NULL),
(172, 'LoveHate.aspx', 'Content', 0, 'DataList', '#DataList_172 {\n	margin:15px 0px;\n	border:1px solid #E0E0E0;\n	padding:20px;\n}\n#DataList_172 img {\n	max-width:160px; \n	max-height:160px;\n	vertical-align:top;\n	margin-bottom:15px;\n}\n#DataList_172 div.paging {\n	font-weight:bold;\n	padding:4px;\n	text-align:center;\n	margin-top:50px;\n}\n#DataList_172 div.paging a {\n	text-transform:uppercase;\n}\n#DataList_172 div.paging a.prev {\n	float:left;\n}\n#DataList_172 div.paging a.next {\n	float:right;\n}\n', 'Cinar.CMS.Serialization\nEntityName,14,ContentPictureFilter,18,ContentId=@ContentHowManyItems,2,20OrderBy,2,IdAscending,5,FalseDataTemplate,347,<img \n	src="$=this.ThumbPicture$"\n	path="$=entity.FileName$" \n	like=''$=entity.LikeIt$'' \n	title=''$=Context.Server.HtmlEncode(entity.Title)$'' \n	desc=''$=Context.Server.HtmlEncode(entity.Description)$'' \n	tagData=''$=Context.Server.HtmlEncode(entity.TagData)$'' \n	entityId=''$=entity.Id$''\n	likeSrc=''/external/icons/$=Provider.Content.Title$.png''/>\n&nbsp;\nPictureWidth,3,180PictureHeight,3,180DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseDivClassName,12,extraItemDivId,3,172Template,13,LoveHate.aspxRegion,7,ContentOrderNo,1,0Name,8,DataListCSS,437,#DataList_172 {\n	margin:15px 0px;\n	border:1px solid #E0E0E0;\n	padding:20px;\n}\n#DataList_172 img {\n	max-width:160px; \n	max-height:160px;\n	vertical-align:top;\n	margin-bottom:15px;\n}\n#DataList_172 div.paging {\n	font-weight:bold;\n	padding:4px;\n	text-align:center;\n	margin-top:50px;\n}\n#DataList_172 div.paging a {\n	text-transform:uppercase;\n}\n#DataList_172 div.paging a.prev {\n	float:left;\n}\n#DataList_172 div.paging a.next {\n	float:right;\n}\nTopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,15,gogPop lightBoxUseCache,7,DefaultCacheLifeTime,1,0ShowPaging,4,TrueCropPicture,4,True', NULL, NULL, 0, NULL, 'Default', NULL),
(173, 'People.aspx', 'ContentLeft', 0, 'ContentListByFilter', '#ContentListByFilter_173 {\n	margin-top:30px;\n}\n', 'Cinar.CMS.Serialization\nFilter,18,Category=@CategoryHowManyItems,1,1SkipFirst,1,0Random,1,0OrderBy,19,Content.PublishDateAscending,5,FalseShowPictureLeftRight,5,FalseShowCurrentContent,4,TrueShowFirstItemWithPicture,5,FalseWhichPicture,15,Content.PicturePictureWidth,3,180PictureHeight,1,0BulletIcon,0,TitleLength,2,50DescriptionLength,3,150DateFormat,12,dd MMMM yyyyFieldOrder,14,image,categoryCreateLink,4,TrueLinkTarget,0,ShowDescriptionAsLink,5,FalseUseTemplate,0,ForceToUseTemplate,5,FalseMoreLink,0,DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,3,173Template,11,People.aspxRegion,11,ContentLeftOrderNo,1,0Name,19,ContentListByFilterCSS,47,#ContentListByFilter_173 {\n	margin-top:30px;\n}\nTopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,22,vitrin solda rotate-10UseCache,7,DefaultCacheLifeTime,1,0DivClassName,12,extraItemDiv', NULL, NULL, 0, NULL, 'Default', NULL),
(174, 'People.aspx', 'Content', 0, 'ContentDisplay', '#ContentDisplay_174 div.title {\n	font-size:16px;\n	border-bottom:1px solid #CFCFCF;\n	padding-top:24px;\n}\n#ContentDisplay_174 div.text {\n	margin-top:20px;\n	background:white;	\n}\n#ContentDisplay_174 div.innerDiv {\n	padding:10px 100px;\n	background:white;	\n}\n#ContentDisplay_174 div.date {\n	margin-bottom:24px;\n	font-size:12px;\n}\n\n', 'Cinar.CMS.Serialization\nFieldOrder,22,title,date,region,textDateFormat,12,dd MMMM yyyyFilter,11,Id=@ContentTagTemplate,0,Id,3,174Template,11,People.aspxRegion,7,ContentOrderNo,1,0Name,14,ContentDisplayCSS,325,#ContentDisplay_174 div.title {\n	font-size:16px;\n	border-bottom:1px solid #CFCFCF;\n	padding-top:24px;\n}\n#ContentDisplay_174 div.text {\n	margin-top:20px;\n	background:white;	\n}\n#ContentDisplay_174 div.innerDiv {\n	padding:10px 100px;\n	background:white;	\n}\n#ContentDisplay_174 div.date {\n	margin-bottom:24px;\n	font-size:12px;\n}\n\nTopHtml,22,<div class="innerDiv">BottomHtml,1065,</div>\n\n<p style="text-align:right;padding:20px 100px;background:white">\n<a title="facebook" onclick="window.open(''http://www.facebook.com/sharer.php?u=''+encodeURIComponent(location.href), ''facebook'',''toolbar=yes,scrollbars=yes,resizable=yes,width=800,height=600''); return false;" href="javascript:;"><img src="/UserFiles/Images/design/facebook.png" border="0"/></a>\n<a title="twitter" onclick="window.open(''http://twitter.com/?status=''+encodeURIComponent(document.title)+'' ''+encodeURIComponent(location.href), ''twitter'',''toolbar=yes,scrollbars=yes,resizable=yes,width=800,height=600''); return false;" href="javascript:;"><img src="/UserFiles/Images/design/twitter.png" border="0"/></a>\n<a title="google" onclick="window.open(''http://www.google.com/bookmarks/mark?op=edit&amp;bkmk=''+encodeURIComponent(location.href)+''&amp;title=''+encodeURIComponent(document.title)+''&amp;source=Blank'', ''google'',''toolbar=yes,scrollbars=yes,resizable=yes,width=800,height=600''); return false;" href="javascript:;"><img src="/UserFiles/Images/design/google.png" border="0"/></a>\n</p>ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(176, 'People.aspx', 'Header', 0, 'RegionRepeater', '', 'Cinar.CMS.Serialization\nId,3,176Template,11,People.aspxRegion,6,HeaderOrderNo,1,0Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0RegionToCopy,6,HeaderPage,12,Default.aspx', NULL, NULL, 0, NULL, 'Default', NULL),
(177, 'People.aspx', 'Header', 1, 'StaticHtml', '#StaticHtml_177 {\n}\n\n#StaticHtml_177 .headerMenu { border-bottom:1px solid black}\n#StaticHtml_177 .tab {\n	border:1px solid black; \n	border-bottom:none;\n	float:right; \n	padding:2px 40px;\n	cursor:pointer;\n	margin-left:10px;\n}\n#StaticHtml_177 .search {\n}\n#StaticHtml_177 .subscribe {\n	background:black;\n	color:white;\n}', 'Cinar.CMS.Serialization\nInnerHtml,244,<a href="/Default.aspx"><img src="/UserFiles/Images/design/logo.jpg" border="0"/></a>\n\n<div class="headerMenu">\n<div class="subscribe tab">Subscribe</div>\n<div class="search tab" id="searchTab">Search</div>\n<div style="clear:both"></div>\n</div>LangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,3,177Template,11,People.aspxRegion,6,HeaderOrderNo,1,1Name,10,StaticHtmlCSS,315,#StaticHtml_177 {\n}\n\n#StaticHtml_177 .headerMenu { border-bottom:1px solid black}\n#StaticHtml_177 .tab {\n	border:1px solid black; \n	border-bottom:none;\n	float:right; \n	padding:2px 40px;\n	cursor:pointer;\n	margin-left:10px;\n}\n#StaticHtml_177 .search {\n}\n#StaticHtml_177 .subscribe {\n	background:black;\n	color:white;\n}TopHtml,0,BottomHtml,0,ParentModuleId,3,176CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 176, NULL, 'Default', NULL),
(178, 'People.aspx', 'Header', 2, 'SearchForm', '#SearchForm_178 {\n	position:absolute;\n	right:147px;\n	top:156px;\n}\n#SearchForm_178 input.searchText {\n	border:none;\n	width:114px;\n}\n\n', 'Cinar.CMS.Serialization\nResultsPage,11,Search.aspxId,3,178Template,11,People.aspxRegion,6,HeaderOrderNo,1,2Name,10,SearchFormCSS,132,#SearchForm_178 {\n	position:absolute;\n	right:147px;\n	top:156px;\n}\n#SearchForm_178 input.searchText {\n	border:none;\n	width:114px;\n}\n\nTopHtml,273,<script>\nElement.observe(document, ''dom:loaded'', function(){\n	var sf = $(''SearchForm_82'');\n	sf.hide();\n	$(''searchTab'').on(''click'', function(){\n		sf.show();\n		sf.down(''.searchText'').focus();\n	});\n	sf.down(''.searchText'').on(''blur'', function(){\n		sf.hide();\n	});\n});\n</script>BottomHtml,0,ParentModuleId,3,176CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 176, NULL, 'Default', NULL),
(179, 'People.aspx', 'Footer', 0, 'ContentListByTag', '#ContentListByTag_179 div.title {\n	border-bottom:1px solid black;\n	padding:50px 0px 5px 65px;\n	margin-bottom:30px;\n	font-family:spinnaker;\n}\n\n#ContentListByTag_179 div.paging {\n	float:left;\n	width:60px;\n	height:120px;\n	background-repeat:no-repeat;\n}\n\n#ContentListByTag_179 .cerceve {\n	float:left;\n	width:855px;\n	border: 1px solid #efefef;\n\n}\n\n#ContentListByTag_179 div.clItem {\n	float:left;\n	margin:0px 5px 5px 0px;\n}\n', 'Cinar.CMS.Serialization\nFilter,20,CategoryId=@CategoryHowManyItems,2,10SkipFirst,1,0Random,1,0OrderBy,19,Content.PublishDateAscending,5,FalseShowPictureLeftRight,5,FalseShowCurrentContent,4,TrueShowFirstItemWithPicture,5,FalseWhichPicture,15,Content.PicturePictureWidth,1,0PictureHeight,3,120BulletIcon,0,TitleLength,2,50DescriptionLength,3,150DateFormat,12,dd MMMM yyyyFieldOrder,5,imageCreateLink,4,TrueLinkTarget,0,ShowDescriptionAsLink,5,FalseUseTemplate,0,ForceToUseTemplate,5,FalseMoreLink,0,DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,3,179Template,11,People.aspxRegion,6,FooterOrderNo,1,0Name,16,ContentListByTagCSS,418,#ContentListByTag_179 div.title {\n	border-bottom:1px solid black;\n	padding:50px 0px 5px 65px;\n	margin-bottom:30px;\n	font-family:spinnaker;\n}\n\n#ContentListByTag_179 div.paging {\n	float:left;\n	width:60px;\n	height:120px;\n	background-repeat:no-repeat;\n}\n\n#ContentListByTag_179 .cerceve {\n	float:left;\n	width:855px;\n	border: 1px solid #efefef;\n\n}\n\n#ContentListByTag_179 div.clItem {\n	float:left;\n	margin:0px 5px 5px 0px;\n}\nTopHtml,182,<div class="title">ARCHIVE</div>\n\n<div class="paging" style="background-image:url(/UserFiles/Images/design/prev.png); background-position:right center;"></div>\n\n<div class="cerceve">BottomHtml,163,</div>\n<div class="paging"  style="background-image:url(/UserFiles/Images/design/next.png); background-position:left center;"></div>\n<div style="clear:both"></div>ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0DivClassName,12,extraItemDivCropPicture,5,False', NULL, NULL, 0, NULL, 'Default', NULL),
(180, 'People.aspx', 'ContentLeft', 1, 'SQLDataList', '#SQLDataList_180 {\n	margin-top:60px;\n	width:230px;\n}\n#SQLDataList_180 .tag {\n	border-bottom: 1px solid #8A8A8A;\n	padding:10px 0px 4px 4px;\n	text-transform:uppercase;\n}', 'Cinar.CMS.Serialization\nSQL,175,SELECT\n	t.Id,\n	t.Name\nFROM\n	Tag t\n	INNER JOIN ContentTag ct ON ct.TagId = t.Id\n	INNER JOIN Content c ON c.Id = ct.ContentId\nWHERE\n	c.CategoryId = $=Context.Content.CategoryId$DataTemplate,110,<div class="tag">\n<a href="/AuthorList.aspx?item=$=Context.Content.Id$&tagId=$=row.Id$">$=row.Name$</a>\n</div>PictureWidth,1,0PictureHeight,1,0DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,3,180Template,11,People.aspxRegion,11,ContentLeftOrderNo,1,1Name,11,SQLDataListCSS,167,#SQLDataList_180 {\n	margin-top:60px;\n	width:230px;\n}\n#SQLDataList_180 .tag {\n	border-bottom: 1px solid #8A8A8A;\n	padding:10px 0px 4px 4px;\n	text-transform:uppercase;\n}TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,5,soldaUseCache,7,DefaultCacheLifeTime,1,0DivClassName,12,extraItemDiv', NULL, NULL, 0, NULL, 'Default', NULL),
(181, 'People.aspx', 'Footer', 1, 'RegionRepeater', '', 'Cinar.CMS.Serialization\nId,3,181Template,11,People.aspxRegion,6,FooterOrderNo,1,1Name,14,RegionRepeaterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0RegionToCopy,6,FooterPage,12,Default.aspx', NULL, NULL, 0, NULL, 'Default', NULL),
(182, 'People.aspx', 'Footer', 2, 'StaticHtml', '#StaticHtml_182 {\n	border-top:1px solid black;\n	border-bottom:1px solid black;\n	text-align:center;\n	font-family: Spinnaker, tahoma;\n	padding:4px;\n	margin-top:10px;\n	clear:both;\n	margin-top:30px;\n}\n', 'Cinar.CMS.Serialization\nInnerHtml,1332,<a href="/Love.aspx?item=5">LOVE</a> &nbsp; &nbsp; &nbsp;\n<a href="/Hate.aspx?item=4">HATE</a> &nbsp; &nbsp; &nbsp;\n| &nbsp; &nbsp; &nbsp;\n<a href="/Other.aspx?item=33">ABOUT US</a> &nbsp; &nbsp; &nbsp;\n<a href="/Other.aspx?item=34">CONTACT US</a> &nbsp; &nbsp; &nbsp;\n<a href="/">© 2011 BLANK-MAGAZINE.COM</a>\n\n<div style="float:right">\n<a title="facebook" onclick="window.open(''http://www.facebook.com/sharer.php?u=''+encodeURIComponent(location.href), ''facebook'',''toolbar=yes,scrollbars=yes,resizable=yes,width=800,height=600''); return false;" href="javascript:;"><img src="/UserFiles/Images/design/facebook.png" border="0"/></a>\n<a title="twitter" onclick="window.open(''http://twitter.com/?status=''+encodeURIComponent(document.title)+'' ''+encodeURIComponent(location.href), ''twitter'',''toolbar=yes,scrollbars=yes,resizable=yes,width=800,height=600''); return false;" href="javascript:;"><img src="/UserFiles/Images/design/twitter.png" border="0"/></a>\n<a title="google" onclick="window.open(''http://www.google.com/bookmarks/mark?op=edit&amp;bkmk=''+encodeURIComponent(location.href)+''&amp;title=''+encodeURIComponent(document.title)+''&amp;source=Blank'', ''google'',''toolbar=yes,scrollbars=yes,resizable=yes,width=800,height=600''); return false;" href="javascript:;"><img src="/UserFiles/Images/design/google.png" border="0"/></a>\n</div>LangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,3,182Template,11,People.aspxRegion,6,FooterOrderNo,1,2Name,10,StaticHtmlCSS,197,#StaticHtml_182 {\n	border-top:1px solid black;\n	border-bottom:1px solid black;\n	text-align:center;\n	font-family: Spinnaker, tahoma;\n	padding:4px;\n	margin-top:10px;\n	clear:both;\n	margin-top:30px;\n}\nTopHtml,0,BottomHtml,0,ParentModuleId,3,181CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 181, NULL, 'Default', NULL);
INSERT INTO `module` (`Id`, `Template`, `Region`, `OrderNo`, `Name`, `CSS`, `Details`, `TopHtml`, `BottomHtml`, `ParentModuleId`, `CSSClass`, `UseCache`, `CacheLifeTime`) VALUES
(183, 'People.aspx', 'conRegion174', 0, 'ContentPicture', '', 'Cinar.CMS.Serialization\nWidth,3,718Height,1,0Id,3,183Template,11,People.aspxRegion,12,conRegion174OrderNo,1,0Name,14,ContentPictureCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,3,174CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 174, NULL, 'Default', NULL),
(184, 'People.aspx', 'Content', 2, 'DataList', '#DataList_184 div.paging {\n	font-weight:bold;\n	padding:4px;\n	text-align:center;\n	margin-top:50px;\n}\n#DataList_184 div.paging a {\n	text-transform:uppercase;\n}\n#DataList_184 div.paging a.prev {\n	float:left;\n}\n#DataList_184 div.paging a.next {\n	float:right;\n}\n#DataList_184 div.innerDiv {\n	padding:10px 100px;\n	background:white;\n	min-height:400px;\n}\n#DataList_184 div.innerDiv img {\n\n}', 'Cinar.CMS.Serialization\nEntityName,14,ContentPictureFilter,18,ContentId=@ContentHowManyItems,2,20OrderBy,2,IdAscending,5,FalseDataTemplate,310,<img \n	src="$=this.ThumbPicture$"\n	path="$=entity.FileName$" \n	like=''$=entity.LikeIt$'' \n	title=''$=Context.Server.HtmlEncode(entity.Title)$'' \n	tagData=''$=Context.Server.HtmlEncode(entity.TagData)$'' \n	entityId=''$=entity.Id$''\n	likeSrc=''/external/icons/love.png''/>\n<div class="clDesc">\n$=entity.Description$\n</div>PictureWidth,3,220PictureHeight,1,0DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseDivClassName,6,clItemId,3,184Template,11,People.aspxRegion,7,ContentOrderNo,1,1Name,8,DataListCSS,382,#DataList_184 div.paging {\n	font-weight:bold;\n	padding:4px;\n	text-align:center;\n	margin-top:50px;\n}\n#DataList_184 div.paging a {\n	text-transform:uppercase;\n}\n#DataList_184 div.paging a.prev {\n	float:left;\n}\n#DataList_184 div.paging a.next {\n	float:right;\n}\n#DataList_184 div.innerDiv {\n	padding:10px 100px;\n	background:white;\n	min-height:400px;\n}\n#DataList_184 div.innerDiv img {\n\n}TopHtml,44,<div class="innerDiv">\n<div class="masonry">BottomHtml,12,</div></div>ParentModuleId,1,0CSSClass,22,showDescOnImg lightBoxUseCache,7,DefaultCacheLifeTime,1,0ShowPaging,4,TrueCropPicture,5,False', NULL, NULL, 0, NULL, 'Default', NULL),
(192, 'Admin.aspx', 'Content', 1, 'DataList', '', 'Cinar.CMS.Serialization\nEntityName,7,ContentFilter,12,CategoryId=6HowManyItems,4,1000OrderBy,2,IdAscending,5,FalseDataTemplate,531,$=entity.Title$<br/>\n<img src="$=this.ThumbPicture$" width="100" height="100" title="Çekimleri Göster"\n	onclick="openEntityListForm(''ContentPicture'', ''Moda Çekimleri'', ''ContentId=$=entity.Id$'', false, null, true)"/><br/>\n<img src="/external/icons/edit.png" title="Düzenle"\n	onclick="editData(''Content'', $=entity.Id$, ''İçerik'', function(){location.reload();}, ''CategoryId=6 AND ClassName=Content'')"/>\n<img src="/external/icons/delete.png" title="Sil"\n	onclick="deleteData(''Content'', $=entity.Id$, function(){location.reload();})"/>\nPictureWidth,3,100PictureHeight,3,100DisplayAsTable,4,TrueCols,4,1000EncloseWithDiv,5,FalseDivClassName,12,extraItemDivId,3,192Template,10,Admin.aspxRegion,7,ContentOrderNo,1,1Name,8,DataListCSS,0,TopHtml,276,<h2>\n<span class="title" onclick="toggleDataList(this)">i am FASHION SHOOT</span>\n<span \n	class="add" \n	onclick="editData(''Content'', 0, ''İçerik'', function(){location.reload();}, ''CategoryId=6 AND ClassName=Content'')">\nAdd New Fashion Shoot\n</span>\n</h2>\n<div class="innerDiv">BottomHtml,135,</div>\nDikkat: En ideal görünüm için kapak resimleri 490×368 px, çekimler ise sırasıyla 526x360 px ve 277x360 px ebatlarında olmalıdır.ParentModuleId,1,0CSSClass,13,adminDataListUseCache,7,DefaultCacheLifeTime,1,0ShowPaging,5,FalseCropPicture,5,False', NULL, NULL, 0, NULL, 'Default', NULL),
(193, 'Admin.aspx', 'Content', 2, 'DataList', '', 'Cinar.CMS.Serialization\nEntityName,6,AuthorFilter,0,HowManyItems,4,1000OrderBy,2,IdAscending,5,FalseDataTemplate,552,$=entity.Name$<br/>\n<img src="$=this.ThumbPicture$" width="100" height="100" title="Yazıları Göster"\n	onclick="openEntityListForm(''Content'', ''Yazar Yazıları'', ''AuthorId=$=entity.Id$ AND ClassName=Content AND CategoryId=8'', false, null, true, ''İçerik'')"/><br/>\n<img src="/external/icons/edit.png" title="Düzenle"\n	onclick="editData(''Author'', $=entity.Id$, ''İçerik'', function(){location.reload();},''DisableAutoContent=1'')"/>\n<img src="/external/icons/delete.png" title="Sil"\n	onclick="deleteData(''Author'', $=entity.Id$, function(){location.reload();})"/>PictureWidth,3,100PictureHeight,3,100DisplayAsTable,4,TrueCols,4,1000EncloseWithDiv,5,FalseDivClassName,12,extraItemDivId,3,193Template,10,Admin.aspxRegion,7,ContentOrderNo,1,2Name,8,DataListCSS,0,TopHtml,247,<h2>\n<span class="title" onclick="toggleDataList(this)">i am AUTHORS</span>\n<span \n	class="add" \n	onclick="editData(''Author'', 0, ''İçerik'', function(){location.reload();},''DisableAutoContent=1'')">\nAdd New Author\n</span>\n</h2>\n<div class="innerDiv">BottomHtml,6,</div>ParentModuleId,1,0CSSClass,13,adminDataListUseCache,7,DefaultCacheLifeTime,1,0ShowPaging,4,TrueCropPicture,5,False', NULL, NULL, 0, NULL, 'Default', NULL),
(194, 'Admin.aspx', 'Header', 0, 'StaticHtml', '#StaticHtml_194 {\n	margin-bottom:20px;\n}\n\n#StaticHtml_194 .headerMenu { border-bottom:1px solid black}\n#StaticHtml_194 .tab {\n	border:1px solid black; \n	border-bottom:none;\n	float:right; \n	padding:2px 40px;\n	cursor:pointer;\n	margin-left:10px;\n}\n#StaticHtml_194 .search {\n}\n#StaticHtml_194 .subscribe {\n	background:black;\n	color:white;\n}', 'Cinar.CMS.Serialization\nInnerHtml,91,<a href="/Admin.aspx"><img src="/UserFiles/Images/design/blank_admin.jpg" border="0"/></a>\nLangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,3,194Template,10,Admin.aspxRegion,6,HeaderOrderNo,1,0Name,10,StaticHtmlCSS,336,#StaticHtml_194 {\n	margin-bottom:20px;\n}\n\n#StaticHtml_194 .headerMenu { border-bottom:1px solid black}\n#StaticHtml_194 .tab {\n	border:1px solid black; \n	border-bottom:none;\n	float:right; \n	padding:2px 40px;\n	cursor:pointer;\n	margin-left:10px;\n}\n#StaticHtml_194 .search {\n}\n#StaticHtml_194 .subscribe {\n	background:black;\n	color:white;\n}TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(195, 'Admin.aspx', 'Footer', 1, 'StaticHtml', '#StaticHtml_195 {\n	border-top:1px solid black;\n	border-bottom:1px solid black;\n	text-align:center;\n	font-family: Spinnaker, tahoma;\n	padding:4px;\n	margin-top:10px;\n	clear:both;\n	margin-top:30px;\n}\n', 'Cinar.CMS.Serialization\nInnerHtml,130,<a href="http://www.artechin.com">designed by ARTECHIN</a>\n&amp;\n<a href="http://www.cinarteknoloji.com">powered by ÇINAR CMS</a>\nLangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,3,195Template,10,Admin.aspxRegion,6,FooterOrderNo,1,1Name,10,StaticHtmlCSS,197,#StaticHtml_195 {\n	border-top:1px solid black;\n	border-bottom:1px solid black;\n	text-align:center;\n	font-family: Spinnaker, tahoma;\n	padding:4px;\n	margin-top:10px;\n	clear:both;\n	margin-top:30px;\n}\nTopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(196, 'Admin.aspx', 'Content', 0, 'PageSecurity', '', 'Cinar.CMS.Serialization\nRoleToRead,6,EditorRoleToChange,8,DesignerRedirectPage,10,Login.aspxId,3,196Template,10,Admin.aspxRegion,7,ContentOrderNo,1,2Name,12,PageSecurityCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(197, 'Admin.aspx', 'Content', 3, 'DataList', '', 'Cinar.CMS.Serialization\nEntityName,7,ContentFilter,12,CategoryId=7HowManyItems,4,1000OrderBy,2,IdAscending,5,FalseDataTemplate,514,$=entity.Title$<br/>\n<img src="$=this.ThumbPicture$" width="100" height="100" title="Çekimleri Göster"\n	onclick="openEntityListForm(''ContentPicture'', ''People Çekimleri'', ''ContentId=$=entity.Id$'')"/><br/>\n<img src="/external/icons/edit.png" title="Düzenle"\n	onclick="editData(''Content'', $=entity.Id$, ''İçerik'', function(){location.reload();}, ''CategoryId=7 AND ClassName=Content'')"/>\n<img src="/external/icons/delete.png" title="Sil"\n	onclick="deleteData(''Content'', $=entity.Id$, function(){location.reload();})"/>\nPictureWidth,3,100PictureHeight,3,100DisplayAsTable,4,TrueCols,4,1000EncloseWithDiv,5,FalseDivClassName,12,extraItemDivId,3,197Template,10,Admin.aspxRegion,7,ContentOrderNo,1,3Name,8,DataListCSS,0,TopHtml,260,<h2>\n<span class="title" onclick="toggleDataList(this)">i am PEOPLE</span>\n<span class="add" \n	onclick="editData(''Content'', 0, ''İçerik'', function(){location.reload();}, ''CategoryId=7 AND ClassName=Content'')">\nAdd New Person\n</span>\n</h2>\n<div class="innerDiv">BottomHtml,6,</div>ParentModuleId,1,0CSSClass,13,adminDataListUseCache,7,DefaultCacheLifeTime,1,0ShowPaging,5,False', NULL, NULL, 0, NULL, 'Default', NULL),
(198, 'Admin.aspx', 'Content', 4, 'DataList', '', 'Cinar.CMS.Serialization\nEntityName,7,ContentFilter,13,CategoryId=48HowManyItems,4,1000OrderBy,2,IdAscending,5,FalseDataTemplate,515,$=entity.Title$<br/>\n<img src="$=this.ThumbPicture$" width="100" height="100" title="Çekimleri Göster"\n	onclick="openEntityListForm(''ContentPicture'', ''Çekimler'', ''ContentId=$=entity.Id$'')"/><br/>\n<img src="/external/icons/edit.png" title="Düzenle"\n	onclick="editData(''Content'', $=entity.Id$, ''İçerik'', function(){location.reload();}, ''CategoryId=48 AND ClassName=Content'')"/>\n<!--\n<img src="/external/icons/delete.png" title="Sil"\n	onclick="deleteData(''Content'', $=entity.Id$, function(){location.reload();})"/>\n-->PictureWidth,3,100PictureHeight,3,100DisplayAsTable,4,TrueCols,4,1000EncloseWithDiv,5,FalseDivClassName,12,extraItemDivId,3,198Template,10,Admin.aspxRegion,7,ContentOrderNo,1,4Name,8,DataListCSS,0,TopHtml,134,<h2>\n<span class="title" onclick="toggleDataList(this)">i am LOVE & HATE</span>\n<span class="add"></span>\n</h2>\n<div class="innerDiv">BottomHtml,6,</div>ParentModuleId,1,0CSSClass,13,adminDataListUseCache,7,DefaultCacheLifeTime,1,0ShowPaging,5,False', NULL, NULL, 0, NULL, 'Default', NULL),
(199, 'Admin.aspx', 'Content', 5, 'DataList', '', 'Cinar.CMS.Serialization\nEntityName,7,ContentFilter,12,CategoryId=3HowManyItems,4,1000OrderBy,2,IdAscending,5,FalseDataTemplate,512,$=entity.Title$<br/>\n<img src="$=this.ThumbPicture$" width="100" height="100" title="Çekimleri Göster"\n	onclick="openEntityListForm(''ContentPicture'', ''City Çekimleri'', ''ContentId=$=entity.Id$'')"/><br/>\n<img src="/external/icons/edit.png" title="Düzenle"\n	onclick="editData(''Content'', $=entity.Id$, ''İçerik'', function(){location.reload();}, ''CategoryId=3 AND ClassName=Content'')"/>\n<img src="/external/icons/delete.png" title="Sil"\n	onclick="deleteData(''Content'', $=entity.Id$, function(){location.reload();})"/>\nPictureWidth,3,100PictureHeight,3,100DisplayAsTable,4,TrueCols,4,1000EncloseWithDiv,5,FalseDivClassName,12,extraItemDivId,3,199Template,10,Admin.aspxRegion,7,ContentOrderNo,1,5Name,8,DataListCSS,0,TopHtml,256,<h2>\n<span class="title" onclick="toggleDataList(this)">i am CITY</span>\n<span class="add" \n	onclick="editData(''Content'', 0, ''İçerik'', function(){location.reload();}, ''CategoryId=3 AND ClassName=Content'')">\nAdd New City\n</span>\n</h2>\n<div class="innerDiv">BottomHtml,6,</div>ParentModuleId,1,0CSSClass,13,adminDataListUseCache,7,DefaultCacheLifeTime,1,0ShowPaging,5,False', NULL, NULL, 0, NULL, 'Default', NULL),
(200, 'Admin.aspx', 'Content', 6, 'DataList', '', 'Cinar.CMS.Serialization\nEntityName,7,ContentFilter,12,CategoryId=9HowManyItems,4,1000OrderBy,2,IdAscending,5,FalseDataTemplate,498,$=entity.Title$<br/>\n<img src="$=this.ThumbPicture$" width="100" height="100" title="Çekimler"\n	onclick="openEntityListForm(''ContentPicture'', ''Çekimler'', ''ContentId=$=entity.Id$'')"/><br/>\n<img src="/external/icons/edit.png" title="Düzenle"\n	onclick="editData(''Content'', $=entity.Id$, ''İçerik'', function(){location.reload();}, ''CategoryId=9 AND ClassName=Content'')"/>\n<img src="/external/icons/delete.png" title="Sil"\n	onclick="deleteData(''Content'', $=entity.Id$, function(){location.reload();})"/>\nPictureWidth,3,100PictureHeight,3,100DisplayAsTable,4,TrueCols,4,1000EncloseWithDiv,5,FalseDivClassName,12,extraItemDivId,3,200Template,10,Admin.aspxRegion,7,ContentOrderNo,1,6Name,8,DataListCSS,0,TopHtml,266,<h2>\n<span class="title" onclick="toggleDataList(this)">i am BIRD VIEW</span>\n<span class="add" \n	onclick="editData(''Content'', 0, ''İçerik'', function(){location.reload();}, ''CategoryId=9 AND ClassName=Content'')">\nAdd New Bird View\n</span>\n</h2>\n<div class="innerDiv">BottomHtml,6,</div>ParentModuleId,1,0CSSClass,13,adminDataListUseCache,7,DefaultCacheLifeTime,1,0ShowPaging,5,False', NULL, NULL, 0, NULL, 'Default', NULL),
(201, 'Admin.aspx', 'Content', 7, 'DataList', '', 'Cinar.CMS.Serialization\nEntityName,7,ContentFilter,13,CategoryId=20HowManyItems,4,1000OrderBy,2,IdAscending,5,FalseDataTemplate,499,$=entity.Title$<br/>\n<img src="$=this.ThumbPicture$" width="100" height="100" title="Çekimler"\n	onclick="openEntityListForm(''ContentPicture'', ''Çekimler'', ''ContentId=$=entity.Id$'')"/><br/>\n<img src="/external/icons/edit.png" title="Düzenle"\n	onclick="editData(''Content'', $=entity.Id$, ''İçerik'', function(){location.reload();}, ''CategoryId=20 AND ClassName=Content'')"/>\n<img src="/external/icons/delete.png" title="Sil"\n	onclick="deleteData(''Content'', $=entity.Id$, function(){location.reload();})"/>\nPictureWidth,3,100PictureHeight,3,100DisplayAsTable,4,TrueCols,4,1000EncloseWithDiv,5,FalseDivClassName,12,extraItemDivId,3,201Template,10,Admin.aspxRegion,7,ContentOrderNo,1,7Name,8,DataListCSS,0,TopHtml,259,<h2>\n<span class="title" onclick="toggleDataList(this)">i am MUSIC</span>\n<span class="add" \n	onclick="editData(''Content'', 0, ''İçerik'', function(){location.reload();}, ''CategoryId=20 AND ClassName=Content'')">\nAdd New Music\n</span>\n</h2>\n<div class="innerDiv">BottomHtml,6,</div>ParentModuleId,1,0CSSClass,13,adminDataListUseCache,7,DefaultCacheLifeTime,1,0ShowPaging,5,False', NULL, NULL, 0, NULL, 'Default', NULL),
(202, 'Admin.aspx', 'Content', 8, 'DataList', '', 'Cinar.CMS.Serialization\nEntityName,7,ContentFilter,13,CategoryId=26HowManyItems,4,1000OrderBy,2,IdAscending,5,FalseDataTemplate,499,$=entity.Title$<br/>\n<img src="$=this.ThumbPicture$" width="100" height="100" title="Çekimler"\n	onclick="openEntityListForm(''ContentPicture'', ''Çekimler'', ''ContentId=$=entity.Id$'')"/><br/>\n<img src="/external/icons/edit.png" title="Düzenle"\n	onclick="editData(''Content'', $=entity.Id$, ''İçerik'', function(){location.reload();}, ''CategoryId=26 AND ClassName=Content'')"/>\n<img src="/external/icons/delete.png" title="Sil"\n	onclick="deleteData(''Content'', $=entity.Id$, function(){location.reload();})"/>\nPictureWidth,3,100PictureHeight,3,100DisplayAsTable,4,TrueCols,4,1000EncloseWithDiv,5,FalseDivClassName,12,extraItemDivId,3,202Template,10,Admin.aspxRegion,7,ContentOrderNo,1,8Name,8,DataListCSS,0,TopHtml,259,<h2>\n<span class="title" onclick="toggleDataList(this)">i am THINK</span>\n<span class="add" \n	onclick="editData(''Content'', 0, ''İçerik'', function(){location.reload();}, ''CategoryId=26 AND ClassName=Content'')">\nAdd New Think\n</span>\n</h2>\n<div class="innerDiv">BottomHtml,6,</div>ParentModuleId,1,0CSSClass,13,adminDataListUseCache,7,DefaultCacheLifeTime,1,0ShowPaging,5,False', NULL, NULL, 0, NULL, 'Default', NULL),
(203, 'Admin.aspx', 'Content', 9, 'DataList', '', 'Cinar.CMS.Serialization\nEntityName,7,ContentFilter,13,CategoryId=24HowManyItems,4,1000OrderBy,2,IdAscending,5,FalseDataTemplate,507,$=entity.Title$<br/>\n<img src="$=this.ThumbPicture$" width="100" height="100" title="Çekimleri Göster"\n	onclick="openEntityListForm(''ContentPicture'', ''Çekimler'', ''ContentId=$=entity.Id$'')"/><br/>\n<img src="/external/icons/edit.png" title="Düzenle"\n	onclick="editData(''Content'', $=entity.Id$, ''İçerik'', function(){location.reload();}, ''CategoryId=24 AND ClassName=Content'')"/>\n<img src="/external/icons/delete.png" title="Sil"\n	onclick="deleteData(''Content'', $=entity.Id$, function(){location.reload();})"/>\nPictureWidth,3,100PictureHeight,3,100DisplayAsTable,4,TrueCols,4,1000EncloseWithDiv,5,FalseDivClassName,12,extraItemDivId,3,203Template,10,Admin.aspxRegion,7,ContentOrderNo,1,9Name,8,DataListCSS,0,TopHtml,261,<h2>\n<span class="title" onclick="toggleDataList(this)">i am BEAUTY</span>\n<span class="add" \n	onclick="editData(''Content'', 0, ''İçerik'', function(){location.reload();}, ''CategoryId=24 AND ClassName=Content'')">\nAdd New Beauty\n</span>\n</h2>\n<div class="innerDiv">BottomHtml,6,</div>ParentModuleId,1,0CSSClass,13,adminDataListUseCache,7,DefaultCacheLifeTime,1,0ShowPaging,5,False', NULL, NULL, 0, NULL, 'Default', NULL),
(204, 'Admin.aspx', 'Content', 10, 'DataList', '', 'Cinar.CMS.Serialization\nEntityName,7,ContentFilter,13,CategoryId=22HowManyItems,4,1000OrderBy,2,IdAscending,5,FalseDataTemplate,507,$=entity.Title$<br/>\n<img src="$=this.ThumbPicture$" width="100" height="100" title="Çekimleri Göster"\n	onclick="openEntityListForm(''ContentPicture'', ''Çekimler'', ''ContentId=$=entity.Id$'')"/><br/>\n<img src="/external/icons/edit.png" title="Düzenle"\n	onclick="editData(''Content'', $=entity.Id$, ''İçerik'', function(){location.reload();}, ''CategoryId=22 AND ClassName=Content'')"/>\n<img src="/external/icons/delete.png" title="Sil"\n	onclick="deleteData(''Content'', $=entity.Id$, function(){location.reload();})"/>\nPictureWidth,3,100PictureHeight,3,100DisplayAsTable,4,TrueCols,4,1000EncloseWithDiv,5,FalseDivClassName,12,extraItemDivId,3,204Template,10,Admin.aspxRegion,7,ContentOrderNo,2,10Name,8,DataListCSS,0,TopHtml,259,<h2>\n<span class="title" onclick="toggleDataList(this)">i am WORDS</span>\n<span class="add" \n	onclick="editData(''Content'', 0, ''İçerik'', function(){location.reload();}, ''CategoryId=22 AND ClassName=Content'')">\nAdd New Words\n</span>\n</h2>\n<div class="innerDiv">BottomHtml,6,</div>ParentModuleId,1,0CSSClass,13,adminDataListUseCache,7,DefaultCacheLifeTime,1,0ShowPaging,5,False', NULL, NULL, 0, NULL, 'Default', NULL),
(205, 'Admin.aspx', 'Content', 11, 'DataList', '', 'Cinar.CMS.Serialization\nEntityName,7,ContentFilter,13,CategoryId=15HowManyItems,4,1000OrderBy,2,IdAscending,5,FalseDataTemplate,507,$=entity.Title$<br/>\n<img src="$=this.ThumbPicture$" width="100" height="100" title="Çekimleri Göster"\n	onclick="openEntityListForm(''ContentPicture'', ''Çekimler'', ''ContentId=$=entity.Id$'')"/><br/>\n<img src="/external/icons/edit.png" title="Düzenle"\n	onclick="editData(''Content'', $=entity.Id$, ''İçerik'', function(){location.reload();}, ''CategoryId=15 AND ClassName=Content'')"/>\n<img src="/external/icons/delete.png" title="Sil"\n	onclick="deleteData(''Content'', $=entity.Id$, function(){location.reload();})"/>\nPictureWidth,3,100PictureHeight,3,100DisplayAsTable,4,TrueCols,4,1000EncloseWithDiv,5,FalseDivClassName,12,extraItemDivId,3,205Template,10,Admin.aspxRegion,7,ContentOrderNo,2,11Name,8,DataListCSS,0,TopHtml,257,<h2>\n<span class="title" onclick="toggleDataList(this)">i am ROOM</span>\n<span class="add" \n	onclick="editData(''Content'', 0, ''İçerik'', function(){location.reload();}, ''CategoryId=15 AND ClassName=Content'')">\nAdd New Room\n</span>\n</h2>\n<div class="innerDiv">BottomHtml,6,</div>ParentModuleId,1,0CSSClass,13,adminDataListUseCache,7,DefaultCacheLifeTime,1,0ShowPaging,5,False', NULL, NULL, 0, NULL, 'Default', NULL),
(206, 'Admin.aspx', 'Content', 12, 'DataList', '', 'Cinar.CMS.Serialization\nEntityName,7,ContentFilter,13,CategoryId=10HowManyItems,4,1000OrderBy,2,IdAscending,5,FalseDataTemplate,507,$=entity.Title$<br/>\n<img src="$=this.ThumbPicture$" width="100" height="100" title="Çekimleri Göster"\n	onclick="openEntityListForm(''ContentPicture'', ''Çekimler'', ''ContentId=$=entity.Id$'')"/><br/>\n<img src="/external/icons/edit.png" title="Düzenle"\n	onclick="editData(''Content'', $=entity.Id$, ''İçerik'', function(){location.reload();}, ''CategoryId=10 AND ClassName=Content'')"/>\n<img src="/external/icons/delete.png" title="Sil"\n	onclick="deleteData(''Content'', $=entity.Id$, function(){location.reload();})"/>\nPictureWidth,3,100PictureHeight,3,100DisplayAsTable,4,TrueCols,4,1000EncloseWithDiv,5,FalseDivClassName,12,extraItemDivId,3,206Template,10,Admin.aspxRegion,7,ContentOrderNo,2,12Name,8,DataListCSS,0,TopHtml,257,<h2>\n<span class="title" onclick="toggleDataList(this)">i am COOK</span>\n<span class="add" \n	onclick="editData(''Content'', 0, ''İçerik'', function(){location.reload();}, ''CategoryId=10 AND ClassName=Content'')">\nAdd New Cook\n</span>\n</h2>\n<div class="innerDiv">BottomHtml,6,</div>ParentModuleId,1,0CSSClass,13,adminDataListUseCache,7,DefaultCacheLifeTime,1,0ShowPaging,5,False', NULL, NULL, 0, NULL, 'Default', NULL),
(207, 'Admin.aspx', 'Content', 13, 'DataList', '', 'Cinar.CMS.Serialization\nEntityName,7,ContentFilter,13,CategoryId=32HowManyItems,4,1000OrderBy,2,IdAscending,5,FalseDataTemplate,332,$=entity.Title$<br/>\n<img src="/external/icons/edit.png" title="Düzenle"\n	onclick="editData(''Content'', $=entity.Id$, ''İçerik'', function(){location.reload();}, ''CategoryId=32 AND ClassName=Content'')"/>\n<img src="/external/icons/delete.png" title="Sil"\n	onclick="deleteData(''Content'', $=entity.Id$, function(){location.reload();})"/>\nPictureWidth,3,100PictureHeight,3,100DisplayAsTable,4,TrueCols,4,1000EncloseWithDiv,5,FalseDivClassName,12,extraItemDivId,3,207Template,10,Admin.aspxRegion,7,ContentOrderNo,2,13Name,8,DataListCSS,0,TopHtml,265,<h2>\n<span class="title" onclick="toggleDataList(this)">i am FOOTER LINKS</span>\n<span class="add" \n	onclick="editData(''Content'', 0, ''İçerik'', function(){location.reload();}, ''CategoryId=32 AND ClassName=Content'')">\nAdd New Cook\n</span>\n</h2>\n<div class="innerDiv">BottomHtml,6,</div>ParentModuleId,1,0CSSClass,13,adminDataListUseCache,7,DefaultCacheLifeTime,1,0ShowPaging,5,False', NULL, NULL, 0, NULL, 'Default', NULL),
(209, 'Admin.aspx', 'Content', 14, 'StaticHtml', '#StaticHtml_209 .innerDiv {\n	padding:10px;\n	border:1px solid black;\n}\n', 'Cinar.CMS.Serialization\nInnerHtml,916,<a href="Admin.aspx?subFilter=day">Today</a> &nbsp;&nbsp;|&nbsp;&nbsp;\n<a href="Admin.aspx?subFilter=week">Last Week</a> &nbsp;&nbsp;|&nbsp;&nbsp;\n<a href="Admin.aspx?subFilter=month">Last Month</a> &nbsp;&nbsp;|&nbsp;&nbsp;\n<a href="Admin.aspx?subFilter=all">All Time</a> <br/>\n<textarea cols="50" rows="10" wrap="off">\n$\nvar subFilter = Context.Request[''subFilter''];\nif(subFilter==''day'')\n	subFilter=DateTime.Now.AddDays(-1);\nif(subFilter==''week'')\n	subFilter=DateTime.Now.AddDays(-7);\nif(subFilter==''month'')\n	subFilter=DateTime.Now.AddDays(-30);\nif(subFilter==''all'')\n	subFilter=DateTime.Now.AddDays(-10000);\nif(subFilter)\n{\n	var sql = "\n		SELECT \n			Description \n		FROM \n			Log \n		WHERE \n			Category=''Subscribe'' AND \n			InsertDate > ''"+subFilter.ToString(''yyyy-MM-dd'')+"''\n		ORDER BY Id DESC";\n\n	var dt = Context.Database.GetDataTable(sql);\n	foreach(var dr in dt.Rows)\n		echo(dr.Description+"\\r\\n");\n}\n$\n</textarea>\nLangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,3,209Template,10,Admin.aspxRegion,7,ContentOrderNo,2,14Name,10,StaticHtmlCSS,70,#StaticHtml_209 .innerDiv {\n	padding:10px;\n	border:1px solid black;\n}\nTopHtml,132,<h2>\n<span class="title" onclick="toggleDataList(this)">SUBSCRIPTIONS</span>\n<span class="add" ></span>\n</h2>\n<div class="innerDiv">BottomHtml,6,</div>ParentModuleId,1,0CSSClass,13,adminDataListUseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(210, 'Moda.aspx', 'Content', 1, 'DataList', '', 'Cinar.CMS.Serialization\nEntityName,14,ContentPictureFilter,26,ContentId=@PreviousContentHowManyItems,2,30OrderBy,7,OrderNoAscending,4,TrueDataTemplate,333,<img \n	src="$=this.ThumbPicture$"\n	path="$=entity.FileName$" \n	like=''$=entity.LikeIt$'' \n	title=''$=Context.Server.HtmlEncode(entity.Title)$'' \n	desc=''$=Context.Server.HtmlEncode(entity.Description)$'' \n	tagData=''$=Context.Server.HtmlEncode(entity.TagData)$'' \n	entityId=''$=entity.Id$''\n	likeSrc=''/external/icons/love.png''\n	height="360"/> PictureWidth,1,0PictureHeight,3,360DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseDivClassName,12,extraItemDivId,3,210Template,9,Moda.aspxRegion,7,ContentOrderNo,1,1Name,8,DataListCSS,0,TopHtml,0,BottomHtml,1,\nParentModuleId,1,0CSSClass,18,slideShow lightBoxUseCache,7,DefaultCacheLifeTime,1,0ShowPaging,5,False', NULL, NULL, 0, NULL, 'Default', NULL),
(211, 'Author.aspx', 'Content', 2, 'StaticHtml', '#StaticHtml_211 {\n	font-family: spinnaker;\n	margin-top:10px;\n}\n', 'Cinar.CMS.Serialization\nInnerHtml,899,$\nvar tagSQL = '''';\nif (Provider.Request["tagId"])\n   tagSQL = " AND Id in (SELECT ContentId FROM ContentTag WHERE TagId = "+Provider.Tag.Id+")";\nvar prevSQL = "select Id from Content where Id<"+Provider.Content.Id+" AND CategoryId="+Provider.Content.CategoryId+" AND AuthorId="+Provider.Content.AuthorId+" " + tagSQL + " order by Id desc limit 1";\nvar prevDr = Provider.Database.GetDataRow(prevSQL);\nvar nextSQL = "select Id from Content where Id>"+Provider.Content.Id+" AND CategoryId="+Provider.Content.CategoryId+" AND AuthorId="+Provider.Content.AuthorId+" " + tagSQL + " order by Id limit 1";\nvar nextDr = Provider.Database.GetDataRow(nextSQL);\n\n$\n\n<div>\n$if(prevDr){$\n<div style="float:left"><a href="Author.aspx?item=$=prevDr.Id$">PREVIOUS</a></div>\n$}$\n\n$if(nextDr){$\n<div style="float:right"><a href="Author.aspx?item=$=nextDr.Id$">NEXT</a></div>\n$}$\n\n<div style="clear:both"></div>\n</div>\nLangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,CSS,63,#StaticHtml_211 {\n	font-family: spinnaker;\n	margin-top:10px;\n}\nTopHtml,0,BottomHtml,0,CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0Id,3,211Template,11,Author.aspxRegion,7,ContentOrderNo,1,1Name,10,StaticHtmlParentModuleId,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(212, 'Moda.aspx', 'Content', 2, 'StaticHtml', '#StaticHtml_212 {\n	font-family: spinnaker;\n	margin:10px 40px;\n}\n', 'Cinar.CMS.Serialization\nInnerHtml,803,$\nvar tagSQL = '''';\nif (Provider.Request["tagId"])\n   tagSQL = " AND Id in (SELECT ContentId FROM ContentTag WHERE TagId = "+Provider.Tag.Id+")";\nvar prevSQL = "select Id from Content where Id<"+Provider.Content.Id+" AND CategoryId="+Provider.Content.CategoryId + tagSQL + " order by Id desc limit 1";\nvar prevDr = Provider.Database.GetDataRow(prevSQL);\nvar nextSQL = "select Id from Content where Id>"+Provider.Content.Id+" AND CategoryId="+Provider.Content.CategoryId + tagSQL + " order by Id limit 1";\nvar nextDr = Provider.Database.GetDataRow(nextSQL);\n\n$\n\n<div>\n$if(prevDr){$\n<div style="float:left"><a href="/Moda.aspx?item=$=prevDr.Id$">PREVIOUS</a></div>\n$}$\n\n$if(nextDr){$\n<div style="float:right"><a href="/Moda.aspx?item=$=nextDr.Id$">NEXT</a></div>\n$}$\n\n<div style="clear:both"></div>\n</div>\nLangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,CSS,64,#StaticHtml_212 {\n	font-family: spinnaker;\n	margin:10px 40px;\n}\nTopHtml,0,BottomHtml,0,CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0Id,3,212Template,9,Moda.aspxRegion,7,ContentOrderNo,1,2Name,10,StaticHtmlParentModuleId,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(215, 'Default.aspx', 'ContentLeft', 3, 'ContentListByFilter', '', 'Cinar.CMS.Serialization\nFilter,4,Id=5HowManyItems,1,1SkipFirst,1,0Random,1,0OrderBy,19,Content.PublishDateAscending,5,FalseShowPictureLeftRight,5,FalseShowCurrentContent,5,FalseShowFirstItemWithPicture,5,FalseWhichPicture,15,Content.PicturePictureWidth,3,218PictureHeight,1,0BulletIcon,0,TitleLength,2,50DescriptionLength,3,150DateFormat,3,agoFieldOrder,23,image,title,descriptionCreateLink,4,TrueLinkTarget,0,ShowDescriptionAsLink,5,FalseUseTemplate,0,ForceToUseTemplate,5,FalseMoreLink,0,DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,3,215Template,12,Default.aspxRegion,11,ContentLeftOrderNo,1,3Name,19,ContentListByFilterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,24,vitrin shadow toogleDescUseCache,7,DefaultCacheLifeTime,1,0DivClassName,12,extraItemDiv', NULL, NULL, 0, NULL, 'Default', NULL),
(216, 'Default.aspx', 'ContentRight', 3, 'ContentListByFilter', '', 'Cinar.CMS.Serialization\nFilter,4,Id=4HowManyItems,1,1SkipFirst,1,0Random,1,0OrderBy,19,Content.PublishDateAscending,5,FalseShowPictureLeftRight,5,FalseShowCurrentContent,5,FalseShowFirstItemWithPicture,5,FalseWhichPicture,15,Content.PicturePictureWidth,3,218PictureHeight,1,0BulletIcon,0,TitleLength,2,50DescriptionLength,3,150DateFormat,3,agoFieldOrder,23,image,title,descriptionCreateLink,4,TrueLinkTarget,0,ShowDescriptionAsLink,5,FalseUseTemplate,0,ForceToUseTemplate,5,FalseMoreLink,0,DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,3,216Template,12,Default.aspxRegion,12,ContentRightOrderNo,1,3Name,19,ContentListByFilterCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,24,vitrin shadow toogleDescUseCache,7,DefaultCacheLifeTime,1,0DivClassName,12,extraItemDiv', NULL, NULL, 0, NULL, 'Default', NULL),
(218, 'Admin.aspx', 'Footer', 0, 'StaticHtml', '#StaticHtml_218 {\n	text-align:right;\n	margin-top:20px;\n}\n', 'Cinar.CMS.Serialization\nInnerHtml,164,<span class="btn OK"\nonclick="location.href=''/'';"\n>Ana Sayfa</span>\n<span class="btn cancel"\nonclick="location.href=''/DoLogin.ashx?logout=1'';"\n>Oturumu Kapat</span>LangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,3,218Template,10,Admin.aspxRegion,6,FooterOrderNo,1,0Name,10,StaticHtmlCSS,57,#StaticHtml_218 {\n	text-align:right;\n	margin-top:20px;\n}\nTopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(220, 'Author.aspx', 'ContentLeft', 0, 'StaticHtml', '#StaticHtml_220 {\n	float:left;\n	margin-top:10px;\n}\n', 'Cinar.CMS.Serialization\nInnerHtml,149,$if(Provider.Content.Author.Picture2) {$\n<a href="/AboutMe.aspx?item=$=Provider.Content.Id$"><img src="$=Provider.Content.Author.Picture2$"/></a>\n$}$LangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,3,220Template,11,Author.aspxRegion,11,ContentLeftOrderNo,1,0Name,10,StaticHtmlCSS,51,#StaticHtml_220 {\n	float:left;\n	margin-top:10px;\n}\nTopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(221, 'Admin.aspx', 'Content', 15, 'StaticHtml', '#StaticHtml_221 .innerDiv {\n	padding:10px;\n	border:1px solid black;\n}\n', 'Cinar.CMS.Serialization\nInnerHtml,1703,$\nusing System.IO;\n\nvar introType = Provider.Request[''introType''];\nif(introType ){\n\nContext.ExecuteNonQuery("update Content set Tags = ''"+introType+"'' where Id=65");\nvar files = Context.GetDataTable("select * from ContentPicture where ContentId=65").Rows;\n\nvar xml = ''<?xml version="1.0" encoding="utf-8" ?>\n<settings welcomeMediaType="''+introType+''" mediaInterval="5" logoUrl="logos/BlankLogo.png" homepageFileName="/Default.aspx" iaminUrl="logos/iamin1.png" iaminOverColor="0xFFFFFF">\n	<positions playerBottomPadding="98" playerRightPadding="6" logoPos="TC" iaminPos="BC"/> \n		\n	<video>\n'';\nforeach(var flv in files)\n	if(flv.FileName.EndsWith(''.flv''))\n		xml += ''		<location>''+flv.FileName+''</location>'';\nxml += ''	</video>\n		\n	<photos>\n'';\nforeach(var jpg in files)\n	if(jpg.FileName.EndsWith(''.jpg''))\n		xml += ''		<photo><location>''+jpg.FileName+''</location></photo>'';\nxml += ''	</photos>\n\n	<trackList>\n'';\nforeach(var mp3 in files)\n	if(mp3.FileName.EndsWith(''.mp3''))\n		xml += ''		<track><creator>01</creator><title>''+mp3.Title+''</title><location>''+mp3.FileName+''</location></track>'';\nxml += ''	</trackList>\n</settings>'';\n\nFile.WriteAllText(Provider.Server.MapPath(''/UserFiles/intro/settings.xml''), xml);	\n}\n\nvar intro = Context.GetDataRow("select * from Content where Id=65");\n$\n<span class="btn" onclick="openEntityListForm(''ContentPicture'', ''Intro Files'', ''ContentId=65'', false, null, true)">Intro FLV, JPG ve MP3 dosyalarını düzenle</span>\n\nIntro Type: \n<select id="introType">\n<option $=(intro.Tags==''video'' ? ''selected'':'''')$>video</option>\n<option $=(intro.Tags==''photo'' ? ''selected'':'''')$>photo</option>\n</select>\n\n<button onclick="location.href=''Admin.aspx?introType=''+\\$F(''introType'')">SAVE</button>\n\nLangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,3,221Template,10,Admin.aspxRegion,7,ContentOrderNo,2,15Name,10,StaticHtmlCSS,70,#StaticHtml_221 .innerDiv {\n	padding:10px;\n	border:1px solid black;\n}\nTopHtml,130,<h2>\n<span class="title" onclick="toggleDataList(this)">INTRO FILES</span>\n<span class="add" ></span>\n</h2>\n<div class="innerDiv">BottomHtml,6,</div>ParentModuleId,1,0CSSClass,13,adminDataListUseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL),
(222, 'People.aspx', 'Content', 1, 'StaticHtml', '#StaticHtml_222 {\n	background:white;\n	padding:0px 50px;\n}\n', 'Cinar.CMS.Serialization\nInnerHtml,52,<div style="border-top:1px solid #CCC;">&nbsp;</div>LangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,3,222Template,11,People.aspxRegion,7,ContentOrderNo,1,1Name,10,StaticHtmlCSS,58,#StaticHtml_222 {\n	background:white;\n	padding:0px 50px;\n}\nTopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', NULL, NULL, 0, NULL, 'Default', NULL);

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı: `pollanswer`
--

DROP TABLE IF EXISTS `pollanswer`;
CREATE TABLE `pollanswer` (
  `Answer` varchar(100) COLLATE utf8_turkish_ci NOT NULL,
  `Hit` int(11) NOT NULL DEFAULT '0',
  `PollQuestionId` int(11) NOT NULL,
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `InsertDate` datetime NOT NULL DEFAULT '1990-01-01 00:00:00',
  `InsertUserId` int(11) DEFAULT NULL,
  `UpdateDate` datetime DEFAULT NULL,
  `UpdateUserId` int(11) DEFAULT NULL,
  `Visible` tinyint(1) NOT NULL DEFAULT '1',
  `OrderNo` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=1 ;

--
-- Tablo döküm verisi `pollanswer`
--


-- --------------------------------------------------------

--
-- Tablo için tablo yapısı: `pollquestion`
--

DROP TABLE IF EXISTS `pollquestion`;
CREATE TABLE `pollquestion` (
  `Question` varchar(100) COLLATE utf8_turkish_ci NOT NULL,
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `InsertDate` datetime NOT NULL DEFAULT '1990-01-01 00:00:00',
  `InsertUserId` int(11) DEFAULT NULL,
  `UpdateDate` datetime DEFAULT NULL,
  `UpdateUserId` int(11) DEFAULT NULL,
  `Visible` tinyint(1) NOT NULL DEFAULT '1',
  `OrderNo` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=2 ;

--
-- Tablo döküm verisi `pollquestion`
--

INSERT INTO `pollquestion` (`Question`, `Id`, `InsertDate`, `InsertUserId`, `UpdateDate`, `UpdateUserId`, `Visible`, `OrderNo`) VALUES
('En çok hangi moda dergisini takip ediyorsunuz?', 1, '2011-11-04 23:52:23', 1, '2011-11-04 23:52:23', 1, 1, 0);

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı: `pollquestionlang`
--

DROP TABLE IF EXISTS `pollquestionlang`;
CREATE TABLE `pollquestionlang` (
  `PollQuestionId` int(11) NOT NULL,
  `LangId` int(11) NOT NULL,
  `Question` varchar(100) COLLATE utf8_turkish_ci NOT NULL,
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `InsertDate` datetime NOT NULL DEFAULT '1990-01-01 00:00:00',
  `InsertUserId` int(11) DEFAULT NULL,
  `UpdateDate` datetime DEFAULT NULL,
  `UpdateUserId` int(11) DEFAULT NULL,
  `Visible` tinyint(1) NOT NULL DEFAULT '1',
  `OrderNo` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=1 ;

--
-- Tablo döküm verisi `pollquestionlang`
--


-- --------------------------------------------------------

--
-- Tablo için tablo yapısı: `product`
--

DROP TABLE IF EXISTS `product`;
CREATE TABLE `product` (
  `ContentId` int(11) NOT NULL,
  `ListPrice` decimal(10,0) DEFAULT NULL,
  `DiscountRate` decimal(10,0) DEFAULT NULL,
  `VATRate` decimal(10,0) DEFAULT NULL,
  `OtherTaxRate` decimal(10,0) DEFAULT NULL,
  `Point` int(11) DEFAULT NULL,
  `Width` decimal(10,0) DEFAULT NULL,
  `Height` decimal(10,0) DEFAULT NULL,
  `Depth` decimal(10,0) DEFAULT NULL,
  `Weight` decimal(10,0) DEFAULT NULL,
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `InsertDate` datetime NOT NULL DEFAULT '1990-01-01 00:00:00',
  `InsertUserId` int(11) DEFAULT NULL,
  `UpdateDate` datetime DEFAULT NULL,
  `UpdateUserId` int(11) DEFAULT NULL,
  `Visible` tinyint(1) NOT NULL DEFAULT '1',
  `OrderNo` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=1 ;

--
-- Tablo döküm verisi `product`
--


-- --------------------------------------------------------

--
-- Tablo için tablo yapısı: `recommendation`
--

DROP TABLE IF EXISTS `recommendation`;
CREATE TABLE `recommendation` (
  `ContentId` int(11) NOT NULL,
  `NameFrom` varchar(100) COLLATE utf8_turkish_ci NOT NULL,
  `EmailFrom` varchar(100) COLLATE utf8_turkish_ci NOT NULL,
  `NameTo` varchar(100) COLLATE utf8_turkish_ci NOT NULL,
  `EmailTo` varchar(100) COLLATE utf8_turkish_ci NOT NULL,
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `InsertDate` datetime NOT NULL DEFAULT '1990-01-01 00:00:00',
  `InsertUserId` int(11) DEFAULT NULL,
  `UpdateDate` datetime DEFAULT NULL,
  `UpdateUserId` int(11) DEFAULT NULL,
  `Visible` tinyint(1) NOT NULL DEFAULT '1',
  `OrderNo` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=1 ;

--
-- Tablo döküm verisi `recommendation`
--


-- --------------------------------------------------------

--
-- Tablo için tablo yapısı: `source`
--

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
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=2 ;

--
-- Tablo döküm verisi `source`
--

INSERT INTO `source` (`UserId`, `Name`, `Description`, `Picture`, `Picture2`, `Id`, `InsertDate`, `InsertUserId`, `UpdateDate`, `UpdateUserId`, `Visible`, `OrderNo`) VALUES
(NULL, 'Editorial', NULL, NULL, NULL, 1, '1990-01-01 00:00:00', NULL, NULL, NULL, 1, NULL);

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı: `tag`
--

DROP TABLE IF EXISTS `tag`;
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
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=9 ;

--
-- Tablo döküm verisi `tag`
--

INSERT INTO `tag` (`DisplayName`, `Headline`, `ContentCount`, `Noise`, `Name`, `Description`, `Picture`, `Picture2`, `Id`, `InsertDate`, `InsertUserId`, `UpdateDate`, `UpdateUserId`, `Visible`, `OrderNo`) VALUES
(NULL, 0, 3, 0, 'röportaj', NULL, NULL, NULL, 1, '2011-10-28 20:22:17', 1, '2011-11-21 17:23:44', 1, 1, 0),
(NULL, 0, 1, 0, 'fotoğraf çekimi', NULL, NULL, NULL, 2, '2011-10-30 16:35:35', 1, '2011-10-30 16:35:35', 1, 1, 0),
(NULL, 0, 1, 0, 'ordan burdan', NULL, NULL, NULL, 3, '2011-10-30 16:36:42', 1, '2011-10-30 16:36:42', 1, 1, 0),
(NULL, 0, 1, 0, 'selection', NULL, NULL, NULL, 5, '2011-11-02 17:15:27', 1, '2011-11-02 17:15:27', 1, 1, 0),
(NULL, 0, 1, 0, 'fameous', NULL, NULL, NULL, 6, '2011-11-02 17:21:33', 1, '2011-11-02 17:21:33', 1, 1, 0),
(NULL, 0, 1, 0, 'doğu', NULL, NULL, NULL, 7, '2011-11-02 18:37:49', 1, '2011-11-02 18:37:49', 1, 1, 0),
(NULL, 0, 1, 0, 'batı', NULL, NULL, NULL, 8, '2011-11-02 18:38:01', 1, '2011-11-02 18:38:01', 1, 1, 0);

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı: `taglang`
--

DROP TABLE IF EXISTS `taglang`;
CREATE TABLE `taglang` (
  `TagId` int(11) NOT NULL,
  `LangId` int(11) NOT NULL,
  `Name` varchar(200) COLLATE utf8_turkish_ci NOT NULL,
  `Description` varchar(300) COLLATE utf8_turkish_ci DEFAULT NULL,
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=1 ;

--
-- Tablo döküm verisi `taglang`
--


-- --------------------------------------------------------

--
-- Tablo için tablo yapısı: `template`
--

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
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=16 ;

--
-- Tablo döküm verisi `template`
--

INSERT INTO `template` (`FileName`, `HTMLCode`, `Id`, `InsertDate`, `InsertUserId`, `UpdateDate`, `UpdateUserId`, `Visible`, `OrderNo`) VALUES
('Author.aspx', '<html>\n<head>\n$=this.HeadSection$\n<style>\n#ContentLeft {text-align:left; float:left; width:240px; margin-right:10px;}\n</style>\n</head>\n<body>\n    <div id="page">\n        <div id="Header" class="Region Header">$=this.Header$</div>\n\n		<div id="ContentLeft" class="Region ContentLeft">$=this.ContentLeft$</div>\n		<div id="Content" class="Region Content" style="width:734px;float:right">$=this.Content$</div>\n		<div style="clear:both"></div>\n\n        <div id="Footer" class="Region Footer">$=this.Footer$</div>\n    </div>\n\n</body>\n</html>', 1, '1990-01-01 00:00:00', 0, '2011-11-08 21:34:07', 1, 1, 0),
('Default.aspx', '<html>\n<head>\n$=this.HeadSection$\n<script>\ndocument.observe(''dom:loaded'', function(){\n	\\$\\$(''.vitrin .clPubDate'').each(function(elm){\n		elm.previous().innerHTML = ''<div>''+elm.previous().innerHTML+''</div>'';\n		elm.previous().down().insert(elm.remove());\n	});\n});\n</script>\n<style>\n#ContentLeft {text-align:left; float:left; width:240px; margin-right:10px;}\n#orta {text-align:left; float:left; width:490px; min-height:500px;}\n#OrtaLeft {text-align:left; float:left; width:240px;}\n#OrtaRight {text-align:left; float:right; width:240px;}\n#Content2 {text-align:left; float:left; width:250px;}\n#ContentRight {text-align:left; float:right; width:240px;}\n</style>\n</head>\n<body>\n    <div id="page">\n        <div id="Header" class="Region Header">$=this.Header$</div>\n\n		<div id="ContentLeft" class="Region ContentLeft">$=this.ContentLeft$</div>\n		<div id="orta">\n		<div id="Content" class="Region Content">$=this.Content$</div>\n		<div id="OrtaLeft" class="Region OrtaLeft">$=this.OrtaLeft$</div>\n		<div id="OrtaRight" class="Region OrtaRight">$=this.OrtaRight$</div>\n		</div>\n		<div id="ContentRight" class="Region ContentRight">$=this.ContentRight$</div>\n		<div style="clear:both"></div>\n\n        <div id="Footer" class="Region Footer">$=this.Footer$</div>\n    </div>\n</body>\n</html>', 2, '2011-10-28 00:19:25', 1, '2011-11-08 21:33:02', 1, 1, 0),
('AuthorList.aspx', '<html>\n<head>\n$=this.HeadSection$\n<style>\n#ContentLeft {text-align:left; float:left; width:240px; margin-right:10px;}\n</style>\n</head>\n<body>\n    <div id="page">\n        <div id="Header" class="Region Header">$=this.Header$</div>\n\n		<div id="ContentLeft" class="Region ContentLeft">$=this.ContentLeft$</div>\n		<div id="Content" class="Region Content" style="width:734px;float:right">$=this.Content$</div>\n		<div style="clear:both"></div>\n\n        <div id="Footer" class="Region Footer">$=this.Footer$</div>\n    </div>\n</body>\n</html>', 3, '2011-10-30 15:57:13', 1, '2011-11-09 04:01:08', 1, 1, 0),
('Other.aspx', '<html>\n<head>\n$=this.HeadSection$\n</head>\n<body>\n    <div id="page">\n        <div id="Header" class="Region Header">$=this.Header$</div>\n\n		<div id="Content" class="Region Content" style="width:100%">$=this.Content$</div>\n		<div style="clear:both"></div>\n\n        <div id="Footer" class="Region Footer">$=this.Footer$</div>\n    </div>\n</body>\n</html>', 4, '2011-10-30 17:54:53', 1, '2011-10-30 17:56:02', 1, 1, 0),
('Search.aspx', '<html>\n<head>\n$=this.HeadSection$\n</head>\n<body>\n    <div id="page">\n        <div id="Header" class="Region Header">$=this.Header$</div>\n\n		<div id="Content" class="Region Content" style="width:100%">$=this.Content$</div>\n		<div style="clear:both"></div>\n\n        <div id="Footer" class="Region Footer">$=this.Footer$</div>\n    </div>\n</body>\n</html>', 5, '2011-11-01 15:23:49', 1, '2011-11-01 15:23:49', 1, 1, 0),
('City.aspx', '<html>\n<head>\n$=this.HeadSection$\n<script>\ndocument.observe(''dom:loaded'', function(){\n	\\$\\$(''.vitrin .clPubDate'').each(function(elm){\n		elm.previous().innerHTML = ''<div>''+elm.previous().innerHTML+''</div>'';\n		elm.previous().down().insert(elm.remove());\n	});\n});\n</script>\n</head>\n<body>\n    <div id="page">\n        <div id="Header" class="Region Header">$=this.Header$</div>\n\n		<div id="ContentLeft" class="Region ContentLeft" style="width:237px;margin-left:10px;float:left;min-height:600px;">$=this.ContentLeft$</div>\n		<div id="Content" class="Region Content" style="width:237px;margin-left:10px;float:left">$=this.Content$</div>\n		<div id="Content2" class="Region Content2" style="width:237px;margin-left:10px;float:left">$=this.Content2$</div>\n		<div id="ContentRight" class="Region ContentRight" style="width:237px;margin-left:10px;float:right">$=this.ContentRight$</div>\n		<div style="clear:both"></div>\n\n        <div id="Footer" class="Region Footer">$=this.Footer$</div>\n    </div>\n</body>\n</html>', 7, '2011-11-02 15:11:50', 1, '2011-11-22 21:21:10', 1, 1, 0),
('Category.aspx', '<html>\n<head>\n$=this.HeadSection$\n</head>\n<body>\n    <div id="page">\n        <div id="Header" class="Region Header">$=this.Header$</div>\n\n		<div id="ContentLeft" class="Region ContentLeft">$=this.ContentLeft$</div>\n		<div id="Content" class="Region Content" style="width:734px">$=this.Content$</div>\n		<div style="clear:both"></div>\n\n        <div id="Footer" class="Region Footer">$=this.Footer$</div>\n    </div>\n</body>\n</html>', 8, '2011-11-02 17:12:59', 1, '2011-11-02 17:12:59', 1, 1, 0),
('Content.aspx', '<html>\n<head>\n$=this.HeadSection$\n<style>\n#ContentLeft {text-align:left; float:left; width:240px; margin-right:10px;}\n</style>\n</head>\n<body>\n    <div id="page">\n        <div id="Header" class="Region Header">$=this.Header$</div>\n\n		<div id="ContentLeft" class="Region ContentLeft">$=this.ContentLeft$</div>\n		<div id="Content" class="Region Content" style="width:734px;float:right">$=this.Content$</div>\n		<div style="clear:both"></div>\n\n        <div id="Footer" class="Region Footer">$=this.Footer$</div>\n    </div>\n</body>\n</html>', 9, '2011-11-02 17:13:22', 1, '2011-11-08 21:34:50', 1, 1, 0),
('AboutMe.aspx', '<html>\n<head>\n$=this.HeadSection$\n<style>\n#ContentLeft {text-align:left; float:left; width:240px; margin-right:10px;}\n</style>\n</head>\n<body>\n    <div id="page">\n        <div id="Header" class="Region Header">$=this.Header$</div>\n\n		<div id="ContentLeft" class="Region ContentLeft">$=this.ContentLeft$</div>\n		<div id="Content" class="Region Content" style="width:734px;float:right">$=this.Content$</div>\n		<div style="clear:both"></div>\n\n        <div id="Footer" class="Region Footer">$=this.Footer$</div>\n    </div>\n\n</body>\n</html>', 10, '2011-11-09 04:02:56', 1, '2011-11-09 04:02:56', 1, 1, 0),
('Login.aspx', '<html>\n<head>\n$=this.HeadSection$\n</head>\n<body>\n    <div id="page">\n        <div id="Header" class="Region Header">$=this.Header$</div>\n\n		<div id="Content" class="Region Content" style="width:100%">$=this.Content$</div>\n		<div style="clear:both"></div>\n\n        <div id="Footer" class="Region Footer">$=this.Footer$</div>\n    </div>\n</body>\n</html>', 11, '2011-11-09 04:39:51', 1, '2011-11-09 04:39:51', 1, 1, 0),
('Moda.aspx', '<html>\n<head>\n$=this.HeadSection$\n</head>\n<body>\n    <div id="page">\n        <div id="Header" class="Region Header">$=this.Header$</div>\n\n		<div id="Content" class="Region Content" style="width:100%">$=this.Content$</div>\n		<div style="clear:both"></div>\n\n        <div id="Footer" class="Region Footer">$=this.Footer$</div>\n    </div>\n</body>\n</html>', 12, '2011-11-09 05:14:13', 1, '2011-11-09 05:14:13', 1, 1, 0),
('LoveHate.aspx', '<!DOCTYPE html>\n<html>\n<head>\n$=this.HeadSection$\n</head>\n<body>\n    <div id="page">\n        <div id="Header" class="Region Header">$=this.Header$</div>\n\n		<div id="Content" class="Region Content" style="width:100%">$=this.Content$</div>\n		<div style="clear:both"></div>\n\n        <div id="Footer" class="Region Footer">$=this.Footer$</div>\n    </div>\n</body>\n</html>', 13, '2011-11-11 17:42:18', 1, '2011-11-19 20:46:57', 1, 1, 0),
('People.aspx', '<html>\n<head>\n$=this.HeadSection$\n<style>\n#ContentLeft {text-align:left; float:left; width:240px; margin-right:10px;}\n.kalin {\n	margin-top:10px;\n	background:#E1E3E2;\n	padding: 35px 35px 35px 35px;\n}\n</style>\n</head>\n<body>\n    <div id="page">\n        <div id="Header" class="Region Header">$=this.Header$</div>\n\n		<div id="Content" class="Region Content" style="width:990px">\n			<div class="kalin shadow">\n			$=this.Content$\n			</div>\n		</div>\n		<div style="clear:both"></div>\n\n        <div id="Footer" class="Region Footer">$=this.Footer$</div>\n    </div>\n</body>\n</html>', 14, '2011-11-12 11:47:08', 1, '2011-11-21 21:39:42', 1, 1, 0),
('Admin.aspx', '<html>\n<head>\n$=this.HeadSection$\n<script>\ndocument.observe(''dom:loaded'', function(){\n	\\$\\$(''.adminDataList .innerDiv'').each(function(elm){\n		elm.hide();\n		elm.previous().setStyle({background:''#606060''});\n		elm.previous().down(''.add'').hide();\n	});\n	var lastOpenedInnerDiv = getCookie(''lastOpenedInnerDiv'');\n	if(!lastOpenedInnerDiv) lastOpenedInnerDiv = 0;\n	toggleDataList(\\$\\$(''.adminDataList .innerDiv'')[lastOpenedInnerDiv]);\n});\nfunction toggleDataList(elm){\n	if(!elm.hasClassName(''innerDiv''))\n		elm = elm.up(''.adminDataList'').down(''.innerDiv'');\n	\\$\\$(''.adminDataList .innerDiv'').without(elm).each(function(d){\n		d.hide();\n		d.previous().setStyle({background:''#606060''});\n		d.previous().down(''.add'').hide();\n	});\n	if(elm.visible()){\n		elm.hide();\n		elm.previous().setStyle({background:''#606060''});\n		elm.previous().down(''.add'').hide();\n	} else {\n		elm.show();\n		elm.previous().setStyle({background:''#FFA826''});\n		elm.previous().down(''.add'').show();\n		setCookie(''lastOpenedInnerDiv'', \\$\\$(''.adminDataList .innerDiv'').indexOf(elm));\n	}\n}\n</script>\n</head>\n<body>\n    <div id="page">\n        <div id="Header" class="Region Header">$=this.Header$</div>\n\n		<div id="Content" class="Region Content" style="width:100%">$=this.Content$</div>\n		<div style="clear:both"></div>\n\n        <div id="Footer" class="Region Footer">$=this.Footer$</div>\n    </div>\n</body>\n</html>', 15, '2011-11-12 20:58:05', 1, '2011-11-14 15:08:25', 1, 1, 0);

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı: `user`
--

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
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=9 ;

--
-- Tablo döküm verisi `user`
--

INSERT INTO `user` (`Email`, `Password`, `Keyword`, `Nick`, `Roles`, `Name`, `Surname`, `Gender`, `Occupation`, `Company`, `Department`, `PhoneCell`, `PhoneWork`, `PhoneHome`, `AddressLine1`, `AddressLine2`, `City`, `Country`, `ZipCode`, `Web`, `Education`, `Certificates`, `About`, `Avatar`, `RedirectCount`, `Id`, `InsertDate`, `InsertUserId`, `UpdateDate`, `UpdateUserId`, `Visible`, `OrderNo`) VALUES
('root@local', '63A9F0EA7BB98050', 'jhrd74ghe63', 'Admin', 'User,Editor,Designer', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, '1990-01-01 00:00:00', NULL, NULL, NULL, 1, NULL),
('editor', '63A9F0EA7BB98050', 'ge548rhe46e', 'Editör', 'User,Editor', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2, '1990-01-01 00:00:00', NULL, NULL, NULL, 1, NULL),
('anonim', '', '63beyte674hge', 'Anonim', '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 3, '1990-01-01 00:00:00', NULL, NULL, NULL, 1, NULL),
('tanla@blank-magazine.com', '', 'EA43183197BD2AE5', 'tanla', 'User', 'Tanla', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 0, 4, '2011-10-26 20:10:46', 1, '2011-10-26 22:51:44', 1, 1, 0),
('bahar@blank-magazine.com', '', '1224702C19A12CD7', 'bahar', 'User', 'Bahar', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 0, 7, '2011-10-26 21:27:49', 1, '2011-10-26 22:51:36', 1, 1, 0),
('deniz@blank-magazine.com', '', '2C27E24C0E9FC51C', 'deniz', 'User', 'Deniz', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 0, 8, '2011-10-26 22:49:23', 1, '2011-10-26 22:51:28', 1, 1, 0);

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı: `usercomment`
--

DROP TABLE IF EXISTS `usercomment`;
CREATE TABLE `usercomment` (
  `ContentId` int(11) NOT NULL,
  `Nick` varchar(100) COLLATE utf8_turkish_ci NOT NULL,
  `Web` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `Email` varchar(100) COLLATE utf8_turkish_ci DEFAULT NULL,
  `IP` varchar(50) COLLATE utf8_turkish_ci NOT NULL,
  `Title` varchar(200) COLLATE utf8_turkish_ci NOT NULL,
  `CommentText` text COLLATE utf8_turkish_ci NOT NULL,
  `ParentId` int(11) DEFAULT '0',
  `ResponseCount` int(11) NOT NULL DEFAULT '0',
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `InsertDate` datetime NOT NULL DEFAULT '1990-01-01 00:00:00',
  `InsertUserId` int(11) DEFAULT NULL,
  `UpdateDate` datetime DEFAULT NULL,
  `UpdateUserId` int(11) DEFAULT NULL,
  `Visible` tinyint(1) NOT NULL DEFAULT '1',
  `OrderNo` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=1 ;

--
-- Tablo döküm verisi `usercomment`
--

