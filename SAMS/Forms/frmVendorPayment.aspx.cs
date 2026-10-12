using System;
using System.Data;

using SAMSBusinessLayer.Classes;
using System.Web.UI.WebControls;
using SAMSCommonLayer.Classes;

public partial class Forms_frmVendorPayment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.LoadDistributor();
            this.LoadPrincipal();
            this.LoadBank();
            this.LoadVendorPayment();
            txtChequeDate.Text = Convert.ToDateTime(Session["CurrentWorkDate"]).ToString("dd-MMM-yyyy");
        }
    }

    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadBank();
        this.LoadVendorPayment();
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        //DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        //clsWebFormUtil.FillDropDownList(this.ddlLocation, dt, 0, 2, true);


        DataTable dt3 = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        DataTable dt2 = dt3.Clone();
        DataRow[] dr = dt3.Select("SUBZONE_ID = 5");

        if (dr.Length > 0)
        {
            for (int k = 0; k < dr.Length; k++)
            {
                dt2.ImportRow(dr[k]);
            }
        }

        clsWebFormUtil.FillDropDownList(this.ddlLocation, dt2, 0, 2, true);
        this.Session.Add("DistributorId", int.Parse(ddlLocation.SelectedValue.ToString()));
    }

    protected void ddlPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadVendorPayment();
    }

    /// <summary>
    /// Loads Principals To Principal Combo
    /// </summary>
    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlPrincipal, m_dt, 0, 1, true);
    }

    protected void LoadBank()
    {
        ddlBank.Items.Clear();
        if (ddlPaymentMode.SelectedValue != "19"  )
        {
            SAMSCommonLayer.Classes.Configuration.GetAccountHead();
            AccountHeadController mAccountController = new AccountHeadController();
            DataTable dt = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, long.Parse(SAMSCommonLayer.Classes.Configuration.BankDefaultType));
            clsWebFormUtil.FillDropDownList(ddlBank, dt, 0, 4);
        }
        else
        {
            //if (ddlLocation.SelectedValue == "6")
            //{
            //    ddlBank.Items.Add(new ListItem("Cash In Hand Islamabad", "194"));
            //}
            //else if (ddlLocation.SelectedValue == "4")
            //{
            //    ddlBank.Items.Add(new ListItem("Cash In Hand Karachi", "191"));
            //}
            //else if (ddlLocation.SelectedValue == "2")
            //{
            //    ddlBank.Items.Add(new ListItem("Cash In Hand Lahore", "190"));
            //}
            //else if (ddlLocation.SelectedValue == "5")
            //{
            //    ddlBank.Items.Add(new ListItem("Cash In Hand Multan", "193"));
            //}
            //else if (ddlLocation.SelectedValue == "3")
            //{
            //    ddlBank.Items.Add(new ListItem("Cash In Hand Peshawar", "192"));
            //}
            //else
            {
                ddlBank.Items.Add(new ListItem("Cash In Hand", "631"));
            }
        }
    }

    protected void ddlPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPaymentMode.SelectedValue == "18")// check
        {
            txtCheque.Enabled = true;
            txtSlip.Enabled = true;
            ibnEndDate.Enabled = true;
            txtNarration.Enabled = true;
            ddlBank.Enabled = true;
        }
        else if (ddlPaymentMode.SelectedValue == "19")// cash
        {
            txtCheque.Enabled = false;
            txtSlip.Enabled = false;
            ibnEndDate.Enabled = false;
          //  txtNarration.Enabled = false;
          //  ddlBank.Enabled = false;
        }
        else
        {
            txtCheque.Enabled = true;
            txtSlip.Enabled = true;
            ibnEndDate.Enabled = true;
            txtNarration.Enabled = true;
            ddlBank.Enabled = true;
        }

        this.LoadBank();
        this.LoadVendorPayment();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        LedgerController LedgCtl = new LedgerController();
        long AccontHeadID = 324;
        decimal Amount = 0;
        decimal Discount = 0;
        decimal IncomeTax = 0;
                
        try
        {
          Amount =  Convert.ToDecimal(txtAmount.Text);
        }
        catch (Exception ex)
        {
           System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please enter amount.');", true);
           return;
        }

        if (Amount <= 0)
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please enter amount.');", true);
            return;
        }

        try
        {
            Discount = Convert.ToDecimal(txtDiscount.Text);
        }
        catch (Exception ex)
        {
            Discount = 0;
        }

        try
        {
            IncomeTax = Convert.ToDecimal(txtIncomeTax.Text);
        }
        catch (Exception ex)
        {
            IncomeTax = 0;
        }


        if (ddlPrincipal.SelectedValue == "1" || ddlPrincipal.SelectedValue == "4" || ddlPrincipal.SelectedValue == "5" || ddlPrincipal.SelectedValue == "40")
        {
            AccontHeadID = 424;
        }

        LedgCtl.InsertVendorLedger(AccontHeadID, Convert.ToInt32(ddlLocation.SelectedValue), Amount + Discount + IncomeTax, 0, Convert.ToDateTime(Session["CurrentWorkDate"]), txtNarration.Text, Convert.ToInt32(ddlPrincipal.SelectedValue), Constants.LongNullValue, Constants.Document_Purchase, Convert.ToInt32(Session["UserID"]), Convert.ToInt32(ddlPaymentMode.SelectedValue),Convert.ToDateTime(txtChequeDate.Text),txtCheque.Text,txtSlip.Text);
        LedgCtl.InsertVendorLedger(Convert.ToInt32(ddlBank.SelectedValue), Convert.ToInt32(ddlLocation.SelectedValue), 0, Amount, Convert.ToDateTime(Session["CurrentWorkDate"]), txtNarration.Text, Convert.ToInt32(ddlPrincipal.SelectedValue), Constants.LongNullValue, Constants.Document_Purchase, Convert.ToInt32(Session["UserID"]), Convert.ToInt32(ddlPaymentMode.SelectedValue), Convert.ToDateTime(txtChequeDate.Text), txtCheque.Text, txtSlip.Text);
        

        //if (Discount > 0)
        //{
        //    LedgCtl.InsertVendorLedger(476, Convert.ToInt32(ddlLocation.SelectedValue), 0, Discount, Convert.ToDateTime(Session["CurrentWorkDate"]), txtNarration.Text, Convert.ToInt32(ddlPrincipal.SelectedValue), Constants.LongNullValue, Constants.Document_Purchase, Convert.ToInt32(Session["UserID"]), Convert.ToInt32(ddlPaymentMode.SelectedValue), Convert.ToDateTime(txtChequeDate.Text), txtCheque.Text, txtSlip.Text);
        //}

        //if (IncomeTax > 0)
        //{
        //    LedgCtl.InsertVendorLedger(478, Convert.ToInt32(ddlLocation.SelectedValue), 0, IncomeTax, Convert.ToDateTime(Session["CurrentWorkDate"]), txtNarration.Text, Convert.ToInt32(ddlPrincipal.SelectedValue), Constants.LongNullValue, Constants.Document_Purchase, Convert.ToInt32(Session["UserID"]), Convert.ToInt32(ddlPaymentMode.SelectedValue), Convert.ToDateTime(txtChequeDate.Text), txtCheque.Text, txtSlip.Text);
        //}

        ClearForm();
        this.LoadVendorPayment();
    }

    private void ClearForm()
    {
        txtCheque.Text = "";
        txtSlip.Text = "";
        txtAmount.Text = "";
        txtDiscount.Text = "";
        txtIncomeTax.Text = "";
        txtNarration.Text = "";
    }

    /// <summary>
    /// Loads Opening Credits Detail To Grid
    /// </summary>
    private void LoadVendorPayment()
    {
        CustomerDataController cdc = new CustomerDataController();
        DataTable dt = cdc.GetVendorPayment(int.Parse(ddlLocation.SelectedValue.ToString()),Convert.ToInt32(ddlPrincipal.SelectedValue), DateTime.Parse(Session["CurrentWorkDate"].ToString()),Convert.ToInt32(ddlPaymentMode.SelectedValue));
        GrdOrder.DataSource = dt;
        GrdOrder.DataBind();
    }

    protected void GrdOrder_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        LedgerController LedgerCtl = new LedgerController();
        if (LedgerCtl.DeleteVendorPayment(Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlPrincipal.SelectedValue), Convert.ToDateTime(Session["CurrentWorkDate"]), Convert.ToInt32(ddlPaymentMode.SelectedValue), GrdOrder.Rows[e.RowIndex].Cells[0].Text.Replace("&nbsp;", ""), GrdOrder.Rows[e.RowIndex].Cells[1].Text.Replace("&nbsp;", ""), Convert.ToDateTime(GrdOrder.Rows[e.RowIndex].Cells[2].Text.Replace("&nbsp;", ""))))
        {
            this.ClearForm();
            this.LoadVendorPayment();
        }
        else
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some error occured. Opening Credit not deleted.');", true);
        }
    }
}