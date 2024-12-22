using System;
using System.Collections.Generic;
class Concert
{
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; }
    public int AvailableSeats { get; set; }

    public Concert(string name, DateTime date, string location, int availableSeats)
    {
        Name = name;
        Date = date;
        Location = location;
        AvailableSeats = availableSeats;
    }
}
class Ticket
{
    public Concert Concert { get; set; }
    public decimal Price { get; set; }
    public int SeatNumber { get; set; }

    public Ticket(Concert concert, decimal price, int seatNumber)
    {
        Concert = concert;
        Price = price;
        SeatNumber = seatNumber;
    }
}
class BookingSystem
{
    private List<Concert> concerts = new List<Concert>();

    public void AddConcert(Concert concert)
    {
        concerts.Add(concert);
        Console.WriteLine($"Dodano koncert: {concert.Name} w {concert.Location}, {concert.Date.ToShortDateString()}.");
    }

    public void BookTicket(string concertName, int seatNumber, decimal price)
    {
        var concert = concerts.FirstOrDefault(c => c.Name == concertName);

        if (concert == null)
        {
            Console.WriteLine("Koncert nie znaleziony.");
            return;
        }

        if (concert.AvailableSeats <= 0)
        {
            Console.WriteLine("Brak dostępnych miejsc.");
            return;
        }

        concert.AvailableSeats--;
        Ticket ticket = new Ticket(concert, price, seatNumber);
        Console.WriteLine($"Zarezerwowano bilet na koncert {concert.Name}, miejsce {seatNumber}, cena: {price:C}.");
    }

    public void ShowConcerts(Func<Concert, bool> filter)
    {
        var filteredConcerts = concerts.Where(filter).ToList();

        if (!filteredConcerts.Any())
        {
            Console.WriteLine("Brak koncertów do wyświetlenia.");
            return;
        }

        foreach (var concert in filteredConcerts)
        {
            Console.WriteLine($"{concert.Name} - {concert.Location} - {concert.Date.ToShortDateString()} - Dostępne miejsca: {concert.AvailableSeats}");
        }
    }
}