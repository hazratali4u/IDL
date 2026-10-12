<%@ Page Title="SAMS :: Vendor Payment" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmVendorPayment.aspx.cs" Inherits="Forms_frmVendorPayment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" Runat="Server">
    <div id="right_data">
        <asp:UpdatePanel ID="upVendorPayment" runat="server">
            <ContentTemplate>
                <table width="100%">                    
                    <tr>
                        <td style="width:2%;">&nbsp;</td>
                        <td style="width:10%;">
                            <strong><asp:Label ID="lblLocation" runat="server" Text="Location"></asp:Label> </strong>
                        </td>
                        <td style="width:28%">
                            <asp:DropDownList ID="ddlLocation" runat="server" Width="200px" 
                                AutoPostBack="true" onselectedindexchanged="ddlLocation_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="width:10%;"></td>
                        <td style="width:50%;"></td>

                    </tr>
                    <tr>
                        <td style="width:2%;">&nbsp;</td>
                        <td style="width:10%;">
                            <strong><asp:Label ID="lblPrincipal" runat="server" Text="Principal"></asp:Label> </strong>
                        </td>
                        <td style="width:28%">
                            <asp:DropDownList ID="ddlPrincipal" runat="server" Width="200px" 
                                AutoPostBack="true" onselectedindexchanged="ddlPrincipal_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="width:10%;"></td>
                        <td style="width:50%;"></td>
                    </tr>
                    <tr>
                        <td style="width:2%;">&nbsp;</td>
                        <td style="width:10%;">
                            <strong><asp:Label ID="lblPaymentMode" runat="server" Text="Payment Mode"></asp:Label> </strong>
                        </td>
                        <td style="width:28%">
                            <asp:DropDownList ID="ddlPaymentMode" runat="server" Width="200px" 
                                AutoPostBack="True" 
                                onselectedindexchanged="ddlPaymentMode_SelectedIndexChanged">
                                <asp:ListItem Value="18" Text="Cheque"></asp:ListItem>
                                <asp:ListItem Value="19" Text="Cash"></asp:ListItem>
                                 <asp:ListItem Value="20" Text="Online Transfer"></asp:ListItem>
                                  <asp:ListItem Value="21" Text="Pay Order"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="width:10%;">
                            <strong><asp:Label ID="lblBank" runat="server" Text="Bank"></asp:Label> </strong>
                        </td>
                        <td style="width:50%;">
                            <asp:DropDownList ID="ddlBank" runat="server" Width="200px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:2%;">&nbsp;</td>
                        <td style="width:10%;">
                            <strong><asp:Label ID="lblCheque" runat="server" Text="Instrument No"></asp:Label> </strong>
                        </td>
                        <td style="width:28%">
                            <asp:TextBox ID="txtCheque" runat="server" Width="200px"></asp:TextBox>
                        </td>
                        <td style="width:10%;">
                            <strong><asp:Label ID="lblSlip" runat="server" Text="Slip No"></asp:Label> </strong>
                        </td>
                        <td style="width:50%;">
                            <asp:TextBox ID="txtSlip" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:2%;">&nbsp;</td>
                        <td style="width:10%;">
                            <strong><asp:Label ID="lblChequeDate" runat="server" Text="Instrument Date"></asp:Label> </strong>
                        </td>
                        <td style="width:28%">
                            <asp:TextBox ID="txtChequeDate" runat="server" Width="180px" onkeyup="BlockEndDateKeyPress()"
                                Enabled="false"></asp:TextBox>
                            <asp:ImageButton ID="ibnEndDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                Width="16px" />
                            <ajax:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibnEndDate"
                                TargetControlID="txtChequeDate">
                            </ajax:CalendarExtender>
                        </td>
                        <td style="width:10%;">
                            <strong><asp:Label ID="lblAmount" runat="server" Text="Amount"></asp:Label> </strong>                                                        
                        </td>
                        <td style="width:50%;">
                            <asp:TextBox ID="txtAmount" runat="server" Width="200px"></asp:TextBox>                  
                            <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtAmount"
                                    FilterType="Custom" ValidChars="-.0123456789">
                            </ajax:FilteredTextBoxExtender>           
                        </td>
                    </tr>
                    <tr>
                        <td ></td>
                        <td >
                            <strong><asp:Label ID="lblIncomeTax" runat="server" Text="Income Tax" Visible ="false" ></asp:Label> </strong>
                        </td>
                        <td >
                            <asp:TextBox ID="txtIncomeTax" runat="server" Width="200px" Visible ="false" ></asp:TextBox>
                        </td>
                        <td >
                            <strong><asp:Label ID="lblDiscount" runat="server" Text="Discount" Visible ="false" ></asp:Label> </strong>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDiscount" runat="server" Width="200px" Visible ="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:2%;">&nbsp;</td>
                        <td style="width:10%;">
                            <strong><asp:Label ID="lblNarration" runat="server" Text="Narration"></asp:Label> </strong>
                        </td>
                        <td colspan="3" style="width:88%">
                            <asp:TextBox ID="txtNarration" runat="server" Width="67%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width:2%;">&nbsp;</td>
                        <td style="width:10%;">
                            
                        </td>
                        <td style="width:28%">
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="Button" 
                                onclick="btnSave_Click" />
                        </td>
                        <td style="width:10%;">
                            
                        </td>
                        <td style="width:50%;">
                            
                        </td>                        
                    </tr>
                    <tr>
                        <td colspan="5"> &nbsp; </td>
                    </tr>
                    <tr>
                        <td style="width:2%;">&nbsp;</td>
                        <td style="width:98%;" colspan="4">
                            <asp:Panel ID="Panel12" runat="server" Width="750px" Height="200px" ScrollBars="Vertical" BorderStyle="solid" BorderWidth="1px" >
                                        <asp:GridView ID="GrdOrder" runat="server" Width="728px" ForeColor="SteelBlue"
                                            AutoGenerateColumns="False" BackColor="White" BorderColor="White" 
                                            HorizontalAlign="Center" onrowdeleting="GrdOrder_RowDeleting">
                                            <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                PreviousPageText="Previous"></PagerSettings>
                                            <Columns>                                                                                                                                                                                                                                                
                                                <asp:BoundField DataField="CHEQUE_NO" HeaderText="Cheque No">
                                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SLIP_NO" HeaderText="Slip No">
                                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CHQUE_DATE" HeaderText="Cheque Date" DataFormatString="{0:dd-MMM-yyyy}">
                                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:f2}">
                                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                </asp:BoundField>                                                                                                
                                                <asp:BoundField DataField="Discount" HeaderText="Discount" DataFormatString="{0:f2}">
                                                     <HeaderStyle CssClass="HidePanel" />
                                         <ItemStyle CssClass="HidePanel" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="IncomeTax" HeaderText="IncomeTax" DataFormatString="{0:f2}">
                                                    <HeaderStyle CssClass="HidePanel" />
                                         <ItemStyle CssClass="HidePanel" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDelete" runat="server" Text="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                            CommandName="Delete"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="tblhead">
                                            </HeaderStyle>
                                        </asp:GridView>
                                    </asp:Panel>
                        </td>            
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

