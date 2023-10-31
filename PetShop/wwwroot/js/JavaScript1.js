// Hàm cập nhật tổng tiền
function updateGrandTotal() {
    var grandTotal = 0;
    var checkboxes = document.querySelectorAll(input[type="checkbox"]);
    for (var i = 0; i < checkboxes.length; i++) {
        if (checkboxes[i].checked) {
            var id = checkboxes[i].id.split('-')[1];
            var total = document.getElementById(total- + id).innerText;
            total = total.replace(/[^0-9.-]+/g, "");
            grandTotal += parseFloat(total);
        }
    }
    document.getElementById(grand-total-display).innerText = "Grand Total: $" + grandTotal.toFixed(2);
}

function updateQuantity(productId, change, price, productName, productImage, action) {
    var inputElement = document.getElementById(productId);
    var quantity = parseInt(inputElement.value);

    if (action === increase) {
        quantity += change; // Increase the quantity
    } else if (action === decrease) {
        quantity += change; // Decrease the quantity
    }

    if (quantity <= 0) {
        removeFromLocalStorage(productId);
        var row = document.getElementById(row- + productId);
        row.remove();
    } else {
        UpdateToLocalStorage(productId, productName, price, productImage, quantity);
        inputElement.value = quantity;
        var totalElement = document.getElementById(total- + productId);
        totalElement.innerText = "$" + (quantity * price).toFixed(2);
    }

    updateGrandTotal();
}


// Hàm loại bỏ sản phẩm khỏi Local Storage
function removeFromLocalStorage(productId) {
    if (typeof (Storage) !== "undefined") {
        var cartItems = JSON.parse(localStorage.getItem("cartItems")) || [];
        cartItems = cartItems.filter(item => item.productId !== productId);
        localStorage.setItem("cartItems", JSON.stringify(cartItems));
    } else {
        alert("Local storage is not supported in this browser.");
    }

    updateGrandTotal();
}


function UpdateToLocalStorage(productId, productName, productPrice, productImage, quantity) {
    if (typeof (Storage) !== "undefined") {
        var cartItems = JSON.parse(localStorage.getItem("cartItems")) || [];
        var existingItem = cartItems.find(item => item.productId === productId);

        if (existingItem) {
            existingItem.quantity = quantity;
        } else {
            cartItems.push({ productId, productName, productPrice, productImage, quantity });
        }

        localStorage.setItem("cartItems", JSON.stringify(cartItems));
    } else {
        alert("Local storage is not supported in this browser.");
    }
}


function getCartItemsFromLocalStorage() {
    if (typeof (Storage) !== "undefined") {
        var cartItems = JSON.parse(localStorage.getItem("cartItems")) || [];
        return cartItems;
    } else {
        alert("Local storage is not supported in this browser.");
        return [];
    }
}

window.onload = function () {
    var cartItems = getCartItemsFromLocalStorage();
    var tbody = document.getElementById("cartItemsDisplay");

    for (var i = 0; i < cartItems.length; i++) {
        var cartItem = cartItems[i];
        var total = cartItem.productPrice * cartItem.quantity;

        var row = document.createElement("tr");
        row.innerHTML = `
        <td>
            <div>
                <input type="checkbox" id="select-${cartItem.productId}" onclick="updateGrandTotal()">
            </div>
        </td>
        <td>
            <div class="d-flex img-cart">
                <img src="${cartItem.productImage}" alt="">
            </div>
        </td>
        <td>
            <div class="media">
                <div class="media-body">
                    <p>${cartItem.productName}</p>
                </div>
            </div>
        </td>
        <td>
            <h5>$${cartItem.productPrice.toFixed(2)}</h5>
        </td>
        <td>
            <div class="product_count">
                <input type="text" name="qty" id="${cartItem.productId}" maxlength="12" value="${cartItem.quantity}" title="Quantity:" class="input-text qty">
                <button onclick="updateQuantity(${cartItem.productId}, 1, ${cartItem.productPrice}, ${cartItem.productName}, ${cartItem.productImage}, increase)" class="increase items-count" type="button">
                    <i class="lnr lnr-chevron-up"></i>
                </button>
                <button onclick="updateQuantity(${cartItem.productId}, -1, ${cartItem.productPrice}, ${cartItem.productName}, ${cartItem.productImage}, decrease)" class="reduced items-count" type="button">
                    <i class="lnr lnr-chevron-down"></i>
                </button>
            </div>

        </td>
        <td id="total-${cartItem.productId}">
            <h5>$${total.toFixed(2)}</h5>
        </td>`;

        tbody.appendChild(row);
    }

};