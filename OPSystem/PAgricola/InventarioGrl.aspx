<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="InventarioGrl.aspx.vb" Inherits="OPSystem.InventarioGrl" %>
<%@ Register src="~/ControlUser/BarEventos.ascx" tagname="BarEventos" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="../CSS/jquery-ui.css"  rel="stylesheet" type="text/css" />
    <script src="../JS/jquery-1.9.1.js"></script>
    <script src="../JS/jquery-ui.js"></script>
    <script src="../UI/i18n/datepicker-es.js"></script>

     <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
    rel="stylesheet" type="text/css" />

      <%--Agregar Estas referencias al final de todas, con la finalidad de que respete el diseño de los tabs y el calendario en español.--%>
     <%--Trae el diseño del calendario.--%>
    <script src="../UI/i18n/datepicker-es.js"></script>

    <script>
        $(function () {
            $("#MainContent_txt_PersonalFecAlta").datepicker($.datepicker.regional["es"]);
        });
    </script>

     <script>
         $(function () {
             $("#MainContent_txtSearch_Fecini").datepicker($.datepicker.regional["es"]);
         });
    </script>

     <script>
         $(function () {
             $("#MainContent_txtSearch_Fecfin").datepicker($.datepicker.regional["es"]);
         });
    </script>

        <div class="panel-body">
        <asp:Panel ID="pnlEventos" runat="server">
            <uc1:BarEventos ID="BarEventos1" runat="server" />
        </asp:Panel>
        <asp:Panel ID="pnlFiltros" runat="server">
            <%-- <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
            <Triggers>              
                 <asp:AsyncPostBackTrigger ControlID="imgbtnCancelaFiltro" EventName="Click" />
            </Triggers>
            <ContentTemplate>--%>
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
                                        <asp:Panel ID="Panel138" runat="server" Width="133px">
                                            <asp:Label ID="Label18" runat="server" Font-Bold="True" ForeColor="#6600CC" Text="Empresa:" Font-Size="Medium"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 57px">
                                        <asp:Panel ID="Panel147" runat="server">
                                            <asp:TextBox ID="txtSearch_Emp" runat="server" CssClass="form-control" Height="28px" Width="300px"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 57px"></td>
                                    <td style="height: 57px"></td>
                                </tr>
                                <tr>
                                    <td style="height: 54px; width: 83px">
                                        <asp:Panel ID="Panel139" runat="server" Width="133px">
                                            <asp:Label ID="Label8" runat="server" Font-Bold="True" ForeColor="#6600CC" Text="Almacen:" Font-Size="Medium"></asp:Label>
                                        </asp:Panel>                                        
                                    </td>
                                    <td style="height: 54px">
                                        <asp:Panel ID="Panel142" runat="server">
                                            <asp:TextBox ID="txtSearch_Alm" runat="server" CssClass="form-control" Height="30px" Width="300px"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 54px"></td>
                                    <td style="height: 54px"></td>
                                </tr>
                                <tr>
                                    <td style="height: 53px; width: 83px">
                                        <asp:Panel ID="Panel140" runat="server" Width="133px">
                                            <asp:Label ID="Label17" runat="server" Font-Bold="True" ForeColor="#6600CC" Text="Producto:" Font-Size="Medium"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 53px">
                                        <asp:Panel ID="Panel143" runat="server">
                                            <asp:TextBox ID="txtSearch_Prod" runat="server" CssClass="form-control" Height="30px" Width="300px"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 53px"></td>
                                    <td style="height: 53px"></td>
                                </tr>
                                <tr>
                                    <td style="height: 54px; width: 83px">
                                        <asp:Panel ID="Panel141" runat="server" Width="133px">
                                            <asp:Label ID="Label19" runat="server" Font-Bold="True" ForeColor="#6600CC" Text="Lote:" Font-Size="Medium"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 54px">
                                        <asp:Panel ID="Panel144" runat="server" Width="662px">
                                            <asp:TextBox ID="txtSearch_Lote" runat="server" CssClass="form-control" Height="30px" Width="300px"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 54px"></td>
                                    <td style="height: 54px"></td>
                                </tr>
                                <tr>
                                    <td style="height: 49px; width: 83px">
                                        <asp:Panel ID="Panel154" runat="server" Width="133px">
                                            <asp:Label ID="Label68" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#6600CC" Text="Entre:"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 49px">
                                        <asp:Panel ID="Panel155" runat="server">
                                            <table style="width:100%;">
                                                <tr>
                                                    <td style="width: 182px">
                                                        <asp:TextBox ID="txtSearch_Fecini" runat="server" CssClass="form-control" Height="30px" Width="153px"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 23px">
                                                        <asp:Label ID="Label69" runat="server" Font-Bold="True" Text="Y"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSearch_Fecfin" runat="server" CssClass="form-control" Height="30px" Width="153px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 49px">&nbsp;</td>
                                    <td style="height: 49px">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="height: 37px; width: 83px">
                                        <asp:Panel ID="Panel148" runat="server" Width="133px">
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 37px">
                                        <asp:Panel ID="Panel149" runat="server" Visible="False">
                                            <asp:CheckBox ID="CheckSearch_Invent" runat="server" ForeColor="#6600CC" Text="Considerar solo Inventario." />
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 37px">&nbsp;</td>
                                    <td style="height: 37px">&nbsp;</td>
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
                                                    <td style="width: 99px">&nbsp;</td>
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
            <%-- </ContentTemplate>
        </asp:UpdatePanel>--%>
        </asp:Panel>

     <asp:Panel ID="pnlAdd" runat="server">
                  <%--<asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Always" runat="server">
            <Triggers>              
                 <asp:AsyncPostBackTrigger ControlID="imgBtnAplicaFiltro" EventName="Click" />
                 <asp:AsyncPostBackTrigger ControlID="imgbtnCancelaFiltro" EventName="Click" />
                 <asp:AsyncPostBackTrigger ControlID="ImgBtnAceptar" EventName="Click" />
                 <asp:AsyncPostBackTrigger ControlID="ImgBtnCancelar" EventName="Click" />
            </Triggers>
            <ContentTemplate>--%>
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
                                        <asp:Panel ID="Panel1" runat="server">
                                            <asp:Label ID="Label2" runat="server" CssClass="col-md-2 control-label" Text="FECHA CORTE" Font-Bold="False"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 51px; width: 10px;">
                                        <asp:Panel ID="Panel2" runat="server" Width="416px">
                                            <asp:TextBox ID="txt_Fecha" runat="server" Width="300px" placeholder="Fecha Corte" CssClass="form-control"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 51px">
                                        &nbsp;</td>
                                    <td style="height: 51px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 247px; height: 41px;">
                                        <asp:Panel ID="Panel3" runat="server">
                                            <asp:Label ID="Label1" runat="server" CssClass="col-md-2 control-label" Text="EMPRESA" Font-Bold="False"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 41px; width: 10px;">
                                        <asp:Panel ID="Panel4" runat="server" Width="415px">
                                            <asp:DropDownList ID="DDL_EMPRESA" runat="server" AutoPostBack="True" CssClass="form-control" Height="35px" Width="352px">
                                            </asp:DropDownList>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 41px">
                                        &nbsp;</td>
                                    <td style="height: 41px">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 247px; height: 51px;">
                                        <asp:Panel ID="Panel170" runat="server">
                                            <asp:Label ID="Label21" runat="server" CssClass="col-md-2 control-label" Font-Bold="False" Text="ALMACEN"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 51px; width: 10px;">
                                       
                                        <asp:Panel ID="Panel157" runat="server" Width="415px">                                         
                                                <asp:DropDownList ID="DDL_ALM" runat="server" CssClass="form-control" Height="35px" Width="352px">
                                                </asp:DropDownList>                                           
                                        </asp:Panel>
                                       
                                    </td>
                                    <td style="height: 51px">
                                        &nbsp;</td>
                                    <td style="height: 51px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 247px; height: 51px;">
                                        <asp:Panel ID="Panel162" runat="server">
                                            <asp:Label ID="Label24" runat="server" CssClass="col-md-2 control-label" Font-Bold="False" Text="PRODUCTO"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 51px; width: 10px;">
                                        <asp:DropDownList ID="DDL_PRODUCTO" runat="server" AutoPostBack="True" CssClass="form-control" Height="35px" Width="352px">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="height: 51px">
                                        &nbsp;</td>
                                    <td style="height: 51px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 247px; height: 51px;">
                                        <asp:Panel ID="Panel14" runat="server">
                                            <asp:Label ID="Label7" runat="server" CssClass="col-md-2 control-label" Text="LOTE" Font-Bold="False"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 51px; width: 10px;">
                                        <asp:Panel ID="Panel15" runat="server" Width="414px">
                                            <asp:TextBox ID="txt_Lote" runat="server" CssClass="form-control" placeholder="Lote" Width="300px"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 51px">
                                        &nbsp;</td>
                                    <td style="height: 51px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 247px; height: 51px;">
                                        <asp:Panel ID="Panel150" runat="server">
                                            <asp:Label ID="Label20" runat="server" CssClass="col-md-2 control-label" Text="UBICACIÓN" Font-Bold="False"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td style="width: 10px; height: 51px;">
                                        <asp:Panel ID="Panel151" runat="server" Width="413px">                                                                                            
                                            <asp:TextBox ID="txt_Ubicacion" runat="server" CssClass="form-control" placeholder="Ubicacion" Width="300px"></asp:TextBox>                                                    
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 51px">
                                        &nbsp;</td>
                                    <td style="height: 51px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 247px; height: 51px;">
                                        <asp:Panel ID="Panel158" runat="server">
                                            <asp:Label ID="Label22" runat="server" CssClass="col-md-2 control-label" Font-Bold="False" Text="PESO"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td style="width: 10px; height: 51px;">
                                        <asp:Panel ID="Panel160" runat="server" Width="416px">
                                            <asp:TextBox ID="txt_Peso" runat="server" CssClass="form-control" placeholder="Peso" Width="300px"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 51px">
                                        &nbsp;</td>
                                    <td style="height: 51px"></td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:Panel ID="Panel171" runat="server">
                                            <table style="width:100%;">
                                                <tr>
                                                    <td style="width: 212px">
                                                        <asp:Panel ID="Panel172" runat="server" Width="216px">
                                                            <asp:Label ID="Label70" runat="server" CssClass="col-md-2 control-label" Font-Bold="False" Text="CANTIDAD LÓGICA" ForeColor="#9900FF"></asp:Label>
                                                        </asp:Panel>
                                                    </td>
                                                    <td style="width: 214px">
                                                        <asp:Panel ID="Panel173" runat="server" Width="187px">
                                                            <asp:TextBox ID="txt_CantLogica" runat="server" CssClass="form-control" placeholder="Cantidad Logica" Width="152px"></asp:TextBox>
                                                        </asp:Panel>
                                                    </td>
                                                    <td style="width: 191px">
                                                        <asp:Panel ID="Panel174" runat="server" Width="184px">
                                                            <asp:Label ID="Label71" runat="server" CssClass="col-md-2 control-label" Font-Bold="False" Text="CANTIDAD FISICA" ForeColor="#9900FF"></asp:Label>
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel175" runat="server" Width="187px">
                                                            <asp:TextBox ID="txt_CantFisica" runat="server" CssClass="form-control" placeholder="Cantidad Fisica" Width="152px" ForeColor="#CC0000"></asp:TextBox>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 247px">&nbsp;</td>
                                    <td style="width: 10px">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Panel ID="Panel6" runat="server">
                                            <table style="width:100%;">
                                                <tr>
                                                    <td style="width: 259px">&nbsp;</td>
                                                    <td style="width: 173px">
                                                        <asp:ImageButton ID="ImgBtnAceptar" runat="server" ImageUrl="~/Img/Acept-form.png" Width="30px" />
                                                        <asp:Label ID="Label25" runat="server" Text="Ajustar"></asp:Label>
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
             <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
        </asp:Panel>

        <asp:Panel ID="pnlListar" runat="server">
         <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Always" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="SelectedIndexChanging" />
