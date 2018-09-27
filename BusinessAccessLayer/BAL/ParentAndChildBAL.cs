﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;
using BusinessAccessLayer.BO;
using System.Data;

namespace BusinessAccessLayer.BAL
{
   public class ParentAndChildBAL
    {
       private DbConnection _dbconnection;
       public ParentAndChildBAL()
       {
           _dbconnection = new DbConnection();
       }

       public DataTable GetParentCode()
       {
           DataTable dt = new DataTable();
           string query = @"select Distinct Parent_Code from SBP_Parent_Child_Details";
           try
           {
               _dbconnection.ConnectDatabase();
               dt = _dbconnection.ExecuteQuery(query);
           }
           catch (Exception ex)
           {
               throw ex;
           }
           finally
           {
               _dbconnection.CloseDatabase();
           }
           return dt;
       }

       /// <summary>
       /// Update Registration Image and owner info
       /// Rashedul Hasan 2015 sep 03
       /// </summary>
       /// <param name="bo">Parent Child Registration's image update</param>
       /// <param name="P_Bo">Owner Name cell email gender</param>
       public void Update_Parent_Registration_Image(ParentChildRegistrationBO bo, ParentAndChildBO P_Bo)
       {
           string query = "SBP_Update_ParentChildInfo";
           try
           {
               _dbconnection.ConnectDatabase();
               // _dbconnection.ActiveStoredProcedure();
               if (bo.Registration_Image != null)
                   _dbconnection.AddParameter("@Registration_Image", SqlDbType.Image, bo.Registration_Image);
               else
                   _dbconnection.AddParameter("@Registration_Image", SqlDbType.Image, DBNull.Value);

               _dbconnection.AddParameter("@Update_By", SqlDbType.VarChar, GlobalVariableBO._userName);
               if (string.IsNullOrEmpty(bo.Parent_Code))
                   _dbconnection.AddParameter("@Parent_Code", SqlDbType.VarChar, DBNull.Value);
               else
                   _dbconnection.AddParameter("@Parent_Code", SqlDbType.VarChar, bo.Parent_Code);
               if (string.IsNullOrEmpty(P_Bo.Owner_cell))
                   _dbconnection.AddParameter("@Hadeler_Contact_Mobile", SqlDbType.VarChar, P_Bo.Owner_cell);
               else
                   _dbconnection.AddParameter("@Hadeler_Contact_Mobile", SqlDbType.VarChar, P_Bo.Owner_cell);
               if (string.IsNullOrEmpty(P_Bo.Owner_Email_1))
                   _dbconnection.AddParameter("@Handeler_Email_1", SqlDbType.VarChar, DBNull.Value);
               else
                   _dbconnection.AddParameter("@Handeler_Email_1", SqlDbType.VarChar, P_Bo.Owner_Email_1);
               if (string.IsNullOrEmpty(P_Bo.Parent_Gender))
                   _dbconnection.AddParameter("@Hadeler_Gender", SqlDbType.VarChar, DBNull.Value);
               else
                   _dbconnection.AddParameter("@Hadeler_Gender", SqlDbType.VarChar, P_Bo.Parent_Gender);
               if (string.IsNullOrEmpty(P_Bo.Parent_Name))
                   _dbconnection.AddParameter("@Handeler_Parent_Name", SqlDbType.VarChar, DBNull.Value);
               else
                   _dbconnection.AddParameter("@Handeler_Parent_Name", SqlDbType.VarChar, P_Bo.Parent_Name);


               _dbconnection.ExecuteProQuery(query);
           }
           catch (Exception ex)
           {
               throw ex;
           }

       }

