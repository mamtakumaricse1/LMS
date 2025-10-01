<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs"
    Inherits="LMS5.AdminDashboard" MasterPageFile="~/Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-4">
          <h2 class="mb-3">📚 Admin Dashboard</h2>

        <!-- User Management Card -->
        <div class="card mb-4">
            <div class="card-header bg-primary text-white">
                User Management
            </div>
            <div class="card-body">
                <asp:GridView ID="GridViewUsers" runat="server" CssClass="table table-bordered table-striped"
                    AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="ID" />
                        <asp:BoundField DataField="Username" HeaderText="Username" />
                        <asp:BoundField DataField="FullName" HeaderText="Full Name" />
                        <asp:BoundField DataField="Role" HeaderText="Role" />
                        <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" DataFormatString="{0:yyyy-MM-dd}" />
                    </Columns>
                </asp:GridView>

                <div class="mt-3">
                    <asp:Button ID="btnAddUser" runat="server" Text="Register User" CssClass="btn btn-success me-2"
                        OnClick="btnAddUser_Click" />
                    <asp:Button ID="btnEditUser" runat="server" Text="Edit/Delete Users" CssClass="btn btn-warning"
                        OnClick="btnEditUser_Click" />
                </div>
            </div>
        </div>

        <!-- Book Management Card -->
        <div class="card">
            <div class="card-header bg-success text-white">
                Book Management
            </div>
            <div class="card-body">
                <asp:Button ID="btnBookReport" runat="server" Text="View Books Report" CssClass="btn btn-info"
                    OnClick="btnBookReport_Click" />
            </div>
        </div>
    </div>

</asp:Content>
