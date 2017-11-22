Imports Security_System
Imports Sys_Empaque
Imports System.IO
Imports ClosedXML.Excel

Public Class ListadoFletesAjo
    Inherits Page
    Private Ds As New DataSet
    Private oUsr As New UserLogin

    Private Sub ListadoFletesAjo_Init(sender As Object, e As EventArgs) Handles Me.Init
        oUsr = Session("Usr")
        If oUsr Is Nothing Then
            Context.GetOwinContext().Authentication.SignOut()
            Response.Redirect("~/Account/Login")
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            SetFormConfig()
            LoadLista()
        End If
    End Sub

    Private Sub SetFormConfig()
        pnlEventos.Visible = True
        pnlFiltros.Visible = False
        pnlAdd.Visible = False
        pnlListar.Visible = True
        'With BarEventos1
        '    .Nuevo = False
        '    .Eliminar = False
        '    .Editar = False
        '    .Exportar = True
        '    .Filtrar = True
        '    .Listar = False
        'End With
        'Valores predeterminados
        'Tamaño de pagina predeterminado
        GridView1.PageSize = 100
    End Sub

    Private Sub LoadLista()
        Dim oSql As New SQLFletesAjo(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            If Not IsDate(txtSearch_fle_keyfec.Text) Then txtSearch_fle_keyfec.Text = "01/01/2017"
            oCs.Create("@fecini", CDate(txtSearch_fle_keyfec.Text))
            oCs.Create("@fecfin", Now())
            oCs.Create("@Proveedor", "%" & txtSearch_Proveedor.Text & "%")
            oCs.Create("@Rancho", "%" & txtSearch_Rancho.Text & "%")
            Dim oTabla As DataTable = oSql._List(oSql.List, "ESTRUCTURAS", oCs)
            LoadGrid(GridView1, oTabla)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

    Private Function TablaLista() As DataTable
        Dim oSql As New SQLFletesAjo(oUsr)
        Dim oCs As New ColeccionPrmSql
        TablaLista = Nothing
        Try
            If Not IsDate(txtSearch_fle_keyfec.Text) Then txtSearch_fle_keyfec.Text = "01/01/2017"
            oCs.Create("@fecini", CDate(txtSearch_fle_keyfec.Text))
            oCs.Create("@fecfin", Now())
            oCs.Create("@Proveedor", "%" & txtSearch_Proveedor.Text & "%")
            oCs.Create("@Rancho", "%" & txtSearch_Rancho.Text & "%")
            Dim oTabla As DataTable = oSql._List(oSql.List, "ESTRUCTURAS", oCs)
            oTabla.Columns("FleteID").Caption = "ID"
            oTabla.Columns("FleteFechaRegistro").Caption = "Fecha"
            oTabla.Columns("TipoFleteNombre").Caption = "Tipo"
            oTabla.Columns("Proveedor").Caption = "Proveedor"
            oTabla.Columns("Rancho").Caption = "Rancho"
            oTabla.Columns("UbicacionNombre").Caption = "Tabla"
            oTabla.Columns("CultivoNombre").Caption = "Cultivo"
            oTabla.Columns("VariedadNombre").Caption = "Variedad"
            oTabla.Columns("CosechaObservacion").Caption = "Observación"
            oTabla.Columns("UnidadEmpaqueNombre").Caption = "Envase"
            oTabla.Columns("CantEnviada").Caption = "Cantidad"
            oTabla.Columns("CantRecibida").Caption = "CantidadR"
            oTabla.Columns("ClasificaSizeNombre").Caption = "Calidad"
            oTabla.Columns("TransportistaNombre").Caption = "Transportista"
            oTabla.Columns("PBruto").Caption = "Bruto"
            oTabla.Columns("PTara").Caption = "Tara"
            oTabla.Columns("PEnvaseRecibido").Caption = "EnvasePeso"
            oTabla.Columns("Ticket").Caption = "Ticket"

            Return oTabla
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Function

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
                'SetFormEdit(sAcción, GridView1)
            Case "Eliminar"
                'SetFormEdit(sAcción, GridView1)
            Case "Editar"
                'SetFormEdit(sAcción, GridView1)
            Case "Filtrar"
                pnlFiltros.Visible = True
            Case "Exportar"
                'Así es como se utilizan los botones especiales en la barra de controles
                ExportClosedXML("FLETES")
            Case Else
        End Select
    End Sub

    Private Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        sender.PageIndex = e.NewPageIndex
        LoadLista()
    End Sub

    Private Function ExportClosedXML(ByVal sHoja As String) As Boolean
        ExportClosedXML = False
        Try
            Using dt As DataTable = TablaLista()
                Using wb As New XLWorkbook()
                    wb.Worksheets.Add(dt, sHoja)
                    Response.Clear()
                    Response.Buffer = True
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;filename=SqlExport.xlsx")
                    Using MyMemoryStream As New MemoryStream()
                        wb.SaveAs(MyMemoryStream)
                        MyMemoryStream.WriteTo(Response.OutputStream)
                        Response.Flush()
                        Response.End()
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function
End Class