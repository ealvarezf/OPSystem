<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RegProduccion.aspx.vb" Inherits="OPSystem.RegProduccion" %>

<%@ Register src="../ControlUser/BarEventos.ascx" tagname="BarEventos" tagprefix="uc1" %>
<%@ Register assembly="Infragistics45.Web.v17.1, Version=17.1.20171.2024, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.GridControls" tagprefix="ig" %>

<%@ Register assembly="Infragistics45.Web.v17.1, Version=17.1.20171.2024, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI" tagprefix="ig" %>

<%@ Register assembly="Infragistics45.Web.v17.1, Version=17.1.20171.2024, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 399px;
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
                <ig:WebScriptManager ID="WebScriptManager1" runat="server">
                </ig:WebScriptManager>
                <asp:Label ID="lblInfo" runat="server" Text="Label" Visible="False"></asp:Label>
            </asp:Panel>
            <asp:Panel ID="pnlListar" runat="server" Width="1088px">
                <ig:WebDataGrid ID="wdgProduccion" runat="server" Height="350px" Width="1086px" AutoGenerateColumns="False" DataKeyFields="OrdenProduccionID" DataSourceID="SqlDataSource1" >
                    <Columns>
                        <ig:BoundDataField DataFieldName="OrdenID" Key="OrdenID" Hidden="True">
                            <Header Text="OrdenID">
                            </Header>
                        </ig:BoundDataField>
                        <ig:BoundDataField DataFieldName="OrdenProduccionID" Key="OrdenProduccionID">
                            <Header Text="OrdenProduccionID">
                            </Header>
                        </ig:BoundDataField>
                        <ig:BoundDataField DataFieldName="ProduccionEmpresaID" Key="ProduccionEmpresaID" Hidden="True">
                            <Header Text="ProduccionEmpresaID">
                            </Header>
                        </ig:BoundDataField>
                        <ig:BoundDataField DataFieldName="ProduccionUbicacionID" Key="ProduccionUbicacionID" Hidden="True">
                            <Header Text="ProduccionUbicacionID">
                            </Header>
                        </ig:BoundDataField>
                        <ig:BoundDataField DataFieldName="ProduccionProductoID" Key="ProduccionProductoID">
                            <Header Text="ProduccionProductoID">
                            </Header>
                        </ig:BoundDataField>
                        <ig:BoundDataField DataFieldName="ProductoNombre" Key="ProductoNombre">
                            <Header Text="ProductoNombre">
                            </Header>
                        </ig:BoundDataField>
                        <ig:BoundDataField DataFieldName="ProductoCantidad" Key="ProductoCantidad">
                            <Header Text="ProductoCantidad">
                            </Header>
                        </ig:BoundDataField>
                        <ig:BoundDataField DataFieldName="ProductoPeso" Key="ProductoPeso">
                            <Header Text="ProductoPeso">
                            </Header>
                        </ig:BoundDataField>
                        <ig:BoundDataField DataFieldName="ProduccionProductoLote" Key="ProduccionProductoLote">
                            <Header Text="ProduccionProductoLote">
                            </Header>
                        </ig:BoundDataField>
                        <ig:BoundDataField DataFieldName="ProduccionRegistroID" DataFormatString="{0:G}" Key="ProduccionRegistroID">
                            <Header Text="ProduccionRegistroID">
                            </Header>
                        </ig:BoundDataField>
                    </Columns>
                    <EditorProviders>
                        <ig:DropDownProvider ID="wdgProduccion_DropDownProvider1">
                            <EditorControl ClientIDMode="Predictable" DataSourceID="SqlDataSource2" DropDownContainerMaxHeight="200px" EnableAnimations="False" EnableDropDownAsChild="False" TextField="ProductoNombre" ValueField="ProductoID">
                                <DropDownItemBinding TextField="ProductoNombre" ValueField="ProductoID" />
                            </EditorControl>
                        </ig:DropDownProvider>
                    </EditorProviders>
                    <Behaviors>
                        <ig:EditingCore>
                            <Behaviors>
                                <ig:CellEditing>
                                    <ColumnSettings>
                                        <ig:EditingColumnSetting ColumnKey="OrdenProduccionID" ReadOnly="True" />
                                        <ig:EditingColumnSetting ColumnKey="ProduccionProductoLote" ReadOnly="True" />
                                        <ig:EditingColumnSetting ColumnKey="ProduccionRegistroID" ReadOnly="True" />
                                        <ig:EditingColumnSetting ColumnKey="ProduccionProductoID" EditorID="wdgProduccion_DropDownProvider1" />
                                    </ColumnSettings>
                                </ig:CellEditing>
                                <ig:RowAdding>
                                </ig:RowAdding>
                            </Behaviors>
                        </ig:EditingCore>
                        <ig:Activation>
                        </ig:Activation>
                    </Behaviors>
                </ig:WebDataGrid>
            </asp:Panel>            
        </form>
    </div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:AgroGemPrdConnectionString2 %>" DeleteCommand="DELETE FROM [ORDENPPRODUCCION] WHERE [OrdenID] = @OrdenID AND [OrdenProduccionID] = @OrdenProduccionID" InsertCommand="INSERT INTO [ORDENPPRODUCCION] ([OrdenID], [OrdenProduccionID], [ProductoCantidad], [ProductoPeso], [ProduccionEmpresaID], [ProduccionUbicacionID], [ProduccionProductoID], [ProduccionProductoLote], [ProduccionRegistroID]) VALUES (@OrdenID, @OrdenProduccionID, @ProductoCantidad, @ProductoPeso, @ProduccionEmpresaID, @ProduccionUbicacionID, @ProduccionProductoID, @ProduccionProductoLote, @ProduccionRegistroID)" SelectCommand="SELECT OrdenID, OrdenProduccionID, ProduccionEmpresaID, ProduccionUbicacionID, ProduccionProductoID, ProductoNombre, ProductoCantidad, ProductoPeso,
       ProduccionProductoLote, ProduccionRegistroID
  FROM ORDENPPRODUCCION OP LEFT JOIN PRODUCTOS P ON P.EmpresaID = OP.ProduccionEmpresaID
                           AND P.ProductoID = OP.ProduccionProductoID
  WHERE OrdenID = @OrdenID" UpdateCommand="UPDATE [ORDENPPRODUCCION] SET [ProductoCantidad] = @ProductoCantidad, [ProductoPeso] = @ProductoPeso, [ProduccionEmpresaID] = @ProduccionEmpresaID, [ProduccionUbicacionID] = @ProduccionUbicacionID, [ProduccionProductoID] = @ProduccionProductoID, [ProduccionProductoLote] = @ProduccionProductoLote, [ProduccionRegistroID] = @ProduccionRegistroID WHERE [OrdenID] = @OrdenID AND [OrdenProduccionID] = @OrdenProduccionID">
        <DeleteParameters>
            <asp:Parameter Name="OrdenID" Type="Int32" />
            <asp:Parameter Name="OrdenProduccionID" Type="Int16" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="OrdenID" Type="Int32" />
            <asp:Parameter Name="OrdenProduccionID" Type="Int16" />
            <asp:Parameter Name="ProductoCantidad" Type="Decimal" />
            <asp:Parameter Name="ProductoPeso" Type="Decimal" />
            <asp:Parameter Name="ProduccionEmpresaID" Type="Int16" />
            <asp:Parameter Name="ProduccionUbicacionID" Type="String" />
            <asp:Parameter Name="ProduccionProductoID" Type="String" />
            <asp:Parameter Name="ProduccionProductoLote" Type="String" />
            <asp:Parameter Name="ProduccionRegistroID" Type="DateTime" />
        </InsertParameters>
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="7" Name="OrdenID" QueryStringField="key" Type="Int32" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="ProductoCantidad" Type="Decimal" />
            <asp:Parameter Name="ProductoPeso" Type="Decimal" />
            <asp:Parameter Name="ProduccionEmpresaID" Type="Int16" />
            <asp:Parameter Name="ProduccionUbicacionID" Type="String" />
            <asp:Parameter Name="ProduccionProductoID" Type="String" />
            <asp:Parameter Name="ProduccionProductoLote" Type="String" />
            <asp:Parameter Name="ProduccionRegistroID" Type="DateTime" />
            <asp:Parameter Name="OrdenID" Type="Int32" />
            <asp:Parameter Name="OrdenProduccionID" Type="Int16" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:AgroGemPrdConnectionString %>" SelectCommand="SELECT [ProductoID], [ProductoNombre] FROM [PRODUCTOS] WHERE ([EmpresaID] = @EmpresaID)">
        <SelectParameters>
            <asp:Parameter DefaultValue="15" Name="EmpresaID" Type="Int16" />
        </SelectParameters>
    </asp:SqlDataSource>
</body>
</html>
