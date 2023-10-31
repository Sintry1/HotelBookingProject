Feature: Booking

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
	Given I have a booking request with the following details
            | Start Date  | End Date    |
            | 2023-11-01  | 2023-11-05  |
	When I attempt booking before occupied range
	Then Booking True

Scenario: Booking After Occupied Range
Given I have a booking request with the following details
            | Start Date  | End Date    |
            | 2023-12-01  | 2023-12-03  |
	When I attempt booking after occupied range
	Then Booking True

Scenario: No Booking During Occupied Range
	Given I have a booking request with the following details
            | Start Date  | End Date    |
            | 2023-11-26 | 2023-11-27  |
	When I attempt booking during occupied range
	Then Booking False

Scenario: EndDate overlaps occupied range
	Given I have a booking request with the following details
            | Start Date  | End Date    |
            | 2023-11-01  | 2023-11-11  |
	When I attempt booking during occupied range
	Then Booking False