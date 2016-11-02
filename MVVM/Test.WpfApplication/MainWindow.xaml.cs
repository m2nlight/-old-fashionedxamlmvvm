using System.Windows;

namespace Test.WpfApplication
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _viewModel;

        public MainWindow()
        {
            if (_viewModel == null)
            {
                _viewModel = new MainWindowViewModel();
            }

            this.DataContext = _viewModel;
            InitializeComponent();
        }
    }
}
