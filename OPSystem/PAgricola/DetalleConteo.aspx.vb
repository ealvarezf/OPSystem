'Imports CrystalDecisions.CrystalReports.Engine
'Imports CrystalDecisions.Shared
Imports Security_System
Imports DataAgro
Imports ClosedXML.Excel
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Printing

Public Class DetalleConteo
    Inherits System.Web.UI.Page
    Private Ds As New DataSet
    Private oUsr As UserLogin
    Private oCs As New ColeccionPrmSql
    Dim oRp As InfoRp
    Dim iKey As Integer = 0

    Private Sub DetalleConteo_Init(sender As Object, e As EventArgs) Handles Me.Init
        oUsr = Session("Usr")
        oRp = Session("INFORPT")
        If oUsr Is Nothing Then
            Response.Redirect("Agro.aspx")
        Else
            oUsr.Mis.Función = "DetalleConteo"
        End If

        If oRp Is Nothing Then
            Response.Redirect("Agro.aspx")
        End If
        ObtenerConteos()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim ExisteCont As String = ExistDetCont()
            ViewState("Filtros") = ""
            CargarDatosConteo(iKey)
            SetFormConfig(ExisteCont)
            LoadLista(ExisteCont)
        End If
    End Sub

    Private Sub ObtenerConteos()
        Try
            Select Case oRp.Response
                Case "DetalleConteo"

                    If oRp.ikey > 0 Then

                        'Dim str_java As String
                        'str_java = "<script language='javascript'>"
                        'str_java += " window.close();"
                        'str_java += "</script>"
                        'RegisterClientScriptBlock("conceptos", str_java)

                        iKey = oRp.ikey
                    Else
                        Response.Redirect("Agro.aspx")
                    End If

            End Select

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

    '==============================================================================================================================
    'Inicialización 
    '==============================================================================================================================
    Private Sub SetFormConfig(Optional ByVal ExistDet As String = "")

        If ExistDet = "S" Then  'Si existe Detalle Del conteo
            pnlEventos.Visible = False
            pnlEv2.Visible = True
            pnlFiltros.Visible = False
            pnlAdd.Visible = False
            pnlListar.Visible = False
            pnlListar2.Visible = True
            pnlEncabezado.Visible = True
            pnlGridRegitrosFiltrados.Visible = False
            pnlGridRegistrosDetalle.Visible = True
            pnlConfirmarInv.Visible = False
        Else
            pnlEventos.Visible = True
            pnlEv2.Visible = False
            pnlFiltros.Visible = False
            pnlAdd.Visible = False
            pnlListar.Visible = True
            pnlListar2.Visible = False
            pnlEncabezado.Visible = True
            pnlConfirmarInv.Visible = False
        End If

        'With BarEventos1
        '    .Nuevo = False
        '    .Eliminar = False
        '    .Editar = False
        '    .Exportar = False
        '    .Filtrar = True
        '    .Listar = False
        '    .Especial1 = False
        '    .Especial3 = False
        'End With

        With BarEventos1
            .Filtrar.Boton.Visible = True
            .Filtrar.Boton.ToolTip = "Filtrar"
        End With

        With BarEventosOld
            .Nuevo = False
            .Eliminar = True
            .Editar = False
            .Exportar = True
            .Filtrar = False
            .Listar = False
            .Especial1 = True
            .Especial3 = False
            .Especial4 = True
            .Especial5 = True
        End With

        'With BarEventos2
        '    .Eliminar.Boton.Visible = True
        '    .Eliminar.Boton.ToolTip = "Eliminar"
        '    .Exportar.Boton.Visible = True
        '    .Exportar.Boton.ToolTip = "Exportar"
        '    .Especial1.Boton.Visible = True
        '    .Especial1.Boton.ToolTip = "Transferir Registros"
        '    .Especial1.Boton.ImageUrl = "~/Img/Recibo.jpg"
        '    .Especial4.Boton.Visible = True
        '    .Especial4.Boton.ImageUrl = "~/Img/cerrar.jpg"
        '    .Especial4.Boton.ToolTip = "Cerrar Conteos"
        '    .Especial5.Boton.Visible = True
        '    .Especial5.Boton.ImageUrl = "~/Img/transferir.jpg"
        '    .Especial5.Boton.ToolTip = "Importar registros"
        'End With

        'Valores predeterminados
        'Tamaño de pagina predeterminado
        GridView1.PageSize = 50

        CargarDatosConteo(iKey)

        'txt_Descripción.Text = ""
        'txt_Fecha.Text = ""
        'DDL_EMPRESA.SelectedIndex = -1
        'DDL_ALMACEN.SelectedIndex = -1

        'RFV_DESC.Enabled = False
        'RFV_FECHA.Enabled = False
        'RFV_EMPRESA.Enabled = False
        'RFV_ALMACEN.Enabled = False

        'Dim oSqlE As New SQLEmpresa(oUsr)
        'Dim lCsE As New ColeccionPrmSql
        'lCsE.Create("@status", oUsr.Mis.Status)
        'lCsE.Create("_Tabla", "EMPRESAS")
        'lCsE.Create("_Qry", oSqlE.ListBasica)
        'lCsE.Create("_Order", "EmpresaNombre")
        ''lCsE.Create("_Filtro", "ubi_keytub = 'I'")
        'lCsE.Create("_DefaultKey", 0)
        'lCsE.Create("_DefaultDes", "[SELECCIONAR]")
        'LoadCombo(oUsr, DDL_EMPRESA, lCsE)
        'DDL_EMPRESA.SelectedIndex = -1

        'Dim oSqlA As New SQLAlmacen(oUsr)
        'lCsE.Create("@EmpresaID", 0)
        'lCsE.ItemValue("_Tabla") = "ALMACENES"
        'lCsE.ItemValue("_Qry") = oSqlA.List_Combo
        'lCsE.ItemValue("_Order") = "UbicacionNombre"
        'lCsE.ItemValue("_Defaultkey") = "%"
        'lCsE.ItemValue("_DefaultDes") = "[SELECCIONAR]"
        'LoadCombo(oUsr, DDL_ALMACEN, lCsE)
        'DDL_ALMACEN.SelectedIndex = -1

    End Sub


    '================================================================================================================================
    'Acciones con el modelo de datos
    '================================================================================================================================
    'Acciones con el modelo de datos
    Private Sub LoadLista(ByRef Exist As String)
        Dim oSql As New SQLConteosDetalle(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            oCs.Create("@ConteoID", iKey)
            oCs.Create("@TipoPrd", txtSearch_TipoProd.Text & "%")
            oCs.Create("@Producto", txtSearch_Produc.Text & "%")
            If CheckSearch_Invent.Checked = True Then
                oCs.Create("_FiltroGrid", "A.ProductoLote IS NOT NULL")
            Else
                oCs.Create("_FiltroGrid", "A.UbicacionID LIKE '%'")
            End If
            'Dim oTabla As DataTable = oSql._Lista(oSql.ListAllProd, "CONTEOSDETALLE", oCs)
            If Exist = "S" Then
                Dim oTabla As DataTable = oSql._List(oSql.ListDetCon, "CONTEOSDETALLE", oCs)
                Dim oTb As DataTable = oSql._List(oSql.ListExport, "CONTEOSDETALLE", oCs)
                LoadGrid(GridView3, oTabla)
                coloresDatosaModificar(Exist)

                If GridView3.Rows.Count = 0 Then
                    Dim ExisteCont As String = ExistDetCont()
                    SetFormConfig(ExisteCont)
                    LoadLista(ExisteCont)
                End If

                If StatusConteo() = "I" Then
                    GridView3.Columns(14).Visible = True
                Else
                    GridView3.Columns(14).Visible = False
                End If

                'If Session("TablaGrid") Is Nothing Then
                '    Session.Add("TablaGrid", oTabla)
                'Else
                '    Session("TablaGrid") = oTabla
                'End If

                'Session que guarda El DataTable que Genera el Excel
                If Session("TablaExport") Is Nothing Then
                    Session.Add("TablaExport", oTb)
                Else
                    Session("TablaExport") = oTb
                End If

                'Session que guarda El DataTable que genera la Transferencia de datos A Inventario                
                If Session("TablaGridTrans") Is Nothing Then
                    Session.Add("TablaGridTrans", oTabla)
                Else
                    Session("TablaGridTrans") = oTabla
                End If

            Else
                Dim oTabla As DataTable = oSql._List(oSql.ListAllProd, "CONTEOSDETALLE", oCs)
                LoadGrid(GridView1, oTabla)
                'If Session("TablaGrid") Is Nothing Then
                '    Session.Add("TablaGrid", oTabla)
                'Else
                '    Session("TablaGrid") = oTabla
                'End If

                'Session que Guarda El Datatable que Genera El Grid A confirmar para insertar y descargar
                If Session("TablaVW_Conteo") Is Nothing Then
                    Session.Add("TablaVW_Conteo", oTabla)
                Else
                    Session("TablaVW_Conteo") = oTabla
                End If

            End If

            'Dim sQry As String = IIf(Exist = "S", oSql.ListDetCon, oSql.ListAllProd)
            'Dim oTabla As DataTable = oSql._Lista(sQry, "CONTEOSDETALLE", oCs)
            'LoadGrid(GridView1, oTabla)            

            If GridView1.Rows.Count > 0 Then
                pnlBotones.Visible = True
            Else
                pnlBotones.Visible = False
            End If

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

    '-------- CONDICIONES 

    Private Function ExistDetCont() As String
        Dim oSql As New SQLConteos(oUsr)
        Dim oCs As New ColeccionPrmSql
        ExistDetCont = ""
        Try
            oCs.Create("@ConteoID", iKey)
            oCs.Create("_VALOR", "Exist")
            Return oSql._Valor(oSql.ExistDetalle, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Private Function StatusConteo() As String
        Dim oSql As New SQLConteos(oUsr)
        Dim oCs As New ColeccionPrmSql
        StatusConteo = ""
        Try
            oCs.Create("@ConteoID", iKey)
            oCs.Create("_VALOR", "ConteoStatus")
            Return oSql._Valor(oSql.Item, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Private Sub CargarDatosConteo(ByVal ikeycont As Integer)
        Dim oSql As New SQLConteos(oUsr)
        Dim oCs As New ColeccionPrmSql
        Dim fecha As String = ""
        Try
            oCs.Create("@ConteoID", ikeycont)
            Dim oTb As DataTable = oSql._List(oSql.ListaConteo, "CONTEOS", oCs)
            If Not oTb Is Nothing Then
                If oTb.Rows.Count > 0 Then
                    Dim Dr As DataRow = oTb.Rows(0)
                    lblID.Text = Dr("ConteoID").ToString
                    fecha = Dr("ConteoFecha").ToString
                    lblFecha.Text = CDate(fecha)
                    lblEmpresa.Text = Dr("EmpresaNombre").ToString
                    lblAlmacen.Text = Dr("UbicacionNombre").ToString
                    lblDescripcion.Text = Dr("ConteoDescripcion").ToString
                End If
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub


    Protected Sub imgBtnAplicaFiltro_Click(sender As Object, e As EventArgs) Handles imgBtnAplicaFiltro.Click
        Dim ExisteCont As String = ExistDetCont()
        pnlFiltros.Visible = True
        LoadLista(ExisteCont)
    End Sub

    Protected Sub imgbtnCancelaFiltro_Click(sender As Object, e As EventArgs) Handles imgbtnCancelaFiltro.Click
        pnlFiltros.Visible = False
        txtSearch_TipoProd.Text = ""
        txtSearch_Produc.Text = ""
        CheckSearch_Invent.Checked = False

        If GridView1.Rows.Count > 0 Then
            pnlBotones.Visible = True
        End If
    End Sub


    Private Sub BarEventos1_MsgEvent(sAcción As String) Handles BarEventos1.MsgEvent
        Select Case sAcción
            Case "Nuevo"
                SetFormEdit(sAcción, GridView1)
                'pnlFlete.Visible = Not pnlFlete.Visible
            Case "Eliminar"
                'SetFormEdit(sAcción, GridView1)
            Case "Editar"
                SetFormEdit(sAcción, GridView1)
            Case "Filtrar"
                Dim ExisteCont As String = ExistDetCont()
                pnlFiltros.Visible = True
                If ExisteCont = "S" Then
                    pnl_Search_check.Visible = False
                Else
                    pnl_Search_check.Visible = True
                End If

                pnlBotones.Visible = False

            Case "Exportar"
                'ExportClosedXML("Inventarios")
            Case "Especial3"
                SetFormEdit(sAcción, GridView1)
            Case Else
        End Select
    End Sub


    Private Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        Dim ExisteCont As String = ExistDetCont()
        sender.PageIndex = e.NewPageIndex
        LoadLista(ExisteCont)
    End Sub


    Private Sub SetFormEdit(ByVal sAcc As String, ByVal oGrid As GridView)
        Dim oSql As New SQLConteos(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            Select Case sAcc
                Case "Nuevo"
                    'Se podria configurar botones de aceptar y cancelar
                    pnlEventos.Visible = False
                    pnlFiltros.Visible = False
                    pnlAdd.Visible = True
                    pnlListar.Visible = False
                    pnlListar2.Visible = False
                    pnlEncabezado.Visible = False
                    'RFV_DESC.Enabled = True
                    'RFV_FECHA.Enabled = True
                    'RFV_EMPRESA.Enabled = True
                    'RFV_ALMACEN.Enabled = True

                Case "Editar"
                    'pnlEventos.Visible = False
                    'pnlFiltros.Visible = False
                    'pnlAdd.Visible = True
                    'pnlListar.Visible = False

                    'RFV_DESC.Enabled = True
                    'RFV_FECHA.Enabled = True
                    'RFV_EMPRESA.Enabled = True
                    'RFV_ALMACEN.Enabled = True

                    'Dim iKey As Integer = oGrid.DataKeys(oGrid.SelectedRow.DataItemIndex).Value
                    'oCs.Create("@ConteoID", iKey)
                    'Dim oTb As DataTable = oSql._Item(oSql.Item, "CONTEOS", oCs)
                    'If Not oTb Is Nothing Then
                    '    For Each Dr As DataRow In oTb.Rows
                    '        lblAcción.Text = sAcc
                    '        lbl_NoConteo.Text = iKey.ToString
                    '        txt_Descripción.Text = Dr("ConteoDescripcion").ToString
                    '        txt_Fecha.Text = Dr("ConteoFecha").ToString
                    '        GetIndex(DDL_EMPRESA, Dr("EmpresaID").ToString)
                    '        'LoadCombos(DDL_EMPRESA.SelectedValue)
                    '        GetIndex(DDL_ALMACEN, Dr("UbicacionID").ToString)
                    '        Exit For
                    '    Next
                    'End If

                Case "Eliminar"

                Case Else

            End Select

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub


    Protected Sub imgbtnNext_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnNext.Click
        Dim oCs As New ColeccionPrmSql
        pnlListar2.Visible = True
        pnlGridRegitrosFiltrados.Visible = True
        pnlGridRegistrosDetalle.Visible = False
        pnlListar.Visible = False
        pnlEncabezado.Visible = True
        pnlEventos.Visible = False
        pnlEv2.Visible = True
        pnlFiltros.Visible = False
        LoadListaDetalleContAdd(oCs)
    End Sub



    Private Sub LoadListaDetalleContAdd(ByVal oCs As ColeccionPrmSql)
        Dim oSql As New SQLConteosDetalle(oUsr)
        Dim a As Integer = 0
        Try
            'If Not Session("Csc") Is Nothing Then
            '    oCs = Session("Csc")
            'Else
            '    oCs.Create("@status", oUsr.Mis.Status)
            '    oCs.Create("@keysal", txtkey2.Text)
            'End If
            'oCs.Create("_FiltroGrid", ViewState("Filtros"))
            Dim oTabla As DataTable = filldata()
            'LoadGrid(GridView2, oTabla)
            If Session("TablaVW_Conteo") Is Nothing Then
                LoadGrid(GridView2, oTabla)
                Session("TablaVW_Conteo") = oTabla
            Else
                'LoadGrid(GridView2, oTabla)
                Dim dt As DataTable = TryCast(Session("TablaVW_Conteo"), DataTable)
                LoadGrid(GridView2, dt)
                Session("TablaVW_Conteo") = dt
            End If

            If GridView2.Rows.Count > 0 Then
                a = a + 1
            Else
                a = a + 2
            End If

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub


    Private Sub LoadListaDetalleContAddExist(ByVal oCs As ColeccionPrmSql)
        Dim oSql As New SQLConteosDetalle(oUsr)
        Try
            'If Not Session("Csc") Is Nothing Then
            '    oCs = Session("Csc")
            'Else
            '    oCs.Create("@status", oUsr.Mis.Status)
            '    oCs.Create("@keysal", txtkey2.Text)
            'End If
            'oCs.Create("_FiltroGrid", ViewState("Filtros"))
            oCs.Create("@ConteoID", iKey)
            Dim oTabla As DataTable = oSql._List(oSql.ListDetCon, "CONTEODETALLE", oCs)
            If Not oTabla Is Nothing Then
                LoadGrid(GridView2, oTabla)
            End If

            GridView2.Columns(14).Visible = False

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub


    Public Function filldata() As DataTable
        Dim dts As New DataTable()
        dts.Columns.Add("ConteoID", GetType(Integer))
        dts.Columns.Add("EmpresaID", GetType(Integer))
        dts.Columns.Add("EmpresaNombre", GetType(String))
        dts.Columns.Add("UbicacionID", GetType(Integer))
        dts.Columns.Add("UbicacionNombre", GetType(String))
        dts.Columns.Add("TipoPrdNombre", GetType(String))
        dts.Columns.Add("ProductoID", GetType(Integer))
        dts.Columns.Add("ProductoNombre", GetType(String))
        dts.Columns.Add("EnvasePeso", GetType(Double))
        dts.Columns.Add("MarcaNombre", GetType(String))
        dts.Columns.Add("ProductoLote", GetType(String))
        dts.Columns.Add("Origen", GetType(String))
        dts.Columns.Add("ConteoLogico", GetType(Double))
        dts.Columns.Add("InventarioUbicacionPasillo", GetType(String))
        dts.Columns.Add("ConteoStatus", GetType(String))
        Return dts
    End Function

    Public Sub AgregarRegistroGridview(ByRef ConteoID As Integer,
            ByRef EmpresaID As Integer,
            ByRef NomEmp As Integer,
            ByRef keyubi As String,
            ByRef Nomubi As String,
            ByRef TipoProd As String,
            ByRef KeyProd As String,
            ByRef NomProd As String,
            ByRef EnvasePeso As String,
            ByRef Marca As String,
            ByRef Lote As Double,
            ByRef CantidadLog As Double,
            ByRef Pasillo As Double,
            ByRef Status As Double)
        Try
            If Session("dts") Is Nothing Then
                Dim dt As DataTable = filldata()
                'Dim dt As DataTable = TryCast(Session("dts"), DataTable)
                Dim Row1 As DataRow
                Row1 = dt.NewRow()
                Row1("ConteoID") = ConteoID
                Row1("EmpresaID") = EmpresaID
                Row1("EmpresaNombre") = NomEmp
                Row1("UbicacionID") = keyubi
                Row1("UbicacionNombre") = Nomubi
                Row1("TipoPrdNombre") = TipoProd
                Row1("ProductoID") = KeyProd
                Row1("ProductoNombre") = NomProd
                Row1("EnvasePeso") = EnvasePeso
                Row1("MarcaNombre") = Marca
                Row1("ProductoLote") = Lote
                Row1("ConteoLogico") = CantidadLog
                Row1("InventarioUbicacionPasillo") = Pasillo
                Row1("ConteoStatus") = Status
                dt.Rows.Add(Row1)
                GridView2.DataSource = dt
                GridView2.DataBind()
                Session("dts") = dt
            Else
                Dim dt As DataTable = TryCast(Session("dts"), DataTable)
                Dim Row1 As DataRow
                Row1 = dt.NewRow()
                Row1("ConteoID") = ConteoID
                Row1("EmpresaID") = EmpresaID
                Row1("EmpresaNombre") = NomEmp
                Row1("UbicacionID") = keyubi
                Row1("UbicacionNombre") = Nomubi
                Row1("TipoPrdNombre") = TipoProd
                Row1("ProductoID") = KeyProd
                Row1("ProductoNombre") = NomProd
                Row1("EnvasePeso") = EnvasePeso
                Row1("MarcaNombre") = Marca
                Row1("ProductoLote") = Lote
                Row1("ConteoLogico") = CantidadLog
                Row1("InventarioUbicacionPasillo") = Pasillo
                Row1("ConteoStatus") = Status
                dt.Rows.Add(Row1)
                GridView2.DataSource = dt
                GridView2.DataBind()
                Session("dts") = dt
            End If
            GridView2.Columns(14).Visible = True
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub


    Private Sub GridView2_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView2.RowDeleting
        Dim oSQL As New SQL(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            Dim iKey As Integer = sender.DataKeys(e.RowIndex).Value()
            Dim keyrow As Integer = e.RowIndex
            Dim ExisteCont As String = ExistDetCont()

            If ExisteCont = "S" Then

                Dim keycont As String = sender.DataKeys(e.RowIndex).Values(0).ToString
                Dim keyprod As String = sender.DataKeys(e.RowIndex).Values(1).ToString
                Dim nolote As String = sender.DataKeys(e.RowIndex).Values(2).ToString

                If DeleteRowFisico(keycont, keyprod, nolote) Then
                    ExisteCont = ExistDetCont()
                    LoadLista(ExisteCont)
                End If

            Else
                If Session("TablaVW_Conteo") Is Nothing Then

                Else
                    Dim dt As DataTable = TryCast(Session("TablaVW_Conteo"), DataTable)
                    dt.Rows.RemoveAt(keyrow)
                    GridView2.DataSource = dt
                    GridView2.DataBind()
                    Session("TablaVW_Conteo") = dt
                End If
            End If



        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

    Private Function DeleteRowFisico(ByVal keycont As Integer, ByRef keyprod As String, ByRef lote As String) As Boolean
        Dim oSql As New SQLConteosDetalle(oUsr)
        Dim oCs As New ColeccionPrmSql
        DeleteRowFisico = False

        'Dim sKeyUbi As String = oGrid.DataKeys(Row.RowIndex).Values(0).ToString
        'Dim iKey As Integer = oGrid.DataKeys(oGrid.SelectedRow.DataItemIndex).Values(0)
        'Dim sKeyUbi As String = sender.DataKeys(e.RowIndex).Values(0).ToString
        'Dim sKeyVar As String = sender.DataKeys(e.RowIndex).Values(1).ToString
        'Dim iyear As String = sender.DataKeys(e.RowIndex).Values(2).ToString

        Try
            oCs.Create("@ConteoID", keycont)
            oCs.Create("@ProductoID", keyprod)
            oCs.Create("@ProductoLote", lote)
            Return oSql.ExecuteQry(oSql.DeleteRow, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Function

    Private Sub GridView2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView2.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim bTn As LinkButton = e.Row.FindControl("lnkBtnDelete")
            If Not bTn Is Nothing Then
                bTn.Attributes.Add("onclick", "return confirm('¿Realmente desea eliminar este registro?')")
            End If
        End If
    End Sub


    Private Function ExportClosedXML(ByVal sHoja As String) As Boolean
        ExportClosedXML = False
        Try
            Using dt As DataTable = DirectCast(Session("TablaExport"), DataTable)
                dt.Columns(0).Caption = "ID Conteo"
                dt.Columns(1).Caption = "Empresa"
                dt.Columns(2).Caption = "Almacen"
                dt.Columns(3).Caption = "Tipo Producto"
                dt.Columns(4).Caption = "ID Producto"
                dt.Columns(5).Caption = "Producto"
                dt.Columns(6).Caption = "Peso"
                dt.Columns(7).Caption = "Marca"
                dt.Columns(8).Caption = "Lote"
                dt.Columns(9).Caption = "Origen"
                dt.Columns(10).Caption = "Cantidad"
                dt.Columns(11).Caption = "Ubicacion"
                dt.Columns(12).Caption = "Status"
                Using wb As New XLWorkbook()
                    wb.Worksheets.Add(dt, sHoja)
                    Response.Clear()
                    Response.Buffer = True
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;filename=InventarioProductos.xlsx")
                    Using MyMemoryStream As New MemoryStream()
                        wb.SaveAs(MyMemoryStream)
                        MyMemoryStream.WriteTo(Response.OutputStream)
                        Response.Flush()
                        Response.End()
                    End Using
                End Using
            End Using

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try

    End Function


    Private Sub BarEventosOld_MsgEvent(sAcción As String) Handles BarEventosOld.MsgEvent
        Select Case sAcción
            Case "Nuevo"
                'SetFormEdit(sAcción, GridView1)

            Case "Eliminar"
                SetFormEdit2(sAcción, GridView3)

            Case "Editar"
                'SetFormEdit(sAcción, GridView1)
            Case "Filtrar"

            Case "Exportar"
                Dim ExisteCont As String = ExistDetCont()
                If ExisteCont = "S" Then
                    ExportClosedXML("ModificaInventario")
                Else
                    Response.Write("<script>window.alert('Error, Debe Cerrar los registros para Generar El XLS');</script>")
                End If
                coloresDatosaModificar(ExisteCont)
            Case "Especial1"
                Dim ExisteCont As String = ExistDetCont()
                Dim Status As String = StatusConteo()
                If ExistDetCont() = "S" And StatusConteo() = "I" Then
                    pnlConfirmarInv.Visible = True
                Else
                    Select Case ExisteCont
                        Case "N"
                            Response.Write("<script>window.alert('Error, No Existen Registros para Transferir');</script>")
                    End Select

                    Select Case Status
                        Case "C"
                            Response.Write("<script>window.alert('Error, Ya fueron Transferidos los datos al inventario');</script>")
                        Case "A"
                            Response.Write("<script>window.alert('Error, Aun no se ha Importado El Archivo');</script>")
                    End Select

                End If

            Case "Especial3"

            Case "Especial4"
                Dim ExisteCont As String = ExistDetCont()
                If ExisteCont = "N" Then    'Si no Existen detalles Conteos Entra la condicion
                    If RecorrerRegistrosDet() Then  'Agrega Los registros al detalle conteo
                        pnlGridRegistrosDetalle.Visible = True
                        pnlGridRegitrosFiltrados.Visible = False
                        ExisteCont = ExistDetCont()
                        LoadLista(ExisteCont)
                    End If
                Else
                    Response.Write("<script>window.alert('Error, ya Fue cerrado el filtro, debe eliminar el filtro completo, para crearlo nuevamente');</script>")
                End If

            Case "Especial5"
                'Importa El Archivo txt para modificar los datos
                Dim ExisteCont As String = ExistDetCont()
                If ExisteCont = "S" Then    'Si Existen detalles Conteos Entra la condicion
                    If StatusConteo() = "A" Or StatusConteo() = "I" Then
                        pnlEventos.Visible = False
                        pnlEv2.Visible = False
                        pnlFiltros.Visible = False
                        pnlAdd.Visible = True
                        pnlLoading.Visible = False
                        pnlListar.Visible = False
                        pnlListar2.Visible = False
                        pnlEncabezado.Visible = False
                    Else
                        Response.Write("<script>window.alert('Error, No se Puede Importar El archivo, Ya fue cerrado el Inventarios');</script>")
                    End If
                Else
                    Response.Write("<script>window.alert('Error, No se Puede Importar El archivo, Debe Generar el Archivo xls');</script>")
                End If
            Case Else

        End Select
        'Dim ExisteCont As String = ExistDetCont()
        coloresDatosaModificar(ExistDetCont())
    End Sub


    Private Sub SetFormEdit2(ByVal sAcc As String, ByVal oGrid As GridView)
        Dim oSql As New SQLConteos(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            Select Case sAcc
                Case "Nuevo"
                    'Se podria configurar botones de aceptar y cancelar                  
                Case "Editar"

                Case "Eliminar"
                    Dim ExisteCont As String = ExistDetCont()
                    If oGrid.Rows.Count > 0 Then
                        If ExisteCont = "S" And StatusConteo() <> "C" Then
                            If DeleteFisico() Then
                                ExisteCont = ExistDetCont()
                                pnlListar2.Visible = False
                                pnlListar.Visible = True
                                pnlEventos.Visible = True
                                pnlEv2.Visible = False
                                pnlEncabezado.Visible = True
                                UpdateStatusConteos("A")
                                LoadLista(ExisteCont)
                                Response.Write("<script>window.alert('Regitros Eliminados con Exito.');</script>")
                            End If
                        Else
                            Response.Write("<script>window.alert('Error, No se puede eliminar, No existen Registros Creados, ó Ya se ha cerrado el Inventario');</script>")
                        End If
                    Else
                        Response.Write("<script>window.alert('Error, No Existen Registros por eliminar');</script>")
                    End If
                Case Else
            End Select

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub


    'Recorremos el Grid Filtrado para Insertar Detalle
    Private Function RecorrerRegistrosDet() As Boolean
        Dim numreg As Integer = GridView2.Rows.Count
        Dim bander As Integer = 0
        RecorrerRegistrosDet = False
        Try
            For Each row As GridViewRow In GridView2.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    Dim keycont As Integer = GridView2.DataKeys(row.RowIndex).Value
                    Dim keyprod As String = row.Cells(6).Text    ' Obtiene El ID del Producto
                    Dim LoteExist As String = row.Cells(10).Text    ' Obtiene El Lote Existente
                    'Dim Lote As String = "0" + "-" + "0" + "-" + "0" + "-" + DateTime.Now.ToString("dd/MM/yyyy").Replace("/", "") + "-" + ""
                    Dim Lote As String = GetLote(2, "0", 0, DateTime.Now.ToString("dd/MM/yyyy"), "")
                    Dim CantFisica As Integer = 0
                    If LoteExist = "" Then
                        If SaveDetalle(keycont, keyprod, Lote, CantFisica) Then
                            bander = bander + 1
                        End If
                    Else
                        If SaveDetalle(keycont, keyprod, LoteExist, CantFisica) Then
                            bander = bander + 1
                        End If
                    End If
                    'Dim Lote As String = GetLote(2, "T140", 7, "25/08/2017", "3ra")
                End If
            Next

            If bander = numreg Then
                Return True
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function


    Private Function GetLote(ByVal Proceso As Integer, ByVal tabla As String, ByRef Variedad As Integer, ByRef fecha As String, ByRef obs As String) As String
        Dim oSql As New SQLInventariosAgro(oUsr)
        Dim oCs As New ColeccionPrmSql
        GetLote = ""
        Try
            oCs.Create("@Proceso", Proceso)
            oCs.Create("@Tabla", tabla)
            oCs.Create("@Variedad", Variedad)
            oCs.Create("@Fecha", CDate(fecha))
            oCs.Create("@Observ", obs)
            Dim oTb As DataTable = oSql._Item(oSql.Building_Lote, "INVENTARIOS", oCs)
            If Not oTb Is Nothing Then
                For Each Dr As DataRow In oTb.Rows
                    Return Dr("Lote")
                Next
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try

    End Function

    Private Function SaveDetalle(ByRef keycon As Integer, ByRef keyprod As String, ByRef nolote As String, ByRef conFisico As Double) As Boolean
        Dim oSql As New SQLConteosDetalle(oUsr)
        Dim oCs As New ColeccionPrmSql
        'Dim sKeyfec As String = GridView2.DataKeys(GridView2.SelectedRow.DataItemIndex).Value
        SaveDetalle = False
        Try
            oCs.Create("@ConteoID", keycon)
            oCs.Create("@ProductoID", keyprod)
            oCs.Create("@ProductoLote", nolote)
            Dim oTb As DataTable = oSql._Item(oSql.ItemDet, "CONTEOSDETALLE", oCs)
            If Not oTb Is Nothing Then
                oTb.Columns("ConteoID").Unique = True
                oTb.Columns("ProductoID").Unique = True
                oTb.Columns("ProductoLote").Unique = True
                If oTb.Rows.Count = 0 Then
                    'Dim iKeypsi As Integer = oSql._Valor(oSql.NextID, "NEXTID", oCs)
                    Dim Dr As DataRow = oTb.NewRow
                    Dr("ConteoID") = keycon
                    Dr("ProductoID") = keyprod
                    Dr("ProductoLote") = nolote
                    Dr("ConteoFisico") = conFisico
                    oTb.Rows.Add(Dr)
                    Return oSql.StatemenInsert(oTb)
                Else
                    Dim Dr As DataRow = oTb.Rows(0)
                    Dr("ConteoFisico") = conFisico
                    Return oSql.StatemenUpdate(oTb)
                End If
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    'Private Function UpdateConteos(ByVal sAcc As String) As Boolean
    '    Dim oSql As New SQLConteos(oUsr)
    '    Dim oCs As New ColeccionPrmSql
    '    UpdateConteos = False
    '    Try
    '        oCs.Create("@ConteoID", iKey)
    '        If sAcc = "Nuevo" Then
    '            'oSql.ExecuteQry(oSql.Update, oCs)
    '        Else
    '            Return oSql.ExecuteQry(oSql.Update, oCs)
    '        End If
    '    Catch ex As Exception
    '        Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
    '    End Try
    'End Function

    Private Function UpdateStatusConteos(ByRef status As String) As Boolean
        Dim oSql As New SQLConteos(oUsr)
        Dim oCs As New ColeccionPrmSql
        UpdateStatusConteos = False
        Try
            oCs.Create("@ConteoID", iKey)
            Dim oTb As DataTable = oSql._Item(oSql.Item, "CONTEOS", oCs)
            If Not oTb Is Nothing Then
                oTb.Columns("ConteoID").Unique = True
                oTb.Columns("ConteoID").AutoIncrement = True
                If oTb.Rows.Count = 0 Then
                    Dim Dr As DataRow = oTb.NewRow
                Else
                    Dim Dr As DataRow = oTb.Rows(0)
                    Dr("ConteoStatus") = status
                    Return oSql.StatemenUpdate(oTb)
                End If
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Function

    Private Function DeleteFisico() As Boolean
        Dim oSql As New SQLConteosDetalle(oUsr)
        Dim oCs As New ColeccionPrmSql
        DeleteFisico = False
        Try
            oCs.Create("@ConteoID", iKey)
            Return oSql.ExecuteQry(oSql.Delete, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Function



    Private Sub coloresDatosaModificar(ByRef Exist As String)
        Dim Status As String = StatusConteo()
        For Each row As GridViewRow In GridView3.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim SiemTem As String = row.Cells(5).Text
                Dim keyfec As String = row.Cells(1).Text

                If Status = "C" Then
                    row.Cells(12).BackColor = Color.FromName("#FFA500")   'color naranja;     Ya fue Cerrado el inventario
                    row.Cells(12).ForeColor = Color.White
                    row.Cells(13).BackColor = Color.FromName("#FFA500")   'color naranja;     Ya fue Cerrado el inventario
                    row.Cells(13).ForeColor = Color.White
                ElseIf Status = "I" Then
                    row.Cells(12).BackColor = Color.FromName("#006400")   'color Verde;     Confirmado para Realizar El ajuste de inventario
                    row.Cells(12).ForeColor = Color.White
                    row.Cells(13).BackColor = Color.FromName("#006400")   'color Verde;     Confirmado para Realizar El ajuste de inventario
                    row.Cells(13).ForeColor = Color.White
                Else
                    row.Cells(12).BackColor = Color.FromName("#FFFFFF") 'color blanco  sin Confirmacion de Cerrar el Conteo
                    row.Cells(12).ForeColor = Color.Black
                    row.Cells(13).BackColor = Color.FromName("#FFFFFF") 'color blanco  sin Confirmacion de Cerrar el Conteo
                    row.Cells(13).ForeColor = Color.Black
                End If
            End If
        Next
    End Sub


    Private Sub TransferirRegistros()
        Try
            Dim ExisteCont As String = ExistDetCont()
            If ExisteCont = "S" And StatusConteo() = "I" Then
                'init_RsInventario()

                If Session("TablaGridTrans") Is Nothing Then
                Else
                    Dim dt As DataTable = TryCast(Session("TablaGridTrans"), DataTable)
                    'Session("TablaGridTrans") = dt
                    If SaveAjuste(dt) Then
                        UpdateStatusConteos("C")
                        Dim sLink As String = "DetalleConteo.aspx"
                        Response.Redirect(sLink, False)
                    Else
                        Response.Write("<script>window.alert('Error, No se pudo Transferir los datos, Revice sus datos');</script>")
                    End If
                End If
                'Dim dt As DataTable = TryCast(Session("TablaGridTrans"), DataTable)
            Else
                Response.Write("<script>window.alert('Error, No se han Importado el Archivo (.txt)');</script>")
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub



    Private Sub ObtenerDatosGrid()
        For Each oGrid As GridViewRow In GridView3.Rows
            If oGrid.RowType = DataControlRowType.DataRow Then
                Dim keycont As String = oGrid.Cells(0).Text
                Dim ConteoID As Integer = DataBinder.Eval(oGrid.DataItem, "ConteoID")
                Dim ProductoID As Integer = DataBinder.Eval(oGrid.DataItem, "ProductoID")
                Dim ProductoLote As Integer = DataBinder.Eval(oGrid.DataItem, "ProductoLote")
            End If
        Next
    End Sub


    Private Function SaveAjuste(ByVal oTb As DataTable) As Boolean
        Dim oSql As New SQLInventarios(oUsr)
        Dim oCs As New ColeccionPrmSql
        Dim bExito As Boolean = True
        Try
            oCs.Create("@EmpresaID", 0)
            oCs.Create("@UbicacionID", "")
            oCs.Create("@ProductoID", "")
            oCs.Create("@ProductoLote", "")
            oCs.Create("@InventarioInicial", "")
            oCs.Create("@InventarioFisico", 0)
            oCs.Create("@InventarioPeso", 0)
            oCs.Create("@InventarioUbicacionPasillo", "")

            For Each Dr As DataRow In oTb.Rows
                'oCs.ItemValue("") = Dr("")
                oCs.ItemValue("@EmpresaID") = Dr("EmpresaID")
                oCs.ItemValue("@UbicacionID") = Dr("UbicacionID")
                oCs.ItemValue("@ProductoID") = Dr("ProductoID")
                oCs.ItemValue("@ProductoLote") = Dr("ProductoLote")
                oCs.ItemValue("@InventarioInicial") = CDate(DateTime.Now.ToString())
                oCs.ItemValue("@InventarioFisico") = Dr("ConteoLogico")
                oCs.ItemValue("@InventarioPeso") = 0
                oCs.ItemValue("@InventarioUbicacionPasillo") = Dr("InventarioUbicacionPasillo")
                bExito = bExito And oSql.ExecuteStore("SP_INS_AJUSTEINV", oCs)
            Next
            Return bExito

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function



    '----------------------------------------------------------------------------------------------------------------
    '--- Comienza Codigo para Importar Documento Excel (txt)
    '----------------------------------------------------------------------------------------------------------------


    Private Function Importar() As Boolean
        Dim hfc As HttpFileCollection = Request.Files
        Dim sLine As String = ""
        Dim arrLinea() As String
        Dim bFirstLine As Boolean = True
        Dim iLinea As Integer = 0
        Try
            For i As Integer = 0 To hfc.Count - 1
                Dim hpf As HttpPostedFile = hfc(i)
                If hpf.ContentLength > 0 Then
                    init_RsToInsert()
                    Dim objReader = New StreamReader(hpf.InputStream, System.Text.Encoding.Default)
                    Do
                        sLine = objReader.ReadLine()
                        If Not sLine Is Nothing Then
                            iLinea += 1
                            arrLinea = sLine.Split(vbTab)
                            If bFirstLine Then
                                CustomValidator1.IsValid = ValidaHeader(arrLinea)
                                CustomValidator1.ErrorMessage = IIf(CustomValidator1.IsValid, "", "ERROR DE FORMATO" & vbCrLf & "Linea: " & iLinea.ToString)
                                bFirstLine = False
                            Else
                                Dim sMsj As String = ValidaLinea(arrLinea, Ds.Tables("CONTEOS"))
                                CustomValidator1.IsValid = IIf(sMsj = "", True, False)
                                CustomValidator1.ErrorMessage = IIf(CustomValidator1.IsValid, "", sMsj & vbCrLf & "Linea: " & iLinea.ToString)
                            End If
                            If Not CustomValidator1.IsValid Then Exit Do
                        End If
                    Loop Until sLine Is Nothing
                    objReader.Close()

                    If Not CustomValidator1.IsValid Then
                        Exit Function
                    End If
                    Return Insert_Refacciones(Ds.Tables("CONTEOS"))

                End If
            Next
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function



    Private Sub init_RsToInsert()
        Try
            Dim dtRpIns As New DataTable("CONTEOS")
            dtRpIns.MinimumCapacity = 100
            dtRpIns.CaseSensitive = False

            dtRpIns.Columns.Add("ConteoID", GetType(Integer))
            dtRpIns.Columns.Add("ProductoID", GetType(String)).MaxLength = 20
            dtRpIns.Columns.Add("ProductoLote", GetType(String)).MaxLength = 40
            dtRpIns.Columns.Add("ConteoFisico", GetType(Double))
            dtRpIns.Columns.Add("UbicacionPasillo", GetType(String)).MaxLength = 100
            Ds.Tables.Add(dtRpIns)

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)

        End Try
    End Sub


    Private Function ValidaHeader(ByVal Arh() As String) As Boolean
        Dim bValido As Boolean = (Arh.Length = 13)

        bValido = bValido And InStr(Arh(0), "ID Conteo") > 0
        bValido = bValido And InStr(Arh(1), "Empresa") > 0
        bValido = bValido And InStr(Arh(2), "Almacen") > 0
        bValido = bValido And InStr(Arh(3), "Tipo Producto") > 0
        bValido = bValido And InStr(Arh(4), "ID Producto") > 0
        bValido = bValido And InStr(Arh(5), "Producto") > 0
        bValido = bValido And InStr(Arh(6), "Peso") > 0
        bValido = bValido And InStr(Arh(7), "Marca") > 0
        bValido = bValido And InStr(Arh(8), "Lote") > 0
        bValido = bValido And InStr(Arh(9), "Origen") > 0
        bValido = bValido And InStr(Arh(10), "Cantidad") > 0
        bValido = bValido And InStr(Arh(11), "Ubicacion") > 0
        bValido = bValido And InStr(Arh(12), "Status") > 0

        Return bValido

    End Function



    Private Function ValidaLinea(ByVal Arh() As String, ByRef oTb As DataTable) As String
        Dim bValido As Boolean = (Arh.Length = 13)
        Dim Dr As DataRow = oTb.NewRow
        Try
            If Not bValido Then
                Return "ERROR EN FORMATO LINEA"
            End If

            If Arh(0) = "" Then
                Return "NO EXISTE CONTEO"
            End If

            If Arh(4) = "" Then
                Return "NO EXISTE PRODUCTO"
            End If

            If Arh(5) = "" Then
                Return "NO EXISTE PRODUCTO"
            End If

            If Arh(8) = "" Then
                Return "NO EXISTE LOTE"
            End If

            If Arh(9) = "" Then
                Return "NO EXISTE ORIGEN"
            End If

            If Arh(10) = "0" Then
                Return "CANTIDAD NO VALIDA"
            End If

            If Arh(12) = "Cerrado" Or Arh(12) = "" Then
                Return "STATUS NO VALIDO"
            End If

            'If Len(Arh(0)) > 100 Then Arh(0) = Arh(0).Substring(0, 100)
            'If Len(Arh(3)) > 40 Then Arh(3) = Arh(3).Substring(0, 40)
            'If Len(Arh(5)) > 10 Then Arh(5) = Arh(5).Substring(0, 10)
            'If Arh(6) = "" Then Arh(6) = "0"
            'If Arh(7) = "" Then Arh(7) = "0"
            'If Arh(8) = "" Then Arh(8) = "0"
            'If Arh(13) = "" Then Arh(13) = "0"

            If Arh(0) = 0 Then Arh(0) = 0
            'If Len(Arh(4)) > 20 Then Arh(4) = Arh(6).Substring(0, 20)
            If Len(Arh(8)) > 40 Then Arh(8) = Arh(8).Substring(0, 40)
            If Arh(10) = 0 Then Arh(10) = 1
            If Arh(11) = "" Then Arh(11) = ""

            Dr("ConteoID") = Arh(0)
            'Dr("REF_KEYPRV") = GetPrv(Arh(1))
            'Dr("REF_KEYMAR") = GetEmpresa(Arh(2))
            'Dr("REF_MODELO") = Arh(3)
            'Dr("REF_NPARTE") = Arh(4)
            'Dr("REF_UBICAC") = Arh(5)
            'Dr("ProductoID") = GetProductoID(Arh(4))
            Dr("ProductoID") = Arh(4)
            'Dr("REF_CANMAX") = Arh(7)
            'Dr("REF_CANTID") = Arh(8)
            'Dr("REF_KEYCAT") = GetCatego(Arh(9))
            Dr("ProductoLote") = Arh(8) 'Arh(10) No incluye subcategorias la importación
            Dr("ConteoFisico") = Arh(10) 'Arh(11)
            Dr("UbicacionPasillo") = Arh(11)
            'Dr("REF_PRECIO") = Arh(13)

            oTb.Rows.Add(Dr)
            Return ""

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
            Return ex.Message
        End Try

    End Function


    Private Function GetEmpresa(ByVal desEmp As String) As Integer
        Dim oSql As New SQLEmpresa(oUsr)
        Dim oCs As New ColeccionPrmSql
        GetEmpresa = 0
        Try
            oCs.Create("@desEmp", desEmp)
            Dim oTb As DataTable = oSql._Item(oSql.keyItem, "EMPRESAS", oCs)
            If Not oTb Is Nothing Then
                For Each Dr As DataRow In oTb.Rows
                    Return Dr("EmpresaID")
                Next
            End If

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try

    End Function


    Private Function GetProductoID(ByVal desProd As String) As String
        Dim oSql As New SQLProductosAgro(oUsr)
        Dim oCs As New ColeccionPrmSql
        GetProductoID = ""
        Try
            oCs.Create("@DesProd", desProd & "%")
            Dim oTb As DataTable = oSql._Item(oSql.ItemID, "PRODUCTOS", oCs)
            If Not oTb Is Nothing Then
                For Each Dr As DataRow In oTb.Rows
                    Return Dr("ProductoID")
                Next
            End If

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try

    End Function



    Private Function Insert_Refacciones(ByVal oTb As DataTable) As Boolean
        Dim oSql As New SQLInventarios(oUsr)
        Dim oCs As New ColeccionPrmSql
        Dim bExito As Boolean = True
        Dim Exito As String = ""
        Try
            oCs.Create("@ConteoID", 0)
            oCs.Create("@ProductoID", "")
            oCs.Create("@ProductoLote", "")
            oCs.Create("@ConteoFisico", 0)
            oCs.Create("@UbicacionPasillo", "")
            oCs.Create("@Exito", "", "out")

            For Each Dr As DataRow In oTb.Rows
                Exito = ""
                'oCs.ItemValue("") = Dr("")
                oCs.ItemValue("@ConteoID") = Dr("ConteoID")
                oCs.ItemValue("@ProductoID") = Dr("ProductoID")
                oCs.ItemValue("@ProductoLote") = Dr("ProductoLote")
                oCs.ItemValue("@ConteoFisico") = Dr("ConteoFisico")
                oCs.ItemValue("@UbicacionPasillo") = Dr("UbicacionPasillo")
                'oCs.ItemValue("@Exito") = 0,"out"
                'bExito = bExito And oSql.OraExecuteQry(oSql.Insert, oCs)

                If Exito = "" Then
                    oSql.ExecuteStore("SP_UPDATE_DETCONTEO", oCs)
                    Exito = oCs.ItemValue("@Exito")
                End If

                'bExito = bExito And oSql.ExecuteStore("SP_UPDATE_DETCONTEO", oCs)
                bExito = bExito And Exito = "S"
            Next
            Return bExito

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function


    Protected Sub btnUpload_Click(sender As Object, e As ImageClickEventArgs) Handles btnUpload.Click
        If Importar() Then
            UpdateStatusConteos("I")
            Dim sLink As String = "DetalleConteo.aspx"
            Response.Redirect(sLink, False)
        Else
            Response.Write("<script>window.alert('Error, Los registros Adjuntados no Existen');</script>")
        End If
    End Sub

    Protected Sub ImgBtnCancelar_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnCancelar.Click
        Dim ExisteCont As String = ExistDetCont()
        SetFormConfig(ExisteCont)
        coloresDatosaModificar(ExisteCont)
    End Sub


    '----------------------------------------------------------------------------------------------------------------
    '----- TRANSFERIR REGISTROS A INVENTARIOS
    '----------------------------------------------------------------------------------------------------------------

    Private Sub GridView3_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles GridView3.RowUpdating
        Dim oSQL As New SQL(oUsr)
        Dim cs As New ColeccionPrmSql
        Try
            'Dim keypsi As Integer = GridView1.DataKeys(GridView1.SelectedRow.DataItemIndex).Value
            'Dim iKey As Integer = sender.DataKeys(e.RowIndex).Values("pst_keypst")
            Dim keycont As String = sender.DataKeys(e.RowIndex).Values(0).ToString
            Dim keyProd As String = sender.DataKeys(e.RowIndex).Values(1).ToString
            Dim Lote As String = sender.DataKeys(e.RowIndex).Values(2).ToString
            Dim Cantidad As Double = DirectCast(sender.Rows(e.RowIndex).Cells(12).Controls(0), TextBox).Text
            Dim Pasillo As String = DirectCast(sender.Rows(e.RowIndex).Cells(13).Controls(0), TextBox).Text

            'cs.Create("@keypst", keycont)
            'cs.Create("@inocui", keyProd)
            'cs.Create("@observ", Lote)
            'cs.Create("@observ", Cantidad)
            'cs.Create("@observ", Pasillo)
            'If oSQL.ExecuteStore("SP_UPDATE_INOCUIPOST", cs) Then
            '    GridView3.EditIndex = -1
            '    Dim ExisteCont As String = ExistDetCont()
            '    LoadLista(ExisteCont)
            'End If
            If IsNumeric(Cantidad) And Val(Cantidad) > 0 Then
                If UpdateDetConteos(keycont, keyProd, Lote, Cantidad, Pasillo) Then
                    'UpdateStatusConteos("I")
                    GridView3.EditIndex = -1
                    Dim ExisteCont As String = ExistDetCont()
                    LoadLista(ExisteCont)
                End If
            End If

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

    Protected Sub GridView3_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles GridView3.RowCancelingEdit
        Dim oCs As New ColeccionPrmSql
        GridView3.EditIndex = -1
        Dim ExisteCont As String = ExistDetCont()
        LoadLista(ExisteCont)
    End Sub

    Private Sub GridView3_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridView3.RowEditing
        Dim oSQL As New SQL(oUsr)
        Dim oCs As New ColeccionPrmSql
        GridView3.EditIndex = e.NewEditIndex
        Dim ExisteCont As String = ExistDetCont()
        LoadLista(ExisteCont)
    End Sub

    Private Function UpdateDetConteos(
            ByRef keycont As Integer, ByRef keyProd As String, ByRef lote As String,
            ByRef Cantidad As Double, ByRef Pasillo As String
            ) As Boolean
        Dim oSql As New SQLConteosDetalle(oUsr)
        Dim oCs As New ColeccionPrmSql
        UpdateDetConteos = False
        Try
            oCs.Create("@ConteoID", keycont)
            oCs.Create("@ProductoID", keyProd)
            oCs.Create("@ProductoLote", lote)
            Dim oTb As DataTable = oSql._Item(oSql.ItemDet, "CONTEOSDETALLE", oCs)
            If Not oTb Is Nothing Then
                oTb.Columns("ConteoID").Unique = True
                oTb.Columns("ConteoID").AutoIncrement = True
                oTb.Columns("ProductoID").Unique = True
                oTb.Columns("ProductoLote").Unique = True
                If oTb.Rows.Count = 0 Then
                    Dim Dr As DataRow = oTb.NewRow
                Else
                    Dim Dr As DataRow = oTb.Rows(0)
                    Dr("ConteoFisico") = Cantidad
                    Dr("UbicacionPasillo") = Pasillo
                    Return oSql.StatemenUpdate(oTb)
                End If
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Function



    'Private Sub btnClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClose.Click
    '    ' Esto producirá un valor de UserClosing en FormClosing
    '    'Me.Close()
    'End Sub


    'Private Sub btnExit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExit.Click
    '    ' Esto producirá un valor de ApplicationExitCall en FormClosing
    '    ' Application.Exit()
    'End Sub

    'Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) _ Handles Me.FormClosing
    '    ' Mostrar un mensaje de porqué se está cerrando
    '    MessageBox.Show(e.CloseReason.ToString)
    '    ' Estos son los valores posibles
    '    Select Case e.CloseReason
    '        Case CloseReason.ApplicationExitCall
    '        Case CloseReason.FormOwnerClosing
    '        Case CloseReason.MdiFormClosing
    '        Case CloseReason.None
    '        Case CloseReason.TaskManagerClosing
    '        Case CloseReason.UserClosing
    '        Case CloseReason.WindowsShutDown
    '    End Select
    'End Sub



    Protected Sub btnCancelarExp_Click(sender As Object, e As ImageClickEventArgs) Handles btnCancelarExp.Click
        pnlConfirmarInv.Visible = False
    End Sub

    Protected Sub btnTransferir_Click(sender As Object, e As ImageClickEventArgs) Handles btnTransferir.Click
        Dim ExisteCont As String = ExistDetCont()
        Dim Status As String = StatusConteo()
        If ExistDetCont() = "S" And StatusConteo() = "I" Then
            Dim Condicion As String = ""
            TransferirRegistros()
            pnlConfirmarInv.Visible = False
        Else
            Select Case ExisteCont
                Case "N"
                    Response.Write("<script>window.alert('Error, No Existen Registros para Transferir');</script>")
            End Select

            Select Case Status
                Case "C"
                    Response.Write("<script>window.alert('Error, Ya fueron Transferidos los datos al inventario');</script>")
                    'Case "A"
                    'Response.Write("<script>window.alert('Error, Aun no Se ha Importado el Archivo');</script>")
            End Select

        End If
    End Sub
End Class