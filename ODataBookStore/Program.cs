using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using ODataBookStore.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});
builder.Services.AddControllers().AddOData(option => option.Select().Filter().Count().OrderBy().Expand().SetMaxTop(100).AddRouteComponents("odata",GetEdmModel()));
builder.Services.AddDbContext<BookStoreContext>(option => option.UseInMemoryDatabase("BookLists"));

static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder oDataConventionModelBuilder = new ODataConventionModelBuilder();
    oDataConventionModelBuilder.EntitySet<Book>("Books");
    oDataConventionModelBuilder.EntitySet<Press>("Presses");
    return oDataConventionModelBuilder.GetEdmModel();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseODataBatching();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
