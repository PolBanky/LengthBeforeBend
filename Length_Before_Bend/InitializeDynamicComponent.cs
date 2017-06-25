//#define __A__

using System;
using System.Drawing;
using System.Windows.Forms;
using WinLib;               // using WinLib !!!!!!!!!!!!!

namespace Length_Before_Bend
{
    public partial class aForm1 : aForm
    {   
        #region  Ссылки на графические объекты
        static int thinLine = 1;
        static int thickLine = 3;
        static float dimExt = 5.0F;
        static float dimLength = 40.0F;
        static float dimLength1 = 50.0F;
        Graphics g;     // Ссылка на графический объект
        //***********
        Pen blackSolidPen = new Pen(Color.Black, thinLine);
        Pen whiteSolidPen = new Pen(Color.White, thickLine);
        Pen blueSolidPen =  new Pen(Color.Blue,  thickLine);
        Pen redDashedPen =  new Pen(Color.Red,   thinLine);
            // Create font and brush for text
        Font txtFont = new Font("Arial", 14);
        SolidBrush txtBrush = new SolidBrush(Color.Black);
            //*****
        PointF p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12;
        PointF p11x, p12x, p11y, p12y, p21y, p22y, p31y, p32y, p51x, p52x, p91y, p92y, p61y, p62y;
        // Create point for upper-left corner of drawing.
        PointF txtP1, txtP2, txtP3, txtP4;
        RectangleF r1, r2, r3, r4;  // для дуг
        #endregion  Ссылки на графические объекты

        #region  Объявление ссылок на контролы (чтоб видны были издаля)

        PictureBox pict_01;
        TxtBoxDbl TBox_1, TBox_2, TBox_3, TBox_4;               // Окна для ввода размеров сечения  //#define TBox_1 H;
        LabelR lbl_TBox_1, lbl_TBox_2, lbl_TBox_3, lbl_TBox_4;  // Каменты к окнам ввода
        Button_72 button_RUN /*, button_Choice*/;               // Кнопка
        aLabel Result_1;

        #endregion  Объявление ссылок на контролы (чтоб видны были издаля)

        // InitializeDynamicComponent()
        void InitializeDynamicComponent()
        {
            int gap = 5 /*, gap1 = 1*/;    // величина промежутка между контролами
            redDashedPen.DashPattern = new float[] { 4.0F, 2.0F, 1.0F, 3.0F };

            #region PictureBoxes
            //
            // pict_01
            //
            pict_01 = new PictureBox();
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            //bool chk;
            //chk = DoubleBuffered;
            //MessageBox.Show("DoubleBuffered = " + chk.ToString());
            pict_01.Top = gap;          // т.е. коорд Y == 5
            pict_01.Left = gap;         // т.е. коорд X == 5
            pict_01.BorderStyle = BorderStyle.Fixed3D;
            //pict_01.BackColor = Color.WhiteSmoke;
            pict_01.BackColor = Color.White;
            pict_01.Height = 385;
            pict_01.Width = 281;
            pict_01.TabStop = false;
            this.pict_01.Paint += new PaintEventHandler(pict_01_Paint);         // pict_01.Paint !!!!!
            pict_01.MouseClick += new MouseEventHandler(pict_01_MouseClick);    // определение обработчика события
            #endregion PictureBoxes
            
            #region TxtBoxDbls and their's labels
            //
            // TBox_1
            //
            TBox_1 = new TxtBoxDbl(pict_01);  // т.е. ставится к картинке снизу
            TBox_1.name1 = "H, mm";
            TBox_1.name2 = "высота стенки профиля";
            TBox_1.TxtChanged += new EventHandler(TBox_1_TxtChanged);
            // lbl_TBox_1
            lbl_TBox_1 = new LabelR(TBox_1, TBox_1.name1);
            //
            // TBox_2
            //
            TBox_2 = new TxtBoxDbl(TBox_1);
            TBox_2.name1 = "L, mm";
            TBox_2.name2 = "длина полки профиля";
            TBox_2.TxtChanged += new EventHandler(TBox_1_TxtChanged);
            // lbl_TBox_2
            lbl_TBox_2 = new LabelR(TBox_2, TBox_2.name1);
            //
            // TBox_3
            //
            TBox_3 = new TxtBoxDbl(TBox_2);
            TBox_3.name1 = "S, mm";
            TBox_3.name2 = "толщина листа профиля";
            TBox_3.TxtChanged += new EventHandler(TBox_1_TxtChanged);
            // lbl_TBox_3
            lbl_TBox_3 = new LabelR(TBox_3, TBox_3.name1);
            //
            // TBox_4
            //
            TBox_4 = new TxtBoxDbl(TBox_3);
            TBox_4.name1 = "R, mm";
            TBox_4.name2 = "радиус изгиба профиля";
            TBox_4.TxtChanged += new EventHandler(TBox_1_TxtChanged);
            // lbl_TBox_3
            lbl_TBox_4 = new LabelR(TBox_4, TBox_4.name1);
            #endregion  // TxtBoxDbls and their's labels
            
            #region Buttons
            //
            // button_RUN
            //
            button_RUN = new Button_72(lbl_TBox_1, Place.right);
            button_RUN.Image = WinLib.Properties.Resources.Edit;
            //button_RUN.TabIndex = 5;
            button_RUN.Click +=new EventHandler(button_RUN_Click);
            #endregion
            
            #region aLabel Result
            //
            // Result_1
            //
            Result_1 = new aLabel(TBox_4, Place.bottom, 280, 144);
            Result_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            //TxtBoxDbl.showErr = Result_1;   // т.е. сюда выводятся сообщения об ошибках
            TBox_1.showErr = Result_1;
            TBox_2.showErr = Result_1;
            TBox_3.showErr = Result_1;
            TBox_4.showErr = Result_1;

            #endregion aLabel Result

            #region ToolTips
            //
            // Tip1
            //
            Tip1 = new ToolTip();
            Tip1.InitialDelay = 500;
            Tip1.ToolTipIcon = ToolTipIcon.Info;
            Tip1.ToolTipTitle = "Just Do It !";
            Tip1.SetToolTip(this.pict_01, "Click LEFT button\nto change profile.");
            Tip1.IsBalloon = true;
            //
            // Tip2
            //
            Tip2 = new ToolTip();
            Tip2.InitialDelay = 500;
            Tip2.ToolTipIcon = ToolTipIcon.Info;
            Tip2.ToolTipTitle = "Just Do It !";
            Tip2.SetToolTip(this.button_RUN, "Click LEFT button to calculate.");
            Tip2.IsBalloon = true;
            //
            // HelpProvider
            //
            hlP1 = new HelpProvider();
            hlP1.SetShowHelp(this, true);
            hlpFile = this.Text + ".txt";
           // MessageBox.Show(hlpFile);
            hlP1.HelpNamespace = hlpFile;

            #endregion ToolTips

            #region Add Controls to Form
            //
            // Add Controls to Form1
            //
            this.Controls.Add(pict_01);
            this.Controls.Add(TBox_1);
            this.Controls.Add(lbl_TBox_1);
            this.Controls.Add(TBox_2);
            this.Controls.Add(lbl_TBox_2);
            this.Controls.Add(TBox_3);
            this.Controls.Add(lbl_TBox_3);
            this.Controls.Add(TBox_4);
            this.Controls.Add(lbl_TBox_4);
            this.Controls.Add(button_RUN);
            this.Controls.Add(Result_1);
            #endregion  // Add Controls to Form
            
            //****************
            refInit();  // создание пойнтов по ранее объявленным ссылкам 
        }   // end of - void InitializeDynamicComponent()


