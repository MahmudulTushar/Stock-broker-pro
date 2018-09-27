﻿using System;
using System.Data;
using BusinessAccessLayer.BO;
using DataAccessLayer;
using BusinessAccessLayer.Constants;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;


namespace BusinessAccessLayer.BAL
{
    public class SMSSyncBAL
    {

        private DbConnection _dbConnection;

        public SMSSyncBAL()
        {
            _dbConnection = new DbConnection();
        }

        public void Connect_SBP()
        {
            _dbConnection.ConnectDatabase();
            _dbConnection.StartTransaction();
        }
        public void Connect_WithoutTransaction_SBP()
        {
            _dbConnection.ConnectDatabase();
           // _dbConnection.StartTransaction();
        }
        public void CloseConnection_SBP()
        {
            _dbConnection.ConnectDatabase();
        }

        public void Commit_SBP()
        {
            _dbConnection.Commit();

        }

        public void Rollback_SBP()
        {
            _dbConnection.Rollback();

        }

        public void Connect_SMS()
        {
            _dbConnection.ConnectDatabase_SMSSender();
            _dbConnection.StartTransaction_SMSSender();
        }

        public void Connect_WithoutTransaction_SMS()
        {
            _dbConnection.ConnectDatabase_SMSSender();
            //_dbConnection.StartTransaction_SMSSender();
        }

        public void CloseConnection_SMS()
        {
            _dbConnection.CloseDatabase_SMSSender();

        }

        public void Commit_SMS()
        {
            _dbConnection.Commit_SMSSender();

        }

        public void Rollback_SMS()
        {
            _dbConnection.Rollback_SMSSender();
        }

        public SqlConnection GetConnection()
        {
            return _dbConnection.GetConnection();
        }

        public void SetConnection(SqlConnection con)
        {
            _dbConnection.SetConnection(con);
        }

        public SqlConnection GetConnection_SMS()
        {
            return _dbConnection.GetConnection_SMSSender();
        }

        public void SetConnection_SMS(SqlConnection con)
        {
            _dbConnection.SetConnection_SMSSender(con);
        }

        public SqlTransaction GetTransaction()
        {
            return _dbConnection.GetTransaction();
        }

        public void SetTransaction(SqlTransaction trans)
        {
            _dbConnection.SetTransaction(trans);
        }

        public SqlTransaction GetTransaction_SMS()
        {
            return _dbConnection.GetTransaction_SMSSender();
        }

        public void SetTransaction_SMS(SqlTransaction trans)
        {
            _dbConnection.SetTransaction_SMSSender(trans);
        }

        #region TradeSync
        
