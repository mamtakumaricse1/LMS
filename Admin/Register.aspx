<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs"
    Inherits="LMS5.Register" MasterPageFile="~/Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .card {
            min-width: 600px;
            margin: 40px auto;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 4px 10px rgba(0,0,0,0.1);
            background: #fff;
        }
        h2 { text-align: center; color: purple; margin-bottom: 20px; }
        .text-danger { font-size: 0.9rem; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card">
        <h2>Register New User</h2>

        <!-- Full Name -->
        <div class="mb-3">
            <asp:Label ID="lblFullName" runat="server" Text="Full Name"></asp:Label>
            <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvFullName" runat="server" ControlToValidate="txtFullName" ErrorMessage="Full Name is required" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revFullName" runat="server" ControlToValidate="txtFullName" ValidationExpression="^[A-Za-z\s]+$" ErrorMessage="Full Name must contain only letters" CssClass="text-danger" Display="Dynamic"></asp:RegularExpressionValidator>
        </div>

        <!-- Username -->
        <div class="mb-3">
            <asp:Label ID="lblUsername" runat="server" Text="Username"></asp:Label>
            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="txtUsername" ErrorMessage="Username is required" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revUsername" runat="server" ControlToValidate="txtUsername" ValidationExpression="^[A-Za-z]+$" ErrorMessage="Username must contain only letters" CssClass="text-danger" Display="Dynamic"></asp:RegularExpressionValidator>
        </div>

        <!-- Password -->
        <div class="mb-3">
            <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>
            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password is required" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revPassword" runat="server" ControlToValidate="txtPassword"
                ValidationExpression="^(?=.*[A-Za-z])(?=.*[^A-Za-z0-9]).{6,}$"
                ErrorMessage="Password must be at least 6 characters, with at least 1 letter and 1 special character"
                CssClass="text-danger" Display="Dynamic"></asp:RegularExpressionValidator>
        </div>

        <!-- Role Dropdown -->
        <div class="mb-3">
            <asp:Label ID="lblRole" runat="server" Text="Role"></asp:Label>
            <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-select">
                <asp:ListItem Text="Select Role" Value="" />
                <asp:ListItem Text="Admin" Value="Admin" />
                <asp:ListItem Text="Librarian" Value="Librarian" />
                <asp:ListItem Text="Member" Value="Member" />
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvRole" runat="server" ControlToValidate="ddlRole" InitialValue="" ErrorMessage="Please select a role" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>

        <!-- Buttons -->
        <div class="d-grid gap-2">
            <asp:Button ID="btnRegister" runat="server" Text="Register User" CssClass="btn btn-primary" OnClick="btnRegister_Click" CausesValidation="true" />
            
        </div>

        <div class="mt-2 text-center">
            <asp:Label ID="lblMsg" runat="server" CssClass="fw-bold"></asp:Label>
        </div>
    </div>
</asp:Content>
