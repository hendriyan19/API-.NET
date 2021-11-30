using System.Net;
using API.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Base
{


    public class BasesController<Entity, Repository, Key> : ControllerBase
        where Entity: class
        where Repository : IRepository<Entity, Key>
    {
        private readonly Repository repository;

        public BasesController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult<Entity> Get()
        {
            var result = repository.Get();
            return Ok(result);
        }

        [HttpPost]
        public ActionResult<Entity> Post(Entity entity)
        {

            var check = repository.Insert(entity);


            if (check != 0)
            {
                return Ok(new
                {
                    status = HttpStatusCode.OK,
                    data = repository.Insert(entity),
                    message = "Data Berhasil Di Insert"
                });

            }
            return NotFound(new
            {
                status = HttpStatusCode.NotFound,
                data = "",
                message = "Data Tidak bisa di Insert"
            });


        }

        [HttpPut]
        public ActionResult<Entity> Update(Entity entity, Key key)
        {

            var UpdateData = repository.Update(entity, key);

            if (UpdateData != 0)
            {
                return Ok(new
                {
                    status = HttpStatusCode.OK,
                    data = repository.Update(entity, key),
                    message = "Data Berhasil Di Update"
                });

            }
            return NotFound(new
            {
                status = HttpStatusCode.NotFound,
                data = "",
                message = "Data Tidak bisa di Update"
            });


        }

        [HttpDelete("{NIK}")]
        public ActionResult Delete(Key NIK)
        {
            repository.Delete(NIK);
            return Ok();
        }

    }
}
