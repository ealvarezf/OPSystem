Imports System.Data.SqlClient
Imports System.Data
Imports System.Xml
Imports System
Imports System.IO
Imports System.Configuration
Imports System.Configuration.ConfigurationManager
Imports System.Net.Mail
Imports System.Net.Mail.SmtpClient

Public Class SQL
    Private Cn As New SqlClient.SqlConnection
    Private Qy As New SqlClient.SqlCommand
    'Private SQLAdapter As SqlDataAdapter
    Private m_oUsr As UserLogin
    Private m_strCnx As String

    Private Function OpenDataBase(ByVal sCnx As String) As SqlConnection
        Dim Cnx As New SqlConnection(sCnx)
        OpenDataBase = Nothing
        Try

            Cnx.Open()
            If (Cnx.State = ConnectionState.Open) Then
                If SetLanguage(Cnx, "ESPAÑOL") Then
                    Return Cnx
                End If
            End If

        Catch ex As Exception
            AddErrorLog(m_oUsr.Mis.Log, ex.Message + vbCrLf + ex.TargetSite.ToString)
        End Try

    End Function

    Private Function SetLanguage(ByRef Cn As SqlConnection, ByVal sIdioma As String) As Boolean
        Dim sStatement As String
        Try

            sStatement = "SET LANGUAGE @IDIOMA"
            sStatement = sStatement.Replace("@IDIOMA", sIdioma)
            Dim Qy As New SqlCommand(sStatement, Cn)
            Qy.CommandType = CommandType.Text

            Qy.ExecuteNonQuery()
            Qy.Dispose()

            Return True

        Catch ex As Exception
            AddErrorLog(m_oUsr.Mis.Log, ex.Message + vbCrLf + ex.TargetSite.ToString)
            Return False

        End Try

    End Function

    Private Function QryLinkDB(ByVal sDataBase As String, ByVal sStatement As String) As String
        sDataBase = sDataBase & ".dbo."
        sStatement = Replace(sStatement, "@DB_", sDataBase)
        Return sStatement
    End Function

    Public Sub New(ByVal oUsr As UserLogin)
        m_oUsr = oUsr

        Dim Cnx As String = oUsr.Mis.StrCnx
        Dim Cnb As System.Data.SqlClient.SqlConnectionStringBuilder = New System.Data.SqlClient.SqlConnectionStringBuilder(Cnx)
        Cnb.Password = Decript(Cnb.Password)
        Cnb.UserID = Decript(Cnb.UserID)
        m_strCnx = Cnb.ToString

    End Sub

    Public Function _List(ByVal sQry As String, ByVal sTabla As String, ByVal oCs As ColeccionPrmSql) As DataTable
        Dim Ds As New DataSet
        _List = Nothing
        Try
            If GetQry(Ds, sTabla, sQry, oCs) Then
                Return Ds.Tables(sTabla)
            End If

        Catch ex As Exception
            Tools.AddErrorLog(m_oUsr.Mis.Log, ex.Message + vbCrLf + ex.TargetSite.ToString)
        End Try
    End Function

    Public Function _Item(ByVal sQry As String, ByVal sTabla As String, ByVal oCs As ColeccionPrmSql) As DataTable
        Dim Ds As New DataSet
        _Item = Nothing
        Try
            If GetQry(Ds, sTabla, sQry, oCs) Then
                Return Ds.Tables(sTabla)
            End If

        Catch ex As Exception
            Tools.AddErrorLog(m_oUsr.Mis.Log, ex.Message + vbCrLf + ex.TargetSite.ToString)
        End Try
    End Function

    Public Function _Value(ByVal sQry As String, ByVal sField As String, ByVal oCs As ColeccionPrmSql) As Object
        Dim Ds As New DataSet
        _Value = Nothing
        Try
            If GetQry(Ds, "VALOR", sQry, oCs) Then
                Return Ds.Tables("VALOR").Rows(0).Item(sField)
            End If
        Catch ex As Exception
            Tools.AddErrorLog(m_oUsr.Mis.Log, ex.Message + vbCrLf + ex.TargetSite.ToString)
        End Try
    End Function

    Public Function GetQry(ByRef Ds As DataSet, ByVal sTabla As String, ByVal sQry As String, ByVal cs As ColeccionPrmSql, Optional ByVal sPrefix As String = "") As Boolean
        Dim oPrm As SqlParameter
        Try
            sQry = sQry.Replace("VIEW_FILTRO", "")
            sQry = QryLinkDB(sPrefix, sQry)
            Cn = OpenDataBase(m_strCnx)

            Dim myDataAdapter As New SqlDataAdapter(sQry, Cn)
            myDataAdapter.SelectCommand.CommandType = CommandType.Text
            myDataAdapter.SelectCommand.CommandTimeout = 300

            For Each Prm As PrmSql In cs
                If InStr(sQry.ToUpper, Prm.Nombre.ToUpper) > 0 Then
                    oPrm = New SqlParameter(Prm.Nombre, Prm.Valor)
                    myDataAdapter.SelectCommand.Parameters.Add(oPrm)
                End If
            Next

            myDataAdapter.Fill(Ds, sTabla) 'Fill the DataSet with the rows returned.
            myDataAdapter.Dispose()
            Cn.Close() 'Close the connection.

            Return True

        Catch ex As Exception
            AddErrorLog(m_oUsr.Mis.Log, ex.Message + vbCrLf + ex.TargetSite.ToString)
            Return False
        Finally
            Cn.Close()
        End Try

    End Function

    Public Function ExecuteQry(ByVal sQry As String, ByVal cs As ColeccionPrmSql, Optional ByVal sPrefix As String = "") As Boolean
        Dim oPrm As SqlParameter
        Dim sStatement As String = QryLinkDB(sPrefix, sQry)
        Try

            Cn = OpenDataBase(m_strCnx)

            Dim Qy As New SqlCommand(sStatement, Cn)
            Qy.CommandType = CommandType.Text

            For Each Prm As PrmSql In cs
                If InStr(sStatement.ToUpper, Prm.Nombre.ToUpper) > 0 Then
                    oPrm = New SqlParameter(Prm.Nombre, Prm.Valor)
                    Qy.Parameters.Add(oPrm)
                End If
            Next

            Qy.ExecuteNonQuery()
            Qy.Dispose()

            Return True

        Catch ex As Exception
            AddErrorLog(m_oUsr.Mis.Log, ex.Message + vbCrLf + ex.TargetSite.ToString)
            Return False
        Finally
            Cn.Close()
        End Try

    End Function

    Public Overloads Function ExecuteStore(ByVal Pd As String, ByVal cs As ColeccionPrmSql) As Boolean
        Dim oPrm As SqlParameter

        Try
            Cn = OpenDataBase(m_strCnx)

            Dim Qy As New SqlCommand(Pd, Cn)
            Qy.CommandType = CommandType.StoredProcedure
            Qy.CommandTimeout = 300

            For Each Prm As PrmSql In cs
                oPrm = New SqlParameter(Prm.Nombre, Prm.Valor)
                If Prm.Dirección = "out" Then oPrm.Direction = ParameterDirection.Output
                Qy.Parameters.Add(oPrm)
            Next

            Qy.ExecuteNonQuery()
            For Each op As SqlParameter In Qy.Parameters
                If op.Direction = ParameterDirection.Output Then
                    cs.ItemValue(op.ParameterName) = op.Value
                End If
            Next
            Qy.Dispose()

            Return True

        Catch ex As Exception
            AddErrorLog(m_oUsr.Mis.Log, ex.Message + vbCrLf + ex.TargetSite.ToString)
            Return False
        Finally
            Cn.Close()
        End Try

    End Function

    Public Function StatemenUpdate(ByVal oTb As DataTable) As Boolean
        Dim sStatement As String = "UPDATE @TABLE_NAME SET @ASIGNA WHERE @CONDICION"
        Dim sColumnas As String = ""
        Dim sParameter As String = ""
        Dim sAsignacion As String = ""
        Dim sCondición As String = ""
        Dim sTipoDato As String = ""
        Dim sTipoDatoC As String = ""
        Dim oPrm As SqlParameter

        Try
            If oTb.Rows.Count > 0 Then

                For Each oCol As DataColumn In oTb.Columns

                    If oCol.Unique Then
                        sCondición = sCondición & oCol.ColumnName & " = " & "@" & oCol.ColumnName & " AND "
                    Else
                        sAsignacion = sAsignacion & oCol.ColumnName & " = " & "@" & oCol.ColumnName & ","
                    End If
                    'sTipoDato = sTipoDato & oCol.DataType.ToString & ","
                Next
                sAsignacion = Left(sAsignacion, sAsignacion.Length - 1)
                sCondición = Left(sCondición, sCondición.Length - 5)
                'sTipoDato = Left(sTipoDato, sTipoDato.Length - 1)

                sStatement = sStatement.Replace("@TABLE_NAME", oTb.TableName).Replace("@ASIGNA", sAsignacion).Replace("@CONDICION", sCondición)

                Cn = OpenDataBase(m_strCnx)

                Dim Qy As New SqlCommand(sStatement, Cn)
                Qy.CommandType = CommandType.Text

                For Each oCol As DataColumn In oTb.Columns
                    oPrm = New SqlParameter("@" & oCol.ColumnName, Type.GetType(oCol.DataType.ToString))
                    oPrm.Direction = ParameterDirection.Input
                    Qy.Parameters.Add(oPrm)
                Next

                'Ejecución de la sentencia insert para cada registro
                For Each oRow As DataRow In oTb.Rows
                    For Each oCol As DataColumn In oTb.Columns
                        Qy.Parameters("@" & oCol.ColumnName).Value = oRow(oCol.ColumnName)
                    Next
                    Qy.ExecuteNonQuery()
                Next

            End If

            Return True

        Catch ex As Exception
            AddErrorLog(m_oUsr.Mis.Log, ex.Message + vbCrLf + ex.TargetSite.ToString)
            Return False
        Finally
            Cn.Close()
        End Try

    End Function

    Public Function StatemenInsert(ByVal oTb As DataTable, Optional ByRef iNewID As Integer = -1) As Boolean
        Dim sStatement As String = "INSERT INTO @TABLE_NAME (@CAMPOS) VALUES(@PARAMETERS)"
        Dim sColumnas As String = ""
        Dim sParameter As String = ""
        Dim sTipoDato As String = ""
        Dim oPrm As SqlParameter

        Try
            If oTb.Rows.Count > 0 Then

                For Each oCol As DataColumn In oTb.Columns
                    If Not oCol.AutoIncrement And oCol.Caption <> "Excluir" Then
                        sColumnas = sColumnas & oCol.ColumnName & ","
                        sParameter = sParameter & "@" & oCol.ColumnName & ","
                        sTipoDato = sTipoDato & oCol.DataType.ToString & ","
                    End If
                Next
                sColumnas = Left(sColumnas, sColumnas.Length - 1)
                sParameter = Left(sParameter, sParameter.Length - 1)
                sTipoDato = Left(sTipoDato, sTipoDato.Length - 1)

                sStatement = sStatement.Replace("@TABLE_NAME", oTb.TableName).Replace("@CAMPOS", sColumnas).Replace("@PARAMETERS", sParameter)

                Cn = OpenDataBase(m_strCnx)

                Dim Qy As New SqlCommand(sStatement, Cn)
                Qy.CommandType = CommandType.Text

                For Each oCol As DataColumn In oTb.Columns
                    If Not oCol.AutoIncrement And oCol.Caption <> "Excluir" Then
                        oPrm = New SqlParameter("@" & oCol.ColumnName, Type.GetType(oCol.DataType.ToString))
                        oPrm.Direction = ParameterDirection.Input
                        Qy.Parameters.Add(oPrm)
                    End If
                Next

                'Ejecución de la sentencia insert para cada registro
                For Each oRow As DataRow In oTb.Rows
                    For Each oCol As DataColumn In oTb.Columns
                        If Not oCol.AutoIncrement And oCol.Caption <> "Excluir" Then
                            Qy.Parameters("@" & oCol.ColumnName).Value = oRow(oCol.ColumnName)
                        End If
                    Next
                    Qy.ExecuteNonQuery()
                Next

                'Para obtener el registro insertado
                If iNewID = 0 Then
                    Dim oT As New DataTable
                    Dim myDataAdapter As New SqlDataAdapter("SELECT IDENT_CURRENT('ORDENP') as NewID", Cn)
                    myDataAdapter.SelectCommand.CommandType = CommandType.Text
                    myDataAdapter.Fill(oT)
                    iNewID = oT.Rows(0).Item("NewID").ToString
                    myDataAdapter.Dispose()
                End If
                'Fin

            End If

            Return True

        Catch ex As Exception
            AddErrorLog(m_oUsr.Mis.Log, ex.Message + vbCrLf + ex.TargetSite.ToString)
            Return False
        Finally
            Cn.Close()
        End Try

    End Function

    '================================================================================================================
    ' Ernesto Alvarez Flores
    '================================================================================================================
    Public Function GetTabla(ByRef Ds As DataSet, ByVal Tb As String, ByVal Pd As String, ByVal cs As ColeccionPrmSql) As Boolean
        Dim oPrm As SqlParameter
        Try
            Cn = OpenDataBase(m_strCnx)

            Dim myDataAdapter As New SqlDataAdapter(Pd, Cn)
            myDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            myDataAdapter.SelectCommand.CommandTimeout = 300

            For Each Prm As PrmSql In cs
                oPrm = New SqlParameter(Prm.Nombre, Prm.Valor)
                myDataAdapter.SelectCommand.Parameters.Add(oPrm)
            Next

            myDataAdapter.Fill(Ds, Tb) 'Fill the DataSet with the rows returned.
            myDataAdapter.Dispose()
            Cn.Close() 'Close the connection.

            Return True

        Catch ex As Exception
            AddErrorLog(m_oUsr.Mis.Log, ex.Message + vbCrLf + ex.TargetSite.ToString)
            Return False
        Finally
            Cn.Close()
        End Try

    End Function

