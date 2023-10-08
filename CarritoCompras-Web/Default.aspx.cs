using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net;
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
        }

        protected void btnPrevImage_Click(object sender, EventArgs e)
        {

        }
    }
}