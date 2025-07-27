<%@ Page Title="Agregar episodios" Language="C#" MasterPageFile="~/admin.Master" AutoEventWireup="true" CodeBehind="ControlEpisodio.aspx.cs" Inherits="ZonaNicaragua.ControlEpisodio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            background-color: #121212;
            color: #ffffff;
        }

        .table-wrapper {
            overflow-x: auto;
        }

        table {
            border-radius: 12px;
            overflow: hidden;
            white-space: nowrap;
        }

        .table td, .table th {
            vertical-align: middle;
        }

        .text-truncate {
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
            max-width: 250px;
        }

        .btn {
            margin: 2px;
            transition: transform 0.2s ease-in-out;
        }

            .btn:hover {
                transform: scale(1.05);
            }

        .modal-content {
            background-color: #141414;
            color: #fff;
        }

        .modal-header,
        .modal-footer {
            border: none;
        }

        .btn-close {
            filter: invert(1);
        }

        @media (max-width: 768px) {
            .modal-lg {
                max-width: 95%;
            }
        }

        table {
            border-radius: 12px;
            overflow: hidden;
        }

        body {
            background-color: #121212;
            color: #ffffff;
        }

        .text-truncate {
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
        }

        .btn {
            margin: 2px;
            transition: transform 0.2s ease-in-out;
        }

            .btn:hover {
                transform: scale(1.1);
            }

        .netflix-modal .modal-content {
            background-color: #141414;
            color: #fff;
            border: none;
            border-radius: 0;
            overflow: hidden;
            transition: all 0.3s ease-in-out;
        }

        .netflix-modal .modal-header,
        .netflix-modal .modal-body,
        .netflix-modal .modal-footer {
            background-color: #141414;
            border: none;
        }

        .netflix-modal .btn-close {
            filter: invert(1);
        }

        .netflix-modal .btn-primary {
            background-color: #e50914;
            border: none;
        }

            .netflix-modal .btn-primary:hover {
                background-color: #f40612;
            }

        .netflix-modal .btn-secondary {
            background-color: #333;
            border: none;
        }

            .netflix-modal .btn-secondary:hover {
                background-color: #444;
            }

        .modal-title {
            font-size: 1.5rem;
            font-weight: bold;
        }

        .netflix-modal .modal-dialog {
            max-width: 800px;
            margin: 2rem auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label runat="server" ID="lblId" Visible="false"></asp:Label>
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container mt-5">
                <h2 class="mb-4">Listado de episodios</h2>

                <div class="d-flex justify-content-end mb-3">
                    <a href="ControlSerie.aspx" class="btn btn-primary bi bi-movies-add">Series</a>
                    <a href="addEpisodios.aspx" class="btn btn-success bi bi-movies-add">Agregar Episodios</a>
                </div>

                <div class="table-responsive">
                    <table id="tablaPeliculas" class="table table-striped table-hover table-bordered align-middle">
                        <thead class="table-dark text-center">
                            <tr>
                                <th>ID</th>
                                <th>Título</th>
                                <th>Descripción</th>
                                <th style="display: none;">Url episodio</th>
                                <th>Episodio</th>
                                <th>Temporada</th>
                                <th>Tiempo</th>
                                <th>Serie</th>
                                <th>Acciones</th>
                            </tr>
                        </thead>
                        <tbody class="table-dark">
                            <asp:Repeater ID="gvPelicula" runat="server" OnItemCommand="gvPelicula_ItemCommand">
                                <ItemTemplate>
                                    <tr class="text-center">
                                        <td><%# Eval("IdEpisodio") %></td>
                                        <td class="fw-bold"><%# Eval("TituloEpisodio") %></td>
                                        <td class="text-start text-truncate" style="max-width: 250px;"><%# Eval("Descripcion") %></td>
                                        <td class="text-start text-truncate" id="dato_<%# Eval("IdEpisodio") %>" style="display: none; max-width: 250px;"><%# Eval("UrlVideo") %></td>
                                        <td><span class="badge bg-danger p-2"><%# Eval("NumeroEpisodio") %></span></td>
                                        <td><%# Eval("NombreTemporada") %></td>
                                        <td><%# Eval("TiempoEpisodio") %></td>
                                        <td><span class="badge bg-danger p-2"><%# Eval("TituloSerie") %></span></td>
                                        <td>
                                            <asp:LinkButton runat="server" CommandName="Visualizar" CommandArgument='<%# Eval("IdEpisodio") %>' CssClass="btn btn-primary bi bi-eye btn-sm" />
                                            <asp:LinkButton runat="server" CommandName="Editar" CommandArgument='<%# Eval("IdEpisodio") %>' CssClass="btn btn-warning btn-sm bi bi-pencil-square" />
                                            <asp:LinkButton runat="server" ID="btnEliminar" CommandName="Eliminar" CommandArgument='<%# Eval("IdEpisodio") %>' CssClass="btn btn-danger btn-sm bi bi-trash" />
                                            <button type="button" class="btn btn-sm btn-success" onclick="copiarDato('dato_<%# Eval("IdEpisodio") %>')">Copiar</button>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
                <div class="modal fade" id="modalVista" tabindex="-1" aria-labelledby="modalVistaLabel" aria-hidden="true">
                    <div class="modal-dialog modal-lg modal-dialog-centered">
                        <div class="modal-content rounded-4">
                            <div class="modal-header bg-dark text-white">
                                <asp:Label ID="Label1" runat="server" CssClass="modal-title fs-5" />
                                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                            </div>
                            <div class="modal-body">
                                <div class="row g-3">
                                    <div class="col-md-4">
                                        <label class="form-label">Título</label>
                                        <asp:TextBox ID="TituloTxt" runat="server" CssClass="form-control" ReadOnly="true" />
                                    </div>
                                    <div class="col-md-4">
                                        <label class="form-label">Descripción</label>
                                        <asp:TextBox ID="Descripción" runat="server" CssClass="form-control" ReadOnly="true" />
                                    </div>
                                    <div class="col-md-4">
                                        <label class="form-label">Duración</label>
                                        <asp:TextBox ID="duracionTxt" runat="server" CssClass="form-control" ReadOnly="true" />
                                    </div>
                                    <div class="col-md-4">
                                        <label class="form-label">Imagen horizontal</label>
                                        <asp:Image ID="Image1" runat="server" CssClass="img-fluid mt-2 rounded" Style="max-height: 200px;" />
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:LinkButton runat="server" ID="btnReproducir" CssClass="btn btn-primary" Text="Reproducir" />
                                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Modal Eliminar -->
                <div class="modal fade" id="mEliminar" tabindex="-1" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered">
                        <div class="modal-content rounded-4 border-0 shadow">
                            <div class="modal-header bg-danger text-white rounded-top-4">
                                <h5 class="modal-title">
                                    <i class="bi bi-exclamation-triangle me-2"></i>Confirmar Eliminación
                                </h5>
                                <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                            </div>
                            <div class="modal-body text-center">
                                <p class="fs-5 text-white">¿Estás seguro de eliminar esta película?</p>
                                <p class="text-muted mb-0">
                                    <asp:Label ID="peliculaTxt" CssClass="text-white" runat="server" />
                                </p>
                            </div>
                            <div class="modal-footer justify-content-center">
                                <button type="button" class="btn btn-secondary px-4" data-bs-dismiss="modal">
                                    <i class="bi bi-x-circle me-1"></i>Cancelar
                                </button>
                                <asp:LinkButton runat="server" ID="btnConfirm" OnClick="btnConfirm_Click" CssClass="btn btn-danger px-4 bi bi-trash me-1" Text="Eliminar" />
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Modal tipo Netflix -->
                <div class="modal fade netflix-modal" id="modalAcciones" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-lg">
                        <!-- modal-lg para más espacio -->
                        <div class="modal-content">
                            <div class="container mt-4">
                                <div class="card-body">
                                    <div class="row mb-3">
                                        <h2>Editar Serie</h2>
                                        <div class="col-md-4">
                                            <label for="txtTitulo" class="form-label">Título</label>
                                            <asp:TextBox ID="txttTitulo" runat="server" CssClass="form-control" Placeholder="Título..." />
                                        </div>
                                        <div class="col-md-4">
                                            <label for="" class="form-label">Url episodio</label>
                                            <asp:TextBox runat="server" ID="lblUrl" CssClass="form-control" Placeholder=""></asp:TextBox>
                                        </div>
                                        <!-- Imagen Horizontal -->
                                        <div class="col-md-4">
                                            <label for="tbImagenH" class="form-label">Imagen Horizontal</label>
                                            <asp:TextBox runat="server" ID="tbImagenH" CssClass="form-control" Placeholder="URL Imagen Horizontal" />
                                            <asp:Image runat="server" ID="previewH" CssClass="img-fluid mt-2" Style="max-height: 800px;" />
                                            <asp:Image ID="Image2" runat="server" CssClass="img-fluid mt-2 rounded" Style="max-height: 200px;" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row mb-3">
                                    <div class="row mb-3">
                                        <div class="col-md-4">
                                            <label for="" class="form-label">Tiempo de episodio</label>
                                            <asp:TextBox runat="server" ID="tnTiempoEpi" CssClass="form-control" Placeholder=""></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <label for="txtSinopsis" class="form-label">Descripción</label>
                                            <asp:TextBox ID="txtSinopsis" runat="server" CssClass="form-control"
                                                TextMode="MultiLine" Placeholder="Sinopsis..."></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="text-start mb-3">
                                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="lbUpdate_Click" />
                                    </div>

                                    <div class="text-start">
                                        <asp:Label runat="server" ID="lblConfirmacion" CssClass="text-success"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script>
        function mostrarAlerta(titulo, mensaje, tipo) {
            Swal.fire({
                title: titulo,
                text: mensaje,
                icon: tipo, // success, error, warning, info, question
                confirmButtonText: 'Aceptar'
            });
        }
        function copiarDato(idElemento) {
            var texto = document.getElementById(idElemento).innerText;
            navigator.clipboard.writeText(texto).then(function () {
                mostrarAlerta("Exito", "¡Copiado al portapapeles! " + texto, "success");
            }, function (err) {
                mostrarAlerta("Exito", "Error al copiar: " + err, "success");
            });
        }
    </script>
</asp:Content>
