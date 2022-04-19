# VR Slider

Unity 3D (C# + Oculus Integration + DOTween) prototype of [this interaction concept](https://dribbble.com/shots/16776679-Meta-Slider).

**Direct Manipulation:**
- ✅ [Touch Controller](https://dribbble.com/shots/17942856-VR-Slider-Prototype-Touch-Controller-Direct-Manipulation)
- ✅ [Virtual Hands]()

**Pointer:**
- 💡 Touch Controller
- 💡 Virtual Hands


## Touch Controller. Direct Manipulation

![VR Slider Prototype [Touch Controller, Direct Manipulation]](/demo1.gif)

In this version, you can directly interact with the UI element via VR Controllers. There are a couple of things you need to keep in mind when you use such interaction mechanics.

**Pros**

- It's super fun and feels like a real thing (immersive).
- Natural affordance. The mental model of real-world things perfectly matches pushing and pulling action with decreasing and increasing the counter (supported by haptic feedback). 

**Cons**

- The interaction mechanics are heavily restricted by arm movement and reliability of your back. Personally, I have no problems with the interactions, but I'm quite sure that people with different heights and body types will struggle with this version of the interaction. Potentially,  we can fix the issue by finding an optimal form factor for the UI — element sizes and margins (research needed) to decrease the dependency on body movement (respect people's stamina).


### Notes
- Maybe, it's worth adding a visual UI state for the hold-to-accelerate-the-counter action (the haptics works fine).
- Color coding for the hover state (emission)?


## Virtual Hands. Direct Manipulation

![VR Slider Prototype [Virtual Hands, Direct Manipulation]](/demo2.gif)

In this version, I used hand tracking as input. Pinching feels very natural but the lack of tactile feedback increases the number of cognitive efforts you spend to successfully interact with it: there is no haptic feedback; slow movement tracking; and low precision.

**Pros:**
— Fun.

**Cons:**
— Slow and low.

Can be used for video game cases, but for casual-routine apps accuracy and precision of hands-based interactions are too low at the moment, which makes it useless for professional usage. 


## Links

[Portfolio](https://olegfrolov.design/) | [Linkedin](https://www.linkedin.com/in/oleg-frolov-6a6a4752/) | [Dribbble](https://dribbble.com/Volorf) | [Twitter](https://www.twitter.com/volorf)
