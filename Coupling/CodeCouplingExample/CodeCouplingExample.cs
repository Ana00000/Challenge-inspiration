using System.Collections.Generic;
using System.Data.SqlClient;

namespace ExamplesApp.Coupling
{
    class CodeCouplingExample
    {
        /// <summary>
        /// 1) For the FindSuitableBlog method, determine the following:
        /// efferentCoupling(FindSuitableBlog)
        /// couplingStrength(FindSuitableBlog, BlogPost class), including the imaginary logic in TODO
        /// couplingStrength(FindSuitableBlog, SqlCommand class)
        /// couplingStrength(FindSuitableBlog, System.Data.SQLClient namespace)
        ///
        /// 2) Come up with a solution that would reduce the couplingStrength to the BlogPost class.
        /// 3) Describe which refactorings you would make to reduce the efferentCoupling of the FindSuitableBlog method.
        /// </summary>
        public BlogPost FindSuitableBlog()
        {
            // Get some configuration
            int threshold = int.Parse(ConfigurationManager.AppSettings["threshold"]);
            string connectionString = ConfigurationManager.AppSettings["connectionString"];

            // Connect to DB
            string sql = @"select * from BlogPost limit ";
            sql += threshold;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Deserialize entity
                        string title = reader["Title"].ToString();
                        string destination = reader["Destination"].ToString();
                        string text = reader["Text"].ToString();
                        BlogPost post = new BlogPost(title, destination, text);

                        // Perform business logic
                        bool suitable = false;
                        //TODO: Logic that compares the destination and checks the title and text for suitable words.
                        //TODO: If all the checks pass, suitable is set to true.
                        if (suitable) return post;
                    }
                }

                return null;
            }
        }
    }

    internal class BlogPost
    {
        string Title { get; }
        string Destination { get; }
        string Text { get; }

        public BlogPost(string title, string destination, string text)
        {
            Title = title;
            Destination = destination;
            Text = text;
        }
    }

    internal class ConfigurationManager
    {
        public static Dictionary<string, string> AppSettings { get; internal set; }
    }
}
