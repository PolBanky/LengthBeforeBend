//#define __A__

using System;
using System.Drawing;
using System.Windows.Forms;
using WinLib;               // using WinLib !!!!!!!!!!!!!

namespace Length_Before_Bend
{
    public partial class aForm1 : aForm
    {
        // Инициализация пойнтов
        void refInit()
        {
            p1 = new PointF(200.0F, 300.0F);  // При изменении координат сдвигается вся фигура
            p2 = new PointF(p1.X - 96.0F, p1.Y);
            p3 = new PointF(p1.X, p1.Y - 12.0F);
            p4 = new PointF(p2.X, p3.Y);
            p5 = new PointF(p1.X - 120.0F, p1.Y - 24.0F);
            p6 = new PointF(p5.X, p5.Y - 192.0F);
            p7 = new PointF(p5.X + 12.0F, p5.Y);
            p8 = new PointF(p7.X, p6.Y);
            p9 = new PointF(p2.X, p1.Y - 240.0F);
            p10 = new PointF(p1.X, p9.Y);
            p11 = new PointF(p2.X, p2.Y - 228.0F);
            p12 = new PointF(p1.X, p11.Y);
            //*
            // PointF p11x, p12x, p11y, p12y, p21y, p22y, p31y, p32y, p51x, p52x, p91y, p92y;
            p11x = new PointF(p1.X, p1.Y + dimLength);
            p51x = new PointF(p5.X, p1.Y + dimLength);
            p12x = new PointF(p1.X, p1.Y + dimLength - dimExt);
            p52x = new PointF(p5.X, p1.Y + dimLength - dimExt);

            p21y = new PointF(p2.X - dimLength1, p2.Y);
            p91y = new PointF(p9.X - dimLength1, p9.Y);
            p22y = new PointF(p21y.X + dimExt, p2.Y);
            p92y = new PointF(p91y.X + dimExt, p9.Y);
            //*
            p61y = new PointF(p91y.X, p6.Y);
            p62y = new PointF(p92y.X, p6.Y);
            //*
            p11y = new PointF(p1.X + dimLength, p1.Y);
            p31y = new PointF(p3.X + dimLength, p3.Y);
            p12y = new PointF(p11y.X - dimExt, p2.Y);
            p32y = new PointF(p31y.X - dimExt, p3.Y - 20.0F);

            //*
            txtP1 = new PointF(p1.X - 95.0F,  p1.Y - 42.0F);  // "R"
            txtP2 = new PointF(p1.X - 70.0F,  p1.Y + 12.0F);  // "L"
            txtP3 = new PointF(p1.X - 162.0F, p1.Y - 130.0F); // "H"
            txtP4 = new PointF(p1.X + 15.0F,  p1.Y - 35.0F);  // "S"

            //*** ректанглы для определения положения дуг
            r1 = new RectangleF(p5.X, p5.Y - 24.0F, 48.0F, 48.0F);  // координаты верхне-левого угла и ширина и высота (которая вниз)
            r2 = new RectangleF(p7.X, p7.Y - 12.0F, 24.0F, 24.0F);
            r3 = new RectangleF(p6.X, p9.Y, 48.0F, 48.0F);
            r4 = new RectangleF(p8.X, p11.Y, 24.0F, 24.0F);
        }   // end of - refInit()


