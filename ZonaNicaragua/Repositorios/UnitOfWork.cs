using System;
using ZonaNicaragua.Models;

public interface UnitOfWork : IDisposable
{
    IUsuarioRepositorio<Peliculas> Peliculas { get; }
    IUsuarioRepositorio<Usuarios> Usuarios { get; }
    IUsuarioRepositorio<ImagenV> ImagenV { get; }
    IUsuarioRepositorio<M_IMAGENH> M_IMAGENH { get; }
    IUsuarioRepositorio<M_IMAGENHS> M_IMAGENHS { get; }
    IUsuarioRepositorio<M_IMAGENVS> M_IMAGENVS { get; }
    IUsuarioRepositorio<Series> Series { get; }
    IUsuarioRepositorio<Episodios> Episodios { get; }
    IUsuarioRepositorio<Temporadas> Temporadas { get; }
    IUsuarioRepositorio<TipoVideos> TipoVideos { get; }
    IUsuarioRepositorio<TipoUsuarios> TipoUsuarios { get; }
    int Complete();
}
