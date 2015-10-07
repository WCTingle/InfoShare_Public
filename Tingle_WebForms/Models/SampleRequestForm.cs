using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tingle_WebForms.Models
{
    public class SampleRequestForm
    {
        [Key]
        public int RecordId { get; set; }

        public DateTime Timestamp { get; set; }

        [MaxLength(100)]
        public string ProjectName { get; set; }

        [MaxLength(100)]
        public string ItemNumber { get; set; }

        [MaxLength(100)]
        public string Customer { get; set; }

        [MaxLength(100)]
        public string StyleNameColor { get; set; }

        [MaxLength(100)]
        public string AccountNumber { get; set; }

        [MaxLength(100)]
        public string Size { get; set; }

        [MaxLength(100)]
        public string Contact { get; set; }

        [MaxLength(100)]
        public string Quantity { get; set; }

        [MaxLength(100)]
        public string PhoneNumber { get; set; }

        public Boolean DealerAwareOfCost { get; set; }

        public Boolean DealerAwareOfFreight { get; set; }

        [MaxLength(100)]
        public string ShipToName { get; set; }

        [MaxLength(100)]
        public string ShipToAddress { get; set; }

        [MaxLength(100)]
        public string ShipToCity { get; set; }

        [MaxLength(100)]
        public string ShipToState { get; set; }

        [MaxLength(100)]
        public string ShipToZip { get; set; }

        [MaxLength(100)]
        public string ShipToPhone { get; set; }

        [MaxLength(2000)]
        public string AdditionalInfo { get; set; }

        [MaxLength(100)]
        public string RequestHandler { get; set; }

        [MaxLength(10)]
        public string Company { get; set; }

        public virtual Status Status { get; set; }

        public virtual SystemUsers SubmittedUser { get; set; }

        public virtual SystemUsers RequestedUser { get; set; }

        public virtual SystemUsers AssignedUser { get; set; }

        public virtual SystemUsers LastModifiedUser { get; set; }

        public DateTime? DueDate { get; set; }

        public virtual Priority Priority { get; set; }

        public DateTime LastModifiedTimestamp { get; set; }

        [MaxLength(100)]
        public string SubmittedByUser { get; set; }

        [MaxLength(100)]
        public string ModifiedByUser { get; set; }

        [MaxLength(1000)]
        public string CCFormToEmail { get; set; }

        [MaxLength(4000)]
        public string CompletedNotes { get; set; }

        [MaxLength(1000)]
        public string CCCompletedFormToEmail { get; set; }
    }
}