using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Activities.Expressions;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Home : Page
{
    public static String id = "";
    int itemID; int categoryID;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["Username"] == null) // Check if the session has expired
        {
            Response.Redirect("Login.aspx");
        }
        string username = Globalfunction.GetLoggedInUsername();
        if (!string.IsNullOrEmpty(username))
        {
            // Use the username for displaying or other logic
            lblUsername.Text = "Hello, " + username;
        }
        sessionCheck();
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
        Response.Cache.SetNoStore();
        if (!IsPostBack)
        {
            LoadItemNames();
            storedetails();
            Projectdetails();
            LoadItemUnit();
            gvInventory.Columns[0].Visible = false;
            //BindGrid();
            BinfGrid_new();
            if (Application["Username"] != null)
            {
                lblUsername.Text = "Welcome, " + Application["Username"].ToString();
            }
            else
            {
                // Redirect to login page if no user is logged in
                Session.Clear();
                Session.Abandon();
                System.Web.Security.FormsAuthentication.SignOut();
                sessionLogout();
                Response.Redirect("~/Login.aspx");
            }
        }

    }


    public void sessionCheck()
    {
        //string username = Session["UserName"].ToString(); // Use Trim to remove any leading/trailing whitespace

        Session["Sessionid"] = Session.SessionID;

        // Get the connection string from Web.config
        string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Define the command and specify the stored procedure
            using (SqlCommand command = new SqlCommand("useauthentication", connection))
            {
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters
                command.Parameters.AddWithValue("@SessionId", Session["Sessionid"]);
                command.Parameters.AddWithValue("@case", 5);  // Indicating this is an update operation

                // Execute the command and check how many rows were affected
                int rowsAffected = command.ExecuteNonQuery();

                // Check if the update was successful
                if (rowsAffected == 0)
                {
                    Session.Clear();
                    Session.Abandon();
                    System.Web.Security.FormsAuthentication.SignOut();
                    sessionLogout();
                    Response.Redirect("~/Login.aspx");
                }

            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        sessionCheck();
        ValidateSurplusQty();


    }
    protected void rec_save()
    {
        if (ddlItemName.SelectedIndex != 0)
        {
            if (ddlCategory.SelectedIndex != 0)
            {
                // Get the connection string from Web.config
                string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Define the command and specify the stored procedure
                    using (SqlCommand command = new SqlCommand("ITEM_INSERT", connection))
                    {
                        Session["Sessionid"] = Session.SessionID;

                        command.CommandType = CommandType.StoredProcedure;
                        if (btnSave.Text == "Save")
                        {
                            command.Parameters.AddWithValue("@action", "Insert");
                        }
                        else if (btnSave.Text == "Update")
                        {
                            command.Parameters.AddWithValue("@action", "Update");
                            command.Parameters.AddWithValue("@Inventory_Id", id);

                        }
                        else
                        {
                            command.Parameters.AddWithValue("@action", "");
                        }

                        command.Parameters.AddWithValue("@ItemCode", txtItemCode.Text);
                        command.Parameters.AddWithValue("@ItemDescription", txtItemDescription.Text);

                        if (!string.IsNullOrEmpty(ddlItemSize.SelectedValue))
                        {
                            command.Parameters.AddWithValue("@ItemSize1", ddlItemSize.SelectedValue.ToString());
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@ItemSize1", DBNull.Value);
                        }

                        command.Parameters.AddWithValue("@ItemUnits", ddlItemUnits.SelectedItem.Value);

                        if (!string.IsNullOrEmpty(txtSurplusQty.Text))
                        {
                            command.Parameters.AddWithValue("@SurplusQty", txtSurplusQty.Text);
                        }

                        command.Parameters.AddWithValue("@POReferenceNo", txtPOReferenceNo.Text);
                        command.Parameters.AddWithValue("@POItemNumber", txtPOItemNumber.Text);
                        command.Parameters.AddWithValue("@CertificateAvailable", ddlCertificateAvailable.SelectedIndex);
                        command.Parameters.AddWithValue("@StoreLocation", ddlStoreLocation.SelectedValue);
                        command.Parameters.AddWithValue("@IssuedProjectNo", ddlIssuedProjectNo.SelectedValue);

                        if (!string.IsNullOrEmpty(txtDateOfIssued.Text))
                        {
                            command.Parameters.AddWithValue("@DateOfIssued", Convert.ToDateTime(txtDateOfIssued.Text));
                        }

                        command.Parameters.AddWithValue("@Itemid", Convert.ToInt32(ddlItemName.SelectedValue));
                        command.Parameters.AddWithValue("@item_categoryid", Convert.ToInt32(ddlCategory.SelectedValue));

                        if (!string.IsNullOrEmpty(ddlItemSize1.SelectedValue))
                        {
                            command.Parameters.AddWithValue("@itemsize2", ddlItemSize1.SelectedValue.ToString());
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@itemsize2", DBNull.Value);
                        }

                        if (!string.IsNullOrEmpty(txtsize3.Text))
                        {
                            command.Parameters.AddWithValue("@itemsize3", txtsize3.Text);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@itemsize3", DBNull.Value);
                        }

                        command.Parameters.AddWithValue("@SessionId", Session["Sessionid"]);

                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();

                    }
                }

                // Optional: Display a success message or clear the form
                //Response.Write("<script>alert('Record saved successfully');</script>");
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "showMessage();", true);
                Response.Redirect(Request.RawUrl);
                BindGrid();
                //ClearItemDetails();
            }
        
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "showMessage2();", true);
        } 
    }
        else
        {
            //Response.Write("<script>alert('Please Select Discipline');</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "showMessage1();", true);
            ClearItemDetails();
        }
    } 
    protected void ValidateSurplusQty()
    {
        //string surplusQty = Request.Form["SurplusQty"];
        string surplusQty = txtSurplusQty.Text;
        if (string.IsNullOrWhiteSpace(surplusQty))
        {
            //Response.Write("<script>alert('Surplus Qty is required.');</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "showMessage();", true);
            //Response.Redirect(Request.RawUrl);
            return;
        }
        else
        {
            
            rec_save();
            
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        // Redirect to a specific page or the previous page
        //Session.Clear();
        Response.Redirect("HomePage.aspx");

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearItemDetails();
    }
    protected void btnedit_Click(object sender, EventArgs e)
    {

    }
    private void LoadItemNames()
    {
        // Get the connection string from Web.config
        string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Define the command and specify the stored procedure
            using (SqlCommand command = new SqlCommand("GetItemNames", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Case", 1);
                // Open the connection
                connection.Open();

                // Execute the command and load results into a SqlDataReader
                SqlDataReader reader = command.ExecuteReader();

                // Bind the data to the dropdown
                ddlItemName.DataSource = reader;
                ddlItemName.DataTextField = "Item_Name";  // The column to display
                ddlItemName.DataValueField = "ItemID";   // The column to use as the value
                ddlItemName.DataBind();

                // Add a default item at the beginning of the list
                ddlItemName.Items.Insert(0, new ListItem("-- Select Item --", "0"));


            }
        }

    }
    public void storedetails()
    {
        // Get the connection string from Web.config
        string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Define the command and specify the stored procedure
            using (SqlCommand command = new SqlCommand("StoreDetails", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Open the connection
                connection.Open();

                // Execute the command and load results into a SqlDataReader
                SqlDataReader reader = command.ExecuteReader();

                // Bind the data to the dropdown
                ddlStoreLocation.DataSource = reader;
                ddlStoreLocation.DataTextField = "storename";  // The column to display
                ddlStoreLocation.DataValueField = "store_id";   // The column to use as the value
                ddlStoreLocation.DataBind();

                // Add a default item at the beginning of the list
                ddlStoreLocation.Items.Insert(0, new ListItem("-- Select Store Name --", "0"));
            }
        }
    }
    public void Projectdetails()
    {
        // Get the connection string from Web.config
        string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Define the command and specify the stored procedure
            using (SqlCommand command = new SqlCommand("ProjectDetails", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Open the connection
                connection.Open();

                // Execute the command and load results into a SqlDataReader
                SqlDataReader reader = command.ExecuteReader();

                // Bind the data to the dropdown
                ddlIssuedProjectNo.DataSource = reader;
                ddlIssuedProjectNo.DataTextField = "Projectname";  // The column to display
                ddlIssuedProjectNo.DataValueField = "Project_id";   // The column to use as the value
                ddlIssuedProjectNo.DataBind();

                // Add a default item at the beginning of the list
                ddlIssuedProjectNo.Items.Insert(0, new ListItem("-- Select Project Name --", "0"));
            }
        }
    }
    protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
    {

        int itemID;
        if (int.TryParse(ddlItemName.SelectedValue, out itemID) && itemID != 0)
        {
                LoadItemCategory(itemID);
            
        }
        else
        {
            ClearItemDetails();  // Optional: Clear fields if no valid item is selected
        }
    }
    private void LoadItemCategory(int itemID)
    {
        ddlCategory.Items.Clear();
        // Get the connection string from Web.config
        string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Define the command and specify the stored procedure
            using (SqlCommand command = new SqlCommand("GetItemCategory", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ItemID", itemID);
                // Open the connection
                connection.Open();

                // Execute the command and load results into a SqlDataReader
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ddlCategory.Items.Add(new ListItem(reader["Category"].ToString(), reader["Item_categoryID"].ToString()));
                }

                ddlCategory.Items.Insert(0, new ListItem("-- Select Category --", "0"));

            }
        }
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        {
            //int itemID; int categoryID;
            if (int.TryParse(ddlItemName.SelectedValue, out itemID) && itemID != 0 &&
         int.TryParse(ddlCategory.SelectedValue, out categoryID) && categoryID != 0)
            {
                // Call ItemSizedetails with both itemID and categoryID
                ItemSizedetails(itemID, categoryID);
                ItemSizedetails2(itemID, categoryID);
            }
            else
            {
                // Optional: Clear fields if no valid item is selected
                ClearItemDetails();
            }
        }
    }
    private void ItemSizedetails(int itemID, int categoryID)
    {
        // Get the connection string from Web.config
        ddlItemSize.Items.Clear();
        string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Define the command and specify the stored procedure
            using (SqlCommand command = new SqlCommand("ItemSizeDetails", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ItemID", itemID);
                command.Parameters.AddWithValue("@categoryID", categoryID);
                command.Parameters.AddWithValue("@case", 2);
                // Open the connection
                connection.Open();

                // Execute the command and load results into a SqlDataReader
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ddlItemSize.Items.Add(new ListItem(reader["size1"].ToString(), reader["sizeid"].ToString()));
                }

                ddlItemSize.Items.Insert(0, new ListItem("-- Select Item Size --", "0"));

            }
        }

    } 
    private void ItemSizedetails2(int itemID, int categoryID)
    {
        ddlItemSize1.Items.Clear();
        // Get the connection string from Web.config
        string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Define the command and specify the stored procedure
            using (SqlCommand command = new SqlCommand("ItemSizeDetails", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ItemID", itemID);
                command.Parameters.AddWithValue("@categoryID", categoryID);
                command.Parameters.AddWithValue("@case", 3);
                // Open the connection
                connection.Open();

                // Execute the command and load results into a SqlDataReader
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ddlItemSize1.Items.Add(new ListItem(reader["size2"].ToString(), reader["sizeid"].ToString()));
                }

                ddlItemSize1.Items.Insert(0, new ListItem("-- Select Item Size --", "0"));

            }
        }

    }

    private void BinfGrid_new() 
    {
       
        GVwebServiceRef.GVwebServiceSoapClient client = new GVwebServiceRef.GVwebServiceSoapClient();
        DataTable data = client.GetGridData();
        gvInventory.DataSource = data;
        gvInventory.DataBind();
    }
    private void BindGrid()
    {
        
        string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {

            using (SqlCommand command = new SqlCommand("ITEMGRID_LOAD", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@case", 1);
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    gvInventory.DataSource = dataTable;
                    gvInventory.DataBind();

                }
            }
        }
    }
    private void ClearItemDetails()
        {
            txtItemCode.Text = "";
            txtItemDescription.Text = "";
            ddlItemSize.Items.Clear();
            ddlItemUnits.SelectedIndex = -1;
            txtSurplusQty.Text = "";
            txtPOReferenceNo.Text = "";
            txtPOItemNumber.Text = "";
            txtDateOfIssued.Text = "";
            //txtQtyIssued.Text = "";
            //txtBalanceQty.Text = "";
            ddlItemName.SelectedIndex = -1;
            ddlCategory.Items.Clear();
            ddlStoreLocation.SelectedIndex = -1;
            ddlIssuedProjectNo.SelectedIndex = -1;
            ddlCertificateAvailable.SelectedIndex = -1;
            btnSave.Text = "Save";
            ddlItemSize1.Items.Clear();
            txtsize3.Text = "";
            txtSurplusQty.Enabled = true;
            BindGrid();
            gvInventory.PageIndex=0;

    }
    protected void gvInventory_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvInventory.EditIndex = e.NewEditIndex;
        BindGrid();
    }
    protected void gvInventory_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvInventory.EditIndex = -1;
        BindGrid();
    }
    protected void gvInventory_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        // Get the connection string from Web.config
        string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Define the command and specify the stored procedure
            using (SqlCommand command = new SqlCommand("ITEM_INSERT", connection))
            {
                Session["Sessionid"] = Session.SessionID;
                //try
                //{
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters and assign values from form inputs
              
                command.Parameters.AddWithValue("@ItemCode", txtItemCode.Text);
                command.Parameters.AddWithValue("@ItemDescription", txtItemDescription.Text);
                command.Parameters.AddWithValue("@ItemSize", ddlItemSize.SelectedValue);
                command.Parameters.AddWithValue("@ItemSize1", ddlItemSize1.SelectedValue);
                command.Parameters.AddWithValue("@ItemUnits", ddlItemUnits.SelectedItem);
                command.Parameters.AddWithValue("@SurplusQty", Convert.ToInt32(txtSurplusQty.Text));
                command.Parameters.AddWithValue("@POReferenceNo", txtPOReferenceNo.Text);
                command.Parameters.AddWithValue("@POItemNumber", txtPOItemNumber.Text);
                command.Parameters.AddWithValue("@CertificateAvailable", ddlCertificateAvailable.SelectedValue);
                //bool isCertificateAvailable = rblCertificateAvailable.SelectedValue == "1"; // Assuming "1" is "Yes/True" and "0" is "No/False"
                //command.Parameters.AddWithValue("@CertificateAvailable", isCertificateAvailable);
                //command.Parameters.AddWithValue("@CertificateAvailable", Convert.ToBoolean(rblCertificateAvailable.SelectedValue));
                command.Parameters.AddWithValue("@StoreLocation", ddlStoreLocation.SelectedValue);
                command.Parameters.AddWithValue("@IssuedProjectNo", ddlIssuedProjectNo.SelectedValue);
                command.Parameters.AddWithValue("@DateOfIssued", Convert.ToDateTime(txtDateOfIssued.Text));
                //command.Parameters.AddWithValue("@QtyIssued", Convert.ToInt32(txtQtyIssued.Text));
                //command.Parameters.AddWithValue("@BalanceQty", Convert.ToInt32(txtBalanceQty.Text));
                command.Parameters.AddWithValue("@Itemid",ddlItemName.SelectedValue);
                command.Parameters.AddWithValue("@item_categoryid",ddlCategory.SelectedValue);
                command.Parameters.AddWithValue("@SessionId", Session["Sessionid"]);
                // Open the connection and execute the command
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                
            }
        }

        // Optional: Display a success message or clear the form
        Response.Write("<script>alert('Record saved successfully');</script>");
        BindGrid();
        ClearItemDetails();
    }
    protected void gvInventory_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string selectedValue = ddlCategory.SelectedValue;
        //LoadItemCategory(itemID);
        string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand("ITEM_INSERT", connection))
            {
                GridViewRow row = gvInventory.SelectedRow;
                 id = gvInventory.DataKeys[row.RowIndex]["inventory_id"].ToString();

                command.CommandType = CommandType.StoredProcedure;

                // Add parameters and assign values from form inputs
                command.Parameters.AddWithValue("@action", "Get Details");
                command.Parameters.AddWithValue("@Inventory_Id", id);

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        // Populate fields from dataTable
                        txtItemCode.Text = dataTable.Rows[0]["ItemCode"].ToString();
                        txtItemDescription.Text = dataTable.Rows[0]["ItemDescription"].ToString();
                        if (string.IsNullOrEmpty(dataTable.Rows[0]["unit_id"].ToString()))
                        {
                            ddlItemUnits.SelectedIndex = -1;
                        }
                        else 
                        {
                            ddlItemUnits.SelectedValue = dataTable.Rows[0]["unit_id"].ToString();
                        }
                        if (string.IsNullOrEmpty(dataTable.Rows[0]["ISSUE_STATUS"].ToString()))
                        {
                            txtSurplusQty.Enabled = true;
                            txtSurplusQty.Text = dataTable.Rows[0]["SurplusQty"].ToString();

                        }
                        else 
                        {
                            txtSurplusQty.Enabled = false;
                            txtSurplusQty.Text = dataTable.Rows[0]["SurplusQty"].ToString();
                        }
                        
                        txtPOReferenceNo.Text = dataTable.Rows[0]["POReferenceNo"].ToString();
                        txtPOItemNumber.Text = dataTable.Rows[0]["POItemNumber"].ToString();
                        //ddlCertificateAvailable.SelectedValue = dataTable.Rows[0]["CertificateAvailable"].ToString();
                        if (!Convert.IsDBNull(dataTable.Rows[0]["CertificateAvailable"]))
                        {
                            bool isCertificateAvailable = Convert.ToBoolean(dataTable.Rows[0]["CertificateAvailable"]);
                            ddlCertificateAvailable.SelectedValue = isCertificateAvailable ? "0" : "1";
                        }
                        else
                        {
                            ddlCertificateAvailable.SelectedValue = "1";  // Or handle it based on your requirements
                        }

                        if (string.IsNullOrEmpty(dataTable.Rows[0]["StoreLocation"].ToString()))
                        {
                            ddlStoreLocation.SelectedIndex = -1;
                        }
                        else
                        {
                            ddlStoreLocation.SelectedValue = dataTable.Rows[0]["StoreLocation"].ToString();
                        }
                            if (string.IsNullOrEmpty(dataTable.Rows[0]["IssuedProjectNo"].ToString()))
                        {
                            ddlIssuedProjectNo.SelectedIndex = -1;
                        }
                        else
                        {
                            ddlIssuedProjectNo.SelectedValue = dataTable.Rows[0]["IssuedProjectNo"].ToString();
                        }
                   
                        txtsize3.Text = dataTable.Rows[0]["itemsize3"].ToString();

                        if (string.IsNullOrEmpty(dataTable.Rows[0]["DateOfIssued"].ToString()))
                        {
                            txtDateOfIssued.Text = "";
                        }
                        else
                        {
                            txtDateOfIssued.Text = Convert.ToDateTime(dataTable.Rows[0]["DateOfIssued"].ToString()).ToString("dd-MM-yyyy");
                        }

                            ddlItemName.SelectedValue = dataTable.Rows[0]["Itemid"].ToString();
                       
                        itemID = Convert.ToInt32(dataTable.Rows[0]["itemid"].ToString());
                        LoadItemCategory(itemID); // Use the correct variable name

                        if (dataTable.Rows[0]["item_categoryid"] == DBNull.Value ||
                            string.IsNullOrEmpty(dataTable.Rows[0]["item_categoryid"].ToString()))
                        {
                            ddlCategory.SelectedIndex = -1; // Clear selection
                        }
                        else
                        {
                            ddlCategory.SelectedValue = dataTable.Rows[0]["Item_categoryID"].ToString(); // Set the dropdown to the correct category 
                        }

                        if (dataTable.Rows[0]["itemSize1"] == DBNull.Value ||
                            string.IsNullOrEmpty(dataTable.Rows[0]["itemSize1"].ToString()))
                        {
                            ddlItemSize.Items.Clear();
                        }
                        else
                        {
                            ItemSizedetails(itemID, Convert.ToInt32(dataTable.Rows[0]["item_categoryid"]));
                            ddlItemSize.SelectedValue = dataTable.Rows[0]["itemSize1"].ToString();
                        }
                        if (dataTable.Rows[0]["itemSize2"] == DBNull.Value ||
                            string.IsNullOrEmpty(dataTable.Rows[0]["itemSize2"].ToString()))
                        {
                            ddlItemSize1.Items.Clear();
                        }
                        else
                        {
                            ItemSizedetails2(itemID, Convert.ToInt32(dataTable.Rows[0]["item_categoryid"]));
                            ddlItemSize1.SelectedValue = dataTable.Rows[0]["itemSize2"].ToString();
                        }
                            btnSave.Text = "Update";
                    }
                }
            }
        }
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        System.Web.Security.FormsAuthentication.SignOut();
        sessionLogout();
        Response.Redirect("~/Login.aspx");
    }
    protected void gvInventory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        // Set the new page index
        gvInventory.PageIndex = e.NewPageIndex;

        // Rebind the GridView to reflect the change
        //BindGrid();
        //BinfGrid_new();
        CheckDropdownSelections_NEW();
    }
    public void sessionLogout()
    {
        //string username = Session["UserName"].ToString(); // Use Trim to remove any leading/trailing whitespace
        Session["Sessionid"] = Session.SessionID;

        // Get the connection string from Web.config
        string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Define the command and specify the stored procedure
            using (SqlCommand command = new SqlCommand("useauthentication", connection))
            {
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters
                command.Parameters.AddWithValue("@SessionId", Session["Sessionid"]);
                command.Parameters.AddWithValue("@case", 3);  // Indicating this is an update operation

                // Execute the command and check how many rows were affected
                int rowsAffected = command.ExecuteNonQuery();

                // Check if the update was successful
                if (rowsAffected > 0)
                {
                    // Successful update, handle the logic here (e.g., notify the user)
                    Console.WriteLine("Password updated successfully.");
                }
                else
                {
                    // If no rows were affected, handle the failure (e.g., user not found)
                    Console.WriteLine("Update failed. Username not found or invalid.");
                }
            }
        }
    }
    protected void gvInventory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // Check if the current row is a data row
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Find the Select button in the row (assumed to be the first cell)
            LinkButton selectButton = (LinkButton)e.Row.Cells[0].Controls[0];

            // Customize the button's appearance
            selectButton.Text = ""; // Remove the default text
            selectButton.CssClass = "custom-button-style"; // Apply custom CSS class

            // Optionally, add an image or icon
            selectButton.Text = "<i class='fa fa-edit'></i>"; // Example with Font Awesome icon

            e.Row.Cells[0].ToolTip = "This is the Name";
            string description = DataBinder.Eval(e.Row.DataItem, "ItemDescription").ToString();
            e.Row.Cells[1].ToolTip = description;

        }
    }
    protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["ondblclick"] = Page.ClientScript.GetPostBackEventReference(gvInventory, "DoubleClick$" + e.Row.RowIndex);
        }
    }

    private void LoadItemUnit()
    {
        // Get the connection string from Web.config
        string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Define the command and specify the stored procedure
            using (SqlCommand command = new SqlCommand("ITEMGRID_SEARCH_LOAD", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                //command.Parameters.AddWithValue("@ponumber", ddlporefrence.SelectedValue.ToString());
                command.Parameters.AddWithValue("@Case", 19);
                // Open the connection
                connection.Open();

                // Execute the command and load results into a SqlDataReader
                SqlDataReader reader = command.ExecuteReader();

                // Bind the data to the dropdown
                ddlItemUnits.DataSource = reader;
                ddlItemUnits.DataTextField = "Uint_name";  // The column to display
                ddlItemUnits.DataValueField = "unit_id";   // The column to use as the value
                ddlItemUnits.DataBind();

                // Add a default item at the beginning of the list
                
                ddlItemUnits.Items.Insert(0, new ListItem("-- Select Unit --", "0"));


            }
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CheckDropdownSelections_NEW();
    }
    protected void CheckDropdownSelections_NEW()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {

            using (SqlCommand command = new SqlCommand("ITEMGRID_LOAD", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                //if(ddlItemName.SelectedValue = null)

                int selectedValue; // Declare the variable outside
                if (string.IsNullOrEmpty(ddlItemName.SelectedValue) ||
                    (int.TryParse(ddlItemName.SelectedValue, out selectedValue) && selectedValue > 0))
                {
                    command.Parameters.AddWithValue("@ItemID", string.IsNullOrWhiteSpace(ddlItemName.SelectedValue) ? (object)DBNull.Value : ddlItemName.SelectedValue);
                }
                else
                {
                    command.Parameters.AddWithValue("@ItemID", null);
                }

                if (string.IsNullOrEmpty(ddlCategory.SelectedValue) ||
                    (int.TryParse(ddlCategory.SelectedValue, out selectedValue) && selectedValue > 0))
                {
                    command.Parameters.AddWithValue("@categoryID", string.IsNullOrWhiteSpace(ddlCategory.SelectedValue) ? (object)DBNull.Value : ddlCategory.SelectedValue);
                }
                else
                {
                    command.Parameters.AddWithValue("@categoryID", null);
                }
                if (string.IsNullOrEmpty(ddlItemSize.SelectedValue) ||
                    (int.TryParse(ddlItemSize.SelectedValue, out selectedValue) && selectedValue > 0))
                {
                    command.Parameters.AddWithValue("@itemsizeid", string.IsNullOrWhiteSpace(ddlItemSize.SelectedValue) ? (object)DBNull.Value : ddlItemSize.SelectedValue);
                }
                else
                {
                    command.Parameters.AddWithValue("@itemsizeid", null);
                }

                if (string.IsNullOrEmpty(ddlItemSize1.SelectedValue) ||
                   (int.TryParse(ddlItemSize1.SelectedValue, out selectedValue) && selectedValue > 0))
                {
                    command.Parameters.AddWithValue("@itemsizeid1", string.IsNullOrWhiteSpace(ddlItemSize1.SelectedValue) ? (object)DBNull.Value : ddlItemSize1.SelectedValue);
                }
                else
                {
                    command.Parameters.AddWithValue("@itemsizeid1", null);
                }
                
                    command.Parameters.AddWithValue("@case", 2);
                

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    gvInventory.DataSource = dataTable;
                    gvInventory.DataBind();

                }
            }
        }
    }
}
