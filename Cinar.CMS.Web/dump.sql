CREATE TABLE `author`(
	`DisableAutoContent` bool NOT NULL DEFAULT '0',
	`UserId` int NULL,
	`Name` varchar(100) NOT NULL,
	`Description` text NULL,
	`Picture` varchar(100) NULL,
	`Id` int NOT NULL AUTO_INCREMENT PRIMARY KEY,
	`InsertDate` datetime NOT NULL DEFAULT '1990-01-01 00:00:00',
	`InsertUserId` int NULL,
	`UpdateDate` datetime NULL,
	`UpdateUserId` int NULL,
	`Visible` bool NOT NULL DEFAULT '1',
	`OrderNo` int NULL);

insert into `author` (`DisableAutoContent`, `UserId`, `Name`, `Description`, `Picture`, `Id`, `InsertDate`, `InsertUserId`, `UpdateDate`, `UpdateUserId`, `Visible`, `OrderNo`) values ('0', null, 'Editorial', null, null, '1', '1990-01-01 00:00', null, null, null, '1', null);
CREATE TABLE `configuration`(
	`SiteName` varchar(100) NULL,
	`SiteAddress` varchar(100) NULL,
	`SiteDescription` text NULL,
	`SiteKeywords` text NULL,
	`SiteLogo` varchar(100) NULL,
	`SiteIcon` varchar(100) NULL,
	`SessionTimeout` int NULL,
	`BufferOutput` bool NULL,
	`MultiLang` bool NULL,
	`DefaultLang` int NOT NULL,
	`DefaultStyleSheet` text NULL,
	`CountTags` bool NULL,
	`UseHTMLEditor` bool NULL,
	`DefaultDateFormat` varchar(100) NULL,
	`NoPicture` varchar(100) NULL,
	`ThumbQuality` int NULL,
	`ImageUploadMaxWidth` int NULL,
	`AuthEmail` varchar(100) NULL,
	`MailHost` varchar(100) NULL,
	`MailUsername` varchar(100) NULL,
	`MailPassword` varchar(100) NULL,
	`UseCache` varchar(30) NOT NULL DEFAULT 'False',
	`CacheLifeTime` int NULL,
	`MainPage` varchar(100) NULL,
	`CategoryPage` varchar(100) NULL,
	`ContentPage` varchar(100) NULL,
	`LoginPage` varchar(100) NULL,
	`MembershipFormPage` varchar(100) NULL,
	`MembershipProfilePage` varchar(100) NULL,
	`RememberPasswordFormPage` varchar(100) NULL,
	`UserActivationPage` varchar(100) NULL,
	`AdminPage` varchar(100) NULL,
	`Id` int NOT NULL AUTO_INCREMENT PRIMARY KEY,
	`InsertDate` datetime NOT NULL DEFAULT '1990-01-01 00:00:00',
	`InsertUserId` int NULL,
	`UpdateDate` datetime NULL,
	`UpdateUserId` int NULL,
	`Visible` bool NOT NULL DEFAULT '1',
	`OrderNo` int NULL);

