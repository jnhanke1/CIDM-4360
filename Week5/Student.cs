class Student{
    public int studentID; 
    public string studentName; 
    private float studentGPA = 0; //encapsulated

    public float getGPA(){ //define method
        return studentGPA;
    }

    public void setGPA(float gpa){ //obtain encapsulated info.
        studentGPA = gpa; 
    }
}