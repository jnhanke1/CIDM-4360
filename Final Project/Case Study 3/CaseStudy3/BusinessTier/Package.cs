namespace CaseStudy3;
class Package{
    public string tracking_number {get;set;} = string.Empty; 
    public int id {get;set;}
    
    public string recipient_name {get; set;} = string.Empty; 

    public int unit_number {get; set;} 
    public string postalAgency {get; set;} = string.Empty; 
    public DateTime deliveryDate {get; set; } 
    public DateTime pickupDate {get; set; }
    public string full_name {get; set;} = string.Empty; 
}
