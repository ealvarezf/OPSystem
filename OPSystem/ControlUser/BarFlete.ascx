<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="BarFlete.ascx.vb" Inherits="OPSystem.BarFlete" %>
<%--Trae el diseño del calendario.--%>
    <script src="../UI/i18n/datepicker-es.js"></script>

<%--Trae el diseño de panel --%>
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">


       <script>
            $(function () {
                $("#MainContent_Flete1_txtfecha").datepicker($.datepicker.regional["es"]);
            });
    </script>


 <script>
     $(function () {
         $("#MainContent_txtkeyfle").datepicker($.datepicker.regional["es"]);
     });
    </script>

<style type="text/css">
    .auto-style1 {
        width: 277px;
    }
    .auto-style2 {
        width: 184px;
    }
    .auto-style3 {
        height: 59px;
    }
    .auto-style6 {
        height: 44px;
    }
    .auto-style7 {
        height: 42px;
    }
    .auto-style8 {
        height: 43px;
    }
    .auto-style9 {
        height: 24px;
    }
    .auto-style10 {
        height: 24px;
        width: 27px;
    }
    .auto-style11 {
        height: 59px;
        width: 14px;
    }
    .auto-style13 {
        height: 43px;
        width: 27px;
    }
    .auto-style14 {
        height: 42px;
        width: 14px;
    }
    .auto-style15 {
        width: 14px;
    }
    .auto-style16 {
        height: 59px;
        width: 27px;
    }
    .auto-style17 {
        height: 42px;
        width: 27px;
    }
    .auto-style19 {
        height: 24px;
        width: 395px;
    }
    .auto-style20 {
        height: 59px;
        width: 395px;
    }
    .auto-style21 {
        height: 44px;
        width: 395px;
    }
    .auto-style22 {
        height: 43px;
        width: 395px;
    }
    .auto-style23 {
        height: 42px;
        width: 395px;
    }
    .auto-style24 {
        width: 395px;
    }
    .auto-style27 {
        height: 44px;
        width: 27px;
    }
    .auto-style28 {
    }
    .auto-style29 {
        width: 224px;
    }
    .auto-style30 {
        height: 24px;
        width: 100px;
    }
    .auto-style31 {
        height: 59px;
        width: 100px;
    }
    .auto-style32 {
        height: 44px;
        width: 100px;
    }
    .auto-style33 {
        height: 43px;
        width: 100px;
    }
    .auto-style34 {
        height: 42px;
        width: 100px;
    }
    .auto-style35 {
        width: 100px;
    }
    .auto-style37 {
        width: 25px;
    }
    .auto-style38 {
        width: 185px;
    }
    .auto-style39 {
        width: 308px
    }
    .auto-style40 {
        width: 142px;
    }
    .auto-style41 {
        width: 168px;
    }
</style>



