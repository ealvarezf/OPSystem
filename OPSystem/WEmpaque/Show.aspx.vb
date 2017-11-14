Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports Security_System
Imports System.IO
Imports System.Net

Public Class Show
    Inherits System.Web.UI.Page
    Private customerReport As ReportDocument
    Dim oUsr As UserLogin
    Dim oRp As InfoRp

    Private Sub Show_Init(sender As Object, e As EventArgs) Handles Me.Init
        oUsr = Session("Usr")
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
        Response.ContentType = "application/octet-stream"
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + sFlName)

        ' Write the file to the Response
        Const bufferLength As Integer = 10000
        Dim buffer As Byte() = New [Byte](bufferLength - 1) {}
        Dim length As Integer = 0
        Dim download As Stream = Nothing
        Try
            'Si el archivo existe en disco …
            If IO.File.Exists(sArchivo) Then
                download = New FileStream(sArchivo, FileMode.Open, FileAccess.Read)
                Do
                    If Response.IsClientConnected Then
                        length = download.Read(buffer, 0, bufferLength)
                        Response.OutputStream.Write(buffer, 0, length)
                        buffer = New [Byte](bufferLength - 1) {}
                    Else
                        length = -1
                    End If
                Loop While length > 0
                Response.Flush()
                Response.[End]()
            Else
                Throw New Exception("Error: No se encontró el archivo.")
            End If
        Catch ex As Exception

        Finally
            If download IsNot Nothing Then
                download.Close()
            End If
        End Try
    End Sub



End Class