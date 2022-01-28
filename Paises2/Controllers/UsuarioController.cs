using Paises2.DataModel;
using Paises2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Paises2.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UsuarioController : ApiController
    {

        [HttpPost]
        public IHttpActionResult AddUserList(List<UsuarioViewModel> lmodel)
        {
            using (DataModel.PlanetEntities db = new DataModel.PlanetEntities())
            {
                try
                {
                    var oUser = new DataModel.User();
                    foreach (UsuarioViewModel x in lmodel)
                    {
                        oUser.Email = x.Email;
                        oUser.UserName = x.UserName;
                        oUser.Pass = x.Pass;
                        db.User.Add(oUser);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    return BadRequest();
                }
                //db.Paises.AddRange((IEnumerable<DataModel.Paises>)lmodel);
            }
            return Ok("Coleccion Guardada");
        }


        [HttpGet]
        public IHttpActionResult GetUsers()
        {
            List<UsuarioViewModel> LUsuarioes = new List<UsuarioViewModel>();
            using (PlanetEntities db = new PlanetEntities())
            {
                try
                {
                    LUsuarioes = (from x in db.User
                                  select new UsuarioViewModel
                                  {
                                      UserId = (int)x.UserId,
                                      Email = x.Email,
                                      Pass = x.Pass,
                                      UserName = x.UserName
                                  }).ToList();
                }
                catch (Exception e)
                {
                    return BadRequest();
                }
            }
            return Ok(LUsuarioes);
        }



        [HttpGet]
        public IHttpActionResult GetUserByName(string name)
        {
            UsuarioViewModel Usuario = new UsuarioViewModel();

            using (PlanetEntities db = new PlanetEntities())
            {
                try
                {
                    Usuario = db.User.Where(x => x.UserName.Equals(name))
                        .Select(x => new UsuarioViewModel
                        {
                            UserId = (int)x.UserId,
                            Email = x.Email,
                            UserName = x.UserName,
                            Pass = x.Pass
                        }
                        ).FirstOrDefault();
                }
                catch (Exception e)
                {
                    return BadRequest();
                }
            }
            return Ok(Usuario);
        }



        [HttpPut]
        public IHttpActionResult PutUser(UsuarioViewModel model)
        {
            User ExistUser = new User();

            using (DataModel.PlanetEntities db = new DataModel.PlanetEntities())
            {
                try
                {
                    ExistUser = db.User.Where(x => x.UserId.Equals(model.UserId))
                        .FirstOrDefault();

                    if (ExistUser != null)
                    {
                        Console.WriteLine("entro el en if");
                        ExistUser.UserName = model.UserName;
                        db.SaveChanges();
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception e)
                {
                    return BadRequest();
                }
            }
            return Ok();
        }


        [HttpDelete]
        public IHttpActionResult DeleteUser(int id)
        {
            DataModel.User ExistUser = new DataModel.User();

            using (DataModel.PlanetEntities db = new DataModel.PlanetEntities())
            {
                try
                {
                    ExistUser = db.User.Where(x => x.UserId.Equals(id))
                        .FirstOrDefault();

                    if (ExistUser != null)
                    {
                        db.User.Remove(ExistUser);
                        db.SaveChanges();
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception e)
                {
                    return BadRequest();
                }
            }
            return Ok();
        }



        [HttpGet]
        public IHttpActionResult ValidateUser(string email, string pass)
        {
            UsuarioViewModel VUser = new UsuarioViewModel();
            string tk = "";
            try
            {
                using (PlanetEntities db = new PlanetEntities())
                {
                    VUser = db.User.Where(x => x.Email.Equals(email) && x.Pass.Equals(pass))
                        .Select(x => new UsuarioViewModel
                        {
                            UserId = (int)x.UserId,
                            Email = x.Email,
                            UserName = x.UserName,
                            Pass = x.Pass
                        }
                        ).FirstOrDefault();

                    if (VUser != null)
                    {
                        tk = Tokenize.CreateToken(VUser.Email, VUser.Pass);
                    }
                    else
                    {
                        return BadRequest();
                    }

                }
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
            return Ok(tk);
        }



    }
}
