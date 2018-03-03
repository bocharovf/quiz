# Continuous integration and delivery

Process of building, testing and publishing application is automated via [Visual Studio Online (VSTS)](https://www.visualstudio.com).

There are two build definitions created:
* To build and test every tagged commit on master branch. If all tests are green it builds Docker images for components and pushes them to [public Docker Hub repository](https://hub.docker.com/r/bocharovf/).
* To verify every pull request to master branch by running unit tests for all components.

So it's almost enough to create new release on Github to get it built and published.

Currently the only remaining manual piece of work is updating of Docker images on production server. Thought I am going to eliminate that little inconvenience soon.