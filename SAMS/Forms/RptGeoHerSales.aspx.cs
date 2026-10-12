using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommonLayer.Classes;

/// <summary>
/// Form For Value Reconciliation Report
/// </summary>
public partial class Forms_RptGeoHerSales : System.Web.UI.Page
{
    GeoHierarchyController gController = new GeoHierarchyController();
    DistributorTownController DisTown = new DistributorTownController();

    /// <summary>
    /// Page_Load Function
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadRegion();
            this.LoadZone();
            this.LoadTerritory();
            LoadTown();
            this.LoadAssingned();

            LoadPrincipal();
            SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtFromMonth.Text = SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime.ToString("MMM-yyyy");
            txtToMonth.Text = SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime.ToString("MMM-yyyy");
        }
    }

    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadZone();
        this.LoadTerritory();
        LoadTown();
        this.LoadAssingned();
        ScriptManager.GetCurrent(Page).SetFocus(ddlZone);
    }

    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadTerritory();
        this.LoadAssingned();
        this.LoadAssingned();
        ScriptManager.GetCurrent(Page).SetFocus(ddlTerritory);
    }

    protected void ddlTerritory_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadTown();
        this.LoadAssingned();
        ScriptManager.GetCurrent(Page).SetFocus(DrpTown);
    }

    protected void LoadRegion()
    {
        DataTable dt = gController.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, null, null, true, Constants.IntNullValue, Constants.Region, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        this.ddlRegion.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlRegion, dt, "GEO_ID", "GEO_NAME");
    }

    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        DrpPrincipal.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.DrpPrincipal, m_dt, 0, 1);
    }

    /// <summary>
    /// Loads Zones To Area Combo
    /// </summary>
    protected void LoadZone()
    {
        if (ddlRegion.Items.Count > 0)
        {
            ddlZone.Items.Clear();
            GeoHierarchyController gController = new GeoHierarchyController();
            ddlZone.Items.Add(new clsListItems("All", Constants.IntNullValue.ToString()));
            DataTable dt = gController.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, Convert.ToInt32(ddlRegion.SelectedValue.ToString()), null, null, true, Constants.IntNullValue, Constants.Zone, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(ddlZone, dt, "GEO_ID", "GEO_NAME");
        }
    }

    /// <summary>
    /// Loads Territories To Territory Combo
    /// </summary>
    protected void LoadTerritory()
    {
        if (ddlZone.Items.Count > 0)
        {
            ddlTerritory.Items.Clear();
            GeoHierarchyController gController = new GeoHierarchyController();
            ddlTerritory.Items.Add(new clsListItems("All", Constants.IntNullValue.ToString()));
            DataTable dt = gController.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, Convert.ToInt32(ddlZone.SelectedValue.ToString()), null, null, true, Constants.IntNullValue, Constants.Territory, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(ddlTerritory, dt, "GEO_ID", "GEO_NAME");
        }
    }

    protected void LoadTown()
    {
        if (ddlTerritory.Items.Count > 0)
        {
            DrpTown.Items.Clear();
            DataTable dt = gController.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, int.Parse(ddlTerritory.SelectedValue.ToString()), null, null, true, Constants.IntNullValue, Constants.Town, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
            DrpTown.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpTown, dt, 0, 4, false);
        }
    }

    /// <summary>
    /// Loads Assigned Locations To Assigned ListBox on Location Tab
    /// </summary>
    private void LoadAssingned()
    {
        drpDistributor.Items.Clear();
        UserController mUserController = new UserController();
        DistributorController ds = new DistributorController();
        DataTable dtMasterDistributor = ds.SelectDistributorInfoByTownbyUser(Constants.IntNullValue, int.Parse(DrpTown.SelectedValue), int.Parse(this.Session["UserID"].ToString()), Convert.ToInt32(ddlRegion.SelectedValue), Convert.ToInt32(ddlZone.SelectedValue), Convert.ToInt32(ddlTerritory.SelectedValue), 1);
        this.drpDistributor.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dtMasterDistributor, "DISTRIBUTOR_ID", "DISTRIBUTOR_NAME");
    }

    /// <summary>
    /// Shows Value Reconciliation in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }

    /// <summary>
    /// Shows Value Reconciliation in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }

    protected void DrpTown_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadAssingned();
        ScriptManager.GetCurrent(Page).SetFocus(drpDistributor);
    }

    private void ShowReport(int ReportType)
    {
        CrystalDecisions.CrystalReports.Engine.ReportDocument crpReport = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
        var dPrint = new DocumentPrintController();
        var rptCustomerCtl = new RptCustomerController();
        DataSet ds = null;
        var dt = dPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue));

        int TypeID = 0, GeoID = 0;
        if (DrpTown.SelectedValue != Constants.IntNullValue.ToString())
        {
            TypeID = 1;
            GeoID = Convert.ToInt32(DrpTown.SelectedValue);
        }
        else if (ddlTerritory.SelectedValue != Constants.IntNullValue.ToString())
        {

            TypeID = 2;
            GeoID = Convert.ToInt32(ddlTerritory.SelectedValue);
        }
        else if (ddlZone.SelectedValue != Constants.IntNullValue.ToString())
        {
            TypeID = 3;
            GeoID = Convert.ToInt32(ddlZone.SelectedValue);
        }
        else if (ddlRegion.SelectedValue != Constants.IntNullValue.ToString())
        {
            TypeID = 4;
            GeoID = Convert.ToInt32(ddlRegion.SelectedValue);
        }
        else
        {
            TypeID = 5;
        }

        DataTable dtHierarchy = DisTown.GetTownHierarchy(GeoID, TypeID);

        DateTime dtFrom = DateTime.Parse(txtStartDate.Text + " 00:00:00");
        DateTime dtTo = DateTime.Parse(txtEndDate.Text + " 23:59:59");

        if (rblType.SelectedValue == "2")
        {
            DateTime dtFromMonth = Convert.ToDateTime(txtFromMonth.Text);
            DateTime dtToMonth = Convert.ToDateTime(txtToMonth.Text);
            dtFrom = new DateTime(dtFromMonth.Year, dtFromMonth.Month, 1);
            dtTo = new DateTime(dtToMonth.Year, dtToMonth.Month, 1).AddMonths(1).AddDays(-1);
        }

        System.Text.StringBuilder sbDistributorIDs = new System.Text.StringBuilder();
        System.Text.StringBuilder sbTownIDs = new System.Text.StringBuilder();
        if (drpDistributor.SelectedValue == Constants.IntNullValue.ToString())
        {
            foreach (ListItem li in drpDistributor.Items)
            {
                sbDistributorIDs.Append(li.Value);
                sbDistributorIDs.Append(",");
            }
        }
        else
        {
            sbDistributorIDs.Append(drpDistributor.SelectedValue);
        }

        if (DrpTown.SelectedValue == Constants.IntNullValue.ToString())
        {
            foreach (ListItem li in DrpTown.Items)
            {
                sbTownIDs.Append(li.Value);
                sbTownIDs.Append(",");
            }
        }
        else
        {
            sbTownIDs.Append(DrpTown.SelectedValue);
        }

        ds = rptCustomerCtl.GetGeoHerSales(sbDistributorIDs.ToString(), int.Parse(DrpPrincipal.SelectedValue), sbTownIDs.ToString(), dtFrom, dtTo, int.Parse(ddlRegion.SelectedValue), int.Parse(ddlZone.SelectedValue), int.Parse(ddlTerritory.SelectedValue), int.Parse(DrpTown.SelectedValue));

        if (rblType.SelectedValue == "1")
        {
            crpReport = new SAMSBusinessLayer.Reports.CrpGeoHerSales();
        }
        else
        {
            crpReport = new SAMSBusinessLayer.Reports.CrpGeoHerSales2();
        }
        crpReport.SetDataSource(ds);
        crpReport.Refresh();

        crpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        crpReport.SetParameterValue("DateFrom", txtStartDate.Text);
        crpReport.SetParameterValue("DateTo", txtEndDate.Text);
        crpReport.SetParameterValue("Principal", DrpPrincipal.SelectedItem.Text);
        crpReport.SetParameterValue("Distributor", drpDistributor.SelectedItem.Text);
        crpReport.SetParameterValue("Region", dtHierarchy.Rows[0]["Region"].ToString());
        crpReport.SetParameterValue("Area", dtHierarchy.Rows[0]["Area"].ToString());
        crpReport.SetParameterValue("Territory", dtHierarchy.Rows[0]["Territory"].ToString());
        crpReport.SetParameterValue("Town", dtHierarchy.Rows[0]["Town"].ToString());

        this.Session.Add("ReportType", ReportType);
        this.Session.Add("CrpReport", crpReport);

        const string url = "'Default.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        var cstype = this.GetType();
        var cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }

    protected void rblType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblDateFrom.Visible = true;
        lblDateTo.Visible = true;
        lblMonthFrom.Visible = true;
        lblMonthTo.Visible = true;
        txtStartDate.Visible = true;
        txtEndDate.Visible = true;
        txtFromMonth.Visible = true;
        txtToMonth.Visible = true;
        ibtnStartDate.Visible = true;
        ibnEndDate.Visible = true;
        ibFromMonth.Visible = true;
        ibToMonth.Visible = true;

        if (rblType.SelectedValue == "1")
        {
            lblMonthFrom.Visible = false;
            lblMonthTo.Visible = false;
            txtFromMonth.Visible = false;
            txtToMonth.Visible = false;
            ibFromMonth.Visible = false;
            ibToMonth.Visible = false;
        }
        else
        {
            lblDateFrom.Visible = false;
            lblDateTo.Visible = false;
            txtStartDate.Visible = false;
            txtEndDate.Visible = false;
            ibtnStartDate.Visible = false;
            ibnEndDate.Visible = false;
        }
        ScriptManager.GetCurrent(Page).SetFocus(ddlRegion);
    }
}