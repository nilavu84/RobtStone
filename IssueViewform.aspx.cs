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
using System.Web.Services;

[WebService(Namespace = "http://192.168.0.227:90/surplus/GVwebService.asmx")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.ComponentModel.ToolboxItem(false)]
public partial class IssueViewform : System.Web.UI.Page
{
    
    int DataTable;
    protected void Page_Load(object sender, EventArgs e)
    {
        //sessionCheck();

       

        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
        Response.Cache.SetNoStore();
        if (!IsPostBack)
        {
            LoadItemNames();
            BindGrid();
            ddlsearch.Visible = true;
            txtDate.Visible = false;
            calDate.Visible = false;
            ddluniversal.SelectedIndex = 4;
            txtDate.Visible = true;
            ddlsearch.Visible = false;
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

            using (SqlCommand command = new SqlCommand("item_issued_details", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                //command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
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
    protected void DisablePagingForExport()
    {
        gvInventory.AllowPaging = false; // Disable paging
        BindGrid();                 // Re-bind the GridView to all data
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {

        //lblError.Visible = false; // Reset error visibility
        //string selectedValue = ddluniversal.SelectedValue;
        //string inputValue = txtDate.Text.Trim();

        lblError.Visible = false; // Reset error visibility
        string selectedValue = ddluniversal.SelectedValue;
        string inputValue = txtDate.Text.Trim();
        if (CheckBox1.Checked)
        {
            CheckDropdownSelections_NEW();
        }
        else 
        { 
        
        if (selectedValue == "4") // Validate for Date format
        {
            DateTime parsedDate; // Declare the variable explicitly
            if (!DateTime.TryParse(inputValue, out parsedDate))
            {
                lblError.Text = "Please enter a valid date in the format DD/MM/YYYY.";
                lblError.Visible = true;
                return;
            }
            else
            {
                CheckDropdownSelections_NEW();
            }
        }
        else if (selectedValue == "5") // Validate for Number format
        {
            decimal parsedNumber; // Declare the variable explicitly
            if (!decimal.TryParse(inputValue, out parsedNumber))
            {
                lblError.Text = "Please enter a valid number.";
                lblError.Visible = true;
                return;
            }
            else
            {
                CheckDropdownSelections_NEW();
            }
        }
        else if (selectedValue == "3")
        {
            if (ddlsearch.SelectedIndex == 0)
            {
                lblError.Text = "Please Select User Name.";
                lblError.Visible = true;
                return;
            }
            else
            {
                CheckDropdownSelections_NEW();
            }
        }
        else if (selectedValue == "2")
        {
            if (ddlsearch.SelectedIndex == 0)
            {
                lblError.Text = "Please Select Store Location.";
                lblError.Visible = true;
                return;
            }
            else
            {
                CheckDropdownSelections_NEW();
            }
        }
        else if (selectedValue == "1")
        {
            if (ddlsearch.SelectedIndex == 0)
            {
                lblError.Text = "Please Select the Project.";
                lblError.Visible = true;
                return;
            }
            else
            {
                CheckDropdownSelections_NEW();
            }
        }
        }



    }

    [WebMethod]
    protected void CheckDropdownSelections_NEW()
    {
        DateTime issueDate;
        string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                
                using (SqlCommand command = new SqlCommand("item_issued_details", connection))
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
                    command.Parameters.AddWithValue("@ITEMCATEGORY", string.IsNullOrWhiteSpace(ddlCategory.SelectedValue) ? (object)DBNull.Value : ddlCategory.SelectedValue);
                }
                else
                {
                    command.Parameters.AddWithValue("@ITEMCATEGORY", null);
                }
                if (string.IsNullOrEmpty(ddlItemsize.SelectedValue) ||
                    (int.TryParse(ddlItemsize.SelectedValue, out selectedValue) && selectedValue > 0))
                {
                    command.Parameters.AddWithValue("@ITEMSIZE", string.IsNullOrWhiteSpace(ddlItemsize.SelectedValue) ? (object)DBNull.Value : ddlItemsize.SelectedValue);
                }
                else
                {
                    command.Parameters.AddWithValue("@ITEMSIZE", null);
                }
                
                if (string.IsNullOrEmpty(ddlItemsize1.SelectedValue) ||
                   (int.TryParse(ddlItemsize1.SelectedValue, out selectedValue) && selectedValue > 0))
                {
                    command.Parameters.AddWithValue("@ITEMSIZE1", string.IsNullOrWhiteSpace(ddlItemsize1.SelectedValue) ? (object)DBNull.Value : ddlItemsize1.SelectedValue);
                }
                else
                {
                    command.Parameters.AddWithValue("@ITEMSIZE1", null);
                }
                
                
                if (ddluniversal.SelectedIndex == 1)// project wise
                {
                    if (string.IsNullOrEmpty(ddlsearch.SelectedValue) ||
                        (int.TryParse(ddlsearch.SelectedValue, out selectedValue) && selectedValue > 0))
                    {
                        command.Parameters.AddWithValue("@PROJECTNO", string.IsNullOrWhiteSpace(ddlsearch.SelectedValue) ? (object)DBNull.Value : ddlsearch.SelectedValue);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@PROJECTNO", null);
                    }
                    
                }
                else if (ddluniversal.SelectedIndex == 2)
                {
                    if (string.IsNullOrEmpty(ddlsearch.SelectedValue) ||
                        (int.TryParse(ddlsearch.SelectedValue, out selectedValue) && selectedValue > 0))
                    {
                        command.Parameters.AddWithValue("@STOREID", string.IsNullOrWhiteSpace(ddlsearch.SelectedValue) ? (object)DBNull.Value : ddlsearch.SelectedValue);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@STOREID", null);
                    }
                    
                }
                else if(ddluniversal.SelectedIndex == 3)
                {
                    if (string.IsNullOrEmpty(ddlsearch.SelectedValue) ||
                        (int.TryParse(ddlsearch.SelectedValue, out selectedValue) && selectedValue > 0))
                    {
                        command.Parameters.AddWithValue("@USERNAME", string.IsNullOrWhiteSpace(ddlsearch.SelectedValue) ? (object)DBNull.Value : ddlsearch.SelectedValue);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@USERNAME", null);
                    }
                    
                }
                else if (ddluniversal.SelectedIndex == 4)
                {
                    if (DateTime.TryParse(txtDate.Text, out issueDate))
                    {
                        command.Parameters.AddWithValue("@ISSUEDATE", issueDate);
                    }
                    //command.Parameters.AddWithValue("@ISSUEDATE", txtDate.Text);
                }
                else if (ddluniversal.SelectedIndex == 5)
                {
                    command.Parameters.AddWithValue("@ENTRYID", txtDate.Text);
                }
                command.Parameters.AddWithValue("@case", 13);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        gvInventory.DataSource = dataTable;
                        gvInventory.DataBind();

                    }
                }
            }
            //Response.Write("<script>alert('Discipline Selected');</script>");
        }
    protected void CheckDropdownSelections()
    {
        // Check if only `ddlItemName` is selected, and `ddlCategory` and `ddlItemsize` are not selected
        if ((ddlItemName.SelectedIndex > 0) &&
        ddlCategory.SelectedIndex <= 0 &&
        ddlItemsize.SelectedIndex <= 0 &&
        ddlItemsize1.SelectedIndex <= 0)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                using (SqlCommand command = new SqlCommand("item_issued_details", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ItemID", ddlItemName.SelectedValue);
                    command.Parameters.AddWithValue("@case", 13);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        gvInventory.DataSource = dataTable;
                        gvInventory.DataBind();

                    }
                }
            }
            //Response.Write("<script>alert('Discipline Selected');</script>");
        }

