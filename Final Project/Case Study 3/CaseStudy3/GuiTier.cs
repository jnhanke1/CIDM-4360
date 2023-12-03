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
        Console.WriteLine(" "); 
        Console.WriteLine("Please select an option to continue:");
        Console.WriteLine("\t 1. List all Residents in System"); 
        Console.WriteLine("\t 2. Search for a Resident");
        Console.WriteLine("\t 3. Add a Package Received");
        Console.WriteLine("\t 4. Notate a Package Picked Up");
        Console.WriteLine("\t 5. Delete a Package");
        Console.WriteLine("\t 6. Search Package History"); 
        Console.WriteLine("\t 7. See Pending Packages"); 
        Console.WriteLine("\t 8. See Unknown Packages"); 
        Console.WriteLine("\t 9. Log Out");
        int option = Convert.ToInt16(Console.ReadLine());
        return option;
    }


    // show resident records returned from database
    public void displayResidents(DataTable tableResident){
        Console.WriteLine("--------------- Residents List -------------------");
        foreach(DataRow row in tableResident.Rows){
           Console.WriteLine($"Resident {row["id"]}: {row["full_name"]} \t Unit Number: {row["unit_number"]} \t Email:{row["email"]}");
        }
        Console.WriteLine("---if recipient is not in Resident List, please list name as UNKNON with ID 999 and Unit 00 when entering a new package."); 
    }

        // show resident records returned from database
    public void displayPackages(DataTable tablePackage){
        Console.WriteLine("--------------- Package Record History -------------------");
        foreach(DataRow row in tablePackage.Rows){
           Console.WriteLine($"Recipient Name {row["recipient_name"]} | Tracking Number: {row["tracking_number"]} | Delivery Date: {row["deliveryDate"]} | Postal Agency: {row["postalAgency"]} | Date Picked Up: {row["pickupDate"]}");
        }
        
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
