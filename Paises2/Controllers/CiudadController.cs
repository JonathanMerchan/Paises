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
    public class CiudadController : ApiController
    {

        [HttpPost]
        public IHttpActionResult AddCitysList(List<CiudadViewModel> lmodel)
        {
            try
            {
                using (DataModel.PlanetEntities db = new DataModel.PlanetEntities())
                {

                    var oCitys = new DataModel.Citys();
                    foreach (CiudadViewModel x in lmodel)
                    {
                        oCitys.CountryId = x.CountryId;
                        oCitys.CityName = x.CityName;
                        db.Citys.Add(oCitys);
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
        public IHttpActionResult GetCitys()
        {
            List<CiudadViewModel> LCiudades = new List<CiudadViewModel>();
            try
            {

                using (DataModel.PlanetEntities db = new DataModel.PlanetEntities())
                {
                    LCiudades = (from x in db.Citys
                                 select new CiudadViewModel
                                 {
                                     CityId = (int)x.CityId,
                                     CityName = x.CityName
                                 }).ToList();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            return Ok(LCiudades);
        }

        [HttpGet]
        public IHttpActionResult GetCitysById(int id)
        {
            Models.CiudadViewModel pais = new Models.CiudadViewModel();
            try
            {
                using (DataModel.PlanetEntities db = new DataModel.PlanetEntities())
                {
                    pais = db.Citys.Where(x => x.CityId.Equals(id))
                        .Select(x => new Models.CiudadViewModel
                        {
                            CityId = (int)x.CityId,
                            CityName = x.CityName
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

        [HttpGet]
        public IHttpActionResult GetCitysByCountryIdList(int countryId)
        {
            List<CiudadViewModel> lCiudades = new List<CiudadViewModel>();
            try
            {
                using (PlanetEntities db = new PlanetEntities())
                {
                    lCiudades = db.Citys.Where(x => x.CountryId.Equals(countryId))
                        .Select(x => new CiudadViewModel
                        {
                            CityId = x.CityId,
                            CityName = x.CityName
                        }
                        ).ToList();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            return Ok(lCiudades);
        }

        [HttpPut]
        public IHttpActionResult PutCitys(CiudadViewModel model)
        {
            Citys ExistCitys = new Citys();
            try
            {
                using (DataModel.PlanetEntities db = new DataModel.PlanetEntities())
                {
                    ExistCitys = db.Citys.Where(x => x.CityId.Equals(model.CityId))
                        .FirstOrDefault();

                    if (ExistCitys != null)
                    {
                        Console.WriteLine("entro el en if");
                        ExistCitys.CityName = model.CityName;
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
        public IHttpActionResult DeleteCitys(int id)
        {
            DataModel.Citys ExistCitys = new DataModel.Citys();
            try
            {
                using (DataModel.PlanetEntities db = new DataModel.PlanetEntities())
                {
                    ExistCitys = db.Citys.Where(x => x.CityId.Equals(id))
                        .FirstOrDefault();

                    if (ExistCitys != null)
                    {
                        db.Citys.Remove(ExistCitys);
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
