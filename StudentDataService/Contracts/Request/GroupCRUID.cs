using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDataService.Contracts.Request
{
    public class GroupCRUID
    {
        /// <summary>
        /// Имя, <=25, обязательно
        /// </summary>
        public string Name
        { get; set; }
    }
}
