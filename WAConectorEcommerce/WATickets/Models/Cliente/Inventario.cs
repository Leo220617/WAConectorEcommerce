namespace WATickets.Models.Cliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Inventario")]
    public partial class Inventario
    {
        public int id { get; set; }

        [StringLength(50)]
        public string ItemCode { get; set; }

        [StringLength(500)]
        public string ItemName { get; set; }

        [StringLength(500)]
        public string Categoria { get; set; }

        [StringLength(50)]
        public string Cabys { get; set; }

        [StringLength(2)]
        public string TipoIVA { get; set; }

        [StringLength(8)]
        public string WhsCode { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? OnHand { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? IsCommited { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Stock { get; set; }

        

        [Column(TypeName = "money")]
        public decimal? Precio { get; set; }

        [StringLength(5)]
        public string Currency { get; set; }

        [Column(TypeName = "money")]
        public decimal? TipoCambio { get; set; }

        [Column(TypeName = "money")]
        public decimal? Total { get; set; }

        public DateTime? FechaActualizacion { get; set; }

        public DateTime? FechaActPrec { get; set; }

        [StringLength(10)]
        public string ListaPrecio { get; set; }

    }
}
