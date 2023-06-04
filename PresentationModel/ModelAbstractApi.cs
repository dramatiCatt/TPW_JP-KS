using Logic;
using System.Collections.ObjectModel;


namespace TP.ConcurrentProgramming.PresentationModel
{
    public abstract class ModelAbstractApi
    {
        public LogicAbstractApi logic;
        public static ModelAbstractApi CreateApi() {
            return new ModelLayer();
        }

        public ObservableCollection<BallModel> balls;
        public abstract void stop();
        public abstract void create(int num);

        private class ModelLayer : ModelAbstractApi
        {
            public ModelLayer()
            {
                balls = new ObservableCollection<BallModel>();
                logic = LogicAbstractApi.CreateApi(null);
                logic.LogicApiEvent += (sender, args) => LogicApiEventHandler();
            }

            public override void create(int num)
            {
                logic.create(num);
                balls.Clear();
                for(int i = 0; i < num; i++)
                {
                    BallModel model = new BallModel(logic.getX(i), logic.getY(i));
                    balls.Add(model);
                }
            }
            public override void stop()
            {
                logic.stop();
                balls.Clear();
            }
            private void LogicApiEventHandler()
            {

                for (int i = 0; i < logic.getNum(); i++)
                {
                    if (logic.getNum() == balls.Count)
                    {
                        balls[i].XPos = logic.getX(i);
                        balls[i].YPos = logic.getY(i);
                    }
                }
            }
        }
    }
}