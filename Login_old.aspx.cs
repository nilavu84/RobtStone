using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    DateTime startDate;
    DateTime lastAccessed;
    string sessionId;

        protected void btnLogin_Click(object sender, EventArgs e)
    {
        string username = txtUsername.Text.Trim(); // Use Trim to remove any leading/trailing whitespace
        string password = txtPassword.Text.Trim();
       
        
        string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand("useauthentication", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Authname", username);
                command.Parameters.AddWithValue("@Authpassword", password);
                command.Parameters.AddWithValue("@case", 1);

                try
                {
                    // Open the connection
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    FormsAuthentication.SetAuthCookie(username, false);
                    if (count > 0)
                    {
                        // Redirect to a welcome page or dashboard
                        sessionMaintain();
                        string username1 = txtUsername.Text;
                        Session["UserName"] = username1;
                        Application["Username"] = username1;
                        Response.Redirect("HomePage.aspx", false);
                        Context.ApplicationInstance.CompleteRequest();
                        SaveSessionToDatabase(sessionId, username1, startDate, lastAccessed);
                    }
                    else
                    {
                        // Show error message
                        lblError.Text = "Invalid username or password.";
                        lblError.Visible = true;
                    }
                }
                catch (Exception ex)
                { Console.WriteLine(ex); }
                {
                    // Log the exception (you can log to a file or a logging system)
                    lblError.Text = "An error occurred while processing your request. Please try again later.";
                    lblError.Visible = true;
                }
            }
        }
    
}
 
    public void sessionMaintain()
    {
        string username = txtUsername.Text.Trim(); // Use Trim to remove any leading/trailing whitespace
        string password = txtPassword.Text.Trim();

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
                command.Parameters.AddWithValue("@Authname", username);
                command.Parameters.AddWithValue("@Authpassword", password);
                command.Parameters.AddWithValue("@case", 2);  // Indicating this is an update operation

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
    protected void Page_Load(object sender, EventArgs e)
    {
        
        // Check if the session is new (first time visit or session expired)
        // Session.Clear();
        //Session.Abandon();
        if (Session["SessionId"] == null)
        {
            // Generate a unique Session ID if it doesn't exist
             sessionId = Session.SessionID;
            string userName = string.Empty; // Default value if the user is not logged in.

            // If the user is logged in, you can get the username from Session or Identity
            if (User.Identity.IsAuthenticated)
            {
                userName = User.Identity.Name; // This will give you the username if using Forms Authentication or ASP.NET Identity
                Session["SessionId"] = sessionId;
                //Session["UserName"] = userName;
            }


            startDate = DateTime.Now;
            lastAccessed = DateTime.Now;

            // Store session data in the session object (optional)




            // Save the session data to the database
            //
        }
        else
        {
            // Session already exists, update the last accessed time
            sessionId = Session.SessionID;
            lastAccessed = DateTime.Now;
            
            // Update the last accessed time in the database
            //UpdateSessionLastAccessed(sessionId, lastAccessed);
        }
    }
    private void SaveSessionToDatabase(string sessionId, string userName, DateTime startDate, DateTime lastAccessed)
    {
        // Get the connection string from Web.config
        string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;

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
    private void UpdateSessionLastAccessed(string sessionId, DateTime lastAccessed)
    {
        // Get the connection string from Web.config
        string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Define the SQL query to update the last accessed time
            string query = "UPDATE UserSessions SET LastAccessed = @LastAccessed WHERE SessionId = @SessionId";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // Add parameters to prevent SQL injection
                command.Parameters.AddWithValue("@SessionId", sessionId);
                command.Parameters.AddWithValue("@LastAccessed", lastAccessed);

                connection.Open();
                command.ExecuteNonQuery(); // Execute the update query
            }
        }
    }
}