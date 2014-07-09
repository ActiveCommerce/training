using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveCommerce.Training.CartPersistence.Pipelines.RestoreCart
{
    interface IRestoreCartProcessor
    {
        void Process(RestoreCartArgs args);
    }
}
