using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class RIN
    {
        int min = 0;    // нижня границя
        int max = 100;  // верхня границя
        public string info = "число"; // ім*я параметра
        public bool n_bool; // успішність перетворення

        // Обмежене число: поле n і властивість N
        int n;
        public int N    // властивість
        {
            get { return n; }
            set
            {
                if (value < min)
                {
                    n = min;
                    n_bool = false;
                }
                else if (value > max)
                {
                    n = max;
                    n_bool = false;
                }
                else
                {
                    n = value;
                    n_bool = true;
                }
            }
        }

        // конструктор
        public RIN(string n_st, int n_min, int n_max, int n_def, string n_info)
        {
            min = n_min;
            max = n_max;
            info = n_info;
            n_bool = true;
            try
            {
                N = Convert.ToInt32(n_st);
                if (!n_bool)
                {
                    info = "Помилка вводу параметра << " + info + " >>. Число не з діапазона. Автоматично присвоюєьбся нижня/верхня границя. Для зміни введіть ціле число від " + min.ToString() + " до " + max.ToString();
                }
            }
            catch
            {
                info = "Помилка вводу параметра << " + info + " >>. Введіть ціле число від " + min.ToString() + " до " + max.ToString() + ". По замовчанню параметр =  " + n_def.ToString(); ;
                n = n_def;
                n_bool = false;
            }
        }
    }
}

