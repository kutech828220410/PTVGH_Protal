using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyUI;
using Basic;
using SQLUI;
namespace 屏東榮總傳送櫃
{
    public partial class Form1 : Form
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
        MyThread myThread;
        List<string> input_position = new List<string>();
        List<string> output_position = new List<string>();

        public Form1()
        {
            InitializeComponent();            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            MyMessageBox.form = this.FindForm();
            MyMessageBox.音效 = false;
            plC_UI_Init1.音效 = false;
            hcG_485_IO1.PortName = "COM3";
            plC_UI_Init1.Run(this.FindForm(), PLC);
            plC_UI_Init1.UI_Finished_Event += PlC_UI_Init1_UI_Finished_Event;

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
        }
        private void sub_program()
        {
            List<object[]> list_輸出資料表 = this.sqL_DataGridView_輸出資料表.SQL_GetAllRows(false);
            List<object[]> list_輸出資料表_replace = new List<object[]>();
            for (int i = 0; i < list_輸出資料表.Count; i++)
            {
                string 輸出位置 = list_輸出資料表[i][(int)enum_輸出資料表.輸出位置].ObjectToString();
                string 輸出狀態 = list_輸出資料表[i][(int)enum_輸出資料表.輸出狀態].ObjectToString();
                string 狀態更新 = list_輸出資料表[i][(int)enum_輸出資料表.狀態更新].ObjectToString();
                if (狀態更新.StringIsEmpty() == false)
                {
                    if (狀態更新 == true.ToString())
                    {
                        PLC.lowerMachine.properties.Device.Set_Device(輸出位置, (輸出狀態 == true.ToString()));
                        list_輸出資料表[i][(int)enum_輸出資料表.狀態更新] = false;
                       list_輸出資料表_replace.Add(list_輸出資料表[i]);
                    }
                }
            }
            if (list_輸出資料表_replace.Count > 0) sqL_DataGridView_輸出資料表.SQL_ReplaceExtra(list_輸出資料表_replace, false);



            List<object[]> list_輸入資料表 = this.sqL_DataGridView_輸入資料表.SQL_GetAllRows(false);
            List<object[]> list_輸入資料表_buf = new List<object[]>();
            List<object[]> list_輸入資料表_add = new List<object[]>();
            List<object[]> list_輸入資料表_replace = new List<object[]>();
            for (int i = 0; i < input_position.Count; i++)
            {
                object state = new object();
                PLC.lowerMachine.properties.Device.Get_Device(input_position[i] , out state);
                list_輸入資料表_buf = list_輸入資料表.GetRows((int)enum_輸入資料表.輸入位置, input_position[i]);
                if(list_輸入資料表_buf.Count == 0)
                {
                    object[] value = new object[new enum_輸入資料表().GetLength()];
                    value[(int)enum_輸入資料表.GUID] = Guid.NewGuid().ToString();
                    value[(int)enum_輸入資料表.輸入位置] = input_position[i];
                    value[(int)enum_輸入資料表.輸入狀態] = ((bool)state).ToString();
                    list_輸入資料表_add.Add(value);

                }
                else
                {
                    object[] value = list_輸入資料表_buf[0];
                    value[(int)enum_輸入資料表.輸入位置] = input_position[i];
                    value[(int)enum_輸入資料表.輸入狀態] = ((bool)state).ToString();
                    list_輸入資料表_replace.Add(value);
                }
            }
            if (list_輸入資料表_add.Count > 0) sqL_DataGridView_輸入資料表.SQL_AddRows(list_輸入資料表_add, false);
            if (list_輸入資料表_replace.Count > 0) sqL_DataGridView_輸入資料表.SQL_ReplaceExtra(list_輸入資料表_replace, false);

        }
        private void PlC_UI_Init1_UI_Finished_Event()
        {
            SQLUI.SQL_DataGridView.ConnentionClass connentionClass = new SQL_DataGridView.ConnentionClass();
            connentionClass.DataBaseName = "portal";
            connentionClass.IP = "127.0.0.1";
            connentionClass.Port = 3306;
            connentionClass.UserName = "user";
            connentionClass.Password = "66437068";
            SQLUI.SQL_DataGridView.SQL_Set_Properties(this.sqL_DataGridView_輸入資料表, connentionClass);
            SQLUI.SQL_DataGridView.SQL_Set_Properties(this.sqL_DataGridView_輸出資料表, connentionClass);
            this.sqL_DataGridView_輸入資料表.Init();
            this.sqL_DataGridView_輸出資料表.Init();
            sqL_DataGridView_輸入資料表.TableName = "input_list";
            sqL_DataGridView_輸出資料表.TableName = "output_list";
            if (this.sqL_DataGridView_輸入資料表.SQL_IsTableCreat() == false) sqL_DataGridView_輸入資料表.SQL_CreateTable();
            else sqL_DataGridView_輸入資料表.SQL_CheckAllColumnName(true);
            if (this.sqL_DataGridView_輸出資料表.SQL_IsTableCreat() == false) sqL_DataGridView_輸出資料表.SQL_CreateTable();
            else sqL_DataGridView_輸出資料表.SQL_CheckAllColumnName(true);

            sqL_DataGridView_輸入資料表.SQL_GetAllRows(true);

            myThread = new MyThread();
            myThread.AutoRun(true);
            myThread.Add_Method(sub_program);
            myThread.SetSleepTime(100);
            myThread.Trigger();
        }
    }
}
