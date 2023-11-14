// Hàm cập nhật tổng tiền
var selectedProducts = [];
function updateGrandTotal() {
    var grandTotal = 0;
    var checkboxes = document.querySelectorAll('input[type="checkbox"]');
    selectedProducts = [];
    for (var i = 0; i < checkboxes.length; i++) {
        if (checkboxes[i].checked) {
            var id = checkboxes[i].id.split('-')[1];
            var total = document.getElementById('total-' + id).innerText;

            var productName = document.getElementById('name-' + id).innerText;



            var Quanlity = document.getElementById("quantity-" + id).value;
            alert(Quanlity);


            var image = document.getElementById('image-' + id).src;
            console.log(image);
            total = total.replace(/[^0-9.-]+/g, "");
            grandTotal += parseFloat(total);
            selectedProducts.push({ id: id, total: parseFloat(total), name: productName, quantity: Quanlity, image: image });
        }
    }
    document.getElementById('grand-total-display').innerText = "Grand Total: $" + grandTotal.toFixed(2);
}

function GetInforUser() {
    var email = document.getElementById("email").value;
    var fullName = document.getElementById("fullName").value;
    var contactAddress = document.getElementById("contactAddress").value;

    // Get selected values from select elements
    var selectedProvince = document.getElementById("city");
    var selectedDistrict = document.getElementById("district");
    var selectedWard = document.getElementById("ward");
    var textcity = selectedProvince.options[selectedProvince.selectedIndex].text;
    var textdistrict = selectedDistrict.options[selectedDistrict.selectedIndex].text;
    var textward = selectedWard.options[selectedWard.selectedIndex].text;
    var customer_contact_info = textcity + " " + textdistrict + " " + textward;

    // Create an object with user information
    var userInfo = {
        email: email,
        fullName: fullName,
        contactAddress: contactAddress,
        customer_contact_info: customer_contact_info,
    };

    // Push the user information object into the selectedProducts array
    selectedProducts.push(userInfo);

    // You can now use the selectedProducts array as needed, e.g., send it to your server or perform other actions.
    console.log(selectedProducts); // Log the selected products for testing

    // If you want to send this data to your server, you can use an AJAX request or any suitable method.
}
function updateQuantity(productId, change, price, productName, productImage, action) {
    var inputElement = document.getElementById(productId);
    var quantity = parseInt(inputElement.value);

    if (action === 'increase') { // Use 'increase' as a string
        quantity += change; // Increase the quantity
    } else if (action === 'decrease') { // Use 'decrease' as a string
        quantity += change; // Decrease the quantity
    }

    if (quantity <= 0) {
        removeFromLocalStorage(productId);
        var row = document.getElementById('row-' + productId); // Use quotes around the ID
        row.remove();
    } else {
        UpdateToLocalStorage(productId, productName, price, productImage, quantity);
        inputElement.value = quantity;
        var totalElement = document.getElementById('total-' + productId); // Use quotes around the ID
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
            <div class="d-flex img-cart" >
                <img class="img-fluid" id="image-${cartItem.productId}" src="${cartItem.productImage}" alt="" >
            </div>
        </td>
        <td>
            <div class="media">
                <div class="media-body" id="name-${cartItem.productId}">
                    <p>${cartItem.productName}</p>
                </div>
            </div>
        </td>
        <td>
            <h5>$${cartItem.productPrice.toFixed(2)}</h5>
        </td>
        <td>
            <div class="product_count">
                <input type="text" name="qty" id="quantity-${cartItem.productId}" maxlength="12" value="${cartItem.quantity}" title="Quantity:" class="input-text qty">
                <button onclick="updateQuantity('${cartItem.productId}', 1, ${cartItem.productPrice}, '${cartItem.productName}', '${cartItem.productImage}', 'increase');" class="increase items-count" type="button">
                    <i class="lnr lnr-chevron-up"></i>
                </button>
                <button onclick="updateQuantity('${cartItem.productId}', -1, ${cartItem.productPrice}, '${cartItem.productName}', '${cartItem.productImage}', 'decrease');" class="reduced items-count" type="button">
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

document.getElementById("proceed-to-checkout").addEventListener("click", () => {
    // Xử lý khi người dùng nhấn nút "Checkout"
    GetInforUser();
    fetch('/api/Cart/checkout', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(selectedProducts),


    })
        .then(response => {
            if (response.ok) {
                // Handle success, e.g., show a success message or redirect to a confirmation page
                console.log("Checkout successful!");
            } else {
                // Handle errors, e.g., show an error message
                console.log("Checkout failed.");
            }
        })
        .catch(error => { // Add the parameter to capture the error
            // Handle the error
            console.error(error.message);
        });
});

