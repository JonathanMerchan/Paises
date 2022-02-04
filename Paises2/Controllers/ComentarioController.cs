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
    public class ComentarioController : ApiController
    {

        [HttpPost]
        public IHttpActionResult AddCommentList(List<ComentarioViewModel> lmodel)
        {
            try
            {
                using (PlanetEntities db = new PlanetEntities())
                {


                    var oComment = new Comment();
                    foreach (ComentarioViewModel x in lmodel)
                    {
                        oComment.CommentId = x.CommentId;
                        oComment.PlaceId = x.PlaceId;
                        oComment.CategoryId = x.CategoryId;
                        oComment.Comment1 = x.Comment;
                        oComment.UsarioId = x.UserId;
                        db.Comment.Add(oComment);
                        db.SaveChanges();
                    }

                    //db.Paises.AddRange((IEnumerable<DataModel.Paises>)lmodel);
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            return Ok("Coleccion Guardada");
        }

        [HttpGet]
        public IHttpActionResult GetComments()
        {
            List<ComentarioViewModel> LComentarioes = new List<ComentarioViewModel>();
            try
            {
                using (PlanetEntities db = new PlanetEntities())
                {
                    LComentarioes = (from x in db.Comment
                                     select new ComentarioViewModel
                                     {
                                         CommentId = (int)x.CommentId,
                                         PlaceId = x.PlaceId,
                                         CategoryId = x.CategoryId,
                                         Comment = x.Comment1,
                                         N_likes = x.N_likes,
                                         UserId = x.UsarioId
                                     }).ToList();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            return Ok(LComentarioes);
        }

        [HttpGet]
        public IHttpActionResult GetCommentById(int Id)
        {
            ComentarioViewModel Comentario = new ComentarioViewModel();
            try
            {
                using (PlanetEntities db = new PlanetEntities())
                {
                    Comentario = db.Comment.Where(x => x.CommentId.Equals(Id))
                        .Select(x => new ComentarioViewModel
                        {
                            CommentId = (int)x.CommentId,
                            PlaceId = x.PlaceId,
                            CategoryId = x.CategoryId,
                            Comment = x.Comment1,
                            N_likes = x.N_likes,
                            UserId = x.UsarioId
                        }
                        ).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            return Ok(Comentario);
        }

        [HttpGet]
        public IHttpActionResult GetCommentByplaceIdList(int placeId)
        {
            List<ComentarioViewModel> lComentarioes = new List<ComentarioViewModel>();
            try
            {
                using (PlanetEntities db = new PlanetEntities())
                {
                    lComentarioes = db.Comment.Where(x => x.PlaceId.Equals(placeId))
                        .Select(x => new ComentarioViewModel
                        {
                            CommentId = (int)x.CommentId,
                            PlaceId = x.PlaceId,
                            CategoryId = x.CategoryId,
                            Comment = x.Comment1,
                            N_likes = x.N_likes,
                            UserId = x.UsarioId
                        }
                        ).ToList();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            return Ok(lComentarioes);
        }


        [HttpPut]
        public IHttpActionResult PutComment(ComentarioViewModel model)
        {
            Comment ExistComment = new Comment();
            try
            {
                using (DataModel.PlanetEntities db = new DataModel.PlanetEntities())
                {
                    ExistComment = db.Comment.Where(x => x.PlaceId.Equals(model.PlaceId))
                        .FirstOrDefault();

                    if (ExistComment != null)
                    {
                        Console.WriteLine("entro el en if");
                        ExistComment.Comment1 = model.Comment;
                        db.SaveChanges();
                    }
                    else
                    {
                        return NotFound();
                    }

                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            return Ok();
        }

        //[HttpPatch]

        [HttpDelete]
        public IHttpActionResult DeleteComment(int id)
        {
            DataModel.Comment ExistComment = new DataModel.Comment();
            try
            {
                using (DataModel.PlanetEntities db = new DataModel.PlanetEntities())
                {
                    ExistComment = db.Comment.Where(x => x.CommentId.Equals(id))
                        .FirstOrDefault();

                    if (ExistComment != null)
                    {
                        db.Comment.Remove(ExistComment);
                        db.SaveChanges();
                    }
                    else
                    {
                        return NotFound();
                    }

                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
