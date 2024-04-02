using System;
using System.Collections.Generic;

public enum SeatLabel { A, B, C, D }

public class Seat
{
    public SeatLabel Label { get; }
    public bool IsBooked { get; set; }
    public Passenger Passenger { get; set; }

    public Seat(SeatLabel label)
    {
        Label = label;
        IsBooked = false;
        Passenger = null;
    }
}

public class Passenger
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int SeatPreference { get; set; } // 1 for Window, 2 for Aisle, 0 for Any
    public Seat AssignedSeat { get; set; }

    public Passenger(string firstName, string lastName, int seatPreference)
    {
        FirstName = firstName;
        LastName = lastName;
        SeatPreference = seatPreference;
        AssignedSeat = null;
    }
}

public class Plane
{
    private List<List<Seat>> seatingPlan;

    public Plane()
    {
        seatingPlan = new List<List<Seat>>();
        for (int i = 0; i < 12; i++)
        {
            var row = new List<Seat>();
            for (int j = 0; j < 4; j++)
            {
                row.Add(new Seat((SeatLabel)j));
            }
            seatingPlan.Add(row);
        }
    }

    public string BookTicket(Passenger passenger)
    {
        foreach (var row in seatingPlan)
        {
            foreach (var seat in row)
            {
                if (!seat.IsBooked && passenger.SeatPreference == 0)
                {
                    seat.IsBooked = true;
                    seat.Passenger = passenger;
                    passenger.AssignedSeat = seat;
                    return $"The seat located in row {seatingPlan.IndexOf(row) + 1} {(char)(seat.Label + 'A')} has been booked.";
                }
                else if (!seat.IsBooked && passenger.SeatPreference == 1 &&
                         (seat.Label == SeatLabel.A || seat.Label == SeatLabel.D))
                {
                    seat.IsBooked = true;
                    seat.Passenger = passenger;
                    passenger.AssignedSeat = seat;
                    return $"The window seat located in row {seatingPlan.IndexOf(row) + 1} {(char)(seat.Label + 'A')} has been booked.";
                }
                else if (!seat.IsBooked && passenger.SeatPreference == 2 &&
                         (seat.Label == SeatLabel.B || seat.Label == SeatLabel.C))
                {
                    seat.IsBooked = true;
                    seat.Passenger = passenger;
                    passenger.AssignedSeat = seat;
                    return $"The aisle seat located in row {seatingPlan.IndexOf(row) + 1} {(char)(seat.Label + 'A')} has been booked.";
                }
            }
        }
        return "Sorry, the plane is fully booked.";
    }

    public void PrintSeatingChart()
    {
        foreach (var row in seatingPlan)
        {
            foreach (var seat in row)
            {
                if (seat.IsBooked)
                {
                    Console.Write($"{seat.Passenger.FirstName[0]}{seat.Passenger.LastName[0]} ");
                }
                else
                {
                    Console.Write($"{(char)(seat.Label + 'A')} ");
                }
            }
            Console.WriteLine();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Plane plane = new Plane();
        while (true)
        {
            Console.WriteLine("\nPlease enter 1 to book a ticket.");
            Console.WriteLine("Please enter 2 to see seating chart.");
            Console.WriteLine("Please enter 3 to exit the application.");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("\nPlease enter the passenger's first name:");
                    string firstName = Console.ReadLine();
                    Console.WriteLine("Please enter the passenger's last name:");
                    string lastName = Console.ReadLine();
                    Console.WriteLine("Please enter 1 for a Window seat preference, 2 for an Aisle seat preference, or hit enter to pick first available seat:");
                    string preferenceInput = Console.ReadLine();
                    int preference;
                    if (!int.TryParse(preferenceInput, out preference))
                    {
                        preference = 0;
                    }
                    Passenger passenger = new Passenger(firstName, lastName, preference);
                    string result = plane.BookTicket(passenger);
                    Console.WriteLine(result);
                    break;

                case "2":
                    plane.PrintSeatingChart();
                    break;

                case "3":
                    Console.WriteLine("Exiting the application.");
                    return;

                default:
                    Console.WriteLine("Invalid choice. Please enter 1, 2, or 3.");
                    break;
            }
        }
    }
}
