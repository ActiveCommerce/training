<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="performance.aspx.cs" Inherits="ActiveCommerce.Training.Web.sitecore.admin.performance" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Click "Run." Results will be displayed here and in Sitecore logs. <br />
        <asp:Button runat="server" Text="Run" OnClick="Run_Click"/> <br />
        <asp:Label id="uxDone" runat="server" Text="Done" ForeColor="Red" Visible="false" />
        
        <br /> <br />

        <table id="uxResults" runat="server" visible="false">
            <tr>
                <th><asp:Label ID="uxNumItems" runat="server" /></th>
                <th>Bucket</th>
                <th>Non-bucket</th>
            </tr>
            <tr>
                <th>Create</th>
                <td><asp:Label id="uxBucketCreate" runat="server" /></td>
                <td><asp:Label id="uxNonBucketCreate" runat="server" /></td>
            </tr>
            <tr>
                <th>Edit</th>
                <td><asp:Label id="uxBucketEdit" runat="server" /></td>
                <td><asp:Label id="uxNonBucketEdit" runat="server" /></td>
            </tr>
            <tr>
                <th>Sitecore Query</th>
                <td><asp:Label id="uxBucketQuery" runat="server" /></td>
                <td><asp:Label id="uxNonBucketQuery" runat="server" /></td>
            </tr>
            <tr>
                <th>Delete</th>
                <td><asp:Label id="uxBucketDelete" runat="server" /></td>
                <td><asp:Label id="uxNonBucketDlete" runat="server" /></td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
