using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Windows.Media.Media3D;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using System.Activities.Expressions;

public partial class Itemissued : System.Web.UI.Page
{
    public static String id = "";
    public static string inventoryId;
    public static int CA ;
    protected void Page_Load(object sender, EventArgs e)
    {



        if (Session["Username"] == null) // Check if the session has expired
        {
            Response.Redirect("Login.aspx");
        }
        Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
        Response.Cache.SetValidUntilExpires(false);
        Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
        if (!IsPostBack)
        {
            LoadItemNames();
            Projectdetails();
            BindGrid();
            txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy"); // Format as needed
            //txtDate.Attributes["placeholder"] = "MM/DD/YYYY";
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
    private void LoadPoNumber()
    {
        // Get the connection string from Web.config
        string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Define the command and specify the stored procedure
            using (SqlCommand command = new SqlCommand("ITEMGRID_SEARCH_LOAD", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ItemID", ddlItemName.SelectedValue);
                command.Parameters.AddWithValue("@categoryID", ddlCategory.SelectedValue);
                command.Parameters.AddWithValue("@itemsizeid", ddlItemsize.SelectedValue);
                command.Parameters.AddWithValue("@Case", 12);
                // Open the connection
                connection.Open();

                // Execute the command and load results into a SqlDataReader
                SqlDataReader reader = command.ExecuteReader();

                // Bind the data to the dropdown
                ddlporefrence.DataSource = reader;
                ddlporefrence.DataTextField = "POReferenceNo";  // The column to display
                //ddlItemName.DataValueField = "ItemID";   // The column to use as the value
                ddlporefrence.DataBind();

                // Add a default item at the beginning of the list
                ddlporefrence.Items.Insert(0, new ListItem("-- Select PO Number --", "0"));



            }
        }

    }
    //private void LoadItemCode()
    //{
    //    // Get the connection string from Web.config
    //    string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;

    //    using (SqlConnection connection = new SqlConnection(connectionString))
    //    {
    //        // Define the command and specify the stored procedure
    //        using (SqlCommand command = new SqlCommand("ITEMGRID_SEARCH_LOAD", connection))
    //        {
    //            command.CommandType = CommandType.StoredProcedure;
    //            command.Parameters.AddWithValue("@ponumber", ddlporefrence.SelectedValue.ToString());
    //            command.Parameters.AddWithValue("@Case", 15);
    //            // Open the connection
    //            connection.Open();

    //            // Execute the command and load results into a SqlDataReader
    //            SqlDataReader reader = command.ExecuteReader();

    //            // Bind the data to the dropdown
    //            ddlItemcode.DataSource = reader;
    //            ddlItemcode.DataTextField = "ItemCode";  // The column to display
    //            //ddlItemName.DataValueField = "ItemID";   // The column to use as the value
    //            ddlItemcode.DataBind();

    //            // Add a default item at the beginning of the list
    //            ddlItemcode.Items.Insert(0, new ListItem("-- Select Item Code --", "0"));


    //        }
    //    }

    //}
    private void abailableQty()
    {
        // Get the connection string from Web.config
        string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Define the command and specify the stored procedure
            using (SqlCommand command = new SqlCommand("ITEMGRID_SEARCH_LOAD", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ItemID", ddlItemName.SelectedValue);
                command.Parameters.AddWithValue("@categoryID", ddlCategory.SelectedValue);
                command.Parameters.AddWithValue("@itemsizeid", ddlItemsize.SelectedItem.ToString());
                command.Parameters.AddWithValue("@ponumber", ddlporefrence.SelectedValue.ToString());
                command.Parameters.AddWithValue("@Case", 14);
                // Open the connection
                connection.Open();

                // Execute the command and load results into a SqlDataReader
                SqlDataReader reader = command.ExecuteReader();
                
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
    protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
    {

        //btSearch.Style["background-color"] = "red";
        ddlCategory.Items.Clear();
        ClearGridView();
        int itemID;
        if (int.TryParse(ddlItemName.SelectedValue, out itemID) && itemID != 0)
        {

            LoadItemCategory(itemID);
        }
        else
        {
            //ClearItemDetails();  // Optional: Clear fields if no valid item is selected
        }
    }
    private void LoadItemCategory(int itemID)
    {
        // Get the connection string from Web.config
        string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Define the command and specify the stored procedure
            using (SqlCommand command = new SqlCommand("GetItemCategory", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ItemID", itemID);
                //command.Parameters.AddWithValue("@Case", 1);
                // Open the connection
                connection.Open();

                // Execute the command and load results into a SqlDataReader
                SqlDataReader reader = command.ExecuteReader();

                // Bind the data to the dropdown
                if (reader.HasRows)
                {
                    ddlCategory.DataSource = reader;
                    ddlCategory.DataTextField = "Category";  // The column to display
                    ddlCategory.DataValueField = "Item_categoryID";   // The column to use as the value
                    ddlCategory.DataBind();

                    // Add a default item at the beginning of the list
                    ddlCategory.Items.Insert(0, new ListItem("-- Select Category --", "0"));
                }
                reader.Close();
            }
            connection.Close();
        }
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddlItemsize.Items.Clear();
        ClearGridView();
        int Item_categoryID;
        if (int.TryParse(ddlCategory.SelectedValue, out Item_categoryID) && Item_categoryID != 0)
        {
            Debug.WriteLine("test" + ddlCategory.SelectedValue + " " + Item_categoryID);

            LoadItemSize(Item_categoryID);
        }
        else
        {
            //ClearItemDetails();  // Optional: Clear fields if no valid item is selected
        }
    }
    private void LoadItemSize(int Item_categoryID)
    {
        // Get the connection string from Web.config
        string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Define the command and specify the stored procedure
            using (SqlCommand command1 = new SqlCommand("ITEMGRID_SEARCH_LOAD", connection))
            {
                command1.CommandType = CommandType.StoredProcedure;
                command1.Parameters.AddWithValue("@categoryID", Item_categoryID);
                command1.Parameters.AddWithValue("@Case", 13);
                // Open the connection
                connection.Open();

                // Execute the command and load results into a SqlDataReader
                SqlDataReader reader1 = command1.ExecuteReader();

                // Bind the data to the dropdown
                if (reader1.HasRows)
                {
                    //Debug.WriteLine(reader1.HasRows);


                    ddlItemsize.DataSource = reader1;

                    ddlItemsize.DataTextField = "Size_name";  // The column to display
                    ddlItemsize.DataValueField = "Size_id";   // The column to use as the value
                    ddlItemsize.DataBind();
                    ddlItemsize.Items.Insert(0, new ListItem("-- Select Item Size --", "0"));
                    /*ddlItemsize.Items.Add(new ListItem("-- Select Item Size --", "0"));
                    int rowCount = 0;
                    while (reader1.Read())
                    {
                        rowCount++;
                        Debug.WriteLine("Item Value " +reader1["Size_Name"].ToString());
                        ddlItemsize.Items.Add(new ListItem(reader1["Size_Name"].ToString(), reader1["Size_id"].ToString()));
                        // Process each row as needed
                    }*/

                    // Add a default item at the beginning of the list

                }
                reader1.Close();

            }
            connection.Close();
        }
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        gvInventory.Visible = true;
        btSearch.Style.Remove("background-color");
        bool isItemNameSelected = ddlItemName.SelectedIndex != 0 && ddlItemName.SelectedIndex != -1;
        bool isCategorySelected = ddlCategory.SelectedIndex != 0 && ddlCategory.SelectedIndex != -1;
        bool isItemSizeSelected = ddlItemsize.SelectedIndex != 0 && ddlItemsize.SelectedIndex != -1;
        bool isPOReferenceSelected = ddlporefrence.SelectedIndex != 0 && ddlporefrence.SelectedIndex != -1;

        if (isItemNameSelected && isCategorySelected)
        {
            if (!isItemSizeSelected && !isPOReferenceSelected)
            {
                CA = 1; // Only ItemName and Category selected
            }
            else if (isItemSizeSelected && !isPOReferenceSelected)
            {
                CA = 2; // ItemName, Category, and ItemSize selected
            }
            else if (isItemSizeSelected && isPOReferenceSelected)
            {
                CA = 3; // All dropdowns selected
            }

            // Call BindGrid only once
            BindGrid();
        }
        
    }
    private DataTable GetData()
    {
        // Replace with your actual data-fetching logic
        DataTable dt = new DataTable();
        // Populate DataTable
        return dt;
    }
    protected void gvInventory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        // Set the new page index
        gvInventory.PageIndex = e.NewPageIndex;

        // Rebind the GridView to reflect the change
        BindGrid();
    }
    protected void btClear_Click(object sender, EventArgs e)
    {
        page_clear();
    }
    private void page_clear()
    {
        ddlItemName.SelectedIndex = -1;
        ddlCategory.Items.Clear();
        ddlItemsize.Items.Clear();
        //ddlItemcode.SelectedIndex = -1;
        ddlporefrence.Items.Clear();
        ddlissuedproject.Items.Clear();
        //ClearGridView();
        gvInventory.Visible = false;
        txtDate.Attributes["placeholder"] = "MM/DD/YYYY";
    }
    protected void ClearGridView()
    {
        gvInventory.DataSource = null; // Remove the data source
        gvInventory.DataBind();       // Rebind the GridView to reflect the changes
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {

       
        Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();

        // Reload the page
        Response.Redirect(Request.RawUrl);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        //Session.Clear();
        Response.Redirect("HomePage.aspx");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

    }
    protected void ddlItemsize_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlporefrence.Items.Clear();
        ClearGridView();
        int Size_id;
        if (int.TryParse(ddlItemsize.SelectedValue, out Size_id) && Size_id != 0)
        {
            Debug.WriteLine("test" + ddlCategory.SelectedValue + " " + Size_id);

            LoadPoNumber();
        }
        else
        {
            //ClearItemDetails();  // Optional: Clear fields if no valid item is selected
        }
    }
    protected void gvInventory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowState.HasFlag(DataControlRowState.Edit))
        {
            // Find the dropdown in the EditItemTemplate
            DropDownList ddlIssuedProject = (DropDownList)e.Row.FindControl("ddlIssuedProject");

            if (ddlIssuedProject != null)
            {
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
                        ddlissuedproject.DataSource = reader;
                        ddlissuedproject.DataTextField = "Projectname";  // The column to display
                        ddlissuedproject.DataValueField = "Project_id";   // The column to use as the value
                        ddlissuedproject.DataBind();

                        // Add a default item at the beginning of the list
                        ddlissuedproject.Items.Insert(0, new ListItem("-- Select Project Name --", "0"));
                    }
                }

                // Optionally set the selected value based on the current row's data
                string selectedProject = DataBinder.Eval(e.Row.DataItem, "IssuedProjectID").ToString();
                ddlIssuedProject.SelectedValue = selectedProject;
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
                ddlissuedproject.DataSource = reader;
                ddlissuedproject.DataTextField = "Projectname";  // The column to display
                ddlissuedproject.DataValueField = "Project_id";   // The column to use as the value
                ddlissuedproject.DataBind();

                // Add a default item at the beginning of the list
                ddlissuedproject.Items.Insert(0, new ListItem("-- Select Project Name --", "0"));
            }
        }
    }

    protected void ddlporefrence_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlporefrence.SelectedValue != "" || ddlporefrence.SelectedIndex != -1)
        {

            //LoadItemCode();
            //abailableQty();
        }
    }
    protected void gvInventory_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvInventory.EditIndex = -1;
        //lblMessage.Visible = false;
        BindGrid();
    }
    
    protected void gvInventory_RowEditing(object sender, GridViewEditEventArgs e)
    {
        // Set the row to edit mode
        //gvInventory.EditIndex = e.NewEditIndex;
        //BindGrid();
        // Get the row index being edited
        int rowIndex = e.NewEditIndex;

        // Assuming you have a primary key or unique ID bound in a column (e.g., "ID")
        // Use the row index to access the data in that row
        GridViewRow row = gvInventory.Rows[rowIndex];

        // Example: Assuming the primary key is in the first column (index 0)
        inventoryId = gvInventory.DataKeys[rowIndex].Value.ToString();
        
        //EditRowUpdate(availableQty, issuedQty, inventoryId);
        // You can now use the recordId for further processing
        //int lblMessage = $"Editing record with ID: {recordId}";

        // Set the edit index to allow editing
        gvInventory.EditIndex = rowIndex;

        // Rebind the data to the GridView (you must do this to enter edit mode)
        //BindGrid(); // A method you define to bind/reload data to the GridView

    }
  
    private void EditRowUpdate(decimal availableQty, decimal issuedQty, string inventoryId)
    {
        DateTime issueDate;
        string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Define the command and specify the stored procedure
            using (SqlCommand command = new SqlCommand("item_issued_details", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                Session["Sessionid"] = Session.SessionID;

                // Add parameters and assign values from form inputs
                command.Parameters.AddWithValue("@ITEMID", ddlItemName.SelectedValue); // Assuming ddlItemName is the item dropdown
                command.Parameters.AddWithValue("@ITEMCATEGORY", ddlCategory.SelectedValue); // Assuming ddlCategory is the category dropdown
                command.Parameters.AddWithValue("@ITEMSIZE", ddlItemsize.SelectedValue); // Assuming ddlItemsize is the size dropdown
                command.Parameters.AddWithValue("@POREFNO", ddlporefrence.SelectedValue); // Assuming ddlporefrence is the PO reference dropdown
                command.Parameters.AddWithValue("@PROJECTNO", ddlissuedproject.SelectedValue); // Assuming ddlissuedproject is the project dropdown
                command.Parameters.AddWithValue("@AVAILQTY", availableQty); // Parsed available quantity
                command.Parameters.AddWithValue("@ISSUEQTY", issuedQty); // Parsed issued quantity
                command.Parameters.AddWithValue("@BALANCEQTY", availableQty - issuedQty); // Calculate balance quantity
                command.Parameters.AddWithValue("@INVENTORY_ID", inventoryId); // Inventory ID from DataKey
                command.Parameters.AddWithValue("@SessionId", Session["Sessionid"]); // Current user/session ID
                if (DateTime.TryParse(txtDate.Text, out issueDate))
                {
                    command.Parameters.AddWithValue("@ISSUEDATE", issueDate);
                }
                command.Parameters.AddWithValue("@CASE", 1);
                // Open the connection and execute the command
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
    protected void gvInventory_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        if (ddlissuedproject.SelectedIndex == -1 || ddlissuedproject.SelectedIndex == 0)
        {
            Response.Write("<script>alert('Please Choose Project')</script>");
            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "showMessage();", true);
            
            e.Cancel = true;
            return;
        }
      

        // Retrieve the row being edited
        GridViewRow row = gvInventory.Rows[e.RowIndex];

        // Get the "Available Qty" and "Issued Qty" values
        TextBox txtAvailableQty = (TextBox)row.FindControl("txtAvailableQty");
        TextBox txtIssuedQty = (TextBox)row.FindControl("txtIssuedQty");

        //if (txtAvailableQty != null && txtIssuedQty != null && txtIssuedQty != "")
        if (txtAvailableQty != null && txtIssuedQty != null && !string.IsNullOrEmpty(txtIssuedQty.Text))
        {
            {
                // Parse values
                decimal availableQty = decimal.Parse(txtAvailableQty.Text);
                decimal issuedQty = decimal.Parse(txtIssuedQty.Text);

                // Check if issuedQty exceeds availableQty
                if (issuedQty > availableQty)
                {
                    Response.Write("<script>alert('Issued Quantity is More than Available Quantity');</script>");
                    e.Cancel = true;
                    return;
                }
                else if (issuedQty < 0)
                {
                    Response.Write("<script>alert('Issued Quantity is Not Empty');</script>");
                    e.Cancel = true;
                    return;
                }
                // Proceed with updating the row
                EditRowUpdate(availableQty, issuedQty, inventoryId);

            }
            Response.Write("<script>alert('Item Issued successfully');</script>");
            
            gvInventory.DataSource = null; // Remove the data source
            gvInventory.DataBind();
            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "showMessage();", true);
            //gvInventory.EditIndex = -1;
            //Response.Redirect(Request.RawUrl);
        }
    }

    private void SaveIssuedProject(int inventoryId, string selectedProject)
    {
        // Implement logic to update the database
    }
    private void BindGrid()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
           

            using (SqlCommand command = new SqlCommand("ITEMGRID_SEARCH_LOAD", connection))
            {
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ItemID", ddlItemName.SelectedValue);
                    command.Parameters.AddWithValue("@categoryID", ddlCategory.SelectedValue);
                    command.Parameters.AddWithValue("@itemsizeid", ddlItemsize.SelectedValue);
                    command.Parameters.AddWithValue("@ponumber", ddlporefrence.SelectedValue);
                    command.Parameters.AddWithValue("@CASE", 18);
                    command.Parameters.AddWithValue("@CA", CA);
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
    protected void ddlItemcode_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void gvInventory_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = gvInventory.SelectedRow;
        Debug.WriteLine(gvInventory.SelectedRow.Cells.ToString());
        //gvInventory.DataKeys[row.RowIndex]["inventory_id"].ToString());

    }

    protected void txtIssuedQty_KeyUp(object sender, EventArgs e) 
    {
        SaveChanges(this, e);
    }
    protected void SaveChanges(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvInventory.Rows)
        {
            var txtIssuedQty = (TextBox)row.FindControl("txtIssuedQty");
            var lblBalanceQty = (Label)row.FindControl("lblBalanceQty");

            int availableQty = int.Parse(row.Cells[1].Text);
            int issuedQty = string.IsNullOrEmpty(txtIssuedQty.Text) ? 0 : int.Parse(txtIssuedQty.Text);

            if (issuedQty > availableQty)
            {
                // Handle validation error
            }
            else
            {
                // Save data to database
            }
        }
    }
    protected void gvInventory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Save"))
        {
            // Check if project is selected
            if (ddlissuedproject.SelectedIndex == -1 || ddlissuedproject.SelectedIndex == 0)
            {
                Response.Write("<script>alert('Please Choose Project');</script>");
                return;
            }

            // Retrieve the row index
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvInventory.Rows[rowIndex];

            // Retrieve controls and data
            TextBox txtAvailableQty = (TextBox)row.FindControl("txtAvailableQty");
            TextBox txtIssuedQty = (TextBox)row.FindControl("txtIssuedQty");
            Label lblInventoryId = (Label)row.FindControl("lblInventoryId"); // Assuming lblInventoryId exists

            if (txtAvailableQty != null && txtIssuedQty != null && lblInventoryId != null)
            {
                decimal availableQty = decimal.Parse(txtAvailableQty.Text);
                decimal issuedQty = decimal.Parse(txtIssuedQty.Text);
                int inventoryId = int.Parse(lblInventoryId.Text);

                // Check if issued quantity exceeds available quantity
                if (issuedQty > availableQty)
                {
                    Response.Write("<script>alert('Issued Quantity is More than Available Quantity');</script>");
                    return;
                }

                // Perform update logic
                EditRowUpdate(availableQty, issuedQty, inventoryId);

                // Display success message and rebind GridView
                Response.Write("<script>alert('Item Issued successfully');</script>");
                //ClientScript.RegisterStartupScript(this.GetType(), "alert", "showMessage();", true);
                gvInventory.DataSource = GetUpdatedData(); // Fetch the updated data source
                gvInventory.DataBind();
            }
        }
    }

    private object GetUpdatedData()
    {
        throw new NotImplementedException();
    }

    private void EditRowUpdate(decimal availableQty, decimal issuedQty, int inventoryId)
    {
        throw new NotImplementedException();
    }
}