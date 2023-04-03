using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Text.Json.Nodes;

namespace WebAPI.Models
{
    public class SQLModel
    {
        //Attributes 
        private static string ConString = "Data Source=(localdb)\\SPCADB;Initial Catalog=HelpingJordan;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static List<string> GetWords() //SQL Method to retrieve words 
        {
            //Initialise SqlConnection with Connection String
            //Use SQLConnection inside of SQLCOmmand with SQL STATEMENT 
            List<string> words = new List<string>();
            using (SqlConnection con = new SqlConnection(ConString))
            {
                //Opened connection string 
                con.Open();
                string sql = "SELECT TOP 10 word from Words Order by NEWID()";
                using (SqlCommand command = new SqlCommand(sql, con))
                {
                    using(SqlDataReader reader = command.ExecuteReader()) { 
                        while(reader.Read())
                        {
                            string word = (string)reader["word"]; 
                            words.Add(word);
                        }
                    }
                }
            } 
            return words;
        }
        public static void InsertUser(string Username)
        {
            using (SqlConnection con = new SqlConnection(ConString))
                {
                con.Open();
                string sql = "INSERT INTO users (name,password) VALUES(@Name,@Password)"; 
                SqlCommand command = new SqlCommand(sql, con);
                command.Parameters.AddWithValue("@Name", Username);
                command.Parameters.AddWithValue("@Password", "12345");
                command.ExecuteNonQuery();
                }       
            }
        public static async void GetUsers()
        {
            using(HttpClient client = new HttpClient())
            {
                string url = "https://wordapidata.000webhostapp.com/?getnamesenglish"; 
                HttpResponseMessage response = await client.GetAsync(url);
                

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    List<String> users = JsonConvert.DeserializeObject<List<String>>(json);
                    foreach (string user in users)
                    {
                        SQLModel.InsertUser(user);
                    }
                }
            }
        }

    }





}
