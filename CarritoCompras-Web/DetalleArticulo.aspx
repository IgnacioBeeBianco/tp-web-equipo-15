<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="DetalleArticulo.aspx.cs" Inherits="CarritoCompras_Web.DetalleArticulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <link rel="stylesheet" type="text/css" href="Content/estilos.css">

    <div class="container" id="divPrincipal" runat="server">

        <div id="imagenContainer" class="card">
            <asp:Image ID="imgArticulo1" runat="server" CssClass="card-img-top1" AlternateText="Artículo" />
            <div class="card-body">
                <h5 class="card-title" runat="server" id="lblTitulo"></h5>
                <p class="card-text" runat="server" id="P1"></p>
                <div class="button-container text-center">
                    <asp:Button Text="Prev" runat="server" ID="btnPrev" class="btn btn-primary" OnClick="btnPrev_Click" />
                    <asp:Button Text="Next" runat="server" ID="btnNext" class="btn btn-primary" OnClick="btnNext_Click" />
                </div>
            </div>
        </div>

        <div class="info-container">
            <div>
                <asp:Label Text="Codigo:" runat="server" CssClass="lblText" />
                <asp:Label Text="   " runat="server" ID="lblCode" CssClass="lblInfo" />
            </div>
            <div>
                <asp:Label Text="Nombre:" runat="server" CssClass="lblText" />
                <asp:Label Text="   " runat="server" ID="lblNombre" CssClass="lblInfo" />
            </div>
            <div>
                <asp:Label Text="Descripción:" runat="server" CssClass="lblText" />
                <asp:Label Text="   " runat="server" ID="lblDescripcion" CssClass="lblInfo" />
            </div>
            <div>
                <asp:Label Text="Marca:" runat="server" CssClass="lblText" />
                <asp:Label Text="   " runat="server" ID="lblMarca" CssClass="lblInfo" />
            </div>
            <div>
                <asp:Label Text="Categoria:" runat="server" CssClass="lblText" />
                <asp:Label Text="   " runat="server" ID="lblCategoria" CssClass="lblInfo" />
            </div>
            <div>
                <asp:Label Text="Precio:" runat="server" CssClass="lblText" />
                <asp:Label Text="   " runat="server" ID="lblPrecio" CssClass="lblInfo" />
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
