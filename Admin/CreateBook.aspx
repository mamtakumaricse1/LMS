<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateBook.aspx.cs" Inherits="LMS5.Admin.CreateBook" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Create Book - LMS</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body { background-color: #f8f9fa; font-family: "Segoe UI", Tahoma, Geneva, Verdana, sans-serif; padding: 20px; }
        .container { max-width: 600px; margin: 0 auto; }
        .message { margin-bottom: 12px; font-weight: bold; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
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
                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-secondary" OnClick="btnBack_Click" />
            </div>
        </div>
    </form>
</body>
</html>
