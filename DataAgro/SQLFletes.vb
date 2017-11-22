Imports System
Imports System.IO
Imports Security_System
Public Class SQLFletes

    Inherits SQL
    Private m_sLog As String
    Private Ds As New DataSet


    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
        m_sLog = oUsr.Mis.Log
    End Sub

    Public ReadOnly Property ListaForCombo As String
        Get
            Return " "
        End Get
    End Property

    Public ReadOnly Property Lista As String
        Get
            Return " "
        End Get
    End Property

    Public ReadOnly Property Item As String
        Get
            Return " SELECT * FROM Flete" &
                    " WHERE FleteID = @keyfle "
        End Get
    End Property


    Public ReadOnly Property ItemArr As String
        Get
            Return " SELECT sal_keysal,sal_observ" &
                   " FROM AA_SALIDAS_PLANTA" &
                   " WHERE sal_status = 'A' AND sal_exfle = 'N' "
        End Get
    End Property


    Public ReadOnly Property Delete As String
        Get
            Return " DELETE FROM FLETE " &
                   " WHERE FleteID = @FleteID"
        End Get
    End Property


End Class




Public Class SQLFletesPlanta

    Inherits SQL
    Private m_sLog As String
    Private Ds As New DataSet


    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
        m_sLog = oUsr.Mis.Log
    End Sub

    Public ReadOnly Property ListaForCombo As String
        Get
            Return " "
        End Get
    End Property

    Public ReadOnly Property Lista As String
        Get
            Return "  SELECT F.FleteID,FleteFecha,FleteFechaEnvio,FleteFechaRecibo," &
             " left(COALESCE(UO.UbicacionNombre,''),LEN(COALESCE(UO.UbicacionNombre,''))) +'  '+'->'+'  '+left(UD.UbicacionNombre,LEN(UD.UbicacionNombre)) as rut_desrut," &
             " T.TransportistaNombre," &
             " (left(COALESCE(TC.CamionDescripcion,''),LEN(COALESCE(TC.CamionDescripcion,''))) + ' [N ECO] ' + left(COALESCE(TC.CamionEconomico,''),LEN(COALESCE(TC.CamionEconomico,''))) + ' [PLC] ' + left(COALESCE(TC.CamionPlacas,''),LEN(COALESCE(TC.CamionPlacas,'')))) as cam_descam," &
             " left(COALESCE(O.OperadorNombre,''),LEN(COALESCE(O.OperadorNombre,''))) +'  '+','+'  '+left(O.OperadorApellidos,LEN(O.OperadorApellidos)) as Ope_desope," &
             " F.FleteObservacion,UR.SgUserName AS usu_nombre,SP.sal_keysal,sal_fecsal," &
             " E.ubi_desubi as ubi_descia,UBR.ubi_desubi as ubi_desdes,C.cul_descul,V.var_desvar,sal_nolote,sal_fecsie," &
             " UB.ubi_desubi, sal_cancon, sal_cancjs, sal_porlat, sal_porger, sal_observ,sal_canconf," &
             " CASE WHEN sal_tipsal = 'C' THEN 'CAMPO' " &
             " WHEN sal_tipsal = 'D' THEN 'DESECHO'" &
             " ELSE NULL END as sal_tipsal" &
             " FROM AgroGE.dbo.FLETE F " &
             " LEFT JOIN AgroGE.dbo.FLETEPLANTA FP" &
             " ON (FP.FleteID=F.FleteID)" &
             " LEFT JOIN AgroGE.dbo.AA_SALIDAS_PLANTA SP" &
             " ON (SP.sal_keysal = FP.SalidaPlantaID)" &
             " LEFT JOIN RUTA R ON (R.RutaID=F.RutaID)" &
             " LEFT JOIN RUTACOSTO RC ON (RC.RutaID=R.RutaID)" &
             " LEFT JOIN UBICACIONES UO ON (R.OrigenID=UO.UbicacionID)" &
             " LEFT JOIN UBICACIONES UD ON (R.DestinoID=UD.UbicacionID)" &
             " LEFT JOIN TIPOTRANSPORTE TT ON (TT.TipoTransporteID=RC.TipoTransporteID)" &
             " LEFT JOIN TRANSPORTISTACAMION TC ON (TC.TipoTransporteID=TT.TipoTransporteID And TC.TransportistaID=F.TransportistaID And TC.CamionID=F.CamionID)" &
             " LEFT JOIN TIPOFLETETRANSPORTISTA TFT ON (TFT.TipoFleteID=F.TipoFleteID AND TFT.TransportistaID = F.TransportistaID)" &
             " LEFT JOIN TRANSPORTISTA T ON (T.TransportistaID=TFT.TransportistaID And T.TransportistaID=TC.TransportistaID)" &
             " LEFT JOIN TRANSPORTISTAOPERADOR O ON (O.TransportistaID=T.TransportistaID AND O.TransportistaID = F.TransportistaID AND O.OperadorID=F.OperadorID)" &
             " LEFT JOIN TIPOFLETE TF ON (TF.TipoFleteID=TFT.TipoFleteID)" &
             " LEFT JOIN TIPOFLETEUSER TFU ON (TFU.TipoFleteID=TF.TipoFleteID)" &
             " LEFT JOIN SGUSUARIOS U ON (U.SgUserID = TFU.UserFleteIDS)" &
             " LEFT JOIN SGUSUARIOS UG ON (UG.SgUserID = F.FleteUsuario)" &
             " LEFT JOIN SGUSUARIOS UTF ON (UTF.SgUserID=TF.TipoFleteResponsable)" &
             " LEFT JOIN SGUSUARIOS UR ON (UR.SgUserID=F.FleteUsuarioRecibe)" &
             " LEFT JOIN AgroGE.dbo.NM_UBICACIONES UB ON (UB.ubi_keyubi = SP.sal_keyubi And UB.ubi_keytub = 'I')" &
             " LEFT Join AgroGE.dbo.NM_UBICACIONES UBR ON (UBR.ubi_keyubi=SP.sal_keydes And UBR.ubi_keytub = 'R')" &
             " LEFT JOIN AgroGE.dbo.NM_UBICACIONES E ON (E.ubi_keyubi=SP.sal_keycia)" &
             " LEFT JOIN AgroGE.dbo.AA_VARIEDADES V ON (V.var_keyvar= SP.sal_keyvar)" &
             " LEFT JOIN AgroGE.dbo.NM_CULTIVOS C ON (C.cul_keycul= V.var_keycul)" &
             " WHERE TFU.UserFleteIDS = @keyusu " &
                        " AND TF.TipoFleteID = 1 " &
                        " AND F.FleteID LIKE @keyfle" &
                        " AND CONVERT(VARCHAR(12),FleteFecha,103) LIKE @keyfec " &
                        " AND SP.sal_keysal LIKE @keysal" &
                        " AND CASE WHEN sal_tipsal = 'C' THEN 'CAMPO' " &
                        " WHEN sal_tipsal = 'D' THEN 'DESECHO'" &
                        " ELSE NULL END LIKE @tipsal" &
                        " AND FleteFecha between @fecini and @fecfin" &
                        " ORDER BY F.FleteID desc,sal_keysal desc"
        End Get
    End Property

    Public ReadOnly Property Item As String
        Get
            Return " SELECT * FROM FLETEPLANTA" &
                   " WHERE FleteID = @keyfle AND SalidaPlantaID = @keysal "
        End Get
    End Property


    Public ReadOnly Property ObtenerFechaIni As String
        Get
            Return " SELECT TOP 1 FleteID,CONVERT(VARCHAR(10), FleteFecha, 103) as FleteFecha FROM FLETE " &
                    " ORDER BY CAST(FleteFecha AS DATE) asc"
        End Get
    End Property

    Public ReadOnly Property ObtenerFechaFin As String
        Get
            Return " SELECT TOP 1 FleteID,CONVERT(VARCHAR(10), FleteFecha, 103) as FleteFecha FROM FLETE " &
                    " ORDER BY CAST(FleteFecha AS DATE) desc"
        End Get
    End Property


