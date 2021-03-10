using System;
using System.Collections.Generic;

namespace ExamplesApp.Method
{
    class DoctorService
    {
        /// <summary>
        /// 1) By looking at the comments, extract the appropriate methods.
        /// 2) Identify similar code in the new methods and reduce code duplication by extracting a shared method.
        /// </summary>
        /// <param name="doctor"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        bool IsAvailable(Doctor doctor, Operation operation)
        {
            if (IsOnVacation(doctor, operation)) return false;
            if (IsInOtherOperation(doctor, operation)) return false;
            return true;
        }

        private static bool IsInOtherOperation(Doctor doctor, Operation operation)
        {
            if (doctor.GetOperations() != null)
            {
                foreach (Operation op in doctor.GetOperations())
                {
                    DateTime newStart = op.GetStartTime();
                    DateTime newEnd = op.GetEndTime();

                    if (DoesTimeOverlap(operation, newEnd, newStart)) return true;
                }
            }

            return false;
        }

        private static bool DoesTimeOverlap(Operation operation, DateTime newEnd, DateTime newStart)
        {
            if (operation.GetStartTime() > operation.GetEndTime())
                throw new InvalidOperationException("Invalid operation time frame.");
            //---s1---| oldStart |---e1---s2---e2---s3---| oldEnd |---e3---
            if (operation.GetStartTime() <= newEnd && operation.GetEndTime() >= newStart)
            {
                return true;
            }

            return false;
        }

        private static bool IsOnVacation(Doctor doctor, Operation operation)
        {
            if (doctor.GetVacationSlots() != null)
            {
                foreach (VacationSlot vacation in doctor.GetVacationSlots())
                {
                    DateTime newStart = vacation.GetStartTime();
                    DateTime newEnd = vacation.GetEndTime();

                    if (DoesTimeOverlap(operation, newEnd, newStart)) return true;
                }
            }

            return false;
        }
    }

    internal class VacationSlot
    {
        public DateTime GetStartTime()
        {
            throw new NotImplementedException();
        }

        public DateTime GetEndTime()
        {
            throw new NotImplementedException();
        }
    }

    internal class Operation
    {
        public DateTime GetStartTime()
        {
            throw new NotImplementedException();
        }

        public DateTime GetEndTime()
        {
            throw new NotImplementedException();
        }
    }

    internal class Doctor
    {
        public List<Operation> GetOperations()
        {
            throw new System.NotImplementedException();
        }

        public List<VacationSlot> GetVacationSlots()
        {
            throw new NotImplementedException();
        }
    }
}
