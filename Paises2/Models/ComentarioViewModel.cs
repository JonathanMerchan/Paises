using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Paises2.Models
{
    public class ComentarioViewModel
    {
        public int CommentId { get; set; }
        public int PlaceId { get; set; }
        public int CategoryId { get; set; }
        public string Comment { get; set; }
        public int N_likes { get; set; }
        public int UserId { get; set; }



    }
}