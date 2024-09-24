using BankingApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>()
	.AddDefaultTokenProviders();

var app = builder.Build();

async Task CreateRoles(IServiceProvider serviceProvider)
{
	var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
	string[] roleNames = { "USER", "ADMIN" };

	foreach (var roleName in roleNames)
	{
		var roleExist = await roleManager.RoleExistsAsync(roleName);
		if (!roleExist)
		{
			await roleManager.CreateAsync(new IdentityRole(roleName));
		}
	}
}

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	try
	{
		await CreateRoles(services);
	}
	catch (Exception ex)
	{
		Console.WriteLine($"An error occurred while creating roles: {ex.Message}");
	}
}

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "Banking API V1");
	});
}

app.UseRouting();
app.UseAuthentication();
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.Run();
