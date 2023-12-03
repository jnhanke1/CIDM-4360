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
    conn.Open(); 
    //Console.WriteLine("-------------- List of all Residents -------------"); 
    string SQL = "SELECT * FROM Residents;"; 
    MySqlCommand cmd = new MySqlCommand(SQL, conn); 

    MySqlDataReader rdr = cmd.ExecuteReader(); 

    DataTable tableResident = new DataTable(); 
    tableResident.Load(rdr); 
    rdr.Close(); 
    conn.Close(); 
    return tableResident; 
}


public void displayResidents(DataTable tableResident){
    MySqlConnection conn = new MySqlConnection(connStr);
    Console.WriteLine("-------------------- List of Residents  --------------------");
        try
        {  
            conn.Open();
            foreach(DataRow row in tableResident.Rows) {
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


public DataTable SearchResident(){
        MySqlConnection conn = new MySqlConnection(connStr);
        Console.WriteLine("-------------------- Resident Search --------------------"); 
        Console.WriteLine("Please input a resident name to search: ");
        string resident1 = Console.ReadLine();
        Console.WriteLine("Please input the resident unit Number: "); 
        string? residentUnit = Console.ReadLine(); 
        try
        {  
            conn.Open();
            string procedure = "searchRes";
            MySqlCommand cmd = new MySqlCommand(procedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@inputResident", resident1);
            cmd.Parameters.AddWithValue("@inputUnitNo", residentUnit);

            MySqlDataReader rdr = cmd.ExecuteReader();
            DataTable tableTargetResident = new DataTable(); 
            tableTargetResident.Load(rdr); 
            rdr.Close();
            conn.Close();
            return tableTargetResident;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            conn.Close();
            return null;
        }
    }


    //add a package 
public void addPackage(){
        MySqlConnection conn = new MySqlConnection(connStr);
        Console.WriteLine("-------------------- Add a Package --------------------"); 
        Console.WriteLine("Please input the following information to record: ");
        Console.WriteLine("Please input Tracking Number: "); 
        string tracking_number = Console.ReadLine(); 
        Console.WriteLine("Please input Recipient's Name: "); 
        string recipient_name = Console.ReadLine(); 
        Console.WriteLine("Please input Resident's ID (999 if not a resident): "); 
        int id = int.Parse(Console.ReadLine()); 
        Console.WriteLine("Please input Unit Number: "); 
        int unit_number = int.Parse(Console.ReadLine()); 
        Console.WriteLine("Please input postal Agency:"); 
        string postalAgency = Console.ReadLine(); 
        Console.WriteLine("Please input Delivery Date: "); 
        string? deliveryDate = Console.ReadLine(); 
        
        try
        {  
            conn.Open();
            string procedure = "addPackage";
            MySqlCommand cmd = new MySqlCommand(procedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@inputTrackingNo", tracking_number);
           // cmd.Parameters["@nputTrackingNo"].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@inputRecipientName", recipient_name);
            cmd.Parameters.AddWithValue("@inputID", id); 
            cmd.Parameters.AddWithValue("@inputunitNumber", unit_number); 
           // cmd.Parameters["@inputRecipientName"].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@inputPostalAgency", postalAgency); 
            //cmd.Parameters["@inputPostalAgency"].Direction = ParameterDirection.Input; 
            cmd.Parameters.AddWithValue("@inputDeliveryDate", Convert.ToDateTime(deliveryDate)); 
           // cmd.Parameters["@inputDeliveryDate"].Direction = ParameterDirection.Input; 

            cmd.ExecuteNonQuery();
            Console.WriteLine("Package has been added to system."); 
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
public void PickupPackage(){
        MySqlConnection conn = new MySqlConnection(connStr);
        Console.WriteLine("-------------------- Notate a Package as Picked Up --------------------"); 
        Console.WriteLine("Please input a tracking number to search: ");
        string tracking_number = Console.ReadLine();
        try
        {  
            conn.Open();
            string procedure = "PickupPackage";
            MySqlCommand cmd = new MySqlCommand(procedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@inputTrackingNo", tracking_number);
            // cmd.Parameters.AddWithValue("@inputRecipientName", Package.recipient_name);
            // cmd.Parameters.AddWithValue("@inputResidentName", Package.full_name);

            cmd.ExecuteNonQuery();
            Console.WriteLine("Package has been updated as picked up."); 
            conn.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            conn.Close();
            return;
        }
    }

    //delete a package
public void deletePackage(){
        MySqlConnection conn = new MySqlConnection(connStr);
        Console.WriteLine("-------------------- Delete a Package --------------------"); 
        Console.WriteLine("Please input the following information to delete record: ");
        Console.WriteLine("Please enter Recipient Name: "); 
        string recipient_name = Console.ReadLine(); 
        Console.WriteLine("Please enter Tracking Number: ");
        string tracking_number = Console.ReadLine();
        try
        {  
            conn.Open();
            string procedure = "deletePackage";
            MySqlCommand cmd = new MySqlCommand(procedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@inputRecipientName", recipient_name);       
            cmd.Parameters.AddWithValue("@inputTrackingNo", tracking_number);
    
            cmd.ExecuteNonQuery();
            Console.WriteLine("Package has been removed from system."); 
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
public DataTable searchPackage(){
        MySqlConnection conn = new MySqlConnection(connStr);
        Console.WriteLine("-------------------- View Package History --------------------"); 
        Console.WriteLine("Please input Recipient Name: "); 
        string recipient_name = Console.ReadLine(); 
        Console.WriteLine("Please input Resdient's Unit Number: "); 
        int unit_number = int.Parse(Console.ReadLine()); 

        try
        {  
            conn.Open();
            string procedure = "searchPackage";
            MySqlCommand cmd = new MySqlCommand(procedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@inputRecipientName", recipient_name);
            cmd.Parameters.AddWithValue("@inputUnitNo", unit_number);

            MySqlDataReader rdr = cmd.ExecuteReader();
            DataTable tableTargetPackage = new DataTable(); 
            tableTargetPackage.Load(rdr); 
            rdr.Close();
            conn.Close();
            return tableTargetPackage;

            
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            conn.Close();
            return null;
        }
    }

    //see pending packages
public DataTable PendingPackages(){

    MySqlConnection conn = new MySqlConnection(connStr); 
    conn.Open(); 
    string SQL = "SELECT * FROM Pending_Packages;"; 
    MySqlCommand cmd = new MySqlCommand(SQL, conn); 

    MySqlDataReader rdr = cmd.ExecuteReader(); 

    DataTable Pending_Packages = new DataTable(); 
    Pending_Packages.Load(rdr); 
    rdr.Close(); 
    conn.Close(); 
    return Pending_Packages; 
}

    //see unknown pakcages

public DataTable UnknownPkg(){

    MySqlConnection conn = new MySqlConnection(connStr); 
    conn.Open(); 
    string SQL = "SELECT * FROM Unknown_Packages;"; 
    MySqlCommand cmd = new MySqlCommand(SQL, conn); 

    MySqlDataReader rdr = cmd.ExecuteReader(); 

    DataTable Unknown_Packages = new DataTable(); 
    Unknown_Packages.Load(rdr); 
    rdr.Close(); 
    conn.Close(); 
    return Unknown_Packages; 
}




}