Imports Security_System

Public Class SQLBascula
    Inherits Sql

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub

    Public ReadOnly Property List() As String
        Get
            Return " select e.EmpresaID, EmpresaNombre, BasculaIDF, b.ProcesoID," &
                   " p.ProcesoNombre, CONVERT(char(10), BasculaFecha, 103) as Fecha, " &
                   " BasculaPesoBruto, BasculaPesoTara, " &
                   " (BasculaPesoBruto - BasculaPesoTara) as PesoNeto, " &
                   " CONVERT(varchar, BasculaFechaRegistro, 100) as FechaRegistro " &
                   " from bascula b join EMPRESAS e on b.EmpresaID = e.EmpresaID " &
                   " join PROCESOS p on b.ProcesoID = p.ProcesoID " &
                   " where BasculaIDF LIKE @EmpIDF " &
                   " AND BasculaFecha BETWEEN @fecini AND @fecfin " &
                   " ORDER BY BasculaFechaRegistro DESC"
        End Get
    End Property
End Class

Public Class SQLCargarDatos
    Inherits Sql

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub

    Public ReadOnly Property ItemFolio() As String
        Get
            Return "select dbo.sig_folio(@fol) as folio"
        End Get
    End Property

    Public ReadOnly Property ItemOperador() As String
        Get
            Return " SELECT OperadorID FROM FLETE where FleteID = @flete "
        End Get
    End Property

    Public ReadOnly Property ValddlAdd() As String
        Get
            Return " select ParametroEntrada from BASCULAPARAMETROS " &
                   " where EmpresaID=@IDE and BasculaIDF=@IDF and ParametroID=@IDP "
        End Get
    End Property

    Public ReadOnly Property CargaDatos() As String
        Get
            Return " select p.ProcesoNombre, p.EmpresaID, e.EmpresaNombre, p.UbicacionDestinoID " &
                   " from PROCESOS p join EMPRESAS e on p.EmpresaID = e.EmpresaID " &
                   " where p.ProcesoID = @keyproceso "
        End Get
    End Property

    Public ReadOnly Property CargaDatosUpd() As String
        Get
            Return " select b.EmpresaID, e.EmpresaNombre, BasculaIDF, b.ProcesoID, p.ProcesoNombre, " &
                   " b.BasculaFecha, b.BasculaPesoBruto, b.BasculaPesoTara, " &
                   " (BasculaPesoBruto-BasculaPesoTara) as PesoNeto " &
                   " from BASCULA b join EMPRESAS e on b.EmpresaID=e.EmpresaID " &
                   " join PROCESOS p on b.ProcesoID=p.ProcesoID " &
                   " where b.EmpresaID=@IDE and BasculaIDF=@IDF "
        End Get
    End Property

    Public ReadOnly Property CargaParametros() As String
        Get
            Return " select pp.ParametroID, p.ParametroNombre " &
                   " from PARAMETROS p join PROCESOSPARAMETROS pp on p.ParametroID = pp.ParametroID " &
                   " where pp.ProcesoID = @keyproceso and p.ParametroLista = 'N' "
        End Get
    End Property

    Public ReadOnly Property CargaParametrosLista() As String
        Get
            Return " select pp.ParametroID, p.ParametroNombre " &
                   " from PARAMETROS p join PROCESOSPARAMETROS pp on p.ParametroID = pp.ParametroID " &
                   " where pp.ProcesoID = @keyproceso and p.ParametroLista = 'S' "
        End Get
    End Property

    Public ReadOnly Property CargaGuia() As String
        Get
            Return " select bg.GuiaID, bg.UbicacionID, u.UbicacionNombre, bg.VariedadID, " &
                   " v.VariedadNombre, bg.EnvaseID, ue.UnidadEmpaqueNombre, " &
                   " e.EnvasePeso, CONCAT(u.UbicacionNombre,' ',c.CultivoNombre,' ',v.VariedadNombre) as Origen, " &
                   " bg.GuiaCantidad, bg.GuiaPesoBruto, (bg.GuiaCantidad*e.EnvasePeso) as TaraEnvase, " &
                   " (bg.GuiaPesoBruto - (bg.GuiaCantidad*e.EnvasePeso)) as PesoNeto " &
                   " from BASCULAGUIA bg join UBICACIONES u on bg.UbicacionID=u.UbicacionID " &
                   " join VARIEDAD v On bg.VariedadID=v.VariedadID " &
                   " join ENVASES e on bg.EnvaseID=e.EnvaseID " &
                   " join UNIDADEMPAQUE ue on ue.UnidadEmpaqueID=e.EnvaseID " &
                   " join CULTIVO c on c.CultivoID=v.CultivoID " &
                   " where bg.EmpresaID=@id_empresa And bg.BasculaIDF=@id_folio " &
                   " order by bg.GuiaID "
        End Get
    End Property

    Public ReadOnly Property CargaGuiaFlete() As String
        Get
            Return " select CosechaID as GuiaID, fc.UbicacionID, u.UbicacionNombre, fc.VariedadID, v.VariedadNombre, " &
                   " FC.EnvaseID, ue.UnidadEmpaqueNombre, EnvasePeso, " &
                   " CONCAT(u.UbicacionNombre,' ',c.CultivoNombre,' ',v.VariedadNombre) as Origen, " &
                   " CosechaCantidad as GuiaCantidad, 0 AS GuiaPesoBruto, " &
                   " (CosechaCantidad*e.EnvasePeso) as TaraEnvase, (0 - (CosechaCantidad*e.EnvasePeso)) as PesoNeto " &
                   " from FLETECOSECHA fc join UBICACIONES u on fc.UbicacionID=u.UbicacionID " &
                   " join VARIEDAD v On fc.VariedadID=v.VariedadID " &
                   " join ENVASES e on fc.EnvaseID=e.EnvaseID " &
                   " join UNIDADEMPAQUE ue on ue.UnidadEmpaqueID=e.EnvaseID " &
                   " join CULTIVO c on c.CultivoID=v.CultivoID " &
                   " where FleteID = @fleteid "
        End Get
    End Property

    Public ReadOnly Property ComboUbicaciones() As String
        Get
            Return " select a.UbicacionID, u.UbicacionNombre " &
                   " from UBICACIONES u JOIN ALMACENES a on u.UbicacionID=a.UbicacionID " &
                   " where EmpresaID=15 "
        End Get
    End Property

    Public ReadOnly Property ComboVariedades() As String
        Get
            Return " select v.VariedadID, c.CultivoNombre + ' - ' + v.VariedadNombre as VariedadNombre " &
                   " from VARIEDAD v join CULTIVO c on v.CultivoID=c.CultivoID " &
                   " where VariedadStatus='A' and v.CultivoID IN (1,11) " &
                   " order by VariedadNombre "
        End Get
    End Property

    Public ReadOnly Property ComboEnvases() As String
        Get
            Return " select EnvaseID, UnidadEmpaqueNombre  " &
                   " from ENVASES e join UNIDADEMPAQUE ue on e.EnvaseID=ue.UnidadEmpaqueID "
        End Get
    End Property

    Public ReadOnly Property ComboOperadores() As String
        Get
            Return " SELECT TRO.OperadorID, TRO.OperadorNombre + ' ' + TRO.OperadorApellidos AS Nombre " &
                   " FROM FLETE F JOIN TRANSPORTISTA T ON F.TransportistaID=T.TransportistaID " &
                   " JOIN TRANSPORTISTAOPERADOR TRO ON TRO.TransportistaID=T.TransportistaID " &
                   " WHERE F.FleteID = @fleteid "
        End Get
    End Property

    Public ReadOnly Property ComboFletes() As String
        Get
            Return " SELECT FleteID, CAST(FleteID AS varchar) + ' - ' + dbo.GETDESUBICA(RUTA.OrigenID) + '  --> ' + dbo.GETDESUBICA(RUTA.DestinoID) AS ruta  " &
                   " FROM FLETE JOIN RUTA ON FLETE.RutaID = RUTA.RutaID " &
                   " WHERE FleteID NOT IN (SELECT ParametroEntrada FROM BASCULAPARAMETROS WHERE ParametroID = 1) "
        End Get
    End Property

    Public ReadOnly Property ComboFletesUpd() As String
        Get
            Return " SELECT ParametroEntrada, CAST(ParametroEntrada AS varchar) + ' - ' + dbo.GETDESUBICA(R.OrigenID) + '  --> ' + dbo.GETDESUBICA(R.DestinoID) AS ruta  " &
                   " FROM BASCULAPARAMETROS BP JOIN FLETE F ON BP.ParametroEntrada = F.FleteID " &
                   " JOIN RUTA R ON F.RutaID = R.RutaID " &
                   " WHERE ParametroID = 1 AND BasculaIDF = @IDF "
        End Get
    End Property

    Public ReadOnly Property ComboOperadorUpd() As String
        Get
            Return " SELECT ParametroEntrada, OperadorNombre + ' ' + OperadorApellidos as Nombre " &
                   " FROM BASCULAPARAMETROS BP JOIN TRANSPORTISTAOPERADOR T ON BP.ParametroEntrada = T.OperadorID " &
                   " WHERE ParametroID = 2 AND BasculaIDF = @IDF "
        End Get
    End Property

    Public ReadOnly Property PesoEnv() As String
        Get
            Return " select EnvasePeso from ENVASES where EnvaseID = @EnvaseID  "
        End Get
    End Property

    Public ReadOnly Property CultivoNom() As String
        Get
            Return " select c.CultivoNombre from CULTIVO c join VARIEDAD v on c.CultivoID=v.CultivoID  " &
                   " where v.VariedadID = @VariedadID "
        End Get
    End Property
