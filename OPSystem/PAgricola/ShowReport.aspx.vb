Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports Security_System
Imports DataAgro

Public Class ShowReport
    Inherits System.Web.UI.Page
    Private customerReport As ReportDocument
    Private Ds As New DataSet
    Dim oUsr As UserLogin
    Dim oRp As InfoRp

    Private Sub ShowReport_Init(sender As Object, e As EventArgs) Handles Me.Init
        oUsr = Session("Usr")
        oRp = Session("INFORPT")
        ConfigureCrystalReports()
        'SetReportConfig()
        'ConfigureCrystalReports()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub ConfigureCrystalReports()
        Try
            Select Case oRp.Reporte
                Case "SALIDASEMILLA"
                    customerReport = New CrySalSemilla
                    With customerReport
                        '.DataDefinition.FormulaFields("Titulo").Text = "'" & oRp.Nombre & "'"
                        '.DataDefinition.FormulaFields("Fecha").Text = "'" & oRp.InfoFiltros & "'"
                    End With
                    Dim myDataSet As DataSet = oRp.OD
                    customerReport.SetDataSource(myDataSet)

                    'Case "PROGDISPONIBILIDAD"
                    '    customerReport = New CryProgDisponibilidad
                    '    With customerReport
                    '        '.DataDefinition.FormulaFields("Fecha").Text = "'" & oRp.Nombre & "'"
                    '        '.DataDefinition.FormulaFields("Fecha").Text = "'" & oRp.InfoFiltros & "'"
                    '        '.DataDefinition.FormulaFields("Fecha").Text = "'" & oRp.Fecha & "'"
                    '        '.DataDefinition.FormulaFields("NumPeriodo").Text = "'" & oRp.numPeriodo & "'"
                    '    End With
                    '    Dim myDataSet As DataSet = oRp.OD
                    '    customerReport.SetDataSource(myDataSet)

                Case Else

            End Select

            CrystalReportViewer1.ReportSource = customerReport

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)

        End Try
    End Sub

    Private Sub ConfigureCrystalReports(ByVal myDataSet As DataSet, ByVal Orienta As PaperOrientation)
        Try
            customerReport.SetDataSource(myDataSet)
            customerReport.PrintOptions.PaperSize = PaperSize.DefaultPaperSize
            customerReport.PrintOptions.PaperOrientation = Orienta
            CrystalReportViewer1.ReportSource = customerReport
            'ReportViewer1.ToolbarImagesFoldeUrl = "MisImagenesReportes/toolbar/"
            ' CrystalReportViewer1.ToolbarImagesFolderUrl = "images/images"

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub




End Class