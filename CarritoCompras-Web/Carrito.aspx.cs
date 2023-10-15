using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CarritoCompras_Web
{
    public partial class Carrito : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Purchase cart = GetCartSession();

            if (!IsPostBack)
            {
                if (cart != null)
                {
                    LoadTable(cart);
                    ShowCart();
                    HideNoItemsLabel();
                    SummaryPanel.DataBind();
                }
                else
                {
                    HideCart();
                    ShowNoItemsLabel();
                }
            }
            else
            {
                if (cart != null)
                {
                    if (cart.Articulos.Count > 0)
                    {
                        LoadTable(cart);
                        ShowCart();
                        HideNoItemsLabel();
                    }
                    else
                    {
                        HideCart();
                        ShowNoItemsLabel();
                    }
                }
                else
                {
                    HideCart();
                    ShowNoItemsLabel();
                }
            }
        }

        private void ShowCart()
        {
            CartPanel.Visible = true;
            TableContent.Visible = true;
            SummaryPanel.Visible = true;
        }

        private void HideCart()
        {
            CartPanel.Visible = false;
            TableContent.Visible = false;
            SummaryPanel.Visible = false;
        }

        private void ShowNoItemsLabel()
        {
            NoItemsLabel.Visible = true;
        }

        private void HideNoItemsLabel()
        {
            NoItemsLabel.Visible = false;
        }
        

        private Purchase GetCartSession()
        {
            try
            {
                return Session["Cart"] as Purchase;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadTable(Purchase cart)
        {
            TableContent.DataSource = cart.Articulos;
            TableContent.DataBind();
        }

        private void RefreshSession(Purchase purchase)
        {
            Session["Cart"] = purchase;
            Session["itemsToCart"] = purchase.Articulos.Count();
        }

        protected void RemoveButton_Command(object sender, CommandEventArgs e)
        {
            int articuloId = int.Parse(((LinkButton)sender).CommandArgument);
            Purchase cart = Session["Cart"] as Purchase;
            if(cart != null)
            {
                Articulo articulo = cart.Articulos.First(art => art.Key.Id == articuloId).Key;
                if(articulo != null)
                {
                    cart.Articulos.Remove(articulo);
                }
            }

            LoadTable(cart); 

            if(cart.Articulos.Count < 1)
            {
                HideCart();
                ShowNoItemsLabel();
            }

            RefreshSession(cart);
        }

        protected decimal GetTotalAmount()
        {
            Purchase purchase = GetCartSession();

            return purchase.TotalAmount;
        }

        protected int GetTotalItems()
        {
            Purchase purchase = GetCartSession();
            int totalItems = 0;

            foreach (var quantity in purchase.Articulos.Values)
            {
                totalItems += quantity;
            }

            return totalItems;
        }

        

        

        
    }
}