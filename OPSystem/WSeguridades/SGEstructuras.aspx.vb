Imports Security_System
Public Class SGEstructuras
    Inherits System.Web.UI.Page
    Private Ds As New DataSet
    Private oUsr As UserLogin
    Private oCs As New ColeccionPrmSql
    Private Sub SGEstructuras_Init(sender As Object, e As EventArgs) Handles Me.Init
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
            .Especial1.Boton.Visible = True
            .Especial1.Boton.ImageUrl = "~/Img/EventosEsp.jpg"
        End With
        'Valores predeterminados
        txt_EstructuraMenuID.Text = ""
        txt_EstructuraMenu.Text = ""
        txt_EstructuraMenuID.MaxLength = 3
        txt_EstructuraMenu.MaxLength = 60
        'Tamaño de pagina predeterminado
        GridView1.PageSize = 10
    End Sub

    '================================================================================================================================
    'Acciones con el modelo de datos
    Private Sub LoadLista()
        Dim oSql As New SQLEstructuras(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            oCs.Create("@EstructuraMenu", txtSearch_EstructuraMenu.Text & "%")
            Dim oTabla As DataTable = oSql._List(oSql.List, "ESTRUCTURAS", oCs)
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
                If GridView1.SelectedRow Is Nothing Then
                    Response.Write("<script>window.alert('No ha seleccionado Registro ');</script>")
                    Exit Sub
                End If
                SetFormEdit(sAcción, GridView1)
            Case "Editar"
                If GridView1.SelectedRow Is Nothing Then
                    Response.Write("<script>window.alert('No ha seleccionado Registro ');</script>")
                    Exit Sub
                End If
                SetFormEdit(sAcción, GridView1)
            Case "Filtrar"
                pnlFiltros.Visible = True
            Case "Especial1"
                Dim sUrl As String = "SGEstructuraDetalle.aspx?Id=@Key"
                Dim sKey As String = GridView1.DataKeys(GridView1.SelectedRow.DataItemIndex).Value
                Response.Redirect(sUrl.Replace("@Key", sKey))
            Case Else
        End Select
    End Sub

    Private Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        sender.PageIndex = e.NewPageIndex
        LoadLista()
    End Sub
    Private Sub SetFormEdit(ByVal sAcc As String, ByVal oGrid As GridView)
        Dim oSql As New SQLEstructuras(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            pnlEventos.Visible = False
            pnlFiltros.Visible = False
            pnlAdd.Visible = True
            pnlListar.Visible = False

            Select Case sAcc
                Case "Nuevo"
                    lblAcción.Text = sAcc
                    'Configuraciones iniciales
                Case Else
                    Dim sKey As String = oGrid.DataKeys(oGrid.SelectedRow.RowIndex).Value
                    oCs.Create("@EstructuraMenuID", sKey)
                    Dim oTb As DataTable = oSql._Item(oSql.Item, "R", oCs)
                    If Not oTb Is Nothing Then
                        For Each Dr As DataRow In oTb.Rows
                            lblAcción.Text = sAcc
                            txt_EstructuraMenuID.Text = sKey.ToString
                            txt_EstructuraMenu.Text = Dr("EstructuraMenu").ToString
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
        Dim oSql As New SQLEstructuras(oUsr)
        Dim oCs As New ColeccionPrmSql
        SaveTable = False
        Try
            oCs.Create("@EstructuraMenuID", txt_EstructuraMenuID.Text)
            Dim oTb As DataTable = oSql._Item(oSql.Item, "SGESTRUCTURA_MENU", oCs)
            If Not oTb Is Nothing Then
                oTb.Columns("EstructuraMenuID").Unique = True
                If oTb.Rows.Count = 0 Then
                    Dim Dr As DataRow = oTb.NewRow
                    Dr("EstructuraMenuID") = txt_EstructuraMenuID.Text
                    Dr("EstructuraMenu") = txt_EstructuraMenu.Text
                    oTb.Rows.Add(Dr)
                    Return oSql.StatemenInsert(oTb)
                Else
                    Dim Dr As DataRow = oTb.Rows(0)
                    Dr("EstructuraMenu") = txt_EstructuraMenu.Text
                    Return oSql.StatemenUpdate(oTb)
                End If
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Function

End Class