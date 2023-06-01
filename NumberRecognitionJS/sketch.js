function setup() {
    let fileURL = "./newdata.bytes";
    readFileFromURL(fileURL);
}

function readFileFromURL(fileURL) {
    fetch(fileURL)
        .then((response) => response.arrayBuffer())
        .then((arrayBuffer) => {
            let binaryString = convertArrayBufferToBinaryString(arrayBuffer);
            console.log(binaryString);
        })
        .catch((error) => {
            console.log("Failed to load the file:", error);
        });
}

function convertArrayBufferToBinaryString(arrayBuffer) {
    let binaryString = "";
    let bytes = new Uint8Array(arrayBuffer);
    let length = bytes.byteLength;

    for (let i = 0; i < length; i++) {
        binaryString += String.fromCharCode(bytes[i]);
    }

    return binaryString;
}

window.addEventListener("DOMContentLoaded", setup);
