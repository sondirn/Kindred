using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Kindred.Base.Maps.Utils;

namespace Kindred.Base.Utils
{
    public static class JsonDeserialize
    {
        //Deserialize JSON string into an object
        public static T DeserializeJSON<T>(string path)
        {
            //Get get string from path of file
            var jsonString = ReadJSON(path);
            try
            {
                //Return object w/ json data
                 return JsonConvert.DeserializeObject<T>(jsonString);
            }
            catch(Exception ex)
            {
                //write to console
                Console.WriteLine("Problem: " + ex.Message.ToString());
                return default;
            }

        }

        public static string ReadJSON(string path)
        {
            try
            {
                string result;
                using(var reader = new StreamReader(@"Content\MapTest.json"))
                {
                    result = reader.ReadToEnd();
                }
                return result;
            }catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message.ToString() + ", Could not load JSON file: " + path);
                throw new System.ArgumentException("Error: " + ex.Message.ToString() + ", Could not load JSON file: " + path);
            }
            
        }
    }
}
