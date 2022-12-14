using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Olympics.Models;
using System.Data.SqlClient;
using System.Configuration;

namespace Olympics.Controllers
{
    static class Partecipations
    {
        //STRINGA CONNESSIONE AL DATABASE
        public static string connectionString { get { return ConfigurationManager.ConnectionStrings["Database"].ConnectionString; } }


        //METODI
        public static List<string> getGenders()
        {
            List<string> genders = new List<string>();

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "SELECT DISTINCT Sex FROM athletes";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.IsDBNull(reader.GetOrdinal("Sex")))
                                continue;
                            genders.Add((string)reader["Sex"]);
                        }
                    }

                    return genders;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public static List<string> getGames()
        {
            List<string> games = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "SELECT DISTINCT Games FROM athletes";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.IsDBNull(reader.GetOrdinal("Games")))
                                continue;
                            games.Add((string)reader["Games"]);
                        }
                    }

                    return games;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public static List<string> getMedals()
        {
            List<string> medal = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "SELECT DISTINCT Medal FROM athletes";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.IsDBNull(reader.GetOrdinal("Medal")))
                                continue;
                            medal.Add((string)reader["Medal"]);
                        }
                    }

                    return medal;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public static List<string> getSports(string game)
        {
            List<string> sports = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "SELECT DISTINCT Sport FROM athletes WHERE Games = @Game";

                    command.Parameters.AddWithValue("@Game", game);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.IsDBNull(reader.GetOrdinal("Sport")))
                                continue;
                            sports.Add((string)reader["Sport"]);
                        }
                    }

                    return sports;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public static List<string> getEvents(string game, string sport)
        {
            List<string> events = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "SELECT DISTINCT Event FROM athletes WHERE Games = @Game AND Sport = @Sport";

                    command.Parameters.AddWithValue("@Game", game);
                    command.Parameters.AddWithValue("@Sport", sport);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.IsDBNull(reader.GetOrdinal("Event")))
                                continue;
                            events.Add((string)reader["Event"]);
                        }
                    }

                    return events;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public static List<Partecipation> getAll(string name, string sex, string game, string sport, string evento, string medal, int currentPage, int dimensionPage, ref int totalPage)
        {
            List<Partecipation> a = new List<Partecipation>();

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM athletes";


                    if (name != null || sex != null || game != null || sport != null || evento != null || medal != null)
                    {
                        command.CommandText += " WHERE";

                        if (name != null)
                        {
                            command.CommandText += " Name LIKE @Name AND";
                            command.Parameters.AddWithValue("@Name", $"%{name}%");
                        }
                        if (sex != null)
                        {
                            command.CommandText += " Sex LIKE @Sex AND";
                            command.Parameters.AddWithValue("@Sex", $"%{sex}%");
                        }
                        if (game != null)
                        {
                            command.CommandText += " Games LIKE @Game AND";
                            command.Parameters.AddWithValue("@Game", $"%{game}%");
                        }
                        if (evento != null)
                        {
                            command.CommandText += " Event LIKE @Event AND";
                            command.Parameters.AddWithValue("@Event", $"%{evento}%");
                        }
                        if (sport != null)
                        {
                            command.CommandText += " Sport LIKE @Sport AND";
                            command.Parameters.AddWithValue("@Sport", $"%{sport}%");
                        }
                        if (medal != null)
                        {
                            command.CommandText += " Medal LIKE @Medal AND";
                            command.Parameters.AddWithValue("@Medal", $"%{medal}%");
                        }

                        command.CommandText = command.CommandText.Substring(0, command.CommandText.Length - 3);

                    }


                    //command 2 serve semplicemente per tenere traccia del nuemero di pagine totali
                    SqlCommand command2 = new SqlCommand();
                    command2.Connection = connection;
                    command2.CommandText = command.CommandText.Replace("*", "COUNT(*)");

                    command2.Parameters.AddWithValue("@Name", $"%{name}%");
                    command2.Parameters.AddWithValue("@Sex", $"%{sex}%");
                    command2.Parameters.AddWithValue("@Game", $"%{game}%");
                    command2.Parameters.AddWithValue("@Event", $"%{evento}%");
                    command2.Parameters.AddWithValue("@Sport", $"%{sport}%");
                    command2.Parameters.AddWithValue("@Medal", $"%{medal}%");

                    totalPage = (int)command2.ExecuteScalar() + 1;
                    



                    command.CommandText += " ORDER BY Id";
                    command.CommandText += " OFFSET @n ROWS";
                    command.CommandText += " FETCH NEXT @m ROWS ONLY";
                    
                    command.Parameters.AddWithValue("@n", (currentPage - 1) * dimensionPage);
                    command.Parameters.AddWithValue("@m", dimensionPage);

                    


                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            a.Add(editPartecipation(reader));
                        }
                    }
                    return a;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public static Partecipation editPartecipation(SqlDataReader reader)
        {
            return new Partecipation
            {
                Id = (long)reader["Id"],
                IdAthlete = reader.IsDBNull(reader.GetOrdinal("IdAthlete")) ? null : (long?)reader["IdAthlete"],
                Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : (string)reader["Name"],
                Sex = reader.IsDBNull(reader.GetOrdinal("Sex")) ? null : (string)reader["Sex"],
                Age = reader.IsDBNull(reader.GetOrdinal("Age")) ? null : (int?)reader["Age"],
                Height = reader.IsDBNull(reader.GetOrdinal("Height")) ? null : (int?)reader["Height"],
                Weight = reader.IsDBNull(reader.GetOrdinal("Weight")) ? null : (int?)reader["Weight"],
                NOC = reader.IsDBNull(reader.GetOrdinal("NOC")) ? null : (string)reader["NOC"],
                Games = reader.IsDBNull(reader.GetOrdinal("Games")) ? null : (string)reader["Games"],
                Year = reader.IsDBNull(reader.GetOrdinal("Year")) ? -1 : (int)reader["Year"],
                Season = reader.IsDBNull(reader.GetOrdinal("Season")) ? null : (string)reader["Season"],
                Sport = reader.IsDBNull(reader.GetOrdinal("Sport")) ? null : (string)reader["Sport"],
                Event = reader.IsDBNull(reader.GetOrdinal("Event")) ? null : (string)reader["Event"],
                City = reader.IsDBNull(reader.GetOrdinal("City")) ? null : (string)reader["City"],
                Medal = reader.IsDBNull(reader.GetOrdinal("Medal")) ? null : (string)reader["Medal"]
            };
        }
    }
}
