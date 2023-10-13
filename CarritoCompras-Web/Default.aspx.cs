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
                Purchase = new Purchase();
                if (Session["itemsToCart"] == null)
                {
                    Session["itemsToCart"] = 0;
                }
                else
                {
                    itemsToCart = (int)Session["ItemsToCart"];
                }
                CartCountLabel.Text = itemsToCart.ToString();
                LoadSortOptions(sortOptions);
                LoadMarcasRepeater(listaMarca);
                LoadCategoriasRepeater(listaCategorias);
                LoadArticulosRepeater(listaArticulos);
            }
            else
            {
                itemsToCart = (int)Session["ItemsToCart"];
                CartCountLabel.Text = itemsToCart.ToString();
            }

        }

        private void LoadArticulosRepeater(List<Articulo> articulos)
        {
            rptArticulos.DataSource = articulos;
            rptArticulos.DataBind();
        }

        private void LoadCategoriasRepeater(List<Categoria> categorias)
        {
            rptCategoria.DataSource = categorias;
            rptCategoria.DataBind();
        }

        private void LoadMarcasRepeater(List<Marca> marcas)
        {
            rptBrands.DataSource = marcas;
            rptBrands.DataBind();
        }

        private void LoadSortOptions(List<String> sortOptions)
        {
            SortOptionsDropDown.DataSource = sortOptions;
            SortOptionsDropDown.DataBind();
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
            Session["ItemsToCart"] = itemsToCart;
            CartCountLabel.Text = itemsToCart.ToString();

            LinkButton button = sender as LinkButton;
            int articuloId = int.Parse(button.CommandArgument);
            Articulo articulo = listaArticulos.Find(art => art.Id == articuloId);

            Purchase purchase = Session["Cart"] as Purchase;
            if (purchase != null)
            {
                if (purchase.Articulos.ContainsKey(articulo))
                {
                    purchase.Articulos[articulo] += 1;
                }
                else
                {
                    purchase.Articulos[articulo] = 1;
                }
            }
            else
            {
                purchase = new Purchase();
                purchase.Articulos.Add(articulo, 1);
            }

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

            LoadArticulosRepeater(listaArticulos);
        }

        protected void CheckBoxBrands_CheckedChanged(object sender, EventArgs e)
        {

            CheckBox checkBox = (CheckBox)sender;
            string brand = checkBox.Text;
            List<Articulo> listaArticulosFiltrada = listaArticulos.Where(art => art.Marca.Descripcion == brand).ToList();

            LoadArticulosRepeater(listaArticulosFiltrada);
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