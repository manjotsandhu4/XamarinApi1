using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XamarinApi1.Models;

namespace XamarinApi1.Controllers
{
    public class MasterController : ApiController
    {
        SqlConnection conn;
        [HttpGet]
        //public IEnumerable<Visit> GetVisits()
        public System.Web.Http.Results.JsonResult<List<Visit>> GetVisits()
        {
            List<Visit> visitData = new List<Visit>();

            connection();            

            SqlCommand cmd = new SqlCommand("spGetVisit", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Visit visit = new Visit();
                visit.Id = Convert.ToInt32(reader["Id"]);
                visit.OccurenceNo = reader["OccurenceNo"].ToString();
                visit.NameOfAccused = reader["NameOfAccused"].ToString();
                visit.DateOfCourtAppearence = Convert.ToDateTime(reader["DateOfCourtAppearence"]);
                visit.CourtAppearenceTime = reader["CourtAppearenceTime"].ToString();
                visit.DateOfOffence = reader["DateOfOffence"].ToString();
                visit.CourtHouseAddress = reader["CourtHouseAddress"].ToString();
                visit.ReasonForAppearence = reader["ReasonForAppearence"].ToString();

                visit.CheckInTime = reader["CheckInTime"] as DateTime? ?? default(DateTime);
                visit.CheckOutTime = reader["CheckOutTime"] as DateTime? ?? default(DateTime);
                visit.Testify = reader["Testify"] as string;
                visit.TimeCalledIn = reader["TimeCalledIn"] as string;
                visit.NoTestifyReason = reader["NoTestifyReason"] as string;
                visit.LunchTimeStart = reader["LunchTimeStart"] as string;
                visit.LunchTimeEnd = reader["LunchTimeEnd"] as string;
                
                visitData.Add(visit);
            }
            conn.Close();
            //return visitData;
            return this.Json(visitData);
        }

        public Response SaveVisit(Visit visit)
        {
            Response response = new Response();
            try
            {
                if (string.IsNullOrEmpty(visit.OccurenceNo))
                {
                    response.Message = "Occurence No is required";
                    response.Status = 0;
                }
                else if (string.IsNullOrEmpty(visit.NameOfAccused))
                {
                    response.Message = "Name Of Accused is required";
                    response.Status = 0;
                }
                else if (string.IsNullOrEmpty(visit.ReasonForAppearence))
                {
                    response.Message = "Reason for experience is required";
                    response.Status = 0;
                }
                else if (string.IsNullOrEmpty(visit.CourtHouseAddress))
                {
                    response.Message = "Court Address is required";
                    response.Status = 0;
                }
                else if (string.IsNullOrEmpty(visit.CourtAppearenceTime))
                {
                    response.Message = "Court appearence time is required";
                    response.Status = 0;
                }
                else if (string.IsNullOrEmpty(visit.DateOfOffence))
                {
                    response.Message = "Date of offence is required";
                    response.Status = 0;
                }
                else
                {
                    connection();
                    SqlCommand com = new SqlCommand("spAddVisit", conn);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@Id", visit.Id);
                    com.Parameters.AddWithValue("@OccurenceNo", visit.OccurenceNo);
                    com.Parameters.AddWithValue("@NameOfAccused", visit.NameOfAccused);
                    com.Parameters.AddWithValue("@ReasonForAppearence", visit.ReasonForAppearence);
                    com.Parameters.AddWithValue("@CourtHouseAddress", visit.CourtHouseAddress);
                    com.Parameters.AddWithValue("@CourtAppearenceTime", visit.CourtAppearenceTime);
                    com.Parameters.AddWithValue("@DateOfOffence", visit.DateOfOffence);
                    com.Parameters.AddWithValue("@DateOfCourtAppearence", visit.DateOfCourtAppearence);
                    //adding check-in paramaeters 
                    com.Parameters.AddWithValue("@CheckInTime", visit.CheckInTime);
                    com.Parameters.AddWithValue("@CheckOutTime", visit.CheckOutTime);
                    com.Parameters.AddWithValue("@Testify", visit.Testify);
                    com.Parameters.AddWithValue("@TimeCalledIn", visit.TimeCalledIn);
                    com.Parameters.AddWithValue("@NoTestifyReason", visit.NoTestifyReason);
                    com.Parameters.AddWithValue("@LunchTimeStart", visit.LunchTimeStart);
                    com.Parameters.AddWithValue("@LunchTimeEnd", visit.LunchTimeEnd);

                    conn.Open();
                    int i = com.ExecuteNonQuery();
                    conn.Close();
                    if (i >= 1)
                    {
                        response.Message = "Visit saved Successfully";
                        response.Status = 1;
                    }
                    else
                    {
                        response.Message = "Visit failed to save!!!";
                        response.Status = 0;
                    }
                }
                
            }catch(Exception ex)
            {
                response.Message = ex.Message;
                response.Status = 0;
            }

            return response;


        }

        private void connection()
        {
            string conString = ConfigurationManager.ConnectionStrings["getConnection"].ToString();
            conn = new SqlConnection(conString);
        }
    }
}
 