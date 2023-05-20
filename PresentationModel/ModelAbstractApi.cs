using Logic;
using System.Collections.ObjectModel;


namespace TP.ConcurrentProgramming.PresentationModel
{
    public abstract class ModelAbstractApi
    {
        private ObservableCollection<BallModel> ballsModel = new ObservableCollection<BallModel>();
        public static ModelAbstractApi CreateAPI(LogicAbstractApi logicApi = default)
        {
            return new ModelLayer(logicApi ?? LogicAbstractApi.CreateApi());
        }
        public abstract ObservableCollection<BallModel> create(int num);
        public abstract void stop();
        public abstract int Height { get; }
        public abstract int Width { get; }
        public ObservableCollection<BallModel> BallsModel
        {
            get => ballsModel;
            set => ballsModel = value;
        }

        internal class ModelLayer : ModelAbstractApi
        {
            private readonly LogicAbstractApi logic;
            public ModelLayer(LogicAbstractApi logicLayer)
            {
                logic = logicLayer;
            }
            public override int Width => logic.Width;
            public override int Height => logic.Height;
            public override ObservableCollection<BallModel> create(int num)
            {
                logic.CreateBalls(number);
                ballsModel.Clear();
                foreach (BallLogic ball in logic.GetBalls())
                {
                    ballsModel.Add(new BallModel(ball));
                }
                return ballsModel;
            }
            public override void stop()
            {
                logic.stop();
                ballsModel.Clear();
            }
        }
    }
}