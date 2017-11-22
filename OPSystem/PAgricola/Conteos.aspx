<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Conteos.aspx.vb" Inherits="OPSystem.Conteos" %>
<%@ Register src="~/ControlUser/BarEventos.ascx" tagname="BarEventos" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <link href="../CSS/jquery-ui.css"  rel="stylesheet" type="text/css" />
    <script src="../JS/jquery-1.9.1.js"></script>
    <script src="../JS/jquery-ui.js"></script>
    <script src="../UI/i18n/datepicker-es.js"></script>

    <script type="text/javascript">
        $(function () {
            var index = $("#MainContent_sel_tab").val();
            $("#tabs").tabs({ active: index });
        })
    </script>   

    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
    rel="stylesheet" type="text/css" />

<%-- <asp:AsyncPostBackTrigger ControlID="ImgBtnCancelar" EventName="Click" />--%>     <%--   <uc1:BarEventos ID="BtnAdd" runat="server" OnAceptarClicked="BtnAdd_AceptarClicked"/>--%>
    <script src="../UI/i18n/datepicker-es.js"></script>

    <%--                 <asp:AsyncPostBackTrigger ControlID="BarEventos1_btnNuevo" EventName="Click" />
                 <asp:AsyncPostBackTrigger ControlID="BarEventos1_btnEditar" EventName="Click" />
                 <asp:AsyncPostBackTrigger ControlID="BarEventos1_btnFiltrar" EventName="Click" />
                 <asp:AsyncPostBackTrigger ControlID="BarEventos1_btnEsp3" EventName="Click" />--%>
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">

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

            <%-- <asp:AsyncPostBackTrigger ControlID="ImgBtnCancelar" EventName="Click" />--%>
 
            <asp:Panel ID="pnlEventos" runat="server">
            <uc1:BarEventos ID="BarEventos1" runat="server" OnAceptarClicked="BarEventos1_AceptarClicked"/>
         <%--   <uc1:BarEventos ID="BtnAdd" runat="server" OnAceptarClicked="BtnAdd_AceptarClicked"/>--%>
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
                                            <asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#6600CC" Text="Descripción:"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 57px">
                                        <asp:Panel ID="Panel152" runat="server">
                                            <asp:TextBox ID="txtSearch_Desc" runat="server" CssClass="form-control" Height="30px" Width="300px"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 57px">&nbsp;</td>
                                    <td style="height: 57px">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 83px; height: 57px;">
                                        <asp:Panel ID="Panel138" runat="server" Width="133px">
                                            <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#6600CC" Text="Empresa:"></asp:Label>
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
                                    <td style="height: 49px; width: 83px">
                                        <asp:Panel ID="Panel141" runat="server" Width="133px">
                                            <asp:Label ID="Label19" runat="server" Font-Bold="True" ForeColor="#6600CC" Text="Fecha:" Font-Size="Medium"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 49px">
                                        <asp:Panel ID="Panel144" runat="server" Width="662px">
                                            <asp:TextBox ID="txtSearch_Fecha" runat="server" CssClass="form-control" Height="30px" Width="300px"></asp:TextBox>
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
                                    <td style="width: 150px">
                                        &nbsp;</td>
                                    <td style="width: 10px">
                                        <asp:Panel ID="pnlAcción" runat="server" Width="415px">
                                            <asp:Label ID="lblAcción"  runat="server" Font-Bold="True" ForeColor="#5D7B9D"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="PnlKey" runat="server">
                                            <asp:Label ID="lbl_ConteoID" runat="server" CssClass="col-md-2 control-label"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td style="width: 10px">
                                        <asp:Panel ID="pnlDes" runat="server" Width="416px">
                                            <asp:Label ID="lbl_NoConteo" runat="server"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 150px; height: 39px;">
                                        <asp:Panel ID="Panel1" runat="server">
                                            <asp:Label ID="Label1" runat="server" CssClass="col-md-2 control-label" Text="DESCRIPCIÓN"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 39px; width: 10px;">
                                        <asp:Panel ID="Panel2" runat="server" Width="416px">
                                            <asp:TextBox ID="txt_Descripción" runat="server" Width="300px" CssClass="form-control"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 39px">
                                        <asp:Panel ID="Panel153" runat="server">
                                            <asp:RequiredFieldValidator ID="RFV_DESC" runat="server" ControlToValidate="txt_Descripción" CssClass="alert-warning" ErrorMessage="*" Font-Size="Medium"></asp:RequiredFieldValidator>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 39px">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 150px; height: 31px;">
                                        <asp:Panel ID="Panel3" runat="server">
                                            <asp:Label ID="Label2" runat="server" CssClass="col-md-2 control-label" Text="FECHA"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 31px; width: 10px;">
                                        <asp:Panel ID="Panel4" runat="server" Width="415px">
                                            <asp:TextBox ID="txt_Fecha" runat="server" Width="300px" CssClass="form-control"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 31px">
                                        <asp:Panel ID="Panel154" runat="server">
                                            <asp:RequiredFieldValidator ID="RFV_FECHA" runat="server" ControlToValidate="txt_Fecha" CssClass="alert-warning" ErrorMessage="*" Font-Size="Medium"></asp:RequiredFieldValidator>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 31px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 150px; height: 48px;">
                                        <asp:Panel ID="Panel14" runat="server">
                                            <asp:Label ID="Label7" runat="server" CssClass="col-md-2 control-label" Text="EMPRESA"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 48px; width: 10px;">
                                        <asp:Panel ID="Panel15" runat="server" Width="414px">
                                            <asp:DropDownList ID="DDL_EMPRESA" runat="server" CssClass="form-control" Height="35px" Width="352px" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 48px">
                                        <asp:Panel ID="Panel155" runat="server">
                                            <asp:CompareValidator ID="RFV_EMPRESA" runat="server" ControlToValidate="DDL_EMPRESA" CssClass="alert-warning" ErrorMessage="*" Font-Size="Medium" Operator="NotEqual" Type="Integer" ValueToCompare="0"></asp:CompareValidator>
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 48px">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Panel ID="Panel150" runat="server">
                                            <asp:Label ID="Label20" runat="server" CssClass="col-md-2 control-label" Text="ALMACEN"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td style="width: 10px">
                                        <asp:Panel ID="Panel151" runat="server" Width="413px">
                                               <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                                                 <ContentTemplate>
                                            <asp:DropDownList ID="DDL_ALMACEN" runat="server" CssClass="form-control" Height="35px" Width="352px">
                                            </asp:DropDownList>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="DDL_EMPRESA" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel156" runat="server">
                                            <asp:CompareValidator ID="RFV_ALMACEN" runat="server" ControlToValidate="DDL_ALMACEN" CssClass="alert-warning" ErrorMessage="*" Font-Size="Medium" Operator="NotEqual" ValueToCompare="%"></asp:CompareValidator>
                                        </asp:Panel>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Panel ID="Panel5" runat="server">
                                            <table style="width:100%;">
                                                <tr>
                                                    <td style="width: 157px">&nbsp;</td>
                                                    <td style="width: 120px">
                                                        <asp:ImageButton ID="ImgBtnAceptar" runat="server" ImageUrl="~/Img/Acept-form.png" Width="30px" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="ImgBtnCancelar" runat="server" CausesValidation="False" ImageUrl="~/Img/Cancel-form.png" Width="30px" />
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

                  <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="SelectedIndexChanging" />
