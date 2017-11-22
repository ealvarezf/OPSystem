Imports Security_System
Imports DataAgro
Imports ClosedXML.Excel
Imports System.IO
Imports System
Imports System.Collections
Public Class SalidaSemilla
    Inherits System.Web.UI.Page
    Private Ds As New DataSet
    Private oUsr As UserLogin
    Private oCs As New ColeccionPrmSql
    Dim oRps As InfoRp
    Dim iKeySalida As Integer
    Dim oRp As InfoRp
    Dim iKeyfle As Integer = 0

    Private Sub SalidaSemilla_Init(sender As Object, e As EventArgs) Handles Me.Init
        oUsr = Session("Usr")
        oRps = Session("INFORPT")
        iKeyfle = (Request.Params("key"))
        'ObtenerFlete()
        If oUsr Is Nothing Then
            Response.Redirect("Agro.aspx")
        Else
            oUsr.Mis.Función = "SalidaSemilla"
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ViewState("Filtros") = ""
            Session.Remove("dts")   ' Limpia la sesion que guarda los registros seleccionados y almacenados en el grid de confirmación
            Session.Remove("Csc")   ' Limpia la sesion que que trae los datos del flete Guardado, para guardar sus detalles
            Session.Remove("dtcheck")    ' Limpia la sesion que Guarda el Datatable de los registros que tienen check en true
            SetFormConfig()
            LoadLista()
        End If

        If Not Session("INFORP") Is Nothing Then
            Dim oRp As InfoRp = Session("INFORP")
            If oRp.Enviado Then
                Dim url As String = Request.ApplicationPath + "PAgricola/Show.aspx"
                Dim url2 As String = Page.Request.Url.AbsoluteUri
                'Response.Write("<script>window.open('Show.aspx','_blank');</script>")

                'Dim sUrl2 As String = url
                'Dim sScript As String = "<script language =javascript> "
                'sScript += "window.open('" & sUrl2 & "',null,'toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1,width=1200,height=Auto,left=100,top=100');"
                'sScript += "</script> "
                'Response.Write(sScript)

                Dim sUrl2 As String = url
                Dim sScript As String = "<script language =javascript> "
                sScript += "window.open('" & sUrl2 & "','_blank');"
                sScript += "</script> "
                Response.Write(sScript)
            End If
        End If

    End Sub

    Private Sub ObtenerFlete()
        Try
            If Not oRps Is Nothing Then
                Select Case oRps.Response
                    Case "SalidaSemilla"
                        If oRps.ikey > 0 Then
                            iKeyfle = oRps.ikey
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
    Private Sub SetFormConfig()
        pnlEventos.Visible = True
        pnlFiltros.Visible = False
        pnlFlete.Visible = False
        pnlAdd.Visible = False
        pnlListar.Visible = True
        pnlAddSalFlete.Visible = False
        pnlEventos2.Visible = False



        'With BarEventos1
        '    If iKeyfle > 0 Then
        '        .Nuevo = False
        '        .Eliminar = False
        '        .Editar = False
        '    Else
        '        .Nuevo = True
        '        .Eliminar = True
        '        .Editar = True
        '    End If
        '    .Exportar = True
        '    .Filtrar = True
        '    .Listar = True
        '    .Especial1 = False
        '    .Especial2 = True
        '    .Especial3 = True
        '    .Especial6 = True
        'End With

        With BarEventos1
            If iKeyfle > 0 Then
                .Nuevo.Boton.Visible = False
                .Editar.Boton.Visible = False
                .Eliminar.Boton.Visible = False
            Else
                .Nuevo.Boton.Visible = True
                .Nuevo.Boton.ToolTip = "Nuevo"
                .Editar.Boton.Visible = True
                .Editar.Boton.ToolTip = "Editar"
                .Eliminar.Boton.Visible = True
                .Eliminar.Boton.ToolTip = "Eliminar"
            End If
            .Exportar.Boton.Visible = True
            .Exportar.Boton.ToolTip = "Exportar"
            .Listar.Boton.Visible = True
            .Listar.Boton.ToolTip = "Listar"
            .Filtrar.Boton.Visible = True
            .Filtrar.Boton.ToolTip = "Filtrar"
            .Especial2.Boton.Visible = True
            .Especial2.Boton.ImageUrl = "~/Img/icon-fletes.png"
            .Especial2.Boton.ToolTip = ""
            .Especial3.Boton.Visible = True
            .Especial3.Boton.ToolTip = ""
            .Especial3.Boton.ImageUrl = "~/Img/Consultar.png"
            .Especial6.Boton.Visible = True
            .Especial6.Boton.ImageUrl = "~/Img/camion04.png"
            .Especial6.Boton.ToolTip = ""
        End With

        'With BarEventos1
        '    .Especial3 = True
        'End With

        With BarEventos2
            .Especial3.Boton.Visible = True
            .Especial3.Boton.ImageUrl = "~/Img/Consultar.png"
            .Especial3.Boton.ToolTip = ""
        End With

        '<uc1:Buscador ID = "Buscador1" runat = "server" OnAceptarClicked="Buscador1_AceptarClicked"/>

        'Dim ctrlAccPeq As Button
        'ctrlAccPeq = LoadControl("~/MisControles/BarEventos.ascx.ascx")

        'pn.ContentContainer.Controls.Add(ctrlAccPeq)

        'Valores predeterminados
        'Tamaño de pagina predeterminado
        GridView1.PageSize = 25
        GridView2.PageSize = 25

        lblkeyfle.Text = ""
        lbl_SalidaSem.Text = ""
        txt_Fecha.Text = ""
        DDL_EMPRESA_ORG.SelectedIndex = -1
        DDL_ALM_ORG.SelectedIndex = -1
        txt_Origen.Text = ""
        DDL_EMPRESA_DEST.SelectedIndex = -1
        DDL_ALMACEN_DES.SelectedIndex = -1
        txt_Elaboro.Text = ""
        txt_Encargado.Text = ""
        lblkeysal.Text = ""

        RFV_FECHA.Enabled = False
        RFV_EMPRESAO.Enabled = False
        RFV_ALMACENO.Enabled = False
        RFV_ORIGEN.Enabled = False
        RFV_EMPRESAD.Enabled = False
        RFV_ALMACEND.Enabled = False
        RFV_ELABORO.Enabled = False
        RFV_ENCARGADO.Enabled = False

        Dim oSqlE As New SQLEmpresa(oUsr)
        Dim lCsE As New ColeccionPrmSql
        lCsE.Create("@status", oUsr.Mis.Status)
        lCsE.Create("_Tabla", "EMPRESAS")
        lCsE.Create("_Qry", oSqlE.ListBasica)
        lCsE.Create("_Order", "EmpresaNombre")
        'lCsE.Create("_Filtro", "ubi_keytub = 'I'")
        lCsE.Create("_DefaultKey", 0)
        lCsE.Create("_DefaultDes", "[SELECCIONAR]")
        LoadCombo(oUsr, DDL_EMPRESA_ORG, lCsE)
        LoadCombo(oUsr, DDL_EMPRESA_DEST, lCsE)
        DDL_EMPRESA_DEST.SelectedIndex = -1
        DDL_EMPRESA_ORG.SelectedIndex = -1

        Dim oSqlA As New SQLAlmacenAgro(oUsr)
        lCsE.Create("@EmpresaID", 0)
        lCsE.ItemValue("_Tabla") = "ALMACENES"
        lCsE.ItemValue("_Qry") = oSqlA.List_Combo
        lCsE.ItemValue("_Order") = "UbicacionNombre"
        lCsE.ItemValue("_Defaultkey") = "%"
        lCsE.ItemValue("_DefaultDes") = "[SELECCIONAR]"
        LoadCombo(oUsr, DDL_ALM_ORG, lCsE)
        LoadCombo(oUsr, DDL_ALMACEN_DES, lCsE)
        DDL_ALM_ORG.SelectedIndex = -1
        DDL_ALMACEN_DES.SelectedIndex = -1

    End Sub


    Public Sub LoadComboAlmacen(ByRef EmpresaID_O As Integer, ByRef DesAlm As String)
        Dim oSqlE As New SQLAlmacenAgro(oUsr)
        Dim lCsE As New ColeccionPrmSql
        Try
            lCsE.Create("@EmpresaID", EmpresaID_O)
            lCsE.Create("_Tabla", "ALMACENES")
            lCsE.Create("_Qry", oSqlE.List_Combo)
            lCsE.Create("_Order", "UbicacionNombre")
            lCsE.Create("_DefaultKey", "%")
            lCsE.Create("_DefaultDes", "[SELECCIONAR]")
            If DesAlm = "O" Then
                LoadCombo(oUsr, DDL_ALM_ORG, lCsE)
            Else
                LoadCombo(oUsr, DDL_ALMACEN_DES, lCsE)
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub


    '================================================================================================================================
    'Acciones con el modelo de datos
    Private Sub LoadLista()
        Dim oSql As New SQLSalidaSemilla(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            'If GridView2.Rows.Count > 0 Then
            '    RecorrerGridSalidasCheckedIn()
            '    AgregarSalidasCheckedIn()
            '    'ObtenerSalidasGrid()
            'End If
            oCs.Create("@Folio", txtSearch_Folio.Text & "%")
            oCs.Create("@Fecha", txtSearch_Fecha.Text & "%")
            oCs.Create("@AlmOrigen", txtSearch_AlmO.Text & "%")
            oCs.Create("@AlmDest", txtSearch_AlmD.Text & "%")
            oCs.Create("@FleteID", iKeyfle)
            Dim oTabla As DataTable = oSql._List(IIf(iKeyfle = 0, oSql.List, oSql.ListExistFlete), "SALIDASEMILLA", oCs)
            LoadGrid(GridView1, oTabla)
            If Session("TablaSSemilla") Is Nothing Then
                Session.Add("TablaSSemilla", oTabla)
            Else
                Session("TablaSSemilla") = oTabla
            End If

            'If GridView2.Rows.Count > 0 Then
            '    RecorrerGridSalidasCheckedOut()
            'End If

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

    Private Sub LoadListaAddFlete()
        Dim oSql As New SQLSalidaSemilla(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            If GridView2.Rows.Count > 0 Then
                AgregarSalidasCheckedIn()
                'ObtenerSalidasGrid()
            End If

            oCs.Create("@Folio", txtSearch_Folio.Text & "%")
            oCs.Create("@Fecha", txtSearch_Fecha.Text & "%")
            oCs.Create("@AlmOrigen", txtSearch_AlmO.Text & "%")
            oCs.Create("@AlmDest", txtSearch_AlmD.Text & "%")
            Dim oTabla As DataTable = oSql._List(oSql.List, "SALIDASEMILLA", oCs)
            LoadGrid(GridView2, oTabla)
            If Session("TablaSSemilla") Is Nothing Then
                Session.Add("TablaSSemilla", oTabla)
            Else
                Session("TablaSSemilla") = oTabla
            End If

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

    Public Sub LimpiarGridSalidas()
        Try
            Session.Remove("dtcheck")
            Dim dt As DataTable = TryCast(Session("dtcheck"), DataTable)
            GridView2.DataSource = dt
            GridView2.DataBind()
            Session("dtcheck") = dt
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub


    Public Sub LimpiarGridSalidaFlete()
        Try
            Session.Remove("dts")
            Dim dt As DataTable = TryCast(Session("dts"), DataTable)
            GridView3.DataSource = dt
            GridView3.DataBind()
            Session("dts") = dt
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub

    Protected Sub imgBtnAplicaFiltro_Click(sender As Object, e As EventArgs) Handles imgBtnAplicaFiltro.Click
        pnlFiltros.Visible = False
        LoadLista()
        LoadListaAddFlete()
    End Sub

    Protected Sub imgbtnCancelaFiltro_Click(sender As Object, e As EventArgs) Handles imgbtnCancelaFiltro.Click
        pnlFiltros.Visible = False
        txtSearch_Folio.Text = ""
        txtSearch_Fecha.Text = ""
        txtSearch_AlmO.Text = ""
        txtSearch_AlmD.Text = ""
        LoadLista()
        LoadListaAddFlete()
    End Sub


    Private Sub BarEventos1_MsgEvent(sAcción As String) Handles BarEventos1.MsgEvent
        Select Case sAcción
            Case "Nuevo"
                SetFormEdit(sAcción, GridView1)
                'pnlFlete.Visible = Not pnlFlete.Visible
            Case "Eliminar"
                SetFormEdit(sAcción, GridView1)
            Case "Editar"
                SetFormEdit(sAcción, GridView1)
            Case "Filtrar"
                pnlFiltros.Visible = True
            Case "Exportar"
                'ExportClosedXML("Inventarios")
                SetFormEdit(sAcción, GridView1)
            Case "Listar"
                SetFormEdit(sAcción, GridView1)

            Case "Especial2"
                SetFormEdit(sAcción, GridView1)

            Case "Especial3"
                SetFormEdit(sAcción, GridView1)

            Case "Especial6"
                pnlAddSalFlete.Visible = True
                pnlListar.Visible = False
                pnlGridSalidasFlete.Visible = False
                pnltxtAgregarS.Visible = True
                pnltxtSAgregadas.Visible = False
                'pnlimgAdd.Visible = True
                pnlBotones.Visible = True
                pnlEventos.Visible = False
                pnlEventos2.Visible = False
                'pnlEvSal.Visible = True
                LoadListaAddFlete()
                PnlListar2.Visible = True
            Case Else
        End Select

    End Sub


    Private Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        sender.PageIndex = e.NewPageIndex
        'GridView1.SelectedRow = Nothing
        LoadLista()
    End Sub

    Private Sub GridView2_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView2.PageIndexChanging
        sender.PageIndex = e.NewPageIndex
        LoadListaAddFlete()
    End Sub

    Private Sub GridView1_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles GridView1.SelectedIndexChanging
        Dim ikey As Integer = GridView1.DataKeys(e.NewSelectedIndex).Value()
        If ikey > 0 Then
            iKeySalida = ikey
            lblkeysal.Text = ikey
        End If
    End Sub
    Private Sub GridView2_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles GridView2.SelectedIndexChanging
        Dim ikey As Integer = GridView2.DataKeys(e.NewSelectedIndex).Value()
        If ikey > 0 Then
            iKeySalida = ikey
            lblkeysal.Text = ikey
        End If
    End Sub

    'Funciones que Recuperan un Valor
    'Private Function NomUsuario() As String
    '    Dim oSql As New SQLConteos(oUsr)
    '    Dim oCs As New ColeccionPrmSql
    '    NomUsuario = ""
    '    Try
    '        oCs.Create("@ConteoID", iKey)
    '        oCs.Create("_VALOR", "Exist")
    '        Return oSql._Valor(oSql.ExistDetalle, oCs)
    '    Catch ex As Exception
    '        Tools.AddErrorLog(oUsr.Mis.Log, ex)
    '    End Try
    'End Function

    Private Sub SetFormEdit(ByVal sAcc As String, ByVal oGrid As GridView)
        Dim oSql As New SQLSalidaSemilla(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            Select Case sAcc
                Case "Nuevo"
                    'Se podria configurar botones de aceptar y cancelar
                    pnlEventos.Visible = False
                    pnlFiltros.Visible = False
                    pnlAdd.Visible = True
                    pnlListar.Visible = False

                    RFV_FECHA.Enabled = True
                    RFV_EMPRESAO.Enabled = True
                    RFV_ALMACENO.Enabled = True
                    RFV_ORIGEN.Enabled = True
                    RFV_EMPRESAD.Enabled = True
                    RFV_ALMACEND.Enabled = True
                    RFV_ELABORO.Enabled = True
                    RFV_ENCARGADO.Enabled = True

                    txt_Elaboro.Text = oUsr.FirstName

                Case "Editar"
                    If oGrid.SelectedRow IsNot Nothing Then
                        Dim ikey As Integer = Val(lblkeysal.Text)
                        If Val(lblkeysal.Text) > 0 Then
                            If ExisteFlete(ikey) Then
                                pnlEventos.Visible = False
                                pnlFiltros.Visible = False
                                pnlAdd.Visible = True
                                pnlListar.Visible = False

                                RFV_FECHA.Enabled = True
                                RFV_EMPRESAO.Enabled = True
                                RFV_ALMACENO.Enabled = True
                                RFV_ORIGEN.Enabled = True
                                RFV_EMPRESAD.Enabled = True
                                RFV_ALMACEND.Enabled = True
                                RFV_ELABORO.Enabled = True
                                RFV_ENCARGADO.Enabled = True

                                'Dim iKey As Integer = oGrid.DataKeys(oGrid.SelectedRow.DataItemIndex).Value                 
                                oCs.Create("@SalidaSemillaIDF", ikey)
                                Dim oTb As DataTable = oSql._Item(oSql.Item, "SALIDASEMILLA", oCs)
                                Dim fecha As String = ""
                                If Not oTb Is Nothing Then
                                    For Each Dr As DataRow In oTb.Rows
                                        lblAcción.Text = sAcc
                                        lbl_SalidaSem.Text = ikey
                                        fecha = Dr("SalidaSemillaFecha").ToString
                                        txt_Fecha.Text = CDate(fecha)
                                        GetIndex(DDL_EMPRESA_ORG, Dr("EmpresaID").ToString)
                                        LoadComboAlmacen(DDL_EMPRESA_ORG.SelectedValue, "O")
                                        GetIndex(DDL_ALM_ORG, Dr("UbicacionID").ToString)
                                        txt_Origen.Text = Dr("SalidaSemillaOrigen").ToString
                                        GetIndex(DDL_EMPRESA_DEST, Dr("EmpresaDestinoID").ToString)
                                        LoadComboAlmacen(DDL_EMPRESA_DEST.SelectedValue, "D")
                                        GetIndex(DDL_ALMACEN_DES, Dr("UbicacionDestinoID").ToString)
                                        txt_Elaboro.Text = Dr("SalidaSemillaElaboro").ToString
                                        txt_Encargado.Text = Dr("SalidaSemillaEncargadoSiembra").ToString
                                        Exit For
                                    Next
                                End If
                            End If
                        End If
                    End If

                Case "Eliminar"
                    'If oGrid.SelectedRow IsNot Nothing Then
                    '    Dim iKey As Integer = oGrid.DataKeys(oGrid.SelectedRow.DataItemIndex).Value
                    '    DeleteFisico(iKey)
                    'End If
                    If Val(lblkeysal.Text) > 0 Then
                        Dim flete As String = ExisteFlete(Val(lblkeysal.Text))
                        Dim Productos As String = ExistenProductos(Val(lblkeysal.Text))
                        If flete = "N" And Productos = "N" Then     ' SI no Existe Flete y no Tiene Producto Se elimina correctamente el registro de salida
                            DeleteFisico(Val(lblkeysal.Text))
                            LoadLista()
                        Else
                            Select Case flete
                                Case "S"
                                    Response.Write("<script>window.alert('Erro, No se Puede Eliminar, Ya Existe Flete Generado');</script>")
                            End Select

                            Select Case Productos
                                Case "S"
                                    Response.Write("<script>window.alert('Erro, No se Puede Eliminar, Contiene Productos dentro');</script>")
                            End Select

                        End If


                    End If

                Case "Especial2"

                    If Val(lblkeysal.Text) > 0 Then
                        If UsuarioResponsable(5) = oUsr.keyusu And ExisteFlete(Val(lblkeysal.Text)) = "S" Then
                            OpenDevolucion(Val(lblkeysal.Text))
                        Else
                            Select Case ExisteFlete(Val(lblkeysal.Text))
                                Case "N"
                                    Response.Write("<script>window.alert('Error, Aun no Existe Flete');</script>")
                            End Select

                            Select Case UsuarioResponsable(5)
                                Case <> oUsr.keyusu
                                    Response.Write("<script>window.alert('Error, No Tiene Privilegios para Recibir Flete');</script>")
                            End Select
                        End If
                    End If

                Case "Especial3"
                    'If oGrid.SelectedRow IsNot Nothing Then
                    '    Dim iKey As Integer = oGrid.DataKeys(oGrid.SelectedRow.DataItemIndex).Value
                    '    Dim sKey As String = oGrid.DataKeys(oGrid.SelectedRow.DataItemIndex).Value
                    '    Dim ikey2 As Integer = GridView1.DataKeys(GridView1.SelectedRow.DataItemIndex).Value
                    '    OpenDetalle(iKey)
                    'End If

                    If Val(lblkeysal.Text) > 0 Then
                        OpenDetalle(Val(lblkeysal.Text))
                    End If

                Case "Listar"
                    'If oGrid.SelectedRow IsNot Nothing Then
                    '    Dim iKey As Integer = oGrid.DataKeys(oGrid.SelectedRow.DataItemIndex).Value
                    '    'SendReporteSalidaSem(iKey)
                    '    EnvioReporte(iKey)
                    'End If

                    If Val(lblkeysal.Text) > 0 Then
                        EnvioReporte(Val(lblkeysal.Text))
                    End If

                Case "Exportar"
                    'If oGrid.SelectedRow IsNot Nothing Then
                    '    Dim iKey As Integer = oGrid.DataKeys(oGrid.SelectedRow.DataItemIndex).Value
                    '    SendReporteSalidaSem(iKey)
                    '    'EnvioReporte(iKey)
                    'End If

                    If Val(lblkeysal.Text) > 0 Then
                        SendReporteSalidaSem(Val(lblkeysal.Text))
                        'EnvioReporte(iKey)
                    End If

                Case "Especial6"

                Case Else

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
                .Response = "DetalleSalidaSem"
                .ikey = ikeycont
                .Enviado = True
            End With
            Session.Add("INFORPT", oRp)

            Dim url As String = Request.ApplicationPath + "PAgricola/SalSemillaDetalle.aspx"
            Dim url2 As String = Page.Request.Url.AbsoluteUri
            'Dim sUrl2 As String = url
            Dim sUrl2 As String = "SalSemillaDetalle.aspx"
            Dim sScript As String = "<script language =javascript> "
            sScript += "window.open('" & sUrl2 & "','SalidaDetalle');"
            sScript += "</script> "
            Response.Write(sScript)

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub



    Private Sub OpenDevolucion(ByVal ikeycont As Integer)
        Dim Dsr As New DataSet
        Dim oCs As New ColeccionPrmSql
        Dim oRp As New InfoRp
        Try
            Session.Remove("INFORPTDEV")
            With oRp
                .Response = "SalidaDevolucion"
                .ikey = ikeycont
                .Enviado = True
            End With
            Session.Add("INFORPTDEV", oRp)

            Dim url As String = Request.ApplicationPath + "PAgricola/SalidaDevolucion.aspx"
            Dim url2 As String = Page.Request.Url.AbsoluteUri
            'Dim sUrl2 As String = url
            Dim sUrl2 As String = "SalidaDevolucion.aspx"
            Dim sScript As String = "<script language =javascript> "
            sScript += "window.open('" & sUrl2 & "','SalidaDevolucion');"
            sScript += "</script> "
            Response.Write(sScript)

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub



    Private Function DeleteFisico(ByRef iKey As Integer) As Boolean
        Dim oSql As New SQLSalidaSemilla(oUsr)
        Dim oCs As New ColeccionPrmSql
        DeleteFisico = False
        Try
            oCs.Create("@SalidaSemillaIDF", iKey)
            Return oSql.ExecuteQry(oSql.Delete, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Function

    Protected Sub ImgBtnAceptar_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnAceptar.Click
        If Save(lblAcción.Text) Then
            LoadLista()
            SetFormConfig()
        End If
    End Sub

    Protected Sub ImgBtnCancelar_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnCancelar.Click
        SetFormConfig()
    End Sub


    Private Function Save(ByVal sAcc As String) As Boolean
        Dim oSql As New SQLSalidaSemilla(oUsr)
        Dim oCs As New ColeccionPrmSql
        Save = False
        Try
            oCs.Create("@SalidaSemillaIDF", lbl_SalidaSem.Text)
            Dim oTb As DataTable = oSql._Item(oSql.Item, "SALIDASEMILLA", oCs)
            If Not oTb Is Nothing Then
                oTb.Columns("SalidaSemillaIDF").Unique = True
                'oTb.Columns("SalidaSemillaIDF").AutoIncrement = True
                If oTb.Rows.Count = 0 Then
                    Dim FOLIO As Integer = oSql._Value(oSql.NextID, "Folio", oCs)
                    Dim Dr As DataRow = oTb.NewRow
                    Dr("SalidaSemillaIDF") = FOLIO
                    Dr("EmpresaID") = DDL_EMPRESA_ORG.SelectedValue
                    Dr("SalidaSemillaFecha") = txt_Fecha.Text
                    Dr("SalidaSemillaFechaRegistro") = CDate(DateTime.Now.ToString())
                    Dr("SalidaSemillaOrigen") = txt_Origen.Text
                    Dr("UbicacionID") = DDL_ALM_ORG.SelectedValue
                    Dr("SalidaSemillaElaboro") = txt_Elaboro.Text
                    Dr("SalidaSemillaEncargadoSiembra") = txt_Encargado.Text
                    Dr("EmpresaDestinoID") = DDL_EMPRESA_DEST.SelectedValue
                    Dr("UbicacionDestinoID") = DDL_ALMACEN_DES.SelectedValue
                    Dr("SalidaSemillaStatusProceso") = "A"
                    oTb.Rows.Add(Dr)
                    If oSql.StatemenInsert(oTb) Then
                        If UpdateFolio(FOLIO) Then
                            Return True
                        End If
                    End If
                    'Return oSql.StatemenInsert(oTb)
                Else
                    ' Edita
                    Dim Dr As DataRow = oTb.Rows(0)
                    Dr("EmpresaID") = DDL_EMPRESA_ORG.SelectedValue
                    Dr("SalidaSemillaFecha") = txt_Fecha.Text
                    Dr("SalidaSemillaFechaRegistro") = CDate(DateTime.Now.ToString())
                    Dr("SalidaSemillaOrigen") = txt_Origen.Text
                    Dr("UbicacionID") = DDL_ALM_ORG.SelectedValue
                    Dr("SalidaSemillaElaboro") = txt_Elaboro.Text
                    Dr("SalidaSemillaTransportista") = ""
                    Dr("SalidaSemillaEncargadoSiembra") = txt_Encargado.Text
                    Dr("EmpresaDestinoID") = DDL_EMPRESA_DEST.SelectedValue
                    Dr("UbicacionDestinoID") = DDL_ALMACEN_DES.SelectedValue
                    Return oSql.StatemenUpdate(oTb)
                End If
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Function

    Private Function UpdateFolio(ByVal Valor As Integer) As Boolean
        Dim oSql As New SQLSalidaSemilla(oUsr)
        Dim oCs As New ColeccionPrmSql
        UpdateFolio = False
        Try
            oCs.Create("@FolioID", 6)
            oCs.Create("@FolioValor", Valor)
            Return oSql.ExecuteQry(oSql.Update, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Function


    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim keysal As String = DataBinder.Eval(e.Row.DataItem, "SalidaSemillaIDF")
            'Dim check As CheckBox = DirectCast(e.Row.FindControl("chkMark"), CheckBox)
            Dim imagen1 As System.Web.UI.WebControls.Image = DirectCast(e.Row.FindControl("GrdimgFlete"), System.Web.UI.WebControls.Image)
            Dim statusfle As String = StatusFlete(keysal)
            Dim StatusSal As String = StatusSalida(keysal)

            'If statusfle = "P" Then
            '    imagen1.ImageUrl = "~/Img/warning.png"
            'ElseIf statusfle = "T" Then
            '    imagen1.ImageUrl = "~/Img/Transporte.gif"
            'ElseIf statusfle = "R" Then
            '    imagen1.ImageUrl = "~/Img/recibido.jpeg"
            'End If

            If StatusSal = "A" Then
                imagen1.ImageUrl = "~/Img/warning.png"
            ElseIf StatusSal = "E" Then
                imagen1.ImageUrl = "~/Img/Transporte.gif"
            ElseIf StatusSal = "R" Or StatusSal = "D" Then
                imagen1.ImageUrl = "~/Img/recibido.jpeg"
            End If

        End If
    End Sub

    Protected Sub GridView2_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView2.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim keysal As String = DataBinder.Eval(e.Row.DataItem, "SalidaSemillaIDF")
            Dim check As CheckBox = DirectCast(e.Row.FindControl("chkMark"), CheckBox)
            Dim imagen1 As System.Web.UI.WebControls.Image = DirectCast(e.Row.FindControl("GrdimgFlete"), System.Web.UI.WebControls.Image)
            Dim statusfle As String = StatusFlete(keysal)

            If statusfle = "P" Then
                imagen1.ImageUrl = "~/Img/warning.png"
                check.Enabled = True
            ElseIf statusfle = "T" Then
                imagen1.ImageUrl = "~/Img/Transporte.gif"
                check.Enabled = False
            ElseIf statusfle = "R" Then
                imagen1.ImageUrl = "~/Img/recibido.jpeg"
                check.Enabled = False
            End If

        End If
    End Sub


    Private Function StatusFlete(ByRef keysal As String) As String
        Dim oSql As New SQLSalidaSemilla(oUsr)
        Dim oCs As New ColeccionPrmSql
        StatusFlete = ""
        Try
            oCs.Create("@SalidaSemillaIDF", keysal)
            oCs.Create("_VALOR", "sal_status")
            Return oSql._Valor(oSql.ItemStatus, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Private Function StatusSalida(ByRef keysal As String) As String
        Dim oSql As New SQLSalidaSemilla(oUsr)
        Dim oCs As New ColeccionPrmSql
        StatusSalida = ""
        Try
            oCs.Create("@SalidaSemillaIDF", keysal)
            oCs.Create("_VALOR", "SalidaSemillaStatusProceso")
            Return oSql._Valor(oSql.ItemStatus, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Private Sub SendReporteSalidaSem(ByRef SalidaSemillaIDF As Integer)
        Dim oSql As New SQLSalidaSemilla(oUsr)
        Dim Dsr As New DataSet
        Dim oCs As New ColeccionPrmSql
        Dim oRp As New InfoRp
        Try
            Session.Remove("INFORPT")
            oCs.Create("@SalidaSemillaIDF", SalidaSemillaIDF)
            If oSql.GetQry(Ds, "VW_SALIDA_SEMILLA", oSql.ListReport, oCs) Then
                With oRp
                    .Reporte = "SALIDASEMILLA"
                    .Nombre = "Salida de Semilla"
                    '.Nombre = ""
                    .OD = Ds
                    .Enviado = True
                End With
                Session.Add("INFORPT", oRp)
                'Response.Write("<script>window.open('ShowInvProd.aspx','_blank');</script>")
                'Response.Redirect("ShowInvProd.aspx")

                'Dim sUrl2 As String = "ShowReport.aspx"
                'Dim sScript As String = "<script language =javascript> "
                'sScript += "window.open('" & sUrl2 & "',null,'toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1,width=500,height=300,left=100,top=100');"
                'sScript += "</script> "
                'Response.Write(sScript)


                Dim url As String = Request.ApplicationPath + "PAgricola/ShowReport.aspx"
                Dim url2 As String = Page.Request.Url.AbsoluteUri
                Dim sUrl2 As String = url
                Dim sScript As String = "<script language =javascript> "
                sScript += "window.open('" & sUrl2 & "','_blank');"
                sScript += "</script> "
                Response.Write(sScript)

            End If

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub


    Private Sub EnvioReporte(ByRef SalidaSemillaIDF As Integer)
        Dim oSql As New SQLSalidaSemilla(oUsr)
        Dim Dsr As New DataSet
        Dim oCs As New ColeccionPrmSql
        Dim oRp As New InfoRp
        Try
            Session.Remove("INFORP")
            oCs.Create("@SalidaSemillaIDF", SalidaSemillaIDF)
            If oSql.GetQry(Ds, "VW_SALIDA_SEMILLA", oSql.ListReport, oCs) Then

                With oRp
                    .Reporte = "SALIDASEMILLA"
                    .Nombre = "Salida de Semilla " & SalidaSemillaIDF & ".PDF"
                    .OD = Ds
                    .Enviado = True
                End With

                Session.Add("INFORP", oRp)
                'Nueva Salida de semilla Ajo
                'Response.Redirect("SalidaSemilla.aspx", True)

                Dim url As String = Request.ApplicationPath + "PAgricola/Show.aspx"
                Dim url2 As String = Page.Request.Url.AbsoluteUri
                'Dim sUrl2 As String = url
                Dim sUrl2 As String = "Show.aspx"
                Dim sScript As String = "<script language =javascript> "
                sScript += "window.open('" & sUrl2 & "','_blank');"
                sScript += "</script> "
                Response.Write(sScript)
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub





    Protected Sub DDL_EMPRESA_ORG_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDL_EMPRESA_ORG.SelectedIndexChanged
        Dim oSqlE As New SQLAlmacenAgro(oUsr)
        Dim lCsE As New ColeccionPrmSql
        lCsE.Create("@EmpresaID", DDL_EMPRESA_ORG.SelectedValue)
        lCsE.Create("_Tabla", "EMPRESAS")
        lCsE.Create("_Qry", oSqlE.List_Combo)
        lCsE.Create("_Order", "UbicacionNombre")
        'lCsE.Create("_Filtro", "ubi_keytub = 'I'")
        lCsE.Create("_DefaultKey", "%")
        lCsE.Create("_DefaultDes", "[SELECCIONAR]")
        LoadCombo(oUsr, DDL_ALM_ORG, lCsE)
        DDL_ALM_ORG.SelectedIndex = -1
    End Sub

    Protected Sub DDL_EMPRESA_DEST_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDL_EMPRESA_DEST.SelectedIndexChanged
        Dim oSqlE As New SQLAlmacenAgro(oUsr)
        Dim lCsE As New ColeccionPrmSql
        lCsE.Create("@EmpresaID", DDL_EMPRESA_DEST.SelectedValue)
        lCsE.Create("_Tabla", "EMPRESAS")
        lCsE.Create("_Qry", oSqlE.List_Combo)
        lCsE.Create("_Order", "UbicacionNombre")
        'lCsE.Create("_Filtro", "ubi_keytub = 'I'")
        lCsE.Create("_DefaultKey", "%")
        lCsE.Create("_DefaultDes", "[SELECCIONAR]")
        LoadCombo(oUsr, DDL_ALMACEN_DES, lCsE)
        DDL_ALMACEN_DES.SelectedIndex = -1
    End Sub


    Protected Sub btnCancelarF_Click(sender As Object, e As ImageClickEventArgs) Handles btnCancelarF.Click

        'pnlAddSalFlete.Visible = False
        'pnlListar.Visible = True
        'pnlGridSalidasFlete.Visible = False
        'pnlBotones.Visible = True
        'pnlEventos.Visible = True

        SetFormConfig()
        Session.Remove("dts")   ' Limpia la sesion que guarda los registros seleccionados y almacenados en el grid de confirmación
        Session.Remove("dtcheck")    ' Limpia la sesion que Guarda el Datatable de los registros que tienen check en true   
        LimpiarGridSalidas()    ' Limpia el grid de las salidas donde estan los check seleccionados 
        LimpiarGridSalidaFlete()    ' Limpia El grid donde estan las salidas agregadas al resumen del flete.
        'LoadListaAddFlete()
    End Sub



    'GridView3 Resumen de lo seleccionado 


    'Obtiene los regitros seleccionado del gird de salidas con la finalidad de agregarlos a otro gridview

    Public Function filldatasal() As DataTable
        Dim dts As New DataTable()
        dts.Columns.Add("SalidaSemillaIDF", GetType(Integer))
        dts.Columns.Add("SalidaSemillaFecha", GetType(Date))
        dts.Columns.Add("Alm_Org", GetType(String))
        dts.Columns.Add("Alm_Des", GetType(String))
        dts.Columns.Add("SalidaSemillaLotes", GetType(String))
        dts.Columns.Add("SalidaSemillaElaboro", GetType(String))
        dts.Columns.Add("SalidaSemillaEncargadoSiembra", GetType(String))
        Return dts
    End Function


    Public Sub AgregarRegistroGridview(ByRef SalidaSemillaIDF As Integer,
            ByRef SalidaSemillaFecha As Date,
            ByRef Alm_Org As String,
            ByRef Alm_Des As String,
            ByRef SalidaSemillaOrigen As String,
            ByRef SalidaSemillaElaboro As String,
            ByRef SalidaSemillaEncargadoSiembra As String)
        Try
            If Session("dts") Is Nothing Then
                Dim dt As DataTable = filldatasal()
                Dim Row1 As DataRow
                Row1 = dt.NewRow()
                Row1("SalidaSemillaIDF") = SalidaSemillaIDF
                Row1("SalidaSemillaFecha") = SalidaSemillaFecha
                Row1("Alm_Org") = Alm_Org
                Row1("Alm_Des") = Alm_Des
                Row1("SalidaSemillaLotes") = SalidaSemillaOrigen
                Row1("SalidaSemillaElaboro") = SalidaSemillaElaboro
                Row1("SalidaSemillaEncargadoSiembra") = SalidaSemillaEncargadoSiembra
                dt.Rows.Add(Row1)
                GridView3.DataSource = dt
                GridView3.DataBind()
                Session("dts") = dt

            Else
                Dim dt As DataTable = TryCast(Session("dts"), DataTable)
                Dim Row1 As DataRow
                Row1 = dt.NewRow()
                Row1("SalidaSemillaIDF") = SalidaSemillaIDF
                Row1("SalidaSemillaFecha") = SalidaSemillaFecha
                Row1("Alm_Org") = Alm_Org
                Row1("Alm_Des") = Alm_Des
                Row1("SalidaSemillaLotes") = SalidaSemillaOrigen
                Row1("SalidaSemillaElaboro") = SalidaSemillaElaboro
                Row1("SalidaSemillaEncargadoSiembra") = SalidaSemillaEncargadoSiembra
                dt.Rows.Add(Row1)
                GridView3.DataSource = dt
                GridView3.DataBind()
                Session("dts") = dt
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub



    ' Borrar Registro En memoria del GridView de confirmacion 

    Public Sub deleteRegistroGridview(ByRef keyrow As Integer)
        Dim oSQL As New SQL(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            If Session("dts") Is Nothing Then

            Else
                Dim dt As DataTable = TryCast(Session("dts"), DataTable)
                dt.Rows.RemoveAt(keyrow)
                GridView3.DataSource = dt
                GridView3.DataBind()
                Session("dts") = dt
            End If

            If GridView3.Rows.Count > 0 Then
            Else
            End If

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub


    Private Sub GridView3_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView3.RowDeleting
        Dim oSQL As New SQL(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            Dim iKey As Integer = sender.DataKeys(e.RowIndex).Value()
            Dim keyrow As Integer = e.RowIndex
            If Session("dts") Is Nothing Then

            Else
                Dim dt As DataTable = TryCast(Session("dts"), DataTable)
                dt.Rows.RemoveAt(keyrow)
                GridView3.DataSource = dt
                GridView3.DataBind()
                Session("dts") = dt
            End If

            If GridView3.Rows.Count = 0 Then
                pnlFlete.Visible = False
                Session.Remove("dts")   ' Limpia la sesion que guarda los registros seleccionados y almacenados en el grid de confirmación
                Session.Remove("dtcheck")    ' Limpia la sesion que Guarda el Datatable de los registros que tienen check en true   
                LimpiarGridSalidas()    ' Limpia el grid de las salidas donde estan los check seleccionados 
                LimpiarGridSalidaFlete()
                SetFormConfig()
            End If

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub


    Private Sub GridView3_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView3.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim bTn As LinkButton = e.Row.FindControl("lnkBtnDelete")
            If Not bTn Is Nothing Then
                bTn.Attributes.Add("onclick", "return confirm('¿Realmente desea eliminar este registro?')")
            End If
        End If
    End Sub



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

    Private Function ExistenProductos(ByRef keysal As Integer) As String
        Dim oSql As New SQLSalidaSemilla(oUsr)
        Dim oCs As New ColeccionPrmSql
        ExistenProductos = ""
        Try
            oCs.Create("@SalidaSemillaIDF", keysal)
            oCs.Create("_VALOR", "ExistProd")
            Return oSql._Valor(oSql.ExisteProd, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function


    Private Function UsuarioResponsable(ByRef keytpf As Integer) As Integer
        Dim oSql As New SQL_TipoFlete(oUsr)
        Dim oCs As New ColeccionPrmSql
        UsuarioResponsable = 0
        Try
            oCs.Create("@keytipofle", keytpf)
            oCs.Create("_VALOR", "TipoFleteResponsable")
            Return oSql._Valor(oSql.Item, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function


    Public Sub DeleteCheckedDatatable(ByRef keyrow As Integer, ByRef checked As String)
        Dim oSQL As New SQL(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            If Session("dtcheck") Is Nothing Then

            Else
                Dim dt As DataTable = TryCast(Session("dtcheck"), DataTable)
                dt.Rows.RemoveAt(keyrow)
                Session("dtcheck") = dt
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub



    'Obtiene los regitros seleccionado del gird de salidas con la finalidad de agregarlos a otro gridview
    Private Sub AgregarSalidasCheckedIn()
        Dim oSql As New SQLSalidaSemilla(oUsr)
        Dim oCs As New ColeccionPrmSql
        Dim noSal As Integer = 0
        Dim fecsal As String = ""
        Dim Alm_Org As String = ""
        Dim Alm_Des As String = ""
        Dim Origen As String = ""
        Dim Elaboro As String = ""
        Dim Encargado As String = ""
        Try
            oCs.Create("@SalidaSemillaIDF", 0)
            'oCs.Create("@status", oUsr.Mis.Status)
            'Recuperamos el datatble de la variable de sesion en caso de que contenga datos 
            Dim dt As DataTable = TryCast(Session("dts"), DataTable)
            For Each row As GridViewRow In GridView2.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    If row.Enabled Then
                        Dim cb As CheckBox = row.FindControl("chkMark")
                        If cb IsNot Nothing AndAlso cb.Checked Then
                            Dim keysal As Integer = GridView2.DataKeys(row.RowIndex).Value
                            oCs.ItemValue("@SalidaSemillaIDF") = keysal
                            oCs.ItemValue("@status") = oUsr.Mis.Status
                            Dim oTb As DataTable = oSql._Item(oSql.ItemSalidaLista, "SALIDASEMILLA", oCs)
                            If Not oTb Is Nothing Then
                                oTb.Columns("SalidaSemillaIDF").Unique = True
                                If oTb.Rows.Count > 0 Then
                                    Dim Dr As DataRow = oTb.Rows(0)
                                    noSal = Dr("SalidaSemillaIDF").ToString
                                    fecsal = Dr("SalidaSemillaFecha").ToString
                                    Alm_Org = Dr("Alm_Org").ToString
                                    Alm_Des = Dr("Alm_Des").ToString
                                    Origen = Dr("SalidaSemillaLotes").ToString
                                    Elaboro = Dr("SalidaSemillaElaboro").ToString
                                    Encargado = Dr("SalidaSemillaEncargadoSiembra").ToString
                                    'oTb.Rows.Add(Dr)
                                End If
                            End If

                            If dt IsNot Nothing Then
                                'Revisamos si ya existe el usuario
                                Dim DR() As DataRow
                                DR = dt.Select("SalidaSemillaIDF = " & keysal)
                                If DR.Length > 0 Then
                                    'Ya existe el registro
                                Else
                                    If ExisteFlete(keysal) = "N" Then
                                        If ExistenProductos(keysal) = "S" Then
                                            AgregarRegistroGridview(noSal, CDate(fecsal), Alm_Org, Alm_Des, Origen, Elaboro, Encargado)
                                        End If
                                    End If
                                End If
                            Else
                                If ExisteFlete(keysal) = "N" Then
                                    If ExistenProductos(keysal) = "S" Then
                                        AgregarRegistroGridview(noSal, CDate(fecsal), Alm_Org, Alm_Des, Origen, Elaboro, Encargado)
                                    End If
                                End If
                            End If
                        Else
                            ' Eliminar Registro 
                            Dim keysal As Integer = GridView2.DataKeys(row.RowIndex).Value
                            'Dim dt As DataTable = TryCast(Session("dtcheck"), DataTable)
                            If dt IsNot Nothing Then
                                'Revisamos si ya existe el registro
                                Dim DR() As DataRow
                                DR = dt.Select("SalidaSemillaIDF = " & keysal)
                                If DR.Length > 0 Then
                                    'si existe el numero de salida en el que va el recorrido                              
                                    Dim posicion As Integer = 0
                                    For Each item As DataRow In dt.Rows
                                        Dim valor As Integer = item("SalidaSemillaIDF").ToString
                                        If valor = keysal Then
                                            deleteRegistroGridview(posicion)
                                            Exit For
                                        End If
                                        posicion += 1
                                    Next
                                Else
                                    ' No hacer nada
                                End If
                            End If
                        End If
                    End If
                End If
            Next


        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub


    'Obtener Datos del FLete en el control Flete

    Private Sub Flete1_MsgWork(sAcción As String, sMensaje As String, ByRef dt As DataTable, Cs As ColeccionPrmSql) Handles Flete1.MsgWork
        Select Case sAcción
            Case "Guardar"
                If Session("Cs") Is Nothing Then
                    Session.Add("Cs", Cs)
                Else
                    Session("Cs") = Cs
                End If
                If saveFlete(dt, Cs) Then
                    'Terminar el flete
                    SaveMovAlmacenSem(Val(lblkeyfle.Text))  'Ejecuta el Movimiento de salida de Inventario
                    CallReporte(txtResumen.Text)
                    LoadLista()
                    pnlFlete.Visible = Not pnlFlete.Visible
                    SetFormConfig()
                    Session.Remove("dts")   ' Limpia la sesion que guarda los registros seleccionados y almacenados en el grid de confirmación
                    Session.Remove("Csc")   ' Limpia la sesion que que trae los datos del flete Guardado, para guardar sus detalles
                    Session.Remove("dtcheck")    ' Limpia la sesion que Guarda el Datatable de los registros que tienen check en true
                    LimpiarGridSalidaFlete()
                    Response.Write("<script>window.alert('Flete Generado Con exito.');</script>")
                End If


            Case "Cancelar"
                'pnlFlete.Visible = Not pnlFlete.Visible
                If sMensaje = "" Then
                    pnlFlete.Visible = False
                    SetFormConfig()
                    Session.Remove("dts")   ' Limpia la sesion que guarda los registros seleccionados y almacenados en el grid de confirmación
                    Session.Remove("dtcheck")    ' Limpia la sesion que Guarda el Datatable de los registros que tienen check en true
                    LimpiarGridSalidaFlete()
                    pnlFiltros.Visible = False
                    LimpiarGridSalidas()
                    LoadLista()
                End If

        End Select
    End Sub


    Private Function saveFlete(dt As DataTable, ByVal oCs As ColeccionPrmSql) As Boolean
        Dim oSql As New SQLFletes(oUsr)
        Dim oSqlD As New SQLFletesSemilla(oUsr)
        Dim keyfle As Integer = 0
        Dim fechas As String = ""
        Dim fechareg As String = ""
        Dim keyrut As Integer = 0
        Dim keytpf As Integer = 0
        Dim keytra As Integer = 0
        Dim keycam As Integer = 0
        Dim keyope As Integer = 0
        Dim keyusu As Integer = 0
        Dim observ As String = ""
        saveFlete = False
        Try
            If Not Session("Csc") Is Nothing Then
                oCs = Session("Csc")
            Else
                Session.Add("Csc", oCs)
            End If
            Dim oTabla As DataTable = dt
            If Not oTabla Is Nothing Then
                If oTabla.Rows.Count > 0 Then
                    Dim Dr As DataRow = oTabla.Rows(0)
                    fechas = Dr("FleteFecha").ToString
                    fechareg = Dr("FleteFechaRegistro").ToString
                    keyrut = Dr("RutaID").ToString
                    keytpf = Dr("TipoFleteID").ToString
                    keytra = Dr("TransportistaID").ToString
                    keycam = Dr("CamionID").ToString
                    keyope = Dr("OperadorID").ToString
                    keyusu = Dr("FleteUsuario").ToString
                    observ = Dr("FleteObservacion").ToString

                    If oTabla.Rows.Count > 0 Then
                        oCs.Clear()
                        oCs.Create("@FleteFecha", CDate(Dr("FleteFecha").ToString))
                        oCs.Create("@fechReg", CDate(DateTime.Now.ToString()))
                        oCs.Create("@keyrut", Dr("RutaID").ToString)
                        oCs.Create("@keytpf", Dr("TipoFleteID").ToString)
                        oCs.Create("@keytra", Dr("TransportistaID").ToString)
                        oCs.Create("@keycam", Dr("CamionID").ToString)
                        oCs.Create("@keyope", Dr("OperadorID").ToString)
                        oCs.Create("@keyusu", oUsr.keyusu)
                        oCs.Create("@observ", Dr("FleteObservacion").ToString)
                        oCs.Create("@keyfle", 0, "out")

                        If keyfle = 0 Then
                            If oSql.ExecuteStore("SP_INS_FLETE", oCs) Then
                                keyfle = oCs.ItemValue("@keyfle")
                                lblkeyfle.Text = keyfle
                                Resumen(keyfle, fechas.Split(" ")(0), "Empaque Aguilares", keytpf, DesRuta(keyrut), CostoRuta(keyrut, keytra, keycam), DesTransportista(keytra), DesCamion(keycam), DesOperador(keyope, keytra), ResponsableTpf(keytpf), observ)
                                oCs.Create("@SalidaSemillaIDF", 0)
                                ' Inserta Detalles
                                For Each row As GridViewRow In GridView3.Rows
                                    If row.RowType = DataControlRowType.DataRow Then
                                        Dim keysal As Integer = GridView3.DataKeys(row.RowIndex).Value
                                        oCs.ItemValue("@SalidaSemillaIDF") = keysal
                                        'oCs.ItemValue("@keyfle") = keyfle
                                        Dim oTb As DataTable = oSqlD._Item(oSqlD.Item, "FLETESEMILLA", oCs)
                                        If Not oTb Is Nothing Then
                                            oTb.Columns("FleteID").Unique = True
                                            oTb.Columns("SalidaSemillaIDF").Unique = True
                                            If oTb.Rows.Count = 0 Then
                                                Dim Drs As DataRow = oTb.NewRow
                                                Drs("FleteID") = keyfle
                                                Drs("SalidaSemillaIDF") = keysal
                                                Drs("SalidaSemillaObs") = ""
                                                If ExisteFlete(keysal) = "N" Then
                                                    oTb.Rows.Add(Drs)
                                                    If oSql.StatemenInsert(oTb) Then
                                                        ' Hacer Update del Status de la salida a "E" Enviado 
                                                        If UpdateStatusSal(keysal, "E") Then
                                                            Dim bander As Integer = 0
                                                            bander = bander + 1
                                                        End If
                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If
                                Next
                                Return True
                            End If

                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function


    Private Function DesRuta(ByRef keyrut As Integer) As String
        Dim oSql As New SQLRutas(oUsr)
        Dim oCs As New ColeccionPrmSql
        DesRuta = ""
        Try
            oCs.Create("@keyrut", keyrut)
            oCs.Create("_VALOR", "rut_desrut")
            Return oSql._Valor(oSql.ItemRuta, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Private Function CostoRuta(ByRef keyrut As Integer, ByRef keytra As Integer, ByRef keycam As Integer) As Double
        Dim oSql As New SQLRutas_Costos(oUsr)
        Dim oCs As New ColeccionPrmSql
        CostoRuta = 0
        Try
            oCs.Create("@keyrut", keyrut)
            oCs.Create("@keytra", keytra)
            oCs.Create("@keycam", keycam)
            oCs.Create("_VALOR", "RutaCosto")
            Return oSql._Valor(oSql.ItemCostos, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Private Function DesTipoFlete(ByRef keytpf As Integer) As String
        Dim oSql As New SQL_TipoFlete(oUsr)
        Dim oCs As New ColeccionPrmSql
        DesTipoFlete = ""
        Try
            oCs.Create("@keytipofle", keytpf)
            oCs.Create("_VALOR", "TipoFleteNombre")
            Return oSql._Valor(oSql.Item, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function


    Private Function ResponsableTpf(ByRef keytpf As Integer) As String
        Dim oSql As New SQL_TipoFlete(oUsr)
        Dim oCs As New ColeccionPrmSql
        ResponsableTpf = ""
        Try
            oCs.Create("@keytpf", keytpf)
            oCs.Create("_VALOR", "usu_nombre")
            Return oSql._Valor(oSql.ItemTipoFlete, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function


    Private Function DesTransportista(ByRef keytra As Integer) As String
        Dim oSql As New SQL_Transportista(oUsr)
        Dim oCs As New ColeccionPrmSql
        DesTransportista = ""
        Try
            oCs.Create("@keytran", keytra)
            oCs.Create("_VALOR", "TransportistaNombre")
            Return oSql._Valor(oSql.Item, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function


    Private Function DesCamion(ByRef keycam As Integer) As String
        Dim oSql As New SQL_Camion(oUsr)
        Dim oCs As New ColeccionPrmSql
        DesCamion = ""
        Try
            oCs.Create("@keycam", keycam)
            oCs.Create("_VALOR", "cam_descam")
            Return oSql._Valor(oSql.ItemCamion, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function


    Private Function DesOperador(ByRef keyope As Integer, ByRef keytra As Integer) As String
        Dim oSql As New SQL_Operador(oUsr)
        Dim oCs As New ColeccionPrmSql
        DesOperador = ""
        Try
            oCs.Create("@keyope", keyope)
            oCs.Create("@keytra", keytra)
            oCs.Create("_VALOR", "ope_nombre")
            Return oSql._Valor(oSql.ItemOperador, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function


    Private Sub GridView3_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles GridView3.SelectedIndexChanging
        Dim iKeysal As Integer = GridView3.DataKeys(e.NewSelectedIndex).Value()
    End Sub


    ' Ver detalle de cada Salida 
    Private Sub BarEventos2_MsgEvent(sAcción As String) Handles BarEventos2.MsgEvent
        Select Case sAcción

            Case "Especial3"
                SetFormEdit2(sAcción, GridView3)
            Case Else
        End Select

    End Sub



    Private Sub SetFormEdit2(ByVal sAcc As String, ByVal oGrid As GridView)
        Dim oSql As New SQLSalidaSemilla(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            Select Case sAcc
                Case "Especial3"
                    If oGrid.SelectedRow IsNot Nothing Then
                        Dim iKey As Integer = oGrid.DataKeys(oGrid.SelectedRow.DataItemIndex).Value
                        OpenDetalle(iKey)
                    Else
                        Response.Write("<script>window.alert('Debe seleccionar una salida');</script>")
                    End If
                Case Else
            End Select

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub



    '-------------------------------------------------------------------------------------------------------
    '-------------- DESCARGA REPORTE -----------------------------------------------
    '-------------------------------------------------------------------------------------------------------



    Private Sub Resumen(ByRef keyfle As Integer, ByRef fechaflete As String, ByRef Proveedor As String, ByRef keytpf As Integer,
                ByRef Ruta As String, ByRef Costo As Double, ByRef Transportista As String, ByRef Camion As String,
                Operador As String, ByRef Responsable As String, ByRef obs As String)
        Dim sFlete As String = String.Empty
        Try
            sFlete = Space(10) + "GRUPO U" + Space(10) + vbCrLf
            sFlete += "Orden de Flete: " & keyfle & vbCrLf
            sFlete += "Fecha: " & CDate(fechaflete).ToString & vbCrLf
            sFlete += "Proveedor: " & Proveedor & vbCrLf
            'sFlete += "Movimiento:" & lblTipoFlete.Text & vbCrLf
            sFlete += "Movimiento: " & DesTipoFlete(keytpf) & vbCrLf
            sFlete += "Ruta:" & Ruta & vbCrLf
            sFlete += "Costo: " & Costo & vbCrLf
            sFlete += "Transporta/Camión: " & Camion & vbCrLf
            sFlete += "Operador: " & Operador & vbCrLf
            sFlete += "Responsable: " & Responsable & vbCrLf
            For Each Dgr As GridViewRow In GridView3.Rows
                If Dgr.RowType = DataControlRowType.DataRow Then
                    sFlete += "-------------------------------------------" & vbCrLf
                    sFlete += "Salida No: " & Dgr.Cells(1).Text & vbCrLf
                    sFlete += "Fecha: " & Dgr.Cells(2).Text & vbCrLf
                    sFlete += "Almacen Origen: " & Dgr.Cells(3).Text & vbCrLf
                    sFlete += "Almacen Destino: " & Dgr.Cells(4).Text & vbCrLf
                    sFlete += "Origen: " & Dgr.Cells(5).Text & vbCrLf
                    sFlete += "Elaboró: " & Dgr.Cells(6).Text & vbCrLf
                    sFlete += "****************************************" & vbCrLf
                    'sFlete += "PRODUCTOS" & " " & "Salida No." & " " & Dgr.Cells(1).Text & vbCrLf
                    LoadListaDetalleSalida(Dgr.Cells(1).Text)
                    For Each Dgr2 As GridViewRow In GridView4.Rows
                        If Dgr2.RowType = DataControlRowType.DataRow Then
                            'sFlete += "---------------------------" & vbCrLf
                            sFlete += "       PRODUCTOS       " & vbCrLf
                            'sFlete += "PRODUCTOS" & " " & "Salida No." & " " & Dgr.Cells(1).Text & vbCrLf
                            sFlete += "Producto: " & Dgr2.Cells(2).Text & vbCrLf
                            sFlete += "Cantidad: " & Dgr2.Cells(3).Text & vbCrLf
                            sFlete += "Peso: " & Dgr2.Cells(4).Text & vbCrLf
                            sFlete += "Densidad: " & Dgr2.Cells(5).Text & vbCrLf
                            sFlete += "Observaciones: " & Dgr2.Cells(6).Text & vbCrLf
                            'sFlete += ".............................." & vbCrLf                           
                        End If
                    Next
                End If
            Next
            sFlete += "Observación: " & obs & vbCrLf
            sFlete += "FIN RECIBO" & vbCrLf & vbCrLf
            Select Case keytpf
                Case 1
                    sFlete += "Fecha de emisión: 03-MAR-15" & vbCrLf
                    sFlete += "F-100-PAA-64" & vbCrLf
                    sFlete += "Rev: 02" & vbCrLf
                    sFlete += "PRODUCTO CERTIFICADO GLOBAL"
                Case 2
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

    'Genera Grid view con los detalles de cada alida de semilla 
    Private Sub LoadListaDetalleSalida(ByRef ikey As Integer)
        Dim oSql As New SQLDetalleSalidaSemilla(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            oCs.Create("@SalidaSemillaIDF", ikey)
            Dim oTabla As DataTable = oSql._List(oSql.List, "SALIDASEMILLADETALLE", oCs)
            LoadGrid(GridView4, oTabla)

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub


    'Genera Flete, ENvia y descarga el Flete.

    Private Sub CallReporte(ByVal sTexto As String)
        Dim oRp As New InfoRp
        Dim Body As String = String.Empty
        Try
            Dim dtOut As New DataTable("VW_FLETE")
            dtOut.MinimumCapacity = 100
            dtOut.CaseSensitive = False

            ' Se crean todas las columnas de las tablas.
            dtOut.Columns.Add("row", GetType(String)).MaxLength = 255
            Ds.Tables.Add(dtOut)
            Dim oLineas() As String = sTexto.Split(vbCrLf)
            For Each sLinea As String In oLineas
                Dim dr As DataRow = dtOut.NewRow
                dr("row") = sLinea.Replace("&nbsp;", "")
                dtOut.Rows.Add(dr)
            Next
            Body = "SE HA GENERADO UNA NUEVA ORDEN DE FLETE DE SEMILLA DE AJO, REVISAR ARCHIVO ADJUNTO" & vbCrLf
            Body += "" & vbCrLf
            Body += "" & vbCrLf
            Body += "Redireccionar para Aceptar las Salida Correspondientes" & vbCrLf
            Body += "http://GabWeb/PAgricola/SalidaSemilla.aspx" + "?key=" + lblkeyfle.Text & vbCrLf
            Body += "" & vbCrLf
            Body += "" & vbCrLf
            Body += "En caso de no redireccionar entrar al siguiente Link " & vbCrLf
            Body += "http://200.76.124.19:2232/PAgricola/SalidaSemilla.aspx" + "?key=" + lblkeyfle.Text & vbCrLf
            Body += ""

            With oRp
                .Reporte = "FLETE_SEMILLA"
                .Nombre = "ORDEN DE FLETE" & lblkeyfle.Text & ".PDF"
                .Descripcion = Body

                .Envia = "S"
                .Parametros.Create("_MAILFROM", "fletesajo@grupou.mx")
                Dim ArTo As List(Of String) = GetMailto("SENDMAIL-FLETES_SEMILLA", "_MAILTO")
                .Parametros.Create("_MAILFP", ArTo)
                .OD = Ds
                .Enviado = True
            End With

            Session.Add("INFORP", oRp)
            'Nuevo flete
            Response.Redirect("SalidaSemilla.aspx", True)

            'Dim url As String = Request.ApplicationPath + "PAgricola/Show.aspx"
            'Dim url2 As String = Page.Request.Url.AbsoluteUri
            'Dim sUrl2 As String = url
            'Dim sScript As String = "<script language =javascript> "
            'sScript += "window.open('" & sUrl2 & "','_blank');"
            'sScript += "</script> "
            'Response.Write(sScript)

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub

    Private Function GetMailto(ByVal sSec As String, ByVal sKey As String) As List(Of String)
        'Dim oSql As New SQLSetting(oUsr)
        Dim oSql As New SQL_Setting(oUsr)
        Dim oCs As New ColeccionPrmSql
        Dim Ar As New List(Of String)
        GetMailto = Nothing
        Try
            oCs.Create("@secion", sSec)
            oCs.Create("@sname", sKey)
            Dim oTb As DataTable = oSql._Item(oSql.Item, "MAILFP", oCs)
            If Not oTb Is Nothing Then
                If oTb.Rows.Count > 0 Then
                    For Each sItem As String In oTb.Rows(0).Item("set_svalue").ToString.Split(",")
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



    'Realizar Movimiento de Almacen 
    Private Function SaveMovAlmacenSem(ByRef keyfle As Integer) As Boolean
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
                    oCs.Create("@keyfle", keyfle)
                    oCs.Create("@ejecuteinserts", 0)    ' Solo Ejecutara Un Movimiento
                    oCs.Create("@MovimientoTipo", "S")  ' EL Primer Movimiento es de Salida
                    oCs.Create("@MovimientoTipoDes", "E")   'El Segundo Movimiento es de Entrada
                    oCs.Create("@OrigenMovID", 3)       ' Salida de semilla     
                    oCs.Create("@OrigenMovIDDes", 4)    ' Cualquier numero Alfinal no entra en la condicion
                    If oSql.ExecuteStore("SP_CUR_INSMOV", oCs) Then
                        Return True
                    End If
                End If
            End If
        Catch ex As Exception
            'Dim s As String = ex.TargetSite.Name
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function


    Private Function UpdateStatusSal(ByRef keysal As Integer, ByRef status As String) As Boolean
        Dim oSql As New SQLSalidaSemilla(oUsr)
        Dim oCs As New ColeccionPrmSql
        UpdateStatusSal = False
        Try
            oCs.Create("@SalidaSemillaIDF", keysal)
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

    Protected Sub imgbtnNext_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnNext.Click
        If GridView2.Rows.Count > 0 Then
            AgregarSalidasCheckedIn()

            If GridView3.Rows.Count > 0 Then
                pnlGridSalidasFlete.Visible = True
                PnlListar2.Visible = False
                pnlBotones.Visible = False
                pnltxtAgregarS.Visible = False
                pnltxtSAgregadas.Visible = True
                pnlEventos2.Visible = True
                pnlFlete.Visible = Not pnlFlete.Visible
            Else
                Response.Write("<script>window.alert('Para continuar, Debe seleccionar las salidas a agregar al flete ó Agregar Productos a las Salidas');</script>")
            End If
        End If
    End Sub

End Class