using ClassLibraryDAL.Interfaces;
using ClassLibraryModels;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ClassLibraryDAL.Services
{

    public class UserProfilesService : IUserProfilesInterface
    {
        public int Create(UserProfilesModel ob)
        {
            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_CreateUserProfile", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@UserId", ob.UserId);
                        cmd.Parameters.AddWithValue("@FullName", ob.FullName);
                        cmd.Parameters.AddWithValue("@PhoneNumber", ob.PhoneNumber);
                        cmd.Parameters.AddWithValue("@ResidentialAddress", ob.ResidentialAddress);
                        cmd.Parameters.AddWithValue("@City", ob.City);
                        cmd.Parameters.AddWithValue(
                            "@ProfilePhotoUrl",
                            (object?)ob.ProfilePhotoUrl ?? DBNull.Value
                        );

                        int result = cmd.ExecuteNonQuery();

                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating user profile: " + ex.Message);
                return 0;
            }
        }


        public List<UserProfilesModel> Read()
        {
            List<UserProfilesModel> profiles = new List<UserProfilesModel>();

            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_GetUserProfiles", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                UserProfilesModel profile = new UserProfilesModel
                                {
                                    ProfileId = reader.GetInt64(reader.GetOrdinal("ProfileId")),
                                    UserId = reader.GetInt64(reader.GetOrdinal("UserId")),
                                    FullName = reader.GetString(reader.GetOrdinal("FullName")),
                                    PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                                    ResidentialAddress = reader.GetString(reader.GetOrdinal("ResidentialAddress")),
                                    City = reader.GetString(reader.GetOrdinal("City")),
                                    ProfilePhotoUrl = reader.IsDBNull(reader.GetOrdinal("ProfilePhotoUrl")) ? null : reader.GetString(reader.GetOrdinal("ProfilePhotoUrl")),
                                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                                    UpdatedAt = reader.GetDateTime(reader.GetOrdinal("UpdatedAt"))

                                };
                                profiles.Add(profile);
                            }
                        }
                    }
                }

                return profiles;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading user profiles: " + ex.Message);

                return new List<UserProfilesModel>();
            }
        }


        public int Update(UserProfilesModel ob)
        {
            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_UpdateUserProfile", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ProfileId", ob.ProfileId);
                        cmd.Parameters.AddWithValue("@UserId", ob.UserId);
                        cmd.Parameters.AddWithValue("@FullName", ob.FullName);
                        cmd.Parameters.AddWithValue("@PhoneNumber", ob.PhoneNumber);
                        cmd.Parameters.AddWithValue("@ResidentialAddress", ob.ResidentialAddress);
                        cmd.Parameters.AddWithValue("@City", ob.City);

                        cmd.Parameters.AddWithValue(
                            "@ProfilePhotoUrl",
                            (object?)ob.ProfilePhotoUrl ?? DBNull.Value
                        );

                        cmd.Parameters.AddWithValue("@UpdatedAt", ob.UpdatedAt);

                        int result = cmd.ExecuteNonQuery();

                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating user profile: " + ex.Message);
                return 0;
            }
        }

        public int Delete(long profileId)
        {
            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_DeleteUserProfile", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ProfileId", profileId);

                        int result = cmd.ExecuteNonQuery();

                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting user profile: " + ex.Message);
                return 0;
            }
        }

        public List<UserProfilesModel> GetByUserId(long userId)
        {
            List<UserProfilesModel> profiles =
                new List<UserProfilesModel>();

            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(
                        "sp_GetUserProfileByUserId", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue(
                            "@UserId", userId);

                        using (SqlDataReader reader =
                               cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                UserProfilesModel profile =
                                    new UserProfilesModel
                                    {
                                        ProfileId =reader.GetInt64(reader.GetOrdinal("ProfileId")),
                                        UserId =reader.GetInt64(reader.GetOrdinal("UserId")),
                                        FullName =reader.GetString(reader.GetOrdinal("FullName")),
                                        PhoneNumber =reader.GetString(reader.GetOrdinal("PhoneNumber")),
                                        ResidentialAddress =reader.GetString(reader.GetOrdinal("ResidentialAddress")),
                                        City =reader.GetString(reader.GetOrdinal("City")),
                                        ProfilePhotoUrl =reader.IsDBNull(reader.GetOrdinal("ProfilePhotoUrl"))? null: reader.GetString(reader.GetOrdinal("ProfilePhotoUrl")),
                                        CreatedAt =reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                                        UpdatedAt =reader.GetDateTime(reader.GetOrdinal("UpdatedAt"))
                                    };

                                profiles.Add(profile);
                            }
                        }
                    }
                }

                return profiles;
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "Error getting profile by user: "+ ex.Message);

                return new List<UserProfilesModel>();
            }
        }

    }
}

  

