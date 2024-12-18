<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contententplaceholder1" Runat="Server">
</asp:Content>--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <link rel="stylesheet" href="styles.css" type="text/css" />
    <link href="StyleSheet_new.css" rel="stylesheet" />
    <style>
    body {
        font-family: Arial, sans-serif;
        margin: 0;
    }
    /* Style the menu bar */
    .menu1 {
        background-color: #333;
        overflow: hidden;
        padding: 0;
        margin: 0;
    }

    /* Menu items */
    .menu1 li {
        float: left;
        list-style-type: none;
    }

    /* Links inside the menu */
    .menu1 li a {
        display: block;
        color: white;
        text-align: center;
        padding: 14px 16px;
        text-decoration: none;
    }

    /* Change color on hover */
    .menu1 li a:hover {
        background-color: #111;
    }
    .logout-button {
    padding: 10px 20px;
    background-color: #f44336; /* Red background for visibility */
    color: white;
    border: none;
    border-radius: 5px;
    text-decoration: none;
    font-size: 16px;
    cursor: pointer;
    transition: background-color 0.3s ease;
}

/* Hover effect */
.logout-button:hover {
    background-color: #d32f2f; /* Darker red on hover */
}

/* Positioning in header or navbar */
.header .logout-button {
    margin-left: auto;
}
/* Header and Navbar styling */
.header {
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 10px 20px;
    background-color: #333;
    color: white;
}

.header .nav {
    display: flex;
    align-items: center;
}

.header .nav a {
    color: white;
    text-decoration: none;
    padding: 10px;
}

.header .nav a:hover {
    text-decoration: underline;
}
.right-aligned-table {
    margin-left: auto;
    margin-right: 0;
}
.menu-bar .bold-text {
    font-weight: bold;
}
#Menu {
    text-align: center;
    margin-right:auto;
    margin-left: auto;
}

.StaticHoverStyle {
    text-align: center;
}

</style>
</head>
<body>
    <form id="form1" runat="server">
        
    <div class="form-header" align="center" ><h1> Surplus Management  </h1></div>
    
  <div class="table-responsive">
      <table align="center"></table>
          </div>
                <div style="text-align: right;">
<%--    <asp:LinkButton ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" CssClass="logout-button" 
        OnClientClick="return confirm('Are you sure you want to log out?');"/>  --%>  
</div>    
        <br />
        
        <div> <%--id="dolphincontainer">--%>
        <br />
            <br /><br /><div> <%--id="dolphinnav" >--%>
    <asp:Menu ID="Menu1" runat="server" Orientation="Horizontal"  >
        
    <Items >
        <asp:MenuItem Text="Home" Value="Home" NavigateUrl="~/Home.aspx" ></asp:MenuItem>
        <asp:MenuItem Text="User Master" Value="Usermaster" NavigateUrl="~/Usercontrol.aspx" ></asp:MenuItem>
        <asp:MenuItem Text="Master" Value="master" >
            <asp:MenuItem Text="Disipline" Value="Disipline" NavigateUrl="~/Usercontrol.aspx" ></asp:MenuItem>
            <asp:MenuItem Text="Category" Value="Category" NavigateUrl="~/Usercontrol.aspx" ></asp:MenuItem>
            <asp:MenuItem Text="Disipline Category Mapping" Value="disipCategorymap" NavigateUrl="~/Usercontrol.aspx" ></asp:MenuItem>
            <asp:MenuItem Text="Store Master" Value="StoreMaster" NavigateUrl="~/Usercontrol.aspx" ></asp:MenuItem>
            <asp:MenuItem Text="Project Master" Value="ProjectMaster" NavigateUrl="~/Usercontrol.aspx" ></asp:MenuItem>
         </asp:MenuItem>
       <%-- <asp:MenuItem Text="Services" Value="Services" NavigateUrl="~/Services.aspx"></asp:MenuItem>
        <asp:MenuItem Text="Contact" Value="Contact" NavigateUrl="~/Contact.aspx"></asp:MenuItem>--%>
    </Items>
        
</asp:Menu>
</div><br />
            </div>
        
    </form>
</body>
</html>
<%--CssClass="menu-bar"--%>
