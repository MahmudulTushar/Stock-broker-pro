UPDATE dbo.SBP_Customers
SET 
BO_Category_ID=(SELECT BO_Category_ID FROM dbo.SBP_BO_Category WHERE BO_Category=SBP_DP29.BO_Category),
BO_Type_ID=(SELECT BO_Type_ID FROM dbo.SBP_BO_Type WHERE BO_Type=SBP_DP29.BO_Type),
BO_Status_ID=(SELECT BO_Status_ID FROM dbo.SBP_BO_Status WHERE BO_Status=dbo.SBP_DP29.BO_Status),
BO_Open_Date=CONVERT(DATETIME,Setup_Date,103),
BO_Close_Date=CONVERT(DATETIME,Closure_Date,103)
FROM dbo.SBP_DP29 
WHERE CAST(SUBSTRING(SBP_DP29.BO_ID,9,8) AS VARCHAR(13))=SBP_Customers.BO_ID


UPDATE dbo.SBP_Cust_Personal_Info
SET Cust_Name=dbo.SBP_DP29.Full_Name,
Father_Name=dbo.SBP_DP29.[BO Father_Husband],
Mother_Name=dbo.SBP_DP29.BO_Mother
FROM dbo.SBP_DP29 
WHERE
dbo.SBP_DP29.Internal_Reference_Number=dbo.SBP_Cust_Personal_Info.Cust_Code

UPDATE dbo.SBP_Cust_Contact_Info 
SET Address1=dbo.SBP_DP29.Address,
City_Name=dbo.SBP_DP29.City,
Division_Name=dbo.SBP_DP29.State,
Country_Name=dbo.SBP_DP29.Country,
Phone=dbo.SBP_DP29.Telphone_Number,
Fax=dbo.SBP_DP29.Fax,
Email=dbo.SBP_DP29.Email
FROM dbo.SBP_DP29 
WHERE 
dbo.SBP_DP29.Internal_Reference_Number=dbo.SBP_Cust_Contact_Info.Cust_Code

UPDATE dbo.SBP_Cust_Bank_Info
SET Bank_Name=dbo.SBP_DP29.Bank_Name,
Branch_Name=dbo.SBP_DP29.Branch_Name,
Account_No=dbo.SBP_DP29.Account_Number
FROM dbo.SBP_DP29
WHERE 
dbo.SBP_DP29.Internal_Reference_Number=dbo.SBP_Cust_Bank_Info.Cust_Code

UPDATE dbo.SBP_Cust_Additional_Info
SET Recidency=dbo.SBP_DP29.Residency
FROM
dbo.SBP_DP29
WHERE 
dbo.SBP_DP29.Internal_Reference_Number=dbo.SBP_Cust_Additional_Info.Cust_Code