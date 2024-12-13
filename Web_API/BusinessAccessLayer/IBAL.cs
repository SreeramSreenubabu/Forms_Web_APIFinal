using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormApp.DataAccessLayer;

namespace BusinessAccessLayer
{
    public interface IBAL
    {
        UserModel Login(string email, string password);
        List<ProfileModel> GetProfiles();
        List<AttendanceModel> GetAttendance();
        string CalculateWorkingHoursForToday(List<AttendanceModel> attendance);
        void InsertAttendance();
        void UpdateAttendance();
    }
}
