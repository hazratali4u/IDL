<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="RptVolumeSale.aspx.cs" Inherits="Forms_RptVolumeSale" Title="SAMS:: Target Vs Achievement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
<script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
<script language="javascript" type="text/javascript">
    function pageLoad() {
        $("select").searchable();
    }
</script>
    <div id="right_data">
        <table width="100%">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table>
                                <tbody>  
                                    <tr>
                                        <td align="left">
                                        </td>
                                        <td style="width: 95px" align="left">
                                            <strong>
                                                <asp:Label ID="Label1" runat="server" Text="Report Type"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width: 1px" align="left">
                                        </td>
                                        <td style="height: 25px" align="left">
                                            <asp:RadioButtonList ID="rblType" runat="server" RepeatDirection="Horizontal" Width="230px">
                                                <asp:ListItem Value="1" Text="Value Wise" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Quantity Wise"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>                                  
                                    <tr>
                                        <td align="left">
                                        </td>
                                        <td style="width: 95px" align="left">
                                            <strong>
                                                <asp:Label ID="lblRegion" runat="server" Text="Region"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width: 1px" align="left">
                                        </td>
                                        <td style="height: 25px" align="left">
                                            <asp:DropDownList ID="ddlRegion" runat="server" Width="200px" 
                                                AutoPostBack="True" onselectedindexchanged="ddlRegion_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                        </td>
                                        <td style="width: 95px" align="left">
                                            <strong>
                                                <asp:Label ID="lblZone" runat="server" Text="Area"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width: 1px" align="left">
                                        </td>
                                        <td style="height: 25px" align="left">
                                            <asp:DropDownList ID="ddlZone" runat="server" Width="200px" 
                                                AutoPostBack="True" onselectedindexchanged="ddlZone_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                        </td>
                                        <td style="width: 95px" align="left">
                                            <strong>
                                                <asp:Label ID="lblTerritory" runat="server" Text="Territory"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width: 1px" align="left">
                                        </td>
                                        <td style="height: 25px" align="left">
                                            <asp:DropDownList ID="ddlTerritory" runat="server" Width="200px" 
                                                AutoPostBack="True" onselectedindexchanged="ddlTerritory_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                    <td></td>
                                        <td style="width: 31px; height: 28px" align="left">
                                            <strong>
                                                <asp:Label ID="Label16" runat="server" Text="Town" Width="58px"></asp:Label></strong>
                                        </td>
                                        <td></td>
                                        <td style="height: 16px; width: 2px;" align="left">
                                            <asp:DropDownList ID="DrpTown" runat="server" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="DrpTown_SelectedIndexChanged"
                                                CssClass="DropList">
                                            </asp:DropDownList>
                                        </td>
                                        </tr>                                    
                                    <tr>
                                        <td align="left">
                                        </td>
                                        <td style="width: 95px" align="left">
                                            <strong>
                                                <asp:Label ID="lbltoLocation" runat="server" Width="94px" Text="Location" CssClass="lblbox"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1px" align="left">
                                        </td>
                                        <td style="height: 25px" align="left">
                                            <asp:DropDownList ID="drpDistributor" runat="server" Width="200px"
                                                AutoPostBack="true" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                        </td>
                                        <td style="width: 95px" align="left">
                                            <strong>
                                                <asp:Label ID="lblCustomer" runat="server" Text="Distributor">
                                            </asp:Label></strong>
                                        </td>
                                        <td style="width: 1px" align="left">
                                        </td>
                                        <td style="height: 25px" align="left">
                                            <asp:DropDownList ID="ddlCustomer" runat="server" Width="200px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                        </td>
                                        <td style="width: 95px" align="left">
                                            <strong>
                                                <asp:Label ID="Label6" runat="server" Width="78px" Text="Principal" CssClass="lblbox"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1px" align="left">
                                        </td>
                                        <td style="height: 25px" align="left">
                                            <asp:DropDownList ID="DrpPrincipal" runat="server" Width="200px" CssClass="DropList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>                                       
                                    <tr>
                                        <td align="left">
                                        </td>
                                        <td style="width: 95px" align="left">
                                            <strong>
                                                <asp:Label ID="lblDateFrom" runat="server" Width="76px" Height="13px" Text="To Date"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1px" align="left">
                                        </td>
                                        <td style="height: 25px" align="left">
                                            &nbsp;<asp:TextBox ID="txtStartDate" onkeyup="BlockStartDateKeyPress()" runat="server"
                                                Width="150px" CssClass="txtBox" MaxLength="10"></asp:TextBox>
                                            <asp:ImageButton ID="ibtnStartDate" runat="server" Width="16px" ImageUrl="~/App_Themes/Granite/Images/date.gif">
                                            </asp:ImageButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                        </td>
                                        <td style="width: 95px" align="left">
                                        </td>
                                        <td style="width: 1px" align="left">
                                        </td>
                                        <td style="height: 25px" align="left">
                                            <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
                                            <cc1:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy"
                                                PopupButtonID="ibtnStartDate" TargetControlID="txtStartDate">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    &nbsp; &nbsp;
                    <asp:Button ID="btnViewPDF" runat="server" CssClass="Button" Width="90" Text="View PDF"
                        OnClick="btnViewPDF_Click" OnClientClick="return CheckReportType();" />
                    <asp:Button ID="btnViewExcel" runat="server" CssClass="Button" Width="90" Text="View Excel"
                        OnClick="btnViewExcel_Click" OnClientClick="return CheckReportType();" />                         
                </td>
            </tr>
        </table>
        &nbsp;
    </div>
</asp:Content>
