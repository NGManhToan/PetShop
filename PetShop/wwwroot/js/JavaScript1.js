// Hàm cập nhật tổng tiền
var selectedProducts = [];
var userInfo = [];
function updateGrandTotal() {
    var grandTotal = 0;
    var checkboxes = document.querySelectorAll('input[type="checkbox"]');
    selectedProducts = [];
    for (var i = 0; i < checkboxes.length; i++) {
        if (checkboxes[i].checked) {
            var id = checkboxes[i].id.split('-')[1];
            var Price = document.getElementById('total-' + id).innerText;

            var productName = document.getElementById('name-' + id).innerText;
            var Quanlity = document.getElementById("quantity-" + id).value;
            var image = document.getElementById('image-' + id).src;
            console.log(image);
            Price = Price.replace(/[^0-9.-]+/g, "");
            grandTotal += parseFloat(Price);
            selectedProducts.push({ id: parseFloat(id), Price: Price, name: productName, quantity: Quanlity, image: image });
        }
    }
    document.getElementById('grand-total-display').innerText = "Grand Total: $" + grandTotal.toFixed(2);
}

function GetInforUser() {
    var email = document.getElementById("email").value;
    var fullName = document.getElementById("fullName").value;
    var PhoneNumber = document.getElementById("number").value;
    var contactAddress = document.getElementById("deliveryaddress").value;

    // Get selected values from select elements
    var selectedProvince = document.getElementById("city");
    var selectedDistrict = document.getElementById("district");
    var selectedWard = document.getElementById("ward");
    var textcity = selectedProvince.options[selectedProvince.selectedIndex].text;
    var textdistrict = selectedDistrict.options[selectedDistrict.selectedIndex].text;
    var textward = selectedWard.options[selectedWard.selectedIndex].text;
    var customer_contact_info = contactAddress + " " + textcity + " " + textdistrict + " " + textward;

    // Create an object with user information
    var Info = {
        email: email,
        fullName: fullName,
        PhoneNumber: PhoneNumber,
        customer_contact_info: customer_contact_info,
    };

    // Set userInfo as an object, not an array
    userInfo = Info;

    //// You can now use the selectedProducts array as needed, e.g., send it to your server or perform other actions.
    console.log(userInfo);
    console.log(selectedProducts); // Log the selected products for testing
}



function updateQuantity(productId, change, price, productName, productImage, action) {
    var inputElement = document.getElementById("quantity-" + productId);
    var quantity = inputElement ? parseInt(inputElement.value) : 0;

    if (action === 'increase') { // Use 'increase' as a string
        quantity += change; // Increase the quantity
    } else if (action === 'decrease') { // Use 'decrease' as a string
        quantity += change; // Decrease the quantity
    }

    if (quantity <= 1) {
        // Show confirmation dialog
        var confirmDelete = window.confirm("Do you want to delete this product?");
        if (confirmDelete) {
            removeProductAndReload(productId);
        } else {
            // Reset quantity to 1 if the user cancels
            quantity = 1;
        }
    } else {
        UpdateToLocalStorage(productId, productName, price, productImage, quantity);
        inputElement.value = quantity;
        var totalElement = document.getElementById('total-' + productId);
        totalElement.innerText = "$" + (quantity * price).toFixed(2);
    }

    updateGrandTotal();
}

/* Hàm loại bỏ sản phẩm khỏi Local Storage*/
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
function removeProductAndReload(productId) {
    // Xóa thông tin từ localStorage
    removeFromLocalStorage(productId);

    // Xóa phần tử khỏi DOM nếu tồn tại
    var row = document.getElementById('row-' + productId);
    if (row) {
        row.remove();
    }

    // Tải lại trang
    location.reload();
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


function validateForm() {
    let fullName = document.getElementById('fullName').value;
    let email = document.getElementById('email').value;
    let number = document.getElementById('number').value;
    let city = document.getElementById('city').value;
    let district = document.getElementById('district').value;
    let ward = document.getElementById('ward').value;
    let deliveryAddress = document.getElementById('deliveryaddress').value;

    // Check if any required field is empty
    if (!fullName || !email || !number || !city || !district || !ward || !deliveryAddress) {
        alert('Please fill in all required fields');
        return false;
    }
    return true;
}

document.getElementById("proceed-to-checkout").addEventListener("click", () => {
    // Kiểm tra xem có sản phẩm được chọn không
    if (validateForm()) {
        // Check if there are selected products
        if (selectedProducts.length === 0) {
            alert("Vui lòng chọn sản phẩm trước khi thanh toán.");
        } else {
            // Xử lý khi người dùng nhấn nút "Checkout"
            GetInforUser();
            fetch('/api/Cart/noAccount', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    CartItems: selectedProducts,
                    InfoUser: userInfo,
                }),
            })
                .then(response => {
                    if (response.ok) {
                        console.log("Thanh toán thành công!");

                        // Xóa các sản phẩm đã chọn từ local storage
                        clearLocalStorage(selectedProducts);

                        // Hiển thị thông báo thành công
                        alert("Thanh toán thành công!");

                        // Tải lại trang
                        location.reload();
                    } else {
                        console.log("Thanh toán thất bại.");
                        // Hiển thị thông báo thất bại
                        alert("Thanh toán thất bại.");
                    }
                })
                .catch(error => {
                    console.error(error.message);
                });
        }
    }
    // If form validation fails, it will halt the checkout process.
});


// Add a function to clear selected products from local storage
function clearLocalStorage(productsToRemove) {
    if (typeof (Storage) !== "undefined") {
        // Get current cart items from local storage
        var cartItems = JSON.parse(localStorage.getItem("cartItems")) || [];

        /*        console.log("Before filter:", cartItems);*/

        // Remove selected products from the local storage array
        cartItems = cartItems.filter(item => !productsToRemove.some(selectedItem => selectedItem.id === parseInt(item.productId)));

        /*        console.log("After filter:", cartItems);*/

        // Update local storage with the modified cart items
        localStorage.setItem("cartItems", JSON.stringify(cartItems));
    } else {
        alert("Local storage is not supported in this browser.");
    }
}