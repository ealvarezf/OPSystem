Imports Security_System
'Ernesto Alvarez Flores
'Prueba de colaboración
Public Class MovAlmacen
    Inherits System.Web.UI.Page
    Private Ds As New DataSet
    Private oUsr As UserLogin
    Private oCs As New ColeccionPrmSql

#Region "Inicializar datos"
    Private Function abc() As Boolean
        'Paco
    End Function


    Private Sub SetFormConfig()
        Try
            pnlEventos.Visible = True
            pnlFiltros.Visible = False
            pnlAdd.Visible = False
            pnlListar.Visible = True

            With BarEventos1
                .Especial1.Boton.Visible = True
                .Especial1.Boton.ToolTip = "Aplica movimiento a inventarios"
                .Especial1.Boton.ImageUrl = "~/Img/PInventario.jpg"
            End With

            LoadComboAlmacen(ddl_UbicacionAlmacenID)
            LoadComboAlmacen(Search_ddl_UbicacionAlmacenID)
            LoadComboRancho(ddl_UbicacionRanchoID)
            LoadComboRancho(Search_ddl_UbicacionRanchoID)
            LoadComboTabla(ddl_UbicacionTablaID, "")
            LoadComboTabla(Search_ddl_UbicacionTablaID, "")
            LoadComboCultivo(ddl_CultivoID)
            LoadComboCultivo(Search_ddl_CultivoID)
            LoadComboVariedad(ddl_VariedadID, 0)
            LoadComboVariedad(Search_ddl_VariedadID, 0)
            LoadComboProcesos(ddl_ProcesoID)
            LoadComboProcesos(Search_ddl_ProcesoID)

            txt_MovAlmacenObs.MaxLength = 255
            GridView1.PageSize = 10
            txt_MovAlmacenID.Enabled = False

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub
#End Region

