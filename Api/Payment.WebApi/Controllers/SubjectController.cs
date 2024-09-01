using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payment.BusinessLayer.Abstract;
using Payment.EntityLayer.Concrete;

namespace Payment.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpGet]
        public IActionResult SubjectList()
        {
            var values = _subjectService.TGetList();
            return Ok(values);
        }
        [HttpPost]
        public IActionResult AddSubject(Subject subject)
        {
            _subjectService.TInsert(subject);
            return Ok();
        }
        [HttpDelete("{id}")]//Buradaki işlem önemli
        public IActionResult DeleteSubject(int id)
        {
            var values = _subjectService.TGetByID(id);
            _subjectService.TDelete(values);
            return Ok();
        }
        [HttpPut]
        public IActionResult UpdateSubject(Subject subject)
        {
            _subjectService.TUpdate(subject);
            return Ok();
        }
        [HttpGet("{id}")]
        public IActionResult GetSubject(int id)
        {
            var values = _subjectService.TGetByID(id);
            return Ok(values);
        }
    }
}
