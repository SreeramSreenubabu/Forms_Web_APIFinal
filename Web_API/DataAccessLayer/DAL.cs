using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WindowsFormApp.DataAccessLayer;
using System.Configuration;
using DataAccessLayer;
using System.Windows.Forms;
using WindowsFormApp.Logger;
using Logger;

namespace WindowsFormApp.DataAccessLayer
{
    public class DAL : IDAL
    {
        public ILog _logger = new Log();

        private readonly string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"]?.ConnectionString;

        //LOGIN
        public UserModel LoginValid(string email, string password)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string is not initialized.");
            }
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("GetLogin", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Password", password);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new UserModel
                                {
                                    Email = reader["Email"].ToString(),
                                    Password = reader["Password"].ToString()
                                };
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("SQL Error: " + ex.Message);
                    _logger.LoggerMessage("SQL Error: " + ex.Message, 3);
                    throw;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    _logger.LoggerMessage("Error: " + ex.Message, 3);
                    throw;
                }
            }
            return null;
        }



        //PROFILE GET
        public List<ProfileModel> GetAllProfiles()
        {
            List<ProfileModel> profiles = new List<ProfileModel>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("GetEmployeeDetails", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProfileModel profile = new ProfileModel
                                {
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    DOB = Convert.ToDateTime(reader["DOB"]).ToString("yyyy-MM-dd"),
                                    Gender = reader["Gender"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    City = reader["City"].ToString(),
                                    State = reader["State"].ToString(),
                                    PhoneNumber = reader["PhoneNumber"].ToString(),
                                };
                                profiles.Add(profile);
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("SQL Error: " + ex.Message);
                    _logger.LoggerMessage("SQL Error: " + ex.Message, 3);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    _logger.LoggerMessage("Error: " + ex.Message, 3);
                }
            }

            return profiles;
        }


        //ATTENDANCE INSERT
        public void AttendanceInsert()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("InsertAttendance", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("SQL Error: " + ex.Message);
                    _logger.LoggerMessage("SQL Error: " + ex.Message, 3);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    _logger.LoggerMessage("Error: " + ex.Message, 3);
                }
            }
        }

        //ATTENDANCE UPDATE
        public void AttendanceUpdate()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UpdateAttendance", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("SQL Error: " + ex.Message);
                    _logger.LoggerMessage("SQL Error: " + ex.Message, 3);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    _logger.LoggerMessage("Error: " + ex.Message, 3);
                }
            }
        }

        //ATTENDANCE GET
        public List<AttendanceModel> GetAllAttendance()
        {
            List<AttendanceModel> attendanceList = new List<AttendanceModel>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("GetAttendanceDetails", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AttendanceModel attendance = new AttendanceModel
                                {
                                    Date = ((DateTime)reader["Date"]).ToString("dd-MM-yyyy"),
                                    Login_Time = reader["Login_Time"].ToString(),
                                    Logout_Time = reader["Logout_Time"].ToString()
                                };
                                attendanceList.Add(attendance);
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("SQL Error: " + ex.Message);
                    _logger.LoggerMessage("SQL Error: " + ex.Message, 3);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    _logger.LoggerMessage("Error: " + ex.Message, 3);
                }
            }
            return attendanceList;
        }
    }
}

public class AttendanceModel
{
    public string Date { get; set; }
    public string Login_Time { get; set; }
    public string Logout_Time { get; set; }
}

public class ProfileModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string DOB { get; set; }
    public string Gender { get; set; }
    public string Email { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PhoneNumber { get; set; }
}

public class UserModel
{
    public string Email { get; set; }
    public string Password { get; set; }
}