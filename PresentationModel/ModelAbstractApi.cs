using Data;
using Logic;
using System.Collections.ObjectModel;
using System.Reflection;


namespace TP.ConcurrentProgramming.PresentationModel
{
    public abstract class ModelAbstractApi
    {
        public static ModelAbstractApi Instance()
        {
            return new ModelLayer();
        }
        public ObservableCollection<BallModel> ballsModel;
        public static ModelAbstractApi CreateApi(LogicAbstractApi logicApi = default)
        {
            return new ModelLayer(logicApi ?? LogicAbstractApi.CreateApi());
        }
        public ObservableCollection<BallModel> Balls;
        public abstract void stop();

        public ObservableCollection<BallModel> BallsModel
        {
            get => ballsModel;
            set => ballsModel = value;
        }

        internal class ModelLayer : ModelAbstractApi
        {

            public ModelLayer()
            {
                Balls = new ObservableCollection<BallModel>();
                logic = logic.CreateApi(null);
                logic.LogicApiEvent += (sender, args) => LogicApiEventHandler();
            }
            private readonly LogicAbstractApi logic;
            public ModelLayer(LogicAbstractApi logicLayer)
            {
                logic = logicLayer;
            }
            public override ObservableCollection<BallModel> create(int num)
            {
                logic.create(num);
                ballsModel.Clear();
                for(int i = 0; i < num; i++)
                {
                    BallModel model = new BallModel(logic.getX(i), logic.getY(i));
                    Balls.Add(model);
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