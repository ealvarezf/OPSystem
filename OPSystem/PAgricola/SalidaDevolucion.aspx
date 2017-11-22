<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="SalidaDevolucion.aspx.vb" Inherits="OPSystem.SalidaDevolucion" %>

<%@ Register Src="~/ControlUser/BarEventos.ascx" TagName="BarEventos" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../CSS/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../JS/jquery-1.9.1.js"></script>
    <script src="../JS/jquery-ui.js"></script>

    <%-- <asp:TemplateField HeaderText="Agregar">                       
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkMark" runat="server"  />
                                        </ItemTemplate>
                                    </asp:TemplateField> --%>
    

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
            <uc1:BarEventos ID="BarEventos1" runat="server" OnAceptarClicked="BarEventos1_AceptarClicked" />
        </asp:Panel>

      <asp:Panel ID="pnlEncabezado" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td colspan="6">
                        <asp:Panel ID="Panel208" runat="server">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="auto-style59" style="width: 360px">&nbsp;</td>
                                    <td>
                                        <asp:Label ID="Label227" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#0033CC" Text="Salida"></asp:Label>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style67" style="width: 30px"></td>
                    <td class="auto-style68">
                        <asp:Panel ID="Panel193" runat="server" Width="121px">
                            <asp:Label ID="Label221" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="#0066CC" Text="No. Salida"></asp:Label>
                        </asp:Panel>
                    </td>
                    <td class="auto-style69">
                        <asp:Panel ID="Panel205" runat="server" Width="256px">
                            <asp:Label ID="lblID" runat="server" Font-Size="Small" ForeColor="Red"></asp:Label>
                        </asp:Panel>
                    </td>
                    <td class="auto-style70">
                        <asp:Panel ID="Panel199" runat="server" Width="121px">
                            <asp:Label ID="Label224" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="#0066CC" Text="Fecha"></asp:Label>
                        </asp:Panel>
                    </td>
                    <td class="auto-style71">
                        <asp:Panel ID="Panel202" runat="server" Width="199px">
                            <asp:Label ID="lblFecha" runat="server" Font-Size="Small" ForeColor="Red"></asp:Label>
                        </asp:Panel>
                    </td>
                    <td class="auto-style71"></td>
                </tr>
                <tr>
                    <td class="auto-style67" style="width: 30px"></td>
                    <td class="auto-style68">
                        <asp:Panel ID="Panel194" runat="server" Width="120px">
                            <asp:Label ID="Label222" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="#0066CC" Text="Almacen Origen"></asp:Label>
                        </asp:Panel>
                    </td>
                    <td class="auto-style69">
                        <asp:Panel ID="Panel206" runat="server" Width="261px">
                            <asp:Label ID="lblAlmOrigen" runat="server" Font-Size="Small" ForeColor="Red"></asp:Label>
                        </asp:Panel>
                    </td>
                    <td class="auto-style70">
                        <asp:Panel ID="Panel200" runat="server" Width="120px">
                            <asp:Label ID="Label225" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="#0066CC" Text="Almacen Destino"></asp:Label>
                        </asp:Panel>
                    </td>
                    <td class="auto-style71">
                        <asp:Panel ID="Panel203" runat="server" Width="281px">
                            <asp:Label ID="lblAlmDestino" runat="server" Font-Size="Small" ForeColor="Red"></asp:Label>
                        </asp:Panel>
                    </td>
                    <td class="auto-style71"></td>
                </tr>
                <tr>
                    <td class="auto-style66" style="width: 30px">&nbsp;</td>
                    <td class="auto-style42">
                        <asp:Panel ID="Panel195" runat="server" Width="123px">
                            <asp:Label ID="Label223" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="#0066CC" Text="Elaboró"></asp:Label>
                        </asp:Panel>
                    </td>
                    <td class="auto-style61">
                        <asp:Panel ID="Panel207" runat="server" Width="255px">
                            <asp:Label ID="lblElaboro" runat="server" Font-Size="Small" ForeColor="Red"></asp:Label>
                        </asp:Panel>
                    </td>
                    <td class="auto-style63">
                        <asp:Panel ID="Panel201" runat="server" Width="123px">
                            <asp:Label ID="Label226" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="#0066CC" Text="Encargado"></asp:Label>
                        </asp:Panel>
                    </td>
                    <td>
                        <asp:Panel ID="Panel204" runat="server" Width="280px">
                            <asp:Label ID="lblEncargado" runat="server" Font-Size="Small" ForeColor="Red"></asp:Label>
                        </asp:Panel>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style66" style="width: 30px">&nbsp;</td>
                    <td class="auto-style42">&nbsp;</td>
                    <td class="auto-style61">&nbsp;</td>
                    <td class="auto-style63">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </asp:Panel>


         <asp:Panel ID="pnlConfirmarSalida" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td class="auto-style66" style="width: 30px">&nbsp;</td>
                    <td class="auto-style42" style="width: 27px">&nbsp;</td>
                    <td style="width: 112px">&nbsp;</td>
                    <td class="auto-style63" style="width: 589px">
                        <asp:Panel ID="Panel222" runat="server">
                             <asp:Panel ID="Panel3" runat="server" BorderColor="#000099" BorderStyle="Solid">
            <table style="width: 100%;">
                <tr>
                    <td colspan="6">
                        <asp:Panel ID="Panel4" runat="server" >
                            <table style="width: 100%;">
                                <tr>
                                    <td class="auto-style59" style="width: 218px">&nbsp;</td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#996600" Text="Confirmar Salida"></asp:Label>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style66" style="width: 30px">&nbsp;</td>
                    <td class="auto-style42">&nbsp;</td>
                    <td class="auto-style61">&nbsp;</td>
                    <td class="auto-style63" style="width: 3px">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style66" colspan="6">
                        <asp:Panel ID="Panel219" runat="server">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="auto-style59" style="width: 192px">&nbsp;</td>
                                    <td>
                                        <asp:Panel ID="Panel220" runat="server">
                                            <asp:Label ID="Label234" runat="server" Font-Bold="True" ForeColor="#5D7B9D" Text="¿Desea Confirmar la salida?"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style66" style="width: 30px">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="auto-style61" colspan="2">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style66" style="width: 30px">&nbsp;</td>
                    <td class="auto-style42" colspan="4">
                                           <asp:Panel ID="Panel145" runat="server">
                                            <table style="width:100%;">
                                                <tr>
                                                    <td style="width: 245px">&nbsp;</td>
                                                    <td style="width: 175px; margin-left: 40px;">
                                                        <a id="popup" data-toggle="modal" data-target="#myModal2"/>
                                                        <asp:Button ID="imgConfirmarSal" runat="server" CausesValidation="False" CssClass="btn-info disabled" Text="Confirmar" />
                                                    </td>
                                                    <td style="width: 185px">
                                                        <%--<asp:Button ID="imgbtnCancelaFiltro" runat="server" CausesValidation="False" CssClass="btn-danger disabled active" Text="Cancelar" />--%>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style66" style="width: 30px">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="auto-style61">&nbsp;</td>
                    <td style="width: 3px">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </asp:Panel>
                        </asp:Panel>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </asp:Panel>

    
      <asp:Panel ID="pnlAdd" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Panel ID="pnlformulario" runat="server">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 247px">&nbsp;</td>
                                    <td style="width: 10px">
                                        <asp:Panel ID="pnlAcción" runat="server" Width="415px">
                                            <asp:Label ID="lblAcción" runat="server" Font-Bold="True" ForeColor="#5D7B9D" Visible="False"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 247px; height: 51px;">
                                        <asp:Panel ID="PnlKey" runat="server">
                                            <asp:Label ID="lbl_FolioID" runat="server" CssClass="col-md-2 control-label" Font-Bold="False" ForeColor="Black">FOLIO SALIDA</asp:Label>
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
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 51px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 247px; height: 51px;">
                                        <asp:Panel ID="Panel170" runat="server">
                                            <asp:Label ID="Label21" runat="server" CssClass="col-md-2 control-label" Text="ALMACEN ORIGEN" Font-Bold="False"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 51px; width: 10px;">
                                        <asp:Panel ID="Panel157" runat="server" Width="415px">                                       
                                                    <asp:TextBox ID="txt_AlmOrg" runat="server" CssClass="form-control" placeholder="Almacen Origen" Width="300px"></asp:TextBox>                                                
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 51px">
                                        <asp:Panel ID="Panel163" runat="server">
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 51px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 247px; height: 51px;">
                                        <asp:Panel ID="Panel215" runat="server">
                                            <asp:Label ID="Label229" runat="server" CssClass="col-md-2 control-label" Font-Bold="False" Text="CANTIDAD EXISTENTE"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 51px; width: 10px;">
                                        <asp:Panel ID="Panel216" runat="server" Width="415px">
                                            <asp:TextBox ID="txt_CantExist" runat="server" CssClass="form-control" placeholder="Almacen Origen" Width="300px"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 51px">&nbsp;</td>
                                    <td style="height: 51px">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 247px; height: 51px;">
                                        <asp:Panel ID="Panel162" runat="server">
                                            <asp:Label ID="Label24" runat="server" CssClass="col-md-2 control-label" Font-Bold="False" Text="ALMACEN DESTINO"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 51px; width: 10px;">
                                        <asp:Panel ID="Panel209" runat="server" Width="415px">
                                            <asp:TextBox ID="txt_AlmDest" runat="server" CssClass="form-control" placeholder="Almacen Origen" Width="300px"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 51px">
                                        <asp:Panel ID="Panel166" runat="server">
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 51px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 247px; height: 58px;">
                                        <asp:Panel ID="Panel14" runat="server">
                                            <asp:Label ID="Label7" runat="server" CssClass="col-md-2 control-label" Text="ORIGEN SEMILLA" Font-Bold="False"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 58px; width: 10px;">
                                        <asp:Panel ID="Panel15" runat="server" Width="414px">
                                            <asp:TextBox ID="txt_Origen" runat="server" CssClass="form-control" Height="65px" placeholder="Origen Semilla" TextMode="MultiLine" Width="352px"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 58px">
                                        <asp:Panel ID="Panel164" runat="server">
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 58px"></td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="height: 51px;">
                                        <asp:Panel ID="Panel210" runat="server" Height="32px">
                                            <hr style="height: -42px" />
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 51px;" colspan="4">
                                        <asp:Panel ID="pnlEAgregar" runat="server">
                                            
                                            <table style="width:100%;">
                                                <tr>
                                                    <td style="width: 380px">&nbsp;</td>
                                                    <td>
                                                        <asp:Label ID="Label231" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#5D7B9D" Text="Agregar"></asp:Label>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </table>
                                            
                                        </asp:Panel>
                                        <asp:Panel ID="pnlEModificar" runat="server">
                                            <table style="width:100%;">
                                                <tr>
                                                    <td style="width: 381px">&nbsp;</td>
                                                    <td>
                                                        <asp:Label ID="Label232" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#5D7B9D" Text="Modificar"></asp:Label>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </table>
                                            </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 247px; height: 51px;">
                                        <asp:Panel ID="Panel150" runat="server">
                                            <asp:Label ID="Label20" runat="server" CssClass="col-md-2 control-label" Font-Bold="False" Text="PRODUCTO"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td style="width: 10px; height: 51px;">
                                        <asp:Panel ID="Panel151" runat="server" Width="413px">
                                            <asp:DropDownList ID="DDL_PRODUCTO" runat="server" CssClass="form-control" Height="35px" Width="352px">
                                            </asp:DropDownList>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 51px">
                                        <asp:Panel ID="Panel165" runat="server">
                                            <asp:CompareValidator ID="RFV_PRODUCTO" runat="server" ControlToValidate="DDL_PRODUCTO" CssClass="alert-warning" ErrorMessage="*" Font-Size="Medium" Operator="NotEqual" ValueToCompare="%"></asp:CompareValidator>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 51px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 247px; height: 51px;">
                                        <asp:Panel ID="Panel158" runat="server">
                                            <asp:Label ID="Label22" runat="server" CssClass="col-md-2 control-label" Font-Bold="False" Text="CANTIDAD DEVOLUCIÓN"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td style="width: 10px; height: 51px;">
                                        <asp:Panel ID="Panel160" runat="server" Width="416px">
                                            <table style="width:100%;">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txt_Cantidad" runat="server" CssClass="form-control" placeholder="Cantidad a Devolver" Width="300px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:RangeValidator ID="RFVLIMITECANT" runat="server" ControlToValidate="txt_Cantidad" errormessage="Por favor Ingrese valores entre 0 - 100" forecolor="Red" maximumvalue=" 100" minimumvalue="0" Type="Double"></asp:RangeValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 51px">
                                        <asp:Panel ID="Panel167" runat="server">
                                            <asp:RequiredFieldValidator ID="RFV_CANTIDAD" runat="server" ControlToValidate="txt_Cantidad" CssClass="alert-warning" ErrorMessage="*" Font-Size="Medium"></asp:RequiredFieldValidator>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 51px">
                                        <asp:Panel ID="Panel214" runat="server">
                                            <table style="width:100%;">
                                                <tr>
                                                    <td style="width: 111px">
                                                        <asp:RegularExpressionValidator ID="RFVCANNUM" runat="server" ControlToValidate="txt_Cantidad" CssClass="alert-warning" ErrorMessage="*Ingrese Valores Numericos" ForeColor="Red" ValidationExpression="\d*\.?\d*"></asp:RegularExpressionValidator>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 247px; height: 51px;">
                                        <asp:Panel ID="Panel169" runat="server">
                                            <asp:Label ID="Label27" runat="server" CssClass="col-md-2 control-label" placeholder="Persona Encargada" Font-Bold="False" Text="ALMACEN DEVOLUCIÓN"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td style="width: 10px; height: 51px;">
                                        <asp:Panel ID="Panel161" runat="server" Width="416px">
                                            <asp:DropDownList ID="DDL_ALMACEN_DES" runat="server" CssClass="form-control" Height="35px" Width="352px">
                                            </asp:DropDownList>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 51px">
                                        <asp:Panel ID="Panel168" runat="server">
                                            <asp:CompareValidator ID="RFV_ALMACENDEV" runat="server" ControlToValidate="DDL_ALMACEN_DES" CssClass="alert-warning" ErrorMessage="*" Font-Size="Medium" Operator="NotEqual" ValueToCompare="%"></asp:CompareValidator>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 51px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 247px; height: 80px;">
                                        <asp:Panel ID="Panel211" runat="server">
                                            <asp:Label ID="Label228" runat="server" CssClass="col-md-2 control-label" Font-Bold="False" placeholder="Persona Encargada" Text="OBSERVACIÓN"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td style="width: 10px; height: 80px;">
                                        <asp:Panel ID="Panel212" runat="server" Width="416px">
                                            <asp:TextBox ID="txt_Observacion" runat="server" CssClass="form-control" Height="65px" placeholder="Observación" TextMode="MultiLine" Width="352px"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 80px"></td>
                                    <td style="height: 80px">&nbsp;</td>
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
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td style="width: 259px">&nbsp;</td>
                                                    <td style="width: 120px">
                                                        <a id="popup" data-toggle="modal" data-target="#myModal"/>
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


   <%--Termina .modal content--%>


      <asp:Panel ID="pnlListar2" runat="server">

            <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="always" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="GridView2" EventName="SelectedIndexChanging" />
                    <asp:AsyncPostBackTrigger ControlID="GridView2" EventName="PageIndexChanging" />
                </Triggers>
                <ContentTemplate>

                    <table style="width: 100%;">
                        <tr>
                            <td>&nbsp;</td>
                            <td style="width: 19px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Panel ID="Panel217" runat="server">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="width: 52px">
                                                <asp:Label ID="lblkeyProd" runat="server" Visible="False"></asp:Label>
                                            </td>
                                            <td style="width: 26px">&nbsp;</td>
                                            <td style="width: 49px">&nbsp;</td>
                                            <td style="width: 215px">&nbsp;</td>
                                            <td>
                                                <asp:Label ID="Label230" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="Red" Text="Enviado"></asp:Label>
                                            </td>
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
                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" EmptyDataText="No Existen Registros." CellPadding="4" DataKeyNames="SalidaSemillaIDF,ProductoID,ProductoLote" Font-Size="XX-Small" ForeColor="#333333" GridLines="None" ShowFooter="True" AllowPaging="True" CssClass="table table-condensed" Width="864px">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:CommandField InsertVisible="False" ShowSelectButton="True" />
                                        <%-- <asp:TemplateField HeaderText="Agregar">                       
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkMark" runat="server"  />
                                        </ItemTemplate>
                                    </asp:TemplateField> --%>
                                        <asp:BoundField DataField="SalidaSemillaIDF" HeaderText="ID" ItemStyle-CssClass="SalidaSemillaIDF" />
                                        <asp:BoundField DataField="ProductoNombre" HeaderText="Producto" ItemStyle-CssClass="ProductoNombre" />
                                        <asp:BoundField DataField="Origen" HeaderText="Origen" ItemStyle-CssClass="Origen" />
                                        <asp:BoundField DataField="SalidaSemillaCantidad" HeaderText="Cantidad" ItemStyle-CssClass="SalidaSemillaCantidad" />
                                        <asp:BoundField DataField="SalidaSemillaPeso" HeaderText="Peso" ItemStyle-CssClass="SalidaSemillaPeso" />
                                        <asp:BoundField DataField="SalidaSemillaDensidad" HeaderText="Densidad" ItemStyle-CssClass="SalidaSemillaDensidad" />
                                        <asp:BoundField DataField="SalidaSemillaObservaciones" HeaderText="Observaciones" ItemStyle-CssClass="SalidaSemillaObservaciones" />
                                        <asp:BoundField DataField="sal_status" HeaderText="Status" ItemStyle-CssClass="sal_status" />
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
                                        <td style="height: 22px; width: 191px"></td>
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


      <asp:Panel ID="pnlListar3" runat="server">

            <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="always" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="GridView3" EventName="SelectedIndexChanging" />
                    <asp:AsyncPostBackTrigger ControlID="GridView3" EventName="PageIndexChanging" />
                </Triggers>
                <ContentTemplate>

                    <table style="width: 100%;">
                        <tr>
                            <td>&nbsp;</td>
                            <td style="width: 19px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Panel ID="Panel7" runat="server">
                                    <table class="nav-justified">
                                        <tr>
                                            <td style="width: 52px">
                                                <asp:Label ID="lblDevolucionesID" runat="server" Visible="False"></asp:Label>
                                            </td>
                                            <td style="width: 26px">&nbsp;</td>
                                            <td style="width: 49px">&nbsp;</td>
                                            <td style="width: 207px">&nbsp;</td>
                                            <td>
                                                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="Red" Text="Devoluciones"></asp:Label>
                                            </td>
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
                                 <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" EmptyDataText="No Existen Registros." CellPadding="4" DataKeyNames="SalidaSemillaIDF,DevProductoID,DevProductoLote" Font-Size="XX-Small" ForeColor="#333333" GridLines="None" ShowFooter="True" AllowPaging="True" CssClass="table table-condensed" Width="864px" PageSize="15">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                           <Columns>                                           
                                <asp:BoundField DataField="SalidaSemillaIDF" HeaderText="ID" />
                                <asp:BoundField DataField="ProductoNombre" HeaderText="Producto"/>
                                <asp:BoundField DataField="Origen" HeaderText="Origen">
                                <FooterStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                                <HeaderStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                                <ItemStyle HorizontalAlign="Left" Width="100px" Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DevSemillaCantidad" HeaderText="Cantidad" />
                                <asp:BoundField DataField="Alm_Org" HeaderText="Almacen Existente"/>
                                <asp:BoundField DataField="Alm_Dest" HeaderText="Almacen Devolucion"/>
                                <asp:BoundField DataField="DevSemillaObservaciones" HeaderText="Observaciones"/> 
                                <asp:TemplateField HeaderText="Eliminar" >
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkBtnDelete" CausesValidation ="false" runat="server" CommandName="Delete">Eliminar</asp:LinkButton>
                                </ItemTemplate>
                                </asp:TemplateField>                               
                                   <asp:CommandField InsertVisible="False" ShowSelectButton="True" SelectText="Editar." />
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
                                        <td style="height: 22px; width: 191px"></td>
                                        <td style="height: 22px; width: 447px">&nbsp;</td>
                                        <td style="height: 22px"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="height: 22px">
                                            <asp:Panel ID="pnlNota" runat="server" Visible="False">
                                                <table style="width:100%;">
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="Panel221" runat="server" BorderColor="Red" BorderStyle="Dotted" Width="744px">
                                                                <table class="nav-justified">
                                                                    <tr>
                                                                        <td style="width: 76px">&nbsp;</td>
                                                                        <td>
                                                                            <asp:Label ID="Label235" runat="server" Font-Bold="True" ForeColor="#3333FF" Text="Nota:"></asp:Label>
                                                                            <asp:Label ID="Label236" runat="server" ForeColor="#3333FF" Text="Debe Cerrar el movimiento Despues de haber terminado las devoluciones"></asp:Label>
                                                                        </td>
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
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>

        </asp:Panel>
     



       </div>


</asp:Content>