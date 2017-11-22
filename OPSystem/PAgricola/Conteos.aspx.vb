Imports Security_System
Imports DataAgro
Imports ClosedXML.Excel
Imports System.IO
Imports System
Imports System.Collections

Public Class Conteos
    Inherits System.Web.UI.Page
    Private Ds As New DataSet
    Private oUsr As UserLogin
    Private oCs As New ColeccionPrmSql

    Private Sub Conteos_Init(sender As Object, e As EventArgs) Handles Me.Init
        oUsr = Session("Usr")
        If oUsr Is Nothing Then
            Response.Redirect("Agro.aspx")
        Else
            oUsr.Mis.Función = "Conteos"
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ViewState("Filtros") = ""
            SetFormConfig()
            LoadLista()
        End If

        'Genera Reporte Y redirecciona pantalla de fletes.
        'If Not Session("INFORP") Is Nothing Then
        '    Dim oRp As InfoRp = Session("INFORP")
        '    If oRp.Enviado Then
        '        Dim url As String = Request.ApplicationPath + "PAgricola/DetalleConteo.aspx" + "?" + "Key=" + iKey.ToString
        '        Dim url2 As String = Page.Request.Url.AbsoluteUri
        '        Dim sUrl2 As String = url
        '        'Dim sScript As String = "<script language =javascript> "
        '        'sScript += "window.open('" & sUrl2 & "','_blank');"
        '        'sScript += "</script> "
        '        'Response.Write(sScript)

        '        Dim sScript As String = "<script language =javascript> "
        '        sScript += "window.open('" & sUrl2 & "',null,'toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1,width=500,height=300,left=100,top=100');"
        '        sScript += "</script> "
        '        Response.Write(sScript)
        '    End If
        'End If


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
        '    .Nuevo = True
        '    .Eliminar = False
        '    .Editar = True
        '    .Exportar = False
        '    .Filtrar = True
        '    .Listar = False
        '    .Especial1 = False
        '    .Especial3 = True
        'End With

        With BarEventos1
            .Nuevo.Boton.Visible = True
            .Nuevo.Boton.ToolTip = "Nuevo"
            .Editar.Boton.Visible = True
            .Editar.Boton.ToolTip = "Editar"
            .Filtrar.Boton.Visible = True
            .Filtrar.Boton.ToolTip = "Filtrar"
            .Especial3.Boton.Visible = True
            .Especial3.Boton.ImageUrl = "~/Img/Consultar.png"
            .Especial3.Boton.ToolTip = "Ver Conteos"
        End With

        '<uc1:Buscador ID = "Buscador1" runat = "server" OnAceptarClicked="Buscador1_AceptarClicked"/>

        'Dim ctrlAccPeq As Button
        'ctrlAccPeq = LoadControl("~/MisControles/BarEventos.ascx.ascx")

        'pn.ContentContainer.Controls.Add(ctrlAccPeq)

        'Valores predeterminados
        'Tamaño de pagina predeterminado
        GridView1.PageSize = 10

        txt_Descripción.Text = ""
        txt_Fecha.Text = ""
        DDL_EMPRESA.SelectedIndex = -1
        DDL_ALMACEN.SelectedIndex = -1
        lblkeyconteo.Text = ""

        RFV_DESC.Enabled = False
        RFV_FECHA.Enabled = False
        RFV_EMPRESA.Enabled = False
        RFV_ALMACEN.Enabled = False

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

        Dim oSqlA As New SQLAlmacenAgro(oUsr)
        lCsE.Create("@EmpresaID", 0)
        lCsE.ItemValue("_Tabla") = "ALMACENES"
        lCsE.ItemValue("_Qry") = oSqlA.List_Combo
        lCsE.ItemValue("_Order") = "UbicacionNombre"
        lCsE.ItemValue("_Defaultkey") = "%"
        lCsE.ItemValue("_DefaultDes") = "[SELECCIONAR]"
        LoadCombo(oUsr, DDL_ALMACEN, lCsE)
        DDL_ALMACEN.SelectedIndex = -1


    End Sub

    Public Sub LoadCombos(ByRef EmpresaID As Integer)
        Dim oSqlE As New SQLAlmacenAgro(oUsr)
        Dim lCsE As New ColeccionPrmSql
        Try
            lCsE.Create("@EmpresaID", EmpresaID)
            lCsE.Create("_Tabla", "ALMACENES")
            lCsE.Create("_Qry", oSqlE.List_Combo)
            lCsE.Create("_Order", "UbicacionNombre")
            'lCsE.Create("_Filtro", "ubi_keytub = 'I'")
            lCsE.Create("_DefaultKey", "%")
            lCsE.Create("_DefaultDes", "[SELECCIONAR]")
            LoadCombo(oUsr, DDL_ALMACEN, lCsE)
            DDL_ALMACEN.SelectedIndex = -1
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub


    '================================================================================================================================
    'Acciones con el modelo de datos
    Private Sub LoadLista()
        Dim oSql As New SQLConteos(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            oCs.Create("@Empresa", txtSearch_Emp.Text & "%")
            oCs.Create("@Almacen", txtSearch_Alm.Text & "%")
            oCs.Create("@Desc", txtSearch_Desc.Text & "%")
            oCs.Create("@Fecha", txtSearch_Fecha.Text & "%")
            Dim oTabla As DataTable = oSql._List(oSql.ListBasica, "CONTEOS", oCs)
            LoadGrid(GridView1, oTabla)
            If Session("TablaConteo") Is Nothing Then
                Session.Add("TablaConteo", oTabla)
            Else
                Session("TablaConteo") = oTabla
            End If

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

    Protected Sub imgBtnAplicaFiltro_Click(sender As Object, e As EventArgs) Handles imgBtnAplicaFiltro.Click
        pnlFiltros.Visible = True
        LoadLista()
    End Sub

    Protected Sub imgbtnCancelaFiltro_Click(sender As Object, e As EventArgs) Handles imgbtnCancelaFiltro.Click
        txtSearch_Emp.Text = ""
        txtSearch_Alm.Text = ""
        txtSearch_Desc.Text = ""
        txtSearch_Fecha.Text = ""
        pnlFiltros.Visible = False
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
                pnlFiltros.Visible = True
            Case "Exportar"
                ExportClosedXML("Inventarios")
            Case "Especial3"
                SetFormEdit(sAcción, GridView1)
            Case Else
        End Select
    End Sub

    Protected Sub textbox_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim Numcarr As String = ""
        Dim txt As TextBox = TryCast(sender, TextBox)
        Dim row As GridViewRow = TryCast(sender.NamingContainer, GridViewRow)
        Dim imgb As TextBox = row.FindControl("txtAdd")

    End Sub

    'Protected Sub OnAceptarClicked(ByVal sender As Object, ByVal e As EventArgs)
    '    'Dim row As GridViewRow = TryCast(sender.NamingContainer, GridViewRow)
    '    ''Dim 
    '    'Dim imgb As Button = row.FindControl("BarEventos1")
    'End Sub

    'Protected Sub BarEventos1_AceptarClicked(sender As Object, e As EventArgs)
    '    Dim bander As String = ""
    '    If BarEventos1.Nuevo = True Then
    '        bander = "S"
    '    Else
    '        bander = "N"
    '    End If

    '    If BarEventos1.Filtrar = True Then
    '        bander = "S"
    '    Else
    '        bander = "N"
    '    End If

    'End Sub


    Private Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        sender.PageIndex = e.NewPageIndex
        LoadLista()
    End Sub

    Private Sub GridView1_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles GridView1.SelectedIndexChanging
        Dim ikey As Integer = GridView1.DataKeys(e.NewSelectedIndex).Value()
        If ikey > 0 Then
            lblkeyconteo.Text = ikey
        End If
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

                    RFV_DESC.Enabled = True
                    RFV_FECHA.Enabled = True
                    RFV_EMPRESA.Enabled = True
                    RFV_ALMACEN.Enabled = True

                Case "Editar"
                    If oGrid.SelectedRow IsNot Nothing Then
                        pnlEventos.Visible = False
                        pnlFiltros.Visible = False
                        pnlAdd.Visible = True
                        pnlListar.Visible = False

                        RFV_DESC.Enabled = True
                        RFV_FECHA.Enabled = True
                        RFV_EMPRESA.Enabled = True
                        RFV_ALMACEN.Enabled = True

                        'Dim iKey As Integer = oGrid.DataKeys(oGrid.SelectedRow.DataItemIndex).Value
                        Dim ikey As Integer = Val(lblkeyconteo.Text)
                        oCs.Create("@ConteoID", ikey)
                        Dim oTb As DataTable = oSql._Item(oSql.Item, "CONTEOS", oCs)
                        If Not oTb Is Nothing Then
                            For Each Dr As DataRow In oTb.Rows
                                Dim fecha As String = ""
                                lblAcción.Text = sAcc
                                lbl_NoConteo.Text = ikey.ToString
                                txt_Descripción.Text = Dr("ConteoDescripcion").ToString
                                fecha = Dr("ConteoFecha").ToString
                                txt_Fecha.Text = CDate(fecha)
                                GetIndex(DDL_EMPRESA, Dr("EmpresaID").ToString)
                                LoadCombos(DDL_EMPRESA.SelectedValue)
                                GetIndex(DDL_ALMACEN, Dr("UbicacionID").ToString)
                                Exit For
                            Next
                        End If
                    End If
                Case "Eliminar"

                Case "Especial3"

                    'If oGrid.SelectedRow IsNot Nothing Then
                    '    Dim iKey As Integer = oGrid.DataKeys(oGrid.SelectedRow.DataItemIndex).Value
                    '    OpenDetalle(iKey)
                    'End If

                    If oGrid.SelectedRow IsNot Nothing Then
                        If Val(lblkeyconteo.Text) > 0 Then
                            OpenDetalle(Val(lblkeyconteo.Text))
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

    Private Sub OpenDetalle(ByVal ikeycont As Integer)
        Dim Dsr As New DataSet
        Dim oCs As New ColeccionPrmSql
        Dim oRp As New InfoRp
        Try
            Session.Remove("INFORPT")
            With oRp
                .Response = "DetalleConteo"
                .ikey = ikeycont
                .Enviado = True
            End With
            Session.Add("INFORPT", oRp)

            'Redirecciona a pantalla de Detalle Conteos.
            'Dim url As String = Request.ApplicationPath + "PAgricola/DetalleConteo.aspx" + "?" + "Key=" + ikeycont.ToString
            Dim url As String = Request.ApplicationPath + "PAgricola/DetalleConteo.aspx"
            Dim url2 As String = Page.Request.Url.AbsoluteUri
            'Dim sUrl2 As String = url
            Dim sUrl2 As String = "DetalleConteo.aspx"
            Dim sScript As String = "<script language =javascript> "
            sScript += "window.open('" & sUrl2 & "','DetalleConteo');"
            sScript += "</script> "
            Response.Write(sScript)

            'Dim sScript As String = "<script language =javascript> "
            'sScript += "window.open('" & sUrl2 & "','_blank');"
            'sScript += "</script> "
            'Response.Write(sScript)

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub


    Protected Sub ImgBtnAceptar_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnAceptar.Click
        If Save(lblAcción.Text) Then
            LoadLista()
            SetFormConfig()
        End If
    End Sub

    Protected Sub ImgBtnCancelar_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnCancelar.Click
        SetFormConfig()
    End Sub


    'Private Function Save(ByVal sAcc As String) As Boolean
    '    Dim oSql As New SQLConteos(oUsr)
    '    Dim oCs As New ColeccionPrmSql
    '    Save = False
    '    Try
    '        oCs.Create("@PersonalRfc", txt_PersonalRfc.Text)
    '        oCs.Create("@PersonalCurp", txt_PersonalCurp.Text)
    '        oCs.Create("@PersonalFecAlta", CDate(txt_PersonalFecAlta.Text))
    '        oCs.Create("@PersonalID", lbl_PersonalID.Text)
    '        If sAcc = "Nuevo" Then
    '            oSql.ExecuteQry(oSql.Update, oCs)
    '        Else
    '            Return oSql.ExecuteQry(oSql.Update, oCs)
    '        End If
    '    Catch ex As Exception
    '        Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
    '    End Try
    'End Function


    Private Function Save(ByVal sAcc As String) As Boolean
        Dim oSql As New SQLConteos(oUsr)
        Dim oCs As New ColeccionPrmSql
        Save = False
        Try
            oCs.Create("@ConteoID", lbl_NoConteo.Text)
            Dim oTb As DataTable = oSql._Item(oSql.Item, "CONTEOS", oCs)
            If Not oTb Is Nothing Then
                oTb.Columns("ConteoID").Unique = True
                oTb.Columns("ConteoID").AutoIncrement = True
                If oTb.Rows.Count = 0 Then
                    Dim Dr As DataRow = oTb.NewRow
                    Dr("ConteoDescripcion") = txt_Descripción.Text
                    Dr("ConteoFecha") = txt_Fecha.Text
                    Dr("ConteoUsuarioID") = oUsr.keyusu
                    Dr("ConteoStatus") = oUsr.Mis.Status
                    Dr("EmpresaID") = DDL_EMPRESA.SelectedValue
                    Dr("UbicacionID") = DDL_ALMACEN.SelectedValue
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
                    Dr("ConteoDescripcion") = txt_Descripción.Text
                    Dr("ConteoFecha") = txt_Fecha.Text
                    Dr("EmpresaID") = DDL_EMPRESA.SelectedValue
                    Dr("UbicacionID") = DDL_ALMACEN
                    Return oSql.StatemenUpdate(oTb)
                End If
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Function


    Private Function ExportClosedXML(ByVal sHoja As String) As Boolean
        ExportClosedXML = False
        Try
            Using dt As DataTable = DirectCast(Session("TablaConteo"), DataTable)
                'dt.Columns(0).Caption = "ID Empresa"
                'dt.Columns(1).Caption = "Empresa"
                'dt.Columns(2).Caption = "ID Almacen"
                'dt.Columns(3).Caption = "Almacen"
                'dt.Columns(4).Caption = "ID Producto"
                'dt.Columns(5).Caption = "Producto"
                'dt.Columns(6).Caption = "Lote"
                'dt.Columns(7).Caption = "Lote Historico"
                'dt.Columns(8).Caption = "Ubicación"
                'dt.Columns(9).Caption = "Cantidad Logica"
                'dt.Columns(10).Caption = "Cantidad Fisica"
                'dt.Columns(11).Caption = "Peso"
                'dt.Columns(12).Caption = "Fecha Corte"
                Using wb As New XLWorkbook()
                    wb.Worksheets.Add(dt, sHoja)
                    Response.Clear()
                    Response.Buffer = True
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;filename=SqlExport.xlsx")
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

    Protected Sub DDL_EMPRESA_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDL_EMPRESA.SelectedIndexChanged
        Dim oSqlE As New SQLAlmacenAgro(oUsr)
        Dim lCsE As New ColeccionPrmSql
        Try
            lCsE.Create("@EmpresaID", DDL_EMPRESA.SelectedValue)
            lCsE.Create("_Tabla", "ALMACENES")
            lCsE.Create("_Qry", oSqlE.List_Combo)
            lCsE.Create("_Order", "UbicacionNombre")
            'lCsE.Create("_Filtro", "ubi_keytub = 'I'")
            lCsE.Create("_DefaultKey", "%")
            lCsE.Create("_DefaultDes", "[SELECCIONAR]")
            LoadCombo(oUsr, DDL_ALMACEN, lCsE)
            DDL_ALMACEN.SelectedIndex = -1
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub



End Class