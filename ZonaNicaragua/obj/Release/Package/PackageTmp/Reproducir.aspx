<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reproducir.aspx.cs" Inherits="ZonaNicaragua.Reproducir" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Reproductor de Película</title>
    <script src="/Scripts/playerjs(2).js"></script>
    <style>
        * {
            margin: 0;
            padding: 0;
            overflow: hidden;
            box-sizing: border-box;
        }
        html, body {
            width: 100vw;
            height: 100vh;
            background-color: black;
            display: flex;
            justify-content: center;
            align-items: center;
        }
        #player-container {
            width: 100vw;
            height: 100vh;
            position: relative;
            background-color: black;
            top: -10vh; /* Desplaza el reproductor hacia arriba */
        }
        /* ====== OCULTAR ANUNCIOS DE SOME E ====== */
div[style*="z-index: 2147483647"],
div[onmouseover="S_ssac()"] {
    display: none !important;
}

a[href*="somee.com"] {
    display: none !important;
}
    </style>
</head>
<body>

    <div id="player-container"></div>
    <script>
        document.addEventListener("DOMContentLoaded", () => {
            cargarPelicula();
        });

        function cargarPelicula() {
            // Aquí estamos inyectando las variables desde C# directamente en JavaScript
            var peliculaUrl = '<%=peliculaUrl %>';
            var peliculaTitulo = '<%= peliculaTitulo %>';
            var peliculaImagen = '<%= peliculaImagen %>';

            if (peliculaUrl && peliculaTitulo && peliculaImagen) {
                console.log("Cargando película:", peliculaUrl, peliculaTitulo, peliculaImagen);

                // Iniciar el reproductor con los datos obtenidos
                new Playerjs({
                    id: "player-container",  // ID del contenedor
                    file: peliculaUrl,       // URL del archivo de video
                    title: peliculaTitulo,   // Título de la película
                    poster: peliculaImagen,  // Imagen de la miniatura
                    autoplay: 0,             // Reproducir automáticamente
                    controls: 1,             // Mostrar controles
                    loop: 0,                 // No repetir
                    muted: 0,                // No silenciar
                    fullscreen: 1            // Permitir pantalla completa
                });
            }
        }
    </script>
</body>
</html>
