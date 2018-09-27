﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessAccessLayer.BAL;
using BusinessAccessLayer.Constants;

namespace StockbrokerProNewArch
{
    public partial class TradeFileProcess_FlexTrade : Form
    {
        public static string _oldCustCode;
        public static string _oldBo;
        public static int _IdforUpdate;
        
        private DataTable dtTradefileData = new DataTable();
             

        public TradeFileProcess_FlexTrade()
        {
            InitializeComponent();
            
        }     

        //private void ProcessInstrumentGroupChange(DataTable dtTradeInfo)
        //{
        //    try
        //    {
        //        DataView view = new DataView(dtTradeInfo);

        //        //Column2=Comp_Short_Code,Column18=Comp_Cat_Group
        //        DataTable distinctValues = view.ToTable(true, new string[] { "SecurityCode", "Category" });

        //        for (int i = 0; i < distinctValues.Rows.Count; i++)
        //        {
        //            string comp_Short_Code = distinctValues.Rows[i][0].ToString();
        //            string comp_Group = distinctValues.Rows[i][1].ToString();
        //            int Trade_file_GroupID;
        //            int CompanyGroupID;
                
        //            Trade_file_GroupID = Convert.ToInt32(queryInstrumentInfo.Where(t => t["Comp_Category"].ToString() == comp_Group).ToList()[0].ItemArray[0]);

