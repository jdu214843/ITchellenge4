# Build bosqichi uchun SDK image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# .csproj faylini qo'shish va restore qilish
COPY *.csproj ./
RUN dotnet restore

# Dastur kodini nusxalash va build qilish
COPY . ./
RUN dotnet publish -c Release -o out

# Run bosqichi uchun runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Portni ochish
EXPOSE 5000

# Dasturni ishga tushirish
ENTRYPOINT ["dotnet", "ITchallenge4.dll"]
