/*create database ZONANIC;*/

CREATE TABLE M_TIPO_USUARIO(
	IdTipoUsuario INT PRIMARY KEY IDENTITY NOT NULL,
	TipoUsuario VARCHAR(100) NOT NULL
);

CREATE TABLE M_USUARIO (
    IdUsuario INT IDENTITY PRIMARY KEY NOT NULL,
    Usuario VARCHAR(100) NOT NULL,
    Clave VARCHAR(250) NOT NULL,
    Activo BIT NOT NULL,
	Fecha DATETIME NOT NULL DEFAULT GETDATE(),
    IdTipoUsuario INT NOT NULL REFERENCES M_TIPO_USUARIO(IdTipoUsuario)
);

insert into M_TIPO_USUARIO(TipoUsuario)VALUES
('SUPER ADMINISTRADOR'),
('ADMINISTRADOR'),
('GESTOR'),
('COMUN');

insert into M_USUARIO(Usuario, Clave, Activo, IdTipoUsuario)VALUES
('Milton dev', '123', 1, 1);


--M_PELICULAS
CREATE TABLE M_PUNTUACION_PELICULA(
	IdPuntuacionPelicula INT NOT NULL PRIMARY KEY IDENTITY,
	PuntajePelicula INT NOT NULL,
	IdUsuario int not null references M_USUARIO(IdUsuario),
	IdSerie int not null references M_SERIE(IdSerie)
);

CREATE TABLE M_PUNTUACION_SERIE(
	IdPuntuacionSerie INT NOT NULL PRIMARY KEY IDENTITY,
	PuntajeSerie INT NOT NULL,
	IdUsuario int not null references M_USUARIO(IdUsuario),
	IdSerie int not null references M_SERIE(IdSerie)
);


CREATE TABLE M_TIPO_VIDEO(
	IdTipoVideo INT NOT NULL PRIMARY KEY IDENTITY,
	TipoVideo Varchar(150) not null
);
create table M_GENERO(
	IdGenero int not null identity primary key,
	Genero varchar(100) not null
);
insert into M_GENERO(Genero) values
('Acción'),
('Romance'),
('Terror'),
('Música'),
('Animación'),
('Infantil'),
('Historia');

CREATE TABLE M_PELICULA(
	IdPelicula INT PRIMARY KEY IDENTITY NOT NULL,
	UrlPelicula VARCHAR(MAX) NOT NULL,
	PeliculaAndroid VARCHAR(100) NOT NULL,
	TituloPelicula VARCHAR(250) NOT NULL,
	SinopsisPelicula VARCHAR(500) NOT NULL,
	TiempoPelicula VARCHAR(100) NOT NULL,
	Fecha DATETIME NOT NULL DEFAULT GETDATE(),
	IdGenero int references M_GENERO(IdGenero) NOT NULL,
	IdPuntuacionPelicula INT NOT NULL REFERENCES M_PUNTUACION_PELICULA(IdPuntuacionPelicula),
);


CREATE TABLE M_IMAGENV(
	IdImagenV int primary key Identity not null,
	UrlImagenV varchar(max) not null,
	EstadoImagenV bit not null,
	IdPeliculaV int References M_PELICULA(IdPelicula)
);
CREATE TABLE M_IMAGENH(
	IdImagenH int primary key Identity not null,
	UrlImagenH varchar(max) not null,
	EstadoImagenH bit not null,
	IdPeliculaH int References M_PELICULA(IdPelicula)
);
CREATE TABLE M_SERIE(
	IdSerie INT PRIMARY KEY IDENTITY NOT NULL,
	TituloSerie VARCHAR(250) NOT NULL,
	SinopsisSerie VARCHAR(500) NOT NULL,
	Fecha DATETIME NOT NULL DEFAULT GETDATE(),
	IdTipoVideo INT NOT NULL REFERENCES M_TIPO_VIDEO(IdTipoVideo),
	IdGenero int references M_GENERO(IdGenero) NOT NULL,
	IdEpisodioQuedo int not null
);

CREATE TABLE M_IMAGENVS(
	IdImagenVS int primary key Identity not null,
	UrlImagenVS varchar(max) not null,
	EstadoImagenVS bit not null,
	IdSerieV int References M_SERIE(IdSerie)
);
CREATE TABLE M_IMAGENHS(
	IdImagenHS int primary key Identity not null,
	UrlImagenHS varchar(max) not null,
	EstadoImagenHS bit not null,
	IdSerieH int References M_SERIE(IdSerie)
);
CREATE TABLE M_ELENCOP(
	IdElencoP INT NOT NULL PRIMARY KEY IDENTITY,
	imagenAutorP VARCHAR(MAX) NOT NULL,
	PersonajeAutorP VARCHAR(250) NOT NULL,
	IdPelicula INT REFERENCES M_PELICULA(IdPelicula)
);
CREATE TABLE M_ELENCOS(
	IdElencoS INT NOT NULL PRIMARY KEY IDENTITY,
	imagenAutorS VARCHAR(MAX) NOT NULL,
	PersonajeAutorS VARCHAR(250) NOT NULL,
	IdSerie INT REFERENCES M_SERIE(IdSerie)
);

CREATE TABLE Temporadas (
    IdTemporada INT PRIMARY KEY IDENTITY NOT NULL,
    NumeroTemporada INT NOT NULL,
    NombreTemporada VARCHAR(100) NOT NULL,
    IdSerie INT NOT NULL REFERENCES M_SERIE(IdSerie)
);

CREATE TABLE M_EPISODIOS(
	IdEpisodio int not null identity primary key,
	TituloEpisodio varchar(100) not null,
	Descripcion varchar(500) not null,
	NumeroEpisodio int not null,
	Miniatura varchar(max) not null,
	UrlVideo varchar(max) not null,
	TiempoEpisodio varchar(100) not null,
	IdTemporadaE int not null REFERENCES Temporadas(IdTemporada),
	IdSerieE int references M_SERIE(IdSerie),
);