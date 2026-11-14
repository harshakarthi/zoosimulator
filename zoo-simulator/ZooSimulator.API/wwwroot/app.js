const API_URL = '/api/zoo';

const SPECIES = [
    { name: 'Monkey', title: 'Monkeys' },
    { name: 'Giraffe', title: 'Giraffes' },
    { name: 'Elephant', title: 'Elephants' },
];

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

async function feedAnimals() {
    try {
        const response = await fetch(API_URL + '/feed', { method: 'POST' });
        if (!response.ok) throw new Error();

        const result = await response.json();
        showStatus(result.status);
    } catch {
        showError('Feeding failed. Please try again.');
    }
}

function showStatus(status) {
    setText('zoo-date', formatDate(status.zooTime));
    setText('zoo-time', formatTime(status.zooTime));
    setText('alive', `${status.aliveCount} / ${status.totalCount}`);

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

function speciesSection(species, allAnimals) {
    const animals = allAnimals
        .filter(a => a.species === species.name)
        .sort((a, b) => sortGroup(a) - sortGroup(b));

    if (animals.length === 0) return '';

    return `
    <section class="species">
      <h2>${species.title}</h2>
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

function sortGroup(animal) {
    if (animal.isDead) return 2;
    if (!animal.canWalk) return 0;
    return 1;
}

function healthLevel(animal) {
    if (animal.isDead) return 'dead';
    if (animal.species === 'Elephant') {
        return animal.canWalk ? 'ok' : 'low';
    }
    if (animal.health < animal.threshold + 10) return 'low';
    return 'ok';
}

function ruleFor(species, threshold) {
    if (species === 'Elephant') {
        return `Cannot walk below ${threshold}%, dies if still below ${threshold}% after the next hour`;
    }
    return `Dies if health drops below ${threshold}%`;
}

function formatDate(isoText) {
    const date = new Date(isoText);
    const pad = number => String(number).padStart(2, '0');
    return `${pad(date.getDate())}/${pad(date.getMonth() + 1)}/${date.getFullYear()}`;
}

function formatTime(isoText) {
    const date = new Date(isoText);
    const pad = number => String(number).padStart(2, '0');
    return `${pad(date.getHours())}:${pad(date.getMinutes())}`;
}

function showHint(text) {
    const hint = document.getElementById('hint');
    hint.textContent = text;
    hint.hidden = text === '';
}

function setText(id, text) {
    document.getElementById(id).textContent = text;
}

function showError(text) {
    const error = document.getElementById('error');
    error.textContent = text;
    error.hidden = text === '';
}


document.getElementById('feed-button').addEventListener('click', feedAnimals);
loadStatus();
setInterval(loadStatus, 1000);  