        // Check if `ddlItemName` and `ddlCategory` are selected, but `ddlItemsize` is not selected
        if ((ddlItemName.SelectedIndex > 0) &&
        (ddlCategory.SelectedIndex > 0) &&
        ddlItemsize.SelectedIndex <= 0 &&
        ddlItemsize1.SelectedIndex <= 0)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                using (SqlCommand command = new SqlCommand("item_issued_details", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ItemID", ddlItemName.SelectedValue);
                    command.Parameters.AddWithValue("@ITEMCATEGORY", ddlCategory.SelectedValue);
                    command.Parameters.AddWithValue("@case", 4);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        gvInventory.DataSource = dataTable;
                        gvInventory.DataBind();

                    }
                }
            }
            //Response.Write("<script>alert('Discipline and Category Selected');</script>");
        }

        // Check if all three dropdowns are selected
        if ((ddlItemName.SelectedIndex > 0) &&
        (ddlCategory.SelectedIndex > 0) &&
        (ddlItemsize.SelectedIndex > 0) &&
        (ddlItemsize1.SelectedIndex <= 0))
        {
            string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                using (SqlCommand command = new SqlCommand("item_issued_details", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ItemID", ddlItemName.SelectedValue);
                    command.Parameters.AddWithValue("@ITEMCATEGORY", ddlCategory.SelectedValue);
                    command.Parameters.AddWithValue("@ITEMSIZE", ddlItemsize.SelectedValue);
                    command.Parameters.AddWithValue("@case", 5);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        gvInventory.DataSource = dataTable;
                        gvInventory.DataBind();

                    }
                }
            }
            //Response.Write("<script>alert('Discipline, Category, and Size Selected');</script>");
        }

        //Item Size2
        if ((ddlItemName.SelectedIndex > 0) &&
        (ddlCategory.SelectedIndex > 0) &&
        (ddlItemsize.SelectedIndex > 0) &&
        ddlItemsize1.SelectedIndex > 0)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                using (SqlCommand command = new SqlCommand("item_issued_details", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ItemID", ddlItemName.SelectedValue);
                    command.Parameters.AddWithValue("@ITEMCATEGORY", ddlCategory.SelectedValue);
                    command.Parameters.AddWithValue("@ITEMSIZE", ddlItemsize.SelectedValue);
                    command.Parameters.AddWithValue("@ITEMSIZE1", ddlItemsize1.SelectedValue);
                    command.Parameters.AddWithValue("@case", 6);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        gvInventory.DataSource = dataTable;
                        gvInventory.DataBind();

                    }
                }
            }
            //Response.Write("<script>alert('Discipline, Category, and Size Selected');</script>");
        }

        // Check if none of the dropdowns are selected
        if ((ddlItemName.SelectedIndex <= 0) &&
        (ddlCategory.SelectedIndex <= 0) &&
        (ddlItemsize.SelectedIndex <= 0) &&
        (ddlItemsize1.SelectedIndex <= 0))
        {
            if (ddluniversal.SelectedIndex > 0 && ddlsearch.SelectedIndex > 0)
            {
                if (ddluniversal.SelectedIndex == 1)
                {
                    if (ddluniversal.SelectedIndex == 1 && ddlItemName.SelectedIndex <= 0 && ddlCategory.SelectedIndex<= 0)
                    {
                        string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {

                            using (SqlCommand command = new SqlCommand("item_issued_details", connection))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@PROJECTNO", ddlsearch.SelectedValue);
                                command.Parameters.AddWithValue("@case", 9);
                                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                                {
                                    DataTable dataTable = new DataTable();
                                    adapter.Fill(dataTable);

                                    gvInventory.DataSource = dataTable;
                                    gvInventory.DataBind();

                                }
                            }
                        }
                        //Response.Write("<script>alert('project details');</script>");
                    }
                    else if (ddluniversal.SelectedIndex==1 && ddlItemName.SelectedIndex>0 && ddlCategory.SelectedIndex <= 0)
                    {
                        string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {

                            using (SqlCommand command = new SqlCommand("item_issued_details", connection))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@PROJECTNO", ddlsearch.SelectedValue);
                                command.Parameters.AddWithValue("@ItemID", ddlItemName.SelectedValue);
                                command.Parameters.AddWithValue("@case", 14);
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
                    else if (ddluniversal.SelectedIndex == 1 && ddlItemName.SelectedIndex > 0 && ddlCategory.SelectedIndex <= 0)
                    {
                        string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {

                            using (SqlCommand command = new SqlCommand("item_issued_details", connection))
                            {
                                
                                command.CommandType = CommandType.StoredProcedure;
                                if (ddlsearch.SelectedValue != null)
                                {
                                    command.Parameters.AddWithValue("@PROJECTNO", ddlsearch.SelectedValue);
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@PROJECTNO", null);
                                }
                                    command.Parameters.AddWithValue("@ItemID", ddlItemName.SelectedValue);
                                command.Parameters.AddWithValue("@case", 13);
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
                else if (ddluniversal.SelectedIndex == 2)
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {

                        using (SqlCommand command = new SqlCommand("item_issued_details", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@STOREID", ddlsearch.SelectedValue);
                            command.Parameters.AddWithValue("@case", 10);
                            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                            {
                                DataTable dataTable = new DataTable();
                                adapter.Fill(dataTable);

                                gvInventory.DataSource = dataTable;
                                gvInventory.DataBind();

                            }
                        }
                    }
                    //Response.Write("<script>alert('Store details');</script>");
                }
                else if (ddluniversal.SelectedIndex == 3)
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {

                        using (SqlCommand command = new SqlCommand("item_issued_details", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@STOREID", ddlsearch.SelectedValue);
                            command.Parameters.AddWithValue("@case", 11);
                            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                            {
                                DataTable dataTable = new DataTable();
                                adapter.Fill(dataTable);

                                gvInventory.DataSource = dataTable;
                                gvInventory.DataBind();

                            }
                        }
                    }
                    //Response.Write("<script>alert('User details');</script>");
                }
            }
            else if (ddluniversal.SelectedIndex > 0 && txtDate.Text != "")
            {
                string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    using (SqlCommand command = new SqlCommand("item_issued_details", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ISSUEDATE", txtDate.Text);
                        command.Parameters.AddWithValue("@case", 12);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            gvInventory.DataSource = dataTable;
                            gvInventory.DataBind();

                        }
                    }
                }
                //Response.Write("<script>alert('Date Details');</script>");
            }
            else
            {
                Response.Write("<script>alert('Please Select at Least One Option');</script>");
            }
        }
    }
    public void page_clear()
    {

        ddlItemName.SelectedIndex = -1;
        ddlCategory.SelectedIndex = -1;
        ddlItemsize.SelectedIndex = -1;
        ddlItemsize1.SelectedIndex = -1;
        ddluniversal.SelectedIndex = -1;
        ddlsearch.Items.Clear();
        //ClientScript.RegisterStartupScript(this.GetType(), "ReloadPage", "location.reload();", true);
        BindGrid();
        gvInventory.PageIndex = 0;
        txtDate.Text = "";
        ddluniversal.SelectedIndex = 4;
        txtDate.Visible = true;
        txtDate.Attributes["placeholder"] = "DD/MM/YYYY";
        ddlsearch.Visible = false;
        CheckBox1.Checked = false;
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
        if (gvInventory.Rows.Count >= 25)
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
                    BindGrid(); // Rebind the GridView with all data if needed.

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
    protected override void Render(HtmlTextWriter writer)
    {
        ClientScript.RegisterForEventValidation("ControlID"); // Replace with the actual control ID
        base.Render(writer);
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
    protected void ddluniversal_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddluniversal.SelectedIndex == 1)
        {
            ddlsearch.Visible = true;
            txtDate.Visible = false;
            calDate.Visible = false;
            ddlsearch.Items.Clear();
            txtDate.Text = "";
            Projectdetails();
        }
        else if (ddluniversal.SelectedIndex == 2)
        {
            ddlsearch.Visible = true;
            txtDate.Visible = false;
            calDate.Visible = false;
            ddlsearch.Items.Clear();
            txtDate.Text = "";
            storedetails();
        }
        else if (ddluniversal.SelectedIndex == 3)
        {
            ddlsearch.Visible = true;
            txtDate.Visible = false;
            calDate.Visible = false;
            ddlsearch.Items.Clear();
            txtDate.Text = "";
            Usernamedetails();
        }
        else if (ddluniversal.SelectedIndex == 4)
        {
            txtDate.Text = "";
            ddlsearch.Visible = false;
            ddlsearch.Items.Clear();
            txtDate.Visible=true;
            txtDate.Attributes["placeholder"] = "dd/mm/YYYY";
            //calDate.Visible = true;
        }
        else if (ddluniversal.SelectedIndex == 5)
        {
            txtDate.Text = "";
            ddlsearch.Visible = false;
            ddlsearch.Items.Clear();
            txtDate.Visible = true;
            txtDate.Attributes["placeholder"] = "Entry ID";
            //calDate.Visible = true;
        }
    }
    public void issuedatewise()
    {
        // Get the connection string from Web.config
        string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Define the command and specify the stored procedure
            using (SqlCommand command = new SqlCommand("item_issued_details", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Case", 8);
                // Open the connection
                connection.Open();

                // Execute the command and load results into a SqlDataReader
                SqlDataReader reader = command.ExecuteReader();

                // Bind the data to the dropdown
                ddlsearch.DataSource = reader;
                ddlsearch.DataTextField = "IssueDate";  // The column to display
                //ddlsearch.DataValueField = "UserId";   // The column to use as the value
                ddlsearch.DataBind();

                // Add a default item at the beginning of the list
                ddlsearch.Items.Insert(0, new ListItem("-- Select Date --", "0"));
            }
        }
    }
    public void Usernamedetails()
    {
        // Get the connection string from Web.config
        string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Define the command and specify the stored procedure
            using (SqlCommand command = new SqlCommand("item_issued_details", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Case", 7);
                // Open the connection
                connection.Open();

                // Execute the command and load results into a SqlDataReader
                SqlDataReader reader = command.ExecuteReader();

                // Bind the data to the dropdown
                ddlsearch.DataSource = reader;
                ddlsearch.DataTextField = "Username";  // The column to display
                ddlsearch.DataValueField = "UserId";   // The column to use as the value
                ddlsearch.DataBind();

                // Add a default item at the beginning of the list
                ddlsearch.Items.Insert(0, new ListItem("-- Select User Name --", "0"));
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
                ddlsearch.DataSource = reader;
                ddlsearch.DataTextField = "storename";  // The column to display
                ddlsearch.DataValueField = "store_id";   // The column to use as the value
                ddlsearch.DataBind();

                // Add a default item at the beginning of the list
                ddlsearch.Items.Insert(0, new ListItem("-- Select Store Name --", "0"));
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
                ddlsearch.DataSource = reader;
                ddlsearch.DataTextField = "Projectname";  // The column to display
                ddlsearch.DataValueField = "Project_id";   // The column to use as the value
                ddlsearch.DataBind();

                // Add a default item at the beginning of the list
                ddlsearch.Items.Insert(0, new ListItem("-- Select Project Name --", "0"));
            }
        }
    }
    protected void calDate_SelectionChanged(object sender, EventArgs e) 
    {
        // Update the textbox with the selected date from the calendar
        txtDate.Text = calDate.SelectedDate.ToString("DD/MM/YYY");
        calDate.Visible = false;

    }
    protected void txtDate_SelectionChanged(object sender, EventArgs e)
    {
        // Update the textbox with the selected date from the calendar
        txtDate.Text = calDate.SelectedDate.ToString("DD/MM/YYY");
        calDate.Visible = false;
    }
    protected void ddlsearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selectedValue = ddlsearch.SelectedValue;

        if (selectedValue == "custom")
        {
            // Allow user to select a custom date from the calendar
            txtDate.Text = "Select a date from the calendar.";
        }
        else if (!string.IsNullOrEmpty(selectedValue))
        {
            // Update the textbox with the selected value
            txtDate.Text = selectedValue;

            // Optionally, set the calendar's selected date
            DateTime selectedDate;
            if (DateTime.TryParse(selectedValue, out selectedDate))
            {
                calDate.SelectedDate = selectedDate;
                calDate.VisibleDate = selectedDate;
            }
        }
        else
        {
            txtDate.Text = string.Empty; // Clear the textbox
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
        ddluniversal.SelectedIndex = 4;
        txtDate.Text = "";
        txtDate.Attributes["placeholder"] = "DD/MM/YYYY";
    }
}