<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmChequeEntry.aspx.cs" Inherits="Forms_frmChequeEntry" Title="SAMS :: Cheque Entry" %>
<%@ Register Assembly="AjaxControlToolkit"   Namespace="AjaxControlToolkit"    TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
      <script language="JavaScript" type="text/javascript">
      
      Sys.WebForms.PageRequestManager.getInstance().add_beginRequest( startRequest );

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest( endRequest );

        function startRequest( sender, e )
        { 
            
            document.getElementById('<%=btnSave.ClientID%>').disabled = true;
            document.getElementById('<%=btnCancel.ClientID%>').disabled = true;
    
        }

        function endRequest( sender, e ) 
        { 
           
            document.getElementById('<%=btnSave.ClientID%>').disabled = false;
            document.getElementById('<%=btnCancel.ClientID%>').disabled = false;
    
        }
    
    function ValidateForm()
    {
        var str;
        str  =  document.getElementById("<%= txtAmount.ClientID %>").value;
        if(str == null || str.length == 0)
		{
			alert('Must enter Amount');
			return false;
		}
		
    }
 
  </script>
  <div id="right_data">
    <div >
        <table width="100%">
            <tr>
                <td >
                    <asp:UpdatePanel id="UpdatePanel2" runat="server" RenderMode="Inline">
                        <contenttemplate>
                        <DIV style="Z-INDEX: 101; LEFT: 400px; WIDTH: 100px; POSITION: absolute; TOP: 150px; HEIGHT: 100px"><asp:Panel id="Panel21" runat="server">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                    <ProgressTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" Height="26px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                            Width="23px" />
                        Wait Update
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </asp:Panel> </DIV>
<TABLE><TBODY><TR><TD align=left colSpan=2>
<asp:Label id="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label> </TD><TD style="WIDTH: 201px" align=left colSpan=1></TD>
    <TD align=left colSpan=1></TD><TD style="WIDTH: 201px" align=left colSpan=1></TD><TD align=left colSpan=1></TD></TR><TR><TD style="HEIGHT: 17px" align=left>
            <strong><asp:Label id="lblfromLocation" runat="server" Width="94px" Text="Location" CssClass="lblbox"></asp:Label></strong></TD><TD style="WIDTH: 201px; HEIGHT: 17px" align=left><asp:DropDownList id="drpDistributor" runat="server" Width="226px" CssClass="DropList" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged" AutoPostBack="True">
            </asp:DropDownList></TD><TD style="WIDTH: 201px; HEIGHT: 17px" align=left></TD>
    <TD style="HEIGHT: 17px" align=left>
        &nbsp;</TD><TD style="WIDTH: 201px; HEIGHT: 17px" align=left>&nbsp;</TD><TD style="HEIGHT: 17px" align=left>
        
        </TD></TR><TR><TD style="HEIGHT: 17px" align=left>
    <strong>
    <asp:Label ID="Label111" runat="server" CssClass="lblbox" Text="Status" 
        Width="86px"></asp:Label>
    </strong></TD><TD style="WIDTH: 201px; HEIGHT: 17px" align=left>
        <asp:DropDownList ID="DrpStatus" runat="server" AutoPostBack="True" 
            CssClass="DropList" OnSelectedIndexChanged="DrpStatus_SelectedIndexChanged" 
            Width="226px">
        </asp:DropDownList>
    </TD><TD style="WIDTH: 201px; HEIGHT: 17px" align=left></TD>
    <TD style="HEIGHT: 17px" align=left>
        &nbsp;</TD><TD style="WIDTH: 201px; HEIGHT: 17px" align=left>
        &nbsp;</TD><TD style="HEIGHT: 17px" align=left></TD></TR><TR><TD align=left>
            <strong>
            <asp:Label ID="Label112" runat="server" CssClass="lblbox" 
                Text="Deposit Account" Width="98px"></asp:Label>
            </strong></TD><TD style="WIDTH: 201px" align=left>
            <asp:DropDownList ID="DrpBankAccount" runat="server" AutoPostBack="True" 
                CssClass="DropList" OnSelectedIndexChanged="DrpStatus_SelectedIndexChanged" 
                Width="226px">
            </asp:DropDownList>
    </TD><TD style="WIDTH: 201px" align=left>
        <strong><asp:Label id="lblDocumentNo" runat="server" Width="25px" Text="            " CssClass="lblbox"></asp:Label></strong></TD><TD align=left colSpan=2 rowSpan=11>
        &nbsp;</TD><TD align=left rowSpan=11></TD></TR><TR><TD align=left>
        &nbsp;</TD><TD align=left>&nbsp;</TD><TD style="WIDTH: 201px"></TD></TR><TR><TD align=left>
            <strong><asp:Label id="Label5" runat="server" Width="94px" Text="Chque No" CssClass="lblbox"></asp:Label></strong></TD><TD align=left>
            <strong><asp:Label id="Label3" runat="server" Width="94px" Text="Bank Name" CssClass="lblbox"></asp:Label></strong></TD><TD style="WIDTH: 201px"></TD></TR><TR><TD align=left><asp:TextBox id="txtChequeNo" runat="server" Width="94px" CssClass="txtBox"></asp:TextBox></TD><TD vAlign=top align=left><asp:TextBox id="txtBankName" runat="server" Width="192px" CssClass="txtBox"></asp:TextBox></TD><TD vAlign=top align=left></TD></TR><TR><TD vAlign=top align=left>
            <strong><asp:Label id="Label13" runat="server" Width="94px" Text="Remarks" CssClass="lblbox"></asp:Label></strong></TD><TD style="WIDTH: 201px" vAlign=top align=left></TD><TD style="WIDTH: 201px" vAlign=top align=left></TD></TR><TR><TD vAlign=top align=left colSpan=2><asp:TextBox id="txtRemarks" runat="server" Width="298px" CssClass="txtBox"></asp:TextBox></TD><TD style="WIDTH: 201px" vAlign=top align=left></TD></TR><TR><TD vAlign=top align=left>
            <strong><asp:Label id="Label6" runat="server" Width="74px" Text="Chq Amount" CssClass="lblbox"></asp:Label></strong></TD><TD style="WIDTH: 201px" vAlign=top align=left>
            <strong><asp:Label id="Label9" runat="server" Width="73px" Text="Cheque Date" CssClass="lblbox"></asp:Label></strong></TD><TD style="WIDTH: 201px" vAlign=top align=left>&nbsp;</TD></TR><TR><TD vAlign=top align=left><asp:TextBox id="txtAmount" runat="server" Width="94px" CssClass="txtBox "></asp:TextBox></TD><TD style="WIDTH: 201px" vAlign=top align=left><asp:TextBox id="txtStartDate" runat="server" Width="132px" CssClass="txtBox "></asp:TextBox></TD><TD style="WIDTH: 201px" vAlign=top align=left></TD></TR><TR><TD vAlign=top align=left>
            <strong><asp:Label id="Label2" runat="server" Width="96px" Text="Slip No" CssClass="lblbox"></asp:Label></strong></TD><TD style="WIDTH: 201px" vAlign=top align=left>
            <strong><asp:Label id="Label1" runat="server" Width="100px" Text="Recevied Date" CssClass="lblbox"></asp:Label></strong></TD><TD style="WIDTH: 201px" vAlign=top align=left></TD></TR><TR><TD vAlign=top align=left><asp:TextBox id="txtSlipNo" runat="server" Width="94px" CssClass="txtBox "></asp:TextBox></TD><TD style="WIDTH: 201px" vAlign=top align=left><asp:TextBox id="txtReceivedDate" runat="server" Width="128px" CssClass="txtBox " ReadOnly="True"></asp:TextBox></TD><TD style="WIDTH: 201px" vAlign=top align=left></TD></TR><TR><TD vAlign=top align=left>
            <asp:Button accessKey="S" id="btnSave" onclick="btnSave_Click" runat="server" 
                Width="102px" Font-Size="8pt" Text="Save" CssClass="Button" Enabled="False" /> </TD><TD style="WIDTH: 201px" vAlign=top align=left>
            <asp:Button accessKey="C" id="btnCancel" runat="server" Width="120px" 
                Font-Size="8pt" Text="Cancel" OnClick="btnCancel_Click" CssClass="Button" 
                Enabled="False" /> </TD><TD style="WIDTH: 201px" vAlign=top align=left></TD></TR></TBODY></TABLE><cc1:FilteredTextBoxExtender id="FilteredTextBoxExtender1" runat="server" FilterType="Custom" ValidChars="0123456789." TargetControlID="txtAmount"></cc1:FilteredTextBoxExtender> <cc1:MaskedEditExtender id="MaskedEditExtender2" runat="server" TargetControlID="txtStartDate" Mask="99/99/9999" MaskType="Date">
                            </cc1:MaskedEditExtender> 
                            <asp:HiddenField id="HFChqueProcessId" runat="server"></asp:HiddenField> 
                            <asp:HiddenField ID="hfCustomerID" runat="server" />
</contenttemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        
           </div>
    <div >
        <table width="100%">
            <tr>
                <td >
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
<TABLE style="BORDER-RIGHT: silver thin inset; BORDER-TOP: silver thin inset; BORDER-LEFT: silver thin inset; WIDTH: 650px; BORDER-BOTTOM: silver thin inset"><TBODY><TR><TD style="HEIGHT: 20px" align=left colSpan=5><asp:Panel id="Panel12" runat="server" Width="750px" Height="200px" ScrollBars="Vertical"><TABLE style="BORDER-RIGHT: silver thin inset; BORDER-TOP: silver thin inset; BORDER-LEFT: silver thin inset; BORDER-BOTTOM: silver thin inset; BACKGROUND-COLOR: silver" width="100%"><TBODY><TR><TD style="HEIGHT: 21px" align=left>
<strong><asp:Label id="Label110" runat="server" Width="154px" Text="Select Searching Type"></asp:Label></strong> </TD><TD style="WIDTH: 170px; HEIGHT: 21px" align=left><asp:DropDownList id="ddSearchType" runat="server" Width="200px" CssClass="DropList">
                                                        <asp:ListItem Value="CHEQUE_NO">All Records</asp:ListItem>
                                                        <asp:ListItem Value="CUSTOMER_NAME">Customer</asp:ListItem>
                                                        <asp:ListItem Value="BANK_NAME">Bank Name</asp:ListItem>
                                                        <asp:ListItem Value="SlipNo">Slip No</asp:ListItem>
                                                        <asp:ListItem Value="account_name">Deposit Account </asp:ListItem>
                                                    </asp:DropDownList> </TD><TD style="WIDTH: 224px; HEIGHT: 21px" align=left><asp:TextBox id="txtSeach" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox> </TD><TD style="WIDTH: 250px; HEIGHT: 21px" align=left>                                                    
                                                    <asp:Button id="btnFilter" runat="server" Width="85px" Font-Size="8pt" Text="Filter" OnClick="btnFilter_Click"></asp:Button> </TD></TR></TBODY></TABLE>
                                                    <asp:GridView id="GrdOrder" runat="server" Width="100%" ForeColor="SteelBlue" CssClass="gridRow2" BorderColor="White" HorizontalAlign="Center" BackColor="White" AutoGenerateColumns="False" OnRowEditing="GrdOrder_RowEditing" OnRowDeleting="GrdOrder_RowDeleting">
                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                            PreviousPageText="Previous" />
                                        <Columns>
                                            <asp:BoundField DataField="CHEQUE_PROCESS_ID" HeaderText="CHEQUE_PROCESS_ID">
                                                <HeaderStyle CssClass="HidePanel" />
                                                <ItemStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CUSTOMER_ID" HeaderText="CUSTOMER_ID">
                                                <HeaderStyle CssClass="HidePanel" />
                                                <ItemStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="Customer">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CHEQUE_NO" HeaderText="Chq. No">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="BANK_NAME" HeaderText="Bank Name">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CHEQUE_DATE" HeaderText="Chq.Date">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="RECEIVED_DATE" HeaderText="Received Date">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DEPOSIT_DATE" HeaderText="Deposit Date">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CHEQUE_AMOUNT" DataFormatString="{0:F2}" HeaderText="Chq.Amount">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SlipNo" HeaderText="Slip No">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                                <HeaderStyle CssClass="HidePanel" />
                                                <ItemStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="area_id" HeaderText="area_id">
                                                <HeaderStyle CssClass="HidePanel" />
                                                <ItemStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="account_name" HeaderText="Deposit Account">
                                                <ControlStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="account_head_id" HeaderText="account_head_id">
                                                <HeaderStyle CssClass="HidePanel" />
                                                <ItemStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DeliveryManID" HeaderText="DeliveryManID">
                                                <HeaderStyle CssClass="HidePanel" />
                                                <ItemStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                            <asp:CommandField HeaderText="Edit" ShowEditButton="True">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                            </asp:CommandField>
                                        </Columns>
                                        <HeaderStyle CssClass="tblhead" HorizontalAlign="Center"
                                            VerticalAlign="Middle" />
                                    </asp:GridView>
                                     </asp:Panel> </TD></TR></TBODY></TABLE>
</ContentTemplate>
        </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>   
    </div>
</asp:Content>