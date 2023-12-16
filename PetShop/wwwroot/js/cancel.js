function cancelItem(id, name, image, price, quantity, status) {
    const orderedCart = {
        Id: id,
        Name: name,
        Image: image,
        Price: price,
        Quantity: quantity,
        Status: status
    };
    fetch('/api/Cart/CancelItem', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(orderedCart)
    })

        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            console.log('Item canceled');
            alert('Đã hủy thành công');
            location.reload();
        })

        .catch(error => {
            console.error(error.message);
        });
}
