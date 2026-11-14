# Zoo Simulator

This code builds a zoo simulator following the instructions in requirements.md file. 

The zoo has monkeys, giraffes and elephants. Every 20 seconds one zoo hour passes and the animals lose health. The user can feed the animals from a web page, and animals die based on the rules for their type.

---

### Architecture

Web browser   ->   ZooSimulator.API   ->   ZooSimulator.Core
(browser)	       (endpoints + clocks)		     (zoo rules)										

---

### How it works
1. When the API starts, ZooService creates 5 monkeys, 5 giraffes and 5 elephants at 100% health.
2. ZooSimulationService runs in the background. Every 20 seconds it calls AdvanceHour() which gives each animal a random health damage.
3. The web page calls GET /api/zoo every second and shows the date, time and every animal's health and status.
4. When the user clicks Feed animals, the page calls POST /api/zoo/feed. The zoo creates one random value per animal type and feeds every animal of that type. 
5. Each animal checks its own rule after every hour and decides if it can walk or if it dies. 

---

### Projects

| Project | What it does |
|---|---|
| ZooSimulator.Core | Contains the main zoo simulator logic. It sets up the animals, applies the hourly health damage, adds health when the animals are fed, and checks each animals's rules to see if it can walk or has died. As a class library, it can be plugged into any project. |
| ZooSimulator.API | ASP.NET Core web API with 2 endpoints: GET/api/zoo returns the zoo time and the status of every animal, and POST/api/zoo/feed feeds the animals. A background service runs the zoo clock, moving it one hour every 20 seconds. The API also serves the web page from wwwroot, where the user can see the animals and feed them. |
| ZooSimulator.Tests | xUnit tests for the animal rules and zoo. |

### Assumptions 
- Random values are decimals, from the minimum up to 0% to 20% for damage and 10% to 25% for feeding. 
- Elephant : if an elephant health goes back to 70% or above at any time during the hour, it is saved, even if it drops below 70% again at the next hour. It then gets another hour to recover. It only dies if health stays below 70% for the whole hour. 
- Dead animals stay dead. They do not lose health and cannot be fed. 
- Feeding only increases health, so it can never kill an animal.
- The zoo clock starts at 9:00 on the day the API starts.
- The requirement says 1 zoo hour passes every 20 real seconds, so 1 real second is treated as 3 zoo minutes. This lets the clock on the page show both hours and minutes.

---

### Current Design
- **Seperate layers** - The rules live in ZooSimulator.Core. The API only runs the clock and returns data. The web page only shows what the API returns. 
- **No hardcoded values** - Threshold (30,50,70), random ranges (0-20, 10-25), number of animals, hour length and opening hour are all in the Zoo section of appsettings.json, read into ZooConfiguration.
- **Background clock** - ZooSimulationService is a BackgroundService, so time passes even when nobody has the page open. 
- The timer and the Feed button can run at the same time, so ZooService uses a lock around every read and change.

---

### Application Behaviour
- The Feed button is disabled when all animals are dead or when every living animal is already at 100% health. 
- Living animals are shown first, dead animals last. For elephants, the ones that cannot walk are shown at the top, as they need feeding first. This way, the animals at risk are always the first thing the user sees. 
- Health bars are green when healthy, orange when close to the limit (within 10% for monkeys and giraffes, or when an elephant cannot walk) and grey when dead. 

---

### How to run 
- Use command line 
- From the folder with ZooSimulator.slnx
- ```bash dotnet run --project ZooSimulator.API ```

Then open **http://localhost:5223** in the browser.

---

### Future Improvements 
- Restart button to start a new zoo from the page when all animals have died, without restarting the API.
- More tests: API and web page tests, so the whole flow is tested, not just the zoo rules.
- Check the config at the startup so any wrong values in appsettings.json are caught early instead of zoo behaving incorrectly
- Event log to show what happened and when, so its easy to identify why an animal died.
