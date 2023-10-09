<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CarritoCompras_Web.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="d-flex justify-content-end m-3">
        <button class="btn btn-primary"> 
            <a class="btn btn-primary" data-bs-toggle="offcanvas" href="#offcanvasExample" role="button" aria-controls="offcanvasExample">
                <i class="bi bi-cart"></i>
            </a>

        </button>
    </div>
    <div class="container d-flex justify-content-center flex-wrap p-4 gap-2">
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
                            <a href="#" class="btn btn-primary">Agregar al carrito</a>
                            <a href="DetalleArticulo.aspx?id=<%#Eval("Id")%>" class="btn btn-primary">Ver detalle</a>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

    </div>

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
