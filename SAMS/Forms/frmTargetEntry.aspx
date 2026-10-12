<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmTargetEntry.aspx.cs" Inherits="Forms_frmTargetEntry" Title="SAMS :: Target Entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
<style>
element.style {
    position: absolute;
    top: 18px;
    left: 0px;
    width: 282px;
    border: 1px solid rgb(51, 51, 51);
    font-weight: normal;
    padding: 0px;
    text-transform: none;
    z-index: 999;
    display: none;
    background-color: rgb(255, 255, 255);
}
</style>
<script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(startRequest);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);
        function startRequest(sender, e) {
            document.getElementById('<%=btnSave.ClientID%>').disabled = true;
        }
        function endRequest(sender, e) {
            document.getElementById('<%=btnSave.ClientID%>').disabled = false;
        }
        function ConfirmDelete() {
            if (confirm("Do you want to Cancel this record?") == true)
                return true;

            else {
                return false;
            }
        }
        function ValidateForm() {
            var str;
            str = document.getElementById('<%=txtFromdate.ClientID%>').value;
            if (str == null || str.length <= 1) {
                alert('Must Select Target Month');
                return false;
            }
            str = document.getElementById('<%=drpPrincipal.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Select Principal');
                return false;
            }
            str = document.getElementById('<%=drpDistributor.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Select Distributor');
                return false;
            }
            return true;
        }

        function pageLoad() {
            $("select").searchable();
            var Str = $('#<%=ddlSKU.ClientID %> option:selected').text();
            var unitprice = Str.substring(Str.indexOf(':') + 1);
            document.getElementById("<%= txtUnitRate.ClientID %>").value = unitprice;
            $('#<%=ddlSKU.ClientID %>').change(function () {
                var Str = $('#<%=ddlSKU.ClientID %> option:selected').text();
                var unitprice = Str.substring(Str.indexOf(':') + 1);
                document.getElementById("<%= txtUnitRate.ClientID %>").value = unitprice;
            });
        }

        function CalculateAmount() {
            var rate = document.getElementById("<%= txtUnitRate.ClientID %>").value;
            var qty = document.getElementById("<%= txtQuantity.ClientID %>").value;

            if (parseFloat(rate) > 0) {
            }
            else {
                rate = 0;
            }
            if (parseFloat(qty) > 0) {
            }
            else {
                qty = 0;
            }

            document.getElementById("<%= txtAmount.ClientID %>").value = (parseFloat(rate) * parseFloat(qty)).toFixed(2);
        }
    </script>
    <div id="right_data">
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="upSearchResults" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td align="left" style="height: 25px">
                                                <strong>
                                                    <asp:Label ID="Label6" runat="server" CssClass="lblbox" Height="14px" Text="Location"
                                                        Width="98px"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px" align="left">
                                                <asp:DropDownList ID="drpDistributor" runat="server" CssClass="DropList" Width="212px"
                                                    OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 25px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="height: 25px">
                                                <strong>
                                                    <asp:Label ID="lbltoLocation" runat="server" Width="71px" Text="Principal" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px" align="left">
                                                <asp:DropDownList ID="drpPrincipal" runat="server" Width="211px" CssClass="DropList"
                                                    AutoPostBack="True" OnSelectedIndexChanged="drpPrincipal_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 25px">
                                                <strong>
                                                    <asp:Label ID="Label5" runat="server" CssClass="lblbox" Width="50px"></asp:Label></strong>
                                            </td>
                                        </tr>
                                       
                                                    <asp:Label ID="Label2" runat="server" Width="98px" Visible="false" Text="Target For Type" CssClass="lblbox"
                                                        Height="14px"></asp:Label>
                                                <asp:DropDownList ID="DrpTargetType" runat="server" Width="211px" CssClass="DropList"
                                                     Visible="false" AutoPostBack="True">
                                                    
                                                    <asp:ListItem Value="98" Selected="True">Town</asp:ListItem>
                                                </asp:DropDownList>
                                            
                                       
                                        <tr>
                                            <td align="left" style="height: 25px">
                                                <strong>
                                                    <asp:Label ID="lblDocumentNo" runat="server" Width="76px" Text="Target For" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px" align="left">
                                                <asp:DropDownList ID="DrpTargetFor" runat="server" Width="210px" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 25px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="height: 25px">
                                                <strong>
                                                    <asp:Label ID="Label1" runat="server" Width="72px" Text="For Month" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px" align="left">
                                                <asp:TextBox ID="txtFromdate" runat="server" Width="204px" CssClass="txtBox" MaxLength="1"></asp:TextBox>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:ImageButton ID="ibtnStartDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                                    Width="16px" />
                                                <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
                                                <cc1:CalendarExtender ID="CEStartDate" runat="server" Format="MMM-yyyy" PopupButtonID="ibtnStartDate"
                                                    TargetControlID="txtFromdate">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
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
                            <table width="100%">
                                <tr>
                                    <td style="width: 282px;">
                                        <strong>
                                            <asp:Label ID="lblskuname" runat="server" CssClass="tblhead" Font-Bold="True" ForeColor="White"
                                                Text="SKU Name" Width="274px"></asp:Label></strong>
                                    </td>
                                    <td style="width: 62px;">
                                        <strong>
                                            <asp:Label ID="lblquantity" runat="server" CssClass="tblhead" Font-Bold="True" ForeColor="White"
                                                Height="16px" Text="Quantity" Width="62px"></asp:Label></strong>
                                    </td>
                                    <td style="width: 70px;">
                                        <strong>
                                            <asp:Label ID="lblFreeSKU" runat="server" CssClass="tblhead" Font-Bold="True" ForeColor="White"
                                                Height="16px" Text="SKU Rate" Width="70px"></asp:Label></strong>
                                    </td>
                                    <td style="width: 70px;">
                                        <strong>
                                            <asp:Label ID="lblBatchNo" runat="server" CssClass="tblhead" Font-Bold="True" ForeColor="White"
                                                Height="16px" Text="Amount" Width="70px"></asp:Label></strong>
                                    </td>
                                    <td>
                                        <strong>
                                            <asp:Label ID="Label41" runat="server" CssClass="tblhead" Font-Bold="True" ForeColor="White"
                                                Height="16px" Text="Add SKU" Width="85px"></asp:Label></strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 282px;">
                                        <asp:DropDownList ID="ddlSKU" runat="server" Width="282">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtQuantity" runat="server" CssClass="txtBox " onfocus="SearchedCode()"
                                            Width="70px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtUnitRate" runat="server" CssClass="txtBox" Enabled="False" Width="73px">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAmount" runat="server" Enabled="False" Width="76px">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" AccessKey="A" Font-Size="8pt" OnClick="btnSave_Click"
                                            CssClass="button" Text="Save" ValidationGroup="vg" Width="95px" onfocus="CalculateAmount()"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        <asp:Panel ID="Panel2" runat="server" BorderColor="Silver" BorderStyle="Groove" BorderWidth="1px"
                                            Height="180px" ScrollBars="Vertical" Width="640px" DefaultButton="btnSave">
                                            <asp:GridView ID="GrdPurchase" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                BorderColor="White" CssClass="gridRow2" ForeColor="SteelBlue" HorizontalAlign="Center"
                                                OnRowDeleting="GrdPurchase_RowDeleting" OnRowEditing="GrdPurchase_RowEditing"
                                                ShowHeader="False" Width="620px">
                                                <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                    PreviousPageText="Previous" />
                                                <RowStyle ForeColor="Black" />
                                                <Columns>
                                                    <asp:BoundField DataField="TARGET_ID" HeaderText="TARGET_ID">
                                                        <HeaderStyle CssClass="HidePanel" />
                                                        <ItemStyle CssClass="HidePanel" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                        <HeaderStyle CssClass="HidePanel" />
                                                        <ItemStyle CssClass="HidePanel" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="SKU_CODE" HeaderText="SKU Code">
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left"
                                                            Width="85px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="SKU_NAME" HeaderText="SKU Name">
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left"
                                                            Width="205px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="QUANTITY" HeaderText="Quantity">
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                            Width="75px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="UNIT_PRICE" HeaderText="Rate" DataFormatString="{0:F2}">
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                            Width="75px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="AMOUNT" HeaderText="Amount" DataFormatString="{0:F2}">
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:CommandField HeaderText="Edit" ShowEditButton="True">
                                                        <ItemStyle BorderColor="Silver" BorderWidth="1px" Width="40px" />
                                                    </asp:CommandField>
                                                    <asp:TemplateField HeaderText="Delete">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                Text="Delete"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" Width="45px" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="White" />
                                                <PagerStyle BackColor="Transparent" />
                                                <HeaderStyle CssClass="tblhead" />
                                                <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 12px">
                                    </td>
                                    <td style="height: 12px">
                                    </td>
                                    <td align="right">
                                    </td>
                                    <td style="height: 12px">
                                    </td>
                                    <td style="height: 12px">
                                        <strong>
                                            <asp:Label ID="Label3" runat="server" CssClass="tblhead" Font-Bold="True" ForeColor="White"
                                                Height="16px" Text="Total Value" Width="70px"></asp:Label></strong>
                                    </td>
                                    <td colspan="2" align="left">
                                        <asp:TextBox ID="txtTotalAmt" runat="server" CssClass="txtBox" Width="95px" ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>                    
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        </div>
    </div>
</asp:Content>
