using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace Proyek
{
    public partial class ProductDetail : System.Web.UI.Page
    {
        //String myconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\SIB 17\Semester 5\Fai\Proyek FAI Github\WebEng\Hansul\Proyek\Proyek\App_Data\WebProject.mdf;Integrated Security=True";//Punya Adriel

        //string myconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\WebEng\Hansul\Proyek\Proyek\App_Data\WebProject.mdf;Integrated Security=True";//punya Hansel
        String myconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='D:\SIB\Semester 5\Web Engineering\WebEng\Hansul\Proyek\Proyek\App_Data\WebProject.mdf';Integrated Security=True";//punya Johannes
        //string myconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\WebEng\Hansul\Proyek\Proyek\App_Data\WebProject.mdf;Integrated Security=True";//punya William
        SqlConnection conn;
        public void TestConn()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
        }

        void getProduct()
        {
            string cmd = "SELECT dbo.Category.CategoryName as Cat, dbo.Product.Name as NamaProduk, dbo.Product.SellPrice as Harga from dbo.Product ,dbo.Pict, dbo.Category WHERE dbo.Product.CategoryID = dbo.Category.CategoryID and dbo.Product.ProductID = '" + Request.QueryString["id"] + "'";
            TestConn();
            SqlDataAdapter sq = new SqlDataAdapter(cmd, conn);
            DataTable dt = new DataTable();
            sq.Fill(dt);
            DescProduct.Text = "<h3>"+dt.Rows[0]["NamaProduk"]+" </h3>" +
                "<h2>"+ dt.Rows[0]["Harga"] + "</h2>" +
                "<ul class='list'>" +
                "<li><a class='active' href='#'><span>Category</span> : "+ dt.Rows[0]["Cat"] + "</a></li>" +
                "<li>" +
                "<a href = '#'> <span> Availibility </span> : In Stock</a>" +
                  "</li>" +
                "</ul>" +
                "<p></p>";
            conn.Close();
            getGambar(dt.Rows[0]["NamaProduk"].ToString());
        }

        void getGambar(string id)
        {
            TestConn();
            string cmd = "SELECT PictData From Pict Where ProductID = '"+ id + "'";
            SqlDataAdapter sq = new SqlDataAdapter(cmd, conn);
            DataTable dt = new DataTable();
            sq.Fill(dt);
            LbGambar.Text = "<div class='product_image_area section_padding'>" +
            "<div class='container'>" +
            "<div class='row s_product_inner justify-content-between'>" +
            "<div class='col-lg-7 col-xl-7'>" +
            "<div class='product_slider_img'>" +
            "<div id = 'vertical'>";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                byte[] ba = (byte[])dt.Rows[i]["PictData"];
                string strBase64 = Convert.ToBase64String(ba);
                string tes2 = strBase64;
                LbGambar.Text += "<div data-thumb = 'data:image/png;base64, " + tes2 + "'>" +
                 "<img src = 'data:image/png;base64, "+tes2+"' />" +
                 "</div>";
            }
            conn.Close();
        }
        public void SignOut()
        {
            Session["siapa"] = null;
            Response.Redirect("home.aspx");
        }

        protected void btnSearch(object sender, EventArgs e)
        {

            Response.Write("<script> alert('" + search_input.Value + "')</script>");

            //btn_search.Text = "as";
            //  Response.Write("asa");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(myconn);

            getProduct();

            if (!IsPostBack)
            {
                try
                {
                    string logout = Request.QueryString["sgout"];
                    if (logout == "true")
                    {
                        SignOut();
                    }
                }
                catch (Exception)
                {

                }
                if (Session["siapa"] == null)
                {
                    lbWesLogin.Text = "Welcome, guest";
                    lbTokek.Text = "<a class='dropdown-item' href='login.aspx'>Sign In</a>" + "<a class='dropdown-item' href='register.aspx'>Sign Up</a>";
                }
                else
                {
                    lbWesLogin.Text = "Welcome, " + (string)Session["siapa"];
                    lbTokek.Text = "<a class='dropdown-item' href='#'>Edit Profile</a>" + "<a class='dropdown-item' href='home.aspx?sgout=true'>Logout</a>";
                }
            }
        }

        protected void btn_insert_Click(object sender, EventArgs e)
        {
            //Response.Write("<script>alert('sasa') </script>");
            string id = Request.QueryString["id"];
            if (Session["siapa"] == null) //guest
            {
                if(Session["idguest"]==null)
                {
                    TestConn();

                    int temp = 0;

                    SqlDataAdapter sq = new SqlDataAdapter("SELECT * FROM dbo.GuestCart", conn);
                    DataTable dt = new DataTable();
                    sq.Fill(dt);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        temp = int.Parse(dt.Rows[i]["Id"].ToString());



                    }
                    conn.Close();
                    temp++;

                    

                    Session["idguest"] = temp;
                }

                TestConn();
                SqlCommand cmd = new SqlCommand("INSERT INTO dbo.GuestCart(ProductID,Qty) values('" + id + "','" + qty.Value+"" + "')", conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            else
            {
                

                TestConn();
                SqlCommand cmd = new SqlCommand("INSERT INTO dbo.CartUser(Username,ProductID,Qty) values('" + Session["siapa"].ToString()+"','" + id + "','" + qty.Value + "" + "')", conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }

        }
    }
}