Imports Security_System

Public Class SQLAgro
    Inherits Sql

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



Public Class SQLInventariosAgro
    Inherits SQL

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub

    Public ReadOnly Property ListBasica() As String
        Get
            Return " SELECT EmpresaID,EmpresaNombre,UbicacionID,UbicacionNombre,ProductoID,ProductoNombre,ProductoLote," &
                    " ProductoLoteHis,InventarioUbicacionPasillo,InventarioLogico,InventarioFisico,InventarioPeso,InventarioInicial" &
                    " FROM VW_CONSTRUIR_CONTEOS " &
                    " WHERE EmpresaNombre LIKE @Empresa" &
                    " And UbicacionNombre Like @Almacen" &
                    " And ProductoNombre Like @Producto" &
                    " AND NOT ProductoLote IS NULL" &
                    " AND CASE WHEN ProductoLote IS NULL THEN '' ELSE ProductoLote END LIKE @Lote" &
                    " AND CAST(InventarioInicial as date) BETWEEN @fecini AND @fecfin"
        End Get
    End Property

    Public ReadOnly Property ListExport() As String
        Get
            Return " SELECT EmpresaNombre,UbicacionNombre,ProductoNombre,dbo.BuildingInfo_Lote(ProductoLote) as ProductoLote," &
                    " InventarioUbicacionPasillo,InventarioLogico,COALESCE(InventarioFisico,0) As InventarioFisico,InventarioPeso," &
                    " CONVERT(VARCHAR(12),InventarioInicial,103) as InventarioInicial" &
                    " FROM VW_CONSTRUIR_CONTEOS " &
                    " WHERE EmpresaNombre LIKE @Empresa" &
                    " And UbicacionNombre Like @Almacen" &
                    " And ProductoNombre Like @Producto" &
                    " AND NOT ProductoLote IS NULL" &
                    " AND CASE WHEN ProductoLote IS NULL THEN '' ELSE ProductoLote END LIKE @Lote" &
                    " AND CAST(InventarioInicial as date) BETWEEN @fecini AND @fecfin"
        End Get
    End Property

    'Lista Inventario ExistenteA nivel de Almacen
    Public ReadOnly Property List() As String
        Get
            Return " SELECT ProductoID,ProductoNombre,ProductoLote,dbo.BuildingInfo_Lote(ProductoLote) as Origen,InventarioLogico,UbicacionID,UbicacionNombre" &
                " FROM VW_CONSTRUIR_CONTEOS VC" &
                " WHERE EmpresaID = @EmpresaID" &
                " And UbicacionID = @UbicacionID" &
                " And Not ProductoLote Is NULL " &
                " AND ProductoNombre LIKE @Producto" &
                "  AND NOT EXISTS" &
                "(SELECT * FROM SALIDASEMILLA SM " &
                " LEFT JOIN SALIDASEMILLADETALLE SD ON (SM.SalidaSemillaIDF=SD.SalidaSemillaIDF)" &
                " WHERE SM.SalidaSemillaIDF = @SalidaSemillaIDF AND VC.ProductoID=SD.ProductoID AND VC.ProductoLote=SD.ProductoLote AND SM.EmpresaID = VC.EmpresaID)"
        End Get
    End Property

    Public ReadOnly Property Item() As String
        Get
            Return " Select * FROM INVENTARIOS " &
                    " WHERE EmpresaID = @keycia And UbicacionID = @keyubi And ProductoID = @keyprod And ProductoLote = @nolote"
        End Get
    End Property

    Public ReadOnly Property ItemInv() As String
        Get
            Return " SELECT * FROM VW_CONSTRUIR_CONTEOS" &
                    " WHERE NOT ProductoLote Is NULL " &
                    " AND EmpresaID = @keycia And UbicacionID = @keyubi And ProductoID = @keyprod And ProductoLote = @nolote"
        End Get
    End Property


    Public ReadOnly Property Building_Lote() As String
        Get
            Return " SELECT dbo.Building_Lote(@Proceso,@Tabla,@Variedad,@Fecha,@Observ) as Lote"
        End Get
    End Property

    'Public ReadOnly Property Building_Lote() As String
    '    Get
    '        Return " SELECT dbo.Building_Lote(@Proceso,@Tabla,@Variedad,GETDATE(),@Observ) as Lote"
    '    End Get
    'End Property

    Public ReadOnly Property ObtenerFechaIni As String
        Get
            Return " Select TOP 1 CONVERT(VARCHAR(10), InventarioInicial, 103) As InventarioInicial" &
                   " FROM INVENTARIOS" &
                   " ORDER BY CAST(InventarioInicial As Date) asc"
        End Get
    End Property

    Public ReadOnly Property ObtenerFechaFin As String
        Get
            Return " Select TOP 1 CONVERT(VARCHAR(10), InventarioInicial, 103) As InventarioInicial" &
                    " FROM INVENTARIOS" &
                    " ORDER BY CAST(InventarioInicial As Date) desc"
        End Get
    End Property

    Public ReadOnly Property Update() As String
        Get
            Return "  "
        End Get
    End Property


End Class




Public Class SQLConteos
    Inherits Sql

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub

    Public ReadOnly Property ListBasica() As String
        Get
            Return "  Select ConteoID,ConteoDescripcion,ConteoFecha,ConteoUsuarioID,dbo.Get_DesStatusCont(ConteoStatus) As ConteoStatus," &
                    " C.EmpresaID,EmpresaNombre,C.UbicacionID,UbicacionNombre" &
                    " FROM CONTEOS C" &
                    " LEFT JOIN ALMACENES A On (A.EmpresaID=C.EmpresaID And A.UbicacionID=C.UbicacionID)" &
                    " LEFT JOIN UBICACIONES U On (U.UbicacionID = A.UbicacionID)" &
                    " LEFT JOIN EMPRESAS E On (E.EmpresaID = A.EmpresaID)" &
                    " WHERE ConteoDescripcion Like @desc" &
                    " And EmpresaNombre Like @Empresa" &
                    " And UbicacionNombre Like @Almacen" &
                    " And CONVERT(VARCHAR(10),ConteoFecha,103) Like @Fecha" &
                    " ORDER BY C.ConteoFecha desc, C.ConteoID desc"
        End Get
    End Property

    Public ReadOnly Property ListaConteo() As String
        Get
            Return " Select ConteoID,ConteoDescripcion,ConteoFecha,ConteoUsuarioID,dbo.Get_DesStatusCont(ConteoStatus) As ConteoStatus," &
                " C.EmpresaID,EmpresaNombre,C.UbicacionID,UbicacionNombre" &
                " FROM CONTEOS C" &
                " LEFT JOIN ALMACENES A On (A.EmpresaID=C.EmpresaID And A.UbicacionID=C.UbicacionID)" &
                " LEFT JOIN UBICACIONES U On (U.UbicacionID = A.UbicacionID)" &
                " LEFT JOIN EMPRESAS E On (E.EmpresaID = A.EmpresaID)" &
                " WHERE ConteoID = @ConteoID " &
                " ORDER BY C.ConteoFecha desc, C.ConteoID desc"
        End Get
    End Property

    Public ReadOnly Property List() As String
        Get
            Return "  "
        End Get
    End Property

    Public ReadOnly Property Item() As String
        Get
            Return " Select * FROM CONTEOS " &
                    " WHERE ConteoID = @ConteoID "
        End Get
    End Property

    Public ReadOnly Property ExistDetalle() As String
        Get
            Return " Select TOP 1 C.ConteoDescripcion,DC.ConteoID," &
                    " Case When DC.ConteoID Is NULL Then 'N' ELSE 'S' END Exist," &
                    " DC.ProductoID" &
                    " FROM CONTEOS C " &
                    " LEFT JOIN CONTEOSDETALLE DC" &
                    " ON (DC.ConteoID=C.ConteoID)" &
                    " WHERE C.ConteoID = @ConteoID "
        End Get
    End Property

    Public ReadOnly Property Update() As String
        Get
            Return " UPDATE CONTEOS SET ConteoStatus = 'D' " &
                   " WHERE ConteoID = @ConteoID"
        End Get
    End Property


