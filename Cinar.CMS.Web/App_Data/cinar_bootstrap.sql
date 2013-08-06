CREATE TABLE `author`(
	`Title` varchar(100) NULL,
	`DisableAutoContent` bool NOT NULL DEFAULT '0',
	`UserId` int NULL,
	`Name` varchar(100) NOT NULL,
	`Description` text NULL,
	`Picture` varchar(100) NULL,
	`Picture2` varchar(100) NULL,
	`Id` int NOT NULL AUTO_INCREMENT PRIMARY KEY,
	`InsertDate` datetime NOT NULL DEFAULT '1990-01-01 00:00:00',
	`InsertUserId` int NULL,
	`UpdateDate` datetime NULL,
	`UpdateUserId` int NULL,
	`Visible` bool NOT NULL DEFAULT '1',
	`OrderNo` int NULL);

insert into `author` (`Title`, `DisableAutoContent`, `UserId`, `Name`, `Description`, `Picture`, `Picture2`, `Id`, `InsertDate`, `InsertUserId`, `UpdateDate`, `UpdateUserId`, `Visible`, `OrderNo`) values (null, '0', null, 'Editorial', null, null, null, '1', '1990-01-01 00:00', null, null, null, '1', null);
CREATE TABLE `configuration`(
	`SiteName` varchar(100) NULL,
	`SiteAddress` varchar(100) NULL,
	`SiteDescription` text NULL,
	`SiteKeywords` text NULL,
	`SiteLogo` varchar(100) NULL,
	`SiteIcon` varchar(100) NULL,
	`UseExternalLibrary` varchar(30) NOT NULL DEFAULT 'False',
	`SessionTimeout` int NULL,
	`BufferOutput` bool NULL,
	`MultiLang` bool NULL,
	`DefaultLang` int NOT NULL,
	`LogHit` bool NULL,
	`DefaultStyleSheet` text NULL,
	`DefaultJavascript` text NULL,
	`DefaultPageLoadScript` text NULL,
	`Routes` text NULL,
	`CountTags` bool NULL,
	`DefaultDateFormat` varchar(100) NULL,
	`NoPicture` varchar(100) NULL,
	`ThumbQuality` int NULL,
	`ImageUploadMaxWidth` int NULL,
	`AuthEmail` varchar(100) NULL,
	`MailHost` varchar(100) NULL,
	`MailPort` int NULL,
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

insert into `configuration` (`SiteName`, `SiteAddress`, `SiteDescription`, `SiteKeywords`, `SiteLogo`, `SiteIcon`, `UseExternalLibrary`, `SessionTimeout`, `BufferOutput`, `MultiLang`, `DefaultLang`, `LogHit`, `DefaultStyleSheet`, `DefaultJavascript`, `DefaultPageLoadScript`, `Routes`, `CountTags`, `DefaultDateFormat`, `NoPicture`, `ThumbQuality`, `ImageUploadMaxWidth`, `AuthEmail`, `MailHost`, `MailPort`, `MailUsername`, `MailPassword`, `UseCache`, `CacheLifeTime`, `MainPage`, `CategoryPage`, `ContentPage`, `LoginPage`, `MembershipFormPage`, `MembershipProfilePage`, `RememberPasswordFormPage`, `UserActivationPage`, `AdminPage`, `Id`, `InsertDate`, `InsertUserId`, `UpdateDate`, `UpdateUserId`, `Visible`, `OrderNo`) values ('Merhaba Dünya', 'www.example.com', 'This is sample web site developed with Cinar CMS', 'cinar, cms, for developers, dotnet, html, javascript, bootstrap, jquery', '', '', 'Bootstrap + jQuery', '120', '1', '1', '1', '0', '/* Move down content because we have a fixed navbar that is 50px tall */\r\nbody {\r\n  padding-top: 50px;\r\n  padding-bottom: 20px;\r\n}\r\n\r\n/* Set widths on the navbar form inputs since otherwise they''re 100% wide */\r\n.navbar-form input[type="text"],\r\n.navbar-form input[type="password"] {\r\n  width: 93px;\r\n}\r\n\r\n.navbar-form {\r\n    margin-top: 13px;\r\n}\r\n\r\n.navbar-form .form-control {\r\n    height: 22px;\r\n}\r\n\r\n.navbar .btn-small {\r\n    padding: 1px 10px;\r\n}\r\n\r\n/* Wrapping element */\r\n/* Set some basic padding to keep content from hitting the edges */\r\n#content {\r\n  padding-left: 15px;\r\n  padding-right: 15px;\r\n}\r\n\r\n/* Responsive: Portrait tablets and up */\r\n@media screen and (min-width: 768px) {\r\n  /* Let the jumbotron breathe */\r\n  .jumbotron {\r\n    margin-top: 20px;\r\n  }\r\n  /* Remove padding from wrapping element since we kick in the grid classes here */\r\n  #content {\r\n    padding: 0;\r\n  }\r\n}', 'jQuery(''.dropdown-menu input, .dropdown-menu label'').click(function(e) {\r\n    e.stopPropagation();\r\n});', '', '', '1', 'dd MMMM yyyy', '', '90', '960', 'info@example.com', 'mail.example.com', '25', '', '', 'False', '15', 'Default.aspx', 'Category.aspx', 'Content.aspx', 'Login.aspx', 'Membership.aspx', 'Profile.aspx', 'RememberPassword.aspx', 'Activation.aspx', 'Admin.aspx', '1', '2013-08-05 22:29', '2', '2013-08-06 18:04', '1', '1', '0');
CREATE TABLE `content`(
	`Keyword` varchar(100) NULL,
	`ClassName` varchar(100) NOT NULL DEFAULT 'Content',
	`CategoryId` int NOT NULL DEFAULT 1,
	`Title` varchar(200) NOT NULL,
	`Description` text NULL,
	`Metin` text NULL,
	`PublishDate` datetime NOT NULL DEFAULT '1990-01-01 00:00:00',
	`Picture` varchar(100) NULL,
	`Picture2` varchar(100) NULL,
	`Keywords` text NULL,
	`Hierarchy` varchar(100) NOT NULL,
	`AuthorId` int NULL,
	`SourceId` int NULL,
	`Tags` varchar(300) NULL,
	`TagRanks` varchar(300) NULL,
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
	`LikeIt` int NOT NULL DEFAULT 0,
	`Id` int NOT NULL AUTO_INCREMENT PRIMARY KEY,
	`InsertDate` datetime NOT NULL DEFAULT '1990-01-01 00:00:00',
	`InsertUserId` int NULL,
	`UpdateDate` datetime NULL,
	`UpdateUserId` int NULL,
	`Visible` bool NOT NULL DEFAULT '1',
	`OrderNo` int NULL);

insert into `content` (`Keyword`, `ClassName`, `CategoryId`, `Title`, `Description`, `Metin`, `PublishDate`, `Picture`, `Picture2`, `Keywords`, `Hierarchy`, `AuthorId`, `SourceId`, `Tags`, `TagRanks`, `ShowInPage`, `ShowContentsInPage`, `ShowCategoriesInPage`, `IsManset`, `SpotTitle`, `ContentSourceId`, `SourceLink`, `ViewCount`, `CommentCount`, `RecommendCount`, `LikeIt`, `Id`, `InsertDate`, `InsertUserId`, `UpdateDate`, `UpdateUserId`, `Visible`, `OrderNo`) values (null, 'Category', '0', 'Kök', null, null, '1990-01-01 00:00', null, null, null, '', null, null, null, null, null, null, null, '0', null, null, null, '0', '0', '0', '0', '1', '1990-01-01 00:00', null, null, null, '1', null);
insert into `content` (`Keyword`, `ClassName`, `CategoryId`, `Title`, `Description`, `Metin`, `PublishDate`, `Picture`, `Picture2`, `Keywords`, `Hierarchy`, `AuthorId`, `SourceId`, `Tags`, `TagRanks`, `ShowInPage`, `ShowContentsInPage`, `ShowCategoriesInPage`, `IsManset`, `SpotTitle`, `ContentSourceId`, `SourceLink`, `ViewCount`, `CommentCount`, `RecommendCount`, `LikeIt`, `Id`, `InsertDate`, `InsertUserId`, `UpdateDate`, `UpdateUserId`, `Visible`, `OrderNo`) values ('ba4f14c8-6219-4894-ab3b-3d5e728b6ddc', 'Category', '1', 'Ürünler', '', '', '2006-08-03 01:32', '', '', '', '00001', '1', '1', '', '', '', '', '', '0', '', '0', '', '0', '0', '0', '0', '2', '2013-08-06 01:33', '1', '2013-08-06 01:33', '1', '1', '0');
insert into `content` (`Keyword`, `ClassName`, `CategoryId`, `Title`, `Description`, `Metin`, `PublishDate`, `Picture`, `Picture2`, `Keywords`, `Hierarchy`, `AuthorId`, `SourceId`, `Tags`, `TagRanks`, `ShowInPage`, `ShowContentsInPage`, `ShowCategoriesInPage`, `IsManset`, `SpotTitle`, `ContentSourceId`, `SourceLink`, `ViewCount`, `CommentCount`, `RecommendCount`, `LikeIt`, `Id`, `InsertDate`, `InsertUserId`, `UpdateDate`, `UpdateUserId`, `Visible`, `OrderNo`) values ('bab0ac25-185b-40d9-9446-ed849373cdb3', 'Category', '1', 'Hizmetler', '', '', '2006-08-03 01:33', '', '', '', '00001', '1', '1', '', '', '', '', '', '0', '', '0', '', '0', '0', '0', '0', '3', '2013-08-06 01:34', '1', '2013-08-06 01:34', '1', '1', '0');
insert into `content` (`Keyword`, `ClassName`, `CategoryId`, `Title`, `Description`, `Metin`, `PublishDate`, `Picture`, `Picture2`, `Keywords`, `Hierarchy`, `AuthorId`, `SourceId`, `Tags`, `TagRanks`, `ShowInPage`, `ShowContentsInPage`, `ShowCategoriesInPage`, `IsManset`, `SpotTitle`, `ContentSourceId`, `SourceLink`, `ViewCount`, `CommentCount`, `RecommendCount`, `LikeIt`, `Id`, `InsertDate`, `InsertUserId`, `UpdateDate`, `UpdateUserId`, `Visible`, `OrderNo`) values ('4fbfa145-9ad9-4c57-b493-fcb350a31b4d', 'Content', '1', 'Hakkımızda', '', '', '2003-08-27 01:34', '', '', '', '00001', '1', '1', '', '', '', '', '', '0', '', '0', '', '0', '0', '0', '0', '4', '2013-08-06 01:34', '1', '2013-08-06 03:00', '1', '1', '0');
insert into `content` (`Keyword`, `ClassName`, `CategoryId`, `Title`, `Description`, `Metin`, `PublishDate`, `Picture`, `Picture2`, `Keywords`, `Hierarchy`, `AuthorId`, `SourceId`, `Tags`, `TagRanks`, `ShowInPage`, `ShowContentsInPage`, `ShowCategoriesInPage`, `IsManset`, `SpotTitle`, `ContentSourceId`, `SourceLink`, `ViewCount`, `CommentCount`, `RecommendCount`, `LikeIt`, `Id`, `InsertDate`, `InsertUserId`, `UpdateDate`, `UpdateUserId`, `Visible`, `OrderNo`) values ('5a622fd5-6d65-450c-98a7-7ae72b7f1487', 'Content', '1', 'İletişim', '', '', '2003-08-27 01:34', '', '', '', '00001', '1', '1', '', '', '', '', '', '0', '', '0', '', '0', '0', '0', '0', '5', '2013-08-06 01:34', '1', '2013-08-06 03:00', '1', '1', '0');
insert into `content` (`Keyword`, `ClassName`, `CategoryId`, `Title`, `Description`, `Metin`, `PublishDate`, `Picture`, `Picture2`, `Keywords`, `Hierarchy`, `AuthorId`, `SourceId`, `Tags`, `TagRanks`, `ShowInPage`, `ShowContentsInPage`, `ShowCategoriesInPage`, `IsManset`, `SpotTitle`, `ContentSourceId`, `SourceLink`, `ViewCount`, `CommentCount`, `RecommendCount`, `LikeIt`, `Id`, `InsertDate`, `InsertUserId`, `UpdateDate`, `UpdateUserId`, `Visible`, `OrderNo`) values ('13a2d432-bd84-4b90-9500-8fb410ddc889', 'Content', '2', 'Ürün 1', '', '', '2003-08-27 17:44', '', '', '', '00001,00002', '1', '1', '', '', '', '', '', '0', '', '0', '', '0', '0', '0', '0', '6', '2013-08-06 17:44', '1', '2013-08-06 17:44', '1', '1', '0');
insert into `content` (`Keyword`, `ClassName`, `CategoryId`, `Title`, `Description`, `Metin`, `PublishDate`, `Picture`, `Picture2`, `Keywords`, `Hierarchy`, `AuthorId`, `SourceId`, `Tags`, `TagRanks`, `ShowInPage`, `ShowContentsInPage`, `ShowCategoriesInPage`, `IsManset`, `SpotTitle`, `ContentSourceId`, `SourceLink`, `ViewCount`, `CommentCount`, `RecommendCount`, `LikeIt`, `Id`, `InsertDate`, `InsertUserId`, `UpdateDate`, `UpdateUserId`, `Visible`, `OrderNo`) values ('0cca40c8-9a31-4f53-8671-6826c1741405', 'Content', '2', 'Ürün 2', '', '', '2006-08-03 17:44', '', '', '', '00001,00002', '1', '1', '', '', '', '', '', '0', '', '0', '', '0', '0', '0', '0', '7', '2013-08-06 17:45', '1', '2013-08-06 17:45', '1', '1', '0');
insert into `content` (`Keyword`, `ClassName`, `CategoryId`, `Title`, `Description`, `Metin`, `PublishDate`, `Picture`, `Picture2`, `Keywords`, `Hierarchy`, `AuthorId`, `SourceId`, `Tags`, `TagRanks`, `ShowInPage`, `ShowContentsInPage`, `ShowCategoriesInPage`, `IsManset`, `SpotTitle`, `ContentSourceId`, `SourceLink`, `ViewCount`, `CommentCount`, `RecommendCount`, `LikeIt`, `Id`, `InsertDate`, `InsertUserId`, `UpdateDate`, `UpdateUserId`, `Visible`, `OrderNo`) values ('72966369-d084-4be1-b50d-3749bb9c4654', 'Content', '3', 'Hizmet 1', '', '', '2006-08-03 17:45', '', '', '', '00001,00003', '1', '1', '', '', '', '', '', '0', '', '0', '', '0', '0', '0', '0', '8', '2013-08-06 17:45', '1', '2013-08-06 17:45', '1', '1', '0');
insert into `content` (`Keyword`, `ClassName`, `CategoryId`, `Title`, `Description`, `Metin`, `PublishDate`, `Picture`, `Picture2`, `Keywords`, `Hierarchy`, `AuthorId`, `SourceId`, `Tags`, `TagRanks`, `ShowInPage`, `ShowContentsInPage`, `ShowCategoriesInPage`, `IsManset`, `SpotTitle`, `ContentSourceId`, `SourceLink`, `ViewCount`, `CommentCount`, `RecommendCount`, `LikeIt`, `Id`, `InsertDate`, `InsertUserId`, `UpdateDate`, `UpdateUserId`, `Visible`, `OrderNo`) values ('a524cde2-fff8-453d-aa6c-87e9581d95d6', 'Content', '3', 'Hizmet 2', '', '', '2006-08-03 17:45', '', '', '', '00001,00003', '1', '1', '', '', '', '', '', '0', '', '0', '', '0', '0', '0', '0', '9', '2013-08-06 17:45', '1', '2013-08-06 17:45', '1', '1', '0');
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
	`Template` varchar(50) NOT NULL,
	`Region` varchar(50) NOT NULL,
	`OrderNo` int NOT NULL DEFAULT 0,
	`Name` varchar(30) NOT NULL,
	`CSS` text NULL,
	`Details` text NULL,
	`TopHtml` text NULL,
	`BottomHtml` text NULL,
	`ParentModuleId` int NOT NULL DEFAULT 0,
	`CSSClass` varchar(100) NULL,
	`Visible` bool NULL,
	`RoleToRead` varchar(100) NULL,
	`UseCache` varchar(30) NOT NULL DEFAULT 'Default',
	`CacheLifeTime` int NULL);

