using ClassLibraryDAL.Interfaces;
using ClassLibraryModels;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ClassLibraryDAL.Services
{
    public class QrCodesService : IQrCodesInterface
    {
        public int Create(QrCodesModel ob)
        {
            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_CreateQrCode", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SerialNo", ob.SerialNo);
                        cmd.Parameters.AddWithValue("@QrCodeValue", ob.QrCodeValue);
                        cmd.Parameters.AddWithValue("@QrLink", ob.QrLink);
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating QR code: " + ex.Message);
                return 0;
            }
        }

        public PublicQrCodeModel? GetPublicQrCode(string qrCodeValue)
        {
            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(
                        "sp_GetPublicQrCode", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue(
                            "@QrCodeValue",
                            qrCodeValue
                        );

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new PublicQrCodeModel
                                {
                                    QrId = reader.GetInt64(
                                        reader.GetOrdinal("QrId")
                                    ),

                                    QrCodeValue = reader.GetString(
                                        reader.GetOrdinal("QrCodeValue")
                                    ),

                                    VehicleId = reader.GetInt64(
                                        reader.GetOrdinal("VehicleId")
                                    ),

                                    IsAssigned = reader.GetBoolean(
                                        reader.GetOrdinal("IsAssigned")
                                    ),

                                    Status = reader.GetString(
                                        reader.GetOrdinal("Status")
                                    ),

                                    VehicleName = reader.GetString(
                                        reader.GetOrdinal("VehicleName")
                                    ),

                                    PlateNumber = reader.GetString(
                                        reader.GetOrdinal("PlateNumber")
                                    ),

                                    Color = reader.GetString(
                                        reader.GetOrdinal("Color")
                                    ),

                                    VehicleType = reader.GetString(
                                        reader.GetOrdinal("VehicleType")
                                    )
                                };
                            }
                        }
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "Error getting public QR code: "
                    + ex.Message
                );

                return null;
            }
        }
        public List<QrCodesModel> Read()
        {
            List<QrCodesModel> qrCodes = new List<QrCodesModel>();
            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_GetQrCodes", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                QrCodesModel qr = new QrCodesModel
                                {
                                    QrId = reader.GetInt64(reader.GetOrdinal("QrId")),
                                    SerialNo = reader.GetString(reader.GetOrdinal("SerialNo")),
                                    QrCodeValue = reader.GetString(reader.GetOrdinal("QrCodeValue")),
                                    QrLink = reader.GetString(reader.GetOrdinal("QrLink")),
                                    IsAssigned = reader.GetBoolean(reader.GetOrdinal("IsAssigned")),
                                    VehicleId = reader.IsDBNull(reader.GetOrdinal("VehicleId"))?null : reader.GetInt64(reader.GetOrdinal("VehicleId")),
                                    Status = reader.GetString(reader.GetOrdinal("Status")),
                                    AssignedAt = reader.IsDBNull(reader.GetOrdinal("AssignedAt"))? null : reader.GetDateTime(reader.GetOrdinal("AssignedAt")),
                                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
                                };
                                qrCodes.Add(qr);
                            }
                        }
                    }
                }
                return qrCodes;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading QR codes: " + ex.Message);

                return new List<QrCodesModel>();
            }
        }
        public int Update(QrCodesModel ob)
        {
            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_UpdateQrCode", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@QrId", ob.QrId);
                        cmd.Parameters.AddWithValue("@SerialNo", ob.SerialNo);
                        cmd.Parameters.AddWithValue("@QrCodeValue", ob.QrCodeValue);
                        cmd.Parameters.AddWithValue("@QrLink", ob.QrLink);
                        cmd.Parameters.AddWithValue("@IsAssigned", ob.IsAssigned);
                        cmd.Parameters.AddWithValue("@VehicleId",(object?)ob.VehicleId ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Status", ob.Status);
                        cmd.Parameters.AddWithValue("@AssignedAt",(object?)ob.AssignedAt ?? DBNull.Value
                        );
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating QR code: " + ex.Message);
                return 0;
            }
        }
        public int Delete(long qrId)
        {
            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_DeleteQrCode", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@QrId", qrId);
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting QR code: " + ex.Message);
                return 0;
            }
        }

        public List<QrCodesModel> GetByVehicleId(long vehicleId)
        {
            List<QrCodesModel> qrCodes = new List<QrCodesModel>();

            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(
                        "sp_GetQrCodeByVehicleId", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue(
                            "@VehicleId",
                            vehicleId
                        );

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                QrCodesModel qr = new QrCodesModel
                                {
                                    QrId = reader.GetInt64(
                                        reader.GetOrdinal("QrId")
                                    ),

                                    SerialNo = reader.GetString(
                                        reader.GetOrdinal("SerialNo")
                                    ),

                                    QrCodeValue = reader.GetString(
                                        reader.GetOrdinal("QrCodeValue")
                                    ),

                                    QrLink = reader.GetString(
                                        reader.GetOrdinal("QrLink")
                                    ),

                                    IsAssigned = reader.GetBoolean(
                                        reader.GetOrdinal("IsAssigned")
                                    ),

                                    VehicleId = reader.IsDBNull(
                                        reader.GetOrdinal("VehicleId")
                                    )
                                        ? null
                                        : reader.GetInt64(
                                            reader.GetOrdinal("VehicleId")
                                        ),

                                    Status = reader.GetString(
                                        reader.GetOrdinal("Status")
                                    ),

                                    AssignedAt = reader.IsDBNull(
                                        reader.GetOrdinal("AssignedAt")
                                    )
                                        ? null
                                        : reader.GetDateTime(
                                            reader.GetOrdinal("AssignedAt")
                                        ),

                                    CreatedAt = reader.GetDateTime(
                                        reader.GetOrdinal("CreatedAt")
                                    )
                                };

                                qrCodes.Add(qr);
                            }
                        }
                    }
                }

                return qrCodes;
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "Error getting QR code by vehicle: "
                    + ex.Message
                );

                //return new List<QrCodesModel>();
                throw;
            }
        }
    }
    }
