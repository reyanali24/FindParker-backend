using ClassLibraryDAL.Interfaces;
using ClassLibraryModels;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ClassLibraryDAL.Services
{
    public class EmergencyContactsService : IEmergencyContactsInterface
    {
        public int Create(EmergencyContactsModel ob)
        {
            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_CreateEmergencyContact", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", ob.UserId);
                        cmd.Parameters.AddWithValue("@ContactName", ob.ContactName);
                        cmd.Parameters.AddWithValue("@ContactPhone", ob.ContactPhone);
                        cmd.Parameters.AddWithValue("@Relationship",(object?)ob.Relationship ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@IsPrimary", ob.IsPrimary);
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "Error creating emergency contact: " + ex.Message);
                return 0;
            }
        }

        public List<EmergencyContactsModel> Read()
        {
            List<EmergencyContactsModel> contacts = new List<EmergencyContactsModel>();
            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_GetEmergencyContacts", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader =cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                EmergencyContactsModel contact =new EmergencyContactsModel
                                    {
                                        ContactId = reader.GetInt64(reader.GetOrdinal("ContactId")),
                                        UserId =reader.GetInt64(reader.GetOrdinal("UserId")),
                                        ContactName = reader.GetString( reader.GetOrdinal("ContactName")),
                                        ContactPhone =reader.GetString( reader.GetOrdinal( "ContactPhone")),
                                        Relationship = reader.IsDBNull(reader.GetOrdinal( "Relationship"))? null : reader.GetString(reader.GetOrdinal("Relationship")),
                                        IsPrimary = reader.GetBoolean(  reader.GetOrdinal( "IsPrimary")),
                                        CreatedAt =reader.GetDateTime( reader.GetOrdinal("CreatedAt")),
                                        UpdatedAt =reader.GetDateTime(reader.GetOrdinal("UpdatedAt"))
                                    };
                                contacts.Add(contact);
                            }
                        }
                    }
                }

                return contacts;
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "Error reading emergency contacts: "
                    + ex.Message);

                return new List<EmergencyContactsModel>();
            }
        }

        public int Update(EmergencyContactsModel ob)
        {
            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_UpdateEmergencyContact", con))
                    {
                        cmd.CommandType =CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ContactId", ob.ContactId);
                        cmd.Parameters.AddWithValue("@UserId", ob.UserId);
                        cmd.Parameters.AddWithValue("@ContactName", ob.ContactName);
                        cmd.Parameters.AddWithValue("@ContactPhone", ob.ContactPhone);
                        cmd.Parameters.AddWithValue("@Relationship",(object?)ob.Relationship ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@IsPrimary", ob.IsPrimary);
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "Error updating emergency contact: "+ ex.Message);
                return 0;
            }
        }
        public int Delete(long contactId)
        {
            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_DeleteEmergencyContact", con))
                    {
                        cmd.CommandType =CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ContactId", contactId);
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting emergency contact: "+ ex.Message);

                return 0;
            }
        }

        public List<EmergencyContactsModel> GetByUserId(long userId)
        {
            List<EmergencyContactsModel> contacts =
                new List<EmergencyContactsModel>();

            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_GetEmergencyContactsByUserId", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@UserId", userId);

                        using (SqlDataReader reader =cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                EmergencyContactsModel contact =new EmergencyContactsModel
                                    {
                                        ContactId =reader.GetInt64(reader.GetOrdinal("ContactId")),

                                        UserId =reader.GetInt64(reader.GetOrdinal("UserId")),

                                        ContactName =reader.GetString(reader.GetOrdinal("ContactName")),

                                        ContactPhone =reader.GetString(reader.GetOrdinal("ContactPhone")),

                                        Relationship =reader.IsDBNull(reader.GetOrdinal("Relationship"))? null: reader.GetString(reader.GetOrdinal("Relationship")),

                                        IsPrimary =reader.GetBoolean(reader.GetOrdinal("IsPrimary")),

                                        CreatedAt =reader.GetDateTime(reader.GetOrdinal("CreatedAt")),

                                        UpdatedAt =reader.GetDateTime(reader.GetOrdinal("UpdatedAt"))
                                    };

                                contacts.Add(contact);
                            }
                        }
                    }
                }

                return contacts;
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "Error getting emergency contacts by user: "
                    + ex.Message);

                return new List<EmergencyContactsModel>();
            }
        }
    }
}
