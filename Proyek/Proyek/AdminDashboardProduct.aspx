﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminDashboardProduct.aspx.cs" Inherits="Proyek.AdminDashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- Required meta tags -->
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <title>aranoz</title>
    <link rel="icon" href="img/favicon.png">
    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="css/bootstrap.min.css">
    <!-- animate CSS -->
    <link rel="stylesheet" href="css/animate.css">
    <!-- owl carousel CSS -->
    <link rel="stylesheet" href="css/owl.carousel.min.css">
    <!-- nice select CSS -->
    <link rel="stylesheet" href="css/nice-select.css">
    <!-- font awesome CSS -->
    <link rel="stylesheet" href="css/all.css">
    <!-- flaticon CSS -->
    <link rel="stylesheet" href="css/flaticon.css">
    <link rel="stylesheet" href="css/themify-icons.css">
    <!-- font awesome CSS -->
    <link rel="stylesheet" href="css/magnific-popup.css">
    <!-- swiper CSS -->
    <link rel="stylesheet" href="css/slick.css">
    <link rel="stylesheet" href="css/price_rangs.css">
    <!-- style CSS -->
    <link rel="stylesheet" href="css/style.css">
</head>
<body>
    <form id="form1" runat="server">

    <!--================Category Product Area =================-->
    <section class="cat_product_area section_padding">
        <div class="container">
            <div class="row">
                <div class="col-lg-3">
                    <div class="left_sidebar_area">
                        <aside class="left_widgets p_filter_widgets">
                            <div class="l_w_title">
                                <h3>Master</h3>
                            </div>
                            <div class="widgets_inner">
                                <ul class="list">
                                    <li>
                                        <a href="AdminDashboardProduct.aspx">Product</a>
                                    </li>
                                    <li>
                                        <a href="#">Category</a>
                                    </li>
                                    <li>
                                        <a href="AdminDashboardBrand.aspx">Brand</a>
                                    </li>
                                    <li>
                                        <a href="#">Customer</a>
                                    </li>
                                    <li>
                                        <a href="AdminDashboardPromo.aspx">Promo</a>
                                    </li>
                                    <li>
                                        <a href="Login.aspx">Log Out</a>
                                    </li>
                                </ul>
                            </div>
                        </aside>
                    </div>
                </div>
                <div class="col-lg-9">
                    <div>

                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <br />
                        <br />
                        <br />
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <br />
                                Master Product<br />
                                <asp:Label ID="lbl_tempid" runat="server" Visible="False"></asp:Label>
                                <br />
                                <asp:Label ID="Label1" runat="server" Text="Name: "></asp:Label>
                                <br />
                                <asp:TextBox ID="tb_name" runat="server"></asp:TextBox>
                                <br />
                                &nbsp;<br />
                                <asp:Label ID="Label2" runat="server" Text="Category: "></asp:Label>
                                <br />
                                <asp:DropDownList ID="ddl_category" runat="server">
                                </asp:DropDownList>
                                <br />
                                <br />
                                <asp:Label ID="Label3" runat="server" Text="Sell Price: "></asp:Label>
                                <br />
                                <asp:TextBox ID="tb_sellprice" runat="server"></asp:TextBox>
                                <br />
                                <asp:Label ID="Label4" runat="server" Text="Brand "></asp:Label>
                                <br />
                                <asp:DropDownList ID="ddl_brand" runat="server">
                                </asp:DropDownList>
                                <br />
                                <asp:Label ID="Label5" runat="server" Text="Specs "></asp:Label>
                                <br />
                                <asp:TextBox ID="tb_spec" runat="server" TextMode="MultiLine"></asp:TextBox>
                                <br />
                                <asp:Label ID="Label6" runat="server" Text="Promo "></asp:Label>
                                <br />
                                <asp:DropDownList ID="ddl_promo" runat="server">
                                </asp:DropDownList>
                                <asp:DropDownList ID="promo_hidden" runat="server" Visible="False">
                                </asp:DropDownList>
                                <br />
                                <br />
                                <asp:Label ID="Label7" runat="server" Text="Active "></asp:Label>
                                <br />
                                <asp:DropDownList ID="ddl_active" runat="server">
                                </asp:DropDownList>
                                <br />
                                <br />
                                <br />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        Gambar<br />
                        <br />
                        File Foto dibawah 1mb ya..<br />
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                        <asp:Button ID="btn_insertpict" runat="server" OnClick="btn_insertpict_Click" Text="Ok" />
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <br />
                                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Hapus Foto " />
                                <br />
                                <asp:Image ID="Image1" runat="server" Height="121px" Width="149px" />
                                <asp:Label ID="lbl_idx_foto" runat="server" Text="0"></asp:Label>
                                <br />
                                <br />
                                <asp:Label ID="lbl_1" runat="server" Text="0"></asp:Label>
                                &nbsp;to
                                <asp:Label ID="lbl_2" runat="server" Text="0"></asp:Label>
                                <br />
                                <br />
                                <br />
                                <asp:Button ID="btn_next" runat="server" OnClick="btn_next_Click" Text="Next" />
                                <asp:Button ID="btn_previous" runat="server" OnClick="btn_previous_Click" Text="Previous" />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <asp:Button ID="btn_insert" runat="server" OnClick="btn_insert_Click" Text="Insert" />
                                <asp:Button ID="btn_edit" runat="server" Enabled="False" OnClick="btn_edit_Click" Text="Edit" />
                                <br />
                                <br />
                                <br />
                                <br />
                                <asp:GridView ID="GridView1" runat="server" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting1">
                                </asp:GridView>