End Class


Public Class SQLFletesSemilla

    Inherits SQL
    Private m_sLog As String
    Private Ds As New DataSet


    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
        m_sLog = oUsr.Mis.Log
    End Sub

    Public ReadOnly Property ListaForCombo As String
        Get
            Return " "
        End Get
    End Property

    Public ReadOnly Property Lista As String
        Get
            Return " SELECT F.FleteID,FleteFecha,FleteFechaEnvio,FleteFechaRecibo," &
             " left(COALESCE(UO.UbicacionNombre,''),LEN(COALESCE(UO.UbicacionNombre,''))) +'  '+'->'+'  '+left(COALESCE(UD.UbicacionNombre,''),LEN(COALESCE(UD.UbicacionNombre,''))) as rut_desrut," &
             " T.TransportistaNombre," &
             " (left(COALESCE(TC.CamionDescripcion,''),LEN(COALESCE(TC.CamionDescripcion,''))) + ' [N ECO] ' + left(COALESCE(TC.CamionEconomico,''),LEN(COALESCE(TC.CamionEconomico,''))) + ' [PLC] ' + left(COALESCE(TC.CamionPlacas,''),LEN(COALESCE(TC.CamionPlacas,'')))) as cam_descam," &
             " left(COALESCE(O.OperadorNombre,''),LEN(COALESCE(O.OperadorNombre,''))) +'  '+','+'  '+left(COALESCE(O.OperadorApellidos,''),LEN(COALESCE(O.OperadorApellidos,''))) as Ope_desope," &
             " F.FleteObservacion,UR.SgUserName,SM.SalidaSemillaIDF,SalidaSemillaFecha," &
             " AUO.UbicacionNombre as AlmOrg,AUD.UbicacionNombre as AlmDes," &
             " SalidaSemillaEncargadoSiembra,SalidaSemillaLotes" &
             " FROM FLETE F " &
             " LEFT JOIN FLETESEMILLA FP" &
             " ON (FP.FleteID=F.FleteID)" &
             " LEFT JOIN SALIDASEMILLA SM" &
             " ON (SM.SalidaSemillaIDF = FP.SalidaSemillaIDF)" &
             " LEFT JOIN RUTA R ON (R.RutaID=F.RutaID)" &
             " LEFT JOIN RUTACOSTO RC ON (RC.RutaID=R.RutaID)" &
             " LEFT JOIN UBICACIONES UO ON (R.OrigenID=UO.UbicacionID)" &
             " LEFT JOIN UBICACIONES UD ON (R.DestinoID=UD.UbicacionID)" &
             " LEFT JOIN TIPOTRANSPORTE TT ON (TT.TipoTransporteID=RC.TipoTransporteID)" &
             " LEFT JOIN TRANSPORTISTACAMION TC ON (TC.TipoTransporteID=TT.TipoTransporteID AND TC.TransportistaID=F.TransportistaID AND TC.CamionID=F.CamionID)" &
             " LEFT JOIN TIPOFLETETRANSPORTISTA TFT ON (TFT.TipoFleteID=F.TipoFleteID And TFT.TransportistaID = F.TransportistaID)" &
             " LEFT JOIN TRANSPORTISTA T ON (T.TransportistaID=TFT.TransportistaID AND T.TransportistaID=TC.TransportistaID)" &
             " LEFT JOIN TRANSPORTISTAOPERADOR O ON (O.TransportistaID=T.TransportistaID And O.TransportistaID = F.TransportistaID And O.OperadorID=F.OperadorID)" &
             " LEFT JOIN TIPOFLETE TF ON (TF.TipoFleteID=TFT.TipoFleteID)" &
             " LEFT JOIN TIPOFLETEUSER TFU ON (TFU.TipoFleteID=TF.TipoFleteID)" &
             " LEFT JOIN SGUSUARIOS U ON (U.SgUserID = TFU.UserFleteIDS)" &
             " LEFT JOIN SGUSUARIOS UG ON (UG.SgUserID = F.FleteUsuario)" &
             " LEFT JOIN SGUSUARIOS UTF ON (UTF.SgUserID=TF.TipoFleteResponsable)" &
             " LEFT JOIN SGUSUARIOS UR ON (UR.SgUserID=F.FleteUsuarioRecibe) " &
             " LEFT JOIN ALMACENES ALOR ON (ALOR.EmpresaID=SM.EmpresaID AND ALOR.UbicacionID=SM.UbicacionID)" &
             " LEFT JOIN ALMACENES ALDES ON (ALDES.EmpresaID=SM.EmpresaID And ALDES.UbicacionID=SM.UbicacionID) " &
             " LEFT JOIN UBICACIONES AUO ON (AUO.UbicacionID=ALOR.UbicacionID)" &
             " LEFT JOIN UBICACIONES AUD ON (AUD.UbicacionID=ALDES.UbicacionID)" &
             " LEFT JOIN EMPRESAS EO ON (EO.EmpresaID=ALOR.EmpresaID)" &
             " LEFT JOIN EMPRESAS ED ON (ED.EmpresaID=ALDES.EmpresaID)" &
             " WHERE TFU.UserFleteIDS = @keyusu " &
             " And TF.TipoFleteID = 2 " &
             " AND F.FleteID LIKE @FleteID " &
             " And CONVERT(VARCHAR(12),FleteFecha,103) Like @FleteFecha " &
             " And COALESCE(SM.SalidaSemillaIDF, 0) Like @SalidaSemillaIDF " &
             " AND FleteFecha between @fecini and @fecfin" &
             " ORDER BY F.FleteID desc,SM.SalidaSemillaIDF desc"
        End Get
    End Property

    Public ReadOnly Property Item As String
        Get
            Return " SELECT * FROM FLETESEMILLA" &
                   " WHERE FleteID = @keyfle And SalidaSemillaIDF = @SalidaSemillaIDF"
        End Get
    End Property

    Public ReadOnly Property ItemNumSalida As String
        Get
            Return " SELECT F.FleteID,COALESCE(FS.SalidaSemillaIDF,0) as SalidaSemillaIDF " &
                    " FROM Flete F" &
                    " LEFT JOIN FLETESEMILLA FS" &
                    " ON (FS.FleteID=FS.FleteID)" &
                    " WHERE F.FleteID = @FleteID"
        End Get
    End Property

    Public ReadOnly Property ObtenerFechaIni As String
        Get
            Return " Select TOP 1 FleteID,CONVERT(VARCHAR(10), FleteFecha, 103) As FleteFecha FROM FLETE  " &
                    " WHERE TipoFleteID = @TipoFleteID " &
                    " ORDER BY CAST(FleteFecha As Date) asc"
        End Get
    End Property

    Public ReadOnly Property ObtenerFechaFin As String
        Get
            Return " Select TOP 1 FleteID,CONVERT(VARCHAR(10), FleteFecha, 103) As FleteFecha FROM FLETE " &
                    " WHERE TipoFleteID = @TipoFleteID " &
                    " ORDER BY CAST(FleteFecha As Date) desc"
        End Get
    End Property


