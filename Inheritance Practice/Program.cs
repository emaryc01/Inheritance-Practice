using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*Demonstrates: multiple levels of inheritance; sub classes overriding methods from base classes;  values passed to base constructors from
  sub class constructors; use of the ToString method; constructors; get-sets; method overloading via more than one field constructor; arrays of objects
  Emer Campbell
  2016*/

namespace Inheritance_Practice
{
    class Vehicle
    {
        private int vehicleNo;
        private String make;
        private String model;
        private String colour;
        private double ratePerDay;
        private int noRentalsToDate;

        public Vehicle() //default constructor initialises vehicle class with empty values
        {
            vehicleNo = 0;
            make = " ";
            model = " ";
            colour = " ";
            ratePerDay = 0;
            noRentalsToDate = 0;
        }

       //field constructor which allows all values of Vehicle to be initialised when object is created                                                                                
        public Vehicle(int vehicleNo, String make, String model, String colour, double ratePerDay, int noRentalsToDate)                                                                                 
        {   //each field is assigned revelent value from the parameter list
            this.vehicleNo = vehicleNo;
            this.make = make;
            this.model = model;
            this.colour = colour;
            this.ratePerDay = ratePerDay;
            this.noRentalsToDate = noRentalsToDate;
        }

        //field constructor which allows only VehicleNo, ratePerDay and noRentalsToDate values of Vehicle to be initialised when object is created 
        public Vehicle(int vehicleNo, double ratePerDay, int noRentalsToDate)
        {
            this.vehicleNo = vehicleNo;
            this.ratePerDay = ratePerDay;
            this.noRentalsToDate = noRentalsToDate;
        }
        //the three constructors are examples of method overloading as all three methods have the same name but a different number of parameters.

        /*The ToString method returns all property values as a single concatenated string.  Notice that vehicleNo, ratePerDay and noRentalsToDate 
           must all also be converted to strings inside this method. Also notice the use of the virtual keyword.  We will
           override this method in the subclasses*/
        
        public virtual string ToString()
        {
            return "VN is " + vehicleNo.ToString() + "make is " + make + "model is " + model + "colour is " + colour + "RPD is " + ratePerDay.ToString() + " NRTD is " + noRentalsToDate;
        }

        /*Calculates the total income from all rentals of this class. Notice the use of the virtual keyword.  We will
           override this method in the subclasses */
        public virtual double calc_Income()
        {
            return ratePerDay * noRentalsToDate;
        }

        /*Each of the private variable must be accessed by a get-set.  Notice the naming convention-property name but with a capital
          first letter.  Also notice no parameter - this is implicit*/
        public int VehicleNo
        {
            get { return vehicleNo; }
            set { vehicleNo = value; }
        }
        public string Make
        {
            get { return make; }
            set { make = value; }
        }
        public string Model
        {
            get { return model; }
            set { model = value; }
        }
        public string Colour
        {
            get { return colour; }
            set { colour = value; }
        }
        public double RatePerDay
        {
            get { return ratePerDay; }
            set { ratePerDay = value; }
        }
        public int NoRentalsToDate
        {
            get { return noRentalsToDate; }
            set { noRentalsToDate = value; }
        }
    }

    class Car : Vehicle //Car inherits from Vehicle class
    {
        String regNo; //Car also has 3 unique properties
        int power;
        char satNav;

        public Car()
            : base()//base (Vehicle) default constructor called when Car object initialised
        {
            regNo = " ";//only the 3 unique values need assigned.  The others are assigned by Vehicle default constructor
            power = 0;
            satNav = ' ';
        }

        public Car(int vehicleNo, String make, String model, String colour, double ratePerDay, int noRentalsToDate, String regNo, int power, char satNav)
            : base //base (Vehicle) field constructor called when Car object initialised and common properties passed via parameters shown below
                (vehicleNo, make, model, colour, ratePerDay, noRentalsToDate)
        {
            this.regNo = regNo;//only the 3 unique values need assigned.  The others are assigned by Vehicle default constructor
            this.power = power;
            this.satNav = satNav;

        }

        public override string ToString()//overrides ToString method from base (Vehicle) class
        {
            /*3 unique properties concatenated together but also concatenated with the string returned by the base (Vehicle) ToString method
              which is called from inside this version */
            return base.ToString() + "RegNo is " + regNo + "power is " + power + "satNav is " + satNav;
        }

        //base (Vehicle) calc_income overridden to include satNav cost
        public override double calc_Income()
        {
            double income;

            if (satNav == 'B')
            { income = base.calc_Income() * 1.04; }//base method called and value returned added to satNav cost
            else if (satNav == 'C')
            { income = base.calc_Income() * 1.075; }
            else income = base.calc_Income();

            return income;
        }

        //3 unique properties also need get-sets in order for them to be accessed outside the class
        public String RegNo
        {
            get { return regNo; }
            set { regNo = value; }
        }

        public int Power
        {
            get { return power; }
            set { power = value; }
        }

        public char SatNav
        {
            get { return satNav; }
            set { satNav = value; }
        }


    }

    //Van inherits from Car.  This means that also inherits all properties and methods from Vehicle.  This is an example of multi-level inheritance.
    class Van : Car
    {
        int capacity; //Van has 2 unique properties
        char wheelbase;
        
        public Van()/*In this example the base constructor is not called.  This means that only the 2 unique properties will be
                     assigned empty values*/
        {
            capacity = 0;
            wheelbase = ' ';
        }

        public Van(int vehicleNo, String make, String model, String colour, double ratePerDay, int noRentalsToDate, String regNo, int power, char satNav, int capacity, char wheelbase)
            : base /*base (Vehicle) field constructor called when Car object initialised and common properties passed via parameters shown below.
                    Note that all but the unique Van properties are passed in a single set of brackets.  The Car constructor will split them further 
                    and pass the relevent properties to Vehicle*/
                (vehicleNo, make, model, colour, ratePerDay, noRentalsToDate, regNo, power, satNav)
        {
            this.capacity = capacity; //unique properties assigned values
            this.wheelbase = wheelbase;
        }

        //get-sets for unique values
        public int Capacity
        {
            get { return capacity; }
            set { capacity = value; }
        }

        public char Wheelbase
        {
            get { return wheelbase; }
            set { wheelbase = value; }
        }
    }
    
    //Example of objects in use.  
    class Program
    {
        static void Main(string[] args)
        {
            //Car object created using field constructor and relevent methods called.
            Car C = new Car(1, "Hyundai", "i10", "red", 50.50, 2, "MFZ 1521", 1, 'B');
            Console.WriteLine("Full details of this car: {0}", C.ToString());//ToString method called for object C
            Console.WriteLine("The income for this car is £{0}", C.calc_Income());//calc_Income method called for object C
            
            Vehicle[] vehicleArray = new Vehicle[150];//array of vehicles created
            
            for(int i = 0; i<5; i++)
            {
                //5 objects instantiated with the shorter 2nd field constructor.  
                vehicleArray[i] = new Vehicle(i, i * 2, i * 3);
                Console.WriteLine("Please enter a make for car {0}: ", vehicleArray[i].VehicleNo);//get method used to output vehicleNo
                vehicleArray[i].Make = Console.ReadLine();//set method for make property used to assign value from user

            }

            double totalIncome = 0;
            for (int x = 0; x < 5; x++)
            {
                totalIncome += vehicleArray[x].calc_Income();//running total of income calculated for all vehicle objects.  
            }

            Console.WriteLine("total income for all items in the vehicle array : £{0}" , totalIncome);
            Console.Read();
        }
    }
}
