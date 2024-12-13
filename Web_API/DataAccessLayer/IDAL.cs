using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormApp.DataAccessLayer;

namespace DataAccessLayer
{
    public interface IDAL
    {
        UserModel LoginValid(string email, string password);
        List<ProfileModel> GetAllProfiles();
        List<AttendanceModel> GetAllAttendance();
        void AttendanceInsert();
        void AttendanceUpdate();
    }
}
