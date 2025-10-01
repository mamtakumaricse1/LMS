<%@ Page Title="My Borrowed Books" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyBooks.aspx.cs" Inherits="LMS5.Member.MyBooks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>My Borrowed Books</h2>

    <asp:Label ID="lblMessage" runat="server" CssClass="mb-3"></asp:Label>

    <asp:GridView ID="GridViewMyBooks" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
        EmptyDataText="You have not borrowed any books yet."
        OnRowCommand="GridViewMyBooks_RowCommand"
        OnRowDataBound="GridViewMyBooks_RowDataBound">
        <Columns>
            <asp:BoundField DataField="Title" HeaderText="Book Title" />
            <asp:BoundField DataField="AuthorName" HeaderText="Author" />
            <asp:BoundField DataField="CategoryName" HeaderText="Category" />
            <asp:BoundField DataField="BorrowDate" HeaderText="Borrow Date" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField HeaderText="Due Date" DataField="DueDate" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField DataField="ReturnDate" HeaderText="Return Date" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField DataField="Status" HeaderText="Status" />
            <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                    <asp:Button ID="btnReturn" runat="server" Text="Return" CssClass="btn btn-warning btn-sm"
                        CommandName="ReturnBook" CommandArgument='<%# Eval("Id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

   
</asp:Content>