End Class



Public Class SQLConteosDetalle
    Inherits SQL

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub

    Public ReadOnly Property ListDetCon() As String
        Get
            Return " SELECT  C.ConteoID, EM.EmpresaID,EM.EmpresaNombre,A.UbicacionID,U.UbicacionNombre,TP.TipoPrdNombre, " &
                    " P.ProductoID,P.ProductoNombre,EV.EnvasePeso,COALESCE(M.MarcaNombre,'') AS MarcaNombre," &
                    " COALESCE(DC.ProductoLote,'') As ProductoLote,dbo.BuildingInfo_Lote(DC.ProductoLote) as Origen,DC.ConteoFisico As ConteoLogico," &
                    " COALESCE(DC.UbicacionPasillo,'') AS InventarioUbicacionPasillo," &
                    " dbo.Get_DesStatusCont(C.ConteoStatus) as ConteoStatus" &
                    " FROM CONTEOS C " &
                    " LEFT JOIN CONTEOSDETALLE DC On (DC.ConteoID=C.ConteoID) " &
                    " LEFT JOIN ALMACENES A" &
                    " On (A.EmpresaID=C.EmpresaID And A.UbicacionID=C.UbicacionID)" &
                    " LEFT JOIN UBICACIONES U On (U.UbicacionID=A.UbicacionID)" &
                    " LEFT JOIN PRODUCTOS P ON (P.ProductoID=DC.ProductoID And P.EmpresaID=c.EmpresaID)" &
                    " LEFT JOIN TIPOPRD TP On (TP.TipoPrdID=P.TipoPrdID)" &
                    " LEFT JOIN ENVASES EV On (EV.EnvaseID = P.EnvaseID)" &
                    " LEFT JOIN MARCAS M ON (M.MarcaID=P.MarcaID)" &
                    " LEFT JOIN EMPRESAS EM ON (EM.EmpresaID = A.EmpresaID)" &
                    " WHERE C.ConteoID = @ConteoID " &
                    "VIEW_FILTRO "
        End Get
    End Property

    Public ReadOnly Property ListAllProd() As String
        Get
            Return " SELECT C.ConteoID,A.EmpresaID,EmpresaNombre,A.UbicacionID,UbicacionNombre,TipoPrdNombre,A.ProductoID,ProductoNombre,EnvasePeso," &
                    " MarcaNombre,A.ProductoLote,dbo.BuildingInfo_Lote(A.ProductoLote) as Origen,COALESCE(InventarioLogico,0) As ConteoLogico,InventarioUbicacionPasillo" &
                    " ,dbo.Get_DesStatusCont(C.ConteoStatus) As ConteoStatus" &
                    " FROM VW_CONSTRUIR_CONTEOS A" &
                    " LEFT JOIN CONTEOS C ON (C.EmpresaID=A.EmpresaID And C.UbicacionID = A.UbicacionID)" &
                    " LEFT JOIN CONTEOSDETALLE DC ON (DC.ConteoID=C.ConteoID And DC.ProductoID = A.ProductoID And DC.ProductoLote = A.ProductoLote)" &
                    " WHERE ProductoNombre Like @Producto" &
                    " And TipoPrdNombre Like @TipoPrd" &
                    " And C.ConteoID = @ConteoID " &
                    "VIEW_FILTRO "
        End Get
    End Property

    Public ReadOnly Property ListExport() As String
        Get
            Return " SELECT C.ConteoID,EM.EmpresaNombre,U.UbicacionNombre,TP.TipoPrdNombre,DC.ProductoID, " &
                    " P.ProductoNombre,EV.EnvasePeso,COALESCE(M.MarcaNombre,'') AS MarcaNombre,COALESCE(DC.ProductoLote,'') AS ProductoLote,dbo.BuildingInfo_Lote(DC.ProductoLote) as Origen,DC.ConteoFisico as ConteoLogico,COALESCE(DC.UbicacionPasillo,'') AS InventarioUbicacionPasillo," &
                    " dbo.Get_DesStatusCont(C.ConteoStatus) As ConteoStatus " &
                    " FROM CONTEOS C " &
                    " LEFT JOIN CONTEOSDETALLE DC On (DC.ConteoID=C.ConteoID) " &
                    " LEFT JOIN ALMACENES A" &
                    " ON (A.EmpresaID=C.EmpresaID And A.UbicacionID=C.UbicacionID)" &
                    " LEFT JOIN UBICACIONES U ON (U.UbicacionID=A.UbicacionID)" &
                    " LEFT JOIN PRODUCTOS P ON (P.ProductoID=DC.ProductoID And P.EmpresaID=c.EmpresaID)" &
                    " LEFT JOIN TIPOPRD TP ON (TP.TipoPrdID=P.TipoPrdID)" &
                    " LEFT JOIN ENVASES EV ON (EV.EnvaseID = P.EnvaseID)" &
                    " LEFT JOIN MARCAS M ON (M.MarcaID=P.MarcaID)" &
                    " LEFT JOIN EMPRESAS EM ON (EM.EmpresaID = A.EmpresaID)" &
                    " WHERE C.ConteoID = @ConteoID"
        End Get
    End Property

    Public ReadOnly Property List() As String
        Get
            Return "  "
        End Get
    End Property

    Public ReadOnly Property Item() As String
        Get
            Return " SELECT * FROM CONTEOSDETALLE " &
                   " WHERE ConteoID = @ConteoID "
        End Get
    End Property

    Public ReadOnly Property ItemDet() As String
        Get
            Return " SELECT * FROM CONTEOSDETALLE " &
                   " WHERE ConteoID = @ConteoID " &
                   " AND ProductoID = @ProductoID" &
                   " And ProductoLote = @ProductoLote"
        End Get
    End Property

    Public ReadOnly Property Update() As String
        Get
            Return "  "
        End Get
    End Property

    Public ReadOnly Property Delete() As String
        Get
            Return " DELETE FROM CONTEOSDETALLE" &
                   " WHERE ConteoID = @ConteoID"
        End Get
    End Property

    Public ReadOnly Property DeleteRow() As String
        Get
            Return " DELETE FROM CONTEOSDETALLE" &
                   " WHERE ConteoID = @ConteoID" &
                   " AND ProductoID = @ProductoID" &
                   " And ProductoLote = @ProductoLote"
        End Get
    End Property


End Class




Public Class SQLEmpresa
    Inherits Sql

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub

    Public ReadOnly Property ListBasica() As String
        Get
            Return "SELECT * FROM EMPRESAS"
        End Get
    End Property

    Public ReadOnly Property List() As String
        Get
            Return "  "
        End Get
    End Property

    Public ReadOnly Property Item() As String
        Get
            Return " SELECT * FROM EMPRESAS" &
                    " WHERE EmpresaID = @EmpresaID "
        End Get
    End Property

    Public ReadOnly Property keyItem() As String
        Get
            Return " SELECT * FROM EMPRESAS" &
                    " WHERE EmpresaNombre = @desEmp "
        End Get
    End Property

    Public ReadOnly Property Update() As String
        Get
            Return "  "
        End Get
    End Property


End Class




Public Class SQLAlmacenAgro
    Inherits SQL

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub

    Public ReadOnly Property ListBasica() As String
        Get
            Return "  "
        End Get
    End Property

    Public ReadOnly Property List() As String
        Get
            Return "  "
        End Get
    End Property

    Public ReadOnly Property List_Combo() As String
        Get
            Return " SELECT A.UbicacionID,UbicacionNombre FROM ALMACENES A" &
                    " LEFT JOIN UBICACIONES U " &
                    " ON (U.UbicacionID=A.UbicacionID)" &
                    " WHERE EmpresaID = @EmpresaID "
        End Get
    End Property

    Public ReadOnly Property Item() As String
        Get
            Return " SELECT * FROM ALMACENES " &
                   " WHERE EmpresaID = @EmpresaID AND UbicacionID = @UbicacionID "
        End Get
    End Property

    Public ReadOnly Property Update() As String
        Get
            Return "  "
        End Get
    End Property


