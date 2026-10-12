using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommonLayer.Classes;

/// <summary>
/// Form For Value Reconciliation Report
/// </summary>
public partial class Forms_RptVolumeSale : System.Web.UI.Page
{
    GeoHierarchyController gController = new GeoHierarchyController();
    DistributorController ds = new DistributorController();
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
            this.LoadTown();
            this.LoadAssingned();
            this.LoadCustomer();
            this.LoadPrincipal();
            SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
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
        this.LoadTown();
        this.LoadAssingned();
        ScriptManager.GetCurrent(Page).SetFocus(ddlTerritory);
    }

    protected void ddlTerritory_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadTown();
        this.LoadAssingned();
        ScriptManager.GetCurrent(Page).SetFocus(DrpTown);
    }

    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadCustomer();
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
            clsWebFormUtil.FillDropDownList(this.DrpTown, dt, 0, 4);
        }
    }

    /// <summary>
    /// Loads Assigned Locations To Assigned ListBox on Location Tab
    /// </summary>
    private void LoadAssingned()
    {
        drpDistributor.Items.Clear();
        DataTable dtMasterDistributor = ds.SelectDistributorInfoByTownbyUser(Constants.IntNullValue, int.Parse(DrpTown.SelectedValue), int.Parse(this.Session["UserID"].ToString()), Convert.ToInt32(ddlRegion.SelectedValue), Convert.ToInt32(ddlZone.SelectedValue), Convert.ToInt32(ddlTerritory.SelectedValue), 1);
        clsWebFormUtil.FillDropDownList(drpDistributor, dtMasterDistributor, "DISTRIBUTOR_ID", "DISTRIBUTOR_NAME");
    }

    private void LoadCustomer()
    {
        ddlCustomer.Items.Clear();
        CustomerDataController mController = new CustomerDataController();
        DataTable dtCustomer = mController.SelectAllCustomer(Convert.ToInt32(drpDistributor.SelectedValue), Constants.IntNullValue, Convert.ToInt32(DrpTown.SelectedValue));
        ddlCustomer.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlCustomer, dtCustomer, "CUSTOMER_ID", "CUSTOMER_DETAIL");
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
        this.LoadAssingned();
        this.LoadCustomer();
        ScriptManager.GetCurrent(Page).SetFocus(drpDistributor);
    }

    private void ShowReport(int ReportType)
    {
        SAMSBusinessLayer.Classes.DocumentPrintController DPrint = new SAMSBusinessLayer.Classes.DocumentPrintController();
        RptSaleController RptSaleCtl = new RptSaleController();
        System.Text.StringBuilder sbTownIDs = new System.Text.StringBuilder();
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
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));

        SAMSBusinessLayer.Reports.CrpVolumeSalesDetail CrpReport = new SAMSBusinessLayer.Reports.CrpVolumeSalesDetail();

        DataSet ds = null;

        ds = RptSaleCtl.SelectVolumeSale(int.Parse(DrpPrincipal.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), Convert.ToDateTime(txtStartDate.Text + " 00:00:00"), Convert.ToInt32(rblType.SelectedValue), int.Parse(this.Session["UserId"].ToString()), Convert.ToInt32(ddlCustomer.SelectedValue), sbTownIDs.ToString());
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();

        CrpReport.SetParameterValue("ReportName", "Target Vs Achievement ( " + rblType.SelectedItem.Text + " )");
        CrpReport.SetParameterValue("Region", dtHierarchy.Rows[0]["Region"].ToString());
        CrpReport.SetParameterValue("Area", dtHierarchy.Rows[0]["Area"].ToString());
        CrpReport.SetParameterValue("Territory", dtHierarchy.Rows[0]["Territory"].ToString());
        CrpReport.SetParameterValue("Town", dtHierarchy.Rows[0]["Town"].ToString());
        CrpReport.SetParameterValue("Location", this.drpDistributor.SelectedItem.Text.ToString());
        CrpReport.SetParameterValue("Principal", this.DrpPrincipal.SelectedItem.Text.ToString());
        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("TargetDate", txtStartDate.Text);

        this.Session.Add("CrpReport", CrpReport);
        this.Session.Add("ReportType", 0);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }
}