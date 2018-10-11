# Homework 2

For this second assignment, we were tasked with creating a single web page using HTML, CSS (with Bootstrap), JavaScript and jQuery. (Mostly the last two, since this is what this assignment is about.)

## Links

* Assignment page can be found [here](http://www.wou.edu/~morses/classes/cs46x/assignments/HW2.html).
* Demo can be found [here](https://mlarios1.github.io/CS460/hw2/).
* Code repository referencing this work can be found [here](https://github.com/mlarios1/CS460/tree/master/hw2).
* Clone repo link: [https://github.com/mlarios1/CS460.git](https://github.com/mlarios1/CS460.git)

## Step 1: Preparation

Before diving into this assignment, I took some time to brush up and re-familiarize myself with JavaScript since it's been about two years since I've last written code with it. Immediately after, I went on to take an introductory course to jQuery at Codecademy and study most of the jQuery functions from a jQuery "cheat sheet" reference webpage compiled by Oscar Otero [here](https://oscarotero.com/jquery/).

## Step 2: Designing a web page

At this point, I haven't fully mastered JavaScript (and jQuery), but I felt I had the necessary skills to complete the assignment. That said, I've decided to reuse the HTML and CSS files I've created for Homework 1, not only because it satisfies most of the Homework 2 requirements, but because I figured I'd need the time to work on the JavaScript/jQuery component.

Keeping in mind I needed to incorporate a few form elements and have it generate a list or table element, I've decided, after a few days, to create some sort of change dispenser, simliar to what you would find at an ATM, or more likely, at a self-checkout kiosk. I had the following design in mind:

![gallery page](https://mlarios1.github.io/mlarios1.github.io/CS460/HW2/hw2_design.jpg)

Unfortuately, I had to water down the design several times bceause not only was it going to get complicated, but it was something I probably wouldn't be able to finish over the course of the three days I had left to work with.

## Step 3: Coding the assignment

Using the HTML file I copied from Homework 1, I've added a few elements, like the text field, which only takes in number values, doesn't allow for negative values, has a placeholder and increments by 1.
```html
<div class="col-sm-4">
  <p>Step 1: Enter an amount</p>
  <label>$</label>
  <input type="number" id="input-amt" min="0"  placeholder="0" step="1">
</div>
```

Checkboxes, all which would be ticked by default upon page load.
```html
<div class="col-sm-4">
  <p>Step 2: How do you want your change?</p>
  <!-- Change type checkboxes -->
  <div class="row">
    <div class="col-sm-12">
      <input type="checkbox" name="hundred" checked>
      <label f>$100 bills</label>
      <br>
      <input type="checkbox" name="fifty" checked>
      <label>$50 bills</label>
      <br>
      <input type="checkbox" name="twenty" checked>
      <label>$20 bills</label>
      <br>
      <input type="checkbox" name="ten" checked>
      <label>$10 bills</label>
      <br>
      <input type="checkbox" name="five" checked>
      <label>$5 bills</label>
      <br>
      <input type="checkbox" name="one" checked>
      <label>$1 bills</label>
    </div>
  </div>
  <!-- /Change type checkboxes -->
</div>
```

Last, but not least, the "Calculate" button, which would trigger a series of functions in my JavaScript file.
```html
<div class="col-sm-4">
  <p>Step 3: Calculate your change</p>
  <button id="submit">Calculate</button>
</div>
```

## Step 4: Coding the assignment (JavaScript/jQuery)

After modifying the HTML document in preparation for "hooking" with my JavaScript file, I started by creating variables at the top like so, followed by sets of basic functions (No HTML/JS "hookups" yet).
```javascript
/* Amount of money to "insert" */
var credit = 0;

/* Number of each bill dispensed */
var hundreds = 0;
var fifties = 0;
var twenties = 0;
var tens = 0;
var fives = 0;
var ones = 0;

function dispenseChange() {
  //code...
}

function reset() {
  //code...
}
```

One of the core basic functions I've written, ```dispenseChange()```, ensures that change is being dispensed properly. Eventually, it would also use jQuery functions to check if a checkbox for, say $50 bills, is checked so it knows whether or not to dispense $50 bills to the user.
```javascript
function dispenseChange() {
  while(credit != 0) {
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
    else {
      alert("It's not possible to properly dispense your requested change with the amount you entered and/or the checkboxes you ticked. Click the \"Start over\" button to try again.");
      credit = 0; //forces credit to "0" to stop infinite looping
    }
  }
}
```

After that was up and running, I created a function that would append HTML code to the HTML document which would generate a table, showing the amount of each bill that was dispensed, followed by a button that would delete the table and reset the program.
```javascript
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
    <button id='clear-data' onclick=\"reset(); $('.print-table').remove();\">Start over</button>\
    <label>-- Clears table and resets the program</label>\
  </div>");
```

Finally, after all that was set up, I created an event listener so that when the "Calculate" button is pressed, it would perform various tasks, including making sure valid values were being passed, dispensing bills, printing a table and so forth.
```javascript
$('#submit').on('click', function() { //when user clicks "Calculate" button
    $('#submit').prop('disabled', true); //prevents creation of multiple tables
    credit = financial(document.getElementById('input-amt').value);
    if(initialCheck() === true) { //if it passes, then execute block
      $('#input-amt').val(credit); //Sets textbox value in step 1. Mostly if user enters floating-point value (i.e. 0.001)
      dispenseChange();
      printTable();
    }
  });
```
Because we were required to branch off from our master branch, I've created a second branch called ```javascript``` where I would commit all my JavaScript work, while committing all my HTML work back at the ```master``` branch. Eventually, I merged them back under the master branch and confirmed it was still working.

![Web page demo](https://mlarios1.github.io/mlarios1.github.io/CS460/HW2/hw2.png)
