# Project Design Document: FishBowl
  - **Created at**: 01/02/2024
  - **Author**: Luis Art. Guerra
## NOTE:
This document was the design of the initial concept of the demo. Even though the basic ideas are still there, the finished product is a little more complex. Don't get confused by the current state of the project. Enjoy!

## Project Concept

### Summary
The player will be presented a fish bowl with 3 fishes. Each fish will behave diferently depending how near the mouse cursor is close to them.

### 1. Player Control

You control a **[Mouse Cursor]** in this **[SIDE VIEW]** game where:

| User Input | Makes the player |
|------------|------------------|
|Mouse movement|Move the mouse cursor|

### 2. Basic Gameplay

During the game:

| Object type | Action type | Condition |
|-------------|-------------|------|
|Red Fish|Will get closer to the mouse cursor|if is at a defined distance from the cursor|
|Blue Fish|Will play dead|if touched by the mouse cursor|
|Blue Fish|Will recover after a defined time|if it has not been touched bu the mouse since|
|Green Fish|Will flee from the mouse cursor|if the mouse cursor gets close by a defined distance|

The goal of the game is to **watch the reactions of the fishes to the mouse cursor**.

### 3. Sounds & Effects

NO SOUND

### 4. Gameplay Mechanics

No game mechanics, this is a just a demo.

### 5. User Interface

The fishes and bowl will be visible from the start.

### 6. Other Features

### 7. Project Objectives

Objectives of the project:
- Link to github project
- Demonstration of Inheritance
  
  Each color fish inherist the basic behavior from Fish class.

- Demonstration of Polymorphism (overriding/overloading)

  Each color fish overrides the basic behavior swimming functionality from Fish class.

- Demonstration of Encapsulation (getters and setters)

  Speed and FollowDirection will be able to be changed by the child classes by using getters and setters to avoid issues for the Fish class. 

- Demonstration of abstraction (higher level methods to hide unnecessary details)

  The color fish does not care how basic swimming works. They just know they can implement a new behaviour and activate it from SetSwimType. They are just in charge of defining how the behavior swimming works.

### 8. Classes

### Fish Class - Inherits from: MonoBehaviour
This class contains all basic elements for the fish

|Property|Type|Description|
|-|-|-|
|Speed|Float|Swimming Speed|
|FollowDirection|Vector2|Defines the direction the fish will go when doing Behaviour Swimming|
|DistanceLimit|Float|The max distance the fish is allowed to swim from the center coordinates|
|CenterPoint|Vector2|The center coordinates|

|Function|Description|Modifier|
|-|-|-|
|DoBasicSwimming|Function called to make the fish swim in a random direction and defined speed|protected|
|DoBehaviourSwimming|Called when a fish detects a specific change. Base action: Just call DoBasicSwimming. Can be overrided by child class for new behaviour|virtual|
|Swim|Function that is called each frame with **Update** and executes a swimming function|private|
|SetSwimType|Accepts only two values: "Standard" or "Special". This values are used by Swim function to know which type of swimming to call|public|

### RedFish Class - Inherits from: Fish

This fish tries to get close to the mouse cursor if the mouse cursor gets to a specific distance.

|Function|Description|Modifier|
|-|-|-|
|DoBehaviourSwimming|Swim behavior of the red fish|override|

### GreenFish Class - Inherits from: Fish

This fish tries to flee from the mouse cursor if the mouse cursor gets to a specific distance.

|Function|Description|Modifier|
|-|-|-|
|DoBehaviourSwimming|Swim behavior of the green fish|override|

### BlueFish Class - Inherits from: Fish

This fish plays dead if the mouse cursor toches it.

|Function|Description|Modifier|
|-|-|-|
|DoBehaviourSwimming|Swim behavior of the blue fish|override|