#Region "CargaDatos"
    Private Sub LoadLista()
        Dim oSql As New SQLMovimientosAlmacen(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            If Search_txt_MovAlmacenFecha.Text = "" Then Search_txt_MovAlmacenFecha.Text = "01/01/2017"
            oCs.Create("@EmpresaID", oUsr.Mis.Empresa)
            oCs.Create("@MovAlmacenSentido", Search_ddl_MovAlmacenSentido.SelectedValue)
            oCs.Create("@MovAlmacenFecha", CDate(Search_txt_MovAlmacenFecha.Text))
            oCs.Create("@UbicacionID", Search_ddl_UbicacionAlmacenID.SelectedValue)
            oCs.Create("@TablaID", Search_ddl_UbicacionTablaID.SelectedValue)
            oCs.Create("@VariedadID", IIf(Search_ddl_VariedadID.SelectedValue = 0, "%", Search_ddl_VariedadID.SelectedValue))
            Dim oTabla As DataTable = oSql._List(oSql.List, "MOVIMIENTOALMACEN", oCs)
            LoadGrid(GridView1, oTabla)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub
    Private Sub LoadComboAlmacen(ByVal ddl As DropDownList)
        Dim oSql As New SQLAlmacen(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            oCs.Create("@EmpresaID", oUsr.Mis.Empresa)
            Dim oTb As DataTable = oSql._List(oSql.List, "ALMACENES", oCs)
            If Not oTb Is Nothing Then
                oTb.DefaultView.Sort = "UbicacionNombre"
                Dim Dr As DataRow = oTb.NewRow
                Dr("UbicacionID") = "%"
                Dr("UbicacionNombre") = "[SELECCIONAR]"
                oTb.Rows.Add(Dr)
                LoadCombo(oUsr, ddl, oTb.DefaultView)
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub
    Private Sub LoadComboRancho(ByVal ddl As DropDownList)
        Dim oSql As New SQLRanchos(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            Dim oTb As DataTable = oSql._List(oSql.List, "RANCHOS", oCs)
            If Not oTb Is Nothing Then
                oTb.DefaultView.Sort = "UbicacionNombre"
                Dim Dr As DataRow = oTb.NewRow
                Dr("RanchoID") = "%"
                Dr("UbicacionNombre") = "[SELECCIONAR]"
                oTb.Rows.Add(Dr)
                LoadCombo(oUsr, ddl, oTb.DefaultView)
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub
    Private Sub LoadComboTabla(ByVal ddl As DropDownList, ByVal sKey As String)
        Dim oSql As New SQLTablas(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            oCs.Create("@RanchoID", sKey)
            Dim oTb As DataTable = oSql._List(oSql.List, "TABLAS", oCs)
            If Not oTb Is Nothing Then
                oTb.DefaultView.Sort = "UbicacionNombre"
                Dim Dr As DataRow = oTb.NewRow
                Dr("TablaID") = "%"
                Dr("UbicacionNombre") = "[SELECCIONAR]"
                oTb.Rows.Add(Dr)
                LoadCombo(oUsr, ddl, oTb.DefaultView)
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub
    Private Sub LoadComboCultivo(ByVal ddl As DropDownList)
        Dim oSql As New SQLCultivos(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            Dim oTb As DataTable = oSql._List(oSql.List, "CULTIVOS", oCs)
            If Not oTb Is Nothing Then
                oTb.DefaultView.Sort = "CultivoNombre"
                Dim Dr As DataRow = oTb.NewRow
                Dr("CultivoID") = "0"
                Dr("CultivoNombre") = "[SELECCIONAR]"
                oTb.Rows.Add(Dr)
                LoadCombo(oUsr, ddl, oTb.DefaultView)
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub
    Private Sub LoadComboVariedad(ByVal ddl As DropDownList, ByVal iKey As Integer)
        Dim oSql As New SQLVariedades(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            oCs.Create("@CultivoID", iKey)
            Dim oTb As DataTable = oSql._List(oSql.List, "VARIEDADES", oCs)
            If Not oTb Is Nothing Then
                oTb.DefaultView.Sort = "VariedadNombre"
                Dim Dr As DataRow = oTb.NewRow
                Dr("VariedadID") = "0"
                Dr("VariedadNombre") = "[SELECCIONAR]"
                oTb.Rows.Add(Dr)
                LoadCombo(oUsr, ddl, oTb.DefaultView)
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub
    Private Sub LoadComboProcesos(ByVal ddl As DropDownList)
        Dim oSql As New SQLProcesos(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            Dim oTb As DataTable = oSql._List(oSql.List, "PROCESOS", oCs)
            If Not oTb Is Nothing Then
                oTb.DefaultView.Sort = "ProcesoNombre"
                Dim Dr As DataRow = oTb.NewRow
                Dr("ProcesoID") = "0"
                Dr("ProcesoNombre") = "[SELECCIONAR]"
                oTb.Rows.Add(Dr)
                LoadCombo(oUsr, ddl, oTb.DefaultView)
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub
    Private Sub SetFormEdit(ByVal sAcc As String, ByVal oGrid As GridView, Optional ByVal sTooltip As String = "")
        Dim oSql As New SQLMovimientosAlmacen(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            If sAcc <> "Nuevo" Then
                If oGrid.SelectedRow Is Nothing Then
                    Response.Write("<script>window.alert('No ha seleccionado Registro ');</script>")
                    Exit Sub
                End If
            End If

            pnlEventos.Visible = False
            pnlFiltros.Visible = False
            pnlAdd.Visible = True
            pnlListar.Visible = False

            lblAcción.ToolTip = sAcc
            lblAcción.Text = IIf(sTooltip = "", sAcc, sTooltip)
            ImgBtnAceptar.Enabled = True

            Select Case sAcc
                Case "Nuevo"
                    'Inicializar datos

                Case "Editar"
                    Dim sKey As String = oGrid.DataKeys(oGrid.SelectedRow.RowIndex).Value
                    Dim sSentido As String = sKey.Substring(0, 1)
                    Dim iMovAlmacenID As Integer = sKey.Substring(1, sKey.Length - 1)
                    oCs.Create("@MovAlmacenSentido", sSentido)
                    oCs.Create("@MovAlmacenID", iMovAlmacenID)
                    Dim oTb As DataTable = oSql._Item(oSql.ItemInfo, "MOVIMIENTOALMACEN", oCs)
                    If Not oTb Is Nothing Then
                        For Each Dr As DataRow In oTb.Rows
                            GetIndex(ddl_MovAlmacenSentido, Dr("MovAlmacenSentido"))
                            txt_MovAlmacenID.Text = Dr("MovAlmacenID").ToString
                            txt_MovAlmacenFecha.Text = Dr("MovAlmacenFecha").ToString
                            GetIndex(ddl_UbicacionAlmacenID, Dr("UbicacionID").ToString)
                            GetIndex(ddl_UbicacionRanchoID, Dr("RanchoID").ToString)
                            LoadComboTabla(ddl_UbicacionTablaID, Dr("RanchoID").ToString)
                            GetIndex(ddl_UbicacionTablaID, Dr("TablaID").ToString)
                            GetIndex(ddl_CultivoID, Dr("CultivoID").ToString)
                            LoadComboVariedad(ddl_VariedadID, Dr("CultivoID").ToString)
                            GetIndex(ddl_VariedadID, Dr("VariedadID").ToString)
                            GetIndex(ddl_ProcesoID, Dr("ProcesoID").ToString)
                            txt_MovAlmacenObs.Text = Dr("MovAlmacenObs")
                            Exit For
                        Next
                    End If

                Case "Eliminar", "Especial1"
                    Dim sKey As String = oGrid.DataKeys(oGrid.SelectedRow.RowIndex).Value
                    Dim sSentido As String = sKey.Substring(0, 1)
                    Dim iMovAlmacenID As Integer = sKey.Substring(1, sKey.Length - 1)
                    oCs.Create("@MovAlmacenSentido", sSentido)
                    oCs.Create("@MovAlmacenID", iMovAlmacenID)
                    Dim oTb As DataTable = oSql._Item(oSql.ItemInfo, "MOVIMIENTOALMACEN", oCs)
                    If Not oTb Is Nothing Then
                        For Each Dr As DataRow In oTb.Rows
                            GetIndex(ddl_MovAlmacenSentido, Dr("MovAlmacenSentido"))
                            txt_MovAlmacenID.Text = Dr("MovAlmacenID").ToString
                            txt_MovAlmacenFecha.Text = Dr("MovAlmacenFecha").ToString
                            GetIndex(ddl_UbicacionAlmacenID, Dr("UbicacionID").ToString)
                            GetIndex(ddl_UbicacionRanchoID, Dr("RanchoID").ToString)
                            LoadComboTabla(ddl_UbicacionTablaID, Dr("RanchoID").ToString)
                            GetIndex(ddl_UbicacionTablaID, Dr("TablaID").ToString)
                            GetIndex(ddl_CultivoID, Dr("CultivoID").ToString)
                            LoadComboVariedad(ddl_VariedadID, Dr("CultivoID").ToString)
                            GetIndex(ddl_VariedadID, Dr("VariedadID").ToString)
                            GetIndex(ddl_ProcesoID, Dr("ProcesoID").ToString)
                            txt_MovAlmacenObs.Text = Dr("MovAlmacenObs")

                            If sAcc = "Especial1" And Dr("MovAlmacenStsProceso") = "B" Then
                                Response.Write("<script>window.alert('Registro ya aplicado a inventario ');</script>")
                                ImgBtnAceptar.Enabled = False
                            End If

                            Exit For
                        Next
                    End If

                    ddl_MovAlmacenSentido.Enabled = False
                    txt_MovAlmacenID.Enabled = False
                    txt_MovAlmacenFecha.Enabled = False
                    ddl_UbicacionAlmacenID.Enabled = False
                    ddl_UbicacionRanchoID.Enabled = False
                    ddl_UbicacionTablaID.Enabled = False
                    ddl_CultivoID.Enabled = False
                    ddl_VariedadID.Enabled = False
                    ddl_ProcesoID.Enabled = False
                    txt_MovAlmacenObs.Enabled = False

            End Select

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub
#End Region
#Region "Eventos"
    Private Sub MovAlmacen_Init(sender As Object, e As EventArgs) Handles Me.Init
        oUsr = Session("Usr")
        oUsr.Mis.Empresa = 15
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            SetFormConfig()
            LoadLista()
        End If
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
            Case "Especial1"
                SetFormEdit(sAcción, GridView1)

            Case Else
        End Select
    End Sub

    Protected Sub ddl_UbicacionRanchoID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_UbicacionRanchoID.SelectedIndexChanged
        LoadComboTabla(ddl_UbicacionTablaID, ddl_UbicacionRanchoID.SelectedValue)
    End Sub
    Protected Sub ddl_CultivoID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_CultivoID.SelectedIndexChanged
        LoadComboVariedad(ddl_VariedadID, ddl_CultivoID.SelectedValue)
    End Sub
    Private Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        sender.PageIndex = e.NewPageIndex
        LoadLista()
    End Sub
    Protected Sub ImgBtnAceptar_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnAceptar.Click
        Select Case lblAcción.ToolTip
            Case "Eliminar"
                If Delete() Then
                    LoadLista()
                    SetFormConfig()
                End If
            Case "Especial1"
                If Aplica() Then
                    LoadLista()
                    SetFormConfig()
                End If
            Case Else
                If SaveTable() Then
                    LoadLista()
                    SetFormConfig()
                End If
        End Select
    End Sub
    Protected Sub ImgBtnCancelar_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnCancelar.Click
        SetFormConfig()
    End Sub
    Protected Sub imgBtnAplicaFiltro_Click(sender As Object, e As ImageClickEventArgs) Handles imgBtnAplicaFiltro.Click
        pnlFiltros.Visible = False
        LoadLista()
    End Sub
    Protected Sub imgbtnCancelaFiltro_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnCancelaFiltro.Click
        pnlFiltros.Visible = False
    End Sub
#End Region

#Region "AccionesDatos"
    Private Function Delete() As Boolean
        Dim oSql As New SQLMovimientosAlmacen(oUsr)
        Dim oCs As New ColeccionPrmSql
        Delete = False
        Try
            oCs.Create("@MovAlmacenSentido", ddl_MovAlmacenSentido.SelectedValue)
            oCs.Create("@MovAlmacenID", Val(txt_MovAlmacenID.Text))
            Return oSql.ExecuteQry(oSql.Delete, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Function
    Private Function SaveTable() As Boolean
        Dim oSql As New SQLMovimientosAlmacen(oUsr)
        Dim oCs As New ColeccionPrmSql
        SaveTable = False
        Try
            oCs.Create("@MovAlmacenSentido", ddl_MovAlmacenSentido.SelectedValue)
            oCs.Create("@MovAlmacenID", Val(txt_MovAlmacenID.Text))
            Dim oTb As DataTable = oSql._Item(oSql.Item, "MOVIMIENTOALMACEN", oCs)
            If Not oTb Is Nothing Then
                oTb.Columns("MovAlmacenSentido").Unique = True
                oTb.Columns("MovAlmacenID").Unique = True
                If oTb.Rows.Count = 0 Then
                    Dim Dr As DataRow = oTb.NewRow
                    Dr("MovAlmacenSentido") = ddl_MovAlmacenSentido.SelectedValue
                    Select Case Dr("MovAlmacenSentido")
                        Case "E"
                            Dr("MovAlmacenID") = oSql._Value(oSql.NextEntradaID, "NextID", oCs)
                        Case "S"
                            Dr("MovAlmacenID") = oSql._Value(oSql.NextSalidaID, "NextID", oCs)
                    End Select
                    Dr("MovAlmacenObs") = txt_MovAlmacenObs.Text
                    Dr("MovAlmacenStsProceso") = "A"
                    Dr("ProcesoID") = ddl_ProcesoID.SelectedValue
                    Dr("EmpresaID") = oUsr.Mis.Empresa
                    Dr("UbicacionID") = ddl_UbicacionAlmacenID.SelectedValue
                    Dr("TablaID") = ddl_UbicacionTablaID.SelectedValue
                    Dr("VariedadID") = ddl_VariedadID.SelectedValue
                    Dr("MovAlmacenFecha") = CDate(txt_MovAlmacenFecha.Text)
                    oTb.Rows.Add(Dr)
                    Return oSql.StatemenInsert(oTb)
                Else
                    ' No hay opción
                    Dim Dr As DataRow = oTb.Rows(0)
                    Dr("MovAlmacenObs") = txt_MovAlmacenObs.Text
                    Dr("MovAlmacenStsProceso") = "A"
                    Dr("ProcesoID") = ddl_ProcesoID.SelectedValue
                    Dr("EmpresaID") = oUsr.Mis.Empresa
                    Dr("UbicacionID") = ddl_UbicacionAlmacenID.SelectedValue
                    Dr("TablaID") = ddl_UbicacionTablaID.SelectedValue
                    Dr("VariedadID") = ddl_VariedadID.SelectedValue
                    Dr("MovAlmacenFecha") = CDate(txt_MovAlmacenFecha.Text)
                    Return oSql.StatemenUpdate(oTb)
                End If
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Function
    Private Function Aplica() As Boolean
        Dim oSql As New SQLMovimientosAlmacen(oUsr)
        Dim oCs As New ColeccionPrmSql
        Aplica = False
        Try
            oCs.Create("@MovAlmacenSentido", ddl_MovAlmacenSentido.SelectedValue)
            oCs.Create("@MovAlmacenID", Val(txt_MovAlmacenID.Text))
            Return oSql.ExecuteStore("SP_INV_APLICA_REGISTRO_MANUAL", oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Function
#End Region

End Class