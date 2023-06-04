using System.Windows.Input;
using TP.ConcurrentProgramming.PresentationModel;
using System.Collections.ObjectModel;

namespace TP.ConcurrentProgramming.PresentationViewModel
{
    public class MainWindowVM
    {
        private ModelAbstractApi Api { get; set; }
        public ObservableCollection<BallModel> balls { get; }
        public int numBalls { get; set; }
        public ICommand StartClick { get; set; }
        public ICommand StopClick { get; set; }
        public MainWindowVM()
        {
            Api = ModelAbstractApi.CreateApi();
            balls = Api.balls;
            StartClick = new RelayCommand(add);
            StopClick = new RelayCommand(stop);
        }

        public void add()
        {
            Api.create(numBalls);
        }

        private void stop()
        {
            Api.stop();
        }
    }
}
