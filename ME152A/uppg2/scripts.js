let numberOfPicturesInGame;
let numberOfPicturesInGameDefault = 5;
let memoryCards = [];
let cardNotTurned = `https://farm6.staticflickr.com/5105/5571418938_8efb0ae32c.jpg`;

let turnedCards = [];
let scoreCeeper = 0;
let speedOfFadeAlert = 50;
let speedOfFadeImages = 500;

// This function gets pictures to be used from the Flickr api
function GetFlickrResentPhotos() {
    fetch("https://api.flickr.com/services/rest?method=flickr.photos.getRecent&api_key=b7a5d4b3f4ceae6f8a6e98af1fbac4f4&format=json&nojsoncallback=1")
        .then(response => response.json())
        .then(json => {
            CreateMemoryCards(json.photos.photo.sort(() => Math.random() - 0.5));
        }).then(() => WriteMemmoryCardsToGui());
}

// This function creates the memory cards used in the game
// Parameter: Photos - images fetched from the Flickr api
function CreateMemoryCards(photos) {
    for (let i = 0; i < numberOfPicturesInGame; i++) {
        let memoryCard = {};
        let currentPhoto = photos[i];
        memoryCard.pictureWhenTurned = `https://farm${currentPhoto.farm}.staticflickr.com/${currentPhoto.server}/${currentPhoto.id}_${currentPhoto.secret}.jpg`;
        memoryCard.pictureWhenNotTurned = cardNotTurned;
        memoryCard.turned = false;

        memoryCard.id = i;
        memoryCards.push(memoryCard);
        let memoryCard2 = memoryCard;
        memoryCards.push(memoryCard2);
        memoryCards = memoryCards.sort(() => Math.random() - 0.5);
    }
}

// This function turns a memory card
// Param: event - used to check that an image was clicked and when switching images
function Turn(event) {
    if (event.target.tagName.toLowerCase() === "img") {
        let chosenImageOject = memoryCards.filter(x => x.id === parseInt(event.target.dataset.id))[0];
        chosenImageOject.turned = true;
        ChangeImage(chosenImageOject, event);
        SetScoreOrRegretTurn(chosenImageOject).then((idOfTurnedCard) => {
            turnedCards.push(chosenImageOject);

            // If not doing this check the pictures would be removed even if a couple has not been turned up
            if (turnedCards.length % 2 === 0) {
                document.querySelector("#scoreCountView > span").textContent = ++scoreCeeper;
                if (numberOfPicturesInGame === scoreCeeper) {
                    FetchHappyPicture();
                }
            }

        }).catch(imageObject => {
            window.setTimeout(function () {
                RegretTurnMisMatchingPair(imageObject);
            }, 1000);
        });
    }
}

// This function fetches a happy picture from the Giphy api, when all cards have been collected
function FetchHappyPicture() {
    fetch("http://api.giphy.com/v1/gifs/search?api_key=mcdoiGMok69AqKOKB7F4LrNYdoHVTdyB&q=happy&limit=10").then(response => response.json())
        .then(json => {
            let randomIndex = Math.floor((Math.random() * 10).toFixed());
            ShowGiphyAlert("You made it!", `<img src="${json.data[randomIndex].images.original.url}"/>`);
        });
}

// This function restarts the game by reloading the page
function StartOver() {
    location.reload();
}

// Checks if Score should be set a if turn should be rejected via promise
// Param: chosenImageOject - object to check setScore or regret
// Returns promise
function SetScoreOrRegretTurn(chosenImageOject) {
    return new Promise((resolve, reject) => {
        if ((turnedCards.length % 2 !== 0) && (turnedCards[turnedCards.length - 1].id !== chosenImageOject.id)) {
            reject(chosenImageOject);
        }

        resolve(chosenImageOject.id);
    });
}

// This function Regrets turn of mismatching pair
// Param: lastCardInPairTurned - object of card last turned
function RegretTurnMisMatchingPair(lastCardInPairTurned) {
    lastCardInPairTurned.turned = false;
    let lastCardTurnedUpElement = $(`#memoryContainer img:not([src='${lastCardInPairTurned.pictureWhenNotTurned}'])[data-id='${lastCardInPairTurned.id}']`);
    $(lastCardTurnedUpElement).fadeOut(0, () => $(lastCardTurnedUpElement).attr("src", lastCardInPairTurned.pictureWhenNotTurned)).fadeIn(speedOfFadeImages);

    let firstCardInPairTurned = turnedCards.pop();
    firstCardInPairTurned.turned = false;
    let firstCardTurnedUpElement = $(`#memoryContainer img:not([src='${firstCardInPairTurned.pictureWhenNotTurned}'])[data-id='${firstCardInPairTurned.id}']`);
    $(firstCardTurnedUpElement).fadeOut(0, () => $(firstCardTurnedUpElement).attr("src", firstCardInPairTurned.pictureWhenNotTurned)).fadeIn(speedOfFadeImages);
}

// This function changes images depending when turned
// Param: chosenImageOject - used to check what picture to use
// Param: event used to know what element to change picture of
function ChangeImage(chosenImageOject, event) {
    $(event.target).fadeOut(0, () => $(event.target).attr("src", chosenImageOject.pictureWhenTurned).fadeIn(speedOfFadeImages));
}

// Write the memory cards to the GUI
function WriteMemmoryCardsToGui() {
    let memoryContainer = document.querySelector("#memoryContainer");
    memoryContainer.addEventListener("click", (event) => Turn(event));

    memoryCards.map((element, index) => {
        let imgElement = document.createElement("img");
        imgElement.src = element.pictureWhenNotTurned;
        imgElement.dataset.id = element.id;

        memoryContainer.appendChild(imgElement);
    });
}

