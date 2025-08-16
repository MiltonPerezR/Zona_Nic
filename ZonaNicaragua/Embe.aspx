<%@ Page Title="Reproductor de Video" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Embe.aspx.cs" Inherits="ZonaNicaragua.Embe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .fullscreen-iframe {
            position: fixed;   /* fijo en toda la pantalla */
            top: 0;
            left: 0;
            width: 100vw;      /* ancho completo */
            height: 100vh;     /* alto completo */
            border: none;
            margin: 0;
            padding: 0;
            z-index: 9999;     /* siempre encima de todo */
            background: black; /* fondo negro detrás del video */
        }
    </style>

    <iframe id="videoFrame" runat="server" class="fullscreen-iframe" allowfullscreen></iframe>
</asp:Content>
