<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="CarritoCompras_Web.Carrito" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        table {
           
            border-collapse: collapse;
            margin-bottom: 20px;
        }

        th, td {
            padding: 10px;
            text-align: left;
        }

        .remove-button {
            background-color: none; 
            color: black;
            padding: 5px 10px;
            border: none;
            cursor: pointer;
            border-radius: 10px;
        }

        .hyperlink{
            margin-left: 3rem;
            text-align: start;
            text-decoration: none;
            color: black;
            cursor: pointer;
        }

        .summary-box{
            text-align: left;
            background-color: #eeeeee;
            height: 400px;
            padding: 2rem;
        }

        .line {
            border: 0;
            border-top: 1px solid #000; 
        }

        .custom-input{
            background: none;
            color: #999999;
        }

        .custom-span{
            background: none;
            color: #999999;
        }

        .panel-span{
            color: #999999;
        }

        

    </style>

    <div class="content">
        <div class="m-5">
            <h3>Productos en mi carrito</h3>
        </div>

        <asp:Label ID="NoItemsLabel" runat="server" Text="Agrega un artículo al carrito para efectuar la compra" CssClass="d-table m-auto">

        </asp:Label>
        <div class="row h-100">
            <asp:Panel ID="CartPanel" runat="server" CssClass="row">
                <asp:Panel runat="server" ID="CartContent" CssClass="col-md-9">
                        <asp:Repeater runat="server" ID="TableContent">
                            <HeaderTemplate>
                                <table class="content-table m-4 w-100">
                                    <tr>
                                        <th></th>
                                        <th>Nombre del Artículo</th>
                                        <th>Cantidad</th>
                                        <th>Precio unitario</th>
                                        <th>Precio total</th>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:Image ID="imgArticulo" runat="server" Width="150px" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "Key.ImagenURL[0].ImagenUrl") ?? "~/Resources/OIP.jpg" %>' />
                                    </td>
                                    <td>
                                        <asp:Literal runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Key.Nombre") %>' />
                                    </td>
                                    <td>
                                        <asp:Literal runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Value") %>' />
                                    </td>
                                    <td>
                                        <asp:Literal runat="server" Text='<%# String.Format("{0:C}", (Decimal)DataBinder.Eval(Container.DataItem, "Key.Precio")) %>' />
                                    </td>
                                    <td>
                                        <asp:Literal runat="server" Text='<%# String.Format("{0:C}", (Decimal)DataBinder.Eval(Container.DataItem, "Key.Precio") * (int)DataBinder.Eval(Container.DataItem, "Value")) %>' />
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="DeleteButton" runat="server" CssClass="remove-button" CommandName="Remove" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Key.Id") %>' OnCommand="RemoveButton_Command">
                                            <i class="bi bi-x-lg"></i>
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>

                </asp:Panel>
                <asp:Panel runat="server" ID="SummaryPanel" CssClass="col-md-3 summary-box">
                   <h3>Resúmen</h3>

                    <div class="costs d-flex flex-column mt-5 gap-3">

                        <div class="d-flex justify-content-between">
                            <span class="panel-span">Items</span>
                            <asp:Literal runat="server" Text='<%# GetTotalItems() %>' />
                        </div>
                        <div class="d-flex justify-content-between">
                            <span class="panel-span">Subtotal</span>
                            <asp:Literal runat="server" Text='<%# String.Format("{0:C}", GetTotalAmount()) %>' />
                        </div>

                        <div class="d-flex justify-content-between">
                            <span class="panel-span">Envio</span>
                            <asp:Literal runat="server" Text='<%# String.Format("{0:C}", 25)%>' />
                        </div>


                    </div>
                    <hr class="line">

                    <div class="gift-code d-flex justify-content-between align-items-center mt-2">
                        <div class="input-group">
                            <asp:TextBox runat="server" Text="Ingresa tu cupón de regalo" CssClass="form-control custom-input"></asp:TextBox>                          
                            <span class="input-group-text custom-span" id="basic-addon2"><i class="bi bi-arrow-right"></i></span>
                        </div>

                        
                    </div>
                    <hr class="line">

                    <div>
                        <div class="d-flex justify-content-between">
                            <span class="panel-span">Precio total</span>
                            <asp:Literal runat="server" Text='<%# String.Format("{0:C}", GetTotalAmount() + 25) %>' />
                        </div>
                    </div>

                </asp:Panel>
                
            </asp:Panel>
        </div>
        <div class="row mb-5">
            <asp:HyperLink ID="BackToIndex" runat="server" CssClass="hyperlink" NavigateUrl="~/Default.aspx">
                <i class="bi bi-arrow-left"> Seguir comprando</i>
            </asp:HyperLink>
        </div>
    </div>
    

</asp:Content>
