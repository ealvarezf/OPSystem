Imports Security_System

Public Class PrmEnt
    Public Property ParametroID As Integer
    Public Property ParametroValor As String
End Class
Public Class SQLInventarios
    Inherits SQL
    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub
    Public ReadOnly Property ListEnvaseGasto() As String
        Get
            Return " SELECT ConcatenaID, (UnidadEmpaqueNombre + ' ' + ProductoLote) UnidadEmpaqueNombre FROM VW_INVENTARIOS_FULL " &
                   "  WHERE EmpresaID = @EmpresaID " &
                   "    AND UbicacionID = @UbicacionID " &
                   "    AND ProductoLote LIKE @ProductoLote "
        End Get
    End Property

    Public ReadOnly Property ValorCultivo() As String
        Get
            Return " SELECT CultivoID FROM VARIEDAD WHERE VARIEDADID = @variedad "
        End Get
    End Property

    Public ReadOnly Property ValorUbicacionDestino() As String
        Get
            Return " SELECT UbicacionDestinoID FROM PROCESOS WHERE PROCESOID = @proceso "
        End Get
    End Property

    Public ReadOnly Property ValorSize() As String
        Get
            Return " SELECT ClasificaSizeNombre FROM FLETECOSECHA FC JOIN BASCULAGUIA BG ON FC.CosechaID = BG.GuiaID " &
                   " AND FC.VariedadID = BG.VariedadID AND FC.UbicacionID = BG.UbicacionID " &
                   " JOIN ClasificaSize CS on CS.ClasificaSizeID = FC.ClasificaSizeID " &
                   " WHERE FC.UbicacionID = @ubi AND FC.VariedadID = @vari AND FC.FleteID = @flete "
        End Get
    End Property
End Class
Public Class SQLDetalleGasto
    Inherits SQL
    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub
    Public ReadOnly Property List() As String
        Get
            Return " SELECT OrdenID, OrdenGastoID, OrdenGastoExT, GastoEmpresaID, GastoUbicacionID, GastoProductoID, ProductoNombre, OrdenGastoPesoBruto, " &
                   "        OrdenGastoPesoTara, GastoProductoLote, OrdenGastoInfo1, OrdenGastoPesoNeto, OrdenGastoPesoAcumulado " &
                   "   FROM ORDENPGASTO OG LEFT JOIN PRODUCTOS P ON P.EmpresaID = OG.GastoEmpresaID " &
                   "                                       AND P.ProductoID = OG.GastoProductoID " &
                   "  WHERE OrdenID = @OrdenID "
        End Get
    End Property

    Public ReadOnly Property Insert() As String
        Get
            Return " INSERT INTO ORDENPGASTO(OrdenID, OrdenGastoID, OrdenGastoExT, OrdenGastoPesoBruto, OrdenGastoPesoTara, " &
                   "                         GastoProductoID, GastoProductoLote, GastoEmpresaID, GastoUbicacionID) " &
                   "                  VALUES(@OrdenID, @OrdenGastoID, @OrdenGastoExT, @OrdenGastoPesoBruto, @OrdenGastoPesoTara," &
                   "                         @GastoProductoID, @GastoProductoLote, @GastoEmpresaID, @GastoUbicacionID) "
        End Get
    End Property

End Class
Public Class SQLDetalleProduccion
    Inherits SQL
    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub
    Public ReadOnly Property List() As String
        Get
            Return " SELECT OrdenID, OrdenProduccionID, ProduccionEmpresaID, ProduccionUbicacionID, ProduccionProductoID, ProductoNombre, ProductoCantidad, ProductoPeso, " &
                   "        ProduccionProductoLote, ProduccionRegistroID  " &
                   "   FROM ORDENPPRODUCCION OP LEFT JOIN PRODUCTOS P ON P.EmpresaID = OP.ProduccionEmpresaID " &
                   "                                                 AND P.ProductoID = OP.ProduccionProductoID " &
                   "  WHERE OrdenID = @OrdenID "
        End Get
    End Property

