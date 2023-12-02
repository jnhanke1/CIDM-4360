namespace CaseStudy3;

using System.ComponentModel.Design;
using System.Data;
using MySql.Data; 
using MySql.Data.MySqlClient;

class GuiTier{
    Staff staff = new Staff();
    DataTier database = new DataTier();

    // print login page  -working!
    public Staff Login(){
        Console.WriteLine("------Welcome to Course Management System------");
        Console.WriteLine("Please input Staff Username: ");
        staff.staff_username = Console.ReadLine(); 
        Console.WriteLine("Please input password: ");
        staff.staff_password = Console.ReadLine();
        return staff;
    }
    // print Dashboard after user logs in successfully -- working!
    public int Dashboard(Staff staff){
        DateTime localDate = DateTime.Now;
        Console.WriteLine("---------------Dashboard-------------------");
        Console.WriteLine($"Hello: {staff.staff_username}; Date/Time: {localDate.ToString()}");
        Console.WriteLine("Please select an option to continue:");
        Console.WriteLine("1. List all Residents in System"); 
        Console.WriteLine("2. Search for a Resident");
        Console.WriteLine("3. Add a Package Received");
        Console.WriteLine("4. Notate a Package Picked Up");
        Console.WriteLine("5. Delete a Package");
        Console.WriteLine("6. Search Package History"); 
        Console.WriteLine("7. See Pending Packages"); 
        Console.WriteLine("8. See Unknown Packages"); 
        Console.WriteLine("9. Log Out");
        int option = Convert.ToInt16(Console.ReadLine());
        return option;
    }

    // show resident records returned from database
    public void displayResidents(DataTable tableResidents){
        Console.WriteLine("--------------- Residents List -------------------");
        foreach(DataRow row in tableResidents.Rows){
           Console.WriteLine($"Resident {row["id"]}: {row["full_name"]} \t Unit Number: {row["unit_number"]} \t Email:{row["email"]}");
        }
        Console.WriteLine("---if recipient is not in Resident List, please list name as UNKNON with ID 999 and Unit 00 when entering a new package."); 
    }

    // show pending packages returned from View
    public void pendingPackages(DataTable Pending_Packages){
        Console.WriteLine("--------------- Pending Packages -------------------");
        foreach(DataRow row in Pending_Packages.Rows){
            Console.WriteLine($"Recipient: {row["recipient_name"]} \t Tracking Number: {row["tracking_number"]} \t Delivered: {row["deliverydate"]} \t Postal Agency: {row["postalAgency"]}");
        }
    }


    // show unknown packages returned from View
    public void unkownPackages(DataTable Unknown_Packages){
        Console.WriteLine("--------------- Unknown Packages (to be returned to Postal Agency) -------------------");
        foreach(DataRow row in Unknown_Packages.Rows){
            Console.WriteLine($"Recipient: {row["recipient_name"]} \t Tracking Number: {row["tracking_number"]} \t Delivered: {row["deliverydate"]} \t Postal Agency: {row["postalAgency"]}");
        }
    }
}
