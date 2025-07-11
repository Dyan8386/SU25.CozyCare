using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCare.ViewModels.DTOs
{
    public class ReviewDto
    {
        public int reviewId { get; set; }

        public int customerId { get; set; }

        public int detailId { get; set; }

        public string reviewTarget { get; set; }

        public int? rating { get; set; }

        public string comment { get; set; }

        public DateTime? reviewDate { get; set; }
    }

    public class CreateReviewDto
    {
        public int customerId { get; set; }
        public int detailId { get; set; }
        public string reviewTarget { get; set; }
        public int? rating { get; set; }
        public string comment { get; set; }
        public DateTime? reviewDate { get; set; }
    }
    public class UpdateReviewDto
    {
        public string reviewTarget { get; set; }
        public int? rating { get; set; }
        public string comment { get; set; }
        public DateTime? reviewDate { get; set; }
    }
    public class LockReviewDto
    {
        public bool? IsActive { get; set; }

    }
}
