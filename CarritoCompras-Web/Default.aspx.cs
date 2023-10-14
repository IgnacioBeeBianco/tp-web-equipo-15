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