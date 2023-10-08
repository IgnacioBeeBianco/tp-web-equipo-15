<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CarritoCompras_Web.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container d-flex justify-content-center flex-wrap p-4">
    <asp:Repeater ID="rptArticulos" runat="server" OnItemCreated="rptArticulos_ItemCreated">
        <ItemTemplate>
            <div class="col-md-4">
                <div class="card mb-4">

                    <div class="card-body text-center">
                        <asp:Image ID="imgArticulo" runat="server" CssClass="img-fluid" />
                        <div class="btn-container">
                            <asp:Button ID="btnPrevImage" runat="server" Text="Prev" OnClick="btnPrevImage_Click" CssClass="btn btn-primary"/>
                            
                            <asp:Button ID="btnNextImage" runat="server" Text="Next" OnClick="btnNextImage_Click" CssClass="btn btn-primary"/>

                        </div>
                        <h5 class="card-title mt-3"><%# Eval("Nombre") %></h5>
                        <p class="card-text"><%# Eval("Descripcion") %></p>
                        <p class="card-text">$<%# Eval("Precio") %></p>
                        <a href="#" class="btn btn-primary">Agregar al carrito</a>
                        <a href="DetalleArticulo.aspx?id=<%#Eval("Id")%>" class="btn btn-primary">Detalle Articulo</a>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>

    </div>

</asp:Content>
