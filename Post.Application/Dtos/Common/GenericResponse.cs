﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Dtos.Common
{
    public class GenericResponse
    {
        public string Message { get; set; }
        public object Data { get; set; }
        public HttpStatusCode Status { get; set; } = HttpStatusCode.OK;
    }
}
