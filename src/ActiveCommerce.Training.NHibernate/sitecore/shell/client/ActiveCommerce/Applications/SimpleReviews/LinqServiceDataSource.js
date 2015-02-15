define(["sitecore"], function (Sitecore) {

    var model = Sitecore.Definitions.Models.ComponentModel.extend(
      {
          initialize: function (options) {
              this._super();

              this.set("serviceUrl", "");
              this.set("where", "");
              this.set("pageSize", 0);
              this.set("pageIndex", 0);
              this.set("totalItemsCount", 0);
              this.set("items", null);
              this.set("sorting", "");
              this.set("pagingMode", "appending");
              this.set("isBusy", false);
              this.set("hasItems", false);
              this.set("hasNoItems", true);
              this.set("hasMoreItems", false);

              this.on("change:where change:pageSize change:pageIndex change:sorting", this.refresh, this);

              this.isReady = false;
              this.pendingRequests = 0;
              this.lastPage = 0;
          },

          refresh: function () {

              this.set("pageIndex", 0);
              this.lastPage = 0;
              this.getItems();
          },

          next: function () {
              this.lastPage++;
              this.getItems();
          },

          getItems: function () {
              if (!this.isReady) {
                  return;
              }

              var search = this.get("where"),
                url = this.get("serviceUrl"),
                options = this.getOptions();

              if (!url) {
                  return;
              }

              this.pendingRequests++;
              this.set("isBusy", true);

              _sc.debug("LinqServiceDataSource request: '", url, "', query:", search);

              $.ajax({
                  url: url,
                  type: "POST",
                  data: {
                      where: search,
                      orderBy: options.sorting,
                      pageSize: options.pageSize,
                      pageIndex: options.pageIndex,
                      shopContext: "training"
                  },
                  context: this,
                  success: function (data) {
                      this.completed(data);
                  }
              });
          },

          getOptions: function () {
              var options = {}, fields;
              var pageSize = this.get("pageSize");
              if (pageSize) {
                  options.pageSize = pageSize;

                  if (this.get("pagingMode") == "appending") {
                      options.pageIndex = this.lastPage;
                  }
                  else {
                      options.pageIndex = this.get("pageIndex");
                  }
              }

              if (this.get("sorting") != "") {
                  options.sorting = this.get("sorting");
              }

              return options;
          },

          completed: function (data) {
              _sc.debug("LinqServiceDataSource received: ", data);

              var items = data.Items;
              var totalCount = data.TotalCount;

              if (this.get("pagingMode") == "appending" && this.lastPage > 0) {
                  items = this.get("items").concat(items);
                  this.set("items", items, { force: true });
              }
              else {
                  this.set("items", items, { force: true });
              }

              this.set("totalItemsCount", totalCount);
              this.set("hasItems", items && items.length > 0);
              this.set("hasNoItems", !items || items.length === 0);
              this.set("hasMoreItems", items.length < totalCount);

              this.pendingRequests--;
              if (this.pendingRequests <= 0) {
                  var self = this;
                  self.set("isBusy", false);

                  this.pendingRequests = 0;
              }

              this.trigger("itemsChanged");
          }
      }
    );

    var view = Sitecore.Definitions.Views.ComponentView.extend(
      {
          listen: _.extend({}, Sitecore.Definitions.Views.ComponentView.prototype.listen, {
              "refresh:$this": "refresh",
              "next:$this": "next"
          }),

          initialize: function (options) {
              this._super();

              var pageIndex, pageSize, fields;

              this.model.set("serviceUrl", this.$el.attr("data-sc-serviceUrl"));
              pageSize = parseInt(this.$el.attr("data-sc-pagesize"), 10) || 0;
              this.model.set("pageSize", pageSize);
              pageIndex = parseInt(this.$el.attr("data-sc-pageindex"), 10) || 0;
              this.model.set("pageIndex", pageIndex);
              this.model.set("sorting", this.$el.attr("data-sc-sorting"));
              this.model.set("where", this.$el.attr("data-sc-where") || "");
              this.model.set("serviceUrl", this.$el.attr("data-sc-serviceurl") || "");
              this.model.set("pagingMode", this.$el.attr("data-sc-pagingmode") || "appending"); // or paged
              
              this.model.isReady = true;
          },

          afterRender: function () {
              this.refresh();
          },

          refresh: function () {
              this.model.refresh();
          },

          next: function () {
              this.model.next();
          }
      }
    );

    Sitecore.Factories.createComponent("LinqServiceDataSource", model, view, ".sc-LinqServiceDataSource");
});