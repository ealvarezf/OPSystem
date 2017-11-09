Imports Security_System
Public Class SGPerfilDetalle
    Inherits System.Web.UI.Page
    Private Ds As New DataSet
    Private oUsr As UserLogin
    Private oCs As New ColeccionPrmSql

    Private sKey As String
    Private sDes As String
    Private Sub SGPerfilDetalle_Init(sender As Object, e As EventArgs) Handles Me.Init
        oUsr = Session("Usr")
        sKey = Request.Params("Id")
        sDes = Request.Params("Des")
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txt_PerfilID.Text = sKey
            txt_EstructuraMenuID.Text = GetEstructuraID(sKey)
            SetFormConfig()
            LoadTreeView(txt_EstructuraMenuID.Text)
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

    End Sub

    '================================================================================================================================
    'Acciones con el modelo de datos
    '================================================================================================================================
    Private Function GetEstructuraID(ByVal sKey As String) As String
        Dim oSql As New SQLPerfiles(oUsr)
        Dim oCs As New ColeccionPrmSql
        GetEstructuraID = String.Empty
        Try
            oCs.Create("@PerfilID", sKey)
            GetEstructuraID = oSql._Value(oSql.Item, "EstructuraMenuID", oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)

        End Try
    End Function


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
                    'Se agregan nodos de evento
                    AddNodeEvento(mnuNewTreeItem, drTreeItem)
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

    Private Function Permiso(ByVal sPfl As String, ByVal sMod As String, ByVal sFun As String, ByVal sEve As String) As Boolean
        Dim oSql As New SQLDetallesPerfil(oUsr)
        Dim oCs As New ColeccionPrmSql
        Permiso = False
        Try
            oCs.Create("@PerfilID", sPfl)
            oCs.Create("@ModuloID", sMod)
            oCs.Create("@SGFuncionID", sFun)
            Return (oSql._Value(oSql.Item, sEve, oCs) = "S")

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Function

    Private Sub AddNodeEvento(ByRef mnuNodeItem As TreeNode, ByVal Drf As DataRow)
        Dim oSql As New SQLFunciones(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            If Drf("Nodo").ToString = "F" Then

                'Eventos Estandar

                oCs.Create("@SGFuncionID", Drf("ModuloID"))
                Dim oTb As DataTable = oSql._Item(oSql.Item, "FUN", oCs)
                If Not oTb Is Nothing Then
                    Dim Dr As DataRow = oTb.Rows(0)

                    mnuNodeItem.Checked = Permiso(sKey, Drf("ModuloPadreID"), Drf("ModuloID"), "PerfilConsultar")

                    If Dr("SGFuncionNuevo").ToString = "S" Then
                        Dim NewItem As New TreeNode
                        NewItem.Value = Drf("ModuloID").ToString + "_PerfilNuevo"
                        NewItem.Text = "NUEVO"
                        NewItem.ToolTip = "Evento"
                        NewItem.Checked = Permiso(sKey, Drf("ModuloPadreID"), Drf("ModuloID"), "PerfilNuevo")
                        mnuNodeItem.ChildNodes.Add(NewItem)
                    End If
                    If Dr("SGFuncionEliminar").ToString = "S" Then
                        Dim NewItem As New TreeNode
                        NewItem.Value = Drf("ModuloID").ToString + "_PerfilEliminar"
                        NewItem.Text = "ELIMINAR"
                        NewItem.ToolTip = "Evento"
                        NewItem.Checked = Permiso(sKey, Drf("ModuloPadreID"), Drf("ModuloID"), "PerfilEliminar")
                        mnuNodeItem.ChildNodes.Add(NewItem)
                    End If
                    If Dr("SGFuncionRecuperar").ToString = "S" Then
                        Dim NewItem As New TreeNode
                        NewItem.Value = Drf("ModuloID").ToString + "_PerfilRecuperar"
                        NewItem.Text = "RECUPERAR"
                        NewItem.ToolTip = "Evento"
                        NewItem.Checked = Permiso(sKey, Drf("ModuloPadreID"), Drf("ModuloID"), "PerfilRecuperar")
                        mnuNodeItem.ChildNodes.Add(NewItem)
                    End If

                    If Dr("SGFuncionModificar").ToString = "S" Then
                        Dim NewItem As New TreeNode
                        NewItem.Value = Drf("ModuloID").ToString + "_PerfilModificar"
                        NewItem.Text = "MODIFICAR"
                        NewItem.ToolTip = "Evento"
                        NewItem.Checked = Permiso(sKey, Drf("ModuloPadreID"), Drf("ModuloID"), "PerfilModificar")
                        mnuNodeItem.ChildNodes.Add(NewItem)
                    End If

                    If Dr("SGFuncionListar").ToString = "S" Then
                        Dim NewItem As New TreeNode
                        NewItem.Value = Drf("ModuloID").ToString + "_PerfilListar"
                        NewItem.Text = "LISTAR"
                        NewItem.ToolTip = "Evento"
                        NewItem.Checked = Permiso(sKey, Drf("ModuloPadreID"), Drf("ModuloID"), "PerfilListar")
                        mnuNodeItem.ChildNodes.Add(NewItem)
                    End If

                    If Dr("SGFuncionExportar").ToString = "S" Then
                        Dim NewItem As New TreeNode
                        NewItem.Value = Drf("ModuloID").ToString + "_PerfilExportar"
                        NewItem.Text = "EXPORTAR"
                        NewItem.ToolTip = "Evento"
                        NewItem.Checked = Permiso(sKey, Drf("ModuloPadreID"), Drf("ModuloID"), "PerfilExportar")
                        mnuNodeItem.ChildNodes.Add(NewItem)
                    End If

                    If Dr("SGFuncionGraficar").ToString = "S" Then
                        Dim NewItem As New TreeNode
                        NewItem.Value = Drf("ModuloID").ToString + "_PerfilGraficar"
                        NewItem.Text = "GRAFICAR"
                        NewItem.ToolTip = "Evento"
                        NewItem.Checked = Permiso(sKey, Drf("ModuloPadreID"), Drf("ModuloID"), "PerfilGraficar")
                        mnuNodeItem.ChildNodes.Add(NewItem)
                    End If

                    If Dr("SGFuncionEventoEsp1").ToString = "S" Then
                        Dim NewItem As New TreeNode
                        NewItem.Value = Drf("ModuloID").ToString + "_PerfilEspecial1"
                        NewItem.Text = Dr("SGFuncionEventoEsp1Nombre")
                        NewItem.ToolTip = "Evento"
                        NewItem.Checked = Permiso(sKey, Drf("ModuloPadreID"), Drf("ModuloID"), "PerfilEspecial1")
                        mnuNodeItem.ChildNodes.Add(NewItem)
                    End If

                    If Dr("SGFuncionEventoEsp2").ToString = "S" Then
                        Dim NewItem As New TreeNode
                        NewItem.Value = Drf("ModuloID").ToString + "_PerfilEspecial2"
                        NewItem.Text = Dr("SGFuncionEventoEsp2Nombre")
                        NewItem.ToolTip = "Evento"
                        NewItem.Checked = Permiso(sKey, Drf("ModuloPadreID"), Drf("ModuloID"), "PerfilEspecial2")
                        mnuNodeItem.ChildNodes.Add(NewItem)
                    End If

                    If Dr("SGFuncionEventoEsp3").ToString = "S" Then
                        Dim NewItem As New TreeNode
                        NewItem.Value = Drf("ModuloID").ToString + "_PerfilEspecial3"
                        NewItem.Text = Dr("SGFuncionEventoEsp3Nombre")
                        NewItem.ToolTip = "Evento"
                        NewItem.Checked = Permiso(sKey, Drf("ModuloPadreID"), Drf("ModuloID"), "PerfilEspecial3")
                        mnuNodeItem.ChildNodes.Add(NewItem)
                    End If

                    If Dr("SGFuncionEventoEsp4").ToString = "S" Then
                        Dim NewItem As New TreeNode
                        NewItem.Value = Drf("ModuloID").ToString + "_PerfilEspecial4"
                        NewItem.Text = Dr("SGFuncionEventoEsp4Nombre")
                        NewItem.ToolTip = "Evento"
                        NewItem.Checked = Permiso(sKey, Drf("ModuloPadreID"), Drf("ModuloID"), "PerfilEspecial4")
                        mnuNodeItem.ChildNodes.Add(NewItem)
                    End If

                    If Dr("SGFuncionEventoEsp5").ToString = "S" Then
                        Dim NewItem As New TreeNode
                        NewItem.Value = Drf("ModuloID").ToString + "_PerfilEspecial5"
                        NewItem.Text = Dr("SGFuncionEventoEsp5Nombre")
                        NewItem.ToolTip = "Evento"
                        NewItem.Checked = Permiso(sKey, Drf("ModuloPadreID"), Drf("ModuloID"), "PerfilEspecial5")
                        mnuNodeItem.ChildNodes.Add(NewItem)
                    End If

                    If Dr("SGFuncionEventoEsp6").ToString = "S" Then
                        Dim NewItem As New TreeNode
                        NewItem.Value = Drf("ModuloID").ToString + "_PerfilEspecial6"
                        NewItem.Text = Dr("SGFuncionEventoEsp6Nombre")
                        NewItem.ToolTip = "Evento"
                        NewItem.Checked = Permiso(sKey, Drf("ModuloPadreID"), Drf("ModuloID"), "PerfilEspecial6")
                        mnuNodeItem.ChildNodes.Add(NewItem)
                    End If

                    If Dr("SGFuncionEventoEsp7").ToString = "S" Then
                        Dim NewItem As New TreeNode
                        NewItem.Value = Drf("ModuloID").ToString + "_PerfilEspecial7"
                        NewItem.Text = Dr("SGFuncionEventoEsp7Nombre")
                        NewItem.ToolTip = "Evento"
                        NewItem.Checked = Permiso(sKey, Drf("ModuloPadreID"), Drf("ModuloID"), "PerfilEspecial7")
                        mnuNodeItem.ChildNodes.Add(NewItem)
                    End If

                    If Dr("SGFuncionEventoEsp8").ToString = "S" Then
                        Dim NewItem As New TreeNode
                        NewItem.Value = Drf("ModuloID").ToString + "_PerfilEspecial8"
                        NewItem.Text = Dr("SGFuncionEventoEsp8Nombre")
                        NewItem.ToolTip = "Evento"
                        NewItem.Checked = Permiso(sKey, Drf("ModuloPadreID"), Drf("ModuloID"), "PerfilEspecial8")
                        mnuNodeItem.ChildNodes.Add(NewItem)
                    End If

                    If Dr("SGFuncionEventoEsp9").ToString = "S" Then
                        Dim NewItem As New TreeNode
                        NewItem.Value = Drf("ModuloID").ToString + "_PerfilEspecial9"
                        NewItem.Text = Dr("SGFuncionEventoEsp9Nombre")
                        NewItem.ToolTip = "Evento"
                        NewItem.Checked = Permiso(sKey, Drf("ModuloPadreID"), Drf("ModuloID"), "PerfilEspecial9")
                        mnuNodeItem.ChildNodes.Add(NewItem)
                    End If

                    If Dr("SGFuncionEventoEsp10").ToString = "S" Then
                        Dim NewItem As New TreeNode
                        NewItem.Value = Drf("ModuloID").ToString + "_PerfilEspecial10"
                        NewItem.Text = Dr("SGFuncionEventoEsp10Nombre")
                        NewItem.ToolTip = "Evento"
                        NewItem.Checked = Permiso(sKey, Drf("ModuloPadreID"), Drf("ModuloID"), "PerfilEspecial10")
                        mnuNodeItem.ChildNodes.Add(NewItem)
                    End If

                End If


            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try

    End Sub

    Private Sub BarEventos1_MsgEvent(sAcción As String) Handles BarEventos1.MsgEvent
        Select Case sAcción
            Case "Nuevo"
                SaveArbolDetalle(TreeView1)
                LoadTreeView(txt_EstructuraMenuID.Text)
            Case "Eliminar"

            Case "Editar"

            Case "Filtrar"

            Case "Especial1"

            Case Else
        End Select
    End Sub


    Private Sub ChekUp(ByVal node As TreeNode)
        Try
            If node.Checked Then
                If Not node.Parent Is Nothing Then
                    node.Parent.Checked = True
                    ChekUp(node.Parent)
                End If
            End If

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

    Private Sub ChekDown(ByVal node As TreeNode)
        Try
            For Each onh As TreeNode In node.ChildNodes
                onh.Checked = node.Checked
                ChekDown(onh)
            Next
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

    Protected Sub TreeView1_SelectedNodeChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TreeView1.SelectedNodeChanged
        Dim Nodo As TreeNode = sender.SelectedNode()
        ChekUp(Nodo)
        ChekDown(Nodo)
    End Sub

    '=========================================================================================================================================
    ' Guarda cambios del perfil
    '=========================================================================================================================================

    Private Function InsDetallePfl(ByVal oCs As ColeccionPrmSql) As Boolean
        Dim oSQL As New SQLDetallesPerfil(oUsr)
        InsDetallePfl = False
        Try
            Return oSQL.ExecuteQry(oSQL.Ins_DetallePerfil, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Function

    Private Function UpdDetallePfl(ByVal oCs As ColeccionPrmSql) As Boolean
        Dim oSQL As New SQLDetallesPerfil(oUsr)
        UpdDetallePfl = False
        Try
            Return oSQL.ExecuteQry(oSQL.Upd_DetallePerfil, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Function

    Private Function DelDetallePfl(ByVal oCs As ColeccionPrmSql) As Boolean
        Dim oSQL As New SQLDetallesPerfil(oUsr)
        DelDetallePfl = False
        Try
            Return oSQL.ExecuteQry(oSQL.Del_DetallePerfil, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Function

    Private Sub SaveArbolDetalle(ByVal tv As TreeView)
        Dim oCs As New ColeccionPrmSql
        Try
            oCs.Create("@PerfilID", txt_PerfilID.Text)
            DelDetallePfl(oCs)
            For Each n As TreeNode In tv.Nodes
                LeerRecursive(n)
            Next
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

    Private Function LeerRecursive(ByVal n As TreeNode) As Boolean
        LeerRecursive = False
        Try
            For Each aNode As TreeNode In n.ChildNodes
                If aNode.ToolTip = "Función" Then
                    If aNode.Checked Then

                        Dim oCs As New ColeccionPrmSql
                        oCs.Create("@PerfilID", txt_PerfilID.Text)
                        oCs.Create("@ModuloID", aNode.Parent.Value)
                        oCs.Create("@SGFuncionID", aNode.Value)
                        oCs.Create("@PerfilConsultar", "S")
                        oCs.Create("@PerfilNuevo", "N")
                        oCs.Create("@PerfilEliminar", "N")
                        oCs.Create("@PerfilRecuperar", "N")
                        oCs.Create("@PerfilModificar", "N")
                        oCs.Create("@PerfilListar", "N")
                        oCs.Create("@PerfilExportar", "N")
                        oCs.Create("@PerfilGraficar", "N")
                        oCs.Create("@PerfilEspecial1", "N")
                        oCs.Create("@PerfilEspecial2", "N")
                        oCs.Create("@PerfilEspecial3", "N")
                        oCs.Create("@PerfilEspecial4", "N")
                        oCs.Create("@PerfilEspecial5", "N")
                        oCs.Create("@PerfilEspecial6", "N")
                        oCs.Create("@PerfilEspecial7", "N")
                        oCs.Create("@PerfilEspecial8", "N")
                        oCs.Create("@PerfilEspecial9", "N")
                        oCs.Create("@PerfilEspecial10", "N")

                        InsDetallePfl(oCs)

                        For Each nH As TreeNode In aNode.ChildNodes
                            If nH.Checked Then
                                Dim sItem As String = nH.Value.Replace(aNode.Value, "").Replace("_", "@")
                                oCs.ItemValue(sItem) = "S"
                            End If
                        Next
                        UpdDetallePfl(oCs)
                    End If
                Else
                    LeerRecursive(aNode)
                End If
            Next

            Return True

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Function
End Class