        public void CCS_EmptyTables_UITransApplied()
        {
            string queryString = @"
                             TRUNCATE TABLE dbksclCallCenter..tbl_Customer_All  
                             TRUNCATE TABLE dbksclCallCenter..tbl_company_all  
                             TRUNCATE TABLE dbksclCallCenter..tbl_Cust_Paymnet_All  
                             TRUNCATE TABLE dbksclCallCenter..tbl_Share_Balance  
                             TRUNCATE TABLE dbksclCallCenter..tbl_ShareTrade_Details  
                             TRUNCATE TABLE dbksclCallCenter..tbl_Share_Draw_Withdraw_All  
                             TRUNCATE TABLE dbksclCallCenter..tbl_Customer_Account  ";
            try
            {
                //_dbConnection.ConnectDatabase();
                _dbConnection.ExecuteNonQuery_SMSSender(queryString);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
        }

        public SqlDataReader Get_CCS_Company_UITransApplied()
        {
            SqlDataReader dr;
            string queryString = @"SELECT  
                                 Code_No
                                 ,Comp_Name
                                 ,Comp_Short_Code
                                 ,Comp_Cat_ID=
                                 (
	                                CASE  
		                                WHEN Comp_Cat_ID= 1 THEN 'A'  
		                                WHEN Comp_Cat_ID= 2 THEN 'B'  
		                                WHEN Comp_Cat_ID= 3 THEN 'G'  
		                                WHEN Comp_Cat_ID= 4 THEN 'N'   
		                                WHEN Comp_Cat_ID= 5 THEN 'Z'  
	                                END  
                                 )
                                 ,Face_Value
                                 ,Market_Lot
                                 ,Share_Type
                                 ,ISNULL(Issuer_ID,0) AS Issuer_ID
                                 ,ISIN_No 
                                 FROM SBP_Database..SBP_Company  ";
            try
            {
                //_dbConnection.ConnectDatabase();
                dr = _dbConnection.ExecuteReader(queryString);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
            return dr;
        }

        public void Insert_CCS_Company_UITransApplied(SqlDataReader dr)
        {
            int i = 0;
            string queryString = string.Empty;
            CommonBAL combal = new CommonBAL();
            try
            {
                while (dr.Read())
                {
                    if (i == 10)
                    {
                        string kamal = "kamal";
                    }
                    
                   queryString = @"INSERT INTO dbksclCallCenter..tbl_company_all(code_no,name,Company_Short_Code,mkt_group,face_val,mkt_lot,type,issuer_id,isin)   
                                VALUES(
                                    '" + combal.HandlingSingelQuation(dr["Code_No"].ToString()) + @"'
                                    ,'" + combal.HandlingSingelQuation(dr["Comp_Name"].ToString()) + @"'
                                    ,'" + combal.HandlingSingelQuation(dr["Comp_Short_Code"].ToString()) + @"'
                                    ,'" + dr["Comp_Cat_ID"] + @"'
                                    ," + dr["Face_Value"] + @"
                                    ," + dr["Market_Lot"] + @"
                                    ,'" + dr["Share_Type"] + @"'
                                    ," + dr["Issuer_ID"]+ @"
                                    ,'" + dr["ISIN_No"] + @"'
                                )";
                    _dbConnection.ExecuteNonQuery_SMSSender(queryString);
                    i++;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                dr.Close();
                //_dbConnection.CloseDatabase();
            }
        }

        public SqlDataReader Get_CCS_ShareDW_UITransApplied()
        {
            SqlDataReader dr;
            string queryString = @" SELECT  
                                 Cust_Code
                                 ,Comp_Short_Code
                                 ,ISNULL(Quantity,0) AS Quantity
                                 ,Deposit_Withdraw
                                 ,Received_Date
                                 ,Vouchar_No
                                 ,ISNULL(No_Script,0) AS No_Script
                                 ,Share_Type
                                 ,Entry_Date 
                                 FROM SBP_Database..SBP_Share_DW 
                                 WHERE Cust_Code IS NOT NULL
                                 AND Cust_Code>=100";
            try
            {
                //_dbConnection.ConnectDatabase();
                dr = _dbConnection.ExecuteReader(queryString);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
            return dr;
        }

        public void Insert_CCS_ShareDW_UITransApplied(SqlDataReader dr)
        {
            int i = 0;
            string queryString = string.Empty;
            CommonBAL combal = new CommonBAL();
            try
            {
                
                while (dr.Read())
                {
                    queryString = @"INSERT INTO dbksclCallCenter..tbl_Share_Draw_Withdraw_All(Cust_Code,Company_Short_Code,quantity,Draw_Withdraw,Received_date,bill_no,no_script,Type,Entry_Date)     
                                VALUES(
                                    '" + combal.HandlingSingelQuation(dr["Cust_Code"].ToString()) + @"'
                                    ,'" + combal.HandlingSingelQuation(dr["Comp_Short_Code"].ToString()) + @"'
                                    ," + dr["Quantity"] + @"
                                    ,'" + dr["Deposit_Withdraw"] + @"'
                                    ,'" + dr["Received_Date"] + @"'
                                    ,'" + combal.HandlingSingelQuation(dr["Vouchar_No"].ToString()) + @"'
                                    ," + dr["No_Script"] + @"
                                    ,'" + dr["Share_Type"] + @"'
                                    ,'" + dr["Entry_Date"] + @"'
                                )";
                    _dbConnection.ExecuteNonQuery_SMSSender(queryString);
                    i++;
                }

            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                dr.Close();
                //_dbConnection.CloseDatabase();
            }
        }

        public SqlDataReader Get_CCS_Payment_UITransApplied()
        {
            SqlDataReader dr;
            
            string queryString = @"SELECT  
                                   CONVERT(NUMERIC,Cust_Code) AS Cust_Code,  
	                               ISNULL(Amount,0) AS  Amount,  
	                               Payment_Media ,  
	                               Received_Date ,          
	                               Payment_Media_No ,  
	                               Payment_Media_Date ,  
	                               Bank_Name ,  
	                               Received_By ,  
	                               Deposit_Withdraw ,  
	                               Voucher_Sl_No ,  
	                               Entry_Date,   
	                               'N/A' AS [N/A]
	                            FROM SBP_Database..SBP_Payment WHERE CONVERT(NUMERIC,Cust_Code)>0   ";
            try
            {
                //_dbConnection.ConnectDatabase();
                dr = _dbConnection.ExecuteReader(queryString);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
            return dr;
        }

        public void Insert_CCS_Payment_UITransApplied(SqlDataReader dr)
        {
            int i = 0;
            string queryString = string.Empty;
            CommonBAL combal = new CommonBAL();

            try
            {
                while (dr.Read())
                {
                    queryString = @"  INSERT INTO dbksclCallCenter..tbl_Cust_Paymnet_All  
	                                       ( Cust_Code ,  
		                                     amount ,  
		                                     Check_Cash ,  
		                                     Received_date ,  
		                                     check_no ,  
		                                     check_date ,  
		                                     bank_name ,  
		                                     received_by ,  
		                                     Deposit_withdraw ,  
		                                     SL_No ,  
		                                     Entry_Date ,  
		                                     tk_in_word  
	                                       )  
                                       VALUES(
                                           '" +combal.HandlingSingelQuation(dr["Cust_Code"].ToString()) + @"',  
                                           " + dr["Amount"] + @",  
                                           '" + combal.HandlingSingelQuation(dr["Payment_Media"].ToString()) + @"' ,  
                                           '" + dr["Received_Date"] + @"',          
                                           '" + combal.HandlingSingelQuation(dr["Payment_Media_No"].ToString()) + @"',  
                                           '" + dr["Payment_Media_Date"] + @"',  
                                           '" + combal.HandlingSingelQuation(dr["Bank_Name"].ToString()) + @"',  
                                           '" + dr["Received_By"] + @"',  
                                           '" + dr["Deposit_Withdraw"] + @"',  
                                           '" + combal.HandlingSingelQuation(dr["Voucher_Sl_No"].ToString()) + @"',  
                                           '" + dr["Entry_Date"] + @"',   
                                           '" + dr["N/A"] + @"' 
                                       )";
                    _dbConnection.ExecuteNonQuery_SMSSender(queryString);
                    i++;
                    if (!dr.NextResult())
                        break;
                }

            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                dr.Close();
                //_dbConnection.CloseDatabase();
            }
        }

        public SqlDataReader Get_CCS_ShareBalance_UITransApplied()
        {
            SqlDataReader dr;
            string queryString = @"SELECT  
                                   CONVERT(NUMERIC,Cust_Code) AS Cust_Code ,  
                                   Comp_Short_Code ,  
                                   SUM(ISNULL(Balance,0)) AS Balance ,  
                                   SUM(ISNULL(Matured_Balance,0)) AS Matured_Balance ,  
                                   ISNULL((SELECT Comp_Category FROM SBP_Comp_Category,dbo.SBP_Company WHERE dbo.SBP_Company.Comp_Cat_ID=dbo.SBP_Comp_Category.Comp_Cat_ID AND dbo.SBP_Company.Comp_Short_Code=dbo.SBP_Share_Balance_Temp.Comp_Short_Code),'') AS Comp_Category,  
                                   ISNULL((SELECT Share_Type FROM dbo.SBP_Company WHERE dbo.SBP_Company.Comp_Short_Code=dbo.SBP_Share_Balance_Temp.Comp_Short_Code),'') AS Share_Type
                                   FROM SBP_Database..SBP_Share_Balance_Temp  
                                   WHERE CONVERT(NUMERIC,Cust_Code)>0 AND Comp_Short_Code!=''  
                                   GROUP BY Cust_Code,Comp_Short_Code";
            try
            {
                //_dbConnection.ConnectDatabase();
                dr = _dbConnection.ExecuteReader(queryString);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
               
                //_dbConnection.CloseDatabase();
            }
            return dr;
        }

        public void Insert_CCS_ShareBalance_UITransApplied(SqlDataReader dr)
        {
            int i = 0;
            string queryString = string.Empty;
            CommonBAL combal = new CommonBAL();
            try
            {
                while (dr.Read())
                {
                    queryString = @"
                                       INSERT INTO dbksclCallCenter..tbl_Share_Balance  
                                           ( Cust_Code ,  
                                             Company_Short_Code ,  
                                             Balance ,  
                                             Matured_Balance ,  
                                             Mkt_group ,  
                                             Type  
                                           )  
                                       VALUES(
                                           '" + combal.HandlingSingelQuation(dr["Cust_Code"].ToString()) + @"',
                                           '" + combal.HandlingSingelQuation(dr["Comp_Short_Code"].ToString()) + @"',  
                                           " + dr["Balance"]+ @",  
                                           " + dr["Matured_Balance"] + @",  
                                           '" + dr["Comp_Category"] + @"',
                                           '" + dr["Share_Type"] + @"'
                                       )";
                    _dbConnection.ExecuteNonQuery_SMSSender(queryString);
                    i++;
                    if (!dr.NextResult())
                        break;
                }

            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                dr.Close();
                //_dbConnection.CloseDatabase();
            }
        }

        public SqlDataReader Get_CCS_ShareDetails_UITransApplied()
        {
            SqlDataReader dr;
            string queryString = @" 
                                SELECT  Cust_Code ,  
                                   Comp_Short_Code ,  
                                   ISNULL(Buy_Dep_Qty,0) AS Buy_Dep_Qty ,  
                                   ISNULL(Sell_Withdraw_Qty,0) AS Sell_Withdraw_Qty ,  
                                   ISNULL(Balance,0) AS Balance ,  
                                   Trade_Date ,                  
                                   ISNULL(Buy_Total,0) AS Buy_Total ,  
                                   ISNULL(Buy_Avg,0) AS Buy_Avg ,  
                                   ISNULL(Sell_Total,0) AS Sell_Total,  
                                   ISNULL(Sell_Avg,0) AS Sell_Avg,  
                                   Remarks 
                                FROM SBP_Database..SBP_Share_Balance_Temp
                                WHERE ISNULL(Cust_Code,'')<>''";
            try
            {
                //_dbConnection.ConnectDatabase();
                dr = _dbConnection.ExecuteReader(queryString);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
            return dr;
        }

        public void Insert_CCS_ShareDetails_UITransApplied(SqlDataReader dr)
        {
            int i = 0;
            string queryString = string.Empty;
            CommonBAL combal=new CommonBAL();
            try
            {
                while (dr.Read())
                {
                    queryString = @"
                                       INSERT INTO dbksclCallCenter..tbl_ShareTrade_Details  
                                       ( Cust_Code ,  
                                         Company_Short_Code ,  
                                         Buy_Deposit_Qty ,  
                                         Sell_WithDraw_Qty ,  
                                         Balance ,  
                                         Transaction_Date ,  
                                         Buy_Total ,  
                                         Buy_Avg ,  
                                         Sell_Total ,  
                                         Sell_Avg ,  
                                         Remarks  
                                       )  
                                       VALUES(
                                           '"+combal.HandlingSingelQuation(dr["Cust_Code"].ToString()) + @"' ,  
                                           '"+combal.HandlingSingelQuation(dr["Comp_Short_Code"].ToString()) + @"' ,  
                                           " +dr["Buy_Dep_Qty"] + @" ,  
                                           " +dr["Sell_Withdraw_Qty"] + @" ,  
                                           " +dr["Balance"] + @" ,  
                                           '"+dr["Trade_Date"] + @"' ,                  
                                           " +dr["Buy_Total"] + @" ,  
                                           " +dr["Buy_Avg"] + @" ,  
                                           " +dr["Sell_Total"] + @" ,  
                                           " +dr["Sell_Avg"] + @",  
                                           '"+ combal.HandlingSingelQuation( dr["Remarks"].ToString()) + @"'
                                      )";
                    _dbConnection.ExecuteNonQuery_SMSSender(queryString);
                    i++;
                    if (!dr.NextResult())
                        break;
                }

            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                dr.Close();
                //_dbConnection.CloseDatabase();
            }
        }

        #endregion

        public void TruncateTable_SMSSyncImportIPORequest_UITransApplied()
        {

            string query = string.Empty;
            query = "TRUNCATE TABLE SMS_Sync_Import_IPORequest";
            CommonBAL comBAL = new CommonBAL();
            //int slNo = comBAL.GenerateID("SBP_Upload_History", "Upload_ID");
            //queryStringUploadHistory = "INSERT INTO SBP_Upload_History(Upload_ID,File_Name,Upload_Date,Entry_By) SELECT TOP 1 " + slNo + ",'tradeFile',Date,'admin' FROM SBP_Transactions_Temp_FlexTrade";

            try
            {
                //_dbConnection.ConnectDatabase();
                //_dbConnection.StartTransaction();
                _dbConnection.ExecuteNonQuery(query);
                //_dbConnection.ExecuteNonQuery(queryStringUploadHistory);
                //_dbConnection.Commit();
            }
            catch (Exception exception)
            {
                //_dbConnection.Rollback();
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
        }

        public void InsertTable_SMSSyncImportIPORequest_UITransApplied(DataTable dt)
        {

            List<string> InsertQueryList = new List<string>();
            IPOProcessBAL ipoBal = new IPOProcessBAL();
            DataTable dt_RefundType = ipoBal.GetRefundMethod();

            int i = 1;
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    //if (i == 27)
                    //{
                    //    string kamal = "Kamal";
                    //}

                    string temp = @"INSERT INTO 
                    [SBP_Database].[dbo].[SMS_Sync_Import_IPORequest]
                    (
                     [Cust_Code]
                    , [RegisteredCode]
                    , [Money_TransactionType_Name]
                    , [Money_TransactionType_Name_ID]
                    , [Deposit_Withdraw]
                    , [Amount]
                    , [TransferCode]
                    , [IPOSessionID]
                    , [LotNo]
                    , [Refund_Name]
                    , [RefundType_ID]
                    , [SMSReqID]
                    , [SMSReceiveID]
                    , [Cheque_Number]
                    , [Routing_Number]
                    , [Payment_Media]
                    , [ApplicationType]
                    , [Remarks]
                    , [Received_Date]
                    , [Media_Type]
                    )
                    VALUES (
                        '" + Convert.ToString(dr["Cust_Code"]) + @"'
                        ,'" + Convert.ToString(dr["RegisteredCode"]) + @"'
                        ,'" + Convert.ToString(dr["Money_TransactionType_Name"]) + @"'
                        ,'" + Convert.ToString(dr["Money_TransactionType_ID"]) + @"'
                        ,'" + Convert.ToString(dr["Deposit_Withdraw"]) + @"'
                        ,'" + Convert.ToString(dr["Amount"]) + @"'
                        ,'" + Convert.ToString(dr["TransferCode"]) + @"'
                        ,'" + Convert.ToString(dr["IPOSessionID"]) + @"'
                        ,'" + Convert.ToString(dr["LotNo"]) + @"'
                        ,'" + Convert.ToString(dr["Refund_Name"]) + @"'
                        ,'" + Convert.ToString(dr["RefundType_ID"]) + @"'
                        ,'" + Convert.ToString(dr["SMSReqID"]) + @"'
                        ,'" + Convert.ToString(dr["SMSReceiveID"]) + @"'
                        , '" + Convert.ToString(dr["Cheque_Number"]) + @"'
                        ,'" + Convert.ToString(dr["Routing_Number"]) + @"'
                        ,'" + Convert.ToString(dr["Payment_Media"]) + @"'
                        ,'" + Convert.ToString(dr["ApplicationType"]) + @"'
                        ,'" + Convert.ToString(dr["Remarks"]) + @"'
                        ,'" + Convert.ToString(dr["Received_Date"]) + @"'  
                        ,'" + Convert.ToString(dr["Media Type"]) + @"'   
                    )";
                    _dbConnection.ExecuteNonQuery(temp);
                    //InsertQueryList.Add(temp);
                    i++;
                }
            }
            catch (Exception ex)
            {
                //string kamal = "kamal";
                throw new Exception(ex.Message);
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
        }

        public DataTable GetProcessedIPORequest_UITransApplied()
        {
            DataTable dt = new DataTable();
            string query = string.Empty;
            query = @"  SELECT Distinct T.ChannelID
                        FROM (
                            SELECT [ChannelID] AS ChannelID
                            FROM [SBP_Database].[dbo].[SBP_IPO_Customer_Broker_MoneyTransaction]
                            WHERE [Channel]='" + Indication_IPOPaymentTransaction.ChannelType_SMS + @"'                            
                            UNION ALL
                            SELECT [SMSReqID] AS ChannelID
                            FROM [SBP_Database].[dbo].[SMS_Sync_Import_IPORequest_DeleteLog] Where Media_Type='" + Indication_IPOPaymentTransaction.ChannelType_SMS + @"'
                            UNION ALL
                            Select bs.ChannelID AS ChannelID
                            FROM SBP_IPO_Application_BasicInfo as bs
                            JOIN SBP_IPO_Application_ExtendedInfo as ext
                            ON bs.ID=ext.BasicInfo_ID
                            WHERE  bs.ChannelType='" + Indication_IPOPaymentTransaction.ChannelType_SMS+ @"'
                            AND ISNULL(bs.ChannelID,'')<>''
                        ) AS T";
            try
            {
                //_dbConnection.ConnectDatabase();
                dt = _dbConnection.ExecuteQuery(query);

            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
            return dt;
        }
        public DataTable GetProcessedIPORequest_ForWeb_UITransApplied()
        {
            DataTable dt = new DataTable();
            string query = string.Empty;
            query = @"  SELECT Distinct T.ChannelID
                        FROM (
                                SELECT [ChannelID] AS ChannelID
                                FROM [SBP_Database].[dbo].[SBP_IPO_Customer_Broker_MoneyTransaction]
                                WHERE [Channel]='" + Indication_IPOPaymentTransaction.ChannelType_Web + @"'                            
                                UNION ALL
                                SELECT [SMSReqID] AS ChannelID
                                FROM [SBP_Database].[dbo].[SMS_Sync_Import_IPORequest_DeleteLog] Where Media_Type='" + Indication_IPOPaymentTransaction.ChannelType_Web + @"'
                                UNION ALL
                                Select bs.ChannelID AS ChannelID
                                FROM SBP_IPO_Application_BasicInfo as bs
                                JOIN SBP_IPO_Application_ExtendedInfo as ext
                                ON bs.ID=ext.BasicInfo_ID
                                WHERE  bs.ChannelType='" + Indication_IPOPaymentTransaction.ChannelType_Web + @"'
                                AND ISNULL(bs.ChannelID,'')<>''
                        ) AS T";
            try
            {
                //_dbConnection.ConnectDatabase();
                dt = _dbConnection.ExecuteQuery(query);

            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
            return dt;
        }
        public DataTable GetProcessedIPORequest_UITransApplied_Email()
        {
            DataTable dt = new DataTable();
            string query = string.Empty;
            query = @"  SELECT Distinct T.ChannelID
                        FROM (
                            SELECT [ChannelID] AS ChannelID
                            FROM [SBP_Database].[dbo].[SBP_IPO_Customer_Broker_MoneyTransaction]
                            WHERE [Channel]='" + Indication_IPOPaymentTransaction.ChannelType_Email + @"'                            
                            UNION ALL
                            SELECT [SMSReqID] AS ChannelID
                            FROM [SBP_Database].[dbo].[SMS_Sync_Import_IPORequest_DeleteLog] Where Media_Type='" + Indication_IPOPaymentTransaction.ChannelType_Email + @"'
                            UNION ALL
                            Select bs.ChannelID AS ChannelID
                            FROM SBP_IPO_Application_BasicInfo as bs
                            JOIN SBP_IPO_Application_ExtendedInfo as ext
                            ON bs.ID=ext.BasicInfo_ID
                            WHERE bs.ChannelType='" + Indication_IPOPaymentTransaction.ChannelType_Email+ @"'
                            AND ISNULL(bs.ChannelID,'')<>''
                        ) AS T";
            try
            {
                //_dbConnection.ConnectDatabase();
                dt = _dbConnection.ExecuteQuery(query);

            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
            return dt;
        }



        public DataTable GetProcessedDeposit_Withdraw_Request_SMS()
        {
            DataTable dt = new DataTable();
            try
            {                
                string Query = @"SELECT Distinct T.ChannelID
                        FROM (
                            SELECT [ChannelID] AS ChannelID
                            FROM [SBP_Database].[dbo].[SBP_IPO_Customer_Broker_MoneyTransaction]
                            WHERE [Channel]='"+Indication_IPOPaymentTransaction.ChannelType_SMS + @"' AND Intended_IPOSession_ID=0
                            UNION ALL
                            SELECT [SMSReqID] AS ChannelID
                            FROM [SBP_Database].[dbo].[SMS_Sync_Import_IPORequest_DeleteLog] Where Media_Type='"+Indication_IPOPaymentTransaction.ChannelType_SMS+ @"'                           
                        ) AS T
                        ";
                dt = _dbConnection.ExecuteQuery(Query);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
            finally
            {

            }
            return dt;
        }

        public DataTable GetProcessedDeposit_Withdraw_Request_Email()
        {
            DataTable dt = new DataTable();
            try
            {
                string Query = @"SELECT Distinct T.ChannelID
                        FROM (
                            SELECT [ChannelID] AS ChannelID
                            FROM [SBP_Database].[dbo].[SBP_IPO_Customer_Broker_MoneyTransaction]
                            WHERE [Channel]='" + Indication_IPOPaymentTransaction.ChannelType_Email + @"' AND Intended_IPOSession_ID=0
                            UNION ALL
                            SELECT [SMSReqID] AS ChannelID
                            FROM [SBP_Database].[dbo].[SMS_Sync_Import_IPORequest_DeleteLog] Where Media_Type='" + Indication_IPOPaymentTransaction.ChannelType_Email + @"'                           
                        ) AS T
                        ";
                dt = _dbConnection.ExecuteQuery(Query);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
            finally
            {

            }
            return dt;
        }

        public DataTable GetProcessed_Trade_Deposit_Withdraw_Request_Email_SMS()
        {
            DataTable dt = new DataTable();
            try
            {
                string Query = @"Select OnlineOrderNo from SBP_Payment_Posting_Request 
                                                     Where  Channel='"+Indication_IPOPaymentTransaction.ChannelType_SMS+ @"' 
                                                     OR Channel='" + Indication_IPOPaymentTransaction.ChannelType_SMS + "'";
                dt = _dbConnection.ExecuteQuery(Query);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
            finally
            {

            }
            return dt;
        }
        public DataTable GetNewIPORequest_UITransApplied(DataTable Dt,DataTable dt_Email,DataTable dt_Deposite,DataTable dt_Deposit_Email)
        {
            DataTable ResultDT = new DataTable();
            DataTable dt_GotTrade_Deposit_Email_SMS = new DataTable();
            dt_GotTrade_Deposit_Email_SMS = GetProcessed_Trade_Deposit_Withdraw_Request_Email_SMS();


            string query_CreateTemp = @"CREATE TABLE #ProcessedRequest(
                            Inserted_ID INT,
            );
            ";
            string query_CreateTemp_Email = @"CREATE TABLE #ProcessedRequestEmail(
                            Inserted_Email_ID INT,
            );
            ";

            string query_CreateTemp_SMS_Deposite = @"CREATE TABLE #ProcessedRequestSMS_Deposite(
                            Inserted_SMS_Deposite_ID INT,
            );
            ";

            string query_CreateTemp_Email_Deposite = @"CREATE TABLE #ProcessedRequestEmail_Deposite(
                            Inserted_Email_Deposite_ID INT,
            );
            ";
            string query_CreateTemp_Trade_Deposit_Email_Sms = @"CREATE TABLE #ProcessedRequestEmail_Deposite_Trade(
                            Inserted_Email_SMS_Deposite_Trade_ID INT,
            );
            ";


            List<string> query_ListInsertTemp = new List<string>();
            List<string> query_ListInsertTemp_Email = new List<string>();

            string insertQuery = string.Empty;
            string insertQuery_Email = string.Empty;
            string insertQuery_Deposite = string.Empty;
            string insertQuery_Deposite_Email = string.Empty;
            string insertQuery_Trade_Deposit_Email = string.Empty;
            foreach (DataRow dr in Dt.Rows)
            {
                string queryTmp_InsertTemp = @"INSERT INTO #ProcessedRequest(Inserted_ID) VALUES(" + Convert.ToString(dr["ChannelID"]) + @")
                ";
                insertQuery = insertQuery + queryTmp_InsertTemp;
                //query_ListInsertTemp.Add(queryTmp_InsertTemp);
            }

            foreach (DataRow dr in dt_Email.Rows)
            {
                string queryTmp_InsertTemp_Email = @"INSERT INTO #ProcessedRequestEmail(Inserted_Email_ID) VALUES(" + Convert.ToString(dr["ChannelID"]) + @")
                ";
                insertQuery_Email = insertQuery_Email + queryTmp_InsertTemp_Email;
                //query_ListInsertTemp.Add(queryTmp_InsertTemp);
            }

            foreach (DataRow dr in dt_Deposite.Rows)
            {
                string queryTmp_InsertTemp_Email = @"INSERT INTO #ProcessedRequestSMS_Deposite(Inserted_SMS_Deposite_ID) VALUES(" + Convert.ToString(dr["ChannelID"]) + @")
                ";
                insertQuery_Deposite = insertQuery_Deposite + queryTmp_InsertTemp_Email;
                //query_ListInsertTemp.Add(queryTmp_InsertTemp);
            }

            foreach (DataRow dr in dt_Deposit_Email.Rows)
            {
                string queryTmp_InsertTemp_Email = @"INSERT INTO #ProcessedRequestEmail_Deposite(Inserted_Email_Deposite_ID) VALUES(" + Convert.ToString(dr["ChannelID"]) + @")
                ";
                insertQuery_Deposite_Email = insertQuery_Deposite_Email + queryTmp_InsertTemp_Email;
                //query_ListInsertTemp.Add(queryTmp_InsertTemp);
            }
            foreach (DataRow dr in dt_GotTrade_Deposit_Email_SMS.Rows)
            {
                string queryTmp_InsertTemp_Email = @"INSERT INTO #ProcessedRequestEmail_Deposite_Trade(Inserted_Email_SMS_Deposite_Trade_ID) VALUES(" + Convert.ToString(dr["OnlineOrderNo"]) + @")
                ";
                insertQuery_Trade_Deposit_Email = insertQuery_Trade_Deposit_Email + queryTmp_InsertTemp_Email;
                //query_ListInsertTemp.Add(queryTmp_InsertTemp);
            }





            string query_GetNewRequest = @"SELECT 
                                        [Cust_Code]
                                        ,[ReferenceNumber] AS RegisteredCode
                                        , [CompanyShortName]
                                        ,
                                        (
                                            CASE req.PaymentType
                                                WHEN 'ptrade' THEN 'TRTA'
                                                WHEN 'pmipo' THEN 'TRIPO'
                                                ELSE ''		
                                            END	
                                        ) AS Money_TransactionType_Name
                                        ,(
                                            CASE req.PaymentType
                                                WHEN 'ptrade' THEN " + Indication_IPOPaymentTransaction.TRTA_ID + @"
                                                WHEN 'pmipo' THEN " + Indication_IPOPaymentTransaction.TRIPO_ID + @"
                                                ELSE ''		
                                            END	
                                        ) AS Money_TransactionType_ID
                                        ,'Deposit' AS Deposit_Withdraw
                                        ,(
                                             ISNULL((
                                                SElect TotalAmount from SBP_Database.dbo.SBP_IPO_Session   as t Where t.IPOSession_Name= req.CompanyShortName
                                             ),0)
                                             +
                                             ISNULL((
                                                     SELECT SUM(t.[Ch_Rate]) FROM [dbksclCallCenter].[dbo].[tbl_IPO_Charge] as t 
                                                    --WHERE t.Ch_Item='" + Indication_IPOPaymentTransaction.IPOApp_Charge + @"'  
                                             ),0)
                                         ) AS Amount
                                        ,(
                                            CASE req.PaymentType
                                                WHEN 'ptrade' THEN Cust_Code
                                                WHEN 'pmipo' THEN ReferenceNumber
                                                ELSE ''
                                            END		
                                        )AS TransferCode
                                        ,(
                                            SElect ID from SBP_Database.dbo.SBP_IPO_Session as t Where t.IPOSession_Name=req.CompanyShortName
                                        )AS IPOSessionID
                                        ,0 AS LotNo
                                        , (
                                            CASE req.RefundType
                                                WHEN 'ripo' THEN 'TRIPO'
                                                WHEN 'rtrade' THEN 'TRTA'
                                                WHEN 'rmipo' THEN 'TRPRIPO'
                                                ELSE ''
                                            END
                                        )AS Refund_Name
                                        , (
                                            CASE req.RefundType
                                                WHEN 'ripo' THEN " + Indication_IPORefundType.Refund_TRIPO_ID + @"
                                                WHEN 'rtrade' THEN " + Indication_IPORefundType.Refund_TRTA_ID + @"
                                                WHEN 'rmipo' THEN " + Indication_IPORefundType.Refund_TRPRIPO_ID + @"
                                                ELSE ''
                                            END
                                        )AS RefundType_ID
                                        ,req.[ID] as SMSReqID
                                        ,req.[ReceiveID] AS SMSReceiveID
                                        , (
                                            CASE 
                                                WHEN req.PaymentType='ptrade' AND ISNULL(req.CompanyShortName,'')<>'' THEN 'ApplyTogether'
                                                 WHEN req.PaymentType='pmipo' AND ISNULL(req.CompanyShortName,'')<>'' THEN 'ApplyTogether' 
                                                WHEN req.PaymentType='pipo' AND ISNULL(req.CompanyShortName,'')<>'' THEN 'SingleApplication'
                                                ELSE '' 
                                            END
                                        )AS ApplicationType 
                                        , [Remarks]
                                        ,'' AS 'Cheque_Number'
                                        ,'' AS 'Routing_Number'
                                        , [DataTime] AS [Received_Date]
                                        ,'" + Indication_IPOPaymentTransaction.ChannelType_SMS+ @"' AS 'Media Type'
                                        ,'' as Payment_Media
                                        FROM [dbksclCallCenter].[dbo].[tbl_IPO_Request] as req
                                        WHERE 
                                        req.PaymentType IN ('ptrade','pmipo','pipo')                                                                
                                        AND [ID] NOT IN (
			                                            SELECT t.Inserted_ID FROM #ProcessedRequest as t 
                                        )
                                       UNION ALL
                                       SELECT 
                                        [Cust_Code]
                                        ,[ReferenceNumber] AS RegisteredCode
                                        , [CompanyShortName]
                                        ,
                                        (
                                            CASE req.PaymentType
                                                WHEN 'ptrade' THEN 'TRTA'
                                                WHEN 'pmipo' THEN 'TRIPO'
                                                ELSE ''		
                                            END	
                                        ) AS Money_TransactionType_Name
                                        ,(
                                            CASE req.PaymentType
                                                WHEN 'ptrade' THEN " + Indication_IPOPaymentTransaction.TRTA_ID + @"
                                                WHEN 'pmipo' THEN " + Indication_IPOPaymentTransaction.TRIPO_ID + @"
                                                ELSE ''		
                                            END	
                                        ) AS Money_TransactionType_ID
                                        ,'Deposit' AS Deposit_Withdraw
                                        ,(
                                             ISNULL((
                                                SElect TotalAmount from SBP_Database.dbo.SBP_IPO_Session   as t Where t.IPOSession_Name= req.CompanyShortName
                                             ),0)
                                             +
                                             ISNULL((
                                                     SELECT SUM(t.[Ch_Rate]) FROM [dbksclCallCenter].[dbo].[tbl_IPO_Charge] as t 
                                                    --WHERE t.Ch_Item='" + Indication_IPOPaymentTransaction.IPOApp_Charge + @"'  
                                             ),0)
                                         ) AS Amount
                                        ,(
                                            CASE req.PaymentType
                                                WHEN 'ptrade' THEN Cust_Code
                                                WHEN 'pmipo' THEN ReferenceNumber
                                                ELSE ''
                                            END		
                                        )AS TransferCode
                                        ,(
                                            SElect ID from SBP_Database.dbo.SBP_IPO_Session as t Where t.IPOSession_Name=req.CompanyShortName
                                        )AS IPOSessionID
                                        ,0 AS LotNo
                                        , (
                                            CASE req.RefundType
                                                WHEN 'ripo' THEN 'TRIPO'
                                                WHEN 'rtrade' THEN 'TRTA'
                                                WHEN 'rmipo' THEN 'TRPRIPO'
                                                ELSE ''
                                            END
                                        )AS Refund_Name
                                        , (
                                            CASE req.RefundType
                                                WHEN 'ripo' THEN " + Indication_IPORefundType.Refund_TRIPO_ID + @"
                                                WHEN 'rtrade' THEN " + Indication_IPORefundType.Refund_TRTA_ID + @"
                                                WHEN 'rmipo' THEN " + Indication_IPORefundType.Refund_TRPRIPO_ID + @"
                                                ELSE ''
                                            END
                                        )AS RefundType_ID
                                        ,req.[ID] as SMSReqID
                                        ,req.[ReceiveID] AS SMSReceiveID
                                        , (
                                            CASE 
                                                WHEN req.PaymentType='ptrade' AND ISNULL(req.CompanyShortName,'')<>'' THEN 'ApplyTogether'
                                                 WHEN req.PaymentType='pmipo' AND ISNULL(req.CompanyShortName,'')<>'' THEN 'ApplyTogether' 
                                                WHEN req.PaymentType='pipo' AND ISNULL(req.CompanyShortName,'')<>'' THEN 'SingleApplication'
                                                ELSE '' 
                                            END
                                        )AS ApplicationType 
                                        , [Remarks]
                                        ,'' AS 'Cheque_Number'
                                        ,'' AS 'Routing_Number'
                                        , [DataTime] AS [Received_Date]
                                        ,'" + Indication_IPOPaymentTransaction.ChannelType_Email+ @"' AS 'Media Type'
                                        ,'' as Payment_Media
                                        FROM [dbksclCallCenter].[dbo].[tbl_Email_Request] as req
                                        WHERE  req.CompanyShortName is not null
                                        AND  req.PaymentType IN ('ptrade','pmipo','pipo')                                                                
                                        AND [ID] NOT IN (
			                                            SELECT t.Inserted_Email_ID FROM #ProcessedRequestEmail as t 
                                        )
                                      UNION ALL
                                         Select Cust_Code
                                               ,Reg_Cust_Code AS 'RegisteredCode' 
                                               ,'' AS 'CompanyShortName'
                                               ,PaymentType AS 'Money_TransactionType_Name'
                                               ,(Select ID from SBP_Database.dbo.SBP_IPO_MoneyTrans_Type Where MoneyTransType_Name=PaymentType) AS 'Money_TransactionType_ID'
                                               ,Deposite_Withdraw AS 'Deposit_Withdraw'
                                               ,Amount
                                               ,Reg_Cust_Code AS 'TransferCode'
                                               ,'' AS 'IPOSessionID'
                                               ,'' AS 'LotNo'
                                               ,'' AS 'Refund_Name'
                                               ,'' AS 'RefundType_ID'
                                               ,ID AS 'SMSRegID'
                                               ,ReceiveID AS 'SMSReceiveID'
                                               ,'Free Transaction' AS 'ApplicationType'
                                               ,'' AS 'Remark'
                                               ,Cheque_No AS 'Cheque_Number'
                                               ,Routing_No AS 'Routing_Number'
                                               ,DateTime AS 'Received_Date'
                                               ,Media_Type AS 'Media Type'
                                               ,Payment_Media
                                               from [dbksclCallCenter].dbo.tbl_SMS_Deposite_Withdraw
                                               Where --Deposite_Withdraw='Deposit'
                                               [ID] NOT IN(
                                                            Select Inserted_SMS_Deposite_ID from #ProcessedRequestSMS_Deposite
                                                            )
                                               AND [ID] NOT IN(
                                                            Select Inserted_Email_SMS_Deposite_Trade_ID from #ProcessedRequestEmail_Deposite_Trade
                                                            )
                                        
                                        ";
            string DropTemp = "DROP TABLE #ProcessedRequest";

            string finalQuery = query_CreateTemp + query_CreateTemp_Email + query_CreateTemp_SMS_Deposite + query_CreateTemp_Trade_Deposit_Email_Sms + query_CreateTemp_Email_Deposite + insertQuery + insertQuery_Email + insertQuery_Deposite + insertQuery_Deposite_Email + insertQuery_Trade_Deposit_Email + query_GetNewRequest;

            try
            {
                //_dbConnection.ConnectDatabase_SMSSender();
                //_dbConnection.StartTransaction_SMSSender();
                //_dbConnection.ExecuteNonQuery_SMSSender(query_CreateTemp);
                //foreach (string data in query_ListInsertTemp)
                //    _dbConnection.ExecuteQuery_SMSSender(data);
                //ResultDT = _dbConnection.ExecuteQuery_SMSSender(query_GetNewRequest);
                //_dbConnection.ExecuteNonQuery_SMSSender(DropTemp);
                ResultDT = _dbConnection.ExecuteQuery_SMSSender(finalQuery);

                //_dbConnection.Commit_SMSSender();
            }
            catch (Exception exception)
            {
                //_dbConnection.Rollback_SMSSender();
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase_SMSSender();
            }

            return ResultDT;
        }

        public DataTable GetNewIPORequest_FroWeb_UITransApplied(DataTable Dt)
        {
            DataTable ResultDT = new DataTable();
            string query_CreateTemp = @"
            CREATE TABLE #ProcessedRequest(
                            Inserted_ID INT,
            );
            ";

            List<string> query_ListInsertTemp = new List<string>();
           
            string insertQuery = string.Empty;
            string insertQuery_Email = string.Empty;
            foreach (DataRow dr in Dt.Rows)
            {
                string queryTmp_InsertTemp = @"INSERT INTO #ProcessedRequest(Inserted_ID) VALUES(" + Convert.ToString(dr["ChannelID"]) + @")
                ";
                insertQuery = insertQuery + queryTmp_InsertTemp;
                //query_ListInsertTemp.Add(queryTmp_InsertTemp);
            }

            string query_GetNewRequest = @"SELECT 
                                        [Cust_Code]
                                        ,[ReferenceNumber] AS RegisteredCode
                                        , [CompanyShortName]
                                        ,
                                        (
                                            CASE LOWER(req.PaymentType)
                                                WHEN 'ptrade' THEN 'TRTA'
                                                WHEN 'pmipo' THEN 'TRIPO'
                                                WHEN 'peft' THEN 'EFT'
                                                ELSE ''		
                                            END	
                                        ) AS Money_TransactionType_Name
                                        ,(
                                            CASE LOWER(req.PaymentType)
                                                WHEN 'ptrade' THEN " + Indication_IPOPaymentTransaction.TRTA_ID + @"
                                                WHEN 'pmipo' THEN " + Indication_IPOPaymentTransaction.TRIPO_ID + @"
                                                WHEN 'peft' THEN " + Indication_IPOPaymentTransaction.EFT_ID + @"
                                                ELSE ''		
                                            END	
                                        ) AS Money_TransactionType_ID
                                        ,(
                                            CASE 
                                                    WHEN ISNULL([App_With_IPO_MoneyTransfer_Request_ID],0)<>0 THEN '"+Indication_PaymentMode.Deposit+ @"'
                                                    WHEN ISNULL([Trade_With_IPO_MoneyTransfer_Request_ID],0)<>0 AND ISNULL([Child_Dep_IPO_MoneyTransfer_Request_ID],0)<>0  THEN '"+Indication_PaymentMode.Deposit+@"'
                                                    WHEN ISNULL(FreeTrns_IPO_MoneyTransfer_Request_ID,0)<>0 THEN req.[FreeTrns_Deposit_Withdraw]

                                            END
                                        ) AS Deposit_Withdraw
                                        ,(
                                            CASE 
                                                    WHEN ISNULL([App_With_IPO_MoneyTransfer_Request_ID],0)<>0 THEN 
                                                        ISNULL((
                                                            Select t.TotalAmount From [dbksclCallCenter].[dbo].[tbl_IPO_SessionforCompanyInfo] as t Where t.Company_Short_Code=req.CompanyShortName
                                                         ),0)
                                                         +
                                                         ISNULL((
                                                                 SELECT SUM(t.[Ch_Rate]) FROM [dbksclCallCenter].[dbo].[tbl_IPO_Charge] as t 
                                                                --WHERE t.Ch_Item='" + Indication_IPOPaymentTransaction.IPOApp_Charge + @"'  
                                                         ),0)
                                                    WHEN ISNULL(Trade_With_IPO_MoneyTransfer_Request_ID,0)<>0 AND ISNULL(Child_Dep_IPO_MoneyTransfer_Request_ID,0)<>0 THEN 
                                                        req.Child_Dep_Amount
                                                    WHEN ISNULL(FreeTrns_IPO_MoneyTransfer_Request_ID,0)<>0 THEN 
                                                        req.[FreeTrns_Amount]
                                            END
                                         ) AS Amount
                                        ,( CASE 
                                                    WHEN ISNULL([App_With_IPO_MoneyTransfer_Request_ID],0)<>0 THEN
                                                        CASE req.PaymentType
                                                            WHEN 'ptrade' THEN Cust_Code
                                                            WHEN 'pmipo' THEN ReferenceNumber                                                            
                                                            ELSE ''
                                                        END	
                                                   WHEN ISNULL([FreeTrns_IPO_MoneyTransfer_Request_ID],0)<>0 THEN
                                                        CASE req.PaymentType
                                                            WHEN 'ptrade' THEN Cust_Code
                                                            WHEN 'pmipo' THEN ReferenceNumber
                                                            ELSE ''
                                                        END
                                                  WHEN ISNULL(Trade_With_IPO_MoneyTransfer_Request_ID,0)<>0 AND ISNULL(Child_Dep_IPO_MoneyTransfer_Request_ID,0)<>0 THEN
                                                        CASE req.PaymentType
                                                            WHEN 'ptrade' THEN Cust_Code                                                           
                                                            ELSE ''
                                                   END	 	
                                            END
                                        )AS TransferCode
                                        ,(
                                            Select t.IPO_SessionID From [dbksclCallCenter].[dbo].[tbl_IPO_SessionforCompanyInfo] as t Where t.Company_Short_Code=req.CompanyShortName
                                        )AS IPOSessionID
                                        ,0 AS LotNo
                                        , ( 
                                            CASE 
                                                    WHEN ISNULL([App_With_IPO_MoneyTransfer_Request_ID],0)<>0 THEN
                                                        CASE LOWER(req.RefundType)
                                                            WHEN 'ripo' THEN 'TRIPO'
                                                            WHEN 'rtrade' THEN 'TRTA'
                                                            WHEN 'rmipo' THEN 'TRPRIPO'
                                                            WHEN 'reft' THEN 'EFT'
                                                            ELSE ''
                                                        END
                                                    ELSE ''
                                            END
                                        )AS Refund_Name
                                        ,( 
                                            CASE 
                                                    WHEN ISNULL([App_With_IPO_MoneyTransfer_Request_ID],0)<>0 THEN
                                                       CASE LOWER(req.RefundType)
                                                            WHEN 'ripo' THEN " + Indication_IPORefundType.Refund_TRIPO_ID + @"
                                                            WHEN 'rtrade' THEN " + Indication_IPORefundType.Refund_TRTA_ID + @"
                                                            WHEN 'rmipo' THEN " + Indication_IPORefundType.Refund_TRPRIPO_ID + @"
                                                            WHEN 'reft' THEN " + Indication_IPORefundType.Refund_EFT_ID+ @"
                                                            ELSE ''
                                                        END
                                                    ELSE ''
                                            END
                                        )AS RefundType_ID
                                        ,req.[ID] as SMSReqID
                                        ,req.[ReceiveID] AS SMSReceiveID
                                        ,(
                                            CASE 
                                                 WHEN ISNULL([App_With_IPO_MoneyTransfer_Request_ID],0)<>0 AND req.PaymentType='ptrade' AND ISNULL(req.CompanyShortName,'')<>'' THEN 'ApplyTogether'
                                                 WHEN ISNULL([App_With_IPO_MoneyTransfer_Request_ID],0)<>0 AND req.PaymentType='pmipo' AND ISNULL(req.CompanyShortName,'')<>'' THEN 'ApplyTogether' 
                                                 WHEN ISNULL([App_With_IPO_MoneyTransfer_Request_ID],0)<>0 AND req.PaymentType='pipo' AND ISNULL(req.CompanyShortName,'')<>'' THEN 'SingleApplication'
                                                 WHEN ISNULL(FreeTrns_IPO_MoneyTransfer_Request_ID,0)<>0 THEN 'FreeTransaction'
                                                 WHEN ISNULL(Trade_With_IPO_MoneyTransfer_Request_ID,0)<>0 AND ISNULL(Child_Dep_IPO_MoneyTransfer_Request_ID,0)<>0 THEN 'FreeTransaction'              
                                                ELSE '' 
                                            END
                                        )AS ApplicationType 
                                        , [Remarks]
                                        ,'' AS 'Cheque_Number'
                                        ,'' AS 'Routing_Number'                                        
                                        , [DateTime] AS [Received_Date]
                                        ,'' as Payment_Media
                                        ,'" + Indication_IPOPaymentTransaction.ChannelType_Web + @"' AS 'Media Type'
                                        FROM [dbksclCallCenter].[dbo].[tbl_Web_Request] as req
                                        WHERE 
                                        LOWER(req.PaymentType) IN ('ptrade','pmipo','pipo','peft')                                                                
                                        AND [ID] NOT IN (
			                                            SELECT t.Inserted_ID FROM #ProcessedRequest as t 
                                        )
                                        ";
            string DropTemp = @"IF OBJECT_ID('tempdb..#ProcessedRequest') IS NOT NULL
                                BEGIN
                                        DROP TABLE #ProcessedRequest
                                END";

            string finalQuery =DropTemp+ query_CreateTemp + insertQuery + query_GetNewRequest ;

            try
            {
                //_dbConnection.ConnectDatabase_SMSSender();
                //_dbConnection.StartTransaction_SMSSender();
                //_dbConnection.ExecuteNonQuery_SMSSender(query_CreateTemp);
                //foreach (string data in query_ListInsertTemp)
                //    _dbConnection.ExecuteQuery_SMSSender(data);
                //ResultDT = _dbConnection.ExecuteQuery_SMSSender(query_GetNewRequest);
                //_dbConnection.ExecuteNonQuery_SMSSender(DropTemp);
                ResultDT = _dbConnection.ExecuteQuery_SMSSender(finalQuery);

                //_dbConnection.Commit_SMSSender();
            }
            catch (Exception exception)
            {
                //_dbConnection.Rollback_SMSSender();
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase_SMSSender();
            }

            return ResultDT;
        }

        public SqlDataReader GetIPO_SessionforCompanyInfo_UITransApplied()
        {
            SqlDataReader dr;
            string query = string.Empty;
            query = @"  
                    SELECT 
                      [Company_Short_Code] AS Company_Short_Code
                    , [Company_Name] AS Company_Name
                    , [Company_Address] AS Company_Address
                    , sess.[ID] AS [IPO_SessionID]
                    , [IPOSession_Name] AS IPO_SessionName
                    , [No_Of_Share] AS No_Of_Share
                    , [Amount] AS Amount
                    , [Total_Share_Value] AS ToTal_ShareValue
                    , [Premium] AS Premium
                    , [TotalAmount] AS TotalAmount
                    , [Session_Date] AS CutofDate 
                    , comp.ID as [IPO_Company_ID]
                    , comp.Company_Name as [IPO_Company_Name]
                    , comp.Company_Address as [IPO_Company_Address]
                    , comp.Bank_ID as [IPO_Company_Bank_ID]
                    , comp.Bank_Name as [IPO_Company_Bank_Name]
                    , comp.Branch_ID as [IPO_Company_Branch_ID]
                    , comp.Branch_Name as [IPO_Company_Branch_Name]
                    , comp.BankAcc_No as [IPO_Company_BankAcc_No]
                    , comp.RoutingNo as [IPO_Company_RoutingNo]
                    , sess.IPOSession_Desc as [IPOSession_Desc]
                    , sess.Application_Type_ID as [Application_Type_ID]
                    , sess.Total_Share_Value as [Total_Share_Value]
                    , ISNULL(sess.IsMaturedForTrade,0) as [IsMaturedForTrade]
                    , sess.[Status] as [Status]
                    ,0 AS WebSyncFlag
                    FROM [SBP_Database].[dbo].[SBP_IPO_Session] AS sess
                    JOIN 
                    [SBP_Database].[dbo].[SBP_IPO_Company_Info] AS comp
                    ON sess.[IPO_Company_ID]=comp.[ID]
                    WHERE sess.[Status]=0  ";
            try
            {
                _dbConnection.ConnectDatabase();
                dr = _dbConnection.ExecuteReader(query);

            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
            return dr;
        }

        public SqlDataReader GetIPO_Customer_All_UITransApplied()
        {
            SqlDataReader dr;
            string query = string.Empty;
            query = @"  SELECT 
                        cust.Cust_Code AS [Cust_Code]
                        ,per.Cust_Name AS [Cust_Name]
                        ,per.Gender AS [Gender]
                        ,(Select t.Name From SBP_Database.dbo.SBP_Guardian1 As t Where t.Cust_Code=cust.Cust_Code) AS [Guardian]
                        ,per.Mother_Name AS [Mother_name]
                        ,per.Father_Name AS [Father_Husband_Name]
                        ,[cont].Address1 AS [Cust_Address]
                        ,cont.Phone AS [telephone]
                        ,cont.Mobile AS [mobile]
                        ,[add].Nationality AS [nationality]
                        ,[add].Recidency AS [Recidency]
                        ,per.[DOB] AS [DateOfBirth]
                        ,per.[Occupation] as [Occupation]
                        ,'' AS authorized_name 
                        ,'' AS authorized_address 
                        ,'' AS PinCode
                        ,bnk.Bank_Name AS [BankName]
                        ,bnk.Branch_Name AS [BranchName]
                        ,bnk.Account_No AS [BankAccoNo]
                        ,bnk.Routing_No AS [BankAccRouingNo]    
                        ,cust.BO_ID AS bo_id
                        ,( Select t.BO_Status From SBP_Database.dbo.SBP_BO_Status As t Where t.BO_Status_ID=cust.BO_Status_ID) AS [BO_Status]
                        ,cust.BO_Open_Date AS BO_Open_date
                        ,cust.BO_Close_Date AS BO_Close_date
                        ,(Select t.BO_Type From SBP_BO_Type as t Where t.BO_Type_ID=cust.BO_Type_ID) AS [AC_Type]
                        ,(Select t.Cust_Status  From SBP_Database.dbo.SBP_Cust_Status As t Where t.Cust_Status_ID=cust.Cust_Status_ID) AS [AC_Status]                      
                        ,cust.Cust_Close_Date AS [AC_Closed_date]
                        FROM [SBP_Database].dbo.[SBP_Customers] AS cust
                        LEFT OUTER JOIN dbo.SBP_Cust_Additional_Info AS [add]
                        ON cust.Cust_Code=[add].Cust_Code
                        LEFT OUTER JOIN dbo.SBP_Cust_Personal_Info AS per
                        ON cust.Cust_Code=per.Cust_Code
                        LEFT OUTER JOIN dbo.SBP_Cust_Contact_Info as cont
                        ON cust.Cust_Code=cont.Cust_Code
                        LEFT OUTER JOIN dbo.SBP_Cust_Bank_Info as bnk
                        ON cust.Cust_Code=bnk.Cust_Code
                        WHERE cust.BO_Status_ID=1 AND cust.Cust_Code>='100'" + @"
                        --AND cust.Cust_Code IN (
                        --Select t.Cust_Code from SBP_Service_Registration as t  
                        --Where t.[Web_Service]=1 OR t.[SMS_Confirmation]=1 OR t.[SMS_Trade]=1 OR t.[Email]=1 OR t.[SMS_MoneyDeposit_Confirmation]=1 
                        --OR t.[SMS_MoneyWithdraw_Confirmation]=1 
                        --OR t.[SMS_EFTWithdraw_Confirmation]=1 
                        --OR t.[SMS_IPOConfiramation]=1 
                        --OR t.[MoneyDeposit_Confirmation_Email]=1 
                        --OR t.[MoneyWithdraw_Confirmation_Email]=1 
                        --OR t.[EFTWithdraw_Confirmation_Email]=1 
                        --OR t.[Trade_Confirmation_Email]=1 
                        --OR t.[IPO_Confiramation_Email]=1  
                        --)
                       ";
            try
            {
                //_dbConnection.ConnectDatabase();
                dr = _dbConnection.ExecuteReader(query);

            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
            return dr;
        }

        public SqlDataReader GetIPO_Customer_All_UITransApplied(string Cust_Code)
        {
            SqlDataReader dr;
            string query = string.Empty;
            query = @"  SELECT 
                        cust.Cust_Code AS [Cust_Code]
                        ,per.Cust_Name AS [Cust_Name]
                        ,per.Gender AS [Gender]
                        ,(Select t.Name From SBP_Database.dbo.SBP_Guardian1 As t Where t.Cust_Code=cust.Cust_Code) AS [Guardian]
                        ,per.Mother_Name AS [Mother_name]
                        ,per.Father_Name AS [Father_Husband_Name]
                        ,[cont].Address1 AS [Cust_Address]
                        ,cont.Phone AS [telephone]
                        ,cont.Mobile AS [mobile]
                        ,[add].Nationality AS [nationality]
                        ,[add].Recidency AS [Recidency]
                        ,per.[DOB] AS [DateOfBirth]
                        ,per.[Occupation] as [Occupation]
                        ,'' AS authorized_name 
                        ,'' AS authorized_address 
                        ,'' AS PinCode
                        ,bnk.Bank_Name AS [BankName]
                        ,bnk.Branch_Name AS [BranchName]
                        ,bnk.Account_No AS [BankAccoNo]
                        ,bnk.Routing_No AS [BankAccRouingNo]    
                        ,cust.BO_ID AS bo_id
                        ,( Select t.BO_Status From SBP_Database.dbo.SBP_BO_Status As t Where t.BO_Status_ID=cust.BO_Status_ID) AS [BO_Status]
                        ,cust.BO_Open_Date AS BO_Open_date
                        ,cust.BO_Close_Date AS BO_Close_date
                        ,(Select t.BO_Type From SBP_BO_Type as t Where t.BO_Type_ID=cust.BO_Type_ID) AS [AC_Type]
                        ,(Select t.Cust_Status  From SBP_Database.dbo.SBP_Cust_Status As t Where t.Cust_Status_ID=cust.Cust_Status_ID) AS [AC_Status]                      
                        ,cust.Cust_Close_Date AS [AC_Closed_date]
                        FROM [SBP_Database].dbo.[SBP_Customers] AS cust
                        LEFT OUTER JOIN dbo.SBP_Cust_Additional_Info AS [add]
                        ON cust.Cust_Code=[add].Cust_Code
                        LEFT OUTER JOIN dbo.SBP_Cust_Personal_Info AS per
                        ON cust.Cust_Code=per.Cust_Code
                        LEFT OUTER JOIN dbo.SBP_Cust_Contact_Info as cont
                        ON cust.Cust_Code=cont.Cust_Code
                        LEFT OUTER JOIN dbo.SBP_Cust_Bank_Info as bnk
                        ON cust.Cust_Code=bnk.Cust_Code
                        WHERE cust.BO_Status_ID=1 AND cust.Cust_Code>='100'" + @"
                        --AND cust.Cust_Code IN (
                        --Select t.Cust_Code from SBP_Service_Registration as t  
                        --Where t.[Web_Service]=1 OR t.[SMS_Confirmation]=1 OR t.[SMS_Trade]=1 OR t.[Email]=1 OR t.[SMS_MoneyDeposit_Confirmation]=1 
                        --OR t.[SMS_MoneyWithdraw_Confirmation]=1 
                        --OR t.[SMS_EFTWithdraw_Confirmation]=1 
                        --OR t.[SMS_IPOConfiramation]=1 
                        --OR t.[MoneyDeposit_Confirmation_Email]=1 
                        --OR t.[MoneyWithdraw_Confirmation_Email]=1 
                        --OR t.[EFTWithdraw_Confirmation_Email]=1 
                        --OR t.[Trade_Confirmation_Email]=1 
                        --OR t.[IPO_Confiramation_Email]=1  
                        --)
                        AND cust.Cust_Code='"+Cust_Code+"'";
            try
            {
                //_dbConnection.ConnectDatabase();
                dr = _dbConnection.ExecuteReader(query);

            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
            return dr;
        }

        public SqlDataReader GetIPO_Charge_UITransApplied()
        {
            SqlDataReader dr;
            string query = string.Empty;
            query = @"  SELECT Ch_ID,Ch_Item,Ch_Rate,Ch_Effective_Date     
                        FROM [SBP_Database].[dbo].[SBP_Ch_Def_All]
                        WHERE Ch_Item IN ('" + Indication_IPOPaymentTransaction.IPOApp_Charge + "','" + Indication_IPOPaymentTransaction.IPORefund_Charge + @"')";
            try
            {
                //_dbConnection.ConnectDatabase();
                dr = _dbConnection.ExecuteReader(query);

            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
            return dr;
        }

        public SqlDataReader GetIPO_Customer_IPO_Account_UITransApplied()
        {
            SqlDataReader dr;
            string query = string.Empty;
            query = @"  SELECT 
                        T.Cust_Code
                        ,ISNULL(SUM(T.dep_Bal),0)-ISNULL(SUM(T.with_Bal),0) AS Balance
                        ,ISNULL(SUM(T.dep),0) AS Deposit
                        ,ISNULL(SUM(T.[with]),0) AS withdraw
                        ,MAX([Date]) AS last_update
                        FROM (
                            Select
                            Cust_Code
                            ,(
                                CASE 
                                    WHEN Deposit_Withdraw='Deposit' AND Money_TransactionType_Name NOT IN ('TRTA') THEN Amount
                                END
                            ) AS dep
                            ,(
                                CASE 
                                    WHEN Deposit_Withdraw='Withdraw' AND Money_TransactionType_Name NOT IN ('TRTA') THEN Amount
                                END
                            )AS [with]
                            ,(
                                CASE 
                                    WHEN Deposit_Withdraw='Deposit'  THEN Amount
                                END
                            ) AS dep_Bal
                            ,(
                                CASE 
                                    WHEN Deposit_Withdraw='Withdraw' THEN Amount
                                END
                            )AS with_Bal
                            ,Received_Date as [Date]
                            From [SBP_Database].[dbo].SBP_IPO_Customer_Broker_MoneyTransaction as trn
                           
                        ) AS T
                        --RIGHT OUTER JOIN [SBP_Database].[dbo].[SBP_Customers] as cust
                        --ON cust.Cust_Code=T.Cust_Code
                        WHERE T.Cust_Code>='100' 
                        GROUP BY T.Cust_Code";
            try
            {
                //_dbConnection.ConnectDatabase();
                dr = _dbConnection.ExecuteReader(query);

            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
            return dr;
        }

        public SqlDataReader GetIPO_Customer_IPO_Account_UITransApplied(string[] Cust_Code)
        {
            string JoinedCust_Code = (Cust_Code.Length > 0 ? String.Join(",", Cust_Code) : "0");

            SqlDataReader dr;
            string query = string.Empty;
            query = @"   SELECT 
                        T.Cust_Code
                        ,ISNULL(SUM(T.dep_Bal),0)-ISNULL(SUM(T.with_Bal),0) AS Balance
                        ,ISNULL(SUM(T.dep),0) AS Deposit
                        ,ISNULL(SUM(T.[with]),0) AS withdraw
                        ,MAX([Date]) AS last_update
                        FROM (
                            Select
                            Cust_Code
                            ,(
                                CASE 
                                    WHEN Deposit_Withdraw='Deposit' AND Money_TransactionType_Name NOT IN ('TRTA') THEN Amount
                                    ELSE 0.00
                                END
                            ) AS dep
                            ,(
                                CASE 
                                    WHEN Deposit_Withdraw='Withdraw' AND Money_TransactionType_Name NOT IN ('TRTA') THEN Amount
                                    ELSE 0.00
                                END
                            )AS [with]
                            ,(
                                CASE 
                                    WHEN Deposit_Withdraw='Deposit'  THEN Amount
                                    ELSE 0.00
                                END
                            ) AS dep_Bal
                            ,(
                                CASE 
                                    WHEN Deposit_Withdraw='Withdraw' THEN Amount
                                    ELSE 0.00
                                END
                            )AS with_Bal
                            ,Received_Date as [Date]
                            From [SBP_Database].[dbo].SBP_IPO_Customer_Broker_MoneyTransaction as trn
                           
                        ) AS T
                        --RIGHT OUTER JOIN [SBP_Database].[dbo].[SBP_Customers] as cust
                        --ON cust.Cust_Code=T.Cust_Code
                        WHERE T.Cust_Code>='100' AND T.Cust_Code IN (" + JoinedCust_Code + @")
                        GROUP BY T.Cust_Code";
            try
            {
                //_dbConnection.ConnectDatabase();
                dr = _dbConnection.ExecuteReader(query);

            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
            return dr;
        }

        public SqlDataReader GetIPO_Customer_Trade_Account_UITransApplied()
        {
            SqlDataReader dr;
            string query = string.Empty;
            query = @"  Select 
                        temp.Cust_Code
                        ,ISNULL(SUM(Balance),0) AS Balance
                        ,ISNULL(SUM(Sell_Deposit),0) AS Deposit
                        ,ISNULL(SUM(Buy_Withdraw),0) AS Withdraw
                        ,MAX(Rec_Date) AS [last_update]
                        From [SBP_Database].[dbo].SBP_Money_Balance_Temp AS temp
                        --RIGHT OUTER JOIN [SBP_Database].[dbo].[SBP_Customers] as cust
                        --ON cust.Cust_Code=temp.Cust_Code
                        WHERE ISNULL(temp.Cust_Code,'')<>'' AND temp.Cust_Code>='100'
                        GROUP BY temp.Cust_Code
            ";
            try
            {
                //_dbConnection.ConnectDatabase();
                dr = _dbConnection.ExecuteReader(query);

            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
            return dr;
        }

        public SqlDataReader GetIPO_Customer_Trade_Account_UITransApplied(string[] Cust_Code)
        {
            string JoinedCust_Code = (Cust_Code.Length > 0 ? String.Join(",", Cust_Code) : "0");

            SqlDataReader dr;
            string query = string.Empty;
            query = @" Select 
                        temp.Cust_Code
                        ,ISNULL(SUM(Balance),0) AS Balance
                        ,ISNULL(SUM(Sell_Deposit),0) AS Deposit
                        ,ISNULL(SUM(Buy_Withdraw),0) AS Withdraw
                        ,MAX(Rec_Date) AS [last_update]
                        From [SBP_Database].[dbo].[SBP_Money_Balance_Temp] AS temp
                        --RIGHT OUTER JOIN [SBP_Database].[dbo].[SBP_Customers] as cust
                        --ON cust.Cust_Code=temp.Cust_Code
                        WHERE ISNULL(temp.Cust_Code,'')<>'' AND temp.Cust_Code>='100'
                        AND ISNULL(temp.Cust_Code,'') IN (" + JoinedCust_Code + @")
                        GROUP BY temp.Cust_Code";
            try
            {
                //_dbConnection.ConnectDatabase();
                dr = _dbConnection.ExecuteReader(query);

            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
            return dr;
        }

        public SqlDataReader GetIPO_ServiceRegistration_UITransApplied()
        {
            SqlDataReader dr;
            string query = string.Empty;
            query = @"  SELECT T.Cust_Code,MAX(Mobile_No) AS Mobile_No,MAX(Email) AS Email,MAX(WebService) AS WebService,MAX(EmailService) AS EmailService, MAX(SmsService) AS SmsService, 0 AS WebSyncFlag
                        FROM (
                                SELECT cust.Cust_Code,Mobile_No,Email,0 AS WebService,0 As EmailService,1 AS SmsService  
                                FROM SBP_Service_Registration as reg
                                JOIN SBP_Customers as cust
                                ON reg.Cust_Code=cust.Cust_Code
                                WHERE 
                                (
                                    ISNULL(reg.SMS_Confirmation,0)=1
                                    OR
                                    ISNULL(reg.SMS_EFTWithdraw_Confirmation,0)=1
                                    OR
                                    ISNULL(reg.SMS_IPOConfiramation,0)=1
                                    OR
                                    ISNULL(reg.SMS_MoneyDeposit_Confirmation,0)=1
                                    OR
                                    ISNULL(reg.SMS_MoneyWithdraw_Confirmation,0)=1
                                    OR
                                    ISNULL(reg.SMS_Trade,0)=1
                                    
                                )
                                AND ISNULL(reg.Mobile_No,'')<>''
                                AND cust.BO_Status_ID=1 
                                
                                UNION ALL
                                
                                SELECT cust.Cust_Code,Mobile_No,Email,0 AS WebService,1 As EmailService,0 AS SmsService    
                                FROM SBP_Service_Registration as reg
                                JOIN SBP_Customers as cust
                                ON reg.Cust_Code=cust.Cust_Code
                                WHERE 
                                (                        

                                    ISNULL(reg.MoneyDeposit_Confirmation_Email,0)=1
                                    OR 
                                    ISNULL(reg.MoneyWithdraw_Confirmation_Email,0)=1
                                    OR 
                                    ISNULL(reg.EFTWithdraw_Confirmation_Email,0)=1
                                    OR 
                                    ISNULL(reg.Trade_Confirmation_Email,0)=1
                                    OR 
                                    ISNULL(reg.IPO_Confiramation_Email,0)=1
                                )
                                AND ISNULL(reg.Email,'')<>''
                                AND cust.BO_Status_ID=1 

                               UNION ALL

                               SELECT cust.Cust_Code,Mobile_No,Email,1 AS WebService,0 As EmailService,0 AS SmsService    
                               FROM SBP_Service_Registration as reg
                               JOIN SBP_Customers as cust
                               ON reg.Cust_Code=cust.Cust_Code 
                               WHERE ISNULL(reg.Web_Service,0)<>0
                               AND cust.BO_Status_ID=1  

                          )AS T 
                          GROUP BY T.Cust_Code";

            try
            {
                //_dbConnection.ConnectDatabase();
                dr = _dbConnection.ExecuteReader(query);

            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
            return dr;
        }

        public SqlDataReader GetIPO_ServiceRegistration_UITransApplied(string[] Cust_Code)
        {
            SqlDataReader dr;
            string joinedCust_Code = String.Join(",", Cust_Code);
            string query = string.Empty;
            query = @"  SELECT T.Cust_Code,MAX(Mobile_No) AS Mobile_No,MAX(Email) AS Email,MAX(WebService) AS WebService,MAX(EmailService) AS EmailService, MAX(SmsService) AS SmsService, 0 AS WebSyncFlag
                        FROM (
                                SELECT Cust_Code,Mobile_No,Email,0 AS WebService,0 As EmailService,1 AS SmsService  
                                FROM SBP_Service_Registration as reg
                                WHERE 
                                (
                                    ISNULL(reg.SMS_Confirmation,0)=1
                                    OR
                                    ISNULL(reg.SMS_EFTWithdraw_Confirmation,0)=1
                                    OR
                                    ISNULL(reg.SMS_IPOConfiramation,0)=1
                                    OR
                                    ISNULL(reg.SMS_MoneyDeposit_Confirmation,0)=1
                                    OR
                                    ISNULL(reg.SMS_MoneyWithdraw_Confirmation,0)=1
                                    OR
                                    ISNULL(reg.SMS_Trade,0)=1
                                    
                                )
                                AND ISNULL(reg.Mobile_No,'')<>''
                                AND reg.Cust_Code IN ("+joinedCust_Code+@")
                                
                                UNION ALL
                                
                                SELECT Cust_Code,Mobile_No,Email,0 AS WebService,1 As EmailService,0 AS SmsService    
                                FROM SBP_Service_Registration as reg
                                WHERE 
                                (                        

                                    ISNULL(reg.MoneyDeposit_Confirmation_Email,0)=1
                                    OR 
                                    ISNULL(reg.MoneyWithdraw_Confirmation_Email,0)=1
                                    OR 
                                    ISNULL(reg.EFTWithdraw_Confirmation_Email,0)=1
                                    OR 
                                    ISNULL(reg.Trade_Confirmation_Email,0)=1
                                    OR 
                                    ISNULL(reg.IPO_Confiramation_Email,0)=1
                                )
                                AND ISNULL(reg.Email,'')<>''
                                AND reg.Cust_Code IN (" + joinedCust_Code + @")

                               UNION ALL

                               SELECT Cust_Code,Mobile_No,Email,1 AS WebService,0 As EmailService,0 AS SmsService    
                               FROM SBP_Service_Registration as reg
                               WHERE ISNULL(reg.Web_Service,0)<>0
                               AND reg.Cust_Code IN (" + joinedCust_Code + @") 

                          )AS T 
                          GROUP BY T.Cust_Code";

            try
            {
                //_dbConnection.ConnectDatabase();
                dr = _dbConnection.ExecuteReader(query);

            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
            return dr;
        }

        public void InsertTable_SMSSyncExport_Confirmation_SMS_Reg_UITransApplied(SqlDataReader dr)
        {

            List<string> InsertQueryList = new List<string>();
            IPOProcessBAL ipoBal = new IPOProcessBAL();
            DataTable dt_RefundType = ipoBal.GetRefundMethod();

            try
            {
                while (dr.Read())
                {
                    string temp = @"INSERT INTO [dbksclCallCenter].[dbo].[tbl_Confirmation_SMS_Reg]
                                   (
                                    [Cust_Code]
                                    ,[Mobile]
                                    ,[Email]
                                    ,[WebService]
                                    ,[EmailService]
                                    ,[SmsService]
                                    ,[WebSyncFlag]
                                    )
                                    VALUES
                                   (
                                    '" + dr["Cust_Code"] + @"'
                                    ,'" + dr["Mobile_No"] + @"'
                                    ,'" + dr["Email"] + @"'
                                    ," + dr["WebService"] + @"
                                    ," + dr["EmailService"] + @"
                                    ," + dr["SmsService"] + @"
                                    ," + dr["WebSyncFlag"] + @"
                                    )";
                    _dbConnection.ExecuteNonQuery_SMSSender(temp);
                    //InsertQueryList.Add(temp);

                    //_dbConnection.ConnectDatabase();
                    //_dbConnection.StartTransaction();
                    //foreach (string data in InsertQueryList)
                    //    _dbConnection.ExecuteNonQuery_SMSSender(data);
                    //_dbConnection.ExecuteNonQuery(queryStringUploadHistory);
                    //_dbConnection.Commit();
                }
            }
            catch (Exception exception)
            {
                //_dbConnection.Rollback();
                throw exception;
            }
            finally
            {
                dr.Close();
                //_dbConnection.CloseDatabase();
            }
        }

        public void TruncateTable_SMSSyncExport_Confirmation_SMS_Reg_UITransApplied()
        {

            string query = string.Empty;
            query = "TRUNCATE TABLE [tbl_Confirmation_SMS_Reg]";
            CommonBAL comBAL = new CommonBAL();
            //int slNo = comBAL.GenerateID("SBP_Upload_History", "Upload_ID");
            //queryStringUploadHistory = "INSERT INTO SBP_Upload_History(Upload_ID,File_Name,Upload_Date,Entry_By) SELECT TOP 1 " + slNo + ",'tradeFile',Date,'admin' FROM SBP_Transactions_Temp_FlexTrade";

            try
            {
                //_dbConnection.ConnectDatabase();
                //_dbConnection.StartTransaction();
                _dbConnection.ExecuteNonQuery_SMSSender(query);
                //_dbConnection.ExecuteNonQuery(queryStringUploadHistory);
                //_dbConnection.Commit();
            }
            catch (Exception exception)
            {
                //_dbConnection.Rollback();
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
        }

        public void DeleteData_SMSSyncExport_Confirmation_SMS_Reg_UITransApplied(string[] Cust_Code)
        {

            string query = string.Empty;
            string JoinedCust_Code = (Cust_Code.Length > 0 ? String.Join(",", Cust_Code) : "0");

            query = "DELETE [tbl_Confirmation_SMS_Reg] WHERE Cust_Code IN (" + JoinedCust_Code + ")";
            CommonBAL comBAL = new CommonBAL();
            //int slNo = comBAL.GenerateID("SBP_Upload_History", "Upload_ID");
            //queryStringUploadHistory = "INSERT INTO SBP_Upload_History(Upload_ID,File_Name,Upload_Date,Entry_By) SELECT TOP 1 " + slNo + ",'tradeFile',Date,'admin' FROM SBP_Transactions_Temp_FlexTrade";

            try
            {
                //_dbConnection.ConnectDatabase_SMSSender();
                //_dbConnection.StartTransaction();
                _dbConnection.ExecuteNonQuery_SMSSender(query);
                //_dbConnection.ExecuteNonQuery(queryStringUploadHistory);
                //_dbConnection.Commit();
            }
            catch (Exception exception)
            {
                //_dbConnection.Rollback();
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase_SMSSender();
            }
        }

        public SqlDataReader GetData_FreeMoneyTransactionRequest_Status_UITransApplied()
        {
           
            SqlDataReader dr;
            string query = string.Empty;
            query = @"    Select Cust_Code AS [Cust_Code]
                            ,ID AS Trans_ID
                            ,Received_Date AS [Received_Date]
                            ,Amount AS [Amount]
                            ,Deposit_Withdraw AS [Deposit_Withdraw]
                            ,Money_TransactionType_Name AS [TransactionType]
                            ,Trans_Reason AS [Trans_Reason]
                            ,Remarks AS [Remarks]
                            ,(
	                            CASE 
			                            WHEN Approval_Status=0 THEN 'Pending'
			                            WHEN Approval_Status=1 THEN 'Approved'
			                            WHEN Approval_Status=2 THEN 'Rejected'
	                            END 
                            )AS [Status]
                            ,Approval_Date AS [ActionDate]
                            ,trn.Rejected_Reason AS [ActionDesc]
                             ,trn.ChannelID AS [ChannelID]
                             ,trn.Channel AS [ChannelType]
                             ,0 AS WebSyncFalg
                            From SBP_IPO_Customer_Broker_MoneyTransaction as trn
                            Where ISNULL(trn.Channel,'')<>'' AND ISNULL(trn.ChannelID,0)<>0
                            AND Approval_Status>0 AND ISNULL(Intended_IPOSession_ID,0)=0
                            
                            UNION ALL
		
		                    SELECT 
                            [Cust_Code]
                            ,0 AS Trans_ID 
                            ,delLog.Deleted_Date AS [Received_Date]
                            ,delLog.Amount AS [Amount]
                            ,delLog.Deposit_Withdraw AS [Deposit_Withdraw]
                            ,delLog.Money_TransactionType_Name AS [TransactionType]
                            ,'' AS [Trans_Reason]
                            ,delLog.Remarks AS [Remarks]
                            ,'Rejected' AS [Status]
                            ,delLog.Deleted_Date AS [ActionDate]
                            ,delLog.Delete_Reason AS [ActionDesc]
                            ,ISNULL(delLog.SMSReqID,0) AS [ChannelID]
                            ,ISNULL(delLog.Media_Type,'') AS [ChannelType]
                            ,0 AS WebSyncFalg
		                    FROM [SBP_Database].[dbo].[SMS_Sync_Import_IPORequest_DeleteLog] as delLog
		                    Where ISNULL(delLog.[IPOSessionID],0) =0"
                
                ;
            try
            {
                //_dbConnection.ConnectDatabase();
                dr = _dbConnection.ExecuteReader(query);

            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
            return dr;
        }

        public SqlDataReader GetData_FreeMoneyTransactionRequest_Status_UITransApplied(string[] IDs)
        {
            string ExportedIDs = string.Empty;
            string JoinedCust_IDs = (IDs.Length > 0 ? String.Join(",", IDs) : "0");

            SqlDataReader dr;
            string query = string.Empty;
            query = @"    Select Cust_Code AS [Cust_Code]
                            ,ID AS Trans_ID
                            ,Received_Date AS [Received_Date]
                            ,Amount AS [Amount]
                            ,Deposit_Withdraw AS [Deposit_Withdraw]
                            ,Money_TransactionType_Name AS [TransactionType]
                            ,Trans_Reason AS [Trans_Reason]
                            ,Remarks AS [Remarks]
                            ,(
	                            CASE 
			                            WHEN Approval_Status=0 THEN 'Pending'
			                            WHEN Approval_Status=1 THEN 'Approved'
			                            WHEN Approval_Status=2 THEN 'Rejected'
	                            END 
                            )AS [Status]
                            ,Approval_Date AS [ActionDate]
                            ,trn.Rejected_Reason AS [ActionDesc]
                             ,trn.ChannelID AS [ChannelID]
                             ,trn.Channel AS [ChannelType]
                             ,0 AS WebSyncFalg
                            From SBP_IPO_Customer_Broker_MoneyTransaction as trn
                            Where ISNULL(trn.Channel,'')<>'' AND ISNULL(trn.ChannelID,0)<>0
                            AND Approval_Status>0 AND ISNULL(Intended_IPOSession_ID,0)=0
                            AND ID IN (" +JoinedCust_IDs+@")
                            
                            UNION ALL
		
		                    SELECT 
                            [Cust_Code]
                            ,0 AS Trans_ID 
                            ,delLog.Deleted_Date AS [Received_Date]
                            ,delLog.Amount AS [Amount]
                            ,delLog.Deposit_Withdraw AS [Deposit_Withdraw]
                            ,delLog.Money_TransactionType_Name AS [TransactionType]
                            ,'' AS [Trans_Reason]
                            ,delLog.Remarks AS [Remarks]
                            ,'Rejected' AS [Status]
                            ,delLog.Deleted_Date AS [ActionDate]
                            ,delLog.Delete_Reason AS [ActionDesc]
                            ,ISNULL(delLog.SMSReqID,0) AS [ChannelID]
                            ,ISNULL(delLog.Media_Type,'') AS [ChannelType]
                            ,0 AS WebSyncFalg
		                    FROM [SBP_Database].[dbo].[SMS_Sync_Import_IPORequest_DeleteLog] as delLog
		                    Where ISNULL(delLog.[IPOSessionID],0) =0"

                ;
            try
            {
                //_dbConnection.ConnectDatabase();
                dr = _dbConnection.ExecuteReader(query);

            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
            return dr;
        }
        
        public void DeleteData_FreeMoneyTransactionRequest_UITransApplied(string[] IDs)
        {

            string query = string.Empty;
            string JoinedCust_IDs = (IDs.Length > 0 ? String.Join(",", IDs) : "0");
            query = "DELETE FROM [tbl_MoneyTransactionRequest_Status] WHERE Trans_ID IN (" + JoinedCust_IDs + ")";
            CommonBAL comBAL = new CommonBAL();
            //int slNo = comBAL.GenerateID("SBP_Upload_History", "Upload_ID");
            //queryStringUploadHistory = "INSERT INTO SBP_Upload_History(Upload_ID,File_Name,Upload_Date,Entry_By) SELECT TOP 1 " + slNo + ",'tradeFile',Date,'admin' FROM SBP_Transactions_Temp_FlexTrade";

            try
            {
                //_dbConnection.ConnectDatabase();
                //_dbConnection.StartTransaction();
                _dbConnection.ExecuteNonQuery_SMSSender(query);
                //_dbConnection.ExecuteNonQuery(queryStringUploadHistory);
                //_dbConnection.Commit();
            }
            catch (Exception exception)
            {
                //_dbConnection.Rollback();
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
        }


        public void TruncateTable_MoneyTransactionRequest_UITransApplied()
        {

            string query = string.Empty;
            query = "TRUNCATE TABLE [tbl_MoneyTransactionRequest_Status]";
            CommonBAL comBAL = new CommonBAL();
            //int slNo = comBAL.GenerateID("SBP_Upload_History", "Upload_ID");
            //queryStringUploadHistory = "INSERT INTO SBP_Upload_History(Upload_ID,File_Name,Upload_Date,Entry_By) SELECT TOP 1 " + slNo + ",'tradeFile',Date,'admin' FROM SBP_Transactions_Temp_FlexTrade";

            try
            {
                //_dbConnection.ConnectDatabase();
                //_dbConnection.StartTransaction();
                _dbConnection.ExecuteNonQuery_SMSSender(query);
                //_dbConnection.ExecuteNonQuery(queryStringUploadHistory);
                //_dbConnection.Commit();
            }
            catch (Exception exception)
            {
                //_dbConnection.Rollback();
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
        }

        public void InsertTable_MoneyTransactionRequest_Status_UITransApplied(SqlDataReader dr)
        {

            List<string> InsertQueryList = new List<string>();
            //int i = 1;
            try
            {
                //_dbConnection.ConnectDatabase();
                //_dbConnection.StartTransaction();
                while (dr.Read())
                {
                    //if (i == 27)
                    //{
                    //    string kamal = "Kamal";
                    //}

                    string temp = @"INSERT INTO 
                    [tbl_MoneyTransactionRequest_Status]
                    (
                          [Trans_ID]   
                          ,[Cust_Code]
                          ,[Received_Date]
                          ,[Amount]
                          ,[Deposit_Withdraw]
                          ,[TransactionType]
                          ,[Trans_Reason]
                          ,[Remarks]
                          ,[Status]
                          ,[ActionDate]
                          ,[ActionDesc]
                          ,[ChannelID]
                          ,[ChannelType]
                          ,[WebSyncFalg]
                    )
                    VALUES (
                        " + Convert.ToString(dr["Trans_ID"]) + @"    
                        ,'" + Convert.ToString(dr["Cust_Code"]) + @"'
                        ,'" + Convert.ToString(dr["Received_Date"]) + @"'
                        ," + Convert.ToString(dr["Amount"]) + @"
                        ,'" + Convert.ToString(dr["Deposit_Withdraw"]) + @"'
                        ,'" + Convert.ToString(dr["TransactionType"]) + @"'
                        ,'" + Convert.ToString(dr["Trans_Reason"]) + @"'
                        ,'" + Convert.ToString(dr["Remarks"]) + @"'
                        ,'" + Convert.ToString(dr["Status"]) + @"'
                        ,'" + Convert.ToString(dr["ActionDate"]) + @"'
                        ,'" + Convert.ToString(dr["ActionDesc"]) + @"'
                        ," + Convert.ToString(dr["ChannelID"]) + @"
                        ,'" + Convert.ToString(dr["ChannelType"]) + @"'
                        ," + Convert.ToString(dr["WebSyncFalg"]) + @"
                    )";

                    _dbConnection.ExecuteNonQuery_SMSSender(temp);
                    //i++;
                }
                //_dbConnection.ExecuteNonQuery(queryStringUploadHistory);
                //_dbConnection.Commit();
            }
            catch (Exception exception)
            {
                //_dbConnection.Rollback();
                throw exception;
            }
            finally
            {
                dr.Close();
                //_dbConnection.CloseDatabase();
            }
        }

        public void TruncateTable_SMSSyncExport_AccountGrouping_Info_UITransApplied()
        {

            string query = string.Empty;
            query = "TRUNCATE TABLE [tbl_AccountGrouping_Info]";
            CommonBAL comBAL = new CommonBAL();
            //int slNo = comBAL.GenerateID("SBP_Upload_History", "Upload_ID");
            //queryStringUploadHistory = "INSERT INTO SBP_Upload_History(Upload_ID,File_Name,Upload_Date,Entry_By) SELECT TOP 1 " + slNo + ",'tradeFile',Date,'admin' FROM SBP_Transactions_Temp_FlexTrade";

            try
            {
                //_dbConnection.ConnectDatabase();
                //_dbConnection.StartTransaction();
                _dbConnection.ExecuteNonQuery_SMSSender(query);
                //_dbConnection.ExecuteNonQuery(queryStringUploadHistory);
                //_dbConnection.Commit();
            }
            catch (Exception exception)
            {
                //_dbConnection.Rollback();
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
        }

        public void InsertTable_SMSSyncExport_AccountGrouping_Info_UITransApplied(SqlDataReader dr)
        {

            List<string> InsertQueryList = new List<string>();
            //int i = 1;
            try
            {
                //_dbConnection.ConnectDatabase();
                //_dbConnection.StartTransaction();
                while (dr.Read())
                {
                    //if (i == 27)
                    //{
                    //    string kamal = "Kamal";
                    //}

                    string temp = @"INSERT INTO 
                    [tbl_AccountGrouping_Info]
                    (
                       [Registration_ID]
                      ,[Parent_Code]
                      ,[Child_Code]
                      ,[Owner_CustCode]
                      ,[Owner_Name]
                      ,[Owner_Parent_Name]
                      ,[Owner_Mobile]
                      ,[Owner_Land]
                      ,[Owner_Email_1]
                      ,[Owner_Email_2]
                      ,[Owner_Email_3]
                      ,[Child_Name]
                      ,[Child_Account_Type]
                      ,[Child_BO_ID]
                      ,[Last_Updated_Date]
                      ,[WebSyncFlag]    
                    )
                    VALUES (
                        " + Convert.ToString(dr["Registration_ID"]) + @"
                        ,'" + Convert.ToString(dr["Parent_Code"]) + @"'
                        ,'" + Convert.ToString(dr["Child_Code"]) + @"'
                        ,'" + Convert.ToString(dr["Parent_Id"]) + @"'
                        ,'" + Convert.ToString(dr["Handeler_Name"]) + @"'
                        ,'" + Convert.ToString(dr["Handeler_Parent_Name"]) + @"'
                        ,'" + Convert.ToString(dr["Hadeler_Contact_Mobile"]) + @"'
                        ,'" + Convert.ToString(dr["Handeler_Contact_Land"]) + @"'
                        ,'" + Convert.ToString(dr["Handeler_Email_1"]) + @"'
                        ,'" + Convert.ToString(dr["Handeler_Email_2"]) + @"'
                        ,'" + Convert.ToString(dr["Handeler_Email_3"]) + @"'
                        ,'" + Convert.ToString(dr["Child_Name"]) + @"'
                        ,'" + Convert.ToString(dr["Child_Account_Type"]) + @"'
                        ,'" + Convert.ToString(dr["Child_BO_ID"]) + @"'
                        ,'" + Convert.ToString(dr["Last_Updated_Date"]) + @"'
                        ," + Convert.ToString(dr["WebSyncFlag"]) + @"   
                    )";

                    _dbConnection.ExecuteNonQuery_SMSSender(temp);
                    //i++;
                }
                //_dbConnection.ExecuteNonQuery(queryStringUploadHistory);
                //_dbConnection.Commit();
            }
            catch (Exception exception)
            {
                //_dbConnection.Rollback();
                throw exception;
            }
            finally
            {
                dr.Close();
                //_dbConnection.CloseDatabase();
            }
        }

        public SqlDataReader GetIPO_AccountGrouping_Info_UITransApplied()
        {
            SqlDataReader dr;
            string query = string.Empty;
            query = @" SELECT pr.Registration_ID,pr.Parent_Code,pr.Child_Code,own.Parent_Id,own.Handeler_Name,own.Handeler_Parent_Name,own.Hadeler_Contact_Mobile,own.Handeler_Contact_Land,own.Handeler_Email_1,own.Handeler_Email_2,own.Handeler_Email_3,ISNULL(( Select t.Cust_Name From SBP_Cust_Personal_Info as t Where t.Cust_Code=pr.Child_Code),'') AS Child_Name,ISNULL(( Select t.BO_ID From SBP_Customers as t Where t.Cust_Code=pr.Child_Code),'') AS Child_BO_ID,ISNULL(( Select t.BO_Type From SBP_Customers as c JOIN SBP_BO_Type as t ON c.BO_Type_ID=t.BO_Type_ID AND c.Cust_Code=pr.Child_Code),'') Child_Account_Type ,GETDATE() AS Last_Updated_Date,0 AS WebSyncFlag 
                        FROM [SBP_Database].[dbo].[SBP_Parent_Child_Details] as pr
                        JOIN [SBP_Database].[dbo].[SBP_Parent_Child_Owner_Details] as own
                        ON pr.Registration_ID=own.Registration_id";
            try
            {
                //_dbConnection.ConnectDatabase();
                dr = _dbConnection.ExecuteReader(query);

            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
            return dr;
        }

        public SqlDataReader GetIPO_AccountGrouping_Info_UITransApplied(string ParentCode)
        {
            SqlDataReader dr;
            string query = string.Empty;
            query = @"  SELECT pr.Registration_ID,pr.Parent_Code,pr.Child_Code,own.Parent_Id,own.Handeler_Name,own.Handeler_Parent_Name,own.Hadeler_Contact_Mobile,own.Handeler_Contact_Land,own.Handeler_Email_1,own.Handeler_Email_2,own.Handeler_Email_3,ISNULL(( Select t.Cust_Name From SBP_Cust_Personal_Info as t Where t.Cust_Code=pr.Child_Code),'') AS Child_Name,ISNULL(( Select t.BO_ID From SBP_Customers as t Where t.Cust_Code=pr.Child_Code),'') AS Child_BO_ID,ISNULL(( Select t.BO_Type From SBP_Customers as c JOIN SBP_BO_Type as t ON c.BO_Type_ID=t.BO_Type_ID AND c.Cust_Code=pr.Child_Code),'') Child_Account_Type ,GETDATE() AS Last_Updated_Date,0 AS WebSyncFlag 
                        FROM [SBP_Database].[dbo].[SBP_Parent_Child_Details] as pr
                        JOIN [SBP_Database].[dbo].[SBP_Parent_Child_Owner_Details] as own
                        ON pr.Registration_ID=own.Registration_id
                        WHERE pr.Parent_Code='" + ParentCode + "'";
            try
            {
                //_dbConnection.ConnectDatabase();
                dr = _dbConnection.ExecuteReader(query);

            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
            return dr;
        }

        public void TruncateTable_SMSSyncExport_IPO_SessionforCompanyInfo_UITransApplied()
        {

            string query = string.Empty;
            query = "TRUNCATE TABLE [tbl_IPO_SessionforCompanyInfo]";
            CommonBAL comBAL = new CommonBAL();
            //int slNo = comBAL.GenerateID("SBP_Upload_History", "Upload_ID");
            //queryStringUploadHistory = "INSERT INTO SBP_Upload_History(Upload_ID,File_Name,Upload_Date,Entry_By) SELECT TOP 1 " + slNo + ",'tradeFile',Date,'admin' FROM SBP_Transactions_Temp_FlexTrade";

            try
            {
                //_dbConnection.ConnectDatabase();
                //_dbConnection.StartTransaction();
                _dbConnection.ConnectDatabase_SMSSender();
                _dbConnection.ExecuteNonQuery_SMSSender(query);
                //_dbConnection.ExecuteNonQuery(queryStringUploadHistory);
                //_dbConnection.Commit();
            }
            catch (Exception exception)
            {
                //_dbConnection.Rollback();
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
        }

        public void InsertTable_SMSSyncExport_IPO_SessionforCompanyInfo_UITransApplied(SqlDataReader dr)
        {

            List<string> InsertQueryList = new List<string>();
            IPOProcessBAL ipoBal = new IPOProcessBAL();
            DataTable dt_RefundType = ipoBal.GetRefundMethod();

            try
            {
                while (dr.Read())
                {
                  string temp = @"INSERT INTO [dbksclCallCenter].[dbo].[tbl_IPO_SessionforCompanyInfo]
                  (
                                 [Company_Short_Code]
                                , [Company_Name]
                                , [Company_Address]
                                , [IPO_SessionID]
                                , [IPO_SessionName]
                                , [No_Of_Share]
                                , [Amount]
                                , [ToTal_ShareValue]
                                , [Premium]
                                , [TotalAmount]
                                , [CutofDate]
                                ,[IPO_Company_ID]
                                ,[IPO_Company_Name]
                                ,[IPO_Company_Address]
                                ,[IPO_Company_Bank_ID]
                                ,[IPO_Company_Bank_Name]
                                ,[IPO_Company_Branch_ID]
                                ,[IPO_Company_Branch_Name]
                                ,[IPO_Company_BankAcc_No]
                                ,[IPO_Company_RoutingNo]
                                ,[IPOSession_Desc]
                                ,[Application_Type_ID]
                                ,[Total_Share_Value]
                                ,[IsMaturedForTrade]
                                ,[Status]
                                ,[WebSyncFlag]     
                )
                VALUES (
                    '" + Convert.ToString(dr["Company_Short_Code"]) + @"'
                    ,'" + Convert.ToString(dr["Company_Name"]) + @"'
                    ,'" + Convert.ToString(dr["Company_Address"]) + @"'
                    ," + Convert.ToString(dr["IPO_SessionID"]) + @"
                    ,'" + Convert.ToString(dr["IPO_SessionName"]) + @"'
                    ," + Convert.ToString(dr["No_Of_Share"]) + @"
                    ," + Convert.ToString(dr["Amount"]) + @"
                    ," + Convert.ToString(dr["ToTal_ShareValue"]) + @"
                    ," + Convert.ToString(dr["Premium"]) + @"
                    ," + Convert.ToString(dr["TotalAmount"]) + @"
                    ,'" + Convert.ToDateTime(dr["CutofDate"].ToString()).ToShortDateString() + @"'
                    ," + Convert.ToString(dr["IPO_Company_ID"]) + @"
                    ,'" + Convert.ToString(dr["IPO_Company_Name"]) + @"'
                    ,'" + Convert.ToString(dr["IPO_Company_Address"]) + @"'
                    ," + Convert.ToString(dr["IPO_Company_Bank_ID"]) + @"
                    ,'" + Convert.ToString(dr["IPO_Company_Bank_Name"]) + @"'
                    ," + Convert.ToString(dr["IPO_Company_Branch_ID"]) + @"
                    ,'" + Convert.ToString(dr["IPO_Company_Branch_Name"]) + @"'
                    ,'" + Convert.ToString(dr["IPO_Company_BankAcc_No"]) + @"'
                    ,'" + Convert.ToString(dr["IPO_Company_RoutingNo"]) + @"'
                    ,'" + Convert.ToString(dr["IPOSession_Desc"]) + @"'
                    ," + Convert.ToString(dr["Application_Type_ID"]) + @"
                    ," + Convert.ToString(dr["Total_Share_Value"]) + @"
                    ," + Convert.ToInt32(dr["IsMaturedForTrade"]) + @"
                    ," + Convert.ToString(dr["Status"]) + @"
                    ," + Convert.ToString(dr["WebSyncFlag"]) + @"
                )";
                    _dbConnection.ExecuteNonQuery_SMSSender(temp);
                    //InsertQueryList.Add(temp);

                    //_dbConnection.ConnectDatabase();
                    //_dbConnection.StartTransaction();
                    //foreach (string data in InsertQueryList)
                    //    _dbConnection.ExecuteNonQuery_SMSSender(data);
                    //_dbConnection.ExecuteNonQuery(queryStringUploadHistory);
                    //_dbConnection.Commit();
                }
            }
            catch (Exception exception)
            {
                //_dbConnection.Rollback();
                throw exception;
            }
            finally
            {
                dr.Close();
                //_dbConnection.CloseDatabase();
            }
        }

        public void TruncateTable_SMSSyncExport_Customer_Trade_Account_UITransApplied()
        {

            string query = string.Empty;
            query = "TRUNCATE TABLE tbl_Customer_Account";
            CommonBAL comBAL = new CommonBAL();
            //int slNo = comBAL.GenerateID("SBP_Upload_History", "Upload_ID");
            //queryStringUploadHistory = "INSERT INTO SBP_Upload_History(Upload_ID,File_Name,Upload_Date,Entry_By) SELECT TOP 1 " + slNo + ",'tradeFile',Date,'admin' FROM SBP_Transactions_Temp_FlexTrade";

            try
            {
                //_dbConnection.ConnectDatabase();
                //_dbConnection.StartTransaction();
                _dbConnection.ExecuteNonQuery_SMSSender(query);
                //_dbConnection.ExecuteNonQuery(queryStringUploadHistory);
                //_dbConnection.Commit();
            }
            catch (Exception exception)
            {
                //_dbConnection.Rollback();
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
        }

        public void DeleteData_SMSSyncExport_Customer_Trade_Account_UITransApplied(string[] Cust_Code)
        {

            string JoinedCust_Code = (Cust_Code.Length > 0 ? String.Join(",", Cust_Code) : "0");

            string query = string.Empty;
            query = "Delete tbl_Customer_Account Where Cust_Code IN (" + JoinedCust_Code + @")";
            CommonBAL comBAL = new CommonBAL();
            //int slNo = comBAL.GenerateID("SBP_Upload_History", "Upload_ID");
            //queryStringUploadHistory = "INSERT INTO SBP_Upload_History(Upload_ID,File_Name,Upload_Date,Entry_By) SELECT TOP 1 " + slNo + ",'tradeFile',Date,'admin' FROM SBP_Transactions_Temp_FlexTrade";

            try
            {
                //_dbConnection.ConnectDatabase();
                //_dbConnection.StartTransaction();
                _dbConnection.ExecuteNonQuery_SMSSender(query);
                //_dbConnection.ExecuteNonQuery(queryStringUploadHistory);
                //_dbConnection.Commit();
            }
            catch (Exception exception)
            {
                //_dbConnection.Rollback();
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
        }

        public void InsertTable_SMSSyncExport_Customer_Trade_Account_UITransApplied(SqlDataReader dr)
        {

            List<string> InsertQueryList = new List<string>();
            IPOProcessBAL ipoBal = new IPOProcessBAL();
            DataTable dt_RefundType = ipoBal.GetRefundMethod();

            try
            {
                while (dr.Read())
                {
                    string temp = @"INSERT INTO [dbksclCallCenter].[dbo].[tbl_Customer_Account]
                                (
                                [Cust_Code]
                                , [Balance]
                                , [Deposit]
                                , [Withdraw]
                                , [last_update]
                                ,[WebSyncFlag] 
                                )
                        VALUES (
                            '" + Convert.ToString(dr["Cust_Code"]) + @"'
                            ," + Convert.ToString(dr["Balance"]) + @"
                            ," + Convert.ToString(dr["Deposit"]) + @"
                            ," + Convert.ToString(dr["Withdraw"]) + @"
                            ," + (Convert.ToString(dr["last_update"]) == string.Empty || Convert.ToString(dr["last_update"]) == DateTime.MaxValue.ToString() || Convert.ToString(dr["last_update"]) == DateTime.MinValue.ToString() ? "NULL" : Convert.ToString("'" + Convert.ToDateTime(dr["last_update"].ToString()).ToString() + "'")) + @"
                            ,0              
                        )";
                    //InsertQueryList.Add(temp);
                    _dbConnection.ExecuteNonQuery_SMSSender(temp);

                }

                //_dbConnection.ConnectDatabase();
                //_dbConnection.StartTransaction();

                //foreach (string data in InsertQueryList)
                //{
                //    _dbConnection.ExecuteNonQuery(data);

                //}
                //_dbConnection.ExecuteNonQuery(queryStringUploadHistory);
                //_dbConnection.Commit();
            }
            catch (Exception exception)
            {
                //_dbConnection.Rollback();
                throw exception;
            }
            finally
            {
                dr.Close();
                //_dbConnection.CloseDatabase();
            }
        }

        public void TruncateTable_SMSSyncExport_Customer_IPO_Account_UITransApplied()
        {

            string query = string.Empty;
            query = "TRUNCATE TABLE tbl_Customer_IPO_Account";
            CommonBAL comBAL = new CommonBAL();
            //int slNo = comBAL.GenerateID("SBP_Upload_History", "Upload_ID");
            //queryStringUploadHistory = "INSERT INTO SBP_Upload_History(Upload_ID,File_Name,Upload_Date,Entry_By) SELECT TOP 1 " + slNo + ",'tradeFile',Date,'admin' FROM SBP_Transactions_Temp_FlexTrade";

            try
            {
                //_dbConnection.ConnectDatabase();
                //_dbConnection.StartTransaction();
                _dbConnection.ExecuteNonQuery_SMSSender(query);
                //_dbConnection.ExecuteNonQuery(queryStringUploadHistory);
                //_dbConnection.Commit();
            }
            catch (Exception exception)
            {
                //_dbConnection.Rollback();
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
        }

        public void DeleteData_SMSSyncExport_Customer_IPO_Account_UITransApplied(string[] Cust_Code)
        {

            string query = string.Empty;
            string JoinedCust_Code = (Cust_Code.Length > 0 ? String.Join(",", Cust_Code) : "0");

            query = "DELETE tbl_Customer_IPO_Account WHERE Cust_Code IN (" + JoinedCust_Code + ")";
            CommonBAL comBAL = new CommonBAL();
            //int slNo = comBAL.GenerateID("SBP_Upload_History", "Upload_ID");
            //queryStringUploadHistory = "INSERT INTO SBP_Upload_History(Upload_ID,File_Name,Upload_Date,Entry_By) SELECT TOP 1 " + slNo + ",'tradeFile',Date,'admin' FROM SBP_Transactions_Temp_FlexTrade";

            try
            {
                //_dbConnection.ConnectDatabase();
                //_dbConnection.StartTransaction();
                _dbConnection.ExecuteNonQuery_SMSSender(query);
                //_dbConnection.ExecuteNonQuery(queryStringUploadHistory);
                //_dbConnection.Commit();
            }
            catch (Exception exception)
            {
                //_dbConnection.Rollback();
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
        }

        public void InsertTable_SMSSyncExport_Customer_IPO_Account_UITransApplied(SqlDataReader dr)
        {

            List<string> InsertQueryList = new List<string>();
            IPOProcessBAL ipoBal = new IPOProcessBAL();
            DataTable dt_RefundType = ipoBal.GetRefundMethod();

            try
            {
                while (dr.Read())
                {
                    string temp = @"INSERT INTO [dbksclCallCenter].[dbo].[tbl_Customer_IPO_Account]
                            (  [Cust_Code]
                            , [Balance]
                            , [Deposit]
                            , [Withdraw]
                            , [last_update] 
                            ,[WebSyncFlag]
                            )
                    VALUES (
                        '" + Convert.ToString(dr["Cust_Code"]) + @"'
                        ," + Convert.ToString(dr["Balance"]) + @"
                        ," + Convert.ToString(dr["Deposit"]) + @"
                        ," + Convert.ToString(dr["Withdraw"]) + @"
                       ," + (Convert.ToString(dr["last_update"]) == string.Empty || Convert.ToString(dr["last_update"]) == DateTime.MaxValue.ToString() || Convert.ToString(dr["last_update"]) == DateTime.MinValue.ToString() ? "NULL" : Convert.ToString("'" + Convert.ToDateTime(dr["last_update"].ToString()).ToString() + "'")) + @"              
                       ,0
                    )";
                    //InsertQueryList.Add(temp);
                    _dbConnection.ExecuteNonQuery_SMSSender(temp);
                }
                //_dbConnection.ConnectDatabase();
                //_dbConnection.StartTransaction();
                //foreach (string data in InsertQueryList)
                //_dbConnection.ExecuteNonQuery(data);
                //_dbConnection.ExecuteNonQuery(queryStringUploadHistory);
                //_dbConnection.Commit();
            }
            catch (Exception exception)
            {
                //_dbConnection.Rollback();
                throw exception;
            }
            finally
            {
                dr.Close();
                //_dbConnection.CloseDatabase();
            }
        }

        public void TruncateTable_SMSSyncExport_Customer_All_UITransApplied()
        {

            string query = string.Empty;
            query = "TRUNCATE TABLE tbl_Customer_All";
            CommonBAL comBAL = new CommonBAL();
            //int slNo = comBAL.GenerateID("SBP_Upload_History", "Upload_ID");
            //queryStringUploadHistory = "INSERT INTO SBP_Upload_History(Upload_ID,File_Name,Upload_Date,Entry_By) SELECT TOP 1 " + slNo + ",'tradeFile',Date,'admin' FROM SBP_Transactions_Temp_FlexTrade";

            try
            {
                //_dbConnection.ConnectDatabase();
                //_dbConnection.StartTransaction();
                _dbConnection.ExecuteNonQuery_SMSSender(query);
                //_dbConnection.ExecuteNonQuery(queryStringUploadHistory);
                //_dbConnection.Commit();
            }
            catch (Exception exception)
            {
                //_dbConnection.Rollback();
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
        }

        public void DeleteData_SMSSyncExport_Customer_All_UITransApplied(string Cust_Code)
        {

          

            string query = string.Empty;
            query = "Delete tbl_Customer_All Where Cust_Code='"+Cust_Code+"'";
            CommonBAL comBAL = new CommonBAL();
            //int slNo = comBAL.GenerateID("SBP_Upload_History", "Upload_ID");
            //queryStringUploadHistory = "INSERT INTO SBP_Upload_History(Upload_ID,File_Name,Upload_Date,Entry_By) SELECT TOP 1 " + slNo + ",'tradeFile',Date,'admin' FROM SBP_Transactions_Temp_FlexTrade";

            try
            {
                //_dbConnection.ConnectDatabase();
                //_dbConnection.StartTransaction();
                _dbConnection.ExecuteNonQuery_SMSSender(query);
                //_dbConnection.ExecuteNonQuery(queryStringUploadHistory);
                //_dbConnection.Commit();
            }
            catch (Exception exception)
            {
                //_dbConnection.Rollback();
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
        }

        public void TruncateTable_tbl_IPO_Charge_UITransApplied()
        {

            string query = string.Empty;
            query = "TRUNCATE TABLE tbl_IPO_Charge";
            CommonBAL comBAL = new CommonBAL();
            //int slNo = comBAL.GenerateID("SBP_Upload_History", "Upload_ID");
            //queryStringUploadHistory = "INSERT INTO SBP_Upload_History(Upload_ID,File_Name,Upload_Date,Entry_By) SELECT TOP 1 " + slNo + ",'tradeFile',Date,'admin' FROM SBP_Transactions_Temp_FlexTrade";

            try
            {
                //_dbConnection.ConnectDatabase();
                //_dbConnection.StartTransaction();
                _dbConnection.ExecuteNonQuery_SMSSender(query);
                //_dbConnection.ExecuteNonQuery(queryStringUploadHistory);
                //_dbConnection.Commit();
            }
            catch (Exception exception)
            {
                //_dbConnection.Rollback();
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
        }

        public void InsertTable_SMSSyncExport_Customer_All_UITransApplied(SqlDataReader dr)
        {

            List<string> InsertQueryList = new List<string>();
            IPOProcessBAL ipoBal = new IPOProcessBAL();
            DataTable dt_RefundType = ipoBal.GetRefundMethod();

            int i = 0;
            try
            {
                while (dr.Read())
                {
                    //if (i == 9603)
                    //{
                    //    string Kamal = "kamal";
                    //}

                    string temp_1 = @"INSERT INTO [dbksclCallCenter].[dbo].[tbl_Customer_All]
                          (
                               [Cust_Code]
                              ,[Cust_Name]
                              ,[Gender]
                              ,[Guardian]
                              ,[Mother_name]
                              ,[Father_Husband_Name]
                              ,[Cust_Address]
                              ,[telephone]
                              ,[mobile]
                              ,[nationality]
                              ,[Recidency]
                              ,[DateOfBirth]
                              ,[Occupation]
                              ,[authorized_name]
                              ,[authorized_address]
                              ,[PinCode]
                              ,[BankName]
                              ,[BranchName]
                              ,[BankAccoNo]
                              ,[BankAccRouingNo]
                              ,[bo_id]
                              ,[BO_Status]
                              ,[BO_Open_date]
                              ,[BO_Close_date]
                              ,[AC_Type]
                              ,[AC_Status]
                              ,[AC_Closed_date]                          
                          )
                          VALUES (   
                                '" + Convert.ToString(dr["Cust_Code"]) + @"'"
                                 + ",'" + Convert.ToString(dr["Cust_Name"]) + @"'"
                                + ",'" + Convert.ToString(dr["Gender"]) + @"'"
                                + ",'" + Convert.ToString(dr["Guardian"]) + @"'"
                                + ",'" + Convert.ToString(dr["Mother_name"]) + @"'"
                                + ",'" + Convert.ToString(dr["Father_Husband_Name"]) + @"'"    
                                + ",'" + Convert.ToString(dr["Cust_Address"]).Replace("'", "") + @"'"
                                + ",'" + Convert.ToString(dr["telephone"]) + @"'"
                                + ",'" + Convert.ToString(dr["mobile"]) + @"'"
                                + ",'" + Convert.ToString(dr["nationality"]) + @"'"
                                + ",'" + Convert.ToString(dr["Recidency"]) + @"'"
                                + ",'" + Convert.ToString(dr["DateOfBirth"]) + @"'"
                                + ",'" + Convert.ToString(dr["Occupation"]).Replace("'", "") + @"'"
                                + ",'" + Convert.ToString(dr["authorized_name"]) + @"'"
                                + ",'" + Convert.ToString(dr["authorized_address"]).Replace("'", "") + @"'"
                                + ",'" + Convert.ToString(dr["PinCode"]) + @"'"
                                + ",'" + Convert.ToString(dr["BankName"]) + @"'"
                                + ",'" + Convert.ToString(dr["BranchName"]) + @"'"
                                + ",'" + Convert.ToString(dr["BankAccoNo"]) + @"'"
                                + ",'" + Convert.ToString(dr["BankAccRouingNo"]) + @"'"
                                + ",'" + Convert.ToString(dr["bo_id"]) + @"'"
                                + ",'" + Convert.ToString(dr["BO_Status"]) + @"'"
                                + "," + (Convert.ToString(dr["BO_Open_date"]) == string.Empty || Convert.ToString(dr["BO_Open_date"]) == DateTime.MaxValue.ToString() || Convert.ToString(dr["BO_Open_date"]) == DateTime.MinValue.ToString() ? "NULL" : Convert.ToString("'" + Convert.ToDateTime(dr["BO_Open_date"].ToString()).ToShortDateString() + "'")) + @""
                                + "," + (Convert.ToString(dr["BO_Close_date"]) == string.Empty || Convert.ToString(dr["BO_Close_date"]) == DateTime.MaxValue.ToString() || Convert.ToString(dr["BO_Close_date"]) == DateTime.MinValue.ToString() ? "NULL" : Convert.ToString("'" + Convert.ToDateTime(dr["BO_Close_date"].ToString()).ToShortDateString() + "'")) + @""
                                + ",'" + Convert.ToString(dr["AC_Type"]) + @"'"
                                + ",'" + Convert.ToString(dr["AC_Status"]) + @"'"
                                + "," + (Convert.ToString(dr["AC_Closed_date"]) == string.Empty || Convert.ToString(dr["AC_Closed_date"]) == DateTime.MaxValue.ToString() || Convert.ToString(dr["AC_Closed_date"]) == DateTime.MinValue.ToString() ? "NULL" : Convert.ToString("'" + Convert.ToDateTime(dr["AC_Closed_date"].ToString()).ToShortDateString() + "'")) + @""
                            + ")";
                    _dbConnection.ExecuteNonQuery_SMSSender(temp_1);
                    //InsertQueryList.Add(temp_1);
                    i++;
                }
            }
            catch (Exception exception)
            {
                //_dbConnection.Rollback();
                throw exception;
            }
            finally
            {
                dr.Close();
                //_dbConnection.CloseDatabase();
            }
        }

        public void TruncateTable_tbl_IPO_SessionApplications_UITransApplied()
        {

            string query = string.Empty;
            query = "TRUNCATE TABLE [tbl_IPO_SessionApplications]";
            CommonBAL comBAL = new CommonBAL();
            //int slNo = comBAL.GenerateID("SBP_Upload_History", "Upload_ID");
            //queryStringUploadHistory = "INSERT INTO SBP_Upload_History(Upload_ID,File_Name,Upload_Date,Entry_By) SELECT TOP 1 " + slNo + ",'tradeFile',Date,'admin' FROM SBP_Transactions_Temp_FlexTrade";

            try
            {
                //_dbConnection.ConnectDatabase();
                //_dbConnection.StartTransaction();
                _dbConnection.ExecuteNonQuery_SMSSender(query);
                //_dbConnection.ExecuteNonQuery(queryStringUploadHistory);
                //_dbConnection.Commit();
            }
            catch (Exception exception)
            {
                //_dbConnection.Rollback();
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
        }

        public void DeleteData_tbl_IPO_SessionApplications_UITransApplied(string[] IDs)
        {

            string query = string.Empty;
            string JoinedCust_IDs = (IDs.Length > 0 ? String.Join(",", IDs) : "0");
            query = "DELETE FROM [tbl_IPO_SessionApplications] WHERE ID IN (" + JoinedCust_IDs + ")";
            CommonBAL comBAL = new CommonBAL();
            //int slNo = comBAL.GenerateID("SBP_Upload_History", "Upload_ID");
            //queryStringUploadHistory = "INSERT INTO SBP_Upload_History(Upload_ID,File_Name,Upload_Date,Entry_By) SELECT TOP 1 " + slNo + ",'tradeFile',Date,'admin' FROM SBP_Transactions_Temp_FlexTrade";

            try
            {
                //_dbConnection.ConnectDatabase();
                //_dbConnection.StartTransaction();
                _dbConnection.ExecuteNonQuery_SMSSender(query);
                //_dbConnection.ExecuteNonQuery(queryStringUploadHistory);
                //_dbConnection.Commit();
            }
            catch (Exception exception)
            {
                //_dbConnection.Rollback();
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
        }

        public void InsertTable_IPO_SessionApplications_UITransApplied(SqlDataReader dr)
        {

            List<string> InsertQueryList = new List<string>();
            IPOProcessBAL ipoBal = new IPOProcessBAL();

            int i = 0;
            try
            {
                while (dr.Read())
                {
                    //if (i == 9603)
                    //{
                    //    string Kamal = "kamal";
                    //}

                    string temp_1 = @"INSERT INTO [dbksclCallCenter].[dbo].[tbl_IPO_SessionApplications]
                                       ([Cust_Code]
                                       ,[IPO_Session_ID]
                                       ,[IPO_Session_Name]
                                       ,[Application_ID]
                                       ,[Status]
                                       ,[ActionDate]
                                       ,[ActionDesc]           
                                       ,[ChannelID]
                                      ,[ChannelType]
                                      ,[WebSyncFalg] )
                                 VALUES
                                       ('" + dr["Cust_Code"]+@"'
                                       ,"+dr["IPOSession_ID"]+@"
                                       ,'"+dr["IPOSession_Name"]+@"'
                                       ,"+dr["ID"]+@"
                                       ,'"+dr["Status"]+@"'
                                        ," + ((dr["ActionDate"].ToString() == string.Empty) || (dr["ActionDate"].ToString() == DateTime.MinValue.ToString()) ? "NULL" : "'" + dr["ActionDate"].ToString() + "'") + @"
                                        ,'" + dr["ActionDesc"] + @"'
                                       ," +dr["ChannelID"] +@"
                                       ,'"+dr["ChannelType"]+@"'
                                       ,0)";
                    _dbConnection.ExecuteNonQuery_SMSSender(temp_1);
                    //InsertQueryList.Add(temp_1);
                    i++;
                }
            }
            catch (Exception exception)
            {
                //_dbConnection.Rollback();
                throw exception;
            }
            finally
            {
                dr.Close();
                //_dbConnection.CloseDatabase();
            }
        }

        public void InsertTable_tbl_IPO_Charge_UITransApplied(SqlDataReader dr)
        {

            List<string> InsertQueryList = new List<string>();
            IPOProcessBAL ipoBal = new IPOProcessBAL();
            DataTable dt_RefundType = ipoBal.GetRefundMethod();

            int i = 0;
            try
            {
                while (dr.Read())
                {
                    //if (i == 9603)
                    //{
                    //    string Kamal = "kamal";
                    //}

                    string temp_1 = @"INSERT INTO [dbksclCallCenter].[dbo].[tbl_IPO_Charge]
                                           ([Ch_ID]
                                           ,[Ch_Item]
                                           ,[Ch_Rate]
                                           ,[Ch_Effective_Date])
                                   VALUES
                                   (
                                        " + dr["Ch_ID"] + @"
                                        ,'" + dr["Ch_Item"] + @"'
                                        ," + dr["Ch_Rate"] + @"
                                        ,'" + dr["Ch_Effective_Date"] + @"'                                                       
                                    )";
                    _dbConnection.ExecuteNonQuery_SMSSender(temp_1);
                    //InsertQueryList.Add(temp_1);
                    i++;
                }
            }
            catch (Exception exception)
            {
                //_dbConnection.Rollback();
                throw exception;
            }
            finally
            {
                dr.Close();
                //_dbConnection.CloseDatabase();
            }
        }

        public DataTable GetIPOEmailNotification(int SessionID)
        {
            DataTable dt = new DataTable();
            string query = string.Empty;
            query = @"  Declare @SessionID INT=" + Convert.ToString(SessionID) + @"
                        --Declare @ApplicationStatus varchar(50)='Successfull'

                        Select 
                        bs.ID as [IPOApplicationID]
                        ,bs.Cust_Code as Cust_Code
                        ,(Select t.Status_Name From SBP_IPO_Approval_Status as t Where t.ID=bs.Application_Satus) AS IPO_Status
                        ,bs.IPOSession_ID as IPO_Session_ID
                        ,bs.IPOSession_Name as IPO_Session_Name
                        ,'' as EmailType
                        ,'' as FailureReason
                        ,0 as [Status]
                        ,NULL as [DelivaryDate]
                        From SBP_IPO_Application_BasicInfo as bs
                        JOIN SBP_IPO_Application_ExtendedInfo as ext
                        ON bs.ID=ext.BasicInfo_ID
                        JOIN SBP_IPO_Approval_Status as appst
                        ON appst.ID=bs.Application_Satus
                        JOIN SBP_Service_Registration as svc
                        ON svc.Cust_Code=bs.Cust_Code
                        Where bs.IPOSession_ID=@SessionID
                        AND bs.Application_Satus IN (1,3,4)
                        AND svc.IPO_Confiramation_Email=1
                        --AND appst.Status_Name=@ApplicationStatus
                    ";
            try
            {
                //_dbConnection.ConnectDatabase();
                dt = _dbConnection.ExecuteQuery(query);

            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
            return dt;
        }

        #region
//        public DataTable GetIPOSMSNotification_Single_ApproveReject_UITransApplied(int SessionID, string AlreadyExportedIDs)
//        {
//            DataTable dt = new DataTable();
//            string query = string.Empty;
//            string ApplicationStage = "1,2";
//            query = @"  Declare @SessionID INT=" + SessionID + @"
//                        Declare @ApplicationStatus varchar(50)='" + ApplicationStage + @"'
//
//                        Select 
//                        bs.Cust_Code as Cust_Code
//                        ,'' AS [SMSType]
//                        ,bs.ID as [IPOApplicationID]
//                        ,bs.IPOSession_ID AS IPOSession_ID
//                        ,(
//	                        Select c.Company_Short_Code
//	                        From SBP_IPO_Company_Info as c
//	                        Where c.ID=	(
//						                        Select t.IPO_Company_ID 
//						                        From SBP_IPO_Session as t 
//						                        Where t.ID=bs.IPOSession_ID
//	                        )
//                        ) AS Comp_Short_Code
//                        ,(Select t.Status_Name From SBP_IPO_Approval_Status as t Where t.ID=bs.Application_Satus)+'_'+bs.IPOSession_Name  AS Trans_Reason
//                        ,'' as [Message]
//                        ,svc.Mobile_No as [Destination] 
//                        ,NULL as [DelivaryDate]
//                        ,0 as Status
//                        ,(
//                            Select t.Status_Name 
//                            From SBP_IPO_Approval_Status as t 
//                            Where t.ID=bs.Application_Satus
//                        ) AS ApplicationStatus
//                        From SBP_IPO_Application_BasicInfo as bs
//                        JOIN SBP_IPO_Application_ExtendedInfo as ext
//                        ON bs.ID=ext.BasicInfo_ID
//                        JOIN SBP_IPO_Approval_Status as appst
//                        ON appst.ID=bs.Application_Satus
//                        JOIN SBP_Service_Registration as svc
//                        ON svc.Cust_Code=bs.Cust_Code
//                        Where bs.IPOSession_ID=@SessionID
//                         AND bs.Application_Satus IN (" + ApplicationStage + @")
//                        AND svc.SMS_IPOConfiramation=1
//                        AND bs.Cust_Code NOT IN (
//                                                    Select Child_Code AS Cust_Code
//						                            From SBP_Parent_Child_Details
//                        )
//                        AND bs.ID NOT IN (
//                                            " + Convert.ToString(AlreadyExportedIDs != string.Empty ? AlreadyExportedIDs : "0") + @"
//                        )
//                        AND ISNULL(bs.ChannelID,0)<>0
//                        AND ISNULL(bs.ChannelType,'')='" + Indication_IPOPaymentTransaction.ChannelType_SMS + @"'
//
//                        UNION ALL
//
//	                    Select 
//	                    Cust_Code
//	                    ,'' AS SMSType
//	                    ,0 AS IPOApplicationID
//	                    ,IPOSessionID AS [IPOSession_ID]
//	                    ,(
//		                    Select t.Company_Short_Code 
//		                    From SBP_IPO_Company_Info as t 
//		                    Where t.ID=(
//					                    Select g.IPO_Company_ID
//					                    From SBP_IPO_Session as g
//					                    Where g.ID=del.IPOSessionID
//		                    )
//	                    ) AS Comp_Short_Code
//	                    ,(Select t.Status_Name From SBP_IPO_Approval_Status as t Where t.ID=2)+'_'+
//	                    (
//		                    Select s.IPOSession_Name
//		                    From SBP_IPO_Session as s
//		                    Where s.ID=del.IPOSessionID
//	                    )   AS Trans_Reason
//	                    ,'' AS [Message]
//	                    ,(
//		                    Select t.Hadeler_Contact_Mobile
//		                    From SBP_Parent_Child_Owner_Details as t
//		                    Where ISNULL(t.Hadeler_Contact_Mobile,'')<>''
//		                    AND t.Parent_Id=(
//							                    Select c.Parent_Code
//							                    From SBP_Parent_Child_Details as c
//							                    Where c.Child_Code=del.Cust_Code
//		                    )
//	                    ) as [Destination] 
//	                    ,NULL AS [DelivaryDate]
//	                    ,0 AS [Status]
//	                    ,(
//		                    Select s.Status_Name
//		                    From SBP_IPO_Approval_Status as s
//		                    WHere s.ID=2
//	                    ) AS [ApplicationStatus]
//	                    From dbo.SMS_Sync_Import_IPORequest_DeleteLog as del
//	                    Where del.IPOSessionID=@SessionID
//	                    AND del.Cust_Code NOT IN (
//							                    Select dtl.Child_Code
//							                    From SBP_Parent_Child_Details as dtl
//	                    )               
//                    ";
//            try
//            {
//                //_dbConnection.ConnectDatabase();
//                dt = _dbConnection.ExecuteQuery(query);

//            }
//            catch (Exception exception)
//            {
//                throw exception;
//            }
//            finally
//            {
//                //_dbConnection.CloseDatabase();
//            }
//            return dt;
//        }
        #endregion


        public DataTable GetIPOSMSNotification_Single_ApproveReject_UITransApplied(int SessionID, string AlreadyExportedIDs)
        {
            DataTable dt = new DataTable();
            string query = string.Empty;
            string ApplicationStage = "1,2";
            query = @"  Declare @SessionID INT=" + SessionID + @"
                        Declare @ApplicationStatus varchar(50)='" + ApplicationStage + @"'

                        Select 
                        bs.Cust_Code as Cust_Code
                        ,'' AS [SMSType]
                        ,bs.ID as [IPOApplicationID]
                        ,bs.IPOSession_ID AS IPOSession_ID
                        ,(
	                        Select c.Company_Short_Code
	                        From SBP_IPO_Company_Info as c
	                        Where c.ID=	(
						                        Select t.IPO_Company_ID 
						                        From SBP_IPO_Session as t 
						                        Where t.ID=bs.IPOSession_ID
	                        )
                        ) AS Comp_Short_Code
                        ,(Select t.Status_Name From SBP_IPO_Approval_Status as t Where t.ID=bs.Application_Satus)+'_'+bs.IPOSession_Name  AS Trans_Reason
                        ,'' as [Message]
                        ,svc.Mobile_No as [Destination] 
                        ,NULL as [DelivaryDate]
                        ,0 as Status
                        ,(
                            Select t.Status_Name 
                            From SBP_IPO_Approval_Status as t 
                            Where t.ID=bs.Application_Satus
                        ) AS ApplicationStatus
                        From SBP_IPO_Application_BasicInfo as bs
                        JOIN SBP_IPO_Application_ExtendedInfo as ext
                        ON bs.ID=ext.BasicInfo_ID
                        JOIN SBP_IPO_Approval_Status as appst
                        ON appst.ID=bs.Application_Satus
                        JOIN SBP_Service_Registration as svc
                        ON svc.Cust_Code=bs.Cust_Code
                        Where bs.IPOSession_ID=@SessionID
                         AND bs.Application_Satus IN (" + ApplicationStage + @")
                        AND svc.SMS_IPOConfiramation=1
                        AND bs.Cust_Code NOT IN (
                                                    Select Child_Code AS Cust_Code
						                            From SBP_Parent_Child_Details
                        )
                        AND bs.ID NOT IN (
                                            " + Convert.ToString(AlreadyExportedIDs != string.Empty ? AlreadyExportedIDs : "0") + @"
                        )
                        AND ISNULL(bs.ChannelID,0)<>0
                        AND ISNULL(bs.ChannelType,'')='" + Indication_IPOPaymentTransaction.ChannelType_SMS + @"'
                       -- AND (dbo.SBP_Service_Registration.Mobile_No IS NOT NULL)
                       
                          UNION ALL

	                    Select 
	                    Cust_Code
	                    ,'' AS SMSType
	                    ,0 AS IPOApplicationID
	                    ,IPOSessionID AS [IPOSession_ID]
	                    ,(
		                    Select t.Company_Short_Code 
		                    From SBP_IPO_Company_Info as t 
		                    Where t.ID=(
					                    Select g.IPO_Company_ID
					                    From SBP_IPO_Session as g
					                    Where g.ID=del.IPOSessionID
		                    )
	                    ) AS Comp_Short_Code
	                    ,(Select t.Status_Name From SBP_IPO_Approval_Status as t Where t.ID=2)+'_'+
	                    (
		                    Select s.IPOSession_Name
		                    From SBP_IPO_Session as s
		                    Where s.ID=del.IPOSessionID
	                    )   AS Trans_Reason
	                    ,'' AS [Message]
	                    ,(
		                    Select t.Hadeler_Contact_Mobile
		                    From SBP_Parent_Child_Owner_Details as t
		                    Where ISNULL(t.Hadeler_Contact_Mobile,'')<>''
		                    AND t.Parent_Id=(
							                    Select c.Parent_Code
							                    From SBP_Parent_Child_Details as c
							                    Where c.Child_Code=del.Cust_Code
		                    )
	                    ) as [Destination] 
	                    ,NULL AS [DelivaryDate]
	                    ,0 AS [Status]
	                    ,(
		                    Select s.Status_Name
		                    From SBP_IPO_Approval_Status as s
		                    WHere s.ID=2
	                    ) AS [ApplicationStatus]
	                    From dbo.SMS_Sync_Import_IPORequest_DeleteLog as del
	                    Where del.IPOSessionID=@SessionID
	                    AND del.Cust_Code NOT IN (
							                    Select dtl.Child_Code
							                    From SBP_Parent_Child_Details as dtl
	                    )               
                    ";
            try
            {
                //_dbConnection.ConnectDatabase();
                dt = _dbConnection.ExecuteQuery(query);

            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
            return dt;
        }


        #region
        //        public DataTable GetIPOSMSNotification_Single_ApproveReject(int SessionID, string AlreadyExportedIDs)
//        {
//            DataTable dt = new DataTable();
//            string query = string.Empty;
//            string ApplicationStage = "1,2";

//            query = @"  Declare @SessionID INT=" + SessionID + @"
//                                Declare @ApplicationStatus varchar(50)='" + ApplicationStage + @"'
//         
//                                        SELECT bs.Cust_Code,
//                                         'ApplicationSMSNotification_SMSType' AS SMSType,
//                                          bs.ID AS IPOApplicationID,
//                                          bs.IPOSession_ID,
//                                          
//                                              (SELECT Company_Short_Code
//							                                     FROM   SBP_Database.dbo.SBP_IPO_Company_Info AS c
//								                                       WHERE (ID =
//												                                    (SELECT IPO_Company_ID
//														                                     FROM   SBP_Database.dbo.SBP_IPO_Session AS t
//															                                     WHERE (ID = bs.IPOSession_ID)
//												                                     )
//										                                      )
//		                                    ) AS Comp_Short_Code,
//                                                       (SELECT Status_Name
//							                                    FROM   SBP_Database.dbo.SBP_IPO_Approval_Status AS t
//								                                     WHERE (ID = bs.Application_Satus)) + '_' + bs.IPOSession_Name AS Trans_Reason, '' AS Message, NULL AS DelivaryDate, 0 AS Status,
//                                    							
//                                                       (SELECT Status_Name
//							                                    FROM   SBP_Database.dbo.SBP_IPO_Approval_Status AS t
//								                                     WHERE (ID = bs.Application_Satus)
//					                                    ) AS ApplicationStatus,
//	                                       SBP_Database.dbo.SBP_Service_Registration.Mobile_No AS Destination,
//	                                       bs.ChannelType
//                                    FROM  SBP_Database.dbo.SBP_IPO_Application_BasicInfo AS bs INNER JOIN
//                                          SBP_Database.dbo.SBP_IPO_Application_ExtendedInfo AS ext ON bs.ID = ext.BasicInfo_ID INNER JOIN
//                                          SBP_Database.dbo.SBP_Service_Registration ON bs.Cust_Code = SBP_Database.dbo.SBP_Service_Registration.Cust_Code INNER JOIN
//                                          SBP_Database.dbo.SBP_IPO_Approval_Status AS appst ON appst.ID = bs.Application_Satus INNER JOIN
//                                          SBP_Database.dbo.SBP_Service_Registration AS svc ON svc.Cust_Code = bs.Cust_Code
//                                          
//                                    WHERE (bs.Application_Satus IN (" + ApplicationStage + @")) 
//                                              AND (svc.SMS_IPOConfiramation = 1)  
//                                              AND (bs.ChannelID <> 0) 
//                                              AND (bs.ChannelType = 'SMS') 
//                                              AND (bs.IPOSession_ID = " + SessionID + @") 
//                                              AND ( (bs.Cust_Code IN
//                                                          (
//                                                            SELECT Child_Code AS Cust_Code
//                                                            FROM   SBP_Database.dbo.SBP_Parent_Child_Details AS SBP_Parent_Child_Details
//                                                           )
//                                                          )
//                                                        )
//                                             AND ( not 
//                                                (
//                                                  bs.Cust_Code IN  
//                                                       (" + Convert.ToString(AlreadyExportedIDs != string.Empty ? AlreadyExportedIDs : "0") + @")
//                                                 )
//                                               )
//                                             -- AND (dbo.SBP_Service_Registration.Mobile_No IS NOT NULL)
//
//                    ";
//            //            query = @"  Declare @SessionID INT=" + SessionID + @"
//            //                        Declare @ApplicationStatus varchar(50)='" + ApplicationStage + @"'
//            //
//            //                        Select 
//            //                        bs.Cust_Code as Cust_Code
//            //                        ,'' AS [SMSType]
//            //                        ,bs.ID as [IPOApplicationID]
//            //                        ,bs.IPOSession_ID AS IPOSession_ID
//            //                        ,(
//            //	                        Select c.Company_Short_Code
//            //	                        From SBP_IPO_Company_Info as c
//            //	                        Where c.ID=	(
//            //						                        Select t.IPO_Company_ID 
//            //						                        From SBP_IPO_Session as t 
//            //						                        Where t.ID=bs.IPOSession_ID
//            //	                        )
//            //                        ) AS Comp_Short_Code
//            //                        ,(Select t.Status_Name From SBP_IPO_Approval_Status as t Where t.ID=bs.Application_Satus)+'_'+bs.IPOSession_Name  AS Trans_Reason
//            //                        ,'' as [Message]
//            //                        ,svc.Mobile_No as [Destination] 
//            //                        ,NULL as [DelivaryDate]
//            //                        ,0 as Status
//            //                        ,(
//            //                            Select t.Status_Name 
//            //                            From SBP_IPO_Approval_Status as t 
//            //                            Where t.ID=bs.Application_Satus
//            //                        ) AS ApplicationStatus
//            //                        From SBP_IPO_Application_BasicInfo as bs
//            //                        JOIN SBP_IPO_Application_ExtendedInfo as ext
//            //                        ON bs.ID=ext.BasicInfo_ID
//            //                        JOIN SBP_IPO_Approval_Status as appst
//            //                        ON appst.ID=bs.Application_Satus
//            //                        JOIN SBP_Service_Registration as svc
//            //                        ON svc.Cust_Code=bs.Cust_Code
//            //                        Where bs.IPOSession_ID=@SessionID
//            //                          AND bs.Application_Satus IN (" + ApplicationStage + @")
//            //                        AND svc.SMS_IPOConfiramation=1
//            //                        AND bs.Cust_Code NOT IN (
//            //                                                    Select Child_Code AS Cust_Code
//            //						                            From SBP_Parent_Child_Details
//            //                        )
//            //                        AND bs.ID NOT IN (
//            //                                            " + Convert.ToString(AlreadyExportedIDs != string.Empty ? AlreadyExportedIDs : "0") + @"
//            //                        )
//            //                        AND ISNULL(bs.ChannelID,0)<>0
//            //                        AND ISNULL(bs.ChannelType,'')='" + Indication_IPOPaymentTransaction.ChannelType_SMS + @"'
//            //
//            //                        UNION ALL
//            //
//            //	                    Select 
//            //	                    del.Cust_Code
//            //	                    ,'' AS SMSType
//            //	                    ,0 AS IPOApplicationID
//            //	                    ,IPOSessionID AS [IPOSession_ID]
//            //	                    ,(
//            //		                    Select t.Company_Short_Code 
//            //		                    From SBP_IPO_Company_Info as t 
//            //		                    Where t.ID=(
//            //					                    Select g.IPO_Company_ID
//            //					                    From SBP_IPO_Session as g
//            //					                    Where g.ID=del.IPOSessionID
//            //		                    )
//            //	                    ) AS Comp_Short_Code
//            //	                    ,(Select t.Status_Name From SBP_IPO_Approval_Status as t Where t.ID=2)+'_'+
//            //	                    (
//            //		                    Select s.IPOSession_Name
//            //		                    From SBP_IPO_Session as s
//            //		                    Where s.ID=del.IPOSessionID
//            //	                    )   AS Trans_Reason
//            //	                    ,'' AS [Message]
//            //	                    ,svc.Mobile_No as [Destination] 
//            //	                    ,NULL AS [DelivaryDate]
//            //	                    ,0 AS [Status]
//            //	                    ,(
//            //		                    Select s.Status_Name
//            //		                    From SBP_IPO_Approval_Status as s
//            //		                    WHere s.ID=2
//            //	                    ) AS [ApplicationStatus]
//            //	                    From dbo.SMS_Sync_Import_IPORequest_DeleteLog as del
//            //                        JOIN SBP_Service_Registration as svc
//            //                        ON svc.Cust_Code=del.Cust_Code
//            //	                    Where del.IPOSessionID=@SessionID
//            //                        AND svc.SMS_IPOConfiramation=1
//            //	                    AND del.Cust_Code NOT IN (
//            //							                    Select dtl.Child_Code
//            //							                    From SBP_Parent_Child_Details as dtl
//            //	                    )
//            //
//            //                    ";
//            try
//            {
//                _dbConnection.ConnectDatabase();
//                dt = _dbConnection.ExecuteQuery(query);

//            }
//            catch (Exception exception)
//            {
//                throw exception;
//            }
//            finally
//            {
//                _dbConnection.CloseDatabase();
//            }
//            return dt;
//        }
        #endregion

        public DataTable GetIPOSMSNotification_Single_ApproveReject(int SessionID, string AlreadyExportedIDs)
        {
            DataTable dt = new DataTable();
            string query = string.Empty;
            string ApplicationStage = "1,2";

            query = @"  Declare @SessionID INT=" + SessionID + @"
                                Declare @ApplicationStatus varchar(50)='" + ApplicationStage + @"'
         
                                        SELECT bs.Cust_Code,
                                         'ApplicationSMSNotification_SMSType' AS SMSType,
                                          bs.ID AS IPOApplicationID,
                                          bs.IPOSession_ID,
                                          
                                              (SELECT Company_Short_Code
							                                     FROM   SBP_Database.dbo.SBP_IPO_Company_Info AS c
								                                       WHERE (ID =
												                                    (SELECT IPO_Company_ID
														                                     FROM   SBP_Database.dbo.SBP_IPO_Session AS t
															                                     WHERE (ID = bs.IPOSession_ID)
												                                     )
										                                      )
		                                    ) AS Comp_Short_Code,
                                                       (SELECT Status_Name
							                                    FROM   SBP_Database.dbo.SBP_IPO_Approval_Status AS t
								                                     WHERE (ID = bs.Application_Satus)) + '_' + bs.IPOSession_Name AS Trans_Reason, '' AS Message, NULL AS DelivaryDate, 0 AS Status,
                                    							
                                                       (SELECT Status_Name
							                                    FROM   SBP_Database.dbo.SBP_IPO_Approval_Status AS t
								                                     WHERE (ID = bs.Application_Satus)
					                                    ) AS ApplicationStatus,
	                                       SBP_Database.dbo.SBP_Service_Registration.Mobile_No AS Destination,
	                                       bs.ChannelType
                                    FROM  SBP_Database.dbo.SBP_IPO_Application_BasicInfo AS bs INNER JOIN
                                          SBP_Database.dbo.SBP_IPO_Application_ExtendedInfo AS ext ON bs.ID = ext.BasicInfo_ID INNER JOIN
                                          SBP_Database.dbo.SBP_Service_Registration ON bs.Cust_Code = SBP_Database.dbo.SBP_Service_Registration.Cust_Code INNER JOIN
                                          SBP_Database.dbo.SBP_IPO_Approval_Status AS appst ON appst.ID = bs.Application_Satus INNER JOIN
                                          SBP_Database.dbo.SBP_Service_Registration AS svc ON svc.Cust_Code = bs.Cust_Code
                                          
                                    WHERE (bs.Application_Satus IN (" + ApplicationStage + @")) 
                                              AND (svc.SMS_IPOConfiramation = 1)  
                                              AND (bs.ChannelID <> 0) 
                                              AND (bs.ChannelType = 'SMS') 
                                              AND (bs.IPOSession_ID = " + SessionID + @") 
                                              AND ( (bs.Cust_Code IN
                                                          (
                                                            SELECT Child_Code AS Cust_Code
                                                            FROM   SBP_Database.dbo.SBP_Parent_Child_Details AS SBP_Parent_Child_Details
                                                           )
                                                          )
                                                        )
                                             AND ( not 
                                                (
                                                  bs.Cust_Code IN  
                                                       (" + Convert.ToString(AlreadyExportedIDs != string.Empty ? AlreadyExportedIDs : "0") + @")
                                                 )
                                               )
                                              AND (dbo.SBP_Service_Registration.Mobile_No IS NOT NULL)

                    ";

            try
            {
                _dbConnection.ConnectDatabase();
                dt = _dbConnection.ExecuteQuery(query);

            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                _dbConnection.CloseDatabase();
            }
            return dt;
        }


        public DataTable GetIPOSMSNotification_Single_SuccUnsucc_UITransApplied(int SessionID, string AlreadyExportedIDs)
        {
            DataTable dt = new DataTable();
            string query = string.Empty;
            string ApplicationStage = "3,4";
            query = @"  Declare @SessionID INT=" + SessionID + @"
                        Declare @ApplicationStatus varchar(50)='" + ApplicationStage + @"'

                        Select 
                        bs.Cust_Code as Cust_Code
                        ,'" + Indication_IPOPaymentTransaction.ApplicationSMSNotification_SMSType + @"' AS SMSType
                        ,bs.ID as [IPOApplicationID]
                        ,bs.IPOSession_ID AS IPOSession_ID
                        ,(
	                        Select c.Company_Short_Code
	                        From SBP_IPO_Company_Info as c 
	                        Where c.ID=	(
						                        Select t.IPO_Company_ID 
						                        From SBP_IPO_Session as t 
						                        Where t.ID=bs.IPOSession_ID
	                        )
                        ) AS Comp_Short_Code
                        ,(Select t.Status_Name From SBP_IPO_Approval_Status as t Where t.ID=bs.Application_Satus)+'_'+bs.IPOSession_Name  AS Trans_Reason
                        ,'' as [Message]
                        ,svc.Mobile_No as [Destination] 
                        ,NULL as [DelivaryDate]
                        ,0 as Status
                         ,(
                            Select t.Status_Name 
                            From SBP_IPO_Approval_Status as t 
                            Where t.ID=bs.Application_Satus
                        ) AS ApplicationStatus
                        From SBP_IPO_Application_BasicInfo as bs
                        JOIN SBP_IPO_Application_ExtendedInfo as ext
                        ON bs.ID=ext.BasicInfo_ID
                        JOIN SBP_IPO_Approval_Status as appst
                        ON appst.ID=bs.Application_Satus
                        JOIN SBP_Service_Registration as svc
                        ON svc.Cust_Code=bs.Cust_Code
                        Where bs.IPOSession_ID=@SessionID
                          AND bs.Application_Satus IN (" + ApplicationStage + @")
                        AND svc.SMS_IPOConfiramation=1
                        AND bs.Cust_Code NOT IN (
                                                    Select Child_Code AS Cust_Code
						                            From SBP_Parent_Child_Details
                        )
                        AND bs.ID NOT IN (
                                            " + Convert.ToString(AlreadyExportedIDs != string.Empty ? AlreadyExportedIDs : "0") + @"
                        )
                        AND ISNULL(bs.ChannelID,0)<>0
                        AND ISNULL(bs.ChannelType,'')='" + Indication_IPOPaymentTransaction.ChannelType_SMS + @"'               
                    ";
            try
            {
                //_dbConnection.ConnectDatabase();
                dt = _dbConnection.ExecuteQuery(query);

            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
            return dt;
        }

        #region
//        public DataTable GetIPOSMSNotification_Single_SuccUnsucc(int SessionID, string AlreadyExportedIDs)
//        {
//            DataTable dt = new DataTable();
//            string query = string.Empty;
//            string ApplicationStage = "3,4";


//            query = @"  Declare @SessionID INT=" + SessionID + @" Declare @ApplicationStatus varchar(50)='" + ApplicationStage + @"'
//         
//                                        SELECT bs.Cust_Code,
//                                         '" + Indication_IPOPaymentTransaction.ApplicationSMSNotification_SMSType + @"' AS SMSType,
//                                          bs.ID AS IPOApplicationID,
//                                          bs.IPOSession_ID,
//                                          
//                                              (SELECT Company_Short_Code
//							                                     FROM   SBP_Database.dbo.SBP_IPO_Company_Info AS c
//								                                       WHERE (ID =
//												                                    (SELECT IPO_Company_ID
//														                                     FROM   SBP_Database.dbo.SBP_IPO_Session AS t
//															                                     WHERE (ID = bs.IPOSession_ID)
//												                                     )
//										                                      )
//		                                    ) AS Comp_Short_Code,
//                                                       (SELECT Status_Name
//							                                    FROM   SBP_Database.dbo.SBP_IPO_Approval_Status AS t
//								                                     WHERE (ID = bs.Application_Satus)) + '_' + bs.IPOSession_Name AS Trans_Reason, '' AS Message, NULL AS DelivaryDate, 0 AS Status,
//                                    							
//                                                       (SELECT Status_Name
//							                                    FROM   SBP_Database.dbo.SBP_IPO_Approval_Status AS t
//								                                     WHERE (ID = bs.Application_Satus)
//					                                    ) AS ApplicationStatus,
//	                                       SBP_Database.dbo.SBP_Service_Registration.Mobile_No AS Destination,
//	                                       bs.ChannelType
//                                    FROM  SBP_Database.dbo.SBP_IPO_Application_BasicInfo AS bs INNER JOIN
//                                          SBP_Database.dbo.SBP_IPO_Application_ExtendedInfo AS ext ON bs.ID = ext.BasicInfo_ID INNER JOIN
//                                          SBP_Database.dbo.SBP_Service_Registration ON bs.Cust_Code = SBP_Database.dbo.SBP_Service_Registration.Cust_Code INNER JOIN
//                                          SBP_Database.dbo.SBP_IPO_Approval_Status AS appst ON appst.ID = bs.Application_Satus INNER JOIN
//                                          SBP_Database.dbo.SBP_Service_Registration AS svc ON svc.Cust_Code = bs.Cust_Code
//                                          
//                                    WHERE (bs.Application_Satus IN (" + ApplicationStage + @")) 
//                                              AND (svc.SMS_IPOConfiramation = 1)  
//                                              AND (bs.ChannelID <> 0) 
//                                              AND (bs.ChannelType = 'SMS') 
//                                              AND (bs.IPOSession_ID = " + SessionID + @") 
//                                              AND (dbo.SBP_Service_Registration.Mobile_No IS NOT NULL)
//                                              AND ( (bs.Cust_Code NOT IN 
//                                                          (
//                                                            SELECT Child_Code AS Cust_Code
//                                                            FROM   SBP_Database.dbo.SBP_Parent_Child_Details AS SBP_Parent_Child_Details
//                                                            
//                                                           )
//                                                          )
//                                                        )
//                                             AND ( not 
//                                                (
//                                                  bs.ID IN  
//                                                       (" + Convert.ToString(AlreadyExportedIDs != string.Empty ? AlreadyExportedIDs : "0") + @")
//                                                 )
//                                               )
//                                            AND (dbo.SBP_Service_Registration.Mobile_No IS NOT NULL)
//                        ";

//            //            query = @"  Declare @SessionID INT=" + SessionID + @"
//            //                        Declare @ApplicationStatus varchar(50)='" + ApplicationStage + @"'
//            //
//            //                        Select 
//            //                        bs.Cust_Code as Cust_Code
//            //                        ,'" + Indication_IPOPaymentTransaction.ApplicationSMSNotification_SMSType + @"' AS SMSType
//            //                        ,bs.ID as [IPOApplicationID]
//            //                        ,bs.IPOSession_ID AS IPOSession_ID
//            //                        ,(
//            //	                        Select c.Company_Short_Code
//            //	                        From SBP_IPO_Company_Info as c 
//            //	                        Where c.ID=	(
//            //						                        Select t.IPO_Company_ID 
//            //						                        From SBP_IPO_Session as t 
//            //						                        Where t.ID=bs.IPOSession_ID
//            //	                        )
//            //                        ) AS Comp_Short_Code
//            //                        ,(Select t.Status_Name From SBP_IPO_Approval_Status as t Where t.ID=bs.Application_Satus)+'_'+bs.IPOSession_Name  AS Trans_Reason
//            //                        ,'' as [Message]
//            //                        ,svc.Mobile_No as [Destination] 
//            //                        ,NULL as [DelivaryDate]
//            //                        ,0 as Status
//            //                        ,(
//            //                            Select t.Status_Name 
//            //                            From SBP_IPO_Approval_Status as t 
//            //                            Where t.ID=bs.Application_Satus
//            //                        ) AS ApplicationStatus
//            //                        From SBP_IPO_Application_BasicInfo as bs
//            //                        JOIN SBP_IPO_Application_ExtendedInfo as ext
//            //                        ON bs.ID=ext.BasicInfo_ID
//            //                        JOIN SBP_IPO_Approval_Status as appst
//            //                        ON appst.ID=bs.Application_Satus
//            //                        JOIN SBP_Service_Registration as svc
//            //                        ON svc.Cust_Code=bs.Cust_Code
//            //                        Where bs.IPOSession_ID=@SessionID
//            //                        AND bs.Application_Satus IN (" + ApplicationStage + @")
//            //                        AND svc.SMS_IPOConfiramation=1
//            //                        AND bs.Cust_Code NOT IN (
//            //                                                    Select Child_Code AS Cust_Code
//            //						                            From SBP_Parent_Child_Details
//            //                        )
//            //                        AND bs.ID NOT IN ( " + Convert.ToString(AlreadyExportedIDs != string.Empty ? AlreadyExportedIDs : "0") + @" )
//            //                        AND ISNULL(bs.ChannelID,0)<>0
//            //                        AND ISNULL(bs.ChannelType,'')='" + Indication_IPOPaymentTransaction.ChannelType_SMS + @"'
//            //                    ";

//            try
//            {
//                _dbConnection.ConnectDatabase();
//                dt = _dbConnection.ExecuteQuery(query);

//            }
//            catch (Exception exception)
//            {
//                throw exception;
//            }
//            finally
//            {
//                _dbConnection.CloseDatabase();
//            }
//            return dt;
//        }
        #endregion


        #region
//        public DataTable GetIPOSMSNotification_Single_SuccUnsucc(int SessionID, string AlreadyExportedIDs)
//        {
//            DataTable dt = new DataTable();
//            string query = string.Empty;
//            string ApplicationStage = "3,4";


//            query = @" Declare @SessionID INT=" + SessionID + @" Declare @ApplicationStatus varchar(50)='" + ApplicationStage + @"'
//         
//                                        SELECT bs.Cust_Code,
//                                         '" + Indication_IPOPaymentTransaction.ApplicationSMSNotification_SMSType + @"' AS SMSType,
//                                          bs.ID AS IPOApplicationID,
//                                          bs.IPOSession_ID,
//                                          
//                                              (SELECT Company_Short_Code
//							                                     FROM   SBP_Database.dbo.SBP_IPO_Company_Info AS c
//								                                       WHERE (ID =
//												                                    (SELECT IPO_Company_ID
//														                                     FROM   SBP_Database.dbo.SBP_IPO_Session AS t
//															                                     WHERE (ID = bs.IPOSession_ID)
//												                                     )
//										                                      )
//		                                    ) AS Comp_Short_Code,
//                                                       (SELECT Status_Name
//							                                    FROM   SBP_Database.dbo.SBP_IPO_Approval_Status AS t
//								                                     WHERE (ID = bs.Application_Satus)) + '_' + bs.IPOSession_Name AS Trans_Reason, '' AS Message, NULL AS DelivaryDate, 0 AS Status,
//                                    							
//                                                       (SELECT Status_Name
//							                                    FROM   SBP_Database.dbo.SBP_IPO_Approval_Status AS t
//								                                     WHERE (ID = bs.Application_Satus)
//					                                    ) AS ApplicationStatus,
//	                                       SBP_Database.dbo.SBP_Service_Registration.Mobile_No AS Destination,
//	                                       bs.ChannelType
//                                    FROM  SBP_Database.dbo.SBP_IPO_Application_BasicInfo AS bs INNER JOIN
//                                          SBP_Database.dbo.SBP_IPO_Application_ExtendedInfo AS ext ON bs.ID = ext.BasicInfo_ID INNER JOIN
//                                          SBP_Database.dbo.SBP_Service_Registration ON bs.Cust_Code = SBP_Database.dbo.SBP_Service_Registration.Cust_Code INNER JOIN
//                                          SBP_Database.dbo.SBP_IPO_Approval_Status AS appst ON appst.ID = bs.Application_Satus INNER JOIN
//                                          SBP_Database.dbo.SBP_Service_Registration AS svc ON svc.Cust_Code = bs.Cust_Code
//                                          
//                                    WHERE (bs.Application_Satus IN (" + ApplicationStage + @")) 
//                                              AND (svc.SMS_IPOConfiramation = 1)  
//                                              AND (bs.ChannelID <> 0) 
//                                              AND (bs.ChannelType = 'SMS') 
//                                              AND (bs.IPOSession_ID = " + SessionID + @") 
//                                              AND (dbo.SBP_Service_Registration.Mobile_No IS NOT NULL)
//                                              AND ( (bs.Cust_Code NOT IN 
//                                                          (
//                                                            SELECT Child_Code AS Cust_Code
//                                                            FROM   SBP_Database.dbo.SBP_Parent_Child_Details AS SBP_Parent_Child_Details
//                                                            
//                                                           )
//                                                          )
//                                                        )
//                                             AND ( not 
//                                                (
//                                                  bs.ID IN  
//                                                       (" + Convert.ToString(AlreadyExportedIDs != string.Empty ? AlreadyExportedIDs : "0") + @")
//                                                 )
//                                               )
//                                            AND (dbo.SBP_Service_Registration.Mobile_No IS NOT NULL)
//                        ";


//            try
//            {
//                _dbConnection.ConnectDatabase();
//                dt = _dbConnection.ExecuteQuery(query);

//            }
//            catch (Exception exception)
//            {
//                throw exception;
//            }
//            finally
//            {
//                _dbConnection.CloseDatabase();
//            }
//            return dt;
//        }
        #endregion

        public DataTable GetIPOSMSNotification_Single_SuccUnsucc(int SessionID, string AlreadyExportedIDs)
        {
            DataTable dt = new DataTable();
            string query = string.Empty;
            string ApplicationStage = "3,4";


            query = @" Declare @SessionID INT=" + SessionID + @" Declare @ApplicationStatus varchar(50)='" + ApplicationStage + @"'
         
                                        SELECT bs.Cust_Code,
                                         '" + Indication_IPOPaymentTransaction.ApplicationSMSNotification_SMSType + @"' AS SMSType,
                                          bs.ID AS IPOApplicationID,
                                          bs.IPOSession_ID,
                                          
                                              (SELECT Company_Short_Code
							                                     FROM   SBP_Database.dbo.SBP_IPO_Company_Info AS c
								                                       WHERE (ID =
												                                    (SELECT IPO_Company_ID
														                                     FROM   SBP_Database.dbo.SBP_IPO_Session AS t
															                                     WHERE (ID = bs.IPOSession_ID)
												                                     )
										                                      )
		                                    ) AS Comp_Short_Code,
                                                       (SELECT Status_Name
							                                    FROM   SBP_Database.dbo.SBP_IPO_Approval_Status AS t
								                                     WHERE (ID = bs.Application_Satus)) + '_' + bs.IPOSession_Name AS Trans_Reason, '' AS Message, NULL AS DelivaryDate, 0 AS Status,
                                    							
                                                       (SELECT Status_Name
							                                    FROM   SBP_Database.dbo.SBP_IPO_Approval_Status AS t
								                                     WHERE (ID = bs.Application_Satus)
					                                    ) AS ApplicationStatus,
	                                       SBP_Database.dbo.SBP_Service_Registration.Mobile_No AS Destination,
	                                       bs.ChannelType
                                    FROM  SBP_Database.dbo.SBP_IPO_Application_BasicInfo AS bs INNER JOIN
                                          SBP_Database.dbo.SBP_IPO_Application_ExtendedInfo AS ext ON bs.ID = ext.BasicInfo_ID INNER JOIN
                                          SBP_Database.dbo.SBP_Service_Registration ON bs.Cust_Code = SBP_Database.dbo.SBP_Service_Registration.Cust_Code INNER JOIN
                                          SBP_Database.dbo.SBP_IPO_Approval_Status AS appst ON appst.ID = bs.Application_Satus INNER JOIN
                                          SBP_Database.dbo.SBP_Service_Registration AS svc ON svc.Cust_Code = bs.Cust_Code
                                          
                                    WHERE (bs.Application_Satus IN (" + ApplicationStage + @")) 
                                              AND (svc.SMS_IPOConfiramation = 1)  
                                              AND (bs.ChannelID <> 0) 
                                              AND (bs.ChannelType = 'SMS') 
                                              AND (bs.IPOSession_ID = " + SessionID + @") 
                                              AND (dbo.SBP_Service_Registration.Mobile_No IS NOT NULL)
                                              AND ( (bs.Cust_Code NOT IN 
                                                          (
                                                            SELECT Child_Code AS Cust_Code
                                                            FROM   SBP_Database.dbo.SBP_Parent_Child_Details AS SBP_Parent_Child_Details
                                                            
                                                           )
                                                          )
                                                        )
                                             AND ( not 
                                                (
                                                  bs.ID IN  
                                                       (" + Convert.ToString(AlreadyExportedIDs != string.Empty ? AlreadyExportedIDs : "0") + @")
                                                 )
                                               )
                                            AND (dbo.SBP_Service_Registration.Mobile_No IS NOT NULL)
                        ";


            try
            {
                _dbConnection.ConnectDatabase();
                dt = _dbConnection.ExecuteQuery(query);

            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                _dbConnection.CloseDatabase();
            }
            return dt;
        }




        #region
        //        public string GetAlreadyExportedNotifications_ForParentChild_UITransApplied(int SessionID)
//        {
//            string ExportedIDs = string.Empty;
//            string query = string.Empty;
//            query = @"  
//                        SET ARITHABORT  ON  
//                        SELECT STUFF((
//                            SELECT ',' + Convert(Varchar(150),ISNULL([ReferenceID],0))
//                            FROM [tbl_IPO_Confirmation_SMS] as g
//                            WHERE g.SMSType=bs.SMSType AND IPO_Session_ID=" + SessionID + @" AND IsParentChild=1
//                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '') AS ExportedIDs
//                        FROM [dbksclCallCenter].[dbo].[tbl_IPO_Confirmation_SMS] AS bs
//                        WHERE SMSType='" + Indication_IPOPaymentTransaction.ApplicationSMSNotification_SMSType + @"'
//                        AND IPO_Session_ID=" + SessionID + @"
//                        AND IsParentChild=1
//                        --WHERE SMSType='IPO_Query'
//                        Group By SMSType
//                        SET ARITHABORT  OFF 
//                    ";
//            try
//            {
//                //_dbConnection.ConnectDatabase();
//                DataTable dt = _dbConnection.ExecuteQuery_SMSSender(query);
//                if (dt.Rows.Count == 1)
//                    ExportedIDs = dt.Rows[0][0].ToString();
//            }
//            catch (Exception exception)
//            {
//                throw exception;
//            }
//            finally
//            {
//                //_dbConnection.CloseDatabase();
//            }
//            return ExportedIDs;

//        }
        #endregion

        public string GetAlreadyExportedNotifications_ForParentChild_UITransApplied(int SessionID)
        {
            string ExportedIDs = string.Empty;
            string query = string.Empty;
            query = @"  
                         SET ARITHABORT  ON  
                                    SELECT STUFF((
                                        SELECT ',' + Convert(Varchar(150),ISNULL([ReferenceID],0))
                                          FROM [tbl_IPO_Confirmation_SMS] as g
                                            WHERE g.SMSType=bs.SMSType AND IPO_Session_ID=" + SessionID + @" AND (Status <> N'0')
                                         FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '') AS ExportedIDs
                                    FROM [dbksclCallCenter].[dbo].[tbl_IPO_Confirmation_SMS]AS bs
                                    WHERE SMSType='" + Indication_IPOPaymentTransaction.ApplicationSMSNotification_SMSType + @"'
                                    AND IPO_Session_ID=" + SessionID + @"
                                    --AND IsParentChild=1
                                    --WHERE SMSType='IPO_Query'
                                    Group By SMSType
                                    SET ARITHABORT  OFF 
                     ";
            try
            {
                //_dbConnection.ConnectDatabase();
                DataTable dt = _dbConnection.ExecuteQuery_SMSSender(query);
                if (dt.Rows.Count == 1)
                    ExportedIDs = dt.Rows[0][0].ToString();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
            return ExportedIDs;

        }

        public string GetAlreadyExportedNotifications_ForParentChild(int SessionID)
        {
            string ExportedIDs = string.Empty;
            string query = string.Empty;

            query = @"  
                         SET ARITHABORT  ON  
                                    SELECT STUFF((
                                        SELECT ',' + Convert(Varchar(150),ISNULL([ReferenceID],0))
                                          FROM [tbl_IPO_Confirmation_SMS] as g
                                            WHERE g.SMSType=bs.SMSType AND IPO_Session_ID=" + SessionID + @" AND (Status <> N'0')
                                         FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '') AS ExportedIDs
                                    FROM [dbksclCallCenter].[dbo].[tbl_IPO_Confirmation_SMS]AS bs
                                    WHERE SMSType='" + Indication_IPOPaymentTransaction.ApplicationSMSNotification_SMSType + @"'
                                    AND IPO_Session_ID=" + SessionID + @"
                                    --AND IsParentChild=1
                                    --WHERE SMSType='IPO_Query'
                                    Group By SMSType
                                    SET ARITHABORT  OFF 
                    
                     ";

            //            query = @"  
            //                        SET ARITHABORT  ON  
            //                        SELECT STUFF((
            //                            SELECT ',' + Convert(Varchar(150),ISNULL([ReferenceID],0))
            //                            FROM [tbl_IPO_Confirmation_SMS] as g
            //                            WHERE g.SMSType=bs.SMSType AND IPO_Session_ID=" + SessionID + @"
            //                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '') AS ExportedIDs
            //                        FROM [dbksclCallCenter].[dbo].[tbl_IPO_Confirmation_SMS] AS bs
            //                        WHERE SMSType='" + Indication_IPOPaymentTransaction.ApplicationSMSNotification_SMSType + @"'
            //                        AND IPO_Session_ID=" + SessionID + @"
            //                        AND IsParentChild=0
            //                        --WHERE SMSType='IPO_Query'
            //                        Group By SMSType
            //                        SET ARITHABORT  OFF 
            //                    ";

            //            query = @"  
            //                        SET ARITHABORT  ON  
            //                        SELECT STUFF((
            //                            SELECT ',' + Convert(Varchar(150),ISNULL([ReferenceID],0))
            //                            FROM [tbl_IPO_Confirmation_SMS] as g
            //                            WHERE g.SMSType=bs.SMSType AND IPO_Session_ID=" + SessionID + @" AND IsParentChild=1
            //                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '') AS ExportedIDs
            //                        FROM [dbksclCallCenter].[dbo].[tbl_IPO_Confirmation_SMS] AS bs
            //                        WHERE SMSType='" + Indication_IPOPaymentTransaction.ApplicationSMSNotification_SMSType + @"'
            //                        AND IPO_Session_ID=" + SessionID + @"
            //                        AND IsParentChild=1
            //                        --WHERE SMSType='IPO_Query'
            //                        Group By SMSType
            //                        SET ARITHABORT  OFF 
            //                    ";
            try
            {
                _dbConnection.ConnectDatabase_SMSSender();
                DataTable dt = _dbConnection.ExecuteQuery_SMSSender(query);
                if (dt.Rows.Count == 1)
                    ExportedIDs = dt.Rows[0][0].ToString();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                _dbConnection.CloseDatabase_SMSSender();
            }
            return ExportedIDs;

        }

        public string GetAlreadyExportedNotifications_ForSingle_UITransApplied(int SessionID)
        {
            string ExportedIDs = string.Empty;
            string query = string.Empty;
            query = @"  
                        SET ARITHABORT  ON  
                        SELECT STUFF((
                            SELECT ',' + Convert(Varchar(150),ISNULL([ReferenceID],0))
                            FROM [tbl_IPO_Confirmation_SMS] as g
                            WHERE g.SMSType=bs.SMSType AND IPO_Session_ID=" + SessionID + @"
                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '') AS ExportedIDs
                        FROM [dbksclCallCenter].[dbo].[tbl_IPO_Confirmation_SMS] AS bs
                        WHERE SMSType='" + Indication_IPOPaymentTransaction.ApplicationSMSNotification_SMSType + @"'
                        AND IPO_Session_ID=" + SessionID + @"
                        AND IsParentChild=0
                        --WHERE SMSType='IPO_Query'
                        Group By SMSType
                        SET ARITHABORT  OFF 
                    ";
            try
            {
                //_dbConnection.ConnectDatabase();
                DataTable dt = _dbConnection.ExecuteQuery_SMSSender(query);
                if (dt.Rows.Count == 1)
                    ExportedIDs = dt.Rows[0][0].ToString();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
            return ExportedIDs;

        }

        #region
//        public string GetAlreadyExportedNotifications_ForSingle(int SessionID)
//        {
//            string ExportedIDs = string.Empty;
//            string query = string.Empty;


//            query = @"  
//                         SET ARITHABORT  ON  
//                                    SELECT STUFF((
//                                        SELECT ',' + Convert(Varchar(150),ISNULL([ReferenceID],0))
//                                          FROM [tbl_IPO_Confirmation_SMS] as g
//                                            WHERE g.SMSType=bs.SMSType AND IPO_Session_ID=" + SessionID + @" AND (Status <> N'0')
//                                         FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '') AS ExportedIDs
//                                    FROM [dbksclCallCenter].[dbo].[tbl_IPO_Confirmation_SMS]AS bs
//                                    WHERE SMSType='" + Indication_IPOPaymentTransaction.ApplicationSMSNotification_SMSType + @"'
//                                    AND IPO_Session_ID=" + SessionID + @"
//                                    --AND IsParentChild=1
//                                    --WHERE SMSType='IPO_Query'
//                                    Group By SMSType
//                                    SET ARITHABORT  OFF 
//                     ";

//            //            query = @"  
//            //                     
//            //
//            //                        SET ARITHABORT  ON  
//            //                           SELECT STUFF((
//            //                                      SELECT g.Cust_Code, Status 
//            //                                         FROM [tbl_IPO_Confirmation_SMS]as g
//            //                                             WHERE (g.IPO_Session_ID = " + SessionID + @") AND (g.Status <> N'0')) AS ExportedIDs
//            //                    
//            //                            FROM [dbksclCallCenter].[dbo].[tbl_IPO_Confirmation_SMS] AS bs
//            //    
//            //                        SET ARITHABORT  OFF
//            //
//            //                    ";

//            //            query = @"  
//            //                                    SET ARITHABORT  ON  
//            //                                    SELECT STUFF((
//            //                                        SELECT ',' + Convert(Varchar(150),ISNULL([ReferenceID],0))
//            //                                        FROM [tbl_IPO_Confirmation_SMS] as g
//            //                                        WHERE g.SMSType=bs.SMSType AND IPO_Session_ID=" + SessionID + @"
//            //                                        FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '') AS ExportedIDs
//            //                                    FROM [dbksclCallCenter].[dbo].[tbl_IPO_Confirmation_SMS] AS bs
//            //                                    WHERE SMSType='" + Indication_IPOPaymentTransaction.ApplicationSMSNotification_SMSType + @"'
//            //                                    AND IPO_Session_ID=" + SessionID + @"
//            //                                    AND IsParentChild=0
//            //                                    --WHERE SMSType='IPO_Query'
//            //                                    Group By SMSType
//            //                                    SET ARITHABORT  OFF 
//            //                                ";
//            try
//            {
//                _dbConnection.ConnectDatabase_SMSSender();
//                DataTable dt = _dbConnection.ExecuteQuery_SMSSender(query);

//                if (dt.Rows.Count == 1)
//                    ExportedIDs = dt.Rows[0][0].ToString();
//            }
//            catch (Exception exception)
//            {
//                throw exception;
//            }
//            finally
//            {
//                _dbConnection.CloseDatabase_SMSSender();
//            }
//            return ExportedIDs;

//        }
        #endregion

        public string GetAlreadyExportedNotifications_ForSingle(int SessionID)
        {
            string ExportedIDs = string.Empty;
            string query = string.Empty;


            query = @"  
                         SET ARITHABORT  ON  
                                    SELECT STUFF((
                                        SELECT ',' + Convert(Varchar(150),ISNULL([ReferenceID],0))
                                          FROM [tbl_IPO_Confirmation_SMS] as g
                                            WHERE g.SMSType=bs.SMSType AND IPO_Session_ID=" + SessionID + @" AND (Status <> N'0')
                                         FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '') AS ExportedIDs
                                    FROM [dbksclCallCenter].[dbo].[tbl_IPO_Confirmation_SMS]AS bs
                                    WHERE SMSType='" + Indication_IPOPaymentTransaction.ApplicationSMSNotification_SMSType + @"'
                                    AND IPO_Session_ID=" + SessionID + @"
                                    --AND IsParentChild=1
                                    --WHERE SMSType='IPO_Query'
                                    Group By SMSType
                                    SET ARITHABORT  OFF 
                     ";

            try
            {
                _dbConnection.ConnectDatabase_SMSSender();
                DataTable dt = _dbConnection.ExecuteQuery_SMSSender(query);

                if (dt.Rows.Count == 1)
                    ExportedIDs = dt.Rows[0][0].ToString();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                _dbConnection.CloseDatabase_SMSSender();
            }
            return ExportedIDs;

        }
        public SqlDataReader GetApplicationList_AllreadyApplied()
        {
            string ExportedIDs = string.Empty;
            string query = string.Empty;
            SqlDataReader dr ;
            query = @"  
                        Select Cust_Code,IPOSession_ID,IPOSession_Name,ID, ( Select g.Status_Name  From SBP_IPO_Approval_Status as g Where g.ID=app.Application_Satus) AS Status,AppStatus_UpdatedDate AS ActionDate,AppStatus_RejectedReason AS ActionDesc,ISNULL(ChannelID,0) AS ChannelID,ISNULL(ChannelType,'') AS ChannelType 
                        From SBP_IPO_Application_BasicInfo as app
                        Where app.IPOSession_ID = (
				                                        Select sess.ID
				                                        From SBP_IPO_Session as sess
				                                        Where sess.[Status]=0
                        )
                        
                        UNION ALL
                        
                        SELECT 
                                [Cust_Code]
                                ,[IPOSessionID]
                                ,(Select t.IPOSession_Name From SBP_IPO_Session as t Where t.ID=delLog.IPOSessionID) AS IPOSession_Name
                                ,0 AS ID
                                ,( 
	                                Select g.Status_Name  
	                                From SBP_IPO_Approval_Status as g 
	                                Where g.ID=2
                                ) AS Status
                                ,[Deleted_Date] AS ActionDate
                                ,[Delete_Reason] AS ActionDesc
                                ,ISNULL(SMSReqID,0) AS ChannelID
                                ,ISNULL(Media_Type,'') AS ChannelType      
                        FROM [SBP_Database].[dbo].[SMS_Sync_Import_IPORequest_DeleteLog] as delLog
                        Where delLog.[IPOSessionID] = (
                                    Select sess.ID
                                    From SBP_IPO_Session as sess
                                    Where sess.[Status]=0
                        )                        
                     ";
            try
            {
                //_dbConnection.ConnectDatabase_SMSSender();
                dr = _dbConnection.ExecuteReader(query);
                
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase_SMSSender();
            }
            
            return dr;

        }

        public SqlDataReader GetApplicationList_AllreadyApplied(string[] IDs)
        {
            string ExportedIDs = string.Empty;
            string query = string.Empty;
            string JoinedCust_IDs = (IDs.Length > 0 ? String.Join(",", IDs) : "0");

            SqlDataReader dr;
            query = @"  
                        Select Cust_Code,IPOSession_ID,IPOSession_Name,ID, ( Select g.Status_Name  From SBP_IPO_Approval_Status as g Where g.ID=app.Application_Satus) AS Status,AppStatus_UpdatedDate AS ActionDate,AppStatus_RejectedReason AS ActionDesc,ISNULL(ChannelID,0) AS ChannelID,ISNULL(ChannelType,'') AS ChannelType 
                        From SBP_IPO_Application_BasicInfo as app
                        Where app.IPOSession_ID = (
				                                        Select sess.ID
				                                        From SBP_IPO_Session as sess
				                                        Where sess.[Status]=0
                        )
                        AND app.ID IN (" + JoinedCust_IDs + @")
                        
                        UNION ALL
                        
                        SELECT 
                                [Cust_Code]
                                ,[IPOSessionID]
                                ,(Select t.IPOSession_Name From SBP_IPO_Session as t Where t.ID=delLog.IPOSessionID) AS IPOSession_Name
                                ,0 AS ID
                                ,( 
	                                Select g.Status_Name  
	                                From SBP_IPO_Approval_Status as g 
	                                Where g.ID=2
                                ) AS Status
                                ,[Deleted_Date] AS ActionDate
                                ,[Delete_Reason] AS ActionDesc
                                ,ISNULL(SMSReqID,0) AS ChannelID
                                ,ISNULL(Media_Type,'') AS ChannelType      
                        FROM [SBP_Database].[dbo].[SMS_Sync_Import_IPORequest_DeleteLog] as delLog
                        Where delLog.[IPOSessionID] = (
                                    Select sess.ID
                                    From SBP_IPO_Session as sess
                                    Where sess.[Status]=0
                        )
                     ";
            try
            {
                //_dbConnection.ConnectDatabase_SMSSender();
                dr = _dbConnection.ExecuteReader(query);

            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase_SMSSender();
            }

            return dr;

        }
        
        public SqlDataReader GetTransactionList_Approve_Reject()
        {
            string ExportedIDs = string.Empty;
            string query = string.Empty;
            SqlDataReader dr;
            query = @"  
                      SELECT 
                           [ID] AS [MoneyTransID]
                          ,[Cust_Code] AS [Cust_Code]
                          ,[Received_Date] AS [Received_Date]      
                          ,[Amount] AS [Amount]
                          ,[Deposit_Withdraw] AS [Deposit_Withdraw]
                          ,[Money_TransactionType_Name] AS [MoneyTransType]
                          ,[Approval_Status] AS [Status]
                          ,[Approval_Date] AS [ActionDate]
                          ,[Rejected_Reason] AS [ActionDesc]
                          ,[Clearing_Status]
                          ,[ChannelID] AS [ChannelID]
                          ,[Channel] AS [ChannelType]
                          ,0 AS [WebSyncFalg]
                    FROM [SBP_IPO_Customer_Broker_MoneyTransaction] as trn
                    LEFT OUTER JOIN  
                    [SBP_IPO_Customer_Broker_Transaction_Details] as dtl
                    ON trn.ID=dtl.TransID
                    WHERE ISNULL(trn.Channel,'')<>''
                    AND ISNULL(trn.[ChannelID],0)<>0
                    AND trn.Approval_Status=1
                     ";
            try
            {
                //_dbConnection.ConnectDatabase_SMSSender();
                dr = _dbConnection.ExecuteReader(query);

            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase_SMSSender();
            }

            return dr;

        }

        #region
//        public DataTable GetIPOSMSNotification_ParentChild_SuccUnsucc_UITransApplied(int SessionID, string AlreadyExportedIDs)
//        {
////            DataTable dt = new DataTable();
////            string query = string.Empty;
////            query = @" Declare @SessionID INT=" + SessionID + @"
////
////                        Select 
////	                        bs.Cust_Code as Cust_Code
////	                        ,ISNULL((
////		                        Select dtl.Parent_Code
////		                        From SBP_Parent_Child_Details as dtl
////		                        Where dtl.Child_Code=bs.Cust_Code
////	                        ),'')AS Parent_Code
////	                        ,'' AS [SMSType]
////	                        ,bs.ID as [IPOApplicationID]
////	                        ,(
////		                        Select c.Company_Short_Code
////		                        From SBP_IPO_Company_Info as c
////		                        Where c.ID=	(
////							                        Select t.IPO_Company_ID 
////							                        From SBP_IPO_Session as t 
////							                        Where t.ID=bs.IPOSession_ID
////		                        )
////	                        ) AS Comp_Short_Code
////	                        ,(Select t.Status_Name From SBP_IPO_Approval_Status as t Where t.ID=bs.Application_Satus) AS [ApplicationStatus_Name]
////	                        ,bs.Application_Satus as [Application_Status_ID]
////	                        ,bs.IPOSession_ID AS [IPOSession_ID]
////	                        ,bs.IPOSession_Name AS [IPOSession_Name]
////	                        ,(Select t.Status_Name From SBP_IPO_Approval_Status as t Where t.ID=bs.Application_Satus)+'_'+bs.IPOSession_Name  AS Trans_Reason
////	                        ,'' as [Message]
////	                        ,svc.Mobile_No as [Destination] 
////	                        ,NULL as [DelivaryDate]
////	                        ,0 as Status
////                        INTO #BaseTable
////                        From SBP_IPO_Application_BasicInfo as bs
////                        JOIN SBP_IPO_Application_ExtendedInfo as ext
////                        ON bs.ID=ext.BasicInfo_ID
////                        JOIN SBP_IPO_Approval_Status as appst
////                        ON appst.ID=bs.Application_Satus
////                        JOIN SBP_Service_Registration as svc
////                        ON svc.Cust_Code=bs.Cust_Code
////                        Where bs.IPOSession_ID=@SessionID
////                        AND bs.Application_Satus IN (1,2,3,4)
////                        AND svc.SMS_IPOConfiramation=1
////                        AND bs.Cust_Code IN (
////													                        Select Child_Code AS Cust_Code
////													                        From SBP_Parent_Child_Details
////                        )
////                        AND ISNULL(bs.ChannelID,0)<>0
////                        AND ISNULL(bs.ChannelType,'')='" + Indication_IPOPaymentTransaction.ChannelType_SMS + @"'               
////
////                        --Select * From #BaseTable
////                        SET ARITHABORT  ON     
////                    
////                        SELECT 
////	                        bs.Parent_Code
////	                        ,bs.IPOSession_ID
////                            ,bs.IPOSession_Name
////	                        --,MAX(bs.[IPOApplicationID]) AS IPOApplicationID
////                            ,STUFF((
////                            SELECT ',' + g.Cust_Code
////                            FROM #BaseTable as g
////                            WHERE g.Parent_Code=bs.Parent_Code AND g.Application_Status_ID=4
////                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '') AS Succ_Clients
////                            ,STUFF((
////                            SELECT ',' + g.Cust_Code
////                            FROM #BaseTable as g
////                            WHERE g.Parent_Code=bs.Parent_Code AND g.Application_Status_ID=3
////                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '') AS Unsucc_Clients
////                            ,'" + Indication_IPOPaymentTransaction.ApplicationSMSNotification_SMSType + @"' AS SMSType
////                            ,'' AS [Message]
////                            ,MAX(bs.Comp_Short_Code) AS Comp_Short_Code
////                            ,MAX(bs.Trans_Reason) AS Trans_Reason
////                            ,MAX(bs.Destination) AS Destination
////                            ,MAX(bs.DelivaryDate) AS DelivaryDate
////                            ,MAX(bs.Status) AS Status   
////                        FROM #BaseTable as bs
////                        WHERE bs.Application_Status_ID IN (3,4)
////                        AND bs.Parent_Code NOT IN (
////                                            " + Convert.ToString(AlreadyExportedIDs != string.Empty ? AlreadyExportedIDs : "0") + @"
////                        )
////                        GROUP BY bs.Parent_Code,[IPOSession_ID],[IPOSession_Name]
////                        ORDER BY bs.Parent_Code
////
////                        SET ARITHABORT  OFF
////                    ";

//            DataTable dt = new DataTable();
//            string query = string.Empty;

//            query = @" Declare @SessionID INT=" + SessionID + @"
//            SELECT bs.Cust_Code, ISNULL
//                   ((SELECT Parent_Code
//                     FROM   dbo.SBP_Parent_Child_Details AS dtl
//                     WHERE (Child_Code = bs.Cust_Code)), N'') AS Parent_Code, '' AS SMSType, bs.ID AS IPOApplicationID,
//                   (SELECT Company_Short_Code
//                    FROM   dbo.SBP_IPO_Company_Info AS c
//                    WHERE (ID =
//                                       (SELECT IPO_Company_ID
//                                        FROM   dbo.SBP_IPO_Session AS t
//                                        WHERE (ID = bs.IPOSession_ID)))) AS Comp_Short_Code,
//                   (SELECT Status_Name
//                    FROM   dbo.SBP_IPO_Approval_Status AS t
//                    WHERE (ID = bs.Application_Satus)) AS ApplicationStatus_Name, bs.Application_Satus AS Application_Status_ID, bs.IPOSession_ID, bs.IPOSession_Name,
//                   (SELECT Status_Name
//                    FROM   dbo.SBP_IPO_Approval_Status AS t
//                    WHERE (ID = bs.Application_Satus)) + '_' + bs.IPOSession_Name AS Trans_Reason, '' AS Message, NULL AS DelivaryDate, 0 AS Status, svc.Mobile_No AS Destination, 
//               bs.ChannelType
//          
//           into   #BaseTable          
//               
//FROM  dbo.SBP_IPO_Application_BasicInfo AS bs INNER JOIN
//               dbo.SBP_IPO_Application_ExtendedInfo AS ext ON bs.ID = ext.BasicInfo_ID INNER JOIN
//               dbo.SBP_IPO_Approval_Status AS appst ON appst.ID = bs.Application_Satus INNER JOIN
//               dbo.SBP_Service_Registration AS svc ON svc.Cust_Code = bs.Cust_Code
//WHERE (bs.IPOSession_ID = " + SessionID + @") AND (bs.Application_Satus IN (3, 4)) AND (svc.SMS_IPOConfiramation = 1) AND (bs.Cust_Code IN
//                   (SELECT Child_Code AS Cust_Code
//                    FROM   dbo.SBP_Parent_Child_Details)) AND (ISNULL(bs.ChannelID, 0) <> 0) 
//                    AND (bs.ChannelType = 'SMS')
//                    and(svc.Mobile_No IS NOT NULL)
//                    
//
//             -- delete  from #BaseTable4     
//       -- select * from #BaseTable4
//        
//         SET ARITHABORT  ON     
//                     SELECT 
//	                         bs.Parent_Code
//	                        ,bs.IPOSession_ID
//                            ,bs.IPOSession_Name
//	                        --,MAX(bs.[IPOApplicationID]) AS IPOApplicationID
//                            ,STUFF((
//                            SELECT ',' + g.Cust_Code
//                            FROM #BaseTable as g
//                            WHERE g.Parent_Code=bs.Parent_Code AND g.Application_Status_ID=3
//                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '') AS Succ_Clients
//                            ,STUFF((
//                            SELECT ',' + g.Cust_Code
//                            FROM #BaseTable as g
//                            WHERE g.Parent_Code=bs.Parent_Code AND g.Application_Status_ID=4
//                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '') AS Unsucc_Clients
//                            ,'" + Indication_IPOPaymentTransaction.ApplicationSMSNotification_SMSType + @"' AS SMSType
//                            ,'' AS [Message]
//                            ,MAX(bs.Comp_Short_Code) AS Comp_Short_Code
//                            ,MAX(bs.Trans_Reason) AS Trans_Reason
//                            ,MAX(bs.Destination) AS Destination
//                            ,MAX(bs.DelivaryDate) AS DelivaryDate
//                            ,MAX(bs.Status) AS Status   
//                        FROM #BaseTable as bs
//                        WHERE bs.Application_Status_ID IN (3,4)
//                        AND bs.Parent_Code NOT IN (0)                     
//                        GROUP BY bs.Parent_Code,[IPOSession_ID],[IPOSession_Name]
//                        ORDER BY bs.Parent_Code
//
//                        SET ARITHABORT  OFF
//                                ";

//            try
//            {
//                //_dbConnection.ConnectDatabase();
//                dt = _dbConnection.ExecuteQuery(query);

//            }
//            catch (Exception exception)
//            {
//                throw exception;
//            }
//            finally
//            {
//                //_dbConnection.CloseDatabase();
//            }
//            return dt;
//        }
        #endregion


        public DataTable GetIPOSMSNotification_ParentChild_SuccUnsucc_UITransApplied(int SessionID, string AlreadyExportedIDs)
        {
            DataTable dt = new DataTable();
            string query = string.Empty;

            query = @" Declare @SessionID INT=" + SessionID + @"
            SELECT bs.Cust_Code, ISNULL
                   ((SELECT Parent_Code
                     FROM   dbo.SBP_Parent_Child_Details AS dtl
                     WHERE (Child_Code = bs.Cust_Code)), N'') AS Parent_Code, '' AS SMSType, bs.ID AS IPOApplicationID,
                   (SELECT Company_Short_Code
                    FROM   dbo.SBP_IPO_Company_Info AS c
                    WHERE (ID =
                                       (SELECT IPO_Company_ID
                                        FROM   dbo.SBP_IPO_Session AS t
                                        WHERE (ID = bs.IPOSession_ID)))) AS Comp_Short_Code,
                   (SELECT Status_Name
                    FROM   dbo.SBP_IPO_Approval_Status AS t
                    WHERE (ID = bs.Application_Satus)) AS ApplicationStatus_Name, bs.Application_Satus AS Application_Status_ID, bs.IPOSession_ID, bs.IPOSession_Name,
                   (SELECT Status_Name
                    FROM   dbo.SBP_IPO_Approval_Status AS t
                    WHERE (ID = bs.Application_Satus)) + '_' + bs.IPOSession_Name AS Trans_Reason, '' AS Message, NULL AS DelivaryDate, 0 AS Status, svc.Mobile_No AS Destination, 
               bs.ChannelType
          
           into   #BaseTable          
               
FROM  dbo.SBP_IPO_Application_BasicInfo AS bs INNER JOIN
               dbo.SBP_IPO_Application_ExtendedInfo AS ext ON bs.ID = ext.BasicInfo_ID INNER JOIN
               dbo.SBP_IPO_Approval_Status AS appst ON appst.ID = bs.Application_Satus INNER JOIN
               dbo.SBP_Service_Registration AS svc ON svc.Cust_Code = bs.Cust_Code
WHERE (bs.IPOSession_ID = " + SessionID + @") AND (bs.Application_Satus IN (3, 4)) AND (svc.SMS_IPOConfiramation = 1) AND (bs.Cust_Code IN
                   (SELECT Child_Code AS Cust_Code
                    FROM   dbo.SBP_Parent_Child_Details)) AND (ISNULL(bs.ChannelID, 0) <> 0) 
                    AND (bs.ChannelType = 'SMS')
                    and(svc.Mobile_No IS NOT NULL)
                    

             -- delete  from #BaseTable4     
       -- select * from #BaseTable4
        
         SET ARITHABORT  ON     
                     SELECT 
	                         bs.Parent_Code
	                        ,bs.IPOSession_ID
                            ,bs.IPOSession_Name
	                        --,MAX(bs.[IPOApplicationID]) AS IPOApplicationID
                            ,STUFF((
                            SELECT ',' + g.Cust_Code
                            FROM #BaseTable as g
                            WHERE g.Parent_Code=bs.Parent_Code AND g.Application_Status_ID=3
                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '') AS Succ_Clients
                            ,STUFF((
                            SELECT ',' + g.Cust_Code
                            FROM #BaseTable as g
                            WHERE g.Parent_Code=bs.Parent_Code AND g.Application_Status_ID=4
                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '') AS Unsucc_Clients
                            ,'" + Indication_IPOPaymentTransaction.ApplicationSMSNotification_SMSType + @"' AS SMSType
                            ,'' AS [Message]
                            ,MAX(bs.Comp_Short_Code) AS Comp_Short_Code
                            ,MAX(bs.Trans_Reason) AS Trans_Reason
                            ,MAX(bs.Destination) AS Destination
                            ,MAX(bs.DelivaryDate) AS DelivaryDate
                            ,MAX(bs.Status) AS Status   
                        FROM #BaseTable as bs
                        WHERE bs.Application_Status_ID IN (3,4)
                        AND bs.Parent_Code NOT IN (0)                     
                        GROUP BY bs.Parent_Code,[IPOSession_ID],[IPOSession_Name]
                        ORDER BY bs.Parent_Code

                        SET ARITHABORT  OFF
                                ";

            try
            {
                //_dbConnection.ConnectDatabase();
                dt = _dbConnection.ExecuteQuery(query);

            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                _dbConnection.CloseDatabase();
            }
            return dt;
        }


        #region
        //        public DataTable GetIPOSMSNotification_ParentChild_SuccUnsucc(int SessionID, string AlreadyExportedIDs)
//        {
//            DataTable dt = new DataTable();
//            string query = string.Empty;


//            query = @" Declare @SessionID INT=" + SessionID + @"
//
//                        Select 
//	                        bs.Cust_Code as Cust_Code
//	                        ,ISNULL((
//		                        Select dtl.Parent_Code
//		                        From SBP_Parent_Child_Details as dtl
//		                        Where dtl.Child_Code=bs.Cust_Code
//	                        ),'')AS Parent_Code
//	                        ,'' AS [SMSType]
//	                        ,bs.ID as [IPOApplicationID]
//	                        ,(
//		                        Select c.Company_Short_Code
//		                        From SBP_IPO_Company_Info as c
//		                        Where c.ID=	(
//							                        Select t.IPO_Company_ID 
//							                        From SBP_IPO_Session as t 
//							                        Where t.ID=bs.IPOSession_ID
//		                        )
//	                        ) AS Comp_Short_Code
//	                        ,(Select t.Status_Name From SBP_IPO_Approval_Status as t Where t.ID=bs.Application_Satus) AS [ApplicationStatus_Name]
//	                        ,bs.Application_Satus as [Application_Status_ID]
//	                        ,bs.IPOSession_ID AS [IPOSession_ID]
//	                        ,bs.IPOSession_Name AS [IPOSession_Name]
//	                        ,(Select t.Status_Name From SBP_IPO_Approval_Status as t Where t.ID=bs.Application_Satus)+'_'+bs.IPOSession_Name  AS Trans_Reason
//	                        ,'' as [Message]
//	                        ,svc.Mobile_No as [Destination] 
//	                        ,NULL as [DelivaryDate]
//	                        ,0 as Status
//                        INTO #BaseTable
//                        From SBP_IPO_Application_BasicInfo as bs
//                        JOIN SBP_IPO_Application_ExtendedInfo as ext
//                        ON bs.ID=ext.BasicInfo_ID
//                        JOIN SBP_IPO_Approval_Status as appst
//                        ON appst.ID=bs.Application_Satus
//                        JOIN SBP_Service_Registration as svc
//                        ON svc.Cust_Code=bs.Cust_Code
//                        Where bs.IPOSession_ID=@SessionID
//                        AND bs.Application_Satus IN (3,4)
//                        AND svc.SMS_IPOConfiramation=1
//                        AND bs.Cust_Code IN (
//													                        Select Child_Code AS Cust_Code
//													                        From SBP_Parent_Child_Details
//                        )
//                        AND ISNULL(bs.ChannelID,0)<>0
//                        AND ISNULL(bs.ChannelType,'')='" + Indication_IPOPaymentTransaction.ChannelType_SMS + @"'
//                        AND (svc.Mobile_No IS NOT NULL)
//
//                        --Select * From #BaseTable
//                        SET ARITHABORT  ON     
//                    
//                        SELECT 
//	                        bs.Parent_Code
//	                        ,bs.IPOSession_ID
//                            ,bs.IPOSession_Name
//                            --,MAX(bs.[IPOApplicationID]) AS IPOApplicationID
//                            ,STUFF((
//                            SELECT ',' + g.Cust_Code
//                            FROM #BaseTable as g
//                            WHERE g.Parent_Code=bs.Parent_Code AND g.Application_Status_ID=3
//                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '') AS Succ_Clients
//                            ,STUFF((
//                            SELECT ',' + g.Cust_Code
//                            FROM #BaseTable as g
//                            WHERE g.Parent_Code=bs.Parent_Code AND g.Application_Status_ID=4
//                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '') AS Unsucc_Clients
//                            ,'" + Indication_IPOPaymentTransaction.ApplicationSMSNotification_SMSType + @"' AS SMSType
//                            ,'' AS [Message]
//                            ,MAX(bs.Comp_Short_Code) AS Comp_Short_Code
//                            ,MAX(bs.Trans_Reason) AS Trans_Reason
//                            ,MAX(bs.Destination) AS Destination
//                            ,MAX(bs.DelivaryDate) AS DelivaryDate
//                            ,MAX(bs.Status) AS Status   
//                        FROM #BaseTable as bs
//                        WHERE bs.Application_Status_ID IN (3,4)
//                         AND bs.Parent_Code Not IN (
//                                            " + Convert.ToString(AlreadyExportedIDs != string.Empty ? AlreadyExportedIDs : "0") + @"
//                        )
//                        GROUP BY bs.Parent_Code,[IPOSession_ID],[IPOSession_Name]
//                        ORDER BY bs.Parent_Code
//
//                        SET ARITHABORT  OFF
//                    ";
//            try
//            {
//                _dbConnection.ConnectDatabase();
//                dt = _dbConnection.ExecuteQuery(query);

//            }
//            catch (Exception exception)
//            {
//                throw exception;
//            }
//            finally
//            {
//                _dbConnection.CloseDatabase();
//            }
//            return dt;
//        }
        #endregion


        public DataTable GetIPOSMSNotification_ParentChild_SuccUnsucc(int SessionID, string AlreadyExportedIDs)
        {
            DataTable dt = new DataTable();
            string query = string.Empty;


            query = @" Declare @SessionID INT=" + SessionID + @"
        
        SELECT bs.Cust_Code
        , ISNULL((SELECT Parent_Code
                        FROM dbo.SBP_Parent_Child_Details AS dtl
                             WHERE (Child_Code = bs.Cust_Code)), '') AS Parent_Code
       , '' AS SMSType, bs.ID AS IPOApplicationID
       ,(SELECT Company_Short_Code
                 FROM   dbo.SBP_IPO_Company_Info AS c
                         WHERE (ID =(SELECT IPO_Company_ID
                                 FROM   dbo.SBP_IPO_Session AS t
                                           WHERE (ID = bs.IPOSession_ID)))) AS Comp_Short_Code
                                        ,(SELECT Status_Name
                                                     FROM   dbo.SBP_IPO_Approval_Status AS t
                                                              WHERE (ID = bs.Application_Satus)) AS ApplicationStatus_Name
                                        , bs.Application_Satus AS Application_Status_ID
                                         , bs.IPOSession_ID, bs.IPOSession_Name
                   ,(SELECT Status_Name
                            FROM   dbo.SBP_IPO_Approval_Status AS t
                                 WHERE (ID = bs.Application_Satus)) + '_' + bs.IPOSession_Name AS Trans_Reason
                    , '' AS Message
                    
                    ,(Select t.Hadeler_Contact_Mobile
		                From SBP_Parent_Child_Owner_Details as t
		                     Where ISNULL(t.Hadeler_Contact_Mobile,'')<>''
		                            AND t.Parent_Id=(Select c.Parent_Code
							                    From SBP_Parent_Child_Details as c
							                             Where c.Child_Code=bs.Cust_Code
		                                   )
                        ) as Destination 
                     ,NULL as [DelivaryDate]
                     , 0 AS Status
       
      
                    
                 INTO #BaseTable
               
                             FROM  dbo.SBP_IPO_Application_BasicInfo AS bs INNER JOIN
                             dbo.SBP_IPO_Application_ExtendedInfo AS ext ON bs.ID = ext.BasicInfo_ID INNER JOIN
                             dbo.SBP_IPO_Approval_Status AS appst ON appst.ID = bs.Application_Satus

                                    WHERE 
                                    
                                           (bs.IPOSession_ID = @SessionID) 
                                          AND (bs.Application_Satus IN (3,4)) 
                                          AND (bs.Cust_Code IN
                                                      (SELECT Child_Code AS Cust_Code
                                                                 FROM   dbo.SBP_Parent_Child_Details)) 
                                          AND (ISNULL(bs.ChannelID, 0) <> 0) 
                                          AND (ISNULL(bs.ChannelType, '') = 'SMS')
                                          
                                          
                                          
             --Select * From #BaseTable
                                SET ARITHABORT  ON     
                            
                                SELECT 
        	                         bs.Parent_Code
        	                        ,bs.IPOSession_ID
                                    ,bs.IPOSession_Name
                                    --,MAX(bs.[IPOApplicationID]) AS IPOApplicationID
                                    ,STUFF((
                                    SELECT ',' + g.Cust_Code
                                    FROM #BaseTable as g
                                    WHERE g.Parent_Code=bs.Parent_Code AND g.Application_Status_ID=3
                                    FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '') AS Succ_Clients
                                    ,STUFF((
                                    SELECT ',' + g.Cust_Code
                                    FROM #BaseTable as g
                                    WHERE g.Parent_Code=bs.Parent_Code AND g.Application_Status_ID=4
                                    FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '') AS Unsucc_Clients
                                    ,'" + Indication_IPOPaymentTransaction.ApplicationSMSNotification_SMSType + @"' AS SMSType
                                    ,'' AS [Message]
                                    ,MAX(bs.Comp_Short_Code) AS Comp_Short_Code
                                    ,MAX(bs.Trans_Reason) AS Trans_Reason                                 
                                    ,MAX(bs.Destination) AS Destination
                                    ,MAX(bs.DelivaryDate) AS DelivaryDate
                                    ,MAX(bs.Status) AS Status   
                                FROM #BaseTable as bs
                                WHERE bs.Application_Status_ID IN (3,4)
                                 AND bs.Parent_Code Not IN (
                                                   ''
                                )
                                
                                GROUP BY bs.Parent_Code,[IPOSession_ID],[IPOSession_Name]
                                ORDER BY bs.Parent_Code
        
                                SET ARITHABORT  OFF           
                            ";
            try
            {
                _dbConnection.ConnectDatabase();
                dt = _dbConnection.ExecuteQuery(query);

            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                _dbConnection.CloseDatabase();
            }
            return dt;
        }

        #region
//        public DataTable GetIPOSMSNotification_ParentChild_ApproveReject_UITransApplied(int SessionID, string AlreadyExportedIDs)
//        {
//            DataTable dt = new DataTable();
//            string query = string.Empty;
//            query = @"  Declare @SessionID INT=" + SessionID + @"
//
//                        Select 
//                        bs.Cust_Code as Cust_Code
//                        ,ISNULL((
//                            Select dtl.Parent_Code
//                            From SBP_Parent_Child_Details as dtl
//                            Where dtl.Child_Code=bs.Cust_Code
//                        ),'')AS Parent_Code
//                        ,'' AS [SMSType]
//                        ,bs.ID as [IPOApplicationID]
//                        ,(
//                            Select c.Company_Short_Code
//                            From SBP_IPO_Company_Info as c
//                            Where c.ID=	(
//	                                            Select t.IPO_Company_ID 
//	                                            From SBP_IPO_Session as t 
//	                                            Where t.ID=bs.IPOSession_ID
//                            )
//                        ) AS Comp_Short_Code
//                        ,(Select t.Status_Name From SBP_IPO_Approval_Status as t Where t.ID=bs.Application_Satus) AS [ApplicationStatus_Name]
//                        ,bs.Application_Satus as [Application_Status_ID]
//                        ,bs.IPOSession_ID AS [IPOSession_ID]
//                        ,bs.IPOSession_Name AS [IPOSession_Name]
//                        ,(Select t.Status_Name From SBP_IPO_Approval_Status as t Where t.ID=bs.Application_Satus)+'_'+bs.IPOSession_Name  AS Trans_Reason
//                        ,'' as [Message]
//                        ,(
//		                    Select t.Hadeler_Contact_Mobile
//		                    From SBP_Parent_Child_Owner_Details as t
//		                    Where ISNULL(t.Hadeler_Contact_Mobile,'')<>''
//		                    AND t.Parent_Id=(
//							                    Select c.Parent_Code
//							                    From SBP_Parent_Child_Details as c
//							                    Where c.Child_Code=bs.Cust_Code
//		                    )
//                        ) as [Destination] 
//                        ,NULL as [DelivaryDate]
//                        ,0 as Status
//                        INTO #BaseTable
//                        From SBP_IPO_Application_BasicInfo as bs
//                        JOIN SBP_IPO_Application_ExtendedInfo as ext
//                        ON bs.ID=ext.BasicInfo_ID
//                        JOIN SBP_IPO_Approval_Status as appst
//                        ON appst.ID=bs.Application_Satus
//                        Where bs.IPOSession_ID=@SessionID
//                        AND bs.Application_Satus IN (1,2,3,4)
//
//                        AND bs.Cust_Code IN (
//							                                                Select Child_Code AS Cust_Code
//							                                                From SBP_Parent_Child_Details
//                        )
//                        AND ISNULL(bs.ChannelID,0)<>0
//                        AND ISNULL(bs.ChannelType,'')='" + Indication_IPOPaymentTransaction.ChannelType_SMS + @"'
//
//                        UNION ALL
//
//                        Select 
//                        Cust_Code
//                        ,RegisteredCode AS Parent_Code
//                        ,'' AS SMSType
//                        ,0 AS IPOApplicationID
//                        ,(
//	                        Select t.Company_Short_Code 
//	                        From SBP_IPO_Company_Info as t 
//	                        Where t.ID=(
//				                        Select g.IPO_Company_ID
//				                        From SBP_IPO_Session as g
//				                        Where g.ID=del.IPOSessionID
//	                        )
//                        ) AS Comp_Short_Code
//                        ,(
//	                        Select s.Status_Name
//	                        From SBP_IPO_Approval_Status as s
//	                        WHere s.ID=2
//                        ) AS [ApplicationStatus_Name]
//                        ,(
//	                        Select s.ID
//	                        From SBP_IPO_Approval_Status as s
//	                        WHere s.ID=2
//                        ) AS [Application_Status_ID]
//                        ,IPOSessionID AS [IPOSession_ID]
//                        ,(
//	                        Select s.IPOSession_Name
//	                        From SBP_IPO_Session as s
//	                        Where s.ID=del.IPOSessionID
//                        ) AS [IPOSession_Name]
//                        ,(Select t.Status_Name From SBP_IPO_Approval_Status as t Where t.ID=2)+'_'+
//                        (
//	                        Select s.IPOSession_Name
//	                        From SBP_IPO_Session as s
//	                        Where s.ID=del.IPOSessionID
//                        )   AS Trans_Reason
//                        ,'' AS [Message]
//                        ,(
//                            Select t.Hadeler_Contact_Mobile
//                            From SBP_Parent_Child_Owner_Details as t
//                            Where ISNULL(t.Hadeler_Contact_Mobile,'')<>''
//                            AND t.Parent_Id=(
//	                                            Select c.Parent_Code
//	                                            From SBP_Parent_Child_Details as c
//	                                            Where c.Child_Code=del.Cust_Code
//                            )
//                        ) as [Destination] 
//                        ,NULL AS [DelivaryDate]
//                        ,0 AS [Status]
//                        From dbo.SMS_Sync_Import_IPORequest_DeleteLog as del
//                        Where del.IPOSessionID=@SessionID
//                        AND del.Cust_Code IN (
//						            Select dtl.Child_Code
//						            From SBP_Parent_Child_Details as dtl
//                        )                        
//
//
//                        --Select * From #BaseTable
//                         SET ARITHABORT  ON     
//                        SELECT 
//	                        bs.Parent_Code
//	                        ,bs.IPOSession_ID
//                            ,bs.IPOSession_Name
//	                        --,MAX(bs.[IPOApplicationID]) AS IPOApplicationID
//                            ,STUFF((
//                            SELECT ',' + g.Cust_Code
//                            FROM #BaseTable as g
//                            WHERE g.Parent_Code=bs.Parent_Code AND g.Application_Status_ID=1
//                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '') AS Succ_Clients
//                            ,STUFF((
//                            SELECT ',' + g.Cust_Code
//                            FROM #BaseTable as g
//                            WHERE g.Parent_Code=bs.Parent_Code AND g.Application_Status_ID=2
//                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '') AS Unsucc_Clients
//                            ,'" + Indication_IPOPaymentTransaction.ApplicationSMSNotification_SMSType + @"' AS SMSType
//                            ,'' AS [Message]
//                            ,MAX(bs.Comp_Short_Code) AS Comp_Short_Code
//                            ,MAX(bs.Trans_Reason) AS Trans_Reason
//                            ,MAX(bs.Destination) AS Destination
//                            ,MAX(bs.DelivaryDate) AS DelivaryDate
//                            ,MAX(bs.Status) AS Status   
//                        FROM #BaseTable as bs
//                        WHERE bs.Application_Status_ID IN (1,2)
//                         AND bs.Parent_Code NOT IN (
//                                            " + Convert.ToString(AlreadyExportedIDs != string.Empty ? AlreadyExportedIDs : "0") + @"
//                        )
//                        GROUP BY bs.Parent_Code,[IPOSession_ID],[IPOSession_Name]
//                        ORDER BY bs.Parent_Code
//                         SET ARITHABORT  OFF 
//                    ";
//            try
//            {
//                //_dbConnection.ConnectDatabase();
//                dt = _dbConnection.ExecuteQuery(query);

//            }
//            catch (Exception exception)
//            {
//                throw exception;
//            }
//            finally
//            {
//                //_dbConnection.CloseDatabase();
//            }
//            return dt;
//        }
       
        #endregion

        public DataTable GetIPOSMSNotification_ParentChild_ApproveReject_UITransApplied(int SessionID, string AlreadyExportedIDs)
        {
            DataTable dt = new DataTable();
            string query = string.Empty;
            query = @"  Declare @SessionID INT=" + SessionID + @"

                        Select 
                        bs.Cust_Code as Cust_Code
                        ,ISNULL((
                            Select dtl.Parent_Code
                            From SBP_Parent_Child_Details as dtl
                            Where dtl.Child_Code=bs.Cust_Code
                        ),'')AS Parent_Code
                        ,'' AS [SMSType]
                        ,bs.ID as [IPOApplicationID]
                        ,(
                            Select c.Company_Short_Code
                            From SBP_IPO_Company_Info as c
                            Where c.ID=	(
	                                            Select t.IPO_Company_ID 
	                                            From SBP_IPO_Session as t 
	                                            Where t.ID=bs.IPOSession_ID
                            )
                        ) AS Comp_Short_Code
                        ,(Select t.Status_Name From SBP_IPO_Approval_Status as t Where t.ID=bs.Application_Satus) AS [ApplicationStatus_Name]
                        ,bs.Application_Satus as [Application_Status_ID]
                        ,bs.IPOSession_ID AS [IPOSession_ID]
                        ,bs.IPOSession_Name AS [IPOSession_Name]
                        ,(Select t.Status_Name From SBP_IPO_Approval_Status as t Where t.ID=bs.Application_Satus)+'_'+bs.IPOSession_Name  AS Trans_Reason
                        ,'' as [Message]
                        ,(
		                    Select t.Hadeler_Contact_Mobile
		                    From SBP_Parent_Child_Owner_Details as t
		                    Where ISNULL(t.Hadeler_Contact_Mobile,'')<>''
		                    AND t.Parent_Id=(
							                    Select c.Parent_Code
							                    From SBP_Parent_Child_Details as c
							                    Where c.Child_Code=bs.Cust_Code
		                    )
                        ) as [Destination] 
                        ,NULL as [DelivaryDate]
                        ,0 as Status
                        INTO #BaseTable
                        From SBP_IPO_Application_BasicInfo as bs
                        JOIN SBP_IPO_Application_ExtendedInfo as ext
                        ON bs.ID=ext.BasicInfo_ID
                        JOIN SBP_IPO_Approval_Status as appst
                        ON appst.ID=bs.Application_Satus
                        Where bs.IPOSession_ID=@SessionID
                        AND bs.Application_Satus IN (1,2,3,4)

                        AND bs.Cust_Code IN (
							                                                Select Child_Code AS Cust_Code
							                                                From SBP_Parent_Child_Details
                        )
                        AND ISNULL(bs.ChannelID,0)<>0
                        AND ISNULL(bs.ChannelType,'')='" + Indication_IPOPaymentTransaction.ChannelType_SMS + @"'

                        UNION ALL

                        Select 
                        Cust_Code
                        ,RegisteredCode AS Parent_Code
                        ,'' AS SMSType
                        ,0 AS IPOApplicationID
                        ,(
	                        Select t.Company_Short_Code 
	                        From SBP_IPO_Company_Info as t 
	                        Where t.ID=(
				                        Select g.IPO_Company_ID
				                        From SBP_IPO_Session as g
				                        Where g.ID=del.IPOSessionID
	                        )
                        ) AS Comp_Short_Code
                        ,(
	                        Select s.Status_Name
	                        From SBP_IPO_Approval_Status as s
	                        WHere s.ID=2
                        ) AS [ApplicationStatus_Name]
                        ,(
	                        Select s.ID
	                        From SBP_IPO_Approval_Status as s
	                        WHere s.ID=2
                        ) AS [Application_Status_ID]
                        ,IPOSessionID AS [IPOSession_ID]
                        ,(
	                        Select s.IPOSession_Name
	                        From SBP_IPO_Session as s
	                        Where s.ID=del.IPOSessionID
                        ) AS [IPOSession_Name]
                        ,(Select t.Status_Name From SBP_IPO_Approval_Status as t Where t.ID=2)+'_'+
                        (
	                        Select s.IPOSession_Name
	                        From SBP_IPO_Session as s
	                        Where s.ID=del.IPOSessionID
                        )   AS Trans_Reason
                        ,'' AS [Message]
                        ,(
                            Select t.Hadeler_Contact_Mobile
                            From SBP_Parent_Child_Owner_Details as t
                            Where ISNULL(t.Hadeler_Contact_Mobile,'')<>''
                            AND t.Parent_Id=(
	                                            Select c.Parent_Code
	                                            From SBP_Parent_Child_Details as c
	                                            Where c.Child_Code=del.Cust_Code
                            )
                        ) as [Destination] 
                        ,NULL AS [DelivaryDate]
                        ,0 AS [Status]
                        From dbo.SMS_Sync_Import_IPORequest_DeleteLog as del
                        Where del.IPOSessionID=@SessionID
                        AND del.Cust_Code IN (
						            Select dtl.Child_Code
						            From SBP_Parent_Child_Details as dtl
                        )                        
                          --AND (dbo.SBP_Service_Registration.Mobile_No IS NOT NULL)

                        --Select * From #BaseTable
                         SET ARITHABORT  ON     
                        SELECT 
	                        bs.Parent_Code
	                        ,bs.IPOSession_ID
                            ,bs.IPOSession_Name
	                        --,MAX(bs.[IPOApplicationID]) AS IPOApplicationID
                            ,STUFF((
                            SELECT ',' + g.Cust_Code
                            FROM #BaseTable as g
                            WHERE g.Parent_Code=bs.Parent_Code AND g.Application_Status_ID=1
                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '') AS Succ_Clients
                            ,STUFF((
                            SELECT ',' + g.Cust_Code
                            FROM #BaseTable as g
                            WHERE g.Parent_Code=bs.Parent_Code AND g.Application_Status_ID=2
                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '') AS Unsucc_Clients
                            ,'" + Indication_IPOPaymentTransaction.ApplicationSMSNotification_SMSType + @"' AS SMSType
                            ,'' AS [Message]
                            ,MAX(bs.Comp_Short_Code) AS Comp_Short_Code
                            ,MAX(bs.Trans_Reason) AS Trans_Reason
                            ,MAX(bs.Destination) AS Destination
                            ,MAX(bs.DelivaryDate) AS DelivaryDate
                            ,MAX(bs.Status) AS Status   
                        FROM #BaseTable as bs
                        WHERE bs.Application_Status_ID IN (1,2)
                         AND bs.Parent_Code NOT IN (
                                            " + Convert.ToString(AlreadyExportedIDs != string.Empty ? AlreadyExportedIDs : "0") + @"
                        )
                        GROUP BY bs.Parent_Code,[IPOSession_ID],[IPOSession_Name]
                        ORDER BY bs.Parent_Code
                         SET ARITHABORT  OFF 
                    ";
            try
            {
                //_dbConnection.ConnectDatabase();
                dt = _dbConnection.ExecuteQuery(query);

            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
            return dt;
        }
      


        public DataTable GetIPOSMSNotification_ParentChild_ApproveReject(int SessionID, string AlreadyExportedIDs)
        {
            DataTable dt = new DataTable();
            string query = string.Empty;


            query = @"  Declare @SessionID INT=" + SessionID + @"

                        Select 
                        bs.Cust_Code as Cust_Code
                        ,ISNULL((
                            Select dtl.Parent_Code
                            From SBP_Parent_Child_Details as dtl
                            Where dtl.Child_Code=bs.Cust_Code
                        ),'')AS Parent_Code
                        ,'' AS [SMSType]
                        ,bs.ID as [IPOApplicationID]
                        ,(
                            Select c.Company_Short_Code
                            From SBP_IPO_Company_Info as c
                            Where c.ID=	(
	                                            Select t.IPO_Company_ID 
	                                            From SBP_IPO_Session as t 
	                                            Where t.ID=bs.IPOSession_ID
                            )
                        ) AS Comp_Short_Code
                        ,(Select t.Status_Name From SBP_IPO_Approval_Status as t Where t.ID=bs.Application_Satus) AS [ApplicationStatus_Name]
                        ,bs.Application_Satus as [Application_Status_ID]
                        ,bs.IPOSession_ID AS [IPOSession_ID]
                        ,bs.IPOSession_Name AS [IPOSession_Name]
                        ,(Select t.Status_Name From SBP_IPO_Approval_Status as t Where t.ID=bs.Application_Satus)+'_'+bs.IPOSession_Name  AS Trans_Reason
                        ,'' as [Message]
                        ,(
		                    Select t.Hadeler_Contact_Mobile
		                    From SBP_Parent_Child_Owner_Details as t
		                    Where ISNULL(t.Hadeler_Contact_Mobile,'')<>''
		                    AND t.Parent_Id=(
							                    Select c.Parent_Code
							                    From SBP_Parent_Child_Details as c
							                    Where c.Child_Code=bs.Cust_Code
		                    )
                        ) as [Destination] 
                        ,NULL as [DelivaryDate]
                        ,0 as Status
                        INTO #BaseTable
                        From SBP_IPO_Application_BasicInfo as bs
                        JOIN SBP_IPO_Application_ExtendedInfo as ext
                        ON bs.ID=ext.BasicInfo_ID
                        JOIN SBP_IPO_Approval_Status as appst
                        ON appst.ID=bs.Application_Satus
                        Where bs.IPOSession_ID=@SessionID
                        AND bs.Application_Satus IN (1,2)

                        AND bs.Cust_Code IN (
							                                                Select Child_Code AS Cust_Code
							                                                From SBP_Parent_Child_Details
                        )
                        AND ISNULL(bs.ChannelID,0)<>0
                        AND ISNULL(bs.ChannelType,'')='" + Indication_IPOPaymentTransaction.ChannelType_SMS + @"'
                       -- AND (dbo.SBP_Service_Registration.Mobile_No IS NOT NULL)

                        UNION ALL

                        Select 
                        Cust_Code
                        ,RegisteredCode AS Parent_Code
                        ,'' AS SMSType
                        ,0 AS IPOApplicationID
                        ,(
	                        Select t.Company_Short_Code 
	                        From SBP_IPO_Company_Info as t 
	                        Where t.ID=(
				                        Select g.IPO_Company_ID
				                        From SBP_IPO_Session as g
				                        Where g.ID=del.IPOSessionID
	                        )
                        ) AS Comp_Short_Code
                        ,(
	                        Select s.Status_Name
	                        From SBP_IPO_Approval_Status as s
	                        WHere s.ID=2
                        ) AS [ApplicationStatus_Name]
                        ,(
	                        Select s.ID
	                        From SBP_IPO_Approval_Status as s
	                        WHere s.ID=2
                        ) AS [Application_Status_ID]
                        ,IPOSessionID AS [IPOSession_ID]
                        ,(
	                        Select s.IPOSession_Name
	                        From SBP_IPO_Session as s
	                        Where s.ID=del.IPOSessionID
                        ) AS [IPOSession_Name]
                        ,(Select t.Status_Name From SBP_IPO_Approval_Status as t Where t.ID=2)+'_'+
                        (
	                        Select s.IPOSession_Name
	                        From SBP_IPO_Session as s
	                        Where s.ID=del.IPOSessionID
                        )   AS Trans_Reason
                        ,'' AS [Message]
                        ,(
                            Select t.Hadeler_Contact_Mobile
                            From SBP_Parent_Child_Owner_Details as t
                            Where ISNULL(t.Hadeler_Contact_Mobile,'')<>''
                            AND t.Parent_Id=(
	                                            Select c.Parent_Code
	                                            From SBP_Parent_Child_Details as c
	                                            Where c.Child_Code=del.Cust_Code
                            )
                        ) as [Destination] 
                        ,NULL AS [DelivaryDate]
                        ,0 AS [Status]
                        From dbo.SMS_Sync_Import_IPORequest_DeleteLog as del
                        Where del.IPOSessionID=@SessionID
                        AND del.Cust_Code IN (
						            Select dtl.Child_Code
						            From SBP_Parent_Child_Details as dtl
                        )

                        --Select * From #BaseTable
                         SET ARITHABORT  ON     
                        SELECT 
	                        bs.Parent_Code
	                        ,bs.IPOSession_ID
                            ,bs.IPOSession_Name
	                        --,MAX(bs.[IPOApplicationID]) AS IPOApplicationID
                            ,STUFF((
                            SELECT ',' + g.Cust_Code
                            FROM #BaseTable as g
                            WHERE g.Parent_Code=bs.Parent_Code AND g.Application_Status_ID=1
                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '') AS Succ_Clients
                            ,STUFF((
                            SELECT ',' + g.Cust_Code
                            FROM #BaseTable as g
                            WHERE g.Parent_Code=bs.Parent_Code AND g.Application_Status_ID=2
                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '') AS Unsucc_Clients
                            ,'" + Indication_IPOPaymentTransaction.ApplicationSMSNotification_SMSType + @"' AS SMSType
                            ,'' AS [Message]
                            ,MAX(bs.Comp_Short_Code) AS Comp_Short_Code
                            ,MAX(bs.Trans_Reason) AS Trans_Reason
                            ,MAX(bs.Destination) AS Destination
                            ,MAX(bs.DelivaryDate) AS DelivaryDate
                            ,MAX(bs.Status) AS Status   
                        FROM #BaseTable as bs
                        WHERE bs.Application_Status_ID IN (1,2)
                         AND bs.Parent_Code NOT IN (
                                            " + Convert.ToString(AlreadyExportedIDs != string.Empty ? AlreadyExportedIDs : "0") + @"
                        )
                        GROUP BY bs.Parent_Code,[IPOSession_ID],[IPOSession_Name]
                        ORDER BY bs.Parent_Code
                         SET ARITHABORT  OFF 
                    ";
            try
            {
                _dbConnection.ConnectDatabase();
                dt = _dbConnection.ExecuteQuery(query);

            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                _dbConnection.CloseDatabase();
            }
            return dt;
        }



        #region
//        public DataTable GetIPOSMSNotification_ParentChild_ApproveReject(int SessionID, string AlreadyExportedIDs)
//        {
//            DataTable dt = new DataTable();
//            string query = string.Empty;


//            query = @"  Declare @SessionID INT=" + SessionID + @"
//
//                        Select 
//                        bs.Cust_Code as Cust_Code
//                        ,ISNULL((
//                            Select dtl.Parent_Code
//                            From SBP_Parent_Child_Details as dtl
//                            Where dtl.Child_Code=bs.Cust_Code
//                        ),'')AS Parent_Code
//                        ,'' AS [SMSType]
//                        ,bs.ID as [IPOApplicationID]
//                        ,(
//                            Select c.Company_Short_Code
//                            From SBP_IPO_Company_Info as c
//                            Where c.ID=	(
//	                                            Select t.IPO_Company_ID 
//	                                            From SBP_IPO_Session as t 
//	                                            Where t.ID=bs.IPOSession_ID
//                            )
//                        ) AS Comp_Short_Code
//                        ,(Select t.Status_Name From SBP_IPO_Approval_Status as t Where t.ID=bs.Application_Satus) AS [ApplicationStatus_Name]
//                        ,bs.Application_Satus as [Application_Status_ID]
//                        ,bs.IPOSession_ID AS [IPOSession_ID]
//                        ,bs.IPOSession_Name AS [IPOSession_Name]
//                        ,(Select t.Status_Name From SBP_IPO_Approval_Status as t Where t.ID=bs.Application_Satus)+'_'+bs.IPOSession_Name  AS Trans_Reason
//                        ,'' as [Message]
//                        ,(
//		                    Select t.Hadeler_Contact_Mobile
//		                    From SBP_Parent_Child_Owner_Details as t
//		                    Where ISNULL(t.Hadeler_Contact_Mobile,'')<>''
//		                    AND t.Parent_Id=(
//							                    Select c.Parent_Code
//							                    From SBP_Parent_Child_Details as c
//							                    Where c.Child_Code=bs.Cust_Code
//		                    )
//                        ) as [Destination] 
//                        ,NULL as [DelivaryDate]
//                        ,0 as Status
//                        INTO #BaseTable
//                        From SBP_IPO_Application_BasicInfo as bs
//                        JOIN SBP_IPO_Application_ExtendedInfo as ext
//                        ON bs.ID=ext.BasicInfo_ID
//                        JOIN SBP_IPO_Approval_Status as appst
//                        ON appst.ID=bs.Application_Satus
//                        Where bs.IPOSession_ID=@SessionID
//                        AND bs.Application_Satus IN (1,2)
//
//                        AND bs.Cust_Code IN (
//							                                                Select Child_Code AS Cust_Code
//							                                                From SBP_Parent_Child_Details
//                        )
//                        AND ISNULL(bs.ChannelID,0)<>0
//                        AND ISNULL(bs.ChannelType,'')='" + Indication_IPOPaymentTransaction.ChannelType_SMS + @"'
//                       -- AND (dbo.SBP_Service_Registration.Mobile_No IS NOT NULL)
//
//                        UNION ALL
//
//                        Select 
//                        Cust_Code
//                        ,RegisteredCode AS Parent_Code
//                        ,'' AS SMSType
//                        ,0 AS IPOApplicationID
//                        ,(
//	                        Select t.Company_Short_Code 
//	                        From SBP_IPO_Company_Info as t 
//	                        Where t.ID=(
//				                        Select g.IPO_Company_ID
//				                        From SBP_IPO_Session as g
//				                        Where g.ID=del.IPOSessionID
//	                        )
//                        ) AS Comp_Short_Code
//                        ,(
//	                        Select s.Status_Name
//	                        From SBP_IPO_Approval_Status as s
//	                        WHere s.ID=2
//                        ) AS [ApplicationStatus_Name]
//                        ,(
//	                        Select s.ID
//	                        From SBP_IPO_Approval_Status as s
//	                        WHere s.ID=2
//                        ) AS [Application_Status_ID]
//                        ,IPOSessionID AS [IPOSession_ID]
//                        ,(
//	                        Select s.IPOSession_Name
//	                        From SBP_IPO_Session as s
//	                        Where s.ID=del.IPOSessionID
//                        ) AS [IPOSession_Name]
//                        ,(Select t.Status_Name From SBP_IPO_Approval_Status as t Where t.ID=2)+'_'+
//                        (
//	                        Select s.IPOSession_Name
//	                        From SBP_IPO_Session as s
//	                        Where s.ID=del.IPOSessionID
//                        )   AS Trans_Reason
//                        ,'' AS [Message]
//                        ,(
//                            Select t.Hadeler_Contact_Mobile
//                            From SBP_Parent_Child_Owner_Details as t
//                            Where ISNULL(t.Hadeler_Contact_Mobile,'')<>''
//                            AND t.Parent_Id=(
//	                                            Select c.Parent_Code
//	                                            From SBP_Parent_Child_Details as c
//	                                            Where c.Child_Code=del.Cust_Code
//                            )
//                        ) as [Destination] 
//                        ,NULL AS [DelivaryDate]
//                        ,0 AS [Status]
//                        From dbo.SMS_Sync_Import_IPORequest_DeleteLog as del
//                        Where del.IPOSessionID=@SessionID
//                        AND del.Cust_Code IN (
//						            Select dtl.Child_Code
//						            From SBP_Parent_Child_Details as dtl
//                        )
//
//                        --Select * From #BaseTable
//                         SET ARITHABORT  ON     
//                        SELECT 
//	                        bs.Parent_Code
//	                        ,bs.IPOSession_ID
//                            ,bs.IPOSession_Name
//	                        --,MAX(bs.[IPOApplicationID]) AS IPOApplicationID
//                            ,STUFF((
//                            SELECT ',' + g.Cust_Code
//                            FROM #BaseTable as g
//                            WHERE g.Parent_Code=bs.Parent_Code AND g.Application_Status_ID=1
//                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '') AS Succ_Clients
//                            ,STUFF((
//                            SELECT ',' + g.Cust_Code
//                            FROM #BaseTable as g
//                            WHERE g.Parent_Code=bs.Parent_Code AND g.Application_Status_ID=2
//                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '') AS Unsucc_Clients
//                            ,'" + Indication_IPOPaymentTransaction.ApplicationSMSNotification_SMSType + @"' AS SMSType
//                            ,'' AS [Message]
//                            ,MAX(bs.Comp_Short_Code) AS Comp_Short_Code
//                            ,MAX(bs.Trans_Reason) AS Trans_Reason
//                            ,MAX(bs.Destination) AS Destination
//                            ,MAX(bs.DelivaryDate) AS DelivaryDate
//                            ,MAX(bs.Status) AS Status   
//                        FROM #BaseTable as bs
//                        WHERE bs.Application_Status_ID IN (1,2)
//                         AND bs.Parent_Code NOT IN (
//                                            " + Convert.ToString(AlreadyExportedIDs != string.Empty ? AlreadyExportedIDs : "0") + @"
//                        )
//                        GROUP BY bs.Parent_Code,[IPOSession_ID],[IPOSession_Name]
//                        ORDER BY bs.Parent_Code
//                         SET ARITHABORT  OFF 
//                    ";
//            try
//            {
//                _dbConnection.ConnectDatabase();
//                dt = _dbConnection.ExecuteQuery(query);

//            }
//            catch (Exception exception)
//            {
//                throw exception;
//            }
//            finally
//            {
//                _dbConnection.CloseDatabase();
//            }
//            return dt;
//        }
        #endregion
        //        public DataTable GetSMSFormat(string SMSCode)
        //        {
        //            DataTable dt = new DataTable();
        //            string query = string.Empty;
        //            query = @"  SELECT [ID]
        //                              ,[SMSID]
        //                              ,[Message]
        //                              ,[Message_Title]
        //                              ,[Remark]
        //                        FROM [dbksclCallCenter].[dbo].[tbl_MessageStoreHouse]
        //                        WHERE SMSID='" + SMSCode + @"'
        //                    ";
        //            try
        //            {
        //                _dbConnection.ConnectDatabase_SMSSender();
        //                dt = _dbConnection.ExecuteQuery_SMSSender(query);

        //            }
        //            catch (Exception exception)
        //            {
        //                throw exception;
        //            }
        //            finally
        //            {
        //                _dbConnection.CloseDatabase_SMSSender();
        //            }
        //            return dt;
        //        }

        public string GetSMSFormat_UITransApply(string SMSCode)
        {
            string result = string.Empty;
            string query = string.Empty;
            query = @"  SELECT [ID]
                              ,[SMSID]
                              ,[Message]
                              ,[Message_Title]
                              ,[Remark]
                        FROM [dbksclCallCenter].[dbo].[tbl_MessageStoreHouse]
                        WHERE SMSID='" + SMSCode + @"'
                    ";
            try
            {
                //_dbConnection.ConnectDatabase();
                DataTable dt = _dbConnection.ExecuteQuery_SMSSender(query);
                if (dt.Rows.Count == 1)
                    result = Convert.ToString(dt.Rows[0]["Message"]);

            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
            return result;
        }

        public List<DataRow> GetSMSText_Single_Successfull_UITransApplied(List<DataRow> dt, string SMSFormat)
        {
            List<DataRow> DtSMSResult = new List<DataRow>();

            string[] formatArray = SMSFormat.Split('_');

            foreach (DataRow dr in dt.ToList())
            {
                string Msg = formatArray[0] + "" + dr["Cust_Code"] + "" + formatArray[1] + "" + dr["Comp_Short_Code"] + "" + formatArray[2];

                dr["Message"] = Msg;
                //  dr["Status"] = 1;
            }

            DtSMSResult = dt;

            return DtSMSResult;
        }

        public List<DataRow> GetSMSText_Single_Unsucc_UITransApplied(List<DataRow> dt, string SMSFormat)
        {
            List<DataRow> DtSMSResult = new List<DataRow>();

            string[] formatArray = SMSFormat.Split('_');

            foreach (DataRow dr in dt.ToList())
            {
                string Msg = formatArray[0] + "" + dr["Cust_Code"] + "" + formatArray[1] + "''" + dr["Comp_Short_Code"] + "''" + formatArray[2]; ;
                dr["Message"] = Msg;
                // dr["Status"] = 1;
            }

            DtSMSResult = dt;

            return DtSMSResult;
        }

        public List<DataRow> GetSMSText_ParentChild_Succ_Unsucc_Together_UITransApplied(List<DataRow> dt, string SMSFormat)
        {
            List<DataRow> DtSMSResult = new List<DataRow>();

            string[] formatArray = SMSFormat.Split('_');

            foreach (DataRow dr in dt.ToList())
            {
                string Msg = formatArray[0] + dr["Succ_Clients"] + formatArray[1] + "''" + dr["Unsucc_Clients"] + "''" + formatArray[2] + "''" + dr["Comp_Short_Code"] + "''" + formatArray[3];

                // Unfortunately ID:_ Unsuccessfull to get_ IPO Share and money will be refunded within next two working days.

                dr["Message"] = Msg;
                //  dr["Status"] = 1;
            }

            DtSMSResult = dt;

            return DtSMSResult;


        }

        public List<DataRow> GetSMSText_ParentChild_AllUnsuc_UITransApplied(List<DataRow> dt, string SMSFormat)
        {
            List<DataRow> DtSMSResult = new List<DataRow>();

            string[] formatArray = SMSFormat.Split('_');

            foreach (DataRow dr in dt.ToList())
            {
                string Msg = formatArray[0] + dr["Unsucc_Clients"] + formatArray[1] + "''" + dr["Comp_Short_Code"] + "''" + formatArray[2];
                dr["Message"] = Msg;
                // dr["Status"] = 1;
            }

            DtSMSResult = dt;

            return DtSMSResult;
        }

        public List<DataRow> GetSMSText_ParentChild_AllSucc_UITransApplied(List<DataRow> dt, string SMSFormat)
        {
            List<DataRow> DtSMSResult = new List<DataRow>();

            string[] formatArray = SMSFormat.Split('_');

            foreach (DataRow dr in dt.ToList())
            {
                string Msg = formatArray[0] +"''"+ dr["Comp_Short_Code"]+"''" + formatArray[1] + "" + dr["Succ_Clients"] + "" + formatArray[2]; ;
                dr["Message"] = Msg;
            }

            DtSMSResult = dt;

            return DtSMSResult;
        }

        public List<DataRow> GetSMSText_Single_Approved_UITransApplied(List<DataRow> dt, string SMSFormat)
        {
            List<DataRow> DtSMSResult = new List<DataRow>();

            string[] formatArray = SMSFormat.Split('_');

            foreach (DataRow dr in dt.ToList())
            {
                string Msg = formatArray[0] + "''" + dr["Comp_Short_Code"] + "''" + formatArray[1] + dr["Cust_Code"] + formatArray[2];
                dr["Message"] = Msg;
            }

            DtSMSResult = dt;

            return DtSMSResult;
        }

        public List<DataRow> GetSMSText_Single_Rejected_UITransApplied(List<DataRow> dt, string SMSFormat)
        {
            List<DataRow> DtSMSResult = new List<DataRow>();

            string[] formatArray = SMSFormat.Split('_');

            foreach (DataRow dr in dt.ToList())
            {
                string Msg = formatArray[0] + "''" + dr["Comp_Short_Code"] + "''" + formatArray[1] + dr["Cust_Code"] + formatArray[2]; ;
                dr["Message"] = Msg;
            }

            DtSMSResult = dt;

            return DtSMSResult;
        }

        public List<DataRow> GetSMSText_ParentChild_Approved_Rejected_Together_UITransApplied(List<DataRow> dt, string SMSFormat)
        {
            List<DataRow> DtSMSResult = new List<DataRow>();

            string[] formatArray = SMSFormat.Split('_');

            foreach (DataRow dr in dt.ToList())
            {
                string Msg = formatArray[0] + "''" + dr["Comp_Short_Code"] + "''" + formatArray[1] + dr["Succ_Clients"] + formatArray[2] + dr["Unsucc_Clients"] + formatArray[3];
                dr["Message"] = Msg;
            }

            DtSMSResult = dt;

            return DtSMSResult;

        }

        public List<DataRow> GetSMSText_ParentChild_AllRejected_UITransApplied(List<DataRow> dt, string SMSFormat)
        {
            List<DataRow> DtSMSResult = new List<DataRow>();

            string[] formatArray = SMSFormat.Split('_');

            foreach (DataRow dr in dt.ToList())
            {
                string Msg = formatArray[0] + "''" + dr["Comp_Short_Code"] + "''" + formatArray[1] + dr["Unsucc_Clients"] + formatArray[2]; ;
                dr["Message"] = Msg;
            }

            DtSMSResult = dt;

            return DtSMSResult;
        }

        public List<DataRow> GetSMSText_ParentChild_AllApproved_UITransApplied(List<DataRow> dt, string SMSFormat)
        {
            List<DataRow> DtSMSResult = new List<DataRow>();

            string[] formatArray = SMSFormat.Split('_');

            foreach (DataRow dr in dt.ToList())
            {
                string Msg = formatArray[0] + "''" + dr["Comp_Short_Code"] + "''" + formatArray[1] + dr["Succ_Clients"] + formatArray[2]; ;
                dr["Message"] = Msg;
            }

            DtSMSResult = dt;

            return DtSMSResult;
        }

        public void InsertTable_SMSSyncExport_ForSingle_IPO_Confirmation_SMS_UITransApplied(List<DataRow> dt)
        {

            List<string> InsertQueryList = new List<string>();
            IPOProcessBAL ipoBal = new IPOProcessBAL();
            DataTable dt_RefundType = ipoBal.GetRefundMethod();

            //int i = 0;
            try
            {
                foreach (DataRow dr in dt.ToList())
                {
                    //if (i == 10326)
                    //{
                    //    string Kamal = "kamal";
                    //}

                    string temp_1 = @"INSERT INTO [tbl_IPO_Confirmation_SMS]
                                    (
	                                    [Cust_Code]
                                       ,[SMSType]
                                       ,[IsParentChild] 
                                       ,[ReferenceID]
                                       ,[IPO_Session_ID]
                                       ,[Trans_Reason]
                                       ,[Message]
                                       ,[Destination]
                                       ,[DeliveryDateTime]
                                       ,[Status]
                                    )
                                    VALUES
                                    (
	                                    '" + dr["Cust_Code"] + @"'
                                       ,'" + Indication_IPOPaymentTransaction.ApplicationSMSNotification_SMSType + @"'
                                       ,0
                                       ," + dr["IPOApplicationID"] + @"
                                       ," + dr["IPOSession_ID"] + @"
                                       ,'" + dr["Trans_Reason"] + @"'
                                       ,'" + dr["Message"] + @"'
                                       ,'" + dr["Destination"] + @"'
                                       ,'" + dr["DelivaryDate"] + @"'
                                       ,'" + dr["Status"] + @"'
                                    )";
                    _dbConnection.ExecuteNonQuery_SMSSender(temp_1);
                    //InsertQueryList.Add(temp_1);
                    //i++;
                }

            }
            catch (Exception exception)
            {
                //_dbConnection.Rollback();
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
        }

        public void InsertTable_SMSSyncExport_FroParentChild_IPO_Confirmation_SMS_UITransApplied(List<DataRow> dt)
        {

            List<string> InsertQueryList = new List<string>();
            IPOProcessBAL ipoBal = new IPOProcessBAL();
            DataTable dt_RefundType = ipoBal.GetRefundMethod();

            //int i = 0;
            try
            {
                foreach (DataRow dr in dt.ToList())
                {
                    //if (i == 10326)
                    //{
                    //    string Kamal = "kamal";
                    //}

                    string temp_1 = @"INSERT INTO [tbl_IPO_Confirmation_SMS]
                                    (
	                                    [Cust_Code]
                                       ,[SMSType]
                                       ,[IsParentChild]
                                       ,[ReferenceID]
                                       ,[IPO_Session_ID]
                                       ,[Trans_Reason]
                                       ,[Message]
                                       ,[Destination]
                                       ,[DeliveryDateTime]
                                       ,[Status]
                                    )
                                    VALUES
                                    (
	                                    '" + dr["Parent_Code"] + @"'
                                       ,'" + Indication_IPOPaymentTransaction.ApplicationSMSNotification_SMSType + @"'
                                       ,1 
                                       ," + dr["Parent_Code"] + @"
                                       ," + dr["IPOSession_ID"] + @"
                                       ,'" + dr["Trans_Reason"] + @"'
                                       ,'" + dr["Message"] + @"'
                                       ,'" + dr["Destination"] + @"'
                                       ,'" + dr["DelivaryDate"] + @"'
                                       ,'" + dr["Status"] + @"'
                                    )";
                    _dbConnection.ExecuteNonQuery_SMSSender(temp_1);
                    //InsertQueryList.Add(temp_1);
                    //i++;
                }

            }
            catch (Exception exception)
            {
                //_dbConnection.Rollback();
                throw exception;
            }
            finally
            {
                //_dbConnection.CloseDatabase();
            }
        }


    }
}