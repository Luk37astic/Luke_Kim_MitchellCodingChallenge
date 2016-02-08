using ClassLibrary;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALayer
{
    public class DAL
    {
        #region VARIABLES
        private string connectionString;
        #endregion

        #region CONSTRUCTORS
        public DAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        #endregion

        #region DATABASE METHODS
        public MitchellClaim DBGetClaimByID(string claimNum)
        {
            MitchellClaim ret = new MitchellClaim();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string query =
                        " SELECT * " +
                        " FROM (( " +
                        "         MitchellClaims LEFT JOIN StatusCode ON (MitchellClaims.Status = StatusCode.StatusID) " +
                        "     ) AS withStatus LEFT JOIN LossInfo ON (withStatus.LossInfo = LossInfo.LossInfoID) " +
                        " ) AS withLossInfo LEFT JOIN CauseOfLossCode ON (withLossInfo.CauseOfLoss = CauseOfLossCode.CauseID) " +
                        " WHERE ClaimNumber = :ClaimNumber ";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.Add(new NpgsqlParameter("ClaimNumber", NpgsqlDbType.Varchar));

                        command.Parameters[0].Value = claimNum;

                        using (NpgsqlDataReader dr = command.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                ret = new MitchellClaim()
                                {
                                    ClaimNumber = dr["ClaimNumber"].ToString().Trim(),
                                    ClaimantFirstName = dr["ClaimantFirstName"].ToString().Trim(),
                                    ClaimantLastName = dr["ClaimantLastName"].ToString().Trim(),
                                    Status = dr["StatusDesc"].ToString().Trim(),
                                    LossDate = DateTime.Parse(dr["LossDate"].ToString().Trim()),
                                    LossInfo = new LossInfoType()
                                    {
                                        CauseOfLoss = dr["CauseDesc"].ToString().Trim(),
                                        ReportedDate = DateTime.Parse(dr["ReportedDate"].ToString().Trim()),
                                        LossDescription = dr["LossDescription"].ToString().Trim()
                                    },
                                    AssignedAdjusterID = long.Parse(dr["AssignedAdjusterID"].ToString().Trim()),
                                    Vehicles = new Vehicles()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Error handling
                Console.WriteLine("Error in DBGetClaimByID(string claimNum): " + ex.Message);
            }
            finally
            {

            }

            return ret;
        }
        public List<MitchellClaim> DBGetAllClaims()
        {
            List<MitchellClaim> ret = new List<MitchellClaim>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string query =
                        " SELECT * " +
                        " FROM (( " +
                        "         MitchellClaims LEFT JOIN StatusCode ON (MitchellClaims.Status = StatusCode.StatusID) " +
                        "     ) AS withStatus LEFT JOIN LossInfo ON (withStatus.LossInfo = LossInfo.LossInfoID) " +
                        " ) AS withLossInfo LEFT JOIN CauseOfLossCode ON (withLossInfo.CauseOfLoss = CauseOfLossCode.CauseID) ";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        using (NpgsqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                ret.Add(new MitchellClaim()
                                {
                                    ClaimNumber = dr["ClaimNumber"].ToString().Trim(),
                                    ClaimantFirstName = dr["ClaimantFirstName"].ToString().Trim(),
                                    ClaimantLastName = dr["ClaimantLastName"].ToString().Trim(),
                                    Status = dr["StatusDesc"].ToString().Trim(),
                                    LossDate = DateTime.Parse(dr["LossDate"].ToString().Trim()),
                                    LossInfo = new LossInfoType()
                                    {
                                        CauseOfLoss = dr["CauseDesc"].ToString().Trim(),
                                        ReportedDate = DateTime.Parse(dr["ReportedDate"].ToString().Trim()),
                                        LossDescription = dr["LossDescription"].ToString().Trim()
                                    },
                                    AssignedAdjusterID = long.Parse(dr["AssignedAdjusterID"].ToString().Trim()),
                                    Vehicles = new Vehicles()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Error handling
                Console.WriteLine("Error in DBGetAllClaims(): " + ex.Message);
            }
            finally
            {

            }

            return ret;
        }
        public List<MitchellClaim> DBGetClaimsInDates(DateTime start, DateTime end)
        {
            List<MitchellClaim> ret = new List<MitchellClaim>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string query =
                        " SELECT * " +
                        " FROM (( " +
                        "         MitchellClaims LEFT JOIN StatusCode ON (MitchellClaims.Status = StatusCode.StatusID) " +
                        "     ) AS withStatus LEFT JOIN LossInfo ON (withStatus.LossInfo = LossInfo.LossInfoID) " +
                        " ) AS withLossInfo LEFT JOIN CauseOfLossCode ON (withLossInfo.CauseOfLoss = CauseOfLossCode.CauseID) " +
                        " WHERE LossDate BETWEEN :StartDate AND :EndDate ";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.Add(new NpgsqlParameter("StartDate", NpgsqlDbType.Timestamp));
                        command.Parameters.Add(new NpgsqlParameter("EndDate", NpgsqlDbType.Timestamp));

                        command.Parameters[0].Value = start;
                        command.Parameters[1].Value = end;

                        using (NpgsqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                ret.Add(new MitchellClaim()
                                {
                                    ClaimNumber = dr["ClaimNumber"].ToString().Trim(),
                                    ClaimantFirstName = dr["ClaimantFirstName"].ToString().Trim(),
                                    ClaimantLastName = dr["ClaimantLastName"].ToString().Trim(),
                                    Status = dr["StatusDesc"].ToString().Trim(),
                                    LossDate = DateTime.Parse(dr["LossDate"].ToString().Trim()),
                                    LossInfo = new LossInfoType()
                                    {
                                        CauseOfLoss = dr["CauseDesc"].ToString().Trim(),
                                        ReportedDate = DateTime.Parse(dr["ReportedDate"].ToString().Trim()),
                                        LossDescription = dr["LossDescription"].ToString().Trim()
                                    },
                                    AssignedAdjusterID = long.Parse(dr["AssignedAdjusterID"].ToString().Trim()),
                                    Vehicles = new Vehicles()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Error handling
                Console.WriteLine("Error in DBGetClaimsInDates(DateTime start, DateTime end): " + ex.Message);
            }
            finally
            {

            }

            return ret;
        }
        
        public void DBCreateClaim(MitchellClaim newClaim)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string query =
                        " INSERT INTO MitchellClaims (ClaimNumber, ClaimantFirstName, ClaimantLastName, Status, LossDate, LossInfo, AssignedAdjusterID, Vehicles) " +
                        " VALUES (:ClaimNumber, :ClaimantFirstName, :ClaimantLastName, :Status, :LossDate, :LossInfo, :AssignedAdjusterID, :Vehicles); ";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.Add(new NpgsqlParameter("ClaimNumber", NpgsqlDbType.Varchar));
                        command.Parameters.Add(new NpgsqlParameter("ClaimantFirstName", NpgsqlDbType.Varchar));
                        command.Parameters.Add(new NpgsqlParameter("ClaimantLastName", NpgsqlDbType.Varchar));
                        command.Parameters.Add(new NpgsqlParameter("Status", NpgsqlDbType.Integer));
                        command.Parameters.Add(new NpgsqlParameter("LossDate", NpgsqlDbType.Timestamp));
                        command.Parameters.Add(new NpgsqlParameter("LossInfo", NpgsqlDbType.Integer));
                        command.Parameters.Add(new NpgsqlParameter("AssignedAdjusterID", NpgsqlDbType.Bigint));
                        command.Parameters.Add(new NpgsqlParameter("Vehicles", NpgsqlDbType.Integer));

                        command.Parameters[0].Value = newClaim.ClaimNumber;
                        command.Parameters[1].Value = (newClaim.ClaimantFirstName == "") ? ("") : (newClaim.ClaimantFirstName);
                        command.Parameters[2].Value = (newClaim.ClaimantLastName == "") ? ("") : (newClaim.ClaimantLastName);
                        command.Parameters[3].Value = (newClaim.Status == "OPEN") ? (1) : (2);
                        command.Parameters[4].Value = newClaim.LossDate;
                        command.Parameters[5].Value = DBCreateLossInfo(newClaim.LossInfo);
                        command.Parameters[6].Value = newClaim.AssignedAdjusterID;
                        command.Parameters[7].Value = DBCreateVehicleList(newClaim);
                        
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Error handling
                Console.WriteLine("Error in DBCreateClaim: " + ex.Message);
            }
            finally
            {

            }
        }
        public int DBCreateLossInfo(LossInfoType newLossInfo)
        {
            int newLossInfoID = 0;

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string query =
                        " INSERT INTO LossInfo (CauseOfLoss, ReportedDate, LossDescription) " +
                        " VALUES (:CauseOfLoss, :ReportedDate, :LossDescription) RETURNING LossInfoID; ";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        int causeOfLossID = 0;
                        switch (newLossInfo.CauseOfLoss)
                        {
                            case "Collision":
                                causeOfLossID = 1;
                                break;
                            case "Explosion":
                                causeOfLossID = 2;
                                break;
                            case "Fire":
                                causeOfLossID = 3;
                                break;
                            case "Hail":
                                causeOfLossID = 4;
                                break;
                            case "Mechanical Breakdown":
                                causeOfLossID = 5;
                                break;
                            case "Other":
                                causeOfLossID = 6;
                                break;
                        }

                        command.Parameters.Add(new NpgsqlParameter("CauseOfLoss", NpgsqlDbType.Integer));
                        command.Parameters.Add(new NpgsqlParameter("ReportedDate", NpgsqlDbType.Timestamp));
                        command.Parameters.Add(new NpgsqlParameter("LossDescription", NpgsqlDbType.Varchar));

                        command.Parameters[0].Value = causeOfLossID;
                        command.Parameters[1].Value = newLossInfo.ReportedDate;
                        command.Parameters[2].Value = (newLossInfo.LossDescription == "") ? ("") : (newLossInfo.LossDescription);

                        newLossInfoID = Int32.Parse(command.ExecuteScalar().ToString().Trim());
                    }
                }
            }
            catch (Exception ex)
            {
                // Error handling
                Console.WriteLine("Error in DBCreateLossInfo: " + ex.Message);
            }
            finally
            {

            }

            return newLossInfoID;
        }
        public int DBCreateVehicleList(MitchellClaim newClaim)
        {
            int newVehicleListID = 0;

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string query = " INSERT INTO VehicleList (VListClaimNumber) VALUES (:VListClaimNumber) RETURNING VListID; ";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.Add(new NpgsqlParameter("VListClaimNumber", NpgsqlDbType.Varchar));

                        command.Parameters[0].Value = newClaim.ClaimNumber;

                        newVehicleListID = Int32.Parse(command.ExecuteScalar().ToString().Trim());
                    }
                }
            }
            catch (Exception ex)
            {
                // Error handling
                Console.WriteLine("Error in DBCreateVehicleList: " + ex.Message);
            }
            finally
            {

            }

            if (newClaim.Vehicles.VehicleDetails.Count > 0)
            {
                // Store each vehicle in the claim in the database
                foreach (VehicleDetails v in newClaim.Vehicles.VehicleDetails)
                {
                    DBCreateVehicles(v, newVehicleListID);
                }
            }

            //Console.WriteLine("Added vehicle list ID: " + newVehicleListID);
            return newVehicleListID;
        }
        public void DBCreateVehicles(VehicleDetails newVehicle, int vehicleListID)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string query =
                        " INSERT INTO VehicleInfo (ListID, ModelYear, MakeDescription, ModelDescription, EngineDescription, ExteriorColor, Vin, LicPlate, LicPlateState, LicPlateExpDate, DamageDescription, Mileage) " +
                        " VALUES (:ListID, :ModelYear, :MakeDescription, :ModelDescription, :EngineDescription, :ExteriorColor, :Vin, :LicPlate, :LicPlateState, :LicPlateExpDate, :DamageDescription, :Mileage); ";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.Add(new NpgsqlParameter("ListID", NpgsqlDbType.Integer));
                        command.Parameters.Add(new NpgsqlParameter("ModelYear", NpgsqlDbType.Integer));
                        command.Parameters.Add(new NpgsqlParameter("MakeDescription", NpgsqlDbType.Varchar));
                        command.Parameters.Add(new NpgsqlParameter("ModelDescription", NpgsqlDbType.Varchar));
                        command.Parameters.Add(new NpgsqlParameter("EngineDescription", NpgsqlDbType.Varchar));
                        command.Parameters.Add(new NpgsqlParameter("ExteriorColor", NpgsqlDbType.Varchar));
                        command.Parameters.Add(new NpgsqlParameter("Vin", NpgsqlDbType.Varchar));
                        command.Parameters.Add(new NpgsqlParameter("LicPlate", NpgsqlDbType.Varchar));
                        command.Parameters.Add(new NpgsqlParameter("LicPlateState", NpgsqlDbType.Varchar));
                        command.Parameters.Add(new NpgsqlParameter("LicPlateExpDate", NpgsqlDbType.Date));
                        command.Parameters.Add(new NpgsqlParameter("DamageDescription", NpgsqlDbType.Varchar));
                        command.Parameters.Add(new NpgsqlParameter("Mileage", NpgsqlDbType.Integer));

                        command.Parameters[0].Value = vehicleListID;
                        command.Parameters[1].Value = newVehicle.ModelYear;
                        command.Parameters[2].Value = newVehicle.MakeDescription;
                        command.Parameters[3].Value = newVehicle.ModelDescription;
                        command.Parameters[4].Value = newVehicle.EngineDescription;
                        command.Parameters[5].Value = newVehicle.ExteriorColor;
                        command.Parameters[6].Value = newVehicle.Vin;
                        command.Parameters[7].Value = newVehicle.LicPlate;
                        command.Parameters[8].Value = newVehicle.LicPlateState;
                        command.Parameters[9].Value = newVehicle.LicPlateExpDate;
                        command.Parameters[10].Value = newVehicle.DamageDescription;
                        command.Parameters[11].Value = newVehicle.Mileage;

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Error handling
                Console.WriteLine("Error in DBCreateVehicles: " + ex.Message);
            }
            finally
            {

            }
        }

        public void DBDeleteClaim(string claimNum)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string query =
                        " DELETE FROM LossInfo WHERE LossInfoID = (SELECT LossInfo FROM MitchellClaims WHERE ClaimNumber = :ClaimNumber); " +
                        " DELETE FROM VehicleList WHERE VListClaimNumber = :ClaimNumber; " +
                        " DELETE FROM MitchellClaims WHERE ClaimNumber = :ClaimNumber; ";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.Add(new NpgsqlParameter("ClaimNumber", NpgsqlDbType.Varchar));

                        command.Parameters[0].Value = claimNum;
                        
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Error handling
                Console.WriteLine("Error in DBDeleteClaim(): " + ex.Message);
            }
            finally
            {

            }
        }
        public void DBDeleteAllClaims()
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string query =
                        " DELETE FROM LossInfo; " +
                        " DELETE FROM VehicleList; " +
                        " DELETE FROM MitchellClaims; ";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Error handling
                Console.WriteLine("Error in DBDeleteAllClaims(): " + ex.Message);
            }
            finally
            {

            }
        }
        #endregion

        // TODO:
        //public void DBUpdateClaim(MitchellClaim upClaim)
        //{

        //}
    }
}
