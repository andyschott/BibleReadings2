const selectElement = document.querySelector('#readerSelect');

if (selectElement) {
    selectElement.addEventListener('change', (event) => {
        const name = event.target.value;
        if (name === undefined || name === '') {
            return;
        }

        const reader = {
            name: name
        };

        const request = new XMLHttpRequest();
        request.addEventListener('load', onReaderSaved);
        request.open('PUT', '/api/reader');
        request.setRequestHeader('Content-Type', 'application/json');
        request.send(JSON.stringify(reader));
    });
}

function onReaderSaved() {
    if (this.status !== 200) {
        return;
    }

    const readerDescription = document.querySelector('#reader');
    if (!readerDescription) {
        return;
    }

    const description = JSON.parse(this.responseText);
    readerDescription.innerHTML = description.description;
}

const passageSearch = document.querySelector('#passageSearch');
if (passageSearch) {
    passageSearch.addEventListener('click', lookupPassage);
}

const passageInput = document.querySelector('#passage');
if (passageInput) {
    passageInput.addEventListener('keydown', (event) => {
        if (event.key === 'Enter') {
            event.preventDefault();
            lookupPassage();
        }
    });
}

function lookupPassage() {
    const passageInput = document.getElementById('passage');
    if (!passageInput || !passageInput.value) {
        return;
    }

    const uri = encodeURI(`/api/lookup/${passageInput.value}`);

    const request = new XMLHttpRequest();
    request.addEventListener('load', onPassageUrlReceived);
    request.open('GET', uri);
    request.setRequestHeader('Content-Type', 'application/json');
    request.send();
}

function onPassageUrlReceived() {
    if (this.status !== 200) {
        return;
    }

    const url = JSON.parse(this.responseText);
    window.location.href = url;
}
