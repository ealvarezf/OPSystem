<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="FletesAdmin.aspx.vb" Inherits="OPSystem.FletesAdmin" validateRequest="false" %>
<%@ Register src="../ControlUser/BarEventos.ascx" tagname="BarEventos" tagprefix="uc1" %>
<%@ Register src="../ControlUser/BarEventosOld.ascx" tagname="BarEventos" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function FleteExiste() {
            alert('¡El Flete que intenta eliminar o modificar ya fue recibido!')
        }
        function FleteEliminado() {
            alert('¡Flete eliminado con Exito!')
        }
        function FleteNoEliminado() {
            alert('¡Ha ocurrido un error al intentar eliminar el flete. Intentelo de nuevo!')
        }
        function ValidaSelect() {
            alert('¡No ha hecho ninguna selección!');
        }
    </script>

    <center>
    <br /><br /><br /><br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
            <ContentTemplate>
                <uc1:BarEventos ID="BarEventos2" runat="server" />
                <asp:Panel ID="PnlWizard" runat="server">
                    <uc2:BarEventos ID="BarEventos3" runat="server" />
                <asp:Wizard ID="Wizard1" runat="server" ActiveStepIndex="0" BackColor="#F7F6F3" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Names="Verdana" Font-Size="Medium" Height="312px" Width="800px">
                    <HeaderStyle BackColor="#5D7B9D" BorderStyle="Solid" Font-Bold="True" Font-Size="0.9em" ForeColor="White" HorizontalAlign="Left" />
                    <NavigationButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="X-Large" ForeColor="#284775" />
                    <SideBarButtonStyle BorderWidth="0px" Font-Names="Verdana" ForeColor="White" />
                    <SideBarStyle BackColor="#2D2D30" BorderWidth="0px" Font-Size="Small" VerticalAlign="Top" Width="150px" />
                    <StepStyle BorderWidth="0px" ForeColor="#5D7B9D" />
                    <WizardSteps>
                        <asp:WizardStep ID="WizardStep1" runat="server" Title="FLETE AJO">
                            <asp:Panel ID="pnlFlete" runat="server">
                                <table style="width:100%;">
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Panel ID="Panel1" runat="server">
                                                <asp:Label ID="Label1" runat="server" Text="Fecha"></asp:Label>
                                            </asp:Panel>
                                        </td>
                                        <td>
                                            <asp:Panel ID="Panel2" runat="server">
                                                <asp:Label ID="lblFecha" runat="server" Text="Fecha"></asp:Label>
                                            </asp:Panel>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Panel ID="Panel3" runat="server">
                                                <asp:Label ID="lblTipo" runat="server" Text="Tipo"></asp:Label>
                                            </asp:Panel>
                                        </td>
                                        <td>
                                            <asp:Panel ID="Panel4" runat="server">
                                                <asp:DropDownList ID="DDLTipo" runat="server" AutoPostBack="True" Width="530px">
                                                </asp:DropDownList>
                                            </asp:Panel>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            <asp:CompareValidator ID="ValidaTipo" runat="server" ControlToValidate="DDLTipo" CssClass="alert-warning" ErrorMessage="¡¡ Información requerida !!" Operator="NotEqual" Type="Integer" ValueToCompare="0"></asp:CompareValidator>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Panel ID="Panel27" runat="server">
                                                <asp:Label ID="lblRes" runat="server" Text="Responsable"></asp:Label>
                                            </asp:Panel>
                                        </td>
                                        <td>
                                            <asp:Panel ID="Panel28" runat="server">
                                                <div style="margin-left: auto; margin-right: auto; text-align: center;">
                                                    <asp:Label ID="lblResponsable" runat="server" AutoPostBack="True" Width="530px"></asp:Label>
                                                </div>
                                            </asp:Panel>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Panel ID="Panel5" runat="server">
                                                <asp:Label ID="Label2" runat="server" Text="Proveedor"></asp:Label>
                                            </asp:Panel>
                                        </td>
                                        <td>
                                            <asp:Panel ID="Panel6" runat="server">
                                                <asp:DropDownList ID="DDLPrv" runat="server" AutoPostBack="True" Width="530px">
                                                </asp:DropDownList>
                                            </asp:Panel>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Panel ID="Panel12" runat="server">
                                                <asp:CompareValidator ID="ValidaPrv" runat="server" ControlToValidate="DDLPrv" CssClass="alert-warning" ErrorMessage="¡¡ Información requerida !!" Operator="NotEqual" ValueToCompare="%"></asp:CompareValidator>
                                            </asp:Panel>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Label ID="Label3" runat="server" Text="Ruta"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Panel ID="Panel7" runat="server">
                                                <asp:DropDownList ID="DDLRuta" runat="server" Width="530px" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </asp:Panel>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:CompareValidator ID="ValidaRuta" runat="server" ControlToValidate="DDLRuta" CssClass="alert-warning" ErrorMessage="¡¡ Información requerida !!" Operator="NotEqual" Type="Integer" ValueToCompare="0"></asp:CompareValidator>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Label ID="lblCamion" runat="server" Text="Camión"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Panel ID="Panel8" runat="server">
                                                <asp:DropDownList ID="DDLCamíon" runat="server" Width="530px" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </asp:Panel>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:CompareValidator ID="ValidaCamion" runat="server" ControlToValidate="DDLCamíon" CssClass="alert-warning" ErrorMessage="¡¡ Información requerida !!" Operator="NotEqual" Type="Integer" ValueToCompare="0"></asp:CompareValidator>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Label ID="Label5" runat="server" Text="Operador"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Panel ID="Panel9" runat="server">
                                                <asp:DropDownList ID="DDLOperador" runat="server" Width="530px" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </asp:Panel>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:CompareValidator ID="ValidaOperador" runat="server" ControlToValidate="DDLOperador" CssClass="alert-warning" ErrorMessage="¡¡ Información requerida !!" Operator="NotEqual" Type="Integer" ValueToCompare="0"></asp:CompareValidator>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                                        <td>
                                                            <asp:Panel ID="Panel31" runat="server">
                                                                <asp:Label ID="Label6" runat="server" Text="Observación"></asp:Label>
                                                            </asp:Panel>
                                                        </td>
                                                        <td>
                                                            <asp:Panel ID="Panel32" runat="server">
                                                                <asp:TextBox ID="txtObsGen" runat="server" MaxLength="50" TextMode="MultiLine" Width="500px" AutoPostBack="True"></asp:TextBox>
                                                            </asp:Panel>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                </table>
                            </asp:Panel>
                        </asp:WizardStep>
                        <asp:WizardStep ID="WizardStep2" runat="server" Title="DETALLE">
                            <asp:Panel ID="pnlDetalle" runat="server">
                                <table style="width:100%;">
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Panel ID="Panel10" runat="server">
                                                <asp:Label ID="lblFlete" runat="server" Text="ID Flete #"></asp:Label>
                                            </asp:Panel>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Panel ID="pnlEventos" runat="server">
                                                <uc2:BarEventos ID="BarEventos1" runat="server" />
                                            </asp:Panel>
                                
                                            <asp:Panel ID="pnlAdd" runat="server">

                                                <table style="width:100%;">
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="Panel23" runat="server">
                                                                <asp:Label ID="lblID" runat="server" Text="ID"></asp:Label>
                                                            </asp:Panel>
                                                        </td>
                                                        <td>
                                                            <asp:Panel ID="Panel24" runat="server">
                                                                <asp:TextBox ID="txtKey" runat="server" Enabled="False" AutoPostBack="True"></asp:TextBox>
                                                            </asp:Panel>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="Panel15" runat="server">
                                                                <asp:Label ID="Label7" runat="server" Text="Contenedor"></asp:Label>
                                                            </asp:Panel>
                                                        </td>
                                                        <td>
                                                            <asp:Panel ID="Panel19" runat="server">
                                                                <asp:DropDownList ID="DDLConten" runat="server" Width="530px" AutoPostBack="True">
                                                                </asp:DropDownList>
                                                            </asp:Panel>
                                                        </td>
                                                        <td>
                                                            <asp:CompareValidator ID="ValCon" runat="server" ControlToValidate="DDLConten" CssClass="alert-warning" ErrorMessage="Requerido" Font-Size="X-Small" Operator="NotEqual" Type="Integer" ValueToCompare="0"></asp:CompareValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="Panel16" runat="server">
                                                                <asp:Label ID="Label8" runat="server" Text="Cantidad"></asp:Label>
                                                            </asp:Panel>
                                                        </td>
                                                        <td>
                                                            <asp:Panel ID="Panel22" runat="server">
                                                                <asp:TextBox ID="txtCan" runat="server" AutoPostBack="True"></asp:TextBox>
                                                            </asp:Panel>
                                                        </td>
                                                        <td>
                                                            <asp:RequiredFieldValidator ID="ValCan" runat="server" ControlToValidate="txtCan" CssClass="alert-warning" ErrorMessage="Requerido" Font-Size="X-Small"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="Panel17" runat="server">
                                                                <asp:Label ID="Label9" runat="server" Text="Origen"></asp:Label>
                                                            </asp:Panel>
                                                        </td>
                                                        <td>
                                                            <asp:Panel ID="Panel20" runat="server">
                                                                <asp:DropDownList ID="DDLOrigen" runat="server" Width="530px" AutoPostBack="True">
                                                                </asp:DropDownList>
                                                            </asp:Panel>
                                                        </td>
                                                        <td>
                                                            <asp:CompareValidator ID="ValOrg" runat="server" ControlToValidate="DDLOrigen" CssClass="alert-warning" ErrorMessage="Requerido" Font-Size="X-Small" Operator="NotEqual" ValueToCompare="%"></asp:CompareValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="Panel18" runat="server">
                                                                <asp:Label ID="Label10" runat="server" Text="Calidad"></asp:Label>
                                                            </asp:Panel>
                                                        </td>
                                                        <td>
                                                            <asp:Panel ID="Panel21" runat="server">
                                                                <asp:DropDownList ID="DDLCal" runat="server" Width="530px" AutoPostBack="True">
                                                                </asp:DropDownList>
                                                            </asp:Panel>
                                                        </td>
                                                        <td>
                                                            <asp:CompareValidator ID="ValCal" runat="server" ControlToValidate="DDLCal" CssClass="alert-warning" ErrorMessage="Requerido" Font-Size="X-Small" Operator="NotEqual" Type="Integer" ValueToCompare="0"></asp:CompareValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="Panel13" runat="server">
                                                                <asp:Label ID="Label4" runat="server" Text="Variedad"></asp:Label>
                                                            </asp:Panel>
                                                        </td>
                                                        <td>
                                                            <asp:Panel ID="Panel14" runat="server">
                                                                <asp:DropDownList ID="DDLVar" runat="server" Width="530px" AutoPostBack="True">
                                                                </asp:DropDownList>
                                                            </asp:Panel>
                                                        </td>
                                                        <td>
                                                            <asp:CompareValidator ID="ValVar" runat="server" ControlToValidate="DDLVar" CssClass="alert-warning" ErrorMessage="Requerido" Font-Size="X-Small" Operator="NotEqual" Type="Integer" ValueToCompare="0"></asp:CompareValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="Panel29" runat="server">
                                                                <asp:Label ID="Label11" runat="server" Text="Observación"></asp:Label>
                                                            </asp:Panel>
                                                        </td>
                                                        <td>
                                                            <asp:Panel ID="Panel30" runat="server">
                                                                <asp:TextBox ID="txtObs" runat="server" MaxLength="50" TextMode="MultiLine" Width="500px" AutoPostBack="True"></asp:TextBox>
                                                            </asp:Panel>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>
                                                            <asp:Panel ID="pnlAceptar" runat="server">
                                                                <table style="width:100%;">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Panel ID="Panel25" runat="server">
                                                                                <asp:ImageButton ID="btnAceptar" runat="server" ImageUrl="~/Img/Save-icon.png" Width="30px" AutoPostBack="True" />
                                                                            </asp:Panel>
                                                                        </td>
                                                                        <td>&nbsp;</td>
                                                                        <td>
                                                                            <asp:Panel ID="Panel26" runat="server">
                                                                                <asp:ImageButton ID="btnCancelar" runat="server" CausesValidation="False" ImageUrl="~/Img/Cancelar.jpg" Width="30px" AutoPostBack="True" />
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                </table>
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
                                        <td>
                                            <asp:Panel ID="pnlLista" runat="server">
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                                    CellPadding="4" Font-Size="X-Small" ForeColor="#333333" GridLines="None" 
                                                    Width="631px" DataKeyNames="CosechaID">
                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                    <Columns>
                                                        <asp:BoundField DataField="CosechaID" HeaderText="IDD" />
                                                        <asp:BoundField HeaderText="Ubicación" DataField="UbicacionNombre" />
                                                        <asp:BoundField HeaderText="Variedad" DataField="VariedadNombre" />
                                                        <asp:BoundField HeaderText="Cant." DataField="CosechaCantidad" />
                                                        <asp:BoundField HeaderText="Contenedor" DataField="UnidadEmpaqueNombre" />
                                                        <asp:BoundField HeaderText="Calidad" DataField="ClasificaSizeNombre" />
                                                        <asp:CommandField ShowSelectButton="True" />
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
                                            </asp:Panel>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </asp:WizardStep>
                        <asp:WizardStep runat="server" Title="RESUMEN">
                            <asp:Panel ID="pnlResumen" runat="server">
                                <table style="width:100%;">
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Panel ID="Panel11" runat="server">
                                                <asp:TextBox ID="txtResumen" runat="server" Height="222px" TextMode="MultiLine" Width="620px" Enabled="False"></asp:TextBox>
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
                        </asp:WizardStep>
                    </WizardSteps>
                </asp:Wizard>
                </asp:Panel>
                <asp:Panel ID="PnlListas" runat="server">
            <br />
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="4" 
                            DataKeyNames="RutaID,CamionID,OperadorID" Font-Size="XX-Small" 
                            ForeColor="#333333" GridLines="None" ShowFooter="True" AllowPaging="True" 
                            CssClass="table table-condensed" ShowHeaderWhenEmpty="True">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="FleteID" HeaderText="Flete ID" />
                                <asp:BoundField DataField="FleteFecha" HeaderText="Fecha" />
                                <asp:BoundField DataField="TipoFleteNombre" HeaderText="Tipo Flete" />
                                <asp:BoundField DataField="rut_desrut" HeaderText="Ruta" />
                                <asp:BoundField DataField="TransportistaNombre" HeaderText="Transportista" />
                                <asp:BoundField DataField="cam_descam" HeaderText="Camión" />
                                <asp:BoundField DataField="Operador" HeaderText="Operador" />
                                <asp:BoundField DataField="FleteObservacion" HeaderText="Observacion" />
                                <asp:BoundField DataField="RutaID" Visible="False" />
                                <asp:BoundField DataField="CamionID" Visible="False" />
                                <asp:BoundField DataField="OperadorID" Visible="False" />
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
                </asp:Panel>
                </ContentTemplate>
        </asp:UpdatePanel>
        </center>
</asp:Content>