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

### Prototype 4

#### Feature:
- The enemy will chase the player around the island.
- A powerup will spawn in a random position on the map and last for 5 seconds after pickup, granting the player super strength that blasts away enemies.
- The Spawn Manager will operate in waves, spawning multiple enemies and a new powerup with each iteration.

#### Display:
<div style="display: flex; justify-content: center; align-items: center">
<img src="media/Prototype_4.gif" alt="示例图01">
</div>

#### Knowledge point:
<details>
<summary><b>Coroutine</b></summary>
Coroutines in Unity are specialized functions that can pause execution at specific points and resume later, making theme ideal for handling phased logic or delayed actions.

1. <b>The Nature of Coroutines: Iterator-Based State Machines</b>

    Coroutines are fundamentally built on Csharp's <b>IEnumerator</b> interface, functioning as pausable, resumable state machines:
    
    The <b>IEnumerator</b> interface has two critical members:
    
    - <b>object Current:</b> Returns the element at the current iteration position.
    - <b>bool MoveNext():</b> Advances the iterator to the next state; returns true if there's more to process, false when finished. 

    When a coroutine reached <b>yield return</b>: 1. It pauses execution of the current method. 2. It records the current execution position (context). 3. It returns control to the Unity Engine. 4. It resumes from the paused position once specific conditions are met.

2. <b>Relationship with the Main Thread</b>

    Coroutines do not create new threads; all code run on the main thread. Thus, time-consuming operations(such as complex calculations) within a coroutine will still block the main thread, causing stutters.

    The Unity engine checks the status of all active coroutines at specific points each frame(e.g., after <b>Update</b> and before <b>LateUpdate</b>).If resumption conditions are met(e.g., <b>yield return null</b> waiting for the next frame), the coroutine's remaining code continues executing.

3. <b>Key Differences between Threads and Coroutines</b>

    |      Differences      |      Thread      |      Coroutine      |
    | --------------------- | ---------------- | -------------------- |
    | Scheduling Method     | Kernel-level Preemptive (The OS forcibly allocates CPU resources, e.g., switching threads every 10ms) | User-level Cooperative (Coroutines decide when to yield CPU on their own, e.g., yielding when encountering IO blocking) |
    | Context Switch Overhead | High (Needs to enter kernel mode, save/restore thread context, and perform security checks) | Low (User-mode switch, only saves the coroutine’s "execution progress" such as the current line of code and variable values) |
    | Resource Footprint | Large (A single thread occupies several MBs of memory); a process can have at most a few thousand threads | Extremely small (A single coroutine occupies several KBs of memory); a single thread can host tens of thousands or even hundreds of thousands of coroutines |
    | Applicable Scenarios | CPU-intensive tasks (e.g., complex algorithms), I/O-intensive tasks (but with low efficiency) | I/O-intensive tasks (e.g., API calls, database queries, file reading/writing) |

4. <b>yield return Type</b>
    - yield return null: Pauses for one frame and resumes after Update and before LateUpdate
    - yield return new WaitForSeconds(): Pauses for a specified duration.
    - yield return new WaitForEndOfFrame(): Pauses until the end of the current frame.
    - yield return new WaitForFixedUpdate(): Pauses until the next FixedUpdate executioon
    - yield return StartCoroutine(AnotherCoroutine()): Pauses the current coroutine and resumes only after another coroutine finishes executing.
    - yield return www/yield return unityWebRequest: Pauses until a network request completes.

5. <b>Methods for "Starting and Stopping" Coroutines</b>

    <b>Starting Coroutines</b>: Can only be done via the <b>MonoBehaviour</b> method <b>StartCoroutine()</b>, with two calling styles:

    - Start without references: <b>StartCoroutine(MyCoroutine())</b> (cannot stop individually; only <b>StopAllCoroutines()</b> works).
    - Start with reference: <b>Coroutine coroutineRef = StartCoroutine(MyCoroutine())</b> (can stop the specific coroutine via <b>StopCoroutine(coroutineRef)</b>—more flexible).

    <b>Stopping Coroutines</b>: 

    - StopCoroutine(coroutineRef)
    - StopCoroutine("MyCoroutine")
    - StopAllCoroutines()
    - Hidden Rule: If the MonoBehaviour hosting the coroutine is destroyed (Destroy(gameObject)), all its unfinished coroutines will stop automatically. However, if the coroutine contains logic that "accesses destroyed objects," null reference errors may still occur—always check if the object is alive inside the coroutine.
    
</details>

#### Notes:

▲ The Line <b>Vector3 lookDirection = (player.transform.position - transform.position).normalized;</b> shows that using vector subtraction to calculate the direction vector between two objects (enemy and player) gives a vector that contains not only directional information but also has a length(magnitude) representing the straight-line distance betweeen the two objects.