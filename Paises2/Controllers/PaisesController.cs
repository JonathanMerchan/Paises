using Paises2.DataModel;
using Paises2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Paises2.Controllers
{
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
        public IHttpActionResult AddCountryList(List<Models.PaisesRequest> lmodel)
        {
            using (DataModel.PlanetaEntities db = new DataModel.PlanetaEntities())
            {
                var oPaises = new DataModel.Paises();
                foreach (Models.PaisesRequest x in lmodel)
                {
                    oPaises.CountryName = x.CountryName;
                    db.Paises.Add(oPaises);
                    db.SaveChanges();|
                }
                //db.Paises.AddRange((IEnumerable<DataModel.Paises>)lmodel);
            }
            return Ok("Coleccion Guardada");
        }




        [HttpGet]
        public IHttpActionResult GetCountry()
        {
            List<Models.PaisesViewModel> LPaises = new List<Models.PaisesViewModel>();
            using (DataModel.PlanetaEntities db = new DataModel.PlanetaEntities())
            {
                LPaises = (from x in db.Paises
                           select new Models.PaisesViewModel
                           {
                               CountryId = (int)x.CountryId,
                               CountryName = x.CountryName
                           }).ToList();
            }
            return Ok(LPaises);
        }

        [HttpGet]
        public IHttpActionResult GetCountryById(int id)
        {
            Models.PaisesViewModel pais = new Models.PaisesViewModel();

            using (DataModel.PlanetaEntities db = new DataModel.PlanetaEntities())
            {
                pais = db.Paises.Where(x => x.CountryId.Equals(id))
                    .Select(x => new Models.PaisesViewModel
                    {
                        CountryId = (int)x.CountryId,
                        CountryName = x.CountryName
                    }
                    ).FirstOrDefault();
            }
            return Ok(pais);
        }

        [HttpPut]
        public IHttpActionResult PutCountry(PaisesViewModel model)
        {
            Paises ExistCountry = new Paises();

            using (DataModel.PlanetaEntities db = new DataModel.PlanetaEntities())
            { 
                ExistCountry = db.Paises.Where(x => x.CountryId.Equals(model.CountryId))
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
            return Ok();
        }

        //[HttpPatch]

        [HttpDelete]
        public IHttpActionResult DeleteCountry(int id)
                    {
            DataModel.Paises ExistCountry = new DataModel.Paises();

            using (DataModel.PlanetaEntities db = new DataModel.PlanetaEntities())
            {
                ExistCountry = db.Paises.Where(x => x.CountryId.Equals(id))
                    .FirstOrDefault();

                if (ExistCountry != null)
                {
                    db.Paises.Remove(ExistCountry);
                    db.SaveChanges();
                }
                else
                {
                    return NotFound();
                }

            }
            return Ok();
        }



    }
}
