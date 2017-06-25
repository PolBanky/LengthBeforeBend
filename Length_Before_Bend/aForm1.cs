#define __A__

/// <summary>
/// Программа для расчета длины развертки гнутых уголков и швеллеров
/// <summary>

using System.Windows.Forms;
using WinLib;   // !!!!!!!!!!!!!

namespace Length_Before_Bend
{
    // ОПРЕДЕЛЕНИЕ ПОЛЬЗОВАТЕЛЬСКИХ ТИПОВ ДАННЫХ
    enum Profile { angle, channel };    // какой профиль - уголок и швеллер

    //  !!!!!!!!!!!!!!!!!!!!!!!!!!!  Form1 : aForm  !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    public partial class aForm1 : aForm
    {   
        // *** D A T A  ***
        Profile profile = Profile.channel;

        // ***** ДАННЫЕ С КЛАВИАТУРЫ ***** //

        // СВОЙСТВО H   // ПЕРЕМЕННАЯ НЕ НУЖНА!!!
        double H { get { return TBox_1.val; } }  // Высота стенки профиля - снаружи
        // end of - СВОЙСТВО H
        // СВОЙСТВО L   // ПЕРЕМЕННАЯ НЕ НУЖНА!!!
        double L { get { return TBox_2.val; } }  // Длина полок профиля (одинаковые если две) - снаружи
        // end of - СВОЙСТВО L
        // СВОЙСТВО S   // ПЕРЕМЕННАЯ НЕ НУЖНА!!!
        double S { get { return TBox_3.val; } }  // Толщина листа
        // end of - СВОЙСТВО S
        // СВОЙСТВО R   // ПЕРЕМЕННАЯ НЕ НУЖНА!!!
        double R { get { return TBox_4.val; } }  // Радиус сгиба внутренний
        // end of - СВОЙСТВО R

        // ***** РАСЧЕТ ДАННЫХ ***** //

        // СВОЙСТВО S_half   // ПЕРЕМЕННАЯ НЕ НУЖНА!!!
        double S_half { get { return S / 2.0; } }  // Половина толщины листа
        // end of - СВОЙСТВО S_half
        // СВОЙСТВО R_mid   // ПЕРЕМЕННАЯ НЕ НУЖНА!!!
        double R_mid { get { return R + S_half; } }  // Радиус по средней линии
        // end of - СВОЙСТВО R_mid
        // СВОЙСТВО Arc_mid_Length   // ПЕРЕМЕННАЯ НЕ НУЖНА!!!
        double Arc_mid_Length { get { return Trig.PiDiv4 * 2.0 * R_mid; } }  // Длина дуги ПО СРЕДНЕЙ ЛИНИИ !!! т.е. Arc_mid_Length = Pi * D_mid / 4 
        // end of - СВОЙСТВО Arc_mid_Length
        // СВОЙСТВО Arc_half_mid_Length   // ПЕРЕМЕННАЯ НЕ НУЖНА!!!
        double Arc_half_mid_Length { get { return Arc_mid_Length / 2.0; } }  // Половина длины дуги
        // end of - СВОЙСТВО Arc_half_mid_Length
        // СВОЙСТВО H_mid   // ПЕРЕМЕННАЯ НЕ НУЖНА!!!
        double H_mid { get {
                double a=0.0;
                switch (profile)
                {
                    case Profile.angle:
                        a = H - S_half;
                        warnIndex = 2;
                        break;
                    case Profile.channel:
                        a = H - S;
                        warnIndex = 3;
                        break;
                }   // end of - switch
            return a;
        }   // end of - get
        }  // Высота стенки профиля по средней линии
        // end of - СВОЙСТВО H_mid
        // СВОЙСТВО L_mid   // ПЕРЕМЕННАЯ НЕ НУЖНА!!!
        double L_mid { get { return L - S_half; } }  // Длина полок профиля до средней линии стенки
        // end of - СВОЙСТВО L_mid
        // СВОЙСТВО H_not_direct   // ПЕРЕМЕННАЯ НЕ НУЖНА!!!
        double H_not_direct { get {  // not_direct - не прямая т.е. радиус
                double a = 0.0;
                switch (profile)
                {
                    case Profile.angle:
                        a = S;       // высота толщины листа - пока без радиуса
                        warnIndex = 0;  // для - string[] warnings
                        break;
                    case Profile.channel:
                        a = 2.0 * S; // высота двух толщин листа - пока без радиуса
                        warnIndex = 1;
                        break;
                }   // end of - switch
                return a;
            }   // end of - get
        }  // сумма толщин полок (если две)
        // end of - СВОЙСТВО H_not_direct
        // СВОЙСТВО L_not_direct   // ПЕРЕМЕННАЯ НЕ НУЖНА!!!
        double L_not_direct { get { warnIndex = 4; return S; } }  // толщина стенки
        // end of - СВОЙСТВО L_not_direct
        // СВОЙСТВО H_mid_not_direct   // ПЕРЕМЕННАЯ НЕ НУЖНА!!!
        double H_mid_not_direct { get {
                double a = 0.0;
                switch (profile)
                {
                    case Profile.angle:
                        a = R_mid;    // высота толщины листа + радиус
                        warnIndex = 2;
                        break;
                    case Profile.channel:
                        a = (R_mid * 2.0); // высота двух толщин листа + 2 * радиус
                        warnIndex = 3;
                        break;   
                }   // end of - switch
                return a;
            }   // end of - get
        }  // Высота стенки профиля по средней линии непрямая (радиусы - один или два)
        // end of - СВОЙСТВО H_mid_not_direct
        // СВОЙСТВО L_mid_not_direct   // ПЕРЕМЕННАЯ НЕ НУЖНА!!!
        double L_mid_not_direct { get { warnIndex = 5; return R_mid; } }  // Длина полки профиля непрямая (радиус)
        // end of - СВОЙСТВО L_mid_not_direct
        // СВОЙСТВО H_direct   // ПЕРЕМЕННАЯ НЕ НУЖНА!!!
        double H_direct { get { return H_mid - H_mid_not_direct; } }  // длина прямого участка стенки
        // end of - СВОЙСТВО H_direct
        // СВОЙСТВО L_direct   // ПЕРЕМЕННАЯ НЕ НУЖНА!!!
        double L_direct { get { return L_mid - L_mid_not_direct; } }  // длина прямого участка полки
        // end of - СВОЙСТВО L_direct
        // СВОЙСТВО GeneralLength   // ПЕРЕМЕННАЯ НЕ НУЖНА!!!
        double GeneralLength { get {
            double general_Length = 0.0;
            switch (profile)
            {
                case Profile.angle:
                    general_Length = H_direct + L_direct + Arc_mid_Length;    // высота толщины листа + радиус
                    break;
                case Profile.channel:
                    general_Length = H_direct + (2.0 * L_direct) + (2.0 * Arc_mid_Length); // высота двух толщин листа + 2 * радиус
                    break;
            }   // end of - switch
            return general_Length;
        }    }  // Длина развертки
        // end of - СВОЙСТВО GeneralLength

        // СВОЙСТВО edge_bendLine   // ПЕРЕМЕННАЯ НЕ НУЖНА!!!
        double edge_bendLine { get { return L_direct + Arc_half_mid_Length; } }
        // end of - СВОЙСТВО edge_bendLine

        //***
        string err =  "Error !\n";
        //string err1 = "Error !\ndata in cell - \"";
        //string err2 = "\"\ncan't == 0";

        string[] warnings =
        {
            "H < S",            // 0 
            "H < 2*S",          // 1 
            "H < S+R",          // 2 
            "H < (2*S)+(2*R)",  // 3 
            "L < S",            // 4 
            "L < S + R"         // 5 
        };
        int warnIndex = 0;

        // *** Конструктор ***
        public aForm1()
        {
            InitializeComponent();
            InitializeDynamicComponent();
#if __A__
            //MessageBox.Show("Inside konstructor class aForm1 : aForm - and after InitializeDynamicComponent(); !!!");
            msg("Inside konstructor class aForm1 : aForm - and after InitializeDynamicComponent(); !!!");
#endif
        }   // end of - конструктор
        
    }       // end of - class Form1 : aForm
}           // end of - namespace Length_Before_Bend
