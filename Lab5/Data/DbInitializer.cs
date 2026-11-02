using BaiThucHanh5_Net10.Models;

namespace BaiThucHanh5_Net10.Data;

public static class DbInitializer
{
    public static void Seed(SchoolContext context)
    {
        if (context.Majors.Any()) return;

        var majors = new List<Major>
        {
            new() { MajorName = "IT" },
            new() { MajorName = "Economics" },
            new() { MajorName = "Mathematics" }
        };

        context.Majors.AddRange(majors);
        context.SaveChanges();

        context.Learners.AddRange(
            new Learner { FirstName = "Carson", LastName = "Alexander", MajorID = majors[0].MajorID, EnrollmentDate = new DateTime(2025, 1, 9) },
            new Learner { FirstName = "Tuấn", LastName = "Trần", MajorID = majors[0].MajorID, EnrollmentDate = new DateTime(2025, 9, 9) },
            new Learner { FirstName = "Lan", LastName = "Nguyễn", MajorID = majors[1].MajorID, EnrollmentDate = new DateTime(2025, 2, 15) },
            new Learner { FirstName = "Minh", LastName = "Phạm", MajorID = majors[2].MajorID, EnrollmentDate = new DateTime(2025, 3, 20) }
        );
        context.SaveChanges();
    }
}
