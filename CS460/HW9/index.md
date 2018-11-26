# Homework 9

For our final assignment this term, we were asked to deploy our finished [Homework 8](https://mlarios1.github.io/mlarios1.github.io/CS460/HW8/) web application to the cloud, using Microsoft Azure as our cloud deployment platform.

## Links

* Assignment page can be found [here](http://www.wou.edu/~morses/classes/cs46x/assignments/HW9_1819.html)
* Deployed website on Microsoft Azure: [http://reginalds.azurewebsites.net/](http://reginalds.azurewebsites.net/)

## Step 1: Create a Resource Group

To begin, I went over to my Microsoft Azure account. From there, I created a resource group called **CS460Cloud**, which will host my web application and my database.

```<INSERT IMAGE HERE>```

## Step 2: Create a Database

After creating a resource group, I then created a database where I chose a user name and password to connect to it from Visual Studio at a later time.

```<INSERT IMAGE HERE>```

## Step 3: Create a Firewall Rule

Once I added a database, I went on to create a firewall rule in the firewall settings panel which will allow me to execute SQL commands to the database remotely.

```<INSERT IMAGE HERE>```

## Step 4: Execute SQL Script to Database

After setting up the database, I went on over to Visual Studio to connect to it and execute my ```up.sql``` script.

```<INSERT IMAGE HERE>```

## Step 5: Create a Web App Service

Heading back to my Azure account, I went on to create a web app service by going to _App Settings_, clicking the **Add** button and picking _Web App_. From there, I filled out some basic setup information, such as choosing an app name, subscription, resource group and so forth and hit **Create**.

## Step 6: Set up the Web App Service

At this point, the web app service is up and running. However, before I can deploy my web app to the cloud, I need it to somehow communicate to the database server. To accomplish this, I grabbed the connection string from my database server in the database control panel

```<INSERT IMAGE HERE>```

and then I headed back over to the web app, went to _Application Settings_ and scrolled down to the _connection strings_ section to paste in my connection string (which has my user name and password in there in order to connect to it), typed in my database name and made sure the type was set to **SQLServer**.

> **AUTHOR'S NOTE: I recognize that entering my database login information in plain text in the connection string compromises the security of my database server. However, since this is the first time we're working on a cloud-based platform late in the term, we've been instructed to leave it as-is, but we will work on concealing this kind of information next term.**

```<INSERT IMAGE HERE>```

## Step 7: Deploy to the Cloud

Now that everything's all set up and ready to go, I switch over to Visual Studio, build my solution (to make sure there are no errors that need to be corrected) and navigate to _Build > Publish_ to deploy my web app to the cloud where a window pops up and asks me how I want to publish it and where to host it.

```<INSERT IMAGE HERE>```

## Step 8: Demo the Website

At this stage, I've successfully published my website to the cloud. All that's left to do is test it out and make sure everything functions as intended.

_Demo of video:_
```<INSERT IMAGE HERE>```

Also, if you'd like, you can visit my website at [http://reginalds.azurewebsites.net/](http://reginalds.azurewebsites.net/) and play around with it.
