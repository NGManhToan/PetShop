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

async function checkUserAccount() {
    // Gọi API để lấy userId
    const response = await fetch('/api/Cart/idUser', {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
        },
    });
    if (!response.ok) {
        return null;
    }

    // Trả về userId
    return await response.json();
}

async function updateCartItemCoun2() {
    var userId = await checkUserAccount();
    var cartItemCount;

    if (userId) {
        // Nếu có userId, gọi API để lấy số lượng từ data
        const response = await fetch('/api/Cart/CountItem', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
            },
        });
        if (response.ok) {
            const data = await response.json();
            console.log(data);  // In ra giá trị trả về
            cartItemCount = data.slsp; 
        } else {
            alert("Failed to get cart items from server.");
            return;
        }
    } else {
        // Nếu không có userId, lấy số lượng từ local storage
        if (typeof (Storage) !== "undefined") {
            var cartItems = JSON.parse(localStorage.getItem("cartItems")) || [];
            cartItemCount = cartItems.length;
        } else {
            alert("Local storage is not supported in this browser.");
            return;
        }
    }

    var cartItemCountElement = document.getElementById("cartItemCount");
    if (cartItemCountElement) {
        cartItemCountElement.textContent = cartItemCount;
    }
}


document.addEventListener("DOMContentLoaded", function () {
    updateCartItemCoun2();
});
