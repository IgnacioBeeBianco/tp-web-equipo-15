<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="CarritoCompras_Web.Carrito" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        table {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 20px;
        }

        th, td {
            padding: 10px;
            text-align: left;
        }

        .remove-button {
            background-color: #ff6347; 
            color: white;
            padding: 5px 10px;
            border: none;
            cursor: pointer;
        }

        .remove-button:hover {
            background-color: #d9534f; 
        }

        .hyperlink{
            margin-left: 3rem;
            text-align: start;
            text-decoration: none;
            color: black;
            cursor: pointer;
        }
    </style>

    <div class="content">
        <div class="m-5">
            <h3>Productos en mi carrito</h3>
        </div>

        <div class="row">
            <asp:Label ID="NoItemsLabel" runat="server" Text="Agrega un artículo al carrito para efectuar la compra" Visible='<%# Session["Cart"] == null %>' CssClass="text-center"></asp:Label>
            <asp:Panel ID="CartPanel" runat="server" Visible='<%# Session["Cart"] != null %>' CssClass="row">
                <asp:Panel runat="server" ID="CartContent" CssClass="col-md-9">
                    <div class="content-table m-4">
                        <asp:Table ID="CartTable" runat="server">
                            <asp:TableHeaderRow>
                                <asp:TableHeaderCell></asp:TableHeaderCell>
                                <asp:TableHeaderCell>Nombre del Artículo</asp:TableHeaderCell>
                                <asp:TableHeaderCell>Precio</asp:TableHeaderCell>
                                <asp:TableHeaderCell>Acciones</asp:TableHeaderCell>
                            </asp:TableHeaderRow>
                        </asp:Table>
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" ID="SummaryPanel" CssClass="col-md-3">
                   <h3>Summary</h3>
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
