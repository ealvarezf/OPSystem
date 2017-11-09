Imports Security_System
Imports System.Configuration
Public Class CnxDefault
    Shared Function GetDataStore() As String
        Dim Store As New MyDataStore
        GetDataStore = String.Empty
        Try
            With Store
                .Source = System.Configuration.ConfigurationManager.AppSettings("DataSource")
                .DataBase = System.Configuration.ConfigurationManager.AppSettings("DataBase")
                .User = Decript(System.Configuration.ConfigurationManager.AppSettings("User"))
                .Password = Decript(System.Configuration.ConfigurationManager.AppSettings("Password"))
                Return .StringCnxSQL
            End With

        Catch ex As Exception
            Dim s As String = ex.Message
        End Try
    End Function

    Shared Function GetDataStoreInfoEncript() As String
        Dim Store As New MyDataStore
        GetDataStoreInfoEncript = String.Empty
        Try
            With Store
                .Source = System.Configuration.ConfigurationManager.AppSettings("DataSource")
                .DataBase = System.Configuration.ConfigurationManager.AppSettings("DataBase")
                .User = System.Configuration.ConfigurationManager.AppSettings("User")
                .Password = System.Configuration.ConfigurationManager.AppSettings("Password")
                Return .StringCnxSQL
            End With

        Catch ex As Exception
            Dim s As String = ex.Message
        End Try
    End Function

    Shared Function Decript(ByVal sWord As String) As String
        'Función que se encarga de la Decriptación del String dado
        'Argumentos: sWord  string a Desencriptar
        'Regresa:    El resultado de la Decriptación
        'Globales:   No modifica
        '
        Dim i As Integer, j As Integer        'contadores para el ciclo for
        Dim sPaso As String  'concactenación de los caracteres decriptados
        Dim sCh As String  'caracter encriptado
        Dim sTemp As Double  'obtener el valor con el que se decripta cada caracter
        Dim sPrimer As String  'primer parte del string
        Dim sResult As String  'palabra decriptada

        Decript = ""

        Try

            If Len(sWord) > 55 Then
                i = 1
                sPaso = ""
                sTemp = 55
                While i <= 55
                    If i Mod 2 = 0 Then
                        sCh = Asc(Mid(sWord, i, 1))
                        sPaso = sPaso & Chr(sCh - sTemp)
                    Else
                        sCh = Asc(Mid(sWord, i, 1))
                        If sCh Mod 2 = 0 Then
                            sPaso = sPaso & Chr(sCh - ((sTemp * 2) - 1))
                        Else
                            sPaso = sPaso & Chr(sCh - ((sTemp * 2) + 1))
                        End If
                    End If
                    i = i + 1
                    sTemp = sTemp - 1
                End While
                'If len(
                sPrimer = sPaso
                sPaso = ""
                i = 1
                j = 56

                sTemp = Len(sWord) - 55
                While i <= Len(sWord) - 55
                    If i Mod 2 = 0 Then
                        sCh = Asc(Mid(sWord, j, 1))
                        sPaso = sPaso & Chr(sCh - sTemp)
                    Else
                        sCh = Asc(Mid(sWord, j, 1))
                        If sCh Mod 2 = 0 Then
                            sPaso = sPaso & Chr(sCh - ((sTemp * 2) - 1))
                        Else
                            sPaso = sPaso & Chr(sCh - ((sTemp * 2) + 1))
                        End If
                    End If
                    i = i + 1
                    sTemp = sTemp - 1
                    j = j + 1
                End While
                sResult = sPrimer + sPaso
            Else
                i = 1
                sPaso = ""
                sTemp = Len(sWord)
                While i <= Len(sWord)
                    If i Mod 2 = 0 Then
                        sCh = Asc(Mid(sWord, i, 1))
                        sPaso = sPaso & Chr(sCh - sTemp)
                    Else
                        sCh = Asc(Mid(sWord, i, 1))
                        If sCh Mod 2 = 0 Then
                            sPaso = sPaso & Chr(sCh - ((sTemp * 2) - 1))
                        Else
                            sPaso = sPaso & Chr(sCh - ((sTemp * 2) + 1))
                        End If
                    End If
                    i = i + 1
                    sTemp = sTemp - 1
                End While
                sResult = sPaso
            End If

            Decript = sResult

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, ex.Source)

        End Try

    End Function

    Shared Function Encript(ByVal ws_pal_abr As String) As String
        'Función que se encarga de la Encriptación del String dado
        'Argumentos: ws_pla_abr  string a encriptar
        'Regresa:    El resultado de la encriptación
        'Globales:   No modifica
        '
        Dim i, j As Integer       'contador para el ciclo for
        Dim ws_pal_asc As String  'concatenacion del string encriptado
        Dim ws_pal_xor As String  'obtencion del caracter para encriptarlo
        Dim wn_tem_por As Double  'valor con el que se encripta el caracter
        Dim ws_pri_mer As String  'primer parte del string
        Dim ws_enc_rip As String  'string encriptado

        Encript = ""

        Try

            If Len(ws_pal_abr) > 55 Then
                i = 1
                ws_pal_asc = ""
                wn_tem_por = 55
                While i <= 55
                    If wn_tem_por <> 0 Then
                        If i Mod 2 = 0 Then
                            ws_pal_xor = Asc(Mid(ws_pal_abr, i, 1)) + wn_tem_por
                            ws_pal_xor = ws_pal_xor Xor 0
                            ws_pal_asc = ws_pal_asc & Chr(ws_pal_xor)
                            ws_pal_xor = ""
                        Else
                            ws_pal_xor = Asc(Mid(ws_pal_abr, i, 1)) + (wn_tem_por * 2)
                            ws_pal_xor = ws_pal_xor Xor 1
                            ws_pal_asc = ws_pal_asc & Chr(ws_pal_xor)
                            ws_pal_xor = ""
                        End If
                        i = i + 1
                        wn_tem_por = wn_tem_por - 1
                    End If
                End While
                ws_pri_mer = ws_pal_asc
                ws_pal_asc = ""
                i = 1
                j = 56
                wn_tem_por = Len(ws_pal_abr) - 55
                While i <= Len(ws_pal_abr) - 55
                    If wn_tem_por <> 0 Then
                        If i Mod 2 = 0 Then
                            ws_pal_xor = Asc(Mid(ws_pal_abr, j, 1)) + wn_tem_por
                            ws_pal_xor = ws_pal_xor Xor 0
                            ws_pal_asc = ws_pal_asc & Chr(ws_pal_xor)
                            ws_pal_xor = ""
                        Else
                            ws_pal_xor = Asc(Mid(ws_pal_abr, j, 1)) + (wn_tem_por * 2)
                            ws_pal_xor = ws_pal_xor Xor 1
                            ws_pal_asc = ws_pal_asc & Chr(ws_pal_xor)
                            ws_pal_xor = ""
                        End If
                        i = i + 1
                        wn_tem_por = wn_tem_por - 1
                        j = j + 1
                    Else
                        wn_tem_por = Len(ws_pal_abr)
                    End If
                End While
                ws_enc_rip = ws_pri_mer + ws_pal_asc
            Else
                i = 1
                ws_pal_asc = ""
                wn_tem_por = Len(ws_pal_abr)
                While i <= Len(ws_pal_abr)
                    If wn_tem_por <> 0 Then
                        If i Mod 2 = 0 Then
                            ws_pal_xor = Asc(Mid(ws_pal_abr, i, 1)) + wn_tem_por
                            ws_pal_xor = ws_pal_xor Xor 0
                            ws_pal_asc = ws_pal_asc & Chr(ws_pal_xor)
                            ws_pal_xor = ""
                        Else
                            ws_pal_xor = Asc(Mid(ws_pal_abr, i, 1)) + (wn_tem_por * 2)
                            ws_pal_xor = ws_pal_xor Xor 1
                            ws_pal_asc = ws_pal_asc & Chr(ws_pal_xor)
                            ws_pal_xor = ""
                        End If
                        i = i + 1
                        wn_tem_por = wn_tem_por - 1
                    Else
                        wn_tem_por = Len(ws_pal_abr)
                    End If
                End While
                ws_enc_rip = ws_pal_asc
            End If

            Encript = ws_enc_rip

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, ex.Source)
        End Try

    End Function

    Shared Function GetUserInfo(ByVal oUsr As UserLogin) As Boolean
        Dim oSql As New AppUsuarios(oUsr)
        Dim oCs As New ColeccionPrmSql
        Dim Ds As New DataSet
        GetUserInfo = True
        Try
            oCs.Create("@SgUserEmail", oUsr.Email)
            If oSql.GetQry(Ds, "SG_USUARIOS", oSql.Item, oCs) Then
                oUsr.keyusu = Ds.Tables("SG_USUARIOS").Rows(0).Item("SgUserID")
                oUsr.Pfl.ID = Ds.Tables("SG_USUARIOS").Rows(0).Item("PerfilID").ToString
                oUsr.FirstName = Ds.Tables("SG_USUARIOS").Rows(0).Item("SgUserName").ToString
                Return True
                '
            End If
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Function