End Class
Public Class SQLProduccion
    Inherits SQL
    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub
    Public ReadOnly Property List() As String
        Get
            Return " SELECT O.ProcesoID, ProcesoNombre, OrdenID, OrdenFecha, OrdenFechaRegistro, dbo.Get_EntradaParametro(OrdenID,18) LoteOrigen, " &
                   " 	    dbo.BuildingInfo_LoteOrigen(dbo.Get_EntradaParametro(OrdenID,18)+'-') LoteOrigenInfo " &
                   "   FROM ORDENP O LEFT JOIN PROCESOS P ON P.ProcesoID = O.ProcesoID " &
                   "  WHERE O.ProcesoID = @ProcesoID "
        End Get
    End Property
    Public ReadOnly Property ItemView() As String
        Get
            Return " SELECT * FROM ORDENP OP LEFT JOIN PROCESOS P ON P.ProcesoID = OP.ProcesoID  " &
                   "  WHERE OrdenID = @OrdenID "
        End Get
    End Property

    Public ReadOnly Property Item() As String
        Get
            Return " SELECT * FROM ORDENP WHERE OrdenID = @OrdenID "
        End Get
    End Property

    Public ReadOnly Property ParameterEntries() As String
        Get
            Return " SELECT * FROM PARAMETROSENTRADA WHERE OrdenID = @OrdenID"
        End Get
    End Property

    Public ReadOnly Property InsertParameterEntries() As String
        Get
            Return " INSERT INTO PARAMETROSENTRADA(OrdenID, ParametroID, ParametroEntradaValor) " &
                   "   VALUES(@OrdenID, @ParametroID, @ParametroEntradaValor)"
        End Get
    End Property
    Public ReadOnly Property UpdateParameterEntries() As String
        Get
            Return " UPDATE PARAMETROSENTRADA SET ParametroEntradaValor = @ParametroEntradaValor " &
                   "  WHERE OrdenID = @OrdenID " &
                   "    AND ParametroID = @ParametroID "
        End Get
    End Property

End Class
Public Class SQLOrigen
    Inherits SQL
    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub
    Public ReadOnly Property List() As String
        Get
            Return " SELECT DISTINCT dbo.Building_LoteOrigen(ProductoLote) LoteOrigen, dbo.BuildingInfo_LoteOrigen(ProductoLote) LoteOrigenInfo  " &
                   "   FROM INVENTARIOS " &
                   "  WHERE EmpresaID = @EmpresaID " &
                   "    AND UbicacionID = @UbicacionID "
        End Get
    End Property

End Class
Public Class SQLProcesos
    Inherits SQL
    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub
    Public ReadOnly Property List() As String
        Get
            Return " SELECT * FROM PROCESOS "
        End Get
    End Property
    Public ReadOnly Property Item() As String
        Get
            Return " SELECT * FROM PROCESOS WHERE ProcesoID = @ProcesoID "
        End Get
    End Property

End Class
Public Class SQLTurnos
    Inherits SQL
    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub
    Public ReadOnly Property List() As String
        Get
            Return " SELECT * FROM TURNOS "
        End Get
    End Property
    Public ReadOnly Property Item() As String
        Get
            Return " SELECT * FROM TURNOS WHERE TurnoID = @TurnoID "
        End Get
    End Property

End Class
Public Class SQLResponsables
    Inherits SQL
    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub
    Public ReadOnly Property List() As String
        Get
            Return " SELECT * FROM RESPONSABLES "
        End Get
    End Property
    Public ReadOnly Property Item() As String
        Get
            Return " SELECT * FROM RESPONSABLES WHERE ResponsableID = @ResponsableID "
        End Get
    End Property

End Class
Public Class SQLEntradasParametros
    Inherits SQL
    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub
    Public ReadOnly Property List() As String
        Get
            Return " SELECT * FROM PARAMETROSENTRADA E LEFT Join PARAMETROS P ON P.ParametroID = E.ParametroID " &
                   "  WHERE OrdenID = @OrdenID "
        End Get
    End Property
    Public ReadOnly Property Item() As String
        Get
            Return " SELECT * FROM PARAMETROSENTRADA E LEFT Join PARAMETROS P ON P.ParametroID = E.ParametroID " &
                   "  WHERE OrdenID = @OrdenID " &
                   "    AND E.ParametroID = @ParametroID "
        End Get
    End Property

