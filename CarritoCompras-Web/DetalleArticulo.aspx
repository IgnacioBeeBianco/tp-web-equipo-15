<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="DetalleArticulo.aspx.cs" Inherits="CarritoCompras_Web.DetalleArticulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .info-container {
            text-align: left; /* Alinea el contenido al centro izquierdo */
            margin: 0 auto; /* Centra el contenido horizontalmente en el contenedor */
            max-width: 300px; /* Establece un ancho máximo para el contenedor (ajústalo según tus necesidades) */
        }

            .info-container label {
                display: block;
                margin-bottom: 10px; /* Espacio vertical entre etiquetas */
            }

        <style>
        .container {
            text-align: left; /* Alinea el contenido al centro izquierdo */
        }

        .card {
            float: left; /* Hace que los elementos floten a la izquierda */
            margin-right: 20px; /* Espacio horizontal entre elementos */
        }

        .text-center {
            text-align: left; /* Alinea los botones al centro horizontalmente */
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="info-container">
        <div>

            <asp:Label Text="" runat="server" ID="lblID" />
        </div>
        <div>
            <asp:Label Text="Codigo: " runat="server"/>
            <asp:Label Text="" runat="server" ID="lblCode" />
        </div>
        <div>
            <asp:Label Text="Nombre: " runat="server"/>
            <asp:Label Text="" runat="server" ID="lblNombre" />
        </div>
        <div>
            <asp:Label Text="Descripción: " runat="server"/>
            <asp:Label Text="" runat="server" ID="lblDescripcion" />
        </div>
        <div>
            <asp:Label Text="Marca: " runat="server"/>
            <asp:Label Text="" runat="server" ID="lblMarca" />
        </div>
        <div>
            <asp:Label Text="Categoria: " runat="server"/>
            <asp:Label Text="" runat="server" ID="lblCategoria" />
        </div>
        <div>
            <asp:Label Text="Precio: $" runat="server"/>
            <asp:Label Text="" runat="server" ID="lblPrecio" />
        </div>
    </div>

    <div class="container">
        <asp:Repeater ID="rptImagenes" runat="server">
            <ItemTemplate>
                <div class="card">
                    <img src='<%# Eval("ImagenUrl") %>' class="card-img-top">
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <div class="text-center">
            <asp:Button Text="Prev" runat="server" ID="btnPrev" class="btn btn-primary" OnClick="btnPrev_Click" />
            <asp:Button Text="Next" runat="server" ID="btnNext" class="btn btn-primary" OnClick="btnNext_Click" />
        </div>
    </div>





    <asp:Label Text="" runat="server" ID="lblError" />

</asp:Content>
