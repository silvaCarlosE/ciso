using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrjIntegrado.Models
{
    public class Perda
    {
        public int id_perda { get; set; }
        public int quantidade { get; set; }
        public DateTime dataperda { get; set; }
        public int id_tipo_papel { get; set; }
        public int id_func { get; set; }
        //criar chave estrangeira para id_tipo e id_func
    }
}