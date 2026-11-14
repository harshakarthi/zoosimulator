// Talks to the Zoo Simulator API and draws the page.

const API_URL = '/api/zoo';

// One section is shown per species, in this order
const SPECIES = [
  { name: 'Monkey', title: 'Monkeys' },
  { name: 'Giraffe', title: 'Giraffes' },
  { name: 'Elephant', title: 'Elephants' },
];

// ---------- Calls to the API ----------

// Gets the latest zoo status and redraws the page
async function loadStatus() {
  try {
    const response = await fetch(API_URL);
    if (!response.ok) throw new Error();

    const status = await response.json();
    showStatus(status);
    showError('');
  } catch {
    showError('Could not connect to the API.');
  }
}

// Feeds the animals, then shows the result
async function feedAnimals() {
  try {
    const response = await fetch(API_URL + '/feed', { method: 'POST' });
    if (!response.ok) throw new Error();

    const result = await response.json();
    showStatus(result.status);
    showFeedMessage(result);
  } catch {
    showError('Feeding failed. Please try again.');
  }
}

// ---------- Drawing the page ----------

function showStatus(status) {
  setText('hour-length', `1 zoo hour = ${status.hourLengthSeconds} seconds`);
  setText('zoo-time', formatTime(status.zooTime));
  setText('next-hour', Math.round(status.secondsUntilNextHour) + 's');
  setText('alive', `${status.aliveCount} / ${status.totalCount}`);

  // Feeding is only useful if a living animal is below 100%
  const living = status.animals.filter(a => !a.isDead);
  const canFeed = living.some(a => a.health < 100);
  document.getElementById('feed-button').disabled = !canFeed;

  if (living.length === 0) {
    showHint('All the animals have died.');
  } else if (!canFeed) {
    showHint('All living animals are at full health.');
  } else {
    showHint('');
  }

  document.getElementById('species-sections').innerHTML =
    SPECIES.map(species => speciesSection(species, status.animals)).join('');
}

// The heading, rule and table for one species
function speciesSection(species, allAnimals) {
  const animals = allAnimals
    .filter(a => a.species === species.name)
    .sort((a, b) => sortGroup(a) - sortGroup(b));

  if (animals.length === 0) return '';

  const aliveCount = animals.filter(a => !a.isDead).length;

  return `
    <section class="species">
      <h2>${species.title} <small>(${aliveCount} / ${animals.length} alive)</small></h2>
      <p class="rule">${ruleFor(species.name, animals[0].threshold)}</p>
      <table>
        <thead>
          <tr><th>Name</th><th>Health</th><th>Status</th></tr>
        </thead>
        <tbody>
          ${animals.map(animalRow).join('')}
        </tbody>
      </table>
    </section>`;
}

// One table row: name, health bar and status label
function animalRow(animal) {
  const level = healthLevel(animal);

  return `
    <tr class="${animal.isDead ? 'dead-row' : ''}">
      <td>${animal.name}</td>
      <td>
        <div class="health">
          <div class="bar"><div class="bar-fill ${level}" style="width: ${animal.health}%"></div></div>
          <span>${animal.health.toFixed(2)}%</span>
        </div>
      </td>
      <td><span class="status ${level}">${animal.status}</span></td>
    </tr>`;
}

// Only shows the percentage for species that still have living animals
function showFeedMessage(result) {
  const livingSpecies = result.status.animals.filter(a => !a.isDead).map(a => a.species);

  const fed = result.feeds
    .filter(f => livingSpecies.includes(f.species))
    .map(f => `${f.species} +${f.percentage.toFixed(1)}%`)
    .join(', ');

  const message = document.getElementById('message');
  message.textContent = 'Animals fed: ' + fed;
  message.hidden = false;
}

// ---------- Small helpers ----------

// Order in each table: elephants that cannot walk, then alive, then dead
function sortGroup(animal) {
  if (animal.isDead) return 2;
  if (!animal.canWalk) return 0;
  return 1;
}

// Colour of the health bar and status: green, orange (close to the threshold) or grey (dead)
function healthLevel(animal) {
  if (animal.isDead) return 'dead';
  if (animal.health < animal.threshold + 10) return 'low';
  return 'ok';
}

// The rule shown under each heading, using the threshold from the API
function ruleFor(species, threshold) {
  if (species === 'Elephant') {
    return `Cannot walk below ${threshold}%, dies if still below ${threshold}% after the next hour`;
  }
  return `Dies if health drops below ${threshold}%`;
}

// "2026-11-13T14:00:00" -> "13/11/2026 14:00"
function formatTime(isoText) {
  const date = new Date(isoText);
  const pad = number => String(number).padStart(2, '0');
  return `${pad(date.getDate())}/${pad(date.getMonth() + 1)}/${date.getFullYear()} ` +
         `${pad(date.getHours())}:${pad(date.getMinutes())}`;
}

function setText(id, text) {
  document.getElementById(id).textContent = text;
}

function showHint(text) {
  const hint = document.getElementById('hint');
  hint.textContent = text;
  hint.hidden = text === '';
}

function showError(text) {
  const error = document.getElementById('error');
  error.textContent = text;
  error.hidden = text === '';
}

// ---------- Start ----------

document.getElementById('feed-button').addEventListener('click', feedAnimals);
loadStatus();
setInterval(loadStatus, 1000); // refresh every second
