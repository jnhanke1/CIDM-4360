namespace CaseStudy3;

using System.ComponentModel.Design;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;

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
            Console.WriteLine($" "); 
            while(_continue){
                int option  = appGUI.Dashboard(staff_username);
                switch(option)
                {
                    // list of all residents: 
                    case 1:
                    Console.WriteLine("> List All Residents");
                     DataTable tableResident = database.SearchAllResident(); 
                    appGUI.displayResidents(tableResident);
                        break;

                     case 2: //search for a resident:
                        Console.WriteLine("> Search for a Resident by name or unit number: ");
                        DataTable tableTargetResident = database.SearchResident(); 
                         if(tableTargetResident != null)
                            appGUI.displayResidents(tableTargetResident); 
                        break; 
                    // // Add a Package Received
                     case 3:
                         Console.WriteLine("> Add a Package Received");
                         database.addPackage(); 
                         break;
                    // // Notate a Package Picked Up
                     case 4:
                         Console.WriteLine("> Notate a Package Picked Up");
                         database.PickupPackage(); 
                         break;
                    // // Delete a Package
                     case 5:
                         Console.WriteLine("> Delete a Package");
                         database.deletePackage(); 
                         break;      
                    // // Search Package History
                     case 6:
                         Console.WriteLine("> Search Package History");
                         DataTable tableTargetPackage = database.searchPackage();                          
                         if(tableTargetPackage != null)
                            appGUI.displayPackages(tableTargetPackage); 
                        break; 
                     case 7:
                         Console.WriteLine("> See Pending Packages");
                        DataTable Pending_Packages = database.PendingPackages(); 
                        appGUI.pendingPackages(Pending_Packages);
                         break;                   
                     case 8:
                         Console.WriteLine("> See Unknown Packages");
                        DataTable UnknownPkg = database.UnknownPkg(); 
                        appGUI.unkownPackages(UnknownPkg); 
                         break;   
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
  

