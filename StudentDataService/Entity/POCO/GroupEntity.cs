using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDataService.Entity.POCO
{
    [Table("tbl_" + nameof(GroupEntity))]
    public class GroupEntity
    {
        [Key]
        [Column("GP_Key")]
        public int Key
        { get; set; }

        /// <summary>
        /// Имя, <=25, обязательно
        /// </summary>
        [Column("GP_Name")]
        [Required]
        [StringLength(25)]
        public string Name 
        { get; set; }

        /// <summary>
        /// Список связанных студентов
        /// </summary>
        [InverseProperty(nameof(StudentToGroupEntity.Group))]
        public virtual ICollection<StudentToGroupEntity> Students { get; set; }
    }
}
