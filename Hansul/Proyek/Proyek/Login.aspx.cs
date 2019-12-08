﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyek
{
    public partial class Login : System.Web.UI.Page
    {
        // String myconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='D:\SIB\Semester 5\Web Engineering\WebEng\Hansul\Proyek\Proyek\App_Data\WebProject.mdf';Integrated Security=True";//punya Johannes

        String myconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\SIB 17\Semester 5\Fai\Proyek FAI Github\WebEng\Hansul\Proyek\Proyek\App_Data\WebProject.mdf;Integrated Security=True";//punya Adriel

       // String myconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\WebEng\Hansul\Proyek\Proyek\App_Data\WebProject.mdf;Integrated Security=True";//punya HANSUL
        protected void Page_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(myconn);
        }
        SqlConnection conn;
        public void TestConn()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
        }

        bool login(string username,string password)
        {
            try
            {
                TestConn();

                SqlDataAdapter sq = new SqlDataAdapter("SELECT * FROM dbo.Users", conn);
                DataTable dt = new DataTable();
                sq.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["Username"].ToString() == username)
                    {
                       if (dt.Rows[i]["Password"].ToString() == password)
                       {
                            Session["siapa"] = dt.Rows[i]["Username"].ToString();
                            return (true);
                       }
                    }

                }
                conn.Close();
                return false;
            }
            catch (Exception ex)
            {

                Response.Write(ex.Message.ToString());
                return false;
            }
        }
        protected void btnSearch(object sender, EventArgs e)
        {

            Response.Write("<script> alert('" + search_input.Value + "')</script>");

            //btn_search.Text = "as";
            //  Response.Write("asa");
        }
        protected void btnLogin(object sender, EventArgs e)
        {
            if (txtusername.Value == "admin" && txtpassword.Value == "admin")
            {
                Response.Redirect("AdminDashboardProduct.aspx");
            }
            else
            {
                if(login(txtusername.Value+"", txtpassword.Value+"")) // berhasil login sesuai set Cookie User
                {
                    Response.Redirect("Home.aspx");
                }
                else // login gagal 
                {

                }
            }


        }
    }
}