using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            DB database = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand($"SELECT * FROM `users` WHERE `login`=@uL AND `pass`=@uP", database.GetConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginBox.Text;
            command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = passwordBox.Password;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if(table.Rows.Count > 0)
            {
                MessageBox.Show("Yes");
            }
            else
            {
                MessageBox.Show("No");
            }
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            this.labelAutorization.Visibility = Visibility.Hidden;
            this.labelRegistration.Visibility = Visibility.Visible;
            this.loginButton.Visibility = Visibility.Hidden;
            this.createAccountButton.Visibility = Visibility.Visible;
            this.backToLoginButton.Visibility = Visibility.Visible;
            this.passwordBox.Visibility = Visibility.Hidden;
            this.createPasswordBox.Visibility = Visibility.Visible;
            this.dhaLabel.Visibility = Visibility.Hidden;
            this.registerButton.Visibility = Visibility.Hidden;
        }

        private void backToLoginButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
