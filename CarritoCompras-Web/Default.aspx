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
            <div class="col-2 justify-content-center">
            <div class="filter-container m-5">
                <div class="title text-center">
                    <h3>Categorías</h3>

                </div>
                <div class="brand-filter">
                <h5>Marca</h5>
                    <asp:Repeater ID="rptBrands" runat="server">
                        <ItemTemplate>
                            <div class="form-check">
                                <asp:CheckBox runat="server" Text='<%# Eval("Descripcion") %>' CssClass="text-start"/>
                            </div>

                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="category-filter">
                    <h5 class="mt-4">Categoría</h5>
                    <div class="d-flex flex-column">
                        <asp:Repeater ID="rptCategoria" runat="server">
                            <ItemTemplate>
                                <div class="form-check">
                                    <asp:CheckBox  runat="server" Text='<%# Eval("Descripcion") %>' CssClass="flex-row; p-1" />

                                </div>
                            </ItemTemplate>
                        </asp:Repeater>

                    </div>


                </div>
                <!-- Agrega aquí tus controles para filtrar por precio -->
            </div>
        </div>
            <div class="container d-flex flex-column justify-content-center p-4 gap-2 col-10">
                <div class="sorts">
                    <asp:DropDownList ID="SortOptionsDropDown" runat="server" AutoPostBack="true" OnSelectedIndexChanged="SortOptionsDropDown_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div class="items d-flex flex-wrap ">
                    <asp:Repeater ID="rptArticulos" runat="server">
                        <HeaderTemplate>
                        
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="col-md-3">
                                <div class="card mb-4" style="width:20rem;">

                                    <div class="card-body text-center">
                                        <asp:Image ID="imgArticulo" runat="server" CssClass="card-img-top" ImageUrl='<%# ((List<Dominio.Imagen>)Eval("ImagenURL")).Count > 0 ? ((List<Dominio.Imagen>)Eval("ImagenURL")).First().ImagenUrl : "~/Resources/OIP.jpg" %>' />
                                    
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
