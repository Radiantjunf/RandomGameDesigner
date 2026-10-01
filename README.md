# Random Game Designer

A web app that helps you brainstorm game ideas using games from your Steam library. Enter a Steam profile, then combine inspiration from three randomly selected games: one for **Art & Camera**, one for **Mechanics**, and one for **Setting**.

The app displays each game's title, header image, and genres. Enable **Categories** to show its assigned inspiration role, or click **Get Idea** again for another combination.

## Technology

- ASP.NET Core targeting .NET 6 for the backend.
- React 17 with Create React App, Bootstrap, and Reactstrap for the frontend.
- Steam Web API for profile and library lookups, and the Steam Store API for game details.
- xUnit for backend tests and the Create React App test runner for frontend tests.

There is no database; the backend requests data from Steam.

## Prerequisites

- .NET 6 SDK.
- Node.js and npm. The repository does not pin a Node.js version; the frontend uses `react-scripts` 4.0.3.
- A valid Steam Web API key.
- A Steam profile with public profile and game details, and games available for lookup.
- Internet access for dependency installation and Steam requests.

## Configuration

Set your Steam API key in the terminal used to start the backend:

```powershell
$env:STEAM_API_KEY = "<your-steam-api-key>"
```

[DataAccess.cs](RandomGameDesigner/DataAccess.cs) reads this environment variable. Do not commit your key.

The authentication settings in [Program.cs](RandomGameDesigner/Program.cs) read `Authentication:Steam:ClientId` and `Authentication:Steam:ClientSecret` from ASP.NET Core configuration. Their environment variable equivalents are `Authentication__Steam__ClientId` and `Authentication__Steam__ClientSecret`; supply these when configuring sign-in.

Steam sign-in is configured separately in [Program.cs](RandomGameDesigner/Program.cs), while the sign-in button and callback use hard-coded Azure deployment URLs in [NavMenu.js](RandomGameDesigner/ClientApp/src/components/NavMenu.js) and [GameController.cs](RandomGameDesigner/Controllers/GameController.cs). For local use, enter your Steam profile directly. Local sign-in requires updating that configuration and those URLs.

## Run locally

Run these commands from the repository root:

```powershell
dotnet dev-certs https --trust
dotnet restore RandomGameDesigner.sln
dotnet run --project RandomGameDesigner/RandomGameDesigner.csproj --launch-profile RandomGameDesigner
```

The debug build installs frontend dependencies if `ClientApp/node_modules` is missing. The development SPA proxy starts the React server with `npm start`.

Open https://localhost:7214 to access the app through the development startup flow. The React development server runs at https://localhost:44470. The backend also listens on http://localhost:5214 and redirects HTTP requests to HTTPS.

If you start the frontend separately, run the following in another PowerShell terminal while the backend is running:

```powershell
cd RandomGameDesigner/ClientApp
npm install
$env:ASPNETCORE_HTTPS_PORT = "7214"
npm start
```

The port variable directs frontend API requests to the backend; without it or `ASPNETCORE_URLS`, the frontend proxy falls back to the IIS Express HTTP port `47217`. Do not start a second frontend server if the SPA proxy has already started one.

## Usage

1. Enter a Steam profile URL, 64-bit SteamID, or vanity ID.
2. Click **Get Idea** or press Enter.
3. Enable **Categories** to display the Art & Camera, Mechanics, and Setting labels.
4. Use the three games as inspiration for a new concept, then generate again as needed.

Accepted input formats:

```text
https://steamcommunity.com/id/<vanity-id>
https://steamcommunity.com/profiles/<steam-id>
<steam-id>
<vanity-id>
```

## API

`POST /game` accepts a JSON body:

```json
{
  "name": "<steam-id-or-vanity-id-or-profile-url>"
}
```

On success, it returns an array of three game objects with `topText`, `gameKey`, `title`, `tags`, `genres`, and `imageLink` fields. The controller samples up to six owned games, filters out invalid results, and uses the first three valid games.

Some lookup failures return a game object whose `title` contains an error message, such as `Error user not found`, `Error user is private`, or `Error can't find games`.

`GET /game` handles the Steam sign-in callback using the `openid.identity` query parameter and redirects to the configured deployment URL. Swagger is currently enabled only outside the Development environment.

## Tests

From the repository root:

```powershell
dotnet test RandomGameDesigner.sln
```

Backend tests include profile URL parsing and live Steam API calls. Tests that query Steam depend on credentials, network access, account visibility, and external data; one test asserts an exact game image URL that may change.

For the frontend:

```powershell
cd RandomGameDesigner/ClientApp
npm test -- --watchAll=false
npm run lint
```

The frontend currently includes an app-rendering smoke test.

## Build and publish

From the repository root:

```powershell
dotnet build RandomGameDesigner.sln
dotnet publish RandomGameDesigner/RandomGameDesigner.csproj --configuration Release --output ./publish
```

Publishing runs `npm install` and `npm run build`, then includes the frontend build under `wwwroot` in the publish output.

## Project layout

```text
RandomGameDesigner.sln
RandomGameDesigner/
  ClientApp/              React frontend and development proxy
  Controllers/            Game API and template weather endpoint
  Models/                 Game and request models
  DataAccess.cs           Steam API requests and response parsing
  IdeaGenerator.cs        Steam profile input helpers
  Program.cs              ASP.NET Core startup and authentication
RandomGameDesignerTests/  xUnit tests
```

## Current limitations

- If fewer than three valid games remain after sampling, the controller indexes past the available results instead of returning a friendly error.
- Empty input and unexpected Steam responses are not consistently handled.
- Steam sign-in uses deployment-specific URLs and needs additional configuration for local use or another host.
