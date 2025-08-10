<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ZonaNicaragua.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ZonaNic | Inicio de Sesión</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        /* ====== OCULTAR ANUNCIOS DE SOME E ====== */
        div[style*="z-index: 2147483647"],
        div[onmouseover="S_ssac()"] {
            display: none !important;
        }

        a[href*="somee.com"] {
            display: none !important;
        }

        body {
            background: #f5f6fa;
        }

        .login-container {
            max-width: 400px;
            margin: 80px auto;
            padding: 30px;
            background: #fff;
            border-radius: 15px;
            box-shadow: 0 0 20px rgba(0,0,0,0.1);
        }

        .login-title {
            text-align: center;
            font-weight: bold;
            margin-bottom: 25px;
            color: #007BFF;
        }

        /* Estilos para foco visible en TV remoto */
        .focusable {
            outline: none;
        }
        .focusable:focus {
            outline: 3px solid #007BFF;
            border-radius: 6px;
            box-shadow: 0 0 8px #007BFF;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="login-container">
                <h2 class="login-title focusable" tabindex="0">ZonaNic</h2>

                <div class="mb-3">
                    <label for="txtUsuario" class="form-label">Usuario</label>
                    <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control focusable" TabIndex="0" />
                </div>

                <div class="mb-3">
                    <label for="txtClave" class="form-label">Contraseña</label>
                    <asp:TextBox ID="txtClave" runat="server" CssClass="form-control focusable" TextMode="Password" TabIndex="0" />
                </div>

                <asp:Button ID="btnLogin" runat="server" Text="Iniciar Sesión" CssClass="btn btn-primary w-100 focusable" OnClick="btnLogin_Click" TabIndex="0" />

                <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger mt-3 d-block text-center" />
            </div>
        </div>
    </form>

    
</body>
</html>
