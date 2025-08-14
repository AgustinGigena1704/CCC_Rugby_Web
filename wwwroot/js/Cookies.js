function setCookie(name, value, expire) {
    var expires = "";
    if (expire) {
        expires = "; expires=" + expire;
    }
    document.cookie = name + "=" + (value || "") + expires + "; path=/";
}

function deleteCookie(name) {
    document.cookie = name + "=; expires = Thu, 01 Jan 1970 00:00:00 UTC; path =/;"; 
}

function getCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}
