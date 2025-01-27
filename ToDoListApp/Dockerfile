# Image versiyasini tanlash
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Loyihaning barcha fayllarini nusxalash
COPY ["ToDoListApp/ToDoListApp.csproj", "./"]

# Restore jarayonini amalga oshirish
RUN dotnet restore

# Loyihaning qolgan kodlarini nusxalash
COPY . ./
WORKDIR "/src/ToDoListApp"

# Loyihani build qilish
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Final bosqich: ishlashga tayyor bo'lish
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ToDoListApp.dll"]
