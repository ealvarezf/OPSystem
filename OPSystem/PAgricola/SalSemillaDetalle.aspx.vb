Imports Security_System
Imports DataAgro
Imports ClosedXML.Excel
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Printing
Public Class SalSemillaDetalle
    Inherits System.Web.UI.Page

    Private Ds As New DataSet
    Private oUsr As UserLogin
    Private oCs As New ColeccionPrmSql
    Dim oRp As InfoRp
    Dim iKey As Integer = 0
    Dim keyEmpresa As Integer = 0
    Dim Almacen As String = ""

    Private Sub SalSemillaDetalle_Init(sender As Object, e As EventArgs) Handles Me.Init
        oUsr = Session("Usr")
        oRp = Session("INFORPT")

        If oUsr Is Nothing Then
            Response.Redirect("Agro.aspx")
        Else
            oUsr.Mis.Función = "DetalleSalidaSem"
        End If

        If oRp Is Nothing Then
            Response.Redirect("Agro.aspx")
        End If
        'Obtiene el ID de la salida como parametro
        ObtenerSalida()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ViewState("Filtros") = ""
            SetFormConfig()
            LoadLista()
            CargarDatosSalida(iKey)
            txtSearch_Producto.Text = ""
            LoadListaProductos()
        End If

        'Dim NewButtonControl As New HtmlButton()
        'NewButtonControl.ID = "NewButtonControl"
        'NewButtonControl.InnerHtml = "Click Me"
        'AddHandler NewButtonControl.ServerClick, AddressOf Button_Click
        'ControlContainer.Controls.Add(NewButtonControl)

    End Sub

    Private Sub ObtenerSalida()
        Try
            Select Case oRp.Response
                Case "DetalleSalidaSem"
                    If oRp.ikey > 0 Then
                        iKey = oRp.ikey
                        ObtenerAlmacen(iKey)
                    Else
                        Response.Redirect("Agro.aspx")
                    End If
                Case Else
                    Response.Redirect("Agro.aspx")
            End Select

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

    '==============================================================================================================================
    'Inicialización 
    '==============================================================================================================================
    Private Sub SetFormConfig(Optional ByVal ExistDet As String = "")

        pnlFiltros.Visible = False
        pnlAdd.Visible = False
        pnlListar.Visible = True

        If ExisteFlete(iKey) = "S" Then
            pnlEventos.Visible = False
        Else
            pnlEventos.Visible = True
        End If

        'With BarEventos1
        '    .Nuevo = True
        '    .Eliminar = True
        '    .Editar = True
        '    .Exportar = False
        '    .Filtrar = False
        '    .Listar = False
        '    .Especial1 = False
        '    .Especial3 = False
        'End With

        With BarEventos1
            .Nuevo.Boton.Visible = True
            .Nuevo.Boton.ToolTip = "Nuevo"
            .Editar.Boton.Visible = True
            .Editar.Boton.ToolTip = "Editar"
            .Eliminar.Boton.Visible = True
            .Eliminar.Boton.ToolTip = "Eliminar"
        End With

        txt_Producto.Text = ""
        txt_Producto.ToolTip = ""
        txt_Origen.Text = ""
        txt_Cantidad.Text = ""
        txt_Peso.Text = ""
        txt_Densidad.Text = ""
        txt_Observaciones.Text = ""
        txtSearch_Producto.Text = ""

        lblID.Text = ""
        lblFecha.Text = ""
        lblAlmOrigen.Text = ""
        lblAlmDestino.Text = ""
        lblElaboro.Text = ""
        lblEncargado.Text = ""

        RFV_PRODUCTO.Enabled = False
        RFV_ORIGEN.Enabled = False
        RFV_CANT.Enabled = False
        RFV_PESO.Enabled = False
        RFV_DENSIDAD.Enabled = False

        RFVCANNUM.Enabled = False
        RFVPESONUM.Enabled = False
        RFVDENSIDNUM.Enabled = False

        'Valores predeterminados
        'Tamaño de pagina predeterminado
        GridView1.PageSize = 10

        CargarDatosSalida(iKey)
        LoadListaProductos()
    End Sub


    '================================================================================================================================
    'Acciones con el modelo de datos
    Private Sub LoadLista()
        Dim oSql As New SQLDetalleSalidaSemilla(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            oCs.Create("@SalidaSemillaIDF", iKey)
            Dim oTabla As DataTable = oSql._List(oSql.List, "SALIDASEMILLADETALLE", oCs)
            LoadGrid(GridView1, oTabla)

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

    Protected Sub imgBtnAplicaFiltro_Click(sender As Object, e As EventArgs) Handles imgBtnAplicaFiltro.Click
        pnlFiltros.Visible = False
        LoadLista()
    End Sub

    Protected Sub imgbtnCancelaFiltro_Click(sender As Object, e As EventArgs) Handles imgbtnCancelaFiltro.Click
        pnlFiltros.Visible = False
        LoadLista()
    End Sub


    Private Sub BarEventos1_MsgEvent(sAcción As String) Handles BarEventos1.MsgEvent
        Select Case sAcción
            Case "Nuevo"
                SetFormEdit(sAcción, GridView1)

            Case "Eliminar"
                '<a id = "popup" data-toggle="modal" data-target="#myModal2"/>
                SetFormEdit(sAcción, GridView1)

            Case "Editar"
                SetFormEdit(sAcción, GridView1)
            Case "Filtrar"
                pnlFiltros.Visible = True

            Case "Exportar"
                'ExportClosedXML("Inventarios")
            Case "Especial3"
                SetFormEdit(sAcción, GridView1)
            Case Else
        End Select
    End Sub


    Private Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        sender.PageIndex = e.NewPageIndex
        LoadLista()
    End Sub

    Private Sub GridView1_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles GridView1.SelectedIndexChanging
        Dim ikey As Integer = GridView1.DataKeys(e.NewSelectedIndex).Values(0)
        Dim sProd As String = GridView1.DataKeys(e.NewSelectedIndex).Values(1)
        Dim sLote As String = GridView1.DataKeys(e.NewSelectedIndex).Values(2)
        If ikey > 0 Then
            lbkeyProd.Text = ikey.ToString + "|" + sProd + "|" + sLote
        End If
    End Sub

    Private Sub SetFormEdit(ByVal sAcc As String, ByVal oGrid As GridView)
        Dim oSql As New SQLDetalleSalidaSemilla(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            Select Case sAcc
                Case "Nuevo"
                    'Se podria configurar botones de aceptar y cancelar
                    pnlEventos.Visible = False
                    pnlFiltros.Visible = False
                    pnlAdd.Visible = True
                    pnlListar.Visible = False
                    pnlAddProd.Visible = True
                    pnlLimpiar.Visible = True

                    RFV_PRODUCTO.Enabled = True
                    RFV_CANT.Enabled = True
                    RFV_PESO.Enabled = True
                    RFV_DENSIDAD.Enabled = True

                    RFVCANNUM.Enabled = True
                    RFVPESONUM.Enabled = True
                    RFVDENSIDNUM.Enabled = True

                    lblAcción.Text = sAcc

                Case "Editar"
                    If oGrid.SelectedRow IsNot Nothing Then
                        pnlEventos.Visible = False
                        pnlFiltros.Visible = False
                        pnlAdd.Visible = True
                        pnlListar.Visible = False

                        RFV_PRODUCTO.Enabled = True
                        RFV_CANT.Enabled = True
                        RFV_PESO.Enabled = True
                        RFV_DENSIDAD.Enabled = True

                        RFVCANNUM.Enabled = True
                        RFVPESONUM.Enabled = True
                        RFVDENSIDNUM.Enabled = True

                        Dim iKey As Integer = oGrid.DataKeys(oGrid.SelectedRow.DataItemIndex).Values(0)
                        Dim ProductoID As String = oGrid.DataKeys(oGrid.SelectedRow.DataItemIndex).Values(1)
                        Dim ProductoLote As String = oGrid.DataKeys(oGrid.SelectedRow.DataItemIndex).Values(2)
                        Dim idProd As Integer = lbkeyProd.Text.Split("|")(0)
                        oCs.Create("@SalidaSemillaIDF", iKey)
                        oCs.Create("@ProductoID", ProductoID)
                        oCs.Create("@ProductoLote", ProductoLote)
                        Dim oTb As DataTable = oSql._Item(oSql.Item, "SALIDASEMILLADETALLE", oCs)
                        If Not oTb Is Nothing Then
                            For Each Dr As DataRow In oTb.Rows
                                lblAcción.Text = sAcc
                                lbl_NoSalida.Text = Dr("SalidaSemillaIDF").ToString
                                txt_Producto.Text = ProductoNombre(Dr("ProductoID"), Dr("ProductoLote"))
                                txt_Producto.ToolTip = Dr("ProductoID").ToString + "|" + Dr("ProductoLote").ToString
                                txt_Cantidad.Text = Dr("SalidaSemillaCantidad").ToString
                                txt_Peso.Text = Dr("SalidaSemillaPeso").ToString
                                txt_Densidad.Text = Dr("SalidaSemillaDensidad").ToString
                                txt_Observaciones.Text = Dr("SalidaSemillaObservaciones").ToString
                                txt_Origen.Text = ConstruirLote(Dr("ProductoLote").ToString)
                                Exit For
                            Next
                        End If

                        If lblAcción.Text = "Editar" Then
                            pnlAddProd.Visible = False
                            pnlLimpiar.Visible = False
                        End If
                    End If

                Case "Eliminar"
                    If oGrid.SelectedRow IsNot Nothing Then
                        Dim iKey As Integer = oGrid.DataKeys(oGrid.SelectedRow.DataItemIndex).Values(0)
                        Dim ProductoID As String = oGrid.DataKeys(oGrid.SelectedRow.DataItemIndex).Values(1)
                        Dim Nolote As String = oGrid.DataKeys(oGrid.SelectedRow.DataItemIndex).Values(2)
                        Dim idProd As Integer = lbkeyProd.Text.Split("|")(0)
                        If DeleteFisico(iKey, ProductoID, Nolote) Then
                            LoadLista()
                            LoadListaProductos()
                        End If
                    End If

                Case Else

            End Select

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub


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


    Private Function ProductoNombre(ByRef ProductoID As String, ByRef ProductoLote As String) As String
        Dim oSql As New SQLInventariosAgro(oUsr)
        Dim oCs As New ColeccionPrmSql
        ProductoNombre = ""
        Try
            oCs.Create("@keycia", keyEmpresa)
            oCs.Create("@keyubi", Almacen)
            oCs.Create("@keyprod", ProductoID)
            oCs.Create("@nolote", ProductoLote)
            oCs.Create("_VALOR", "ProductoNombre")
            Return oSql._Valor(oSql.ItemInv, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

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



    Private Sub LoadListaProductos()
        Dim oSql As New SQLInventariosAgro(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            oCs.Create("@EmpresaID", keyEmpresa)
            oCs.Create("@UbicacionID", Almacen)
            oCs.Create("@Producto", txtSearch_Producto.Text & "%")
            oCs.Create("@SalidaSemillaIDF", iKey)
            Dim oTabla As DataTable = oSql._List(oSql.List, "VW_CONSTRUIR_CONTEOS", oCs)
            LoadGrid(GridView4, oTabla)

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

    Private Sub GridView4_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView4.PageIndexChanging
        sender.PageIndex = e.NewPageIndex
        LoadListaProductos()
    End Sub

    Private Sub GridView4_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles GridView4.SelectedIndexChanging
        Dim skey As String = GridView4.DataKeys(e.NewSelectedIndex).Value()
        If skey <> "" Then
            lblProductoID.Text = skey
        End If
    End Sub

    Protected Sub txtSearch_Producto_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtSearch_Producto.TextChanged
        Dim oCs As New ColeccionPrmSql
        LoadListaProductos()
    End Sub

    'Protected Sub ImgBtnBuscar_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnBuscar.Click
    '    Dim oSql As New SQLSalidaSemilla(oUsr)
    '    Dim oCs As New ColeccionPrmSql
    '    LoadListaProductos()
    'End Sub


    Private Sub ObtenerAlmacen(ByVal NoSal As Integer)
        Dim oSql As New SQLSalidaSemilla(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            oCs.Create("@SalidaSemillaIDF", NoSal)
            Dim oTb As DataTable = oSql._Item(oSql.Item, "SalidaSemilla", oCs)
            If Not oTb Is Nothing Then
                If oTb.Rows.Count > 0 Then
                    Dim Dr As DataRow = oTb.Rows(0)
                    keyEmpresa = Dr("EmpresaID").ToString
                    Almacen = Dr("UbicacionID").ToString
                End If
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub

    Private Sub CargarProducto(ByVal oGrid As GridView)
        Dim oSql As New SQLInventarios(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            'Dim keyprod As String = oGrid.DataKeys(oGrid.SelectedRow.DataItemIndex).Value
            Dim IDProd As String = lblProductoID.Text
            Dim Producto As String = oGrid.SelectedRow.Cells(2).Text
            Dim nolote As String = oGrid.SelectedRow.Cells(3).Text
            txt_Producto.ToolTip = IDProd + "|" + nolote
            txt_Producto.Text = Producto
            txt_Origen.Text = ConstruirLote(nolote)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub

    'Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As EventArgs)
    '    CargarProducto(GridView4)
    'End Sub

    Sub Button_Click(sender As Object, e As EventArgs)
        CargarProducto(GridView4)
    End Sub

    Protected Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        CargarProducto(GridView4)
        LoadListaProductos()
    End Sub
    Protected Sub ImgBtnLimpiar_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnLimpiar.Click
        txt_Producto.Text = ""
        txt_Producto.ToolTip = ""
        txt_Origen.Text = ""
    End Sub

    Protected Sub ImgBtnAceptar_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnAceptar.Click
        If Save(lblAcción.Text) Then
            LoadLista()
            LoadListaProductos()
            SetFormConfig()
        End If
    End Sub
    Protected Sub ImgBtnCancelar_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnCancelar.Click
        SetFormConfig()
    End Sub


    Private Function Save(ByVal sAcc As String) As Boolean
        Dim oSql As New SQLDetalleSalidaSemilla(oUsr)
        Dim oCs As New ColeccionPrmSql
        Save = False
        Try
            oCs.Create("@SalidaSemillaIDF", lbl_NoSalida.Text)
            oCs.Create("@ProductoID", txt_Producto.ToolTip.Split("|")(0))
            oCs.Create("@ProductoLote", txt_Producto.ToolTip.Split("|")(1))
            Dim oTb As DataTable = oSql._Item(oSql.Item, "SALIDASEMILLADETALLE", oCs)
            If Not oTb Is Nothing Then
                oTb.Columns("SalidaSemillaIDF").Unique = True
                oTb.Columns("ProductoID").Unique = True
                oTb.Columns("ProductoLote").Unique = True
                'oTb.Columns("ConteoID").AutoIncrement = True
                If oTb.Rows.Count = 0 Then
                    Dim Dr As DataRow = oTb.NewRow
                    Dr("SalidaSemillaIDF") = iKey
                    Dr("ProductoID") = txt_Producto.ToolTip.Split("|")(0)
                    Dr("ProductoLote") = txt_Producto.ToolTip.Split("|")(1)
                    Dr("SalidaSemillaCantidad") = txt_Cantidad.Text
                    Dr("SalidaSemillaPeso") = txt_Peso.Text
                    Dr("SalidaSemillaDensidad") = txt_Densidad.Text
                    Dr("SalidaSemillaObservaciones") = txt_Observaciones.Text
                    oTb.Rows.Add(Dr)
                    Return oSql.StatemenInsert(oTb)
                    'If PeriodoDisponible(Dr("epr_keygpo"), Dr("epr_keycia"), CDate(txt_erp_fecfin.Text)) Then
                    '    CustomValidator1.IsValid = True
                    '    oTb.Rows.Add(Dr)
                    '    Return oSql.StatemenInsert(oTb)
                    'Else
                    '    CustomValidator1.IsValid = False
                    'End If
                Else
                    ' Edita
                    Dim Dr As DataRow = oTb.Rows(0)
                    Dr("SalidaSemillaCantidad") = txt_Cantidad.Text
                    Dr("SalidaSemillaPeso") = txt_Peso.Text
                    Dr("SalidaSemillaDensidad") = txt_Densidad.Text
                    Dr("SalidaSemillaObservaciones") = txt_Observaciones.Text
                    Return oSql.StatemenUpdate(oTb)
                End If
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Function


    'Protected Sub ImgBtnDelete_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnDelete.Click
    '    Try
    '        Dim iKey As Integer = sender.DataKeys(e.RowIndex).Value()
    '        oCs.Create("@carr_keycarr", iKey)
    '        With GridView1
    '            For Each oRow As GridViewRow In .Rows

    '            Next
    '        End With
    '    Catch ex As Exception
    '        Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
    '    End Try
    'End Sub


    'Protected Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
    '    Try
    '        For Each row As GridViewRow In GridView1.Rows
    '            If row.RowType = DataControlRowType.DataRow Then
    '                If row.Enabled Then
    '                    Dim cb As ImageButton = row.FindControl("ImgBtnDelete")
    '                    If cb IsNot Nothing Then
    '                        'cb.OnClientClick = "Delete"
    '                        If cb.OnClientClick = True Then
    '                            Dim i As Integer = 0
    '                            i = i + 1
    '                        End If
    '                    End If
    '                End If
    '                End If
    '        Next
    '    Catch ex As Exception
    '        Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
    '    End Try
    'End Sub

    Sub Delete_Click(sender As Object, e As EventArgs)
        Dim i As Integer = 0
        i = i + 1
    End Sub

    'Private Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        Dim bTn As ImageButton = e.Row.FindControl("ImgBtnDelete")
    '        If Not bTn Is Nothing Then
    '            'bTn.Attributes.Add("onclick", "return confirm('¿Realmente desea eliminar este registro?')")
    '        End If
    '    End If
    'End Sub

    Private Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        Dim oSQL As New SQLDetalleSalidaSemilla(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            'Dim row As GridViewRow = TryCast(sender.NamingContainer, GridViewRow)            
            'Dim txtNum As Label = GridView3.FindControl("Label1")
            Dim iKey As Integer = sender.DataKeys(e.RowIndex).Value()
            oCs.Create("@carr_keycarr", iKey)
            'oCs.Create("@ptl_keyptl", txtkey.Text)

            With GridView1
                For Each oRow As GridViewRow In .Rows
                    'Dim cb As DropDownList = oRow.FindControl("DDLUBICACION6")
                    Dim cb As Label = oRow.FindControl("Label1")
                    If cb IsNot Nothing Then
                        If Val(cb.Text) = iKey And Val(cb.Text) > 0 Then
                            If oSQL.ExecuteQry(oSQL.Delete, oCs) Then
                                LoadLista()
                            End If
                        End If
                    End If
                Next
            End With

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

    'Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
    '    Response.Write("<h1>Nombre de categoría seleccionada: " + e.CommandName + "</h1>")
    'End Sub


    'Funciones

    Private Function ExisteFlete(ByRef keysal As Integer) As String
        Dim oSql As New SQLSalidaSemilla(oUsr)
        Dim oCs As New ColeccionPrmSql
        ExisteFlete = ""
        Try
            oCs.Create("@SalidaSemillaIDF", keysal)
            oCs.Create("_VALOR", "Existfle")
            Return oSql._Valor(oSql.ExisteFlete, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function


    Private Function ConstruirLote(ByRef Lote As String) As String
        Dim oSql As New SQLSalidaSemilla(oUsr)
        Dim oCs As New ColeccionPrmSql
        ConstruirLote = ""
        Try
            oCs.Create("@Lote", Lote)
            oCs.Create("_VALOR", "OrigenProd")
            Return oSql._Valor(oSql.DesLote, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function





End Class