End Class



Public Class SQLProductosAgro
    Inherits SQL

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub

    Public ReadOnly Property ListBasica() As String
        Get
            Return "  "
        End Get
    End Property

    Public ReadOnly Property List() As String
        Get
            Return "  "
        End Get
    End Property

    Public ReadOnly Property List_Combo() As String
        Get
            Return " SELECT ProductoID, ProductoNombre" &
                   " FROM PRODUCTOS "
        End Get
    End Property

    Public ReadOnly Property Item() As String
        Get
            Return "  "
        End Get
    End Property

    Public ReadOnly Property ItemID() As String
        Get
            Return " Select * FROM PRODUCTOS" &
                   " WHERE ProductoNombre Like @DesProd"
        End Get
    End Property

    Public ReadOnly Property Update() As String
        Get
            Return "  "
        End Get
    End Property


End Class


Public Class SQLSalidaSemilla
    Inherits Sql

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub

    Public ReadOnly Property ListDetCon() As String
        Get
            Return ""
        End Get
    End Property

    Public ReadOnly Property ListReport() As String
        Get
            Return " Select SM.SalidaSemillaIDF,SM.SalidaSemillaFecha,SM.SalidaSemillaOrigen," &
            " VC.ProductoID,VC.ProductoNombre,VC.ProductoLote,SSD.SalidaSemillaCantidad,SalidaSemillaPeso,SalidaSemillaDensidad,SalidaSemillaObservaciones," &
            " SM.SalidaSemillaElaboro,SM.SalidaSemillaTransportista,SM.SalidaSemillaEncargadoSiembra," &
            " REVERSE(LEFT(REVERSE(VC.ProductoLote), CHARINDEX('-', REVERSE(VC.ProductoLote))-1 )) as Tamaño," &
            " SalidaSemillaLotes,dbo.Get_NoFlete_Sal(SM.SalidaSemillaIDF) as FleteID, dbo.Get_Agricultor(SM.SalidaSemillaIDF) as Agricultor," &
            " DevProductoNombre, DevSemillaCantidad, DevSemillaObservaciones,UO.UbicacionNombre as Alm_OrgD, DevSemillaAlmacen, UD.UbicacionNombre as Alm_DestD," &
            " CASE WHEN SSM.SalidaSemillaIDF IS NOT NULL THEN REVERSE(LEFT(REVERSE(VC.ProductoLote), CHARINDEX('-', REVERSE(VC.ProductoLote))-1 ))" &
            " ELSE '' END TamañoDev," &
            " dbo.Get_Settings(N'RREF', 1) AS REVISION,dbo.Get_Settings(N'RCCP', 1) AS CODIGO, dbo.Get_Settings(N'RFEC', 1) AS FECHA" &
            " FROM SALIDASEMILLA SM" &
            " LEFT JOIN SALIDASEMILLADETALLE SSD ON (SM.SalidaSemillaIDF=SSD.SalidaSemillaIDF)" &
            " LEFT JOIN VW_CONSTRUIR_CONTEOS VC ON (VC.ProductoID=SSD.ProductoID And VC.ProductoLote = SSD.ProductoLote" &
            " AND VC.EmpresaID = SM.EmpresaID AND VC.UbicacionID = SM.UbicacionID)" &
            " LEFT JOIN SALIDASEMILLADEVOLUCION SSM ON (SSD.SalidaSemillaIDF=SSM.SalidaSemillaIDF " &
            " AND SSM.DevProductoID = SSD.ProductoID AND SSM.DevProductoLote = SSD.ProductoLote)" &
            " LEFT JOIN PRODUCTOS P ON (P.EmpresaID=SM.EmpresaID And P.ProductoID=SSM.DevProductoID)" &
            " LEFT JOIN ALMACENES AO On (AO.EmpresaID=SM.EmpresaDestinoID And AO.UbicacionID = SM.UbicacionDestinoID)" &
            " LEFT JOIN EMPRESAS EO ON (EO.EmpresaID = AO.EmpresaID)" &
            " LEFT JOIN UBICACIONES UO On (UO.UbicacionID=AO.UbicacionID)" &
            " LEFT JOIN ALMACENES AD ON (AD.EmpresaID = SM.EmpresaID And AD.UbicacionID = SSM.DevSemillaAlmacen)" &
            " LEFT JOIN EMPRESAS ED ON (ED.EmpresaID = AD.EmpresaID)" &
            " LEFT JOIN UBICACIONES UD ON (UD.UbicacionID=AD.UbicacionID)" &
            " WHERE Not VC.ProductoLote Is NULL" &
            " AND SM.SalidaSemillaIDF = @SalidaSemillaIDF"
        End Get
    End Property

    Public ReadOnly Property List() As String
        Get
            Return " SELECT SM.SalidaSemillaIDF,SalidaSemillaFecha,UO.UbicacionNombre as Alm_Org, UD.UbicacionNombre as Alm_Des, " &
                " SalidaSemillaLotes, SalidaSemillaElaboro, SalidaSemillaEncargadoSiembra,dbo.Get_StatusSalida(SalidaSemillaStatusProceso) as StatusSal" &
                " FROM SALIDASEMILLA SM" &
                " LEFT JOIN ALMACENES AO On (AO.EmpresaID=SM.EmpresaID And AO.UbicacionID = SM.UbicacionID)" &
                " LEFT JOIN ALMACENES AD ON (AD.EmpresaID = SM.EmpresaDestinoID AND AD.UbicacionID = SM.UbicacionDestinoID)" &
                " LEFT JOIN EMPRESAS EO ON (EO.EmpresaID = AO.EmpresaID)" &
                " LEFT JOIN UBICACIONES UO ON (UO.UbicacionID=AO.UbicacionID)" &
                " LEFT JOIN EMPRESAS ED ON (ED.EmpresaID = AD.EmpresaID)" &
                " LEFT JOIN UBICACIONES UD ON (UD.UbicacionID=AD.UbicacionID)" &
                " WHERE SM.SalidaSemillaIDF LIKE @Folio " &
                " And CONVERT(VARCHAR(10),SalidaSemillaFecha,103) Like @Fecha" &
                " And UO.UbicacionNombre Like @AlmOrigen " &
                " And UD.UbicacionNombre Like @AlmDest " &
                " ORDER BY SM.SalidaSemillaIDF DESC"
        End Get
    End Property

    Public ReadOnly Property ListExistFlete() As String
        Get
            Return " SELECT SM.SalidaSemillaIDF,SalidaSemillaFecha,UO.UbicacionNombre as Alm_Org, UD.UbicacionNombre as Alm_Des, " &
                " SalidaSemillaLotes, SalidaSemillaElaboro, SalidaSemillaEncargadoSiembra,dbo.Get_StatusSalida(SalidaSemillaStatusProceso) as StatusSal" &
                " FROM SALIDASEMILLA SM" &
                " LEFT JOIN ALMACENES AO On (AO.EmpresaID=SM.EmpresaID And AO.UbicacionID = SM.UbicacionID)" &
                " LEFT JOIN ALMACENES AD ON (AD.EmpresaID = SM.EmpresaDestinoID AND AD.UbicacionID = SM.UbicacionDestinoID)" &
                " LEFT JOIN EMPRESAS EO ON (EO.EmpresaID = AO.EmpresaID)" &
                " LEFT JOIN UBICACIONES UO ON (UO.UbicacionID=AO.UbicacionID)" &
                " LEFT JOIN EMPRESAS ED ON (ED.EmpresaID = AD.EmpresaID)" &
                " LEFT JOIN UBICACIONES UD ON (UD.UbicacionID=AD.UbicacionID)" &
                " LEFT JOIN FLETESEMILLA FS ON (FS.SalidaSemillaIDF=SM.SalidaSemillaIDF)" &
                " LEFT JOIN FLETE F on (F.FleteID=FS.FleteID)" &
                " WHERE SM.SalidaSemillaIDF LIKE @Folio " &
                " And CONVERT(VARCHAR(10),SalidaSemillaFecha,103) Like @Fecha" &
                " And UO.UbicacionNombre Like @AlmOrigen " &
                " And UD.UbicacionNombre Like @AlmDest " &
                " AND F.FleteID = @FleteID " &
                " ORDER BY SM.SalidaSemillaIDF DESC"
        End Get
    End Property


    Public ReadOnly Property ListSalida() As String
        Get
            Return " SELECT SM.SalidaSemillaIDF,CONVERT(VARCHAR(10), SalidaSemillaFecha, 103) as SalidaSemillaFecha,UO.UbicacionNombre as Alm_Org, UD.UbicacionNombre as Alm_Des, " &
                 " SalidaSemillaOrigen, SalidaSemillaElaboro, SalidaSemillaEncargadoSiembra " &
                 " FROM SALIDASEMILLA SM" &
                 " LEFT JOIN ALMACENES AO On (AO.EmpresaID=SM.EmpresaID And AO.UbicacionID = SM.UbicacionID) " &
                 " LEFT JOIN ALMACENES AD ON (AD.EmpresaID = SM.EmpresaDestinoID AND AD.UbicacionID = SM.UbicacionDestinoID) " &
                 " LEFT JOIN EMPRESAS EO ON (EO.EmpresaID = AO.EmpresaID) " &
                 " LEFT JOIN UBICACIONES UO ON (UO.UbicacionID=AO.UbicacionID) " &
                 " LEFT JOIN EMPRESAS ED ON (ED.EmpresaID = AD.EmpresaID) " &
                 " LEFT JOIN UBICACIONES UD ON (UD.UbicacionID=AD.UbicacionID) " &
                 " WHERE SM.SalidaSemillaIDF LIKE @SalidaSemillaIDF"
        End Get
    End Property


    Public ReadOnly Property ListSalidasByFlete() As String
        Get
            Return " SELECT SM.SalidaSemillaIDF,CONVERT(VARCHAR(10), SalidaSemillaFecha, 103) as SalidaSemillaFecha,UO.UbicacionNombre as Alm_Org, UD.UbicacionNombre as Alm_Des, " &
                    " SalidaSemillaLotes, SalidaSemillaElaboro, SalidaSemillaEncargadoSiembra" &
                    " FROM SALIDASEMILLA SM" &
                    " LEFT JOIN ALMACENES AO On (AO.EmpresaID=SM.EmpresaID And AO.UbicacionID = SM.UbicacionID)" &
                    " LEFT JOIN ALMACENES AD ON (AD.EmpresaID = SM.EmpresaDestinoID AND AD.UbicacionID = SM.UbicacionDestinoID) " &
                    " LEFT JOIN EMPRESAS EO On (EO.EmpresaID = AO.EmpresaID) " &
                    " LEFT JOIN UBICACIONES UO ON (UO.UbicacionID=AO.UbicacionID) " &
                    " LEFT JOIN EMPRESAS ED ON (ED.EmpresaID = AD.EmpresaID) " &
                    " LEFT JOIN UBICACIONES UD ON (UD.UbicacionID=AD.UbicacionID)" &
                    " WHERE EXISTS " &
                    " (SELECT * FROM FLETESEMILLA FS" &
                        " LEFT JOIN FLETE F ON (F.FleteID=FS.FleteID)" &
                        " WHERE SM.SalidaSemillaIDF=FS.SalidaSemillaIDF" &
                        " AND F.FleteID = @FleteID)"
        End Get
    End Property


    Public ReadOnly Property Item() As String
        Get
            Return " SELECT SalidaSemillaIDF,EmpresaID,SalidaSemillaFecha,SalidaSemillaFechaRegistro,SalidaSemillaOrigen,UbicacionID, " &
                    " SalidaSemillaElaboro,SalidaSemillaEncargadoSiembra,EmpresaDestinoID,UbicacionDestinoID,SalidaSemillaStatusProceso " &
                    " FROM SALIDASEMILLA " &
                    " WHERE SalidaSemillaIDF = @SalidaSemillaIDF"
        End Get
    End Property

    Public ReadOnly Property ItemStatus() As String
        Get
            Return " Select SalidaSemillaIDF,dbo.Get_ExisteFleteSem(SalidaSemillaIDF) As sal_status,SalidaSemillaStatusProceso" &
                    " FROM SALIDASEMILLA " &
                    " WHERE SalidaSemillaIDF = @SalidaSemillaIDF "
        End Get
    End Property

    Public ReadOnly Property ExisteFlete As String
        Get
            Return " Select Case When FP.SalidaSemillaIDF Is NULL Then 'N' ELSE 'S' END AS Existfle " &
                    " FROM SALIDASEMILLA S" &
                    " LEFT JOIN FLETESEMILLA FP On (S.SalidaSemillaIDF=FP.SalidaSemillaIDF)" &
                    " WHERE S.SalidaSemillaIDF = @SalidaSemillaIDF"
        End Get
    End Property
    Public ReadOnly Property ExisteProd As String
        Get
            Return " SELECT SM.SalidaSemillaIDF,Case When SSD.SalidaSemillaIDF Is NULL Then 'N' ELSE 'S' END AS ExistProd" &
                    " FROM SALIDASEMILLA SM" &
                    " LEFT JOIN SALIDASEMILLADETALLE SSD" &
                    " ON (SSD.SalidaSemillaIDF=SM.SalidaSemillaIDF)" &
                    " WHERE SM.SalidaSemillaIDF = @SalidaSemillaIDF"
        End Get
    End Property


    Public ReadOnly Property ItemSalidaLista() As String
        Get
            Return " SELECT SM.SalidaSemillaIDF,SalidaSemillaFecha,UO.UbicacionNombre as Alm_Org, UD.UbicacionNombre as Alm_Des," &
                 " SalidaSemillaOrigen, SalidaSemillaElaboro,SalidaSemillaLotes, SalidaSemillaEncargadoSiembra" &
                 " FROM SALIDASEMILLA SM" &
                 " LEFT JOIN ALMACENES AO On (AO.EmpresaID=SM.EmpresaID And AO.UbicacionID = SM.UbicacionID)" &
                 " LEFT JOIN ALMACENES AD ON (AD.EmpresaID = SM.EmpresaDestinoID AND AD.UbicacionID = SM.UbicacionDestinoID)" &
                 " LEFT JOIN EMPRESAS EO ON (EO.EmpresaID = AO.EmpresaID)" &
                 " LEFT JOIN UBICACIONES UO ON (UO.UbicacionID=AO.UbicacionID)" &
                 " LEFT JOIN EMPRESAS ED ON (ED.EmpresaID = AD.EmpresaID)" &
                 " LEFT JOIN UBICACIONES UD ON (UD.UbicacionID=AD.UbicacionID)" &
                 " WHERE SM.SalidaSemillaIDF = @SalidaSemillaIDF "
        End Get
    End Property

    Public ReadOnly Property DesLote() As String
        Get
            Return " SELECT dbo.BuildingInfo_Lote(@Lote) as OrigenProd"
        End Get
    End Property

    Public ReadOnly Property Update() As String
        Get
            Return " UPDATE FOLIOS SET FolioValor = @FolioValor" &
                   " WHERE FolioID = @FolioID"
        End Get
    End Property

    Public ReadOnly Property Delete() As String
        Get
            Return " DELETE FROM SALIDASEMILLA " &
                    " WHERE SalidaSemillaIDF = @SalidaSemillaIDF"
        End Get
    End Property

    Public ReadOnly Property NextID As String
        Get
            Return "SELECT dbo.Sig_Folio(6) As Folio"
        End Get
    End Property


