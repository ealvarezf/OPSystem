<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="BarEventosOld.ascx.vb" Inherits="OPSystem.BarEventosOld" %>
<asp:Panel ID="pnlEventos" runat="server">
    <asp:ImageButton ID="btnNuevo" runat="server" ImageUrl="~/Img/ActionInsert.png" Width="30px"  />
    <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/Img/Eliminar.jpg" Width="30px" />
    <asp:ImageButton ID="btnEditar" runat="server" ImageUrl="~/Img/editar.png" Width="30px" />
    <asp:ImageButton ID="btnExportar" runat="server" ImageUrl="~/Img/Exportar.png" Width="30px"  />
    <asp:ImageButton ID="btnFiltrar" runat="server" ImageUrl="~/Img/filter.png" Width="30px" />
    <asp:ImageButton ID="btnListar" runat="server" ImageUrl="~/Img/Impresora.png" Width="30px" />
    <asp:ImageButton ID="btnEsp1" runat="server" ImageUrl="~/Img/Cancelar.jpg" Width="30px" />
    <asp:ImageButton ID="btnEsp2" runat="server" ImageUrl="~/Img/icon-fletes.png" Width="30px" />
</asp:Panel>