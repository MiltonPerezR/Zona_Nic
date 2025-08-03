<%@ Page Title="ZonaNic - Películas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ZonaNicaragua.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
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

            <!-- Timer para refrescar el banner cada 5 minutos -->
            <asp:Timer ID="timerBanner" runat="server" Interval="15000" OnTick="timerBanner_Tick" />

            <div class="genero-titulo">Estrenos</div>
            <div class="carousel-suggestions mt-3">
                <asp:Repeater ID="rptSugerencias" runat="server" OnItemCommand="rptSugerencias_ItemCommand">
                    <ItemTemplate>
                        <div class="suggestion-card focusable" tabindex="0">
                            <div class="ribbon-year"><%#Eval("FechaEstreno") %></div>
                            <asp:LinkButton ID="lnkSugerencia" runat="server" CommandName="VerPelicula" CommandArgument='<%# Eval("Id") %>'
                                CssClass="text-decoration-none text-white" Style="display: block;">
                    <img src='<%# Eval("imagen") %>' alt='<%# Eval("Titulo") %>' />
                    <div class="p-2">
                        <div class="d-flex align-items-center gap-2 small mb-1 text-light">
                            <span class="badge bg-secondary">7+</span>
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


            <!-- Secciones de Películas -->
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
                                    <%--<div class="info-overlay">
                                        <h4><%# Eval("Peliculas.TituloPelicula") %></h4>
                                        <div class="info-details">
                                            Calidad: <%# Eval("Peliculas.Calidad") %>
                                        </div>
                                    </div>--%>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
