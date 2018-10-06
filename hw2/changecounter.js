var credit = 0;

var hundreds = 0;
var fifties = 0;
var twenties = 0;
var tens = 0;
var fives = 0;
var ones = 0;

function main() {
  /* Nothing here yet */
}

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
    if(credit % 100 === 0) {
      credit -= 100;
      hundreds++;
    }
    else if(credit % 50 === 0) {
      credit -= 50;
      fifties++;
    }
    else if(credit % 20 === 0) {
      credit -= 20;
      twenties++;
    }
    else if(credit % 10 === 0) {
      credit -= 10;
      tens++;
    }
    else if(credit % 5 === 0) {
      credit -= 5;
      fives++;
    }
    else if(credit % 1 === 0) {
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
  /* Nothing here yet */
}

$(document).ready(main);
