<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="MovAlmacenDetalle.aspx.vb" Inherits="OPSystem.MovAlmacenDetalle" %>
<%@ Register src="../ControlUser/BarEventos.ascx" tagname="BarEventos" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../CSS/jquery-ui.css"  rel="stylesheet" type="text/css" />
    <script src="../JS/jquery-1.9.1.js"></script>
    <script src="../JS/jquery-ui.js"></script>
    <script src="../UI/i18n/datepicker-es.js"></script>
    <script>
        $(function () {
            $("#MainContent_txt_MovAlmacenFecha").datepicker($.datepicker.regional["es"]);
        });
        $(function () {
            $("#MainContent_Search_txt_MovAlmacenFecha").datepicker($.datepicker.regional["es"]);
        });
    </script>
    <asp:Panel ID="pnlEventos" runat="server">
        <uc1:BarEventos ID="BarEventos1" runat="server" />
    </asp:Panel>
    <asp:Panel ID="pnlFiltros" runat="server"></asp:Panel>
    <asp:Panel ID="pnlTitulo" runat="server">
        <asp:Label ID="lblTitulo" runat="server" Text="Label" CssClass="form-control" Font-Bold="True" ForeColor="#888888"></asp:Label>
    </asp:Panel>
    <asp:Panel ID="pnlAdd" runat="server">
        <table style="width:100%;">
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Panel ID="Panel1" runat="server">
                            <table style="width:100%;">
                                <tr>
                                    <td style="width: 150px; height: 22px;">
                                        </td>
                                    <td style="height: 22px">
                                        <asp:Panel ID="Panel2" runat="server">
                                            <asp:Label ID="lblAcción" runat="server" Font-Bold="True" ForeColor="#5D7B9D"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 22px">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel3" runat="server">
                                            <asp:Label ID="Label2" runat="server" CssClass="col-md-2 control-label" Text="Producto"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel4" runat="server">
                                            <asp:DropDownList ID="ddl_ProductoID" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel49" runat="server">
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddl_ProductoID" CssClass="alert-warning" ErrorMessage="CompareValidator" Operator="NotEqual" ValueToCompare="%"></asp:CompareValidator>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px; height: 22px;">
                                        <asp:Panel ID="Panel14" runat="server">
                                            <asp:Label ID="Label4" runat="server" CssClass="col-md-2 control-label" Text="Lote"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 22px">
                                        <asp:Panel ID="Panel15" runat="server">
                                            <asp:TextBox ID="txt_MovAlmacenLote" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 22px">
                                        <asp:Panel ID="Panel48" runat="server">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_MovAlmacenLote" CssClass="alert-warning" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px; height: 22px;">
                                        <asp:Panel ID="Panel50" runat="server">
                                            <asp:Label ID="Label11" runat="server" CssClass="col-md-2 control-label" Text="Especificación"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 22px">
                                        <asp:Panel ID="Panel51" runat="server">
                                            <asp:TextBox ID="txt_MovAlmacenEsp" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 22px">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel16" runat="server">
                                            <asp:Label ID="Label5" runat="server" CssClass="col-md-2 control-label" Text="Cantidad"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel18" runat="server">
                                            <asp:TextBox ID="txt_MovAlmacenCantidad" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel47" runat="server">
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel23" runat="server">
                                            <asp:Label ID="Label10" runat="server" CssClass="col-md-2 control-label" Text="Peso Kg"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel27" runat="server">
                                            <asp:TextBox ID="txt_MovAlmacenPeso" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">&nbsp;</td>
                                    <td>
                                        <asp:Panel ID="Panel5" runat="server">
                                            <asp:ImageButton ID="ImgBtnAceptar" runat="server" ImageUrl="~/Img/Acept-form.png" Width="30px" />
                                            <asp:ImageButton ID="ImgBtnCancelar" runat="server" CausesValidation="False" ImageUrl="~/Img/Cancel-form.png" Width="30px" />
                                        </asp:Panel>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlListar" runat="server">
        <table style="width:100%;">
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="ProductoID,MovAlmacenLote" Font-Size="XX-Small" ForeColor="#333333" GridLines="None" ShowFooter="True" AllowPaging="True" CssClass="table table-condensed">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="ProductoID" HeaderText="ProductoID" />
                                <asp:BoundField DataField="MovAlmacenLote" HeaderText="Lote" />
                                <asp:BoundField DataField="ProductoNombre" HeaderText="ProductoNombre" />
                                <asp:BoundField DataField="UnidadEmpaqueNombre" HeaderText="UnidadEmpaque" />
                                <asp:BoundField DataField="MovAlmacenCantidad" HeaderText="Cantidad" />
                                <asp:BoundField DataField="MovAlmacenPeso" HeaderText="Peso" />
                                <asp:CommandField InsertVisible="False" ShowSelectButton="True" />
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
        </table>
    </asp:Panel>
</asp:Content>
