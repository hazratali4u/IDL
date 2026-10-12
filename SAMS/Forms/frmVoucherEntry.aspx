<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmVoucherEntry.aspx.cs" Inherits="Forms_frmVoucherEntry" Title="SAMS :: Voucher Entry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" Runat="Server">
   <script language="JavaScript" type="text/javascript">
          function pageLoad() {
           $("select").searchable();
       }
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest( startRequest );

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest( endRequest );

        function startRequest( sender, e )
        { 
            
            document.getElementById('<%=btnSave.ClientID%>').disabled = true;
            document.getElementById('<%=btnDone.ClientID%>').disabled = true;

        }

        function endRequest( sender, e ) 
        { 
           
            document.getElementById('<%=btnSave.ClientID%>').disabled = false;
            document.getElementById('<%=btnDone.ClientID%>').disabled = false;

       }

       function AccountHeadSelected(source, eventArgs) {           
           document.getElementById("<%=txtAccountDes.ClientID %>").focus();
       } 
    
    function ValidateForm()
    {        
        var Debit = document.getElementById("<%= txtDebitAmount.ClientID %>").value;
        var Credit = document.getElementById("<%= txtCreditAmount.ClientID %>").value;
        var VoucherType = document.getElementById("<%= DrpVoucherType.ClientID %>").value;
        
        if(Debit.length == 0 && Credit.length == 0)
        {
            alert('Debit or Credit Must Enter');
            return false;
        }
               

        if (Debit > 0 && Credit > 0)
        {
            alert('Only Debit or Credit Can Enter');
            return false;
        }
        return true;
 }
 
  </script>
  <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
 <div id="right_data">
     <div >
         <table width="100%">
             <tr>
                 <td>
                     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                         <ContentTemplate>
<TABLE><TBODY><TR><TD style="HEIGHT: 25px" align=left>
<strong><asp:Label id="Label4" runat="server" Width="61px" Text="Location" CssClass="lblbox"></asp:Label></strong> </TD><TD style="HEIGHT: 20px" align=left>
        <asp:DropDownList id="drpDistributor" runat="server" Width="206px" 
            CssClass="DropList" AutoPostBack="True" 
            onselectedindexchanged="drpDistributor_SelectedIndexChanged">
                                         </asp:DropDownList></TD><TD align=left></TD><TD style="WIDTH: 99px" align=left>
                                         &nbsp;</TD><TD align=left>&nbsp;</TD></TR><TR>
        <TD align=left style="HEIGHT: 25px">
                                         <strong>
                                         <asp:Label ID="Label16" runat="server" CssClass="lblbox" Height="16px" 
                                             Text="Voucher Type" Width="83px"></asp:Label>
                                         </strong> </TD>
        <TD align=left style="HEIGHT: 20px">
                                         <asp:DropDownList ID="DrpVoucherType" runat="server" AutoPostBack="True" 
                                             CssClass="DropList" 
                                             OnSelectedIndexChanged="DrpVoucherType_SelectedIndexChanged" Width="205px">
                                         </asp:DropDownList>
                                         
                                         </TD><TD align=left>&nbsp;</TD><TD style="WIDTH: 99px;" align=left>
                                         <strong>
                                         <asp:Label ID="lblSubType" runat="server" CssClass="lblbox" Height="16px" 
                                             Text="Voucher Sub Type" Width="120px"></asp:Label>
                                         </strong> </TD><TD  align=left>
                                         <asp:DropDownList ID="DrpVoucherSubType" runat="server" AutoPostBack="True" 
                                             CssClass="DropList" 
                                             OnSelectedIndexChanged="DrpVoucherSubType_SelectedIndexChanged" Width="205px">
                                         </asp:DropDownList>
        </TD></TR><TR><TD align=left>
                                         <strong>
                                         <asp:Label ID="lblPaymentMode" runat="server" CssClass="lblbox" Text="Payment Mode" 
                                             Width="87px"></asp:Label>
                                         </strong> </TD><TD  align=left>
                                         <asp:DropDownList ID="DrpPaymentMode" runat="server" CssClass="DropList" 
                                             Width="206px">
                                         </asp:DropDownList>
        </TD><TD align=left></TD><TD style="WIDTH: 99px" align=left>
                                         <strong> 
                                         <asp:Label id="lblBankHead" runat="server" Width="98px" Text="Bank/Cash Head" 
                                             CssClass="lblbox"></asp:Label></strong> </TD><TD align=left>
            <asp:DropDownList ID="drpBanks" runat="server" CssClass="DropList" 
                Width="206px">
            </asp:DropDownList>
        </TD></TR><TR><TD align=left>
                                          <strong> 
                                          <asp:Label id="lblChequeNo" runat="server" Width="76px" 
                                              Text="Cheque No" CssClass="lblbox" ></asp:Label></strong></TD><TD align=left>
                                          <asp:TextBox id="txtChequeNo" runat="server" Width="200px" CssClass="txtBox " ></asp:TextBox>
                                          </TD><TD align=left>
                                         </TD><TD style="WIDTH: 99px; " align=left>
                                          <strong>
                                          <asp:Label ID="lblSlipNo" runat="server" CssClass="lblbox" Text="Slip No" 
                                              Width="91px"></asp:Label>
                                          </strong>
                                          </TD>
                                         <TD align=left>
                                         
                                                                                                                          
                                             <asp:TextBox ID="txtSlipNo" runat="server" Width="200px"></asp:TextBox>
                                         
                                                                                                                          
                                         </TD></TR><TR><TD align=left>
                                         <strong> <asp:Label id="lblDueDate" runat="server" Width="85px" Text="Due Date"></asp:Label></strong>
                                          </TD>
                                          
                                          <TD align=left><asp:TextBox id="txtDueDate" runat="server" Width="180px" 
                                                  onkeyup="BlockEndDateKeyPress()" ReadOnly="true"></asp:TextBox>
                                              <asp:ImageButton ID="ibnEndDate" runat="server" 
                                                  ImageUrl="~/App_Themes/Granite/Images/date.gif" Width="16px" />
                                              <cc1:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy" 
                                                  PopupButtonID="ibnEndDate" TargetControlID="txtDueDate">
                                              </cc1:CalendarExtender>
        </TD><TD align=left>
                                          
                                          &nbsp;</TD>
                                          <TD style="WIDTH: 99px; " align=left>
        </TD><TD align=left>
            </TD></TR>
    <tr>
        <td align="left" style="HEIGHT: 25px">
            <strong>
            <asp:Label ID="Label10" runat="server" CssClass="lblbox" Text="Payee's Name" 
                Width="91px"></asp:Label>
            </strong>
        </td>
        <td align="left" style="HEIGHT: 25px">
            <asp:TextBox ID="txtpayeesName" runat="server" CssClass="txtBox " Width="200px"></asp:TextBox>
        </td>
        <td align="left" style="HEIGHT: 25px">
        </td>
        <td align="left" style="WIDTH: 99px; HEIGHT: 25px">
            <strong>
            <asp:Label ID="Label3" runat="server" CssClass="lblbox" Text="Narration" 
                Width="85px"></asp:Label>
            </strong>
        </td>
        <td align="left" style="HEIGHT: 25px">
            <asp:TextBox ID="txtRemarks" runat="server" CssClass="txtBox " MaxLength="250" 
                Width="200px"></asp:TextBox>               

        </td>
    </tr>
    </TBODY></TABLE> 
                                         
                                         <asp:TextBox id="txtTotalDebit" runat="server" Width="100px" CssClass="HidePanel"></asp:TextBox><asp:TextBox id="txtTotalCredit" runat="server" Width="73px" CssClass="HidePanel"></asp:TextBox> &nbsp; &nbsp; &nbsp; 