End Class



Public Class SQLDetalleSalidaSemilla
    Inherits Sql

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub

    Public ReadOnly Property ListExport() As String
        Get
            Return " "
        End Get
    End Property

    Public ReadOnly Property List() As String
        Get
            Return " SELECT SM.SalidaSemillaIDF,VC.ProductoID,VC.ProductoNombre,VC.ProductoLote,dbo.BuildingInfo_Lote(VC.ProductoLote) as Origen,SSD.SalidaSemillaCantidad,SalidaSemillaPeso,SalidaSemillaDensidad,SalidaSemillaObservaciones,dbo.Get_StatusSalida(SM.SalidaSemillaStatusProceso) as sal_status" &
                    " FROM SALIDASEMILLA SM" &
                    " LEFT JOIN SALIDASEMILLADETALLE SSD ON (SM.SalidaSemillaIDF=SSD.SalidaSemillaIDF)" &
                    " LEFT JOIN VW_CONSTRUIR_CONTEOS VC ON (VC.ProductoID=SSD.ProductoID And VC.ProductoLote = SSD.ProductoLote" &
                    " AND VC.EmpresaID = SM.EmpresaID AND VC.UbicacionID = SM.UbicacionID)" &
                    " WHERE NOT VC.ProductoLote IS NULL " &
                    " AND SM.SalidaSemillaIDF = @SalidaSemillaIDF"
        End Get
    End Property

    Public ReadOnly Property Item() As String
        Get
            Return " SELECT * FROM SALIDASEMILLADETALLE " &
                   " WHERE SalidaSemillaIDF = @SalidaSemillaIDF AND ProductoID = @ProductoID AND ProductoLote = @ProductoLote"
        End Get
    End Property

    Public ReadOnly Property ItemProducto() As String
        Get
            Return " SELECT SM.SalidaSemillaIDF,SalidaSemillaFecha,ED.EmpresaID,SM.UbicacionID,UO.UbicacionNombre as Alm_Org, UD.UbicacionNombre as Alm_Des," &
                      " SalidaSemillaOrigen,ProductoLote,SalidaSemillaElaboro,dbo.BuildingInfo_Lote(ProductoLote) as Origen,SalidaSemillaLotes, SalidaSemillaEncargadoSiembra,SSD.ProductoID,SSD.SalidaSemillaCantidad," &
                      " DevSemillaCantidad, DevSemillaObservaciones, DevSemillaAlmacen" &
                      " FROM SALIDASEMILLA SM" &
                      " LEFT JOIN SALIDASEMILLADETALLE SSD " &
                      " ON (SSD.SalidaSemillaIDF=SM.SalidaSemillaIDF) " &
                      " LEFT JOIN ALMACENES AO On (AO.EmpresaID=SM.EmpresaID And AO.UbicacionID = SM.UbicacionID)" &
                      " LEFT JOIN ALMACENES AD ON (AD.EmpresaID = SM.EmpresaDestinoID AND AD.UbicacionID = SM.UbicacionDestinoID)" &
                      " LEFT JOIN EMPRESAS EO ON (EO.EmpresaID = AO.EmpresaID)" &
                      " LEFT JOIN UBICACIONES UO ON (UO.UbicacionID=AO.UbicacionID)" &
                      " LEFT JOIN EMPRESAS ED ON (ED.EmpresaID = AD.EmpresaID)" &
                      " LEFT JOIN UBICACIONES UD ON (UD.UbicacionID=AD.UbicacionID)" &
                      " LEFT JOIN SALIDASEMILLADEVOLUCION SSM ON (SSM.SalidaSemillaIDF = SSD.SalidaSemillaIDF" &
                      " AND SSM.DevProductoID=SSD.ProductoID AND SSM.DevProductoLote=SSD.ProductoLote)" &
                      " WHERE SM.SalidaSemillaIDF = @SalidaSemillaIDF AND SSD.ProductoID = @ProductoID AND ProductoLote = @ProductoLote"
        End Get
    End Property


    Public ReadOnly Property ExisteDevolucion() As String
        Get
            Return " SELECT CASE WHEN SSM.SalidaSemillaIDF IS NULL THEN 'N' ELSE 'S' END AS EXISTDEV" &
                    " FROM SALIDASEMILLADETALLE SSD " &
                    " LEFT JOIN SALIDASEMILLADEVOLUCION SSM ON (SSM.SalidaSemillaIDF = SSD.SalidaSemillaIDF" &
                    " And SSM.DevProductoID=SSD.ProductoID And SSM.DevProductoLote=SSD.ProductoLote)" &
                    " WHERE SSD.SalidaSemillaIDF = @SalidaSemillaIDF AND SSD.ProductoID = @ProductoID AND SSD.ProductoLote = @ProductoLote"
        End Get
    End Property


    Public ReadOnly Property Update() As String
        Get
            Return " "
        End Get
    End Property


    Public ReadOnly Property Delete() As String
        Get
            Return " DELETE FROM SALIDASEMILLADETALLE" &
                    " WHERE SalidaSemillaIDF = @SalidaSemillaIDF And ProductoID = @ProductoID AND ProductoLote = @ProductoLote"
        End Get
    End Property

