using System.Diagnostics;
using Efcore_demo.Data;
using Efcore_demo.Entities;
using Microsoft.AspNetCore.Mvc;
using Efcore_demo.Models;

namespace Efcore_demo.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        var todos = _context.Todos.ToList();
        return View(todos);
    }

    public IActionResult Edit(long id)
    {
        var item = _context.Todos.FirstOrDefault(x => x.Id == id);
        item.Title = "Update";
        
        _context.Todos.Update(item);
        _context.SaveChanges();
        return View();
    }

    public IActionResult Delete(long id)
    {
        var item = _context.Todos.FirstOrDefault(x => x.Id == id);
        _context.Todos.Remove(item);
        _context.SaveChanges();
        return View();
    }
    
    
    public IActionResult Add()
    {
        var todo = new Todo()
        {
            Title = "x",
            Description = "y",
            IsCompleted = false
        };
        _context.Todos.Add(todo);
        _context.SaveChanges();
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}