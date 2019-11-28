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
        //string myconn = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Downloads\Proyek\Proyek\App_Data\WebProject.mdf;Integrated Security = True";//punya Hansel
        string myconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\WebEng\Hansul\Proyek\Proyek\App_Data\WebProject.mdf;Integrated Security=True";//punya William
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
        protected void Page_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(myconn);

            getProduct();
        }
    }
}