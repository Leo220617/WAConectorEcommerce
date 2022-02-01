 

namespace WATickets.Models.Cliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    [Table("Categorias")]
    public partial class Categorias
    {
        public int id { get; set; }
        public int idSAP { get; set; }
        public string Nombre { get; set; }
    }
}