End Class


Public Class SQLRutas
    Inherits SQL
    Private m_sLog As String
    Private Ds As New DataSet


    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
        m_sLog = oUsr.Mis.Log
    End Sub

    Public ReadOnly Property ListaForCombo As String
        Get
            Return " Select R.RutaID," &
                    " left(COALESCE(O.UbicacionNombre,''),LEN(COALESCE(O.UbicacionNombre,''))) +'  '+'->'+'  '+left(D.UbicacionNombre,LEN(D.UbicacionNombre)) as rut_desrut," &
                    " R.RutaDistancia, R.RutaStatus" &
                    " FROM RUTA R " &
                    " LEFT JOIN UBICACIONES O On (R.OrigenID=O.UbicacionID)" &
                    " LEFT JOIN UBICACIONES D ON (D.UbicacionID=R.DestinoID)" &
                    " LEFT JOIN RUTATIPOFLETE TF ON (TF.RutaID = R.RutaID)" &
                    " WHERE RutaStatus = @status AND TF.TipoFleteID = @keytpf"
        End Get
    End Property

    Public ReadOnly Property Lista As String
        Get
            Return " SELECT R.RutaID,O.UbicacionNombre as ubi_desubiO,D.UbicacionNombre as ubi_desubiD," &
                    " left(COALESCE(O.UbicacionNombre,''),LEN(COALESCE(O.UbicacionNombre,''))) +'  '+'->'+'  '+left(D.UbicacionNombre,LEN(D.UbicacionNombre)) as rut_desrut," &
                    " R.RutaDistancia, R.RutaStatus" &
                    " FROM RUTA R" &
                    " LEFT JOIN UBICACIONES O ON (R.OrigenID=O.UbicacionID)" &
                    " LEFT JOIN UBICACIONES D ON (D.UbicacionID=R.DestinoID)" &
                    " WHERE RutaStatus = @status"
        End Get
    End Property

    Public ReadOnly Property Item As String
        Get
            Return " SELECT * FROM RUTA R " &
                    " WHERE RutaID = @keyrut "
        End Get
    End Property


    Public ReadOnly Property ItemRuta As String
        Get
            Return " SELECT R.RutaID," &
                    " left(COALESCE(O.UbicacionNombre,''),LEN(COALESCE(O.UbicacionNombre,''))) +'  '+'->'+'  '+left(D.UbicacionNombre,LEN(D.UbicacionNombre)) as rut_desrut," &
                    " R.RutaDistancia, R.RutaStatus" &
                    " FROM RUTA R " &
                    " LEFT JOIN UBICACIONES O ON (R.OrigenID=O.UbicacionID) " &
                    " LEFT JOIN UBICACIONES D ON (D.UbicacionID=R.DestinoID)  " &
                    " WHERE RutaID = @keyrut"
        End Get
    End Property


