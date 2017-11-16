Imports Security_System
Imports Sys_Empaque
Imports System.Net
Imports System.IO

Public Class Fletes
    Inherits System.Web.UI.Page
    Private Ds As New DataSet
    Private oUsr As UserLogin
    Private oCs As New ColeccionPrmSql
    Private Const ABC = "FLETE"
    Private Const ABCD = "FLETECOSECHA"
    Private Const STEP_FLETE = 0
    Private Const STEP_DETALLE = 1
    Private Const STEP_RESUMEN = 2
    Dim proveedorID As String = ""
    Dim FleteID As Integer

    Private Sub Fletes_Init(sender As Object, e As EventArgs) Handles Me.Init
        oUsr = Session("Usr")
        Dim user As String
        user = ValidaUser()
        If String.IsNullOrEmpty(user) Then
            Response.Redirect("~/")
        Else
            lblResponsable.Text = user
        End If
        If oUsr Is Nothing Then
            Response.Redirect("~/")
        Else
            oUsr.Mis.Función = "FL_FLETES"
        End If
        PnlWizard.Visible = False
        PnlListas.Visible = True
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            SetFormConfig()
        End If
        If Not Session("INFORP") Is Nothing Then
            Dim oRp As InfoRp = Session("INFORP")
            If oRp.Enviado Then
                Response.Write("<script>window.open('Show.aspx','_blank');</script>")
            End If
        End If
    End Sub

    'Inicialización
    Private Sub SetFormConfig()
        'Dim oPer As New Permisos(oUsr)
        'If Not oPer.FUN_CONSUL Then
        'Response.Redirect("Agro.aspx")
        'End If
        lblFecha.Text = Now().Date.ToString.Split(" ")(0)
        pnlEventos.Visible = True
        pnlAdd.Visible = False
        With BarEventos1
            .Nuevo = True
            .Eliminar = True
            .Editar = True
            .Exportar = False
            .Filtrar = False
            .Listar = False
        End With
        'With BarEventos2
        '    .Nuevo = True
        '    .Eliminar = True
        '    .Editar = True
        '    .Exportar = False
        '    .Filtrar = False
        '    .Listar = False
        'End With
        With BarEventos3
            .Nuevo = False
            .Eliminar = False
            .Editar = False
            .Exportar = False
            .Filtrar = False
            .Listar = False
            .Especial1 = True
        End With
        LoadLista()
        LoadTipo()
        LoadPrv()
        LoadCamiones()
        LoadContenedor()
        LoadCalidad()
        LoadVariedad()
        GetIndex(DDLTipo, 2)
        DDLTipo.Enabled = False
    End Sub

    Private Sub LoadLista()
        Dim oSql As New SQLFletes(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            oCs.Create("@IDUser", oUsr.keyusu)
            Dim oTabla As DataTable = oSql._List(oSql.ListaFlete, "FLETELISTA", oCs)
            LoadGrid(GridView2, oTabla)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

    Private Sub LoadTipo()
        Dim oSql As New SQLFletes(oUsr)
        Dim lCs As New ColeccionPrmSql
        Try
            lCs.Create("@status", oUsr.Mis.Status)
            lCs.Create("_Tabla", "Tipos")
            lCs.Create("_Qry", oSql.ListaTipo)
            lCs.Create("_Order", "tipofletenombre")
            lCs.Create("_DefaultKey", 0)
            lCs.Create("_DefaultDes", "[SELECCIONAR]")
            LoadCombo(oUsr, DDLTipo, lCs)
            DDLTipo.SelectedIndex = -1

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub

    Private Sub LoadPrv()
        Dim oSql As New SQLFletes(oUsr)
        Dim lCs As New ColeccionPrmSql
        Try
            lCs.Create("_Tabla", "PRV")
            lCs.Create("_Qry", oSql.ListaProveedor)
            lCs.Create("_Order", "ubi_desubi")
            lCs.Create("_Filtro", "TipoUbicaID = 'O'")
            lCs.Create("_DefaultKey", "%")
            lCs.Create("_DefaultDes", "[SELECCIONAR]")
            LoadCombo(oUsr, DDLPrv, lCs)
            DDLPrv.SelectedIndex = -1

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub

    Private Sub LoadRutas(ByVal sKeyOrg As String)
        Dim oSql As New SQLFletes(oUsr)
        Dim lCs As New ColeccionPrmSql
        Try
            lCs.Create("@status", oUsr.Mis.Status)
            lCs.Create("@keyorg", sKeyOrg)
            lCs.Create("_Tabla", "RUT")
            lCs.Create("_Qry", oSql.ListaRuta)
            lCs.Create("_Order", "rut_desrut")
            lCs.Create("_DefaultKey", "%")
            lCs.Create("_DefaultDes", "[SELECCIONAR]")
            LoadCombo(oUsr, DDLRuta, lCs)
            DDLRuta.SelectedIndex = -1

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub

    Private Sub LoadCamiones()
        Dim oSql As New SQLFletes(oUsr)
        Dim lCs As New ColeccionPrmSql
        Try
            lCs.Create("@status", oUsr.Mis.Status)
            lCs.Create("@keytpf", 2) '2 Es unicamente Ajo
            lCs.Create("_Tabla", "CAM")
            lCs.Create("_Qry", oSql.ListaCamion)
            lCs.Create("_Order", "cam_descam")
            lCs.Create("_DefaultKey", 0)
            lCs.Create("_DefaultDes", "[SELECCIONAR]")
            LoadCombo(oUsr, DDLCamíon, lCs)
            DDLCamíon.SelectedIndex = -1

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub

    Private Sub LoadOperadores(ByVal iKeyCam As Integer)
        Dim oSql As New SQLFletes(oUsr)
        Dim lCs As New ColeccionPrmSql
        Try
            lCs.Create("@status", oUsr.Mis.Status)
            lCs.Create("@keycam", iKeyCam)
            lCs.Create("_Tabla", "OPE")
            lCs.Create("_Qry", oSql.ListaOperador)
            lCs.Create("_Order", "ope_nomful")
            lCs.Create("_DefaultKey", 0)
            lCs.Create("_DefaultDes", "[SELECCIONAR]")
            LoadCombo(oUsr, DDLOperador, lCs)
            DDLOperador.SelectedIndex = -1

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub

    Private Sub LoadContenedor()
        Dim oSql As New SQLFletes(oUsr)
        Dim lCs As New ColeccionPrmSql
        Try
            lCs.Create("@status", oUsr.Mis.Status)
            lCs.Create("_Tabla", "CON")
            lCs.Create("_Qry", oSql.ListaContenedor)
            lCs.Create("_Order", "UnidadEmpaqueNombre")
            lCs.Create("_DefaultKey", 0)
            lCs.Create("_DefaultDes", "[SELECCIONAR]")
            LoadCombo(oUsr, DDLConten, lCs)
            DDLConten.SelectedIndex = -1

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub

    Private Sub LoadCalidad()
        Dim oSql As New SQLFletes(oUsr)
        Dim lCs As New ColeccionPrmSql
        Try
            lCs.Create("@cultivo", 1)
            lCs.Create("_Tabla", "CMP")
            lCs.Create("_Qry", oSql.ListaCalidad)
            lCs.Create("_Order", "ClasificaSizeNombre")
            lCs.Create("_DefaultKey", 0)
            lCs.Create("_DefaultDes", "[SELECCIONAR]")
            LoadCombo(oUsr, DDLCal, lCs)
            DDLCal.SelectedIndex = -1

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub

    Private Sub LoadOrigen(ByVal prv As String)
        Dim oSql As New SQLFletes(oUsr)
        Dim lCs As New ColeccionPrmSql
        Try
            lCs.Create("_Tabla", "ORG")
            Dim query As String = oSql.ListaOrigen.Replace("@proveedor", "'" + prv + "'")
            lCs.Create("_Qry", query)
            lCs.Create("_Order", "ubi_desubi")
            lCs.Create("_Filtro", "TipoUbicaID = 'T'")
            lCs.Create("_DefaultKey", "%")
            lCs.Create("_DefaultDes", "[SELECCIONAR]")
            LoadCombo(oUsr, DDLOrigen, lCs)
            DDLOrigen.SelectedIndex = -1

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub

    Private Sub LoadVariedad()
        Dim oSql As New SQLFletes(oUsr)
        Dim lCs As New ColeccionPrmSql
        Try
            lCs.Create("@status", oUsr.Mis.Status)
            lCs.Create("_Tabla", "Variedades")
            lCs.Create("_Qry", oSql.ListaVariedad)
            lCs.Create("_Order", "VariedadNombre")
            lCs.Create("_DefaultKey", 0)
            lCs.Create("_DefaultDes", "[SELECCIONAR]")
            LoadCombo(oUsr, DDLVar, lCs)
            DDLTipo.SelectedIndex = -1
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub

    Private Sub DDLPrv_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDLPrv.SelectedIndexChanged
        LoadRutas(DDLPrv.SelectedValue)
        proveedorID = ObtieneProveedor(DDLPrv.SelectedValue)
        lblFecha.ToolTip = proveedorID
        If proveedorID = "ROOT" Then
            proveedorID = DDLPrv.SelectedValue
            lblFecha.ToolTip = proveedorID
        End If
        'lblFecha.Text = proveedorID
    End Sub

    Private Sub DDLCamíon_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDLCamíon.SelectedIndexChanged
        LoadOperadores(DDLCamíon.SelectedValue)
        lblCamion.ToolTip = "Hola Mundo!!!"
    End Sub

    Private Sub BarEventos1_MsgEvent(sAcción As String) Handles BarEventos1.MsgEvent
        Select Case sAcción
            Case "Nuevo"
                pnlAdd.Visible = True
                pnlLista.Visible = False
            Case "Eliminar"
                If DeleteRowLogica(GridView1) Then
                    LoadLista(oCs)
                End If
            Case "Editar"
                Editar(GridView1)

            Case Else
        End Select
    End Sub

    Private Sub BarEventos2_MsgEvent(sAcción As String) Handles BarEventos2.MsgEvent
        Select Case sAcción
            Case "Nuevo"
                PnlListas.Visible = False
                PnlWizard.Visible = True
            Case "Eliminar"
                Try
                    Dim row As GridViewRow = GridView2.SelectedRow
                    Dim sKey As Integer = row.Cells(0).Text
                    If ValidaExistencia(sKey) Then
                        ScriptManager.RegisterStartupScript(Me, GetType(Page), "FleteExiste", "FleteExiste();", True)
                    ElseIf DeleteFlete(FleteID) Then
                        ScriptManager.RegisterStartupScript(Me, GetType(Page), "FleteEliminado", "FleteEliminado();", True)
                        LoadLista()
                    Else
                        ScriptManager.RegisterStartupScript(Me, GetType(Page), "FleteNoEliminado", "FleteNoEliminado();", True)
                    End If
                Catch ex As Exception
                    ScriptManager.RegisterStartupScript(Me, GetType(Page), "ValidaSelect", "ValidaSelect();", True)
                End Try
            Case "Editar"
                Try
                    Dim row As GridViewRow = GridView2.SelectedRow
                    Dim colsNoVisible = GridView2.DataKeys(row.RowIndex).Values
                    Dim sKey As Integer = row.Cells(0).Text
                    If ValidaExistencia(sKey) Then
                        ScriptManager.RegisterStartupScript(Me, GetType(Page), "FleteExiste", "FleteExiste();", True)
                    Else
                        GetIndex(DDLPrv, ObtienePrv(ObtieneRuta(colsNoVisible(0))))
                        LoadRutas(DDLPrv.SelectedValue)
                        GetIndex(DDLRuta, colsNoVisible(0))
                        GetIndex(DDLCamíon, colsNoVisible(1))
                        LoadOperadores(DDLCamíon.SelectedValue)
                        GetIndex(DDLOperador, colsNoVisible(2))
                        LoadOrigen(ObtieneOrigen(DDLRuta.SelectedValue))
                        txtObsGen.Text = row.Cells(7).Text
                        lblFlete.Text = lblFlete.Text.Replace("#", sKey.ToString)
                        lblFlete.ToolTip = sKey.ToString
                        proveedorID = ObtieneProveedor(DDLPrv.SelectedValue)
                        lblFecha.ToolTip = proveedorID
                        If proveedorID = "ROOT" Then
                            proveedorID = DDLPrv.SelectedValue
                            lblFecha.ToolTip = proveedorID
                        End If
                        SetFormConfigDetalle()
                        LoadLista(oCs)
                        DDLPrv.Enabled = False
                        DDLRuta.Enabled = False
                        PnlListas.Visible = False
                        PnlWizard.Visible = True
                    End If
                Catch ex As Exception
                    Tools.AddErrorLog(oUsr.Mis.Log, ex)
                    ScriptManager.RegisterStartupScript(Me, GetType(Page), "ValidaSelect", "ValidaSelect();", True)
                End Try
            Case Else
        End Select
    End Sub

    Private Sub BarEventos3_MsgEvent(sAcción As String) Handles BarEventos3.MsgEvent
        Select Case sAcción
            Case Else
                Response.Redirect("Fletes.aspx", True)
        End Select
    End Sub

    '================================================================================================================================
    'Acciones con el modelo de datos
    Private Sub LoadLista(ByVal oCs As ColeccionPrmSql)
        Dim oSql As New SQLFleteDetalle(oUsr)
        Try
            oCs.Create("@status", oUsr.Mis.Status)
            oCs.Create("@keyfle", Val(lblFlete.Text.Replace("ID Flete ", "")))
            Dim oTabla As DataTable = oSql._Lista(ABC, oCs)
            LoadGrid(GridView1, oTabla)

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub

    Private Function SaveFlete(ByRef iKey As Integer) As Boolean
        Dim oSql As New SQLFleteDetalle(oUsr)
        Dim oCs As New ColeccionPrmSql
        SaveFlete = False
        Try
            oCs.Create("@keyfle", iKey)
            Dim oTb As DataTable = oSql._Item(ABC, oCs)
            If Not oTb Is Nothing Then
                oTb.Columns("FleteID").Unique = True
                If oTb.Rows.Count = 0 Then
                    oCs.Clear()
                    oCs.Create("@FleteFecha", CDate(lblFecha.Text))
                    oCs.Create("@keyrut", DDLRuta.SelectedValue)
                    oCs.Create("@keytpf", DDLTipo.SelectedValue)
                    oCs.Create("@keytra", ObtieneTransportista())
                    oCs.Create("@keycam", DDLCamíon.SelectedValue)
                    oCs.Create("@keyope", DDLOperador.SelectedValue)
                    oCs.Create("@keyusu", oUsr.keyusu)
                    'oCs.Create("@observ", txtObs.Text)
                    Dim nFolio As Double = Now.DayOfYear + Now.Hour / 24 + Now.Minute / (60 * 24) + Now.Second / (60 * 60 * 24)
                    oCs.Create("@observ", txtObsGen.Text)
                    oCs.Create("@keyfle", 0, "out")

                    'Dim Csl As New ColeccionPrmSql : Csl.Create("_VALOR", "NEXTID")
                    'iKey = oSql._Valor(oSql.NextID, Csl)
                    'oCs.Create("@keyfle", iKey)
                    If oSql.ExecuteStore("SP_INS_FLETE", oCs) Then
                        iKey = oCs.ItemValue("@keyfle")
                        lblFlete.ToolTip = iKey.ToString
                        lblFlete.Text = lblFlete.Text.Replace("#", iKey.ToString)
                        Return True
                    End If
                Else
                    Dim Dr As DataRow = oTb.Rows(0)
                    Dr("RutaID") = DDLRuta.SelectedValue
                    Dr("CamionID") = DDLCamíon.SelectedValue
                    Dr("OperadorID") = DDLOperador.SelectedValue
                    Dr("FleteObservacion") = txtObsGen.Text
                    Dr("TransportistaID") = ObtieneTransportista()
                    Return oSql.StatemenUpdate(oTb)
                End If

            End If

        Catch ex As Exception
            'Dim s As String = ex.TargetSite.Name
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Private Function Save(ByVal iKeyFle As Integer) As Boolean
        Dim oSql As New SQLFleteDetalle(oUsr)
        Dim oCs As New ColeccionPrmSql
        Save = False
        Try
            oCs.Create("@keyf", iKeyFle)
            oCs.Create("@keydfl", Val(txtKey.Text))
            Dim oTb As DataTable = oSql._ItemD(ABCD, oCs)
            If Not oTb Is Nothing Then
                oTb.Columns("CosechaID").Unique = True
                oTb.Columns("FleteID").Unique = True
                If oTb.Rows.Count = 0 Then
                    Dim Dr As DataRow = oTb.NewRow
                    'Dim iKey As Integer = oSql._Valor(oSql.NextIDD, "NEXTID", oCs)
                    Dim iKey As Integer = 1
                    Dr("CosechaID") = Maximo(iKey)
                    Dr("FleteID") = iKeyFle
                    'Dr("dfl_keypst") = DDLPost.SelectedValue
                    Dr("EnvaseID") = DDLConten.SelectedValue
                    Dr("CosechaCantidad") = Val(txtCan.Text)
                    Dr("UbicacionID") = DDLOrigen.SelectedValue
                    Dr("ClasificaSizeID") = DDLCal.SelectedValue
                    Dr("CosechaObservacion") = txtObs.Text
                    Dr("VariedadID") = DDLVar.SelectedValue
                    oTb.Rows.Add(Dr)
                    Return oSql.StatemenInsert(oTb)
                Else
                    Dim Dr As DataRow = oTb.Rows(0)
                    'Dr("dfl_keypst") = DDLPost.SelectedValue
                    Dr("EnvaseID") = DDLConten.SelectedValue
                    Dr("CosechaCantidad") = Val(txtCan.Text)
                    Dr("UbicacionID") = DDLOrigen.SelectedValue
                    Dr("ClasificaSizeID") = DDLCal.SelectedValue
                    Dr("CosechaObservacion") = txtObs.Text
                    Dr("VariedadID") = DDLVar.SelectedValue
                    Return oSql.StatemenUpdate(oTb)
                End If
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Private Function DeleteRowLogica(ByVal oGrid As GridView) As Boolean
        Dim oSql As New SQLFleteDetalle(oUsr)
        Dim oCs As New ColeccionPrmSql
        DeleteRowLogica = False
        Try
            Dim sKey As String = oGrid.DataKeys(oGrid.SelectedRow.DataItemIndex).Value
            Return DeleteDetalle(sKey)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Private Function DeleteDetalle(ByVal IDD As Integer) As Boolean
        Dim oSQL As New SQLFleteDetalle(oUsr)
        Dim cs As New ColeccionPrmSql
        DeleteDetalle = False
        Try
            cs.Create("@IDF", IDD)
            cs.Create("@keyf", lblFlete.ToolTip)
            Return oSQL.ExecuteQry(oSQL.DelDetalle, cs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Private Sub Editar(ByVal oGrid As GridView)
        Dim oSql As New SQLFleteDetalle(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            Dim sKey As String = oGrid.DataKeys(oGrid.SelectedRow.DataItemIndex).Value
            pnlAdd.Visible = True
            pnlLista.Visible = False
            oCs.Create("@keyf", lblFlete.ToolTip)
            oCs.Create("@keydfl", sKey)
            Dim oTb As DataTable = oSql._ItemD(ABCD, oCs)
            If Not oTb Is Nothing Then
                If oTb.Rows.Count > 0 Then
                    Dim Dr As DataRow = oTb.Rows(0)
                    txtKey.Text = Dr("CosechaID")
                    GetIndex(DDLOrigen, Dr("UbicacionID"))
                    GetIndex(DDLVar, Dr("VariedadID"))
                    txtCan.Text = Dr("CosechaCantidad")
                    txtObs.Text = Dr("CosechaObservacion")
                    GetIndex(DDLCal, Dr("ClasificaSizeID"))
                    GetIndex(DDLConten, Dr("EnvaseID"))
                End If
            End If

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub

    Protected Sub btnAceptar_Click(sender As Object, e As ImageClickEventArgs) Handles btnAceptar.Click
        Dim iKeyFle As Integer = Val(lblFlete.ToolTip)
        If SaveFlete(iKeyFle) Then
            Save(iKeyFle)
        End If
        SetFormConfigDetalle()
        LoadLista(oCs)
    End Sub

    'Inicializar Detalle
    Private Sub SetFormConfigDetalle()
        Try
            DDLCal.SelectedIndex = -1
            DDLConten.SelectedIndex = -1
            DDLOrigen.SelectedIndex = -1
            DDLVar.SelectedIndex = -1
            txtKey.Text = ""
            txtCan.Text = ""
            txtObs.Text = ""
            pnlAdd.Visible = False
            pnlLista.Visible = True
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub

    Private Sub SetFormConfigGeneral()
        Try
            DDLPrv.SelectedIndex = -1
            DDLRuta.SelectedIndex = -1
            DDLCamíon.SelectedIndex = -1
            DDLOperador.SelectedIndex = -1
            DDLCal.SelectedIndex = -1
            DDLConten.SelectedIndex = -1
            DDLOrigen.SelectedIndex = -1
            DDLVar.SelectedIndex = -1
            txtKey.Text = ""
            txtCan.Text = ""
            txtObs.Text = ""
            pnlAdd.Visible = False
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub

    Private Sub Resumen()
        Dim sFlete As String = String.Empty
        Try
            sFlete = Space(10) + "GRUPO U" + Space(10) + vbCrLf
            sFlete += "Orden de Flete:" & lblFlete.ToolTip & vbCrLf
            sFlete += "Fecha:" & FechaFlete(lblFlete.ToolTip).ToString & vbCrLf
            sFlete += "Proveedor:" & DDLPrv.SelectedItem.Text & vbCrLf
            'sFlete += "Movimiento:" & lblTipoFlete.Text & vbCrLf
            sFlete += "Movimiento:" & DDLTipo.SelectedItem.Text & vbCrLf
            sFlete += "Ruta:" & DDLRuta.SelectedItem.Text & vbCrLf
            'sFlete += "Costo:" & lblCamion.ToolTip & vbCrLf
            sFlete += "Transporta/Camión:" & DDLCamíon.SelectedItem.Text & vbCrLf
            sFlete += "Operador:" & DDLOperador.SelectedItem.Text & vbCrLf
            sFlete += "Responsable:" & lblResponsable.Text & vbCrLf
            For Each Dgr As GridViewRow In GridView1.Rows
                If Dgr.RowType = DataControlRowType.DataRow Then
                    sFlete += "---------------------------" & vbCrLf
                    sFlete += "Ubicacion:" & Dgr.Cells(1).Text & vbCrLf
                    sFlete += "Cantidad: " & Dgr.Cells(3).Text & "  Unidad: " & Dgr.Cells(4).Text & vbCrLf
                    sFlete += "Variedad:" & Dgr.Cells(2).Text & vbCrLf
                    sFlete += "Calidad: " & Dgr.Cells(5).Text & vbCrLf
                End If
            Next
            sFlete += "Observación: " & txtObsGen.Text & vbCrLf
            sFlete += "FIN RECIBO" & vbCrLf & vbCrLf
            Select Case DDLTipo.SelectedValue
                Case 2
                    Select Case lblFecha.ToolTip
                        Case "O1" 'Aguilares
                            sFlete += "Fecha de emisión: 03-MAR-15" & vbCrLf
                            sFlete += "F-100-PAA-64" & vbCrLf
                            sFlete += "Rev: 02" & vbCrLf
                            sFlete += "PRODUCTO CERTIFICADO GLOBAL"
                        Case "O2" 'La Mina
                            sFlete += "Fecha de emisión: 03-MAR-15" & vbCrLf
                            sFlete += "F-100-PAM-01" & vbCrLf
                            sFlete += "Rev: 02" & vbCrLf
                            sFlete += "PRODUCTO CERTIFICADO GLOBAL" & vbCrLf
                            sFlete += "4052852483681"
                        Case Else
                            'Externos
                            sFlete += "Fecha de emisión: 03-MAR-15" & vbCrLf
                            sFlete += "F-100-PAA-64" & vbCrLf
                            sFlete += "Rev: 02" & vbCrLf
                            sFlete += "PRODUCTO CERTIFICADO GLOBAL"
                    End Select
                Case 4
                    sFlete += "Fecha de emisión: 30-JUN-11" & vbCrLf
                    sFlete += "F-100-EAG-02" & vbCrLf
                    sFlete += "Rev: 02" & vbCrLf
                    sFlete += ""
            End Select

            txtResumen.Text = sFlete

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub

    Private Function FechaFlete(ByVal iKey As Integer) As Date
        Dim oSql As New SQLFleteDetalle(oUsr)
        Dim Cs As New ColeccionPrmSql
        FechaFlete = Nothing
        Try
            Cs.Create("@keyfle", iKey)
            Cs.Create("_VALOR", "FleteFechaRegistro")
            Return oSql._Valor(oSql.Item, Cs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Protected Sub Wizard1_FinishButtonClick(sender As Object, e As WizardNavigationEventArgs) Handles Wizard1.FinishButtonClick
        'Terminar el flete
        CallReporte(txtResumen.Text)
    End Sub

    Private Sub Wizard1_NextButtonClick(sender As Object, e As WizardNavigationEventArgs) Handles Wizard1.NextButtonClick
        Select Case e.NextStepIndex
            Case STEP_RESUMEN
                Resumen()
        End Select
    End Sub

    Private Sub CallReporte(ByVal sTexto As String)
        Dim oRp As New InfoRp
        Try
            Dim dtOut As New DataTable("VW_FLETE")
            dtOut.MinimumCapacity = 100
            dtOut.CaseSensitive = False

            ' Se crean todas las columnas de las tablas.
            dtOut.Columns.Add("row", GetType(String)).MaxLength = 255
            Ds.Tables.Add(dtOut)
            Dim oLineas() As String = sTexto.Split(vbLf)
            For Each sLinea As String In oLineas
                Dim dr As DataRow = dtOut.NewRow
                dr("row") = sLinea
                dtOut.Rows.Add(dr)
            Next

            With oRp
                .Reporte = "FLETE"
                .Nombre = "ORDEN_DE_FLETE_" & lblFlete.ToolTip & ".PDF"
                .Parametros.Create("_MAILFROM", "fletesajo@grupou.mx")
                Dim ArTo As List(Of String) = GetMailto("SENDMAIL AJO", "_MAILTO")
                .Parametros.Create("_MAILTO", ArTo)
                .OD = Ds
                .Enviado = True
            End With

            Session.Add("INFORP", oRp)
            'Nuevo flete
            Response.Redirect("Fletes.aspx", True)

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub

    Private Function GetMailto(ByVal sSec As String, ByVal sKey As String) As List(Of String)
        Dim oSql As New SQLSetting(oUsr)
        Dim oCs As New ColeccionPrmSql
        Dim Ar As New List(Of String)
        GetMailto = Nothing
        Try
            oCs.Create("@secion", sSec)
            oCs.Create("@sname", sKey)
            Dim oTb As DataTable = oSql._Item("MAILTO", oCs)
            If Not oTb Is Nothing Then
                If oTb.Rows.Count > 0 Then
                    For Each sItem As String In oTb.Rows(0).Item("set_svalue").ToString.Split(";")
                        If sItem <> "" Then
                            Ar.Add(sItem)
                        End If
                    Next
                End If
            End If

            Return Ar

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Protected Sub btnCancelar_Click(sender As Object, e As ImageClickEventArgs) Handles btnCancelar.Click
        SetFormConfigDetalle()
        pnlAdd.Visible = False
        pnlLista.Visible = True
    End Sub

    Private Function ValidaUser() As String
        Dim oSql As New SQLFletes(oUsr)
        Dim oCs As New ColeccionPrmSql
        ValidaUser = ""
        Try
            oCs.Create("@userid", oUsr.keyusu)
            oCs.Create("@tipoflete", 2)
            oCs.Create("_VALOR", "SgUserEmail")
            Return oSql._Valor(oSql.ValUser, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Private Function ObtieneTransportista() As String
        Dim oSql As New SQLFletes(oUsr)
        Dim oCs As New ColeccionPrmSql
        ObtieneTransportista = ""

        Try
            oCs.Create("@camionid", DDLCamíon.SelectedValue)
            oCs.Create("_VALOR", "TransportistaID")
            Return oSql._Valor(oSql.ValTransportista, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Private Function ObtieneProveedor(ByVal provee As String) As String
        Dim oSql As New SQLFletes(oUsr)
        Dim oCs As New ColeccionPrmSql
        ObtieneProveedor = ""
        Try
            oCs.Create("@prv", provee)
            oCs.Create("_VALOR", "UbicacionPadreID")
            Return oSql._Valor(oSql.ValProveedor, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Private Sub DDLRuta_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDLRuta.SelectedIndexChanged
        LoadOrigen(ObtieneOrigen(DDLRuta.SelectedValue))
    End Sub

    Private Function ObtieneOrigen(ByVal ruta As String) As String
        Dim oSql As New SQLFletes(oUsr)
        Dim oCs As New ColeccionPrmSql
        ObtieneOrigen = ""
        Try
            oCs.Create("@ruta", ruta)
            oCs.Create("_VALOR", "OrigenID")
            Return oSql._Valor(oSql.ValOrigen, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Private Sub GridView2_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView2.PageIndexChanging
        GridView2.PageIndex = e.NewPageIndex
        LoadLista()
    End Sub

    Private Function ValidaExistencia(ByVal sKey As Integer) As Boolean
        Dim oSql As New SQLFletes(oUsr)
        Dim oCs As New ColeccionPrmSql
        ValidaExistencia = False

        Try
            'Dim sKey As String = oGrid.DataKeys(oGrid.SelectedRow.DataItemIndex).Value
            oCs.Create("@Flete", sKey)
            Dim oTb As DataTable = oSql._Item(ABCD, oCs, oSql.Item)
            If Not oTb Is Nothing Then
                If oTb.Rows.Count > 0 Then
                    Return True
                Else
                    FleteID = sKey
                End If
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Private Function DeleteFlete(ByVal IDD As Integer) As Boolean
        Dim oSQL As New SQLFletes(oUsr)
        Dim cs As New ColeccionPrmSql
        DeleteFlete = False
        Try
            cs.Create("@IDF", IDD)
            oSQL.ExecuteQry(oSQL.DelFleteCosecha, cs)
            oSQL.ExecuteQry(oSQL.DelFlete, cs)
            Return True
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Private Function ObtienePrv(ByVal ruta As String) As String
        Dim oSql As New SQLFletes(oUsr)
        Dim oCs As New ColeccionPrmSql
        ObtienePrv = ""
        Try
            oCs.Create("@ruta", ruta)
            oCs.Create("_VALOR", "UbicacionID")
            Return oSql._Valor(oSql.ValPrv, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Private Function ObtieneRuta(ByVal rutaid As String) As String
        Dim oSql As New SQLFletes(oUsr)
        Dim oCs As New ColeccionPrmSql
        ObtieneRuta = ""
        Try
            oCs.Create("@ruta", rutaid)
            oCs.Create("_VALOR", "OrigenID")
            Return oSql._Valor(oSql.ValRuta, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Private Function Maximo(ByVal max As Integer) As Integer
        Dim num As Integer = 1
        Maximo = 1
        Try
            For Each Dgr As GridViewRow In GridView1.Rows
                If Dgr.RowType = DataControlRowType.DataRow Then
                    num = Convert.ToInt32(Dgr.Cells(0).Text)
                    If num >= max Then
                        max = num + 1
                    End If
                End If
            Next
            Return max
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Private Sub txtCan_TextChanged(sender As Object, e As EventArgs) Handles txtCan.TextChanged
        If IsNumeric(txtCan.Text) Then
            If Not (txtCan.Text Mod 1 = 0) Then
                txtCan.Text = ""
            End If
        Else
            txtCan.Text = ""
        End If
    End Sub
End Class