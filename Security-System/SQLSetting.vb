Public Class SQLSetting
    Inherits SQL
    Private m_sLog As String
    Private Ds As New DataSet

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
        m_sLog = oUsr.Mis.Log
    End Sub

    Public ReadOnly Property Lista As String
        Get
            Return "SELECT * FROM SETTINGS "
        End Get
    End Property

    Public ReadOnly Property Item As String
        Get
            Return "SELECT * FROM SETTINGS WHERE set_secion = @secion AND set_sname = @sname"
        End Get
    End Property

    Public Function _Item(ByVal sTabla As String, ByVal oCs As ColeccionPrmSql) As DataTable
        _Item = Nothing
        Try
            If GetQry(Ds, sTabla, Item, oCs) Then
                Return Ds.Tables(sTabla)
            End If

        Catch ex As Exception
            AddErrorLog(m_sLog, ex.Message)
        End Try
    End Function

End Class

Public Class SQLEstructuras
    Inherits SQL
    Private m_sLog As String
    Private Ds As New DataSet

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
        m_sLog = oUsr.Mis.Log
    End Sub

    Public ReadOnly Property List() As String
        Get
            Return " SELECT * FROM SGESTRUCTURA_MENU "
        End Get
    End Property

    Public ReadOnly Property Item() As String
        Get
            Return " SELECT * FROM SGESTRUCTURA_MENU WHERE EstructuraMenuID = @EstructuraMenuID "
        End Get
    End Property

    Public ReadOnly Property InsertED() As String
        Get
            Return " INSERT INTO SGESTRUCTURASM_DETALLE(EstructuraMenuID, ModuloID, ModuloPadreID, EstructuraMenuOrden) " &
                   "  VALUES(@EstructuraMenuID, @ModuloID, @ModuloPadreID, @EstructuraMenuOrden) "
        End Get
    End Property

    Public ReadOnly Property DeleteED() As String
        Get
            Return " DELETE FROM SGESTRUCTURASM_DETALLE " &
                   "  WHERE EstructuraMenuID = @EstructuraMenuID " &
                   "    AND ModuloID = @ModuloID "
        End Get
    End Property

    Public ReadOnly Property InsertFM() As String
        Get
            Return " INSERT INTO SGFUNCIONES_MODULO(EstructuraMenuID, ModuloID, SGFuncionID, SGFuncionModuloOrden)" & _
                   "  VALUES(@EstructuraMenuID, @ModuloID, @SGFuncionID, @SGFuncionModuloOrden) "
        End Get
    End Property

    Public ReadOnly Property DeleteFM() As String
        Get
            Return " DELETE FROM SGFUNCIONES_MODULO " & _
                   "  WHERE EstructuraMenuID = @EstructuraMenuID " & _
                   "    AND ModuloID = @ModuloID " & _
                   "    AND SGFuncionID = @SGFuncionID "
        End Get
    End Property

    Public ReadOnly Property ItemMaxChild() As String
        Get
            Return " SELECT MAX(EstructuraMenuOrden) MaxChild FROM SGESTRUCTURASM_DETALLE " &
                   "  WHERE EstructuraMenuID = @EstructuraMenuID " &
                   "    AND ModuloPadreID = @ModuloPadreID "
        End Get
    End Property

    Public ReadOnly Property ItemMaxChildFuncion() As String
        Get
            Return " SELECT MAX(SGFuncionModuloOrden) MaxChild FROM SGFUNCIONES_MODULO " & _
                   "  WHERE EstructuraMenuID = @EstructuraMenuID " & _
                   "    AND ModuloID = @ModuloID "
        End Get
    End Property

    Public ReadOnly Property ListItemsDisponibles() As String
        Get
            Return " SELECT ModuloID NodoID, Modulo + ' - M' Nodo, 'M' NodoTipo  FROM SGMODULOS M " &
                   "  WHERE NOT EXISTS (SELECT * FROM SGESTRUCTURASM_DETALLE ED WHERE ED.EstructuraMenuID = @EstructuraMenuID AND ED.ModuloID = M.ModuloID) " &
                   "  UNION " &
                   " SELECT SGFuncionID, SGFuncion + ' - F', 'F' Tipo  FROM SGFUNCIONES F " &
                   "  WHERE NOT EXISTS(SELECT * FROM SGFUNCIONES_MODULO FM WHERE FM.EstructuraMenuID = @EstructuraMenuID AND FM.SGFuncionID = F.SGFuncionID) "
        End Get
    End Property

    Public ReadOnly Property ListDestalle() As String
        Get
            Return " SELECT EstructuraMenuID, ED.ModuloID, ModuloPadreID, EstructuraMenuOrden, Modulo, ModuloAyuda, ModuloNombre, 'M' as Nodo " &
                   "   FROM SGESTRUCTURASM_DETALLE ED LEFT JOIN SGMODULOS M ON M.ModuloID = ED.ModuloID " &
                   "  WHERE EstructuraMenuID = @keyeme " &
                   "  UNION " &
                   " SELECT EstructuraMenuID, FM.SGFuncionID ModuloID, ModuloID ModuloPadreID, SGFuncionModuloOrden EstructuraMenuOrden, " &
                   "       SGFuncion Modulo, SGFuncionAyuda ModuloAyuda, SGFuncion ModuloNombre, 'F' as Nodo " &
                   "   FROM SGFUNCIONES_MODULO FM LEFT JOIN SGFUNCIONES F ON F.SGFuncionID = FM.SGFuncionID " &
                   "  WHERE EstructuraMenuID = @keyeme " &
                   "  ORDER BY ModuloPadreID "
        End Get
    End Property

