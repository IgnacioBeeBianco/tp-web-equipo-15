using Dominio;
using Negocio;
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
                // Verifica si el parámetro "id" existe en la URL
                if (Request.QueryString["Id"] != null)
                {
                    // Obtén el ID del artículo de la URL
                    int articuloID = Convert.ToInt32(Request.QueryString["Id"]);

                    // Utiliza el ID para cargar y mostrar los detalles del artículo
                    lblID.Text = "ID: " + articuloID.ToString();
                }
                else
                {
                    lblError.Text= "ERROR, No hay ningun articulo seleccionado para ver el detalle";
                }
            }

        }
    }
}