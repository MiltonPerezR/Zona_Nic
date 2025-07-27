<%@ Page Title="Información de Película" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InfoPelicula.aspx.cs" Inherits="ZonaNicaragua.InfoPelicula" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .hero {
            position: relative;
            height: 420px;
            background-size: cover;
            background-position: center;
            background-repeat: no-repeat;
            border-radius: 0 0 20px 20px;
        }

        .hero-overlay {
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background: linear-gradient(to top, rgba(0, 0, 0, 0.8), rgba(0, 0, 0, 0.1));
            border-radius: 0 0 20px 20px;
        }

        .carousel-suggestions {
            display: flex;
            gap: 1rem;
            overflow-x: auto;
            padding: 1rem 0;
        }

        .suggestion-card {
            flex: 0 0 auto;
            width: 140px;
            border-radius: 8px;
            overflow: hidden;
            background-color: #1c1c1c;
        }

        .suggestion-card img {
            width: 100%;
            height: 200px;
            object-fit: cover;
        }
    </style>

    <!-- Hero Image -->
    <header id="heroContainer" runat="server" class="hero" style="background-image: url('/ruta/a/tu/imagen.jpg');">
        <div class="hero-overlay"></div>
    </header>

    <!-- Botones -->
    <div class="container mt-3 px-4">
        <div class="d-flex gap-2 flex-wrap">
            <asp:LinkButton runat="server" CssClass="btn btn-light" ID="btnPlay" OnClick="btnPlay_Click">
                <i class="bi bi-play-fill me-1"></i> Ver ahora
            </asp:LinkButton>
            <asp:LinkButton runat="server" CssClass="btn btn-light" ID="btn2" OnClick="btn2_Click">
                <i class="bi bi-play-fill me-1"></i> Ver ahora 2
            </asp:LinkButton>
            <button class="btn btn-outline-light"><i class="bi bi-plus-lg"></i> Agregar</button>
        </div>
    </div>

    <!-- Descripcion -->
    <div class="container mt-3 px-4">
        <h1 class="fw-bold">
            <asp:Label ID="lblTitulo" runat="server" />
            <span class="text-danger">
                <asp:Label ID="lblSubtitulo" runat="server" />
            </span>
        </h1>
        <p class="text-muted mb-1">
            <asp:Label ID="lblInfoBasica" runat="server" />
        </p>
        <p>
            <asp:Label ID="lblDescripcion" runat="server" />
        </p>
    </div>

    <!-- Tabs -->
    <nav class="px-4">
        <ul class="nav mt-4" id="myTab" role="tablist">
            <li class="nav-item">
                <a class="nav-link active" data-bs-toggle="tab" href="#sugerencias" role="tab">Sugerencias</a>
            </li>
        </ul>
    </nav>

    <!-- Contenido de Tabs -->
    <div class="tab-content px-4">
        <div class="tab-pane fade show active" id="sugerencias" role="tabpanel">
            <div class="carousel-suggestions">
                <asp:Repeater ID="rptSugerencias" runat="server" OnItemCommand="rptSugerencias_ItemCommand">
                    <ItemTemplate>
                        <div class="suggestion-card">
                            <asp:LinkButton ID="lnkSugerencia" runat="server" CommandName="VerPelicula" CommandArgument='<%# Eval("Id") %>' CssClass="text-decoration-none text-white" Style="display: block;">
                                <img src='<%# Eval("imagen") %>' alt='<%# Eval("TituloPelicula") %>' />
                                <div class="p-2">
                                    <div class="d-flex align-items-center gap-2 text-muted small mb-1">
                                        <span class="badge bg-secondary">7+</span>
                                        <span><%# Eval("FechaEstreno") %></span>
                                        <span class="text-white"><%# Eval("Calidad") %></span>
                                        <span class="text-white"><i class="bi bi-earbuds"></i></span>
                                    </div>
                                    <h6 class="text-truncate"><%# Eval("TituloPelicula") %></h6>
                                </div>
                            </asp:LinkButton>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>

<script>
    // Función para interpolar (transicionar) entre dos colores
    function interpolateColor(color1, color2, factor) {
        const c1 = color1.match(/\d+/g).map(Number);
        const c2 = color2.match(/\d+/g).map(Number);
        const result = c1.map((v, i) => Math.round(v + factor * (c2[i] - v)));
        return `rgb(${result[0]}, ${result[1]}, ${result[2]})`;
    }

    window.addEventListener("scroll", () => {
        const navbar = document.querySelector(".navbar");
        const body = document.body;

        if (window.scrollY > 50) {
            navbar.classList.add("bg-dark");
        } else {
            navbar.classList.remove("bg-dark");
        }

        // 👉 Aquí defines el rango de scroll para la transición de color
        const start = 200;
        const end = 500;
        const scrollY = window.scrollY;

        let factor = 0;
        if (scrollY <= start) {
            factor = 0;
        } else if (scrollY >= end) {
            factor = 1;
        } else {
            factor = (scrollY - start) / (end - start);
        }

        // 👉 Color inicial (puedes cambiarlo por el color que quieras al inicio)
        const startColor = "rgb(30, 27, 27)"; // Café (#4b4242)

        // 👉 Color final al hacer scroll (puedes cambiarlo también si lo deseas)
        const endColor = "rgb(0, 0, 0)"; // Negro

        // Aplicar color interpolado al fondo del body
        const newColor = interpolateColor(startColor, endColor, factor);
        body.style.backgroundColor = newColor;
    });

    // 👉 Color inicial cuando se carga la página
    document.body.style.backgroundColor = "rgb(30, 27, 27)";
</script>
    </asp:Content>