using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
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

        private List<Articulo> listaArticulos = new List<Articulo>();
        private int imageIndex = 0; // Índice de la imagen actual
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarArticulos();
            }
            
        }

        private void CargarArticulos()
        {
            ArticuloNegocio negocio = new ArticuloNegocio(); // Asegúrate de crear una instancia de tu negocio.
            this.listaArticulos = negocio.listar();
            
            rptArticulos.DataSource = listaArticulos;
            rptArticulos.DataBind();
            
        }

        protected string GetImage(object imagenesObj)
        {
            if (imagenesObj != null && imagenesObj is List<Imagen> imagenes && imagenes.Count > 0)
            {
                return imagenes[0].ImagenUrl;
            }

            // Si no hay imágenes o la lista está vacía, puedes devolver una URL por defecto válida.
            return "/Resources/OIP.jpg"; // Asumiendo que esta es una ruta válida
        }

    }
}