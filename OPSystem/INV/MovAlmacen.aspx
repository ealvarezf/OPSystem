<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="MovAlmacen.aspx.vb" Inherits="OPSystem.MovAlmacen" %>
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
                                        <asp:Panel ID="Panel7" runat="server">
                                            <asp:Label ID="lblFiltro" runat="server" Font-Bold="True" ForeColor="#5D7B9D">Filtros</asp:Label>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel8" runat="server">
                                            <asp:Label ID="Label3" runat="server" CssClass="col-md-2 control-label" Text="Tipo"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel9" runat="server">
                                            <asp:DropDownList ID="Search_ddl_MovAlmacenSentido" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="%">TODOS</asp:ListItem>
                                                <asp:ListItem Value="E">ENTRADA</asp:ListItem>
                                                <asp:ListItem Value="S">SALIDA</asp:ListItem>
                                            </asp:DropDownList>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel12" runat="server">
                                            <asp:Label ID="Label14" runat="server" CssClass="col-md-2 control-label" Text="Fecha"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel13" runat="server">
                                            <asp:TextBox ID="Search_txt_MovAlmacenFecha" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel32" runat="server">
                                            <asp:Label ID="Label15" runat="server" CssClass="col-md-2 control-label" Text="Almacen"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel33" runat="server">
                                            <asp:DropDownList ID="Search_ddl_UbicacionAlmacenID" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel34" runat="server">
                                            <asp:Label ID="Label16" runat="server" CssClass="col-md-2 control-label" Text="Origen Rancho"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel35" runat="server">
                                            <asp:DropDownList ID="Search_ddl_UbicacionRanchoID" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel36" runat="server">
                                            <asp:Label ID="Label17" runat="server" CssClass="col-md-2 control-label" Text="Origen Tabla"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel37" runat="server">
                                            <asp:DropDownList ID="Search_ddl_UbicacionTablaID" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel38" runat="server">
                                            <asp:Label ID="Label18" runat="server" CssClass="col-md-2 control-label" Text="Origen Cultivo"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel39" runat="server">
                                            <asp:DropDownList ID="Search_ddl_CultivoID" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel40" runat="server">
                                            <asp:Label ID="Label19" runat="server" CssClass="col-md-2 control-label" Text="Origen Variedad"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel41" runat="server">
                                            <asp:DropDownList ID="Search_ddl_VariedadID" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel42" runat="server">
                                            <asp:Label ID="Label20" runat="server" CssClass="col-md-2 control-label" Text="Proceso"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel43" runat="server">
                                            <asp:DropDownList ID="Search_ddl_ProcesoID" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">&nbsp;</td>
                                    <td>
                                        <asp:Panel ID="Panel46" runat="server">
                                            <asp:ImageButton ID="imgBtnAplicaFiltro" runat="server" ImageUrl="~/Img/Acept-form.png" Width="30px" />
                                            <asp:ImageButton ID="imgbtnCancelaFiltro" runat="server" CausesValidation="False" ImageUrl="~/Img/Cancel-form.png" Width="30px" style="height: 28px" />
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
                                            <asp:Label ID="Label2" runat="server" CssClass="col-md-2 control-label" Text="Tipo"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel4" runat="server">
                                            <asp:DropDownList ID="ddl_MovAlmacenSentido" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="E">ENTRADA</asp:ListItem>
                                                <asp:ListItem Value="S">SALIDA</asp:ListItem>
                                            </asp:DropDownList>
                                        </asp:Panel>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 150px; height: 22px;">
                                        <asp:Panel ID="Panel14" runat="server">
                                            <asp:Label ID="Label4" runat="server" CssClass="col-md-2 control-label" Text="ID"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 22px">
                                        <asp:Panel ID="Panel15" runat="server">
                                            <asp:TextBox ID="txt_MovAlmacenID" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 22px">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel16" runat="server">
                                            <asp:Label ID="Label5" runat="server" CssClass="col-md-2 control-label" Text="Fecha"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel18" runat="server">
                                            <asp:TextBox ID="txt_MovAlmacenFecha" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel47" runat="server">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_MovAlmacenFecha" CssClass="alert-warning" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel17" runat="server">
                                            <asp:Label ID="Label6" runat="server" CssClass="col-md-2 control-label" Text="Almacen"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel20" runat="server">
                                            <asp:DropDownList ID="ddl_UbicacionAlmacenID" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel48" runat="server">
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddl_UbicacionAlmacenID" CssClass="alert-warning" ErrorMessage="CompareValidator" Operator="NotEqual" ValueToCompare="%">Falta Almacen</asp:CompareValidator>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel28" runat="server">
                                            <asp:Label ID="Label11" runat="server" CssClass="col-md-2 control-label" Text="Origen Rancho"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel30" runat="server">
                                            <asp:DropDownList ID="ddl_UbicacionRanchoID" runat="server" CssClass="form-control" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel49" runat="server">
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel19" runat="server">
                                            <asp:Label ID="Label7" runat="server" CssClass="col-md-2 control-label" Text="Origen Tabla"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel24" runat="server">
                                            <asp:DropDownList ID="ddl_UbicacionTablaID" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel50" runat="server">
                                            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddl_UbicacionTablaID" CssClass="alert-warning" ErrorMessage="CompareValidator" Operator="NotEqual" ValueToCompare="%">Falta Tabla Origen</asp:CompareValidator>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel29" runat="server">
                                            <asp:Label ID="Label12" runat="server" CssClass="col-md-2 control-label" Text="Origen Cultivo"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel31" runat="server">
                                            <asp:DropDownList ID="ddl_CultivoID" runat="server" CssClass="form-control" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel51" runat="server">
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel21" runat="server">
                                            <asp:Label ID="Label8" runat="server" CssClass="col-md-2 control-label" Text="Origen Variedad"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel25" runat="server">
                                            <asp:DropDownList ID="ddl_VariedadID" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel52" runat="server">
                                            <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="ddl_VariedadID" CssClass="alert-warning" ErrorMessage="CompareValidator" Operator="GreaterThan" Type="Integer" ValueToCompare="0">Falta Variedad</asp:CompareValidator>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel22" runat="server">
                                            <asp:Label ID="Label9" runat="server" CssClass="col-md-2 control-label" Text="Proceso"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel26" runat="server">
                                            <asp:DropDownList ID="ddl_ProcesoID" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel53" runat="server">
                                            <asp:CompareValidator ID="CompareValidator6" runat="server" ControlToValidate="ddl_ProcesoID" CssClass="alert-warning" ErrorMessage="CompareValidator" Operator="GreaterThan" Type="Integer" ValueToCompare="0">Falta Proceso</asp:CompareValidator>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel23" runat="server">
                                            <asp:Label ID="Label10" runat="server" CssClass="col-md-2 control-label" Text="Observaciones"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel27" runat="server">
                                            <asp:TextBox ID="txt_MovAlmacenObs" runat="server" CssClass="form-control" Width="775px" TextMode="MultiLine"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                    <td>&nbsp;</td>
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
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="MovAlmacenID" Font-Size="XX-Small" ForeColor="#333333" GridLines="None" ShowFooter="True" AllowPaging="True" CssClass="table table-condensed">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:HyperLinkField DataNavigateUrlFields="MovAlmacenID" DataTextField="MovAlmacenID" HeaderText="ID" DataNavigateUrlFormatString="MovAlmacenDetalle.aspx?key={0}" />
                                <asp:BoundField DataField="MovAlmacenFecha" HeaderText="Fecha" />
                                <asp:BoundField DataField="Almacen" HeaderText="Almacen" />
                                <asp:BoundField DataField="Tabla" HeaderText="Tabla" />
                                <asp:BoundField DataField="VariedadNombre" HeaderText="Variedad" />
                                <asp:BoundField DataField="ProcesoNombre" HeaderText="Proceso" />
                                <asp:BoundField DataField="MovAlmacenObs" HeaderText="Observación" />
                                <asp:BoundField DataField="MovAlmacenStsProceso" HeaderText="Estatus" />
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
