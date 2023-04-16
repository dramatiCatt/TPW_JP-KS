
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using TP.ConcurrentProgramming.PresentationModel;
using TP.ConcurrentProgramming.PresentationViewModel;

namespace TP.ConcurrentProgramming.PresentationViewModel
{
    public class MainWindowVM : VMBase

    {
        private IList balls;
        private ModelAbstractApi ModelLayer = ModelAbstractApi.CreateApi();
        private int width;
        private int height;
        private int num;
        private string text;


        public MainWindowVM() : this(ModelAbstractApi.CreateApi())
        {
        }

        public MainWindowVM(ModelAbstractApi modelAbstractApi)
        {
            ModelLayer = modelAbstractApi;
            StartClick = new RelayCommand(() => create());
            StopClick = new RelayCommand(() => stop());
            height = ModelLayer.Height + 4;
            width = ModelLayer.Width + 4;
            balls = ModelLayer.create(num);
        }

        public ICommand StartClick { get; set; }

        private void create()
        {
            ModelLayer.create(num);
            ModelLayer.moving();
        }

        public ICommand StopClick { get; set; }

        private void stop()
        {
            ModelLayer.stop();
        }

        public int Height
        {
            get
            {
                return height;
            }
        }

        public int Width
        {
            get
            {
                return width;
            }
        }

        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                try
                {
                    int val = System.Int32.Parse(text);
                    if (val > 0 && val <= 20)
                    {
                        num = val;
                    }
                    else
                    {
                        num = 0;
                    }
                    RaisePropertyChanged(nameof(Number));

                }
                catch (System.FormatException)
                {
                    Trace.WriteLine("Text() z viewModel rzucil wyjatek Format");
                    num = 0;
                    RaisePropertyChanged(nameof(Number));
                }
                catch (System.OverflowException)
                {
                    Trace.WriteLine("Text() z viewModel rzucil wyjatek Overflow");
                    num = 0;
                    RaisePropertyChanged(nameof(Number));
                }
            }
        }

        public int Number
        {
            get
            {
                return num;
            }
        }

        public IList Balls
        {
            get
            {
                return balls;
            }
            set
            {
                if (value.Equals(balls))
                    return;
                balls = value;
                RaisePropertyChanged(nameof(Balls));
            }
        }
    }
}
