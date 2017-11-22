<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="SalidaSemilla.aspx.vb" Inherits="OPSystem.SalidaSemilla" %>
<%@ Register src="~/ControlUser/BarEventos.ascx" tagname="BarEventos" tagprefix="uc1" %>
<%@ Register Src ="~/ControlUser/BarFlete.ascx" TagName="BarFlete" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="../CSS/jquery-ui.css"  rel="stylesheet" type="text/css" />
    <script src="../JS/jquery-1.9.1.js"></script>
    <script src="../JS/jquery-ui.js"></script>
    <script src="../UI/i18n/datepicker-es.js"></script>
    <script src="../UI/i18n/datepicker-es.js"></script>

    <script>
        $(function () {
            $("#MainContent_txt_Fecha").datepicker($.datepicker.regional["es"]);
        });
    </script>

     <script>
         $(function () {
             $("#MainContent_txtSearch_Fecha").datepicker($.datepicker.regional["es"]);
         });
    </script>


    <div class="panel-body">

         <asp:Panel ID="pnlEventos" runat="server">
            <uc1:BarEventos ID="BarEventos1" runat="server" OnAceptarClicked="BarEventos1_AceptarClicked"/>
         </asp:Panel>

         <asp:Panel ID="pnlFiltros" runat="server">
            <table style="width:100%;">
                <tr>
                    <td>&nbsp;</td>
                    <td style="width: 878px">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td style="width: 878px">
                        <asp:Panel ID="Panel136" runat="server" CssClass="panel-footer" Width="836px">
                            <table style="width: 99%; height: 204px;">
                                <tr>
                                    <td style="width: 126px">&nbsp;</td>
                                    <td>
                                        <asp:Label ID="lblFiltro0" runat="server" Font-Bold="True" ForeColor="#5D7B9D">Filtros</asp:Label>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 126px; height: 57px;">
                                        <asp:Panel ID="Panel140" runat="server" Width="133px">
                                            <asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#6600CC" Text="Folio:"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 57px">
                                        <asp:Panel ID="Panel152" runat="server">
                                            <asp:TextBox ID="txtSearch_Folio" runat="server" CssClass="form-control" Height="30px" Width="300px"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 57px">&nbsp;</td>
                                    <td style="height: 57px">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 126px; height: 57px;">
                                        <asp:Panel ID="Panel138" runat="server" Width="133px">
                                            <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#6600CC" Text="Fecha:"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 57px">
                                        <asp:Panel ID="Panel147" runat="server">
                                            <asp:TextBox ID="txtSearch_Fecha" runat="server" CssClass="form-control" Height="30px" Width="300px"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 57px"></td>
                                    <td style="height: 57px"></td>
                                </tr>
                                <tr>
                                    <td style="height: 54px; width: 126px">
                                        <asp:Panel ID="Panel139" runat="server" Width="133px">
                                            <asp:Label ID="Label8" runat="server" Font-Bold="True" ForeColor="#6600CC" Text="Almacen Origen:" Font-Size="Medium"></asp:Label>
                                        </asp:Panel>                                        
                                    </td>
                                    <td style="height: 54px">
                                        <asp:Panel ID="Panel142" runat="server">
                                            <asp:TextBox ID="txtSearch_AlmO" runat="server" CssClass="form-control" Height="30px" Width="300px"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 54px"></td>
                                    <td style="height: 54px"></td>
                                </tr>
                                <tr>
                                    <td style="height: 49px; width: 126px">
                                        <asp:Panel ID="Panel141" runat="server" Width="133px">
                                            <asp:Label ID="Label19" runat="server" Font-Bold="True" ForeColor="#6600CC" Text="Almacen Destino:" Font-Size="Medium"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 49px">
                                        <asp:Panel ID="Panel144" runat="server" Width="662px">
                                            <asp:TextBox ID="txtSearch_AlmD" runat="server" CssClass="form-control" Height="28px" Width="300px"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 49px"></td>
                                    <td style="height: 49px"></td>
                                </tr>
                                <tr>
                                    <td style="height: 24px; width: 126px"></td>
                                    <td style="height: 24px"></td>
                                    <td style="height: 24px"></td>
                                    <td style="height: 24px"></td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="height: 24px; ">
                                        <asp:Panel ID="Panel145" runat="server">
                                            <table style="width:100%;">
                                                <tr>
                                                    <td style="width: 122px">&nbsp;</td>
                                                    <td style="width: 175px; margin-left: 40px;">
                                                        <asp:Button ID="imgBtnAplicaFiltro" runat="server" CausesValidation="False" CssClass="btn btn-info" Text="Buscar" />
                                                    </td>
                                                    <td style="width: 475px">
                                                        <asp:Button ID="imgbtnCancelaFiltro" runat="server" CausesValidation="False" CssClass="btn-danger disabled active" Text="Cancelar" />
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </table>
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
                    <td style="width: 878px">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </asp:Panel>

         <asp:Panel ID="pnlFlete" runat="server">
                <uc2:BarFlete ID="Flete1" runat="server" />
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
                                    <td style="width: 247px">
                                        &nbsp;</td>
                                    <td style="width: 10px">
                                        <asp:Panel ID="pnlAcción" runat="server" Width="415px">
                                            <asp:Label ID="lblAcción"  runat="server" Font-Bold="True" ForeColor="#5D7B9D" Visible="False"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 247px; height: 51px;">
                                        <asp:Panel ID="PnlKey" runat="server">
                                            <asp:Label ID="lbl_FolioID" runat="server" CssClass="col-md-2 control-label" Font-Bold="False" ForeColor="Black">FOLIO</asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td style="width: 10px; height: 51px;">
                                        <asp:Panel ID="pnlDes" runat="server" Width="416px">
                                            <asp:Label ID="lbl_SalidaSem" runat="server"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 51px"></td>
                                    <td style="height: 51px">
                                        <asp:Label ID="lblkeyfle" runat="server" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 247px; height: 51px;">
                                        <asp:Panel ID="Panel1" runat="server">
                                            <asp:Label ID="Label2" runat="server" CssClass="col-md-2 control-label" Text="FECHA" Font-Bold="False"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 51px; width: 10px;">
                                        <asp:Panel ID="Panel2" runat="server" Width="416px">
                                            <asp:TextBox ID="txt_Fecha" runat="server" Width="300px" placeholder="Selecciona Fecha" CssClass="form-control"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 51px">
                                        <asp:Panel ID="Panel153" runat="server">
                                            <asp:RequiredFieldValidator ID="RFV_FECHA" runat="server" ControlToValidate="txt_Fecha" CssClass="alert-warning" ErrorMessage="*" Font-Size="Medium"></asp:RequiredFieldValidator>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 51px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 247px; height: 41px;">
                                        <asp:Panel ID="Panel3" runat="server">
                                            <asp:Label ID="Label1" runat="server" CssClass="col-md-2 control-label" Text="EMPRESA ORIGEN" Font-Bold="False"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 41px; width: 10px;">
                                        <asp:Panel ID="Panel4" runat="server" Width="415px">
                                            <asp:DropDownList ID="DDL_EMPRESA_ORG" runat="server" AutoPostBack="True" CssClass="form-control" Height="35px" Width="352px">
                                            </asp:DropDownList>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 41px">
                                        <asp:Panel ID="Panel155" runat="server">
                                            <asp:CompareValidator ID="RFV_EMPRESAO" runat="server" ControlToValidate="DDL_EMPRESA_ORG" CssClass="alert-warning" ErrorMessage="*" Font-Size="Medium" Operator="NotEqual" Type="Integer" ValueToCompare="0"></asp:CompareValidator>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 41px">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 247px; height: 51px;">
                                        <asp:Panel ID="Panel170" runat="server">
                                            <asp:Label ID="Label21" runat="server" CssClass="col-md-2 control-label" Font-Bold="False" Text="ALMACEN ORIGEN"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 51px; width: 10px;">
                                       
                                        <asp:Panel ID="Panel157" runat="server" Width="415px">
                                            <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                                            <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="DDL_EMPRESA_ORG" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <asp:DropDownList ID="DDL_ALM_ORG" runat="server" CssClass="form-control" Height="35px" Width="352px">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </asp:Panel>
                                       
                                    </td>
                                    <td style="height: 51px">
                                        <asp:Panel ID="Panel163" runat="server">
                                            <asp:CompareValidator ID="RFV_ALMACENO" runat="server" ControlToValidate="DDL_ALM_ORG" CssClass="alert-warning" ErrorMessage="*" Font-Size="Medium" Operator="NotEqual" ValueToCompare="%"></asp:CompareValidator>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 51px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 247px; height: 51px;">
                                        <asp:Panel ID="Panel162" runat="server">
                                            <asp:Label ID="Label24" runat="server" CssClass="col-md-2 control-label" Font-Bold="False" Text="ORIGEN"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 51px; width: 10px;">
                                        <asp:TextBox ID="txt_Origen" runat="server" CssClass="form-control" placeholder="Origen Semilla" Width="300px"></asp:TextBox>
                                    </td>
                                    <td style="height: 51px">
                                        <asp:Panel ID="Panel166" runat="server">
                                            <asp:RequiredFieldValidator ID="RFV_ORIGEN" runat="server" ControlToValidate="txt_Origen" CssClass="alert-warning" ErrorMessage="*" Font-Size="Medium"></asp:RequiredFieldValidator>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 51px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 247px; height: 51px;">
                                        <asp:Panel ID="Panel14" runat="server">
                                            <asp:Label ID="Label7" runat="server" CssClass="col-md-2 control-label" Text="EMPRESA DESTINO" Font-Bold="False"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 51px; width: 10px;">
                                        <asp:Panel ID="Panel15" runat="server" Width="414px">
                                            <asp:DropDownList ID="DDL_EMPRESA_DEST" runat="server" AutoPostBack="True" CssClass="form-control" Height="35px" Width="352px">
                                            </asp:DropDownList>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 51px">
                                        <asp:Panel ID="Panel164" runat="server">
                                            <asp:CompareValidator ID="RFV_EMPRESAD" runat="server" ControlToValidate="DDL_EMPRESA_DEST" CssClass="alert-warning" ErrorMessage="*" Font-Size="Medium" Operator="NotEqual" Type="Integer" ValueToCompare="0"></asp:CompareValidator>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 51px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 247px; height: 51px;">
                                        <asp:Panel ID="Panel150" runat="server">
                                            <asp:Label ID="Label20" runat="server" CssClass="col-md-2 control-label" Text="ALMACEN DESTINO" Font-Bold="False"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td style="width: 10px; height: 51px;">
                                        <asp:Panel ID="Panel151" runat="server" Width="413px">
                                              
                                                <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="DDL_EMPRESA_DEST" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                            <ContentTemplate>
                                            <asp:DropDownList ID="DDL_ALMACEN_DES" runat="server" CssClass="form-control" Height="35px" Width="352px">
                                            </asp:DropDownList>
                                               </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 51px">
                                        <asp:Panel ID="Panel165" runat="server">
                                            <asp:CompareValidator ID="RFV_ALMACEND" runat="server" ControlToValidate="DDL_ALMACEN_DES" CssClass="alert-warning" ErrorMessage="*" Font-Size="Medium" Operator="NotEqual" ValueToCompare="%"></asp:CompareValidator>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 51px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 247px; height: 51px;">
                                        <asp:Panel ID="Panel158" runat="server">
                                            <asp:Label ID="Label22" runat="server" CssClass="col-md-2 control-label" Font-Bold="False" Text="ELABORÓ"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td style="width: 10px; height: 51px;">
                                        <asp:Panel ID="Panel160" runat="server" Width="416px">
                                            <asp:TextBox ID="txt_Elaboro" runat="server" CssClass="form-control" placeholder="Persona que Elaboró" Width="300px"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 51px">
                                        <asp:Panel ID="Panel167" runat="server">
                                            <asp:RequiredFieldValidator ID="RFV_ELABORO" runat="server" ControlToValidate="txt_Elaboro" CssClass="alert-warning" ErrorMessage="*" Font-Size="Medium"></asp:RequiredFieldValidator>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 51px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 247px; height: 36px;">
                                        <asp:Panel ID="Panel169" runat="server">
                                            <asp:Label ID="Label27" runat="server" CssClass="col-md-2 control-label" placeholder="Persona Encargada" Font-Bold="False" Text="ENCARGADO DE SIEMBRA"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td style="width: 10px; height: 36px;">
                                        <asp:Panel ID="Panel161" runat="server" Width="416px">
                                            <asp:TextBox ID="txt_Encargado" runat="server" placeholder="Encargado" CssClass="form-control" Width="300px"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 36px">
                                        <asp:Panel ID="Panel168" runat="server">
                                            <asp:RequiredFieldValidator ID="RFV_ENCARGADO" runat="server" ControlToValidate="txt_Encargado" CssClass="alert-warning" ErrorMessage="*" Font-Size="Medium"></asp:RequiredFieldValidator>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 36px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 247px">&nbsp;</td>
                                    <td style="width: 10px">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Panel ID="Panel5" runat="server">
                                            <table style="width:100%;">
                                                <tr>
                                                    <td style="width: 259px">&nbsp;</td>
                                                    <td style="width: 120px">
                                                        <asp:ImageButton ID="ImgBtnAceptar" runat="server" ImageUrl="~/Img/Acept-form.png" Width="30px" />
                                                        <asp:Label ID="Label25" runat="server" Text="Guardar"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="ImgBtnCancelar" runat="server" CausesValidation="False" ImageUrl="~/Img/Cancel-form.png" Width="30px" />
                                                        <asp:Label ID="Label26" runat="server" Text="Cancelar"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                    <td>&nbsp;</td>
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

                  <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="always" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="SelectedIndexChanging" />
                 <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="PageIndexChanging" />        
            </Triggers>
            <ContentTemplate>

            <table style="width:100%;">
                <tr>
                    <td>&nbsp;</td>
                    <td style="width: 19px">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Panel ID="Panel130" runat="server">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 52px">&nbsp;</td>
                                    <td style="width: 26px">&nbsp;</td>
                                    <td style="width: 49px">&nbsp;</td>
                                    <td style="width: 221px">&nbsp;</td>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#0000CC" Text="Salidas Semilla"></asp:Label>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 52px">&nbsp;</td>
                                    <td style="width: 26px">&nbsp;</td>
                                    <td style="width: 49px">
                                        &nbsp;</td>
                                    <td style="width: 221px">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td style="width: 19px">&nbsp;</td>
                    <td>
                        <asp:Label ID="lblkeysal" runat="server" Visible="False"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td style="width: 19px">
                        &nbsp;</td>
                    <td>
                      <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" EmptyDataText="No Existen Registros." CellPadding="4" DataKeyNames="SalidaSemillaIDF" Font-Size="XX-Small" ForeColor="#333333" GridLines="None" ShowFooter="True" AllowPaging="True" CssClass="table table-condensed" Width="864px">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                           <Columns>
                                <asp:CommandField InsertVisible="False" ShowSelectButton="True" />                                                              
                                 <asp:BoundField DataField="SalidaSemillaIDF" HeaderText="ID" ItemStyle-CssClass="SalidaSemillaIDF"/>
                                <asp:BoundField DataField="SalidaSemillaFecha" DataFormatString="{0:d}" HeaderText="Fecha" ItemStyle-CssClass="SalidaSemillaFecha"/>
                                       <asp:BoundField DataField="Alm_Org" HeaderText="Almacen Origen" ItemStyle-CssClass="Alm_Org"/>    
                                       <asp:BoundField DataField="Alm_Des" HeaderText="Almacen Destino" ItemStyle-CssClass="Alm_Des"/>
                                       <asp:BoundField DataField="SalidaSemillaLotes" HeaderText="Origenes" ItemStyle-CssClass="SalidaSemillaLotes"/>                                                             
                                       <asp:BoundField DataField="SalidaSemillaElaboro" HeaderText="Elaboró" ItemStyle-CssClass="SalidaSemillaElaboro"/>   
                                      <asp:BoundField DataField="SalidaSemillaEncargadoSiembra" HeaderText="Encargado" ItemStyle-CssClass="SalidaSemillaEncargadoSiembra"/>                                 
                                <asp:TemplateField HeaderText="Status Flete" ItemStyle-CssClass="StatusFlete">
                                    <ItemTemplate>
                                        <asp:Image ID="GrdimgFlete" runat="server" Height="26px" Width="40px"  />                                        
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                <asp:BoundField DataField="StatusSal" HeaderText="Status Salida" ItemStyle-CssClass="StatusSal"/>                      
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
                    <td style="width: 19px">&nbsp;</td>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 268px; height: 22px"></td>
                                <td style="height: 22px; width: 191px">                                   
                                </td>
                                <td style="height: 22px; width: 447px">&nbsp;</td>
                                <td style="height: 22px"></td>
                            </tr>
                        </table>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td style="width: 19px">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            </ContentTemplate>
        </asp:UpdatePanel>

        </asp:Panel> 

        
           <asp:Panel ID="pnlAddSalFlete" runat="server" Width="1242px" style="margin-right: 337px">
                   <table style="width:100%;">
                       <tr>
                           <td class="auto-style72"></td>
                           <td class="auto-style73"></td>
                           <td class="auto-style74"></td>
                           <td class="auto-style74"></td>
                       </tr>
                       <tr>
                           <td class="auto-style15">&nbsp;</td>
                           <td class="auto-style46">
                               <div class="container">
                                    <div class="panel-group">
                                        <div class="panel panel-success">
                                          <div class="panel-heading">
                                              <asp:Panel ID="Panel184" runat="server" Width="1113px">
                                              <table style="width:100%;">
                                                  <tr>
                                                      <td class="auto-style24" style="width: 363px">&nbsp;</td>
                                                      <td class="auto-style44">
                                                          <asp:Panel ID="pnltxtAgregarS" runat="server">
                                                              <asp:Label ID="Label215" runat="server" Font-Bold="True" Text="Agregar Salidas al Flete"></asp:Label>
                                                          </asp:Panel>
                                                      </td>
                                                      <td>&nbsp;</td>
                                                  </tr>
                                                  <tr>
                                                      <td class="auto-style24" style="width: 363px">&nbsp;</td>
                                                      <td class="auto-style44">
                                                          <asp:Panel ID="pnltxtSAgregadas" runat="server">
                                                              <asp:Label ID="Label218" runat="server" Font-Bold="True" Text="Resumen de Salidas Por Agregar al Flete"></asp:Label>
                                                          </asp:Panel>
                                                      </td>
                                                      <td>&nbsp;</td>
                                                  </tr>
                                              </table>
                                              </asp:Panel>
                                            </div>
                                          <div class="panel-body">
                                              <asp:Panel ID="Panel183" runat="server">
                                       
       <table class="ui-accordion">
                                                  <tr>
                                                      <td colspan="4">
                                                          <asp:Panel ID="pnlEventos2" runat="server">
                                                                    <uc1:BarEventos ID="BarEventos2" runat="server"/>
                                                                 </asp:Panel>
                                                    <asp:Panel ID="pnlGridSalidasFlete" runat="server" Visible="False">                                                       
                                                              <table style="width:100%;">
                                                                  <tr>
                                                                      <td>&nbsp;</td>
                                                                      <td>
                                                                            
                                                                      </td>
                                                                      <td>&nbsp;</td>
                                                                  </tr>
                                                                  <tr>
                                                                      <td>&nbsp;</td>
                                                                      <td>&nbsp;</td>
                                                                      <td>&nbsp;</td>
                                                                  </tr>
                                                                  <tr>
                                                                      <td>&nbsp;</td>
                                                                      <td>                                       
                                                                          <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" CellPadding="4" CellSpacing="2" CssClass="gridview" DataKeyNames="SalidaSemillaIDF" EmptyDataText="No hay registros." Font-Size="XX-Small" ForeColor="#333333" GridLines="None" ShowFooter="True">
                                                                              <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                              <Columns>                                                                                 
                                                                                  <asp:CommandField InsertVisible="False" ShowSelectButton="True" />                                                                               
                                                                                  <asp:BoundField DataField="SalidaSemillaIDF" HeaderText="ID" ItemStyle-Width="100px" />
                                                                                  <asp:BoundField DataField="SalidaSemillaFecha" DataFormatString="{0:d}" HeaderText="Fecha" ItemStyle-Width="100px" />
                                                                                  <asp:BoundField DataField="Alm_Org" HeaderText="Almacen Origen" ItemStyle-Width="100px" />
                                                                                  <asp:BoundField DataField="Alm_Des" HeaderText="Almecen Destino" ItemStyle-Width="100px" />
                                                                                  <asp:BoundField DataField="SalidaSemillaLotes" HeaderText="Origen" ItemStyle-Width="100px" />
                                                                                  <asp:BoundField DataField="SalidaSemillaElaboro" HeaderText="Elaboró" ItemStyle-Width="100px" />
                                                                                  <asp:BoundField DataField="SalidaSemillaEncargadoSiembra" HeaderText="Encargado" ItemStyle-Width="100px" />
                                                                                  <asp:TemplateField HeaderText="Eliminar">
                                                                                      <ItemTemplate>
                                                                                          <asp:LinkButton ID="lnkBtnDelete" runat="server" CausesValidation="false" CommandName="Delete">Eliminar</asp:LinkButton>
                                                                                      </ItemTemplate>
                                                                                  </asp:TemplateField>                                                                                 
                                                                              </Columns>
                                                                              <EditRowStyle BackColor="#999999" />
                                                                              <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                              <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
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
                                                              </table>                                                 
                                                          </asp:Panel>
                                                      </td>
                                                      <td>&nbsp;</td>
                                                  </tr>
                                                  <tr>
                                                      <td class="auto-style49">&nbsp;</td>
                                                      <td class="auto-style70">&nbsp;</td>
                                                      <td>
                                                          &nbsp;</td>
                                                      <td class="auto-style56">
                                                          &nbsp;</td>
                                                      <td>&nbsp;</td>
                                                  </tr>
                                                  <tr>
                                                      <td class="auto-style49">
                                                       
                                                          &nbsp;</td>
                                                      <td class="auto-style70">&nbsp;</td>
                                                      <td>&nbsp;</td>
                                                      <td class="auto-style56">&nbsp;</td>
                                                      <td>&nbsp;</td>
                                                  </tr>
                                                  <tr>
                                                      <td class="auto-style49" colspan="4">
                                                          <asp:Panel ID="PnlListar2" runat="server">
           <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="always" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="GridView2" EventName="SelectedIndexChanging" />
                   <asp:AsyncPostBackTrigger ControlID="GridView2" EventName="PageIndexChanging" />    
            </Triggers>
            <ContentTemplate>
            <table style="width:100%;">
                <tr>
                    <td>&nbsp;</td>
                    <td style="width: 19px">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Panel ID="Panel6" runat="server">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 52px">&nbsp;</td>
                                    <td style="width: 26px">&nbsp;</td>
                                    <td style="width: 49px">&nbsp;</td>
                                    <td style="width: 221px">&nbsp;</td>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#0000CC" Text="Salidas Semilla"></asp:Label>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 52px">&nbsp;</td>
                                    <td style="width: 26px">&nbsp;</td>
                                    <td style="width: 49px">
                                        &nbsp;</td>
                                    <td style="width: 221px">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td style="width: 19px">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td style="width: 19px">
                        &nbsp;</td>
                    <td>
                      <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" EmptyDataText="No Existen Registros." CellPadding="4" DataKeyNames="SalidaSemillaIDF" Font-Size="XX-Small" ForeColor="#333333" GridLines="None" ShowFooter="True" AllowPaging="True" CssClass="table table-condensed">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                           <Columns>                                         
                                     <asp:TemplateField HeaderText="Agregar">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkMark" runat="server"  />
                                        </ItemTemplate>
                                          <FooterStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                                        <HeaderStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" Width="30px" Wrap="False" />
                                    </asp:TemplateField> 
                                <asp:BoundField DataField="SalidaSemillaIDF" HeaderText="ID" >    
                                        <FooterStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                                        <HeaderStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" Width="30px" Wrap="False" />
                               </asp:BoundField>
                                <asp:BoundField DataField="SalidaSemillaFecha" DataFormatString="{0:d}" HeaderText="Fecha" >    
                                        <FooterStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                                        <HeaderStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" Width="100px" Wrap="False" />
                               </asp:BoundField>
                                <asp:BoundField DataField="Alm_Org" HeaderText="Almacen Origen" ItemStyle-HorizontalAlign="Center">
                                        <FooterStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                                        <HeaderStyle HorizontalAlign="Left" Width="20px" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" Width="100px" Wrap="False" />
                                </asp:BoundField>                             
                                     <asp:BoundField DataField="Alm_Des" HeaderText="Almacen Destino" ItemStyle-HorizontalAlign="Center">
                                        <FooterStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                                        <HeaderStyle HorizontalAlign="center" Width="20px" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" Width="100px" Wrap="False" />
                                </asp:BoundField>  
                                <asp:BoundField DataField="SalidaSemillaLotes" HeaderText="Origen" >                                        
                                </asp:BoundField>  
                                <asp:BoundField DataField="SalidaSemillaElaboro" HeaderText="Elaboró" ItemStyle-HorizontalAlign="Center" >
                                        <FooterStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                                        <HeaderStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" Width="100px" Wrap="False" />
                                </asp:BoundField>  
                               <asp:BoundField DataField="SalidaSemillaEncargadoSiembra" HeaderText="Encargado">
                                        <FooterStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                                        <HeaderStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" Width="100px" Wrap="False" />
                               </asp:BoundField>
                                <asp:TemplateField HeaderText="Status Flete" ItemStyle-CssClass="StatusFlete">
                                    <ItemTemplate>
                                        <asp:Image ID="GrdimgFlete" runat="server" Height="26px" Width="40px"  />
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                                        <HeaderStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" Width="30px" Wrap="False" />
                                </asp:TemplateField>
                               <asp:BoundField DataField="StatusSal" HeaderText="Status Salida" >    
                                        <FooterStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                                        <HeaderStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" Width="100px" Wrap="False" />
                               </asp:BoundField>
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
                    <td style="width: 19px">&nbsp;</td>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 268px; height: 22px"></td>
                                <td style="height: 22px; width: 191px">                                   
                                </td>
                                <td style="height: 22px; width: 447px">&nbsp;</td>
                                <td style="height: 22px"></td>
                            </tr>
                        </table>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td style="width: 19px">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            </ContentTemplate>
        </asp:UpdatePanel>
                                                          </asp:Panel>
                                                      </td>
                                                      <td class="auto-style49">&nbsp;</td>
                                                  </tr>
                                                  <tr>
                                                      <td class="auto-style49">&nbsp;</td>
                                                      <td class="auto-style70">&nbsp;</td>
                                                      <td>&nbsp;</td>
                                                      <td class="auto-style56">&nbsp;</td>
                                                      <td>&nbsp;</td>
                                                  </tr>
                                                  <tr>
                                                      <td class="auto-style49">&nbsp;</td>
                                                      <td class="auto-style54" colspan="3">
                                                          <asp:Panel ID="pnlBotones" runat="server">
                                                              <table style="width:100%;">
                                                                  <tr>
                                                                      <td class="auto-style62" style="width: 207px">&nbsp;</td>
                                                                      <td class="auto-style59">
                                                                          <asp:Panel ID="Panel186" runat="server" Width="225px">
                                                                              <asp:ImageButton ID="btnCancelarF" runat="server" CausesValidation="False" Height="38px" ImageUrl="~/Img/Cancel-form.png" Width="40px" />
                                                                              <asp:Label ID="Label216" runat="server" Font-Bold="True" ForeColor="#CC0000" Text="Cancelar"></asp:Label>
                                                                          </asp:Panel>
                                                                      </td>
                                                                      <td class="auto-style60">
                                                                          <asp:Panel ID="pnlNext" runat="server" Width="190px">
                                                                               <a id="popup" data-toggle="modal" data-target="#myModal2"/>
                                                                              <asp:ImageButton ID="imgbtnNext" runat="server" Height="47px" ImageUrl="~/Img/Continuar.png" Width="50px" />
                                                                              <asp:Label ID="Label217" runat="server" Font-Bold="True" ForeColor="#3366CC" Text="Siguiente"></asp:Label>
                                                                          </asp:Panel>
                                                                      </td>
                                                                      <td>&nbsp;</td>
                                                                  </tr>
                                                              </table>
                                                          </asp:Panel>
                                                      </td>
                                                      <td>&nbsp;</td>
                                                  </tr>
                                              </table>
                                              </asp:Panel>
                                            </div>
                                        </div>                             
                                    </div>
                               </div>
                           </td>
                           <td>&nbsp;</td>
                           <td>&nbsp;</td>
                       </tr>
                       <tr>
                           <td class="auto-style15">&nbsp;</td>
                           <td class="auto-style46">
                               <asp:Panel ID="Panel188" runat="server">
                                   <table style="width:100%;">
                                       <tr>
                                           <td>&nbsp;</td>
                                           <td class="auto-style76">
                                               <asp:TextBox ID="txtResumen" runat="server" TextMode="MultiLine" Width="193px" Visible="False"></asp:TextBox>
                                           </td>
                                           <td>&nbsp;</td>
                                       </tr>
                                       <tr>
                                           <td>&nbsp;</td>
                                           <td class="auto-style76">&nbsp;</td>
                                           <td>&nbsp;</td>
                                       </tr>
                                       <tr>
                                           <td>&nbsp;</td>
                                           <td class="auto-style76">
                                               <asp:Panel ID="Panel189" Visible ="false" runat="server">
                                                   <asp:GridView ID="GridView4" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" CssClass="table table-condensed" DataKeyNames="SalidaSemillaIDF,ProductoID,ProductoLote" EmptyDataText="No Existen Registros." Font-Size="XX-Small" ForeColor="#333333" GridLines="None" ShowFooter="True">
                                                       <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                       <Columns>
                                                           <asp:CommandField InsertVisible="False" ShowSelectButton="True">
                                                           </asp:CommandField>
                                                           <asp:BoundField DataField="SalidaSemillaIDF" HeaderText="ID" />                                                           
                                                           <asp:BoundField DataField="ProductoNombre" HeaderText="Producto">
                                                           </asp:BoundField>                                                          
                                                           <asp:BoundField DataField="SalidaSemillaCantidad" HeaderText="Cantidad">
                                                           </asp:BoundField>                                                          
                                                           <asp:BoundField DataField="SalidaSemillaPeso" HeaderText="Peso">
                                                           </asp:BoundField>
                                                           <asp:BoundField DataField="SalidaSemillaDensidad" HeaderText="Densidad" />
                                                           <asp:BoundField DataField="SalidaSemillaObservaciones" HeaderText="Observaciones">
                                                           </asp:BoundField>                                                                                                                 
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
                                       <tr>
                                           <td>&nbsp;</td>
                                           <td class="auto-style76">&nbsp;</td>
                                           <td>&nbsp;</td>
                                       </tr>
                                   </table>
                               </asp:Panel>
                           </td>
                           <td>&nbsp;</td>
                           <td>&nbsp;</td>
                       </tr>
                   </table>

                </asp:Panel>



    </div>
</asp:Content>