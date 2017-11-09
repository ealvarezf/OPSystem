Imports Security_System
Public Class MovAlmacenDetalle
    Inherits System.Web.UI.Page
    Private Ds As New DataSet
    Private oUsr As UserLogin
    Private oCs As New ColeccionPrmSql

#Region "Inicializar datos"
    Private Sub SetFormConfig()
        Try
            pnlEventos.Visible = True
            pnlFiltros.Visible = False
            pnlAdd.Visible = False
            pnlListar.Visible = True

            With BarEventos1
                .Filtrar.Boton.Visible = False
            End With

            LoadComboProductos(ddl_ProductoID)
            txt_MovAlmacenLote.MaxLength = 40
            txt_MovAlmacenLote.Text = GetPreLote(lblTitulo.Text.Substring(0, 1), lblTitulo.Text.Substring(1, lblTitulo.Text.Length - 1))
            txt_MovAlmacenLote.Enabled = False

            GridView1.PageSize = 10

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub
#End Region
#Region "Eventos"
    Private Sub MovAlmacenDetalle_Init(sender As Object, e As EventArgs) Handles Me.Init
        oUsr = Session("Usr")
        lblTitulo.Text = Request.Params("key")
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            SetFormConfig()
            LoadLista(lblTitulo.Text.Substring(0, 1), lblTitulo.Text.Substring(1, lblTitulo.Text.Length - 1))
        End If
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
        LoadLista(lblTitulo.Text.Substring(0, 1), lblTitulo.Text.Substring(1, lblTitulo.Text.Length - 1))
    End Sub
    Protected Sub ImgBtnAceptar_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnAceptar.Click
        Select Case lblAcción.Text
            Case "Eliminar"
                If Delete() Then
                    LoadLista(lblTitulo.Text.Substring(0, 1), lblTitulo.Text.Substring(1, lblTitulo.Text.Length - 1))
                    SetFormConfig()
                End If
            Case Else
                If SaveTable() Then
                    LoadLista(lblTitulo.Text.Substring(0, 1), lblTitulo.Text.Substring(1, lblTitulo.Text.Length - 1))
                    SetFormConfig()
                End If
        End Select
    End Sub
    Protected Sub ImgBtnCancelar_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnCancelar.Click
        SetFormConfig()
    End Sub