End Class



Public Class SQLDevolucionSemilla
    Inherits Sql

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub

    Public ReadOnly Property ListExport() As String
        Get
            Return " "
        End Get
    End Property

    Public ReadOnly Property List() As String
        Get
            Return " SELECT SSD.SalidaSemillaIDF,SSD.DevProductoID,P.ProductoNombre,SSD.DevProductoLote," &
                    " Case When SSD.DevProductoLote Is Not NULL Then dbo.BuildingInfo_Lote(SSD.DevProductoLote) Else '' END as Origen," &
                    " DevProductoNombre, DevSemillaCantidad, DevSemillaObservaciones,UO.UbicacionNombre as Alm_Org, DevSemillaAlmacen, UD.UbicacionNombre as Alm_Dest" &
                    " FROM SALIDASEMILLA SM" &
                    " LEFT JOIN SALIDASEMILLADETALLE SSM ON (SM.SalidaSemillaIDF=SSM.SalidaSemillaIDF)" &
                    " LEFT JOIN SALIDASEMILLADEVOLUCION SSD On (SSM.SalidaSemillaIDF=SSD.SalidaSemillaIDF " &
                    " AND SSD.DevProductoID = SSM.ProductoID AND SSD.DevProductoLote = SSM.ProductoLote)" &
                    " LEFT JOIN PRODUCTOS P ON (P.EmpresaID=SM.EmpresaID And P.ProductoID=SSD.DevProductoID) " &
                    " LEFT JOIN ALMACENES AO On (AO.EmpresaID=SM.EmpresaDestinoID And AO.UbicacionID = SM.UbicacionDestinoID)" &
                    " LEFT JOIN EMPRESAS EO ON (EO.EmpresaID = AO.EmpresaID)" &
                    " LEFT JOIN UBICACIONES UO ON (UO.UbicacionID=AO.UbicacionID)" &
                    " LEFT JOIN ALMACENES AD ON (AD.EmpresaID = SM.EmpresaID And AD.UbicacionID = SSD.DevSemillaAlmacen)" &
                    " LEFT JOIN EMPRESAS ED ON (ED.EmpresaID = AD.EmpresaID)" &
                    " LEFT JOIN UBICACIONES UD ON (UD.UbicacionID=AD.UbicacionID)" &
                    " WHERE SSD.SalidaSemillaIDF = @SalidaSemillaIDF"
        End Get
    End Property

    Public ReadOnly Property Item() As String
        Get
            Return " SELECT * FROM SALIDASEMILLADEVOLUCION " &
                   " WHERE SalidaSemillaIDF = @SalidaSemillaIDF AND DevProductoID = @ProductoID AND DevProductoLote = @ProductoLote"
        End Get
    End Property

    Public ReadOnly Property ItemAll() As String
        Get
            Return " SELECT * FROM SALIDASEMILLADEVOLUCION " &
                   " WHERE SalidaSemillaIDF = @SalidaSemillaIDF AND DevProductoID = @ProductoID AND DevProductoLote = @ProductoLote AND DevSemillaAlmacen = @Almacen"
        End Get
    End Property

    Public ReadOnly Property Update() As String
        Get
            Return " "
        End Get
    End Property


    Public ReadOnly Property Delete() As String
        Get
            Return " DELETE FROM SALIDASEMILLADEVOLUCION " &
                    " WHERE SalidaSemillaIDF = @SalidaSemillaIDF And DevProductoID = @ProductoID AND DevProductoLote = @ProductoLote"
        End Get
    End Property

