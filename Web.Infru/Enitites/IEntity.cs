﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Infru.Enitites
{
    public interface IEntity<T>
    {
        public T Id { get; set; }
    }
}
