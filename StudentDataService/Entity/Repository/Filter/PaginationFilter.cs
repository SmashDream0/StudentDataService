using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDataService.Entity.Repository.Filter
{
    public class PaginationFilter
    {
        /// <summary>
        /// Индекс выборки, от нуля
        /// </summary>
        public int Index
        { get; set; }

        /// <summary>
        /// Объем выборки
        /// </summary>
        public int Size
        { get; set; }
    }
}
