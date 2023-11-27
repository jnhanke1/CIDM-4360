namespace Week7Lecture2;
// import MySQL package for connecting remote database
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
class Program
{
    static void Main(string[] args)
    {
        string connStr = "server=20.172.0.16;user=jnhanke1;database=jnhanke1;port=8080;password=jnhanke1";
        MySqlConnection conn = new MySqlConnection(connStr);
          try
        {
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();
             // input information for new record
            Console.WriteLine("Enter Username:");
            string input_username = Console.ReadLine();
            Console.WriteLine("Enter Password:");
            string input_password = Console.ReadLine();
            string procedure = "LoginCount";
            MySqlCommand cmd = new MySqlCommand(procedure, conn);
            cmd.CommandType = CommandType.StoredProcedure; // set the commandType as storedProcedure
            cmd.Parameters.AddWithValue("@inputUsername", input_username);
            cmd.Parameters.AddWithValue("@inputPassword", input_password);
            // set the out parameter @userCount in procedure as output parameter
            cmd.Parameters.Add("@userCount", MySqlDbType.Int32).Direction =  ParameterDirection.Output;
            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Close();
            // convert returned value to int datatype
            int returnCount = (int) cmd.Parameters["@userCount"].Value;
            // if return value is 1, it means can find the user, else can not find the user
            if(returnCount ==1){
                Console.WriteLine("Login Successfully!");
            }
            else{
                Console.WriteLine("Cannot find user");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }


        conn.Close();
        Console.WriteLine("Done.");
    }
}