        // !!!!!__EVENTS__!!!!!

        // TBox_1_TxtChanged - !!!  это не стандартное TextChanged  !!!
        void TBox_1_TxtChanged(object sender, EventArgs e) // работает для всех TBox_ а не только TBox_1 !!!!
        {
            //TxtBoxDbl a = (TxtBoxDbl)sender;
            if (Result_1.BackColor == System.Drawing.Color.LightPink)
            {
                Result_1.BackColor = System.Drawing.SystemColors.Info;
            }   // end of - if
            if (Result_1.Text != "")
            Result_1.Text = "";
        }   // end of - TBox_1_TxtChanged


        // pict_01_MouseClick - т.е. клик на картинке !!! происходит замена картинки
        void pict_01_MouseClick(object sender, MouseEventArgs e)
        {
            g = pict_01.CreateGraphics();
            g.Clear(Color.White);
            if (profile == Profile.angle)    //if(num==1)
                draw_Channel_Iron(ref g);
            else
                draw_Angle_Iron(ref g); //pict_01_MouseClick(
            //***
            if (Result_1.BackColor == System.Drawing.Color.LightPink)
                Result_1.BackColor = System.Drawing.SystemColors.Info;
            Result_1.Text = "";
        }   // end of - void InitializeDynamicComponent()


        // button_RUN_Click - клик по кнопке RUN  !!!!!!!!!!!!!! 
        void button_RUN_Click(object sender, EventArgs e)
        {   
            // Вначале проверка правильности данных в полях ввода !!!
            //*****  1 
            if (TBox_1.Val == 0.0) return;
            //*****  2 
            //if (TBox_2.Val == 0.0) return;
            if (!TBox_2.Val_bool) return;
            //*****  3 
            if (TBox_3.Val == 0.0) return;
            else // если толщина листа больше нуля !!!
            {
                // по стенке (пока без радиуса)
                if (H < H_not_direct)
                {
                    warnShow(ref TBox_3, err + warnings[warnIndex]);    // "Error !\nH < 2 * S"
                    return;
                }
                // по полке (пока без радиуса)
                if (L < L_not_direct)
                {
                    warnShow(ref TBox_3, err + warnings[warnIndex]);
                    return;
                }            
            }   // end of - else
            //*****  4 
            if (TBox_4.Val == 0.0) return;
            else // если радиус больше нуля !
            {
                // далее расчет по средней линии листа !
                // по стенке
                if (H_mid < H_mid_not_direct)
                {
                    warnShow(ref TBox_4, err + warnings[warnIndex]); // "Error !\nH < 2*S + 2*R"
                    return;
                }
                // по полке
                if (L_mid < L_mid_not_direct)
                {
                    warnShow(ref TBox_4, err + warnings[warnIndex]);
                    return;
                }
                // ну уж если намана все... RESULT
                if (Result_1.BackColor == System.Drawing.Color.LightPink)            
                Result_1.BackColor = System.Drawing.SystemColors.Info;
                Result_1.setText("Длина развертки\n= ", GeneralLength, " mm\n",
                    "от края до линии сгиба\n= ", edge_bendLine, " mm");
            }       // else
        }           // end of - button_RUN_Click
        

        void pict_01_Paint(object sender, PaintEventArgs e)
        {
#if __A__
            MessageBox.Show("Inside pict_01_Paint(object sender, PaintEventArgs e)");
#endif
            g = e.Graphics;
            //*****
            if (profile == Profile.angle)    //if(num==1)
                draw_Angle_Iron(ref g);
            else
                draw_Channel_Iron(ref g);
#if __A__
            MessageBox.Show("Inside - after drawing");
#endif      //MessageBox.Show("Now clear");
        }   // pict_01_Paint

    }       // class Form1 : aForm
}           // namespace Length_Before_Bend