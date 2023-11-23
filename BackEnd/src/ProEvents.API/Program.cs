var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient("development", client =>
{
    if (builder.Environment.IsDevelopment())
    {
        // Desativar a validação do certificado em ambientes de desenvolvimento
        client.DefaultRequestHeaders.Add("Connection", "keep-alive");
    }
});
// Adiciona serviços ao contêiner.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração do redirecionamento HTTPS
builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = 5239; // Substitua pela porta HTTPS correta
});

var app = builder.Build();
// Configuração do pipeline de solicitação HTTP.
app.UseHttpsRedirection();

// Configuração do pipeline de solicitação HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection(); // Certifique-se de usar apenas em ambientes de produção
}

app.UseAuthorization();
app.MapControllers();

app.Run();
