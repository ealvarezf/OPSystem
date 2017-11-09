Imports Security_System
Public Class SGSubFunciones
    Inherits System.Web.UI.Page
    Private Ds As New DataSet
    Private oUsr As UserLogin
    Private sKeyFunc As String
    Private Sub SGSubFunciones_Init(sender As Object, e As EventArgs) Handles Me.Init
        oUsr = Session("Usr")
        sKeyFunc = Request.Params("Id")
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If GetValidaPRIM(sKeyFunc) = "S" And GetValidaMD(sKeyFunc) = "S" Then
                SetFormConfig()
                LoadLista()
            Else
                Response.Redirect("SGFunciones.aspx")
            End If
        End If
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

        'End With
        'Valores predeterminados
        txtSearch_SGSubFuncionID.Text = ""

        txt_SGFuncionID.Text = sKeyFunc
        'txt_SGSubFuncionID.Text = sKeyFunc
        txt_SGSubFuncionOrden.Text = ""

        txt_SGFuncionID.MaxLength = 32
        'txt_SGSubFuncionID.MaxLength = 32

        'Tamaño de pagina predeterminado
        GridView1.PageSize = 10

        'DDLFUNCION
        Dim oSqlf As New SQLSubFunciones(oUsr)
        Dim lCsf As New ColeccionPrmSql

        lCsf.Create("_Tabla", "SGFUNCIONES")
        lCsf.Create("_Qry", oSqlf.ItemFuncion)
        lCsf.Create("_DefaultKey", 0)
        lCsf.Create("_DefaultDes", "[SELECCIONAR]")
        LoadCombo(oUsr, DDLSGSubFuncionID, lCsf)
        DDLSGSubFuncionID.SelectedIndex = -1
    End Sub

    Private Sub FlushNuevo()
        txt_SGFuncionID.Text = sKeyFunc
        ' txt_SGSubFuncionID.Text = sKeyFunc
        txt_SGSubFuncionOrden.Text = ""

        txt_SGFuncionID.Enabled = False
        DDLSGSubFuncionID.Enabled = True
        txt_SGSubFuncionOrden.Enabled = True
    End Sub

    Private Sub FlushEditar()
        txt_SGFuncionID.Enabled = False
        DDLSGSubFuncionID.Enabled = False
        txt_SGSubFuncionOrden.Enabled = True
    End Sub

    Private Sub LockEliminar()
        txt_SGFuncionID.Enabled = False
        DDLSGSubFuncionID.Enabled = False
        txt_SGSubFuncionOrden.Enabled = False
    End Sub

    Private Function GetValidaPRIM(ByVal PRIM As String) As String
        Dim oSql As New SQLSubFunciones(oUsr)
        Dim oCs As New ColeccionPrmSql
        GetValidaPRIM = ""
        Try
            oCs.Create("@SGFuncionID", PRIM)
            Return oSql._Valor(oSql.GetValidaFuncPRIM, "SGFuncionPrimaria", oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Private Function GetValidaMD(ByVal MD As String) As String
        Dim oSql As New SQLSubFunciones(oUsr)
        Dim oCs As New ColeccionPrmSql
        GetValidaMD = ""
        Try
            oCs.Create("@SGFuncionID", MD)
            Return oSql._Valor(oSql.GetValidaFuncMD, "SGFuncionMaestroDetalle", oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Private Sub LoadComboFuncionID()
        Dim oSqlf As New SQLSubFunciones(oUsr)
        Dim lCsf As New ColeccionPrmSql
        Try
            lCsf.Create("_Tabla", "SGFUNCIONES")
            lCsf.Create("_Qry", oSqlf.ItemFuncion)
            lCsf.Create("_DefaultKey", 0)
            lCsf.Create("_DefaultDes", "[SELECCIONAR]")
            LoadCombo(oUsr, DDLSGSubFuncionID, lCsf)
            DDLSGSubFuncionID.SelectedIndex = -1
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

    '================================================================================================================================
    'Acciones con el modelo de datos
    Private Sub LoadLista()
        Dim oSql As New SQLSubFunciones(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            oCs.Create("@SGFuncionID", sKeyFunc)
            oCs.Create("@SGSubFuncionID", txtSearch_SGSubFuncionID.Text & "%")
            oCs.Create("@SGFuncionEstatus", oUsr.Mis.Status)
            Dim oTabla As DataTable = oSql._List(oSql.ListSubF, "SGFUNCIONESSUBFUNCION", oCs)
            LoadGrid(GridView1, oTabla)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

    Protected Sub imgBtnAplicaFiltro_Click(sender As Object, e As ImageClickEventArgs) Handles imgBtnAplicaFiltro.Click
        pnlFiltros.Visible = False
        LoadLista()
    End Sub

    Protected Sub imgbtnCancelaFiltro_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnCancelaFiltro.Click
        pnlFiltros.Visible = False
    End Sub

    Private Sub BarEventos1_MsgEvent(sAcción As String) Handles BarEventos1.MsgEvent
        Select Case sAcción
            Case "Nuevo"
                SetFormEdit(sAcción, GridView1)
            Case "Editar"
                If lblSGFuncionID.Text <> "" Then
                    SetFormEdit(sAcción, GridView1)
                Else
                    Response.Write("<script>window.alert('No ha seleccionado el Id de un Registro ');</script>")
                End If
            Case "Eliminar"
                If lblSGFuncionID.Text <> "" Then
                    SetFormEdit(sAcción, GridView1)
                Else
                    Response.Write("<script>window.alert('No ha seleccionado el Id de un Registro ');</script>")
                End If
            Case "Filtrar"
                pnlFiltros.Visible = True

            Case Else
        End Select
    End Sub

    Private Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        sender.PageIndex = e.NewPageIndex
        LoadLista()
    End Sub

    Private Sub GridView1_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles GridView1.SelectedIndexChanging
        Dim IdFunc As String = GridView1.DataKeys(e.NewSelectedIndex).Value()
        lblSGFuncionID.Text = IdFunc
        Dim IdSubFunc As String = GridView1.DataKeys(e.NewSelectedIndex).Item(1)
        lblSGSubFuncionID.Text = IdSubFunc
    End Sub

    Private Sub SetFormEdit(ByVal sAcc As String, ByVal oGrid As GridView)
        Dim oSql As New SQLFunciones(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            pnlEventos.Visible = False
            pnlFiltros.Visible = False
            pnlAdd.Visible = True
            pnlListar.Visible = False

            Select Case sAcc
                Case "Nuevo"
                    'Configuraciones iniciales
                    lblAcción.Text = sAcc
                    pnlEventos.Visible = False
                    txt_SGFuncionID.Enabled = False
                    DDLSGSubFuncionID.Enabled = True
                    FlushNuevo()
                Case "Editar"
                    lblAcción.Text = sAcc
                    pnlEventos.Visible = False
                    txt_SGFuncionID.Enabled = False
                    DDLSGSubFuncionID.Enabled = False
                    DDLSGSubFuncionID.Items.Clear()
                    LoadEdit()
                    FlushEditar()
                Case "Eliminar"
                    lblAcción.Text = sAcc
                    pnlEventos.Visible = False
                    txt_SGFuncionID.Enabled = False
                    DDLSGSubFuncionID.Enabled = False
                    txt_SGSubFuncionOrden.Enabled = False
                    DDLSGSubFuncionID.Items.Clear()
                    LoadEdit()
                    LockEliminar()
            End Select

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

    Protected Sub ImgBtnAceptar_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnAceptar.Click
        If lblAcción.Text = "Nuevo" Then
            If txt_SGFuncionID.Text <> "" And DDLSGSubFuncionID.SelectedValue <> "0" And txt_SGSubFuncionOrden.Text <> "" Then
                SaveSubF()
                LoadLista()
                SetFormConfig()
            Else
                Response.Write("<script>window.alert('No ha llenado los campos importantes obligatorios');</script>")
            End If
        End If
        If lblAcción.Text = "Editar" Then
            If txt_SGFuncionID.Text <> "" And DDLSGSubFuncionID.SelectedValue <> "0" And txt_SGSubFuncionOrden.Text <> "" Then
                UpdateSubF()
                LoadLista()
                SetFormConfig()
            Else
                Response.Write("<script>window.alert('No ha llenado los campos importantes obligatorios');</script>")
            End If
        End If
        If lblAcción.Text = "Eliminar" Then
            If DeleteSubF() Then
                LoadLista()
                SetFormConfig()
            End If
        End If
    End Sub

    Protected Sub ImgBtnCancelar_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnCancelar.Click
        SetFormConfig()
    End Sub

    Private Sub LoadEdit()
        Dim oSql As New SQLSubFunciones(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            oCs.Create("@SGFuncionID", lblSGFuncionID.Text)
            oCs.Create("@SGSubFuncionID", lblSGSubFuncionID.Text)
            Dim oTb As DataTable = oSql._Item(oSql.GetSubF, "SGFUNCIONESSUBFUNCION", oCs)
            If Not oTb Is Nothing Then
                If oTb.Rows.Count > 0 Then
                    Dim Dr As DataRow
                    Dr = oTb.Rows(0)
                    txt_SGFuncionID.Text = Dr("SGFuncionID")
                    DDLSGSubFuncionID.Items.Add(Dr("SGSubFuncionID").ToString)
                    GetIndex(DDLSGSubFuncionID, Dr("SGSubFuncionID").ToString)
                    txt_SGSubFuncionOrden.Text = Dr("SGSubFuncionOrden")
                End If
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try

    End Sub

    Private Function SaveSubF() As Boolean
        Dim oSql As New SQLSubFunciones(oUsr)
        Dim oCs As New ColeccionPrmSql
        SaveSubF = False
        Try
            oCs.Create("@SGFuncionID", "")
            Dim oTb As DataTable = oSql._Item(oSql.InsertSubF, "SGFUNCIONESSUBFUNCION", oCs)
            If Not oTb Is Nothing Then
                oTb.Columns("SGFuncionID").Unique = True
                oTb.Columns("SGSubFuncionID").Unique = True
                If oTb.Rows.Count = 0 Then
                    Dim Dr As DataRow = oTb.NewRow
                    Dr("SGFuncionID") = txt_SGFuncionID.Text
                    Dr("SGSubFuncionID") = DDLSGSubFuncionID.SelectedValue
                    Dr("SGSubFuncionOrden") = txt_SGSubFuncionOrden.Text
                    oTb.Rows.Add(Dr)
                    Return oSql.StatemenInsert(oTb)
                End If
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Function

    Private Function UpdateSubF() As Boolean
        Dim oSql As New SQLSubFunciones(oUsr)
        Dim oCs As New ColeccionPrmSql
        UpdateSubF = False
        Try
            oCs.Create("@SGFuncionID", lblSGFuncionID.Text)
            oCs.Create("@SGSubFuncionID", lblSGSubFuncionID.Text)
            Dim oTb As DataTable = oSql._Item(oSql.UpdateSubF, "SGFUNCIONESSUBFUNCION", oCs)
            If Not oTb Is Nothing Then
                oTb.Columns("SGFuncionID").Unique = True
                oTb.Columns("SGSubFuncionID").Unique = True
                If oTb.Rows.Count > 0 Then
                    Dim Dr As DataRow = oTb.Rows(0)
                    Dr("SGFuncionID") = txt_SGFuncionID.Text
                    Dr("SGSubFuncionID") = DDLSGSubFuncionID.SelectedValue
                    Dr("SGSubFuncionOrden") = txt_SGSubFuncionOrden.Text

                    Return oSql.StatemenUpdate(oTb)
                End If
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Function

    Private Function DeleteSubF() As Boolean
        Dim oSql As New SQLSubFunciones(oUsr)
        Dim oCs As New ColeccionPrmSql
        DeleteSubF = False
        Try
            oCs.Create("@SGFuncionID", lblSGFuncionID.Text)
            oCs.Create("@SGSubFuncionID", lblSGSubFuncionID.Text)
            Return oSql.ExecuteQry(oSql.DeleteSubF, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Function
End Class