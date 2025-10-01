<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BookReport.aspx.cs" Inherits="LMS5.Admin.BookReport" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Book Report - LMS</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body { background-color: #f8f9fa; font-family: "Segoe UI", Tahoma, Geneva, Verdana, sans-serif; padding: 20px; }
        .container { max-width: 1000px; margin: 0 auto; }
        .btn-back { margin-bottom: 12px; }
        .message { margin-bottom: 12px; font-weight: bold; }
        .gridview td { vertical-align: middle !important; }
        .search-bar { margin-bottom: 12px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2 class="mb-3">Book Report</h2>

            <asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>

            <div class="search-bar mb-3 d-flex gap-2">
                <asp:TextBox ID="txtSearchAuthor" runat="server" CssClass="form-control" Placeholder="Search by Author"></asp:TextBox>
                <asp:TextBox ID="txtSearchCategory" runat="server" CssClass="form-control" Placeholder="Search by Category"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-secondary" OnClick="btnReset_Click" />
            </div>

            <div class="mb-3">
                <asp:Button ID="btnBack" runat="server" Text="Back to Dashboard" CssClass="btn btn-secondary" OnClick="btnBack_Click" />
                <asp:Button ID="btnAddBook" runat="server" Text="Add New Book" CssClass="btn btn-success ms-2" OnClick="btnAddBook_Click" />
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
                    <asp:BoundField DataField="AddedDate" HeaderText="Added Date" DataFormatString="{0:yyyy-MM-dd}" ReadOnly="True" />
                    <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
