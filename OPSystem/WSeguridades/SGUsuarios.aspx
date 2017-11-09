<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="SGUsuarios.aspx.vb" Inherits="OPSystem.SGUsuarios" %>
<%@ Register src="../ControlUser/BarEventos.ascx" tagname="BarEventos" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../CSS/jquery-ui.css"  rel="stylesheet" type="text/css" />
    <script src="../JS/jquery-1.9.1.js"></script>
    <script src="../JS/jquery-ui.js"></script>
    <script src="../UI/i18n/datepicker-es.js"></script>

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
                                            <asp:Label ID="Label3" runat="server" CssClass="col-md-2 control-label" Text="Email"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel8" runat="server">
                                            <asp:TextBox ID="txtSearch_SgUserEmail" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel9" runat="server">
                                            <asp:Label ID="Label4" runat="server" CssClass="col-md-2 control-label" Text="Perfil"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel10" runat="server">
                                            <%--<asp:TextBox ID="txtSearch_PerfilID" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>--%>
                                            <asp:DropDownList ID="DDLSearch_PerfilID" runat ="server" Width="280px" CssClass="form-control" ></asp:DropDownList>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                 <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel11" runat="server">
                                            <asp:Label ID="Label5" runat="server" CssClass="col-md-2 control-label" Text="Nombre"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel12" runat="server">
                                            <asp:TextBox ID="txtSearch_SgNombre" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
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
                                        <asp:Panel ID="Panel14" runat="server">
                                            <asp:Label ID="Label6" runat="server" CssClass="col-md-2 control-label" Text="ID"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel15" runat="server">
                                            <asp:TextBox ID="txt_SgUserID" runat="server" Width="300px" CssClass="form-control"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel16" runat="server">
                                            <asp:Label ID="Label7" runat="server" CssClass="col-md-2 control-label" Text="User Name"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel17" runat="server">
                                            <asp:TextBox ID="txt_SgUserName" runat="server" Width="300px" CssClass="form-control"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel18" runat="server">
                                            <asp:Label ID="Label8" runat="server" CssClass="col-md-2 control-label" Text="Email"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel19" runat="server">
                                            <asp:TextBox ID="txt_SgUserEmail" runat="server" Width="300px" CssClass="form-control"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel20" runat="server">
                                            <asp:Label ID="Label9" runat="server" CssClass="col-md-2 control-label" Text="Fecha Alta"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel21" runat="server">
                                            <asp:TextBox ID="txt_SgUserAlta" runat="server" Width="300px" CssClass="form-control"></asp:TextBox>
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
                                            <%--<asp:TextBox ID="txt_SGSubFuncionID" runat="server" Width="300px" CssClass="form-control"></asp:TextBox>--%>
                                            <asp:DropDownList ID="DDLPerfilID" runat ="server" Width="280px" CssClass="form-control" ></asp:DropDownList>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr hidden="hidden">
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel22" runat="server">
                                            <asp:Label ID="Label10" runat="server" CssClass="col-md-2 control-label" Text="Passwd"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel23" runat="server">
                                            <asp:TextBox ID="txtSgUserPasswd" runat="server" Width="300px" CssClass="form-control"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel3" runat="server">
                                            <asp:Label ID="Label2" runat="server" CssClass="col-md-2 control-label" Text="Apellidos"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel4" runat="server">
                                            <asp:TextBox ID="txt_SgApellidos" runat="server" Width="300px" CssClass="form-control"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="PnlKey" runat="server">
                                            <asp:Label ID="lbl_" runat="server" CssClass="col-md-2 control-label" Text="Nombre"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="pnlDes" runat="server">
                                            <asp:TextBox ID="txt_SgNombre" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">&nbsp;</td>
                                    <td>
                                        <asp:Label ID="lblSgUserID" runat="server" Visible ="false" ></asp:Label>
                                        <%--<asp:Label ID="lbl2" runat="server" Visible ="false" ></asp:Label>--%>
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
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="SgUserID" Font-Size="XX-Small" ForeColor="#333333" GridLines="None" ShowFooter="True" AllowPaging="True" CssClass="table table-condensed">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="SgUserID" HeaderText="ID" />
                                <asp:BoundField DataField="SgUserName" HeaderText="UserName" />
                                <asp:BoundField DataField="SgUserEmail" HeaderText="Email" />
                                <asp:BoundField DataField="SgUserAlta" HeaderText="Alta" />
                                <asp:BoundField DataField="PerfilNombre" HeaderText="Perfil" />
                                <asp:BoundField DataField="SgApellidos" HeaderText="Apellidos" />
                                <asp:BoundField DataField="SgNombre" HeaderText="Nombre" />
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
