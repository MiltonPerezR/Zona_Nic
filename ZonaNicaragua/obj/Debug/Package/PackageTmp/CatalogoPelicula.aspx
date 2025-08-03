<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CatalogoPelicula.aspx.cs" Inherits="ZonaNicaragua.CatalogoPelicula" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Catálogo de Películas - ZONANIC</title>
    <link href="Content/css/movies.css" rel="stylesheet" />
    <link href="Content/css/Inicio.css" rel="stylesheet" />
    <style>
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }
        body{
            font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif;
        }
        /* NAVBAR */
        .navbar {
            position: fixed;
            top: 0;
            left: 0;
            right: 0;
            height: 60px;
            background-color: #000;
            color: black;
            display: flex;
            align-items: center;
            justify-content: center;
            z-index: 9999;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.5);
        }

        .navbar h1 {
            font-size: 1.5rem;
            margin: 0;
            color: #fff;
            letter-spacing: 1px;
        }

        .navbar .back-button {
            position: absolute;
            left: 16px;
            background: none;
            border: none;
            color: #fff;
            font-size: 1.5rem;
            cursor: pointer;
            display: flex;
            align-items: center;
        }

        .navbar .back-button:hover {
            color: #ddd;
        }

        /* CATÁLOGO */
        .catalogo-container {
            display: flex;
            flex-wrap: wrap;
            gap: 10px;
            justify-content: flex-start;
            padding: 24px 16px;
            margin-top: 70px; /* deja espacio para el navbar fijo */
            width: 100vw;
        }

        .catalogo-card {
            background: #181818;
            border-radius: 2px;
            overflow: hidden;
            width: 160px;
            margin-bottom: 16px;
            box-shadow: 0 2px 8px rgba(0,0,0,0.25);
            transition: transform 0.2s;
            display: flex;
            flex-direction: column;
        }

        .catalogo-card:hover {
            transform: scale(1.04);
            box-shadow: 0 6px 24px rgba(0,0,0,0.4);
        }

        .catalogo-link {
            text-decoration: none;
            color: inherit;
            display: block;
            height: 100%;
        }

        .catalogo-img {
            width: 100%;
            height: 300px;
            object-fit: cover;
            display: block;
        }

        .catalogo-info {
            padding: 0.8rem 1rem 1rem 1rem;
            display: flex;
            flex-direction: column;
            gap: 0.3rem;
        }

        .catalogo-titulo {
            font-size: 1.1rem;
            font-weight: 700;
            color: #fff;
            margin: 0 0 0.2rem 0;
        }

        .catalogo-genero, .catalogo-calidad, .catalogo-fecha {
            font-size: 0.95rem;
            color: #bbb;
            margin-right: 0.5rem;
        }

        @media (max-width: 900px) {
            .catalogo-card { width: 45vw; min-width: 160px; }
        }

        @media (max-width: 600px) {
            .catalogo-card { width: 95vw; min-width: 120px; }
            .catalogo-img { height: 180px; }
        }

        .text-center {
            text-align: center;
        }

        .mt-4 {
            margin-top: 2rem;
        }

        .btn {
            padding: 10px 20px;
            background-color: #007bff;
            color: white;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            font-size: 1rem;
        }

        .btn:hover {
            background-color: #0056b3;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">

        <!-- NAVBAR -->
        <div class="navbar">
            <a href="Default.aspx" type="button" class="back-button">
                &#8592; <!-- ← flecha izquierda -->
            </a>
            <h1>ZONANIC</h1>
        </div>

        <!-- CATÁLOGO -->
        <div class="catalogo-container">
            <asp:Repeater ID="rptCatalogo" runat="server" OnItemCommand="rptCatalogo_ItemCommand1">
                <ItemTemplate>
                    <div class="catalogo-card">
                        <asp:LinkButton runat="server" CommandName="VerPelicula" CommandArgument='<%# Eval("Id") %>' CssClass="catalogo-link">
                            <img src='<%# Eval("Imagen") %>' alt='<%# Eval("Titulo") %>' class="catalogo-img" />
                        </asp:LinkButton>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <div class="text-center mt-4">
            <asp:Button ID="btnVerMas" runat="server" Text="Ver más" CssClass="btn" OnClick="btnVerMas_Click" />
        </div>

    </form>
</body>
</html>
