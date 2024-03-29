﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Proyek.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <link rel="icon" href="img/favicon.png">
    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="css/bootstrap.min.css">
    <!-- animate CSS -->
    <link rel="stylesheet" href="css/animate.css">
    <!-- owl carousel CSS -->
    <link rel="stylesheet" href="css/owl.carousel.min.css">
    <!-- font awesome CSS -->
    <link rel="stylesheet" href="css/all.css">
    <!-- flaticon CSS -->
    <link rel="stylesheet" href="css/flaticon.css">
    <link rel="stylesheet" href="css/themify-icons.css">
    <!-- font awesome CSS -->
    <link rel="stylesheet" href="css/magnific-popup.css">
    <!-- swiper CSS -->
    <link rel="stylesheet" href="css/slick.css">
    <!-- style CSS -->
    <link rel="stylesheet" href="css/style.css">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

        <!--::header part start::-->
    <header class="main_menu home_menu">
        <div class="container">
            <div class="row align-items-center">
                <div class="col-lg-12">
                    <nav class="navbar navbar-expand-lg navbar-light">
                        <a class="navbar-brand" href="home.aspx"> <img src="img/Logo/logo.png" alt=""/> </a>
                        <button class="navbar-toggler" type="button" data-toggle="collapse"
                            data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent"
                            aria-expanded="false" aria-label="Toggle navigation">
                            <span class="menu_icon"><i class="fas fa-bars"></i></span>
                        </button>

                        <div class="collapse navbar-collapse main-menu-item" id="navbarSupportedContent">
                            <ul class="navbar-nav">
                                <li class="nav-item">
                                    <a class="nav-link" href="Home.aspx">Home</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" href="ProductCategory.aspx?page=1">Product</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" href="contact.html">About Us</a>
                                </li>
                            </ul>
                        </div>
                        <div class="header_icon d-flex">
                            <a id="search_1" href="javascript:void(0)"><i class="ti-search"></i></a>
                            <%--<div class="dropdown cart" href="ShoppingCart.aspx">
                                <a class="dropdown-toggle" href="ShoppingCart.aspx" id="navbarDropdown3" role="button"
                                    data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                    <i class="fas fa-cart-plus" href="ShoppingCart.aspx"></i>
                                </a>
                                
                            </div>--%>
                            <div class="dropdown cart" href="ShoppingCart.aspx">
                                <a class="btn-3" href="ShoppingCart.aspx">
                               <%-- <a class="dropdown-toggle" href="ShoppingCart.aspx" id="navbarDropdown3" role="button"
                                    data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                   
                                </a>--%>
                            <%--     <div class="dropdown-menu" aria-labelledby="navbarDropdown">
                                    <div class="single_product">
                                        
                                    </div>
                                </div> --%>
                                 <i class="fas fa-cart-plus"></i>
                            </a>
                                
                            </div>
                            <div class="dropdown">
                                <a class="dropdown-toggle" href="#" id="navbarDropdown3" role="button"
                                    data-toggle="dropdown">
                                    <asp:Label ID="lbWesLogin" runat="server" Text="Label"></asp:Label>
                                </a>
                                 <div class="dropdown-menu" aria-labelledby="navbarDropdown">
                                    <div class="single_product">
                                        <asp:Label ID="lbTokek" runat="server" Text="Label"></asp:Label>
                                        <%--<a class="dropdown-item" href="ProductCategory.aspx"> shop category</a>
                                        <a class="dropdown-item" href="single-product.html">product details</a>--%>
                                    </div>
                                </div> 
                                
                            </div>
                        </div>
                        <%--<div>
                            <li class="nav-item dropdown">
                                    <a class="nav-link dropdown-toggle" href="blog.html" id="navbarDropdown_1"
                                        role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                        Shop
                                    </a>
                                   
                                </li>
                        </div>--%>
                    </nav>
                </div>
            </div>
        </div>
        <div class="search_input" id="search_input_box">
            <div class="container ">
                <div class="d-flex justify-content-between search-inner" >
                    <input runat="server" type="text" class="form-control" id="search_input" placeholder="Search Here" />
                    <asp:Button runat="server" type="submit" OnClick="btnSearch" class="btn"></asp:Button>
                    <span class="ti-close" id="close_search" title="Close Search"></span>
                </div>
            </div>
        </div>
    </header>
    <!-- Header part end-->

    <!-- banner part start-->
    <section class="banner_part">
        <div class="container">
            <div class="row align-items-center">
                <div class="col-lg-12">
                    <div class="banner_slider owl-carousel">
                        <div class="single_banner_slider">
                            <div class="row">
                                <div class="col-lg-5 col-md-8">
                                    <div class="banner_text">
                                        <div class="banner_text_iner">
                                            <h1>New sensor on budget</h1>
                                            <p>Incididunt ut labore et dolore magna aliqua quis ipsum
                                                suspendisse ultrices gravida. Risus commodo viverra</p>
                                            <a href="?1" class="btn_2">buy now</a>
                                        </div>
                                    </div>
                                </div>
                                <div class="banner_img d-none d-lg-block">
                                    <img src="img/banner.png" alt="" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!-- banner part start-->

    <!-- feature_part start-->
    <section class="feature_part padding_top">
        <div class="container">
            <div class="row justify-content-center">
                <div class="col-lg-8">
                    <div class="section_tittle text-center">
                        <h2>Featured Category</h2>
                    </div>
                </div>
            </div>
            <div class="row align-items-center justify-content-between">
                <div class="col-lg-7 col-sm-6">
                    <div class="single_feature_post_text">
