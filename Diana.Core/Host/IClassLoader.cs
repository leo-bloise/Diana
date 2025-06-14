﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diana.Core.Host
{
    public interface IClassLoader
    {
        /// <summary>
        /// Loads all command types from the assembly that implement ICommand interface.
        /// </summary>
        IEnumerable<Type> LoadCommands();
    }
}
