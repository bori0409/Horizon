using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HorizonUpNow.Model;

namespace HorizonUpNow.Controllers
{
    [Route("api/motion")]
    [ApiController]
    public class MotionController : Controller
    {
        private const string GET_ALL = "select* from Motion";

        // GET: Motion
        public IEnumerable<MotionModel> Get()
        {
            //return dbneshto.Get();
            List<MotionModel> liste = new List<MotionModel>();
            using (SqlConnection conn = new SqlConnection(ConnectionString.connectionStrings))
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
                            MotionModel motion = ReadNextElement(reader);
                            liste.Add(motion);
                        }
                    }
                    return liste;


                }
            }

        }
        protected MotionModel ReadNextElement(SqlDataReader reader)
        {
            MotionModel mymotion = new MotionModel();
            mymotion.Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
            mymotion.Roll = reader.IsDBNull(1) ? 0 : reader.GetDouble(1);
            mymotion.Yaw = reader.IsDBNull(2) ? 0 : reader.GetDouble(2);
            mymotion.Pitch = reader.IsDBNull(3) ? 0 : reader.GetDouble(3);
            mymotion.MyDataTime = reader.IsDBNull(4) ? DateTime.Parse("1900-11-11T00:00:00.00") : reader.GetDateTime(4);
            mymotion.DeviceId = reader.IsDBNull(5) ? 0 : reader.GetInt32(5);
            return mymotion;
        }

        // GET: api/Motion/5
        [Route("{id}")]
        public MotionModel Get(int id)
        {
            string selectString = "select* from Motion where MotionId = @id";
            using (SqlConnection conn = new SqlConnection(ConnectionString.connectionStrings))
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

        public int Post([FromBody] MotionModel value)
        {
            string insertString = "insert into Motion (Roll, Yaw, Pitch, MyDateTime,DeviceId) values(@thisroll, @thisyaw, @thisPitch, @thisMydateTime, @thisdeviceUD); ";
            using (SqlConnection conn = new SqlConnection(Controllers.ConnectionString.connectionStrings))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(insertString, conn))
                {
                   
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


        //    // GET: Motion/Edit/5
        //    public ActionResult Edit(int id)
        //    {
        //        return View();
        //    }

        //    // POST: Motion/Edit/5
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public ActionResult Edit(int id, IFormCollection collection)
        //    {
        //        try
        //        {
        //            // TODO: Add update logic here

        //            return RedirectToAction(nameof(Index));
        //        }
        //        catch
        //        {
        //            return View();
        //        }
        //    }

        //    // GET: Motion/Delete/5
        //    public ActionResult Delete(int id)
        //    {
        //        return View();
        //    }

        //    // POST: Motion/Delete/5
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public ActionResult Delete(int id, IFormCollection collection)
        //    {
        //        try
        //        {
        //            // TODO: Add delete logic here

        //            return RedirectToAction(nameof(Index));
        //        }
        //        catch
        //        {
        //            return View();
        //        }
        //    }
        //}
    }
}