using CarritoCompras_Web.Properties;
using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Web.UI.WebControls.Image;

namespace CarritoCompras_Web
{
    public partial class Default : System.Web.UI.Page
    {

        private List<Articulo> listaArticulos;
        private List<Categoria> listaCategorias;
        private List<Marca> listaMarca;
        private List<string> sortOptions = new List<string>();
        public int itemsToCart;
        public Purchase Purchase;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.listaArticulos = CargarArticulos();
            this.listaCategorias = CargarCategorias();
            this.listaMarca = CargarMarca();
            this.sortOptions.Add("Por menor precio");
            this.sortOptions.Add("Por mayor precio");

            if (!IsPostBack)
            {
                Purchase = new Purchase(); // Crea una nueva instancia de Purchase
                Purchase.amount = 0;
                itemsToCart = 0;
                ViewState["ItemsToCart"] = itemsToCart;
                CartCountLabel.Text = itemsToCart.ToString();
                SortOptionsDropDown.DataSource = sortOptions;
                SortOptionsDropDown.DataBind();
                rptBrands.DataSource = listaMarca;
                rptBrands.DataBind();
                rptCategoria.DataSource = listaCategorias;
                rptCategoria.DataBind();
                rptArticulos.DataSource = listaArticulos;
                rptArticulos.DataBind();
            }
            else
            {
                itemsToCart = (int)ViewState["ItemsToCart"];
            }

        }

        private List<Articulo> CargarArticulos()
        {
            ArticuloNegocio negocio = new ArticuloNegocio(); 

            return negocio.listar();

        }

        private List<Categoria> CargarCategorias()
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            return categoriaNegocio.List();
        }

        private List<Marca> CargarMarca()
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            return marcaNegocio.list();
        }

        protected void rptArticulos_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            if (!IsPostBack)
            {
                if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    Articulo articulo = (Articulo) e.Item.DataItem;
                    Image imgArticulo = (Image)e.Item.FindControl("imgArticulo"); 
                    if (articulo.ImagenURL.Count > 0 || articulo.ImagenURL != null)
                    {
                        try
                        {
                        
                            if (IsValidUrl(articulo.ImagenURL.First().ImagenUrl) == HttpStatusCode.OK)
                            {
                                imgArticulo.ImageUrl = articulo.ImagenURL.First().ImagenUrl;
                            }
                        
                        }catch (Exception)
                        {
                            imgArticulo.ImageUrl = "~/Resources/OIP.jpg";
                        }

                    }
                    else
                    {
                        imgArticulo.ImageUrl = "~/Resources/OIP.jpg";
                    }
                }

            }
            
        }

        private HttpStatusCode IsValidUrl(string url)
        {
            try
            {
                HttpWebRequest request = WebRequest.CreateHttp(url);
                request.Method = "GET";
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    return response.StatusCode;
                }
            }
            catch(Exception e) 
            {
                throw e;
            }
        }

        protected void AddToCart_Click(object sender, EventArgs e)
        {
            itemsToCart++;
            ViewState["ItemsToCart"] = itemsToCart;
            CartCountLabel.Text = itemsToCart.ToString();

            LinkButton button = sender as LinkButton;
            int articuloId = int.Parse(button.CommandArgument);

            Purchase purchase = Session["Cart"] as Purchase;
            if (purchase == null)
            {
                purchase = new Purchase();
                purchase.Oid = 1;
                purchase.Articulos = new List<Articulo>();
                purchase.amount = 0;
            }

            Articulo articulo = listaArticulos.Find(art => art.Id == articuloId);
            purchase.Articulos.Add(articulo);
            purchase.amount += articulo.Precio;

            Session["Cart"] = purchase;
        }


        protected void SortOptionsDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = SortOptionsDropDown.SelectedValue;

            if (selectedValue == "Por menor precio")
            {
                listaArticulos = listaArticulos.OrderBy(item => item.Precio).ToList();
            }
            else if (selectedValue == "Por mayor precio")
            {
                listaArticulos = listaArticulos.OrderByDescending(item => item.Precio).ToList();
            }

            rptArticulos.DataSource = listaArticulos;
            rptArticulos.DataBind();
        }

        protected void CheckBoxBrands_CheckedChanged(object sender, EventArgs e)
        {

            CheckBox checkBox = (CheckBox)sender;
            string brand = checkBox.Text;
            List<Articulo> listaArticulosFiltrada = listaArticulos.Where(art => art.Marca.Descripcion == brand).ToList();

            rptArticulos.DataSource= listaArticulosFiltrada;
            rptArticulos.DataBind();
        }

        protected void btnVerDetalle_Click(object sender, EventArgs e)
        {
            string idArticulo = ((Button)sender).CommandArgument;
            Response.Redirect("DetalleArticulo.aspx?id=" + idArticulo);

        }

        protected void PurchaseButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Carrito.aspx");
        }
    }
}