<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewPage.aspx.cs" enableEventValidation="false"   Inherits="ViewPage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" >
    
<%--<link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">--%>
<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>
<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js"></script>
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <title>Surplus Management</title>
    <style>
        /* CSS Styling */
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
        .password-box {
    width: 300px;
    padding: 10px;
    border: 1px solid #ccc;
    border-radius: 5px;
    font-size: 16px;
}

    </style>
</head>
<body>
    <form id="form1" runat="server" >
        
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <ContentTemplate>
       <div class="row">
<Center>
             <div class="col-sm-12" >
     <label  class="form-header">  Surplus Management Report </label>
   
   </div>
   </Center>
        
</div>    
<div style="text-align: left;">
    <img src="Images/RSME%20Logo_14%20(141x41).png" /> </div>
    
            
                             <div style="text-align: right;">
<asp:LinkButton ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" CssClass="logout-button" 
    OnClientClick="return confirm('Are you sure you want to log out?');"/>    
              </div>
        <table align="left">
            <tr>
         
         <%--<td width="20%" style ="height:50px;text-align:left"><label for="lblsearchName" Discipline:></label></td>--%>
         <td width="30%"> <asp:DropDownList ID="lblsearchName" runat="server" class="form-control" AutoPostBack="True" Visible="false">  
            <asp:ListItem Text="Discipline wise Search" Value="0"></asp:ListItem>
            <asp:ListItem Text="Category wise Search" Value="1"></asp:ListItem>    
            <asp:ListItem Text="Item Code wise Search" Value="2"></asp:ListItem>
            <asp:ListItem Text="PO Number wise Search" Value="3"></asp:ListItem> 
             <asp:ListItem Text="Store wise Search" Value="4"></asp:ListItem>
             <asp:ListItem Text="Project wise Search" Value="5"></asp:ListItem>
             </asp:DropDownList>
         </td>
            
         <td width="20%" style ="height:50px;text-align:right"><label> Discipline:</label>
             <asp:DropDownList ID="ddlDiscipline" runat="server" class="form-control" AutoPostBack="false"  onchange="loadDropdown(this.value)" ></asp:DropDownList>
            </td>
                <td>
             <%--<td width="20%" style ="height:50px;text-align:right">--%>
             <label> Category:</label>
         <asp:DropDownList ID="ddlCategory" runat="server" class="form-control" AutoPostBack="false" onchange="loadDropdown1(2)" ></asp:DropDownList>  </td>
                   <td> <label> Item Size:</label>
        <asp:DropDownList ID="ddlItemSize" runat="server" class="form-control" AutoPostBack="false"  ></asp:DropDownList>  </td>
         <asp:textbox ID="ddlsearchName" runat="server" class="form-control" AutoPostBack="false" Visible="false"></asp:textbox>

         <asp:textbox ID="Textbox1" runat="server" class="form-control"  Text="1" Visible="true"></asp:textbox>
        <asp:textbox ID="Textbox2" runat="server" class="form-control"  Text="1" Visible="true"></asp:textbox>

         <td ><asp:Button ID="btnsearch" runat="server" CssClass="btn-search" Text="Search" AutoPostBack="false" OnClick="btnsearch_Click" /></td>
                    
    </tr></table>
        
                            <div align="center" >
                    
                       <asp:GridView ID="gvInventory" runat="server" Font-Size="13px" 
            
                AutoGenerateColumns="False" CssClass="grid-view" AllowSorting="True" 
                CellPadding="3" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" 
                BorderWidth="1px" showfoote="true" DataKeyNames="inventory_id" >
                               <Columns>

        <asp:BoundField DataField="inventory_id" HeaderText="inventory_id" SortExpression="inventory_id" Visible="false"/>
       <asp:BoundField DataField="Item_Name" HeaderText="Discipline" >
           <ItemStyle HorizontalAlign="Left" VerticalAlign="Bottom" /> 
           <HeaderStyle HorizontalAlign="Center" />
            </asp:BoundField>
        
       <asp:BoundField DataField="Category" HeaderText="Item Category" />
       <asp:BoundField DataField="ItemCode" HeaderText="Item Code" />
     
       <asp:BoundField DataField="ItemDescription" HeaderText="Item Description" />
       <asp:BoundField DataField="ItemSize" HeaderText="Item Size" />
       <asp:BoundField DataField="ItemUnits" HeaderText="Item Units" />

       <asp:BoundField DataField="SurplusQty" HeaderText="Surplus Qty" >
                  <ItemStyle HorizontalAlign="Right" VerticalAlign="Bottom" /> 
                   <HeaderStyle HorizontalAlign="Center" />
               </asp:BoundField>
                   
       <asp:BoundField DataField="POReferenceNo" HeaderText="PO Reference No" />
       <asp:BoundField DataField="CertificateAvailable" HeaderText="Certificate Available" >
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Bottom" /> 
                   <HeaderStyle HorizontalAlign="Center" />
