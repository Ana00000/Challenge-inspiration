class LowCohesionAbstract
{
    private string _a;
    private string _b;
    private string _c;

    //Constructor omitted for brevity.
    public void DoA()
    {
        //Logic that uses _a
    }

    public void DoAB()
    {
        //Logic that uses _a and _b
    }

    public void DoC1()
    {
        //Logic that uses _c
    }

    public void DoC2()
    {
        //Logic that uses _c
    }
}
//Active: While keeping all fields and methods, refactor the code to have
//one or more highly-cohesive classes.

//Reflective: Consider the following scenario. This class evolves as new requirements are resolved.
//In time, the logic that manipulates _c shares some similarity with the logic that manipulates _a or _b.
//To avoid duplicate code, the programmer extracts a shared private method, used by both pieces of logic.
//What are the benefits and disadvantages of this refactoring operation?
//Tip: Consider what would happen when new requirements for change arrive.


//NOTE: For reflective learners (especially mod-high) a warning message before looking at the answer ->
//The Smart Tutor considers you a reflective learner.
//To take full advantage of your learning style, make sure you've written down your answer (at least in short bullets).
//Only then should you reveal the answer and mark how aligned your thinking was.
//NOTE: Emphasize the importance of honesty for our Tutor to become effective.