using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Purchase
    {
        public int Oid { get; set; }
        public Dictionary<Articulo, int> Articulos { get; set; }

        public Purchase()
        {
            Articulos = new Dictionary<Articulo, int>();
        }

        public Decimal TotalAmount
        {
            get
            {
                decimal total = 0;
                foreach (var item in Articulos)
                {
                    total += item.Key.Precio * item.Value;
                }
                return total;
            }
        }
    }

}
