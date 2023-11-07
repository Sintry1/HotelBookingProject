Feature: SpecflowBooking

A short summary of the feature
No clue if the following is done correct, but it's an attempt at implementing specflow

Background: 
Given occupiedRange
Example: 
| startDate | endDate |
|  2023-11-25  |    2023-11-30     |
|  2023-11-25  |    2023-11-30     |
|  2023-11-10  |    2023-11-13     |
|  2023-11-10  |    2023-11-13     |



@mytag
Scenario: Booking before occupied range
	Given I have input a startDate
	And I have input an endDate
	When I attempt booking before occupied range
	Then Booking True

Scenario: Booking After Occupied Range
	Given I have input a startDate 
	And I have input an endDate 
	When I attempt booking after occupied range
	Then Booking True

Scenario: No Booking During Occupied Range
	Given I have input a startDate during Occupied Range
	And I have input a endDate during Occupied Range
	When I attempt booking during occupied range
	Then Booking False

Scenario: EndDate overlaps occupied range
	Given I have input a startDate before Occupied Range
	And I have input a endDate during Occupied Range
	When I attempt booking during occupied range
	Then Booking False