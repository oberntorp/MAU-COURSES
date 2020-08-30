let gameWidth = window.innerWidth - 20;
let gameHeight = window.innerHeight - 20;

let config = {
    type: Phaser.AUTO,
    width: gameWidth,
    height: gameHeight,
    physics: {
        default: 'arcade',
        arcade: {
            gravity: {
                y: 200
            }
        }
    },
    scene: {
        preload: preload,
        create: create,
        update: update
    }
};

let game = new Phaser.Game(config);
let platforms;
let player;
let viruses;
let cursors;
let weapons;
let scoreText;
let highScore;
let highScoreText;
let score = 0;
let lastFired = 0;

let soundShooting;
let soundFallingVirus;
let soundGameOver;
let numberOfVirusesStart = 2;

// This function is responsible for loading all assets (images, sounds etc, that is used by the game)
function preload() {
    this.load.image('background', 'assets/backgrounds/spelbg.png');
    this.load.spritesheet('player',
        'assets/players/defender.png', {
            frameWidth: 53,
            frameHeight: 37
        }
    );
    this.load.image('platform', 'assets/platforms/platform.png');
    this.load.image('virus', 'assets/opponents/virus.png');
    this.load.image('weapon', 'assets/wepons/spruta.png');
    this.load.audio('shooting', [
        'assets/sounds/shoot/NovaShot.mp3',
        'assets/sounds/shoot/NovaShot.ogg',
        'assets/sounds/shoot/NovaShot.wav'
    ]);
    this.load.audio('fallingvirus', [
        'assets/sounds/shot_virus/fall.mp3',
        'assets/sounds/shot_virus/fall.ogg',
        'assets/sounds/shot_virus/fall.wav'
    ]);
    this.load.audio('gameover', [
        'assets/sounds/gameover/gameover.mp3',
        'assets/sounds/gameover/gameover.ogg',
        'assets/sounds/gameover/gameover.wav'
    ]);
}

// This function creates all objects that will be used in the game, this includes both messages shown in the start as well as players, platforms etc
function create() {
    getLastHighScore();
    showWelcomeMessage(this);
    setBackgroundImmage(this);
    createInGameMessages(this);
    platforms = this.physics.add.staticGroup();
    generatePlatforms(4);

    soundShooting = this.sound.add('shooting');
    soundFallingVirus = this.sound.add('fallingvirus');
    soundGameOver = this.sound.add('gameover');

    createPlayer(this);

    this.physics.add.collider(player, platforms);

    weapons = this.physics.add.group({
        defaultKey: "weapon",
        maxSize: 1,
        runChildUpdate: true
    });

    this.physics.add.overlap(weapons, platforms, (weapon, platform) => {
        console.log(weapon);
        weapon.setVisible(false);
        weapon.setActive(false);
    });

    this.physics.add.collider(weapons, platforms);

    cursors = this.input.keyboard.createCursorKeys();

    createViruses(this, numberOfVirusesStart);

}

// Saves last highScore and to global letiable
function getLastHighScore() {
    highScore = (localStorage.getItem("highScoreOnGameOver") != null) ? parseInt(localStorage.getItem("highScoreOnGameOver")) : -1;
}

// This function creates messages about Score and HighScore shown in the games upper corners
// Param: scene - game object used to handle physics objects etc
function createInGameMessages(scene) {
    scoreText = scene.add.text(16, 16, "Score: 0", {
        fontSize: "32px",
        fill: "#fff"
    });
    highScoreText = scene.add.text(gameWidth - 540, 16, getHighScoreMessage(), {
        fontSize: "32px",
        fill: "#fff"
    });
}

// This image sets the background image and scales it according to canvas size
// Param: scene - game object used to handle physics objects etc
function setBackgroundImmage(scene) {
    let image = scene.add.image(scene.cameras.main.width / 2, scene.cameras.main.height / 2, "background");
    let scaleX = scene.cameras.main.width / image.width;
    let scaleY = scene.cameras.main.height / image.height;
    let scale = Math.max(scaleX, scaleY);
    image.setScale(scale).setScrollFactor(0);
}

