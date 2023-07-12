function togglePage(buttonId) {
    var button1 = document.getElementById('button1');
    var button2 = document.getElementById('button2');
    var page1Content = document.getElementById('page1-content');
    var page2Content = document.getElementById('page2-content');

    if (buttonId === 'button1') {
        button1.classList.add('active');
        button2.classList.remove('active');
        page1Content.classList.add('active');
        page2Content.classList.remove('active');
    } else if (buttonId === 'button2') {
        button2.classList.add('active');
        button1.classList.remove('active');
        page2Content.classList.add('active');
        page1Content.classList.remove('active');
    }
}
