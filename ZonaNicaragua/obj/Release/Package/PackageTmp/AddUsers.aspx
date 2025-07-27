<%@ Page Title="" Language="C#" MasterPageFile="~/admin.Master" AutoEventWireup="true" CodeBehind="AddUsers.aspx.cs" Inherits="ZonaNicaragua.AddUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="container">
            <div class="card-body">
                <div class="row mb-3">
                    <div class="col-md-4">
                        <label for="txtUsuario" class="form-label">Usuario</label>
                        <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" Placeholder="Usuario..." />
                    </div>
                    <div class="col-md-4">
                        <label for="txtClave" class="form-label">Clave</label>
                        <asp:TextBox ID="txtClave" runat="server" CssClass="form-control" Placeholder="Clave..." />
                    </div>
                    <div class="col-md-4">
                        <label for="ddlTipoUsuario" class="form-label">Tipo usuario</label>
                        <asp:DropDownList ID="ddlTipoUsuario" runat="server" CssClass="form-select">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="text-firt">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardar_Click" />
            </div>
            <div class="text-firt">
                <asp:Label runat="server" ID="lblConfirmacion"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