End Class

Public Class MyDataStore
    Public Property Source() As String
    Public Property DataBase() As String
    Public Property User() As String
    Public Property Password() As String
    Public ReadOnly Property StringCnxSQL() As String
        Get
            Dim sStringCnx = "Data Source=@server;Initial Catalog=@dbase;Persist Security Info=True;User ID=@user;Password=@password"
            Dim Cnb As SqlClient.SqlConnectionStringBuilder = New SqlClient.SqlConnectionStringBuilder(sStringCnx) With {
                .DataSource = Source,
                .InitialCatalog = DataBase,
                .Password = Password,
                .UserID = User
            }
            Return Cnb.ToString
        End Get
    End Property
End Class

Public Class AppMenu
    Inherits SQL

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub

    Public ReadOnly Property Lista
        Get
            Return " SELECT PD.SGFuncionID as men_keymen, SGFuncion as men_desmen, PD.ModuloID as men_keypad, Modulo as men_despad, SGFuncionModuloOrden as men_posisi, SGFuncionWeb as men_fmaspx, " &
                   "        SGFuncionConsultar+SGFuncionNuevo+SGFuncionEliminar+SGFuncionRecuperar+SGFuncionModificar+SGFuncionListar+SGFuncionExportar+SGFuncionGraficar as men_permis " &
                   "   FROM SGPERFILES_DETALLE PD LEFT JOIN SGPERFILES P ON P.PerfilID = PD.PerfilID " &
                   "                           LEFT JOIN SGFUNCIONES F ON F.SGFuncionID = PD.SGFuncionID " &
                   "                           LEFT JOIN SGMODULOS M ON M.ModuloID = PD.ModuloID " &
                   "						   LEFT JOIN SGFUNCIONES_MODULO FM ON FM.EstructuraMenuID = P.EstructuraMenuID AND FM.ModuloID = PD.ModuloID AND FM.SGFuncionID = PD.SGFuncionID " &
                   "  WHERE P.PerfilID = @keypfl " &
                   "    AND PerfilConsultar = 'S' " &
                   "    AND F.SGFuncionPrimaria = 'S' " &
                   "  UNION " &
                   " SELECT ED.ModuloID as men_keymen, Modulo as men_desmen, ModuloPadreID as men_keypad, " &
                   "        (SELECT Modulo FROM SGMODULOS WHERE ModuloID = ModuloPadreID) as men_despad, EstructuraMenuOrden as men_posisi, '' as men_fmaspx, '' as men_permis " &
                   "   FROM SGESTRUCTURASM_DETALLE ED LEFT JOIN SGMODULOS M ON M.ModuloID = ED.ModuloID " &
                   "  WHERE EstructuraMenuID = (SELECT EstructuraMenuID FROM SGPERFILES WHERE PerfilID = @keypfl) " &
                   "    AND EstructuraMenuID = (SELECT EstructuraMenuID FROM SGPERFILES WHERE PerfilID = @keypfl) "
        End Get
    End Property


