# Homework 7

For our next assignment, we were tasked with creating a responsive, single-page application using an existing API. In this case, we worked with GIPHY, given its low risk and simplicity, especially since we're working with an external API for the very first time.

## Links

* Assignment page can be found [here](http://www.wou.edu/~morses/classes/cs46x/assignments/HW7_1819.html)
* Code repository referencing this work can be found [here](https://github.com/lariosm/lariosm.github.io/tree/master/hw7)
* Clone repo link: [https://github.com/lariosm/lariosm.github.io.git](https://github.com/lariosm/lariosm.github.io.git)
* Video demo link: [https://youtu.be/LYGLfIfKgsE](https://youtu.be/LYGLfIfKgsE)

## Step 1: Preparation

Before diving in to this assignment, I took some time to educate myself with AJAX and JSON syntax as well as play around with GIPHY's API Explorer to get a sense how requests are sent and how responses are received.

## Step 2: Creating the View

To begin this assignment, I started out by creating a view that has an ```<h1>``` heading and an input box which would later be used to perform JavaScript functions in order to display a mix of plain text and ```.gif```s below.

Layout.cshtml:
```HTML
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Internet Language Translator</title>
    <link href="~/Content/Site.css" rel="stylesheet" type="text/css" />
    <link href="~/Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="~/Scripts/modernizr-2.8.3.js"></script>
</head>
<body>
    <div class="container body-content">
        @RenderBody()
    </div>

    <script src="~/Scripts/jquery-3.3.1.min.js"></script>
    <script src="~/Scripts/bootstrap.min.js"></script>
    @RenderSection("GiphyScript", required: false)
</body>
</html>
```

Index.cshtml:
```HTML
<h1>Internet Language Translator</h1>

@*Input box*@
<form action="/Home/Index" method="get">
    <div class="row">
        <div class="col-sm-12">
            <input name="text" id="textbox" placeholder="Start typing in your message here..." />
        </div>
    </div>
</form>

<br />
<br />

@*Displays plain text and GIF images*@
<div class="row">
    <div id="live-display">
        <!--Text and GIF stickers will appear as the user types-->
    </div>
</div>

@section GiphyScript
{
    <script src="~/Scripts/giphy.js" type="text/javascript"></script>
}
```
[![Simple view](https://lariosm.github.io/lariosm.github.io/Logs/HW7/view.PNG)](https://lariosm.github.io/lariosm.github.io/Logs/HW7/view.PNG)
  
## Step 3: Creating and Programming Controllers

Next, I created two controllers, one called ```HomeController.cs``` and another called ```TranslateController.cs```. ```HomeController``` loads the view mentioned in the previous step while ```TranslateController``` sends a GET request to GIPHY. How that works is when a user types in a word in the text box, a JavaScript file will process the input (more on that later), then it calls ```TranslatorController``` using AJAX with the word to look up on GIPHY's servers (which uses my API key to help perform the search) and returns the response in JSON form which the JavaScript file will use to display an image.

HomeController.cs:
```C#
public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
```

TranslateController.cs:
```C#
public class TranslateController : Controller
    {
        /// <summary>
        /// Sends request to GIPHY and returns it in JSON form
        /// </summary>
        /// <param name="word">Word to look up</param>
        /// <returns>Returns GIPHY response in JSON form</returns>
        public JsonResult TranslateGIF(string word)
        {
            //URI to contact GIPHY's servers
            string uri = "https://api.giphy.com/v1/stickers/translate?api_key=" +
                         System.Web.Configuration.WebConfigurationManager.AppSettings["GiphyKey"] +
                         "&s=" + word;

            //Create a web request
            WebRequest dataRequest = WebRequest.Create(uri);

            //Get the (JSON) data 
            Stream dataStream = dataRequest.GetResponse().GetResponseStream();

            //Parse the received (JSON) data
            var parsedData =  new System.Web.Script.Serialization.JavaScriptSerializer()
                                  .DeserializeObject(new StreamReader(dataStream)
                                  .ReadToEnd());

            //return the (JSON) data
            return Json(parsedData, JsonRequestBehavior.AllowGet);
        }
    }
```

## Step 4: Creating a Custom Route

Once the controllers were set up, we were asked to create a custom route (with an "API feel") to handle AJAX requests. To accomplish this, I went over to ```RouteConfig.cs``` and added a ```MapRoute()``` method (in addition to the default route) that the AJAX method in the JavaScript file will use to call ```TranslateController.cs``` to make requests.

```C#
public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "GiphyAPI",
                url: "Giphy/Image/{word}",
                defaults: new { Controller = "Translate", action = "TranslateGIF", word = UrlParameter.Optional}

                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
```

## Step 5: Creating a Dynamic View

Having completed the previous steps above, I want to create a dynamic view that displays a mix of plain text and ```.gif``` images below the text box, but how? Using JavaScript, of course. To accomplish this, I wrote up a ```main()``` function which listens for a spacebar press. If the spacebar is pressed, it retrieves the text typed in the textbox thus far, converts it into an array (especially if there are multiple words in the textbox), then looks at the last word typed and converts it to an all-lowercase string in order to check against an array of "boring words". If the word typed is a "boring word", then it'll display to the browser as plain text. If not, it makes a call to the ```giphyRequest()``` function, passing in the word.

Array of "boring words":
```JavaScript
var boringWords = ["i", "in", "a", "but", "going", "i'm", "my", ",", ".", "",
                   "for", "not", "of", "to", "from", "make", "just", "know",
                   "other", "this", "than", "then", "as", "he", "he's", "his",
                   "him", "she", "she's", "her", "so", "we", "is", "be", "see",
                   "you", "later", "the", "i've", "isn't", "got"];
```

```main()``` function:
```JavaScript
/**
 * The main method which drives the script.
 */
function main() {
    $("#textbox").keypress(function (e) {
        if (e.keyCode == 32) { //Listens for spacebar press from input box
            var input = document.getElementById("textbox").value; //Gets text from input box.
            input = input.split(" "); //Splits words into an array.
            input = input[input.length - 1]; //Get the last word in "array"
            var word = input.toLowerCase(); //Converts word to all lowercase to check against "boring" words

            //Checks if the word is a "boring" word
            if (boringWords.includes(word)) { //It's a boring word
                $("#live-display").append("<label>" + input + "</label>&nbsp;") //Displays word as plain text
            }
            else { //It's a "fun" word
                giphyRequest(input); //Send request to GIPHY and display as a .gif.
            }
        }
    });
}
```

When ```giphyRequest()``` is called, it takes the word and appends it to the route string, which the AJAX method will then call on the ```TranslateGIF()``` method in ```TranslateController.cs``` to make a request to GIPHY with the word to look up.

```JavaScript
/**
 * Sends request to GIPHY servers
 * @param {any} keyWord Word we want to get a .gif of
 */
function giphyRequest(keyWord) {
    var source = "Giphy/Image/" + keyWord;

    $.ajax({
        type: "GET",
        dataType: "json",
        url: source,
        success: displayGIF,
        error: ajaxError
    });
}
```

If the request returns a JSON-formatted response, then it will call on ```displayGIF()``` to display the ```.gif``` image from GIPHY to the browser. Otherwise, it will call on ```ajaxError()``` to inform the user it could not load the image due to some technical problem (i.e. servers are down, reached request limit, etc.).

```JavaScript
/**
 * Displays .gif image to client
 * @param {any} imageData JSON data received from GIPHY server
 */
function displayGIF(imageData) {
    var image = imageData.data.images.fixed_height_small.url; //URI of the .gif to display

    $("#live-display").append("<img src=\"" + image + "\"/>&nbsp;"); //Appends .gif to HTML document
}
```

```JavaScript
/** 
 * Displays AJAX error as pop-up alert
 */
function ajaxError() {
    alert("Unable to load image.");
}
```

## Step 6: Logging Requests

In addition to making a responsive, single-page application, we were asked to log search requests. More specifically, we were asked to make a table that holds search requests, the client's IP address and agent string, as well as the date and time the request was made. To accomplish this, I started by creating an ```up.sql``` script which will create a table to hold in the requested information.

```
CREATE TABLE [dbo].[AccessLogs]
(
  [ID]          INT IDENTITY (1,1)  NOT NULL,
  [IPAddress]   NVARCHAR(40)        NOT NULL,
  [Keyword]     NVARCHAR(100)       NOT NULL,
  [AgentString] NVARCHAR(256)       NOT NULL,
  [TimeStamp]   DATETIME            NOT NULL,

  CONSTRAINT [PK_dbo.AccessLogs] PRIMARY KEY CLUSTERED ([ID] ASC)
);
```

Next, I created a model called ```AccessLogs.cs``` that reinforces the requirements asked in ```up.sql```.

Snippet of model class:
```C#
public class AccessLogs
    {
        /// <summary>
        /// Primary key
        /// </summary>
        [Key]
        [Required]
        public int ID { get; set; }

        /// <summary>
        /// Client's IP address
        /// </summary>
        [Required]
        public String IPAddress { get; set; }
```

Then, I created a database context class that will act as a bridge between the database and web application.

```C#
/// <summary>
    /// Acts as a bridge between database and web application
    /// </summary>
    public class AccessLogsContext : DbContext
    {
        public AccessLogsContext() : base("name=AccessLogsDB") {}

        public virtual DbSet<AccessLogs> Logs { get; set; }
    }
```

The last thing to do in this step is write log request code in ```TranslateController.cs```, which essentially logs all AJAX requests to the database.

```C#
private AccessLogsContext db = new AccessLogsContext();

//[CODE IN-BETWEEN]

AccessLogs log = new AccessLogs();

log.IPAddress = Request.UserHostAddress; //logs client's IP address
log.KeyWord = word; //logs client's requested word
log.AgentString = Request.UserAgent; //logs client's browser information

//saves current request to database
db.Logs.Add(log);
db.SaveChanges();
```

## Step 7: Demo of Final Product

At this point, the assignment is pretty much complete. All that's left to do is confirm the application works as intended. [Follow this link to view the demo](https://youtu.be/LYGLfIfKgsE).
