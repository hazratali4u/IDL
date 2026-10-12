using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommonLayer.Classes;
using SAMSBusinessLayer.Reports;

/// <summary>
/// Form For  Party Statement Report
/// </summary>
public partial class Forms_RptCustomerStatment : System.Web.UI.Page
{
    GeoHierarchyController gController = new GeoHierarchyController();
    DistributorTownController DisTown = new DistributorTownController();
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.LoadRegion();
            this.LoadZone();
            this.LoadTerritory();
            this.LoadTown();
            this.LoadAssingned();
            this.LoadCustomer();
            SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }

    protected void LoadRegion()
    {
        DataTable dt = gController.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, null, null, true, Constants.IntNullValue, Constants.Region, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        this.ddlRegion.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlRegion, dt, "GEO_ID", "GEO_NAME");
    }
    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadZone();
        this.LoadTerritory();
        this.LoadTown();
        this.LoadAssingned();
        this.LoadCustomer();
        ScriptManager.GetCurrent(Page).SetFocus(ddlZone);
    }

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
    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadTerritory();
        this.LoadTown();
        this.LoadAssingned();
        this.LoadCustomer();
        ScriptManager.GetCurrent(Page).SetFocus(ddlTerritory);
    }

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
    protected void ddlTerritory_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadTown();
        this.LoadAssingned();
        this.LoadCustomer();
        ScriptManager.GetCurrent(Page).SetFocus(DrpTown);
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
    protected void DrpTown_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadAssingned();
        LoadCustomer();
        ScriptManager.GetCurrent(Page).SetFocus(drpDistributor);
    }



    private void LoadAssingned()
    {
        drpDistributor.Items.Clear();
        UserController mUserController = new UserController();
        DistributorController ds = new DistributorController();
        DataTable dtMasterDistributor = ds.SelectDistributorInfoByTownbyUser(Constants.IntNullValue, int.Parse(DrpTown.SelectedValue), int.Parse(this.Session["UserID"].ToString()), Convert.ToInt32(ddlRegion.SelectedValue), Convert.ToInt32(ddlZone.SelectedValue), Convert.ToInt32(ddlTerritory.SelectedValue), 1);
        this.drpDistributor.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dtMasterDistributor, "DISTRIBUTOR_ID", "DISTRIBUTOR_NAME");
    }
    private void LoadCustomer()
    {
        DrpCustomer.Items.Clear();
        if (drpDistributor.Items.Count > 0 )
        {
            CustomerDataController mController = new CustomerDataController();
            DataTable dt = mController.SelectAllCustomer2(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, int.Parse(DrpTown.SelectedValue));
            this.DrpCustomer.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpCustomer, dt, 0, 3, false);
        }
    }
    
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }
    
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }


    protected void ShowReport(int type)
    {
        DocumentPrintController DPrint = new DocumentPrintController();
        RptCustomerController RptCustomerCtl = new RptCustomerController();
        DataSet ds = null;

        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));

        DataControl dc = new DataControl();
        ds = RptCustomerCtl.SelectPartystatment(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue,
            DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(ddlRegion.SelectedValue), int.Parse(ddlZone.SelectedValue), int.Parse(ddlTerritory.SelectedValue), int.Parse(DrpTown.SelectedValue), int.Parse(DrpCustomer.SelectedValue));

        SAMSBusinessLayer.Reports.CrpCustomerStatment CrpReport = new SAMSBusinessLayer.Reports.CrpCustomerStatment();
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();

        CrpReport.SetParameterValue("From_Date", DateTime.Parse(txtStartDate.Text));
        CrpReport.SetParameterValue("To_Date", DateTime.Parse(txtEndDate.Text));
        CrpReport.SetParameterValue("Principal", "All");
        CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

        CrpReport.SetParameterValue("Region", ddlRegion.SelectedItem.Text);
        CrpReport.SetParameterValue("Zone", ddlZone.SelectedItem.Text);
        CrpReport.SetParameterValue("Terr", ddlTerritory.SelectedItem.Text);
        CrpReport.SetParameterValue("Town", DrpTown.SelectedItem.Text);
        CrpReport.SetParameterValue("Customer", DrpCustomer.SelectedItem.Text);

        this.Session.Add("ReportType", type);
        this.Session.Add("CrpReport", CrpReport);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }


    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCustomer();
    }
}
