using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FlightViewModel vm;
        public MainWindow()
        {
            InitializeComponent();
          
            vm = new FlightViewModel(new MyFlightModel(new MyTelnetClient()));

            vm.getModel().connect("127.0.0.1", 5402);
            while (vm.getModel().isConnected() == false) { }
            vm.getModel().start();
            DataContext = vm;
            this.screen.DataContext = vm;
            this.board.DataContext = vm;
            

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            vm.getModel().connect("127.0.0.1", 5402);
        }
        
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            vm.getModel().disConnect();
        }

        private void MyJoystick_Loaded(object sender, RoutedEventArgs e)
        {
            vm.movePlain(myJoystick.Rudder, myJoystick.Elevator);
        }
    }
}
