namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Data;
    using Tingle_WebForms.Models;
    using Tingle_WebForms.Forms;

    internal sealed class Configuration : DbMigrationsConfiguration<Tingle_WebForms.Models.FormContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Tingle_WebForms.Models.FormContext context)
        {
            //Seed Roles
            UserRoles roleUser = new UserRoles{RoleName = "User",RoleDescription = "User can submit forms."};
            UserRoles roleReportsUser = new UserRoles{RoleName = "ReportsUser",RoleDescription = "User can view reports, but not edit them."};
            UserRoles roleReportsAdmin = new UserRoles{RoleName = "ReportsAdmin",RoleDescription = "User can view and edit reports."};
            UserRoles roleSuperUser = new UserRoles { RoleName = "SuperUser", RoleDescription = "User has full administrative rights." };
            UserRoles roleDeveloper = new UserRoles { RoleName = "Developer", RoleDescription = "Developer" };

            context.UserRoles.AddOrUpdate(u => u.RoleName, roleUser);
            context.UserRoles.AddOrUpdate(u => u.RoleName, roleReportsUser);
            context.UserRoles.AddOrUpdate(u => u.RoleName, roleReportsAdmin);
            context.UserRoles.AddOrUpdate(u => u.RoleName, roleSuperUser);
            context.UserRoles.AddOrUpdate(u => u.RoleName, roleDeveloper);


            //Seed Forms
            TForm expeditedOrder = new TForm
            {
                FormCreator = "Admin",
                FormName = "Expedited Order",
                FormNameHtml = "Expedited Order",
                FormUrl = "ExpeditedOrderForm.aspx",
                Notes = "Expedited Order Form",
                Status = 1,
                Timestamp = DateTime.Now
            };

            TForm priceChangeRequest = new TForm
            {
                FormCreator = "Admin",
                FormName = "Price Change Request",
                FormNameHtml = "Price Change<br />Request",
                FormUrl = "PriceChangeRequestForm.aspx",
                Notes = "Price Change Request Form Notes",
                Status = 0,
                Timestamp = DateTime.Now
            };

            TForm orderCancellation = new TForm
            {
                FormCreator = "Admin",
                FormName = "Order Cancellation",
                FormNameHtml = "Order Cancellation",
                FormUrl = "OrderCancellationForm.aspx",
                Notes = "Order Cancellation Form Notes",
                Status = 0,
                Timestamp = DateTime.Now
            };

            TForm hotRushForm = new TForm
            {
                FormCreator = "Admin",
                FormName = "Hot Rush",
                FormNameHtml = "Hot Rush",
                FormUrl = "HotRushForm.aspx",
                Notes = "Hot Rush Form Notes",
                Status = 0,
                Timestamp = DateTime.Now
            };

            TForm lowInventory = new TForm
            {
                FormCreator = "Admin",
                FormName = "Low Inventory",
                FormNameHtml = "Low Inventory",
                FormUrl = "LowInventoryForm.aspx",
                Notes = "Low Inventory Form Notes",
                Status = 0,
                Timestamp = DateTime.Now
            };

            TForm sampleRequest = new TForm
            {
                FormCreator = "Admin",
                FormName = "Sample Request",
                FormNameHtml = "Sample Request",
                FormUrl = "SampleRequestForm.aspx",
                Notes = "Sample Request Form Notes",
                Status = 0,
                Timestamp = DateTime.Now
            };

            TForm directOrder = new TForm
            {
                FormCreator = "Admin",
                FormName = "Direct Order",
                FormNameHtml = "Direct Order",
                FormUrl = "DirectOrderForm.aspx",
                Notes = "Direct Order Form Notes",
                Status = 0,
                Timestamp = DateTime.Now
            };

            TForm requestForCheck = new TForm
            {
                FormCreator = "Admin",
                FormName = "Request For Check",
                FormNameHtml = "Request For<br />Check",
                FormUrl = "RequestForCheckForm.aspx",
                Notes = "Request For Check Form Notes",
                Status = 0,
                Timestamp = DateTime.Now
            };

            TForm mustIncludeForm = new TForm
            {
                FormCreator = "Admin",
                FormName = "Must Include",
                FormNameHtml = "Must Include",
                FormUrl = "MustIncludeForm.aspx",
                Notes = "Must Include Form Notes",
                Status = 0,
                Timestamp = DateTime.Now
            };

            TForm cannotWaitForContainerForm = new TForm
            {
                FormCreator = "Admin",
                FormName = "Cannot Wait For Container",
                FormNameHtml = "Cannot<br />Wait For<br />Container",
                FormUrl = "CannotWaitForContainerForm.aspx",
                Notes = "Cannot Wait For Container Form Notes",
                Status = 0,
                Timestamp = DateTime.Now
            };

            if (!context.TForms.Any(x => x.FormName == orderCancellation.FormName)) { context.TForms.Add(orderCancellation); }
            if (!context.TForms.Any(x => x.FormName == expeditedOrder.FormName)) { context.TForms.Add(expeditedOrder); }
            if (!context.TForms.Any(x => x.FormName == priceChangeRequest.FormName)) { context.TForms.Add(priceChangeRequest); }
            if (!context.TForms.Any(x => x.FormName == hotRushForm.FormName)) { context.TForms.Add(hotRushForm); }
            if (!context.TForms.Any(x => x.FormName == lowInventory.FormName)) { context.TForms.Add(lowInventory); }
            if (!context.TForms.Any(x => x.FormName == sampleRequest.FormName)) { context.TForms.Add(sampleRequest); }
            if (!context.TForms.Any(x => x.FormName == directOrder.FormName)) { context.TForms.Add(directOrder); }
            if (!context.TForms.Any(x => x.FormName == requestForCheck.FormName)) { context.TForms.Add(requestForCheck); }
            if (!context.TForms.Any(x => x.FormName == mustIncludeForm.FormName)) { context.TForms.Add(mustIncludeForm); }
            if (!context.TForms.Any(x => x.FormName == cannotWaitForContainerForm.FormName)) { context.TForms.Add(cannotWaitForContainerForm); }
            
            //Seed ExpediteCodes
            ExpediteCode expCode1 = new ExpediteCode { Code = "EXP100", Description = "Mill Error", Status = 1, Timestamp = DateTime.Now };
            ExpediteCode expCode2 = new ExpediteCode { Code = "EXP200", Description = "Customer Error", Status = 1, Timestamp = DateTime.Now };
            ExpediteCode expCode3 = new ExpediteCode { Code = "EXP300", Description = "Tingle Error", Status = 1, Timestamp = DateTime.Now };
            ExpediteCode expCode4 = new ExpediteCode { Code = "EXP400", Description = "Install Date", Status = 1, Timestamp = DateTime.Now };
            ExpediteCode expCode5 = new ExpediteCode { Code = "EXP500", Description = "Can't wait on production date", Status = 1, Timestamp = DateTime.Now };
            ExpediteCode expCode6 = new ExpediteCode { Code = "EXP600", Description = "Search Other Distributors", Status = 1, Timestamp = DateTime.Now };
            ExpediteCode expCode7 = new ExpediteCode { Code = "EXP777", Description = "General", Status = 1, Timestamp = DateTime.Now };
            ExpediteCode expCode8 = new ExpediteCode { Code = "EXP800", Description = "Direct Order", Status = 1, Timestamp = DateTime.Now };
            ExpediteCode expCode9 = new ExpediteCode { Code = "EXP911", Description = "Immediate Attn", Status = 1, Timestamp = DateTime.Now };

            if (context.ExpediteCodes.Count() < 1)
            {
                if (!context.ExpediteCodes.Any(x => x.Code == expCode1.Code)) { context.ExpediteCodes.Add(expCode1); }
                if (!context.ExpediteCodes.Any(x => x.Code == expCode2.Code)) { context.ExpediteCodes.Add(expCode2); }
                if (!context.ExpediteCodes.Any(x => x.Code == expCode3.Code)) { context.ExpediteCodes.Add(expCode3); }
                if (!context.ExpediteCodes.Any(x => x.Code == expCode4.Code)) { context.ExpediteCodes.Add(expCode4); }
                if (!context.ExpediteCodes.Any(x => x.Code == expCode5.Code)) { context.ExpediteCodes.Add(expCode5); }
                if (!context.ExpediteCodes.Any(x => x.Code == expCode6.Code)) { context.ExpediteCodes.Add(expCode6); }
                if (!context.ExpediteCodes.Any(x => x.Code == expCode7.Code)) { context.ExpediteCodes.Add(expCode7); }
                if (!context.ExpediteCodes.Any(x => x.Code == expCode8.Code)) { context.ExpediteCodes.Add(expCode8); }
                if (!context.ExpediteCodes.Any(x => x.Code == expCode9.Code)) { context.ExpediteCodes.Add(expCode9); }
            }

            //Seed Statuses
            Status status1 = new Status { StatusText = "In Progress" };
            Status status2 = new Status { StatusText = "On Hold" };
            Status status3 = new Status { StatusText = "Completed" };
           
            if (!context.Statuses.Any())
            {
                if (!context.Statuses.Any(x => x.StatusText == status1.StatusText)) { context.Statuses.Add(status1); }
                if (!context.Statuses.Any(x => x.StatusText == status2.StatusText)) { context.Statuses.Add(status2); }
                if (!context.Statuses.Any(x => x.StatusText == status3.StatusText)) { context.Statuses.Add(status3); }
            }


            //Seed PO Statuses
            PurchaseOrderStatus poStatus1 = new PurchaseOrderStatus { Status = "Open", StatusCode = "O" };
            PurchaseOrderStatus poStatus2 = new PurchaseOrderStatus { Status = "Assigned", StatusCode = "A" };
            PurchaseOrderStatus poStatus3 = new PurchaseOrderStatus { Status = "Transit", StatusCode = "T" };
            PurchaseOrderStatus poStatus4 = new PurchaseOrderStatus { Status = "Future", StatusCode = "F" };
            PurchaseOrderStatus poStatus5 = new PurchaseOrderStatus { Status = "Rejected", StatusCode = "R" };
            PurchaseOrderStatus poStatus6 = new PurchaseOrderStatus { Status = "Confirmed", StatusCode = "K" };


            if (!context.POStatuses.Any())
            {
                context.POStatuses.Add(poStatus1);
                context.POStatuses.Add(poStatus2);
                context.POStatuses.Add(poStatus3);
                context.POStatuses.Add(poStatus4);
                context.POStatuses.Add(poStatus5);
                context.POStatuses.Add(poStatus6);
            }


            //Seed Prioties
            Priority pri1 = new Priority { PriorityText = "High" };
            Priority pri2 = new Priority { PriorityText = "Normal" };
            Priority pri3 = new Priority { PriorityText = "Low" };

            if (!context.Priorities.Any())
            {
                context.Priorities.Add(pri1);
                context.Priorities.Add(pri2);
                context.Priorities.Add(pri3);
            }


            //Seed Price Change Request Products
            PriceChangeRequestProducts prod1 = new PriceChangeRequestProducts { ProductText = "Armstrong" };
            PriceChangeRequestProducts prod2 = new PriceChangeRequestProducts { ProductText = "Other" };

            if (!context.PriceChangeRequestProducts.Any())
            {
                context.PriceChangeRequestProducts.Add(prod1);
                context.PriceChangeRequestProducts.Add(prod2);
            }


            //Seed Plants
            Plant plant1 = new Plant { PlantText = "Dickson" };
            Plant plant2 = new Plant { PlantText = "Kankakee" };
            Plant plant3 = new Plant { PlantText = "Stillwater" };
            Plant plant4 = new Plant { PlantText = "Lancaster" };
            Plant plant5 = new Plant { PlantText = "Southgate" };
            Plant plant6 = new Plant { PlantText = "Other" };
            Plant plant7 = new Plant { PlantText = "West Plains" };
            Plant plant8 = new Plant { PlantText = "JacksonTN" };
            Plant plant9 = new Plant { PlantText = "Jackson" };
            Plant plant10 = new Plant { PlantText = "Beverly" };
            Plant plant11 = new Plant { PlantText = "Givens" };

            if (!context.Plants.Any())
            {
                context.Plants.Add(plant1);
                context.Plants.Add(plant2);
                context.Plants.Add(plant3);
                context.Plants.Add(plant4);
                context.Plants.Add(plant5);
                context.Plants.Add(plant6);
                context.Plants.Add(plant7);
                context.Plants.Add(plant8);
                context.Plants.Add(plant9);
                context.Plants.Add(plant10);
                context.Plants.Add(plant11);
            }

            //Seed Warehouses
            Warehouse wh1 = new Warehouse { WarehouseText = "KAN" };
            Warehouse wh2 = new Warehouse { WarehouseText = "DEN" };
            Warehouse wh3 = new Warehouse { WarehouseText = "STL" };
            Warehouse wh4 = new Warehouse { WarehouseText = "HYD" };
            Warehouse wh5 = new Warehouse { WarehouseText = "SFG" };
            Warehouse wh6 = new Warehouse { WarehouseText = "SFH" };

            if (!context.Warehouses.Any())
            {
                context.Warehouses.Add(wh1);
                context.Warehouses.Add(wh2);
                context.Warehouses.Add(wh3);
                context.Warehouses.Add(wh4);
                context.Warehouses.Add(wh5);
                context.Warehouses.Add(wh6);
            }


            //Seed User Statuses
            UserStatus us1 = new UserStatus { StatusText = "Online" };
            UserStatus us2 = new UserStatus { StatusText = "Away" };
            UserStatus us3= new UserStatus { StatusText = "Busy" };

            if (!context.UserStatuses.Any())
            {
                context.UserStatuses.Add(us1);
                context.UserStatuses.Add(us2);
                context.UserStatuses.Add(us3);
            }

            //Seed Vendors
            Vendor v1 = new Vendor { Timestamp = DateTime.Now, VendorName = "Armstrong Laminate" };
            Vendor v2 = new Vendor { Timestamp = DateTime.Now, VendorName = "Armstrong Wood" };

            if (!context.Vendors.Any())
            {
                context.Vendors.Add(v1);
                context.Vendors.Add(v2);
            }

            //Seed Inventory Approval Statuses
            InventoryApprovalStatus s1 = new InventoryApprovalStatus { StatusDescription = "Pending Approval" };
            InventoryApprovalStatus s2 = new InventoryApprovalStatus { StatusDescription = "Approved" };
            InventoryApprovalStatus s3= new InventoryApprovalStatus { StatusDescription = "Ordered" };
            InventoryApprovalStatus s4= new InventoryApprovalStatus { StatusDescription = "Arrived" };
            InventoryApprovalStatus s5 = new InventoryApprovalStatus { StatusDescription = "Invoiced" };
            InventoryApprovalStatus s6 = new InventoryApprovalStatus { StatusDescription = "Cancelled" };

            if (!context.InventoryApprovalStatuses.Any())
            {
                context.InventoryApprovalStatuses.Add(s1);
                context.InventoryApprovalStatuses.Add(s2);
                context.InventoryApprovalStatuses.Add(s3);
                context.InventoryApprovalStatuses.Add(s4);
                context.InventoryApprovalStatuses.Add(s5);
                context.InventoryApprovalStatuses.Add(s6);
            }



            base.Seed(context);
        }
    }
}