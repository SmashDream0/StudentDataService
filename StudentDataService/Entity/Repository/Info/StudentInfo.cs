using StudentDataService.Entity.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDataService.Entity.Repository.Info
{
    public class StudentInfo
    {
        public int Key
        { get; set; }

        /// <summary>
        /// Пол
        /// </summary>
        public ESex Sex
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
        /// Уникальный идентификатор
        /// </summary>
        public String Code
        { get; set; }

        /// <summary>
        /// Имена групп
        /// </summary>
        public IEnumerable<String> GroupNames
        { get; set; }
    }
}
