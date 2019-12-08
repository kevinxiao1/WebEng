using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyek
{
    public partial class Home : System.Web.UI.Page
    {
        public void SignOut()
        {
            Session["siapa"] = null;
            Response.Redirect("Home.aspx");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string logout = Request.QueryString["sgout"];
                    if (logout=="true")
                    {
                        SignOut();
                    }
                }
                catch (Exception)
                {
                    
                }
                if (Session["siapa"]==null)
                {
                    lbWesLogin.Text = "Welcome, guest";
                    lbTokek.Text = "<a class='dropdown-item' href='login.aspx'>Sign In</a>"+ "<a class='dropdown-item' href='register.aspx'>Sign Up</a>";
                }
                else
                {
                    lbWesLogin.Text = "Welcome, "+(string)Session["siapa"];
                    lbTokek.Text = "<a class='dropdown-item' href='#'>Edit Profile</a>" + "<a class='dropdown-item' href='home.aspx?sgout=true'>Logout</a>";
                }
            }
        }
        protected void btnSearch(object sender, EventArgs e)
        {
            //Response.Write("<script> alert('"+search_input.Value+"')</script>");
            Response.Redirect("ProductCategory.aspx?name="+search_input.Value+"");
        }

        protected void btnBuyNow(object sender, EventArgs e)
        {
            Response.Write("<script> alert('mantull')</script>");

        }
    }
}