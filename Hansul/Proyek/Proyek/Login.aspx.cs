using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyek
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnLogin(object sender, EventArgs e)
        {
            if (txtusername.Value == "admin" && txtpassword.Value == "admin")
            {
                Response.Redirect("AdminDashboard.aspx");
            }

        }
    }
}