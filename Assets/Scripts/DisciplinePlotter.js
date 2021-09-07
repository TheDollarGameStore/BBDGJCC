// https://www.graphreader.com/plotter

let currentDiscipline = 500;
const eventList = [];

const COIN_COUNT = 10;
const COIN_MILLISECONDS = 10000;
const COIN_DELTA_DISCIPLINE = 25;
const COIN_MILLISECONDS_OFFSET = 10000;

const GARLIC_COUNT = 0;
const GARLIC_MILLISECONDS = 0;
const GARLIC_DELTA_DISCIPLINE = 0;
const GARLIC_MILLISECONDS_OFFSET = 0;

const TOMATO_COUNT = 0;
const TOMATO_MILLISECONDS = 0;
const TOMATO_DELTA_DISCIPLINE = 0;
const TOMATO_MILLISECONDS_OFFSET = 0;

const TURNIP_COUNT = 4;
const TURNIP_MILLISECONDS = 500;
const TURNIP_DELTA_DISCIPLINE = -50;
const TURNIP_MILLISECONDS_OFFSET = 0;

const BROCCOLI_COUNT = 6;
const BROCCOLI_MILLISECONDS = 4000;
const BROCCOLI_DELTA_DISCIPLINE = -100;
const BROCCOLI_MILLISECONDS_OFFSET = 12000;

function initializeTurnips() {
  for (let turnipCount = 0; turnipCount < TURNIP_COUNT; turnipCount++) {
    const turnipMilliseconds = turnipCount * TURNIP_MILLISECONDS + TURNIP_MILLISECONDS_OFFSET;
    eventList.push({
      milliseconds: turnipMilliseconds,
      deltaDiscipline: TURNIP_DELTA_DISCIPLINE
    });
    for (let coinCount = 0; coinCount < COIN_COUNT; coinCount++) {
      eventList.push({
        milliseconds: coinCount * COIN_MILLISECONDS + COIN_MILLISECONDS_OFFSET + turnipMilliseconds,
        deltaDiscipline: COIN_DELTA_DISCIPLINE
      });
    }
  }
}

function initializeTowersLinear(TOWER_COUNT, TOWER_MILLISECONDS, TOWER_DELTA_DISCIPLINE, TOWER_MILLISECONDS_OFFSET) {
  for (let towerCount = 0; towerCount < TOWER_COUNT; towerCount++) {
    eventList.push({
      milliseconds: towerCount * TOWER_MILLISECONDS + TOWER_MILLISECONDS_OFFSET,
      deltaDiscipline: TOWER_DELTA_DISCIPLINE
    });
  }
}

function plotDisciplineOverTime() {
  initializeTurnips();
  initializeTowersLinear(GARLIC_COUNT, GARLIC_MILLISECONDS, GARLIC_DELTA_DISCIPLINE, GARLIC_MILLISECONDS_OFFSET);
  initializeTowersLinear(TOMATO_COUNT, TOMATO_MILLISECONDS, TOMATO_DELTA_DISCIPLINE, TOMATO_MILLISECONDS_OFFSET);
  initializeTowersLinear(BROCCOLI_COUNT, BROCCOLI_MILLISECONDS, BROCCOLI_DELTA_DISCIPLINE, BROCCOLI_MILLISECONDS_OFFSET);
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

console.log(plotDisciplineOverTime());
