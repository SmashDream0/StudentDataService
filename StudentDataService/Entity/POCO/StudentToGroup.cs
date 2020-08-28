using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDataService.Entity.POCO
{
    [Table("tbl_" + nameof(StudentToGroup))]
    public class StudentToGroup
    {
        [Key]
        [Column("SG_Key")]
        public int Key
        { get; set; }

        /// <summary>
        /// Ключ группы
        /// </summary>
        [Column("SG_GroupKey")]
        [Required]
        public int GroupKey
        { get; set; }

        /// <summary>
        /// Ключ студента
        /// </summary>
        [Column("SG_StudentKey")]
        [Required]
        public int StudentKey
        { get; set; }


        [ForeignKey(nameof(GroupKey))]
        public virtual Group Group { get; set; }


        [ForeignKey(nameof(StudentKey))]
        public virtual Student Student { get; set; }
    }
}
