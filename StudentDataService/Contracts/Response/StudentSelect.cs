using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDataService.Contracts.Response
{
    public class StudentSelect
    {
        /// <summary>
        /// Ключ
        /// </summary>
        public Int32 Key
        { get; set; }

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
        public String Code
        { get; set; }
    }
}
