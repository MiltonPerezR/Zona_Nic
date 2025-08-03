<%@ Page Title="Info Serie" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InfoSerie.aspx.cs" Inherits="ZonaNicaragua.InfoSerie" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <style>
                body {
                    background-color: #000;
                    color: white;
                    font-family: 'Segoe UI', sans-serif;
                }
                .banner-container {
                    position: relative;
                    width: 100%;
                    max-height: 580px;
                    overflow: hidden;
                    border-radius: 16px;
                    margin-bottom: 20px;
                }
                .tabs-container {
                    margin-top: 20px;
                    padding: 0 5%;
                }
                .banner-image {
                    width: 100%;
                    object-fit: cover;
                    border-radius: 16px;
                }
                .overlay-content {
                    position: absolute;
                    top: 50%;
                    left: 5%;
                    transform: translateY(-50%);
                    z-index: 2;
                    color: white;
                    max-width: 50%;
                    background: rgba(0, 0, 0, 0.7);
                    padding: 30px;
                    border-radius: 16px;
                }
                .overlay-content h1 {
                    font-size: 2.5rem;
                    font-weight: bold;
                }
                .overlay-content p {
                    font-size: 1.2rem;
                    margin-top: 15px;
                }
                .btn-disney {
                    background-color: #fff;
                    color: #000;
                    font-weight: bold;
                    border-radius: 8px;
                    padding: 10px 30px;
                    border: none;
                    margin-right: 10px;
                }
                .btn-disney-outline {
                    background-color: transparent;
                    color: #fff;
                    border: 1px solid #fff;
                    border-radius: 8px;
                    padding: 10px 30px;
                }
                .tabs {
                    display: flex;
                    gap: 30px;
                    font-weight: bold;
                    font-size: 1.1rem;
                    border-bottom: 1px solid #333;
                    padding-bottom: 10px;
                    color: #888;
                    cursor: pointer;
                }
                .tab.active {
                    color: white;
                    border-bottom: 3px solid white;
                    padding-bottom: 6px;
                }
                .tab-content {
                    display: none;
                    margin-top: 30px;
                }
                .tab-content.active-tab {
                    display: block;
                }
                .temporada-select {
                    margin-top: 20px;
                }
                .season-dropdown {
                    background-color: #222;
                    color: white;
                    padding: 8px 12px;
                    border-radius: 8px;
                    border: none;
                }
                .episodios-wrapper {
                    display: flex;
                    justify-content: flex-start;
                }
                .episodios-grid {
                    display: flex;
                    flex-wrap: wrap;
                    gap: 30px;
                    justify-content: flex-start;
                }
                .episodio-card-grid {
                    background-color: #111;
                    border-radius: 12px;
                    overflow: hidden;
                    width: 300px;
                    flex-shrink: 0;
                }
                .episodio-img-grid {
                    width: 100%;
                    height: 150px;
                    object-fit: cover;
                }
                .episodio-text {
                    padding: 15px;
                }
                .episodio-text p {
                    font-size: 0.95rem;
                    color: #ccc;
                    display: -webkit-box;
                    -webkit-line-clamp: 2;
                    -webkit-box-orient: vertical;
                    overflow: hidden;
                    text-overflow: ellipsis;
                }
                .duracion {
                    font-size: 0.85rem;
                    color: #aaa;
                    display: block;
                    margin-top: 5px;
                }
                @media screen and (max-width: 768px) {
                    .episodios-grid {
                        flex-direction: column;
                        gap: 16px;
                    }
                    .episodio-card-grid {
                        display: flex;
                        flex-direction: row;
                        width: 100%;
                        border-radius: 12px;
                        overflow: hidden;
                        background-color: #111;
                    }
                    .episodio-img-grid {
                        width: 130px;
                        height: 100%;
                        object-fit: cover;
                        flex-shrink: 0;
                    }
                    .episodio-text {
                        flex: 1;
                        padding: 10px;
                        display: flex;
                        flex-direction: column;
                        justify-content: center;
                    }
                    .episodio-text strong {
                        font-size: 1rem;
                    }
                    .episodio-text p {
                        font-size: 0.9rem;
                        color: #ccc;
                        margin-top: 4px;
                    }
                    .duracion {
                        font-size: 0.8rem;
                        color: #aaa;
                        margin-top: 6px;
                    }
                }
                @media screen and (min-width: 768px) {
                    .banner-container {
                        position: relative;
                        width: 60%;
                        max-height: 580px;
                        overflow: hidden;
                        border-radius: 16px;
                        margin: 0 auto 20px auto;
                    }
                    .tabs {
                        justify-content: center;
                        margin-top: 20px;
                        padding: 0 5%;
                    }
                }
                .episodio-card-grid-link {
                    display: block;
                    text-decoration: none;
                    color: inherit;
                }
                .episodio-card-grid-link:hover .episodio-card-grid {
                    background-color: #222;
                    cursor: pointer;
                    transform: scale(1.02);
                    transition: all 0.2s ease;
                }
                
                /* Estilos para foco visible en TV remoto */
                .focusable {
                    outline: none;
                }
                
            </style>

            <!-- Banner Principal -->
            <div class="banner-container">
                <asp:Image ID="imagePrincipal" runat="server" CssClass="banner-image" />
            </div>

            <!-- Ahora fuera del banner -->
            <div class="serie-info-container">
                <div class="serie-info-content">
                    <h1>
                        <asp:Label ID="lblTitle" runat="server" CssClass="focusable" tabindex="0" />
                    </h1>
                    <asp:Label runat="server" ID="lblIdTipos" Style="display: none;" />
                    <p>
                        <asp:Label ID="Sinopsis" runat="server" CssClass="focusable" tabindex="0" />
                    </p>
                    <div class="action-buttons">
                        <asp:Button ID="btnVerAhora" runat="server" CssClass="btn-disney focusable" Text="CONTINUAR" OnClick="btnVerAhora_Click" TabIndex="0" />
                        <asp:Button ID="btnLista" runat="server" CssClass="btn-disney-outline focusable" Text="Mi Lista" OnClick="btnLista_Click" TabIndex="0" />
                    </div>
                </div>
            </div>

            <!-- Sección de pestañas -->
            <div class="tabs-container">
                <span class="tab active focusable" tabindex="0" onclick="mostrarTab('episodios')">EPISODIOS</span>

                <!-- Contenido: EPISODIOS -->
                <div id="episodios" class="tab-content active-tab">
                    <div class="temporada-select">
                        Temporada: 
                        <asp:DropDownList ID="ddlTemporadas" runat="server" AutoPostBack="true" CssClass="season-dropdown focusable" OnSelectedIndexChanged="ddlTemporadas_SelectedIndexChanged" TabIndex="0" />
                    </div>

                    <div class="episodios-wrapper contain mt-4 mb-5">
                        <div class="episodios-grid">
                            <asp:Repeater ID="rptEpisodios" runat="server" OnItemCommand="rptEpisodios_ItemCommand">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" CommandName="VerEpisodio" CommandArgument='<%# Eval("IdEpisodio") %>' CssClass="episodio-card-grid-link focusable" TabIndex="0">
                                        <div class="episodio-card-grid">
                                            <img src='<%# Eval("ImagenUrl") %>' alt="Episodio" class="episodio-img-grid" />
                                            <div class="episodio-text">
                                                <strong><%# Eval("Numero") %>. <%# Eval("Titulo") %></strong>
                                                <p><%# Eval("Descripcion") %></p>
                                                <span class="duracion">(<%# Eval("Duracion") %>)</span>
                                            </div>
                                        </div>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>

                <!-- Contenido: SUGERENCIAS -->
                <div id="sugerencias" class="tab-content">
                    <p style="padding: 30px;">Aquí irán las sugerencias relacionadas con la serie.</p>
                </div>

                <!-- Contenido: DETALLES -->
                <div id="detalles" class="tab-content">
                    <p style="padding: 30px;">Aquí puedes mostrar detalles técnicos, actores, producción, etc.</p>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script>
        function mostrarTab(id) {
            document.querySelectorAll('.tab').forEach(tab => tab.classList.remove('active'));
            document.querySelectorAll('.tab-content').forEach(content => content.classList.remove('active-tab'));
            document.querySelector(`.tab[onclick*="${id}"]`).classList.add('active');
            document.getElementById(id).classList.add('active-tab');
            document.getElementById(id).scrollIntoView({ behavior: 'smooth' });
        }
    </script>

   
</asp:Content>
