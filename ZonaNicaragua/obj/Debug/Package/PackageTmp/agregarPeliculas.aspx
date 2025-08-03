<%@ Page Title="Agregar Películas" Language="C#" Async="true" MasterPageFile="~/admin.Master" AutoEventWireup="true" CodeBehind="agregarPeliculas.aspx.cs" Inherits="ZonaNicaragua.agregarPeliculas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .card-movie {
            border: none;
            border-radius: 20px;
            overflow: hidden;
            box-shadow: 0 8px 20px rgba(0, 0, 0, 0.1);
        }

        .form-section {
            background: #ffffff;
            padding: 30px;
            border-radius: 20px;
            box-shadow: 0 4px 15px rgba(0, 0, 0, 0.07);
        }

        .btn-rounded {
            border-radius: 30px;
            padding: 10px 25px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="container mt-5">
        <div class="form-section">
            <h2 class="mb-4 text-dark">🎬 Buscar y Agregar Película</h2>

            <div class="input-group mb-4">
                <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control form-control-lg" placeholder="Escribe el título de la película..." />
                <asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-dark btn-rounded" Text="Buscar" OnClick="btnBuscar_Click" />
            </div>

            <asp:Panel ID="pnlResultado" runat="server" Visible="false" CssClass="mb-5">
                <div class="card card-movie">
                    <div class="row g-0">
                        <div class="col-md-4">
                            <asp:Image ID="imgPoster" runat="server" CssClass="img-fluid h-100" />
                        </div>
                        <div class="col-md-8">
                            <div class="card-body">
                                <h3 class="card-title text-primary"><asp:Label ID="lblTitulo" runat="server" /></h3>
                                <p><asp:Label ID="lblSinopsis" runat="server" CssClass="text-secondary" /></p>
                                <p class="mb-1"><strong>Género:</strong> <asp:Label ID="lblGenero" runat="server" /></p>
                                <p class="mb-1"><strong>Estreno:</strong> <asp:Label ID="lblEstreno" runat="server" /></p>
                                <p class="mb-3"><strong>Puntuación:</strong> <asp:Label ID="lblPuntuacion" runat="server" /></p>

                                <!-- HiddenFields para guardar -->
                                <asp:HiddenField ID="hfPoster" runat="server" />
                                <asp:HiddenField ID="hfTitulo" runat="server" />
                                <asp:HiddenField ID="hfSinopsis" runat="server" />
                                <asp:HiddenField ID="hfGenero" runat="server" />
                                <asp:HiddenField ID="hfEstreno" runat="server" />
                                <asp:HiddenField ID="hfPuntuacion" runat="server" />

                                <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-success btn-rounded" Text="💾 Guardar en la base de datos" OnClick="btnGuardar_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
