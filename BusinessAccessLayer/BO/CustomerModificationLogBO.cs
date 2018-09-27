﻿using System;

namespace BusinessAccessLayer.BO
{
    public class CustomerModificationLogBO
    {
        private long _slNo;
        private string _custCode;
        private string _accStatus;
        private string _custGroup;
        private string _custStatus;
        private DateTime _customerOpenDate;
        private DateTime? _customerCloseDate;
        private int _custParentCode;
        private string _specialInstruction;
        private string _boID;
        private string _boType;
        private string _boCategory;
        private DateTime _boOpenDate;
        private DateTime _boCloseDate;
        private string _boStatus;
        private int _isDirSE;
        private string _nameAddressSE;
        private string _accountHolder;
        private string _fatherName;
        private string _motherName;
        private DateTime _dOB;
        private string _gender;
        private string _occupation;
        private string _nationalId;
        private string _countryName;
        private string _divisionName;
        private string _cityName;
        private string _postCode;
        private string _address1;
        private string _address2;
        private string _address3;
        private string _telephone;
        private string _mobile;
        private string _fax;
        private string _email;
        private string _residency;
        private string _nationality;
        private string _statementCycle;
        private string _internalRefNo;
        private string _companyRegNo;
        private DateTime _companyRegDate;
        private int _isAccLinkRequest;
        private string _accLinkBO;
        private int _isStandingIns;
        private string _bankName;
        private string _branchName;

        private string _sWIFT_Code;
        private string _iBAN;
        private string _routing_No;
        private string _district_Name;

        private string _accountNo;
        private int _isEDC;
        private int _isTaxExemption;
        private string _TIN;
        private string _passportNo;
        private string _issuePlace;
        private DateTime _issueDate;
        private DateTime _expireDate;
        private string _jointName;
        private string _jointFatherName;
        private string _jointMotherName;
        private DateTime _jointDOB;
        private string _jointSex;
        private string _jointNationality;
        private string _jointAddress;
        private string _jointNationalId;
        private string _joitnPhone;
        private string _jointMobile;
        private string _jointEmail;
        private string _operatedBy;
        private string _authorName;
        private string _authorAddress;
        private string _authorMobile;
        private string _introName;
        private string _introAddress;
        private string _introBOID;
        private string _introRemarks;
        private DateTime _modificationTime;
        private DateTime _modificationDate;
        private string _modifiedBy;
        private string _AccountType;


        public CustomerModificationLogBO()
        {
            _custCode = "";
            _accStatus = "";
            _custGroup = "";
            _custStatus = "";
            //_customerOpenDate;
            //_customerCloseDate;
            _custParentCode = 0;
            _specialInstruction = "";
            _boID = "";
            _boType = "";
            _boCategory = "";
            //_boOpenDate;
            //_boCloseDate;
            _boStatus = "";
            _isDirSE = 0;
            _nameAddressSE = "";
            _accountHolder = "";
            _fatherName = "";
            _motherName = "";
            //_dOB;
            _gender = "";
            _occupation = "";
            _nationalId = "";
            _countryName = "";
            _divisionName = "";
            _cityName = "";
            _postCode = "";
            _address1 = "";
            _address2 = "";
            _address3 = "";
            _telephone = "";
            _mobile = "";
            _fax = "";
            _email = "";
            _residency = "";
            _nationality = "";
            _statementCycle = "";
            _internalRefNo = "";
            _companyRegNo = "";
            _isAccLinkRequest = 0;
            _isStandingIns = 0;
            _accLinkBO = "";
            //_companyRegDate;
            _bankName = "";
            _branchName = "";
            _accountNo = "";
            _isEDC = 0;
            _isTaxExemption = 0;
            _TIN = "";
            _passportNo = "";
            _issuePlace = "";
            //_issueDate;
            // _expireDate;
            _jointName = "";
            _jointFatherName = "";
            _jointMotherName = "";
            //_jointDOB;
            _jointSex = "";
            _jointNationality = "";
            _jointAddress = "";
            _jointNationalId = "";
            _joitnPhone = "";
            _jointMobile = "";
            _jointEmail = "";
            _operatedBy = "";
            _authorName = "";
            _authorAddress = "";
            _authorMobile = "";
            _introName = "";
            _introAddress = "";
            _introBOID = "";
            _introRemarks = "";
            _modificationTime = DateTime.Now;
            _modificationDate = DateTime.Now;
            _modifiedBy = "";
            _AccountType = string.Empty;
        }

