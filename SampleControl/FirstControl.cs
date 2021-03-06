﻿using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace SampleControl
{
    public class FirstControl:Control
    {
        public FirstControl()
        { 
        }

        private ContentAlignment alignmentValue = ContentAlignment.MiddleLeft;
        private Color _textColor = Color.Blue;

        [DefaultValue(typeof(Color),"Blue")]
        [Category("颜色"),Description("字体颜色")]
        public Color TextColor
        {
            get { return _textColor; }
            set { _textColor = value; Invalidate(); }
        }
        

        [Category("Alignment"), Description("指定内容的对齐方式")]
        
        public ContentAlignment TextAlignment
        {
            get 
            {
                return alignmentValue;
            }
            set 
            {
                alignmentValue = value;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            StringFormat style = new StringFormat();
            switch (alignmentValue)
            {
                case ContentAlignment.MiddleLeft:
                    style.Alignment = StringAlignment.Near;
                    break;
                case ContentAlignment.MiddleRight:
                    style.Alignment = StringAlignment.Far;
                    break;
                case ContentAlignment.MiddleCenter:
                    style.Alignment = StringAlignment.Center;
                    break;
            }
            
            e.Graphics.DrawString(Text, Font, new SolidBrush(_textColor), ClientRectangle, style);

        }

    }
}
