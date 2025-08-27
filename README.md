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

The line <b>public static GameManager Instance;</b> in GameManager.cs &nbsp;is a common implementation of the <b>Singleton Pattern（单例模式）</b> in game development.

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

### Prototype 3

#### Feature:
- Add sounds and paricles when the character is running, jumping, and crashing.
- With the animations from the animator controller, the character will have 3 new anmations that occur int 3 different game states including running, jumping, and death.

#### Display:
<div style="display: flex; justify-content: center; align-items: center">
<img src="media/Prototype_3.gif" alt="示例图01">
</div>

#### Knowledge point:
<details>
<summary><b>ForceMode</b></summary>
ForceMode is an <b>enum</b> in Unity that determines how force is applied to a <b>RigidBody</b> via the <b>AddForce()</b> method. The 4 Types of ForceMode:

<b>1. ForceMode.Force (Default)</b>

Applies a continuous force. Best for simulating sustained forces like thrusters, wind, or custom gravity.

<b>2. ForceMode.Impluse</b>

Applies an instantaneous impulse. Best for simulating sudden impacts like jumps, collisions, or bullet hits.

<b>3. ForceMode.VelocityChange</b>

Directly modifies velocity. Best for precise velocity control, such as teleport-like movement or forced knockback.

<b>4. ForceMode.Acceleration</b>

Applies continuous acceleration. Best for simulating mass-agnostic acceleration, like spaceship thrust in zero-gravity.

</details>

<details>
<summary><b>GetComponent<>()</b></summary>

All components attached to a GameObject are stored in a tightly packed, linear array within the native(C++) memory managed by the Unity engine.<i>[The core part of the Unity engine is written in C++, which directly manages the computer's "native memory"].</i>

a navie <b>GetComponent<>()</b> operation would work like searching Array.This native approach has a time complexity of <b>O(n)</b>.

Unity does not use the naive approach for every call. It employs optimizations. leading to two primary execution paths:
1. The Fast Path: the engine knows the typical location or has a precomputed lookup key (like a hash) for these critcial components. This allows it to find them in near-constant time, <b>O(1)</b>, bypassing the need for a full array iteration（迭代）.
2. The Slow Path: It resembles the linear iteration process described above. However, Unity applies optimizations: 
- <b>Type Caching: </b>After the first successful <b>GetComponent<<none>MyCustomScript>() </b> call, the engine may cache the reference to that component type on that specific GameObject. Subsequent calls for the same type can then be served from the cache, making them much faster.<i>[Like Cache ?]</i>

- <b>Important Note:</b> This cache is per-type, not per-variable. Calling <b>GetComponent<<none>MyCustomScript>() </b>from two different scripts will likely hit the cache on the second call.
</details>

#### Notes:

▲ The line <b>private void OnCollisionEnter(Collision collision)</b> in PlayerController.cs shows that the prerequisite for two objects to collide is that at least one side has a Rigidbody and both sides have a Collider (and Is Trigger is not checked). 