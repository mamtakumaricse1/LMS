<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageBooks.aspx.cs" Inherits="LMS5.Admin.ManageBooks" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage Books - LMS</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body { background-color: #f8f9fa; font-family: "Segoe UI", Tahoma, Geneva, Verdana, sans-serif; padding: 20px; }
        .container { max-width: 1000px; margin: 0 auto; }
        .btn-back { margin-bottom: 12px; }
        .message { margin-bottom: 12px; font-weight: bold; }
        .gridview td { vertical-align: middle !important; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2 class="mb-3">Manage Books</h2>

            <asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>

            <div class="mb-3">
                <asp:Button ID="btnAddBook" runat="server" Text="Add New Book" CssClass="btn btn-success me-2" OnClick="btnAddBook_Click" />
                <asp:Button ID="btnBack" runat="server" Text="Back to Dashboard" CssClass="btn btn-secondary" OnClick="btnBack_Click" />
            </div>

            <asp:GridView ID="GridViewBooks" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped gridview"
                OnRowEditing="GridViewBooks_RowEditing"
                OnRowUpdating="GridViewBooks_RowUpdating"
                OnRowCancelingEdit="GridViewBooks_RowCancelingEdit"
                OnRowDeleting="GridViewBooks_RowDeleting"
                DataKeyNames="BookId">
                <Columns>
                    <asp:BoundField DataField="BookId" HeaderText="ID" ReadOnly="True" />
                    <asp:BoundField DataField="Title" HeaderText="Title" />
                    <asp:TemplateField HeaderText="Author">
                        <ItemTemplate>
                            <%# Eval("AuthorName") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlAuthors" runat="server"></asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Category">
                        <ItemTemplate>
                            <%# Eval("CategoryName") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlCategories" runat="server"></asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ISBN" HeaderText="ISBN" />
                    <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