insert into `configuration` (`SiteName`, `SiteAddress`, `SiteDescription`, `SiteKeywords`, `SiteLogo`, `SiteIcon`, `SessionTimeout`, `BufferOutput`, `MultiLang`, `DefaultLang`, `DefaultStyleSheet`, `CountTags`, `UseHTMLEditor`, `DefaultDateFormat`, `NoPicture`, `ThumbQuality`, `ImageUploadMaxWidth`, `AuthEmail`, `MailHost`, `MailUsername`, `MailPassword`, `UseCache`, `CacheLifeTime`, `MainPage`, `CategoryPage`, `ContentPage`, `LoginPage`, `MembershipFormPage`, `MembershipProfilePage`, `RememberPasswordFormPage`, `UserActivationPage`, `AdminPage`, `Id`, `InsertDate`, `InsertUserId`, `UpdateDate`, `UpdateUserId`, `Visible`, `OrderNo`) values ('isimsiz.com', 'www.isimsiz.com', 'Sitenin tanımı, bu site ne hakkındadır?', 'sitenizle,ilgili,anahtar,kelimleri,böyle,yazınız', '', '', '120', '1', '0', '1', 'body {\n	font-family:Verdana;\n	font-size:12px;\n	text-align:center;\n}\ntd {\n	font-size:12px;\n}\na {\n	color: #9c9c9c;\n	text-decoration:none\n}\na:hover{\n	color: #0687d5;\n}\n\nDIV {\n	-moz-box-sizing:border-box;\n	box-sizing:border-box;\n	margin:0;\n	padding:0;\n}\n\n#page {\n	width: 1000px; \n	height: 100%; \n	margin-top:10px; \n	margin-right:auto;\n	margin-left:auto;\n	background: white;\n}\n#Header {text-align:center}\n#ContentLeft {text-align:left; float:left; width:245px;}\n#Content {text-align:left; float:left; width:490px;}\n#ContentRight {text-align:left; float:left; width:245px;}\n#Footer {text-align:left; clear:both;}\n\n\n\n', '1', '0', 'dd MMMM yyyy', '', '55', '640', 'info@isimsiz.com', 'mail.isimsiz.com', '', '', 'False', '15', 'Main.aspx', 'Category.aspx', 'Content.aspx', 'Login.aspx', 'Membership.aspx', 'Profile.aspx', 'RememberPassword.aspx', 'Activation.aspx', 'Admin.aspx', '1', '2011-10-26 11:00', '3', '2011-10-26 15:48', '1', '1', '0');
CREATE TABLE `content`(
	`ClassName` varchar(100) NOT NULL DEFAULT 'Content',
	`Title` varchar(200) NOT NULL,
	`Description` text NULL,
	`Keywords` text NULL,
	`Metin` text NULL,
	`Hierarchy` varchar(100) NOT NULL,
	`AuthorId` int NULL,
	`SourceId` int NULL,
	`PublishDate` datetime NOT NULL DEFAULT '1990-01-01 00:00:00',
	`Picture` varchar(100) NULL,
	`Tags` varchar(300) NULL,
	`TagRanks` varchar(300) NULL,
	`CategoryId` int NOT NULL DEFAULT 1,
	`ShowInPage` varchar(100) NULL,
	`ShowContentsInPage` varchar(100) NULL,
	`ShowCategoriesInPage` varchar(100) NULL,
	`IsManset` bool NOT NULL DEFAULT '0',
	`SpotTitle` varchar(200) NULL,
	`ContentSourceId` int NULL,
	`SourceLink` varchar(200) NULL,
	`ViewCount` int NOT NULL DEFAULT 0,
	`CommentCount` int NOT NULL DEFAULT 0,
	`RecommendCount` int NOT NULL DEFAULT 0,
	`Id` int NOT NULL AUTO_INCREMENT PRIMARY KEY,
	`InsertDate` datetime NOT NULL DEFAULT '1990-01-01 00:00:00',
	`InsertUserId` int NULL,
	`UpdateDate` datetime NULL,
	`UpdateUserId` int NULL,
	`Visible` bool NOT NULL DEFAULT '1',
	`OrderNo` int NULL);

