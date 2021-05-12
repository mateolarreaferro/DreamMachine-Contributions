## Dream Machine Contributions

_"The **DREAM MACHINE** is a journey and a music driven adventure game where players interact with objects, dreamers and dream makers to solve puzzles and discover the story. The experience features recorded dialogue combined with real-time performance elements to enable the dreamer to complete their journey"._

[Dream Machine Web](https://www.dreammachine.live/)



Dream Machine is a Virtual Reality play conceptualized by Grammy-nominated visiting artist Nona Hendryx in collaboration with Lori Landay, Akito Van Troyer, and electronic music pioneer Laurie Anderson. 

Van Troyer is an Assistant Professor of Electronic Production and Design at Berklee College of Music and a Research Affiliate at MIT Media Lab who directs the audio visual team of this experience. As a member of his special seminar, my role consists in creating Unity C# solutions that enable game mechanics, audio reactive systems, visuals (using shader graph), and sound design implemented in native Unity and Wwise.



## Scripts

1. **AuroraManager.cs:** Audio Reactive system that creates an overall shape based on a series of given parameter, and animates individual objects inputed by user/developer by changing scale, rotation, and movement based on an audio clip.
2. **FFTGrandManager.cs:** System created based on AuroraManager.cs but for static objects.
3. **BirdBehaviour.cs:** Behaviour that triggers the AudioSource of a Gameobject based on randomness.
4. **BirdsManager.cs:** System that creates an area of GameObjects with AudioSources based on distance and height parameters and then assigns different audio clips that simulate an environment populated by different types of birds.
5. **HealthBarCrown:** Simple system that counts down the number of times this character can be hit before getting destroyed. Aditionally, it triggers events in Wwise and a Haptics system developed by Chris Lane. 
6. **PickUp.cs**: Simple system that counts the number of times the player picks up an object and updates the UI
7. **SelectionManager.cs**: System that uses raycast to detect and change the material of an 'interactable' object based on where the player is looking at.
8. **Spline.cs**: System that determines the shortest distance (based on a sequence of points in space) to a moving target. 
9. **SplineMover.cs**: Tracks the player and follows its movement through the predetermined sequence of points. This optimizes the use of audio sources in a scene as it only uses one that moves with the player.
10. **TimeKeeperTickTockManager.cs:** a metronome-like system that allows the increase or dicrease of the playing frequency of an audio clip.  



## Acknowledgments 
This tools were developed under the mentorship of [Chris Lane](https://chris-lane.com/) and [Akito Van Troyer](https://vantroyer.com/)