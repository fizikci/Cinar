<%@ Page Language="C#" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="DomainService.Core" %>
<%@ Import Namespace="$=table.Name$.API.Entity" %>
<%@ Import Namespace="$=db.Name$.API" %>
<script runat="server">
    public int logCount;
    public int logOKCount;
    protected override void OnLoad(EventArgs e)
    {
        switch (Request["method"])
        {
            case "lastLogs":
                {
                    Response.ContentType = "application/json";
                    string res = "";//Provider.Database.ReadList<EppQueue>("select * from EppQueue(nolock) where Id>{0} AND (Status='Done' OR Status='Fail') order by Id desc", Request["lastId"]).ToJSON();
                    Response.Write(res);
                    Response.End();
                    return;
                }
            case "dailyCounts":
                {
                    Response.ContentType = "application/json";
                    string res = Provider.Database.GetDataTable("select top 180 CONVERT(VARCHAR(10), [InsertDate], 102) as [LogDateYearMonth], count(*) as Hit from [EppQueue](nolock) where Command={0} group by CONVERT(VARCHAR(10), [InsertDate], 102) order by 1 desc;", Request["MethodName"]).Rows.Cast<DataRow>()
                        .Select(dr => string.Format("[{0}, {1}]", dr["LogDateYearMonth"], dr["Hit"])).StringJoin(",");
                    Response.Write("[" + res + "]");
                    Response.End();
                    return;
                }
        }

        logCount = Provider.Database.GetInt("select count(*) from EppQueue(nolock)");
        logOKCount = Provider.Database.GetInt("select count(*) from EppQueue(nolock) where Status='Done'");
    }


</script>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <title>FBS Services Core</title>

    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">

    <link href="/ext/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="/ext/css/bootstrap-responsive.min.css" rel="stylesheet" type="text/css" />

    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400italic,600italic,400,600" rel="stylesheet">
    <link href="/ext/css/font-awesome.min.css" rel="stylesheet">

    <link href="/ext/css/ui-lightness/jquery-ui-1.10.0.custom.min.css" rel="stylesheet">

    <link href="/ext/css/base-admin-2.css" rel="stylesheet">
    <link href="/ext/css/base-admin-2-responsive.css" rel="stylesheet">

    <link href="/ext/css/pages/dashboard.css" rel="stylesheet" type="text/css">

    <link href="/ext/css/custom.css" rel="stylesheet">
        <!-- Le HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
    <![endif]-->
    <script>
        setTimeout("location.reload()", 60000);
        var lastLogs = "";
        var apiUsageData = [<%=Provider.Database.GetDataTable("select Command as label, count(*) as data from EppQueue(NOLOCK) group by Command having count(*)>100").Rows.Cast<DataRow>().Select(dr => dr.ToJSON()).StringJoin(",")%>];
        var apiUsageInMinutesData = <%= "[" +
                                        Provider.Database.GetDataTable(@"select 
	                                                                (datepart(hour, InsertDate)*60+datepart(minute, InsertDate))/10 as dk, 
	                                                                count(*) as hit 
                                                                from 
	                                                                EppQueue(NOLOCK) 
                                                                where 
	                                                                InsertDate>DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) 
                                                                group by 
	                                                                (datepart(hour, InsertDate)*60+datepart(minute, InsertDate))/10 
                                                                order by 
	                                                                (datepart(hour, InsertDate)*60+datepart(minute, InsertDate))/10").Rows.Cast<DataRow>()
                                        .Select(dr => string.Format("[{0}, {1}]", dr["dk"], dr["hit"])).StringJoin(",")
                                        + "]"
        %>;
    </script>
	<style>
	.table.table-bordered tbody tr td {
		padding-top: 0px;
		padding-bottom: 0px;
	}
	</style>
</head>

<body>


<div class="main">

    <div class="container-liquid">

      <div class="row">
      	
      	<div class="span6">
      		
      		<div class="widget stacked">
					
				<div class="widget-header">
					<i class="icon-star"></i>
					<h3>Quick Stats</h3>
				</div> <!-- /widget-header -->
				
				<div class="widget-content">
					
					<div class="stats">
						
						<div class="stat">
							<span class="stat-value" id="totalAPICalls"><%=logCount %></span>									
							Total API Calls
						</div>
						
						<div class="stat">
							<span class="stat-value" id="todayAPICalls"><%=Provider.Database.GetInt("select count(*) from EppQueue(NOLOCK) where InsertDate>DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE()))") %></span>									
							Today (after midnight)
						</div>
						
						<div class="stat">
							<span class="stat-value"><!--%=Provider.Database.GetValue("select avg(ProcessTime) from EppQueue(NOLOCK)") %--> ms</span>									
							Avg. Process Time
						</div>
						
					</div>
					
                    <div id="recentAPICall" class="stat"></div>
                    <span id="apiName">&nbsp;</span>

					<div id="chart-stats" class="stats">
						
						<div class="stat stat-chart">							
							<div id="donut-chart" class="chart-holder"></div> <!-- #donut -->							
						</div> <!-- /substat -->
						
						<div class="stat stat-time">									
							<span class="stat-value">% <%=(100 * logOKCount/(float)logCount).ToString("0.00") %></span>
							Success Rate
						</div> <!-- /substat -->
						
					</div> <!-- /substats -->
					
				</div> <!-- /widget-content -->
					
			</div> <!-- /widget -->	

			<div class="widget stacked widget-table action-table">
					
				<div class="widget-header">
					<i class="icon-th-list"></i>
					<h3>Most Frequent API Calls</h3>
				</div> <!-- /widget-header -->
				
				<div class="widget-content">
					
					<table class="table table-striped table-bordered">
						<thead>
							<tr>
								<th>Registry</th>
								<th>Command</th>
								<th>Hit</th>
							</tr>
						</thead>
						<tbody>