End Class

Public Class AppUsuarios
    Inherits SQL

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub

    Public ReadOnly Property Lista
        Get
            Return " SELECT * FROM SGUSUARIOS WHERE SgUserEmail = @SgUserEmail "
        End Get
    End Property

    Public ReadOnly Property Item
        Get
            Return " SELECT * FROM SGUSUARIOS WHERE SgUserEmail = @SgUserEmail "
        End Get
    End Property

End Class

Public Class AppFunciones
    Inherits SQL

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub

    Public ReadOnly Property ItemForPage As String
        Get
            Return " SELECT * FROM SGFUNCIONES WHERE SGFuncionWeb = @SGFuncionWeb "
        End Get
    End Property

    Public ReadOnly Property Item As String
        Get
            Return " SELECT * FROM SGFUNCIONES WHERE SGFuncionID = @SGFuncionID "
        End Get
    End Property

End Class

Public Class AppSubFunciones
    Inherits SQL
    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub

    Public ReadOnly Property Item As String
        Get
            Return " SELECT *, (SELECT SGFuncion FROM SGFUNCIONES F WHERE F.SGFuncionID = SF.SGFuncionID) SGFuncionPadre FROM SGFUNCIONESSUBFUNCION SF WHERE SGSubFuncionID = @SGSubFuncionID "
        End Get
    End Property

