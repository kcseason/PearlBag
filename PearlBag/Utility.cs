using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
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

        public static void ExportToExcel3(DataTable m_DataTable, string s_FileName, string title)
        {
            //创建1个工作簿，相当于1个Excel文件
            //Excel的文档结构是 Workbook->Worksheet（1个book可以包含多个sheet）
            Workbook workbook = new Workbook();

            //获取第一个sheet，进行操作，下标是从0开始
            Worksheet sheet = workbook.Worksheets[0];
            sheet.Name = title + "_" + m_DataTable.TableName;

            //向A1单元格写入文字
            sheet.Range["A1"].Text = "图案名称：" + title;
            for (int r = 0; r < m_DataTable.Rows.Count; r++)
                for (int c = 0; c < m_DataTable.Columns.Count; c++)
                {
                    sheet.Range[r + 2, c + 1].Text = m_DataTable.Rows[r][c].ToString();
                }

            // 处理内容
            int rowAmount = m_DataTable.Rows.Count + 1;
            string rowLocation = "K" + rowAmount;
            sheet.Range["A2:" + rowLocation].Style.Font.FontName = "微软雅黑";//字体名称
            sheet.Range["A2:" + rowLocation].Style.Font.Size = 20;//字体大小
            sheet.Range["A2:" + rowLocation].RowHeight = 29;
            sheet.Range["A2:" + rowLocation].Style.VerticalAlignment = VerticalAlignType.Bottom;
            int colNum = sheet.Columns.Count();
            for (int i = 0; i < colNum; i++)
                if (i % 2 == 0)
                    sheet.Columns[i].Style.HorizontalAlignment = HorizontalAlignType.Left;
                else
                    sheet.Columns[i].Style.HorizontalAlignment = HorizontalAlignType.Right;

            // 处理顺序列
            sheet.Columns[0].Style.Font.IsItalic = true;
            sheet.Columns[0].Style.Font.Color = Color.Red;

            // 处理标题行
            sheet.Rows[0].Merge();
            sheet.Rows[0].Style.Font.IsItalic = false;
            sheet.Rows[0].Style.Font.Color = Color.Black;
            sheet.Rows[0].Style.Font.FontName = "微软雅黑";//字体名称
            sheet.Rows[0].Style.Font.Size = 29;//字体大小
            sheet.Rows[0].RowHeight = 60;
            sheet.Rows[0].Style.VerticalAlignment = VerticalAlignType.Top;
            sheet.Rows[0].Style.HorizontalAlignment = HorizontalAlignType.Center;

            // 补充说明行
            rowAmount++;
            sheet.Range["A" + rowAmount + ":K" + rowAmount].Merge();
            sheet.Range["A" + rowAmount + ":K" + rowAmount].Text = "注意：第一个底珠已包括左侧,右侧以及另一半的底珠在内。";
            sheet.Range["A" + rowAmount + ":K" + rowAmount].Style.Font.Color = Color.Black;
            sheet.Range["A" + rowAmount + ":K" + rowAmount].Style.Font.FontName = "微软雅黑";//字体名称
            sheet.Range["A" + rowAmount + ":K" + rowAmount].Style.Font.Size = 14;//字体大小
            sheet.Range["A" + rowAmount + ":K" + rowAmount].RowHeight = 20;
            sheet.Range["A" + rowAmount + ":K" + rowAmount].Style.VerticalAlignment = VerticalAlignType.Bottom;
            sheet.Range["A" + rowAmount + ":K" + rowAmount].Style.HorizontalAlignment = HorizontalAlignType.Right;

            // 全部自适应
            //for (int c = 1; c <= sheet.Columns.Count(); c++)
            //sheet.AutoFitColumn(2);

            // 页面设置
            sheet.PageSetup.Orientation = PageOrientationType.Portrait;
            sheet.PageSetup.PaperSize = PaperSizeType.PaperA4;
            sheet.PageSetup.LeftMargin = sheet.PageSetup.RightMargin = 0.2;
            sheet.PageSetup.TopMargin = sheet.PageSetup.BottomMargin = 0.6;
            sheet.PageSetup.CenterHorizontally = true;
            sheet.PageSetup.BlackAndWhite = false;

            // 将Excel文件保存到指定文件,还可以指定Excel版本
            try
            {
                workbook.SaveToFile(s_FileName + ".xls", ExcelVersion.Version97to2003);
            }
            catch
            {
                KillProcess("Excel");
                workbook.SaveToFile(s_FileName + ".xls", ExcelVersion.Version97to2003);
            }

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

        public static DataTable CopyFromDataTable(DataTable fromDt)
        {
            DataTable toDt = fromDt.Clone();
            foreach (DataRow dr in fromDt.Rows)
                toDt.ImportRow(dr);

            return toDt;
        }
    }
}
