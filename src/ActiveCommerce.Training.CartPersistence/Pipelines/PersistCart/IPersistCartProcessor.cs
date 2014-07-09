using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveCommerce.Training.CartPersistence.Pipelines.PersistCart
{
    interface IPersistCartProcessor
    {
        void Process(PersistCartArgs args);
    }
}
