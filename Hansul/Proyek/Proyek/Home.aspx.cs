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
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSearch(object sender, EventArgs e)
        {
           
            Response.Write("<script> alert('"+search_input.Value+"')</script>");

            //btn_search.Text = "as";
            //  Response.Write("asa");
        }

        protected void btnBuyNow(object sender, EventArgs e)
        {
            Response.Write("<script> alert('mantull')</script>");

        }
    }
}