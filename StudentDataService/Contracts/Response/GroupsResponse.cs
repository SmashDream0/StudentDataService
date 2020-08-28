using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDataService.Contracts.Response
{
    public class GroupsResponse
    { 
        public IEnumerable<GroupResponse> Groups { get; set; }
    }

    public class GroupResponse
    {
        public int Key { get; set; }

        public string Name { get; set; }

        public int StudentCount { get; set; }
    }
}
