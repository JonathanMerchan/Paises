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
    public class CategoriaController : ApiController
    {

        [HttpPost]
        public IHttpActionResult AddCategoryList(List<CategoriaViewModel> lmodel)
        {
            try
            {
                using (PlanetEntities db = new PlanetEntities())
                {
                    var oCategory = new Category();
                    foreach (CategoriaViewModel x in lmodel)
                    {
                        oCategory.CategoryId = x.CategoryId;
                        oCategory.CategoryName = x.CategoryName;
                        db.Category.Add(oCategory);
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
        public IHttpActionResult GetCategorys()
        {
            List<CategoriaViewModel> LCategoriaes = new List<CategoriaViewModel>();
            try
            {
                using (PlanetEntities db = new PlanetEntities())
                {
                    LCategoriaes = (from x in db.Category
                                    select new CategoriaViewModel
                                    {
                                        CategoryId = (int)x.CategoryId,
                                        CategoryName = x.CategoryName
                                    }).ToList();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            return Ok(LCategoriaes);
        }

        [HttpGet]
        public IHttpActionResult GetCategoryById(int Id)
        {
            CategoriaViewModel Categoria = new CategoriaViewModel();
            try
            {
                using (PlanetEntities db = new PlanetEntities())
                {
                    Categoria = db.Category.Where(x => x.CategoryId.Equals(Id))
                        .Select(x => new CategoriaViewModel
                        {
                            CategoryId = (int)x.CategoryId,
                            CategoryName = x.CategoryName
                        }
                        ).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            return Ok(Categoria);
        }

        [HttpPut]
        public IHttpActionResult PutCategory(CategoriaViewModel model)
        {
            Category ExistCategory = new Category();
            try
            {
                using (DataModel.PlanetEntities db = new DataModel.PlanetEntities())
                {
                    ExistCategory = db.Category.Where(x => x.CategoryId.Equals(model.CategoryId))
                        .FirstOrDefault();
                    if (ExistCategory != null)
                    {
                        Console.WriteLine("entro el en if");
                        ExistCategory.CategoryName = model.CategoryName;
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
        public IHttpActionResult DeleteCategory(int id)
        {
            DataModel.Category ExistCategory = new DataModel.Category();
            try
            {
                using (DataModel.PlanetEntities db = new DataModel.PlanetEntities())
                {
                    ExistCategory = db.Category.Where(x => x.CategoryId.Equals(id))
                        .FirstOrDefault();

                    if (ExistCategory != null)
                    {
                        db.Category.Remove(ExistCategory);
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
