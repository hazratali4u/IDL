<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmDistributorCustomer.aspx.cs" Inherits="Forms_frmDistributorCustomer"
    Title="SAMS :: New Customer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script language="JavaScript" type="text/javascript">

        function NameValidation(event) {
            // Allow: backspace, delete, tab and escape
            if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 32 ||
            // Allow: Ctrl+A
            (event.keyCode == 65 && event.ctrlKey === true) ||
            // Allow: home, end, left, right
            (event.keyCode >= 35 && event.keyCode <= 39) ||
            // Allow: Dash, Underscoor
            (event.keyCode == 189) ||
            //Allow Comma,Period
            ((event.keyCode == 190 || event.keyCode == 188) && event.shiftKey === false) ||
            //Allow a-z
            (event.keyCode >= 65 && event.keyCode <= 90)) {
                // let it happen, don't do anything
                return;
            }
            else {
                // Ensure that it is a number and stop the keypress
                event.preventDefault();
            }
        }

        function AddressValidation(event) {
            // Allow: backspace, delete, tab , escape and space bar
            if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 32 ||
            // Allow: Ctrl+A
            (event.keyCode == 65 && event.ctrlKey === true) ||
            // Allow: home, end, left, right
            (event.keyCode >= 35 && event.keyCode <= 39) ||
            // Allow: Dash, Underscoor
            (event.keyCode == 189) ||
            // Allow: Open bracket, Close bracket
            ((event.keyCode == 57 || event.keyCode == 48) && event.shiftKey === true) ||
            //Allow Comma,Period
            ((event.keyCode == 190 || event.keyCode == 188) && event.shiftKey === false) ||
            //Allow 0-9
            ((event.keyCode >= 48 && event.keyCode <= 57) && event.shiftKey === false) || //Standard Numbers
            (event.keyCode >= 96 && event.keyCode <= 105) || //Keypad numbers
            //Allow a-z
            (event.keyCode >= 65 && event.keyCode <= 90)) {
                // let it happen, don't do anything
                return;
            }
            else {
                // Ensure that it is a number and stop the keypress
                event.preventDefault();
            }
        }

        function PhoneValidation(event) {
            // Allow: backspace, delete, tab , escape
            if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 ||
            // Allow: Ctrl+A
            (event.keyCode == 65 && event.ctrlKey === true) ||
            // Allow: home, end, left, right
            (event.keyCode >= 35 && event.keyCode <= 39) ||
            // Allow: Dash
            (event.keyCode == 189 && event.shiftKey === false) ||
            //Allow 0-9
            ((event.keyCode >= 48 && event.keyCode <= 57) && event.shiftKey === false) || //Standard Numbers
            //Keypad numbers
            (event.keyCode >= 96 && event.keyCode <= 105)) {
                // let it happen, don't do anything
                return;
            }
            else {
                // Ensure that it is a number and stop the keypress
                event.preventDefault();
            }
        }

        function pageLoad() {
            $('#<%=txtCNIC.ClientID %>').mask("99999-9999999-9");
            $('#<%=txtNTN.ClientID %>').mask("9999999-9");
            $('#<%=txtIsRegister.ClientID %>').mask("99-99-9999-999-99");

            $('#<%=Grid_users.ClientID %>').tablesorter(
	     {
	         headers: {
	             25: {
	                 sorter: false
	             }
	         }
	     }
	     );

        }
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(startRequest);

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);

        function startRequest(sender, e) {
            document.getElementById('<%=btnSave.ClientID%>').disabled = true;
            document.getElementById('<%=btnSearch.ClientID%>').disabled = true;
            document.getElementById('<%=btnCancel.ClientID%>').disabled = true;

        }

        function endRequest(sender, e) {

            document.getElementById('<%=btnSave.ClientID%>').disabled = false;
            document.getElementById('<%=btnSearch.ClientID%>').disabled = false;
            document.getElementById('<%=btnCancel.ClientID%>').disabled = false;
        }


        function ValidateForm() {
            var str;
            str = document.getElementById('<%=txtCustomerName.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Customer Name');
                return false;
            }
            str = document.getElementById('<%=txtContactPerson.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Contact Person Name');
                return false;
            }
            str = document.getElementById('<%=txtAddress.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Address');
                return false;
            }

            str = document.getElementById('<%=txtPhoneNo.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Phone No');
                return false;
            }
            str = document.getElementById('<%=txtRegdate.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Register Date');
                return false;
            }

            str = document.getElementById('<%=txtCNIC.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter CNIC #');
                return false;
            }

            str = document.getElementById('<%=txtNTN.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter NTN #');
                return false;
            }

            if ($('#<%= ChbIsRegister.ClientID %>').is(':checked')) {
                str = document.getElementById('<%=txtIsRegister.ClientID%>').value;
                if (str == null || str.length == 0) {
                    alert('Must enter STRN #');
                    return false;
                }
            }

            return true;
        }
        function SearchRecord() {
            var str;
            str = document.getElementById('<%=txtSeach.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Key Word for Searching');
                return false;
            }
            return true;
        }
    
    </script>
    <div id="right_data">
        <div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <table width="100%" cellpadding="3" cellspacing="3">
                        <tr>
                            <td style="width:10%">
                                <strong>
                                        <asp:Label ID="lblLocation" runat="server" Text="Location"></asp:Label>
                                </strong>
                            </td>
                            <td style="width:30%">
                                <asp:DropDownList ID="DrpDistributor" runat="server" Width="205px" TabIndex="0"
                                    OnSelectedIndexChanged="DrpDistributor_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                            <td style="width:10%">
                                <strong>
                                        <asp:Label ID="lblChannelType" runat="server" Text="Channel Type"></asp:Label>
                                </strong>
                            </td>
                            <td style="width:50%">
                                <asp:DropDownList ID="drpChannelType" runat="server" Width="205px" TabIndex="9">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:10%">
                                <strong>
                                        <asp:Label ID="lblTown" runat="server" Text="Town"></asp:Label>
                                </strong>
                            </td>
                            <td style="width:30%">
                                <asp:DropDownList ID="DrpTown" runat="server" Width="205px" OnSelectedIndexChanged="DrpTown_SelectedIndexChanged"
                                    AutoPostBack="True" TabIndex="1">
                                </asp:DropDownList>
                            </td>
                            <td style="width:10%">
                                <strong>
                                        <asp:Label ID="lblBusinessType" runat="server" Text="Business Type"></asp:Label>
                                </strong>
                            </td>
                            <td style="width:50%">
                                <asp:DropDownList ID="DrpBusinessType" runat="server" Width="205px" TabIndex="10">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:10%">
                                <strong>
                                        <asp:Label ID="lblCustomerName" runat="server" Text="Customer Name"></asp:Label>
                                </strong>
                            </td>
                            <td style="width:30%">
                                <asp:TextBox ID="txtCustomerName" runat="server" Width="205px" MaxLength="100" TabIndex="2"></asp:TextBox>
                            </td>
                            <td style="width:10%">
                                <strong>
                                        <asp:Label ID="lblPriceGroup" runat="server" Text="Price Group"></asp:Label>
                                </strong>
                            </td>
                            <td style="width:50%">
                                <asp:DropDownList ID="drpPriceGroup" runat="server" AutoPostBack="True" Width="205" TabIndex="11"
                                    OnSelectedIndexChanged="drpPriceGroup_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:10%">
                                <strong>
                                    <asp:Label ID="lblContactPerson" runat="server" Text="Contact Person"></asp:Label>
                                </strong>
                            </td>
                            <td style="width:30%">
                                <asp:TextBox ID="txtContactPerson" runat="server" Width="205px" MaxLength="50"  TabIndex="3"></asp:TextBox>
                            </td>
                            <td style="width:10%">
                                <strong>
                                    <asp:Label ID="lblTradOfferGroup" runat="server" Text="T.O. Group"></asp:Label>
                                </strong>
                            </td>
                            <td style="width:50%">
                                <asp:DropDownList ID="DrpVolumeClass" runat="server" Width="205px" TabIndex="12">
                                 </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:10%">
                                <strong>
                                    <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label>
                                </strong>
                            </td>
                            <td style="width:30%">
                                <asp:TextBox ID="txtAddress" runat="server" Width="205px" MaxLength="250" TabIndex="4"></asp:TextBox>
                            </td>
                            <td style="width:10%">
                                <strong>
                                    <asp:Label ID="lblReplacement" runat="server" Text="Replacement"></asp:Label>
                                </strong>
                            </td>
                            <td style="width:50%">
                                <asp:DropDownList ID="ddlReplacement" runat="server" Width="205px" TabIndex="13">
                                    <asp:ListItem Value="1" Text="Applicable"></asp:ListItem>
                                    <asp:ListItem Value="2" Text=" Not Applicable"></asp:ListItem>
                                 </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:10%">
                                <strong>
                                    <asp:Label ID="lblPhoneNo" runat="server" Text="Phone No"></asp:Label>
                                </strong>
                            </td>
                            <td style="width:30%">
                                <asp:TextBox ID="txtPhoneNo" runat="server" Width="205px" MaxLength="15" TabIndex="5"></asp:TextBox>
                            </td>
                            <td style="width:10%">
                                <strong>
                                    <asp:Label ID="lblUnloading" runat="server" Text="Unloading"></asp:Label>
                                </strong>
                            </td>
                            <td style="width:50%">
                                <asp:DropDownList ID="ddlUnloading" runat="server" Width="205px" TabIndex="14">
                                    <asp:ListItem Value="1" Text="Applicable"></asp:ListItem>
                                    <asp:ListItem Value="2" Text=" Not Applicable"></asp:ListItem>
                                 </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:10%">
                                <strong>
                                    <asp:Label ID="lblCNIC" runat="server" Text="CNIC #"></asp:Label>
                                </strong>
                            </td>
                            <td style="width:30%">
                                <asp:TextBox ID="txtCNIC" runat="server" Width="205px" TabIndex="6"></asp:TextBox>
                            </td>
                            <td style="width:10%">
                                <strong>
                                    <asp:Label ID="lblCreditLimit" runat="server" Text="Credit Limit"></asp:Label>
                                </strong>
                            </td>
                            <td style="width:50%">
                                <asp:TextBox ID="txtCreditLimit" runat="server" Width="205px" TabIndex="15"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:10%">
                                <strong>
                                    <asp:Label ID="lblNTN" runat="server" Text="NTN #"></asp:Label>
                                </strong>
                            </td>
                            <td style="width:30%">
                                <asp:TextBox ID="txtNTN" runat="server" Width="205px" TabIndex="7"></asp:TextBox>
                            </td>
                            <td style="width:10%">
                                <strong>
                                    <asp:Label ID="lblAllowedDays" runat="server" Text="Allowed Days"></asp:Label>
                                </strong>
                            </td>
                            <td style="width:50%">
                                <asp:TextBox ID="txtAllowedDays" runat="server" Width="205px" TabIndex="16"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:10%">
                                <strong>
                                    <asp:CheckBox ID="ChbIsRegister" runat="server" Text="Is Register" AutoPostBack="True"
                                        OnCheckedChanged="ChbIsRegister_CheckedChanged" TabIndex="8"></asp:CheckBox>
                                </strong>
                            </td>
                            <td style="width:30%">
                                <asp:TextBox ID="txtIsRegister" runat="server" Width="205px" Enabled="False"></asp:TextBox>
                            </td>
                            <td style="width:10%">
                                <strong>
                                    <asp:Label ID="lblRegisterDate" runat="server" Text="Register Date" Visible="false"></asp:Label>
                                </strong>
                            </td>
                            <td style="width:50%">
                                <asp:TextBox ID="txtRegdate" runat="server" Width="205px" ReadOnly="True" TabIndex="17" Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tbody>
                            <tr>
                                <td style="width: 143px" align="left">
                                </td>
                                <td style="width: 175px">
                                    <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Width="80px" Font-Size="8pt"
                                        Text="Save" ValidationGroup="vg" CssClass="Button"  TabIndex="18"/>
                                    &nbsp;
                                    <asp:Button ID="btnCancel" OnClick="btnCancel_Click" runat="server" Width="80px"
                                        Font-Size="8pt" Text="Cancel" CssClass="Button"  TabIndex="19"/>
                                </td>
                                <td style="width: 1px">
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkIsActive" runat="server" Width="93px" Text="IsActive" Checked="True">
                                    </asp:CheckBox>
                                </td>
                                <td style="width: 219px">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div style="z-index: 101; left: 740px; width: 100px; position: absolute; top: 244px;
                height: 100px">
                &nbsp;<asp:Panel ID="Panel21" runat="server">
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                        <ProgressTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server" Height="26px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                Width="23px" />
                            Wait Update
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </asp:Panel>
            </div>
        </div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="border-right: silver thin inset; border-top: silver thin inset; border-left: silver thin inset;
                        width: 100%; border-bottom: silver thin inset; background-color: silver">
                        <tbody>
                            <tr>
                                <td style="height: 21px" align="left">
                                    <asp:Label ID="Label10" runat="server" Width="153px" Text="Select Searching Type"></asp:Label>
                                </td>
                                <td style="width: 170px; height: 21px" align="left">
                                    <asp:DropDownList ID="ddSearchType" runat="server" Width="200px" CssClass="DropList">
                                        <asp:ListItem Value="CUSTOMER_CODE">Customer Code</asp:ListItem>
                                        <asp:ListItem Value="CUSTOMER_NAME">Customer Name</asp:ListItem>
                                        <asp:ListItem Value="CONTACT_PERSON">Contact Person</asp:ListItem>
                                        <asp:ListItem Value="CONTACT_NUMBER">Contact Number</asp:ListItem>
                                        <asp:ListItem Value="ADDRESS">Address</asp:ListItem>
                                        <asp:ListItem Value="EMAIL_ADDRESS">Email Address</asp:ListItem>
                                        <%--  <asp:ListItem Value="GEO_NAME">Town Name</asp:ListItem>
                                        <asp:ListItem Value="AREA_NAME">Route Name</asp:ListItem>--%>
                                        <asp:ListItem Value="ROUTE_NAME">Market Name</asp:ListItem>
                                        <asp:ListItem Value="SLASH_DESC">Channel Type</asp:ListItem>
                                        <asp:ListItem>CNIC</asp:ListItem>
                                        <asp:ListItem>NTN</asp:ListItem>
                                        <asp:ListItem Value="GST_NUMBER">STRN</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 224px; height: 21px" align="left">
                                    <asp:TextBox ID="txtSeach" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                </td>
                                <td style="height: 21px" align="left" width="250">
                                    <asp:Button ID="btnSearch" runat="server" Width="85px" Font-Size="8pt" Text="Filter"
                                        OnClick="btnSearch_Click"></asp:Button>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:Panel ID="Panel2" runat="server" Width="100%" Height="200px" ScrollBars="Vertical">
                        <asp:GridView ID="Grid_users" runat="server" Width="100%" ForeColor="SteelBlue" CssClass="tablesorter"
                            HorizontalAlign="Center" AutoGenerateColumns="False" BackColor="White" BorderColor="White"
                            OnRowEditing="Grid_users_RowEditing">
                            <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                PreviousPageText="Previous"></PagerSettings>
                            <RowStyle ForeColor="Black"></RowStyle>
                            <Columns>
                                <asp:BoundField DataField="CUSTOMER_ID" HeaderText="Customer Id">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="DISTRIBUTOR_ID" HeaderText="DISTRIBUTOR_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" CssClass="HidePanel">
                                    </ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="BUSINESS_TYPE_ID" HeaderText="BUSINESS_TYPE_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PROMOTION_CLASS" HeaderText="PROMOTION_CLASS">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CHANNEL_TYPE_ID" HeaderText="CHANNEL_TYPE_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="TOWN_ID" HeaderText="TOWN_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" CssClass="HidePanel">
                                    </ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="AREA_ID" HeaderText="AREA_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" CssClass="HidePanel">
                                    </ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ROUTE_ID" HeaderText="ROUTE_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" CssClass="HidePanel">
                                    </ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CUSTOMER_CODE" HeaderText="Code">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="Name">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CONTACT_PERSON" HeaderText="Contact Person">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" CssClass="HidePanel">
                                    </ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CONTACT_NUMBER" HeaderText="Contact Number">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EMAIL_ADDRESS" HeaderText="Email">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" CssClass="HidePanel">
                                    </ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ADDRESS" HeaderText="ADDRESS">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="GST_NUMBER" HeaderText="Gst No">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" CssClass="HidePanel">
                                    </ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ChannelType" HeaderText="Channel Type">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="GEO_NAME" HeaderText="Town">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="AREA_NAME" HeaderText="Route">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ROUTE_NAME" HeaderText="Market">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="IS_ACTIVE" HeaderText="Status">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="REGDATE">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="IS_STAND" HeaderText="Stand">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="IS_COOLER" HeaderText="Cooler">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CNIC" HeaderText="CNIC">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="NTN" HeaderText="NTN">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CREDIT_LIMIT" HeaderText="CREDIT_LIMIT" DataFormatString="{0:F2}">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ALLOWED_DAYS" HeaderText="ALLOWED_DAYS ">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="REPLACEMENT" HeaderText="REPLACEMENT">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="UNLOADING" HeaderText="UNLOADING">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:CommandField ShowEditButton="True" HeaderText="Edit">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:CommandField>
                            </Columns>
                            <FooterStyle BackColor="White"></FooterStyle>
                            <PagerStyle BackColor="Transparent"></PagerStyle>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#007395"
                                Font-Bold="True" ForeColor="White"></HeaderStyle>
                            <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333">
                            </AlternatingRowStyle>
                        </asp:GridView>
                    </asp:Panel>
                    <asp:DropDownList ID="DrpRoute" runat="server" Width="205px" CssClass="DropList"
                        OnSelectedIndexChanged="DrpRoute_SelectedIndexChanged" AutoPostBack="True" Visible="false">
                    </asp:DropDownList>
                    <asp:DropDownList ID="DrpMarket" Visible="false" runat="server" Width="205px" CssClass="DropList">
                    </asp:DropDownList>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
