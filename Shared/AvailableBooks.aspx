<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AvailableBooks.aspx.cs"
    Inherits="LMS5.Shared.AvailableBooks" MasterPageFile="~/Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .container-available {
            max-width: 1000px;
            margin: 20px auto;
            padding-bottom: 120px; /* space for sticky footer */
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-available">
        <h2 class="mb-3">📖 Available Books</h2>

        <asp:Label ID="lblMessage" runat="server" CssClass="fw-bold text-danger mb-3"></asp:Label>

                   <asp:GridView ID="GridViewAvailableBooks" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                OnRowDataBound="GridViewAvailableBooks_RowDataBound" OnRowCommand="GridViewAvailableBooks_RowCommand">
                <Columns>
                    <asp:BoundField DataField="Title" HeaderText="Title" />
                    <asp:BoundField DataField="AuthorName" HeaderText="Author" />
                    <asp:BoundField DataField="CategoryName" HeaderText="Category" />
                    <asp:BoundField DataField="ISBN" HeaderText="ISBN" />
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <span id="lblStatus" runat="server" class="badge"></span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:Button ID="btnBorrow" runat="server" CssClass="btn btn-primary btn-sm" Text="Borrow" CommandName="Borrow" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>


       
    </div>
</asp:Content>
