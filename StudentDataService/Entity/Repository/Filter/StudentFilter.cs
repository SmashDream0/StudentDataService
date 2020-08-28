using StudentDataService.Entity.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDataService.Entity.Repository.Filter
{
    public class StudentFilter
    {
        /// <summary>
        /// Пол
        /// </summary>
        public ESex? Sex
        { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string Surname
        { get; set; }

        /// <summary>
        /// Имя
        /// </summary>  
        public string Firstname
        { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string Middlename
        { get; set; }

        /// <summary>
        /// Имя группы
        /// </summary>
        public string GroupName
        { get; set; }

        /// <summary>
        /// Пагинация
        /// </summary>
        public PaginationFilter Pagination
        { get; set; }
    }
}
