using System.Web.UI;
using ActiveCommerce.Web.Models;
using System;
using ActiveCommerce.Web.skins;
using Glass.Mapper.Sc.Web.Ui;

namespace ActiveCommerce.Training.CustomProduct.skins.training
{
    public partial class BookTab : AbstractGlassUserControl, ITabControl, IProductDetailsControl
    {
        public virtual ActiveCommerce.Products.Product Model { get; set; }
        public virtual ProductViewModel ViewModel { get; set; }

        protected BookProduct Book
        {
            get
            {
                var book = Model as BookProduct;
                if (book == null)
                {
                    throw new Exception("Product isn't a book -- double check your Unity registration for your custom product type!");
                }
                return book;
            }
        }

        public string Id
        {
            get { return "book-tab"; }
        }

        public string TitleKey
        {
            get { return "Product-Tab-Book"; }
        }

    }
}