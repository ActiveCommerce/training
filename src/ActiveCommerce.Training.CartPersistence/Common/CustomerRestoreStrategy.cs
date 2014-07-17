using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActiveCommerce.Training.CartPersistence.Common
{
    public enum CustomerRestoreStrategy
    {
        None = 0,
        Merge = 1,
        Overwrite = 2
    }
}