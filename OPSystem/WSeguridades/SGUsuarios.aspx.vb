Imports Security_System
Public Class SGUsuarios
    Inherits System.Web.UI.Page
    Private Ds As New DataSet
    Private oUsr As UserLogin
    Private sKeyFunc As String
    Private Sub SGUsuarios_Init(sender As Object, e As EventArgs) Handles Me.Init
        oUsr = Session("Usr")
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            SetFormConfig()
            LoadLista()
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

        With BarEventos1
            .Editar.Boton.ToolTip = "Editar"
            .Listar.Boton.ToolTip = "Listar"
            .Especial1.Boton.ToolTip = "Acceso División"
            .Especial1.Boton.ImageUrl = "~/Img/accesodv.png"
            .Especial1.Boton.Visible = True
        End With

        'Valores predeterminados
        txtSearch_SgUserEmail.Text = ""
        txtSearch_SgNombre.Text = ""

        txt_SgUserID.Text = ""
        txt_SgUserName.Text = ""
        txt_SgUserEmail.Text = ""
        txt_SgUserAlta.Text = ""
        txtSgUserPasswd.Text = ""
        txt_SgApellidos.Text = ""
        txt_SgNombre.Text = ""

        'Tamaño de pagina predeterminado
        GridView1.PageSize = 10

        'DDLPERFIL
        Dim oSqlp As New SQLUsuarios(oUsr)
        Dim lCsp As New ColeccionPrmSql

        lCsp.Create("_Tabla", "PERFILES")
        lCsp.Create("_Qry", oSqlp.ItemPerfilID)
        lCsp.Create("_DefaultKey", 0)
        lCsp.Create("_DefaultDes", "[SELECCIONAR]")
        LoadCombo(oUsr, DDLPerfilID, lCsp)
        DDLPerfilID.SelectedIndex = -1
    End Sub

    Private Sub FlushEditar()
        txt_SgUserID.Enabled = False
        txt_SgUserName.Enabled = False
        txt_SgUserEmail.Enabled = False
        txt_SgUserAlta.Enabled = False
        DDLPerfilID.Enabled = True
        txtSgUserPasswd.Enabled = False
        txt_SgApellidos.Enabled = True
        txt_SgNombre.Enabled = True
    End Sub

    Private Sub LoadComboPerfilID()
        Dim oSqlp As New SQLUsuarios(oUsr)
        Dim lCsp As New ColeccionPrmSql
        Try
            lCsp.Create("_Tabla", "PERFILES")
            lCsp.Create("_Qry", oSqlp.ItemPerfilID)
            lCsp.Create("_DefaultKey", 0)
            lCsp.Create("_DefaultDes", "[SELECCIONAR]")
            LoadCombo(oUsr, DDLPerfilID, lCsp)
            DDLPerfilID.SelectedIndex = -1
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

    '================================================================================================================================
    'Acciones con el modelo de datos
    Private Sub LoadLista()
        Dim oSql As New SQLUsuarios(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            Dim oTabla As DataTable = oSql._List(oSql.ListUsuarios1, "SGUSUARIOS", oCs)
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

            Case "Editar"
                If lblSgUserID.Text <> "" Then
                    SetFormEdit(sAcción, GridView1)
                Else
                    Response.Write("<script>window.alert('No ha seleccionado el Id de un Registro ');</script>")
                End If
            Case "Filtrar"
                pnlFiltros.Visible = True
            Case "Especial1"
                If lblSgUserID.Text <> "" Then
                    Dim sUrl As String = "AccesosDv.aspx?Id=@Key"
                    ' Dim sKey As String = GridView1.DataKeys(GridView1.SelectedRow.DataItemIndex).Value
                    Dim sKey As String = lblSgUserID.Text
                    Response.Redirect(sUrl.Replace("@Key", sKey))
                Else
                    Response.Write("<script>window.alert('No ha seleccionado el Id de un Registro ');</script>")
                End If
            Case Else
        End Select
    End Sub

    Private Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        sender.PageIndex = e.NewPageIndex
        LoadLista()
    End Sub

    Private Sub GridView1_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles GridView1.SelectedIndexChanging
        Dim IdFunc As String = GridView1.DataKeys(e.NewSelectedIndex).Value()
        lblSgUserID.Text = IdFunc
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
                Case "Editar"
                    lblAcción.Text = sAcc
                    pnlEventos.Visible = False
                    LoadEdit()
                    FlushEditar()
            End Select

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

    Protected Sub ImgBtnAceptar_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnAceptar.Click
        If lblAcción.Text = "Editar" Then
            If DDLPerfilID.SelectedValue <> "0" And txt_SgApellidos.Text <> "" And txt_SgNombre.Text <> "" Then
                UpdateUser()
                LoadLista()
                SetFormConfig()
            Else
                Response.Write("<script>window.alert('No ha llenado los campos importantes obligatorios');</script>")
            End If
        End If
    End Sub

    Protected Sub ImgBtnCancelar_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnCancelar.Click
        SetFormConfig()
    End Sub

    Private Sub LoadEdit()
        Dim oSql As New SQLUsuarios(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            oCs.Create("@SgUserID", lblSgUserID.Text)
            Dim oTb As DataTable = oSql._Item(oSql.GetUser, "SGUSUARIOS", oCs)
            If Not oTb Is Nothing Then
                If oTb.Rows.Count > 0 Then
                    Dim Dr As DataRow
                    Dr = oTb.Rows(0)
                    txt_SgUserID.Text = Dr("SgUserID")
                    txt_SgUserName.Text = Dr("SgUserName")
                    txt_SgUserEmail.Text = Dr("SgUserEmail")
                    txt_SgUserAlta.Text = Dr("SgUserAlta")
                    DDLPerfilID.SelectedValue = Dr("PerfilID")
                    txtSgUserPasswd.Text = Dr("SgUserPasswd")
                    txt_SgApellidos.Text = Dr("SgApellidos")
                    txt_SgNombre.Text = Dr("SgNombre")
                End If
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

    Private Function UpdateUser() As Boolean
        Dim oSql As New SQLUsuarios(oUsr)
        Dim oCs As New ColeccionPrmSql
        UpdateUser = False
        Try
            oCs.Create("@SgUserID", lblSgUserID.Text)
            Dim oTb As DataTable = oSql._Item(oSql.UpdateUser, "SGUSUARIOS", oCs)
            If Not oTb Is Nothing Then
                oTb.Columns("SgUserID").Unique = True
                If oTb.Rows.Count > 0 Then
                    Dim Dr As DataRow = oTb.Rows(0)
                    Dr("SgUserID") = txt_SgUserID.Text
                    Dr("SgUserName") = txt_SgUserName.Text
                    Dr("SgUserEmail") = txt_SgUserEmail.Text
                    Dr("SgUserAlta") = txt_SgUserAlta.Text
                    Dr("PerfilID") = DDLPerfilID.SelectedValue
                    Dr("SgUserPasswd") = txtSgUserPasswd.Text
                    Dr("SgApellidos") = txt_SgApellidos.Text
                    Dr("SgNombre") = txt_SgNombre.Text
                    Return oSql.StatemenUpdate(oTb)
                End If
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Function

End Class