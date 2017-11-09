<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="OrdenesProduccion.aspx.vb" Inherits="OPSystem.OrdenesProduccion" %>
<%@ Register assembly="Infragistics45.Web.v17.1, Version=17.1.20171.1001, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.NavigationControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel-body">
        <table style="width:100%;">
    <tr>
        <td style="width: 259px">
            <ig:WebExplorerBar ID="WebExplorerBar1" runat="server" Width="250px" GroupContentsHeight="" Height="842px">
                <Groups>
                    <ig:ExplorerBarGroup GroupContentsHeight="" Text="Ordenes Producción">
                        <Items>
                            <ig:ExplorerBarItem ImageUrl="~/Img/MD_PVCATB.ico" Text="Preselección" NavigateUrl="~/OP/ListOrdenes.aspx?pid=3" Target="iframe1">
                            </ig:ExplorerBarItem>
                            <ig:ExplorerBarItem Text="Desgrane" ImageUrl="~/Img/EV_ESPECIAL4.ico" NavigateUrl="~/OP/ListOrdenes.aspx?pid=4" Target="iframe1">
                            </ig:ExplorerBarItem>
                            <ig:ExplorerBarItem Text="Tratamiento" ImageUrl="~/Img/SG_MODULOS.ico" NavigateUrl="~/OP/ListOrdenes.aspx?pid=2" Target="iframe1">
                            </ig:ExplorerBarItem>
                        </Items>
                    </ig:ExplorerBarGroup>
                    <ig:ExplorerBarGroup GroupContentsHeight="" Text="Productos">
                        <Items>
                            <ig:ExplorerBarItem Text="Item">
                            </ig:ExplorerBarItem>
                            <ig:ExplorerBarItem Text="Item">
                            </ig:ExplorerBarItem>
                            <ig:ExplorerBarItem Text="Item">
                            </ig:ExplorerBarItem>
                        </Items>
                    </ig:ExplorerBarGroup>
                    <ig:ExplorerBarGroup GroupContentsHeight="" Text="Inventarios">
                        <Items>
                            <ig:ExplorerBarItem Text="Item">
                            </ig:ExplorerBarItem>
                            <ig:ExplorerBarItem Text="Item">
                            </ig:ExplorerBarItem>
                            <ig:ExplorerBarItem Text="Item">
                            </ig:ExplorerBarItem>
                        </Items>
                    </ig:ExplorerBarGroup>
                </Groups>
            </ig:WebExplorerBar>
        </td>
        <td><iframe name="iframe1" style="width:1600px; height:850px; margin-top:0px; border:0px;"></iframe></td>
    </tr>
    <tr>
        <td style="width: 259px">&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td style="width: 259px">&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
</table>    
    </div>
</asp:Content>
