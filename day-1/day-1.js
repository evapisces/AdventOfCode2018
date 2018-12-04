var fs = require('fs');

var testPuzzleFile = "test-input-2.txt";
var puzzleFile = "puzzle-input.txt";
var puzzleInput = [];

var content = fs.readFileSync(puzzleFile, 'utf8');

var puzzleInput = content.split('\n');

console.log("Part A: Frequency = " + puzzlePartA(puzzleInput));
console.log("Part B: First double frequency = " + puzzlePartB(puzzleInput));

function puzzlePartA(puzzleList) {
    console.log("Advent of Code 2018 - Day 1, Part A");

    var frequency = 0;

    puzzleList.forEach(element => {
        var sign = element.substring(0, 1);
        var number = parseInt(element.substring(1, element.length));
        switch (sign) {
            case '+':
                frequency += number;
                break;
            case '-':
                frequency -= number;
                break;
            default:
                break;
        }
    });

    return frequency;
}

function puzzlePartB(puzzleList) {
    console.log("Advent of Code 2018 - Day 1, Part B");

    var frequencies = new Array();
    var frequency = 0;

    var i = 0;
    var proceed = true;

    do {
        var sign = puzzleList[i].substring(0, 1);

        var number = parseInt(puzzleList[i].substring(1, puzzleList[i].length));
        switch (sign) {
            case '+':
                frequency += number;
                break;
            case '-':
                frequency -= number;
                break;
            default:
                break;
        }

        if (!frequencies.includes(frequency)) {
            frequencies.push(frequency);
        } else {
            proceed = false;
        }

        if (i == puzzleList.length - 1) {
            i = 0;
        } else {
            i++;
        }

    } while (proceed);

    return frequency;
}