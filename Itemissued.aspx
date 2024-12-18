<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Itemissued.aspx.cs" Inherits="Itemissued" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <title> Surplus Management Issued </title>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<link rel="stylesheet" href="styles.css" type="text/css" />
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" rel="stylesheet" />
<link href="//maxcdn.bootstrapcdn.com/font-awesome/4.2.0/css/font-awesome.min.css" rel="stylesheet" />
<link href="https://code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css" rel="stylesheet" />
<link href="//maxcdn.bootstrapcdn.com/bootstrap/3.1.1/css/bootstrap.min.css?parameter=1" rel="stylesheet" />
<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<script src="https://code.jquery.com/ui/1.13.2/jquery-ui.min.js"></script>
<link rel="stylesheet" href="https://code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css" />
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

    .gridview {
        width: 100%;
        border-collapse: collapse;
    }
    .gridview th, .gridview td {
        border: 1px solid #ddd;
        padding: 8px;
    }
    .gridview th {
        background-color: #006699;
        color: #f4f4f9;
        text-align: left;
    }

    .gridview .edit-mode input {
        width: 100%;
        padding: 4px;
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
    color: #006699;
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
.datagrid-class th { background-color: #006699; font-weight: bold; text-align: center;}

  #customModal {
            display: none;
            position: fixed;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            background-color: white;
            padding: 20px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
            z-index: 1000;
            border-radius: 8px;
            text-align: center;
        }
        #modalOverlay {
            display: none;
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(0, 0, 0, 0.5);
            z-index: 999;
        }
        .modal-close {
            margin-top: 10px;
            padding: 8px 16px;
            background-color: #007bff;
            color: white;
            border: none;
            border-radius: 4px;
            cursor: pointer;
        }

    </style>

</head>
<body>

    <!-- Messagebox Modal -->
      <div id="modalOverlay"></div>
    <div id="customModal">
        <p id="modalMessage">This is a message.</p>
        <button class="modal-close" onclick="closeModal()">Close</button>
    </div>

   <form id="form1" runat="server">
                       <table width="100%"><tr class="header">
    <td><img src="Images/RSME%20Logo_14%20(141x41).png" alt="Logo" /></td>
    <td align="center"><div class="logo">Surplus Issued</div></td>
    <td><asp:Label ID="lblUsername" runat="server" CssClass="username-label"></asp:Label>
        <asp:LinkButton ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" 
        CssClass="logout-button" OnClientClick="return confirm('Are you sure you want to log out?');" /></td>
            </tr>
</table>
            
    <div class="row">
        
        
         </div>   
        <br />
       <br />
                     
            <asp:ScriptManager ID="ScriptManager1" runat="server" />

<table width="75%" align="center" border="0" >
    <tr>
        <td width="2%" style="height:50px; text-align:left">
            <label for="ddlItemName">Discipline:</label>
        </td>
        <td width="10%">
            <asp:UpdatePanel ID="updatepanel1" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="ddlItemName" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged" Width="170px"></asp:DropDownList>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlItemName" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
                   
        </td>
        <td width="10%"><label for="ddlCategory">Item Category:</label></td>
        <td width="10%">
            <asp:UpdatePanel ID="updatepanel2" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="ddlCategory" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" Width="170px"></asp:DropDownList>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlCategory" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td width="5%"><label for="ddlItemsize">Item Size:</label></td>
        <td width="10%">
            <asp:UpdatePanel ID="updatepanel3" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="ddlItemsize" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlItemsize_SelectedIndexChanged" Width="170px"></asp:DropDownList>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlItemsize" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
                
     
 
        </td>
                
    </tr>