End Class

Public Class SQLFunciones
    Inherits SQL
    Private m_sLog As String
    Private Ds As New DataSet

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
        m_sLog = oUsr.Mis.Log
    End Sub

    Public ReadOnly Property List() As String
        Get
            Return " SELECT * FROM SGFUNCIONES WHERE SGFuncionEstatus = @SGFuncionEstatus " &
                   "   AND SGFuncion LIKE @SGFuncion "
        End Get
    End Property

    Public ReadOnly Property Item() As String
        Get
            Return " SELECT * FROM SGFUNCIONES WHERE SGFuncionID = @SGFuncionID "
        End Get
    End Property

    Public ReadOnly Property Insert() As String
        Get
            Return ""
        End Get
    End Property

    Public ReadOnly Property Update() As String
        Get
            Return ""
        End Get
    End Property

End Class
Public Class SQLSubFunciones
    Inherits SQL
    Private m_sLog As String
    Private Ds As New DataSet

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
        m_sLog = oUsr.Mis.Log
    End Sub

    Public ReadOnly Property ListSubF() As String
        Get
            Return " SELECT * FROM SGFUNCIONESSUBFUNCION SGFS " &
                    "   LEFT JOIN SGFUNCIONES SGF ON SGF.SGFuncionID=SGFS.SGFuncionID  " &
                    " WHERE SGF.SGFuncionEstatus = @SGFuncionEstatus  AND SGFS.SGFuncionID =@SGFuncionID " &
                    "   AND SGFS.SGSubFuncionID LIKE @SGSubFuncionID "
        End Get
    End Property

    Public ReadOnly Property GetSubF() As String
        Get
            Return " SELECT * FROM SGFUNCIONESSUBFUNCION WHERE SGFuncionID = @SGFuncionID AND SGSubFuncionID=@SGSubFuncionID"
        End Get
    End Property

    Public ReadOnly Property GetValidaFuncPRIM() As String
        Get
            Return " SELECT SGFuncionPrimaria FROM SGFUNCIONES WHERE SGFuncionID = @SGFuncionID "
        End Get
    End Property

    Public ReadOnly Property GetValidaFuncMD() As String
        Get
            Return " SELECT SGFuncionMaestroDetalle FROM SGFUNCIONES WHERE SGFuncionID = @SGFuncionID "
        End Get
    End Property

    'Public ReadOnly Property ItemFuncion() As String
    '    Get
    '        Return " SELECT SGFuncionID,SGFuncion FROM SGFUNCIONES "
    '    End Get
    'End Property

    Public ReadOnly Property ItemFuncion() As String
        Get
            Return " SELECT F.SGFuncionID,F.SGFuncion FROM SGFUNCIONES F WHERE F.SGFuncionPrimaria='N' AND F.SGFuncionMaestroDetalle='N' " &
                   " AND NOT EXISTS (SELECT * FROM SGFUNCIONESSUBFUNCION SF WHERE SF.SGSubFuncionID=F.SGFuncionID ) "
        End Get
    End Property

    Public ReadOnly Property ReItemFuncion() As String
        Get
            Return " SELECT * FROM SGFUNCIONESSUBFUNCION   "
        End Get
    End Property

    Public ReadOnly Property InsertSubF() As String
        Get
            Return "SELECT * FROM SGFUNCIONESSUBFUNCION WHERE SGFuncionID = @SGFuncionID"
        End Get
    End Property

    Public ReadOnly Property UpdateSubF() As String
        Get
            Return "SELECT * FROM SGFUNCIONESSUBFUNCION WHERE SGFuncionID = @SGFuncionID AND SGSubFuncionID=@SGSubFuncionID"
        End Get
    End Property

    Public ReadOnly Property DeleteSubF() As String
        Get
            Return "DELETE FROM SGFUNCIONESSUBFUNCION WHERE SGFuncionID = @SGFuncionID AND SGSubFuncionID=@SGSubFuncionID"
        End Get
    End Property

    Public Function _Valor(ByVal sQry As String, ByVal sField As String, ByVal oCs As ColeccionPrmSql) As Object
        _Valor = Nothing
        Try
            If GetQry(Ds, "VALOR", sQry, oCs) Then
                Return Ds.Tables("VALOR").Rows(0).Item(sField)
            End If
        Catch ex As Exception
            Tools.AddErrorLog(m_sLog, ex)
        End Try
    End Function

