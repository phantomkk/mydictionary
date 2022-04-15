// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function deleteWord(wordGuid) {
    console.log(wordGuid);
    $.ajax({
        cache: false,
        type: 'get',
        url: '/DeleteNewWord',
        contentType: "application/json",
        data: { wordId: wordGuid },
        success: function (data, status, xhr) {
            alert("Data: " + data + "\nStatus: " + status);
        }
    });
}

function getTooltip(e) {
    e.stopPropagation();
    var selectedText = getSelectionText();
    if (selectedText.length === 0) {
        $('.mytooltip').css({
            top: y,
            left: x + 50
        }).hide();
        return;
    }
    $("#description").html(selectedText);

    console.log(selectedText);
    var x = event.clientX;
    var y = event.clientY;

    console.log(selectedText.length);
    var tar = e.target.getBoundingClientRect();
    console.log(tar);
    $('.mytooltip').css({
        top: y,
        left: x + 50
    }).show();
}

$(document).on("click", function (e) {
    e.stopPropagation();
    $('.mytooltip').hide();
});

function getSelectionText() {
    var text = "";
    if (window.getSelection) {
        text = window.getSelection().toString();
    } else if (document.selection && document.selection.type !== "Control") {
        text = document.selection.createRange().text;
    }
    return text;
}

function addWord() {
    var exId = $("#exampleId").val();
    var word = getSelectionText();
    var jsoon = JSON.stringify({
        exampleId: exId,
        word: word
    });
    console.log(jsoon);
    $.ajax({
        cache: false,
        type: 'post',
        url: '/Home/AddWord/',
        contentType: "application/json;",
        data: jsoon,
        success: function (data, status, xhr) {
            alert("Add new word " + status);
        }
    });
}