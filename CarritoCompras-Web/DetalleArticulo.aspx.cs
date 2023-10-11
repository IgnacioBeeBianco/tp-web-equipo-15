using Dominio;
using Negocio;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarritoCompras_Web
{
    public partial class DetalleArticulo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DetalleArt();
            }

        }

        public void DetalleArt()
        {
            int articuloID = 0;

            if (Request.QueryString["Id"] != null)
            {
                articuloID = Convert.ToInt32(Request.QueryString["Id"]);
            }

            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo articulo = negocio.ObtenerDetallesArticulo(articuloID);

            if (articulo != null)
            {
                lblID.Text = articulo.Id.ToString();
                lblCode.Text = articulo.Code.ToString();
                lblNombre.Text = articulo.Nombre;
                lblDescripcion.Text = articulo.Descripcion;
                lblCategoria.Text = articulo.Categoria.Descripcion;
                lblMarca.Text = articulo.Marca.Descripcion;
                lblPrecio.Text = articulo.Precio.ToString();

                ImagenDAO imagenDAO = new ImagenDAO();
                List<Imagen> imagenes = imagenDAO.GetImagenes(articuloID);

                if (imagenes != null && imagenes.Count > 0)
                {
                    rptCarousel.DataSource = imagenes;
                    rptCarousel.DataBind();
                }
            }
        }
    }
}