// This function is used to create and do the initial configuration of the player
// Param: scene - game object used to handle physics objects etc
function createPlayer(scene) {
    player = scene.physics.add.sprite(200, 200, "player");
    confiurePlayer(scene);
}

// This function is called by CreatePlayer - it configures initial position and sets the animation of the player
// Param: scene - game object used to handle physics objects etc
function confiurePlayer(scene) {
    player.x = Math.floor(Phaser.Math.Between(0.1, 10) * 100);
    player.setCollideWorldBounds(true);
    scene.anims.create({
        key: 'left',
        frames: [{
            key: 'player',
            frame: 0
        }],
        frameRate: 20,
    });
    scene.anims.create({
        key: 'right',
        frames: [{
            key: 'player',
            frame: 1
        }],
        frameRate: 20,
    });
}

// This function shows a wellcome message to the user, informing about the game and it´s purpose
// Param: scene - game object used to handle physics objects etc
function showWelcomeMessage(scene) {
    scene.physics.pause();
    createSweatAlert("Welcome to 'kill the virus'",
        `As a chicken, you will kill corona viruses with syringes! \n ${getHighScoreMessage()}`,
        "success").then(clickedOrNot => {
        if (clickedOrNot) {
            scene.physics.resume();
        }
    });
}

// This function gets the message to display about the highscore
// Returns highScoreMessage
function getHighScoreMessage() {
    let messageOnHighScore = (highScore) ? `Your last highScore was: ${highScore}` : "This is your first time here";

    return messageOnHighScore;
}

// This function generates the platforms in the game
// Param: numberOfPlatforms - how many platforms will be created
function generatePlatforms(numberOfPlatforms) {
    let xCordinate = 130;
    let distanceBetweenPlatforms = (gameWidth) / numberOfPlatforms;

    for (let i = 0; i < numberOfPlatforms; i++) {
        platforms.create(xCordinate, 468, "platform");
        xCordinate += distanceBetweenPlatforms;
    }
}

// This function creates the viruses in the game
// Param: scene - game object used to handle physics objects etc
// Param: numberOfViruses - how many viruses that will be created
function createViruses(scene, numberOfViruses) {
    scene.time.addEvent({
        delay: 5090,
        callback: () => {
            viruses = scene.physics.add.group({
                key: "virus",
                repeat: numberOfViruses,
                setXY: {
                    x: 60,
                    y: 70,
                    stepX: (gameWidth) / numberOfViruses
                }
            });
            scene.physics.add.collider(viruses, platforms);
            configureViruses();
            overlapVirusesWeapons(scene);
            overlapVirusesPlayer(scene);
        },
        loop: false
    });
}

// This function is responsible for configuring the viruses, namely: velocity, setColiderWithWorldBounds and bounce
function configureViruses() {
    viruses.children.iterate(virus => {
        virus.setVelocity(Phaser.Math.Between(0.5, 4), Phaser.Math.Between(0.9, 2));
        virus.setCollideWorldBounds(true);
        virus.setBounce(Phaser.Math.Between(0.1, 1));
    });
}

// This function controles what happens then Viruses and weapons overlap (collide)
// Namely: disables the virus, plays a sound and lastly, as long as there is viruses left, a new one is generated
// Param: scene - game object used to handle physics objects etc
function overlapVirusesWeapons(scene) {
    scene.physics.add.overlap(weapons, viruses, (weapon, virus) => {
        virus.disableBody(true, true);
        soundFallingVirus.play();
        if (viruses.countActive(true) > 0) {
            createNewVirus();
        }
        createNewVirusesIfNoLeft(scene);
        incrementScoreShown();
        if (isHighScoreReached()) {
            scene.physics.pause();
            createSweatAlert("Your last highScore was reached", "Do you want to continue or stop playing?", "success", null, ["No, stop playing", "Yes please continue to play"]).then(playOrNot => {
                if (playOrNot) {
                    scene.physics.resume();
                }
            });
        }
    });
}

// Imcrements score
function incrementScoreShown() {
    score += 1;
    scoreText.setText(`Score: ${score}`);
}

