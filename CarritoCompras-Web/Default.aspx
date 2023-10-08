<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CarritoCompras_Web.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container d-flex justify-content-center flex-wrap p-4">
    <asp:Repeater ID="rptArticulos" runat="server">
        <ItemTemplate>
            <div class="col-md-4">
                <div class="card mb-4">

                    <div class="card-body">
                        <asp:Image runat="server" ImageUrl='<%# ((List<Dominio.Imagen>)Eval("ImagenURL")).Count > 0 ? ((List<Dominio.Imagen>)Eval("ImagenURL")).First().ImagenUrl : "~/Ruta/ImagenPorDefecto.jpg" %>' CssClass="img-fluid" />
                        <h5 class="card-title"><%# Eval("Nombre") %></h5>
                        <p class="card-text"><%# Eval("Descripcion") %></p>
                        <p class="card-text">$<%# Eval("Precio") %></p>
                        <a href="#" class="btn btn-primary">Agregar al carrito</a>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>

    </div>

</asp:Content>
