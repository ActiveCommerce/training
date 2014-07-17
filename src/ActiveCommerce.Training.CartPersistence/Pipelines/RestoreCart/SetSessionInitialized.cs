﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActiveCommerce.Training.CartPersistence.Common;

namespace ActiveCommerce.Training.CartPersistence.Pipelines.RestoreCart
{
    public class SetSessionInitialized : IRestoreCartProcessor
    {
        public void Process(RestoreCartArgs args)
        {
            CartPersistenceContext.CartSessionInitialized = true;
        }
    }
}