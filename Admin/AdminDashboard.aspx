<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs"
    Inherits="LMS5.AdminDashboard" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-4">

        <!-- Header Row: Welcome + Logout -->
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h2 class="text-primary mb-0">📚 Admin Dashboard</h2>

          
        </div>

        <!-- Summary Cards Row -->
        <div class="row text-center mb-4">
            <div class="col-md-3">
                <div class="card shadow-sm border-primary">
                    <div class="card-body">
                        <h5 class="card-title text-muted">Total Books</h5>
                        <h2 class="text-primary"><asp:Label ID="lblBooks" runat="server" Text="0"></asp:Label></h2>
                    </div>
                </div>
            </div>

            <div class="col-md-3">
                <div class="card shadow-sm border-success">
                    <div class="card-body">
                        <h5 class="card-title text-muted">Total Users</h5>
                        <h2 class="text-success"><asp:Label ID="lblUsers" runat="server" Text="0"></asp:Label></h2>
                    </div>
                </div>
            </div>

            <div class="col-md-3">
                <div class="card shadow-sm border-warning">
                    <div class="card-body">
                        <h5 class="card-title text-muted">Books Issued</h5>
                        <h2 class="text-warning"><asp:Label ID="lblIssued" runat="server" Text="0"></asp:Label></h2>
                    </div>
                </div>
            </div>

            <div class="col-md-3">
                <div class="card shadow-sm border-danger">
                    <div class="card-body">
                        <h5 class="card-title text-muted">Overdue Books</h5>
                        <h2 class="text-danger"><asp:Label ID="lblOverdue" runat="server" Text="0"></asp:Label></h2>
                    </div>
                </div>
            </div>
        </div>

        <!-- Management Section -->
        <div class="card mb-4 shadow-sm">
            <div class="card-header bg-primary text-white">
                <strong>🔧 Quick Management</strong>
            </div>
            <div class="card-body text-center">
                <asp:Button ID="btnManageUsers" runat="server" Text="👤 Manage Users"
                    CssClass="btn btn-outline-primary me-3" OnClick="btnManageUsers_Click" />

                <asp:Button ID="btnManageBooks" runat="server" Text="📘 Manage Books"
                    CssClass="btn btn-outline-success me-3" OnClick="btnManageBooks_Click" />

                <asp:Button ID="btnBookReport" runat="server" Text="📊 View Reports"
                    CssClass="btn btn-outline-info" OnClick="btnBookReport_Click" />
            </div>
        </div>

        <!-- Recent Activities -->
        <div class="card shadow-sm">
            <div class="card-header bg-secondary text-white">
                <strong>🕓 Recent Borrowing Activity</strong>
            </div>
            <div class="card-body">
                <asp:GridView ID="GridViewRecent" runat="server" CssClass="table table-bordered table-striped"
                    AutoGenerateColumns="False" EmptyDataText="No recent activity found.">
                    <Columns>
                        <asp:BoundField DataField="FullName" HeaderText="User" />
                        <asp:BoundField DataField="Title" HeaderText="Book Title" />
                        <asp:BoundField DataField="BorrowDate" HeaderText="Issued On" DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:BoundField DataField="DueDate" HeaderText="Due Date" DataFormatString="{0:yyyy-MM-dd}" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

</asp:Content>
