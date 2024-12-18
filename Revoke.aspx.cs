using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.DynamicData;

public partial class Revoke : System.Web.UI.Page
{
    int inventory_id;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Username"] == null) // Check if the session has expired
        {
            Response.Redirect("Login.aspx");
        }
        Projectdetails();
        Session["Sessionid"] = Session.SessionID;
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
        if (txtItementryid.Text != "")
        {
            txtItementryid.Enabled = false;
            BindGrid();
            LoadIproject();
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
                command.Parameters.AddWithValue("@INVENTORY_ID", txtItementryid.Text);
                command.Parameters.AddWithValue("@CASE", 15);
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

    

    protected void btBack_Click(object sender, EventArgs e)
    {
        //Session.Clear();
        Response.Redirect("HomePage.aspx");
    }

    protected void btClear_Click(object sender, EventArgs e)
    {
        clear();
    }
    private void LoadIproject()
    {
        //ddlissuedproject.Items.Clear();
        // Get the connection string from Web.config
        string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Define the command and specify the stored procedure
            using (SqlCommand command = new SqlCommand("item_issued_details", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@INVENTORY_ID", txtItementryid.Text.ToString());
                command.Parameters.AddWithValue("@CASE", 16);

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        if (string.IsNullOrEmpty(dataTable.Rows[0]["Project_code"].ToString()))
                        {
                            ddlissuedproject.SelectedIndex = -1;
                        }
                        else
                        {
                            ddlissuedproject.SelectedValue = dataTable.Rows[0]["issuedprojectno"].ToString();
                        }
                    }
                }
            }
        }
    }
    protected void RadioButton_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton selectedButton = (RadioButton)sender;

        // Handle the logic for the selected radio button
        string selectedValue = selectedButton.Text; // Get the text of the selected button
        if(selectedValue == "Yes") 
        {
            //ddlissuedproject.Visible = true;
            
            LoadIproject();
            btnissuerevoke.Text = "Update";
;       }
        else if (selectedValue == "No")
        {  
            //ddlissuedproject.Visible = false;
            btnissuerevoke.Text = "Revoke";
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

    protected void btnissuerevoke_Click(object sender, EventArgs e)
    {
        if(btnissuerevoke.Text== "Update") 
        {
            Updateproject();
            Response.Write("<script>alert('Project Changed');</script>");
            btnissuerevoke.Text = "Revoke";
        }
        else if (btnissuerevoke.Text== "Revoke")
        {
            if (txtItementryid.Text != "")
            {
                isserevoke();
                Response.Write("<script>alert('Last Enter the Transaction Revoked');</script>");
            }
            
                
            
        }
    }
    public void Updateproject()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Define the command and specify the stored procedure
            using (SqlCommand command = new SqlCommand("item_issued_details", connection))
            {
                Session["Sessionid"] = Session.SessionID;
                //try
                //{
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CASE", 18);
                command.Parameters.AddWithValue("@SessionId", Session["Sessionid"]);
                command.Parameters.AddWithValue("@INVENTORY_ID", txtItementryid.Text);
                command.Parameters.AddWithValue("@PROJECTNO", ddlissuedproject.SelectedValue);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                //Response.Write("<script>alert('Item Revoked successfully');</script>");
                //Response.Redirect(Request.RawUrl);
                BindGrid();
            }
        }
    }
    public void clear() 
    {
        txtItementryid.Text = "";
        txtItementryid.Enabled = true;
        ddlissuedproject.SelectedIndex = -1;
        gvInventory.DataSource = null;
        gvInventory.DataBind();
    

    }
    public void isserevoke()
    {
        
        string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Define the command and specify the stored procedure
            using (SqlCommand command = new SqlCommand("item_issued_details", connection))
            {
                Session["Sessionid"] = Session.SessionID;
                //try
                //{
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CASE", 17);
                command.Parameters.AddWithValue("@SessionId", Session["Sessionid"]);
                command.Parameters.AddWithValue("@INVENTORY_ID", txtItementryid.Text);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                Response.Write("<script>alert('Item Revoked successfully');</script>");
                //Response.Redirect(Request.RawUrl);
                BindGrid();
            }
        }
        
    }
}