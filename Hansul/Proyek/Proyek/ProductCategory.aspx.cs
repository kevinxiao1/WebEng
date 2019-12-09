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
    public partial class ProductCategory : System.Web.UI.Page
    {

        //public AdminDashboardCategory ad = new AdminDashboardCategory();


        //String myconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\SIB 17\Semester 5\Fai\Proyek FAI Github\WebEng\Hansul\Proyek\Proyek\App_Data\WebProject.mdf;Integrated Security=True";//Punya Adriel
        //string myconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\WebEng\Hansul\Proyek\Proyek\App_Data\WebProject.mdf;Integrated Security=True";//punya Hansel
        //string myconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\WebEng\Hansul\Proyek\Proyek\App_Data\WebProject.mdf;Integrated Security=True";//punya Hansel
        //String myconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='D:\SIB\Semester 5\Web Engineering\WebEng\Hansul\Proyek\Proyek\App_Data\WebProject.mdf';Integrated Security=True";//punya Johannes
       string myconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\SIB\Projek FAI\WebEng\Hansul\Proyek\Proyek\App_Data\WebProject.mdf;Integrated Security=True";//punya William
        SqlConnection conn;
        public void SignOut()
        {
            Session["siapa"] = null;
            Response.Redirect("Home.aspx");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(myconn);

            getdata();
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
            string cmd = "SELECT dbo.Product.ProductID as ID, dbo.Product.Name as NamaProduk, dbo.Product.SellPrice as Harga from dbo.Product";
            string search = "";
            string name = "";
            string sort = "";
            search = Request.QueryString["search"];
            name = Request.QueryString["name"];
            sort = Request.QueryString["Sort"];

            if (search != "" && search != null)
            {
               cmd = "SELECT Product.ProductID as ID, Product.Name as NamaProduk, Product.SellPrice as Harga from dbo.Product Product, dbo.Category Cat WHERE Cat.CategoryID = Product.CategoryID and Cat.CategoryName = '" + search + "'";
            }
            else if(name != "" && name != null)
            {
                cmd = "SELECT Product.ProductID as ID, Product.Name as NamaProduk, Product.SellPrice as Harga from dbo.Product Product, dbo.Category Cat WHERE Cat.CategoryID = Product.CategoryID and Product.Name LIKE  '%" + name + "%'";
            }
            else if (sort !="" && sort != null)
            {
                if(sort == "Name")
                {
                    cmd = "SELECT dbo.Product.ProductID as ID, dbo.Product.Name as NamaProduk, dbo.Product.SellPrice as Harga from dbo.Product ORDER BY dbo.Product.Name";
                }
                else if (sort == "SellPrice")
                {
                    cmd = "SELECT dbo.Product.ProductID as ID, dbo.Product.Name as NamaProduk, dbo.Product.SellPrice as Harga from dbo.Product ORDER BY dbo.Product.SellPrice";
                }
                else if (sort == "Category")
                {
                    cmd = "SELECT dbo.Product.ProductID as ID, dbo.Product.Name as NamaProduk, dbo.Product.SellPrice as Harga from dbo.Product ORDER BY dbo.Product.CategoryID";
                }
            }
            TestConn();
            SqlDataAdapter sq = new SqlDataAdapter(cmd, conn);
            DataTable dt = new DataTable();
            sq.Fill(dt);
            string tes = "";
            string tes2 = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string gbr = "SELECT ProductID, PictData FROM Pict where ProductID = '"+ dt.Rows[i]["NamaProduk"] + "'";
                SqlDataAdapter sg = new SqlDataAdapter(gbr, conn);
                DataTable dt2 = new DataTable();
                sg.Fill(dt2);
                for (int j = 0; j < dt2.Rows.Count; j++)
                {
                    if(dt2.Rows[j]["ProductID"].ToString()==tes)
                    {

                    }
                    else
                    {
                        byte[] ba = (byte[])dt2.Rows[j]["PictData"];
                        string strBase64 = Convert.ToBase64String(ba);
                        tes2 = strBase64;
                        break;
                        
                        //list_string.Add(strBase64);
                    }
                    //list_byte.Add(ba);
                }
                int harga = int.Parse(dt.Rows[i]["Harga"].ToString());
                listProduct.Text = listProduct.Text + "<div class=" + "col-lg-4 col-sm-6" + "><div class=" + "single_product_item" + "><img src=data:image/png;base64," + tes2 + "><div class=" + "single_product_text" + "><h4>" + dt.Rows[i]["NamaProduk"] + "</h4><h3>Rp. " + Convert.ToDecimal(harga).ToString("#,##0") + "</h3><a href = " + "ProductDetail.aspx?id=" + dt.Rows[i]["ID"] + " class=" + "add_cart" + ">View Product<i class=" + "ti-heart" + "></i></a></div></div></div>";
               
                FoundTxt.Text = "<p><span>" + dt.Rows.Count + " </span> Product Found</p>";
            }
        }

        void getdata()
        {
            TestConn();

            //FORMAT(1234, 'C', 'fr-FR')
            SqlDataAdapter sq = new SqlDataAdapter("SELECT * from dbo.Category", conn);

            // SqlDataAdapter sq = new SqlDataAdapter("SELECT * from dbo.Product", conn);
            DataTable dt = new DataTable();
            sq.Fill(dt);
            //Response.Write("<script>alert(" + dt.Rows.Count + ")</script>");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SqlDataAdapter countData = new SqlDataAdapter("SELECT COUNT(ProductID) as JML FROM dbo.Product WHERE CategoryID = '" +dt.Rows[i]["CategoryID"]+ "'",conn);
                DataTable CountD = new DataTable();
                countData.Fill(CountD);
                if (i == 0)
                {
                    ProductCategories.Text = "<li> <a href="+"#"+"> "+dt.Rows[i]["CategoryName"]+ " </ a > <span>("+CountD.Rows[0]["JML"]+")</span> </ li > ";
                }
                else
                {
                    ProductCategories.Text = ProductCategories.Text+"<li> <a href=" + "#" + "> " + dt.Rows[i]["CategoryName"] + " </ a > <span>(" + CountD.Rows[0]["JML"] + ")</span> </ li > ";
                }
            }
            //GridView1.DataSource = dt;
            //GridView1.DataBind();

            conn.Close();
        }

        public void search(string t)
        {
            string cmd = "SELECT Product.ProductID as ID, Product.Name as NamaProduk, Product.SellPrice as Harga from dbo.Product Product, dbo.Category Cat WHERE Cat.CategoryID = Product.CategoryID and Product.Name LIKE  '%" + t + "%'";
            TestConn();
            SqlDataAdapter sq = new SqlDataAdapter(cmd, conn);
            DataTable dt = new DataTable();
            sq.Fill(dt);
            string tes = "";
            string tes2 = "";
            listProduct.Text = "";
            FoundTxt.Text = "<p><span>"+ dt.Rows.Count + " </span> Product Found</p>";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string gbr = "SELECT ProductID, PictData FROM Pict where ProductID = '" + dt.Rows[i]["NamaProduk"] + "'";
                SqlDataAdapter sg = new SqlDataAdapter(gbr, conn);
                DataTable dt2 = new DataTable();
                sg.Fill(dt2);
                for (int j = 0; j < dt2.Rows.Count; j++)
                {
                    if (dt2.Rows[j]["ProductID"].ToString() == tes)
                    {

                    }
                    else
                    {
                        byte[] ba = (byte[])dt2.Rows[j]["PictData"];
                        string strBase64 = Convert.ToBase64String(ba);
                        tes2 = strBase64;
                        break;

                        //list_string.Add(strBase64);
                    }
                    //list_byte.Add(ba);
                }
                listProduct.Text = listProduct.Text + "<div class=" + "col-lg-4 col-sm-6" + "><div class=" + "single_product_item" + "><img src=data:image/png;base64," + tes2 + "><div class=" + "single_product_text" + "><h4>" + dt.Rows[i]["NamaProduk"] + "</h4><h3>" + dt.Rows[i]["Harga"] + "</h3><a href = " + "ProductDetail.aspx?id=" + dt.Rows[i]["ID"] + " class=" + "add_cart" + ">View Product<i class=" + "ti-heart" + "></i></a></div></div></div>";
            }
        }

        protected void btnSearch(object sender, EventArgs e)
        {
            //Response.Write("<script> alert('" + search_input.Value + "')</script>");
            search(search_input.Value);
        }
    }
}