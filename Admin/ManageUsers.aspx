<%@ Page Title="Manage Users" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageUsers.aspx.cs" Inherits="LMS5.Admin.ManageUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mt-3 mb-3 d-flex justify-content-end">
        <asp:Button ID="btnAddUser" runat="server" Text="Add New User" CssClass="btn btn-success" OnClick="btnAddUser_Click" />
    </div>

    <div class="container">
        <h2 class="text-center text-primary mb-4">Manage Users</h2>

        <asp:Label ID="lblMsg" runat="server" CssClass="fw-bold mt-2 d-block"></asp:Label>

        <asp:GridView ID="GridViewUsers" runat="server" CssClass="table table-bordered table-striped"
            AutoGenerateColumns="False" DataKeyNames="Id"
            OnRowEditing="GridViewUsers_RowEditing"
            OnRowCancelingEdit="GridViewUsers_RowCancelingEdit"
            OnRowUpdating="GridViewUsers_RowUpdating"
            OnRowDeleting="GridViewUsers_RowDeleting"
            OnRowDataBound="GridViewUsers_RowDataBound">

            <Columns>
                <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="True" />

                <asp:TemplateField HeaderText="Username">
                    <ItemTemplate>
                        <%# Eval("Username") %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" Text='<%# Bind("Username") %>'></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revUsername" runat="server"
                            ControlToValidate="txtUsername"
                            ValidationExpression="^[A-Za-z]+$"
                            ErrorMessage="Username must contain only letters"
                            CssClass="text-danger" Display="Dynamic"></asp:RegularExpressionValidator>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Full Name">
                    <ItemTemplate>
                        <%# Eval("FullName") %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" Text='<%# Bind("FullName") %>'></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revFullName" runat="server"
                            ControlToValidate="txtFullName"
                            ValidationExpression="^[A-Za-z\s]+$"
                            ErrorMessage="Full Name must contain only letters"
                            CssClass="text-danger" Display="Dynamic"></asp:RegularExpressionValidator>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Role">
                    <ItemTemplate>
                        <%# Eval("Role") %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-select">
                            <asp:ListItem Text="Admin" Value="Admin" />
                            <asp:ListItem Text="Librarian" Value="Librarian" />
                            <asp:ListItem Text="Member" Value="Member" />
                        </asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" DataFormatString="{0:yyyy-MM-dd}" ReadOnly="True" />

                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" CssClass="btn btn-primary btn-sm me-2">Edit</asp:LinkButton>
                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CssClass="btn btn-danger btn-sm"
                            OnClientClick="return confirm('Are you sure you want to delete this user?');">Delete</asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="btnUpdate" runat="server" CommandName="Update" CssClass="btn btn-success btn-sm me-2">Update</asp:LinkButton>
                        <asp:LinkButton ID="btnCancel" runat="server" CommandName="Cancel" CssClass="btn btn-secondary btn-sm">Cancel</asp:LinkButton>
                    </EditItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
