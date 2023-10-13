<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="DetalleArticulo.aspx.cs" Inherits="CarritoCompras_Web.DetalleArticulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        .container {
            display: flex;
            flex-direction: row;
            align-items: flex-start;
            justify-content: space-between;
        }

        .info-container {
            display: flex;
            flex-direction: column;
            align-items: flex-start;
        }

            .info-container div {
                display: flex;
                justify-content: flex-start;
                margin-bottom: 5px;
            }

        .card {
            width: 50%;
            display: flex;
            flex-direction: column;
            justify-content: center;
        }

            .card img {
                max-width: 200%;
                height: auto;
            }

        .centered-container {
            display: flex;
            flex-direction: column;
            align-items: center;
            justify-content: center;
            height: 100vh; /* Establece la altura al 100% del viewport */
            text-align: center; /* Opcional, para centrar el texto dentro de los elementos */
        }

            .centered-container div {
                margin: 10px; /* Espacio entre los elementos div */
            }
    </style>

    <div class="container" id="divPrincipal" runat="server">
        <div id="imagenContainer" class="card" style="width: 18rem;">
            <asp:Image ID="imgArticulo" runat="server" CssClass="card-img-top" AlternateText="Artículo" />
            <div class="card-body">
                <h5 class="card-title" runat="server" id="lblTitulo"></h5>
                <p class="card-text" runat="server" id="P1"></p>
                <div class="text-center">
                    <asp:Button Text="Prev" runat="server" ID="btnPrev" class="btn btn-primary" OnClick="btnPrev_Click" />
                    <asp:Button Text="Next" runat="server" ID="btnNext" class="btn btn-primary" OnClick="btnNext_Click" />
                </div>
            </div>
        </div>
        <div class="info-container">
            <div>
                <asp:Label Text="Codigo:  " runat="server" />
                <asp:Label Text=" " runat="server" ID="lblCode" />
            </div>
            <div>
                <asp:Label Text="Nombre:  " runat="server" />
                <asp:Label Text=" " runat="server" ID="lblNombre" />
            </div>
            <div>
                <asp:Label Text="Descripción:  " runat="server" />
                <asp:Label Text=" " runat="server" ID="lblDescripcion" />
            </div>
            <div>
                <asp:Label Text="Marca:  " runat="server" />
                <asp:Label Text=" " runat="server" ID="lblMarca" />
            </div>
            <div>
                <asp:Label Text="Categoria:  " runat="server" />
                <asp:Label Text=" " runat="server" ID="lblCategoria" />
            </div>
            <div>
                <asp:Label Text="Precio: $ " runat="server" />
                <asp:Label Text=" " runat="server" ID="lblPrecio" />
            </div>
        </div>
    </div>
    <div class="centered-container">
        <div>
            <asp:Label ID="lblError" runat="server" Visible="false" CssClass="alert alert-warning"></asp:Label>
        </div>
        <div>
            <asp:Button Text="Volver al Inicio" runat="server" ID="btnVolver" class="btn btn-primary" OnClick="btnVolver_Click" />
        </div>
    </div>


</asp:Content>
