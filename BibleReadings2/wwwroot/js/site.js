// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const selectElement = document.querySelector('#readerSelect');
selectElement.addEventListener('change', (event) => {
    const name = event.target.value;
    if(name === undefined || name === '') {
        return;
    }

    const reader = {
        "name": name,
        "date": new Date().toISOString()
    };

    const request = new XMLHttpRequest();
    request.addEventListener('load', onReaderSaved);
    request.open('PUT', '/api/reader');
    request.setRequestHeader('Content-Type', 'application/json');
    request.send(JSON.stringify(reader));
})

function onReaderSaved() {
    if(this.status !== 200) {
        return;
    }

    const p = document.querySelector('#reader');
    const description = JSON.parse(this.responseText);
    p.innerHTML = description.description;
}
