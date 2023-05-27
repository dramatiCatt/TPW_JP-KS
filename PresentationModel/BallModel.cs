using Logic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace TP.ConcurrentProgramming.PresentationModel
{
	public class BallModel : INotifyPropertyChanged
	{
		private float xPos;
		private float yPos;
		private int radius;
        public BallModel(float x, float y)
        {
            xPos = x;
            yPos = y;
        }
        public int Radius
		{
			get => radius;
			set
			{
				radius = value;
				RaisePropertyChanged("Radius");
			}
		}
		public float XPos
		{
			get => xPos;
			set
			{
				xPos = value;
				RaisePropertyChanged("XPos");
			}
		}
		public float YPos
		{
			get => yPos;
			set
			{
				yPos = value;
				RaisePropertyChanged("YPos");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
		{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}