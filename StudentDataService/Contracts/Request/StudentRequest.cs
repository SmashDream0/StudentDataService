using StudentDataService.Entity.POCO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDataService.Contracts.Request
{
    public class StudentRequest
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
        public PaginationRequest Pagination
        { get; set; }
    }
}