End Class



Public Class SQLRutas_Costos
    Inherits SQL
    Private m_sLog As String
    Private Ds As New DataSet


    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
        m_sLog = oUsr.Mis.Log
    End Sub

    Public ReadOnly Property ListaForCombo As String
        Get
            Return ""
        End Get
    End Property

    Public ReadOnly Property Lista As String
        Get
            Return " SELECT * FROM RUTACOSTO RC" &
                    " LEFT JOIN TIPOTRANSPORTE TPT ON (TPT.TipoTransporteID = RC.TipoTransporteID)" &
                    " LEFT JOIN VW_RUTAS_FLETES R On (R.RutaID=RC.RutaID)" &
                    " WHERE R.RutaStatus = @status"
        End Get
    End Property

    Public ReadOnly Property Item As String
        Get
            Return " Select * FROM RUTACOSTO" &
                    " WHERE RutaID = @keyrut And TipoTransporteID = @keytpt "
        End Get
    End Property


    Public ReadOnly Property ItemCostos As String
        Get
            Return " Select * FROM RUTACOSTO RC " &
                     " LEFT JOIN TIPOTRANSPORTE TPT On (TPT.TipoTransporteID = RC.TipoTransporteID)" &
                     " LEFT JOIN VW_RUTAS_FLETES R On (R.RutaID=RC.RutaID)" &
                     " LEFT JOIN TRANSPORTISTACAMION TC On (TC.TipoTransporteID=TPT.TipoTransporteID)" &
                     " WHERE TC.TransportistaID = @keytra And TC.CamionID = @keycam And R.RutaID = @keyrut "
        End Get
    End Property


