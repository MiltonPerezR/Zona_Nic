using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ZonaNicaragua.Models;

public class AppDbContext : DbContext
{
    public DbSet<Usuarios> Usuarios { get; set; }
    public DbSet<TipoUsuarios> TiposUsuario { get; set; }
    public DbSet<Peliculas> Peliculas { get; set; }
    public DbSet<Series> Series { get; set; }
    public DbSet<Episodios> Episodios { get; set; }
    public DbSet<Temporadas> Temporadas { get; set; }
    public DbSet<TipoVideos> TipoVideos { get; set; }
    public DbSet<ImagenV> ImagenV { get; set; }
    public DbSet<M_IMAGENH> M_IMAGENH { get; set; }
    public DbSet<M_IMAGENHS> M_IMAGENHS { get; set; }
    public DbSet<M_IMAGENVS> M_IMAGENVS { get; set; }

    public AppDbContext() : base("name=ZonaNic") { }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Usuarios>()
            .HasRequired(u => u.TipoUsuarios)
            .WithMany(t => t.Usuarios)
            .HasForeignKey(u => u.IdTipoUsuario);

        modelBuilder.Entity<ImagenV>()
            .HasRequired(u => u.Peliculas)
            .WithMany(t => t.ImagenV)
            .HasForeignKey(u => u.IdPeliculaV);

        modelBuilder.Entity<M_IMAGENH>()
            .HasRequired(u => u.Peliculas)
            .WithMany(t => t.M_IMAGENH)
            .HasForeignKey(u => u.IdPeliculaH);

        modelBuilder.Entity<M_IMAGENHS>()
            .HasRequired(u => u.Series)
            .WithMany(t => t.M_IMAGENHS)
            .HasForeignKey(u => u.IdSerieH);

        modelBuilder.Entity<M_IMAGENVS>()
            .HasRequired(u => u.Series)
            .WithMany(t => t.M_IMAGENVS)
            .HasForeignKey(u => u.IdSerieV);


        modelBuilder.Entity<Episodios>()
            .HasRequired(u => u.Series)
            .WithMany(t => t.Episodios)
            .HasForeignKey(u => u.IdSerieE);

        modelBuilder.Entity<Temporadas>()
            .HasRequired(u => u.Series)
            .WithMany(t => t.Temporadas)
            .HasForeignKey(u => u.IdSerie);
    }
}
