using Spire.Xls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestExcel
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable("员工信息表");
            dt.Columns.Add("name");
            dt.Columns.Add("gender");
            dt.Columns.Add("age");
            DataRow dr1 = dt.NewRow();
            dr1["name"] = "张三";
            dr1["gender"] = "男";
            dr1["age"] = "21";
            dt.Rows.Add(dr1);
            DataRow dr2 = dt.NewRow();
            dr2["name"] = "李四";
            dr2["gender"] = "变性";
            dr2["age"] = "33";
            dt.Rows.Add(dr2);
            DataRow dr3 = dt.NewRow();
            dr3["name"] = "李娜";
            dr3["gender"] = "女";
            dr3["age"] = "40";
            dt.Rows.Add(dr3);

            gridControl1.DataSource = dt;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //创建1个工作簿，相当于1个Excel文件
            //Excel的文档结构是 Workbook->Worksheet（1个book可以包含多个sheet）
            Workbook workbook = new Workbook();

            //获取第一个sheet，进行操作，下标是从0开始
            Worksheet sheet = workbook.Worksheets[0];
            sheet.Name = "测试excel";

            //向A1单元格写入文字
            sheet.Range["A1"].Text = "Hello,World!";

            //将Excel文件保存到指定文件,还可以指定Excel版本
            workbook.SaveToFile("Sample.xlsx", ExcelVersion.Version2007);

            System.Diagnostics.Process.Start("Sample.xlsx");
        }
    }
}
