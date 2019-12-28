# Parking Charge Calculator

To edit the scenarios, edit "[ProjectDirectory]/ParkingChargeCalculator/ParkingChargeInstructions.csv".<br/>
It is included in the solution by default.<br/>
If you wish to change the file then just update the "FilePath" const in CalculateParkingChargesExecutor.cs.

## File Structure

| CsvIndex | Key         | Format                | Description                                                       |
|----------|-------------|-----------------------|-------------------------------------------------------------------|
| 0        | ChargeType  | ShortState | LongStay | The ChargeType of the scenario                                    |
| 1        | ChargeStart | yyyy-MM-ddThh:mm:ss   | The start DateTime of the parking period                          |
| 2        | ChargeEnd   | yyyy-MM-ddThh:mm:ss   | The end DateTime of the parking period (ChargeEnd >= ChargeStart) |

### Example:

ShortStay,2020-01-01T10:30:00,2020-01-01T14:15:00<br/>
LongStay,2020-01-01T05:00:00,2020-01-04T17:30:00
