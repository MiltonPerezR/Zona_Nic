<%@ Page Title="" Language="C#" MasterPageFile="~/admin.Master" AutoEventWireup="true" CodeBehind="ControlSerie.aspx.cs" Inherits="ZonaNicaragua.ControlSerie" %>

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
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Label runat="server" ID="lblId" Visible="false" />

            <div class="container mt-5">
                <h2 class="mb-4">Listado de series</h2>

                <div class="d-flex justify-content-end mb-3">
                    <a href="addSerie.aspx" class="btn btn-primary bi bi-plus-circle me-1"> Agregar Serie</a>
                    <a href="ControlEpisodio.aspx" class="btn btn-success bi bi-plus-circle"> Agregar Episodio</a>
                </div>

                <div class="table-responsive">
                    <table id="tablaPeliculas" class="table table-striped table-hover table-bordered align-middle">
                        <thead class="table-dark text-center">
                            <tr>
                                <th>ID</th>
                                <th>Título</th>
                                <th>Género</th>
                                <th>Sinopsis</th>
                                <th>Acciones</th>
                            </tr>
                        </thead>
                        <tbody class="table-dark">
                            <asp:Repeater ID="gvPelicula" runat="server" OnItemCommand="gvPelicula_ItemCommand">
                                <ItemTemplate>
                                    <tr class="text-center">
                                        <td><%# Eval("IdSerie") %></td>
                                        <td class="fw-bold"><%# Eval("TituloSerie") %></td>
                                        <td><span class="badge bg-danger p-2"><%# Eval("Genero") %></span></td>
                                        <td class="text-start text-truncate" style="max-width: 250px;"><%# Eval("SinopsisSerie") %></td>
                                        <td>
                                            <asp:LinkButton runat="server" CommandName="Visualizar" CommandArgument='<%# Eval("IdSerie") %>' CssClass="btn btn-primary bi bi-eye btn-sm" />
                                            <asp:LinkButton runat="server" CommandName="Editar" CommandArgument='<%# Eval("IdSerie") %>' CssClass="btn btn-warning btn-sm bi bi-pencil-square" />
                                            <asp:LinkButton runat="server" ID="btnEliminar" CommandName="Eliminar" CommandArgument='<%# Eval("IdSerie") %>' CssClass="btn btn-danger btn-sm bi bi-trash" />
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
                                    <label class="form-label">Sinopsis</label>
                                    <asp:TextBox ID="SinopsisTxt" runat="server" CssClass="form-control" ReadOnly="true" />
                                </div>
                                <div class="col-md-4">
                                    <label class="form-label">Genero</label>
                                    <asp:TextBox ID="generot" runat="server" CssClass="form-control" ReadOnly="true" />
                                </div>
                                <div class="col-md-4">
                                    <label class="form-label">Miniatura</label>
                                    <div></div>
                                    <asp:Image ID="mini" runat="server" CssClass="img-fluid mt-2 rounded" Style="max-height: 200px;" />
                                </div>
                                <div class="col-md-4">
                                    <label class="form-label">Poster</label>
                                    <asp:Image ID="pos" runat="server" CssClass="img-fluid mt-2 rounded" Style="max-height: 200px;" />
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
                <div class="modal fade netflix-modal" id="modalAcciones" tabindex="-1">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="container mt-4">
                                <div class="card-body">
                                    <h2>Editar Serie</h2>
                                    <div class="row mb-3">
                                        <div class="col-md-4">
                                            <label class="form-label">Título</label>
                                            <asp:TextBox ID="txttTitulo" runat="server" CssClass="form-control" />
                                        </div>
                                        <div class="col-md-4">
                                            <label class="form-label">Sinopsis</label>
                                            <asp:TextBox ID="txtSinopsis" runat="server" CssClass="form-control" />
                                        </div>
                                        <div class="col-md-4">
                                            <label class="form-label">Género</label>
                                            <asp:TextBox ID="genero" runat="server" CssClass="form-control" />
                                        </div>
                                        <div class="col-md-4">
                                            <label class="form-label">Géneros</label>
                                            <asp:TextBox ID="generos" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>

                                    <div class="row mb-3">
                                        <div class="col-md-4">
                                            <label class="form-label">Imagen Vertical</label>
                                            <asp:TextBox ID="tbImagenV" runat="server" CssClass="form-control" onkeyup="previewImage(this, 'previewV')" />
                                            <asp:Image ID="previewV" runat="server" CssClass="img-fluid mt-2" Style="max-height: 200px;" />
                                        </div>
                                        <div class="col-md-4">
                                            <label class="form-label">Imagen Horizontal</label>
                                            <asp:TextBox ID="tbImagenH" runat="server" CssClass="form-control" />
                                            <asp:Image ID="previewH" runat="server" CssClass="img-fluid mt-2" Style="max-height: 200px;" />
                                        </div>
                                    </div>

                                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardar_Click" />
                                    <asp:Label ID="lblConfirmacion" runat="server" CssClass="text-success ms-3" />
                                </div>
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
    </asp:UpdatePanel>

    <!-- SweetAlert y fixes -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script>
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $('.modal').modal('hide');
            $('.modal-backdrop').remove();
            document.body.classList.remove('modal-open');
            document.body.style.overflow = 'auto';
        });

        function previewImage(input, imgId) {
            var image = document.getElementById(imgId);
            image.src = input.value;
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
</asp:Content>