#End Region
#Region "CargaDatos"
    Private Sub LoadLista(ByVal sKey As String, ByVal iKey As Integer)
        Dim oSql As New SQLMovimientosAlmacenDetalle(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            oCs.Create("@MovAlmacenSentido", sKey)
            oCs.Create("@MovAlmacenID", iKey)
            Dim oTabla As DataTable = oSql._List(oSql.List, "DETALLE", oCs)
            LoadGrid(GridView1, oTabla)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub
    Private Sub LoadComboProductos(ByVal ddl As DropDownList)
        Dim oSql As New SQLProductos(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            oCs.Create("@EmpresaID", oUsr.Mis.Empresa)
            Dim oTb As DataTable = oSql._List(oSql.ListCombo, "PRD", oCs)
            If Not oTb Is Nothing Then
                oTb.DefaultView.Sort = "Producto"
                Dim Dr As DataRow = oTb.NewRow
                Dr("ProductoID") = "%"
                Dr("Producto") = "[SELECCIONAR]"
                oTb.Rows.Add(Dr)
                LoadCombo(oUsr, ddl, oTb.DefaultView)
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub
    Private Function GetPreLote(ByVal sKey As String, ByVal iKey As Integer) As String
        Dim oSql As New SQLMovimientosAlmacen(oUsr)
        GetPreLote = String.Empty
        Try
            oCs.Create("@MovAlmacenSentido", sKey)
            oCs.Create("@MovAlmacenID", iKey)
            Dim oTb As DataTable = oSql._Item(oSql.Item, "MA", oCs)
            If Not oTb Is Nothing Then
                For Each Dr As DataRow In oTb.Rows
                    oCs.Create("@ProcesoID", Dr("ProcesoID"))
                    oCs.Create("@TablaID", Dr("TablaID"))
                    oCs.Create("@VariedadID", Dr("VariedadID"))
                    oCs.Create("@FechaID", Dr("MovAlmacenFecha"))
                    oCs.Create("@Extra", "")
                    GetPreLote = oSql._Value(oSql.PreLote, "PreLote", oCs)
                    Exit For
                Next
            End If

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Function
    Private Sub SetFormEdit(ByVal sAcc As String, ByVal oGrid As GridView)
        Dim oSql As New SQLMovimientosAlmacenDetalle(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            pnlEventos.Visible = False
            pnlFiltros.Visible = False
            pnlAdd.Visible = True
            pnlListar.Visible = False

            lblAcción.Text = sAcc
            Select Case sAcc
                Case "Nuevo"
                    'Inicializar datos
                    ddl_ProductoID.Enabled = True
                    txt_MovAlmacenEsp.Enabled = True

                Case "Editar"
                    Dim sKeySent As String = lblTitulo.Text.Substring(0, 1)
                    Dim iKeyMov As Integer = lblTitulo.Text.Substring(1, lblTitulo.Text.Length - 1)
                    Dim sKeyProd As String = oGrid.DataKeys(oGrid.SelectedRow.RowIndex).Values(0)
                    Dim sKeyLote As String = oGrid.DataKeys(oGrid.SelectedRow.RowIndex).Values(1)
                    oCs.Create("@MovAlmacenSentido", sKeySent)
                    oCs.Create("@MovAlmacenID", iKeyMov)
                    oCs.Create("@ProductoID", sKeyProd)
                    oCs.Create("@MovAlmacenLote", sKeyLote)
                    Dim oTb As DataTable = oSql._Item(oSql.Item, "MOVIMIENTOALMACENDETALLE", oCs)
                    If Not oTb Is Nothing Then
                        For Each Dr As DataRow In oTb.Rows
                            GetIndex(ddl_ProductoID, Dr("ProductoID"))
                            txt_MovAlmacenLote.Text = Dr("MovAlmacenLote")
                            txt_MovAlmacenCantidad.Text = Dr("MovAlmacenCantidad")
                            txt_MovAlmacenPeso.Text = Dr("MovAlmacenPeso")
                            Exit For
                        Next
                    End If
                    ddl_ProductoID.Enabled = False
                    txt_MovAlmacenEsp.Enabled = False

                Case "Eliminar"
                    Dim sKeySent As String = lblTitulo.Text.Substring(0, 1)
                    Dim iKeyMov As Integer = lblTitulo.Text.Substring(1, lblTitulo.Text.Length - 1)
                    Dim sKeyProd As String = oGrid.DataKeys(oGrid.SelectedRow.RowIndex).Values(0)
                    Dim sKeyLote As String = oGrid.DataKeys(oGrid.SelectedRow.RowIndex).Values(1)
                    oCs.Create("@MovAlmacenSentido", sKeySent)
                    oCs.Create("@MovAlmacenID", iKeyMov)
                    oCs.Create("@ProductoID", sKeyProd)
                    oCs.Create("@MovAlmacenLote", sKeyLote)
                    Dim oTb As DataTable = oSql._Item(oSql.Item, "MOVIMIENTOALMACENDETALLE", oCs)
                    If Not oTb Is Nothing Then
                        For Each Dr As DataRow In oTb.Rows
                            GetIndex(ddl_ProductoID, Dr("ProductoID"))
                            txt_MovAlmacenLote.Text = Dr("MovAlmacenLote")
                            txt_MovAlmacenCantidad.Text = Dr("MovAlmacenCantidad")
                            txt_MovAlmacenPeso.Text = Dr("MovAlmacenPeso")
                            Exit For
                        Next
                    End If
                    ddl_ProductoID.Enabled = False
                    txt_MovAlmacenEsp.Enabled = False
                    txt_MovAlmacenCantidad.Enabled = False
                    txt_MovAlmacenPeso.Enabled = False

            End Select

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub
#End Region
#Region "AccionesDatos"
    Private Function Delete() As Boolean
        Dim oSql As New SQLMovimientosAlmacenDetalle(oUsr)
        Dim oCs As New ColeccionPrmSql
        Delete = False
        Try
            oCs.Create("@MovAlmacenSentido", lblTitulo.Text.Substring(0, 1))
            oCs.Create("@MovAlmacenID", lblTitulo.Text.Substring(1, lblTitulo.Text.Length - 1))
            oCs.Create("@ProductoID", ddl_ProductoID.SelectedValue)
            oCs.Create("@MovAlmacenLote", txt_MovAlmacenLote.Text)
            Return oSql.ExecuteQry(oSql.Delete, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Function
    Private Function SaveTable() As Boolean
        Dim oSql As New SQLMovimientosAlmacenDetalle(oUsr)
        Dim oCs As New ColeccionPrmSql
        SaveTable = False
        Try
            oCs.Create("@MovAlmacenSentido", lblTitulo.Text.Substring(0, 1))
            oCs.Create("@MovAlmacenID", lblTitulo.Text.Substring(1, lblTitulo.Text.Length - 1))
            oCs.Create("@ProductoID", ddl_ProductoID.SelectedValue)
            If lblAcción.Text = "Nuevo" Then
                oCs.Create("@MovAlmacenLote", txt_MovAlmacenLote.Text & txt_MovAlmacenEsp.Text)
            Else
                oCs.Create("@MovAlmacenLote", txt_MovAlmacenLote.Text)
            End If

            Dim oTb As DataTable = oSql._Item(oSql.Item, "MOVIMIENTOALMACENDETALLE", oCs)
            If Not oTb Is Nothing Then
                oTb.Columns("MovAlmacenSentido").Unique = True
                oTb.Columns("MovAlmacenID").Unique = True
                oTb.Columns("ProductoID").Unique = True
                oTb.Columns("MovAlmacenLote").Unique = True
                If oTb.Rows.Count = 0 Then
                    Dim Dr As DataRow = oTb.NewRow
                    Dr("MovAlmacenSentido") = lblTitulo.Text.Substring(0, 1)
                    Dr("MovAlmacenID") = lblTitulo.Text.Substring(1, lblTitulo.Text.Length - 1)
                    Dr("ProductoID") = ddl_ProductoID.SelectedValue
                    Dr("MovAlmacenLote") = txt_MovAlmacenLote.Text
                    Dr("MovAlmacenCantidad") = Val(txt_MovAlmacenCantidad.Text)
                    Dr("MovAlmacenPeso") = Val(txt_MovAlmacenPeso.Text)
                    oTb.Rows.Add(Dr)
                    Return oSql.StatemenInsert(oTb)
                Else
                    ' No hay opción
                    Dim Dr As DataRow = oTb.Rows(0)
                    Dr("MovAlmacenCantidad") = Val(txt_MovAlmacenCantidad.Text)
                    Dr("MovAlmacenPeso") = Val(txt_MovAlmacenPeso.Text)
                    Return oSql.StatemenUpdate(oTb)
                End If
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Function
#End Region

End Class