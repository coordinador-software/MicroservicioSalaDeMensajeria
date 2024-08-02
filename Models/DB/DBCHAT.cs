using Microsoft.EntityFrameworkCore;

namespace ChatAPI.Models.DB
{
    public partial class DBCHAT : DbContext
    {
        public DBCHAT() { }

        public DBCHAT(DbContextOptions<DBCHAT> options) : base(options) { }

        public virtual DbSet<SISTEMAS> C_CONCEPTOS { get; set; } = null!;
        public virtual DbSet<SALAS> SALAS { get; set; } = null!;
        public virtual DbSet<PARTICIPANTES> PARTICIPANTES { get; set; } = null!;
        public virtual DbSet<MENSAJES> MENSAJES { get; set; } = null!;
        public virtual DbSet<MENSAJES_HISTORICOS> MENSAJES_HISTORICOS { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<SALAS>(entity =>
            //    entity.Property(e => e.SALA_ID).has hasDefaultValueSql();
        }

    }
}
