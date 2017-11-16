<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AgregarBascula.aspx.vb" Inherits="OPSystem.AgregarBascula" %>
<%@ Register src="../ControlUser/BarEventos.ascx" tagname="BarEventos" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../CSS/jquery-ui.css"  rel="stylesheet" type="text/css" />
    <script src="../JS/jquery-1.9.1.js"></script>
    <script src="../JS/jquery-ui.js"></script>
    <script src="../UI/i18n/datepicker-es.js"></script>
    <script>
        $(function () {
            $("#MainContent_txt_fecha").datepicker($.datepicker.regional["es"]);
        });
    </script>

    <script type="text/javascript">
        function ValidaForms() {
            alert('¡No debe dejar campos sin valor o en cero!')
        }
        function ErrorInsert() {
            alert('¡Error al insertar datos. Vuelva a Intentarlo!')
        }
        function ErrorUpdate() {
            alert('¡Error al actualizar datos. Vuelva a Intentarlo!')
        }
        function ErrorDelete() {
            alert('¡Error al borrar datos. Vuelva a Intentarlo!')
        }
        function ConfirmDelete() {
            return confirm('¿Realmente desea Eliminar el registro?')
        }
    </script>

    <div class="panel-body">
        

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="txt_peso_bruto" EventName="TextChanged" />
                <asp:AsyncPostBackTrigger ControlID="txt_peso_tara" EventName="TextChanged" />
            </Triggers>
        <ContentTemplate>
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
                        <asp:Panel ID="Panel6" runat="server">
                            <table style="width:100%;">
                                <tr>
                                    <td style="width: 150px">
                                        &nbsp;</td>
                                    <td>
                                        <asp:Panel ID="pnlAcción0" runat="server">
                                            <asp:Label ID="lblFiltro" runat="server" Font-Bold="True" ForeColor="#5D7B9D" Font-Size="17">Recepción Fletes</asp:Label>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel7" runat="server">
                                            <asp:Label ID="Label3" runat="server" CssClass="col-md-2 control-label" Text="Empresa ID" Width="150px"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel8" runat="server">
                                            <asp:TextBox ID="lbl_empresa_id" runat="server" CssClass="form-control" Width="300px" ReadOnly="True"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel16" runat="server">
                                            <asp:Label ID="Label5" runat="server" CssClass="col-md-2 control-label" Text="Empresa Nombre" Width="150px"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel19" runat="server">
                                            <asp:TextBox ID="lbl_empresa_nombre" runat="server" CssClass="form-control" Width="300px" ReadOnly="true"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel1" runat="server">
                                            <asp:Label ID="Label1" runat="server" CssClass="col-md-2 control-label" Text="IDF" Width="150px"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel2" runat="server">
                                            <asp:TextBox ID="lbl_idf" runat="server" CssClass="form-control" Width="300px" ReadOnly="true"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel3" runat="server">
                                            <asp:Label ID="Label2" runat="server" CssClass="col-md-2 control-label" Text="Proceso ID" Width="150px"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel4" runat="server">
                                            <asp:TextBox ID="lbl_proceso_id" runat="server" CssClass="form-control" Width="300px" ReadOnly="true"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel5" runat="server">
                                            <asp:Label ID="Label4" runat="server" CssClass="col-md-2 control-label" Text="Proceso Nombre" Width="150px"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel9" runat="server">
                                            <asp:TextBox ID="lbl_proceso_nombre" runat="server" CssClass="form-control" Width="300px" ReadOnly="true"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel10" runat="server">
                                            <asp:Label ID="Label6" runat="server" CssClass="col-md-2 control-label" Text="Fecha" Width="150px"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel11" runat="server">
                                            <asp:TextBox ID="txt_fecha" runat="server" CssClass="form-control" Width="300px" ReadOnly="false" AutoPostBack="True"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel12" runat="server">
                                            <asp:Label ID="Label7" runat="server" CssClass="col-md-2 control-label" Text="Peso Bruto" Width="150px"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel14" runat="server">
                                            <asp:TextBox ID="txt_peso_bruto" runat="server" CssClass="form-control" Width="300px" ReadOnly="false" AutoPostBack="True">0.00</asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel15" runat="server">
                                            <asp:Label ID="Label8" runat="server" CssClass="col-md-2 control-label" Text="Peso Tara" Width="150px"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel17" runat="server">
                                            <asp:TextBox ID="txt_peso_tara" runat="server" CssClass="form-control" Width="300px" AutoPostBack="True">0.00</asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel18" runat="server">
                                            <asp:Label ID="Label9" runat="server" CssClass="col-md-2 control-label" Text="Peso Neto" Width="150px"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel20" runat="server">
                                            <asp:TextBox ID="lbl_peso_neto" runat="server" CssClass="form-control" Width="300px" ReadOnly="true">0.00</asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel21" runat="server">
                                            <asp:Label ID="Label10" runat="server" CssClass="col-md-2 control-label" Text="Fecha Registro" Width="150px"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel22" runat="server">
                                            <asp:TextBox ID="lbl_fecha_registro" runat="server" CssClass="form-control" Width="300px" ReadOnly="true"></asp:TextBox>
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
        </ContentTemplate>
    </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlParametros" runat="server">
                    <table style="width:60%;">
                            <tr>
                                <td>&nbsp;</td>
                                <td><asp:Label ID="Label11" runat="server" Font-Bold="True" ForeColor="#5D7B9D">PARÁMETROS</asp:Label></td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" CellPadding="4" 
                                        DataKeyNames="ParametroID" Font-Size="XX-Small" 
                                        ForeColor="#333333" GridLines="None" ShowFooter="False" AllowPaging="False" 
                                        CssClass="table table-condensed" ShowHeader="True" ShowHeaderWhenEmpty="True">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:BoundField DataField="ParametroID" HeaderText="Parámetro ID" />
                                            <asp:BoundField DataField="ParametroNombre" HeaderText="Parámetro Nombre" />
                                            <asp:TemplateField HeaderText="Valor">
                                                    <ItemTemplate>
                                                    <asp:DropDownList ID="ddlAdd" runat="server" CssClass="form-control" 
                                                        AutoPostBack="True" OnTextChanged="ddl_flete_Changed"/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
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
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" 
                                        DataKeyNames="ParametroID" Font-Size="XX-Small" ShowHeaderWhenEmpty="True"
                                        ForeColor="#333333" GridLines="None" ShowFooter="False" AllowPaging="False" 
                                        CssClass="table table-condensed" ShowHeader="True">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:BoundField DataField="ParametroID" HeaderText="Parámetro ID" />
                                            <asp:BoundField DataField="ParametroNombre" HeaderText="Parámetro Nombre" />
                                            <asp:TemplateField HeaderText="Valor">
                                                    <ItemTemplate>
                                                    <asp:TextBox ID="txtAdd" runat="server"
                                                        CssClass="form-control" AutoPostBack="True" 
                                                        OnTextChanged="txt_Folio_Changed" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
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
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
            <ContentTemplate>
                <asp:Panel ID="pnlGuia" runat="server">
                    <table style="width:100%;">
                            <tr>
                                <td>&nbsp;</td>
                                <td><asp:Label ID="Label12" runat="server" Font-Bold="True" ForeColor="#5D7B9D">GUÍA</asp:Label></td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="4" 
                                        Font-Size="XX-Small" ShowHeaderWhenEmpty="True"
                                        ForeColor="#333333" GridLines="None" ShowFooter="False" AllowPaging="False" 
                                        CssClass="table table-condensed" ShowHeader="True">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:BoundField DataField="GuiaID" Visible="False" />
                                            <asp:BoundField DataField="UbicacionID" Visible="False" />
                                            <asp:BoundField DataField="VariedadID" Visible="False" />
                                            <asp:BoundField DataField="EnvaseID" Visible="False" />
                                            <asp:BoundField DataField="EnvasePeso" Visible="False" />
                                            <asp:BoundField DataField="Origen" Visible="False" />
                                            <asp:BoundField DataField="GuiaCantidad" Visible="False" />
                                            <asp:BoundField DataField="GuiaPesoBruto" Visible="False" />
                                            <asp:BoundField DataField="TaraEnvase" Visible="False" />
                                            <asp:BoundField DataField="PesoNeto" Visible="False" />
                                            <asp:TemplateField HeaderText="Guia ID">
                                                    <ItemTemplate>
                                                    <asp:TextBox ID="txt_GuiaID" runat="server"
                                                        CssClass="form-control" ReadOnly="True" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Ubicación">
                                                    <ItemTemplate>
                                                    <asp:DropDownList ID="ddl_ubicacion" runat="server" CssClass="form-control" 
                                                        AutoPostBack="True" OnTextChanged="ddl_ubicacion_Changed"/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Variedad">
                                                    <ItemTemplate>
                                                    <asp:DropDownList ID="ddl_variedad" runat="server" CssClass="form-control" 
                                                        AutoPostBack="True" OnTextChanged="ddl_variedad_Changed" Enabled="True" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Envase">
                                                    <ItemTemplate>
                                                    <asp:DropDownList ID="ddl_envase" runat="server" CssClass="form-control"
                                                        AutoPostBack="True" OnTextChanged="ddl_envase_Changed" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Peso Envase">
                                                    <ItemTemplate>
                                                    <asp:TextBox ID="txt_PesoEnvase" runat="server"
                                                        CssClass="form-control" ReadOnly="true" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Origen">
                                                    <ItemTemplate>
                                                    <asp:TextBox ID="txt_Origen" runat="server"
                                                        CssClass="form-control" ReadOnly="true" TextMode="MultiLine" Width="180px" Height="60px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Cantidad">
                                                    <ItemTemplate>
                                                    <asp:TextBox ID="txt_Cantidad" runat="server" AutoPostBack="True"
                                                        CssClass="form-control" 
                                                        OnTextChanged="txt_Cantidad_textchanged" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Peso Bruto">
                                                    <ItemTemplate>
                                                    <asp:TextBox ID="txt_PesoBruto" runat="server"
                                                        CssClass="form-control" AutoPostBack="True" 
                                                        OnTextChanged="txt_Cantidad_textchanged" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Tara Envase">
                                                    <ItemTemplate>
                                                    <asp:TextBox ID="txt_TaraEnvase" runat="server"
                                                        CssClass="form-control" ReadOnly="true" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Peso Neto">
                                                    <ItemTemplate>
                                                    <asp:TextBox ID="txt_PesoNeto" runat="server"
                                                        CssClass="form-control" ReadOnly="true" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
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
            </ContentTemplate>
        </asp:UpdatePanel>
             <asp:ImageButton ID="imgBtnAplicaFiltro" runat="server" ImageUrl="~/Img/Acept-form.png" Width="30px" />
            <asp:ImageButton ID="imgbtnCancelaFiltro" runat="server" CausesValidation="False" ImageUrl="~/Img/Cancel-form.png" Width="30px" />
        
   </div>
</asp:Content>