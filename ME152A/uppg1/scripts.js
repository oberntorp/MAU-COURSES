import dataset from "./dataset.js";

function Database(dataset) {
  this.movies = dataset;
}

/*
 * Returnerar en array med alla filmer
 * Parametrar: inga
 * Returneras:
 * En array med alla filmer enligt objektet:
 * title, year, runtime, imdbID, genre, actos
 */
Database.prototype.getAll = function () {
  return this.movies.map(z => {
    let returnObject = {};
    returnObject.title = z.Title;
    returnObject.year = z.Year;
    returnObject.runtime = z.Runtime;
    returnObject.imdbID = z.imdbRating;
    returnObject.genre = z.Genre;
    returnObject.actors = z.Actors;

    return returnObject;
  });
};

/*
 * Returnerar en array med alla filmer
 * Parametrar: inga
 * Returneras:
 * En array med alla filmer enligt objektet:
 * title, year, plot, genre, actors
 */
Database.prototype.getAllMovies = function () {
  return this.movies.filter(x => x.Type == "movie").map(z => {
    let returnObject = {};
    returnObject.title = z.Title;
    returnObject.year = z.Year;
    returnObject.Plot = z.Runtime;
    returnObject.genre = z.Genre;
    returnObject.actors = z.Actors;

    return returnObject;
  });
};

/*
 * Returnerar en array med alla serier
 * Parametrar: inga
 * Returneras:
 * En array med alla serier enligt objektet:
 * title, year, plot, seasons, genre, actors
 */
Database.prototype.getAllShows = function () {
  return this.movies.filter(x => x.Type == "series").map(y => {
    let returnObject = {};
    returnObject.title = y.Title;
    returnObject.year = y.Year;
    returnObject.Plot = y.Runtime;
    returnObject.seasons = y.totalSeasons;
    returnObject.genre = y.Genre;
    returnObject.actors = y.Actors;

    return returnObject;
  });
};

/*
 * Returnerar en array med alla filmer filtrerade på Releasedate
 * Parametrar: fromYear, toYear
 * Returneras:
 * En array med alla filmer som matchar releaseDate enligt objektet:
 * title, year, imdbID
 */
Database.prototype.filterByRelease = function (fromYear, toYear) {
  return this.movies.map(x => {
    // As releasedate is written dd mm yyyy I want to get the year
    let yearReleased = Number(x.Released.split(" ")[2]);

    let returnObject = {};
    returnObject.title = x.Title;
    returnObject.year = x.Year;
    returnObject.imdbID = x.imdbID;
    returnObject.release = yearReleased;

    return returnObject;
  }).filter(y => (y.release >= Number(fromYear)) && (y.release <= Number(toYear))).map(y => {
    let returnObject = {};
    returnObject.title = y.title;
    returnObject.year = y.year;
    returnObject.imdbID = y.imdbID;
    returnObject.release = y.release;

    return returnObject;
  });
};

/*
 * Returnerar en array med alla filmer
 * Parametrar: rating (konverteras om till number vid jämförelse)
 * Returneras:
 * En array med alla filmer som matchar IMDBRating enligt objektet:
 * title, imdbRating, imdbVotes
 */
Database.prototype.filterByIMDBRating = function (rating) {
  return this.movies.filter(x => x.imdbRating >= Number(rating)).map(x => {
    let returnObject = {};
    returnObject.title = x.Title;
    returnObject.imdbRating = x.imdbRating;
    returnObject.imdbVotes = x.imdbVotes;

    return returnObject;
  });
};

/*
 * Returnerar en array med alla filmer
 * Denna metod använder sig av hjälpmetoder för att hntera fall där runTime är olika formaterad ("200min", "200 min" och 200)
 * Parametrar: runTime
 * Returneras:
 * En array med alla filmer som matchar Runtime enligt objektet:
 * title, runtime, imdbID
 */
Database.prototype.filterByRuntime = function (runTime) {
  runTime = addSpaceIfStringHasNone(runTime);
  const runtimeInMinutes = extractTimeAsNumberIfString(runTime);

  return this.movies.map(x => {
    let returnObject = {};
    returnObject.title = x.Title;
    returnObject.imdbRating = x.Runtime;
    returnObject.imdbID = x.imdbID;
    returnObject.runtimeNumberPart = x.Runtime.split(" ")[0];
    returnObject.runtimeAllParts = x.Runtime;

    return returnObject;
  }).filter(x => x.runtimeNumberPart >= runtimeInMinutes).map(y => {
    let returnObject = {};
    returnObject.title = y.Title;
    returnObject.runtime = y.runtimeAllParts;
    returnObject.imdbID = y.imdbID;

    return returnObject;
  });
};

/*
 * Hjälpmetod till filterByRuntime
 * För att underlätta för extractTimeAsNumberIfString
 * Parametrar: runTime
 * Returneras:
 * runTime med mellanslag om strängen inte har det
 */
function addSpaceIfStringHasNone(runTime) {
  let number = [];
  let string = "";
  if (isNaN(runTime) && (runTime.indexOf(" ") === -1)) {
    for (let i = 0; i < runTime.length; i++) {
      if (!isNaN(runTime[i])) {
        number.push(runTime[i]);
      } else {
        string += runTime[i];
      }
    }
    return `${number.join("")} ${string}`;
  } else {
    return runTime;
  }
}

/*
 * Hjälpmetod till filterByRuntime
 * Parametrar: runTime
 * Returneras:
 * runTimes nummerdel om runTime är en sträng, om siffra, returneras denna
 */
function extractTimeAsNumberIfString(runTime) {
  return isNaN(runTime) ? Number(runTime.split(" ")[0]) : runTime;
}

/*
 * Returnerar en array med alla filmer
 * Parametrar: genre
 * FÖr att vara oberoende av hur användaren anger geren (versaler, germener eller blandat)
 * Så körs toLowerCase på Genre i filterfunktionen
 * En array med alla filmer som matchar genre enligt objektet:
 * title, genre, imdbID
 */
Database.prototype.filterByGenre = function (genre) {
  return this.movies.filter(x => x.Genre.toLowerCase().indexOf(genre.toLowerCase()) >= 0).map(x => {
    let returnObject = {};
    returnObject.title = x.Title;
    returnObject.genre = x.Genre;
    returnObject.imdbID = x.imdbID;

    return returnObject;
  });
};

// WHen working with a module, to be able to access anything, that to be accessed needs to be stored in the window object
window.database = new Database(dataset);

console.log(dataset);