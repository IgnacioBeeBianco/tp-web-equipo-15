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
        public int itemsToCart = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.listaArticulos = CargarArticulos();
            this.listaCategorias = CargarCategorias();
            this.listaMarca = CargarMarca();
            this.sortOptions.Add("Por menor precio");
            this.sortOptions.Add("Por mayor precio");

            if (!IsPostBack)
            {
                SortOptionsDropDown.DataSource = sortOptions;
                SortOptionsDropDown.DataBind();
                rptBrands.DataSource = listaMarca;
                rptBrands.DataBind();
                rptCategoria.DataSource = listaCategorias;
                rptCategoria.DataBind();
                rptArticulos.DataSource = listaArticulos;
                rptArticulos.DataBind();
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

        public void OnNextImageBtnClick()
        {

        }

        protected void btnNextImage_Click(object sender, EventArgs e)
        {
            
            Button btnNextImage = (Button)sender;
            RepeaterItem item = (RepeaterItem)btnNextImage.NamingContainer;
            Image imgArticulo = (Image)item.FindControl("imgArticulo");
            Articulo articulo = listaArticulos[item.ItemIndex];

            string currentUrl = imgArticulo.ImageUrl;

            int currentIndex = articulo.ImagenURL.FindIndex(img => img.ImagenUrl == currentUrl);

            try
            {
                string nextUrl = articulo.ImagenURL[currentIndex + 1].ImagenUrl;
                if (IsValidUrl(nextUrl) == HttpStatusCode.OK)
                {
                    imgArticulo.ImageUrl = nextUrl;
                }
                else
                {
                    imgArticulo.ImageUrl = "~/Resources/OIP.jpg";
                }
            }
            catch (Exception)
            {
                return;
            }

        }

        protected void btnPrevImage_Click(object sender, EventArgs e)
        {

        }

        protected void AddToCart_Click(object sender, EventArgs e)
        {
            this.itemsToCart++;

        }

        protected int GetItemsInCart()
        {
            return itemsToCart;
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

            rptCategoria.DataSource = listaArticulos;
            rptCategoria.DataBind();
        }
    }
}