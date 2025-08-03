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
            top: -10vh;
        }

        /* Botones accesibles con control remoto */
        .control-remote {
            position: absolute;
            top: 10px;
            left: 10px;
            z-index: 10000;
            display: flex;
            gap: 10px;
        }
        .control-remote button {
            padding: 10px 20px;
            font-size: 16px;
            background-color: #f39c12;
            color: white;
            border: none;
            border-radius: 8px;
            cursor: pointer;
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

    <div id="player-container"></div>

    <script>
        document.addEventListener("DOMContentLoaded", () => {
            cargarPelicula();
            setTimeout(updateFocusableControls, 500); // Asegura que se carguen todos
        });

        function cargarPelicula() {
            var peliculaUrl = '<%=peliculaUrl %>';
            var peliculaTitulo = '<%= peliculaTitulo %>';
            var peliculaImagen = '<%= peliculaImagen %>';

            if (peliculaUrl && peliculaTitulo && peliculaImagen) {
                new Playerjs({
                    id: "player-container",
                    file: peliculaUrl,
                    title: peliculaTitulo,
                    poster: peliculaImagen,
                    autoplay: 0,
                    controls: 1,
                    loop: 0,
                    muted: 0,
                    fullscreen: 1
                });
            }
        }

        // === Navegación con flechas y ENTER ===
        let focusables = [];
        let currentIndex = 0;

        function updateFocusableControls() {
            focusables = Array.from(document.querySelectorAll(".focusable"));
            focusables.forEach(el => el.setAttribute("tabindex", "0"));
            focusables[currentIndex]?.focus();
        }

        function moveFocus(dir) {
            if (!focusables.length) return;
            const current = focusables[currentIndex];
            const rect = current.getBoundingClientRect();
            let bestIndex = currentIndex;
            let bestScore = Infinity;

            for (let i = 0; i < focusables.length; i++) {
                if (i === currentIndex) continue;
                const target = focusables[i];
                const tRect = target.getBoundingClientRect();
                const dx = tRect.left - rect.left;
                const dy = tRect.top - rect.top;

                if (dir === "up" && dy >= 0) continue;
                if (dir === "down" && dy <= 0) continue;
                if (dir === "left" && dx >= 0) continue;
                if (dir === "right" && dx <= 0) continue;

                const dist = dx * dx + dy * dy;
                if (dist < bestScore) {
                    bestScore = dist;
                    bestIndex = i;
                }
            }

            currentIndex = bestIndex;
            focusables[currentIndex].focus();
        }

        document.addEventListener("keydown", e => {
            switch (e.key) {
                case "ArrowUp":
                    moveFocus("up");
                    e.preventDefault();
                    break;
                case "ArrowDown":
                    moveFocus("down");
                    e.preventDefault();
                    break;
                case "ArrowLeft":
                    moveFocus("left");
                    e.preventDefault();
                    break;
                case "ArrowRight":
                    moveFocus("right");
                    e.preventDefault();
                    break;
                case "Enter":
                    focusables[currentIndex]?.click();
                    e.preventDefault();
                    break;
            }
        });
    </script>
</body>
</html>
