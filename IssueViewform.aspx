<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IssueViewform.aspx.cs" Inherits="IssueViewform" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<link href="https://code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css" rel="stylesheet" />
<link href="//maxcdn.bootstrapcdn.com/bootstrap/3.1.1/css/bootstrap.min.css?parameter=1" rel="stylesheet">
<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<script src="https://code.jquery.com/ui/1.13.2/jquery-ui.min.js"></script>
<link rel="stylesheet" href="https://code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css" />



<head runat="server">
    <title> Surplus Issue View Report </title>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<link rel="stylesheet" href="styles.css" type="text/css" />


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
.datagrid-class {         width: 100%;         border-collapse: collapse;     }     
.datagrid-class th,     
.datagrid-class td {                  padding: 5px;     }     
.datagrid-class th { background-color: #006699; font-weight: bold; text-align: center;}
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
            <table width="100%" style="align-content:center">
            <tr class="header">
             
    <td><img src="Images/RSME%20Logo_14%20(141x41).png" alt="Logo" /></td>
    
    <td><center><div class="logo">Surplus Issue Report</div></center></td>
        <td><asp:Label ID="lblUsername" runat="server" CssClass="username-label"></asp:Label>
        <asp:LinkButton ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" 
        CssClass="logout-button" OnClientClick="return confirm('Are you sure you want to log out?');" /></td>
            </tr>      
</table>
              
        
            <asp:ScriptManager ID="ScriptManager1" runat="server" />

<table width="78%" align="center" border="0">
    
     <td width="2%" style="height:80px; text-align:center" >
     <div style="height:30px;" align="center"><label for="ddlItemName">Search Type:</label></div>
        
     <div align="center"><asp:UpdatePanel ID="updatepanel5" runat="server">
         <ContentTemplate>
             <asp:DropDownList ID="ddluniversal" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddluniversal_SelectedIndexChanged" Width="170px">
                        <asp:ListItem Text="Select Search Type" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Project Wise" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Store Location Wise" Value="2"></asp:ListItem> 
                        <asp:ListItem Text="User Wise" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Date Wise" Value="4"></asp:ListItem>
                        <asp:ListItem Text="Entry ID" Value="5"></asp:ListItem>
             </asp:DropDownList>
         </ContentTemplate>
         <Triggers>
             <asp:AsyncPostBackTrigger ControlID="ddlItemName" EventName="SelectedIndexChanged" />
         </Triggers>
     </asp:UpdatePanel></div>
 </td>
    <td width="3%" style="height:50px; text-align:center">
    <div style="height:30px;" align="center"><label for="ddlsearchName">Search :</label></div>
    <div align="center"><asp:UpdatePanel ID="updatepanel6" runat="server">
        <ContentTemplate>
            <asp:DropDownList ID="ddlsearch" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlsearch_SelectedIndexChanged" Width="150px">
            </asp:DropDownList>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlsearch" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
        </div>
            <asp:UpdatePanel ID="updatepanel8" runat="server">
    <ContentTemplate>
        <asp:TextBox ID="txtDate" runat="server"  CssClass="form-control datepicker" AutoPostBack="true" placeholder="DD/MM/YYYY" Width="100%" autocomplete="off" ></asp:TextBox>
        
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="calDate" EventName="SelectionChanged" />
    </Triggers>
              </asp:UpdatePanel>
        <asp:UpdatePanel ID="updatepanel7" runat="server">
    <ContentTemplate>
        <asp:Calendar ID="calDate" runat="server" OnSelectionChanged="calDate_SelectionChanged"  AutoPostBack="true" Visible="true">
            
        </asp:Calendar>
            </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="calDate" EventName="SelectionChanged" />
    </Triggers>
</asp:UpdatePanel>
        
</td>
    
        <td width="4%" style="height:50px; text-align:center"> <asp:CheckBox ID="CheckBox1" runat="server" Text="Specific Search" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" TextAlign="right" />  </td>
    
    <td width="2%" style="height: 50px; text-align: center">
        <div style="height: 30px;" align="center">
            <label for="ddlItemName">Discipline:</label></div>

        <div align="center">
            <asp:UpdatePanel ID="updatepanel1" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="ddlItemName" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged" Width="150px"></asp:DropDownList>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlItemName" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </td>
        <td width="2%" style="height:50px; text-align:center">
            <div style="height:30px;" align="center"><label for="ddlCategory" >Item Category:</label></div>
       
           <div align="center"> <asp:UpdatePanel ID="updatepanel2" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="ddlCategory" runat="server" class="form-control" AutoPostBack="true"  OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" Width="150px"></asp:DropDownList>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlCategory" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
               </div>
        </td>
        <td width="2%" style="height:50px; text-align:center">
            <div style="height:30px;"align="center" ><label for="ddlItemsize">Item Size1:</label></div>
        
          <div align="center">  <asp:UpdatePanel ID="updatepanel3" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="ddlItemsize" runat="server" class="form-control" AutoPostBack="true" Width="150px"></asp:DropDownList>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlItemsize" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
              </div>
        </td>
        <td width="2%" style="height:50px; text-align:center">
            <div style="height:30px;" align="center" ><label for="ddlItemsize">Item Size2:</label></div>
    
           <div align="center"> <asp:UpdatePanel ID="updatepanel4" runat="server">
        <ContentTemplate>
            <asp:DropDownList ID="ddlItemsize1" runat="server" class="form-control" AutoPostBack="true" Width="150px"></asp:DropDownList>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlItemsize1" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel></div>
</td>
        <td width="3%">
            <asp:Button ID="btnsearch" runat="server"  Text="Search"  OnClick="btnsearch_Click" class="label1"/>
            
        </td>
   
</table>
                   <br />
<div> <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false"></asp:Label></div>
            
       <div align="center" >
       <asp:GridView ID="gvInventory" runat="server" Font-Size="13px"
       AutoGenerateColumns="False" AllowSorting="True"
       CellPadding="3" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None"
       BorderWidth="1px" showfoote="true" DataKeyNames="issueid" AllowPaging="True" 
       PageSize="20" PageIndex="0" OnPageIndexChanging="gvInventory_PageIndexChanging" PagerStyle-CssClass="datagrid" CssClass="datagrid-class">
       <Columns>

        <asp:BoundField DataField="issueid" HeaderText="issueid" SortExpression="issueid" Visible="false"/>
       <asp:BoundField DataField="EntryID" HeaderText="Entry ID" />
           <asp:BoundField DataField="IssueDate" HeaderText="Date Of Issued" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" >
               <HeaderStyle Width="100px" />
                <ItemStyle Width="100px" />
                </asp:BoundField>
           <asp:BoundField DataField="Item_Name" HeaderText="Discipline" >
           <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" /> 
           <HeaderStyle HorizontalAlign="Center" />
            </asp:BoundField>
       <asp:BoundField DataField="Category" HeaderText="Item Category" />
       <asp:BoundField DataField="ItemCode" HeaderText="Item Code" />
        <asp:BoundField DataField="ItemDescription" HeaderText="Item Description" >
             <HeaderStyle Width="150px" />
             <ItemStyle Width="450px" />
             <ItemStyle Height="20px" />
             <ItemStyle Font-Size="Small" />
             </asp:BoundField>

       <asp:BoundField DataField="Size1" HeaderText="Size1" />
       <asp:BoundField DataField="Size2" HeaderText="Size2" />
       <asp:BoundField DataField="itemSize3" HeaderText="Size3" />
       <asp:BoundField DataField="ItemUnits" HeaderText="Item Units" />
       <asp:BoundField DataField="POReferenceNo" HeaderText="PO Reference No" />
       <asp:BoundField DataField="Project_code" HeaderText="Project Name" />
       <asp:BoundField DataField="Store_name" HeaderText="Store Name" />
       <asp:BoundField DataField="availableqty" HeaderText="Available Qty" >
           <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" /> 
            <HeaderStyle HorizontalAlign="Center" />
        </asp:BoundField>
           <asp:BoundField DataField="issuedqty" HeaderText="Qty Issued" >
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" /> 
                <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
       <asp:BoundField DataField="balanceqty" HeaderText="Balance Qty" >
           <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" /> 
               <HeaderStyle HorizontalAlign="Center" />
                   </asp:BoundField>
           <asp:BoundField DataField="Username" HeaderText="Issue User" />

   </Columns>
        <FooterStyle BackColor="White" ForeColor="#000066" />
        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
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
        <br />

                        
              
          <td align="center">
               

<!-- Save Button -->
              </td>
                  <td>
<!-- Save Clear -->
<div class="form-group" >
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

    <script>

        function clearDatalist() {

        }

        function clearPreviousResults() {
            // Clear the input field
            const txtDate = document.getElementById('<%= txtDate.ClientID %>');
         
    if (txtDate) {
        txtDate.value = ''; // Reset the input field
        alert(results.innerHTML);
    } else {
                console.error('Input element with ID "<%= txtDate.ClientID %>" not found.');
            }

            // Clear the search results
            const results = document.querySelector('.search-results');
            if (results) {
                alert(results.innerHTML); // Display current innerHTML (optional)
                results.innerHTML = ''; // Clear the search results
            } else {
                console.error('Element with class "search-results" not found.');
            }
        }
        <%--function clearPreviousResults()
        {
            
            dataList = document.getElementById('<%= txtDate.ClientID %>');
            // Clear all options
            //alert(dataList.value);
            //dataList.style.backgroundColor = '#28a745';
            
           
            //alert("Test after");

           // document.getElementById("<%= txtDate.ClientID %>").value = '';
            //const results = document.querySelector('.search-results');
            //if (results) {
              //  alert(results.innerHTML);
               // results.innerHTML = ''; // Clear the search results
            //}
        }--%>



      

    </script>

     <style>
        .form-container {
            margin: 50px;
            font-family: Arial, sans-serif;
        }

        label {
            display: block;
            margin-bottom: 5px;
        }

        .form-control {
            padding: 8px;
            font-size: 14px;
            width: 250px;
        }

        .btn-submit {
            padding: 10px 20px;
            background-color: #007bff;
            color: #fff;
            border: none;
            cursor: pointer;
            font-size: 14px;
        }

        .btn-submit:hover {
            background-color: #0056b3;
        }
    </style>

        

   
        <script>
            $(document).ready(function () {
                function initializeCalendar()
                {
                    const searchOption = document.getElementById('<%= ddluniversal.ClientID %>');
                    if (searchOption.value == 4)
                    {
                        $("#txtDate").datepicker(
                            {
                                dateFormat: "dd/mm/yy",
                                changeMonth: true,
                                changeYear: true,
                                showAnim: "slideDown"
                            });
                    }

                }





                // Initialize on page load
               
    initializeCalendar();

            // Show and reinitialize calendar when dropdown changes
            $("#ddlDatewise").change(function () {
                $("#txtDate").show(); // Ensure text box is visible
               
                clearPreviousResults();
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
                    clearPreviousResults(); 
                $(this).datepicker("show");
    });
});
        </script>
 

    <script>
        document.addEventListener('DOMContentLoaded', function () {
            const button = document.getElementById('<%= btnsearch.ClientID %>');

        button.addEventListener('click', function () {
            button.style.backgroundColor = '#28a745'; // Change to green on click
            setTimeout(() => {
                button.style.backgroundColor = ''; // Reset to original after 1 second
            }, 1000);
        });
    });
    </script>
</body>
</html>