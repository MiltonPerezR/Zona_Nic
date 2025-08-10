<%@ Page Title="" Language="C#" MasterPageFile="~/admin.Master" AutoEventWireup="true" CodeBehind="ControlPelicula.aspx.cs" Inherits="ZonaNicaragua.ControlPelicula" %>

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
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:Label runat="server" ID="lblId" Visible="false" />
    <asp:Label runat="server" ID="peliText" Visible="false" />

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <!-- Encabezado -->
            <div class="container-fluid mt-4">
                <div class="d-flex justify-content-between align-items-center mb-3">
                    <h2 class="fw-bold">🎬 Listado de películas</h2>
                    <a href="addPeliculas.aspx" class="btn btn-success">
                        <i class="bi bi-plus-circle me-1"></i>Agregar Película
                    </a>
                </div>

                <!-- Tabla -->
                <div class="table-wrapper">
                    <table class="table table-dark table-bordered table-hover align-middle text-center" id="tablaPeliculas">
                        <thead class="table-secondary text-dark">
                            <tr>
                                <th>ID</th>
                                <th>Título</th>
                                <th>Género</th>
                                <th>Sinopsis</th>
                                <th style="display: none;">URL</th>
                                <th>Duración</th>
                                <th>Acciones</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="gvPelicula" runat="server" OnItemCommand="gvPelicula_ItemCommand">
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("IdPelicula") %></td>
                                        <td class="fw-bold"><%# Eval("TituloPelicula") %></td>
                                        <td><span class="badge bg-danger p-2"><%# Eval("Genero") %></span></td>
                                        <td class="text-start text-truncate"><%# Eval("SinopsisPelicula") %></td>
                                        <td style="display: none;" id="dato_<%# Eval("IdPelicula") %>"><%# Eval("UrlPelicula") %></td>
                                        <td><%# Eval("TiempoPelicula") %></td>
                                        <td>
                                            <asp:LinkButton runat="server" CommandName="Visualizar" CommandArgument='<%# Eval("IdPelicula") %>' CssClass="btn btn-sm btn-primary bi bi-eye" />
                                            <asp:LinkButton runat="server" CommandName="Editar" CommandArgument='<%# Eval("IdPelicula") %>' CssClass="btn btn-sm btn-warning bi bi-pencil-square" />
                                            <asp:LinkButton runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("IdPelicula") %>' CssClass="btn btn-sm btn-danger bi bi-trash" />
                                            <button type="button" class="btn btn-sm btn-success" onclick="copiarDato('dato_<%# Eval("IdPelicula") %>')"><i class="bi bi-clipboard"></i></button>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
            </div>

            <!-- Modal Vista -->
            <div class="modal fade" id="modalVista" tabindex="-1" aria-labelledby="modalVistaLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg modal-dialog-centered">
                    <div class="modal-content rounded-4">
                        <div class="modal-header bg-dark text-white">
                            <asp:Label ID="lblTituloModal" runat="server" CssClass="modal-title fs-5" />
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                        </div>
                        <div class="modal-body">
                            <div class="row g-3">
                                <div class="col-md-4">
                                    <label class="form-label">Título</label>
                                    <asp:TextBox ID="TituloTxt" runat="server" CssClass="form-control" ReadOnly="true" />
                                </div>
                                <div class="col-md-4">
                                    <label class="form-label">Sinopsis</label>
                                    <asp:TextBox ID="SinopsisTxt" runat="server" CssClass="form-control" ReadOnly="true" />
                                </div>
                                <div class="col-md-4">
                                    <label class="form-label">Estreno</label>
                                    <asp:TextBox ID="estrenoTxt" runat="server" CssClass="form-control" ReadOnly="true" />
                                </div>
                                <div class="col-md-4">
                                    <label class="form-label">Géneros</label>
                                    <asp:TextBox ID="GenerosTxt" runat="server" CssClass="form-control" ReadOnly="true" />
                                </div>
                            </div>
                            <div class="row g-3">
                                <div class="col-md-4">
                                    <label>Miniatura</label>
                                    <div></div>
                                    <asp:Image ID="mini" runat="server" CssClass="img-fluid mt-2 rounded" Style="max-height: 200px;" />
                                </div>
                                <div class="col-md-4">
                                    <label>Poster</label>
                                    <asp:Image ID="poster" runat="server" CssClass="img-fluid mt-2 rounded" Style="max-height: 200px;" />
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Modal Editar -->
            <div class="modal fade" id="modalAcciones" tabindex="-1" aria-hidden="true">
                <div class="modal-dialog modal-lg modal-dialog-centered">
                    <div class="modal-content rounded-4">
                        <div class="modal-body p-4">
                            <h4 class="mb-4">Editar Película</h4>
                            <div class="row g-3">
                                <div class="col-md-4">
                                    <label>Título</label>
                                    <asp:TextBox ID="txttTitulo" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-md-4">
                                    <label>Sinopsis</label>
                                    <asp:TextBox ID="txtSinopsis" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-md-4">
                                    <label>Duración</label>
                                    <asp:TextBox ID="tbDuracion" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-md-4">
                                    <label>Fecha de estreno</label>
                                    <asp:TextBox ID="fechaEstreno" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-md-4">
                                    <label>URL Película</label>
                                    <asp:TextBox ID="tburPelicula" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-md-4">
                                    <label>Género</label>
                                    <asp:TextBox ID="Genero" runat="server" CssClass="form-control" />
                                </div>
                                
                                <div class="col-md-4">
                                    <label>Calidad</label>
                                    <asp:TextBox ID="calidad" runat="server" CssClass="form-control" />
                                </div>
                                
                                <div class="col-md-4">
                                    <label>Imagen Vertical</label>
                                    <asp:TextBox ID="tbImagenV" runat="server" CssClass="form-control" onkeyup="previewImage(this, 'previewV')" />
                                    <asp:Image ID="previewV" runat="server" CssClass="img-fluid mt-2 rounded" Style="max-height: 200px;" />
                                </div>
                                <div class="col-md-4">
                                    <label>Imagen Horizontal</label>
                                    <asp:TextBox ID="tbImagenH" runat="server" CssClass="form-control" />
                                    <asp:Image ID="previewH" runat="server" CssClass="img-fluid mt-2 rounded" Style="max-height: 200px;" />
                                </div>
                                <div class="col-md-4">
                                    <label>Clasificación Edad</label>
                                    <asp:TextBox ID="ClasificacionEdad" runat="server" CssClass="form-control" />
                                </div>
                            </div>
                            <div class="mt-4">
                                <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-success" Text="Guardar" OnClick="btnGuardar_Click" />
                                <asp:Label ID="lblConfirmacion" runat="server" CssClass="text-success ms-3" />
                            </div>
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

        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnConfirm" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>

    <!-- Scripts Finales -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script>
        function copiarDato(idElemento) {
            const texto = document.getElementById(idElemento)?.innerText || '';
            if (texto.trim() !== '') {
                navigator.clipboard.writeText(texto)
                    .then(() => Swal.fire('¡Copiado!', texto, 'success'))
                    .catch(err => Swal.fire('Error', 'No se pudo copiar.', 'error'));
            } else {
                Swal.fire('Aviso', 'No hay dato para copiar.', 'info');
            }
        }

        function mostrarAlerta(titulo, mensaje, tipo) {
            Swal.fire({
                title: titulo,
                text: mensaje,
                icon: tipo,
                confirmButtonText: 'Aceptar'
            });
        }
    </script>
    <script type="text/javascript">
        // Se ejecuta al terminar cualquier postback con UpdatePanel
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            // Forzar cierre de cualquier modal abierto
            $('.modal').modal('hide');
            // Eliminar backdrop viejo
            $('.modal-backdrop').remove();
            // Desbloquear scroll y clics
            $('body').removeClass('modal-open').css('padding-right', '');
        });
    </script>
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        // Al comenzar petición asincrónica
        prm.add_initializeRequest(function () {
            // Mostrar spinner si tienes uno
            document.getElementById("loadingPanel")?.style?.setProperty("display", "block");
        });

        // Al terminar petición asincrónica
        prm.add_endRequest(function () {
            // Ocultar spinner
            document.getElementById("loadingPanel")?.style?.setProperty("display", "none");

            // Cerrar modal de edición si está abierto
            $('.modal').modal('hide');

            // Eliminar cualquier backdrop viejo
            $('.modal-backdrop').remove();

            // Restaurar estado del body para permitir scroll
            $('body').removeClass('modal-open').css({
                'overflow': 'auto',
                'padding-right': ''
            });
        });
    </script>

</asp:Content>
