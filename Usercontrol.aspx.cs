using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Usercontrol : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        gvInventory.Columns[0].Visible = false;
        BindUser();
        //if (!IsPostBack)
        //{
        //    BindGridView();
        //}
    }


    private void BindGridView()
    {
        // Replace with your actual data retrieval logic
        gvInventory.DataSource = GetData(); // Example method to get data from database
        gvInventory.DataBind();
    }
    private DataTable GetData()
    {
        // Mock data source (replace with actual database call)
        DataTable dt = new DataTable();
        dt.Columns.Add("userid");
        dt.Columns.Add("username");
        dt.Columns.Add("Activestatus");
        //dt.Rows.Add("1", "John Doe", "30");
        //dt.Rows.Add("2", "Jane Smith", "25");
        return dt;
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvInventory.EditIndex = e.NewEditIndex;
        BindGridView(); // Rebind the grid to display the editable row
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvInventory.EditIndex = -1;
        BindGridView(); // Rebind the grid to cancel editing mode
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        // Get the ID of the record to update
        string id = gvInventory.DataKeys[e.RowIndex].Value.ToString();

        // Get the new values entered by the user
        GridViewRow row = gvInventory.Rows[e.RowIndex];
        string userid = ((TextBox)row.Cells[1].Controls[0]).Text;
        string username = ((TextBox)row.Cells[2].Controls[0]).Text;
        string activestatus = ((TextBox)row.Cells[3].Controls[0]).Text;

        // Update the record in your data source
        UpdateRecord(userid, username, activestatus); // Replace with your update logic

        // Reset the edit index and rebind the grid
        gvInventory.EditIndex = -1;
        BindGridView();
    }
    protected void UpdateRecord(string userid, string username, string activestatus)
    {
        string connectionString = "StockConnectionstring";
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "UPDATE usermanager SET username = @userName, activestatus = @activestatus WHERE userID = @userid";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@useridID", userid);
            cmd.Parameters.AddWithValue("@userName", username);
            cmd.Parameters.AddWithValue("@userid", userid);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        // Redirect to a specific page or the previous page
        Session.Clear();
        Response.Redirect("HomePage.aspx");

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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        
            // Get the connection string from Web.config
            string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Define the command and specify the stored procedure
                using (SqlCommand command = new SqlCommand("user_loading", connection))
                {
                    Session["Sessionid"] = Session.SessionID;
                    if (btnSave.Text == "Save")
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        // Add parameters and assign values from form inputs
                        command.Parameters.AddWithValue("@case", 2);
                        command.Parameters.AddWithValue("@username", txtUsername.Text);
                        command.Parameters.AddWithValue("@password", txtpassword.Text);
                        command.Parameters.AddWithValue("@user_status", Userstatus.SelectedItem); // Assuming "1" is "Yes/True" and "0" is "No/False"
                        command.Parameters.AddWithValue("@user_Role", Roleid.SelectedItem);
                        command.Parameters.AddWithValue("@SessionId", Session["Sessionid"]);
                    }
                    else
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        // Add parameters and assign values from form inputs
                        command.Parameters.AddWithValue("@case", 4);
                        command.Parameters.AddWithValue("@username", txtUsername.Text);
                        command.Parameters.AddWithValue("@password", txtpassword.Text);
                        command.Parameters.AddWithValue("@user_status", Userstatus.SelectedItem); // Assuming "1" is "Yes/True" and "0" is "No/False"
                        command.Parameters.AddWithValue("@user_Role", Roleid.SelectedItem);
                        command.Parameters.AddWithValue("@SessionId", Session["Sessionid"]);
                        command.Parameters.AddWithValue("@userid", id);

                    }
                        // Open the connection and execute the command
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                }
            



            // Optional: Display a success message or clear the form
            Response.Write("<script>alert('User Created successfully');</script>");
            BindUser();
            //ClearItemDetails();

        }
    }
    protected void gvInventory_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        
            // Get the connection string from Web.config
            string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Define the command and specify the stored procedure
                using (SqlCommand command = new SqlCommand("user_loading", connection))
                {
                    Session["Sessionid"] = Session.SessionID;
                    //try
                    //{
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters and assign values from form inputs
                    command.Parameters.AddWithValue("@case", 4);
                    command.Parameters.AddWithValue("@username", txtUsername.Text);
                    command.Parameters.AddWithValue("@password", txtpassword.Text);
                    command.Parameters.AddWithValue("@user_status", Userstatus.SelectedItem); // Assuming "1" is "Yes/True" and "0" is "No/False"
                    command.Parameters.AddWithValue("@user_Role", Roleid.SelectedItem);
                    command.Parameters.AddWithValue("@SessionId", Session["Sessionid"]);
                    // Open the connection and execute the command
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                }
            }
        
    }
        protected void btnClear_Click(object sender, EventArgs e)
    {
        TEXT_CLEAR();

    }
    private void BindUser()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {

            using (SqlCommand command = new SqlCommand("user_loading", connection))
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
    private void TEXT_CLEAR()
    {
        txtUsername.Text = "";
        txtpassword.Text = "";

        var cntls = GetAll(this, typeof(RadioButton));
        foreach (Control cntrl in cntls)
        {
            RadioButton _rb = (RadioButton)cntrl;
            if (_rb.Checked)
            {
                _rb.Checked = false;
            }
        }
    }
    public IEnumerable<Control> GetAll(Control control, Type type)
    {
        var controls = control.Controls.Cast<Control>();
        return controls.SelectMany(ctrls => GetAll(ctrls, type)).Concat(controls).Where(c => c.GetType() == type);
    }

    protected void gvInventory_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvInventory.EditIndex = e.NewEditIndex;
        BindUser();
    }
    protected void gvInventory_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvInventory.EditIndex = -1;
        BindUser();
    }
    public static string id = "";
    protected void gvInventory_SelectedIndexChanged(object sender, GridViewUpdateEventArgs e)
    {
        // Get the connection string from Web.config
        string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Define the command and specify the stored procedure
               Session["Sessionid"] = Session.SessionID;
                using (SqlCommand command = new SqlCommand("user_loading", connection))
                {
                    GridViewRow row = gvInventory.SelectedRow;

                    id = gvInventory.DataKeys[row.RowIndex]["userid"].ToString();


                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters and assign values from form inputs
                    command.Parameters.AddWithValue("@action", "Get Details");
                    command.Parameters.AddWithValue("@userid", id);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        if (dataTable.Rows.Count > 0)
                        {
                            txtUsername.Text = dataTable.Rows[0]["@username"].ToString();
                            txtpassword.Text = dataTable.Rows[0]["@password"].ToString();
                            Userstatus.SelectedValue = dataTable.Rows[0]["@user_status"].ToString();
                            Roleid.SelectedValue = dataTable.Rows[0]["@rolename"].ToString();
                            btnSave.Text = "Update";
                        }

                    }

                                    }
            }
        Response.Write("<script>alert('Record Update successfully');</script>");
        BindUser();
    }
  
    public static int RE ;
    protected void gvInventory_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {

            using (SqlCommand command = new SqlCommand("user_loading", connection))
            {
                GridViewRow row = gvInventory.SelectedRow;

                id = gvInventory.DataKeys[row.RowIndex]["userid"].ToString();


                command.CommandType = CommandType.StoredProcedure;

                // Add parameters and assign values from form inputs
                command.Parameters.AddWithValue("@case", 3);
                command.Parameters.AddWithValue("@userid", id);
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0)
                    {
                        txtUsername.Text = dataTable.Rows[0]["Username"].ToString();
                        Userstatus.SelectedValue = dataTable.Rows[0]["rolename"].ToString();
                        Roleid.SelectedValue = dataTable.Rows[0]["status"].ToString();
                        btnSave.Text = "Update";
                    }

                }

                

            }
        }
    }
}