End Class



Public Class SQL_Setting
    Inherits Sql
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


End Class



Public Class SQLUbicaciones
    Inherits Sql

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub

    Public ReadOnly Property ListCombo() As String
        Get
            Return " SELECT UbicacionID, UbicacionNombre" &
                    " FROM UBICACIONES " &
                    " WHERE TipoUbicaID = @TipoUbi"
        End Get
    End Property

    Public ReadOnly Property ListComboEmpresas() As String
        Get
            Return " SELECT UE.UbicacionID,UbicacionNombre" &
                    " FROM EMPRESA_ORIGENPTA UE" &
                    " LEFT JOIN UBICACIONES U" &
                    " ON (U.UbicacionID=UE.UbicacionID)"
        End Get
    End Property

    Public ReadOnly Property List() As String
        Get
            Return " "
        End Get
    End Property

    Public ReadOnly Property ListaRanchos As String
        Get
            Return " SELECT UE.UbicacionID as EmpresaID,U.UbicacionNombre as EmpresaNombre," &
                    " UR.UbicacionID As RanchoID,UR.UbicacionNombre As RanchoNombre,UR.UbicacionTag As RanchoDesc" &
                    " FROM EMPRESA_ORIGENPTA UE" &
                    " LEFT JOIN UBICACIONES U" &
                    " ON (U.UbicacionID=UE.UbicacionID)" &
                    " LEFT JOIN UBICACIONES UR ON (UR.UbicacionPadreID=UE.UbicacionID)" &
                    " WHERE UR.UbicacionNombre LIKE @Descripcion AND COALESCE(UR.UbicacionTag,'') LIKE @tagubi" &
                    " AND UE.UbicacionID = @Empresa"
        End Get
    End Property

    Public ReadOnly Property ListaTablas As String
        Get
            Return " SELECT UR.UbicacionID as RanchoID,UR.UbicacionNombre as RanchoNombre,UT.UbicacionID as TablaID,UT.UbicacionNombre as TablaNombre,UT.UbicacionTag" &
                    " FROM UBICACIONES UR" &
                    " LEFT JOIN UBICACIONES UT" &
                    " ON (UT.UbicacionPadreID=UR.UbicacionID)" &
                    " WHERE UR.UbicacionID = @RanchoID" &
                    " AND UT.UbicacionNombre LIKE @Tabla" &
                    " And COALESCE(UT.UbicacionTag,'') LIKE @tagubi"
        End Get
    End Property


    Public ReadOnly Property Item() As String
        Get
            Return " SELECT * FROM UBICACIONES" &
                   " WHERE UbicacionID = @UbicacionID"
        End Get
    End Property

    Public ReadOnly Property ItemName() As String
        Get
            Return " SELECT UbicacionNombre FROM UBICACIONES" &
                   " WHERE UbicacionID = @UbicacionID"
        End Get
    End Property

    Public ReadOnly Property ItemInvID() As String
        Get
            Return " SELECT * FROM UBICACIONES" &
                    " WHERE UbicacionTag Like @UbicacionTag" &
                    " AND TipoUbicaID = 'I'"
        End Get
    End Property

    Public ReadOnly Property ItemEmpID() As String
        Get
            Return " SELECT U.UbicacionID,U.UbicacionNombre " &
                    " FROM EMPRESA_ORIGENPTA EO " &
                    " LEFT JOIN UBICACIONES U" &
                    " ON (EO.UbicacionID=U.UbicacionID)" &
                    " WHERE U.UbicacionNombre LIKE @UbicacionNombre" &
                    " OR U.UbicacionTag LIKE @UbicacionNombre"
        End Get
    End Property

    Public ReadOnly Property ItemDesID() As String
        Get
            Return " SELECT * FROM UBICACIONES" &
                    " WHERE UbicacionNombre Like @UbicacionNombre " &
                    " AND TipoUbicaID = 'R'" &
                    " AND UbicacionPadreID IN " &
                        " (SELECT UbicacionID FROM EMPRESA_ORIGENPTA EO)"
        End Get
    End Property

    Public ReadOnly Property NextID() As String
        Get
            Return " SELECT COALESCE(MAX(CAST(RIGHT(UbicacionID, LEN(UbicacionID)-1) as int)),0) + 1 as ubicacion_nextid " &
                    " FROM UBICACIONES " &
                    " WHERE UbicacionID <> 'ROOT'" &
                    " AND LEFT(UbicacionID,1)= @keytub"
        End Get
    End Property

    Public ReadOnly Property Update() As String
        Get
            Return " "
        End Get
    End Property


    Public ReadOnly Property Delete() As String
        Get
            Return " DELETE FROM UBICACIONES" &
                   " WHERE UbicacionID = @UbicacionID"
        End Get
    End Property

End Class



Public Class SQLVariedad
    Inherits Sql

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub

    Public ReadOnly Property ListComboCultivoV() As String
        Get
            Return " SELECT VariedadID,left(COALESCE(C.CultivoNombre,''),LEN(COALESCE(C.CultivoNombre,''))) +'  '+'->'+'  '+left(V.VariedadNombre,LEN(V.VariedadNombre)) as Variedad" &
                    " FROM CULTIVO C" &
                    " LEFT JOIN VARIEDAD V ON (V.CultivoID=C.CultivoID)" &
                    " WHERE VariedadStatus = 'A'"
        End Get
    End Property

    Public ReadOnly Property ListComboCulInv() As String
        Get
            Return " SELECT V.VariedadID,left(COALESCE(C.CultivoNombre,''),LEN(COALESCE(C.CultivoNombre,''))) +'  '+'->'+'  '+left(V.VariedadNombre,LEN(V.VariedadNombre)) as Variedad" &
                    " FROM VARIEDAD_EMPRESA VE" &
                    " LEFT JOIN VARIEDAD V ON (VE.VariedadID=V.VariedadID)" &
                    " LEFT JOIN CULTIVO C ON (V.CultivoID=C.CultivoID)" &
                    " WHERE VE.EmpresaID = EmpresaID"
        End Get
    End Property

    'Lista los cultivos que no existen en el detalle de variedades a mostrar exclusivamente en programa de siembra 

    Public ReadOnly Property ListaComboVarDetalle As String
        Get
            Return " SELECT VariedadID, VariedadNombre + '-' + CultivoNombre AS var_desvar" &
                    " FROM VARIEDAD V LEFT JOIN CULTIVO C On C.CultivoID = V.CultivoID" &
                    " WHERE Not EXISTS" &
                    " (SELECT * FROM VARIEDAD_EMPRESA VE WHERE V.VariedadID= VE.VariedadID)" &
                    " AND VariedadStatus = @status"
        End Get
    End Property

    Public ReadOnly Property List() As String
        Get
            Return " SELECT * FROM VARIEDAD" &
                    " WHERE VariedadStatus = 'A' "
        End Get
    End Property

    Public ReadOnly Property ListAll() As String
        Get
            Return " SELECT * " &
                    " FROM VARIEDAD V" &
                    " LEFT JOIN CULTIVO C ON (C.CultivoID=V.CultivoID)" &
                    " WHERE VariedadStatus = @Status AND V.CultivoID = @CultivoID " &
                    " AND V.VariedadNombre LIKE @Variedad "
        End Get
    End Property


    Public ReadOnly Property Item() As String
        Get
            Return " SELECT * FROM VARIEDAD" &
                    " WHERE VariedadID = @VariedadID "
        End Get
    End Property

    Public ReadOnly Property ItemID() As String
        Get
            Return " Select * FROM VARIEDAD V " &
                    " LEFT JOIN VARIEDAD_EMPRESA VE " &
                    " On (VE.VariedadID=V.VariedadID)" &
                    " LEFT JOIN CULTIVO C On (C.CultivoID=V.CultivoID)" &
                    " WHERE VE.EmpresaID = 10 " &
                    " And V.VariedadNombre Like @VariedadNombre " &
                    " And C.CultivoNombre Like @CultivoNombre "
        End Get
    End Property

    Public ReadOnly Property ItemIDOrg() As String
        Get
            Return " Select *" &
                    " FROM Get_VW_CULTIVO_VARIEDAD(@VariedadNombre,@CultivoNombre)" &
                    " WHERE TIpCul = 'Org' AND TipVar = 'Org' "
        End Get
    End Property

    Public ReadOnly Property ItemIDConv() As String
        Get
            Return " SELECT *" &
                    " FROM Get_VW_CULTIVO_VARIEDAD(@VariedadNombre,@CultivoNombre)" &
                    " WHERE TIpCul = '' AND TipVar = '' "
        End Get
    End Property


    Public ReadOnly Property Organico() As String
        Get
            Return " SELECT dbo.Get_CultivoOrganico(@VariedadNombre,@CultivoNombre) AS Organico"
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



