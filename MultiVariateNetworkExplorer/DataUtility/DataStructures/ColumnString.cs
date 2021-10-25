﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataUtility
{
    public class ColumnString : IColumn
    {
        public IList Data { get; } = new List<string>();

        public int DataCount
        {
            get
            {
                return this.Data.Count;
            }
        }

        public object this[int index]
        {
            get
            {
                return this.Data[index];
            }
        }
        public void AddData(object value, bool updateExtremes = false)
        {
            string stringValue = (string)value;
            this.Data.Add(stringValue);
        }

        public IEnumerator GetEnumerator()
        {
            return Data.GetEnumerator();
        }
    }
}