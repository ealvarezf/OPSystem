Imports Security_System
Public Class SGModulos
    Inherits System.Web.UI.Page
    Private Ds As New DataSet
    Private oUsr As UserLogin
    Private oCs As New ColeccionPrmSql
    Private Sub SGModulos_Init(sender As Object, e As EventArgs) Handles Me.Init
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

        End With
        'Valores predeterminados
        txt_ModuloID.Text = ""
        txt_Modulo.Text = ""
        txt_ModuloAyuda.Text = ""
        txt_ModuloNombre.Text = ""
        txt_ModuloID.MaxLength = 6
        txt_Modulo.MaxLength = 12
        txt_ModuloAyuda.MaxLength = 60
        txt_ModuloNombre.MaxLength = 50
        'Tamaño de pagina predeterminado
        GridView1.PageSize = 10
    End Sub

    '================================================================================================================================
    'Acciones con el modelo de datos
    Private Sub LoadLista()
        Dim oSql As New SQLModulos(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            oCs.Create("@PersonalNomFull", txtSearch_Modulo.Text & "%")
            oCs.Create("@ModuloEstatus", oUsr.Mis.Status)
            Dim oTabla As DataTable = oSql._List(oSql.List, "MODULOS", oCs)
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
            Case "Eliminar"
                SetFormEdit(sAcción, GridView1)
            Case "Editar"
                SetFormEdit(sAcción, GridView1)
            Case "Filtrar"
                pnlFiltros.Visible = True

            Case Else
        End Select
    End Sub

    Private Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        sender.PageIndex = e.NewPageIndex
        LoadLista()
    End Sub

    Private Sub SetFormEdit(ByVal sAcc As String, ByVal oGrid As GridView)
        Dim oSql As New SQLModulos(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            pnlEventos.Visible = False
            pnlFiltros.Visible = False
            pnlAdd.Visible = True
            pnlListar.Visible = False

            Select Case sAcc
                Case "Nuevo"
                    'Configuraciones iniciales
                Case Else
                    Dim sKey As String = oGrid.DataKeys(oGrid.SelectedRow.RowIndex).Value
                    oCs.Create("@ModuloID", sKey)
                    Dim oTb As DataTable = oSql._Item(oSql.Item, "R", oCs)
                    If Not oTb Is Nothing Then
                        For Each Dr As DataRow In oTb.Rows
                            lblAcción.Text = sAcc
                            txt_ModuloID.Text = sKey.ToString
                            txt_Modulo.Text = Dr("Modulo").ToString
                            txt_ModuloAyuda.Text = Dr("ModuloAyuda").ToString
                            txt_ModuloNombre.Text = Dr("ModuloNombre").ToString
                            Exit For
                        Next
                    End If

            End Select

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

    Protected Sub ImgBtnAceptar_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnAceptar.Click
        If SaveTable() Then
            LoadLista()
            SetFormConfig()
        End If
    End Sub

    Protected Sub ImgBtnCancelar_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnCancelar.Click
        SetFormConfig()
    End Sub

    Private Function SaveTable() As Boolean
        Dim oSql As New SQLModulos(oUsr)
        Dim oCs As New ColeccionPrmSql
        SaveTable = False
        Try
            oCs.Create("@ModuloID", txt_ModuloID.Text)
            Dim oTb As DataTable = oSql._Item(oSql.Item, "SGMODULOS", oCs)
            If Not oTb Is Nothing Then
                oTb.Columns("ModuloID").Unique = True
                If oTb.Rows.Count = 0 Then
                    Dim Dr As DataRow = oTb.NewRow
                    Dr("ModuloID") = txt_ModuloID.Text
                    Dr("Modulo") = txt_Modulo.Text
                    Dr("ModuloAyuda") = txt_ModuloAyuda.Text
                    Dr("ModuloNombre") = txt_ModuloNombre.Text
                    Dr("ModuloEstatus") = oUsr.Mis.Status
                    oTb.Rows.Add(Dr)
                    Return oSql.StatemenInsert(oTb)
                Else
                    Dim Dr As DataRow = oTb.Rows(0)
                    Dr("Modulo") = txt_Modulo.Text
                    Dr("ModuloAyuda") = txt_ModuloAyuda.Text
                    Dr("ModuloNombre") = txt_ModuloNombre.Text
                    Return oSql.StatemenUpdate(oTb)
                End If
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Function
End Class