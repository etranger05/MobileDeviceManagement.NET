
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Rest.Data;
using Rest.DTOs;
using Rest.Models;

namespace Rest.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;

        public UsersController(IRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        // GET api/users
        // GET api/users?name=<encoded-name>
        [HttpGet(Name="GetUserByName")]
        public ActionResult<UserReadDTO> GetUserByName(string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                return Ok(repository.GetUsers().Select(user => mapper.Map<UserReadDTO>(user)));
            }

            var user = repository.GetUserByName(name);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<UserReadDTO>(user));
        }

        // POST api/users
        [HttpPost]
        public ActionResult<UserReadDTO> CreateUser(UserCreateDTO userCreateDTO)
        {
            var userModel = mapper.Map<User>(userCreateDTO);
            
            repository.CreateUser(userModel);
            repository.SaveChanges();

            var userReadDTO = mapper.Map<UserReadDTO>(userModel);

            return CreatedAtRoute(nameof(GetUserByName), new {name = userReadDTO.Username}, userReadDTO);
        }

        // PUT api/users/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateUser(int id, UserUpdateDTO userUpdateDTO)
        {
            var userModel = repository.GetUserById(id);
            if (userModel == null)
            {
                return NotFound();
            }

            mapper.Map(userUpdateDTO, userModel);
            
            repository.UpdateUser(userModel);
            repository.SaveChanges();

            return NoContent();
        }

        // PATCH api/users/{id}
        [HttpPatch("{id}")]
        public ActionResult PatchUser(int id, JsonPatchDocument<UserUpdateDTO> patchDocument)
        {
            var userModel = repository.GetUserById(id);
            if (userModel == null)
            {
                return NotFound();
            }

            var userToPatch = mapper.Map<UserUpdateDTO>(userModel);
            patchDocument.ApplyTo(userToPatch, ModelState);

            if (!TryValidateModel(userToPatch))
            {
                return ValidationProblem(ModelState);
            }

            mapper.Map(userToPatch, userModel);

            repository.UpdateUser(userModel);
            repository.SaveChanges();

            return NoContent();
        }

        // DELETE api/users/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            var userToDelete = repository.GetUserById(id);
            if (userToDelete == null)
            {
                return NotFound();
            }

            repository.DeleteUser(userToDelete);
            repository.SaveChanges();

            return NoContent();
        }
        
    }
}