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
        public double Rudder
        {
            get { return (double)Joy.X / 85; }
            set { SetValue(rudderP, value); }
        }
        public static readonly DependencyProperty rudderP =
            DependencyProperty.Register("Rudder", typeof(string), typeof(MyJoystick));
         
        public double Elevator
        {
            get { return (double)-Joy.Y / 85; }
            set { SetValue(elevatorP, value); }
        }
        public static readonly DependencyProperty elevatorP =
            DependencyProperty.Register("Elevator", typeof(string), typeof(MyJoystick));

        public static readonly DependencyProperty knobChanged =
     DependencyProperty.Register("KnobChanged", typeof(bool), typeof(MyJoystick));

        public bool KnobChanged
        {
            get { return (bool)GetValue(knobChanged); }
            set { SetValue(knobChanged, value); }
        }

        public MyJoystick()
        {
            InitializeComponent();
        }

        private void Joy_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                RudderVal.Content = Joy.X / 85;
                ElevatorVal.Content = -Joy.Y / 85;
                KnobChanged = true;
            }
        }

        private void Joy_MouseUp(object sender, MouseButtonEventArgs e)
        {
            RudderVal.Content = Joy.X;
            ElevatorVal.Content = -Joy.Y;
            KnobChanged = true;
        }
    }
}
