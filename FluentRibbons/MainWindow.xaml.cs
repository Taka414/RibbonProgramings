using Fluent;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace Ribbons.Sample
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        public MainWindow()
        {
            this.InitializeComponent();

            var vm = new MainWindowViewModel();

            var folder = new Folder() { Text = "111" };
            folder.Children.Add(new Folder() { Text = "AAA" });
            folder.Children.Add(new Folder() { Text = "BBB" });
            folder.Children.Add(new Folder() { Text = "CCC" });

            vm.RootFolders.Add(folder);
            vm.RootFolders.Add(new Folder() { Text = "222" });
            vm.RootFolders.Add(new Folder() { Text = "333" });

            this.DataContext = vm;
        }

        // コンテキストタブの表示切替
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.tabGroup1.Visibility == Visibility.Visible)
            {
                this.tabGroup1.Visibility = Visibility.Collapsed;
                this.tabGroup2.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.tabGroup1.Visibility = Visibility.Visible;
                this.tabGroup2.Visibility = Visibility.Visible;
            }
        }

        private void RibbonGroupBox_LauncherClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hi", "Dialog");
        }
    }

    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Folder> RootFolders { get; private set; } = new ObservableCollection<Folder>();

        public Color[] ThemeColors { get; } = { Colors.Red, Colors.Green, Colors.Blue, Colors.White, Colors.Black, Colors.Purple };
    }

    public class Folder : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public BitmapImage Image
        {
            get
            {
                return new BitmapImage(new Uri("/assets/icons8-Folder-x16.png", UriKind.Relative));
            }
        }

        private string _Text;
        public string Text
        {
            get
            {
                return this._Text;
            }
            set
            {
                this._Text = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Text)));
            }
        }
        
        public ObservableCollection<Folder> Children { get; private set; } = new ObservableCollection<Folder>();
    }
}
