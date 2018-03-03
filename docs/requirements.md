# Functional requirements
**Note**: this is initial feature list done on early planning stage. Keep it here just for historical reasons. It's frozen and read only. All feature planning will be done on github.

## Auth
* Auth via Facebook, google+, VK etc...
* Auth with login / password
* User & admin roles

## Quiz editor
* CRUD for quizzes / questions
* Share quiz invitations via email
* Upload images for quiz
* Manage achievements
* Access: anonymous quiz / public quiz / by invitation code / for particular user

## Quiz flow
* Time limitation for quiz, question, area
* Answers shuffle
* Question shuffle by group / tag
* quiz areas - large subsets of questions
* ability to postpone question
* Ability to replace question from group
* ability to change previous answer
* Save quiz progress and come back later

# Score calculation
* Different score calculation strategies

## Different types of questions
* choose single/many right answer(s) from options
* Several sub-questions in question
* drag & arrange parts of answer
* choose single wrong answer among right ones
* free-text answers with validators (equal, like, regex)

## Collect and show statistics
* for current quiz on finish
* for authorized user's - all passed quizzes on demand
* for quiz author - statistic for all his quizzes

## Achievements for
* passing particular quiz / quizzes
* earn particular % of right answers
* passing quiz in particular time

## Other features
* Import/export quizzes

# Technical requirements
* .NETCore 2.0 / .NET Standard
* ASP.NET Core
* Angular + Typescript
* SQL database accessed with ORM (MySQL/PostgreSQL/?)
* Redis as a cache
* Docker
* Hosted on Ubuntu 16.04