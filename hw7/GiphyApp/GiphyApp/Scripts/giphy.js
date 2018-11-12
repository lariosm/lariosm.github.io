var boringWords = ["i", "in", "a", "but", "going", "i'm", "my", ",", ".", "",
                   "for", "not", "of", "to", "from", "make", "just", "know",
                   "other", "this", "than", "then", "as", "he", "he's", "his",
                   "him", "she", "she's", "her", "so", "we", "is", "be", "see",
                   "you", "later", "the", "i've", "isn't", "got"];

function giphyRequest(keyWord) {
    var source = "Giphy/Image/" + keyWord;

    $.ajax({
        type: "GET",
        dataType: "json",
        url: source,
        success: displayGIF,
        error: ajaxError
    });
}

function displayGIF(imageData) {
    var image = imageData.data.images.fixed_height_small.url;

    $("#live-display").append("<img src=\"" + image + "\"/>&nbsp;");
}

function ajaxError() {
    alert("Check that your API key is active.");
}

function main() {
    $("#textbox").keypress(function (e) {
        if (e.keyCode == 32) { //if spacebar is pressed
            var input = document.getElementById("textbox").value;
            input = input.split(" ");
            input = input[input.length - 1];
            input = input.toLowerCase(); //Converts input to lowercase to check against "boring" words

            //Checks if the input is a "boring" word
            if (boringWords.includes(input)) { //It's a boring word
                $("#live-display").append("<label>" + input + "</label>&nbsp;")
            }
            else { //It's a "fun" word
                giphyRequest(input);
            }
        }
    });
}

$(document).ready(main);