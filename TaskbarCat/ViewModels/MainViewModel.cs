using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using TaskbarCat.Controllers;
using TaskbarCat.Services;

namespace TaskbarCat.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly CPUMonitorService _cpuMonitorService;
        private readonly AnimationService _animationService;
        private readonly AnimationController _animationController;
        private readonly DispatcherTimer _timer;

        private string _currentAnimal = "Dog";

        private BitmapImage? _currentFrame;
        public BitmapImage? CurrentFrame
        {
            get => _currentFrame;
            set
            {
                _currentFrame = value;
                OnPropertyChanged();
            }
        }

        public ICommand SwitchAnimalCommand { get; }

        public MainViewModel()
        {
            _cpuMonitorService = new CPUMonitorService();
            _animationService = new AnimationService();
            _animationController = new AnimationController();

            SwitchAnimalCommand = new RelayCommand(SwitchAnimal);

            // Start with the default animal
            SwitchAnimal(_currentAnimal);

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(16); // ~60 FPS
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void SwitchAnimal(object? parameter)
        {
            if (parameter is string animalName)
            {
                _currentAnimal = animalName;
                var frames = _animationService.GetFrames(_currentAnimal);
                _animationController.SetFrames(frames);
            }
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            float cpuUsage = _cpuMonitorService.GetCurrentCpuUsage();
            _animationController.Update(_timer.Interval.TotalSeconds, cpuUsage);
            CurrentFrame = _animationController.CurrentFrame;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // Simple RelayCommand implementation
    public class RelayCommand : ICommand
    {
        private readonly Action<object?> _execute;
        public event EventHandler? CanExecuteChanged;

        public RelayCommand(Action<object?> execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object? parameter) => true;
        public void Execute(object? parameter) => _execute(parameter);
    }
}
