Imports Security_System
Public Class SGFunciones
    Inherits System.Web.UI.Page
    Private Ds As New DataSet
    Private oUsr As UserLogin
    Private Sub SGFunciones_Init(sender As Object, e As EventArgs) Handles Me.Init
        oUsr = Session("Usr")
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            SetFormConfig()
            LoadLista()
        End If
    End Sub
    '==============================================================================================================================
    'Inicialización 
    '==============================================================================================================================
    Private Sub SetFormConfig()
        pnlEventos.Visible = True
        pnlFiltros.Visible = False
        pnlAdd.Visible = False
        pnlListar.Visible = True
        'With BarEventos1

        'End With
        'Valores predeterminados
        txt_SGFuncionID.Text = ""
        txt_SGFuncion.Text = ""
        txt_SGFuncionAyuda.Text = ""
        txt_SGFuncionOLEDLL.Text = ""
        txt_SGFuncionSubsistema.Text = ""
        txt_SGFuncionWeb.Text = ""
        txt_SGFuncionID.MaxLength = 32
        txt_SGFuncion.MaxLength = 20
        txt_SGFuncionAyuda.MaxLength = 60
        txt_SGFuncionOLEDLL.MaxLength = 100
        txt_SGFuncionSubsistema.MaxLength = 2

        txt_SGFuncionEventoEspNombre1.MaxLength = 50
        txt_SGFuncionEventoEspNombre2.MaxLength = 50
        txt_SGFuncionEventoEspNombre3.MaxLength = 50
        txt_SGFuncionEventoEspNombre4.MaxLength = 50
        txt_SGFuncionEventoEspNombre5.MaxLength = 50
        txt_SGFuncionEventoEspNombre6.MaxLength = 50
        txt_SGFuncionEventoEspNombre7.MaxLength = 50
        txt_SGFuncionEventoEspNombre8.MaxLength = 50
        txt_SGFuncionEventoEspNombre9.MaxLength = 50
        txt_SGFuncionEventoEspNombre10.MaxLength = 50

        'Tamaño de pagina predeterminado
        GridView1.PageSize = 10
    End Sub

    '================================================================================================================================
    'Acciones con el modelo de datos
    Private Sub LoadLista()
        Dim oSql As New SQLFunciones(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            oCs.Create("@SGFuncion", txtSearch_SGFuncion.Text & "%")
            oCs.Create("@SGFuncionEstatus", oUsr.Mis.Status)
            Dim oTabla As DataTable = oSql._List(oSql.List, "FUNCIONES", oCs)
            LoadGrid(GridView1, oTabla)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

    Protected Sub imgBtnAplicaFiltro_Click(sender As Object, e As ImageClickEventArgs) Handles imgBtnAplicaFiltro.Click
        pnlFiltros.Visible = False
        LoadLista()
    End Sub

    Protected Sub imgbtnCancelaFiltro_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnCancelaFiltro.Click
        pnlFiltros.Visible = False
    End Sub

    Private Sub BarEventos1_MsgEvent(sAcción As String) Handles BarEventos1.MsgEvent
        Select Case sAcción
            Case "Nuevo"
                SetFormEdit(sAcción, GridView1)
            Case "Eliminar"
                SetFormEdit(sAcción, GridView1)
            Case "Editar"
                SetFormEdit(sAcción, GridView1)
            Case "Filtrar"
                pnlFiltros.Visible = True

            Case Else
        End Select
    End Sub

    Private Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        sender.PageIndex = e.NewPageIndex
        LoadLista()
    End Sub

    Private Sub SetFormEdit(ByVal sAcc As String, ByVal oGrid As GridView)
        Dim oSql As New SQLFunciones(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            pnlEventos.Visible = False
            pnlFiltros.Visible = False
            pnlAdd.Visible = True
            pnlListar.Visible = False

            Select Case sAcc
                Case "Nuevo"
                    'Configuraciones iniciales
                Case Else
                    Dim sKey As String = oGrid.DataKeys(oGrid.SelectedRow.RowIndex).Value
                    oCs.Create("@SGFuncionID", sKey)
                    Dim oTb As DataTable = oSql._Item(oSql.Item, "R", oCs)
                    If Not oTb Is Nothing Then
                        For Each Dr As DataRow In oTb.Rows
                            lblAcción.Text = sAcc
                            txt_SGFuncionID.Text = sKey.ToString
                            txt_SGFuncion.Text = Dr("SGFuncion").ToString
                            txt_SGFuncionAyuda.Text = Dr("SGFuncionAyuda").ToString
                            txt_SGFuncionOLEDLL.Text = Dr("SGFuncionOLEDLL").ToString
                            txt_SGFuncionSubsistema.Text = Dr("SGFuncionSubsistema").ToString
                            txt_SGFuncionWeb.Text = Dr("SGFuncionWeb").ToString
                            chk_SGFuncionConsultar.Checked = (Dr("SGFuncionConsultar").ToString = "S")
                            chk_SGFuncionNuevo.Checked = (Dr("SGFuncionNuevo").ToString = "S")
                            chk_SGFuncionEliminar.Checked = (Dr("SGFuncionEliminar").ToString = "S")
                            chk_SGFuncionRecuperar.Checked = (Dr("SGFuncionRecuperar").ToString = "S")
                            chk_SGFuncionModificar.Checked = (Dr("SGFuncionModificar").ToString = "S")
                            chk_SGFuncionListar.Checked = (Dr("SGFuncionListar").ToString = "S")
                            chk_SGFuncionExportar.Checked = (Dr("SGFuncionExportar").ToString = "S")
                            chk_SGFuncionGraficar.Checked = (Dr("SGFuncionGraficar").ToString = "S")
                            chk_SGFuncionMaestroDetalle.Checked = (Dr("SGFuncionMaestroDetalle").ToString = "S")
                            chk_SGFuncionPrimaria.Checked = (Dr("SGFuncionPrimaria").ToString = "S")
                            chk_SGFuncionEventoEsp1.Checked = (Dr("SGFuncionEventoEsp1").ToString = "S")
                            chk_SGFuncionEventoEsp2.Checked = (Dr("SGFuncionEventoEsp2").ToString = "S")
                            chk_SGFuncionEventoEsp3.Checked = (Dr("SGFuncionEventoEsp3").ToString = "S")
                            chk_SGFuncionEventoEsp4.Checked = (Dr("SGFuncionEventoEsp4").ToString = "S")
                            chk_SGFuncionEventoEsp5.Checked = (Dr("SGFuncionEventoEsp5").ToString = "S")
                            chk_SGFuncionEventoEsp6.Checked = (Dr("SGFuncionEventoEsp6").ToString = "S")
                            chk_SGFuncionEventoEsp7.Checked = (Dr("SGFuncionEventoEsp7").ToString = "S")
                            chk_SGFuncionEventoEsp8.Checked = (Dr("SGFuncionEventoEsp8").ToString = "S")
                            chk_SGFuncionEventoEsp9.Checked = (Dr("SGFuncionEventoEsp9").ToString = "S")
                            chk_SGFuncionEventoEsp10.Checked = (Dr("SGFuncionEventoEsp10").ToString = "S")
                            txt_SGFuncionEventoEspNombre1.Text = Dr("SGFuncionEventoEsp1Nombre").ToString
                            txt_SGFuncionEventoEspNombre2.Text = Dr("SGFuncionEventoEsp2Nombre").ToString
                            txt_SGFuncionEventoEspNombre3.Text = Dr("SGFuncionEventoEsp3Nombre").ToString
                            txt_SGFuncionEventoEspNombre4.Text = Dr("SGFuncionEventoEsp4Nombre").ToString
                            txt_SGFuncionEventoEspNombre5.Text = Dr("SGFuncionEventoEsp5Nombre").ToString
                            txt_SGFuncionEventoEspNombre6.Text = Dr("SGFuncionEventoEsp6Nombre").ToString
                            txt_SGFuncionEventoEspNombre7.Text = Dr("SGFuncionEventoEsp7Nombre").ToString
                            txt_SGFuncionEventoEspNombre8.Text = Dr("SGFuncionEventoEsp8Nombre").ToString
                            txt_SGFuncionEventoEspNombre9.Text = Dr("SGFuncionEventoEsp9Nombre").ToString
                            txt_SGFuncionEventoEspNombre10.Text = Dr("SGFuncionEventoEsp10Nombre").ToString
                            Exit For
                        Next
                    End If

            End Select

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

    Protected Sub ImgBtnAceptar_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnAceptar.Click
        If SaveTable() Then
            LoadLista()
            SetFormConfig()
        End If
    End Sub

    Protected Sub ImgBtnCancelar_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnCancelar.Click
        SetFormConfig()
    End Sub

    Private Function SaveTable() As Boolean
        Dim oSql As New SQLFunciones(oUsr)
        Dim oCs As New ColeccionPrmSql
        SaveTable = False
        Try
            oCs.Create("@SGFuncionID", txt_SGFuncionID.Text)
            Dim oTb As DataTable = oSql._Item(oSql.Item, "SGFUNCIONES", oCs)
            If Not oTb Is Nothing Then
                oTb.Columns("SGFuncionID").Unique = True
                If oTb.Rows.Count = 0 Then
                    Dim Dr As DataRow = oTb.NewRow
                    Dr("SGFuncionID") = txt_SGFuncionID.Text
                    Dr("SGFuncion") = txt_SGFuncion.Text
                    Dr("SGFuncionAyuda") = txt_SGFuncionAyuda.Text
                    Dr("SGFuncionOLEDLL") = txt_SGFuncionOLEDLL.Text
                    Dr("SGFuncionSubsistema") = txt_SGFuncionSubsistema.Text
                    Dr("SGFuncionWeb") = txt_SGFuncionWeb.Text
                    Dr("SGFuncionConsultar") = IIf(chk_SGFuncionConsultar.Checked, "S", "N")
                    Dr("SGFuncionNuevo") = IIf(chk_SGFuncionNuevo.Checked, "S", "N")
                    Dr("SGFuncionEliminar") = IIf(chk_SGFuncionEliminar.Checked, "S", "N")
                    Dr("SGFuncionRecuperar") = IIf(chk_SGFuncionRecuperar.Checked, "S", "N")
                    Dr("SGFuncionModificar") = IIf(chk_SGFuncionModificar.Checked, "S", "N")
                    Dr("SGFuncionListar") = IIf(chk_SGFuncionListar.Checked, "S", "N")
                    Dr("SGFuncionExportar") = IIf(chk_SGFuncionExportar.Checked, "S", "N")
                    Dr("SGFuncionGraficar") = IIf(chk_SGFuncionGraficar.Checked, "S", "N")
                    Dr("SGFuncionMaestroDetalle") = IIf(chk_SGFuncionMaestroDetalle.Checked, "S", "N")
                    Dr("SGFuncionPrimaria") = IIf(chk_SGFuncionPrimaria.Checked, "S", "N")
                    Dr("SGFuncionEstatus") = oUsr.Mis.Status

                    Dr("SGFuncionEventoEsp1") = IIf(chk_SGFuncionEventoEsp1.Checked, "S", "N")
                    Dr("SGFuncionEventoEsp2") = IIf(chk_SGFuncionEventoEsp2.Checked, "S", "N")
                    Dr("SGFuncionEventoEsp3") = IIf(chk_SGFuncionEventoEsp3.Checked, "S", "N")
                    Dr("SGFuncionEventoEsp4") = IIf(chk_SGFuncionEventoEsp4.Checked, "S", "N")
                    Dr("SGFuncionEventoEsp5") = IIf(chk_SGFuncionEventoEsp5.Checked, "S", "N")
                    Dr("SGFuncionEventoEsp6") = IIf(chk_SGFuncionEventoEsp6.Checked, "S", "N")
                    Dr("SGFuncionEventoEsp7") = IIf(chk_SGFuncionEventoEsp7.Checked, "S", "N")
                    Dr("SGFuncionEventoEsp8") = IIf(chk_SGFuncionEventoEsp8.Checked, "S", "N")
                    Dr("SGFuncionEventoEsp9") = IIf(chk_SGFuncionEventoEsp9.Checked, "S", "N")
                    Dr("SGFuncionEventoEsp10") = IIf(chk_SGFuncionEventoEsp10.Checked, "S", "N")
                    Dr("SGFuncionEventoEsp1Nombre") = txt_SGFuncionEventoEspNombre1.Text
                    Dr("SGFuncionEventoEsp2Nombre") = txt_SGFuncionEventoEspNombre2.Text
                    Dr("SGFuncionEventoEsp3Nombre") = txt_SGFuncionEventoEspNombre3.Text
                    Dr("SGFuncionEventoEsp4Nombre") = txt_SGFuncionEventoEspNombre4.Text
                    Dr("SGFuncionEventoEsp5Nombre") = txt_SGFuncionEventoEspNombre5.Text
                    Dr("SGFuncionEventoEsp6Nombre") = txt_SGFuncionEventoEspNombre6.Text
                    Dr("SGFuncionEventoEsp7Nombre") = txt_SGFuncionEventoEspNombre7.Text
                    Dr("SGFuncionEventoEsp8Nombre") = txt_SGFuncionEventoEspNombre8.Text
                    Dr("SGFuncionEventoEsp9Nombre") = txt_SGFuncionEventoEspNombre9.Text
                    Dr("SGFuncionEventoEsp10Nombre") = txt_SGFuncionEventoEspNombre10.Text

                    oTb.Rows.Add(Dr)
                    Return oSql.StatemenInsert(oTb)
                Else
                    Dim Dr As DataRow = oTb.Rows(0)
                    Dr("SGFuncion") = txt_SGFuncion.Text
                    Dr("SGFuncionAyuda") = txt_SGFuncionAyuda.Text
                    Dr("SGFuncionOLEDLL") = txt_SGFuncionOLEDLL.Text
                    Dr("SGFuncionSubsistema") = txt_SGFuncionSubsistema.Text
                    Dr("SGFuncionWeb") = txt_SGFuncionWeb.Text
                    Dr("SGFuncionConsultar") = IIf(chk_SGFuncionConsultar.Checked, "S", "N")
                    Dr("SGFuncionNuevo") = IIf(chk_SGFuncionNuevo.Checked, "S", "N")
                    Dr("SGFuncionEliminar") = IIf(chk_SGFuncionEliminar.Checked, "S", "N")
                    Dr("SGFuncionRecuperar") = IIf(chk_SGFuncionRecuperar.Checked, "S", "N")
                    Dr("SGFuncionModificar") = IIf(chk_SGFuncionModificar.Checked, "S", "N")
                    Dr("SGFuncionListar") = IIf(chk_SGFuncionListar.Checked, "S", "N")
                    Dr("SGFuncionExportar") = IIf(chk_SGFuncionExportar.Checked, "S", "N")
                    Dr("SGFuncionGraficar") = IIf(chk_SGFuncionGraficar.Checked, "S", "N")
                    Dr("SGFuncionMaestroDetalle") = IIf(chk_SGFuncionMaestroDetalle.Checked, "S", "N")
                    Dr("SGFuncionPrimaria") = IIf(chk_SGFuncionPrimaria.Checked, "S", "N")

                    Dr("SGFuncionEventoEsp1") = IIf(chk_SGFuncionEventoEsp1.Checked, "S", "N")
                    Dr("SGFuncionEventoEsp2") = IIf(chk_SGFuncionEventoEsp2.Checked, "S", "N")
                    Dr("SGFuncionEventoEsp3") = IIf(chk_SGFuncionEventoEsp3.Checked, "S", "N")
                    Dr("SGFuncionEventoEsp4") = IIf(chk_SGFuncionEventoEsp4.Checked, "S", "N")
                    Dr("SGFuncionEventoEsp5") = IIf(chk_SGFuncionEventoEsp5.Checked, "S", "N")
                    Dr("SGFuncionEventoEsp6") = IIf(chk_SGFuncionEventoEsp6.Checked, "S", "N")
                    Dr("SGFuncionEventoEsp7") = IIf(chk_SGFuncionEventoEsp7.Checked, "S", "N")
                    Dr("SGFuncionEventoEsp8") = IIf(chk_SGFuncionEventoEsp8.Checked, "S", "N")
                    Dr("SGFuncionEventoEsp9") = IIf(chk_SGFuncionEventoEsp9.Checked, "S", "N")
                    Dr("SGFuncionEventoEsp10") = IIf(chk_SGFuncionEventoEsp10.Checked, "S", "N")
                    Dr("SGFuncionEventoEsp1Nombre") = txt_SGFuncionEventoEspNombre1.Text
                    Dr("SGFuncionEventoEsp2Nombre") = txt_SGFuncionEventoEspNombre2.Text
                    Dr("SGFuncionEventoEsp3Nombre") = txt_SGFuncionEventoEspNombre3.Text
                    Dr("SGFuncionEventoEsp4Nombre") = txt_SGFuncionEventoEspNombre4.Text
                    Dr("SGFuncionEventoEsp5Nombre") = txt_SGFuncionEventoEspNombre5.Text
                    Dr("SGFuncionEventoEsp6Nombre") = txt_SGFuncionEventoEspNombre6.Text
                    Dr("SGFuncionEventoEsp7Nombre") = txt_SGFuncionEventoEspNombre7.Text
                    Dr("SGFuncionEventoEsp8Nombre") = txt_SGFuncionEventoEspNombre8.Text
                    Dr("SGFuncionEventoEsp9Nombre") = txt_SGFuncionEventoEspNombre9.Text
                    Dr("SGFuncionEventoEsp10Nombre") = txt_SGFuncionEventoEspNombre10.Text

                    Return oSql.StatemenUpdate(oTb)
                End If
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Function

End Class