End Class





Public Class SQL_TipoFlete
    Inherits SQL
    Private m_sLog As String
    Private Ds As New DataSet


    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
        m_sLog = oUsr.Mis.Log
    End Sub

    Public ReadOnly Property ListaForCombo As String
        Get
            Return " Select * FROM TIPOFLETE TF" &
                    " WHERE TF.TipoFleteID In " &
                    " (Select TipoFleteID FROM TIPOFLETEUSER " &
                    " WHERE UserFleteIDS = @keyusu) "
        End Get
    End Property

    Public ReadOnly Property Lista As String
        Get
            Return " "
        End Get
    End Property

    Public ReadOnly Property Item As String
        Get
            Return " Select * FROM TIPOFLETE" &
                   " WHERE TipoFleteID = @keytipofle"
        End Get
    End Property

    Public ReadOnly Property ItemTipoFlete As String
        Get
            Return " Select * FROM TIPOFLETE T" &
                   " LEFT JOIN TIPOFLETEUSER TFU On (TFU.TipoFleteID=T.TipoFleteID)" &
                   " LEFT JOIN SGUSUARIOS U On (U.SgUserID = TFU.UserFleteIDS)" &
                   " WHERE TFU.TipoFleteID = @keytpf"
        End Get
    End Property


End Class




