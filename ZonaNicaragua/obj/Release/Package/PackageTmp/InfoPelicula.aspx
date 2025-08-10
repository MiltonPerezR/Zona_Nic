<%@ Page Title="Información de Película" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InfoPelicula.aspx.cs" Inherits="ZonaNicaragua.InfoPelicula" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        body {
            background-color: rgb(30, 27, 27);
        }

        .hero {
            position: relative;
            width: 100vw;
            margin-left: calc(-50vw + 50%);
            height: 500px;
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
            scrollbar-width: none;
        }

        .carousel-suggestions::-webkit-scrollbar {
            display: none;
        }

        .suggestion-card {
            flex: 0 0 auto;
            width: 150px;
            border-radius: 10px;
            overflow: hidden;
            background-color: #1c1c1c;
            transition: transform 0.3s;
        }

        .suggestion-card img {
            width: 100%;
            height: 210px;
            object-fit: cover;
        }

        .suggestion-card .p-2 {
            background: rgba(0, 0, 0, 0.6);
            padding: 0.5rem;
        }

        .suggestion-card h6,
        .suggestion-card span {
            text-shadow: 0 1px 2px rgba(0, 0, 0, 0.8);
        }

        .nav-tabs .nav-link {
            color: white;
        }

        .nav-tabs .nav-link.active {
            background-color: transparent;
            border-bottom: 2px solid red;
            font-weight: bold;
        }

        /* 👉 Estilos para navegación con control remoto */
        .focusable:focus {
            outline: 3px solid #f39c12;
            border-radius: 10px;
            transition: outline 0.2s ease-in-out;
        }

        .suggestion-card:focus {
            transform: scale(1.05);
        }
    </style>

    <!-- Hero -->
    <header id="heroContainer" runat="server" class="hero">
        <div class="hero-overlay"></div>
    </header>

    <!-- Botones -->
    <div class="container mt-4 px-4">
        <div class="d-flex flex-wrap gap-2">
            <asp:LinkButton runat="server" CssClass="btn btn-light focusable" ID="btnPlay" OnClick="btnPlay_Click" TabIndex="0">
                <i class="bi bi-play-fill me-1"></i> Ver ahora
            </asp:LinkButton>
            <asp:LinkButton runat="server" CssClass="btn btn-light focusable" ID="btn2" OnClick="btn2_Click" TabIndex="0">
                <i class="bi bi-play-fill me-1"></i> Ver ahora 2
            </asp:LinkButton>
            <asp:LinkButton runat="server" CssClass="btn btn-outline-light focusable" ID="btnAgregar" TabIndex="0">
                <i class="bi bi-plus-lg"></i> Agregar
            </asp:LinkButton>
        </div>
    </div>

    <!-- Información -->
    <div class="container mt-4 px-4">
        <h1 class="fw-bold text-white">
            <asp:Label ID="lblTitulo" runat="server" />
            <span class="text-danger">
                <asp:Label ID="lblSubtitulo" runat="server" />
            </span>
        </h1>
        <p class="text-muted small mb-1">
            <asp:Label ID="lblInfoBasica" runat="server" />
        </p>
        <p class="text-light">
            <asp:Label ID="lblDescripcion" runat="server" />
        </p>
    </div>

    <!-- Tabs -->
    <nav class="px-4 mt-4">
        <ul class="nav nav-tabs" id="myTab" role="tablist">
            <li class="nav-item">
                <a class="nav-link active" data-bs-toggle="tab" href="#sugerencias" role="tab">Sugerencias</a>
            </li>
        </ul>
    </nav>

    <!-- Contenido Tab -->
    <div class="tab-content px-4">
        <div class="tab-pane fade show active" id="sugerencias" role="tabpanel">
            <div class="carousel-suggestions mt-3">
                <asp:Repeater ID="rptSugerencias" runat="server" OnItemCommand="rptSugerencias_ItemCommand">
                    <ItemTemplate>
                        <div class="suggestion-card focusable" tabindex="0">
                            <asp:LinkButton ID="lnkSugerencia" runat="server" CommandName="VerPelicula" CommandArgument='<%# Eval("Id") %>'
                                CssClass="text-decoration-none text-white" Style="display: block;">
                                <img src='<%# Eval("imagen") %>' alt='<%# Eval("TituloPelicula") %>' />
                                <div class="p-2">
                                    <div class="d-flex align-items-center gap-2 small mb-1 text-light">
                                        <span class="badge bg-secondary">7+</span>
                                        <span><%# Eval("FechaEstreno") %></span>
                                        <span><%# Eval("Calidad") %></span>
                                        <span><i class="bi bi-earbuds"></i></span>
                                    </div>
                                    <h6 class="text-truncate mb-0"><%# Eval("TituloPelicula") %></h6>
                                </div>
                            </asp:LinkButton>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>

    <!-- Scripts -->
    <script>
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

            const start = 200;
            const end = 500;
            const scrollY = window.scrollY;
            let factor = scrollY <= start ? 0 : scrollY >= end ? 1 : (scrollY - start) / (end - start);

            const startColor = "rgb(30, 27, 27)";
            const endColor = "rgb(0, 0, 0)";
            body.style.backgroundColor = interpolateColor(startColor, endColor, factor);
        });


        document.addEventListener("DOMContentLoaded", () => {
            updateFocusables();
        });

        // Soporte tras postback (UpdatePanel)
        if (typeof Sys !== "undefined") {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                updateFocusables();
            });
        }
    </script>
</asp:Content>
