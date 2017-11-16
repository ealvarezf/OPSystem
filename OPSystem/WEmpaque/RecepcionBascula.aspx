<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="RecepcionBascula.aspx.vb" Inherits="OPSystem.RecepcionBascula" %>
<%@ Register src="../ControlUser/BarEventos.ascx" tagname="BarEventos" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../CSS/jquery-ui.css"  rel="stylesheet" type="text/css" />
    <script src="../JS/jquery-1.9.1.js"></script>
    <script src="../JS/jquery-ui.js"></script>
    <script src="../UI/i18n/datepicker-es.js"></script>

    <script type="text/javascript">
        function ValidaSelect() {
            //Ingresamos un mensaje a mostrar
            //var mensaje = confirm("¿Te gusta Desarrollo Geek?");
            alert('¡No ha hecho ninguna selección!');
            //Detectamos si el usuario acepto el mensaje
            /*if (mensaje) {
                alert("¡Gracias por aceptar!");
            }
            //Detectamos si el usuario denegó el mensaje
            else {
                alert("¡Haz denegado el mensaje!");
            }*/
        }
</script>
    
    <center>
    <div class="panel-body">
        
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
                                            <asp:Label ID="lbl_Identificador" runat="server" CssClass="col-md-2 control-label" Text="ID"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="pnlDes" runat="server">
                                            <asp:TextBox ID="txt_Identificador" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel1" runat="server">
                                            <asp:Label ID="Label1" runat="server" CssClass="col-md-2 control-label" Text="Periodo"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel2" runat="server">
                                            <asp:TextBox ID="txt_pmo_keyfec" runat="server" Width="300px" CssClass="form-control"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel22" runat="server">
                                            <asp:Label ID="Label8" runat="server" CssClass="col-md-2 control-label" Text="Observación"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel23" runat="server">
                                            <asp:TextBox ID="txt_pmo_observ" runat="server" CssClass="form-control" MaxLength="255" Width="300px"></asp:TextBox>
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

        <asp:Panel ID="pnlEventos" runat="server">
                    <uc1:BarEventos ID="BarEventos1" runat="server" />
                </asp:Panel>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="imgBtnAplicaFiltro" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="imgbtnCancelaFiltro" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="txtSearch_emp_idf" EventName="TextChanged" />
                <asp:AsyncPostBackTrigger ControlID="txtSearch_emp_keyfec" EventName="TextChanged" />
                <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="SelectedIndexChanging" />
            </Triggers>
            <ContentTemplate>
                <script>
                    $(function () {
                        $("#MainContent_txtSearch_emp_keyfec").datepicker($.datepicker.regional["es"]);
                    });
                </script>
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
                                            <asp:Label ID="Label3" runat="server" CssClass="col-md-2 control-label" Text="IDF"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel8" runat="server">
                                            <asp:TextBox ID="txtSearch_emp_idf" runat="server" CssClass="form-control" Width="300px" AutoPostBack="True"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel16" runat="server">
                                            <asp:Label ID="Label5" runat="server" CssClass="col-md-2 control-label" Text="Fecha"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel19" runat="server">
                                            <asp:TextBox ID="txtSearch_emp_keyfec" runat="server" CssClass="form-control" Width="300px" AutoPostBack="True"></asp:TextBox>
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
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" 
                            DataKeyNames="EmpresaID,BasculaIDF" Font-Size="XX-Small" 
                            ForeColor="#333333" GridLines="None" ShowFooter="True" AllowPaging="True" 
                            CssClass="table table-condensed" ShowHeaderWhenEmpty="True">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="EmpresaID" HeaderText="Empresa ID" />
                                <asp:BoundField DataField="EmpresaNombre" HeaderText="Empresa Nombre" />
                                <asp:BoundField DataField="BasculaIDF" HeaderText="IDF" />
                                <asp:BoundField DataField="ProcesoID" HeaderText="Proceso ID" />
                                <asp:BoundField DataField="ProcesoNombre" HeaderText="Proceso Nombre" />
                                <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                                <asp:BoundField DataField="BasculaPesoBruto" HeaderText="Peso Bruto" />
                                <asp:BoundField DataField="BasculaPesoTara" HeaderText="Peso Tara" />
                                <asp:BoundField DataField="PesoNeto" HeaderText="Peso Neto" />
                                <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" />
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
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
    </center>
</asp:Content>