$(document).ready(function () {
    $(".add-to-cart").off('click').on('click', function (e) {
        e.preventDefault();

        var productId = $(this).data('product-id').toString();
        var productName = $(this).data('product-name');
        var productPrice = parseFloat($(this).data('product-price'));
        var productImage = $(this).data('product_image');

        addToLocalStorage(productId, productName, productPrice, productImage,1); // You can adjust the quantity here

        alert("Sản phẩm đã được thêm vào giỏ hàng!");
    });
});


function addToLocalStorage(productId, productName, productPrice, productImage, quantity) {
    if (typeof (Storage) !== "undefined") {
        var cartItems = JSON.parse(localStorage.getItem("cartItems")) || [];
        var existingItem = cartItems.find(item => item.productId === productId);

        if (existingItem) {
            existingItem.quantity += quantity;
        } else {
            cartItems.push({ productId, productName, productPrice, productImage, quantity });
        }

        localStorage.setItem("cartItems", JSON.stringify(cartItems));
        updateCartItemCoun2();
    } else {
        alert("Local storage is not supported in this browser.");
    }
}

function updateCartItemCoun2() {
    function getCartItemsFromLocalStorage() {
        if (typeof (Storage) !== "undefined") {
            var cartItems = JSON.parse(localStorage.getItem("cartItems")) || [];
            return cartItems;
        } else {
            alert("Local storage is not supported in this browser.");
            return [];
        }
    }
    var cartItems = getCartItemsFromLocalStorage();
    var cartItemCount = document.getElementById("cartItemCount");
    if (cartItemCount) {
        cartItemCount.textContent = cartItems.length;
    }
}
