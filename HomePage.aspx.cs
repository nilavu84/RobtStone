using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel.Security;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HomePage : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["Username"] == null) // Check if the session has expired
        {
            Response.Redirect("Login.aspx");
        }
        if (!IsPostBack)
        {
            string username = Globalfunction.GetLoggedInUsername();

            if (Application["Username"] != null)
            {
                lblUsername.Text = "Welcome, " + Application["Username"].ToString();
                SESSIONLOGIN();
                // Send the server's current time to the client
               
                DateTime serverTime = DateTime.Now;
                lblClock.Text = serverTime.ToString("hh:mm:ss tt");

                // Register the script to sync the clock using String.Format
                string script = String.Format(
                    "initializeClock('{0}');",
                    serverTime.ToString("yyyy-MM-ddTHH:mm:ss")
                );
                ClientScript.RegisterStartupScript(this.GetType(), "SyncClock", script, true);

            }
            else
            {
                // Redirect to login page if no user is logged in
                
            }
        }

    }
    

    private void SaveSessionToDatabase(string sessionId, string userName, DateTime startDate, DateTime lastAccessed)
    {
        // Get the connection string from Web.config
        string connectionString = ConfigurationManager.ConnectionStrings["YourConnectionString"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Define the SQL query to insert session data
            string query = "INSERT INTO UserSessions (SessionId, UserName, StartDate, LastAccessed,STATUS) VALUES (@SessionId, @UserName, @StartDate, @LastAccessed,1)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // Add parameters to prevent SQL injection
                command.Parameters.AddWithValue("@SessionId", sessionId);
                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@StartDate", startDate);
                command.Parameters.AddWithValue("@LastAccessed", lastAccessed);

                connection.Open();
                command.ExecuteNonQuery(); // Execute the insert query
            }
        }
    }
    string Rolename , UserName1;
    public void SESSIONLOGIN()
    {

        string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {

            using (SqlCommand command = new SqlCommand("useauthentication", connection))
            {
                //Session["Sessionid"] = Session.SessionID;

                command.CommandType = CommandType.StoredProcedure;

                // Add parameters and assign values from form inputs
                command.Parameters.AddWithValue("@Sessionid", Session.SessionID);
                command.Parameters.AddWithValue("@Case", 4);
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0)
                    {
                        UserName1 = dataTable.Rows[0]["Username"].ToString();
                        Rolename = dataTable.Rows[0]["rolename"].ToString();

                    }
                    else
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
        if (!IsPostBack)
        {
            string username = UserName1;

            // Add a menu item programmatically if the user is an Admin
            
            if (Rolename=="Admin")
            {

                Menu1.Visible = true;
                Menu2.Visible = true;
                Menu3.Visible = true;
                Menu4.Visible = true;
                Menu5.Visible = false;



            }
            if (Rolename == "EUser")
            {
             
                Menu1.Visible = true;
                Menu2.Visible = true;
                Menu3.Visible = false;
                Menu4.Visible = false;
                Menu5.Visible = false;

            }
            if (Rolename == "EVUser")
            {

                Menu1.Visible = false;
                Menu2.Visible = true;
                Menu3.Visible = false;
                Menu4.Visible = false;
                Menu5.Visible = false;   
            }

            if (Rolename == "Issue")
            {

                Menu1.Visible = false;
                Menu2.Visible = false;
                Menu3.Visible = true;
                Menu4.Visible = true;
                Menu5.Visible = false;

            }
            if (Rolename == "VIssue")
            {

                Menu1.Visible = false;
                Menu2.Visible = false;
                Menu3.Visible = false;
                Menu4.Visible = true;
                Menu5.Visible = false;

            }
            if (Rolename == "Administrator")
            {
                Menu1.Visible = true;
                Menu2.Visible = true;
                Menu3.Visible = true;
                Menu4.Visible = true;
                Menu5.Visible = true;
                


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
}