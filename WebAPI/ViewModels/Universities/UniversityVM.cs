﻿using WebAPI.Model;

namespace WebAPI.ViewModels.Universities
{
    public class UniversityVM
    {
        public Guid? Guid { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
