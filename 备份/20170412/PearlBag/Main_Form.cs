﻿using GlacialComponents.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestPingtu;

namespace PearlBag
{
    public partial class Main_Form : Form
    {
        // 另起线程显示等待界面
        Thread TD;

        // 画板上所有的珠子列表
        private Pearls pearls;
        private Pearl curPearl;
        // 指定珠子大小
        private Size size;
        //// 程序默认设置缓存
        //private DataTable dt;
        //  默认珠子全局布局
        private int colNum = 23;
        private int rowNum = 31;
        // 是否首次绘画
        private bool isFirstPaint = true;
        // 是否首次打开分解模板
        private bool isFirstResult2 = true;
        // 是否绘画花边
        private bool isBagBorder = true;
        private int borderHeight = 15;
        private PictureBox[] pbBorder;
        // 背景画布
        private PictureBox pbBackGround;
        private Label halfLb;
        // 横向,竖向珠子间隔
        private int spaceLeft2Right;
        private int spaceTop2Bot;
        // 当前选择颜色
        private string dotColor;

        public Main_Form()
        {
#if DEBUG
            colNum = 10;
            rowNum = 15;
#endif
            size = new System.Drawing.Size(24, 24);
            spaceLeft2Right = size.Width / 2;
            spaceTop2Bot = 7;
            pearls = new Pearls();
            halfLb = new Label();
            if (isBagBorder)
            {
                pbBorder = new PictureBox[4];
                pbBorder[0] = new PictureBox();
                pbBorder[1] = new PictureBox();
                pbBorder[2] = new PictureBox();
                pbBorder[3] = new PictureBox();
            }
            pbBackGround = new PictureBox();
            InitializeComponent();
        }

        /// <summary>
        /// 页面载入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_Form_Load(object sender, EventArgs e)
        {
            // 加载按钮提示语
            SetToolTips();
            // 初始珠子布局
            InitRowColumnNum();
            // 初始化颜色列表
            InitColorList();
            // 初始化结果列表
            InitListView();
            // 生成珠子
            CreatePearl();
            // 绘画作业
            PaintWork();
            // 计算珠子数量
            CalculateResult(CalculateType.all);

            pl_main3.Focus();
            timer1.Enabled = true;
        }

        /// <summary>
        /// 初始化颜色列表
        /// </summary>
        private void InitColorList()
        {
            cb_bgColor1.BringToFront();
            List<BtnColor> colors1 = new List<BtnColor>();
            foreach (Color cl in ColorUtility.GetColor4Canvas())
            {
                BtnColor color = new BtnColor(cl);
                colors1.Add(color);
            }
            cb_bgColor1.DataSource = colors1;
            cb_bgColor1.DisplayMember = "Name";
            cb_bgColor1.ValueMember = "Value";
            cb_bgColor1.SelectedValue = ColorUtility.BlackBG().Name;
            cb_bgColor1.SelectedIndexChanged += cb_bgColor1_SelectedIndexChanged;

            cb_bgColor2.BringToFront();
            List<BtnColor> colors = new List<BtnColor>();
            foreach (Color cl in ColorUtility.GetColor4Pearl())
            {
                BtnColor color = new BtnColor(cl);
                colors.Add(color);
            }
            cb_bgColor2.DataSource = colors;
            cb_bgColor2.DisplayMember = "Name";
            cb_bgColor2.ValueMember = "Value";
            cb_bgColor2.SelectedValue = ColorUtility.Blue().Name;
            cb_bgColor2.SelectedIndexChanged += cb_bgColor2_SelectedIndexChanged;

            foreach (Control ctl in gb_color.Controls)
            {
                if (ctl is RadioButton)
                {
                    RadioButton rdBtn = (RadioButton)ctl;
                    rdBtn.Click += rdBtn_Click;
                }
            }
        }

        /// <summary>
        /// 记录选择的颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdBtn_Click(object sender, EventArgs e)
        {
            RadioButton rdBtn = (RadioButton)sender;
            if (rdBtn.Checked)
                dotColor = rdBtn.Tag.ToString();
        }

        /// <summary>
        /// 初始珠子布局
        /// </summary>
        private void InitRowColumnNum()
        {
            tb_Col.Text = colNum.ToString();
            tb_Row.Text = rowNum.ToString();
            tb_allNum.Text = (colNum * rowNum * 2).ToString();
        }

        /// <summary>
        /// 初始化珠子结果列表界面
        /// </summary>
        private void InitListView()
        {
            ImageList image = new ImageList();
            image.ImageSize = new Size(1, 30);
            lv_result.SmallImageList = image;

            lv_result.View = View.Details;
            lv_result.Columns[0].Width = Convert.ToInt32(lv_result.Width * 0.2);
            lv_result.Columns[1].Width = Convert.ToInt32(lv_result.Width * 0.5);
            lv_result.Columns[2].Width = Convert.ToInt32(lv_result.Width * 0.2);
            //lv_result.Columns[3].Width = Convert.ToInt32(lv_result.Width * 0.18);

            lv_result2.Columns[0].Width = Convert.ToInt32(lv_result.Width * 0.2);
            lv_result2.Columns[1].Width = Convert.ToInt32(lv_result.Width * 0.5);
            lv_result2.Columns[2].Width = Convert.ToInt32(lv_result.Width * 0.2);
        }

