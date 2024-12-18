using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Numeric;
using System.Diagnostics;

public partial class ViewPage : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

        sessionCheck();
        if (!IsPostBack)
        {
            BindGrid(string.Empty);
            LoadItemNames();        // Initial load with no filter
        }
        else 
        {
            //Console.WriteLine(ddlDiscipline.SelectedValue);
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
    private void BindGrid(string searchTerm)
    {

        string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {

            using (SqlCommand command = new SqlCommand("ITEMGRID_SEARCH_LOAD", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                command.Parameters.AddWithValue("@case", lblsearchName.SelectedIndex);
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
    int itemID;
    protected void ddlDiscipline_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selectedValue1 = Convert.ToInt32(ddlDiscipline.SelectedIndex).ToString();// Debug line
        //Console.WriteLine("Selected Value: " + selectedValue); // Output to check value

        if (int.TryParse(selectedValue1, out itemID) && itemID != 0)
        
        {
            LoadItemCategory(itemID);
        }
        else
        {
            Console.WriteLine("Invalid itemID: " + itemID); // Debug output
                                                            // Optional: ClearItemDetails(); 
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
                if (reader.Read())
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
        }
    }
    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
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
                ddlDiscipline.DataSource = reader;
                ddlDiscipline.DataTextField = "Item_Name";  // The column to display
                ddlDiscipline.DataValueField = "ItemID";   // The column to use as the value
                ddlDiscipline.DataBind();

                // Add a default item at the beginning of the list
                ddlDiscipline.Items.Insert(0, new ListItem("-- Select Item --", "0"));


            }
        }

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for the specified ASP.NET server control.
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        // Redirect to a specific page or the previous page
        Session.Clear();
        Response.Redirect("HomePage.aspx");

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        // Redirect to a specific page or the previous page
        //BindGrid_old();

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
    protected void btnsearch_Click(object sender, EventArgs e)
    {

        try
        {
            //// Check if the TextBox is not empty
            //if (!string.IsNullOrEmpty(ddlsearchName.Text))
            //{
            //    // Call the BindGrid method with the TextBox input as the parameter
            //    BindGrid(ddlsearchName.Text.Trim());
            //}
            //Console.WriteLine("Password updated successfully.");
            //CheckDropdownSelections();
            Debug.WriteLine(ddlDiscipline.SelectedValue.ToString());
        }
        catch (Exception e1)
        {
            
            Debug.WriteLine(e.ToString());
            Debug.WriteLine(ddlDiscipline.SelectedValue.ToString());
        }
        //CheckDropdownSelections();


    }
    private void BindGrid_old()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {

            using (SqlCommand command = new SqlCommand("ITEMGRID_LOAD", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    gvInventory.DataSource = dataTable;
                    gvInventory.DataBind();

                }

                //connection.Open();
                //SqlDataReader reader = command.ExecuteReader();
                //gvInventory.DataSource = reader;
                //gvInventory.DataBind();

            }
        }
    }

    //protected override void Render(HtmlTextWriter writer)
    //{
    //    foreach (ListItem item in ddlCategory.Items)
    //    {
    //        ClientScript.RegisterForEventValidation(ddlCategory.UniqueID, item.Value);
    //    }
    //    base.Render(writer);
    //}

    protected void CheckDropdownSelections()
        {
            // Check if the first dropdown is selected and the others are not selected
            if (ddlDiscipline.SelectedIndex != 0 && ddlCategory.SelectedIndex == 0 && ddlItemSize.SelectedIndex == 0)
            {
                Console.WriteLine("First dropdown selected, second and third not selected");
            }
            // Check if the first and second dropdowns are selected, but the third is not
            else if (ddlDiscipline.SelectedIndex != 0 && ddlCategory.SelectedIndex != 0 && ddlItemSize.SelectedIndex == 0)
            {
                Console.WriteLine("First and second dropdowns selected, third not selected");
            }
            // Check if all dropdowns are selected
            else if (ddlDiscipline.SelectedIndex != 0 && ddlCategory.SelectedIndex != 0 && ddlItemSize.SelectedIndex != 0)
            {
                Console.WriteLine("All dropdowns selected");
            }
            // Check for other combinations or prompt the user to select the appropriate dropdown
            else
            {
                Console.WriteLine("Please ensure all dropdowns are selected as required");
            }
        }
    }
