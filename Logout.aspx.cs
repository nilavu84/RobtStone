﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Clear session data
        Session.Clear();
        Session.Abandon();

        // Redirect to login page
        Response.Redirect("Login.aspx");
    }
}