        /// <summary>
        /// 生成珠子
        /// </summary>
        private void CreatePearl()
        {
            int row = Convert.ToInt32(tb_Row.Text.Trim());
            int col = Convert.ToInt32(tb_Col.Text.Trim());

            Color bgColorCanvas = ColorUtility.GetColor(cb_bgColor1.SelectedValue.ToString());
            Color bgColorPearls = ColorUtility.GetColor(cb_bgColor2.SelectedValue.ToString());

            pearls.CreatePearls(col, row, bgColorCanvas, bgColorPearls, size);
        }

        /// <summary>
        /// 绘制工作
        /// </summary>
        private void PaintWork()
        {
            // 开始绘画位置
            Point local = new Point(0, 0);
            // 计算预计珠子画布长高
            int[] intArr = GetLengthAndHeight();
            int length = intArr[0];
            int height = intArr[1];
            // 绘画行号标尺
            PaintRowNum(height);
            // 绘画花边
            PaintBagBorder(ref local, length, height);
            // 绘画一半位置分界面
            PaintHalfBorder(ref local, length, height);
            // 绘制背景画布
            PaintBackGround();
            // 绘制珠子
            PaintPearl();

            isFirstPaint = false;
        }

        /// <summary>
        /// 绘画珠子
        /// </summary>
        private void PaintPearl()
        {
            if (!isFirstPaint)
                return;

            // 辅助绘画位置
            Point point = new Point(0, 0);
            // 循环画出所有珠子
            for (int row = 1; row <= rowNum; row++)
            {
                // 第二排开始缩小上下间隔
                if (row > 1)
                    point.Y -= spaceTop2Bot;

                for (int col = 1; col <= colNum; col++)
                {
                    // 第一行第一列后退一半宽度开始添加
                    if (col == 1 && Utility.IsOdd(row))
                        point.X = point.X + size.Width / 2 + size.Width / 2 / 2;

                    Pearl pl = pearls.PearlListSet[row - 1][col - 1];
                    pl.btn.Location = new Point(point.X, point.Y);
                    pl.btn.MouseEnter += btn_MouseEnter;
                    pl.btn.Click += btn_Click;
                    pbBackGround.Controls.Add(pl.btn);
                    //Graphics gra = pbBackGround.CreateGraphics();
                    //gra.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    //Brush bush = new SolidBrush(Color.Green);//填充的颜色
                    //gra.FillEllipse(bush, point.X, point.Y, 24, 24);//
                    point.X += size.Width + size.Width / 2;
                }

                point.Y = point.Y + size.Width;
                point.X = 0;
            }

            tb_allNum.Text = pearls.CountEnable.ToString();
            pbBackGround.Focus();
        }

        /// <summary>
        /// 计算预计画布长高
        /// </summary>
        /// <returns></returns>
        private int[] GetLengthAndHeight()
        {
            int row = Convert.ToInt32(tb_Row.Text.Trim());
            int col = Convert.ToInt32(tb_Col.Text.Trim());

            int[] intArray = new int[2];
            int length = col * size.Width + col * size.Width / 2 + size.Width / 2 / 2;
            int height = row * size.Height - (row - 1) * spaceTop2Bot;
            intArray[0] = length;
            intArray[1] = height;

            return intArray;
        }

        /// <summary>
        /// 绘画花边
        /// </summary>
        /// <param name="local"></param>
        private void PaintBagBorder(ref Point local, int length, int height)
        {
            if (!isBagBorder)
                return;

            int width = length + borderHeight;
            pbBorder[0].Size = pbBorder[1].Size = new Size(width, borderHeight);
            //pbBorder[2].Size = pbBorder[3].Size = new Size(borderHeight, height + borderHeight);
            pbBorder[2].Size = new Size(borderHeight, height);
            pbBorder[3].Size = new Size(borderHeight, height + borderHeight);

            pbBorder[0].Location = new Point(local.X, local.Y);
            //pbBorder[1].Location = new Point(local.X + borderHeight, local.Y + borderHeight + height);
            pbBorder[2].Location = new Point(local.X, local.Y + borderHeight);
            pbBorder[3].Location = new Point(local.X + length + borderHeight, local.Y);
            local = new Point(local.X + borderHeight, local.Y + borderHeight);

            if (!isFirstPaint)
                return;

            foreach (PictureBox pb in pbBorder)
            {
                pb.BorderStyle = BorderStyle.None;
                pb.BackgroundImage = imageList1.Images["border"];
                pb.BackgroundImageLayout = ImageLayout.Tile;
                pl_main3.Controls.Add(pb);
            }

            //RotateImage(pbBorder[1], "bottom");
            RotateImage(pbBorder[2], "left");
            RotateImage(pbBorder[3], "right");

            pbBorder[1].Visible = false;
        }

        /// <summary>
        /// 绘画一半位置的分界线
        /// </summary>
        /// <param name="local"></param>
        private void PaintHalfBorder(ref  Point local, int length, int height)
        {
            halfLb.Location = new Point(isBagBorder ? local.X - borderHeight : local.X, local.Y + height);
            halfLb.Width = isBagBorder ? length + borderHeight * 2 : length;

            if (!isFirstPaint)
                return;

            halfLb.AutoSize = false;
            halfLb.BorderStyle = BorderStyle.FixedSingle;
            halfLb.Height = 2;
            pl_main3.Controls.Add(halfLb);
        }

