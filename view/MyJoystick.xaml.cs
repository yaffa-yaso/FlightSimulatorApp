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

namespace FlightSimulatorApp.view
{
    /// <summary>
    /// Interaction logic for MyJoystick.xaml
    /// </summary>
    public partial class MyJoystick : UserControl
    {
        public double Throttle
        {
            get { return (double)throttleVal.Content; }
            set { SetValue(throttleP, value); }
        }

        public static readonly DependencyProperty throttleP =
            DependencyProperty.Register("Throttle", typeof(double), typeof(MyJoystick));
       public double Aileron
        {
            get { return (double)aileronVal.Content; }
            set { SetValue(aileronP, value); }
        }

        public static readonly DependencyProperty aileronP =
            DependencyProperty.Register("Aileron", typeof(double), typeof(MyJoystick));
        public double Rudder
        {
            get { return (double)Joy.X / 85; }
            set { SetValue(rudderP, value); }
        }

        public static readonly DependencyProperty rudderP =
            DependencyProperty.Register("Rudder", typeof(string), typeof(MyJoystick));
         public double Elevator
        {
            get { return (double)Joy.Y / 85; }
            set { SetValue(elevatorP, value); }
        }

        public static readonly DependencyProperty elevatorP =
            DependencyProperty.Register("Elevator", typeof(string), typeof(MyJoystick));
        public MyJoystick()
        {
            InitializeComponent();
        }

        private void Joy_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                rudderVal.Content = Joy.X / 85;
                elevatorVal.Content = -Joy.Y / 85;
            }
        }

        private void Joy_MouseUp(object sender, MouseButtonEventArgs e)
        {
            rudderVal.Content = Joy.X;
            elevatorVal.Content = -Joy.Y;
        }
    }
}
