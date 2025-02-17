﻿using System.Collections.Generic;
using Trsys.BackOffice.Abstractions;

namespace Trsys.Frontend.Web.Models.Admin
{
    public class UsersViewModel
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int TotalCount { get; set; }
        public List<UserDto> Items { get; set; }
    }
}
