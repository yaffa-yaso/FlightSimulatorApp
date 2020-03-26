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

            this.Loaded += (s, e) =>
            {
                Mouse.Capture(Knob);
            };
            /// Knob.PreviewMouseDown += Knob_MouseUp;
        }

        private Point MouseDownLocation = new Point();

        private void Knob_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) {
                MouseDownLocation.X = e.GetPosition(this).X;
                MouseDownLocation.Y = e.GetPosition(this).Y;
            }

        }

        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                double black = blackZone.Width / 2;
                double x = e.GetPosition(this).X - MouseDownLocation.X;
                double y = e.GetPosition(this).Y - MouseDownLocation.Y;
                double inLim = Math.Sqrt(x * x + y * y);

                if (inLim < black)
                {
                    knobPosition.X = x;
                    knobPosition.Y = y;
                }
                else if (x < black && x > -black)
                {
                    knobPosition.X = x;
                    if (knobPosition.Y >= 0)
                    {
                        knobPosition.Y = Math.Sqrt(black * black - x * x);
                    }
                    else
                    {
                        knobPosition.Y = -Math.Sqrt(black * black - x * x);
                    }
                }
                else if (y < black && y > -black)
                {
                    knobPosition.Y = y;
                    if (knobPosition.X >= 0)
                    {
                        knobPosition.X = Math.Sqrt(black * black - y * y);
                    }
                    else
                    {
                        knobPosition.X = -Math.Sqrt(black * black - y * y);
                    }
                }
            }
            joystickDirection();
        }

        private void Knob_MouseUp(object sender, MouseButtonEventArgs e)
        {
            knobPosition.X = 0;
            knobPosition.Y = 0;

            joystickDirection();
        }

        private void centerKnob_Completed(object sender, EventArgs e){ }

        private void joystickDirection() {
            rudderVal.Content = knobPosition.X / blackZone.Width * 2;
            elevatorVal.Content = -knobPosition.Y / blackZone.Width * 2; 
        }
    }
}
