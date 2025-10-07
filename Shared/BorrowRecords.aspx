<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="BorrowRecords.aspx.cs"
    Inherits="LMS5.Shared.BorrowRecords"
    MasterPageFile="~/Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2 class="text-primary mb-3">📋 Borrow Records</h2>
        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger mb-2"></asp:Label>

       <asp:GridView ID="GridViewRecords" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped">
    <Columns>
        <asp:BoundField DataField="BorrowId" HeaderText="ID" ReadOnly="True" />
        <asp:BoundField DataField="BookTitle" HeaderText="Book Title" />
        <asp:BoundField DataField="MemberName" HeaderText="Borrower" />
        <asp:BoundField DataField="BorrowDate" HeaderText="Borrow Date" DataFormatString="{0:yyyy-MM-dd}" />
        <asp:BoundField DataField="DueDate" HeaderText="Due Date" DataFormatString="{0:yyyy-MM-dd}" />
        <asp:BoundField DataField="ReturnDate" HeaderText="Return Date" DataFormatString="{0:yyyy-MM-dd}" />
    </Columns>
</asp:GridView>

    </div>
</asp:Content>
