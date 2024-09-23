window.setJwtCookie = (jwtToken, expiry) => {
    var expires = `expires = ${expiry}`;
    document.cookie = `JwtToken=${jwtToken}; ${expires}; path=/; Secure; SameSite=Strict`;
};