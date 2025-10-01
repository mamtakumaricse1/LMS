<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="LMS.Register" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register User - LMS</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body { background: #f8f9fa; font-family: "Segoe UI", Tahoma, Geneva, Verdana, sans-serif; padding: 40px; }
        .card { max-width: 500px; margin: auto; padding: 20px; border-radius: 10px; box-shadow: 0 4px 10px rgba(0,0,0,0.1); background: #fff; }
        h2 { text-align: center; color: purple; margin-bottom: 20px; }
        .text-danger { font-size: 0.9rem; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="card">
            <h2>Register New User</h2>
            <!-- Full Name just to show commit-->

            <!-- Full Name -->
            <div class="mb-3">
                <asp:Label ID="lblFullName" runat="server" Text="Full Name"></asp:Label>
                <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvFullName" runat="server" ControlToValidate="txtFullName" ErrorMessage="Full Name is required" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>

            <!-- Username -->
            <div class="mb-3">
                <asp:Label ID="lblUsername" runat="server" Text="Username"></asp:Label>
                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="txtUsername" ErrorMessage="Username is required" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>

            <!-- Password -->
            <div class="mb-3">
                <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password is required" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revPassword" runat="server" ControlToValidate="txtPassword" ValidationExpression=".{6,}" ErrorMessage="Password must be at least 6 characters" CssClass="text-danger" Display="Dynamic"></asp:RegularExpressionValidator>
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

            <!-- Register Button -->
            <div class="d-grid gap-2">
                <asp:Button ID="btnRegister" runat="server" Text="Register User" CssClass="btn btn-primary" OnClick="btnRegister_Click" CausesValidation="true" />
                <asp:Button ID="btnBack" runat="server" Text="Back to Dashboard" CssClass="btn btn-secondary btn-back" OnClick="btnBack_Click" CausesValidation="false" />
            </div>

            <div class="mt-2 text-center">
                <asp:Label ID="lblMsg" runat="server" ForeColor="Red" CssClass="fw-bold"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
