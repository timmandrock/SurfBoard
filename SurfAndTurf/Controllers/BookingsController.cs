using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SurfAndTurf.Models;

public class BookingsController : Controller
{
    private readonly HttpClient _client;

    public BookingsController()
    {
        _client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5000") // Your API Base Address
        };
    }

    public async Task<IActionResult> Index()
    {
        HttpResponseMessage response = await _client.GetAsync("api/APIBookings");
        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var bookings = JsonSerializer.Deserialize<IEnumerable<Bookings>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(bookings);
        }
        return NotFound(); // or return an error view
    }

    public async Task<IActionResult> Details(int id)
    {
        HttpResponseMessage response = await _client.GetAsync($"api/APIBookings/{id}");
        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var booking = JsonSerializer.Deserialize<Bookings>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(booking);
        }
        return NotFound(); // or return an error view
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Bookings booking)
    {
        var jsonString = JsonSerializer.Serialize(booking);
        var stringContent = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PostAsync("api/APIBookings", stringContent);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction(nameof(Index));
        }
        return View(booking); // or return an error view
    }
    /*
     GET Requests: Sends a GET request and reads the JSON response as a string, then deserializes it into your model.
     POST Requests: Serializes the booking object into a JSON string, creates a StringContent object, and sends it with a POST request.
     HttpClient to manually send HTTP requests and handle the responses without using the Microsoft.AspNet.WebApi.Client package. Below is a simple example of how you can do this in your BookingsController. 
    This example assumes you have a Bookings class available to deserialize the JSON response into.
     */


    // Similar implementations for Edit, Delete, etc.
}
