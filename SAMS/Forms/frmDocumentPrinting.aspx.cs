using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommonLayer.Classes;
using SAMSBusinessLayer.Reports;  
using CrystalDecisions.CrystalReports.Engine;

/// <summary>
/// Form For Print Sale Document Report
/// </summary>
public partial class Forms_frmDocumentPrinting : System.Web.UI.Page
{
    /// <summary>
    /// Page_Load Function
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadDistributor();
            this.LoadRoute();
            this.LoadCustomer();
            SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");

        }
    }
        
    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo2(Constants.IntNullValue);
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, "DISTRIBUTOR_ID", "DISTRIBUTOR_NAME");
    }
        
    /// <summary>
    /// Loads Routes To Route Combo
    /// </summary>
    private void LoadRoute()
    {
        if (drpDistributor.Items.Count > 0)
        {
            //ddlTown.Items.Clear();
            //DistributorAreaController mController = new DistributorAreaController();
            //DataTable dt = mController.SelectDist_Area(Constants.LongNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, null, null);
            //ddlTown.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            //clsWebFormUtil.FillDropDownList(ddlTown, dt, 0, 6);

            ddlTown.Items.Clear();
            GeoHierarchyController gController = new GeoHierarchyController();
            DataTable dt = gController.SelectGeoHierarchy(int.Parse(drpDistributor.SelectedValue.ToString()));
            ddlTown.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(ddlTown, dt, 0, 1);
        }
        else
        {
            ddlTown.Items.Clear();
        }
    }
    
    /// <summary>
    /// Loads Customers To Customer Combo
    /// </summary>
    private void LoadCustomer()
    {
        DrpCustomer.Items.Clear();
        DrpCustomer.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        if (drpDistributor.Items.Count > 0 && ddlTown.Items.Count > 0 && ddlTown.SelectedValue != Constants.IntNullValue.ToString())
        {
            CustomerDataController mController = new CustomerDataController();
            DataTable dt = mController.SelectAllCustomer(int.Parse(drpDistributor.SelectedValue.ToString()),  Constants.IntNullValue,int.Parse(ddlTown.SelectedValue.ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpCustomer, dt, 0, 3, false);

        }
    }

    /// <summary>
    /// Loads Sale Forces And Routes
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadRoute();
    }
    
    /// <summary>
    /// Loads Customers
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ddlTown_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCustomer();
    }

    /// <summary>
    /// Shows Print Sale Document in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }
    
    /// <summary>
    /// Gets Print Sale Document And Shows Either in Excel or PDF
    /// </summary>
    /// <param name="p_ReportType">Type</param>
    private void ShowReport(int p_ReportType)
    {
        int p_CustomerType = Constants.IntNullValue;
        if (rblCustomerType.SelectedValue != "-1")
        {
            p_CustomerType = Convert.ToInt32(rblCustomerType.SelectedValue);
        }

        RptSaleController RptSaleCtl = new RptSaleController();
        DocumentPrintController DPrint = new DocumentPrintController();
        DataSet ds = null;

        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));

        if (dt.Rows.Count > 0)
        {
            if (DrpLedgerType.SelectedIndex == 3)
            {
                DataControl dc = new DataControl();
                ds = RptSaleCtl.SelectDocumentforPrint(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue,
                    DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), 0, Constants.LongNullValue, p_CustomerType, Convert.ToInt32(DrpCustomer.SelectedValue),Convert.ToInt32(ddlTown.SelectedValue),0);

                ReportDocument SubReport = new ReportDocument();
                ReportDocument CrpReport = new ReportDocument();
                if (DrpSort.SelectedValue == "0")
                {
                    CrpReport = new SAMSBusinessLayer.Reports.CrpDeliveryChallan();
                }
                else
                {
                    CrpReport = new SAMSBusinessLayer.Reports.CrpDeliveryChallanCustomer();
                }

                if (rbtSortOrder.SelectedIndex == 0)
                {
                    CrpReport.DataDefinition.SortFields[0].SortDirection = CrystalDecisions.Shared.SortDirection.AscendingOrder;
               
                }
                else
                {
                    CrpReport.DataDefinition.SortFields[0].SortDirection = CrystalDecisions.Shared.SortDirection.DescendingOrder;
                }

                SubReport = CrpReport.OpenSubreport("Subreport1");
                CrpReport.SetDataSource(ds);
                SubReport.SetDataSource(ds);
                CrpReport.Refresh();
                CrpReport.SetParameterValue("COMPANY_NAME", dt.Rows[0]["COMPANY_NAME"].ToString());
                CrpReport.SetParameterValue("CompanyAddress", "Branch Address: " + dt.Rows[0]["ADDRESS1"].ToString());

                this.Session.Add("CrpReport", CrpReport);
                this.Session.Add("ReportType", 0);
                string url = "'Default.aspx'";
                string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);

            }
            else if (DrpLedgerType.SelectedIndex == 4)
            {

                DataControl dc = new DataControl();
                ds = RptSaleCtl.SelectCSDUSCDocumentforPrint(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue,
                    DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), 1, Constants.LongNullValue,p_CustomerType,Convert.ToInt32(DrpCustomer.SelectedValue),Convert.ToInt32(ddlTown.SelectedValue));


                ReportDocument CrpReport = new ReportDocument();
                if (DrpSort.SelectedValue == "0")
                {
                    CrpReport = new SAMSBusinessLayer.Reports.CrpPrintUSDInvoice();
                }
                else
                {
                    CrpReport = new SAMSBusinessLayer.Reports.CrpPrintUSDInvoiceCustomer();
                }

                CrpReport.SetDataSource(ds);
                CrpReport.Refresh();

                if (rbtSortOrder.SelectedIndex == 0)
                {
                    CrpReport.DataDefinition.SortFields[0].SortDirection = CrystalDecisions.Shared.SortDirection.AscendingOrder;
                }
                else
                {
                    CrpReport.DataDefinition.SortFields[0].SortDirection = CrystalDecisions.Shared.SortDirection.DescendingOrder;
                }

                CrpReport.SetParameterValue("COMPANY_NAME", dt.Rows[0]["COMPANY_NAME"].ToString());
                CrpReport.SetParameterValue("CompanyAddress", "" + dt.Rows[0]["ADDRESS1"].ToString());
                CrpReport.SetParameterValue("TAXREGISTERATION_NO", dt.Rows[0]["GST_NUMBER"].ToString());
                CrpReport.SetParameterValue("DISTRIBUTOR_NAME", dt.Rows[0]["DISTRIBUTOR_NAME"].ToString());
                this.Session.Add("CrpReport", CrpReport);
                this.Session.Add("ReportType", 0);
                string url = "'Default.aspx'";
                string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }
            else
            { // mean sale order
                if (DrpLedgerType.SelectedIndex == 0)
                {
                    DataControl dc = new DataControl();
                    ds = RptSaleCtl.SelectDocumentforPrint3(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue,
                        DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(DrpLedgerType.SelectedValue.ToString()), Constants.LongNullValue, p_CustomerType, Convert.ToInt32(DrpCustomer.SelectedValue), Convert.ToInt32(ddlTown.SelectedValue), 0);

                    
                    ReportDocument CrpReport = new ReportDocument();

                    if (DrpSort.SelectedValue == "0")
                    {
                        CrpReport = new SAMSBusinessLayer.Reports.CrpPrintDocumentOrder();
                        CrpReport.SetDataSource(ds);
                        CrpReport.Refresh();
                    }

                    else
                    {
                       CrpReport = new SAMSBusinessLayer.Reports.CrpPrintDocumentOrderCustomerWise();
                      

                        CrpReport.SetDataSource(ds);
                        CrpReport.Refresh();
                    }



                    if (rbtSortOrder.SelectedIndex == 0)
                    {
                        CrpReport.DataDefinition.SortFields[0].SortDirection = CrystalDecisions.Shared.SortDirection.AscendingOrder;
                    }
                    else
                    {
                        CrpReport.DataDefinition.SortFields[0].SortDirection = CrystalDecisions.Shared.SortDirection.DescendingOrder;
                    }

                    CrpReport.SetParameterValue("COMPANY_NAME", dt.Rows[0]["COMPANY_NAME"].ToString());
                    CrpReport.SetParameterValue("NtnNumber", "NTN :" + dt.Rows[0]["NTN_NO"].ToString());
                    CrpReport.SetParameterValue("CompanyAddress",dt.Rows[0]["ADDRESS1"].ToString() );
                    CrpReport.SetParameterValue("BranchAddress", "Email: " + dt.Rows[0]["ADDRESS2"].ToString());
                    CrpReport.SetParameterValue("TAXREGISTERATION_NO", dt.Rows[0]["GST_NUMBER"].ToString());
                    CrpReport.SetParameterValue("DISTRIBUTOR_NAME", dt.Rows[0]["DISTRIBUTOR_NAME"].ToString());
                    CrpReport.SetParameterValue("CONTACT_NUMBER","Ph: " + dt.Rows[0]["CONTACT_NUMBER"].ToString());
                    CrpReport.SetParameterValue("PrintType", "0");
                    this.Session.Add("CrpReport", CrpReport);
                    this.Session.Add("ReportType", p_ReportType);
                    string url = "'Default.aspx'";
                    string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                    Type cstype = this.GetType();
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(cstype, "OpenWindow", script);
                }
                else if (DrpLedgerType.SelectedIndex == 1)
                {  // mean sale invoice

                    DataControl dc = new DataControl();
                    ds = RptSaleCtl.SelectDocumentforPrint4(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue,
                        DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(DrpLedgerType.SelectedValue.ToString()), Constants.LongNullValue, p_CustomerType, Convert.ToInt32(DrpCustomer.SelectedValue), Convert.ToInt32(ddlTown.SelectedValue), 0);


                    ReportDocument SubReport = new ReportDocument();
                    ReportDocument SubReport2 = new ReportDocument();
                     ReportDocument SubReport3 = new ReportDocument();
                     ReportDocument SubReport4 = new ReportDocument();
                    ReportDocument CrpReport = new ReportDocument();

                    if (DrpSort.SelectedValue == "0")
                    {
                        if (dt.Rows[0]["COMPANY_NAME"].ToString() == "International Distribution Ltd.")
                        {
                            CrpReport = new SAMSBusinessLayer.Reports.CrpPrintDocumentInvoice();
                        }
                        else
                        {
                            CrpReport = new SAMSBusinessLayer.Reports.CrpPrintDocumentInvoiceGeneral();
                        }
                          
                    }
                    else
                    {
                        if (dt.Rows[0]["COMPANY_NAME"].ToString() == "International Distribution Ltd.")
                        {
                            CrpReport = new SAMSBusinessLayer.Reports.CrpPrintDocumentInvoiceCustomerWise();
                        }
                        else
                        {
                            CrpReport = new SAMSBusinessLayer.Reports.CrpPrintDocumentInvoiceCustomerWiseGeneral();
                        }
                    }

                    SubReport = CrpReport.OpenSubreport("SubReportReplacement");
                    SubReport2 = CrpReport.OpenSubreport("SubReportDiscount");
                    SubReport3 = CrpReport.OpenSubreport("SubReportSalesTax");
                    SubReport4 = CrpReport.OpenSubreport("SubReportUnloading");
                    CrpReport.SetDataSource(ds);
                    SubReport.SetDataSource(ds);
                    SubReport2.SetDataSource(ds);
                    SubReport3.SetDataSource(ds);
                    SubReport4.SetDataSource(ds);
                    CrpReport.Refresh();

                    if (rbtSortOrder.SelectedIndex == 0)
                    {
                        CrpReport.DataDefinition.SortFields[0].SortDirection = CrystalDecisions.Shared.SortDirection.AscendingOrder;
                    }
                    else
                    {
                        CrpReport.DataDefinition.SortFields[0].SortDirection = CrystalDecisions.Shared.SortDirection.DescendingOrder;
                    }

                    CrpReport.SetParameterValue("COMPANY_NAME", dt.Rows[0]["COMPANY_NAME"].ToString());
                    CrpReport.SetParameterValue("NtnNumber", "NTN :" + dt.Rows[0]["NTN_NO"].ToString());
                    CrpReport.SetParameterValue("CompanyAddress", dt.Rows[0]["ADDRESS1"].ToString());
                    CrpReport.SetParameterValue("BranchAddress", "Email: " + dt.Rows[0]["ADDRESS2"].ToString());
                    CrpReport.SetParameterValue("TAXREGISTERATION_NO", dt.Rows[0]["GST_NUMBER"].ToString());
                    CrpReport.SetParameterValue("DISTRIBUTOR_NAME", dt.Rows[0]["DISTRIBUTOR_NAME"].ToString());
                    CrpReport.SetParameterValue("CONTACT_NUMBER","Ph: "+ dt.Rows[0]["CONTACT_NUMBER"].ToString());
                    CrpReport.SetParameterValue("PrintType", "0");
                    this.Session.Add("CrpReport", CrpReport);
                    this.Session.Add("ReportType", p_ReportType);
                    string url = "'Default.aspx'";
                    string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                    Type cstype = this.GetType();
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(cstype, "OpenWindow", script);
                }
                else
                {
                }
            }
        }
    }

    /// <summary>
    /// Shows Print Sale Document in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }
}
