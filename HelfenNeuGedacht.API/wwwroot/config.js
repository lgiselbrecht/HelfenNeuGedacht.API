// Zentrale API-Konfiguration für das HelfenNeuGedacht Frontend
const API_CONFIG = {
    BASE_URL: 'http://localhost:5062/api',
    ENDPOINTS: {
        EVENTS: '/events',
        SHIFTS: '/shifts',
        ORGANIZATION: '/v1/organization',
        AUTH: '/authenticate'
    },
    
    // Helper-Funktion um vollständige URLs zu erstellen
    getUrl(endpoint) {
        return this.BASE_URL + this.ENDPOINTS[endpoint];
    }
};

// Für ältere Browser-Kompatibilität als window-Objekt verfügbar machen
if (typeof window !== 'undefined') {
    window.API_CONFIG = API_CONFIG;
}
