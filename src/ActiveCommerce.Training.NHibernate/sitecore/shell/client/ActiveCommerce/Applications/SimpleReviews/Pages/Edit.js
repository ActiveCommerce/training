define(["sitecore"], function (Sitecore) {
    var Edit = Sitecore.Definitions.App.extend({
        
      //get the review we are editing from the query string
      initialized: function () {
          var id = Sitecore.Helpers.url.getQueryParameters(window.location.href)['id'];
          if (id) {
              this.getReview(id);
          }
      },
      
      //retrieve the review that we are editing from the server
      getReview: function (id) {
          var that = this;
          $.ajax({
              url: "/sitecore/shell/client/ActiveCommerce/api/SimpleReviews/ById",
              type: "POST",
              data: {
                  id: id
              },
              success: function (data) {
                  that.bindReview(data);
              }
          });
      },
      
      //populate form from the review model
      bindReview: function (review) {
          var reviewedOn = new Date(parseInt(review.ReviewedOn.substr(6)));
          this.ReviewedOnText.set("text", reviewedOn.getMonth() + '/' + reviewedOn.getDate() + '/' + reviewedOn.getFullYear());
          this.ApprovedCheckBox.set("isChecked", review.Approved);
          this.ProductCodeTextBox.set("text", review.ProductCode);
          this.ReviewTitleTextBox.set("text", review.ReviewTitle);
          this.ReviewerNameTextBox.set("text", review.ReviewerName);
          this.ReviewerEmailTextBox.set("text", review.ReviewerEmail);
          
          //there appears to be a SPEAK bug that prevents this from rendering?
          this.RatingSlider.set("selectedValue", review.Rating);
          
          this.ReviewTextArea.set("text", review.Review);
          this.ReviewIdHidden.set("text", review.ReviewId);
      },
      
      //save review data back to the server
      save: function() {
          var that = this;
          $.ajax({
              url: "/sitecore/shell/client/ActiveCommerce/api/SimpleReviews/Save",
              type: "POST",
              data: that.buildReviewModel(),
              success: function (data) {
                  var message = { text: 'Product review saved successfully.', actions: [], temporary: true, closable: true };
                  that.MessageBar.addMessage("notification", message);
              }
          });
      },

      //populate review model from the form
      buildReviewModel: function() {
          var review = {              
              ReviewId: this.ReviewIdHidden.get("text"),
              ProductCode: this.ProductCodeTextBox.get("text"),
              ReviewTitle: this.ReviewTitleTextBox.get("text"),
              ReviewerName: this.ReviewerNameTextBox.get("text"),
              ReviewerEmail: this.ReviewerEmailTextBox.get("text"),
              Rating: this.RatingSlider.get("selectedValue"),
              Review: this.ReviewTextArea.get("text"),
              Approved: this.ApprovedCheckBox.get("isChecked"),
          };
          return review;
      },
      
      deleteReview: function() {
        var that = this;
        $.ajax({
            url: "/sitecore/shell/client/ActiveCommerce/api/SimpleReviews/Delete",
            type: "POST",
            data: {
                reviewId: this.ReviewIdHidden.get("text")
            },
            success: function (data) {
                window.location.replace('/sitecore/client/ActiveCommerce/Applications/SimpleReviews/Pages/Reviews');
            }
        });
      }
  });

  return Edit;
});