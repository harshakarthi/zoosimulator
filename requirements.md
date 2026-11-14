## Zoo Simulator – Requirements

## Animals
- The zoo has 3 types of animals: Monkey, Giraffe and Elephant.
- The zoo opens with 5 of each type, so 15 animals in total.

## Health
- Every animal has a health value as a percentage.
- Every animal starts at 100% health.
- Health must be a decimal (floating point) value.

## Time
- 1 hour in the zoo passes every 20 seconds in real time.
- Every hour a random value between 0 and 20 is created for each animal.
- The animal's health goes down by that percentage of its current health.

## Feeding
- The user must be able to feed the animals.
- When feeding, 3 random values between 10 and 25 are created, one for each type of animal.
- All animals of the same type get the same value.
- The animal's health goes up by that percentage of its current health.
- Maximum health is 100%.

## When animals die
- Monkey: dies straight away if health goes below 30%.
- Giraffe: dies straight away if health goes below 50%.
- Elephant:
		- If health goes below 70%, it cannot walk.
		- If health is still below 70% after the next hour has passed, it dies.

## User interface
- Show the current status of each animal, and keep it updated regularly.
- Have an option to feed the animals.
- Show the current time in the zoo.