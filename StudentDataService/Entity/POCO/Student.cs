using StudentDataService.Entity.POCO.Attribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDataService.Entity.POCO
{
    [Table("tbl_" + nameof(Student))]
    public class Student
    {
        #region Свойства

        [Key]
        [Column("ST_Key")]
        public int Key
        { get; set; }

        /// <summary>
        /// Пол, обязательно
        /// </summary>
        [Column("ST_Sex", TypeName ="INT")]
        [Required]
        public ESex Sex 
        { get; set; }

        /// <summary>
        /// Отчество, <=40, обязательно
        /// </summary>
        [Column("ST_Surname")]
        [Required]
        [StringLength(40)]
        public string Surname
        { get; set; }

        /// <summary>
        /// Имя, <=40, обязательно
        /// </summary>  
        [Column("ST_Firstname")]
        [Required]
        [StringLength(40)]
        public string Firstname
        { get; set; }

        /// <summary>
        /// Отчество, <=60, не обязательно
        /// </summary>
        [Column("ST_Middlename")]
        [StringLength(60)]
        public string Middlename
        { get; set; }

        /// <summary>
        /// Уникальный идентификатор >=6<=16, не обязательно, уникально
        /// </summary>
        [Column("ST_Code")]
        [StringLength(16, MinimumLength = 6)]
        [UniqueKey]
        public String Code
        { get; set; }

        #endregion

        #region Навигационные свойства

        /// <summary>
        /// Список связанных групп
        /// </summary>
        [InverseProperty(nameof(StudentToGroup.Student))]
        public virtual ICollection<StudentToGroup> StudentToGroups { get; set; }

        #endregion
    }

    public enum ESex 
    {
        Male = 1, 
        Female = 2,
    }
}
