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
        private string _createdBy;
        private string _creationDate;
        private string _action;

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

        public string CreatedBy
        {
            get { return _createdBy; }
            set { _createdBy = value; }
        }

        public string CreationDate
        {
            get { return _creationDate; }
            set { _creationDate = value; }
        }

        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }
        public DataProfiles() { }

        public void DataProfileInfo(string dataProfileName = null, string itemType = null, string relatedData = null, string createdBy = null, string creationDate = null, string action = null)
        {
            this.DataProfileName = dataProfileName;
            this.ItemType = itemType;
            this.RelatedData = relatedData;
            this.CreatedBy = createdBy;
            this.CreationDate = CreationDate;
            this.Action = action;
        }
    }
}