Public Class SQL_Transportista
    Inherits SQL
    Private m_sLog As String
    Private Ds As New DataSet


    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
        m_sLog = oUsr.Mis.Log
    End Sub

    Public ReadOnly Property ListaForCombo As String
        Get
            Return " Select * FROM TRANSPORTISTA " &
                " WHERE TransportistaStatus = @status"
        End Get
    End Property

    Public ReadOnly Property ListaCombo As String
        Get
            Return "Select * FROM TRANSPORTISTA T " &
                    " Left JOIN TIPOFLETETRANSPORTISTA TFT On (TFT.TransportistaID=T.TransportistaID)" &
                    " LEFT JOIN TIPOFLETE TF On (TF.TipoFleteID=TFT.TipoFleteID)" &
                    " WHERE TF.TipoFleteID = @keytpf And T.TransportistaStatus = @status "
        End Get
    End Property

    Public ReadOnly Property Lista As String
        Get
            Return " Select * FROM TRANSPORTISTA " &
                   " WHERE TransportistaStatus = @status"
        End Get
    End Property

    Public ReadOnly Property Item As String
        Get
            Return " Select * FROM TRANSPORTISTA" &
                   " WHERE TransportistaID = @keytran"
        End Get
    End Property

End Class




Public Class SQL_Camion
    Inherits SQL
    Private m_sLog As String
    Private Ds As New DataSet


    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
        m_sLog = oUsr.Mis.Log
    End Sub

    Public ReadOnly Property ListaForCombo As String
        Get
            Return " Select CamionID, " &
                   " left(COALESCE(CamionDescripcion,''),LEN(COALESCE(CamionDescripcion,''))) +'  '+'|'+'  '+left(CamionPlacas,LEN(CamionPlacas)) +'  '+'|'+'  '+left(CamionEconomico,LEN(CamionEconomico)) as cam_descam" &
                   " FROM TRANSPORTISTACAMION" &
                   " WHERE CamionStatus = @status"
        End Get
    End Property

    Public ReadOnly Property ListaComboByTra As String
        Get
            Return " SELECT CamionID, " &
                    " left(COALESCE(CamionDescripcion,''),LEN(COALESCE(CamionDescripcion,''))) +'  '+'[ECO]'+'  '+left(CamionPlacas,LEN(CamionPlacas)) +'  '+'[PLC]'+'  '+left(CamionEconomico,LEN(CamionEconomico)) as cam_descam" &
                    " FROM TRANSPORTISTACAMION" &
                    " WHERE TransportistaID = @keytra AND CamionStatus = @status"
        End Get
    End Property

    Public ReadOnly Property Lista As String
        Get
            Return " SELECT CamionID,CamionDescripcion,CamionPlacas,CamionEconomico,T.TransportistaNombre,TP.TipoTransporteNombre" &
                    " FROM TRANSPORTISTACAMION C" &
                    " LEFT JOIN TRANSPORTISTA T ON (T.TransportistaID=C.TransportistaID)" &
                    " LEFT JOIN TIPOTRANSPORTE TP ON (TP.TipoTransporteID=C.TipoTransporteID)" &
                    " WHERE CamionStatus = @status"
        End Get
    End Property

    Public ReadOnly Property Item As String
        Get
            Return " SELECT * FROM TRANSPORTISTACAMION " &
                   " WHERE TransportistaID = @keycam"
        End Get
    End Property


    Public ReadOnly Property ItemCamion As String
        Get
            Return " SELECT CamionID, " &
                   " left(COALESCE(CamionDescripcion,''),LEN(COALESCE(CamionDescripcion,''))) +'  '+'[ECO]'+'  '+left(CamionPlacas,LEN(CamionPlacas)) +'  '+'[PLC]'+'  '+left(CamionEconomico,LEN(CamionEconomico)) as cam_descam " &
                   " FROM TRANSPORTISTACAMION " &
                   " WHERE CamionID = @keycam "
        End Get
    End Property



