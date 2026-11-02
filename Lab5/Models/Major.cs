using System.ComponentModel.DataAnnotations;

namespace BaiThucHanh5_Net10.Models;

public class Major
{
    public int MajorID { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên chuyên ngành.")]
    [StringLength(100)]
    [Display(Name = "Tên chuyên ngành")]
    public string MajorName { get; set; } = string.Empty;

    public ICollection<Learner> Learners { get; set; } = new List<Learner>();
}
