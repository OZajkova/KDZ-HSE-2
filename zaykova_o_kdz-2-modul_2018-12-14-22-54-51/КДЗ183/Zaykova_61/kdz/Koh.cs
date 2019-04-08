using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace kdz
{
	class Koh : Fractal
	{
		public Koh(int deep, Color st, Color fin) : base(deep) {
			st_color = st;
			fin_color = fin;
		}

		/// <summary>
		/// строит кривую Коха по точками начала и конца(первые 4 координаты), глубине рекурсии k, углу А, цвету и поверхности рисования. 
		/// </summary>
		/// <param name="x1"></param>
		/// <param name="y1"></param>
		/// <param name="x2"></param>
		/// <param name="y2"></param>
		/// <param name="k"></param>
		/// <param name="A"></param>
		/// <param name="col"></param>
		/// <param name="g"></param>
		public void plot_Koh(float x1, float y1, float x2, float y2, int k, float A, int col, ref Graphics g)
		{
			if(k > 0)
			{
				float len = (float)Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
				float section = len / 3;
				float a = x1 + (float)(1.0 / 3) * (x2 - x1) + section * (float)Math.Cos(A + (Math.PI / 3)); //Х координата вершины
				float b = y1 + (float)(1.0 / 3) * (y2 - y1) - section * (float)Math.Sin(A + (Math.PI / 3));//У координата
																										   //Выведем ее: к середине исходного отрезка надо прибавить столько, чтобы полученная точка образовывала равносторонний треугольник, стоящий на 2 секции
																										   //Следовательно, прибавить надо sqrt(section^2 + (1/2 * section)^2)=3/4 section^2

				//g.DrawLine(penn, x1 + (float)(1.0 / 3) * (x2 - x1), y1 + (float)(1.0 / 3) * (y2 - y1), x1 + (float)(2.0 / 3) * (x2 - x1), y1 + (float)(2.0 / 3) * (y2 - y1));

				Color cc = find_Color(col);
				penn = new Pen(cc, 3);


				g.DrawLine(penn, x1 + (float)(1.0 / 3) * (x2 - x1), y1 + (float)(1.0 / 3) * (y2 - y1), a, b);
				g.DrawLine(penn, a, b, x1 + (float)(2.0 / 3) * (x2 - x1), y1 + (float)(2.0 / 3) * (y2 - y1));

				plot_Koh(x1 + (float)(1.0 / 3) * (x2 - x1), y1 + (float)(1.0 / 3) * (y2 - y1), a, b, k - 1, A + (float)Math.PI / 3, col + 1, ref g);
				plot_Koh(a, b, x1 + (float)(2.0 / 3) * (x2 - x1), y1 + (float)(2.0 / 3) * (y2 - y1), k - 1, A - (float)Math.PI / 3, col + 1, ref g);

				plot_Koh(x1, y1, x1 + (float)(1.0 / 3) * (x2 - x1), y1 + (float)(1.0 / 3) * (y2 - y1), k - 1, A, col + 1, ref g);
				plot_Koh(x1 + (float)(2.0 / 3) * (x2 - x1), y1 + (float)(2.0 / 3) * (y2 - y1), x2, y2, k - 1, A, col + 1, ref g);
			}
		}
	}
}