insert into `content` (`ClassName`, `Title`, `Description`, `Keywords`, `Metin`, `Hierarchy`, `AuthorId`, `SourceId`, `PublishDate`, `Picture`, `Tags`, `TagRanks`, `CategoryId`, `ShowInPage`, `ShowContentsInPage`, `ShowCategoriesInPage`, `IsManset`, `SpotTitle`, `ContentSourceId`, `SourceLink`, `ViewCount`, `CommentCount`, `RecommendCount`, `Id`, `InsertDate`, `InsertUserId`, `UpdateDate`, `UpdateUserId`, `Visible`, `OrderNo`) values ('Category', 'Kök', null, null, null, '', null, null, '1980-01-01 00:00', null, null, null, '0', null, null, null, '0', null, null, null, '0', '0', '0', '1', '2011-10-26 11:00', null, null, null, '1', null);
insert into `content` (`ClassName`, `Title`, `Description`, `Keywords`, `Metin`, `Hierarchy`, `AuthorId`, `SourceId`, `PublishDate`, `Picture`, `Tags`, `TagRanks`, `CategoryId`, `ShowInPage`, `ShowContentsInPage`, `ShowCategoriesInPage`, `IsManset`, `SpotTitle`, `ContentSourceId`, `SourceLink`, `ViewCount`, `CommentCount`, `RecommendCount`, `Id`, `InsertDate`, `InsertUserId`, `UpdateDate`, `UpdateUserId`, `Visible`, `OrderNo`) values ('Category', 'Foto', '', '', '', '00001', '1', '1', '2011-10-26 17:09', '', '', '', '1', '', '', '', '0', '', '0', '', '0', '0', '0', '2', '2011-10-26 17:18', '1', '2011-10-26 17:18', '1', '1', '0');
insert into `content` (`ClassName`, `Title`, `Description`, `Keywords`, `Metin`, `Hierarchy`, `AuthorId`, `SourceId`, `PublishDate`, `Picture`, `Tags`, `TagRanks`, `CategoryId`, `ShowInPage`, `ShowContentsInPage`, `ShowCategoriesInPage`, `IsManset`, `SpotTitle`, `ContentSourceId`, `SourceLink`, `ViewCount`, `CommentCount`, `RecommendCount`, `Id`, `InsertDate`, `InsertUserId`, `UpdateDate`, `UpdateUserId`, `Visible`, `OrderNo`) values ('Category', 'City', '', '', '', '00001', '1', '1', '2011-10-26 17:18', '', '', '', '1', '', '', '', '0', '', '0', '', '0', '0', '0', '3', '2011-10-26 17:18', '1', '2011-10-26 17:18', '1', '1', '0');
insert into `content` (`ClassName`, `Title`, `Description`, `Keywords`, `Metin`, `Hierarchy`, `AuthorId`, `SourceId`, `PublishDate`, `Picture`, `Tags`, `TagRanks`, `CategoryId`, `ShowInPage`, `ShowContentsInPage`, `ShowCategoriesInPage`, `IsManset`, `SpotTitle`, `ContentSourceId`, `SourceLink`, `ViewCount`, `CommentCount`, `RecommendCount`, `Id`, `InsertDate`, `InsertUserId`, `UpdateDate`, `UpdateUserId`, `Visible`, `OrderNo`) values ('Category', 'Hate', '', '', '', '00001', '1', '1', '2011-10-26 17:18', '', '', '', '1', '', '', '', '0', '', '0', '', '0', '0', '0', '4', '2011-10-26 17:18', '1', '2011-10-26 17:18', '1', '1', '0');
insert into `content` (`ClassName`, `Title`, `Description`, `Keywords`, `Metin`, `Hierarchy`, `AuthorId`, `SourceId`, `PublishDate`, `Picture`, `Tags`, `TagRanks`, `CategoryId`, `ShowInPage`, `ShowContentsInPage`, `ShowCategoriesInPage`, `IsManset`, `SpotTitle`, `ContentSourceId`, `SourceLink`, `ViewCount`, `CommentCount`, `RecommendCount`, `Id`, `InsertDate`, `InsertUserId`, `UpdateDate`, `UpdateUserId`, `Visible`, `OrderNo`) values ('Category', 'Love', '', '', '', '00001', '1', '1', '2011-10-26 17:18', '', '', '', '1', '', '', '', '0', '', '0', '', '0', '0', '0', '5', '2011-10-26 17:18', '1', '2011-10-26 17:18', '1', '1', '0');
insert into `content` (`ClassName`, `Title`, `Description`, `Keywords`, `Metin`, `Hierarchy`, `AuthorId`, `SourceId`, `PublishDate`, `Picture`, `Tags`, `TagRanks`, `CategoryId`, `ShowInPage`, `ShowContentsInPage`, `ShowCategoriesInPage`, `IsManset`, `SpotTitle`, `ContentSourceId`, `SourceLink`, `ViewCount`, `CommentCount`, `RecommendCount`, `Id`, `InsertDate`, `InsertUserId`, `UpdateDate`, `UpdateUserId`, `Visible`, `OrderNo`) values ('Category', 'Moda', '', '', '', '00001', '1', '1', '2011-10-26 17:18', '', '', '', '1', '', '', '', '0', '', '0', '', '0', '0', '0', '6', '2011-10-26 17:19', '1', '2011-10-26 17:19', '1', '1', '0');
insert into `content` (`ClassName`, `Title`, `Description`, `Keywords`, `Metin`, `Hierarchy`, `AuthorId`, `SourceId`, `PublishDate`, `Picture`, `Tags`, `TagRanks`, `CategoryId`, `ShowInPage`, `ShowContentsInPage`, `ShowCategoriesInPage`, `IsManset`, `SpotTitle`, `ContentSourceId`, `SourceLink`, `ViewCount`, `CommentCount`, `RecommendCount`, `Id`, `InsertDate`, `InsertUserId`, `UpdateDate`, `UpdateUserId`, `Visible`, `OrderNo`) values ('Category', 'People', '', '', '', '00001', '1', '1', '2011-10-26 17:19', '', '', '', '1', '', '', '', '0', '', '0', '', '0', '0', '0', '7', '2011-10-26 17:19', '1', '2011-10-26 17:19', '1', '1', '0');
insert into `content` (`ClassName`, `Title`, `Description`, `Keywords`, `Metin`, `Hierarchy`, `AuthorId`, `SourceId`, `PublishDate`, `Picture`, `Tags`, `TagRanks`, `CategoryId`, `ShowInPage`, `ShowContentsInPage`, `ShowCategoriesInPage`, `IsManset`, `SpotTitle`, `ContentSourceId`, `SourceLink`, `ViewCount`, `CommentCount`, `RecommendCount`, `Id`, `InsertDate`, `InsertUserId`, `UpdateDate`, `UpdateUserId`, `Visible`, `OrderNo`) values ('Category', 'Author', '', '', '', '00001', '1', '1', '2011-10-26 17:19', '', '', '', '1', '', '', '', '0', '', '0', '', '0', '0', '0', '8', '2011-10-26 17:19', '1', '2011-10-26 17:19', '1', '1', '0');
insert into `content` (`ClassName`, `Title`, `Description`, `Keywords`, `Metin`, `Hierarchy`, `AuthorId`, `SourceId`, `PublishDate`, `Picture`, `Tags`, `TagRanks`, `CategoryId`, `ShowInPage`, `ShowContentsInPage`, `ShowCategoriesInPage`, `IsManset`, `SpotTitle`, `ContentSourceId`, `SourceLink`, `ViewCount`, `CommentCount`, `RecommendCount`, `Id`, `InsertDate`, `InsertUserId`, `UpdateDate`, `UpdateUserId`, `Visible`, `OrderNo`) values ('Category', 'Bird View', '', '', '', '00001', '1', '1', '2011-10-26 17:19', '', '', '', '1', '', '', '', '0', '', '0', '', '0', '0', '0', '9', '2011-10-26 17:19', '1', '2011-10-26 17:19', '1', '1', '0');
insert into `content` (`ClassName`, `Title`, `Description`, `Keywords`, `Metin`, `Hierarchy`, `AuthorId`, `SourceId`, `PublishDate`, `Picture`, `Tags`, `TagRanks`, `CategoryId`, `ShowInPage`, `ShowContentsInPage`, `ShowCategoriesInPage`, `IsManset`, `SpotTitle`, `ContentSourceId`, `SourceLink`, `ViewCount`, `CommentCount`, `RecommendCount`, `Id`, `InsertDate`, `InsertUserId`, `UpdateDate`, `UpdateUserId`, `Visible`, `OrderNo`) values ('Category', 'Cook', '', '', '', '00001', '1', '1', '2011-10-26 17:19', '', '', '', '1', '', '', '', '0', '', '0', '', '0', '0', '0', '10', '2011-10-26 17:20', '1', '2011-10-26 17:20', '1', '1', '0');
CREATE TABLE `lang`(
	`Code` varchar(100) NOT NULL,
	`Name` varchar(100) NOT NULL,
	`Id` int NOT NULL AUTO_INCREMENT PRIMARY KEY,
	`InsertDate` datetime NOT NULL DEFAULT '1990-01-01 00:00:00',
	`InsertUserId` int NULL,
	`UpdateDate` datetime NULL,
	`UpdateUserId` int NULL,
	`Visible` bool NOT NULL DEFAULT '1',
	`OrderNo` int NULL);

