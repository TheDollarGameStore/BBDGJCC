// https://www.graphreader.com/plotter

const COOLDOWN = {
  COIN: 7500,
  GARLIC: 0,
  TOMATO: 0,
  TURNIP: 0,
  BROCCOLI: 0
};

const DELTA_DISCIPLINE = {
  COIN: 25,
  TURNIP: -50,
  TOMATO: -150,
  GARLIC: -200,
  BROCCOLI: -100
};

let currentDiscipline;
const eventList = [];

function initializeTowersLinear(name, count, millisecondsBefore, millisecondsBetween) {
  if (millisecondsBetween < COOLDOWN[name]) {
    console.log(`${name} NOT YET OFF COOLDOWN`);
  }
  for (let index = 0; index < count; index++) {
    const milliseconds = millisecondsBefore + millisecondsBetween * index;
    eventList.push({
      milliseconds,
      deltaDiscipline: DELTA_DISCIPLINE[name]
    });
    if (name === 'TURNIP') {
      for (let coinIndex = 1; coinIndex <= 12; coinIndex++) {
        eventList.push({
          milliseconds: milliseconds + COOLDOWN.COIN * coinIndex,
          deltaDiscipline: DELTA_DISCIPLINE.COIN
        });
      }
    }
  }
}

function initializeLevel1() {
  currentDiscipline = 1000; // the cost of 10 broccolies at the time of writing
  initializeTowersLinear(
    name = 'BROCCOLI',
    count = 10, // 4 broccolies are needed to defeat 4 burgers, allow 6 errors
    millisecondsBefore = 5000, // allow the player to look around the screen a bit and figure out what to do
    millisecondsBetween = 2000 // allow the player to become dextrous and capable over time
  );
}

function initializeLevel2() {
  currentDiscipline = 500; // the cost of 4 turnips and 3 broccolies at the time of writing
  initializeTowersLinear(
    name = 'TURNIP',
    count = 4, // 4 are needed as initial turnips
    millisecondsBefore = 2000, // the player should be able to respond faster than before
    millisecondsBetween = 1000 // the player should be able to respond faster than before
  );
  initializeTowersLinear(
    name = 'BROCCOLI',
    count = 6, // at worst, 1 enemy spawns across every lane, over the time it takes to afford 6 broccolies
    millisecondsBefore = 6000, // allow the player to place turnips before spawning initial enemies
    millisecondsBetween = 6000 // allow the player to observe spawn lane before placement
  );
  initializeTowersLinear(
    name = 'TURNIP',
    count = 3,
    millisecondsBefore = 13000, // time before turnips can be afforded
    millisecondsBetween = 2000  // time before turnips can be afforded
  );
  initializeTowersLinear(
    name = 'TURNIP',
    count = 5, // fill up second column with turnips
    millisecondsBefore = 33000, // time before turnips can be afforded
    millisecondsBetween = 4000  // time before turnips can be afforded
  );
  initializeTowersLinear(
    name = 'BROCCOLI',
    count = 6, // increase enemy spawns to require 2 broccolies per lane
    millisecondsBefore = 42000, // time before broccolies can be afforded
    millisecondsBetween = 5000 // allow the player to observe spawn lane before placement
  );
}

function initializeLevel3() { }

function initializeLevel4() { }

function initializeLevel5() { }

function initializeLevel6() { }

function initializeEndless(level) { }

function plotDisciplineOverTime(level) {
  switch (level) {
    case 1:
      initializeLevel1();
      break;
    case 2:
      initializeLevel2();
      break;
    case 3:
      initializeLevel3();
      break;
    case 4:
      initializeLevel4();
      break;
    case 5:
      initializeLevel5();
      break;
    case 6:
      initializeLevel6();
      break;
    default:
      initializeEndless(level);
      break;
  }
  let xList = [0];
  let yList = [currentDiscipline];
  eventList.sort((a, b) => a.milliseconds - b.milliseconds);
  eventList.forEach(event => {
    xList.push(`${event.milliseconds / 1000}`.replace(',', '.'));
    xList.push(`${event.milliseconds / 1000}`.replace(',', '.'));
    yList.push(currentDiscipline);
    yList.push(currentDiscipline += event.deltaDiscipline);
  });
  return `{x:[${xList.join(',')}],y:[${yList.join(',')}]}`;
}

console.log(plotDisciplineOverTime(parseInt(process.argv[2])));
