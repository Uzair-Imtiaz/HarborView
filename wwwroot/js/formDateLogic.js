let today = new Date(),
    day = today.getDate(),
    month = today.getMonth() + 1,
    year = today.getFullYear();
    hours = today.getHours(),
    minutes = today.getMinutes();
if (day < 10) {
    day = '0' + day
}
if (month < 10) {
    month = '0' + month
}
if (hours < 10) {
    hours = '0' + hours;
}
if (minutes < 10) {
    minutes = '0' + minutes;
}
today = year + '-' + month + '-' + day;
var currentTime = hours + ':' + minutes;

const checkinInput = document.getElementById('checkin');
checkinInput.setAttribute("min", today);
checkinInput.setAttribute("value", today);

const checkoutInput = document.getElementById('checkout');
const checkinDate = new Date(checkinInput.value);
const minCheckoutDate = new Date(checkinDate);
minCheckoutDate.setDate(checkinDate.getDate() + 1);

checkoutInput.value = minCheckoutDate.toISOString().split('T')[0];
checkoutInput.min = minCheckoutDate.toISOString().split('T')[0];

// Validates time input
const timeInput = document.getElementById('time');
timeInput.min = currentTime;
timeInput.value = currentTime;
timeInput.addEventListener('input', function () {
    var selectedTime = timeInput.value;
    if (selectedTime < currentTime) {
        timeInput.value = currentTime;
    }
});

// Function to validate checkout date
function updateCheckoutDate() {
    const checkinInput = document.getElementById('checkin');
    const checkoutInput = document.getElementById('checkout');

    const checkinDate = new Date(checkinInput.value);
    const minCheckoutDate = new Date(checkinDate);
    minCheckoutDate.setDate(checkinDate.getDate() + 1);

    checkoutInput.min = minCheckoutDate.toISOString().split('T')[0];
    checkoutInput.value = minCheckoutDate.toISOString().split('T')[0];
}
