Imports Security_System

Public Class SQLPlagas
    Inherits SQL

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub

    Public ReadOnly Property List() As String
        Get
            Return " "
        End Get
    End Property

    Public ReadOnly Property ListTimbresFaltantes() As String
        Get
            Return " "
        End Get
    End Property

End Class



Public Class SQLPRGMONITOREO
    Inherits SQL

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub

    Public ReadOnly Property ListBasica() As String
        Get
            Return " "
        End Get
    End Property

    Public ReadOnly Property ListExport() As String
        Get
            Return " "
        End Get
    End Property

    Public ReadOnly Property List() As String
        Get
            Return " SELECT PrgMonitoreoID,TablaID,UbicacionNombre,C.CultivoID,CultivoNombre," &
                    " left(COALESCE(CultivoNombre,''),LEN(COALESCE(CultivoNombre,''))) +'  '+'->'+'  '+left(COALESCE(V.VariedadNombre,''),LEN(COALESCE(V.VariedadNombre,''))) as Cultivo," &
                    " V.VariedadID,VariedadNombre," &
                    " left(COALESCE(UbicacionNombre,''),LEN(COALESCE(UbicacionNombre,''))) +'  '+' '+'  '+left(COALESCE(ST.SectorTablaNombre,''),LEN(COALESCE(ST.SectorTablaNombre,''))) as Tabla," &
                    " left(COALESCE(CultivoNombre,''),LEN(COALESCE(CultivoNombre,''))) +'  '+'->'+'  '+left(COALESCE(VariedadNombre,''),LEN(COALESCE(VariedadNombre,''))) as Cultivo," &
                    " PM.SectorTablaID,ST.SectorTablaNombre,PrgMonitoreoHa,PrgMonitoreoFechaTransplante,PrgMonitoreoEstatus" &
                    " FROM PRGMONITOREO PM" &
                    " LEFT JOIN SECTORTABLA ST ON (ST.SectorTablaID=PM.SectorTablaID)" &
                    " LEFT JOIN VARIEDAD V ON (V.VariedadID = PM.VariedadID)" &
                    " LEFT JOIN CULTIVO C ON (C.CultivoID = V.CultivoID)" &
                    " LEFT JOIN UBICACIONES U ON (U.UbicacionID=PM.TablaID)" &
                    " WHERE PrgMonitoreoID LIKE @Folio " &
                    " And PrgMonitoreoFechaTransplante Like @Fecha " &
                    " And UbicacionNombre Like @Tabla " &
                    " And CultivoNombre Like @Cultivo " &
                    " And PrgMonitoreoHa Like @Has" &
                    " ORDER BY PrgMonitoreoID desc "
        End Get
    End Property

    Public ReadOnly Property Item() As String
        Get
            Return " SELECT * FROM PRGMONITOREO" &
                   " WHERE PrgMonitoreoID = @PrgMonitoreoID "
        End Get
    End Property

    Public ReadOnly Property keycul() As String
        Get
            Return " SELECT C.CultivoID,C.CultivoNombre,V.VariedadID,V.VariedadNombre" &
                    " FROM PRGMONITOREO P " &
                    " LEFT JOIN VARIEDAD V ON (V.VariedadID=P.VariedadID)" &
                    " LEFT JOIN CULTIVO C ON (V.CultivoID=C.CultivoID)" &
                    " WHERE P.PrgMonitoreoID = @PrgMonitoreoID "
        End Get
    End Property

    Public ReadOnly Property Delete() As String
        Get
            Return " DELETE FROM PRGMONITOREO" &
                    " WHERE PrgMonitoreoID = @PrgMonitoreoID "
        End Get
    End Property


End Class


