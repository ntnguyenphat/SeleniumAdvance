using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAdvance.DataObjects
{
    public class DataProfiles
    {
        private string _dataProfileName;
        private string _itemType;
        private string _relatedData;

        public string DataProfileName
        {
            get { return _dataProfileName; }
            set { _dataProfileName = value; }
        }
        

        public string ItemType
        {
            get { return _itemType; }
            set { _itemType = value; }
        }
        

        public string RelatedData
        {
            get { return _relatedData; }
            set { _relatedData = value; }
        }

        public DataProfiles() { }

        public void DataProfileInfo(string dataProfileName, string itemType, string relatedData)
        {
            this.DataProfileName = dataProfileName;
            this.ItemType = itemType;
            this.RelatedData = relatedData;
        }
    }
}
