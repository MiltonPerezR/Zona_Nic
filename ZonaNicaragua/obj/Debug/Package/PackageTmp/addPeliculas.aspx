<%@ Page Title="Agregar Películas" Language="C#" Async="true" MasterPageFile="~/admin.Master" AutoEventWireup="true" CodeBehind="addPeliculas.aspx.cs" Inherits="ZonaNicaragua.addPeliculas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            background-color: #121212;
            color: #ffffff;
        }

        .card-custom {
            background-color: #1c1c1c;
            color: #fff;
            border-radius: 1rem;
            border: none;
        }

        .form-control,
        .form-select {
            background-color: #2c2c2c;
            color: #fff;
            border: 1px solid #444;
        }

        .form-control::placeholder {
            color: #aaa;
        }

        label {
            font-weight: 500;
            color: #fff;
        }

        .btn-rojo {
            background-color: #e50914;
            color: white;
            border: none;
        }

        .btn-rojo:hover {
            background-color: #ff0a16;
        }

        .btn-outline-light {
            border-color: #ccc;
            color: #ccc;
        }

        .btn-outline-light:hover {
            background-color: #333;
            color: #fff;
        }

        .text-danger {
            font-size: 0.85rem;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="card card-custom shadow-lg">
            <div class="card-header bg-black text-white">
                <h5 class="mb-0">🎬 Agregar Película</h5>
            </div>

            <div class="card-body">
                <div class="row g-4">

                    <!-- Columna izquierda -->
                    <div class="col-md-7">
                        <div class="row g-3">
                            <div class="col-md-12 d-flex">
                                <div class="flex-fill me-2">
                                    <label>Título</label>
                                    <asp:TextBox ID="txttTitulo" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="txttTitulov" runat="server" ControlToValidate="txttTitulo" CssClass="text-danger" ValidationGroup="Formulario" />
                                </div>
                                <div>
                                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-rojo mt-4" OnClick="btnBuscar_Click" />
                                </div>
                            </div>

                            <div class="col-md-12">
                                <label>Sinopsis</label>
                                <asp:TextBox ID="txtSinopsis" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" />
                                <asp:RequiredFieldValidator ID="rfvSinopsis" runat="server" ControlToValidate="txtSinopsis" CssClass="text-danger" ValidationGroup="Formulario" />
                            </div>

                            <div class="col-md-4">
                                <label>Duración</label>
                                <asp:TextBox ID="tbDuracion" runat="server" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="tbDuracionv" runat="server" ControlToValidate="tbDuracion" CssClass="text-danger" ValidationGroup="Formulario" />
                            </div>

                            <div class="col-md-4">
                                <label>Calidad</label>
                                <asp:TextBox ID="Calidad" runat="server" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="Calidad1" runat="server" ControlToValidate="Calidad" CssClass="text-danger" ValidationGroup="Formulario" />
                            </div>

                            <div class="col-md-4">
                                <label>Estreno</label>
                                <asp:TextBox ID="fechaEstreno" runat="server" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="fechaEstreno1" runat="server" ControlToValidate="fechaEstreno" CssClass="text-danger" ValidationGroup="Formulario" />
                            </div>

                            <div class="col-md-6">
                                <label>Género</label>
                                <asp:TextBox ID="genero" runat="server" CssClass="form-control" />
                            </div>
                            <div class="col-md-6">
                                <label>Géneros</label>
                                <asp:TextBox ID="generosM" runat="server" CssClass="form-control" />
                            </div>
                            <div class="col-md-6">
                                <label>URL Película</label>
                                <asp:TextBox ID="tburPelicula" runat="server" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="tburPeliculav" runat="server" ControlToValidate="tburPelicula" CssClass="text-danger" ValidationGroup="Formulario" />
                            </div>

                            <div class="col-md-6">
                                <label>Imagen Horizontal</label>
                                <asp:TextBox ID="tbImagenH" runat="server" CssClass="form-control" oninput="document.getElementById('imgPoster').src = this.value;" />
                                <asp:RequiredFieldValidator ID="tbImagenHv" runat="server" ControlToValidate="tbImagenH" CssClass="text-danger" ValidationGroup="Formulario" />
                            </div>

                            <div class="col-md-6">
                                <label>Imagen Vertical</label>
                                <asp:TextBox ID="tbImagenV" runat="server" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="tbImagenVv" runat="server" ControlToValidate="tbImagenV" CssClass="text-danger" ValidationGroup="Formulario" />
                            </div>
                        </div>
                    </div>

                    <!-- Columna derecha: vista previa -->
                    <div class="col-md-5 text-center">
                        <label class="form-label">Vista previa de póster</label><br />
                        <asp:Image ID="imgPoster" runat="server" ClientIDMode="Static" CssClass="img-thumbnail" Width="40%" />
                        <asp:Image ID="imgPosterH" runat="server" ClientIDMode="Static" CssClass="img-thumbnail" Width="40%" />
                    </div>
                </div>

                <hr class="border-secondary" />
                <div class="d-flex justify-content-between">
                    <asp:Button ID="btnCerrar" runat="server" Text="Salir" CssClass="btn btn-outline-light" OnClick="btnCerrar_Click" />
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar Película" CssClass="btn btn-rojo" ValidationGroup="Formulario" OnClick="btnGuardar_Click" />
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
