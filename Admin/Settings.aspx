<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Settings.aspx.cs"
    Inherits="LMS5.Admin.Settings" MasterPageFile="~/Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2>⚙️ System Settings</h2>
        <asp:Label ID="lblMsg" runat="server" CssClass="text-success mb-3"></asp:Label>

        <div class="mt-3">
            <asp:Label ID="lblLibraryName" runat="server" Text="Library Name" CssClass="form-label"></asp:Label>
            <asp:TextBox ID="txtLibraryName" runat="server" CssClass="form-control mb-2" Placeholder="Library Name"></asp:TextBox>

            <asp:Label ID="lblAdminEmail" runat="server" Text="Admin Email" CssClass="form-label"></asp:Label>
            <asp:TextBox ID="txtAdminEmail" runat="server" CssClass="form-control mb-2" Placeholder="Admin Email"></asp:TextBox>

            <asp:Label ID="lblBorrowDays" runat="server" Text="Default Borrow Days" CssClass="form-label"></asp:Label>
            <asp:TextBox ID="txtBorrowDays" runat="server" CssClass="form-control mb-2" Placeholder="Borrow Days"></asp:TextBox>

            <asp:Label ID="lblFine" runat="server" Text="Fine Per Day (₹)" CssClass="form-label"></asp:Label>
            <asp:TextBox ID="txtFinePerDay" runat="server" CssClass="form-control mb-2" Placeholder="Fine Amount"></asp:TextBox>

            <asp:Button ID="btnSave" runat="server" Text="Save Settings" CssClass="btn btn-primary mt-2" OnClick="btnSave_Click" />
        </div>
    </div>
</asp:Content>
