<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
	<telerik:RadStyleSheetManager id="RadStyleSheetManager1" runat="server" />
    <link href="MuseumStyles.css" type="text/css" rel="Stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
	<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
		<Scripts>
			<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
			<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
			<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
		</Scripts>
	</telerik:RadScriptManager>
	<script type="text/javascript">
		//Put your JavaScript code here.
    </script>
	<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
	</telerik:RadAjaxManager>
	<div id="pageHeader">
        This is the header place-holder
	</div>
    <div id="mainPageLeftColumn">
        <h3>Featured Exhibit</h3>
        <p>List 5 first pictures Horizontally
            <br />
            <h4>Photos of #exhibitName#</h4>
            <br />
            Description of exhibit
        </p>
        <hr />
        <p>
            <h3>Exhibits by Tag</h3>
            Tag Cloud Goes Here
        </p>

    </div>

    <div id="mainPageRightColumn">
        <h3>Newest Exhibits</h3>
    </div>

    <div id="pageFooter">
        &copy;2013 
    </div>




	</form>
</body>
</html>
