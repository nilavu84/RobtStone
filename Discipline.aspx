<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Discipline.aspx.cs" Inherits="Discipline" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<%--<link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">--%>
<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>
    <title>Inventory Management Form</title>
    <style>
        /* CSS Styling */
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
      
    <form id="form1" runat="server">
     
        <div>
            <div class="form-header">DISCIPLINE MASTER</div>
            
          <div class="table-responsive">
              <table align="center">
                  <td>
                    
            <!-- User Name -->
            <div class="form-group">
                <label for="txtdisiplinneme">Discipline Name:</label>
                <asp:TextBox ID="txtdisiplinneme" runat="server" ></asp:TextBox>
            </div>
                      </td>
                  
                  <td>
                <!-- User Status -->
            <div class="form-group row">
                <label>Status:</label>
                <asp:dropdownlist ID="Userstatus" runat="server" >
                    <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Inactive" Value="0"></asp:ListItem> 
                </asp:dropdownlist>
            </div></td>
             
            </table>
                          <div style="text-align: right;">
<asp:LinkButton ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" CssClass="logout-button" 
    OnClientClick="return confirm('Are you sure you want to log out?');"/>    
              <table align="center">  

                  <tr>
                      <td align="center">
                           
            
            <!-- Save Button -->
            <div class="form-group">
                <asp:Button ID="btnSave" runat="server" CssClass="btn-save" Text="Save"  OnClick="btnSave_Click" />
            </div>
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
                  </tr></table>
                         
<%--<OnClick="btnSave_Click"/>--%>
        </div>                 
            
           
    
</div>
         <div align="center" style="text-align: left;">
             <asp:GridView ID="gvdiscipline" runat="server" Font-Size="13px" AutoGenerateColumns="False" CssClass="grid-view" AllowSorting="True"
                 PageSize="20" CellPadding="3" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
                 <Columns>
                     <asp:BoundField DataField="ItemID" HeaderText="ID" Visible="false" />
                    <asp:BoundField DataField="Item_Name" HeaderText="Item Name" />
                    <asp:BoundField DataField="Activestatus" HeaderText="Active status" />
                  
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
</div>

       
    
    
        </form>
    <script>
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
    <script>
        window.history.pushState(null, document.title, window.location.href);
        window.addEventListener('popstate', function (event) {
            window.history.pushState(null, document.title, window.location.href);
            alert("Navigation is disabled on this page.");
        });
    </script>
    
        <%--<script type="text/javascript">
            function gridViewDoubleClick(row) {
        var cells = row.getElementsByTagName("td");
            document.getElementById('<%= txtUsername.ClientID %>').value = cells[0].innerText.trim();
        document.getElementById('<%= txtpassword.ClientID %>').value = cells[1].innerText.trim();
      
    }
</script>--%>


    

</body>
</html>