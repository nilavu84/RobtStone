<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Searchpage.aspx.cs" Inherits="Searchpage" Debug="true"  EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title> Surplus View Report </title>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<link rel="stylesheet" href="styles.css" type="text/css" />
    <link href="//maxcdn.bootstrapcdn.com/bootstrap/3.1.1/css/bootstrap.min.css?parameter=1" rel="stylesheet">
<%--<link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" rel="stylesheet" />--%>
<%--<link href="//maxcdn.bootstrapcdn.com/font-awesome/4.2.0/css/font-awesome.min.css" rel="stylesheet" />--%>
     <link href="StyleSheet_new.css" rel="stylesheet" />
        <style> 

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
        /* CSS Styling */

        .custom-button-style {
    background-color: #adc9e6;
    color: white;
    border: none;
    padding: 5px 10px;
    cursor: pointer;
    border-radius: 5px;
    text-decoration: none;
}

.custom-button-style:hover {
    background-color: #db8553;
}

        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f9;
        }
        .form-container {
            max-width: 600px;
            margin: 0 auto;
            padding: 20px;
            background-color: #fff;
            box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
            border-radius: 8px;
        }
        .form-header {
            font-size: 2.5em;
            margin-bottom: 20px;
            color: #006699;            
            text-align: center;
        }
        .form-group {
            display: flex;
            flex-direction: column;
            margin-bottom: 15px;
        }
        .form-group label {
            font-weight: bold;
            margin-bottom: 5px;
        }
        .form-group input[type="text"],
        .form-group select,
        .form-group .aspNetRadioButtonList {
            padding: 8px;
            border: 1px solid #ccc;
            border-radius: 4px;
        }
        .form-group .aspNetRadioButtonList {
            display:flow;
            gap: 15px;
        }
        .form-group .aspNetRadioButtonList label {
            margin-bottom: 0;
        }
        .form-group .btn-save {
            background-color: #4CAF50;
            color: white;
            border: none;
            padding: 10px;
            border-radius: 4px;
            cursor: pointer;
            font-size: 1em;
        }
        .form-group .btn-save:hover {
            background-color: #45a049;
        }
        .required {
  color: red;
}
 
    input:required {
        border: 2px solid red;
    }
        .datagrid {         width: 100%;         border-collapse: collapse;     }  
    .datagrid th, .datagrid td {         border: 1px solid #ddd;         padding: 8px;         text-align: center;     }   
    .datagrid th {         background-color: #f4f4f4;         font-weight: bold;  font-size: 12px;   }    
    .datagrid .pager {         margin: 10px 0;         text-align: center;     }  
    .datagrid .pager a {         padding: 5px 10px;         margin: 0 2px;         text-decoration: none;         color: red; border: 1px solid #ddd; border-radius: 4px; border-block-color:antiquewhite; } 
    .datagrid .pager a:hover { background-color: red; color: white; }
    
    .datagrid tr:hover {         background-color: #f1f1f1;     }     
    .datagrid td {  color: red; font-size: 12px;font-weight:bold;  }
    .datagrid .pager button {
    padding: 5px 10px;
    margin: 0 2px;
    text-decoration: none;
    color: red;
    border: 1px solid #ddd;
    border-radius: 4px;
    background-color: white;
    cursor: pointer;
}

.datagrid .pager button:hover {
    background-color: red;
    color: white;
}

    .btn-search {
        background-color: #007BFF; /* Default button color */
        color: white;
        border: none;
        padding: 10px 15px;
        font-size: 16px;
        cursor: pointer;
        transition: background-color 0.3s ease; /* Smooth color transition */
    }

    .btn-search:hover {
        background-color: #0056b3; /* Highlight color on hover */
    }

    .btn-search:focus {
        outline: 2px solid #0056b3; /* Focus outline for accessibility */
        background-color: #004080; /* Optional focus background */
    }
    /* Base styling for the label */
.label1 {
    font-family: 'Arial', sans-serif; /* Choose a clean and readable font */
    font-size: 15px;                 /* Set font size */
    font-weight: bold;               /* Make it bold for emphasis */
    color: #fff;                     /* White text for contrast */
    background: linear-gradient(135deg, #ff4d4d, #ff7a00); /* Gradient background */
    padding: 10px 20px;              /* Add padding for spacing */
    border-radius: 25px;             /* Rounded edges for a pill-shaped look */
    text-transform: uppercase;       /* All uppercase letters */
    letter-spacing: 2px;             /* Slight spacing between letters */
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2); /* Subtle shadow for depth */
    display: inline-block;           /* Inline label appearance */
    text-align: center;              /* Center text inside */
}

/* Hover effect */
.label1:hover {
    background: linear-gradient(135deg, #ff7a00, #ff4d4d); /* Reverse gradient */
    box-shadow: 0 6px 12px rgba(0, 0, 0, 0.3); /* Deeper shadow on hover */
    transform: scale(1.05);           /* Slight scale-up effect */
    transition: all 0.3s ease-in-out; /* Smooth transition for hover effects */
}

/* Optional animation */
.label1 {
    animation: fadeIn 1s ease-in-out;
}

@keyframes fadeIn {
    0% {
        opacity: 0;
        transform: translateY(-10px);
    }
    100% {
        opacity: 1;
        transform: translateY(0);
    }
}
/* Base styling for the label */
.alt-label {
    font-family: 'Roboto', sans-serif; /* Sleek and modern font */
    font-size: 20px;                  /* Medium size for versatility */
    font-weight: 500;                 /* Semi-bold for subtle emphasis */
    color: #333;                      /* Dark gray text for a clean look */
    background: #f9f9f9;              /* Light background for contrast */
    border: 2px solid #4CAF50;        /* Bold border for structure */
    padding: 8px 16px;                /* Compact padding for a neat look */
    border-radius: 12px;              /* Slightly rounded edges */
    display: inline-block;            /* Inline for flexibility */
    text-transform: capitalize;       /* Keep it clean with capitalized letters */
    letter-spacing: 1px;              /* Subtle spacing for readability */
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1); /* Gentle shadow for depth */
    transition: all 0.3s ease;        /* Smooth transition for effects */
}

/* Hover effect */
.alt-label:hover {
    background: #4CAF50;              /* Switch to green on hover */
    color: #fff;                      /* Text color changes to white */
    border-color: #3e8e41;            /* Darker green border */
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2); /* Deeper shadow */
    transform: translateY(-3px);      /* Subtle lift effect */
    cursor: pointer;                  /* Indicates interactivity */
}

/* Optional: Adding a subtle animation */
.alt-label {
    animation: slideIn 0.5s ease-out;
}

@keyframes slideIn {
    0% {
        opacity: 0;
        transform: translateX(-20px);
    }
    100% {
        opacity: 1;
        transform: translateX(0);
    }
}
/* Header Styles */
.header {
    display: flex;
    align-items: center;
    justify-content: space-between;
    padding: 10px 20px;
    background-color: #0289cc;
    color: white;
}

.header img {
    height: 50px;
}

.header .logout-button {
    background-color:brown;
    color: white;
    border: none;
    padding: 8px 16px;
    font-size: 14px;
    border-radius: 4px;
    cursor: pointer;
    transition: 0.3s ease;
}

.header .logout-button:hover {
    background-color: #d32f2f;
}
/* Form Header */
.form-header {
    text-align: center;
    font-size: 2rem;
    color: #0289cc;
    margin-top: 20px;
}
        .logo {
    font-size: 24px;
    font-weight: bold;
    text-transform: uppercase;
    letter-spacing: 2px;
    color: #FFD700; /* Golden accent color */
}
            /* Footer */
    .footer {
        text-align: center;
        padding: 10px;
        background-color: #0289cc;
        color: white;
        position: fixed;
        bottom: 0;
        width: 100%;
    }
    .username-label {
    font-size: 16px;
    color: white;
    margin-right: 20px;
}
    .cell-padding {
    padding: 5px 10px; /* Top-Bottom, Left-Right */
    
}
    .datagrid-class {         width: 100%;         border-collapse: collapse;     }     
    .datagrid-class th,     
    .datagrid-class td {                  padding: 5px;     }     
    .datagrid-class th { background-color: #006699; font-weight: bold; text-align: center;}
     .custom-checkbox {
        display: none; /* Hide the default checkbox */
    }

    .custom-checkbox + label {
        display: inline-flex;
        align-items: center;
        cursor: pointer;
    }

    .custom-checkbox + label::before {
        content: '';
        display: inline-block !important;
        width: 16px;
        height: 16px;
        border: 2px solid #0289cc;
        border-radius: 4px; /* For rounded checkboxes */
        background: white;
        margin-right: 8px;
        transition: background-color 0.2s, border-color 0.2s;
    }

    .custom-checkbox:checked + label::before {
        background-color: #0289cc;
        border-color: #0289cc;
    }
    #gvInventory table {
       border-collapse: separate; /* Allow spacing between rows */
       border-spacing: 0 10px; /* 10px space between rows */
   }
   #gvInventory td {
       padding: 10px; /* Add padding inside cells for better spacing */
   }
 
</style>
</head>
<body>
            <form id="form1" runat="server">
   
                                                 <table width="100%"><tr class="header">
    <td><img src="Images/RSME%20Logo_14%20(141x41).png" alt="Logo" /></td>
    <td align="center"><div class="logo">Surplus View Form</div></td>
    <td><asp:Label ID="lblUsername" runat="server" CssClass="username-label"></asp:Label>
        <asp:LinkButton ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" 
        CssClass="logout-button" OnClientClick="return confirm('Are you sure you want to log out?');" /></td>
            </tr>
</table>
                
        
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
<hr />
<table width="78%" align="center" border="0">
    <tr>
        <td width="1%" style="height:50px; text-align:right">
            <div ><label for="ddlItemName">Discipline:</label></div>
        </td>
        <td width="2%">
            <asp:UpdatePanel ID="updatepanel1" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="ddlItemName" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged" Width="170px"></asp:DropDownList>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlItemName" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td width="2%" style="height:50px; text-align:right" ><label for="ddlCategory">Item Category:</label></td>
        <td width="3%">
            <asp:UpdatePanel ID="updatepanel2" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="ddlCategory" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" Width="200px"></asp:DropDownList>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlCategory" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td width="2%" style="height:50px; text-align:right"><label for="ddlItemsize">Item Size1:</label></td>
        <td width="3%">
            <asp:UpdatePanel ID="updatepanel3" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="ddlItemsize" runat="server" class="form-control" AutoPostBack="true" Width="150px"></asp:DropDownList>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlItemsize" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td width="2%" style="height:50px; text-align:right"><label for="ddlItemsize">Item Size2:</label></td>
<td width="3%">
    <asp:UpdatePanel ID="updatepanel4" runat="server">
        <ContentTemplate>
            <asp:DropDownList ID="ddlItemsize1" runat="server" class="form-control" AutoPostBack="true" Width="150px"></asp:DropDownList>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlItemsize1" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel></td>
        <td width="3%"> <asp:CheckBox ID="CheckBox1" runat="server" Text="Select All" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />  </td>
        <td width="1%"><asp:Button ID="btnsearch" runat="server" class="label1" Text="Search" AutoPostBack="false" OnClick="btnsearch_Click" /></td>
    </tr>
</table>
            <br />
       <div align="center" >
       <asp:GridView ID="gvInventory" runat="server" Font-Size="13px" 
       AutoGenerateColumns="False" AllowSorting="True"
       CellPadding="5" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None"
       BorderWidth="1px" ShowFooter="true" DataKeyNames="inventory_id" AllowPaging="True"
       PageSize="20" PageIndex="0" OnPageIndexChanging="gvInventory_PageIndexChanging" 
       PagerStyle-CssClass="datagrid" CellSpacing="10" CssClass="datagrid-class">
    <Columns>
        <asp:BoundField DataField="inventory_id" HeaderText="Entry ID" SortExpression="inventory_id" />
        <asp:BoundField DataField="Item_Name" HeaderText="Discipline" >
            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="cell-padding"  /> 
        </asp:BoundField>
        <asp:BoundField DataField="Category" HeaderText="Item Category">
            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="cell-padding" />
        </asp:BoundField>
        <asp:BoundField DataField="ItemCode" HeaderText="Item Code">
            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="cell-padding" />
        </asp:BoundField>
         <asp:BoundField DataField="ItemDescription" HeaderText="Item Description" >
             <HeaderStyle Width="150px" />
             <ItemStyle Width="550px" />
             
             </asp:BoundField>
       
        <asp:BoundField DataField="ItemSize1" HeaderText="Size1">
            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="cell-padding" />
        </asp:BoundField>
        <asp:BoundField DataField="ItemSize2" HeaderText="Size2">
            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="cell-padding" />
        </asp:BoundField>
        <asp:BoundField DataField="ItemSize3" HeaderText="Size3" />
        <asp:BoundField DataField="ItemUnits" HeaderText="Item Units" />
        <asp:BoundField DataField="SurplusQty" HeaderText="Surplus Qty">
            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
        </asp:BoundField>
        <asp:BoundField DataField="POReferenceNo" HeaderText="PO Reference No">
            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="cell-padding" />
        </asp:BoundField>
        <asp:BoundField DataField="CertificateAvailable" HeaderText="Certificate Available">
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
        </asp:BoundField>
        <asp:BoundField DataField="Store_name" HeaderText="Store Name">
            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="cell-padding" />
        </asp:BoundField>
        <asp:BoundField DataField="Store_location" HeaderText="Store location" />
        <asp:BoundField DataField="Project_name" HeaderText="Project Name" />
        <asp:BoundField DataField="issuedqty" HeaderText="Issued Qty">
            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" CssClass="cell-padding"/>
        </asp:BoundField>
        <asp:BoundField DataField="BalanceQty" HeaderText="Available Qty">
            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" CssClass="cell-padding" />
        </asp:BoundField>
    </Columns>
    <FooterStyle BackColor="White" ForeColor="#000066" />
    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle"/>
     <PagerSettings         Mode="NumericFirstLast"         PreviousPageText="Prev"         NextPageText="Next" />
    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
    <RowStyle ForeColor="#000066" />
    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
    <SortedAscendingCellStyle BackColor="#F1F1F1" />
    <SortedAscendingHeaderStyle BackColor="#007DBB" />
    <SortedDescendingCellStyle BackColor="#CAC9C9" />
    <SortedDescendingHeaderStyle BackColor="#00547E" />
</asp:GridView>

           
        </div>
                       <table align="center">  

      <tr>

          <td align="center">
               
<br />
              <br />
              <hr />
<!-- Save Button -->
              </td>
                  <td>
<!-- Save Clear -->
<div class="form-group">
    <asp:Button ID="btClear" runat="server" class="alt-label" Text="Clear"  OnClick="btnClear_Click" />
</div>
                  </td>
          <td style="width:30px"></td>
                  <td>
        <!-- Export Excel-->
        <div class="form-group">
            <asp:Button ID="btnExportToExcel" runat="server" class="alt-label" Text="Export"  OnClick="btnExportToExcel_Click" />
        </div>
                          </td>
          <td style="width:30px"></td>
            <td>
<!-- back Button -->
<div class="form-group">
    <asp:Button ID="btBack" runat="server" class="alt-label" Text="Back"  OnClick="btnBack_Click" />
</div>
                  </td>
      </tr></table> 
                <div class="footer">
    &copy; 2024 ROBT STONE. All rights reserved.
</div>
            </form>
    <script>
    var idleTime = 0;

    // Increment the idle time counter every minute
    var idleInterval = setInterval(timerIncrement, 60000); // 1 minute

    // Reset the idle timer on user activity
    document.onmousemove = resetTimer;
    document.onkeypress = resetTimer;
    document.onscroll = resetTimer;
    document.onclick = resetTimer;

    function timerIncrement() {
        idleTime++;
        if (idleTime >= 10) { // 10 minutes
            // Redirect to logout page
            window.location.href = 'Logout.aspx';
        }
    }

    function resetTimer() {
        idleTime = 0;
    }
    </script>

  
        
</body>
</html>
