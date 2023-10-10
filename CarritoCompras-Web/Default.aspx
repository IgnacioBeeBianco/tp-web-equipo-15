<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CarritoCompras_Web.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="hero-content wv-100 text-center h-50 d-flex justify-content-center align-items-center" style="background-image: url('/Resources/pexels-jéshoots-238118.jpg'); background-size: cover;">
        <h1>Catálogo de productos</h1>
    </div>

    <div class="d-flex justify-content-end m-3">
        <button class="btn btn-primary"> 
            <a class="btn btn-primary" data-bs-toggle="offcanvas" href="#offcanvasExample" role="button" aria-controls="offcanvasExample">
                <i class="bi bi-cart"></i>
            </a>

        </button>
    </div>
    <section>
        <div class="row">
            <div class="col-3 justify-content-center">
            <div class="filter-container m-3">
                <h3>Categorías</h3>
                <h5>Marca</h5>
                <div class="form-check">
                    <asp:CheckBox ID="CheckBox1" runat="server" Text="Samsung" CssClass="text-start"/>
                </div>
                <div class="form-check">
                    <asp:CheckBox ID="CheckBox2" runat="server" Text="Motorola" CssClass="text-start"/>
                </div>
                <div class="form-check">
                    <asp:CheckBox ID="CheckBox3" runat="server" Text="Sony" CssClass="text-start"/>
                </div>
                <div class="form-check">
                    <asp:CheckBox ID="CheckBox4" runat="server" Text="Apple" CssClass="text-start"/>
                </div>
                <h5 class="mt-4">Precio</h5>
                <!-- Agrega aquí tus controles para filtrar por precio -->
            </div>
        </div>
            <div class="container d-flex justify-content-center flex-wrap p-4 gap-2 col-8">
                <asp:Repeater ID="rptArticulos" runat="server" OnItemCreated="rptArticulos_ItemCreated">
                    <ItemTemplate>
                        <div class="col-md-4">
                            <div class="card mb-4">

                                <div class="card-body text-center">
                                    <asp:Image ID="imgArticulo" runat="server" CssClass="card-img-top" />
                                    <div class="btn-container mt-3">
                                        <asp:Button ID="btnPrevImage" runat="server" Text="Prev" OnClick="btnPrevImage_Click" CssClass="btn btn-primary"/>
                            
                                        <asp:Button ID="btnNextImage" runat="server" Text="Next" OnClick="btnNextImage_Click" CssClass="btn btn-primary"/>

                                    </div>
                                    <h5 class="card-title mt-3"><%# Eval("Nombre") %></h5>
                                    <p class="card-text"><%# Eval("Descripcion") %></p>
                                    <p class="card-text">$<%# Eval("Precio") %></p>
                                    <asp:Button CssClass="btn btn-primary" runat="server" Text="Agregar al carrito"/>
                                    <a href="DetalleArticulo.aspx?id=<%#Eval("Id")%>" class="btn btn-primary">Ver detalle</a>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

            </div>
        </div>
        
    </section>

    <div class="offcanvas offcanvas-start" tabindex="-1" id="offcanvasExample" aria-labelledby="offcanvasExampleLabel">
      <div class="offcanvas-header">
        <h5 class="offcanvas-title" id="offcanvasExampleLabel">Bienvenido a tu carrito!</h5>
        <button type="button" class="btn-close" data-bs-dismiss="offcanvas" aria-label="Close"></button>
      </div>
      <div class="offcanvas-body">
        <div>
            Carrito
        </div>
        <div class="dropdown mt-3">
          
        </div>
      </div>
    </div>

</asp:Content>
