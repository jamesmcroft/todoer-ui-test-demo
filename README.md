# ToDoer UI Test Example

This is an example project showcasing how to write maintainable UI tests using the Selenium framework with C#.

The repo contains a Vue 3 application (`src/ToDoer.App`), a .NET 5 Web API (`src/ToDoer.API`), a functional test project (`tests/ToDoer.FunctionalTests`), and the Selenium UI test project (`tests/ToDoer.UITests`).

The UI test project uses the [Legerity](https://github.com/MADE-Apps/legerity) framework to write the maintainable application element wrappers. It is also used to simplify the approach to launching the application in multiple platform scenarios, such as running on Google Chrome and Microsoft Edge.

This project has been used as a demo for my **Should you write UI tests?** presentation at the following events:

- [DDD East Midlands 2021](https://www.dddeastmidlands.com/2021/speakers/james-croft/)

## What's shown in the demo? 💭

- **Base UI Test Structure** - A showcase of creating a maintainable UI test project structure including using Legerity's AppManager to simplify the launch of your application as a `BaseTestFixture`, as well as writing UI tests for functional requirements
- **Page Object Pattern** - Taking advantage of this common UI test design pattern, this demo showcases how you can leverage the page object pattern in a UI test project to create easy to maintain tests.
- **UI Element Wrappers** - Using a combination of out-of-the-box Legerity element wrappers plus showcasing how to create element wrappers for your application's UI components. This approach provides an easy to implement interaction model for creating UI tests.
- **Multi Browser Testing** - Taking advantage of test fixture sources in NUnit and the Legerity AppManager, this project showcases how you can run the same tests on multiple browsers! In this example, we showcase tests running on Microsoft Edge (Chromium) and Google Chrome.

## Running the ToDoer demo 🏗

This demo has been built as separate front-end (Vue 3 web app) and back-end (ASP.NET Core API) applications. 

The back-end takes advantage of Entity Framework Core with a SQL database. If you're using [Docker](https://www.docker.com/), we've provided a `docker-compose` file at the root of the repo that you can use to spin up a SQL Server instance for this demo. Alternatively, you can update the connection string in the `appsettings.json` file to point to a local instance.

The project has configurations for running the demo in [Visual Studio Code](https://code.visualstudio.com/) simply by going to the **Run and Debug (CTRL+Shift+D)** pane, and spinning up the **Run: ToDoer** configuration. 

This will run up the web API on https://localhost:5001, and the ToDoer app will run on https://localhost:3000/.

### Running the tests 👏🏻

Now that you have the application running locally on your machine, it's time to run the tests! 

**Note**, the UI test project runs tests on **Google Chrome (v94)** and **Microsoft Edge (v94)**. You will need to ensure you have these versions installed. If you are using an older version of these browers, you will need to upgrade. If you are using a newer version of these browsers, you will need to update the `<PackageReference>` in the **ToDoer.UITests.csproj** file for **Selenium.WebDriver.ChromeDriver** and **Selenium.WebDriver.MSEdgeDriver** to the latest versions.

To run the tests, you can do this within Visual Studio Code by running the command **CTRL+Shift+P**, typing in **Run Test Task**, hitting enter, and selecting the **run_ui_tests** option. 

The UI tests will start running on your local machine, launching the browser at the application URL, and begin the automation! 

When complete, you'll find the test pass/fail results in your Visual Studio Code terminal 🙌🏻 
