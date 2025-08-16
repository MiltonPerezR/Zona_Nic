<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reproducir.aspx.cs" Inherits="ZonaNicaragua.Reproducir" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Reproductor de Película</title>
    <script src="/Scripts/playerjs(1).js"></script>
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
            /*top: -10vh;*/
        }
        

        .focusable:focus {
            outline: 2px solid #fff;
            outline-offset: 2px;
        }
        /* Ocultar anuncios Somee */
        div[style*="z-index: 2147483647"],
        div[onmouseover="S_ssac()"],
        a[href*="somee.com"] {
            display: none !important;
        }
    </style>
</head>
<body>

    <script src="/Scripts/playerjs.js"></script>

<div id="player-container"></div>

<script>
    let player;

    document.addEventListener("DOMContentLoaded", () => {
        cargarPlayer();
    });

    function cargarPlayer() {
        var tipo = '<%= tipo %>';

        if (tipo === "Pelicula") {
            var url = '<%= peliculaUrl %>';
            var titulo = '<%= peliculaTitulo %>';
            var imagen = '<%= peliculaImagen %>';
            var recomendaciones = <%= recomendacionesJson ?? "[]" %>;

            player = new Playerjs({
                id: "player-container",
                file: url,
                title: titulo,
                poster: imagen,
                autoplay: 0,
                controls: 1,
                rel: recomendaciones
            });
        }
        else if (tipo === "Serie") {
            var playlist = <%= playlistJson ?? "[]" %>;
            var recomendaciones = <%= recomendacionesJson ?? "[]" %>;

            player = new Playerjs({
                id: "player-container",
                //file: JSON.stringify(playlist),
                file: playlist,
                autoplay: 0,
                controls: 1,
                autonext: 1,
                rel: recomendaciones
            });
        }
    }
</script>

</body>
</html>
