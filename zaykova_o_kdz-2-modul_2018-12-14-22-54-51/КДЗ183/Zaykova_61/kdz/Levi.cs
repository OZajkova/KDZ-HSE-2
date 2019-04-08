using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;


namespace kdz
{
	class Levi : Fractal
	{
		public Levi (int deep, Color st, Color fin) : base(deep) {
			st_color = st;
			fin_color = fin;
		}
		
		public void plot_Levi(int x1, int y1, int x2, int y2, int k, ref Graphics g)
		{
			if (k == 0)
			{
				g.DrawLine(penn, x1, y1, x2, y2);
			}
			else
			{
				int x3 = (int)((double)(x1 + x2) / 2 - (double)(y2 - y1) / 2);
				int y3 = (int)((double)(y1 + y2) / 2 + (double)(x2 - x1) / 2);
				plot_Levi(x1, y1, x3, y3, k - 1, ref g);
				plot_Levi(x3, y3, x2, y2, k - 1, ref g);
			}
		}
	}
}
