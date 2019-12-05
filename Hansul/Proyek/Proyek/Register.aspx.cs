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

         String myconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\SIB 17\Semester 5\Fai\Proyek FAI Github\WebEng\Hansul\Proyek\Proyek\App_Data\WebProject.mdf;Integrated Security=True";//punya Adriel
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
    }
}