        public long SlNo
        {
            get { return _slNo; }
            set { _slNo = value; }
        }

        public string CustCode
        {
            get { return _custCode; }
            set { _custCode = value; }
        }

        public string AccStatus
        {
            get { return _accStatus; }
            set { _accStatus = value; }
        }

        public string CustGroup
        {
            get { return _custGroup; }
            set { _custGroup = value; }
        }

        public string CustStatus
        {
            get { return _custStatus; }
            set { _custStatus = value; }
        }

        public DateTime CustomerOpenDate
        {
            get { return _customerOpenDate; }
            set { _customerOpenDate = value; }
        }

        public DateTime? CustomerCloseDate
        {
            get { return _customerCloseDate; }
            set { _customerCloseDate = value; }
        }

        public int CustParentCode
        {
            get { return _custParentCode; }
            set { _custParentCode = value; }
        }

        public string SpecialInstruction
        {
            get { return _specialInstruction; }
            set { _specialInstruction = value; }
        }

        public string BoId
        {
            get { return _boID; }
            set { _boID = value; }
        }

        public string BoType
        {
            get { return _boType; }
            set { _boType = value; }
        }

        public string NboCategory
        {
            get { return BoCategory; }
            set { BoCategory = value; }
        }

        public DateTime BoOpenDate
        {
            get { return _boOpenDate; }
            set { _boOpenDate = value; }
        }

        public DateTime BoCloseDate
        {
            get { return _boCloseDate; }
            set { _boCloseDate = value; }
        }

        public string BoStatus
        {
            get { return _boStatus; }
            set { _boStatus = value; }
        }

        public int IsDirSe
        {
            get { return _isDirSE; }
            set { _isDirSE = value; }
        }

        public string NameAddressSe
        {
            get { return _nameAddressSE; }
            set { _nameAddressSE = value; }
        }

        public string AccountHolder
        {
            get { return _accountHolder; }
            set { _accountHolder = value; }
        }

        public string FatherName
        {
            get { return _fatherName; }
            set { _fatherName = value; }
        }

        public string MotherName
        {
            get { return _motherName; }
            set { _motherName = value; }
        }

        public DateTime DOb
        {
            get { return _dOB; }
            set { _dOB = value; }
        }

        public string Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        public string Occupation
        {
            get { return _occupation; }
            set { _occupation = value; }
        }

        public string NationalId
        {
            get { return _nationalId; }
            set { _nationalId = value; }
        }

        public string CountryName
        {
            get { return _countryName; }
            set { _countryName = value; }
        }

        public string DivisionName
        {
            get { return _divisionName; }
            set { _divisionName = value; }
        }

        public string CityName
        {
            get { return _cityName; }
            set { _cityName = value; }
        }

        public string PostCode
        {
            get { return _postCode; }
            set { _postCode = value; }
        }

        public string Address1
        {
            get { return _address1; }
            set { _address1 = value; }
        }

        public string Address2
        {
            get { return _address2; }
            set { _address2 = value; }
        }

        public string Address3
        {
            get { return _address3; }
            set { _address3 = value; }
        }

        public string Telephone
        {
            get { return _telephone; }
            set { _telephone = value; }
        }

        public string Mobile
        {
            get { return _mobile; }
            set { _mobile = value; }
        }

