using System;

namespace Visitor.Model.Entity.Table
{
    /// <summary>
    /// 基础数据_区域
    /// </summary>
    public partial class Data_Area
    {
        /// <summary>
        /// 区域ID
        /// </summary>
        protected int _id;

        /// <summary>
        /// 区域类别
        /// </summary>
        protected string _type;

        /// <summary>
        /// 区域名称
        /// </summary>
        protected string _name;

        /// <summary>
        /// 排序
        /// </summary>
        protected int _index;

        /// <summary>
        /// 基础数据_区域
        /// </summary>
        public Data_Area()
        {
        }

        /// <summary>
        /// 区域ID
        /// </summary>
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// 区域类别
        /// </summary>
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        /// <summary>
        /// 区域名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// 排序
        /// </summary>
        public int Index
        {
            get { return _index; }
            set { _index = value; }
        }

        /// <summary>
        /// 重置基础数据_区域
        /// </summary>
        public void Reset()
        {
            _id = 0;
            _type = null;
            _name = null;
            _index = 0;
        }

        /// <summary>
        /// 复制基础数据_区域
        /// </summary>
        public void AssignFrom(Data_Area AObj)
        {
            if (AObj == null)
            {
                Reset();
                return;
            }
            _id = AObj._id;
            _type = AObj._type;
            _name = AObj._name;
            _index = AObj._index;
        }

        /// <summary>
        /// 偏移时间
        /// </summary>
        public void OffsetTime(int offsetMinutes)
        {
            foreach (var property in this.GetType().GetProperties())
            {
                if (property.GetType().Equals(typeof(DateTime)))
                {
                    property.SetValue(this, ((DateTime)property.GetValue(this, null)).AddMinutes(offsetMinutes), null);
                }
                else if (property.GetType().Equals(typeof(DateTime?)))
                {
                    var value = (DateTime?)property.GetValue(this, null);
                    if (value.HasValue)
                    {
                        property.SetValue(this, value.Value.AddMinutes(offsetMinutes), null);
                    }
                }
            }
        }

        /// <summary>
        /// 重写Equals
        /// </summary>
        public override bool Equals(object obj)
        {
            return obj is Data_Area area &&
                   _id == area._id &&
                   _type == area._type &&
                   _name == area._name &&
                   _index == area._index &&
                   ID == area.ID &&
                   Type == area.Type &&
                   Name == area.Name &&
                   Index == area.Index;
        }
    }
}
