﻿#pragma warning disable IDE0073

using System.Collections.Generic;

namespace FrameworkRepository.Tests.Models
{
    internal class User
    {
        public int Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Mail { get; set; }



        public virtual List<Basket>? Baskets { get; set; }

        public virtual List<History>? History { get; set; }
    }
}
