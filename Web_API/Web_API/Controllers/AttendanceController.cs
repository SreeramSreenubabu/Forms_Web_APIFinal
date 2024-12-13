using BusinessAccessLayer;
using Microsoft.AspNetCore.Mvc;
using WindowsFormApp.Logger;
using Logger;

namespace Web_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceController : Controller
    {
        private readonly IBAL _bal;
        private ILog _logger = new Log();

        public AttendanceController(IBAL bal)
        {
            _bal = bal;
        }

        #region Get Attendance
        [HttpGet("GetAttendance")]
        public ActionResult<List<Models.AttendanceModel>> GetAttendance()
        {
            try
            {
                var attendance = _bal.GetAttendance();
                if (attendance == null || attendance.Count == 0)
                {
                    return NotFound("No attendance records found.");
                }
                return Ok(attendance);
            }
            catch (Exception ex)
            {
                _logger.LoggerMessage("Error occurred while fetching attendance." + ex, 3);
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        #endregion

        #region Insert Attendance
        [HttpPost("Insert")]
        public IActionResult InsertAttendance()
        {
            try
            {
                _bal.InsertAttendance();
                return Ok("Attendance recorded successfully.");
            }
            catch (Exception ex)
            {
                _logger.LoggerMessage("Error occurred while inserting attendance." + ex, 3);
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        #endregion

        #region Update Attendance
        [HttpPut("Update")]
        public IActionResult UpdateAttendance()
        {
            try
            {
                _bal.UpdateAttendance();
                return Ok("Attendance updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LoggerMessage("Error occurred while updating attendance." + ex, 3);
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        #endregion

        #region Working Hours
        [HttpGet("GetWorkingHours")]
        public ActionResult<string> CalculateWorkingHoursForToday()
        {
            try
            {
                var attendance = _bal.GetAttendance();
                string workingHours = _bal.CalculateWorkingHoursForToday(attendance);
                return Ok(workingHours);
            }
            catch (Exception ex)
            {
                _logger.LoggerMessage("Error occurred while calculating working hours." + ex, 3); 
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        #endregion
    }
}
