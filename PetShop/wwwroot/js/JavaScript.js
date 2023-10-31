$(document).ready(function () {
    $(".add-to-cart").click(function (e) {
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
    } else {
        alert("Local storage is not supported in this browser.");
    }
}
// Function to add an item to local storage


// Function to retrieve cart items from local storage