End Class

Public Class PrmSql
    Public Property Nombre As String
    Public Property Valor As Object
    Public Property Dirección As String
End Class

Public Class ColeccionPrmSql
    Inherits System.Collections.CollectionBase

    'La propiedad Item configura ó devuelve un objeto PrmSql
    Default Property Item(ByVal indice As Integer) As PrmSql
        Get
            Return CType(InnerList.Item(indice), PrmSql)
        End Get
        Set(ByVal value As PrmSql)
            InnerList.Item(indice) = value
        End Set
    End Property

    Property ItemValue(ByVal sNombre As String) As Object
        Get
            ItemValue = Nothing
            For Each oE As PrmSql In InnerList
                If oE.Nombre = sNombre Then
                    Return oE.Valor
                End If
            Next
        End Get
        Set(ByVal value As Object)
            For Each oE As PrmSql In InnerList
                If oE.Nombre = sNombre Then
                    oE.Valor = value
                End If
            Next
        End Set
    End Property

    'Solo puede agregar objetos PrmSql a esta colección
    Sub Add(ByVal value As PrmSql)
        InnerList.Add(value)
    End Sub

    Function Create(ByVal sNombre As String, ByVal oVal As Object, Optional ByVal Direc As String = "inp") As PrmSql
        Dim oPrm As New PrmSql
        Try
            With oPrm
                .Nombre = sNombre
                .Valor = oVal
                .Dirección = Direc
            End With
            Add(oPrm)

        Catch ex As Exception
            Dim s As String = ex.Message
        End Try

        Return oPrm

    End Function

    Overloads Sub Clear()
        InnerList.Clear()
    End Sub

