﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cinar.Entities.Standart;

namespace Cinar.WinUI
{
    public interface IFormEntity
    {
        BaseEntity CurrentEntity { get; }
    }
}