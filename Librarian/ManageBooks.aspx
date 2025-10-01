<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageBooks.aspx.cs" Inherits="LMS5.Librarian.ManageBooks" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .form-container { max-width: 1000px; margin: 24px auto; background-color: rgba(255,255,255,0.9); padding: 20px; border-radius: 8px; box-shadow: 0 2px 6px rgba(0,0,0,0.1); }
        .message { margin-bottom: 12px; font-weight: bold; }
        .gridview td { vertical-align: middle !important; }
        .btn-back { margin-bottom: 12px; }
    </style>

    <script type="text/javascript">
        // ✅ Client-side ISBN validation (9–13 digits only)
        function validateISBN(source, args) {
            var regex = /^\d{9,13}$/;
            args.IsValid = regex.test(args.Value);
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-container">
        <h2 class="mb-3">Manage Books</h2>

        <asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>

        <div class="mb-3">
            <asp:Button ID="btnAddBook" runat="server" Text="Add New Book" CssClass="btn btn-success me-2" OnClick="btnAddBook_Click" />
            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-secondary btn-back" OnClick="btnBack_Click" />
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

                
                <asp:TemplateField HeaderText="ISBN">
                    <ItemTemplate>
                        <%# Eval("ISBN") %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtISBN" runat="server" Text='<%# Bind("ISBN") %>' CssClass="form-control" />
                        <asp:CustomValidator ID="cvISBN" runat="server" ControlToValidate="txtISBN"
                            ErrorMessage="ISBN must be 9–13 digits" Display="Dynamic"
                            ClientValidationFunction="validateISBN"
                            ForeColor="Red" />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
