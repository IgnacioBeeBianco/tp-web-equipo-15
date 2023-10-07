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
    public partial class Default : System.Web.UI.Page
    {
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
            List<Articulo> listaArticulos = negocio.listar();

            // Puedes usar un control Repeater para mostrar los artículos en tu página.
            rptArticulos.DataSource = listaArticulos;
            rptArticulos.DataBind();
        }

    }
}