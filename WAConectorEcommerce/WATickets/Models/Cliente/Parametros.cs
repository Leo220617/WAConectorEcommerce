 

namespace WATickets.Models.Cliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("Parametros")]
    public partial class Parametros
    {
        public int id { get; set; }
        public int Series { get; set; }
        public string SQLCliente { get; set; }
        public string SQLInventario { get; set; }
        public int SeriesClientes { get; set; }
        public int GroupCodeClientes { get; set; }
    }
}