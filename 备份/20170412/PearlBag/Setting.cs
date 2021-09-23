using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PearlBag
{
    public partial class SettingForm : Form
    {
        public DataTable dt;
        public string title;
        public SettingForm()
        {
            InitializeComponent();

            dt = new DataTable();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            dt.Clear();

            DataRow dr1 = dt.NewRow();
            dr1["bagTyle"] = "大包";
            dr1["firstRowNum"] = string.IsNullOrEmpty(tb_bigFirstRowNum.Text.Trim()) ? "23" : tb_bigFirstRowNum.Text.Trim();
            dr1["allRowIndex"] = string.IsNullOrEmpty(tb_bigAllRowNum.Text.Trim()) ? "32" : tb_bigAllRowNum.Text.Trim();
            dr1["bgColor"] = cb_bgColor.SelectedValue;
            dt.Rows.Add(dr1);

            DataRow dr2 = dt.NewRow();
            dr2["bagTyle"] = "中包";
            dr2["firstRowNum"] = string.IsNullOrEmpty(tb_midFirstRowNum.Text.Trim()) ? "15" : tb_midFirstRowNum.Text.Trim();
            dr2["allRowIndex"] = string.IsNullOrEmpty(tb_midAllRowNum.Text.Trim()) ? "25" : tb_midAllRowNum.Text.Trim();
            dt.Rows.Add(dr2);

            DataRow dr3 = dt.NewRow();
            dr3["bagTyle"] = "小包";
            dr3["firstRowNum"] = string.IsNullOrEmpty(tb_miniFirstRowNum.Text.Trim()) ? "10" : tb_miniFirstRowNum.Text.Trim();
            dr3["allRowIndex"] = string.IsNullOrEmpty(tb_miniAllRowNum.Text.Trim()) ? "15" : tb_miniAllRowNum.Text.Trim();
            dt.Rows.Add(dr3);

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            ds.WriteXml(Application.StartupPath + "\\setting.xml");

            this.Close();
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            InitColorList();
            InitValue();

        }

        private void InitValue()
        {
            string settingXml = Application.StartupPath + "\\setting.xml";
            if (!File.Exists(settingXml))
            {
                tb_bigFirstRowNum.Text = "23";
                tb_bigAllRowNum.Text = "32";
                tb_midFirstRowNum.Text = "15";
                tb_midAllRowNum.Text = "25";
                tb_miniFirstRowNum.Text = "10";
                tb_miniAllRowNum.Text = "20";

                cb_bgColor.SelectedValue = ColorUtility.Blue().Name;

                return;
            }

            DataSet ds = new DataSet();
            ds.ReadXml(settingXml);
            if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                tb_bigFirstRowNum.Text = "23";
                tb_bigAllRowNum.Text = "32";
                tb_midFirstRowNum.Text = "15";
                tb_midAllRowNum.Text = "25";
                tb_miniFirstRowNum.Text = "10";
                tb_miniAllRowNum.Text = "20";

                cb_bgColor.SelectedValue = ColorUtility.Blue().Name;

                return;
            }

            dt = ds.Tables[0].Copy();
            tb_bigFirstRowNum.Text = Convert.ToString(dt.Rows[0]["firstRowNum"]);
            tb_bigAllRowNum.Text = Convert.ToString(dt.Rows[0]["allRowIndex"]);
            tb_midFirstRowNum.Text = Convert.ToString(dt.Rows[1]["firstRowNum"]);
            tb_midAllRowNum.Text = Convert.ToString(dt.Rows[1]["allRowIndex"]);
            tb_miniFirstRowNum.Text = Convert.ToString(dt.Rows[2]["firstRowNum"]);
            tb_miniAllRowNum.Text = Convert.ToString(dt.Rows[2]["allRowIndex"]);

            cb_bgColor.SelectedValue = Convert.ToString(dt.Rows[0]["bgColor"]);
        }

        private void InitColorList()
        {
            List<BtnColor> colors = new List<BtnColor>();
            foreach (Color cl in ColorUtility.GetColor4Pearl())
            {
                BtnColor color = new BtnColor(cl);
                colors.Add(color);
            }

            cb_bgColor.DataSource = colors;
            cb_bgColor.DisplayMember = "Name";
            cb_bgColor.ValueMember = "Value";
        }
    }
}
