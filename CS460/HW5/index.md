# Homework 5

Similar to our last assignment, we were tasked with writing a ASP.NET MVC web application, this time with a database. The goal of this assignment was to create an input form (in our case, a tenant request form) that would interact with a database.

## Links

* Assignment page can be found [here](http://www.wou.edu/~morses/classes/cs46x/assignments/HW5_1819.html)
* Code repository referencing this work can be found [here](https://github.com/mlarios1/CS460/tree/master/hw5)
* Clone repo link: [https://github.com/mlarios1/CS460.git](https://github.com/mlarios1/CS460.git)
* Video demo link: [https://youtu.be/DFnHS4PDMIA](https://youtu.be/DFnHS4PDMIA)

## Step 1: Preparation
Given the nature of this assignment, there was not much new material to learn. However, I had to re-familiarize myself with SQL syntax (MySQL in particular), given I had not worked with it since June 2018. Soon after, I went on to learn about Transact-SQL (T-SQL) since we're to create a table that Visual Studio's built-in LocalDB can work with.

## Step 2: Writing an SQL table
At this point, I haven't fully mastered SQL, but I know enough to create a single table needed for this assignment. To begin, I started by writing an SQL script (saved as ```up.sql```) in Visual Studio's table designer (mostly to check syntax errors) that constructs a table, containing the necessary keys to store in information from the tenant request form to the database and populated it with a few sample entries.

```SQL
-- Tenants table
CREATE TABLE [dbo].[Tenants]
(
  [ID]            INT IDENTITY (1,1)  NOT NULL,
  [FirstName]     NVARCHAR(25)        NOT NULL,
  [LastName]      NVARCHAR(35)       NOT NULL,
  [PhoneNumber]   NVARCHAR(12)        NOT NULL, 
  [ApartmentName] NVARCHAR(40)       NOT NULL, 
  [UnitNumber]    INT                 NOT NULL, 
  [Description]   NVARCHAR(MAX)       NOT NULL, 
  [Checkbox]      BIT                 NOT NULL, 
  [Received]      DATETIME            NULL 
  CONSTRAINT [PK_dbo.Tenants] PRIMARY KEY CLUSTERED ([ID] ASC)
);

INSERT INTO [dbo].[Tenants] (FirstName, LastName, PhoneNumber, ApartmentName, UnitNumber, Description, Checkbox, Received) VALUES
	('Jim','Johnson','503-999-8888', 'Alpha Apartments', 11, 'Stovetop is in need of repair.', 0, '10-18-2018 02:30:18 PM' ),
	('John','Schwartz','503-777-5555', 'Woodland Apartments', 7, 'Heating element in dryer is broken.', 1, '10-01-2018 10:08:32 AM' ),
	('Kate','Ocean','503-444-3333', 'Vista Apartments', 4, 'Mice infestation is out of control', 1, '09-30-2018 07:44:09 PM' ),
	('Suzy','Collins','971-444-8888', 'Woodland Apartments', 16, 'Can we get a swing set for the playground?', 0, '10-13-2018 09:55:57 AM' ),
	('John','Skeeter','971-222-5555', 'Alpha Apartments', 3, 'Tired of asking neighbor to turn down the music.', 1, '09-22-2018 10:12:07 PM' )
GO
```

In addition, I also wrote another SQL script (saved as ```down.sql```) that would clear the table above, if needed to.
```SQL
-- Take the Tenants table down
DROP TABLE [dbo].[Tenants];
```

## Step 3: Writing the Model class

After writing SQL scripts for building and dropping the table, I went on to write a model class I later saved as ```Tenant.cs```, which will not only serve to reinforce the requirements from the ```up.sql``` script but enforce it server and client-side using data annotations. Having [this page](https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=netframework-4.7.2) from Microsoft in handy not only made the modeling process easier, it also gave me better control on the constraints I wanted to lay out there. Here are some examples:

```C#
[Required(ErrorMessage = "This field cannot be left blank."), Display(Name = "Last name")]
[StringLength(35, MinimumLength = 3, ErrorMessage = "Must be between 3-35 characters.")]
public string LastName { get; set; }

[Required(ErrorMessage = "Phone number must be in proper format. i.e. 555-555-5555")]
[Display(Name = "Phone number"), Phone]
public string PhoneNumber { get; set; }

[Required(ErrorMessage = "This field cannot be left blank."), Display(Name = "Apartment name")]
[StringLength(40, MinimumLength = 8, ErrorMessage = "Must be between 8-40 characters.")]
public string ApartmentName { get; set; }

[Required(ErrorMessage = "This field cannot be left blank."), Display(Name = "Unit number")]
[Range(1, 99, ErrorMessage = "Please enter a one or two-digit number")]
public int UnitNumber { get; set; }
```

## Step 4: Writing a Database Context class

Following the creation of the model class, I created a "database context" class (```TenantContext.cs```) that would act as a "bridge" between the database and the web application, which helps with creating, viewing and deleting entries.

```C#
/// <summary>
    /// Acts as a bridge between database and web application
    /// </summary>
    public class TenantContext : DbContext
    {
        public TenantContext() : base("name=TenantForms")
        {

        }

        public virtual DbSet<Tenant> Tenants { get; set; }
    }
```

## Step 5: Writing the controller

At this stage, I've pretty much finished the modeling aspect of the assignment. However, before starting on the views, I had to create additional methods in ```HomeController.cs``` to add in functionality when a user creates or views requests while guarding against human error. Let's look at the following code snippet as an example:

```C#
	// GET: Home/Delete/[ID]
        public ActionResult Delete(int? id)
        {
            if (id == null) //Non-existant ID?
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); //Returns a 400 error
            }
            Tenant tenant = db.Tenants.Find(id); //looks up tenant by ID
            if (tenant == null) //Non-existant tenant object? Return an error page.
            {
                return HttpNotFound();
            }
            return View(tenant);
        }

        // POST: Home/Delete/[ID]
        //Deletes tenant request from database.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tenant tenant = db.Tenants.Find(id);
            db.Tenants.Remove(tenant);
            db.SaveChanges();
            return RedirectToAction("ViewForms");
        }
```

Suppose we have a user that wants to delete a request. How that works is, when they click on the delete link on a request in the view requests page, it will call the Delete GET method, passing in the ID number of the request and first checks to see if the ID associated with the request exists. If so, we then check to make sure the request (treated as a Tenant object) exists. If so, we return the details of the request the user wants to delete. Had it been the other way around in either one of the two cases, they would've been presented with an error page.

At this point, the user is very sure about deleting the request. Upon clicking the "Delete" button, the deletion will take place server-side. After that's complete, we'll want to take the user back to the "view requests" page. That's handled using a ```RedirectToAction()``` method which will do just that.

Combined, we have what's known as a GET/POST/Redirect pattern that is commonly used by web developers.

## Step 6: Writing views

When I wrote views in the past (see [Homework 4](https://mlarios1.github.io/mlarios1.github.io/CS460/HW4/)), I mostly wrote results using ViewBags. However, in this assignment, we were required to write strongly-typed views. For the most part, my views consisted mostly of lambda functions written in Razor to send or return information. Here's a snippet from my ViewForms page:

```HTML
@*Table header with generated names from model class*@
<table class="table">
    <tr>
        <th>
            @Html.DisplayNameFor(model => model.FirstName)
        </th>
        <th>
            @Html.DisplayNameFor(model => model.LastName)
        </th>
        <th>
            @Html.DisplayNameFor(model => model.PhoneNumber)
        </th>
        <th>
            @Html.DisplayNameFor(model => model.ApartmentName)
        </th>
        <th>
            @Html.DisplayNameFor(model => model.UnitNumber)
        </th>
        <th>
            @Html.DisplayNameFor(model => model.Description)
        </th>
        <th>
            @Html.DisplayNameFor(model => model.Checkbox)
        </th>
        <th>
            @Html.DisplayNameFor(model => model.Received)
        </th>
        <th></th>
    </tr>

@*Generates a row of information for each entry from the database*@
@foreach (var item in Model) {
    <tr>
        <td>
            @Html.DisplayFor(modelItem => item.FirstName)
        </td>
        <td>
            @Html.DisplayFor(modelItem => item.LastName)
        </td>
        <td>
            @Html.DisplayFor(modelItem => item.PhoneNumber)
        </td>
        <td>
            @Html.DisplayFor(modelItem => item.ApartmentName)
        </td>
        <td>
            @Html.DisplayFor(modelItem => item.UnitNumber)
        </td>
        <td>
            @Html.DisplayFor(modelItem => item.Description)
        </td>
        <td>
            @Html.DisplayFor(modelItem => item.Checkbox)
        </td>
        <td>
            @Html.DisplayFor(modelItem => item.Received)
        </td>
        <td>
            @*Link to delete request*@
            @Html.ActionLink("Delete", "Delete", new { id=item.ID })
        </td>
    </tr>
```

As I wrote my views, the one I struggled getting to work was the arrangement of the tenant request form (```RequestForm.cshtml```), namely the description box. I got it to stretch vertically using ```@rows = 10```. However, it just wouldn't stretch horizontally. After tweaking the code for nearly six hours straight, I found that the problem lied within ```site.css``` in which the width was specified to a fixed size. After commenting that part out, I found that the description box was now able to stretch horizontally to the most the container would allow, which is what I wanted.

```
/* Set width on the form input elements since they're 100% wide by default
input,
select,
textarea {
    max-width: 280px; //The culprit to my problem.
}
*/
```

## Step 7: Demoing the Website

At this stage, my views have all been written and all that's left to do is demo my website to confirm it works.
[Click here to view the demo.](https://youtu.be/DFnHS4PDMIA)
