namespace BigSchool.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Course")]
    public partial class Course
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Course()
        {
            Attendances = new HashSet<Attendance>();
        }

        public int Id { get; set; }

        [Display(Name = "Lecturer Id")]
        [Required(ErrorMessage = "Lecturer Id không được để trống")]
        [StringLength(255, ErrorMessage = "LecturerId không được quá 255 ký tự")]
        public string LecturerId { get; set; }

        [Required(ErrorMessage = "Place không được để trống")]
        [StringLength(255, ErrorMessage = "Place không được quá 255 ký tự")]
        public string Place { get; set; }

        [Display(Name = "Date Time")]
        [Required(ErrorMessage = "Date Time không được để trống")]
        [FutureDate(ErrorMessage = "Date Time phải lớn hơn ngày hiện tại")]
        public DateTime DateTime { get; set; }

        [Display(Name = "Category ID")]
        [Required(ErrorMessage = "Category ID không được để trống")]
        public int CategoryId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Attendance> Attendances { get; set; }

        public virtual Category Category { get; set; }

        public List<Category> ListCategory = new List<Category>();

        public string Name, LectureName;

        public bool isLogin = false;
        public bool isShowGoing = false;
        public bool isShowFollow = false;
    }

    public class FutureDate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return (DateTime)value > DateTime.Now;
        }
    }
}
