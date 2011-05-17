<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="ScrewTurn.Wiki.Search" ValidateRequest="false" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<asp:Content ID="ctnSearch" ContentPlaceHolderID="CphMaster" runat="server">
	<asp:Literal ID="lblStrings" runat="server" />

	<script type="text/javascript">
	<!--
		function ToggleCategoriesList() {
			var chk = document.getElementById(AllNamespacesCheckbox);
			if(chk.checked) document.getElementById("CategoryFilterDiv").style["display"] = "none";
			else document.getElementById("CategoryFilterDiv").style["display"] = "";
		}
	// -->
	</script>

	<h1 class="pagetitlesystem"><asp:Literal ID="lblTitle" runat="server" Text="Arama" EnableViewState="False" meta:resourcekey="lblTitleResource1" /></h1>
	<p><asp:Literal ID="lblInstructions" runat="server" Text="Burada sitedeki tüm dökümanlar üzerinde arama yapabilirsiniz.<br /><b>Þu daha çok iþe yarayabilir</b>: <a href='http://www.google.com?q=site:cinarteknoloji.com'>http://www.google.com?q=site:cinarteknoloji.com</a>." EnableViewState="False" meta:resourcekey="lblInstructionsResource1" /></p>
	
	<div id="SearchControlsDiv">
		<asp:TextBox ID="txtQuery" runat="server" CssClass="textbox" meta:resourcekey="txtQueryResource1" />
		<asp:Button ID="btnGo" runat="server" Text="ARA" EnableViewState="False" CssClass="button" OnClick="btnGo_Click" meta:resourcekey="btnGoResource1" /><br />
		
		<div id="RadiosDiv">
			<asp:RadioButton ID="rdoAtLeastOneWord" runat="server" Text="En az bir kelime" Checked="True" GroupName="search" meta:resourcekey="rdoAtLeastOneWordResource1" />
			<asp:RadioButton ID="rdoAllWords" runat="server" Text="Tüm kelimeler" GroupName="search" meta:resourcekey="rdoAllWordsResource1" />
			<asp:RadioButton ID="rdoExactPhrase" runat="server" Text="Girildiði gibi" GroupName="search" meta:resourcekey="rdoExactPhraseResource1" />
		</div>
		
		<asp:CheckBox ID="chkAllNamespaces" runat="server" Text="Tüm bölümleri ara" Checked="true" onclick="javascript:ToggleCategoriesList();" meta:resourcekey="chkAllNamespacesResource1" />
		<br />
		<asp:CheckBox ID="chkFilesAndAttachments" runat="server" Text="Dosyalarý ve eklentileri de ara" Checked="true" meta:resourcekey="chkFilesAndAttachmentsResource1" />
	</div>
	
	<div id="CategoryFilterDiv">
		<h4><asp:Literal ID="lblCategoryFilter" runat="server" Text="Filter by Category" EnableViewState="False" meta:resourcekey="lblCategoryFilterResource1" /></h4>
		<div id="CategoryFilterInternalDiv">
			<i><asp:CheckBox ID="chkUncategorizedPages" runat="server" Text="Uncategorized Pages" Checked="True" meta:resourcekey="chkUncategorizedPagesResource1" /></i><br />
			<asp:CheckBoxList ID="lstCategories" runat="server" RepeatLayout="Flow" meta:resourcekey="lstCategoriesResource1" />
		</div>
	</div>
	
	<div id="SearchStatsDiv">
		<asp:Literal ID="lblStats" runat="server" meta:resourcekey="lblStatsResource1" />
	</div>
	
	<div id="ResultsDiv">
		<asp:Repeater ID="rptResults" runat="server">
			<ItemTemplate>
				<h3 class='searchresult<%# Eval("Type") %>'><a href='<%# Eval("Link") %>' title='<%# Eval("Title") %>'><%# Eval("Title") %></a> &mdash; <%# Eval("Relevance", "{0:N1}") %>%</h3>
				<p class="excerpt" style='<%# (((string)Eval("FormattedExcerpt")).Length > 0 ? "" : "display: none;") %>'><%# Eval("FormattedExcerpt") %></p>
			</ItemTemplate>
		</asp:Repeater>
	</div>
	
	<asp:Literal ID="lblHideCategoriesScript" runat="server" />
	
	<script type="text/javascript">
		ToggleCategoriesList();
	</script>
	
</asp:Content>
