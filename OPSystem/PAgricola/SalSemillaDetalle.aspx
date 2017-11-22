<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="SalSemillaDetalle.aspx.vb" Inherits="OPSystem.SalSemillaDetalle" %>
<%@ Register src="~/ControlUser/BarEventos.ascx" tagname="BarEventos" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <link href="../CSS/jquery-ui.css"  rel="stylesheet" type="text/css" />
    <script src="../JS/jquery-1.9.1.js"></script>
    <script src="../JS/jquery-ui.js"></script>
    <script src="../UI/i18n/datepicker-es.js"></script>


    <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">  
    <style type="text/css" media="all">
     @import "boostrap.min.css";
        .style1
        {
            width: 161px;
        }
        .style2
        {
            width: 62px;
        }
    *{-webkit-box-sizing:border-box;-moz-box-sizing:border-box}
        .style3
        {
            color: #333333;
            font-size: Medium;
            border-collapse: collapse;
            background-color: transparent;
        }
        .auto-style10 {
            width: 150px;
            height: 46px;
        }
        .auto-style11 {
            width: 162px;
            height: 46px;
        }
        .auto-style14 {
            width: 21px;
            height: 56px;
        }
        .auto-style16 {
            width: 150px;
            height: 56px;
        }
        .auto-style17 {
            height: 56px;
        }
        .auto-style19 {
            width: 150px;
            height: 52px;
        }
        .auto-style20 {
            width: 21px;
            height: 52px;
        }
        .auto-style21 {
            height: 52px;
        }
        .auto-style22 {
            width: 150px;
            height: 51px;
        }
        .auto-style23 {
            width: 21px;
            height: 51px;
        }
        .auto-style25 {
            height: 51px;
        }
        .auto-style26 {
            width: 150px;
            height: 63px;
        }
        .auto-style27 {
            width: 21px;
            height: 63px;
        }
        .auto-style28 {
            height: 63px;
        }
        .auto-style29 {
            width: 330px;
        }
        .auto-style30 {
            width: 329px;
        }
        .auto-style31 {
            display: block;
            padding: 6px 12px;
            font-size: 14px;
            line-height: 1.428571429;
            color: #555555;
            vertical-align: middle;
            background-color: #ffffff;
            border: 1px solid #cccccc;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);
            box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);
            -webkit-transition: border-color ease-in-out 0.15s, box-shadow ease-in-out 0.15s;
            transition: border-color ease-in-out 0.15s, box-shadow ease-in-out 0.15s;
        }
        .auto-style32 {
            width: 21px;
        }
        .auto-style33 {
            width: 89%;
        }
        .auto-style34 {
            width: 46px;
        }
        .auto-style35 {
            width: 94%;
        }
        .auto-style36 {
            width: 102px;
        }
        .auto-style37 {
            width: 180px;
            height: 46px;
        }
        .auto-style38 {
            width: 554px;
        }
        .auto-style39 {
            width: 157px;
        }
        .auto-style40 {
            width: 112px;
        }
        .auto-style41 {
            margin-left: 40px;
        }
        .auto-style42 {
            width: 129px;
        }
        .auto-style59 {
            width: 363px;
        }
        .auto-style61 {
            width: 209px;
        }
        .auto-style63 {
            width: 135px;
        }
        .auto-style66 {
            width: 89px;
        }
        .auto-style67 {
            width: 89px;
            height: 23px;
        }
        .auto-style68 {
            width: 129px;
            height: 23px;
        }
        .auto-style69 {
            width: 209px;
            height: 23px;
        }
        .auto-style70 {
            width: 135px;
            height: 23px;
        }
        .auto-style71 {
            height: 23px;
        }
        .auto-style72 {
            height: 46px;
        }
    </style>

    <script>
        $(document).ready(function () {
        $("#mostrarmodal2").modal("show");
    });