End Class

Public Class SQLInsertarDatos
    Inherits Sql

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub

    Public ReadOnly Property InsGeneral As String
        Get
            Return "insert into BASCULA values (@IDEmp,@IDF,@Fecha,@PBruto,@PTara,@FecReg,@ProcesoID,0)"
        End Get
    End Property

    Public ReadOnly Property InsParametro As String
        Get
            Return "insert into BASCULAPARAMETROS values (@IDEmp,@IDF,@PID,@PEntrada)"
        End Get
    End Property

    Public ReadOnly Property InsGuia As String
        Get
            Return "insert into BASCULAGUIA values (@IDEmp,@IDF,@GuiaID,@UbicaID,@VarID,@EnvID,@Cantidad,@PBruto)"
        End Get
    End Property

    Public ReadOnly Property UpdFolio As String
        Get
            Return "update FOLIOS set FolioValor=@IDF where FolioID=@FolID"
        End Get
    End Property

    Public ReadOnly Property InsInventario As String
        Get
            Return " INSERT INTO MOVIMIENTOS (EmpresaID, UbicacionID, ProductoID, MovimientoClasifica, MovimientoDocumento, " &
                   " MovimientoFechaDoc, MovimientoCantidad, MovimientoPeso, MovimientoLote, MovimientoLoteHis, " &
                   " MovimientoObs, MovimientoTipo, OrigenMovID) " &
                   " VALUES(@IDEmp,@UbicaID,@ProdID,@MovClas,@IDF,@fecha,@Cantidad,@PNeto, " &
                   " dbo.Building_Lote(@ProcesoID,@TablaID,@VarID,@fechaLote,@Extra),@MovLoteHis,@MovObs,@MovTipo,@OrigenMovID) "
        End Get
    End Property
