using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDataService.Contracts.Response
{
    public class GroupSelect
    {
        public int Key
        { get; set; }

        /// <summary>
        /// Имя, <=25, обязательно
        /// </summary>
        public string Name
        { get; set; }
    }
}
