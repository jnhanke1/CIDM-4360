namespace Week5;

class Program
{
    static void Main(string[] args)
    {
        //create student object: 
        Student stu1 = new Student(); 
        stu1.studentID =111; 
        stu1.studentName = "Alice"; 

                        
        //print student info: 
        Console.WriteLine("Student ID: "+stu1.studentID); 
        Console.WriteLine("Student Name: "+stu1.studentName); 
        //Console.WriteLine("Student GPA: "+stu1.studentGPA);  -- when public

        Console.WriteLine("Initial Student GPA: "+stu1.getGPA()); //shows and pulls initial student GPA from encapsulation using access modifier
        stu1.setGPA(3.5f); //sets stu1 gpa at a new number using above access to private encapsulated data.
        Console.WriteLine("Current Student GPA: "+stu1.getGPA()); //calls GPA again after setting it to new number.
    }
}
