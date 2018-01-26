# LeBlancCodes.Calendar
This project is an ASP.NET web service that serves a customized calendar with specialized recurring events.

## Classes

### <a name="BaseYearlyRecurringEvent"></a> BaseYearlyRecurringEvent
All yearly recurring events inherit from the same base type, *abstract*.

| Member | Type | Description |
| --- | --- | --- |
| Id | [Guid] | A unique identifier |
| Name | [string] | The event name |
| Type | [YearlyRecurringEventType](#YearlyRecurringEventType) | The type of recurring event |
| GetDate([int] year) | [DateTime] | Return the date this event occurs for the specified year |

### <a name="EasterBasedYearlyRecurringEvent"></a> EasterBasedYearlyRecurringEvent
> Inherits [BaseYearlyRecurringEvent](#BaseYearlyRecurringEvent)

This recurring event is dependent on when Easter occurs (e.g., Easter, Good Friday, Mardi Gras, Ash Wednesday, etc.)

| Member | Type | Description |
| --- | --- | --- |
| Offset | [int] | Between 365 and -365 exclusive, the number of days relative to Easter Sunday |

### <a name="ReferenceBasedYearlyRecurringEvent"></a> ReferenceBasedYearlyRecurringEvent
> Inherits [BaseYearlyRecurringEvent](#BaseYearlyRecurringEvent)

This recurring event is dependent on when another recurring event occurs (e.g., the day after Thanksgiving; _sidenote_: the fourth Friday in a month is before the fourth Thursday in a month if the first day of that month is a Friday).

| Member | Type | Description |
| --- | --- | --- |
| EventId | [Guid] | The unique identifier of the source event |
| Offset | [int] | Between 365 and -365 exclusive, the number of days relative to the source event |

### <a name="FixedDateBasedYearlyRecurringEvent"></a> FixedDateBasedYearlyRecurringEvent
> Inherits [BaseYearlyRecurringEvent](#BaseYearlyRecurringEvent)

This recurring event is set on a fixed date (e.g., New Year's, July 4) but shifts according to the specified observance rules. If no observance rule is defined for the `day_of_week` the event occurs, the event occurs on the exact date.

| Member | Type | Description |
| --- | --- | --- |
| Month | [Month](#Month) | The month |
| Day | [int] The day of the month |
| ObservanceRules | ICollection<[ObservanceRule](#ObservanceRule)> | The observance rules |

### <a name="MonthWeekBasedYearlyRecurringEvent"></a> MonthWeekBasedYearlyRecurringEvent
> Inherits [BaseYearlyRecurringEvent](#BaseYearlyRecurringEvent)

This recurring event occurs on the specified [DayOfWeek](#DayOfWeek) of the specified `Week` of the specified [Month](#Month), (e.g., Labor Day, Thanksgiving). The `Week` can be negative to indicate that it should count back from the last week, (e.g., Memorial Day).

| Member | Type | Description |
| --- | --- | --- |
| Month | [Month](#Month) | The month |
| Week | [int] | Between 5 and -5 inclusive, the week of the month |
| DayOfWeek | [DayOfWeek](#DayOfWeek) | The day of the week the event occurs | 

### <a name="ObservanceRule"></a> ObservanceRule
Observance rules define what action [FixedDateBasedYearlyRecurringEvent](#FixedDateBasedYearlyRecurringEvent) takes when determining its occurrences.

| Member | Type | Description |
| --- | --- | --- |
| DayOfWeek | [DayOfWeek](#DayOfWeek) | The day of the week the recurring event originally occurs |
| Action | [ObservanceAction](#ObservanceAction) | The action to take if the event originally occurs on `DayOfWeek` |
| Offset | [int] | Required if `Action` is `Shift`, between 7 and -7 exclusive, the number of days to shift

## Enums

### <a name="YearlyRecurringEventType"></a> YearlyRecurringEventType
0. [EasterBasedYearlyRecurringEvent](#EasterBasedYearlyRecurringEvent)
0. [ReferenceBasedYearlyRecurringEvent](#ReferenceBasedYearlyRecurringEvent)
0. [FixedDateBasedYearlyRecurringEvent](#FixedDateBasedYearlyRecurringEvent)
0. [MonthWeekBasedYearlyRecurringEvent](#MonthWeekBasedYearlyRecurringEvent)

### <a name="Month"></a> Month
1. January
2. February
3. March
4. April
5. May
6. June
7. July
8. August
9. September
10. October
11. November
12. December

### <a name="DayOfWeek"></a> DayOfWeek
0. Sunday
1. Monday
2. Tuesday
3. Wednesday
4. Thursday
5. Friday
6. Saturday

### <a name="ObservanceAction"></a> ObservanceAction
0. Shift
1. Drop

[Guid]: https://docs.microsoft.com/en-us/dotnet/api/system.guid?view=netcore-2.0
[string]: https://docs.microsoft.com/en-us/dotnet/api/system.string?view=netcore-2.0
[int]: https://docs.microsoft.com/en-us/dotnet/api/system.int32?view=netcore-2.0
[DateTime]: https://docs.microsoft.com/en-us/dotnet/api/system.datetime?view=netcore-2.0
[object]: https://docs.microsoft.com/en-us/dotnet/api/system.object?view=netcore-2.0
