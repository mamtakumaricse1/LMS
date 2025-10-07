<%@ Page Title="Librarian Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LibrarianDashboard.aspx.cs" Inherits="LMS5.Librarian.LibrarianDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <!-- Add any custom CSS/JS here -->
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2 class="text-primary mb-3">📚 Librarian Dashboard</h2>
        <p class="lead">Welcome, Librarian! Use the quick actions below to manage your daily library tasks.</p>

        <div class="row mt-4">
            <div class="col-md-3 mb-3">
                <asp:Button ID="btnManageBooks" runat="server" Text="📘 Manage Books" CssClass="btn btn-primary w-100"
                    OnClick="btnManageBooks_Click" />
            </div>
            <div class="col-md-3 mb-3">
                <asp:Button ID="btnAvailableBooks" runat="server" Text="📖 Available Books" CssClass="btn btn-success w-100"
                    OnClick="btnAvailableBooks_Click" />
            </div>
            <div class="col-md-3 mb-3">
                <asp:Button ID="btnBorrowRecords" runat="server" Text="📚 Borrow Records" CssClass="btn btn-warning w-100"
                    OnClick="btnBorrowRecords_Click" />
            </div>
            <div class="col-md-3 mb-3">
                <asp:Button ID="btnOverdueBooks" runat="server" Text="⏰ Overdue Books" CssClass="btn btn-danger w-100"
                    OnClick="btnOverdueBooks_Click" />
            </div>
        </div>
    </div>
</asp:Content>