</script>

    <script type="text/javascript">
        $(function () {
            var index = $("#MainContent_sel_tab").val();
            $("#tabs").tabs({ active: index });
        })
    </script>   

    <script>
         $(function () {
             $("#MainContent_txt_Fecha").datepicker($.datepicker.regional["es"]);
         });
    </script>


    <div class="panel-body">

        <asp:Panel ID="pnlEventos" runat="server">
            <uc1:BarEventos ID="BarEventos1" runat="server" />
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
                                    <td style="width: 83px">&nbsp;</td>
                                    <td>
                                        <asp:Label ID="lblFiltro0" runat="server" Font-Bold="True" ForeColor="#5D7B9D">Filtros</asp:Label>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 83px; height: 57px;">
                                        <asp:Panel ID="Panel140" runat="server" Width="133px">
                                            <asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#6600CC" Text="Producto:"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 57px">
                                        <asp:Panel ID="Panel152" runat="server">
                                            <asp:TextBox ID="txtSearch_Produc" runat="server" CssClass="form-control" Height="30px" Width="300px"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 57px">&nbsp;</td>
                                    <td style="height: 57px">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 83px; height: 57px;">
                                        <asp:Panel ID="Panel138" runat="server" Width="133px">
                                            <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#6600CC" Text="Tipo Producto:"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 57px">
                                        <asp:Panel ID="Panel147" runat="server">
                                            <asp:TextBox ID="txtSearch_TipoProd" runat="server" CssClass="form-control" Height="28px" Width="300px"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 57px"></td>
                                    <td style="height: 57px"></td>
                                </tr>
                                <tr>
                                    <td style="height: 24px; width: 83px"></td>
                                    <td style="height: 24px"></td>
                                    <td style="height: 24px"></td>
                                    <td style="height: 24px"></td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="height: 24px; ">
                                        <asp:Panel ID="Panel145" runat="server">
                                            <table style="width:100%;">
                                                <tr>
                                                    <td class="auto-style40">&nbsp;</td>
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
                                    <td class="auto-style32">
                                        <asp:Panel ID="pnlAcción" runat="server" Width="415px">
                                            <asp:Label ID="lblAcción"  runat="server" Font-Bold="True" ForeColor="#5D7B9D" Visible="False"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="PnlKey" runat="server">
                                            <asp:Label ID="lbl_SalidaID" runat="server" CssClass="col-md-2 control-label"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td class="auto-style32">
                                        <asp:Panel ID="pnlDes" runat="server" Width="416px">
                                            <asp:Label ID="lbl_NoSalida" runat="server"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style19">
                                        <asp:Panel ID="Panel1" runat="server">
                                            <asp:Label ID="Label1" runat="server" CssClass="col-md-2 control-label" Text="PRODUCTO"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td class="auto-style20">
                                        <asp:Panel ID="Panel191" runat="server" Width="602px">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td class="auto-style38">
                                                <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                                                    <Triggers>
                                                      <asp:AsyncPostBackTrigger ControlID="ImgBtnLimpiar" EventName="Click" />
                                                  </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txt_Producto" runat="server" CssClass="auto-style31" Enabled="False" placeholder="Selecciona un producto" Width="530px"></asp:TextBox>
                                                    </ContentTemplate>
                                              </asp:UpdatePanel>
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="RFV_PRODUCTO" runat="server" ControlToValidate="txt_Producto" CssClass="alert-warning" ErrorMessage="*" Font-Size="Medium"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                                                                           
                                                        <asp:Panel ID="pnlAddProd" runat="server">
                                                   <%--         <a id="popup" data-toggle="modal" data-target="#myModal"/>--%>
                                                            <asp:ImageButton ID="ImgBtnBuscar" runat="server" CausesValidation="False" ImageUrl="~/Img/flechaArriba.jpg" Width="30px" />
                                                        </asp:Panel>                                                                                                           
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                    <td class="auto-style21">
                                        
                                        <asp:Panel ID="pnlLimpiar" runat="server">
                                            <asp:ImageButton ID="ImgBtnLimpiar" runat="server" CausesValidation="False" ImageUrl="~/Img/Eliminar.jpg" Width="30px" />
                                        </asp:Panel>
                                        
                                    </td>
                                    <td class="auto-style21"></td>
                                </tr>
                                <tr>
                                    <td class="auto-style19">
                                        <asp:Panel ID="Panel209" runat="server">
                                            <asp:Label ID="Label228" runat="server" CssClass="col-md-2 control-label" Text="ORIGEN"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td class="auto-style20">
                                        <asp:Panel ID="Panel210" runat="server" Width="594px">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td class="auto-style72">
                                                         <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                                                    <Triggers>
                                                      <asp:AsyncPostBackTrigger ControlID="ImgBtnLimpiar" EventName="Click" />
                                                  </Triggers>
                                                    <ContentTemplate>
                                                        <asp:Panel ID="Panel211" runat="server">
                                                            <asp:TextBox ID="txt_Origen" runat="server" CssClass="form-control" placeholder="Origen Producto" Width="500px" Enabled="False" Height="40px" TextMode="MultiLine"></asp:TextBox>
                                                        </asp:Panel>
                                                        </ContentTemplate>
                                              </asp:UpdatePanel>
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="RFV_ORIGEN" runat="server" ControlToValidate="txt_Origen" CssClass="alert-warning" ErrorMessage="*" Font-Size="Medium"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                    <td class="auto-style21">&nbsp;</td>
                                    <td class="auto-style21">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style16">
                                        <asp:Panel ID="Panel3" runat="server">
                                            <asp:Label ID="Label2" runat="server" CssClass="col-md-2 control-label" Text="CANTIDAD"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td class="auto-style14">
                                        <asp:Panel ID="Panel4" runat="server" Width="594px">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td class="auto-style37">
                                                        <asp:TextBox ID="txt_Cantidad" runat="server" placeholder="Agrega Cantidad" CssClass="form-control" Width="300px"></asp:TextBox>
                                                    </td>
                                                    <td class="auto-style36">
                                                        <asp:RequiredFieldValidator ID="RFV_CANT" runat="server" ControlToValidate="txt_Cantidad" CssClass="alert-warning" ErrorMessage="*" Font-Size="Medium"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                    <td class="auto-style17">
                                        <asp:Panel ID="Panel154" runat="server">
                                            <asp:RegularExpressionValidator ID="RFVCANNUM" runat="server" ControlToValidate="txt_Cantidad" CssClass="alert-warning" ErrorMessage="*Ingrese Valores Numericos" ForeColor="Red" ValidationExpression="\d*\.?\d*"></asp:RegularExpressionValidator>
                                        </asp:Panel>
                                    </td>
                                    <td class="auto-style17"></td>
                                </tr>
                                <tr>
                                    <td class="auto-style19">
                                        <asp:Panel ID="Panel14" runat="server">
                                            <asp:Label ID="Label7" runat="server" CssClass="col-md-2 control-label" Text="PESO"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td class="auto-style20">
                                        <asp:Panel ID="Panel15" runat="server" Width="600px">
                                            <table class="auto-style35">
                                                <tr>
                                                    <td class="auto-style30">
                                                        <asp:TextBox ID="txt_Peso" runat="server" CssClass="form-control" placeholder="Peso (Kg)" Width="300px"></asp:TextBox>
                                                    </td>
                                                    <td class="auto-style34">
                                                        <asp:RequiredFieldValidator ID="RFV_PESO" runat="server" ControlToValidate="txt_Peso" CssClass="alert-warning" ErrorMessage="*" Font-Size="Medium"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                    <td class="auto-style21">
                                        <asp:Panel ID="Panel155" runat="server">
                                            <asp:RegularExpressionValidator ID="RFVPESONUM" runat="server" ControlToValidate="txt_Peso" CssClass="alert-warning" ErrorMessage="*Ingrese Valores Numericos" ForeColor="Red" ValidationExpression="\d*\.?\d*"></asp:RegularExpressionValidator>
                                        </asp:Panel>
                                    </td>
                                    <td class="auto-style21"></td>
                                </tr>
                                <tr>
                                    <td class="auto-style22">
                                        <asp:Panel ID="Panel150" runat="server">
                                            <asp:Label ID="Label20" runat="server" CssClass="col-md-2 control-label" Text="DENSIDAD"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td class="auto-style23">
                                        <asp:Panel ID="Panel151" runat="server" Width="601px">
                                                       <table class="auto-style33">
                                                           <tr>
                                                               <td class="auto-style29">
                                                                   <asp:TextBox ID="txt_Densidad" runat="server" CssClass="form-control" placeholder="Densidad" Width="300px"></asp:TextBox>
                                                               </td>
                                                               <td>
                                                                   <asp:RequiredFieldValidator ID="RFV_DENSIDAD" runat="server" ControlToValidate="txt_Densidad" CssClass="alert-warning" ErrorMessage="*" Font-Size="Medium"></asp:RequiredFieldValidator>
                                                               </td>
                                                               <td>&nbsp;</td>
                                                           </tr>
                                                       </table>
                                        </asp:Panel>
                                    </td>
                                    <td class="auto-style25">
                                        <asp:Panel ID="Panel156" runat="server">
                                            <asp:RegularExpressionValidator ID="RFVDENSIDNUM" runat="server" ControlToValidate="txt_Densidad" CssClass="alert-warning" ErrorMessage="*Ingrese Valores Numericos" ForeColor="Red" ValidationExpression="\d*\.?\d*"></asp:RegularExpressionValidator>
                                        </asp:Panel>
                                    </td>
                                    <td class="auto-style25"></td>
                                </tr>
                                <tr>
                                    <td class="auto-style26">
                                        <asp:Panel ID="Panel188" runat="server">
                                            <asp:Label ID="Label220" runat="server" CssClass="col-md-2 control-label" Text="OBSERVACIONES"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td class="auto-style27">
                                        <asp:Panel ID="Panel189" runat="server" Width="456px">
                                            <asp:TextBox ID="txt_Observaciones" runat="server" placeholder="Observación"  CssClass="form-control" Height ="100" Width="300px" TextMode="MultiLine"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                    <td class="auto-style28"></td>
                                    <td class="auto-style28"></td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Panel ID="Panel5" runat="server">
                                            <table style="width:100%;">
                                                <tr>
                                                    <td style="width: 157px">&nbsp;</td>
                                                    <td class="auto-style11">
                                                        <asp:ImageButton ID="ImgBtnAceptar" runat="server" ImageUrl="~/Img/Acept-form.png" Width="30px" />
                                                        <asp:Label ID="Label218" runat="server" Text="Guardar"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="ImgBtnCancelar" runat="server" CausesValidation="False" ImageUrl="~/Img/Cancel-form.png" Width="30px" />
                                                        <asp:Label ID="Label219" runat="server" Text="Cancelar"></asp:Label>
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
            <table class="nav-justified">
                <tr>
                    <td>&nbsp;</td>
                    <td style="width: 19px">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Panel ID="Panel192" runat="server">
                            <table style="width:100%;">
                                <tr>
                                    <td colspan="6">
                                        <asp:Panel ID="Panel208" runat="server">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td class="auto-style59">&nbsp;</td>
                                                    <td>
                                                        <asp:Label ID="Label227" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#0033CC" Text="Encabezado Salida"></asp:Label>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style67"></td>
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
                                    <td class="auto-style67"></td>
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
                                    <td class="auto-style66">&nbsp;</td>
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
                                    <td class="auto-style66">&nbsp;</td>
                                    <td class="auto-style42">&nbsp;</td>
                                    <td class="auto-style61">&nbsp;</td>
                                    <td class="auto-style63">&nbsp;</td>
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
                    <td colspan="4">
                        <asp:Panel ID="Panel130" runat="server">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 52px">&nbsp;</td>
                                    <td style="width: 26px">&nbsp;</td>
                                    <td style="width: 49px">&nbsp;</td>
                                    <td class="auto-style39">&nbsp;</td>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#0000CC" Text="PRODUCTOS EN LA SALIDA"></asp:Label>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 52px">&nbsp;</td>
                                    <td style="width: 26px">&nbsp;</td>
                                    <td style="width: 49px">
                                        &nbsp;</td>
                                    <td class="auto-style39">&nbsp;</td>
                                    <td>
                                        
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
                        <asp:Label ID="lbkeyProd" runat="server" Visible="False"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td style="width: 19px">
                        &nbsp;</td>
                    <td class="auto-style41">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" EmptyDataText="No Existen Registros." CellPadding="4" DataKeyNames="SalidaSemillaIDF,ProductoID,ProductoLote"  Font-Size="XX-Small" ForeColor="#333333" GridLines="None" ShowFooter="True" AllowPaging="True" CssClass="table table-condensed">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:CommandField InsertVisible="False" ShowSelectButton="True" />
                               <%-- <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton Text="Ver" ID="lnkView" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                               <%-- <asp:CommandField CausesValidation="False" InsertVisible="False" ShowEditButton="True" />--%>
                                <asp:BoundField DataField="SalidaSemillaIDF" HeaderText="ID" ItemStyle-CssClass="SalidaSemillaIDF"/>
                                <asp:BoundField DataField="ProductoNombre" HeaderText="Producto" ItemStyle-CssClass="ProductoNombre"/>
                                <asp:BoundField DataField="Origen" HeaderText="Origen" ItemStyle-CssClass="Origen"/>
                                <asp:BoundField DataField="SalidaSemillaCantidad" HeaderText="Cantidad" ItemStyle-CssClass="SalidaSemillaCantidad"/>
                                <asp:BoundField DataField="SalidaSemillaPeso" HeaderText="Peso" ItemStyle-CssClass="SalidaSemillaPeso"/>
                                <asp:BoundField DataField="SalidaSemillaDensidad" HeaderText="Densidad" ItemStyle-CssClass="SalidaSemillaDensidad"/>
                                <asp:BoundField DataField="SalidaSemillaObservaciones" HeaderText="Observaciones" ItemStyle-CssClass="SalidaSemillaObservaciones"/>
                                 <%--  <asp:TemplateField headerText="Eliminar" HeaderStyle-ForeColor="white">
                                        <ItemTemplate>
                                        <a id="popup" data-toggle="modal" data-target="#myModal2"/>
                                            <asp:ImageButton ID="ImgBtnDelete" CommandName="Delete" runat="server" Height="15px" 
                                                ImageUrl="~/Img/Cancel-form.png" Width="15px" />
                                        </ItemTemplate>                                        
                              </asp:TemplateField>--%>
                             <%-- <asp:TemplateField>
                                        <ItemTemplate>
                                             <a id="popup" data-toggle="modal" data-target="#myModal2"/>
                                            <asp:LinkButton ID="lnkBtnDelete" runat="server" CommandName="Delete">x</asp:LinkButton>
                                        </ItemTemplate>
                              </asp:TemplateField>--%>
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
                        &nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </asp:Panel> 


         <asp:Panel ID="PanelModal" runat="server">
     <%-- <table style="width:100%;">--%>
  <%--<asp:UpdatePanel ID="UpdatePanel4" runat="server">--%>
     <div class="modal fade" id="myModal" role="dialog" style="height: 800px; width: 880px;">
 
