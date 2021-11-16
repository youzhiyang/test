using System;

namespace Visitor.Model.Entity.Table
{
    /// <summary>
    /// 基础数据_数据字典
    /// </summary>
    public partial class Data_Dictionary
    {
        /// <summary>
        /// 字典ID
        /// </summary>
        protected Guid _dictID;

        /// <summary>
        /// 字典类型
        /// </summary>
        protected string _dataType;

        /// <summary>
        /// 字典代码
        /// </summary>
        protected string _code;

        /// <summary>
        /// 字典名
        /// </summary>
        protected string _name;

        /// <summary>
        /// 字典值
        /// </summary>
        protected string _value;

        /// <summary>
        /// 扩展值1
        /// </summary>
        protected string _extValue1;

        /// <summary>
        /// 扩展值2
        /// </summary>
        protected string _extValue2;

        /// <summary>
        /// 扩展值3
        /// </summary>
        protected string _extValue3;

        /// <summary>
        /// 字典备注
        /// </summary>
        protected string _remark;

        /// <summary>
        /// 是否可以编辑
        /// </summary>
        protected bool _editable;

        /// <summary>
        /// 排序
        /// </summary>
        protected int? _order;

        /// <summary>
        /// 基础数据_数据字典
        /// </summary>
        public Data_Dictionary()
        {
        }

        /// <summary>
        /// 字典ID
        /// </summary>
        public Guid DictID
        {
            get { return _dictID; }
            set { _dictID = value; }
        }

        /// <summary>
        /// 字典类型
        /// </summary>
        public string DataType
        {
            get { return _dataType; }
            set { _dataType = value; }
        }

        /// <summary>
        /// 字典代码
        /// </summary>
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        /// <summary>
        /// 字典名
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// 字典值
        /// </summary>
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        /// <summary>
        /// 扩展值1
        /// </summary>
        public string ExtValue1
        {
            get { return _extValue1; }
            set { _extValue1 = value; }
        }

        /// <summary>
        /// 扩展值2
        /// </summary>
        public string ExtValue2
        {
            get { return _extValue2; }
            set { _extValue2 = value; }
        }

        /// <summary>
        /// 扩展值3
        /// </summary>
        public string ExtValue3
        {
            get { return _extValue3; }
            set { _extValue3 = value; }
        }

        /// <summary>
        /// 字典备注
        /// </summary>
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

        /// <summary>
        /// 是否可以编辑
        /// </summary>
        public bool Editable
        {
            get { return _editable; }
            set { _editable = value; }
        }

        /// <summary>
        /// 排序
        /// </summary>
        public int? Order
        {
            get { return _order; }
            set { _order = value; }
        }

        /// <summary>
        /// 重置基础数据_数据字典
        /// </summary>
        public void Reset()
        {
            _dictID = Guid.NewGuid();
            _dataType = null;
            _code = null;
            _name = null;
            _value = null;
            _extValue1 = null;
            _extValue2 = null;
            _extValue3 = null;
            _remark = null;
            _editable = false;
            _order = null;
        }

        /// <summary>
        /// 复制基础数据_数据字典
        /// </summary>
        public void AssignFrom(Data_Dictionary AObj)
        {
            if (AObj == null)
            {
                Reset();
                return;
            }
            _dictID = AObj._dictID;
            _dataType = AObj._dataType;
            _code = AObj._code;
            _name = AObj._name;
            _value = AObj._value;
            _extValue1 = AObj._extValue1;
            _extValue2 = AObj._extValue2;
            _extValue3 = AObj._extValue3;
            _remark = AObj._remark;
            _editable = AObj._editable;
            _order = AObj._order;
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
    }
}
