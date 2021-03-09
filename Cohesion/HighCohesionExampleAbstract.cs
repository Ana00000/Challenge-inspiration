class HighCohesionAbstract
{
    private string _a;
    private string _b;

    //Constructor omitted for brevity.
    public void DoA()
    {
        //Logic that uses _a
    }

    public void DoAB()
    {
        //Logic that uses _a and _b
    }
}
//Active - How would you change the class to achieve perfect cohesion?
//a - Add DoB that uses _b
//b - Add DoBA that uses _b and _a
//c - Change DoAB to only use _b
//d - Change DoA to use _a and _b
//e - All of the above.

//Reflective - Take a moment to imagine a software project that only has
//classes such as these, which contain several fields and bits of logic that work only with that data.
//What are the benefits and drawbacks that such a design brings?
//How do these cohesive objects interact with each other?