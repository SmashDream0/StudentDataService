using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDataService.Contracts.Request
{
    public class StudentCRUID
    {
        /// <summary>
        /// Пол
        /// </summary>
        public ESex Sex
        { get; set; }

        /// <summary>
        /// Отчество, <=40, обязательно
        /// </summary>
        public string Surname
        { get; set; }

        /// <summary>
        /// Имя, <=40, обязательно
        /// </summary>        
        public string Firstname
        { get; set; }

        /// <summary>
        /// Отчество, <=60, не обязательно
        /// </summary>
        public string Middlename
        { get; set; }

        /// <summary>
        /// Уникальный идентификатор >=6<=16, не обязательно, уникально
        /// </summary>
        public string Code
        { get; set; }
    }
}