</ContentTemplate>
                     </asp:UpdatePanel>
                     </td>
             </tr>
         </table>
     </div>
     <div >
         <table width="100%">
             <tr>
                 <td>
                     <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                         <ContentTemplate>
<TABLE style="BORDER-RIGHT: #007395 1px solid; BORDER-TOP: #007395 1px solid; BORDER-LEFT: #007395 1px solid; BORDER-BOTTOM: #007395 1px solid" cellSpacing=0 cellPadding=0>
<TBODY>
<TR>
<TD class="tblhead">
<asp:Label id="lblskuCode" runat="server" ForeColor="White" Font-Bold="True" Text="Account Description"></asp:Label></TD>
    <TD class="tblhead">
<asp:Label id="lblskuname" runat="server" ForeColor="White" Font-Bold="True" Text="Remarks"></asp:Label></TD>
    <TD class="tblhead">
<asp:Label id="Label11" runat="server" ForeColor="White" Font-Bold="True" Text="Debit" ></asp:Label></TD><TD class="tblhead">
<asp:Label id="Label12" runat="server" ForeColor="White" Font-Bold="True" Text="Credit" ></asp:Label></TD><TD></TD>
<TD><asp:Label id="Label8" runat="server" Width="70px" ></asp:Label></TD>
</TR>
<TR>

<TD style="HEIGHT: 12px">
<asp:DropDownList ID="ddlAccountHead" runat="server" Width="300"></asp:DropDownList>
</TD>
    <TD style="HEIGHT: 12px"><asp:TextBox id="txtAccountDes" runat="server" Width="140px"></asp:TextBox></TD>
    
                                         <TD style="HEIGHT: 12px"><asp:TextBox id="txtDebitAmount" runat="server" Width="80px"></asp:TextBox></TD>
                                         <TD style="HEIGHT: 12px"><asp:TextBox id="txtCreditAmount" runat="server" Width="80px"></asp:TextBox></TD><TD style="HEIGHT: 12px"></TD><TD style="WIDTH: 79px; HEIGHT: 12px">
                                         <asp:Button accessKey="A" id="btnSave" onclick="btnSave_Click" runat="server" Width="70px" Font-Size="8pt" Text="Add New" CssClass="Button" /> </TD></TR>
