<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="ManageBooks.aspx.cs"
    Inherits="LMS5.Shared.ManageBooks"
    MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />

    <!-- ISBN Validation Script -->
    <script type="text/javascript">
        function validateISBN(txtBoxId) {
            var txt = document.getElementById(txtBoxId).value;
            var regex = /^\d{9,13}$/;
            if (!regex.test(txt)) {
                alert("ISBN must be 9 to 13 digits only.");
                return false;
            }
            return true;
        }
    </script>

    <style>
        .container {
            max-width: 1000px;
            margin: 20px auto;
        }

        .message {
            margin-bottom: 12px;
            font-weight: bold;
        }

        .gridview td {
            vertical-align: middle !important;
        }

        .btn {
            border-radius: 6px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h2 class="mb-3 text-primary text-center">📘 Manage Books</h2>

        <!-- Message Label -->
        <asp:Label ID="lblMessage" runat="server" CssClass="message text-center d-block"></asp:Label>

        <!-- Add New Book Button -->
        <div class="mb-3 text-end">
            <asp:Button ID="btnAddBook" runat="server"
                Text="Add New Book"
                CssClass="btn btn-success"
                OnClick="btnAddBook_Click" />
        </div>

        <!-- Books Grid -->
        <asp:GridView ID="GridViewBooks" runat="server"
            AutoGenerateColumns="False"
            CssClass="table table-bordered table-striped gridview"
            DataKeyNames="BookId"
            OnRowEditing="GridViewBooks_RowEditing"
            OnRowUpdating="GridViewBooks_RowUpdating"
            OnRowCancelingEdit="GridViewBooks_RowCancelingEdit"
            OnRowDeleting="GridViewBooks_RowDeleting"
            OnRowDataBound="GridViewBooks_RowDataBound">

            <Columns>
                <%-- Book ID --%>
                <asp:BoundField DataField="BookId" HeaderText="ID" ReadOnly="True" />

                <%-- Title --%>
                <asp:BoundField DataField="Title" HeaderText="Title" />

                <%-- Author --%>
                <asp:TemplateField HeaderText="Author">
                    <ItemTemplate>
                        <%# Eval("AuthorName") %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlAuthors" runat="server" CssClass="form-select"></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>

                <%-- Category --%>
                <asp:TemplateField HeaderText="Category">
                    <ItemTemplate>
                        <%# Eval("CategoryName") %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlCategories" runat="server" CssClass="form-select"></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>

                <%-- ISBN --%>
                <asp:TemplateField HeaderText="ISBN">
                    <ItemTemplate>
                        <%# Eval("ISBN") %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtISBN" runat="server"
                            Text='<%# Bind("ISBN") %>'
                            CssClass="form-control"
                            onblur="validateISBN(this.id)"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

                <%-- Command Buttons --%>
                <asp:CommandField ShowEditButton="True" ShowDeleteButton="True"
                    EditText="Edit" DeleteText="Delete"
                    ControlStyle-CssClass="btn btn-sm btn-primary me-2" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
