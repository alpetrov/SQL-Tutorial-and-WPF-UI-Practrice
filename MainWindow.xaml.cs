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
        
        string currentLogin;

        Dictionary<string, List<UIElement>> formElements;

        List<User> users = new List<User>();
        int currentPage = 1;
        const int EPP = 9;

        public MainWindow()
        {
            InitializeComponent();
            formElements = SetGroups();
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
                currentLogin = loginBox.Text;
            }
            else
            {
                MessageBox.Show("Wrong username/password");
                return;
            }

            CloseLoginForm();
            OpenMainMenuForm();

            SetDefaultValues();
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            CloseLoginForm();
            OpenRegistrationForm();

            SetDefaultValues();

            this.Width = 720;
        }

        private void backToLoginButton_Click(object sender, RoutedEventArgs e)
        {
            CloseRegistrationForm();
            OpenLoginForm();

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
                MessageBox.Show("Account created.");

                CloseRegistrationForm();
                OpenLoginForm();

                SetDefaultValues();

                this.Width = 400;
            }
            else
            {
                MessageBox.Show("Account hasn't been created");
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

        private void deleteAccount_Click(object sender, RoutedEventArgs e)
        {
            string messageBoxText = $"Do you want to delete your account with username \"{currentLogin}\"?";
            string caption = "Delete my account";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
            if (result == MessageBoxResult.No)
            {
                return;
            }

            DB database = new DB();

            MySqlCommand command = new MySqlCommand("DELETE FROM `users` WHERE login=@uL", database.GetConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = currentLogin;

            database.OpenConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Account deleted...");
            }

            database.CloseConnection();

            currentLogin = null;

            CloseMainMenuForm();
            OpenLoginForm();

            SetDefaultValues();
        }

        void SetDefaultValues()
        {
            loginBox.Text = "Login";
            createPasswordBox.Text = "Password";
            passwordBox.Password = "Password";
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
        }

        Dictionary<string, List<UIElement>> SetGroups()
        {
            Dictionary<string, List<UIElement>> elements = new Dictionary<string, List<UIElement>>();

            List<UIElement> loginForm = new List<UIElement>();
            loginForm.Add(loginBox);
            loginForm.Add(passwordBox);
            loginForm.Add(loginButton);
            loginForm.Add(dhaLabel);
            loginForm.Add(labelAutorization);
            loginForm.Add(registerButton);
            loginForm.Add(userIcon);
            loginForm.Add(lockIcon);
            elements.Add("loginForm", loginForm);

            List<UIElement> registrationForm = new List<UIElement>();
            registrationForm.Add(loginBox);
            registrationForm.Add(userIcon);
            registrationForm.Add(lockIcon);
            registrationForm.Add(createPasswordBox);
            registrationForm.Add(createAccountButton);
            registrationForm.Add(labelRegistration);
            registrationForm.Add(backToLoginButton);
            registrationForm.Add(nameLabel);
            registrationForm.Add(surnameLabel);
            elements.Add("registrationForm", registrationForm);

            List<UIElement> mainMenuForm = new List<UIElement>();
            mainMenuForm.Add(mainMenuLabel);
            mainMenuForm.Add(deleteAccountButton);
            mainMenuForm.Add(successLabel);
            mainMenuForm.Add(seeUsersList_Button);
            elements.Add("mainMenuForm", mainMenuForm);

            List<UIElement> usersListForm = new List<UIElement>();
            usersListForm.Add(usersListGrid);
            usersListForm.Add(usersListLabel);
            usersListForm.Add(pageNumberLabel);
            usersListForm.Add(previousPageButton);
            usersListForm.Add(nextPageButton);
            elements.Add("usersListForm", usersListForm);

            return elements;
        }

        void OpenUsersListForm()
        {
            foreach (var item in formElements["usersListForm"])
            {
                item.Visibility = Visibility.Visible;
            }
        }

        void CloseUsersListForm()
        {
            foreach (var item in formElements["usersListForm"])
            {
                item.Visibility = Visibility.Hidden;
            }
        }

        void OpenLoginForm()
        {
            foreach (var item in formElements["loginForm"])
            {
                item.Visibility = Visibility.Visible;
            }
        }

        void CloseLoginForm()
        {
            foreach (var item in formElements["loginForm"])
            {
                item.Visibility = Visibility.Hidden;
            }
        }

        void OpenRegistrationForm()
        {
            foreach (var item in formElements["registrationForm"])
            {
                item.Visibility = Visibility.Visible;
            }
        }

        void CloseRegistrationForm()
        {
            foreach (var item in formElements["registrationForm"])
            {
                item.Visibility = Visibility.Hidden;
            }
        }

        void OpenMainMenuForm()
        {
            foreach (var item in formElements["mainMenuForm"])
            {
                item.Visibility = Visibility.Visible;
            }
        }

        void CloseMainMenuForm()
        {
            foreach (var item in formElements["mainMenuForm"])
            {
                item.Visibility = Visibility.Hidden;
            }
        }

        private void seeUsersListButton_Click(object sender, RoutedEventArgs e)
        {
            CloseMainMenuForm();
            OpenUsersListForm();

            DB database = new DB();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable dataTable = new DataTable();

            MySqlCommand command = new MySqlCommand("SELECT `id` AS 'ID', `login` AS `Username` FROM `users`", database.GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(dataTable);

            users.Clear();
            foreach (var row in dataTable.Rows)
            {
                User user = (User)Activator.CreateInstance(typeof(User), row);
                users.Add(user);
            }

            usersListGrid.DataContext = users.GetRange(0, EPP);
        }

        private void nextPageButton_Click(object sender, RoutedEventArgs e)
        {
            if((currentPage+1) * EPP - users.Count > 8) { return; }
            currentPage++;
            pageNumberLabel.Content = $"Page number: {currentPage}";
            if (currentPage * EPP <= users.Count)
            {
                usersListGrid.DataContext = users.GetRange((currentPage - 1) * EPP, currentPage * EPP);
            }
            else
            {
                usersListGrid.DataContext = users.Skip(Math.Max(0, (currentPage-1) * EPP));
            }
        }

        private void previousPageButton_Click(object sender, RoutedEventArgs e)
        {
            if(currentPage!=1) currentPage--;
            pageNumberLabel.Content = $"Page number: {currentPage}";
            usersListGrid.DataContext = users.GetRange((currentPage - 1) * EPP, currentPage * EPP);
        }
    }
}