<br />
<br />
<br />
<br />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br />
                        <br />
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!--================End Category Product Area =================-->


    <!--::footer_part start::-->
    <footer class="footer_part">
        <div class="copyright_part">
            <div class="container">
                <div class="row">
                    <div class="col-lg-8">
                        <div class="copyright_text">
                            <P><!-- Link back to Colorlib can't be removed. Template is licensed under CC BY 3.0. -->
Copyright &copy;<script>document.write(new Date().getFullYear());</script> All rights reserved | This template is made with <i class="ti-heart" aria-hidden="true"></i> by <a href="https://colorlib.com" target="_blank">Colorlib</a>
<!-- Link back to Colorlib can't be removed. Template is licensed under CC BY 3.0. --></P>
                        </div>
                    </div>
                    <div class="col-lg-4">
                        <div class="footer_icon social_icon">
                            <ul class="list-unstyled">
                                <li><a href="#" class="single_social_icon"><i class="fab fa-facebook-f"></i></a></li>
                                <li><a href="#" class="single_social_icon"><i class="fab fa-twitter"></i></a></li>
                                <li><a href="#" class="single_social_icon"><i class="fas fa-globe"></i></a></li>
                                <li><a href="#" class="single_social_icon"><i class="fab fa-behance"></i></a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </footer>
    <!--::footer_part end::-->

    <!-- jquery plugins here-->
    <script src="js/jquery-1.12.1.min.js"></script>
    <!-- popper js -->
    <script src="js/popper.min.js"></script>
    <!-- bootstrap js -->
    <script src="js/bootstrap.min.js"></script>
    <!-- easing js -->
    <script src="js/jquery.magnific-popup.js"></script>
    <!-- swiper js -->
    <script src="js/swiper.min.js"></script>
    <!-- swiper js -->
    <script src="js/masonry.pkgd.js"></script>
    <!-- particles js -->
    <script src="js/owl.carousel.min.js"></script>
    <script src="js/jquery.nice-select.min.js"></script>
    <!-- slick js -->
    <script src="js/slick.min.js"></script>
    <script src="js/jquery.counterup.min.js"></script>
    <script src="js/waypoints.min.js"></script>
    <script src="js/contact.js"></script>
    <script src="js/jquery.ajaxchimp.min.js"></script>
    <script src="js/jquery.form.js"></script>
    <script src="js/jquery.validate.min.js"></script>
    <script src="js/mail-script.js"></script>
    <script src="js/stellar.js"></script>
    <script src="js/price_rangs.js"></script>
    <!-- custom js -->
    <script src="js/custom.js"></script>
    </form>
</body>
</html>
