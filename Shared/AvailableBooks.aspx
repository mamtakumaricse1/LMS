<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AvailableBooks.aspx.cs" Inherits="LMS5.Shared.AvailableBooks" %>
<%@ Register Assembly="System.Web.Extensions" Namespace="System.Web.UI" TagPrefix="asp" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <title>Available Books</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-4">
            <h2>Available Books </h2>
            <asp:Label ID="lblMessage" runat="server" CssClass="mt-2"></asp:Label>
            <asp:GridView ID="GridViewAvailableBooks" runat="server" AutoGenerateColumns="False" CssClass="table table-striped mt-3"
                OnRowDataBound="GridViewAvailableBooks_RowDataBound" OnRowCommand="GridViewAvailableBooks_RowCommand">
                <Columns>
                    <asp:BoundField DataField="Title" HeaderText="Title" />
                    <asp:BoundField DataField="AuthorName" HeaderText="Author" />
                    <asp:BoundField DataField="CategoryName" HeaderText="Category" />
                    <asp:BoundField DataField="ISBN" HeaderText="ISBN" />
                    <asp:BoundField DataField="Status" HeaderText="Status" />
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:Button ID="btnBorrow" runat="server" CssClass="btn btn-primary btn-sm" Text="Borrow" CommandName="Borrow" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <asp:Button ID="btnBack" runat="server" CssClass="btn btn-secondary mt-3" Text="Back" OnClick="btnBack_Click" />
        </div>
    </form>
</body>
</html>
