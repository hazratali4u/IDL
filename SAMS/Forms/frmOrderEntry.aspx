<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmOrderEntry.aspx.cs" Inherits="Forms_frmOrderEntry" Title="SAMS :: Order/Invoice Step 2"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
<script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">

        function pageLoad() {

            $("select").searchable();
        }

            $('#<%=ddlBatch.ClientID %>').change(function () {
                document.getElementById("<%= lblInStore.ClientID %>").innerHTML = '0';
                document.getElementById("<%= lblInBatch.ClientID %>").innerHTML = '0';
                var obj = jQuery.parseJSON($("#<%= hfBatchNo.ClientID %>").val());
                
                for (var i = 0; i < obj.length; i++) {
                    var item = obj[i];
                    if (item.SKU_ID == $('#<%=ddlBatch.ClientID %> option:selected').val() && item.BATCH_NO == $('#<%=ddlBatch.ClientID %> option:selected').text()) {
                        document.getElementById("<%= txtExpDate.ClientID %>").value = item.EXPIRY_DATE;
                        var unitprice = $('#<%=ddlSKuCde.ClientID %> option:selected').text();
                        unitprice = unitprice.substring(unitprice.indexOf(':') + 1);
                        document.getElementById("<%= txtUnitRate.ClientID %>").value = unitprice;
                       
                        break;
                    }
                }
                document.getElementById("<%= hfBatchValue.ClientID%>").value = $('#<%=ddlBatch.ClientID %> option:selected').text();
                document.getElementById("<%= ddlBatch.ClientID %>").focus();
            });
            $('#<%=ddlSKuCde.ClientID %>').change(function () {
                var obj = jQuery.parseJSON($("#<%= hfBatchNo.ClientID %>").val());
                var ddlBatch = $('#<%=ddlBatch.ClientID %>');
                var unitprice = $('#<%=ddlSKuCde.ClientID %> option:selected').text();
                unitprice = unitprice.substring(unitprice.indexOf(':') + 1);
                document.getElementById("<%= txtUnitRate.ClientID %>").value = unitprice;
                $('#<%=ddlBatch.ClientID %> option').remove();
               
                for (var i = 0; i < obj.length; i++) {
                    var item = obj[i];
                    document.getElementById("<%= lblInStore.ClientID %>").innerHTML = item.CLOSING_STOCK;
                    document.getElementById("<%= lblInBatch.ClientID %>").innerHTML = item.UNITS_IN_STOCK;
                    if (item.SKU_ID == $('#<%=ddlSKuCde.ClientID %> option:selected').val()) {
                        var opts = $("<option />");
                        opts = $("<option />").html(item.BATCH_NO).attr("value", item.SKU_ID);
                        ddlBatch.append(opts);
                      break;
                    }
                }
                document.getElementById("<%= hfBatchValue.ClientID%>").value = $('#<%=ddlBatch.ClientID %> option:selected').text();
                document.getElementById("<%= ddlSKuCde.ClientID %>").focus();
            });
           // document.getElementById("<%= ddlStore.ClientID %>").focus();
        
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(startRequest);

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);

        function startRequest(sender, e) {

            document.getElementById('<%=btnSave.ClientID%>').disabled = true;
            document.getElementById('<%=btnCalculate.ClientID%>').disabled = true;

        }

        function endRequest(sender, e) {

            document.getElementById('<%=btnSave.ClientID%>').disabled = false;
            document.getElementById('<%=btnCalculate.ClientID%>').disabled = false;
        }


        function ValidateForm() {
            var str;

           
            str = document.getElementById('<%=txtQuantity.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter Quantity');
                return false;
            }
            str = document.getElementById('<%=txtTransporter.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter Transporter');
                return false;
            }
            str = document.getElementById('<%=txtTruckNo.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter Truck No');
                return false;
            }
            str = document.getElementById('<%=txtTruckSize.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter Truck Size');
                return false;
            }
            str = document.getElementById('<%=txtDriverCellNo.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter Driver Cell No');
                return false;
            }

