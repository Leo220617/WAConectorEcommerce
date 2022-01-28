namespace WATickets.Models.Cliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Clientes
    {
        public int id { get; set; }

        [StringLength(15)]
        public string CardCode { get; set; }

        [StringLength(100)]
        public string CardName { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(20)]
        public string Telefono { get; set; }

        [StringLength(32)]
        public string Cedula { get; set; }

        [StringLength(500)]
        public string Direccion { get; set; }

        public string ListaPrecio { get; set; }
    }
}
