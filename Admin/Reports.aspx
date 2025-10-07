<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="LMS5.Admin.Reports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2>📊 Library Reports</h2>
        <asp:GridView ID="GridViewReport" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="BookId" HeaderText="Book ID" />
                <asp:BoundField DataField="Title" HeaderText="Title" />
                <asp:BoundField DataField="AuthorName" HeaderText="Author" />
                <asp:BoundField DataField="CategoryName" HeaderText="Category" />
                <asp:BoundField DataField="TimesBorrowed" HeaderText="Times Borrowed" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>

