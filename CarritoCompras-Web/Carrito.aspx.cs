using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CarritoCompras_Web
{
    public partial class Carrito : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Cart"] != null)
                {
                    LoadTable();
                    CartPanel.Visible = true;
                    CartTable.Visible = true;
                    NoItemsLabel.Visible = false;
                    SummaryPanel.Visible = true;
                    
                }
                else
                {
                    CartTable.Visible = false;
                    NoItemsLabel.Visible = true;
                    SummaryPanel.Visible = false;   
                }
            }
        }

        private void LoadTable()
        {
            Purchase cart = Session["Cart"] as Purchase;
            cart.Articulos.ForEach(articulo =>
            {
                TableRow row = new TableRow();

                TableCell imageCell = new TableCell();
                if (articulo.ImagenURL.Any())
                {
                    Image image = new Image();
                    image.ImageUrl = articulo.ImagenURL.First().ImagenUrl;
                    image.CssClass = "img-fluid";
                    imageCell.Controls.Add(image);
                }
                row.Cells.Add(imageCell);

                TableCell nombreCell = new TableCell();
                nombreCell.Text = articulo.Nombre;
                row.Cells.Add(nombreCell);


                TableCell precioCell = new TableCell();
                precioCell.Text = articulo.Precio.ToString("C");
                row.Cells.Add(precioCell);

                TableCell accionesCell = new TableCell();

                Button removeButton = new Button();
                removeButton.Text = "Eliminar";
                removeButton.CommandName = "Remove";
                removeButton.CommandArgument = articulo.Id.ToString();
                removeButton.Command += RemoveButton_Command;
                removeButton.CssClass = "remove-button";
                accionesCell.Controls.Add(removeButton);
                row.Cells.Add(accionesCell);

                imageCell.CssClass = "item-image w-25";
                nombreCell.CssClass = "item-name";
                precioCell.CssClass = "item-price";
                accionesCell.CssClass = "item-actions";
                removeButton.CssClass = "remove-button";

                CartTable.Rows.Add(row);

            });
        }

        protected void RemoveButton_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                int articuloId = Convert.ToInt32(e.CommandArgument);

                if (Session["Cart"] != null)
                {
                    Purchase cart = Session["Cart"] as Purchase;
                    cart.Articulos.RemoveAt(articuloId);
                    Session["Cart"] = cart;
                }
            }
        }

    }
}