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
    public partial class ShoppingCart : System.Web.UI.Page
    {

        //String myconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\SIB 17\Semester 5\Fai\Proyek FAI Github\WebEng\Hansul\Proyek\Proyek\App_Data\WebProject.mdf;Integrated Security=True";//Punya Adriel
       // string myconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\WebEng\Hansul\Proyek\Proyek\App_Data\WebProject.mdf;Integrated Security=True";//punya Hansel
        String myconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='D:\SIB\Semester 5\Web Engineering\WebEng\Hansul\Proyek\Proyek\App_Data\WebProject.mdf';Integrated Security=True";//punya Johannes
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

        string ConvertHarga(string hrg)
        {
            string temp = "";
            int co = 0;
            for (int i = hrg.Length-1; i >= 0; i--)
            {
                co++;

                if((co%3==0) && (i>0))
                {
                    temp += hrg[i]+ ",";
                }
                else
                {

                    temp += hrg[i];
                }

            }


            string temp2 = "";
            for (int i = temp.Length-1; i >= 0; i--)
            {
                temp2 += temp[i];
            }

            return temp2;
        }


        //idguest
        public void SignOut()
        {
            Session["siapa"] = null;
            Session["siapaUsername"] = null;
            Response.Redirect("Home.aspx");
        }
        protected void Page_Load(object sender, EventArgs e)
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
            if (Session["siapa"]!=null)
            {
                conn = new SqlConnection(myconn);

                string username = (string)Session["siapaUsername"];
                string temp = "";


                TestConn();

                SqlDataAdapter sq = new SqlDataAdapter("SELECT * FROM dbo.CartUser", conn);
                DataTable dt = new DataTable();
                sq.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["Username"].ToString() == username)
                    {

                        SqlDataAdapter sq2 = new SqlDataAdapter("SELECT * FROM dbo.Product where ProductID='" + dt.Rows[i]["ProductID"] + "'", conn);
                        DataTable dt2 = new DataTable();
                        sq2.Fill(dt2);

                        for (int y = 0; y < dt2.Rows.Count; y++)
                        {
                            string tes = "";
                            string tes2 = "";
                            string gbr = "SELECT ProductID, PictData FROM Pict where ProductID = '" + dt2.Rows[y]["Name"] + "'";
                            SqlDataAdapter sg = new SqlDataAdapter(gbr, conn);
                            DataTable dt3 = new DataTable();
                            sg.Fill(dt3);
                            for (int j = 0; j < dt3.Rows.Count; j++)
                            {
                                if (dt3.Rows[j]["ProductID"].ToString() == tes)
                                {

                                }
                                else
                                {
                                    byte[] ba = (byte[])dt3.Rows[j]["PictData"];
                                    string strBase64 = Convert.ToBase64String(ba);
                                    tes2 = strBase64;
                                    break;

                                    //list_string.Add(strBase64);
                                }




                            }

                            int totharga = int.Parse(dt2.Rows[y]["SellPrice"].ToString()) * int.Parse(dt.Rows[i]["Qty"].ToString());

                        //    temp += "<tr><td> <div class='media'><div class='d-flex'> <img src=data:image/png;base64," + tes2 + " width='80' height='80'>  </div>  <div class='media-body'>   <p>" + dt2.Rows[y]["Name"] + "</p>  </div> </div>  </td> <td> <h5>Rp. " + ConvertHarga(dt2.Rows[y]["SellPrice"].ToString()) + "</h5> </td>  <td> <div class='product_count'><input type='number' onchange='Berubah()' min='0' value='" + ConvertHarga(dt.Rows[i]["Qty"].ToString())+"'></div> </td> <td> <h5>IDR "+ConvertHarga(totharga.ToString())+"</h5>   </td>  </tr>";
                        //}


                        temp += "<tr><td> <div class='media'><div class='d-flex'> <img src=data:image/png;base64," + tes2 + " width='80' height='80'>  </div>  <div class='media-body'>   <p>" + dt2.Rows[y]["Name"] + "</p>  </div> </div>  </td> <td> <h5 id='kiri" + dt2.Rows[y]["ProductID"] + "'>Rp. " + ConvertHarga(dt2.Rows[y]["SellPrice"].ToString()) + "</h5> </td>  <td> <div class='product_count'><input id='" + dt2.Rows[y]["ProductID"] + "' type='number' onchange='Berubah(this.id,this.value)' value='" + ConvertHarga(dt.Rows[i]["Qty"].ToString()) + "' min='0'></div> </td> <td> <h5 id='kanan" + dt2.Rows[y]["ProductID"] + "'> IDR. " + ConvertHarga(totharga.ToString()) + "</h5> </td>  </tr>";
                    }

                    //   <% --< tr >
                    //  < td >
                    //    < div class="media">
                    //      <div class="d-flex">
                    //        <img src = "img/product/single-product/cart-1.jpg" alt="" />
                    //      </div>
                    //      <div class="media-body">
                    //        <p>Minimalistic shop for multipurpose use</p>
                    //      </div>
                    //    </div>
                    //  </td>
                    //  <td>
                    //    <h5>$360.00</h5>
                    //  </td>
                    //  <td>
                    //    <div class="product_count">
                    //      <span class="input-number-decrement"> <i class="ti-angle-down"></i></span>
                    //      <input class="input-number" type="text" value="1" min="0" max="10">
                    //      <span class="input-number-increment"> <i class="ti-angle-up"></i></span>
                    //    </div>
                    //  </td>
                    //  <td>
                    //    <h5>$720.00</h5>
                    //  </td>
                    //</tr>--%>


                }
                    conn.Close();


                    lbIsi.Text = temp;

                }
            }
            else
            {
                try
                {
                    // int temp = int.Parse(Session["idguest"].ToString());
                    conn = new SqlConnection(myconn);
                    TestConn();
                    string temp = "";
                    SqlDataAdapter sq = new SqlDataAdapter("SELECT * FROM dbo.GuestCart", conn);
                    DataTable dt = new DataTable();
                    sq.Fill(dt);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (int.Parse(dt.Rows[i]["Id"].ToString()) >= int.Parse(Session["idguest"].ToString()))
                        {

                            SqlDataAdapter sq2 = new SqlDataAdapter("SELECT * FROM dbo.Product where ProductID='" + dt.Rows[i]["ProductID"] + "'", conn);
                            DataTable dt2 = new DataTable();
                            sq2.Fill(dt2);

                            for (int y = 0; y < dt2.Rows.Count; y++)
                            {
                                string tes = "";
                                string tes2 = "";
                                string gbr = "SELECT ProductID, PictData FROM Pict where ProductID = '" + dt2.Rows[y]["Name"] + "'";
                                SqlDataAdapter sg = new SqlDataAdapter(gbr, conn);
                                DataTable dt3 = new DataTable();
                                sg.Fill(dt3);
                                for (int j = 0; j < dt3.Rows.Count; j++)
                                {
                                    if (dt3.Rows[j]["ProductID"].ToString() == tes)
                                    {

                                    }
                                    else
                                    {
                                        byte[] ba = (byte[])dt3.Rows[j]["PictData"];
                                        string strBase64 = Convert.ToBase64String(ba);
                                        tes2 = strBase64;
                                        break;

                                        //list_string.Add(strBase64);
                                    }




                                }
                                int totharga = int.Parse(dt2.Rows[y]["SellPrice"].ToString()) * int.Parse(dt.Rows[i]["Qty"].ToString());

                                temp += "<tr><td> <div class='media'><div class='d-flex'> <img src=data:image/png;base64," + tes2 + " width='80' height='80'>  </div>  <div class='media-body'>   <p>" + dt2.Rows[y]["Name"] + "</p>  </div> </div>  </td> <td> <h5 id='kiri" + dt2.Rows[y]["ProductID"] + "'>Rp. " + ConvertHarga( dt2.Rows[y]["SellPrice"].ToString() )+ "</h5> </td>  <td> <div class='product_count'><input id='" + dt2.Rows[y]["ProductID"] + "' type='number' onchange='Berubah(this.id,this.value)' value='" + ConvertHarga(dt.Rows[i]["Qty"].ToString()) + "' min='0'></div> </td> <td> <h5 id='kanan" + dt2.Rows[y]["ProductID"] + "'> IDR. " + ConvertHarga(totharga.ToString() )+ "</h5> </td>  </tr>";
                            }
                            

                            //   <% --< tr >
                            //  < td >
                            //    < div class="media">
                            //      <div class="d-flex">
                            //        <img src = "img/product/single-product/cart-1.jpg" alt="" />
                            //      </div>
                            //      <div class="media-body">
                            //        <p>Minimalistic shop for multipurpose use</p>
                            //      </div>
                            //    </div>
                            //  </td>
                            //  <td>
                            //    <h5>$360.00</h5>
                            //  </td>
                            //  <td>
                            //    <div class="product_count">
                            //      <span class="input-number-decrement"> <i class="ti-angle-down"></i></span>
                            //      <input class="input-number" type="text" value="1" min="0" max="10">
                            //      <span class="input-number-increment"> <i class="ti-angle-up"></i></span>
                            //    </div>
                            //  </td>
                            //  <td>
                            //    <h5>$720.00</h5>
                            //  </td>
                            //</tr>--%>


                        }
                        conn.Close();


                        lbIsi.Text = temp;

                    }
                }
                catch (Exception)
                {
                    
                }
               

            }


            
        }
        protected void btnSearch(object sender, EventArgs e)
        {
            Response.Redirect("ProductCategory.aspx?name=" + search_input.Value + "");
            //Response.Write("<script> alert('" + search_input.Value + "')</script>");
            //btn_search.Text = "as";
            //  Response.Write("asa");
        }
    }
}