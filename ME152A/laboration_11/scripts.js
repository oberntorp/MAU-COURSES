$(document).ready(function () {
    let questions = [];
    let answers = [];
    let currentQuestion = 0;
    let score = 0;
    let questionAnswered = false;

    $.getJSON("http://jservice.io/api/category", {
        id: 253
    }, function (data) {
        $.each(data.clues, function (key, value) {
            const {
                answer,
                question
            } = value;
            if (question != "") {
                answers.push(answer);
                if (questions.length <= 10) {
                    questions.push({
                        q: question,
                        a: answer
                    });
                }
            }
        });

        let questionsDiv = $("#main #questions");

        $("#questionNumber").find("span").text(currentQuestion + 1);
        $(questionsDiv).append(`<div class="question">${questions[0].q}</div>`);
        $(questionsDiv).append(`<div id="answers">${GetAnswersInnerHtml(answers, 0)}</div>`);

        // Get innerHtml of answer lternatives
        // Param: Answers - get the answer alternatives
        function GetAnswersInnerHtml(answers, indexCurrentQuestion) {
            let [answer1Index, answer2Index, answer3Index] = GetIndexDistinctAnswers(answers, indexCurrentQuestion);
            return `<span>a: ${answers[answer1Index]}</span> <span>b: ${answers[answer2Index]}</span> <span>c: ${answers[answer3Index]}</span>`;
        }

        $(".question").fadeIn("200", () => $(".answers").fadeIn("350"));

        // Click next question
        // Gets id of the new question
        // Checks that the new question is within bounds, if not disable button and display result
        $("#nextQ").on("click", function () {
            $("#nextQ + p").remove();
            if (questionAnswered) {
                let idNewQuestion = ++currentQuestion;
                questionAnswered = false;
                if (questions.length > idNewQuestion) {
                    $("#questionNumber").find("span").text(idNewQuestion + 1);

                    $(".question").text(questions[idNewQuestion].q).hide().fadeIn("200", () => $("#answers").html(GetAnswersInnerHtml(answers, idNewQuestion)).hide().fadeIn(350));
                } else {
                    $("#nextQ").attr("disabled", "");
                    $("#nextQ").after(`<p style='color: green'>That was all questions, your score ${score}</p>`);
                }
            } else {
                $("#nextQ").after(`<p style='color: red'>Please answer the question</p>`);
            }
        });
    });

    // Deppenting on category, the questions and its answers may recurr, thats why I only want distinct answers
    function GetIndexDistinctAnswers(answers, indexOfAnswer) {
        let indexOfAnswersToGet = [indexOfAnswer, (indexOfAnswer + 3), (indexOfAnswer + 5)];
        let arrayOfDistinctIndexes = [];

        arrayOfDistinctIndexes.push(answers[indexOfAnswer]);

        indexOfAnswersToGet.map(x => {
            if (!arrayOfDistinctIndexes.includes(answers[x]))
                arrayOfDistinctIndexes.push(answers[x]);
            else
                arrayOfDistinctIndexes.push(answers[++x]);
        });


        return arrayOfDistinctIndexes.map((x, i) => arrayOfDistinctIndexes[i] = answers.indexOf(arrayOfDistinctIndexes[i])).sort();
    }

    // Click handler for answers, as answers is not in dom from start, we need to call it on main and set it to stop an #answers span
    $("#main").on("click", "#answers span", function () {
        let answerClicked = $(this).text().split(":")[1].trim();
        questionAnswered = true;
        if (answerClicked === questions[currentQuestion].a) {
            score++;
            $("#score").find("span").text(score);

            $(this).append('<i class="fas fa-check" style="color: darkgreen; margin-left: 5px;"></i>');
        } else {
            $(this).append('<i class="fas fa-times" style="color: darkred; margin-left: 5px;"></i>');
        }
    });
});