        /// <summary>
        /// 绘制背景画布
        /// </summary>
        private void PaintBackGround()
        {
            int[] intArr = GetLengthAndHeight();
            int length = intArr[0];
            int height = intArr[1];
            pbBackGround.Size = new Size(length, height);
            pbBackGround.Location = new Point(borderHeight, borderHeight);
            pbBackGround.BackColor = ColorUtility.GetColor(cb_bgColor1.SelectedValue.ToString());

            pl_main3.Controls.Add(pbBackGround);

            pbBackGround.SendToBack();
        }

        /// <summary>
        /// 旋转pictureBox的背景图片
        /// </summary>
        /// <param name="pb"></param>
        /// <param name="direction"></param>
        private void RotateImage(PictureBox pb, string direction)
        {
            Image img = pb.BackgroundImage;

            switch (direction)
            {
                case "right": img.RotateFlip(RotateFlipType.Rotate90FlipY); break;
                case "bottom": img.RotateFlip(RotateFlipType.Rotate180FlipX); break;
                case "left": img.RotateFlip(RotateFlipType.Rotate90FlipX); break;
            }

            pb.BackgroundImage = img;
        }

        void btn_MouseEnter(object sender, EventArgs e)
        {
            ExCirclePanel btn = (ExCirclePanel)sender;
            string[] info = btn.Tag.ToString().Split('|');
            curPearl = pearls.GetPearlByRowCol(Convert.ToInt32(info[0]), Convert.ToInt32(info[1]));
            if (curPearl.PeralColor.Name.Equals(ColorUtility.Invisible().Name))
                return;

            int row = Convert.ToInt32(info[0]);
            int col = Convert.ToInt32(info[1]);
            int position = (row - 1) * pearls.ColumnNum + (!Utility.IsOdd(row) ? pearls.ColumnNum - col + 1 : col);

            lb_pearlNO.Text = position.ToString();
            lb_pearlNO.ForeColor = curPearl.PeralColor;
            lb_pearlColor.Text = ColorUtility.GetColorText(curPearl.PeralColor.Name);
            lb_pearlColor.ForeColor = curPearl.PeralColor;
        }

        void btn_Click(object sender, EventArgs e)
        {
            ExCirclePanel btn = (ExCirclePanel)sender;
            if (btn.BackColor.Name.Equals(ColorUtility.Invisible().Name))
                return;

            string[] info = btn.Tag.ToString().Split('|');
            int row = Convert.ToInt32(info[0]);
            int col = Convert.ToInt32(info[1]);
            curPearl = pearls.GetPearlByRowCol(row, col);

            if (!string.IsNullOrEmpty(dotColor))
                RefreshPearls(CalculateType.dot);

            CalculateResult(CalculateType.dot, curPearl.Index);

            btn_MouseEnter(sender, null);
        }

        private void CalculateResult(CalculateType type)
        {
            CalculateResult(type, -1);
        }

        /// <summary>
        /// 
        /// </summary>
        private void CalculateResult(CalculateType type, int pearlIndex)
        {
            int row = Convert.ToInt32(tb_Row.Text.Trim());
            int col = Convert.ToInt32(tb_Col.Text.Trim());
            int allNum = Convert.ToInt32(tb_allNum.Text.Trim());
            string colorName = ColorUtility.GetColorText(cb_bgColor2.SelectedValue.ToString());

            DataTable dt = new DataTable();
            dt.Columns.Add("index", System.Type.GetType("System.String"));
            dt.Columns.Add("colorName", System.Type.GetType("System.String"));
            dt.Columns.Add("colorValue", System.Type.GetType("System.String"));
            dt.Columns.Add("num", System.Type.GetType("System.String"));

            if (type == CalculateType.all)
            {
                DataRow dr = dt.NewRow();
                dr["index"] = "1".PadRight(5, ' ');
                dr["colorName"] = colorName;
                dr["colorValue"] = cb_bgColor2.SelectedValue.ToString();
                dr["num"] = allNum.ToString();
                dt.Rows.Add(dr);
            }

            if (type == CalculateType.backGround || type == CalculateType.dot)
            {
                int rowIndex = 1;
                int colorNum = 0;
                string lastColorVl = null;
                string curColorVl = null;

                for (int r = 0; r < row; r++)
                {
                    // 奇数行，从左到右
                    if (!Utility.IsOdd(r + 1))
                        for (int c = col - 1; c >= 0; c--)
                            ArrayPearls(row, col, r, c, ref rowIndex, ref colorNum,
                                ref lastColorVl, ref curColorVl, ref dt);
                    // 偶数行，从右到左
                    else
                        for (int c = 0; c < col; c++)
                            ArrayPearls(row, col, r, c, ref rowIndex, ref colorNum,
                                ref lastColorVl, ref curColorVl, ref dt);
                }
            }

            int selectedIndex = -1;
            if (dt.Rows.Count > 0)
            {
                int amount = 0;
                bool selected = false;
                foreach (DataRow dr in dt.Rows)
                {
                    amount = amount + Convert.ToInt32(dr["num"]);

                    // 找到当前点击珠子所计算结果的位置并选中该行
                    if (pearlIndex > 0 && !selected)
                    {
                        if (amount >= pearlIndex)
                        {
                            selectedIndex = Convert.ToInt32(dr["index"]);
                            selected = true;
                        }
                    }
                }

                DataRow extraHalf = dt.NewRow();
                extraHalf["index"] = (dt.Rows.Count + 1).ToString().PadRight(5, ' ');
                extraHalf["colorName"] = colorName + "(另一半)";
                extraHalf["colorValue"] = cb_bgColor2.SelectedValue.ToString();
                extraHalf["num"] = amount.ToString();
                dt.Rows.Add(extraHalf);

                DataRow blankRow = dt.NewRow();
                dt.Rows.Add(blankRow);
                DataRow drAmount = dt.NewRow();
                drAmount["colorName"] = "共计：";
                drAmount["num"] = (amount * 2).ToString();
                dt.Rows.Add(drAmount);
            }

            lv_result.Items.Clear();
            lv_result2.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                ListViewItem item = new ListViewItem(dr[0].ToString());
                item.Font = new Font(item.Font.FontFamily, 14f, item.Font.Style | FontStyle.Bold);
                //item.UseItemStyleForSubItems = false;
                item.SubItems.Add(dr[1].ToString());
                item.SubItems.Add(dr[3].ToString());
                item.SubItems.Add(string.Empty);
                //item.SubItems[3].BackColor = ColorUtility.GetColor(dr[2].ToString());
                lv_result.Items.Add(item);
                //ListViewItem colorItem = new ListViewItem(string.Empty);
                //colorItem.BackColor = ColorUtility.GetColor(dr[2].ToString());
                //lv_showColor.Items.Add(colorItem);
                //foreach (System.Windows.Forms.ListViewItem.ListViewSubItem subItem in item.SubItems)
                //    subItem.Font = new Font(subItem.Font.FontFamily, 14f,
                //        subItem.Font.Style | FontStyle.Bold);

                GLItem item2 = new GLItem();
                item2.Text = dr[0].ToString();
                GLSubItem subItem1 = new GLSubItem();
                if (dr[1].ToString().Equals(string.Empty))
                    subItem1.Text = dr[1].ToString();
                else
                    subItem1.Text = dr[1].ToString().Substring(0, 2).Equals(cb_bgColor2.Text) ?
                        "底珠" : dr[1].ToString();
                item2.SubItems.Add(subItem1);
                GLSubItem subItem2 = new GLSubItem();
                subItem2.Text = dr[3].ToString();
                item2.SubItems.Add(subItem2);
                lv_result2.Items.Add(item2);
            }

