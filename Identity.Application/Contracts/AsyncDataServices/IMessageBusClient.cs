﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Contracts.AsyncDataServices
{
   public interface IMessageBusClient
    {
        void PublishNewUser(Domain.Models.User user);
    }
}
