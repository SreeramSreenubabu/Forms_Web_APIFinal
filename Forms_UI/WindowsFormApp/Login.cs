using System;
using System.Windows.Forms;
using WindowsFormApp.BusinessAccessLayer;
using BusinessAccessLayer;
using WindowsFormApp.Logger;
using Logger;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Configuration;

namespace WindowsFormApp
{
    public partial class Login : Form
    {
        public IBAL _BAL = new BAL();
        public ILog _logger = new Log();
        private static readonly HttpClient client = new HttpClient();
        private readonly string _baseUrl = ConfigurationManager.AppSettings["BaseUrl"];
        private readonly string _login = ConfigurationManager.AppSettings["Login.LoginUser"];
        private readonly string _attInsert = ConfigurationManager.AppSettings["Attendance.LoginTime"];

        public Login()
        {
            InitializeComponent();
            txtPassword.PasswordChar = '*';
            this.FormClosing += Dashboard_FormClosing;
        }

        private void Dashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Confirm Exit", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                _logger.SetUsername(Environment.UserName);
                _logger.LoggerMessage("Application closed", 1);
            }
        }

        private async void btn_Login(object sender, EventArgs e)
        {
            string username = txtUserName.Text;
            string password = txtPassword.Text;

            // Validate Email
            if (string.IsNullOrWhiteSpace(username) ||
                !username.Contains("@") ||
                !username.Contains(".") ||
                username.IndexOf('@') > username.LastIndexOf('.'))
            {
                MessageBox.Show("Please enter a valid email address");
                return;
            }

            // Validate Password
            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters long");
                return;
            }

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Username column is empty");
                _logger.LoggerMessage("Login failed: Username column is empty", 2);
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Password column is empty");
                _logger.LoggerMessage("Login failed: Password column is empty", 2);
                return;
            }

            try
            {
                var loginInfo = new { Email = username, Password = password };
                var attendanceInfo = new { Email = username };

                string loginJson = JsonConvert.SerializeObject(loginInfo);
                string AttendanceJson = JsonConvert.SerializeObject(attendanceInfo);

                var loginContent = new StringContent(loginJson, Encoding.UTF8, "application/json");
                var AttendanceContent = new StringContent(AttendanceJson, Encoding.UTF8, "application/json");

                var loginResponse = await client.PostAsync($"{_baseUrl}{_login}", loginContent);
                var attendanceResponse = await client.PostAsync($"{_baseUrl}{_attInsert}", AttendanceContent);
                try
                {
                    if (loginResponse.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Login successful!");
                        _logger.SetUsername(username);
                        _logger.LoggerMessage("Login Successful", 1);
                        _logger.LoggerMessage($"Login Info - User: [{username}]", 1);
                        _logger.LoggerMessage("Login API: " + loginResponse, 1);

                        try
                        {
                            // Insert Attendance
                            if (attendanceResponse.IsSuccessStatusCode)
                            {
                                _logger.LoggerMessage("Attendance Inserted", 1);
                                _logger.LoggerMessage("Insert Attendance API: " + attendanceResponse, 1);
                            }
                            else
                            {
                                MessageBox.Show("Error while inserting attendance");
                                _logger.LoggerMessage("Error while inserting attendance", 3);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error while inserting attendance");
                            _logger.LoggerMessage("Error while inserting attendance: " + ex.Message, 3);
                        }
                        Dashboard dashboard = new Dashboard(username);
                        dashboard.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password.");
                        _logger.LoggerMessage("Login failed: Invalid username or password", 2);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurs while Login: " + ex.Message);
                    _logger.LoggerMessage("Error occurs while Login: " + ex, 3);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Please check the connection", "Connection Lost");
                _logger.LoggerMessage($"{_baseUrl}{_login}" + ex.Message, 3);
            }
        }

        private void btn_Clear(object sender, EventArgs e)
        {
            txtUserName.Text = "";
            txtPassword.Text = "";
        }

        private void btn_Exit(object sender, EventArgs e)
        {
            _logger.SetUsername(Environment.UserName);
            _logger.LoggerMessage("Application Exit Button Clicked", 1);
            this.Close();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            _logger.LoggerMessage($"App Run Env - System Name: [{Environment.MachineName}], System User: [{Environment.UserName}], IP: {_logger.GetLocalIPAddress()}", 1);
        }
    }
}