            if (selectedIndex > 0)
            {
                lv_result.Items[selectedIndex - 1].Selected = true;
                lv_result.TopItem = lv_result.Items[selectedIndex - 1];

                lv_result2.Items[selectedIndex - 1].Selected = true;
                lv_result2.HotItemIndex = selectedIndex - 1;
            }
            //lv_result.EnsureVisible(0);
            if (tab_listview.SelectedIndex == 0)
                lv_result.Focus();
            else
                lv_result2.Focus();
        }

        private void ArrayPearls(int row, int col, int r, int c, ref int rowIndex, ref int colorNum,
            ref string lastColorVl, ref string curColorVl, ref DataTable dt)
        {
            string curColorName = pearls.PearlListSet[r][c].ColorName;
            curColorVl = pearls.PearlListSet[r][c].PeralColor.Name;
            // 第一个珠子
            if (string.IsNullOrEmpty(lastColorVl))
            {
                lastColorVl = curColorVl;
                colorNum++;
                return;
            }

            if (lastColorVl.Equals(curColorVl))
            {
                // 判断是否偶数行最后一个
                if ((Utility.IsOdd(r + 1) && r == (row - 1) && c == (col - 1)) ||
                    (!Utility.IsOdd(r + 1) && r == (row - 1) && c == 0))
                {
                    colorNum++;
                    DataRow dr = dt.NewRow();
                    dr["index"] = rowIndex.ToString().PadRight(5, ' ');
                    dr["colorName"] = curColorName;
                    dr["colorValue"] = curColorVl;
                    dr["num"] = colorNum.ToString();
                    dt.Rows.Add(dr);
                    return;
                }
                lastColorVl = curColorVl;
                colorNum++;
                return;
            }

            if (!lastColorVl.Equals(curColorVl))
            {
                // 最后一个
                if (r == (row - 1))
                {
                    if ((Utility.IsOdd(r + 1) && (c + 1) == col) || (!Utility.IsOdd(r + 1) && c == 0))
                    {
                        DataRow dr1 = dt.NewRow();
                        dr1["index"] = rowIndex.ToString().PadRight(5, ' ');
                        dr1["colorName"] = ColorUtility.GetColorText(lastColorVl);
                        dr1["colorValue"] = lastColorVl;
                        dr1["num"] = colorNum.ToString();
                        dt.Rows.Add(dr1);
                        colorNum = 0;
                        lastColorVl = curColorVl;
                        colorNum++;
                        rowIndex++;

                        DataRow dr2 = dt.NewRow();
                        dr2["index"] = rowIndex.ToString().PadRight(5, ' ');
                        dr2["colorName"] = curColorName;
                        dr2["colorValue"] = curColorVl;
                        dr2["num"] = colorNum.ToString();
                        dt.Rows.Add(dr2);

                        return;
                    }
                }
            }
            DataRow dr3 = dt.NewRow();
            dr3["index"] = rowIndex.ToString().PadRight(5, ' ');
            dr3["colorName"] = ColorUtility.GetColorText(lastColorVl); ;
            dr3["colorValue"] = lastColorVl;
            dr3["num"] = colorNum.ToString();
            dt.Rows.Add(dr3);
            colorNum = 0;
            lastColorVl = curColorVl;
            colorNum++;
            rowIndex++;
        }

