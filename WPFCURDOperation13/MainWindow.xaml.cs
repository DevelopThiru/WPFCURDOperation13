using Microsoft.Data.SqlClient;
using System.Data;

using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace WPFCURDOperation13
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            loadData();
        }
       
        SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-97UNCBB\SQLEXPRESS;Initial Catalog=CurdOperation;Integrated Security=True;TrustServerCertificate=True");



        public void CelarData()
        {
            txt_ID.Clear();
            txt_FirstName.Clear();
            txt_LastName.Clear();
            txt_Email.Clear();
            txt_Passwod.Clear();
            txt_Age.Clear();
            txt_city.Clear();
        }


        public void loadData()
        {
            SqlCommand command = new SqlCommand("select * from WPFCurd ", connection);
            DataTable dt = new DataTable();
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            dt.Load(reader);
            connection.Close();

            dataGrid.ItemsSource = dt.DefaultView;
        }

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            CelarData();
        }

        public bool IsVaild()
        {
            if (txt_FirstName.Text == string.Empty)
            {
                MessageBox.Show("FirstName is Required", "Field", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txt_LastName.Text == string.Empty)
            {
                MessageBox.Show("LastName is Required", "Field", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txt_Email.Text == string.Empty)
            {
                MessageBox.Show("Emil is Required", "Field", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txt_Passwod.Text == string.Empty)
            {
                MessageBox.Show("Password is Required", "Field", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txt_Age.Text == string.Empty)
            {
                MessageBox.Show("Age is Required", "Field", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txt_city.Text == string.Empty)
            {
                MessageBox.Show("City is Required", "Field", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }


        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (IsVaild())
                {

                    SqlCommand command = new SqlCommand("insert into WPFCurd values (@FirstName,@LastName,@Email,@Password,@City,@Age)", connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@FirstName", txt_FirstName.Text);
                    command.Parameters.AddWithValue("@LastName", txt_LastName.Text);
                    command.Parameters.AddWithValue("@Email", txt_Email.Text);
                    command.Parameters.AddWithValue("@Password", txt_Passwod.Text);
                    command.Parameters.AddWithValue("@City", txt_city.Text);
                    command.Parameters.AddWithValue("@Age", txt_Age.Text);


                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                    loadData();
                    MessageBox.Show("Data Added Sucessfuylly", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    CelarData();

                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dataGrid.SelectedItem != null)
            {
                DataRowView dataRow = (DataRowView)dataGrid.SelectedItem;
                txt_ID.Text = dataRow["ID"].ToString();
                txt_FirstName.Text = dataRow["FirstName"].ToString();
                txt_LastName.Text = dataRow["LastName"].ToString();
                txt_Email.Text = dataRow["Email"].ToString();
                txt_Passwod.Text = dataRow["Password"].ToString();
                txt_city.Text = dataRow["City"].ToString();
                txt_Age.Text = dataRow["Age"].ToString();

            }
        }

        private void btn_Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("Update WPFCurd set FirstName='" + txt_FirstName.Text + "',LastName='" + txt_LastName.Text + "',Email='" + txt_Email.Text + "',Password='" + txt_Passwod.Text + "',City='" + txt_city.Text + "',Age='" + txt_Age.Text + "' Where ID = "+txt_ID.Text+"",connection);
                command.ExecuteNonQuery();
                MessageBox.Show("Data SuccessFully Updated","Update",MessageBoxButton.OK, MessageBoxImage.Information); 
                connection.Close();
                CelarData();
                loadData();
                connection.Close(); 
            }
            catch(Exception)
            {

            }
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("Delete from WPFCurd where ID = " + txt_ID.Text + "", connection);
                command.ExecuteNonQuery();
                MessageBox.Show("Delete Successfully", "Delete",MessageBoxButton.OK,MessageBoxImage.Information);
                connection.Close();
                CelarData();
                loadData();
                connection.Close();
                
               
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}