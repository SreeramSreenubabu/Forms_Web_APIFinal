using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Configuration;
using WindowsFormApp.BusinessAccessLayer;
using BusinessAccessLayer;
using WindowsFormApp.Logger;
using Logger;
using System.Net.Http;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Text;
using Azure;

namespace WindowsFormApp
{
    public partial class Dashboard : Form
    {
        private IBAL _BAL = new BAL();
        private ILog _logger = new Log();
        private Login login = new Login();
        private static readonly HttpClient client = new HttpClient();
        private string username;
        private readonly string _baseUrl = ConfigurationManager.AppSettings["BaseUrl"];
        private readonly string _getProfile = ConfigurationManager.AppSettings["Profile.GetProfile"];
        private readonly string _getAttendance = ConfigurationManager.AppSettings["Attendance.GetAttendance"];
        private readonly string _attInsert = ConfigurationManager.AppSettings["Attendance.LoginTime"];
        private readonly string _attUpdate = ConfigurationManager.AppSettings["Attendance.LogoutTime"];

        public Dashboard(string username)
        {
            InitializeComponent();
            LoadProfiles();
            LoadAttendance();
            this.FormClosing += Dashboard_FormClosing;
            this.username = username;
            _logger.SetUsername(username);
        }

        private async void Dashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Confirm Exit", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                try
                {
                    var attendanceData = new { Email = username };
                    var attendanceJson = JsonConvert.SerializeObject(attendanceData);
                    var attendanceContent = new StringContent(attendanceJson, Encoding.UTF8, "application/json");

                    // Update Attendance
                    var attendanceResponse = await client.PutAsync($"{_baseUrl}{_attUpdate}", attendanceContent);
                    if (attendanceResponse.IsSuccessStatusCode)
                    {
                        _logger.SetUsername(Environment.UserName);
                        _logger.LoggerMessage("Logout recorded", 1);
                        _logger.LoggerMessage("Logout Attendance Update API: " + attendanceResponse, 1);

                    }
                    else
                    {
                        MessageBox.Show("Error while updating attendance");
                        _logger.LoggerMessage("Error while updating attendance", 3);
                    }
                    login.Show();
                    _logger.SetUsername(Environment.UserName);
                    _logger.LoggerMessage("Logout Recorder and Attendance Updated", 1);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while updating attendance: " + ex.Message);
                    _logger.LoggerMessage("Error while updating attendance in dashboard: " + ex.Message, 3);
                }
            }
        }

        private async void LoadProfiles()
        {
            try
            {
                // Get Profile
                var profilesResponse = await client.GetAsync($"{_baseUrl}{_getProfile}");
                if (profilesResponse.IsSuccessStatusCode)
                {
                    var profiles = await profilesResponse.Content.ReadFromJsonAsync<List<ProfileModel>>();
                    dataGridViewProfilesDash.DataSource = profiles;
                    _logger.LoggerMessage("Profile Get API: " + profilesResponse, 1);
                }
                else
                {
                    MessageBox.Show("Error: " + profilesResponse.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                _logger.LoggerMessage("Exception occurs when Get Profile in Dashboard: " + ex.Message, 3);
                MessageBox.Show("Exception occurs when Get Profile in Dashboard: " + ex.Message);
            }
        }

        private async void LoadAttendance()
        {
            try
            {
                // Get Attendance
                var attendanceResponse = await client.GetAsync($"{_baseUrl}{_getAttendance}");
                if (attendanceResponse.IsSuccessStatusCode)
                {
                    var attendances = await attendanceResponse.Content.ReadFromJsonAsync<List<AttendanceModel>>();
                    dataGridViewAttDash.DataSource = attendances;
                    _logger.LoggerMessage("Attendance Get API: " + attendanceResponse, 1);
                }
                else
                {
                    MessageBox.Show("Error: " + attendanceResponse.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                _logger.LoggerMessage("Exception occurs when Get Attendance in Dashboard: " + ex.Message, 3);
                MessageBox.Show("Exception occurs when Get Attendance in Dashboard: " + ex.Message);
            }
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            _logger.LoggerMessage("Dashboard opened", 1);
        }

        private void grid_Profile(object sender, DataGridViewCellEventArgs e)
        {
            Profile profileForm = new Profile(username);
            profileForm.Show();
            _logger.LoggerMessage("Profile opened", 1);
            this.Hide();
        }

        private void dataGridViewAtt_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Attendance attendanceModel = new Attendance(username);
            attendanceModel.Show();
            _logger.LoggerMessage("Attendance opened", 1);
            this.Hide();
        }

        private async void btn_Logout(object sender, EventArgs e)
        {
            var attendanceData = new { Email = username };
            var attendanceJson = JsonConvert.SerializeObject(attendanceData);
            var attendanceContent = new StringContent(attendanceJson, Encoding.UTF8, "application/json");
            try
            {
                // Update Attendance
                var attendanceResponse = await client.PutAsync($"{_baseUrl}{_attUpdate}", attendanceContent);
                if (attendanceResponse.IsSuccessStatusCode)
                {
                    MessageBox.Show("Logout Successful");
                    _logger.SetUsername(Environment.UserName);
                    _logger.LoggerMessage("Logout recorded", 1);
                    _logger.LoggerMessage("Logout Attendance Update API: " + attendanceResponse, 1);
                }
                else
                {
                    MessageBox.Show("Error while updating attendance");
                    _logger.LoggerMessage("Error while updating attendance", 3);
                }
                this.Hide();
                login.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while updating attendance: " + ex.Message);
                _logger.LoggerMessage("Error while updating attendance in dashboard: " + ex.Message, 3);
            }
        }
    }
}
