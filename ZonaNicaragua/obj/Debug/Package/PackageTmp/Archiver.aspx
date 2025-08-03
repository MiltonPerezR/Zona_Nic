<%@ Page Title="" Language="C#" MasterPageFile="~/admin.Master" AutoEventWireup="true" CodeBehind="Archiver.aspx.cs" Inherits="ZonaNicaragua.Archiver" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Bootstrap 5 CDN -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            font-family: 'Source Sans 3', sans-serif;
            background-color: #121212;
            color: #ffffff;
        }

        .card {
            color: #ffffff;
            border-radius: 16px;
            background-color: #242222;
        }

        .form-label {
            font-weight: bold;
        }

        .btn {
            min-width: 160px;
        }

        @media print {
            body * {
                visibility: hidden;
            }

            #tablaPeliculas, #tablaPeliculas * {
                visibility: visible;
            }

            #tablaPeliculas {
                position: absolute;
                left: 0;
                top: 0;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="container mt-4">
        <div class="card shadow-lg p-4">
            <h3 class="text-center mb-4">🎬 Enviar correo</h3>

            <hr class="my-4" />
            <div class="col-md-6">
                <label>Clave</label>
                <asp:TextBox ID="clave" runat="server" CssClass="form-control" Placeholder="Inserta la clave secreta"/>
                <asp:Label runat="server" ID="clavetxt" CssClass="text-danger" Visible="false"></asp:Label>
            </div>
            <div class="d-flex flex-wrap justify-content-center gap-2 mt-5">
                <asp:Button ID="btnExportInsert" runat="server" Text="Exportar como INSERT SQL" CssClass="btn btn-success" OnClick="btnExportInsert_Click" />
                <asp:Button ID="Correo" runat="server" Text="Enviar correo como INSERT SQL" CssClass="btn btn-success" OnClick="btnExportInsert_Click1" />
            </div>
        </div>
    </div>
</asp:Content>
