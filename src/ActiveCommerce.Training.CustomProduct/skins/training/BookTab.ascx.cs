using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ActiveCommerce.Training.CustomProduct.skins.training
{
    public partial class BookTab : ActiveCommerce.Web.skins.ProductDetails_Base, ActiveCommerce.Web.skins.ITabControl
    {
        protected BookProduct Book
        {
            get
            {
                return Model as BookProduct;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (Model == null)
            {
                this.Visible = false;
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