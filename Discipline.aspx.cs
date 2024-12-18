using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Discipline : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        gvdiscipline.Columns[0].Visible = false;
        LoadItemNames();
    }
    private void LoadItemNames()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {

            using (SqlCommand command = new SqlCommand("GetItemNames", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@case",1);
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    gvdiscipline.DataSource = dataTable;
                    gvdiscipline.DataBind();

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
    private void Discipline_insert()
    {
        // Get the connection string from Web.config
        string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Define the command and specify the stored procedure
            using (SqlCommand command = new SqlCommand("GetItemNames", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Case", 2);
                command.Parameters.AddWithValue("@itemname", txtdisiplinneme.Text);
                command.Parameters.AddWithValue("@status", Userstatus.SelectedIndex);
                // Open the connection
                connection.Open();

                // Execute the command and load results into a SqlDataReader
                SqlDataReader reader = command.ExecuteReader();

                // Bind the data to the dropdown
                //  ddlItemName.DataSource = reader;
                //  ddlItemName.DataTextField = "Item_Name";  // The column to display
                //  ddlItemName.DataValueField = "ItemID";   // The column to use as the value
                //  ddlItemName.DataBind();

                // Add a default item at the beginning of the list



            }
        }

    }
    protected void btnClear_Click(object sender,EventArgs e)
    {
        txtdisiplinneme.Text = "";
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        // Redirect to a specific page or the previous page
        Session.Clear();
        Response.Redirect("HomePage.aspx");

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Discipline_insert();
        LoadItemNames();
    }
}