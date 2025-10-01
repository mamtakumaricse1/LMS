<%@ Page Title="Manage Users" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageUsers.aspx.cs" Inherits="LMS5.Admin.ManageUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h2 class="text-center text-primary mb-4">Manage Users</h2>

        <asp:GridView ID="GridViewUsers" runat="server" CssClass="table table-bordered table-striped"
            AutoGenerateColumns="False" DataKeyNames="Id"
            OnRowEditing="GridViewUsers_RowEditing"
            OnRowCancelingEdit="GridViewUsers_RowCancelingEdit"
            OnRowUpdating="GridViewUsers_RowUpdating"
            OnRowDeleting="GridViewUsers_RowDeleting">
            
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="True" />
                <asp:BoundField DataField="Username" HeaderText="Username" />
                <asp:BoundField DataField="FullName" HeaderText="Full Name" />
                <asp:BoundField DataField="Role" HeaderText="Role" />
                <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" DataFormatString="{0:yyyy-MM-dd}" ReadOnly="True" />
                
                <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" DeleteText="Delete" EditText="Edit" />
            </Columns>
        </asp:GridView>

        <div class="mt-3">
            <asp:Button ID="btnAddUser" runat="server" Text="Add New User" CssClass="btn btn-success" OnClick="btnAddUser_Click" />
            <asp:Label ID="lblMsg" runat="server" CssClass="fw-bold mt-2 d-block"></asp:Label>
        </div>
    </div>
</asp:Content>
