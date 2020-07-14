using MySqlX.XDevAPI.Common;
using System;
using System.Windows;


namespace PhoneSpravachnik
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string foundFirstName = "";
        private string foundLastName = "";
        private string foundNumber = "";
        public MainWindow()
        {
            InitializeComponent();
            UpdateListBox();
        }

        private void UpdateListBox()
        {
            DBConnection db = new DBConnection();
            var result = db.SelectQuery("SELECT * FROM phonebook");
            List_Box.Items.Clear();
            while (result.Read())
            {
                string ID = Convert.ToString(result.GetInt32("id")) + ". ";
                string firstName = result.GetString("first_name") + " ";
                string lastName = result.GetString("last_name") + " ";
                string phoneNumber = result.GetString("number");
                List_Box.Items.Add(ID + firstName + lastName + phoneNumber);
            }
            result.Close();
            db.Close();
        }
        private void Button_Click_AddToBase(object sender, RoutedEventArgs e)
        {
            string firstName = first_name.Text;
            string lastName = last_name.Text;
            string phone = phone_number.Text;
            if(firstName == null || firstName.Length < 2 || lastName == null || lastName.Length < 2 || phone == null ||
                phone.Length < 6)
            {
                MessageBox.Show("Некоректные данные.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            DBConnection db = new DBConnection();
            db.InsertQuery($"INSERT INTO phonebook (first_name, last_name, number) VALUES ('{firstName}'," +
                $" '{lastName}', '{phone}')");
            db.Close();
            UpdateListBox();
        }

        private void Button_Click_Searching(object sender, RoutedEventArgs e)
        {
            string phoneNumber = find_phone_number.Text;

            DBConnection db = new DBConnection();
            var result = db.SelectQuery("SELECT id, first_name, last_name FROM phonebook WHERE number = '" + phoneNumber +"'");
            string firstName = "";
            string lastName = "";
            string ID = "";

            bool found = false;
            while (result.Read())
            {
                firstName = result.GetString("first_name");
                lastName = result.GetString("last_name");
                ID = Convert.ToString(result.GetInt32("id"));
                found = true;
                break;
            }
            result.Close();
            db.Close();
            if (!found)
            {
                MessageBox.Show("Ничего не найдено.\nПопробуйте заново.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            label_ID.Visibility = Visibility.Visible;
            label_last_name.Visibility = Visibility.Visible;
            label_name.Visibility = Visibility.Visible;
            label_phone.Visibility = Visibility.Visible;

            ID_DB.Visibility = Visibility.Visible;
            first_name_DB.Visibility = Visibility.Visible;
            last_name_DB.Visibility = Visibility.Visible;
            phone_number_DB.Visibility = Visibility.Visible;

            ID_DB.Text = ID;
            first_name_DB.Text = firstName;
            last_name_DB.Text = lastName;
            phone_number_DB.Text = phoneNumber;

            foundFirstName = firstName;
            foundLastName = lastName;
            foundNumber = phoneNumber;

            CheckBox.Visibility = Visibility.Visible;

        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            int ID = Convert.ToInt32(ID_DB.Text);
            string firstName = first_name_DB.Text;
            string lastName = last_name_DB.Text;
            string number = phone_number_DB.Text;

            if(firstName.Equals(foundFirstName) && lastName.Equals(foundLastName) && number.Equals(foundNumber))
            {
                MessageBox.Show("Вы ничего не изменили.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DBConnection db = new DBConnection();
            db.UpdateQuery($"UPDATE phonebook SET first_name = '{firstName}', last_name = '{lastName}', " +
                $" number = '{number}' WHERE id = {ID}");
            db.Close();
            MessageBox.Show("Данные изменены", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            UpdateListBox();
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            first_name_DB.IsEnabled = true;
            last_name_DB.IsEnabled = true;
            phone_number_DB.IsEnabled = true;
            Save_Button.Visibility = Visibility.Visible;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            first_name_DB.IsEnabled = false;
            last_name_DB.IsEnabled = false;
            phone_number_DB.IsEnabled = false;
            Save_Button.Visibility = Visibility.Hidden;
        }

        
    }
}
