using System;
using System.Windows;
using TaskbarCat.ViewModels;
using System.Windows.Forms;
using TaskbarCat.Services;

namespace TaskbarCat
{
    public partial class MainWindow : Window
    {
        private NotifyIcon? _notifyIcon;
        private readonly SettingsService _settingsService;

        public MainWindow()
        {
            InitializeComponent();
            _settingsService = new SettingsService();
            this.Loaded += MainWindow_Loaded;
            this.Closing += MainWindow_Closing;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = new MainViewModel();
            PositionWindowOnTaskbar();
            SetupNotifyIcon();
        }

        private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            _notifyIcon?.Dispose();
        }

        private void SetupNotifyIcon()
        {
            _notifyIcon = new NotifyIcon();
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var resourceName = "TaskbarCat.Images.Dog.Dog-Icon.ico";
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream != null)
                {
                    _notifyIcon.Icon = new System.Drawing.Icon(stream);
                }
            }
            _notifyIcon.Text = "Taskbar Dog";
            _notifyIcon.Visible = true;

            var contextMenu = new ContextMenuStrip();

            var switchDogMenuItem = new ToolStripMenuItem("Switch to Dog");
            switchDogMenuItem.Click += (s, e) => SwitchAnimal("Dog");
            contextMenu.Items.Add(switchDogMenuItem);

            var switchCatMenuItem = new ToolStripMenuItem("Switch to Cat");
            switchCatMenuItem.Click += (s, e) => SwitchAnimal("Cat");
            contextMenu.Items.Add(switchCatMenuItem);

            contextMenu.Items.Add(new ToolStripSeparator());

            var exitMenuItem = new ToolStripMenuItem("Exit");
            exitMenuItem.Click += (s, e) => System.Windows.Application.Current.Shutdown();
            contextMenu.Items.Add(exitMenuItem);

            _notifyIcon.ContextMenuStrip = contextMenu;
        }

        private void SwitchAnimal(string animalName)
        {
            if (this.DataContext is MainViewModel vm)
            {
                vm.SwitchAnimalCommand.Execute(animalName);
            }
        }

        private void PositionWindowOnTaskbar()
        {
            var desktopWorkingArea = SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width - _settingsService.HorizontalOffset;
            this.Top = desktopWorkingArea.Bottom - this.Height;
        }
    }
}
