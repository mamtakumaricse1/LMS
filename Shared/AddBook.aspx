<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddBook.aspx.cs" Inherits="LMS5.Shared.AddBook" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
    .container {
        min-width: 600px;
        margin: 20px auto
        padding-bottom: -400px; /* ensures Save button stays above footer */
    }

    .message {
        margin-bottom: 12px;
        font-weight: bold;
    }
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h2 class="mb-3 text-center">📚 Add New Book</h2>
        <asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>

        <!-- Title -->
        <div class="mb-3">
            <label class="form-label">Title</label>
            <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" placeholder="Enter book title"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTitle" runat="server" 
                ControlToValidate="txtTitle" ErrorMessage="Title is required"
                CssClass="text-danger" Display="Dynamic" />
        </div>

        <!-- Author -->
        <div class="mb-3">
            <label class="form-label">Author</label>
            <asp:DropDownList ID="ddlAuthors" runat="server" CssClass="form-select"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvAuthor" runat="server" 
                ControlToValidate="ddlAuthors" InitialValue=""
                ErrorMessage="Select an author" CssClass="text-danger" Display="Dynamic" />
        </div>

        <!-- Category -->
        <div class="mb-3">
            <label class="form-label">Category</label>
            <asp:DropDownList ID="ddlCategories" runat="server" CssClass="form-select"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvCategory" runat="server"
                ControlToValidate="ddlCategories" InitialValue=""
                ErrorMessage="Select a category" CssClass="text-danger" Display="Dynamic" />
        </div>

        <!-- ISBN -->
        <div class="mb-3">
            <label class="form-label">ISBN</label>
            <asp:TextBox ID="txtISBN" runat="server" CssClass="form-control" placeholder="Enter 9–13 digit ISBN"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvISBN" runat="server"
                ControlToValidate="txtISBN" ErrorMessage="ISBN is required"
                CssClass="text-danger" Display="Dynamic" />
            <asp:RegularExpressionValidator ID="revISBN" runat="server"
                ControlToValidate="txtISBN" ValidationExpression="^\d{9,13}$"
                ErrorMessage="ISBN must be 9–13 digits" CssClass="text-danger" Display="Dynamic" />
        </div>

        <!-- Added Date -->
        <div class="mb-3">
            <label class="form-label">Added Date</label>
            <asp:TextBox ID="txtAddedDate" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvAddedDate" runat="server"
                ControlToValidate="txtAddedDate" ErrorMessage="Added Date is required"
                CssClass="text-danger" Display="Dynamic" />
        </div>

        <!-- Buttons -->
        <div class="d-flex justify-content-between">
            <asp:Button ID="btnSave" runat="server" Text="Save Book" CssClass="btn btn-success" OnClick="btnSave_Click" />
            
        </div>
    </div>
</asp:Content>