        /// <summary>
        /// 描绘行数目
        /// </summary>
        /// <param name="local"></param>
        private void PaintRowNum(int height)
        {
            Point local = new Point(0, 0);
            pl_rowNum.Controls.Clear();

            int rowNum = Convert.ToInt32(tb_Row.Text.Trim());
            // 处理是否绘画花边
            int allRowNum = rowNum + 1;
            int startRow = isBagBorder ? 0 : 1;
            int rowHeight = height / rowNum;

            Size labelSize = new Size(pl_rowNum.Width, rowHeight);
            // 开始绘画行号
            for (int i = startRow; i < allRowNum; i++)
            {
                Label lb = new Label();
                lb.AutoSize = false;
                lb.BorderStyle = BorderStyle.FixedSingle;
                lb.Size = labelSize;
                lb.Font = new Font(lb.Font.FontFamily, 12f, lb.Font.Style | FontStyle.Bold);
                lb.TextAlign = ContentAlignment.MiddleCenter;

                if (isBagBorder && i == 0)
                {
                    lb.Text = string.Empty;
                    lb.Size = new Size(labelSize.Width, borderHeight + 2);
                }
                else
                    lb.Text = i.ToString();

                lb.Location = new Point(local.X, local.Y);
                pl_rowNum.Controls.Add(lb);

                local.Y = local.Y + lb.Size.Height;
            }
            local.X = local.X + labelSize.Width;
            local.Y = 0;
        }

        private void btn_bigBag_Click(object sender, EventArgs e)
        {
            tb_Col.Text = "23";
            tb_Row.Text = "31";
            tb_allNum.Text = (Convert.ToInt32(tb_Row.Text.Trim()) * Convert.ToInt32(tb_Col.Text.Trim())).ToString();

            RefreshPearls(CalculateType.all);

            PaintWork();

            CalculateResult(CalculateType.all);
        }

        private void btn_midBag_Click(object sender, EventArgs e)
        {
            tb_Col.Text = "15";
            tb_Row.Text = "20";
            tb_allNum.Text = (Convert.ToInt32(tb_Row.Text.Trim()) * Convert.ToInt32(tb_Col.Text.Trim())).ToString();

            RefreshPearls(CalculateType.all);

            PaintWork();

            pl_main3.Refresh();

            CalculateResult(CalculateType.all);
        }

        private void btn_miniBag_Click(object sender, EventArgs e)
        {
            tb_Col.Text = "10";
            tb_Row.Text = "15";
            tb_allNum.Text = (Convert.ToInt32(tb_Row.Text.Trim()) * Convert.ToInt32(tb_Col.Text.Trim())).ToString();

            RefreshPearls(CalculateType.all);

            PaintWork();

            CalculateResult(CalculateType.all);
        }

