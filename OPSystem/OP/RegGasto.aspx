<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RegGasto.aspx.vb" Inherits="OPSystem.RegGasto" %>

<%@ Register Assembly="Infragistics45.Web.v17.1, Version=17.1.20171.2024, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI" TagPrefix="ig" %>

<%@ Register src="../ControlUser/BarEventos.ascx" tagname="BarEventos" tagprefix="uc1" %>

<%@ Register assembly="Infragistics45.Web.v17.1, Version=17.1.20171.2024, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.GridControls" tagprefix="ig" %>

<%@ Register assembly="Infragistics45.Web.v17.1, Version=17.1.20171.2024, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v17.1, Version=17.1.20171.2024, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">



.igdd_ControlArea
{
	background-color: none;
	border:solid 1px #C9C9C9;
	table-layout: fixed;
}


.igdd_ControlArea
{
	background-color: none;
	border:solid 1px #C9C9C9;
	table-layout: fixed;
}


.igdd_ValueDisplay
{
	background-color:Transparent;
	font-weight:normal;
	font-size:10pt;
	font-family: Segoe UI,Verdana,Helvetica,sans-serif;
	border-width:0px;
	width: 100%;
	z-index: 0;
}


.igdd_ValueDisplay
{
	background-color:Transparent;
	font-weight:normal;
	font-size:10pt;
	font-family: Segoe UI,Verdana,Helvetica,sans-serif;
	border-width:0px;
	width: 100%;
	z-index: 0;
}


.igdd_DropDownButton
{
	width: 22px;
	height: 22px;
	z-index: 9999;
}


.igdd_DropDownButton
{
	width: 22px;
	height: 22px;
	z-index: 9999;
}


.igdd_DropDownListContainer
{
	background-color:White;
	border:solid 1px #868686;
	float: left;
}


.igdd_DropDownListContainer
{
	background-color:White;
	border:solid 1px #868686;
	float: left;
}


.igdd_DropDownList
{
	background-color:White;
	font-size:10pt;
	font-family: Segoe UI,Verdana,Helvetica,sans-serif;
	margin:0px;
	padding:1px;
}


