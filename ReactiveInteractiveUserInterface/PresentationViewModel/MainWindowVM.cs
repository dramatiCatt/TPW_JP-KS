
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using TP.ConcurrentProgramming.PresentationModel;
using TP.ConcurrentProgramming.PresentationViewModel;

namespace TP.ConcurrentProgramming.PresentationViewModel
{
    public class MainWindowVM : INotifyPropertyChanged

    {
        private IList _balls;
        private ModelAbstractApi model { get; set; }
        private int _width;
        private int _height;
        private int _num;
        private string _text;


        public MainWindowVM()
        {
            model = ModelAbstractApi.CreateAPI();
            StartClick = new RelayCommand(() => create());
            StopClick = new RelayCommand(() => stop());
            _height = model.Height + 4;
            _width = model.Width + 4;
            _balls = model.create(_num);
        }

        public ICommand StopClick { get; set; }
        public ICommand StartClick { get; set; }

        private void create()
        {
            model.create(_num);
        }

        private void stop()
        {
            model.stop();
        }

        public int Height
        {
            get => _height;
            set
            {
                _height = value;
                RaisePropertyChanged("Height");
            }
        }

        public int Width
        {
            get => _width;
            set
            {
                _width = value;
                RaisePropertyChanged("Width");
            }
        }

        public string BallsNum
        {
            get { return _text; }
            set
            {
                _text = value;
                try
                {
                    int val = System.Int32.Parse(_text);
                    if (val > 0 && val <= 20)
                    {
                        _num = val;
                    }
                    else
                    {
                        _num = 0;
                    }
                    RaisePropertyChanged(nameof(Number));

                }
                catch (System.FormatException)
                {
                    Trace.WriteLine("Text() z viewModel rzucil wyjatek Format");
                    _num = 0;
                    RaisePropertyChanged(nameof(Number));
                }
                catch (System.OverflowException)
                {
                    Trace.WriteLine("Text() z viewModel rzucil wyjatek Overflow");
                    _num = 0;
                    RaisePropertyChanged(nameof(Number));
                }
            }
        }

        public int Number
        {
            get
            {
                return _num;
            }
        }

        public IList Balls
        {
            get => _balls;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {

            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