End Class

Public Class AdminXML
    Private Const ITEM_SERV = 0
    Private Const ITEM_BASE = 1
    Private Const ITEM_USER = 2
    Private Const ITEM_PASW = 3

    Private Const ORAC_BASE = 0
    Private Const ORAC_USER = 1
    Private Const ORAC_PASW = 2

    Private Const CONN_STRING = "Data Source=@SERVER;Initial Catalog=@DBASE;Persist Security Info=True;User ID=@USER;Password=@PASSWORD"
    Private Const CONN_STRING_ORA = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=ERNESTO)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=XE)));User Id=@USER;Password=@PASSWORD;Enlist=false;Pooling=true"
    Private Const CONN_STRING_ORACLE = "DATA SOURCE=@DBASE;PERSIST SECURITY INFO=True;USER ID=@USER; PASSWORD=@PASSWORD;"


    Public Shared ReadOnly Property SQLServerConnString(ByVal sDivision As String) As String
        Get
            Dim s As String
            Dim ar() As String
            s = AppSettings(sDivision)
            ar = Split(s, ";")
            Select Case sDivision
                Case "ORA"
                    If ar.LongLength = 2 Then
                        s = CONN_STRING_ORA.Replace("@USER", Decript(ar(0))).Replace("@PASSWORD", Decript(ar(1)))

                    End If
                Case "ORACLE"
                    If ar.LongLength = 3 Then
                        s = CONN_STRING_ORACLE.Replace("@DBASE", ar(ORAC_BASE)).Replace("@USER", Decript(ar(ORAC_USER))).Replace("@PASSWORD", Decript(ar(ORAC_PASW)))
                    End If

                Case "SS"
                    If ar.LongLength = 4 Then
                        s = CONN_STRING.Replace("@SERVER", ar(ITEM_SERV)).Replace("@DBASE", ar(ITEM_BASE)).Replace("@USER", Decript(ar(ITEM_USER))).Replace("@PASSWORD", Decript(ar(ITEM_PASW)))
                    End If
                Case Else
                    s = ""

            End Select

            Return s

        End Get
    End Property

