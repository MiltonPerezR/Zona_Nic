<%@ Page Title="Agregar Serie" Language="C#" Async="true" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="addSerie.aspx.cs" Inherits="ZonaNicaragua.addSerie" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .card-custom {
            background-color: #1e1e2f;
            color: #ffffff;
            border: none;
            border-radius: 12px;
            box-shadow: 0 5px 15px rgba(0, 0, 0, 0.3);
        }

        .form-label {
            font-weight: 600;
        }

        .preview-img {
            max-height: 180px;
            border-radius: 10px;
            object-fit: cover;
        }

        .btn-block {
            width: 100%;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="card card-custom p-4">
            <h3 class="mb-4">📺 Agregar Nueva Serie</h3>

            <div class="row mb-4">
                <div class="col-md-6">
                    <label for="txtTitulo" class="form-label">Título de la serie</label>
                    <asp:TextBox ID="txttTitulo" runat="server" CssClass="form-control" Placeholder="Ej: Breaking Bad" />
                </div>
                <div class="col-md-6 d-flex align-items-end">
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-warning btn-block" OnClick="btnBuscar_Click" />
                </div>
            </div>

            <div class="row mb-4">
                <div class="col-md-12">
                    <label for="txtSinopsis" class="form-label">Sinopsis</label>
                    <asp:TextBox ID="txtSinopsis" TextMode="MultiLine" Rows="4" runat="server" CssClass="form-control" Placeholder="Descripción corta de la serie" />
                </div>
            </div>

            <div class="row mb-4">
                <div class="col-md-4">
                    <label for="ddlGenero" class="form-label">Género principal</label>
                    <asp:TextBox ID="genero" runat="server" CssClass="form-control" Placeholder="" />

                </div>
                <div class="col-md-4">
                    <label for="ddlTipoVideo" class="form-label">Tipo de video</label>
                    <asp:DropDownList ID="ddlTipoVideo" runat="server" CssClass="form-select"></asp:DropDownList>
                </div>
                <div class="col-md-4">
                    <label for="generosss" class="form-label">Géneros detectados</label>
                    <asp:TextBox ID="generosss" runat="server" CssClass="form-control" Enabled="false" />
                </div>
            </div>

            <div class="row mb-4">
                <div class="col-md-6">
                    <label for="tbImagenV" class="form-label">Imagen vertical (Poster)</label>
                    <asp:TextBox ID="tbImagenV" runat="server" CssClass="form-control" Placeholder="https://..." />
                    <asp:Image ID="imgPoster" runat="server" CssClass="mt-2 preview-img" />
                </div>
                <div class="col-md-6">
                    <label for="tbImagenH" class="form-label">Imagen horizontal (Fondo)</label>
                    <asp:TextBox ID="tbImagenH" runat="server" CssClass="form-control" Placeholder="https://..." />
                    <asp:Image ID="imgPosterH" runat="server" CssClass="mt-2 preview-img" />
                </div>
            </div>

            <div class="row mt-4">
                
                <div class="col-md-6 d-grid">
                    <asp:Button ID="btnCerrar" runat="server" Text="❌ Cancelar" CssClass="btn btn-danger btn-block" OnClick="btnCerrar_Click" />
                </div>
                <div class="col-md-6 d-grid">
                    <asp:Button ID="btnGuardar" runat="server" Text="💾 Guardar Serie" CssClass="btn btn-success btn-block" OnClick="btnGuardar_Click" />
                </div>
            </div>
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
