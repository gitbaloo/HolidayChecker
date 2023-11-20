using System;
using System.Collections;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HolidayCalendar;
public class HolidayCalendar : IHolidayCalendar
{
    private readonly HttpClient _httpClient;
    private readonly string _apiBaseUrl = "https://api.sallinggroup.com";
    private readonly string _bearerToken = "c0825d79-42ef-418e-b224-931d714be77b";

    public HolidayCalendar(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _bearerToken);
    }

    //Method will only check whether selected date is a holiday but not whether it is a national holiday
    public async Task<bool> IsHoliday(DateTime date)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/v1/holidays/is-holiday?date={date:yyyy-MM-dd}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return bool.Parse(content);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Unexpected exception: {e.Message}");
            throw;
        }
    }

    //Method has to use the same API Get as the GetHolidays since through that we get additional information whether it is a national holiday and not just a holiday.
    public async Task<bool> IsNationalHoliday(DateTime date)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/v1/holidays?startDate={date:yyyy-MM-dd}&endDate={date:yyyy-MM-dd}&translation=en-us");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var holidays = JsonConvert.DeserializeObject<List<Holiday>>(content);

            return holidays.Any() && holidays[0].NationalHoliday;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Unexpected exception: {e.Message}");
            throw;
        }
    }

    //Method to find holidays between to chosen dates. Method will also include holidays that are NOT national holidays (IE Xmas Eve)
    public async Task<ICollection<Holiday>> GetHolidays(DateTime startDate, DateTime endDate)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/v1/holidays?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}&translation=en-us");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var holidayObjects = JsonConvert.DeserializeObject<List<Holiday>>(content);

            return holidayObjects;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Unexpected exception: {e.Message}");
            throw;
        }
    }
}
