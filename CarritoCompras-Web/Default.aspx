<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CarritoCompras_Web.Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        // Función para guardar la posición del scroll en sessionStorage
        function guardarPosicionScroll() {
            var scrollY = window.scrollY || document.documentElement.scrollTop;
            sessionStorage.setItem("scrollPosition", scrollY);
        }

        // Función para restaurar la posición del scroll desde sessionStorage
        function restaurarPosicionScroll() {
            var scrollY = parseInt(sessionStorage.getItem("scrollPosition"));
            if (!isNaN(scrollY)) {
                window.scrollTo(0, scrollY);
            }
        }

        // Registra un evento antes de la recarga de la página para guardar la posición del scroll
        window.onbeforeunload = function () {
            guardarPosicionScroll();
        }

        // Registra un evento después de cargar la página para restaurar la posición del scroll
        window.onload = function () {
            restaurarPosicionScroll();
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .section {
          margin: 20px;
        }

        .hero-content {
          background: linear-gradient(rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5)), url('/Resources/pexels-jéshoots-238118.jpg');
          background-size: cover;
          background-position: center;
          color: #fff;
          text-align: center;
          padding: 100px 0;
        }

        .hero-content h1 {
          font-size: 36px;
          font-weight: bold;
        }

        .btn-primary {
          background-color: #007BFF;
          color: #fff;
          padding: 10px 20px;
          border: none;
          transition: background-color 0.3s;
        }

        .btn-primary:hover {
          background-color: #0056b3;
        }

        .bi-cart {
          font-size: 24px;
          margin-right: 10px;
        }

        .filter-container {
          background-color: #f5f5f5;
          padding: 20px;
          border-radius: 5px;
        }

        .brand-filter h6, .category-filter h6 {
          font-size: 18px;
          font-weight: bold;
          margin-top: 10px;
        }

        input[type="checkbox"] {
          margin: 10px 0;
        }

        .items {
          display: flex;
          flex-wrap: wrap;
          justify-content: space-between;
        }

        .card {
          background-color: #fff;
          border-radius: 5px;
          box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
          transition: box-shadow 0.3s;
        }

        .card:hover {
          box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
        }

        .card-title {
          font-size: 20px;
          font-weight: bold;
        }

        .card-text {
          font-size: 14px;
        }

        .btn-primary {
          background-color: #007BFF;
          color: #fff;
          padding: 8px 16px;
          text-decoration: none;
          display: inline-block;
          border-radius: 4px;
          transition: background-color 0.3s;
        }

        .btn-primary:hover {
          background-color: #0056b3;
        }

        .cart-count {
            background-color: #ff6347; 
            color: #fff; 
            border-radius: 50%; 
            padding: 5px 10px; 
            font-size: 16px; 
        }

        .cart-button {
            display: inline-block;
            padding: 10px 20px; 
        }

        
        .cart-button i {
            font-size: 24px; 
            margin-right: 5px; 
        }

    </style>
    <div class="hero-content wv-100 text-center h-50 d-flex justify-content-start align-items-center" style="background-image: url('/Resources/pexels-jéshoots-238118.jpg'); background-size: cover;">
        <h1 class="ms-5">Catálogo de productos</h1>
    </div>

    <div class="d-flex justify-content-end m-3">
        <asp:LinkButton ID="PurchaseButton" runat="server" OnClick="PurchaseButton_Click" CssClass="btn btn-primary cart-button">
            <asp:Label ID="CartCountLabel" runat="server" CssClass="bi bi-cart"></asp:Label>
            <asp:Label runat="server"><%# itemsToCart %></asp:Label>
        </asp:LinkButton>
    </div>

    <section>

        <div class="row">
            <div class="col-3">

                <div class="justify-content-center">
                    <div class="filter-container m-5">
                        <div class="title text-center">
                            <h4>Categorías</h4>
                            <div class="filter-container">
                                <h6 class="mt-4">Buscar</h6>
                                <asp:TextBox ID="txtFilterByName" runat="server" OnTextChanged="txtFilterByName_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="brand-filter">
                            <h6>Marca</h6>
                            <asp:Repeater ID="rptBrands" runat="server">
                                <ItemTemplate>
                                 <div>
                                    <asp:CheckBox runat="server" Text='<%# Eval("Descripcion") %>' 
                                        ID="CheckBoxBrands" AutoPostBack="true" 
                                        OnCheckedChanged="CheckBoxBrands_CheckedChanged" 
                                        CommandArgument='<%# Eval("Id") %>'
                                        />
                                </div>
                            </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <div class="category-filter">
                            <h6 class="mt-4">Categoría</h6>
                                <asp:Repeater ID="rptCategoria" runat="server">
                                    <ItemTemplate>
                                        <div>
                                            <asp:CheckBox  runat="server" Text='<%# Eval("Descripcion") %>' CssClass="flex-row; p-1" />
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-9">

                <div class="container d-flex flex-column justify-content-center gap-2 col-10">
                    <div class="sorts">
                        <asp:DropDownList ID="SortOptionsDropDown" runat="server" AutoPostBack="true" OnSelectedIndexChanged="SortOptionsDropDown_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="items">
                        <asp:Repeater ID="rptArticulos" runat="server" OnItemDataBound="rptArticulos_ItemDataBound">
                            <ItemTemplate>
                                <div class="col-sm-12 col-lg-5">
                                    <div class="card mb-4 w-100">

                                        <div class="card-body text-center">
                                            <asp:Label ID="IdArticulo" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                            <asp:Image ID="imgArticulo" runat="server" CssClass="card-img-top w-50" />
                                    
                                            <h5 class="card-title mt-3"><%# Eval("Nombre") %></h5>
                                            <p class="card-text"><%# Eval("Descripcion") %></p>
                                            <p class="card-text"><%# String.Format("{0:C}", Eval("Precio")) %></p>
                                            <asp:LinkButton ID="button" runat="server" CssClass="btn btn-primary" CommandArgument='<%# Eval("Id") %>' CommandName="ArticuloId" OnClick="AddToCart_Click">
                                                <i class="bi bi-cart-plus-fill"></i>
                                            </asp:LinkButton>
                                            <a href="DetalleArticulo.aspx?id=<%#Eval("Id")%>" class="btn btn-primary"><i class="bi bi-eye-fill"></i></a>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>

                    </div>

                </div>
            </div>
        </div>
        
    </section>

</asp:Content>
