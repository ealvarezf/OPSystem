<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="DetalleConteo.aspx.vb" Inherits="OPSystem.DetalleConteo" %>
<%@ Register src="~/ControlUser/BarEventos.ascx" tagname="BarEventos" tagprefix="uc1" %>
<%@ Register src="~/ControlUser/BarEventosOld.ascx" tagname="BarEventosOld" tagprefix="uc2" %>
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
        .auto-style1 {
            width: 260px;
        }
        .auto-style2 {
            width: 350px;
        }
        .auto-style3 {
            width: 244px;
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

     <%--Termina .modal content--%>

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
                                    <td style="height: 49px; width: 83px">
                                        <asp:Panel ID="Panel141" runat="server" Width="133px">
                                        </asp:Panel>                                        
                                    </td>
                                    <td style="height: 49px">
                                        <asp:Panel ID="pnl_Search_check" runat="server" Width="662px">
                                            <asp:CheckBox ID="CheckSearch_Invent" runat="server" ForeColor="#6600CC" Text="Considerar solo Inventario." />
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 49px"></td>
                                    <td style="height: 49px"></td>
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
                                    <td style="width: 43px">
                                        &nbsp;</td>
                                    <td style="width: 257px">&nbsp;</td>
                                    <td>
                                        <asp:Panel ID="pnlAcción" runat="server">
                                            <asp:Label ID="lblAcción"  runat="server" Font-Bold="True" ForeColor="#5D7B9D"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 43px">&nbsp;</td>
                                    <td style="width: 257px">
                                        <asp:FileUpload ID="FileUpload1" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPlantilla" runat="server" Enabled="False" Width="337px"></asp:TextBox>
                                        <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="CustomValidator"></asp:CustomValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 43px">&nbsp;</td>
                                    <td style="width: 257px">&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 43px">&nbsp;</td>
                                    <td colspan="2">
                                        <asp:Panel ID="Panel151" runat="server">
                                            <table class="nav-justified">
                                                <tr>
                                                    <td style="width: 142px">&nbsp;</td>
                                                    <td style="width: 180px">
                                                        <asp:ImageButton ID="btnUpload" runat="server" Height="52px" ImageUrl="~/Img/upload2.jpg" Width="57px" />
                                                    </td>
                                                    <td style="width: 316px">
                                                        <asp:ImageButton ID="ImgBtnCancelar" runat="server" CausesValidation="False" ImageUrl="~/Img/Cancel-form.png" Width="30px" />
                                                        <asp:Label ID="Label20" runat="server" Text="Cancelar"></asp:Label>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 43px">&nbsp;</td>
                                    <td colspan="2">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 43px">&nbsp;</td>
                                    <td colspan="2">
                                        <asp:Panel ID="Panel150" runat="server">
                                            <asp:Label ID="Label66" runat="server" Font-Bold="True" Text="NOTA:"></asp:Label>
                                            <asp:Label ID="Label67" runat="server" Text="Solo Podrá Adjuntar Extensiones  &quot;.txt&quot;   (Texto Delimitado por tabulaciones)"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 43px">&nbsp;</td>
                                    <td style="width: 257px">&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Panel ID="pnlLoading" runat="server">
                                            <table style="width:100%;">
                                                <tr>
                                                    <td style="width: 92px">&nbsp;</td>
                                                    <td style="width: 343px">
                                                        <asp:Panel ID="Panel153" runat="server" Width="331px">
                                                            <asp:Image ID="imgloading" runat="server" Height="149px" ImageUrl="~/Img/4F2.gif" Width="145px" />
                                                        </asp:Panel>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 43px">&nbsp;</td>
                                    <td style="width: 257px">&nbsp;</td>
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

                 <asp:Panel ID="pnlEncabezado" runat="server">
                            <table style="width:100%;">
                                <tr>
                                    <td colspan="6">
                                        <asp:Panel ID="Panel208" runat="server">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td class="auto-style2">&nbsp;</td>
                                                    <td>
                                                        <asp:Label ID="Label227" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#0033CC" Text="Encabezado Conteo"></asp:Label>
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
                                            <asp:Label ID="Label221" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="#0066CC" Text="No. Conteo"></asp:Label>
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
                                            <asp:Label ID="Label222" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="#0066CC" Text="Empresa"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td class="auto-style69">
                                        <asp:Panel ID="Panel206" runat="server" Width="261px">
                                            <asp:Label ID="lblEmpresa" runat="server" Font-Size="Small" ForeColor="Red"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td class="auto-style70">
                                        <asp:Panel ID="Panel200" runat="server" Width="120px">
                                            <asp:Label ID="Label225" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="#0066CC" Text="Almacen"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td class="auto-style71">
                                        <asp:Panel ID="Panel203" runat="server" Width="281px">
                                            <asp:Label ID="lblAlmacen" runat="server" Font-Size="Small" ForeColor="Red"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td class="auto-style71"></td>
                                </tr>
                                <tr>
                                    <td class="auto-style66">&nbsp;</td>
                                    <td class="auto-style42">
                                        <asp:Panel ID="Panel195" runat="server" Width="123px">
                                            <asp:Label ID="Label223" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="#0066CC" Text="Descripción"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td class="auto-style61">
                                        <asp:Panel ID="Panel207" runat="server" Width="255px">
                                            <asp:Label ID="lblDescripcion" runat="server" Font-Size="Small" ForeColor="Red"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td class="auto-style63">
                                        <asp:Panel ID="Panel201" runat="server" Width="123px">
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel204" runat="server" Width="280px">
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

        <asp:Panel ID="pnlListar" runat="server">
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
                                    <td class="auto-style3">&nbsp;</td>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="#0000CC" Text="PRODUCTOS"></asp:Label>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 52px">&nbsp;</td>
                                    <td style="width: 26px">&nbsp;</td>
                                    <td style="width: 49px">
                                        &nbsp;</td>
                                    <td class="auto-style3">&nbsp;</td>
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
                      <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" EmptyDataText="No Existen Registros." CellPadding="4" DataKeyNames="ConteoID" Font-Size="XX-Small" ForeColor="#333333" GridLines="None" ShowFooter="True" AllowPaging="True" PageSize="20" CssClass="table table-condensed">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                           <Columns>
                                <asp:CommandField InsertVisible="False" ShowSelectButton="True" />
                                <asp:BoundField DataField="ConteoID" HeaderText="ID" />                               
                                <asp:BoundField DataField="EmpresaNombre" HeaderText="Descripción" ItemStyle-HorizontalAlign="Center">
                                        <FooterStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                                        <HeaderStyle HorizontalAlign="Left" Width="20px" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" Width="100px" Wrap="False" />
                                </asp:BoundField>                                                              
                                <asp:BoundField DataField="UbicacionID" HeaderText="ID Almacen"/>
                                <asp:BoundField DataField="UbicacionNombre" HeaderText="Almacen" ItemStyle-HorizontalAlign="Center" >
                                        <FooterStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                                        <HeaderStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" Width="100px" Wrap="False" />
                                </asp:BoundField>   
                               <asp:BoundField DataField="ProductoID" HeaderText="ID Producto"/>
                                <asp:BoundField DataField="ProductoNombre" HeaderText="Producto" ItemStyle-HorizontalAlign="Center" >
                                        <FooterStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                                        <HeaderStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" Width="100px" Wrap="False" />
                                </asp:BoundField> 
                                <asp:BoundField DataField="EnvasePeso" HeaderText="Peso"/>
                               <asp:BoundField DataField="TipoPrdNombre" HeaderText="Tipo"/>
                               <asp:BoundField DataField="MarcaNombre" HeaderText="Marca"/>
                               <asp:BoundField DataField="ProductoLote" HeaderText="Lote"/>
                               <asp:BoundField DataField="Origen" HeaderText="Origen"/>
                               <asp:BoundField DataField="ConteoLogico" HeaderText="Cantidad Inv"/> 
                               <asp:BoundField DataField="ConteoStatus" HeaderText="Status"/> 
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
                            <tr>
                                <td colspan="4" style="height: 22px">
                                       <asp:Panel ID="pnlBotones" runat="server">
                                                              <table style="width:100%;">
                                                                  <tr>
                                                                      <td class="auto-style62" style="width: 295px">&nbsp;</td>
                                                                      <td class="auto-style59">
                                                                          <asp:Panel ID="Panel186" runat="server" Width="225px">
                                                                              <asp:ImageButton ID="btnCancelarF" runat="server" CausesValidation="False" Height="38px" ImageUrl="~/Img/Cancel-form.png" Width="40px" Visible="False" />
                                                                              <asp:Label ID="Label216" runat="server" Font-Bold="True" ForeColor="#CC0000" Text="Cancelar" Visible="False"></asp:Label>
                                                                          </asp:Panel>
                                                                      </td>
                                                                      <td class="auto-style60">
                                                                          <asp:Panel ID="Panel187" runat="server" Width="190px">
                                                                              <asp:ImageButton ID="imgbtnNext" runat="server" Height="47px" ImageUrl="~/Img/Continuar.png" Width="50px" />
                                                                              <asp:Label ID="Label217" runat="server" Font-Bold="True" ForeColor="#3366CC" Text="Siguiente"></asp:Label>
                                                                          </asp:Panel>
                                                                      </td>
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
                <tr>
                    <td>&nbsp;</td>
                    <td style="width: 19px">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </asp:Panel> 


                       <asp:Panel ID="pnlListar2" runat="server" Width="846px">
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
                                                          <td style="width: 390px">&nbsp;</td>
                                                          <td style="width: 349px">
                                                              <asp:Label ID="Label215" runat="server" Font-Bold="True" Text="Productos A Modificar"></asp:Label>
                                                          </td>
                                                          <td>&nbsp;</td>
                                                      </tr>
                                                  </table>
                                              
                                              </asp:Panel>
                                            </div>
                                          <div class="panel-body">
                                              <asp:Panel ID="Panel183" runat="server">
                                              <table style="width:100%;">
                                                  <tr>
                                                      <td colspan="4">
                                                      <asp:Panel ID="pnlEv2" runat="server">
                                                        <%-- <uc1:BarEventos ID="BarEventos2" runat="server" />--%>
                                                          <uc2:BarEventosOld ID ="BarEventosOld" runat="server" />
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
                                                      <td class="auto-style49" colspan="5">
                                                          <asp:Panel ID="pnlConfirmarInv" runat="server">
                                                              <table style="width:100%;">
                                                                  <tr>
                                                                      <td style="width: 306px">&nbsp;</td>
                                                                      <td class="auto-style1">
                                                                          <asp:Panel ID="Panel188" runat="server">
                                                                              <a id="popup" data-toggle="modal" data-target="#myModal2"/>
                                                                              <asp:ImageButton ID="btnTransferir" runat="server" Height="52px" ImageUrl="~/Img/importar.jpg" Width="85px" />
                                                                          </asp:Panel>
                                                                      </td>
                                                                      <td>
                                                                          <asp:ImageButton ID="btnCancelarExp" runat="server" Height="33px" ImageUrl="~/Img/Cancel-form.png" Width="47px" />
                                                                      </td>
                                                                  </tr>
                                                              </table>
                                                          </asp:Panel>
                                                      </td>
                                                  </tr>
                                                  <tr>
                                                      <td class="auto-style49">&nbsp;</td>
                                                      <td class="auto-style70">&nbsp;</td>
                                                      <td>&nbsp;</td>
                                                      <td class="auto-style56">&nbsp;</td>
                                                      <td>&nbsp;</td>
                                                  </tr>
                                                  <tr>
                                                      <td class="auto-style49" colspan="4">
                                                          <asp:Panel ID="pnlGridRegitrosFiltrados" runat="server" >
                                                              <table style="width:100%;">
                                                                  <tr>
                                                                      <td>&nbsp;</td>
                                                                      <td>
                                                                         <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" EmptyDataText="No hay registros." CssClass="gridview" DataKeyNames="ConteoID,ProductoID,ProductoLote" GridLines="None" ShowFooter="True" CellPadding="4" CellSpacing="2" Font-Size="XX-Small" ForeColor="#333333">
                                                                              <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                              <Columns>
                                                                                <%-- <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton Text="Ver" ID="lnkView" runat="server" />
                                                                                    </ItemTemplate>
                                                                                 </asp:TemplateField>--%>
                                                                                 <asp:BoundField DataField="ConteoID" HeaderText="ID" ItemStyle-Width="100px"/>
                                                                                <asp:BoundField DataField="EmpresaID" HeaderText="ID Empresa" ItemStyle-Width="100px"/>
                                                                                <asp:BoundField DataField="EmpresaNombre" HeaderText="Empresa" ItemStyle-Width="100px"/>
                                                                                <asp:BoundField DataField="UbicacionID" HeaderText="ID Almacen" ItemStyle-Width="100px"/>
                                                                                <asp:BoundField DataField="UbicacionNombre" HeaderText="Almacen" ItemStyle-Width="100px" />
                                                                                <asp:BoundField DataField="TipoPrdNombre" HeaderText="Tipo Producto" ItemStyle-Width="100px"/>
                                                                                <asp:BoundField DataField="ProductoID" HeaderText="ID Producto" ItemStyle-Width="100px"/>
                                                                                <asp:BoundField DataField="ProductoNombre" HeaderText="Producto" ItemStyle-Width="100px"/>
                                                                                <asp:BoundField DataField="EnvasePeso" HeaderText="Peso" ItemStyle-Width="100px"/>
                                                                                <asp:BoundField DataField="MarcaNombre" HeaderText="Marca" ItemStyle-Width="100px"/>
                                                                                <asp:BoundField DataField="ProductoLote" HeaderText="Lote" ItemStyle-Width="100px"/>
                                                                                  <asp:BoundField DataField="Origen" HeaderText="Origen" ItemStyle-Width="100px"/>
                                                                                <asp:BoundField DataField="ConteoLogico" HeaderText="Cantidad Inventario" ItemStyle-Width="100px"/>                                    
                                                                                <asp:BoundField DataField="InventarioUbicacionPasillo" HeaderText="Ubicacion" ItemStyle-Width="100px"/>
                                                                                <asp:BoundField DataField="ConteoStatus" HeaderText="Status" ItemStyle-Width="100px"/>                                                                               
                                                                                <asp:TemplateField HeaderText="Eliminar" >
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lnkBtnDelete" CausesValidation ="false" runat="server" CommandName="Delete">Eliminar</asp:LinkButton>
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
                                                      <td class="auto-style49" colspan="4">
                                                            <asp:Panel ID="pnlGridRegistrosDetalle" runat="server" >
                                                              <table style="width:100%;">
                                                                  <tr>
                                                                      <td>&nbsp;</td>
                                                                      <td>
                                                                         <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" EmptyDataText="No hay registros." CssClass="gridview" DataKeyNames="ConteoID,ProductoID,ProductoLote" GridLines="None" ShowFooter="True" CellPadding="4" CellSpacing="2" Font-Size="XX-Small" ForeColor="#333333">
                                                                              <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                              <Columns>                                                                               
                                                                                 <asp:BoundField DataField="ConteoID" HeaderText="ID" ReadOnly="True"  ItemStyle-Width="100px"/>
                                                                                <asp:BoundField DataField="EmpresaID" HeaderText="ID Empresa" ReadOnly="True"  ItemStyle-Width="100px"/>
                                                                                <asp:BoundField DataField="EmpresaNombre" HeaderText="Empresa" ReadOnly="True"  ItemStyle-Width="100px"/>
                                                                                <asp:BoundField DataField="UbicacionID" HeaderText="ID Almacen" ReadOnly="True"  ItemStyle-Width="100px"/>
                                                                                <asp:BoundField DataField="UbicacionNombre" HeaderText="Almacen" ReadOnly="True"  ItemStyle-Width="100px" />
                                                                                <asp:BoundField DataField="TipoPrdNombre" HeaderText="Tipo Producto" ReadOnly="True" ItemStyle-Width="100px"/>
                                                                                <asp:BoundField DataField="ProductoID" HeaderText="ID Producto" ReadOnly="True"  ItemStyle-Width="100px"/>
                                                                                <asp:BoundField DataField="ProductoNombre" HeaderText="Producto" ReadOnly="True"  ItemStyle-Width="100px"/>
                                                                                <asp:BoundField DataField="EnvasePeso" HeaderText="Peso" ReadOnly="True"  ItemStyle-Width="100px"/>
                                                                                <asp:BoundField DataField="MarcaNombre" HeaderText="Marca" ReadOnly="True" ItemStyle-Width="100px"/>
                                                                                <asp:BoundField DataField="ProductoLote" HeaderText="Lote" ReadOnly="True" ItemStyle-Width="100px"/>
                                                                                <asp:BoundField DataField="Origen" HeaderText="Origen" ReadOnly="True" ItemStyle-Width="100px"/>
                                                                                <asp:BoundField DataField="ConteoLogico" HeaderText="Cantidad Inventario" ItemStyle-Width="100px"/>                                    
                                                                                <asp:BoundField DataField="InventarioUbicacionPasillo" HeaderText="Ubicacion" ItemStyle-Width="100px"/>
                                                                                <asp:BoundField DataField="ConteoStatus" HeaderText="Status" ReadOnly="True" ItemStyle-Width="100px"/>                                                                               
                                                                                <asp:CommandField InsertVisible="False" ShowEditButton="True" 
                                                                                    CausesValidation="False" />  
                                                                              </Columns>
                                                                              <EditRowStyle BackColor="#999999" />
                                                                              <FooterStyle BackColor="#FF1493" Font-Bold="True" ForeColor="White" />
                                                                              <HeaderStyle BackColor="#FF1493" Font-Bold="True" ForeColor="White" />
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
                                              </table>
                                              </asp:Panel>
                                            </div>
                                        </div>
                                   <%--<asp:Panel ID="Panel183" runat="server" style="margin-left: 26px">

                                   </asp:Panel>--%>
                                    </div>
                               </div>
                           </td>
                           <td>&nbsp;</td>
                           <td>&nbsp;</td>
                       </tr>
                   </table>

                </asp:Panel>



            <%--Termina .modal content--%>



        </div>






</asp:Content>