        //            //if (queryInstrumentCompanyInfo.Any(p => p.Field<string>(0) == comp_Short_Code))
        //            //{
        //            //    MessageBox.Show("Exist");
        //            //}
        //            if (queryInstrumentCompanyInfo.Any(t => t.ItemArray[0].ToString() == comp_Short_Code))
        //            {
        //                CompanyGroupID =Convert.ToInt32(queryInstrumentCompanyInfo.Where(t => t["Comp_Short_Code"].ToString() == comp_Short_Code).ToList()[0].ItemArray[1]);
        //            }
        //            else
        //            {
        //                CompanyGroupID = Trade_file_GroupID;
        //            }
        //            if (Trade_file_GroupID != CompanyGroupID)
        //            {
        //                UpdateCompanyGroup(comp_Short_Code, Trade_file_GroupID);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        private void ProcessInstrumentGroupChange(DataTable dtPriceInfoDt)
        {
            try
            {
                List<DataRow> queryInstrumentInfo = new List<DataRow>();
                List<DataRow> queryInstrumentCompanyInfo = new List<DataRow>();

                TradePriceBAL Bal = new TradePriceBAL();

                DataTable dt_Temp_InstrumentGroup = new DataTable();
                DataTable dt_Temp_InstrumentGroupFromCompany = new DataTable();

                dt_Temp_InstrumentGroup = Bal.GetInstrumentGroupInfo();
                dt_Temp_InstrumentGroupFromCompany = Bal.GetInstrumentCompanyInfo();

                queryInstrumentInfo = dt_Temp_InstrumentGroup.Rows.Cast<DataRow>().ToList();
                queryInstrumentCompanyInfo = dt_Temp_InstrumentGroupFromCompany.Rows.Cast<DataRow>().ToList();


                //var data = dtPriceInfoDt.Rows.Cast<DataRow>().GroupBy(t => new { Comp_Short_Code = Convert.ToString(t["Instrument_Code"]), Category = Convert.ToString(t["Category"]) })
                //    .Select(t => new { Comp_Short_Code = t.Key.Comp_Short_Code, Category = t.Key.Category });

                var data = dtPriceInfoDt.Rows.Cast<DataRow>().GroupBy(t => new { Comp_Short_Code = Convert.ToString(t["SecurityCode"]), Category = Convert.ToString(t["Category"]) })
                    .Select(t => new { Comp_Short_Code = t.Key.Comp_Short_Code, Category = t.Key.Category });

                foreach (var eachData in data)
                {
                    string comp_Short_Code = eachData.Comp_Short_Code;
                    string comp_Group = eachData.Category;

                    var dataTemp = queryInstrumentInfo.Where(t => t["Comp_Category"].ToString() == comp_Group).SingleOrDefault().ItemArray[0];
                    if (dataTemp == null)
                        throw new Exception(comp_Group + " Category Not Found In Master Info");
                    int Comp_CatgID_FoundFrom_CompanyCatgInfo = Convert.ToInt32(dataTemp);


                    var dataTemp_1 = queryInstrumentCompanyInfo.Where(t => t["Comp_Short_Code"].ToString() == comp_Short_Code).SingleOrDefault().ItemArray[1];
                    if (dataTemp_1 == null)
                        throw new Exception(comp_Short_Code + " Company Not Found In Master Info");
                    int Comp_CatgID_FoundFrom_SBPCompanyInfo = Convert.ToInt32(dataTemp_1);

                    if (Comp_CatgID_FoundFrom_CompanyCatgInfo != Comp_CatgID_FoundFrom_SBPCompanyInfo)
                    {
                        UpdateCompanyGroup(comp_Short_Code, Comp_CatgID_FoundFrom_CompanyCatgInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void UpdateCompanyGroup(string comp_Short_Code, int groupId)
        {
            TradeBAL tradeBal = new TradeBAL();
            tradeBal.ChangeCompanyGroup(comp_Short_Code, groupId);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            ValidateBOID();
            ValidateCustCode();
            ValidateCustCodeBOID();
            ValidateCompany();
            ValdateISIN();
            ValidateCompanyCategory();
            ValidateGroupMisMatch();
            if (lbCustCodeError.Items.Count == 0 && lbBOError.Items.Count == 0 && lbCodeBOError.Items.Count==0 && lbCompShortCodeError.Items.Count == 0 && lbISINError.Items.Count == 0 && lbCompanyCatError.Items.Count == 0)
            {
                btnProcess.Enabled = true;
            }
            else
            {

                Height = 505;
            }
            if(dtgGroupMismatch.Rows.Count>0)
            {
                //btnChangeGroup.Enabled = true;
                btnChangeGroup.Enabled = true;
                btnProcess.Enabled = false;
            }
            else
            {
                //btnChangeGroup.Enabled = false;
                //btnChangeGroup.Enabled = true;
                btnChangeGroup.Enabled = true;
                btnProcess.Enabled = true;
            }
        }
       
        private void ValidateGroupMisMatch()
        {
            try
            {
                TradeBAL tradeBal = new TradeBAL();
                DataTable groupDataTable = new DataTable();
                groupDataTable = tradeBal.ValidateGroupMisMatch_FlexTrade();
                if (groupDataTable.Rows.Count > 0)
                {
                    dtgGroupMismatch.DataSource = groupDataTable;
                    dtgGroupMismatch.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    this.Size = new Size(874, 500);
                }
                else
                {

                    dtgGroupMismatch.Columns.Clear();
                }
            }
            catch (Exception exc)
            {

                MessageBox.Show("Group Mismatch Error." + exc.Message);
            }
        }

        private void ValidateCompanyCategory()
        {
            try
            {
                TradeBAL tradeBal = new TradeBAL();
                DataTable ctegoryDataTable = new DataTable();
                ctegoryDataTable = tradeBal.ValidateCompanyCategory_FlexTrade();
                if (ctegoryDataTable.Rows.Count > 0)
                {
                    lbCompanyCatError.Items.Clear();
                    for (int i = 0; i < ctegoryDataTable.Rows.Count; i++)
                    {
                        lbCompanyCatError.Items.Add(ctegoryDataTable.Rows[i]["Category"]);
                    }
                }
                else
                {
                    lbCompanyCatError.Items.Clear();
                }
            }
            catch (Exception exc)
            {

                MessageBox.Show("Company Validation Error. " + exc.Message);
            }
        }

        private void ValidateCustCodeBOID()
        {
            try
            {
                TradeBAL tradeBal = new TradeBAL();
                DataTable codeBODataTable = new DataTable();
                codeBODataTable = tradeBal.ValidateCustCodeBOID_FlexTrade();
                lbCodeBOError.Items.Clear();
                if (codeBODataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < codeBODataTable.Rows.Count; i++)
                    {
                        lbCodeBOError.Items.Add(codeBODataTable.Rows[i]["Cust_Code_BO"]);
                    }
                }
                else
                {
                    lbCodeBOError.Items.Clear();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Client Code & BO Id Mismatch Error." + exc.Message);
            }
        }

        private void ValdateISIN()
        {
            try
            {
                TradeBAL tradeBal = new TradeBAL();
                DataTable isinDataTable = new DataTable();
                isinDataTable = tradeBal.ValidateISIN_FlexTrade();
                if (isinDataTable.Rows.Count > 0)
                {
                    lbISINError.Items.Clear();
                    for (int i = 0; i < isinDataTable.Rows.Count; i++)
                    {
                        lbISINError.Items.Add(isinDataTable.Rows[i]["ISIN"]);

                    }
                }
                else
                {
                    lbISINError.Items.Clear();
                }
            }
            catch (Exception exc)
            {

                MessageBox.Show("ISIN Validation Error. " + exc.Message);
            }


        }

        private void ValidateCustCode()
        {
            try
            {
                TradeBAL tradeBal = new TradeBAL();
                DataTable custCodeDataTable = new DataTable();
                custCodeDataTable = tradeBal.ValidateCustCode_FlexTrade();
                if (custCodeDataTable.Rows.Count > 0)
                {
                    lbCustCodeError.Items.Clear();
                    for (int i = 0; i < custCodeDataTable.Rows.Count; i++)
                    {
                        lbCustCodeError.Items.Add(custCodeDataTable.Rows[i]["ClientCode"]);

                    }
                }
                else
                {
                    lbCustCodeError.Items.Clear();

                }
            }
            catch (Exception exc)
            {

                MessageBox.Show("Customer Code Validation Error. " + exc.Message);
            }
        }

        private void ValidateCompany()
        {
            try
            {
                TradeBAL tradeBal = new TradeBAL();
                DataTable companyDataTable = new DataTable();
                companyDataTable = tradeBal.ValidateCompany_FlexTrade();
                if (companyDataTable.Rows.Count > 0)
                {
                    lbCompShortCodeError.Items.Clear();
                    for (int i = 0; i < companyDataTable.Rows.Count; i++)
                    {
                        lbCompShortCodeError.Items.Add(companyDataTable.Rows[i]["SecurityCode"]);

                    }
                }
                else
                {
                    lbCompShortCodeError.Items.Clear();
                }

            }

            catch (Exception exc)
            {

                MessageBox.Show("Company Validation Error." + exc.Message);
            }

        }

        private void ValidateBOID()
        {
            try
            {
                TradeBAL tradeBal = new TradeBAL();
                DataTable boDataTable = new DataTable();
                boDataTable = tradeBal.ValidateBOID_FlexTrade();
                if (boDataTable.Rows.Count > 0)
                {
                    lbBOError.Items.Clear();
                    for (int i = 0; i < boDataTable.Rows.Count; i++)
                    {
                        lbBOError.Items.Add(boDataTable.Rows[i]["BOID"]);

                    }
                }
                else
                {
                    lbBOError.Items.Clear();
                }
            }
            catch (Exception exc)
            {

                MessageBox.Show("BO ID Validation Error. " + exc.Message);
            }

        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                TradeBAL tradeBal = new TradeBAL();
                tradeBal.SaveIntoTransaction_FlexTrade();
                MessageBox.Show("Transaction Data imported successfully.");
                this.Close();
            }
            catch (Exception exc)
            {

                MessageBox.Show("Transaction Data import operation failed. " + exc.Message);
            }
        }

        private void TradeFileProcess_Load(object sender, EventArgs e)
        {
            LoadDataIntoGrid();
            this.Size = new Size(874, 300);
        }

        private void LoadDataIntoGrid()
        {
            TradeBAL tradeFileBal = new TradeBAL();
            DataTable datatable = tradeFileBal.GetGridData_FlexTrade();
            dtgTradeFile.DataSource = datatable;
            dtgTradeFile.Columns[0].Visible = false;
            dtgTradeFile.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dtTradefileData = datatable;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dtgTradeFile.Rows.Count <= 0)
            {
                MessageBox.Show("No customer has been select for Edit", "Invalid Selection.");
                return;
            }
            LoadDataFromGrid();
            TradeCustCodeEditNew tradeCustCodeEdit = new TradeCustCodeEditNew(Indication_Forms_Title.TradeCust_CodeEditNew_FlexTrade);
            tradeCustCodeEdit.ShowDialog();
            LoadDataIntoGrid();
        }

        private void LoadDataFromGrid()
        {
            foreach (DataGridViewRow row in this.dtgTradeFile.SelectedRows)
            {
                if (dtgTradeFile[0, row.Index].Value != DBNull.Value)
                    _IdforUpdate =Convert.ToInt32(dtgTradeFile["ID", row.Index].Value);

                if (dtgTradeFile[2, row.Index].Value != DBNull.Value)
                    _oldBo = dtgTradeFile["BOID", row.Index].Value.ToString();

                if (dtgTradeFile[1, row.Index].Value != DBNull.Value)
                    _oldCustCode = dtgTradeFile["ClientCode", row.Index].Value.ToString();

                if (dtgTradeFile[2, row.Index].Value != DBNull.Value)
                    _oldBo = dtgTradeFile["BOID", row.Index].Value.ToString();

            }
        }

        private void dtgTradeFile_SelectionChanged(object sender, EventArgs e)
        {
            LoadDataFromGrid();
        }

        private void btnChangeGroup_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessInstrumentGroupChange(dtTradefileData);
                MessageBox.Show(@"Group Changed Successfully");
                this.Size = new Size(874, 300);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}