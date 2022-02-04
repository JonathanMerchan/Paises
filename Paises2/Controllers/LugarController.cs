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
    public class LugarController : ApiController
    {

        [HttpPost]
        public IHttpActionResult AddPlaceList(List<LugarViewModel> lmodel)
        {
            try
            {
                using (DataModel.PlanetEntities db = new DataModel.PlanetEntities())
                {

                    var oPlace = new DataModel.Place();
                    foreach (LugarViewModel x in lmodel)
                    {
                        oPlace.CityId = x.CityId;
                        oPlace.PlaceName = x.PlaceName;
                        db.Place.Add(oPlace);
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
        public IHttpActionResult GetPlaces()
        {
            List<LugarViewModel> LLugares = new List<LugarViewModel>();
            try
            {
                using (DataModel.PlanetEntities db = new DataModel.PlanetEntities())
                {
                    LLugares = (from x in db.Place
                                select new LugarViewModel
                                {
                                    PlaceId = (int)x.PlaceId,
                                    CityId = x.CityId,
                                    PlaceName = x.PlaceName
                                }).ToList();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            return Ok(LLugares);
        }

        [HttpGet]
        public IHttpActionResult GetPlaceById(int Id)
        {
            LugarViewModel Lugar = new LugarViewModel();
            try
            {
                using (PlanetEntities db = new PlanetEntities())
                {
                    Lugar = db.Place.Where(x => x.PlaceId.Equals(Id))
                        .Select(x => new LugarViewModel
                        {
                            PlaceId = (int)x.PlaceId,
                            PlaceName = x.PlaceName
                        }
                        ).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            return Ok(Lugar);
        }

        [HttpGet]
        public IHttpActionResult GetPlaceByCityIdList(int cityId)
        {
            List<LugarViewModel> lLugares = new List<LugarViewModel>();
            try
            {
                using (PlanetEntities db = new PlanetEntities())
                {
                    lLugares = db.Place.Where(x => x.CityId.Equals(cityId))
                        .Select(x => new LugarViewModel
                        {
                            PlaceId = x.PlaceId,
                            CityId = x.CityId,
                            PlaceName = x.PlaceName
                        }
                        ).ToList();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            return Ok(lLugares);
        }

        [HttpPut]
        public IHttpActionResult PutPlace(LugarViewModel model)
        {
            Place ExistPlace = new Place();
            try
            {
                using (DataModel.PlanetEntities db = new DataModel.PlanetEntities())
                {
                    ExistPlace = db.Place.Where(x => x.CityId.Equals(model.CityId))
                        .FirstOrDefault();

                    if (ExistPlace != null)
                    {
                        Console.WriteLine("entro el en if");
                        ExistPlace.PlaceName = model.PlaceName;
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
        public IHttpActionResult DeletePlace(int id)
        {
            DataModel.Place ExistPlace = new DataModel.Place();
            try
            {
                using (DataModel.PlanetEntities db = new DataModel.PlanetEntities())
                {
                    ExistPlace = db.Place.Where(x => x.PlaceId.Equals(id))
                        .FirstOrDefault();

                    if (ExistPlace != null)
                    {
                        db.Place.Remove(ExistPlace);
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