<asp:Panel ID="Panel1" runat="server">
    <table style="width:100%;">
        <tr>
            <td class="auto-style15">
                <asp:Panel ID="Panel2" runat="server">
                    <table style="width:100%;">
                        <tr>
                            <td>
                                <asp:Panel ID="Panel3" runat="server" Width="690px">
                                    <div class="container">
  <h2></h2>
    <asp:Panel ID="Panel4" runat="server">
    </asp:Panel>
  <div class="panel-group">
    <div class="panel panel-primary">
      <div class="panel-heading">
          <asp:Panel ID="Panel5" runat="server">

              <table style="width: 100%;">
                  <tr>
                      <td class="auto-style2">&nbsp;</td>
                      <td class="auto-style68"></td>
                      <td class="auto-style1">
                          <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="REALIZAR FLETE"></asp:Label>
                      </td>
                      <td class="auto-style68"></td>
                      <td class="auto-style68">&nbsp;</td>
                  </tr>
              </table>

          </asp:Panel>
        </div>
      <div class="panel-body">
           <asp:Panel ID="Panel6" runat="server">

               <table style="width: 100%;">
                   <tr>
                       <td class="auto-style10">
                           </td>
                       <td class="auto-style30">
                           <asp:Panel ID="Panel24" runat="server">
                               <asp:Label ID="Label12" runat="server" Font-Bold="True" ForeColor="#3366CC" Text="ID:" Visible="False"></asp:Label>
                           </asp:Panel>
                       </td>
                       <td class="auto-style19">
                           <asp:TextBox ID="txtkeyfle" runat="server" Width="159px" Visible="False"></asp:TextBox>
                       </td>
                       <td class="auto-style9"></td>
                   </tr>
                   <tr>
                       <td class="auto-style16">
                       </td>
                       <td class="auto-style31">
                           <asp:Panel ID="Panel8" runat="server">
                               <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="#3366CC" Text="Fecha:"></asp:Label>
                           </asp:Panel>
                       </td>
                       <td class="auto-style20">
                           <asp:Panel ID="Panel16" runat="server">
                               <asp:TextBox ID="txtfecha" runat="server" Width="159px" CssClass="form-control"></asp:TextBox>
                           </asp:Panel>
                       </td>
                       <td class="auto-style3">
                           <asp:RequiredFieldValidator ID="RFVFECHA" runat="server" ControlToValidate="txtfecha" CssClass="alert-warning" ErrorMessage="Dato requerido"></asp:RequiredFieldValidator>
                       </td>
                   </tr>
                   <tr>
                       <td class="auto-style27">
                           &nbsp;</td>
                       <td class="auto-style32">
                           <asp:Panel ID="Panel10" runat="server">
                               <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="#3366CC" Text="Tipo Flete:"></asp:Label>
                           </asp:Panel>
                       </td>
                       <td class="auto-style21">
                           <asp:Panel ID="Panel17" runat="server">
                               <asp:DropDownList ID="DDLTIPOFLE" runat="server" AutoPostBack="True" CssClass="form-control" Height="34px" Width="206px">
                               </asp:DropDownList>
                           </asp:Panel>
                       </td>
                       <td class="auto-style6">
                           <asp:CompareValidator ID="RFVTIPOFLE" runat="server" ControlToValidate="DDLTIPOFLE" CssClass="alert-warning" ErrorMessage=" Requerido" Font-Size="Medium" Operator="NotEqual" Type="Integer" ValueToCompare="0"></asp:CompareValidator>
                       </td>
                   </tr>
                   <tr>
                       <td class="auto-style27">
                       </td>
                       <td class="auto-style32">
                           <asp:Panel ID="Panel9" runat="server" Height="20px">
                               <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="#3366CC" Text="Ruta:"></asp:Label>
                           </asp:Panel>
                       </td>
                       <td class="auto-style21">
                           <asp:Panel ID="Panel7" runat="server">
                               <asp:UpdatePanel ID="UpdatePanel4" runat="server" >
                                    <ContentTemplate>
                               <asp:DropDownList ID="DDLRUTA" runat="server" Height="34px" Width="340px" CssClass="form-control">
                               </asp:DropDownList>
                                    </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="DDLTIPOFLE" EventName="SelectedIndexChanged" />
                                </Triggers>
                              </asp:UpdatePanel>
                           </asp:Panel>
                       </td>
                       <td class="auto-style6">
                           <asp:CompareValidator ID="RFVRUTA" runat="server" ControlToValidate="DDLRUTA" CssClass="alert-warning" ErrorMessage=" Requerido" Font-Size="Medium" Operator="NotEqual" Type="Integer" ValueToCompare="0"></asp:CompareValidator>
                       </td>
                   </tr>
                   <tr>
                       <td class="auto-style13">
                       </td>
                       <td class="auto-style33">
                           <asp:Panel ID="Panel11" runat="server">
                               <asp:Label ID="Label5" runat="server" Font-Bold="True" ForeColor="#3366CC" Text="Transportista:"></asp:Label>
                           </asp:Panel>
                       </td>
                       <td class="auto-style22">
                           <asp:Panel ID="Panel18" runat="server">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" >
                                    <ContentTemplate>
                               <asp:DropDownList ID="DDLTRANS" runat="server" Height="34px" Width="271px" AutoPostBack="True" CssClass="form-control">
                               </asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="DDLTIPOFLE" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                           </asp:Panel>
                       </td>
                       <td class="auto-style8">
                           <asp:CompareValidator ID="RFVTRANS" runat="server" ControlToValidate="DDLTRANS" CssClass="alert-warning" ErrorMessage=" Requerido" Font-Size="Medium" Operator="NotEqual" Type="Integer" ValueToCompare="0"></asp:CompareValidator>
                       </td>
                   </tr>
                   <tr>
                       <td class="auto-style17">
                           </td>
                       <td class="auto-style34">
                           <asp:Panel ID="Panel12" runat="server">
                               <asp:Label ID="Label6" runat="server" Font-Bold="True" ForeColor="#3366CC" Text="Camión:"></asp:Label>
                           </asp:Panel>
                       </td>
                       <td class="auto-style23">
                           <asp:Panel ID="Panel19" runat="server">
                               <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                                    <ContentTemplate>
                               <asp:DropDownList ID="DDLCAMION" runat="server" Height="34px" Width="271px" CssClass="form-control">
                                </asp:DropDownList>
                            </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="DDLTRANS" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                           </asp:Panel>
                       </td>
                       <td class="auto-style7">
                           <asp:CompareValidator ID="RFVCAMION" runat="server" ControlToValidate="DDLCAMION" CssClass="alert-warning" ErrorMessage=" Requerido" Font-Size="Medium" Operator="NotEqual" Type="Integer" ValueToCompare="0"></asp:CompareValidator>
                       </td>
                   </tr>
                   <tr>
                       <td class="auto-style27">
                       </td>
                       <td class="auto-style32">
                           <asp:Panel ID="Panel13" runat="server">
                               <asp:Label ID="Label7" runat="server" Font-Bold="True" ForeColor="#3366CC" Text="Operador:"></asp:Label>
                           </asp:Panel>
                       </td>
                       <td class="auto-style21">
                           <asp:Panel ID="Panel20" runat="server">
                               <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
                                    <ContentTemplate>
                               <asp:DropDownList ID="DDOPERADOR" runat="server" Height="34px" Width="271px" CssClass="form-control">
                               </asp:DropDownList>
                                     </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="DDLTRANS" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                           </asp:Panel>
                       </td>
                       <td class="auto-style6">
                           <asp:CompareValidator ID="RFVOPERADOR" runat="server" ControlToValidate="DDOPERADOR" CssClass="alert-warning" ErrorMessage=" Requerido" Font-Size="Medium" Operator="NotEqual" Type="Integer" ValueToCompare="0"></asp:CompareValidator>
                       </td>
                   </tr>
                   <tr>
                       <td class="auto-style28">
                           &nbsp;</td>
                       <td class="auto-style35">
                           <asp:Panel ID="Panel15" runat="server">
                               <asp:Label ID="Label9" runat="server" Font-Bold="True" ForeColor="#3366CC" Text="Observación:"></asp:Label>
                           </asp:Panel>
                       </td>
                       <td class="auto-style24">
                           <asp:Panel ID="Panel22" runat="server">
                               <asp:TextBox ID="txtobserv" runat="server" Height="83px" TextMode="MultiLine" Width="303px" CssClass="form-control"></asp:TextBox>
                           </asp:Panel>
                       </td>
                       <td class="auto-style37"></td>
                   </tr>
                   <tr>
                       <td class="auto-style28">&nbsp;</td>
                       <td class="auto-style35">&nbsp;</td>
                       <td class="auto-style24">&nbsp;</td>
                       <td>&nbsp;</td>
                   </tr>
                   <tr>
                       <td class="auto-style28" colspan="4">
                           <asp:Panel ID="Panel25" runat="server">
                               <table style="width:100%;">
                                   <tr>
                                       <td class="auto-style41">&nbsp;</td>
                                       <td class="auto-style40">
                                           <asp:ImageButton ID="imgbtnGuardar" runat="server" Height="36px" ImageUrl="~/Img/Save-icon.png" Width="35px" />
                                           <asp:Label ID="Label10" runat="server" Font-Bold="True" ForeColor="#003399" Text="Guardar"></asp:Label>
                                       </td>
                                       <td class="auto-style39">
                                           <asp:ImageButton ID="imgbtnCancelar" runat="server" CausesValidation="False" Height="33px" ImageUrl="~/Img/Cancelar.jpg" Width="38px" />
                                           <asp:Label ID="Label11" runat="server" Font-Bold="True" ForeColor="#000099" Text="Cancelar"></asp:Label>
                                       </td>
                                       <td>&nbsp;</td>
                                   </tr>
                               </table>
                           </asp:Panel>
                       </td>
                   </tr>
               </table>

          </asp:Panel>
      </div>
    </div>

  </div>
</div>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
            <td class="auto-style11"></td>
        </tr>
        <tr>
            <td class="auto-style14">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Panel>