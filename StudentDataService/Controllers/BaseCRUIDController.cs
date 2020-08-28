using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDataService.Controllers
{
    public abstract class BaseCRUIDController<TKey, TCUModel, TSelect> : ControllerBase
    {
        public BaseCRUIDController(ILogger<BaseCRUIDController<TKey, TCUModel, TSelect>> logger)
        { Logger = logger; }

        protected ILogger<BaseCRUIDController<TKey, TCUModel, TSelect>> Logger 
        { get; private set; }

        [HttpPost(nameof(Insert))]
        [Authorize(Roles = "admin")]
        public abstract TKey Insert([FromBody][Required] TCUModel model);

        [HttpGet(nameof(GetByKey))]
        [Authorize(Roles = "admin,user")]
        public abstract TSelect GetByKey([FromQuery][Required] TKey key);

        [HttpGet(nameof(GetAll))]
        [Authorize(Roles = "admin,user")]
        public abstract IEnumerable<TSelect> GetAll();

        [HttpPut(nameof(Update))]
        [Authorize(Roles = "admin")]
        public abstract bool Update([FromBody][Required] TSelect model);

        [HttpDelete(nameof(Remove))]
        [Authorize(Roles = "admin")]
        public abstract void Remove([FromQuery][Required] TKey key);
    }
}
