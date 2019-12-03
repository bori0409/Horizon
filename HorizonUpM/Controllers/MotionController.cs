using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using HorizonUpM.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HorizonUpM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotionController : ControllerBase
    {

        private const string GET_ALL = "select* from Motion";
        // GET: api/Motion
        [HttpGet]
        public IEnumerable<Motion> Get()
        {
            //return dbneshto.Get();
            List<Motion> liste = new List<Motion>();
            using (SqlConnection conn = new SqlConnection(ConnectionString.connectionString))
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                }
                using (SqlCommand cmd = new SqlCommand(GET_ALL, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Motion motion = ReadNextElement(reader);
                            liste.Add(motion);
                        }
                    }
                    return liste;


                }
            }

        }
        protected Motion ReadNextElement(SqlDataReader reader)
        {
            Motion mymotion = new Motion();
            mymotion.Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
            mymotion.Roll = reader.IsDBNull(1) ? 0 : reader.GetDouble(1);
            mymotion.Yaw = reader.IsDBNull(2) ? 0 : reader.GetDouble(2);
            mymotion.Pitch = reader.IsDBNull(3) ? 0 : reader.GetDouble(3);
            mymotion.MyDataTime = reader.IsDBNull(4) ? DateTime.Parse("1900-11-11T00:00:00.00") : reader.GetDateTime(4);
            mymotion.DeviceId = reader.IsDBNull(5) ? 0 : reader.GetInt32(5);
            return mymotion;
        }

        // GET: api/Motion/5
        [HttpGet("{id}", Name = "Get")]
        public Motion Get(int id)
        {
            string selectString = "select* from Motion where MotionId = @id";
            using (SqlConnection conn = new SqlConnection(ConnectionString.connectionString))
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                }
                using (SqlCommand cmd = new SqlCommand(selectString, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            return ReadNextElement(reader);
                        }
                        else
                        {
                            return null;
                        }
                    }


                }
            }
        }

        // POST: api/Motion
        [HttpPost]
        public int Post([FromBody] Motion value)
        {
            string insertString = "insert into Motion (MotionId,Roll, Yaw, Pitch, MyDateTime,DeviceId) values(@thisid, @thisroll, @thisyaw, @thisPitch, @thisMydateTime, @thisdeviceUD); ";
            using (SqlConnection conn = new SqlConnection(Controllers.ConnectionString.connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(insertString, conn))
                {
                    command.Parameters.AddWithValue("@thisid", value.Id);
                    command.Parameters.AddWithValue("@thisroll", value.Roll);
                    command.Parameters.AddWithValue("@thisyaw", value.Yaw);
                    command.Parameters.AddWithValue("@thisPitch", value.Pitch);
                    command.Parameters.AddWithValue("@thisMydateTime", value.MyDataTime);
                    command.Parameters.AddWithValue("@thisdeviceUD", value.DeviceId);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
        }

        // PUT: api/Motion/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