Public Class SQLTablas
    Inherits SQL

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub

    Public ReadOnly Property ListCombo() As String
        Get
            Return " Select T.TablaID," &
                    " left(COALESCE(TB.UbicacionNombre,''),LEN(COALESCE(TB.UbicacionNombre,''))) +'  '+'->'+'  '+left(COALESCE(R.UbicacionNombre,''),LEN(COALESCE(R.UbicacionNombre,''))) +'  '+'->'+'  '+left(COALESCE(Z.ZonaNombre,''),LEN(COALESCE(Z.ZonaNombre,''))) as Tabla," &
                    " TB.UbicacionNombre,T.RanchoID,R.UbicacionID,T.ZonaID,Z.ZonaNombre" &
                    " FROM TABLAS T " &
                    " LEFT JOIN UBICACIONES TB ON (T.TablaID=TB.UbicacionID)" &
                    " LEFT JOIN UBICACIONES R ON (T.RanchoID=R.UbicacionID)" &
                    " LEFT JOIN ZONAS Z ON (Z.ZonaID=T.ZonaID)"
        End Get
    End Property

    Public ReadOnly Property List() As String
        Get
            Return " SELECT T.TablaID,TB.UbicacionNombre,T.RanchoID,R.UbicacionID,T.ZonaID,Z.ZonaNombre," &
                    " left(COALESCE(TB.UbicacionNombre,''),LEN(COALESCE(TB.UbicacionNombre,''))) +'  '+'->'+'  '+left(COALESCE(R.UbicacionNombre,''),LEN(COALESCE(R.UbicacionNombre,''))) +'  '+'->'+'  '+left(COALESCE(Z.ZonaNombre,''),LEN(COALESCE(Z.ZonaNombre,''))) as Tabla" &
                    " FROM TABLAS T " &
                    " LEFT JOIN UBICACIONES TB ON (T.TablaID=TB.UbicacionID)" &
                    " LEFT JOIN UBICACIONES R ON (T.RanchoID=R.UbicacionID)" &
                    " LEFT JOIN ZONAS Z ON (Z.ZonaID=T.ZonaID)"
        End Get
    End Property


    Public ReadOnly Property Item() As String
        Get
            Return " SELECT * FROM TABLAS"
        End Get
    End Property

    Public ReadOnly Property Update() As String
        Get
            Return " "
        End Get
    End Property


    Public ReadOnly Property Delete() As String
        Get
            Return " "
        End Get
    End Property

End Class


Public Class SQLSectorTabla
    Inherits SQL

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub

    Public ReadOnly Property ListCombo() As String
        Get
            Return " SELECT * FROM SECTORTABLA"
        End Get
    End Property

    Public ReadOnly Property List() As String
        Get
            Return " "
        End Get
    End Property


    Public ReadOnly Property Item() As String
        Get
            Return " "
        End Get
    End Property

    Public ReadOnly Property Update() As String
        Get
            Return " "
        End Get
    End Property


    Public ReadOnly Property Delete() As String
        Get
            Return " "
        End Get
    End Property

End Class


Public Class SQLEspecie
    Inherits SQL

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub

    Public ReadOnly Property ListCombo() As String
        Get
            Return " SELECT E.EspecieID,E.EspecieNombre,E.EspecieEstatus,COUNT(E.EspecieID) as NUMREG " &
                    " FROM ESPECIE E " &
                    " LEFT JOIN ESPECIECARACTERISTICA EC ON (E.EspecieID=EC.EspecieID) " &
                    " LEFT JOIN CULTIVOPLAGA CP On (CP.EspecieID=E.EspecieID) " &
                    " LEFT JOIN CULTIVO C ON (C.CultivoID=CP.CultivoID) " &
                    " WHERE CP.CultivoID = @CultivoID " &
                    " AND NOT EXISTS " &
                        " (SELECT * FROM PRGMONITOREOESPECIE PME " &
                        " WHERE PME.EspecieID=E.EspecieID AND PME.EspecieCaracteristicaID=EC.EspecieCaracteristicaID " &
                        " And PME.PrgMonitoreoID = @PrgMonitoreoID) " &
                    " GROUP BY E.EspecieID,E.EspecieNombre,E.EspecieEstatus "
        End Get
    End Property

    Public ReadOnly Property ListByCul() As String
        Get
            Return " SELECT E.EspecieID,EspecieNombre, C.CultivoID, C.CultivoNombre" &
                    " FROM ESPECIE E " &
                    " LEFT JOIN CULTIVOPLAGA CP ON (CP.EspecieID=E.EspecieID)" &
                    " LEFT JOIN CULTIVO C ON (C.CultivoID=CP.CultivoID)" &
                    " WHERE CP.CultivoID = 14"
        End Get
    End Property

    Public ReadOnly Property List() As String
        Get
            Return " SELECT * FROM ESPECIE E" &
                    " WHERE EspecieEstatus = 'A'" &
                    " AND EspecieID LIKE @EspecieID " &
                    " AND EspecieNombre LIKE @EspecieNombre"
        End Get
    End Property

    Public ReadOnly Property Item() As String
        Get
            Return " SELECT * FROM ESPECIE E" &
                    " WHERE EspecieEstatus = 'A'"
        End Get
    End Property

    Public ReadOnly Property Update() As String
        Get
            Return " "
        End Get
    End Property


    Public ReadOnly Property Delete() As String
        Get
            Return " "
        End Get
    End Property

