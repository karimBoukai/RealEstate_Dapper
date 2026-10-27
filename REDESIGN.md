# Front-end redesign — build & run

The architecture is unchanged: ASP.NET Core 8, separate **RealEstate_Dapper_Api** (Dapper + SQL Server) and
**RealEstate_Dapper_UI** (Razor MVC + ViewComponents). Vite only compiles static assets into
`RealEstate_Dapper_UI/wwwroot/dist`; ASP.NET Core still does all hosting, routing and rendering.

## Prerequisites
- .NET 8 SDK, SQL Server with the existing `DbDapperRealEstate` database
- Node.js 20+ (only if you change front-end sources — the compiled `wwwroot/dist` is included)

## Configure
- API: `RealEstate_Dapper_Api/appsettings.json` → `ConnectionStrings:connection` (unchanged).
- UI: `RealEstate_Dapper_UI/appsettings.json` → `ApiSettingsKey:BaseUrl` (default `https://localhost:44387/api/`, unchanged).

## Build & run
```bash
# 1. front-end assets (optional — prebuilt output is in wwwroot/dist)
cd RealEstate_Dapper_UI/ClientApp
npm ci
npm run build          # or: npm run watch  (rebuilds on change)

# 2. API  (https://localhost:44387)
cd ../../RealEstate_Dapper_Api
dotnet run --launch-profile https

# 3. UI   (https://localhost:44391)
cd ../RealEstate_Dapper_UI
dotnet run --launch-profile https
```
Or open `RealEstate_Dapper_Api.sln` in Visual Studio and start both projects as before.

## Front-end layout
```
RealEstate_Dapper_UI/ClientApp/
  package.json, package-lock.json, vite.config.js
  src/styles/      tokens.css (design tokens, light/dark), base.css (shared components),
                   site.css, auth.css, dashboard.css, *.entry.css (bundles)
  src/site/        main.js (public entry), motion.js (GSAP), smooth.js (Lenis)
  src/dashboard/   main.js (sidebar, theme, tables, calendar), charts.js (Chart.js, code-split)
  src/islands/     mount.jsx + React islands (PropertyGallery, SplitTitle)
  src/vendor/react-bits/SplitText.jsx  (React Bits, MIT + Commons Clause — unmodified)
→ wwwroot/dist/  site.css, site.js, dashboard.css, dashboard.js, chunks/, assets/ (fonts)
```
Razor references the entries with `asp-append-version`, so cache-busting works with stable file names.
