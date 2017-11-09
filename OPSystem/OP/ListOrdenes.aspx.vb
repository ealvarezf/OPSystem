Imports Infragistics.Web.UI.GridControls
Imports Infragistics.Web.UI.GridControls.WebDataGrid
Imports Security_System
Public Class ListOrdenes
    Inherits System.Web.UI.Page
    Private Ds As New DataSet
    Private oUsr As UserLogin
    Private oCs As New ColeccionPrmSql
    Private sKeyProceso As String

    Private Sub ListOrdenes_Init(sender As Object, e As EventArgs) Handles Me.Init
        oUsr = Session("Usr")
        SqlDataSource1.ConnectionString = CnxDefault.GetDataStore
        sKeyProceso = Request.Params("pid")
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadComboProcesos()
            LoadComboTurnos()
            LoadComboResponsables()
            SetFormConfig()
            LoadLista()
        End If
    End Sub

    Private Sub SetFormConfig()
        pnlEventos.Visible = True
        pnlAdd.Visible = False
        pnlListar.Visible = True
        With BarEventos1
            .EtiquetaMaestro.Visible = True
            .EtiquetaMaestro.Text = "Ordenes de producción " & ddl_ProcesoID.SelectedItem.Text
            .EtiquetaDetalle.Visible = False
            .Back.Visible = False

            .Consultar.Boton.Visible = True
            .Nuevo.Boton.Visible = True
            .Editar.Boton.Visible = True
            .Eliminar.Boton.Visible = False

            .Especial1.Boton.Visible = True
            .Especial1.Boton.ToolTip = "Registro de gasto"
            .Especial1.Boton.ImageUrl = "~/Img/descarga.png"

            .Especial2.Boton.Visible = True
            .Especial2.Boton.ImageUrl = "~/Img/EventosEsp.jpg"
            .Especial2.Boton.ToolTip = "Registro de producción"
        End With
        'Valores predeterminados
        txt_OrdenID.Text = ""
        txt_OrdenID.Enabled = False
        wdp_OrdenFecha.Text = Now().ToShortDateString
        GetIndex(ddl_ProcesoID, sKeyProceso)
        ddl_ProcesoID.Enabled = False

        Dim Drp As DataRow = GetInfoProceso(sKeyProceso)
        If Not Drp Is Nothing Then
            LoadComboOrigen(Drp("EmpresaOrigenID"), Drp("UbicacionOrigenID"))
        End If

    End Sub

    Private Sub LoadLista()
        Dim oSql As New SQLProduccion(oUsr)
        Dim oPrm As Parameter
        Try
            oPrm = New Parameter("@ProcesoID") With {.DefaultValue = ddl_ProcesoID.SelectedValue}
            SqlDataSource1.SelectParameters.Add(oPrm)
            SqlDataSource1.SelectCommand = oSql.List
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

    Private Sub LoadComboOrigen(ByVal EmpresaID As Integer, ByVal AlmacenID As String)
        Dim oSql As New SQLOrigen(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            oCs.Create("@EmpresaID", EmpresaID)
            oCs.Create("@UbicacionID", AlmacenID)
            Dim oTb As DataTable = oSql._List(oSql.List, "Origen", oCs)
            If Not oTb Is Nothing Then
                Dim oDrn As DataRow = oTb.NewRow
                oDrn.Item("LoteOrigen") = "0"
                oDrn.Item("LoteOrigenInfo") = "[SELECCIONAR ORIGEN]"
                oTb.Rows.Add(oDrn)
                oTb.DefaultView.Sort = "LoteOrigen"
                LoadCombo(oUsr, ddl_Origen, oTb.DefaultView)
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

    Private Sub LoadComboProcesos()
        Dim oSql As New SQLProcesos(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            Dim oTb As DataTable = oSql._List(oSql.List, "Procesos", oCs)
            If Not oTb Is Nothing Then
                oTb.DefaultView.Sort = "ProcesoNombre"
                LoadCombo(oUsr, ddl_ProcesoID, oTb.DefaultView)
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

    Private Sub LoadComboTurnos()
        Dim oSql As New SQLTurnos(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            Dim oTb As DataTable = oSql._List(oSql.List, "Turnos", oCs)
            If Not oTb Is Nothing Then
                Dim oDrn As DataRow = oTb.NewRow
                oDrn.Item("TurnoID") = "0"
                oDrn.Item("TurnoNombre") = "[SELECCIONAR TURNO]"
                oTb.Rows.Add(oDrn)
                oTb.DefaultView.Sort = "TurnoNombre"
                LoadCombo(oUsr, ddl_Turno, oTb.DefaultView)
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

    Private Sub LoadComboResponsables()
        Dim oSql As New SQLResponsables(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            Dim oTb As DataTable = oSql._List(oSql.List, "Responsables", oCs)
            If Not oTb Is Nothing Then
                Dim oDrn As DataRow = oTb.NewRow
                oDrn.Item("ResponsableID") = "0"
                oDrn.Item("ResponsableNombre") = "[SELECCIONAR TURNO]"
                oTb.Rows.Add(oDrn)
                oTb.DefaultView.Sort = "ResponsableNombre"
                LoadCombo(oUsr, ddl_responsable, oTb.DefaultView)
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

    Private Function GetInfoProceso(ByVal ProcesoID As Integer) As DataRow
        Dim oSql As New SQLProcesos(oUsr)
        Dim oCs As New ColeccionPrmSql
        GetInfoProceso = Nothing
        Try
            oCs.Create("@ProcesoID", ProcesoID)
            Dim oTb As DataTable = oSql._Item(oSql.Item, "Procesos", oCs)
            If Not oTb Is Nothing Then
                For Each Dr As DataRow In oTb.Rows
                    Return Dr
                Next
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Function

    Private Function SaveParametrosEntries(ByVal sAcc As String, arp As List(Of PrmEnt)) As Boolean
        Dim oSql As New SQLProduccion(oUsr)
        SaveParametrosEntries = False
        Try
            For Each item As PrmEnt In arp
                Dim oCs As New ColeccionPrmSql
                oCs.Create("@OrdenID", txt_OrdenID.Text)
                oCs.Create("@ParametroID", item.ParametroID)
                oCs.Create("@ParametroEntradaValor", item.ParametroValor)
                If sAcc = "Nuevo" Then
                    oSql.ExecuteQry(oSql.InsertParameterEntries, oCs)
                Else
                    oSql.ExecuteQry(oSql.UpdateParameterEntries, oCs)
                End If
            Next
            Return True
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Function

    Private Function Save() As Boolean
        Dim oSql As New SQLProduccion(oUsr)
        Dim oCs As New ColeccionPrmSql
        Dim bExito As Boolean = False
        Dim iNew As Integer = 0
        Save = False
        Try
            oCs.Create("@OrdenID", txt_OrdenID.Text)
            Dim oTb As DataTable = oSql._Item(oSql.Item, "ORDENP", oCs)
            If Not oTb Is Nothing Then
                oTb.Columns("OrdenID").Unique = True
                oTb.Columns("OrdenID").AutoIncrement = True
                If oTb.Rows.Count = 0 Then
                    Dim Dr As DataRow = oTb.NewRow
                    Dr("OrdenFecha") = wdp_OrdenFecha.Text
                    Dr("OrdenFechaRegistro") = Now()
                    Dr("ProcesoID") = ddl_ProcesoID.SelectedValue
                    oTb.Rows.Add(Dr)
                    bExito = oSql.StatemenInsert(oTb, iNew)
                    txt_OrdenID.Text = iNew.ToString
                Else
                    Dim Dr As DataRow = oTb.Rows.Item(0)
                    Dr("OrdenFecha") = wdp_OrdenFecha.Text
                    bExito = oSql.StatemenUpdate(oTb)
                End If
            End If

            If bExito Then
                Dim Ar As New List(Of PrmEnt)
                Dim pObserv As New PrmEnt : pObserv.ParametroID = 3 : pObserv.ParametroValor = wtxt_observ.Text
                Ar.Add(pObserv)
                Dim pTurno As New PrmEnt : pTurno.ParametroID = 9 : pTurno.ParametroValor = ddl_Turno.SelectedValue
                Ar.Add(pTurno)
                Dim phoraini As New PrmEnt : phoraini.ParametroID = 10 : phoraini.ParametroValor = wtxt_horainicio.Text
                Ar.Add(phoraini)
                Dim phorafin As New PrmEnt : phorafin.ParametroID = 11 : phorafin.ParametroValor = wtxt_horafin.Text
                Ar.Add(phorafin)
                Dim pjornales As New PrmEnt : pjornales.ParametroID = 12 : pjornales.ParametroValor = wtxt_jornales.Text
                Ar.Add(pjornales)
                Dim presponsable As New PrmEnt : presponsable.ParametroID = 13 : presponsable.ParametroValor = ddl_responsable.SelectedValue
                Ar.Add(presponsable)
                Dim pOrigen As New PrmEnt : pOrigen.ParametroID = 18 : pOrigen.ParametroValor = ddl_Origen.SelectedValue
                Ar.Add(pOrigen)
                bExito = bExito And SaveParametrosEntries(lblAcción.Text, Ar)
            End If

            Return bExito

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Function

    Private Sub BarEventos1_MsgEvent(sAcción As String) Handles BarEventos1.MsgEvent
        If WebDataGrid1.Behaviors.Selection.SelectedRows.Count > 0 Then
            txt_OrdenID.Text = WebDataGrid1.Behaviors.Selection.SelectedRows(0).DataKey(0).ToString
        End If
        Select Case sAcción
            Case "Consultar"
                WebDataGrid1.DataBind()
            Case "Nuevo"
                SetFormEdit(sAcción)
            Case "Editar"
                SetFormEdit(sAcción)
            Case "Especial1"
                Dim sLink As String = "RegGasto.aspx?key=@id".Replace("@id", txt_OrdenID.Text)
                Response.Redirect(sLink, False)
            Case "Especial2"
                Dim sLink As String = "RegProduccion.aspx?key=@id".Replace("@id", txt_OrdenID.Text)
                Response.Redirect(sLink, False)
            Case Else
        End Select
    End Sub

    Private Sub SetFormEdit(ByVal sAcc As String)
        Dim oSql As New SQLProduccion(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            pnlEventos.Visible = False
            pnlAdd.Visible = True
            pnlListar.Visible = False



            lblAcción.Text = sAcc
            Select Case sAcc
                Case "Nuevo"
                    'Configuraciones iniciales
                    txt_OrdenID.Text = ""
                    wdp_OrdenFecha.Text = Now().ToShortDateString
                    ddl_Origen.SelectedItemIndex = 0
                    ddl_Turno.SelectedItemIndex = 0
                    ddl_responsable.SelectedItemIndex = 0
                    wtxt_horainicio.Text = ""
                    wtxt_horafin.Text = ""
                    wtxt_jornales.Text = ""
                    wtxt_observ.Text = ""

                Case "Editar"
                    Wizard1.ActiveStepIndex = 0
                    oCs.Create("@OrdenID", txt_OrdenID.Text)
                    Dim oTb As DataTable = oSql._Item(oSql.ItemView, "R", oCs)
                    If Not oTb Is Nothing Then
                        For Each Dr As DataRow In oTb.Rows
                            wdp_OrdenFecha.Text = Dr("OrdenFecha")
                            Dim oTp As DataTable = oSql._List(oSql.ParameterEntries, "Parametros", oCs)
                            If Not oTp Is Nothing Then
                                For Each Drp As DataRow In oTp.Rows
                                    Select Case Drp("ParametroID")
                                        Case 3 'OBSERVACION
                                            wtxt_observ.Text = Drp("ParametroEntradaValor")
                                        Case 9 'TURNO
                                            GetIndex(ddl_Turno, Drp("ParametroEntradaValor"))
                                        Case 10 'HORAINI
                                            wtxt_horainicio.Text = Drp("ParametroEntradaValor")
                                        Case 11 'HORAFIN
                                            wtxt_horafin.Text = Drp("ParametroEntradaValor")
                                        Case 12 'JORNALES
                                            wtxt_jornales.Text = Drp("ParametroEntradaValor")
                                        Case 13 'RESPONSABLE
                                            GetIndex(ddl_responsable, Drp("ParametroEntradaValor"))
                                        Case 18 'ORIGEN
                                            GetIndex(ddl_Origen, Drp("ParametroEntradaValor"))

                                        Case Else
                                    End Select
                                Next
                            End If
                            Exit For
                        Next
                    End If

                Case Else
            End Select

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

    Protected Sub Wizard1_FinishButtonClick(sender As Object, e As WizardNavigationEventArgs) Handles Wizard1.FinishButtonClick
        If Save() Then
            SetFormConfig()
        End If
    End Sub

    Private Sub Wizard1_CancelButtonClick(sender As Object, e As EventArgs) Handles Wizard1.CancelButtonClick
        SetFormConfig()
    End Sub

    Private Sub Wizard1_NextButtonClick(sender As Object, e As WizardNavigationEventArgs) Handles Wizard1.NextButtonClick
        Select Case e.NextStepIndex
            Case 0
                Dim s As String = ""
            Case 1
                lblOrigen.Text = ddl_Origen.SelectedItem.Text
        End Select
    End Sub

End Class