<<<<<<< HEAD
                        <h3>Digital Camera</h3>
                        <a href="ProductCategory.aspx?search=Camera" class="feature_btn">EXPLORE NOW <i class="fas fa-play"></i></a>
                        <img src="img/feature/fitur2.png" alt=""/>
=======
                        <h3>Mirrorless</h3>
                        <a href="ProductCategory.aspx?search=Camera" class="feature_btn">EXPLORE NOW <i class="fas fa-play"></i></a>
                        <img src="img/feature/banner1.png" alt=""/>
>>>>>>> 1593c27a191d07307e5ff54ff6553dec1ecde841
                    </div>
                </div>
                <div class="col-lg-5 col-sm-6">
                    <div class="single_feature_post_text">
                        <h3>Action Camera</h3>
<<<<<<< HEAD
                        <a href="ProductCategory.aspx?search=Camera" class="feature_btn">EXPLORE NOW <i class="fas fa-play"></i></a>
                        <img src="img/feature/fitur3.png" alt=""/>
=======
                        <a href="ProductCategory.aspx?search=Camera" class="feature_btn">EXPLORE NOW <i class="fas fa-play"></i></a>
                        <img src="img/feature/banner2.png" alt=""/>
>>>>>>> 1593c27a191d07307e5ff54ff6553dec1ecde841
                    </div>
                </div>
                <div class="col-lg-5 col-sm-6">
                    <div class="single_feature_post_text">
                        <h3>Camera Lens</h3>
                        <a href="ProductCategory.aspx?search=Lensa" class="feature_btn">EXPLORE NOW <i class="fas fa-play"></i></a>
                        <img src="img/feature/fitur1.png" alt=""/>
                    </div>
                </div>
                <div class="col-lg-7 col-sm-6">
                    <div class="single_feature_post_text">
                        <h3>Audio and Accessories</h3>
<<<<<<< HEAD
                        <a href="ProductCategory.aspx?search=Merc" class="feature_btn">EXPLORE NOW <i class="fas fa-play"></i></a>
                        <img src="img/feature/fitur4.png" alt=""/>
=======
                        <a href="ProductCategory.aspx?search=Merc" class="feature_btn">EXPLORE NOW <i class="fas fa-play"></i></a>
                        <img src="img/feature/banner4.png" alt=""/>
>>>>>>> 1593c27a191d07307e5ff54ff6553dec1ecde841
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!-- upcoming_event part start-->

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
    </form>
    
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
    <!-- custom js -->
    <script src="js/custom.js"></script>
</body>
</html>
