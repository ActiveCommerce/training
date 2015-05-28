<%@ Page Language="C#" AutoEventWireup="true" %>

<%
    Sitecore.Analytics.Tracker.Current.CurrentPage.Cancel();
    HttpContext.Current.Session.Abandon();    
%>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
