using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentDataService.Attributes;
using StudentDataService.Contracts.Attributes;
using StudentDataService.Contracts.Request;
using StudentDataService.Contracts.Response;
using StudentDataService.Entity.Repository.Filter;
using StudentDataService.Entity.Repository.Student;
using StudentDataService.Entity.Specification.Student;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDataService.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ValidateModel]
    public class StudentController : BaseCRUIDController<Int32, StudentCRUID, StudentSelect>
    {
        public StudentController(ILogger<StudentController> logger, IStudentRepository studentRepository) : base(logger)
        { _studentRepository = studentRepository; }

        private readonly IStudentRepository _studentRepository;

        [HttpPost(nameof(Get))]
        public StudentsResponse Get([FromBody] StudentRequest request)
        {
            var filter = new StudentFilter()
            {
                Sex = (request.Sex.HasValue ? (Entity.POCO.ESex)request.Sex: new Entity.POCO.ESex?()),
                Firstname = request.Firstname,
                Middlename = request.Middlename,
                Surname = request.Surname,
                GroupName = request.GroupName,
            };

            if (request.Pagination != null)
            {
                filter.Pagination = new PaginationFilter()
                {
                    Index = request.Pagination.Index,
                    Size = request.Pagination.Size,
                };
            }

            var result = _studentRepository.Find(filter);

            return new StudentsResponse()
            {
                Students = result.Select(x => new StudentResponse()
                {
                    Key = x.Key,
                    Code = x.Code,
                    Sex = (Contracts.ESex)x.Sex,
                    Firstname = x.Firstname,
                    Middlename = x.Middlename,
                    Surname = x.Surname,
                    GroupNames = String.Join(",", x.GroupNames),
                }).ToArray()
            };
        }

        public override int Insert(StudentCRUID model)
        {
            var student = new Entity.POCO.Student()
            { 
                Code = model.Code,
                Firstname = model.Firstname,
                Middlename = model.Middlename,
                Sex = (Entity.POCO.ESex)model.Sex,
                Surname = model.Surname,
            };

            _studentRepository.Add(student);
            _studentRepository.SaveChanges();

            return student.Key;
        }

        public override StudentSelect GetByKey(int key)
        {
            var student = _studentRepository.FindByKey(key);

            StudentSelect result = null;

            if (student != null)
            {
                result = new StudentSelect()
                {
                    Key = student.Key,
                    Code = student.Code,
                    Firstname = student.Firstname,
                    Middlename = student.Middlename,
                    Sex = (Contracts.ESex)student.Sex,
                    Surname = student.Surname,
                };
            }

            return result;
        }

        public override IEnumerable<StudentSelect> GetAll()
        {
            var students = _studentRepository.GetAll();

            return students.Select(x => new StudentSelect()
            {
                Key = x.Key,
                Code = x.Code,
                Firstname = x.Firstname,
                Middlename = x.Middlename,
                Sex = (Contracts.ESex)x.Sex,
                Surname = x.Surname,
            }).ToArray();
        }

        public override bool Update(StudentSelect model)
        {
            var student = _studentRepository.FindByKey(model.Key);

            var result = (student != null);

            if (result)
            {
                if (!String.IsNullOrEmpty(model.Code))
                { student.Code = model.Code; }

                if (!String.IsNullOrEmpty(model.Firstname))
                { student.Firstname = model.Firstname; }

                if (!String.IsNullOrEmpty(model.Middlename))
                { student.Middlename = model.Middlename; }

                if (Enum.IsDefined(typeof(Contracts.ESex), model.Sex))
                { student.Sex = (Entity.POCO.ESex)model.Sex; }

                if (!String.IsNullOrEmpty(model.Surname))
                { student.Surname = model.Surname; }

                _studentRepository.Update(student);
                _studentRepository.SaveChanges();
            }

            return result;
        }

        public override void Remove(int key)
        {
            _studentRepository.Remove(new Entity.POCO.Student() { Key = key });
            _studentRepository.SaveChanges();
        }
    }
}
