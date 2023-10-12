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
        public List<Articulo> Articulos { get; set; }

        public Decimal amount { get; set; }
    }
}
