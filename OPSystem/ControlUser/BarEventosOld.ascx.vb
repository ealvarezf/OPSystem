Public Class BarEventosOld
    Inherits System.Web.UI.UserControl
    Public Event MsgEvent(ByVal sAcción As String)
    Public Property Nuevo() As Boolean
    Public Property Eliminar() As Boolean
    Public Property Editar() As Boolean
    Public Property Exportar() As Boolean
    Public Property Filtrar() As Boolean
    Public Property Listar() As Boolean
    Public Property Especial1 As Boolean
    Public Property Especial2 As Boolean
    Public Property Especial3 As Boolean
    Public Property Especial4 As Boolean
    Public Property Especial5 As Boolean
    Public Property ImageEsp1 As String
    Public Property ImageEsp2 As String
    Public Property ImageEsp3 As String
    Public Property ImageEsp4 As String
    Public Property ImageEsp5 As String
    Public Property DesEvEsp1 As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            SetImagen()
            btnNuevo.ToolTip = "Nuevo"
            btnNuevo.Visible = Nuevo
            btnEliminar.ToolTip = "Eliminar"
            btnEliminar.Visible = Eliminar
            btnEliminar.Attributes.Add("onclick", "return confirm('¿Realmente desea Eliminar el registro?')")
            btnEditar.ToolTip = "Editar"
            btnEditar.Visible = Editar
            btnExportar.Visible = Exportar
            btnFiltrar.Visible = Filtrar
            btnListar.Visible = Listar
            btnEsp1.ToolTip = "Cancelar"
            btnEsp1.Visible = Especial1
            btnEsp1.Attributes.Add("onclick", "return confirm('¿Realmente desea Cancelar la captura de Flete?')")
            btnEsp2.Visible = Especial2
        End If
    End Sub

    Private Sub SetImagen()
        If ImageEsp1 <> "" Then btnEsp1.ImageUrl = "~/Img/" & ImageEsp1
        If ImageEsp2 <> "" Then btnEsp2.ImageUrl = "~/Img/" & ImageEsp2
        If ImageEsp3 <> "" Then btnEsp2.ImageUrl = "~/Img/" & ImageEsp3
        If ImageEsp4 <> "" Then btnEsp2.ImageUrl = "~/Img/" & ImageEsp4
        If ImageEsp5 <> "" Then btnEsp2.ImageUrl = "~/Img/" & ImageEsp5
    End Sub

    Protected Sub btnNuevo_Click(sender As Object, e As ImageClickEventArgs) Handles btnNuevo.Click
        RaiseEvent MsgEvent("Nuevo")
    End Sub

    Protected Sub btnEliminar_Click(sender As Object, e As ImageClickEventArgs) Handles btnEliminar.Click
        RaiseEvent MsgEvent("Eliminar")
    End Sub

    Protected Sub btnEditar_Click(sender As Object, e As ImageClickEventArgs) Handles btnEditar.Click
        RaiseEvent MsgEvent("Editar")
    End Sub

    Protected Sub btnExportar_Click(sender As Object, e As ImageClickEventArgs) Handles btnExportar.Click
        RaiseEvent MsgEvent("Exportar")
    End Sub

    Protected Sub btnFiltrar_Click(sender As Object, e As ImageClickEventArgs) Handles btnFiltrar.Click
        RaiseEvent MsgEvent("Filtrar")
    End Sub

    Protected Sub btnListar_Click(sender As Object, e As ImageClickEventArgs) Handles btnListar.Click
        RaiseEvent MsgEvent("Listar")
    End Sub

    Private Sub btnEsp1_Click(sender As Object, e As ImageClickEventArgs) Handles btnEsp1.Click
        RaiseEvent MsgEvent("Especial1")
    End Sub

    Protected Sub btnEsp2_Click(sender As Object, e As ImageClickEventArgs) Handles btnEsp2.Click
        RaiseEvent MsgEvent("Especial2")
    End Sub

End Class