<TR>
<TD align=center colSpan=8 rowSpan=1>
<asp:GridView id="GrdOrder" runat="server" Width="100%" ForeColor="SteelBlue" CssClass="gridRow2" BackColor="White" AutoGenerateColumns="False" BorderColor="White" CaptionAlign="Left" HorizontalAlign="Center" OnRowDataBound="GrdOrder_RowDataBound" OnRowDeleting="GrdOrder_RowDeleting" OnRowEditing="GrdOrder_RowEditing" ShowFooter="True" ShowHeader="False">
    <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
        PreviousPageText="Previous" />
    <Columns>
        <asp:BoundField DataField="Account_Head_Id" HeaderText="Account Head Id">
            <FooterStyle CssClass="HidePanel" />
            <HeaderStyle CssClass="HidePanel" />
            <ItemStyle CssClass="HidePanel" />
        </asp:BoundField>
        <asp:BoundField DataField="Account_Name" HeaderText="Account Name">
            <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="295px" />
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left"/>
        </asp:BoundField>
        <asp:BoundField DataField="Account_Name">
            <HeaderStyle CssClass="HidePanel" />
            <ItemStyle CssClass="HidePanel" />
            <FooterStyle CssClass="HidePanel" />
        </asp:BoundField>
        <asp:BoundField DataField="REMARKS" HeaderText="Account Description">
            <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" Width="140px"/>
        </asp:BoundField>        
        <asp:BoundField DataField="debit" DataFormatString="{0:F2}" HeaderText="Debit">
            <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
            <HeaderStyle HorizontalAlign="Right" />
            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" Width="80px"/>
        </asp:BoundField>
        <asp:BoundField DataField="credit" DataFormatString="{0:F2}" HeaderText="Credit">
            <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
            <HeaderStyle HorizontalAlign="Right" />
            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" Width="80px" />
        </asp:BoundField>
        <asp:BoundField DataField="Principal_id" HeaderText="Principal">
            <FooterStyle HorizontalAlign="Right" />
            <HeaderStyle CssClass="HidePanel" />
            <ItemStyle CssClass="HidePanel" HorizontalAlign="Right" />
        </asp:BoundField>
        <asp:CommandField HeaderText="Edit" ShowEditButton="True">
            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
            <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
        </asp:CommandField>
        <asp:TemplateField HeaderText="Delete">
            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                    Text="Delete"></asp:LinkButton>
            </ItemTemplate>
            <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
        </asp:TemplateField>
    </Columns>
    <HeaderStyle CssClass="tblhead" />
</asp:GridView>                                          
                                                    
        </TD>
        </TR>                                                    
        </TBODY>
        </TABLE>                                                    
                                                    <cc1:FilteredTextBoxExtender id="FilteredTextBoxExtender2" runat="server" TargetControlID="txtCreditAmount" FilterType="Custom" ValidChars=".0123456789">
                             </cc1:FilteredTextBoxExtender>
                             <DIV style="Z-INDEX: 101; LEFT: 706px; WIDTH: 100px; POSITION: absolute; TOP: 364px; HEIGHT: 100px">
                             <asp:Panel id="Panel21" runat="server">
                                     <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                                         <ProgressTemplate>
                                             <asp:ImageButton ID="ImageButton1" runat="server" Height="35px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                                 Width="39px" />
                                             Wait Update
                                         </ProgressTemplate>
                                     </asp:UpdateProgress>
                                 </asp:Panel> 
                                 </DIV>
                                 <cc1:FilteredTextBoxExtender id="FilteredTextBoxExtender1" runat="server" TargetControlID="txtDebitAmount" FilterType="Custom" ValidChars="0123456789.">
                             </cc1:FilteredTextBoxExtender> &nbsp; 
</ContentTemplate>
                     </asp:UpdatePanel>
                 </td>
             </tr>
         </table>
         <table width="50%">
            <tr>
                <td align="center">
                    <asp:Button ID="btnDone" runat="server" AccessKey="S" OnClick="btnDone_Click" Text="Save" CssClass="Button" />
                    &nbsp;<asp:Button ID="btnCancel" runat="server" AccessKey="S" OnClick="btnCancel_Click" 
                        Text="Cancel" CssClass="Button" />                        
                </td>
            </tr>
         </table>
       </div>
<br />
  </div>
</asp:Content>

