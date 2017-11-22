Imports Security_System
Imports DataAgro
Public Class BarFlete
    Inherits System.Web.UI.UserControl
    Dim oUsr As UserLogin
    Private Cs As New ColeccionPrmSql
    Private Dg As GridView
    Private Dt As DataTable
    Private IDF As Integer
    Private Ds As New DataSet
    Private sFiltro As String
    Private Const ABC = "FLETE"
    Public idNoticia As Integer
    Public teste As String

    Public Event MsgWork(ByVal sAcción As String, ByVal sMensaje As String, ByRef otabla As DataTable, ByVal Cs As ColeccionPrmSql)
    Private Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        oUsr = Session("Usr")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            SetFormConfig()
            loadCombos()
        End If
    End Sub


    '==============================================================================================================================
    'Inicialización 
    Private Sub SetFormConfig()

        txtkeyfle.Text = ""
        txtfecha.Text = ""
        DDLRUTA.SelectedIndex = -1
        DDLTIPOFLE.SelectedIndex = -1
        DDLTRANS.SelectedIndex = -1
        DDLCAMION.SelectedIndex = -1
        DDOPERADOR.SelectedIndex = -1
        txtobserv.Text = ""

        'RFVFECHA.Enabled = False
        'RFVRUTA.Enabled = False
        'RFVTIPOFLE.Enabled = False
        'RFVTRANS.Enabled = False
        'RFVCAMION.Enabled = False
        'RFVOPERADOR.Enabled = False

    End Sub


    Public Sub loadCombos()
        Dim oSql As New SQLRutas(oUsr)
        Dim lCs As New ColeccionPrmSql
        Try
            lCs.Create("@status", "E")
            lCs.Create("@keytpf", 0)
            lCs.Create("_Tabla", "RUTA")
            lCs.Create("_Qry", oSql.ListaForCombo)
            lCs.Create("_Order", "rut_desrut")
            lCs.Create("_DefaultKey", 0)
            lCs.Create("_DefaultDes", "[SELECCIONAR]")
            LoadCombo(oUsr, DDLRUTA, lCs)
            DDLRUTA.SelectedIndex = -1

            Dim oSqlT As New SQL_TipoFlete(oUsr)
            lCs.Create("@keyusu", oUsr.keyusu)
            lCs.ItemValue("@status") = oUsr.Mis.Status
            lCs.ItemValue("_Tabla") = "TIPOFLETE"
            lCs.ItemValue("_Qry") = oSqlT.ListaForCombo
            lCs.ItemValue("_Order") = "TipoFleteNombre"
            lCs.ItemValue("_Defaultkey") = 0
            lCs.ItemValue("_DefaultDes") = "[SELECCIONAR]"
            LoadCombo(oUsr, DDLTIPOFLE, lCs)
            DDLTIPOFLE.SelectedIndex = -1

            Dim oSqlTR As New SQL_Transportista(oUsr)
            lCs.ItemValue("@status") = "E"
            lCs.ItemValue("_Tabla") = "TRANSPORTISTA"
            lCs.ItemValue("_Qry") = oSqlTR.ListaForCombo
            lCs.ItemValue("_Order") = "TransportistaNombre"
            lCs.ItemValue("_Defaultkey") = 0
            lCs.ItemValue("_DefaultDes") = "[SELECCIONAR]"
            LoadCombo(oUsr, DDLTRANS, lCs)
            DDLTRANS.SelectedIndex = -1


            Dim oSqlC As New SQL_Camion(oUsr)
            lCs.ItemValue("@status") = "E"
            lCs.ItemValue("_Tabla") = "TRANSPORTISTACAMION"
            lCs.ItemValue("_Qry") = oSqlC.ListaForCombo
            lCs.ItemValue("_Order") = "cam_descam"
            lCs.ItemValue("_Defaultkey") = 0
            lCs.ItemValue("_DefaultDes") = "[SELECCIONAR]"
            LoadCombo(oUsr, DDLCAMION, lCs)
            DDLCAMION.SelectedIndex = -1

            Dim oSqlO As New SQL_Operador(oUsr)
            lCs.ItemValue("@status") = "E"
            lCs.ItemValue("_Tabla") = "TRANSPORTISTAOPERADOR"
            lCs.ItemValue("_Qry") = oSqlO.ListaForCombo
            lCs.ItemValue("_Order") = "ope_nombre"
            lCs.ItemValue("_Defaultkey") = 0
            lCs.ItemValue("_DefaultDes") = "[SELECCIONAR]"
            LoadCombo(oUsr, DDOPERADOR, lCs)
            DDOPERADOR.SelectedIndex = -1

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

    Private Function SaveFlete(ByRef Cs As ColeccionPrmSql) As DataTable
        Dim oSql As New SQLFletes(oUsr)
        'Dim oCs As New ColeccionPrmSql
        'SaveFlete = False
        SaveFlete = Nothing
        Try
            Cs.Create("@keyfle", txtkeyfle.Text)
            Dim oTb As DataTable = oSql._Item(oSql.Item, "FLETE", Cs)
            If Not oTb Is Nothing Then
                oTb.Columns("FleteID").Unique = True
                oTb.Columns("FleteID").AutoIncrement = True
                If oTb.Rows.Count = 0 Then
                    Dim Dr As DataRow = oTb.NewRow
                    Dr("FleteFecha") = txtfecha.Text
                    Dr("FleteFechaRegistro") = DateTime.Now.ToString()
                    Dr("RutaID") = DDLRUTA.SelectedValue
                    Dr("TipoFleteID") = DDLTIPOFLE.SelectedValue
                    Dr("TransportistaID") = DDLTRANS.SelectedValue
                    Dr("CamionID") = DDLCAMION.SelectedValue
                    Dr("OperadorID") = DDOPERADOR.SelectedValue
                    Dr("FleteUsuario") = oUsr.keyusu
                    Dr("FleteObservacion") = txtobserv.Text
                    oTb.Rows.Add(Dr)
                    Return oTb
                    'Return oSql.StatemenInsert(oTb)
                Else
                    Dim Dr As DataRow = oTb.Rows(0)
                    Dr("FleteID") = txtkeyfle.Text
                    Dr("FleteFecha") = txtfecha.Text
                    Dr("FleteFechaRegistro") = DateTime.Now.ToString()
                    Dr("RutaID") = DDLRUTA.SelectedValue
                    Dr("TipoFleteID") = DDLTIPOFLE.SelectedValue
                    Dr("TransportistaID") = DDLTRANS.SelectedValue
                    Dr("CamionID") = DDLCAMION.SelectedValue
                    Dr("OperadorID") = DDOPERADOR.SelectedValue
                    Dr("FleteUsuario") = oUsr.keyusu
                    Dr("FleteObservacion") = txtobserv.Text
                    oTb.Rows.Add(Dr)
                    Return oTb
                    'Return oSql.StatemenUpdate(oTb)
                End If
            End If

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function


    Private Function LimpiarControles() As String
        LimpiarControles = ""
        Try
            txtkeyfle.Text = ""
            txtfecha.Text = ""
            DDLRUTA.SelectedIndex = -1
            DDLTIPOFLE.SelectedIndex = -1
            DDLTRANS.SelectedIndex = -1
            DDLCAMION.SelectedIndex = -1
            DDOPERADOR.SelectedIndex = -1
            txtobserv.Text = ""
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Function


    Protected Sub imgbtnGuardar_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnGuardar.Click
        Dim Cs As New ColeccionPrmSql
        'Dim sGuardar As String = SaveFlete(oCs)
        Dim Datos As DataTable = SaveFlete(Cs)
        LimpiarControles()
        RaiseEvent MsgWork("Guardar", "", Datos, Cs)
    End Sub

    Protected Sub imgbtnCancelar_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnCancelar.Click
        Dim Cs As New ColeccionPrmSql
        Dim ds As New DataTable
        RaiseEvent MsgWork("Cancelar", LimpiarControles, ds, Cs)
    End Sub

    Protected Sub DDLTRANS_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDLTRANS.SelectedIndexChanged
        Dim oSqC As New SQL_Camion(oUsr)
        Dim lCs As New ColeccionPrmSql
        Try
            lCs.Create("@status", oUsr.Mis.Status)
            lCs.Create("@keytra", DDLTRANS.SelectedValue)
            lCs.Create("_Tabla", "TRANSPORTISTACAMION")
            lCs.Create("_Qry", oSqC.ListaComboByTra)
            lCs.Create("_Order", "cam_descam")
            lCs.Create("_DefaultKey", 0)
            lCs.Create("_DefaultDes", "[SELECCIONAR]")
            LoadCombo(oUsr, DDLCAMION, lCs)
            DDLCAMION.SelectedIndex = -1

            Dim oSqlO As New SQL_Operador(oUsr)
            lCs.ItemValue("@status") = oUsr.Mis.Status
            lCs.ItemValue("_Tabla") = "TRANSPORTISTAOPERADOR"
            lCs.ItemValue("_Qry") = oSqlO.ListaComboByTra
            lCs.ItemValue("_Order") = "ope_nombre"
            lCs.ItemValue("_Defaultkey") = 0
            lCs.ItemValue("_DefaultDes") = "[SELECCIONAR]"
            LoadCombo(oUsr, DDOPERADOR, lCs)
            DDOPERADOR.SelectedIndex = -1

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

    Protected Sub DDLTIPOFLE_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDLTIPOFLE.SelectedIndexChanged
        Dim oSqC As New SQL_Transportista(oUsr)
        Dim lCs As New ColeccionPrmSql
        Try
            lCs.Create("@status", oUsr.Mis.Status)
            lCs.Create("@keytpf", DDLTIPOFLE.SelectedValue)
            lCs.Create("_Tabla", "TRANSPORTISTA")
            lCs.Create("_Qry", oSqC.ListaCombo)
            lCs.Create("_Order", "TransportistaNombre")
            lCs.Create("_DefaultKey", 0)
            lCs.Create("_DefaultDes", "[SELECCIONAR]")
            LoadCombo(oUsr, DDLTRANS, lCs)
            DDLTRANS.SelectedIndex = -1

            Dim oSqlR As New SQLRutas(oUsr)
            lCs.ItemValue("_Tabla") = "RUTA"
            lCs.ItemValue("_Qry") = oSqlR.ListaForCombo
            lCs.ItemValue("_Order") = "rut_desrut"
            LoadCombo(oUsr, DDLRUTA, lCs)
            DDLRUTA.SelectedIndex = -1

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub




End Class