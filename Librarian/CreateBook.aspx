<%@ Page Title="Create Book" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateBook.aspx.cs" Inherits="LMS5.Librarian.CreateBook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <!-- Page-specific CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .message { margin-bottom: 12px; font-weight: bold; }
        .form-container { max-width: 600px; margin: 24px auto; background-color: rgba(255,255,255,0.9); padding: 20px; border-radius: 8px; box-shadow: 0 2px 6px rgba(0,0,0,0.15); }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-container">
        <h2 class="mb-3">Add New Book</h2>

        <asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>

        <div class="mb-3">
            <label class="form-label">Title</label>
            <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle"
                ErrorMessage="Title is required" CssClass="text-danger" Display="Dynamic" />
        </div>

        <div class="mb-3">
            <label class="form-label">Author</label>
            <asp:DropDownList ID="ddlAuthors" runat="server" CssClass="form-select"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvAuthor" runat="server" ControlToValidate="ddlAuthors"
                InitialValue="" ErrorMessage="Select an author" CssClass="text-danger" Display="Dynamic" />
        </div>

        <div class="mb-3">
            <label class="form-label">Category</label>
            <asp:DropDownList ID="ddlCategories" runat="server" CssClass="form-select"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvCategory" runat="server" ControlToValidate="ddlCategories"
                InitialValue="" ErrorMessage="Select a category" CssClass="text-danger" Display="Dynamic" />
        </div>

        <div class="mb-3">
            <label class="form-label">ISBN</label>
            <asp:TextBox ID="txtISBN" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvISBN" runat="server" ControlToValidate="txtISBN"
                ErrorMessage="ISBN is required" CssClass="text-danger" Display="Dynamic" />
            <asp:RegularExpressionValidator ID="revISBN" runat="server" ControlToValidate="txtISBN"
                CssClass="text-danger" ValidationExpression="^\d{9,13}$" ErrorMessage="ISBN must be 9-13 digits" Display="Dynamic" />
        </div>

        <div class="mb-3">
            <label class="form-label">Added Date</label>
            <asp:TextBox ID="txtAddedDate" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvAddedDate" runat="server" ControlToValidate="txtAddedDate"
                ErrorMessage="Added Date is required" CssClass="text-danger" Display="Dynamic" />
        </div>

        <div class="mb-3">
            <asp:Button ID="btnSave" runat="server" Text="Save Book" CssClass="btn btn-success me-2" OnClick="btnSave_Click" />
           
        </div>
    </div>
</asp:Content>
