using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyek
{
    public partial class AdminDashboardCategory : System.Web.UI.Page
    {
        String myconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\SIB 17\Semester 5\Fai\Proyek FAI\Tampulan\Proyek\Proyek\App_Data\WebProject.mdf;Integrated Security=True";//Punya Adriel
        //String myconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='D:\SIB\Semester 5\Web Engineering\WebEng\Hansul\Proyek\Proyek\App_Data\WebProject.mdf';Integrated Security=True";//punya Johannes
        //string myconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\SIB\Projek FAI\Proyek\Proyek\App_Data\WebProject.mdf;Integrated Security=True";//punya Will
        //string myconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\WebEng\Hansul\Proyek\Proyek\App_Data\WebProject.mdf;Integrated Security=True";//punya Hansel
        SqlConnection conn;

        public void TestConn()
        {
            if(conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
        }

        public void TData()
        {
            TestConn();
            SqlDataAdapter sq = new SqlDataAdapter("SELECT CategoryID, CategoryName from dbo.Category where flagCT = 1", conn);
            
            DataTable dt = new DataTable();
            sq.Fill(dt);
            dt.Columns.Add("Action");

            GridView1.DataSource = dt;
            GridView1.DataBind();

            conn.Close();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(myconn);
            TData();
        }

        string getLastIndex(string table, string fieldname, string inisial)
        {
            TestConn();
            string q = "select max(substring(" + fieldname + ",3,3)) as yes from " + table;
            
            SqlDataAdapter sq = new SqlDataAdapter(q, conn);
            DataTable dt = new DataTable();
            sq.Fill(dt);

            int norut = 0;
            string kode = "";

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
            return (kode);
        }

        bool cekCtgName(string name)
        {
            TestConn();
            SqlDataAdapter sq = new SqlDataAdapter("SELECT * FROM dbo.Category where flagCT = 1", conn);
            DataTable dt = new DataTable();
            sq.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["CategoryName"].ToString() == name)
                {
                    conn.Close();
                    return (true);

                }
            }
            conn.Close();
            return (false);
        }

        protected void btn_insert_Click(object sender, EventArgs e)
        {
            if (cekCtgName(tb_name.Text))
            {
                Response.Write("<script>alert('Category name is already exist') </script>");
            }
            else
            {
                TestConn();
                SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Category values('" + getLastIndex("Category", "CategoryID", "CT") + "','" + tb_name.Text + "',1)", conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            TData();
        }

        protected void btn_edit_Click(object sender, EventArgs e)
        {
            TestConn();
            SqlCommand cmd = new SqlCommand("Update dbo.Category set CategoryName = '" + tb_name.Text + "' WHERE CategoryID = '" + lbl_tempid.Text + "'", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            btn_edit.Enabled = false;
            btn_insert.Enabled = true;
            TData();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "editime")
            {
                GridViewRow grv = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                int index = grv.RowIndex;

                lbl_tempid.Text = (GridView1.Rows[index].Cells[0].Text.ToString());
                tb_name.Text = (GridView1.Rows[index].Cells[1].Text.ToString());

                btn_insert.Enabled = false;
                btn_edit.Enabled = true;
            }
        }

        private void Lb2_Click(object sender, EventArgs e) //delete
        {
        }

        private void Lb_Click(object sender, EventArgs e)
        {
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {

                Button lb = new Button();
                lb.Text = "Edit";
                lb.CommandName = "editime";
                e.Row.Cells[2].Controls.Add(lb);
                lb.Click += Lb_Click;

                Button lb2 = new Button();
                lb2.Text = "Delete";
                lb2.CommandName = "delete";
                e.Row.Cells[2].Controls.Add(lb2);
                lb2.Click += Lb2_Click;
                lb2.OnClientClick = "return confirm('Do you want Delete?')";
                
            }
        }

        protected void GridView1_RowDeleting1(object sender, GridViewDeleteEventArgs e)
        {
            string index = (GridView1.Rows[e.RowIndex].Cells[0].Text.ToString());
            TestConn();
            SqlCommand cmd = new SqlCommand("Update dbo.Category set flagCT = 0 WHERE CategoryID = '" + index + "'", conn);
            cmd.ExecuteNonQuery();
            
            conn.Close();
            TData();
        }
    }
}