export function initHubSwitcher(enableAppsUrl) {
    var hubSwitcherConfig = {
        elementName: '#hub-switcher',
        hubRootUrl: enableAppsUrl,
        scriptUrl: enableAppsUrl + '/components/hub-switcher/hub-switcher.min.js',
        loadNotifications: true,
        hideProfileMenu: true
    };

    var hubSwitcherElement = document.querySelector(hubSwitcherConfig.elementName);

    if (hubSwitcherElement) {
        var hubSwitcherScript = document.createElement('script');
        hubSwitcherScript.src = hubSwitcherConfig.scriptUrl;

        hubSwitcherScript.addEventListener('load', function () {
            embeddedNotifications.EmbeddedNotificationsHelper.insertEmbeddedNotifications(
                new notificationsService(),
                enableAppsUrl);

            var hubSwitcherComponent = new HubSwitcher();
            hubSwitcherComponent.container = hubSwitcherElement;
            hubSwitcherComponent.init(hubSwitcherConfig);
        });

        hubSwitcherScript.addEventListener('error', function () {
            hubSwitcherElement.parentElement.removeChild(hubSwitcherElement);
            document.body.removeChild(hubSwitcherScript);
        });

        document.body.appendChild(hubSwitcherScript);
    }
}

export class notificationsService extends embeddedNotifications.NotificationsService {
    constructor() {
        super();
    }

    // Triggered by embedded notifications on first load
    loadNotifications(_) {
        // Listen to notifications being loaded into Claims
        document.addEventListener('add-notification', (notification) => {
            this.addNotificationToMenu(notification.detail); 
        }, false);
        document.addEventListener('add-notifications', (notifications) => {
            notifications.detail.key.forEach(o => {
                o.initialLoad = true;
                if(o.type === 5 ){
                    o.type = 'Actionable';
                }
                super.addNotificationToMenu(o);
            }, false);
        })

        // Raise event to signal that embedded notifications has initialised and should load initial notifications
        document.dispatchEvent(
            new CustomEvent(
                'load-notifications',
                {
                    bubbles: true,
                }
            )
        )
        return Promise.resolve([]);
    }

    // Clear methods are triggered by embedded notifications when notifications are manually dismissed
    clearNotifications() {
        return this.clearAllNotifications();
    }

    
    clearAllNotifications() {
        // Raise clear event to be be passed on to Notifications api
        document.dispatchEvent(
            new CustomEvent(
                'clear-notification',
                {
                    bubbles: true,
                }
            )
        )
    }
}