insert into `module` (`Id`, `Template`, `Region`, `OrderNo`, `Name`, `CSS`, `Details`, `TopHtml`, `BottomHtml`, `ParentModuleId`, `CSSClass`, `Visible`, `RoleToRead`, `UseCache`, `CacheLifeTime`) values ('1', 'Default.aspx', 'content', '0', 'StaticHtml', '', 'Cinar.CMS.Serialization\nInnerHtml,1126,  <div class="col-lg-4">\n    <h2>Başlık 1</h2>\n    <p>Donec id elit non mi porta gravida at eget metus. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus. Etiam porta sem malesuada magna mollis euismod. Donec sed odio dui. </p>\n    <p><a class="btn btn-default" href="#">View details &raquo;</a></p>\n  </div>\n  <div class="col-lg-4">\n    <h2>Başlık 2</h2>\n    <p>Donec id elit non mi porta gravida at eget metus. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus. Etiam porta sem malesuada magna mollis euismod. Donec sed odio dui. </p>\n    <p><a class="btn btn-default" href="#">View details &raquo;</a></p>\n </div>\n  <div class="col-lg-4">\n    <h2>Başlık 3</h2>\n    <p>Donec sed odio dui. Cras justo odio, dapibus ac facilisis in, egestas eget quam. Vestibulum id ligula porta felis euismod semper. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus.</p>\n    <p><a class="btn btn-default" href="#">View details &raquo;</a></p>\n  </div>\nLangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,1,1Template,12,Default.aspxRegion,7,contentOrderNo,1,0Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,3,rowVisible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0', null, null, '0', null, null, null, 'Default', null);
insert into `module` (`Id`, `Template`, `Region`, `OrderNo`, `Name`, `CSS`, `Details`, `TopHtml`, `BottomHtml`, `ParentModuleId`, `CSSClass`, `Visible`, `RoleToRead`, `UseCache`, `CacheLifeTime`) values ('2', 'Default.aspx', 'footer', '0', 'StaticHtml', '', 'Cinar.CMS.Serialization\nInnerHtml,59,<hr/>\n<p>&copy; $=Provider.Configuration.SiteName$ 2013</p>LangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,1,2Template,12,Default.aspxRegion,6,footerOrderNo,1,0Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0', null, null, '0', null, null, null, 'Default', null);
insert into `module` (`Id`, `Template`, `Region`, `OrderNo`, `Name`, `CSS`, `Details`, `TopHtml`, `BottomHtml`, `ParentModuleId`, `CSSClass`, `Visible`, `RoleToRead`, `UseCache`, `CacheLifeTime`) values ('3', 'Default.aspx', 'topNav', '0', 'StaticHtml', '', 'Cinar.CMS.Serialization\nInnerHtml,2920,<button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".nav-collapse">\n    <span class="icon-bar"></span>\n    <span class="icon-bar"></span>\n    <span class="icon-bar"></span>\n</button>\n<a class="navbar-brand" href="#">$=Provider.Configuration.SiteName$</a>\n<div class="nav-collapse collapse">\n    <ul class="nav navbar-nav">\n        <li class="active"><a href="/">Home</a></li>\n$\nforeach(var cat in Provider.Content.Root.Contents){\n    if(cat.ClassName=="Category"){\n        echo(''<li class="dropdown"><a href="''+cat.GetPageLinkWithTitle('''')+''" class="dropdown-toggle" data-toggle="dropdown">''+cat.Title+'' <b class="caret"></b></a>'');\n        echo(''<ul class="dropdown-menu">'');\n        foreach(var subCat in cat.Contents)\n            echo(''<li><a href="''+subCat.GetPageLinkWithTitle('''')+''">''+subCat.Title+''</a></li>'');\n        echo(''</ul></li>'');\n    }\n    else \n        echo(''<li><a href="''+cat.GetPageLinkWithTitle('''')+''">''+cat.Title+''</a></li>'');\n}\n$\n    </ul>\n$\nif(Provider.Configuration.MultiLang){\n$\n    <div class="btn-group pull-right" style="margin-left:7px">\n        <button type="button" class="btn btn-default btn-small dropdown-toggle" data-toggle="dropdown" style="margin-top:13px">\n            Dil <b class="caret"></b>\n        </button>\n        <ul class="dropdown-menu">\n            <li><a href="#">Türkçe</a></li>\n            <li><a href="#">English</a></li>\n        </ul>\n    </div>\n$\n}\n\nif (Provider.User.IsAnonim())\n{\n$\n    <form class="navbar-form form-inline pull-right" method="post" action="DoLogin.ashx">\n        <input type="hidden" name="RedirectURL" value="$=Provider.Request.RawUrl$"/>\n        <input type="text" name="Email" placeholder="Email" class="form-control">\n        <input type="password" name="Passwd" placeholder="Şifre" class="form-control">\n        <button type="submit" class="btn btn-primary btn-small">Giriş</button>\n    </form>\n$\n} else {\n$\n    <div class="btn-group pull-right">\n        <button type="button" class="btn btn-success btn-small dropdown-toggle" data-toggle="dropdown" style="margin-top:13px">\n            $=Provider.User.FullName$ <b class="caret"></b>\n        </button>\n        <ul class="dropdown-menu">\n            <li><a href="$=Provider.Configuration.MembershipProfilePage$">Profil</a></li>\n            $ if(Provider.User.IsInRole(''Designer'')){ $ <li><a href="?DesignMode=$= (Provider.DesignMode?''Off'':''On'') $">$= (Provider.DesignMode?''İzleme Modu'':''Tasarım Modu'') $ </a></li>$ } $\n            $ if(Provider.User.IsInRole(''Editor'')){ $ <li><a href="$=Provider.Configuration.AdminPage$">Site Yönetimi</a></li>$ } $\n            <li class="divider"></li>\n            <li><a href="/DoLogin.ashx?logout=1">Oturumu Kapat</a></li>\n        </ul>            \n    </div>        \n    <img src="$=Provider.GetThumbPath(Provider.User.Avatar,0,32,false)$" class="img-circle pull-right" style="height: 32px;margin: 9px 10px;"/>\n$\n}\n$\n</div>LangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,1,3Template,12,Default.aspxRegion,6,topNavOrderNo,1,0Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0', null, null, '0', null, null, null, 'Default', null);
insert into `module` (`Id`, `Template`, `Region`, `OrderNo`, `Name`, `CSS`, `Details`, `TopHtml`, `BottomHtml`, `ParentModuleId`, `CSSClass`, `Visible`, `RoleToRead`, `UseCache`, `CacheLifeTime`) values ('4', 'Default.aspx', 'jumbo', '0', 'StaticHtml', '', 'Cinar.CMS.Serialization\nInnerHtml,315,<h1>Merhaba, dünya!</h1>\n<p>This is a template for a simple marketing or informational website. It includes a large callout called the hero unit and three supporting pieces of content. Use it as a starting point to create something more unique.</p>\n<p><a class="btn btn-primary btn-large">Learn more &raquo;</a></p>LangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,1,4Template,12,Default.aspxRegion,5,jumboOrderNo,1,0Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,9,jumbotronVisible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0', null, null, '0', null, null, null, 'Default', null);
CREATE TABLE `source`(
	`UserId` int NULL,
	`Name` varchar(100) NOT NULL,
	`Description` text NULL,
	`Picture` varchar(100) NULL,
	`Picture2` varchar(100) NULL,
	`Id` int NOT NULL AUTO_INCREMENT PRIMARY KEY,
	`InsertDate` datetime NOT NULL DEFAULT '1990-01-01 00:00:00',
	`InsertUserId` int NULL,
	`UpdateDate` datetime NULL,
	`UpdateUserId` int NULL,
	`Visible` bool NOT NULL DEFAULT '1',
	`OrderNo` int NULL);

insert into `source` (`UserId`, `Name`, `Description`, `Picture`, `Picture2`, `Id`, `InsertDate`, `InsertUserId`, `UpdateDate`, `UpdateUserId`, `Visible`, `OrderNo`) values (null, 'Editorial', null, null, null, '1', '1990-01-01 00:00', null, null, null, '1', null);
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

insert into `template` (`FileName`, `HTMLCode`, `Id`, `InsertDate`, `InsertUserId`, `UpdateDate`, `UpdateUserId`, `Visible`, `OrderNo`) values ('Default.aspx', '<!DOCTYPE html>\r\n<html>\r\n<head>\r\n$=this.HeadSection$\r\n</head>\r\n<body>\r\n    <div class="navbar navbar-inverse navbar-fixed-top">\r\n      <div id="topNav" class="Region container">$=this.topNav$</div>\r\n    </div>\r\n    \r\n    <div id="jumbo" class="Region container">\r\n        $=this.jumbo$\r\n    </div>\r\n    \r\n    <div id="content" class="Region container">\r\n        $=this.content$\r\n    </div>\r\n    \r\n    <footer id="footer" class="Region container">\r\n        $=this.footer$\r\n    </footer>\r\n</body>\r\n</html>', '1', '1990-01-01 00:00', null, null, null, '1', null);
CREATE TABLE `user`(
	`Email` varchar(100) NOT NULL,
	`Password` varchar(16) NOT NULL,
	`Keyword` varchar(16) NULL,
	`Nick` varchar(100) NULL,
	`Roles` varchar(100) NOT NULL,
	`Name` varchar(50) NULL,
	`Surname` varchar(50) NULL,
	`Gender` varchar(50) NULL,
	`BirthDate` datetime NULL,
	`IdentityNumber` varchar(100) NULL,
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
CREATE INDEX `UNQ_User_Email` ON `user` (`Email`);

insert into `user` (`Email`, `Password`, `Keyword`, `Nick`, `Roles`, `Name`, `Surname`, `Gender`, `BirthDate`, `IdentityNumber`, `Occupation`, `Company`, `Department`, `PhoneCell`, `PhoneWork`, `PhoneHome`, `AddressLine1`, `AddressLine2`, `City`, `Country`, `ZipCode`, `Web`, `Education`, `Certificates`, `About`, `Avatar`, `RedirectCount`, `Id`, `InsertDate`, `InsertUserId`, `UpdateDate`, `UpdateUserId`, `Visible`, `OrderNo`) values ('root@local', '63A9F0EA7BB98050', 'jhrd74ghe63', 'admin', 'User,Editor,Designer', 'Root', 'User', null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, '1', '1990-01-01 00:00', null, null, null, '1', null);
insert into `user` (`Email`, `Password`, `Keyword`, `Nick`, `Roles`, `Name`, `Surname`, `Gender`, `BirthDate`, `IdentityNumber`, `Occupation`, `Company`, `Department`, `PhoneCell`, `PhoneWork`, `PhoneHome`, `AddressLine1`, `AddressLine2`, `City`, `Country`, `ZipCode`, `Web`, `Education`, `Certificates`, `About`, `Avatar`, `RedirectCount`, `Id`, `InsertDate`, `InsertUserId`, `UpdateDate`, `UpdateUserId`, `Visible`, `OrderNo`) values ('anonim', '', '63beyte674hge', 'anonim', '', 'Anonim', 'User', null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, '2', '1990-01-01 00:00', null, null, null, '1', null);
insert into `user` (`Email`, `Password`, `Keyword`, `Nick`, `Roles`, `Name`, `Surname`, `Gender`, `BirthDate`, `IdentityNumber`, `Occupation`, `Company`, `Department`, `PhoneCell`, `PhoneWork`, `PhoneHome`, `AddressLine1`, `AddressLine2`, `City`, `Country`, `ZipCode`, `Web`, `Education`, `Certificates`, `About`, `Avatar`, `RedirectCount`, `Id`, `InsertDate`, `InsertUserId`, `UpdateDate`, `UpdateUserId`, `Visible`, `OrderNo`) values ('editor', '63A9F0EA7BB98050', 'ge548rhe46e', 'editor', 'User,Editor', 'Editor', 'User', null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, '3', '1990-01-01 00:00', null, null, null, '1', null);
