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
using System.Threading;
using System.Web.Services;

[WebService(Namespace = "http://192.168.0.227:90/surplus/GVwebService.asmx")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.ComponentModel.ToolboxItem(false)]

public partial class Searchpage : System.Web.UI.Page
{
    int DataTable;
    protected void Page_Load(object sender, EventArgs e)
    {
        //sessionCheck();
        if (Session["Username"] == null) // Check if the session has expired
        {
            Response.Redirect("Login.aspx");
        }
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
        Response.Cache.SetNoStore();
        if (!IsPostBack)
        {
            LoadItemNames();
            BindGrid();
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
    private void BindGrid()
    {

        string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {

            using (SqlCommand command = new SqlCommand("ITEMGRID_SEARCH_LOAD", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                //command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                command.Parameters.AddWithValue("@case", 0);
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

    private void BinfGrid_new()
    {
        GVwebServiceRef.GVwebServiceSoapClient client = new GVwebServiceRef.GVwebServiceSoapClient();
        DataTable dataTable = client.SearchBindGrid();
        gvInventory.DataSource = DataTable;
        gvInventory.DataBind();
    }
    protected void DisablePagingForExport()
    {
        gvInventory.AllowPaging = false; // Disable paging
        BindGrid();                 // Re-bind the GridView to all data
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        CheckDropdownSelections_NEW();
    }

    public class MyWebService : System.Web.Services.WebService
    {
        public MyWebService()
        {
            // Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }

        [WebMethod]
        public DataTable Web_CheckDropdownSelections(string ddlItemName, string ddlCategory, string ddlItemsize, string ddlItemsize1, bool checkBoxChecked)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("ITEMGRID_SEARCH_LOAD", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    int selectedValue; // Declare the variable outside

                    // Check ItemID parameter
                    if (string.IsNullOrEmpty(ddlItemName) ||
                        (int.TryParse(ddlItemName, out selectedValue) && selectedValue > 0))
                    {
                        command.Parameters.AddWithValue("@ItemID", string.IsNullOrWhiteSpace(ddlItemName) ? (object)DBNull.Value : ddlItemName);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ItemID", null);
                    }

                    // Check Category parameter
                    if (string.IsNullOrEmpty(ddlCategory) ||
                        (int.TryParse(ddlCategory, out selectedValue) && selectedValue > 0))
                    {
                        command.Parameters.AddWithValue("@categoryID", string.IsNullOrWhiteSpace(ddlCategory) ? (object)DBNull.Value : ddlCategory);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@categoryID", null);
                    }

                    // Check ItemSize parameter
                    if (string.IsNullOrEmpty(ddlItemsize) ||
                        (int.TryParse(ddlItemsize, out selectedValue) && selectedValue > 0))
                    {
                        command.Parameters.AddWithValue("@itemsizeid", string.IsNullOrWhiteSpace(ddlItemsize) ? (object)DBNull.Value : ddlItemsize);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@itemsizeid", null);
                    }

                    // Check ItemSize1 parameter
                    if (string.IsNullOrEmpty(ddlItemsize1) ||
                        (int.TryParse(ddlItemsize1, out selectedValue) && selectedValue > 0))
                    {
                        command.Parameters.AddWithValue("@itemsizeid1", string.IsNullOrWhiteSpace(ddlItemsize1) ? (object)DBNull.Value : ddlItemsize1);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@itemsizeid1", null);
                    }

                    // Set the case based on CheckBox1 status
                    if (checkBoxChecked)
                    {
                        command.Parameters.AddWithValue("@case", 20);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@case", 21);
                    }

                    // Execute query and return the results in DataTable
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable; // Return DataTable as the result of the web service method
                    }
                }
            }
        }
    }
    protected void CheckDropdownSelections_NEW()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {

            using (SqlCommand command = new SqlCommand("ITEMGRID_SEARCH_LOAD", connection))
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
                if (string.IsNullOrEmpty(ddlItemsize.SelectedValue) ||
                    (int.TryParse(ddlItemsize.SelectedValue, out selectedValue) && selectedValue > 0))
                {
                    command.Parameters.AddWithValue("@itemsizeid", string.IsNullOrWhiteSpace(ddlItemsize.SelectedValue) ? (object)DBNull.Value : ddlItemsize.SelectedValue);
                }
                else
                {
                    command.Parameters.AddWithValue("@itemsizeid", null);
                }

                if (string.IsNullOrEmpty(ddlItemsize1.SelectedValue) ||
                   (int.TryParse(ddlItemsize1.SelectedValue, out selectedValue) && selectedValue > 0))
                {
                    command.Parameters.AddWithValue("@itemsizeid1", string.IsNullOrWhiteSpace(ddlItemsize1.SelectedValue) ? (object)DBNull.Value : ddlItemsize1.SelectedValue);
                }
                else
                {
                    command.Parameters.AddWithValue("@itemsizeid1", null);
                }
                if (CheckBox1.Checked)
                {
                    command.Parameters.AddWithValue("@case", 20);
                }
                else 
                {
                    command.Parameters.AddWithValue("@case", 21);
                }

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
   
    public void page_clear()
    {
        
        ddlItemName.SelectedIndex = -1;
        ddlCategory.SelectedIndex = -1;
        ddlItemsize.SelectedIndex = -1;
        //ClientScript.RegisterStartupScript(this.GetType(), "ReloadPage", "location.reload();", true);
        BindGrid();
        gvInventory.PageIndex = 0;
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        page_clear();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        //Session.Clear();
        Response.Redirect("HomePage.aspx");
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        if (gvInventory.Rows.Count >= 20)
        {
            
            DisablePagingForExport();
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", "attachment;filename=FilteredGridViewExport.xls");
            Response.Charset = "";
            this.EnableViewState = false;

            using (System.IO.StringWriter sw = new System.IO.StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                   
                    gvInventory.RenderControl(hw);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();

                }
            }
        }
        else
        {
            // Clear the response and set the content type to Excel
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", "attachment;filename=FilteredGridViewExport.xls");
            Response.Charset = "";
            this.EnableViewState = false;

            using (System.IO.StringWriter sw = new System.IO.StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    // Remove paging to export all rows
                    gvInventory.AllowPaging = false;
                    // Rebind the GridView with all data if needed.

                    // Render the GridView to the HtmlTextWriter
                    gvInventory.RenderControl(hw);

                    // Write the rendered content to the response
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
        }


    }



    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for the specified ASP.NET server control.
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
                if (rowsAffected < 0)
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

    protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
    {

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
        
        int Item_categoryID;
        if (int.TryParse(ddlCategory.SelectedValue, out Item_categoryID) && Item_categoryID != 0)
        {
            Debug.WriteLine("test" + ddlCategory.SelectedValue + " " + Item_categoryID);

            LoadItemSize1(Item_categoryID);
            LoadItemSize2(Item_categoryID);
        }
        else
        {
            //ClearItemDetails();  // Optional: Clear fields if no valid item is selected
        }
    }
    private void LoadItemSize1(int Item_categoryID)
    {
        // Get the connection string from Web.config
        string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Define the command and specify the stored procedure
            using (SqlCommand command1 = new SqlCommand("ItemSizeDetails", connection))
            {
                command1.CommandType = CommandType.StoredProcedure;
                command1.Parameters.AddWithValue("@categoryID", Item_categoryID);
                command1.Parameters.AddWithValue("@Case", 4);
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
                    ddlItemsize.Items.Insert(0,new ListItem("-- Select Item Size --", "0"));
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

    private void LoadItemSize2(int Item_categoryID)
    {
        // Get the connection string from Web.config
        string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Define the command and specify the stored procedure
            using (SqlCommand command1 = new SqlCommand("ItemSizeDetails", connection))
            {
                command1.CommandType = CommandType.StoredProcedure;
                command1.Parameters.AddWithValue("@categoryID", Item_categoryID);
                command1.Parameters.AddWithValue("@Case", 5);
                // Open the connection
                connection.Open();

                // Execute the command and load results into a SqlDataReader
                SqlDataReader reader1 = command1.ExecuteReader();

                // Bind the data to the dropdown
                if (reader1.HasRows)
                {
                    //Debug.WriteLine(reader1.HasRows);


                    ddlItemsize1.DataSource = reader1;

                    ddlItemsize1.DataTextField = "Size_name";  // The column to display
                    ddlItemsize1.DataValueField = "Size_id";   // The column to use as the value
                    ddlItemsize1.DataBind();
                    ddlItemsize1.Items.Insert(0, new ListItem("-- Select Item Size --", "0"));
                    

                }
                reader1.Close();

            }
            connection.Close();
        }
    }

    protected void gvInventory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        // Set the new page index
        gvInventory.PageIndex = e.NewPageIndex;

        // Rebind the GridView to reflect the change
        CheckDropdownSelections_NEW();
    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if(CheckBox1.Checked)
        {

        }
    }
}