using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestPingtu;

namespace PearlBag
{
    class Pearl
    {
        public ExPictureBox btn
        {
            set;
            get;
        }

        private Size _pearlSize;
        public Size PearlSize
        {
            set { _pearlSize = value; }
            get { return _pearlSize; }
        }

        public Pearl(int index)
        {
            btn = new ExPictureBox(_peralColor, false, _pearlSize);
            btn.Cursor = System.Windows.Forms.Cursors.Hand;

            this.Index = index;
        }

        public Pearl(Size size, int index)
        {
            btn = new ExPictureBox(_peralColor, false, size);
            btn.Cursor = System.Windows.Forms.Cursors.Hand;

            this.Index = index;
            _pearlSize = size;
        }

        public Pearl(Size size, int index, Color color)
        {
            btn = new ExPictureBox(color, false, size);
            btn.Cursor = System.Windows.Forms.Cursors.Hand;

            this.Index = index;
            this.PeralColor = color;
            _pearlSize = size;
        }

        public void SetPosition(int row, int col)
        {
            _rowNO = row;
            _columnNO = col;
            btn.Tag = _rowNO.ToString() + "|" + _columnNO.ToString();
        }

        public Pearl(Size size, int index, Color color, int row, int col)
        {
            btn = new ExPictureBox(color, false, size);
            btn.Tag = _rowNO.ToString() + "|" + _columnNO.ToString();
            btn.Cursor = System.Windows.Forms.Cursors.Hand;

            this.Index = index;
            this.PeralColor = color;
            _pearlSize = size;

            this._rowNO = row;
            this._columnNO = col;
        }

        private Color _peralColor = Color.DodgerBlue;
        public Color PeralColor
        {
            get { return _peralColor; }
            set
            {
                _peralColor = value;
                btn.BackColor = _peralColor;

                ColorName = ColorUtility.GetColorText(_peralColor.Name);
                ColorValue = _peralColor.Name;
            }
        }

        public string ColorName
        {
            get;
            set;
        }

        public string ColorValue
        {
            get;
            set;
        }

        public int Index
        {
            get;
            set;
        }

        private int _rowNO = -1;
        public int RowNO
        {
            get { return _rowNO; }
            set { _rowNO = value; }
        }

        private int _columnNO = -1;
        public int ColumnNO
        {
            get { return _columnNO; }
            set { _columnNO = value; }
        }

        public Pearl Copy()
        {
            Pearl copy = new Pearl(_pearlSize, this.Index, _peralColor);
            copy.btn.Tag = this.Index;
            return copy;
        }

        //private bool _isClick = false;
        //public bool IsClick
        //{
        //    get { return _isClick; }
        //    set { _isClick = value; }
        //}

        private bool _isEnable = true;
        public bool IsEnable
        {
            get { return _isEnable; }
            set { _isEnable = value; }
        }

        public void Enable(Color color)
        {
            _isEnable = true;
            PeralColor = color;
            btn.Cursor = Cursors.Hand;
        }

        public void Disable()
        {
            _isEnable = false;
            PeralColor = ColorUtility.Invisible();
            btn.Cursor = Cursors.Arrow;
        }
    }

}
