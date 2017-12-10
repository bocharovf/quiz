# Entities
* Quiz template - series of questions and flow settings
* Quiz template group - group of quiz templates
* Quiz template section - logical section of quiz template
* Question - combination of question and answers
* Question group - group of similar / interchangeable questions
* Answer - answer for question
* Quiz - concrete passing of quiz based on quiz template
* Quiz question - passing of concrete question in quiz
* Quiz answer - answer for concrete question in quiz
* Achievement - achievement granted for quiz passing
* User achievement - concrete user achievement
* Invitation - invitation to pass a quiz
* User
* Role

## Quiz template
* Title
* Description
* Image
* Access
* Collection of quiz template sections

## Quiz template group
* Title
* Description
* Access
* Collection of quiz templates

## Quiz template section
* Title
* Description
* Image
* Collection of questions

## Question
* Text
* Type
* Collection of answers
* Collection of tags

## Question group
* Title
* Description
* Collection of questions

## Answer
* Text
* Is correct flag

## Quiz
* Date start
* Date end
* Is postponed
* Is cancelled
* Reference to user
* Reference to quiz template
* Collection of quiz passing questions

## Quiz question
* Date start
* Date end
* Reference to question
* Collection of quiz passing answers

## Quiz answer
* Reference to answer
* Raw answer value (for free text answers)
* Is correct flag
* Score earned
* Is skipped

## Achievement
* Title
* Description
* Image
* Reference to quiz template group or quiz template  
* Various calculation settings

## User achievement
* Reference to achievement
* User
* Date
* Collection of quizzes

## Invitation
* Title
* Text
* Reference to user