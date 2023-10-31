Feature: Booking

A short summary of the feature
No clue if the following is done correct, but it's an attempt at implementing specflow

Background: 
Given occupiedRange
| room | startDate | endDate |
| 1    |  2023-11-25  |    2023-11-30     |
| 2    |  2023-11-25  |    2023-11-30     |
| 1    |  2023-11-10  |    2023-11-13     |
| 2    |  2023-11-10  |    2023-11-13     |



@mytag
Scenario: Booking before occupied range
	Given I have input a startDate before Occupied Range
	And I have input a endDate before Occupied Range
	When I attempt booking before occupied range
	Then A booking is made before occupied range

Scenario: Booking After Occupied Range
	Given I have input a startDate after Occupied Range
	And I have input a endDate after Occupied Range
	When I attempt booking after occupied range
	Then A booking is made after occupied range

Scenario: No Booking During Occupied Range
	Given I have input a startDate during Occupied Range
	And I have input a endDate during Occupied Range
	When I attempt booking during occupied range
	Then no booking is made