        // Непосредственное рисование !!!!
        void draw_Channel_Iron(ref Graphics a)
        {
            profile = Profile.channel;
            a.DrawLine(blueSolidPen, p1, p2);
            a.DrawLine(blueSolidPen, p1, p3);
            a.DrawLine(blueSolidPen, p3, p4);
            a.DrawLine(blueSolidPen, p5, p6);
            a.DrawLine(blueSolidPen, p7, p8);
            a.DrawLine(blueSolidPen, p9, p10);
            a.DrawLine(blueSolidPen, p10, p12);
            a.DrawLine(blueSolidPen, p12, p11);
            //***
            // PointF p11x, p12x, p11y, p12y, p21y, p22y, p31y, p32y, p51x, p52x, p91y, p92y;
            a.DrawLine(blackSolidPen, p1, p11x);
            a.DrawLine(blackSolidPen, p5, p51x);
            a.DrawLine(blackSolidPen, p12x, p52x);

            a.DrawLine(blackSolidPen, p2, p21y);
            a.DrawLine(blackSolidPen, p9, p91y);
            a.DrawLine(blackSolidPen, p22y, p92y);

            a.DrawLine(blackSolidPen, p1, p11y);
            a.DrawLine(blackSolidPen, p3, p31y);
            a.DrawLine(blackSolidPen, p12y, p32y);

            //a.DrawRectangle(redDashedPen, r1.X, r1.Y, r1.Width, r1.Height);
            a.DrawArc(blueSolidPen, r1, 90.0F, 90.0F);
            a.DrawArc(blueSolidPen, r2, 90.0F, 90.0F);
            a.DrawArc(blueSolidPen, r3, 180.0F, 90.0F);
            a.DrawArc(blueSolidPen, r4, 180.0F, 90.0F);
            //***
            a.DrawString("R", txtFont, txtBrush, txtP1);
            a.DrawString("L", txtFont, txtBrush, txtP2);
            a.DrawString("H", txtFont, txtBrush, txtP3);
            a.DrawString("S", txtFont, txtBrush, txtP4);
        }   // draw_Channel_Iron(ref Graphics a)


        void draw_Angle_Iron(ref Graphics a)
        {
            profile = Profile.angle;
            a.DrawLine(blueSolidPen, p1, p2);
            a.DrawLine(blueSolidPen, p1, p3);
            a.DrawLine(blueSolidPen, p3, p4);
            a.DrawLine(blueSolidPen, p5, p6);
            a.DrawLine(blueSolidPen, p7, p8);
            a.DrawLine(blueSolidPen, p6, p8);
            //a.DrawLine(blueSolidPen, p9, p10);
            //a.DrawLine(blueSolidPen, p10, p12);
            //a.DrawLine(blueSolidPen, p12, p11);
            //***
            // PointF p11x, p12x, p11y, p12y, p21y, p22y, p31y, p32y, p51x, p52x, p91y, p92y;
            a.DrawLine(blackSolidPen, p1, p11x);
            a.DrawLine(blackSolidPen, p5, p51x);
            a.DrawLine(blackSolidPen, p12x, p52x);

            a.DrawLine(blackSolidPen, p2, p21y);
            a.DrawLine(blackSolidPen, p6, p61y);
            a.DrawLine(blackSolidPen, p22y, p62y);

            a.DrawLine(blackSolidPen, p1, p11y);
            a.DrawLine(blackSolidPen, p3, p31y);
            a.DrawLine(blackSolidPen, p12y, p32y);

            //a.DrawRectangle(redDashedPen, r1.X, r1.Y, r1.Width, r1.Height);
            a.DrawArc(blueSolidPen, r1, 90.0F, 90.0F);
            a.DrawArc(blueSolidPen, r2, 90.0F, 90.0F);
            //a.DrawArc(blueSolidPen, r3, 180.0F, 90.0F);
            //a.DrawArc(blueSolidPen, r4, 180.0F, 90.0F);
            //***
            a.DrawString("R", txtFont, txtBrush, txtP1);
            a.DrawString("L", txtFont, txtBrush, txtP2);
            a.DrawString("H", txtFont, txtBrush, txtP3);
            a.DrawString("S", txtFont, txtBrush, txtP4);
        }   // draw_Channel_Iron(ref Graphics a)


        void warnShow(ref TxtBoxDbl a, string msg)
        {
            Result_1.BackColor = System.Drawing.Color.LightPink;
            Result_1.Text = msg;
            a.Focus();
        }   // end of - warnShow()

    }   // class Form1 : aForm
}       // namespace Length_Before_Bend