<div class="modal-dialog" style="height: 600px; width: 800px;">
      <!-- Modal content-->
 
<div class="modal-content"  style="float: left; width: 800px;"> 
 <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="always" runat="server">
 <ContentTemplate>
<div class="modal-header">
          <caption>
              <button class="close" data-dismiss="modal" type="button">
                  ×
              </button>
              <h4 class="modal-title">
                  <asp:Panel ID="Panel6" runat="server" Width="415px">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td class="auto-style10">
                                                       <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="Small" Text="Producto"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSearch_Producto" runat="server" placeholder="Buscar Producto" CssClass="form-control" AutoPostBack="True" Width="418px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>                 
                  <h4></h4>
                  <h4></h4>
                  <h4></h4>
                  <h4></h4>
                  <h4></h4>
                  <h4></h4>
                  <h4></h4>
              </h4>
          </caption>
 
       </div>
               <%-- </ContentTemplate>--%>
     
 
<div class="modal-body" style="float: left; display: block;">
                        &nbsp;<asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" 
                            CellPadding="4"  DataKeyNames="ProductoID" 
                            EmptyDataText="No se encontraron registros para los criterios de búsqueda ingresados." 
                            EnableModelValidation="True" Font-Size="Medium" CssClass="gridview" ForeColor="#333333" 
                            GridLines="None" ShowHeaderWhenEmpty="true" Width="740px" AllowPaging="True" PageSize="7">
                            <RowStyle BackColor="#EFF3FB" />
                            <EditRowStyle BackColor="#CEECF5" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:CommandField InsertVisible="False" ShowSelectButton="True" 
                                    Visible="true" />
                                <asp:BoundField DataField="ProductoID" HeaderText="No." ReadOnly="True"/>
                                <asp:BoundField DataField="ProductoNombre" HeaderText="Nombre" ReadOnly="True" />
                                <asp:BoundField DataField="ProductoLote" HeaderText="Lote" ReadOnly="True" />
                                 <asp:BoundField DataField="InventarioLogico" HeaderText="Cantidad" ReadOnly="True" />
                                 <asp:BoundField DataField="Origen" HeaderText="Origen" ReadOnly="True" />
                            </Columns>
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#FFE4C4" Font-Bold="True" ForeColor="#333333" />
                        </asp:GridView>
                         </ContentTemplate>
                    <Triggers>
                      <asp:AsyncPostBackTrigger ControlID="txtSearch_Producto" EventName="TextChanged" />
                  </Triggers>
              </asp:UpdatePanel>
                    &nbsp;</div>
 
 <div class="modal-footer">
     <asp:Label ID="lblProductoID" runat="server" Visible="False"></asp:Label>
          <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
         <%-- <button id="btnAgregar" type="button" class="btn btn-primary" onserverclick="btnAgregar">Agregar</button>--%>
          <asp:Button ID="btnAgregar" runat="server" CausesValidation ="false" class="btn btn-primary" Text="Agregar" />
        </div>
 
      </div>
 
    </div>
 
  <%--</div>--%>


              <%--<asp:PlaceHolder ID="ControlContainer" runat="server"/>--%>
                                     
     </asp:Panel>
     
        </div>




</asp:Content>