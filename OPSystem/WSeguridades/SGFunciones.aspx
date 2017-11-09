<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="SGFunciones.aspx.vb" Inherits="OPSystem.SGFunciones" %>
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
                                    <td style="width: 150px">
                                        &nbsp;</td>
                                    <td>
                                        <asp:Panel ID="pnlAcción0" runat="server">
                                            <asp:Label ID="lblFiltro" runat="server" Font-Bold="True" ForeColor="#5D7B9D">Filtros</asp:Label>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel7" runat="server">
                                            <asp:Label ID="Label3" runat="server" CssClass="col-md-2 control-label" Text="Función"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel8" runat="server">
                                            <asp:TextBox ID="txtSearch_SGFuncion" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
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
                                            <asp:TextBox ID="txt_SGFuncionID" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel1" runat="server">
                                            <asp:Label ID="Label1" runat="server" CssClass="col-md-2 control-label" Text="Función"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel2" runat="server">
                                            <asp:TextBox ID="txt_SGFuncion" runat="server" Width="300px" CssClass="form-control"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel3" runat="server">
                                            <asp:Label ID="Label2" runat="server" CssClass="col-md-2 control-label" Text="Ayuda"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel4" runat="server">
                                            <asp:TextBox ID="txt_SGFuncionAyuda" runat="server" Width="300px" CssClass="form-control"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel14" runat="server">
                                            <asp:Label ID="Label7" runat="server" CssClass="col-md-2 control-label" Text="OLEDLL"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel15" runat="server">
                                            <asp:TextBox ID="txt_SGFuncionOLEDLL" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel16" runat="server">
                                            <asp:Label ID="Label8" runat="server" CssClass="col-md-2 control-label" Text="Subsistema"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel18" runat="server">
                                            <asp:TextBox ID="txt_SGFuncionSubsistema" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel17" runat="server">
                                            <asp:Label ID="Label9" runat="server" CssClass="col-md-2 control-label" Text="Web"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel19" runat="server">
                                            <asp:TextBox ID="txt_SGFuncionWeb" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">&nbsp;</td>
                                    <td>
                                        <asp:Panel ID="Panel31" runat="server">
                                            <table style="width:100%;">
                                                <tr>
                                                    <td style="width: 325px">
                                                        <asp:Panel ID="Panel33" runat="server" CssClass="checkbox">
                                                            <asp:CheckBox ID="chk_SGFuncionPrimaria" runat="server" Text="Primaria" />
                                                        </asp:Panel>
                                                    </td>
                                                    <td style="width: 136px">
                                                        <asp:Panel ID="Panel42" runat="server" CssClass="checkbox">
                                                            <asp:CheckBox ID="chk_SGFuncionEventoEsp1" runat="server" Text="Especial1" />
                                                        </asp:Panel>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <asp:Panel ID="Panel52" runat="server">
                                                            <asp:TextBox ID="txt_SGFuncionEventoEspNombre1" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 325px">
                                                        <asp:Panel ID="Panel32" runat="server" CssClass="checkbox">
                                                            <asp:CheckBox ID="chk_SGFuncionMaestroDetalle" runat="server" Text="MaestroDetalle" />
                                                        </asp:Panel>
                                                    </td>
                                                    <td style="width: 136px">
                                                        <asp:Panel ID="Panel43" runat="server" CssClass="checkbox">
                                                            <asp:CheckBox ID="chk_SGFuncionEventoEsp2" runat="server" Text="Especial2" />
                                                        </asp:Panel>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <asp:Panel ID="Panel53" runat="server">
                                                            <asp:TextBox ID="txt_SGFuncionEventoEspNombre2" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 325px">
                                                        <asp:Panel ID="Panel34" runat="server" CssClass="checkbox">
                                                            <asp:CheckBox ID="chk_SGFuncionConsultar" runat="server" Text="Consultar" />
                                                        </asp:Panel>
                                                    </td>
                                                    <td style="width: 136px">
                                                        <asp:Panel ID="Panel44" runat="server" CssClass="checkbox">
                                                            <asp:CheckBox ID="chk_SGFuncionEventoEsp3" runat="server" Text="Especial3" />
                                                        </asp:Panel>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <asp:Panel ID="Panel54" runat="server">
                                                            <asp:TextBox ID="txt_SGFuncionEventoEspNombre3" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 325px">
                                                        <asp:Panel ID="Panel35" runat="server" CssClass="checkbox">
                                                            <asp:CheckBox ID="chk_SGFuncionNuevo" runat="server" Text="Nuevo" />
                                                        </asp:Panel>
                                                    </td>
                                                    <td style="width: 136px">
                                                        <asp:Panel ID="Panel45" runat="server" CssClass="checkbox">
                                                            <asp:CheckBox ID="chk_SGFuncionEventoEsp4" runat="server" Text="Especial4" />
                                                        </asp:Panel>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <asp:Panel ID="Panel55" runat="server">
                                                            <asp:TextBox ID="txt_SGFuncionEventoEspNombre4" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 325px">
                                                        <asp:Panel ID="Panel36" runat="server" CssClass="checkbox">
                                                            <asp:CheckBox ID="chk_SGFuncionEliminar" runat="server" Text="Eliminar" />
                                                        </asp:Panel>
                                                    </td>
                                                    <td style="width: 136px">
                                                        <asp:Panel ID="Panel46" runat="server" CssClass="checkbox">
                                                            <asp:CheckBox ID="chk_SGFuncionEventoEsp5" runat="server" Text="Especial5" />
                                                        </asp:Panel>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <asp:Panel ID="Panel56" runat="server">
                                                            <asp:TextBox ID="txt_SGFuncionEventoEspNombre5" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 325px">
                                                        <asp:Panel ID="Panel37" runat="server" CssClass="checkbox">
                                                            <asp:CheckBox ID="chk_SGFuncionRecuperar" runat="server" Text="Recuperar" />
                                                        </asp:Panel>
                                                    </td>
                                                    <td style="width: 136px">
                                                        <asp:Panel ID="Panel47" runat="server" CssClass="checkbox">
                                                            <asp:CheckBox ID="chk_SGFuncionEventoEsp6" runat="server" Text="Especial6" />
                                                        </asp:Panel>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <asp:Panel ID="Panel57" runat="server">
                                                            <asp:TextBox ID="txt_SGFuncionEventoEspNombre6" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 325px">
                                                        <asp:Panel ID="Panel38" runat="server" CssClass="checkbox">
                                                            <asp:CheckBox ID="chk_SGFuncionModificar" runat="server" Text="Modificar" />
                                                        </asp:Panel>
                                                    </td>
                                                    <td style="width: 136px">
                                                        <asp:Panel ID="Panel48" runat="server" CssClass="checkbox">
                                                            <asp:CheckBox ID="chk_SGFuncionEventoEsp7" runat="server" Text="Especial7" />
                                                        </asp:Panel>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <asp:Panel ID="Panel58" runat="server">
                                                            <asp:TextBox ID="txt_SGFuncionEventoEspNombre7" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 325px">
                                                        <asp:Panel ID="Panel39" runat="server" CssClass="checkbox">
                                                            <asp:CheckBox ID="chk_SGFuncionListar" runat="server" Text="Listar" />
                                                        </asp:Panel>
                                                    </td>
                                                    <td style="width: 136px">
                                                        <asp:Panel ID="Panel49" runat="server" CssClass="checkbox">
                                                            <asp:CheckBox ID="chk_SGFuncionEventoEsp8" runat="server" Text="Especial8" />
                                                        </asp:Panel>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <asp:Panel ID="Panel59" runat="server">
                                                            <asp:TextBox ID="txt_SGFuncionEventoEspNombre8" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 325px">
                                                        <asp:Panel ID="Panel40" runat="server" CssClass="checkbox">
                                                            <asp:CheckBox ID="chk_SGFuncionExportar" runat="server" Text="Exportar" />
                                                        </asp:Panel>
                                                    </td>
                                                    <td style="width: 136px">
                                                        <asp:Panel ID="Panel50" runat="server" CssClass="checkbox">
                                                            <asp:CheckBox ID="chk_SGFuncionEventoEsp9" runat="server" Text="Especial9" />
                                                        </asp:Panel>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <asp:Panel ID="Panel60" runat="server">
                                                            <asp:TextBox ID="txt_SGFuncionEventoEspNombre9" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 325px">
                                                        <asp:Panel ID="Panel41" runat="server" CssClass="checkbox">
                                                            <asp:CheckBox ID="chk_SGFuncionGraficar" runat="server" Text="Graficar" />
                                                        </asp:Panel>
                                                    </td>
                                                    <td style="width: 136px">
                                                        <asp:Panel ID="Panel51" runat="server" CssClass="checkbox">
                                                            <asp:CheckBox ID="chk_SGFuncionEventoEsp10" runat="server" Text="Especial10" />
                                                        </asp:Panel>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <asp:Panel ID="Panel61" runat="server">
                                                            <asp:TextBox ID="txt_SGFuncionEventoEspNombre10" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
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
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" CssClass="table table-condensed" DataKeyNames="SGFuncionID" Font-Size="XX-Small" ForeColor="#333333" GridLines="None" ShowFooter="True">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:HyperLinkField DataNavigateUrlFields="SGFuncionID" DataNavigateUrlFormatString="SGSubFunciones.aspx?id={0}" HeaderText="SubFuncion" Text="Ir" />
                                <asp:BoundField DataField="SGFuncionID" HeaderText="ID" />
                                <asp:BoundField DataField="SGFuncion" HeaderText="Función" />
                                <asp:BoundField DataField="SGFuncionAyuda" HeaderText="Ayuda" />
                                <asp:BoundField DataField="SGFuncionOLEDLL" HeaderText="OLEDLL" />
                                <asp:BoundField DataField="SGFuncionSubsistema" HeaderText="Subsistema" />
                                <asp:BoundField DataField="SGFuncionWeb" HeaderText="Url" />
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
