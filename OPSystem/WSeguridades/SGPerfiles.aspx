<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="SGPerfiles.aspx.vb" Inherits="OPSystem.SGPerfiles" %>
<%@ Register src="../ControlUser/BarEventos.ascx" tagname="BarEventos" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../CSS/jquery-ui.css"  rel="stylesheet" type="text/css" />
    <script src="../JS/jquery-1.9.1.js"></script>
    <script src="../JS/jquery-ui.js"></script>
    <script src="../UI/i18n/datepicker-es.js"></script>

    <div class="panel-body">
        <asp:Panel ID="pnlEventos" runat="server">
            <uc1:BarEventos ID="BarEventos1" runat="server" />
        </asp:Panel>
        <asp:Panel ID="pnlFiltros" runat="server">
            <table style="width:100%;">
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Panel ID="Panel6" runat="server">
                            <table style="width:100%;">
                                <tr>
                                    <td style="width: 150px; height: 22px;">
                                        </td>
                                    <td style="height: 22px">
                                        <asp:Panel ID="pnlAcción0" runat="server">
                                            <asp:Label ID="lblFiltro" runat="server" Font-Bold="True" ForeColor="#5D7B9D">Filtros</asp:Label>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel7" runat="server">
                                            <asp:Label ID="Label3" runat="server" CssClass="col-md-2 control-label" Text="Perfil"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel8" runat="server">
                                            <asp:TextBox ID="txtSearch_PerfilNombre" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">&nbsp;</td>
                                    <td>
                                        <asp:Panel ID="Panel13" runat="server">
                                            <asp:ImageButton ID="imgBtnAplicaFiltro" runat="server" ImageUrl="~/Img/Acept-form.png" Width="30px" />
                                            <asp:ImageButton ID="imgbtnCancelaFiltro" runat="server" CausesValidation="False" ImageUrl="~/Img/Cancel-form.png" Width="30px" />
                                        </asp:Panel>
                                    </td>
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
                        <asp:Panel ID="pnlformulario" runat="server">
                            <table style="width:100%;">
                                <tr>
                                    <td style="width: 150px">
                                        &nbsp;</td>
                                    <td>
                                        <asp:Panel ID="pnlAcción" runat="server">
                                            <asp:Label ID="lblAcción"  runat="server" Font-Bold="True" ForeColor="#5D7B9D"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="PnlKey" runat="server">
                                            <asp:Label ID="lbl_ModuloID" runat="server" CssClass="col-md-2 control-label" Text="ID"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="pnlDes" runat="server">
                                            <asp:TextBox ID="txt_PerfilID" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel1" runat="server">
                                            <asp:Label ID="Label1" runat="server" CssClass="col-md-2 control-label" Text="Perfil"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel2" runat="server">
                                            <asp:TextBox ID="txt_PerfilNombre" runat="server" Width="300px" CssClass="form-control"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel14" runat="server">
                                            <asp:Label ID="Label4" runat="server" CssClass="col-md-2 control-label" Text="Estructura"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel15" runat="server">
                                            <asp:DropDownList ID="DDL_EstructuraMenuID" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">&nbsp;</td>
                                    <td>
                                        <asp:Panel ID="Panel5" runat="server">
                                            <asp:ImageButton ID="ImgBtnAceptar" runat="server" ImageUrl="~/Img/Acept-form.png" Width="30px" />
                                            <asp:ImageButton ID="ImgBtnCancelar" runat="server" CausesValidation="False" ImageUrl="~/Img/Cancel-form.png" Width="30px" />
                                        </asp:Panel>
                                    </td>
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
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="PerfilID" Font-Size="XX-Small" ForeColor="#333333" GridLines="None" ShowFooter="True" AllowPaging="True" CssClass="table table-condensed">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="PerfilID" HeaderText="ID" />
                                <asp:BoundField DataField="PerfilNombre" HeaderText="Perfil" />
                                <asp:BoundField DataField="EstructuraMenuID" HeaderText="EstructuraID" />
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
    </div>
</asp:Content>