<%--</table>
            <table width="70%" align="center" border="1">--%>
    <tr>
        <td width="8%" style="height:50px; text-align:left">
            <label for="ddlporefrence">PO Refrence:</label>
        </td>
        <td width="10%">
            <asp:UpdatePanel ID="updatepanel4" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="ddlporefrence" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlporefrence_SelectedIndexChanged"  Width="170px"></asp:DropDownList>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlporefrence" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
                <td width="5%"><label for="ddlCategory">Issued Date:</label></td>
        <td width="10%">
        <asp:UpdatePanel ID="updatepanel6" runat="server">
        <ContentTemplate>
            <%--<asp:DropDownList ID="ddlItemcode" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlItemcode_SelectedIndexChanged"  Width="170px"></asp:DropDownList>--%>
            <asp:TextBox ID="txtDate" runat="server"  CssClass="form-control datepicker" AutoPostBack="true" placeholder="MM/DD/YYYY" Width="64%"></asp:TextBox>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="txtDate"  />
        </Triggers>
    </asp:UpdatePanel>
</td>
        <td width="8%"><label for="ddlCategory">Issued Project:</label></td>
        <td width="10%">
            <asp:UpdatePanel ID="updatepanel5" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="ddlissuedproject" runat="server" class="form-control" AutoPostBack="true"  Width="170px"></asp:DropDownList>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlissuedproject" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
            <%--<span id="issuedprojectError" style="color: red; display: none;">Issue of Project is required.</span>--%>
                
        </td>
        
          </tr>
    </table>

    
            <br />
                                   <table align="center" >  

      <tr>

          <td align="center">
               

<!-- Save Button -->
              </td>
                  <td>
<!-- Save Clear -->
<div class="form-group">
    <asp:Button ID="btSearch" runat="server" class="alt-label" Text="Search" OnClick="btnsearch_Click" OnClientClick="resetButtonColor()"/>
</div>
                  </td>
         <td style="width:20px"></td>
                              <td>
        <!-- back Button -->
        <div class="form-group">
            <asp:Button ID="btClear" runat="server" class="alt-label" Text="Clear"  OnClick="btClear_Click"  />
        </div>
                          </td>
          <td style="width:20px"></td>   
          <td>
        <!-- Export Excel-->
        <div class="form-group">
            <asp:Button ID="btnBack" runat="server" class="alt-label" Text="Back"  OnClick="btnBack_Click" />
        </div>
                          </td>
      </tr></table> 
            <table align="center">
                <tr>
                    <td align="center">
                        <asp:GridView ID="gvInventory" runat="server" AutoGenerateColumns="false"
                            DataKeyNames="Inventory_Id" OnRowUpdating="gvInventory_RowUpdating"  AllowSorting="True"
                            OnRowEditing="gvInventory_RowEditing" OnRowCancelingEdit="gvInventory_RowCancelingEdit"
                            OnSelectedIndexChanged="gvInventory_SelectedIndexChanged" 
                            CellPadding="3" BackColor="White" BorderColor="#CCCCCC" BorderStyle="Ridge"
                            BorderWidth="1px" showfoote="true" Font-Size="13px" PageIndex="0" AllowPaging="True"
                            PageSize="15" OnPageIndexChanging="gvInventory_PageIndexChanging" OnRowCommand="gvInventory_RowCommand" 
                             PagerStyle-CssClass="datagrid" CssClass="datagrid-class">
                            <Columns>
        <asp:BoundField DataField="inventory_id" HeaderText="Entry id"  ReadOnly="true" />
        <asp:BoundField DataField="ItemName" HeaderText="Item Name" ReadOnly="true" />
        <asp:BoundField DataField="ITEMCODE" HeaderText="Item Code" ReadOnly="true" />
        <asp:BoundField DataField="Category" HeaderText="Category" ReadOnly="true" />
        <asp:BoundField DataField="Size_name" HeaderText="Size" ReadOnly="true"  />
        <asp:BoundField DataField="POReferenceNo" HeaderText="PO Reference No" ReadOnly="true" />
        <asp:BoundField DataField="Store_name" HeaderText="Store Name" ReadOnly="true" />
        <%--<asp:BoundField DataField="SurplusQty" HeaderText="Surplus Qty" />--%>
               
        
      
        <asp:TemplateField HeaderText="Available Qty">
            <ItemTemplate>
                <asp:Label ID="lblAvailableQty" runat="server" Text='<%# Eval("AvailableQty") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtAvailableQty" runat="server" Text='<%# Eval("AvailableQty") %>' Enabled="false"  ></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Issued Qty">
            <ItemTemplate>
                <asp:Label ID="lblIssuedQty" runat="server" Text='<%# Eval("QtyIssued") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtIssuedQty" runat="server" Text='<%# Eval("QtyIssued") %>' onkeyup="calculateBalance1(this)" onkeypress="allowOnlyNumbersAndSingleDot(event)"></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Balance Qty">
            <ItemTemplate>
                <asp:Label ID="lblBalanceQty" runat="server" Text='<%# Eval("BalanceQty") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtBalanceQty" runat="server" Text='<%# Eval("BalanceQty") %>' Enabled="false"></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>
        
       
        <asp:CommandField ShowEditButton="True" />
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
                    </td>
                </tr>
            </table>
       <div class="footer">
    &copy; 2024 ROBT STONE. All rights reserved.
