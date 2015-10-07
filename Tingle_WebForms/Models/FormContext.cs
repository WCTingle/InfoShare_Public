using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Tingle_WebForms.Models
{
    public class FormContext : DbContext
    {
        public FormContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<ExpeditedOrderForm> ExpeditedOrderForms { get; set; }

        public DbSet<PriceChangeRequestForm> PriceChangeRequestForms { get; set; }

        public DbSet<OrderCancellationForm> OrderCancellationForms { get; set; }

        public DbSet<HotRushForm> HotRushForms { get; set; }

        public DbSet<LowInventoryForm> LowInventoryForms { get; set; }

        public DbSet<SampleRequestForm> SampleRequestForms { get; set; }

        public DbSet<DirectOrderForm> DirectOrderForms { get; set; }

        public DbSet<RequestForCheckForm> RequestForCheckForms { get; set; }

        public DbSet<MustIncludeForm> MustIncludeForms { get; set; }

        public DbSet<ExpediteCode> ExpediteCodes { get; set; }

        public DbSet<TForm> TForms { get; set; }

        public DbSet<EmailAddress> EmailAddresses { get; set; }

        public DbSet<Status> Statuses { get; set; }

        public DbSet<Priority> Priorities { get; set; }

        public DbSet<UserRoles> UserRoles { get; set; }

        public DbSet<SystemUsers> SystemUsers { get; set; }

        public DbSet<UserStatus> UserStatuses { get; set; }

        public DbSet<FavoriteForm> FavoriteForms { get; set; }

        public DbSet<PurchaseOrderStatus> POStatuses { get; set; }

        public DbSet<FormPermissions> FormPermissions { get; set; }

        public DbSet<NotificationEmailAddress> NotificationEmailAddresses { get; set; }

        public DbSet<Comments> Comments { get; set; }

        public DbSet<RequestNotifications> RequestNotifications { get; set; }

        public DbSet<PriceChangeRequestProducts> PriceChangeRequestProducts { get; set; }

        public DbSet<Plant> Plants { get; set; }

        public DbSet<Warehouse> Warehouses { get; set; }

        public DbSet<SkuQuantity> SkuQuantityItems { get; set; }

        public DbSet<CannotWaitForContainerForm> CannotWaitForContainerForms { get; set; }

        public DbSet<Vendor> Vendors { get; set; }

        public DbSet<InventoryApprovalForm> InventoryApprovalItems { get; set; }

        public DbSet<InventoryApprovalStatus> InventoryApprovalStatuses {get;set;}

        public DbSet<InventoryNotificationEmails> InventoryNotificationEmailAddresses { get; set; }

        public DbSet<InventoryApprovalNotifications> InventoryApprovalNotifications{ get; set; }

        public DbSet<InventoryComments> InventoryApprovalComments { get; set; }

        public DbSet<UserRequestAssociation> UserRequests { get; set; }

        public DbSet<UserAssignmentAssociation> UserAssignments { get; set; }

    }
}