<%--              <asp:AsyncPostBackTrigger ControlID="BarEventos1_btnNuevo" EventName="Click" />
                 <asp:AsyncPostBackTrigger ControlID="BarEventos1_btnEditar" EventName="Click" />
                 <asp:AsyncPostBackTrigger ControlID="BarEventos1_btnFiltrar" EventName="Click" />
                 <asp:AsyncPostBackTrigger ControlID="BarEventos1_btnEsp3" EventName="Click" />--%>
              <%--   <asp:AsyncPostBackTrigger ControlID="imgBtnAplicaFiltro" EventName="Click" />
                 <asp:AsyncPostBackTrigger ControlID="imgbtnCancelaFiltro" EventName="Click" />
                 <asp:AsyncPostBackTrigger ControlID="ImgBtnAceptar" EventName="Click" />
                 <asp:AsyncPostBackTrigger ControlID="ImgBtnCancelar" EventName="Click" />--%>
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
                                    <td style="width: 232px">&nbsp;</td>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#0000CC" Text="INVENTARIOS GENERALES "></asp:Label>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 52px">&nbsp;</td>
                                    <td style="width: 26px">&nbsp;</td>
                                    <td style="width: 49px">
                                        &nbsp;</td>
                                    <td style="width: 232px">&nbsp;</td>
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
                        <asp:Label ID="lblInventario" runat="server" Visible="False"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td style="width: 19px">
                        &nbsp;</td>
                    <td>
                      <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" EmptyDataText="No Existen Registros." CellPadding="4" DataKeyNames="EmpresaID,UbicacionID,ProductoID,ProductoLote" Font-Size="XX-Small" ForeColor="#333333" GridLines="None" ShowFooter="True" AllowPaging="True" PageSize="50" CssClass="table table-condensed">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                           <Columns>
                                <asp:CommandField InsertVisible="False" ShowSelectButton="True" />
                                <asp:BoundField DataField="EmpresaID" HeaderText="ID EMP" />                               
                                 <asp:BoundField DataField="EmpresaNombre" HeaderText="Empresa" ItemStyle-HorizontalAlign="Center">
                                        <FooterStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                                        <HeaderStyle HorizontalAlign="Left" Width="20px" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" Width="100px" Wrap="False" />
                                </asp:BoundField>                             
                                <asp:BoundField DataField="UbicacionID" HeaderText="ID Alm"/>
                                     <asp:BoundField DataField="UbicacionNombre" HeaderText="Almacen" ItemStyle-HorizontalAlign="Center">
                                        <FooterStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                                        <HeaderStyle HorizontalAlign="center" Width="20px" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" Width="100px" Wrap="False" />
                                </asp:BoundField>  
                                    <asp:BoundField DataField="ProductoID" HeaderText="ID Producto"/>
                                <asp:BoundField DataField="ProductoNombre" HeaderText="Producto" ItemStyle-HorizontalAlign="Center" SortExpression="ProductoNombre">
                                        <FooterStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                                        <HeaderStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" Width="100px" Wrap="False" />
                                </asp:BoundField>  
                                <asp:BoundField DataField="ProductoLote" HeaderText="Lote"/>
                                 <asp:BoundField DataField="ProductoLoteHis" HeaderText="Lote Hist"/>                               
                                <asp:BoundField DataField="InventarioLogico" HeaderText="Cantidad Inventario Logica" />
                                <asp:BoundField DataField="InventarioFisico" HeaderText="Cantidad Inventario Fisica" /> 
                                <asp:BoundField DataField="InventarioPeso" HeaderText="Peso" /> 
                               <asp:BoundField DataField="InventarioUbicacionPasillo" HeaderText="Ubicación"/>
                                <asp:BoundField DataField="InventarioInicial" DataFormatString="{0:d}" HeaderText="Fecha Corte" />                                                                    
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
    </div> 


</asp:Content>