Imports Security_System
Imports Sys_Empaque

Public Class RecepcionBascula
    Inherits Page
    Private Ds As DataSet
    Private oUsr As UserLogin
    Private oCs As New ColeccionPrmSql

    Private Sub RecepcionBascula_Init(sender As Object, e As EventArgs) Handles Me.Init
        oUsr = Session("Usr")
        If oUsr Is Nothing Then
            Context.GetOwinContext().Authentication.SignOut()
            Response.Redirect("~/Account/Login")
        End If
        Session.Add("Proceso", 1)
        Session.Add("Folios", 5)
    End Sub

    Private Sub RecepcionBascula_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Me.Form.Attributes.Add("autocomplete", "off")
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
        'With BarEventos1
        '    .Nuevo = True
        '    .Eliminar = True
        '    .Editar = True
        '    .Exportar = False
        '    .Filtrar = True
        '    .Listar = False
        'End With
        'Valores predeterminados
        'txt_ModuloID.Text = ""
        'txt_Modulo.Text = ""
        'txt_ModuloAyuda.Text = ""
        'txt_ModuloNombre.Text = ""
        'txt_ModuloID.MaxLength = 6
        'txt_Modulo.MaxLength = 12
        'txt_ModuloAyuda.MaxLength = 60
        'txt_ModuloNombre.MaxLength = 50
        'Tamaño de pagina predeterminado
        GridView1.PageSize = 10
    End Sub

    '================================================================================================================================
    'Acciones con el modelo de datos
    Private Sub LoadLista()
        Dim oSql As New SQLBascula(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            oCs.Create("@EmpIDF", txtSearch_emp_idf.Text & "%")
            If IsDate(txtSearch_emp_keyfec.Text) Then
                oCs.Create("@fecini", CDate(txtSearch_emp_keyfec.Text))
                oCs.Create("@fecfin", CDate(txtSearch_emp_keyfec.Text))
            Else
                Dim hora As String
                hora = DateTime.Now.ToString("dd/MM/yyyy")
                oCs.Create("@fecini", CDate("01/01/2000"))
                oCs.Create("@fecfin", CDate(hora))
            End If
            Dim oTabla As DataTable = oSql._List(oSql.List, "BASCULA", oCs)
            LoadGrid(GridView1, oTabla)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

    Private Sub imgBtnAplicaFiltro_Click(sender As Object, e As ImageClickEventArgs) Handles imgBtnAplicaFiltro.Click
        'pnlFiltros.Visible = False
        LoadLista()
        txtSearch_emp_idf.Text = ""
        txtSearch_emp_keyfec.Text = ""
        txtSearch_emp_keyfec.Enabled = True
    End Sub

    Private Sub imgbtnCancelaFiltro_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnCancelaFiltro.Click
        pnlFiltros.Visible = False
        txtSearch_emp_idf.Text = ""
        txtSearch_emp_keyfec.Text = ""
        LoadLista()
    End Sub

    Private Sub BarEventos1_MsgEvent(sAcción As String) Handles BarEventos1.MsgEvent
        Select Case sAcción
            Case "Nuevo"
                Dim nuevo As String = "AgregarBascula" + "?key=INS,0,0"
                Response.Redirect(nuevo)
            'Case "Eliminar"
            '    SetFormEdit(sAcción, GridView1)
            Case "Editar"
                SetFormEdit(sAcción, GridView1)
            Case "Eliminar"
                SetFormEdit(sAcción, GridView1)
            Case "Filtrar"
                pnlFiltros.Visible = True
            Case Else
        End Select
    End Sub

    Private Sub SetFormEdit(ByVal sAcc As String, ByVal oGrid As GridView)
        Try

            Select Case sAcc
                Case "Nuevo"
                    lblAcción.Text = sAcc
                    'Configuraciones iniciales

                Case "Editar"
                    Dim row As GridViewRow = oGrid.SelectedRow
                    'ImgBtnAceptar.Attributes.Add("onclick", "return confirm('Si incluye fecha de pago se generara nomina')")
                    Dim IDE As String = row.Cells(0).Text
                    Dim IDF As String = row.Cells(2).Text
                    Dim update As String = "AgregarBascula" + "?key=UPD," & IDE & "," & IDF
                    Response.Redirect(update)

                Case "Eliminar"
                    Dim row As GridViewRow = oGrid.SelectedRow
                    'ImgBtnAceptar.Attributes.Add("onclick", "return confirm('Si incluye fecha de pago se generara nomina')")
                    Dim IDE As String = row.Cells(0).Text
                    Dim IDF As String = row.Cells(2).Text
                    Dim update As String = "AgregarBascula" + "?key=DEL," & IDE & "," & IDF
                    Response.Redirect(update)
            End Select

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, GetType(Page), "ValidaSelect", "ValidaSelect();", True)
        End Try
    End Sub

    Private Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        sender.PageIndex = e.NewPageIndex
        LoadLista()
    End Sub

    Protected Sub txtSearch_emp_idf_TextChanged(sender As Object, e As EventArgs) Handles txtSearch_emp_idf.TextChanged
        LoadLista()
    End Sub

    Protected Sub txtSearch_emp_keyfec_TextChanged(sender As Object, e As EventArgs) Handles txtSearch_emp_keyfec.TextChanged
        LoadLista()
    End Sub

    Private Sub GridView1_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles GridView1.SelectedIndexChanging

    End Sub
End Class