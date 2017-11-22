Imports Security_System
Imports DataAgro
Imports ClosedXML.Excel
Imports System.IO
Imports System
Imports System.Collections
Imports System.Drawing
Imports System.Drawing.Printing
Imports OfficeOpenXml
Imports OfficeOpenXml.Drawing
Imports DocumentFormat.OpenXml
Imports DocumentFormat.OpenXml.Packaging

Public Class InventarioGrl
    Inherits System.Web.UI.Page
    Private Ds As New DataSet
    Private oUsr As UserLogin
    Private oCs As New ColeccionPrmSql

    Private Sub InventarioGrl_Init(sender As Object, e As EventArgs) Handles Me.Init
        oUsr = Session("Usr")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ViewState("Filtros") = ""
            SetFormConfig()
            LoadLista()
            ImgBtnAceptar.Attributes.Add("onclick", "return confirm('¿Realmente desea Ajustar las Cantidades?')")
        End If
    End Sub


    '==============================================================================================================================
    'Inicialización 
    '==============================================================================================================================
    Private Sub SetFormConfig()
        pnlEventos.Visible = True
        pnlFiltros.Visible = False
        pnlAdd.Visible = False
        'pnlLoading.Visible = False
        pnlListar.Visible = True
        'With BarEventos1
        '    .Nuevo = False
        '    .Eliminar = False
        '    .Editar = False
        '    .Exportar = True
        '    .Filtrar = True
        '    .Listar = False
        '    .Especial1 = True
        'End With

        With BarEventos1
            .Exportar.Boton.Visible = True
            .Exportar.Boton.ToolTip = "Exportar"
            .Filtrar.Boton.Visible = True
            .Filtrar.Boton.ToolTip = "Filtrar"
            .Especial1.Boton.Visible = True
            .Especial1.Boton.ImageUrl = "~/Img/Recibo.jpg"
            .Especial1.Boton.ToolTip = "Ver Movimientos"
        End With

        'Valores predeterminados
        'Tamaño de pagina predeterminado
        GridView1.PageSize = 50

        txt_Fecha.Text = ""
        DDL_EMPRESA.SelectedIndex = -1
        DDL_ALM.SelectedIndex = -1
        DDL_PRODUCTO.SelectedIndex = -1
        txt_Lote.Text = ""
        txt_Peso.Text = ""
        txt_Ubicacion.Text = ""
        txt_CantLogica.Text = ""
        txt_CantFisica.Text = ""

        txt_Fecha.Enabled = False
        DDL_EMPRESA.Enabled = False
        DDL_ALM.Enabled = False
        DDL_PRODUCTO.Enabled = False
        txt_Lote.Enabled = False
        txt_Peso.Enabled = False
        txt_Ubicacion.Enabled = False
        txt_CantLogica.Enabled = False
        txt_CantFisica.Enabled = False

        Dim oSqlE As New SQLEmpresa(oUsr)
        Dim lCsE As New ColeccionPrmSql
        lCsE.Create("@status", oUsr.Mis.Status)
        lCsE.Create("_Tabla", "EMPRESAS")
        lCsE.Create("_Qry", oSqlE.ListBasica)
        lCsE.Create("_Order", "EmpresaNombre")
        'lCsE.Create("_Filtro", "ubi_keytub = 'I'")
        lCsE.Create("_DefaultKey", 0)
        lCsE.Create("_DefaultDes", "[SELECCIONAR]")
        LoadCombo(oUsr, DDL_EMPRESA, lCsE)
        DDL_EMPRESA.SelectedIndex = -1

        Dim oSqlP As New SQLProductosAgro(oUsr)
        lCsE.ItemValue("_Tabla") = "PRODUCTOS"
        lCsE.ItemValue("_Qry") = oSqlP.List_Combo
        lCsE.ItemValue("_Order") = "ProductoNombre"
        lCsE.ItemValue("_DefaultKey") = "%"
        lCsE.ItemValue("_DefaultDes") = "[SELECCIONAR]"
        LoadCombo(oUsr, DDL_PRODUCTO, lCsE)
        DDL_PRODUCTO.SelectedIndex = -1

    End Sub

    Public Sub LoadComboAlmacen(ByRef EmpresaID As Integer)
        Dim oSqlE As New SQLAlmacenAgro(oUsr)
        Dim lCsE As New ColeccionPrmSql
        Try
            lCsE.Create("@EmpresaID", EmpresaID)
            lCsE.Create("_Tabla", "ALMACENES")
            lCsE.Create("_Qry", oSqlE.List_Combo)
            lCsE.Create("_Order", "UbicacionNombre")
            lCsE.Create("_DefaultKey", "%")
            lCsE.Create("_DefaultDes", "[SELECCIONAR]")
            LoadCombo(oUsr, DDL_ALM, lCsE)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub

    '================================================================================================================================
    'Acciones con el modelo de datos
    Private Sub LoadLista()
        Dim oSql As New SQLInventariosAgro(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            oCs.Create("@Empresa", txtSearch_Emp.Text & "%")
            oCs.Create("@Almacen", txtSearch_Alm.Text & "%")
            oCs.Create("@Producto", txtSearch_Prod.Text & "%")
            oCs.Create("@Lote", txtSearch_Lote.Text & "%")
            If IsDate(txtSearch_Fecini.Text) And IsDate(txtSearch_Fecfin.Text) Then
                oCs.Create("@fecini", CDate(txtSearch_Fecini.Text))
                oCs.Create("@fecfin", CDate(txtSearch_Fecfin.Text))
            Else
                oCs.Create("@fecini", CDate(ObtenerFecha("I")))
                oCs.Create("@fecfin", CDate(ObtenerFecha("F")))
            End If
            'If CheckSearch_Invent.Checked = True Then
            '    'oCs.Create("@Exist", "AND ProductoLote IS NOT NULL")
            '    oCs.Create("_FiltroGrid", "ProductoLote IS NOT NULL")
            'Else
            '    'oCs.Create("@Exist", "AND ProductoID LIKE '%'")
            '    oCs.Create("_FiltroGrid", "ProductoID LIKE '%'")
            'End If
            Dim oTabla As DataTable = oSql._List(oSql.ListBasica, "VW_CONSTRUIR_CONTEOS", oCs)
            Dim oTb As DataTable = oSql._List(oSql.ListExport, "VW_CONSTRUIR_CONTEOS", oCs)
            LoadGrid(GridView1, oTabla)
            If Session("oTbGridExport") Is Nothing Then
                Session.Add("oTbGridExport", oTb)
            Else
                Session("oTbGridExport") = oTb
            End If

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

    Private Function ObtenerFecha(ByRef Condicion As String) As String
        Dim oSql As New SQLInventariosAgro(oUsr)
        Dim oCs As New ColeccionPrmSql
        ObtenerFecha = ""
        Try
            'oCs.Create("@keysal", Condicion)
            oCs.Create("_VALOR", "InventarioInicial")
            If Condicion = "I" Then
                Return oSql._Valor(oSql.ObtenerFechaIni, oCs)
            Else
                Return oSql._Valor(oSql.ObtenerFechaFin, oCs)
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Protected Sub imgBtnAplicaFiltro_Click(sender As Object, e As EventArgs) Handles imgBtnAplicaFiltro.Click
        'pnlFiltros.Visible = False
        LoadLista()
    End Sub

    Protected Sub imgbtnCancelaFiltro_Click(sender As Object, e As EventArgs) Handles imgbtnCancelaFiltro.Click
        pnlFiltros.Visible = False
        txtSearch_Emp.Text = ""
        txtSearch_Alm.Text = ""
        txtSearch_Prod.Text = ""
        txtSearch_Lote.Text = ""
        txtSearch_Fecini.Text = ""
        txtSearch_Fecfin.Text = ""
    End Sub

    Private Sub BarEventos1_MsgEvent(sAcción As String) Handles BarEventos1.MsgEvent
        Select Case sAcción
            Case "Nuevo"
                'SetFormEdit(sAcción, GridView1)
                'pnlFlete.Visible = Not pnlFlete.Visible

            Case "Eliminar"
                'SetFormEdit(sAcción, GridView1)
            Case "Editar"
                'SetFormEdit(sAcción, GridView1)
            Case "Filtrar"
                pnlFiltros.Visible = True
            Case "Exportar"
                Dim Hoja As String = "Inventario" + " " + DateTime.Now.ToString("dd/MM/yyyy")
                'ExportClosedXML("Inventario")
                GeneraExcel(Hoja)

            Case "Especial1"
                SetFormEdit(sAcción, GridView1)

            Case Else
        End Select
    End Sub

    Private Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        sender.PageIndex = e.NewPageIndex
        LoadLista()
    End Sub

    Private Sub GridView1_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles GridView1.SelectedIndexChanging
        Dim EmpresaID As Integer = GridView1.DataKeys(e.NewSelectedIndex).Values(0)
        Dim UbicacionID As String = GridView1.DataKeys(e.NewSelectedIndex).Values(1)
        Dim ProductoID As String = GridView1.DataKeys(e.NewSelectedIndex).Values(2)
        Dim ProductoLote As String = GridView1.DataKeys(e.NewSelectedIndex).Values(3)
        If EmpresaID > 0 And UbicacionID <> "" And ProductoID <> "" And ProductoLote <> "" Then
            lblInventario.Text = EmpresaID.ToString + "|" + UbicacionID + "|" + ProductoID + "|" + ProductoLote
        End If
    End Sub


    Private Sub SetFormEdit(ByVal sAcc As String, ByVal oGrid As GridView)
        Dim oSql As New SQLInventariosAgro(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            Select Case sAcc
                Case "Nuevo"
                    'Se podria configurar botones de aceptar y cancelar

                Case "Editar"

                Case "Eliminar"

                Case "Especial1"
                    If oGrid.SelectedRow IsNot Nothing Then
                        If lblInventario.Text <> "" Then
                            pnlEventos.Visible = False
                            pnlFiltros.Visible = False
                            pnlListar.Visible = False
                            pnlAdd.Visible = True

                            oCs.Create("@keycia", lblInventario.Text.Split("|")(0))
                            oCs.Create("@keyubi", lblInventario.Text.Split("|")(1))
                            oCs.Create("@keyprod", lblInventario.Text.Split("|")(2))
                            oCs.Create("@nolote", lblInventario.Text.Split("|")(3))
                            Dim oTb As DataTable = oSql._Item(oSql.Item, "INVENTARIOS", oCs)
                            Dim fecha As String = ""
                            If Not oTb Is Nothing Then
                                For Each Dr As DataRow In oTb.Rows
                                    lblAcción.Text = sAcc
                                    fecha = Dr("InventarioInicial").ToString
                                    txt_Fecha.Text = CDate(fecha.Split(" ")(0))
                                    GetIndex(DDL_EMPRESA, Dr("EmpresaID").ToString)
                                    LoadComboAlmacen(DDL_EMPRESA.SelectedValue)
                                    GetIndex(DDL_ALM, Dr("UbicacionID").ToString)
                                    GetIndex(DDL_PRODUCTO, Dr("ProductoID").ToString)
                                    txt_Lote.Text = Dr("ProductoLote").ToString
                                    txt_Peso.Text = Dr("InventarioPeso").ToString
                                    txt_Ubicacion.Text = Dr("InventarioUbicacionPasillo").ToString
                                    txt_CantLogica.Text = Dr("InventarioLogico").ToString
                                    txt_CantFisica.Text = Dr("InventarioFisico").ToString
                                    Exit For
                                Next
                            End If
                        End If
                    End If

                Case Else
                    'Dim iKey As Integer = oGrid.DataKeys(oGrid.SelectedRow.DataItemIndex).Value
                    'oCs.Create("@PersonalID", iKey)
                    'Dim oTb As DataTable = oSql._Item(oSql.Item, "R", oCs)
                    'If Not oTb Is Nothing Then
                    '    For Each Dr As DataRow In oTb.Rows
                    '        lblAcción.Text = sAcc
                    '        lbl_PersonalID.Text = iKey.ToString
                    '        lbl_PersonalNomFull.Text = Dr("PersonalNomFull").ToString
                    '        txt_PersonalRfc.Text = Dr("PersonalRfc").ToString
                    '        txt_PersonalCurp.Text = Dr("PersonalCurp").ToString
                    '        txt_PersonalFecAlta.Text = Dr("PersonalFecAlta").ToShortDateString
                    '        Exit For
                    '    Next
                    'End If

            End Select

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

    Private Function ExportClosedXML(ByVal sHoja As String) As Boolean
        ExportClosedXML = False
        Try
            Using dt As DataTable = DirectCast(Session("oTbGridExport"), DataTable)
                dt.Columns(0).Caption = "Empresa"
                dt.Columns(1).Caption = "Almacen"
                dt.Columns(2).Caption = "Producto"
                dt.Columns(3).Caption = "Lote"
                dt.Columns(4).Caption = "Ubicación"
                dt.Columns(5).Caption = "Inventario Lógico"
                dt.Columns(6).Caption = "InventarioFisico"
                dt.Columns(7).Caption = "Peso"
                dt.Columns(8).Caption = "Fecha Corte"
                Using wb As New XLWorkbook()
                    wb.Worksheets.Add(dt, sHoja)
                    Response.Clear()
                    Response.Buffer = True
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;filename=InventarioActual.xlsx")
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



    Public Sub GeneraExcel(ByVal sHoja As String)
        ' Creamos el archivo
        Dim ep As New ExcelPackage()
        Dim oSql As New SQLInventarios(oUsr)
        Dim cs As New ColeccionPrmSql
        'ep.Workbook.Worksheets.Add()
        'LibroExcel = ep.Workbooks.Add()
        ep.Workbook.Worksheets.Add(sHoja)
        Dim ew1 As ExcelWorksheet = ep.Workbook.Worksheets(1)
        Using dt As DataTable = DirectCast(Session("oTbGridExport"), DataTable)

            'Importamos Una imagen 
            Dim imageFile As String = Server.MapPath("~/") + "Img\image007.jpg"
            Dim imageFile2 As String = Server.MapPath("~/") + "Img\Aguilares.jpg"

            ' Definir el valor de una celda de ambas maneras
            'ew1.Cells(1, 2).Value = "Invernaderos Arroyo SPR de R"
            ew1.Cells("A2").Value = "Aguilares SPR de RL"
            'ew1.Cells("A2").Value = "Invernaderos Arroyo SPR de R"
            ' Unir dos o mas celdas de ambas maneras
            'ew1.Cells(1, 1, 1, 16).Merge = True
            ew1.Cells("A2:I2").Merge = True
            ' Aplicar estilo al tipo de letra
            ew1.Cells(1, 2).Style.Font.Bold = True
            ew1.Cells("A2").Style.Font.Bold = True
            ' Definir el estilo de los bordes de la celda(s)
            ew1.Cells("A5:I5").Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.[Double]
            ' Definir la alineación horizontal y vertical
            ew1.Cells("A2").Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center
            ew1.Cells("A2").Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center
            ' Definir el estilo del tipo de letra
            ew1.Cells("A2").Style.Font.Bold = True
            'Define Tamaño y estilo de letra a celdas
            ew1.Cells("A2:I2").Style.Font.Size = 18
            ew1.Cells("A2:I2").Style.Font.Name = "Baskerville Old Face"

            'ew1.Cells("A5:Z5").AutoFilter.ToString()
            ew1.Cells("A5:I5").AutoFitColumns()

            ' Definir el valor de una celda de ambas maneras
            'ew1.Cells(1, 1).Value = "PROGRAMA DE DISPONIBILIDAD DE PLANTA"
            ew1.Cells("A3").Value = "INVENTARIO ACTUAL"
            ' Unir dos o mas celdas de ambas maneras
            'ew1.Cells(1, 1, 1, 20).Merge = True
            ew1.Cells("A3:I3").Merge = True
            ' Aplicar estilo al tipo de letra
            ew1.Cells(1, 1).Style.Font.Bold = True
            ew1.Cells("A3").Style.Font.Bold = True
            ' Definir el estilo de los bordes de la celda(s)
            ew1.Cells("A3:I3").Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.[Double]
            ' Definir la alineación horizontal y vertical
            ew1.Cells("A3").Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center
            ew1.Cells("A3").Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center
            ' Definir el estilo del tipo de letra
            ew1.Cells("A3").Style.Font.Bold = True
            ew1.Cells("A3:I3").Style.Font.Size = 18
            ew1.Cells("A3:I3").Style.Font.Name = "Baskerville Old Face"


            Dim width As Double
            Dim height As Double
            Dim image As System.Drawing.Image = System.Drawing.Image.FromFile(imageFile)
            Try
                width = image.Width * 10.0 / image.HorizontalResolution
                height = image.Height * 10.0 / image.VerticalResolution
            Finally
                image.Dispose()
                'ew1.Drawings.AddPicture(imageFile, image)
            End Try

            AddImage(ew1, 0, 0, imageFile)
            Dim logo As Image = Image.FromFile(imageFile2)

            For a As Integer = 1 To 1
                ew1.Row(a * 14).Height = 25.0
                ew1.Column(a * 14).Width = 25.0
            Next

            For a As Integer = 0 To 0
                Dim picture = ew1.Drawings.AddPicture(a.ToString(), logo)
                picture.SetPosition(a * 1, 0, 7, 0)
            Next

            'La función LoadFromDataTable transfiere la data del objeto DataTable al objecto de excel.
            ew1.Cells("A5").LoadFromDataTable(dt, True)

            ' Definir el valor de las celdas de encabezado del contenidos
            ew1.Cells("A5").Value = "EMPRESA."
            ew1.Cells("A6:A1000").Style.Font.Size = 10
            ew1.Cells("A6:A1000").Style.Font.Name = "Arial"

            ew1.Cells("B5").Value = "ALMACEN"
            ew1.Cells("B5:B1000").Style.Font.Size = 10
            ew1.Cells("B5:B1000").Style.Font.Name = "Arial"
            ew1.Cells("B6:B1000").Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center
            ew1.Cells("B6:B1000").Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center

            ew1.Cells("C5").Value = "PRODUCTO"
            ew1.Cells("C5:C1000").Style.Font.Size = 10
            ew1.Cells("C5:C1000").Style.Font.Name = "Arial"
            ew1.Cells("C6:C1000").Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center
            ew1.Cells("C6:C1000").Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center

            ew1.Cells("D5").Value = "LOTE"
            ew1.Cells("D5:D1000").Style.Font.Size = 10
            ew1.Cells("D5:D1000").Style.Font.Name = "Arial"
            ew1.Cells("D6:D1000").Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center
            ew1.Cells("D6:D1000").Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center

            ew1.Cells("E5").Value = "UBICACIÓN"
            ew1.Cells("E5:E1000").Style.Font.Size = 10
            ew1.Cells("E5:E1000").Style.Font.Name = "Arial"
            ew1.Cells("E6:E1000").Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center
            ew1.Cells("E6:E1000").Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center

            'ew1.Cells("B11:B1000").AddComment("Danger: this number seems odd!", "Juan de Dios")
            'ew1.Cells("B11:B1000").Comment.Author = oUsr.FirstName

            'ew1.Cells("B11:B1000").AddComment("Listado de recibos", "Masanasa")
            'rowindex = 3

            ew1.Cells("F5").Value = "INVENTARIO" & vbCrLf & "LÓGICO"
            ew1.Cells("F5:F1000").Style.Font.Size = 10
            ew1.Cells("F5:F1000").Style.Font.Name = "Arial"
            ew1.Cells("F6:F1000").Style.Numberformat.Format = "#,##0"
            ew1.Cells("F6:F1000").Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center
            ew1.Cells("F6:F1000").Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center

            ew1.Cells("G5").Value = "INVENTARIO" & vbCrLf & "FISICO"
            ew1.Cells("G5:G1000").Style.Font.Size = 10
            ew1.Cells("G5:G1000").Style.Font.Name = "Arial"
            ew1.Cells("G6:G1000").Style.Numberformat.Format = "#,##0"
            ew1.Cells("G6:G1000").Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center
            ew1.Cells("G6:G1000").Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center

            ew1.Cells("H5").Value = "PESO"
            ew1.Cells("H5:H1000").Style.Font.Size = 10
            ew1.Cells("H5:H1000").Style.Font.Name = "Arial"
            ew1.Cells("H6:H1000").Style.Numberformat.Format = "#,##0"
            ew1.Cells("H6:H1000").Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center
            ew1.Cells("H6:H1000").Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center

            ew1.Cells("I5").Value = "FECHA" & vbCrLf & " " & "CORTE"
            ew1.Cells("I5:I1000").Style.Font.Size = 10
            ew1.Cells("I5:I1000").Style.Font.Name = "Arial"
            ew1.Cells("I6:I1000").Style.Numberformat.Format = "dd-mm-yyyy"
            ew1.Cells("I6:I1000").Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center
            ew1.Cells("I6:I1000").Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center

            ' Aplicar estilo al tipo de letra            
            'ew1.Cells(1, 16).Style.Font.Bold = True
            ew1.Cells("A5:I5").Style.Font.Bold = True
            ' Definir el estilo de los bordes de la celda(s)
            ew1.Cells("A5:I5").Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.[Double]
            ' Definir la alineación horizontal y vertical
            ew1.Cells("A5:I5").Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center
            ew1.Cells("A5:I5").Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center
            ' Definir el estilo del tipo de letra
            ew1.Cells("A5:I5").Style.Font.Bold = True
            ew1.Cells("A5:I5").Style.Font.Size = 10
            ew1.Cells("A5:I5").Style.Font.Name = "Arial"

            dt.Clear()
            cs.Clear()
            'dt.Reset()
            'dt.Dispose()

        End Using
        Response.Clear()
        Response.Buffer = True
        Response.Charset = ""
        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
        Response.AddHeader("content-disposition", "attachment;filename= InventarioActual" + ".xlsx")
        Using MyMemoryStream As New MemoryStream()
            ep.SaveAs(MyMemoryStream)
            MyMemoryStream.WriteTo(Response.OutputStream)
            Response.Flush()
            Response.End()
        End Using
    End Sub


    Private Sub AddImage(oSheet As ExcelWorksheet, rowIndex As Integer, colIndex As Integer, imagePath As String)
        Dim image As New Bitmap(imagePath)
        Dim excelImage As ExcelPicture = Nothing
        If image IsNot Nothing Then
            excelImage = oSheet.Drawings.AddPicture("Debopam Pal", image)
            excelImage.From.Column = colIndex
            excelImage.From.Row = rowIndex
            excelImage.SetSize(75, 60)
            ' 2x2 px space for better alignment
            excelImage.From.ColumnOff = Pixel2MTU(2)
            excelImage.From.RowOff = Pixel2MTU(2)

            image.Dispose()

        End If
    End Sub

    Public Function Pixel2MTU(pixels As Integer) As Integer
        Dim mtus As Integer = pixels * 9525
        Return mtus
    End Function


    Private Function Save(ByVal sAcc As String) As Boolean
        Dim oSql As New SQLInventariosAgro(oUsr)
        Dim oCs As New ColeccionPrmSql
        Save = False
        Try
            oCs.Create("@keyprod", DDL_PRODUCTO.SelectedValue)
            oCs.Create("@nolote", txt_Lote.Text)
            oCs.Create("@keycia", DDL_EMPRESA.SelectedValue)
            oCs.Create("@keyubi", DDL_ALM.SelectedValue)
            Dim oTb As DataTable = oSql._Item(oSql.Item, "INVENTARIOS", oCs)
            If Not oTb Is Nothing Then
                oTb.Columns("EmpresaID").Unique = True
                oTb.Columns("UbicacionID").Unique = True
                oTb.Columns("ProductoID").Unique = True
                oTb.Columns("ProductoLote").Unique = True
                'oTb.Columns("ConteoID").AutoIncrement = True
                If oTb.Rows.Count > 0 Then
                    Dim Dr As DataRow = oTb.Rows(0)
                    Dr("InventarioLogico") = txt_CantFisica.Text
                    Return oSql.StatemenUpdate(oTb)
                End If
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Function


    Protected Sub ImgBtnAceptar_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnAceptar.Click
        If Save(lblAcción.Text) Then
            LoadLista()
            SetFormConfig()
            Response.Write("<script>window.alert('El Ajuste se ha Realizado correctamente');</script>")
        End If
    End Sub

    Protected Sub ImgBtnCancelar_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnCancelar.Click
        SetFormConfig()
    End Sub



End Class