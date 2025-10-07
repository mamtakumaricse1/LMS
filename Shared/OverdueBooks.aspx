<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="OverdueBooks.aspx.cs"
    Inherits="LMS5.Shared.OverdueBooks"
    MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .table th, .table td {
            vertical-align: middle !important;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2 class="text-primary mb-3">⏰ Overdue Books</h2>
        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger mb-2"></asp:Label>

        <asp:GridView ID="GridViewOverdue" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped">
            <Columns>
                <asp:BoundField DataField="BorrowId" HeaderText="ID" ReadOnly="True" />
                <asp:BoundField DataField="BookTitle" HeaderText="Book Title" />
                <asp:BoundField DataField="Username" HeaderText="Borrower" />
                <asp:BoundField DataField="BorrowDate" HeaderText="Borrow Date" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="DueDate" HeaderText="Due Date" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="DaysOverdue" HeaderText="Days Overdue" />
                <asp:BoundField DataField="FineAmount" HeaderText="Fine" DataFormatString="{0:C}" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
