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
using System.Text.RegularExpressions;
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
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    Articulo articulo = (Articulo)e.Item.DataItem;
                    Image imgArticulo = (Image)e.Item.FindControl("imgArticulo");
                    if (articulo.ImagenURL.Count > 0 || articulo.ImagenURL != null)
                    {
                        try
                        {

                            if (IsValidUrl(articulo.ImagenURL.First().ImagenUrl) == HttpStatusCode.OK)
                            {
                                imgArticulo.ImageUrl = articulo.ImagenURL.First().ImagenUrl;
                            }

                        }
                        catch (Exception)
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
            catch (Exception e)
            {
                throw e;
            }
        }

        protected void AddToCart_Click(object sender, EventArgs e)
        {
            Purchase purchase = new Purchase();
            itemsToCart++;
            ViewState["ItemsToCart"] = itemsToCart;
            CartCountLabel.Text = itemsToCart.ToString();

            Button button = sender as Button;
            int articuloId = int.Parse(button.CommandArgument);

            Articulo articulo = listaArticulos.Find(art => art.Id == articuloId);

            purchase.Oid = 1;
            purchase.Articulos.Add(articulo);
            purchase.amount += articulo.Precio;
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

        private List<Articulo> FiltrarArticulos()
        {
            List<Articulo> listaArticulosFiltrada = listaArticulos;

            // Filtra por marca solo si hay marcas seleccionadas
            var marcasSeleccionadas = rptBrands.Items.Cast<RepeaterItem>()
                .Where(item => ((CheckBox)item.FindControl("CheckBoxBrands")).Checked)
                .Select(item => ((CheckBox)item.FindControl("CheckBoxBrands")).Text);

            if (marcasSeleccionadas.Any())
            {
                listaArticulosFiltrada = listaArticulosFiltrada.Where(art => marcasSeleccionadas.Contains(art.Marca.Descripcion)).ToList();
            }

            // Filtra por categoría solo si hay categorías seleccionadas
            var categoriasSeleccionadas = rptCategoria.Items.Cast<RepeaterItem>()
                .Where(item => ((CheckBox)item.FindControl("CheckBoxCategoria")).Checked)
                .Select(item => ((CheckBox)item.FindControl("CheckBoxCategoria")).Text);

            if (categoriasSeleccionadas.Any())
            {
                listaArticulosFiltrada = listaArticulosFiltrada.Where(art => categoriasSeleccionadas.Contains(art.Categoria.Descripcion)).ToList();
            }

            return listaArticulosFiltrada;
        }

        protected void btnVerDetalle_Click(object sender, EventArgs e)
        {
            string idArticulo = ((Button)sender).CommandArgument;
            Response.Redirect("DetalleArticulo.aspx?id=" + idArticulo);

        }

        protected void CheckBoxBrands_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarFiltrosYMostrarMensaje();
        }

        protected void CheckBoxCategoria_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarFiltrosYMostrarMensaje();
        }

        protected void txtFilterByName_TextChanged(object sender, EventArgs e)
        {
            ActualizarFiltrosYMostrarMensaje();
        }
        
        private void ActualizarFiltrosYMostrarMensaje()
        {
            string filterText = txtFilterByName.Text.Trim();

            if (!string.IsNullOrEmpty(filterText))
            {
                List<Articulo> listaFiltrada = listaArticulos.Where(articulo => articulo.Nombre.Contains(filterText)).ToList();

                rptArticulos.DataSource = listaFiltrada;
                rptArticulos.DataBind();

                if (listaFiltrada.Count == 0)
                {
                    lblMensajeNoArticulos.Text = "No se encontraron artículos con el nombre seleccionado";
                    lblMensajeNoArticulos.Visible = true;
                    SortOptionsDropDown.Visible = false;
                }
                else
                {
                    lblMensajeNoArticulos.Visible = false;
                    SortOptionsDropDown.Visible = true;
                }
            }
            else
            {
                List<Articulo> listaArticulosFiltrada = FiltrarArticulos();

                rptArticulos.DataSource = listaArticulosFiltrada;
                rptArticulos.DataBind();

                if (listaArticulosFiltrada.Count == 0)
                {
                    string mensaje = "No se encontraron artículos";

                    // Verifica si se seleccionaron marcas
                    if (rptBrands.Items.Cast<RepeaterItem>().Any(item => ((CheckBox)item.FindControl("CheckBoxBrands")).Checked))
                    {
                        mensaje += " de la marca";
                    }

                    // Verifica si se seleccionaron categorías
                    if (rptCategoria.Items.Cast<RepeaterItem>().Any(item => ((CheckBox)item.FindControl("CheckBoxCategoria")).Checked))
                    {
                        if (rptBrands.Items.Cast<RepeaterItem>().Any(item => ((CheckBox)item.FindControl("CheckBoxBrands")).Checked))
                        {
                            mensaje += " y categoría";
                        }
                        else
                        {
                            mensaje += " de la categoría";
                        }
                    }

                    mensaje += " seleccionada";

                    lblMensajeNoArticulos.Text = mensaje;
                    lblMensajeNoArticulos.Visible = true;
                    SortOptionsDropDown.Visible = false;
                }
                else
                {
                    lblMensajeNoArticulos.Visible = false;
                    SortOptionsDropDown.Visible = true;
                }
            }
        }

    }
}