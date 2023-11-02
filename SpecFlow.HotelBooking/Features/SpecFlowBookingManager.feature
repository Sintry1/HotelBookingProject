Feature: Booking

A short summary of the feature
No clue if the following is done correct, but it's an attempt at implementing specflow

Background: 
Given I have a repository of booked dates


@mytag
Scenario Outline: Create a booking
    Given I have entered a start date in <startDate>
    And I have entered an end date in <endDate>
    When I press Create New Booking
    Then The result should be <return>

    	Examples:
	| startDate | endDate | return |
	| 2         | 5       | 1      |
	| 21        | 24      | 1      |
	| 9         | 21      | -1     |
	| 6         | 10      | -1     |
	| 6         | 15      | -1     |
	| 15        | 21      | -1     |
	| 20        | 24      | -1     |
	| 10        | 15      | -1     |
	| 10        | 20      | -1     |
	| 14        | 20      | -1     |