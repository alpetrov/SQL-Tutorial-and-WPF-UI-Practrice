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
        bool loginChanged;
        bool passwordChanged;
        bool nameChanged;
        bool surnameChanged;

        public MainWindow()
        {
            InitializeComponent();
            this.Width = 400;
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
            this.surnameLabel.Visibility = Visibility.Visible;
            this.nameLabel.Visibility = Visibility.Visible;

            this.Width = 720;
        }

        private void backToLoginButton_Click(object sender, RoutedEventArgs e)
        {
            this.labelAutorization.Visibility = Visibility.Visible;
            this.labelRegistration.Visibility = Visibility.Hidden;
            this.loginButton.Visibility = Visibility.Visible;
            this.createAccountButton.Visibility = Visibility.Hidden;
            this.backToLoginButton.Visibility = Visibility.Hidden;
            this.passwordBox.Visibility = Visibility.Visible;
            this.createPasswordBox.Visibility = Visibility.Hidden;
            this.dhaLabel.Visibility = Visibility.Visible;
            this.registerButton.Visibility = Visibility.Visible;
            this.surnameLabel.Visibility = Visibility.Hidden;
            this.nameLabel.Visibility = Visibility.Hidden;

            this.Width = 400;
        }

        private void createAccountButton_Click(object sender, RoutedEventArgs e)
        {

        }


        private void loginBox_KeyDown(object sender, KeyEventArgs e)
        {
            loginChanged = true;
            loginBox.SetCurrentValue(ForegroundProperty, Brushes.Black);
        }

        private void loginBox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!loginChanged)
            {
                if (this.loginBox.Text == "")
                {
                    this.loginBox.Text = "Login";
                }
            }
        }

        private void loginBox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!loginChanged)
            {
                this.loginBox.Text = "";
            }
        }

        private void passwordBox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!passwordChanged)
            {
                if (this.passwordBox.Password == "")
                {
                    this.passwordBox.Password = "password";
                }
            }
        }

        private void passwordBox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!passwordChanged)
            {
                this.passwordBox.Password = "";
            }
        }

        private void passwordBox_KeyDown(object sender, KeyEventArgs e)
        {
            passwordChanged = true;
            passwordBox.SetCurrentValue(ForegroundProperty, Brushes.Black);
        }

        private void nameBox_KeyDown(object sender, KeyEventArgs e)
        {
            nameChanged = true;
            nameBox.SetCurrentValue(ForegroundProperty, Brushes.Black);
        }

        private void nameBox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!nameChanged)
            {
                this.nameBox.Text = "";
            }
        }

        private void nameBox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!nameChanged)
            {
                if (this.nameBox.Text == "")
                {
                    this.nameBox.Text = "Name";
                }
            }
        }

        private void surnameBox_KeyDown(object sender, KeyEventArgs e)
        {
            surnameChanged = true;
            surnameBox.SetCurrentValue(ForegroundProperty, Brushes.Black);
        }

        private void surnameBox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!surnameChanged)
            {
                this.surnameBox.Text = "";
            }
        }

        private void surnameBox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!surnameChanged)
            {
                if (this.surnameBox.Text == "")
                {
                    this.surnameBox.Text = "Surname";
                }
            }
        }

        private void createPasswordBox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!passwordChanged)
            {
                this.createPasswordBox.Text = "";
            }
        }

        private void createPasswordBox_MouseLeave(object sender, MouseEventArgs e)
        {
            if(!passwordChanged)
            {
                if (this.createPasswordBox.Text == "")
                {
                    this.createPasswordBox.Text = "Surname";
                }
            }
        }

        private void createPasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            passwordChanged = true;
            createPasswordBox.SetCurrentValue(ForegroundProperty, Brushes.Black);
        }
    }
}