</div>
            </form>
  <%--  <script>
        function changeButtonColor() {
            // Find the button and change its color
            const button = document.getElementById('<%= btSearch.ClientID %>');
        button.style.backgroundColor = 'red'; // Set to your desired color
    }

    function resetButtonColor() {
        // Reset the button color
        const button = document.getElementById('<%= btSearch.ClientID %>');
            button.style.backgroundColor = ''; // Reset to default
        }
    </script>--%>
   
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
<script type="text/javascript">
        function calculateBalance(txtAvailableQty) {
            // Get the row containing the text box
            var row = txtAvailableQty.closest('tr');

            // Find the issued quantity label in the same row
            var issuedQtyLabel = row.querySelector('[id*="QtyIssued"]');
            if (!issuedQtyLabel) {
                alert("Issued Quantity Label not found. Please check the ID.");
                return;
            }

            // Find the balance quantity text box
            var balanceQtyTextBox = row.querySelector('[id*="txtBalanceQty"]');
            if (!balanceQtyTextBox) {
                alert("Balance Quantity TextBox not found. Please check the ID.");
                return;
            }

            // Get the values of available and issued quantities
            var availableQty = parseFloat(txtAvailableQty.value) || 0;
            var issuedQty = parseFloat(issuedQtyLabel.innerText) || 0;

            // Calculate the balance quantity
            var balanceQty = availableQty - issuedQty;

            // Update the balance quantity text box
            balanceQtyTextBox.value = balanceQty.toFixed(2); // Set the value with 2 decimal places
        }
    </script>   
   
    <script>

        function calculateBalance1(objIssueQty) {

            var row = objIssueQty.closest('tr');
            var issuedQty = parseFloat(objIssueQty.value) || 0;
          

            try {


                var balanceQtyTextBox = row.querySelector('[id*="txtBalanceQty"]');

                var avlbleQtyTextBox = row.querySelector('[id*="txtAvailableQty"]');
                
               
                balanceQtyTextBox.value = parseFloat(avlbleQtyTextBox.value) - issuedQty;

                if (balanceQtyTextBox.value < 0)
                {
                    alert("Issued Quantity is Morethen Avalilable Quantity");
                    balanceQtyTextBox.value = "";
                    issuedQty.value = "";

                }
            
                
            } catch (e) { alert("Error " + e); }


        }

    </script>

    <script>
       
    </script>
            <script>
                $(document).ready(function () {
                    function initializeCalendar() {
                        $("#txtDate").datepicker({
                            dateFormat: "dd/mm/yy",
                            changeMonth: true,
                            changeYear: true,
                            showAnim: "slideDown"
                        });
                    }

                    // Initialize on page load
                    initializeCalendar();

                    // Show and reinitialize calendar when dropdown changes
                    $("#ddlDatewise").change(function () {
                        $("#txtDate").show(); // Ensure text box is visible
                        initializeCalendar();
                    });

                    // Reinitialize calendar on search button click
                    $("#btnSearch").click(function () {
                        // Simulate search operation
                        setTimeout(function () {
                            initializeCalendar();
                        }, 100);
                    });

                    // Ensure calendar shows on focus
                    $("#txtDate").focus(function () {
                        $(this).datepicker("show");
                    });
                });



               

            </script>
     

</body>
</html>
