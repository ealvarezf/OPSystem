Imports Security_System
Imports Sys_Empaque
Imports System.Drawing

Public Class AgregarBascula
    Inherits System.Web.UI.Page
    Private Ds As DataSet
    Private oUsr As UserLogin
    Private oCs As New ColeccionPrmSql
    Private iKey As String
    Private ArriKey() As String
    Dim GuiaID As TextBox
    Dim Ubicacion As DropDownList
    Dim variedad As DropDownList
    Dim envase As DropDownList
    Dim PesoEnvase As TextBox
    Dim Origen As TextBox
    Dim Cantidad As TextBox
    Dim PesoBruto As TextBox
    Dim TaraEnvase As TextBox
    Dim PesoNeto As TextBox
    Dim txtAdd As TextBox
    Dim ddlAdd As DropDownList
    Dim err As Integer = 0
    Dim Total As Double
    Dim FleteGlobal As Object


    Private Sub AgregarBascula_Init(sender As Object, e As EventArgs) Handles Me.Init
        oUsr = Session("Usr")

        'OBTIENE LOS PARAMETROS NECESARIOS PARA SABER SI ES INSERT, UPDATE O DELETE
        If Not Request.Params("Key") Is Nothing Then
            iKey = Request.Params("Key").ToString
            ArriKey = iKey.Split(",")
        End If
        If Not IsPostBack Then
            Me.Form.Attributes.Add("autocomplete", "off") 'EVITA QUE EL NAVEGADOR GUARDE DATOS

            'EN CASO DE QUE QUIERAN ACCEDER DIRECTAMENTE A LA PAGINA Y NO PROPORCIONEN VALORES
            'REDIRECCIONA
            If ArriKey Is Nothing Then
                Response.Redirect("~/WEmpaque/RecepcionBascula")
            End If

            Select Case ArriKey(0)
                Case "INS"
                    lbl_fecha_registro.Text = DateTime.Now
                    txt_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy")
                    lbl_idf.Text = Folio()
                    CargarDatos()
                    LoadLista()
                    If Not IsNumeric(FleteGlobal) Then
                        FleteGlobal = 0
                    End If
                    LoadListaGuia(FleteGlobal)
                    Try
                        For Each row As GridViewRow In GridView2.Rows
                            If row.RowType = DataControlRowType.DataRow Then
                                'Dim keycont As Integer = GridView1.DataKeys(row.RowIndex).Value
                                PesoEnvase = row.FindControl("txt_PesoEnvase")
                                Cantidad = row.FindControl("txt_Cantidad")
                                TaraEnvase = row.FindControl("txt_TaraEnvase")
                                PesoNeto = row.FindControl("txt_PesoNeto")
                                PesoBruto = row.FindControl("txt_PesoBruto")

                                PesoEnvase.Text = "0.00"
                                Cantidad.Text = "0.00"
                                TaraEnvase.Text = "0.00"
                                PesoNeto.Text = "0.00"
                                PesoBruto.Text = "0.00"
                            End If
                        Next
                    Catch ex As Exception
                        Tools.AddErrorLog(oUsr.Mis.Log, ex)
                    End Try

                Case "UPD"
                    lblFiltro.Text = "Actualizar Flete"
                    lblFiltro.BorderColor = Color.LightSkyBlue
                    lblFiltro.BorderStyle = BorderStyle.Solid
                    lbl_fecha_registro.Text = DateTime.Now
                    CargarDatosUpd(ArriKey(1), ArriKey(2))
                    LoadLista()
                    LoadListaGuia()

                Case "DEL"
                    lblFiltro.Text = "Eliminar Flete"
                    lblFiltro.BorderColor = Color.Red
                    lblFiltro.BorderStyle = BorderStyle.Solid
                    lbl_fecha_registro.Text = DateTime.Now
                    CargarDatosUpd(ArriKey(1), ArriKey(2))
                    LoadLista()
                    LoadListaGuia()
                    txt_fecha.Enabled = False
                    txt_peso_bruto.Enabled = False
                    txt_peso_tara.Enabled = False
            End Select
        End If
    End Sub

    Protected Sub txt_peso_tara_TextChanged(sender As Object, e As EventArgs) Handles txt_peso_tara.TextChanged
        If String.IsNullOrEmpty(txt_peso_tara.Text) Then
            txt_peso_tara.Text = "0.00"
        End If
        If IsNumeric(txt_peso_tara.Text) Then
            If CType(txt_peso_tara.Text, Double) > 0 Then
                lbl_peso_neto.Text = (CType(txt_peso_bruto.Text, Double) - CType(txt_peso_tara.Text, Double))
            Else
                txt_peso_tara.Text = "0.00"
                lbl_peso_neto.Text = (CType(txt_peso_bruto.Text, Double) - CType(txt_peso_tara.Text, Double))
            End If
        Else
            txt_peso_tara.Text = "0.00"
            lbl_peso_neto.Text = (CType(txt_peso_bruto.Text, Double) - CType(txt_peso_tara.Text, Double))
        End If
        ValidaPesoNeto()
        If CType(lbl_peso_neto.Text, Double) < 0 Or CType(lbl_peso_neto.Text, Double) < Total Then
            txt_peso_tara.Text = "0.00"
            lbl_peso_neto.Text = (CType(txt_peso_bruto.Text, Double) - CType(txt_peso_tara.Text, Double))
        End If
    End Sub

    Protected Sub txt_peso_bruto_TextChanged(sender As Object, e As EventArgs) Handles txt_peso_bruto.TextChanged
        If String.IsNullOrEmpty(txt_peso_bruto.Text) Then
            txt_peso_bruto.Text = "0.00"
        End If
        If IsNumeric(txt_peso_bruto.Text) Then
            If CType(txt_peso_bruto.Text, Double) > 0 Then
                lbl_peso_neto.Text = (CType(txt_peso_bruto.Text, Double) - CType(txt_peso_tara.Text, Double))
            Else
                txt_peso_bruto.Text = "0.00"
                lbl_peso_neto.Text = (CType(txt_peso_bruto.Text, Double) - CType(txt_peso_tara.Text, Double))
            End If
        Else
            txt_peso_bruto.Text = "0.00"
            lbl_peso_neto.Text = (CType(txt_peso_bruto.Text, Double) - CType(txt_peso_tara.Text, Double))
        End If
        ValidaPesoNeto()
        If CType(lbl_peso_neto.Text, Double) < 0 Or CType(lbl_peso_neto.Text, Double) < Total Then
            txt_peso_tara.Text = "0.00"
            txt_peso_bruto.Text = "0.00"
            lbl_peso_neto.Text = (CType(txt_peso_bruto.Text, Double) - CType(txt_peso_tara.Text, Double))
        End If
    End Sub

    'OBTIENE EL SIGUIENTE VALOR PARA IDF
    Private Function Folio() As Integer
        Dim oSql As New SQLCargarDatos(oUsr)
        Dim oCs As New ColeccionPrmSql
        Dim fol As Integer
        fol = Session("Folios")
        Folio = 0

        Try
            oCs.Create("@fol", fol)
            oCs.Create("_VALOR", "Folio")
            Return oSql._Valor(oSql.ItemFolio, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    'CARGA LOS DATOS PRINCIPALES AL INSERTAR
    Private Sub CargarDatos()
        Dim oSql As New SQLCargarDatos(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            oCs.Create("@keyproceso", Session("Proceso"))
            Dim oTb As DataTable = oSql._List(oSql.CargaDatos, "DATOS", oCs)
            If Not oTb Is Nothing Then
                If oTb.Rows.Count > 0 Then
                    Dim Dr As DataRow = oTb.Rows(0)
                    lbl_proceso_nombre.Text = Dr("ProcesoNombre")
                    lbl_proceso_id.Text = Session("Proceso")
                    lbl_empresa_id.Text = Dr("EmpresaID")
                    lbl_empresa_nombre.Text = Dr("EmpresaNombre")
                    'Session.Add("ubient", Dr("UbicacionDestinoID"))
                End If
            End If

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub

    'CARGA LOS DATOS PRINCIPALES AL ACTUALIZAR O ELIMINAR
    Private Sub CargarDatosUpd(ByVal IDE As Integer, ByVal IDF As Integer)
        Dim oSql As New SQLCargarDatos(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            oCs.Create("@IDE", IDE)
            oCs.Create("@IDF", IDF)
            Dim oTb As DataTable = oSql._List(oSql.CargaDatosUpd, "DATOSUPD", oCs)
            If Not oTb Is Nothing Then
                If oTb.Rows.Count > 0 Then
                    Dim Dr As DataRow = oTb.Rows(0)
                    lbl_proceso_nombre.Text = Dr("ProcesoNombre")
                    lbl_proceso_id.Text = Session("Proceso")
                    lbl_empresa_id.Text = Dr("EmpresaID")
                    lbl_empresa_nombre.Text = Dr("EmpresaNombre")
                    lbl_idf.Text = Dr("BasculaIDF")
                    Dim fec As String = Dr("BasculaFecha")
                    txt_fecha.Text = Convert.ToDateTime(fec).ToString("dd/MM/yyyy")
                    txt_peso_bruto.Text = Dr("BasculaPesoBruto")
                    txt_peso_tara.Text = Dr("BasculaPesoTara")
                    lbl_peso_neto.Text = Dr("PesoNeto")
                End If
            End If

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub

    'LLENA AUTOMATICAMENTE LOS GRIDS DE PARAMETROS, YA SEA CON TEXTBOX O CON DROP DOWN LIST
    Private Sub LoadLista()
        Dim oSql As New SQLCargarDatos(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            oCs.Create("@keyproceso", Session("Proceso"))
            Dim oTabla As DataTable = oSql._List(oSql.CargaParametros, "PARAMETROS", oCs)
            LoadGrid(GridView1, oTabla)
            Dim oTabla1 As DataTable = oSql._List(oSql.CargaParametrosLista, "PARAMETROS1", oCs)
            LoadGrid(GridView3, oTabla1)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

    'Protected Sub textbox_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
    'Dim Numcarr As String
    'Dim txt As TextBox = TryCast(sender, TextBox)
    'Dim row As GridViewRow = TryCast(sender.NamingContainer, GridViewRow)
    'Dim rowIndex As Integer = row.RowIndex
    'Dim imgb As TextBox = row.FindControl("txtAdd")
    '   Numcarr = imgb.Text
    '  labelcarrera.Text = Numcarr
    'If IsNumeric(Numcarr) And CInt(Val(Numcarr)) > 0 Then
    'If ExistCarrEnPtl(txtkey.Text, Numcarr) = "N" Then  ' COndicion que devuelve Si ya existe la carrera ingresada en la tabla detalle

    'If Save() Then  ' Guarda o Actualiza El plantel
    'If SaveCarrera(txtkey.Text, Numcarr) Then   'Guarda la carrera en el detalle
    '                   imgb.Visible = False
    '                  LoadListaCarreras(oCs)  ' Actualiza el Grid de carreras x detalle                       
    'End If
    'Else
    '               imgb.Text = ""
    '              imgb.Visible = True
    'End If

    'End If
    'Else
    '       Response.Write("<script>window.alert('La Carrera ingresada No es valida');</script>")
    '      imgb.Visible = True
    '     imgb.Text = ""
    'End If
    'End Sub

    Protected Sub imgBtnAplicaFiltro_Click(sender As Object, e As ImageClickEventArgs) Handles imgBtnAplicaFiltro.Click
        'BOTON PARA FINALIZAR LA OPERACIÓN
        'CONDICION PARA VALIDAR QUE NO HAYA VALORES EN CERO O SIN VALOR EN TODOS LOS FORMULARIOS
        If CType(txt_peso_bruto.Text, Double) = 0 Or CType(txt_peso_tara.Text, Double) = 0 Or ValidaDDL() Or ValidaTXT() Or ValidaGuia() Then
            ScriptManager.RegisterStartupScript(Me, GetType(Page), "ValidaForms", "ValidaForms();", True)
        Else
            Select Case ArriKey(0)
                Case "INS"
                    'INSERTA TODOS LOS DATOS, ACTUALIZA EL FOLIO Y EN CASO DE NO HABER ERROR
                    'REDIRECCIONA
                    If InsertGeneral() And InsertParametrosList() And InsertParametrosText() And InsertGuia() Then
                        UpdateFolio()
                        If InsertInventario() Then
                            Response.Redirect("RecepcionBascula")
                        Else
                            ScriptManager.RegisterStartupScript(Me, GetType(Page), "ErrorInsertInv", "ErrorInsertInv();", True)
                        End If
                    Else
                        ScriptManager.RegisterStartupScript(Me, GetType(Page), "ErrorInsert", "ErrorInsert();", True)
                    End If
                Case "UPD"
                    'ACTUALIZA LOS DATOS Y EN CASO DE NO HABER ERROR REDIRECCIONA
                    If UpdateGeneral() And UpdateParametrosList() And UpdateParametrosText() And DeleteGuia() And InsertGuia() Then
                        Response.Redirect("RecepcionBascula")
                    Else
                        ScriptManager.RegisterStartupScript(Me, GetType(Page), "ErrorUpdate", "ErrorUpdate();", True)
                    End If
                Case "DEL"
                    'ELIMINA LOS DATOS Y EN CASO DE NO HABER ERROR REDIRECCIONA
                    If InsertInventario() Then
                        If DeleteGuia() And DeleteParametros() And DeleteGeneral() Then
                            Response.Redirect("RecepcionBascula")
                        Else
                            ScriptManager.RegisterStartupScript(Me, GetType(Page), "ErrorDelete", "ErrorDelete();", True)
                        End If
                    Else
                        ScriptManager.RegisterStartupScript(Me, GetType(Page), "ErrorInsertInv", "ErrorInsertInv();", True)
                    End If
            End Select
        End If
    End Sub

    Protected Sub imgbtnCancelaFiltro_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnCancelaFiltro.Click
        Response.Redirect("RecepcionBascula")
    End Sub

    'Private Sub RecorrerRegistrosDet()
    '    Dim numreg As Integer = GridView1.Rows.Count
    '    Dim bander As Integer = 0
    '    Dim Lote As String = ""
    '    Dim Lote1 As String = ""
    '    Try
    '        For Each row As GridViewRow In GridView1.Rows
    '            If row.RowType = DataControlRowType.DataRow Then
    '                Dim keycont As Integer = GridView1.DataKeys(row.RowIndex).Value
    '                Dim valor As TextBox = row.FindControl("txtAdd")
    '                Dim keyprod As String = valor.Text
    '                Lote = Lote & keycont & " " & keyprod & "/" & vbLf
    '            End If
    '        Next
    '        For Each row As GridViewRow In GridView2.Rows
    '            If row.RowType = DataControlRowType.DataRow Then
    '                'Dim keycont As Integer = GridView1.DataKeys(row.RowIndex).Value
    '                GuiaID = row.FindControl("txt_GuiaID")
    '                If String.IsNullOrEmpty(GuiaID.Text) Then

    '                Else
    '                    Ubicacion = row.FindControl("ddl_ubicacion")
    '                    variedad = row.FindControl("ddl_variedad")
    '                    envase = row.FindControl("ddl_envase")
    '                    PesoEnvase = row.FindControl("txt_PesoEnvase")
    '                    Origen = row.FindControl("txt_Origen")
    '                    Cantidad = row.FindControl("txt_Cantidad")
    '                    PesoBruto = row.FindControl("txt_PesoBruto")
    '                    TaraEnvase = row.FindControl("txt_TaraEnvase")
    '                    PesoNeto = row.FindControl("txt_PesoNeto")
    '                    Lote1 = Lote1 & GuiaID.Text & "-" & Ubicacion.SelectedValue & "/" & Ubicacion.SelectedItem.Text & "-" &
    '                        variedad.SelectedValue & "/" & variedad.SelectedItem.Text & "-" &
    '                        envase.SelectedValue & "/" & envase.SelectedItem.Text & "-" &
    '                        PesoEnvase.Text & "-" & Origen.Text & "-" & Cantidad.Text & "-" &
    '                        PesoBruto.Text & "-" & TaraEnvase.Text & "-" & PesoNeto.Text
    '                End If
    '            End If
    '        Next
    '        Origen.Text = "7 HER-MARAVILLAS AJO TW CELAYENSE"
    '    Catch ex As Exception
    '        Tools.AddErrorLog(oUsr.Mis.Log, ex)
    '    End Try
    'End Sub

    Private Sub LoadListaGuia()
        Dim oSql As New SQLCargarDatos(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            oCs.Create("@id_empresa", lbl_empresa_id.Text)
            oCs.Create("@id_folio", lbl_idf.Text)
            Dim oTabla As DataTable = oSql._List(oSql.CargaGuia, "GUIA", oCs)
            LoadGrid(GridView2, oTabla)
            'GuiaUpd(GridView2)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

    Private Sub LoadListaGuia(ByVal flete As Object)
        Dim oSql As New SQLCargarDatos(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try
            'oCs.Create("@id_empresa", lbl_empresa_id.Text)
            'oCs.Create("@id_folio", lbl_idf.Text)
            oCs.Create("@fleteid", flete)
            Dim oTabla As DataTable = oSql._List(oSql.CargaGuiaFlete, "GUIA", oCs)
            LoadGrid(GridView2, oTabla)
            'GuiaUpd(GridView2)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try
    End Sub

    Private Sub GridView2_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GridView2.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim ddlUbicaciones As DropDownList = DirectCast(e.Row.FindControl("ddl_ubicacion"), DropDownList)
            Dim ddlVariedades As DropDownList = DirectCast(e.Row.FindControl("ddl_variedad"), DropDownList)
            Dim ddlEnvases As DropDownList = DirectCast(e.Row.FindControl("ddl_envase"), DropDownList)
            Dim oSql As New SQLFletes(oUsr)
            Dim lCsI As New ColeccionPrmSql
            Dim oSqlI As New SQLCargarDatos(oUsr)

            'lCsI.Create("@status", oUsr.Mis.Status)
            'lCsI.Create("@keyubi", keyubi)
            If ArriKey(0) = "UPD" Or ArriKey(0) = "DEL" Then
                FleteGlobal = ObtieneFlete()
            End If
            If IsNumeric(FleteGlobal) Then
                lCsI.Create("_Tabla", "UBICACIONES")
                lCsI.Create("_Qry", oSql.ListaOrigen)
                'lCsI.Create("_Order", "UbicacionNombre")
                'lCsI.Create("_Filtro", "TipoUbicaID = 'T'")
                lCsI.Create("_DefaultKey", "%")
                lCsI.Create("_DefaultDes", "[SELECCIONAR]")
                lCsI.Create("@proveedor", ObtieneOrigen(ObtieneRuta(FleteGlobal)))
                LoadCombo(oUsr, ddlUbicaciones, lCsI)
                ddlUbicaciones.SelectedIndex = -1
            End If

            lCsI.ItemValue("_Tabla") = "VARIEDAD"
            lCsI.ItemValue("_Qry") = oSqlI.ComboVariedades
            lCsI.ItemValue("_Order") = "VariedadNombre"
            LoadCombo(oUsr, ddlVariedades, lCsI)
            ddlVariedades.SelectedIndex = -1

            lCsI.ItemValue("_Tabla") = "ENVASES"
            lCsI.ItemValue("_Qry") = oSqlI.ComboEnvases
            lCsI.ItemValue("_Order") = "UnidadEmpaqueNombre"
            LoadCombo(oUsr, ddlEnvases, lCsI)
            ddlEnvases.SelectedIndex = -1

            GuiaID = e.Row.FindControl("txt_GuiaID")
            PesoEnvase = e.Row.FindControl("txt_PesoEnvase")
            Cantidad = e.Row.FindControl("txt_Cantidad")
            TaraEnvase = e.Row.FindControl("txt_TaraEnvase")
            PesoNeto = e.Row.FindControl("txt_PesoNeto")
            PesoBruto = e.Row.FindControl("txt_PesoBruto")
            Origen = e.Row.FindControl("txt_Origen")

            If Not String.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "GuiaID"))) Then
                GuiaID.Text = DataBinder.Eval(e.Row.DataItem, "GuiaID")
                GetIndex(ddlUbicaciones, DataBinder.Eval(e.Row.DataItem, "UbicacionID"))
                GetIndex(ddlVariedades, DataBinder.Eval(e.Row.DataItem, "VariedadID"))
                GetIndex(ddlEnvases, DataBinder.Eval(e.Row.DataItem, "EnvaseID"))
                PesoEnvase.Text = DataBinder.Eval(e.Row.DataItem, "EnvasePeso")
                Origen.Text = DataBinder.Eval(e.Row.DataItem, "Origen")
                Cantidad.Text = DataBinder.Eval(e.Row.DataItem, "GuiaCantidad")
                PesoBruto.Text = DataBinder.Eval(e.Row.DataItem, "GuiaPesoBruto")
                TaraEnvase.Text = DataBinder.Eval(e.Row.DataItem, "TaraEnvase")
                PesoNeto.Text = DataBinder.Eval(e.Row.DataItem, "PesoNeto")
            Else
                PesoEnvase.Text = "0.00"
                Cantidad.Text = "0"
                PesoBruto.Text = "0.00"
                TaraEnvase.Text = "0.00"
                PesoNeto.Text = "0.00"
            End If

            If ArriKey(0) = "DEL" Then
                ddlUbicaciones.Enabled = False
                ddlVariedades.Enabled = False
                ddlEnvases.Enabled = False
                Cantidad.Enabled = False
                PesoBruto.Enabled = False
            End If
        End If
    End Sub

    Protected Sub txt_Cantidad_textchanged(sender As Object, e As EventArgs)
        Total = 0
        For Each row As GridViewRow In GridView2.Rows
            If row.RowType = DataControlRowType.DataRow Then
                'Dim keycont As Integer = GridView1.DataKeys(row.RowIndex).Value
                GuiaID = row.FindControl("txt_GuiaID")

                PesoEnvase = row.FindControl("txt_PesoEnvase")
                Cantidad = row.FindControl("txt_Cantidad")
                TaraEnvase = row.FindControl("txt_TaraEnvase")
                PesoNeto = row.FindControl("txt_PesoNeto")
                PesoBruto = row.FindControl("txt_PesoBruto")
                Try
                    If String.IsNullOrEmpty(PesoBruto.Text) Then
                        PesoBruto.Text = "0.00"
                    End If
                    If String.IsNullOrEmpty(Cantidad.Text) Then
                        Cantidad.Text = "0.00"
                    End If
                    If IsNumeric(Cantidad.Text) And IsNumeric(PesoBruto.Text) Then
                        If CType(Cantidad.Text, Double) >= 0 And CType(PesoBruto.Text, Double) >= 0 Then
                            TaraEnvase.Text = (CType(Cantidad.Text, Integer) * CType(PesoEnvase.Text, Double))
                            PesoNeto.Text = (CType(PesoBruto.Text, Double) - CType(TaraEnvase.Text, Double))
                            Total = Total + CType(PesoNeto.Text, Double)
                        Else
                            Cantidad.Text = "0.00"
                            PesoBruto.Text = "0.00"
                            TaraEnvase.Text = "0.00"
                            PesoNeto.Text = "0.00"
                        End If
                        If (CType(lbl_peso_neto.Text, Double) < Total) Or (CType(PesoNeto.Text, Double) < 0) Then
                            PesoBruto.Text = "0.00"
                            PesoBruto.BorderColor = Color.Red
                            PesoBruto.ForeColor = Color.Red
                            PesoNeto.Text = (CType(PesoBruto.Text, Double) - CType(TaraEnvase.Text, Double))
                        Else
                            PesoBruto.BorderColor = Color.Black
                            PesoBruto.ForeColor = Color.Black
                        End If
                    Else
                        Cantidad.Text = "0.00"
                        PesoBruto.Text = "0.00"
                        TaraEnvase.Text = "0.00"
                        PesoNeto.Text = "0.00"
                    End If
                Catch ex As Exception
                    Tools.AddErrorLog(oUsr.Mis.Log, ex)
                End Try
            End If
        Next
    End Sub

    Protected Sub ddl_envase_Changed(sender As Object, e As EventArgs)
        Try
            Total = 0
            For Each row As GridViewRow In GridView2.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    'Dim keycont As Integer = GridView1.DataKeys(row.RowIndex).Value
                    GuiaID = row.FindControl("txt_GuiaID")
                    envase = row.FindControl("ddl_envase")
                    PesoEnvase = row.FindControl("txt_PesoEnvase")
                    TaraEnvase = row.FindControl("txt_TaraEnvase")
                    PesoNeto = row.FindControl("txt_PesoNeto")
                    PesoBruto = row.FindControl("txt_PesoBruto")
                    Cantidad = row.FindControl("txt_Cantidad")
                    Dim Id As Integer
                    Id = envase.SelectedValue
                    PesoEnvase.Text = PesoCajon(Id)
                    TaraEnvase.Text = CType(PesoEnvase.Text, Double) * CType(Cantidad.Text, Double)
                    PesoNeto.Text = CType(PesoBruto.Text, Double) - CType(TaraEnvase.Text, Double)
                    Total = Total + CType(PesoNeto.Text, Double)
                    If (CType(lbl_peso_neto.Text, Double) < Total) Or (CType(PesoNeto.Text, Double) < 0) Then
                        PesoBruto.Text = "0.00"
                        PesoBruto.BorderColor = Color.Red
                        PesoBruto.ForeColor = Color.Red
                        PesoNeto.Text = (CType(PesoBruto.Text, Double) - CType(TaraEnvase.Text, Double))
                    Else
                        PesoBruto.BorderColor = Color.Black
                        PesoBruto.ForeColor = Color.Black
                    End If
                End If
                'ValidaGuiaID()
            Next
        Catch ex As Exception
            PesoEnvase.Text = "0.00"
            TaraEnvase.Text = "0.00"
            PesoNeto.Text = PesoBruto.Text
            'ValidaGuiaID()
        End Try
    End Sub

    Private Function PesoCajon(ID As Integer) As String
        Dim oSql As New SQLCargarDatos(oUsr)
        Dim oCs As New ColeccionPrmSql
        PesoCajon = ""

        Try
            oCs.Create("@EnvaseID", ID)
            oCs.Create("_VALOR", "EnvasePeso")
            Return oSql._Valor(oSql.PesoEnv, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Protected Sub ddl_variedad_Changed(sender As Object, e As EventArgs)
        Try
            For Each row As GridViewRow In GridView2.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    'Dim keycont As Integer = GridView1.DataKeys(row.RowIndex).Value
                    GuiaID = row.FindControl("txt_GuiaID")
                    Ubicacion = row.FindControl("ddl_ubicacion")
                    variedad = row.FindControl("ddl_variedad")
                    Origen = row.FindControl("txt_Origen")
                    Dim TestComp As Integer
                    Dim IdC As Object
                    IdC = variedad.SelectedValue
                    TestComp = StrComp(Convert.ToString(IdC), "%", CompareMethod.Text)
                    If Not TestComp = 0 Then
                        Origen.Text = Ubicacion.SelectedItem.Text & " " & Cultivo(IdC) & " " & variedad.SelectedItem.Text
                    End If
                    'ValidaGuiaID()
                End If
            Next
        Catch ex As Exception
            Origen.Text = ""
            'ValidaGuiaID()
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub

    Protected Sub ddl_ubicacion_Changed(sender As Object, e As EventArgs)
        Try
            For Each row As GridViewRow In GridView2.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    'Dim keycont As Integer = GridView1.DataKeys(row.RowIndex).Value
                    GuiaID = row.FindControl("txt_GuiaID")
                    Ubicacion = row.FindControl("ddl_ubicacion")
                    variedad = row.FindControl("ddl_variedad")
                    Origen = row.FindControl("txt_Origen")
                    Dim TestComp As Integer
                    Dim IdC As Object
                    IdC = variedad.SelectedValue
                    TestComp = StrComp(Convert.ToString(IdC), "%", CompareMethod.Text)
                    If Not TestComp = 0 Then
                        Origen.Text = Ubicacion.SelectedItem.Text & " " & Cultivo(IdC) & " " & variedad.SelectedItem.Text
                    End If
                    'ValidaGuiaID()
                End If
            Next
        Catch ex As Exception
            Origen.Text = ""
            'ValidaGuiaID()
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub

    Private Function Cultivo(ID As Integer) As String
        Dim oSql As New SQLCargarDatos(oUsr)
        Dim oCs As New ColeccionPrmSql
        Cultivo = ""

        Try
            oCs.Create("@VariedadID", ID)
            oCs.Create("_VALOR", "CultivoNombre")
            Return oSql._Valor(oSql.CultivoNom, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Protected Sub txt_Folio_Changed(sender As Object, e As EventArgs)
        For Each row As GridViewRow In GridView1.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim key As Integer = GridView1.DataKeys(row.RowIndex).Value
                txtAdd = row.FindControl("txtAdd")
                If key = 1 Then
                    If Not IsNumeric(txtAdd.Text) Then
                        txtAdd.Text = "0"
                    End If
                    If CType(txtAdd.Text, Double) < 0 Then
                        txtAdd.Text = "0"
                    End If
                End If
            End If
        Next
    End Sub

    Private Sub GridView3_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView3.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            ddlAdd = DirectCast(e.Row.FindControl("ddlAdd"), DropDownList)
            Dim oSqlI As New SQLCargarDatos(oUsr)
            Dim lCsI As New ColeccionPrmSql
            Dim keyubi As String = ""
            Dim cs As New ColeccionPrmSql
            Dim idParametro As Integer = e.Row.Cells(0).Text

            'lCsI.Create("@status", oUsr.Mis.Status)
            'lCsI.Create("@keyubi", keyubi)

            Select Case ArriKey(0)
                Case "INS"
                    If idParametro = 1 Then
                        lCsI.Create("_Tabla", "FLETEPENDIENTE")
                        lCsI.Create("_Qry", oSqlI.ComboFletes)
                        lCsI.Create("_Order", "FleteID")
                        lCsI.Create("_DefaultKey", "%")
                        lCsI.Create("_DefaultDes", "[SELECCIONAR]")
                        LoadCombo(oUsr, ddlAdd, lCsI)
                        'lCsI = Nothing
                    End If
                    ddlAdd.SelectedIndex = -1
                Case Else
                    'Dim id As Integer = e.Row.Cells(0).Text
                    Try
                        If idParametro = 1 Then
                            lCsI.Create("_Tabla", "FLETEUPDATE")
                            lCsI.Create("_Qry", oSqlI.ComboFletesUpd)
                            lCsI.Create("_Order", "ParametroEntrada")
                            lCsI.Create("_DefaultKey", "%")
                            lCsI.Create("_DefaultDes", "[SELECCIONAR]")
                            lCsI.Create("@IDF", ArriKey(2))
                            LoadCombo(oUsr, ddlAdd, lCsI)
                            GetIndex(ddlAdd, ObtieneFlete())
                            ddlAdd.Enabled = False
                        End If
                        If idParametro = 2 Then
                            lCsI.Create("_Tabla", "OPERADORUPDATE")
                            lCsI.Create("_Qry", oSqlI.ComboOperadorUpd)
                            lCsI.Create("_Order", "ParametroEntrada")
                            lCsI.Create("_DefaultKey", "%")
                            lCsI.Create("_DefaultDes", "[SELECCIONAR]")
                            lCsI.Create("@IDF", ArriKey(2))
                            LoadCombo(oUsr, ddlAdd, lCsI)
                            GetIndex(ddlAdd, ObtieneOperador())
                            ddlAdd.Enabled = False
                        End If
                    Catch ex As Exception
                        Tools.AddErrorLog(oUsr.Mis.Log, ex)
                    End Try
            End Select

        End If
    End Sub

    Private Sub txt_fecha_TextChanged(sender As Object, e As EventArgs) Handles txt_fecha.TextChanged
        If Not IsDate(txt_fecha.Text) Then
            txt_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy")
        End If
    End Sub

    Private Function ValidaDDL() As Boolean
        ValidaDDL = False
        Dim TestComp As Integer
        Try
            For Each row As GridViewRow In GridView3.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    'Dim keycont As Integer = GridView1.DataKeys(row.RowIndex).Value
                    ddlAdd = row.FindControl("ddlAdd")
                    Dim keyprod As String = ddlAdd.SelectedValue
                    TestComp = StrComp(keyprod, "%", CompareMethod.Text)
                    If TestComp = 0 Then
                        ValidaDDL = True
                    End If
                End If
            Next
            Return ValidaDDL
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Private Function ValidaTXT() As Boolean
        ValidaTXT = False
        Try
            For Each row As GridViewRow In GridView1.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    'Dim keycont As Integer = GridView1.DataKeys(row.RowIndex).Value
                    txtAdd = row.FindControl("txtAdd")
                    Dim keyprod As String = txtAdd.Text
                    If String.IsNullOrEmpty(keyprod) Then
                        ValidaTXT = True
                    End If
                End If
            Next
            Return ValidaTXT
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Private Function ValidaGuia() As Boolean
        ValidaGuia = False
        Dim Ubica As Integer
        Dim Varie As Integer
        Dim Env As Integer
        Try
            For Each row As GridViewRow In GridView2.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    'Dim keycont As Integer = GridView1.DataKeys(row.RowIndex).Value
                    GuiaID = row.FindControl("txt_GuiaID")
                    Ubicacion = row.FindControl("ddl_ubicacion")
                    variedad = row.FindControl("ddl_variedad")
                    envase = row.FindControl("ddl_envase")
                    Cantidad = row.FindControl("txt_Cantidad")
                    PesoBruto = row.FindControl("txt_PesoBruto")


                    If String.IsNullOrEmpty(GuiaID.Text) Then

                    Else
                        Ubica = StrComp(Ubicacion.SelectedValue, "%", CompareMethod.Text)
                        Varie = StrComp(variedad.SelectedValue, "%", CompareMethod.Text)
                        Env = StrComp(envase.SelectedValue, "%", CompareMethod.Text)
                        If CType(Cantidad.Text, Integer) = 0 Or CType(PesoBruto.Text, Double) = 0 Or Ubica = 0 Or Varie = 0 Or Env = 0 Then
                            ValidaGuia = True
                        End If
                    End If
                End If
            Next
            Return ValidaGuia
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Private Function InsertGeneral() As Boolean
        Dim oSQL As New SQLInsertarDatos(oUsr)
        Dim cs As New ColeccionPrmSql
        InsertGeneral = False
        Try
            cs.Create("@IDEmp", lbl_empresa_id.Text)
            cs.Create("@IDF", lbl_idf.Text)
            cs.Create("@Fecha", CDate(txt_fecha.Text))
            cs.Create("@PBruto", txt_peso_bruto.Text)
            cs.Create("@PTara", txt_peso_tara.Text)
            cs.Create("@FecReg", CDate(lbl_fecha_registro.Text))
            cs.Create("@ProcesoID", lbl_proceso_id.Text)
            Return oSQL.ExecuteQry(oSQL.InsGeneral, cs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Private Function InsertParametrosList() As Boolean
        Dim oSQL As New SQLInsertarDatos(oUsr)
        Dim cs As New ColeccionPrmSql
        InsertParametrosList = False
        Try
            For Each row As GridViewRow In GridView3.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    'Dim keycont As Integer = GridView3.DataKeys(row.RowIndex).Value
                    Dim cs1 As New ColeccionPrmSql
                    Dim ID As String = row.Cells(0).Text
                    ddlAdd = row.FindControl("ddlAdd")
                    Dim keyprod As String = ddlAdd.SelectedValue

                    cs.Create("@IDEmp", lbl_empresa_id.Text)
                    cs.Create("@IDF", lbl_idf.Text)
                    cs.Create("@PID", ID)
                    cs.Create("@PEntrada", keyprod)
                    oSQL.ExecuteQry(oSQL.InsParametro, cs)
                    cs = cs1
                End If
            Next
            Return True
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Private Function InsertParametrosText() As Boolean
        Dim oSQL As New SQLInsertarDatos(oUsr)
        Dim cs As New ColeccionPrmSql
        InsertParametrosText = False
        Try
            For Each row As GridViewRow In GridView1.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    'Dim keycont As Integer = GridView3.DataKeys(row.RowIndex).Value
                    Dim cs1 As New ColeccionPrmSql
                    Dim ID As String = row.Cells(0).Text
                    txtAdd = row.FindControl("txtAdd")
                    Dim keyprod As String = txtAdd.Text

                    cs.Create("@IDEmp", lbl_empresa_id.Text)
                    cs.Create("@IDF", lbl_idf.Text)
                    cs.Create("@PID", ID)
                    cs.Create("@PEntrada", keyprod)
                    oSQL.ExecuteQry(oSQL.InsParametro, cs)
                    cs = cs1
                End If
            Next
            Return True
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Private Function InsertGuia() As Boolean
        Dim oSQL As New SQLInsertarDatos(oUsr)
        Dim cs As New ColeccionPrmSql
        InsertGuia = False
        Try
            For Each row As GridViewRow In GridView2.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    'Dim keycont As Integer = GridView3.DataKeys(row.RowIndex).Value
                    Dim cs1 As New ColeccionPrmSql
                    GuiaID = row.FindControl("txt_GuiaID")
                    Ubicacion = row.FindControl("ddl_ubicacion")
                    variedad = row.FindControl("ddl_variedad")
                    envase = row.FindControl("ddl_envase")
                    Cantidad = row.FindControl("txt_Cantidad")
                    PesoBruto = row.FindControl("txt_PesoBruto")

                    If String.IsNullOrEmpty(GuiaID.Text) Then

                    Else
                        cs.Create("@IDF", lbl_idf.Text)
                        cs.Create("@IDEmp", lbl_empresa_id.Text)
                        cs.Create("@GuiaID", GuiaID.Text)
                        cs.Create("@UbicaID", Ubicacion.SelectedValue)
                        'cs.Create("@UbicaID", Session("ubient"))
                        cs.Create("@VarID", variedad.SelectedValue)
                        cs.Create("@EnvID", envase.SelectedValue)
                        cs.Create("@Cantidad", Cantidad.Text)
                        cs.Create("@PBruto", PesoBruto.Text)
                        oSQL.ExecuteQry(oSQL.InsGuia, cs)
                        cs = cs1
                    End If
                End If
            Next
            Return True
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Private Sub UpdateFolio()
        Dim oSQL As New SQLInsertarDatos(oUsr)
        Dim cs As New ColeccionPrmSql
        Dim fol As Integer
        fol = Session("Folios")
        Try
            cs.Create("@IDF", lbl_idf.Text)
            cs.Create("@FolID", fol)
            oSQL.ExecuteQry(oSQL.UpdFolio, cs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub

    'Private Sub ValidaGuiaID1(ByVal Max As Integer)
    '    Dim Ubica As Integer
    '    Dim Varie As Integer
    '    Dim Env As Integer

    '    Try
    '        For Each row As GridViewRow In GridView2.Rows
    '            If row.RowType = DataControlRowType.DataRow Then
    '                'Dim keycont As Integer = GridView1.DataKeys(row.RowIndex).Value
    '                GuiaID = row.FindControl("txt_GuiaID")
    '                Ubicacion = row.FindControl("ddl_ubicacion")
    '                variedad = row.FindControl("ddl_variedad")
    '                envase = row.FindControl("ddl_envase")
    '                Cantidad = row.FindControl("txt_Cantidad")
    '                PesoBruto = row.FindControl("txt_PesoBruto")
    '                TaraEnvase = row.FindControl("txt_TaraEnvase")
    '                PesoNeto = row.FindControl("txt_PesoNeto")


    '                Ubica = StrComp(Ubicacion.SelectedValue, "%", CompareMethod.Text)
    '                Varie = StrComp(variedad.SelectedValue, "%", CompareMethod.Text)
    '                Env = StrComp(envase.SelectedValue, "%", CompareMethod.Text)
    '                If CType(Cantidad.Text, Integer) = 0 Or CType(PesoBruto.Text, Double) = 0 Or Ubica = 0 Or Varie = 0 Or Env = 0 Then
    '                    If Not String.IsNullOrEmpty(GuiaID.Text) Then
    '                        GuiaID.Text = ""
    '                        Cantidad.Text = "0"
    '                        PesoBruto.Text = "0.00"
    '                        TaraEnvase.Text = "0.00"
    '                        PesoNeto.Text = "0.00"
    '                    End If
    '                Else
    '                    If String.IsNullOrEmpty(GuiaID.Text) Then
    '                        GuiaID.Text = Max
    '                    End If
    '                End If
    '            End If
    '        Next
    '    Catch ex As Exception
    '        Tools.AddErrorLog(oUsr.Mis.Log, ex)
    '    End Try
    'End Sub

    Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            txtAdd = DirectCast(e.Row.FindControl("txtAdd"), TextBox)
            Dim oSqlI As New SQLCargarDatos(oUsr)
            Dim lCsI As New ColeccionPrmSql
            Dim cs As New ColeccionPrmSql

            Select Case ArriKey(0)
                Case "UPD"
                    Dim id As Integer = e.Row.Cells(0).Text
                    Try
                        Dim cs1 As New ColeccionPrmSql
                        cs.Create("@IDE", lbl_empresa_id.Text)
                        cs.Create("@IDF", lbl_idf.Text)
                        cs.Create("@IDP", id)
                        cs.Create("_VALOR", "ParametroEntrada")
                        txtAdd.Text = oSqlI._Valor(oSqlI.ValddlAdd, cs)
                        cs = cs1
                    Catch ex As Exception
                        Tools.AddErrorLog(oUsr.Mis.Log, ex)
                    End Try
                Case "DEL"
                    Dim id As Integer = e.Row.Cells(0).Text
                    Try
                        Dim cs1 As New ColeccionPrmSql
                        cs.Create("@IDE", lbl_empresa_id.Text)
                        cs.Create("@IDF", lbl_idf.Text)
                        cs.Create("@IDP", id)
                        cs.Create("_VALOR", "ParametroEntrada")
                        txtAdd.Text = oSqlI._Valor(oSqlI.ValddlAdd, cs)
                        cs = cs1
                        txtAdd.Enabled = False
                    Catch ex As Exception
                        Tools.AddErrorLog(oUsr.Mis.Log, ex)
                    End Try
            End Select

        End If
    End Sub

    Private Sub ValidaPesoNeto()
        Total = 0
        For Each row As GridViewRow In GridView2.Rows
            If row.RowType = DataControlRowType.DataRow Then
                'Dim keycont As Integer = GridView1.DataKeys(row.RowIndex).Value
                PesoNeto = row.FindControl("txt_PesoNeto")
                Try
                    Total = Total + CType(PesoNeto.Text, Double)
                Catch ex As Exception
                    Tools.AddErrorLog(oUsr.Mis.Log, ex)
                End Try
            End If
        Next
    End Sub

    Private Function UpdateGeneral() As Boolean
        Dim oSQL As New SQLActualizarDatos(oUsr)
        Dim cs As New ColeccionPrmSql
        UpdateGeneral = False
        Try
            cs.Create("@IDEmp", lbl_empresa_id.Text)
            cs.Create("@IDF", lbl_idf.Text)
            cs.Create("@Fecha", CDate(txt_fecha.Text))
            cs.Create("@PBruto", txt_peso_bruto.Text)
            cs.Create("@PTara", txt_peso_tara.Text)
            cs.Create("@FecReg", CDate(lbl_fecha_registro.Text))
            Return oSQL.ExecuteQry(oSQL.UpdGeneral, cs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Private Function UpdateParametrosList() As Boolean
        Dim oSQL As New SQLActualizarDatos(oUsr)
        Dim cs As New ColeccionPrmSql
        UpdateParametrosList = False
        Try
            For Each row As GridViewRow In GridView3.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    'Dim keycont As Integer = GridView3.DataKeys(row.RowIndex).Value
                    Dim cs1 As New ColeccionPrmSql
                    Dim ID As String = row.Cells(0).Text
                    ddlAdd = row.FindControl("ddlAdd")
                    Dim keyprod As String = ddlAdd.SelectedValue

                    cs.Create("@IDEmp", lbl_empresa_id.Text)
                    cs.Create("@IDF", lbl_idf.Text)
                    cs.Create("@PID", ID)
                    cs.Create("@PEntrada", keyprod)
                    oSQL.ExecuteQry(oSQL.UpdParametro, cs)
                    cs = cs1
                End If
            Next
            Return True
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Private Function UpdateParametrosText() As Boolean
        Dim oSQL As New SQLActualizarDatos(oUsr)
        Dim cs As New ColeccionPrmSql
        UpdateParametrosText = False
        Try
            For Each row As GridViewRow In GridView1.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    'Dim keycont As Integer = GridView3.DataKeys(row.RowIndex).Value
                    Dim cs1 As New ColeccionPrmSql
                    Dim ID As String = row.Cells(0).Text
                    txtAdd = row.FindControl("txtAdd")
                    Dim keyprod As String = txtAdd.Text

                    cs.Create("@IDEmp", lbl_empresa_id.Text)
                    cs.Create("@IDF", lbl_idf.Text)
                    cs.Create("@PID", ID)
                    cs.Create("@PEntrada", keyprod)
                    oSQL.ExecuteQry(oSQL.UpdParametro, cs)
                    cs = cs1
                End If
            Next
            Return True
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Private Function DeleteGuia() As Boolean
        Dim oSQL As New SQLEliminarDatos(oUsr)
        Dim cs As New ColeccionPrmSql
        DeleteGuia = False
        Try
            cs.Create("@IDF", lbl_idf.Text)
            cs.Create("@IDEmp", lbl_empresa_id.Text)
            Return oSQL.ExecuteQry(oSQL.DelGuia, cs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Private Function DeleteParametros() As Boolean
        Dim oSQL As New SQLEliminarDatos(oUsr)
        Dim cs As New ColeccionPrmSql
        DeleteParametros = False
        Try
            cs.Create("@IDF", lbl_idf.Text)
            cs.Create("@IDEmp", lbl_empresa_id.Text)
            Return oSQL.ExecuteQry(oSQL.DelParams, cs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Private Function DeleteGeneral() As Boolean
        Dim oSQL As New SQLEliminarDatos(oUsr)
        Dim cs As New ColeccionPrmSql
        DeleteGeneral = False
        Try
            cs.Create("@IDF", lbl_idf.Text)
            cs.Create("@IDEmp", lbl_empresa_id.Text)
            Return oSQL.ExecuteQry(oSQL.DelGeneral, cs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Protected Sub ddl_flete_Changed(sender As Object, e As EventArgs)
        Try
            Dim IdC As Object = vbNull
            Dim TestComp As Integer
            Dim oSqlI As New SQLCargarDatos(oUsr)
            Dim lC As New ColeccionPrmSql
            For Each row As GridViewRow In GridView3.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    'Dim keycont As Integer = GridView1.DataKeys(row.RowIndex).Value
                    ddlAdd = row.FindControl("ddlAdd")

                    Dim param As Integer = row.Cells(0).Text
                    If param = 1 Then
                        IdC = ddlAdd.SelectedValue
                        FleteGlobal = IdC
                    End If

                    If param = 2 Then
                        TestComp = StrComp(Convert.ToString(IdC), "%", CompareMethod.Text)
                        If Not TestComp = 0 Then
                            lC.Create("_Tabla", "TRANSPORTISTAOPERADOR")
                            lC.Create("_Qry", oSqlI.ComboOperadores)
                            lC.Create("_Order", "Nombre")
                            lC.Create("_DefaultKey", 0)
                            lC.Create("_DefaultDes", "[SELECCIONAR]")
                            lC.Create("@fleteid", IdC)
                            LoadCombo(oUsr, ddlAdd, lC)
                            GetIndex(ddlAdd, OperadorVal(IdC))
                        Else
                            GetIndexForText(ddlAdd, "[SELECCIONAR]")
                        End If
                    End If
                End If
            Next

            If IsNumeric(FleteGlobal) Then
                LoadListaGuia(FleteGlobal)
            End If

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub

    Private Function OperadorVal(ByVal flete As Integer) As Integer
        Dim oSql As New SQLCargarDatos(oUsr)
        Dim oCs As New ColeccionPrmSql
        OperadorVal = 0

        Try
            oCs.Create("@flete", flete)
            oCs.Create("_VALOR", "OperadorID")
            Return oSql._Valor(oSql.ItemOperador, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Private Function ObtieneOrigen(ByVal ruta As String) As String
        Dim oSql As New SQLFletes(oUsr)
        Dim oCs As New ColeccionPrmSql
        ObtieneOrigen = ""
        Try
            oCs.Create("@ruta", ruta)
            oCs.Create("_VALOR", "OrigenID")
            Return oSql._Valor(oSql.ValOrigen, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Private Function ObtieneRuta(ByVal flete As String) As String
        Dim oSql As New SQLFletes(oUsr)
        Dim oCs As New ColeccionPrmSql
        ObtieneRuta = ""
        Try
            oCs.Create("@flete", flete)
            oCs.Create("_VALOR", "RutaID")
            Return oSql._Valor(oSql.ValRutaID, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Private Function ObtieneFlete() As String
        Dim oSql As New SQLFletes(oUsr)
        Dim oCs As New ColeccionPrmSql
        ObtieneFlete = ""
        Try
            oCs.Create("@IDF", ArriKey(2))
            oCs.Create("_VALOR", "ParametroEntrada")
            Return oSql._Valor(oSql.ValFleteID, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Private Function ObtieneOperador() As String
        Dim oSql As New SQLFletes(oUsr)
        Dim oCs As New ColeccionPrmSql
        ObtieneOperador = ""
        Try
            oCs.Create("@IDF", ArriKey(2))
            oCs.Create("_VALOR", "ParametroEntrada")
            Return oSql._Valor(oSql.ValOperadorID, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    'METODOS NUEVOS PARA INVENTARIO
    Private Function InsertInventario() As Boolean
        Dim oSQL As New SQLInsertarDatos(oUsr)
        Dim cs As New ColeccionPrmSql
        InsertInventario = False
        Try
            For Each row As GridViewRow In GridView2.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    'Dim keycont As Integer = GridView3.DataKeys(row.RowIndex).Value
                    Dim cs1 As New ColeccionPrmSql
                    PesoNeto = row.FindControl("txt_PesoNeto")
                    GuiaID = row.FindControl("txt_GuiaID")
                    Ubicacion = row.FindControl("ddl_ubicacion")
                    variedad = row.FindControl("ddl_variedad")
                    envase = row.FindControl("ddl_envase")
                    Cantidad = row.FindControl("txt_Cantidad")
                    PesoBruto = row.FindControl("txt_PesoBruto")

                    If String.IsNullOrEmpty(GuiaID.Text) Then

                    Else
                        cs.Create("@IDEmp", lbl_empresa_id.Text)
                        cs.Create("@UbicaID", ObtieneUbicaDestino())
                        'CONDICIOON PARA EVALUAR EL PRODUCTOID
                        'METODO PARA OBTENER EL PRODUCTO ID
                        Select Case ObtieneCultivo(variedad.SelectedValue)
                            Case 1 'AJO
                                cs.Create("@ProdID", "AJO")
                            Case 11 'AJORGANICO
                                cs.Create("@ProdID", "AJORGANICO")
                            Case Else
                                cs.Create("@ProdID", "AJO")
                        End Select
                        cs.Create("@MovClas", "")
                        cs.Create("@IDF", lbl_idf.Text)
                        cs.Create("@fecha", txt_fecha.Text)
                        cs.Create("@Cantidad", Cantidad.Text)
                        cs.Create("@PNeto", PesoNeto.Text)

                        'PARAMETROS DE BUILDING_LOTE
                        cs.Create("@ProcesoID", lbl_proceso_id.Text)
                        cs.Create("@TablaID", Ubicacion.SelectedValue)
                        cs.Create("@VarID", variedad.SelectedValue)
                        cs.Create("@fechaLote", txt_fecha.Text)
                        cs.Create("@Extra", ObtieneSize(Ubicacion.SelectedValue, variedad.SelectedValue))

                        cs.Create("@MovLoteHis", "")
                        cs.Create("@OrigenMovID", 7)

                        Select Case ArriKey(0)
                            Case "INS"
                                'REALIZA UN MOVIMIENTO DE ENTRADA AL INVENTARIO
                                cs.Create("@MovTipo", "E")
                                cs.Create("@MovObs", "Recepción de MP")
                            Case "DEL"
                                'REALIZA UN MOVIMIENTO DE SALIDA AL INVENTARIO
                                cs.Create("@MovTipo", "S")
                                cs.Create("@MovObs", "Cancelación de Recepción de MP")
                        End Select
                        oSQL.ExecuteQry(oSQL.InsInventario, cs)
                        cs = cs1
                    End If
                End If
            Next
            Return True
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Private Function ObtieneCultivo(ByVal variedad As Integer) As Integer
        Dim oSql As New SQLInventarios(oUsr)
        Dim oCs As New ColeccionPrmSql
        ObtieneCultivo = 0
        Try
            oCs.Create("@variedad", variedad)
            oCs.Create("_VALOR", "CultivoID")
            Return oSql._Valor(oSql.ValorCultivo, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Private Function ObtieneUbicaDestino() As String
        Dim oSql As New SQLInventarios(oUsr)
        Dim oCs As New ColeccionPrmSql
        ObtieneUbicaDestino = ""
        Try
            oCs.Create("@proceso", lbl_proceso_id.Text)
            oCs.Create("_VALOR", "UbicacionDestinoID")
            Return oSql._Valor(oSql.ValorUbicacionDestino, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Private Function ObtieneFleteInv() As Integer
        Dim oSql As New SQLFletes(oUsr)
        Dim oCs As New ColeccionPrmSql
        ObtieneFleteInv = 0
        Try
            oCs.Create("@IDF", lbl_idf.Text)
            oCs.Create("_VALOR", "ParametroEntrada")
            Return oSql._Valor(oSql.ValFleteID, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

    Private Function ObtieneSize(ByVal ubicacion As String, ByVal variedad As Integer) As String
        Dim oSql As New SQLInventarios(oUsr)
        Dim oCs As New ColeccionPrmSql
        ObtieneSize = ""
        Try
            oCs.Create("@ubi", ubicacion)
            oCs.Create("@vari", variedad)
            oCs.Create("@flete", ObtieneFleteInv())
            oCs.Create("_VALOR", "ClasificaSizeNombre")
            Return oSql._Valor(oSql.ValorSize, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function
    'FIN DE METODOS PARA INVENTARIO
End Class