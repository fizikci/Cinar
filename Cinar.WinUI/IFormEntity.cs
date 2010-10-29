using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cinar.Entities;

namespace Cinar.WinUI
{
    public interface IFormEntity
    {
        BaseEntity CurrentEntity { get; }
    }
}