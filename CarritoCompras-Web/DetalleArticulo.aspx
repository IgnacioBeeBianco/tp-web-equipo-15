<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="DetalleArticulo.aspx.cs" Inherits="CarritoCompras_Web.DetalleArticulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div>
        <asp:Label Text="" runat="server" ID="lblID" />
        <asp:Label Text="" runat="server" ID="lblCode" />
        <asp:Label Text="" runat="server" ID="lblNombre" />
        <asp:Label Text="" runat="server" ID="lblDescripcion" />
        <asp:Label Text="" runat="server" ID="lblMarca" />
        <asp:Label Text="" runat="server" ID="lblCategoria" />
        <asp:Label Text="" runat="server" ID="lblPrecio" />
    </div>
    
    <div>
        <div id="imageCarousel" class="carousel slide" data-ride="carousel">

            <ol class="carousel-indicators">
                <li data-target="#imageCarousel" runat="server"></li>
            </ol>


            <div class="carousel-inner text-center">
                <asp:Repeater ID="rptCarousel" runat="server">
                    <ItemTemplate>
                        <div class='<%# Container.ItemIndex == 0 ? "carousel-item active" : "carousel-item" %>'>
                            <img src='<%# Eval("ImagenURL") %>' alt="Imagen de artículo"  width="800" height="600"/>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>


            <a class="carousel-control-prev" href="#imageCarousel" role="button" data-slide="prev">
                <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                <span class="sr-only">Anterior</span>
            </a>
            <a class="carousel-control-next" href="#imageCarousel" role="button" data-slide="next">
                <span class="carousel-control-next-icon" aria-hidden="true"></span>
                <span class="sr-only">Siguiente</span>
            </a>
        </div>


    </div>





    <asp:Label Text="" runat="server" ID="lblError" />

</asp:Content>
