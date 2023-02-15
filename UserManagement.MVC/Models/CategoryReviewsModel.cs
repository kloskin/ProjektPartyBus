using System.Collections.Generic;

namespace UserManagement.MVC.Models
{
    public class CategoryReviewsModel
    {
        public RentCategory RentCategory { get; set; }
        public List<Review> Reviews { get; set; }

        public CategoryReviewsModel()
        {
            this.RentCategory = new RentCategory();
            this.Reviews = new List<Review>();
        }
    }
}
