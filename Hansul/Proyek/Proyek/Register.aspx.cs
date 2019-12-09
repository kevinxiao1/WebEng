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
    public partial class Register : System.Web.UI.Page
    {
        // String myconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='D:\SIB\Semester 5\Web Engineering\WebEng\Hansul\Proyek\Proyek\App_Data\WebProject.mdf';Integrated Security=True";//punya Johannes

        //String myconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\SIB 17\Semester 5\Fai\Proyek FAI Github\WebEng\Hansul\Proyek\Proyek\App_Data\WebProject.mdf;Integrated Security=True";//punya Adriel
        //String myconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\WebEng\Hansul\Proyek\Proyek\App_Data\WebProject.mdf;Integrated Security=True";//punya HANSUL
        // string myconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\SIB\Projek FAI\WebEng\Hansul\Proyek\Proyek\App_Data\WebProject.mdf;Integrated Security=True";//punya William
        string myconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\SIB 17\Semester 5\Fai\Proyek FAI Github\WebEng\Hansul\Proyek\Proyek\App_Data\WebProject.mdf;Integrated Security=True";//punya Adriel

        // String myconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\WebEng\Hansul\Proyek\Proyek\App_Data\WebProject.mdf;Integrated Security=True";//punya HANSUL
        
        // String myconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='D:\SIB\Semester 5\Web Engineering\WebEng\Hansul\Proyek\Proyek\App_Data\WebProject.mdf';Integrated Security=True";//punya Johanne
        //String myconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\SIB 17\Semester 5\Fai\Proyek FAI Github\WebEng\Hansul\Proyek\Proyek\App_Data\WebProject.mdf;Integrated Security=True";//punya Adriel
       // String myconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\WebEng\Hansul\Proyek\Proyek\App_Data\WebProject.mdf;Integrated Security=True";//punya HANSUL
        //string myconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\SIB\Projek FAI\WebEng\Hansul\Proyek\Proyek\App_Data\WebProject.mdf;Integrated Security=True";//punya William
        public void SignOut()
        {
            Session["siapa"] = null;
            Response.Redirect("Home.aspx");
        }


       // String myconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\SIB 17\Semester 5\Fai\Proyek FAI Github\WebEng\Hansul\Proyek\Proyek\App_Data\WebProject.mdf;Integrated Security=True";//punya Adriel
       // String myconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\WebEng\Hansul\Proyek\Proyek\App_Data\WebProject.mdf;Integrated Security=True";//punya HANSUL

        SqlConnection conn;
        public void TestConn()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(myconn);
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

        bool cekusername(string username)
        {
            try
            {
                TestConn();

                SqlDataAdapter sq = new SqlDataAdapter("SELECT * FROM dbo.Users", conn);
                DataTable dt = new DataTable();
                sq.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if(dt.Rows[i]["Username"].ToString() == username)
                    {
                        return false;
                    }
                    
                }
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                
                Response.Write(ex.Message.ToString());
                return false;
            }
        }

        protected void btnLogin(object sender, EventArgs e)//btn register
        {
            if(txtpassword.Value!=txtCPassword.Value)
            {
                Response.Write("<script> alert('Password dan Confirmasi Password tidak sama!')</script>");
            }
            else if(cekusername(txtusername.Value+""))
            {
                TestConn();
                SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Users values('" + txtusername.Value+"" + "','" + txtpassword.Value+"" + "','"+txtname.Value+"','" + txtAddress.Value+"" + "' ," + int.Parse(txtPhone.Value+"") + ",1)", conn);
                cmd.ExecuteNonQuery();
                conn.Close();

                Response.Write("<script> alert('berhasil!')</script>");
            }
            else
            {
                Response.Write("<script> alert('Username Sudah Terdaftar!')</script>");
            }
        }
        protected void btnSearch(object sender, EventArgs e)
        {

            //Response.Write("<script> alert('" + search_input.Value + "')</script>");

            //btn_search.Text = "as";
            //  Response.Write("asa");
        }
    }
}