insert into `lang` (`Code`, `Name`, `Id`, `InsertDate`, `InsertUserId`, `UpdateDate`, `UpdateUserId`, `Visible`, `OrderNo`) values ('tr-TR', 'Türkçe', '1', '1990-01-01 00:00', null, null, null, '1', null);
CREATE TABLE `module`(
	`Id` int NOT NULL AUTO_INCREMENT PRIMARY KEY,
	`Template` varchar(20) NOT NULL,
	`Region` varchar(20) NOT NULL,
	`OrderNo` int NOT NULL DEFAULT 0,
	`Name` varchar(30) NOT NULL,
	`CSS` text NULL,
	`Details` text NULL,
	`TopHtml` text NULL,
	`BottomHtml` text NULL,
	`ParentModuleId` int NOT NULL DEFAULT 0,
	`CSSClass` varchar(100) NULL,
	`UseCache` varchar(30) NOT NULL DEFAULT 'Default',
	`CacheLifeTime` int NULL);

insert into `module` (`Id`, `Template`, `Region`, `OrderNo`, `Name`, `CSS`, `Details`, `TopHtml`, `BottomHtml`, `ParentModuleId`, `CSSClass`, `UseCache`, `CacheLifeTime`) values ('1', 'Main.aspx', 'Content', '1', 'LoginForm', null, '<?xml version="1.0" encoding="utf-16"?><LoginForm xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"><Id>1</Id><Template>Login.aspx</Template><Region>Content</Region><OrderNo>1</OrderNo><Name>LoginForm</Name><CSS/></LoginForm>', null, null, '0', null, 'Default', null);
insert into `module` (`Id`, `Template`, `Region`, `OrderNo`, `Name`, `CSS`, `Details`, `TopHtml`, `BottomHtml`, `ParentModuleId`, `CSSClass`, `UseCache`, `CacheLifeTime`) values ('2', 'Main.aspx', 'Header', '0', 'StaticHtml', '#StaticHtml_2 {\n}\n\n#StaticHtml_2 .headerMenu { border-bottom:1px solid black}\n#StaticHtml_2 .tab {\n	border:1px solid black; \n	border-bottom:none;\n	float:right; \n	padding:5px 20px;\n	cursor:hand;\n	margin-left:10px;\n}\n#StaticHtml_2 .search {\n}\n#StaticHtml_2 .subscribe {\n	background:black;\n	color:white;\n}', 'Cinar.CMS.Serialization\nInnerHtml,226,<a href="/Main.aspx"><img src="/UserFiles/Images/design/logo.jpg" border="0"/></a>\n\n<div class="headerMenu">\n<div class="subscribe tab">Subscribe</div>\n<div class="search tab">Search</div>\n<div style="clear:both"></div>\n</div>LangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,1,2Template,9,Main.aspxRegion,6,HeaderOrderNo,1,0Name,10,StaticHtmlCSS,302,#StaticHtml_2 {\n}\n\n#StaticHtml_2 .headerMenu { border-bottom:1px solid black}\n#StaticHtml_2 .tab {\n	border:1px solid black; \n	border-bottom:none;\n	float:right; \n	padding:5px 20px;\n	cursor:hand;\n	margin-left:10px;\n}\n#StaticHtml_2 .search {\n}\n#StaticHtml_2 .subscribe {\n	background:black;\n	color:white;\n}TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', null, null, '0', null, 'Default', null);
insert into `module` (`Id`, `Template`, `Region`, `OrderNo`, `Name`, `CSS`, `Details`, `TopHtml`, `BottomHtml`, `ParentModuleId`, `CSSClass`, `UseCache`, `CacheLifeTime`) values ('3', 'Main.aspx', 'Content', '0', 'ContentListByFilter', '#ContentListByFilter_3 {\n	background: #ECE8DC;\n	padding: 15px;\n	margin: 10px 10px 0 0;\n	float: left;\n}\n', 'Cinar.CMS.Serialization\nFilter,0,HowManyItems,2,10SkipFirst,1,0Random,1,0OrderBy,19,Content.PublishDateAscending,5,FalseShowPictureLeftRight,5,FalseShowCurrentContent,5,FalseShowFirstItemWithPicture,5,FalseWhichPicture,15,Content.PicturePictureWidth,1,0PictureHeight,1,0BulletIcon,0,TitleLength,2,50DescriptionLength,3,150DateFormat,12,dd MMMM yyyyFieldOrder,5,imageCreateLink,4,TrueLinkTarget,0,ShowDescriptionAsLink,5,FalseUseTemplate,0,ForceToUseTemplate,5,FalseMoreLink,0,DisplayAsTable,5,FalseCols,1,1EncloseWithDiv,5,FalseId,1,3Template,9,Main.aspxRegion,7,ContentOrderNo,1,0Name,19,ContentListByFilterCSS,103,#ContentListByFilter_3 {\n	background: #ECE8DC;\n	padding: 15px;\n	margin: 10px 10px 0 0;\n	float: left;\n}\nTopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', null, null, '0', null, 'Default', null);
insert into `module` (`Id`, `Template`, `Region`, `OrderNo`, `Name`, `CSS`, `Details`, `TopHtml`, `BottomHtml`, `ParentModuleId`, `CSSClass`, `UseCache`, `CacheLifeTime`) values ('4', 'Main.aspx', 'Footer', '0', 'StaticHtml', '', 'Cinar.CMS.Serialization\nInnerHtml,0,LangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,1,4Template,9,Main.aspxRegion,6,FooterOrderNo,1,0Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,UseCache,7,DefaultCacheLifeTime,1,0', null, null, '0', null, 'Default', null);
CREATE TABLE `source`(
	`UserId` int NULL,
	`Name` varchar(100) NOT NULL,
	`Description` text NULL,
	`Picture` varchar(100) NULL,
	`Id` int NOT NULL AUTO_INCREMENT PRIMARY KEY,
	`InsertDate` datetime NOT NULL DEFAULT '1990-01-01 00:00:00',
	`InsertUserId` int NULL,
	`UpdateDate` datetime NULL,
	`UpdateUserId` int NULL,
	`Visible` bool NOT NULL DEFAULT '1',
	`OrderNo` int NULL);

