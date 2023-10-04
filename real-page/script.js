let data = [];

let x = 0;
let y = 0;
let isDown = false;
let text = "";

document.onmousemove = e =>
{
    x = e.clientX
    y = e.clientY
}

document.onkeydown = e => text = e.key;

document.onmousedown = _ => isDown = true;

document.onmouseup = _ => isDown = false;

setInterval(() => {
    data.push({
        x: x,
        y: y,
        isDown: isDown,
        text: text
    });

    text = "";
}, 50);

function save()
{
    var a = document.createElement("a");

    let textData = JSON.stringify(data)
        .replaceAll("},{", "},\n\t{", )
        .replace("[", "[\n\t")
        .replace("]", "\n]");

    var file = new Blob([textData], { type: 'text/plain' });
    a.href = URL.createObjectURL(file);
    a.download = "user-data.json";
    a.click();
    data = [];
}