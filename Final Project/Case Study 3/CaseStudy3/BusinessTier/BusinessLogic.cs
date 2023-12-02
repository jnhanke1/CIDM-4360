namespace CaseStudy3;

using System.ComponentModel.Design;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using MySql.Data.MySqlClient;
class BusinessLogic
{
   
    static void Main(string[] args)
    {
        bool _continue = true;
        Staff staff_username;
        GuiTier appGUI = new GuiTier();
        DataTier database = new DataTier();
        Resident resident; 
        Package tracking_number; 
        Package recipient_name; 

        // start GUI
        staff_username = appGUI.Login();

       
        if (database.LoginCheck(staff_username)){
            Console.WriteLine($"Welcome {staff_username}!");
            while(_continue){
                int option  = appGUI.Dashboard(staff_username);
                switch(option)
                {
                    // list of all residents: 
                    case 1:
                    Console.WriteLine("List of All Residents");
                     DataTable tableResident = database.SearchAllResident(); 
                    appGUI.displayResidents(tableResident);
                        break;

                    // case 2: //search for a resident:
                    //     Console.WriteLine("Search for a Resident");
                    //     DataTable tableResidents = database.SearchResident(resident); 
                    //     if(tableResidents != null)
                    //         database.SearchResident(resident); 
                    // // Add a Package Received
                    // case 3:
                    //     Console.WriteLine("Add a Package Received");
                    //     database.addPackage(tracking_number); 
                    //     break;
                    // // Notate a Package Picked Up
                    // case 4:
                    //     Console.WriteLine("Notate a Package Picked Up");
                    //     database.PickupPackage(tracking_number); 
                    //     break;
                    // // Delete a Package
                    // case 5:
                    //     Console.WriteLine("Delete a Package");
                    //     database.deletePackage(tracking_number); 
                    //     break;      
                    // // Search Package History
                    // case 6:
                    //     Console.WriteLine("Search Package History");
                    //     database.searchPackage(recipient_name);
                    //     break; 
                    // case 7:
                    //     Console.WriteLine("See Pending Packages");
                    // //    DataTable Pending_Packages(DataTable tablePending_Packages){
                    // //     Console.WriteLine("-------------------- List of all Pending Packages --------------------"); 
                    // //     foreach(DataRow row in tablePending_Packages.Rows){
                    // //         Console.WriteLine($"Recipient: {row["recipient_name"]} \t Tracking Number: {row["tracking_number"]} \t Delivery Date: {row["deliveryDate"]} \t Postal Agency: {row["postalAgency"]} ");
                    // //     }

                    //         DataTable tablePackages = appGUI.pendingPackages(DataTable Pending_Packages); 
                    //         appGUI.pendingPackages(DataTable Pending_Packages);
                    //     break;                   
                    // case 8:
                    //     Console.WriteLine("See Unknown Packages");
                    //         DataTable unknownPkg = appGUI.unkownPackages(DataTable Unknown_Packages); 
                    //         appGUI.unknownPackages(DataTable Unknown_Packages);
                    //     break;   
                    case 9:
                        _continue = false;
                        Console.WriteLine("Log out, Goodbye.");
                        break;
                    // default: wrong input
                    default:
                        Console.WriteLine("Wrong Input");
                        break;}
                }
            
        
        }

        else{
                Console.WriteLine("Login Failed, Goodbye.");
        }        
    }   

}
  

