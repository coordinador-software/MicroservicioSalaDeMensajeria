using Microsoft.EntityFrameworkCore;

namespace ChatAPI.Models.DB
{
    public partial class DBCHAT : DbContext
    {
        public DBCHAT() { }

        public DBCHAT(DbContextOptions<DBCHAT> options) : base(options) { }

        public virtual DbSet<SISTEMAS> SISTEMAS { get; set; } = null!;
        public virtual DbSet<SALAS> SALAS { get; set; } = null!;
        public virtual DbSet<PARTICIPANTES> PARTICIPANTES { get; set; } = null!;
        public virtual DbSet<MENSAJES> MENSAJES { get; set; } = null!;
        public virtual DbSet<MENSAJES_HISTORICOS> MENSAJES_HISTORICOS { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SISTEMAS>(entity =>
            {
                entity.HasKey(e => e.SISTEMA_ID);

                entity.ToTable("SISTEMAS");

                entity.Property(e => e.SISTEMA_ID)
                   .HasColumnName("SISTEMA_ID")
                   .HasDefaultValueSql("(newid())");

                entity.Property(e => e.NOMBRE_SISTEMA)
                   .HasColumnName("NOMBRE_SISTEMA")
                   .IsRequired();

                entity.Property(e => e.API_KEY)
                  .HasColumnName("API_KEY")
                  .IsRequired();

                entity.Property(e => e.FECHA_REGISTRO)
                   .HasPrecision(0)
                   .HasColumnName("FECHA_REGISTRO");

                entity.Property(e => e.ELIMINAR_SALAS)
                    .HasColumnName("ELIMINAR_SALAS");

                entity.Property(e => e.ELIMINAR_MENSAJES)
                    .HasColumnName("ELIMINAR_MENSAJES");

                entity.Property(e => e.ELIMINAR_ARCHIVOS)
                    .HasColumnName("ELIMINAR_ARCHIVOS");

                entity.Property(e => e.ELIMINADO)
                    .HasColumnName("ELIMINADO");

                entity.HasQueryFilter(s => !s.ELIMINADO);

            });
            modelBuilder.Entity<SALAS>(entity =>
            {
                entity.HasKey(e => e.SALA_ID);

                entity.ToTable("SALAS");

                entity.Property(e => e.SALA_ID)
                    .HasColumnName("SALA_ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.SISTEMA_ID).HasColumnName("SISTEMA_ID");

                entity.Property(e => e.NOMBRE_SALA)
                    .HasColumnName("NOMBRE_SALA")
                    .IsRequired();

                entity.Property(e => e.DESCRIPCION_SALA)
                    .HasColumnName("DESCRIPCION_SALA")
                    .IsRequired(false);

                entity.Property(e => e.ESTATUS)
                    .HasColumnName("ESTATUS")
                    .IsRequired();

                entity.Property(e => e.FECHA_REGISTRO)
                    .HasColumnName("FECHA_REGISTRO")
                    .IsRequired();

                entity.Property(e => e.FECHA_MODIFICACION)
                    .HasColumnName("FECHA_MODIFICACION")
                    .IsRequired(false);

                entity.Property(e => e.USUARIO_REGISTRO)
                    .HasColumnName("USUARIO_REGISTRO")
                    .IsRequired();

                entity.Property(e => e.USUARIO_MODIFICACION)
                    .HasColumnName("USUARIO_MODIFICACION")
                    .IsRequired(false);

                entity.Property(e => e.ELIMINADO)
                   .HasColumnName("ELIMINADO");

                entity.HasOne(e => e.SISTEMA)
                    .WithMany(e => e.SALAS)
                    .HasForeignKey(x => x.SISTEMA_ID)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_SALAS_SISTEMAS");

                entity.HasQueryFilter(s => !s.ELIMINADO);
            });

            modelBuilder.Entity<PARTICIPANTES>(entity =>
            {
                entity.HasKey(e => e.PARTICIPANTE_ID);

                entity.ToTable("PARTICIPANTES");

                entity.Property(e => e.PARTICIPANTE_ID)
                   .HasColumnName("PARTICIPANTE_ID")
                   .HasDefaultValueSql("(newid())");

                entity.Property(e => e.SALA_ID).HasColumnName("SALA_ID");

                entity.Property(e => e.USUARIO_SEG_ID).HasColumnName("USUARIO_SEG_ID");

                entity.Property(e => e.FECHA_REGISTRO)
                   .HasColumnName("FECHA_REGISTRO")
                   .IsRequired();

                entity.Property(e => e.ELIMINADO)
                   .HasColumnName("ELIMINADO");

                entity.HasOne(e => e.SALA)
                    .WithMany(e => e.PARTICIPANTES)
                    .HasForeignKey(x => x.SALA_ID)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_PARTICIPANTES_SALAS");

                entity.HasQueryFilter(p => !p.ELIMINADO);
            });

            modelBuilder.Entity<MENSAJES>(entity =>
            {
                entity.HasKey(e => e.MENSAJE_ID);

                entity.ToTable("MENSAJES");

                entity.Property(e => e.MENSAJE_ID)
                   .HasColumnName("MENSAJE_ID")
                   .HasDefaultValueSql("(newid())");

                entity.Property(e => e.SALA_ID).HasColumnName("SALA_ID");

                entity.Property(e => e.USUARIO_SEG_ID).HasColumnName("USUARIO_SEG_ID");

                entity.Property(e => e.MENSAJE)
                    .HasColumnName("MENSAJE")
                    .IsRequired();

                entity.Property(e => e.FECHA_REGISTRO)
                       .HasColumnName("FECHA_REGISTRO")
                       .IsRequired();

                entity.Property(e => e.TIPO_ARCHIVO)
                    .HasColumnName("TIPO_ARCHIVO")
                    .IsRequired();

                entity.Property(e => e.ELIMINADO)
                   .HasColumnName("ELIMINADO");

                entity.HasOne(e => e.SALA)
                    .WithMany(e => e.MENSAJES)
                    .HasForeignKey(x => x.SALA_ID)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_MENSAJES_SALAS");

                entity.HasQueryFilter(m => !m.ELIMINADO);
            });

            modelBuilder.Entity<MENSAJES_HISTORICOS>(entity =>
            {
                entity.HasKey(e => e.MENSAJE_ID);

                entity.ToTable("MENSAJES_HISTORICOS");

                entity.Property(e => e.MENSAJE_ID)
                   .HasColumnName("MENSAJE_ID")
                   .HasDefaultValueSql("(newid())");

                entity.Property(e => e.SALA_ID).HasColumnName("SALA_ID");

                entity.Property(e => e.USUARIO_SEG_ID).HasColumnName("USUARIO_SEG_ID");

                entity.Property(e => e.MENSAJE)
                    .HasColumnName("MENSAJE")
                    .IsRequired();

                entity.Property(e => e.FECHA_REGISTRO)
                       .HasColumnName("FECHA_REGISTRO")
                       .IsRequired();

                entity.Property(e => e.TIPO_ARCHIVO)
                    .HasColumnName("TIPO_ARCHIVO")
                    .IsRequired();

                entity.Property(e => e.ELIMINADO)
                   .HasColumnName("ELIMINADO");

                entity.HasOne(e => e.SALA)
                    .WithMany(e => e.MENSAJES_HISTORICOS)
                    .HasForeignKey(x => x.SALA_ID)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_MENSAJES_HISTORICOS_SALAS");

                entity.HasQueryFilter(m => !m.ELIMINADO);
            });
        }

    }
}