.igdd_DropDownList
{
	background-color:White;
	font-size:10pt;
	font-family: Segoe UI,Verdana,Helvetica,sans-serif;
	margin:0px;
	padding:1px;
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
                <table style="width:100%;">
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            <asp:Panel ID="Panel7" runat="server">
                                <asp:Label ID="lblAcción" runat="server" CssClass="col-md-2 control-label" Font-Bold="True" Font-Size="Large" Text="Evento"></asp:Label>
                            </asp:Panel>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Panel ID="Panel1" runat="server">
                                <asp:Label ID="Label1" runat="server" Text="Tipo de Envases"></asp:Label>
                            </asp:Panel>
                        </td>
                        <td>
                            <asp:Panel ID="Panel2" runat="server">
                                <ig:WebDropDown ID="wddl_GastoProductoID" runat="server" Width="400px">
                                </ig:WebDropDown>
                            </asp:Panel>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Panel ID="Panel3" runat="server">
                                <asp:Label ID="Label2" runat="server" Text="Cantidad"></asp:Label>
                            </asp:Panel>
                        </td>
                        <td>
                            <asp:Panel ID="Panel4" runat="server">
                                <ig:WebNumericEditor ID="wne_OrdenGastoExT" runat="server">
                                </ig:WebNumericEditor>
                            </asp:Panel>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Panel ID="Panel5" runat="server">
                                <asp:Label ID="Label3" runat="server" Text="Peso Tara"></asp:Label>
                            </asp:Panel>
                        </td>
                        <td>
                            <asp:Panel ID="Panel6" runat="server">
                                <ig:WebNumericEditor ID="wne_OrdenGastoPesoTara" runat="server">
                                </ig:WebNumericEditor>
                            </asp:Panel>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Panel ID="pnlAceptar" runat="server">
                                <asp:ImageButton ID="imgbtnAceptar" runat="server" Height="30px" ImageUrl="~/Img/Acept-form.png" Width="30px" />
                                <asp:ImageButton ID="imgbtnCancelar" runat="server" Height="30px" ImageUrl="~/Img/Cancel-form.png" Width="30px" />
                            </asp:Panel>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlListar" runat="server" Width="1088px">
                <ig:WebScriptManager ID="WebScriptManager1" runat="server"></ig:WebScriptManager>
                <asp:Label ID="lblInfo" runat="server" Text="Label" Visible="False"></asp:Label>
                <ig:WebDataGrid ID="WebDataGrid1" runat="server" Height="350px" Width="1033px" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" DataKeyFields="OrdenGastoID" >                
                    <Columns>
                        <ig:BoundDataField DataFieldName="OrdenID" Hidden="True" Key="OrdenID">
                            <Header Text="OrdenID">
                            </Header>
                        </ig:BoundDataField>
                        <ig:BoundDataField DataFieldName="OrdenGastoID" Key="OrdenGastoID">
                            <Header Text="No">
                            </Header>
                        </ig:BoundDataField>
                        <ig:BoundDataField DataFieldName="OrdenGastoExT" Key="OrdenGastoExT">
                            <Header Text="ExT">
                            </Header>
                        </ig:BoundDataField>
                        <ig:BoundDataField DataFieldName="GastoEmpresaID" Hidden="True" Key="GastoEmpresaID">
                            <Header Text="GastoEmpresaID">
                            </Header>
                        </ig:BoundDataField>
                        <ig:BoundDataField DataFieldName="GastoUbicacionID" Hidden="True" Key="GastoUbicacionID">
                            <Header Text="GastoUbicacionID">
                            </Header>
                        </ig:BoundDataField>
                        <ig:BoundDataField DataFieldName="GastoProductoID" Key="GastoProductoID">
                            <Header Text="Envase">
                            </Header>
                        </ig:BoundDataField>
                        <ig:BoundDataField DataFieldName="ProductoNombre" Hidden="True" Key="ProductoNombre">
                            <Header Text="ProductoNombre">
                            </Header>
                        </ig:BoundDataField>
                        <ig:BoundDataField DataFieldName="OrdenGastoPesoBruto" Key="OrdenGastoPesoBruto">
                            <Header Text="PesoBruto">
                            </Header>
                        </ig:BoundDataField>
                        <ig:BoundDataField DataFieldName="OrdenGastoPesoTara" Key="OrdenGastoPesoTara">
                            <Header Text="PesoTara">
                            </Header>
                        </ig:BoundDataField>
                        <ig:BoundDataField DataFieldName="OrdenGastoPesoNeto" Key="OrdenGastoPesoNeto">
                            <Header Text="PesoNeto">
                            </Header>
                        </ig:BoundDataField>
                        <ig:BoundDataField DataFieldName="OrdenGastoPesoAcumulado" Key="OrdenGastoPesoAcumulado">
                            <Header Text="PesoAcumulado">
                            </Header>
                        </ig:BoundDataField>
                        <ig:BoundDataField DataFieldName="GastoProductoLote" Key="GastoProductoLote">
                            <Header Text="ProductoLote">
                            </Header>
                        </ig:BoundDataField>
                        <ig:BoundDataField DataFieldName="OrdenGastoInfo1" Key="OrdenGastoInfo1">
                            <Header Text="Observ1">
                            </Header>
                        </ig:BoundDataField>
                    </Columns>
                    <Behaviors>
                        <ig:Activation>
                        </ig:Activation>
                        <ig:EditingCore>
                            <Behaviors>
                                <ig:CellEditing>
                                    <ColumnSettings>
                                        <ig:EditingColumnSetting ColumnKey="OrdenGastoID" ReadOnly="True" />
                                        <ig:EditingColumnSetting ColumnKey="OrdenGastoPesoNeto" ReadOnly="True" />
                                        <ig:EditingColumnSetting ColumnKey="OrdenGastoPesoAcumulado" ReadOnly="True" />
                                        <ig:EditingColumnSetting ColumnKey="GastoProductoLote" ReadOnly="True" />
                                    </ColumnSettings>
                                </ig:CellEditing>
                            </Behaviors>
                        </ig:EditingCore>
                    </Behaviors>
                </ig:WebDataGrid>
            </asp:Panel>
        </form>
    </div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:AgroGemPrdConnectionString3 %>" DeleteCommand="DELETE FROM [ORDENPGASTO] WHERE [OrdenID] = @OrdenID AND [OrdenGastoID] = @OrdenGastoID" InsertCommand="INSERT INTO [ORDENPGASTO] ([OrdenID], [OrdenGastoID], [OrdenGastoExT], [OrdenGastoPesoBruto], [OrdenGastoPesoTara], [OrdenGastoRegistro], [GastoProductoID], [GastoProductoLote], [GastoEmpresaID], [GastoUbicacionID], [OrdenGastoInfo3], [OrdenGastoInfo2], [OrdenGastoInfo1]) VALUES (@OrdenID, @OrdenGastoID, @OrdenGastoExT, @OrdenGastoPesoBruto, @OrdenGastoPesoTara, @OrdenGastoRegistro, @GastoProductoID, @GastoProductoLote, @GastoEmpresaID, @GastoUbicacionID, @OrdenGastoInfo3, @OrdenGastoInfo2, @OrdenGastoInfo1)" SelectCommand="SELECT OrdenID, OrdenGastoID, OrdenGastoExT, GastoEmpresaID, GastoUbicacionID, GastoProductoID, ProductoNombre, OrdenGastoPesoBruto, OrdenGastoPesoTara,
       GastoProductoLote, OrdenGastoInfo1, OrdenGastoPesoNeto, OrdenGastoPesoAcumulado
  FROM ORDENPGASTO OG LEFT JOIN PRODUCTOS P ON P.EmpresaID = OG.GastoEmpresaID
                                           AND P.ProductoID = OG.GastoProductoID
  WHERE OrdenID = @OrdenID" UpdateCommand="UPDATE [ORDENPGASTO] SET [OrdenGastoExT] = @OrdenGastoExT, [OrdenGastoPesoBruto] = @OrdenGastoPesoBruto, [OrdenGastoPesoTara] = @OrdenGastoPesoTara, [OrdenGastoRegistro] = @OrdenGastoRegistro, [GastoProductoID] = @GastoProductoID, [GastoProductoLote] = @GastoProductoLote, [GastoEmpresaID] = @GastoEmpresaID, [GastoUbicacionID] = @GastoUbicacionID, [OrdenGastoInfo3] = @OrdenGastoInfo3, [OrdenGastoInfo2] = @OrdenGastoInfo2, [OrdenGastoInfo1] = @OrdenGastoInfo1 WHERE [OrdenID] = @OrdenID AND [OrdenGastoID] = @OrdenGastoID">
        <DeleteParameters>
            <asp:Parameter Name="OrdenID" Type="Int32" />
            <asp:Parameter Name="OrdenGastoID" Type="Int16" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="OrdenID" Type="Int32" />
            <asp:Parameter Name="OrdenGastoID" Type="Int16" />
            <asp:Parameter Name="OrdenGastoExT" Type="Int16" />
            <asp:Parameter Name="OrdenGastoPesoBruto" Type="Decimal" />
            <asp:Parameter Name="OrdenGastoPesoTara" Type="Decimal" />
            <asp:Parameter Name="OrdenGastoRegistro" Type="DateTime" />
            <asp:Parameter Name="GastoProductoID" Type="String" />
            <asp:Parameter Name="GastoProductoLote" Type="String" />
            <asp:Parameter Name="GastoEmpresaID" Type="Int16" />
            <asp:Parameter Name="GastoUbicacionID" Type="String" />
            <asp:Parameter Name="OrdenGastoInfo3" Type="String" />
            <asp:Parameter Name="OrdenGastoInfo2" Type="String" />
            <asp:Parameter Name="OrdenGastoInfo1" Type="String" />
        </InsertParameters>
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="7" Name="OrdenID" QueryStringField="key" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="OrdenGastoExT" Type="Int16" />
            <asp:Parameter Name="OrdenGastoPesoBruto" Type="Decimal" />
            <asp:Parameter Name="OrdenGastoPesoTara" Type="Decimal" />
            <asp:Parameter Name="OrdenGastoRegistro" Type="DateTime" />
            <asp:Parameter Name="GastoProductoID" Type="String" />
            <asp:Parameter Name="GastoProductoLote" Type="String" />
            <asp:Parameter Name="GastoEmpresaID" Type="Int16" />
            <asp:Parameter Name="GastoUbicacionID" Type="String" />
            <asp:Parameter Name="OrdenGastoInfo3" Type="String" />
            <asp:Parameter Name="OrdenGastoInfo2" Type="String" />
            <asp:Parameter Name="OrdenGastoInfo1" Type="String" />
            <asp:Parameter Name="OrdenID" Type="Int32" />
            <asp:Parameter Name="OrdenGastoID" Type="Int16" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:AgroGemPrdConnectionString3 %>" SelectCommand="SELECT * FROM [__MigrationHistory]"></asp:SqlDataSource>
</body>
</html>
