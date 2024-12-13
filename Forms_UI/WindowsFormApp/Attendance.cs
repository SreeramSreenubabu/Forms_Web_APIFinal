using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Windows.Forms;
using WindowsFormApp.BusinessAccessLayer;
using BusinessAccessLayer;
using WindowsFormApp.Logger;
using Logger;
using System.Net.Http;
using System.Net.Http.Json;
using Newtonsoft.Json;

namespace WindowsFormApp
{
    public partial class Attendance : Form
    {
        private IBAL _BAL = new BAL();
        private ILog _logger = new Log();
        private Login login = new Login();
        private static readonly HttpClient client = new HttpClient();
        private string username;
        private readonly string _baseUrl = ConfigurationManager.AppSettings["BaseUrl"];
        private readonly string _getAttendance = ConfigurationManager.AppSettings["Attendance.GetAttendance"];
        private readonly string _attUpdate = ConfigurationManager.AppSettings["Attendance.LogoutTime"];

        public Attendance(string username)
        {
            InitializeComponent();
            LoadAttendance();
            this.FormClosing += Dashboard_FormClosing;
            this.username = username;
            _logger.SetUsername(username);
        }

        private async void LoadAttendance()
        {
            try
            {
                // Get Attendance
                var attendanceResponse = await client.GetAsync($"{_baseUrl}{_getAttendance}");
                if (attendanceResponse.IsSuccessStatusCode)
                {
                    var attendance = await attendanceResponse.Content.ReadFromJsonAsync<List<AttendanceModel>>();
                    dataGridViewAtt.DataSource = attendance;
                    _logger.LoggerMessage("Get Attendance API: " + attendanceResponse, 1);
                    string workingHours = _BAL.CalculateWorkingHoursForToday(attendance);
                    TotalWorkingHours.Text = $"{workingHours}";
                    PresentDate.Text = "DATE: " + (DateTime.Now).ToString("dd-MM-yyyy");
                }
                else
                {
                    MessageBox.Show("Error: " + attendanceResponse.ReasonPhrase);
                    _logger.LoggerMessage("Error while fetching attendance: " + attendanceResponse.ReasonPhrase, 3);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
                _logger.LoggerMessage("Exception while fetching attendance: " + ex.Message, 3);
            }
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
                        _logger.LoggerMessage("Logout API: " + attendanceResponse, 1);
                    }
                    else
                    {
                        MessageBox.Show("Error while updating attendance");
                        _logger.LoggerMessage("Error while updating attendance", 3);
                    }
                    login.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while updating attendance: " + ex.Message);
                    _logger.LoggerMessage("Error while updating attendance in Dashboard_FormClosing: " + ex.Message, 3);
                }
            }
        }

        private async void btn_Logout(object sender, EventArgs e)
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
                    MessageBox.Show("Logout Successful");
                    _logger.SetUsername(Environment.UserName);
                    _logger.LoggerMessage("Logout recorded", 1);
                    _logger.LoggerMessage("Logout API: " + attendanceResponse, 1);
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
                _logger.LoggerMessage("Error while updating attendance in btn_Logout: " + ex.Message, 3);
            }
        }

        private void btn_Home(object sender, EventArgs e)
        {
            this.Hide();
            _logger.LoggerMessage("Attendance closed", 1);
            Dashboard dashboard = new Dashboard(username);
            dashboard.Show();
        }
    }
}