        private void btn_createmBag_Click(object sender, EventArgs e)
        {
            if (!CheckNumber())
            {
                MessageBox.Show("请输入正确数字范围！\r\n列数：1-23 \r\n排数：1-31", "注意", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            rowNum = Convert.ToInt32(tb_Row.Text.Trim());
            colNum = Convert.ToInt32(tb_Col.Text.Trim());
            tb_allNum.Text = (Convert.ToInt32(tb_Row.Text.Trim()) * Convert.ToInt32(tb_Col.Text.Trim())).ToString();

            RefreshPearls(CalculateType.all);

            PaintWork();

            CalculateResult(CalculateType.all);
        }

        private bool CheckNumber()
        {
            bool bl = true;
            int row = Convert.ToInt32(tb_Row.Text.Trim());
            int col = Convert.ToInt32(tb_Col.Text.Trim());

            if (row > 31 || row == 0)
            {
                bl = false;
                tb_Row.Text = pearls.RowNum.ToString();
            }
            if (col > 23 || col == 0)
            {
                bl = false;
                tb_Col.Text = pearls.ColumnNum.ToString();
            }

            return bl;
        }

        /// <summary>
        /// 
        /// </summary>
        private void RefreshPearls(CalculateType type)
        {
            int row;
            int col;
            Color color;
            if (type == CalculateType.dot)
            {
                row = curPearl.RowNO;
                col = curPearl.ColumnNO;
                color = ColorUtility.GetColor(dotColor);
            }
            else
            {
                row = Convert.ToInt32(tb_Row.Text.Trim());
                col = Convert.ToInt32(tb_Col.Text.Trim());
                color = ColorUtility.GetColor(cb_bgColor2.SelectedValue.ToString());
            }

            pearls.RefreshPearls(col, row, color, type);
        }

        private void cb_bgColor1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pbBackGround.BackColor = ColorUtility.GetColor(cb_bgColor1.SelectedValue.ToString());

            pearls.BgColorCanvas = ColorUtility.GetColor(cb_bgColor1.SelectedValue.ToString());
        }

        private void cb_bgColor2_SelectedIndexChanged(object sender, EventArgs e)
        {
            pearls.BgColorPearls = ColorUtility.GetColor(cb_bgColor2.SelectedValue.ToString());

            RefreshPearls(CalculateType.backGround);

            CalculateResult(CalculateType.backGround);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_save_Click(object sender, EventArgs e)
        {
            using (TitleForm titleFrm = new TitleForm())
            {
                if (titleFrm.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return;

                string title = titleFrm.title;
                DataTable sampleDtHead = new DataTable();
                sampleDtHead.Columns.Add("colNum");
                sampleDtHead.Columns.Add("rowNum");
                sampleDtHead.Columns.Add("allRowNum");
                sampleDtHead.Columns.Add("width");
                sampleDtHead.Columns.Add("height");
                sampleDtHead.Columns.Add("bgColorPearls");
                sampleDtHead.Columns.Add("bgColorCanvas");

                DataTable sampleDtBody = new DataTable();
                sampleDtBody.Columns.Add("index");
                sampleDtBody.Columns.Add("color");
                //sampleDtBody.Columns.Add("isClick");
                sampleDtBody.Columns.Add("rowNO");
                sampleDtBody.Columns.Add("colNO");

                int i = 1;
                foreach (List<Pearl> list in pearls.PearlListSet)
                    foreach (Pearl pl in list)
                    {
                        if (sampleDtHead.Rows.Count == 0)
                        {
                            DataRow newRowHead = sampleDtHead.NewRow();
                            newRowHead["colNum"] = tb_Col.Text;
                            newRowHead["rowNum"] = tb_Row.Text;
                            newRowHead["allRowNum"] = tb_allNum.Text;
                            newRowHead["width"] = pl.PearlSize.Width;
                            newRowHead["height"] = pl.PearlSize.Height;
                            newRowHead["bgColorPearls"] = pearls.BgColorPearls.Name;
                            newRowHead["bgColorCanvas"] = pearls.BgColorCanvas.Name;
                            sampleDtHead.Rows.Add(newRowHead);
                        }

                        DataRow newRowBody = sampleDtBody.NewRow();
                        newRowBody["index"] = pl.Index;
                        newRowBody["color"] = pl.PeralColor.Name;
                        //newRowBody["isClick"] = pl.IsClick ? "1" : "0";
                        newRowBody["rowNO"] = pl.RowNO.ToString();
                        newRowBody["colNO"] = pl.ColumnNO.ToString();
                        sampleDtBody.Rows.Add(newRowBody);
                    }

                DataSet ds = new DataSet();
                ds.Tables.Add(sampleDtHead);
                ds.Tables.Add(sampleDtBody);
                ds.WriteXml(Application.StartupPath + "\\" + title + ".xml");

                string listXml = Application.StartupPath + "\\fileNameList.xml";
                DataSet ds2 = new DataSet();
                if (File.Exists(listXml))
                {
                    ds2.ReadXml(listXml);
                    if (ds2.Tables.Count == 0 || ds2.Tables[0].Rows.Count == 0)
                    {
                        File.Delete(listXml);
                        DataTable fileNameListDt = new DataTable();
                        fileNameListDt.Columns.Add("fileName");
                        DataRow dr2 = fileNameListDt.NewRow();
                        dr2["fileName"] = title;
                        fileNameListDt.Rows.Add(dr2);
                        ds2.Tables.Add(fileNameListDt);
                    }
                    else
                    {
                        List<int> repeatIndex = new List<int>();
                        int delIndex = 0;
                        foreach (DataRow dr in ds2.Tables[0].Rows)
                        {
                            if (dr["fileName"].ToString().Equals(title))
                                repeatIndex.Add(delIndex);
                            delIndex++;
                        }

                        DataRow[] drs = ds2.Tables[0].Select("fileName = '" + title + "'");
                        foreach (DataRow dr in drs)
                            ds2.Tables[0].Rows.Remove(dr);

                        DataRow dr2 = ds2.Tables[0].NewRow();
                        dr2["fileName"] = title;
                        ds2.Tables[0].Rows.InsertAt(dr2, 0);

                        File.Delete(listXml);
                    }
                }
                else
                {
                    DataTable fileNameListDt = new DataTable();
                    fileNameListDt.Columns.Add("fileName");
                    DataRow dr2 = fileNameListDt.NewRow();
                    dr2["fileName"] = title;
                    fileNameListDt.Rows.Add(dr2);
                    ds2.Tables.Add(fileNameListDt);
                }
                ds2.WriteXml(listXml);

                loadFileNameList();
            }

            MessageBox.Show("该模板已成功保存！", "保存成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btn_print_Click(object sender, EventArgs e)
        {

        }

        private void cb_sampleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cb_sampleList.Text.Trim()))
                return;

            string sampleXml = Application.StartupPath + "\\" + cb_sampleList.Text + ".xml";
            if (!File.Exists(sampleXml))
            {
                MessageBox.Show("无法找到该模板！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataSet ds = new DataSet();
            ds.ReadXml(sampleXml);
            if (ds.Tables.Count < 2 || ds.Tables[0].Rows.Count == 0)
                return;

            DataTable dt1 = ds.Tables[0];
            size = new System.Drawing.Size(Convert.ToInt32(dt1.Rows[0]["width"]),
                Convert.ToInt32(dt1.Rows[0]["height"]));
            tb_Col.Text = Convert.ToString(dt1.Rows[0]["colNum"]);
            tb_Row.Text = Convert.ToString(dt1.Rows[0]["rowNum"]);
            tb_allNum.Text = Convert.ToString(dt1.Rows[0]["allRowNum"]);
            pearls.BgColorPearls = ColorUtility.GetColor(dt1.Rows[0]["bgColorPearls"].ToString());
            pearls.BgColorCanvas = ColorUtility.GetColor(dt1.Rows[0]["bgColorCanvas"].ToString());
            cb_bgColor1.SelectedValueChanged -= cb_bgColor1_SelectedIndexChanged;
            cb_bgColor2.SelectedValueChanged -= cb_bgColor2_SelectedIndexChanged;
            cb_bgColor1.SelectedValue = pearls.BgColorCanvas.Name;
            cb_bgColor2.SelectedValue = pearls.BgColorPearls.Name;
            RefreshPearls(CalculateType.all);
            PaintWork();
            cb_bgColor2.SelectedValueChanged += cb_bgColor1_SelectedIndexChanged;
            cb_bgColor2.SelectedValueChanged += cb_bgColor1_SelectedIndexChanged;

            DataTable dt2 = ds.Tables[1];
            foreach (DataRow dr in dt2.Rows)
            {
                //bool isClick = Convert.ToString(dr["isClick"]).Equals("1") ? true : false;
                // 背景点已画，无需再重画
                //if (!isClick)
                //    continue;

                Color cl = ColorUtility.GetColor(Convert.ToString(dr["color"]));
                int rowNO = Convert.ToInt32(dr["rowNO"]);
                int colNO = Convert.ToInt32(dr["colNO"]);
                pearls.RefreshPearls(colNO, rowNO, cl, CalculateType.dot);
            }

            CalculateResult(CalculateType.dot);
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool loadSampleList = false;
        private void cb_sampleList_Click(object sender, EventArgs e)
        {
            if (loadSampleList)
                return;

            loadFileNameList();
            loadSampleList = true;
        }

        private void loadFileNameList()
        {
            string listXml = Application.StartupPath + "\\fileNameList.xml";
            if (!File.Exists(listXml))
                return;

            cb_sampleList.Items.Clear();

            DataSet ds = new DataSet();
            ds.ReadXml(listXml);
            if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return;

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                cb_sampleList.Items.Add(ds.Tables[0].Rows[i][0].ToString());
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cb_sampleList.Text.Trim()))
                return;

            string fileName = cb_sampleList.Text.Trim();

            string sampleXml = Application.StartupPath + "\\" + fileName + ".xml";
            if (File.Exists(sampleXml))
                File.Delete(sampleXml);

            string listXml = Application.StartupPath + "\\fileNameList.xml";
            DataSet ds2 = new DataSet();
            if (File.Exists(listXml))
            {
                ds2.ReadXml(listXml);
                if (ds2.Tables.Count == 0 || ds2.Tables[0].Rows.Count == 0)
                    return;

                int delIndex = 0;
                foreach (DataRow dr in ds2.Tables[0].Rows)
                {
                    if (dr["fileName"].ToString().Equals(fileName))
                        break;
                    delIndex++;
                }

                ds2.Tables[0].Rows.Remove(ds2.Tables[0].Rows[delIndex]);

                File.Delete(listXml);
                ds2.WriteXml(listXml);
            }

            loadFileNameList();
        }

        private void btn_setting_Click(object sender, EventArgs e)
        {
            //using (SettingForm settingFrm = new SettingForm())
            //{
            //    if (settingFrm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //    {
            //        dt.Clear();
            //        dt = settingFrm.dt.Copy();
            //    }
            //}
        }

        private DataTable SettingDt()
        {
            string settingXml = Application.StartupPath + "\\setting.xml";
            if (!File.Exists(settingXml))
                return null;

            DataSet ds = new DataSet();
            ds.ReadXml(settingXml);
            if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return null;

            return ds.Tables[0];
        }

        private void ShowWaitForm()
        {
            Wait_Form waitFrm = new Wait_Form();
            waitFrm.ShowDialog();

        }

        private void btn_export_Click(object sender, EventArgs e)
        {
            // 以下为导出text文本
            //string fileName = string.IsNullOrEmpty(cb_sampleList.Text.Trim()) ?
            //    DateTime.Now.ToString("yy年MM月dd日HH时mm分") : cb_sampleList.Text.Trim();

            //string newFileName = Application.StartupPath + "\\" + fileName + ".txt";
            //if (File.Exists(newFileName))
            //    File.Delete(newFileName);

            //FileStream fs = new FileStream(newFileName, FileMode.Create);
            //StreamWriter sw = new StreamWriter(fs);
            ////开始写入
            //sw.Write(fileName + "\r\n");
            //sw.Write("顺序   ");
            //sw.Write("颜色     ");
            //sw.Write("数量     " + "\r\n");

            //foreach (ListViewItem item in lv_result.Items)
            //{
            //    sw.Write(item.SubItems[0].Text + "      ");
            //    sw.Write(item.SubItems[1].Text + "      ");
            //    sw.Write(item.SubItems[2].Text + "\r\n");
            //}

            ////清空缓冲区
            //sw.Flush();
            ////关闭流
            //sw.Close();
            //fs.Close();

            //System.Diagnostics.Process.Start(newFileName); //打开此文件。

            // 以下为导出excel文本
            string fileNmae = "";
            if (string.IsNullOrEmpty(cb_sampleList.Text))
                fileNmae = "珠子总数_" + DateTime.Now.ToString("yy年MM月dd日HH时mm分ss秒");
            else
                fileNmae = cb_sampleList.Text;

            DataTable dt = null;
            if (tab_listview.SelectedIndex == 0)
                dt = ExportResult1(fileNmae);
            else
                dt = ExportResult2(fileNmae);

            Utility.ExportToExcel2(dt, fileNmae);
            //打开此文件。
            System.Diagnostics.Process.Start(fileNmae + ".xls");
        }

        private DataTable ExportResult1(string fileNmae)
        {
            DataTable dt = new DataTable(fileNmae);
            for (int i = 1; i < 6; i++)
            {
                dt.Columns.Add("颜色" + i, System.Type.GetType("System.String"));
                dt.Columns.Add("数量" + i, System.Type.GetType("System.String"));
            }

            int row = lv_result.Items.Count % 5 == 0 ? lv_result.Items.Count / 5 : lv_result.Items.Count / 5 + 1;
            for (int r = 0; r < row; r++)
            {
                DataRow dr = dt.NewRow();
                for (int c = 0; c < 5; c++)
                {
                    if ((r * 5 + c) >= lv_result.Items.Count)
                        break;

                    ListViewItem item = lv_result.Items[r * 5 + c];
                    dr[2 * c] = string.IsNullOrEmpty(item.SubItems[1].Text) ?
                        item.SubItems[1].Text : item.SubItems[1].Text.Substring(0, 2);
                    dr[2 * c + 1] = item.SubItems[2].Text;
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }

        private DataTable ExportResult2(string fileNmae)
        {
            DataTable dt = new DataTable(fileNmae);
            for (int i = 1; i < 6; i++)
            {
                dt.Columns.Add("部位" + i, System.Type.GetType("System.String"));
                dt.Columns.Add("数量" + i, System.Type.GetType("System.String"));
            }

            int row = (lv_result2.Items.Count - 2) % 5 == 0 ?
                (lv_result2.Items.Count - 2) / 5 : (lv_result2.Items.Count - 2) / 5 + 1;
            int exportNum = 0;
            for (int r = row - 1; r >= 0; r--)
            {
                DataRow dr = dt.NewRow();
                for (int c = 4; c >= 0; c--)
                {
                    if (exportNum >= lv_result2.Items.Count - 2)
                        break;
                    GLItem item = lv_result2.Items[r * 5 + c];
                    dr[2 * (4 - c)] = item.SubItems[1].Text;
                    dr[2 * (4 - c) + 1] = item.SubItems[2].Text;

                    exportNum++;
                }
                dt.Rows.Add(dr);
            }

            DataRow endDr = dt.NewRow();
            endDr[0] = lv_result2.Items[lv_result2.Items.Count - 1].SubItems[1].Text;
            endDr[1] = lv_result2.Items[lv_result2.Items.Count - 1].SubItems[2].Text;
            dt.Rows.Add(endDr);

            return dt;
        }
        private void tb_Row_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckImputNumber(e);
        }

        private void tb_Col_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckImputNumber(e);
        }

        private void CheckImputNumber(KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
                MessageBox.Show("请输入数字！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //else
            //    e.Handled = true;
        }

        private void Main_Form_Shown(object sender, EventArgs e)
        {
            //TD.Abort();
            //foreach (List<Pearl> list in pearls.PearlListSet)
            //    foreach (Pearl pl in list)
            //        pl.btn.Click += btn_Click;
        }

        private void labelColor_Click(object sender, EventArgs e)
        {
            Label lb = (Label)sender;
            RadioButton rdBtn = (RadioButton)(gb_color.Controls.Find(lb.Tag.ToString(), false)[0]);
            rdBtn.Checked = true;
            rdBtn_Click(rdBtn, null);
            //lb.FlatStyle = FlatStyle.Flat;
            //rdBtn.FlatStyle = FlatStyle.Flat;
            //rdBtn.FlatAppearance.BorderColor = Color.Red;
            //rdBtn.FlatAppearance.BorderSize = 2;
        }

        private void lv_result_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 生成图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_exportPic_Click(object sender, EventArgs e)
        {
            string fileNmae = "";
            if (string.IsNullOrEmpty(cb_sampleList.Text))
                fileNmae = "模板图片_" + DateTime.Now.ToString("yy年MM月dd日HH时mm分ss秒") + ".png";
            else
                fileNmae = cb_sampleList.Text + ".png";

            Utility.ExportToPicture(pbBackGround, fileNmae);

            System.Diagnostics.Process.Start(fileNmae);
        }

        bool goRight = true;
        // 滚动广告条
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (lb_ad.Location.X <= pl_main3.Location.X ||
                ((lb_ad.Location.X < (pl_main3.Width - lb_ad.Width)) && goRight))
            {
                lb_ad.Location = new Point(lb_ad.Location.X + 5, pl_main3.Location.Y + pl_main3.Height - 40);
                goRight = true;
            }
            if (lb_ad.Location.X >= pl_main3.Width - lb_ad.Width ||
                (lb_ad.Location.X > pl_main3.Location.X) && !goRight)
            {
                lb_ad.Location = new Point(lb_ad.Location.X - 5, pl_main3.Location.Y + pl_main3.Height - 40);
                goRight = false;
            }
        }

        private void chb_ad_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Enabled = chb_ad.Checked;
        }

        /// <summary>
        /// 给按钮添加提示信息
        /// </summary>
        private void SetToolTips()
        {
            toolTip1.SetToolTip(btn_save, "保存当前图案为模板");
            toolTip1.SetToolTip(btn_close, "退出软件");
            toolTip1.SetToolTip(btn_delete, "删除模板");
            toolTip1.SetToolTip(btn_export, "导出文本");
            toolTip1.SetToolTip(btn_exportPic, "生成图片");
            toolTip1.SetToolTip(btn_bigBag, "大包(31X23)");
            toolTip1.SetToolTip(btn_midBag, "小包(15X20)");
            toolTip1.SetToolTip(btn_createmBag, "生成指定大小的包");
        }

    }

}
