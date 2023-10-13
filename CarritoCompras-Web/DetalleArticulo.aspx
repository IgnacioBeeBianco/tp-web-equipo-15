<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="DetalleArticulo.aspx.cs" Inherits="CarritoCompras_Web.DetalleArticulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .container {
            display: flex;
        }

        #imagenContainer {
            width: 900px;
            margin-left: 0.5%;
            margin-top: 7%;
            padding: 10px;
        }

        .card-img-top {
            width: 900px;
            height: 600px;
        }

        .info-container {
            margin-top: 20%;
            flex: 1;
            padding: 30px;
        }

        .lblText {
            font-size: 18px;
            font-weight: bold;
            color: #A0D732;
            text-decoration: underline;
        }

        .lblInfo {
            font-size: 16px;
            font-weight: bold;
            color: #A0A7AD;
        }

        .centered-container {
            display: flex;
            flex-direction: column;
            align-items: center;
            justify-content: center;
            height: 90vh;
            text-align: center;
        }

            .centered-container div {
                margin: 10px;
            }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container" id="divPrincipal" runat="server">

        <div id="imagenContainer" class="card">
            <asp:Image ID="imgArticulo" runat="server" CssClass="card-img-top" AlternateText="Artículo" />
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
