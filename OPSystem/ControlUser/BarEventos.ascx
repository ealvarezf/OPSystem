<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="BarEventos.ascx.vb" Inherits="OPSystem.BarEventos" %>
<style type="text/css">
    .auto-style1 {
        width: 60px;
    }
    .auto-style2 {
        width: 702px;
    }
</style>
<asp:Panel ID="pnlInfomaster" runat="server" BackColor="#888888">
    <table style="width:100%;">
        <tr>
            <td class="auto-style1">
                <asp:Panel ID="pnl1" runat="server" Width="60px">
                    <asp:ImageButton ID="btnBack" runat="server" ImageUrl="~/Img/back.png" ToolTip="Regresar" Width="30px" Visible="False" />
                </asp:Panel>
            </td>
            <td class="auto-style2">
                <asp:Panel ID="pnlInfoMaestro" runat="server">
                    <asp:Label ID="lblInfo" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#333333" Text="Función Maestro"></asp:Label>
                </asp:Panel>
            </td>
            <td class="auto-style2">
                <asp:Panel ID="pnlInfoDetalle" runat="server">
                    <asp:Label ID="lblInfoDetalle" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#333333" Text="Función Detalle"></asp:Label>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="pnlEventos" runat="server">
    <asp:ImageButton ID="btnConsultar" runat="server" ImageUrl="~/Img/Refresh2.png" Width="30px" ToolTip="Refrescar" Visible="False"  />
    <asp:ImageButton ID="btnNuevo" runat="server" ImageUrl="~/Img/ActionInsert.png" Width="30px" Visible="False"  />
    <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/Img/Delete-Button-PNG-Image.png" Width="30px" Visible="False" />
    <asp:ImageButton ID="btnRecuperar" runat="server" ImageUrl="~/Img/1-512.png" Width="30px" Visible="False" />
    <asp:ImageButton ID="btnEditar" runat="server" ImageUrl="~/Img/editar.png" Width="30px" Visible="False" />
    <asp:ImageButton ID="btnExportar" runat="server" ImageUrl="~/Img/Exportar.png" Width="30px" Visible="False"  />
    <asp:ImageButton ID="btnListar" runat="server" ImageUrl="~/Img/Impresora.png" Width="30px" Visible="False" />
    <asp:ImageButton ID="btnGraficar" runat="server" ImageUrl="~/Img/graficar.jpg" Width="30px" Visible="False" />
    <asp:ImageButton ID="btnFiltrar" runat="server" ImageUrl="~/Img/filter.png" Width="30px" Visible="False" />
    <asp:ImageButton ID="btnEsp1" runat="server" ImageUrl="~/Img/Recibo.jpg" Width="30px" Visible="False" />
    <asp:ImageButton ID="btnEsp2" runat="server" ImageUrl="~/Img/EventosEsp.jpg" Width="30px" Visible="False" />
    <asp:ImageButton ID="btnEsp3" runat="server" ImageUrl="~/Img/EventosEsp.jpg" Width="30px" Visible="False" />
    <asp:ImageButton ID="btnEsp4" runat="server" ImageUrl="~/Img/EventosEsp.jpg" Width="30px" Visible="False" />
    <asp:ImageButton ID="btnEsp5" runat="server" ImageUrl="~/Img/EventosEsp.jpg" Width="30px" Visible="False" />
    <asp:ImageButton ID="btnEsp6" runat="server" ImageUrl="~/Img/EventosEsp.jpg" Width="30px" Visible="False" />
    <asp:ImageButton ID="btnEsp7" runat="server" ImageUrl="~/Img/EventosEsp.jpg" Width="30px" Visible="False" />
    <asp:ImageButton ID="btnEsp8" runat="server" ImageUrl="~/Img/EventosEsp.jpg" Width="30px" Visible="False" />
    <asp:ImageButton ID="btnEsp9" runat="server" ImageUrl="~/Img/EventosEsp.jpg" Width="30px" Visible="False" />
    <asp:ImageButton ID="btnEsp10" runat="server" ImageUrl="~/Img/EventosEsp.jpg" Width="30px" Visible="False" />
    <asp:ImageButton ID="btnAyuda" runat="server" ImageUrl="~/Img/ayuda.png" Width="30px" Visible="False" />

</asp:Panel>
