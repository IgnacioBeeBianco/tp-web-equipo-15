<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CarritoCompras_Web.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" src="Content/funcionesJS.js"></script>
    <link rel="stylesheet" type="text/css" href="Content/estilos.css">

    <div class="hero-content wv-100 text-center h-50 d-flex justify-content-start align-items-center" style="background-image: url('/Resources/pexels-jéshoots-238118.jpg'); background-size: cover;">
        <h1 class="ms-5">Catálogo de productos</h1>
    </div>

    <div class="d-flex justify-content-end m-3">
        <asp:LinkButton ID="PurchaseButton" runat="server" OnClick="PurchaseButton_Click" CssClass="btn btn-primary cart-button">
            <asp:Label ID="CartCountLabel" runat="server" CssClass="bi bi-cart"></asp:Label>
            <asp:Label runat="server"><%# itemsToCart %></asp:Label>
        </asp:LinkButton>
    </div>

    <section>

        <div class="row">
            <div class="col-3">

                <div class="justify-content-center">
                    <div class="filter-container m-5">
                        <div class="title text-center">
                            <h4 class="mt-4">Filtros</h4>
                            <div class="filter-container">
                                <h6 class="mt-4">Buscar</h6>
                                <asp:TextBox ID="txtFilterByName" runat="server" OnTextChanged="txtFilterByName_TextChanged" AutoPostBack="true" CssClass="form-control custom-input"></asp:TextBox>
                            </div>
                        </div>
                        <div class="brand-filter">
                            <h6 class="mt-4">Marca</h6>
                            <asp:Repeater ID="rptBrands" runat="server">
                                <ItemTemplate>
                                    <div>
                                        <asp:CheckBox runat="server" Text='<%# Eval("Descripcion") %>'
                                            ID="CheckBoxBrands" AutoPostBack="true"
                                            OnCheckedChanged="CheckBoxBrands_CheckedChanged"
                                            CommandArgument='<%# Eval("Id") %>' />
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <div class="category-filter">
                            <h6 class="mt-4">Categoría</h6>
                            <asp:Repeater ID="rptCategoria" runat="server">
                                <ItemTemplate>
                                    <div>
                                        <asp:CheckBox runat="server" Text='<%# Eval("Descripcion") %>'
                                            ID="CheckBoxCategoria" AutoPostBack="true"
                                            OnCheckedChanged="CheckBoxCategoria_CheckedChanged"
                                            CommandArgument='<%# Eval("Id") %>' />
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-9">

                <div class="container d-flex flex-column justify-content-center gap-2 col-10">
                    <div class="sorts">
                        <asp:DropDownList ID="SortOptionsDropDown" runat="server" AutoPostBack="true" OnSelectedIndexChanged="SortOptionsDropDown_SelectedIndexChanged"></asp:DropDownList>
                    </div>

                    <asp:Label ID="lblMensajeNoArticulos" runat="server" Visible="false" CssClass="alert alert-warning"></asp:Label>

                    <div class="items">
                        <asp:Repeater ID="rptArticulos" runat="server" OnItemDataBound="rptArticulos_ItemDataBound">
                            <ItemTemplate>
                                <div class="col-12">
                                    <div class="card mb-4 w-100">

                                        <div class="card-body text-center">
                                            <asp:Label ID="IdArticulo" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                            <asp:Image ID="imgArticulo" runat="server" CssClass="card-img-top w-50" />
                                    
                                            <h5 class="card-title mt-3"><%# Eval("Nombre") %></h5>
                                            <p class="card-text"><%# Eval("Descripcion") %></p>
                                            <p class="card-text"><%# String.Format("{0:C}", Eval("Precio")) %></p>
                                            <asp:LinkButton ID="button" runat="server" CssClass="btn btn-primary" CommandArgument='<%# Eval("Id") %>' CommandName="ArticuloId" OnClick="AddToCart_Click">
                                                <i class="bi bi-cart-plus-fill"></i>
                                            </asp:LinkButton>
                                            <a href="DetalleArticulo.aspx?id=<%#Eval("Id")%>" class="btn btn-primary"><i class="bi bi-eye-fill"></i></a>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>

                    </div>

                </div>
            </div>
        </div>

    </section>

</asp:Content>