Public Class SQL_VARIEDADES_EMP
    Inherits Sql
    Private m_sLog As String
    Private Ds As New DataSet


    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
        m_sLog = oUsr.Mis.Log
    End Sub


    Public ReadOnly Property Lista As String
        Get
            Return " SELECT VE.VariedadID,E.EmpresaNombre, C.CultivoNombre, V.VariedadNombre " &
                    " FROM VARIEDAD_EMPRESA VE LEFT JOIN VARIEDAD V On (V.VariedadID=VE.VariedadID)" &
                    " LEFT JOIN EMPRESAS E ON (E.EmpresaID=VE.EmpresaID) " &
                    " left JOIN CULTIVO C ON (C.CultivoID=V.CultivoID)" &
                    " WHERE VE.EmpresaID = @EmpresaID" &
                    " AND CultivoNombre LIKE @Cultivo" &
                    " And VariedadNombre Like @Variedad" &
                    " ORDER BY CultivoNombre asc"
        End Get
    End Property


    Public ReadOnly Property Item As String
        Get
            Return " SELECT * FROM VARIEDAD_EMPRESA " &
                   " WHERE VariedadID = @VariedadID"
        End Get
    End Property


    Public ReadOnly Property DeleteCul As String
        Get
            Return " DELETE FROM VARIEDAD_EMPRESA" &
                    " WHERE VariedadID = @VariedadID"
        End Get
    End Property


End Class



Public Class SQLEmpresasInv
    Inherits Sql

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub

    Public ReadOnly Property ListBasica() As String
        Get
            Return "SELECT U.UbicacionID,U.UbicacionNombre " &
                    " FROM EMPRESA_ORIGENPTA EO " &
                    " LEFT JOIN UBICACIONES U" &
                    " ON (EO.UbicacionID=U.UbicacionID)"
        End Get
    End Property

    Public ReadOnly Property List() As String
        Get
            Return "  "
        End Get
    End Property

    Public ReadOnly Property Item() As String
        Get
            Return " SELECT * FROM EMPRESAS" &
                    " WHERE EmpresaID = @EmpresaID "
        End Get
    End Property

    Public ReadOnly Property Update() As String
        Get
            Return "  "
        End Get
    End Property


End Class



Public Class SQLDestinoInv
    Inherits Sql

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub

    Public ReadOnly Property ListCombo() As String
        Get
            Return " SELECT UbicacionID,UbicacionNombre " &
                    " FROM UBICACIONES" &
                    " WHERE TipoUbicaID = 'R'" &
                    " AND UbicacionPadreID = @UbicacionPadreID"
        End Get
    End Property

    Public ReadOnly Property ListBasica() As String
        Get
            Return " SELECT * FROM UBICACIONES" &
                    " WHERE TipoUbicaID = 'R'" &
                    " AND UbicacionPadreID = @UbicacionPadreID"
        End Get
    End Property

    Public ReadOnly Property List() As String
        Get
            Return "  "
        End Get
    End Property

    Public ReadOnly Property Item() As String
        Get
            Return "  "
        End Get
    End Property

    Public ReadOnly Property Update() As String
        Get
            Return "  "
        End Get
    End Property


End Class



Public Class SQLInvernaderos
    Inherits Sql

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub

    Public ReadOnly Property ListBasica() As String
        Get
            Return " SELECT UbicacionID,UbicacionTag " &
                    " FROM UBICACIONES" &
                    " WHERE UbicacionPadreID IN ('I1','I2','I28','I29')" &
                    " AND UbicacionID NOT IN ('I2')"
        End Get
    End Property

    Public ReadOnly Property List() As String
        Get
            Return "  "
        End Get
    End Property

    Public ReadOnly Property Item() As String
        Get
            Return "  "
        End Get
    End Property

    Public ReadOnly Property Update() As String
        Get
            Return "  "
        End Get
    End Property


End Class


Public Class SQLContenedoresInv
    Inherits Sql

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub

    Public ReadOnly Property ListBasica() As String
        Get
            Return " SELECT con_keycon, con_capaci " &
                    " FROM CONTENEDOR " &
                    " WHERE con_status = 'A'" &
                    " AND con_descon LIKE 'CHAROLA%'"
        End Get
    End Property

    Public ReadOnly Property List() As String
        Get
            Return "  "
        End Get
    End Property

    Public ReadOnly Property Item() As String
        Get
            Return "  "
        End Get
    End Property

    Public ReadOnly Property ItemID() As String
        Get
            Return " SELECT * FROM CONTENEDOR" &
                    " WHERE con_descon Like 'CHAROLA%' " &
                    " AND con_capaci LIKE @capaci "
        End Get
    End Property

    Public ReadOnly Property Update() As String
        Get
            Return "  "
        End Get
    End Property


End Class



Public Class SQLLineaProductoInv
    Inherits Sql

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub

    Public ReadOnly Property ListBasica() As String
        Get
            Return " SELECT * FROM LINEA_PRODUCTO " &
                    " WHERE lnp_status = 'A'"
        End Get
    End Property

    Public ReadOnly Property List() As String
        Get
            Return "  "
        End Get
    End Property

    Public ReadOnly Property Item() As String
        Get
            Return "  "
        End Get
    End Property

    Public ReadOnly Property Update() As String
        Get
            Return "  "
        End Get
    End Property


End Class


