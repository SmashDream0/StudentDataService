using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDataService.Entity.POCO
{
    [Table("User")]
    public class User
    {
        [Key]
        [Column("US_Key")]
        public int Key { get; set; }

        [Column("US_Login")]
        [StringLength(25)]
        public String Login { get; set; }

        [Column("US_Password")]
        [StringLength(25)]
        public String Password { get; set; }

        [Column("US_Role")]
        [StringLength(25)]
        public String Role { get; set; }
    }
}
