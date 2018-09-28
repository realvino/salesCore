namespace tibs.stem.Authorization
{
    /// <summary>
    /// Defines string constants for application's permission names.
    /// <see cref="AppAuthorizationProvider"/> for permission definitions.
    /// </summary>
    public static class AppPermissions
    {
        //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

        public const string Pages = "Pages";

        public const string Pages_DemoUiComponents= "Pages.DemoUiComponents";
        public const string Pages_Administration = "Pages.Administration";

        public const string Pages_Administration_Roles = "Pages.Administration.Roles";
        public const string Pages_Administration_Roles_Create = "Pages.Administration.Roles.Create";
        public const string Pages_Administration_Roles_Edit = "Pages.Administration.Roles.Edit";
        public const string Pages_Administration_Roles_Delete = "Pages.Administration.Roles.Delete";

        public const string Pages_Administration_Users = "Pages.Administration.Users";
        public const string Pages_Administration_Users_Create = "Pages.Administration.Users.Create";
        public const string Pages_Administration_Users_Edit = "Pages.Administration.Users.Edit";
        public const string Pages_Administration_Users_Delete = "Pages.Administration.Users.Delete";
        public const string Pages_Administration_Users_ChangePermissions = "Pages.Administration.Users.ChangePermissions";
        public const string Pages_Administration_Users_Impersonation = "Pages.Administration.Users.Impersonation";

        public const string Pages_Administration_Languages = "Pages.Administration.Languages";
        public const string Pages_Administration_Languages_Create = "Pages.Administration.Languages.Create";
        public const string Pages_Administration_Languages_Edit = "Pages.Administration.Languages.Edit";
        public const string Pages_Administration_Languages_Delete = "Pages.Administration.Languages.Delete";
        public const string Pages_Administration_Languages_ChangeTexts = "Pages.Administration.Languages.ChangeTexts";

        public const string Pages_Administration_AuditLogs = "Pages.Administration.AuditLogs";

        public const string Pages_Administration_OrganizationUnits = "Pages.Administration.OrganizationUnits";
        public const string Pages_Administration_OrganizationUnits_ManageOrganizationTree = "Pages.Administration.OrganizationUnits.ManageOrganizationTree";
        public const string Pages_Administration_OrganizationUnits_ManageMembers = "Pages.Administration.OrganizationUnits.ManageMembers";

        public const string Pages_Administration_HangfireDashboard = "Pages.Administration.HangfireDashboard";

        public const string Pages_Tenant_Management = "Pages.Tenant.Managemant";
        public const string Pages_Tenant_Management_Leads = "Pages.Tenant.Managemant.Leads";


        //TENANT-SPECIFIC PERMISSIONS

        public const string Pages_Tenant_Dashboard = "Pages.Tenant.Dashboard";

        public const string Pages_Tenant_Dashboard_SalesDashboard = "Pages.Tenant.Dashboard.SalesDashboard";

        public const string Pages_Tenant_Dashboard_StandingPage = "Pages.Tenant.Dashboard.StandingPage";
        public const string Pages_Tenant_Dashboard_StandingPage_SalesDashboard = "Pages.Tenant.Dashboard.StandingPage.SalesDashboard";
        public const string Pages_Tenant_Dashboard_StandingPage_LeadKanban = "Pages.Tenant.Dashboard.StandingPage.LeadKanban";
        public const string Pages_Tenant_Dashboard_StandingPage_LeadGrid = "Pages.Tenant.Dashboard.StandingPage.LeadGrid";
        public const string Pages_Tenant_Dashboard_StandingPage_QuotationGrid = "Pages.Tenant.Dashboard.StandingPage.QuotationGrid";

        public const string Pages_Tenant_Dashboard_Dashboard = "Pages.Tenant.Dashboard.Dashboard";


        public const string Pages_Administration_Tenant_Settings = "Pages.Administration.Tenant.Settings";

        public const string Pages_Administration_Tenant_SubscriptionManagement = "Pages.Administration.Tenant.SubscriptionManagement";

        //HOST-SPECIFIC PERMISSIONS

        public const string Pages_Editions = "Pages.Editions";
        public const string Pages_Editions_Create = "Pages.Editions.Create";
        public const string Pages_Editions_Edit = "Pages.Editions.Edit";
        public const string Pages_Editions_Delete = "Pages.Editions.Delete";

        public const string Pages_Tenants = "Pages.Tenants";
        public const string Pages_Tenants_Create = "Pages.Tenants.Create";
        public const string Pages_Tenants_Edit = "Pages.Tenants.Edit";
        public const string Pages_Tenants_ChangeFeatures = "Pages.Tenants.ChangeFeatures";
        public const string Pages_Tenants_Delete = "Pages.Tenants.Delete";
        public const string Pages_Tenants_Impersonation = "Pages.Tenants.Impersonation";

        public const string Pages_Administration_Host_Maintenance = "Pages.Administration.Host.Maintenance";
        public const string Pages_Administration_Host_Settings = "Pages.Administration.Host.Settings";
        public const string Pages_Administration_Host_Dashboard = "Pages.Administration.Host.Dashboard";

        //Leads

        public const string Pages_Tenant_Leads = "Pages.Tenant.Leads";

        public const string Pages_Tenant_Leads_Leads = "Pages.Tenant.Leads.Leads";
        public const string Pages_Tenant_Leads_Leads_Create = "Pages.Tenant.Leads.Leads.Create";
        public const string Pages_Tenant_Leads_Leads_Edit = "Pages.Tenant.Leads.Leads.Edit";

        public const string Pages_Tenant_Leads_Leads_Gridview = "Pages.Tenant.Leads.Leads.Gridview";
        public const string Pages_Tenant_Leads_Leads_Gridview_Edit = "Pages.Tenant.Leads.Leads.Gridview.Edit";
        public const string Pages_Tenant_Leads_Leads_Gridview_Delete = "Pages.Tenant.Leads.Leads.Gridview.Delete";

        //Quotation

        public const string Pages_Tenant_Quotation = "Pages.Tenant.Quotation";

        public const string Pages_Tenant_Quotation_Quotations = "Pages.Tenant.Quotation.Quotations";
        public const string Pages_Tenant_Quotation_Quotations_Create = "Pages.Tenant.Quotation.Quotations.Create";
        public const string Pages_Tenant_Quotation_Quotations_Edit = "Pages.Tenant.Quotation.Quotations.Edit";
        public const string Pages_Tenant_Quotation_Quotations_Delete = "Pages.Tenant.Quotation.Quotations.Delete";

        public const string Pages_Tenant_Quotation_Quotations_Edit_AddQuotationProduct = "Pages.Tenant.Quotation.Quotations.Edit.QuotationProductAdd";
        public const string Pages_Tenant_Quotation_Quotations_Edit_EditQuotationProduct = "Pages.Tenant.Quotation.Quotations.Edit.QuotationProductEdit";
        public const string Pages_Tenant_Quotation_Quotations_Edit_DeleteQuotationProduct = "Pages.Tenant.Quotation.Quotations.Edit.QuotationProductDelete";
        public const string Pages_Tenant_Quotation_Quotations_Edit_AddQuotationService = "Pages.Tenant.Quotation.Quotations.Edit.QuotationServiceAdd";
        public const string Pages_Tenant_Quotation_Quotations_Edit_EditQuotationService = "Pages.Tenant.Quotation.Quotations.Edit.QuotationServiceEdit";
        public const string Pages_Tenant_Quotation_Quotations_Edit_DeleteQuotationService = "Pages.Tenant.Quotation.Quotations.Edit.QuotationServiceDelete";

        public const string Pages_Tenant_Quotation_QuotationStatus = "Pages.Tenant.Quotation.QuotationStatus";
        public const string Pages_Tenant_Quotation_QuotationStatus_Create = "Pages.Tenant.Quotation.QuotationStatus.Create";
        public const string Pages_Tenant_Quotation_QuotationStatus_Edit = "Pages.Tenant.Quotation.QuotationStatus.Edit";
        public const string Pages_Tenant_Quotation_QuotationStatus_Delete = "Pages.Tenant.Quotation.QuotationStatus.Delete";

        public const string Pages_Tenant_Quotation_QuotationMaster = "Pages.Tenant.Quotation.QuotationMaster";

        public const string Pages_Tenant_Quotation_QuotationMaster_Delivery = "Pages.Tenant.Quotation.QuotationMaster.Delivery";
        public const string Pages_Tenant_Quotation_QuotationMaster_Delivery_Create = "Pages.Tenant.Quotation.QuotationMaster.Delivery.Create";
        public const string Pages_Tenant_Quotation_QuotationMaster_Delivery_Edit = "Pages.Tenant.Quotation.QuotationMaster.Delivery.Edit";
        public const string Pages_Tenant_Quotation_QuotationMaster_Delivery_Delete = "Pages.Tenant.Quotation.QuotationMaster.Delivery.Delete";

        public const string Pages_Tenant_Quotation_QuotationMaster_Reason = "Pages.Tenant.Quotation.QuotationMaster.Reason";
        public const string Pages_Tenant_Quotation_QuotationMaster_Reason_Create = "Pages.Tenant.Quotation.QuotationMaster.Reason.Create";
        public const string Pages_Tenant_Quotation_QuotationMaster_Reason_Edit = "Pages.Tenant.Quotation.QuotationMaster.Reason.Edit";
        public const string Pages_Tenant_Quotation_QuotationMaster_Reason_Delete = "Pages.Tenant.Quotation.QuotationMaster.Reason.Delete";

        public const string Pages_Tenant_Quotation_QuotationMaster_Freight = "Pages.Tenant.Quotation.QuotationMaster.Freight";
        public const string Pages_Tenant_Quotation_QuotationMaster_Freight_Create = "Pages.Tenant.Quotation.QuotationMaster.Freight.Create";
        public const string Pages_Tenant_Quotation_QuotationMaster_Freight_Edit = "Pages.Tenant.Quotation.QuotationMaster.Freight.Edit";
        public const string Pages_Tenant_Quotation_QuotationMaster_Freight_Delete = "Pages.Tenant.Quotation.QuotationMaster.Freight.Delete";

        public const string Pages_Tenant_Quotation_QuotationMaster_Packing = "Pages.Tenant.Quotation.QuotationMaster.Packing";
        public const string Pages_Tenant_Quotation_QuotationMaster_Packing_Create = "Pages.Tenant.Quotation.QuotationMaster.Packing.Create";
        public const string Pages_Tenant_Quotation_QuotationMaster_Packing_Edit = "Pages.Tenant.Quotation.QuotationMaster.Packing.Edit";
        public const string Pages_Tenant_Quotation_QuotationMaster_Packing_Delete = "Pages.Tenant.Quotation.QuotationMaster.Packing.Delete";

        public const string Pages_Tenant_Quotation_QuotationMaster_QPayment = "Pages.Tenant.Quotation.QuotationMaster.QPayment";
        public const string Pages_Tenant_Quotation_QuotationMaster_QPayment_Create = "Pages.Tenant.Quotation.QuotationMaster.QPayment.Create";
        public const string Pages_Tenant_Quotation_QuotationMaster_QPayment_Edit = "Pages.Tenant.Quotation.QuotationMaster.QPayment.Edit";
        public const string Pages_Tenant_Quotation_QuotationMaster_QPayment_Delete = "Pages.Tenant.Quotation.QuotationMaster.QPayment.Delete";

        public const string Pages_Tenant_Quotation_QuotationMaster_TitleOfQuotation = "Pages.Tenant.Quotation.QuotationMaster.TitleOfQuotation";
        public const string Pages_Tenant_Quotation_QuotationMaster_TitleOfQuotation_Create = "Pages.Tenant.Quotation.QuotationMaster.TitleOfQuotation.Create";
        public const string Pages_Tenant_Quotation_QuotationMaster_TitleOfQuotation_Edit = "Pages.Tenant.Quotation.QuotationMaster.TitleOfQuotation.Edit";
        public const string Pages_Tenant_Quotation_QuotationMaster_TitleOfQuotation_Delete = "Pages.Tenant.Quotation.QuotationMaster.TitleOfQuotation.Delete";

        public const string Pages_Tenant_Quotation_QuotationMaster_Validity = "Pages.Tenant.Quotation.QuotationMaster.Validity";
        public const string Pages_Tenant_Quotation_QuotationMaster_Validity_Create = "Pages.Tenant.Quotation.QuotationMaster.Validity.Create";
        public const string Pages_Tenant_Quotation_QuotationMaster_Validity_Edit = "Pages.Tenant.Quotation.QuotationMaster.Validity.Edit";
        public const string Pages_Tenant_Quotation_QuotationMaster_Validity_Delete = "Pages.Tenant.Quotation.QuotationMaster.Validity.Delete";

        public const string Pages_Tenant_Quotation_QuotationMaster_Warranty = "Pages.Tenant.Quotation.QuotationMaster.Warranty";
        public const string Pages_Tenant_Quotation_QuotationMaster_Warranty_Create = "Pages.Tenant.Quotation.QuotationMaster.Warranty.Create";
        public const string Pages_Tenant_Quotation_QuotationMaster_Warranty_Edit = "Pages.Tenant.Quotation.QuotationMaster.Warranty.Edit";
        public const string Pages_Tenant_Quotation_QuotationMaster_Warranty_Delete = "Pages.Tenant.Quotation.QuotationMaster.Warranty.Delete";

        public const string Pages_Tenant_Quotation_SalesOrder = "Pages.Tenant.Quotation.SalesOrder";
        public const string Pages_Tenant_Quotation_SalesOrder_Edit = "Pages.Tenant.Quotation.SalesOrder.Edit";

        //ProductFamily
        public const string Pages_Tenant_ProductFamily = "Pages.Tenant.ProductFamily";

        public const string Pages_Tenant_ProductFamily_ProductGroup = "Pages.Tenant.ProductFamily.ProductGroup";
        public const string Pages_Tenant_ProductFamily_ProductGroup_Create = "Pages.Tenant.ProductFamily.ProductGroup.Create";
        public const string Pages_Tenant_ProductFamily_ProductGroup_Edit = "Pages.Tenant.ProductFamily.ProductGroup.Edit";
        public const string Pages_Tenant_ProductFamily_ProductGroup_Delete = "Pages.Tenant.ProductFamily.ProductGroup.Delete";

        public const string Pages_Tenant_ProductFamily_ProductSubgroup = "Pages.Tenant.ProductFamily.ProductSubgroup";
        public const string Pages_Tenant_ProductFamily_ProductSubgroup_Create = "Pages.Tenant.ProductFamily.ProductSubgroup.Create";
        public const string Pages_Tenant_ProductFamily_ProductSubgroup_Edit = "Pages.Tenant.ProductFamily.ProductSubgroup.Edit";
        public const string Pages_Tenant_ProductFamily_ProductSubgroup_Delete = "Pages.Tenant.ProductFamily.ProductSubgroup.Delete";

        public const string Pages_Tenant_ProductFamily_Products = "Pages.Tenant.ProductFamily.Products";
        public const string Pages_Tenant_ProductFamily_Products_Create = "Pages.Tenant.ProductFamily.Products.Create";
        public const string Pages_Tenant_ProductFamily_Products_Edit = "Pages.Tenant.ProductFamily.Products.Edit";
        public const string Pages_Tenant_ProductFamily_Products_Delete = "Pages.Tenant.ProductFamily.Products.Delete";

        public const string Pages_Tenant_ProductFamily_Services = "Pages.Tenant.ProductFamily.Services";
        public const string Pages_Tenant_ProductFamily_Services_Create = "Pages.Tenant.ProductFamily.Services.Create";
        public const string Pages_Tenant_ProductFamily_Services_Edit = "Pages.Tenant.ProductFamily.Services.Edit";
        public const string Pages_Tenant_ProductFamily_Services_Delete = "Pages.Tenant.ProductFamily.Services.Delete";

        public const string Pages_Tenant_ProductFamily_PriceLevel = "Pages.Tenant.ProductFamily.PriceLevel";
        public const string Pages_Tenant_ProductFamily_PriceLevel_Create = "Pages.Tenant.ProductFamily.PriceLevel.Create";
        public const string Pages_Tenant_ProductFamily_PriceLevel_Edit = "Pages.Tenant.ProductFamily.PriceLevel.Edit";
        public const string Pages_Tenant_ProductFamily_PriceLevel_Delete = "Pages.Tenant.ProductFamily.PriceLevel.Delete";

        //AddressBook
        public const string Pages_Tenant_AddressBook = "Pages.Tenant.AddressBook";

        public const string Pages_Tenant_AddressBook_Company = "Pages.Tenant.AddressBook.Company";
        public const string Pages_Tenant_AddressBook_Company_Create = "Pages.Tenant.AddressBook.Company.Create";
        public const string Pages_Tenant_AddressBook_Company_Edit = "Pages.Tenant.AddressBook.Company.Edit";
        public const string Pages_Tenant_AddressBook_Company_Delete = "Pages.Tenant.AddressBook.Company.Delete";

        public const string Pages_Tenant_AddressBook_Contact = "Pages.Tenant.AddressBook.Contact";
        public const string Pages_Tenant_AddressBook_Contact_Create = "Pages.Tenant.AddressBook.Contact.Create";
        public const string Pages_Tenant_AddressBook_Contact_Edit = "Pages.Tenant.AddressBook.Contact.Edit";
        public const string Pages_Tenant_AddressBook_Contact_Delete = "Pages.Tenant.AddressBook.Contact.Delete";

        //Master
        public const string Pages_Tenant_Master = "Pages.Tenant.Master";

        public const string Pages_Tenant_Master_ActivityType = "Pages.Tenant.Master.ActivityType";
        public const string Pages_Tenant_Master_ActivityType_Create = "Pages.Tenant.Master.ActivityType.Create";
        public const string Pages_Tenant_Master_ActivityType_Edit = "Pages.Tenant.Master.ActivityType.Edit";
        public const string Pages_Tenant_Master_ActivityType_Delete = "Pages.Tenant.Master.ActivityType.Delete";

        public const string Pages_Tenant_Master_MileStone = "Pages.Tenant.Master.MileStone";
        public const string Pages_Tenant_Master_MileStone_Create = "Pages.Tenant.Master.MileStone.Create";
        public const string Pages_Tenant_Master_MileStone_Edit = "Pages.Tenant.Master.MileStone.Edit";
        public const string Pages_Tenant_Master_MileStone_Delete = "Pages.Tenant.Master.MileStone.Delete";

        public const string Pages_Tenant_Master_MileStoneStatus = "Pages.Tenant.Master.MileStoneStatus";
        public const string Pages_Tenant_Master_MileStoneStatus_Create = "Pages.Tenant.Master.MileStoneStatus.Create";
        public const string Pages_Tenant_Master_MileStoneStatus_Edit = "Pages.Tenant.Master.MileStoneStatus.Edit";
        public const string Pages_Tenant_Master_MileStoneStatus_Delete = "Pages.Tenant.Master.MileStoneStatus.Delete";

        public const string Pages_Tenant_Master_Currency = "Pages.Tenant.Master.Currency";
        public const string Pages_Tenant_Master_Currency_Create = "Pages.Tenant.Master.Currency.Create";
        public const string Pages_Tenant_Master_Currency_Edit = "Pages.Tenant.Master.Currency.Edit";
        public const string Pages_Tenant_Master_Currency_Delete = "Pages.Tenant.Master.Currency.Delete";

        public const string Pages_Tenant_Master_CustomerType = "Pages.Tenant.Master.CustomerType";
        public const string Pages_Tenant_Master_CustomerType_Create = "Pages.Tenant.Master.CustomerType.Create";
        public const string Pages_Tenant_Master_CustomerType_Edit = "Pages.Tenant.Master.CustomerType.Edit";
        public const string Pages_Tenant_Master_CustomerType_Delete = "Pages.Tenant.Master.CustomerType.Delete";

        public const string Pages_Tenant_Master_InfoType = "Pages.Tenant.Master.InfoType";
        public const string Pages_Tenant_Master_InfoType_Create = "Pages.Tenant.Master.InfoType.Create";
        public const string Pages_Tenant_Master_InfoType_Edit = "Pages.Tenant.Master.InfoType.Edit";
        public const string Pages_Tenant_Master_InfoType_Delete = "Pages.Tenant.Master.InfoType.Delete";

    }
}