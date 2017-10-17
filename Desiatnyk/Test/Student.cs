using System;

namespace Test
{
    partial class Program
    {
        class Student : IComparable<Student>
        {
            public Student()
            {

            }
            public Student(int val)
            {
                Grad = val;
            }
            public int Grad { get; set; }
            public int CompareTo(Student other)
            {
                if (this.Grad > other.Grad)
                {
                    return 1;
                }
                else if (this.Grad < other.Grad)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