End Class



Public Class SQLEspecieCaracteristica
    Inherits SQL

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub

    Public ReadOnly Property ListCombo() As String
        Get
            Return " SELECT left(COALESCE(EC.EspecieID,0),LEN(COALESCE(EC.EspecieID,0))) +''+'|'+''+left(COALESCE(EC.EspecieCaracteristicaID,0),LEN(COALESCE(EC.EspecieCaracteristicaID,0))) as ID, EspecieCaracteristicaNombre,EspecieCaracteristicaNivelCrit" &
                    " FROM ESPECIECARACTERISTICA EC" &
                    " LEFT JOIN ESPECIE E ON (E.EspecieID=EC.EspecieID)" &
                    " WHERE E.EspecieID = @EspecieID" &
                    " AND NOT EXISTS " &
                        " (SELECT * FROM PRGMONITOREOESPECIE PME" &
                        " WHERE PME.EspecieID=E.EspecieID " &
                        " And PME.EspecieCaracteristicaID = EC.EspecieCaracteristicaID" &
                        " And PME.PrgMonitoreoID = @PrgMonitoreoID)"
        End Get
    End Property

    Public ReadOnly Property List() As String
        Get
            Return " "
        End Get
    End Property


    Public ReadOnly Property Item() As String
        Get
            Return " "
        End Get
    End Property

    Public ReadOnly Property Update() As String
        Get
            Return " "
        End Get
    End Property


    Public ReadOnly Property Delete() As String
        Get
            Return " "
        End Get
    End Property

End Class



