using System.Numerics;
using Logic;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections;
using System.Collections.ObjectModel;

namespace TP.ConcurrentProgramming.PresentationModel
{
    public abstract class ModelAbstractApi
    {
        public abstract int Height { get; } 
        public abstract int Width { get; }
        public abstract ObservableCollection<Ball> create(int num);
        public abstract void stop();
        public abstract void moving();
        public static ModelAbstractApi CreateApi()
        {
            return new ModelApi();
        }
    }

    internal class ModelApi : ModelAbstractApi
    {
        private readonly Manager manager = new Manager();
        public override int Width => Manager.width;
        public override int Height => Manager.height;
        public override void moving() => manager.moving();
        public override ObservableCollection<Ball> create(int num)
        {
            manager.create(num);
            return manager.Balls;
        }

        public override void stop() => manager.stop();

    }
}