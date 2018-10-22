# Homework 4

For this next assignment, we were tasked with writing a ASP.NET MVC web application. Our goal was to write server-side dynamic web pages, one using ```GET``` -- which requests data from from a specified source, the other using ```POST``` -- which sends data to the server to create or update a resource.

## Links

* Assignment Page can be found [here](http://www.wou.edu/~morses/classes/cs46x/assignments/HW4_1819.html)
* Code repository referencing this work can be found [here](https://github.com/mlarios1/CS460/tree/master/hw4)
* Clone repo link: [https://github.com/mlarios1/CS460.git](https://github.com/mlarios1/CS460.git)

## Step 1: Preparation

Before starting on this assignment, I took the time to learn Razor HTML, understand how ```GET```and ```POST``` methods work and get a handle how to create a web application with Visual Studio. The last part was difficult because while I found some tutorials at [Microsoft](https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/introduction/getting-started) and [TutorialsTeacher.com](http://www.tutorialsteacher.com/mvc/asp.net-mvc-tutorials) on this matter, I didn't fully understand why things work the way they should and which files worked with what until Dr. Scot Morse showed the class some examples and the logic behind it.

## Step 2: Updating the Home Page

Having understood the material explained in the previous step, I felt I had the necessary knowledge and skills to get started on this assignment. To begin, I created a feature branch in Git called ```mileconverter```. Following that, I updated the home page Microsoft auto-generated for me to match what my professor [had](http://www.wou.edu/~morses/classes/cs46x/assignments/images/hw4_home.png).

In ```index.cshtml```, I mostly updated the ```<p>``` and ```<h2>``` tags as well as the buttons as shown below:

```html
@{
    ViewBag.Title = "Home";
}

<div class="jumbotron">
    <h1>CS460 Homework 4</h1>
    <p class="lead">
        A few forms and some simple server-side logic -- learning the basics of GET, POST, query strings, form data and
        handling it all from an ASP.NET MVC 5 application.
    </p>
    <p><a href="http://www.wou.edu/~morses/classes/cs46x/assignments/HW4_1819.html" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>
</div>

<div class="row">
    <div class="col-md-6">
        <h2>Mile to Metric Converter</h2>
        <p>
            Want to know how many millimeters there are in 26.3 miles? This calulator is for you. Uses query strings
            to send form data to the server, which performs the calculation and returns the answer in the requested page.
        </p>
        <p><a class="btn btn-primary" href="MileConverter">Try it out &raquo;</a></p>
    </div>
    <div class="col-md-6">
        <h2>Color Chooser</h2>
        <p>
            Typical online color choosers are way too useful. We wanted something fun and completely useless.
            This form POST's the data to the server.
        </p>
        <p><a class="btn btn-primary" href="/Color/Create">Check it out &raquo;</a></p>
    </div>
</div>
```
  
In addition, I also made a few changes to ```_layout.cshtml```, such as the ```@Html.ActionLink``` links for the navigation menu as well as tweaking the copyright info in the footer.

```html
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>@ViewBag.Title - CS460 HW4</title>
    @Styles.Render("~/Content/css")
    @Scripts.Render("~/bundles/modernizr")
</head>
<body>
    <!--navigation bar-->
    <div class="navbar navbar-inverse navbar-fixed-top">
        <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                @Html.ActionLink("CS460 HW4", "Index", "Home", new { area = "" }, new { @class = "navbar-brand" })
            </div>
            <div class="navbar-collapse collapse">
                <ul class="nav navbar-nav">
                    <!-- nav bar page links-->
                    <li>@Html.ActionLink("Home", "Index", "Home")</li>
                    <li>@Html.ActionLink("Converter", "MileConverter", "Home")</li>
                    <li>@Html.ActionLink("ColorChooser", "Create", "Color")</li>
                </ul>
            </div>
        </div>
    </div>
    <div class="container body-content">
        @RenderBody()
        <hr />
        <footer>
            <!--copyright info-->
            <p>&copy; @DateTime.Now.Year - Manuel Larios</p>
        </footer>
    </div>

    <!--load scripts-->
    @Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/bundles/bootstrap")
    @RenderSection("scripts", required: false)
</body>
</html>
```

## Step 3: Writing MileConverter using ```GET```

After updating the home page, I went on to create ```MileConverter.cshtml```, using labels, a textbox, a set of radio buttons and a "Convert" button which will process the input passed by the user. The idea behind this page is that it would take user input in miles and convert it to metric units, such as millimeters, meters and so forth.

```html
<h2>Convert Miles to Metric</h2>
<form action="/Home/MileConverter" method="get">
    <div class="row">
        <div class="col-sm-4">
            <!-- Input text box-->
            <p>
                <label for="mileinput">Miles</label>
                <br />
                <input type="number" name="mileinput" min="0" />
            </p>
        </div>
        <div class="col-sm-6">
            <!-- Unit of measure radio buttons-->
            <h4>Select a unit</h4>
            <p>
                <input type="radio" name="unitselect" value="millimeters" checked="checked" />
                <label for="unitselect">Millimeters</label>
                <br />
                <input type="radio" name="unitselect" value="centimeters" />
                <label for="unitselect">Centimeters</label>
                <br />
                <input type="radio" name="unitselect" value="meters" />
                <label for="unitselect">Meters</label>
                <br />
                <input type="radio" name="unitselect" value="kilometers" />
                <label for="unitselect">Kilometers</label>
            </p>

            <!-- "Convert button"-->
            <input type="submit" class="btn btn-primary" name="btn" value="Convert" />
        </div>
    </div>
</form>
```

Next, I created an ```ActionResult``` method in ```HomeController.cs``` using ```GET```, which will perform server-side conversion from miles to metric. However, it also presents a problem because any ```ActionResult``` method using ```GET``` will display query strings in the address bar, which would allow users to bypass the input form and type in queries of their choice which may cause an exception to be thrown. To combat this, I've written some server-side validation in nested form which performs a series of checks to determine if it can perform mile-to-metric conversion using the values passed. If all goes well, it performs the conversion by multiplying the ```miles``` variable to a metric unit of their choice and then displays the result. Otherwise, it'll display an error message letting users the conversion was unsuccessful in some way.

```C#
/// <summary>
        /// GET method for MileConverter.cshtml page
        /// </summary>
        /// <returns>view of MileConverter.cshtml</returns>
        [HttpGet]
        public ActionResult MileConverter()
        {
            double miles;
            double metricResult = 0; //where metric conversion values will be stored
            string unit = Request.QueryString["unitselect"];
            ViewBag.Show = false;

            if(Request.QueryString["btn"] == "Convert") //has "Convert" button been pressed?
            {
                if (Double.TryParse(Request.QueryString["mileinput"], out miles)) //can input value be converted to a double?
                {
                    if(miles >= 0) //input value a positive number?
                    {
                        if (unit == "millimeters")
                        {
                            metricResult = miles * 1609000;
                            ViewBag.Show = true;
                        }
                        else if (unit == "centimeters")
                        {
                            metricResult = miles * 160934.4;
                            ViewBag.Show = true;
                        }
                        else if (unit == "meters")
                        {
                            metricResult = miles * 1609.344;
                            ViewBag.Show = true;
                        }
                        else if (unit == "kilometers")
                        {
                            metricResult = miles * 1.609;
                            ViewBag.Show = true;
                        }
                        else //not a valid unit of measure
                        {
                            ViewBag.Display = "Invalid unit type. Please try again.";
                        }
                    }
                    else //if not, let the user know
                    {
                        ViewBag.Display = "Negative values are not allowed. Please try again.";
                    }
                    

                    if (ViewBag.Show) //displays computed output to user
                    {
                        ViewBag.Display = miles + " miles is equal to " + metricResult + " " + unit;
                    }
                }
                else //if not, let the user know
                {
                    ViewBag.Display = "Invalid input. Please try again.";
                }
            }

            return View();
        }
```

Given the results, I'd say the mile converter page was a success. Immediately after, I merged ```mileconverter``` back to ```master``` and worked on the next step.

## Step 4: Writing ColorChooser using ```POST```

Satisfied all was well in the previous step, I went on to work on the color chooser page, which will take two inputs, in hexademical form and return the result of mixing the two colors together and display it to the user. To begin, I created another feature branch called ```colorchooser```. In it, I created a separate controller called ```ColorController.cs``` and created a web page called ```Create.cshtml```, using a mix of HTML and Razor which consisted of nothing more than two labels and textboses and a "Mix Colors" button which will send the input to the server. Because we were also tasked with ensuring the user enters hexadecimal colors, we had to create client, as well as server-side form validation. On the client-side, I wrote a ```@pattern``` attribute that asks the textbox to strictly search for a hex color value pattern and reject everything else as well as making sure it wasn't empty by adding the ```@required``` attribute.

```HTML
@{
    ViewBag.Title = "Color";
}

<h2>Create a New Color</h2>

<p>Please enter colors in HTML hexadecimal format: #AABBCC</p>

@using (Html.BeginForm("Create", "Color", FormMethod.Post))
{
    @Html.Label("firstColor", "First color");
    @Html.TextBox("firstColor", null, htmlAttributes: new { @class = "form-control", @pattern = "#[0-9A-Fa-f]{3,6}", @title = "Ex: #F38FCB or #B3C", @required = true });

    @Html.Label("secondColor", "Second color");
    @Html.TextBox("secondColor", null, htmlAttributes: new { @class = "form-control", @pattern = "#[0-9A-Fa-f]{3,6}", @title = "Ex: #F38FCB or #B3C", @required = true });

    <br />
    <input type="submit" class="btn btn-primary" value="Mix colors" />
}

@{
    if (ViewBag.Show) //displays result to the user
    {
        <br />
        <br />
        <div class="row">
            <div class="col-sm-1" style="@ViewBag.Square"></div>
            <div class="col-sm-1" style="width: 50px; height: 100px;"><h1>+</h1></div>
            <div class="col-sm-1" style="@ViewBag.Square2"></div>
            <div class="col-sm-1" style="width: 50px; height: 100px;"><h1>=</h1></div>
            <div class="col-sm-1" style="@ViewBag.Square3"></div>
            <div class="col-sm-7"></div> @*Intentionally left blank*@
        </div>
    }
    else //displays error message to the user
    {
        <br />
        <br />
        <h3 style="color: #f44242">@ViewBag.ErrorMessage</h3>
    }
}
```

Back at ```ColorController.cs```, on the server-side, I created an ```ActionResult``` method called ```Create()``` with two string parameters which should read in inputs from the Create.cshtml webpage, all using POST which doesn't display query strings. In it, I wrote a ```Regex``` variable, which consists of a similar pattern I wrote for client-side validation, only in regular expression form. With that, I used it to check the inputs passed through the parameter to see if they match the hex color pattern. At the same time, it also makes sure ```null``` values are not being passed, so that saves me from having to write additional code. If all goes well, it translates the inputs, both ```string``` values, into ```Color``` objects in order to add red, green and blue values to from a new color. 

Last, but not least, I had to come up with a way to display the colors back to the webpage. Using a suggestion from some peers I worked with, I wrote a line of code that should attach to the ```style``` attribute, which I then created back at ```Create.cshtml``` that has a height, width, border and the background of the color in that ```<div>``` element.

```
/// <summary>
        /// POST method for the create.cshtml page
        /// </summary>
        /// <param name="firstColor">hex pattern of first color</param>
        /// <param name="secondColor">hex pattern of second color</param>
        /// <returns>view of create.cshtml page with results of user's input</returns>
        [HttpPost]
        public ActionResult Create(string firstColor, string secondColor)
        {
            Regex hexColorPattern = new Regex("^#[a-fA-F0-9]{3,6}$"); //hex color pattern to be used for validation
            ViewBag.Show = false;

            if (hexColorPattern.IsMatch(firstColor) && hexColorPattern.IsMatch(secondColor)) //are inputs of hex color values? (Server-side input validation)
            {
                Color color1 = ColorTranslator.FromHtml(firstColor);
                Color color2 = ColorTranslator.FromHtml(secondColor);
                string finalColor; //stores hex color value after mixing colors

                int red; //stores "mixed" red values
                int blue; //stores "mixed" blue values
                int green; //stores "mixed" green values

                /**
                 * Performs color addition for each color value. If a color's combined
                 * value is more than 255, cap it at 255. Otherwise, store the result.
                 */

                //mixing red values
                if(color1.R + color2.R > 255)
                {
                    red = 255;
                }
                else
                {
                    red = color1.R + color2.R;
                }

                //mixing green values
                if(color1.G + color2.G > 255)
                {
                    green = 255;
                }
                else
                {
                    green = color1.G + color2.G;
                }

                //mixing blue values
                if (color1.B + color2.B > 255)
                {
                    blue = 255;
                }
                else
                {
                    blue = color1.B + color2.B;
                }

                finalColor = ColorTranslator.ToHtml(Color.FromArgb(255, red, green, blue)); //converts computed RGB values to hex color value

                //style attributes for displaying colors
                ViewBag.Square = "width: 70px; height: 70px; border: 1px #000 solid; border-radius: 10px; background: " + firstColor + ";";
                ViewBag.Square2 = "width: 70px; height: 70px; border: 1px #000 solid; border-radius: 10px; background: " + secondColor + ";";
                ViewBag.Square3 = "width: 70px; height: 70px; border: 1px #000 solid; border-radius: 10px; background: " + finalColor + ";";

                ViewBag.Show = true; //displays result to the user
            }
            else //if not, display error message to user
            {
                ViewBag.ErrorMessage = "Invalid input(s). Try using hex values like #F38FCB or #B3C.";
            }

            return View();
        }
```

After making sure the color chooser worked properly, I merged ```colorchooser``` back to ```master```, added comments to code where I thought needed some explanation and pushed it to my remote Git repository at GitHub.
