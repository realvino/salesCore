using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.MultiTenancy;

namespace tibs.stem.Authorization
{
    /// <summary>
    /// Application's authorization provider.
    /// Defines permissions for the application.
    /// See <see cref="AppPermissions"/> for all permission names.
    /// </summary>
    public class AppAuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

        public AppAuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public AppAuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

            var pages = context.GetPermissionOrNull(AppPermissions.Pages) ?? context.CreatePermission(AppPermissions.Pages, L("Pages"));
            //pages.CreateChildPermission(AppPermissions.Pages_DemoUiComponents, L("DemoUiComponents"));

            var management = pages.CreateChildPermission(AppPermissions.Pages_Tenant_Management, L("Management"), multiTenancySides: MultiTenancySides.Tenant);
            management.CreateChildPermission(AppPermissions.Pages_Tenant_Management_Leads, L("Can Manage Leads"), multiTenancySides: MultiTenancySides.Tenant);

              var dashboard = pages.CreateChildPermission(AppPermissions.Pages_Tenant_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Tenant);
            dashboard.CreateChildPermission(AppPermissions.Pages_Tenant_Dashboard_Dashboard, L("Dashboard"));
            dashboard.CreateChildPermission(AppPermissions.Pages_Tenant_Dashboard_SalesDashboard, L("SalesDashboard"));
            //Leads
            var leads = pages.CreateChildPermission(AppPermissions.Pages_Tenant_Leads, L("Leads"));

            var enquiry = leads.CreateChildPermission(AppPermissions.Pages_Tenant_Leads_Leads, L("LeadKanban"));
            enquiry.CreateChildPermission(AppPermissions.Pages_Tenant_Leads_Leads_Create, L("CreateLead"));
            enquiry.CreateChildPermission(AppPermissions.Pages_Tenant_Leads_Leads_Edit, L("EditLeadKanban"));

            var gridView = enquiry.CreateChildPermission(AppPermissions.Pages_Tenant_Leads_Leads_Gridview, L("LeadGrid"));
            gridView.CreateChildPermission(AppPermissions.Pages_Tenant_Leads_Leads_Gridview_Edit, L("EditLeadGrid"));
            gridView.CreateChildPermission(AppPermissions.Pages_Tenant_Leads_Leads_Gridview_Delete, L("DeleteLeadGrid"));

