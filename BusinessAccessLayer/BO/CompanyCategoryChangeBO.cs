﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessAccessLayer.BO
{
    public class CompanyCategoryChangeBO
    {
        private long _slNo;
        private string _compShortCode;
        private int _oldCategoryID;
        private int _newCategoryID;
        private DateTime _effectiveDate;
        public CompanyCategoryChangeBO()
        {
            _slNo = 0;
            _compShortCode = "";
            _effectiveDate = DateTime.Now;
        }

        public long SlNo
        {
            get { return _slNo; }
            set { _slNo = value; }
        }

        public string CompShortCode
        {
            get { return _compShortCode; }
            set { _compShortCode = value; }
        }

        public int OldCategoryId
        {
            get { return _oldCategoryID; }
            set { _oldCategoryID = value; }
        }

        public int NewCategoryId
        {
            get { return _newCategoryID; }
            set { _newCategoryID = value; }
        }

        public DateTime EffectiveDate
        {
            get { return _effectiveDate; }
            set { _effectiveDate = value; }
        }
    }
}