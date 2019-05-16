using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        // GET api/values
        public IEnumerable<Student> getData(){

            var studentList = new List<Student>();
            Student student1 = new Student();
            student1.ID = 101;
            student1.Name = "Student 1";
            student1.Age = 17;
            student1.Major = "Science";
            student1.Address = "Atlanta";
            studentList.Add(student1);
            Student student2 = new Student();
            student2.ID = 102;
            student2.Name = "Student 2";
            student2.Age = 18;
            student2.Major = "Arts";
            student2.Address = "Atlanta";
            studentList.Add(student2);
            Student student3 = new Student();
            student3.ID = 103;
            student3.Name = "Student 3";
            student3.Age = 18;
            student3.Major = "Arts";
            student3.Address = "Atlanta";
            studentList.Add(student3);
            return studentList;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Student>> Get()
        {
		Console.WriteLine("Get Students start");
                return getData().ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Student> Get(int id)
        {
	    Console.WriteLine("Get Student start");
            var student = getData().Where(x=> x.ID == id).FirstOrDefault();
            
            return student;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
    public class Student
    {
        public string Name {get; set;}
        public int Age {get; set;}
        public string Major {get; set;}
        public string Address {get; set;}
        public int ID{get; set;}

    
    }
}