// This function shows an alert with an image from giphy
// Param: header - header of alert
// Param: imageToDisplay - image to be displayed
function ShowGiphyAlert(header, imageToDisplay) {
    var alertCode = '<div id="overlay"></div>';
    alertCode += `<div id="alertContainer"><div id="alertContent"><div id="header"><h1>${header}</h1></div><div id="body">${imageToDisplay}<p>Do you want to play again?</p></div><div id="footer"><span><button data-reload>Yes, reload</button><button>No, close this message</button></span></div></div>`;
    $("body").prepend(alertCode);

    $("#alertContainer button").click(function (event) {
        if (event.target.dataset.reload != null) {
            StartOver();
        } else {
            CloseAlert();
        }
    });
}

// This function shows a confirm message
// Param: header - header of confirm
// Paam: message - message of confirm
// Param: labelOfInput - placeholder used in the input element
// Param: initial - controls wether extra button for description should be shown
function ShowConfirm(header, message, labelOfInput, initial = "") {
    var buttonIfInitial = (initial === "initial") ? '<button data-description>Description of service</button>' : '';

    var alertCode = '<div id="overlay"></div>';
    alertCode += `<div id="alertContainer"><div id="alertContent"><div id="header"><h1>${header}</h1></div><div id="body"><p>${message}</p><input type="text" placeholder="${labelOfInput}"/></div><div id="footer"><span><button data-send>Save</button><button>Do not save, close this message</button>${buttonIfInitial}</span></div></div>`;
    $("body").prepend(alertCode);

    $("#alertContainer button").click(function (event) {
        if (event.target.dataset.send != null) {
            let numberOfPairs = parseInt($("#alertContainer input").val());

            if (numberOfPairs < numberOfPicturesInGameDefault || isNaN(numberOfPairs)) {
                SetNumberOfPairsEntered(numberOfPairs);
                CloseAlert();
                ShowAlert("Information", (!isNaN(numberOfPairs)) ? `You entered less than ${numberOfPicturesInGameDefault}, it is minimum and has been set` : `You entered nothing, ${numberOfPicturesInGameDefault} pairs is set`, "p");
                GetFlickrResentPhotos();
            } else {
                SetNumberOfPairsEntered(numberOfPairs);
                GetFlickrResentPhotos();
                CloseAlert();
            }
        } else if (event.target.dataset.description != null) {
            CloseAlert();
            let olDescription = "<ol><li>A memory game that is using the flickr api to get images</li><li>Jquery to make memory cards, alerts and confirms fade. <li>Jquery is also used for building alerts and confirms</li></li><li>When all pairs are turned, giphy is used to show a message with a happy gif of some sort</li></ol><strong>Lessons learned:</strong><p>It was interesting and informative to use the apis, <br/> I have used Jquery before, so the most informative part was the use of the apis </p> <p>I have not used Jquery effects with callbacks. <br/> That was the most informative part regarding Jquery</p>";
            ShowAlert("Service description", `<p>The service is made up of:</p>${olDescription}`, "div", "left", "initial");
        } else {
            CloseAlert();
        }
    });
}

// This function is used by ShowConfirm to set the number of pairs that the user has entered
function SetNumberOfPairsEntered(numberOfPairs) {
    let numberPairsGiven = (!isNaN(numberOfPairs) || numberOfPairs > numberOfPicturesInGameDefault) ? parseInt($("#alertContainer input").val()) : numberOfPicturesInGameDefault;
    numberOfPicturesInGame = (numberPairsGiven === 0) ? numberOfPicturesInGameDefault : numberPairsGiven;
}

// This function shows an message alert
// Param: header - header of alert
// Paam: message - message of alert
// Param: messageContainerElementTextAlign - used to add other textAlign, to better control alignment of bigger paragraphs
// Param: initial - controls wether extra buttons is to be shown when called with description
function ShowAlert(header, message, messageContainerElement, messageContainerElementTextAlign = "", initial = "") {
    let buttonIfInitial = (initial === "initial") ? `<button data-start-reload>Done reading, reload</button><button data-start-default>Done reading, start game with ${numberOfPicturesInGameDefault} pairs</button>` : '<button data-close>Close message</button>';
    let textAlignIfAny = (messageContainerElementTextAlign != "") ? `style="text-align: ${messageContainerElementTextAlign}"` : "";

    var alertCode = '<div id="overlay"></div>';
    alertCode += `<div id="alertContainer"><div id="alertContent"><div id="header"><h1>${header}</h1></div><div id="body"><${messageContainerElement} ${textAlignIfAny}>${message}</${messageContainerElement}></div><div id="footer"><span>${buttonIfInitial}</span></div></div>`;
    $("body").prepend(alertCode);

    $("#alertContainer button").click(function (event) {
        if (event.target.dataset.close != null) {
            CloseAlert();
        } else if (event.target.dataset.startDefault != null) {
            SetNumberOfPairsEntered();
            GetFlickrResentPhotos();
            CloseAlert();
        } else if (event.target.dataset.startReload != null) {
            StartOver();
        }
    });
}

// Closes the alert/confirm
function CloseAlert() {
    $("#overlay").fadeOut(speedOfFadeAlert, () => $("#overlay").remove());
    $("#alertContainer").fadeOut(speedOfFadeAlert, () => $("#overlay").remove());
}

ShowConfirm("Welcome to this memory game", `How many pairs do you desire? If nothing is entered ${numberOfPicturesInGameDefault} is default, you can´t go under this number.`, "Number of pairs", "initial");