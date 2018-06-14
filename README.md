# Project Title

Client-Service application for News Feed

## Description

Client Application allows to browse through news titles and read each of them.

After it launched - user get the welcome screen, after pressing the go button - the list of news title appearing.
By clicking them user get able to navigate to the selected article and read content and comments.

Client application keeps feed up to date, so you don't miss new posts.

Application consist of two parts

* WPF Client - Windows desktop application for browsing post and comments

* Http service - web.api services for data distributing


### Server architecture
Web service part of application - WEB api for retrivment application data such as post, summaries, comments.
Web.api is represented as Rest like service with only get method for next catalogs - post, postsummaries, comments

Database component is library provided by customer service.
As database component is slow and will sometimes timeout and throw an exception - current solution includes caching service for requested data for more reliable response.
Server side provides async model for all data requests. 
The service endpoint presented as Web.Api set of controllers.

### Client architecture
NewsFeed Client is WPF application.
With using Caliburn.Micro it provides MVVM pattern for all view/viewmodels.
Reactive extension allows us to provide peridocally data request to get latest updates for feed and comment feed
Addtional set of controls as Wpf.Toolkit is used for providing better UI/UX design
NUnit and Moq frameworks allows to unit test all of our ui behaviour


### Components

Next components was used for development 
* [Caliburn.Micro](https://github.com/Caliburn-Micro/Caliburn.Micro) - WPF Framework
* [WPF ToolKit](https://github.com/xceedsoftware/wpftoolkit) - WPF Controls
* [Rx.Net](https://github.com/dotnet/reactive) - The Reactive Extensions for .NET 
* [Simple Injector](https://github.com/simpleinjector/SimpleInjector) - Dependency Injection library
* [Moq](https://github.com/Moq/moq4/wiki/Quickstart) - Mocking framework
* [NUnit](https://github.com/nunit/nunit) - Unit Testing Framework



### Configuration

To configure service, you could modify Web.config appconfig section in Web.Services project

* DatabaseConnection - what kind of connection for database component shoud use 
Possible values are:
    * 42
    * towel
    * local
    * vogon

* UserInvalidationTime - value in second for storing cache values for user before they get outdated
* PostInvalidationTime - value in second for storing cache values for post before they get outdated
* SummariesInvalidationTime - value in second for storing cache values for post summaries before they get outdated
* CommentInvalidationTime- value in second for storing cache values for comment before they get outdated


To configure client app you could modify appconfig section of App.config in Client app
* Uri - service http access Uri
* BackgroundWorkerPullTick - how often client will sync data from service


## Running the application

To build and run current application just restore nuget pacakages for solution, after that it could be builded by MSbuild from Visual Studio
To get application work make sure that Web.Services project is run before Client application is launched. It could be done in two ways:
* View Web.Services in browser 
* Set Multiple (Client and Web.Services) startup projects


## Addition comments
* Database registered as Singleton in case it require only instantion for consitent data generation
* Database component was faceded by DB.Facade and Entites libraries
* Server side application prodeces container on each level of abstaction layer - to avoid referencing all project into the Web.Cleint one. Each level of abstaction provides only interface for current functionallity
* Caching was inclued for providing more stable work of application (decresing the latency) of request and get client more reliable for users
* Async model was used for system scalability
* MVVM pattern used for client application
* Rx.net and observables was used to provide reacite loading of PostSummaries and Comments. Client periodically ask http service for new bunch of data.
* Unit test was implements for client application as most significant part of application
* In case of using Caliburn.Micro and it components there is no way to provide unit test for some features of application f.e - Navigation - we cannot mock sealed or non virtual components for INavigationService


## Author

* **Vladimir Zarubin** - *Luxoft* - 


