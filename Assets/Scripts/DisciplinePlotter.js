// https://www.graphreader.com/plotter

const COOLDOWN = {
  COIN: 10000,
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
  currentDiscipline = 1200; // the cost of 12 towers at the time of writing
  initializeTowersLinear(
    name = 'BROCCOLI',
    count = 12, // two times the number of lanes means hopefully at least one tower per lane
    millisecondsBefore = 10000, // allow the player to look around the screen a bit and figure out what to do
    millisecondsBetween = 5000 // allow the player to become dextrous and capable over time
  );
}

function initializeLevel2() { }

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