            //Quotation
            var quotation = pages.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation, L("Quotation"));

            var quotations = quotation.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_Quotations, L("Quotation"));
            quotations.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_Quotations_Create, L("CreateQuotation"));
            quotations.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_Quotations_Delete, L("DeleteQuotation"));

            var editQuotation = quotations.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_Quotations_Edit, L("EditQuotation"));
            editQuotation.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_Quotations_Edit_AddQuotationProduct, L("AddQuotationProduct"));
            editQuotation.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_Quotations_Edit_EditQuotationProduct, L("EditQuotationProduct"));
            editQuotation.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_Quotations_Edit_DeleteQuotationProduct, L("DeleteQuotationProduct"));
            editQuotation.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_Quotations_Edit_AddQuotationService, L("AddQuotationService"));
            editQuotation.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_Quotations_Edit_EditQuotationService, L("EditQuotationService"));
            editQuotation.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_Quotations_Edit_DeleteQuotationService, L("DeleteQuotationService"));

            var status = quotation.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_QuotationStatus, L("QuotationStatus"));
            status.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_QuotationStatus_Create, L("CreateQuotationStatus"));
            status.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_QuotationStatus_Edit, L("EditQuotationStatus"));
            status.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_QuotationStatus_Delete, L("DeleteQuotationStatus"));

            var quotationMaster = quotation.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_QuotationMaster, L("QuotationMaster"));

            var delivery = quotationMaster.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Delivery, L("Delivery"));
            delivery.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Delivery_Create, L("CreateDelivery"));
            delivery.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Delivery_Edit, L("EditDelivery"));
            delivery.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Delivery_Delete, L("DeleteDelivery"));

            var reason = quotationMaster.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Reason, L("Reason"));
            reason.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Reason_Create, L("CreateReason"));
            reason.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Reason_Edit, L("EditReason"));
            reason.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Reason_Delete, L("DeleteReason"));

            var freight = quotationMaster.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Freight, L("Freight"));
            freight.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Freight_Create, L("CreateFreight"));
            freight.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Freight_Edit, L("EditFreight"));
            freight.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Freight_Delete, L("DeleteFreight"));

            var packing = quotationMaster.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Packing, L("Packing"));
            packing.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Packing_Create, L("CreatePacking"));
            packing.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Packing_Edit, L("EditPacking"));
            packing.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Packing_Delete, L("DeletePacking"));

            var qPayment = quotationMaster.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_QPayment, L("QPayment"));
            qPayment.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_QPayment_Create, L("CreateQPayment"));
            qPayment.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_QPayment_Edit, L("EditQPayment"));
            qPayment.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_QPayment_Delete, L("DeleteQPayment"));

            var titleOfQuotation = quotationMaster.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_TitleOfQuotation, L("TitleOfQuotation"));
            titleOfQuotation.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_TitleOfQuotation_Create, L("CreateTitleOfQuotation"));
            titleOfQuotation.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_TitleOfQuotation_Edit, L("EditTitleOfQuotation"));
            titleOfQuotation.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_TitleOfQuotation_Delete, L("DeleteTitleOfQuotation"));

            var validity = quotationMaster.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Validity, L("Validity"));
            validity.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Validity_Create, L("CreateValidity"));
            validity.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Validity_Edit, L("EditValidity"));
            validity.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Validity_Delete, L("DeleteValidity"));

            var warranty = quotationMaster.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Warranty, L("Warranty"));
            warranty.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Warranty_Create, L("CreateWarranty"));
            warranty.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Warranty_Edit, L("EditWarranty"));
            warranty.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Warranty_Delete, L("DeleteWarranty"));

            var salesorder = quotation.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_SalesOrder, L("SalesOrder"));
            salesorder.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_SalesOrder_Edit, L("EditSalesOrder"));

            //Product Family
            var productfamily = pages.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily, L("ProductFamily"));

            var productgroup = productfamily.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_ProductGroup, L("ProductGroup"));
            productgroup.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_ProductGroup_Create, L("CreateProductGroup"));
            productgroup.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_ProductGroup_Edit, L("EditProductGroup"));
            productgroup.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_ProductGroup_Delete, L("DeleteProductGroup"));

            var productsubgroup = productfamily.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_ProductSubgroup, L("ProductSubgroup"));
            productsubgroup.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_ProductSubgroup_Create, L("CreateProductSubgroup"));
            productsubgroup.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_ProductSubgroup_Edit, L("EditProductSubgroup"));
            productsubgroup.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_ProductSubgroup_Delete, L("DeleteProductSubgroup"));

            var products = productfamily.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_Products, L("Products"));
            products.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_Products_Create, L("CreateProducts"));
            products.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_Products_Edit, L("EditProducts"));
            products.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_Products_Delete, L("DeleteProducts"));

            var services = productfamily.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_Services, L("Services"));
            services.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_Services_Create, L("CreateServices"));
            services.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_Services_Edit, L("EditServices"));
            services.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_Services_Delete, L("DeleteServices"));

            var priceLevel = productfamily.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_PriceLevel, L("PriceLevel"));
            priceLevel.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_PriceLevel_Create, L("CreatePriceLevel"));
            priceLevel.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_PriceLevel_Edit, L("EditPriceLevel"));
            priceLevel.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_PriceLevel_Delete, L("DeletePriceLevel"));

            //Addressbook
            var addressbook = pages.CreateChildPermission(AppPermissions.Pages_Tenant_AddressBook, L("AddressBook"));

            var company = addressbook.CreateChildPermission(AppPermissions.Pages_Tenant_AddressBook_Company, L("Company"));
            company.CreateChildPermission(AppPermissions.Pages_Tenant_AddressBook_Company_Create, L("CreateCompany"));
            company.CreateChildPermission(AppPermissions.Pages_Tenant_AddressBook_Company_Edit, L("EditCompany"));
            company.CreateChildPermission(AppPermissions.Pages_Tenant_AddressBook_Company_Delete, L("DeleteCompany"));

            var contact = addressbook.CreateChildPermission(AppPermissions.Pages_Tenant_AddressBook_Contact, L("Contact"));
            contact.CreateChildPermission(AppPermissions.Pages_Tenant_AddressBook_Contact_Create, L("CreateContact"));
            contact.CreateChildPermission(AppPermissions.Pages_Tenant_AddressBook_Contact_Edit, L("EditContact"));
            contact.CreateChildPermission(AppPermissions.Pages_Tenant_AddressBook_Contact_Delete, L("DeleteContact"));

            //Master
            var master = pages.CreateChildPermission(AppPermissions.Pages_Tenant_Master, L("Master"));

            var activityType = master.CreateChildPermission(AppPermissions.Pages_Tenant_Master_ActivityType, L("ActivityType"));
            activityType.CreateChildPermission(AppPermissions.Pages_Tenant_Master_ActivityType_Create, L("CreateActivityType"));
            activityType.CreateChildPermission(AppPermissions.Pages_Tenant_Master_ActivityType_Edit, L("EditActivityType"));
            activityType.CreateChildPermission(AppPermissions.Pages_Tenant_Master_ActivityType_Delete, L("DeleteActivityType"));

            var mileStone = master.CreateChildPermission(AppPermissions.Pages_Tenant_Master_MileStone, L("MileStone"));
            mileStone.CreateChildPermission(AppPermissions.Pages_Tenant_Master_MileStone_Create, L("CreateMileStone"));
            mileStone.CreateChildPermission(AppPermissions.Pages_Tenant_Master_MileStone_Edit, L("EditMileStone"));
            mileStone.CreateChildPermission(AppPermissions.Pages_Tenant_Master_MileStone_Delete, L("DeleteMileStone"));

            var mileStoneStatus = master.CreateChildPermission(AppPermissions.Pages_Tenant_Master_MileStoneStatus, L("MileStoneStatus"));
            mileStoneStatus.CreateChildPermission(AppPermissions.Pages_Tenant_Master_MileStoneStatus_Create, L("CreateMileStoneStatus"));
            mileStoneStatus.CreateChildPermission(AppPermissions.Pages_Tenant_Master_MileStoneStatus_Edit, L("EditMileStoneStatus"));
            mileStoneStatus.CreateChildPermission(AppPermissions.Pages_Tenant_Master_MileStoneStatus_Delete, L("DeleteMileStoneStatus"));

            var customertype = addressbook.CreateChildPermission(AppPermissions.Pages_Tenant_Master_CustomerType, L("CustomerType"));
            customertype.CreateChildPermission(AppPermissions.Pages_Tenant_Master_CustomerType_Create, L("CreateCustomerType"));
            customertype.CreateChildPermission(AppPermissions.Pages_Tenant_Master_CustomerType_Edit, L("EditCustomerType"));
            customertype.CreateChildPermission(AppPermissions.Pages_Tenant_Master_CustomerType_Delete, L("DeleteCustomerType"));

            var infotype = addressbook.CreateChildPermission(AppPermissions.Pages_Tenant_Master_InfoType, L("InfoType"));
            infotype.CreateChildPermission(AppPermissions.Pages_Tenant_Master_InfoType_Create, L("CreateInfoType"));
            infotype.CreateChildPermission(AppPermissions.Pages_Tenant_Master_InfoType_Edit, L("EditInfoType"));
            infotype.CreateChildPermission(AppPermissions.Pages_Tenant_Master_InfoType_Delete, L("DeleteInfoType"));

            var administration = pages.CreateChildPermission(AppPermissions.Pages_Administration, L("Administration"));

            var roles = administration.CreateChildPermission(AppPermissions.Pages_Administration_Roles, L("Roles"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Create, L("CreatingNewRole"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Edit, L("EditingRole"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Delete, L("DeletingRole"));

            var users = administration.CreateChildPermission(AppPermissions.Pages_Administration_Users, L("Users"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Create, L("CreatingNewUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Edit, L("EditingUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Delete, L("DeletingUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_ChangePermissions, L("ChangingPermissions"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Impersonation, L("LoginForUsers"));

            var languages = administration.CreateChildPermission(AppPermissions.Pages_Administration_Languages, L("Languages"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Create, L("CreatingNewLanguage"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Edit, L("EditingLanguage"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Delete, L("DeletingLanguages"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_ChangeTexts, L("ChangingTexts"));

            //administration.CreateChildPermission(AppPermissions.Pages_Administration_AuditLogs, L("AuditLogs"));

            //var organizationUnits = administration.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits, L("OrganizationUnits"));
            //organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree, L("ManagingOrganizationTree"));
            //organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageMembers, L("ManagingMembers"));

          
            //Standing Pages

            //var staticPages = pages.CreateChildPermission(AppPermissions.Pages_Tenant_Dashboard_StandingPage, L("StandingPage"));
            //staticPages.CreateChildPermission(AppPermissions.Pages_Tenant_Dashboard_StandingPage_SalesDashboard, L("SalesDashboard"));
            //staticPages.CreateChildPermission(AppPermissions.Pages_Tenant_Dashboard_StandingPage_LeadKanban, L("LeadKanban"));
            //staticPages.CreateChildPermission(AppPermissions.Pages_Tenant_Dashboard_StandingPage_LeadGrid, L("LeadGrid"));
            //staticPages.CreateChildPermission(AppPermissions.Pages_Tenant_Dashboard_StandingPage_QuotationGrid, L("QuotationGrid"));

            //TENANT-SPECIFIC PERMISSIONS


            administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_SubscriptionManagement, L("Subscription"), multiTenancySides: MultiTenancySides.Tenant);

            //HOST-SPECIFIC PERMISSIONS

            var editions = pages.CreateChildPermission(AppPermissions.Pages_Editions, L("Editions"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Create, L("CreatingNewEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Edit, L("EditingEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Delete, L("DeletingEdition"), multiTenancySides: MultiTenancySides.Host);

            var tenants = pages.CreateChildPermission(AppPermissions.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Create, L("CreatingNewTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Edit, L("EditingTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_ChangeFeatures, L("ChangingFeatures"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Delete, L("DeletingTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Impersonation, L("LoginForTenants"), multiTenancySides: MultiTenancySides.Host);

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Host);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Maintenance, L("Maintenance"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_HangfireDashboard, L("HangfireDashboard"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Host);

           

        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, stemConsts.LocalizationSourceName);
        }
    }
}
