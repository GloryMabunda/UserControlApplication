using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserControlAPI.Data;
using UserControlAPI.Models;

namespace UserControlAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly DBService _service;
        public UsersController(DBService service)
        {
            _service = service;
        }


        [HttpGet("GetUserCount")]
        //GET: api/Users
        public async Task<ActionResult<int>> GetUserCount()
        {
            var data = await _service.GetUsers(null);
            if (data == null)
                return NotFound();
            else
                return data.Count();
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<List<Users>>> GetUsers()
        {
            var data = await _service.GetUsers(null);
            if (data == null)
                return NotFound();
            else
                return data;
        }
        
        // GET: api/Users/5]
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUser(int id)
        {
            var data = await _service.GetUsers(userid: id);
            if (data == null)
                return NotFound();
            else
                return data.FirstOrDefault();
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

        #region Group Permissions
        [HttpGet("GetGroupPermissions")]
        //GET: api/Users
        public async Task<ActionResult<List<GroupPermissions>>> GetGroupPermissions()
        {
            var data = await _service.GetGroupPermissions(null);
            if (data == null)
                return NotFound();
            else
                return data;
        }
        #endregion

        #region Groups
        [HttpGet("GetGroups")]
        //GET: api/Users
        public async Task<ActionResult<List<Groups>>> GetGroups()
        {
            var data = await _service.GetGroups(null);
            if (data == null)
                return NotFound();
            else
                return data;
        }
        #endregion
    }
}
