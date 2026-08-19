using ClassLibraryDAL.Interfaces;
using ClassLibraryModels;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ClassLibraryDAL.Services
{
    public class LoginHistoryService : ILoginHistoryInterface
    {
        public int Create(LoginHistoryModel ob)
        {
            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_CreateLoginHistory", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", ob.UserId);
                        cmd.Parameters.AddWithValue("@DeviceInfo",(object?)ob.DeviceInfo ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@IpAddress",(object?)ob.IpAddress ?? DBNull.Value);

                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "Error creating login history: "
                    + ex.Message);

                return 0;
            }
        }

        public List<LoginHistoryModel> Read()
        {
            List<LoginHistoryModel> history =
                new List<LoginHistoryModel>();

            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_GetLoginHistory", con))
                    {
                        cmd.CommandType =CommandType.StoredProcedure;
                        using (SqlDataReader reader =cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                LoginHistoryModel item =new LoginHistoryModel
                                {
                                        HistoryId =reader.GetInt64(reader.GetOrdinal("HistoryId")),
                                        UserId =reader.GetInt64(reader.GetOrdinal("UserId")),
                                        DeviceInfo =reader.IsDBNull(reader.GetOrdinal("DeviceInfo"))? null: reader.GetString(reader.GetOrdinal("DeviceInfo")),
                                        IpAddress =reader.IsDBNull(reader.GetOrdinal("IpAddress"))? null: reader.GetString(reader.GetOrdinal("IpAddress")),
                                        LoginAt =reader.GetDateTime(reader.GetOrdinal("LoginAt")),
                                        LoggedOutAt =reader.IsDBNull(reader.GetOrdinal("LoggedOutAt"))? null: reader.GetDateTime(reader.GetOrdinal("LoggedOutAt"))
                                    };

                                history.Add(item);
                            }
                        }
                    }
                }

                return history;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading login history: "+ ex.Message);
                return new List<LoginHistoryModel>();
            }
        }

        public int Update(LoginHistoryModel ob)
        {
            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_UpdateLoginHistory", con))
                    {
                        cmd.CommandType =CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@HistoryId", ob.HistoryId);
                        cmd.Parameters.AddWithValue("@LoggedOutAt",(object?)ob.LoggedOutAt?? DBNull.Value);
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating login history: "+ ex.Message);
                return 0;
            }
        }

        public int Delete(long historyId)
        {
            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_DeleteLoginHistory", con))
                    {
                        cmd.CommandType =CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@HistoryId", historyId);
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting login history: "+ ex.Message);

                return 0;
            }
        }


        public List<LoginHistoryModel> GetByUserId(long userId)
        {
            List<LoginHistoryModel> history =
                new List<LoginHistoryModel>();

            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_GetLoginHistoryByUserId", con))
                    {
                        cmd.CommandType =CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        using (SqlDataReader reader =cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                LoginHistoryModel item =new LoginHistoryModel
                                    {
                                        HistoryId =reader.GetInt64(reader.GetOrdinal("HistoryId")),
                                        UserId =reader.GetInt64(reader.GetOrdinal("UserId")),
                                        DeviceInfo =reader.IsDBNull(reader.GetOrdinal("DeviceInfo"))? null: reader.GetString(reader.GetOrdinal("DeviceInfo")),
                                        IpAddress =reader.IsDBNull(reader.GetOrdinal("IpAddress"))? null: reader.GetString(reader.GetOrdinal("IpAddress")),
                                        LoginAt =reader.GetDateTime(reader.GetOrdinal("LoginAt")),
                                        LoggedOutAt =reader.IsDBNull(reader.GetOrdinal("LoggedOutAt"))? null: reader.GetDateTime(reader.GetOrdinal("LoggedOutAt"))
                                    };

                                history.Add(item);
                            }
                        }
                    }
                }

                return history;
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "Error getting login history by user: "+ ex.Message);
                return new List<LoginHistoryModel>();
            }
        }

    }
}

 
