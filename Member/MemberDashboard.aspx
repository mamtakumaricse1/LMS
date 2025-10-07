<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="MemberDashboard.aspx.cs" Inherits="LMS5.Member.MemberDashboard" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .dashboard-card {
            transition: transform 0.2s, box-shadow 0.2s;
            cursor: pointer;
        }
        .dashboard-card:hover {
            transform: translateY(-5px);
            box-shadow: 0 8px 20px rgba(0,0,0,0.15);
        }
        .dashboard-icon {
            font-size: 2.5rem;
        }
        .dashboard-count {
            font-size: 1.5rem;
            font-weight: 700;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="mb-4 text-primary">📚 Member Dashboard</h2>

    <div class="row g-4">

        <!-- Available Books -->
        <div class="col-md-3">
            <div class="card dashboard-card text-center text-white bg-success" onclick="window.location='<%= ResolveUrl("~/Shared/AvailableBooks.aspx") %>'">
                <div class="card-body">
                    <div class="dashboard-icon mb-2">📖</div>
                    <h5 class="card-title">Available Books</h5>
                    <div class="dashboard-count">
                        <asp:Label ID="lblAvailableCount" runat="server" Text="0"></asp:Label>
                    </div>
                </div>
            </div>
        </div>

        <!-- Borrowed Books -->
        <div class="col-md-3">
            <div class="card dashboard-card text-center text-white bg-primary" onclick="window.location='<%= ResolveUrl("~/Member/Borrow.aspx") %>'">
                <div class="card-body">
                    <div class="dashboard-icon mb-2">📝</div>
                    <h5 class="card-title">Borrowed Books</h5>
                    <div class="dashboard-count">
                        <asp:Label ID="lblBorrowedCount" runat="server" Text="0"></asp:Label>
                    </div>
                </div>
            </div>
        </div>

        <!-- My Borrowed Books -->
        <div class="col-md-3">
            <div class="card dashboard-card text-center text-white bg-warning" onclick="window.location='<%= ResolveUrl("~/Member/MyBooks.aspx") %>'">
                <div class="card-body">
                    <div class="dashboard-icon mb-2">📚</div>
                    <h5 class="card-title">My Books</h5>
                    <div class="dashboard-count">
                        <asp:Label ID="lblMyBooksCount" runat="server" Text="0"></asp:Label>
                    </div>
                </div>
            </div>
        </div>

        <!-- My Overdue Books -->
        <div class="col-md-3">
            <div class="card dashboard-card text-center text-white bg-danger" onclick="window.location='<%= ResolveUrl("~/Shared/OverdueBooks.aspx") %>'">
                <div class="card-body">
                    <div class="dashboard-icon mb-2">⚠️</div>
                    <h5 class="card-title">Overdue Books</h5>
                    <div class="dashboard-count">
                        <asp:Label ID="lblOverdueCount" runat="server" Text="0"></asp:Label>
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