End Class

Public Class UserLogin
    Public Property keyusu As Integer
    Public Property usuaux As Integer
    Public Property Email As String
    Public Property FirstName As String
    Public Property LastName As String
    Public Property Pfl As New Perfil
    Public Property Mis As New MiSession
End Class



Public Class Permisos
    Inherits SQL
    Private m_sLog As String
    Private Ds As New DataSet

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
        m_sLog = oUsr.Mis.Log
        GetPermisos(oUsr)
    End Sub

    Private M_FUN_CONSUL As Boolean
    Private M_FUN_ENUEVO As Boolean
    Private M_FUN_ELIMIN As Boolean
    Private M_FUN_RECUPE As Boolean
    Private M_FUN_MODIFI As Boolean
    Private M_FUN_LISTAR As Boolean
    Private M_FUN_EXPORT As Boolean
    Private M_FUN_GRAFIC As Boolean
    Private M_FUN_ESPEC1 As Boolean
    Private M_FUN_ESPEC2 As Boolean
    Private M_FUN_ESPEC3 As Boolean
    Private M_FUN_ESPEC4 As Boolean
    Private M_FUN_ESPEC5 As Boolean

    Public ReadOnly Property FUN_CONSUL As Boolean
        Get
            Return M_FUN_CONSUL
        End Get
    End Property
    Public ReadOnly Property FUN_ENUEVO As Boolean
        Get
            Return M_FUN_ENUEVO
        End Get
    End Property

    Public ReadOnly Property FUN_ELIMIN As Boolean
        Get
            Return M_FUN_ELIMIN
        End Get
    End Property
    Public ReadOnly Property FUN_RECUPE As Boolean
        Get
            Return M_FUN_RECUPE
        End Get
    End Property
    Public ReadOnly Property FUN_MODIFI As Boolean
        Get
            Return M_FUN_MODIFI
        End Get
    End Property
    Public ReadOnly Property FUN_LISTAR As Boolean
        Get
            Return M_FUN_LISTAR
        End Get
    End Property
    Public ReadOnly Property FUN_EXPORT As Boolean
        Get
            Return M_FUN_EXPORT
        End Get
    End Property
    Public ReadOnly Property FUN_GRAFIC As Boolean
        Get
            Return M_FUN_GRAFIC
        End Get
    End Property
    Public ReadOnly Property FUN_ESPEC1 As Boolean
        Get
            Return M_FUN_ESPEC1
        End Get
    End Property
    Public ReadOnly Property FUN_ESPEC2 As Boolean
        Get
            Return M_FUN_ESPEC2
        End Get
    End Property
    Public ReadOnly Property FUN_ESPEC3 As Boolean
        Get
            Return M_FUN_ESPEC3
        End Get
    End Property
    Public ReadOnly Property FUN_ESPEC4 As Boolean
        Get
            Return M_FUN_ESPEC4
        End Get
    End Property
    Public ReadOnly Property FUN_ESPEC5 As Boolean
        Get
            Return M_FUN_ESPEC5
        End Get
    End Property

    Private ReadOnly Property ListaPermisos As String
        Get
            Return " SELECT dpf_consul, dpf_enuevo, dpf_elimin, dpf_recupe, dpf_modifi, dpf_listar, dpf_export, dpf_grafic," & _
                   "        dpf_espec1, dpf_espec2, dpf_espec3, dpf_espec4, dpf_espec5 " & _
                   "   FROM SG_DETALLES_PERFIL " & _
                   "  WHERE dpf_keypfl = @keypfl " & _
                   "    AND dpf_keyfun = @keyfun "
        End Get
    End Property

    Private Sub GetPermisos(ByVal oUsr As UserLogin)
        Dim oCs As New ColeccionPrmSql
        Try
            oCs.Create("@keypfl", oUsr.Pfl.ID)
            oCs.Create("@keyfun", oUsr.Mis.Función)
            If GetQry(Ds, "PERMISOS", ListaPermisos, oCs) Then
                If Not Ds.Tables("PERMISOS") Is Nothing Then
                    For Each Dr As DataRow In Ds.Tables("PERMISOS").Rows
                        M_FUN_CONSUL = (Dr("DPF_CONSUL") = "S")
                        M_FUN_ENUEVO = (Dr("DPF_ENUEVO") = "S")
                        M_FUN_ELIMIN = (Dr("DPF_ELIMIN") = "S")
                        M_FUN_RECUPE = (Dr("DPF_RECUPE") = "S")
                        M_FUN_MODIFI = (Dr("DPF_MODIFI") = "S")
                        M_FUN_LISTAR = (Dr("DPF_LISTAR") = "S")
                        M_FUN_EXPORT = (Dr("DPF_EXPORT") = "S")
                        M_FUN_GRAFIC = (Dr("DPF_GRAFIC") = "S")
                        M_FUN_ESPEC1 = (Dr("DPF_ESPEC1") = "S")
                        M_FUN_ESPEC2 = (Dr("DPF_ESPEC2") = "S")
                        M_FUN_ESPEC3 = (Dr("DPF_ESPEC3") = "S")
                        M_FUN_ESPEC4 = (Dr("DPF_ESPEC4") = "S")
                        M_FUN_ESPEC5 = (Dr("DPF_ESPEC5") = "S")
                        Exit For
                    Next
                End If
            End If
        Catch ex As Exception
            Tools.AddErrorLog(m_sLog, ex)
        End Try
    End Sub


