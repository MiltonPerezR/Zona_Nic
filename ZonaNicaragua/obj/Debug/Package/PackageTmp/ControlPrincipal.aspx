<%@ Page Title="" Language="C#" MasterPageFile="~/admin.Master" AutoEventWireup="true" CodeBehind="ControlPrincipal.aspx.cs" Inherits="ZonaNicaragua.ControlPrincipal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <style>
        body {
            background-color: #121212;
            color: #ffffff;
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
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Label runat="server" ID="lblId" Visible="false" />
            <asp:Label runat="server" ID="peliText" Visible="false" />
            <div class="container mt-5">
                <h2 class="mb-4">Listado de Usuarios</h2>
                <div class="d-flex justify-content-end mb-3">
                    <a id="lbAgregarUsuario" href="AddUsers.aspx" class="btn btn-success bi bi-person-add">Agregar</a>
                </div>

                <div class="table-responsive">
                    <table id="tablaPeliculas" class="table table-striped table-hover table-bordered align-middle nowrap" style="width: 100%">
                        <thead class="table-dark text-center">
                            <tr>
                                <th>ID</th>
                                <th>Usuario</th>
                                <th>Tipo de usuario</th>
                                <th>Activo</th>
                                <th>Acciones</th>
                            </tr>
                        </thead>
                        <tbody class="table-dark">
                            <asp:Repeater ID="gvUsuarios" runat="server" OnItemCommand="gvUsuario_ItemCommand">
                                <ItemTemplate>
                                    <tr class="text-center">
                                        <td><%# Eval("IdUsuario") %></td>
                                        <td class="fw-bold"><%# Eval("Usuario") %></td>
                                        <td><span class="badge bg-danger p-2"><%# Eval("Tipo") %></span></td>
                                        <td><%# Eval("Activo") %></td>
                                        <td>
                                            <asp:LinkButton runat="server" CommandName="Visualizar" CommandArgument='<%# Eval("IdUsuario") %>' CssClass="btn btn-sm btn-primary bi bi-eye" />
                                            <asp:LinkButton runat="server" CommandName="Editar" CommandArgument='<%# Eval("IdUsuario") %>' CssClass="btn btn-sm btn-warning bi bi-pencil-square" />
                                            <asp:LinkButton runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("IdUsuario") %>' CssClass="btn btn-sm btn-danger bi bi-trash" />
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
                                    <label class="form-label text-black">Usuario</label>
                                    <asp:TextBox ID="usertxt" runat="server" CssClass="form-control" ReadOnly="true" />
                                </div>
                                <div class="col-md-4">
                                    <label class="form-label text-black">Contraseña</label>
                                    <asp:TextBox ID="clavetxt" runat="server" CssClass="form-control" ReadOnly="true" />
                                </div>
                                <div class="col-md-4">
                                    <label class="form-label text-black">Tipo usuario</label>
                                    <asp:TextBox ID="tipotxt" runat="server" CssClass="form-control" ReadOnly="true" />
                                </div>
                                <div class="col-md-4">
                                    <label class="form-label text-black">Activo?</label>
                                    <asp:TextBox ID="estadotxt" runat="server" CssClass="form-control" ReadOnly="true" />
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
                            <h4 class="mb-4 text-black">Editar Usuario</h4>
                            <div class="row g-3">
                                <div class="col-md-4">
                                    <label class="form-label text-black">Usuario</label>
                                    <asp:TextBox ID="q" runat="server" CssClass="form-control text-black" />
                                </div>
                                <div class="col-md-4">
                                    <label class="form-label text-black">Contraseña</label>
                                    <asp:TextBox ID="w" runat="server" CssClass="form-control text-black" />
                                </div>
                                <div class="col-md-4">
                                    <label class="form-label text-black">Tipo usuario</label>
                                    <asp:DropDownList ID="ddlTipoUsuario" runat="server" CssClass="form-select"></asp:DropDownList>
                                </div>
                                <div class="col-md-4">
                                    <label class="form-label text-black">Activo?</label>
                                    <asp:TextBox ID="r" runat="server" CssClass="form-control text-black" />
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
                            <p class="fs-5 text-black">¿Estás seguro de eliminar a este usuario?</p>
                            <p class="text-muted mb-0">
                                <asp:Label ID="usuarioTxt" CssClass="text-black" runat="server" />
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
            <script>
                $(document).ready(function () {
                    $('#tablaPeliculas').DataTable({
                        responsive: true,
                        lengthMenu: [[10, 50, 100], [10, 50, 100]],
                        language: {
                            search: "Buscar:",
                            lengthMenu: "Mostrar _MENU_ registros por página",
                            zeroRecords: "No se encontraron resultados",
                            info: "Mostrando página _PAGE_ de _PAGES_",
                            infoEmpty: "No hay registros disponibles",
                            infoFiltered: "(filtrado de _MAX_ registros totales)",
                            paginate: {
                                first: "Primero",
                                last: "Último",
                                next: "Siguiente",
                                previous: "Anterior"
                            }
                        }
                    });
                });
    </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
