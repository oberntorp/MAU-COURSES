import * as datasets from './dataset';

console.log(datasets);

// Uppgift A

/*
* Make all names uppercase
*/
function UpperFirst()
{
    return datasets.names.map(x => x.toUpperCase());
}

console.log("Resultat uppgift A:", UpperFirst());

// Uppgift B

/*
* Add area to all triangles
*/
function AddArea()
{
    return datasets.triangles.map(x => {
        let objectWithArea = 
        {
            width: x.width,
            height: x.height,
            area: x.height * x.width
        };

        return objectWithArea;
    });
}

console.log("Resultat uppgift B:", AddArea());

// Uppgift C

/*
* Filter movies released between 1990 and 2000
*/
function FilterMoviesReleased1990To2000()
{
    return datasets.movies.filter(x => x.year >= 1990 && x.year <= 2000);
}

console.log("Resultat uppgift C:", FilterMoviesReleased1990To2000());

// Uppgift D

/*
* Get all titles of movies released 2000 and after
*
*
*/
function FilterMoviesReleased2000AndOnWardsWithTitle()
{
    return datasets.movies.filter(x => x.year >= 2000).map(y => y.title);
}

console.log("Resultat uppgift D:", FilterMoviesReleased2000AndOnWardsWithTitle());

// Uppgift E

/*
* Get all tasks with high priority that are done
*/
function GetTasksDoneWithHighPrio()
{
    return datasets.todoList.tasks.filter(x => x.complete === true && x.priority === "High").map(y => y.id);
}

console.log("Resultat uppgift E:", GetTasksDoneWithHighPrio());

// Uppgift F

/*
* Concatenate id and title for tasks that are not of low priority and that are still in progress
*/
function GetTasksNotDoneNotHighPrio()
{
    return datasets.todoList.tasks.filter(x => x.complete === false && x.priority !== "Low").map(y => y.title + " " + y.id);
}

console.log("Resultat uppgift E:", GetTasksNotDoneNotHighPrio());

window.upper = UpperFirst();