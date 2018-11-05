# Homework 6

In this week's assignment, we were tasked with using a pre-existing complex database to create a "people" search engine that looks up people from the database and displays relevant results based on the keywords passed to the search box. In addition, we were also required to create a page where users can view details about a searched person, such as their contact info, their company profile and the sales World Wide Importers (a fictitious company created by Microsoft) made to it.

## Links

* Assignment page can be found [here](http://www.wou.edu/~morses/classes/cs46x/assignments/HW6_1819.html)
* Code repository referencing this work can be found [here](https://github.com/mlarios1/CS460/tree/master/hw6)
* Clone repo link: [https://github.com/mlarios1/CS460.git](https://github.com/mlarios1/CS460.git)

## Step 1: Preparation

Given how we've been working with ASP.NET MVC for about three weeks now, there wasn't much new material we needed to cover for this assignment. However, since we're working with a pre-existing complex database, we had to learn about LINQ (language-integrated query) syntax in order to pull up the required information needed to complete this assignment. It was also at this stage I struggled on getting started on my assignment, mostly because I was used to having step-by-step guidance from the assignment page to get where I need to be, whereas in this assignment, we can get as creative as we want while keeping in mind of the general requirements we need to implement.

## Step 2: Importing Database and Model Classes

After spending some time playing with LINQ using LINQPad and receiving guidance from my peers and my professor, I started out by importing the database in Microsoft SQL Server Management Studio and establishing a connection to it to access in Visual Studio. Next, I generated all the models (minus the archives) and placed them in my ```Models``` folder, while placing ```WWIContext.cs``` in to my Data Access Layer (DAL) folder.

[![Solution pane with models](https://mlarios1.github.io/mlarios1.github.io/CS460/HW6/model_imports.PNG)](https://mlarios1.github.io/mlarios1.github.io/CS460/HW6/model_imports.PNG)

## Step 3: Creating a Serach Engine (Feature #1)

Following setup, I modified the ```Index()``` method (which loads the index page) in ```HomeController.cs``` to include a ```string``` parameter called ```query``` which will take in keywords passed in from the search box to process and return the result to display in the index page.

```C#
private WWIContext db = new WWIContext();

/// <summary>
/// Performs the search based on the keywords passed from search box
/// </summary>
/// <param name="query">keyword(s) to search</param>
/// <returns>A list of Person objects</returns>
[HttpGet]
public ActionResult Index(string query)
{
	IEnumerable<Person> people = db.People.Where(p => p.FullName.Contains(null)); //start out with an empty container
	ViewBag.ShowError = false; //do not show search error if loading the page for the first time or after a successful search
	ViewBag.ResultString = ""; //shows empty "result" string if loading the page for the first time or after a failed search

	if(!string.IsNullOrWhiteSpace(query)) //makes sure query is not blank or contains only whitespaces.
	{
		people = db.People.Where(peopleItem => peopleItem.FullName.Contains(query)); //performs the query
		if (!people.Any()) //No matches? It's a failed search.
		{
			ViewBag.ShowError = true; //display failed search message
		}
		else //successful search
		{
			ViewBag.ResultString = "Names matching your search: \"" + query + "\"";
		}
	}

	return View(people.ToList());
}
```

## Step 4: Modifying the Index page

As I tweaked the ```Index()``` method, I scrapped the scaffolded Index page and replaced it with my own, which uses a model in order to iterate through the collection of ```Person``` items. In this case, I'm using it to display relevant results in the search results area. In addition, I've also set it so that each ```Person``` being displayed includes a hyperlink that will lead users to their respective details page. By working back and forth between the controller and and the view, I managed to get the search engine up and running.

```HTML
@model IEnumerable<WWImporters.Models.Person>

@{
    ViewBag.Title = "Home";
}

<br />
<br />

<h2 style="text-align: center;">Client Search</h2>

@*Search box and button*@
<form action="/Home/Index" method="get">
    <div class="row">
        <div class="col-sm-12">
            <input name="query" id="search" placeholder="Search by client name" />
            <input type="submit" class="btn-lg btn-info" id="submit" value="Search" />
        </div>
    </div>
</form>

<br />
<br />

@*Displays an error if no matches are found in controller*@
@if (ViewBag.ShowError)
{
    <h5 style="color: #8B0000">No results found.</h5>
}
else
{
    <h5>@ViewBag.ResultString</h5>
}


@*Displays search results binded with hyperlink*@
@foreach (var person in Model)
{
    <a class="btn btn-default btn-lg btn-block" href="@Url.Action("Details", "Home", new { id=person.PersonID })">@Html.DisplayFor(personItem => person.FullName)</a>
}
```

[![Search engine page](https://mlarios1.github.io/mlarios1.github.io/CS460/HW6/searchengine.PNG)](https://mlarios1.github.io/mlarios1.github.io/CS460/HW6/searchengine.PNG)

## Step 5: Building the Details Page

Having built the search engine, all that was left to do was build the ```Details``` page so that it displays a ```Person```'s name, phone number, photo and so forth. To accomplish this, I created strongly-typed views so that it accesses the database and returns related information about the ```Person``` in question.

```HTML
@model WWImporters.Models.Person

@{
    ViewBag.Title = "Details";
}

<br />
<br />

@*Displays Person information*@
<div class="container">
    <div class="row">
        <div class="col-sm-8 details-box">
            <h2>@Html.DisplayFor(model => model.FullName)</h2>
            <hr style="border-color: black;" />
            <dl class="dl-horizontal">
                <dt>
                    Preferred name:
                </dt>
                <dd>
                    @Html.DisplayFor(model => model.PreferredName)
                </dd>

                <dt>
                    Phone:
                </dt>
                <dd>
                    @Html.DisplayFor(model => model.PhoneNumber)
                </dd>

                <dt>
                    Fax:
                </dt>
                <dd>
                    @Html.DisplayFor(model => model.FaxNumber)
                </dd>

                <dt>
                    E-mail:
                </dt>
                <dd>
                    <a href="mailto:@Html.DisplayFor(model => model.EmailAddress)">@Html.DisplayFor(model => model.EmailAddress)</a>
                </dd>

                <dt>
                    Member since:
                </dt>
                <dd>
                    @Html.DisplayFor(model => model.ValidFrom)
                </dd>
            </dl>
        </div>

        @*Profile photo placeholder*@
        <div class="col-sm-4">
            <img src="https://via.placeholder.com/225?text=Photo" alt="Profile photo" class="center-block"/>
        </div>
    </div>
</div>
```

[![Simple details page](https://mlarios1.github.io/mlarios1.github.io/CS460/HW6/detailspage.PNG)](https://mlarios1.github.io/mlarios1.github.io/CS460/HW6/detailspage.PNG)
