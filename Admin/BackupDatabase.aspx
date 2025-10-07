<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BackupDatabase.aspx.cs"
    Inherits="LMS5.Admin.BackupDatabase" MasterPageFile="~/Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2>💾 Backup Database</h2>
        <asp:Label ID="lblStatus" runat="server" CssClass="text-success"></asp:Label><br /><br />
        <asp:Button ID="btnBackup" runat="server" Text="Backup Database" CssClass="btn btn-primary" OnClick="btnBackup_Click" />
    </div>
</asp:Content>
