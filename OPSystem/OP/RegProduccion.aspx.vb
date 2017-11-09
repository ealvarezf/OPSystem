Imports Security_System
Imports System.Data.SqlClient
Public Class RegProduccion
    Inherits System.Web.UI.Page
    Private Ds As New DataSet
    Private oUsr As UserLogin
    Private oCs As New ColeccionPrmSql
    Private keyord As Integer

    Private Sub RegProduccion_Init(sender As Object, e As EventArgs) Handles Me.Init
        oUsr = Session("Usr")
        keyord = Request.Params("key")
        LoadLista()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            SetFormConfig()
            'LoadLista()
        End If
    End Sub

    Private Sub SetFormConfig()
        'pnlEventos.Visible = True
        'pnlInfomaster.Visible = True
        'pnlAdd.Visible = False
        'pnlListar.Visible = True
        With BarEventos1
            .Pruebas = True
            .Nuevo.Boton.ToolTip = "Nuevo registro"
            .EtiquetaMaestro.Visible = True
            .EtiquetaMaestro.Text = "Orden de Producción: " & keyord.ToString
            .EtiquetaDetalle.Visible = True
            .EtiquetaDetalle.Text = "Registro de producción: "
        End With
        ''Valores predeterminados
        TestComportamientosGrid()
        lblInfo.Text = keyord

    End Sub
    Private Sub LoadLista()
        Dim oSql As New SQLDetalleProduccion(oUsr)
        Dim oCs As New ColeccionPrmSql
        Dim oPrm As Parameter
        Try
            oPrm = New Parameter("@OrdenID") With {.DefaultValue = lblInfo.Text}
            SqlDataSource1.SelectParameters.Add(oPrm)
            SqlDataSource1.SelectCommand = oSql.List
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

    Public Function TestComportamientosGrid() As Boolean
        TestComportamientosGrid = False
        Try
            With wdgProduccion.Behaviors
                .ColumnMoving.Enabled = True
                '.Activation.Enabled = True
            End With

            With wdgProduccion
                .DataKeyFields = "OrdenProduccionID"
            End With

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Function

    Private Sub wdgProduccion_PreRender(sender As Object, e As EventArgs) Handles wdgProduccion.PreRender

    End Sub
    Private Sub BarEventos1_MsgEvent(sAcción As String) Handles BarEventos1.MsgEvent
        Select Case sAcción
            Case "Nuevo"
                If ReplicarRegistro(lblInfo.Text) Then
                    wdgProduccion.DataBind()
                End If
            Case Else
        End Select
    End Sub

    Private Function ReplicarRegistro(ByVal iOrden As Integer) As Boolean
        Dim oSql As New SQLDetalleProduccion(oUsr)
        Dim oCs As New ColeccionPrmSql
        ReplicarRegistro = False
        Try
            oCs.Create("@OrdenID", iOrden)
            Return oSql.ExecuteStore("ProdReplicaRegistro", oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Function

End Class