<%--                 <asp:AsyncPostBackTrigger ControlID="BarEventos1_btnNuevo" EventName="Click" />
                 <asp:AsyncPostBackTrigger ControlID="BarEventos1_btnEditar" EventName="Click" />
                 <asp:AsyncPostBackTrigger ControlID="BarEventos1_btnFiltrar" EventName="Click" />
                 <asp:AsyncPostBackTrigger ControlID="BarEventos1_btnEsp3" EventName="Click" />--%>
                <%-- <asp:AsyncPostBackTrigger ControlID="ImgBtnCancelar" EventName="Click" />--%>
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
                                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#0000CC" Text="CONTEOS"></asp:Label>
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
                        <asp:Label ID="lblkeyconteo" runat="server" Visible="False"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td style="width: 19px">
                        &nbsp;</td>
                    <td>
                      <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" EmptyDataText="No Existen Registros." CellPadding="4" DataKeyNames="ConteoID" Font-Size="XX-Small" ForeColor="#333333" GridLines="None" ShowFooter="True" AllowPaging="True" PageSize="15" CssClass="table table-condensed">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                           <Columns>
                                <asp:CommandField InsertVisible="False" ShowSelectButton="True" >
                                     <FooterStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                                        <HeaderStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                                </asp:CommandField>  
                                <asp:BoundField DataField="ConteoID" HeaderText="ID" ItemStyle-HorizontalAlign="Center" >  
                                         <FooterStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                                        <HeaderStyle HorizontalAlign="Left" Width="20px" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" Width="100px" Wrap="False" />
                                </asp:BoundField>                             
                                <asp:BoundField DataField="ConteoDescripcion" HeaderText="Descripción" ItemStyle-HorizontalAlign="Center">
                                        <FooterStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                                        <HeaderStyle HorizontalAlign="Left" Width="20px" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" Width="100px" Wrap="False" />
                                </asp:BoundField>                             
                           <%--     <asp:BoundField DataField="EmpresaID" HeaderText="ID Empresa"/>--%>
                                     <asp:BoundField DataField="EmpresaNombre" HeaderText="Empresa" ItemStyle-HorizontalAlign="Center">
                                        <FooterStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                                        <HeaderStyle HorizontalAlign="center" Width="20px" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" Width="100px" Wrap="False" />
                                </asp:BoundField>  
                             <%--   <asp:BoundField DataField="UbicacionID" HeaderText="ID Almacen"/>--%>
                                <asp:BoundField DataField="UbicacionNombre" HeaderText="Almacen" ItemStyle-HorizontalAlign="Center" >
                                        <FooterStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                                        <HeaderStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" Width="100px" Wrap="False" />
                                </asp:BoundField>                                 
                               <asp:BoundField DataField="ConteoFecha" DataFormatString="{0:d}" HeaderText="Fecha" >    
                                        <FooterStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                                        <HeaderStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" Width="100px" Wrap="False" />
                               </asp:BoundField>
                               <asp:BoundField DataField="ConteoStatus" HeaderText="Status">
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
                
     <%--   <asp:BoundField DataField="UbicacionID" HeaderText="ID Almacen"/>--%>

            </div>

</asp:Content>