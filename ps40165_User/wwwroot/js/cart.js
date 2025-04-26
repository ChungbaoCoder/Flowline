window.cartStorage = {
    setCart: function (cartJson) {
        localStorage.setItem("cart", cartJson);
    },
    getCart: function () {
        return localStorage.getItem("cart");
    },
    clearCart: function () {
        localStorage.removeItem("cart");
    }
};