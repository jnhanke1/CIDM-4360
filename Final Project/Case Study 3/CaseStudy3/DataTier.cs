namespace CaseStudy3;

using System.ComponentModel.Design;
using System.Configuration;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
class DataTier{
    public string connStr = "server=20.172.0.16;user=jnhanke1;database=jnhanke1;port=8080;password=jnhanke1";

    // perform login check using Stored Procedure "LoginCount" in Database based on given staff's username and Password
       public bool LoginCheck(Staff staff){
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {  
            conn.Open();
            string procedure = "LoginCount";
            MySqlCommand cmd = new MySqlCommand(procedure, conn);
            cmd.CommandType = CommandType.StoredProcedure; // set the commandType as storedProcedure
            cmd.Parameters.AddWithValue("@inputUsername", staff.staff_username);
            cmd.Parameters.AddWithValue("@inputPassword", staff.staff_password);
            cmd.Parameters.Add("@userCount", MySqlDbType.Int32).Direction =  ParameterDirection.Output;
            MySqlDataReader rdr = cmd.ExecuteReader();
           
            int returnCount = (int) cmd.Parameters["@userCount"].Value;
            rdr.Close();
            conn.Close();

            if (returnCount ==1){
                return true;
            }
            else{
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            conn.Close();
            return false;
        }

       
    }

public DataTable SearchAllResident(){
    MySqlConnection conn = new MySqlConnection(connStr); 
    string SQL = "SELECT * FROM Residents;";
    MySqlCommand cmd = new MySqlCommand(SQL, conn); 

    MySqlDataReader rdr = cmd.ExecuteReader();

    DataTable tableResident = new DataTable();
    tableResident.Load(rdr); 
    rdr.Close(); 
    conn.Close(); 
    return tableResident; 
}


public void displayResidents(DataTable tableResidents){
    MySqlConnection conn = new MySqlConnection(connStr);
    Console.WriteLine("-------------------- List of all Residents in System --------------------");
        try
        {  
            conn.Open();
            foreach(DataRow row in tableResidents.Rows) {
            Console.WriteLine($"ID: {row["id"]} - {row["full_name"]} \t Unit {row["unit_number"]} \t Email: {row["email"]} ");
            conn.Close();
            //return;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            conn.Close();
            return;
        }

        }


public void SearchResident(Resident resident){
        MySqlConnection conn = new MySqlConnection(connStr);
        Console.WriteLine("-------------------- Resident Search --------------------"); 
        Console.WriteLine("Please input a resident name to search: ");
        string resident1 = Console.ReadLine();
        try
        {  
            conn.Open();
            string procedure = "searchRes";
            MySqlCommand cmd = new MySqlCommand(procedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@inputResident", resident.full_name);
            cmd.Parameters.AddWithValue("@inputUnitNo", resident.unit_number);

            MySqlDataReader rdr = cmd.ExecuteReader();
            // SELECT * FROM [SearchResident]; 
            // tableResidents.Load(rdr);
            rdr.Close();
            conn.Close();
            // return tableResidents;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            conn.Close();
            return;
        }
    }


    //add a package 
public void addPackage(Package package){
        MySqlConnection conn = new MySqlConnection(connStr);
        Console.WriteLine("-------------------- Add a Package --------------------"); 
        Console.WriteLine("Please input the following information to record: ");
        string package1 = Console.ReadLine();
        try
        {  
            conn.Open();
            string procedure = "addPackage";
            MySqlCommand cmd = new MySqlCommand(procedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@inputTrackingNo", package.tracking_number);
           // cmd.Parameters["@nputTrackingNo"].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@inputRecipientName", package.recipient_name);
            cmd.Parameters.AddWithValue("@inputunitNumber", package.unit_number); 
           // cmd.Parameters["@inputRecipientName"].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@inputPostalAgency", package.postalAgency); 
            //cmd.Parameters["@inputPostalAgency"].Direction = ParameterDirection.Input; 
            cmd.Parameters.AddWithValue("@inputDeliveryDate", package.deliveryDate); 
           // cmd.Parameters["@inputDeliveryDate"].Direction = ParameterDirection.Input; 

            MySqlDataReader rdr = cmd.ExecuteReader();
            Console.WriteLine("Package has been added to system."); 
            rdr.Close();
            conn.Close();
            
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            conn.Close();
            return;
        }
    }

    //notate a package picked up 
public void PickupPackage(Package package){
        MySqlConnection conn = new MySqlConnection(connStr);
        Console.WriteLine("-------------------- Notate a Package as Picked Up --------------------"); 
        Console.WriteLine("Please input a tracking number to search: ");
        string package1 = Console.ReadLine();
        try
        {  
            conn.Open();
            string procedure = "PickupPackage";
            MySqlCommand cmd = new MySqlCommand(procedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@inputTrackingNo", package.tracking_number);
            // cmd.Parameters.AddWithValue("@inputRecipientName", Package.recipient_name);
            // cmd.Parameters.AddWithValue("@inputResidentName", Package.full_name);


            MySqlDataReader rdr = cmd.ExecuteReader();
            // SELECT * FROM [searchPackage]
            // WHERE @inputTrackingNo = Package.tracking_number; 
            // searchPackage.Load(rdr);

            Console.WriteLine("Package has been updated as picked up."); 

            rdr.Close();
            conn.Close();
            // return tableResidents;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            conn.Close();
            return;
        }
    }

    //delete a package
public void deletePackage(Package package){
        MySqlConnection conn = new MySqlConnection(connStr);
        Console.WriteLine("-------------------- Delete a Package --------------------"); 
        Console.WriteLine("Please input the following information to delete record: ");
        string package1 = Console.ReadLine();
        try
        {  
            conn.Open();
            string procedure = "deletePackage";
            MySqlCommand cmd = new MySqlCommand(procedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@inputRecipientName", package.recipient_name);       
            cmd.Parameters.AddWithValue("@inputResidentName", package.full_name); 
            cmd.Parameters.AddWithValue("@inputTrackingNo", package.tracking_number);
           // cmd.Parameters["@nputTrackingNo"].Direction = ParameterDirection.Input;

 

            MySqlDataReader rdr = cmd.ExecuteReader();
            Console.WriteLine("Package has been removed from system."); 
            rdr.Close();
            conn.Close();
            
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            conn.Close();
            return;
        }
    }

    //search package history
public void searchPackage(Package package){
        MySqlConnection conn = new MySqlConnection(connStr);
        Console.WriteLine("-------------------- View Package History --------------------"); 
        Console.WriteLine("Please input the following to search, or leave blank and enter to go to next search criteria: ");
        string package1 = Console.ReadLine();
        try
        {  
            conn.Open();
            string procedure = "searchPackage";
            MySqlCommand cmd = new MySqlCommand(procedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@inputRecipientName", package.recipient_name);
            cmd.Parameters.AddWithValue("@inputTrackingNo", package.tracking_number);
            cmd.Parameters.AddWithValue("@inputUnitNo", package.unit_number);

            MySqlDataReader rdr = cmd.ExecuteReader();
            // SELECT * FROM [SearchResident]; 
            // tableResidents.Load(rdr);
            rdr.Close();
            conn.Close();
            // return tableResidents;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            conn.Close();
            return;
        }
    }

    //see pending packages  -- under GuiTier


    //see unknown pakcages





}