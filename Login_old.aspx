<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Page</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f2f2f2;
            margin: 0;
            padding: 50px;
        }
        .login-container {
            width: 300px;
            margin: auto;
            padding: 20px;
            background-color: white;
            border-radius: 5px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }
        .login-container h2 {
            text-align: center;
        }
        .login-container label {
            display: block;
            margin-bottom: 5px;
        }
        .login-container input {
            width: 100%;
            padding: 10px;
            margin-bottom: 10px;
            border: 1px solid #ccc;
            border-radius: 3px;
        }
        .login-container button {
            width: 100%;
            padding: 10px;
            background-color: #4CAF50;
            color: white;
            border: none;
            border-radius: 3px;
        }
        .error {
            color: red;
            text-align: center;
        }

        /* Reset styles */
* {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
}

body {
    font-family: Arial, sans-serif;
    background: linear-gradient(135deg, #6a11cb, #2575fc);
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100vh;
    color: #333;
}

.login-container {
    background: #fff;
    padding: 2rem;
    border-radius: 8px;
    box-shadow: 0 8px 15px rgba(0, 0, 0, 0.1);
    width: 100%;
    max-width: 400px;
}

.login-form h2 {
    text-align: center;
    margin-bottom: 1.5rem;
    color: #333;
}

.form-group {
    margin-bottom: 1rem;
}

.form-group label {
    display: block;
    margin-bottom: 0.5rem;
    font-weight: bold;
    font-size: 0.9rem;
    color: #555;
}

.form-group input {
    width: 100%;
    padding: 0.8rem;
    border: 1px solid #ddd;
    border-radius: 4px;
    font-size: 0.9rem;
}

.form-group input:focus {
    border-color: #2575fc;
    outline: none;
    box-shadow: 0 0 4px rgba(37, 117, 252, 0.2);
}

.btn {
    width: 100%;
    padding: 0.8rem;
    border: none;
    border-radius: 4px;
    background: #6a11cb;
    color: #fff;
    font-size: 1rem;
    font-weight: bold;
    cursor: pointer;
    transition: background 0.3s ease;
}

.btn:hover {
    background: #2575fc;
}

.signup-link {
    text-align: center;
    margin-top: 1rem;
    font-size: 0.9rem;
    color: #555;
}

.signup-link a {
    color: #2575fc;
    text-decoration: none;
}

.signup-link a:hover {
    text-decoration: underline;
}
    </style>
</head>
<body>
    <div class="login-container">
    <form id="form1" runat="server" class="login-form">
        <%--<div class="login-container" align="center">--%>
            <h2>Login</h2>
            <asp:Label ID="lblError" runat="server" CssClass="error" Visible="false"></asp:Label>
            <div align="Center"><label for="txtUsername">Username</label></div>
            <div align="Center"><asp:TextBox ID="txtUsername" runat="server" placeholder="Enter your username"  style="width:250px;" ></asp:TextBox></div>
            <div align="Center"><label for="txtPassword">Password</label></div>
            <div align="Center"><asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Enter your password" style="width:250px;" ></asp:TextBox></div>
            <%--<button type="submit" id="btnLogin" runat="server" OnClick="btnLogin_Click">Login</button>--%>
            <%--<asp:Button ID="btnLogin" runat="server" CssClass="btn-save" Text="Login"  OnClick="btnLogin_Click" Width="270px"/>--%>
        <div align="Center"><asp:Button ID="Button1" runat="server" class="btn" Text="Login"  OnClick="btnLogin_Click" Width="270px"/></div>
        <%--</div>--%>
    </form>
        </div>
</body>
</html>