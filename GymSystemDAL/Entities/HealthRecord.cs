﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemDAL.Entities
{
    public class HealthRecord : BaseEntity
    {
     
        public decimal Weight { get; set; }
        public decimal Height { get; set; }

        public string BloodType { get; set; }

        public string? Note { get; set; }

    }
}