End Class

Public Class Perfil
    Public Property ID As String
    Public Property Name As String
End Class

Public Class MiSession
    Public Property Empresa As Integer
    Public Property División As String
    Public Property StrCnx As String
    Public Property Log As String
    Public Property Status As String
    Public Property Función As String
    Public Property sPath As String
End Class

Public Class Consultas

    Public Shared Function OPERADORES() As String
        Return "SELECT * FROM OPERADORES"
    End Function

    Shared Function AddWhereFiltro(ByVal sQry As String, ByVal sFiltro As String) As String
        AddWhereFiltro = sQry
        Try
            If sFiltro <> "" Then
                sQry = sQry.Replace("VIEW_FILTRO", "WHERE" & " " & sFiltro)
            End If
            Return sQry
        Catch ex As Exception
            Dim s As String = ex.Message
        End Try
    End Function

    Shared Function AddAndFiltro(ByVal sQry As String, ByVal sFiltro As String) As String
        AddAndFiltro = sQry
        Try
            If sFiltro <> "" Then
                sQry = sQry.Replace("VIEW_FILTRO", "WHERE" & " " & sFiltro)
            End If
            Return sQry
        Catch ex As Exception
            Dim s As String = ex.Message
        End Try
    End Function

    Shared Function CFiltro(ByVal Ope As String, ByVal oCs As ColeccionPrmSql) As String
        CFiltro = ""
        Try
            If oCs.ItemValue("_FiltroGrid").ToString <> "" Then
                Return Ope & " " & oCs.ItemValue("_FiltroGrid").ToString
            End If
        Catch ex As Exception
            Dim s As String = ex.Message
        End Try
    End Function

    Public Shared Function SG_ESTRUCTURA_DEL_PERFIL() As String
        Return "SELECT * FROM SG_PERFILES WHERE pfl_keypfl = @keypfl"
    End Function

    Public Shared Function SG_DETALLES_ESTRUCTURAM_INS() As String
        Return " INSERT INTO SG_DETALLES_ESTRUCTURAM(dem_keyeme, dem_keymod, dem_keypad, dem_keydem)" & _
               " VALUES (@keyeme, @keymod, @keypad, @keydem) "
    End Function

    Public Shared Function SG_FUNCIONES_MODULO_INS() As String
        Return " INSERT INTO SG_FUNCIONES_MODULO(fmo_keyeme, fmo_keymod, fmo_keyfun, fmo_keyfmo)" & _
               " VALUES (@keyeme, @keymod, @keyfun, @keyfmo) "
    End Function

    Public Shared Function SG_DETALLES_ESTRUCTURAM_EVENTOS() As String
        Return " SELECT *, 'M' as tipo, '' as fun_consul, '' as fun_enuevo, '' as fun_elimin, '' as fun_recupe, " & _
               "        '' as fun_modifi, '' as fun_listar, '' as fun_export, '' as fun_grafic " & _
               "   FROM SG_DETALLES_ESTRUCTURAM LEFT JOIN SG_MODULOS ON mod_keymod = dem_keymod " & _
               "  WHERE dem_keyeme = @keyeme " & _
               "  UNION " & _
               " SELECT fmo_keyeme as dem_keyeme, fmo_keyfun as dem_keymod, fmo_keymod as dem_keypad, fmo_keyfmo as dem_keydem, " & _
               "        fun_keyfun as mod_keymod, fun_desfun as mod_desmod, fun_desfun as mod_descor, fun_status as mod_status, " & _
               "        fun_ayufun as mod_ayumod, '' as mod_iconoa, 'F' as tipo, fun_consul, fun_enuevo, fun_elimin, fun_recupe, " & _
               " 	    fun_modifi, fun_listar, fun_export, fun_grafic " & _
               "   FROM SG_FUNCIONES_MODULO LEFT JOIN SG_FUNCIONES ON fmo_keyfun = fun_keyfun " & _
               "  WHERE fmo_keyeme = @keyeme " & _
               "  ORDER BY dem_keypad "
    End Function

    Public Shared Function SG_DETALLES_PERFIL() As String
        Return " SELECT * FROM SG_DETALLES_PERFIL " & _
               "  WHERE dpf_keypfl =  @keypfl "
    End Function

    Public Shared Function SG_DETALLES_PERFIL_DEL() As String
        Return " DELETE FROM SG_DETALLES_PERFIL" & _
               "  WHERE DPF_KEYPFL = @KEYPFL "
    End Function

    Public Shared Function SG_DETALLES_PERFIL_INS() As String
        Return " INSERT INTO SG_DETALLES_PERFIL(DPF_KEYPFL,DPF_KEYEME,DPF_KEYMOD,DPF_KEYFUN,DPF_CONSUL)" & _
               "  VALUES(@KEYPFL,@KEYEME,@KEYMOD,@KEYFUN,@CONSUL)"
    End Function

    Public Shared Function SG_DETALLES_PERFIL_UPD() As String
        Return " UPDATE SG_DETALLES_PERFIL SET DPF_ENUEVO = @ENUEVO, DPF_ELIMIN = @ELIMIN, DPF_RECUPE = @RECUPE, DPF_MODIFI = @MODIFI, " & _
               "                               DPF_LISTAR = @LISTAR, DPF_EXPORT = @EXPORT, DPF_GRAFIC = @GRAFIC " & _
               "  WHERE DPF_KEYPFL = @KEYPFL " & _
               "    AND DPF_KEYEME = @KEYEME " & _
               "    AND DPF_KEYMOD = @KEYMOD " & _
               "    AND DPF_KEYFUN = @KEYFUN "
    End Function

