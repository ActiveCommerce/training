define(["sitecore"], function (Sitecore) {
  var Reviews = Sitecore.Definitions.App.extend({
      initialized: function () {

      },
      
      executeQuery: function() {
          var productCode = this.SearchProductCode.get("text");
          var query = "";
          if (productCode) {
              query = "ProductCode=\"" + productCode + "\"";
          }
          this.ReviewsDataSource.set("where", query);
      }
  });

  return Reviews;
});