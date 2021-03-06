﻿using System.Collections.Generic;
using System.Reflection;

namespace Lottery.Infrastructure.Reflection
{
    public interface IAssemblyFinder
    {
        /// <summary>
        /// Gets all assemblies.
        /// </summary>
        /// <returns>List of assemblies</returns>
        List<Assembly> GetAllAssemblies();
    }
}