// Checks if the score has reached highScore
// Returns bool, true if highScore was reached
function isHighScoreReached() {
    return score === highScore;
}
// This function creates a new virus as one is destroyed
function createNewVirus() {
    let xCordinate = (player.x < 400) ? Phaser.Math.Between(400, 800) : Phaser.Math.Between(0, 400);
    let newVirus = viruses.create(xCordinate, 16, "virus");
    newVirus.setVelocity(Phaser.Math.Between(0, 5, 1), Phaser.Math.Between(0, 9, 2));
    newVirus.setCollideWorldBounds(true);
    newVirus.setBounce(Phaser.Math.Between(0.1, 1));
}

// This function controles whath appens then Viruses and the player overlaps (collide)
// Namely: ends the game, saves a highScore and shows the result
// Param: scene - game object used to handle physics objects etc
function overlapVirusesPlayer(scene) {
    scene.physics.add.overlap(player, viruses, (player, virus) => {
        if (virus.body.touching.down) {
            soundGameOver.play();
            scene.physics.pause();
            player.setTint(0xff0000);
            gameOver = true;
            saveScoreHighScoreIfhigherThanLast();
            let messageAboutHighScore = (score > highScore) ? `You died! Your new highScore is: ${score}` : "You died, as you did not surpass your highscore, it was left untouched";
            createSweatAlert("Oh no!", messageAboutHighScore, "info", "Ok, thank you");
        } else {
            disableVirusIfWalkedOver(scene, virus);
        }
    });
}

// This function saves your score as a highscore if it is higher than your old highscore
function saveScoreHighScoreIfhigherThanLast() {
    if (score > highScore) {
        localStorage.setItem("highScoreOnGameOver", score);
        return score;
    } else {
        return highScore;
    }
}

// Creates a sweatAlert
// Param: title - Title of alert
// Param: text - text of alert
// Param: icon - icon of alert
// Param: button - optional, should the button of the alert have other text
// Param: button - okCancelText, text of the cansel button and okButton as string array
// Returns the promice returned by swal (sweat alert)
function createSweatAlert(title, text, icon, buttonTxt = null, okCancelText = null) {
    let options;
    if (!buttonTxt) {
        options = {
            title: title,
            text: text,
            icon: icon
        };
    } else {
        options = {
            title: title,
            text: text,
            icon: icon,
            button: buttonTxt
        };
    }

    if (okCancelText) {
        options = {
            title: title,
            text: text,
            icon: icon,
            buttons: okCancelText
        };
    }

    return swal(options);
}

// Param: scene - game object used to handle physics objects etc
// Param: virus - virus to be disabled
function disableVirusIfWalkedOver(scene, virus) {
    virus.setVelocity(0, 0);
    virus.disableBody(true, true);
    createNewVirusesIfNoLeft(scene);
}

// This function creates new viruses if no viruses left
function createNewVirusesIfNoLeft(scene) {
    if (viruses.countActive(true) === 0) {
        createViruses(scene, numberOfVirusesStart += Math.floor(Phaser.Math.Between(0.1, 0.8) * 10));
    }
}

// This function is run constantly to update certain game specific conditions
function update() {
    setVelocityDependingOnKey();
    weapons.children.iterate((weapon) => {
        if (weapon.active) {
            if (weapon.y < 0) {
                weapon.setActive(false);
            }
        }
    });
}

// This function is responsible to make the player react to keyStroces: changing direction and shooting
function setVelocityDependingOnKey() {
    if (cursors.up.isDown) {
        player.setVelocityY(-130);
    } else if (cursors.left.isDown) {
        player.setVelocityX(-130);
        player.anims.play("left");
    } else if (cursors.down.isDown) {
        player.setVelocityY(130);
    } else if (cursors.right.isDown) {
        player.setVelocityX(130);
        player.anims.play("right");
    } else {
        player.setVelocityX(0);
    }

    if (cursors.space.isDown) {
        let weapon = weapons.get(player.x, player.y);
        if (weapon) {
            weapon.body.setAllowGravity(false);
            weapon.setActive(true);
            weapon.setVisible(true);
            weapon.setVelocityY(-500);
            soundShooting.play();
        }
    }
}