       #region Delete Info
       /// <summary>
       /// Delete parent child details 
       /// Md.Rashedul Hasan on 06 sep 2015
       /// </summary>
       /// <param name="Cust_Code">Delete Cust code</param>
       public void Delete_Parent_Child_Details(string Cust_Code)
       {
           string query = @"SBP_Delte_Parent_Child_Info";
           try
           {
               _dbconnection.ConnectDatabase();
               _dbconnection.ActiveStoredProcedure();
               _dbconnection.AddParameter("@Child_Code", SqlDbType.VarChar, Cust_Code);
               _dbconnection.AddParameter("@Delte_By", SqlDbType.VarChar, GlobalVariableBO._userName);
               _dbconnection.ExecuteProQuery(query);
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
           finally
           {
               _dbconnection.CloseDatabase();
           }
       }
       #endregion

       #region ParentChildReport
       public DataTable GetAllChild_ShareAndMoney_Information(string Parent_Code)
       {
           DataTable dt = new DataTable();
           string query = @"RptParentChildAllInfo";
           try
           {
               _dbconnection.ConnectDatabase();
               _dbconnection.AddParameter("@ChildCode", SqlDbType.VarChar, Parent_Code);
               dt = _dbconnection.ExecuteProQuery(query);
           }
           catch (Exception ex)
           {
               throw ex;
           }
           finally
           {
               _dbconnection.CloseDatabase();
           }
           return dt;
       }
       #endregion

       #region Parent Info
       public DataTable GetparentChildInfo(string id)
       {
           DataTable dt = new DataTable();
           string query = @"select 
	                        SC.[Cust_Code] AS 'CODE'
	                        ,SC.[BO_ID] AS 'BO ID'
	                        ,SCPI.[Cust_Name] AS 'NAME'
	                        ,SCPI.[Gender] AS 'GENDER'
	                        ,SCPI.[Occupation] AS 'PROFESSION'
	                        ,SCCI.[Address1] AS 'ADDRESS'
	                        ,SCCI.[Phone] AS 'PHONE'
	                        ,SCCI.[Mobile] AS 'CELL'
	                        ,SCCI.[Email] AS 'MAIL'
                        from 
                        [SBP_Customers] as SC
                        INNER JOIN [SBP_Cust_Personal_Info] AS SCPI
                        ON SC.[Cust_Code]=SCPI.[Cust_Code]
                        INNER JOIN [SBP_Cust_Contact_Info] AS SCCI
                        ON SC.[Cust_Code]=SCCI.[Cust_Code]
                        WHERE SC.[Cust_Code]='"+id+"'";
           try
           {
               _dbconnection.ConnectDatabase();
               dt = _dbconnection.ExecuteQuery(query);
           }
           catch (Exception ex)
           {
               throw ex;
           }
           finally
           {
               _dbconnection.CloseDatabase();
           }
           return dt;
       }

       public DataTable GetChildInfo(string id)
       {
           DataTable dt = new DataTable();
           string query = @"select 
	                        SC.[Cust_Code] AS 'CODE'
	                        ,SC.[BO_ID] AS 'BO ID'
	                        ,SCPI.[Cust_Name] AS 'NAME'
	                        ,SCPI.[Gender] AS 'GENDER'
	                        ,SCPI.[Occupation] AS 'PROFESSION'
	                        ,SCCI.[Address1] AS 'ADDRESS'
	                        ,SCCI.[Phone] AS 'PHONE'
	                        ,SCCI.[Mobile] AS 'CELL'
	                        ,SCCI.[Email] AS 'MAIL'
                        from 
                        [SBP_Customers] as SC
                        INNER JOIN [SBP_Cust_Personal_Info] AS SCPI
                        ON SC.[Cust_Code]=SCPI.[Cust_Code]
                        INNER JOIN [SBP_Cust_Contact_Info] AS SCCI
                        ON SC.[Cust_Code]=SCCI.[Cust_Code]
                        WHERE SC.[Cust_Code]='" + id + "'";
           try
           {
               _dbconnection.ConnectDatabase();
               dt = _dbconnection.ExecuteQuery(query);
           }
           catch (Exception ex)
           {
               throw ex;
           }
           finally
           {
               _dbconnection.CloseDatabase();
           }
           return dt;
       }

       public string checkCustCode(string id)
       {
           string code = "";
           DataTable dt = new DataTable();
           string query = @"select cust_code from sbp_customers where cust_code= '"+id+"'";
           try
           {
               _dbconnection.ConnectDatabase();
               dt=_dbconnection.ExecuteQuery(query);
           }
           catch (Exception ex)
           {
               throw ex;
           }
           finally
           {
               _dbconnection.CloseDatabase();
           }
           if (dt.Rows.Count > 0)
           {
               code = dt.Rows[0][0].ToString();
           }
           return code;
       }

       public string CheckParentandchildCode(string Parentid,string Child,string Ownerid)
       {
           string code = "";
           DataTable dt = new DataTable();
           string query = "";
           if (Child != "")
           {
               query = @"select Child_code from [SBP_Parent_Child_Details] where Child_Code='" + Child + "'";
           }
           else if (Parentid != "")
           {
               query = @"select Parent_Code from [SBP_Parent_Child_Details] where Parent_Code='" + Parentid + "'";
           }
           else
           {
               query = @"select [Parent_Id] from [SBP_Parent_Child_Owner_Details] where [Parent_Id]='" + Ownerid + "'";
           }
           try
           { 
               _dbconnection.ConnectDatabase();
               dt = _dbconnection.ExecuteQuery(query);               
           }
           catch (Exception ex)
           {
               throw ex;
           }
           finally
           {
               _dbconnection.CloseDatabase();
           }
           if (dt.Rows.Count > 0)
           {
               code = dt.Rows[0][0].ToString();
           }
           return code;

       }

       public DataTable GetAllParentChildOwnerInfo(string ChildCode)
       {
           DataTable dt = new DataTable();
           string query = @"DECLARE @Parent_Cust_Code Varchar(100)
                                   Set @Parent_Cust_Code= (Select distinct Parent_Code 
                                From  SBP_Parent_Child_Details 
                                Where Parent_Code=(
					                      Select Parent_Code From SBP_Parent_Child_Details Where Child_Code='"+ChildCode+ @"'))--@Parent_Cust_Code)
                                Select 
                                Pr.Parent_Code As [Parent_Code]
                                ,Pr.Child_Code As [Child_Code]
                                ,own.Parent_Id As [Owner Code]
                                ,Pr.Child_BOID As [Bo_ID]
                                ,own.Handeler_BOID As [Owner BoID]
                                ,Pr.Handeler_Name As [Handler_Name]
                                ,Own.Handeler_Parent_Name As [Owner Name]
                                ,own.Handeler_Name 
                                ,Pr.Parent_Name As [Parent_Name]
                                ,Pr.Parent_Gender As [Parent_Gender]
                                ,Pr.Parent_Profession As [Parent_Profession]
                                ,own.Handelr_Occupation As [Owner Profession]
                                ,Pr.Parent_Email As [Parent Email]
                                ,Pr.Parent_Contact_Mobile As [Parent Cell]
                                ,own.Hadeler_Contact_Mobile As [Owner Cell]
                                ,Pr.Parent_Contact_Land As [Parent_Land]
                                ,own.Handeler_Contact_Land As [Owner Land]
                                ,own.Handeler_Email_1 As [Owner Mail]
                                ,own.Handeler_Permanent_Address As [Owner Permanent Address]
                                ,own.Handeler_Present_Address As [Owner Present Address]
                                ,Pr.Parent_Address_Permanet As [parent_Permanent_Address]
                                ,Pr.Parent_Address_Present As [Parent_Address_Present]
                                From SBP_Parent_Child_Details As Pr 
                                Left Join 
                                SBP_Parent_Child_Owner_Details As own
                                On Pr.Parent_Code=own.Parent_Id
                                Where Pr.Parent_Code=@Parent_Cust_Code";
           try
           {
               _dbconnection.ConnectDatabase();
               dt = _dbconnection.ExecuteQuery(query);
           }
           catch (Exception ex)
           {
               throw ex;
           }
           finally
           {
               _dbconnection.CloseDatabase();
           }
           return dt;
       }

