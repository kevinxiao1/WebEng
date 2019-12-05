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
        public AdminDashboardCategory ad = new AdminDashboardCategory();
       // string myconn = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Downloads\Proyek\Proyek\App_Data\WebProject.mdf;Integrated Security = True";//punya Hansel
        String myconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='D:\SIB\Semester 5\Web Engineering\WebEng\Hansul\Proyek\Proyek\App_Data\WebProject.mdf';Integrated Security=True";//punya Johannes
        //string myconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\WebEng\Hansul\Proyek\Proyek\App_Data\WebProject.mdf;Integrated Security=True";//punya William
        SqlConnection conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(myconn);

            getdata();
            getProduct();
        }

        void getProduct()
        {
            string cmd = "SELECT dbo.Product.ProductID as ID, dbo.Product.Name as NamaProduk, dbo.Product.SellPrice as Harga from dbo.Product";
            string search = "";
            search = Request.QueryString["search"];
            if (search != "" && search != null)
            {
                cmd = "SELECT Product.ProductID, Pict.PictData as Gambar, Product.Name as NamaProduk, Product.SellPrice as Harga from dbo.Product Product,dbo.Pict Pict, dbo.Category Cat WHERE Product.Name = Pict.ProductId and Cat.CategoryID = Product.CategoryID and Cat.CategoryName = '" + search+ "'";
            }
            ad.TestConn();
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
                listProduct.Text = listProduct.Text + "<div class=" + "col-lg-4 col-sm-6" + "><div class=" + "single_product_item" + "><img src=data:image/png;base64," + tes2 + "><div class=" + "single_product_text" + "><h4>" + dt.Rows[i]["NamaProduk"] + "</h4><h3>" + dt.Rows[i]["Harga"] + "</h3><a href = " + "ProductDetail.aspx?id=" + dt.Rows[i]["ID"] + " class=" + "add_cart" + ">+ add to cart<i class=" + "ti-heart" + "></i></a></div></div></div>";
            }
        }

        void getdata()
        {
            ad.TestConn();

            //FORMAT(1234, 'C', 'fr-FR')
            SqlDataAdapter sq = new SqlDataAdapter("SELECT * from dbo.Category", conn);

            // SqlDataAdapter sq = new SqlDataAdapter("SELECT * from dbo.Product", conn);
            DataTable dt = new DataTable();
            sq.Fill(dt);
            //Response.Write("<script>alert(" + dt.Rows.Count + ")</script>");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == 0)
                {
                    ProductCategories.Text = "<li> <a href="+"#"+"> "+dt.Rows[i]["CategoryName"]+ " </ a > <span>(250)</span> </ li > ";
                }
                else
                {
                    ProductCategories.Text = ProductCategories.Text+"<li> <a href=" + "#" + "> " + dt.Rows[i]["CategoryName"] + " </ a > <span>(250)</span> </ li > ";
                }
            }


            
            //GridView1.DataSource = dt;
            //GridView1.DataBind();

            conn.Close();
        }
    }
}