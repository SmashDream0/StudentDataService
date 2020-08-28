using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDataService.Contracts.Request
{
    public class PaginationRequest
    {
        /// <summary>
        /// Индекс выборки, от нуля
        /// </summary>
        [Required]
        [Range(0, int.MaxValue)]
        public int Index
        { get; set; }

        /// <summary>
        /// Объем выборки
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int Size
        { get; set; }
    }
}
