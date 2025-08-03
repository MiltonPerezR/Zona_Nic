<%@ Page Title="" Language="C#" MasterPageFile="~/admin.Master" AutoEventWireup="true" CodeBehind="Control.aspx.cs" Inherits="ZonaNicaragua.Control" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main class="app-main text-white">
        <!--begin::App Content Header-->
        <div class="app-content-header">
            <!--begin::Container-->
            <div class="container-fluid">
                <!--begin::Row-->
                <div class="row">
                    <div class="col-sm-6">
                        <h3 class="mb-0 text-white" style="color: black">Dashboard</h3>
                    </div>
                </div>
                <!--end::Row-->
            </div>
        </div>
        <div class="app-content">
            <div class="container-fluid">
                <div class="row">
                    <%--cantidad de peliculas--%>
                    <div class="col-lg-3 col-6">
                        <div class="small-box text-bg-primary" style="display: flex; flex-direction: column; height: 150px;">
                            <div class="inner" style="display: flex; justify-content: space-between; align-items: center;">
                                <!-- Label a la izquierda -->
                                <h3>
                                    <asp:Label runat="server" ID="lblcountPeliculas"></asp:Label>
                                </h3>
                                <!-- Ícono de película de Bootstrap a la derecha -->
                                <i class="bi bi-film" style="font-size: 2.5em;"></i>
                            </div>
                            <p style="padding: 3px 10px;">Películas agregadas</p>
                            <a href="ControlPelicula.aspx" class="small-box-footer link-light link-underline-opacity-0 link-underline-opacity-50-hover">Agregar Películas <i class="bi bi-link-45deg"></i>
                            </a>
                        </div>
                    </div>
                    <!--end::Col-->

                    <%--cantidad de series--%>
                    <div class="col-lg-3 col-6">

                        <div class="small-box text-bg-success" style="display: flex; flex-direction: column; height: 150px;">
                            <div class="inner" style="display: flex; justify-content: space-between; align-items: center;">

                                <h3>
                                    <asp:Label runat="server" ID="lblCantidadSeries"></asp:Label>
                                </h3>
                                <i class="bi bi-camera-reels" style="font-size: 2.5em;"></i>
                            </div>
                            <p style="padding: 3px 10px;">Series agregadas</p>
                            <a href="ControlSerie.aspx" class="small-box-footer link-light link-underline-opacity-0 link-underline-opacity-50-hover">Agregar Series <i class="bi bi-link-45deg"></i>
                            </a>
                        </div>
                        <!--end::Small Box Widget 2-->
                    </div>
                    <!--Animes-->
                    <div class="col-lg-3 col-6">
                        <div class="small-box text-bg-danger" style="display: flex; flex-direction: column; height: 150px;">
                            <div class="inner" style="display: flex; justify-content: space-between; align-items: center;">

                                <h3>
                                    <asp:Label runat="server" ID="lblCantidadesAnimes"></asp:Label>
                                </h3>
                                <!-- Ícono de Bootstrap a la derecha -->
                                <i class="bi bi-fire" style="font-size: 2.5em;"></i>
                            </div>
                            <p style="padding: 3px 10px;">Animes agregados</p>
                            <a href="ControlSerie.aspx" class="small-box-footer link-light link-underline-opacity-0 link-underline-opacity-50-hover">Agregar Animes <i class="bi bi-link-45deg"></i>
                            </a>
                        </div>
                        <!--end::Small Box Widget 4-->
                    </div>
                    <div class="col-lg-3 col-6">
                        <div class="small-box text-bg-danger" style="display: flex; flex-direction: column; height: 150px;">
                            <div class="inner" style="display: flex; justify-content: space-between; align-items: center;">

                                <h3>
                                    <asp:Label runat="server" ID="lblCantidadEpisodios"></asp:Label>
                                </h3>
                                <!-- Ícono de Bootstrap a la derecha -->
                                <i class="bi bi-fire" style="font-size: 2.5em;"></i>
                            </div>
                            <p style="padding: 3px 10px;">Episodios totales</p>
                            <a href="ControlEpisodio.aspx" class="small-box-footer link-light link-underline-opacity-0 link-underline-opacity-50-hover">Agregar Episodio <i class="bi bi-link-45deg"></i>
                            </a>
                        </div>
                        <!--end::Small Box Widget 4-->
                    </div>
                    <div class="col-lg-3 col-6">
                        <!--begin::Small Box Widget 3-->
                        <div class="small-box text-bg-warning" style="display: flex; flex-direction: column; height: 150px;">
                            <div class="inner" style="display: flex; justify-content: space-between; align-items: center;">
                                <!-- Label a la izquierda -->
                                <h3>
                                    <asp:Label runat="server" ID="lblCantidadUsuarios"></asp:Label>
                                </h3>
                                <!-- Ícono de Bootstrap a la derecha -->
                                <i class="bi bi-person-add" style="font-size: 2.5em;"></i>
                            </div>
                            <p style="padding: 3px 10px;">Usuarios registrados</p>
                            <a href="ControlPrincipal.aspx" class="small-box-footer link-dark link-underline-opacity-0 link-underline-opacity-50-hover">More info <i class="bi bi-link-45deg"></i>
                            </a>
                        </div>
                        <!--end::Small Box Widget 3-->
                    </div>
                    <!--end::Col-->


                    <!--end::Col-->
                </div>
                <!--end::Row-->
            </div>
            <!--end::Container-->
        </div>
        <!--end::App Content-->

    </main>
</asp:Content>
