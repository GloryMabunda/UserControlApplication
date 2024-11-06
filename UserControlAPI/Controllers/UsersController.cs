using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserControlAPI.Data;
using UserControlAPI.Models;

namespace UserControlAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly DBService _service;
        public UsersController(DBService service)
        {
            _service = service;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<List<Users>>> GetUsers()
        {
            var user = await _service.GetUsers(null);
            if (user == null)
                return NotFound();
            else
                return Ok(user.FirstOrDefault());
        }

        // GET: api/Users/5]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetUser(int id)
        {
            var user = await _service.GetUsers(userid: id);
            if (user == null)
                return NotFound();
            else
                return Ok(user.FirstOrDefault());
        }

            // Post: api/Users
            [HttpPost]
            public async Task<ActionResult> AddUser(Users user) 
            {
               if(user == null)
                    return BadRequest("Error occured while saving user details");

               var x = await _service.AddUser(user);
               return x ? Ok("User saved successfully") : BadRequest("Error occured while saving user details");
            }

        // Put: api/Users
        [HttpPut]
        public async Task<ActionResult> UpdateUser(Users user)
        {
            if (user == null)
                return BadRequest("Error occured while updating user details");

            var x = await _service.UpdateUser(user);
            return x ? Ok("User updated successfully") : BadRequest("Error occured while updating user details");
        }

        // Delete: api/Users/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var x = await _service.DeleteUser(id);
            return x ? Ok("User deleted") : BadRequest("Error occured while updating user details");
        }
    }
}
