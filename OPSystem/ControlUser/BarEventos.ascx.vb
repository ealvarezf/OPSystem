Imports Security_System
Public Class BarEventos
    Inherits System.Web.UI.UserControl
    Public Event MsgEvent(ByVal sAcción As String)
    Private oUsr As New UserLogin
    Public Sub New()
        Consultar = New ItemEvento("Consultar", btnConsultar)
        Especial1 = New ItemEvento("Especial1", btnConsultar)
    End Sub
    Public Property EtiquetaMaestro() As New Label
    Public Property EtiquetaDetalle() As New Label
    Public Property HeaderBarEventos() As New Panel
    Public Property Back() As New ImageButton
    Public Property Tag() As String
    Public Property Pruebas() As Boolean
    Public Property FuncionID() As String
    Public Property InfoRegistroMaster() As String
    Public Property UrlNegarAutorizacion() As String = "~/Default.aspx"
    Public Property Consultar() As ItemEvento
    Public Property Nuevo() As New ItemEvento("Nuevo", btnNuevo)
    Public Property Eliminar() As New ItemEvento("Eliminar", btnEliminar)
    Public Property Recuperar() As New ItemEvento("Recuperar", btnEliminar)
    Public Property Editar() As New ItemEvento("Editar", btnEditar)
    Public Property Listar() As New ItemEvento("Listar", btnListar)
    Public Property Exportar() As New ItemEvento("Exportar", btnExportar)
    Public Property Graficar() As New ItemEvento("Graficar", btnGraficar)
    Public Property Filtrar() As New ItemEvento("Filtrar", btnFiltrar)
    Public Property Ayuda() As New ItemEvento("Ayuda", btnFiltrar)
    Public Property Especial1() As New ItemEvento("Especial1", btnEsp1)
    Public Property Especial2() As New ItemEvento("Especial2", btnEsp2)
    Public Property Especial3() As New ItemEvento("Especial3", btnEsp3)
    Public Property Especial4() As New ItemEvento("Especial4", btnEsp4)
    Public Property Especial5() As New ItemEvento("Especial5", btnEsp5)
    Public Property Especial6() As New ItemEvento("Especial6", btnEsp6)
    Public Property Especial7() As New ItemEvento("Especial7", btnEsp7)
    Public Property Especial8() As New ItemEvento("Especial8", btnEsp8)
    Public Property Especial9() As New ItemEvento("Especial9", btnEsp9)
    Public Property Especial10() As New ItemEvento("Especial10", btnEsp10)

    Private Sub BarEventos_Init(sender As Object, e As EventArgs) Handles Me.Init
        oUsr = Session("Usr")
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim oSql As New AppPermisos(oUsr)
        Dim oCs As New ColeccionPrmSql
        If Not IsPostBack Then

            Dim sPageAspx As String = Request.RawUrl.Split("/")(Request.RawUrl.Split("/").Length - 1)
            If InStr(sPageAspx, "?") > 0 Then
                sPageAspx = sPageAspx.Split("?")(0)
            End If
            Dim sDirWeb As String = Request.RawUrl.Split("/")(Request.RawUrl.Split("/").Length - 2)
            FuncionID = GetFunction(sDirWeb + "/" + sPageAspx, "SGFuncionID")

            If FuncionID Is Nothing Then Response.Redirect(UrlNegarAutorizacion, True)

            Dim Dri As DataRow = GetInfoFunction(FuncionID)
            If Not Dri Is Nothing Then
                btnBack.Visible = (Dri("SGFuncionPrimaria") = "N") 'Significa que es subfunción
                If btnBack.Visible Then
                    lblInfoDetalle.Text = Dri("SGFuncion")
                    pnlInfoDetalle.Visible = True
                    pnlInfoMaestro.Visible = True
                    Dim Drs As DataRow = GetInfoSubFunction(FuncionID)
                    If Not Drs Is Nothing Then
                        lblInfo.Text = Drs("SGFuncionPadre")
                    End If
                Else
                    pnlInfoDetalle.Visible = False
                    pnlInfoMaestro.Visible = True
                    lblInfo.Text = Dri("SGFuncion")
                End If
            End If

            oCs.Create("@SgUserID", oUsr.keyusu)
            oCs.Create("@SGFuncionID", FuncionID)
            Dim oTb As DataTable = oSql._List(oSql.Lista, "AUTORIZACION", oCs)
            If Not oTb Is Nothing Then
                If oTb.Rows.Count > 0 Or Pruebas Then
                    SetConfigUser()
                    For Each Dr As DataRow In oTb.Rows
                        If Not Pruebas Then
                            btnConsultar.Visible = (Dr("PerfilConsultar") = "S")
                            btnNuevo.Visible = (Dr("PerfilNuevo") = "S")
                            btnEliminar.Visible = (Dr("PerfilEliminar") = "S")
                            btnRecuperar.Visible = (Dr("PerfilRecuperar") = "S")
                            btnEditar.Visible = (Dr("PerfilModificar") = "S")
                            btnExportar.Visible = (Dr("PerfilExportar") = "S")
                            btnListar.Visible = (Dr("PerfilListar") = "S")
                            btnGraficar.Visible = (Dr("PerfilGraficar") = "S")
                            btnEsp1.Visible = (Dr("PerfilEspecial1") = "S")
                            btnEsp2.Visible = (Dr("PerfilEspecial2") = "S")
                            btnEsp3.Visible = (Dr("PerfilEspecial3") = "S")
                            btnEsp4.Visible = (Dr("PerfilEspecial4") = "S")
                            btnEsp5.Visible = (Dr("PerfilEspecial5") = "S")
                            btnEsp6.Visible = (Dr("PerfilEspecial6") = "S")
                            btnEsp7.Visible = (Dr("PerfilEspecial7") = "S")
                            btnEsp8.Visible = (Dr("PerfilEspecial8") = "S")
                            btnEsp9.Visible = (Dr("PerfilEspecial9") = "S")
                            btnEsp10.Visible = (Dr("PerfilEspecial10") = "S")
                        End If
                        Exit For
                    Next
                Else
                    Consultar.Boton.Visible = False
                End If
            Else
                SetConfigUser()
            End If
            If Not Consultar.Boton.Visible Then
                Response.Redirect(UrlNegarAutorizacion, True)
            End If
        End If
    End Sub

    Protected Sub btnConsultar_Click(sender As Object, e As ImageClickEventArgs) Handles btnConsultar.Click
        RaiseEvent MsgEvent(Consultar.Nombre)
    End Sub
    Protected Sub btnNuevo_Click(sender As Object, e As ImageClickEventArgs) Handles btnNuevo.Click
        RaiseEvent MsgEvent(Nuevo.Nombre)
    End Sub
    Protected Sub btnEliminar_Click(sender As Object, e As ImageClickEventArgs) Handles btnEliminar.Click
        RaiseEvent MsgEvent(Eliminar.Nombre)
    End Sub
    Protected Sub btnRecuperar_Click(sender As Object, e As ImageClickEventArgs) Handles btnRecuperar.Click
        RaiseEvent MsgEvent(Recuperar.Nombre)
    End Sub
    Protected Sub btnEditar_Click(sender As Object, e As ImageClickEventArgs) Handles btnEditar.Click
        RaiseEvent MsgEvent(Editar.Nombre)
    End Sub
    Protected Sub btnExportar_Click(sender As Object, e As ImageClickEventArgs) Handles btnExportar.Click
        RaiseEvent MsgEvent(Exportar.Nombre)
    End Sub
    Protected Sub btnFiltrar_Click(sender As Object, e As ImageClickEventArgs) Handles btnFiltrar.Click
        RaiseEvent MsgEvent(Filtrar.Nombre)
    End Sub
    Protected Sub btnListar_Click(sender As Object, e As ImageClickEventArgs) Handles btnListar.Click
        RaiseEvent MsgEvent(Listar.Nombre)
    End Sub
    Protected Sub btnGraficar_Click(sender As Object, e As ImageClickEventArgs) Handles btnGraficar.Click
        RaiseEvent MsgEvent(Graficar.Nombre)
    End Sub
    Protected Sub btnAyuda_Click(sender As Object, e As ImageClickEventArgs) Handles btnAyuda.Click
        RaiseEvent MsgEvent(Ayuda.Nombre)
    End Sub
    Private Sub btnEsp1_Click(sender As Object, e As ImageClickEventArgs) Handles btnEsp1.Click
        RaiseEvent MsgEvent(Especial1.Nombre)
    End Sub

    Protected Sub btnEsp2_Click(sender As Object, e As ImageClickEventArgs) Handles btnEsp2.Click
        RaiseEvent MsgEvent(Especial2.Nombre)
    End Sub
    Protected Sub btnEsp3_Click(sender As Object, e As ImageClickEventArgs) Handles btnEsp3.Click
        RaiseEvent MsgEvent(Especial3.Nombre)
    End Sub

    Protected Sub btnEsp4_Click(sender As Object, e As ImageClickEventArgs) Handles btnEsp4.Click
        RaiseEvent MsgEvent(Especial4.Nombre)
    End Sub

    Protected Sub btnEsp5_Click(sender As Object, e As ImageClickEventArgs) Handles btnEsp5.Click
        RaiseEvent MsgEvent(Especial5.Nombre)
    End Sub

    Protected Sub btnEsp6_Click(sender As Object, e As ImageClickEventArgs) Handles btnEsp6.Click
        RaiseEvent MsgEvent(Especial6.Nombre)
    End Sub

    Protected Sub btnEsp7_Click(sender As Object, e As ImageClickEventArgs) Handles btnEsp7.Click
        RaiseEvent MsgEvent(Especial7.Nombre)
    End Sub

    Protected Sub btnEsp8_Click(sender As Object, e As ImageClickEventArgs) Handles btnEsp8.Click
        RaiseEvent MsgEvent(Especial8.Nombre)
    End Sub

    Protected Sub btnEsp9_Click(sender As Object, e As ImageClickEventArgs) Handles btnEsp9.Click
        RaiseEvent MsgEvent(Especial9.Nombre)
    End Sub

    Protected Sub btnEsp10_Click(sender As Object, e As ImageClickEventArgs) Handles btnEsp10.Click
        RaiseEvent MsgEvent(Especial10.Nombre)
    End Sub

    Private Function GetInfoSubFunction(ByVal SGSubFuncionID As String) As DataRow
        Dim oSql As New AppSubFunciones(oUsr)
        Dim oCs As New ColeccionPrmSql
        GetInfoSubFunction = Nothing
        Try
            oCs.Create("@SGSubFuncionID", SGSubFuncionID)
            Dim oTb As DataTable = oSql._Item(oSql.Item, "SubFuncion", oCs)
            If Not oTb Is Nothing Then
                For Each Dr As DataRow In oTb.Rows
                    Return Dr
                    Exit For
                Next
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try

    End Function

    Private Function GetInfoFunction(ByVal sFuncionID As String) As DataRow
        Dim oSql As New AppFunciones(oUsr)
        Dim oCs As New ColeccionPrmSql
        GetInfoFunction = Nothing
        Try
            oCs.Create("@SGFuncionID", sFuncionID)
            Dim oTb As DataTable = oSql._Item(oSql.Item, "Funcion", oCs)
            If Not oTb Is Nothing Then
                For Each Dr As DataRow In oTb.Rows
                    Return Dr
                    Exit For
                Next
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try

    End Function

    Private Function GetFunction(ByVal sPage As String, ByVal sCampo As String) As String
        Dim oSql As New AppFunciones(oUsr)
        Dim oCs As New ColeccionPrmSql
        GetFunction = String.Empty
        Try
            oCs.Create("@SGFuncionWeb", sPage)
            Return oSql._Value(oSql.ItemForPage, sCampo, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try

    End Function
    Private Sub SetConfigUser()
        Try

            btnFiltrar.Visible = Filtrar.Boton.Visible

            With HeaderBarEventos
                pnlInfomaster.Visible = .Visible
                pnlInfomaster.BackColor = .BackColor
            End With

            With Back
                If Not Request.UrlReferrer Is Nothing Then
                    btnBack.PostBackUrl = Request.UrlReferrer.PathAndQuery
                End If
            End With

            With Consultar.Boton
                btnConsultar.Visible = .Visible
                btnConsultar.ToolTip = .ToolTip
                btnConsultar.Enabled = .Enabled
                btnConsultar.OnClientClick = .OnClientClick
            End With

            With Editar.Boton
                btnEditar.Visible = .Visible
                btnEditar.ToolTip = .ToolTip
                btnEditar.Enabled = .Enabled
                btnEditar.OnClientClick = .OnClientClick
            End With

            With Nuevo.Boton
                btnNuevo.Visible = .Visible
                btnNuevo.ToolTip = .ToolTip
                btnNuevo.Enabled = .Enabled
                btnNuevo.OnClientClick = .OnClientClick
            End With

            With Eliminar.Boton
                btnEliminar.Visible = .Visible
                btnEliminar.ToolTip = .ToolTip
                btnEliminar.Enabled = .Enabled
                btnEliminar.OnClientClick = .OnClientClick
            End With

            With Exportar.Boton
                btnExportar.Visible = .Visible
                btnExportar.ToolTip = .ToolTip
                btnExportar.Enabled = .Enabled
                btnExportar.OnClientClick = .OnClientClick
            End With

            With Listar.Boton
                btnListar.Visible = .Visible
                btnListar.ToolTip = .ToolTip
                btnListar.Enabled = .Enabled
                btnListar.OnClientClick = .OnClientClick
            End With

            With Graficar.Boton
                btnGraficar.Visible = .Visible
                btnGraficar.ToolTip = .ToolTip
                btnGraficar.Enabled = .Enabled
                btnGraficar.OnClientClick = .OnClientClick
            End With

            With Especial1.Boton
                btnEsp1.Visible = .Visible
                btnEsp1.ImageUrl = .ImageUrl
                btnEsp1.ToolTip = .ToolTip
                btnEsp1.Enabled = .Enabled
                btnEsp1.OnClientClick = .OnClientClick
            End With

            With Especial2.Boton
                btnEsp2.Visible = .Visible
                btnEsp2.ImageUrl = .ImageUrl
                btnEsp2.ToolTip = .ToolTip
                btnEsp2.Enabled = .Enabled
                btnEsp2.OnClientClick = .OnClientClick
            End With

            With Especial3.Boton
                btnEsp3.Visible = .Visible
                btnEsp3.ImageUrl = .ImageUrl
                btnEsp3.ToolTip = .ToolTip
                btnEsp3.Enabled = .Enabled
                btnEsp3.OnClientClick = .OnClientClick
            End With

            With Especial4.Boton
                btnEsp4.ImageUrl = .ImageUrl
                btnEsp4.ToolTip = .ToolTip
                btnEsp4.Enabled = .Enabled
                btnEsp4.OnClientClick = .OnClientClick
            End With

            With Especial5.Boton
                btnEsp5.ImageUrl = .ImageUrl
                btnEsp5.ToolTip = .ToolTip
                btnEsp5.Enabled = .Enabled
                btnEsp5.OnClientClick = .OnClientClick
            End With

            With Especial6.Boton
                btnEsp6.ImageUrl = .ImageUrl
                btnEsp6.ToolTip = .ToolTip
                btnEsp6.Enabled = .Enabled
                btnEsp6.OnClientClick = .OnClientClick
            End With

            With Especial7.Boton
                btnEsp7.ImageUrl = .ImageUrl
                btnEsp7.ToolTip = .ToolTip
                btnEsp7.Enabled = .Enabled
                btnEsp7.OnClientClick = .OnClientClick
            End With

            With Especial8.Boton
                btnEsp8.ImageUrl = .ImageUrl
                btnEsp8.ToolTip = .ToolTip
                btnEsp8.Enabled = .Enabled
                btnEsp8.OnClientClick = .OnClientClick
            End With

            With Especial9.Boton
                btnEsp9.ImageUrl = .ImageUrl
                btnEsp9.ToolTip = .ToolTip
                btnEsp9.Enabled = .Enabled
                btnEsp9.OnClientClick = .OnClientClick
            End With

            With Especial10.Boton
                btnEsp10.ImageUrl = .ImageUrl
                btnEsp10.ToolTip = .ToolTip
                btnEsp10.Enabled = .Enabled
                btnEsp10.OnClientClick = .OnClientClick
            End With

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

End Class