End Class
Public Class SQLMovimientosAlmacenDetalle
    Inherits SQL
    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub
    Public ReadOnly Property List() As String
        Get
            Return " SELECT MAD.ProductoID, ProductoNombre, UnidadEmpaqueNombre, UnidadEmpaquePeso, " &
                   "        MovAlmacenLote, MovAlmacenCantidad, MovAlmacenPeso " &
                   "   FROM MOVIMIENTOALMACENDETALLE MAD " &
                   "        LEFT JOIN MOVIMIENTOALMACEN MA ON MA.MovAlmacenSentido = MAD.MovAlmacenSentido AND MA.MovAlmacenID = MAD.MovAlmacenID " &
                   "	    LEFT JOIN PRODUCTOS PRD ON PRD.EmpresaID = MA.EmpresaID AND PRD.ProductoID = MAD.ProductoID " &
                   "	    LEFT JOIN ENVASES E ON E.EnvaseID = PRD.EnvaseID " &
                   "	    LEFT JOIN UNIDADEMPAQUE UE ON UE.UnidadEmpaqueID = PRD.EnvaseID " &
                   "  WHERE MAD.MovAlmacenSentido = @MovAlmacenSentido " &
                   "    AND MAD.MovAlmacenID = @MovAlmacenID "
        End Get
    End Property
    Public ReadOnly Property Item()
        Get
            Return " SELECT * FROM MOVIMIENTOALMACENDETALLE " &
                   "  WHERE MovAlmacenSentido = @MovAlmacenSentido " &
                   "    AND MovAlmacenID = @MovAlmacenID " &
                   "    AND ProductoID = @ProductoID " &
                   "    AND MovAlmacenLote = @MovAlmacenLote "
        End Get
    End Property
    Public ReadOnly Property Delete()
        Get
            Return " DELETE FROM MOVIMIENTOALMACENDETALLE " &
                   "  WHERE MovAlmacenSentido = @MovAlmacenSentido " &
                   "    AND MovAlmacenID = @MovAlmacenID " &
                   "    AND ProductoID = @ProductoID " &
                   "    AND MovAlmacenLote = @MovAlmacenLote "
        End Get
    End Property
End Class
Public Class SQLMovimientosAlmacen
    Inherits SQL
    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub
    Public ReadOnly Property List() As String
        Get
            Return " SELECT MA.EmpresaID, (MovAlmacenSentido + CAST(MovAlmacenID AS varchar(10))) MovAlmacenID, MovAlmacenFecha, dbo.GETDESUBICA(MA.UbicacionID) AS Almacen, " &
                   "       dbo.GETDESUBICA(MA.TablaID) AS Tabla, VariedadNombre, ProcesoNombre, MovAlmacenObs, MovAlmacenStsProceso " &
                   "  FROM MOVIMIENTOALMACEN  MA LEFT JOIN PROCESOS PR ON PR.ProcesoID = MA.ProcesoID " &
                   "                             LEFT JOIN ALMACENES AL ON AL.EmpresaID = MA.EmpresaID AND AL.UbicacionID = MA.UbicacionID " &
                   "							 LEFT JOIN TABLAS T ON T.TablaID = MA.TablaID " &
                   "							 LEFT JOIN VARIEDAD V ON V.VariedadID = MA.VariedadID " &
                   " WHERE MA.EmpresaID = @EmpresaID " &
                   "   AND MovAlmacenSentido LIKE @MovAlmacenSentido " &
                   "   AND MovAlmacenFecha >= @MovAlmacenFecha " &
                   "   AND MA.UbicacionID LIKE @UbicacionID " &
                   "   AND MA.TablaID LIKE @TablaID " &
                   "   AND MA.VariedadID LIKE @VariedadID "
        End Get
    End Property
    Public ReadOnly Property ItemInfo() As String
        Get
            Return " SELECT MovAlmacenSentido, MovAlmacenID, MovAlmacenObs, MovAlmacenStsProceso, ProcesoID, EmpresaID, UbicacionID, " &
                   "        dbo.GETPADRE(TablaID) RanchoID, TablaID, CultivoID, VA.VariedadID, MovAlmacenFecha " &
                   "   FROM MOVIMIENTOALMACEN MA LEFT JOIN VARIEDAD VA ON VA.VariedadID = MA.VariedadID " &
                   "  WHERE MovAlmacenSentido = @MovAlmacenSentido " &
                   "    AND MovAlmacenID = @MovAlmacenID "
        End Get
    End Property
    Public ReadOnly Property Item() As String
        Get
            Return " SELECT * FROM MOVIMIENTOALMACEN " &
                   "  WHERE MovAlmacenSentido = @MovAlmacenSentido " &
                   "    AND MovAlmacenID = @MovAlmacenID "
        End Get
    End Property

    Public ReadOnly Property NextEntradaID() As String
        Get
            Return "SELECT NEXT VALUE FOR SEQA_ENTRADA AS NEXTID"
        End Get
    End Property

    Public ReadOnly Property NextSalidaID() As String
        Get
            Return "SELECT NEXT VALUE FOR SEQA_SALIDA AS NEXTID"
        End Get
    End Property

    Public ReadOnly Property Delete() As String
        Get
            Return "DELETE FROM MOVIMIENTOALMACEN WHERE MovAlmacenSentido = @MovAlmacenSentido AND MovAlmacenID = @MovAlmacenID"
        End Get
    End Property

    Public ReadOnly Property PreLote() As String
        Get
            Return " SELECT dbo.Building_Lote(@ProcesoID, @TablaID, @VariedadID, @FechaID, @Extra) AS PreLote "
        End Get
    End Property

