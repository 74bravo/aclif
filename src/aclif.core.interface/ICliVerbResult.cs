﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aclif
{
    public interface ICliVerbResult
    {
        bool CommandHandled { get; }
        int ResultCode { get; }
        string Message { get; }

    }
}
