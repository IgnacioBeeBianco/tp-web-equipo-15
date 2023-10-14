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
using Label = System.Web.UI.WebControls.Label;

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

        private bool isValidUrl(string url)
        {
            try
            {
                HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
                request.Method = "HEAD";
                request.Timeout = 5000;

                using(HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    int statusCode = (int)response.StatusCode;
                    if(statusCode >= 100 && statusCode < 400)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }catch(WebException ex)
            {
                if(ex.Status == WebExceptionStatus.ProtocolError)
                {
                    return false;
                }
            }catch (Exception)
            {
                return false;
            }

            return false;
        }

        protected void rptArticulos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;
            Image image = item.FindControl("imgArticulo") as Image;
            int articuloId;
            Label lblIdArticulo = item.FindControl("IdArticulo") as Label;
            int.TryParse(lblIdArticulo.Text, out articuloId);

            Articulo articulo = listaArticulos.Find(art => art.Id ==  articuloId);
            
            if(articulo != null)
            {
                string url = articulo.ImagenURL.First().ImagenUrl;
                if (isValidUrl(url))
                {
                    image.ImageUrl = url;
                }
                else
                {
                    image.ImageUrl = "~/Resources/OID.jpg";
                }
            }
            
        }
    }
}