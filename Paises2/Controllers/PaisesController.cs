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
    public class PaisesController : ApiController
    {
        //[HttpPost]
        //public IHttpActionResult AddCountry(Models.PaisesRequest model)
        //{
        //    using (DataModel.PlanetaEntities db = new DataModel.PlanetaEntities())
        //    {
        //        var oPaises = new DataModel.Paises();
        //        oPaises.CountryName = model.CountryName;
        //        db.Paises.Add(oPaises);
        //        //db.Paises.AddRange();
        //        db.SaveChanges();
        //    }
        //    return Ok("Elemento Guardado");
        //}


        [HttpPost]
        public IHttpActionResult AddCountryList(List<PaisesRequest> lmodel)
        {

            if (lmodel is null)
            {
                return BadRequest();
            }
            try
            {
                using (DataModel.PlanetEntities db = new DataModel.PlanetEntities())
                {
                    var oCountry = new DataModel.Country();
                    foreach (Models.PaisesRequest x in lmodel)
                    {
                        oCountry.CountryName = x.CountryName;
                        db.Country.Add(oCountry);
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
        public IHttpActionResult GetCountry()
        {
            List<Models.PaisesViewModel> LPaises = new List<Models.PaisesViewModel>();
            try
            {
                using (DataModel.PlanetEntities db = new DataModel.PlanetEntities())
                {
                    LPaises = (from x in db.Country
                               select new Models.PaisesViewModel
                               {
                                   CountryId = (int)x.CountryId,
                                   CountryName = x.CountryName
                               }).ToList();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            return Ok(LPaises);
        }

        [HttpGet]
        public IHttpActionResult GetCountryById(int id)
        {
            Models.PaisesViewModel pais = new Models.PaisesViewModel();
            try
            {
                using (DataModel.PlanetEntities db = new DataModel.PlanetEntities())
                {
                    pais = db.Country.Where(x => x.CountryId.Equals(id))
                        .Select(x => new Models.PaisesViewModel
                        {
                            CountryId = (int)x.CountryId,
                            CountryName = x.CountryName
                        }
                        ).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            return Ok(pais);
        }

        [HttpPut]
        public IHttpActionResult PutCountry(PaisesViewModel model)
        {
            Country ExistCountry = new Country();
            try
            {
                using (DataModel.PlanetEntities db = new DataModel.PlanetEntities())
                {
                    ExistCountry = db.Country.Where(x => x.CountryId.Equals(model.CountryId))
                        .FirstOrDefault();

                    if (ExistCountry != null)
                    {
                        Console.WriteLine("entro el en if");
                        ExistCountry.CountryName = model.CountryName;
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
        public IHttpActionResult DeleteCountry(int id)
        {
            DataModel.Country ExistCountry = new DataModel.Country();
            try
            {
                using (DataModel.PlanetEntities db = new DataModel.PlanetEntities())
                {
                    ExistCountry = db.Country.Where(x => x.CountryId.Equals(id))
                        .FirstOrDefault();

                    if (ExistCountry != null)
                    {
                        db.Country.Remove(ExistCountry);
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