Public Class SQLCultivo
    Inherits Sql
    Private m_sLog As String
    Private Ds As New DataSet

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
        m_sLog = oUsr.Mis.Log
    End Sub

    Public ReadOnly Property Lista As String
        Get
            Return "SELECT * FROM CULTIVO" &
                    " WHERE CultivoNombre LIKE @Cultivo"
        End Get
    End Property


    Public ReadOnly Property ListaCombo As String
        Get
            Return " SELECT cul_keycul, cul_descul, COUNT(cul_keycul)" &
                   " FROM NM_CULTIVOS C LEFT JOIN AA_VARIEDADES V ON (V.var_keycul=C.cul_keycul)" &
                   " WHERE EXISTS" &
                   " (SELECT * FROM AA_PROGRAMA_SIEMBRA P WHERE P.psi_keyvar=v.var_keyvar AND CONVERT(VARCHAR(10), P.psi_keyfec, 103)= @keyfec )" &
                   " AND V.var_status = @status" &
                   " GROUP BY C.cul_keycul, C.cul_descul"
        End Get
    End Property


    Public ReadOnly Property ListaComboVariedadesbyCul As String
        Get
            Return " SELECT * FROM AA_VARIEDADES" &
                   " WHERE var_keycul = @keycul AND var_status = @status"
        End Get
    End Property


    Public ReadOnly Property ListaComboCul_Disp As String
        Get
            Return " SELECT cul_keycul, ltrim(cul_descul)+space(1) as cul_descul, cul_status, COUNT(*) FROM NM_CULTIVOS C LEFT JOIN AA_VARIEDADES V ON (V.var_keycul=C.cul_keycul)" &
                    " WHERE EXISTS" &
                    " (SELECT * FROM AA_VARIEDADES_EMPRESA WHERE var_keyvar= cem_keyvar)" &
                    " AND var_status = @status" &
                    " GROUP BY cul_keycul,cul_descul,cul_status"
        End Get
    End Property

    Public ReadOnly Property ListaComboCul_Rest As String
        Get
            Return " SELECT * FROM VW_CULTIVOS_VIGENTES" &
            " WHERE Not EXISTS" &
            " (SELECT * FROM AA_RENDIMIENTO_CONF" &
            " WHERE cul_keycul = ren_keycul AND ren_keycon " &
            " IN (SELECT con_keycon FROM AA_CONTENEDOR " &
            " WHERE con_descon LIKE '%charola%'))"
        End Get
    End Property


    Public ReadOnly Property Item As String
        Get
            Return "SELECT * FROM CULTIVO WHERE CultivoID = @CultivoID"
        End Get
    End Property


    Public ReadOnly Property Itemname As String
        Get
            Return "SELECT CultivoNombre FROM CULTIVO WHERE CultivoID = @CultivoID"
        End Get
    End Property


    Public ReadOnly Property Delete As String
        Get
            Return " DELETE FROM CULTIVO" &
                    " WHERE CultivoID = @CultivoID"
        End Get
    End Property


End Class



Public Class SQL_CARGA_SALIDASPTA
    Inherits Sql

    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub

    Public ReadOnly Property ListBasica() As String
        Get
            Return " SELECT * FROM LINEA_PRODUCTO " &
                    " WHERE lnp_status = 'A'"
        End Get
    End Property

    Public ReadOnly Property List() As String
        Get
            Return " SELECT psl_keysal,psl_fecsal,E.UbicacionNombre,D.UbicacionNombre as UbicacionDestino,V.VariedadNombre,C.CultivoNombre,psl_nolote," &
                    " CONVERT(VARCHAR,psl_fecsie1, 103) as psl_fecsie1, " &
                    " Case When CONVERT(VARCHAR,psl_fecsie2, 103) = '01/01/1900' THEN ''" &
                    " ELSE CONVERT(VARCHAR,psl_fecsie2, 103) END as psl_fecsie2," &
                    " CASE WHEN CONVERT(VARCHAR,psl_fecsie3, 103) = '01/01/1900' THEN '' " &
                    " ELSE CONVERT(VARCHAR,psl_fecsie3, 103) END as psl_fecsie3, " &
                    " I.UbicacionTag,psl_cajvac,psl_cancon,CN.con_capaci,psl_cancjs,psl_canpta,psl_porlat,psl_porger," &
                    " psl_porrec,psl_observ,psl_obsmost,psl_canconf,psl_conadd,psl_cjsadd,psl_canhas,psl_tipsal,psl_stsprc" &
                    " FROM PRESALIDAS_PLANTA PS" &
                    " LEFT JOIN EMPRESA_ORIGENPTA EO On (PS.psl_keycia=EO.UbicacionID)" &
                    " LEFT JOIN UBICACIONES E On (EO.UbicacionID=E.UbicacionID)" &
                    " LEFT JOIN UBICACIONES D On (D.UbicacionID=PS.psl_keydes)" &
                    " LEFT JOIN VARIEDAD V On (V.VariedadID=PS.psl_keyvar)" &
                    " LEFT JOIN CULTIVO C On (C.CultivoID=V.CultivoID)" &
                    " LEFT JOIN UBICACIONES I On (I.UbicacionID=PS.psl_keyubi)" &
                    " LEFT JOIN CONTENEDOR CN On (CN.con_keycon=PS.psl_keycon)" &
                    " LEFT JOIN LINEA_PRODUCTO LP On (LP.lnp_keylnp=PS.psl_keylnp)" &
                    " WHERE psl_status = 'A'" &
                    " And psl_keysal Like @keysal" &
                    " And Convert(VARCHAR, psl_fecsal, 103) Like @Fecha " &
                    " And COALESCE(E.UbicacionNombre,'') Like @Empresa" &
                    " And COALESCE(D.UbicacionNombre,'') Like @Destino" &
                    " And COALESCE(V.VariedadNombre,'') Like @Variedad" &
                    " And COALESCE(C.CultivoNombre,'') Like @Cultivo" &
                    " And Convert(VARCHAR, psl_fecsie1, 103) Like @Fecsiem1" &
                    " And COALESCE(I.UbicacionTag,'') Like @Inv" &
                    " And PS.psl_stsprc = @stsprc"
        End Get
    End Property

    Public ReadOnly Property Item() As String
        Get
            Return " SELECT * FROM PRESALIDAS_PLANTA " &
                   " WHERE psl_keysal = @keysal"
        End Get
    End Property

    Public ReadOnly Property ItemEditar() As String
        Get
            Return " SELECT psl_keysal,CONVERT(VARCHAR,psl_fecsal, 103) as psl_fecsal,psl_keycia,psl_descia,psl_keydes,psl_desdes,psl_keyvar,psl_desvar,psl_descul,psl_nolote," &
                    " CONVERT(VARCHAR,psl_fecsie1, 103) As psl_fecsie1," &
                    " Case When CONVERT(VARCHAR,psl_fecsie2, 103) = '01/01/1900' THEN '' " &
                    " ELSE CONVERT(VARCHAR,psl_fecsie2, 103) END as psl_fecsie2," &
                    " CASE WHEN CONVERT(VARCHAR,psl_fecsie3, 103) = '01/01/1900' THEN ''" &
                    " ELSE CONVERT(VARCHAR,psl_fecsie3, 103) END as psl_fecsie3," &
                    " psl_keyubi,psl_desubi,psl_cajvac,psl_cancon,psl_keycon,psl_descon,psl_cancjs," &
                    " psl_conadd,psl_cjsadd,psl_canpta,psl_porlat,psl_porger,psl_porrec,psl_observ,psl_obsmost,psl_canconf,psl_canhas," &
                    " psl_keylnp,psl_deslnp,psl_status,psl_tipsal,psl_stsprc" &
                    " FROM PRESALIDAS_PLANTA" &
                    " WHERE psl_keysal = @keysal"
        End Get
    End Property

    Public ReadOnly Property ItemStatus() As String
        Get
            Return " SELECT psl_status,psl_stsprc " &
                    " FROM PRESALIDAS_PLANTA " &
                    " WHERE psl_keysal = @keysal "
        End Get
    End Property

    Public ReadOnly Property Update() As String
        Get
            Return "  "
        End Get
    End Property

    Public ReadOnly Property Delete() As String
        Get
            Return " DELETE FROM PRESALIDAS_PLANTA" &
                    " WHERE psl_keysal = @keysal "
        End Get
    End Property

    Public ReadOnly Property NextID As String
        Get
            Return "SELECT NEXT VALUE FOR SEQ_SALPTA_ID As NEXTID"
        End Get
    End Property


End Class