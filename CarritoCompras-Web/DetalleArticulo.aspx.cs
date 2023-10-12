using Dominio;
using Negocio;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace CarritoCompras_Web
{
    public partial class DetalleArticulo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int articuloID = ObtenerIDArticuloDesdeQueryString();
                DetalleArt(articuloID);
            }

        }

        private int ObtenerIDArticuloDesdeQueryString()
        {
            int articuloID = 0;
            if (Request.QueryString["Id"] != null)
            {
                articuloID = Convert.ToInt32(Request.QueryString["Id"]);
            }
            return articuloID;
        }

        public void DetalleArt(int articuloID)
        {

            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo articulo = negocio.ObtenerDetallesArticulo(articuloID);

            if (articulo != null)
            {
                lblCode.Text = articulo.Code.ToString();
                lblNombre.Text = articulo.Nombre;
                lblDescripcion.Text = articulo.Descripcion;
                lblCategoria.Text = articulo.Categoria.Descripcion;
                lblMarca.Text = articulo.Marca.Descripcion;
                lblPrecio.Text = articulo.Precio.ToString();

                btnVolver.Visible = false;
                
                ImagenNegocio imagenNegocio = new ImagenNegocio();
                List<Imagen> imagenes = imagenNegocio.GetImagens(articuloID);

                if (imagenes.Count > 0)
                {
                    imgArticulo.ImageUrl = imagenes[0].ImagenUrl;
                    
                }
                else
                {
                    imgArticulo.ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTQwdj-kVHd2ows7cDiCsnSXFdzUv3j2Ns9r0S7HGKKtrBGZRkkrmcvJ5VJEM4nPNEPhUw&usqp=CAU";
                }
                Session["ArticuloImages"] = imagenes;
            }
            else
            {
                lblError.Text = "Debe seleccionar un articulo primero";
                btnVolver.Visible = true;
                divPrincipal.Visible = false;

            }
            
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            List<Imagen> imagenes = (List<Imagen>)Session["ArticuloImages"];
            int currentIndex = Convert.ToInt32(Session["ImageIndex"] ?? 0);

            if (currentIndex > 0)
            {
                currentIndex--;
                imgArticulo.ImageUrl = imagenes[currentIndex].ImagenUrl;
                Session["ImageIndex"] = currentIndex;
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            List<Imagen> imagenes = (List<Imagen>)Session["ArticuloImages"];
            int currentIndex = Convert.ToInt32(Session["ImageIndex"] ?? 0);

            if (currentIndex < imagenes.Count - 1)
            {
                currentIndex++;
                imgArticulo.ImageUrl = imagenes[currentIndex].ImagenUrl;
                Session["ImageIndex"] = currentIndex;
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}