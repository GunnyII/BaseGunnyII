using System;
using System.Collections.Generic;
using System.Text;

namespace SqlDataProvider.Data
{
    public class GiftInfo : DataObject
    {
        private DateTime _addDate;
        private int _count;
        private int _itemID;
        private Dictionary<string, object> _tempInfo = new Dictionary<string, object>();
        private ItemTemplateInfo _template;
        private int _templateId;
        private int _userID;

        internal GiftInfo(ItemTemplateInfo template)
        {
            _template = template;
            if (_template != null)
            {
                _templateId = _template.TemplateID;
            }
            if (_tempInfo == null)
            {
                _tempInfo = new Dictionary<string, object>();
            }
        }

        public bool CanStackedTo(GiftInfo to)
        {
            return ((_templateId == to.TemplateID) && (Template.MaxCount > 1));
        }

        public static GiftInfo CreateFromTemplate(ItemTemplateInfo template, int count)
        {
            if (template == null)
            {
                throw new ArgumentNullException("template");
            }
            return new GiftInfo(template) { TemplateID = template.TemplateID, IsDirty = false, AddDate = DateTime.Now, Count = count };
        }

        public static GiftInfo CreateWithoutInit(ItemTemplateInfo template)
        {
            return new GiftInfo(template);
        }

        // Properties
        public DateTime AddDate
        {
            get
            {
                return _addDate;
            }
            set
            {
                if (!(_addDate == value))
                {
                    _addDate = value;
                    base._isDirty = true;
                }
            }
        }

        public int Count
        {
            get
            {
                return _count;
            }
            set
            {
                if (_count != value)
                {
                    _count = value;
                    base._isDirty = true;
                }
            }
        }

        public int ItemID
        {
            get
            {
                return _itemID;
            }
            set
            {
                _itemID = value;
                base._isDirty = true;
            }
        }

        public Dictionary<string, object> TempInfo
        {
            get
            {
                return _tempInfo;
            }
        }

        public ItemTemplateInfo Template
        {
            get
            {
                return _template;
            }
        }

        public int TemplateID
        {
            get
            {
                return _templateId;
            }
            set
            {
                if (_templateId != value)
                {
                    _templateId = value;
                    base._isDirty = true;
                }
            }
        }

        public int UserID
        {
            get
            {
                return _userID;
            }
            set
            {
                if (_userID != value)
                {
                    _userID = value;
                    base._isDirty = true;
                }
            }
        }

     
    }
}