End Class
Public Class SQLFuncionesEventos
    Inherits SQL
    Private m_sLog As String
    Private Ds As New DataSet

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
        m_sLog = oUsr.Mis.Log
    End Sub

    Public ReadOnly Property ListEventosEspeciales() As String
        Get
            Return " SELECT * FROM SGFUNCIONESEVENTOSESP WHERE SGFuncionID = @SGFuncionID "
        End Get
    End Property

    Public ReadOnly Property Item() As String
        Get
            Return " SELECT * FROM SGFUNCIONESEVENTOSESP WHERE SGFuncionID = @SGFuncionID " &
                   "    AND SGFuncionEventoID = @SGFuncionEventoID "
        End Get
    End Property



End Class

Public Class SQLModulos
    Inherits SQL
    Private m_sLog As String
    Private Ds As New DataSet

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
        m_sLog = oUsr.Mis.Log
    End Sub

    Public ReadOnly Property List() As String
        Get
            Return " SELECT * FROM SGMODULOS WHERE ModuloEstatus = @ModuloEstatus "
        End Get
    End Property

    Public ReadOnly Property Item() As String
        Get
            Return " SELECT * FROM SGMODULOS WHERE ModuloID = @ModuloID "
        End Get
    End Property

    Public ReadOnly Property Insert() As String
        Get
            Return ""
        End Get
    End Property

    Public ReadOnly Property Update() As String
        Get
            Return ""
        End Get
    End Property

End Class

Public Class SQLPerfiles
    Inherits SQL
    Private m_sLog As String
    Private Ds As New DataSet

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
        m_sLog = oUsr.Mis.Log
    End Sub

    Public ReadOnly Property List() As String
        Get
            Return " SELECT * FROM SGPERFILES " &
                   "  WHERE PerfilNombre LIKE @PerfilNombre "
        End Get
    End Property

    Public ReadOnly Property Item() As String
        Get
            Return " SELECT * FROM SGPERFILES WHERE PerfilID = @PerfilID "
        End Get
    End Property

    Public ReadOnly Property Delete() As String
        Get
            Return " DELETE FROM SGPERFILES WHERE PerfilID = @PerfilID "
        End Get
    End Property

End Class

