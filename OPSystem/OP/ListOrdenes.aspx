<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ListOrdenes.aspx.vb" Inherits="OPSystem.ListOrdenes" %>

<%@ Register assembly="Infragistics45.Web.v17.1, Version=17.1.20171.1001, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v17.1, Version=17.1.20171.1001, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.GridControls" tagprefix="ig" %>

<%@ Register src="../ControlUser/BarEventos.ascx" tagname="BarEventos" tagprefix="uc1" %>

<%@ Register assembly="Infragistics45.Web.v17.1, Version=17.1.20171.1001, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v17.1, Version=17.1.20171.1001, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style2 {
            width: 150px;
        }
        .auto-style5 {
            width: 100%;
        }
        .auto-style6 {
            height: 28px;
        }
    </style>
</head>
<body>   
    <div class="panel-body">
    <form id="form1" runat="server">
        <asp:Panel ID="pnlEventos" runat="server">
            <uc1:BarEventos ID="BarEventos1" runat="server" />
        </asp:Panel>
        <asp:Panel ID="pnlAdd" runat="server">
            <asp:Wizard ID="Wizard1" runat="server" BackColor="#F7F6F3" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana" Font-Size="Large" Height="302px" Width="805px" ActiveStepIndex="0" BorderStyle="Solid" DisplayCancelButton="True">
                <HeaderStyle BackColor="#5D7B9D" BorderStyle="Solid" Font-Bold="True" Font-Size="0.9em" ForeColor="White" HorizontalAlign="Left" />
                <NavigationButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284775" />
                <SideBarButtonStyle Font-Names="Verdana" ForeColor="White" BorderWidth="0px" Width="150px" />
                <SideBarStyle BackColor="#888888" Font-Size="0.9em" VerticalAlign="Top" BorderWidth="0px" Width="150px" />
                <StepStyle ForeColor="#5D7B9D" BorderWidth="0px" />
                <WizardSteps>
                    <asp:WizardStep runat="server" title="ORDEN">
                        <div>
                            <table style="width:100%;">
                                <tr>
                                    <td class="auto-style2">
                                        &nbsp;</td>
                                    <td class="auto-style2">&nbsp;</td>
                                    <td>
                                        <asp:Label ID="lblAcción" runat="server" CssClass="col-md-2 control-label" Font-Bold="True" Font-Size="Large" Text="Evento"></asp:Label>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style2">
                                        &nbsp;</td>
                                    <td class="auto-style2">
                                        <asp:Panel ID="Panel1" runat="server">
                                            <asp:Label ID="Label3" runat="server" CssClass="col-md-2 control-label" Text="OrdenID"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel2" runat="server">
                                            <ig:WebTextEditor ID="txt_OrdenID" runat="server">
                                            </ig:WebTextEditor>
                                        </asp:Panel>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style2">
                                        &nbsp;</td>
                                    <td class="auto-style2">
                                        <asp:Panel ID="Panel6" runat="server">
                                            <asp:Label ID="Label4" runat="server" CssClass="col-md-2 control-label" Text="Fecha"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel3" runat="server">
                                            <ig:WebDatePicker ID="wdp_OrdenFecha" runat="server">
                                            </ig:WebDatePicker>
                                        </asp:Panel>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style2">
                                        &nbsp;</td>
                                    <td class="auto-style2">
                                        <asp:Panel ID="Panel8" runat="server">
                                            <asp:Label ID="Label6" runat="server" CssClass="col-md-2 control-label" Text="Proceso"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel5" runat="server">
                                            <ig:WebDropDown ID="ddl_ProcesoID" runat="server" Width="200px">
                                            </ig:WebDropDown>
                                        </asp:Panel>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style2">&nbsp;</td>
                                    <td class="auto-style2">
                                        <asp:Panel ID="Panel25" runat="server">
                                            <asp:Label ID="Label15" runat="server" CssClass="col-md-2 control-label" Text="Origen"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </div>
                        <div>

                            <table class="auto-style5">
                                <tr>
                                    <td class="auto-style6">
                                        <asp:Panel ID="Panel21" runat="server" HorizontalAlign="Center">
                                            <ig:WebDropDown ID="ddl_Origen" runat="server" Width="600px">
                                            </ig:WebDropDown>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>

                        </div>
                    </asp:WizardStep>
                    <asp:WizardStep runat="server" title="PARAMETROS">
                        <div>
                            <table style="width:100%;">
                                <tr>
                                    <td class="auto-style2">
                                        &nbsp;</td>
                                    <td class="auto-style2">&nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Panel ID="Panel24" runat="server">
                                            <asp:Label ID="lblOrigen" runat="server" CssClass="col-md-2 control-label" Font-Bold="True" Font-Size="Small" Text="Origen"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style2">&nbsp;</td>
                                    <td class="auto-style2">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style2">&nbsp;</td>
                                    <td class="auto-style2">
                                        <asp:Panel ID="Panel9" runat="server">
                                            <asp:Label ID="Label7" runat="server" CssClass="col-md-2 control-label" Text="Turno:"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel15" runat="server">
                                            <ig:WebDropDown ID="ddl_Turno" runat="server" Width="200px">
                                            </ig:WebDropDown>
                                        </asp:Panel>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style2">
                                        &nbsp;</td>
                                    <td class="auto-style2">
                                        <asp:Panel ID="Panel10" runat="server">
                                            <asp:Label ID="Label8" runat="server" CssClass="col-md-2 control-label" Text="Hora Inicio:"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel16" runat="server">
                                            <ig:WebTextEditor ID="wtxt_horainicio" runat="server" TextMode="Time">
                                            </ig:WebTextEditor>
                                        </asp:Panel>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style2">
                                        &nbsp;</td>
                                    <td class="auto-style2">
                                        <asp:Panel ID="Panel11" runat="server">
                                            <asp:Label ID="Label9" runat="server" CssClass="col-md-2 control-label" Text="Hora Fin:"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel17" runat="server">
                                            <ig:WebTextEditor ID="wtxt_horafin" runat="server" TextMode="Time">
                                            </ig:WebTextEditor>
                                        </asp:Panel>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style2">&nbsp;</td>
                                    <td class="auto-style2">
                                        <asp:Panel ID="Panel12" runat="server">
                                            <asp:Label ID="Label10" runat="server" CssClass="col-md-2 control-label" Text="Jornales:"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel20" runat="server">
                                            <ig:WebTextEditor ID="wtxt_jornales" runat="server" TextMode="Number">
                                            </ig:WebTextEditor>
                                        </asp:Panel>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style2">&nbsp;</td>
                                    <td class="auto-style2">
                                        <asp:Panel ID="Panel13" runat="server">
                                            <asp:Label ID="Label11" runat="server" CssClass="col-md-2 control-label" Text="Responsable:"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel18" runat="server">
                                            <ig:WebDropDown ID="ddl_responsable" runat="server" Width="200px">
                                            </ig:WebDropDown>
                                        </asp:Panel>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style2">&nbsp;</td>
                                    <td class="auto-style2">
                                        <asp:Panel ID="Panel14" runat="server">
                                            <asp:Label ID="Label12" runat="server" CssClass="col-md-2 control-label" Text="Observaciones:"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel19" runat="server">
                                            <ig:WebTextEditor ID="wtxt_observ" runat="server" Height="65px" MaxLength="255" TextMode="MultiLine" Width="300px">
                                            </ig:WebTextEditor>
                                        </asp:Panel>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </div>
                    </asp:WizardStep>
                </WizardSteps>
            </asp:Wizard>            
            
        </asp:Panel>
        <asp:Panel ID="pnlListar" runat="server">
    
            <ig:WebScriptManager ID="WebScriptManager1" runat="server">
        </ig:WebScriptManager>
            <div>
                    <ig:WebDataGrid ID="WebDataGrid1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Height="750px" Width="1025px" DataKeyFields="OrdenID">
                        <Columns>
                            <ig:BoundDataField DataFieldName="ProcesoID" Key="ProcesoID" Hidden="True">
                                <Header Text="ProcesoID">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="ProcesoNombre" Key="ProcesoNombre">
                                <Header Text="ProcesoNombre">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="OrdenID" Key="OrdenID" Width="80px">
                                <Header Text="OrdenID">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="OrdenFecha" Key="OrdenFecha">
                                <Header Text="OrdenFecha">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="LoteOrigen" Key="LoteOrigen">
                                <Header Text="LoteOrigen">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="LoteOrigenInfo" Key="LoteOrigenInfo">
                                <Header Text="LoteOrigenInfo">
                                </Header>
                            </ig:BoundDataField>
                        </Columns>
                        <EditorProviders>
                            <ig:DatePickerProvider ID="WebDataGrid1_DatePickerProvider1">
                                <EditorControl ClientIDMode="Predictable">
                                </EditorControl>
                            </ig:DatePickerProvider>
                            <ig:DropDownProvider ID="WebDataGrid1_DropDownProvider1">
                                <EditorControl ClientIDMode="Predictable" DataSourceID="SqlDataSource1" DropDownContainerMaxHeight="200px" EnableAnimations="False" EnableDropDownAsChild="False" TextField="ProcesoNombre" ValueField="ProcesoID">
                                    <DropDownItemBinding TextField="ProcesoNombre" ValueField="ProcesoID" />
                                </EditorControl>
                            </ig:DropDownProvider>
                        </EditorProviders>
                        <Behaviors>
                            <ig:RowSelectors>
                            </ig:RowSelectors>
                            <ig:Activation>
                            </ig:Activation>
                            <ig:Filtering>
                            </ig:Filtering>
                            <ig:Selection RowSelectType="Single">
                            </ig:Selection>
                        </Behaviors>
                    </ig:WebDataGrid>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:AgroGemPrdConnectionString %>" SelectCommand="SELECT O.ProcesoID, ProcesoNombre, OrdenID, OrdenFecha, OrdenFechaRegistro, dbo.Get_EntradaParametro(OrdenID,18) LoteOrigen,
dbo.BuildingInfo_LoteOrigen(dbo.Get_EntradaParametro(OrdenID,18)+'-') LoteOrigenInfo
FROM ORDENP O LEFT JOIN PROCESOS P ON P.ProcesoID = O.ProcesoID
WHERE O.ProcesoID = @ProcesoID">
                        <SelectParameters>
                            <asp:QueryStringParameter DefaultValue="4" Name="ProcesoID" QueryStringField="pid" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
    
        </asp:Panel>
    </form>
    </div>
</body>
</html>
