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
    public partial class AdminDashboardPromo : System.Web.UI.Page
    {
        //String myconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\SIB 17\Semester 5\Fai\Proyek FAI\Tampulan\Proyek\Proyek\App_Data\WebProject.mdf;Integrated Security=True";//punya Adriel
        String myconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='D:\SIB\Semester 5\Web Engineering\WebEng\Hansul\Proyek\Proyek\App_Data\WebProject.mdf';Integrated Security=True";//punya Johannes
        //string myconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\WebEng\Hansul\Proyek\Proyek\App_Data\WebProject.mdf;Integrated Security=True";//punya Hansel
        SqlConnection conn;
        //   public AdminDashboardCategory ad = new AdminDashboardCategory();

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
            getdata();
        }

        void getdata()
        {
            try
            {
                TestConn();
                SqlDataAdapter sq = new SqlDataAdapter("SELECT * from promo where PromoID<>'PR000'", conn);
            
                DataTable dt = new DataTable();
                sq.Fill(dt);
                dt.Columns.Add("Action");

                GridView1.DataSource = dt;
                GridView1.DataBind();
                conn.Close();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message.ToString());
            }
        }

        bool cekPromoName(string name)
        {
           TestConn();
            SqlDataAdapter sq = new SqlDataAdapter("SELECT * FROM dbo.Promo", conn);
            DataTable dt = new DataTable();
            sq.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                if (dt.Rows[i]["PromoName"].ToString() == name)
                {
                    conn.Close();
                    return (true);
                }
            }
            conn.Close();
            return (false);
        }

        string getLastIndex(string table, string fieldname, string inisial)
        {
            string kode = "";
            try
            {
                TestConn();
                string q = "select max(substring(" + fieldname + ",3,3)) as yes from " + table;
                SqlDataAdapter sq = new SqlDataAdapter(q, conn);
                DataTable dt = new DataTable();
                sq.Fill(dt);
                int norut = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    try
                    {
                        norut = int.Parse((dt.Rows[i]["yes"].ToString()));
                    }
                    catch (Exception)
                    {
                        norut = 0;
                    }

                }
                norut++;
                kode = inisial + (norut + "").PadLeft(3, '0');
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message.ToString());
            }
            return (kode);
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "editime")
            {
                GridViewRow grv = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                int index = grv.RowIndex;

                lbl_tempid.Text = (GridView1.Rows[index].Cells[0].Text.ToString());
                tb_name.Text = (GridView1.Rows[index].Cells[1].Text.ToString());

                ddl_type.Text = (GridView1.Rows[index].Cells[2].Text.ToString());
                tb_count.Text= (GridView1.Rows[index].Cells[3].Text.ToString());


                btn_insert.Enabled = false;
                btn_edit.Enabled = true;

            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {

                Button lb = new Button();
                lb.Text = "Edit";
                lb.CommandName = "editime";
                e.Row.Cells[4].Controls.Add(lb);
                lb.Click += Lb_Click; ;

                Button lb2 = new Button();
                lb2.Text = "Delete";
                lb2.CommandName = "delete";
                e.Row.Cells[4].Controls.Add(lb2);
                lb2.Click += Lb2_Click; ;
                lb2.OnClientClick = "return confirm('Do you want Delete?')";


            }
        }

        private void Lb2_Click(object sender, EventArgs e)
        {
        }

        private void Lb_Click(object sender, EventArgs e)
        {
        }

        protected void GridView1_RowDeleting1(object sender, GridViewDeleteEventArgs e)
        {
            string index = (GridView1.Rows[e.RowIndex].Cells[0].Text.ToString());


            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM dbo.Promo WHERE PromoID = '" + index + "'", conn);

            cmd.ExecuteNonQuery();

            conn.Close();

            getdata();
        }

        protected void btn_insert_Click(object sender, EventArgs e)
        {
            if (cekPromoName(tb_name.Text) == true)
            {
                Response.Write("<script>alert('Promo name is already exist'); </script>");
            }
            else
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Promo(PromoID,PromoName,PromoType,PromoCount) values('" + getLastIndex("Promo", "PromoID", "PR") + "','" + tb_name.Text + "','"+ddl_type.SelectedItem.ToString()+"' ,'"+tb_count.Text+"')", conn);
                cmd.ExecuteNonQuery();
                conn.Close();

                getdata();
            }
        }

        protected void btn_edit_Click(object sender, EventArgs e)
        {
            
                conn.Open();


                SqlCommand cmd = new SqlCommand("Update dbo.Promo set PromoName = '" + tb_name.Text + "',PromoType='"+ddl_type.SelectedItem.ToString()+"',PromoCount='"+tb_count.Text+"' WHERE PromoID = '" + lbl_tempid.Text + "'", conn);

                cmd.ExecuteNonQuery();


                conn.Close();

                btn_edit.Enabled = false;
                btn_insert.Enabled = true;
                getdata();
            

           
        }
    }
}