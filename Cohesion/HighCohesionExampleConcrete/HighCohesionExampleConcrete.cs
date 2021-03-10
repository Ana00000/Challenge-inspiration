using System;
using System.Collections.Generic;

class StudentEnrollments
{
    private List<Course> _activeCourses;
    private List<Course> _completedCourses;

    //Constructor omitted for brevity.
    public int GetActiveCoursesESPB()
    {
        int totalEspb = 0;
        foreach (Course course in _activeCourses)
        {
            totalEspb += course.ESPB;
        }
        return totalEspb;
    }

    public void EnrollInCourse(Course newCourse)
    {
        if (GetActiveCoursesESPB() + newCourse.ESPB > EnrollmentConstants.MAX_ALLOWED_ACTIVE_ESPB)
            throw new InsufficientESPBRemainingException();

        foreach (Course prerequisite in newCourse.PrerequisiteCourses)
        {
            if(_completedCourses.Contains(prerequisite)) continue;
            throw new PrerequisiteCourseNotCompletedException();
        }

        _activeCourses.Add(newCourse);
    }
}

internal class PrerequisiteCourseNotCompletedException : Exception
{
}

internal class InsufficientESPBRemainingException : Exception
{
}

internal class EnrollmentConstants
{
    public const int MAX_ALLOWED_ACTIVE_ESPB = 60;
}

internal class Course
{
    public int ESPB { get; set; }
    public List<Course> PrerequisiteCourses { get; }
    public string Status { get; set; }
}

//Active - what is the value of the LCOM metric of this class?

//Reflection - The Student class contains the name, surname, and dateOfBirth fields.
//It also has a 1 to 1 association with the StudentEnrollment class.
//We could move the fields and methods of the StudentEnrollment class to the Student class.
//This would reduce the class cohesion, but it would also reduce the number of objects we work with.
//What would be the benefits and drawbacks of such a transformation?
//Consider additional fields or methods you would most likely add to answer new requirements.