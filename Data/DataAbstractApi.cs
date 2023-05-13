using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Data
{
    public abstract class DataAbstractApi
    {
        public static DataAbstractApi CreateApi()
        {
            return new DataApi();
        }
        public abstract void createBalls(int num);
        public abstract void stop();
        public abstract Manager GetManager();
        public abstract ObservableCollection<Ball> GetBalls();
        public abstract int Width { get; }
        public abstract int Height { get; }
    }
    public class DataApi : DataAbstractApi
    {
        private Manager manager = new Manager();
        private int width;
        private int height;
        public DataApi() { }
        public override void create(int num)
        {
            manager = new Manager();
            manger.create(num);
        }
        public override void stop()
        {
            if (manager != null)
            {
                manager.stop();
            }
        }
        public override Manager GetManger()
        {
            return manager;
        }
        public override ObservableCollection<Ball> GetBalls()
        {
            return manager.Balls;
        }
        public override int Height
        {
            get => manager.Height;
        }
        public override int Width
        {
            get => manager.Width;
        }
    }

}