//            var InBatch = document.getElementById('<%=lblInBatch.ClientID%>').innerHTML;
//            var Qty = document.getElementById('<%=txtQuantity.ClientID%>').value;
//            var PrvQty = document.getElementById("<%=hfPrevQty.ClientID %>").value;
//            if (parseInt(Qty) > parseInt(InBatch) + parseInt(PrvQty)) {
//                alert('Quantity can not be greater than available Stock.');
//                document.getElementById("<%= txtQuantity.ClientID %>").focus();
//                return false;
//}
          //  document.getElementById("<%= ddlSKuCde.ClientID %>").focus();
            return true;
        }
       
        function ClearSelection(lb) {
            lb.selectedIndex = -1;
        }

 </script>


    <div id="right_data">
        <div>
            <span class="heading">Order/Invoice Step 2</span>
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td align="left">
                  
                        <div style="z-index: 101; left: 612px; width: 100px; position: absolute; top: 369px;
                            height: 100px">
                            &nbsp;<asp:Panel ID="Panel21" runat="server">
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                                    <ProgressTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                            Width="31px" />
                                        Wait Update
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </asp:Panel>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td valign="top" align="left">
                                                &nbsp;
                                            </td>
                                            <td valign="top" align="left" >
                                                &nbsp;
                                            </td>
                                            <td></td>
                                            <td valign="top"><strong>
                                                    Dispatch No
                                                </strong></td>
                                            <td valign="top">
                                                <asp:TextBox ID="txtDispatchNo" runat="server" CssClass="uppercase" 
                                                    Visible="true" TabIndex="5" ></asp:TextBox></td> 
                                        </tr>
                                       
                                          <tr>
                                            <td  align="left">
                                                <strong>
                                                    <asp:Label ID="Label93" runat="server" Text="Warehouse" Width="74px"></asp:Label>
                                                </strong>
                                            </td>
                                            <td  align="left" colspan="2">
                                                <asp:DropDownList ID="ddlStore" runat="server" TabIndex="0" AutoPostBack="true" OnSelectedIndexChanged="ddlStore_SelectedIndexChanged"
                                                    Width="231px">
                                                </asp:DropDownList>
                                            </td> 
                                            <td ><strong>
                                                    Dispatch Date
                                                </strong></td>
                                            <td style="height: 21px">
                                                <asp:TextBox ID="txtDispatchdate" runat="server" MaxLength="1" onkeyup="BlockFromDateKeyPress()"
                                                    ReadOnly="True" Style="text-align: justify" TabIndex="6" ></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="txtFromdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                                                    PopupButtonID="ImgBntFromCalc" TargetControlID="txtDispatchdate">
                                                   
                                                </ajaxToolkit:CalendarExtender> <asp:ImageButton ID="ImgBntFromCalc" runat="server" CausesValidation="False" ImageUrl="~/App_Themes/Granite/Images/date.gif" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" >
                                                <strong>
                                                    <asp:Label ID="lblDocumentNo" runat="server" Text="Order No" Width="74px"></asp:Label>
                                                </strong>
                                            </td>
                                            <td align="left" colspan="2" >
                                                <asp:DropDownList ID="drpDocumentNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpDocumentNo_SelectedIndexChanged"
                                                    Width="231px" TabIndex="1">
                                                </asp:DropDownList>
                                                <strong>
                                                    <asp:Label ID="lblBillBook" runat="server"  Text="Bill Book No:" Visible="false"></asp:Label>
                                                </strong>
                                                <asp:TextBox ID="txtBillBookNo" runat="server" CssClass="uppercase" MaxLength="10"
                                                    Visible="false"></asp:TextBox>
                                                <asp:CheckBox ID="ChbDiscount" runat="server" AutoPostBack="True" Checked="True"
                                                    Text="Promotion" Visible="False" Width="90px" />
                                            </td><td style="width:94px"><strong>
                                                    Truck No
                                                </strong></td>
                                            <td valign="top">
                                                <asp:TextBox ID="txtTruckNo" runat="server" CssClass="uppercase" 
                                                    Visible="true" TabIndex="7"></asp:TextBox>
                                        </tr>
                                        <tr>
                                            <td align="left" valign="middle">
                                                <strong>
                                                    <asp:Label ID="Label11" runat="server" Width="77px" Text="Order Type"></asp:Label></strong>
                                            </td>
                                            <td align="left" valign="top">
                                                <asp:RadioButtonList ID="RblPayMode" runat="server" Height="1px" RepeatDirection="Horizontal"
                                                    Width="228px" TabIndex="2">
                                                    <asp:ListItem  Value="214">Cash</asp:ListItem>
                                                    <asp:ListItem Value="215">Credit</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="216">Advance</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td align="left" colspan="1" valign="top">
                                            </td>  
                                           </td>    
                                                    <td><strong>
                                                    Transporter
                                                </strong></td>
                                            <td valign="top">
                                                <asp:TextBox ID="txtTransporter" runat="server" CssClass="uppercase" 
                                                    Visible="true" TabIndex="8"></asp:TextBox></td> 
                                        </tr>
                                        <tr>
                                            <td align="left" style="height: 23px">
                                                <strong>
                                                    <asp:Label ID="Label1" runat="server" Text="Customer" Width="74px"></asp:Label>
                                                </strong>
                                            </td>
                                            <td align="left" style="height: 23px">
                                                <asp:DropDownList ID="ddlCustomer" runat="server" Width="231px" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged" TabIndex="3">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left" style="height: 23px">
                                                <asp:TextBox ID="txtDiscountType" runat="server" ReadOnly="True" Width="140px" 
                                                    TabIndex="4"></asp:TextBox>
                                            </td>  
                                             <td ><strong>
                                                   Truck Size
                                                </strong></td>
                                            <td >
                                                <asp:TextBox ID="txtTruckSize" runat="server" CssClass="uppercase" 
                                                    Visible="true" TabIndex="9"></asp:TextBox></td>
                                        </tr>
                                       <tr>
                                           <td></td>
                                            <td></td>
                                             <td></td>
                                              <td><strong>
                                                    Driver Cell No
                                                </strong></td>
                                               <td> <asp:TextBox ID="txtDriverCellNo" runat="server" CssClass="uppercase" 
                                                    Visible="true" TabIndex="10"></asp:TextBox></td>

                                       </tr>
                                        
                                             
                                          
                                    </tbody>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 10px;">
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Panel ID="Panel5" runat="server" >
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <table width="80%">
                                        <tr>
                                            <td style="width: 20%">
                                                <strong>In Warehouse:</strong> <u>
                                                    <asp:Label ID="lblInStore" runat="server" Text="0"></asp:Label>
                                                    <asp:HiddenField ID="hfPrevQty" Value="0" runat="server" />
                                                </u>
                                            </td>
                                            <td style="width: 20%">
                                                <strong>In Batch:</strong> <u>
                                                    <asp:Label ID="lblInBatch" runat="server" Text="0"></asp:Label>
                                                </u>
                                            </td>
                                            <td style="width: 20%">
                                            </td>
                                            <td style="width: 40%">
                                            </td>
                                        </tr>
                                    </table>
                                    <table>
                                        <tr>
                                            
                                            <td style="background-color:#006699; color:White; padding:2px;" >
                                               <strong>SKU Name</strong>
                                            </td>
                                            <td align="center"  style="background-color:#006699; color:White; padding:2px;">
                                                <strong>Batch No</strong> 
                                            </td>
                                            <td style="background-color:#006699; color:White; padding:2px;">
                                               <strong> Quantity</strong>
                                            </td>
                                            <td style="background-color:#006699; color:White; padding:2px;">
                                              <strong>  Unit Price</strong>
                                            </td>
                                            <td align="center"style="background-color:#006699; color:White; padding:2px;">
                                              <strong>  Exp Date</strong>
                                            </td>
                                            <td style="background-color:#006699; color:White; padding:2px;">
                                               <strong> Amount</strong>
                                            </td>
                                            <td >
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            
                                            <td>
                                                   <asp:DropDownList ID ="ddlSKuCde" runat="server" AutoPostBack="true" 
                                                       Width="260px" onselectedindexchanged="ddlSKuCde_SelectedIndexChanged" 
                                                       TabIndex="10"></asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlBatch" runat="server" Width="56px" 
                                                    TabIndex="11">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtQuantity" runat="server"  
                                                    Width="55px" TabIndex="12"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtUnitRate" runat="server" Width="56px" Enabled="false" 
                                                    TabIndex="13" ontextchanged="txtUnitRate_TextChanged"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtExpDate" runat="server" Width="62px" Enabled="false" 
                                                    TabIndex="14"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAmount" runat="server" Width="62px" Enabled="false" 
                                                    TabIndex="15"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnSave" runat="server" Font-Size="8pt" OnClick="btnSave_Click" Text="Add Sku"
                                                    Width="100px" AccessKey="A" CssClass="Button" TabIndex="16" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="8">
                                                <asp:Panel ID="Panel2" runat="server" Height="130px" ScrollBars="Vertical" Width="690px"
                                                    BorderColor="Silver" BorderStyle="Groove" BorderWidth="1px">
                                                    <asp:GridView ID="GrdPurchase" runat="server" ForeColor="SteelBlue" BackColor="White"
                                                        HorizontalAlign="Center" AutoGenerateColumns="False" BorderColor="White" ShowHeader="False"
                                                        OnRowDeleting="GrdPurchase_RowDeleting" OnRowEditing="GrdPurchase_RowEditing">
                                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                            PreviousPageText="Previous"></PagerSettings>
                                                        <Columns>
                                                            <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_CODE" HeaderText="SKU Code">
                                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_NAME" HeaderText="SKU Name">
                                                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid"
                                                                    Width="260px"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="BATCH_NO" HeaderText="Batch No">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" Width="55px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="QUANTITY_UNIT" HeaderText="Quantity">
                                                                <ItemStyle HorizontalAlign="Right" BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid"
                                                                    Width="50px"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="UNIT_PRICE" HeaderText="PRICE" DataFormatString="{0:F2}">
                                                                <ItemStyle HorizontalAlign="Right" BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid"
                                                                    Width="60px"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ExpDate" HeaderText="ExpDate">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" Width="90px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:F2}">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                                    Width="60px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="QUANTITY_CTN" HeaderText="QUANTITY_CTN">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>

                                                             <asp:BoundField DataField="CLAIM_PER" HeaderText="PRICE" DataFormatString="{0:F2}">
                                                              <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:CommandField ShowEditButton="True" HeaderText="Edit">
                                                                <ItemStyle BorderColor="Silver" BorderWidth="2px" Width="35px"></ItemStyle>
                                                            </asp:CommandField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnDelete" runat="server" Text="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                        CommandName="Delete"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid" Width="40px">
                                                                </ItemStyle>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </td>
                    <td style="width: 100px">
                        <asp:HiddenField ID="hfBillBookNo" runat="server" />
                         
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td align="left">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td valign="top" align="left" colspan="2" rowspan="7">
                                                <strong>
                                                    <asp:Label ID="Label10" runat="server" Width="60px" Text="Free SKU"></asp:Label></strong><asp:Panel
                                                        ID="Panel4" runat="server" Width="350px" Height="90px" BorderColor="Silver" BorderStyle="Groove"
                                                        BorderWidth="1px">
                                                        <asp:GridView ID="GrdFreeSKU" runat="server" Width="100%" ForeColor="Silver" CssClass="gridRow2"
                                                            BorderColor="White" BackColor="White" AutoGenerateColumns="False" HorizontalAlign="Center">
                                                            <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                                PreviousPageText="Previous"></PagerSettings>
                                                            <RowStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" ForeColor="Black">
                                                            </RowStyle>
                                                            <Columns>
                                                                <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SKU_Code" HeaderText="SKU Code">
                                                                    <ItemStyle Width="80px" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px">
                                                                    </ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SKU_Name" HeaderText="SKU Name">
                                                                    <ItemStyle Width="200px" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px">
                                                                    </ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Quantity" HeaderText="Qty">
                                                                    <ItemStyle Width="50px" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px">
                                                                    </ItemStyle>
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="tblhead"></HeaderStyle>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                            </td>
                                            <td style="height: 20px" align="left">
                                            </td>
                                            <td style="height: 20px; width:90px;" align="left">
                                                <strong>
                                                    Gross Sale</strong>
                                            </td>
                                            <td style="width: 7px; height: 20px">
                                                <asp:TextBox ID="txtGrossAmount" runat="server" Width="150px" ForeColor="Black" Font-Bold="True"
                                                    CssClass="txtBoxnNum" ReadOnly="True" TabIndex="-1"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                            </td>
                                            <td align="left">
                                                <strong>
                                                    Replacement</strong>
                                            </td>
                                            <td style="width: 7px">
                                                <asp:TextBox ID="numtxtTotalExtraDiscnt" runat="server" Width="150px" ForeColor="Black"
                                                    Font-Bold="True" CssClass="txtBoxnNum" ReadOnly="True" TabIndex="-1"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 18px" align="left">
                                            </td>
                                            <td style="height: 18px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label51" runat="server" Width="110px" Text="Discount"></asp:Label></strong>
                                                     <strong>
                                                    <asp:Label ID="Label8" runat="server" Width="116px" Text="Claimable  Discount" Visible="false"></asp:Label></strong>
                                            </td>
                                            <td style="width: 7px; height: 18px">
                                                <asp:TextBox ID="numTxtTotalStndrdDiscnt" runat="server" Width="150px" ForeColor="Black"
                                                    Font-Bold="True" CssClass="txtBoxnNum" ReadOnly="True" TabIndex="-1"></asp:TextBox>
                                                       <asp:TextBox ID="numtxtUnClaimabledist" runat="server" Width="150px" ForeColor="Black"
                                                    Font-Bold="True" CssClass="txtBoxnNum" ReadOnly="True" Visible ="false" 
                                                    TabIndex="23"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td >
                                            </td>
                                            <td style="height: 20px" align="left">
                                               
                                                <strong>
                                                Unloading Charges
                                                </strong>
                                               
                                            </td>
                                            <td style="width: 7px; height: 20px" align="right" >
                                             
                                                <asp:TextBox ID="numtxtUnLoadingCharges" runat="server" CssClass="txtBoxnNum" 
                                                    Font-Bold="True" ForeColor="Black" ReadOnly="True" Visible="true" 
                                                    Width="150px" TabIndex="-1"></asp:TextBox>
                                             
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 1px; height: 20px" valign="top">
                                            </td>
                                            <td style="width: 1px; height: 20px" align="left">
                                                <strong>
                                                   GST Amount</strong>
                                            </td>
                                            <td style="width: 7px; height: 20px" align="right">
                                                <asp:TextBox ID="numTxtTotalGST" runat="server" Width="150px" ForeColor="Black" Font-Bold="True"
                                                    CssClass="txtBoxnNum" ReadOnly="True" TabIndex="-1"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 1px; height: 20px" valign="top">
                                            </td>
                                            <td style="height: 20px" align="left">
                                                <strong>
                                                    TST Amount</strong>
                                            </td>
                                            <td style="width: 7px; height: 20px" align="right">
                                                <asp:TextBox ID="numTxtTotalTST" runat="server" Width="150px" ForeColor="Black" Font-Bold="True"
                                                    CssClass="txtBoxnNum" ReadOnly="True" TabIndex="-1"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 1px; height: 20px" valign="top">
                                            </td>
                                            <td style="width: 1px; height: 20px" align="left">
                                                <strong>
                                                   Net Amount</strong>
                                            </td>
                                            <td style="width: 7px; height: 20px" align="right">
                                                <asp:TextBox ID="numTxtTotlAmnt" runat="server" Width="150px" ForeColor="Black" Font-Bold="True"
                                                    CssClass="txtBoxnNum" ReadOnly="True" TabIndex="-1"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="2">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td style="width: 100px">
                                                                <asp:Button AccessKey="C" ID="btnCalculate" TabIndex="100" OnClick="btnCalculate_Click"
                                                                    runat="server" Width="100px" Font-Size="8pt" Text="Calculate" Enabled="False"
                                                                    CssClass="Button" />
                                                            </td>
                                                            <td style="width: 100px">
                                                                <asp:Button AccessKey="S" ID="btnSaveOrder" TabIndex="101" OnClick="btnSaveOrder_Click"
                                                                    runat="server" Width="110px" Font-Size="8pt" Text="Save Order" Enabled="False"
                                                                    CssClass="Button" />
                                                            </td>
                                                            <td style="width: 100px">
                                                                <asp:Button AccessKey="H" ID="btnCancel" TabIndex="102" runat="server" Width="110px"
                                                                    Font-Size="8pt" Text="Home" OnClick="btnCancel_Click" CssClass="Button" />
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                            <td style="width: 1px" valign="top" align="left">
                                                &nbsp; &nbsp; &nbsp;&nbsp;
                                            </td>
                                            <td style="width: 1px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label5" runat="server" Width="105px" Text="Cash Received" Visible ="false"></asp:Label></strong>
                                            </td>
                                            <td style="width: 7px" align="right">
                                                <asp:TextBox ID="txtCashReceived" runat="server" Width="150px" ForeColor="Black"
                                                    Font-Bold="True" CssClass="txtBoxnNum" Enabled="False" Visible ="false" 
                                                    TabIndex="-1"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                   <asp:HiddenField ID="hfSKUID" runat="server" />
                                                <asp:HiddenField ID="hfBatchNo" runat="server" />
                                                <asp:HiddenField ID="hfBatchValue" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        &nbsp;&nbsp;
                        <asp:TextBox ID="numTxtTotalSED" runat="server" Font-Bold="False" ForeColor="Black"
                            Width="139px" ReadOnly="True" Visible="False"></asp:TextBox>&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
