using System;
using System.Data;
using SAMSCommonLayer.Classes;
using SAMSDatabaseLayer.Classes;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections;
using SAMSDataAccessLayer.Classes;

namespace SAMSBusinessLayer.Classes
{
   public  class VenderEntryController
    {


       #region Constructor

        /// <summary>
        /// Constructor for OrderEntryController
        /// </summary>
       public VenderEntryController()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#endregion
       #region Insert 
       public bool Add_Vender_Order(int p_Distributor_id, string p_MANUAL_ORDER_ID, int p_TOWN_ID, long p_AREA_ID, int p_PRINCIPAL_ID, long p_SOLD_TO, long p_SHIP_TO, int p_ORDERBOOKER_ID, int p_DELIVERYMAN_ID, int p_OrderTypeId,
       decimal p_TOTAL_AMOUNT, decimal p_EXTRA_DISCOUNT_AMOUNT, decimal p_STANDARD_DISCOUNT_AMOUNT, decimal p_GST_AMOUNT, decimal p_TOTAL_NET_AMOUNT, decimal p_SCHEME_AMOUNT, int p_STATUS_ID, DataTable dtOrderDetail, DataTable dtFreeSKU, int p_UserId, DateTime p_Document_Date, decimal p_SEDAmount, decimal p_TSTAmount, int p_WareHouseID, string p_Transporter, string p_Truck_No, string p_Truck_size, string p_Driver_Cell_No)
       {

           IDbConnection mConnection = null;
           IDbTransaction mTransaction = null;
           decimal TotalAmt = 0, DiscountAmount = 0, ExtraDiscount = 0, GSTAmount = 0, TotalNetAmt = 0;
           try
           {
               mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
               mConnection.Open();
               mTransaction = ProviderFactory.GetTransaction(mConnection);

               spInsertVENDER_ORDER_MASTER mISom = new spInsertVENDER_ORDER_MASTER();
               mISom.Connection = mConnection;
               mISom.Transaction = mTransaction;

               //------------Insert into Sale Order Master----------

               if (dtOrderDetail.Rows.Count > 0)
               {
                   mISom.DISTRIBUTOR_ID = p_Distributor_id;
                   mISom.MANUAL_ORDER_ID = p_MANUAL_ORDER_ID;
                   mISom.PRINCIPAL_ID = p_PRINCIPAL_ID;
                   mISom.AREA_ID = p_AREA_ID;
                   mISom.DELIVERYMAN_ID = p_DELIVERYMAN_ID;
                   mISom.ORDERBOOKER_ID = p_ORDERBOOKER_ID;
                   mISom.DOCUMENT_DATE = p_Document_Date;
                   mISom.SHIP_TO = p_SHIP_TO;
                   mISom.SOLD_TO = p_SOLD_TO;
                   mISom.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                   mISom.EXTRA_DISCOUNT_AMOUNT = p_EXTRA_DISCOUNT_AMOUNT;
                   mISom.STANDARD_DISCOUNT_AMOUNT = p_STANDARD_DISCOUNT_AMOUNT;
                   mISom.GST_AMOUNT = p_GST_AMOUNT;
                   mISom.SCHEME_AMOUNT = p_SCHEME_AMOUNT;
                   mISom.TOTAL_NET_AMOUNT = p_TOTAL_NET_AMOUNT;
                   mISom.TOWN_ID = p_TOWN_ID;
                   mISom.STATUS_ID = p_STATUS_ID;
                   mISom.USER_ID = p_UserId;
                   mISom.TST_AMOUNT = p_TSTAmount;
                   mISom.SED_AMOUNT = p_SEDAmount;
                   mISom.ORDER_TYPE_ID = p_OrderTypeId;
                   mISom.TIME_STAMP = DateTime.Now;
                   mISom.LASTUPDATE_DATE = System.DateTime.Now;
                   mISom.WAREHOUSE_ID = p_WareHouseID;
                   //   
                   mISom.TRANSPORTER = p_Transporter;
                   mISom.TRUCK_NO = p_Truck_No;
                   mISom.TRUCK_SIZE = p_Truck_size;
                   mISom.DRIVER_CELL_NO = p_Driver_Cell_No;
                   //
                   mISom.ExecuteQuery();


                   //----------------Insert into sale order detail-------------
                   spInsertVENDER_ORDER_DETAIL mSaleOrderDetail = new spInsertVENDER_ORDER_DETAIL();
                   mSaleOrderDetail.Connection = mConnection;
                   mSaleOrderDetail.Transaction = mTransaction;

                   foreach (DataRow dr in dtOrderDetail.Rows)
                   {
                       //SaleOrderDetail_Collection mSod_Col=new SaleOrderDetail_Collection ();
                       mSaleOrderDetail.SALE_ORDER_ID = mISom.VENDER_ORDER_ID;
                       mSaleOrderDetail.DISTRIBUTOR_ID = p_Distributor_id;
                       mSaleOrderDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                       mSaleOrderDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                       mSaleOrderDetail.QUANTITY_UNIT = int.Parse(dr["QUANTITY_UNIT"].ToString());
                       mSaleOrderDetail.UNIT_PRICE = decimal.Parse(dr["UNIT_PRICE"].ToString());
                       mSaleOrderDetail.GST_RATE = float.Parse(dr["GST_RATE"].ToString());
                       mSaleOrderDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                       mSaleOrderDetail.EXTRA_DISCOUNT = decimal.Parse(dr["EXTRA_DISCOUNT"].ToString());
                       mSaleOrderDetail.STANDARD_DISCOUNT = decimal.Parse(dr["STANDARD_DISCOUNT"].ToString());
                       mSaleOrderDetail.GST_AMOUNT = decimal.Parse(dr["GST_AMOUNT"].ToString());
                       mSaleOrderDetail.TST_AMOUNT = decimal.Parse(dr["TST_AMOUNT"].ToString());
                       mSaleOrderDetail.SED_AMOUNT = decimal.Parse(dr["SED_AMOUNT"].ToString());
                       mSaleOrderDetail.NET_AMOUNT = decimal.Parse(dr["NET_AMOUNT"].ToString());
                       mSaleOrderDetail.UNLOADING_EXPENSE_RATE = decimal.Parse(dr["unloading_expense_rate"].ToString());
                       mSaleOrderDetail.IS_DELETED = false;
                       mSaleOrderDetail.TIME_STAMP = Convert.ToDateTime(dr["ExpDate"]);

                       TotalAmt += decimal.Parse(dr["AMOUNT"].ToString());
                       ExtraDiscount += decimal.Parse(dr["EXTRA_DISCOUNT"].ToString());
                       DiscountAmount += decimal.Parse(dr["STANDARD_DISCOUNT"].ToString());
                       GSTAmount += decimal.Parse(dr["GST_AMOUNT"].ToString());
                       TotalNetAmt += decimal.Parse(dr["NET_AMOUNT"].ToString());
                       mSaleOrderDetail.ExecuteQuery();



                   }
                   foreach (DataRow df in dtFreeSKU.Rows)
                   {
                       //----------------Insert into sale order Promotion-------------
                       spInsertVENDER_ORDER_PROMOTION mSaleOrderPromo = new spInsertVENDER_ORDER_PROMOTION();
                       mSaleOrderPromo.Connection = mConnection;
                       mSaleOrderPromo.Transaction = mTransaction;

                       mSaleOrderPromo.BASKET_DETAIL_ID = int.Parse(df["BASKET_DETAIL_ID"].ToString());
                       mSaleOrderPromo.BASKET_ID = int.Parse(df["BASKET_ID"].ToString());
                       mSaleOrderPromo.DISTRIBUTOR_ID = p_Distributor_id;
                       mSaleOrderPromo.GST_AMOUNT = decimal.Parse(df["GST_AMOUNT"].ToString());
                       mSaleOrderPromo.GST_RATE = float.Parse(df["GST_RATE"].ToString());
                       mSaleOrderPromo.PROMOTION_ID = int.Parse(df["PROMOTION_ID"].ToString());
                       mSaleOrderPromo.PROMOTION_OFFER_ID = int.Parse(df["PROMOTION_OFFER_ID"].ToString());
                       mSaleOrderPromo.QUANTITY = int.Parse(df["Quantity"].ToString());
                       mSaleOrderPromo.SKU_ID = int.Parse(df["SKU_ID"].ToString());
                       mSaleOrderPromo.UNIT_PRICE = decimal.Parse(df["UNIT_PRICE"].ToString());
                       mSaleOrderPromo.VENDER_ORDER_ID = mISom.VENDER_ORDER_ID;
                       mSaleOrderPromo.AMOUNT = decimal.Parse(df["AMOUNT"].ToString());
                       mSaleOrderPromo.TST_AMOUNT = decimal.Parse(df["TST_AMOUNT"].ToString());
                       mSaleOrderPromo.SED_AMOUNT = 0;
                       mSaleOrderPromo.ExecuteQuery();
                   }

                   mTransaction.Commit();
                   return true;
               }
           }
           catch (Exception exp)
           {
               ExceptionPublisher.PublishException(exp);
               return false;// exp.Message;
           }
           finally
           {
               if (mConnection != null && mConnection.State == ConnectionState.Open)
               {
                   mConnection.Close();
               }
           }
           return true;
       }

        
       public bool Add_Vender_Invoice(int p_Distributor_id, string p_MANUAL_INVOICE_ID, int p_TOWN_ID, long p_AREA_ID, int p_PRINCIPAL_ID, long p_SOLD_TO, long p_SHIP_TO, int p_ORDERBOOKER_ID, int p_DELIVERYMAN_ID, long p_Orderid,
decimal p_TOTAL_AMOUNT, decimal p_EXTRA_DISCOUNT_AMOUNT, decimal p_STANDARD_DISCOUNT_AMOUNT, decimal p_GST_AMOUNT, decimal p_TOTAL_NET_AMOUNT, decimal p_SCHEME_AMOUNT, int InvoiceTypeId, DataTable dtOrderDetail, DataTable dtFreeSKU, int p_UserId, decimal p_CashReceived, DateTime p_DocumentDate, decimal p_TSTAmount, decimal p_SEDAmount, int p_DistributorID2, int p_WAREHOUSE_ID, string p_Transporter, string p_Truck_No, string p_Truck_size, string p_Driver_Cell_No)
       {

           IDbConnection mConnection = null;
           IDbTransaction mTransaction = null;
           decimal TotalAmt = 0, DiscountAmount = 0, ExtraDiscount = 0, GSTAmount = 0, TotalNetAmt = 0;
           try
           {
               mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
               mConnection.Open();
               mTransaction = ProviderFactory.GetTransaction(mConnection);

               spInsertVENDER_INVOICE_MASTER mISom = new spInsertVENDER_INVOICE_MASTER();
               mISom.Connection = mConnection;
               mISom.Transaction = mTransaction;

               //------------Insert into Sale Order Master----------

               if (dtOrderDetail.Rows.Count > 0)
               {
                   mISom.DISTRIBUTOR_ID = p_Distributor_id;
                   mISom.WAREHOUSE_ID = p_WAREHOUSE_ID;
                   mISom.MANUAL_INVOICE_ID = p_MANUAL_INVOICE_ID;
                   mISom.PRINCIPAL_ID = p_PRINCIPAL_ID;
                   mISom.AREA_ID = p_AREA_ID;
                   mISom.DELIVERYMAN_ID = p_DELIVERYMAN_ID;
                   mISom.ORDERBOOKER_ID = p_ORDERBOOKER_ID;
                   mISom.DOCUMENT_DATE = p_DocumentDate;
                   mISom.SOLD_TO = p_SOLD_TO;
                   mISom.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                   mISom.EXTRA_DISCOUNT_AMOUNT = p_EXTRA_DISCOUNT_AMOUNT;
                   mISom.STANDARD_DISCOUNT_AMOUNT = p_STANDARD_DISCOUNT_AMOUNT;
                   mISom.GST_AMOUNT = p_GST_AMOUNT;
                   mISom.SCHEME_AMOUNT = p_SCHEME_AMOUNT;
                   mISom.IS_DELETED = false;
                   mISom.TOTAL_NET_AMOUNT = p_TOTAL_NET_AMOUNT;
                   if (InvoiceTypeId == Constants.Credit_Order_Id)
                   {
                       mISom.CREDIT_AMOUNT = p_TOTAL_NET_AMOUNT - p_CashReceived;
                       mISom.CURRENT_CREDIT_AMOUNT = p_TOTAL_NET_AMOUNT - p_CashReceived;
                   }
                   else
                   {
                       mISom.CREDIT_AMOUNT = 0;
                       mISom.CURRENT_CREDIT_AMOUNT = 0;
                   }
                   mISom.TOWN_ID = p_TOWN_ID;
                   mISom.USER_ID = p_UserId;
                   mISom.SALE_ORDER_ID = p_Orderid;
                   mISom.TST_AMOUNT = p_TSTAmount;
                   mISom.SED_AMOUNT = p_SEDAmount;
                   mISom.TIME_STAMP = DateTime.Now;
                   mISom.LASTUPDATE_DATE = System.DateTime.Now;
                   mISom.IS_DELETED = false;
                   mISom.POSTING = 0;
                   // BY  
                   mISom.TRANSPORTER = p_Transporter;
                   mISom.TRUCK_NO = p_Truck_No;
                   mISom.TRUCK_SIZE = p_Truck_size;
                   mISom.DRIVER_CELL_NO = p_Driver_Cell_No;
                   //
                   mISom.ExecuteQuery();

                   //------------------Ledger Posting--------------------------\\



                   //----------------Insert into sale order detail-------------
                   spInsertVENDER_INVOICE_DETAIL mSaleOrderDetail = new spInsertVENDER_INVOICE_DETAIL();
                   mSaleOrderDetail.Connection = mConnection;
                   mSaleOrderDetail.Transaction = mTransaction;

                   foreach (DataRow dr in dtOrderDetail.Rows)
                   {
                       //SaleOrderDetail_Collection mSod_Col=new SaleOrderDetail_Collection ();
                       mSaleOrderDetail.VENDER_INVOICE_ID = mISom.VENDER_INVOICE_ID;
                       mSaleOrderDetail.DISTRIBUTOR_ID = p_Distributor_id;
                       mSaleOrderDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                       mSaleOrderDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                       mSaleOrderDetail.QUANTITY_UNIT = int.Parse(dr["QUANTITY_UNIT"].ToString());
                       mSaleOrderDetail.UNIT_PRICE = decimal.Parse(dr["UNIT_PRICE"].ToString());
                       mSaleOrderDetail.GST_RATE = float.Parse(dr["GST_RATE"].ToString());
                       mSaleOrderDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                       mSaleOrderDetail.EXTRA_DISCOUNT = decimal.Parse(dr["EXTRA_DISCOUNT"].ToString());
                       mSaleOrderDetail.STANDARD_DISCOUNT = decimal.Parse(dr["STANDARD_DISCOUNT"].ToString());
                       mSaleOrderDetail.GST_AMOUNT = decimal.Parse(dr["GST_AMOUNT"].ToString());
                       mSaleOrderDetail.TST_AMOUNT = decimal.Parse(dr["TST_AMOUNT"].ToString());
                       mSaleOrderDetail.SED_AMOUNT = decimal.Parse(dr["SED_AMOUNT"].ToString());
                       mSaleOrderDetail.NET_AMOUNT = decimal.Parse(dr["NET_AMOUNT"].ToString());
                       mSaleOrderDetail.UNLOADING_EXPENSE_RATE = decimal.Parse(dr["unloading_expense_rate"].ToString());
                       mSaleOrderDetail.IS_DELETED = false;
                       mSaleOrderDetail.TIME_STAMP = Convert.ToDateTime(dr["ExpDate"]);
                       TotalAmt += decimal.Parse(dr["AMOUNT"].ToString());
                       ExtraDiscount += decimal.Parse(dr["EXTRA_DISCOUNT"].ToString());
                       DiscountAmount += decimal.Parse(dr["STANDARD_DISCOUNT"].ToString());
                       GSTAmount += decimal.Parse(dr["GST_AMOUNT"].ToString());
                       TotalNetAmt += decimal.Parse(dr["NET_AMOUNT"].ToString());
                       mSaleOrderDetail.ExecuteQuery();

                       //UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                       //mStockUpdate.Connection = mConnection;
                       //mStockUpdate.Transaction = mTransaction;
                       //mStockUpdate.TYPE_ID = Constants.Document_Invoice;
                       //mStockUpdate.DISTRIBUTOR_ID = p_DistributorID2;
                       //mStockUpdate.STOCK_DATE = p_DocumentDate;
                       //mStockUpdate.SKU_ID = mSaleOrderDetail.SKU_ID;
                       //mStockUpdate.BATCHNO = mSaleOrderDetail.BATCH_NO;
                       //mStockUpdate.STOCK_QTY = mSaleOrderDetail.QUANTITY_UNIT;
                       //mStockUpdate.FREE_QTY = 0;
                       //mStockUpdate.ExecuteQuery();

                       //uspProcessSKUStock mSKUStock = new uspProcessSKUStock();
                       //mSKUStock.Connection = mConnection;
                       //mSKUStock.Transaction = mTransaction;
                       //mSKUStock.SKU_ID = mSaleOrderDetail.SKU_ID;
                       //mSKUStock.DISTRIBUTOR_ID = p_DistributorID2;
                       //mSKUStock.BATCH_NO = mSaleOrderDetail.BATCH_NO;
                       //mSKUStock.UNITS_IN_STOCK = mSaleOrderDetail.QUANTITY_UNIT;
                       //mSKUStock.EXPIRY_DATE = mSaleOrderDetail.TIME_STAMP;
                       //mSKUStock.TYPE_ID = Constants.Document_Invoice;
                       //mSKUStock.ExecuteQuery();

                   }
                   foreach (DataRow df in dtFreeSKU.Rows)
                   {
                       //----------------Insert into sale order Promotion-------------
                       spInsertVENDER_INVOICE_PROMOTION mSaleOrderPromo = new spInsertVENDER_INVOICE_PROMOTION();
                       mSaleOrderPromo.Connection = mConnection;
                       mSaleOrderPromo.Transaction = mTransaction;

                       mSaleOrderPromo.BASKET_DETAIL_ID = int.Parse(df["BASKET_DETAIL_ID"].ToString());
                       mSaleOrderPromo.BASKET_ID = int.Parse(df["BASKET_ID"].ToString());
                       mSaleOrderPromo.DISTRIBUTOR_ID = p_Distributor_id;
                       mSaleOrderPromo.GST_AMOUNT = decimal.Parse(df["GST_AMOUNT"].ToString());
                       mSaleOrderPromo.GST_RATE = float.Parse(df["GST_RATE"].ToString());
                       mSaleOrderPromo.PROMOTION_ID = int.Parse(df["PROMOTION_ID"].ToString());
                       mSaleOrderPromo.PROMOTION_OFFER_ID = int.Parse(df["PROMOTION_OFFER_ID"].ToString());
                       mSaleOrderPromo.QUANTITY = int.Parse(df["Quantity"].ToString());
                       mSaleOrderPromo.SKU_ID = int.Parse(df["SKU_ID"].ToString());
                       mSaleOrderPromo.UNIT_PRICE = decimal.Parse(df["UNIT_PRICE"].ToString());
                       mSaleOrderPromo.TST_AMOUNT = decimal.Parse(df["TST_AMOUNT"].ToString());
                       mSaleOrderPromo.SED_AMOUNT = 0;
                       mSaleOrderPromo.VENDER_INVOICE_ID = mISom.VENDER_INVOICE_ID;
                       mSaleOrderPromo.AMOUNT = decimal.Parse(df["AMOUNT"].ToString());
                       mSaleOrderPromo.ExecuteQuery();

                       //UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                       //mStockUpdate.Connection = mConnection;
                       //mStockUpdate.Transaction = mTransaction;
                       //mStockUpdate.TYPE_ID = Constants.Document_Invoice;
                       //mStockUpdate.DISTRIBUTOR_ID = p_DistributorID2;
                       //mStockUpdate.STOCK_DATE = p_DocumentDate;
                       //mStockUpdate.SKU_ID = mSaleOrderPromo.SKU_ID;
                       //mStockUpdate.BATCHNO = "N/A";
                       //mStockUpdate.STOCK_QTY = 0;
                       //mStockUpdate.FREE_QTY = mSaleOrderPromo.QUANTITY;
                       //mStockUpdate.ExecuteQuery();
                   }

                   #region Account Posting
                   LedgerController LController = new LedgerController();
                   Configuration.GetAccountHead();
                   DistributorController Dcontroller = new DistributorController();


                   string VoucherNo = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, p_Distributor_id);

                   if (InvoiceTypeId == Constants.Advance_PaymentOrder_id)
                   {
                       LController.PostingPrinvipalInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), 97, p_Distributor_id, 0, p_TOTAL_NET_AMOUNT, mISom.DOCUMENT_DATE, "", DateTime.Now, p_PRINCIPAL_ID, int.Parse(p_SOLD_TO.ToString()), mISom.VENDER_INVOICE_ID, mISom.MANUAL_INVOICE_ID, Constants.Document_PrincipalInvoice, p_UserId, mTransaction, mConnection, Constants.Cash_Advance, p_DELIVERYMAN_ID.ToString());
                       LController.PostingPrinvipalInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.PayableAccount), p_Distributor_id, p_TOTAL_NET_AMOUNT, 0, mISom.DOCUMENT_DATE,"", DateTime.Now, p_PRINCIPAL_ID, int.Parse(p_SOLD_TO.ToString()), mISom.VENDER_INVOICE_ID, mISom.MANUAL_INVOICE_ID, Constants.Document_PrincipalInvoice, p_UserId, mTransaction, mConnection, Constants.Cash_Advance, p_DELIVERYMAN_ID.ToString());

                   }
                   else if (InvoiceTypeId == Constants.Credit_Order_Id)
                   {
                       LController.PostingPrinvipalInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.AccountReceivable), p_Distributor_id, p_TOTAL_NET_AMOUNT - p_CashReceived, 0, mISom.DOCUMENT_DATE, "Credit Sale Default", DateTime.Now, p_PRINCIPAL_ID, int.Parse(p_SOLD_TO.ToString()), mISom.VENDER_INVOICE_ID, mISom.MANUAL_INVOICE_ID, Constants.Document_PrincipalInvoice, p_UserId, mTransaction, mConnection, Constants.CreditSale, p_DELIVERYMAN_ID.ToString());
                       LController.PostingPrinvipalInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.SaleAccount), p_Distributor_id, 0, p_TOTAL_NET_AMOUNT - p_CashReceived, mISom.DOCUMENT_DATE, "Credit Sale Default", DateTime.Now, p_PRINCIPAL_ID, int.Parse(p_SOLD_TO.ToString()), mISom.VENDER_INVOICE_ID, mISom.MANUAL_INVOICE_ID, Constants.Document_PrincipalInvoice, p_UserId, mTransaction, mConnection, Constants.CreditSale, p_DELIVERYMAN_ID.ToString());

                   }
                   #endregion

                   #region Update Pending Order

                   //spUpdateSALE_ORDER_MASTER mOrderUpdate = new spUpdateSALE_ORDER_MASTER();
                   //mOrderUpdate.Connection = mConnection;
                   //mOrderUpdate.Transaction = mTransaction;
                   //mOrderUpdate.DISTRIBUTOR_ID = p_Distributor_id;
                   //mOrderUpdate.SALE_ORDER_ID = p_Orderid;
                   //mOrderUpdate.STATUS_ID = Constants.Order_Posted_Id;
                   //mOrderUpdate.ExecuteQuery();

                   #endregion

                   mTransaction.Commit();
                   return true;
               }
           }
           catch (Exception exp)
           {
               ExceptionPublisher.PublishException(exp);
               return false;// exp.Message;
           }
           finally
           {
               if (mConnection != null && mConnection.State == ConnectionState.Open)
               {
                   mConnection.Close();
               }
           }
           return true;
       }

       public bool Update_Vender_Invoice(long p_SALE_INVOICE_ID, DateTime p_DocumentDate, int p_Distributor_id, string p_MANUAL_INVOICE_ID, long p_SOLD_TO, int p_DELIVERYMAN_ID,
     decimal p_TOTAL_AMOUNT, decimal p_EXTRA_DISCOUNT_AMOUNT, decimal p_STANDARD_DISCOUNT_AMOUNT, decimal p_GST_AMOUNT, decimal p_TOTAL_NET_AMOUNT, decimal p_SCHEME_AMOUNT, int InvoiceTypeId, DataTable dtOrderDetail, int p_UserId, decimal p_CashReceived, decimal p_TSTAmount, decimal p_SEDAmount, int p_WAREHOUSE_ID, string p_Transporter, string p_Truck_No, string p_Truck_size, string p_Driver_Cell_No)
       {
           IDbConnection mConnection = null;
           IDbTransaction mTransaction = null;

           try
           {
               try
               {
                   mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                   mConnection.Open();
                   foreach (DataRow dr in dtOrderDetail.Rows)
                   {
                       uspDeleteVenderDetail mOrder = new uspDeleteVenderDetail();
                       mOrder.Connection = mConnection;
                       mOrder.VENDER_INVOICE_ID = p_SALE_INVOICE_ID;
                       mOrder.DISTRIBUTOR_ID = p_Distributor_id;
                       mOrder.QUANTITY = Convert.ToInt32(dr["QUANTITY_UNIT"]);
                       mOrder.SKU_ID = Convert.ToInt32(dr["SKU_ID"]);
                       mOrder.STOCK_DATE = p_DocumentDate;
                       mOrder.ExecuteQuery();
                   }

               }
               catch (Exception exp)
               {
                   ExceptionPublisher.PublishException(exp);
               }
               finally
               {
                   if (mConnection != null && mConnection.State == ConnectionState.Open)
                   {
                       mConnection.Close();
                   }
               }


               decimal TotalAmt = 0, DiscountAmount = 0, ExtraDiscount = 0, GSTAmount = 0, TotalNetAmt = 0;

               mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
               mConnection.Open();
               mTransaction = ProviderFactory.GetTransaction(mConnection);

               uspUpdateVenderMaster mISom = new uspUpdateVenderMaster();
               mISom.Connection = mConnection;
               mISom.Transaction = mTransaction;

               //------------Insert into Sale Order Master----------

               if (dtOrderDetail.Rows.Count > 0)
               {
                   mISom.DISTRIBUTOR_ID = p_Distributor_id;
                   mISom.WAREHOUSE_ID = p_WAREHOUSE_ID;
                   mISom.MANUAL_INVOICE_ID = p_MANUAL_INVOICE_ID;
                   mISom.VENDER_INVOICE_ID = p_SALE_INVOICE_ID;
                   mISom.DELIVERYMAN_ID = p_DELIVERYMAN_ID;
                   mISom.SOLD_TO = p_SOLD_TO;
                   mISom.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                   mISom.EXTRA_DISCOUNT_AMOUNT = p_EXTRA_DISCOUNT_AMOUNT;
                   mISom.STANDARD_DISCOUNT_AMOUNT = p_STANDARD_DISCOUNT_AMOUNT;
                   mISom.GST_AMOUNT = p_GST_AMOUNT;
                   mISom.SCHEME_AMOUNT = p_SCHEME_AMOUNT;
                   mISom.TOTAL_NET_AMOUNT = p_TOTAL_NET_AMOUNT;
                   if (InvoiceTypeId == Constants.Credit_Order_Id)
                   {
                       mISom.CREDIT_AMOUNT = p_TOTAL_NET_AMOUNT - p_CashReceived;
                       mISom.CURRENT_CREDIT_AMOUNT = p_TOTAL_NET_AMOUNT - p_CashReceived;
                   }
                   else
                   {
                       mISom.CREDIT_AMOUNT = 0;
                       mISom.CURRENT_CREDIT_AMOUNT = 0;
                   }
                   mISom.USER_ID = p_UserId;
                   mISom.TST_AMOUNT = p_TSTAmount;
                   mISom.SED_AMOUNT = p_SEDAmount;
                   // BY  
                   mISom.TRANSPORTER = p_Transporter;
                   mISom.TRUCK_NO = p_Truck_No;
                   mISom.TRUCK_SIZE = p_Truck_size;
                   mISom.DRIVER_CELL_NO = p_Driver_Cell_No;
                   //
                   mISom.ExecuteQuery();

                   //----------------Insert into sale order detail-------------
                   spInsertVENDER_INVOICE_DETAIL mSaleOrderDetail = new spInsertVENDER_INVOICE_DETAIL();
                   mSaleOrderDetail.Connection = mConnection;
                   mSaleOrderDetail.Transaction = mTransaction;

                   foreach (DataRow dr in dtOrderDetail.Rows)
                   {
                       //SaleOrderDetail_Collection mSod_Col=new SaleOrderDetail_Collection ();
                       mSaleOrderDetail.VENDER_INVOICE_ID = p_SALE_INVOICE_ID;
                       mSaleOrderDetail.DISTRIBUTOR_ID = p_Distributor_id;
                       mSaleOrderDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                       mSaleOrderDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                       mSaleOrderDetail.QUANTITY_UNIT = int.Parse(dr["QUANTITY_UNIT"].ToString());
                       mSaleOrderDetail.UNIT_PRICE = decimal.Parse(dr["UNIT_PRICE"].ToString());
                       mSaleOrderDetail.GST_RATE = float.Parse(dr["GST_RATE"].ToString());
                       mSaleOrderDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                       mSaleOrderDetail.EXTRA_DISCOUNT = decimal.Parse(dr["EXTRA_DISCOUNT"].ToString());
                       mSaleOrderDetail.STANDARD_DISCOUNT = decimal.Parse(dr["STANDARD_DISCOUNT"].ToString());
                       mSaleOrderDetail.GST_AMOUNT = 0;
                       mSaleOrderDetail.TST_AMOUNT = 0;
                       mSaleOrderDetail.SED_AMOUNT = 0;
                       mSaleOrderDetail.NET_AMOUNT = decimal.Parse(dr["NET_AMOUNT"].ToString());
                       mSaleOrderDetail.UNLOADING_EXPENSE_RATE = decimal.Parse(dr["CLAIM_PER"].ToString());
                       mSaleOrderDetail.IS_DELETED = false;
                       mSaleOrderDetail.TIME_STAMP = p_DocumentDate;
                       TotalAmt += decimal.Parse(dr["AMOUNT"].ToString());
                       ExtraDiscount += decimal.Parse(dr["EXTRA_DISCOUNT"].ToString());
                       DiscountAmount += decimal.Parse(dr["STANDARD_DISCOUNT"].ToString());
                       GSTAmount += decimal.Parse(dr["GST_AMOUNT"].ToString());
                       TotalNetAmt += decimal.Parse(dr["NET_AMOUNT"].ToString());
                       mSaleOrderDetail.ExecuteQuery();

                       //UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                       //mStockUpdate.Connection = mConnection;
                       //mStockUpdate.Transaction = mTransaction;
                       //mStockUpdate.TYPE_ID = Constants.Document_Invoice;
                       //mStockUpdate.DISTRIBUTOR_ID = p_WAREHOUSE_ID;
                       //mStockUpdate.STOCK_DATE = p_DocumentDate;
                       //mStockUpdate.SKU_ID = mSaleOrderDetail.SKU_ID;
                       //mStockUpdate.BATCHNO = mSaleOrderDetail.BATCH_NO;
                       //mStockUpdate.STOCK_QTY = mSaleOrderDetail.QUANTITY_UNIT;
                       //mStockUpdate.FREE_QTY = 0;
                       //mStockUpdate.ExecuteQuery();
                   }

                   #region Account Posting
                   //LedgerController LController = new LedgerController();
                   //Configuration.GetAccountHead();
                   //DistributorController Dcontroller = new DistributorController();


                   //string VoucherNo = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, p_Distributor_id);

                   //if (InvoiceTypeId == Constants.Advance_PaymentOrder_id)
                   //{
                   //    LController.PostingInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.SaleAccount), p_Distributor_id, 0, p_TOTAL_AMOUNT, p_DocumentDate, "Gross Sale Value", DateTime.Now, 0, int.Parse(p_SOLD_TO.ToString()), mISom.SALE_INVOICE_ID, mISom.MANUAL_INVOICE_ID, Constants.Document_SaleInvoice, p_UserId, mTransaction, mConnection, Constants.Cash_Advance, p_DELIVERYMAN_ID.ToString());

                   //    if (p_STANDARD_DISCOUNT_AMOUNT > 0)
                   //    {
                   //        LController.PostingInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.SaleDiscount), p_Distributor_id, p_STANDARD_DISCOUNT_AMOUNT, 0, p_DocumentDate, "Commision/Discount", DateTime.Now, 0, int.Parse(p_SOLD_TO.ToString()), mISom.SALE_INVOICE_ID, mISom.MANUAL_INVOICE_ID, Constants.Document_SaleInvoice, p_UserId, mTransaction, mConnection, Constants.Cash_Advance, p_DELIVERYMAN_ID.ToString());
                   //    }
                   //    if (p_EXTRA_DISCOUNT_AMOUNT > 0)
                   //    {
                   //        LController.PostingInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.SaleScheme), p_Distributor_id, p_EXTRA_DISCOUNT_AMOUNT, 0, p_DocumentDate, "Extra Discount", DateTime.Now, 0, int.Parse(p_SOLD_TO.ToString()), mISom.SALE_INVOICE_ID, mISom.MANUAL_INVOICE_ID, Constants.Document_SaleInvoice, p_UserId, mTransaction, mConnection, Constants.Cash_Advance, p_DELIVERYMAN_ID.ToString());
                   //    }
                   //    if (p_GST_AMOUNT + p_TSTAmount > 0)
                   //    {
                   //        LController.PostingInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.GSTAccount), p_Distributor_id, 0, p_GST_AMOUNT + p_TSTAmount, p_DocumentDate, "Sales Tax", DateTime.Now, 0, int.Parse(p_SOLD_TO.ToString()), mISom.SALE_INVOICE_ID, mISom.MANUAL_INVOICE_ID, Constants.Document_SaleInvoice, p_UserId, mTransaction, mConnection, Constants.Cash_Advance, p_DELIVERYMAN_ID.ToString());
                   //    }
                   //    LController.PostingInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.AccountReceivable), p_Distributor_id, p_TOTAL_NET_AMOUNT, 0, p_DocumentDate, "Credit Sale Default", DateTime.Now, 0, int.Parse(p_SOLD_TO.ToString()), mISom.SALE_INVOICE_ID, mISom.MANUAL_INVOICE_ID, Constants.Document_SaleInvoice, p_UserId, mTransaction, mConnection, Constants.Cash_Advance, p_DELIVERYMAN_ID.ToString());

                   //}
                   //else if (InvoiceTypeId == Constants.Credit_Order_Id)
                   //{
                   //    LController.PostingInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.AccountReceivable), p_Distributor_id, p_TOTAL_NET_AMOUNT - p_CashReceived, 0, p_DocumentDate, "Credit Sale Default", DateTime.Now, 0, int.Parse(p_SOLD_TO.ToString()), mISom.SALE_INVOICE_ID, mISom.MANUAL_INVOICE_ID, Constants.Document_SaleInvoice, p_UserId, mTransaction, mConnection, Constants.CreditSale, p_DELIVERYMAN_ID.ToString());
                   //    LController.PostingInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.SaleAccount), p_Distributor_id, 0, p_TOTAL_NET_AMOUNT - p_CashReceived, p_DocumentDate, "Credit Sale Default", DateTime.Now, 0, int.Parse(p_SOLD_TO.ToString()), mISom.SALE_INVOICE_ID, mISom.MANUAL_INVOICE_ID, Constants.Document_SaleInvoice, p_UserId, mTransaction, mConnection, Constants.CreditSale, p_DELIVERYMAN_ID.ToString());

                   //}
                   #endregion

                   mTransaction.Commit();
                   return true;
               }
           }
           catch (Exception exp)
           {
               ExceptionPublisher.PublishException(exp);
               return false;// exp.Message;
           }
           finally
           {
               if (mConnection != null && mConnection.State == ConnectionState.Open)
               {
                   mConnection.Close();
               }
           }
           return true;
       }


        #endregion


    }
}
