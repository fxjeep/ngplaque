using System;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Paiwei
{
	public partial class ZoomPictureBox : ScrollableControl
	{
		private Image img;

		public Image ImageSource
		{
			get { return img; }
			set { img = value;
				UpdateZoom();
				Invalidate();
			}
		}

		public float ImageWidthInCM
		{
			get
			{
				if (img == null)
				{
					return 0;
				}
				else
				{
					return (float)(img.Width / img.HorizontalResolution);
				}
			}
		}

		public float ImageHeightInCM
		{
			get
			{
				if (img == null)
				{
					return 0;
				}
				else
				{
					return (float)(img.Height / img.VerticalResolution);
				}
			}
		}

		public bool HasImage
		{
			get
			{
				if (img == null)
				{
					return false;
				}
				return true;
			}
		}

		public float Resolution
		{
			get
			{
				if (img != null)
				{
					return img.HorizontalResolution/2.54f;
				}
				return 0;
			}
		}

		private float zoom = 1f;
		public float Zoom
		{
			get { return zoom; }
			set { 
			    if ( value < 0 || value < 0.00001f )
				{
					value = 0.00001f;
				}
				zoom = value;
				UpdateZoom();
				Invalidate();
			}
		}

		public ZoomPictureBox()
		{
			this.SetStyle(ControlStyles.AllPaintingInWmPaint |
						  ControlStyles.UserPaint |
						  ControlStyles.ResizeRedraw |
						  ControlStyles.UserPaint |
						  ControlStyles.DoubleBuffer, true);
			this.AutoScroll = true;
			InitializeComponent();
		}

		private void UpdateZoom()
		{
			if (img == null )
			{
				this.AutoScrollMargin = this.Size;
			}
			else
			{
				this.AutoScrollMinSize = new Size((int)(this.img.Width*zoom+0.5f), 
												  (int)(this.img.Height*zoom+0.5f));
			}
		}

		private InterpolationMode intermode = InterpolationMode.High;
		public InterpolationMode InterMode
		{
			get { return intermode; }
			set { intermode = value; }
		}

		protected override void  OnPaint(PaintEventArgs e)
		{
			if (img != null )
			{ 			
			Matrix mx = new Matrix(zoom, 0, 0, zoom, 0, 0);
			mx.Translate(this.AutoScrollPosition.X / zoom, this.AutoScrollPosition.Y/zoom );
			
			e.Graphics.Transform = mx;
			e.Graphics.InterpolationMode = this.intermode;
			e.Graphics.DrawImage(img, new Rectangle(0, 0, this.img.Width, this.img.Height),
								 0, 0, img.Width, img.Height, GraphicsUnit.Pixel);
			}
			base.OnPaint(e);
		}	
	}
}
