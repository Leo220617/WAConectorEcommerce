namespace WATickets.Models.Cliente
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelCliente : DbContext
    {
        public ModelCliente()
            : base("name=ModelCliente")
        {
        }

        public virtual DbSet<Clientes> Clientes { get; set; }
        public virtual DbSet<Inventario> Inventario { get; set; }
        public virtual DbSet<Login> Login { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<SeguridadModulos> SeguridadModulos { get; set; }
        public virtual DbSet<SeguridadRolesModulos> SeguridadRolesModulos { get; set; }
        public virtual DbSet<BitacoraErrores> BitacoraErrores { get; set; }
        public virtual DbSet<Parametros> Parametros { get; set; }
        public virtual DbSet<ConexionSAP> ConexionSAP { get; set; }
        public virtual DbSet<Impuestos> Impuestos { get; set; }
        public virtual DbSet<EncOrden> EncOrden { get; set; }
        public virtual DbSet<DetOrden> DetOrden { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clientes>()
                .Property(e => e.CardCode)
                .IsUnicode(false);

            modelBuilder.Entity<Clientes>()
                .Property(e => e.CardName)
                .IsUnicode(false);

            modelBuilder.Entity<Clientes>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Clientes>()
                .Property(e => e.Telefono)
                .IsUnicode(false);

            modelBuilder.Entity<Clientes>()
                .Property(e => e.Cedula)
                .IsUnicode(false);

            modelBuilder.Entity<Clientes>()
                .Property(e => e.Direccion)
                .IsUnicode(false);

            modelBuilder.Entity<Inventario>()
                .Property(e => e.ItemCode)
                .IsUnicode(false);

            modelBuilder.Entity<Inventario>()
                .Property(e => e.ItemName)
                .IsUnicode(false);

            modelBuilder.Entity<Inventario>()
                .Property(e => e.WhsCode)
                .IsUnicode(false);

            modelBuilder.Entity<Inventario>()
                .Property(e => e.OnHand)
                .HasPrecision(19, 6);

            modelBuilder.Entity<Inventario>()
                .Property(e => e.IsCommited)
                .HasPrecision(19, 6);

            modelBuilder.Entity<Inventario>()
                .Property(e => e.Stock)
                .HasPrecision(19, 6);

       

            modelBuilder.Entity<Inventario>()
                .Property(e => e.Precio)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Inventario>()
                .Property(e => e.Currency)
                .IsUnicode(false);

            modelBuilder.Entity<Inventario>()
                .Property(e => e.TipoCambio)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Inventario>()
                .Property(e => e.Total)
                .HasPrecision(19, 4);

       

            modelBuilder.Entity<Login>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Login>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Login>()
                .Property(e => e.Clave)
                .IsUnicode(false);

            modelBuilder.Entity<Roles>()
                .Property(e => e.NombreRol)
                .IsUnicode(false);

            modelBuilder.Entity<SeguridadModulos>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);
        }
    }
}
