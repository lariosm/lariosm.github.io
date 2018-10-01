# Homework 1

For this first assignment, we were tasked with creating web pages that conforms to HTML5 standards and styling them with CSS and Bootstrap. In addition, we were required to submit our work to our Git repository using only the command line.

## Links

* Assignment page can be found [here](http://www.wou.edu/~morses/classes/cs46x/assignments/HW1.html)
* Demo can be found [here](https://mlarios1.github.io/CS460/hw1/)
* Code repository referencing this work can be found [here](https://github.com/mlarios1/CS460/tree/master/hw1)
* Clone repo link: https://github.com/mlarios1/CS460.git

## Step 1: Learning the basics

Before starting this assignment, I already had some experience working with HTML and CSS, but had never used Git and Bootstrap before. From Monday through mid-Thursday, I spent some time learning CSS (I was a bit rusty), Git and Bootstrap in that order through various sources such as [Codecademy](https://www.codecademy.com/), [W3Schools](https://www.w3schools.com) and YouTube videos.

## Step 2: Setting up Git

After installing Git on my Windows 10 machine, I started by creating a directory called ```CS460``` in my ```C:``` drive, although it did not occur to me to create the rest of the files (.html and .css files) via command line since that was something I would typically reserve for linux.

Having done that, I started off by customizing my username and e-mail as I would want it to appear in my commits, followed by the creation of a local Git repository and finally adding a remote repository (as instructed by GitHub) which I would ```push``` my work to.

```
git config --global user.name "Manuel Larios"
git config --global user.email "mlarios12@wou.edu"
echo "# testrepo" >> README.md
git init
git add README.md
git commit -m "first commit"
git remote add origin https://github.com/mlarios1/CS460.git
git push -u origin master
```

## Step 2: Creating HTML pages and CSS

For this part of the assignment, I started out by drawing out some sketches since I couldn't visualize the pages i wanted to build. After spending an hour or two coming up with a design I know I would have to commit to, I've decided to start coding the HTML part by creating a basic structure of how I want each of my pages (i.e. ```index.html```, ```gallery.html```, ```bucketlist.html```, ```about.html```) to look like before stuffing any more content. (NOTE: I would occasionally come back and edit this part to match my CSS rules, just so you know.)

```
<!DOCTYPE html>
<html lang="en">
  <head>
    <title>Manuel's Place</title>
    <!--Metadata tags-->
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <!--Bootstrap CSS-->
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css">
    <!--My CSS file-->
    <link rel="stylesheet" href="css/styles.css">
  </head>

  <body>
    <div class="container-fullwidth">
      <div class="heading">
        <h1>Manuel's Place</h1>
      </div>
      <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
        <ul class="navbar-nav">
          <li class="active nav-item">
            <a class="nav-link" href="index.html">Home</a>
          </li>
          <li class="nav-item">
            <a class="nav-link" href="gallery.html">Gallery</a>
          </li>
          <li class="nav-item">
            <a class="nav-link" href="bucketlist.html">Bucket list</a>
          </li>
          <li class="nav-item">
            <a class="nav-link" href="about.html">About</a>
          </li>
        </ul>
      </nav>
    </div>
    <div class="container">
      <div class="content">
        <h2>Hey there!</h2>
        <p>
          Welcome to my website! Well, at least the directory where this is
          hosted at. I haven't thought about what I'm going to put up in my home
          page. While you're here, why not have a look around at my other web
          pages, such as my photo gallery, my bucket list and my about page.
        </p>
      </div>
    </div>
  </body>
</html>
```

After completing that, I went on to create a CSS file that would complement the HTML portion with colors, image resizing and everything in between.

```
body {
  background-color: #3884ff;
}

h1 {
  font-weight: bold;
}

nav li {
  padding-left: 10px;
  padding-right: 10px;
}

table {
    font-family: arial, sans-serif;
    border-collapse: collapse;
    width: 100%;
}

td, th {
    text-align: left;
    padding: 10px;
}

tr:nth-child(even) {
    background-color: #dddddd;
}

img {
  width: auto;
  height: auto;
  max-width: 100%;
  max-height: 100%;
}

.container-fullwidth {
  background-color: #fffa00;
}

.content {
  border-radius: 10px;
  background-color: #ffffff;
  margin-top: 25px;
  margin-bottom: 25px;
  padding: 20px;
}
```

I will admit, however, that I spent nearly the entire day trying to stretch and align the ```container``` that houses the ```h1``` and the ```navbar```. While I did eventually stretch those elements, I couldn't align them in a way so that it offsets from the left side by about 70-80 pixels before giving up.

## Step 3: Moving the project, adding images and the final product

As I was working on this assignment, I later realized that if I wanted to add files from Homework 2, 3 and so forth and not conflict with each other when I stage them, I would need to move my Homework 1 files to a subdirectory, which I later called ```hw1```.

Soon after that, I've decided to create a subfolder called ```imgs``` within my ```hw1``` subdirectory to store any image-related files rather than mix them with HTML files.

In the end, through trial and error, I've managed to create a decent-looking website within the given timeframe. It probably wasn't how I envisioned it, but several elements from my sketches were there and I'm happy with it so far.


![gallery page](https://github.com/mlarios1/mlarios1.github.io/blob/master/CS460/hw1.png)
