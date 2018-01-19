Imports Security_System

Public Class SQLFletes
    Inherits SQL
    Private Ds As New DataSet
    Private m_sLog As String

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
        m_sLog = oUsr.Mis.Log
    End Sub

    Public ReadOnly Property ListaFlete As String
        Get
            Return " select FleteID, FleteObservacion, CONVERT(char(10), FleteFecha, 103) as FleteFecha, TipoFleteNombre, rut_desrut, TransportistaNombre, " &
                   " (' [N ECO] ' + dbo.VCHARLEFT(CamionEconomico) + ' [PLC] ' + dbo.VCHARLEFT(CamionPlacas)) as cam_descam, " &
                   " OperadorNombre + ' ' + OperadorApellidos as Operador, " &
                   " f.RutaID, f.CamionID, f.OperadorID " &
                   " from FLETE f join TIPOFLETE tf on f.TipoFleteID=tf.TipoFleteID " &
                   " join VW_RUTAS vr on f.RutaID=vr.RutaID " &
                   " join TRANSPORTISTA t on f.TransportistaID=t.TransportistaID " &
                   " join TRANSPORTISTACAMION tc on f.CamionID=tc.CamionID " &
                   " join TRANSPORTISTAOPERADOR op on f.OperadorID=op.OperadorID " &
                   " where FleteUsuario = @IDUser " &
                   " order by FleteFechaRegistro desc "
        End Get
    End Property

    Public ReadOnly Property ListaTipo As String
        Get
            Return "select tipofleteid, tipofletenombre from tipoflete"
        End Get
    End Property

    Public ReadOnly Property ListaProveedor() As String
        Get
            Return " SELECT UbicacionID, dbo.GETPATHUBICA(UbicacionID) as ubi_desubi, TipoUbicaID, UbicacionPadreID " &
                   " FROM UBICACIONES " &
                   " WHERE NOT UbicacionPadreID IS NULL "
        End Get
    End Property

    Public ReadOnly Property ListaOrigen() As String
        Get
            Return " WITH ComentariosDescendientes(UbicacionID, TipoUbicaID, UbicacionPadreID, UbicacionNombre, Nivel) AS " &
                   " ( " &
                   " SELECT UbicacionID, TipoUbicaID, UbicacionPadreID, UbicacionNombre, 0 " &
                   " FROM UBICACIONES " &
                   " WHERE UbicacionPadreID = @proveedor " &
                   " UNION ALL " &
                   " SELECT u.UbicacionID, u.TipoUbicaID, u.UbicacionPadreID, u.UbicacionNombre, Nivel + 1 " &
                   " FROM UBICACIONES u INNER JOIN ComentariosDescendientes cd " &
                   " ON u.UbicacionPadreID = cd.UbicacionID " &
                   " ) " &
                   " SELECT UbicacionID, dbo.GETPATHUBICA(UbicacionID) as ubi_desubi, TipoUbicaID, UbicacionPadreID " &
                   " FROM ComentariosDescendientes "
        End Get
    End Property

    Public ReadOnly Property ListaRuta As String
        Get
            Return " SELECT * FROM VW_RUTAS WHERE RutaStatus = @status AND dbo.GETHEREDA(OrigenID, @keyorg) = 1 " &
                   " AND OrigenID like 'R%' AND DestinoID Like 'R%' "
        End Get
    End Property

    Public ReadOnly Property ListaCamion As String
        Get
            Return " SELECT CamionID, (dbo.VCHARLEFT(TransportistaNombre) + ' [N ECO] ' + dbo.VCHARLEFT(CamionEconomico) + ' [PLC] ' + dbo.VCHARLEFT(CamionPlacas)) as cam_descam " &
                   "   FROM TRANSPORTISTACAMION A LEFT JOIN TRANSPORTISTA B ON B.TransportistaID = A.TransportistaID " &
                   "  WHERE EXISTS (SELECT * FROM TIPOFLETETRANSPORTISTA C WHERE C.TransportistaID = B.TransportistaID AND TipoFleteID = @keytpf ) "
        End Get
    End Property

    Public ReadOnly Property ListaResponsable As String
        Get
            Return "select * from responsables"
        End Get
    End Property

    Public ReadOnly Property ListaOperador As String
        Get
            Return " SELECT OperadorID, dbo.VCHARLEFT(OperadorApellidos) + ' ' + dbo.VCHARLEFT(OperadorNombre) as ope_nomful FROM TRANSPORTISTAOPERADOR " &
                   " WHERE TransportistaID = (SELECT TransportistaID FROM TRANSPORTISTACAMION WHERE CamionID = @keycam) "
        End Get
    End Property

    Public ReadOnly Property ListaContenedor As String
        Get
            Return " select e.EnvaseID, ue.UnidadEmpaqueNombre " &
                   " from ENVASES e join UNIDADEMPAQUE ue on e.EnvaseID=ue.UnidadEmpaqueID "
        End Get
    End Property

    Public ReadOnly Property ListaCalidad As String
        Get
            Return " select * from CLASIFICASIZE "
        End Get
    End Property

    Public ReadOnly Property ListaVariedad As String
        Get
            Return " select v.VariedadID, c.CultivoNombre + ' - ' + v.VariedadNombre as VariedadNombre " &
                   " from VARIEDAD v join CULTIVO c on v.CultivoID=c.CultivoID " &
                   " where VariedadStatus='A' and v.CultivoID IN (1,11) " &
                   " order by VariedadNombre "
        End Get
    End Property

    Public ReadOnly Property ValUser As String
        Get
            Return " select SgUserEmail from SGUSUARIOS u join TIPOFLETEUSER f on u.SgUserID = f.UserFleteIDS " &
                   " where f.TipoFleteID = @tipoflete and f.UserFleteIDS = @userid "
        End Get
    End Property

    Public ReadOnly Property ValProveedor As String
        Get
            Return " select UbicacionPadreID from UBICACIONES where TipoUbicaID='O' AND UbicacionID = @prv "
        End Get
    End Property

    Public ReadOnly Property ValOrigen As String
        Get
            Return " select OrigenID from VW_RUTAS where RutaID = @ruta "
        End Get
    End Property

    Public ReadOnly Property ValRutaID As String
        Get
            Return " SELECT RutaID FROM FLETE WHERE FleteID = @flete "
        End Get
    End Property

    Public ReadOnly Property ValFleteID As String
        Get
            Return " SELECT ParametroEntrada FROM BASCULAPARAMETROS WHERE ParametroID = 1 AND BasculaIDF = @IDF "
        End Get
    End Property

    Public ReadOnly Property ValOperadorID As String
        Get
            Return " SELECT ParametroEntrada FROM BASCULAPARAMETROS WHERE ParametroID = 2 AND BasculaIDF = @IDF "
        End Get
    End Property

    Public ReadOnly Property ValRuta As String
        Get
            Return " select OrigenID from RUTA where RutaID = @ruta "
        End Get
    End Property

    Public ReadOnly Property ValPrv As String
        Get
            Return " WITH Padres(UbicacionID, TipoUbicaID, UbicacionPadreID, UbicacionNombre, Nivel) AS " &
                   " ( " &
                   " SELECT UbicacionID, TipoUbicaID, UbicacionPadreID, UbicacionNombre, 0 " &
                   " FROM UBICACIONES " &
                   " WHERE UbicacionID = (SELECT UbicacionPadreID FROM UBICACIONES Where UbicacionID = @ruta) " &
                   " UNION ALL " &
                   " SELECT u.UbicacionID, u.TipoUbicaID, u.UbicacionPadreID, u.UbicacionNombre, Nivel + 1 " &
                   " FROM UBICACIONES u INNER JOIN Padres p " &
                   " ON u.UbicacionPadreID = p.UbicacionID " &
                   " ) " &
                   " SELECT UbicacionID " &
                   " FROM Padres where TipoUbicaID='O' and Nivel = 0 "
        End Get
    End Property

    Public ReadOnly Property ValTransportista As String
        Get
            Return " select TransportistaID from TRANSPORTISTACAMION " &
                   " where CamionID = @camionid "
        End Get
    End Property

    Public ReadOnly Property Item As String
        Get
            Return " select * from BASCULAPARAMETROS where ParametroID=1 and ParametroEntrada=@Flete "
        End Get
    End Property

    Public ReadOnly Property DelFlete As String
        Get
            Return " delete from FLETE where FleteID = @IDF "
        End Get
    End Property

    Public ReadOnly Property DelFleteCosecha As String
        Get
            Return " Delete from FLETECOSECHA where FleteID = @IDF "
        End Get
    End Property

    Public Function _Item(ByVal sTabla As String, ByVal oCs As ColeccionPrmSql, ByVal Qry As String) As DataTable
        _Item = Nothing
        Try
            If GetQry(Ds, sTabla, Qry, oCs) Then
                Return Ds.Tables(sTabla)
            End If

        Catch ex As Exception
            Tools.AddErrorLog(m_sLog, ex)
        End Try
    End Function
