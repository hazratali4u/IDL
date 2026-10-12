using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommonLayer.Classes;

/// <summary>
/// Form To Add, Edit Customer Claim
/// </summary>
public partial class Forms_frmDemangedClaim : System.Web.UI.Page
{
    DataTable ClaimedSKU;
    private static int RowId;
    private static long URowId;

    /// <summary>
    /// Page_Load Function Populates All Combos And Grid On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadDistributor();
            this.LoadClaimType();
            this.LoadAccountHead();
            this.LoadSKUDetail();
            this.CreatTableValue();
            this.CreatTable();
            this.LoadRoute();
            this.LoadCustomer();
            this.LoadGrid();
            btnAddNew.Attributes.Add("onclick", "return ValidateValueForm();");
            btnSave.Attributes.Add("onclick", "return ValidateForm();");
            ScriptManager.GetCurrent(Page).SetFocus(drpDistributor);
            lblRowId.Text = "-1";
        }
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        var dController = new DistributorController();
        var dt = dController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2, true);
    }
    
    /// <summary>
    /// Loads Claim Types To RadioButtonList
    /// </summary>
    private void LoadClaimType()
    {
        RbdClaimType.Items.Add(new ListItem("Credit Claim", Constants.CreditClaim.ToString()));
        RbdClaimType.Items.Add(new ListItem("Debit Claim", Constants.DebitClaim.ToString()));        
        RbdClaimType.SelectedIndex = 0;         
    }
    
    /// <summary>
    /// Loads Account Heads To AccountHead Combo
    /// Modified by: Hazrat Ali
    /// 2012-Feb-16 10:40 AM
    /// Hard coded Account Heads as per Saddruddin Sb Instructions
    /// This is valid only for FDMPL and invalid for BDN and FDM
    /// Commited old code
    /// </summary>
    private void LoadAccountHead()
    {
        AccountHeadController mAccountController = new AccountHeadController();
        DataTable dt = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId,Constants.LongNullValue);
        clsWebFormUtil.FillDropDownList(DrpAccountHead, dt, 0, 4, true);
        #region Commited Code
        /*
        DrpAccountHead.Items.Clear();
        if (RbdClaimType.SelectedValue == Constants.DebitClaim.ToString())
        {
            DrpAccountHead.Items.Add(new ListItem("SECURITY DEPOSIT BOTTLE", "424"));
        }
        else if (RbdClaimType.SelectedValue == Constants.CreditClaim.ToString())
        {
            DrpAccountHead.Items.Add(new ListItem("Adv. against Empty Bottle", "511"));
            DrpAccountHead.Items.Add(new ListItem("Sales Return", "245"));
            DrpAccountHead.Items.Add(new ListItem("Sale Discount - Claimable", "493"));
        }
         */
       
        /*
            AccountHeadController mAccountController = new AccountHeadController();
            DataTable dt = mAccountController.SelectClaimHead(int.Parse(RbdClaimType.SelectedValue.ToString()));
            clsWebFormUtil.FillDropDownList(DrpAccountHead, dt, 0, 2, true);
         */
      
        #endregion
    }
        
    /// <summary>
    /// Creates Datatable For Customer Claim Data
    /// </summary>
    private void CreatTableValue()
    {
        DataTable dtVoucher = new DataTable();
        dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
        dtVoucher.Columns.Add("Account_Code", typeof(string));
        dtVoucher.Columns.Add("Account_Name", typeof(string));
        dtVoucher.Columns.Add("Debit", typeof(decimal));
        dtVoucher.Columns.Add("Credit", typeof(decimal));
        dtVoucher.Columns.Add("Remarks", typeof(string));
        this.Session.Add("dtVoucher", dtVoucher);
        GrdOrder.DataSource = dtVoucher;
        GrdOrder.DataBind();  

    }
    
    /// <summary>
    /// Loads Routes To Route Combo
    /// </summary>
    private void LoadRoute()
    {
        //DistributorAreaController mController = new DistributorAreaController();
        //DataTable dt = mController.SelectDist_Area(Constants.LongNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, null, null);
        //clsWebFormUtil.FillDropDownList(ddlRoute, dt, 0, 6, true);

        ddlRoute.Items.Clear();
        GeoHierarchyController gController = new GeoHierarchyController();
        DataTable dt = gController.SelectGeoHierarchy(int.Parse(drpDistributor.SelectedValue.ToString()));
       // ddlRoute.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(ddlRoute, dt, 0, 1);

        ddlRoute.Enabled = true;     
    }
    
    /// <summary>
    /// Loads Customers To Customer Combo
    /// </summary>
    private void LoadCustomer()
    {
        if (drpDistributor.Items.Count > 0 && ddlRoute.Items.Count > 0)
        {
            var mController = new CustomerDataController();
          

            var dt = mController.SelectAllCustomer(int.Parse(drpDistributor.SelectedValue), Constants.IntNullValue, Convert.ToInt32(ddlRoute.SelectedValue));
            clsWebFormUtil.FillDropDownList(this.DrpCustomer, dt, 0, 17, true);
            this.Session.Add("dt", dt);
        }
        else
        {
            DrpCustomer.Items.Clear();   
        }
    }
    
    /// <summary>
    /// Loads Customer Claims To Grid
    /// </summary>
    private void LoadGrid()
    {
        if (drpDistributor.Items.Count > 0)
        {
            var lController = new LedgerController();
            var dt = lController.SelectClaimDetail(int.Parse(drpDistributor.SelectedValue),int.Parse(RbdClaimType.SelectedValue),
                DateTime.Parse(this.Session["CurrentWorkDate"].ToString()), DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
            GrdOrder.DataSource = dt;
            GrdOrder.DataBind();
        }
    }


    private void ClearGrd_Order()
    {
        txtRemarks.Text = "";
        txtAmount.Text = "";
        btnAddNew.Text = "Save";
        
    }
    /// <summary>
    /// Deletes Customer Claim
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdOrder_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        LedgerController LController = new LedgerController();
        DataControl dc = new DataControl();

        LController.DeleteWareHouseLedger(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[4].Text)),
        int.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[8].Text)),Constants.LongNullValue  , decimal.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[9].Text)),2);

        ClearGrd_Order();
        this.LoadGrid();
        
    }
    private void EnableDisable(bool flag)
    {
        if (flag == true)
        {
            drpDistributor.Enabled = false;
            ddlRoute.Enabled = false;
            DrpCustomer.Enabled = false;
        }
        else
        {
            drpDistributor.Enabled = true;
            ddlRoute.Enabled = true;
            DrpCustomer.Enabled = true;
        }
    }
    protected void GrdOrder_RowEditing(object sender, GridViewEditEventArgs e)
    {
        URowId = Convert.ToInt64(GrdOrder.Rows[e.NewEditIndex].Cells[0].Text);
        ddlRoute.SelectedValue = Convert.ToString( GrdOrder.Rows[e.NewEditIndex].Cells[11].Text);
        this.LoadCustomer();
        DrpCustomer.SelectedValue = Convert.ToString(GrdOrder.Rows[e.NewEditIndex].Cells[1].Text);
        DrpAccountHead.SelectedValue = Convert.ToString(GrdOrder.Rows[e.NewEditIndex].Cells[2].Text);
        txtAmount.Text = Convert.ToString(GrdOrder.Rows[e.NewEditIndex].Cells[9].Text);
        txtRemarks.Text = Convert.ToString(GrdOrder.Rows[e.NewEditIndex].Cells[10].Text.Replace("&nbsp;",""));

        EnableDisable(true);

        btnAddNew.Text = "Update";
    }

    /// <summary>
    /// Saves Customer Claim
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        LedgerController LController = new LedgerController();

        if (btnAddNew.Text == "Save")
        {
            string MaxDocumentId = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, int.Parse(drpDistributor.SelectedValue.ToString()));
            if (RbdClaimType.SelectedValue == Constants.DebitClaim.ToString())
            {
                LController.PostingCash_Bank_Account(Constants.Journal_Voucher, long.Parse(MaxDocumentId), long.Parse(DrpAccountHead.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), 0, decimal.Parse(txtAmount.Text),
                   DateTime.Parse(this.Session["CurrentWorkDate"].ToString()), txtRemarks.Text, DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), 0,
                    null, int.Parse(this.Session["UserId"].ToString()), Constants.LongNullValue, null, Constants.IntNullValue, null, Constants.DateNullValue, Constants.DebitClaim, "");
            }
            else
            {
                LController.PostingCash_Bank_Account(Constants.Journal_Voucher, long.Parse(MaxDocumentId), long.Parse(DrpAccountHead.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), decimal.Parse(txtAmount.Text), 0,
                    DateTime.Parse(this.Session["CurrentWorkDate"].ToString()), txtRemarks.Text, DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), 0,
                     null, int.Parse(this.Session["UserId"].ToString()), Constants.LongNullValue, null, Constants.IntNullValue, null, Constants.DateNullValue, Constants.CreditClaim, "");
            }
        }
        else
        {
            DataControl dc = new DataControl();

            LController.UpdateLedgerClaim(URowId, long.Parse(DrpAccountHead.SelectedValue), decimal.Parse(dc.chkNull_0(txtAmount.Text)), Convert.ToString(txtRemarks.Text), int.Parse(RbdClaimType.SelectedValue));
           
            EnableDisable(false);
            
        }
       
       
        ClearGrd_Order();
       // this.LoadAccountHead();
        this.LoadGrid();
    }

    #region Index/Change

    /// <summary>
    /// Loads Account Heads And Claim Data
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void RbdClaimType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadAccountHead();
        this.LoadGrid();
        this.CreatTable();
        this.LoadPurchaseGrid();
        if (ddlClaimMode.SelectedIndex == 1)
        {
            this.LoadDocumentNo();
        }
    }

    protected void ddlClaimMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlClaimMode.SelectedIndex == 0)
        {
            lblDocumentNo.Visible = false;
            ddlDocumentNo.Visible = false;
            pnlProduct.Visible = false; 

            txtAmount.Visible = true;
            lblAmount.Visible = true;
            btnAddNew.Visible = true;
            pnlValue.Visible = true;
        }
        else
        {
            txtAmount.Visible = false;
            lblAmount.Visible = false;
            btnAddNew.Visible = false;
            pnlValue.Visible = false;

            pnlProduct.Visible = true;
            lblDocumentNo.Visible = true;
            ddlDocumentNo.Visible = true;
            this.LoadDocumentNo();
            this.CreatTable();
            this.LoadPurchaseGrid();
        }
    }

    protected void ddlDocumentNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (long.Parse(ddlDocumentNo.SelectedValue.ToString()) != Constants.LongNullValue)
        {
            DataTable dt = (DataTable)this.Session["dtDocumentNo"];
            DataRow[] foundRows = dt.Select("CLAIM_MASTER_ID  = '" + ddlDocumentNo.SelectedItem.Text + "'");
            if (foundRows.Length > 0)
            {
                DrpAccountHead.SelectedValue = foundRows[0]["ACCOUNT_HEAD_ID"].ToString();
                txtRemarks.Text = foundRows[0]["REMARKS"].ToString();
                txtTotalAmount.Text = foundRows[0]["AMOUNT"].ToString();
                this.LoadClaimDetail();
                this.ClearAll();
                ScriptManager.GetCurrent(Page).SetFocus(txtskuCode);                
            }
        }
        else
        {
            ClearAll();
            this.CreatTable();
            this.LoadPurchaseGrid();
        }
    }

    /// <summary>
    /// Loads Routes And Customers
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadRoute();
        this.LoadCustomer();
        if (ddlClaimMode.SelectedIndex == 1)
        {
            this.LoadDocumentNo();
        }
    }

    /// <summary>
    /// Loads Customers
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ddlRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadCustomer();

        if (ddlClaimMode.SelectedIndex == 1)
        {
            this.LoadDocumentNo();
        }
       
    }

    protected void DrpCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlClaimMode.SelectedIndex == 1)
        {
            this.LoadDocumentNo();
        }
       
    }
    #endregion

    #region Load

    /// <summary>
    /// Loads Document Nos
    /// </summary>
    private void LoadDocumentNo()
    {
        if (DrpCustomer.Items.Count > 0)
        {
            LedgerController LedgrCtl = new LedgerController();
            this.ddlDocumentNo.Items.Clear();
            DataTable dtDocumentNo = LedgrCtl.GetCustomerClaimMaster(Constants.IntNullValue, Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToInt32(DrpCustomer.SelectedValue), Convert.ToDateTime(this.Session["CurrentWorkDate"]), RbdClaimType.SelectedIndex);
            ddlDocumentNo.Items.Add(new clsListItems("New", Constants.LongNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(this.ddlDocumentNo, dtDocumentNo, "LEDGER_ID", "CLAIM_MASTER_ID");
            this.Session.Add("dtDocumentNo", dtDocumentNo);
        }
    }

    private void LoadClaimDetail()
    {
        LedgerController LegrCtl = new LedgerController();
        ClaimedSKU = LegrCtl.GetCustomerClaimDetail(Convert.ToInt32(ddlDocumentNo.SelectedItem.Text));
        this.Session.Add("ClaimedSKU", ClaimedSKU);
        this.LoadPurchaseGrid();
    }

    /// <summary>
    /// Loads SKU Data To SKU ListBox
    /// </summary>
    private void LoadSKUDetail()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable Dtsku_Price = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue), int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 1, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        clsWebFormUtil.FillListBox(this.lstCode, Dtsku_Price, 10, 10, true);
        this.Session.Add("Dtsku_Price", Dtsku_Price);
    }
    #endregion
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataControl dc = new DataControl();
        DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];
        ClaimedSKU = (DataTable)this.Session["ClaimedSKU"];
        DataRow[] foundRows = Dtsku_Price.Select("SKU_CODE  = '" + txtskuCode.Text + "'");
        decimal mTradePrice = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString()));
                

        if (btnSave.Text == "Add Sku")
        {
            if (CheckDublicateSKU())
            {
                DataRow dr = ClaimedSKU.NewRow();
                dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                dr["SKU_Code"] = foundRows[0]["SKU_CODE"];
                dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                dr["QUANTITY_UNIT"] = int.Parse(dc.chkNull_0(txtQuantity.Text));                
                dr["UNIT_PRICE"] = mTradePrice.ToString();

                if (foundRows[0]["GST_ON"].ToString().Trim() == "T")
                {
                    dr["AMOUNT"] = mTradePrice * decimal.Parse(dr["QUANTITY_UNIT"].ToString());
                    dr["BATCH_NO"] = "T";

                }
                else if (foundRows[0]["GST_ON"].ToString().Trim() == "R")
                {
                    dr["AMOUNT"] = mTradePrice * decimal.Parse(dr["QUANTITY_UNIT"].ToString());
                    dr["BATCH_NO"] = "R";

                }
                else
                {
                    dr["AMOUNT"] = mTradePrice * decimal.Parse(dr["QUANTITY_UNIT"].ToString());
                    dr["BATCH_NO"] = "E";
                }
                                
                ClaimedSKU.Rows.Add(dr);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('SKU Already Exists')", true);
            }
        }
        else
        {
            DataRow dr = ClaimedSKU.Rows[RowId];
            dr["SKU_ID"] = foundRows[0]["SKU_ID"];
            dr["SKU_Code"] = foundRows[0]["SKU_CODE"];
            dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
            dr["BATCH_NO"] = txtBatchNo.Text;
            dr["QUANTITY_UNIT"] = int.Parse(dc.chkNull_0(txtQuantity.Text));            
            dr["UNIT_PRICE"] = mTradePrice.ToString();

            if (foundRows[0]["GST_ON"].ToString().Trim() == "T")
            {
                dr["AMOUNT"] = mTradePrice * decimal.Parse(dr["QUANTITY_UNIT"].ToString());
                dr["BATCH_NO"] = "T";

            }
            else if (foundRows[0]["GST_ON"].ToString().Trim() == "R")
            {
                dr["AMOUNT"] = mTradePrice * decimal.Parse(dr["QUANTITY_UNIT"].ToString());
                dr["BATCH_NO"] = "R";

            }
            else
            {
                dr["AMOUNT"] = mTradePrice * decimal.Parse(dr["QUANTITY_UNIT"].ToString());
                dr["BATCH_NO"] = "E";
            }
                        
        }

        decimal TotalProductValue = 0;
        
        foreach (DataRow dr in ClaimedSKU.Rows)
        {
            TotalProductValue += decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString()));           

        }

        txtTotalAmount.Text = Convert.ToString(Math.Round(TotalProductValue, 2));


        this.Session.Add("ClaimedSKU", ClaimedSKU);        
        this.LoadPurchaseGrid();
        this.ClearAll();
        ScriptManager.GetCurrent(Page).SetFocus(txtskuCode);            
    }

    /// <summary>
    /// Checks SKU in Order Grid
    /// </summary>
    /// <returns>True On Success And False On Failure</returns>
    private bool CheckDublicateSKU()
    {
        DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];
        ClaimedSKU = (DataTable)this.Session["ClaimedSKU"];
        DataRow[] foundRows = ClaimedSKU.Select("SKU_CODE  = '" + txtskuCode.Text + "'");
        if (foundRows.Length == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Loads Order To Order Grid
    /// </summary>
    private void LoadPurchaseGrid()
    {
        ClaimedSKU = (DataTable)this.Session["ClaimedSKU"];
        GrdPurchase.DataSource = ClaimedSKU;
        GrdPurchase.DataBind();
    }

    /// <summary>
    /// Clears Some Of Controls
    /// </summary>
    private void ClearAll()
    {
        txtskuCode.Text = "";
        txtskuName.Text = "";
        txtUnitRate.Text = "";
        txtQuantity.Text = "";
        txtProductAmount.Text = "";
        txtBatchNo.Text = "";
        txtRemarks.Text = "";
        btnSave.Text = "Add Sku";
        txtskuCode.Enabled = true;
    }

    /// <summary>
    /// Creates Datatable For Order, Invoice And Sale Return
    /// </summary>
    private void CreatTable()
    {
        ClaimedSKU = new DataTable();
        ClaimedSKU.Columns.Add("SALE_ORDER_DETAIL_ID", typeof(long));
        ClaimedSKU.Columns.Add("CLAIM_MASTER_ID", typeof(long));
        ClaimedSKU.Columns.Add("SKU_ID", typeof(int));
        ClaimedSKU.Columns.Add("SKU_Code", typeof(string));
        ClaimedSKU.Columns.Add("SKU_Name", typeof(string));
        ClaimedSKU.Columns.Add("BATCH_NO", typeof(string));
        ClaimedSKU.Columns.Add("UNIT_PRICE", typeof(decimal));
        ClaimedSKU.Columns.Add("QUANTITY_UNIT", typeof(int));
        ClaimedSKU.Columns.Add("AMOUNT", typeof(decimal));
        this.Session.Add("ClaimedSKU", ClaimedSKU);
    }

    /// <summary>
    /// Sets Order SKU For Edit. This Function Runs When An Existing Order SKU Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPurchase_RowEditing(object sender, GridViewEditEventArgs e)
    {
        RowId = e.NewEditIndex;
        txtskuCode.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[1].Text;
        txtskuName.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[2].Text;
        txtQuantity.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[3].Text;
        txtUnitRate.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[4].Text;
        txtBatchNo.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[5].Text;
        txtProductAmount.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[6].Text;
        txtQuantity.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[3].Text;        
        txtskuCode.Enabled = false;
        btnSave.Text = "Update SKU";

    }

    /// <summary>
    /// Deletes An SKU From Order Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPurchase_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ClaimedSKU = (DataTable)this.Session["ClaimedSKU"];
        ClaimedSKU.Rows.RemoveAt(e.RowIndex);
        this.Session.Add("ClaimedSKU", ClaimedSKU);
        this.LoadPurchaseGrid();
    }

    protected void btnSaveProductClaim_Click(object sender, EventArgs e)
    {
        LedgerController LController = new LedgerController();
        ClaimedSKU = (DataTable)Session["ClaimedSKU"];
        string MaxDocumentId = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, int.Parse(drpDistributor.SelectedValue.ToString()));
        if (RbdClaimType.SelectedValue == Constants.DebitClaim.ToString())
        {
            if (ddlDocumentNo.SelectedValue == Constants.LongNullValue.ToString())
            {
                if (LController.InsertCustomerClaim(Constants.Journal_Voucher, long.Parse(MaxDocumentId), long.Parse(DrpAccountHead.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), 0, decimal.Parse(txtTotalAmount.Text),
                   DateTime.Parse(this.Session["CurrentWorkDate"].ToString()), txtRemarks.Text, DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), 0,
                    null, int.Parse(this.Session["UserId"].ToString()), Constants.LongNullValue, null, Constants.IntNullValue, null, Constants.DateNullValue, Constants.DebitClaim, "", ClaimedSKU))
                {
                    Session.Remove("ClaimedSKU");
                    txtTotalAmount.Text = "";
                    LoadPurchaseGrid();
                    LoadDocumentNo();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Customer Claim saved successfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some error occured.');", true);
                }
            }
            else
            {
                if (LController.UpdateCustomerClaim(Convert.ToInt64(ddlDocumentNo.SelectedValue),Convert.ToInt64(ddlDocumentNo.SelectedItem.Text),0,Convert.ToDecimal(txtTotalAmount.Text),Convert.ToInt32(DrpAccountHead.SelectedValue),txtRemarks.Text,Convert.ToInt32(Session["UserID"]),ClaimedSKU))
                {
                    Session.Remove("ClaimedSKU");
                    txtTotalAmount.Text = "";
                    LoadPurchaseGrid();
                    LoadDocumentNo();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Customer Claim updated successfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some error occured.');", true);
                }
            }
        }
        else
        {
            if (ddlDocumentNo.SelectedValue == Constants.LongNullValue.ToString())
            {
                if (LController.InsertCustomerClaim(Constants.Journal_Voucher, long.Parse(MaxDocumentId), long.Parse(DrpAccountHead.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), decimal.Parse(txtTotalAmount.Text), 0,
                    DateTime.Parse(this.Session["CurrentWorkDate"].ToString()), txtRemarks.Text, DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), 0,
                     null, int.Parse(this.Session["UserId"].ToString()), Constants.LongNullValue, null, Constants.IntNullValue, null, Constants.DateNullValue, Constants.CreditClaim, "", ClaimedSKU))
                {
                    Session.Remove("ClaimedSKU");
                    txtTotalAmount.Text = "";
                    LoadPurchaseGrid();
                    LoadDocumentNo();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Customer Claim Saved Successfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some error occured.');", true);
                }
            }
            else
            {
                if (LController.UpdateCustomerClaim(Convert.ToInt64(ddlDocumentNo.SelectedValue), Convert.ToInt64(ddlDocumentNo.SelectedItem.Text), Convert.ToDecimal(txtTotalAmount.Text), 0, Convert.ToInt32(DrpAccountHead.SelectedValue), txtRemarks.Text, Convert.ToInt32(Session["UserID"]), ClaimedSKU))
                {
                    Session.Remove("ClaimedSKU");
                    txtTotalAmount.Text = "";
                    LoadPurchaseGrid();
                    LoadDocumentNo();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Customer Claim updated successfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some error occured.');", true);
                }
            }
        }
    }

    
}