End Class

Public Class SQLActualizarDatos
    Inherits Sql

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub

    Public ReadOnly Property UpdGeneral As String
        Get
            Return " update BASCULA set BasculaFecha=@Fecha, BasculaPesoBruto=@PBruto, BasculaPesoTara=@PTara, " &
                   " BasculaFechaRegistro = @FecReg " &
                   " where EmpresaID = @IDEmp AND BasculaIDF = @IDF "
        End Get
    End Property

    Public ReadOnly Property UpdParametro As String
        Get
            Return " update BASCULAPARAMETROS set ParametroEntrada=@PEntrada " &
                   " where EmpresaID=@IDEmp AND BasculaIDF=@IDF and ParametroID=@PID "
        End Get
    End Property
End Class

Public Class SQLEliminarDatos
    Inherits Sql

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub

    Public ReadOnly Property DelGuia As String
        Get
            Return " delete from BASCULAGUIA where EmpresaID=@IDEmp And BasculaIDF=@IDF "
        End Get
    End Property

    Public ReadOnly Property DelParams As String
        Get
            Return " delete from BASCULAPARAMETROS where EmpresaID=@IDEmp And BasculaIDF=@IDF "
        End Get
    End Property

    Public ReadOnly Property DelGeneral As String
        Get
            Return " delete from BASCULA where EmpresaID=@IDEmp And BasculaIDF=@IDF "
        End Get
    End Property
End Class