End Class



Public Class SQL_Operador
    Inherits SQL
    Private m_sLog As String
    Private Ds As New DataSet


    Public Sub New(ByVal oUsr As UserLogin)
        MyBase.New(oUsr)
        m_sLog = oUsr.Mis.Log
    End Sub

    Public ReadOnly Property ListaForCombo As String
        Get
            Return " SELECT OperadorID,left(COALESCE(OperadorNombre,''),LEN(COALESCE(OperadorNombre,''))) +'  '+'|'+'  '+left(OperadorApellidos,LEN(OperadorApellidos)) ope_nombre" &
                    " FROM TRANSPORTISTAOPERADOR" &
                    " WHERE OperadorStatus = @status"
        End Get
    End Property

    Public ReadOnly Property ListaComboByTra As String
        Get
            Return " SELECT OperadorID,left(COALESCE(OperadorNombre,''),LEN(COALESCE(OperadorNombre,''))) +'  '+''+'  '+left(COALESCE(OperadorApellidos,''),LEN(COALESCE(OperadorApellidos,''))) as ope_nombre" &
                    " FROM TRANSPORTISTAOPERADOR" &
                    " WHERE TransportistaID = @keytra AND OperadorStatus = @status"
        End Get
    End Property

    Public ReadOnly Property Lista As String
        Get
            Return " SELECT CamionID,CamionDescripcion,CamionPlacas,CamionEconomico,T.TransportistaNombre,TP.TipoTransporteNombre" &
                    " FROM TRANSPORTISTACAMION C" &
                    " LEFT JOIN TRANSPORTISTA T ON (T.TransportistaID=C.TransportistaID)" &
                    " LEFT JOIN TIPOTRANSPORTE TP ON (TP.TipoTransporteID=C.TipoTransporteID)" &
                    " WHERE CamionStatus = @status"
        End Get
    End Property

    Public ReadOnly Property Item As String
        Get
            Return " SELECT * FROM TRANSPORTISTAOPERADOR" &
                    " WHERE OperadorID = @keyope"
        End Get
    End Property


    Public ReadOnly Property ItemOperador As String
        Get
            Return " SELECT TransportistaID,OperadorID,left(COALESCE(OperadorNombre,''),LEN(COALESCE(OperadorNombre,''))) +'  '+''+'  '+left(COALESCE(OperadorApellidos,''),LEN(COALESCE(OperadorApellidos,''))) as ope_nombre " &
                    " FROM TRANSPORTISTAOPERADOR" &
                    " WHERE OperadorID = @keyope AND TransportistaID = @keytra"
        End Get
    End Property




End Class