       public DataTable GetAllParentInfo(string Parent,string child,string owner)
       {
           DataTable dtcode = new DataTable();
           DataTable dtparent = new DataTable();
           string parentcode = @"select distinct Parent_Code from [SBP_Parent_Child_Details] where Parent_Code='" + Parent + "'";
           string ChildCode = @"select distinct Child_Code from [SBP_Parent_Child_Details] where Child_Code='" + child + "'";
           string Owner = @"select distinct Parent_ID from [SBP_Parent_Child_Owner_Details] where Parent_Id='" + owner + "'";
           string AllChildCode = @"DECLARE @Parent_Cust_Code Varchar(100)='" + Parent + @"'
                                Select Child_Code 
                                From  SBP_Parent_Child_Details 
                                Where Parent_Code=(
					                                Select Parent_Code From SBP_Parent_Child_Details Where Child_Code=@Parent_Cust_Code)";
           try
           {
               _dbconnection.ConnectDatabase();
               string code = "";
               if (Parent != "")
               {
                   dtcode = _dbconnection.ExecuteQuery(parentcode);
                   code = dtcode.Rows[0][0].ToString();
                   if (dtcode.Rows.Count > 0)
                   {
                       string query = @"select * from [SBP_Parent_Child_Details] where Parent_Code ='" + code + "'";
                       dtparent = _dbconnection.ExecuteQuery(query);
                   }
               }
               else if (child != "")
               {
                   dtcode = _dbconnection.ExecuteQuery(ChildCode);
                   code = dtcode.Rows[0][0].ToString();
                   if (dtcode.Rows.Count > 0)
                   {
                       string query = @"select * from [SBP_Parent_Child_Details] where Child_code ='" + code + "'";
                       dtparent = _dbconnection.ExecuteQuery(query);
                   }
               }
               else
               {
                   dtcode = _dbconnection.ExecuteQuery(owner);
                   code = dtcode.Rows[0][0].ToString();
                   if (dtcode.Rows.Count > 0)
                   {
                       string query = @"select * from [SBP_Parent_Child_Owner_Details] where Parent_Id ='" + code + "'";
                       dtparent = _dbconnection.ExecuteQuery(query);
                   }
               }
               //string code = dtcode.Rows[0][0].ToString();
               //if (dtcode.Rows.Count > 0)
               //{
               //    string query = @"select * from [SBP_PARENT_INFO] where Parent_Code ='" + code + "'";
               //    dtparent = _dbconnection.ExecuteQuery(query);
               //}
           }
           catch (Exception ex)
           {
               throw ex;
           }
           finally
           {
               this._dbconnection.CloseDatabase();
           }
           return dtparent;
       }

