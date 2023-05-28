using Data;
using Logic;
using System.Collections.ObjectModel;
using System.Reflection;


namespace TP.ConcurrentProgramming.PresentationModel
{
    public abstract class ModelAbstractApi
    {
        public abstract void create(int num);
        public LogicAbstractApi logic;
        public static ModelAbstractApi CreateApi() {
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

        private class ModelLayer : ModelAbstractApi
        {
            public ModelLayer()
            {
                Balls = new ObservableCollection<BallModel>();
                logic = LogicAbstractApi.CreateApi(null);
                logic.LogicApiEvent += (sender, args) => LogicApiEventHandler();
            }
            public ModelLayer(LogicAbstractApi logicLayer)
            {
                logic = logicLayer;
            }
            public override void create(int num)
            {
                logic.create(num);
                ballsModel.Clear();
                for(int i = 0; i < num; i++)
                {
                    BallModel model = new BallModel(logic.getX(i), logic.getY(i));
                    Balls.Add(model);
                }
            }
            public override void stop()
            {
                logic.stop();
                ballsModel.Clear();
            }
            private void LogicApiEventHandler()
            {

                for (int i = 0; i < logic.getNum(); i++)
                {
                    if (logic.getNum() == Balls.Count)
                    {
                        Balls[i].XPos = logic.getX(i);
                        Balls[i].YPos = logic.getY(i);
                    }
                }
            }
        }
    }
}