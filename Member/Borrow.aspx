<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Borrow.aspx.cs" Inherits="LMS5.Member.Borrow" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Borrow a Book</h2>

    <asp:Label ID="lblMessage" runat="server" CssClass="mb-3 d-block"></asp:Label>

    <asp:GridView ID="GridViewBooks" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
        OnRowDataBound="GridViewBooks_RowDataBound"
        OnRowCommand="GridViewBooks_RowCommand">
        <Columns>
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:BoundField DataField="AuthorName" HeaderText="Author" />
            <asp:BoundField DataField="CategoryName" HeaderText="Category" />
            <asp:BoundField DataField="ISBN" HeaderText="ISBN" />
            <asp:BoundField DataField="Status" HeaderText="Status" />
            <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                    <asp:Button ID="btnBorrow" runat="server" CssClass="btn btn-primary btn-sm" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
