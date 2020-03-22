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

namespace FlightSimulatorApp.controls
{
    /// <summary>
    /// Interaction logic for Joystick.xaml
    /// </summary>
    public partial class Joystick : UserControl
    {
        public Joystick()
        {
            InitializeComponent();
        }

        private void centerKnob_Completed(object sender, RoutedEventArgs e){}

        private Point MouseDownLocation;

        private void Knob_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) {
                MouseDownLocation = e.ButtonState;
            }

        }

        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void Knob_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
