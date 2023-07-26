using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SQLUI;
using Basic;
using System.Text;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.ComponentModel;
using System.Reflection;
using System.Configuration;
using IBM.Data.DB2.Core;
using MyOffice;
using NPOI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using H_Pannel_lib;
using System.Drawing;
namespace HIS_WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class controlController : ControllerBase
    {
        public enum enum_輸出資料表
        {
            GUID,
            輸出位置,
            輸出狀態,
            狀態更新,
        }
        public enum enum_輸入資料表
        {
            GUID,
            輸入位置,
            輸入狀態,
        }
        public class inputClass
        {
            private string num = "";
            private string state = "";

            public string Num { get => num; set => num = value; }
            public string State { get => state; set => state = value; }
        }
        public class outputClass
        {
            private string num = "";
            private string state = "";

            public string Num { get => num; set => num = value; }
            public string State { get => state; set => state = value; }
        }
        [Route("input")]
        [HttpGet]
        public string Get_input()
        {
            List<string> input_position = new List<string>();

            input_position.Add("X0");
            input_position.Add("X1");
            input_position.Add("X2");
            input_position.Add("X3");
            input_position.Add("X4");
            input_position.Add("X5");
            input_position.Add("X6");
            input_position.Add("X7");
            input_position.Add("X10");
            input_position.Add("X11");
            input_position.Add("X12");
            input_position.Add("X13");
            input_position.Add("X14");
            input_position.Add("X15");
            input_position.Add("X16");
            input_position.Add("X17");
            input_position.Add("X20");
            input_position.Add("X21");
            input_position.Add("X22");
            input_position.Add("X23");
            input_position.Add("X24");
            input_position.Add("X25");
            input_position.Add("X26");
            input_position.Add("X27");
            input_position.Add("X30");
            input_position.Add("X31");
            input_position.Add("X32");
            List<inputClass> inputClasses = new List<inputClass>();
            List<inputClass> inputClasses_buf = new List<inputClass>();
            SQLControl sQLControl = new SQLControl("127.0.0.1", "portal", "user", "66437068", MySqlSslMode.None);
            sQLControl.TableName = "input_list";
            List<object[]> list_input = sQLControl.GetAllRows(null);
            List<object[]> list_input_buf = new List<object[]>();
            for (int i = 0; i < input_position.Count; i++)
            {
                inputClass inputClass = new inputClass();
                inputClass.Num = $"{ i + 1 }";
                inputClass.State = false.ToString();
                list_input_buf = list_input.GetRows((int)enum_輸入資料表.輸入位置, input_position[i]);
                if (list_input_buf.Count > 0)
                {
                    inputClass.State = (list_input_buf[0][(int)enum_輸入資料表.輸入狀態].ObjectToString() == true.ToString()) ? true.ToString() : false.ToString();
                }
                inputClasses.Add(inputClass);
            }
            return inputClasses.JsonSerializationt(true);
        }
        [Route("light_on/{value}")]
        [HttpGet]
        public string Set_light_on(string value)
        {
            if (value.StringIsEmpty()) return $"引入值空白!";
            int num = value.StringToInt32();
            num = num - 1;
            if (num < 0 || num >= 32) return $"引入值錯誤! value {value}";

            List<string> output_position = new List<string>();

            output_position.Add("Y40");
            output_position.Add("Y41");
            output_position.Add("Y42");
            output_position.Add("Y43");
            output_position.Add("Y44");
            output_position.Add("Y45");
            output_position.Add("Y46");
            output_position.Add("Y47");
            output_position.Add("Y50");
            output_position.Add("Y51");
            output_position.Add("Y52");
            output_position.Add("Y53");
            output_position.Add("Y54");
            output_position.Add("Y55");
            output_position.Add("Y56");
            output_position.Add("Y57");
            output_position.Add("Y60");
            output_position.Add("Y61");
            output_position.Add("Y62");
            output_position.Add("Y63");
            output_position.Add("Y64");
            output_position.Add("Y65");
            output_position.Add("Y66");
            output_position.Add("Y67");
            output_position.Add("Y70");
            output_position.Add("Y71");
            output_position.Add("Y72");

            List<outputClass> outputClasses = new List<outputClass>();
            List<outputClass> outputClasses_buf = new List<outputClass>();
            SQLControl sQLControl = new SQLControl("127.0.0.1", "portal", "user", "66437068", MySqlSslMode.None);
            sQLControl.TableName = "output_list";
            List<object[]> list_output = sQLControl.GetAllRows(null);
            List<object[]> list_output_buf = new List<object[]>();
            string 輸出位置 = output_position[num];
            list_output_buf = list_output.GetRows((int)enum_輸出資料表.輸出位置, 輸出位置);
            if (list_output_buf.Count == 0)
            {
                object[] temp = new object[new enum_輸出資料表().GetLength()];
                temp[(int)enum_輸出資料表.GUID] = Guid.NewGuid().ToString();
                temp[(int)enum_輸出資料表.輸出位置] = 輸出位置;
                temp[(int)enum_輸出資料表.輸出狀態] = true.ToString();
                temp[(int)enum_輸出資料表.狀態更新] = true.ToString();
                sQLControl.AddRow(null, temp);
            }
            else
            {
                object[] temp = list_output_buf[0];
                list_output_buf[0][(int)enum_輸出資料表.輸出位置] = 輸出位置;
                list_output_buf[0][(int)enum_輸出資料表.輸出狀態] = true.ToString();
                list_output_buf[0][(int)enum_輸出資料表.狀態更新] = true.ToString();
                sQLControl.UpdateByDefulteExtra(null, list_output_buf);
            }
            return "OK";
        }

        [Route("light_off/{value}")]
        [HttpGet]
        public string Set_light_off(string value)
        {
            if (value.StringIsEmpty()) return $"引入值空白!";
            int num = value.StringToInt32();
            num = num - 1;
            if (num < 0 || num >= 32) return $"引入值錯誤! value {value}";

            List<string> output_position = new List<string>();

            output_position.Add("Y40");
            output_position.Add("Y41");
            output_position.Add("Y42");
            output_position.Add("Y43");
            output_position.Add("Y44");
            output_position.Add("Y45");
            output_position.Add("Y46");
            output_position.Add("Y47");
            output_position.Add("Y50");
            output_position.Add("Y51");
            output_position.Add("Y52");
            output_position.Add("Y53");
            output_position.Add("Y54");
            output_position.Add("Y55");
            output_position.Add("Y56");
            output_position.Add("Y57");
            output_position.Add("Y60");
            output_position.Add("Y61");
            output_position.Add("Y62");
            output_position.Add("Y63");
            output_position.Add("Y64");
            output_position.Add("Y65");
            output_position.Add("Y66");
            output_position.Add("Y67");
            output_position.Add("Y70");
            output_position.Add("Y71");
            output_position.Add("Y72");

            List<outputClass> outputClasses = new List<outputClass>();
            List<outputClass> outputClasses_buf = new List<outputClass>();
            SQLControl sQLControl = new SQLControl("127.0.0.1", "portal", "user", "66437068", MySqlSslMode.None);
            sQLControl.TableName = "output_list";
            List<object[]> list_output = sQLControl.GetAllRows(null);
            List<object[]> list_output_buf = new List<object[]>();
            string 輸出位置 = output_position[num];
            list_output_buf = list_output.GetRows((int)enum_輸出資料表.輸出位置, 輸出位置);
            if (list_output_buf.Count == 0)
            {
                object[] temp = new object[new enum_輸出資料表().GetLength()];
                temp[(int)enum_輸出資料表.GUID] = Guid.NewGuid().ToString();
                temp[(int)enum_輸出資料表.輸出位置] = 輸出位置;
                temp[(int)enum_輸出資料表.輸出狀態] = false.ToString();
                temp[(int)enum_輸出資料表.狀態更新] = true.ToString();
                sQLControl.AddRow(null, temp);
            }
            else
            {
                object[] temp = list_output_buf[0];
                list_output_buf[0][(int)enum_輸出資料表.輸出位置] = 輸出位置;
                list_output_buf[0][(int)enum_輸出資料表.輸出狀態] = false.ToString();
                list_output_buf[0][(int)enum_輸出資料表.狀態更新] = true.ToString();
                sQLControl.UpdateByDefulteExtra(null, list_output_buf);
            }
            return "OK";
        }

        [Route("dooropen/{value}")]
        [HttpGet]
        public string Set_door(string value)
        {
            if (value.StringIsEmpty()) return $"引入值空白!";
            int num = value.StringToInt32();
            num = num - 1;
            if (num < 0 || num >= 32) return $"引入值錯誤! value {value}";

            List<string> output_position = new List<string>();

            output_position.Add("Y0");
            output_position.Add("Y1");
            output_position.Add("Y2");
            output_position.Add("Y3");
            output_position.Add("Y4");
            output_position.Add("Y5");
            output_position.Add("Y6");
            output_position.Add("Y7");
            output_position.Add("Y10");
            output_position.Add("Y11");
            output_position.Add("Y12");
            output_position.Add("Y13");
            output_position.Add("Y14");
            output_position.Add("Y15");
            output_position.Add("Y16");
            output_position.Add("Y17");
            output_position.Add("Y20");
            output_position.Add("Y21");
            output_position.Add("Y22");
            output_position.Add("Y23");
            output_position.Add("Y24");
            output_position.Add("Y25");
            output_position.Add("Y26");
            output_position.Add("Y27");
            output_position.Add("Y30");
            output_position.Add("Y31");
            output_position.Add("Y32");
          
            List<outputClass> outputClasses = new List<outputClass>();
            List<outputClass> outputClasses_buf = new List<outputClass>();
            SQLControl sQLControl = new SQLControl("127.0.0.1", "portal", "user", "66437068", MySqlSslMode.None);
            sQLControl.TableName = "output_list";
            List<object[]> list_output = sQLControl.GetAllRows(null);
            List<object[]> list_output_buf = new List<object[]>();
            string 輸出位置 = output_position[num];
            list_output_buf = list_output.GetRows((int)enum_輸出資料表.輸出位置, 輸出位置);
            if (list_output_buf.Count == 0)
            {
                object[] temp = new object[new enum_輸出資料表().GetLength()];
                temp[(int)enum_輸出資料表.GUID] = Guid.NewGuid().ToString();
                temp[(int)enum_輸出資料表.輸出位置] = 輸出位置;
                temp[(int)enum_輸出資料表.輸出狀態] = true.ToString();
                temp[(int)enum_輸出資料表.狀態更新] = true.ToString();
                sQLControl.AddRow(null, temp);
            }
            else
            {
                object[] temp = list_output_buf[0];
                list_output_buf[0][(int)enum_輸出資料表.輸出位置] = 輸出位置;
                list_output_buf[0][(int)enum_輸出資料表.輸出狀態] = true.ToString();
                list_output_buf[0][(int)enum_輸出資料表.狀態更新] = true.ToString();
                sQLControl.UpdateByDefulteExtra(null, list_output_buf);
            }
            return "OK";
        }
    }



}
