<%@ Page Title="ZonaNic - Películas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ZonaNicaragua.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <style>
                body {
                    /* 👉 Color de fondo al inicio (oscuro) */
                    background-color: #1e1b1b; /* Color actualizado */
                    color: #fff;
                    transition: background-color 0.5s ease;
                }


                /* Esta clase es por si quieres alternar manualmente el fondo negro */
                .body-alt-color {
                    background-color: rgb(0, 0, 0) !important; /* Este es el color al hacer scroll (negro) */
                }
            </style>

            <!-- Banner Principal -->
            <asp:Label runat="server" ID="lblId" Visible="false"></asp:Label>
            <!-- Banner Principal dinámico -->
            <div id="bannerPrincipal" runat="server" class="banner-principal">
                <div class="banner-contenido">
                    <h1>
                        <asp:Label runat="server" ID="lblTitleP"></asp:Label></h1>
                    <p>
                        <%--<asp:Label runat="server" ID="lblSinopsisP"></asp:Label>--%>
                    </p>
                    <div class="banner-botones">
                        <asp:LinkButton runat="server" ID="btnRepro" CssClass="btn btn-light" OnClick="btnRepro_Click">Reproducir</asp:LinkButton>
                        <asp:LinkButton runat="server" ID="btnInfo" CssClass="btn btn-secondary" OnClick="btnInfo_Click">Más información</asp:LinkButton>
                    </div>
                </div>
            </div>
            <!-- Repetidor de géneros -->
            <asp:Repeater ID="rptGeneros" runat="server">
                <ItemTemplate>
                    <div class="genero-titulo"><%# Eval("Genero") %></div>
                    <div class="peliculas-container">
                        <asp:Repeater ID="rptPeliculas" runat="server" OnItemCommand="rptPeliculas_ItemCommand" DataSource='<%# Eval("Peliculas") %>'>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" CssClass="pelicula-item"
                                    CommandName="VerPelicula"
                                    CommandArgument='<%# Eval("Peliculas.IdPelicula") + "|" + Eval("Tipo") %>'>
                            <img src='<%# Eval("UrlImagenV") %>' alt='<%# Eval("Peliculas.TituloPelicula") %>' />
                            <%--<div class="pelicula-hover-info">
                                <h3><%# Eval("Peliculas.TituloPelicula") %></h3>
                                <div class="pelicula-details"><%# Eval("Peliculas.Generos.Genero") %></div>
                            </div>--%>
                        </asp:LinkButton>

                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.5/font/bootstrap-icons.js"></script>
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
