﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aclif
{
    public static partial class Help
    {
        public interface IHelper
        {
            void Help(int depth);

        }
    }
}
