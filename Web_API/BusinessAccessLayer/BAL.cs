using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WindowsFormApp.DataAccessLayer;
using BusinessAccessLayer;
using WindowsFormApp.Logger;
using Logger;

namespace WindowsFormApp.BusinessAccessLayer
{
    public class BAL : IBAL
    {
        private DAL DAL_Obj = new DAL();
        private ILog _logger = new Log();

        // LOGIN
        public UserModel Login(string email, string password)
        {
            try
            {
                return DAL_Obj.LoginValid(email, password);
            }
            catch (Exception ex)
            {
                _logger.LoggerMessage("Error occurs in Login (BAL.cs): " + ex.Message, 3);
                return null;
            }
        }

        // PROFILE
        public List<ProfileModel> GetProfiles()
        {
            try
            {
                return DAL_Obj.GetAllProfiles();
            }
            catch (Exception ex)
            {
                _logger.LoggerMessage("Error occurs in GetProfiles (BAL.cs): " + ex.Message, 3);
                return new List<ProfileModel>();
            }
        }

        // ATTENDANCE
        public List<AttendanceModel> GetAttendance()
        {
            try
            {
                return DAL_Obj.GetAllAttendance();
            }
            catch (Exception ex)
            {
                _logger.LoggerMessage("Error occurs in GetAttendance (BAL.cs): " + ex.Message, 3);
                return new List<AttendanceModel>();
            }
        }

        // LOGIN ATTENDANCE
        public void InsertAttendance()
        {
            try
            {
                DAL_Obj.AttendanceInsert();
            }
            catch (Exception ex)
            {
                _logger.LoggerMessage("Error occurs in InsertAttendance (BAL.cs): " + ex.Message, 3);
            }
        }

        // LOGOUT ATTENDANCE
        public void UpdateAttendance()
        {
            try
            {
                DAL_Obj.AttendanceUpdate();
            }
            catch (Exception ex)
            {
                _logger.LoggerMessage("Error occurs in UpdateAttendance (BAL.cs): " + ex.Message, 3);
            }
        }

        // WORKING HOURS
        public string CalculateWorkingHoursForToday(List<AttendanceModel> attendance)
        {
            try
            {
                var todayAttendance = attendance.Where(a => a.Date == DateTime.Now.ToString("dd-MM-yyyy")).ToList();
                TimeSpan totalWorkingHours = TimeSpan.Zero;

                if (todayAttendance == null || !todayAttendance.Any())
                {
                    return "No attendance recorded for today.";
                }

                foreach (var att in todayAttendance)
                {
                    if (DateTime.TryParse(att.Login_Time, out DateTime loginTime) &&
                        DateTime.TryParse(att.Logout_Time, out DateTime logoutTime))
                    {
                        totalWorkingHours += (logoutTime - loginTime);
                    }
                }
                return $"{totalWorkingHours.Hours}h {totalWorkingHours.Minutes}m {totalWorkingHours.Seconds}s";
            }
            catch (Exception ex)
            {
                _logger.LoggerMessage("Error occurs in CalculateWorkingHoursForToday (BAL.cs): " + ex.Message, 3);
                return "An error occurred while calculating working hours.";
            }
        }
    }
}