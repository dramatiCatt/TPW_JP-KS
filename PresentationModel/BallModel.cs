using Logic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TP.ConcurrentProgramming.PresentationModel
{
	public class BallModel : INotifyPropertyChanged
	{
		private float xPos;
		private float yPos;
		private int radius;
		public BallModel(LogicBall ball)
		{
			ball.PropertyChanged += BallPropertyChanged;
			XPos = ball.X;
			YPos = ball.Y;
			Radius = ball.R;
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
				xPosition = value;
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
		public float CenterTransform { get => -1 * Radius; }
		public int Diameter { get => 2 * Radius; }

		public event PropertyChangedEventHandler PropertyChanged;
		public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
		{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		private void BallPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			BallLogic b = (BallLogic)sender;

			XPos = b.X;
			YPos = b.Y;
			RaisePropertyChanged("XPos");
			RaisePropertyChanged("YPos");
		}
	}
}