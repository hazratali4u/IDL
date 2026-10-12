using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommonLayer.Classes;

/// <summary>
/// From For Purchase, TranferOut, Purchase Return, TranferIn And Damage
/// </summary>
public partial class Forms_frmPurchaseOrderEntry : System.Web.UI.Page
{
    SKUPriceDetailController PController = new SKUPriceDetailController();
    DataControl dc = new DataControl();
    private static int RowNo;
    DataTable PurchaseSKU;

    /// <summary>
    /// Page_Load Function Populates All Combos On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadPrincipal();
            this.LoadDistributor();
            this.LoadSKUDetail();
            this.CreatTable();
            this.GetDocumentNo();
            btnSave.Attributes.Add("onclick", "return ValidateForm();");
            SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
        }
    }

    private void CreatTable()
    {
        PurchaseSKU = new DataTable();
        PurchaseSKU.Columns.Add("PURCHASE_ORDER_DETAIL_ID", typeof(long));
        PurchaseSKU.Columns.Add("SKU_ID", typeof(int));
        PurchaseSKU.Columns.Add("SKU_Code", typeof(string));
        PurchaseSKU.Columns.Add("SKU_Name", typeof(string));
        PurchaseSKU.Columns.Add("Quantity", typeof(int));
        PurchaseSKU.Columns.Add("QUANTITY_CTN", typeof(int));
        PurchaseSKU.Columns.Add("PRICE", typeof(decimal));
        PurchaseSKU.Columns.Add("AMOUNT", typeof(decimal));
        this.Session.Add("PurchaseSKU", PurchaseSKU);

    }

    #region Check_Duplication
    
    //Stop replication for ref no
    private bool IsBillBookNoExist()
    {
        bool flag = false;
        if ((long.Parse(drpDocumentNo.SelectedValue) == Constants.LongNullValue || this.Session["hfBillBookNo"].ToString() != txtRefNo.Text) && txtRefNo.Text.Trim().Length > 0)
        {
            OrderEntryController OEC = new OrderEntryController();
            DataTable dtBillBookNo = OEC.SelectBillBookNo(int.Parse(drpDistributor.SelectedValue), txtRefNo.Text, 0);
            if (dtBillBookNo.Rows.Count > 0)
            {
                flag = true;
            }
        }
        return flag;
    }

    private bool CheckDublicateSKU()
    {
        DataControl dc = new DataControl();
        DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        DataRow[] foundRows = PurchaseSKU.Select("SKU_ID  = '" + ddlSKuCde.SelectedValue + "'");
        if (foundRows.Length == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion

    #region Load
    private void GetDocumentNo()
    {
        drpDocumentNo.Items.Clear();
        PurchaseController mPurchase = new PurchaseController();
        DataTable dt = mPurchase.SelectPurchaseOrderDocumentNo2(Constants.LongNullValue, int.Parse(drpDistributor.SelectedValue), Constants.IntNullValue, Convert.ToDateTime(Session["CURRENTWORKDATE"]));
        drpDocumentNo.Items.Add(new clsListItems("New", Constants.LongNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDocumentNo, dt, 0, 0);
    }

    /// <summary>
    /// Loads Principals To Principal Comb
    /// </summary>
    private void LoadPrincipal()
    {
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpPrincipal, m_dt, 0, 1, true);
    }
    /// <summary>
    /// Loads Locations To Location From Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2, true);
    }
    /// <summary>
    /// Loads Document Detail Data To Document Detail Grid
    /// </summary>
    private void LoadDocumentDetail()
    {
        PurchaseController mPurchase = new PurchaseController();
        DataTable dt = mPurchase.SelectPurchaseOrderDocumentNo(Convert.ToInt64(drpDocumentNo.SelectedValue), Convert.ToInt32(drpDistributor.SelectedValue), Constants.IntNullValue, Convert.ToDateTime(Session["CurrentWorkDate"]));
        if (dt.Rows.Count > 0)
        {
            txtRefNo.Text = dt.Rows[0]["REF_NO"].ToString();
            ddlCurrency.SelectedValue = dt.Rows[0]["CURRENCY_ID"].ToString();
            txtExchangeRate.Text = dt.Rows[0]["EXCHANGE_RATE"].ToString();
            drpPrincipal.SelectedValue = dt.Rows[0]["SOLD_FROM"].ToString();
            RbUnitType.SelectedValue = dt.Rows[0]["QTY_TYPE"].ToString();

            PurchaseSKU = mPurchase.SelectPurchaseOrderDetail(long.Parse(dt.Rows[0][0].ToString()), Convert.ToInt32(drpDistributor.SelectedValue));
            this.Session.Add("PurchaseSKU", PurchaseSKU);
            LoadGird();
        }
    }
    /// <summary>
    /// Loads Document Detail Data To Document Detail Grid
    /// </summary>
    private void LoadGird()
    {
        int TotalValue = 0;
        decimal TotalAmountFc = 0;
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        GrdPurchase.DataSource = PurchaseSKU;
        GrdPurchase.DataBind();
        foreach (DataRow dr in PurchaseSKU.Rows)
        {
            TotalValue += int.Parse(dr["Quantity"].ToString());
            TotalAmountFc += Convert.ToDecimal(dr["AMOUNT"]);

        }

        txtTotalQuantity.Text = TotalValue.ToString();
        txtAmountFC.Text = Math.Round(TotalAmountFc, 3).ToString();
        txtAmountRs.Text = Math.Round((TotalAmountFc * Convert.ToDecimal(dc.chkNull_0(txtExchangeRate.Text))), 3).ToString();
    }
    private void LoadSKUDetail()
    {
        SkuController SKUCtl = new SkuController();
        if (drpPrincipal.Items.Count > 0)
        {
            DataTable Dtsku_Price = SKUCtl.SelectSkuData(Constants.IntNullValue, Constants.IntNullValue, Convert.ToInt32(drpPrincipal.SelectedValue));
            clsWebFormUtil.FillDropDownList(ddlSKuCde, Dtsku_Price, 0, 1, true);
            this.Session.Add("Dtsku_Price", Dtsku_Price);
        }
    }

    #endregion

    #region Grid Operations

    /// <summary>
    /// Sets Dcoment Detail Data For Edit. This Function Runs When An Existing Document Detail Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPurchase_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];

        RowNo = e.NewEditIndex;
        ddlSKuCde.SelectedValue = GrdPurchase.Rows[e.NewEditIndex].Cells[0].Text;

        if (RbUnitType.SelectedValue == "0")
        {
            txtQuantity.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[3].Text;
            txtRate.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[4].Text;
            txtAmount.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[5].Text;
        }
        else
        {
            DataRow[] foundRows = Dtsku_Price.Select("SKU_ID  = '" + GrdPurchase.Rows[e.NewEditIndex].Cells[0].Text + "'");

            int mPackSize = int.Parse(dc.chkNull_0(foundRows[0]["UNITS_IN_CASE"].ToString()));
            int qty = Convert.ToInt32(GrdPurchase.Rows[e.NewEditIndex].Cells[3].Text);
            txtQuantity.Text = Convert.ToString(qty / mPackSize);
            qty=Convert.ToInt32(txtQuantity.Text); 

            txtAmount.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[5].Text;
            decimal amount= Convert.ToDecimal(GrdPurchase.Rows[e.NewEditIndex].Cells[5].Text);
            txtRate.Text =Convert.ToString(amount / qty);
            txtAmount.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[5].Text;
        }



        txtQuantity.Focus();
        btnSave.Text = "Update Sku";

    }

    /// <summary>
    /// Deletes A Document Detail
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPurchase_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        if (PurchaseSKU.Rows.Count > 0)
        {
            PurchaseSKU.Rows.RemoveAt(e.RowIndex);
            this.Session.Add("PurchaseSKU", PurchaseSKU);
            this.LoadGird();
        }
    }

    #endregion

    #region Click OPerations

   //Grid Operation
    protected void btnSave_Click(object sender, EventArgs e)
    {

        DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        DataRow[] foundRows = Dtsku_Price.Select("SKU_ID  = '" + ddlSKuCde.SelectedValue + "'");

        int mPackSize = int.Parse(dc.chkNull_0(foundRows[0]["UNITS_IN_CASE"].ToString()));

        if (foundRows.Length > 0)
        {
            if (btnSave.Text == "Add Sku")
            {
                if (CheckDublicateSKU())
                {
                    DataRow dr = PurchaseSKU.NewRow();
                    dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                    dr["SKU_Code"] = foundRows[0]["SKU_CODE"];
                    dr["SKU_Name"] = foundRows[0]["SKU_NAME"];



                    if (RbUnitType.SelectedValue == "0")
                    {
                        dr["PRICE"] = decimal.Parse(dc.chkNull_0(txtRate.Text));

                        dr["Quantity"] = int.Parse(dc.chkNull_0(txtQuantity.Text));
                        dr["QUANTITY_CTN"] = 0;

                        dr["AMOUNT"] = decimal.Parse(txtRate.Text) * Convert.ToInt32(dr["Quantity"]);
                    }
                    else
                    {
                        dr["Quantity"] = int.Parse(dc.chkNull_0(txtQuantity.Text)) * mPackSize;

                        dr["QUANTITY_CTN"] = int.Parse(dc.chkNull_0(txtQuantity.Text));

                        decimal rate = Convert.ToDecimal(txtRate.Text);

                        decimal qty = Convert.ToDecimal(dr["Quantity"]);

                        dr["PRICE"] = Convert.ToDecimal((txtAmount.Text)) / (qty);

                        dr["AMOUNT"] = Convert.ToDecimal(txtAmount.Text.ToString());
                    }


                    PurchaseSKU.Rows.Add(dr);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('  " + ddlSKuCde.SelectedItem.Text + " Already Exists ');", true);
                    return;
                }
            }
            else if (btnSave.Text == "Update Sku")
            {
                DataRow dr = PurchaseSKU.Rows[RowNo];
                dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                dr["SKU_Code"] = foundRows[0]["SKU_CODE"];
                dr["SKU_Name"] = foundRows[0]["SKU_NAME"];

                if (RbUnitType.SelectedValue == "0")
                {
                    dr["PRICE"] = decimal.Parse(dc.chkNull_0(txtRate.Text));

                    dr["Quantity"] = int.Parse(dc.chkNull_0(txtQuantity.Text));

                    dr["AMOUNT"] = decimal.Parse(txtRate.Text) * Convert.ToInt32(dr["Quantity"]);
                }
                else
                {
                    dr["Quantity"] = int.Parse(dc.chkNull_0(txtQuantity.Text)) * mPackSize;

                    decimal rate = Convert.ToDecimal(txtRate.Text);

                    decimal qty = Convert.ToDecimal(dr["Quantity"]);

                    dr["PRICE"] = Convert.ToDecimal((txtAmount.Text)) / (qty);

                    dr["AMOUNT"] = Convert.ToDecimal(txtAmount.Text.ToString());


                }


            }
            this.Session.Add("PurchaseSKU", PurchaseSKU);
            this.ClearAll();
            this.LoadGird();
            DisAbaleOption(true);
            ScriptManager.GetCurrent(Page).SetFocus(ddlSKuCde);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Wrong SKU Select');", true);

        }
    }
   
    /// <summary>
    /// Saves All Document Detail Grid Data
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveDocument_Click(object sender, EventArgs e)
    {
        SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];

        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        if (PurchaseSKU.Rows.Count > 0)
        {
            DistributorController mDayClose = new DistributorController();
            if (CalculatePurchase(DateTime.Parse(Session["CurrentWorkDate"].ToString())))
            {
                PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
                PurchaseSKU.Rows.Clear();
                this.Session.Add("PurchaseSKU", PurchaseSKU);
                this.LoadGird();
                this.GetDocumentNo();
               
                this.ClearAll();

                txtExchangeRate.Text = "";
                txtRefNo.Text = "";
                DisAbaleOption(false);
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Record save successfully');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' At least one SKU must enter');", true);

        }
    }

    /// <summary>
    /// Resets Form Controls
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        DisAbaleOption(false);
        this.CreatTable();
        this.LoadGird();
        this.ClearAll();
        txtRefNo.Text = "";
        txtExchangeRate.Text = "";
    }

    #endregion

    #region Index/Change

    /// <summary>
    /// Loads Document Detail Data To Document Detail Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDocumentNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpDocumentNo.SelectedValue.ToString() == Constants.LongNullValue.ToString())
        {
            this.CreatTable();
            this.Session.Add("PurchaseSKU", PurchaseSKU);
            LoadGird();
            this.ClearAll();
            txtExchangeRate.Text = "";
            txtRefNo.Text = "";
            drpPrincipal.Enabled = true;
            drpDistributor.Enabled = true;
            RbUnitType.Enabled = true;
        }
        else
        {
            drpPrincipal.Enabled = false;
            drpDistributor.Enabled = false;
            RbUnitType.Enabled = false;
            this.LoadDocumentDetail();
            this.LoadSKUDetail();
        }
    }
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearAll();
        this.GetDocumentNo();
    }

    /// <summary>
    /// Loads Document Detail To Document Detail Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    /// 
    protected void drpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearAll();
        this.LoadSKUDetail();
    }

    #endregion

    /// <summary>
    /// Save Document Detail Grid Data
    /// </summary>
    /// <param name="MWorkDate">Date</param>
    /// <returns>bool</returns>
    private bool CalculatePurchase(DateTime CWorkDate)
    {
        bool flag = false;

        decimal mTotalAmount = 0;

        PurchaseController mController = new PurchaseController();
        DataTable dtPurchaseDetail = (DataTable)this.Session["PurchaseSKU"];
        foreach (DataRow dr in dtPurchaseDetail.Rows)
        {
            mTotalAmount += decimal.Parse(dr["AMOUNT"].ToString());

        }

        if (drpDocumentNo.SelectedValue.ToString() == Constants.LongNullValue.ToString())
        {
            if (!IsBillBookNoExist())
            {
                bool mResult = mController.InsertPurchaseOrder(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()), txtRefNo.Text,int.Parse(RbUnitType.SelectedValue)
                , Convert.ToInt32(ddlCurrency.SelectedValue), Convert.ToDecimal(dc.chkNull_0(txtExchangeRate.Text)), CWorkDate, mTotalAmount, int.Parse(this.Session["UserId"].ToString()), dtPurchaseDetail);
                return mResult;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('This Ref No already exist,Kindly enter different Ref No');", true);
            }
        }

        else
        {
            bool mResult = mController.UpdatePurchaseOrderDetail(int.Parse(drpDocumentNo.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), txtRefNo.Text, Convert.ToInt32(ddlCurrency.SelectedValue)
            , mTotalAmount, Convert.ToDecimal(dc.chkNull_0(txtExchangeRate.Text)), int.Parse(this.Session["UserId"].ToString()), dtPurchaseDetail);
            return mResult;
        }
        return flag;
    }

    /// <summary>
    /// Enables/Disables Controls
    /// </summary>
    /// <param name="IsDisable">bool</param>
    private void DisAbaleOption(bool IsDisable)
    {
        if (IsDisable == true)
        {
            drpPrincipal.Enabled = false;
            drpDistributor.Enabled = false;
            drpDocumentNo.Enabled = false;
            RbUnitType.Enabled = false;
            txtRefNo.Enabled = false;
            ddlCurrency.Enabled = false;
            txtExchangeRate.Enabled = false;
        }
        else
        {
            drpPrincipal.Enabled = true;
            drpDistributor.Enabled = true;
            drpDocumentNo.Enabled = true;
            drpDocumentNo.SelectedIndex = 0;
            txtRefNo.Enabled = true;
            ddlCurrency.Enabled = true;
            txtExchangeRate.Enabled = true;
            RbUnitType.Enabled = true;
        }
    }

    private void ClearAll()
    {
        
        txtQuantity.Text = "";
        txtRate.Text = "0.00";
        txtAmount.Text = "0.00";
         
        btnSave.Text = "Add Sku";
    }

    
}