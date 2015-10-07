using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tingle_WebForms.Models
{
    public class InventoryApprovalForm
    {
        [Key]
        public int RecordId { get; set; }

        public DateTime Timestamp { get; set; }

        public virtual Vendor Vendor { get; set; }
        
        [MaxLength(25)]
        public String Company { get; set; }

        [MaxLength(50)]
        public String PurchaseOrderNumber { get; set; }

        [MaxLength(200)]
        public String MaterialGroup { get; set; }

        public Decimal Cost { get; set; }

        [MaxLength(25)]
        public String ContainerNumber { get; set; }

        public virtual Priority Priority { get; set; }

        public DateTime? EstimatedShipDate { get; set; }

        public DateTime? EstimatedTimeOfArrival { get; set; }

        public virtual InventoryApprovalStatus Status { get; set; }

        public DateTime? ApprovedDate { get; set; }

        public virtual SystemUsers ApprovedBy { get; set; }

        public DateTime? ActualShipDate { get; set; }

        public DateTime? OrderDate { get; set; }

        public virtual SystemUsers OrderedBy { get; set; }

        public DateTime? ArrivalDate { get; set; }

        [MaxLength(25)]
        public String TimeToArrival { get; set; }

        public virtual SystemUsers ReceivedBy { get; set; }

        public DateTime? InvoiceDate { get; set; }

        public virtual SystemUsers InvoicedBy { get; set; }

        public DateTime LastModifiedTimestamp { get; set; }

        public virtual SystemUsers LastModifiedUser { get; set; }

        public virtual SystemUsers SubmittedUser { get; set; }

        public virtual SystemUsers CancelledBy { get; set; }

        public DateTime? CancelledDate { get; set; }

    }

    public class InventoryApprovalStatus
    {
        [Key]
        public int RecordId { get; set; }

        [MaxLength(25)]
        public String StatusDescription { get; set; }
    }

    public class InventoryNotificationEmails
    {
        [ScaffoldColumn(false)]
        [Key]
        public int RecordId { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public Int16 Status { get; set; }
    }

}