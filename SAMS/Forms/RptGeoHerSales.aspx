<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="RptGeoHerSales.aspx.cs" Inherits="Forms_RptGeoHerSales" Title="SAMS::Geo Hierarchy Sales Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
<script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
<script language="JavaScript" type="text/javascript">
    function pageLoad() {
        $("select").searchable();

    }

    function onCalendarHidden() {
        var cal = $find("calendar1");

        if (cal._monthsBody) {
            for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                var row = cal._monthsBody.rows[i];
                for (var j = 0; j < row.cells.length; j++) {
                    Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call);
                }
            }
        }
    }

    function onCalendarShown() {

        var cal = $find("calendar1");

        cal._switchMode("months", true);

        if (cal._monthsBody) {
            for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                var row = cal._monthsBody.rows[i];
                for (var j = 0; j < row.cells.length; j++) {
                    Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call);
                }
            }
        }
    }

    function call(eventElement) {
        var target = eventElement.target;
        switch (target.mode) {
            case "month":
                var cal = $find("calendar1");
                cal._visibleDate = target.date;
                cal.set_selectedDate(target.date);
                //cal._switchMonth(target.date);
                cal._blur.post(true);
                cal.raiseDateSelectionChanged();
                break;
        }
    }

    function onCalendarHidden2() {
        var cal = $find("calendar2");

        if (cal._monthsBody) {
            for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                var row = cal._monthsBody.rows[i];
                for (var j = 0; j < row.cells.length; j++) {
                    Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call2);
                }
            }
        }
    }

    function onCalendarShown2() {

        var cal = $find("calendar2");

        cal._switchMode("months", true);

        if (cal._monthsBody) {
            for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                var row = cal._monthsBody.rows[i];
                for (var j = 0; j < row.cells.length; j++) {
                    Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call2);
                }
            }
        }
    }

    function call2(eventElement) {
        var target = eventElement.target;
        switch (target.mode) {
            case "month":
                var cal = $find("calendar2");
                cal._visibleDate = target.date;
                cal.set_selectedDate(target.date);
                //cal._switchMonth(target.date);
                cal._blur.post(true);
                cal.raiseDateSelectionChanged();
                break;
        }
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
                                            <asp:RadioButtonList ID="rblType" runat="server" RepeatDirection="Horizontal" Width="230px"
                                            AutoPostBack="true" OnSelectedIndexChanged="rblType_SelectedIndexChanged">
                                                <asp:ListItem Value="1" Text="Distributor Wise" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Month Wise"></asp:ListItem>
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
                                            <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" CssClass="DropList">
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
                                                <asp:Label ID="lblDateFrom" runat="server" Width="76px" Height="13px" Text="From Date"></asp:Label></strong>
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
                                            <strong>
                                                <asp:Label ID="lblDateTo" runat="server" Width="76px" Height="13px" Text="To Date"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1px" align="left">
                                        </td>
                                        <td style="height: 25px" align="left">
                                            &nbsp;<asp:TextBox ID="txtEndDate" onkeyup="BlockStartDateKeyPress()" runat="server"
                                                Width="150px" MaxLength="10"></asp:TextBox>
                                            <asp:ImageButton ID="ibnEndDate" runat="server" Width="16px" ImageUrl="~/App_Themes/Granite/Images/date.gif">
                                            </asp:ImageButton>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td align="left">
                                        </td>
                                        <td style="width: 95px" align="left">
                                            <strong>
                                                <asp:Label ID="lblMonthFrom" runat="server" Height="13px" Text="From Month" Visible="false"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1px" align="left">
                                        </td>
                                        <td style="height: 25px" align="left">
                                            &nbsp;<asp:TextBox ID="txtFromMonth" onkeyup="BlockStartDateKeyPress()" runat="server" Visible="false"
                                                Width="150px" MaxLength="10"></asp:TextBox>
                                            <asp:ImageButton ID="ibFromMonth" runat="server" Width="16px" ImageUrl="~/App_Themes/Granite/Images/date.gif" Visible="false">
                                            </asp:ImageButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                        </td>
                                        <td style="width: 95px" align="left">
                                            <strong>
                                                <asp:Label ID="lblMonthTo" runat="server" Height="13px" Text="To Month" Visible="false"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1px" align="left">
                                        </td>
                                        <td style="height: 25px" align="left">
                                            &nbsp;<asp:TextBox ID="txtToMonth" onkeyup="BlockStartDateKeyPress()" runat="server"
                                                Width="150px" MaxLength="10" Visible="false"></asp:TextBox>
                                            <asp:ImageButton ID="ibToMonth" runat="server" Width="16px" ImageUrl="~/App_Themes/Granite/Images/date.gif" Visible="false">
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
                                            <cc1:CalendarExtender ID="CEEndDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibnEndDate"
                                                TargetControlID="txtEndDate">
                                            </cc1:CalendarExtender>

                                            <cc1:CalendarExtender ID="ceFromMonth" runat="server" Format="MMM-yyyy"
                                                PopupButtonID="ibFromMonth" TargetControlID="txtFromMonth" BehaviorID="calendar1" OnClientShown="onCalendarShown"  OnClientHidden="onCalendarHidden">
                                            </cc1:CalendarExtender>
                                            <cc1:CalendarExtender ID="ceToMonth" runat="server" Format="MMM-yyyy" PopupButtonID="ibToMonth"
                                                TargetControlID="txtToMonth" BehaviorID="calendar2" OnClientShown="onCalendarShown2"  OnClientHidden="onCalendarHidden2">
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
