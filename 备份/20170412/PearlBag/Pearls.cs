using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace PearlBag
{
    class Pearls
    {
        public Pearls() { }

        public Pearls(int _firstRowNum, int _allRowsNum)
        {
            CommonPearls(_firstRowNum, _allRowsNum);
        }

        public Pearls(int _firstRowNum, int _allRowsNum, Size _size)
        {
            CommonPearls(_firstRowNum, _allRowsNum);

            this._peralSize = _size;
        }

        private void CommonPearls(int firstRowNum, int allRowsNum)
        {
            _colNum = firstRowNum;
            _rowNum = allRowsNum;

            PearlListSet = new List<List<Pearl>>();
        }

        public List<List<Pearl>> PearlListSet;

        public void CreatePearls(int col, int row, Color color1, Color color2, Size size)
        {
            // 清空现有珠子列表
            if (PearlListSet == null)
                PearlListSet = new List<List<Pearl>>();
            else
                this.Clear();

            this._colNum = col;
            this._rowNum = row;

            // 计算全部珠子数量
            int allPearlsNum = _colNum * _rowNum;

            int pearlIndex = 0;
            for (int r = 0; r < row; r++)
            {
                List<Pearl> list = new List<Pearl>();
                this.PearlListSet.Add(list);
                for (int c = 0; c < col; c++)
                {
                    if (Utility.IsOdd(r + 1))
                        pearlIndex = r * col + c + 1;
                    else
                        pearlIndex = r * col + col - c;

                    Pearl pearl = new Pearl(size, pearlIndex, color2);
                    pearl.SetPosition(r + 1, c + 1);
                    this.Add(pearl, r);
                }
            }

            this._bgColorCanvas = color1;
        }

        public void RefreshPearls(int colNum, int rowNum, Color color, CalculateType type)
        {
            if (type == CalculateType.dot)
            {
                Pearl pl = PearlListSet[rowNum - 1][colNum - 1];
                //pl.IsClick = true;
                pl.PeralColor = color;
                return;
            }

            this._lastRowNum = _rowNum;
            this._lastColNum = _colNum;
            this._colNum = colNum;
            this._rowNum = rowNum;

            if (colNum == 0 || rowNum == 0)
                DisableAllPearls();

            _CountEnable = 0;

            if (type == CalculateType.all)
                foreach (List<Pearl> list in this.PearlListSet)
                    foreach (Pearl pl in list)
                    {
                        if (pl.RowNO <= rowNum && pl.ColumnNO <= colNum)
                            this.EnablePearl(pl, color);
                        else
                            this.DisablePearl(pl);
                        //pl.IsClick = false;
                    }

            if (type == CalculateType.backGround)
                for (int r = 0; r < rowNum; r++)
                {
                    for (int c = 0; c < colNum; c++)
                    {
                        Pearl pl = PearlListSet[r][c];
                        if (pl.PeralColor.Name.Equals(_lastBgColorPearls))
                            pl.PeralColor = color;

                        if (Utility.IsOdd(r + 1))
                            pl.Index = r * colNum + c + 1;
                        else
                            pl.Index = r * colNum + colNum - c;
                    }
                }

            for (int r = 0; r < rowNum; r++)
            {
                for (int c = 0; c < colNum; c++)
                {
                    Pearl pl = PearlListSet[r][c];
                    if (Utility.IsOdd(r + 1))
                        pl.Index = r * colNum + c + 1;
                    else
                        pl.Index = r * colNum + colNum - c;
                }
            }
        }

        private void DisableAllPearls()
        {
            foreach (List<Pearl> list in PearlListSet)
                foreach (Pearl pearl in list)
                    pearl.IsEnable = false;
        }

        public void Add(Pearl pl, int row)
        {
            if (PearlSize.Width == 0 && PearlSize.Height == 0)
                _peralSize = pl.PearlSize;

            PearlListSet[row].Add(pl);
            _count++;

            if (pl.IsEnable)
                _CountEnable++;
        }

        public Pearl GetPearlByRowCol(int row, int col)
        {
            if (PearlListSet == null || PearlListSet.Count == 0)
                return null;

            return PearlListSet[row - 1][col - 1];
        }

        public void EnablePearl(Pearl pl, Color color)
        {
            _CountEnable++;

            pl.Enable(color);
        }

        public void DisablePearl(Pearl pl)
        {
            _CountEnable--;
            pl.Disable();
        }

        public void Clear()
        {
            PearlListSet.Clear();

            _lastColNum = -1;
            _lastRowNum = -1;

            _rowNum = 0;
            _colNum = 0;
        }

        private Size _peralSize;
        public Size PearlSize
        {
            get { return _peralSize; }
        }

        private int _lastColNum = -1;
        public int LastFirstRowNum
        {
            get { return _lastColNum; }
        }

        private int _lastRowNum = -1;
        public int LastAllRowsNum
        {
            get { return _lastRowNum; }
        }

        private int _colNum = 0;
        public int ColumnNum
        {
            get { return _colNum; }
        }

        private int _rowNum = 0;
        public int RowNum
        {
            get { return _rowNum; }
        }

        private int _CountEnable = 0;
        public int CountEnable
        {
            get { return _CountEnable; }
        }

        private int _count = 0;
        public int Count
        {
            get { return _count; }
        }

        private Color _bgColorCanvas = Color.SaddleBrown;
        public Color BgColorCanvas
        {
            get { return _bgColorCanvas; }
            set { _bgColorCanvas = value; }
        }

        private Color _bgColorPearls = Color.DodgerBlue;
        public Color BgColorPearls
        {
            get { return _bgColorPearls; }
            set
            {
                _lastBgColorPearls = _bgColorPearls.Name;
                _bgColorPearls = value;
            }
        }

        private string _lastBgColorPearls;
    }
}
