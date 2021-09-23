using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace PearlBag
{
    class Utility
    {
        ////将dt导出到excel
        //// 该方法存在版本问题，未解决
        //public static void ExportToExcel(System.Data.DataTable dt, string url)
        //{
        //    Microsoft.Office.Interop.Excel.Application objExcel = null;
        //    Workbook objWorkbook = null;
        //    Worksheet objsheet = null;

        //    try
        //    {
        //        //申明对象
        //        objExcel = new Microsoft.Office.Interop.Excel.Application();
        //        objWorkbook = objExcel.Workbooks.Add(Missing.Value);
        //        objsheet = (Worksheet)objWorkbook.ActiveSheet;

        //        //设置Excel不可见
        //        objExcel.Visible = false;
        //        objExcel.DisplayAlerts = false;

        //        //设置Excel字段类型全部为字符串
        //        objsheet.Cells.NumberFormat = "@";

        //        //向Excel中写入表格的标头
        //        int displayColumnsCount = 1;
        //        for (int i = 0; i < dt.Columns.Count; i++)
        //        {
        //            objExcel.Cells[1, displayColumnsCount] = dt.Columns[i].ColumnName.Trim();
        //            displayColumnsCount++;
        //        }

        //        //向Excel中逐行逐列写入表格中的数据
        //        for (int row = 0; row < dt.Rows.Count; row++)
        //        {
        //            displayColumnsCount = 1;
        //            for (int col = 0; col < dt.Columns.Count; col++)
        //            {
        //                try
        //                {
        //                    objExcel.Cells[row + 2, displayColumnsCount] = dt.Rows[row][col].ToString().Trim();
        //                    displayColumnsCount++;
        //                }
        //                catch (Exception)
        //                { }
        //            }
        //        }
        //        //保存文件
        //        objWorkbook.SaveAs(url + ".xls", Missing.Value, Missing.Value, Missing.Value,
        //            Missing.Value, Missing.Value,
        //            Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlShared,
        //            Missing.Value, Missing.Value, Missing.Value,
        //            Missing.Value, Missing.Value);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    finally
        //    {
        //        //关闭Excel应用
        //        if (objWorkbook != null) objWorkbook.Close(Missing.Value, Missing.Value, Missing.Value);
        //        if (objExcel.Workbooks != null) objExcel.Workbooks.Close();
        //        if (objExcel != null) objExcel.Quit();

        //        //杀死进程
        //        Utility.KillProcess("Excel");
        //        objsheet = null;
        //        objWorkbook = null;
        //        objExcel = null;
        //    }
        //}

        /// <summary>
        /// 根据进程名称杀死进程 
        /// </summary>
        /// <param name=" ProcessName "> DataTable</param>
        private static void KillProcess(string ProcessName)
        {
            System.Diagnostics.Process myproc = new System.Diagnostics.Process();
            try
            {
                foreach (System.Diagnostics.Process thisproc in System.Diagnostics.Process.GetProcessesByName(ProcessName))
                    if (!thisproc.CloseMainWindow())
                        thisproc.Kill();
            }
            catch (Exception ex)
            { throw new Exception("", ex); }
        }

        //将dt导出到excel
        public static void ExportToExcel2(System.Data.DataTable m_DataTable, string s_FileName)
        {
            string FileName = s_FileName + ".xls";
            if (File.Exists(FileName))
                File.Delete(FileName);
            FileStream objFileStream;
            StreamWriter objStreamWriter;
            string strLine = "";
            objFileStream = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Write);
            objStreamWriter = new StreamWriter(objFileStream, System.Text.Encoding.Unicode);
            for (int i = 0; i < m_DataTable.Columns.Count; i++)
            {
                strLine = strLine + m_DataTable.Columns[i].Caption.ToString() + Convert.ToChar(9);
            }
            objStreamWriter.WriteLine(strLine);
            strLine = "";

            for (int i = 0; i < m_DataTable.Rows.Count; i++)
            {
                for (int j = 0; j < m_DataTable.Columns.Count; j++)
                {
                    if (m_DataTable.Rows[i].ItemArray[j] == null)
                        strLine = strLine + " " + Convert.ToChar(9);
                    else
                    {
                        string rowstr = "";
                        rowstr = m_DataTable.Rows[i].ItemArray[j].ToString();
                        if (rowstr.IndexOf("\r\n") > 0)
                            rowstr = rowstr.Replace("\r\n", " ");
                        if (rowstr.IndexOf("\t") > 0)
                            rowstr = rowstr.Replace("\t", " ");
                        strLine = strLine + rowstr + Convert.ToChar(9);
                    }
                }
                objStreamWriter.WriteLine(strLine);
                strLine = "";
            }
            objStreamWriter.Close();
            objFileStream.Close();
        }

        // 判断奇偶数
        public static bool IsOdd(int n)
        {
            return Convert.ToBoolean(n % 2);
        }

        // 生成控件图片 
        public static void ExportToPicture(Control m_Ctrl, string s_FileName)
        {
            Bitmap bit = new Bitmap(m_Ctrl.Width, m_Ctrl.Height);//实例化一个和窗体一样大的bitmap
            Graphics g = Graphics.FromImage(bit);
            g.CompositingQuality = CompositingQuality.HighQuality;//质量设为最高
            //g.CopyFromScreen(pbBackGround.Left, pbBackGround.Top, 0, 0, new Size(pbBackGround.Width, pbBackGround.Height));//保存整个窗体为图片
            g.CopyFromScreen(m_Ctrl.PointToScreen(System.Drawing.Point.Empty), System.Drawing.Point.Empty, m_Ctrl.Size);//只保存某个控件（这里是panel游戏区）

            bit.Save(s_FileName);//默认保存格式为PNG，保存成jpg格式质量不是很好
        }
    }
}
