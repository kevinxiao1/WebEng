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
    public partial class AdminDashboard : System.Web.UI.Page
    {

        String myconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\SIB 17\Semester 5\Fai\Proyek FAI\Tampulan\Proyek\Proyek\App_Data\WebProject.mdf;Integrated Security=True";
        SqlConnection conn;
        protected void Page_Load(object sender, EventArgs e)
        {

            conn = new SqlConnection(myconn);

            if(!IsPostBack)
            {
                isikat();
                isibrand();
                isipromo();
                ddl_active.Items.Add("Yes");
                ddl_active.Items.Add("No");
                Image1.ImageUrl = null;
                
            }



            //if (IsPostBack && FileUpload1.PostedFile != null)
            //{
            //    if (FileUpload1.PostedFile.FileName.Length > 0)
            //    {
            //        string path = Server.MapPath("~/");
            //        FileUpload1.SaveAs(path + FileUpload1.PostedFile.FileName);
            //        Image1.ImageUrl = "~/" + FileUpload1.PostedFile.FileName;
            //    }
            //}






            getdata();


        }


        void getdata()
        {
            conn.Open();

            //FORMAT(1234, 'C', 'fr-FR')
            SqlDataAdapter sq = new SqlDataAdapter("SELECT DISTINCT p.ProductID ,p.Name,c.CategoryName, FORMAT(CONVERT(INT, p.SellPrice),'C','id-ID') AS SellPrice ,b.BrandName,p.Specs,pr.PromoName,p.Status from dbo.Product p,dbo.Brand b,dbo.Promo pr,dbo.Category c where p.CategoryID=c.CategoryID and b.BrandID=p.BrandID and pr.PromoID=p.PromoID", conn);

            // SqlDataAdapter sq = new SqlDataAdapter("SELECT * from dbo.Product", conn);
            DataTable dt = new DataTable();
            sq.Fill(dt);
            dt.Columns.Add("Action");

            GridView1.DataSource = dt;
            GridView1.DataBind();

            conn.Close();
        }
            


        void isikat()
        {
            conn.Open();
            
            SqlDataAdapter sq = new SqlDataAdapter("SELECT * FROM dbo.Category", conn);
            DataTable dt = new DataTable();
            sq.Fill(dt);
            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
               
                 ddl_category.Items.Add(dt.Rows[i]["CategoryID"].ToString() + "-" + dt.Rows[i]["CategoryName"].ToString());
                
               


            }


            conn.Close();
        }


        void isibrand()
        {
            conn.Open();

            SqlDataAdapter sq = new SqlDataAdapter("SELECT * FROM dbo.Brand", conn);
            DataTable dt = new DataTable();
            sq.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                ddl_brand.Items.Add(dt.Rows[i]["BrandID"].ToString() + "-" + dt.Rows[i]["BrandName"].ToString());


            }


            conn.Close();
        }

        void isipromo()
        {
            conn.Open();

            SqlDataAdapter sq = new SqlDataAdapter("SELECT * FROM dbo.Promo", conn);
            DataTable dt = new DataTable();
            sq.Fill(dt);

            ddl_promo.Items.Add("None");
            promo_hidden.Items.Add("PR000");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["PromoID"].ToString() != "PR000")
                {
                    ddl_promo.Items.Add(dt.Rows[i]["PromoName"].ToString() + "-" + dt.Rows[i]["PromoType"].ToString() + "-" + dt.Rows[i]["PromoCount"].ToString());


                    promo_hidden.Items.Add(dt.Rows[i]["PromoID"].ToString());
                }

                   

            }


            conn.Close();
        }

        bool cekProductName(string name)
        {
            conn.Open();

            SqlDataAdapter sq = new SqlDataAdapter("SELECT * FROM dbo.Product", conn);
            DataTable dt = new DataTable();
            sq.Fill(dt);
            

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                if(dt.Rows[i]["Name"].ToString() == name)
                {
                    conn.Close();
                    return (true);
                    
                }
                

            }


            conn.Close();
            return (false);
        }



        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btn_insert_Click(object sender, EventArgs e) //btn insert
        {
            if(cekProductName(tb_name.Text)==true)
            {
                Response.Write("<script>alert('Product name is already exist'); </script>");
            }
            else
            {

                if(Image1.ImageUrl==null)
                {
                    Response.Write("<script>alert('Please Insert Pitcure'); </script>");
                }
                else
                {
                    string kodekat = "";
                    string[] a = ddl_category.SelectedItem.ToString().Split('-');
                    kodekat = a[0];

                    string kodebrand = "";
                    string[] b = ddl_brand.SelectedItem.ToString().Split('-');
                    kodebrand = b[0];


                    string kodepromo = promo_hidden.Items[ddl_promo.SelectedIndex] + "";



                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Product(Name,CategoryID,SellPrice,BrandID,Specs,PromoID,PS,Status) values('" + tb_name.Text + "', '" + kodekat + "' , '" + tb_sellprice.Text + "', '" + kodebrand + "', '" + tb_spec.Text + "', '" + kodepromo + "','','" + ddl_active.SelectedItem.ToString() + "')", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();



                    if(Session["var"]!=null)
                    {
                        List<byte[]> list_byte = new List<byte[]>();
                        list_byte = (List<byte[]>)Session["var"];


                        for (int i = 0; i < list_byte.Count; i++)
                        {
                            conn.Open();
                            cmd = new SqlCommand("insert into Pict (ProductID,PictData) values(@kod,@im) ", conn);
                            cmd.Parameters.AddWithValue("@kod", tb_name.Text);
                            cmd.Parameters.AddWithValue("@im", list_byte[i]);

                            cmd.ExecuteNonQuery();

                            conn.Close();
                        }
                        
                    }








                    Image1.ImageUrl = null;

                    Session["var"] = null;

                    tb_name.Text = "";
                    tb_sellprice.Text = "";
                    tb_spec.Text = "";
                    ddl_active.SelectedIndex = 0;
                    ddl_brand.SelectedIndex = 0;
                    ddl_category.SelectedIndex = 0;
                    ddl_promo.SelectedIndex = 0;
                    promo_hidden.SelectedIndex = 0;


                    getdata();
                }

               
            }
            
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {

                Button lb = new Button();
                lb.Text = "Edit";
                lb.CommandName = "editime";
                e.Row.Cells[8].Controls.Add(lb);
                lb.Click += Lb_Click; ;

                Button lb2 = new Button();
                lb2.Text = "Delete";
                lb2.CommandName = "delete";
                e.Row.Cells[8].Controls.Add(lb2);
                lb2.Click += Lb2_Click ;
                lb2.OnClientClick = "return confirm('Do you want Delete?')";


            }
        }

        private void Lb2_Click(object sender, EventArgs e)
        {

        }

        private void Lb_Click(object sender, EventArgs e) //edit
        {

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "editime")
            {
                GridViewRow grv = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                int index = grv.RowIndex;

                lbl_tempid.Text= (GridView1.Rows[index].Cells[0].Text.ToString());
                tb_name.Text = (GridView1.Rows[index].Cells[1].Text.ToString());


                int ind = 0;
                for (int i = 0; i < ddl_category.Items.Count; i++)
                {
                    if(ddl_category.Items[i].ToString().Contains(GridView1.Rows[index].Cells[2].Text.ToString()))
                    {
                        ind = i;
                    }
                }

                ddl_category.SelectedIndex = ind ;

                tb_sellprice.Text= (GridView1.Rows[index].Cells[3].Text.ToString());

                string b = tb_sellprice.Text.Substring(2);

                string c = "";
                for (int i = 0; i < b.Length; i++)
                {
                    if(b[i]=='.')
                    {

                    }
                    else
                    {
                        c += b[i];
                    }
                }

                string temp = "";
                for (int i = 0; i < c.Length; i++)
                {
                    if(c[i]!=',')
                    {
                        temp += c[i];
                    }
                    else
                    {
                        break;
                    }
                }


                tb_sellprice.Text = int.Parse(temp)+"";


                
                ind = 0;
                for (int i = 0; i < ddl_brand.Items.Count; i++)
                {
                    if(ddl_brand.Items[i].ToString().Contains(GridView1.Rows[index].Cells[4].Text.ToString()))
                    {
                        ind = i;
                    }
                }

                ddl_brand.SelectedIndex = ind;

                tb_spec.Text = GridView1.Rows[index].Cells[5].Text.ToString();


                for (int i = 0; i < ddl_promo.Items.Count; i++)
                {
                    if(ddl_promo.Items[i].ToString().Contains(GridView1.Rows[index].Cells[6].Text.ToString()))
                    {
                        ind = i;
                    }
                }

                ddl_promo.SelectedIndex =ind ;

                ddl_active.Text = GridView1.Rows[index].Cells[7].Text.ToString();



                Session["var"] = null;



                conn.Open();

                SqlDataAdapter sq = new SqlDataAdapter("SELECT * FROM dbo.Pict where ProductID='"+tb_name.Text+"'", conn);
                DataTable dt = new DataTable();
                sq.Fill(dt);
                List<byte[]> list_byte = new List<byte[]>();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    byte[] ba = (byte[])dt.Rows[i]["PictData"];

                    list_byte.Add(ba);


                }

                Session["var"] = list_byte;


                if(list_byte.Count>0)
                {

                    byte[] bc = (byte[])list_byte[0];
                    string strBase64 = Convert.ToBase64String(bc);
                    Image1.ImageUrl = "data:Image/png;base64," + strBase64;
                }
                else
                {
                    Image1.ImageUrl = null;
                }


                conn.Close();


                if (Session["var"] != null)
                {
                    List<byte[]> list_byte2 = new List<byte[]>();

                    list_byte2 = (List<byte[]>)Session["var"];

                    lbl_1.Text = "1";
                    lbl_2.Text = list_byte2.Count + "";
                }



                lbl_idx_foto.Text = "0";
                btn_insert.Enabled = false;
                btn_edit.Enabled = true;
            }
        }

        protected void btn_edit_Click(object sender, EventArgs e)
        {
           
            btn_insert.Enabled = true;
            btn_edit.Enabled = false;

            string kodekat = "";
            string[] a = ddl_category.SelectedItem.ToString().Split('-');
            kodekat = a[0];

            string kodebrand = "";
            string[] b = ddl_brand.SelectedItem.ToString().Split('-');
            kodebrand = b[0];


            string kodepromo = promo_hidden.Items[ddl_promo.SelectedIndex] + "";



            conn.Open();

            SqlCommand cmd = new SqlCommand("Update dbo.Product set Name = '" + tb_name.Text + "' , CategoryID='" + kodekat + "',SellPrice='" + tb_sellprice.Text + "',BrandID='" + kodebrand + "', Specs='" + tb_spec.Text + "', PromoID='" + kodepromo + "',Status='" + ddl_active.SelectedItem.ToString() + "' WHERE ProductID = '" + lbl_tempid.Text + "'", conn);

            cmd.ExecuteNonQuery();


            conn.Close();





            conn.Open();

            cmd = new SqlCommand("DELETE FROM dbo.Pict WHERE ProductID = '" + tb_name.Text + "'", conn);

            cmd.ExecuteNonQuery();

            conn.Close();



            if (Session["var"] != null)
            {
                List<byte[]> list_byte = new List<byte[]>();
                list_byte = (List<byte[]>)Session["var"];


                for (int i = 0; i < list_byte.Count; i++)
                {
                    conn.Open();
                    cmd = new SqlCommand("insert into Pict (ProductID,PictData) values(@kod,@im) ", conn);
                    cmd.Parameters.AddWithValue("@kod", tb_name.Text);
                    cmd.Parameters.AddWithValue("@im", list_byte[i]);

                    cmd.ExecuteNonQuery();

                    conn.Close();
                }




                Image1.ImageUrl = null;

                Session["var"] = null;

                tb_name.Text = "";
                tb_sellprice.Text = "";
                tb_spec.Text = "";
                ddl_active.SelectedIndex = 0;
                ddl_brand.SelectedIndex = 0;
                ddl_category.SelectedIndex = 0;
                ddl_promo.SelectedIndex = 0;
                promo_hidden.SelectedIndex = 0;


            }




            getdata();
            




            
        }
        

        protected void GridView1_RowDeleting1(object sender, GridViewDeleteEventArgs e)
        {
            string index = (GridView1.Rows[e.RowIndex].Cells[0].Text.ToString());


            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM dbo.Product WHERE ProductID = '" + index + "'", conn);

            cmd.ExecuteNonQuery();

            conn.Close();

            getdata();
        }
        

        protected void btn_insertpict_Click(object sender, EventArgs e)
        {
            int lengt = FileUpload1.PostedFile.ContentLength;
            byte[] pic = new byte[lengt];
            FileUpload1.PostedFile.InputStream.Read(pic, 0, lengt);



            
            if (Session["var"] == null)
            {
                List<byte[]> list_byte = new List<byte[]>();
                list_byte.Add(pic);

                Session["var"] = list_byte;


            }
            else
            {
                List<byte[]> list_byte = new List<byte[]>();

                list_byte = (List<byte[]>)Session["var"];

                list_byte.Add(pic);

                Session["var"] = list_byte;

            }


            if (Session["var"] != null)
            {
                List<byte[]> list_byte = new List<byte[]>();

                list_byte = (List<byte[]>)Session["var"];

                lbl_1.Text = "1";
                lbl_2.Text = list_byte.Count + "";
                lbl_idx_foto.Text = "0";
            }


            List<byte[]> list_byte2 = new List<byte[]>();
            list_byte2 = (List<byte[]>)Session["var"];


            byte[] b = (byte[])list_byte2[0];


            string strBase64 = Convert.ToBase64String(b);
            Image1.ImageUrl = "data:Image/png;base64," + strBase64;

        }

        protected void btn_next_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["var"] != null)
                {

                    try
                    {


                        List<byte[]> list_byte2 = new List<byte[]>();
                        list_byte2 = (List<byte[]>)Session["var"];

                        

                        int ix = int.Parse(lbl_idx_foto.Text) + 1;

                        
                        byte[] b = (byte[])list_byte2[ix];

                        lbl_idx_foto.Text = ix + "";
                        lbl_1.Text = int.Parse(lbl_1.Text) + 1+ "";

                        string strBase64 = Convert.ToBase64String(b);
                        Image1.ImageUrl = "data:Image/png;base64," + strBase64;
                    }
                    catch (Exception)
                    {

                    }

                }
            }
            catch (Exception)
            {

                
            }
          
        }

        protected void btn_previous_Click(object sender, EventArgs e)
        {
            if (Session["var"] != null)
            {
                if(int.Parse(lbl_1.Text)<=1)
                {

                }
                else
                {
                    try
                    {
                        List<byte[]> list_byte2 = new List<byte[]>();
                        list_byte2 = (List<byte[]>)Session["var"];


                        int ix = int.Parse(lbl_idx_foto.Text) - 1;

                        byte[] b = (byte[])list_byte2[ix];

                        lbl_idx_foto.Text = ix + "";
                        lbl_1.Text = int.Parse(lbl_1.Text) - 1 + "";


                        string strBase64 = Convert.ToBase64String(b);
                        Image1.ImageUrl = "data:Image/png;base64," + strBase64;
                    }
                    catch (Exception)
                    {

                    }
                }

                

            }
        }

        protected void Button1_Click(object sender, EventArgs e) //hps foto
        {
            if (Session["var"] != null)
            {

                try
                {
                    List<byte[]> list_byte2 = new List<byte[]>();
                    list_byte2 = (List<byte[]>)Session["var"];

                    for (int i = 0; i < list_byte2.Count(); i++)
                    {
                        if(i==int.Parse(lbl_idx_foto.Text))
                        {
                            list_byte2.RemoveAt(i);
                        }
                    }

                    Session["var"] = list_byte2;






                    if (Session["var"] != null)
                    {
                        List<byte[]> list_byte = new List<byte[]>();

                        list_byte = (List<byte[]>)Session["var"];

                        lbl_1.Text = "1";
                        lbl_2.Text = list_byte.Count + "";
                        lbl_idx_foto.Text = "0";



                        list_byte2 = new List<byte[]>();
                        list_byte2 = (List<byte[]>)Session["var"];


                        byte[] b = (byte[])list_byte2[0];


                        string strBase64 = Convert.ToBase64String(b);
                        Image1.ImageUrl = "data:Image/png;base64," + strBase64;


                    }
                    

                }
                catch (Exception)
                {
                    Image1.ImageUrl = null;
                    Session["var"] = null;

                    lbl_1.Text = "0";
                    lbl_2.Text = "0";
                }

            }
        }
    }
}