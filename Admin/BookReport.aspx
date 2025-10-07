<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BookReport.aspx.cs"
    Inherits="LMS5.Admin.BookReport" MasterPageFile="~/Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .container-report {
            max-width: 1000px;
            margin: 20px auto;
            padding-bottom: 120px; /* space for sticky footer */
        }
        .stats-box {
            display: inline-block;
            padding: 12px 20px;
            margin-right: 12px;
            border-radius: 8px;
            background-color: #f1f1f1;
            font-weight: 600;
            min-width: 150px;
            text-align: center;
        }
        .stats-box span {
            display: block;
            font-size: 1.5rem;
            color: #0d6efd;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-report">
        <h2 class="mb-3 text-center">📊 Book Report</h2>

        <!-- Stats Panel -->
        <div class="mb-3">
            <div class="stats-box">
                Total Books
                <span id="lblTotalBooks" runat="server">0</span>
            </div>
            <div class="stats-box">
                Available
                <span id="lblAvailableBooks" runat="server">0</span>
            </div>
            <div class="stats-box">
                Borrowed
                <span id="lblBorrowedBooks" runat="server">0</span>
            </div>
        </div>

        <!-- Filters -->
        <div class="row mb-3">
            <div class="col-md-4">
                <asp:TextBox ID="txtSearchAuthor" runat="server" CssClass="form-control" placeholder="Search by Author"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtSearchCategory" runat="server" CssClass="form-control" placeholder="Search by Category"></asp:TextBox>
            </div>
            <div class="col-md-4 d-flex gap-2">
                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
                <asp:Button ID="btnReset" runat="server" CssClass="btn btn-secondary" Text="Reset" OnClick="btnReset_Click" />
              
            </div>
        </div>

        <asp:Label ID="lblMessage" runat="server" CssClass="fw-bold text-danger mb-3"></asp:Label>

        <!-- GridView -->
        <asp:GridView ID="GridViewBooks" runat="server" CssClass="table table-bordered table-striped"
            AutoGenerateColumns="False" EmptyDataText="No books found">
            <Columns>
                <asp:BoundField DataField="BookId" HeaderText="Book ID" />
                <asp:BoundField DataField="Title" HeaderText="Title" />
                <asp:BoundField DataField="ISBN" HeaderText="ISBN" />
                <asp:BoundField DataField="AuthorName" HeaderText="Author" />
                <asp:BoundField DataField="CategoryName" HeaderText="Category" />
                <asp:BoundField DataField="AddedDate" HeaderText="Added Date" DataFormatString="{0:dd-MMM-yyyy}" />
                <asp:BoundField DataField="Status" HeaderText="Status" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