Public Class SQLProgMonitoreoEspecie
    Inherits SQL

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub

    Public ReadOnly Property ListCombo() As String
        Get
            Return " "
        End Get
    End Property

    Public ReadOnly Property List() As String
        Get
            Return " Select P.PrgMonitoreoID,E.EspecieID,EC.EspecieCaracteristicaID," &
                    " left(COALESCE(C.CultivoNombre,''),LEN(COALESCE(C.CultivoNombre,''))) +'  '+'->'+'  '+left(COALESCE(V.VariedadNombre,''),LEN(COALESCE(V.VariedadNombre,''))) as Cultivo," &
                    " E.EspecieNombre,EC.EspecieCaracteristicaNombre,EC.EspecieCaracteristicaNivelCrit" &
                    " FROM PRGMONITOREOESPECIE PME " &
                    " LEFT JOIN PRGMONITOREO P" &
                    " ON (PME.PrgMonitoreoID = P.PrgMonitoreoID)" &
                    " LEFT JOIN ESPECIECARACTERISTICA EC ON (PME.EspecieID=EC.EspecieID AND PME.EspecieCaracteristicaID=EC.EspecieCaracteristicaID)" &
                    " LEFT JOIN ESPECIE E ON (E.EspecieID=EC.EspecieID)" &
                    " LEFT JOIN VARIEDAD V On (V.VariedadID=P.VariedadID)" &
                    " LEFT JOIN CULTIVO C ON (C.CultivoID=V.CultivoID)" &
                    " WHERE P.PrgMonitoreoID = @PrgMonitoreoID" &
                    " AND E.EspecieNombre LIKE @EspecieNombre" &
                    " And EC.EspecieCaracteristicaNombre Like @Caracteristica" &
                    " ORDER BY EspecieNombre asc"
        End Get
    End Property

    Public ReadOnly Property ListDet() As String
        Get
            Return " Select P.PrgMonitoreoID,E.EspecieID,EC.EspecieCaracteristicaID," &
                    " left(COALESCE(C.CultivoNombre,''),LEN(COALESCE(C.CultivoNombre,''))) +'  '+'->'+'  '+left(COALESCE(V.VariedadNombre,''),LEN(COALESCE(V.VariedadNombre,''))) as Cultivo," &
                    " E.EspecieNombre,EC.EspecieCaracteristicaNombre" &
                    " FROM PRGMONITOREOESPECIE PME " &
                    " LEFT JOIN PRGMONITOREO P" &
                    " ON (PME.PrgMonitoreoID = P.PrgMonitoreoID)" &
                    " LEFT JOIN ESPECIECARACTERISTICA EC ON (PME.EspecieID=EC.EspecieID AND PME.EspecieCaracteristicaID=EC.EspecieCaracteristicaID)" &
                    " LEFT JOIN ESPECIE E ON (E.EspecieID=EC.EspecieID)" &
                    " LEFT JOIN VARIEDAD V On (V.VariedadID=P.VariedadID)" &
                    " LEFT JOIN CULTIVO C ON (C.CultivoID=V.CultivoID)" &
                    " WHERE P.PrgMonitoreoID = @PrgMonitoreoID" &
                    " UNION " &
                    " SELECT 0 AS PrgMonitoroID, 0 AS EspecieID, 0 AS EspecieCaracteristicaID, '' AS Cultivo, '' AS EspecieNombre, '' AS EspecieCaracteristicaNombre" &
                    " FROM PRGMONITOREOESPECIE P" &
                    " ORDER BY PrgMonitoreoID DESC"
        End Get
    End Property


    Public ReadOnly Property ListEncabezado() As String
        Get
            Return " SELECT PrgMonitoreoID," &
                    " left(COALESCE(UT.UbicacionNombre,''),LEN(COALESCE(UT.UbicacionNombre,''))) +'  '+'->'+'  '+left(COALESCE(UR.UbicacionNombre,''),LEN(COALESCE(UR.UbicacionNombre,''))) +'  '+'->'+'  '+left(COALESCE(Z.ZonaNombre,''),LEN(COALESCE(Z.ZonaNombre,''))) as Tabla," &
                    " left(COALESCE(CultivoNombre,''),LEN(COALESCE(CultivoNombre,''))) +'  '+'->'+'  '+left(COALESCE(VariedadNombre,''),LEN(COALESCE(VariedadNombre,''))) as Cultivo," &
                    " PrgMonitoreoHa,PrgMonitoreoFechaTransplante,PrgMonitoreoEstatus" &
                    " FROM PRGMONITOREO P" &
                    " LEFT JOIN TABLAS T ON (T.TablaID=P.TablaID)" &
                    " LEFT JOIN UBICACIONES UT ON (UT.UbicacionID=T.TablaID)" &
                    " LEFT JOIN UBICACIONES UR ON (UR.UbicacionID=T.RanchoID)" &
                    " LEFT JOIN ZONAS Z ON (Z.ZonaID=T.ZonaID)" &
                    " LEFT JOIN VARIEDAD V ON (V.VariedadID=P.VariedadID)" &
                    " LEFT JOIN CULTIVO C ON (C.CultivoID=V.CultivoID)" &
                    " LEFT JOIN SECTORTABLA ST ON (ST.SectorTablaID=P.SectorTablaID)" &
                    " WHERE P.PrgMonitoreoID = @PrgMonitoreoID"
        End Get
    End Property

    Public ReadOnly Property Item() As String
        Get
            Return " SELECT * FROM PRGMONITOREOESPECIE" &
                    " WHERE PrgMonitoreoID = @PrgMonitoreoID AND EspecieID = @EspecieID AND EspecieCaracteristicaID = @CaracteristicaID"
        End Get
    End Property

    Public ReadOnly Property Update() As String
        Get
            Return " "
        End Get
    End Property


    Public ReadOnly Property Delete() As String
        Get
            Return " DELETE FROM PRGMONITOREOESPECIE " &
                   " WHERE PrgMonitoreoID = @PrgMonitoreoID AND EspecieID = @EspecieID AND EspecieCaracteristicaID = @CaracteristicaID"
        End Get
    End Property

End Class