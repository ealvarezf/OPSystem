Imports Security_System
Public Class SGEstructuraDetalle
    Inherits System.Web.UI.Page
    Private Ds As New DataSet
    Private oUsr As UserLogin
    Private oCs As New ColeccionPrmSql

    Private sKey As String
    Private sDes As String
    Private Sub SGEstructuraDetalle_Init(sender As Object, e As EventArgs) Handles Me.Init
        oUsr = Session("Usr")
        sKey = Request.Params("Id")
        sDes = Request.Params("Des")
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txt_EstructuraMenuID.Text = sKey
            SetFormConfig()
            LoadTreeView(sKey)
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

    End Sub

    '================================================================================================================================
    'Acciones con el modelo de datos
    Private Function LoadTreeView(ByVal skey As String) As Boolean
        Dim oSql As New SQLEstructuras(oUsr)
        Dim oCs As New ColeccionPrmSql
        LoadTreeView = False
        Try
            With TreeView1
                .Nodes.Clear()
                Dim mnuNodeItem As New TreeNode
                oCs.Create("@keyeme", skey)
                If oSql.GetQry(Ds, "TREE", oSql.ListDestalle, oCs) Then
                    For Each drMenuItem As Data.DataRow In Ds.Tables("TREE").Rows
                        'Condición que indica si es elemento raiz
                        If drMenuItem("ModuloID").ToString.Equals(drMenuItem("ModuloPadreID")) Then
                            mnuNodeItem = New TreeNode
                            mnuNodeItem.Value = drMenuItem("ModuloID").ToString
                            mnuNodeItem.Text = drMenuItem("Modulo").ToString
                            mnuNodeItem.ToolTip = "Modulo"
                            'agregamos el Ítem al menú
                            TreeView1.Nodes.Add(mnuNodeItem)
                            AddNodeTree(mnuNodeItem, Ds.Tables("TREE"))
                            'hacemos un llamado al metodo recursivo encargado de generar el árbol del menú.
                        End If
                    Next
                End If
                .ExpandAll()

            End With

            LoadTreeView = True

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)

        End Try
    End Function

    Public Sub AddNodeTree(ByRef mnuNodeItem As TreeNode, ByVal dtTreeItems As Data.DataTable)
        Try
            For Each drTreeItem As DataRow In dtTreeItems.Rows
                If drTreeItem("ModuloPadreID").ToString.Equals(mnuNodeItem.Value) And Not drTreeItem("ModuloID").ToString.Equals(mnuNodeItem.Value) Then
                    Dim mnuNewTreeItem As New TreeNode
                    mnuNewTreeItem.Value = drTreeItem("ModuloID").ToString
                    mnuNewTreeItem.Text = drTreeItem("Modulo").ToString
                    mnuNewTreeItem.ToolTip = IIf(drTreeItem("Nodo").ToString = "M", "Modulo", "Función")
                    'Agregamos el nuevo Nodo al NodoItem de nivel superior
                    mnuNodeItem.ChildNodes.Add(mnuNewTreeItem)
                    'Llamada recursiva para ver si el nuevo menu item tiene elementos hijos
                    AddNodeTree(mnuNewTreeItem, dtTreeItems)

                End If
            Next
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)

        End Try

    End Sub

    Protected Sub imgBtnAplicaFiltro_Click(sender As Object, e As ImageClickEventArgs) Handles imgBtnAplicaFiltro.Click
        pnlFiltros.Visible = False
    End Sub

    Protected Sub imgbtnCancelaFiltro_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnCancelaFiltro.Click
        pnlFiltros.Visible = False
    End Sub

    Private Sub BarEventos1_MsgEvent(sAcción As String) Handles BarEventos1.MsgEvent
        Select Case sAcción
            Case "Nuevo"
                SetFormEdit(sAcción, TreeView1)
            Case "Eliminar"
                If DeleteNodo(TreeView1.SelectedNode) Then
                    LoadTreeView(txt_EstructuraMenuID.Text)
                End If
            Case "Editar"
                SetFormEdit(sAcción, TreeView1)
            Case "Filtrar"
                'pnlFiltros.Visible = True

            Case Else
        End Select
    End Sub

    Private Sub LoadComboItems(ByVal sEstruct As String)
        Dim oSql As New SQLEstructuras(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            oCs.Create("@EstructuraMenuID", sEstruct)
            Dim oTb As DataTable = oSql._List(oSql.ListItemsDisponibles, "Items", oCs)
            If Not oTb Is Nothing Then
                'oTb.DefaultView.Sort = ""
                LoadCombo(oUsr, DDL_ModuloID, oTb.DefaultView)
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

    Private Sub SetFormEdit(ByVal sAcc As String, ByVal oTv As TreeView)
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
                    DDL_ModuloPadreID.Items.Clear()
                    Dim iTemPadre As New ListItem
                    iTemPadre.Text = oTv.SelectedNode.Text
                    iTemPadre.Value = oTv.SelectedNode.Value
                    DDL_ModuloPadreID.Items.Add(iTemPadre)
                    DDL_ModuloPadreID.SelectedIndex = 0
                    LoadComboItems(txt_EstructuraMenuID.Text)
                Case Else

            End Select

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

    Protected Sub ImgBtnAceptar_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnAceptar.Click
        If SaveTable() Then
            SetFormConfig()
            LoadTreeView(txt_EstructuraMenuID.Text)
        End If
    End Sub

    Protected Sub ImgBtnCancelar_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnCancelar.Click
        SetFormConfig()
    End Sub

    Private Function SaveEstructuraModulo(ByVal sKeyPad As String, ByVal sKeyMod As String) As Boolean
        Dim oSql As New SQLEstructuras(oUsr)
        Dim oCs As New ColeccionPrmSql
        SaveEstructuraModulo = False
        Try
            oCs.Create("@EstructuraMenuID", txt_EstructuraMenuID.Text)
            oCs.Create("@ModuloID", sKeyMod)
            oCs.Create("@ModuloPadreID", sKeyPad)
            Dim iOrden As Integer = IIf(IsDBNull(oSql._Value(oSql.ItemMaxChild, "MaxChild", oCs)), 0, oSql._Value(oSql.ItemMaxChild, "MaxChild", oCs))
            oCs.Create("@EstructuraMenuOrden", iOrden + 1)
            Return oSql.ExecuteQry(oSql.InsertED, oCs)

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Function

    Private Function SaveEstructuraFuncion(ByVal sKeyPad As String, ByVal sKeyFun As String) As Boolean
        Dim oSql As New SQLEstructuras(oUsr)
        Dim oCs As New ColeccionPrmSql
        SaveEstructuraFuncion = False
        Try
            oCs.Create("@EstructuraMenuID", txt_EstructuraMenuID.Text)
            oCs.Create("@ModuloID", sKeyPad)
            oCs.Create("@SGFuncionID", sKeyFun)
            Dim iOrden As Integer = IIf(IsDBNull(oSql._Value(oSql.ItemMaxChildFuncion, "MaxChild", oCs)), 0, oSql._Value(oSql.ItemMaxChildFuncion, "MaxChild", oCs))
            oCs.Create("@SGFuncionModuloOrden", iOrden + 1)
            Return oSql.ExecuteQry(oSql.InsertFM, oCs)

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Function

    Private Function SaveTable() As Boolean
        Dim oSql As New SQLEstructuras(oUsr)
        Dim oCs As New ColeccionPrmSql
        SaveTable = False
        Try
            Dim sKeyPad As String = DDL_ModuloPadreID.SelectedValue
            Dim sKeyNod As String = DDL_ModuloID.SelectedValue
            Dim sNodo As String = DDL_ModuloID.SelectedItem.Text.Substring(DDL_ModuloID.SelectedItem.Text.Length - 1)
            If sNodo = "M" Then
                Return SaveEstructuraModulo(sKeyPad, sKeyNod)
            Else
                Return SaveEstructuraFuncion(sKeyPad, sKeyNod)
            End If

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Function

    Private Function DeleteItem(ByVal sQry As String, ByVal oCs As ColeccionPrmSql) As Boolean
        Dim oSQL As New SQLEstructuras(oUsr)
        DeleteItem = False
        Try
            Return oSQL.ExecuteQry(sQry, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Function

    Private Function DeleteNodo(ByVal oNode As TreeNode) As Boolean
        Dim oCs As New ColeccionPrmSql
        Dim sQry As String = ""
        Dim bExito As Boolean
        DeleteNodo = False
        Try
            For Each nh As TreeNode In oNode.ChildNodes
                DeleteNodo(nh)
            Next
            oCs.Create("@EstructuraMenuID", txt_EstructuraMenuID.Text)
            oCs.Create("@ModuloID", "")
            oCs.Create("@SGFuncionID", "")
            If oNode.ToolTip = "Función" Then
                Dim oSql As New SQLEstructuras(oUsr)
                sQry = oSql.DeleteFM
                oCs.ItemValue("@ModuloID") = oNode.Parent.Value
                oCs.ItemValue("@SGFuncionID") = oNode.Value
            End If
            If oNode.ToolTip = "Modulo" Then
                Dim oSql As New SQLEstructuras(oUsr)
                sQry = oSql.DeleteED
                oCs.ItemValue("@ModuloID") = oNode.Value
            End If
            If DeleteItem(sQry, oCs) Then
                bExito = True
                TreeView1.Nodes.Remove(oNode)
            End If

            Return bExito

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Function

End Class