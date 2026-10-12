<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmExpenseEntry.aspx.cs" Inherits="frmExpenseEntry" Title="SAMS :: Petty Expense" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
<script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
      <script language="JavaScript" type="text/javascript">
          function pageLoad() {
              $("select").searchable();
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
		else if(str == "0")
		{
			alert('Amount should be greater than zero');
			return false;
		}
		
    }
 
  </script>
  <div id="right_data">
    <div>
        <table width="100%">
            <tr>
                <td>
                    <div style="z-index: 101; left: 537px; width: 100px; position: absolute; top: 250px;
                        height: 83px">
                        &nbsp;<asp:Panel ID="Panel21" runat="server">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                                <ProgressTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" Height="26px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                        Width="23px" />
                                    Wait Update
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </asp:Panel>
                    </div>
                
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
<cc1:FilteredTextBoxExtender id="FilteredTextBoxExtender1" runat="server" FilterType="Custom" TargetControlID="txtAmount" ValidChars="0123456789.">
            
                            </cc1:FilteredTextBoxExtender> <asp:Panel id="Panel1" runat="server">
<table width="40%">
<tbody>

<tr>
    <td>
        <strong> <asp:Label id="Label5" runat="server" Width="116px" Text="Detail Account Type"></asp:Label></strong>
    </td>
    <td >
        <fieldset style="width:70%">
            <asp:RadioButtonList id="rblDetailAccountType" runat="server" Width="270px" AutoPostBack="True" 
            onselectedindexchanged="rblDetailAccountType_SelectedIndexChanged">
                <asp:ListItem Selected="True" Value="8">Administrative Expenses</asp:ListItem>
                <asp:ListItem Value="9">Selling, Distribution and Marketing Expense</asp:ListItem>
            </asp:RadioButtonList>
        <legend></legend>
        </fieldset>
    </td>
</tr>

<tr>
    <td colspan="2"></td>
</tr>

<TR>
<TD style="HEIGHT: 20px" align=left>
<strong><asp:Label id="Label2" runat="server" Width="65px" Text="Voucher No" CssClass="lblbox"></asp:Label></strong>
</TD>
<TD style="WIDTH: 100px"><asp:DropDownList id="DrpVoucherNo" runat="server" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="DrpVoucherNo_SelectedIndexChanged"></asp:DropDownList>
</TD>
</TR>
<tr>
        <td align="left" style="HEIGHT: 20px">
            <strong>
            <asp:Label ID="lblfromLocation" runat="server" CssClass="lblbox" 
                Text="Location" Width="65px"></asp:Label>
            </strong>
        </td>
        <td style="WIDTH: 100px">
            <asp:DropDownList ID="drpDistributor" runat="server" 
                Width="250px" AutoPostBack="True" 
                onselectedindexchanged="drpDistributor_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>    
    <TR><TD style="HEIGHT: 20px" align=left>
                                    <strong>
                                    <asp:Label id="Label62" runat="server" Width="116px" Text="Petty Head" 
                                        CssClass="lblbox"></asp:Label></strong></TD><TD style="WIDTH: 100px">
                                        <asp:DropDownList ID="ddlPettyHead" runat="server" Width="250px">
                                        </asp:DropDownList>
        </TD></TR>
    <TR><TD style="HEIGHT: 20px" align=left>
                                    <strong>
                                    <asp:Label id="Label1" runat="server" Width="116px" Text="Expense Head" 
                                        CssClass="lblbox"></asp:Label></strong> </TD>
        <TD style="width: 100px;">
                                        <asp:DropDownList ID="DrpAccountHead" runat="server" CssClass="DropList" 
                                            Width="250px">
                                        </asp:DropDownList>
                                    </TD></TR><TR>
        <td align="left" style="HEIGHT: 20px">
            <strong>
            <asp:Label ID="Label9" runat="server" CssClass="lblbox" Text="Remarks" 
                Width="71px"></asp:Label>
            </strong>&nbsp;
        </td>
        <td align="left" style="HEIGHT: 20px">
            <asp:TextBox ID="txtRemarks" runat="server" CssClass="txtBox " Width="243px"></asp:TextBox>
        </td>
    </TR><TR><TD style="HEIGHT: 20px" align=left>
<strong><asp:Label id="Label6" runat="server" Width="63px" Text="Amount" 
        CssClass="lblbox"></asp:Label></strong> </TD><TD style="HEIGHT: 20px" align=left>
                                        <asp:TextBox id="txtAmount" runat="server" Width="163px" CssClass="txtBox "></asp:TextBox></TD>
        <td align="left" style="width: 100px;">
        </td>
        <td align="left" colspan="1" style="HEIGHT: 20px">
            <asp:Button ID="btnAddNew" runat="server" accessKey="S" CssClass="Button" 
                Font-Size="8pt" onclick="btnAddNew_Click" Text="Add" Width="95px" />
        </td>
    </TR></TBODY></TABLE></asp:Panel> &nbsp;&nbsp; 
</ContentTemplate>
                    </asp:UpdatePanel>
                    <strong><asp:Label ID="lblRowId" runat="server" Text="Label" Visible="False"></asp:Label></strong>
                </td>
            </tr>
        </table>
        
           </div>
    <div>
    <table width="100%">
        <tr>
            <td>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                                <asp:Panel ID="Panel12" runat="server" Height="136px" ScrollBars="Vertical" Width="620px" BorderColor="Gray" BorderStyle="Groove" BorderWidth="1px">
                                   <asp:GridView ID="GrdOrder" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="White" CaptionAlign="Left" CssClass="gridRow2" ForeColor="SteelBlue"
                                        HorizontalAlign="Center" OnRowDeleting="GrdOrder_RowDeleting"
                                        OnRowEditing="GrdOrder_RowEditing" Width="600px">
                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                            PreviousPageText="Previous" />
                                        <Columns>
                                            <asp:BoundField DataField="Account_Head_Id" HeaderText="Account Head Id">
                                                <FooterStyle CssClass="HidePanel" />
                                                <HeaderStyle CssClass="HidePanel" />
                                                <ItemStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Account_Name" HeaderText="Account Head">
                                                <FooterStyle CssClass="HidePanel" />
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="debit" DataFormatString="{0:F2}" FooterText="Total" HeaderText="Amount">
                                                <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ACCOUNT_PARENT_ID" HeaderText="ACCOUNT_PARENT_ID">
                                                <HeaderStyle CssClass="HidePanel" />
                                                <ItemStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                            <asp:CommandField HeaderText="Edit" ShowEditButton="True">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="47px" />
                                            </asp:CommandField>
                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                        Text="Delete"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="46px" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="tblhead" HorizontalAlign="Center"
                                            VerticalAlign="Middle" />
                                    </asp:GridView> 
                                </asp:Panel>
                <asp:Button ID="btnSave" runat="server" Font-Size="8pt" OnClick="btnSave_Click" Text="Save"
                    Width="117px" CssClass="Button" />
                <strong><asp:Label ID="Label61" runat="server" CssClass="lblbox" Text="Amount" Width="63px"></asp:Label></strong>
                <asp:TextBox ID="txtMainCash" runat="server" CssClass="txtBox " ReadOnly="True"></asp:TextBox>&nbsp;
                                
            </ContentTemplate>
        </asp:UpdatePanel>
        
             </td>
        </tr>
    </table>
    </div>
    </div>
</asp:Content>
