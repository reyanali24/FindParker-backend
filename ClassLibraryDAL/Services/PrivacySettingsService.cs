using ClassLibraryDAL.Interfaces;
using ClassLibraryModels;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ClassLibraryDAL.Services
{
    public class PrivacySettingsService : IPrivacySettingsInterface
    {
        public int Create(PrivacySettingsModel ob)
        {
            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_CreatePrivacySettings", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", ob.UserId);
                        cmd.Parameters.AddWithValue("@PrivacyMode", ob.PrivacyMode);
                        cmd.Parameters.AddWithValue("@MaskedLineEnabled",ob.MaskedLineEnabled);
                        cmd.Parameters.AddWithValue("@AutoReplyEnabled",ob.AutoReplyEnabled);
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating privacy settings: " + ex.Message);

                return 0;
            }
        }

        public List<PrivacySettingsModel> Read()
        {
            List<PrivacySettingsModel> settings =
                new List<PrivacySettingsModel>();

            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_GetPrivacySettings", con))
                    {
                        cmd.CommandType =CommandType.StoredProcedure;

                        using (SqlDataReader reader =cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PrivacySettingsModel setting = new PrivacySettingsModel
                                    {
                                        SettingId =reader.GetInt64(reader.GetOrdinal("SettingId")),
                                        UserId = reader.GetInt64(reader.GetOrdinal("UserId")),
                                        PrivacyMode =reader.GetString(reader.GetOrdinal("PrivacyMode")),
                                        MaskedLineEnabled = reader.GetBoolean(reader.GetOrdinal("MaskedLineEnabled")),
                                        AutoReplyEnabled =reader.GetBoolean(reader.GetOrdinal("AutoReplyEnabled")),
                                        UpdatedAt = reader.GetDateTime(reader.GetOrdinal( "UpdatedAt"))
                                    };
                                settings.Add(setting);
                            }
                        }
                    }
                }
                return settings;
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "Error reading privacy settings: " + ex.Message);
                return new List<PrivacySettingsModel>();
            }
        }
        public int Update(PrivacySettingsModel ob)
        {
            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_UpdatePrivacySettings", con))
                    {
                        cmd.CommandType =CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@SettingId", ob.SettingId);

                        cmd.Parameters.AddWithValue("@UserId", ob.UserId);

                        cmd.Parameters.AddWithValue("@PrivacyMode", ob.PrivacyMode);

                        cmd.Parameters.AddWithValue("@MaskedLineEnabled",ob.MaskedLineEnabled);
                        cmd.Parameters.AddWithValue("@AutoReplyEnabled", ob.AutoReplyEnabled);
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "Error updating privacy settings: " + ex.Message);
                return 0;
            }
        }
        public int Delete(long settingId)
        {
            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_DeletePrivacySettings", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SettingId", settingId);
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "Error deleting privacy settings: " + ex.Message);
                return 0;
            }
        }
        public List<PrivacySettingsModel> GetByUserId(long userId)
        {
            List<PrivacySettingsModel> settings =new List<PrivacySettingsModel>();

            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_GetPrivacySettingsByUserId", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@UserId", userId);

                        using (SqlDataReader reader =cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PrivacySettingsModel setting =new PrivacySettingsModel
                                    {
                                        SettingId =reader.GetInt64(reader.GetOrdinal("SettingId")),

                                        UserId =reader.GetInt64(reader.GetOrdinal("UserId")),

                                        PrivacyMode =reader.GetString(reader.GetOrdinal("PrivacyMode")),

                                        MaskedLineEnabled =reader.GetBoolean(reader.GetOrdinal("MaskedLineEnabled")),

                                        AutoReplyEnabled =reader.GetBoolean(reader.GetOrdinal("AutoReplyEnabled")),

                                        UpdatedAt =reader.GetDateTime(reader.GetOrdinal("UpdatedAt"))
                                    };

                                settings.Add(setting);
                            }
                        }
                    }
                }

                return settings;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting privacy settings by user: "+ ex.Message);

                return new List<PrivacySettingsModel>();
            }
        }
    }
}