insert into `source` (`UserId`, `Name`, `Description`, `Picture`, `Id`, `InsertDate`, `InsertUserId`, `UpdateDate`, `UpdateUserId`, `Visible`, `OrderNo`) values (null, 'Editorial', null, null, '1', '1990-01-01 00:00', null, null, null, '1', null);
CREATE TABLE `template`(
	`FileName` varchar(50) NULL,
	`HTMLCode` text NULL,
	`Id` int NOT NULL AUTO_INCREMENT PRIMARY KEY,
	`InsertDate` datetime NOT NULL DEFAULT '1990-01-01 00:00:00',
	`InsertUserId` int NULL,
	`UpdateDate` datetime NULL,
	`UpdateUserId` int NULL,
	`Visible` bool NOT NULL DEFAULT '1',
	`OrderNo` int NULL);

insert into `template` (`FileName`, `HTMLCode`, `Id`, `InsertDate`, `InsertUserId`, `UpdateDate`, `UpdateUserId`, `Visible`, `OrderNo`) values ('Main.aspx', '<html>\n<head>\n$=this.HeadSection$\n</head>\n<body>\n    <div id="page">\n        <div id="Header" class="Region Header">$=this.Header$</div>\n\n		<div id="ContentLeft" class="Region ContentLeft">$=this.ContentLeft$</div>\n		<div id="Content" class="Region Content">$=this.Content$</div>\n		<div id="ContentRight" class="Region ContentRight">$=this.ContentRight$</div>\n		<div style="clear:both"></div>\n\n        <div id="Footer" class="Region Footer">$=this.Footer$</div>\n    </div>\n</body>\n</html>', '1', '1990-01-01 00:00', '0', '2011-10-26 15:35', '1', '1', '0');
CREATE TABLE `user`(
	`Email` varchar(100) NOT NULL,
	`Password` varchar(16) NOT NULL,
	`Keyword` varchar(16) NULL,
	`Nick` varchar(100) NOT NULL,
	`Roles` varchar(100) NOT NULL,
	`Name` varchar(50) NULL,
	`Surname` varchar(50) NULL,
	`Gender` varchar(50) NULL,
	`Occupation` varchar(50) NULL,
	`Company` varchar(100) NULL,
	`Department` varchar(50) NULL,
	`PhoneCell` varchar(50) NULL,
	`PhoneWork` varchar(50) NULL,
	`PhoneHome` varchar(50) NULL,
	`AddressLine1` varchar(200) NULL,
	`AddressLine2` varchar(200) NULL,
	`City` varchar(50) NULL,
	`Country` varchar(50) NULL,
	`ZipCode` varchar(5) NULL,
	`Web` varchar(150) NULL,
	`Education` varchar(50) NULL,
	`Certificates` varchar(200) NULL,
	`About` varchar(200) NULL,
	`Avatar` varchar(100) NULL,
	`RedirectCount` int NULL,
	`Id` int NOT NULL AUTO_INCREMENT PRIMARY KEY,
	`InsertDate` datetime NOT NULL DEFAULT '1990-01-01 00:00:00',
	`InsertUserId` int NULL,
	`UpdateDate` datetime NULL,
	`UpdateUserId` int NULL,
	`Visible` bool NOT NULL DEFAULT '1',
	`OrderNo` int NULL);

