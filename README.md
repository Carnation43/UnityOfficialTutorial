# UnityOfficialTutorial
做游戏是我的梦想，即使困难重重。

### Prototype 1

#### Features: 
- Allow the player to press a key on the keyboard to switch camera views.
- Transform this into a "local multiplayer" spilt-screen game with two cars where one car is controlled by WASD and the other is controlled by the arrow keys.

#### Display:
<div style="display: flex; justify-content: center; align-items: center">
<img src="media/Prototype_1.gif" alt="示例图01">
</div>

#### Knowledge point:
In Unity's Camera component, <b>Viewport Rect</b> defines the area of the screen where the camera's rendered output will be displayed.
Its main uses include:
1. Split-screen setups: For multiplayers games, multiple cameras can render to separate screen regions(e.g. two cameras each taking 50% width for side-by-side views).
2. Picture-in-picture effects: A secondary camera can render a small inset(e.g. a minimap or charcater close-up) within the main camera's view.
#### Notes:
▲ Replace <b>Update()</b> with <b>LateUpdate()</b> to prevent the jittering camera as the car drives down the road.

### Prototype 2

#### Feature:
- When the number of lives reaches 0, log "GAME OVER" and "Restart" button in the middle of the screen.
- Display a "hunger bar" on top of each of the animals. Each animal require different amounts of food.

#### Display:
<div style="display: flex; justify-content: center; align-items: center">
<img src="media/Prototype_2.gif" alt="示例图01">
</div>

#### Knowledge point:
<details>
<summary><b>Singleton Pattern Introduction</b></summary>

The line <b>public static GameManager Instance;</b> &nbsp;is a common implementation of the <b>Singleton Pattern（单例模式）</b> in game development.

The Singleton Pattern is a design patter that ensures a class has only one instance throughout the entire application and provides a global access point to that instance.

In games, certain core manager classes(like GameManager, UI Manager, AudioManager) need to be accessible globally and should have only one instance to prevent state conflicts.

<b>Props:</b>
- Global access point makes it easy to call from anywhere.
- Ensures onlu one instance exists, preventing state conflicts.

<b>Cons:</b>
- Overuse can lead to high code coupling（耦合）
- May hide dependencies between classes
- Can make unit testing more difficult
</details>

#### Notes:
▲ The line <b>private void OnTriggerEnter(Collider other)</b> illustrates that When A collides with B and the script is attached to B, the <b>other</b> parameter in the <b>OnTriggerEnter</b> function represents A. Morever, both game objects A and B need to have the <b>isTrigger</b> option checked, and one of them requires a <b>RigidBody</b> component.