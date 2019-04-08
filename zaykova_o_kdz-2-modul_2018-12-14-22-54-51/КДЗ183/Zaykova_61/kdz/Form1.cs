using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kdz
{
	public partial class Form1 : Form
	{
		Graphics g;
		Bitmap bmap;
		int size = 1;
		int cur_fract = 0;
		int X = 0, Y = 0;
		Point start;
		Color st_color = Color.Black;
		Color fin_color = Color.Red;
		int deep = 0;

		public Form1()
		{
			InitializeComponent();
			bmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
			g = Graphics.FromImage(bmap);
			MinimumSize = new Size(800, 550); 
		}

		/// <summary>
		/// Строим треугольник Серпинского
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button3_Click(object sender, EventArgs e)
		{
			g.Clear(Color.SeaShell);
			bool flag = true;

			flag = int.TryParse(textBox1.Text, out deep);

			//проверяем корректность
			if (flag)
			{
				if (deep > 12 || deep < 1)
				{
					MessageBox.Show("Глубина рекурсии для данного фрактала не может быть меньше 1 или больше 12!");
					flag = false;
				}
			}
			else
				MessageBox.Show("Глубина введена неверно!");

			if (flag)
			{
				deep--;
				Serpinsky a = new Serpinsky(deep, st_color, fin_color);

				Point centr = new Point(pictureBox1.Width / 2, pictureBox1.Height / 2);
				if (deep != 0)
				{
					a.plot_Serpinsky(new Point(100 * size + X, 370 * size + Y), new Point(300 * size + X, 50 * size + Y), new Point(500 * size + X, 370 * size + Y), deep, 0, ref g);
				}
				if (deep == 0)
					a.st_color = a.fin_color;
				g.DrawLine(new Pen(a.st_color, 3), new Point(100 * size + X, 370 * size + Y), new Point(300 * size + X, 50 * size + Y));
				g.DrawLine(new Pen(a.st_color, 3), new Point(100 * size + X, 370 * size + Y), new Point(500 * size + X, 370 * size + Y));
				g.DrawLine(new Pen(a.st_color, 3), new Point(300 * size + X, 50 * size + Y), new Point(500 * size + X, 370 * size + Y));
				pictureBox1.Image = bmap;
				cur_fract = 3;
			}
		}
		

		
		/// <summary>
		/// Строим С-кривую Леви
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button1_Click(object sender, EventArgs e)
		{
			g.Clear(Color.SeaShell);
			bool flag = true;

			flag = int.TryParse(textBox1.Text, out deep);

			if (flag)
			{
				if (deep > 17 || deep < 1)
				{
					MessageBox.Show("Глубина рекурсии для данного фрактала не может быть меньше 1 или больше 17!");
					flag = false;
				}
			}
			else
				MessageBox.Show("Глубина введена неверно!");

			if (flag)
			{
				//Выпоняем заданное число итераций, для каждой определим свой цвет и нарисуем
				Levi a = new Levi(--deep, st_color, fin_color);
				if(deep != 0)
					for (int j = 0; j < deep + 1; j++)
					{
						int rr = st_color.R;
						int rl = fin_color.R;
						int gr = st_color.G;
						int gl = fin_color.G;
						int br = st_color.B;
						int bl = fin_color.B;
						int R = rr + (rl - rr) * j / deep;
						int G = gr + (gl - gr) * j / deep;
						int B = br + (bl - br) * j / deep;
						a.penn = new Pen(Color.FromArgb(R, G, B), 3);
						a.plot_Levi((pictureBox1.Width / 2) * size + X, (pictureBox1.Height / 3 - 26) * size + Y, (pictureBox1.Width / 2) * size + X, (2 * pictureBox1.Height / 3 - 26) * size + Y, j, ref g);
					}
				else
				{
					a.penn = new Pen(fin_color, 3);
					a.plot_Levi((pictureBox1.Width / 2) * size + X, (pictureBox1.Height / 3 - 26) * size + Y, (pictureBox1.Width / 2) * size + X, (2 * pictureBox1.Height / 3 - 26) * size + Y, 0, ref g);
				}

				
				pictureBox1.Image = bmap;
			}
			cur_fract = 1;
		}

		/// <summary>
		/// Тут строим кривую Коха
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button2_Click(object sender, EventArgs e)
		{
			g.Clear(Color.SeaShell);

			bool flag = true;
			flag = int.TryParse(textBox1.Text, out deep);
			if (flag)
			{
				if (deep > 9 || deep < 1)
				{
					MessageBox.Show("Глубина рекурсии для данного фрактала не может быть меньше 1 или больше 9!");
					flag = false;
				}
			}
			else
				MessageBox.Show("Глубина введена неверно!");

			if (flag)
			{
				Koh a = new Koh(deep - 1, st_color, fin_color);
				if(deep != 1)
					g.DrawLine(new Pen(a.st_color, 3), 100 * size + X, 300 * size + Y, 500 * size + X, 300 * size + Y);
				else
					g.DrawLine(new Pen(a.fin_color, 3), 100 * size + X, 300 * size + Y, 500 * size + X, 300 * size + Y);
				a.plot_Koh(100 * size + X, 300 * size + Y, 500 * size + X, 300 * size + Y, deep - 1, 0, 1, ref g);
			}
				pictureBox1.Image = bmap;

			cur_fract = 2;
		}

		/// <summary>
		/// Происходит изменение масштаба. Вызывается функция перерисовки.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBox1.SelectedItem.ToString() == "x1")
			{
				size = 1;
				X = 0;
				Y = 0;
			}
			if (comboBox1.SelectedItem.ToString() == "x2")
			{
				size = 2;
				X = -pictureBox1.Width / 2;
				Y = -pictureBox1.Height / 2;
			}
			if (comboBox1.SelectedItem.ToString() == "x3")
			{
				size = 3;
				X = -pictureBox1.Width;
				Y = -pictureBox1.Height;
			}
			if (comboBox1.SelectedItem.ToString() == "x5")
			{
				size = 5;
				X = -2 * pictureBox1.Width;
				Y = -2 * pictureBox1.Height;
			}
			ReDraw();
		}


		/// <summary>
		/// Получает размеры окна при изменении. Меняет значения ширины и высоты pictureBox, создает новый bitmap. Если окно свернули, то выводит информацию об этом.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Form1_SizeChanged(object sender, EventArgs e)
		{
			try
			{
				pictureBox1.Width = Width - 233;
				pictureBox1.Height = Height - 26;
				bmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
				g = Graphics.FromImage(bmap);
				ReDraw();
			}
			catch
			{
				MessageBox.Show("Вы свернули окно!");
			}
		}

		private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
		{
			start = new Point(Cursor.Position.X, Cursor.Position.Y);
		}

		/// <summary>
		/// выбор стартового цвета
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button4_Click(object sender, EventArgs e)
		{
			colorDialog1.ShowDialog();
			st_color = colorDialog1.Color;
			button4.BackColor = colorDialog1.Color;
			ReDraw();
		}

		/// <summary>
		/// выбор финального цвета
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button5_Click(object sender, EventArgs e)
		{
			colorDialog2.ShowDialog();
			fin_color = colorDialog2.Color;
			button5.BackColor = colorDialog2.Color;
			ReDraw();
		}

		/// <summary>
		/// сохранение фрактала
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button6_Click(object sender, EventArgs e)
		{
			try
			{
				SaveFileDialog save = new SaveFileDialog();
				save.ShowDialog();
				string filename = save.FileName + ".jpg";
				bmap.Save(filename, System.Drawing.Imaging.ImageFormat.Jpeg);
			}
			catch
			{
				MessageBox.Show("Не удалось сохранить");
			}
		}

		private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
		{
				Point end = new Point(Cursor.Position.X, Cursor.Position.Y);
				X += Cursor.Position.X - start.X;
				Y += Cursor.Position.Y - start.Y;
				ReDraw();
		}

		/// <summary>
		/// Кнопка, задающая исходные значения координаты фрактала. Она возвращает его в зону видимости.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button7_Click(object sender, EventArgs e)
		{
			if(size == 1)
			{
				X = 0; Y = 0;
			}
			if(size == 2)
			{
				X = -pictureBox1.Width / 2;
				Y = -pictureBox1.Height / 2;
			}
			if(size == 3)
			{
				X = -pictureBox1.Width;
				Y = -pictureBox1.Height;
			}
			if(size == 5)
			{
				X = -2 * pictureBox1.Width;
				Y = -2 * pictureBox1.Height;
			}
			ReDraw();
		}

		/// <summary>
		/// выполняет перерисовку фрактала
		/// </summary>
		void ReDraw()
		{
			if (cur_fract == 1)
				button1.PerformClick();
			if (cur_fract == 2)
				button2.PerformClick();
			if (cur_fract == 3)
				button3.PerformClick();
		}
	}
}
