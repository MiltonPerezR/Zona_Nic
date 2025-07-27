<%@ Page Title="Agregar Episodio" Language="C#" Async="true" MasterPageFile="~/admin.Master" AutoEventWireup="true" CodeBehind="addEpisodios.aspx.cs" Inherits="ZonaNicaragua.addEpisodios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .admin-card {
            background: #1e1e2f;
            color: #fff;
            border-radius: 20px;
            padding: 2rem;
            box-shadow: 0 0 25px rgba(0, 0, 0, 0.3);
            display: flex;
            gap: 2rem;
            min-height: 600px;
        }

        .left-panel {
            flex: 1;
            display: flex;
            flex-direction: column;
            gap: 1.5rem;
        }

        .right-panel {
            width: 40%;
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .preview-img {
            max-height: 100%;
            width: auto;
            object-fit: contain;
            border-radius: 15px;
            border: 2px solid #555;
            box-shadow: 0 0 15px rgba(0, 0, 0, 0.5);
            max-height: 550px;
        }

        .form-label {
            font-weight: 600;
            color: #f1f1f1;
        }

        .form-control,
        .form-select {
            background-color: #2c2c3e;
            color: #fff;
            border: 1px solid #444;
            border-radius: 6px;
        }

        .form-control::placeholder {
            color: #999;
        }

        .btn {
            border-radius: 10px;
            font-weight: 500;
        }

        .btn-info {
            background-color: #00d4ff;
            color: #000;
        }

        .btn-success {
            background-color: #28a745;
        }

        .btn-danger {
            background-color: #dc3545;
        }

        @media (max-width: 768px) {
            .admin-card {
                flex-direction: column;
                min-height: auto;
            }

            .right-panel {
                width: 100%;
                max-height: 300px;
                margin-top: 1rem;
            }

            .preview-img {
                width: 100%;
                max-height: 100%;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

        <div class="admin-card">
            <!-- IZQUIERDA - FORMULARIO -->
            <div class="left-panel">
                <h3 class="text-info text-center mb-3">🎬 Agregar Episodio</h3>
                <div>
                    <label class="form-label" for="<%= ddlSerie.ClientID %>">Serie</label>
                    <asp:DropDownList ID="ddlSerie" runat="server" CssClass="form-select" />
                </div>

                <div class="row g-3">
                    <div class="col-md-6">
                        <label class="form-label" for="<%= tbNumeroTemporada.ClientID %>">Número de temporada</label>
                        <asp:TextBox ID="tbNumeroTemporada" runat="server" CssClass="form-control" Placeholder="Ej: 1" />
                    </div>
                    <div class="col-md-6">
                        <label class="form-label" for="<%= lblnumeroEpisodio.ClientID %>">Número de episodio</label>
                        <asp:TextBox ID="lblnumeroEpisodio" runat="server" CssClass="form-control" Placeholder="Ej: 1" />
                    </div>
                </div>

                <asp:Button ID="btnBuscarEpisodio" runat="server" Text="🔍 Buscar desde TMDb" CssClass="btn btn-info w-100 mt-3" OnClick="btnBuscarEpisodio_Click" />

                <div>
                    <label class="form-label" for="<%= txttTitulo.ClientID %>">Título</label>
                    <asp:TextBox ID="txttTitulo" runat="server" CssClass="form-control" Placeholder="Título del episodio..." />
                </div>

                <div>
                    <label class="form-label" for="<%= txtDescripcion.ClientID %>">Descripción</label>
                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" MaxLength="300" Placeholder="Descripción del episodio..." />
                </div>
                
                <div>
                    <label class="form-label" for="<%= tbMiniatura.ClientID %>">Miniatura horizontal</label>
                    <asp:TextBox ID="tbMiniatura" runat="server" CssClass="form-control" Placeholder="https://image.tmdb.org/..." />
                </div>

                <div>
                    <label class="form-label" for="<%= tnTiempoEpi.ClientID %>">Duración del episodio</label>
                    <asp:TextBox ID="tnTiempoEpi" runat="server" CssClass="form-control" Placeholder="Ej: 45 min" />
                </div>
                <div>
                    <label class="form-label" for="<%= lblUrl.ClientID %>">URL del episodio</label>
                    <asp:TextBox ID="lblUrl" runat="server" CssClass="form-control" Placeholder="https://..." />
                </div>

                <div class="d-flex justify-content-between mt-4">
                    <asp:Button ID="btnCerrar" runat="server" Text="⛔ Cancelar" CssClass="btn btn-danger px-4" OnClick="btnCerrar_Click1"/>
                    <asp:Button ID="btnGuardar" runat="server" Text="💾 Guardar" CssClass="btn btn-success px-4" Onclick="btnGuardar_Click"/>
                </div>

            </div>

            <!-- DERECHA - IMAGEN -->
            <div class="right-panel">
                <asp:Image ID="imgPreview" runat="server" CssClass="preview-img" AlternateText="Vista previa de la miniatura" />
            </div>
        </div>

    <script type="text/javascript">
        function mostrarAlerta(titulo, mensaje, tipo) {
            Swal.fire({
                title: titulo,
                text: mensaje,
                icon: tipo,
                confirmButtonText: 'Aceptar'
            });
        }
    </script>
</asp:Content>
