<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="giftcardutility.aspx.cs" Inherits="ActiveCommerce.GiftCards.sitecore.admin.giftcardutility" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Active Commerce Gift Card Utility</title>
    <style>
        .actions {
            border-bottom: 1px solid #cccccc;
            margin-bottom: 10px;
            padding-bottom: 10px;
        }
        td, th {
            padding: 2px 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="actions">
        <asp:Button ID="btnGenerate" runat="server" Text="Generate" onclick="btnGenerate_Click" />
        <asp:Button ID="btnDelete" runat="server" Text="Delete All" onclick="btnDelete_Click" />
    </div>
    <table class="results">
        <tr>
            <th>Id</th>
            <th>Number</th>
            <th>Pin</th>
            <th>Balance</th>
        </tr>
        <asp:ListView runat="server" ID="lvResults">
            <LayoutTemplate>
                <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
                    <td><%#Eval("Id") %></td>
                    <td><%#Eval("Number") %></td>
                    <td><%#Eval("Pin") %></td>
                    <td><%#Eval("Balance", "{0:c}") %></td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </table>
    </form>
</body>
</html>
