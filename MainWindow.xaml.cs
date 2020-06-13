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

namespace SQLFirstTutorial
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

        Point lastPoint;
        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                if (((e.GetPosition(this).X > 91)&&(e.GetPosition(this).X<327))&&((e.GetPosition(this).Y>148) && (e.GetPosition(this).Y<194))|| (e.GetPosition(this).Y > 231) && (e.GetPosition(this).Y < 277))
                {

                }
                else
                {
                    this.Left += e.GetPosition(this).X - lastPoint.X;
                    this.Top += e.GetPosition(this).Y - lastPoint.Y;
                }
            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lastPoint = new Point(e.GetPosition(this).X, e.GetPosition(this).Y);
        }
    }
}
