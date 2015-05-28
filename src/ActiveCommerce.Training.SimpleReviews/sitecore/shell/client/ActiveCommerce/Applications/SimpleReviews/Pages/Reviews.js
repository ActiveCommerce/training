define(["sitecore"], function (Sitecore) {
  var Reviews = Sitecore.Definitions.App.extend({
      initialized: function () {
          this.SearchStatusComboBox.set("items", [
                {
                    Status: "Any"
                },
                {
                    Status: "Approved"
                },
                {
                    Status: "Unapproved"
                }
          ]);
      },
      
      executeQuery: function() {
          var productCode = this.SearchProductCode.get("text");
          var query = "";
          if (productCode) {
              query = "ProductCode=\"" + productCode + "\"";
          }
          var status = this.SearchStatusComboBox.get("selectedValue");
          if (status && query && status != "Any") {
              query = query + " && ";
          }
          if (status && status == "Approved") {
              query = query + "Approved";
          }
          if (status && status == "Unapproved") {
              query = query + "!Approved";
          }
          this.ReviewsDataSource.set("where", query);
      }
  });

  return Reviews;
});