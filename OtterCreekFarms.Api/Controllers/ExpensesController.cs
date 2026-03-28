using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtterCreekFarms.Api.Services;
using OtterCreekFarms.Shared.Models;

namespace OtterCreekFarms.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/expenses")]
public class ExpensesController(IExpenseService expenseService) : ControllerBase
{
    [HttpGet]    public IActionResult GetAll()                          => Ok(expenseService.GetExpenses());
    [HttpPost]   public IActionResult Add(ExpenseItemModel e)          => Ok(expenseService.Add(e));
    [HttpPut("{id}")] public IActionResult Update(int id, ExpenseItemModel e) { e.Id = id; expenseService.Update(e); return Ok(); }
    [HttpDelete("{id}")] public IActionResult Delete(int id)           { expenseService.Delete(id); return Ok(); }
}