Public Class SQLDetallesPerfil
    Inherits SQL
    Private m_sLog As String
    Private Ds As New DataSet

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
        m_sLog = oUsr.Mis.Log
    End Sub

    Public ReadOnly Property List() As String
        Get
            Return " SELECT * FROM SGPERFILES_DETALLE WHERE PerfilID = @PerfilID "
        End Get
    End Property

    Public ReadOnly Property Item() As String
        Get
            Return " SELECT * FROM SGPERFILES_DETALLE WHERE PerfilID = @PerfilID AND ModuloID = @ModuloID AND SGFuncionID = @SGFuncionID "
        End Get
    End Property

    Public ReadOnly Property Delete() As String
        Get
            Return " DELETE FROM SGPERFILES_DETALLE WHERE PerfilID = @PerfilID AND ModuloID = @ModuloID AND SGFuncionID = @SGFuncionID "
        End Get
    End Property

    Public ReadOnly Property Del_DetallePerfil() As String
        Get
            Return " DELETE FROM SGPERFILES_DETALLE WHERE PerfilID = @PerfilID "
        End Get
    End Property

    Public ReadOnly Property Upd_DetallePerfil() As String
        Get
            Return " UPDATE SGPERFILES_DETALLE SET PerfilConsultar = @PerfilConsultar, PerfilNuevo = @PerfilNuevo,         PerfilEliminar = @PerfilEliminar, " &
                   "                               PerfilRecuperar = @PerfilRecuperar, PerfilModificar = @PerfilModificar, PerfilListar = @PerfilListar, " &
                   "							   PerfilExportar =  @PerfilExportar,  PerfilGraficar = @PerfilGraficar,   PerfilEspecial10 = @PerfilEspecial10, " &
                   "							   PerfilEspecial9 = @PerfilEspecial9, PerfilEspecial8 = @PerfilEspecial8, PerfilEspecial7 = @PerfilEspecial7, " &
                   "                               PerfilEspecial6 = @PerfilEspecial6, PerfilEspecial5 = @PerfilEspecial5, PerfilEspecial4 = @PerfilEspecial4, " &
                   "							   PerfilEspecial3 = @PerfilEspecial3, PerfilEspecial2 = @PerfilEspecial2, PerfilEspecial1 = @PerfilEspecial1 " &
                   "  WHERE PerfilID = @PerfilID " &
                   "    AND ModuloID = @ModuloID " &
                   "    AND SGFuncionID = @SGFuncionID "
        End Get
    End Property

    Public ReadOnly Property Ins_DetallePerfil() As String
        Get
            Return " INSERT INTO SGPERFILES_DETALLE(PerfilID, ModuloID, SGFuncionID, PerfilConsultar, PerfilNuevo, " &
                   "             PerfilEliminar, PerfilRecuperar, PerfilModificar, PerfilListar, PerfilExportar, " &
                   "             PerfilGraficar, PerfilEspecial10, PerfilEspecial9, PerfilEspecial8, PerfilEspecial7, " &
                   "             PerfilEspecial6, PerfilEspecial5, PerfilEspecial4, PerfilEspecial3, PerfilEspecial2, " &
                   "             PerfilEspecial1) " &
                   " VALUES(@PerfilID, @ModuloID, @SGFuncionID, @PerfilConsultar, @PerfilNuevo, " &
                   "        @PerfilEliminar, @PerfilRecuperar, @PerfilModificar, @PerfilListar, @PerfilExportar, " &
                   "        @PerfilGraficar, @PerfilEspecial10, @PerfilEspecial9, @PerfilEspecial8, @PerfilEspecial7, " &
                   "        @PerfilEspecial6, @PerfilEspecial5, @PerfilEspecial4, @PerfilEspecial3, @PerfilEspecial2, " &
                   "        @PerfilEspecial1) "
        End Get
    End Property

End Class
Public Class SQLUsuarios
    Inherits SQL
    Private m_sLog As String
    Private Ds As New DataSet

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
        m_sLog = oUsr.Mis.Log
    End Sub

    Public ReadOnly Property ListUsuarios1() As String
        Get
            Return " SELECT S.SgUserID,S.SgUserName,S.SgUserEmail,S.SgUserAlta,PerfilNombre,S.SgApellidos,S.SgNombre " &
                      "  FROM SGUSUARIOS S " &
                      "  LEFT JOIN SGPERFILES P ON P.PerfilID =S.PerfilID  "
        End Get
    End Property

    Public ReadOnly Property ItemSearchPerfilID() As String
        Get
            Return " SELECT PerfilID,PerfilNombre FROM SGPERFILES "
        End Get
    End Property

    Public ReadOnly Property ItemPerfilID() As String
        Get
            Return " SELECT PerfilID,PerfilNombre FROM SGPERFILES "
        End Get
    End Property

    Public ReadOnly Property GetUser() As String
        Get
            Return " SELECT * FROM SGUSUARIOS WHERE SgUserID=@SgUserID "
        End Get
    End Property

    Public ReadOnly Property UpdateUser() As String
        Get
            Return " SELECT * FROM SGUSUARIOS WHERE SgUserID=@SgUserID "
        End Get
    End Property

End Class

