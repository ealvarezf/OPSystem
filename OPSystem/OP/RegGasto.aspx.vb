Imports Security_System
Public Class RegGasto
    Inherits System.Web.UI.Page
    Private Ds As New DataSet
    Private oUsr As UserLogin
    Private oCs As New ColeccionPrmSql
    Private keyord As Integer
    Private Sub RegGasto_Init(sender As Object, e As EventArgs) Handles Me.Init
        oUsr = Session("Usr")
        keyord = Request.Params("key")
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            SetFormConfig()
            LoadLista(lblInfo.Text)
        End If
    End Sub
    Private Sub SetFormConfig()
        pnlEventos.Visible = True
        pnlAdd.Visible = False
        pnlListar.Visible = True
        With BarEventos1
            .EtiquetaMaestro.Visible = True
            .EtiquetaDetalle.Visible = False
            .Back.Visible = False

            .Consultar.Boton.Visible = True
            .Nuevo.Boton.Visible = True
            .Editar.Boton.Visible = True
            .Eliminar.Boton.Visible = False
        End With
        'Valores predeterminados
        If Not IsNumeric(lblInfo.Text) Then
            lblInfo.Text = keyord
            Dim Dr As DataRow = GetInfoOrden(lblInfo.Text)
            If Not Dr Is Nothing Then
                Dim Drp As DataRow = GetInfoEntradaParametro(lblInfo.Text, 18)
                If Not Drp Is Nothing Then
                    LoadComboGastoProducto(Dr("EmpresaOrigenID"), Dr("UbicacionOrigenID"), Drp("ParametroEntradaValor"))
                End If
            End If
        End If

        wne_OrdenGastoExT.Text = ""
        wne_OrdenGastoPesoTara.Text = ""
        wddl_GastoProductoID.SelectedItemIndex = 0

    End Sub

    Private Sub LoadLista(ByVal OrdenID As Integer)
        Dim oSql As New SQLDetalleGasto(oUsr)
        Dim oPrm As Parameter
        Try
            oPrm = New Parameter("@OrdenID") With {.DefaultValue = OrdenID}
            SqlDataSource1.SelectParameters.Add(oPrm)
            SqlDataSource1.SelectCommand = oSql.List
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

    'Carga de Envases = Producto
    Private Sub LoadComboGastoProducto(ByVal EmpresaID As Integer, ByVal AlmacenID As String, ByVal LotesID As String)
        Dim oSql As New SQLInventarios(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            oCs.Create("@EmpresaID", EmpresaID)
            oCs.Create("@UbicacionID", AlmacenID)
            oCs.Create("@ProductoLote", LotesID & "%")
            Dim oTb As DataTable = oSql._List(oSql.ListEnvaseGasto, "GEnvase", oCs)
            If Not oTb Is Nothing Then
                Dim oDrn As DataRow = oTb.NewRow
                oDrn.Item("ConcatenaID") = "%"
                oDrn.Item("UnidadEmpaqueNombre") = "[SELECCIONAR ENVASE]"
                oTb.Rows.Add(oDrn)
                oTb.DefaultView.Sort = "UnidadEmpaqueNombre"
                LoadCombo(oUsr, wddl_GastoProductoID, oTb.DefaultView)
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

    Private Function GetInfoOrden(ByVal OrdenID As Integer) As DataRow
        Dim oSql As New SQLProduccion(oUsr)
        Dim oCs As New ColeccionPrmSql
        GetInfoOrden = Nothing
        Try
            oCs.Create("@OrdenID", OrdenID)
            Dim oTb As DataTable = oSql._Item(oSql.ItemView, "OrdenProducción", oCs)
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

    Private Function GetInfoEntradaParametro(ByVal OrdenID As Integer, ByVal ParametroID As Integer) As DataRow
        Dim oSql As New SQLEntradasParametros(oUsr)
        Dim oCs As New ColeccionPrmSql
        GetInfoEntradaParametro = Nothing
        Try
            oCs.Create("@OrdenID", OrdenID)
            oCs.Create("@ParametroID", ParametroID)
            Dim oTb As DataTable = oSql._Item(oSql.Item, "EntradaParam", oCs)
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

    Private Sub BarEventos1_MsgEvent(sAcción As String) Handles BarEventos1.MsgEvent
        Select Case sAcción
            Case "Consultar"
                WebDataGrid1.DataBind()
            Case "Nuevo"
                If WebDataGrid1.Rows.Count > 0 Then
                    If ReplicarRegistro(lblInfo.Text) Then
                        WebDataGrid1.DataBind()
                    End If
                Else
                    SetFormEdit(sAcción)
                End If

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
                    wddl_GastoProductoID.SelectedItemIndex = 0
                    wne_OrdenGastoExT.Text = 0
                    wne_OrdenGastoPesoTara.Text = 0
                Case Else
            End Select

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

    Protected Sub imgbtnCancelar_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnCancelar.Click
        SetFormConfig()
    End Sub

    Protected Sub imgbtnAceptar_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAceptar.Click
        If Save() Then
            SetFormConfig()
            WebDataGrid1.DataBind()
        End If
    End Sub

    Private Function Save() As Boolean
        Dim oSql As New SQLDetalleGasto(oUsr)
        Dim oCs As New ColeccionPrmSql
        Save = False
        Try
            oCs.Create("@OrdenID", lblInfo.Text)
            oCs.Create("@OrdenGastoID", 1)
            oCs.Create("@OrdenGastoExT", wne_OrdenGastoExT.Text)
            oCs.Create("@OrdenGastoPesoBruto", 0)
            oCs.Create("@OrdenGastoPesoTara", wne_OrdenGastoPesoTara.Text)
            oCs.Create("@GastoEmpresaID", wddl_GastoProductoID.SelectedValue.Split("|")(0))
            oCs.Create("@GastoUbicacionID", wddl_GastoProductoID.SelectedValue.Split("|")(1))
            oCs.Create("@GastoProductoID", wddl_GastoProductoID.SelectedValue.Split("|")(2))
            oCs.Create("@GastoProductoLote", wddl_GastoProductoID.SelectedValue.Split("|")(3))
            Return oSql.ExecuteQry(oSql.Insert, oCs)

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Function

    Private Function ReplicarRegistro(ByVal iOrden As Integer) As Boolean
        Dim oSql As New SQLDetalleGasto(oUsr)
        Dim oCs As New ColeccionPrmSql
        ReplicarRegistro = False
        Try
            oCs.Create("@OrdenID", iOrden)
            Return oSql.ExecuteStore("GastoReplicaRegistro", oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Function


End Class