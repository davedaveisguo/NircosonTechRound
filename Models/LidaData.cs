using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NTest.Models
{
    public class LidaData
    {
        [Key]
        public int Id { get; set; }

        public string ImageURL { get; set; }

        public double SlopeAngle { get; set; }

        public string ImageClock { get; set; }

        public string LidarClock { get; set; }

        public int PrependDist { get; set; }

        public double GroudDist { get; set; }
    }
}
