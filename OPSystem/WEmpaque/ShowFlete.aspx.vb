Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports Security_System
Imports System.IO
Imports System.Net

Public Class ShowFlete
    Inherits System.Web.UI.Page
    Private customerReport As ReportDocument
    Dim oUsr As UserLogin
    Dim oRp As InfoRp

    Private Sub Show_Init(sender As Object, e As EventArgs) Handles Me.Init
        oUsr = Session("Usr")
        If oUsr Is Nothing Then
            Context.GetOwinContext().Authentication.SignOut()
            Response.Redirect("~/Account/Login")
        End If
        oRp = Session("INFORP")
        ConfigureCrystalReports()
    End Sub

    Private Sub ConfigureCrystalReports()
        Try
            Select Case oRp.Reporte
                Case "FLETE"
                    customerReport = New CryFlete
                    Dim myDataSet As DataSet = oRp.OD
                    customerReport.SetDataSource(myDataSet)
                    If Not IsPostBack Then
                        customerReport.ExportToDisk(ExportFormatType.PortableDocFormat, oUsr.Mis.Log.Replace("Err.log", oRp.Nombre))
                        'customerReport.PrintToPrinter(1, False, 1, 1)
                        SendMail()
                        Session.Remove("INFORP")
                        Transferir_Archivo(oUsr.Mis.Log.Replace("Err.log", oRp.Nombre), oRp.Nombre)
                    End If
                Case Else

            End Select

            'CrystalReportViewer1.ReportSource = customerReport
            'Response.Redirect("Fletes.aspx")

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
            Session.Remove("INFORP")
        End Try
    End Sub

    Public Function SendMail() As Boolean
        Dim oSql As New SQLSetting(oUsr)
        Dim oCs As New ColeccionPrmSql
        Dim oInf As New InfoMail
        SendMail = False
        Try
            With oInf
                .User = oUsr
                .MailAttach.Add(oUsr.Mis.Log.Replace("Err.log", oRp.Nombre))
                .MailBody = "SE HA GENERADO UNA NUEVA ORDEN DE FLETE, REVISAR ARCHIVO ADJUNTO"
                .MailFrom = oRp.Parametros.ItemValue("_MAILFROM")
                .MailName = oRp.Nombre
                .MailSmtpCredenciales.Create("@username", "fletesajo@grupou.mx")
                .MailSmtpCredenciales.Create("@password", "8149pfegord$")
                .MailSmtpEnableSsl = False
                .MailSmtpHost = "mail.grupou.mx"
                .MailSmtpPort = 587
                Dim ArTo As List(Of String) = oRp.Parametros.ItemValue("_MAILTO")
                For Each sTo As String In ArTo
                    .MailTo.Add(sTo)
                Next
            End With
            Return Tools.EnvioMail(oInf)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    'Función para descargar archivos
    Protected Sub Transferir_Archivo(ByVal sArchivo As String, ByVal sFlName As String)
        'Si el archivo existe en disco …
        If IO.File.Exists(sArchivo) Then
            'Tamaño del buffer en bytes
            Const LONGITUD_BUFFER As Integer = 1024
            Dim DownloadStream As FileStream
            Dim Leidos, FileSize As Long
            Dim Buffer() As Byte = New Byte(LONGITUD_BUFFER) {}
            DownloadStream = File.OpenRead(sArchivo)
            FileSize = DownloadStream.Length
            Response.Buffer = False
            Response.ClearHeaders()
            Response.ClearContent()
            Response.ContentType = "application/octet-stream"
            Response.AddHeader("Content-Length", FileSize)
            Response.AddHeader("Content-Disposition", "attachment; filename=" + sFlName)
            Leidos = DownloadStream.Read(Buffer, 0, Buffer.Length)
            'Si lei bytes del archivo …
            While (Leidos > 0)
                'Y el cliente sigue conectado …
                If (Context.Response.IsClientConnected) Then
                    'Se le envian bytes
                    Context.Response.OutputStream.Write(Buffer, 0, Leidos)
                End If
                'Re-inicializo el buffer
                Array.Clear(Buffer, 0, Buffer.Length)
                Leidos = DownloadStream.Read(Buffer, 0, Buffer.Length)
            End While
            Context.Response.Flush()
            DownloadStream.Close()
            Response.End()
        Else
            Throw New Exception("Error: No se encontró el archivo.")
        End If
    End Sub

End Class