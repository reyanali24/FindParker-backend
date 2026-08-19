using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ClassLibraryDAL.Interfaces;
using ClassLibraryModels;
using Microsoft.Data.SqlClient;

namespace ClassLibraryDAL.Services
{
    public class UsersService : IUsersInterface
    {
        List<UsersModel> UsersList { get; set; } = new List<UsersModel>();
        public long CreateUser(UsersModel ob)
        {
            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_CreateUser", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FirebaseUid", ob.FirebaseUid);
                        cmd.Parameters.AddWithValue("@Email", ob.Email);
                        cmd.Parameters.AddWithValue("@AuthProvider", ob.AuthProvider.ToString());
                        cmd.Parameters.AddWithValue("@IsActive", ob.IsActive);
                        cmd.Parameters.AddWithValue("@CreatedAt", ob.CreatedAt);
                        cmd.Parameters.AddWithValue("@UpdatedAt", ob.UpdatedAt);
                        cmd.Parameters.AddWithValue("@LastLoginAt", (object?)ob.LastLoginAt ?? DBNull.Value);
                        object? result = cmd.ExecuteScalar();
                        if (result == null || result == DBNull.Value)
                        {
                            return 0;
                        }
                        return Convert.ToInt64(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating user: " + ex.Message, ex);
            }
        }
        public List<UsersModel> Read()
        {
            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_GetUsers", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                UsersModel user = new UsersModel
                                {
                                    UserId = reader.GetInt64(reader.GetOrdinal("UserId")),
                                    FirebaseUid = reader.GetString(reader.GetOrdinal("FirebaseUid")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    AuthProvider = Enum.Parse < AuthProvider >( reader.GetString(reader.GetOrdinal("AuthProvider"))),
                                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                                    UpdatedAt = reader.GetDateTime(reader.GetOrdinal("UpdatedAt")),
                                    LastLoginAt = reader.IsDBNull(reader.GetOrdinal("LastLoginAt")) ? null : reader.GetDateTime(reader.GetOrdinal("LastLoginAt"))
                                };
                                UsersList.Add(user);
                            }
                        }
                    }
                }
                return UsersList;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading users: " + ex.Message);
                return new List<UsersModel>();
            }

        }

        public int Update(UsersModel ob)
        {
            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_UpdateUser", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", ob.UserId);
                        cmd.Parameters.AddWithValue("@FirebaseUid", ob.FirebaseUid);
                        cmd.Parameters.AddWithValue("@Email", ob.Email);
                        cmd.Parameters.AddWithValue("@AuthProvider", ob.AuthProvider.ToString());
                        cmd.Parameters.AddWithValue("@IsActive", ob.IsActive);
                        cmd.Parameters.AddWithValue("@UpdatedAt", ob.UpdatedAt);
                        cmd.Parameters.AddWithValue("@LastLoginAt", (object?)ob.LastLoginAt ?? DBNull.Value);
                        int result = cmd.ExecuteNonQuery();
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating user: " + ex.Message);
                return 0;
            }
        }
        public int Delete(long userId)
        {
            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_DeleteUser", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        int result = cmd.ExecuteNonQuery();
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting user: " + ex.Message);
                return 0;
            }


        }
    }
}