insert into `user` (`Email`, `Password`, `Keyword`, `Nick`, `Roles`, `Name`, `Surname`, `Gender`, `Occupation`, `Company`, `Department`, `PhoneCell`, `PhoneWork`, `PhoneHome`, `AddressLine1`, `AddressLine2`, `City`, `Country`, `ZipCode`, `Web`, `Education`, `Certificates`, `About`, `Avatar`, `RedirectCount`, `Id`, `InsertDate`, `InsertUserId`, `UpdateDate`, `UpdateUserId`, `Visible`, `OrderNo`) values ('root@local', '63A9F0EA7BB98050', 'jhrd74ghe63', 'Admin', 'User,Editor,Designer', null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, '1', '1990-01-01 00:00', null, null, null, '1', null);
insert into `user` (`Email`, `Password`, `Keyword`, `Nick`, `Roles`, `Name`, `Surname`, `Gender`, `Occupation`, `Company`, `Department`, `PhoneCell`, `PhoneWork`, `PhoneHome`, `AddressLine1`, `AddressLine2`, `City`, `Country`, `ZipCode`, `Web`, `Education`, `Certificates`, `About`, `Avatar`, `RedirectCount`, `Id`, `InsertDate`, `InsertUserId`, `UpdateDate`, `UpdateUserId`, `Visible`, `OrderNo`) values ('editor', '63A9F0EA7BB98050', 'ge548rhe46e', 'Editör', 'User,Editor', null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, '2', '1990-01-01 00:00', null, null, null, '1', null);
insert into `user` (`Email`, `Password`, `Keyword`, `Nick`, `Roles`, `Name`, `Surname`, `Gender`, `Occupation`, `Company`, `Department`, `PhoneCell`, `PhoneWork`, `PhoneHome`, `AddressLine1`, `AddressLine2`, `City`, `Country`, `ZipCode`, `Web`, `Education`, `Certificates`, `About`, `Avatar`, `RedirectCount`, `Id`, `InsertDate`, `InsertUserId`, `UpdateDate`, `UpdateUserId`, `Visible`, `OrderNo`) values ('anonim', '', '63beyte674hge', 'Anonim', '', null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, '3', '1990-01-01 00:00', null, null, null, '1', null);
