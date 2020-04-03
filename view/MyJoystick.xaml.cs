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
        public MyJoystick()
        {
            InitializeComponent();
        }
        private void joystickDirection()
        {
            
        }

        private void Joy_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                rudderVal.Content = Joy.X / 85;
                elevatorVal.Content = -Joy.Y / 85;
            }
        }
    }
}
