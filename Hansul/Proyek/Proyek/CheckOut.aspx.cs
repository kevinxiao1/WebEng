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
    public partial class CheckOut : System.Web.UI.Page
    {
        String myconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\SIB 17\Semester 5\Fai\Proyek FAI Github\WebEng\Hansul\Proyek\Proyek\App_Data\WebProject.mdf;Integrated Security=True";//Punya Adriel
        // string myconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\WebEng\Hansul\Proyek\Proyek\App_Data\WebProject.mdf;Integrated Security=True";//punya Hansel
        //String myconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='D:\SIB\Semester 5\Web Engineering\WebEng\Hansul\Proyek\Proyek\App_Data\WebProject.mdf';Integrated Security=True";//punya Johannes
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
        public void SignOut()
        {
            Session["siapa"] = null;
            Response.Redirect("Home.aspx");
        }
        string ConvertHarga(string hrg)
        {
            string temp = "";
            int co = 0;
            for (int i = hrg.Length - 1; i >= 0; i--)
            {
                co++;
                if ((co % 3 == 0) && (i > 0))
                {
                    temp += hrg[i] + ",";
                }
                else
                {
                    temp += hrg[i];
                }
            }
            string temp2 = "";
            for (int i = temp.Length - 1; i >= 0; i--)
            {
                temp2 += temp[i];
            }
            return temp2;
        }
        public void detailPembelian()
        {
            string cmd = "";
            string productName = "";
            string username = "";
            LabelPesanan.Text = "";
            int subtotal = 0;
            if (Session["siapa"]==null)
            {
                cmd = "SELECT * FROM dbo.GuestCart";
            }
            else if (Session["siapa"] != null)
            {
                cmd = "SELECT * FROM dbo.CartUser";
                username = (string)Session["siapaUsername"];
            }
            productName = "SELECT * FROM dbo.Product";
            TestConn();
            SqlDataAdapter sq = new SqlDataAdapter(cmd, conn);
            SqlDataAdapter barang = new SqlDataAdapter(productName, conn);
            DataTable dt = new DataTable();
            DataTable DBbarang = new DataTable();
            sq.Fill(dt);
            barang.Fill(DBbarang);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (username=="")
                {
                    if (int.Parse(dt.Rows[i]["Id"].ToString()) >= int.Parse(Session["idguest"].ToString()))
                    {
                        for (int j = 0; j < DBbarang.Rows.Count; j++)
                        {
                            if (dt.Rows[i]["ProductID"].ToString() == DBbarang.Rows[j]["ProductID"].ToString())
                            {
                                int jum = int.Parse(dt.Rows[i]["Qty"].ToString());
                                int hrg = int.Parse(DBbarang.Rows[j]["SellPrice"].ToString());
                                subtotal += hrg;
                                if (jum<10)
                                {
                                    LabelPesanan.Text += "<li><a href='#'>"+DBbarang.Rows[j]["Name"].ToString()+"<span class='middle'>x 0"+jum+"</span><span class='last'>Rp. "+ConvertHarga(hrg+"")+"</span></a></li>";
                                }
                                else if (jum >= 10)
                                {
                                    LabelPesanan.Text += "<li><a href='#'>" + DBbarang.Rows[j]["Name"].ToString() + "<span class='middle'>x " + jum + "</span><span class='last'>Rp. " + ConvertHarga(hrg + "") + "</span></a></li>";
                                }
                                break;
                            }
                        }
                    }
                }
                else if (username!="")
                {
                    for (int j = 0; j < DBbarang.Rows.Count; j++)
                    {
                        if (dt.Rows[i]["ProductID"].ToString() == DBbarang.Rows[j]["ProductID"].ToString() && dt.Rows[i]["Username"].ToString()==username)
                        {
                            int jum = int.Parse(dt.Rows[i]["Qty"].ToString());
                            int hrg = int.Parse(DBbarang.Rows[j]["SellPrice"].ToString());
                            subtotal += hrg;
                            if (jum < 10)
                            {
                                LabelPesanan.Text += "<li><a href='#'>" + DBbarang.Rows[j]["Name"].ToString() + "<span class='middle'>x 0" + jum + "</span><span class='last'>Rp. " + ConvertHarga(hrg + "") + "</span></a></li>";
                            }
                            else if (jum >= 10)
                            {
                                LabelPesanan.Text += "<li><a href='#'>" + DBbarang.Rows[j]["Name"].ToString() + "<span class='middle'>x " + jum + "</span><span class='last'>Rp. " + ConvertHarga(hrg + "") + "</span></a></li>";
                            }
                            break;
                        }
                    }
                }
            }
            LabelSubtotal.Text = "<li><a href='#'>Subtotal<span>Rp. "+ConvertHarga(subtotal+"")+"</span></a></li>";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
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
                conn = new SqlConnection(myconn);
                detailPembelian();
            }
        }
        protected void btnSearch(object sender, EventArgs e)
        {

            Response.Write("<script> alert('" + search_input.Value + "')</script>");

            //btn_search.Text = "as";
            //  Response.Write("asa");
        }
    }
}