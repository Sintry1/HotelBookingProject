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
	| 2         | 5       | true      |
	| 21        | 24      |true     |
	| 9         | 21      |false    |
	| 6         | 10      |false    |
	| 6         | 15      |false    |
	| 15        | 21      |false    |
	| 20        | 24      |false    |
	| 10        | 15      |false    |
	| 10        | 20      |false    |
	| 14        | 20      |false    |