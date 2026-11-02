using System.ComponentModel.DataAnnotations;

namespace BaiThucHanh5_Net10.Models;

public class Learner
{
    public int LearnerID { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên.")]
    [StringLength(50)]
    [Display(Name = "Tên")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập họ.")]
    [StringLength(50)]
    [Display(Name = "Họ")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng chọn chuyên ngành.")]
    [Display(Name = "Chuyên ngành")]
    public int MajorID { get; set; }

    [Display(Name = "Ngày nhập học")]
    [DataType(DataType.Date)]
    public DateTime EnrollmentDate { get; set; } = DateTime.Today;

    public Major? Major { get; set; }
}
