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
    /// Interaction logic for Joystick.xaml
    /// </summary>
    public partial class Joystick : UserControl
    {
        private Point MouseDownLocation = new Point(0,0);
        public double X
        {
            get { return (double)GetValue(x); }
            set { SetValue(x, value); }
        }

        public static readonly DependencyProperty x =
            DependencyProperty.Register("X", typeof(double), typeof(Joystick));
        public double Y
        {
            get { return (double)GetValue(y); }
            set { SetValue(y, value); }
        }

        public static readonly DependencyProperty y =
            DependencyProperty.Register("Y", typeof(double), typeof(Joystick));

        public Joystick()
        {
            InitializeComponent();
        }
        private void Knob_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                MouseDownLocation.X = e.GetPosition(this).X;
                MouseDownLocation.Y = e.GetPosition(this).Y;
            }

        }

        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                double black = blackZone.Width / 2;
                double x1 = e.GetPosition(this).X - MouseDownLocation.X;
                double y1 = e.GetPosition(this).Y - MouseDownLocation.Y;
                double inLim = Math.Sqrt(x1 * x1 + y1 * y1);

                if (inLim < black)
                {
                    knobPosition.X = x1;
                    knobPosition.Y = y1;

                    X = knobPosition.X;
                    Y = knobPosition.Y;
                }
                else
                {
                    if (x1 == 0)
                    {
                        knobPosition.Y = black;
                        if (y1 < 0)
                        {
                            knobPosition.Y *= -1;
                        }

                        X = knobPosition.X;
                        Y = knobPosition.Y;

                        return;
                    }

                    double m = y1 / x1;
                    double a = m * m + 1;
                    double b = 2 * m * y1 - 2 * m * m * x1;
                    double c = m * m * x1 * x1 - 2 * m * y1 * x1 + y1 * y1 - black * black;

                    if (x1 > 0)
                    {
                        knobPosition.X = (-b + Math.Sqrt(b * b - 4 * a * c)) / (2 * a);
                    }
                    else
                    {
                        knobPosition.X = (-b - Math.Sqrt(b * b - 4 * a * c)) / (2 * a);
                    }

                    knobPosition.Y = m * (knobPosition.X - x1) + y1;

                    X = knobPosition.X;
                    Y = knobPosition.Y;
                }
            }
        }

        private void Knob_MouseUp(object sender, MouseButtonEventArgs e)
        {
            knobPosition.X = 0;
            knobPosition.Y = 0;

            X = knobPosition.X;
            Y = knobPosition.Y;
        }

        private void centerKnob_Completed(object sender, EventArgs e)
        {

        }
    }
}
