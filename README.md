# ToDoer UI Test Example

This is an example project showcasing how to write maintainable UI tests using the Selenium framework with C#.

The repo contains a Vue 3 application (`src/ToDoer.App`), a .NET 5 Web API (`src/ToDoer.API`), a functional test project (`tests/ToDoer.FunctionalTests`), and the Selenium UI test project (`tests/ToDoer.UITests`).

The UI test project uses the [Legerity](https://github.com/MADE-Apps/legerity) framework to write the maintainable application element wrappers. It is also used to simplify the approach to launching the application in multiple platform scenarios, such as running on Google Chrome and Microsoft Edge.

This project has been used as a demo for my **Should you write UI tests?** presentation at the following events:

- [DDD East Midlands 2021](https://www.dddeastmidlands.com/2021/speakers/james-croft/)
