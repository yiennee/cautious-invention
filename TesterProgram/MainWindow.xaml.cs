using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TesterProgram
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as MainViewModel;

            viewModel.RunLog.CollectionChanged += (p, q) =>
            {
                // Scroll to the last item whenever a new item is added
                if (q.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                {
                    logListBox.ScrollIntoView(logListBox.Items[logListBox.Items.Count - 1]);
                }
            };
        }
    }
}
