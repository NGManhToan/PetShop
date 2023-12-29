// Gọi API gửi mail
fetch('/api/mail/sendmail', {
    method: 'POST',
    body: new FormData(contactForm)
})
    .then(response => response.json())
    .then(data => {
        if (data.statusCode === 200) {
            alert("Mail has successfully been sent.");
        } else {
            alert("An error occurred. The Mail could not be sent.");
        }
    })
    .catch((error) => {
        console.error('Error:', error);
    });
