﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SampleControl
{
    public partial class ThermoMeterControl : UserControl
    {
        /// <summary>
        /// 初始化控件
        /// 预设绘图方式：双缓冲，支持透明背景色，自定义绘制
        /// </summary>
        public ThermoMeterControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.Selectable, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);


            InitializeComponent();
        }

        //温度
        private float temperature = 0;
        [Category("温度"), Description("当前温度")]
        public float Temperature
        {
            set { temperature = value; }
            get { return temperature; }
        }

        //最高温度  
        private float highTemperature = 50;
        [Category("温度"), Description("最高温度")]
        public float HighTemperature 
        {
            set { highTemperature = value; }
            get { return highTemperature; }
        }

        //最低温度
        private float lowTemperature = -20;
        [Category("温度"), Description("最低温度")]
        public float LowTemperature
        {
            set { lowTemperature = value; }
            get { return lowTemperature; }
        }

        //当前温度值得字体
        private Font tempFont = new Font("宋体",12);
        [Category("温度"), Description("当前温度数值的字体")]
        public Font TempFont
        {
            get { return tempFont; }
            set { tempFont = value; }
        }

        //当前温度数值的颜色
        private Color tempColor = Color.Black;
        [Category("温度"), Description("当前温度数值的颜色")]
        public Color TempColor
        {
            set { tempColor = value; }
            get { return tempColor; }
        }

        //大刻度线数量
        private int bigScale = 5;
        [Category("刻度"), Description("大刻度线数量")]
        public int BigScale
        {
            set { bigScale = value; }
            get { return bigScale; }
        }

        //小刻度线数量
        private int smallScale = 5;
        [Category("刻度"), Description("小刻度线数量")]
        public int SmallScale 
        {
            set { smallScale = value; }
            get { return smallScale; }
        }

        //刻度字体
        private Font drawFont = new Font("Aril", 9);
        [Category("刻度"), Description("刻度数字的字体")]
        public Font DrawFont
        {
            get { return drawFont; }
            set { drawFont = value; }

        }

        //刻度字体颜色
        private Color drawColor = Color.Black;
        [Category("刻度"), Description("刻度数字颜色")]
        public Color DrawColor
        {
            get { return drawColor; }
            set { drawColor = value; }
        }

        //刻度盘最外圈线条的颜色
        private Color dialOutLineColor = Color.Gray;
        [Category("背景"), Description("刻度盘最外圈线条的颜色")]
        public Color DialOutLineColor
        {
            get { return dialOutLineColor; }
            set { dialOutLineColor = value; }
        }

        //刻度盘背景颜色
        private Color dialBackColor = Color.Gray;
        [Category("背景"), Description("刻度盘背景颜色")]
        public Color DialBackColor
        {
            get { return dialBackColor; }
            set { dialBackColor = value; }
        }


        //大刻度线颜色
        private Color bigScaleColor = Color.Black;
        [Category("刻度"), Description("大刻度线颜色")]
        public Color BigScaleColor
        {
            get { return bigScaleColor; }
            set { bigScaleColor = value; }
        }

        //小刻度线颜色
        private Color smallScaleColor = Color.Black;
        [Category("刻度"), Description("小刻度线颜色")]
        public Color SmallScaleColor
        {
            get { return smallScaleColor; }
            set { smallScaleColor = value; }


        }

        //温度柱背景颜色
        private Color mercuryBackColor = Color.LightGray;
        [Category("刻度"), Description("温度柱背景颜色")]
        public Color MercuryBackColor
        {
            get { return mercuryBackColor; }
            set { mercuryBackColor = value; }

        }

        //温度柱颜色
        private Color mercuryColor = Color.Red;
        [Category("刻度"), Description("温度柱颜色")]
        public Color MercuryColor
        {
            set { mercuryColor = value; }
            get { return mercuryColor; }
        }




        /// <summary>
        /// 变量
        /// </summary>
        private float X;
        private float Y;
        private float H;
        private Pen p, s_p;
        private Brush b;

        protected override void OnPaint(PaintEventArgs e)
        {
            ThermometerControl_Paint(this, e);
            base.OnPaint(e);
        }
        private void ThermometerControl_Paint(object sender, PaintEventArgs e)
        {
            if (temperature > highTemperature)
            {
                temperature = highTemperature;
            }
            if (temperature < lowTemperature)
            {
                temperature = lowTemperature;
            }

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.TranslateTransform(2, 2);

            X = this.Width - 4;
            Y = this.Height - 4;

            //绘制边框（最外边的框）
            p = new Pen(dialOutLineColor, 2);
            e.Graphics.DrawLine(p, 0, X / 2, 0,(Y - X / 2));//第一条竖线
            e.Graphics.DrawLine(p, X, X / 2, X, (Y - X / 2));//第二条竖线
            e.Graphics.DrawArc(p, 0, 0, X, X, 180, 180);//顶部半弧;
            e.Graphics.DrawArc(p,0, Y - X, X, X, 0, 180);//底部半弧；


            //绘制指示柱
            b = new SolidBrush(mercuryBackColor);
            e.Graphics.FillEllipse(b, X * 2 / 5, (X / 2 - X / 10), X / 5, X / 5);
            b = new SolidBrush(mercuryColor);
            e.Graphics.FillEllipse(b, X / 4, Y - X * 9 / 16, X / 2, X / 2);//底部红圆
            e.Graphics.FillRectangle(b, X * 2 / 5, X / 2 + 1, X / 5, Y - X);//红色指示柱

            //在温度计底部，绘制当前温度数值
            b = new SolidBrush(tempColor);
            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Center;
            e.Graphics.DrawString((temperature.ToString() + "℃"), tempFont, b, X / 2, Y - X / 4, format);

            //绘制大刻度线，线宽为2；
            //绘制小刻度线，现况为1；
            //绘制刻度数字，字体，字号，字的颜色在属性中可改
            p = new Pen(bigScaleColor, 2); //设置大刻度线的颜色，线粗
            s_p = new Pen(smallScaleColor, 1);//设置小刻度线的颜色，线粗
            SolidBrush drawBrush = new SolidBrush(drawColor);//设置绘制数字的颜色
            format.Alignment = StringAlignment.Near;// 设置数字水平对齐为中间,垂直对齐为左边
            //计算要绘制数字的数字
            int interval = (int)(highTemperature - lowTemperature) / bigScale;
            int tempNum = (int)highTemperature;
            for(int i = 0;i<=bigScale;++i)
            {
                float b_s_y = X / 2 + i * ((Y - X - X / 2) / bigScale);//绘制大刻度线的垂直位置
                e.Graphics.DrawLine(p, X / 5, b_s_y, X * 2 / 5 - 2, b_s_y);//绘制大刻度线
                e.Graphics.DrawString(tempNum.ToString(),drawFont, drawBrush, X * 3 / 5, b_s_y, format);//绘制刻度数字
                tempNum -= interval;//计算下一次要绘制的数值

                //绘制小刻度线
                if (i < bigScale)
                {
                    for (int j = 1; j < smallScale; j++)
                    {
                        float s_s_y = b_s_y + ((X / 2 + (i + 1) * ((Y - X - X / 2) / bigScale) - b_s_y) / smallScale) * j;
                        e.Graphics.DrawLine(s_p, (X * 3 / 10), s_s_y, (X * 2 / 5 - 2), s_s_y);
                    }
                }
                
            }


            // 计算当前温度的位置            
            float L = Y - X * 3 / 2;
            H = L * (temperature - lowTemperature) / (highTemperature - lowTemperature);
            // 绘制当前温度的位置
            b = new SolidBrush(mercuryBackColor);
            e.Graphics.FillRectangle(b, X * 2 / 5, X / 2, X / 5, (L - H));
 
        }














    }
}
