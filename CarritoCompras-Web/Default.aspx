<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CarritoCompras_Web.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="hero-content wv-100 text-center h-50 d-flex justify-content-start align-items-center" style="background-image: url('/Resources/pexels-jéshoots-238118.jpg'); background-size: cover;">
        <h1 class="ms-5">Catálogo de productos</h1>
    </div>

    <div class="d-flex justify-content-end m-3">
        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-primary">
            <asp:Label ID="CartCountLabel" runat="server" CssClass="bi bi-cart"><%# itemsToCart %></asp:Label>
        </asp:LinkButton>
    </div>
    <section>
        <div class="row">
            <div class="col-2 justify-content-center">
            <div class="filter-container m-5">
                <div class="title text-center">
                    <h4>Categorías</h4>

                </div>
                <div class="brand-filter">
                <h6>Marca</h6>
                    <asp:Repeater ID="rptBrands" runat="server">
                        <ItemTemplate>
                            <div>
                                <asp:CheckBox runat="server" Text='<%# Eval("Descripcion") %>' 
                                    ID="CheckBoxBrands" AutoPostBack="true" 
                                    OnCheckedChanged="CheckBoxBrands_CheckedChanged" 
                                    CommandArgument='<%# Eval("Id") %>'
                                    />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>

                </div>
                <div class="category-filter">
                    <h6 class="mt-4">Categoría</h6>
                        <asp:Repeater ID="rptCategoria" runat="server">
                            <ItemTemplate>
                                <div>
                                    <asp:CheckBox  runat="server" Text='<%# Eval("Descripcion") %>' CssClass="flex-row; p-1" />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                </div>
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
                                        <asp:Button CssClass="btn btn-primary" runat="server" Text="Agregar al carrito" CommandArgument='<%# Eval("Id") %>' CommandName="ArticuloId" OnClick="AddToCart_Click"/>
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
</asp:Content>
