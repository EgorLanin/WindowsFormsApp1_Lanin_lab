using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Point[] p = new Point[1000]; // масив точок - не більше 1000
        int n_fakt; // фактичне число пробоїн
        int Rmin = 0; // Результат - мінімальний радіус
        Graphics graph1; // Графічний контент - холст
        public Form1()
        {
            InitializeComponent();
            graph1 = this.pictureBox1.CreateGraphics();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            graph1.Clear(Color.White);
        }
        // стрільба
        public void shots(Point[] p, int N, int D, int W, int R, int K)
        {
            Random ran = new Random();
            for (int i = 0; i < N; i++)
            {
                double r = R * (11 - K) / 10 * ran.NextDouble();
                double fi = 2 * Math.PI * ran.NextDouble();
                p[i].X = Convert.ToInt32(W * Math.Cos(D * Math.PI / 180) + r * Math.Cos(fi));
                p[i].Y = Convert.ToInt32(W * Math.Sin(R * Math.PI / 180) + r * Math.Sin(fi));
            }
        }
        // Знаходження мінімального радіуса і номера точки
        private int MinRad(Point[] p, int n, out int t)
        {
            double R = 0.0; // мінімальний радіус
            double R1; // радіус точки (x,y)
            t = -1; // номер точки
                    // Цикл по всім точкам, знаходження радіуса R
            for (int i = 0; i < n; i++)
            {
                R1 = Math.Sqrt(p[i].X * p[i].X + p[i].Y * p[i].Y);
                if (R1 > R)
                {
                    R = R1;
                    t = i;
                }
            }
            return Convert.ToInt32(R); // Мінімальний радіус
        }

        // Запуск стрільби по мішені і розрахунок мінімального радіуса круга, який містить  всі попадання 
        private void button2_Click(object sender, EventArgs e)
        {
            // Задання параметрів:
            RIN n = new RIN(textBox1.Text, 1, 1000, 20, "число куль");
            n_fakt = n.N;
            textBox1.Text = n.N.ToString();
            if (!n.n_bool)
                MessageBox.Show(n.info);
            RIN d = new RIN(textBox2.Text, 0, 360, 180, "напрямок вітру");
            if (!d.n_bool)
            {
                MessageBox.Show(d.info);
                textBox2.Text = d.N.ToString();
            }
            RIN w = new RIN(textBox3.Text, 0, 20, 20, "швидкість ветра");
            if (!w.n_bool)
            {
                MessageBox.Show(w.info);
                textBox3.Text = w.N.ToString();
            }
            RIN r = new RIN(textBox4.Text, 100, 200, 200, "радіус мішені");
            if (!r.n_bool)
            {
                MessageBox.Show(r.info);
                textBox4.Text = r.N.ToString();
            }
            RIN k = new RIN(textBox5.Text, 1, 10, 3, "кучність стрільби");
            if (!k.n_bool)
            {
                MessageBox.Show(k.info);
                textBox5.Text = k.N.ToString();
            }
            // Заповнення мішені випадковими пробоїнами
            shots(p, n.N, d.N, w.N, r.N, k.N);
            // Вивід масива точок (x,y) в ListBox1
            listBox1.Items.Clear();
            for (int i = 0; i < n.N; i++)
                listBox1.Items.Add(p[i].X.ToString() + " " + p[i].Y.ToString());
            // Знахождення мінімального радіуса і "номера" точки k
            int t = -1;
            Rmin = MinRad(p, n.N, out t);
            // Вивід результату в ListBox2
            listBox2.Items.Clear();
            listBox2.Items.Add("Результат :");
            listBox2.Items.Add("Радіус максимального круга = " + Rmin.ToString());
            //  Для контроля:
            listBox2.Items.Add("Найвіддаленіша точка № " + (t + 1).ToString() + " x = " + p[t].X.ToString() + " y = " + p[t].Y.ToString());
            int grad = d.N;
            if (grad < 180)
                grad += 180;
            else
                grad -= 180;
            listBox2.Items.Add("Потрібна поправка " + w.N + " на " + grad.ToString() + " градусів");
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // інструменти
            Pen Pen1 = new Pen(Color.Green, 2); // Лінії
            Pen Pen2 = new Pen(Color.Red, 1); // коло
            Pen Pen3 = new Pen(Color.Blue, 1); // Точки
            Pen Pen4 = new Pen(Color.Black, 1); // Мішень
            SolidBrush brush1 = new SolidBrush(Color.Black); // Текст міток на осях
            Font font1 = new Font("Arial", 10); // Шрифт і розмір міток
                                                // Пов*язування холста з pictureBox
            graph1 = this.pictureBox1.CreateGraphics();
            // Відтки на координатних осях
            Single X, Y;
            for (X = -200; X <= 200; X += 50)
                graph1.DrawString(X.ToString(), font1, brush1, X + 200, 200);
            for (Y = -200; Y <= 200; Y += 50)
                graph1.DrawString(Y.ToString(), font1, brush1, 200, 200 - Y);
            // Перетворення комп*ютерної системи координат в математичну
            // Поворот осі Y
            graph1.ScaleTransform(1, -1);
            // Здвиг по осям X и Y
            graph1.TranslateTransform(200, -200);
            // Зображення осей математичної системи координат
            // Вісь X
            graph1.DrawLine(Pen1, -200, 0, 200, 0);
            // Вісь Y
            graph1.DrawLine(Pen1, 0, -200, 0, 200);
            // Робимо засічки по осям координат
            for (X = -200; X <= 200; X += 50)
                graph1.DrawLine(Pen1, X, -5, X, 5);
            for (Y = -200; Y <= 200; Y += 50)
                graph1.DrawLine(Pen1, -5, Y, 5, Y);
            // концентричні круги: 10, 9, 8, ... , 1 (підрахунок очків)
            int rm = Convert.ToInt32(textBox4.Text);
            double dr = rm / 10;
            double rz = 0;
            for (int i = 0; i < 10; i++)
            {
                rz += dr;
                int riz = (int)rz;
                graph1.DrawEllipse(Pen4, -riz, -riz, 2 * riz, 2 * riz);
            }
            // Зображаємо точки і коло мінімального радіуса
            int n = n_fakt;
            for (int i = 0; i < n; i++)
                graph1.FillEllipse(new SolidBrush(Color.BlueViolet), p[i].X - 3, p[i].Y - 3, 6, 6);
            graph1.DrawEllipse(Pen2, -Rmin, -Rmin, 2 * Rmin, 2 * Rmin);
        }
    }
}
