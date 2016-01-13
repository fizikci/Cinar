<%@ Page Language="C#" %>
<%@ Import Namespace="Cinar.CMS.Library" %>
<%@ Import Namespace="Cinar.CMS.Library.Entities" %>
<script runat="server">
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        
        var joining = Request["ecr"].IsEmpty();
        var id = 0;
        int.TryParse(Request["ecr"].IsEmpty() ? Request["ecj"] : Request["ecr"], out id);
        
        if(id>0) {
            EventContact ec = Provider.Database.Read<EventContact>(id);
            if (ec != null) {
                ec.State = joining ? EventContactStates.Joining : EventContactStates.Rejected;
                ec.Save();
            }
        }
    }
</script>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />

        <link href="/UserFiles/assets/css/bootstrap.min.css" rel="stylesheet" />
		<link rel="stylesheet" href="/UserFiles/assets/css/font-awesome.min.css" />

		<!--[if IE 7]>
		  <link rel="stylesheet" href="/UserFiles/assets/css/font-awesome-ie7.min.css" />
		<![endif]-->

		<link rel="stylesheet" href="/UserFiles/assets/css/ace-fonts.css" />
		<link rel="stylesheet" href="/UserFiles/assets/css/ace.min.css" />
		<link rel="stylesheet" href="/UserFiles/assets/css/ace-rtl.min.css" />

		<!--[if lte IE 8]>
		  <link rel="stylesheet" href="/UserFiles/assets/css/ace-ie.min.css" />
		<![endif]-->

		<!--[if lt IE 9]>
		<script src="/UserFiles/assets/js/html5shiv.js"></script>
		<script src="/UserFiles/assets/js/respond.min.js"></script>
		<![endif]-->

		<!--[if !IE]> -->

		<script type="text/javascript">
		    window.jQuery || document.write("<script src='/UserFiles/assets/js/jquery-2.0.3.min.js'>" + "<" + "/script>");
		</script>

		<!-- <![endif]-->

		<!--[if IE]>
        <script type="text/javascript">
         window.jQuery || document.write("<script src='/UserFiles/assets/js/jquery-1.10.2.min.js'>"+"<"+"/script>");
        </script>
        <![endif]-->

		<script type="text/javascript">
		    if ("ontouchend" in document) document.write("<script src='/UserFiles/assets/js/jquery.mobile.custom.min.js'>" + "<" + "/script>");
		</script>

		<script type="text/javascript">
		    function show_box(id) {
		        jQuery('.widget-box.visible').removeClass('visible');
		        jQuery('#' + id).addClass('visible');
		    }
		</script>

        <title>Hazar Events</title>
<meta name="description" content="Hazar Strategy Institute"/>
<meta name="keywords" content=" ,cinar, cms, for developers, dotnet, html, javascript, bootstrap, jquery"/>
<meta name="viewport" content="width=device-width"/>
<META HTTP-EQUIV="Content-Type" CONTENT="text/html; charset=utf-8"/>
<META HTTP-EQUIV="Content-Language" CONTENT="TR"/>
<meta property="og:title" content="Hazar Events"/>
<meta property="og:image" content=""/>
<meta property="og:site_name" content="Hazar Events"/>
<meta property="og:description" content="Hazar Strategy Institute"/>
<link href="/UserFiles/site.ico" rel="SHORTCUT ICON"/>
<link href="/RSS.ashx?item=1" rel="alternate" title="Hazar Events" type="application/rss+xml" />
<link href="/_thumbs/default.css" rel="stylesheet" type="text/css"/>
<link href="/_thumbs/cinar.cms.css" rel="stylesheet" type="text/css"/>
<link href="/_thumbs/famfamfam.css" rel="stylesheet" type="text/css"/>
<link href="/external/themes/default.css" rel="stylesheet" type="text/css"/>
<link href="/external/themes/alphacube.css" rel="stylesheet" type="text/css"/>
<link href="/_thumbs/DefaultStyleSheet.css" rel="stylesheet" type="text/css"/>
<style title="moduleStyles">


</style>
<script type='text/javascript'>var designMode = false, defaultLangId = 1; currLang = 'en'; isDesigner = false;</script>
<script type="text/javascript" src="/external/javascripts/prototype.js"></script>
<script type="text/javascript" src="/_thumbs/default.js"></script>
<script type="text/javascript" src="/_thumbs/message.js"> </script>
<script type="text/javascript" src="/_thumbs/en.js"></script>
<script type="text/javascript" src="/external/javascripts/window.js"></script>
<script type="text/javascript" src="/_thumbs/DefaultJavascript.js"></script>

</head>
<body class="login-layout">
        <script>
            (function (i, s, o, g, r, a, m) {
                i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                    (i[r].q = i[r].q || []).push(arguments)
                }, i[r].l = 1 * new Date(); a = s.createElement(o),
                m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
            })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

            ga('create', 'UA-71136000-1', 'auto');
            ga('send', 'pageview');

        </script>
		<div class="main-container">
			<div class="main-content">
				<div class="row">
					<div class="col-sm-10 col-sm-offset-1">
						<div class="login-container">
							<div class="center">

								<h1>
									<img id="stv_logo" src="/UserFiles/_design/logo.png" width="64" /><br/>
									<span class="white">Hazar Events</span>
								</h1>
								<h4 class="blue">&copy; Hazar Strategy Institute</h4>
							</div>

							<div class="space-6"></div>

							<div class="position-relative">
								<div id="login-box" class="login-box visible widget-box no-border">
									<div class="widget-body">
										<div class="widget-main">
    <h1>We appriciate your feedback.<br /> Thank you very much.</h1>
										</div><!-- /widget-main -->

										<div class="toolbar clearfix">
											<div>
                                                <h6></h6>
											</div>
										</div>
									</div><!-- /widget-body -->
								</div><!-- /login-box -->
							</div><!-- /position-relative -->
						</div>
					</div><!-- /.col -->
				</div><!-- /.row -->
			</div>
		</div><!-- /.main-container -->

	</body>
</html>