End Class

Public Class InfoMail
    Public Property User As UserLogin
    Public Property MailFrom As String
    Public Property MailName As String
    Public Property MailTo As New List(Of String)
    Public Property MailBody As String
    Public Property MailAttach As New List(Of String)
    Public Property MailSmtpPort As Integer
    Public Property MailSmtpHost As String
    Public Property MailSmtpEnableSsl As Boolean
    Public Property MailSmtpCredenciales As New ColeccionPrmSql
End Class

Public Class Tools
    Shared Function ReadConexión(ByVal Name As String) As String
        ReadConexión = ""
        Try
            Return ConfigurationManager.ConnectionStrings(Name).ConnectionString
        Catch ex As Exception
            Dim s As String = ex.Message
        End Try
    End Function

    Overloads Shared Sub AddErrorLog(ByVal sFile As String, ByVal exo As Exception)
        Dim objStreamWriter As StreamWriter = New StreamWriter(sFile, True, System.Text.Encoding.Default)
        Try
            objStreamWriter.WriteLine(CStr(Now()) + exo.Message + " @ " + exo.TargetSite.Name + " @ " + exo.TargetSite.Module.Name)
        Catch ex As Exception
            Dim s As String = ex.Message
            MsgBox(s, MsgBoxStyle.Critical, "Error Rutina: AddErrorLog")
        Finally
            objStreamWriter.Close()
        End Try
    End Sub

    Overloads Shared Sub AddErrorLog(ByVal sFile As String, ByVal sMsj As String)
        Dim objStreamWriter As StreamWriter = New StreamWriter(sFile, True, System.Text.Encoding.Default)
        Try
            objStreamWriter.WriteLine(CStr(Now()) + sMsj)
        Catch ex As Exception
            Dim s As String = ex.Message
            MsgBox(s, MsgBoxStyle.Critical, "Error Rutina: AddErrorLog")
        Finally
            objStreamWriter.Close()
        End Try
    End Sub

    Shared Function EnvioMail(ByVal Inf As InfoMail) As Boolean
        Dim correo As New MailMessage
        Dim smtp As New SmtpClient()
        EnvioMail = False
        Try
            correo.From = New MailAddress(Inf.MailFrom, Inf.MailName, System.Text.Encoding.UTF8)
            For Each sTo As String In Inf.MailTo
                correo.To.Add(sTo)
            Next
            correo.SubjectEncoding = System.Text.Encoding.UTF8
            correo.Subject = Inf.MailName
            correo.Body = Inf.MailBody
            For Each sAtt As String In Inf.MailAttach
                correo.Attachments.Add(New Attachment(sAtt))
            Next
            correo.IsBodyHtml = False
            correo.Priority = MailPriority.High
            smtp.Credentials = New System.Net.NetworkCredential(Inf.MailSmtpCredenciales.ItemValue("@username").ToString, Inf.MailSmtpCredenciales.ItemValue("@password").ToString)
            smtp.Port = Inf.MailSmtpPort
            smtp.Host = Inf.MailSmtpHost
            smtp.EnableSsl = Inf.MailSmtpEnableSsl
            smtp.Send(correo)
            Return True
        Catch ex As Exception
            Dim s As String = ex.Message
            AddErrorLog(Inf.User.Mis.Log, ex.Message + vbCrLf + ex.TargetSite.ToString)
        End Try
    End Function

End Class

Public Class InfoRp
    Public Property Reporte As String
    Public Property Nombre As String
    Public Property InfoFiltros As String
    Public Property Parametros As New ColeccionPrmSql
    Public Property OD As New DataSet
    Public Property Enviado As Boolean
End Class

Module GeneralTools

    Public Function ReadConexión(ByVal Name As String) As String
        ReadConexión = ""
        Try
            Return ConfigurationManager.ConnectionStrings(Name).ConnectionString
        Catch ex As Exception
            Dim s As String = ex.Message
        End Try
    End Function

    Public Sub AddErrorLog(ByVal sFile As String, ByVal sErr As String)
        Dim objStreamWriter As StreamWriter = New StreamWriter(sFile, True, System.Text.Encoding.Default)
        Try
            objStreamWriter.WriteLine(CStr(Now()) + sErr)
        Catch ex As Exception
            Dim s As String = ex.Message
            MsgBox(s, MsgBoxStyle.Critical, "Error Rutina: AddErrorLog")
        Finally
            objStreamWriter.Close()
        End Try
    End Sub

    Public Function Decript(ByVal sWord As String) As String
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

    Public Function Encript(ByVal ws_pal_abr As String) As String
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

End Module