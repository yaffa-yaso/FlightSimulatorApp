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
using System.Net.Sockets;



namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FlightViewModel vm;
        string address = "127.0.0.1";
        int PORT = 5402;
        Mutex M;

        public MainWindow()
        {
            InitializeComponent();
            this.vm = (Application.Current as App).FlightViewModel;




        M = new Mutex(false, "mutex");

            this.DataContext = vm;
            this.screen.DataContext = vm;
            this.board.DataContext = vm;
            

        }
        

        private  async void Button_Click(object sender, RoutedEventArgs e)
        {
            this.ligth.Source = images / red.png;
            await Task.Run(() =>
            {
                vm.getModel().connect(vm.getModel().Address, vm.getModel().Port);
            });
            if (vm.getModel().isConnected() == true)
            {

                vm.getModel().start();
            }
        }
        
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            vm.getModel().disConnect();
        }


        private void myJoystick_MouseMove(object sender, MouseEventArgs e)
        {
            if (myJoystick.KnobChanged)
            {
                vm.movePlain(myJoystick.Rudder, myJoystick.Elevator);
                myJoystick.KnobChanged = false;
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            address = addressBox.Text;
            PORT = Int32.Parse(ipBox.Text);              
            
        }

        private void addressBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