End Class
Public Class AppPermisos
    Inherits SQL

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub

    Public ReadOnly Property Lista As String
        Get
            Return " SELECT * FROM SGUSUARIOS U LEFT JOIN SGPERFILES P ON P.PerfilID = U.PerfilID " &
                   "   LEFT JOIN SGPERFILES_DETALLE PD ON PD.PerfilID = P.PerfilID " &
                   "  WHERE SgUserID = @SgUserID " &
                   "    AND SGFuncionID = @SGFuncionID "
        End Get
    End Property

End Class
Module GFunciones
    Public Sub LoadCombo(ByVal oUsr As UserLogin, ByVal oDDL As Infragistics.Web.UI.ListControls.WebDropDown, ByVal View As DataView)
        Try
            With oDDL
                .DataSource = View
                .ValueField = View.Table.Columns(0).ColumnName
                .TextField = View.Table.Columns(1).ColumnName
                .DataBind()
                .SelectedItemIndex = 0
            End With
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub

    Public Sub GetIndex(ByRef oDDL As Infragistics.Web.UI.ListControls.WebDropDown, ByVal Val As Object)
        Try
            oDDL.SelectedItemIndex = -1
            For Each NextItem As Infragistics.Web.UI.ListControls.DropDownItem In oDDL.Items
                If NextItem.Value = Val Then
                    NextItem.Selected = True
                    oDDL.SelectedItemIndex = NextItem.Index
                End If
            Next
        Catch ex As Exception
            Dim s As String = ex.Message
        End Try
    End Sub

    Public Sub LoadCombo(ByVal oUsr As UserLogin, ByVal oDDL As DropDownList, ByVal View As DataView)
        Try
            With oDDL
                .DataSource = View
                .DataValueField = View.Table.Columns(0).ColumnName
                .DataTextField = View.Table.Columns(1).ColumnName
                .DataBind()
                .SelectedIndex = -1
            End With
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub

    Public Sub LoadCombo(ByVal oUsr As UserLogin, ByVal oDDL As DropDownList, ByVal Cs As ColeccionPrmSql)
        Dim oSQL As New SQL(oUsr)
        Dim Ds As New DataSet
        Dim sTabla As String = Cs.ItemValue("_Tabla")
        Dim sOrderBy As String = Cs.ItemValue("_Order")
        Dim sFilterBy As String = Cs.ItemValue("_Filtro")
        Dim sDefaultKey As String = Cs.ItemValue("_DefaultKey")
        Dim sDefaultDes As String = Cs.ItemValue("_DefaultDes")
        Dim sQry As String = Cs.ItemValue("_Qry")
        Try
            If oSQL.GetQry(Ds, sTabla, sQry, Cs) Then
                Dim tbl As DataTable = Ds.Tables(sTabla)
                Dim view As DataView = New DataView(tbl) With {
                    .Sort = sOrderBy
                }

                If sFilterBy <> "" Then
                    view.RowFilter = sFilterBy
                End If
                oDDL.DataSource = view
                oDDL.DataValueField = tbl.Columns(0).ColumnName
                oDDL.DataTextField = tbl.Columns(1).ColumnName
                oDDL.DataBind()
                If sDefaultKey <> "" Then
                    oDDL.Items.Insert(0, New ListItem(sDefaultDes, sDefaultKey))
                End If
            End If

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub

    Public Sub GetIndex(ByRef oDDL As DropDownList, ByVal Val As Object)
        Try
            oDDL.SelectedIndex = -1
            For Each NextItem As ListItem In oDDL.Items
                If NextItem.Value = Val Then
                    NextItem.Selected = True
                End If
            Next
        Catch ex As Exception
            Dim s As String = ex.Message
        End Try
    End Sub

    Public Sub GetIndexForText(ByRef oDDL As DropDownList, ByVal Val As Object)
        Try
            oDDL.SelectedIndex = -1
            For Each NextItem As ListItem In oDDL.Items
                If NextItem.Text = Val Then
                    NextItem.Selected = True
                End If
            Next
        Catch ex As Exception
            Dim s As String = ex.Message
        End Try
    End Sub

    Public Sub LoadGrid(ByVal oGrid As GridView, ByVal oTbl As DataTable, Optional ByVal sOrdena As String = "")
        Dim view As DataView = New DataView(oTbl)
        If sOrdena <> "" Then view.Sort = sOrdena
        oGrid.DataSource = view
        oGrid.DataBind()
        oGrid.EnableViewState = True
    End Sub

    Public Sub LoadListBx(ByVal oUsr As UserLogin, ByVal oLBx As ListBox, ByVal View As DataView)
        Try
            With oLBx
                .DataSource = View
                .DataValueField = View.Table.Columns(0).ColumnName
                .DataTextField = View.Table.Columns(1).ColumnName
                .DataBind()
                .SelectedIndex = -1
            End With
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub

    Public Sub LoadListBx(ByVal oUsr As UserLogin, ByVal oLBx As ListBox, ByVal Cs As ColeccionPrmSql)
        Dim oSQL As New SQL(oUsr)
        Dim Ds As New DataSet
        Dim sTabla As String = Cs.ItemValue("_Tabla")
        Dim sOrderBy As String = Cs.ItemValue("_Order")
        Dim sDefaultKey As String = Cs.ItemValue("_DefaultKey")
        Dim sDefaultDes As String = Cs.ItemValue("_DefaultDes")
        Dim sQry As String = Cs.ItemValue("_Qry")
        Try
            If oSQL.GetQry(Ds, sTabla, sQry, Cs) Then
                Dim tbl As DataTable = Ds.Tables(sTabla)
                Dim view As DataView = New DataView(tbl)
                view.Sort = sOrderBy
                oLBx.DataSource = view
                oLBx.DataValueField = tbl.Columns(0).ColumnName
                oLBx.DataTextField = tbl.Columns(1).ColumnName
                oLBx.DataBind()
                If sDefaultKey <> "" Then
                    oLBx.Items.Insert(0, New ListItem(sDefaultDes, sDefaultDes))
                End If
            End If

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub

End Module
