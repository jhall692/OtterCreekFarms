using Microsoft.EntityFrameworkCore;
using OtterCreekFarms.Api.Data;
using OtterCreekFarms.Shared.Models;

namespace OtterCreekFarms.Api.Services;

public interface IExpenseService
{
    List<ExpenseItemModel> GetExpenses();
    ExpenseItemModel Add(ExpenseItemModel expense);
    void Update(ExpenseItemModel expense);
    void Delete(int id);
}

public class ExpenseService(IDbContextFactory<AppDbContext> factory) : IExpenseService
{
    public List<ExpenseItemModel> GetExpenses()
    {
        using var db = factory.CreateDbContext();
        return db.Expenses.OrderBy(e => e.Id).ToList();
    }

    public ExpenseItemModel Add(ExpenseItemModel expense)
    {
        using var db = factory.CreateDbContext();
        db.Expenses.Add(expense);
        db.SaveChanges();
        return expense;
    }

    public void Update(ExpenseItemModel expense)
    {
        using var db = factory.CreateDbContext();
        db.Expenses.Update(expense);
        db.SaveChanges();
    }

    public void Delete(int id)
    {
        using var db = factory.CreateDbContext();
        var e = db.Expenses.Find(id);
        if (e is not null) { db.Expenses.Remove(e); db.SaveChanges(); }
    }
}
