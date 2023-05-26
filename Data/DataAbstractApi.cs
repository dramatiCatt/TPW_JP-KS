using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Text;

namespace Data
{
    public abstract class DataAbstractApi
    {
        public static DataAbstractApi CreateApi()
        {
            return new DataApi();
        }
        public abstract void create(int num);
        public abstract void stop();
        public abstract Manager GetManager();
        public abstract ObservableCollection<IBall> GetBall();
        public abstract int Width { get; }
        public abstract int Height { get; }

        public abstract event EventHandler BallEvent;
    }
    public class DataApi : DataAbstractApi
    {
        private Manager manager = new Manager();
        private int _width;
        private int _height;
        public DataApi() { }
        public override void create(int num)
        {
            manager = new Manager();
            manager.create(num);
        }
        public override event EventHandler BallEvent;
        public override void stop()
        {
            if (manager != null)
            {
                manager.stop();
            }
        }
        public override Manager GetManager()
        {
            return manager;
        }
        public override ObservableCollection<IBall> GetBall()
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
