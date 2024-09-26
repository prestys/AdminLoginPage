window.getJwtCookie = () => {
    let cookieArr = document.cookie.split(';');

    for (let i = 0; i < cookieArr.length; i++) {
        let cookie = cookieArr[i].trim();

        if (cookie.startsWith('JwtToken=')) {
            return cookie.substring('JwtToken='.length);
        }
    }
    return null;
};