       public DataTable GetownerInfo(string id)
       {
           DataTable dt = new DataTable();
           string query = @"select * from [SBP_Parent_Child_Owner_Details] where [Parent_Id]='" + id + "'";
           try
           {
               _dbconnection.ConnectDatabase();
               dt=_dbconnection.ExecuteQuery(query);
           }
           catch (Exception ex)
           {
               throw ex;
           }
           finally
           {
               _dbconnection.CloseDatabase();
           }
           return dt;
       }

       public void DeleteClient(string Child)
       {
           string   query = @"delete from [SBP_Parent_Child_Details] where Child_Code='" + Child + "'";
           try
           {
               _dbconnection.ConnectDatabase();               
               _dbconnection.ExecuteNonQuery(query);              
                
           }
           catch (Exception ex)
           {
               throw ex;
           }
           finally
           {
               _dbconnection.CloseDatabase();
           }
       }

       
       public DataTable GetOwnergridData()
       {
           DataTable dt = new DataTable();
           string query = @"select * from [SBP_Parent_Child_Owner_Details]";
           try
           {
               _dbconnection.ConnectDatabase();
               dt = _dbconnection.ExecuteQuery(query);
           }
           catch (Exception ex)
           {
               throw ex;
           }
           finally
           {
               _dbconnection.CloseDatabase();
           }
           return dt;
       }
       