<% foreach (DataRow dr in Provider.Database.GetDataTable("select top 25 c.Name as Registry, q.Command, count(*) as Hit from EppQueue(NOLOCK) q, Company(NOLOCK) c where q.CompanyId=c.Id group by c.Name, q.Command order by 3 desc").Rows)
   { %>
							<tr>
								<td><%=dr["Registry"] %></td>
								<td><%=dr["Command"] %></td>
								<td><%=dr["Hit"] %></td>
							</tr>
<% } %>
						</tbody>
					</table>
					
				</div> <!-- /widget-content -->
			
			</div>
			
	    </div> <!-- /span6 -->

      	<div class="span6">	
					
			<div class="widget stacked">
					
				<div class="widget-header">
					<i class="icon-signal"></i>
					<h3>Daily API Calls (with 10 minutes intervals)</h3>
				</div> <!-- /widget-header -->
				
				<div class="widget-content">					
					<div id="area-chart" class="chart-holder"></div>					
				</div> <!-- /widget-content -->
			
			</div>
					
			<div class="widget stacked widget-table action-table">
					
				<div class="widget-header">
					<i class="icon-th-list"></i>
					<h3>Most Expensive API Calls</h3>
				</div> <!-- /widget-header -->
				
				<div class="widget-content">
					
					<table class="table table-striped table-bordered">
						<thead>
							<tr>
								<th>Registry</th>
								<th>Method</th>
								<th>Avg Time</th>
                                <th>Call Number</th>
							</tr>
						</thead>
						<tbody>
<% foreach (DataRow dr in Provider.Database.GetDataTable(@"select top 10 
	                                                            c.Name as Registry, q.Command, avg(q.ProcessTime) as AvgTime, count(*) as Hit 
                                                            from 
	                                                            EppQueue(NOLOCK) q, Company(NOLOCK) c 
                                                            where
	                                                            q.CompanyId=c.Id and
	                                                            q.Status='Done'
                                                            group by 
	                                                            c.Name, q.Command 
                                                            having 
	                                                            count(*)>20 
                                                            order by 
	                                                            avg(ProcessTime) desc").Rows)
   { %>
							<tr>
								<td><%=dr["Registry"] %></td>
								<td><%=dr["Command"] %></td>
								<td><%=dr["AvgTime"] %></td>
								<td><%=dr["Hit"] %></td>
							</tr>
<% } %>
						</tbody>
					</table>
					
				</div> <!-- /widget-content -->
			
			</div>
			
        </div> <!-- /span6 -->

        <div class="span6">
            
			<div class="widget stacked">
					
				<div class="widget-header">
					<i class="icon-signal"></i>
					<h3>Last Errors</h3>
				</div> <!-- /widget-header -->
				
				<div class="widget-content">					
					<div id="errors" style="height:400px;overflow-y:auto;">
<% foreach (DataRow l in Provider.Database.GetDataTable("select top 50 c.Name as Registry, q.Command, q.DomainName, 'No response' as Response from EppQueue(NOLOCK) q, Company(NOLOCK) c where q.CompanyId=c.Id and q.Status='Failed' order by q.Id desc").Rows)
   { %>
							<div><%=l["Registry"] %>.<%=l["Command"]%> for <b><%=l["DomainName"]%></b>:<br/><%=l["Response"]%></div>
<% } %>

					</div>
				</div> <!-- /widget-content -->
			
			</div>
            
            <div class="widget stacked widget-table action-table">
					
				<div class="widget-header">
					<i class="icon-th-list"></i>
					<h3>Most Frequent Errors</h3>
				</div> <!-- /widget-header -->
				
				<div class="widget-content">
					
					<table class="table table-striped table-bordered">
						<thead>
							<tr>
								<th>CompanyId</th>
								<th>Command</th>
                                <th>Fail</th>
                                <th>Total</th>
                                <th>%</th>
							</tr>
						</thead>
						<tbody>

						</tbody>
					</table>
					
				</div> <!-- /widget-content -->
			
			</div>

            
        </div>

      </div> <!-- /row -->

    </div> <!-- /container -->
    
</div> <!-- /main -->


    <!-- Le javascript
================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <script src="/ext/js/libs/jquery-1.8.3.min.js"></script>
    <script src="/ext/js/libs/jquery-ui-1.10.0.custom.min.js"></script>
    <script src="/ext/js/libs/bootstrap.min.js"></script>

    <script src="/ext/js/plugins/flot/jquery.flot.js"></script>
    <script src="/ext/js/plugins/flot/jquery.flot.pie.js"></script>
    <script src="/ext/js/plugins/flot/jquery.flot.resize.js"></script>

    <script src="/ext/js/Application.js"></script>

</body>
</html>
