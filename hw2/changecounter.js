/* Amount of money to "insert" */
var credit = 0;

/* Number of each bill dispensed */
var hundreds = 0;
var fifties = 0;
var twenties = 0;
var tens = 0;
var fives = 0;
var ones = 0;

function main() {
  $('#submit').on('click', function() {
    credit = financial(document.getElementById('input-amt').value);
    dispenseChange();
    printChange();
    printTable();
  });
}

/* Rounds any decimal values to nearest whole number */
function financial(x) {
  return Number.parseFloat(x).toFixed(0);
}

/* Resets number of bills dispensed */
function reset() {
  credit = 0;
  hundreds = 0;
  fifties = 0;
  twenties = 0;
  tens = 0;
  fives = 0;
  ones = 0;
}

function dispenseChange() {
  while(credit != 0) {
    console.log("looping");
    if($('input[type=checkbox][name="hundred"]').is(':checked') && credit % 100 === 0) {
      credit -= 100;
      hundreds++;
    }
    else if($('input[type=checkbox][name="fifty"]').is(':checked') && credit % 50 === 0) {
      credit -= 50;
      fifties++;
    }
    else if($('input[type=checkbox][name="twenty"]').is(':checked') && credit % 20 === 0) {
      credit -= 20;
      twenties++;
    }
    else if($('input[type=checkbox][name="ten"]').is(':checked') && credit % 10 === 0) {
      credit -= 10;
      tens++;
    }
    else if($('input[type=checkbox][name="five"]').is(':checked') && credit % 5 === 0) {
      credit -= 5;
      fives++;
    }
    else if($('input[type=checkbox][name="one"]').is(':checked') && credit % 1 === 0) {
      credit--;
      ones++;
    }
  }
}

function printChange() {
  console.log("$100s: " + hundreds);
  console.log("$50s: " + fifties);
  console.log("$20s: " + twenties);
  console.log("$10s: " + tens);
  console.log("$5s: " + fives);
  console.log("$1s: " + ones);
}

function printTable() {
  $('#table-change').append("<div class='print-table'>\
    <h4>Your change is as follows:</h4>\
    <table>\
      <tr>\
        <th># of $100 Bills</th>\
        <th># of $50 Bills</th>\
        <th># of $20 Bills</th>\
        <th># of $10 Bills</th>\
        <th># of $5 Bills</th>\
        <th># of $1 Bills</th>\
      </tr>\
      <tr>\
        <td>" + hundreds + "</td>\
        <td>" + fifties + "</td>\
        <td>" + twenties + "</td>\
        <td>" + tens + "</td>\
        <td>" + fives + "</td>\
        <td>" + ones + "</td>\
      </tr>\
    </table>\
    <br>\
    <button id='clear-data'>Start over</button>\
    <label>-- Clears table and resets the program</label>\
  </div>");
}

$(document).ready(main);
