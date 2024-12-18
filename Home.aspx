<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<%--<link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">--%>
<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>

    <link href="//maxcdn.bootstrapcdn.com/bootstrap/3.1.1/css/bootstrap.min.css?parameter=1" rel="stylesheet">
    <%--<link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" rel="stylesheet" />--%>
    <link href="//maxcdn.bootstrapcdn.com/font-awesome/4.2.0/css/font-awesome.min.css" rel="stylesheet" />
    <title>Surplus Management</title>
    <style type="text/css">
     .hidden
     {
         display:none;
     }
</style>
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
    input:required {
        border: 2px solid red;
    }
    .datagrid {         width: 100%;         border-collapse: collapse;     }  
    .datagrid th, .datagrid td {         border: 1px solid #ddd;         padding: 8px;         text-align: left;     }   
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
    .username-label {
    font-size: 16px;
    color: white;
    margin-right: 20px;
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
    .datagrid-class {         width: 100%;         border-collapse: collapse;     }     
.datagrid-class th,     
.datagrid-class td {                  padding: 5px;     }     
.datagrid-class th { background-color: #006699; font-weight: bold; text-align: center;color: white; }

/* General styles for the message box */
.message-box {
    position: fixed;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    background-color:#006699;
    color: #ffffff;
    border: 1px solid #f5c6cb;
    padding: 20px;
    border-radius: 8px;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
    z-index: 1000;
    width: 300px;
    text-align: center;
    font-family: 'Roboto', sans-serif;
}

/* Style the button inside the message box */
.message-box button {
    margin-top: 15px;
    padding: 8px 15px;
    background-color: brown;
    color: #ffffff;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    font-size: 14px;
}

.message-box button:hover {
    background-color: #e2aeb3;
}

/* Hidden class to hide the message box */
.hidden {
    display: none;
}
 #gvInventory table {
        border-collapse: separate; /* Allow spacing between rows */
        border-spacing: 0 10px; /* 10px space between rows */
    }
    #gvInventory td {
        padding: 10px; /* Add padding inside cells for better spacing */
    }
 
    </style>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
   
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%"><tr class="header">
    <td><img src="Images/RSME%20Logo_14%20(141x41).png" alt="Logo" /></td>
    <td align="center"><div class="logo">Surplus Entry Form</div></td>
    <td><asp:Label ID="lblUsername" runat="server" CssClass="username-label"></asp:Label>
        <asp:LinkButton ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" 
        CssClass="logout-button" OnClientClick="return confirm('Are you sure you want to log out?');" /></td>
            </tr>
</table>
                  
     <table align="center" border="0"> <tr><td width="20%" style ="height:50px;text-align:left"><label for="ddlItemName" >Discipline:</label>
         <span class="required">*</span>
                                           </td>
         <td width="20%">      <asp:DropDownList ID="ddlItemName" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged"></asp:DropDownList>
             <%--<span id="ItemNameError" style="color: red; display: none;">Discipline is required.</span>--%>
             <div id="messageBox1" class="message-box hidden">
                <p>Discipline is required.</p>
                    <button onclick="hideMessage('messageBox1')">OK</button></div>
         </td>
         <td width="20%"></td>
         <td width="20%"><label for="ddlCategory">Item Category:</label> <span class="required">*</span></td>
         <td width="20%">            
             <asp:DropDownList ID="ddlCategory" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" > </asp:DropDownList>
                <div id="messageBox2" class="message-box hidden">
   <p>Category is required.</p>
       <button onclick="hideMessage('messageBox2')">OK</button></div>
             <%--<span id="CategoryError" style="color: red; display: none;">Item Category is required.</span></td>--%>            </tr> 
           
     <tr> <td width="20%" style ="height:50px;text-align:left"><label for="txtItemCode">Item Code:</label>
    </td>
         <td><asp:TextBox ID="txtItemCode" class="form-control" runat="server"></asp:TextBox>
                <span id="itemCodeError" style="color: red; display: none;">Item code is required.</span>

         </td>
         <td width="20%"></td> 
         <td> <label for="txtItemDescription">Item Description:</label></td>
         <td> <asp:TextBox ID="txtItemDescription" class="form-control" runat="server" TextMode="MultiLine" MaxLength="5000"></asp:TextBox></td> </tr>          
            
     <tr><td width="20%"><label for="txtItemSize">Item Size 1:</label></td> <td width="20%"><asp:DropDownList ID="ddlItemSize" class="form-control" runat="server" ></asp:DropDownList></td> 
         <td width="20%"></td>  
         <td width="20%"><label for="txtItemSize1">Item Size 2:</label></td> <td width="20%"><asp:DropDownList ID="ddlItemSize1" class="form-control" runat="server" ></asp:DropDownList></td> 
    </tr>
    
     <tr> 
         <td width="20%" style ="height:50px;text-align:left"><label for="txtItemSize1">Item Size 3:</label></td> <td width="20%"><asp:textBox ID="txtsize3" class="form-control" runat="server" ></asp:textBox></td>
         <td width="20%"></td>
         <td width="20%" ><label for="ddlItemUnits">Item Units:</label></td> 
         <td width="20%"> <asp:DropDownList ID="ddlItemUnits" class="form-control" runat="server" ></asp:DropDownList></td>
         </tr>
    <tr>
         <td width="20%" style ="height:50px;text-align:left"> <label for="txtSurplusQty">Surplus Qty:</label> <span class="required">*</span>
             <div id="messageBox" class="message-box hidden">
                <p>Surplus Qty is required.</p>
                <button onclick="hideMessage('messageBox')">OK</button></div>
         </td> 
         <td width="20%"> <asp:TextBox ID="txtSurplusQty" class="form-control" runat="server" onkeypress="allowOnlyNumbersAndSingleDot(event)" OnKeyUp="calculateBalanceQty()" ></asp:TextBox>
         </td> 
         <td width="20%"></td>
         <td width="20%" ><label for="txtPOReferenceNo">PO Reference No:</label></td> <td width="20%"> <asp:TextBox ID="txtPOReferenceNo" class="form-control" runat="server"></asp:TextBox></td>     
        </tr>
    <tr>
        <td width="20%" style ="height:50px;text-align:left"> <label for="txtPOItemNumber">PO Item Number:</label>
        </td>  
         <td width="20%"><asp:TextBox ID="txtPOItemNumber" class="form-control" runat="server"></asp:TextBox></td>  
         <td width="20%"></td>  
         <td width="20%"><label>Certificate Available:</label></td>  
         <td width="20%"><asp:DropDownList ID="ddlCertificateAvailable" class="form-control" runat="server" >
                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                <asp:ListItem Text="No" Value="0"></asp:ListItem> 
                            </asp:DropDownList></td>  
        </tr>
    <tr>
         
        <td width="20%" style ="height:50px;text-align:left"> <label for="ddlStoreLocation">Store Name:</label></td> 
        <td width="20%"> <asp:DropDownList ID="ddlStoreLocation" class="form-control" runat="server"></asp:DropDownList></td> 
        
        <%--<td width="20%" style ="height:50px;text-align:left" > <label for="ddlIssuedProjectNo">Issued Project NO:</label> </td>--%> 
        <td width="20%"> <asp:DropDownList ID="ddlIssuedProjectNo" class="form-control" runat="server" Visible="false"></asp:DropDownList></td> 
   
    
             <td colspan="2" style="text-align: center;">
    <asp:Button ID="btnSearch" runat="server"  class="alt-label"  Text="Search"  OnClientClick="return validateForm();" OnClick="btnSearch_Click" />
                 
  <%--  <asp:RequiredFieldValidator ID="SurplusQtyValidator" runat="server" 
    ControlToValidate="SurplusQtyTextBox" ErrorMessage="Surplus Qty is required." 
    ForeColor="Red" Display="Dynamic" />--%>
                 <asp:Button ID="btnSave" runat="server"  class="alt-label"  Text="Save"  OnClientClick="return validateForm();" OnClick="btnSave_Click" />
             
    <asp:Button ID="btClear" runat="server"  class="alt-label"  Text="Clear"  OnClick="btnClear_Click" />
    <asp:Button ID="btBack" runat="server"  class="alt-label" Text="Back"  OnClick="btnBack_Click" />
    </td>
      
    </tr>
    <tr>
        
        <td width="20%"> <asp:TextBox ID="txtDateOfIssued" class="form-control" placeholder="DD-MM-YYYY" maxlength="10" runat="server" Visible="false" ></asp:TextBox></td> 
        <td width="20%"></td>
       
         
         </tr>

     
     </table>
             
    <hr />
                         
                    
              
          <table >      
                <div align="center" >
                    
                       <asp:GridView ID="gvInventory" runat="server" Font-Size="13px" 
                AutoGenerateSelectButton="True" 
                AutoGenerateColumns="False"  AllowSorting="True" OnRowEditing="gvInventory_RowEditing"
                OnRowCancelingEdit="gvInventory_RowCancelingEdit" OnRowUpdating="gvInventory_RowUpdating"
                CellPadding="3" BackColor="White" BorderColor="#CCCCCC" BorderStyle="Ridge" 
                BorderWidth="1px" showfoote="true" DataKeyNames="inventory_id" 
                onselectedindexchanged="gvInventory_SelectedIndexChanged" OnRowDataBound="gvInventory_RowDataBound" 
                PageIndex="0" AllowPaging="True" OnPageIndexChanging="gvInventory_PageIndexChanging" 
                PageSize="20" PagerStyle-CssClass="datagrid" ViewStateMode="Inherit" CssClass="datagrid-class">
                <Columns>

         <asp:BoundField DataField="inventory_id" HeaderText="inventory_id" SortExpression="inventory_id" Visible="false"/>
        <asp:BoundField DataField="Item_Name" HeaderText="Discipline" >
            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" /> 
            <HeaderStyle HorizontalAlign="Center" />
             </asp:BoundField>
         
        <asp:BoundField DataField="Category" HeaderText="Item Category" />
        <asp:BoundField DataField="ItemCode" HeaderText="Item Code" />
      
        <asp:BoundField DataField="ItemDescription" HeaderText="Item Description" >
            <HeaderStyle Width="150px" />
            <ItemStyle Width="550px" />
            <ItemStyle Height="20px" />
            </asp:BoundField>

        <asp:BoundField DataField="ItemSize1" HeaderText="Size1" />
        <asp:BoundField DataField="ItemSize2" HeaderText="Size2" />
        <asp:BoundField DataField="ItemSize3" HeaderText="Size3" />
        <asp:BoundField DataField="ItemUnits" HeaderText="Item Units" />

        <asp:BoundField DataField="SurplusQty" HeaderText="Surplus Qty" >
                   <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" /> 
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
                    
        <asp:BoundField DataField="POItemNumber" HeaderText="PO Item Number" />
        <asp:BoundField DataField="POReferenceNo" HeaderText="PO Reference No" />
        <asp:BoundField DataField="CertificateAvailable" HeaderText="Certificate Available" >
                     <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" /> 
                    <HeaderStyle HorizontalAlign="Center" />
 </asp:BoundField>
        <asp:BoundField DataField="Store_name" HeaderText="Store Name" />
       <%-- <asp:BoundField DataField="Project_name" HeaderText="Project Name" />--%>
     
    </Columns>
        <FooterStyle BackColor="White" ForeColor="#000066" />
        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
        <PagerSettings Mode="NumericFirstLast" PreviousPageText="Prev" NextPageText="Next" />
        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
        <RowStyle ForeColor="#000066" />
        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#007DBB" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#00547E" />
</asp:GridView>
        <br />
        <br />
        <br />
        </div>
     </table>
  <script type="text/javascript">
      function gridViewDoubleClick(rowIndex) {
          __doPostBack('GridViewDoubleClick', rowIndex);
      }
</script>
        
           
        <script>
            function allowOnlyNumbersAndSingleDot(event) {
                const key = event.key;
                const input = event.target.value;

                // Allow numbers, a single dot, and control keys (backspace, delete, etc.)
                if (
                    !isNaN(key) ||
                    key === "." && !input.includes(".") ||
                    event.ctrlKey ||
                    event.metaKey ||
                    ["Backspace", "Tab", "ArrowLeft", "ArrowRight", "Delete"].includes(key)
                ) {
                    return true;
                }

                // Prevent other input
                event.preventDefault();
            }
        </script>

        <script>
            function validateSurplusQty() {
                var surplusQty = document.getElementById("SurplusQtyInput").value.trim();
                var errorSpan = document.getElementById("SurplusQtyError");
                if (surplusQty === "") {
                    errorSpan.style.display = "inline"; // Show error message
                    return false; // Prevent form submission
                } else {
                    errorSpan.style.display = "none"; // Hide error message
                    return true; // Allow form submission
                }
            }
        </script>
<%--<form id="SurplusForm" runat="server" onsubmit="return validateSurplusQty();">
    <input type="text" id="SurplusQtyInput" name="SurplusQty" />
    <span id="SurplusQtyError" style="color: red; display: none;">Surplus Qty is required.</span>
    <button type="submit">Submit</button>
</form>--%>


                  <script type="text/javascript">
                      
                  function triggerLogout() {
    document.getElementById('<%= btnLogout.ClientID %>').click();
}

// Optionally, you can call this function when needed, such as on a page unload event:
window.addEventListener('unload', function () {
    Session.Clear();
    Session.Abandon();
    System.Web.Security.FormsAuthentication.SignOut();
    sessionLogout();
});
                  </script>
         <%-- <script type="text/javascript">
              function calculateBalanceQty() {
                  var surplusQty = parseFloat(document.getElementById('<%= txtSurplusQty.ClientID %>').value) || 0;
                  var issueQty = parseFloat(document.getElementById('<%= txtQtyIssued.ClientID %>').value) || 0;
                  // Check if issueQty exceeds surplusQty
                  if (issueQty > surplusQty) {
                      alert('Issued Quantity cannot exceed Surplus Quantity.');
                      document.getElementById('<%= txtQtyIssued.ClientID %>').value = ''; // Clear the issueQty field
                        document.getElementById('<%= txtBalanceQty.ClientID %>').value = surplusQty; // Reset balanceQty
                        return; // Exit the function early
                        }

                         // Calculate balanceQty
                        var balanceQty = surplusQty - issueQty;
                  document.getElementById('<%= txtBalanceQty.ClientID %>').value = balanceQty;
                  //var balanceQty = surplusQty - issueQty;

                  //document.getElementById('<%= txtBalanceQty.ClientID %>').value = balanceQty;
              }
          </script>--%>
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
        <script>
            function showMessage() {
                const messageBox = document.getElementById('messageBox');
                messageBox.classList.remove('hidden');
            }

         
            function showMessage1() {
                const messageBox = document.getElementById('messageBox1');
                messageBox.classList.remove('hidden');
            }

         
            function showMessage2() {
                const messageBox = document.getElementById('messageBox2');
                messageBox.classList.remove('hidden');
            }

            function hideMessage(msgbox) {
                const messageBox = document.getElementById(msgbox);
                messageBox.classList.add('hidden');
            }

        </script>
    <div class="footer">
    &copy; 2024 ROBT STONE. All rights reserved.
</div>
        </form>
    <script  type="text/javascript">
    $(document).ready(function() {
        $(".datepicker").datepicker({
            dateFormat: "yy-mm-dd"  // Set format to match the SQL date format
        });
    });
</script>
    <script type="text/javascript">
        function allowOnlyNumbers(event) {
            var charCode = event.which ? event.which : event.keyCode;
            if (charCode < 48 || charCode > 57) {
                event.preventDefault();
            }
        }
    </script>
    <script  type="text/javascript">
        window.history.pushState(null, document.title, window.location.href);
        window.addEventListener('popstate', function (event) {
            window.history.pushState(null, document.title, window.location.href);
            alert("Navigation is disabled on this page.");
        });
    </script>
   
</body>
</html>
