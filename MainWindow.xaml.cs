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
        bool passwordCreationChanged;
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
                if ((e.GetPosition(this).X > 91)&&(e.GetPosition(this).X<327)&&((e.GetPosition(this).Y>148) && (e.GetPosition(this).Y<194) || (e.GetPosition(this).Y > 231) && (e.GetPosition(this).Y < 277)))
                {

                }
                else if ((e.GetPosition(this).X > 448) && (e.GetPosition(this).X < 684) && ((e.GetPosition(this).Y > 148) && (e.GetPosition(this).Y < 194)) || (e.GetPosition(this).Y > 231) && (e.GetPosition(this).Y < 277))
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
            bool loginInput = true;
            bool passwordInput = true;
            if ((loginBox.Text == "Login")|| (loginBox.Text == ""))
            {
                loginInput = false;
            }
            if ((passwordBox.Password == "Password") || (passwordBox.Password == ""))
            {
                passwordInput = false;
            }
            if (!(loginInput && passwordInput))
            {
                string message = "The following fields are empty:\n";
                message += $"\n{(loginInput ? "" : "Login")}";
                message += $"\n{(passwordInput ? "" : "Password")}";
                MessageBox.Show(message);
                return;
            }

            DB database = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login`=@uL AND `pass`=@uP", database.GetConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginBox.Text;
            command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = passwordBox.Password;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if(table.Rows.Count > 0)
            {
                MessageBox.Show("Signed in!");
            }
            else
            {
                MessageBox.Show("Wrong username/password");
                return;
            }

            this.successLabel.Visibility = Visibility.Visible;
            this.labelAutorization.Visibility = Visibility.Hidden;
            this.mainMenuLabel.Visibility = Visibility.Visible;
            this.loginBox.Visibility = Visibility.Hidden;
            this.passwordBox.Visibility = Visibility.Hidden;
            this.userIcon.Visibility = Visibility.Hidden;
            this.lockIcon.Visibility = Visibility.Hidden;
            this.loginButton.Visibility = Visibility.Hidden;
            this.registerButton.Visibility = Visibility.Hidden;
            this.dhaLabel.Visibility = Visibility.Hidden;
            this.deleteAccount_Button.Visibility = Visibility.Visible;
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
            bool loginInput = true;
            bool passwordInput = true;
            bool nameInput = true;
            bool surnameInput = true;

            if ((loginBox.Text == "Login") || (loginBox.Text == ""))
            {
                loginInput = false;
            }
            if ((createPasswordBox.Text == "Password") || (createPasswordBox.Text == ""))
            {
                passwordInput = false;
            }
            if ((nameBox.Text == "Name") || (nameBox.Text == ""))
            {
                nameInput = false;
            }
            if ((surnameBox.Text == "Surname") || (surnameBox.Text == ""))
            {
                surnameInput = false;
            }
            if(!(loginInput&& passwordInput && nameInput && surnameInput))
            {
                string message = "The following fields are empty:\n";
                message += $"\n{(loginInput?"":"Login")}";
                message += $"\n{(passwordInput ? "":"Password")}";
                message += $"\n{(nameInput ? "":"Name")}";
                message += $"\n{(surnameInput ? "":"Surname")}";
                MessageBox.Show(message);
                return;
            }

            if (isUserExists())
            {
                return;
            }

            DB database = new DB();

            MySqlCommand command = new MySqlCommand("INSERT INTO `users`(`login`, `pass`, `name`, `surname`) VALUES (@uL, @uP, @uN, @uS)", database.GetConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginBox.Text;
            command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = createPasswordBox.Text;
            command.Parameters.Add("@uN", MySqlDbType.VarChar).Value = nameBox.Text;
            command.Parameters.Add("@uS", MySqlDbType.VarChar).Value = surnameBox.Text;

            database.OpenConnection();

            if(command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Аккаунт создан.");

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

                loginBox.Text = "Login";
                createPasswordBox.Text = "Password";
                nameBox.Text = "Name";
                surnameBox.Text = "Surname";
                loginChanged = false;
                passwordChanged = false;
                passwordCreationChanged = false;
                nameChanged = false;
                surnameChanged = false;
                loginBox.SetCurrentValue(ForegroundProperty, Brushes.Gray);
                passwordBox.SetCurrentValue(ForegroundProperty, Brushes.Gray);
                createPasswordBox.SetCurrentValue(ForegroundProperty, Brushes.Gray);
                nameBox.SetCurrentValue(ForegroundProperty, Brushes.Gray);
                surnameBox.SetCurrentValue(ForegroundProperty, Brushes.Gray);

                this.Width = 400;
            }
            else
            {
                MessageBox.Show("Аккаунт не создан");
            }

            database.CloseConnection();
        }

        public bool isUserExists()
        {
            DB database = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login`=@uL", database.GetConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginBox.Text;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("This login already exists. Please, Input other login.");
                return true;
            }
            else
            {
                return false;
            }
        }


        private void loginBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (!loginChanged)
            {
                loginBox.Text = "";
            } 
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
                    this.passwordBox.Password = "Password";
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
            if (!passwordChanged)
            {
                passwordBox.Password = "";
            }
            passwordChanged = true;
            passwordBox.SetCurrentValue(ForegroundProperty, Brushes.Black);
        }

        private void nameBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (!nameChanged)
            {
                nameBox.Text = "";
            }
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
            if (!surnameChanged)
            {
                surnameBox.Text = "";
            }
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
            if (!passwordCreationChanged)
            {
                this.createPasswordBox.Text = "";
            }
        }

        private void createPasswordBox_MouseLeave(object sender, MouseEventArgs e)
        {
            if(!passwordCreationChanged)
            {
                if (this.createPasswordBox.Text == "")
                {
                    this.createPasswordBox.Text = "Password";
                }
            }
        }

        private void createPasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (!passwordCreationChanged)
            {
                createPasswordBox.Text = "";
            }
            passwordCreationChanged = true;
            createPasswordBox.SetCurrentValue(ForegroundProperty, Brushes.Black);
        }
    }
}
