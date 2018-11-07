# Homework 6

In this week's assignment, we were tasked with using a pre-existing complex database to create a "people" search engine that looks up people from the database and displays relevant results based on the keywords passed to the search box. In addition, we were also required to create a page where users can view details about a searched person, such as their contact info, their company profile and the sales World Wide Importers (WWI) made to it.

## Links

* Assignment page can be found [here](http://www.wou.edu/~morses/classes/cs46x/assignments/HW6_1819.html)
* Code repository referencing this work can be found [here](https://github.com/mlarios1/CS460/tree/master/hw6)
* Clone repo link: [https://github.com/mlarios1/CS460.git](https://github.com/mlarios1/CS460.git)
* Video demo link: [https://youtu.be/GMYBfBYZiug](https://youtu.be/GMYBfBYZiug)

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

## Step 6: Upgrading the Details Page (Feature #2)

At this point, I already have a working search engine and a ```Details``` page that displays a ```Person```'s contact info. Now we're being asked to populate the ```Details``` page with details about their company and WWI's sales to it, that is if the ```Person``` in question is a customer. To accomplish this, I started out by creating a "view model", which will allow me to return multiple model objects to the view to display the required information.

```C#
/// <summary>
/// Allows multiple objects to be returned to the view
/// </summary>
public class DashboardVM
{
	/// <summary>
        /// Person object
        /// </summary>
        public Person Person { get; set; }

        /// <summary>
        /// Customer object
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// List of invoice objects
        /// </summary>
        public IEnumerable<Invoice> Invoice { get; set; }

        /// <summary>
        /// List of InvoiceLine objects
        /// </summary>
        public IEnumerable<InvoiceLine> InvoiceLine { get; set; }
}
```

Next, I refactored the ```Details()``` method in ```HomeController.cs``` so that below the code shown back in Step 3, it determines whether or not the person searched is a primary contact. If so, it grabs the customer information and performs the necessary calcuations to find the gross sales and profit, as well as return the 10 most profitable items the customer has purchased from WWI. Had it been the other way around, it would've indicated that the ```Person``` is a salesperson and therefore would not return anything other than their basic contact info.

```C#
if(vm.Person.Customers2.Any()) //Is person a primary contact?
{
	ViewBag.PrimaryContact = true; //Person has primary contact
	vm.Customer = vm.Person.Customers2.FirstOrDefault(); //Get customer info

	//get all invoices and invoice lines
	var baseCode = vm.Customer.Orders.SelectMany(i => i.Invoices)
					 .SelectMany(il => il.InvoiceLines);

	ViewBag.GrossSales = baseCode.Sum(e => e.ExtendedPrice); //calculate gross sales
	ViewBag.GrossProfit = baseCode.Sum(lp => lp.LineProfit); //calculate total profit

	vm.InvoiceLine = baseCode.OrderByDescending(lp => lp.LineProfit)
				 .Take(10)
				 .ToList();
}
```

As I worked on refactoring the ```Details()``` method, I also worked on modifying the ```Details``` page so that it not only displays the ```Person```'s contact info, but also the other information mentioned above (if needed), all of which we get from the controller after processing.

Here's a sample of the modified ```Details``` page.
```HTML
<div class="row">
	<div class="col-sm-6 details-box">
		<h2>Purchase History Summary</h2>
		<hr style="border-color: black;"/>
		<dl class="dl-horizontal">
			<dt>Orders:</dt>
			<dd>@Html.DisplayFor(model => model.Customer.Orders.Count)</dd>

			<dt>Gross sales:</dt>
			<dd>@String.Format("{0:C}", ViewBag.GrossSales)</dd>

			<dt>Gross profit:</dt>
			<dd>@String.Format("{0:C}", ViewBag.GrossProfit)</dd>
		</dl>
	</div>
</div>
```

## Step 7: Demoing the Final Product

After all was said and done, the only thing left to do at this point is test the program to confirm my website works as it's supposed to. [Follow this link to view the demo](https://youtu.be/GMYBfBYZiug).
