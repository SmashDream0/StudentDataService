using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDataService.Contracts.Request
{
    public class JoinGroupStudent
    {
        public int StudentKey { get; set; }
        public int GroupKey { get; set; }
    }
}
