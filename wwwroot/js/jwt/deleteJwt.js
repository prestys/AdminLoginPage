window.deleteJwtCookie = () => {
    document.cookie = 'JwtToken=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/; Secure; SameSite=Strict';
};
