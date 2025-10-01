<%@ Page Title="Borrow Records" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BorrowRecords.aspx.cs" Inherits="LMS5.Librarian.BorrowRecords" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2>📑 Borrow Records</h2>
        <asp:Label ID="lblMessage" runat="server" CssClass="text-success fw-bold"></asp:Label>

        <asp:GridView ID="gvBorrowRecords" runat="server" AutoGenerateColumns="False"
            CssClass="table table-bordered table-striped mt-3"
            DataKeyNames="Id"
            OnRowCommand="gvBorrowRecords_RowCommand">

            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Record ID" ReadOnly="true" />
                <asp:BoundField DataField="Username" HeaderText="Member" />
                <asp:BoundField DataField="BookTitle" HeaderText="Book" />
                <asp:BoundField DataField="BorrowDate" HeaderText="Borrowed On" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="ReturnDate" HeaderText="Returned On" DataFormatString="{0:yyyy-MM-dd}" />

                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <%# Eval("ReturnDate") == DBNull.Value ? "Borrowed" : "Returned" %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnReturn" runat="server" Text="Mark Returned" CssClass="btn btn-sm btn-success"
                            CommandName="ReturnBook" CommandArgument='<%# Eval("Id") %>'
                            Visible='<%# Eval("ReturnDate") == DBNull.Value %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        
    </div>
</asp:Content>
