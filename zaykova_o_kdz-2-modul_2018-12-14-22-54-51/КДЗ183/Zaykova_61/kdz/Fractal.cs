using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace kdz
{
	public abstract class Fractal
	{
		public Color st_color = Color.Black;
		public Color fin_color = Color.Red;
		protected int deep;
		public Pen penn = new Pen(Color.Red, 3);

		protected Fractal(int deep)
		{
			this.deep = deep;
		}

		protected Color find_Color(int it)
		{
			int rr = st_color.R;
			int rl = fin_color.R;
			int gr = st_color.G;
			int gl = fin_color.G;
			int br = st_color.B;
			int bl = fin_color.B;
			int R = rr + (rl - rr) * it / deep;
			int G = gr + (gl - gr) * it / deep;
			int B = br + (bl - br) * it / deep;
			return Color.FromArgb(R, G, B);
		}
	}
}
