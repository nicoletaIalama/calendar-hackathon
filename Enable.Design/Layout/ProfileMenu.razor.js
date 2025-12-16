export function initProfileMenu(element, dotnetRef, User, MyAccountAction, SignOutAction, Tenants) {
    element.userContext = User;
    element.myAccountAction = MyAccountAction;
    element.signOutAction = SignOutAction;

    if (!!element.userContext.tenantContext) {
        element.tenants = Tenants.filter(o => o?.id !== element.userContext.tenantContext.id);
    } else {
        element.tenants = Tenants;
    }

    const tenantClickedCallback = (event) => {
        if (!!event?.detail) {
            // Update user's tenant context
            element.userContext.tenantContext = event.detail;

            // Remove the selected tenant from tenants listing
            element.tenants = Tenants.filter(o => o !== event.detail && o?.id !== element.userContext.tenantContext.id);

            // Reset the search term in the tenants listing
            element.resetTenantSearch();

            dotnetRef.invokeMethodAsync('OnTenantClicked', event.detail);
        }
    };

    const searchTermChangedCallback = (event) => {
        const searchTerm = event.detail;

        if (!!searchTerm) {
            // Filter tenants by search term
            element.tenants =
                Tenants.filter(o => o.name.toLowerCase().includes(searchTerm.toLowerCase()) &&
                    o?.id !== element.userContext.tenantContext.id);
            return;
        }

        // If search term is empty, display all available tenants in the listing
        element.tenants = Tenants.filter(o => o.id !== element.userContext.tenantContext.id);
    };

    element.addEventListener('eds-profile-menu__selected-tenant', tenantClickedCallback);
    element.addEventListener('eds-profile-menu__search-term', searchTermChangedCallback);
}