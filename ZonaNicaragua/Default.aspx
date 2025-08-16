<%@ Page Title="ZonaNic - Películas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ZonaNicaragua.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <style>
        .ribbon-year {
            position: absolute;
            top: 10px;
            left: -5px;
            background-color: red;
            color: white;
            padding: 4px 10px;
            font-size: 0.8rem;
            font-weight: bold;
            transform: rotate(-10deg);
            box-shadow: 0 2px 6px rgba(0,0,0,0.3);
            z-index: 10;
            border-radius: 3px;
        }

        .suggestion-card {
            position: relative;
            overflow: hidden;
        }
    </style>
    <style>
        .carousel-wrapper {
            position: relative;
        }

        .carousel-btn {
            position: absolute;
            top: 50%;
            transform: translateY(-50%);
            background-color: rgba(0, 0, 0, 0.5);
            border: none;
            color: white;
            font-size: 2rem;
            padding: 5px 10px;
            cursor: pointer;
            z-index: 50;
            border-radius: 5px;
        }

            .carousel-btn.left {
                left: 5px;
            }

            .carousel-btn.right {
                right: 5px;
            }

        .carousel-suggestions, .peliculas-container {
            display: flex;
            overflow-x: auto;
            scroll-behavior: smooth;
            gap: 10px;
        }

            /* Ocultar barra de scroll */
            .carousel-suggestions::-webkit-scrollbar,
            .peliculas-container::-webkit-scrollbar {
                display: none;
            }
    </style>

    <link href="Content/css/movies.css" rel="stylesheet" type="text/css" />
    <!-- Banner Principal -->
    <asp:Label runat="server" ID="lblId" Visible="false"></asp:Label>

    <asp:UpdatePanel ID="upBanner" runat="server">
        <ContentTemplate>
            <asp:Label runat="server" ID="Label1" Visible="false"></asp:Label>
            <div id="bannerPrincipal" runat="server" class="banner-principal">
                <div class="banner-contenido">
                    <h1>
                        <asp:Label runat="server" ID="lblTitleP"></asp:Label>
                    </h1>
                    <div class="banner-botones">
                        <asp:LinkButton runat="server" ID="btnRepro" CssClass="btn btn-light" OnClick="btnRepro_Click">Reproducir</asp:LinkButton>
                        <asp:LinkButton runat="server" ID="btnInfo" CssClass="btn btn-secondary" OnClick="btnInfo_Click">Más información</asp:LinkButton>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Timer ID="timerBanner" runat="server" Interval="900000" OnTick="timerBanner_Tick" />

    <!-- Estrenos -->
    <div id="estre" runat="server" class="genero-titulo mt-3">Estrenos</div>
    <div class="carousel-wrapper">
        <button runat="server" id="btnl" type="button" class="carousel-btn left" onclick="scrollCarousel('carousel-suggestions', -600)">&#10094;</button>
        <div class="carousel-suggestions mt-3">
            <asp:Repeater ID="rptSugerencias" runat="server" OnItemCommand="rptSugerencias_ItemCommand">
                <ItemTemplate>
                    <div class="suggestion-card focusable" tabindex="0">
                        <div class="ribbon-year"><%#Eval("FechaEstreno") %></div>
                        <asp:LinkButton ID="lnkSugerencia" runat="server" CommandName="VerPelicula" CommandArgument='<%# Eval("Id") + "|" + Eval("Tipo") %>'
                            CssClass="text-decoration-none text-white" Style="display: block;">
                    <img src='<%# Eval("imagen") %>' alt='<%# Eval("Titulo") %>' />
                    <div class="p-2">
                        <div class="d-flex align-items-center gap-2 small mb-1 text-light">
                            <span class="badge bg-secondary"><%#Eval("ClasificacionEdad") %></span>
                            <span><%# Eval("Calidad") %></span>
                            <span><i class="bi bi-earbuds"></i></span>
                        </div>
                        <h6 class="text-truncate mb-0"><%# Eval("Titulo") %></h6>
                    </div>
                        </asp:LinkButton>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <button runat="server" id="btnr" type="button" class="carousel-btn right" onclick="scrollCarousel('carousel-suggestions', 600)">&#10095;</button>
    </div>

    <!-- Películas por género -->
    <asp:Repeater ID="rptGeneros" runat="server">
        <ItemTemplate>
            <div class="genero-titulo"><%# Eval("Genero") %></div>
            <div class="carousel-wrapper">
                <button type="button" class="carousel-btn left" onclick="scrollCarousel('pelis<%# Eval("Genero") %>', -300)">&#10094;</button>
                <div id="pelis<%# Eval("Genero") %>" class="peliculas-container">
                    <asp:Repeater ID="rptPeliculas" runat="server" OnItemCommand="rptPeliculas_ItemCommand" DataSource='<%# Eval("Peliculas") %>'>
                        <ItemTemplate>
                            <asp:LinkButton runat="server" CssClass="pelicula-item"
                                CommandName="VerPelicula"
                                CommandArgument='<%# Eval("Peliculas.IdPelicula") + "|" + Eval("Tipo") %>'>
                            <img loading="lazy" src='<%# Eval("UrlImagenV") %>' alt='<%# Eval("Peliculas.TituloPelicula") %>' />
                            <%--<div class="info-overlay">
                                        <h4><%# Eval("Peliculas.TituloPelicula") %></h4>
                                        <div class="info-details">
                                            Calidad: rertrt
                                        </div>
                                    </div>--%>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <button type="button" class="carousel-btn right" onclick="scrollCarousel('pelis<%# Eval("Genero") %>', 600)">&#10095;</button>
            </div>
        </ItemTemplate>
    </asp:Repeater>

    <script>
        function scrollCarousel(containerId, scrollAmount) {
            const container = document.getElementById(containerId);
            if (container) {
                container.scrollBy({ left: scrollAmount, behavior: 'smooth' });
            }
        }

    </script>

    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
