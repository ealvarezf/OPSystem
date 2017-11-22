Imports Security_System
Imports DataAgro
Imports ClosedXML.Excel
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Printing
Public Class SalidaDevolucion
    Inherits System.Web.UI.Page
    Private Ds As New DataSet
    Private oUsr As UserLogin
    Private oCs As New ColeccionPrmSql
    Dim oRp As InfoRp
    Dim iKey As Integer = 0
    Dim keyEmpresa As Integer = 0
    Dim Almacen As String = ""

    Private Sub SalidaDevolucion_Init(sender As Object, e As EventArgs) Handles Me.Init
        oUsr = Session("Usr")
        oRp = Session("INFORPTDEV")
        iKey = (Request.Params("key"))
        If oUsr Is Nothing Then
            Response.Redirect("Agro.aspx")
        Else
            oUsr.Mis.Función = "SalidaDevolucion"
        End If

        If iKey = 0 Then
            ObtenerSalida()
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ViewState("Filtros") = ""
            SetFormConfig()
            CargarDatosSalida(iKey)
            'LoadLista()
            LoadLista2()
            LoadLista3()
        End If
    End Sub

    Private Sub ObtenerSalida()
        Try
            If Not oRp Is Nothing Then
                Select Case oRp.Response
                    Case "SalidaDevolucion"
                        If oRp.ikey > 0 Then
                            iKey = oRp.ikey
                        Else
                            Response.Redirect("Agro.aspx")
                        End If
                    Case Else
                        Response.Redirect("Agro.aspx")
                End Select
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub


    '==============================================================================================================================
    'Inicialización 
    '==============================================================================================================================
    Private Sub SetFormConfig(Optional ByVal ExistDet As String = "")

        'pnlFiltros.Visible = False
        pnlAdd.Visible = False
        'pnlListar.Visible = True
        pnlListar2.Visible = True
        pnlListar3.Visible = True
        pnlEncabezado.Visible = True
        pnlEventos.Visible = True

        Dim statusSal As String = StatusSalida(iKey)
        If statusSal <> "E" Then
            pnlConfirmarSalida.Visible = False
        Else
            pnlConfirmarSalida.Visible = True
        End If

        'With BarEventos1
        '    .Nuevo = False
        '    .Eliminar = False
        '    .Editar = False
        '    .Exportar = False
        '    .Filtrar = False
        '    .Listar = False
        '    .Especial1 = True
        '    .Especial3 = False
        '    .Especial4 = True
        'End With

        With BarEventos1
            .Especial1.Boton.Visible = True
            .Especial1.Boton.ImageUrl = "~/Img/Recibo.jpg"
            .Especial1.Boton.Visible = ""
            .Especial4.Boton.Visible = True
            .Especial4.Boton.Visible = "~/Img/cerrar.jpg"
            .Especial4.Boton.ToolTip = ""
        End With

        lblID.Text = ""
        lblFecha.Text = ""
        lblAlmOrigen.Text = ""
        lblAlmDestino.Text = ""
        lblElaboro.Text = ""
        lblEncargado.Text = ""

        lbl_SalidaSem.Text = ""
        txt_Fecha.Text = ""
        txt_AlmOrg.Text = ""
        txt_AlmDest.Text = ""
        txt_Origen.Text = ""
        txt_CantExist.Text = ""
        DDL_PRODUCTO.SelectedIndex = -1
        DDL_PRODUCTO.ToolTip = ""
        txt_Cantidad.Text = ""
        DDL_ALMACEN_DES.SelectedIndex = -1
        txt_Observacion.Text = ""
        txt_Cantidad.ToolTip = ""

        lblkeyProd.Text = ""
        lblDevolucionesID.Text = ""

        lbl_SalidaSem.Enabled = False
        txt_Fecha.Enabled = False
        txt_AlmOrg.Enabled = False
        txt_AlmDest.Enabled = False
        txt_Origen.Enabled = False
        DDL_PRODUCTO.Enabled = False
        DDL_ALMACEN_DES.Enabled = True

        RFVCANNUM.Enabled = False
        RFV_PRODUCTO.Enabled = False
        RFV_CANTIDAD.Enabled = False
        RFV_ALMACENDEV.Enabled = False
        RFVLIMITECANT.Enabled = False
        txt_CantExist.Enabled = False

        'Valores predeterminados
        'Tamaño de pagina predeterminado
        'GridView1.PageSize = 10
        GridView2.PageSize = 10
        GridView3.PageSize = 10

        CargarDatosSalida(iKey)

    End Sub


    '================================================================================================================================
    'Acciones con el modelo de datos
    'Private Sub LoadLista()
    '    Dim oSql As New SQLDetalleSalidaSemilla(oUsr)
    '    Dim oCs As New ColeccionPrmSql
    '    Try
    '        oCs.Create("@SalidaSemillaIDF", iKey)
    '        Dim oTabla As New DataTable()

    '        'oTabla.Columns.AddRange(New DataColumn(9) {New DataColumn("ID"), New DataColumn("Name"), New DataColumn("Company"), New DataColumn("Age")])

    '        'oTabla.Columns.AddRange(New DataColumn() {New DataColumn("SalidaSemillaIDF"),
    '        '                        New DataColumn("ProductoNombre"),
    '        '                        New DataColumn("Origen"),
    '        '                        New DataColumn("SalidaSemillaCantidad"),
    '        '                        New DataColumn("SalidaSemillaPeso"),
    '        '                        New DataColumn("SalidaSemillaDensidad"),
    '        '                        New DataColumn("SalidaSemillaObservaciones")})

    '        'Dim dt As New DataTable()

    '        'Dim columns As DataColumn() = {New DataColumn("SalidaSemillaIDF", System.Type.[GetType]("System.Int32")),
    '        '    New DataColumn("ProductoID", System.Type.[GetType]("System.String")),
    '        '    New DataColumn("ProductoNombre", System.Type.[GetType]("System.String")),
    '        '    New DataColumn("ProductoLote", System.Type.[GetType]("System.String")),
    '        '    New DataColumn("Origen", System.Type.[GetType]("System.String")),
    '        '    New DataColumn("SalidaSemillaCantidad", System.Type.[GetType]("System.Int32")),
    '        '    New DataColumn("SalidaSemillaPeso", System.Type.[GetType]("System.Int32")),
    '        '    New DataColumn("SalidaSemillaDensidad", System.Type.[GetType]("System.Int32")),
    '        '    New DataColumn("SalidaSemillaObservaciones", System.Type.[GetType]("System.String"))}
    '        'oTabla.Columns.AddRange(columns)
    '        oTabla = oSql._List(oSql.List, "SALIDASEMILLADETALLE", oCs)

    '        LoadGrid(GridView1, oTabla)
    '        'Attribute to show the Plus Minus Button.
    '        GridView1.HeaderRow.Cells(0).Attributes.Add("data-class", "expand")
    '        'Attribute to hide column in Phone.
    '        GridView1.HeaderRow.Cells(2).Attributes.Add("data-hide", "phone")
    '        GridView1.HeaderRow.Cells(3).Attributes.Add("data-hide", "phone")
    '        'GridView1.HeaderRow.Cells(4).Attributes.Add("data-hide", "phone")
    '        'GridView1.HeaderRow.Cells(5).Attributes.Add("data-hide", "phone")
    '        'GridView1.HeaderRow.Cells(6).Attributes.Add("data-hide", "phone")
    '        'GridView1.HeaderRow.Cells(7).Attributes.Add("data-hide", "phone")
    '        'Adds THEAD and TBODY to GridView.
    '        GridView1.HeaderRow.TableSection = TableRowSection.TableHeader

    '    Catch ex As Exception
    '        Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
    '    End Try
    'End Sub

    Private Sub BarEventos1_MsgEvent(sAcción As String) Handles BarEventos1.MsgEvent
        Select Case sAcción
            Case "Nuevo"
                SetFormEdit(sAcción, GridView2)

            Case "Eliminar"
                SetFormEdit(sAcción, GridView2)

            Case "Especial1"
                SetFormEdit(sAcción, GridView2)

            Case "Especial4"
                SetFormEdit(sAcción, GridView3)

            Case Else
        End Select
    End Sub


    Private Sub SetFormEdit(ByVal sAcc As String, ByVal oGrid As GridView)
        Dim oSql As New SQLDetalleSalidaSemilla(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            Select Case sAcc
                Case "Nuevo"
                    'Se podria configurar botones de aceptar y cancelar
                    pnlEventos.Visible = False
                    'pnlFiltros.Visible = False
                    pnlAdd.Visible = True
                    'pnlListar.Visible = False
                    lblAcción.Text = sAcc

                Case "Eliminar"
                    'If oGrid.SelectedRow IsNot Nothing Then
                    '    If Val(lblkeysal.Text) > 0 Then
                    '        'If DeleteFisico(iKey, ProductoID, Nolote) Then
                    '        '    LoadLista()
                    '        'End If
                    '    End If
                    'End If

                Case "Especial1"
                    If oGrid.SelectedRow IsNot Nothing Then
                        If lblkeyProd.Text <> "" Then
                            Dim idProd As Integer = lblkeyProd.Text.Split("|")(0)
                            Dim ProductoID As String = lblkeyProd.Text.Split("|")(1)
                            Dim ProductoLote As String = lblkeyProd.Text.Split("|")(2)
                            If ExistDevolucion(idProd, ProductoID, ProductoLote) = "N" Then
                                pnlEventos.Visible = False
                                'pnlFiltros.Visible = False
                                pnlEncabezado.Visible = False
                                pnlAdd.Visible = True
                                'pnlListar.Visible = False
                                pnlListar2.Visible = False
                                pnlListar3.Visible = False
                                pnlEAgregar.Visible = True
                                pnlEModificar.Visible = False

                                RFVCANNUM.Enabled = True
                                RFV_PRODUCTO.Enabled = True
                                RFV_CANTIDAD.Enabled = True
                                RFV_ALMACENDEV.Enabled = True

                                Dim fecha As String = ""
                                Dim EmpresaID As Integer = 0
                                Dim cant As Double = 0
                                Dim cantDisp As Double = 0
                                oCs.Create("@SalidaSemillaIDF", iKey)
                                oCs.Create("@ProductoID", ProductoID)
                                oCs.Create("@ProductoLote", ProductoLote)
                                Dim oTb As DataTable = oSql._Item(oSql.ItemProducto, "SALIDASEMILLADETALLE", oCs)
                                If Not oTb Is Nothing Then
                                    For Each Dr As DataRow In oTb.Rows
                                        lblAcción.Text = sAcc
                                        lbl_SalidaSem.Text = Dr("SalidaSemillaIDF").ToString
                                        fecha = Dr("SalidaSemillaFecha").ToString
                                        EmpresaID = Dr("EmpresaID").ToString
                                        txt_Fecha.Text = fecha.Split(" ")(0)
                                        txt_AlmOrg.Text = Dr("Alm_Org").ToString
                                        txt_AlmDest.Text = Dr("Alm_Des").ToString
                                        txt_Origen.Text = Dr("Origen").ToString
                                        txt_CantExist.Text = Dr("SalidaSemillaCantidad").ToString
                                        LoadCombos(EmpresaID)
                                        GetIndex(DDL_PRODUCTO, Dr("ProductoID").ToString)
                                        DDL_PRODUCTO.ToolTip = Dr("ProductoLote").ToString
                                        GetIndex(DDL_ALMACEN_DES, Dr("UbicacionID").ToString)
                                        cantDisp = Dr("SalidaSemillaCantidad")
                                        Exit For
                                    Next

                                    'RFVLIMITECANT.MinimumValue = 0
                                    'RFVLIMITECANT.MaximumValue = 0
                                    cant = CantidadDiponible(Val(lbl_SalidaSem.Text), DDL_PRODUCTO.SelectedValue, DDL_PRODUCTO.ToolTip)
                                    RFVLIMITECANT.Enabled = False
                                    RFVLIMITECANT.MinimumValue = 0
                                    RFVLIMITECANT.MaximumValue = cantDisp
                                    'RFVLIMITECONTEN.ErrorMessage = "Existen solo   " + lblConten.Text + " Disponibles"
                                    RFVLIMITECANT.ErrorMessage = "Existen solo " + cantDisp.ToString + " Disponibles"
                                    RFVLIMITECANT.Enabled = True

                                End If
                            Else
                                Response.Write("<script>window.alert('Error, No se Pueden agregar Devoluciones, debe editar una ya existente');</script>")
                            End If
                        End If
                    End If

                Case "Especial4"
                    Dim statusSal As String = StatusSalida(iKey)
                    If oGrid.Rows.Count > 0 Then
                        If statusSal = "R" Then
                            If UpdateStatusSal("D") Then
                                If SaveMovAlmacenSemDevolucion(iKey) Then
                                    LoadLista2()
                                    LoadLista3()
                                    'Refresca la pagina de Salida Semilla 
                                    Dim sUrl2 As String = "SalidaSemilla.aspx"
                                    Dim sScript As String = "<script language =javascript> "
                                    sScript += "window.open('" & sUrl2 & "','SalidaSemilla');"
                                    sScript += "</script> "
                                    Response.Write(sScript)
                                Else
                                    Response.Write("<script>window.alert('Error, No se Ejecuto EL movimiento de Devolución');</script>")
                                End If
                            End If
                        Else
                            Response.Write("<script>window.alert('Error, Ya Fue cerrada la salida para Devoluciones');</script>")
                        End If
                    Else
                        Response.Write("<script>window.alert('Error, No Existen Devoluciones Por cerrar');</script>")
                    End If

                Case Else

            End Select

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub



    Private Sub Editar(ByVal idProd As Integer, ByRef ProductoID As String, ByRef ProductoLote As String)
        Dim oSql As New SQLDetalleSalidaSemilla(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            pnlAdd.Visible = True
            pnlListar2.Visible = False
            pnlListar3.Visible = False
            pnlEventos.Visible = False
            pnlEncabezado.Visible = False
            pnlEAgregar.Visible = False
            pnlEModificar.Visible = True

            RFVCANNUM.Enabled = True
            RFV_PRODUCTO.Enabled = True
            RFV_CANTIDAD.Enabled = True
            RFV_ALMACENDEV.Enabled = True

            DDL_ALMACEN_DES.Enabled = False

            Dim fecha As String = ""
            Dim EmpresaID As Integer = 0
            Dim cant As Double = 0
            Dim cantDisp As Double = 0
            oCs.Create("@SalidaSemillaIDF", iKey)
            oCs.Create("@ProductoID", ProductoID)
            oCs.Create("@ProductoLote", ProductoLote)
            Dim oTb As DataTable = oSql._Item(oSql.ItemProducto, "SALIDASEMILLADETALLE", oCs)
            If Not oTb Is Nothing Then
                If oTb.Rows.Count > 0 Then
                    Dim Dr As DataRow = oTb.Rows(0)
                    lblAcción.Text = "Editar"
                    lbl_SalidaSem.Text = Dr("SalidaSemillaIDF").ToString
                    fecha = Dr("SalidaSemillaFecha").ToString
                    EmpresaID = Dr("EmpresaID").ToString
                    txt_Fecha.Text = fecha.Split(" ")(0)
                    txt_AlmOrg.Text = Dr("Alm_Org").ToString
                    txt_AlmDest.Text = Dr("Alm_Des").ToString
                    txt_Origen.Text = Dr("Origen").ToString
                    txt_CantExist.Text = Dr("SalidaSemillaCantidad").ToString
                    LoadCombos(EmpresaID)
                    GetIndex(DDL_PRODUCTO, Dr("ProductoID").ToString)
                    DDL_PRODUCTO.ToolTip = Dr("ProductoLote").ToString
                    GetIndex(DDL_ALMACEN_DES, Dr("DevSemillaAlmacen").ToString)
                    cantDisp = Dr("SalidaSemillaCantidad")
                    txt_Cantidad.Text = Dr("DevSemillaCantidad").ToString
                    txt_Cantidad.ToolTip = "Cantidad Devuelta" + " " + txt_Cantidad.Text
                    txt_Observacion.Text = Dr("DevSemillaObservaciones").ToString
                End If
            End If

            cant = CantidadDiponible(Val(lbl_SalidaSem.Text), DDL_PRODUCTO.SelectedValue, DDL_PRODUCTO.ToolTip)
            RFVLIMITECANT.Enabled = False
            RFVLIMITECANT.MinimumValue = 0
            RFVLIMITECANT.MaximumValue = cantDisp
            'RFVLIMITECONTEN.ErrorMessage = "Existen solo   " + lblConten.Text + " Disponibles"
            RFVLIMITECANT.ErrorMessage = "Existen solo " + cantDisp.ToString + " Disponibles"
            RFVLIMITECANT.Enabled = True

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub



    Private Function CantidadDiponible(ByVal keysal As Integer, ByVal keyProd As String, ByVal keyLote As String) As Double
        Dim oSql As New SQLDetalleSalidaSemilla(oUsr)
        Dim oCs As New ColeccionPrmSql
        CantidadDiponible = 0
        Try
            oCs.Create("@SalidaSemillaIDF", keysal)
            oCs.Create("@ProductoID", keyProd)
            oCs.Create("@ProductoLote", keyLote)
            oCs.Create("_VALOR", "SalidaSemillaCantidad")
            Return oSql._Valor(oSql.Item, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Private Function ExistDevolucion(ByVal keysal As Integer, ByVal keyProd As String, ByVal keyLote As String) As String
        Dim oSql As New SQLDetalleSalidaSemilla(oUsr)
        Dim oCs As New ColeccionPrmSql
        ExistDevolucion = ""
        Try
            oCs.Create("@SalidaSemillaIDF", keysal)
            oCs.Create("@ProductoID", keyProd)
            oCs.Create("@ProductoLote", keyLote)
            oCs.Create("_VALOR", "EXISTDEV")
            Return oSql._Valor(oSql.ExisteDevolucion, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Private Function StatusSalida(ByRef keysal As Integer) As String
        Dim oSql As New SQLSalidaSemilla(oUsr)
        Dim oCs As New ColeccionPrmSql
        StatusSalida = ""
        Try
            oCs.Create("@SalidaSemillaIDF", iKey)
            oCs.Create("_VALOR", "SalidaSemillaStatusProceso")
            Return oSql._Valor(oSql.ItemStatus, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Sub LoadCombos(ByRef EmpresaID As Integer)
        Try
            Dim oSqlE As New SQLAlmacenAgro(oUsr)
            Dim lCsE As New ColeccionPrmSql
            lCsE.Create("@EmpresaID", EmpresaID)
            lCsE.Create("_Tabla", "ALMACENES")
            lCsE.Create("_Qry", oSqlE.List_Combo)
            lCsE.Create("_Order", "UbicacionNombre")
            lCsE.Create("_DefaultKey", "%")
            lCsE.Create("_DefaultDes", "[SELECCIONAR]")
            LoadCombo(oUsr, DDL_ALMACEN_DES, lCsE)

            Dim oSqlP As New SQLProductosAgro(oUsr)
            lCsE.ItemValue("_Tabla") = "PRODUCTOS"
            lCsE.ItemValue("_Qry") = oSqlP.List_Combo
            lCsE.ItemValue("_Order") = "ProductoNombre"
            lCsE.ItemValue("_DefaultKey") = "%"
            LoadCombo(oUsr, DDL_PRODUCTO, lCsE)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub


    'Private Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView1.PageIndexChanging
    '    sender.PageIndex = e.NewPageIndex
    '    LoadLista()
    'End Sub

    'Private Sub GridView1_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles GridView1.SelectedIndexChanging
    '    Dim ikey As Integer = GridView1.DataKeys(e.NewSelectedIndex).Value()
    '    If ikey > 0 Then
    '        lblkeysal.Text = ikey
    '    End If
    'End Sub


    'Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
    '    If e.CommandName = "ConmutarStatus" Then

    '        Dim indice As Integer = Convert.ToInt32(e.CommandArgument)
    '        Dim id As Integer = GridView1.DataKeys(indice).Value
    '        'Ahora en id tenemos el id de nuestra tabla.
    '        'Podemos realizar cualquier acción con este valor.
    '    End If
    'End Sub


    Private Sub CargarDatosSalida(ByVal ikeysal As Integer)
        Dim oSql As New SQLSalidaSemilla(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            oCs.Create("@SalidaSemillaIDF", ikeysal)
            Dim oTb As DataTable = oSql._List(oSql.ListSalida, "SALIDASEMILLA", oCs)
            If Not oTb Is Nothing Then
                If oTb.Rows.Count > 0 Then
                    Dim Dr As DataRow = oTb.Rows(0)
                    lblID.Text = Dr("SalidaSemillaIDF").ToString
                    lblFecha.Text = Dr("SalidaSemillaFecha").ToString
                    lblAlmOrigen.Text = Dr("Alm_Org").ToString
                    lblAlmDestino.Text = Dr("Alm_Des").ToString
                    lblElaboro.Text = Dr("SalidaSemillaElaboro").ToString
                    lblEncargado.Text = Dr("SalidaSemillaEncargadoSiembra").ToString
                End If
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub


    Private Function DeleteFisico(ByRef iKey As Integer, ByRef ProductoID As String, ByRef Nolote As String) As Boolean
        Dim oSql As New SQLDetalleSalidaSemilla(oUsr)
        Dim oCs As New ColeccionPrmSql
        DeleteFisico = False
        Try
            oCs.Create("@SalidaSemillaIDF", iKey)
            oCs.Create("@ProductoID", ProductoID)
            oCs.Create("@ProductoLote", Nolote)
            Return oSql.ExecuteQry(oSql.Delete, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Function



    Protected Sub ImgBtnCancelar_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnCancelar.Click
        SetFormConfig()
    End Sub


    Private Function Save(ByVal sAcc As String) As Boolean
        Dim oSql As New SQLDevolucionSemilla(oUsr)
        Dim oCs As New ColeccionPrmSql
        Save = False
        Try
            oCs.Create("@SalidaSemillaIDF", lbl_SalidaSem.Text)
            oCs.Create("@ProductoID", "")
            oCs.Create("@ProductoLote", "")
            Dim oTb As DataTable = oSql._Item(oSql.Item, "SALIDASEMILLADEVOLUCION", oCs)
            If Not oTb Is Nothing Then
                If oTb.Rows.Count = 0 Then
                    oCs.Clear()
                    oCs.Create("@SalidaSemillaIDF", lbl_SalidaSem.Text)
                    oCs.Create("@ProductoID", DDL_PRODUCTO.SelectedValue)
                    oCs.Create("@ProductoLote", DDL_PRODUCTO.ToolTip)
                    oCs.Create("@ProductoNombre", DDL_PRODUCTO.SelectedItem.Text)
                    oCs.Create("@SemillaCantidad", txt_Cantidad.Text)
                    oCs.Create("@Observaciones", txt_Observacion.Text)
                    oCs.Create("@Almacen", DDL_ALMACEN_DES.SelectedValue)
                    If oSql.ExecuteStore("SP_INS_DEVSEMILLA", oCs) Then
                        Return True
                    End If
                End If
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Function


    '-----------------------------------------------------------------------------------------------------------
    '-----------------------------------------------------------------------------------------------------------
    ' GRIDVIEW 2
    Private Sub GridView2_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView2.PageIndexChanging
        sender.PageIndex = e.NewPageIndex
        LoadLista2()
    End Sub

    Private Sub GridView2_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles GridView2.SelectedIndexChanging
        Dim ikey As Integer = GridView2.DataKeys(e.NewSelectedIndex).Values(0)
        Dim sProd As String = GridView2.DataKeys(e.NewSelectedIndex).Values(1)
        Dim sLote As String = GridView2.DataKeys(e.NewSelectedIndex).Values(2)
        If ikey > 0 Then
            lblkeyProd.Text = ikey.ToString + "|" + sProd + "|" + sLote
        End If
    End Sub


    '================================================================================================================================
    'Acciones con el modelo de datos
    Private Sub LoadLista2()
        Dim oSql As New SQLDetalleSalidaSemilla(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            oCs.Create("@SalidaSemillaIDF", iKey)
            Dim oTabla As DataTable = oSql._List(oSql.List, "SALIDASEMILLADETALLE", oCs)
            LoadGrid(GridView2, oTabla)

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub


    Protected Sub GridView2_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView2.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim keysal As String = DataBinder.Eval(e.Row.DataItem, "SalidaSemillaIDF")
            Dim statusSal As String = StatusSalida(keysal)

            If statusSal = "E" Or statusSal = "A" Then
                GridView2.Columns(0).Visible = False
                pnlEventos.Visible = False
                pnlConfirmarSalida.Visible = True
            Else
                GridView2.Columns(0).Visible = True
                pnlEventos.Visible = True
                pnlConfirmarSalida.Visible = False
            End If

            If statusSal = "D" Then
                GridView2.Columns(0).Visible = False
                pnlEventos.Visible = False
            End If

            If statusSal = "R" Then
                pnlNota.Visible = True
            Else
                pnlNota.Visible = False
            End If

        End If
    End Sub



    '================================================================================================================================
    '================================================================================================================================
    '================================================================================================================================
    ' DEVOLUCIONES DE SEMILLA 


    '================================================================================================================================
    'Acciones con el modelo de datos
    Private Sub LoadLista3()
        Dim oSql As New SQLDevolucionSemilla(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            oCs.Create("@SalidaSemillaIDF", iKey)
            Dim oTabla As DataTable = oSql._List(oSql.List, "SALIDASEMILLADETALLE", oCs)
            LoadGrid(GridView3, oTabla)

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

    Private Sub GridView3_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView3.PageIndexChanging
        sender.PageIndex = e.NewPageIndex
        LoadLista3()
    End Sub

    Private Sub GridView3_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles GridView3.SelectedIndexChanging
        Dim ikey As Integer = GridView3.DataKeys(e.NewSelectedIndex).Values(0)
        Dim sProd As String = GridView3.DataKeys(e.NewSelectedIndex).Values(1)
        Dim sLote As String = GridView3.DataKeys(e.NewSelectedIndex).Values(2)
        If ikey > 0 Then
            lblDevolucionesID.Text = ikey.ToString + "|" + sProd + "|" + sLote
            Editar(ikey, sProd, sLote)
        End If
    End Sub

    Private Sub GridView3_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView3.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim keysal As String = DataBinder.Eval(e.Row.DataItem, "SalidaSemillaIDF")
            Dim statusSal As String = StatusSalida(keysal)
            Dim bTn As LinkButton = e.Row.FindControl("lnkBtnDelete")
            If Not bTn Is Nothing Then
                bTn.Attributes.Add("onclick", "return confirm('¿Realmente desea eliminar este registro?')")
            End If

            If statusSal = "D" Then
                GridView3.Columns(7).Visible = False
                GridView3.Columns(8).Visible = False
                GridView2.Columns(0).Visible = False
                pnlEventos.Visible = False
                'pnlNota.Visible = False
            Else
                GridView2.Columns(7).Visible = True
                GridView2.Columns(8).Visible = True
            End If


        End If
    End Sub


    Private Sub GridView3_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView3.RowDeleting
        Dim oSQL As New SQL(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            Dim iKey As Integer = sender.DataKeys(e.RowIndex).Values(0)
            Dim ProductoID As String = sender.DataKeys(e.RowIndex).Values(1)
            Dim ProductoLote As String = sender.DataKeys(e.RowIndex).Values(2)
            Dim keyrow As Integer = e.RowIndex
            If DeleteDevolucion(iKey, ProductoID, ProductoLote) Then
                LoadLista3()
                LoadLista2()
            End If

            If GridView3.Rows.Count > 0 Then
            Else
            End If

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub



    Private Function DeleteDevolucion(ByRef keysal As Integer, ByVal ProductoID As String, ByRef ProductoLote As String) As Boolean
        Dim oSql As New SQLDevolucionSemilla(oUsr)
        Dim oCs As New ColeccionPrmSql
        DeleteDevolucion = False
        Try
            oCs.Create("@SalidaSemillaIDF", lbl_SalidaSem.Text)
            oCs.Create("@ProductoID", "")
            oCs.Create("@ProductoLote", "")
            Dim oTb As DataTable = oSql._Item(oSql.Item, "SALIDASEMILLADEVOLUCION", oCs)
            If Not oTb Is Nothing Then
                If oTb.Rows.Count = 0 Then
                    oCs.Clear()
                    oCs.Create("@SalidaSemillaIDF", keysal)
                    oCs.Create("@ProductoID", ProductoID)
                    oCs.Create("@ProductoLote", ProductoLote)
                    If oSql.ExecuteStore("SP_DEL_DEVSEMILLA", oCs) Then
                        Return True
                    End If
                End If
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Function


    Private Function UpdateStatusSal(ByRef status As String) As Boolean
        Dim oSql As New SQLSalidaSemilla(oUsr)
        Dim oCs As New ColeccionPrmSql
        UpdateStatusSal = False
        Try
            oCs.Create("@SalidaSemillaIDF", iKey)
            Dim oTb As DataTable = oSql._Item(oSql.Item, "SALIDASEMILLA", oCs)
            If Not oTb Is Nothing Then
                oTb.Columns("SalidaSemillaIDF").Unique = True
                If oTb.Rows.Count = 0 Then
                    Dim Dr As DataRow = oTb.NewRow
                Else
                    Dim Dr As DataRow = oTb.Rows(0)
                    Dr("SalidaSemillaStatusProceso") = status
                    Return oSql.StatemenUpdate(oTb)
                End If
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Function




    'Protected Sub GridView3_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridView3.RowEditing
    '    Dim iKey As Integer = sender.DataKeys(e.NewEditIndex).Values(0)
    '    Dim ProductoID As String = sender.DataKeys(e.NewEditIndex).Values(1)
    '    Dim ProductoLote As String = sender.DataKeys(e.NewEditIndex).Values(2)
    '    Try
    '        Editar(iKey, ProductoID, ProductoLote)
    '    Catch ex As Exception
    '        Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
    '    End Try
    'End Sub



    'Realizar Movimiento de Almacen Para Recibir Salida  
    Private Function SaveMovAlmacenSem(ByRef keysal As Integer) As Boolean
        Dim oSql As New SQLInventariosAgro(oUsr)
        Dim oCs As New ColeccionPrmSql
        SaveMovAlmacenSem = False
        Try
            oCs.Create("@keycia", 0)
            oCs.Create("@keyubi", "")
            oCs.Create("@keyprod", "")
            oCs.Create("@nolote", "")
            Dim oTb As DataTable = oSql._Item(oSql.Item, "MOVIMIENTOS", oCs)
            If Not oTb Is Nothing Then
                'oTb.Columns("ins_keypsi").Unique = True
                If oTb.Rows.Count = 0 Then
                    oCs.Clear()
                    oCs.Create("@keysal", keysal)
                    oCs.Create("@ejecuteinserts", 0)    ' Solo Ejecutara Un Movimiento
                    oCs.Create("@MovimientoTipo", "E")  ' EL Primer Movimiento es de Salida
                    oCs.Create("@MovimientoTipoDes", "S")   'El Segundo Movimiento es de Entrada
                    oCs.Create("@OrigenMovID", 4)       ' Salida de semilla     
                    oCs.Create("@OrigenMovIDDes", 3)    ' Cualquier numero Alfinal no entra en la condicion
                    If oSql.ExecuteStore("SP_CUR_INSMOV_SAL", oCs) Then
                        Return True
                    End If
                End If
            End If
        Catch ex As Exception
            'Dim s As String = ex.TargetSite.Name
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function


    'Realizar Movimiento de Almacen De salida del Destino del flete y Entrada al Almacen de devolución 
    Private Function SaveMovAlmacenSemDevolucion(ByRef keysal As Integer) As Boolean
        Dim oSql As New SQLInventariosAgro(oUsr)
        Dim oCs As New ColeccionPrmSql
        SaveMovAlmacenSemDevolucion = False
        Try
            oCs.Create("@keycia", 0)
            oCs.Create("@keyubi", "")
            oCs.Create("@keyprod", "")
            oCs.Create("@nolote", "")
            Dim oTb As DataTable = oSql._Item(oSql.Item, "MOVIMIENTOS", oCs)
            If Not oTb Is Nothing Then
                'oTb.Columns("ins_keypsi").Unique = True
                If oTb.Rows.Count = 0 Then
                    oCs.Clear()
                    oCs.Create("@keysal", keysal)
                    oCs.Create("@ejecuteinserts", 1)    ' Solo Ejecutara Un Movimiento
                    oCs.Create("@MovimientoTipo", "S")  ' EL Primer Movimiento es de Salida
                    oCs.Create("@MovimientoTipoDes", "E")   'El Segundo Movimiento es de Entrada
                    oCs.Create("@OrigenMovID", 3)       ' Salida de semilla     
                    oCs.Create("@OrigenMovIDDes", 4)    ' Cualquier numero Alfinal no entra en la condicion
                    If oSql.ExecuteStore("SP_CUR_INSMOV_SALDEV", oCs) Then
                        Return True
                    End If
                End If
            End If
        Catch ex As Exception
            'Dim s As String = ex.TargetSite.Name
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Protected Sub imgConfirmarSal_Click(sender As Object, e As EventArgs) Handles imgConfirmarSal.Click
        If SaveMovAlmacenSem(iKey) Then   'Ejecuta el Movimiento de salida de Inventario
            If UpdateStatusSal("R") Then
                LoadLista2()
                LoadLista3()
                Dim statusSal As String = StatusSalida(iKey)
                If statusSal = "E" Then
                    pnlConfirmarSalida.Visible = True
                Else
                    pnlConfirmarSalida.Visible = False
                    'Refresca la pagina de Salida Semilla 
                    Dim sUrl2 As String = "SalidaSemilla.aspx"
                    Dim sScript As String = "<script language =javascript> "
                    sScript += "window.open('" & sUrl2 & "','SalidaSemilla');"
                    sScript += "</script> "
                    Response.Write(sScript)
                End If
            End If
        End If
    End Sub

    Protected Sub ImgBtnAceptar_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnAceptar.Click
        If Val(txt_Cantidad.Text) > 0 And Val(txt_Cantidad.Text) <= Val(txt_CantExist.Text) And DDL_ALMACEN_DES.SelectedValue <> "%" Then
            If Save(lblAcción.Text) Then
                LoadLista2()
                LoadLista3()
                SetFormConfig()
            End If
        Else
            Response.Write("<script>window.alert('Debe Teclear Solo Datos Indicados.');</script>")
        End If
    End Sub
End Class