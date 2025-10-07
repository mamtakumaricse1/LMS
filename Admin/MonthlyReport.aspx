
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonthlyReport.aspx.cs"
    Inherits="LMS5.Admin.MonthlyReport" MasterPageFile="~/Site.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2>📅 Monthly Borrowing Summary</h2>
        <asp:GridView ID="GridViewMonthly" runat="server" CssClass="table table-bordered table-striped">
        </asp:GridView>
    </div>
</asp:Content>