        public string Fax
        {
            get { return _fax; }
            set { _fax = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string Residency
        {
            get { return _residency; }
            set { _residency = value; }
        }

        public string Nationality
        {
            get { return _nationality; }
            set { _nationality = value; }
        }

        public string StatementCycle
        {
            get { return _statementCycle; }
            set { _statementCycle = value; }
        }

        public string InternalRefNo
        {
            get { return _internalRefNo; }
            set { _internalRefNo = value; }
        }

        public string CompanyRegNo
        {
            get { return _companyRegNo; }
            set { _companyRegNo = value; }
        }

        public DateTime CompanyRegDate
        {
            get { return _companyRegDate; }
            set { _companyRegDate = value; }
        }

        public string BankName
        {
            get { return _bankName; }
            set { _bankName = value; }
        }

        public string BranchName
        {
            get { return _branchName; }
            set { _branchName = value; }
        }

        public string AccountNo
        {
            get { return _accountNo; }
            set { _accountNo = value; }
        }

        public int IsEdc
        {
            get { return _isEDC; }
            set { _isEDC = value; }
        }

        public int IsTaxExemption
        {
            get { return _isTaxExemption; }
            set { _isTaxExemption = value; }
        }

        public string Tin
        {
            get { return _TIN; }
            set { _TIN = value; }
        }

        public string PassportNo
        {
            get { return _passportNo; }
            set { _passportNo = value; }
        }

        public string IssuePlace
        {
            get { return _issuePlace; }
            set { _issuePlace = value; }
        }

        public DateTime IssueDate
        {
            get { return _issueDate; }
            set { _issueDate = value; }
        }

        public DateTime ExpireDate
        {
            get { return _expireDate; }
            set { _expireDate = value; }
        }

        public string JointName
        {
            get { return _jointName; }
            set { _jointName = value; }
        }

        public string JointFatherName
        {
            get { return _jointFatherName; }
            set { _jointFatherName = value; }
        }

        public string JointMotherName
        {
            get { return _jointMotherName; }
            set { _jointMotherName = value; }
        }

        public DateTime JointDob
        {
            get { return _jointDOB; }
            set { _jointDOB = value; }
        }

        public string JointSex
        {
            get { return _jointSex; }
            set { _jointSex = value; }
        }

        public string JointNationality
        {
            get { return _jointNationality; }
            set { _jointNationality = value; }
        }

        public string JointAddress
        {
            get { return _jointAddress; }
            set { _jointAddress = value; }
        }

        public string JointNationalId
        {
            get { return _jointNationalId; }
            set { _jointNationalId = value; }
        }

        public string JoitnPhone
        {
            get { return _joitnPhone; }
            set { _joitnPhone = value; }
        }

        public string JointMobile
        {
            get { return _jointMobile; }
            set { _jointMobile = value; }
        }

        public string JointEmail
        {
            get { return _jointEmail; }
            set { _jointEmail = value; }
        }

        public string AuthorName
        {
            get { return _authorName; }
            set { _authorName = value; }
        }

        public string AuthorAddress
        {
            get { return _authorAddress; }
            set { _authorAddress = value; }
        }

        public string AuthorMobile
        {
            get { return _authorMobile; }
            set { _authorMobile = value; }
        }

        public string IntroName
        {
            get { return _introName; }
            set { _introName = value; }
        }

        public string IntroAddress
        {
            get { return _introAddress; }
            set { _introAddress = value; }
        }

        public string IntroRemarks
        {
            get { return _introRemarks; }
            set { _introRemarks = value; }
        }

        public DateTime ModificationTime
        {
            get { return _modificationTime; }
            set { _modificationTime = value; }
        }

        public DateTime ModificationDate
        {
            get { return _modificationDate; }
            set { _modificationDate = value; }
        }

        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }

        public string BoCategory
        {
            get { return _boCategory; }
            set { _boCategory = value; }
        }

        public string IntroBOID
        {
            get { return _introBOID; }
            set { _introBOID = value; }
        }

        public string OperatedBy
        {
            get { return _operatedBy; }
            set { _operatedBy = value; }
        }

        public int IsAccLinkRequest
        {
            get { return _isAccLinkRequest; }
            set { _isAccLinkRequest = value; }
        }

        public string AccLinkBo
        {
            get { return _accLinkBO; }
            set { _accLinkBO = value; }
        }

        public int IsStandingIns
        {
            get { return _isStandingIns; }
            set { _isStandingIns = value; }
        }
        public string AccountType
        {
            get
            {
                return _AccountType;
            }
            set
            {
                _AccountType = value;
            }
        }

        public string District_Name
        {
            get { return _district_Name; }
            set { _district_Name = value; }
        }

        public string Routing_No
        {
            get { return _routing_No; }
            set { _routing_No = value; }
        }

        public string IBAN
        {
            get { return _iBAN; }
            set { _iBAN = value; }
        }

        public string SWIFT_Code
        {
            get { return _sWIFT_Code; }
            set { _sWIFT_Code = value; }
        }
    }
}