using System;
using System.IO;
using System.Net;
using Visitor.Model.Entity.Custom;

namespace Visitor.Bll
{
    /// <summary>
    /// 通用业务
    /// </summary>
    public class CommonBll
    {
        /// <summary>
        /// 实例化对象
        /// </summary>
        public static readonly CommonBll Instance = new CommonBll();

        /// <summary>
        /// 获取天气数据
        /// </summary>
        /// <returns></returns>
        public ResponseData GetWeather()
        {
            try
            {
                ////var url = "http://www.weather.com.cn/data/sk/101300501.html";
                //var url = "http://forecast.weather.com.cn/napi/h5map/county/10130";
                //var request = WebRequest.Create(url);
                //var response = request.GetResponse();
                //var getStream = response.GetResponseStream();
                //var reader = new StreamReader(getStream);
                //var content = reader.ReadToEnd();
                //return new ResponseData()
                //{
                //    DataJSON = "{\"data\":" + content + ","
                //        + "\"weatherCode\":{\"10\":\"暴雨\",\"11\":\"大暴雨\",\"12\":\"特大暴雨\",\"13\":\"阵雪\",\"14\":\"小雪\",\"15\":\"中雪\",\"16\":\"大雪\",\"17\":\"暴雪\",\"18\":\"雾\","
                //        + "\"19\":\"冻雨\",\"20\":\"沙尘暴\",\"21\":\"小到中雨\",\"22\":\"中到大雨\",\"23\":\"大到暴雨\",\"24\":\"暴雨到大暴雨\",\"25\":\"大暴雨到特大暴雨\",\"26\":\"小到中雪\","
                //        + "\"27\":\"中到大雪\",\"28\":\"大到暴雪\",\"29\":\"浮尘\",\"30\":\"扬沙\",\"31\":\"强沙尘暴\",\"53\":\"霾\",\"99\":\"\",\"00\":\"晴\",\"01\":\"多云\",\"02\":\"阴\","
                //        + "\"03\":\"阵雨\",\"04\":\"雷阵雨\",\"05\":\"雷阵雨伴有冰雹\",\"06\":\"雨夹雪\",\"07\":\"小雨\",\"08\":\"中雨\",\"09\":\"大雨\"}}"
                //};
                var url = "http://d1.weather.com.cn/weather_index/101300501.html";
                var request = WebRequest.Create(url);
                var response = request.GetResponse();
                var getStream = response.GetResponseStream();
                var reader = new StreamReader(getStream);
                var content = reader.ReadToEnd();
                var startIndex = content.IndexOf("weatherinfo") + "weatherinfo".Length + 2;
                var endIndex = content.IndexOf("}") + 1 - startIndex;
                content = content.Substring(startIndex, endIndex);
                return new ResponseData()
                {
                    DataJSON = "{\"data\":" + content + ","
                        + "\"weatherCode\":{\"10\":\"暴雨\",\"11\":\"大暴雨\",\"12\":\"特大暴雨\",\"13\":\"阵雪\",\"14\":\"小雪\",\"15\":\"中雪\",\"16\":\"大雪\",\"17\":\"暴雪\",\"18\":\"雾\","
                        + "\"19\":\"冻雨\",\"20\":\"沙尘暴\",\"21\":\"小到中雨\",\"22\":\"中到大雨\",\"23\":\"大到暴雨\",\"24\":\"暴雨到大暴雨\",\"25\":\"大暴雨到特大暴雨\",\"26\":\"小到中雪\","
                        + "\"27\":\"中到大雪\",\"28\":\"大到暴雪\",\"29\":\"浮尘\",\"30\":\"扬沙\",\"31\":\"强沙尘暴\",\"53\":\"霾\",\"99\":\"\",\"00\":\"晴\",\"01\":\"多云\",\"02\":\"阴\","
                        + "\"03\":\"阵雨\",\"04\":\"雷阵雨\",\"05\":\"雷阵雨伴有冰雹\",\"06\":\"雨夹雪\",\"07\":\"小雨\",\"08\":\"中雨\",\"09\":\"大雨\"}}"
                };

            }
            catch (Exception ex)
            {
                return new ResponseData() { errcode = -1, errmsg = "读取数据失败。" + ex.Message };
            }
        }
    }
}