</asp:BoundField>
       <asp:BoundField DataField="Store_name" HeaderText="Store Name" />
       <asp:BoundField DataField="Store_location" HeaderText="Store location" />
       <asp:BoundField DataField="Project_name" HeaderText="Project Name" />
       <asp:BoundField DataField="DateOfIssued" HeaderText="Date Of Issued"  DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" />
       <asp:BoundField DataField="QtyIssued" HeaderText="Qty Issued" >
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Bottom" /> 
                <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
       <asp:BoundField DataField="BalanceQty" HeaderText="Balance Qty" >
           <ItemStyle HorizontalAlign="Right" VerticalAlign="Bottom" /> 
               <HeaderStyle HorizontalAlign="Center" />
                   </asp:BoundField>
   </Columns>
        <FooterStyle BackColor="White" ForeColor="#000066" />
        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
        <RowStyle ForeColor="#000066" />
        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#007DBB" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#00547E" />
</asp:GridView>
                  
                 
         
        </div>
            <br />

           
              <table align="center">  

      <tr>

          <td align="center">
               

<!-- Save Button -->
              </td>
                  <td>
<!-- Save Clear -->
<div class="form-group">
    <asp:Button ID="btClear" runat="server" CssClass="btn-save" Text="Clear"  OnClick="btnClear_Click" />
</div>
                  </td>
                            <td>
        <!-- back Button -->
        <div class="form-group">
            <asp:Button ID="btBack" runat="server" CssClass="btn-save" Text="Back"  OnClick="btnBack_Click" />
        </div>
                          </td>
                                      <td>
        <!-- Export Excel-->
        <div class="form-group">
            <asp:Button ID="btnExportToExcel" runat="server" CssClass="btn-save" Text="Export"  OnClick="btnExportToExcel_Click" />
        </div>
                          </td>
      </tr></table>
        </ContentTemplate>
        
     </form>
</body>
    <script type="text/javascript">
    window.onbeforeunload = function (e) {
        // Send a request to a server-side handler to clear the session
        navigator.sendBeacon('login.aspx');
    };
</script>
    <script type="text/javascript">
        function dropdownChanged() {
            try {
                alert("Selected value: ");
                var selectedValue1 = document.getElementById('<% =ddlDiscipline.ClientID %>').value;

                var drp1 = document.getElementById("ddlCategory");
                var newOption = document.createElement("option");
                newOption.text = "New Item Text"; // Text to display
                newOption.value = "NewItemValue"; // Value to store

                // Add the new option to the dropdown
                drp1.add(newOption);
                drp1.addItem ()

                alert("Selected value: " + selectedValue1);
            }
            catch (e) { alert(e); }
        }
</script>
    <script type="text/javascript">
        function updateContent() {
            var selectedValue = $('#<%= ddlDiscipline.ClientID %>').val();
            $.ajax({
                type: "POST",
                url: "ViewPage.aspx/GetSelectedValue",
                data: JSON.stringify({ itemID: selectedValue }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    $('#resultArea').html(response.d);
                },
                error: function (error) {
                    console.log("Error: ", error);
                    $('#resultArea').html("An error occurred.");
                }
            });
        }


        
        function loadDropdown(itemID) {
            $.ajax({
                type: "POST",
                url: "getItemSize1.aspx/GetcategoryID",
                data: JSON.stringify({ parameter: itemID, mthd: 7 }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var dropdown = document.getElementById("ddlCategory");
                    dropdown.innerHTML = ""; // Clear existing items

                    var defaultOption = document.createElement("option");
                    defaultOption.text = "--Select Category--";
                    defaultOption.value = "";
                    dropdown.add(defaultOption);

                    var data = response.d;
                    data.forEach(function (item) {
                        var option = document.createElement("option");
                        option.text = item.Text;
                        option.value = item.Value;
                        dropdown.add(option);
                    });
                },
                error: function (xhr, status, error) {
                    console.error("Error: " + error);
                }
            });
        }
        function loadDropdown1(categoryID) {
            $.ajax({
                type: "POST",
                url: "getItemSize1.aspx/GetcategoryID",
                data: JSON.stringify({ parameter: categoryID,mthd: 8 }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var dropdown = document.getElementById("ddlItemSize");
                    dropdown.innerHTML = ""; // Clear existing items

                    var defaultOption = document.createElement("option");
                    defaultOption.text = "--Select Item Size--";
                    defaultOption.value = "";
                    dropdown.add(defaultOption);

                    var data = response.d;
                    data.forEach(function (item) {
                        var option = document.createElement("option");
                        option.text = item.Text;
                        option.value = item.Value;
                        dropdown.add(option);
                    });
                },
                error: function (xhr, status, error) {
                    console.error("Error: " + error);
                }
            });
        }
        document.getElementById('ddlDiscipline').addEventListener('change', function () {
            var itemID = this.value;
            loadDropdown(itemID, 'CategoryID');
        });

        document.getElementById('ddlCategory').addEventListener('change', function () {
            var itemID = this.value;
            loadDropdown1(itemID);
        });
        

    </script>
</html>