       public void SaveParentInfo(ParentAndChildBO pBo)
       {
           CommonBAL com = new CommonBAL();
           string query = @"INSERT INTO [SBP_Parent_Child_Details]
           (
            Parent_Code
            ,[Child_Code]
           ,[Child_BOID]
           ,[Handeler_Name]
           ,[Parent_Name]
           ,[Parent_Gender]
           ,[Parent_Profession]
           ,[Parent_Email]
           ,[Parent_Contact_Mobile]
           ,[Parent_Contact_Land]
           ,[Parent_Address_Present]
           ,[Parent_Address_Permanet]
            ,[Entry_By]
            ,[Entry_Date])
     VALUES 
           ('" + pBo.Parent_Code + "','"
               + pBo.Child_Code + "','"
              + pBo.Chil_BoId + "','"
           + com.HandlingSingelQuation(pBo.Handeler_Name) + "','"
           + com.HandlingSingelQuation(pBo.Parent_Name) + "','"
           + com.HandlingSingelQuation(pBo.Parent_Gender) + "','"
           + com.HandlingSingelQuation(pBo.Parent_Profession) + "','"
           + com.HandlingSingelQuation(pBo.parent_Email) + "','"
           + pBo.Parent_Cell + "','"
           + pBo.parent_land + "','"
           + com.HandlingSingelQuation(pBo.Parent_Present_addr) + "','"
           + com.HandlingSingelQuation(pBo.parent_Permanent_add) + "','"
           +com.HandlingSingelQuation(GlobalVariableBO._userName)+"',"
           + ("getdate()") + ")";           
           try
           {
               _dbconnection.ConnectDatabase();
               _dbconnection.ExecuteNonQuery(query);
           }
           catch (Exception ex)
           {
               throw ex;
           }
           finally
           {
               _dbconnection.CloseDatabase();
           }
       }
       #endregion

       #region Owner Info

       public void SaveOwnerInfo(ParentAndChildBO pBo,int RegistrationId)
       {
           string query = @"InserOwnerDetailsInfo";
           try
           {
               _dbconnection.ConnectDatabase();
               _dbconnection.ActiveStoredProcedure();
               _dbconnection.AddParameter("@Registration_Id ", SqlDbType.Int, RegistrationId);
               _dbconnection.AddParameter("@Parent_Id", SqlDbType.Int, pBo.Parent_Code);               
               _dbconnection.AddParameter("@Handeler_Parent_Name", SqlDbType.VarChar, pBo.Owner_name);              
               _dbconnection.AddParameter("@Hadeler_Gender", SqlDbType.VarChar, pBo.Owner_gender);
               _dbconnection.AddParameter("@Hadeler_Contact_Mobile", SqlDbType.VarChar, pBo.Owner_cell);
               _dbconnection.AddParameter("@Handeler_Contact_Land", SqlDbType.VarChar, pBo.Owner_land);
               _dbconnection.AddParameter("@Handeler_Email_1", SqlDbType.VarChar, pBo.Owner_email);
               _dbconnection.AddParameter("@Handeler_Email_2", SqlDbType.VarChar, pBo.Owner_Email_1);
               _dbconnection.AddParameter("@Handeler_Email_3", SqlDbType.VarChar, pBo.Owner_Email_2);
               _dbconnection.AddParameter("@Handelr_Occupation", SqlDbType.VarChar, pBo.Owner_profession);
               _dbconnection.AddParameter("@Handeler_Present_Address", SqlDbType.VarChar, pBo.Owner_present_addr);
               _dbconnection.AddParameter("@Handeler_Permanent_Address", SqlDbType.VarChar, pBo.Owner_permanent_add);
               _dbconnection.AddParameter("@Entry_By", SqlDbType.VarChar, GlobalVariableBO._userName);
               _dbconnection.ExecuteProQuery(query);
           }
           catch (Exception ex)
           {
               throw ex;
           }
           finally
           {
               _dbconnection.CloseDatabase();
           }
            
       }

       public void UpdateParentChildOwnerDetails(ParentAndChildBO bo)
       {
           string updateQuery = @"UPDATE [SBP_Database].[dbo].[SBP_Parent_Child_Owner_Details]
                               SET [Parent_Id] = '" + bo.Parent_Code + @"'
                                  ,[Handeler_BOID] = '" + bo.Owner_boid + @"'
                                  ,[Handeler_Parent_Name] = '" + bo.Owner_name + @"'                               
                                  ,[Hadeler_Gender] = '" + bo.Owner_gender + @"'
                                  ,[Hadeler_Contact_Mobile] = '" + bo.Owner_cell + @"'
                                  ,[Handeler_Contact_Land] = '" + bo.Owner_land + @"'
                                  ,[Handeler_Email_1] = '" + bo.Owner_email + @"'
                                  ,[Handeler_Email_2] = '" + bo.Owner_Email_1 + @"'
                                  ,[Handeler_Email_3] = '" + bo.Owner_Email_2 + @"'
                                  ,[Handelr_Occupation] = '" + bo.Owner_profession + @"'
                                  ,[Handeler_Present_Address] = '" + bo.Owner_present_addr + @"'
                                  ,[Handeler_Permanent_Address] = '" + bo.Owner_permanent_add + @"'
                                  ,[Update_By]='"+GlobalVariableBO._userName+ @"'
                                    ,[Update_Date]=GetDate()
                             WHERE  [Parent_Id]='" + bo.Parent_Code + "'";
           try
           {
               _dbconnection.ConnectDatabase();
               _dbconnection.ExecuteNonQuery(updateQuery);
           }
           catch (Exception ex)
           {
               throw ex;
           }
           finally
           {
               _dbconnection.CloseDatabase();
           }
       }

        #endregion


       #region Parent Child Image Info
       public byte[] GetParetnIamge(string parentCode)
       {
           byte[] Image = null;
           DataTable dt = new DataTable();
           string query = @"Select cust.Photo From SBP_Cust_Image As cust
                            Inner Join SBP_Parent_Child_Details As dtl
                            On cust.Cust_Code=dtl.Parent_Code
                            Where cust.Cust_Code='"+parentCode+"' ";
           try
           {
               _dbconnection.ConnectDatabase();
               dt = _dbconnection.ExecuteQuery(query);
               if (dt.Rows.Count > 0)
               {
                   if (dt.Rows[0][0] != DBNull.Value)
                   {
                       Image = (byte[])dt.Rows[0][0];
                   }
               }
           }
           catch (Exception)
           {
               throw;
           }
           finally
           {
               _dbconnection.CloseDatabase();
           }
           return Image;
       }

       public byte[] GetRegisterOwnerImage(string parentcode)
       {
           DataTable dt = new DataTable();
           byte[] image = null;
           string query = @"Select                              
                            Reg.Reg_Owner_Image                            
                            From SBP_Parent_Child_Registration As Reg
                            Where Reg.Parent_Code='" + parentcode + "'";
           try
           {
               _dbconnection.ConnectDatabase();
               dt = _dbconnection.ExecuteQuery(query);
               if (dt.Rows.Count > 0)
               {
                   if (dt.Rows[0][0] != DBNull.Value)
                   {
                       image = (byte[])dt.Rows[0][0];
                   }
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
           finally
           {
               _dbconnection.CloseDatabase();

           }
           return image;
       }

       public byte[] GetAllRegistrationImage(string ParentCode)
       {
           DataTable dt=new DataTable();
           byte[] image=null;
           string query = @"Select 
                            Reg.Registration_Image
                            From SBP_Parent_Child_Registration As Reg
                            Where Reg.Parent_Code='"+ParentCode+"'";
           try
           {
               _dbconnection.ConnectDatabase();
               dt = _dbconnection.ExecuteQuery(query);
               if (dt.Rows.Count > 0)
               {
                   if (dt.Rows[0][0] != DBNull.Value)
                   {
                       image = (byte[])dt.Rows[0][0];
                   }
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
           finally
           {
               _dbconnection.CloseDatabase();

           }
           return image;
       }

       public DataTable GetParentChildImgeAccountInfo(string code)
       {
           DataTable dt = new DataTable();
           string query = @"exec CustomerInfo '" + code + "',''";
           try
           {
               _dbconnection.ConnectDatabase();
               _dbconnection.ActiveStoredProcedure();

               dt = _dbconnection.ExecuteProByText(query);
           }
           catch (Exception ex)
           {
               throw ex;
           }
           finally
           {
               this._dbconnection.CloseDatabase();
           }
           return dt;
       }

       public void DeleteParentChild_LOG(int parent)
       {
           CommonBAL com=new CommonBAL();
           string query = @"ParentChildDeleteLog";  
           try
           {
               _dbconnection.ConnectDatabase();
               _dbconnection.AddParameter("@Parent_Code", SqlDbType.Int, parent);
               _dbconnection.AddParameter("@User_Name", SqlDbType.VarChar, GlobalVariableBO._userName);              
               _dbconnection.ExecuteProQuery(query);
           }
           catch (Exception ex)
           {
               throw ex;
           }
           finally
           {
               _dbconnection.CloseDatabase();
           }

       }

       public void DeleteRegistrationId(string ParentID)
       {
           DataTable dt = new DataTable();
           string query = @"Begin Try
	                        Begin Transaction
		                        
	                        Commit Transaction
                        End try
                        Begin Catch
	                        rollback transaction
                        End Catch
                            ";
           try
           {
               _dbconnection.ConnectDatabase();
               _dbconnection.ExecuteNonQuery(query);
           }
           catch (Exception ex)
           {
               throw ex;
           }
           finally
           {
               _dbconnection.CloseDatabase();
           }
       }

       //public DataTable GetRegistrationIdAndParentCode()
       //{
       //    DataTable dt = new DataTable();
       //    string query = @"Select Registration_ID,Parent_Code From SBP_Parent_Child_Registration order by Registration_ID desc";
       //    try
       //    {
       //        _dbconnection.ConnectDatabase();
       //        dt = _dbconnection.ExecuteQuery(query);
       //    }
       //    catch (Exception ex)
       //    {
       //        throw ex;
       //    }
       //    finally
       //    {
       //        _dbconnection.CloseDatabase();
       //    }
       //    return dt;
       //}

       public void InsertParentChildRegistrationInfo(ParentChildRegistrationBO bo)
       {
           string arrayJoin = "";
           if (bo.Child_Code != null)
           {
                arrayJoin = string.Join(",", bo.Child_Code);
           }
           string query = @"InsertParentChildRegistrationInfo";
           try
           {
               _dbconnection.ConnectDatabase();
               _dbconnection.ActiveStoredProcedure();
               if(bo.Registration_Image!=null)
               _dbconnection.AddParameter("@RegImage",SqlDbType.Image, bo.Registration_Image);
               else
                   _dbconnection.AddParameter("@RegImage", SqlDbType.Image, DBNull.Value);               
               _dbconnection.AddParameter("@EntryByName", SqlDbType.VarChar, bo.UserName);
               _dbconnection.AddParameter("@parentCode", SqlDbType.VarChar, bo.Parent_Code);
               //_dbconnection.AddParameter("@ChildCode", SqlDbType.VarChar, arrayJoin);
               _dbconnection.ExecuteProQuery(query);
           }
           catch (Exception ex)
           {
               //throw ex;
           }
       }

       public string[] GetallChildCode(string ParentId)
       {
           string[] strarr=null;
           DataTable dt = new DataTable();
           string query = @"select Child_Code from SBP_Parent_Child_Details where Parent_Code=(Select Parent_Code From SBP_Parent_Child_Details Where Child_Code='" + ParentId + "')";
           try
           {
               _dbconnection.ConnectDatabase();
               dt = _dbconnection.ExecuteQuery(query);
           }
           catch (Exception ex)
           {
               throw ex;
           }
           finally
           {
               _dbconnection.CloseDatabase();
           }
           if (dt.Rows.Count > 0)
           {
               strarr = dt.Rows.Cast<DataRow>().Select(x => x[0].ToString()).ToArray();
           }
           return strarr;
       }

       public void UpdateRegistraionChildCode(string[] ChildCode,string parent)
       {
           string arrayJoin = string.Join(",", ChildCode);
           string UpdateQuey = "update  SBP_Parent_Child_Registration set Child_Code='" + arrayJoin + "' where Parent_Code='" + parent + "'";
           string UpdateParentDetailsRegistrationId = @"Update SBP_Parent_Child_Details  Set SBP_Parent_Child_Details.registration_ID=(select rr.Registration_ID
                                                        From SBP_Parent_Child_Registration As rr 
                                                        Where rr.Parent_Code= '" + parent + @"')
                                                            where  SBP_Parent_Child_Details.Parent_Code= '" + parent + "'"
               ;
           try
           {
               _dbconnection.ConnectDatabase();
               //_dbconnection.ExecuteNonQuery(UpdateQuey);
               _dbconnection.ExecuteNonQuery(UpdateParentDetailsRegistrationId);
           }
           catch (Exception ex)
           {
               throw ex;
           }
           finally
           {
               _dbconnection.CloseDatabase();
           }
       }

       public string CheckParentCodeIsExist(string Parent_Code)
       {
           DataTable dt = new DataTable();
           string Parent = "";
           string query=@"Select distinct Parent_Code From SBP_Parent_Child_Details Where Parent_Code='"+Parent_Code+"'";
           try
           {
               _dbconnection.ConnectDatabase();
               dt=_dbconnection.ExecuteQuery(query);
           }
           catch (Exception ex)
           {
               throw ex;
           }
           if (dt.Rows.Count > 0)
           {
               Parent = dt.Rows[0]["Parent_Code"].ToString();
           }
           return Parent;

       }


        #endregion

       public string CheckPaentCodeForParentcodesearchFromRegistration(string parent_cod)
       {
           DataTable dt = new DataTable();
           string pr = "";
           string query = @"Select Parent_Code From SBP_Parent_Child_Registration where Parent_Code='" + parent_cod + "'";
           try
           {
               _dbconnection.ConnectDatabase();
               dt = _dbconnection.ExecuteQuery(query);
           }
           catch (Exception ex)
           {
               throw ex;
           }
           if (dt.Rows.Count > 0)
           {
               pr = dt.Rows[0]["Parent_Code"].ToString();
           }
           return pr;
       }
       public string CheckParentCodeFromReistration(string parent_cod)
       {
           DataTable dt = new DataTable();
           string pr = "";
           string query = @"Select Registration_ID From SBP_Parent_Child_Registration where Parent_Code='"+parent_cod+"'";
           try
           {
               _dbconnection.ConnectDatabase();
               dt = _dbconnection.ExecuteQuery(query);
           }
           catch (Exception ex)
           {
               throw ex;
           }
           if (dt.Rows.Count > 0)
           {
               pr = dt.Rows[0]["Registration_ID"].ToString();
           }
           return pr;
       }

       public string CheckParentCodeFromRegistration(string[] Child)
       {
           string ChildJoin = string.Join(",", Child);
           DataTable dt = new DataTable();
           string ParentCode = "";
           string query = @"Select Parent_Code from SBP_Parent_Child_Registration where Parent_Code in (" + ChildJoin + ")";
           try
           {
               _dbconnection.ConnectDatabase();
               dt = _dbconnection.ExecuteQuery(query);
           }
           catch (Exception ex)
           {
               throw ex;
           }
           finally
           {
               _dbconnection.ConnectDatabase();
           }
           if (dt.Rows.Count > 0)
           {
               ParentCode = dt.Rows.Cast<DataRow>().Select(x => x[0].ToString()).FirstOrDefault();
           }
           return ParentCode;
       }
       public DataTable GetAllownerInformation()
       {
           DataTable dt = new DataTable();
           string query = @"select * from SBP_Parent_Child_Owner_Details Order By Owner_ID,Entry_Date desc";
           try
           {
               _dbconnection.ConnectDatabase();
               dt = _dbconnection.ExecuteQuery(query);
           }
           catch (Exception ex)
           {
               throw ex;
           }
           finally
           {
               _dbconnection.CloseDatabase();
           }
           return dt;
       }

       public DataTable GetAllParentChildDetails()
       {
           DataTable dt = new DataTable();
           string query = @"Select 
                                Pr.Parent_Code As [Parent_Code]
                                ,Pr.Child_Code As [Child_Code]
                                ,own.Parent_Id As [Owner Code]
                                ,Pr.Child_BOID As [Bo_ID]
                                ,own.Handeler_BOID As [Owner BoID]
                                ,Pr.Handeler_Name As [Handler_Name]
                                ,Own.Handeler_Parent_Name As [Owner Name]
                                ,own.Handeler_Name 
                                ,Pr.Parent_Name As [Parent_Name]
                                ,Pr.Parent_Gender As [Parent_Gender]
                                ,Pr.Parent_Profession As [Parent_Profession]
                                ,own.Handelr_Occupation As [Owner Profession]
                                ,Pr.Parent_Email As [Parent Email]
                                ,Pr.Parent_Contact_Mobile As [Parent Cell]
                                ,own.Hadeler_Contact_Mobile As [Owner Cell]
                                ,Pr.Parent_Contact_Land As [Parent_Land]
                                ,own.Handeler_Contact_Land As [Owner Land]
                                ,own.Handeler_Email_1 As [Owner Mail]
                                ,own.Handeler_Permanent_Address As [Owner Permanent Address]
                                ,own.Handeler_Present_Address As [Owner Present Address]
                                ,Pr.Parent_Address_Permanet As [parent_Permanent_Address]
                                ,Pr.Parent_Address_Present As [Parent_Address_Present]
                                From SBP_Parent_Child_Details As Pr 
                                Left Join 
                                SBP_Parent_Child_Owner_Details As own
                                On Pr.Parent_Code=own.Parent_Id order by Pr.Parent_Child_Id desc";
           try
           {
               _dbconnection.ConnectDatabase();
               dt=_dbconnection.ExecuteQuery(query);
           }
           catch (Exception ex)
           {
               throw ex;
           }
           finally
           {
               _dbconnection.CloseDatabase();
           }
           return dt;
       }

       public DataTable LoadparentInfo(string ChildCode)
       {
           DataTable dt = new DataTable();
           string Query = @"select Distinct Pr.Cust_Code, Pr.Cust_Name,cust.BO_ID from SBP_Parent_Child_Details As dtl
                            Join SBP_Cust_Personal_Info As Pr
                            On Pr.Cust_Code=dtl.Parent_Code
                            Join SBP_Customers As cust
                            On cust.Cust_Code=dtl.Parent_Code
                            where Parent_Code=(Select Parent_Code From SBP_Parent_Child_Details Where Child_Code='"+ChildCode+"')";
           try
           {
               _dbconnection.ConnectDatabase();
               dt = _dbconnection.ExecuteQuery(Query);
           }
           catch (Exception ex)
           {
               throw ex;
           }
           return dt;
       }

       public DataTable GetRegistrationIdAndParentCode_ForOwner()
       {
           DataTable dt = new DataTable();
           string query = @"Select reg.Parent_Code,reg.Registration_ID
                            From SBP_Parent_Child_Registration as reg
                            WHERE reg.Parent_Code NOT IN(
				                            SELECT dtl.Parent_Id
				                            From SBP_Parent_Child_Owner_Details as dtl				
                            ) order by reg.Registration_ID desc";
           try
           {
               _dbconnection.ConnectDatabase();
               dt = _dbconnection.ExecuteQuery(query);
           }
           catch (Exception ex)
           {
               throw ex;
           }
           finally
           {
               _dbconnection.CloseDatabase();
           }
           return dt;
       }

       public DataTable GetRegistrationIdAndParentCode_ForParenChildDetails()
       {
           DataTable dt = new DataTable();
           string query = @"Select reg.Parent_Code,reg.Registration_ID
                            From SBP_Parent_Child_Registration as reg
                            WHERE reg.Registration_ID NOT IN(
                                                        SELECT ISNULL(dtl.Registration_ID,0)
                                                        From dbo.SBP_Parent_Child_Details as dtl
                                                        GROUP BY dtl.Registration_ID				
                            ) order by reg.Registration_ID desc";
           try
           {
               _dbconnection.ConnectDatabase();
               dt = _dbconnection.ExecuteQuery(query);
           }
           catch (Exception ex)
           {
               throw ex;
           }
           finally
           {
               _dbconnection.CloseDatabase();
           }
           return dt;
       }


    }
}