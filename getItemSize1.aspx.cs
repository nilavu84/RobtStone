using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class getItemSize1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static List<ListItem> GetcategoryID(int parameter,int mthd)
    {
        List<ListItem> items = new List<ListItem>();
        string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;
        
       
        
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand("ITEMGRID_SEARCH_LOAD", connection)) //"ITEMGRID_SEARCH_LOAD"
            {
                command.CommandType = CommandType.StoredProcedure;
                if (mthd == 7)
                {
                    command.Parameters.AddWithValue("@ItemID", parameter);
                }
                else if(mthd == 8)
                {
                    command.Parameters.AddWithValue("@categoryID", parameter);
                }

                command.Parameters.AddWithValue("@Case", mthd);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (mthd == 7)
                    {
                        while (reader.Read())
                        {
                            items.Add(new ListItem
                            {
                                Text = reader["Category"].ToString(),
                                Value = reader["Item_categoryID"].ToString()
                            });
                        }
                    }
                    else if(mthd==8)
                    {

                        while (reader.Read())
                        {
                            items.Add(new ListItem
                            {
                                Text = reader["Size_name"].ToString(),
                                Value = reader["Size_id"].ToString()
                            });
                        }

                    }
                }
            }
            connection.Close();
        }
        
        return items;
    }

    
   /* public static List<ListItem>GetItemSizedetails(int categoryID)
    {
        List<ListItem> items = new List<ListItem>();
        string connectionString = ConfigurationManager.ConnectionStrings["StockConnectionstring"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand("ITEMGRID_SEARCH_LOAD", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@categoryID", categoryID);
                command.Parameters.AddWithValue("@Case", 8);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        items.Add(new ListItem
                        {
                            Text = reader["Size_name"].ToString(),
                            Value = reader["Size_id"].ToString()
                        });
                    }
                }
            }
            connection.Close();
        }
        return items;
    }*/
    public class DropdownItem
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }

}