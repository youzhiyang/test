using System;

namespace Visitor.Model.Entity.Custom
{
    /// <summary>
    /// 返回结果
    /// </summary>
    public class ResponseData
    {
        /// <summary>
        /// 返回码
        /// </summary>
        protected long _errcode;

        /// <summary>
        /// 对返回码的文本描述内容
        /// </summary>
        protected string _errmsg;

        /// <summary>
        /// 返回数据的JSON字符串
        /// </summary>
        protected string _dataJson;

        /// <summary>
        /// 其他扩展数据
        /// </summary>
        protected string _extend;

        /// <summary>
        /// 总数据数
        /// </summary>
        protected long _total;

        /// <summary>
        /// 返回结果
        /// </summary>
        public ResponseData()
        {
            Reset();
        }

        /// <summary>
        /// 返回码
        /// </summary>
        public long errcode
        {
            get { return _errcode; }
            set { _errcode = value; }
        }

        /// <summary>
        /// 对返回码的文本描述内容
        /// </summary>
        public string errmsg
        {
            get { return _errmsg; }
            set { _errmsg = value; }
        }

        /// <summary>
        /// 返回数据的JSON字符串
        /// </summary>
        public string DataJSON
        {
            get { return _dataJson; }
            set { _dataJson = value; }
        }

        /// <summary>
        /// 其他扩展数据
        /// </summary>
        public string Extend
        {
            get { return _extend; }
            set { _extend = value; }
        }

        /// <summary>
        /// 总数据数
        /// </summary>
        public long Total
        {
            get { return _total; }
            set { _total = value; }
        }

        /// <summary>
        /// 重置服务器返回数据
        /// </summary>
        public void Reset()
        {
            _errcode = 0;
            _errmsg = "";
            _dataJson = "[]";
            _extend = "";
            _total = 0;
        }

        /// <summary>
        /// 复制服务器返回数据
        /// </summary>
        public void AssignFrom(ResponseData AObj)
        {
            if (AObj == null)
            {
                Reset();
                return;
            }
            _errcode = AObj._errcode;
            _errmsg = AObj._errmsg;
            _dataJson = AObj._dataJson;
            _extend = AObj._extend;
            _total = AObj._total;
        }
    }
}
