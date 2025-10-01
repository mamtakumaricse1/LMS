<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="LMS5.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>📚 LMS - Login</title>
    <!-- Bootstrap -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background: #f0f2f5;
            font-family: 'Segoe UI', sans-serif;
        }
        .login-card {
            width: 400px;
            margin: 80px auto;
            padding: 30px;
            background: #fff;
            border-radius: 10px;
            border: 1px solid #ddd;
            box-shadow: 0px 4px 12px rgba(0,0,0,0.1);
        }
        .login-card h2 {
            text-align: center;
            color: #007bff;
            margin-bottom: 20px;
        }
        .btn-custom {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-card">
            <h2>Library Login</h2>

            <div class="mb-3">
                <asp:Label ID="lblUser" runat="server" CssClass="form-label" Text="Username"></asp:Label>
                <asp:TextBox ID="txtUser" runat="server" CssClass="form-control" placeholder="Enter username"></asp:TextBox>
            </div>

            <div class="mb-3">
                <asp:Label ID="lblPass" runat="server" CssClass="form-label" Text="Password"></asp:Label>
                <asp:TextBox ID="txtPass" runat="server" CssClass="form-control" TextMode="Password" placeholder="Enter password"></asp:TextBox>
            </div>

            <div class="mb-3">
                <asp:Label ID="lblRole" runat="server" CssClass="form-label" Text="Role"></asp:Label>
                <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-select">
                    <asp:ListItem Text="Admin" Value="Admin"></asp:ListItem>
                    <asp:ListItem Text="Librarian" Value="Librarian"></asp:ListItem>
                    <asp:ListItem Text="Member" Value="Member"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary btn-custom" OnClick="btnLogin_Click" />

            <div class="mt-3 text-center">
                <asp:Label ID="lblMsg" runat="server" ForeColor="Red" CssClass="fw-bold"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
