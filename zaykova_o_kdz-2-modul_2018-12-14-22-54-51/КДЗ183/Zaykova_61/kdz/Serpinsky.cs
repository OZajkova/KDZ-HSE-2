using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace kdz
{
	class Serpinsky : Fractal 
	{
		public Serpinsky (int deep, Color st, Color fin) : base(deep) {
			st_color = st;
			fin_color = fin;
		}

		/// <summary>
		/// строит треугольник Серпинского по 3 точкам-координатам треугольника, глубине рекурсии k, цвету col и поверхности рисования.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		/// <param name="k"></param>
		/// <param name="col"></param>
		/// <param name="g"></param>
		public void plot_Serpinsky(Point x, Point y, Point z, int k, int col, ref Graphics g) //x-самая левая точка, z-самая правая, y-верхняя
		{
			if (k == 0)
			{
				Color a = find_Color(col);
				this.penn = new Pen(a, 3);
				g.DrawLine(penn, x, y);
				g.DrawLine(penn, y, z);
				g.DrawLine(penn, z, x);
			}
			else
			{
				Point L = new Point((x.X + y.X) / 2, (x.Y + y.Y) / 2); //серединка левой
				Point R = new Point((z.X + y.X) / 2, (z.Y + y.Y) / 2); //серединка правой
				Point D = new Point((x.X + z.X) / 2, (x.Y + z.Y) / 2); //серединка нижней

				col++;
				plot_Serpinsky(x, L, D, k - 1, col, ref g);
				plot_Serpinsky(D, R, z, k - 1, col, ref g);
				plot_Serpinsky(L, y, R, k - 1, col, ref g);

				Color a = find_Color(col);
				penn = new Pen(a, 3);
				g.DrawLine(penn, L, R);
				g.DrawLine(penn, R, D);
				g.DrawLine(penn, D, L);
			}
		}
	}
}
