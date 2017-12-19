﻿using System;

namespace DatabaseHandler.Entities
{
    abstract class Base
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsActive { get; set; }
    }
}