End Class

Public Class SQLFleteDetalle
    Inherits SQL
    Private Ds As New DataSet
    Private m_sLog As String

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
        m_sLog = oUsr.Mis.Log
    End Sub

    Public ReadOnly Property NextID As String
        Get
            Return "SELECT NEXT VALUE FOR SEQ_FLETE_ID AS NEXTID"
        End Get
    End Property

    Public ReadOnly Property NextIDD As String
        Get
            Return "SELECT NEXT VALUE FOR SEQ_FLETE_DETALLE AS NEXTID"
        End Get
    End Property

    Public ReadOnly Property Item As String
        Get
            Return " select * from FLETE where FleteID = @keyfle"
        End Get
    End Property

    Public ReadOnly Property ItemD As String
        Get
            Return "select * from FLETECOSECHA where FleteID = @keyf AND CosechaID = @keydfl"
        End Get
    End Property

    Public ReadOnly Property DelDetalle As String
        Get
            Return " delete from FLETECOSECHA where FleteID = @keyf AND CosechaID = @IDF "
        End Get
    End Property

    Public Function _Item(ByVal sTabla As String, ByVal oCs As ColeccionPrmSql) As DataTable
        _Item = Nothing
        Try
            If GetQry(Ds, sTabla, Item, oCs) Then
                Return Ds.Tables(sTabla)
            End If

        Catch ex As Exception
            Tools.AddErrorLog(m_sLog, ex)
        End Try
    End Function

    Public Function _ItemD(ByVal sTabla As String, ByVal oCs As ColeccionPrmSql) As DataTable
        _ItemD = Nothing
        Try
            If GetQry(Ds, sTabla, ItemD, oCs) Then
                Return Ds.Tables(sTabla)
            End If

        Catch ex As Exception
            Tools.AddErrorLog(m_sLog, ex)
        End Try
    End Function

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

    Public ReadOnly Property Lista As String
        Get
            Return " SELECT fc.CosechaID, u.UbicacionNombre, v.VariedadNombre, CosechaCantidad, CosechaObservacion, " &
                   "       cs.ClasificaSizeNombre, ue.UnidadEmpaqueNombre " &
                   " FROM FLETECOSECHA fc join UBICACIONES u on fc.UbicacionID=u.UbicacionID " &
                   "     join VARIEDAD v on v.VariedadID=fc.VariedadID " &
                   " 	 join CLASIFICASIZE cs on cs.ClasificaSizeID=fc.ClasificaSizeID " &
                   "     join ENVASES e on e.EnvaseID=fc.EnvaseID " &
                   "     join UNIDADEMPAQUE ue on e.EnvaseID=ue.UnidadEmpaqueID " &
                   " where fc.FleteID = @keyfle "
        End Get
    End Property

    Public Function _Lista(ByVal sTabla As String, ByVal oCs As ColeccionPrmSql) As DataTable
        _Lista = Nothing
        Try
            If GetQry(Ds, sTabla, Lista, oCs) Then
                Return Ds.Tables(sTabla)
            End If

        Catch ex As Exception
            Tools.AddErrorLog(m_sLog, ex)
        End Try
    End Function

    Public Function _Valor(ByVal sQry As String, ByVal oCs As ColeccionPrmSql) As Object
        _Valor = Nothing
        Try
            If GetQry(Ds, "VALOR", sQry, oCs) Then
                Return Ds.Tables("VALOR").Rows(0).Item(oCs.ItemValue("_VALOR"))
            End If
        Catch ex As Exception
            Tools.AddErrorLog(m_sLog, ex)
        End Try
    End Function
End Class

Public Class SQLFletesAjo
    Inherits SQL
    Private m_sLog As String
    Private Ds As New DataSet

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
        m_sLog = oUsr.Mis.Log
    End Sub

    Public ReadOnly Property List() As String
        Get
            Return " select FleteID, FleteFechaRegistro, TipoFleteNombre, Proveedor, Rancho, UbicacionNombre, " &
                   " CultivoNombre, VariedadNombre, CosechaObservacion, UnidadEmpaqueNombre, CantEnviada, CantRecibida, " &
                   " ClasificaSizeNombre, TransportistaNombre, PBruto, PTara, PEnvaseRecibido, Ticket " &
                   "   FROM VW_FLETE_FULL " &
                   "  WHERE FleteFechaRegistro BETWEEN @fecini AND @fecfin " &
                   "    AND Proveedor LIKE @Proveedor " &
                   "    AND Rancho LIKE @Rancho " &
                   " ORDER BY FleteID "
        End Get
    End Property


End Class