End Class
Public Class SQLAlmacen
    Inherits SQL
    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub
    Public ReadOnly Property List() As String
        Get
            Return " SELECT A.UbicacionID, UbicacionNombre " &
                   "   FROM ALMACENES A LEFT Join UBICACIONES U ON U.UbicacionID = A.UbicacionID " &
                   "  WHERE EmpresaID = @EmpresaID "
        End Get
    End Property

End Class
Public Class SQLProductos
    Inherits SQL
    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub
    Public ReadOnly Property List()
        Get
            Return " SELECT * " &
                   "  FROM PRODUCTOS P LEFT JOIN ENVASES E ON E.EnvaseID = P.EnvaseID " &
                   "                   LEFT JOIN UNIDADEMPAQUE UE ON UE.UnidadEmpaqueID = P.EnvaseID " &
                   "				   LEFT JOIN CLASIFICAPRODUCTO CL ON CL.ClasificaProductoID = P.ClasificaProductoID " &
                   "				   LEFT JOIN MARCAS M ON M.MarcaID = P.MarcaID "
        End Get
    End Property
    Public ReadOnly Property ListCombo() As String
        Get
            Return " SELECT ProductoID, COALESCE(ProductoNombre,'') + ' | ' +  COALESCE(TipoPrdID,'') + ' | ' + COALESCE(UnidadEmpaqueNombre,'') + ' | ' + COALESCE(ClasificaProductoNombre,'') + ' | ' + COALESCE(MarcaNombre,'')  Producto " &
                   "   FROM PRODUCTOS P LEFT JOIN ENVASES E ON E.EnvaseID = P.EnvaseID " &
                   "                    LEFT JOIN UNIDADEMPAQUE UE ON UE.UnidadEmpaqueID = P.EnvaseID " &
                   " 				    LEFT JOIN CLASIFICAPRODUCTO CL ON CL.ClasificaProductoID = P.ClasificaProductoID " &
                   " 				    LEFT JOIN MARCAS M ON M.MarcaID = P.MarcaID " &
                   " WHERE EmpresaID = @EmpresaID "
        End Get
    End Property
End Class
Public Class SQLRanchos
    Inherits SQL
    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub
    Public ReadOnly Property List() As String
        Get
            Return " SELECT DISTINCT RanchoID, UbicacionNombre " &
                   "   FROM TABLAS T LEFT JOIN UBICACIONES U ON U.UbicacionID = T.RanchoID "
        End Get
    End Property
End Class
Public Class SQLTablas
    Inherits SQL
    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub
    Public ReadOnly Property List() As String
        Get
            Return " SELECT TablaID, UbicacionNombre " &
                   "   FROM TABLAS T LEFT JOIN UBICACIONES U ON U.UbicacionID = T.TablaID " &
                   "  WHERE RanchoID = @RanchoID "
        End Get
    End Property
End Class
Public Class SQLCultivos
    Inherits SQL
    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub
    Public ReadOnly Property List() As String
        Get
            Return "SELECT * FROM CULTIVO"
        End Get
    End Property
End Class
Public Class SQLVariedades
    Inherits SQL
    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
    End Sub
    Public ReadOnly Property List() As String
        Get
            Return " SELECT * FROM VARIEDAD " &
                   "  WHERE CultivoID = @CultivoID "
        End Get
    End Property
End Class