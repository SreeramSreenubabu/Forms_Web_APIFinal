using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;
using WindowsFormApp.BusinessAccessLayer;
using BusinessAccessLayer;
using WindowsFormApp.Logger;
using Logger;
using System.Net.Http;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Text;

namespace WindowsFormApp
{
    public partial class Profile : Form
    {
        private IBAL _BAL = new BAL();
        private ILog _logger = new Log();
        private Login login = new Login();
        private static readonly HttpClient client = new HttpClient();
        private string username;
        private readonly string _baseUrl = ConfigurationManager.AppSettings["BaseUrl"];
        private readonly string _getProfile = ConfigurationManager.AppSettings["Profile.GetProfile"];
        private readonly string _attUpdate = ConfigurationManager.AppSettings["Attendance.LogoutTime"];

        public Profile(string username)
        {
            InitializeComponent();
            LoadProfiles();
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
                    var profileData = new { Email = username };
                    var profileJson = JsonConvert.SerializeObject(profileData);
                    var profileContent = new StringContent(profileJson, Encoding.UTF8, "application/json");

                    // Update Attendance
                    var profileResponse = await client.PutAsync($"{_baseUrl}{_attUpdate}", profileContent);
                    if (profileResponse.IsSuccessStatusCode)
                    {
                        _logger.SetUsername(Environment.UserName);
                        _logger.LoggerMessage("Logout recorded", 1);
                        _logger.LoggerMessage("Logout API: " + profileResponse, 1);
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
                    _logger.LoggerMessage("Error while updating attendance in profile: " + ex.Message, 3);
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
                    dataGridViewProfiles.DataSource = profiles;
                    _logger.LoggerMessage("Profile Get API: " + profilesResponse, 1);
                }
                else
                {
                    MessageBox.Show("Error: " + profilesResponse.ReasonPhrase);
                    _logger.LoggerMessage("Error while fetching profiles: " + profilesResponse.ReasonPhrase, 3);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occurs while Get Profile: " + ex.Message);
                _logger.LoggerMessage("Exception occurs while Get Profile: " + ex.Message, 3);
            }
        }

        private async void btn_Logout(object sender, EventArgs e)
        {
            try
            {
                var profileData = new { Email = username };
                var profileJson = JsonConvert.SerializeObject(profileData);
                var profileContent = new StringContent(profileJson, Encoding.UTF8, "application/json");

                // Update Attendance
                var profileResponse = await client.PutAsync($"{_baseUrl}{_attUpdate}", profileContent);
                if (profileResponse.IsSuccessStatusCode)
                {
                    MessageBox.Show("Logout Successful");
                    _logger.SetUsername(Environment.UserName);
                    _logger.LoggerMessage("Logout recorded", 1);
                    _logger.LoggerMessage("Logout Attendance Update API: " + profileResponse, 1);
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
                _logger.LoggerMessage("Error while updating attendance in profile: " + ex.Message, 3);
            }
        }

        private void btn_Home(object sender, EventArgs e)
        {
            this.Hide();
            _logger.LoggerMessage("Profile closed", 1);
            Dashboard dashboard = new Dashboard(username);
            dashboard.Show();
        }
    }
}
